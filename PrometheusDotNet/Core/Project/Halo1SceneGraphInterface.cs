using System;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using Prometheus.Core.Render;
using Prometheus.Core.Tags;
using TagLibrary.Halo1;
using TagLibrary.Types;
using Core.Lightmap;
using Microsoft.DirectX;

namespace Prometheus.Core.Project
{
  public class Halo1SceneGraphInterface : ISceneGraphInterface
  {
    private Instance3DCollection instanceCollection = null;
    private Scenario scenarioTag = null;
    private MapfileVersion version = MapfileVersion.HALOPC;

    //todo: some kind of mapping class so we can update the scenario

    public MapfileVersion Version
    {
      set{version = value;}
    }
    public Halo1SceneGraphInterface(Instance3DCollection collection)
    {
      instanceCollection = collection;
    }
    public void LoadScenario(Scenario st, TagBase scenario_data)
    {
      this.version = scenario_data.Header.GameVersion;
      int i;
      instanceCollection.Clear();

      //Process palettes
      scenarioTag = st;
      
      //load bipeds
      for(i=0; i<st.ScenarioValues.Bipeds.Count; i++)
        CreateInstance(st.ScenarioValues.Bipeds[i]);

      //load decals
      //for(i=0; i<st.ScenarioValues.Decals.Count; i++)
      //  CreateInstance(st.ScenarioValues.Decals[i]);

      //load light fixtures
      for(i=0; i<st.ScenarioValues.LightFixtures.Count; i++)
        CreateInstance(st.ScenarioValues.LightFixtures[i]);

      //load scenery
      for(i=0; i<st.ScenarioValues.Scenery.Count; i++)
        CreateInstance(st.ScenarioValues.Scenery[i]);
      
      //load vehicles
      for(i=0; i<st.ScenarioValues.Vehicles.Count; i++)
        CreateInstance(st.ScenarioValues.Vehicles[i]);
      
      //load netgame equipment
      for(i=0; i<st.ScenarioValues.NetgameEquipment.Count; i++)
        CreateInstance(st.ScenarioValues.NetgameEquipment[i]);

      //load sound scenery
      for(i=0; i<st.ScenarioValues.SoundScenery.Count; i++)
        CreateInstance(st.ScenarioValues.SoundScenery[i]);

      //load player spawns
      for(i=0; i<st.ScenarioValues.PlayerStartingLocations.Count; i++)
        CreateInstance(st.ScenarioValues.PlayerStartingLocations[i]);

      //load netgame flags
      for(i=0; i<st.ScenarioValues.NetgameFlags.Count; i++)
        CreateInstance(st.ScenarioValues.NetgameFlags[i]);


      if(st.ScenarioValues.StructureBsps.Count >= 1)
      {
        //get BSP filename
        TagFileName bsp_filename = new TagFileName(st.ScenarioValues.StructureBsps[0].StructureBsp.Value,
          "sbsp", scenario_data.Header.GameVersion);

        //get Sky filename
        TagFileName sky_filename = new TagFileName(st.ScenarioValues.Skies[0].Sky.Value, "sky ", scenario_data.Header.GameVersion);
        TagBase sky_data = new TagBase();
        sky_data.LoadTagBuffer(sky_filename);
        BinaryReader sky_br = new BinaryReader(sky_data.Stream);
        Sky sky_tag = new Sky();
        sky_tag.Read(sky_br);
        sky_tag.ReadChildData(sky_br);

        MdxRender.RGB fog = new MdxRender.RGB();
        fog.R = sky_tag.SkyValues.OutdoorFogColor.R;
        fog.G = sky_tag.SkyValues.OutdoorFogColor.G;
        fog.B = sky_tag.SkyValues.OutdoorFogColor.B;
        MdxRender.FogColor = fog;

        /// Not sure how these units equate to d3d units - * 2.5 seems to be pretty good.
        MdxRender.FogStart = sky_tag.SkyValues.OutdoorFogStartDistance.Value * 2.5f;
        MdxRender.FogEnd = sky_tag.SkyValues.OutdoorFogOpaqueDistance.Value * 2.5f;
        MdxRender.FogDensity = sky_tag.SkyValues.OutdoorFogMaximumDensity.Value;

        MdxRender.ClearColor = Color.FromArgb(
          (int)(sky_tag.SkyValues.OutdoorAmbientRadiosityColor.R * 255),
          (int)(sky_tag.SkyValues.OutdoorAmbientRadiosityColor.G * 255),
          (int)(sky_tag.SkyValues.OutdoorAmbientRadiosityColor.B * 255));

        MdxRender.FogEnabled = true;

        TagFileName sky_model_tfn;
          
        if(this.version == MapfileVersion.XHALO1)
          sky_model_tfn = new TagFileName(sky_tag.SkyValues.Model.Value, "mode", scenario_data.Header.GameVersion);
        else
          sky_model_tfn = new TagFileName(sky_tag.SkyValues.Model.Value, "mod2", scenario_data.Header.GameVersion);

        MdxRender.LoadBsp(bsp_filename, sky_model_tfn);

        //Load up them thar lights for Radiosity
        ProjectManager.RadiosityLights.Clear();
        //sky_tag
        for(int l=0; l<sky_tag.SkyValues.Lights.Count; l++)
        {
          RadiosityLight rl = new RadiosityLight();
          rl.color[0] = sky_tag.SkyValues.Lights[l].Color.R;
          rl.color[1] = sky_tag.SkyValues.Lights[l].Color.G;
          rl.color[2] = sky_tag.SkyValues.Lights[l].Color.B;
          rl.power =  sky_tag.SkyValues.Lights[l].Power.Value;

          //don't know how this is turned into a vector, but we'll figure it out
          //for now get x,y from yaw and z component from pitch (make sure Z component is negative)
          rl.direction.Z = -1;
          double y,p;
          y = sky_tag.SkyValues.Lights[l].Direction.Y;
          p = sky_tag.SkyValues.Lights[l].Direction.P;
          rl.direction.X = -(float)Math.Cos(y);
          rl.direction.Y = -(float)Math.Sin(y);
          rl.direction.Z = -(float)Math.Sin(p);
          ProjectManager.RadiosityLights.Add(rl);
          MdxRender.AddGlobalLight(0, rl);
        }
      }
      
      instanceCollection.UpdateObjectColors();
    }
    /*
ScenarioEquipmentBlock
ScenarioWeaponBlock
ScenarioMachineBlock
ScenarioControlBlock
ScenarioProfilesBlock
ScenarioPlayersBlock
ScenarioTriggerVolumeBlock
ScenarioStartingEquipmentBlock
ScenarioBspSwitchTriggerVolumeBlock
*/
    
    private void CreateInstance(Scenario.ScenarioLightFixtureBlock block)
    {
      Instance3D scenario_object = null;
      string obj_name = scenarioTag.ScenarioValues.LightFixturesPalette[block.Type.Value].Name.Value;
      scenario_object = new Instance3D(MdxRender.MM.LightModel);

      scenario_object.InitializeRotationBinding(block.Rotation);
      scenario_object.InitializeTranslationBinding(block.Position);
      scenario_object.ObjectType = ObjectType.LightFixtures;
      scenario_object.ObjectName = obj_name;
      instanceCollection.Add(scenario_object);
    }
    private void CreateInstance(Scenario.ScenarioBipedBlock block)
    {
      Instance3D scenario_object = null;
      string obj_name = scenarioTag.ScenarioValues.BipedPalette[block.Type.Value].Name.Value;
      TagFileName model_name = GetModelName(new TagFileName(obj_name, "bipd", version));

      if(model_name.Exists)
        scenario_object = new Instance3D(model_name);
      else
        scenario_object = new Instance3D(MdxRender.MM.DefaultModel);

      scenario_object.InitializeRotationBinding(block.Rotation);
      scenario_object.InitializeTranslationBinding(block.Position);
      scenario_object.ObjectType = ObjectType.Bipeds;
      scenario_object.ObjectName = obj_name;
      instanceCollection.Add(scenario_object);
    }
    private void CreateInstance(Scenario.ScenarioDecalsBlock block)
    {
      Instance3D scenario_object = null;
      string obj_name = scenarioTag.ScenarioValues.DecalPalette[block.DecalType.Value].Reference.Value;

      TagFileName bitmap_tfn = GetDecalBitmap(new TagFileName(obj_name, "deca", this.version));
      PromDecal pd = new PromDecal(1, bitmap_tfn);
      scenario_object = new Instance3D(pd);
      //scenario_object.InitializeRotationBinding(block.Position);
      //todo: bind the byte rotation that these objects use
      scenario_object.InitializeTranslationBinding(block.Position);
      scenario_object.InitializeRotationBinding(block.Ya, block.Pitc);
      scenario_object.ObjectType = ObjectType.Decals;
      scenario_object.ObjectName = obj_name;
      instanceCollection.Add(scenario_object);
    }
    private void CreateInstance(Scenario.ScenarioNetgameFlagsBlock block)
    {
      string obj_name = null;
      Instance3D scenario_object = null;
      //todo: get name of flag
      switch(block.Type.Value)
      {
        case 0:
          scenario_object = new Instance3D(BillboardModel.CTF_Flag);
          obj_name = "CTF Flag";
          break;
        case 1:
          scenario_object = new Instance3D(BillboardModel.CTF_Vehicle);
          obj_name = "CTF Vehicle";
          break;
        case 2:
          scenario_object = new Instance3D(BillboardModel.Oddball);
          obj_name = "Oddball Ball Spawn";
          break;
        case 3:
          scenario_object = new Instance3D(BillboardModel.Race_Track);
          obj_name = "Race - Track";
          break;
        case 4:
          scenario_object = new Instance3D(BillboardModel.Race_Vehicle);
          obj_name = "Race - Vehicle";
          break;
        case 5:
          scenario_object = new Instance3D(BillboardModel.Vegas_Bank);
          obj_name = "Vegas - Bank";
          break;
        case 6:
          scenario_object = new Instance3D(BillboardModel.Teleport_From);
          obj_name = "Teleport From";
          break;
        case 7:
          scenario_object = new Instance3D(BillboardModel.Teleport_To);
          obj_name = "Teleport To";
          break;
        case 8:
          scenario_object = new Instance3D(BillboardModel.KOH);
          obj_name = "Hill Flag";
          break;
        default:
          throw new Exception("Invalid netgame flag type instantiated.");
          break;
      }

      scenario_object.ObjectColor = Color.Green;
      scenario_object.InitializeRotationBinding(block.Facing);
      scenario_object.InitializeTranslationBinding(block.Position);
      scenario_object.ObjectType = ObjectType.NetgameFlags;
      scenario_object.ObjectName = obj_name;
      instanceCollection.Add(scenario_object);
    }
    private void CreateInstance(Scenario.ScenarioPlayersBlock block)
    {
      Instance3D scenario_object = null;
      //todo: get name of spawn block

      scenario_object = new Instance3D(MdxRender.MM.PlayerSpawnModel);
      scenario_object.InitializeRotationBinding(block.Facing);
      scenario_object.InitializeTranslationBinding(block.Position);
      scenario_object.ObjectType = ObjectType.PlayerStart;
      if(block.TeamIndex.Value == 0)
        scenario_object.ObjectColor = Color.Red;
      else
        scenario_object.ObjectColor = Color.Blue;

      scenario_object.PlayerSpawn = true;
      scenario_object.ObjectName = "";
      instanceCollection.Add(scenario_object);
    }
    private void CreateInstance(Scenario.ScenarioSoundSceneryBlock block)
    {
      Instance3D scenario_object = null;
      string obj_name = scenarioTag.ScenarioValues.SoundSceneryPalette[block.Type.Value].Name.Value;

      scenario_object = new Instance3D(BillboardModel.Sound_Scenery);
      scenario_object.ObjectColor = Color.Green;
      scenario_object.InitializeRotationBinding(block.Rotation);
      scenario_object.InitializeTranslationBinding(block.Position);
      scenario_object.ObjectType = ObjectType.SoundScenery;
      scenario_object.ObjectName = obj_name;
      instanceCollection.Add(scenario_object);
    }
    private void CreateInstance(Scenario.ScenarioNetgameEquipmentBlock block)
    {
      Instance3D scenario_object = null;
      string obj_name = block.ItemCollection.Value;
      TagFileName model_name = GetModelName(new TagFileName(obj_name, "itmc", version));

      if(model_name.Exists)
        scenario_object = new Instance3D(model_name);
      else
        scenario_object = new Instance3D(MdxRender.MM.DefaultModel);

      scenario_object.InitializeRotationBinding(block.Facing);
      scenario_object.InitializeTranslationBinding(block.Position);
      scenario_object.ObjectType = ObjectType.Equipment;
      scenario_object.ObjectName = obj_name;
      instanceCollection.Add(scenario_object);
    }
    private void CreateInstance(Scenario.ScenarioVehicleBlock block)
    {
      Instance3D scenario_object = null;
      string obj_name = scenarioTag.ScenarioValues.VehiclePalette[block.Type.Value].Name.Value;
      TagFileName model_name = GetModelName(new TagFileName(obj_name, "vehi", version));

      if(model_name.Exists)
        scenario_object = new Instance3D(model_name);
      else
        scenario_object = new Instance3D(MdxRender.MM.DefaultModel);

      scenario_object.InitializeRotationBinding(block.Rotation);
      scenario_object.InitializeTranslationBinding(block.Position);
      scenario_object.ObjectType = ObjectType.Vehicle;
      scenario_object.ObjectName = obj_name;
      instanceCollection.Add(scenario_object);
    }
    private void CreateInstance(Scenario.ScenarioSceneryBlock block)
    {
      Instance3D scenario_object = null;
      string obj_name = scenarioTag.ScenarioValues.SceneryPalette[block.Type.Value].Name.Value;
      TagFileName model_name = GetModelName(new TagFileName(obj_name, "scen", version));

      if(model_name.Exists)
        scenario_object = new Instance3D(model_name);
      else
        scenario_object = new Instance3D(MdxRender.MM.DefaultModel);

      block.DesiredPermutation.Value = 0;
      block.Name.Value = -1;
      scenario_object.InitializeRotationBinding(block.Rotation);
      scenario_object.InitializeTranslationBinding(block.Position);
      scenario_object.ObjectType = ObjectType.Scenery;
      scenario_object.ObjectName = obj_name;
      instanceCollection.Add(scenario_object);
    }
    private TagFileName GetModelName(TagFileName tfn)
    {
      TagFileName model_name = null;
      string model_type;

      if(tfn.Version == MapfileVersion.XHALO1)
        model_type = "mode";
      else
        model_type = "mod2";

      if(tfn.Exists)
      {
        TagBase tagdata = new TagBase();
        tagdata.LoadTagBuffer(tfn);
        BinaryReader br = new BinaryReader(tagdata.Stream);

        switch(tfn.TagClass)
        {
          case "scen":
          {
            Scenery obj = new Scenery();
            obj.Read(br);
            obj.ReadChildData(br);
            model_name = new TagFileName(obj.ObjectValues.Model.Value, model_type, tfn.Version);
          }
            break;

          case "vehi":
          {
            Vehicle obj = new Vehicle();
            obj.Read(br);
            obj.ReadChildData(br);
            model_name = new TagFileName(obj.ObjectValues.Model.Value, model_type, tfn.Version);
          }
            break;
        
          case "weap":
          {
            Weapon obj = new Weapon();
            obj.Read(br);
            obj.ReadChildData(br);
            model_name = new TagFileName(obj.ObjectValues.Model.Value, model_type, tfn.Version);
          }
            break;

          case "itmc":
          {
            ItemCollection obj = new ItemCollection();
            obj.Read(br);
            obj.ReadChildData(br);

            if(obj.ItemCollectionValues.ItemPermutations.Count > 0)
            {
              string tagclass = obj.ItemCollectionValues.ItemPermutations[0].Item.TagGroup;
              TagFileName itmc_tfn = new TagFileName(obj.ItemCollectionValues.ItemPermutations[0].Item.Value, tagclass, tfn.Version);
              model_name = GetModelName(itmc_tfn);
            }
            int i=0;
            //model_name = new TagFileName(obj.Item_collectionValues.Value, "mod2", tfn.Version);
          }
            break;

          case "eqip":
          {
            Equipment obj = new Equipment();
            obj.Read(br);
            obj.ReadChildData(br);
            model_name = new TagFileName(obj.ObjectValues.Model.Value, model_type, tfn.Version);
          }
            break;
        }

        Trace.WriteLine("model_name = " + model_name.RelativePath);
      }

      return(model_name);
    }

    private TagFileName GetDecalBitmap(TagFileName tfn)
    {
      TagFileName bitmap_name = null;

      if(tfn.Exists)
      {
        TagBase tagdata = new TagBase();
        tagdata.LoadTagBuffer(tfn);
        BinaryReader br = new BinaryReader(tagdata.Stream);

        Decal obj = new Decal();
        obj.Read(br);
        obj.ReadChildData(br);
        bitmap_name = new TagFileName(obj.DecalValues.Map.Value, "bitm", tfn.Version);
        //model_name = new TagFileName(obj.ObjectValues.Model.Value, "mod2", tfn.Version);

      }
      return(bitmap_name);
    }
    public override void AddToPalette(ObjectType type, string relative_path)
    {
      int i;

      switch(type)
      {
        case ObjectType.Actors:
          scenarioTag.ScenarioValues.ActorPalette.AddNew();
          i = scenarioTag.ScenarioValues.ActorPalette.Count - 1;
          scenarioTag.ScenarioValues.ActorPalette[i].Reference.Value = relative_path;
          scenarioTag.ScenarioValues.ActorPalette[i].Reference.TagGroup = "actr";
          break;

        case ObjectType.Bipeds:
          scenarioTag.ScenarioValues.BipedPalette.AddNew();
          i = scenarioTag.ScenarioValues.BipedPalette.Count - 1;
          scenarioTag.ScenarioValues.BipedPalette[i].Name.Value = relative_path;
          scenarioTag.ScenarioValues.BipedPalette[i].Name.TagGroup = "bipd";
          break;

        case ObjectType.Controls:
          scenarioTag.ScenarioValues.ControlPalette.AddNew();
          i = scenarioTag.ScenarioValues.ControlPalette.Count - 1;
          scenarioTag.ScenarioValues.ControlPalette[i].Name.Value = relative_path;
          scenarioTag.ScenarioValues.ControlPalette[i].Name.TagGroup = "";
          break;

        case ObjectType.Decals:
          scenarioTag.ScenarioValues.DecalPalette.AddNew();
          i = scenarioTag.ScenarioValues.DecalPalette.Count - 1;
          scenarioTag.ScenarioValues.DecalPalette[i].Reference.Value = relative_path;
          scenarioTag.ScenarioValues.DecalPalette[i].Reference.TagGroup = "";
          break;

        case ObjectType.Devices:
          //scenarioTag.ScenarioValues.DeviceGroups.AddNew();
          //i = scenarioTag.ScenarioValues.DeviceGroups.Count - 1;
          //scenarioTag.ScenarioValues.DeviceGroups[i].Name.Value = relative_path;
          //scenarioTag.ScenarioValues.DeviceGroups[i].Reference.TagGroup = "";
          break;

        case ObjectType.Equipment:
          scenarioTag.ScenarioValues.EquipmentPalette.AddNew();
          i = scenarioTag.ScenarioValues.EquipmentPalette.Count - 1;
          scenarioTag.ScenarioValues.EquipmentPalette[i].Name.Value = relative_path;
          scenarioTag.ScenarioValues.EquipmentPalette[i].Name.TagGroup = "equip";
          break;

        case ObjectType.LightFixtures:
          scenarioTag.ScenarioValues.LightFixturesPalette.AddNew();
          i = scenarioTag.ScenarioValues.LightFixturesPalette.Count - 1;
          scenarioTag.ScenarioValues.LightFixturesPalette[i].Name.Value = relative_path;
          scenarioTag.ScenarioValues.LightFixturesPalette[i].Name.TagGroup = "";
          break;

        case ObjectType.Machines:
          scenarioTag.ScenarioValues.MachinePalette.AddNew();
          i = scenarioTag.ScenarioValues.MachinePalette.Count - 1;
          scenarioTag.ScenarioValues.MachinePalette[i].Name.Value = relative_path;
          scenarioTag.ScenarioValues.MachinePalette[i].Name.TagGroup = "mach";
          break;

        case ObjectType.Scenery:
          scenarioTag.ScenarioValues.SceneryPalette.AddNew();
          i = scenarioTag.ScenarioValues.SceneryPalette.Count - 1;
          scenarioTag.ScenarioValues.SceneryPalette[i].Name.Value = relative_path;
          scenarioTag.ScenarioValues.SceneryPalette[i].Name.TagGroup = "scen";
          break;

        case ObjectType.Vehicle:
          scenarioTag.ScenarioValues.VehiclePalette.AddNew();
          i = scenarioTag.ScenarioValues.VehiclePalette.Count - 1;
          scenarioTag.ScenarioValues.VehiclePalette[i].Name.Value = relative_path;
          scenarioTag.ScenarioValues.VehiclePalette[i].Name.TagGroup = "vehi";
          break;

        case ObjectType.Weapon:
          scenarioTag.ScenarioValues.WeaponPalette.AddNew();
          i = scenarioTag.ScenarioValues.WeaponPalette.Count - 1;
          scenarioTag.ScenarioValues.WeaponPalette[i].Name.Value = relative_path;
          scenarioTag.ScenarioValues.WeaponPalette[i].Name.TagGroup = "weap";
          break;

        case ObjectType.SoundScenery:
          scenarioTag.ScenarioValues.SoundSceneryPalette.AddNew();
          i = scenarioTag.ScenarioValues.SoundSceneryPalette.Count - 1;
          scenarioTag.ScenarioValues.SoundSceneryPalette[i].Name.Value = relative_path;
          scenarioTag.ScenarioValues.SoundSceneryPalette[i].Name.TagGroup = "";
          break;

        case ObjectType.DOBC:
          //scenarioTag.ScenarioValues.DetailObjectCollectionPalette.Add(ScenarioDetailObjectCollectionPaletteBlock block)
          break;
      }
    }
    public override void AddInstance(ObjectType type, string item_path)
    {
      //todo:  might need to use string instead of palette index because of the problem posed
      //  by netgame equipment (itmc) spawns
      int i = -1;
      short pal = -1;
      float x = MdxRender.Camera.NewSpawnLocation.X;
      float y = MdxRender.Camera.NewSpawnLocation.Y;
      float z = MdxRender.Camera.NewSpawnLocation.Z;

      //public void Add(ScenarioPlayersBlock block)
      //public void Add(ScenarioTriggerVolumeBlock block)
      //public void Add(ScenarioStartingEquipmentBlock block)
      //public void Add(ScenarioBspSwitchTriggerVolumeBlock block)
      //public void Add(ScenarioStructureBspsBlock block)
      //public void Add(ScenarioPlayersBlock block)
      
      switch(type)
      {
        case ObjectType.Actors:
          for(pal=0; pal<scenarioTag.ScenarioValues.ActorPalette.Count; pal++)
            if(item_path == scenarioTag.ScenarioValues.ActorPalette[pal].Reference.Value)break;
          //scenarioTag.ScenarioValues.ActorPalette.
          break;
        case ObjectType.Bipeds:
          for(pal=0; pal<scenarioTag.ScenarioValues.BipedPalette.Count; pal++)
            if(item_path == scenarioTag.ScenarioValues.BipedPalette[pal].Name.Value)break;
          scenarioTag.ScenarioValues.Bipeds.AddNew();
          i = scenarioTag.ScenarioValues.Bipeds.Count - 1;
          //scenarioTag.ScenarioValues.Bipeds[i]
          break;
        case ObjectType.Controls:
          for(pal=0; pal<scenarioTag.ScenarioValues.ControlPalette.Count; pal++)
            if(item_path == scenarioTag.ScenarioValues.ControlPalette[pal].Name.Value)break;
          //scenarioTag.ScenarioValues.ControlPalette.Add(ScenarioControlBlock block)
          break;
        case ObjectType.Decals:
          for(pal=0; pal<scenarioTag.ScenarioValues.DecalPalette.Count; pal++)
            if(item_path == scenarioTag.ScenarioValues.DecalPalette[pal].Reference.Value)break;
          //scenarioTag.ScenarioValues.Decals.Add(ScenarioDecalsBlock block)
          break;
        case ObjectType.Devices:
          //scenarioTag.ScenarioValues.DeviceGroups.
          break;
        case ObjectType.Equipment:
          for(pal=0; pal<scenarioTag.ScenarioValues.EquipmentPalette.Count; pal++)
            if(item_path == scenarioTag.ScenarioValues.EquipmentPalette[pal].Name.Value)break;
          //scenarioTag.ScenarioValues.Equipment.Add(ScenarioEquipmentBlock block)
          break;

        case ObjectType.LightFixtures:
          for(pal=0; pal<scenarioTag.ScenarioValues.LightFixturesPalette.Count; pal++)
            if(item_path == scenarioTag.ScenarioValues.LightFixturesPalette[pal].Name.Value)break;
          //ScenarioLightFixturePaletteBlock block = new ScenarioLightFixturePaletteBlock();
          //scenarioTag.ScenarioValues.LightFixtures.Add(ScenarioLightFixtureBlock block)
          break;

        case ObjectType.Machines:
          for(pal=0; pal<scenarioTag.ScenarioValues.MachinePalette.Count; pal++)
            if(item_path == scenarioTag.ScenarioValues.MachinePalette[pal].Name.Value)break;
          //scenarioTag.ScenarioValues.Machines.Add(ScenarioMachineBlock block)
          break;

        case ObjectType.PlayerStart:
          scenarioTag.ScenarioValues.PlayerStartingLocations.AddNew();
          i = scenarioTag.ScenarioValues.PlayerStartingLocations.Count - 1;
          scenarioTag.ScenarioValues.PlayerStartingLocations[i].Position.X = x;
          scenarioTag.ScenarioValues.PlayerStartingLocations[i].Position.Y = y;
          scenarioTag.ScenarioValues.PlayerStartingLocations[i].Position.Z = z;
          scenarioTag.ScenarioValues.PlayerStartingLocations[i].Facing.Value = 0;
          CreateInstance(scenarioTag.ScenarioValues.PlayerStartingLocations[i]);
          break;

        case ObjectType.Scenery:
          for(pal=0; pal<scenarioTag.ScenarioValues.SceneryPalette.Count; pal++)
            if(item_path == scenarioTag.ScenarioValues.SceneryPalette[pal].Name.Value)break;

          scenarioTag.ScenarioValues.Scenery.AddNew();
          i = scenarioTag.ScenarioValues.Scenery.Count - 1;
          scenarioTag.ScenarioValues.Scenery[i].Position.X = x;
          scenarioTag.ScenarioValues.Scenery[i].Position.Y = y;
          scenarioTag.ScenarioValues.Scenery[i].Position.Z = z;
          scenarioTag.ScenarioValues.Scenery[i].Rotation.R = 0;
          scenarioTag.ScenarioValues.Scenery[i].Rotation.P = 0;
          scenarioTag.ScenarioValues.Scenery[i].Rotation.Y = 0;
          scenarioTag.ScenarioValues.Scenery[i].Type.Value = pal;
          CreateInstance(scenarioTag.ScenarioValues.Scenery[i]);
          break;

        case ObjectType.Vehicle:
          for(pal=0; pal<scenarioTag.ScenarioValues.VehiclePalette.Count; pal++)
            if(item_path == scenarioTag.ScenarioValues.VehiclePalette[pal].Name.Value)break;

          scenarioTag.ScenarioValues.Vehicles.AddNew();
          i = scenarioTag.ScenarioValues.Vehicles.Count - 1;
          scenarioTag.ScenarioValues.Vehicles[i].Position.X = x;
          scenarioTag.ScenarioValues.Vehicles[i].Position.Y = y;
          scenarioTag.ScenarioValues.Vehicles[i].Position.Z = z;
          scenarioTag.ScenarioValues.Vehicles[i].Rotation.R = 0;
          scenarioTag.ScenarioValues.Vehicles[i].Rotation.P = 0;
          scenarioTag.ScenarioValues.Vehicles[i].Rotation.Y = 0;
          scenarioTag.ScenarioValues.Vehicles[i].Type.Value = pal;
          CreateInstance(scenarioTag.ScenarioValues.Vehicles[i]);
          break;

        case ObjectType.Weapon:
          for(pal=0; pal<scenarioTag.ScenarioValues.WeaponPalette.Count; pal++)
            if(item_path == scenarioTag.ScenarioValues.WeaponPalette[pal].Name.Value)break;
          //scenarioTag.ScenarioValues.Weapons.Add(ScenarioWeaponBlock block)
          break;

        case ObjectType.NetgameEquipment:
          scenarioTag.ScenarioValues.NetgameEquipment.AddNew();
          i = scenarioTag.ScenarioValues.NetgameEquipment.Count - 1;
          scenarioTag.ScenarioValues.NetgameEquipment[i].Position.X = x;
          scenarioTag.ScenarioValues.NetgameEquipment[i].Position.Y = y;
          scenarioTag.ScenarioValues.NetgameEquipment[i].Position.Z = z;
          scenarioTag.ScenarioValues.NetgameEquipment[i].Facing.Value = 0;
          scenarioTag.ScenarioValues.NetgameEquipment[i].ItemCollection.Value = item_path;
          CreateInstance(scenarioTag.ScenarioValues.NetgameEquipment[i]);
          break;

        case ObjectType.NetgameFlags:
        {
          scenarioTag.ScenarioValues.NetgameFlags.AddNew();
          i = scenarioTag.ScenarioValues.NetgameFlags.Count - 1;
          scenarioTag.ScenarioValues.NetgameFlags[i].Position.X = x;
          scenarioTag.ScenarioValues.NetgameFlags[i].Position.Y = y;
          scenarioTag.ScenarioValues.NetgameFlags[i].Position.Z = z;
          switch(item_path)
          {
            case "CTF Flag":
              scenarioTag.ScenarioValues.NetgameFlags[i].Type.Value = 0;
              break;
            case "CTF Vehicle":
              scenarioTag.ScenarioValues.NetgameFlags[i].Type.Value = 1;
              break;
            case "Oddball Ball Spawn":
              scenarioTag.ScenarioValues.NetgameFlags[i].Type.Value = 2;
              break;
            case "Race - Track":
              scenarioTag.ScenarioValues.NetgameFlags[i].Type.Value = 3;
              break;
            case "Race - Vehicle":
              scenarioTag.ScenarioValues.NetgameFlags[i].Type.Value = 4;
              break;
            case "Vegas - Bank":
              scenarioTag.ScenarioValues.NetgameFlags[i].Type.Value = 5;
              break;
            case "Teleport From":
              scenarioTag.ScenarioValues.NetgameFlags[i].Type.Value = 6;
              break;
            case "Teleport To":
              scenarioTag.ScenarioValues.NetgameFlags[i].Type.Value = 7;
              break;
            case "Hill Flag":
              scenarioTag.ScenarioValues.NetgameFlags[i].Type.Value = 8;
              break;
            default:
              throw new Exception("could not create netgame flag");
              break;
          }
          CreateInstance(scenarioTag.ScenarioValues.NetgameFlags[i]);
        }
          break;

        case ObjectType.SoundScenery:
          for(pal=0; pal<scenarioTag.ScenarioValues.SoundSceneryPalette.Count; pal++)
            if(item_path == scenarioTag.ScenarioValues.SoundSceneryPalette[pal].Name.Value)break;
          //scenarioTag.ScenarioValues.SoundScenery.Add(ScenarioSoundSceneryBlock block)
          break;
        case ObjectType.DOBC:
          for(pal=0; pal<scenarioTag.ScenarioValues.DetailObjectCollectionPalette.Count; pal++)
            if(item_path == scenarioTag.ScenarioValues.DetailObjectCollectionPalette[pal].Name.Value)break;
          //scenarioTag.ScenarioValues.DetailObjectCollections.
          break;
      }
    }
    public override void RemoveInstance(Instance3D Item)
    {
      //remove the instance

      //decrement the indices of the following items in the same section
    }
    public override void EditInstance(Instance3D Item)
    {
      //update the properties of the respective 
    }
  }
}
