using System;
using TagLibrary.Halo1;

namespace Prometheus.Core.Project
{
  public enum RenderMenuMode {Disabled, SinglePlayer, Multiplayer, ActiveSelection};
  /// <summary>
  /// Summary description for ProjectPopups.
  /// </summary>  public class ProjectPopups
  public class ProjectPopups  
  {
    public string m_LastInsert1 = "";
    public string m_LastInsert2 = "";
    public string m_LastInsert3 = "";

    public string[] m_ActorsList = null;
    public string[] m_BipedsList = null;
    public string[] m_ControlsList = null;
    public string[] m_DecalsList = null;
    public string[] m_DevicesList = null;
    public string[] m_EquipmentList = null;
    public string[] m_LightFixturesList = null;
    public string[] m_MachinesList = null;
    public string[] m_PlayerStartList = null;
    public string[] m_SceneryList = null;
    public string[] m_VehicleList = null;
    public string[] m_WeaponList = null;
    public string[] m_NetgameEquipmentList = null;
    public string[] m_NetgameFlagsList = null;
    public string[] m_SoundSceneryList = null;
    public string[] m_DOBCList = null;

    private RenderMenuMode m_MenuMode = RenderMenuMode.ActiveSelection;
    public bool m_bChanged = true;

    public bool Changed
    {
      get
      {
        RenderMenuMode current_mode = this.Mode;

        if(current_mode != m_MenuMode)
          m_bChanged = true;
        else
          m_bChanged = false;

        return(m_bChanged);
      }
    }
    public RenderMenuMode Mode
    {
      set
      {
        m_MenuMode = value;
      }
      get
      {
        RenderMenuMode mode = RenderMenuMode.Disabled;

        if(ProjectManager.MapSpawns.ObjectSelected)
        {
          mode = RenderMenuMode.ActiveSelection;
        }
        else if((ProjectManager.ProjectLoaded)&&(ProjectManager.ScenarioTag != null))
        {
          switch(ProjectManager.ScenarioTag.ScenarioValues.Type.Value)
          {
            case 0:
              mode = RenderMenuMode.SinglePlayer;
              break;

            case 1:
              mode = RenderMenuMode.Multiplayer;
              break;

            case 2:
              mode = RenderMenuMode.Disabled;
              break;
          }
        }
        else
        {
          mode = RenderMenuMode.Disabled;
        }

        return(mode);
      }
    }

    public ProjectPopups()
    {
      m_ActorsList = new string[0];
      m_BipedsList = new string[0];
      m_ControlsList = new string[0];
      m_DecalsList = new string[0];
      m_DevicesList = new string[0];
      m_EquipmentList = new string[0];
      m_LightFixturesList = new string[0];
      m_MachinesList = new string[0];
      m_PlayerStartList = new string[0];
      m_SceneryList = new string[0];
      m_VehicleList = new string[0];
      m_WeaponList = new string[0];
      m_NetgameEquipmentList = new string[0];
      m_NetgameFlagsList = new string[0];
      m_SoundSceneryList = new string[0];
      m_DOBCList = new string[0];

      m_NetgameFlagsList = new string[9];
      m_NetgameFlagsList[0] = "Netgame Flags.CTF Flag";
      m_NetgameFlagsList[1] = "Netgame Flags.CTF Vehicle";
      m_NetgameFlagsList[2] = "Netgame Flags.Oddball Ball Spawn";
      m_NetgameFlagsList[3] = "Netgame Flags.Race - Track";
      m_NetgameFlagsList[4] = "Netgame Flags.Race - Vehicle";
      m_NetgameFlagsList[5] = "Netgame Flags.Vegas - Bank";
      m_NetgameFlagsList[6] = "Netgame Flags.Teleport From";
      m_NetgameFlagsList[7] = "Netgame Flags.Teleport To";
      m_NetgameFlagsList[8] = "Netgame Flags.Hill Flag";
    }
    public void UpdateMenuLists(Scenario st)
    {
      int pal;

      m_LightFixturesList = new string[st.ScenarioValues.LightFixturesPalette.Count+1];
      for(pal=0; pal<st.ScenarioValues.LightFixturesPalette.Count; pal++)
        m_LightFixturesList[pal] = BuildMenuItem("Light Fixtures", st.ScenarioValues.LightFixturesPalette[pal].Name.Value);
      m_LightFixturesList[pal] = "Light Fixtures.Add...";

      m_SceneryList = new string[st.ScenarioValues.SceneryPalette.Count+1];
      for(pal=0; pal<st.ScenarioValues.SceneryPalette.Count; pal++)
        m_SceneryList[pal] = BuildMenuItem("Scenery", st.ScenarioValues.SceneryPalette[pal].Name.Value);
      m_SceneryList[pal] = "Scenery.Add...";

      m_VehicleList = new string[st.ScenarioValues.VehiclePalette.Count+1];
      for(pal=0; pal<st.ScenarioValues.VehiclePalette.Count; pal++)
        m_VehicleList[pal] = BuildMenuItem("Vehicles", st.ScenarioValues.VehiclePalette[pal].Name.Value);
      m_VehicleList[pal] = "Vehicles.Add...";

      m_DecalsList = new string[st.ScenarioValues.DecalPalette.Count+1];
      for(pal=0; pal<st.ScenarioValues.DecalPalette.Count; pal++)
        m_DecalsList[pal] = BuildMenuItem("Decals", st.ScenarioValues.DecalPalette[pal].Reference.Value);
      m_DecalsList[pal] = "Decals.Add...";

      m_ActorsList = new string[st.ScenarioValues.ActorPalette.Count+1];
      for(pal=0; pal<st.ScenarioValues.ActorPalette.Count; pal++)
        m_ActorsList[pal] = BuildMenuItem("Actors", st.ScenarioValues.ActorPalette[pal].Reference.Value);
      m_ActorsList[pal] = "Actors.Add...";

      m_BipedsList = new string[st.ScenarioValues.BipedPalette.Count+1];
      for(pal=0; pal<st.ScenarioValues.BipedPalette.Count; pal++)
        m_BipedsList[pal] = BuildMenuItem("Bipeds", st.ScenarioValues.BipedPalette[pal].Name.Value);
      m_BipedsList[pal] = "Bipeds.Add...";

      m_ControlsList = new string[st.ScenarioValues.ControlPalette.Count+1];
      for(pal=0; pal<st.ScenarioValues.ControlPalette.Count; pal++)
        m_ControlsList[pal] = BuildMenuItem("Controls", st.ScenarioValues.ControlPalette[pal].Name.Value);
      m_ControlsList[pal] = "Controls.Add...";

      m_MachinesList = new string[st.ScenarioValues.MachinePalette.Count+1];
      for(pal=0; pal<st.ScenarioValues.MachinePalette.Count; pal++)
        m_MachinesList[pal] = BuildMenuItem("Machines", st.ScenarioValues.MachinePalette[pal].Name.Value);
      m_MachinesList[pal] = "Machines.Add...";

      m_EquipmentList = new string[st.ScenarioValues.EquipmentPalette.Count+1];
      for(pal=0; pal<st.ScenarioValues.EquipmentPalette.Count; pal++)
        m_EquipmentList[pal] = BuildMenuItem("Equipment", st.ScenarioValues.EquipmentPalette[pal].Name.Value);
      m_EquipmentList[pal] = "Equipment.Add...";

      m_WeaponList = new string[st.ScenarioValues.WeaponPalette.Count+1];
      for(pal=0; pal<st.ScenarioValues.WeaponPalette.Count; pal++)
        m_WeaponList[pal] = BuildMenuItem("Weapons", st.ScenarioValues.WeaponPalette[pal].Name.Value);
      m_WeaponList[pal] = "Weapons.Add...";

      m_SoundSceneryList = new string[st.ScenarioValues.SoundSceneryPalette.Count+1];
      for(pal=0; pal<st.ScenarioValues.SoundSceneryPalette.Count; pal++)
        m_SoundSceneryList[pal] = BuildMenuItem("Sound Scenery", st.ScenarioValues.SoundSceneryPalette[pal].Name.Value);
      m_SoundSceneryList[pal] = "Sound Scenery.Add...";

      m_DOBCList = new string[st.ScenarioValues.DetailObjectCollectionPalette.Count+1];
      for(pal=0; pal<st.ScenarioValues.DetailObjectCollectionPalette.Count; pal++)
        m_DOBCList[pal] = BuildMenuItem("Detail Objects", st.ScenarioValues.DetailObjectCollectionPalette[pal].Name.Value);
      m_DOBCList[pal] = "Detail Objects.Add...";

      //Netgame Equipment (item collection) handling
      int SearchIndex = 0;
      string[] temp = new string[st.ScenarioValues.NetgameEquipment.Count];
      for(int i=0; i<st.ScenarioValues.NetgameEquipment.Count; i++)
      {
        bool bFound = false;
        SearchIndex = 0;
        while((bFound == false)&&(temp[SearchIndex] != null))
        {
          if(temp[SearchIndex] == st.ScenarioValues.NetgameEquipment[i].ItemCollection.Value)
          {
            bFound = true;
            break;
          }
          SearchIndex++;
        }
        if(bFound == false)
          temp[SearchIndex] = st.ScenarioValues.NetgameEquipment[i].ItemCollection.Value;
      }

      m_NetgameEquipmentList = new string[SearchIndex+2];
      for(int i=0; i<m_NetgameEquipmentList.Length-1; i++)
        m_NetgameEquipmentList[i] = BuildMenuItem("Netgame Equipment", temp[i]);
      
      m_NetgameEquipmentList[SearchIndex+1] = "Netgame Equipment.Add...";
 
      /*
    public string[] m_DevicesList = null;
    public string[] m_PlayerStartList = null;
    
          PlayerStartingLocations.AddNew();
          TriggerVolumes.AddNew();
          NetgameEquipment.AddNew();
          StartingEquipment.AddNew();
 */

    }
    private string BuildMenuItem(string palette, string tag_path)
    {
      string item_name = null;

      int i = tag_path.LastIndexOf('\\');

      if(i >= 0)
      {
//        item_name = palette + "." + tag_path.Substring(i+1);
      }
      item_name = palette + "." + tag_path;

      return(item_name);
    }
    public string[] GetObjectList(string filter)
    {
      string[] list = null;

      switch(filter)
      {
        case "Devices":
          list = m_DevicesList;
          break;
        case "Machines":
          list = m_MachinesList;
          break;
        case "Controls":
          list = m_ControlsList;
          break;
        case "Light Fixtures":
          list = m_LightFixturesList;
          break;
        case "Device Groups":
          //list = ;
          break;
        case "Equipment":
          list = m_EquipmentList;
          break;
        case "Weapons":
          list = m_WeaponList;
          break;
        case "Units":
          //??? list = m_ActorsList;
          break;
        case "Bipeds":
          list = m_BipedsList;
          break;
        case "Vehicles":
          list = m_VehicleList;
          break;
        case "Scenery":
          list = m_SceneryList;
          break;
        case "Sound Scenery":
          list = m_SoundSceneryList;
          break;
        case "Player Starting Points":
          list = m_PlayerStartList;
          break;
        case "Netgame Flags":
          list = m_NetgameFlagsList;
          break;
        case "Netgame Equipment":
          list = m_NetgameEquipmentList;
          break;
        case "Detail Objects":
          list = m_DOBCList;
          break;
        case "Decals":
          list = m_DecalsList;
          break;
      }

      return(list);
    }
    public string[] GetMenuList()
    {
      string[] list = null;
      int length = 0;
      int accum = 0;

      switch(this.Mode)
      {
        case RenderMenuMode.Disabled:
          list = null;    
          break;

        case RenderMenuMode.ActiveSelection:
          list = new string[6];
          list[0] = "Duplicate Object";
          list[1] = "View Object";
          list[2] = "Edit Tag";
          list[3] = "Snap To Normal";
          list[4] = "Reset Orientation";
          list[5] = "Help";
          break;
        
        case RenderMenuMode.Multiplayer:
          length = 3;
          length += m_DecalsList.Length;
          length += m_NetgameEquipmentList.Length;
          length += m_LightFixturesList.Length;
          length += m_MachinesList.Length;
          length += m_NetgameFlagsList.Length;
          length += m_PlayerStartList.Length;
          length += m_SceneryList.Length;
          length += m_VehicleList.Length;
          length += m_SoundSceneryList.Length;
          length += m_DOBCList.Length;
          
          list = new string[length];

          accum = 0;
          list[accum++] = m_LastInsert1;
          list[accum++] = m_LastInsert2;
          list[accum++] = m_LastInsert3;

          if(m_DecalsList != null){m_DecalsList.CopyTo(list, accum); accum += m_DecalsList.Length;}
          if(m_DevicesList != null){m_DevicesList.CopyTo(list, accum); accum += m_DevicesList.Length;}
          if(m_NetgameEquipmentList != null){m_NetgameEquipmentList.CopyTo(list, accum); accum += m_NetgameEquipmentList.Length;}
          if(m_LightFixturesList != null){m_LightFixturesList.CopyTo(list, accum); accum += m_LightFixturesList.Length;}
          if(m_MachinesList != null){m_MachinesList.CopyTo(list, accum); accum += m_MachinesList.Length;}
          if(m_NetgameFlagsList != null){m_NetgameFlagsList.CopyTo(list, accum); accum += m_NetgameFlagsList.Length;}
          if(m_PlayerStartList != null){m_PlayerStartList.CopyTo(list, accum); accum += m_PlayerStartList.Length;}
          if(m_SceneryList != null){m_SceneryList.CopyTo(list, accum); accum += m_SceneryList.Length;}
          if(m_VehicleList != null){m_VehicleList.CopyTo(list, accum); accum += m_VehicleList.Length;}
          if(m_SoundSceneryList != null){m_SoundSceneryList.CopyTo(list, accum); accum += m_SoundSceneryList.Length;}
          if(m_DOBCList != null){m_DOBCList.CopyTo(list, accum); accum += m_DOBCList.Length;}
          break;
        
        case RenderMenuMode.SinglePlayer:
          length = 0;
          if(m_LastInsert1 != "")length++;
          if(m_LastInsert2 != "")length++;
          if(m_LastInsert3 != "")length++;
          length += m_ActorsList.Length;
          length += m_BipedsList.Length;
          length += m_ControlsList.Length;
          length += m_DecalsList.Length;
          length += m_DevicesList.Length;
          length += m_EquipmentList.Length;
          length += m_LightFixturesList.Length;
          length += m_MachinesList.Length;
          length += m_PlayerStartList.Length;
          length += m_SceneryList.Length;
          length += m_VehicleList.Length;
          length += m_WeaponList.Length;
          length += m_SoundSceneryList.Length;
          length += m_DOBCList.Length;

          list = new string[length];

          accum = 0;
          if(m_LastInsert1 != "")list[accum++] = m_LastInsert1;
          if(m_LastInsert2 != "")list[accum++] = m_LastInsert2;
          if(m_LastInsert3 != "")list[accum++] = m_LastInsert3;

          if(m_ActorsList != null){m_ActorsList.CopyTo(list, accum); accum += m_ActorsList.Length;}
          if(m_BipedsList != null){m_BipedsList.CopyTo(list, accum); accum += m_BipedsList.Length;}
          if(m_ControlsList != null){m_ControlsList.CopyTo(list, accum); accum += m_ControlsList.Length;}
          if(m_DecalsList != null){m_DecalsList.CopyTo(list, accum); accum += m_DecalsList.Length;}
          if(m_DevicesList != null){m_DevicesList.CopyTo(list, accum); accum += m_DevicesList.Length;}
          if(m_EquipmentList != null){m_EquipmentList.CopyTo(list, accum); accum += m_EquipmentList.Length;}
          if(m_LightFixturesList != null){m_LightFixturesList.CopyTo(list, accum); accum += m_LightFixturesList.Length;}
          if(m_MachinesList != null){m_MachinesList.CopyTo(list, accum); accum += m_MachinesList.Length;}
          if(m_PlayerStartList != null){m_PlayerStartList.CopyTo(list, accum); accum += m_PlayerStartList.Length;}
          if(m_SceneryList != null){m_SceneryList.CopyTo(list, accum); accum += m_SceneryList.Length;}
          if(m_VehicleList != null){m_VehicleList.CopyTo(list, accum); accum += m_VehicleList.Length;}
          if(m_WeaponList != null){m_WeaponList.CopyTo(list, accum); accum += m_WeaponList.Length;}
          if(m_SoundSceneryList != null){m_SoundSceneryList.CopyTo(list, accum); accum += m_SoundSceneryList.Length;}
          if(m_DOBCList != null){m_DOBCList.CopyTo(list, accum); accum += m_DOBCList.Length;}
          break;
      }

      return(list);
    }
    public void UpdateRecentItems(string MenuText)
    {
      if((MenuText != m_LastInsert1)&&(MenuText != m_LastInsert2)&&(MenuText != m_LastInsert3))
      {
        int k = MenuText.IndexOf('.');

        if(m_LastInsert1 == "")
        {
          m_LastInsert1 = MenuText;
        }
        else if(m_LastInsert2 == "")
        {
          m_LastInsert2 = m_LastInsert1;
          m_LastInsert1 = MenuText;
        }
        else
        {
          m_LastInsert3 = m_LastInsert2;
          m_LastInsert2 = m_LastInsert1;
          m_LastInsert1 = MenuText;
        }
      }
    }
    public void ClearRecentItems()
    {
      m_LastInsert1 = "";
      m_LastInsert2 = "";
      m_LastInsert3 = "";
    }
  }
}
