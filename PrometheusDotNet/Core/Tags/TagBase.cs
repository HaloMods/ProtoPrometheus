using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using Prometheus;
using Prometheus.Core;
using Prometheus.Core.Project;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;
using Prometheus.Core.Render;
using System.Drawing;

namespace Prometheus.Core.Tags
{
  public class REFLEXIVE
  {
    public uint Count;
    public uint Offset;
    public uint Unknown;

    public REFLEXIVE()
    {
    }

    public void Load(ref BinaryReader br)
    {
      Count = br.ReadUInt32();
      Offset = br.ReadUInt32();
      Unknown = br.ReadUInt32();
    }
  }

  public class H2REFLEXIVE
  {
    public uint Count;
    public uint Offset;

    public H2REFLEXIVE()
    {
    }

    public void Load(ref BinaryReader br)
    {
      Count = br.ReadUInt32();
      Offset = br.ReadUInt32();
    }
  }
  public class H2TAG_REFERENCE
  {
    public char[] tag = new char[4];
    public int StringLength;
    public string data;
    public void ReadString(ref BinaryReader br)
    {
      if (StringLength != -1)
      {
        data = new string(br.ReadChars(StringLength));
        br.BaseStream.Position++;
      }
    }
    public void Load(ref BinaryReader br)
    {
      tag = br.ReadChars(4);
      StringLength = br.ReadInt32();
    }
  }
  public class TAG_REFERENCE
  {
    public char[] tag = new char[4];
    public uint NamePtr;
    public int StringLength;
    public uint unknown;
    public string data;
    public void ReadString(ref BinaryReader br)
    {
      if(StringLength > 0)
      {
        data = new string(br.ReadChars(StringLength));
        br.BaseStream.Position++;
      }
    }
    public void Load(ref BinaryReader br)
    {
      tag = br.ReadChars(4);

      NamePtr = br.ReadUInt32();
      StringLength = br.ReadInt32();
      unknown = br.ReadUInt32();
    }
  }

  public class TAG_SOUND_REFS
  {
    char[] tag = new char[4];
    uint[] unknown = new uint[4];

    public void Load(ref BinaryReader br)
    {
      tag = br.ReadChars(4);

      unknown[0] = br.ReadUInt32();
      unknown[1] = br.ReadUInt32();
      unknown[2] = br.ReadUInt32();
      unknown[3] = br.ReadUInt32();
    }
  }

  public class BOUNDING_BOX
  {
    public float[] min = new float[3];
    public float[] max = new float[3];
    private float[] CentroidSum = new float[3];
    private int CentroidCount = 0;
    static private Material bbColor = new Material();
    static private CustomVertex.PositionColored[] boxOutline = new CustomVertex.PositionColored[18];

    public BOUNDING_BOX()
    {
      min[0] = 40000f;
      min[0] = 40000f;
      min[0] = 40000f;
      max[0] = -40000f;
      max[0] = -40000f;
      max[0] = -40000f;
      CentroidSum[0] = 0f;
      CentroidSum[1] = 0f;
      CentroidSum[2] = 0f;
    }

    public void Load(ref BinaryReader br)
    {
      min[0] = br.ReadSingle();
      min[1] = br.ReadSingle();
      min[2] = br.ReadSingle();
			
      max[0] = br.ReadSingle();
      max[1] = br.ReadSingle();
      max[2] = br.ReadSingle();
    }
    public void Update(float x, float y, float z)
    {
      if(x < min[0])
        min[0] = x;
      if(y < min[1])
        min[1] = y;
      if(z < min[2])
        min[2] = z;

      if(x > max[0])
        max[0] = x;
      if(y > max[1])
        max[1] = y;
      if(z > max[2])
        max[2] = z;

      CentroidSum[0] += x;
      CentroidSum[1] += y;
      CentroidSum[2] += z;
      CentroidCount++;
    }
    public void Update(BOUNDING_BOX bb)
    {
      Update(bb.min[0], bb.min[1], bb.min[2]);
      Update(bb.max[0], bb.max[1], bb.max[2]);
    }
    public void GetCentroid(out float x, out float y, out float z)
    {
      if(CentroidCount == 0)
      {
        x=0;
        y=0;
        z=0;
      }
      else
      {
        x = CentroidSum[0]/(float)CentroidCount;
        y = CentroidSum[1]/(float)CentroidCount;
        z = CentroidSum[2]/(float)CentroidCount;
      }
    }
    public void GetCenter(out float x, out float y, out float z)
    {
      x = (max[0] + min[0])/2.0f;
      y = (max[1] + min[1])/2.0f;
      z = (max[2] + min[2])/2.0f;
    }
    public float Radius
    {
      get
      {
        float dist1, dist2;

        dist1 = (float)Math.Sqrt(min[0]*min[0] + min[1]*min[1] + min[2]*min[2]);
        dist2 = (float)Math.Sqrt(max[0]*max[0] + max[1]*max[1] + max[2]*max[2]);

        if(dist1 < dist2)
          dist1 = dist2;

        return dist1;
      }
    }
    public void RenderBoundingBox()
    {
      Vector3 temp_min = new Vector3(min[0], min[1], min[2]);
      Vector3 temp_max = new Vector3(max[0], max[1], max[2]);

      for(int i=0; i<boxOutline.Length; i++)
        boxOutline[i].Color = Color.DarkGray.ToArgb();

      boxOutline[0].X = temp_min.X;
      boxOutline[0].Y = temp_min.Y;
      boxOutline[0].Z = temp_min.Z;

      boxOutline[1].X = temp_min.X;
      boxOutline[1].Y = temp_max.Y;
      boxOutline[1].Z = temp_min.Z;

      boxOutline[2].X = temp_max.X;
      boxOutline[2].Y = temp_max.Y;
      boxOutline[2].Z = temp_min.Z;

      boxOutline[3].X = temp_max.X;
      boxOutline[3].Y = temp_min.Y;
      boxOutline[3].Z = temp_min.Z;

      boxOutline[4].X = temp_min.X;
      boxOutline[4].Y = temp_min.Y;
      boxOutline[4].Z = temp_min.Z;

      boxOutline[5].X = temp_min.X;
      boxOutline[5].Y = temp_min.Y;
      boxOutline[5].Z = temp_max.Z;

      boxOutline[6].X = temp_min.X;
      boxOutline[6].Y = temp_max.Y;
      boxOutline[6].Z = temp_max.Z;

      boxOutline[7].X = temp_min.X;
      boxOutline[7].Y = temp_max.Y;
      boxOutline[7].Z = temp_min.Z;

      boxOutline[8].X = temp_min.X;
      boxOutline[8].Y = temp_max.Y;
      boxOutline[8].Z = temp_max.Z;

      boxOutline[9].X = temp_max.X;
      boxOutline[9].Y = temp_max.Y;
      boxOutline[9].Z = temp_max.Z;

      boxOutline[10].X = temp_max.X;
      boxOutline[10].Y = temp_max.Y;
      boxOutline[10].Z = temp_min.Z;

      boxOutline[11].X = temp_max.X;
      boxOutline[11].Y = temp_max.Y;
      boxOutline[11].Z = temp_max.Z;

      boxOutline[12].X = temp_max.X;
      boxOutline[12].Y = temp_min.Y;
      boxOutline[12].Z = temp_max.Z;

      boxOutline[13].X = temp_max.X;
      boxOutline[13].Y = temp_min.Y;
      boxOutline[13].Z = temp_min.Z;

      boxOutline[14].X = temp_max.X;
      boxOutline[14].Y = temp_min.Y;
      boxOutline[14].Z = temp_max.Z;

      boxOutline[15].X = temp_min.X;
      boxOutline[15].Y = temp_min.Y;
      boxOutline[15].Z = temp_max.Z;

      boxOutline[16].X = temp_min.X;
      boxOutline[16].Y = temp_min.Y;
      boxOutline[16].Z = temp_min.Z;

      boxOutline[17].X = temp_min.X;
      boxOutline[17].Y = temp_min.Y;
      boxOutline[17].Z = temp_max.Z;

      bbColor.Diffuse = Color.White;
      bbColor.Ambient = Color.White;
      MdxRender.Dev.Material = bbColor;
      MdxRender.Dev.RenderState.Lighting = false;
      MdxRender.SM.ConfigureForDiffuseColor();
      MdxRender.Dev.VertexFormat = CustomVertex.PositionColored.Format;
      MdxRender.Dev.DrawUserPrimitives(PrimitiveType.LineStrip, 16, boxOutline);
      MdxRender.Dev.RenderState.Lighting = true;
    }
  }

  public class BOUNDING_BOX2
  {
    public float[,] coordinates = new float[3,2];
       
    public void Load(ref BinaryReader br)
    {
      coordinates[0,0] = br.ReadSingle();
      coordinates[0,1] = br.ReadSingle();
      coordinates[1,0] = br.ReadSingle();
      coordinates[1,1] = br.ReadSingle();
      coordinates[2,0] = br.ReadSingle();
      coordinates[2,1] = br.ReadSingle();
    }
  }

  public class COLOR
  {
    public float R;
    public float G;
    public float B;

    public void Load(ref BinaryReader br)
    {
      R = br.ReadSingle(); 
      G = br.ReadSingle(); 
      B = br.ReadSingle(); 
    }
  }

  public enum RawDataType {None = 0, HaloPcBitmap = 1, HaloPcSound = 2, HaloCeBitmap = 3, HaloCeSound = 4, XHalo2Shared = 5, 
                           XHalo2SinglePlayerShared = 6, XHalo2MainMenu = 7};

  // The main file header.
  public class TagHeader
  {
    static public int PROM_HEADER_SIZE = 0x40;
    protected char[] m_Signature = new char [4] { 'P', 'R', 'O', 'M' };
    protected uint[] m_TagClass = null;
    protected int m_TagRevision = 3;
    public MapfileVersion GameVersion;
    protected int m_HeaderSize = PROM_HEADER_SIZE;
    public int TagSize = 0;
    public int AttachmentsSize = 12;
    public RawDataType RawType = RawDataType.None;

    public TagHeader()
    {
      m_TagClass = new uint[3];
    }
    
    public uint TagClass0
    {
      get
      {
        return m_TagClass[0];
      }
      set
      {
        m_TagClass[0] = value;
      }
    }
    public uint TagClass1
    {
      get
      {
        return m_TagClass[1];
      }
      set
      {
        m_TagClass[1] = value;
      }
    }
    public uint TagClass2
    {
      get
      {
        return m_TagClass[2];
      }
      set
      {
        m_TagClass[2] = value;
      }
    }
    public string Signature
    {
      get{return(new string(m_Signature));}
    }

    public void Read(byte[] buffer) 
    {
      for(int i=0; i<4; i++)m_Signature[i] = BitConverter.ToChar(buffer, i);
      m_HeaderSize = BitConverter.ToInt32(buffer, 4);

			m_TagClass[0] = BitConverter.ToUInt32(buffer,8);
			m_TagClass[1] = BitConverter.ToUInt32(buffer,12);
			m_TagClass[2] = BitConverter.ToUInt32(buffer,16);

      m_TagRevision = BitConverter.ToInt32(buffer, 20);
      GameVersion = (MapfileVersion)BitConverter.ToInt32(buffer, 24);
      TagSize = BitConverter.ToInt32(buffer, 28);
      AttachmentsSize = BitConverter.ToInt32(buffer, 32);
      RawType = (RawDataType)BitConverter.ToInt32(buffer, 36);
    }

    public void Read(ref BinaryReader br) 
    {
      int StartPos = (int)br.BaseStream.Position;
      
      m_Signature = br.ReadChars(4);
      m_HeaderSize = br.ReadInt32();
      m_TagClass[0] = br.ReadUInt32();
      m_TagClass[1] = br.ReadUInt32();
      m_TagClass[2] = br.ReadUInt32();
      m_TagRevision = br.ReadInt32();
      GameVersion = (MapfileVersion)br.ReadInt32();
      TagSize = br.ReadInt32();
      AttachmentsSize = br.ReadInt32();
      RawType = (RawDataType)br.ReadInt32();

      switch(m_TagRevision) //schema
      {
        case 1:
          break;
      }

      int delta = m_HeaderSize - ((int)br.BaseStream.Position - StartPos);
      br.BaseStream.Position += delta;
    }
    public void Write(ref BinaryWriter bw) 
    {
      bw.Seek(0, SeekOrigin.Begin);
      int StartPos = (int)bw.BaseStream.Position;
      bw.Write(m_Signature);
      bw.Write(m_HeaderSize);
      bw.Write(m_TagClass[0]);
      bw.Write(m_TagClass[1]);
      bw.Write(m_TagClass[2]);
      bw.Write(m_TagRevision);
      bw.Write((int)GameVersion);
      bw.Write(TagSize);
      bw.Write(AttachmentsSize);
      bw.Write((int)RawType);

      int delta = m_HeaderSize - ((int)bw.BaseStream.Position - StartPos);
      bw.BaseStream.Position += delta;
    }
    public void Write(ref MemoryStream ms) 
    {
      BinaryWriter bw = new BinaryWriter(ms);
      bw.Seek(0, SeekOrigin.Begin);
      int StartPos = (int)bw.BaseStream.Position;
      bw.Write(m_Signature);
      bw.Write(m_HeaderSize);
      bw.Write(m_TagClass[0]);
      bw.Write(m_TagClass[1]);
      bw.Write(m_TagClass[2]);
      bw.Write(m_TagRevision);
      bw.Write((int)GameVersion);
      bw.Write(TagSize);
      bw.Write(AttachmentsSize);
      bw.Write((int)RawType);
      int delta = m_HeaderSize - ((int)bw.BaseStream.Position - StartPos);
      byte zero = 0;
      for(int i=0; i<delta; i++)bw.Write(zero);
      bw = null;
    }
    public void SeekToAttachStart(ref BinaryReader br)
    {
      br.BaseStream.Seek(m_HeaderSize + TagSize, SeekOrigin.Begin);
    }
    public void SeekToAttachStart(ref MemoryStream ms)
    {
      ms.Seek(m_HeaderSize + TagSize, SeekOrigin.Begin);
    }
    static public void SeekToTagDataStart(ref MemoryStream ms)
    {
      ms.Seek(PROM_HEADER_SIZE, SeekOrigin.Begin);
    }
  }

  // A variable-format attachment.
  public class AttachmentHeader
  {
    protected char[] Signature = new char [8] { 'A', 't', 't', 'a', 'c', 'h', '\0', '\0' };
    public int AttachmentCount = 0;
    public void Read(ref BinaryReader br)
    {
      Signature = br.ReadChars(8);
      AttachmentCount = br.ReadInt32();
    }
    public void Write(ref BinaryWriter bw)
    {
      bw.Write(Signature);
      bw.Write(AttachmentCount);
    }
    public void Write(ref MemoryStream ms)
    {
      BinaryWriter bw = new BinaryWriter(ms);
      bw.Write(Signature);
      bw.Write(AttachmentCount);
      bw.Flush();
      bw = null;
    }
  }
  
  // Binary descriptor for an attachment element. 
  public class AttachmentElement
  {
    public uint Offset;
    public int Size;
    public void Read(ref BinaryReader br) 
    {
      Offset = br.ReadUInt32();
      Size = br.ReadInt32();
    }
    public void Write(ref BinaryWriter bw) 
    {
      bw.Write(Offset);
      bw.Write(Size);
    }
  }


  class TagClassTable
  {
    public string TagClass;
    public string FilenameExt;
  }
  public class TagFileName
  {
    static private TagClassTable[] halo1Table = new TagClassTable[78];
    private string m_RelativePath;
    private MapfileVersion m_Version;
    private TagSource m_TagSource;
    private string m_TagClass;
    public string PathNoExtension
    {
      get
      {
        int end = m_RelativePath.LastIndexOf('.');

        if(end == -1)
          return m_RelativePath;
        else
          return m_RelativePath.Substring(0, end);
      }
    }
    public string TagClass
    {
      get
      {
        if(m_TagClass != "")
          return(m_TagClass);
        else
        {
          //TODO: figure out tag type based on extension/version
          return("");
        }
      }
    }
    public bool Exists
    {
      get
      {
        return(!(m_TagSource == TagSource.NotFound));
      }
    }

    public string MenuName
    {
      get
      {
        int name_start = m_RelativePath.LastIndexOf('\\');
        string temp = m_RelativePath.Substring(name_start+1);
        int ext_index = temp.IndexOf('.');
        temp = temp.Substring(0, ext_index);

        return(temp);
      }
    }
    public string RelativePath
    {
      get{return m_RelativePath;}
      set{m_RelativePath = value;}
    }
    public TagSource Source
    {
      get{return m_TagSource;}
      set{m_TagSource = value;}
    }
    public MapfileVersion Version
    {
      get{return m_Version;}
      set{m_Version = value;}
    }
    public string FileExtension
    {
      get
      {
        int i = m_RelativePath.IndexOf(".");
        string tmp = m_RelativePath.Substring(i+1);
        tmp = tmp.ToLower();
        return(tmp);
      }
    }
    public TagFileName(string RelativePath, MapfileVersion ver, TagSource source)
    {
      m_RelativePath = RelativePath;
      m_Version = ver;
      m_TagSource = source;

      //TODO:  run "Exists" test on source to see if we need to set the NotFound flag
    }
    public TagFileName(string RelativePath, string TagClass, MapfileVersion ver)
    {

      if (RelativePath == null)
      {
        Console.WriteLine("break here");
      }
      m_TagClass = TagClass;
      m_RelativePath = RelativePath;
      m_Version = ver;

      if(m_RelativePath.IndexOf('.') == -1)
      {
        switch(TagClass)
        {
          case "soso":
            m_RelativePath += ".shader_model";
            break;
					case "dahs":
          case "shad":
            m_RelativePath += ".shad";
            break;
          case "antr":
            m_RelativePath += ".model_animations";
            break;
          case "bitm":
            m_RelativePath += ".bitmap";
            break;
          case "scen":
            m_RelativePath += ".scenery";
            break;
          case "vehi":
            m_RelativePath += ".vehicle";
            break;
          case "eqip":
            m_RelativePath += ".equipment";
            break;
          case "weap":
            m_RelativePath += ".weapon";
            break;
          case "scex":
            m_RelativePath += ".shader_transparent_chicago_extended";
            break;
          case "schi":
            m_RelativePath += ".shader_transparent_chicago";
            break;
          case "senv":
            m_RelativePath += ".shader_environment";
            break;
          case "mod2":
            m_RelativePath += ".gbxmodel";
            break;
          case "mode":
            m_RelativePath += ".model";
            break;
          case "sgla":
            m_RelativePath += ".shader_transparent_glass";
            break;
          case "smet":
            m_RelativePath += ".shader_metal";
            break;
          case "sbsp":
            m_RelativePath += ".scenario_structure_bsp";
            break;
          case "itmc":
            m_RelativePath += ".item_collection";
            break;
          case "sky ":
            m_RelativePath += ".sky";
            break;
					case "swat":
						m_RelativePath += ".shader_transparent_water";
						break;
					case "sotr":
						m_RelativePath += ".shader_transparency_generic";
						break;
          case "deca":
            m_RelativePath += ".decal";
            break;
          case "spla":
            m_RelativePath += ".shader_transparent_plasma";
            break;
          default:
            throw new PrometheusException("Error on TagFilename create: No matching TagClass for " + TagClass, true);
            break;
        }
      }

      DetermineTagSource();
    }
    public TagFileName(string RelativePath, MapfileVersion ver)
    {
      m_RelativePath = RelativePath;
      m_Version = ver;
      DetermineTagSource();
    }
    private void DetermineTagSource()
    {
      //priority goes local, local shared, prefab, archive
      bool bFound = false;

      //highest priority:  debug
      if(RelativePath.IndexOf(":") != -1)
      {
        m_TagSource = TagSource.Debug;
        bFound = true;
      }

      //is it local project
      if(!bFound)
      {
        string proj_path = OptionsManager.ActiveProjectPath;
        proj_path += RelativePath;
        if (proj_path.Length < 250)
        {
          if(File.Exists(proj_path))
          {
            m_TagSource = TagSource.LocalProject;
            bFound = true;
          }
        }
      }

      //is it local shared/common?
      if(!bFound)
      {
        string shared_path = OptionsManager.GetSharedTagsPath(m_Version);
        shared_path += RelativePath;
        if (shared_path.Length < 250)
        {
          if(File.Exists(shared_path))
          {
            m_TagSource = TagSource.LocalShared;
            bFound = true;
          }
        }
      }

      //is it a prefab?
      if(!bFound)
      {
        if(ProjectManager.PrefabList != null)
        {
          TagFileName[] tfn_list = ProjectManager.PrefabList;
          for(int i=0; i<tfn_list.Length; i++)
          {
            if(RelativePath == tfn_list[i].RelativePath)
            {
              //todo: figure out how to access the prefab source data
              //if(File.Exists(prefab_path))
              //{
              //  m_TagSource = TagSource.Prefab;
              //  bFound = true;
              //}
            }
          }
        }
      }

      if(!bFound)
      {
        m_TagSource = TagSource.Archive;

        switch(m_Version)
        {
          case MapfileVersion.HALOPC:
          case MapfileVersion.HALOCE:
            bFound = TagLibraryManager.HaloPC.FileExists(m_RelativePath);
            break;

          case MapfileVersion.XHALO1:
            bFound = TagLibraryManager.HaloXbox.FileExists(m_RelativePath);
            break;
          
          case MapfileVersion.XHALO2:
            bFound = TagLibraryManager.Halo2Xbox.FileExists(m_RelativePath);
            break;
        }
        //TODO:  verify file exists in archive
      }

      if(!bFound)
        m_TagSource = TagSource.NotFound;
    }
    
    
    static public string[] GetFilterList()
    {
      string[] filter_list = new string[halo1Table.Length];

      for(int i=0; i<halo1Table.Length; i++)
      {
        filter_list[i] = "*" + halo1Table[i].FilenameExt;
      }

      return(filter_list);
    }
    static public string GetFileExtension(string tagclass)
    {
      string file_extension = null;
      int i;

      for(i=0; i<halo1Table.Length; i++)
        if(tagclass == halo1Table[i].TagClass)
          break;

      if(i < halo1Table.Length)
        file_extension = halo1Table[i].FilenameExt;

      return(file_extension);
    }
    static public void InitExtensionTables()
    {
      for(int i=0; i<halo1Table.Length; i++)
        halo1Table[i] = new TagClassTable();

      halo1Table[0].TagClass = "actr";
      halo1Table[0].FilenameExt = ".actor";
      
      halo1Table[1].TagClass = "actv";
      halo1Table[1].FilenameExt = ".actor_variant";
      
      halo1Table[2].TagClass = "ant!";
      halo1Table[2].FilenameExt = ".antenna";
      
      halo1Table[3].TagClass = "antr";
      halo1Table[3].FilenameExt = ".animation_trigger";
      
      halo1Table[4].TagClass = "bipd";
      halo1Table[4].FilenameExt = ".biped";
      
      halo1Table[5].TagClass = "bitm";
      halo1Table[5].FilenameExt = ".bitmap";
      
      halo1Table[6].TagClass = "coll";
      halo1Table[6].FilenameExt = ".collision_model";
      
      halo1Table[7].TagClass = "colo";
      halo1Table[7].FilenameExt = ".color_group";
      
      halo1Table[8].TagClass = "cont";
      halo1Table[8].FilenameExt = ".contrail";
      
      halo1Table[9].TagClass = "ctrl";
      halo1Table[9].FilenameExt = ".control";
      
      halo1Table[10].TagClass = "deca";
      halo1Table[10].FilenameExt = ".decal";
      
      halo1Table[11].TagClass = "DeLa";
      halo1Table[11].FilenameExt = ".ui_item";
      
      halo1Table[12].TagClass = "devi";
      halo1Table[12].FilenameExt = ".device";
      
      halo1Table[13].TagClass = "dobc";
      halo1Table[13].FilenameExt = ".detail_object_collection";
      
      halo1Table[14].TagClass = "effe";
      halo1Table[14].FilenameExt = ".effect";
      
      halo1Table[15].TagClass = "elec";
      halo1Table[15].FilenameExt = ".lightning";
      
      halo1Table[16].TagClass = "eqip";
      halo1Table[16].FilenameExt = ".equipment";
      
      halo1Table[17].TagClass = "flag";
      halo1Table[17].FilenameExt = ".flag";
      
      halo1Table[18].TagClass = "fog ";
      halo1Table[18].FilenameExt = ".fog";
      
      halo1Table[19].TagClass = "font";
      halo1Table[19].FilenameExt = ".font";
      
      halo1Table[20].TagClass = "foot";
      halo1Table[20].FilenameExt = ".material_effects";
      
      halo1Table[21].TagClass = "garb";
      halo1Table[21].FilenameExt = ".garbage";
      
      halo1Table[22].TagClass = "glw!";
      halo1Table[22].FilenameExt = ".glow";
      
      halo1Table[23].TagClass = "grhi";
      halo1Table[23].FilenameExt = ".grenade_hud_interface";
      
      halo1Table[24].TagClass = "hmt ";
      halo1Table[24].FilenameExt = ".hud_message_text";
      
      halo1Table[25].TagClass = "hud#";
      halo1Table[25].FilenameExt = ".hud_number";
      
      halo1Table[26].TagClass = "hudg";
      halo1Table[26].FilenameExt = ".hud_globals";
      
      halo1Table[27].TagClass = "item";
      halo1Table[27].FilenameExt = ".item";
      
      halo1Table[28].TagClass = "itmc";
      halo1Table[28].FilenameExt = ".item_collection";
      
      halo1Table[29].TagClass = "jpt!";
      halo1Table[29].FilenameExt = ".damage";
      
      halo1Table[30].TagClass = "lens";
      halo1Table[30].FilenameExt = ".lensflare";
      
      halo1Table[31].TagClass = "lifi";
      halo1Table[31].FilenameExt = ".light_fixture";
      
      halo1Table[32].TagClass = "ligh";
      halo1Table[32].FilenameExt = ".light";
      
      halo1Table[33].TagClass = "lsnd";
      halo1Table[33].FilenameExt = ".looping_sound";
      
      halo1Table[34].TagClass = "mach";
      halo1Table[34].FilenameExt = ".machine";
      
      halo1Table[35].TagClass = "matg";
      halo1Table[35].FilenameExt = ".game_globals";
      
      halo1Table[36].TagClass = "metr";
      halo1Table[36].FilenameExt = ".meter";
      
      halo1Table[37].TagClass = "mgs2";
      halo1Table[37].FilenameExt = ".light_volume";
      
      halo1Table[38].TagClass = "mod2";
      halo1Table[38].FilenameExt = ".model_version_2-gearbox_format";
      
      halo1Table[39].TagClass = "mode";
      halo1Table[39].FilenameExt = ".model";
      
      halo1Table[40].TagClass = "mply";
      halo1Table[40].FilenameExt = ".multiplayer_scenario";
      
      halo1Table[41].TagClass = "obje";
      halo1Table[41].FilenameExt = ".object";
      
      halo1Table[42].TagClass = "part";
      halo1Table[42].FilenameExt = ".particle";
      
      halo1Table[43].TagClass = "pctl";
      halo1Table[43].FilenameExt = ".particle_system";
      
      halo1Table[44].TagClass = "phys";
      halo1Table[44].FilenameExt = ".physics";
      
      halo1Table[45].TagClass = "plac";
      halo1Table[45].FilenameExt = ".placeholder";
      
      halo1Table[46].TagClass = "pphy";
      halo1Table[46].FilenameExt = ".point_physics";
      
      halo1Table[47].TagClass = "proj";
      halo1Table[47].FilenameExt = ".projectile";
      
      halo1Table[48].TagClass = "rain";
      halo1Table[48].FilenameExt = ".weather_particle";
      
      halo1Table[49].TagClass = "sbsp";
      halo1Table[49].FilenameExt = ".structure_bsp";
      
      halo1Table[50].TagClass = "scen";
      halo1Table[50].FilenameExt = ".scenery";
      
      halo1Table[51].TagClass = "schi";
      halo1Table[51].FilenameExt = ".shader_transparency_variant";
      
      halo1Table[52].TagClass = "scnr";
      halo1Table[52].FilenameExt = ".scenario";
      
      halo1Table[53].TagClass = "senv";
      halo1Table[53].FilenameExt = ".shader_environment";
      
      halo1Table[54].TagClass = "sgla";
      halo1Table[54].FilenameExt = ".shader_glass";
      
      halo1Table[55].TagClass = "shdr";
      halo1Table[55].FilenameExt = ".shader";
      
      halo1Table[56].TagClass = "sky ";
      halo1Table[56].FilenameExt = ".sky";
      
      halo1Table[57].TagClass = "smet";
      halo1Table[57].FilenameExt = ".shader_metal";
      
      halo1Table[58].TagClass = "snd!";
      halo1Table[58].FilenameExt = ".sound";
      
      halo1Table[59].TagClass = "snde";
      halo1Table[59].FilenameExt = ".sound_environment";
      
      halo1Table[60].TagClass = "soso";
      halo1Table[60].FilenameExt = ".shader_model";
      
      halo1Table[61].TagClass = "sotr";
      halo1Table[61].FilenameExt = ".shader_transparency";
      
      halo1Table[62].TagClass = "Soul";
      halo1Table[62].FilenameExt = ".ui_item_collection";
      
      halo1Table[63].TagClass = "spla";
      halo1Table[63].FilenameExt = ".shader_plasma";
      
      halo1Table[64].TagClass = "ssce";
      halo1Table[64].FilenameExt = ".sound_scenery";
      
      halo1Table[65].TagClass = "str#";
      halo1Table[65].FilenameExt = ".string_list";
      
      halo1Table[66].TagClass = "swat";
      halo1Table[66].FilenameExt = ".shader_water";
      
      halo1Table[67].TagClass = "tagc";
      halo1Table[67].FilenameExt = ".tag_collection";
      
      halo1Table[68].TagClass = "trak";
      halo1Table[68].FilenameExt = ".camera_track";
      
      halo1Table[69].TagClass = "udlg";
      halo1Table[69].FilenameExt = ".dialog";
      
      halo1Table[70].TagClass = "unhi";
      halo1Table[70].FilenameExt = ".unit_hud_interface";
      
      halo1Table[71].TagClass = "unit";
      halo1Table[71].FilenameExt = ".unit";
      
      halo1Table[72].TagClass = "ustr";
      halo1Table[72].FilenameExt = ".unicode_string";
      
      halo1Table[73].TagClass = "vcky";
      halo1Table[73].FilenameExt = ".virtual_keyboard";
      
      halo1Table[74].TagClass = "vehi";
      halo1Table[74].FilenameExt = ".vehicle";
      
      halo1Table[75].TagClass = "weap";
      halo1Table[75].FilenameExt = ".weapon";
      
      halo1Table[76].TagClass = "wind";
      halo1Table[76].FilenameExt = ".wind";

      halo1Table[77].TagClass = "wphi";
      halo1Table[77].FilenameExt = ".weapon_hud_interface";
    }
  }

  /// <summary>
  /// Holds shared functionality for loading data structures
  /// from a file stream.  Optimized for speed of loading.
  /// </summary>
  public class TagBase
  {
    public float RAD_TO_DEG = 57.2957795f;
    public string TagFilename;
    protected int m_BufIndex;
    protected int m_BufLength;
    protected byte [] m_Buffer;
    protected AttachmentHeader m_attachmentHeader;
    protected AttachmentElement[] m_attachments;
    protected TagHeaderType m_headerType;
	  protected MemoryStream m_stream;
    protected TagHeader m_PromHeader;
    public enum TagHeaderType : int
    {
      Blam = 1,
      Prometheus = 2,
      Unknown = 0
    }
    public TagBase()
    {
      m_BufIndex = 0;
      m_BufLength = 0;
      m_PromHeader = new TagHeader();
    }
    public MemoryStream Stream
    {
      get{return m_stream;}
      set{m_stream = value;}
    }
		public TagHeader Header
		{
			get{return m_PromHeader;}
		}
    public void SaveTagBuffer(TagFileName tfn)
    {
      string tag_path = "";

      switch(tfn.Source)
      {
        case TagSource.Debug:
          tag_path = tfn.RelativePath;
          break;

        case TagSource.LocalProject:
          tag_path = OptionsManager.ActiveProjectPath + @"Tags\" + tfn.RelativePath;
          break;

        default:
          throw new Exception("Cannot save a tag that isn't local project or debug.");
          break;
      }
      FileStream tagFile = new FileStream(tag_path, FileMode.Create);
      BinaryWriter tagWriter = new BinaryWriter(tagFile);

      tagWriter.BaseStream.Position = 0;
      this.m_PromHeader.Write(ref tagWriter);
      byte[] temp = this.m_stream.GetBuffer();
      tagWriter.Write(temp, 0, this.m_PromHeader.TagSize);
      tagFile.Close();
    }
    public bool LoadTagBuffer(TagFileName tfn)
    {
      this.TagFilename = tfn.RelativePath;
      bool bStatus = true;
      FileStream tagFile = null;
      BinaryReader tagReader = null;

      try 
      {
        //read file into memory buffer
        switch(tfn.Source)
        {
          case TagSource.Debug:
          case TagSource.LocalShared:
            tagFile = new FileStream(tfn.RelativePath, FileMode.Open);
            tagReader = new BinaryReader(tagFile);
            m_BufLength = (int)tagReader.BaseStream.Length;
            m_Buffer = new byte[m_BufLength];
            tagReader.BaseStream.Position = 0;
            m_Buffer = tagReader.ReadBytes(m_BufLength);	
            tagFile.Close();
            break;

          case TagSource.LocalProject:
            tagFile = new FileStream(OptionsManager.ActiveProjectTagsPath + tfn.RelativePath, FileMode.Open);
            tagReader = new BinaryReader(tagFile);
            m_BufLength = (int)tagReader.BaseStream.Length;
            m_Buffer = new byte[m_BufLength];
            tagReader.BaseStream.Position = 0;
            m_Buffer = tagReader.ReadBytes(m_BufLength);	
            tagFile.Close();
            break;

          case TagSource.Prefab:
            break;
          
          case TagSource.Archive:
            if((tfn.Version == MapfileVersion.HALOPC)||(tfn.Version == MapfileVersion.HALOCE))
            {
              m_Buffer = TagLibraryManager.HaloPC.ReadFile(tfn.RelativePath);
              m_BufLength = m_Buffer.Length;
            }
            else if(tfn.Version == MapfileVersion.XHALO1)
            {
              //todo:  read buffer from archive
							m_Buffer = TagLibraryManager.HaloXbox.ReadFile(tfn.RelativePath);
							m_BufLength = m_Buffer.Length;
            }
            else if(tfn.Version == MapfileVersion.XHALO2)
            {
							m_Buffer = TagLibraryManager.Halo2Xbox.ReadFile(tfn.RelativePath);
							m_BufLength = m_Buffer.Length;
            }
            break;
          case TagSource.NotFound:
            Trace.WriteLine("Could not find " + tfn.RelativePath);
            bStatus = false;
            break;
        }

        if(m_Buffer != null)
        {
          // Check for known headers
          int[] bin = new int[0x10];
          for (int x=0; x<bin.Length; x++)
            bin[x] = BitConverter.ToInt32(m_Buffer, x*4); 
        
          m_headerType = TagHeaderType.Unknown;
          if (bin[0xF] == 0x6D616C62) m_headerType = TagHeaderType.Blam;
          if (bin[0x0] == 0x4d4f5250/*0x70726F6D*/) m_headerType = TagHeaderType.Prometheus;

        
          // Create the memory stream, load the header struct
          if(m_headerType == TagHeaderType.Blam)
          {
            // Read the tag and create the memorystream
            int headerLength = 0x40;
            m_stream = new MemoryStream(m_Buffer, headerLength, m_Buffer.Length - headerLength);
          }
          else if (m_headerType == TagHeaderType.Prometheus)
          {
            m_PromHeader.Read(m_Buffer);

            m_BufLength = m_PromHeader.TagSize;
            m_stream = new MemoryStream(m_Buffer, TagHeader.PROM_HEADER_SIZE, m_PromHeader.TagSize);
        
            // Read the attachment header
            //m_PromHeader.SeekToAttachStart(ref tagReader);
            //m_attachmentHeader.Read(ref tagReader);
          }
          else
          {
            bStatus = false;
            throw new PrometheusException(
              "Unsupported tag file format.", true);
          }
        }
      }
      catch (Exception ex)
      {
        throw new PrometheusException(
          "The specified tag could not be loaded: " + tfn.RelativePath,
          ex,
          true);
      }
      return(bStatus);
    }
  }
}
