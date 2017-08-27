using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class ActorVariant : IBlock
  {
    public ActorVariantBlock ActorVariantValues = new ActorVariantBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'ActorVariant'------------------------------------------------------");
      ActorVariantValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      ActorVariantValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      ActorVariantValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      ActorVariantValues.WriteChildData(writer);
    }
public class ActorVariantBlock : IBlock
{
private Flags  _flags;	
private TagReference _actorDefinition = new TagReference();
private TagReference _unit = new TagReference();
private TagReference _majorVariant = new TagReference();
private Pad  __unnamed;	
private Enum _movementType = new Enum();
private Pad  __unnamed2;	
private Real _initialCrouchChance = new Real();
private RealBounds _crouchTime = new RealBounds();
private RealBounds _runTime = new RealBounds();
private TagReference _weapon = new TagReference();
private Real _maximumFiringDistance = new Real();
private Real _rateOfFire = new Real();
private Angle _projectileError = new Angle();
private RealBounds _firstBurstDelayTime = new RealBounds();
private Real _ne = new Real();
private Real _surpriseDelayTime = new Real();
private Real _surpriseFir = new Real();
private Real _deathFir = new Real();
private Real _deathFir2 = new Real();
private RealBounds _desiredCombatRange = new RealBounds();
private RealVector3D _customStandGunOffset = new RealVector3D();
private RealVector3D _customCrouchGunOffset = new RealVector3D();
private Real _targetTracking = new Real();
private Real _targetLeading = new Real();
private Real _weaponDamageModifier = new Real();
private Real _damagePerSecond = new Real();
private Real _burstOriginRadius = new Real();
private Angle _burstOriginAngle = new Angle();
private RealBounds _burstReturnLength = new RealBounds();
private Angle _burstReturnAngle = new Angle();
private RealBounds _burstDuration = new RealBounds();
private RealBounds _burstSeparation = new RealBounds();
private Angle _burstAngularVelocity = new Angle();
private Pad  __unnamed3;	
private Real _specialDamageModifier = new Real();
private Angle _specialProjectileError = new Angle();
private Real _ne2 = new Real();
private Real _ne3 = new Real();
private Real _ne4 = new Real();
private Real _ne5 = new Real();
private Pad  __unnamed4;	
private Real _movingBurstDuration = new Real();
private Real _movingBurstSeparation = new Real();
private Real _movingRateOfFire = new Real();
private Real _movingProjectileError = new Real();
private Pad  __unnamed5;	
private Real _berserkBurstDuration = new Real();
private Real _berserkBurstSeparation = new Real();
private Real _berserkRateOfFire = new Real();
private Real _berserkProjectileError = new Real();
private Pad  __unnamed6;	
private Real _supe = new Real();
private Real _bombardmentRange = new Real();
private Real _modifiedVisionRange = new Real();
private Enum _specia = new Enum();
private Enum _specia2 = new Enum();
private Real _specia3 = new Real();
private Real _specia4 = new Real();
private Real _meleeRange = new Real();
private Real _meleeAbortRange = new Real();
private RealBounds _berserkFiringRanges = new RealBounds();
private Real _berserkMeleeRange = new Real();
private Real _berserkMeleeAbortRange = new Real();
private Pad  __unnamed7;	
private Enum _grenadeType = new Enum();
private Enum _trajectoryType = new Enum();
private Enum _grenadeStimulus = new Enum();
private ShortInteger _minimumEnemyCount = new ShortInteger();
private Real _enemyRadius = new Real();
private Pad  __unnamed8;	
private Real _grenadeVelocity = new Real();
private RealBounds _grenadeRanges = new RealBounds();
private Real _collateralDamageRadius = new Real();
private RealFraction _grenadeChance = new RealFraction();
private Real _grenadeCheckTime = new Real();
private Real _encounterGrenadeTimeout = new Real();
private Pad  __unnamed9;	
private TagReference _equipment = new TagReference();
private ShortBounds _grenadeCount = new ShortBounds();
private Real _dontDropGrenadesChance = new Real();
private RealBounds _dropWeaponLoaded = new RealBounds();
private ShortBounds _dropWeaponAmmo = new ShortBounds();
private Pad  __unnamed10;	
private Pad  __unnamed11;	
private Real _bodyVitality = new Real();
private Real _shieldVitality = new Real();
private Real _shieldSappingRadius = new Real();
private ShortInteger _forcedShaderPermutation = new ShortInteger();
private Pad  __unnamed12;	
private Pad  __unnamed13;	
private Pad  __unnamed14;	
private Block _changeColors = new Block();
public class ActorVariantChangeColorsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ActorVariantChangeColorsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ActorVariantChangeColorsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ActorVariantChangeColorsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ActorVariantChangeColorsBlock this[int index]
  {
    get { return (InnerList[index] as ActorVariantChangeColorsBlock); }
  }
}
private ActorVariantChangeColorsBlockCollection _changeColorsCollection;
public ActorVariantChangeColorsBlockCollection ChangeColors
{
  get { return _changeColorsCollection; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public TagReference ActorDefinition
{
  get { return _actorDefinition; }
  set { _actorDefinition = value; }
}
public TagReference Unit
{
  get { return _unit; }
  set { _unit = value; }
}
public TagReference MajorVariant
{
  get { return _majorVariant; }
  set { _majorVariant = value; }
}
public Enum MovementType
{
  get { return _movementType; }
  set { _movementType = value; }
}
public Real InitialCrouchChance
{
  get { return _initialCrouchChance; }
  set { _initialCrouchChance = value; }
}
public RealBounds CrouchTime
{
  get { return _crouchTime; }
  set { _crouchTime = value; }
}
public RealBounds RunTime
{
  get { return _runTime; }
  set { _runTime = value; }
}
public TagReference Weapon
{
  get { return _weapon; }
  set { _weapon = value; }
}
public Real MaximumFiringDistance
{
  get { return _maximumFiringDistance; }
  set { _maximumFiringDistance = value; }
}
public Real RateOfFire
{
  get { return _rateOfFire; }
  set { _rateOfFire = value; }
}
public Angle ProjectileError
{
  get { return _projectileError; }
  set { _projectileError = value; }
}
public RealBounds FirstBurstDelayTime
{
  get { return _firstBurstDelayTime; }
  set { _firstBurstDelayTime = value; }
}
public Real Ne
{
  get { return _ne; }
  set { _ne = value; }
}
public Real SurpriseDelayTime
{
  get { return _surpriseDelayTime; }
  set { _surpriseDelayTime = value; }
}
public Real SurpriseFir
{
  get { return _surpriseFir; }
  set { _surpriseFir = value; }
}
public Real DeathFir
{
  get { return _deathFir; }
  set { _deathFir = value; }
}
public Real DeathFir2
{
  get { return _deathFir2; }
  set { _deathFir2 = value; }
}
public RealBounds DesiredCombatRange
{
  get { return _desiredCombatRange; }
  set { _desiredCombatRange = value; }
}
public RealVector3D CustomStandGunOffset
{
  get { return _customStandGunOffset; }
  set { _customStandGunOffset = value; }
}
public RealVector3D CustomCrouchGunOffset
{
  get { return _customCrouchGunOffset; }
  set { _customCrouchGunOffset = value; }
}
public Real TargetTracking
{
  get { return _targetTracking; }
  set { _targetTracking = value; }
}
public Real TargetLeading
{
  get { return _targetLeading; }
  set { _targetLeading = value; }
}
public Real WeaponDamageModifier
{
  get { return _weaponDamageModifier; }
  set { _weaponDamageModifier = value; }
}
public Real DamagePerSecond
{
  get { return _damagePerSecond; }
  set { _damagePerSecond = value; }
}
public Real BurstOriginRadius
{
  get { return _burstOriginRadius; }
  set { _burstOriginRadius = value; }
}
public Angle BurstOriginAngle
{
  get { return _burstOriginAngle; }
  set { _burstOriginAngle = value; }
}
public RealBounds BurstReturnLength
{
  get { return _burstReturnLength; }
  set { _burstReturnLength = value; }
}
public Angle BurstReturnAngle
{
  get { return _burstReturnAngle; }
  set { _burstReturnAngle = value; }
}
public RealBounds BurstDuration
{
  get { return _burstDuration; }
  set { _burstDuration = value; }
}
public RealBounds BurstSeparation
{
  get { return _burstSeparation; }
  set { _burstSeparation = value; }
}
public Angle BurstAngularVelocity
{
  get { return _burstAngularVelocity; }
  set { _burstAngularVelocity = value; }
}
public Real SpecialDamageModifier
{
  get { return _specialDamageModifier; }
  set { _specialDamageModifier = value; }
}
public Angle SpecialProjectileError
{
  get { return _specialProjectileError; }
  set { _specialProjectileError = value; }
}
public Real Ne2
{
  get { return _ne2; }
  set { _ne2 = value; }
}
public Real Ne3
{
  get { return _ne3; }
  set { _ne3 = value; }
}
public Real Ne4
{
  get { return _ne4; }
  set { _ne4 = value; }
}
public Real Ne5
{
  get { return _ne5; }
  set { _ne5 = value; }
}
public Real MovingBurstDuration
{
  get { return _movingBurstDuration; }
  set { _movingBurstDuration = value; }
}
public Real MovingBurstSeparation
{
  get { return _movingBurstSeparation; }
  set { _movingBurstSeparation = value; }
}
public Real MovingRateOfFire
{
  get { return _movingRateOfFire; }
  set { _movingRateOfFire = value; }
}
public Real MovingProjectileError
{
  get { return _movingProjectileError; }
  set { _movingProjectileError = value; }
}
public Real BerserkBurstDuration
{
  get { return _berserkBurstDuration; }
  set { _berserkBurstDuration = value; }
}
public Real BerserkBurstSeparation
{
  get { return _berserkBurstSeparation; }
  set { _berserkBurstSeparation = value; }
}
public Real BerserkRateOfFire
{
  get { return _berserkRateOfFire; }
  set { _berserkRateOfFire = value; }
}
public Real BerserkProjectileError
{
  get { return _berserkProjectileError; }
  set { _berserkProjectileError = value; }
}
public Real Supe
{
  get { return _supe; }
  set { _supe = value; }
}
public Real BombardmentRange
{
  get { return _bombardmentRange; }
  set { _bombardmentRange = value; }
}
public Real ModifiedVisionRange
{
  get { return _modifiedVisionRange; }
  set { _modifiedVisionRange = value; }
}
public Enum Specia
{
  get { return _specia; }
  set { _specia = value; }
}
public Enum Specia2
{
  get { return _specia2; }
  set { _specia2 = value; }
}
public Real Specia3
{
  get { return _specia3; }
  set { _specia3 = value; }
}
public Real Specia4
{
  get { return _specia4; }
  set { _specia4 = value; }
}
public Real MeleeRange
{
  get { return _meleeRange; }
  set { _meleeRange = value; }
}
public Real MeleeAbortRange
{
  get { return _meleeAbortRange; }
  set { _meleeAbortRange = value; }
}
public RealBounds BerserkFiringRanges
{
  get { return _berserkFiringRanges; }
  set { _berserkFiringRanges = value; }
}
public Real BerserkMeleeRange
{
  get { return _berserkMeleeRange; }
  set { _berserkMeleeRange = value; }
}
public Real BerserkMeleeAbortRange
{
  get { return _berserkMeleeAbortRange; }
  set { _berserkMeleeAbortRange = value; }
}
public Enum GrenadeType
{
  get { return _grenadeType; }
  set { _grenadeType = value; }
}
public Enum TrajectoryType
{
  get { return _trajectoryType; }
  set { _trajectoryType = value; }
}
public Enum GrenadeStimulus
{
  get { return _grenadeStimulus; }
  set { _grenadeStimulus = value; }
}
public ShortInteger MinimumEnemyCount
{
  get { return _minimumEnemyCount; }
  set { _minimumEnemyCount = value; }
}
public Real EnemyRadius
{
  get { return _enemyRadius; }
  set { _enemyRadius = value; }
}
public Real GrenadeVelocity
{
  get { return _grenadeVelocity; }
  set { _grenadeVelocity = value; }
}
public RealBounds GrenadeRanges
{
  get { return _grenadeRanges; }
  set { _grenadeRanges = value; }
}
public Real CollateralDamageRadius
{
  get { return _collateralDamageRadius; }
  set { _collateralDamageRadius = value; }
}
public RealFraction GrenadeChance
{
  get { return _grenadeChance; }
  set { _grenadeChance = value; }
}
public Real GrenadeCheckTime
{
  get { return _grenadeCheckTime; }
  set { _grenadeCheckTime = value; }
}
public Real EncounterGrenadeTimeout
{
  get { return _encounterGrenadeTimeout; }
  set { _encounterGrenadeTimeout = value; }
}
public TagReference Equipment
{
  get { return _equipment; }
  set { _equipment = value; }
}
public ShortBounds GrenadeCount
{
  get { return _grenadeCount; }
  set { _grenadeCount = value; }
}
public Real DontDropGrenadesChance
{
  get { return _dontDropGrenadesChance; }
  set { _dontDropGrenadesChance = value; }
}
public RealBounds DropWeaponLoaded
{
  get { return _dropWeaponLoaded; }
  set { _dropWeaponLoaded = value; }
}
public ShortBounds DropWeaponAmmo
{
  get { return _dropWeaponAmmo; }
  set { _dropWeaponAmmo = value; }
}
public Real BodyVitality
{
  get { return _bodyVitality; }
  set { _bodyVitality = value; }
}
public Real ShieldVitality
{
  get { return _shieldVitality; }
  set { _shieldVitality = value; }
}
public Real ShieldSappingRadius
{
  get { return _shieldSappingRadius; }
  set { _shieldSappingRadius = value; }
}
public ShortInteger ForcedShaderPermutation
{
  get { return _forcedShaderPermutation; }
  set { _forcedShaderPermutation = value; }
}
public ActorVariantBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(24);
__unnamed2 = new Pad(2);
__unnamed3 = new Pad(4);
__unnamed4 = new Pad(8);
__unnamed5 = new Pad(8);
__unnamed6 = new Pad(8);
__unnamed7 = new Pad(8);
__unnamed8 = new Pad(4);
__unnamed9 = new Pad(20);
__unnamed10 = new Pad(12);
__unnamed11 = new Pad(16);
__unnamed12 = new Pad(2);
__unnamed13 = new Pad(16);
__unnamed14 = new Pad(12);
_changeColorsCollection = new ActorVariantChangeColorsBlockCollection(_changeColors);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _actorDefinition.Read(reader);
  _unit.Read(reader);
  _majorVariant.Read(reader);
  __unnamed.Read(reader);
  _movementType.Read(reader);
  __unnamed2.Read(reader);
  _initialCrouchChance.Read(reader);
  _crouchTime.Read(reader);
  _runTime.Read(reader);
  _weapon.Read(reader);
  _maximumFiringDistance.Read(reader);
  _rateOfFire.Read(reader);
  _projectileError.Read(reader);
  _firstBurstDelayTime.Read(reader);
  _ne.Read(reader);
  _surpriseDelayTime.Read(reader);
  _surpriseFir.Read(reader);
  _deathFir.Read(reader);
  _deathFir2.Read(reader);
  _desiredCombatRange.Read(reader);
  _customStandGunOffset.Read(reader);
  _customCrouchGunOffset.Read(reader);
  _targetTracking.Read(reader);
  _targetLeading.Read(reader);
  _weaponDamageModifier.Read(reader);
  _damagePerSecond.Read(reader);
  _burstOriginRadius.Read(reader);
  _burstOriginAngle.Read(reader);
  _burstReturnLength.Read(reader);
  _burstReturnAngle.Read(reader);
  _burstDuration.Read(reader);
  _burstSeparation.Read(reader);
  _burstAngularVelocity.Read(reader);
  __unnamed3.Read(reader);
  _specialDamageModifier.Read(reader);
  _specialProjectileError.Read(reader);
  _ne2.Read(reader);
  _ne3.Read(reader);
  _ne4.Read(reader);
  _ne5.Read(reader);
  __unnamed4.Read(reader);
  _movingBurstDuration.Read(reader);
  _movingBurstSeparation.Read(reader);
  _movingRateOfFire.Read(reader);
  _movingProjectileError.Read(reader);
  __unnamed5.Read(reader);
  _berserkBurstDuration.Read(reader);
  _berserkBurstSeparation.Read(reader);
  _berserkRateOfFire.Read(reader);
  _berserkProjectileError.Read(reader);
  __unnamed6.Read(reader);
  _supe.Read(reader);
  _bombardmentRange.Read(reader);
  _modifiedVisionRange.Read(reader);
  _specia.Read(reader);
  _specia2.Read(reader);
  _specia3.Read(reader);
  _specia4.Read(reader);
  _meleeRange.Read(reader);
  _meleeAbortRange.Read(reader);
  _berserkFiringRanges.Read(reader);
  _berserkMeleeRange.Read(reader);
  _berserkMeleeAbortRange.Read(reader);
  __unnamed7.Read(reader);
  _grenadeType.Read(reader);
  _trajectoryType.Read(reader);
  _grenadeStimulus.Read(reader);
  _minimumEnemyCount.Read(reader);
  _enemyRadius.Read(reader);
  __unnamed8.Read(reader);
  _grenadeVelocity.Read(reader);
  _grenadeRanges.Read(reader);
  _collateralDamageRadius.Read(reader);
  _grenadeChance.Read(reader);
  _grenadeCheckTime.Read(reader);
  _encounterGrenadeTimeout.Read(reader);
  __unnamed9.Read(reader);
  _equipment.Read(reader);
  _grenadeCount.Read(reader);
  _dontDropGrenadesChance.Read(reader);
  _dropWeaponLoaded.Read(reader);
  _dropWeaponAmmo.Read(reader);
  __unnamed10.Read(reader);
  __unnamed11.Read(reader);
  _bodyVitality.Read(reader);
  _shieldVitality.Read(reader);
  _shieldSappingRadius.Read(reader);
  _forcedShaderPermutation.Read(reader);
  __unnamed12.Read(reader);
  __unnamed13.Read(reader);
  __unnamed14.Read(reader);
  _changeColors.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_actorDefinition.ReadString(reader);
_unit.ReadString(reader);
_majorVariant.ReadString(reader);
_weapon.ReadString(reader);
_equipment.ReadString(reader);
for (int x=0; x<_changeColors.Count; x++)
{
  ChangeColors.AddNew();
  ChangeColors[x].Read(reader);
}
for (int x=0; x<_changeColors.Count; x++)
  ChangeColors[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _actorDefinition.Write(writer);
    _unit.Write(writer);
    _majorVariant.Write(writer);
    __unnamed.Write(writer);
    _movementType.Write(writer);
    __unnamed2.Write(writer);
    _initialCrouchChance.Write(writer);
    _crouchTime.Write(writer);
    _runTime.Write(writer);
    _weapon.Write(writer);
    _maximumFiringDistance.Write(writer);
    _rateOfFire.Write(writer);
    _projectileError.Write(writer);
    _firstBurstDelayTime.Write(writer);
    _ne.Write(writer);
    _surpriseDelayTime.Write(writer);
    _surpriseFir.Write(writer);
    _deathFir.Write(writer);
    _deathFir2.Write(writer);
    _desiredCombatRange.Write(writer);
    _customStandGunOffset.Write(writer);
    _customCrouchGunOffset.Write(writer);
    _targetTracking.Write(writer);
    _targetLeading.Write(writer);
    _weaponDamageModifier.Write(writer);
    _damagePerSecond.Write(writer);
    _burstOriginRadius.Write(writer);
    _burstOriginAngle.Write(writer);
    _burstReturnLength.Write(writer);
    _burstReturnAngle.Write(writer);
    _burstDuration.Write(writer);
    _burstSeparation.Write(writer);
    _burstAngularVelocity.Write(writer);
    __unnamed3.Write(writer);
    _specialDamageModifier.Write(writer);
    _specialProjectileError.Write(writer);
    _ne2.Write(writer);
    _ne3.Write(writer);
    _ne4.Write(writer);
    _ne5.Write(writer);
    __unnamed4.Write(writer);
    _movingBurstDuration.Write(writer);
    _movingBurstSeparation.Write(writer);
    _movingRateOfFire.Write(writer);
    _movingProjectileError.Write(writer);
    __unnamed5.Write(writer);
    _berserkBurstDuration.Write(writer);
    _berserkBurstSeparation.Write(writer);
    _berserkRateOfFire.Write(writer);
    _berserkProjectileError.Write(writer);
    __unnamed6.Write(writer);
    _supe.Write(writer);
    _bombardmentRange.Write(writer);
    _modifiedVisionRange.Write(writer);
    _specia.Write(writer);
    _specia2.Write(writer);
    _specia3.Write(writer);
    _specia4.Write(writer);
    _meleeRange.Write(writer);
    _meleeAbortRange.Write(writer);
    _berserkFiringRanges.Write(writer);
    _berserkMeleeRange.Write(writer);
    _berserkMeleeAbortRange.Write(writer);
    __unnamed7.Write(writer);
    _grenadeType.Write(writer);
    _trajectoryType.Write(writer);
    _grenadeStimulus.Write(writer);
    _minimumEnemyCount.Write(writer);
    _enemyRadius.Write(writer);
    __unnamed8.Write(writer);
    _grenadeVelocity.Write(writer);
    _grenadeRanges.Write(writer);
    _collateralDamageRadius.Write(writer);
    _grenadeChance.Write(writer);
    _grenadeCheckTime.Write(writer);
    _encounterGrenadeTimeout.Write(writer);
    __unnamed9.Write(writer);
    _equipment.Write(writer);
    _grenadeCount.Write(writer);
    _dontDropGrenadesChance.Write(writer);
    _dropWeaponLoaded.Write(writer);
    _dropWeaponAmmo.Write(writer);
    __unnamed10.Write(writer);
    __unnamed11.Write(writer);
    _bodyVitality.Write(writer);
    _shieldVitality.Write(writer);
    _shieldSappingRadius.Write(writer);
    _forcedShaderPermutation.Write(writer);
    __unnamed12.Write(writer);
    __unnamed13.Write(writer);
    __unnamed14.Write(writer);
    _changeColors.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_actorDefinition.WriteString(writer);
_unit.WriteString(writer);
_majorVariant.WriteString(writer);
_weapon.WriteString(writer);
_equipment.WriteString(writer);
_changeColors.UpdateReflexiveOffset(writer);
for (int x=0; x<_changeColors.Count; x++)
{
  ChangeColors[x].Write(writer);
}
for (int x=0; x<_changeColors.Count; x++)
  ChangeColors[x].WriteChildData(writer);
}
}
public class ActorVariantChangeColorsBlock : IBlock
{
private RealRGBColor _colorLowerBound = new RealRGBColor();
private RealRGBColor _colorUpperBound = new RealRGBColor();
private Pad  __unnamed;	
public RealRGBColor ColorLowerBound
{
  get { return _colorLowerBound; }
  set { _colorLowerBound = value; }
}
public RealRGBColor ColorUpperBound
{
  get { return _colorUpperBound; }
  set { _colorUpperBound = value; }
}
public ActorVariantChangeColorsBlock()
{
__unnamed = new Pad(8);

}
public void Read(BinaryReader reader)
{
  _colorLowerBound.Read(reader);
  _colorUpperBound.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _colorLowerBound.Write(writer);
    _colorUpperBound.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
  }
}
