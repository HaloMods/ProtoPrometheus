using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;
using Prometheus.Core.Tags;
using Prometheus.Core.Tags.Bitm;
using Prometheus.Core.Util;
using Prometheus.Core.Tags.Antr;
using Prometheus.Core.Render;


namespace Prometheus.Core.Tags.Mode
{
	public class MODEL_HEADER 
	{
		public uint  Zero1;
		public uint  unk1a;
		public uint  Offset1;
		public uint  Offset2;
		public uint  Offset3;
		public uint  Offset4;
		public uint  Offset5;
		public short SuperHighLodCutoff;
		public short HighLodCutoff;
		public short MediumLodCutoff;
		public short LowLodCutoff;
		public short SuperLowLodCutoff;
		public short SuperHighLodNodeCount;
		public short HighLodNodeCount;
		public short MediumLodNodeCount;
		public short LowLodNodeCount;
		public short SuperLowLodNodeCount;
		public float u_scale;
		public float v_scale;
		public uint[] unk3 = new uint[29];
		public REFLEXIVE Markers = new REFLEXIVE();
		public REFLEXIVE Nodes = new REFLEXIVE();
		public REFLEXIVE Regions = new REFLEXIVE();
		public REFLEXIVE Geometries = new REFLEXIVE();
		public REFLEXIVE Shaders = new REFLEXIVE();
		public void Load (ref BinaryReader br)
		{
			Zero1 = br.ReadUInt32();
			unk1a = br.ReadUInt32();
			Offset1 = br.ReadUInt32();
			Offset2 = br.ReadUInt32();
			Offset3 = br.ReadUInt32(); 
			Offset4 = br.ReadUInt32();
			Offset5 = br.ReadUInt32();
			SuperHighLodCutoff = br.ReadInt16();
			HighLodCutoff = br.ReadInt16();
			MediumLodCutoff = br.ReadInt16();
			LowLodCutoff = br.ReadInt16();
			SuperLowLodCutoff = br.ReadInt16();
			SuperHighLodNodeCount = br.ReadInt16();
			HighLodNodeCount = br.ReadInt16();
			MediumLodNodeCount = br.ReadInt16();
			LowLodNodeCount = br.ReadInt16();
			SuperLowLodNodeCount = br.ReadInt16();
			u_scale = br.ReadSingle();
			v_scale = br.ReadSingle();
			for (int x=0; x<unk3.Length; x++) 
				unk3[x] = br.ReadUInt32();
			Markers.Load(ref br);
			Nodes.Load(ref br);
			Regions.Load(ref br);
			Geometries.Load(ref br);
			Shaders.Load(ref br);
		}
	}

  public class MODEL_NODE
  {
    public string Name;
    public short NextSiblingNode;
    public short NextChildNode;
    public short ParentNode;
    public short unk1;
    public float[] Translation = new float[3];
    public float[] Rotation = new float[4];
    public float DistanceFromParent;
    //public float[] unk2 = new float[21]; //skip over this data, not useful

    public void LoadPc(ref BinaryReader br)
    {
      char[] tmp = new char[32];
      tmp = br.ReadChars(tmp.Length);

      int len=0;
      while((tmp[len++] != '\0')&&(len<32));
      Name = new string(tmp, 0, len-1);

      NextSiblingNode = br.ReadInt16();
      NextChildNode = br.ReadInt16();
      ParentNode = br.ReadInt16();
      unk1 = br.ReadInt16();
      Translation[0] = br.ReadSingle();
      Translation[1] = br.ReadSingle();
      Translation[2] = br.ReadSingle();
      Rotation[0] = br.ReadSingle();
      Rotation[1] = br.ReadSingle();
      Rotation[2] = br.ReadSingle();
      Rotation[3] = br.ReadSingle();
      DistanceFromParent = br.ReadSingle();
      br.BaseStream.Seek(84, SeekOrigin.Current);
    }
    public void Copy(ref MODEL_NODE nc)
    {
      nc.Name = Name;
      nc.NextSiblingNode = NextSiblingNode;
      nc.NextChildNode = NextChildNode;
      nc.ParentNode = ParentNode;
      nc.Translation[0] = Translation[0];
      nc.Translation[1] = Translation[1];
      nc.Translation[2] = Translation[2];
      nc.Rotation[0] = Rotation[0];
      nc.Rotation[1] = Rotation[1];
      nc.Rotation[2] = Rotation[2];
      nc.Rotation[3] = Rotation[3];
      nc.DistanceFromParent = DistanceFromParent;
    }
  }
  public class MODEL_REGION
	{
		public char[] Name = new char[64];
		public REFLEXIVE Permutations = new REFLEXIVE();
		public MODEL_REGION_PERMUTATION[] PERMUTATIONS;
		public void Load(ref BinaryReader br)
		{
 			Name = br.ReadChars(Name.Length);
			Permutations.Load(ref br);
			PERMUTATIONS = new MODEL_REGION_PERMUTATION[Permutations.Count];
		}
	}
	public class MODEL_REGION_PERMUTATION
	{
		public char[] Name = new char[32];
		public uint[] Flags = new uint[8];
		public short[] LOD_MeshIndex = new short[5];
		public short[] Reserved = new short[7];
		public void Load(ref BinaryReader br)
		{
			Name = br.ReadChars(Name.Length);
			for (int x=0; x<Flags.Length; x++)
				Flags[x] = br.ReadUInt32();
			for (int x=0; x<LOD_MeshIndex.Length; x++)
				LOD_MeshIndex[x] = br.ReadInt16();
			for (int x=0; x<Reserved.Length; x++)
				Reserved[x] = br.ReadInt16();

		}
	}

	public class MODEL_GEOMETRY_HEADER
	{
		public uint[] unk = new uint[9];
		public REFLEXIVE SubmeshHeaders = new REFLEXIVE();
		public PCMODEL_SUBMESH_HEADER[] SUBMESHHEADERS;
    public XBOXMODEL_SUBMESH_HEADER[] XSUBMESHHEADERS;
		public void Load(ref BinaryReader br, MapfileVersion ver)
		{
			uint SizeTest = (uint)br.BaseStream.Position;
			for (int x=0; x<unk.Length; x++)
				unk[x] = br.ReadUInt32();
			SubmeshHeaders.Load(ref br);
			
      if(ver == MapfileVersion.XHALO1)
        XSUBMESHHEADERS = new XBOXMODEL_SUBMESH_HEADER[SubmeshHeaders.Count];
      else
        SUBMESHHEADERS = new PCMODEL_SUBMESH_HEADER[SubmeshHeaders.Count];
			SizeTest = (uint)br.BaseStream.Position - SizeTest;
    }
	}

  public class PCMODEL_SUBMESH_HEADER
  {
    public uint Flags;
    public short ShaderIndex;
    public short PrevFilthyPart;
    public short NextFilthyPart;
    public byte CentroidPrimaryNode;
    public byte CentroidSecondaryNode;
    public float CentroidPrimaryWeight;
    public float CentroidSecondaryWeight;
    public float[] Centroid = new float[3];
    public int  TagVertexCount;
    public uint[] unk5a = new uint[3];
    public uint SubObjectCount;
    public uint unk5b;
    public int  TagTriangleCount;
    public uint[] unk5c = new uint[2];
    public uint unkCount;
    public int  TriangleCount;
    public uint IndexOffset;
    public uint IndexOffset2;
    public uint count2;
    public uint VertexCount;
    public uint zero1;
    public uint RawOffsetBehavior;
    public uint VertexRefOffset;
    public uint[] unk = new uint[7];
    public int ShaderManagerIndex = -1;
	  public Single UScale;
	  public Single VScale;
    public EnhancedMesh m_Mesh = null;

    public void LoadRawData(ref BinaryReader br, byte[] buffer)
    {
      m_Mesh = new EnhancedMesh(TriangleCount);
      
      //Trace.WriteLine(string.Format("vertex_position = {0:X}", br.BaseStream.Position + 0x40)); 

      //load verts
      m_Mesh.AllocateVertexBuffer(MeshFormat.Model, (int)VertexCount);
      for(int v=0; v<VertexCount; v++)
      {
        //version arg only cares if it is xbox, other versions use default (uncompressed) behavior
        m_Mesh.LoadH1ModelVertexData(v, ref br, buffer, UScale, VScale, MapfileVersion.HALOPC);
      }
      m_Mesh.UpdateDxBuffer();

      //Trace.WriteLine(string.Format("index_position = {0:X}", br.BaseStream.Position + 0x40)); 

      //load indices
      int index_count = (TriangleCount/3 + 1)*6;
      m_Mesh.AllocateAndLoadIndexBuffer(PrimitiveType.TriangleStrip, TriangleCount+1, ref br, true);
    }
		public void Load(ref BinaryReader br)
		{
      long org_pos = br.BaseStream.Position;
			Flags = br.ReadUInt32();
			ShaderIndex = br.ReadInt16();
			PrevFilthyPart = br.ReadInt16();
			NextFilthyPart = br.ReadInt16();
			CentroidPrimaryNode = br.ReadByte();
			CentroidSecondaryNode = br.ReadByte();
			CentroidPrimaryWeight = br.ReadSingle();
			CentroidSecondaryWeight = br.ReadSingle();
			Centroid[0] = br.ReadSingle();
			Centroid[1] = br.ReadSingle();
			Centroid[2] = br.ReadSingle();
			TagVertexCount = br.ReadInt32();
			for (int x=0; x<3; x++)
				unk5a[x] = br.ReadUInt32();
			SubObjectCount = br.ReadUInt32();
			unk5b = br.ReadUInt32();
			TagTriangleCount = br.ReadInt32();
			unk5c[0] = br.ReadUInt32();
			unk5c[1] = br.ReadUInt32();
			unkCount = br.ReadUInt32();
			TriangleCount = br.ReadInt32();
			IndexOffset = br.ReadUInt32(); ;
			IndexOffset2 = br.ReadUInt32(); ;
			count2 = br.ReadUInt32(); ;
			VertexCount = br.ReadUInt32(); ;
			zero1 = br.ReadUInt32(); ;
			RawOffsetBehavior = br.ReadUInt32(); ;
			VertexRefOffset = br.ReadUInt32(); ;
			for (int x=0; x<7; x++)
				unk[x] = br.ReadUInt32();
		}
    public void Draw()
    {
      m_Mesh.RenderMesh();
    }
	}


  public class XBOXMODEL_SUBMESH_HEADER
  {
    uint  Flags;
    public short ShaderIndex;
    short PrevFilthyPart;
    short NextFilthyPart;
    short CentroidPrimaryNode;
    float CentroidPrimaryWeight;
    float CentroidSecondaryWeight;
    //uint  unk4c;
    //uint[] unk4d = new uint[2];
    //uint[] unk5a = new uint[4];
    uint  SubObjectCount;
    //uint[] unk5b = new uint[5];
    int   IndexCount;
    uint  IndexOffset;
    uint  IndexOffset2;
    uint  count2;
    uint  VertexCount;
    uint  zero1;
    uint  RawOffsetBehavior;
    uint  VertexRefOffset;
    public Single UScale;
    public Single VScale;
    public EnhancedMesh m_Mesh = null;

    public void Load(BinaryReader br)
    {
      uint SizeTest = (uint)br.BaseStream.Position;
      Flags = br.ReadUInt32();
      ShaderIndex = br.ReadInt16();
      PrevFilthyPart = br.ReadInt16();
      NextFilthyPart = br.ReadInt16();
      CentroidPrimaryNode = br.ReadInt16();
      CentroidPrimaryWeight = br.ReadSingle();
      CentroidSecondaryWeight = br.ReadSingle();
      br.BaseStream.Position += 28;
      //uint  unk4c;
      //uint[] unk4d = new uint[2];
      //uint[] unk5a = new uint[4];
      SubObjectCount = br.ReadUInt32();
      br.BaseStream.Position += 20;
      //uint[] unk5b = new uint[5];
      IndexCount = br.ReadInt32();
      IndexOffset = br.ReadUInt32();
      IndexOffset2 = br.ReadUInt32();
      count2 = br.ReadUInt32();
      VertexCount = br.ReadUInt32();
      zero1 = br.ReadUInt32();
      RawOffsetBehavior = br.ReadUInt32();
      VertexRefOffset = br.ReadUInt32();
			SizeTest = (uint)br.BaseStream.Position - SizeTest;
    }
    public void LoadRawData(ref BinaryReader br, byte[] buffer)
    {
      int triangle_count = ((((IndexCount /3) + 1)*6)/2)/3;
      m_Mesh = new EnhancedMesh(IndexCount);

      //load verts
      m_Mesh.AllocateVertexBuffer(MeshFormat.Model, (int)VertexCount);
      for(int v=0; v<VertexCount; v++)
        m_Mesh.LoadH1ModelVertexData(v, ref br, buffer, UScale, VScale, MapfileVersion.XHALO1);
      m_Mesh.UpdateDxBuffer();

      //load indices
      m_Mesh.AllocateAndLoadH1XboxIndexBuffer(PrimitiveType.TriangleStrip, IndexCount, ref br, true);
      //m_Mesh.AllocateAndLoadIndexBuffer(PrimitiveType.TriangleStrip, IndexCount+1, ref br, true);
    }
    public void Draw()
    {
      m_Mesh.RenderMesh();
    }
  }


  public class MODEL_SHADER
  {
    public TAG_REFERENCE Shader = new TAG_REFERENCE();
    public int permutation;
    public int ShaderManagerIndex = -1;
    public void Load(ref BinaryReader br)
    {
      Shader.Load(ref br);
      permutation = br.ReadInt32();
      br.BaseStream.Position += 12;
    }
  }

  //For animation support
  public class MODEL_NODE_TRANSFORM
  {
    public Prometheus.Core.Util.Matrix m_absolute = new Prometheus.Core.Util.Matrix();
    public Prometheus.Core.Util.Matrix m_relative = new Prometheus.Core.Util.Matrix();
    public float[] m_FinalNode = new float[3];
  }


  public class TagModel : TagBase
	{
    private eLOD m_CurrentLOD;
    private int  m_CurrentPermutation = 0;
    private bool m_bAnimationEnabled;
		private MODEL_HEADER m_header;
    private MODEL_NODE[] m_Nodes;
    private MODEL_NODE[] m_AniNodes;
    private MODEL_REGION[] m_regions;
		private MODEL_GEOMETRY_HEADER[] m_geometries;
    private MODEL_NODE_TRANSFORM[] m_NodeTransforms;
    private MODEL_SHADER[] m_shaders;
    //private Texture[] m_textures;
    public bool m_bModelLoaded;
    private Util.Matrix m_mx = new Util.Matrix();
    public BOUNDING_BOX m_BoundingBox = new BOUNDING_BOX();

		public TagModel() 
		{
			m_header = new MODEL_HEADER();
      m_bModelLoaded = false;
      m_bAnimationEnabled = true;
      m_CurrentLOD = eLOD.SUPER_HIGH;
		}
    public void LoadTagData()
    {
      m_bModelLoaded = false;
      BinaryReader br = new BinaryReader(m_stream);
      // Load the header
      m_header.Load(ref br);

      // Load the nodes and allocate the node transforms
      br.BaseStream.Seek(m_header.Nodes.Offset, SeekOrigin.Begin);
      m_Nodes = new MODEL_NODE[m_header.Nodes.Count];
      for(int x=0; x<m_Nodes.Length; x++)
      {
        m_Nodes[x] = new MODEL_NODE();
        m_Nodes[x].LoadPc(ref br);
      }

      // Load the regions
      br.BaseStream.Seek(m_header.Regions.Offset, SeekOrigin.Begin);
      m_regions = new MODEL_REGION[m_header.Regions.Count];
      for (int x=0; x<m_regions.Length; x++)
      {
        m_regions[x] = new MODEL_REGION();
        m_regions[x].Load(ref br);
      }
      // Load child permutations
      for (int x=0; x<m_regions.Length; x++)
      {
        br.BaseStream.Seek(m_regions[x].Permutations.Offset, SeekOrigin.Begin);
        for (int y=0; y<m_regions[x].Permutations.Count; y++)
        {
          m_regions[x].PERMUTATIONS[y] = new MODEL_REGION_PERMUTATION();
          m_regions[x].PERMUTATIONS[y].Load(ref br);
        }
      }

      // Load the Geometries
      br.BaseStream.Seek(m_header.Geometries.Offset, SeekOrigin.Begin);
      m_geometries = new MODEL_GEOMETRY_HEADER[m_header.Geometries.Count];
      for (int x=0; x<m_geometries.Length; x++)
      {
        m_geometries[x] = new MODEL_GEOMETRY_HEADER();
        m_geometries[x].Load(ref br, this.Header.GameVersion);
      }

      // Load child Submesh Headers
      for (int g=0; g<m_geometries.Length; g++)
      {
        for (int s=0; s<m_geometries[g].SubmeshHeaders.Count; s++)
        {
          if(this.Header.GameVersion == MapfileVersion.XHALO1)
          {
            m_geometries[g].XSUBMESHHEADERS[s] = new XBOXMODEL_SUBMESH_HEADER();
            m_geometries[g].XSUBMESHHEADERS[s].UScale = m_header.u_scale;
            m_geometries[g].XSUBMESHHEADERS[s].VScale = m_header.v_scale;
            m_geometries[g].XSUBMESHHEADERS[s].Load(br);
          }
          else
          {
            m_geometries[g].SUBMESHHEADERS[s] = new PCMODEL_SUBMESH_HEADER();
            m_geometries[g].SUBMESHHEADERS[s].UScale = m_header.u_scale;
            m_geometries[g].SUBMESHHEADERS[s].VScale = m_header.v_scale;
            m_geometries[g].SUBMESHHEADERS[s].Load(ref br);
          }
        }

        for (int s=0; s<m_geometries[g].SubmeshHeaders.Count; s++)
        {
          if(this.Header.GameVersion == MapfileVersion.XHALO1)
            m_geometries[g].XSUBMESHHEADERS[s].LoadRawData(ref br, this.m_Buffer);
          else
            m_geometries[g].SUBMESHHEADERS[s].LoadRawData(ref br, this.m_Buffer);
        }
      }

      // Load the shaders
      br.BaseStream.Seek(m_header.Shaders.Offset, SeekOrigin.Begin);
      m_shaders = new MODEL_SHADER[m_header.Shaders.Count];
      for (int x=0; x<m_shaders.Length; x++)
      {
        m_shaders[x] = new MODEL_SHADER();
        m_shaders[x].Load(ref br);
      }
      // Now load all of the strings
      for (int x=0; x<m_shaders.Length; x++)
      {
        m_shaders[x].Shader.ReadString(ref br);
      }

      for(int x=0; x<m_shaders.Length; x++)
      {
        string tagclass = new string(m_shaders[x].Shader.tag);
        m_shaders[x].ShaderManagerIndex = MdxRender.SM.RegisterShader(new TagFileName(m_shaders[x].Shader.data, tagclass, this.m_PromHeader.GameVersion));
      }

      //Sync up submesh ShaderManagerIndex with global shader manager
      for (int g=0; g<m_geometries.Length; g++)
      {
        for (int s=0; s<m_geometries[g].SubmeshHeaders.Count; s++)
        {
          if(this.Header.GameVersion == MapfileVersion.XHALO1)
          {
            string tag_type = new string(m_shaders[m_geometries[g].XSUBMESHHEADERS[s].ShaderIndex].Shader.tag);
            TagFileName shader_filename = new TagFileName(m_shaders[m_geometries[g].XSUBMESHHEADERS[s].ShaderIndex].Shader.data, 
              tag_type, this.m_PromHeader.GameVersion);
            m_geometries[g].XSUBMESHHEADERS[s].m_Mesh.RegisterShader(shader_filename);
          }
          else
          {
            string tag_type = new string(m_shaders[m_geometries[g].SUBMESHHEADERS[s].ShaderIndex].Shader.tag);
            TagFileName shader_filename = new TagFileName(m_shaders[m_geometries[g].SUBMESHHEADERS[s].ShaderIndex].Shader.data, 
              tag_type, this.m_PromHeader.GameVersion);
            m_geometries[g].SUBMESHHEADERS[s].m_Mesh.RegisterShader(shader_filename);
          }
        }
      }

      //determine bounding box for camera viewer
      if(this.Header.GameVersion == MapfileVersion.XHALO1)
      {
        for(int g=0; g<m_header.Geometries.Count; g++)
          for(int s=0; s<m_geometries[g].SubmeshHeaders.Count; s++)
            m_geometries[g].XSUBMESHHEADERS[s].m_Mesh.UpdateBoundingBox(ref m_BoundingBox);
      }
      else
      {
        for(int g=0; g<m_header.Geometries.Count; g++)
          for(int s=0; s<m_geometries[g].SubmeshHeaders.Count; s++)
            m_geometries[g].SUBMESHHEADERS[s].m_Mesh.UpdateBoundingBox(ref m_BoundingBox);
      }

      m_bModelLoaded = true;
    }
    public Model3D GetModel3D(string name)
    {
      Model3D model = new Model3D(name);

      //load geometry (mesh) data
      model.m_MeshList = new EnhancedMesh[m_header.Geometries.Count][];
      for(int g=0; g<m_header.Geometries.Count; g++)
      {
        model.m_MeshList[g] = new EnhancedMesh[m_geometries[g].SubmeshHeaders.Count];

        for(int s=0; s<m_geometries[g].SubmeshHeaders.Count; s++)
        {
          if(this.Header.GameVersion == MapfileVersion.XHALO1)
            model.m_MeshList[g][s] = m_geometries[g].XSUBMESHHEADERS[s].m_Mesh;
          else
            model.m_MeshList[g][s] = m_geometries[g].SUBMESHHEADERS[s].m_Mesh;
        }
      }

      //load regions/permutations
      int len;
      model.m_Regions = new Region[m_header.Regions.Count];
      for(int r=0; r<m_header.Regions.Count; r++)
      {
        model.m_Regions[r] = new Region();

        //copy region name
        len=0;
        while((this.m_regions[r].Name[len++] != '\0')&&(len<64));
        model.m_Regions[r].Name = new string(this.m_regions[r].Name, 0, len-1);

        //copy region permutations
        model.m_Regions[r].Permutations = new Permutation[this.m_regions[r].Permutations.Count];

        for(int p=0; p<this.m_regions[r].Permutations.Count; p++)
        {
          model.m_Regions[r].Permutations[p] = new Permutation();
          
          len=0;
          while((this.m_regions[r].PERMUTATIONS[p].Name[len++] != '\0')&&(len<32));
          model.m_Regions[r].Permutations[p].Name = new string(this.m_regions[r].PERMUTATIONS[p].Name, 0, len-1);
          
          for(int l=0; l<5; l++)
            model.m_Regions[r].Permutations[p].LodMeshIndex[l] = this.m_regions[r].PERMUTATIONS[p].LOD_MeshIndex[l];

          //halo1 models only have 5 lods, so set the 6th one to match [4], "super high"
          model.m_Regions[r].Permutations[p].LodMeshIndex[5] = this.m_regions[r].PERMUTATIONS[p].LOD_MeshIndex[4];
        }
      }

      model.m_Nodes = new ModelNode[m_Nodes.Length];
      for(int n=0; n<m_Nodes.Length; n++)
      {
        model.m_Nodes[n] = new ModelNode();
        model.m_Nodes[n].NextSiblingNode = this.m_Nodes[n].NextSiblingNode;
        model.m_Nodes[n].NextChildNode = this.m_Nodes[n].NextChildNode;
        model.m_Nodes[n].ParentNode = this.m_Nodes[n].ParentNode;
        
        model.m_Nodes[n].Translation[0] = this.m_Nodes[n].Translation[0];
        model.m_Nodes[n].Translation[1] = this.m_Nodes[n].Translation[1];
        model.m_Nodes[n].Translation[2] = this.m_Nodes[n].Translation[2];
        model.m_Nodes[n].Rotation[0] = this.m_Nodes[n].Rotation[0];
        model.m_Nodes[n].Rotation[1] = this.m_Nodes[n].Rotation[1];
        model.m_Nodes[n].Rotation[2] = this.m_Nodes[n].Rotation[2];
        model.m_Nodes[n].Rotation[3] = this.m_Nodes[n].Rotation[3];
      }

      model.m_BoundingBox = new BOUNDING_BOX();
      model.m_BoundingBox.min[0] = this.m_BoundingBox.min[0];
      model.m_BoundingBox.min[1] = this.m_BoundingBox.min[1];
      model.m_BoundingBox.min[2] = this.m_BoundingBox.min[2];
      model.m_BoundingBox.max[0] = this.m_BoundingBox.max[0];
      model.m_BoundingBox.max[1] = this.m_BoundingBox.max[1];
      model.m_BoundingBox.max[2] = this.m_BoundingBox.max[2];

      return(model);
    }
    private void ProcessNodeOrientation(int node_index)
    {
      Prometheus.Core.Util.Quaternion quat = new Prometheus.Core.Util.Quaternion();
      int n = node_index;

      do
      {
        //set the local transformation for this node
        quat.Load(m_AniNodes[n].Rotation);
        
        m_NodeTransforms[n].m_relative.setRotationQuaternion(quat.m_quat);
        m_NodeTransforms[n].m_relative.setInverseTranslation(m_AniNodes[n].Translation);

        //multiply by parent transform
        if(m_AniNodes[n].ParentNode != -1)
        {
          m_NodeTransforms[n].m_absolute.set(m_NodeTransforms[m_AniNodes[n].ParentNode].m_absolute.m_matrix);
          m_NodeTransforms[n].m_absolute.postMultiply(ref m_NodeTransforms[n].m_relative);
        }
        else
        {
          //this is a root node, do not need to multiply against parent
          m_NodeTransforms[n].m_absolute.set(m_NodeTransforms[n].m_relative.m_matrix);
        }

        //if this has a child, recurse down the hierarchy
        if(m_AniNodes[n].NextChildNode != -1)
          ProcessNodeOrientation(m_AniNodes[n].NextChildNode);

        //move to the next sibling at this recursion level
        n = m_AniNodes[n].NextSiblingNode;
      }while(n != -1);
    }

    private void InverseTransformModel()
    {
      /*
      for(int n=0; n<m_header.Nodes.Count; n++ )
      {
        //Update node locations for our "debug" viewer
        m_NodeTransforms[n].m_FinalNode[0] = 0;
        m_NodeTransforms[n].m_FinalNode[1] = 0;
        m_NodeTransforms[n].m_FinalNode[2] = 0;

        Prometheus.Core.Util.Matrix matrix = new Prometheus.Core.Util.Matrix();
        matrix.set(m_NodeTransforms[n].m_absolute.m_matrix);
        matrix.inverseRotateVect(ref m_NodeTransforms[n].m_FinalNode[0],
          ref m_NodeTransforms[n].m_FinalNode[1],
          ref m_NodeTransforms[n].m_FinalNode[2]);
        matrix.inverseTranslateVect(m_NodeTransforms[n].m_FinalNode);
      }

      //Go through all the vertices and inverse transform them
      //During render, they will be transformed back
      for(int g=0; g<m_header.Geometries.Count; g++)
      {
        for(int s=0; s<m_geometries[g].SubmeshHeaders.Count; s++)
        {
          for(int v=0; v<m_geometries[g].SUBMESHHEADERS[s].TagVertexCount; v++)
          {
            if(m_geometries[g].SUBMESHHEADERS[s].VERTS[v].node1_index != -1)
            {
              m_mx = m_NodeTransforms[m_geometries[g].SUBMESHHEADERS[s].VERTS[v].node1_index].m_absolute;
              
              //inverse transform positions
              m_mx.TranslateVect(ref m_geometries[g].SUBMESHHEADERS[s].VERTS[v].it_pos[0],
                ref m_geometries[g].SUBMESHHEADERS[s].VERTS[v].it_pos[1],
                ref m_geometries[g].SUBMESHHEADERS[s].VERTS[v].it_pos[2]);
              
              m_mx.inverseRotateVect(ref m_geometries[g].SUBMESHHEADERS[s].VERTS[v].it_pos[0],
                ref m_geometries[g].SUBMESHHEADERS[s].VERTS[v].it_pos[1],
                ref m_geometries[g].SUBMESHHEADERS[s].VERTS[v].it_pos[2]);

              //inverse transform normals
              m_mx.TranslateVect(ref m_geometries[g].SUBMESHHEADERS[s].VERTS[v].it_norm[0],
                ref m_geometries[g].SUBMESHHEADERS[s].VERTS[v].it_norm[1],
                ref m_geometries[g].SUBMESHHEADERS[s].VERTS[v].it_norm[2]);
              
              m_mx.inverseRotateVect(ref m_geometries[g].SUBMESHHEADERS[s].VERTS[v].it_norm[0],
                ref m_geometries[g].SUBMESHHEADERS[s].VERTS[v].it_norm[1],
                ref m_geometries[g].SUBMESHHEADERS[s].VERTS[v].it_norm[2]);
            }
          }
        }
      }
          */
    }
    public void SetLOD(eLOD lod)
    {
      if(lod == eLOD.CAUSES_FIRES)
        lod = eLOD.SUPER_HIGH;

      m_CurrentLOD = lod;
    }
    public void DrawModel()
    {
      int geo_index = 0;
      if(this.m_bModelLoaded)
      {
        //Trace.Write("world transform: ---\n"+device.Transform.World.ToString()+"\n");
        for(int region=0; region<m_header.Regions.Count; region++)
        {
          geo_index = m_regions[region].PERMUTATIONS[m_CurrentPermutation].LOD_MeshIndex[(int)m_CurrentLOD];

          for(int s=0; s<m_geometries[geo_index].SubmeshHeaders.Count; s++)
          {
            m_geometries[geo_index].SUBMESHHEADERS[s].Draw();
          }
        }
      }
    }
    public void InitializeAnimationProcessing()
    {
      m_AniNodes = new MODEL_NODE[m_header.Nodes.Count];
      m_NodeTransforms = new MODEL_NODE_TRANSFORM[m_header.Nodes.Count];
      for(int x=0; x<m_header.Nodes.Count; x++)
      {
        m_AniNodes[x] = new MODEL_NODE();
        m_Nodes[x].Copy(ref m_AniNodes[x]);
        m_NodeTransforms[x] = new MODEL_NODE_TRANSFORM();
      }

      this.ProcessNodeOrientation(0);
      //Process the node and vertex transformations to initialize everything
      this.InverseTransformModel();

      m_bAnimationEnabled = true;
    }
    public void RevertToStaticModel()
    {
      if(m_bAnimationEnabled == true)
      {
        m_AniNodes = new MODEL_NODE[m_header.Nodes.Count];
        m_NodeTransforms = new MODEL_NODE_TRANSFORM[m_header.Nodes.Count];
        for(int x=0; x<m_header.Nodes.Count; x++)
        {
          m_AniNodes[x].Copy(ref m_Nodes[x]);
        }

        this.ProcessNodeOrientation(0);
        m_bAnimationEnabled = false;
      }
    }
    public void UpdateKeyframe(ref Animations Ani)
    {
      /*
      if(m_bModelLoaded)
      {
        //copy new nodes into local node structure
        for(int n=0; n<m_header.Nodes.Count; n++)
        {
          Ani.GetAnimationNode(n, ref m_AniNodes[n].Translation, ref m_AniNodes[n].Rotation);
        }

        ProcessNodeOrientation(0);

        //TODO:  update local mesh buffers (DX buffers are updated during draw)
        for(int n=0; n<m_header.Nodes.Count; n++ )
        {
          //Update node locations for our "debug" viewer
          m_NodeTransforms[n].m_FinalNode[0] = 0;
          m_NodeTransforms[n].m_FinalNode[1] = 0;
          m_NodeTransforms[n].m_FinalNode[2] = 0;

          m_mx = m_NodeTransforms[n].m_absolute;

          m_mx.inverseRotateVect(ref m_NodeTransforms[n].m_FinalNode[0],
            ref m_NodeTransforms[n].m_FinalNode[1],
            ref m_NodeTransforms[n].m_FinalNode[2]);
          
          m_mx.inverseTranslateVect(m_NodeTransforms[n].m_FinalNode);
        }

        short node_index;
        int geo_index;
        Vector vec = new Vector();

        //Perform transform operations on active permutation/LOD to save CPU
        for(int region=0; region<m_header.Regions.Count; region++)
        {
          geo_index = m_regions[region].PERMUTATIONS[m_CurrentPermutation].LOD_MeshIndex[(int)m_CurrentLOD];

          for(int s=0; s<m_geometries[geo_index].SubmeshHeaders.Count; s++)
          {
            for(int v=0; v<m_geometries[geo_index].SUBMESHHEADERS[s].TagVertexCount; v++)
            {
              node_index = m_geometries[geo_index].SUBMESHHEADERS[s].VERTS[v].node1_index;
              
              //only transform verts that are linked to a node
              if(node_index != -1)
              {
                m_mx = m_NodeTransforms[node_index].m_absolute;

                //position transform
                vec.load(m_geometries[geo_index].SUBMESHHEADERS[s].VERTS[v].it_pos[0],
                  m_geometries[geo_index].SUBMESHHEADERS[s].VERTS[v].it_pos[1],
                  m_geometries[geo_index].SUBMESHHEADERS[s].VERTS[v].it_pos[2]);
                vec.transform(ref m_mx.m_matrix);

                m_geometries[geo_index].SUBMESHHEADERS[s].VERTS[v].dxv.X = vec.m_vector[0];
                m_geometries[geo_index].SUBMESHHEADERS[s].VERTS[v].dxv.Y = vec.m_vector[1];
                m_geometries[geo_index].SUBMESHHEADERS[s].VERTS[v].dxv.Z = vec.m_vector[2];

                //normal transform
                vec.load(m_geometries[geo_index].SUBMESHHEADERS[s].VERTS[v].it_norm[0],
                  m_geometries[geo_index].SUBMESHHEADERS[s].VERTS[v].it_norm[1],
                  m_geometries[geo_index].SUBMESHHEADERS[s].VERTS[v].it_norm[2]);
                vec.transform(ref m_mx.m_matrix);

                m_geometries[geo_index].SUBMESHHEADERS[s].VERTS[v].dxv.Nx = vec.m_vector[0];
                m_geometries[geo_index].SUBMESHHEADERS[s].VERTS[v].dxv.Ny = vec.m_vector[1];
                m_geometries[geo_index].SUBMESHHEADERS[s].VERTS[v].dxv.Nz = vec.m_vector[2];
              }
            }
            m_geometries[geo_index].SUBMESHHEADERS[s].UpdateDxBuffers();
          }
        }
      }
      */
    }
    public void ProcessDeviceReset()
    {
    }
    public void ProcessDeviceLost()
    {
    }
	}
}
