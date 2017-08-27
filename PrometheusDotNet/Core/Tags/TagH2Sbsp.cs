using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;
using Prometheus.Core.Tags;
using Prometheus.Core.Render;
using H2BitmTag = Prometheus.Core.Tags.H2Bitm;

/*
using Prometheus.Core.Tags.Bitm;
using Prometheus.Core.Util;
using Prometheus.Core.Tags.Antr;
using Prometheus.Core.Render;
*/

namespace Prometheus.Core.Tags.Sbsp
{
  public class H2_BSP_HEADER 
  {
    public H2REFLEXIVE Geometry = new H2REFLEXIVE();
    public H2REFLEXIVE Shaders = new H2REFLEXIVE();
    public H2REFLEXIVE Scenery = new H2REFLEXIVE();
    public H2REFLEXIVE SceneryPos = new H2REFLEXIVE();

    public void Load(ref BinaryReader br)
    {
      byte[] tmp = new byte[0x23C];
      tmp = br.ReadBytes(tmp.Length);
      
      Geometry.Count = BitConverter.ToUInt32(tmp,0x9C);
      Geometry.Offset = BitConverter.ToUInt32(tmp,0xA0);
      Shaders.Count = BitConverter.ToUInt32(tmp,0xA4);
      Shaders.Offset = BitConverter.ToUInt32(tmp,0xA8);
      Scenery.Count = BitConverter.ToUInt32(tmp,0x138);
      Scenery.Offset = BitConverter.ToUInt32(tmp,0x13C);
      SceneryPos.Count = BitConverter.ToUInt32(tmp,0x140);
      SceneryPos.Offset = BitConverter.ToUInt32(tmp,0x144);
      tmp = null;
    }
  }


  public struct H2_BSP_VERTS
  {
    public Vector3 position;
    public float u1;
    public float v1;
    public float u2;
    public float v2;
    public static readonly VertexFormats Format = VertexFormats.Position | VertexFormats.Texture2;

    public H2_BSP_VERTS(float x, float y, float z, float iu1, float iv1, float iu2, float iv2)
    {
      position = new Vector3(x,y,z);
      u1 = iu1;
      v1 = iv1;
      u2 = iu2;
      v2 = iv2;
    }
  }


  public class H2_BSP_LIGHTMAPS
  {
    public short VertexCount;
    public short IndexListCount;

    public uint ModelResourceBlockOffset;
    public uint ModelResourceBlockSize;
    public uint ModelHeaderSize;
    public uint ModelRawSize;

    public H2REFLEXIVE Resources = new H2REFLEXIVE();
    public uint id;

    public H2_RAW_DATA_DESCRIPTOR[] b_RawDescriptors;
    public H2_BSP_BLKH ResourceHeader = new H2_BSP_BLKH();
    public H2_BSP_RSRC_INFO[] ResourceInfo = null;

    public short[] INDICES = null;
    H2_BSP_VERTS[] VERTS;

    public IndexBuffer IndexData = null;
    public VertexBuffer VertData = null;

    uint RelativeOffset;

    public void Load(ref BinaryReader br)
    {
      VertexCount = br.ReadInt16();
      IndexListCount = br.ReadInt16();

      br.BaseStream.Position += 36;

      ModelResourceBlockOffset = br.ReadUInt32();
      ModelResourceBlockSize = br.ReadUInt32();
      ModelHeaderSize = br.ReadUInt32();
      ModelRawSize = br.ReadUInt32();

      Resources.Load(ref br);
      id = br.ReadUInt32();
      br.BaseStream.Position += 108;
    }
    public void LoadRawDescriptors(ref BinaryReader br)
    {
      br.BaseStream.Position = Resources.Offset; 
      b_RawDescriptors = new H2_RAW_DATA_DESCRIPTOR[Resources.Count];
      for(int i=0; i<Resources.Count; i++)
      {
        b_RawDescriptors[i] = new H2_RAW_DATA_DESCRIPTOR();
        b_RawDescriptors[i].Load(ref br);
      }
    }
    public void LoadRawData(ref BinaryReader br)
    {
      RelativeOffset = (uint)br.BaseStream.Position + ModelHeaderSize + 8;
      ModelResourceBlockOffset = (uint)br.BaseStream.Position;
      if (Resources.Count > 0)
      {

        ResourceHeader.Load(ref br);

        //Load Resource Info
        br.BaseStream.Position = RelativeOffset + b_RawDescriptors[0].offset;
        ResourceInfo = new H2_BSP_RSRC_INFO[ResourceHeader.InfoChunkCount];
        for(int i=0; i<ResourceHeader.InfoChunkCount; i++)
        {
          ResourceInfo[i] = new H2_BSP_RSRC_INFO();
          ResourceInfo[i].Load(ref br);
        }


        //Indice Data
        br.BaseStream.Position = RelativeOffset + b_RawDescriptors[ResourceHeader.indexRSRC].offset;  

        INDICES = new short[ResourceHeader.IndexChunkCount];
        for(int i=0; i<ResourceHeader.IndexChunkCount ; i+=3)
        {
          INDICES[i + 0] = br.ReadInt16();
          INDICES[i + 2] = br.ReadInt16();
          INDICES[i + 1] = br.ReadInt16();
        }


        //Vertex Data
        br.BaseStream.Position = RelativeOffset + b_RawDescriptors[ResourceHeader.vertexRSRC].offset;  
      
        VERTS = new H2_BSP_VERTS[VertexCount];

        for(int v=0; v<VertexCount; v++)
        {
          VERTS[v] = new H2_BSP_VERTS();
          VERTS[v].position.X = br.ReadSingle();
          VERTS[v].position.Y = br.ReadSingle();
          VERTS[v].position.Z = br.ReadSingle();
          //VERTS[v].dxv.Nx = 0;
          //VERTS[v].dxv.Ny = 0;
          //VERTS[v].dxv.Nz = 1;

        }

        //Texture Coord Data
        br.BaseStream.Position = RelativeOffset + b_RawDescriptors[ResourceHeader.vertexRSRC+1].offset; 
      
        for(int v=0; v<VertexCount; v++)
        {
          VERTS[v].u1 = br.ReadSingle();
          VERTS[v].v1 = br.ReadSingle();
        }

        //Lightmap Coord Data
        if (Resources.Count > 9)
        {
          br.BaseStream.Position = RelativeOffset + b_RawDescriptors[ResourceHeader.vertexRSRC+3].offset;  
        }
        else
        {
          br.BaseStream.Position = RelativeOffset + b_RawDescriptors[ResourceHeader.vertexRSRC+2].offset;  
        }
      
        for(int v=0; v<VertexCount; v++)
        {
					short u2 = br.ReadInt16();
					short v2 = br.ReadInt16();
					VERTS[v].u2 = DecompressShortToFloat(u2);
					VERTS[v].v2 = DecompressShortToFloat(v2);
					//VERTS[v].u2 = (float)br.ReadInt32();
					//VERTS[v].u2 = (float)br.ReadInt32();
				}

        //Create DirectX resources
        IndexData = new IndexBuffer(typeof(short), (int)ResourceHeader.IndexChunkCount , MdxRender.Dev, Usage.WriteOnly, Pool.Default);
        IndexData.Created += new EventHandler(this.OnIndexBufferCreate);
        OnIndexBufferCreate(IndexData, null);

        VertData = new VertexBuffer(typeof(H2_BSP_VERTS), VertexCount, MdxRender.Dev, 
          Usage.Dynamic | Usage.WriteOnly, H2_BSP_VERTS.Format, Pool.Default);
        VertData.Created += new EventHandler(this.OnVertexBufferCreate);
        OnVertexBufferCreate(VertData, null);
      }

      //Goto End of Block
      br.BaseStream.Position = ModelResourceBlockOffset + ModelResourceBlockSize;

    }
    public float DecompressShortToFloat(short comp_vector)
    {
      int temp;
      temp = comp_vector + comp_vector;
      float decomp_val;
      decomp_val = (((float)temp) + 1)/ (float)65535.0;
      return(decomp_val);
    }
    public float Signed16ToFloat(short val, float min, float max) 
    {
      float percent = (((float)val + 32768) / 65535); 
      return (percent * (max - min)) + min;
    }
    public float Unsigned16ToFloat(ushort val, float min, float max) 
    {
      return ((val / 65535) * (max - min)) + min;
    }

    public void Draw(int LightMapIndex,int ColorMapIndex,int OffsetIndex1,int OffsetIndex2)
    {
      if (Resources.Count > 0)
      {
        try
        {
          TagShad H2BspShader = null;
          MdxRender.Dev.Indices = IndexData;
          MdxRender.Dev.SetStreamSource(0, VertData, 0);
          //MdxRender.Dev.RenderState.FillMode = FillMode.WireFrame  ;
          MdxRender.Dev.VertexFormat = H2_BSP_VERTS.Format;

          for( int y=0; y<ResourceHeader.InfoChunkCount; y++)
          {
            //Set the Shader
            //ResourceInfo[y].ShaderIndex 
            H2BspShader = MdxRender.SM.GetH2BspShader(ResourceInfo[y].ShaderIndex);
            //MdxRender.SM.ConfigureLightmapForBlend2(LightMapIndex,ColorMapIndex,OffsetIndex1,OffsetIndex2);
            MdxRender.SM.ConfigureLightmapForBlend(LightMapIndex,OffsetIndex1);

            H2BspShader.ActivateShaderWithLightmap(ref MdxRender.SM.m_TextureManager);
            
            MdxRender.Dev.DrawIndexedPrimitives(PrimitiveType.TriangleList, 
              0,
              0, 
              VertexCount, 
              ResourceInfo[y].IndexStart,
              ResourceInfo[y].IndexCount/3);

	         }
        }
        catch(Exception e)
        {
        }
      }
    }
    private void OnIndexBufferCreate(object sender, EventArgs e)
    {
      IndexBuffer buffer = (IndexBuffer)sender;
      buffer.SetData(INDICES, 0, LockFlags.None);
    }
    private void OnTextureCreate(object sender, EventArgs e)
    {
      IndexBuffer buffer = (IndexBuffer)sender;
      buffer.SetData(INDICES, 0, LockFlags.None);
    }
    private void OnVertexBufferCreate(object sender, EventArgs e)
    {
      VertexBuffer vb = (VertexBuffer)sender;
      H2_BSP_VERTS[] data = (H2_BSP_VERTS[])vb.Lock(0, 0);

      for(int v=0; v<VertexCount; v++)
      {
        data[v] = VERTS[v];
      }
      vb.Unlock();
    }
  }

  public class H2_BSP_SCENERY
  {
    public short VertexCount;
    public short IndexListCount;

    public uint ModelResourceBlockOffset;
    public uint ModelResourceBlockSize;
    public uint ModelHeaderSize;
    public uint ModelRawSize;

    public Vector3 OffsetVector;
    public H2REFLEXIVE Resources = new H2REFLEXIVE();
    public H2REFLEXIVE Scenery_loc = new H2REFLEXIVE();

    public uint id;

    public H2_RAW_DATA_DESCRIPTOR[] b_RawDescriptors;
    public H2_BSP_BLKH ResourceHeader = new H2_BSP_BLKH();
    public H2_BSP_RSRC_INFO[] ResourceInfo = null;

    public short[] INDICES = null;
    H2_BSP_VERTS[] VERTS;

    public IndexBuffer IndexData = null;
    public VertexBuffer VertData = null;

    uint RelativeOffset;

    public void Load(ref BinaryReader br)
    {
      byte[] TmpBuff = br.ReadBytes(0xC8);
      VertexCount = BitConverter.ToInt16(TmpBuff,0);//br.ReadInt16();
      IndexListCount = BitConverter.ToInt16(TmpBuff,2);//br.ReadInt16();
      ModelResourceBlockOffset = BitConverter.ToUInt32(TmpBuff,0x28);//br.ReadUInt32();
      ModelResourceBlockSize = BitConverter.ToUInt32(TmpBuff,0x2C);//br.ReadUInt32();
      ModelHeaderSize = BitConverter.ToUInt32(TmpBuff,0x30);//br.ReadUInt32();
      ModelRawSize = BitConverter.ToUInt32(TmpBuff,0x34);//br.ReadUInt32();
      Resources.Count = BitConverter.ToUInt32(TmpBuff,0x38);
      Resources.Offset = BitConverter.ToUInt32(TmpBuff,0x3C);
      id = BitConverter.ToUInt32(TmpBuff,0x40);//br.ReadUInt32();
      Scenery_loc.Count = BitConverter.ToUInt32(TmpBuff,0x90);
      Scenery_loc.Offset = BitConverter.ToUInt32(TmpBuff,0x94);
      TmpBuff = null;
    }
    public void LoadRawDescriptors(ref BinaryReader br)
    {
      br.BaseStream.Position = Resources.Offset; 
      b_RawDescriptors = new H2_RAW_DATA_DESCRIPTOR[Resources.Count];
      for(int i=0; i<Resources.Count; i++)
      {
        b_RawDescriptors[i] = new H2_RAW_DATA_DESCRIPTOR();
        b_RawDescriptors[i].Load(ref br);
      }
    }
    public void LoadRawData(ref BinaryReader br)
    {
      RelativeOffset = (uint)br.BaseStream.Position + ModelHeaderSize + 8;
      ModelResourceBlockOffset = (uint)br.BaseStream.Position;
      if (Resources.Count > 0)
      {

        ResourceHeader.Load(ref br);

        //Load Resource Info
        br.BaseStream.Position = RelativeOffset + b_RawDescriptors[0].offset;
        ResourceInfo = new H2_BSP_RSRC_INFO[ResourceHeader.InfoChunkCount];
        for(int i=0; i<ResourceHeader.InfoChunkCount; i++)
        {
          ResourceInfo[i] = new H2_BSP_RSRC_INFO();
          ResourceInfo[i].Load(ref br);
        }


        //Indice Data
        br.BaseStream.Position = RelativeOffset + b_RawDescriptors[ResourceHeader.indexRSRC].offset;  

        INDICES = new short[ResourceHeader.IndexChunkCount];
        for(int i=0; i<ResourceHeader.IndexChunkCount ; i+=3)
        {
          INDICES[i + 0] = br.ReadInt16();
          INDICES[i + 2] = br.ReadInt16();
          INDICES[i + 1] = br.ReadInt16();
        }


        //Vertex Data
        br.BaseStream.Position = RelativeOffset + b_RawDescriptors[ResourceHeader.vertexRSRC].offset;  
      
        VERTS = new H2_BSP_VERTS[VertexCount];

        for(int v=0; v<VertexCount; v++)
        {
          VERTS[v] = new H2_BSP_VERTS();
          VERTS[v].position.X = br.ReadSingle(); //+ OffsetVector.X;
          VERTS[v].position.Y = br.ReadSingle(); //+ OffsetVector.Y;
          VERTS[v].position.Z = br.ReadSingle(); //+ OffsetVector.Z;
          //VERTS[v].dxv.Nx = 0;
          //VERTS[v].dxv.Ny = 0;
          //VERTS[v].dxv.Nz = 1;

        }

        //Texture Coord Data
        br.BaseStream.Position = RelativeOffset + b_RawDescriptors[ResourceHeader.vertexRSRC+1].offset; 
      
        for(int v=0; v<VertexCount; v++)
        {
          VERTS[v].u1 = br.ReadSingle();
          VERTS[v].v1 = br.ReadSingle();
        }

        //Lightmap Coord Data
        if (Resources.Count > 9)
        {
          br.BaseStream.Position = RelativeOffset + b_RawDescriptors[ResourceHeader.vertexRSRC+3].offset;  
        }
        else
        {
          br.BaseStream.Position = RelativeOffset + b_RawDescriptors[ResourceHeader.vertexRSRC+2].offset;  
        }
      
        for(int v=0; v<VertexCount; v++)
        {
          short u2 = br.ReadInt16();
          short v2 = br.ReadInt16();
          VERTS[v].u2 = DecompressShortToFloat(u2);
          VERTS[v].v2 = DecompressShortToFloat(v2);
        
        }

        //Create DirectX resources
        IndexData = new IndexBuffer(typeof(short), (int)ResourceHeader.IndexChunkCount , MdxRender.Dev, Usage.WriteOnly, Pool.Default);
        IndexData.Created += new EventHandler(this.OnIndexBufferCreate);
        OnIndexBufferCreate(IndexData, null);

        VertData = new VertexBuffer(typeof(H2_BSP_VERTS), VertexCount, MdxRender.Dev, 
          Usage.Dynamic | Usage.WriteOnly, H2_BSP_VERTS.Format, Pool.Default);
        VertData.Created += new EventHandler(this.OnVertexBufferCreate);
        OnVertexBufferCreate(VertData, null);
      }

      //Goto End of Block
      br.BaseStream.Position = ModelResourceBlockOffset + ModelResourceBlockSize;

    }
    public float DecompressShortToFloat(short comp_vector)
    {
      int temp;
      temp = comp_vector + comp_vector;
      float decomp_val;
      decomp_val = (((float)temp) + 1)/ (float)65535.0;
      return(decomp_val);
    }
    public float Signed16ToFloat(short val, float min, float max) 
    {
      float percent = (((float)val + 32768) / 65535); 
      return (percent * (max - min)) + min;
    }
    public float Unsigned16ToFloat(ushort val, float min, float max) 
    {
      return ((val / 65535) * (max - min)) + min;
    }

    public void Draw(int LightMapIndex,int OffsetIndex)
    {
      if (Resources.Count > 0)
      {
        try
        {
          TagShad H2BspShader = null;
          MdxRender.Dev.Indices = IndexData;
          MdxRender.Dev.SetStreamSource(0, VertData, 0);
          //MdxRender.Dev.RenderState.FillMode = FillMode.WireFrame  ;
          MdxRender.Dev.VertexFormat = H2_BSP_VERTS.Format;
          for( int y=0; y<ResourceHeader.InfoChunkCount; y++)
          {
            //Set the Shader
            //ResourceInfo[y].ShaderIndex 
            if (OffsetIndex == 0xffff)
            {
              MdxRender.SM.ActivateShader(ResourceInfo[y].ShaderIndex, ShaderPass.First);
            }
            else
            {
              H2BspShader = MdxRender.SM.GetH2BspShader(ResourceInfo[y].ShaderIndex);
              MdxRender.SM.ConfigureLightmapForBlend(LightMapIndex,OffsetIndex);
              H2BspShader.ActivateShaderWithLightmap(ref MdxRender.SM.m_TextureManager);
            }

            MdxRender.Dev.DrawIndexedPrimitives(PrimitiveType.TriangleList, 
              0,
              0, 
              VertexCount, 
              ResourceInfo[y].IndexStart,
              ResourceInfo[y].IndexCount/3);
          }
        }
        catch(Exception e)
        {
        }
      }
    }
    private void OnIndexBufferCreate(object sender, EventArgs e)
    {
      IndexBuffer buffer = (IndexBuffer)sender;
      buffer.SetData(INDICES, 0, LockFlags.None);
    }
    private void OnTextureCreate(object sender, EventArgs e)
    {
      IndexBuffer buffer = (IndexBuffer)sender;
      buffer.SetData(INDICES, 0, LockFlags.None);
    }
    private void OnVertexBufferCreate(object sender, EventArgs e)
    {
      VertexBuffer vb = (VertexBuffer)sender;
      H2_BSP_VERTS[] data = (H2_BSP_VERTS[])vb.Lock(0, 0);

      for(int v=0; v<VertexCount; v++)
      {
        data[v] = VERTS[v];
      }
      vb.Unlock();
    }
  }

  public class H2_BSP_SCENERY_POS
  {
    public Matrix SceneryTransform = new Matrix();
    public short CompX;
    public short CompY;
    public short CompZ;
    public byte SceneryIndex;
    public void Load(ref BinaryReader br)
    {
      byte[] tmpBuff = br.ReadBytes(0x58);
      SceneryTransform.M11 = BitConverter.ToSingle(tmpBuff,0x4);
      SceneryTransform.M12 = BitConverter.ToSingle(tmpBuff,0x8);
      SceneryTransform.M13 = BitConverter.ToSingle(tmpBuff,0xC);
      SceneryTransform.M14 = 0;

      SceneryTransform.M21 = BitConverter.ToSingle(tmpBuff,0x10);
      SceneryTransform.M22 = BitConverter.ToSingle(tmpBuff,0x14);
      SceneryTransform.M23 = BitConverter.ToSingle(tmpBuff,0x18);
      SceneryTransform.M24 = 0;

      SceneryTransform.M31 = BitConverter.ToSingle(tmpBuff,0x1C);
      SceneryTransform.M32 = BitConverter.ToSingle(tmpBuff,0x20);
      SceneryTransform.M33 = BitConverter.ToSingle(tmpBuff,0x24);
      SceneryTransform.M34 = 0;

      SceneryTransform.M41 = BitConverter.ToSingle(tmpBuff,0x28);
      SceneryTransform.M42 = BitConverter.ToSingle(tmpBuff,0x2C);
      SceneryTransform.M43 = BitConverter.ToSingle(tmpBuff,0x30);
      SceneryTransform.M44 = 1;

      CompX = BitConverter.ToInt16(tmpBuff,0x0 + 0x38);
      CompY = BitConverter.ToInt16(tmpBuff,0x2 + 0x38);
      CompZ = BitConverter.ToInt16(tmpBuff,0x4 + 0x38);
      SceneryIndex = tmpBuff[0x34];
      tmpBuff = null;
    }
    public float DecompressShortToFloat(short comp_vector)
    {
      int temp;
      temp = comp_vector + comp_vector;
      float decomp_val;
      decomp_val = (((float)temp) + 1)/ (float)65535.0;
      return(decomp_val);
    }
  }
  public class H2_RAW_DATA_DESCRIPTOR
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


  public class H2_BSP_BLKH
  {
    public uint DataSize;
    public uint InfoChunkCount;
    public uint IndexChunkCount;
    public uint Unk1ChunkCount;
    public uint Unk2ChunkCount;
    public uint Unk3ChunkCount;
    public uint Unk4ChunkCount;
    public uint indexRSRC;
    public uint vertexRSRC;

    public void Load(ref BinaryReader br)
    {
      //br.BaseStream.Position += 4;
      uint test = br.ReadUInt32();
      DataSize = br.ReadUInt32();
      InfoChunkCount = br.ReadUInt32();
      br.BaseStream.Position += 4;
      Unk1ChunkCount = br.ReadUInt32();
      br.BaseStream.Position += 20;
      IndexChunkCount = br.ReadUInt32();
      br.BaseStream.Position += 4;
      Unk2ChunkCount = br.ReadUInt32();
      br.BaseStream.Position += 4;
      Unk3ChunkCount = br.ReadUInt32();
      br.BaseStream.Position += 4;
      Unk4ChunkCount = br.ReadUInt32();
      br.BaseStream.Position += 8;
      indexRSRC = 0;
      vertexRSRC = 0;
      if(InfoChunkCount > 0)
      {
        indexRSRC++;
        vertexRSRC++;
      }
      if(Unk1ChunkCount > 0)
      {
        indexRSRC++;
        vertexRSRC++;
      }
      if(IndexChunkCount > 0) vertexRSRC++;
      
      if(Unk2ChunkCount > 0) vertexRSRC++;
      if(Unk3ChunkCount > 0) vertexRSRC++;
      if(Unk4ChunkCount > 0) vertexRSRC++;
    }
  }


  public class H2_BSP_RSRC_INFO
  {
    public ushort ShaderIndex;
    public ushort IndexStart;
    public ushort IndexCount;
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

  public class H2_BSP_SHADER
  {
    public int ShaderManagerIndex = -1;
    public H2TAG_REFERENCE TagRef = new H2TAG_REFERENCE();
  }
  
  public class Halo2BSP : TagBase
  {
    H2_BSP_HEADER b_Header = new H2_BSP_HEADER();
    H2_BSP_LIGHTMAPS[] b_Lightmaps;
    H2_BSP_SHADER[] m_Shaders;
    BSP_LIGHTMAP[] m_Lightmaps = null;
    H2_BSP_SCENERY[] b_Scenery;
    H2_BSP_SCENERY_POS[] b_SceneryPos;
    ushort[] SbspLightMapIndex1; 
    ushort[] ScenLightMapIndex1;
    ushort[] SbspLightMapIndex2; 
    ushort[] ScenLightMapIndex2;

    public int m_LightmapIndex = -1;
		public int m_ColorMapIndex = -1;
		public int m_ScMapIndex = -1;
    public BOUNDING_BOX m_BoundingBox = new BOUNDING_BOX();
    public bool b_BSPLoaded = false;
    //public void LoadTagData( shader_mgr)

    public void LoadTagData(string FileName)
    {
      int i;
      BinaryReader br = new BinaryReader(m_stream);
      // Load the Header
      string[] SFileName = FileName.Split(new char[]{'.'},256);
      string DirSep = @"\";
      string[] NFileName = SFileName[0].Split(DirSep.ToCharArray(),256);
			string NewDir = "";
			for (int dn = 1; dn < NFileName.Length - 1;dn++)
			{
				NewDir += @"\" + NFileName[dn];
			}
			NewDir += @"\" + NFileName[NFileName.Length - 2];
			string LtmpTag = NewDir + "_" + NFileName[NFileName.Length - 1] + "_lightmap.ltmp";
		//string LtmpTag = SFileName[0] + "_" + NFileName[NFileName.Length - 1] + "_lightmap.ltmp";
			string LightMapTag = NewDir + "_" + NFileName[NFileName.Length - 1] + "_lightmap_bitmaps.bitmap";
    //string LightMapTag = SFileName[0] + "_" + NFileName[NFileName.Length - 1] + "_lightmap_bitmaps.bitmap";
      
			TagFileName tfnLTMP = new TagFileName(LtmpTag,MapfileVersion.XHALO2);
			TagFileName tfnLightMap = new TagFileName(LightMapTag,MapfileVersion.XHALO2);
			
			H2BitmTag.H2Bitmap bmp = new H2BitmTag.H2Bitmap();
			bmp.LoadTagBuffer(tfnLightMap);
			bmp.LoadTagData();

      if(tfnLTMP.Exists)
      {
				TagBase tbLTMP = new TagBase();
				tbLTMP.LoadTagBuffer(tfnLTMP);

        //fsin = fiin.Open(FileMode.Open,FileAccess.Read);
        //fsin.Seek(0x40,System.IO.SeekOrigin.Begin);
        byte[] ltmpheader = new byte[0x104];
        tbLTMP.Stream.Read(ltmpheader,0,ltmpheader.Length);
        uint ltmpsub1Offset = BitConverter.ToUInt32(ltmpheader,0x84);
        tbLTMP.Stream.Seek((long)ltmpsub1Offset,System.IO.SeekOrigin.Begin);
        byte[] ltmpsub1 = new byte[0xA4];
        tbLTMP.Stream.Read(ltmpsub1,0,ltmpsub1.Length);
				uint ColorMapCount = BitConverter.ToUInt32(ltmpsub1,0x08);
				uint ColorMapOffset = BitConverter.ToUInt32(ltmpsub1,0x0C);
				tbLTMP.Stream.Seek((long)ColorMapOffset,System.IO.SeekOrigin.Begin);
				uint ColorMapSize = ColorMapCount * 0x400;
				byte[] ColorMaps = new byte[ColorMapSize];
				tbLTMP.Stream.Read(ColorMaps,0,ColorMaps.Length);
        uint bspLightMapCount = BitConverter.ToUInt32(ltmpsub1,0x28);
        uint bspLightMapOffset = BitConverter.ToUInt32(ltmpsub1,0x2C);
        uint ScenLightMapCount = BitConverter.ToUInt32(ltmpsub1,0x48);
        uint ScenLightMapOffset = BitConverter.ToUInt32(ltmpsub1,0x4C);
        SbspLightMapIndex1 = new ushort[bspLightMapCount]; 
        ScenLightMapIndex1 = new ushort[ScenLightMapCount];
        SbspLightMapIndex2 = new ushort[bspLightMapCount]; 
        ScenLightMapIndex2 = new ushort[ScenLightMapCount];

        byte[] tmpin = new byte[0x04];
        tbLTMP.Stream.Seek(bspLightMapOffset,System.IO.SeekOrigin.Begin);
        for (int r = 0;r<bspLightMapCount;r++)
        {
          tbLTMP.Stream.Read(tmpin,0,tmpin.Length);
          SbspLightMapIndex1[r] = BitConverter.ToUInt16(tmpin,0);
          SbspLightMapIndex2[r] = BitConverter.ToUInt16(tmpin,2);
        }
        tbLTMP.Stream.Seek(ScenLightMapOffset,System.IO.SeekOrigin.Begin);
        for (int r = 0;r<ScenLightMapCount;r++)
        {
          tbLTMP.Stream.Read(tmpin,0,tmpin.Length);
          ScenLightMapIndex1[r] = BitConverter.ToUInt16(tmpin,0);
          ScenLightMapIndex2[r] = BitConverter.ToUInt16(tmpin,2);
        }
				m_ColorMapIndex = MdxRender.SM.m_TextureManager.RegisterTexture3("ColorMap",ref ColorMaps,ref SbspLightMapIndex1,ref SbspLightMapIndex2,ref bmp);
				m_ScMapIndex = MdxRender.SM.m_TextureManager.RegisterTexture3("SColorMap",ref ColorMaps,ref ScenLightMapIndex1,ref ScenLightMapIndex2,ref bmp);
				tbLTMP = null;
				tfnLTMP = null;

      }

      b_Header.Load(ref br);
      //string LtmpName = br.BaseStream.
      m_LightmapIndex = MdxRender.SM.LoadLightmaps2(LightMapTag);
      br.BaseStream.Position = b_Header.Geometry.Offset; 
      //Load the Lightmap Sections
      b_Lightmaps = new H2_BSP_LIGHTMAPS[b_Header.Geometry.Count];
      for(i=0; i<b_Header.Geometry.Count; i++)
      {
        b_Lightmaps[i] = new H2_BSP_LIGHTMAPS();
        b_Lightmaps[i].Load(ref br);
      }
      //Save File pos
      long RawDataOffset = br.BaseStream.Position;
      //Move File pointer ahead for somthing
      for(i=0; i<b_Header.Geometry.Count; i++)
        br.BaseStream.Position += b_Lightmaps[i].ModelResourceBlockSize;
      
      for(i=0; i<b_Header.Geometry.Count; i++)
        b_Lightmaps[i].LoadRawDescriptors(ref br);
      
      br.BaseStream.Position = RawDataOffset;

      for(i=0; i<b_Header.Geometry.Count; i++)
        b_Lightmaps[i].LoadRawData(ref br);
      
      this.b_BSPLoaded = true;
      
      //br.BaseStream.Position = b_Header.Shaders.Offset;
			br.BaseStream.Seek(b_Header.Shaders.Offset,SeekOrigin.Begin);
      m_Shaders = new H2_BSP_SHADER[b_Header.Shaders.Count];
      for(i=0; i<b_Header.Shaders.Count; i++)
      {
        br.BaseStream.Position += 8;
        m_Shaders[i] = new H2_BSP_SHADER();
        m_Shaders[i].TagRef.Load(ref br);
        br.BaseStream.Position += 16;
      }

      for(i=0; i<b_Header.Shaders.Count; i++)
      {
        m_Shaders[i].TagRef.ReadString(ref br);
        m_Shaders[i].ShaderManagerIndex = MdxRender.SM.RegisterShader(new TagFileName(m_Shaders[i].TagRef.data, "dahs", m_PromHeader.GameVersion)); 
      }

      for(i=0; i<b_Header.Geometry.Count; i++)
      {
        for(int j=0; j<b_Lightmaps[i].ResourceHeader.InfoChunkCount; j++)
        {
          b_Lightmaps[i].ResourceInfo[j].ShaderIndex = (ushort)
            m_Shaders[b_Lightmaps[i].ResourceInfo[j].ShaderIndex].ShaderManagerIndex;
        }
      }
      

      br.BaseStream.Position = b_Header.Scenery.Offset;
      b_Scenery = new H2_BSP_SCENERY[b_Header.Scenery.Count];
      for(i=0; i<b_Header.Scenery.Count; i++)
      {
        if (i == 21)
        {
          int BreakPoint = 0;
        }
        b_Scenery[i] = new H2_BSP_SCENERY();
        b_Scenery[i].Load(ref br);
      }
      RawDataOffset = br.BaseStream.Position;
      //Move File pointer
      for(i=0; i<b_Header.Geometry.Count; i++)
        br.BaseStream.Position += b_Lightmaps[i].ModelResourceBlockSize;

      for(i=0; i<b_Header.Scenery.Count; i++)
      {
        b_Scenery[i].LoadRawDescriptors(ref br);
      }
      br.BaseStream.Position = RawDataOffset;

      for(i=0; i<b_Header.Scenery.Count; i++)
        b_Scenery[i].LoadRawData(ref br);

      for(i=0; i<b_Header.Scenery.Count; i++)
      {
        for(int j=0; j<b_Scenery[i].ResourceHeader.InfoChunkCount; j++)
        {
          b_Scenery[i].ResourceInfo[j].ShaderIndex = (ushort)
            m_Shaders[b_Scenery[i].ResourceInfo[j].ShaderIndex].ShaderManagerIndex;
        }
      }
      b_SceneryPos = new H2_BSP_SCENERY_POS[b_Header.SceneryPos.Count];
      br.BaseStream.Position = b_Header.SceneryPos.Offset;
      for (int h = 0; h<b_Header.SceneryPos.Count;h++)
      {
        b_SceneryPos[h] = new H2_BSP_SCENERY_POS();
        b_SceneryPos[h].Load(ref br);
      }
    }

    public void DrawModel()
    {
      if(this.b_BSPLoaded)
      {
				//MdxRender.Dev.Indices = this.IndexData;
				MdxRender.Dev.VertexFormat = PositionTexture2.Format;

        int s;
        for(s=0; s<b_Header.Geometry.Count; s++)
        {
          b_Lightmaps[s].Draw(m_ColorMapIndex,m_ColorMapIndex,SbspLightMapIndex1[s],s);
					//b_Lightmaps[s].Draw(m_LightmapIndex,m_ColorMapIndex,0,0);

        }
        //Don't Draw scenery until we find the placement cords
        for(int g=0; g<b_Header.SceneryPos.Count; g++)
        {
          MdxRender.Dev.Transform.World = b_SceneryPos[g].SceneryTransform;
          b_Scenery[b_SceneryPos[g].SceneryIndex].Draw(m_ScMapIndex,g);
          MdxRender.Dev.Transform.World = Matrix.Identity;
        }
      }
    }
  }
}

