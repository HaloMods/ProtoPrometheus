using System;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using Microsoft.Win32;
using Nini.Config;
using Prometheus.Core;

namespace Prometheus
{
  public enum MapfileVersion{XHALO1 = 0x5, XHALO2 = 0x8, HALOPC = 0x7, HALOCE = 0x261};

  /// <summary>
	/// Summary description for PromOptions.
	/// </summary>
	public class PromOptions
	{
    const int ProfileVersion = 1;
    const int DecompilerVersion = 1;
    const int PrometheusVersion = 1;
    public static SettingsList Settings;
    private static string SettingsFilename = Application.StartupPath + @"\Prometheus.ini";

    public static string HaloPC_BitmapsPath = "";
    public static string HaloPC_SoundsPath = "";
    public static string HaloCE_BitmapsPath = "";
    public static string HaloCE_SoundsPath = "";

    public static string XHalo1_UiPath = ""; //?
    public static string XHalo2_SharedPath = "";
    public static string XHalo2_SinglePlayerSharedPath = "";
    public static string XHalo2_MainMenuPath = "";

		static PromOptions()
		{
      if(!File.Exists(SettingsFilename))
      {
        //Set default values for system options
        RegistryKey rk;
        rk = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft Games\\Halo", false);
        string PcInstallPath = (String)rk.GetValue("EXE Path");
        rk.Close();
        HaloPC_BitmapsPath = PcInstallPath + @"\maps\bitmaps.map";
        HaloPC_SoundsPath = PcInstallPath + @"\maps\sounds.map";

        rk = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft Games\\Halo CE", false);
        string CeInstallPath = (String)rk.GetValue("EXE Path");
        rk.Close();
        HaloCE_BitmapsPath = CeInstallPath + @"\maps\bitmaps.map";
        HaloCE_SoundsPath = CeInstallPath + @"\maps\sounds.map";

        //Create the default ini file
        IniConfigSource source = new IniConfigSource ();

        IConfig config = source.AddConfig ("Decompiler");
        config.Set("HaloPC_BitmapsPath", HaloPC_BitmapsPath);
        config.Set("HaloPC_SoundsPath", HaloPC_SoundsPath);
        config.Set("HaloCE_BitmapsPath", HaloCE_BitmapsPath);
        config.Set("HaloCE_SoundsPath", HaloCE_SoundsPath);
        config.Set("XHalo1_UiPath", "");
        config.Set("XHalo2_SharedPath", "");
        config.Set("XHalo2_SinglePlayerSharedPath", "");
        config.Set("XHalo2_MainMenuPath", "");

        //config = source.AddConfig ("Logging");
        //config.Set ("FilePath", "C:\\temp\\MyApp.log");

        source.Save(SettingsFilename);
      }
    }
    static public string GetExtractPath(int ver)
    {
      string extract_path = "";
      MapfileVersion version = (MapfileVersion)ver;

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
          mag_path = Application.StartupPath + @"\Tag Structures\XHalo";
          break;
        case MapfileVersion.XHALO2:
          mag_path = Application.StartupPath + @"\Tag Structures\XHalo2";
          break;
        case MapfileVersion.HALOPC:
          mag_path = Application.StartupPath + @"\Tag Structures\PcHalo";
          break;
        case MapfileVersion.HALOCE:
          mag_path = Application.StartupPath + @"\Tag Structures\CeHalo";
          break;
      }

      return(mag_path);
    }

    static public void GetGuerillaHeader(string TagClass, byte[] HeaderData)
    {
      FileInfo fi = new FileInfo(Application.StartupPath + @"\Tag Headers\" + TagClass + "." + "BLAM");
      FileStream TagHdrStream;
      TagHdrStream = fi.Open(FileMode.Open,FileAccess.Read);
      TagHdrStream.Read(HeaderData,0,64);
      TagHdrStream.Close();
    }
    static public FileStream GetBitmapStream(MapfileVersion ver)
    {
      string bitmapPath = "";
      FileStream BitmapsMapStream;

      switch(ver)
      {
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
        BitmapsMapStream = fiBitmap.Open(FileMode.Open,FileAccess.Read);
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
        fsSound = fiSound.Open(FileMode.Open,FileAccess.Read);
      }
      catch(Exception e)
      {
        throw new PrometheusException("Failed to load Sounds.map: " + e.Message, true);
      }
        
      return(fsSound); 
    }
    static public void SaveProfile()
    {
      IniConfigSource source = new IniConfigSource ();

      IConfig config = source.AddConfig ("Decompiler");
      config.Set("HaloPC_BitmapsPath", HaloPC_BitmapsPath);
      config.Set("HaloPC_SoundsPath", HaloPC_SoundsPath);
      config.Set("HaloCE_BitmapsPath", HaloCE_BitmapsPath);
      config.Set("HaloCE_SoundsPath", HaloCE_SoundsPath);
      config.Set("XHalo1_UiPath", XHalo1_UiPath);
      config.Set("XHalo2_SharedPath", XHalo2_SharedPath);
      config.Set("XHalo2_SinglePlayerSharedPath", XHalo2_SinglePlayerSharedPath);
      config.Set("XHalo2_MainMenuPath", XHalo2_MainMenuPath);
      source.Save(SettingsFilename);
    }
    static public void LoadProfile()
    {
      string filename = Application.StartupPath + @"\Prometheus.ini";

      if(File.Exists(filename))
      {
        IConfigSource source = new IniConfigSource();
        foreach (IConfig config in source.Configs)
        {
          string[] keys = config.GetKeys();
          for (int x=0; x<keys.Length; x++)
          {
            Setting setting = new Setting(config.Name, keys[x]);
            setting.Data = config.GetString(keys[x]);
            Settings.Add(setting);
          }
        }
      }
    }
    public class Setting
    {
      #region Members
      private string _group;
      private string _name;
      private string _data;
      #endregion

      #region Public Properties
      public string Group
      {
        get { return _group; }
      }
      public string Name
      {
        get { return _name; }
      }
      public string Data
      {
        get { return _data; }
        set
        {
          _data = value;
          if (Updated != null) Updated(this, new EventArgs());
        }
      }
      #endregion

      public event EventHandler Updated;

      public Setting(string group, string name)
      {
        _group = group;
        _name = name;
      }
    }

    public class SettingsList : CollectionBase
    {
      
      #region Properties
      public Setting this[string group, string name]
      {
        get
        {
          foreach (Setting setting in List)
          {
            if (setting.Group.ToLower() == group.ToLower())
            {
              if (setting.Name.ToLower() == name.ToLower())
              {
                return setting;
              }
            }
          }
          // The setting doesn't exist - create it, add it to the list, and return it
          Setting s = new Setting(group, name);
          s.Data = "";
          Add(s);
          return s;
        }
      }
      #endregion

      #region Methods
      public void Add(Setting setting)
      {
        List.Add(setting);
        setting.Updated += new EventHandler(setting_Updated);
      }
      public void Remove(Setting setting) { List.Remove(setting); }
      #endregion

      public event EventHandler Dirty;

      private void setting_Updated(object sender, EventArgs e)
      {
        if (Dirty != null) Dirty(this, new EventArgs());
      }
    }
  }
}
