using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Weapon : Item
  {
    public WeaponBlock WeaponValues = new WeaponBlock();
    public new void Read(BinaryReader reader)
    {
      base.Read(reader);
      Trace.WriteLine("Loading 'Weapon'------------------------------------------------------");
      WeaponValues.Read(reader);
    }
    public new void ReadChildData(BinaryReader reader)
    {
      base.ReadChildData(reader);
      WeaponValues.ReadChildData(reader);
    }
    public new void Write(BinaryWriter writer)
    {
      base.Write(writer);
      WeaponValues.Write(writer);
    }
    public new void WriteChildData(BinaryWriter writer)
    {
      base.WriteChildData(writer);
      WeaponValues.WriteChildData(writer);
    }
public class WeaponBlock : IBlock
{
private Flags  _flags;	
private FixedLengthString _label = new FixedLengthString();
private Enum _secondaryTriggerMode = new Enum();
private ShortInteger _maximumAlternateShotsLoaded = new ShortInteger();
private Enum _aIn = new Enum();
private Enum _bIn = new Enum();
private Enum _cIn = new Enum();
private Enum _dIn = new Enum();
private Real _readyTime = new Real();
private TagReference _readyEffect = new TagReference();
private RealFraction _heatRecoveryThreshold = new RealFraction();
private RealFraction _overheatedThreshold = new RealFraction();
private RealFraction _heatDetonationThreshold = new RealFraction();
private RealFraction _heatDetonationFraction = new RealFraction();
private RealFraction _heatLossPerSecond = new RealFraction();
private RealFraction _heatIllumination = new RealFraction();
private Pad  __unnamed;	
private TagReference _overheated = new TagReference();
private TagReference _detonation = new TagReference();
private TagReference _playerMeleeDamage = new TagReference();
private TagReference _playerMeleeResponse = new TagReference();
private Pad  __unnamed2;	
private TagReference _actorFiringParameters = new TagReference();
private Real _nearReticleRange = new Real();
private Real _farReticleRange = new Real();
private Real _intersectionReticleRange = new Real();
private Pad  __unnamed3;	
private ShortInteger _magnificationLevels = new ShortInteger();
private RealBounds _magnificationRange = new RealBounds();
private Angle _autoaimAngle = new Angle();
private Real _autoaimRange = new Real();
private Angle _magnetismAngle = new Angle();
private Real _magnetismRange = new Real();
private Angle _deviationAngle = new Angle();
private Pad  __unnamed4;	
private Enum _movementPenalized = new Enum();
private Pad  __unnamed5;	
private RealFraction _forwardMovementPenalty = new RealFraction();
private RealFraction _sidewaysMovementPenalty = new RealFraction();
private Pad  __unnamed6;	
private Real _minimumTargetRange = new Real();
private Real _lookingTimeModifier = new Real();
private Pad  __unnamed7;	
private Real _lightPowe = new Real();
private Real _lightPowe2 = new Real();
private TagReference _lightPowe3 = new TagReference();
private TagReference _lightPowe4 = new TagReference();
private Real _ageHeatRecoveryPenalty = new Real();
private Real _ageRateOfFirePenalty = new Real();
private RealFraction _ageMisfireStart = new RealFraction();
private RealFraction _ageMisfireChance = new RealFraction();
private Pad  __unnamed8;	
private TagReference _firstPersonModel = new TagReference();
private TagReference _firstPersonAnimations = new TagReference();
private Pad  __unnamed9;	
private TagReference _hudInterface = new TagReference();
private TagReference _pickupSound = new TagReference();
private TagReference _zoo = new TagReference();
private TagReference _zoo2 = new TagReference();
private Pad  __unnamed10;	
private Real _activeCamoDing = new Real();
private Real _activeCamoRegrowthRate = new Real();
private Pad  __unnamed11;	
private Pad  __unnamed12;	
private Enum _weaponType = new Enum();
private Block _predictedResources = new Block();
private Block _magazines = new Block();
private Block _triggers = new Block();
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
public class MagazinesBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public MagazinesBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(MagazinesBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new MagazinesBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public MagazinesBlock this[int index]
  {
    get { return (InnerList[index] as MagazinesBlock); }
  }
}
private MagazinesBlockCollection _magazinesCollection;
public MagazinesBlockCollection Magazines
{
  get { return _magazinesCollection; }
}
public class TriggersBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public TriggersBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(TriggersBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new TriggersBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public TriggersBlock this[int index]
  {
    get { return (InnerList[index] as TriggersBlock); }
  }
}
private TriggersBlockCollection _triggersCollection;
public TriggersBlockCollection Triggers
{
  get { return _triggersCollection; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public FixedLengthString Label
{
  get { return _label; }
  set { _label = value; }
}
public Enum SecondaryTriggerMode
{
  get { return _secondaryTriggerMode; }
  set { _secondaryTriggerMode = value; }
}
public ShortInteger MaximumAlternateShotsLoaded
{
  get { return _maximumAlternateShotsLoaded; }
  set { _maximumAlternateShotsLoaded = value; }
}
public Enum AIn
{
  get { return _aIn; }
  set { _aIn = value; }
}
public Enum BIn
{
  get { return _bIn; }
  set { _bIn = value; }
}
public Enum CIn
{
  get { return _cIn; }
  set { _cIn = value; }
}
public Enum DIn
{
  get { return _dIn; }
  set { _dIn = value; }
}
public Real ReadyTime
{
  get { return _readyTime; }
  set { _readyTime = value; }
}
public TagReference ReadyEffect
{
  get { return _readyEffect; }
  set { _readyEffect = value; }
}
public RealFraction HeatRecoveryThreshold
{
  get { return _heatRecoveryThreshold; }
  set { _heatRecoveryThreshold = value; }
}
public RealFraction OverheatedThreshold
{
  get { return _overheatedThreshold; }
  set { _overheatedThreshold = value; }
}
public RealFraction HeatDetonationThreshold
{
  get { return _heatDetonationThreshold; }
  set { _heatDetonationThreshold = value; }
}
public RealFraction HeatDetonationFraction
{
  get { return _heatDetonationFraction; }
  set { _heatDetonationFraction = value; }
}
public RealFraction HeatLossPerSecond
{
  get { return _heatLossPerSecond; }
  set { _heatLossPerSecond = value; }
}
public RealFraction HeatIllumination
{
  get { return _heatIllumination; }
  set { _heatIllumination = value; }
}
public TagReference Overheated
{
  get { return _overheated; }
  set { _overheated = value; }
}
public TagReference Detonation
{
  get { return _detonation; }
  set { _detonation = value; }
}
public TagReference PlayerMeleeDamage
{
  get { return _playerMeleeDamage; }
  set { _playerMeleeDamage = value; }
}
public TagReference PlayerMeleeResponse
{
  get { return _playerMeleeResponse; }
  set { _playerMeleeResponse = value; }
}
public TagReference ActorFiringParameters
{
  get { return _actorFiringParameters; }
  set { _actorFiringParameters = value; }
}
public Real NearReticleRange
{
  get { return _nearReticleRange; }
  set { _nearReticleRange = value; }
}
public Real FarReticleRange
{
  get { return _farReticleRange; }
  set { _farReticleRange = value; }
}
public Real IntersectionReticleRange
{
  get { return _intersectionReticleRange; }
  set { _intersectionReticleRange = value; }
}
public ShortInteger MagnificationLevels
{
  get { return _magnificationLevels; }
  set { _magnificationLevels = value; }
}
public RealBounds MagnificationRange
{
  get { return _magnificationRange; }
  set { _magnificationRange = value; }
}
public Angle AutoaimAngle
{
  get { return _autoaimAngle; }
  set { _autoaimAngle = value; }
}
public Real AutoaimRange
{
  get { return _autoaimRange; }
  set { _autoaimRange = value; }
}
public Angle MagnetismAngle
{
  get { return _magnetismAngle; }
  set { _magnetismAngle = value; }
}
public Real MagnetismRange
{
  get { return _magnetismRange; }
  set { _magnetismRange = value; }
}
public Angle DeviationAngle
{
  get { return _deviationAngle; }
  set { _deviationAngle = value; }
}
public Enum MovementPenalized
{
  get { return _movementPenalized; }
  set { _movementPenalized = value; }
}
public RealFraction ForwardMovementPenalty
{
  get { return _forwardMovementPenalty; }
  set { _forwardMovementPenalty = value; }
}
public RealFraction SidewaysMovementPenalty
{
  get { return _sidewaysMovementPenalty; }
  set { _sidewaysMovementPenalty = value; }
}
public Real MinimumTargetRange
{
  get { return _minimumTargetRange; }
  set { _minimumTargetRange = value; }
}
public Real LookingTimeModifier
{
  get { return _lookingTimeModifier; }
  set { _lookingTimeModifier = value; }
}
public Real LightPowe
{
  get { return _lightPowe; }
  set { _lightPowe = value; }
}
public Real LightPowe2
{
  get { return _lightPowe2; }
  set { _lightPowe2 = value; }
}
public TagReference LightPowe3
{
  get { return _lightPowe3; }
  set { _lightPowe3 = value; }
}
public TagReference LightPowe4
{
  get { return _lightPowe4; }
  set { _lightPowe4 = value; }
}
public Real AgeHeatRecoveryPenalty
{
  get { return _ageHeatRecoveryPenalty; }
  set { _ageHeatRecoveryPenalty = value; }
}
public Real AgeRateOfFirePenalty
{
  get { return _ageRateOfFirePenalty; }
  set { _ageRateOfFirePenalty = value; }
}
public RealFraction AgeMisfireStart
{
  get { return _ageMisfireStart; }
  set { _ageMisfireStart = value; }
}
public RealFraction AgeMisfireChance
{
  get { return _ageMisfireChance; }
  set { _ageMisfireChance = value; }
}
public TagReference FirstPersonModel
{
  get { return _firstPersonModel; }
  set { _firstPersonModel = value; }
}
public TagReference FirstPersonAnimations
{
  get { return _firstPersonAnimations; }
  set { _firstPersonAnimations = value; }
}
public TagReference HudInterface
{
  get { return _hudInterface; }
  set { _hudInterface = value; }
}
public TagReference PickupSound
{
  get { return _pickupSound; }
  set { _pickupSound = value; }
}
public TagReference Zoo
{
  get { return _zoo; }
  set { _zoo = value; }
}
public TagReference Zoo2
{
  get { return _zoo2; }
  set { _zoo2 = value; }
}
public Real ActiveCamoDing
{
  get { return _activeCamoDing; }
  set { _activeCamoDing = value; }
}
public Real ActiveCamoRegrowthRate
{
  get { return _activeCamoRegrowthRate; }
  set { _activeCamoRegrowthRate = value; }
}
public Enum WeaponType
{
  get { return _weaponType; }
  set { _weaponType = value; }
}
public WeaponBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(16);
__unnamed2 = new Pad(8);
__unnamed3 = new Pad(2);
__unnamed4 = new Pad(4);
__unnamed5 = new Pad(2);
__unnamed6 = new Pad(4);
__unnamed7 = new Pad(4);
__unnamed8 = new Pad(12);
__unnamed9 = new Pad(4);
__unnamed10 = new Pad(12);
__unnamed11 = new Pad(12);
__unnamed12 = new Pad(2);
_predictedResourcesCollection = new PredictedResourceBlockCollection(_predictedResources);
_magazinesCollection = new MagazinesBlockCollection(_magazines);
_triggersCollection = new TriggersBlockCollection(_triggers);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _label.Read(reader);
  _secondaryTriggerMode.Read(reader);
  _maximumAlternateShotsLoaded.Read(reader);
  _aIn.Read(reader);
  _bIn.Read(reader);
  _cIn.Read(reader);
  _dIn.Read(reader);
  _readyTime.Read(reader);
  _readyEffect.Read(reader);
  _heatRecoveryThreshold.Read(reader);
  _overheatedThreshold.Read(reader);
  _heatDetonationThreshold.Read(reader);
  _heatDetonationFraction.Read(reader);
  _heatLossPerSecond.Read(reader);
  _heatIllumination.Read(reader);
  __unnamed.Read(reader);
  _overheated.Read(reader);
  _detonation.Read(reader);
  _playerMeleeDamage.Read(reader);
  _playerMeleeResponse.Read(reader);
  __unnamed2.Read(reader);
  _actorFiringParameters.Read(reader);
  _nearReticleRange.Read(reader);
  _farReticleRange.Read(reader);
  _intersectionReticleRange.Read(reader);
  __unnamed3.Read(reader);
  _magnificationLevels.Read(reader);
  _magnificationRange.Read(reader);
  _autoaimAngle.Read(reader);
  _autoaimRange.Read(reader);
  _magnetismAngle.Read(reader);
  _magnetismRange.Read(reader);
  _deviationAngle.Read(reader);
  __unnamed4.Read(reader);
  _movementPenalized.Read(reader);
  __unnamed5.Read(reader);
  _forwardMovementPenalty.Read(reader);
  _sidewaysMovementPenalty.Read(reader);
  __unnamed6.Read(reader);
  _minimumTargetRange.Read(reader);
  _lookingTimeModifier.Read(reader);
  __unnamed7.Read(reader);
  _lightPowe.Read(reader);
  _lightPowe2.Read(reader);
  _lightPowe3.Read(reader);
  _lightPowe4.Read(reader);
  _ageHeatRecoveryPenalty.Read(reader);
  _ageRateOfFirePenalty.Read(reader);
  _ageMisfireStart.Read(reader);
  _ageMisfireChance.Read(reader);
  __unnamed8.Read(reader);
  _firstPersonModel.Read(reader);
  _firstPersonAnimations.Read(reader);
  __unnamed9.Read(reader);
  _hudInterface.Read(reader);
  _pickupSound.Read(reader);
  _zoo.Read(reader);
  _zoo2.Read(reader);
  __unnamed10.Read(reader);
  _activeCamoDing.Read(reader);
  _activeCamoRegrowthRate.Read(reader);
  __unnamed11.Read(reader);
  __unnamed12.Read(reader);
  _weaponType.Read(reader);
  _predictedResources.Read(reader);
  _magazines.Read(reader);
  _triggers.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_readyEffect.ReadString(reader);
_overheated.ReadString(reader);
_detonation.ReadString(reader);
_playerMeleeDamage.ReadString(reader);
_playerMeleeResponse.ReadString(reader);
_actorFiringParameters.ReadString(reader);
_lightPowe3.ReadString(reader);
_lightPowe4.ReadString(reader);
_firstPersonModel.ReadString(reader);
_firstPersonAnimations.ReadString(reader);
_hudInterface.ReadString(reader);
_pickupSound.ReadString(reader);
_zoo.ReadString(reader);
_zoo2.ReadString(reader);
for (int x=0; x<_predictedResources.Count; x++)
{
  PredictedResources.AddNew();
  PredictedResources[x].Read(reader);
}
for (int x=0; x<_predictedResources.Count; x++)
  PredictedResources[x].ReadChildData(reader);
for (int x=0; x<_magazines.Count; x++)
{
  Magazines.AddNew();
  Magazines[x].Read(reader);
}
for (int x=0; x<_magazines.Count; x++)
  Magazines[x].ReadChildData(reader);
for (int x=0; x<_triggers.Count; x++)
{
  Triggers.AddNew();
  Triggers[x].Read(reader);
}
for (int x=0; x<_triggers.Count; x++)
  Triggers[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _label.Write(writer);
    _secondaryTriggerMode.Write(writer);
    _maximumAlternateShotsLoaded.Write(writer);
    _aIn.Write(writer);
    _bIn.Write(writer);
    _cIn.Write(writer);
    _dIn.Write(writer);
    _readyTime.Write(writer);
    _readyEffect.Write(writer);
    _heatRecoveryThreshold.Write(writer);
    _overheatedThreshold.Write(writer);
    _heatDetonationThreshold.Write(writer);
    _heatDetonationFraction.Write(writer);
    _heatLossPerSecond.Write(writer);
    _heatIllumination.Write(writer);
    __unnamed.Write(writer);
    _overheated.Write(writer);
    _detonation.Write(writer);
    _playerMeleeDamage.Write(writer);
    _playerMeleeResponse.Write(writer);
    __unnamed2.Write(writer);
    _actorFiringParameters.Write(writer);
    _nearReticleRange.Write(writer);
    _farReticleRange.Write(writer);
    _intersectionReticleRange.Write(writer);
    __unnamed3.Write(writer);
    _magnificationLevels.Write(writer);
    _magnificationRange.Write(writer);
    _autoaimAngle.Write(writer);
    _autoaimRange.Write(writer);
    _magnetismAngle.Write(writer);
    _magnetismRange.Write(writer);
    _deviationAngle.Write(writer);
    __unnamed4.Write(writer);
    _movementPenalized.Write(writer);
    __unnamed5.Write(writer);
    _forwardMovementPenalty.Write(writer);
    _sidewaysMovementPenalty.Write(writer);
    __unnamed6.Write(writer);
    _minimumTargetRange.Write(writer);
    _lookingTimeModifier.Write(writer);
    __unnamed7.Write(writer);
    _lightPowe.Write(writer);
    _lightPowe2.Write(writer);
    _lightPowe3.Write(writer);
    _lightPowe4.Write(writer);
    _ageHeatRecoveryPenalty.Write(writer);
    _ageRateOfFirePenalty.Write(writer);
    _ageMisfireStart.Write(writer);
    _ageMisfireChance.Write(writer);
    __unnamed8.Write(writer);
    _firstPersonModel.Write(writer);
    _firstPersonAnimations.Write(writer);
    __unnamed9.Write(writer);
    _hudInterface.Write(writer);
    _pickupSound.Write(writer);
    _zoo.Write(writer);
    _zoo2.Write(writer);
    __unnamed10.Write(writer);
    _activeCamoDing.Write(writer);
    _activeCamoRegrowthRate.Write(writer);
    __unnamed11.Write(writer);
    __unnamed12.Write(writer);
    _weaponType.Write(writer);
    _predictedResources.Write(writer);
    _magazines.Write(writer);
    _triggers.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_readyEffect.WriteString(writer);
_overheated.WriteString(writer);
_detonation.WriteString(writer);
_playerMeleeDamage.WriteString(writer);
_playerMeleeResponse.WriteString(writer);
_actorFiringParameters.WriteString(writer);
_lightPowe3.WriteString(writer);
_lightPowe4.WriteString(writer);
_firstPersonModel.WriteString(writer);
_firstPersonAnimations.WriteString(writer);
_hudInterface.WriteString(writer);
_pickupSound.WriteString(writer);
_zoo.WriteString(writer);
_zoo2.WriteString(writer);
_predictedResources.UpdateReflexiveOffset(writer);
for (int x=0; x<_predictedResources.Count; x++)
{
  PredictedResources[x].Write(writer);
}
for (int x=0; x<_predictedResources.Count; x++)
  PredictedResources[x].WriteChildData(writer);
_magazines.UpdateReflexiveOffset(writer);
for (int x=0; x<_magazines.Count; x++)
{
  Magazines[x].Write(writer);
}
for (int x=0; x<_magazines.Count; x++)
  Magazines[x].WriteChildData(writer);
_triggers.UpdateReflexiveOffset(writer);
for (int x=0; x<_triggers.Count; x++)
{
  Triggers[x].Write(writer);
}
for (int x=0; x<_triggers.Count; x++)
  Triggers[x].WriteChildData(writer);
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
public class MagazinesBlock : IBlock
{
private Flags  _flags;	
private ShortInteger _roundsRecharged = new ShortInteger();
private ShortInteger _roundsTotalInitial = new ShortInteger();
private ShortInteger _roundsTotalMaximum = new ShortInteger();
private ShortInteger _roundsLoadedMaximum = new ShortInteger();
private Pad  __unnamed;	
private Real _reloadTime = new Real();
private ShortInteger _roundsReloaded = new ShortInteger();
private Pad  __unnamed2;	
private Real _chamberTime = new Real();
private Pad  __unnamed3;	
private Pad  __unnamed4;	
private TagReference _reloadingEffect = new TagReference();
private TagReference _chamberingEffect = new TagReference();
private Pad  __unnamed5;	
private Block _magazines = new Block();
public class MagazineObjectsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public MagazineObjectsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(MagazineObjectsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new MagazineObjectsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public MagazineObjectsBlock this[int index]
  {
    get { return (InnerList[index] as MagazineObjectsBlock); }
  }
}
private MagazineObjectsBlockCollection _magazinesCollection;
public MagazineObjectsBlockCollection Magazines
{
  get { return _magazinesCollection; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public ShortInteger RoundsRecharged
{
  get { return _roundsRecharged; }
  set { _roundsRecharged = value; }
}
public ShortInteger RoundsTotalInitial
{
  get { return _roundsTotalInitial; }
  set { _roundsTotalInitial = value; }
}
public ShortInteger RoundsTotalMaximum
{
  get { return _roundsTotalMaximum; }
  set { _roundsTotalMaximum = value; }
}
public ShortInteger RoundsLoadedMaximum
{
  get { return _roundsLoadedMaximum; }
  set { _roundsLoadedMaximum = value; }
}
public Real ReloadTime
{
  get { return _reloadTime; }
  set { _reloadTime = value; }
}
public ShortInteger RoundsReloaded
{
  get { return _roundsReloaded; }
  set { _roundsReloaded = value; }
}
public Real ChamberTime
{
  get { return _chamberTime; }
  set { _chamberTime = value; }
}
public TagReference ReloadingEffect
{
  get { return _reloadingEffect; }
  set { _reloadingEffect = value; }
}
public TagReference ChamberingEffect
{
  get { return _chamberingEffect; }
  set { _chamberingEffect = value; }
}
public MagazinesBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(8);
__unnamed2 = new Pad(2);
__unnamed3 = new Pad(8);
__unnamed4 = new Pad(16);
__unnamed5 = new Pad(12);
_magazinesCollection = new MagazineObjectsBlockCollection(_magazines);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _roundsRecharged.Read(reader);
  _roundsTotalInitial.Read(reader);
  _roundsTotalMaximum.Read(reader);
  _roundsLoadedMaximum.Read(reader);
  __unnamed.Read(reader);
  _reloadTime.Read(reader);
  _roundsReloaded.Read(reader);
  __unnamed2.Read(reader);
  _chamberTime.Read(reader);
  __unnamed3.Read(reader);
  __unnamed4.Read(reader);
  _reloadingEffect.Read(reader);
  _chamberingEffect.Read(reader);
  __unnamed5.Read(reader);
  _magazines.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_reloadingEffect.ReadString(reader);
_chamberingEffect.ReadString(reader);
for (int x=0; x<_magazines.Count; x++)
{
  Magazines.AddNew();
  Magazines[x].Read(reader);
}
for (int x=0; x<_magazines.Count; x++)
  Magazines[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _roundsRecharged.Write(writer);
    _roundsTotalInitial.Write(writer);
    _roundsTotalMaximum.Write(writer);
    _roundsLoadedMaximum.Write(writer);
    __unnamed.Write(writer);
    _reloadTime.Write(writer);
    _roundsReloaded.Write(writer);
    __unnamed2.Write(writer);
    _chamberTime.Write(writer);
    __unnamed3.Write(writer);
    __unnamed4.Write(writer);
    _reloadingEffect.Write(writer);
    _chamberingEffect.Write(writer);
    __unnamed5.Write(writer);
    _magazines.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_reloadingEffect.WriteString(writer);
_chamberingEffect.WriteString(writer);
_magazines.UpdateReflexiveOffset(writer);
for (int x=0; x<_magazines.Count; x++)
{
  Magazines[x].Write(writer);
}
for (int x=0; x<_magazines.Count; x++)
  Magazines[x].WriteChildData(writer);
}
}
public class MagazineObjectsBlock : IBlock
{
private ShortInteger _rounds = new ShortInteger();
private Pad  __unnamed;	
private TagReference _equipment = new TagReference();
public ShortInteger Rounds
{
  get { return _rounds; }
  set { _rounds = value; }
}
public TagReference Equipment
{
  get { return _equipment; }
  set { _equipment = value; }
}
public MagazineObjectsBlock()
{
__unnamed = new Pad(10);

}
public void Read(BinaryReader reader)
{
  _rounds.Read(reader);
  __unnamed.Read(reader);
  _equipment.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_equipment.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _rounds.Write(writer);
    __unnamed.Write(writer);
    _equipment.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_equipment.WriteString(writer);
}
}
public class TriggersBlock : IBlock
{
private Flags  _flags;	
private RealBounds _roundsPerSecond = new RealBounds();
private Real _accelerationTime = new Real();
private Real _decelerationTime = new Real();
private RealFraction _blurredRateOfFire = new RealFraction();
private Pad  __unnamed;	
private ShortBlockIndex _magazine = new ShortBlockIndex();
private ShortInteger _roundsPerShot = new ShortInteger();
private ShortInteger _minimumRoundsLoaded = new ShortInteger();
private ShortInteger _roundsBetweenTracers = new ShortInteger();
private Pad  __unnamed2;	
private Enum _firingNoise = new Enum();
private RealBounds _error = new RealBounds();
private Real _accelerationTime2 = new Real();
private Real _decelerationTime2 = new Real();
private Pad  __unnamed3;	
private Real _chargingTime = new Real();
private Real _chargedTime = new Real();
private Enum _overchargedAction = new Enum();
private Pad  __unnamed4;	
private Real _chargedIllumination = new Real();
private Real _spewTime = new Real();
private TagReference _chargingEffect = new TagReference();
private Enum _distributionFunction = new Enum();
private ShortInteger _projectilesPerShot = new ShortInteger();
private Real _distributionAngle = new Real();
private Pad  __unnamed5;	
private Angle _minimumError = new Angle();
private AngleBounds _errorAngle = new AngleBounds();
private RealPoint3D _firstPersonOffset = new RealPoint3D();
private Pad  __unnamed6;	
private TagReference _projectile = new TagReference();
private Real _ejectionPortRecoveryTime = new Real();
private Real _illuminationRecoveryTime = new Real();
private Pad  __unnamed7;	
private RealFraction _heatGeneratedPerRound = new RealFraction();
private RealFraction _ageGeneratedPerRound = new RealFraction();
private Pad  __unnamed8;	
private Real _overloadTime = new Real();
private Pad  __unnamed9;	
private Pad  __unnamed10;	
private Pad  __unnamed11;	
private Block _firingEffects = new Block();
public class TriggerFiringEffectBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public TriggerFiringEffectBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(TriggerFiringEffectBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new TriggerFiringEffectBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public TriggerFiringEffectBlock this[int index]
  {
    get { return (InnerList[index] as TriggerFiringEffectBlock); }
  }
}
private TriggerFiringEffectBlockCollection _firingEffectsCollection;
public TriggerFiringEffectBlockCollection FiringEffects
{
  get { return _firingEffectsCollection; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public RealBounds RoundsPerSecond
{
  get { return _roundsPerSecond; }
  set { _roundsPerSecond = value; }
}
public Real AccelerationTime
{
  get { return _accelerationTime; }
  set { _accelerationTime = value; }
}
public Real DecelerationTime
{
  get { return _decelerationTime; }
  set { _decelerationTime = value; }
}
public RealFraction BlurredRateOfFire
{
  get { return _blurredRateOfFire; }
  set { _blurredRateOfFire = value; }
}
public ShortBlockIndex Magazine
{
  get { return _magazine; }
  set { _magazine = value; }
}
public ShortInteger RoundsPerShot
{
  get { return _roundsPerShot; }
  set { _roundsPerShot = value; }
}
public ShortInteger MinimumRoundsLoaded
{
  get { return _minimumRoundsLoaded; }
  set { _minimumRoundsLoaded = value; }
}
public ShortInteger RoundsBetweenTracers
{
  get { return _roundsBetweenTracers; }
  set { _roundsBetweenTracers = value; }
}
public Enum FiringNoise
{
  get { return _firingNoise; }
  set { _firingNoise = value; }
}
public RealBounds Error
{
  get { return _error; }
  set { _error = value; }
}
public Real AccelerationTime2
{
  get { return _accelerationTime2; }
  set { _accelerationTime2 = value; }
}
public Real DecelerationTime2
{
  get { return _decelerationTime2; }
  set { _decelerationTime2 = value; }
}
public Real ChargingTime
{
  get { return _chargingTime; }
  set { _chargingTime = value; }
}
public Real ChargedTime
{
  get { return _chargedTime; }
  set { _chargedTime = value; }
}
public Enum OverchargedAction
{
  get { return _overchargedAction; }
  set { _overchargedAction = value; }
}
public Real ChargedIllumination
{
  get { return _chargedIllumination; }
  set { _chargedIllumination = value; }
}
public Real SpewTime
{
  get { return _spewTime; }
  set { _spewTime = value; }
}
public TagReference ChargingEffect
{
  get { return _chargingEffect; }
  set { _chargingEffect = value; }
}
public Enum DistributionFunction
{
  get { return _distributionFunction; }
  set { _distributionFunction = value; }
}
public ShortInteger ProjectilesPerShot
{
  get { return _projectilesPerShot; }
  set { _projectilesPerShot = value; }
}
public Real DistributionAngle
{
  get { return _distributionAngle; }
  set { _distributionAngle = value; }
}
public Angle MinimumError
{
  get { return _minimumError; }
  set { _minimumError = value; }
}
public AngleBounds ErrorAngle
{
  get { return _errorAngle; }
  set { _errorAngle = value; }
}
public RealPoint3D FirstPersonOffset
{
  get { return _firstPersonOffset; }
  set { _firstPersonOffset = value; }
}
public TagReference Projectile
{
  get { return _projectile; }
  set { _projectile = value; }
}
public Real EjectionPortRecoveryTime
{
  get { return _ejectionPortRecoveryTime; }
  set { _ejectionPortRecoveryTime = value; }
}
public Real IlluminationRecoveryTime
{
  get { return _illuminationRecoveryTime; }
  set { _illuminationRecoveryTime = value; }
}
public RealFraction HeatGeneratedPerRound
{
  get { return _heatGeneratedPerRound; }
  set { _heatGeneratedPerRound = value; }
}
public RealFraction AgeGeneratedPerRound
{
  get { return _ageGeneratedPerRound; }
  set { _ageGeneratedPerRound = value; }
}
public Real OverloadTime
{
  get { return _overloadTime; }
  set { _overloadTime = value; }
}
public TriggersBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(8);
__unnamed2 = new Pad(6);
__unnamed3 = new Pad(8);
__unnamed4 = new Pad(2);
__unnamed5 = new Pad(4);
__unnamed6 = new Pad(4);
__unnamed7 = new Pad(12);
__unnamed8 = new Pad(4);
__unnamed9 = new Pad(8);
__unnamed10 = new Pad(32);
__unnamed11 = new Pad(24);
_firingEffectsCollection = new TriggerFiringEffectBlockCollection(_firingEffects);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _roundsPerSecond.Read(reader);
  _accelerationTime.Read(reader);
  _decelerationTime.Read(reader);
  _blurredRateOfFire.Read(reader);
  __unnamed.Read(reader);
  _magazine.Read(reader);
  _roundsPerShot.Read(reader);
  _minimumRoundsLoaded.Read(reader);
  _roundsBetweenTracers.Read(reader);
  __unnamed2.Read(reader);
  _firingNoise.Read(reader);
  _error.Read(reader);
  _accelerationTime2.Read(reader);
  _decelerationTime2.Read(reader);
  __unnamed3.Read(reader);
  _chargingTime.Read(reader);
  _chargedTime.Read(reader);
  _overchargedAction.Read(reader);
  __unnamed4.Read(reader);
  _chargedIllumination.Read(reader);
  _spewTime.Read(reader);
  _chargingEffect.Read(reader);
  _distributionFunction.Read(reader);
  _projectilesPerShot.Read(reader);
  _distributionAngle.Read(reader);
  __unnamed5.Read(reader);
  _minimumError.Read(reader);
  _errorAngle.Read(reader);
  _firstPersonOffset.Read(reader);
  __unnamed6.Read(reader);
  _projectile.Read(reader);
  _ejectionPortRecoveryTime.Read(reader);
  _illuminationRecoveryTime.Read(reader);
  __unnamed7.Read(reader);
  _heatGeneratedPerRound.Read(reader);
  _ageGeneratedPerRound.Read(reader);
  __unnamed8.Read(reader);
  _overloadTime.Read(reader);
  __unnamed9.Read(reader);
  __unnamed10.Read(reader);
  __unnamed11.Read(reader);
  _firingEffects.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_chargingEffect.ReadString(reader);
_projectile.ReadString(reader);
for (int x=0; x<_firingEffects.Count; x++)
{
  FiringEffects.AddNew();
  FiringEffects[x].Read(reader);
}
for (int x=0; x<_firingEffects.Count; x++)
  FiringEffects[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _roundsPerSecond.Write(writer);
    _accelerationTime.Write(writer);
    _decelerationTime.Write(writer);
    _blurredRateOfFire.Write(writer);
    __unnamed.Write(writer);
    _magazine.Write(writer);
    _roundsPerShot.Write(writer);
    _minimumRoundsLoaded.Write(writer);
    _roundsBetweenTracers.Write(writer);
    __unnamed2.Write(writer);
    _firingNoise.Write(writer);
    _error.Write(writer);
    _accelerationTime2.Write(writer);
    _decelerationTime2.Write(writer);
    __unnamed3.Write(writer);
    _chargingTime.Write(writer);
    _chargedTime.Write(writer);
    _overchargedAction.Write(writer);
    __unnamed4.Write(writer);
    _chargedIllumination.Write(writer);
    _spewTime.Write(writer);
    _chargingEffect.Write(writer);
    _distributionFunction.Write(writer);
    _projectilesPerShot.Write(writer);
    _distributionAngle.Write(writer);
    __unnamed5.Write(writer);
    _minimumError.Write(writer);
    _errorAngle.Write(writer);
    _firstPersonOffset.Write(writer);
    __unnamed6.Write(writer);
    _projectile.Write(writer);
    _ejectionPortRecoveryTime.Write(writer);
    _illuminationRecoveryTime.Write(writer);
    __unnamed7.Write(writer);
    _heatGeneratedPerRound.Write(writer);
    _ageGeneratedPerRound.Write(writer);
    __unnamed8.Write(writer);
    _overloadTime.Write(writer);
    __unnamed9.Write(writer);
    __unnamed10.Write(writer);
    __unnamed11.Write(writer);
    _firingEffects.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_chargingEffect.WriteString(writer);
_projectile.WriteString(writer);
_firingEffects.UpdateReflexiveOffset(writer);
for (int x=0; x<_firingEffects.Count; x++)
{
  FiringEffects[x].Write(writer);
}
for (int x=0; x<_firingEffects.Count; x++)
  FiringEffects[x].WriteChildData(writer);
}
}
public class TriggerFiringEffectBlock : IBlock
{
private ShortInteger _shotCountLowerBound = new ShortInteger();
private ShortInteger _shotCountUpperBound = new ShortInteger();
private Pad  __unnamed;	
private TagReference _firingEffect = new TagReference();
private TagReference _misfireEffect = new TagReference();
private TagReference _emptyEffect = new TagReference();
private TagReference _firingDamage = new TagReference();
private TagReference _misfireDamage = new TagReference();
private TagReference _emptyDamage = new TagReference();
public ShortInteger ShotCountLowerBound
{
  get { return _shotCountLowerBound; }
  set { _shotCountLowerBound = value; }
}
public ShortInteger ShotCountUpperBound
{
  get { return _shotCountUpperBound; }
  set { _shotCountUpperBound = value; }
}
public TagReference FiringEffect
{
  get { return _firingEffect; }
  set { _firingEffect = value; }
}
public TagReference MisfireEffect
{
  get { return _misfireEffect; }
  set { _misfireEffect = value; }
}
public TagReference EmptyEffect
{
  get { return _emptyEffect; }
  set { _emptyEffect = value; }
}
public TagReference FiringDamage
{
  get { return _firingDamage; }
  set { _firingDamage = value; }
}
public TagReference MisfireDamage
{
  get { return _misfireDamage; }
  set { _misfireDamage = value; }
}
public TagReference EmptyDamage
{
  get { return _emptyDamage; }
  set { _emptyDamage = value; }
}
public TriggerFiringEffectBlock()
{
__unnamed = new Pad(32);

}
public void Read(BinaryReader reader)
{
  _shotCountLowerBound.Read(reader);
  _shotCountUpperBound.Read(reader);
  __unnamed.Read(reader);
  _firingEffect.Read(reader);
  _misfireEffect.Read(reader);
  _emptyEffect.Read(reader);
  _firingDamage.Read(reader);
  _misfireDamage.Read(reader);
  _emptyDamage.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_firingEffect.ReadString(reader);
_misfireEffect.ReadString(reader);
_emptyEffect.ReadString(reader);
_firingDamage.ReadString(reader);
_misfireDamage.ReadString(reader);
_emptyDamage.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _shotCountLowerBound.Write(writer);
    _shotCountUpperBound.Write(writer);
    __unnamed.Write(writer);
    _firingEffect.Write(writer);
    _misfireEffect.Write(writer);
    _emptyEffect.Write(writer);
    _firingDamage.Write(writer);
    _misfireDamage.Write(writer);
    _emptyDamage.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_firingEffect.WriteString(writer);
_misfireEffect.WriteString(writer);
_emptyEffect.WriteString(writer);
_firingDamage.WriteString(writer);
_misfireDamage.WriteString(writer);
_emptyDamage.WriteString(writer);
}
}
  }
}
