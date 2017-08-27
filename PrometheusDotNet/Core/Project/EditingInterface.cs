using System;
using TagLibrary.Halo1;
using Prometheus.Core.Tags;
using Prometheus.Core.Render;

namespace Prometheus.Core.Project
{
  public enum ObjectType
  {
    Unknown,
    Actors,
    Bipeds,
    Controls,
    Decals,
    Devices,
    Equipment,
    LightFixtures,
    Machines,
    PlayerStart,
    Scenery,
    Vehicle,
    Weapon,
    NetgameEquipment,
    NetgameFlags,
    SoundScenery,
    DOBC
  };

	/// <summary>
	/// Summary description for EditingInterface.
	/// </summary>
	public class EditingInterface
	{
    static private Instance3DCollection mapSpawns;
    static private ISceneGraphInterface editor = null;
    static private Halo1SceneGraphInterface halo1SceneEditor = null;

    public EditingInterface(Instance3DCollection spawns)
		{
      mapSpawns = spawns;
		}
    public void LoadHalo1Scenario(Scenario st, TagBase scenario_data)
    {
      halo1SceneEditor = new Halo1SceneGraphInterface(mapSpawns);
      halo1SceneEditor.LoadScenario(st, scenario_data);
      editor = halo1SceneEditor;
    }
    public void AddToPalette(ObjectType type, string relative_path)
    {
      editor.AddToPalette(type, relative_path);
    }
    public void AddInstance(ObjectType type, string item_path)
    {
      editor.AddInstance(type, item_path);
    }
    public void RemoveInstance(Instance3D Item)
    {
    }
    public void EditInstance(Instance3D Item)
    {
    }
	}
}
