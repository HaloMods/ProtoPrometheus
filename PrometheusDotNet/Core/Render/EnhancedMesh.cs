using System;
using System.Diagnostics;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.IO;
using Prometheus.Core.Tags;
using Prometheus.Core.Util;
using Core.TagSystem.TagDefinitions;
using Prometheus.Core.Tags.Sbsp;
using System.Drawing;

namespace Prometheus.Core.Render
{
  public enum MeshFormat {Model, Bsp};
  public enum MeshPolyType {TriangleList, TriangleStrip};
  public class MODEL_VERT
  {
    public float[] it_pos = new float[3];
    public float[] it_norm = new float[3];
    public CustomVertex.PositionNormalTextured dxv = new CustomVertex.PositionNormalTextured();
    public short node1_index;
    public short node2_index;
    public float node1_weight;
    public float node2_weight;
    public void Load(ref BinaryReader br, byte[] buffer, float UScale, float VScale)
    {
      dxv.X = br.ReadSingle();
      dxv.Y = br.ReadSingle();
      dxv.Z = br.ReadSingle();
      it_pos[0] = dxv.X;
      it_pos[1] = dxv.Y;
      it_pos[2] = dxv.Z;

      dxv.Nx = br.ReadSingle();
      dxv.Ny = br.ReadSingle();
      dxv.Nz = br.ReadSingle();
      it_norm[0] = dxv.Nx;
      it_norm[1] = dxv.Ny;
      it_norm[2] = dxv.Nz;

      //skip past binormal and tan, we probably won't use them
      br.BaseStream.Seek(24, SeekOrigin.Current);

      dxv.Tu = br.ReadSingle() * UScale;
      dxv.Tv = br.ReadSingle() * VScale;

      node1_index = br.ReadInt16();
      node2_index = br.ReadInt16();
      node1_weight = br.ReadSingle();
      node2_weight = br.ReadSingle();
    }
    public void LoadCompressed(ref BinaryReader br, byte[] buffer, float UScale, float VScale)
    {
      /*
{
  float coord[3];
  UINT  CompNormal;
  UINT  CompBinormal;
  UINT  CompTangent;
  SHORT u;
  SHORT v;
  SHORT unk1;
  SHORT node0_weight;
}MODEL_COMPRESSED_VERT;
*/
      dxv.X = br.ReadSingle();
      dxv.Y = br.ReadSingle();
      dxv.Z = br.ReadSingle();
      it_pos[0] = dxv.X;
      it_pos[1] = dxv.Y;
      it_pos[2] = dxv.Z;

      //read and decompress normal
      uint compressed_norm = br.ReadUInt32();
      float[] tmp = new float[3];
      Compiler.Struct.DecompressIntToVector(compressed_norm, ref tmp);
      dxv.Nx = tmp[0];
      dxv.Ny = tmp[1];
      dxv.Nz = tmp[2];
      it_norm[0] = dxv.Nx;
      it_norm[1] = dxv.Ny;
      it_norm[2] = dxv.Nz;

      uint compressed_binormal = br.ReadUInt32();
      uint compressed_tangent = br.ReadUInt32();

      //read and decompress texture coords
      short u = br.ReadInt16();
      short v = br.ReadInt16();
      dxv.Tu = (u/32767f) * UScale;
      dxv.Tv = (v/32767f) * VScale;

      node1_index = br.ReadInt16();
      node1_weight = br.ReadInt16()/65535f;
    }
  }

  public struct PositionTexture2
  {
    public Vector3 position;
    public Vector3 normal;
    public float u1;
    public float v1;
    public float u2;
    public float v2;
    public static readonly VertexFormats Format = VertexFormats.Position | VertexFormats.Normal | VertexFormats.Texture2;

    public PositionTexture2(float x, float y, float z, float nx, float ny, float nz, float iu1, float iv1, float iu2, float iv2)
    {
      position = new Vector3(x,y,z);
      normal = new Vector3(nx,ny,nz);
      u1 = iu1;
      v1 = iv1;
      u2 = iu2;
      v2 = iv2;
    }
  }

	/// <summary>
	/// Enahnces the Mesh class to store source vertex/index data and automatically recreate
	/// buffers when the device resets.
	/// </summary>
  public class EnhancedMesh
  {
    private MeshFormat m_MeshFormat = MeshFormat.Model;
    private PrimitiveType m_PolyType = PrimitiveType.TriangleList;
    private int m_TriangleCount;
    public int m_VertexCount;

    //Shader stuff
    private int m_ShaderIndex = -1;
    private TagFileName m_ShaderName;
    private BOUNDING_BOX boundingBox = new BOUNDING_BOX();

    private MODEL_VERT[] m_ModelVerts;
    private PositionTexture2[] m_BspVerts;
    public short[] m_Indices;
    private short[] m_OriginalListIndices;
    private int[] m_SortIndices;
    private float[] m_SquaredTriangleDistances;
    
    private VertexBuffer m_VertexBuffer;
    private IndexBuffer m_IndexBuffer;
    private bool bUsesTwoSidedTransparentShader = false;

    public EnhancedMesh(int TriCount)
    {
      m_TriangleCount = TriCount;
    }
    public int TriangleCount
    {
      get
      {
        if(m_PolyType == PrimitiveType.TriangleStrip)
          return(m_Indices.Length-2);
        else
          return m_TriangleCount;
      }
    }
    public BOUNDING_BOX BoundingBox
    {
      get{return boundingBox;}
    }

		public MeshFormat GetFormat
		{
			get{ return this.m_MeshFormat; }
		}

    public void RegisterUtilityShader(UtilityShader us)
    {
      m_ShaderIndex = MdxRender.SM.GetUtilShaderIndex(us);
    }

    public void RegisterShader(TagFileName tfn)
    {
      m_ShaderName = tfn;
      m_ShaderIndex = MdxRender.SM.RegisterShader(tfn);

      //check to see if we need to do polygon sorting (two sided transparent)
      ShaderType st = MdxRender.SM.GetShaderType(tfn.TagClass);

      switch(st)
      {
        case ShaderType.Schi:
          TagSchi ts = MdxRender.SM.GetSchiShader(m_ShaderIndex);
          bUsesTwoSidedTransparentShader = ts.TwoSided;
          break;

        case ShaderType.Scex:
          TagScex tc = MdxRender.SM.GetScexShader(m_ShaderIndex);
          bUsesTwoSidedTransparentShader = tc.TwoSided;
          break;
        case ShaderType.Soso:
          TagSoso ss = MdxRender.SM.GetSosoShader(m_ShaderIndex);
          bUsesTwoSidedTransparentShader = ss.TwoSided;
          break;
      }

      if(bUsesTwoSidedTransparentShader)
      {
        ConvertTriStripToList();
        Trace.WriteLine("Two Sided Shader:  " + tfn.RelativePath);
      }
    }
    /// <summary>
    /// This function is called for loading code-generated models, not halo resources.
    /// </summary>
    /// <param name="PolyType"></param>
    /// <param name="data"></param>
    /// <param name="start"></param>
    /// <param name="count"></param>
    public void AllocateAndSetIndexBuffer(PrimitiveType PolyType, short[] data, int start, int count)
    {
      m_PolyType = PolyType;

      //copy the index data into the local EnhancedMesh index buffer (non-DX)
      int max_index = -1;
      m_Indices = new short[count];
      for(int i=0; i<count; i++)
      {
        m_Indices[i] = data[start+i];

        if(m_Indices[i] > max_index)
          max_index = m_Indices[i];
      }

      Trace.WriteLine(string.Format("max = {0} vert_count = {1} tri_count = {2}", 
        max_index, this.m_VertexCount, this.m_TriangleCount));

      //create the DX buffer and its callback
      m_IndexBuffer = new IndexBuffer(typeof(short), m_Indices.Length, MdxRender.Dev, Usage.WriteOnly, Pool.Default);
      m_IndexBuffer.Created += new EventHandler(this.OnIndexBufferCreate);
      OnIndexBufferCreate(m_IndexBuffer, null);
    }
    public void AllocateAndLoadIndexBuffer(PrimitiveType PolyType, int Count, ref BinaryReader br, bool bPerformWrapTest)
    {
      m_PolyType = PolyType;

      int i;
      short[] temp = new short[Count+3];

      for(i=0; i<Count; i++)
        temp[i] = br.ReadInt16();

      if(bPerformWrapTest)
      {
        temp[i] = br.ReadInt16();
        temp[i+1] = br.ReadInt16();
        temp[i+2] = br.ReadInt16();

        if((temp[i+2] == -1)&&(temp[i+1] == -1))
        {
          Count += 3;
        }
        else if(temp[i+1] == -1)
        {
          Count += 2;
          br.BaseStream.Position -= 2;
        }
        else
        {
          Count += 1;
          br.BaseStream.Position -= 4;
        }
      }

      m_Indices = new short[Count];
      for(i=0; i<Count; i++)
      {
        m_Indices[i] = temp[i];

        if(m_Indices[i] == -1)
          m_Indices[i] = m_Indices[i -1];
      }

      temp = null;

      m_IndexBuffer = new IndexBuffer(typeof(short), Count, MdxRender.Dev, Usage.WriteOnly, Pool.Default);
      m_IndexBuffer.Created += new EventHandler(this.OnIndexBufferCreate);
      OnIndexBufferCreate(m_IndexBuffer, null);
    }
    public void AllocateAndLoadH1XboxIndexBuffer(PrimitiveType PolyType, int Count, ref BinaryReader br, bool bPerformWrapTest)
    {
      m_PolyType = PolyType;

      int i;
			uint SpecAreaSize = (uint)(((Count /3)+1)*6);
			byte[] ba_Indices = new byte[SpecAreaSize];
			byte[] ba_Test = new byte[6];

			ba_Indices = br.ReadBytes((int)SpecAreaSize);
			ba_Test = br.ReadBytes(6);
			short[] temp;// = new short[(SpecAreaSize/3)];

			if (ba_Test[5] != 0xff)
			{
				br.BaseStream.Position -= 6;
				temp = new short[(SpecAreaSize/2)];
			}
			else
			{
				temp = new short[((SpecAreaSize/2) +3)];
				temp[(SpecAreaSize/2)] = BitConverter.ToInt16(ba_Test,0);
				temp[(SpecAreaSize/2)+1] = BitConverter.ToInt16(ba_Test,2);
				temp[(SpecAreaSize/2)+2] = BitConverter.ToInt16(ba_Test,4);
			}

      for(i=0; i<(SpecAreaSize/2); i++)
      {
        temp[i] = BitConverter.ToInt16(ba_Indices,i * 2);//br.ReadInt16();
        //Trace.WriteLine("index["+i.ToString()+"] = "+m_Indices[i].ToString()); 
      }

      //if(bPerformWrapTest)
      //{
       // temp[i] = br.ReadInt16();
       // temp[i+1] = br.ReadInt16();
       // temp[i+2] = br.ReadInt16();
				
			//	if(temp[i+2] == -1)
			//	{
			//		Count +=3;
			//	}
			//	else
			//	{
			//		br.BaseStream.Position -= 6;
			//	}
        //if((temp[i+2] == -1)&&(temp[i+1] == -1))
        //{
        //  Count += 3;
        //}
        //else if(temp[i+1] == -1)
        //{
        //  Count += 2;
        //  br.BaseStream.Position -= 2;
        //}
        //else
        //{
         // Count += 1;
         // br.BaseStream.Position -= 4;
        //}
      //}

      m_Indices = new short[temp.Length];
      for(i=0; i<temp.Length; i++)
      {
        m_Indices[i] = temp[i];

        if(m_Indices[i] == -1)
          m_Indices[i] = m_Indices[i -1];
      }

      temp = null;

      m_IndexBuffer = new IndexBuffer(typeof(short),m_Indices.Length, MdxRender.Dev, Usage.WriteOnly, Pool.Default);
      m_IndexBuffer.Created += new EventHandler(this.OnIndexBufferCreate);
      OnIndexBufferCreate(m_IndexBuffer, null);
    }

    public void ConvertTriStripToList()
    {
      //convert old strip data into tri list
      short[] temp = new short[m_Indices.Length];

      //copy index data to temp array
      for(int i=0; i<m_Indices.Length; i++)
        temp[i] = m_Indices[i];

      //reallocate index buffer for increased size
      m_Indices = new short[m_TriangleCount*3];
      
      //set up the sorting arrays
      m_OriginalListIndices = new short[m_TriangleCount*3];
      m_SquaredTriangleDistances = new float[m_TriangleCount];
      m_SortIndices = new int[m_TriangleCount];

      //regenerate triangle indices in list form
      for(int t=0; t<this.m_TriangleCount; t++)
      {
        m_Indices[3*t] = temp[t];
        m_Indices[3*t+1] = temp[t+1];
        m_Indices[3*t+2] = temp[t+2];

        //keep another copy for sorting purposes
        m_OriginalListIndices[3*t] = temp[t];
        m_OriginalListIndices[3*t+1] = temp[t+1];
        m_OriginalListIndices[3*t+2] = temp[t+2];
      }

      m_IndexBuffer.Created -= new EventHandler(this.OnIndexBufferCreate);
      m_IndexBuffer = null;
      m_IndexBuffer = new IndexBuffer(typeof(short), m_TriangleCount*3, MdxRender.Dev, Usage.WriteOnly, Pool.Default);
      m_IndexBuffer.Created += new EventHandler(this.OnIndexBufferCreate);
      OnIndexBufferCreate(m_IndexBuffer, null);
      this.m_PolyType = PrimitiveType.TriangleList;
    }
    /// <summary>
    /// Called prior to loading the vert data.  Allocates the memory arrays to hold
    /// the pristine vertex data.
    /// </summary>
    /// <param name="Format">Which type of vertex this mesh uses.</param>
    /// <param name="Count">How many verts are in the mesh.</param>
    public void AllocateVertexBuffer(MeshFormat Format, int Count)
    {
      m_MeshFormat = Format;
      m_VertexCount = Count;

      if(m_MeshFormat == MeshFormat.Model)
      {
        //local (non-DX) copy of verts
        m_ModelVerts = new MODEL_VERT[Count];
        for(int i=0; i<Count; i++)
          m_ModelVerts[i] = new MODEL_VERT();

        //allocate DX buffer (this is the one that will get recreated on device reset)
        m_VertexBuffer = new VertexBuffer(typeof(CustomVertex.PositionNormalTextured), m_ModelVerts.Length, MdxRender.Dev, 
          Usage.Dynamic | Usage.WriteOnly, CustomVertex.PositionNormalTextured.Format, Pool.Default);
      }
      else if(m_MeshFormat == MeshFormat.Bsp)
      {
        //local (non-DX) copy of verts
        m_BspVerts = new PositionTexture2[Count]; 
        for(int i=0; i<Count; i++)
          m_BspVerts[i] = new PositionTexture2();

        //allocate DX buffer (this is the one that will get recreated on device reset)
        m_VertexBuffer = new VertexBuffer(typeof(PositionTexture2), m_BspVerts.Length, MdxRender.Dev, 
          Usage.WriteOnly, PositionTexture2.Format, Pool.Default);
      }

      //add an event handler to recreate verts on device reset
      m_VertexBuffer.Created += new EventHandler(this.OnVertexBufferCreate);
    }

    /// <summary>
    /// Copies internal (local) vert data into the DX buffer.
    /// </summary>
    public void UpdateDxBuffer()
    {
      OnVertexBufferCreate(m_VertexBuffer, null);
    }

    public void LoadVertex(int index, float X, float Y, float Z, float Nx, float Ny, float Nz, float U, float V)
    {
      m_ModelVerts[index] = new MODEL_VERT();
      m_ModelVerts[index].dxv.X = X;
      m_ModelVerts[index].dxv.Y = Y;
      m_ModelVerts[index].dxv.Z = Z;
      m_ModelVerts[index].dxv.Nx = Nx;
      m_ModelVerts[index].dxv.Ny = Ny;
      m_ModelVerts[index].dxv.Nz = Nz;
      m_ModelVerts[index].dxv.Tu = U;
      m_ModelVerts[index].dxv.Tv = V;
      boundingBox.Update(X,Y,Z);
    }
    public void LoadH1ModelVertexData(int VertIndex, ref BinaryReader br, byte[] buffer, float UScale, float VScale, MapfileVersion ver)
    {
      m_ModelVerts[VertIndex] = new MODEL_VERT();

      if(ver == MapfileVersion.XHALO1)
        m_ModelVerts[VertIndex].LoadCompressed(ref br, buffer, UScale, VScale);
      else
        m_ModelVerts[VertIndex].Load(ref br, buffer, UScale, VScale);

      boundingBox.Update(m_ModelVerts[VertIndex].dxv.X, m_ModelVerts[VertIndex].dxv.Y, m_ModelVerts[VertIndex].dxv.Z);
    }

    public void SetBspVertexData(int index, Vector3 Position, Vector3 Normal, float u1, float v1)
    {
      m_BspVerts[index].position = Position;
      m_BspVerts[index].normal = Normal;
      m_BspVerts[index].u1 = u1;
      m_BspVerts[index].v1 = v1;
      boundingBox.Update(Position.X, Position.Y, Position.Z);
    }
    public void SetBspVertexLightmapUV(int index, float u2, float v2)
    {
      m_BspVerts[index].u2 = u2;
      m_BspVerts[index].v2 = v2;
    }
    public void RenderMesh()
    {
      int shader_pass_count = 0;
      bool bDoneProcessingShaders = false;

      //SortTransparentPolygons();
      MdxRender.Dev.Indices = m_IndexBuffer;
      MdxRender.Dev.SetStreamSource(0, m_VertexBuffer, 0);

      while(!bDoneProcessingShaders)
      {
        if(m_MeshFormat == MeshFormat.Model)
        {
          MdxRender.SM.SetupStates(m_ShaderIndex);
          bDoneProcessingShaders = MdxRender.SM.ActivateShader(m_ShaderIndex, shader_pass_count++);
          MdxRender.Dev.VertexFormat = CustomVertex.PositionNormalTextured.Format;
        }
        else
        {
          //todo, move shader setup into senv handler instead of outside in bsp code
          MdxRender.Dev.VertexFormat = PositionTexture2.Format;
          bDoneProcessingShaders = true; //only 1 pass
        }
      
        if(m_PolyType == PrimitiveType.TriangleList)
        {
          MdxRender.Dev.DrawIndexedPrimitives(PrimitiveType.TriangleList, 
            0,
            0, 
            m_VertexCount, 
            0,
            m_TriangleCount);
        }
        else if(m_PolyType == PrimitiveType.TriangleStrip)
        {
          MdxRender.Dev.DrawIndexedPrimitives(PrimitiveType.TriangleStrip, 
            0,
            0, 
            m_VertexCount, 
            0,
            m_TriangleCount);
        }
      }

      if(m_MeshFormat == MeshFormat.Model)
        MdxRender.SM.RestoreStates(m_ShaderIndex);
    }

    public void RenderSelected()
    {
      MdxRender.SM.ActivateShader(m_ShaderIndex, ShaderPass.First);
      //MdxRender.Dev.RenderState.FillMode = FillMode.WireFrame;
      MdxRender.Dev.Indices = m_IndexBuffer;
      MdxRender.Dev.SetStreamSource(0, m_VertexBuffer, 0);

      if(m_MeshFormat == MeshFormat.Model)
        MdxRender.Dev.VertexFormat = CustomVertex.PositionNormalTextured.Format;
      else
        MdxRender.Dev.VertexFormat = CustomVertex.PositionNormalTextured.Format;//todo: change to the custom 2uv format
      
      if(m_PolyType == PrimitiveType.TriangleList)
      {
        MdxRender.Dev.DrawIndexedPrimitives(PrimitiveType.TriangleList, 
          0,
          0, 
          m_VertexCount, 
          0,
          m_TriangleCount);
      }
      else if(m_PolyType == PrimitiveType.TriangleStrip)
      {
        MdxRender.Dev.DrawIndexedPrimitives(PrimitiveType.TriangleStrip, 
          0,
          0, 
          m_VertexCount, 
          0,
          m_TriangleCount*3-2);
      }

      //MdxRender.Dev.RenderState.FillMode = FillMode.Solid;
    }


    public float GetBoundingRadius()
    {
      if(this.m_MeshFormat == MeshFormat.Model)
      {
        float sqr_len = -1;
        float max_sl = -1;
        for(int v=0; v<m_ModelVerts.Length; v++)
        {
          sqr_len = m_ModelVerts[v].dxv.X*m_ModelVerts[v].dxv.X + 
            m_ModelVerts[v].dxv.Y*m_ModelVerts[v].dxv.Y +
            m_ModelVerts[v].dxv.Z*m_ModelVerts[v].dxv.Z;

          if(sqr_len > max_sl)
            max_sl = sqr_len;
        }

        float br = (float)Math.Sqrt(max_sl);
        return(br);
      }
      else
      {
        return -1;
      }
    }
    
    /// <summary>
    /// Copies index data into the index buffer.
    /// </summary>
    private void OnIndexBufferCreate(object sender, EventArgs e)
    {
      IndexBuffer buffer = (IndexBuffer)sender;
      buffer.SetData(m_Indices, 0, LockFlags.None);
    }
    
    /// <summary>
    /// Copies vertex data into the vertex buffer.
    /// </summary>
    private void OnVertexBufferCreate(object sender, EventArgs e)
    {
      VertexBuffer vb = (VertexBuffer)sender;
      GraphicsStream data = vb.Lock(0, 0, LockFlags.Discard);

      if(m_MeshFormat == MeshFormat.Model)
      {
        for(int v=0; v<m_ModelVerts.Length; v++)
          data.Write(m_ModelVerts[v].dxv);
      }
      else if(m_MeshFormat == MeshFormat.Bsp)
      {
        for(int v=0; v<m_BspVerts.Length; v++)
          data.Write(m_BspVerts[v]);
      }

      vb.Unlock();
    }

    /// <summary>
    /// Call this function to determine if a ray intersects with this mesh.
    /// It cycles through all the triangles to determine if any intersect
    /// the ray.
    /// </summary>
    /// <param name="Origin">The origin point of the ray</param>
    /// <param name="Direction">The direction of the ray</param>
    /// <returns>true if the ray intersects the mesh.</returns>
    //TODO: determine if we should have a limit to the ray distance
    //otherwise it could cause problems if we select something we can't see.
    public bool IntersectTest(Vector3 Origin, Vector3 Direction)
    {
      int v_base = 0;
      short[] triangle = new short[3];
      bool bIntersected = false;

      if(m_PolyType == PrimitiveType.TriangleList)
      {
        while((v_base+2) < m_Indices.Length)
        {
          triangle[0] = m_Indices[v_base];
          triangle[1] = m_Indices[v_base + 1];
          triangle[2] = m_Indices[v_base + 2];

          bIntersected = IntersectTriangle(Origin, Direction, triangle);

          if(bIntersected)
            break;

          v_base += 3;
        }
      }
		  /* Gren could replace this bottom half with:
		   * if(this.m_PolyType == PrimitiveType.TriangleList)
					i+=3;
				else
					i++;
					and remove the first comparison.
		   */
      else if(m_PolyType == PrimitiveType.TriangleStrip)
      {
        while((v_base+2) < m_Indices.Length)
        {
          triangle[0] = m_Indices[v_base];
          triangle[1] = m_Indices[v_base + 1];
          triangle[2] = m_Indices[v_base + 2];

          bIntersected = IntersectTriangle(Origin, Direction, triangle);

          if(bIntersected)
            break;

          v_base++;
        }
      }

      return(bIntersected);
    }

    private bool IntersectTriangle(Vector3 Origin, Vector3 Direction, short[] triangle)
    {
      bool bIntersected = true;
      float u = -33, v = -33;

      //Based on:  http://www.acm.org/jgt/papers/MollerTrumbore97/
      //           http://www.lighthouse3d.com/opengl/maths/index.php?raytriint

      //Calculate edge vectors
      Vector3 edge1, edge2;
      if(m_MeshFormat == MeshFormat.Model)
      {
        edge1 = new Vector3(m_ModelVerts[triangle[1]].dxv.X -  m_ModelVerts[triangle[0]].dxv.X,
          m_ModelVerts[triangle[1]].dxv.Y -  m_ModelVerts[triangle[0]].dxv.Y,
          m_ModelVerts[triangle[1]].dxv.Z -  m_ModelVerts[triangle[0]].dxv.Z);

        edge2 = new Vector3(m_ModelVerts[triangle[2]].dxv.X -  m_ModelVerts[triangle[0]].dxv.X,
          m_ModelVerts[triangle[2]].dxv.Y -  m_ModelVerts[triangle[0]].dxv.Y,
          m_ModelVerts[triangle[2]].dxv.Z -  m_ModelVerts[triangle[0]].dxv.Z);
      }
      else //bsp
      {
        edge1 = new Vector3();
        edge2 = new Vector3();
      }

      //Determine if the ray lies in the plane of the triangle
      Vector3 pvec = Vector3.Cross(Direction, edge2);

      float det = Vector3.Dot(edge1, pvec);

      if((det > -0.00001)&&(det < 0.00001))
      {
        bIntersected = false;
      }
      else
      {
        float inv_det = 1.0f/det;

        // calculate distance from vert0 to ray origin
        Vector3 tvec = new Vector3(Origin.X - m_ModelVerts[triangle[0]].dxv.X,
          Origin.Y - m_ModelVerts[triangle[0]].dxv.Y,
          Origin.Z - m_ModelVerts[triangle[0]].dxv.Z);

        // calculate U parameter and test bounds
        u = Vector3.Dot(tvec, pvec)*inv_det;

        if((u < 0.0)||(u > 1.0))
        {
          bIntersected = false;
        }
        else
        {
          // prepare to test V parameter
          Vector3 qvec = Vector3.Cross(tvec, edge1);

          // calculate V parameter and test bounds
          v = Vector3.Dot(Direction, qvec)*inv_det;

          if((v < 0.0)||(u+v > 1.0))
            bIntersected = false;

          // calculate t, ray intersects triangle
          //*t = DOT(edge2, qvec) * inv_det;
        }
      }

      return(bIntersected);
    }

		/// <summary>
		/// Ray-BSP intersect for lightmaps.
		/// </summary>
		/// <param name="Origin">Ray origin</param>
		/// <param name="Direction">Ray direction</param>
		/// <param name="Cull">Cull trinagle face</param>
		/// <param name="point">Point of intersection</param>
		/// <returns></returns>
		public bool RayBSPIntersectLight(Vector3 Origin, Vector3 Direction, bool Cull, out Vector3 point,
			out float[] UV, out float t)
		{
			if(this.m_BspVerts == null)
			{
				//throw new Exception("Not a bsp instance");
				point = new Vector3(-1,-1,-1);
				UV = new float[2]{-1,-1};
				t = -1;
				return false;
			}

			bool result = false;
			int i=0;
			float u =0;
			float v =0;
			float w =0xffffffffffffff;
			float[] tUV = new float[2];
			Vector3 tpoint = new Vector3();
			t=0;

			while(i<this.m_Indices.Length)
			{
				Vector3[] testTri = new Vector3[3]
				  {
					this.m_BspVerts[this.m_Indices[i]].position,
					this.m_BspVerts[this.m_Indices[i+1]].position,
					this.m_BspVerts[this.m_Indices[i+2]].position
				  };

				if(this.intersect_triangle(Origin, Direction, testTri, Cull, out t, out u, out v))
				{
						UV = new float[2]
						{
						(float)((this.m_BspVerts[this.m_Indices[i]].u2*u) + (this.m_BspVerts[this.m_Indices[i+1]].u2*v) + (this.m_BspVerts[this.m_Indices[i+2]].u2*(1-u-v))),
						(float)((this.m_BspVerts[this.m_Indices[i]].v2*u) + (this.m_BspVerts[this.m_Indices[i+1]].v2*v) + (this.m_BspVerts[this.m_Indices[i+2]].v2*(1-u-v)))
						};

						point = new Vector3(
							((float)(this.m_BspVerts[this.m_Indices[i]].position.X*u) + (this.m_BspVerts[this.m_Indices[i+1]].position.X*v) + (this.m_BspVerts[this.m_Indices[i+2]].position.X*(1-u-v))),
							((float)(this.m_BspVerts[this.m_Indices[i]].position.Y*u) + (this.m_BspVerts[this.m_Indices[i+1]].position.Y*v) + (this.m_BspVerts[this.m_Indices[i+2]].position.Y*(1-u-v))),
							((float)(this.m_BspVerts[this.m_Indices[i]].position.Z*u) + (this.m_BspVerts[this.m_Indices[i+1]].position.Z*v) + (this.m_BspVerts[this.m_Indices[i+2]].position.Z*(1-u-v)))
							);
						result = true;
                    
					if(t<w)
					{
						w=t;
						tUV = UV;
						tpoint = point;
					}
				}

				if(this.m_PolyType == PrimitiveType.TriangleList)
					i+=3;
				else
					i++;
			}
			if(result)
			{
				UV = tUV;
				point = tpoint;
				return true;
			}
			else
			{
				t=0;
				point = new Vector3(0,0,0);
				UV = new float[2]{u,v};
				return false;
			}
		} 

	  /// <summary>
	  /// Ray-BSP intersect for textures.
	  /// </summary>
	  /// <param name="Origin">Ray origin</param>
	  /// <param name="Direction">Ray direction</param>
	  /// <param name="Cull">Cull trinagle face</param>
	  /// <param name="point">Point of intersection</param>
	  /// <returns></returns>
	  public bool RayBSPIntersectTexture(Vector3 Origin, Vector3 Direction, bool Cull,
		  out float[] UV, out float t)
	  {
		  if(this.m_BspVerts == null)
		  {
			  //throw new Exception("Not a bsp instance");
			  UV = new float[2]{-1,-1};
			  t = -1;
			  return false;
		  }

		  bool result = false;
		  int i=0;
		  float u =0;
		  float v =0;
		  float[] tUV = new float[2];
		  t=0;

		  while(i<this.m_Indices.Length)
		  {
			  Vector3[] testTri = new Vector3[3]
				  {
					  this.m_BspVerts[this.m_Indices[i]].position,
					  this.m_BspVerts[this.m_Indices[i+1]].position,
					  this.m_BspVerts[this.m_Indices[i+2]].position
				  };

			  if(this.intersect_triangle(Origin, Direction, testTri, Cull, out t, out u, out v))
			  {
				  tUV = new float[2]
						{
							//(float)((this.m_BspVerts[this.m_Indices[i]].u2*u) + (this.m_BspVerts[this.m_Indices[i+1]].u2*v) + (this.m_BspVerts[this.m_Indices[i+2]].u2*(1-u-v))),
							//(float)((this.m_BspVerts[this.m_Indices[i]].v2*u) + (this.m_BspVerts[this.m_Indices[i+1]].v2*v) + (this.m_BspVerts[this.m_Indices[i+2]].v2*(1-u-v)))
              (float)((this.m_BspVerts[this.m_Indices[i+1]].u2*u) + (this.m_BspVerts[this.m_Indices[i+2]].u2*v) + (this.m_BspVerts[this.m_Indices[i]].u2*(1-u-v))),
              (float)((this.m_BspVerts[this.m_Indices[i+1]].v2*u) + (this.m_BspVerts[this.m_Indices[i+2]].v2*v) + (this.m_BspVerts[this.m_Indices[i]].v2*(1-u-v)))
            };

          Vector3 temp = new Vector3();
          temp.X = m_BspVerts[m_Indices[i+1]].position.X*u + (m_BspVerts[m_Indices[i+2]].position.X*v) + (m_BspVerts[m_Indices[i+0]].position.X*(1-u-v));
          temp.Y = m_BspVerts[m_Indices[i+1]].position.Y*u + (m_BspVerts[m_Indices[i+2]].position.Y*v) + (m_BspVerts[m_Indices[i+0]].position.Y*(1-u-v));
          temp.Z = m_BspVerts[m_Indices[i+1]].position.Z*u + (m_BspVerts[m_Indices[i+2]].position.Z*v) + (m_BspVerts[m_Indices[i+0]].position.Z*(1-u-v));

          TagBsp.nearestNeighborVerts = new CustomVertex.PositionColored[4];
          TagBsp.nearestNeighborVerts[0] = new CustomVertex.PositionColored(testTri[0], Color.Black.ToArgb());
          TagBsp.nearestNeighborVerts[1] = new CustomVertex.PositionColored(testTri[1], Color.Black.ToArgb());
          TagBsp.nearestNeighborVerts[2] = new CustomVertex.PositionColored(testTri[2], Color.Black.ToArgb());
          TagBsp.nearestNeighborVerts[3] = new CustomVertex.PositionColored(temp, Color.Black.ToArgb());


				  result = true;
			  }

			  if(this.m_PolyType == PrimitiveType.TriangleList)
				  i+=3;
			  else
				  i++;
		  }
		  if(result)
		  {
			  UV = tUV;
			  return true;
		  }
		  else
		  {
			  UV = new float[2]{-1,-1};
			  return false;
		  }
	  }
	  

	  /// <summary>
	  /// Working version
	  /// </summary>
	  /// <param name="orig"></param>
	  /// <param name="dir"></param>
	  /// <param name="verts"></param>
	  /// <param name="cull"></param>
	  /// <param name="t"></param>
	  /// <param name="u"></param>
	  /// <param name="v"></param>
	  /// <returns></returns>
	  public bool intersect_triangle(Vector3 orig, Vector3 dir, Vector3[] verts, bool cull, out float t, out float u, out float v)
	  {
		  t=0; u=0; v=0;
		  const float EPSILON = 0.000001f;
		  Vector3 edge1, edge2, tvec, pvec, qvec;
		  float det,inv_det;

		  /* find vectors for two edges sharing vert0 */
		  edge1 = new Vector3(verts[1].X - verts[0].X, verts[1].Y - verts[0].Y, verts[1].Z - verts[0].Z);
		  edge2 = new Vector3(verts[2].X - verts[0].X, verts[2].Y - verts[0].Y, verts[2].Z - verts[0].Z);

		  /* begin calculating determinant - also used to calculate U parameter */
		  pvec = Vector3.Cross(dir, edge2);

		  /* if determinant is near zero, ray lies in plane of triangle */
		  det = Vector3.Dot(edge1, pvec);
		  if(!cull)
		  {
			  if (det > -EPSILON && det < EPSILON)
				  return false;
		  }
		  else
		  {
			  if (det > -EPSILON)
				  return false;
		  }

		  //Just to be double sure:
		  //Plane plane = Plane.FromPoints(verts[0],verts[1],verts[2]);
		  //Vector3 point;
		  //if(Utility.RayIntersectPlane(orig,dir,plane, out point) != true)
			//  return false;

		  inv_det = 1.0f / det;

		  /* calculate distance from vert0 to ray origin */
		  tvec = new Vector3(orig.X - verts[0].X, orig.Y - verts[0].Y, orig.Z - verts[0].Z);

		  /* calculate U parameter and test bounds */
		  u = Vector3.Dot(tvec, pvec) * inv_det;
		  if (u < 0.0 || u > 1.0)
			  return false;

		  /* prepare to test V parameter */
		  qvec = Vector3.Cross(tvec, edge1);

		  /* calculate V parameter and test bounds */
		  v = Vector3.Dot(dir, qvec) * inv_det;
		  if (v < 0.0 || u + v > 1.0)
			  return false;

		  /* calculate t, ray intersects triangle */
		  t = Vector3.Dot(edge2, qvec) * inv_det;
		  return true;
	  }

		public bool RayAABBIntersect(Vector3 Origin, Vector3 Direction)
		{
			if(Utility.RayAABBIntersect(Origin, Direction, this.boundingBox))
				return true;
			else
				return false;
		}

    public void UpdateBoundingBox(ref BOUNDING_BOX bounds)
    {
      for(int v=0; v<m_ModelVerts.Length; v++)
        bounds.Update(m_ModelVerts[v].dxv.X, m_ModelVerts[v].dxv.Y, m_ModelVerts[v].dxv.Z); 
    }

    public void SortTransparentPolygons()
    {
      if(bUsesTwoSidedTransparentShader)
      {
        float dx,dy,dz;
        Vector3 cam_pos = MdxRender.Camera.Position;
        //m_SortIndices = new short[m_TriangleCount*3];
        //m_TriangleDistances = new float[m_TriangleCount];

        //calculate the arrays needed for sorting polys
        for(int t=0; t<this.m_TriangleCount; t++)
        {
          dx = cam_pos.X - m_ModelVerts[m_OriginalListIndices[3*t]].dxv.X; 
          dy = cam_pos.Y - m_ModelVerts[m_OriginalListIndices[3*t]].dxv.Y; 
          dz = cam_pos.Z - m_ModelVerts[m_OriginalListIndices[3*t]].dxv.Z;
          //calculate square of distance, this avoids expensive sqrt function
          m_SquaredTriangleDistances[t] = dx*dx + dy*dy + dz*dz;
          m_SortIndices[t] = t;
        }

        //perform the sort
        Utility.sort(m_SortIndices, m_SquaredTriangleDistances, 0, m_SortIndices.Length-1);

        //Update the index buffer
        for(int t=0; t<this.m_TriangleCount; t++)
        {
          m_Indices[3*(m_TriangleCount-t-1)] = m_OriginalListIndices[3*m_SortIndices[t]];
          m_Indices[3*(m_TriangleCount-t-1)+1] = m_OriginalListIndices[3*m_SortIndices[t]+1];
          m_Indices[3*(m_TriangleCount-t-1)+2] = m_OriginalListIndices[3*m_SortIndices[t]+2];
        }
        //update DX buffer
        OnIndexBufferCreate(m_IndexBuffer, null);
      }
    }

    #region Animation Code
    public void InverseNodeTransform(NodeTransform[] node_array)
    {
      Util.Matrix vert_transform = new Util.Matrix();
      //Microsoft.DirectX.Matrix vert_transform = new Microsoft.DirectX.Matrix();
      //Microsoft.DirectX.Vector3 vec = new Microsoft.DirectX.Vector3();

      for(int v=0; v<m_ModelVerts.Length; v++)
      {
        if(m_ModelVerts[v].node1_index != -1)
        {
          vert_transform = node_array[m_ModelVerts[v].node1_index].m_absolute;
              
          //inverse transform positions
          vert_transform.TranslateVect(ref m_ModelVerts[v].it_pos[0],
            ref m_ModelVerts[v].it_pos[1],
            ref m_ModelVerts[v].it_pos[2]);
          
          vert_transform.inverseRotateVect(ref m_ModelVerts[v].it_pos[0],
            ref m_ModelVerts[v].it_pos[1],
            ref m_ModelVerts[v].it_pos[2]);

          //inverse transform normals
          vert_transform.TranslateVect(ref m_ModelVerts[v].it_norm[0],
            ref m_ModelVerts[v].it_norm[1],
            ref m_ModelVerts[v].it_norm[2]);
              
          vert_transform.inverseRotateVect(ref m_ModelVerts[v].it_norm[0],
            ref m_ModelVerts[v].it_norm[1],
            ref m_ModelVerts[v].it_norm[2]);
        }
      }
    }
/*
    public void NodeTransform(NodeTransform[] node_array, bool bInverse)
    {
      Microsoft.DirectX.Matrix vert_transform;
      Microsoft.DirectX.Vector3 position = new Microsoft.DirectX.Vector3();
      Microsoft.DirectX.Vector3 normal = new Microsoft.DirectX.Vector3();
      
      for(int v=0; v<m_ModelVerts.Length; v++)
      {
        //only transform verts that are linked to a node
        if(m_ModelVerts[v].node1_index != -1)
        {
          if(bInverse)
            vert_transform = node_array[m_ModelVerts[v].node1_index].m_AbsoluteInverse;
          else
            vert_transform = node_array[m_ModelVerts[v].node1_index].m_absolute;

          //position transform
          position.X = m_ModelVerts[v].it_pos[0];
          position.Y = m_ModelVerts[v].it_pos[1];
          position.Z = m_ModelVerts[v].it_pos[2];
          position.TransformCoordinate(vert_transform);


          //normal transform
          normal.X = m_ModelVerts[v].it_norm[0];
          normal.Y = m_ModelVerts[v].it_norm[1];
          normal.Z = m_ModelVerts[v].it_norm[2];
          normal.TransformNormal(vert_transform);

          if(bInverse)
          {
            m_ModelVerts[v].it_pos[0] = position.X;
            m_ModelVerts[v].it_pos[1] = position.Y;
            m_ModelVerts[v].it_pos[2] = position.Z;
            m_ModelVerts[v].it_norm[0] = normal.X; 
            m_ModelVerts[v].it_norm[1] = normal.Y;
            m_ModelVerts[v].it_norm[2] = normal.Z;
          }
          else
          {
            m_ModelVerts[v].dxv.X = position.X;
            m_ModelVerts[v].dxv.Y = position.Y;
            m_ModelVerts[v].dxv.Z = position.Z;
            m_ModelVerts[v].dxv.Nx = normal.X;
            m_ModelVerts[v].dxv.Ny = normal.Y;
            m_ModelVerts[v].dxv.Nz = normal.Z;
          }
        }
      }
              
      if(!bInverse)
        UpdateDxBuffer();
    }
*/
    public void NodeTransform(NodeTransform[] node_array)
    {
      Prometheus.Core.Util.Matrix vert_transform;
      //Microsoft.DirectX.Matrix vert_transform = new Microsoft.DirectX.Matrix();
      //Microsoft.DirectX.Vector3 vec = new Microsoft.DirectX.Vector3();
      short node_index;
      Vector vec = new Vector();
      
      for(int v=0; v<m_ModelVerts.Length; v++)
      {
        node_index = m_ModelVerts[v].node1_index;
        //only transform verts that are linked to a node
        if(node_index != -1)
        {
          vert_transform = node_array[node_index].m_absolute;

          //position transform
          vec.load(m_ModelVerts[v].it_pos[0],
            m_ModelVerts[v].it_pos[1],
            m_ModelVerts[v].it_pos[2]);
          vec.transform(ref vert_transform.m_matrix);

          m_ModelVerts[v].dxv.X = vec.m_vector[0];
          m_ModelVerts[v].dxv.Y = vec.m_vector[1];
          m_ModelVerts[v].dxv.Z = vec.m_vector[2];

          //normal transform
          vec.load(m_ModelVerts[v].it_norm[0],
            m_ModelVerts[v].it_norm[1],
            m_ModelVerts[v].it_norm[2]);
          vec.transform(ref vert_transform.m_matrix);

          m_ModelVerts[v].dxv.Nx = vec.m_vector[0];
          m_ModelVerts[v].dxv.Ny = vec.m_vector[1];
          m_ModelVerts[v].dxv.Nz = vec.m_vector[2];
        }
      }
              
      UpdateDxBuffer();
    }
    #endregion
  }
}
