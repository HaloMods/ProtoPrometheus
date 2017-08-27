using System;
using System.Collections;
using System.IO;

namespace Tester
{
  public class TagInfo
  {
    public string path;
    public Hashtable maps;
  }

  /// <summary>
	/// Summary description for HaloMapInterface.
	/// </summary>
	public class HaloMapInterface
	{
		private Hashtable tagTypes;
    public Hashtable Tags = new Hashtable();
    public IndexItem[] items;

    public HaloMapInterface()
		{
      tagTypes = new Hashtable();
      tagTypes.Add("bitm" , "Bitmap");
      tagTypes.Add("scnr" , "Scenario");
      tagTypes.Add("sbsp" , "Scenario_Structure_Bsp");
      tagTypes.Add("ant!", "Antenna");
      tagTypes.Add("actr", "Actor");
      tagTypes.Add("actv", "Actor_Variant");
      tagTypes.Add("antr", "Model_Animations");
      tagTypes.Add("bipd", "Biped");
      tagTypes.Add("ctrl", "Control");
      tagTypes.Add("coll", "Model_Collision_Geometry");
      tagTypes.Add("cont", "Contrail");
      tagTypes.Add("deca", "Decal");
      tagTypes.Add("jpt!", "Damage_Effect");
      tagTypes.Add("udlg", "Dialogue");
      tagTypes.Add("dobc", "Detail_Object_Collection");
      tagTypes.Add("DeLa", "UI_Widget_Definition");
      tagTypes.Add("eqip", "Equipment");
      tagTypes.Add("effe", "Effect");
      tagTypes.Add("fog ", "Fog");
      tagTypes.Add("elec", "Lightning");
      tagTypes.Add("font", "Font");
      tagTypes.Add("garb", "Garbage");
      tagTypes.Add("grhi", "Grenade_HUD_Interface");
      tagTypes.Add("matg", "Globals");
      tagTypes.Add("glw!", "Glow");
      tagTypes.Add("hud#", "HUD_Number");
      tagTypes.Add("hudg", "HUD_Globals");
      tagTypes.Add("hmt ", "HUD_Message Text");
      tagTypes.Add("item", "Item");
      tagTypes.Add("itmc", "Item_Collection");
      tagTypes.Add("lens", "Lens");
      tagTypes.Add("lsnd", "Sound_Looping");
      tagTypes.Add("lifi", "Light_Fixture");
      tagTypes.Add("ligh", "Light");
      tagTypes.Add("mgs2", "Light_Volume");
      tagTypes.Add("mach", "Machine");
      tagTypes.Add("mply", "Multiplayer_Scenario");
      tagTypes.Add("metr", "Meter");
      tagTypes.Add("part", "Particle");
      tagTypes.Add("pctl", "Particle_System");
      tagTypes.Add("phys", "Physics");
      tagTypes.Add("pphy", "Point_Physics");
      tagTypes.Add("mod2", "GBXModel");
      tagTypes.Add("proj", "Projectile");
      tagTypes.Add("devc", "PC_Device_Default");
      tagTypes.Add("snd!", "Sound");
      tagTypes.Add("ssce", "Sound_Scenery");
      tagTypes.Add("snde", "Sound_Environment");
      tagTypes.Add("scen", "Scenery");
      tagTypes.Add("senv", "shader_environment");
      tagTypes.Add("soso", "Shader_Model");
      tagTypes.Add("sotr", "Shader_Transparency_Generic");
      tagTypes.Add("swat", "Shader_Transparent_Water");
      tagTypes.Add("sgla", "Shader_Transparent_Glass");
      tagTypes.Add("smet", "Shader_Transparent_Meter");
      tagTypes.Add("spla", "Shader_Transparent_Plasma");
      tagTypes.Add("schi", "Shader_Transparent_Chicago");
      tagTypes.Add("sky ", "Sky");
      tagTypes.Add("trak", "Camera_Track");
      tagTypes.Add("unhi", "Unit_HUD_Interface");
      tagTypes.Add("ustr", "Unicode_String_list");
      tagTypes.Add("scex", "Shader_Transparent_Chicago_Extended");
      tagTypes.Add("tagc", "Tag_Collection");
      tagTypes.Add("foot", "Material_Effects");
      tagTypes.Add("str#", "String_List");
      tagTypes.Add("colo", "Color_Table");
      tagTypes.Add("flag", "Flag"); 
      tagTypes.Add("Soul", "UI_Widget_Collection");
      tagTypes.Add("vehi", "Vehicle");
      tagTypes.Add("vcky", "Virtual_keyboard");
      tagTypes.Add("weap", "Weapon");
      tagTypes.Add("wphi", "Weapon_HUD_Interface");
      tagTypes.Add("rain", "Weather_Particle_System");
      tagTypes.Add("wind", "Wind");
      tagTypes.Add("mode", "Model");
      tagTypes.Add("plac", "Placeholder");
      tagTypes.Add("shdr", "Shader");
      tagTypes.Add("unit", "Unit");
      tagTypes.Add("cdmg", "Continuous_Damage_Effect");
		}


    public void LoadMap(FileStream stream, string mapFilename)
    {
      stream.Position = 0;
      FileHeader fh = new FileHeader();
      fh.Load(stream);

      IndexHeader ih = new IndexHeader();
      stream.Position = fh.OffsetToIndex;
      ih.Load(stream);

      items = new IndexItem[ih.TagCount];
      for (int x=0; x<ih.TagCount; x++)
      {
        items[x] = new IndexItem();
        items[x].Load(stream);
      }
      
      for (int x=0; x<ih.TagCount; x++)
      {
        stream.Position = (int)(items[x].OffsetToString - (ih.IndexMagic - fh.OffsetToIndex - 40));
        byte[] character = new byte[1];
        string filename = "";
        do
        {
          character[0] = (byte)stream.ReadByte();
          if (character[0] != 0)
          {
            filename += System.Text.Encoding.ASCII.GetString(character);
          }
        }
        while (character[0] != 0);

        filename += "." + (string)tagTypes[items[x].Type];
        if (!Tags.ContainsKey(filename))
        {
          TagInfo t1 = new TagInfo();
          t1.maps = new Hashtable();
          t1.path = filename;
          Tags.Add(filename, t1);
        }

        TagInfo t2 = (TagInfo)Tags[filename];
        if (!t2.maps.ContainsKey(mapFilename)) t2.maps.Add(mapFilename, mapFilename);
      }
    }
	}

  #region File Structures
  public struct FileHeader
  {
    public uint OffsetToIndex;
    public uint MapVersion;
    public uint DecompressedSize;
    public void Load(FileStream stream)
    {
      byte[] buffer = new byte[2048];
      stream.Read(buffer, 0, buffer.Length);
      MapVersion = BitConverter.ToUInt32(buffer,0x04);
      OffsetToIndex = BitConverter.ToUInt32(buffer,0x10);
    }
  }
  public struct IndexHeader
  {
    public uint IndexMagic;
    public uint TagCount;
    public void Load(FileStream stream)
    {
      byte[] buffer = new byte[40];
      stream.Read(buffer, 0, buffer.Length);
      IndexMagic = BitConverter.ToUInt32(buffer, 0x00);
      TagCount = BitConverter.ToUInt32(buffer, 12);
    }
  }

  public struct IndexItem
  {
    public string Type;
    public uint ID;
    public uint OffsetToString;
    public uint MetaOffset;
    public uint MetaMagic;
    public void Load(FileStream stream)
    {
      byte[] buffer = new byte[32];
      stream.Read(buffer, 0, buffer.Length);
      Type = System.Text.Encoding.ASCII.GetString(buffer, 3, 1) +
        System.Text.Encoding.ASCII.GetString(buffer, 2, 1) + 
        System.Text.Encoding.ASCII.GetString(buffer, 1, 1) +
        System.Text.Encoding.ASCII.GetString(buffer, 0, 1);
      ID = BitConverter.ToUInt32(buffer, 12);
      OffsetToString = BitConverter.ToUInt32(buffer, 16);
      MetaOffset = BitConverter.ToUInt32(buffer, 20);
    }
  }
  #endregion

}
