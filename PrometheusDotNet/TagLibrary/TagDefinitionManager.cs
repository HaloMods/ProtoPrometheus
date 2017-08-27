using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Xml;

namespace TagLibrary
{
	/// <summary>
	/// Manages Type Definition files.
	/// This is a work in progress - we will need to decide how best to handle the definitions.  Obviously,
	/// having all of them loaded at once isn't going to work.  However, there are multiple sources from
	/// which a document can come (File, embedded resource).
	/// Idea #1: We store a reference to the document (either external file or embedded resource).
	///          When it is required, we load the TDF on the fly.  Any loaded TDF stays loaded until
	///          UnloadTDFS() is called.
	/// </summary>
	public class TagDefinitionManager
	{
		private static Hashtable definitions = new Hashtable();
    private static Hashtable tagTypes = new Hashtable();

    static TagDefinitionManager()
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
      tagTypes.Add("obje", "Object");
      tagTypes.Add("cdmg", "Continuous_Damage_Effect");
    }

    /// <summary>
		/// Registers all TDFs in the given path.
		/// </summary>
    public static void RegisterDefinitions(string path)
		{
		  string[] documents = Directory.GetFiles(path, "*.xml"); // This will eventually be *.tdf
      foreach (string filename in documents)
      {
        try
        {
          // TODO: Consider using something other than XmlDocument to speed things up.
          XmlDocument doc = new XmlDocument();
          doc.Load(filename);
          XmlNode nameNode = doc.SelectSingleNode("//name");
          if (nameNode == null) continue;
          string type = nameNode.Attributes["type"].InnerText;
          definitions.Add(type, filename);
        }
        catch (Exception ex)
        {
          throw new Exception("Unable to load TDF: " + filename, ex);
        }
      }
		}

    public static XmlDocument GetTagDefinition(string tagType)
    {
      tagType = (tagTypes[tagType] as string).ToLower();
      return GetTagDefinitionByName(tagType);
    }

    public static XmlDocument GetTagDefinitionByName(string tagType)
    {
      //      if (!definitions.ContainsKey(tagType))
      //        throw new Exception("TDF not found: " + tagType);
      //
      //      string filename = (string)(definitions[tagType]);
      //      XmlDocument doc = new XmlDocument();
      //      doc.Load(filename);
      //      return doc;
      
      // I'll come back to this - for now, I'm just going to load directly from the resources
      // since we are only dealing with Halo1 definitions at the moment.
      // Load the proper tag definition.
      

      Assembly a = Assembly.GetExecutingAssembly();
      string resourceName = "TagLibrary.Halo1.XML." + tagType + ".xml";
      Stream stream = a.GetManifestResourceStream(resourceName);
      XmlDocument tagDefinition = new XmlDocument();
      tagDefinition.Load(stream);
      return tagDefinition;
    }
	}
}
