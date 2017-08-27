using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;
using Prometheus.Core.Tags;
using Prometheus.Core.Render;
using Prometheus.Core.Tags.Mode;

namespace Prometheus.Core.Tags.Mode
{
  public class H2_MODEL_HEADER 
  {
    public ushort ScriptRef1Index;
    public byte b_unk1;
	  public byte ScriptRef1Size;
    public uint[] unk1 = new uint[4];
    public H2REFLEXIVE Bounds = new H2REFLEXIVE();//56
    public H2REFLEXIVE Region = new H2REFLEXIVE();//16 (nested)
    public H2REFLEXIVE Geometry = new H2REFLEXIVE();//92 (nested)
    public H2REFLEXIVE r4 = new H2REFLEXIVE();//4
    public H2REFLEXIVE r5 = new H2REFLEXIVE();//12
		public ushort ScriptRef2Index;
		public byte b_unk2;
		public byte ScriptRef2Size;
		
    //public uint unk2a;
    public uint unk2b;
    public uint unk2c;
    public H2REFLEXIVE Nodes = new H2REFLEXIVE();//96
    public uint unk3a;
    public uint unk3b;
    public H2REFLEXIVE Markers = new H2REFLEXIVE();
    public uint unk4a;
    public uint unk4b;
    public H2REFLEXIVE Shaders = new H2REFLEXIVE();

    public uint[] unk5 = new uint[7];
    public byte[] ScriptRefs = new byte[0];

    //public REFLEXIVE Shaders = new REFLEXIVE();
    public void Load(ref BinaryReader br)
    {
	  ScriptRef1Index = br.ReadUInt16();
	  b_unk1 = br.ReadByte();
	  ScriptRef1Size = br.ReadByte();
      for(int x=0; x<unk1.Length; x++) 
        unk1[x] = br.ReadUInt32();

      Bounds.Load(ref br);
      Region.Load(ref br);
      Geometry.Load(ref br);
      r4.Load(ref br);
      r5.Load(ref br);
			ScriptRef2Index = br.ReadUInt16();
			b_unk2 = br.ReadByte();
			ScriptRef2Size = br.ReadByte();

      //unk2a = br.ReadUInt32();
      unk2b = br.ReadUInt32();
      unk2c = br.ReadUInt32();
      Nodes.Load(ref br);
      unk3a = br.ReadUInt32();
      unk3b = br.ReadUInt32();
      Markers.Load(ref br);
      Shaders.Load(ref br);

      //bone map

      for (int x=0; x<unk5.Length; x++) 
        unk5[x] = br.ReadUInt32();
		ScriptRefs = new byte[ScriptRef1Size + 1];
	  for (int x=0; x<ScriptRef1Size + 1;x++)
		ScriptRefs[x] = br.ReadByte();
    }
  }


  public class PCMODEL2_VERT
  {
    public float[] it_pos = new float[3];
    public float[] it_norm = new float[3];
    public CustomVertex.PositionNormalTextured dxv = new CustomVertex.PositionNormalTextured();
    public short node1_index;
    public short node2_index;
    public float node1_weight;
    public float node2_weight;
    public void Load(ref BinaryReader br, byte[] buffer,Single UScale,Single VScale )
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
      node1_weight = br.ReadSingle(); ;
      node2_weight = br.ReadSingle(); ;
    }
  }


  public class H2_MODEL_BOUNDS
  {
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minZ;
    public float maxZ;
    public float minU;
    public float maxU;
    public float minV;
    public float maxV;

    public void Load(ref BinaryReader br)
    {
      minX = br.ReadSingle();
      maxX = br.ReadSingle();
      minY = br.ReadSingle();
      maxY = br.ReadSingle();
      minZ = br.ReadSingle();
      maxZ = br.ReadSingle();
      minU = br.ReadSingle();
      maxU = br.ReadSingle();
      minV = br.ReadSingle();
      maxV = br.ReadSingle();
      br.BaseStream.Position += 16;
    }
  }

	public class H2_MODEL_NODE
	{
		public ushort ScriptRef1Index;
		public byte b_unk1;
		public byte ScriptRef1Size;
		byte[] unk = new byte[0x60 - 4];
		public string NodeName;
		public void Load(ref BinaryReader br)
		{
			ScriptRef1Index = br.ReadUInt16();
			b_unk1 = br.ReadByte();
			ScriptRef1Size = br.ReadByte();
			for (int x=0;x<unk.Length;x++)
				unk[x] = br.ReadByte();
		}
		public void LoadScriptRef(ref BinaryReader br)
		{
			for (int x=0;x<ScriptRef1Size+1;x++)
				NodeName +=(char)(br.ReadByte());
		}
	}

  public class H2_MODEL_REGION
  {
    public ushort ScriptRef1Index;
    public byte b_unk1;
    public byte ScriptRef1Size;
    //public int StringIndex;
    public short unk1;
    public short unk2;
    public H2REFLEXIVE Permutation = new H2REFLEXIVE();
    public string ScriptRef;

    public H2_MODEL_PERMUTATION[] Permutations;

    public void Load(ref BinaryReader br)
    {
      //StringIndex = br.ReadInt32();
	  ScriptRef1Index = br.ReadUInt16();
	  b_unk1 = br.ReadByte();
	  ScriptRef1Size = br.ReadByte();
      unk1 = br.ReadInt16();
      unk2 = br.ReadInt16();
      Permutation.Load(ref br);
    }
    public void LoadPermutations(ref BinaryReader br)
    {
      Permutations = new H2_MODEL_PERMUTATION[Permutation.Count];
      for(int i=0; i<Permutation.Count; i++)
      {
        Permutations[i] = new H2_MODEL_PERMUTATION();
        Permutations[i].Load(ref br);
      }
	  for(int i=0; i<Permutation.Count; i++)
	  {
		Permutations[i].LoadScriptRef(ref br);
	  }
    }
    public void LoadScriptRef(ref BinaryReader br)
    {
	  for (int x=0;x<ScriptRef1Size+1;x++)
	  {
		  ScriptRef += (char)(br.ReadByte());  
      }
    }
  }


  public class H2_MODEL_PERMUTATION
  {
    //public int StringIndex;
    public ushort ScriptRef1Index;
	public byte b_unk1;
    public byte ScriptRef1Size;
    public short unk1;
    public short unk2;
    public short[] LOD = new short[6];
    public string ScriptRef;
    public void Load(ref BinaryReader br)
    {
      //StringIndex = br.ReadInt32();
	  ScriptRef1Index = br.ReadUInt16();
	  b_unk1 = br.ReadByte();
	  ScriptRef1Size = br.ReadByte();
      //unk1 = br.ReadInt16();
      //unk2 = br.ReadInt16();

      for(int i=0; i<6; i++)LOD[i] = br.ReadInt16();
    }
	public void LoadScriptRef(ref BinaryReader br)
	{
		for (int x=0;x<ScriptRef1Size+1;x++)
		{
			ScriptRef += (char)(br.ReadByte());
		}
	}
  }

  public class H2_MODEL_GEOMETRY
  {
    public short VertexCount;
    public short IndexListCount;
		
    public int BytesPerVertex;
    public byte Type;

    public uint ModelResourceBlockOffset;
    public uint ModelResourceBlockSize;
    public uint ModelHeaderSize;
    public uint ModelRawSize;

    public H2REFLEXIVE Resources = new H2REFLEXIVE();
    public uint id;

    public H2_MODEL_RAW_DATA_DESCRIPTOR[] m_RawDescriptors;
    public H2_MODEL_BLKH ResourceHeader = new H2_MODEL_BLKH();
    public H2_MODEL_RSRC_INFO[] ResourceInfo = null;

    public short[] INDICES = null;
    PCMODEL2_VERT[] VERTS;
    public IndexBuffer IndexData = null;
    public VertexBuffer VertData = null;

    int TagVertexCount;
    int TagTriangleCount;
    uint RelativeOffset;
    bool UsesList;

    public void Load(ref BinaryReader br)
    {
      BytesPerVertex = br.ReadInt32();
      VertexCount = br.ReadInt16();
      IndexListCount = br.ReadInt16();
			
      br.BaseStream.Position += 12;

      Type = br.ReadByte(); 

      br.BaseStream.Position += 35;

      ModelResourceBlockOffset = br.ReadUInt32();
      ModelResourceBlockSize = br.ReadUInt32();
      ModelHeaderSize = br.ReadUInt32();
      ModelRawSize = br.ReadUInt32();

      Resources.Load(ref br);
      id = br.ReadUInt32();
      br.BaseStream.Position += 8;
    }
    public void LoadRawDescriptors(ref BinaryReader br)
    {
      //br.BaseStream.Position = Resources.Offset; 
      m_RawDescriptors = new H2_MODEL_RAW_DATA_DESCRIPTOR[Resources.Count];
      for(int i=0; i<Resources.Count; i++)
      {
        m_RawDescriptors[i] = new H2_MODEL_RAW_DATA_DESCRIPTOR();
        m_RawDescriptors[i].Load(ref br);
      }
    }
    public void LoadRawData(ref BinaryReader br, ref H2_MODEL_BOUNDS bBox)
    {
      RelativeOffset = (uint)br.BaseStream.Position + ModelHeaderSize + 8;
      TagVertexCount = VertexCount;
      ModelResourceBlockOffset = (uint)br.BaseStream.Position;
      ResourceHeader.Load(ref br);
      TagTriangleCount = (int)ResourceHeader.IndexChunkCount ; 
      //RawData = new byte[ModelResourceBlockSize - 0x78];  //0x78 = blkh size
      //RawData = br.ReadBytes((int)(ModelResourceBlockSize - 0x78));

      if(IndexListCount * 3 == ResourceHeader.IndexChunkCount ) 
      {
        UsesList = true;
      }
      else 
      {
        UsesList = false;
      }

      //Load Resource Info
      br.BaseStream.Position = RelativeOffset + m_RawDescriptors[0].offset;  //skip "rsrc" identifier
      ResourceInfo = new H2_MODEL_RSRC_INFO[ResourceHeader.InfoChunkCount];

      for(int i=0; i<ResourceHeader.InfoChunkCount; i++)
      {
        ResourceInfo[i] = new H2_MODEL_RSRC_INFO();
        ResourceInfo[i].Load(ref br);
      }


      //Indice Data
      br.BaseStream.Position = RelativeOffset + m_RawDescriptors[ResourceHeader.indexRSRC].offset;  //skip "rsrc" identifier
			
      INDICES = new short[ResourceHeader.IndexChunkCount];
      for(int i=0; i<ResourceHeader.IndexChunkCount; i++)
        INDICES[i] = br.ReadInt16();


      //Vertex Data
      br.BaseStream.Position = RelativeOffset + m_RawDescriptors[ResourceHeader.indexRSRC+2].offset;  //skip "rsrc" identifier
			
      VERTS = new PCMODEL2_VERT[VertexCount];

      for(int v=0; v<VertexCount; v++)
      {
        VERTS[v] = new PCMODEL2_VERT();
        VERTS[v].dxv.X = TagHalo2Model.Signed16ToFloat(br.ReadInt16(), bBox.minX, bBox.maxX );
        VERTS[v].dxv.Y = TagHalo2Model.Signed16ToFloat(br.ReadInt16(), bBox.minY, bBox.maxY );
        VERTS[v].dxv.Z = TagHalo2Model.Signed16ToFloat(br.ReadInt16(), bBox.minZ, bBox.maxZ );
        //VERTS[v].dxv.Nx = 0;
        //VERTS[v].dxv.Ny = 0;
        //VERTS[v].dxv.Nz = 1;


				
        if(BytesPerVertex == 1) 
        {
        }
        else if((BytesPerVertex == 2) || (BytesPerVertex == 3 && Type == 1)) 
        {
          br.BaseStream.Position+=2;
        }
        else if(BytesPerVertex == 3) 
        {
          if(Type == 0 || Type == 2) 
          {
            br.BaseStream.Position+=6;
          }
          else if(Type == 4) 
          {
            br.BaseStream.Position+=10;
          }
          else if(Type == 3) 
          {
            br.BaseStream.Position+=6;
          }
          else 
          { 
          }
        }
      }

      //Texture Coord Data
      br.BaseStream.Position = RelativeOffset + m_RawDescriptors[ResourceHeader.indexRSRC+3].offset;  //skip "rsrc" identifier
			
      for(int v=0; v<VertexCount; v++)
      {
        //VERTS[v].dxv.Tu = Halo2Model.Signed32ToFloat(br.ReadInt32(), bBox.minU , bBox.maxU );
        //VERTS[v].dxv.Tv = Halo2Model.Signed32ToFloat(br.ReadInt32(), bBox.minV , bBox.maxV );

        float uv1 = TagHalo2Model.Signed16ToFloat(br.ReadInt16(), bBox.minU , bBox.maxU);
        float uv2 = TagHalo2Model.Signed16ToFloat(br.ReadInt16(), bBox.minV , bBox.maxV);
        
        if(uv1 < 0.0F)uv1 += 1.0F;
        if(uv2 < 0.0F)uv2 += 1.0F;

        VERTS[v].dxv.Tu = uv1;
        VERTS[v].dxv.Tv = uv2;
      }

      br.BaseStream.Position = ModelResourceBlockOffset + ModelResourceBlockSize;

      //Create DirectX resources
      IndexData = new IndexBuffer(typeof(short), INDICES.Length, MdxRender.Dev, Usage.WriteOnly, Pool.Default);
      IndexData.Created += new EventHandler(this.OnIndexBufferCreate);
      OnIndexBufferCreate(IndexData, null);

      VertData = new VertexBuffer(typeof(CustomVertex.PositionNormalTextured), VertexCount, MdxRender.Dev, 
        Usage.Dynamic | Usage.WriteOnly, CustomVertex.PositionNormalTextured.Format, Pool.Default);
      VertData.Created += new EventHandler(this.OnVertexBufferCreate);
      OnVertexBufferCreate(VertData, null);
    }
  
    public void Draw()
    {
      try
      {
        MdxRender.Dev.Indices = IndexData;
        MdxRender.Dev.SetStreamSource(0, VertData, 0);
        MdxRender.Dev.RenderState.FillMode = FillMode.Solid ;
        MdxRender.Dev.VertexFormat = CustomVertex.PositionNormalTextured.Format;
		
        for(int i=0; i<ResourceHeader.InfoChunkCount; i++)
        {
          //Activate the shader for this geometry
          if(ResourceInfo[i].ShaderManagerIndex != -1)
            MdxRender.SM.ActivateShader(ResourceInfo[i].ShaderManagerIndex, ShaderPass.First);

          if (UsesList == true)
          {
            MdxRender.Dev.DrawIndexedPrimitives(PrimitiveType.TriangleList, 
              0,
              0, 
              TagVertexCount, 
              ResourceInfo[i].IndexStart,
              ResourceInfo[i].IndexCount/3);
          }
          else
          {
            MdxRender.Dev.DrawIndexedPrimitives(PrimitiveType.TriangleStrip, 
              0,
              0, 
              TagVertexCount, 
              ResourceInfo[i].IndexStart,
              ResourceInfo[i].IndexCount-2);
          }
        }
      }
      catch(Exception e)
      {
      }
    }
    private void OnIndexBufferCreate(object sender, EventArgs e)
    {
      IndexBuffer buffer = (IndexBuffer)sender;
      buffer.SetData(INDICES, 0, LockFlags.None);
    }
    private void OnVertexBufferCreate(object sender, EventArgs e)
    {
      VertexBuffer vb = (VertexBuffer)sender;
      GraphicsStream data = vb.Lock(0, 0, LockFlags.Discard);

      for(int v=0; v<VERTS.Length; v++)
        data.Write(VERTS[v].dxv);

      vb.Unlock();
    }
  }


  public class H2_MODEL_BONE
  {
  }

  public class H2_MODEL_MARKER_SUB
  {
    public short unk1;
    public short unk2;
    public float[] unk_coords = new float[8];
    public void Load(ref BinaryReader br)
    {
      unk1 = br.ReadInt16();
      unk2 = br.ReadInt16();
      for(int i=0; i<8; i++)
        unk_coords[i] = br.ReadSingle();
    }
  }

  public class H2_MODEL_MARKER
  {
    //public short unk1;
    //public short unk2;
		public ushort ScriptRef1Index;
		public byte b_unk1;
		public byte ScriptRef1Size;
    H2REFLEXIVE Sub = new H2REFLEXIVE();

    H2_MODEL_MARKER_SUB[] SubData;
		public string MarkerName;
    public void Load(ref BinaryReader br)
    {
      ScriptRef1Index = br.ReadUInt16();
			b_unk1 = br.ReadByte();
      ScriptRef1Size = br.ReadByte();
      Sub.Load(ref br);
    }
    public void LoadSub(ref BinaryReader br)
    {
      SubData = new H2_MODEL_MARKER_SUB[Sub.Count];
      for(int i=0; i<Sub.Count; i++)
      {
        SubData[i] = new H2_MODEL_MARKER_SUB();
        SubData[i].Load(ref br);
      }
    }
		public void LoadMarkerName(ref BinaryReader br)
		{
			for(int x=0;x<ScriptRef1Size+1;x++)
			{
				MarkerName += (char)(br.ReadByte());
			}
		}
  }


  public class H2_MODEL_SHADER
  {
    public int ShaderManagerIndex = -1;
    public H2TAG_REFERENCE TagRef = new H2TAG_REFERENCE();
  }

  public class H2_MODEL_RAW_DATA_DESCRIPTOR
  {
    public short unk1;
    public short unk2;
    public short unk3;
    public short unk4;
    public uint size;
    public uint offset;

    public void Load(ref BinaryReader br)
    {
      unk1 = br.ReadInt16();
      unk2 = br.ReadInt16();
      unk3 = br.ReadInt16();
      unk4 = br.ReadInt16();
      size = br.ReadUInt32();
      offset = br.ReadUInt32();
    }
  }


  public class H2_MODEL_BLKH
  {
    public uint InfoChunkCount;
    public uint Unk1ChunkCount;
    public uint Unk2ChunkCount;
    public uint IndexChunkCount;
    public uint Unk3ChunkCount;
    public uint BoneChunkCount;
    public uint indexRSRC;


    //30 32-bit elements
    public void Load(ref BinaryReader br)
    {
      br.BaseStream.Position += 8;
      InfoChunkCount = br.ReadUInt32();  //info size = 0x4c
      br.BaseStream.Position += 4;
      Unk1ChunkCount = br.ReadUInt32();
      br.BaseStream.Position += 4;
      Unk2ChunkCount = br.ReadUInt32();
      br.BaseStream.Position += 12;
      IndexChunkCount = br.ReadUInt32();
      br.BaseStream.Position += 20;
      Unk3ChunkCount = br.ReadUInt32();
      br.BaseStream.Position += 40;
      BoneChunkCount = br.ReadUInt32();
      br.BaseStream.Position += 8;

      indexRSRC = 0;
      if(InfoChunkCount > 0) indexRSRC++;
      if(Unk1ChunkCount > 0) indexRSRC++;
      if(Unk2ChunkCount > 0) indexRSRC++;
    }
  }


  public class H2_MODEL_RSRC_INFO
  {
    public ushort ShaderIndex;
    public ushort IndexStart;
    public ushort IndexCount;
    public int ShaderManagerIndex;
    public float MinX;
    public float MaxX;
    public float MinY;
    public float MaxY;
    public float MinZ;
    public float MaxZ;

    public void Load(ref BinaryReader br)
    {
      br.BaseStream.Position += 4;
      ShaderIndex = br.ReadUInt16();
      IndexStart = br.ReadUInt16();
      IndexCount = br.ReadUInt16();
      br.BaseStream.Position += 38;
      MinX = br.ReadSingle();
      MaxX = br.ReadSingle();
      MinY = br.ReadSingle();
      MaxY = br.ReadSingle();
      MinZ = br.ReadSingle();
      MaxZ = br.ReadSingle();      
    }
  }

	
  public class TagHalo2Model : TagBase
  {
    H2_MODEL_HEADER m_Header = new H2_MODEL_HEADER();
    H2_MODEL_BOUNDS[] m_ModelBounds;
    H2_MODEL_REGION[] m_Regions;
    H2_MODEL_GEOMETRY[] m_Geometry;
		H2_MODEL_NODE[] m_Nodes;
    H2_MODEL_MARKER[] m_Markers;
    H2_MODEL_SHADER[] m_Shaders;

    public BOUNDING_BOX m_BoundingBox = new BOUNDING_BOX();
    public bool m_bModelLoaded = false;
    private eLOD m_CurrentLOD = eLOD.CAUSES_FIRES;

    static public float Signed16ToFloat(short val, float min, float max) 
    {
      float percent = (((float)val + 32768) / 65535); 
      return (percent * (max - min)) + min;
    }
    static public float Signed32ToFloat(int val , float min ,float max)
    {
      return (float)((val + 2.147484E+9F) / 4294967300.0 * (max - min) + min);
    }
    static public float Unsigned16ToFloat(ushort val, float min, float max) 
    {
      return ((val / 65535) * (max - min)) + min;
    }
    public TagHalo2Model()
    {
    }


    public void LoadTagData()
    {
      int i;
      BinaryReader br = new BinaryReader(m_stream);
      // Load the header
      m_Header.Load(ref br);

      //Load Chunks for R1 (model bounds)
      m_ModelBounds = new H2_MODEL_BOUNDS[m_Header.Bounds.Count];
      for(i=0; i<m_Header.Bounds.Count; i++)
      {
        m_ModelBounds[i] = new H2_MODEL_BOUNDS();
        m_ModelBounds[i].Load(ref br);
      }
      m_BoundingBox.min[0] = m_ModelBounds[0].minX;
      m_BoundingBox.min[1] = m_ModelBounds[0].minY;
      m_BoundingBox.min[2] = m_ModelBounds[0].minZ;
      m_BoundingBox.max[0] = m_ModelBounds[0].maxX;
      m_BoundingBox.max[1] = m_ModelBounds[0].maxY;
      m_BoundingBox.max[2] = m_ModelBounds[0].maxZ;

      //Load Chunks/Subchunks for R2 (regions)
      m_Regions = new H2_MODEL_REGION[m_Header.Region.Count];
      for(i=0; i<m_Header.Region.Count; i++)
      {
        m_Regions[i] = new H2_MODEL_REGION();
        m_Regions[i].Load(ref br);
      }

	  for(i=0; i<m_Header.Region.Count; i++)
	  {
	    m_Regions[i].LoadScriptRef(ref br);
		m_Regions[i].LoadPermutations(ref br);
	  }
      //Load Chunks/Subchunks for R3 (geometry)
      m_Geometry = new H2_MODEL_GEOMETRY[m_Header.Geometry.Count];
      for(i=0; i<m_Header.Geometry.Count; i++)
      {
        m_Geometry[i] = new H2_MODEL_GEOMETRY();
        m_Geometry[i].Load(ref br);
      }

      long geometry_start_pos = br.BaseStream.Position;

      for(i=0; i<m_Header.Geometry.Count; i++)
        br.BaseStream.Position += m_Geometry[i].ModelResourceBlockSize;

      for(i=0; i<m_Header.Geometry.Count; i++)
        m_Geometry[i].LoadRawDescriptors(ref br);

      long geometry_end_pos = br.BaseStream.Position;

      br.BaseStream.Position = geometry_start_pos;
      for(i=0; i<m_Header.Geometry.Count; i++)
        m_Geometry[i].LoadRawData(ref br, ref m_ModelBounds[0]);

      br.BaseStream.Position = geometry_end_pos;

      //skip past R4, R5
      br.BaseStream.Position += (m_Header.r4.Count*4);
      br.BaseStream.Position += (m_Header.r5.Count*12);
      //load Nodes
      //br.BaseStream.Position += (m_Header.Nodes.Count*96);
			m_Nodes = new H2_MODEL_NODE[m_Header.Nodes.Count];
			for(i=0;i<m_Header.Nodes.Count;i++)
			{
				m_Nodes[i] = new H2_MODEL_NODE();
				m_Nodes[i].Load(ref br);
			}
			for(i=0;i<m_Header.Nodes.Count;i++)
			{
				m_Nodes[i].LoadScriptRef(ref br);
			}
      //load Markers
      m_Markers = new H2_MODEL_MARKER[m_Header.Markers.Count];
      for(i=0; i<m_Header.Markers.Count; i++)
      {
        m_Markers[i] = new H2_MODEL_MARKER();
        m_Markers[i].Load(ref br);
      }

			for(i=0; i<m_Header.Markers.Count; i++)
			{
				m_Markers[i].LoadMarkerName(ref br);
				m_Markers[i].LoadSub(ref br);
			}
      //load Shaders
      m_Shaders = new H2_MODEL_SHADER[m_Header.Shaders.Count];
      for(i=0; i<m_Header.Shaders.Count; i++)
      {
        br.BaseStream.Position += 8;
        m_Shaders[i] = new H2_MODEL_SHADER();
        m_Shaders[i].TagRef.Load(ref br);
        br.BaseStream.Position += 16;
      }

      for(i=0; i<m_Header.Shaders.Count; i++)
      {
        m_Shaders[i].TagRef.ReadString(ref br);
        m_Shaders[i].ShaderManagerIndex = MdxRender.SM.RegisterShader(new TagFileName(m_Shaders[i].TagRef.data, "dahs", this.m_PromHeader.GameVersion)); 
      }

      //update data structs so we can get the shader activated during draw
      for(i=0; i<m_Header.Geometry.Count; i++)
      {
        for(int j=0; j<m_Geometry[i].ResourceInfo.Length; j++)
        {
          m_Geometry[i].ResourceInfo[j].ShaderManagerIndex = 
            m_Shaders[m_Geometry[i].ResourceInfo[j].ShaderIndex].ShaderManagerIndex;
        }
      }

      this.m_bModelLoaded = true;
    }
    public void DrawModel()
    {
      int geo_index = 0;
      if(this.m_bModelLoaded)
      {
        for(int region=0; region<m_Header.Region.Count; region++)
        {
          geo_index = m_Regions[region].Permutations[0].LOD[(int)m_CurrentLOD];
          m_Geometry[geo_index].Draw();
        }
      }
    }
    public void SetLOD(eLOD lod)
    {
      m_CurrentLOD = lod;
    }
  }
}



#region TEST_CODE
/*
using System;
using System.IO;

namespace ModelLib
{
  /// <summary>
  /// Summary description for model.
  /// </summary>
  public class model
  {
    public modelMeta meta;
#region Raw Structs
    public struct rawHeader 
    {
      public uint infoChunks;
      public uint unkChunks;
      public uint unk2Chunks;
      public uint indexChunks;
      public uint unk3Chunks;
      public uint boneChunks;
      public uint indexRSRC;
      public void Read(ref BinaryReader inRead) 
      {
        inRead.BaseStream.Seek(8, SeekOrigin.Current);
        infoChunks = inRead.ReadUInt32();
        inRead.BaseStream.Seek(4, SeekOrigin.Current);
        unkChunks = inRead.ReadUInt32();
        inRead.BaseStream.Seek(4, SeekOrigin.Current);
        unk2Chunks = inRead.ReadUInt32();
        inRead.BaseStream.Seek(12, SeekOrigin.Current);
        indexChunks = inRead.ReadUInt32();
        inRead.BaseStream.Seek(20, SeekOrigin.Current);
        unk3Chunks = inRead.ReadUInt32();
        inRead.BaseStream.Seek(40, SeekOrigin.Current);
        boneChunks = inRead.ReadUInt32();
        inRead.BaseStream.Seek(8, SeekOrigin.Current);
        indexRSRC = 0;
        if(infoChunks > 0) indexRSRC++;
        if(unkChunks > 0) indexRSRC++;
        if(unk2Chunks > 0) indexRSRC++;
      }
    }
    public struct rawInfo 
    {
      public uint indexCount;
      public range x;
      public range y;
      public range z;
      public void Read(ref BinaryReader inRead) 
      {
        inRead.BaseStream.Seek(8, SeekOrigin.Current);
        indexCount = inRead.ReadUInt16();
        inRead.BaseStream.Seek(38, SeekOrigin.Current);
        x.min = inRead.ReadSingle();
        x.max = inRead.ReadSingle();
        y.min = inRead.ReadSingle();
        y.max = inRead.ReadSingle();
        z.min = inRead.ReadSingle();
        z.max = inRead.ReadSingle();
      }
    }
    public struct vertex 
    {
      public float x;
      public float y;
      public float z;
      public float u;
      public float v;
      public short normalX;
      public short normalY;
      public short normalZ;
      public ushort[] bone;
      public float[] weight;
    }
    public struct rawModel 
    {
      public rawHeader header;
      public rawInfo[] infos;
      public ushort[] indices;
      public vertex[] vertices;
      public byte[] boneMap;
      public ushort MaxBoneCount;
      public void Read(ref BinaryReader inRead, piece parent, boundingBox box) 
      {
        header.Read(ref inRead);
        infos = new rawInfo[header.infoChunks];
#region Read Header
        inRead.BaseStream.Seek(4, SeekOrigin.Current);
        for(int i=0;i<header.infoChunks;i++) 
        {
          infos[i] = new rawInfo();
          infos[i].Read(ref inRead);
        }
#endregion
#region Read Indices
        inRead.BaseStream.Seek(parent.offset + parent.rsrcHeaderSize + parent.rsrcs[header.indexRSRC].offset + 8, SeekOrigin.Begin);
        indices = new ushort[header.indexChunks];
        for(int i=0;i<header.indexChunks;i++) 
        {
          indices[i] = inRead.ReadUInt16();
        }
#endregion
#region Read Bone Map
        if(header.boneChunks > 0) 
        {
          inRead.BaseStream.Seek(parent.offset + parent.rsrcHeaderSize + parent.rsrcs[parent.rsrcCount - 1].offset + 8, SeekOrigin.Begin);
          boneMap = new byte[header.boneChunks];
          boneMap = inRead.ReadBytes((int)header.boneChunks);
        }
#endregion
#region Read Vertices
        inRead.BaseStream.Seek(parent.offset + parent.rsrcHeaderSize + parent.rsrcs[header.indexRSRC + 2].offset + 8, SeekOrigin.Begin);
        vertices = new vertex[parent.vertices];
        for(int i=0;i<parent.vertices;i++) 
        {
          vertices[i] = new vertex();
          vertices[i].x = Signed16ToFloat(inRead.ReadInt16(), box.x.min, box.x.max);
          vertices[i].y = Signed16ToFloat(inRead.ReadInt16(), box.y.min, box.y.max);
          vertices[i].z = Signed16ToFloat(inRead.ReadInt16(), box.z.min, box.z.max);
          if(parent.bytesPerVert == 1) 
          {
            vertices[i].bone = new ushort[0];
            vertices[i].weight = new float[0];
            // 6 byte model, no bone info.
          }
          else if((parent.bytesPerVert == 2) || (parent.bytesPerVert == 3 && parent.type == 1)) 
          {
            vertices[i].bone = new ushort[2];
            vertices[i].weight = new float[2];
            vertices[i].bone[0] = boneMap[inRead.ReadByte()];
            vertices[i].bone[1] = boneMap[inRead.ReadByte()];
            if(vertices[i].bone[1] == boneMap[0]) 
            {
              vertices[i].weight[0] = (float)255 / 256;
              vertices[i].weight[1] = (float)0;
            }
            else 
            {
              vertices[i].weight[0] = (float)128 / 256;
              vertices[i].weight[1] = (float)128 / 256;
            }
          }
          else if(parent.bytesPerVert == 3) 
          {
            if(parent.type == 0 || parent.type == 2) 
            {
              inRead.BaseStream.Seek(2, SeekOrigin.Current);
              vertices[i].bone = new ushort[2];
              vertices[i].weight = new float[2];
              vertices[i].bone[0] = boneMap[inRead.ReadByte()];
              vertices[i].bone[1] = boneMap[inRead.ReadByte()];
              vertices[i].weight[0] = (float)inRead.ReadByte() / 256;
              vertices[i].weight[1] = (float)inRead.ReadByte() / 256;
            }
            else if(parent.type == 4) 
            {
              inRead.BaseStream.Seek(2, SeekOrigin.Current);
              vertices[i].bone = new ushort[4];
              vertices[i].weight = new float[4];
              vertices[i].bone[0] = boneMap[inRead.ReadByte()];
              vertices[i].bone[1] = boneMap[inRead.ReadByte()];
              vertices[i].bone[2] = boneMap[inRead.ReadByte()];
              vertices[i].bone[3] = boneMap[inRead.ReadByte()];
              vertices[i].weight[0] = (float)inRead.ReadByte() / 256;
              vertices[i].weight[1] = (float)inRead.ReadByte() / 256;
              vertices[i].weight[2] = (float)inRead.ReadByte() / 256;
              vertices[i].weight[3] = (float)inRead.ReadByte() / 256;
            }
            else if(parent.type == 3) 
            {
              vertices[i].bone = new ushort[3];
              vertices[i].weight = new float[3];
              vertices[i].bone[0] = boneMap[inRead.ReadByte()];
              vertices[i].bone[1] = boneMap[inRead.ReadByte()];
              vertices[i].bone[2] = boneMap[inRead.ReadByte()];
              vertices[i].weight[0] = (float)inRead.ReadByte() / 256;
              vertices[i].weight[1] = (float)inRead.ReadByte() / 256;
              vertices[i].weight[2] = (float)inRead.ReadByte() / 256;
            }
            else 
            { // Get this type later, set else to seek(6).
              Console.WriteLine(parent.type + " " + parent.bytesPerVert);
            }
          }
          else 
          {
            Console.WriteLine(parent.type + " " + parent.bytesPerVert);
          }
        }
#endregion
#region Read UVs
        inRead.BaseStream.Seek(parent.offset + parent.rsrcHeaderSize + parent.rsrcs[header.indexRSRC + 3].offset + 8, SeekOrigin.Begin); // Using Origin because verts are dword bounded.
        for(int i=0;i<parent.vertices;i++) 
        {
          float fu = (Signed16ToFloat(inRead.ReadInt16(), box.u.min, box.u.max) % 1);
          float fv = (( Signed16ToFloat(inRead.ReadInt16(), box.v.min, box.v.max)) % 1);
          if(fu < 0) fu += 1;
          if(fv < 0) fv += 1;
          vertices[i].u = fu;
          vertices[i].v = fv;
        }
#endregion
#region Read Normals
        inRead.BaseStream.Seek(4, SeekOrigin.Current); // Read over the rsrc.
        for(int i=0;i<parent.vertices;i++) 
        {
          vertices[i].normalY = inRead.ReadInt16();
          vertices[i].normalZ = inRead.ReadInt16();
          inRead.BaseStream.Seek(2, SeekOrigin.Current);
          inRead.BaseStream.Seek(2, SeekOrigin.Current);
          inRead.BaseStream.Seek(2, SeekOrigin.Current);
          vertices[i].normalX = inRead.ReadInt16();
        }
#endregion
      }
#region Signed Values to Float Conversion
      public float Signed16ToFloat(short val, float min, float max) 
      {
        float percent = (((float)val + 32768) / 65535);
        return (percent * (max - min)) + min;
      }
      public float UnSigned16ToFloat(ushort val, float min, float max) 
      {
        float percent = (((float)val) / 65535);
        return (percent * (max - min)) + min;
      }
      public float Signed32ToFloat(int val, float min, float max) 
      {
        float percent = (((float)val + 2147483648) / 4294967295); 
        return (percent * (max - min)) + min;
      }
#endregion
    }
#endregion
#region Meta Structs
    public struct piece
    {
      public string name;
      public ushort vertices;
      public uint bytesPerVert;
      public short type;
      public uint offset;
      public uint size;
      public uint rsrcHeaderSize;
      public uint rsrcCount;
      public uint rsrcOffset;
      public rsrc[] rsrcs;
      public rawModel raw;
      public void Read(ref BinaryReader inRead) 
      {
        bytesPerVert = inRead.ReadUInt32();
        vertices = inRead.ReadUInt16();

        inRead.BaseStream.Seek(14, SeekOrigin.Current);
				
        type = inRead.ReadByte();

        inRead.BaseStream.Seek(35, SeekOrigin.Current);
				
        offset = inRead.ReadUInt32();
        size = inRead.ReadUInt32();
        rsrcHeaderSize = inRead.ReadUInt32();
        inRead.BaseStream.Seek(4, SeekOrigin.Current);

        rsrcCount = inRead.ReadUInt32();
        rsrcOffset = inRead.ReadUInt32();
				
        inRead.BaseStream.Seek(12, SeekOrigin.Current);
      }
    }
    public struct bone
    {
      public ushort name;
      public ushort parent;
      public ushort firstChild;
      public ushort nextSibling;
      public ushort val1;
      public float x;
      public float y;
      public float z;
      public float w;
      public float i;
      public float j;
      public float k;
      public float[] f;
      public float distanceParent;
      public void Read(ref BinaryReader inRead) 
      {
        name = inRead.ReadUInt16();
        inRead.BaseStream.Seek(2, SeekOrigin.Current);
        parent = inRead.ReadUInt16();
        firstChild = inRead.ReadUInt16();
        nextSibling = inRead.ReadUInt16();
        val1 = inRead.ReadUInt16();
        x = inRead.ReadSingle();
        y = inRead.ReadSingle();
        z = inRead.ReadSingle();
        i = inRead.ReadSingle();
        j = inRead.ReadSingle();
        k = inRead.ReadSingle();
        w = inRead.ReadSingle();
        f = new float[13];
        for(int fl=0;fl<13;fl++) f[fl] = inRead.ReadSingle();
        distanceParent = inRead.ReadSingle();
      }
    }
    public struct lod 
    {
      public uint[] lodSubCounts;
      public uint lodSubOffset;
      public string lodName;
      public void Read(ref BinaryReader inRead, string[] scriptStrings) 
      {
        lodName = scriptStrings[inRead.ReadUInt16()];
        inRead.BaseStream.Seek(6, SeekOrigin.Current);
        lodSubCounts = new uint[inRead.ReadUInt32()];
        lodSubOffset = inRead.ReadUInt32();
      }
    }
    public struct modelMeta 
    {
      public boundingBox box;
      public piece[] pieces;
      public bone[] bones;
      public bool raw;
      public string modelName;
      public byte map;
      public void Read(ref BinaryReader inRead, uint magic, string[] scriptStrings) 
      {
        raw = false;
        inRead.BaseStream.Seek(24,SeekOrigin.Current);
        uint boundingBoxOffset = inRead.ReadUInt32() - magic;
        uint lodCount = inRead.ReadUInt32();
        uint lodOffset = inRead.ReadUInt32() - magic;
        //inRead.BaseStream.Seek(8,SeekOrigin.Current);
        uint pieceCount = inRead.ReadUInt32();
        uint pieceOffset = inRead.ReadUInt32() - magic;
        inRead.BaseStream.Seek(28,SeekOrigin.Current);
        uint boneCount = inRead.ReadUInt32();
        uint boneOffset = inRead.ReadUInt32() - magic;
        inRead.BaseStream.Seek(boundingBoxOffset, SeekOrigin.Begin);
        box = new boundingBox();
        box.Read(ref inRead);
				
        pieces = new piece[pieceCount];
				
        lod[] lods = new lod[lodCount];
        inRead.BaseStream.Seek(lodOffset, SeekOrigin.Begin);
        for(int i=0;i<lodCount;i++) 
        {
          lods[i] = new lod();
          lods[i].Read(ref inRead, scriptStrings);
          lods[i].lodSubOffset -= magic;
        }
        for(int i=0;i<lodCount;i++)
        {
          inRead.BaseStream.Seek(lods[i].lodSubOffset, SeekOrigin.Begin);
          int lodSubCounts = lods[i].lodSubCounts.Length;
          for(int n=0;n<lodSubCounts;n++) 
          {
            string lodLevelName = scriptStrings[inRead.ReadUInt16()];
            inRead.BaseStream.Seek(2, SeekOrigin.Current);
            ushort[] lev = new ushort[5];
            for(int z=0;z<5;z++) lev[z] = inRead.ReadUInt16();
            ushort levHigh = inRead.ReadUInt16();
            for(int z=0;z<5;z++) 
            {
              if(lev[z] <= levHigh) pieces[lev[z]].name = lods[i].lodName + "." + lodLevelName + "." + (z + 1);
              if(lev[z] == levHigh) break;
            }
          }
        }
				
        inRead.BaseStream.Seek(pieceOffset, SeekOrigin.Begin);
        for(int i=0;i<pieceCount;i++)
        {
          //pieces[i] = new piece();
          pieces[i].Read(ref inRead);
          pieces[i].rsrcOffset -= magic;
        }
        for(int i=0;i<pieceCount;i++)
        {
          pieces[i].rsrcs = new rsrc[pieces[i].rsrcCount];
          inRead.BaseStream.Seek(pieces[i].rsrcOffset, SeekOrigin.Begin);
          for(int n=0;n<pieces[i].rsrcCount;n++) 
          {
            pieces[i].rsrcs[n].Read(ref inRead);
          }
        }

        inRead.BaseStream.Seek(boneOffset, SeekOrigin.Begin);
        bones = new bone[boneCount];
        for(int i=0;i<boneCount;i++)
        {
          bones[i] = new bone();
          bones[i].Read(ref inRead);
        }
        map = (byte)(pieces[0].offset >> 30);
      }
      public bool ReadRaw(ref BinaryReader inRead) 
      {
        if((pieces[0].offset & 0xC0000000) > 0) return false;
        for(int i=0;i<pieces.Length;i++) 
        {
          inRead.BaseStream.Seek(pieces[i].offset, SeekOrigin.Begin);
          pieces[i].raw = new rawModel();
          pieces[i].raw.Read(ref inRead, pieces[i], box);
        }
        raw = true;
        return raw;
      }
    }
#endregion
    public model()
    {
    }
    public void Read(ref BinaryReader inRead, Functions.Functions.TagInfoStruct tag, uint magic, string[] scriptStrings) 
    {
      meta = new modelMeta();
      meta.modelName = Path.GetFileNameWithoutExtension(tag.Filename);
      inRead.BaseStream.Seek(tag.MetaOffset, SeekOrigin.Begin);
      meta.Read(ref inRead, magic, scriptStrings);
    }
    public bool ReadRaw(ref BinaryReader inRead) 
    {
      if(meta.raw == true) return true;
      return meta.ReadRaw(ref inRead);
    }
    public void WriteObj(string path, bool reverse, string[] scriptStrings) 
    {
      if(meta.raw == false) return;
      if(!(Directory.Exists(path))) Directory.CreateDirectory(path);
      for(int i=0;i<meta.pieces.Length;i++) 
      {
        meta.pieces[i].raw.WriteObj(path + "\\", meta.modelName + "." + meta.pieces[i].name, reverse, meta, scriptStrings);
      }
    }
    public void WriteX(string path) 
    {
      if(meta.raw == false) return;
      if(!(Directory.Exists(path))) Directory.CreateDirectory(path);
      for(int i=0;i<meta.pieces.Length;i++) 
      {
        meta.pieces[i].raw.WriteX(path + "\\", meta.modelName + "." + meta.pieces[i].name, meta);
      }
    }
    public void WriteXWithBones(string path, string[] scriptStrings) 
    {
      if(meta.raw == false) return;
      if(!(Directory.Exists(path))) Directory.CreateDirectory(path);
      for(int i=0;i<meta.pieces.Length;i++) 
      {
        meta.pieces[i].raw.WriteXWithBones(path + "\\", meta.modelName + "." + meta.pieces[i].name, meta, scriptStrings);
      }
    }
  }
}
*/

#endregion