using System;
using System.IO;
using System.Xml;
using System.Diagnostics;
using Prometheus.Core.Tags;
using Prometheus.Core.Render;
using TagLibrary.Halo1;
using Core.Lightmap;
using Prometheus.Core.Compiler;
using DB = Prometheus.Core.Compiler_Gren;

namespace Prometheus.Core.Project
{
	/// <summary>
	/// Summary description for ProjectManager.
	/// </summary>
  public class ProjectManager
  {
    static public Instance3DCollection MapSpawns = new Instance3DCollection();
    static public EditingInterface MapSpawnEditor = new EditingInterface(MapSpawns);
    static private ProjectFile m_ProjFile = null;
    static public ProjectPopups Menus = new ProjectPopups();
    static public TagBase m_ScenarioData = null;
    static public Scenario m_ScenarioTag = null;
    static public TaskList TaskList = new TaskList();
    static private TagFileName[] m_PrefabList = null;
    static public RadiosityLightCollection RadiosityLights = new RadiosityLightCollection();
    static private bool bProjectModified = false;

    //list of open tags

    static public TagFileName[] PrefabList
    {
      get{return m_PrefabList;}
    }
    static public MapfileVersion Version
    {
      get{return m_ScenarioData.Header.GameVersion;}
    }

    static public bool Dirty
    {
      get
      {
        bool bDirty = bProjectModified;
        
        if(m_ProjFile != null)
          bDirty = false;

        return(bDirty);
      }
    }
    static public bool ProjectLoaded
    {
      get{return(m_ScenarioTag != null);}
    }

    static public TagFileName ScenarioTagFileName
    {
      get
      {
        //temporary solution
        return m_ProjFile.Tags["Scenario"].TagFileName;
      }
    }

    static public Scenario ScenarioTag
    {
      get
      {
        return(m_ScenarioTag);
      }
    }

    public static ProjectFile ActiveProject
    {
      get { return ProjectManager.m_ProjFile; }
    }

    public ProjectManager()
		{
		}
    static public void Initialize()
    {
      TaskList.Add(TaskList.TaskType.Error, "Invalid duration", "globals\\vehicle_collision.damage_effect", "Camera Shaking", true);
      TaskList.Add(TaskList.TaskType.Warning, "Test", "globals\\globals.globals", "N/A", true);
    }
    static public void OpenProject(string ProjectFilename)
    {
      //set the Project Path global variable
      int i = ProjectFilename.LastIndexOf('\\');
      
      OptionsManager.ActiveProjectPath = ProjectFilename.Substring(0, i) + "\\";


      XmlDocument doc = new XmlDocument();
      doc.Load(ProjectFilename);
      m_ProjFile = new ProjectFile(doc);
      Trace.WriteLine("Project Loaded: " + m_ProjFile.MapName);

      //Load the scenario tag
      TagFileName scenario_tfn = new TagFileName(m_ProjFile.Tags["Scenario"].Path, MapfileVersion.HALOPC, TagSource.LocalProject);
      
      if(scenario_tfn.Exists)
      {
        LoadScenario(scenario_tfn);
      }
      else
      {
        //todo: freak out!
      }
      //todo: init prefabs?
    }
    static public void SaveProject()
    {
      if(m_ProjFile != null)
      {
        TagFileName scenario_tfn = new TagFileName(m_ProjFile.Tags["Scenario"].Path, ProjectManager.Version, TagSource.LocalProject);

        m_ProjFile.SaveToXML();
        SaveScenario(scenario_tfn);
      }
    }
    static public void CloseProject(bool bSave)
    {
      if(bSave)
        SaveProject();

      Menus.Mode = RenderMenuMode.Disabled;
      MapSpawns.Clear();
      m_ScenarioTag = null;
      m_ScenarioData = null;

      //reset fog

      //clean up resources
    }
    static public void ScanDependencies()
    {
      //save all files
      DB.DependencyBuilder ib = new DB.DependencyBuilder();
      TagFileName stfn = new TagFileName(m_ScenarioData.TagFilename, m_ScenarioData.Header.GameVersion, TagSource.LocalProject);
      ib.ProcessDependants(stfn);

      string[] temp = DB.DependencyBuilder.mapfileTagsIndex.RelativePathList;
      for(int i=0; i<temp.Length; i++)
        Trace.WriteLine("Tag Dependency: " + temp[i]);


      Trace.WriteLine("Broken Dependency List");
      string[] broken = DB.DependencyBuilder.brokenDependencies.RelativePathList;
      string[] broken_parents = DB.DependencyBuilder.brokenDependenciesParents.RelativePathList;
      for(int i=0; i<broken.Length; i++)
      {
        Trace.WriteLine("Broken Dependency: " + broken[i]);
      }

      for(int i=0; i<broken_parents.Length; i++)
      {
        if(broken_parents[i] != null)
          Trace.WriteLine("Broken Parent: " + broken_parents[i]);
        else
          Trace.WriteLine("Broken Parent: none");
      }
      
    }
    static public void BuildMap()
    {
      if(m_ScenarioData != null)
      {
        switch(m_ScenarioData.Header.GameVersion)
        {
          case MapfileVersion.HALOPC:
            HaloPCMAP.Build();
            break;
          case MapfileVersion.XHALO1:
            XBoxHaloMap.Build();
            break;
        }
      }
    }
    static public void LoadScenario(TagFileName tfn)
    {
      if(tfn.Version == MapfileVersion.XHALO2)
      {
      }
      else
      {
        m_ScenarioData = new TagBase();
        m_ScenarioData.LoadTagBuffer(tfn);
        BinaryReader br = new BinaryReader(m_ScenarioData.Stream);

        m_ScenarioTag = new Scenario();
        m_ScenarioTag.Read(br);
        m_ScenarioTag.ReadChildData(br);
        MapSpawnEditor.LoadHalo1Scenario(m_ScenarioTag, m_ScenarioData);
        Menus.UpdateMenuLists(m_ScenarioTag);
      }
    }
    static public void SaveScenario(TagFileName tfn)
    {
      if(tfn.Version == MapfileVersion.XHALO2)
      {
      }
      else
      {
        m_ScenarioData = new TagBase();
        m_ScenarioData.Stream = new MemoryStream();
        BinaryWriter bw = new BinaryWriter(m_ScenarioData.Stream);

        m_ScenarioData.Header.GameVersion = tfn.Version;
        m_ScenarioData.Header.TagClass0 = 0x73636E72;
        m_ScenarioData.Header.TagClass1 = 0xffffffff;
        m_ScenarioData.Header.TagClass2 = 0xffffffff;

        m_ScenarioTag.Write(bw);
        m_ScenarioTag.WriteChildData(bw);
        m_ScenarioData.Header.TagSize = (int)bw.BaseStream.Position;
        m_ScenarioData.SaveTagBuffer(tfn);
      }
    }
    static public string[] GetContextMenuStrings()
    {
      return(Menus.GetMenuList());
    }
    static public ObjectType GetObjectType(string input)
    {
      ObjectType sect = ObjectType.Unknown;

      switch(input)
      {
        case "Light Fixtures":
          sect = ObjectType.LightFixtures;
          break;
        case "Scenery":
          sect = ObjectType.Scenery;
          break;
        case "Vehicles":
          sect = ObjectType.Vehicle;
          break;
        case "Decals":
          sect = ObjectType.Decals;
          break;
        case "Actors":
          sect = ObjectType.Actors;
          break;
        case "Bipeds":
          sect = ObjectType.Bipeds;
          break;
        case "Controls":
          sect = ObjectType.Controls;
          break;
        case "Machines":
          sect = ObjectType.Machines;
          break;
        case "Equipment":
          sect = ObjectType.Equipment;
          break;
        case "Weapons":
          sect = ObjectType.Weapon;
          break;
        case "Sound Scenery":
          sect = ObjectType.SoundScenery;
          break;
        case "Detail Objects":
          sect = ObjectType.DOBC;
          break;
        case "Netgame Flags":
          sect = ObjectType.NetgameFlags;
          break;
      }

      return(sect);
    }
    static public void PerformContextAction(string MenuText, TagFileName browsed_item)
    {
      ObjectType sect = ObjectType.Unknown;
      Menus.UpdateRecentItems(MenuText);
      
      //get action from text before "." delimiter
      int j = MenuText.IndexOf('.');
      string action = MenuText.Substring(0, j);
      string item_path = MenuText.Substring(j+1, MenuText.Length - j - 1);

      sect = GetObjectType(action);


      if(item_path == "Add...")
      {
        string rp = browsed_item.RelativePath;
        int k = rp.IndexOf('.');
        string new_rp = rp.Substring(0, k);
        MapSpawnEditor.AddToPalette(sect, new_rp);
        Menus.UpdateMenuLists(m_ScenarioTag);
      }
      else
      {
        MapSpawnEditor.AddInstance(sect, item_path);
      }
    }
    /// <summary>
    /// Called whenever there is a change to the Scenario Palettes or when
    /// the Scenario type (SP,MP,UI) is changed.
    /// </summary>
    static public void UpdateContextMenus()
    {
      //rebuild the menu lists
      Menus.UpdateMenuLists(m_ScenarioTag);
    }
	}
}
