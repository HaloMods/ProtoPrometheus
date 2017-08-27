using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Scenario : IBlock
  {
    public ScenarioBlock ScenarioValues = new ScenarioBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'Scenario'------------------------------------------------------");
      ScenarioValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      ScenarioValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      ScenarioValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      ScenarioValues.WriteChildData(writer);
    }
public class ScenarioBlock : IBlock
{
private TagReference _dONTUSE = new TagReference();
private TagReference _wONTUSE = new TagReference();
private TagReference _cANTUSE = new TagReference();
private Block _skies = new Block();
private Enum _type = new Enum();
private Flags  _flags;	
private Block _childScenarios = new Block();
private Angle _localNorth = new Angle();
private Pad  __unnamed;	
private Pad  __unnamed2;	
private Block _predictedResources = new Block();
private Block _functions = new Block();
private Data _editorScenarioData = new Data();
private Block _comments = new Block();
private Pad  __unnamed3;	
private Block _objectNames = new Block();
private Block _scenery = new Block();
private Block _sceneryPalette = new Block();
private Block _bipeds = new Block();
private Block _bipedPalette = new Block();
private Block _vehicles = new Block();
private Block _vehiclePalette = new Block();
private Block _equipment = new Block();
private Block _equipmentPalette = new Block();
private Block _weapons = new Block();
private Block _weaponPalette = new Block();
private Block _deviceGroups = new Block();
private Block _machines = new Block();
private Block _machinePalette = new Block();
private Block _controls = new Block();
private Block _controlPalette = new Block();
private Block _lightFixtures = new Block();
private Block _lightFixturesPalette = new Block();
private Block _soundScenery = new Block();
private Block _soundSceneryPalette = new Block();
private Pad  __unnamed4;	
private Block _playerStartingProfile = new Block();
private Block _playerStartingLocations = new Block();
private Block _triggerVolumes = new Block();
private Block _recordedAnimations = new Block();
private Block _netgameFlags = new Block();
private Block _netgameEquipment = new Block();
private Block _startingEquipment = new Block();
private Block _bspSwitchTriggerVolumes = new Block();
private Block _decals = new Block();
private Block _decalPalette = new Block();
private Block _detailObjectCollectionPalette = new Block();
private Pad  __unnamed5;	
private Block _actorPalette = new Block();
private Block _encounters = new Block();
private Block _commandLists = new Block();
private Block _aiAnimationReferences = new Block();
private Block _aiScriptReferences = new Block();
private Block _aiRecordingReferences = new Block();
private Block _aiConversations = new Block();
private Data _scriptSyntaxData = new Data();
private Data _scriptStringData = new Data();
private Block _scripts = new Block();
private Block _globals = new Block();
private Block _references = new Block();
private Block _sourceFiles = new Block();
private Pad  __unnamed6;	
private Block _cutsceneFlags = new Block();
private Block _cutsceneCameraPoints = new Block();
private Block _cutsceneTitles = new Block();
private Pad  __unnamed7;	
private TagReference _customObjectNames = new TagReference();
private TagReference _ingameHelpText = new TagReference();
private TagReference _hudMessages = new TagReference();
private Block _structureBsps = new Block();
public class ScenarioSkyReferenceBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioSkyReferenceBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioSkyReferenceBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioSkyReferenceBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioSkyReferenceBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioSkyReferenceBlock); }
  }
}
private ScenarioSkyReferenceBlockCollection _skiesCollection;
public ScenarioSkyReferenceBlockCollection Skies
{
  get { return _skiesCollection; }
}
public class ScenarioChildScenarioBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioChildScenarioBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioChildScenarioBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioChildScenarioBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioChildScenarioBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioChildScenarioBlock); }
  }
}
private ScenarioChildScenarioBlockCollection _childScenariosCollection;
public ScenarioChildScenarioBlockCollection ChildScenarios
{
  get { return _childScenariosCollection; }
}
public class PredictedResourceBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public PredictedResourceBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(PredictedResourceBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new PredictedResourceBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public PredictedResourceBlock this[int index]
  {
    get { return (InnerList[index] as PredictedResourceBlock); }
  }
}
private PredictedResourceBlockCollection _predictedResourcesCollection;
public PredictedResourceBlockCollection PredictedResources
{
  get { return _predictedResourcesCollection; }
}
public class ScenarioFunctionBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioFunctionBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioFunctionBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioFunctionBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioFunctionBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioFunctionBlock); }
  }
}
private ScenarioFunctionBlockCollection _functionsCollection;
public ScenarioFunctionBlockCollection Functions
{
  get { return _functionsCollection; }
}
public class EditorCommentBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public EditorCommentBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(EditorCommentBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new EditorCommentBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public EditorCommentBlock this[int index]
  {
    get { return (InnerList[index] as EditorCommentBlock); }
  }
}
private EditorCommentBlockCollection _commentsCollection;
public EditorCommentBlockCollection Comments
{
  get { return _commentsCollection; }
}
public class ScenarioObjectNamesBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioObjectNamesBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioObjectNamesBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioObjectNamesBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioObjectNamesBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioObjectNamesBlock); }
  }
}
private ScenarioObjectNamesBlockCollection _objectNamesCollection;
public ScenarioObjectNamesBlockCollection ObjectNames
{
  get { return _objectNamesCollection; }
}
public class ScenarioSceneryBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioSceneryBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioSceneryBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioSceneryBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioSceneryBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioSceneryBlock); }
  }
}
private ScenarioSceneryBlockCollection _sceneryCollection;
public ScenarioSceneryBlockCollection Scenery
{
  get { return _sceneryCollection; }
}
public class ScenarioSceneryPaletteBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioSceneryPaletteBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioSceneryPaletteBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioSceneryPaletteBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioSceneryPaletteBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioSceneryPaletteBlock); }
  }
}
private ScenarioSceneryPaletteBlockCollection _sceneryPaletteCollection;
public ScenarioSceneryPaletteBlockCollection SceneryPalette
{
  get { return _sceneryPaletteCollection; }
}
public class ScenarioBipedBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioBipedBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioBipedBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioBipedBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioBipedBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioBipedBlock); }
  }
}
private ScenarioBipedBlockCollection _bipedsCollection;
public ScenarioBipedBlockCollection Bipeds
{
  get { return _bipedsCollection; }
}
public class ScenarioBipedPaletteBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioBipedPaletteBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioBipedPaletteBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioBipedPaletteBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioBipedPaletteBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioBipedPaletteBlock); }
  }
}
private ScenarioBipedPaletteBlockCollection _bipedPaletteCollection;
public ScenarioBipedPaletteBlockCollection BipedPalette
{
  get { return _bipedPaletteCollection; }
}
public class ScenarioVehicleBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioVehicleBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioVehicleBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioVehicleBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioVehicleBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioVehicleBlock); }
  }
}
private ScenarioVehicleBlockCollection _vehiclesCollection;
public ScenarioVehicleBlockCollection Vehicles
{
  get { return _vehiclesCollection; }
}
public class ScenarioVehiclePaletteBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioVehiclePaletteBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioVehiclePaletteBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioVehiclePaletteBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioVehiclePaletteBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioVehiclePaletteBlock); }
  }
}
private ScenarioVehiclePaletteBlockCollection _vehiclePaletteCollection;
public ScenarioVehiclePaletteBlockCollection VehiclePalette
{
  get { return _vehiclePaletteCollection; }
}
public class ScenarioEquipmentBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioEquipmentBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioEquipmentBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioEquipmentBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioEquipmentBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioEquipmentBlock); }
  }
}
private ScenarioEquipmentBlockCollection _equipmentCollection;
public ScenarioEquipmentBlockCollection Equipment
{
  get { return _equipmentCollection; }
}
public class ScenarioEquipmentPaletteBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioEquipmentPaletteBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioEquipmentPaletteBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioEquipmentPaletteBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioEquipmentPaletteBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioEquipmentPaletteBlock); }
  }
}
private ScenarioEquipmentPaletteBlockCollection _equipmentPaletteCollection;
public ScenarioEquipmentPaletteBlockCollection EquipmentPalette
{
  get { return _equipmentPaletteCollection; }
}
public class ScenarioWeaponBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioWeaponBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioWeaponBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioWeaponBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioWeaponBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioWeaponBlock); }
  }
}
private ScenarioWeaponBlockCollection _weaponsCollection;
public ScenarioWeaponBlockCollection Weapons
{
  get { return _weaponsCollection; }
}
public class ScenarioWeaponPaletteBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioWeaponPaletteBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioWeaponPaletteBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioWeaponPaletteBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioWeaponPaletteBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioWeaponPaletteBlock); }
  }
}
private ScenarioWeaponPaletteBlockCollection _weaponPaletteCollection;
public ScenarioWeaponPaletteBlockCollection WeaponPalette
{
  get { return _weaponPaletteCollection; }
}
public class DeviceGroupBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public DeviceGroupBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(DeviceGroupBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new DeviceGroupBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public DeviceGroupBlock this[int index]
  {
    get { return (InnerList[index] as DeviceGroupBlock); }
  }
}
private DeviceGroupBlockCollection _deviceGroupsCollection;
public DeviceGroupBlockCollection DeviceGroups
{
  get { return _deviceGroupsCollection; }
}
public class ScenarioMachineBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioMachineBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioMachineBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioMachineBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioMachineBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioMachineBlock); }
  }
}
private ScenarioMachineBlockCollection _machinesCollection;
public ScenarioMachineBlockCollection Machines
{
  get { return _machinesCollection; }
}
public class ScenarioMachinePaletteBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioMachinePaletteBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioMachinePaletteBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioMachinePaletteBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioMachinePaletteBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioMachinePaletteBlock); }
  }
}
private ScenarioMachinePaletteBlockCollection _machinePaletteCollection;
public ScenarioMachinePaletteBlockCollection MachinePalette
{
  get { return _machinePaletteCollection; }
}
public class ScenarioControlBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioControlBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioControlBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioControlBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioControlBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioControlBlock); }
  }
}
private ScenarioControlBlockCollection _controlsCollection;
public ScenarioControlBlockCollection Controls
{
  get { return _controlsCollection; }
}
public class ScenarioControlPaletteBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioControlPaletteBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioControlPaletteBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioControlPaletteBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioControlPaletteBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioControlPaletteBlock); }
  }
}
private ScenarioControlPaletteBlockCollection _controlPaletteCollection;
public ScenarioControlPaletteBlockCollection ControlPalette
{
  get { return _controlPaletteCollection; }
}
public class ScenarioLightFixtureBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioLightFixtureBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioLightFixtureBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioLightFixtureBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioLightFixtureBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioLightFixtureBlock); }
  }
}
private ScenarioLightFixtureBlockCollection _lightFixturesCollection;
public ScenarioLightFixtureBlockCollection LightFixtures
{
  get { return _lightFixturesCollection; }
}
public class ScenarioLightFixturePaletteBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioLightFixturePaletteBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioLightFixturePaletteBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioLightFixturePaletteBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioLightFixturePaletteBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioLightFixturePaletteBlock); }
  }
}
private ScenarioLightFixturePaletteBlockCollection _lightFixturesPaletteCollection;
public ScenarioLightFixturePaletteBlockCollection LightFixturesPalette
{
  get { return _lightFixturesPaletteCollection; }
}
public class ScenarioSoundSceneryBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioSoundSceneryBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioSoundSceneryBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioSoundSceneryBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioSoundSceneryBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioSoundSceneryBlock); }
  }
}
private ScenarioSoundSceneryBlockCollection _soundSceneryCollection;
public ScenarioSoundSceneryBlockCollection SoundScenery
{
  get { return _soundSceneryCollection; }
}
public class ScenarioSoundSceneryPaletteBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioSoundSceneryPaletteBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioSoundSceneryPaletteBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioSoundSceneryPaletteBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioSoundSceneryPaletteBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioSoundSceneryPaletteBlock); }
  }
}
private ScenarioSoundSceneryPaletteBlockCollection _soundSceneryPaletteCollection;
public ScenarioSoundSceneryPaletteBlockCollection SoundSceneryPalette
{
  get { return _soundSceneryPaletteCollection; }
}
public class ScenarioProfilesBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioProfilesBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioProfilesBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioProfilesBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioProfilesBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioProfilesBlock); }
  }
}
private ScenarioProfilesBlockCollection _playerStartingProfileCollection;
public ScenarioProfilesBlockCollection PlayerStartingProfile
{
  get { return _playerStartingProfileCollection; }
}
public class ScenarioPlayersBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioPlayersBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioPlayersBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioPlayersBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioPlayersBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioPlayersBlock); }
  }
}
private ScenarioPlayersBlockCollection _playerStartingLocationsCollection;
public ScenarioPlayersBlockCollection PlayerStartingLocations
{
  get { return _playerStartingLocationsCollection; }
}
public class ScenarioTriggerVolumeBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioTriggerVolumeBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioTriggerVolumeBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioTriggerVolumeBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioTriggerVolumeBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioTriggerVolumeBlock); }
  }
}
private ScenarioTriggerVolumeBlockCollection _triggerVolumesCollection;
public ScenarioTriggerVolumeBlockCollection TriggerVolumes
{
  get { return _triggerVolumesCollection; }
}
public class RecordedAnimationBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public RecordedAnimationBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(RecordedAnimationBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new RecordedAnimationBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public RecordedAnimationBlock this[int index]
  {
    get { return (InnerList[index] as RecordedAnimationBlock); }
  }
}
private RecordedAnimationBlockCollection _recordedAnimationsCollection;
public RecordedAnimationBlockCollection RecordedAnimations
{
  get { return _recordedAnimationsCollection; }
}
public class ScenarioNetgameFlagsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioNetgameFlagsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioNetgameFlagsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioNetgameFlagsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioNetgameFlagsBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioNetgameFlagsBlock); }
  }
}
private ScenarioNetgameFlagsBlockCollection _netgameFlagsCollection;
public ScenarioNetgameFlagsBlockCollection NetgameFlags
{
  get { return _netgameFlagsCollection; }
}
public class ScenarioNetgameEquipmentBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioNetgameEquipmentBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioNetgameEquipmentBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioNetgameEquipmentBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioNetgameEquipmentBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioNetgameEquipmentBlock); }
  }
}
private ScenarioNetgameEquipmentBlockCollection _netgameEquipmentCollection;
public ScenarioNetgameEquipmentBlockCollection NetgameEquipment
{
  get { return _netgameEquipmentCollection; }
}
public class ScenarioStartingEquipmentBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioStartingEquipmentBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioStartingEquipmentBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioStartingEquipmentBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioStartingEquipmentBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioStartingEquipmentBlock); }
  }
}
private ScenarioStartingEquipmentBlockCollection _startingEquipmentCollection;
public ScenarioStartingEquipmentBlockCollection StartingEquipment
{
  get { return _startingEquipmentCollection; }
}
public class ScenarioBspSwitchTriggerVolumeBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioBspSwitchTriggerVolumeBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioBspSwitchTriggerVolumeBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioBspSwitchTriggerVolumeBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioBspSwitchTriggerVolumeBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioBspSwitchTriggerVolumeBlock); }
  }
}
private ScenarioBspSwitchTriggerVolumeBlockCollection _bspSwitchTriggerVolumesCollection;
public ScenarioBspSwitchTriggerVolumeBlockCollection BspSwitchTriggerVolumes
{
  get { return _bspSwitchTriggerVolumesCollection; }
}
public class ScenarioDecalsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioDecalsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioDecalsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioDecalsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioDecalsBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioDecalsBlock); }
  }
}
private ScenarioDecalsBlockCollection _decalsCollection;
public ScenarioDecalsBlockCollection Decals
{
  get { return _decalsCollection; }
}
public class ScenarioDecalPaletteBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioDecalPaletteBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioDecalPaletteBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioDecalPaletteBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioDecalPaletteBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioDecalPaletteBlock); }
  }
}
private ScenarioDecalPaletteBlockCollection _decalPaletteCollection;
public ScenarioDecalPaletteBlockCollection DecalPalette
{
  get { return _decalPaletteCollection; }
}
public class ScenarioDetailObjectCollectionPaletteBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioDetailObjectCollectionPaletteBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioDetailObjectCollectionPaletteBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioDetailObjectCollectionPaletteBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioDetailObjectCollectionPaletteBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioDetailObjectCollectionPaletteBlock); }
  }
}
private ScenarioDetailObjectCollectionPaletteBlockCollection _detailObjectCollectionPaletteCollection;
public ScenarioDetailObjectCollectionPaletteBlockCollection DetailObjectCollectionPalette
{
  get { return _detailObjectCollectionPaletteCollection; }
}
public class ActorPaletteBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ActorPaletteBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ActorPaletteBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ActorPaletteBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ActorPaletteBlock this[int index]
  {
    get { return (InnerList[index] as ActorPaletteBlock); }
  }
}
private ActorPaletteBlockCollection _actorPaletteCollection;
public ActorPaletteBlockCollection ActorPalette
{
  get { return _actorPaletteCollection; }
}
public class EncounterBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public EncounterBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(EncounterBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new EncounterBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public EncounterBlock this[int index]
  {
    get { return (InnerList[index] as EncounterBlock); }
  }
}
private EncounterBlockCollection _encountersCollection;
public EncounterBlockCollection Encounters
{
  get { return _encountersCollection; }
}
public class AiCommandListBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public AiCommandListBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(AiCommandListBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new AiCommandListBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public AiCommandListBlock this[int index]
  {
    get { return (InnerList[index] as AiCommandListBlock); }
  }
}
private AiCommandListBlockCollection _commandListsCollection;
public AiCommandListBlockCollection CommandLists
{
  get { return _commandListsCollection; }
}
public class AiAnimationReferenceBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public AiAnimationReferenceBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(AiAnimationReferenceBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new AiAnimationReferenceBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public AiAnimationReferenceBlock this[int index]
  {
    get { return (InnerList[index] as AiAnimationReferenceBlock); }
  }
}
private AiAnimationReferenceBlockCollection _aiAnimationReferencesCollection;
public AiAnimationReferenceBlockCollection AiAnimationReferences
{
  get { return _aiAnimationReferencesCollection; }
}
public class AiScriptReferenceBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public AiScriptReferenceBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(AiScriptReferenceBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new AiScriptReferenceBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public AiScriptReferenceBlock this[int index]
  {
    get { return (InnerList[index] as AiScriptReferenceBlock); }
  }
}
private AiScriptReferenceBlockCollection _aiScriptReferencesCollection;
public AiScriptReferenceBlockCollection AiScriptReferences
{
  get { return _aiScriptReferencesCollection; }
}
public class AiRecordingReferenceBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public AiRecordingReferenceBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(AiRecordingReferenceBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new AiRecordingReferenceBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public AiRecordingReferenceBlock this[int index]
  {
    get { return (InnerList[index] as AiRecordingReferenceBlock); }
  }
}
private AiRecordingReferenceBlockCollection _aiRecordingReferencesCollection;
public AiRecordingReferenceBlockCollection AiRecordingReferences
{
  get { return _aiRecordingReferencesCollection; }
}
public class AiConversationBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public AiConversationBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(AiConversationBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new AiConversationBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public AiConversationBlock this[int index]
  {
    get { return (InnerList[index] as AiConversationBlock); }
  }
}
private AiConversationBlockCollection _aiConversationsCollection;
public AiConversationBlockCollection AiConversations
{
  get { return _aiConversationsCollection; }
}
public class HsScriptsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public HsScriptsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(HsScriptsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new HsScriptsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public HsScriptsBlock this[int index]
  {
    get { return (InnerList[index] as HsScriptsBlock); }
  }
}
private HsScriptsBlockCollection _scriptsCollection;
public HsScriptsBlockCollection Scripts
{
  get { return _scriptsCollection; }
}
public class HsGlobalsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public HsGlobalsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(HsGlobalsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new HsGlobalsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public HsGlobalsBlock this[int index]
  {
    get { return (InnerList[index] as HsGlobalsBlock); }
  }
}
private HsGlobalsBlockCollection _globalsCollection;
public HsGlobalsBlockCollection Globals
{
  get { return _globalsCollection; }
}
public class HsReferencesBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public HsReferencesBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(HsReferencesBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new HsReferencesBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public HsReferencesBlock this[int index]
  {
    get { return (InnerList[index] as HsReferencesBlock); }
  }
}
private HsReferencesBlockCollection _referencesCollection;
public HsReferencesBlockCollection References
{
  get { return _referencesCollection; }
}
public class HsSourceFilesBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public HsSourceFilesBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(HsSourceFilesBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new HsSourceFilesBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public HsSourceFilesBlock this[int index]
  {
    get { return (InnerList[index] as HsSourceFilesBlock); }
  }
}
private HsSourceFilesBlockCollection _sourceFilesCollection;
public HsSourceFilesBlockCollection SourceFiles
{
  get { return _sourceFilesCollection; }
}
public class ScenarioCutsceneFlagBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioCutsceneFlagBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioCutsceneFlagBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioCutsceneFlagBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioCutsceneFlagBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioCutsceneFlagBlock); }
  }
}
private ScenarioCutsceneFlagBlockCollection _cutsceneFlagsCollection;
public ScenarioCutsceneFlagBlockCollection CutsceneFlags
{
  get { return _cutsceneFlagsCollection; }
}
public class ScenarioCutsceneCameraPointBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioCutsceneCameraPointBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioCutsceneCameraPointBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioCutsceneCameraPointBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioCutsceneCameraPointBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioCutsceneCameraPointBlock); }
  }
}
private ScenarioCutsceneCameraPointBlockCollection _cutsceneCameraPointsCollection;
public ScenarioCutsceneCameraPointBlockCollection CutsceneCameraPoints
{
  get { return _cutsceneCameraPointsCollection; }
}
public class ScenarioCutsceneTitleBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioCutsceneTitleBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioCutsceneTitleBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioCutsceneTitleBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioCutsceneTitleBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioCutsceneTitleBlock); }
  }
}
private ScenarioCutsceneTitleBlockCollection _cutsceneTitlesCollection;
public ScenarioCutsceneTitleBlockCollection CutsceneTitles
{
  get { return _cutsceneTitlesCollection; }
}
public class ScenarioStructureBspsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioStructureBspsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioStructureBspsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioStructureBspsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioStructureBspsBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioStructureBspsBlock); }
  }
}
private ScenarioStructureBspsBlockCollection _structureBspsCollection;
public ScenarioStructureBspsBlockCollection StructureBsps
{
  get { return _structureBspsCollection; }
}
public TagReference DONTUSE
{
  get { return _dONTUSE; }
  set { _dONTUSE = value; }
}
public TagReference WONTUSE
{
  get { return _wONTUSE; }
  set { _wONTUSE = value; }
}
public TagReference CANTUSE
{
  get { return _cANTUSE; }
  set { _cANTUSE = value; }
}
public Enum Type
{
  get { return _type; }
  set { _type = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Angle LocalNorth
{
  get { return _localNorth; }
  set { _localNorth = value; }
}
public Data EditorScenarioData
{
  get { return _editorScenarioData; }
  set { _editorScenarioData = value; }
}
public Data ScriptSyntaxData
{
  get { return _scriptSyntaxData; }
  set { _scriptSyntaxData = value; }
}
public Data ScriptStringData
{
  get { return _scriptStringData; }
  set { _scriptStringData = value; }
}
public TagReference CustomObjectNames
{
  get { return _customObjectNames; }
  set { _customObjectNames = value; }
}
public TagReference IngameHelpText
{
  get { return _ingameHelpText; }
  set { _ingameHelpText = value; }
}
public TagReference HudMessages
{
  get { return _hudMessages; }
  set { _hudMessages = value; }
}
public ScenarioBlock()
{
_flags = new Flags(2);
__unnamed = new Pad(20);
__unnamed2 = new Pad(136);
__unnamed3 = new Pad(224);
__unnamed4 = new Pad(84);
__unnamed5 = new Pad(84);
__unnamed6 = new Pad(24);
__unnamed7 = new Pad(108);
_skiesCollection = new ScenarioSkyReferenceBlockCollection(_skies);
_childScenariosCollection = new ScenarioChildScenarioBlockCollection(_childScenarios);
_predictedResourcesCollection = new PredictedResourceBlockCollection(_predictedResources);
_functionsCollection = new ScenarioFunctionBlockCollection(_functions);
_commentsCollection = new EditorCommentBlockCollection(_comments);
_objectNamesCollection = new ScenarioObjectNamesBlockCollection(_objectNames);
_sceneryCollection = new ScenarioSceneryBlockCollection(_scenery);
_sceneryPaletteCollection = new ScenarioSceneryPaletteBlockCollection(_sceneryPalette);
_bipedsCollection = new ScenarioBipedBlockCollection(_bipeds);
_bipedPaletteCollection = new ScenarioBipedPaletteBlockCollection(_bipedPalette);
_vehiclesCollection = new ScenarioVehicleBlockCollection(_vehicles);
_vehiclePaletteCollection = new ScenarioVehiclePaletteBlockCollection(_vehiclePalette);
_equipmentCollection = new ScenarioEquipmentBlockCollection(_equipment);
_equipmentPaletteCollection = new ScenarioEquipmentPaletteBlockCollection(_equipmentPalette);
_weaponsCollection = new ScenarioWeaponBlockCollection(_weapons);
_weaponPaletteCollection = new ScenarioWeaponPaletteBlockCollection(_weaponPalette);
_deviceGroupsCollection = new DeviceGroupBlockCollection(_deviceGroups);
_machinesCollection = new ScenarioMachineBlockCollection(_machines);
_machinePaletteCollection = new ScenarioMachinePaletteBlockCollection(_machinePalette);
_controlsCollection = new ScenarioControlBlockCollection(_controls);
_controlPaletteCollection = new ScenarioControlPaletteBlockCollection(_controlPalette);
_lightFixturesCollection = new ScenarioLightFixtureBlockCollection(_lightFixtures);
_lightFixturesPaletteCollection = new ScenarioLightFixturePaletteBlockCollection(_lightFixturesPalette);
_soundSceneryCollection = new ScenarioSoundSceneryBlockCollection(_soundScenery);
_soundSceneryPaletteCollection = new ScenarioSoundSceneryPaletteBlockCollection(_soundSceneryPalette);
_playerStartingProfileCollection = new ScenarioProfilesBlockCollection(_playerStartingProfile);
_playerStartingLocationsCollection = new ScenarioPlayersBlockCollection(_playerStartingLocations);
_triggerVolumesCollection = new ScenarioTriggerVolumeBlockCollection(_triggerVolumes);
_recordedAnimationsCollection = new RecordedAnimationBlockCollection(_recordedAnimations);
_netgameFlagsCollection = new ScenarioNetgameFlagsBlockCollection(_netgameFlags);
_netgameEquipmentCollection = new ScenarioNetgameEquipmentBlockCollection(_netgameEquipment);
_startingEquipmentCollection = new ScenarioStartingEquipmentBlockCollection(_startingEquipment);
_bspSwitchTriggerVolumesCollection = new ScenarioBspSwitchTriggerVolumeBlockCollection(_bspSwitchTriggerVolumes);
_decalsCollection = new ScenarioDecalsBlockCollection(_decals);
_decalPaletteCollection = new ScenarioDecalPaletteBlockCollection(_decalPalette);
_detailObjectCollectionPaletteCollection = new ScenarioDetailObjectCollectionPaletteBlockCollection(_detailObjectCollectionPalette);
_actorPaletteCollection = new ActorPaletteBlockCollection(_actorPalette);
_encountersCollection = new EncounterBlockCollection(_encounters);
_commandListsCollection = new AiCommandListBlockCollection(_commandLists);
_aiAnimationReferencesCollection = new AiAnimationReferenceBlockCollection(_aiAnimationReferences);
_aiScriptReferencesCollection = new AiScriptReferenceBlockCollection(_aiScriptReferences);
_aiRecordingReferencesCollection = new AiRecordingReferenceBlockCollection(_aiRecordingReferences);
_aiConversationsCollection = new AiConversationBlockCollection(_aiConversations);
_scriptsCollection = new HsScriptsBlockCollection(_scripts);
_globalsCollection = new HsGlobalsBlockCollection(_globals);
_referencesCollection = new HsReferencesBlockCollection(_references);
_sourceFilesCollection = new HsSourceFilesBlockCollection(_sourceFiles);
_cutsceneFlagsCollection = new ScenarioCutsceneFlagBlockCollection(_cutsceneFlags);
_cutsceneCameraPointsCollection = new ScenarioCutsceneCameraPointBlockCollection(_cutsceneCameraPoints);
_cutsceneTitlesCollection = new ScenarioCutsceneTitleBlockCollection(_cutsceneTitles);
_structureBspsCollection = new ScenarioStructureBspsBlockCollection(_structureBsps);

}
public void Read(BinaryReader reader)
{
  _dONTUSE.Read(reader);
  _wONTUSE.Read(reader);
  _cANTUSE.Read(reader);
  _skies.Read(reader);
  _type.Read(reader);
  _flags.Read(reader);
  _childScenarios.Read(reader);
  _localNorth.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  _predictedResources.Read(reader);
  _functions.Read(reader);
  _editorScenarioData.Read(reader);
  _comments.Read(reader);
  __unnamed3.Read(reader);
  _objectNames.Read(reader);
  _scenery.Read(reader);
  _sceneryPalette.Read(reader);
  _bipeds.Read(reader);
  _bipedPalette.Read(reader);
  _vehicles.Read(reader);
  _vehiclePalette.Read(reader);
  _equipment.Read(reader);
  _equipmentPalette.Read(reader);
  _weapons.Read(reader);
  _weaponPalette.Read(reader);
  _deviceGroups.Read(reader);
  _machines.Read(reader);
  _machinePalette.Read(reader);
  _controls.Read(reader);
  _controlPalette.Read(reader);
  _lightFixtures.Read(reader);
  _lightFixturesPalette.Read(reader);
  _soundScenery.Read(reader);
  _soundSceneryPalette.Read(reader);
  __unnamed4.Read(reader);
  _playerStartingProfile.Read(reader);
  _playerStartingLocations.Read(reader);
  _triggerVolumes.Read(reader);
  _recordedAnimations.Read(reader);
  _netgameFlags.Read(reader);
  _netgameEquipment.Read(reader);
  _startingEquipment.Read(reader);
  _bspSwitchTriggerVolumes.Read(reader);
  _decals.Read(reader);
  _decalPalette.Read(reader);
  _detailObjectCollectionPalette.Read(reader);
  __unnamed5.Read(reader);
  _actorPalette.Read(reader);
  _encounters.Read(reader);
  _commandLists.Read(reader);
  _aiAnimationReferences.Read(reader);
  _aiScriptReferences.Read(reader);
  _aiRecordingReferences.Read(reader);
  _aiConversations.Read(reader);
  _scriptSyntaxData.Read(reader);
  _scriptStringData.Read(reader);
  _scripts.Read(reader);
  _globals.Read(reader);
  _references.Read(reader);
  _sourceFiles.Read(reader);
  __unnamed6.Read(reader);
  _cutsceneFlags.Read(reader);
  _cutsceneCameraPoints.Read(reader);
  _cutsceneTitles.Read(reader);
  __unnamed7.Read(reader);
  _customObjectNames.Read(reader);
  _ingameHelpText.Read(reader);
  _hudMessages.Read(reader);
  _structureBsps.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_dONTUSE.ReadString(reader);
_wONTUSE.ReadString(reader);
_cANTUSE.ReadString(reader);
for (int x=0; x<_skies.Count; x++)
{
  Skies.AddNew();
  Skies[x].Read(reader);
}
for (int x=0; x<_skies.Count; x++)
  Skies[x].ReadChildData(reader);
for (int x=0; x<_childScenarios.Count; x++)
{
  ChildScenarios.AddNew();
  ChildScenarios[x].Read(reader);
}
for (int x=0; x<_childScenarios.Count; x++)
  ChildScenarios[x].ReadChildData(reader);
for (int x=0; x<_predictedResources.Count; x++)
{
  PredictedResources.AddNew();
  PredictedResources[x].Read(reader);
}
for (int x=0; x<_predictedResources.Count; x++)
  PredictedResources[x].ReadChildData(reader);
for (int x=0; x<_functions.Count; x++)
{
  Functions.AddNew();
  Functions[x].Read(reader);
}
for (int x=0; x<_functions.Count; x++)
  Functions[x].ReadChildData(reader);
_editorScenarioData.ReadBinary(reader);
for (int x=0; x<_comments.Count; x++)
{
  Comments.AddNew();
  Comments[x].Read(reader);
}
for (int x=0; x<_comments.Count; x++)
  Comments[x].ReadChildData(reader);
for (int x=0; x<_objectNames.Count; x++)
{
  ObjectNames.AddNew();
  ObjectNames[x].Read(reader);
}
for (int x=0; x<_objectNames.Count; x++)
  ObjectNames[x].ReadChildData(reader);
for (int x=0; x<_scenery.Count; x++)
{
  Scenery.AddNew();
  Scenery[x].Read(reader);
}
for (int x=0; x<_scenery.Count; x++)
  Scenery[x].ReadChildData(reader);
for (int x=0; x<_sceneryPalette.Count; x++)
{
  SceneryPalette.AddNew();
  SceneryPalette[x].Read(reader);
}
for (int x=0; x<_sceneryPalette.Count; x++)
  SceneryPalette[x].ReadChildData(reader);
for (int x=0; x<_bipeds.Count; x++)
{
  Bipeds.AddNew();
  Bipeds[x].Read(reader);
}
for (int x=0; x<_bipeds.Count; x++)
  Bipeds[x].ReadChildData(reader);
for (int x=0; x<_bipedPalette.Count; x++)
{
  BipedPalette.AddNew();
  BipedPalette[x].Read(reader);
}
for (int x=0; x<_bipedPalette.Count; x++)
  BipedPalette[x].ReadChildData(reader);
for (int x=0; x<_vehicles.Count; x++)
{
  Vehicles.AddNew();
  Vehicles[x].Read(reader);
}
for (int x=0; x<_vehicles.Count; x++)
  Vehicles[x].ReadChildData(reader);
for (int x=0; x<_vehiclePalette.Count; x++)
{
  VehiclePalette.AddNew();
  VehiclePalette[x].Read(reader);
}
for (int x=0; x<_vehiclePalette.Count; x++)
  VehiclePalette[x].ReadChildData(reader);
for (int x=0; x<_equipment.Count; x++)
{
  Equipment.AddNew();
  Equipment[x].Read(reader);
}
for (int x=0; x<_equipment.Count; x++)
  Equipment[x].ReadChildData(reader);
for (int x=0; x<_equipmentPalette.Count; x++)
{
  EquipmentPalette.AddNew();
  EquipmentPalette[x].Read(reader);
}
for (int x=0; x<_equipmentPalette.Count; x++)
  EquipmentPalette[x].ReadChildData(reader);
for (int x=0; x<_weapons.Count; x++)
{
  Weapons.AddNew();
  Weapons[x].Read(reader);
}
for (int x=0; x<_weapons.Count; x++)
  Weapons[x].ReadChildData(reader);
for (int x=0; x<_weaponPalette.Count; x++)
{
  WeaponPalette.AddNew();
  WeaponPalette[x].Read(reader);
}
for (int x=0; x<_weaponPalette.Count; x++)
  WeaponPalette[x].ReadChildData(reader);
for (int x=0; x<_deviceGroups.Count; x++)
{
  DeviceGroups.AddNew();
  DeviceGroups[x].Read(reader);
}
for (int x=0; x<_deviceGroups.Count; x++)
  DeviceGroups[x].ReadChildData(reader);
for (int x=0; x<_machines.Count; x++)
{
  Machines.AddNew();
  Machines[x].Read(reader);
}
for (int x=0; x<_machines.Count; x++)
  Machines[x].ReadChildData(reader);
for (int x=0; x<_machinePalette.Count; x++)
{
  MachinePalette.AddNew();
  MachinePalette[x].Read(reader);
}
for (int x=0; x<_machinePalette.Count; x++)
  MachinePalette[x].ReadChildData(reader);
for (int x=0; x<_controls.Count; x++)
{
  Controls.AddNew();
  Controls[x].Read(reader);
}
for (int x=0; x<_controls.Count; x++)
  Controls[x].ReadChildData(reader);
for (int x=0; x<_controlPalette.Count; x++)
{
  ControlPalette.AddNew();
  ControlPalette[x].Read(reader);
}
for (int x=0; x<_controlPalette.Count; x++)
  ControlPalette[x].ReadChildData(reader);
for (int x=0; x<_lightFixtures.Count; x++)
{
  LightFixtures.AddNew();
  LightFixtures[x].Read(reader);
}
for (int x=0; x<_lightFixtures.Count; x++)
  LightFixtures[x].ReadChildData(reader);
for (int x=0; x<_lightFixturesPalette.Count; x++)
{
  LightFixturesPalette.AddNew();
  LightFixturesPalette[x].Read(reader);
}
for (int x=0; x<_lightFixturesPalette.Count; x++)
  LightFixturesPalette[x].ReadChildData(reader);
for (int x=0; x<_soundScenery.Count; x++)
{
  SoundScenery.AddNew();
  SoundScenery[x].Read(reader);
}
for (int x=0; x<_soundScenery.Count; x++)
  SoundScenery[x].ReadChildData(reader);
for (int x=0; x<_soundSceneryPalette.Count; x++)
{
  SoundSceneryPalette.AddNew();
  SoundSceneryPalette[x].Read(reader);
}
for (int x=0; x<_soundSceneryPalette.Count; x++)
  SoundSceneryPalette[x].ReadChildData(reader);
for (int x=0; x<_playerStartingProfile.Count; x++)
{
  PlayerStartingProfile.AddNew();
  PlayerStartingProfile[x].Read(reader);
}
for (int x=0; x<_playerStartingProfile.Count; x++)
  PlayerStartingProfile[x].ReadChildData(reader);
for (int x=0; x<_playerStartingLocations.Count; x++)
{
  PlayerStartingLocations.AddNew();
  PlayerStartingLocations[x].Read(reader);
}
for (int x=0; x<_playerStartingLocations.Count; x++)
  PlayerStartingLocations[x].ReadChildData(reader);
for (int x=0; x<_triggerVolumes.Count; x++)
{
  TriggerVolumes.AddNew();
  TriggerVolumes[x].Read(reader);
}
for (int x=0; x<_triggerVolumes.Count; x++)
  TriggerVolumes[x].ReadChildData(reader);
for (int x=0; x<_recordedAnimations.Count; x++)
{
  RecordedAnimations.AddNew();
  RecordedAnimations[x].Read(reader);
}
for (int x=0; x<_recordedAnimations.Count; x++)
  RecordedAnimations[x].ReadChildData(reader);
for (int x=0; x<_netgameFlags.Count; x++)
{
  NetgameFlags.AddNew();
  NetgameFlags[x].Read(reader);
}
for (int x=0; x<_netgameFlags.Count; x++)
  NetgameFlags[x].ReadChildData(reader);
for (int x=0; x<_netgameEquipment.Count; x++)
{
  NetgameEquipment.AddNew();
  NetgameEquipment[x].Read(reader);
}
for (int x=0; x<_netgameEquipment.Count; x++)
  NetgameEquipment[x].ReadChildData(reader);
for (int x=0; x<_startingEquipment.Count; x++)
{
  StartingEquipment.AddNew();
  StartingEquipment[x].Read(reader);
}
for (int x=0; x<_startingEquipment.Count; x++)
  StartingEquipment[x].ReadChildData(reader);
for (int x=0; x<_bspSwitchTriggerVolumes.Count; x++)
{
  BspSwitchTriggerVolumes.AddNew();
  BspSwitchTriggerVolumes[x].Read(reader);
}
for (int x=0; x<_bspSwitchTriggerVolumes.Count; x++)
  BspSwitchTriggerVolumes[x].ReadChildData(reader);
for (int x=0; x<_decals.Count; x++)
{
  Decals.AddNew();
  Decals[x].Read(reader);
}
for (int x=0; x<_decals.Count; x++)
  Decals[x].ReadChildData(reader);
for (int x=0; x<_decalPalette.Count; x++)
{
  DecalPalette.AddNew();
  DecalPalette[x].Read(reader);
}
for (int x=0; x<_decalPalette.Count; x++)
  DecalPalette[x].ReadChildData(reader);
for (int x=0; x<_detailObjectCollectionPalette.Count; x++)
{
  DetailObjectCollectionPalette.AddNew();
  DetailObjectCollectionPalette[x].Read(reader);
}
for (int x=0; x<_detailObjectCollectionPalette.Count; x++)
  DetailObjectCollectionPalette[x].ReadChildData(reader);
for (int x=0; x<_actorPalette.Count; x++)
{
  ActorPalette.AddNew();
  ActorPalette[x].Read(reader);
}
for (int x=0; x<_actorPalette.Count; x++)
  ActorPalette[x].ReadChildData(reader);
for (int x=0; x<_encounters.Count; x++)
{
  Encounters.AddNew();
  Encounters[x].Read(reader);
}
for (int x=0; x<_encounters.Count; x++)
  Encounters[x].ReadChildData(reader);
for (int x=0; x<_commandLists.Count; x++)
{
  CommandLists.AddNew();
  CommandLists[x].Read(reader);
}
for (int x=0; x<_commandLists.Count; x++)
  CommandLists[x].ReadChildData(reader);
for (int x=0; x<_aiAnimationReferences.Count; x++)
{
  AiAnimationReferences.AddNew();
  AiAnimationReferences[x].Read(reader);
}
for (int x=0; x<_aiAnimationReferences.Count; x++)
  AiAnimationReferences[x].ReadChildData(reader);
for (int x=0; x<_aiScriptReferences.Count; x++)
{
  AiScriptReferences.AddNew();
  AiScriptReferences[x].Read(reader);
}
for (int x=0; x<_aiScriptReferences.Count; x++)
  AiScriptReferences[x].ReadChildData(reader);
for (int x=0; x<_aiRecordingReferences.Count; x++)
{
  AiRecordingReferences.AddNew();
  AiRecordingReferences[x].Read(reader);
}
for (int x=0; x<_aiRecordingReferences.Count; x++)
  AiRecordingReferences[x].ReadChildData(reader);
for (int x=0; x<_aiConversations.Count; x++)
{
  AiConversations.AddNew();
  AiConversations[x].Read(reader);
}
for (int x=0; x<_aiConversations.Count; x++)
  AiConversations[x].ReadChildData(reader);
_scriptSyntaxData.ReadBinary(reader);
_scriptStringData.ReadBinary(reader);
for (int x=0; x<_scripts.Count; x++)
{
  Scripts.AddNew();
  Scripts[x].Read(reader);
}
for (int x=0; x<_scripts.Count; x++)
  Scripts[x].ReadChildData(reader);
for (int x=0; x<_globals.Count; x++)
{
  Globals.AddNew();
  Globals[x].Read(reader);
}
for (int x=0; x<_globals.Count; x++)
  Globals[x].ReadChildData(reader);
for (int x=0; x<_references.Count; x++)
{
  References.AddNew();
  References[x].Read(reader);
}
for (int x=0; x<_references.Count; x++)
  References[x].ReadChildData(reader);
for (int x=0; x<_sourceFiles.Count; x++)
{
  SourceFiles.AddNew();
  SourceFiles[x].Read(reader);
}
for (int x=0; x<_sourceFiles.Count; x++)
  SourceFiles[x].ReadChildData(reader);
for (int x=0; x<_cutsceneFlags.Count; x++)
{
  CutsceneFlags.AddNew();
  CutsceneFlags[x].Read(reader);
}
for (int x=0; x<_cutsceneFlags.Count; x++)
  CutsceneFlags[x].ReadChildData(reader);
for (int x=0; x<_cutsceneCameraPoints.Count; x++)
{
  CutsceneCameraPoints.AddNew();
  CutsceneCameraPoints[x].Read(reader);
}
for (int x=0; x<_cutsceneCameraPoints.Count; x++)
  CutsceneCameraPoints[x].ReadChildData(reader);
for (int x=0; x<_cutsceneTitles.Count; x++)
{
  CutsceneTitles.AddNew();
  CutsceneTitles[x].Read(reader);
}
for (int x=0; x<_cutsceneTitles.Count; x++)
  CutsceneTitles[x].ReadChildData(reader);
_customObjectNames.ReadString(reader);
_ingameHelpText.ReadString(reader);
_hudMessages.ReadString(reader);
for (int x=0; x<_structureBsps.Count; x++)
{
  StructureBsps.AddNew();
  StructureBsps[x].Read(reader);
}
for (int x=0; x<_structureBsps.Count; x++)
  StructureBsps[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _dONTUSE.Write(writer);
    _wONTUSE.Write(writer);
    _cANTUSE.Write(writer);
    _skies.Write(writer);
    _type.Write(writer);
    _flags.Write(writer);
    _childScenarios.Write(writer);
    _localNorth.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    _predictedResources.Write(writer);
    _functions.Write(writer);
    _editorScenarioData.Write(writer);
    _comments.Write(writer);
    __unnamed3.Write(writer);
    _objectNames.Write(writer);
    _scenery.Write(writer);
    _sceneryPalette.Write(writer);
    _bipeds.Write(writer);
    _bipedPalette.Write(writer);
    _vehicles.Write(writer);
    _vehiclePalette.Write(writer);
    _equipment.Write(writer);
    _equipmentPalette.Write(writer);
    _weapons.Write(writer);
    _weaponPalette.Write(writer);
    _deviceGroups.Write(writer);
    _machines.Write(writer);
    _machinePalette.Write(writer);
    _controls.Write(writer);
    _controlPalette.Write(writer);
    _lightFixtures.Write(writer);
    _lightFixturesPalette.Write(writer);
    _soundScenery.Write(writer);
    _soundSceneryPalette.Write(writer);
    __unnamed4.Write(writer);
    _playerStartingProfile.Write(writer);
    _playerStartingLocations.Write(writer);
    _triggerVolumes.Write(writer);
    _recordedAnimations.Write(writer);
    _netgameFlags.Write(writer);
    _netgameEquipment.Write(writer);
    _startingEquipment.Write(writer);
    _bspSwitchTriggerVolumes.Write(writer);
    _decals.Write(writer);
    _decalPalette.Write(writer);
    _detailObjectCollectionPalette.Write(writer);
    __unnamed5.Write(writer);
    _actorPalette.Write(writer);
    _encounters.Write(writer);
    _commandLists.Write(writer);
    _aiAnimationReferences.Write(writer);
    _aiScriptReferences.Write(writer);
    _aiRecordingReferences.Write(writer);
    _aiConversations.Write(writer);
    _scriptSyntaxData.Write(writer);
    _scriptStringData.Write(writer);
    _scripts.Write(writer);
    _globals.Write(writer);
    _references.Write(writer);
    _sourceFiles.Write(writer);
    __unnamed6.Write(writer);
    _cutsceneFlags.Write(writer);
    _cutsceneCameraPoints.Write(writer);
    _cutsceneTitles.Write(writer);
    __unnamed7.Write(writer);
    _customObjectNames.Write(writer);
    _ingameHelpText.Write(writer);
    _hudMessages.Write(writer);
    _structureBsps.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_dONTUSE.WriteString(writer);
_wONTUSE.WriteString(writer);
_cANTUSE.WriteString(writer);
_skies.UpdateReflexiveOffset(writer);
for (int x=0; x<_skies.Count; x++)
{
  Skies[x].Write(writer);
}
for (int x=0; x<_skies.Count; x++)
  Skies[x].WriteChildData(writer);
_childScenarios.UpdateReflexiveOffset(writer);
for (int x=0; x<_childScenarios.Count; x++)
{
  ChildScenarios[x].Write(writer);
}
for (int x=0; x<_childScenarios.Count; x++)
  ChildScenarios[x].WriteChildData(writer);
_predictedResources.UpdateReflexiveOffset(writer);
for (int x=0; x<_predictedResources.Count; x++)
{
  PredictedResources[x].Write(writer);
}
for (int x=0; x<_predictedResources.Count; x++)
  PredictedResources[x].WriteChildData(writer);
_functions.UpdateReflexiveOffset(writer);
for (int x=0; x<_functions.Count; x++)
{
  Functions[x].Write(writer);
}
for (int x=0; x<_functions.Count; x++)
  Functions[x].WriteChildData(writer);
_editorScenarioData.WriteBinary(writer);
_comments.UpdateReflexiveOffset(writer);
for (int x=0; x<_comments.Count; x++)
{
  Comments[x].Write(writer);
}
for (int x=0; x<_comments.Count; x++)
  Comments[x].WriteChildData(writer);
_objectNames.UpdateReflexiveOffset(writer);
for (int x=0; x<_objectNames.Count; x++)
{
  ObjectNames[x].Write(writer);
}
for (int x=0; x<_objectNames.Count; x++)
  ObjectNames[x].WriteChildData(writer);
_scenery.UpdateReflexiveOffset(writer);
for (int x=0; x<_scenery.Count; x++)
{
  Scenery[x].Write(writer);
}
for (int x=0; x<_scenery.Count; x++)
  Scenery[x].WriteChildData(writer);
_sceneryPalette.UpdateReflexiveOffset(writer);
for (int x=0; x<_sceneryPalette.Count; x++)
{
  SceneryPalette[x].Write(writer);
}
for (int x=0; x<_sceneryPalette.Count; x++)
  SceneryPalette[x].WriteChildData(writer);
_bipeds.UpdateReflexiveOffset(writer);
for (int x=0; x<_bipeds.Count; x++)
{
  Bipeds[x].Write(writer);
}
for (int x=0; x<_bipeds.Count; x++)
  Bipeds[x].WriteChildData(writer);
_bipedPalette.UpdateReflexiveOffset(writer);
for (int x=0; x<_bipedPalette.Count; x++)
{
  BipedPalette[x].Write(writer);
}
for (int x=0; x<_bipedPalette.Count; x++)
  BipedPalette[x].WriteChildData(writer);
_vehicles.UpdateReflexiveOffset(writer);
for (int x=0; x<_vehicles.Count; x++)
{
  Vehicles[x].Write(writer);
}
for (int x=0; x<_vehicles.Count; x++)
  Vehicles[x].WriteChildData(writer);
_vehiclePalette.UpdateReflexiveOffset(writer);
for (int x=0; x<_vehiclePalette.Count; x++)
{
  VehiclePalette[x].Write(writer);
}
for (int x=0; x<_vehiclePalette.Count; x++)
  VehiclePalette[x].WriteChildData(writer);
_equipment.UpdateReflexiveOffset(writer);
for (int x=0; x<_equipment.Count; x++)
{
  Equipment[x].Write(writer);
}
for (int x=0; x<_equipment.Count; x++)
  Equipment[x].WriteChildData(writer);
_equipmentPalette.UpdateReflexiveOffset(writer);
for (int x=0; x<_equipmentPalette.Count; x++)
{
  EquipmentPalette[x].Write(writer);
}
for (int x=0; x<_equipmentPalette.Count; x++)
  EquipmentPalette[x].WriteChildData(writer);
_weapons.UpdateReflexiveOffset(writer);
for (int x=0; x<_weapons.Count; x++)
{
  Weapons[x].Write(writer);
}
for (int x=0; x<_weapons.Count; x++)
  Weapons[x].WriteChildData(writer);
_weaponPalette.UpdateReflexiveOffset(writer);
for (int x=0; x<_weaponPalette.Count; x++)
{
  WeaponPalette[x].Write(writer);
}
for (int x=0; x<_weaponPalette.Count; x++)
  WeaponPalette[x].WriteChildData(writer);
_deviceGroups.UpdateReflexiveOffset(writer);
for (int x=0; x<_deviceGroups.Count; x++)
{
  DeviceGroups[x].Write(writer);
}
for (int x=0; x<_deviceGroups.Count; x++)
  DeviceGroups[x].WriteChildData(writer);
_machines.UpdateReflexiveOffset(writer);
for (int x=0; x<_machines.Count; x++)
{
  Machines[x].Write(writer);
}
for (int x=0; x<_machines.Count; x++)
  Machines[x].WriteChildData(writer);
_machinePalette.UpdateReflexiveOffset(writer);
for (int x=0; x<_machinePalette.Count; x++)
{
  MachinePalette[x].Write(writer);
}
for (int x=0; x<_machinePalette.Count; x++)
  MachinePalette[x].WriteChildData(writer);
_controls.UpdateReflexiveOffset(writer);
for (int x=0; x<_controls.Count; x++)
{
  Controls[x].Write(writer);
}
for (int x=0; x<_controls.Count; x++)
  Controls[x].WriteChildData(writer);
_controlPalette.UpdateReflexiveOffset(writer);
for (int x=0; x<_controlPalette.Count; x++)
{
  ControlPalette[x].Write(writer);
}
for (int x=0; x<_controlPalette.Count; x++)
  ControlPalette[x].WriteChildData(writer);
_lightFixtures.UpdateReflexiveOffset(writer);
for (int x=0; x<_lightFixtures.Count; x++)
{
  LightFixtures[x].Write(writer);
}
for (int x=0; x<_lightFixtures.Count; x++)
  LightFixtures[x].WriteChildData(writer);
_lightFixturesPalette.UpdateReflexiveOffset(writer);
for (int x=0; x<_lightFixturesPalette.Count; x++)
{
  LightFixturesPalette[x].Write(writer);
}
for (int x=0; x<_lightFixturesPalette.Count; x++)
  LightFixturesPalette[x].WriteChildData(writer);
_soundScenery.UpdateReflexiveOffset(writer);
for (int x=0; x<_soundScenery.Count; x++)
{
  SoundScenery[x].Write(writer);
}
for (int x=0; x<_soundScenery.Count; x++)
  SoundScenery[x].WriteChildData(writer);
_soundSceneryPalette.UpdateReflexiveOffset(writer);
for (int x=0; x<_soundSceneryPalette.Count; x++)
{
  SoundSceneryPalette[x].Write(writer);
}
for (int x=0; x<_soundSceneryPalette.Count; x++)
  SoundSceneryPalette[x].WriteChildData(writer);
_playerStartingProfile.UpdateReflexiveOffset(writer);
for (int x=0; x<_playerStartingProfile.Count; x++)
{
  PlayerStartingProfile[x].Write(writer);
}
for (int x=0; x<_playerStartingProfile.Count; x++)
  PlayerStartingProfile[x].WriteChildData(writer);
_playerStartingLocations.UpdateReflexiveOffset(writer);
for (int x=0; x<_playerStartingLocations.Count; x++)
{
  PlayerStartingLocations[x].Write(writer);
}
for (int x=0; x<_playerStartingLocations.Count; x++)
  PlayerStartingLocations[x].WriteChildData(writer);
_triggerVolumes.UpdateReflexiveOffset(writer);
for (int x=0; x<_triggerVolumes.Count; x++)
{
  TriggerVolumes[x].Write(writer);
}
for (int x=0; x<_triggerVolumes.Count; x++)
  TriggerVolumes[x].WriteChildData(writer);
_recordedAnimations.UpdateReflexiveOffset(writer);
for (int x=0; x<_recordedAnimations.Count; x++)
{
  RecordedAnimations[x].Write(writer);
}
for (int x=0; x<_recordedAnimations.Count; x++)
  RecordedAnimations[x].WriteChildData(writer);
_netgameFlags.UpdateReflexiveOffset(writer);
for (int x=0; x<_netgameFlags.Count; x++)
{
  NetgameFlags[x].Write(writer);
}
for (int x=0; x<_netgameFlags.Count; x++)
  NetgameFlags[x].WriteChildData(writer);
_netgameEquipment.UpdateReflexiveOffset(writer);
for (int x=0; x<_netgameEquipment.Count; x++)
{
  NetgameEquipment[x].Write(writer);
}
for (int x=0; x<_netgameEquipment.Count; x++)
  NetgameEquipment[x].WriteChildData(writer);
_startingEquipment.UpdateReflexiveOffset(writer);
for (int x=0; x<_startingEquipment.Count; x++)
{
  StartingEquipment[x].Write(writer);
}
for (int x=0; x<_startingEquipment.Count; x++)
  StartingEquipment[x].WriteChildData(writer);
_bspSwitchTriggerVolumes.UpdateReflexiveOffset(writer);
for (int x=0; x<_bspSwitchTriggerVolumes.Count; x++)
{
  BspSwitchTriggerVolumes[x].Write(writer);
}
for (int x=0; x<_bspSwitchTriggerVolumes.Count; x++)
  BspSwitchTriggerVolumes[x].WriteChildData(writer);
_decals.UpdateReflexiveOffset(writer);
for (int x=0; x<_decals.Count; x++)
{
  Decals[x].Write(writer);
}
for (int x=0; x<_decals.Count; x++)
  Decals[x].WriteChildData(writer);
_decalPalette.UpdateReflexiveOffset(writer);
for (int x=0; x<_decalPalette.Count; x++)
{
  DecalPalette[x].Write(writer);
}
for (int x=0; x<_decalPalette.Count; x++)
  DecalPalette[x].WriteChildData(writer);
_detailObjectCollectionPalette.UpdateReflexiveOffset(writer);
for (int x=0; x<_detailObjectCollectionPalette.Count; x++)
{
  DetailObjectCollectionPalette[x].Write(writer);
}
for (int x=0; x<_detailObjectCollectionPalette.Count; x++)
  DetailObjectCollectionPalette[x].WriteChildData(writer);
_actorPalette.UpdateReflexiveOffset(writer);
for (int x=0; x<_actorPalette.Count; x++)
{
  ActorPalette[x].Write(writer);
}
for (int x=0; x<_actorPalette.Count; x++)
  ActorPalette[x].WriteChildData(writer);
_encounters.UpdateReflexiveOffset(writer);
for (int x=0; x<_encounters.Count; x++)
{
  Encounters[x].Write(writer);
}
for (int x=0; x<_encounters.Count; x++)
  Encounters[x].WriteChildData(writer);
_commandLists.UpdateReflexiveOffset(writer);
for (int x=0; x<_commandLists.Count; x++)
{
  CommandLists[x].Write(writer);
}
for (int x=0; x<_commandLists.Count; x++)
  CommandLists[x].WriteChildData(writer);
_aiAnimationReferences.UpdateReflexiveOffset(writer);
for (int x=0; x<_aiAnimationReferences.Count; x++)
{
  AiAnimationReferences[x].Write(writer);
}
for (int x=0; x<_aiAnimationReferences.Count; x++)
  AiAnimationReferences[x].WriteChildData(writer);
_aiScriptReferences.UpdateReflexiveOffset(writer);
for (int x=0; x<_aiScriptReferences.Count; x++)
{
  AiScriptReferences[x].Write(writer);
}
for (int x=0; x<_aiScriptReferences.Count; x++)
  AiScriptReferences[x].WriteChildData(writer);
_aiRecordingReferences.UpdateReflexiveOffset(writer);
for (int x=0; x<_aiRecordingReferences.Count; x++)
{
  AiRecordingReferences[x].Write(writer);
}
for (int x=0; x<_aiRecordingReferences.Count; x++)
  AiRecordingReferences[x].WriteChildData(writer);
_aiConversations.UpdateReflexiveOffset(writer);
for (int x=0; x<_aiConversations.Count; x++)
{
  AiConversations[x].Write(writer);
}
for (int x=0; x<_aiConversations.Count; x++)
  AiConversations[x].WriteChildData(writer);
_scriptSyntaxData.WriteBinary(writer);
_scriptStringData.WriteBinary(writer);
_scripts.UpdateReflexiveOffset(writer);
for (int x=0; x<_scripts.Count; x++)
{
  Scripts[x].Write(writer);
}
for (int x=0; x<_scripts.Count; x++)
  Scripts[x].WriteChildData(writer);
_globals.UpdateReflexiveOffset(writer);
for (int x=0; x<_globals.Count; x++)
{
  Globals[x].Write(writer);
}
for (int x=0; x<_globals.Count; x++)
  Globals[x].WriteChildData(writer);
_references.UpdateReflexiveOffset(writer);
for (int x=0; x<_references.Count; x++)
{
  References[x].Write(writer);
}
for (int x=0; x<_references.Count; x++)
  References[x].WriteChildData(writer);
_sourceFiles.UpdateReflexiveOffset(writer);
for (int x=0; x<_sourceFiles.Count; x++)
{
  SourceFiles[x].Write(writer);
}
for (int x=0; x<_sourceFiles.Count; x++)
  SourceFiles[x].WriteChildData(writer);
_cutsceneFlags.UpdateReflexiveOffset(writer);
for (int x=0; x<_cutsceneFlags.Count; x++)
{
  CutsceneFlags[x].Write(writer);
}
for (int x=0; x<_cutsceneFlags.Count; x++)
  CutsceneFlags[x].WriteChildData(writer);
_cutsceneCameraPoints.UpdateReflexiveOffset(writer);
for (int x=0; x<_cutsceneCameraPoints.Count; x++)
{
  CutsceneCameraPoints[x].Write(writer);
}
for (int x=0; x<_cutsceneCameraPoints.Count; x++)
  CutsceneCameraPoints[x].WriteChildData(writer);
_cutsceneTitles.UpdateReflexiveOffset(writer);
for (int x=0; x<_cutsceneTitles.Count; x++)
{
  CutsceneTitles[x].Write(writer);
}
for (int x=0; x<_cutsceneTitles.Count; x++)
  CutsceneTitles[x].WriteChildData(writer);
_customObjectNames.WriteString(writer);
_ingameHelpText.WriteString(writer);
_hudMessages.WriteString(writer);
_structureBsps.UpdateReflexiveOffset(writer);
for (int x=0; x<_structureBsps.Count; x++)
{
  StructureBsps[x].Write(writer);
}
for (int x=0; x<_structureBsps.Count; x++)
  StructureBsps[x].WriteChildData(writer);
}
}
public class ScenarioSkyReferenceBlock : IBlock
{
private TagReference _sky = new TagReference();
public TagReference Sky
{
  get { return _sky; }
  set { _sky = value; }
}
public ScenarioSkyReferenceBlock()
{

}
public void Read(BinaryReader reader)
{
  _sky.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_sky.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _sky.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_sky.WriteString(writer);
}
}
public class ScenarioChildScenarioBlock : IBlock
{
private TagReference _childScenario = new TagReference();
private Pad  __unnamed;	
public TagReference ChildScenario
{
  get { return _childScenario; }
  set { _childScenario = value; }
}
public ScenarioChildScenarioBlock()
{
__unnamed = new Pad(16);

}
public void Read(BinaryReader reader)
{
  _childScenario.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_childScenario.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _childScenario.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_childScenario.WriteString(writer);
}
}
public class PredictedResourceBlock : IBlock
{
private Enum _type = new Enum();
private ShortInteger _resourceIndex = new ShortInteger();
private LongInteger _tagIndex = new LongInteger();
public Enum Type
{
  get { return _type; }
  set { _type = value; }
}
public ShortInteger ResourceIndex
{
  get { return _resourceIndex; }
  set { _resourceIndex = value; }
}
public LongInteger TagIndex
{
  get { return _tagIndex; }
  set { _tagIndex = value; }
}
public PredictedResourceBlock()
{

}
public void Read(BinaryReader reader)
{
  _type.Read(reader);
  _resourceIndex.Read(reader);
  _tagIndex.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _type.Write(writer);
    _resourceIndex.Write(writer);
    _tagIndex.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class ScenarioFunctionBlock : IBlock
{
private Flags  _flags;	
private FixedLengthString _name = new FixedLengthString();
private Real _period = new Real();
private ShortBlockIndex _scalePeriodBy = new ShortBlockIndex();
private Enum _function = new Enum();
private ShortBlockIndex _scaleFunctionBy = new ShortBlockIndex();
private Enum _wobbleFunction = new Enum();
private Real _wobblePeriod = new Real();
private Real _wobbleMagnitude = new Real();
private RealFraction _squareWaveThreshold = new RealFraction();
private ShortInteger _stepCount = new ShortInteger();
private Enum _mapTo = new Enum();
private ShortInteger _sawtoothCount = new ShortInteger();
private Pad  __unnamed;	
private ShortBlockIndex _scaleResultBy = new ShortBlockIndex();
private Enum _boundsMode = new Enum();
private RealFractionBounds _bounds = new RealFractionBounds();
private Pad  __unnamed2;	
private Pad  __unnamed3;	
private ShortBlockIndex _turnOffWith = new ShortBlockIndex();
private Pad  __unnamed4;	
private Pad  __unnamed5;	
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public Real Period
{
  get { return _period; }
  set { _period = value; }
}
public ShortBlockIndex ScalePeriodBy
{
  get { return _scalePeriodBy; }
  set { _scalePeriodBy = value; }
}
public Enum Function
{
  get { return _function; }
  set { _function = value; }
}
public ShortBlockIndex ScaleFunctionBy
{
  get { return _scaleFunctionBy; }
  set { _scaleFunctionBy = value; }
}
public Enum WobbleFunction
{
  get { return _wobbleFunction; }
  set { _wobbleFunction = value; }
}
public Real WobblePeriod
{
  get { return _wobblePeriod; }
  set { _wobblePeriod = value; }
}
public Real WobbleMagnitude
{
  get { return _wobbleMagnitude; }
  set { _wobbleMagnitude = value; }
}
public RealFraction SquareWaveThreshold
{
  get { return _squareWaveThreshold; }
  set { _squareWaveThreshold = value; }
}
public ShortInteger StepCount
{
  get { return _stepCount; }
  set { _stepCount = value; }
}
public Enum MapTo
{
  get { return _mapTo; }
  set { _mapTo = value; }
}
public ShortInteger SawtoothCount
{
  get { return _sawtoothCount; }
  set { _sawtoothCount = value; }
}
public ShortBlockIndex ScaleResultBy
{
  get { return _scaleResultBy; }
  set { _scaleResultBy = value; }
}
public Enum BoundsMode
{
  get { return _boundsMode; }
  set { _boundsMode = value; }
}
public RealFractionBounds Bounds
{
  get { return _bounds; }
  set { _bounds = value; }
}
public ShortBlockIndex TurnOffWith
{
  get { return _turnOffWith; }
  set { _turnOffWith = value; }
}
public ScenarioFunctionBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(2);
__unnamed2 = new Pad(4);
__unnamed3 = new Pad(2);
__unnamed4 = new Pad(16);
__unnamed5 = new Pad(16);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _name.Read(reader);
  _period.Read(reader);
  _scalePeriodBy.Read(reader);
  _function.Read(reader);
  _scaleFunctionBy.Read(reader);
  _wobbleFunction.Read(reader);
  _wobblePeriod.Read(reader);
  _wobbleMagnitude.Read(reader);
  _squareWaveThreshold.Read(reader);
  _stepCount.Read(reader);
  _mapTo.Read(reader);
  _sawtoothCount.Read(reader);
  __unnamed.Read(reader);
  _scaleResultBy.Read(reader);
  _boundsMode.Read(reader);
  _bounds.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  _turnOffWith.Read(reader);
  __unnamed4.Read(reader);
  __unnamed5.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _name.Write(writer);
    _period.Write(writer);
    _scalePeriodBy.Write(writer);
    _function.Write(writer);
    _scaleFunctionBy.Write(writer);
    _wobbleFunction.Write(writer);
    _wobblePeriod.Write(writer);
    _wobbleMagnitude.Write(writer);
    _squareWaveThreshold.Write(writer);
    _stepCount.Write(writer);
    _mapTo.Write(writer);
    _sawtoothCount.Write(writer);
    __unnamed.Write(writer);
    _scaleResultBy.Write(writer);
    _boundsMode.Write(writer);
    _bounds.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    _turnOffWith.Write(writer);
    __unnamed4.Write(writer);
    __unnamed5.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class EditorCommentBlock : IBlock
{
private RealPoint3D _position = new RealPoint3D();
private Pad  __unnamed;	
private Data _comment = new Data();
public RealPoint3D Position
{
  get { return _position; }
  set { _position = value; }
}
public Data Comment
{
  get { return _comment; }
  set { _comment = value; }
}
public EditorCommentBlock()
{
__unnamed = new Pad(16);

}
public void Read(BinaryReader reader)
{
  _position.Read(reader);
  __unnamed.Read(reader);
  _comment.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_comment.ReadBinary(reader);
}
public void Write(BinaryWriter writer)
{
    _position.Write(writer);
    __unnamed.Write(writer);
    _comment.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_comment.WriteBinary(writer);
}
}
public class ScenarioObjectNamesBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private Pad  __unnamed;	
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public ScenarioObjectNamesBlock()
{
__unnamed = new Pad(4);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class ScenarioSceneryBlock : IBlock
{
private ShortBlockIndex _type = new ShortBlockIndex();
private ShortBlockIndex _name = new ShortBlockIndex();
private Flags  _notPlaced;	
private ShortInteger _desiredPermutation = new ShortInteger();
private RealPoint3D _position = new RealPoint3D();
private RealEulerAngles3D _rotation = new RealEulerAngles3D();
private Pad  __unnamed;	
private Pad  __unnamed2;	
private Pad  __unnamed3;	
private Pad  __unnamed4;	
public ShortBlockIndex Type
{
  get { return _type; }
  set { _type = value; }
}
public ShortBlockIndex Name
{
  get { return _name; }
  set { _name = value; }
}
public Flags NotPlaced
{
  get { return _notPlaced; }
  set { _notPlaced = value; }
}
public ShortInteger DesiredPermutation
{
  get { return _desiredPermutation; }
  set { _desiredPermutation = value; }
}
public RealPoint3D Position
{
  get { return _position; }
  set { _position = value; }
}
public RealEulerAngles3D Rotation
{
  get { return _rotation; }
  set { _rotation = value; }
}
public ScenarioSceneryBlock()
{
_notPlaced = new Flags(2);
__unnamed = new Pad(8);
__unnamed2 = new Pad(16);
__unnamed3 = new Pad(8);
__unnamed4 = new Pad(8);

}
public void Read(BinaryReader reader)
{
  _type.Read(reader);
  _name.Read(reader);
  _notPlaced.Read(reader);
  _desiredPermutation.Read(reader);
  _position.Read(reader);
  _rotation.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  __unnamed4.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _type.Write(writer);
    _name.Write(writer);
    _notPlaced.Write(writer);
    _desiredPermutation.Write(writer);
    _position.Write(writer);
    _rotation.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    __unnamed4.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class ScenarioSceneryPaletteBlock : IBlock
{
private TagReference _name = new TagReference();
private Pad  __unnamed;	
public TagReference Name
{
  get { return _name; }
  set { _name = value; }
}
public ScenarioSceneryPaletteBlock()
{
__unnamed = new Pad(32);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_name.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_name.WriteString(writer);
}
}
public class ScenarioBipedBlock : IBlock
{
private ShortBlockIndex _type = new ShortBlockIndex();
private ShortBlockIndex _name = new ShortBlockIndex();
private Flags  _notPlaced;	
private ShortInteger _desiredPermutation = new ShortInteger();
private RealPoint3D _position = new RealPoint3D();
private RealEulerAngles3D _rotation = new RealEulerAngles3D();
private Pad  __unnamed;	
private Pad  __unnamed2;	
private Pad  __unnamed3;	
private Pad  __unnamed4;	
private Real _bodyVitality = new Real();
private Flags  _flags;	
private Pad  __unnamed5;	
private Pad  __unnamed6;	
public ShortBlockIndex Type
{
  get { return _type; }
  set { _type = value; }
}
public ShortBlockIndex Name
{
  get { return _name; }
  set { _name = value; }
}
public Flags NotPlaced
{
  get { return _notPlaced; }
  set { _notPlaced = value; }
}
public ShortInteger DesiredPermutation
{
  get { return _desiredPermutation; }
  set { _desiredPermutation = value; }
}
public RealPoint3D Position
{
  get { return _position; }
  set { _position = value; }
}
public RealEulerAngles3D Rotation
{
  get { return _rotation; }
  set { _rotation = value; }
}
public Real BodyVitality
{
  get { return _bodyVitality; }
  set { _bodyVitality = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public ScenarioBipedBlock()
{
_notPlaced = new Flags(2);
__unnamed = new Pad(8);
__unnamed2 = new Pad(16);
__unnamed3 = new Pad(8);
__unnamed4 = new Pad(8);
_flags = new Flags(4);
__unnamed5 = new Pad(8);
__unnamed6 = new Pad(32);

}
public void Read(BinaryReader reader)
{
  _type.Read(reader);
  _name.Read(reader);
  _notPlaced.Read(reader);
  _desiredPermutation.Read(reader);
  _position.Read(reader);
  _rotation.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  __unnamed4.Read(reader);
  _bodyVitality.Read(reader);
  _flags.Read(reader);
  __unnamed5.Read(reader);
  __unnamed6.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _type.Write(writer);
    _name.Write(writer);
    _notPlaced.Write(writer);
    _desiredPermutation.Write(writer);
    _position.Write(writer);
    _rotation.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    __unnamed4.Write(writer);
    _bodyVitality.Write(writer);
    _flags.Write(writer);
    __unnamed5.Write(writer);
    __unnamed6.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class ScenarioBipedPaletteBlock : IBlock
{
private TagReference _name = new TagReference();
private Pad  __unnamed;	
public TagReference Name
{
  get { return _name; }
  set { _name = value; }
}
public ScenarioBipedPaletteBlock()
{
__unnamed = new Pad(32);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_name.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_name.WriteString(writer);
}
}
public class ScenarioVehicleBlock : IBlock
{
private ShortBlockIndex _type = new ShortBlockIndex();
private ShortBlockIndex _name = new ShortBlockIndex();
private Flags  _notPlaced;	
private ShortInteger _desiredPermutation = new ShortInteger();
private RealPoint3D _position = new RealPoint3D();
private RealEulerAngles3D _rotation = new RealEulerAngles3D();
private Pad  __unnamed;	
private Pad  __unnamed2;	
private Pad  __unnamed3;	
private Pad  __unnamed4;	
private Real _bodyVitality = new Real();
private Flags  _flags;	
private Pad  __unnamed5;	
private CharInteger _multiplayerTeamIndex = new CharInteger();
private Pad  __unnamed6;	
private Flags  _multiplayerSpawnFlags;	
private Pad  __unnamed7;	
public ShortBlockIndex Type
{
  get { return _type; }
  set { _type = value; }
}
public ShortBlockIndex Name
{
  get { return _name; }
  set { _name = value; }
}
public Flags NotPlaced
{
  get { return _notPlaced; }
  set { _notPlaced = value; }
}
public ShortInteger DesiredPermutation
{
  get { return _desiredPermutation; }
  set { _desiredPermutation = value; }
}
public RealPoint3D Position
{
  get { return _position; }
  set { _position = value; }
}
public RealEulerAngles3D Rotation
{
  get { return _rotation; }
  set { _rotation = value; }
}
public Real BodyVitality
{
  get { return _bodyVitality; }
  set { _bodyVitality = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public CharInteger MultiplayerTeamIndex
{
  get { return _multiplayerTeamIndex; }
  set { _multiplayerTeamIndex = value; }
}
public Flags MultiplayerSpawnFlags
{
  get { return _multiplayerSpawnFlags; }
  set { _multiplayerSpawnFlags = value; }
}
public ScenarioVehicleBlock()
{
_notPlaced = new Flags(2);
__unnamed = new Pad(8);
__unnamed2 = new Pad(16);
__unnamed3 = new Pad(8);
__unnamed4 = new Pad(8);
_flags = new Flags(4);
__unnamed5 = new Pad(8);
__unnamed6 = new Pad(1);
_multiplayerSpawnFlags = new Flags(2);
__unnamed7 = new Pad(28);

}
public void Read(BinaryReader reader)
{
  _type.Read(reader);
  _name.Read(reader);
  _notPlaced.Read(reader);
  _desiredPermutation.Read(reader);
  _position.Read(reader);
  _rotation.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  __unnamed4.Read(reader);
  _bodyVitality.Read(reader);
  _flags.Read(reader);
  __unnamed5.Read(reader);
  _multiplayerTeamIndex.Read(reader);
  __unnamed6.Read(reader);
  _multiplayerSpawnFlags.Read(reader);
  __unnamed7.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _type.Write(writer);
    _name.Write(writer);
    _notPlaced.Write(writer);
    _desiredPermutation.Write(writer);
    _position.Write(writer);
    _rotation.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    __unnamed4.Write(writer);
    _bodyVitality.Write(writer);
    _flags.Write(writer);
    __unnamed5.Write(writer);
    _multiplayerTeamIndex.Write(writer);
    __unnamed6.Write(writer);
    _multiplayerSpawnFlags.Write(writer);
    __unnamed7.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class ScenarioVehiclePaletteBlock : IBlock
{
private TagReference _name = new TagReference();
private Pad  __unnamed;	
public TagReference Name
{
  get { return _name; }
  set { _name = value; }
}
public ScenarioVehiclePaletteBlock()
{
__unnamed = new Pad(32);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_name.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_name.WriteString(writer);
}
}
public class ScenarioEquipmentBlock : IBlock
{
private ShortBlockIndex _type = new ShortBlockIndex();
private ShortBlockIndex _name = new ShortBlockIndex();
private Flags  _notPlaced;	
private ShortInteger _desiredPermutation = new ShortInteger();
private RealPoint3D _position = new RealPoint3D();
private RealEulerAngles3D _rotation = new RealEulerAngles3D();
private Pad  __unnamed;	
private Flags  _miscFlags;	
private Pad  __unnamed2;	
public ShortBlockIndex Type
{
  get { return _type; }
  set { _type = value; }
}
public ShortBlockIndex Name
{
  get { return _name; }
  set { _name = value; }
}
public Flags NotPlaced
{
  get { return _notPlaced; }
  set { _notPlaced = value; }
}
public ShortInteger DesiredPermutation
{
  get { return _desiredPermutation; }
  set { _desiredPermutation = value; }
}
public RealPoint3D Position
{
  get { return _position; }
  set { _position = value; }
}
public RealEulerAngles3D Rotation
{
  get { return _rotation; }
  set { _rotation = value; }
}
public Flags MiscFlags
{
  get { return _miscFlags; }
  set { _miscFlags = value; }
}
public ScenarioEquipmentBlock()
{
_notPlaced = new Flags(2);
__unnamed = new Pad(2);
_miscFlags = new Flags(2);
__unnamed2 = new Pad(4);

}
public void Read(BinaryReader reader)
{
  _type.Read(reader);
  _name.Read(reader);
  _notPlaced.Read(reader);
  _desiredPermutation.Read(reader);
  _position.Read(reader);
  _rotation.Read(reader);
  __unnamed.Read(reader);
  _miscFlags.Read(reader);
  __unnamed2.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _type.Write(writer);
    _name.Write(writer);
    _notPlaced.Write(writer);
    _desiredPermutation.Write(writer);
    _position.Write(writer);
    _rotation.Write(writer);
    __unnamed.Write(writer);
    _miscFlags.Write(writer);
    __unnamed2.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class ScenarioEquipmentPaletteBlock : IBlock
{
private TagReference _name = new TagReference();
private Pad  __unnamed;	
public TagReference Name
{
  get { return _name; }
  set { _name = value; }
}
public ScenarioEquipmentPaletteBlock()
{
__unnamed = new Pad(32);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_name.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_name.WriteString(writer);
}
}
public class ScenarioWeaponBlock : IBlock
{
private ShortBlockIndex _type = new ShortBlockIndex();
private ShortBlockIndex _name = new ShortBlockIndex();
private Flags  _notPlaced;	
private ShortInteger _desiredPermutation = new ShortInteger();
private RealPoint3D _position = new RealPoint3D();
private RealEulerAngles3D _rotation = new RealEulerAngles3D();
private Pad  __unnamed;	
private Pad  __unnamed2;	
private Pad  __unnamed3;	
private Pad  __unnamed4;	
private ShortInteger _roundsLeft = new ShortInteger();
private ShortInteger _roundsLoaded = new ShortInteger();
private Flags  _flags;	
private Pad  __unnamed5;	
private Pad  __unnamed6;	
public ShortBlockIndex Type
{
  get { return _type; }
  set { _type = value; }
}
public ShortBlockIndex Name
{
  get { return _name; }
  set { _name = value; }
}
public Flags NotPlaced
{
  get { return _notPlaced; }
  set { _notPlaced = value; }
}
public ShortInteger DesiredPermutation
{
  get { return _desiredPermutation; }
  set { _desiredPermutation = value; }
}
public RealPoint3D Position
{
  get { return _position; }
  set { _position = value; }
}
public RealEulerAngles3D Rotation
{
  get { return _rotation; }
  set { _rotation = value; }
}
public ShortInteger RoundsLeft
{
  get { return _roundsLeft; }
  set { _roundsLeft = value; }
}
public ShortInteger RoundsLoaded
{
  get { return _roundsLoaded; }
  set { _roundsLoaded = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public ScenarioWeaponBlock()
{
_notPlaced = new Flags(2);
__unnamed = new Pad(8);
__unnamed2 = new Pad(16);
__unnamed3 = new Pad(8);
__unnamed4 = new Pad(8);
_flags = new Flags(2);
__unnamed5 = new Pad(2);
__unnamed6 = new Pad(12);

}
public void Read(BinaryReader reader)
{
  _type.Read(reader);
  _name.Read(reader);
  _notPlaced.Read(reader);
  _desiredPermutation.Read(reader);
  _position.Read(reader);
  _rotation.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  __unnamed4.Read(reader);
  _roundsLeft.Read(reader);
  _roundsLoaded.Read(reader);
  _flags.Read(reader);
  __unnamed5.Read(reader);
  __unnamed6.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _type.Write(writer);
    _name.Write(writer);
    _notPlaced.Write(writer);
    _desiredPermutation.Write(writer);
    _position.Write(writer);
    _rotation.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    __unnamed4.Write(writer);
    _roundsLeft.Write(writer);
    _roundsLoaded.Write(writer);
    _flags.Write(writer);
    __unnamed5.Write(writer);
    __unnamed6.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class ScenarioWeaponPaletteBlock : IBlock
{
private TagReference _name = new TagReference();
private Pad  __unnamed;	
public TagReference Name
{
  get { return _name; }
  set { _name = value; }
}
public ScenarioWeaponPaletteBlock()
{
__unnamed = new Pad(32);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_name.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_name.WriteString(writer);
}
}
public class DeviceGroupBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private Real _initialValue = new Real();
private Flags  _flags;	
private Pad  __unnamed;	
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public Real InitialValue
{
  get { return _initialValue; }
  set { _initialValue = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public DeviceGroupBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(12);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _initialValue.Read(reader);
  _flags.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _initialValue.Write(writer);
    _flags.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class ScenarioMachineBlock : IBlock
{
private ShortBlockIndex _type = new ShortBlockIndex();
private ShortBlockIndex _name = new ShortBlockIndex();
private Flags  _notPlaced;	
private ShortInteger _desiredPermutation = new ShortInteger();
private RealPoint3D _position = new RealPoint3D();
private RealEulerAngles3D _rotation = new RealEulerAngles3D();
private Pad  __unnamed;	
private ShortBlockIndex _powerGroup = new ShortBlockIndex();
private ShortBlockIndex _positionGroup = new ShortBlockIndex();
private Flags  _flags;	
private Flags  _flags2;	
private Pad  __unnamed2;	
public ShortBlockIndex Type
{
  get { return _type; }
  set { _type = value; }
}
public ShortBlockIndex Name
{
  get { return _name; }
  set { _name = value; }
}
public Flags NotPlaced
{
  get { return _notPlaced; }
  set { _notPlaced = value; }
}
public ShortInteger DesiredPermutation
{
  get { return _desiredPermutation; }
  set { _desiredPermutation = value; }
}
public RealPoint3D Position
{
  get { return _position; }
  set { _position = value; }
}
public RealEulerAngles3D Rotation
{
  get { return _rotation; }
  set { _rotation = value; }
}
public ShortBlockIndex PowerGroup
{
  get { return _powerGroup; }
  set { _powerGroup = value; }
}
public ShortBlockIndex PositionGroup
{
  get { return _positionGroup; }
  set { _positionGroup = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Flags Flags2
{
  get { return _flags2; }
  set { _flags2 = value; }
}
public ScenarioMachineBlock()
{
_notPlaced = new Flags(2);
__unnamed = new Pad(8);
_flags = new Flags(4);
_flags2 = new Flags(4);
__unnamed2 = new Pad(12);

}
public void Read(BinaryReader reader)
{
  _type.Read(reader);
  _name.Read(reader);
  _notPlaced.Read(reader);
  _desiredPermutation.Read(reader);
  _position.Read(reader);
  _rotation.Read(reader);
  __unnamed.Read(reader);
  _powerGroup.Read(reader);
  _positionGroup.Read(reader);
  _flags.Read(reader);
  _flags2.Read(reader);
  __unnamed2.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _type.Write(writer);
    _name.Write(writer);
    _notPlaced.Write(writer);
    _desiredPermutation.Write(writer);
    _position.Write(writer);
    _rotation.Write(writer);
    __unnamed.Write(writer);
    _powerGroup.Write(writer);
    _positionGroup.Write(writer);
    _flags.Write(writer);
    _flags2.Write(writer);
    __unnamed2.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class ScenarioMachinePaletteBlock : IBlock
{
private TagReference _name = new TagReference();
private Pad  __unnamed;	
public TagReference Name
{
  get { return _name; }
  set { _name = value; }
}
public ScenarioMachinePaletteBlock()
{
__unnamed = new Pad(32);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_name.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_name.WriteString(writer);
}
}
public class ScenarioControlBlock : IBlock
{
private ShortBlockIndex _type = new ShortBlockIndex();
private ShortBlockIndex _name = new ShortBlockIndex();
private Flags  _notPlaced;	
private ShortInteger _desiredPermutation = new ShortInteger();
private RealPoint3D _position = new RealPoint3D();
private RealEulerAngles3D _rotation = new RealEulerAngles3D();
private Pad  __unnamed;	
private ShortBlockIndex _powerGroup = new ShortBlockIndex();
private ShortBlockIndex _positionGroup = new ShortBlockIndex();
private Flags  _flags;	
private Flags  _flags2;	
private ShortInteger __unnamed2 = new ShortInteger();
private Pad  __unnamed4;	
private Pad  __unnamed5;	
public ShortBlockIndex Type
{
  get { return _type; }
  set { _type = value; }
}
public ShortBlockIndex Name
{
  get { return _name; }
  set { _name = value; }
}
public Flags NotPlaced
{
  get { return _notPlaced; }
  set { _notPlaced = value; }
}
public ShortInteger DesiredPermutation
{
  get { return _desiredPermutation; }
  set { _desiredPermutation = value; }
}
public RealPoint3D Position
{
  get { return _position; }
  set { _position = value; }
}
public RealEulerAngles3D Rotation
{
  get { return _rotation; }
  set { _rotation = value; }
}
public ShortBlockIndex PowerGroup
{
  get { return _powerGroup; }
  set { _powerGroup = value; }
}
public ShortBlockIndex PositionGroup
{
  get { return _positionGroup; }
  set { _positionGroup = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Flags Flags2
{
  get { return _flags2; }
  set { _flags2 = value; }
}
public ShortInteger _unnamed2
{
  get { return __unnamed2; }
  set { __unnamed2 = value; }
}
public ScenarioControlBlock()
{
_notPlaced = new Flags(2);
__unnamed = new Pad(8);
_flags = new Flags(4);
_flags2 = new Flags(4);
__unnamed4 = new Pad(2);
__unnamed5 = new Pad(8);

}
public void Read(BinaryReader reader)
{
  _type.Read(reader);
  _name.Read(reader);
  _notPlaced.Read(reader);
  _desiredPermutation.Read(reader);
  _position.Read(reader);
  _rotation.Read(reader);
  __unnamed.Read(reader);
  _powerGroup.Read(reader);
  _positionGroup.Read(reader);
  _flags.Read(reader);
  _flags2.Read(reader);
  __unnamed2.Read(reader);
  __unnamed4.Read(reader);
  __unnamed5.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _type.Write(writer);
    _name.Write(writer);
    _notPlaced.Write(writer);
    _desiredPermutation.Write(writer);
    _position.Write(writer);
    _rotation.Write(writer);
    __unnamed.Write(writer);
    _powerGroup.Write(writer);
    _positionGroup.Write(writer);
    _flags.Write(writer);
    _flags2.Write(writer);
    __unnamed2.Write(writer);
    __unnamed4.Write(writer);
    __unnamed5.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class ScenarioControlPaletteBlock : IBlock
{
private TagReference _name = new TagReference();
private Pad  __unnamed;	
public TagReference Name
{
  get { return _name; }
  set { _name = value; }
}
public ScenarioControlPaletteBlock()
{
__unnamed = new Pad(32);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_name.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_name.WriteString(writer);
}
}
public class ScenarioLightFixtureBlock : IBlock
{
private ShortBlockIndex _type = new ShortBlockIndex();
private ShortBlockIndex _name = new ShortBlockIndex();
private Flags  _notPlaced;	
private ShortInteger _desiredPermutation = new ShortInteger();
private RealPoint3D _position = new RealPoint3D();
private RealEulerAngles3D _rotation = new RealEulerAngles3D();
private Pad  __unnamed;	
private ShortBlockIndex _powerGroup = new ShortBlockIndex();
private ShortBlockIndex _positionGroup = new ShortBlockIndex();
private Flags  _flags;	
private RealRGBColor _color = new RealRGBColor();
private Real _intensity = new Real();
private Angle _falloffAngle = new Angle();
private Angle _cutoffAngle = new Angle();
private Pad  __unnamed2;	
public ShortBlockIndex Type
{
  get { return _type; }
  set { _type = value; }
}
public ShortBlockIndex Name
{
  get { return _name; }
  set { _name = value; }
}
public Flags NotPlaced
{
  get { return _notPlaced; }
  set { _notPlaced = value; }
}
public ShortInteger DesiredPermutation
{
  get { return _desiredPermutation; }
  set { _desiredPermutation = value; }
}
public RealPoint3D Position
{
  get { return _position; }
  set { _position = value; }
}
public RealEulerAngles3D Rotation
{
  get { return _rotation; }
  set { _rotation = value; }
}
public ShortBlockIndex PowerGroup
{
  get { return _powerGroup; }
  set { _powerGroup = value; }
}
public ShortBlockIndex PositionGroup
{
  get { return _positionGroup; }
  set { _positionGroup = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public RealRGBColor Color
{
  get { return _color; }
  set { _color = value; }
}
public Real Intensity
{
  get { return _intensity; }
  set { _intensity = value; }
}
public Angle FalloffAngle
{
  get { return _falloffAngle; }
  set { _falloffAngle = value; }
}
public Angle CutoffAngle
{
  get { return _cutoffAngle; }
  set { _cutoffAngle = value; }
}
public ScenarioLightFixtureBlock()
{
_notPlaced = new Flags(2);
__unnamed = new Pad(8);
_flags = new Flags(4);
__unnamed2 = new Pad(16);

}
public void Read(BinaryReader reader)
{
  _type.Read(reader);
  _name.Read(reader);
  _notPlaced.Read(reader);
  _desiredPermutation.Read(reader);
  _position.Read(reader);
  _rotation.Read(reader);
  __unnamed.Read(reader);
  _powerGroup.Read(reader);
  _positionGroup.Read(reader);
  _flags.Read(reader);
  _color.Read(reader);
  _intensity.Read(reader);
  _falloffAngle.Read(reader);
  _cutoffAngle.Read(reader);
  __unnamed2.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _type.Write(writer);
    _name.Write(writer);
    _notPlaced.Write(writer);
    _desiredPermutation.Write(writer);
    _position.Write(writer);
    _rotation.Write(writer);
    __unnamed.Write(writer);
    _powerGroup.Write(writer);
    _positionGroup.Write(writer);
    _flags.Write(writer);
    _color.Write(writer);
    _intensity.Write(writer);
    _falloffAngle.Write(writer);
    _cutoffAngle.Write(writer);
    __unnamed2.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class ScenarioLightFixturePaletteBlock : IBlock
{
private TagReference _name = new TagReference();
private Pad  __unnamed;	
public TagReference Name
{
  get { return _name; }
  set { _name = value; }
}
public ScenarioLightFixturePaletteBlock()
{
__unnamed = new Pad(32);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_name.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_name.WriteString(writer);
}
}
public class ScenarioSoundSceneryBlock : IBlock
{
private ShortBlockIndex _type = new ShortBlockIndex();
private ShortBlockIndex _name = new ShortBlockIndex();
private Flags  _notPlaced;	
private ShortInteger _desiredPermutation = new ShortInteger();
private RealPoint3D _position = new RealPoint3D();
private RealEulerAngles3D _rotation = new RealEulerAngles3D();
private Pad  __unnamed;	
public ShortBlockIndex Type
{
  get { return _type; }
  set { _type = value; }
}
public ShortBlockIndex Name
{
  get { return _name; }
  set { _name = value; }
}
public Flags NotPlaced
{
  get { return _notPlaced; }
  set { _notPlaced = value; }
}
public ShortInteger DesiredPermutation
{
  get { return _desiredPermutation; }
  set { _desiredPermutation = value; }
}
public RealPoint3D Position
{
  get { return _position; }
  set { _position = value; }
}
public RealEulerAngles3D Rotation
{
  get { return _rotation; }
  set { _rotation = value; }
}
public ScenarioSoundSceneryBlock()
{
_notPlaced = new Flags(2);
__unnamed = new Pad(8);

}
public void Read(BinaryReader reader)
{
  _type.Read(reader);
  _name.Read(reader);
  _notPlaced.Read(reader);
  _desiredPermutation.Read(reader);
  _position.Read(reader);
  _rotation.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _type.Write(writer);
    _name.Write(writer);
    _notPlaced.Write(writer);
    _desiredPermutation.Write(writer);
    _position.Write(writer);
    _rotation.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class ScenarioSoundSceneryPaletteBlock : IBlock
{
private TagReference _name = new TagReference();
private Pad  __unnamed;	
public TagReference Name
{
  get { return _name; }
  set { _name = value; }
}
public ScenarioSoundSceneryPaletteBlock()
{
__unnamed = new Pad(32);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_name.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_name.WriteString(writer);
}
}
public class ScenarioProfilesBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private RealFraction _startingHealthModifier = new RealFraction();
private RealFraction _startingShieldModifier = new RealFraction();
private TagReference _primaryWeapon = new TagReference();
private ShortInteger _roundsLoaded = new ShortInteger();
private ShortInteger _roundsTotal = new ShortInteger();
private TagReference _secondaryWeapon = new TagReference();
private ShortInteger _roundsLoaded2 = new ShortInteger();
private ShortInteger _roundsTotal2 = new ShortInteger();
private CharInteger _startingFragmentationGrenadeCount = new CharInteger();
private CharInteger _startingPlasmaGrenadeCount = new CharInteger();
private CharInteger _startingUnknownGrenadeCount = new CharInteger();
private CharInteger _startingUnknownGrenadeCount2 = new CharInteger();
private Pad  __unnamed;	
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public RealFraction StartingHealthModifier
{
  get { return _startingHealthModifier; }
  set { _startingHealthModifier = value; }
}
public RealFraction StartingShieldModifier
{
  get { return _startingShieldModifier; }
  set { _startingShieldModifier = value; }
}
public TagReference PrimaryWeapon
{
  get { return _primaryWeapon; }
  set { _primaryWeapon = value; }
}
public ShortInteger RoundsLoaded
{
  get { return _roundsLoaded; }
  set { _roundsLoaded = value; }
}
public ShortInteger RoundsTotal
{
  get { return _roundsTotal; }
  set { _roundsTotal = value; }
}
public TagReference SecondaryWeapon
{
  get { return _secondaryWeapon; }
  set { _secondaryWeapon = value; }
}
public ShortInteger RoundsLoaded2
{
  get { return _roundsLoaded2; }
  set { _roundsLoaded2 = value; }
}
public ShortInteger RoundsTotal2
{
  get { return _roundsTotal2; }
  set { _roundsTotal2 = value; }
}
public CharInteger StartingFragmentationGrenadeCount
{
  get { return _startingFragmentationGrenadeCount; }
  set { _startingFragmentationGrenadeCount = value; }
}
public CharInteger StartingPlasmaGrenadeCount
{
  get { return _startingPlasmaGrenadeCount; }
  set { _startingPlasmaGrenadeCount = value; }
}
public CharInteger StartingUnknownGrenadeCount
{
  get { return _startingUnknownGrenadeCount; }
  set { _startingUnknownGrenadeCount = value; }
}
public CharInteger StartingUnknownGrenadeCount2
{
  get { return _startingUnknownGrenadeCount2; }
  set { _startingUnknownGrenadeCount2 = value; }
}
public ScenarioProfilesBlock()
{
__unnamed = new Pad(20);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _startingHealthModifier.Read(reader);
  _startingShieldModifier.Read(reader);
  _primaryWeapon.Read(reader);
  _roundsLoaded.Read(reader);
  _roundsTotal.Read(reader);
  _secondaryWeapon.Read(reader);
  _roundsLoaded2.Read(reader);
  _roundsTotal2.Read(reader);
  _startingFragmentationGrenadeCount.Read(reader);
  _startingPlasmaGrenadeCount.Read(reader);
  _startingUnknownGrenadeCount.Read(reader);
  _startingUnknownGrenadeCount2.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_primaryWeapon.ReadString(reader);
_secondaryWeapon.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _startingHealthModifier.Write(writer);
    _startingShieldModifier.Write(writer);
    _primaryWeapon.Write(writer);
    _roundsLoaded.Write(writer);
    _roundsTotal.Write(writer);
    _secondaryWeapon.Write(writer);
    _roundsLoaded2.Write(writer);
    _roundsTotal2.Write(writer);
    _startingFragmentationGrenadeCount.Write(writer);
    _startingPlasmaGrenadeCount.Write(writer);
    _startingUnknownGrenadeCount.Write(writer);
    _startingUnknownGrenadeCount2.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_primaryWeapon.WriteString(writer);
_secondaryWeapon.WriteString(writer);
}
}
public class ScenarioPlayersBlock : IBlock
{
private RealPoint3D _position = new RealPoint3D();
private Angle _facing = new Angle();
private ShortInteger _teamIndex = new ShortInteger();
private ShortInteger _bspIndex = new ShortInteger();
private Enum _type0 = new Enum();
private Enum _type1 = new Enum();
private Enum _type2 = new Enum();
private Enum _type3 = new Enum();
private Pad  __unnamed;	
public RealPoint3D Position
{
  get { return _position; }
  set { _position = value; }
}
public Angle Facing
{
  get { return _facing; }
  set { _facing = value; }
}
public ShortInteger TeamIndex
{
  get { return _teamIndex; }
  set { _teamIndex = value; }
}
public ShortInteger BspIndex
{
  get { return _bspIndex; }
  set { _bspIndex = value; }
}
public Enum Type0
{
  get { return _type0; }
  set { _type0 = value; }
}
public Enum Type1
{
  get { return _type1; }
  set { _type1 = value; }
}
public Enum Type2
{
  get { return _type2; }
  set { _type2 = value; }
}
public Enum Type3
{
  get { return _type3; }
  set { _type3 = value; }
}
public ScenarioPlayersBlock()
{
__unnamed = new Pad(24);

}
public void Read(BinaryReader reader)
{
  _position.Read(reader);
  _facing.Read(reader);
  _teamIndex.Read(reader);
  _bspIndex.Read(reader);
  _type0.Read(reader);
  _type1.Read(reader);
  _type2.Read(reader);
  _type3.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _position.Write(writer);
    _facing.Write(writer);
    _teamIndex.Write(writer);
    _bspIndex.Write(writer);
    _type0.Write(writer);
    _type1.Write(writer);
    _type2.Write(writer);
    _type3.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class ScenarioTriggerVolumeBlock : IBlock
{
private Skip  __unnamed;	
private FixedLengthString _name = new FixedLengthString();
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public ScenarioTriggerVolumeBlock()
{
__unnamed = new Skip(4);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  _name.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    _name.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class RecordedAnimationBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private CharInteger _version = new CharInteger();
private CharInteger _rawAnimationData = new CharInteger();
private CharInteger _unitControlDataVersion = new CharInteger();
private Pad  __unnamed;	
private ShortInteger _lengthOfAnimation = new ShortInteger();
private Pad  __unnamed2;	
private Pad  __unnamed3;	
private Data _recordedAnimationEventStream = new Data();
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public CharInteger Version
{
  get { return _version; }
  set { _version = value; }
}
public CharInteger RawAnimationData
{
  get { return _rawAnimationData; }
  set { _rawAnimationData = value; }
}
public CharInteger UnitControlDataVersion
{
  get { return _unitControlDataVersion; }
  set { _unitControlDataVersion = value; }
}
public ShortInteger LengthOfAnimation
{
  get { return _lengthOfAnimation; }
  set { _lengthOfAnimation = value; }
}
public Data RecordedAnimationEventStream
{
  get { return _recordedAnimationEventStream; }
  set { _recordedAnimationEventStream = value; }
}
public RecordedAnimationBlock()
{
__unnamed = new Pad(1);
__unnamed2 = new Pad(2);
__unnamed3 = new Pad(4);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _version.Read(reader);
  _rawAnimationData.Read(reader);
  _unitControlDataVersion.Read(reader);
  __unnamed.Read(reader);
  _lengthOfAnimation.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  _recordedAnimationEventStream.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_recordedAnimationEventStream.ReadBinary(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _version.Write(writer);
    _rawAnimationData.Write(writer);
    _unitControlDataVersion.Write(writer);
    __unnamed.Write(writer);
    _lengthOfAnimation.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    _recordedAnimationEventStream.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_recordedAnimationEventStream.WriteBinary(writer);
}
}
public class ScenarioNetgameFlagsBlock : IBlock
{
private RealPoint3D _position = new RealPoint3D();
private Angle _facing = new Angle();
private Enum _type = new Enum();
private ShortInteger _teamIndex = new ShortInteger();
private TagReference _weaponGroup = new TagReference();
private Pad  __unnamed;	
public RealPoint3D Position
{
  get { return _position; }
  set { _position = value; }
}
public Angle Facing
{
  get { return _facing; }
  set { _facing = value; }
}
public Enum Type
{
  get { return _type; }
  set { _type = value; }
}
public ShortInteger TeamIndex
{
  get { return _teamIndex; }
  set { _teamIndex = value; }
}
public TagReference WeaponGroup
{
  get { return _weaponGroup; }
  set { _weaponGroup = value; }
}
public ScenarioNetgameFlagsBlock()
{
__unnamed = new Pad(112);

}
public void Read(BinaryReader reader)
{
  _position.Read(reader);
  _facing.Read(reader);
  _type.Read(reader);
  _teamIndex.Read(reader);
  _weaponGroup.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_weaponGroup.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _position.Write(writer);
    _facing.Write(writer);
    _type.Write(writer);
    _teamIndex.Write(writer);
    _weaponGroup.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_weaponGroup.WriteString(writer);
}
}
public class ScenarioNetgameEquipmentBlock : IBlock
{
private Flags  _flags;	
private Enum _type0 = new Enum();
private Enum _type1 = new Enum();
private Enum _type2 = new Enum();
private Enum _type3 = new Enum();
private ShortInteger _teamIndex = new ShortInteger();
private ShortInteger _spawnTimeInSecond = new ShortInteger();
private Pad  __unnamed;	
private RealPoint3D _position = new RealPoint3D();
private Angle _facing = new Angle();
private TagReference _itemCollection = new TagReference();
private Pad  __unnamed2;	
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Enum Type0
{
  get { return _type0; }
  set { _type0 = value; }
}
public Enum Type1
{
  get { return _type1; }
  set { _type1 = value; }
}
public Enum Type2
{
  get { return _type2; }
  set { _type2 = value; }
}
public Enum Type3
{
  get { return _type3; }
  set { _type3 = value; }
}
public ShortInteger TeamIndex
{
  get { return _teamIndex; }
  set { _teamIndex = value; }
}
public ShortInteger SpawnTimeInSecond
{
  get { return _spawnTimeInSecond; }
  set { _spawnTimeInSecond = value; }
}
public RealPoint3D Position
{
  get { return _position; }
  set { _position = value; }
}
public Angle Facing
{
  get { return _facing; }
  set { _facing = value; }
}
public TagReference ItemCollection
{
  get { return _itemCollection; }
  set { _itemCollection = value; }
}
public ScenarioNetgameEquipmentBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(48);
__unnamed2 = new Pad(48);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _type0.Read(reader);
  _type1.Read(reader);
  _type2.Read(reader);
  _type3.Read(reader);
  _teamIndex.Read(reader);
  _spawnTimeInSecond.Read(reader);
  __unnamed.Read(reader);
  _position.Read(reader);
  _facing.Read(reader);
  _itemCollection.Read(reader);
  __unnamed2.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_itemCollection.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _type0.Write(writer);
    _type1.Write(writer);
    _type2.Write(writer);
    _type3.Write(writer);
    _teamIndex.Write(writer);
    _spawnTimeInSecond.Write(writer);
    __unnamed.Write(writer);
    _position.Write(writer);
    _facing.Write(writer);
    _itemCollection.Write(writer);
    __unnamed2.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_itemCollection.WriteString(writer);
}
}
public class ScenarioStartingEquipmentBlock : IBlock
{
private Flags  _flags;	
private Enum _type0 = new Enum();
private Enum _type1 = new Enum();
private Enum _type2 = new Enum();
private Enum _type3 = new Enum();
private Pad  __unnamed;	
private TagReference _itemCollection1 = new TagReference();
private TagReference _itemCollection2 = new TagReference();
private TagReference _itemCollection3 = new TagReference();
private TagReference _itemCollection4 = new TagReference();
private TagReference _itemCollection5 = new TagReference();
private TagReference _itemCollection6 = new TagReference();
private Pad  __unnamed2;	
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Enum Type0
{
  get { return _type0; }
  set { _type0 = value; }
}
public Enum Type1
{
  get { return _type1; }
  set { _type1 = value; }
}
public Enum Type2
{
  get { return _type2; }
  set { _type2 = value; }
}
public Enum Type3
{
  get { return _type3; }
  set { _type3 = value; }
}
public TagReference ItemCollection1
{
  get { return _itemCollection1; }
  set { _itemCollection1 = value; }
}
public TagReference ItemCollection2
{
  get { return _itemCollection2; }
  set { _itemCollection2 = value; }
}
public TagReference ItemCollection3
{
  get { return _itemCollection3; }
  set { _itemCollection3 = value; }
}
public TagReference ItemCollection4
{
  get { return _itemCollection4; }
  set { _itemCollection4 = value; }
}
public TagReference ItemCollection5
{
  get { return _itemCollection5; }
  set { _itemCollection5 = value; }
}
public TagReference ItemCollection6
{
  get { return _itemCollection6; }
  set { _itemCollection6 = value; }
}
public ScenarioStartingEquipmentBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(48);
__unnamed2 = new Pad(48);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _type0.Read(reader);
  _type1.Read(reader);
  _type2.Read(reader);
  _type3.Read(reader);
  __unnamed.Read(reader);
  _itemCollection1.Read(reader);
  _itemCollection2.Read(reader);
  _itemCollection3.Read(reader);
  _itemCollection4.Read(reader);
  _itemCollection5.Read(reader);
  _itemCollection6.Read(reader);
  __unnamed2.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_itemCollection1.ReadString(reader);
_itemCollection2.ReadString(reader);
_itemCollection3.ReadString(reader);
_itemCollection4.ReadString(reader);
_itemCollection5.ReadString(reader);
_itemCollection6.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _type0.Write(writer);
    _type1.Write(writer);
    _type2.Write(writer);
    _type3.Write(writer);
    __unnamed.Write(writer);
    _itemCollection1.Write(writer);
    _itemCollection2.Write(writer);
    _itemCollection3.Write(writer);
    _itemCollection4.Write(writer);
    _itemCollection5.Write(writer);
    _itemCollection6.Write(writer);
    __unnamed2.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_itemCollection1.WriteString(writer);
_itemCollection2.WriteString(writer);
_itemCollection3.WriteString(writer);
_itemCollection4.WriteString(writer);
_itemCollection5.WriteString(writer);
_itemCollection6.WriteString(writer);
}
}
public class ScenarioBspSwitchTriggerVolumeBlock : IBlock
{
private ShortBlockIndex _triggerVolume = new ShortBlockIndex();
private ShortInteger _source = new ShortInteger();
private ShortInteger _destination = new ShortInteger();
private Pad  __unnamed;	
public ShortBlockIndex TriggerVolume
{
  get { return _triggerVolume; }
  set { _triggerVolume = value; }
}
public ShortInteger Source
{
  get { return _source; }
  set { _source = value; }
}
public ShortInteger Destination
{
  get { return _destination; }
  set { _destination = value; }
}
public ScenarioBspSwitchTriggerVolumeBlock()
{
__unnamed = new Pad(2);

}
public void Read(BinaryReader reader)
{
  _triggerVolume.Read(reader);
  _source.Read(reader);
  _destination.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _triggerVolume.Write(writer);
    _source.Write(writer);
    _destination.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class ScenarioDecalsBlock : IBlock
{
private ShortBlockIndex _decalType = new ShortBlockIndex();
private CharInteger _ya = new CharInteger();
private CharInteger _pitc = new CharInteger();
private RealPoint3D _position = new RealPoint3D();
public ShortBlockIndex DecalType
{
  get { return _decalType; }
  set { _decalType = value; }
}
public CharInteger Ya
{
  get { return _ya; }
  set { _ya = value; }
}
public CharInteger Pitc
{
  get { return _pitc; }
  set { _pitc = value; }
}
public RealPoint3D Position
{
  get { return _position; }
  set { _position = value; }
}
public ScenarioDecalsBlock()
{

}
public void Read(BinaryReader reader)
{
  _decalType.Read(reader);
  _ya.Read(reader);
  _pitc.Read(reader);
  _position.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _decalType.Write(writer);
    _ya.Write(writer);
    _pitc.Write(writer);
    _position.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class ScenarioDecalPaletteBlock : IBlock
{
private TagReference _reference = new TagReference();
public TagReference Reference
{
  get { return _reference; }
  set { _reference = value; }
}
public ScenarioDecalPaletteBlock()
{

}
public void Read(BinaryReader reader)
{
  _reference.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_reference.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _reference.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_reference.WriteString(writer);
}
}
public class ScenarioDetailObjectCollectionPaletteBlock : IBlock
{
private TagReference _name = new TagReference();
private Pad  __unnamed;	
public TagReference Name
{
  get { return _name; }
  set { _name = value; }
}
public ScenarioDetailObjectCollectionPaletteBlock()
{
__unnamed = new Pad(32);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_name.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_name.WriteString(writer);
}
}
public class ActorPaletteBlock : IBlock
{
private TagReference _reference = new TagReference();
public TagReference Reference
{
  get { return _reference; }
  set { _reference = value; }
}
public ActorPaletteBlock()
{

}
public void Read(BinaryReader reader)
{
  _reference.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_reference.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _reference.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_reference.WriteString(writer);
}
}
public class EncounterBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private Flags  _flags;	
private Enum _teamIndex = new Enum();
private Skip  __unnamed;	
private Enum _searchBehavior = new Enum();
private ShortInteger _manualBspIndex = new ShortInteger();
private RealBounds _respawnDelay = new RealBounds();
private Pad  __unnamed2;	
private Block _squads = new Block();
private Block _platoons = new Block();
private Block _firingPositions = new Block();
private Block _playerStartingLocations = new Block();
public class SquadsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public SquadsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(SquadsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new SquadsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public SquadsBlock this[int index]
  {
    get { return (InnerList[index] as SquadsBlock); }
  }
}
private SquadsBlockCollection _squadsCollection;
public SquadsBlockCollection Squads
{
  get { return _squadsCollection; }
}
public class PlatoonsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public PlatoonsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(PlatoonsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new PlatoonsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public PlatoonsBlock this[int index]
  {
    get { return (InnerList[index] as PlatoonsBlock); }
  }
}
private PlatoonsBlockCollection _platoonsCollection;
public PlatoonsBlockCollection Platoons
{
  get { return _platoonsCollection; }
}
public class FiringPositionsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public FiringPositionsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(FiringPositionsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new FiringPositionsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public FiringPositionsBlock this[int index]
  {
    get { return (InnerList[index] as FiringPositionsBlock); }
  }
}
private FiringPositionsBlockCollection _firingPositionsCollection;
public FiringPositionsBlockCollection FiringPositions
{
  get { return _firingPositionsCollection; }
}
public class ScenarioPlayersBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioPlayersBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioPlayersBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioPlayersBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioPlayersBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioPlayersBlock); }
  }
}
private ScenarioPlayersBlockCollection _playerStartingLocationsCollection;
public ScenarioPlayersBlockCollection PlayerStartingLocations
{
  get { return _playerStartingLocationsCollection; }
}
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Enum TeamIndex
{
  get { return _teamIndex; }
  set { _teamIndex = value; }
}
public Enum SearchBehavior
{
  get { return _searchBehavior; }
  set { _searchBehavior = value; }
}
public ShortInteger ManualBspIndex
{
  get { return _manualBspIndex; }
  set { _manualBspIndex = value; }
}
public RealBounds RespawnDelay
{
  get { return _respawnDelay; }
  set { _respawnDelay = value; }
}
public EncounterBlock()
{
_flags = new Flags(4);
__unnamed = new Skip(2);
__unnamed2 = new Pad(76);
_squadsCollection = new SquadsBlockCollection(_squads);
_platoonsCollection = new PlatoonsBlockCollection(_platoons);
_firingPositionsCollection = new FiringPositionsBlockCollection(_firingPositions);
_playerStartingLocationsCollection = new ScenarioPlayersBlockCollection(_playerStartingLocations);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _flags.Read(reader);
  _teamIndex.Read(reader);
  __unnamed.Read(reader);
  _searchBehavior.Read(reader);
  _manualBspIndex.Read(reader);
  _respawnDelay.Read(reader);
  __unnamed2.Read(reader);
  _squads.Read(reader);
  _platoons.Read(reader);
  _firingPositions.Read(reader);
  _playerStartingLocations.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_squads.Count; x++)
{
  Squads.AddNew();
  Squads[x].Read(reader);
}
for (int x=0; x<_squads.Count; x++)
  Squads[x].ReadChildData(reader);
for (int x=0; x<_platoons.Count; x++)
{
  Platoons.AddNew();
  Platoons[x].Read(reader);
}
for (int x=0; x<_platoons.Count; x++)
  Platoons[x].ReadChildData(reader);
for (int x=0; x<_firingPositions.Count; x++)
{
  FiringPositions.AddNew();
  FiringPositions[x].Read(reader);
}
for (int x=0; x<_firingPositions.Count; x++)
  FiringPositions[x].ReadChildData(reader);
for (int x=0; x<_playerStartingLocations.Count; x++)
{
  PlayerStartingLocations.AddNew();
  PlayerStartingLocations[x].Read(reader);
}
for (int x=0; x<_playerStartingLocations.Count; x++)
  PlayerStartingLocations[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _flags.Write(writer);
    _teamIndex.Write(writer);
    __unnamed.Write(writer);
    _searchBehavior.Write(writer);
    _manualBspIndex.Write(writer);
    _respawnDelay.Write(writer);
    __unnamed2.Write(writer);
    _squads.Write(writer);
    _platoons.Write(writer);
    _firingPositions.Write(writer);
    _playerStartingLocations.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_squads.UpdateReflexiveOffset(writer);
for (int x=0; x<_squads.Count; x++)
{
  Squads[x].Write(writer);
}
for (int x=0; x<_squads.Count; x++)
  Squads[x].WriteChildData(writer);
_platoons.UpdateReflexiveOffset(writer);
for (int x=0; x<_platoons.Count; x++)
{
  Platoons[x].Write(writer);
}
for (int x=0; x<_platoons.Count; x++)
  Platoons[x].WriteChildData(writer);
_firingPositions.UpdateReflexiveOffset(writer);
for (int x=0; x<_firingPositions.Count; x++)
{
  FiringPositions[x].Write(writer);
}
for (int x=0; x<_firingPositions.Count; x++)
  FiringPositions[x].WriteChildData(writer);
_playerStartingLocations.UpdateReflexiveOffset(writer);
for (int x=0; x<_playerStartingLocations.Count; x++)
{
  PlayerStartingLocations[x].Write(writer);
}
for (int x=0; x<_playerStartingLocations.Count; x++)
  PlayerStartingLocations[x].WriteChildData(writer);
}
}
public class SquadsBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private ShortBlockIndex _actorType = new ShortBlockIndex();
private ShortBlockIndex _platoon = new ShortBlockIndex();
private Enum _initialState = new Enum();
private Enum _returnState = new Enum();
private Flags  _flags;	
private Enum _uniqueLeaderType = new Enum();
private Pad  __unnamed;	
private Pad  __unnamed2;	
private Pad  __unnamed3;	
private ShortBlockIndex _maneuverToSquad = new ShortBlockIndex();
private Real _squadDelayTime = new Real();
private Flags  _attacking;	
private Flags  _attackingSearch;	
private Flags  _attackingGuard;	
private Flags  _defending;	
private Flags  _defendingSearch;	
private Flags  _defendingGuard;	
private Flags  _pursuing;	
private Pad  __unnamed4;	
private Pad  __unnamed5;	
private ShortInteger _normalDiffCount = new ShortInteger();
private ShortInteger _insaneDiffCount = new ShortInteger();
private Enum _majorUpgrade = new Enum();
private Pad  __unnamed6;	
private ShortInteger _respawnMinActors = new ShortInteger();
private ShortInteger _respawnMaxActors = new ShortInteger();
private ShortInteger _respawnTotal = new ShortInteger();
private Pad  __unnamed7;	
private RealBounds _respawnDelay = new RealBounds();
private Pad  __unnamed8;	
private Block _movePositions = new Block();
private Block _startingLocations = new Block();
private Pad  __unnamed9;	
public class MovePositionsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public MovePositionsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(MovePositionsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new MovePositionsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public MovePositionsBlock this[int index]
  {
    get { return (InnerList[index] as MovePositionsBlock); }
  }
}
private MovePositionsBlockCollection _movePositionsCollection;
public MovePositionsBlockCollection MovePositions
{
  get { return _movePositionsCollection; }
}
public class ActorStartingLocationsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ActorStartingLocationsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ActorStartingLocationsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ActorStartingLocationsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ActorStartingLocationsBlock this[int index]
  {
    get { return (InnerList[index] as ActorStartingLocationsBlock); }
  }
}
private ActorStartingLocationsBlockCollection _startingLocationsCollection;
public ActorStartingLocationsBlockCollection StartingLocations
{
  get { return _startingLocationsCollection; }
}
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public ShortBlockIndex ActorType
{
  get { return _actorType; }
  set { _actorType = value; }
}
public ShortBlockIndex Platoon
{
  get { return _platoon; }
  set { _platoon = value; }
}
public Enum InitialState
{
  get { return _initialState; }
  set { _initialState = value; }
}
public Enum ReturnState
{
  get { return _returnState; }
  set { _returnState = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Enum UniqueLeaderType
{
  get { return _uniqueLeaderType; }
  set { _uniqueLeaderType = value; }
}
public ShortBlockIndex ManeuverToSquad
{
  get { return _maneuverToSquad; }
  set { _maneuverToSquad = value; }
}
public Real SquadDelayTime
{
  get { return _squadDelayTime; }
  set { _squadDelayTime = value; }
}
public Flags Attacking
{
  get { return _attacking; }
  set { _attacking = value; }
}
public Flags AttackingSearch
{
  get { return _attackingSearch; }
  set { _attackingSearch = value; }
}
public Flags AttackingGuard
{
  get { return _attackingGuard; }
  set { _attackingGuard = value; }
}
public Flags Defending
{
  get { return _defending; }
  set { _defending = value; }
}
public Flags DefendingSearch
{
  get { return _defendingSearch; }
  set { _defendingSearch = value; }
}
public Flags DefendingGuard
{
  get { return _defendingGuard; }
  set { _defendingGuard = value; }
}
public Flags Pursuing
{
  get { return _pursuing; }
  set { _pursuing = value; }
}
public ShortInteger NormalDiffCount
{
  get { return _normalDiffCount; }
  set { _normalDiffCount = value; }
}
public ShortInteger InsaneDiffCount
{
  get { return _insaneDiffCount; }
  set { _insaneDiffCount = value; }
}
public Enum MajorUpgrade
{
  get { return _majorUpgrade; }
  set { _majorUpgrade = value; }
}
public ShortInteger RespawnMinActors
{
  get { return _respawnMinActors; }
  set { _respawnMinActors = value; }
}
public ShortInteger RespawnMaxActors
{
  get { return _respawnMaxActors; }
  set { _respawnMaxActors = value; }
}
public ShortInteger RespawnTotal
{
  get { return _respawnTotal; }
  set { _respawnTotal = value; }
}
public RealBounds RespawnDelay
{
  get { return _respawnDelay; }
  set { _respawnDelay = value; }
}
public SquadsBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(2);
__unnamed2 = new Pad(28);
__unnamed3 = new Pad(2);
_attacking = new Flags(4);
_attackingSearch = new Flags(4);
_attackingGuard = new Flags(4);
_defending = new Flags(4);
_defendingSearch = new Flags(4);
_defendingGuard = new Flags(4);
_pursuing = new Flags(4);
__unnamed4 = new Pad(4);
__unnamed5 = new Pad(8);
__unnamed6 = new Pad(2);
__unnamed7 = new Pad(2);
__unnamed8 = new Pad(48);
__unnamed9 = new Pad(12);
_movePositionsCollection = new MovePositionsBlockCollection(_movePositions);
_startingLocationsCollection = new ActorStartingLocationsBlockCollection(_startingLocations);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _actorType.Read(reader);
  _platoon.Read(reader);
  _initialState.Read(reader);
  _returnState.Read(reader);
  _flags.Read(reader);
  _uniqueLeaderType.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  _maneuverToSquad.Read(reader);
  _squadDelayTime.Read(reader);
  _attacking.Read(reader);
  _attackingSearch.Read(reader);
  _attackingGuard.Read(reader);
  _defending.Read(reader);
  _defendingSearch.Read(reader);
  _defendingGuard.Read(reader);
  _pursuing.Read(reader);
  __unnamed4.Read(reader);
  __unnamed5.Read(reader);
  _normalDiffCount.Read(reader);
  _insaneDiffCount.Read(reader);
  _majorUpgrade.Read(reader);
  __unnamed6.Read(reader);
  _respawnMinActors.Read(reader);
  _respawnMaxActors.Read(reader);
  _respawnTotal.Read(reader);
  __unnamed7.Read(reader);
  _respawnDelay.Read(reader);
  __unnamed8.Read(reader);
  _movePositions.Read(reader);
  _startingLocations.Read(reader);
  __unnamed9.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_movePositions.Count; x++)
{
  MovePositions.AddNew();
  MovePositions[x].Read(reader);
}
for (int x=0; x<_movePositions.Count; x++)
  MovePositions[x].ReadChildData(reader);
for (int x=0; x<_startingLocations.Count; x++)
{
  StartingLocations.AddNew();
  StartingLocations[x].Read(reader);
}
for (int x=0; x<_startingLocations.Count; x++)
  StartingLocations[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _actorType.Write(writer);
    _platoon.Write(writer);
    _initialState.Write(writer);
    _returnState.Write(writer);
    _flags.Write(writer);
    _uniqueLeaderType.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    _maneuverToSquad.Write(writer);
    _squadDelayTime.Write(writer);
    _attacking.Write(writer);
    _attackingSearch.Write(writer);
    _attackingGuard.Write(writer);
    _defending.Write(writer);
    _defendingSearch.Write(writer);
    _defendingGuard.Write(writer);
    _pursuing.Write(writer);
    __unnamed4.Write(writer);
    __unnamed5.Write(writer);
    _normalDiffCount.Write(writer);
    _insaneDiffCount.Write(writer);
    _majorUpgrade.Write(writer);
    __unnamed6.Write(writer);
    _respawnMinActors.Write(writer);
    _respawnMaxActors.Write(writer);
    _respawnTotal.Write(writer);
    __unnamed7.Write(writer);
    _respawnDelay.Write(writer);
    __unnamed8.Write(writer);
    _movePositions.Write(writer);
    _startingLocations.Write(writer);
    __unnamed9.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_movePositions.UpdateReflexiveOffset(writer);
for (int x=0; x<_movePositions.Count; x++)
{
  MovePositions[x].Write(writer);
}
for (int x=0; x<_movePositions.Count; x++)
  MovePositions[x].WriteChildData(writer);
_startingLocations.UpdateReflexiveOffset(writer);
for (int x=0; x<_startingLocations.Count; x++)
{
  StartingLocations[x].Write(writer);
}
for (int x=0; x<_startingLocations.Count; x++)
  StartingLocations[x].WriteChildData(writer);
}
}
public class MovePositionsBlock : IBlock
{
private RealPoint3D _position = new RealPoint3D();
private Angle _facing = new Angle();
private Real _weight = new Real();
private RealBounds _time = new RealBounds();
private ShortBlockIndex _animation = new ShortBlockIndex();
private CharInteger _sequenceID = new CharInteger();
private Pad  __unnamed;	
private Pad  __unnamed2;	
private LongInteger _surfaceIndex = new LongInteger();
public RealPoint3D Position
{
  get { return _position; }
  set { _position = value; }
}
public Angle Facing
{
  get { return _facing; }
  set { _facing = value; }
}
public Real Weight
{
  get { return _weight; }
  set { _weight = value; }
}
public RealBounds Time
{
  get { return _time; }
  set { _time = value; }
}
public ShortBlockIndex Animation
{
  get { return _animation; }
  set { _animation = value; }
}
public CharInteger SequenceID
{
  get { return _sequenceID; }
  set { _sequenceID = value; }
}
public LongInteger SurfaceIndex
{
  get { return _surfaceIndex; }
  set { _surfaceIndex = value; }
}
public MovePositionsBlock()
{
__unnamed = new Pad(1);
__unnamed2 = new Pad(44);

}
public void Read(BinaryReader reader)
{
  _position.Read(reader);
  _facing.Read(reader);
  _weight.Read(reader);
  _time.Read(reader);
  _animation.Read(reader);
  _sequenceID.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  _surfaceIndex.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _position.Write(writer);
    _facing.Write(writer);
    _weight.Write(writer);
    _time.Write(writer);
    _animation.Write(writer);
    _sequenceID.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    _surfaceIndex.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class ActorStartingLocationsBlock : IBlock
{
private RealPoint3D _position = new RealPoint3D();
private Angle _facing = new Angle();
private Pad  __unnamed;	
private CharInteger _sequenceID = new CharInteger();
private Flags  _flags;	
private Enum _returnState = new Enum();
private Enum _initialState = new Enum();
private ShortBlockIndex _actorType = new ShortBlockIndex();
private ShortBlockIndex _commandList = new ShortBlockIndex();
public RealPoint3D Position
{
  get { return _position; }
  set { _position = value; }
}
public Angle Facing
{
  get { return _facing; }
  set { _facing = value; }
}
public CharInteger SequenceID
{
  get { return _sequenceID; }
  set { _sequenceID = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Enum ReturnState
{
  get { return _returnState; }
  set { _returnState = value; }
}
public Enum InitialState
{
  get { return _initialState; }
  set { _initialState = value; }
}
public ShortBlockIndex ActorType
{
  get { return _actorType; }
  set { _actorType = value; }
}
public ShortBlockIndex CommandList
{
  get { return _commandList; }
  set { _commandList = value; }
}
public ActorStartingLocationsBlock()
{
__unnamed = new Pad(2);
_flags = new Flags(1);

}
public void Read(BinaryReader reader)
{
  _position.Read(reader);
  _facing.Read(reader);
  __unnamed.Read(reader);
  _sequenceID.Read(reader);
  _flags.Read(reader);
  _returnState.Read(reader);
  _initialState.Read(reader);
  _actorType.Read(reader);
  _commandList.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _position.Write(writer);
    _facing.Write(writer);
    __unnamed.Write(writer);
    _sequenceID.Write(writer);
    _flags.Write(writer);
    _returnState.Write(writer);
    _initialState.Write(writer);
    _actorType.Write(writer);
    _commandList.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class PlatoonsBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private Flags  _flags;	
private Pad  __unnamed;	
private Enum _changeAttackingdefendingStateWhen = new Enum();
private ShortBlockIndex _happensTo = new ShortBlockIndex();
private Pad  __unnamed2;	
private Pad  __unnamed3;	
private Enum _maneuverWhen = new Enum();
private ShortBlockIndex _happensTo2 = new ShortBlockIndex();
private Pad  __unnamed4;	
private Pad  __unnamed5;	
private Pad  __unnamed6;	
private Pad  __unnamed7;	
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Enum ChangeAttackingdefendingStateWhen
{
  get { return _changeAttackingdefendingStateWhen; }
  set { _changeAttackingdefendingStateWhen = value; }
}
public ShortBlockIndex HappensTo
{
  get { return _happensTo; }
  set { _happensTo = value; }
}
public Enum ManeuverWhen
{
  get { return _maneuverWhen; }
  set { _maneuverWhen = value; }
}
public ShortBlockIndex HappensTo2
{
  get { return _happensTo2; }
  set { _happensTo2 = value; }
}
public PlatoonsBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(12);
__unnamed2 = new Pad(4);
__unnamed3 = new Pad(4);
__unnamed4 = new Pad(4);
__unnamed5 = new Pad(4);
__unnamed6 = new Pad(64);
__unnamed7 = new Pad(36);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _flags.Read(reader);
  __unnamed.Read(reader);
  _changeAttackingdefendingStateWhen.Read(reader);
  _happensTo.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  _maneuverWhen.Read(reader);
  _happensTo2.Read(reader);
  __unnamed4.Read(reader);
  __unnamed5.Read(reader);
  __unnamed6.Read(reader);
  __unnamed7.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _flags.Write(writer);
    __unnamed.Write(writer);
    _changeAttackingdefendingStateWhen.Write(writer);
    _happensTo.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    _maneuverWhen.Write(writer);
    _happensTo2.Write(writer);
    __unnamed4.Write(writer);
    __unnamed5.Write(writer);
    __unnamed6.Write(writer);
    __unnamed7.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class FiringPositionsBlock : IBlock
{
private RealPoint3D _position = new RealPoint3D();
private Enum _groupIndex = new Enum();
private Pad  __unnamed;	
public RealPoint3D Position
{
  get { return _position; }
  set { _position = value; }
}
public Enum GroupIndex
{
  get { return _groupIndex; }
  set { _groupIndex = value; }
}
public FiringPositionsBlock()
{
__unnamed = new Pad(10);

}
public void Read(BinaryReader reader)
{
  _position.Read(reader);
  _groupIndex.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _position.Write(writer);
    _groupIndex.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class AiCommandListBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private Flags  _flags;	
private Pad  __unnamed;	
private ShortInteger _manualBspIndex = new ShortInteger();
private Pad  __unnamed2;	
private Block _commands = new Block();
private Block _points = new Block();
private Pad  __unnamed3;	
public class AiCommandBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public AiCommandBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(AiCommandBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new AiCommandBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public AiCommandBlock this[int index]
  {
    get { return (InnerList[index] as AiCommandBlock); }
  }
}
private AiCommandBlockCollection _commandsCollection;
public AiCommandBlockCollection Commands
{
  get { return _commandsCollection; }
}
public class AiCommandPointBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public AiCommandPointBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(AiCommandPointBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new AiCommandPointBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public AiCommandPointBlock this[int index]
  {
    get { return (InnerList[index] as AiCommandPointBlock); }
  }
}
private AiCommandPointBlockCollection _pointsCollection;
public AiCommandPointBlockCollection Points
{
  get { return _pointsCollection; }
}
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public ShortInteger ManualBspIndex
{
  get { return _manualBspIndex; }
  set { _manualBspIndex = value; }
}
public AiCommandListBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(8);
__unnamed2 = new Pad(2);
__unnamed3 = new Pad(24);
_commandsCollection = new AiCommandBlockCollection(_commands);
_pointsCollection = new AiCommandPointBlockCollection(_points);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _flags.Read(reader);
  __unnamed.Read(reader);
  _manualBspIndex.Read(reader);
  __unnamed2.Read(reader);
  _commands.Read(reader);
  _points.Read(reader);
  __unnamed3.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_commands.Count; x++)
{
  Commands.AddNew();
  Commands[x].Read(reader);
}
for (int x=0; x<_commands.Count; x++)
  Commands[x].ReadChildData(reader);
for (int x=0; x<_points.Count; x++)
{
  Points.AddNew();
  Points[x].Read(reader);
}
for (int x=0; x<_points.Count; x++)
  Points[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _flags.Write(writer);
    __unnamed.Write(writer);
    _manualBspIndex.Write(writer);
    __unnamed2.Write(writer);
    _commands.Write(writer);
    _points.Write(writer);
    __unnamed3.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_commands.UpdateReflexiveOffset(writer);
for (int x=0; x<_commands.Count; x++)
{
  Commands[x].Write(writer);
}
for (int x=0; x<_commands.Count; x++)
  Commands[x].WriteChildData(writer);
_points.UpdateReflexiveOffset(writer);
for (int x=0; x<_points.Count; x++)
{
  Points[x].Write(writer);
}
for (int x=0; x<_points.Count; x++)
  Points[x].WriteChildData(writer);
}
}
public class AiCommandBlock : IBlock
{
private Enum _atomType = new Enum();
private ShortInteger _atomModifier = new ShortInteger();
private Real _parameter1 = new Real();
private Real _parameter2 = new Real();
private ShortBlockIndex _point1 = new ShortBlockIndex();
private ShortBlockIndex _point2 = new ShortBlockIndex();
private ShortBlockIndex _animation = new ShortBlockIndex();
private ShortBlockIndex _script = new ShortBlockIndex();
private ShortBlockIndex _recording = new ShortBlockIndex();
private ShortBlockIndex _command = new ShortBlockIndex();
private ShortBlockIndex _objectName = new ShortBlockIndex();
private Pad  __unnamed;	
public Enum AtomType
{
  get { return _atomType; }
  set { _atomType = value; }
}
public ShortInteger AtomModifier
{
  get { return _atomModifier; }
  set { _atomModifier = value; }
}
public Real Parameter1
{
  get { return _parameter1; }
  set { _parameter1 = value; }
}
public Real Parameter2
{
  get { return _parameter2; }
  set { _parameter2 = value; }
}
public ShortBlockIndex Point1
{
  get { return _point1; }
  set { _point1 = value; }
}
public ShortBlockIndex Point2
{
  get { return _point2; }
  set { _point2 = value; }
}
public ShortBlockIndex Animation
{
  get { return _animation; }
  set { _animation = value; }
}
public ShortBlockIndex Script
{
  get { return _script; }
  set { _script = value; }
}
public ShortBlockIndex Recording
{
  get { return _recording; }
  set { _recording = value; }
}
public ShortBlockIndex Command
{
  get { return _command; }
  set { _command = value; }
}
public ShortBlockIndex ObjectName
{
  get { return _objectName; }
  set { _objectName = value; }
}
public AiCommandBlock()
{
__unnamed = new Pad(6);

}
public void Read(BinaryReader reader)
{
  _atomType.Read(reader);
  _atomModifier.Read(reader);
  _parameter1.Read(reader);
  _parameter2.Read(reader);
  _point1.Read(reader);
  _point2.Read(reader);
  _animation.Read(reader);
  _script.Read(reader);
  _recording.Read(reader);
  _command.Read(reader);
  _objectName.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _atomType.Write(writer);
    _atomModifier.Write(writer);
    _parameter1.Write(writer);
    _parameter2.Write(writer);
    _point1.Write(writer);
    _point2.Write(writer);
    _animation.Write(writer);
    _script.Write(writer);
    _recording.Write(writer);
    _command.Write(writer);
    _objectName.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class AiCommandPointBlock : IBlock
{
private RealPoint3D _position = new RealPoint3D();
private Pad  __unnamed;	
public RealPoint3D Position
{
  get { return _position; }
  set { _position = value; }
}
public AiCommandPointBlock()
{
__unnamed = new Pad(8);

}
public void Read(BinaryReader reader)
{
  _position.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _position.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class AiAnimationReferenceBlock : IBlock
{
private FixedLengthString _animationName = new FixedLengthString();
private TagReference _animationGraph = new TagReference();
private Pad  __unnamed;	
public FixedLengthString AnimationName
{
  get { return _animationName; }
  set { _animationName = value; }
}
public TagReference AnimationGraph
{
  get { return _animationGraph; }
  set { _animationGraph = value; }
}
public AiAnimationReferenceBlock()
{
__unnamed = new Pad(12);

}
public void Read(BinaryReader reader)
{
  _animationName.Read(reader);
  _animationGraph.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_animationGraph.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _animationName.Write(writer);
    _animationGraph.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_animationGraph.WriteString(writer);
}
}
public class AiScriptReferenceBlock : IBlock
{
private FixedLengthString _scriptName = new FixedLengthString();
private Pad  __unnamed;	
public FixedLengthString ScriptName
{
  get { return _scriptName; }
  set { _scriptName = value; }
}
public AiScriptReferenceBlock()
{
__unnamed = new Pad(8);

}
public void Read(BinaryReader reader)
{
  _scriptName.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _scriptName.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class AiRecordingReferenceBlock : IBlock
{
private FixedLengthString _recordingName = new FixedLengthString();
private Pad  __unnamed;	
public FixedLengthString RecordingName
{
  get { return _recordingName; }
  set { _recordingName = value; }
}
public AiRecordingReferenceBlock()
{
__unnamed = new Pad(8);

}
public void Read(BinaryReader reader)
{
  _recordingName.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _recordingName.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class AiConversationBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private Flags  _flags;	
private Pad  __unnamed;	
private Real _triggerDistance = new Real();
private Real _ru = new Real();
private Pad  __unnamed2;	
private Block _participants = new Block();
private Block _lines = new Block();
private Pad  __unnamed3;	
public class AiConversationParticipantBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public AiConversationParticipantBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(AiConversationParticipantBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new AiConversationParticipantBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public AiConversationParticipantBlock this[int index]
  {
    get { return (InnerList[index] as AiConversationParticipantBlock); }
  }
}
private AiConversationParticipantBlockCollection _participantsCollection;
public AiConversationParticipantBlockCollection Participants
{
  get { return _participantsCollection; }
}
public class AiConversationLineBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public AiConversationLineBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(AiConversationLineBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new AiConversationLineBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public AiConversationLineBlock this[int index]
  {
    get { return (InnerList[index] as AiConversationLineBlock); }
  }
}
private AiConversationLineBlockCollection _linesCollection;
public AiConversationLineBlockCollection Lines
{
  get { return _linesCollection; }
}
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Real TriggerDistance
{
  get { return _triggerDistance; }
  set { _triggerDistance = value; }
}
public Real Ru
{
  get { return _ru; }
  set { _ru = value; }
}
public AiConversationBlock()
{
_flags = new Flags(2);
__unnamed = new Pad(2);
__unnamed2 = new Pad(36);
__unnamed3 = new Pad(12);
_participantsCollection = new AiConversationParticipantBlockCollection(_participants);
_linesCollection = new AiConversationLineBlockCollection(_lines);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _flags.Read(reader);
  __unnamed.Read(reader);
  _triggerDistance.Read(reader);
  _ru.Read(reader);
  __unnamed2.Read(reader);
  _participants.Read(reader);
  _lines.Read(reader);
  __unnamed3.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_participants.Count; x++)
{
  Participants.AddNew();
  Participants[x].Read(reader);
}
for (int x=0; x<_participants.Count; x++)
  Participants[x].ReadChildData(reader);
for (int x=0; x<_lines.Count; x++)
{
  Lines.AddNew();
  Lines[x].Read(reader);
}
for (int x=0; x<_lines.Count; x++)
  Lines[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _flags.Write(writer);
    __unnamed.Write(writer);
    _triggerDistance.Write(writer);
    _ru.Write(writer);
    __unnamed2.Write(writer);
    _participants.Write(writer);
    _lines.Write(writer);
    __unnamed3.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_participants.UpdateReflexiveOffset(writer);
for (int x=0; x<_participants.Count; x++)
{
  Participants[x].Write(writer);
}
for (int x=0; x<_participants.Count; x++)
  Participants[x].WriteChildData(writer);
_lines.UpdateReflexiveOffset(writer);
for (int x=0; x<_lines.Count; x++)
{
  Lines[x].Write(writer);
}
for (int x=0; x<_lines.Count; x++)
  Lines[x].WriteChildData(writer);
}
}
public class AiConversationParticipantBlock : IBlock
{
private Pad  __unnamed;	
private Flags  _flags;	
private Enum _selectionType = new Enum();
private Enum _actorType = new Enum();
private ShortBlockIndex _useThisObject = new ShortBlockIndex();
private ShortBlockIndex _setNewName = new ShortBlockIndex();
private Pad  __unnamed2;	
private Pad  __unnamed3;	
private FixedLengthString _encounterName = new FixedLengthString();
private Pad  __unnamed4;	
private Pad  __unnamed5;	
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Enum SelectionType
{
  get { return _selectionType; }
  set { _selectionType = value; }
}
public Enum ActorType
{
  get { return _actorType; }
  set { _actorType = value; }
}
public ShortBlockIndex UseThisObject
{
  get { return _useThisObject; }
  set { _useThisObject = value; }
}
public ShortBlockIndex SetNewName
{
  get { return _setNewName; }
  set { _setNewName = value; }
}
public FixedLengthString EncounterName
{
  get { return _encounterName; }
  set { _encounterName = value; }
}
public AiConversationParticipantBlock()
{
__unnamed = new Pad(2);
_flags = new Flags(2);
__unnamed2 = new Pad(12);
__unnamed3 = new Pad(12);
__unnamed4 = new Pad(4);
__unnamed5 = new Pad(12);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  _flags.Read(reader);
  _selectionType.Read(reader);
  _actorType.Read(reader);
  _useThisObject.Read(reader);
  _setNewName.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  _encounterName.Read(reader);
  __unnamed4.Read(reader);
  __unnamed5.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    _flags.Write(writer);
    _selectionType.Write(writer);
    _actorType.Write(writer);
    _useThisObject.Write(writer);
    _setNewName.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    _encounterName.Write(writer);
    __unnamed4.Write(writer);
    __unnamed5.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class AiConversationLineBlock : IBlock
{
private Flags  _flags;	
private ShortBlockIndex _participant = new ShortBlockIndex();
private Enum _addressee = new Enum();
private ShortBlockIndex _addresseeParticipant = new ShortBlockIndex();
private Pad  __unnamed;	
private Real _lineDelayTime = new Real();
private Pad  __unnamed2;	
private TagReference _variant1 = new TagReference();
private TagReference _variant2 = new TagReference();
private TagReference _variant3 = new TagReference();
private TagReference _variant4 = new TagReference();
private TagReference _variant5 = new TagReference();
private TagReference _variant6 = new TagReference();
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public ShortBlockIndex Participant
{
  get { return _participant; }
  set { _participant = value; }
}
public Enum Addressee
{
  get { return _addressee; }
  set { _addressee = value; }
}
public ShortBlockIndex AddresseeParticipant
{
  get { return _addresseeParticipant; }
  set { _addresseeParticipant = value; }
}
public Real LineDelayTime
{
  get { return _lineDelayTime; }
  set { _lineDelayTime = value; }
}
public TagReference Variant1
{
  get { return _variant1; }
  set { _variant1 = value; }
}
public TagReference Variant2
{
  get { return _variant2; }
  set { _variant2 = value; }
}
public TagReference Variant3
{
  get { return _variant3; }
  set { _variant3 = value; }
}
public TagReference Variant4
{
  get { return _variant4; }
  set { _variant4 = value; }
}
public TagReference Variant5
{
  get { return _variant5; }
  set { _variant5 = value; }
}
public TagReference Variant6
{
  get { return _variant6; }
  set { _variant6 = value; }
}
public AiConversationLineBlock()
{
_flags = new Flags(2);
__unnamed = new Pad(4);
__unnamed2 = new Pad(12);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _participant.Read(reader);
  _addressee.Read(reader);
  _addresseeParticipant.Read(reader);
  __unnamed.Read(reader);
  _lineDelayTime.Read(reader);
  __unnamed2.Read(reader);
  _variant1.Read(reader);
  _variant2.Read(reader);
  _variant3.Read(reader);
  _variant4.Read(reader);
  _variant5.Read(reader);
  _variant6.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_variant1.ReadString(reader);
_variant2.ReadString(reader);
_variant3.ReadString(reader);
_variant4.ReadString(reader);
_variant5.ReadString(reader);
_variant6.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _participant.Write(writer);
    _addressee.Write(writer);
    _addresseeParticipant.Write(writer);
    __unnamed.Write(writer);
    _lineDelayTime.Write(writer);
    __unnamed2.Write(writer);
    _variant1.Write(writer);
    _variant2.Write(writer);
    _variant3.Write(writer);
    _variant4.Write(writer);
    _variant5.Write(writer);
    _variant6.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_variant1.WriteString(writer);
_variant2.WriteString(writer);
_variant3.WriteString(writer);
_variant4.WriteString(writer);
_variant5.WriteString(writer);
_variant6.WriteString(writer);
}
}
public class HsScriptsBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private Enum _scriptType = new Enum();
private Enum _returnType = new Enum();
private LongInteger _rootExpressionIndex = new LongInteger();
private Pad  __unnamed;	
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public Enum ScriptType
{
  get { return _scriptType; }
  set { _scriptType = value; }
}
public Enum ReturnType
{
  get { return _returnType; }
  set { _returnType = value; }
}
public LongInteger RootExpressionIndex
{
  get { return _rootExpressionIndex; }
  set { _rootExpressionIndex = value; }
}
public HsScriptsBlock()
{
__unnamed = new Pad(52);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _scriptType.Read(reader);
  _returnType.Read(reader);
  _rootExpressionIndex.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _scriptType.Write(writer);
    _returnType.Write(writer);
    _rootExpressionIndex.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class HsGlobalsBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private Enum _type = new Enum();
private Pad  __unnamed;	
private Pad  __unnamed2;	
private LongInteger _initializationExpressionIndex = new LongInteger();
private Pad  __unnamed3;	
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public Enum Type
{
  get { return _type; }
  set { _type = value; }
}
public LongInteger InitializationExpressionIndex
{
  get { return _initializationExpressionIndex; }
  set { _initializationExpressionIndex = value; }
}
public HsGlobalsBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(4);
__unnamed3 = new Pad(48);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _type.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  _initializationExpressionIndex.Read(reader);
  __unnamed3.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _type.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    _initializationExpressionIndex.Write(writer);
    __unnamed3.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class HsReferencesBlock : IBlock
{
private Pad  __unnamed;	
private TagReference _reference = new TagReference();
public TagReference Reference
{
  get { return _reference; }
  set { _reference = value; }
}
public HsReferencesBlock()
{
__unnamed = new Pad(24);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  _reference.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_reference.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    _reference.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_reference.WriteString(writer);
}
}
public class HsSourceFilesBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private Data _source = new Data();
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public Data Source
{
  get { return _source; }
  set { _source = value; }
}
public HsSourceFilesBlock()
{

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _source.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_source.ReadBinary(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _source.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_source.WriteBinary(writer);
}
}
public class ScenarioCutsceneFlagBlock : IBlock
{
private Pad  __unnamed;	
private FixedLengthString _name = new FixedLengthString();
private RealPoint3D _position = new RealPoint3D();
private RealEulerAngles2D _facing = new RealEulerAngles2D();
private Pad  __unnamed2;	
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public RealPoint3D Position
{
  get { return _position; }
  set { _position = value; }
}
public RealEulerAngles2D Facing
{
  get { return _facing; }
  set { _facing = value; }
}
public ScenarioCutsceneFlagBlock()
{
__unnamed = new Pad(4);
__unnamed2 = new Pad(36);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  _name.Read(reader);
  _position.Read(reader);
  _facing.Read(reader);
  __unnamed2.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    _name.Write(writer);
    _position.Write(writer);
    _facing.Write(writer);
    __unnamed2.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class ScenarioCutsceneCameraPointBlock : IBlock
{
private Pad  __unnamed;	
private FixedLengthString _name = new FixedLengthString();
private Pad  __unnamed2;	
private RealPoint3D _position = new RealPoint3D();
private RealEulerAngles3D _orientation = new RealEulerAngles3D();
private Angle _fieldOfView = new Angle();
private Pad  __unnamed3;	
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public RealPoint3D Position
{
  get { return _position; }
  set { _position = value; }
}
public RealEulerAngles3D Orientation
{
  get { return _orientation; }
  set { _orientation = value; }
}
public Angle FieldOfView
{
  get { return _fieldOfView; }
  set { _fieldOfView = value; }
}
public ScenarioCutsceneCameraPointBlock()
{
__unnamed = new Pad(4);
__unnamed2 = new Pad(4);
__unnamed3 = new Pad(36);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  _name.Read(reader);
  __unnamed2.Read(reader);
  _position.Read(reader);
  _orientation.Read(reader);
  _fieldOfView.Read(reader);
  __unnamed3.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    _name.Write(writer);
    __unnamed2.Write(writer);
    _position.Write(writer);
    _orientation.Write(writer);
    _fieldOfView.Write(writer);
    __unnamed3.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class ScenarioCutsceneTitleBlock : IBlock
{
private Pad  __unnamed;	
private FixedLengthString _name = new FixedLengthString();
private Pad  __unnamed2;	
private Rectangle2D _textBoundsOnScreen = new Rectangle2D();
private ShortInteger _stringIndex = new ShortInteger();
private Pad  __unnamed3;	
private Enum _justification = new Enum();
private Pad  __unnamed4;	
private Pad  __unnamed5;	
private ARGBColor _textColor = new ARGBColor();
private ARGBColor _shadowColor = new ARGBColor();
private Real _fadeInTimeSeconds = new Real();
private Real _upTimeSeconds = new Real();
private Real _fadeOutTimeSeconds = new Real();
private Pad  __unnamed6;	
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public Rectangle2D TextBoundsOnScreen
{
  get { return _textBoundsOnScreen; }
  set { _textBoundsOnScreen = value; }
}
public ShortInteger StringIndex
{
  get { return _stringIndex; }
  set { _stringIndex = value; }
}
public Enum Justification
{
  get { return _justification; }
  set { _justification = value; }
}
public ARGBColor TextColor
{
  get { return _textColor; }
  set { _textColor = value; }
}
public ARGBColor ShadowColor
{
  get { return _shadowColor; }
  set { _shadowColor = value; }
}
public Real FadeInTimeSeconds
{
  get { return _fadeInTimeSeconds; }
  set { _fadeInTimeSeconds = value; }
}
public Real UpTimeSeconds
{
  get { return _upTimeSeconds; }
  set { _upTimeSeconds = value; }
}
public Real FadeOutTimeSeconds
{
  get { return _fadeOutTimeSeconds; }
  set { _fadeOutTimeSeconds = value; }
}
public ScenarioCutsceneTitleBlock()
{
__unnamed = new Pad(4);
__unnamed2 = new Pad(4);
__unnamed3 = new Pad(2);
__unnamed4 = new Pad(2);
__unnamed5 = new Pad(4);
__unnamed6 = new Pad(16);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  _name.Read(reader);
  __unnamed2.Read(reader);
  _textBoundsOnScreen.Read(reader);
  _stringIndex.Read(reader);
  __unnamed3.Read(reader);
  _justification.Read(reader);
  __unnamed4.Read(reader);
  __unnamed5.Read(reader);
  _textColor.Read(reader);
  _shadowColor.Read(reader);
  _fadeInTimeSeconds.Read(reader);
  _upTimeSeconds.Read(reader);
  _fadeOutTimeSeconds.Read(reader);
  __unnamed6.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    _name.Write(writer);
    __unnamed2.Write(writer);
    _textBoundsOnScreen.Write(writer);
    _stringIndex.Write(writer);
    __unnamed3.Write(writer);
    _justification.Write(writer);
    __unnamed4.Write(writer);
    __unnamed5.Write(writer);
    _textColor.Write(writer);
    _shadowColor.Write(writer);
    _fadeInTimeSeconds.Write(writer);
    _upTimeSeconds.Write(writer);
    _fadeOutTimeSeconds.Write(writer);
    __unnamed6.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class ScenarioStructureBspsBlock : IBlock
{
private Pad  __unnamed;	
private TagReference _structureBsp = new TagReference();
public TagReference StructureBsp
{
  get { return _structureBsp; }
  set { _structureBsp = value; }
}
public ScenarioStructureBspsBlock()
{
__unnamed = new Pad(16);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  _structureBsp.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_structureBsp.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    _structureBsp.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_structureBsp.WriteString(writer);
}
}
  }
}
