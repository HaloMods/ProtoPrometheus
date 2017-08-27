using System;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using Microsoft.Win32;
using Nini.Config;
using System.Text.RegularExpressions;

namespace Prometheus.Core
{
  public enum MapfileVersion{XHALO1 = 0x5, XHALO2 = 0x8, HALOPC = 0x7, HALOCE = 0x261};
  public enum TagSource
  {
    NotFound = 0,
    Debug = 1,
    LocalProject = 2,
    LocalShared = 3,
    Prefab = 4,
    Archive = 5
  }

  /// <summary>
  /// Manages all persistent application settings and user settings.
  /// </summary>
  public class OptionsManager
  {
    private static SettingsInterface core = new SettingsInterface();
    private static SettingsInterface user = new SettingsInterface();
    private static bool ready = false;

    //private setting vars
    static private string haloPC_MapsPath = "";
    static private string haloCE_MapsPath = "";
    static private string xhalo1_MapsPath = "";
    static private string xhalo2_MapsPath = "";
    static private string activeProjectPath = "";

    static private bool enableDebugLogging;
    static private int maxLogfileSize;
    static private string logDetail = "";

    static private int cameraSensitivity = 5;
    static private int cameraSpeed = 5;
    static private bool enableFPS = true;

    #region "Paths"
    static public string HaloPC_MapsPath
    {
      get{return haloPC_MapsPath;}
      set{haloPC_MapsPath = value;}
    }
    static public string HaloCE_MapsPath
    {
      get{return haloCE_MapsPath;}
      set{haloCE_MapsPath = value;}
    }
    static public string XHalo1_MapsPath
    {
      get{return xhalo1_MapsPath;}
      set{xhalo1_MapsPath = value;}
    }
    static public string XHalo2_MapsPath
    {
      get{return xhalo2_MapsPath;}
      set{xhalo2_MapsPath = value;}
    }



    static public string HaloPC_BitmapsPath
    {
      get{return haloPC_MapsPath+"\\bitmaps.map";}
    }

    static public string HaloPC_SoundsPath
    {
      get{return haloPC_MapsPath+"\\sounds.map";}
    }

    static public string HaloCE_BitmapsPath
    {
      get{return haloCE_MapsPath+"\\bitmaps.map";}
    }

    static public string HaloCE_SoundsPath
    {
      get{return haloCE_MapsPath+"\\sounds.map";}
    }

    static public FileStream GetBitmapStream(MapfileVersion ver)
    {
      string bitmapPath = "";
      FileStream BitmapsMapStream;

      switch(ver)
      {
        case MapfileVersion.XHALO1:
          bitmapPath = HaloPC_BitmapsPath;
          break;
        case MapfileVersion.HALOPC:
          bitmapPath = HaloPC_BitmapsPath;
          break;
        case MapfileVersion.HALOCE:
          bitmapPath = HaloCE_BitmapsPath;
          break;
      }

      try
      {
        FileInfo fiBitmap = new FileInfo(bitmapPath);
        BitmapsMapStream = fiBitmap.Open(FileMode.Open,FileAccess.Read, FileShare.Read);
      }
      catch(Exception e)
      {
        throw new PrometheusException("Failed to load Bitmaps.map: " + e.Message, true);
      }

      return(BitmapsMapStream); 
    }
    static public FileStream GetSoundStream(MapfileVersion ver)
    {
      string soundPath = "";
      FileStream fsSound;

      switch(ver)
      {
        case MapfileVersion.XHALO1:
          soundPath = HaloPC_SoundsPath;
          break;
        case MapfileVersion.HALOPC:
          soundPath = HaloPC_SoundsPath;
          break;
        case MapfileVersion.HALOCE:
          soundPath = HaloCE_SoundsPath;
          break;
      }

      try
      {
        FileInfo fiSound = new FileInfo(soundPath);
        fsSound = fiSound.Open(FileMode.Open,FileAccess.Read, FileShare.ReadWrite);
      }
      catch(Exception e)
      {
        throw new PrometheusException("Failed to load Sounds.map: " + e.Message, true);
      }
        
      return(fsSound); 
    }
    static public FileStream GetHalo2SharedStream()
    {
      FileStream SharedMapStream;

      try
      {
        FileInfo fiShared = new FileInfo(xhalo2_MapsPath+"shared.map");
        SharedMapStream = fiShared.Open(FileMode.Open,FileAccess.Read);
      }
      catch(Exception e)
      {
        throw new PrometheusException("Failed to load shared.map: " + e.Message, true);
      }

      return(SharedMapStream); 
    }
    static public FileStream GetHalo2SinglePlayerSharedStream()
    {
      FileStream SharedSpStream;

      try
      {
        FileInfo fiSpShared = new FileInfo(xhalo2_MapsPath+"single_player_shared.map");
        SharedSpStream = fiSpShared.Open(FileMode.Open,FileAccess.Read);
      }
      catch(Exception e)
      {
        throw new PrometheusException("Failed to load SinglePlayerShared.map: " + e.Message, true);
      }

      return(SharedSpStream); 
    }
    static public FileStream GetHalo2MainMenuStream()
    {
      FileStream MenuStream;

      try
      {
        FileInfo fiMenu = new FileInfo(xhalo2_MapsPath+"mainmenu.map");
        MenuStream = fiMenu.Open(FileMode.Open,FileAccess.Read);
      }
      catch(Exception e)
      {
        throw new PrometheusException("Failed to load MainMenu.map: " + e.Message, true);
      }

      return(MenuStream); 
    }
    #endregion

    #region "Project"
    static public string LightmapOutputPath
    {
      get{return activeProjectPath + @"lightmaps";}
    }
    static public string ActiveProjectPath
    {
      get{return activeProjectPath;}
      set{activeProjectPath = value;}
    }
    static public string ActiveProjectTagsPath
    {
      get{return activeProjectPath + "Tags\\";}
    }
    static public string GetProjectBaseFolder(MapfileVersion version)
    {
      string proj_base = "";

      switch(version)
      {
        case MapfileVersion.XHALO1:
          proj_base = Application.StartupPath + @"\Games\Xbox\Halo\Projects\";
          break;
        case MapfileVersion.XHALO2:
          proj_base = Application.StartupPath + @"\Games\Xbox\Halo2\Projects\";
          break;
        case MapfileVersion.HALOPC:
        case MapfileVersion.HALOCE:
          proj_base = Application.StartupPath + @"\Games\Pc\Halo\Projects\";
          break;
      }

      return(proj_base);
    }
    #endregion

    #region "Logging"
    static public bool EnableDebugLogging
    {
      get{return enableDebugLogging;}
      set{enableDebugLogging = value;}
    }
    static public int MaxLogfileSize
    {
      get{return maxLogfileSize;}
      set{maxLogfileSize = value;}
    }
    static public string LogDetail
    {
      get{return logDetail;}
      set{logDetail = value;}
    }
    #endregion

    #region "Render"
    static public int CameraSensitivity
    {
      get{return cameraSensitivity;}
      set{cameraSensitivity = value;}
    }
    static public int CameraSpeed
    {
      get{return cameraSpeed;}
      set{cameraSpeed = value;}
    }
    static public bool EnableFPS
    {
      get{return enableFPS;}
      set{enableFPS = value;}
    }
    #endregion

    static public string GetPrefabPath(MapfileVersion version)
    {
      string prefab_path = "";

      switch(version)
      {
        case MapfileVersion.XHALO1:
          prefab_path = Application.StartupPath + @"\Games\Xbox\Halo\Prefabs";
          break;
        case MapfileVersion.XHALO2:
          prefab_path = Application.StartupPath + @"\Games\Xbox\Halo 2\Prefabs";
          break;
        case MapfileVersion.HALOPC:
        case MapfileVersion.HALOCE:
          prefab_path = Application.StartupPath + @"\Games\PC\Halo\Prefabs";
          break;
      }

      return(prefab_path);
    }
    static public string GetSharedTagsPath(MapfileVersion version)
    {
      string shared_tags_path = "";

      switch(version)
      {
        case MapfileVersion.XHALO1:
          shared_tags_path = Application.StartupPath + @"\Games\Xbox\Halo\Prefabs\Tags";
          break;
        case MapfileVersion.XHALO2:
          shared_tags_path = Application.StartupPath + @"\Games\Xbox\Halo 2\Prefabs\Tags";
          break;
        case MapfileVersion.HALOPC:
        case MapfileVersion.HALOCE:
          shared_tags_path = Application.StartupPath + @"\Games\PC\Halo\Prefabs\Tags";
          break;
      }

      return(shared_tags_path);
    }
    static public string GetExtractPath(MapfileVersion version)
    {
      string extract_path = "";

      switch(version)
      {
        case MapfileVersion.XHALO1:
          extract_path = Application.StartupPath + @"\Games\Xbox\Halo\";
          break;
        case MapfileVersion.XHALO2:
          extract_path = Application.StartupPath + @"\Games\Xbox\Halo2\";
          break;
        case MapfileVersion.HALOPC:
        case MapfileVersion.HALOCE:
          extract_path = Application.StartupPath + @"\Games\Pc\Halo\";
          break;
      }

      return(extract_path);
    }
    static public string GetMagfilePath(MapfileVersion ver)
    {
      string mag_path = "";

      switch(ver)
      {
        case MapfileVersion.XHALO1:
          mag_path = Application.StartupPath + @"\Tag Structures\XHalo\";
          break;
        case MapfileVersion.XHALO2:
          mag_path = Application.StartupPath + @"\Tag Structures\Halo2\";
          break;
        case MapfileVersion.HALOPC:
          mag_path = Application.StartupPath + @"\Tag Structures\PcHalo\";
          break;
        case MapfileVersion.HALOCE:
          mag_path = Application.StartupPath + @"\Tag Structures\CeHalo\";
          break;
      }

      return(mag_path);
    }

    static public void GetGuerillaHeader(MapfileVersion ver, string TagClass, byte[] HeaderData)
    {
      FileInfo fi = null;
      if(ver == MapfileVersion.XHALO2)
        fi = new FileInfo(Application.StartupPath + @"\Halo2 Tag Headers\" + TagClass + "." + "BLAM");
      else
        fi = new FileInfo(Application.StartupPath + @"\Tag Headers\" + TagClass + "." + "BLAM");

      FileStream TagHdrStream;
      TagHdrStream = fi.Open(FileMode.Open,FileAccess.Read);
      TagHdrStream.Read(HeaderData,0,64);
      TagHdrStream.Close();
    }

    public static SettingsInterface Core
    {
      get
      {
        if (!ready)
          throw new Exception("OptionsManager has not been initialized.");
        return core;
      }
    }

    public static SettingsInterface User
    {
      get
      {
        if (!ready) 
          throw new Exception("OptionsManager has not been initialized.");
        return user;
      }
    }

    public static void Initialize(string coreFilename, string userFilename)
    {
      core.Filename = coreFilename;
      user.Filename = userFilename;
      ready = true;
    }
    public static void LoadProfile()
    {
      string hpc_maps = "";
      string hce_maps = "";
      #region Detect Registry Install Paths
      RegistryKey rk;
      //Set default values for system options
      rk = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft Games\\Halo", false);
      if(rk != null)
      {
        string PcInstallPath = (String)rk.GetValue("EXE Path");
        rk.Close();
        hpc_maps = PcInstallPath + @"\maps\";
      }

      rk = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft Games\\Halo CE", false);
      if(rk != null)
      {
        string CeInstallPath = (String)rk.GetValue("EXE Path");
        rk.Close();
        hce_maps = CeInstallPath + @"\maps\";
      }
      #endregion

      Initialize(Application.StartupPath + "\\prometheus.ini", Application.StartupPath + "\\user.ini");
      haloPC_MapsPath = (string)Core["Paths", "HaloPC_MapsPath", hpc_maps];
      haloCE_MapsPath = (string)Core["Paths", "HaloCE_MapsPath", hce_maps];
      xhalo1_MapsPath = (string)Core["Paths", "XHalo1_MapsPath", ""];
      xhalo2_MapsPath = (string)Core["Paths", "XHalo2_MapsPath", ""];
      activeProjectPath = (string)Core["Project", "ActiveProjectPath", ""];
      enableDebugLogging = (bool)Core["Logging","EnableDebugLogging", true];
      maxLogfileSize = (int)Core["Logging","MaxLogfileSize", 1024];
      logDetail = (string)Core["Logging", "LogDetail", "Critical Errors"];
      cameraSensitivity = (int)Core["Render","CameraSensitivity", 5];
      cameraSpeed = (int)Core["Render","CameraSpeed", 5];
      enableFPS =  (bool)Core["Render","EnableFPS", true];
    }
    public static void SaveProfile()
    {
      Core["Paths", "HaloPC_MapsPath"] = haloPC_MapsPath;
      Core["Paths", "HaloCE_MapsPath"] = haloCE_MapsPath;
      Core["Paths", "XHalo1_MapsPath"] = xhalo1_MapsPath;
      Core["Paths", "XHalo2_MapsPath"] = xhalo2_MapsPath;
      Core["Project", "ActiveProjectPath"] = activeProjectPath;
      Core["Logging","EnableDebugLogging"] = enableDebugLogging;
      Core["Logging","MaxLogfileSize"] = maxLogfileSize;
      Core["Logging","LogDetail"] = logDetail;
      Core["Render","CameraSensitivity"] = cameraSensitivity;
      Core["Render","CameraSpeed"] = cameraSpeed;
      Core["Render","EnableFPS"] = enableFPS;
    }
  }

  #region Mono's settings manager
  /// <summary>
  /// Provides access to settings that are stored in a file.
  /// </summary>
  public class SettingsInterface
  {
    private SettingsList settings;
    private string filename;

    
    /// <summary>
    /// The file to be used for configuration data.
    /// </summary>
    public string Filename
    {
      get { return filename; }
      set
      { 
        try 
        {
          string folder = Path.GetDirectoryName(value);
          if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
          if (!File.Exists(value)) File.Create(value).Close();
        }
        catch 
        {
          throw new Exception("Error creating INI file or folder: " + value);
        }
        filename = value; 
        Load();
      }
    }

    public object this[string groupName, string valueName, object defaultValue]
    {
      get
      {
        Setting s = settings.FindSetting(groupName, valueName);
        if (s != null) return s.Data;
        
        // The setting doesn't exist - create it, add it to the list, and return it
        s = new Setting(groupName, valueName);
        settings.Add(s);
        s.Data = defaultValue;
        return s.Data;
      }
    }

    public object this[string groupName, string valueName]
    {
      set
      {
        Setting s = settings.FindSetting(groupName, valueName);
        if (s != null)
        {
          s.Data = value;
        }
        else
        {
          // The setting doesn't exist - create it and add it to the list.
          s = new Setting(groupName, valueName);
          settings.Add(s);
          s.Data = value;
        }
      }
    }
    
    // Added to allow you to delete a value from the INI file (thus resetting to default)
    public object this[bool delete, string groupName, string valueName]
    {
      set
      {
        Setting s = settings.FindSetting(groupName, valueName);
        
        // If we've found it, delete it.
        if (s != null && delete == true)
        {
          settings.Remove(groupName, valueName);
        }
      }      
    }

    public SettingsInterface()
    {
      settings = new SettingsList();
      settings.Dirty += new EventHandler(Settings_Dirty);
    }

    private void Settings_Dirty(object sender, EventArgs e)
    {
      Save();
    }
    
    public void Save()
    {
      IConfigSource source = new IniConfigSource(filename);
      
      foreach (Setting setting in settings)
      {
        IConfig config;
        if (source.Configs[setting.Group] == null)
        {
          config = source.AddConfig(setting.Group);  
        }
        else
        {
          config = source.Configs[setting.Group];
        }
        config.Set(setting.Name, setting.Data);
      }
      source.Save();
    }

    public void Load()
    {
      IConfigSource source = new IniConfigSource(filename);
      foreach (IConfig config in source.Configs)
      {
        string[] keys = config.GetKeys();
        for (int x=0; x<keys.Length; x++)
        {
          Setting setting = new Setting(config.Name, keys[x]);
          setting.Data = config.GetString(keys[x]);
          settings.Add(setting);
        }
      }
    }
    // Added to allow you to delete a value from the INI file (thus resetting to default)
    public void Delete(string groupName, string valueName)
    {
      Setting s = settings.FindSetting(groupName, valueName);
        
      // If we've found it, delete it.
      if (s != null)
      {
        settings.Remove(groupName, valueName);
      }
    }
  }

  public class Setting
  {
    private string group;
    private string name;
    private object data;

    public string Group
    {
      get { return group; }
    }
    public string Name
    {
      get { return name; }
    }
    public object Data
    {
      get
      {
        if (data is string)
        {
          if ((string)data == "False") return false;
          if ((string)data == "True") return true;
          if (IsInteger((string)data))
          {
            return (Convert.ToInt32((string)data));
          }
          if (IsFloat((string)data))
          {
            return (Convert.ToSingle((string)data));
          }
        }
        return data;
      }
      set
      {
        data = value;
        if (Updated != null) Updated(this, new EventArgs());
      }
    }

    protected bool IsInteger(string value)
    {
      Regex notNumberPattern = new Regex("[^0-9.-]");
      String validIntegerPattern = "^([-]|[0-9])[0-9]*$";
      Regex objNumberPattern =new Regex(validIntegerPattern);
      return !notNumberPattern.IsMatch(value) &&  objNumberPattern.IsMatch(value);
    }

    protected bool IsFloat(string value)
    {
      Regex notNumberPattern = new Regex("[^0-9.-]");
      String validRealPattern="^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
      Regex objNumberPattern =new Regex(validRealPattern);
      return !notNumberPattern.IsMatch(value) &&  objNumberPattern.IsMatch(value); 
    }

    public event EventHandler Updated;

    public Setting(string group, string name)
    {
      this.group = group;
      this.name = name;
    }
  }

  public class SettingsList : CollectionBase
  {
    public Setting FindSetting(string groupName, string valueName)
    {
      foreach (Setting setting in List)
      {
        if (setting.Group.ToLower() == groupName.ToLower())
        {
          if (setting.Name.ToLower() == valueName.ToLower())
          {
            return setting;
          }
        }
      }
      return null;
    }

    public void Add(Setting setting)
    {
      List.Add(setting);
      setting.Updated += new EventHandler(setting_Updated);        
    }

    public void Remove(string groupName, string valueName)
    {
      Setting setting = FindSetting(groupName, valueName);
      if (setting != null)
      {
        List.Remove(setting);
      }
    }

    public event EventHandler Dirty;

    private void setting_Updated(object sender, EventArgs e)
    {
      if (Dirty != null) Dirty(this, new EventArgs());
    }
  }
  #endregion
}