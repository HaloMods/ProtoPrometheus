using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Actor : IBlock
  {
    public ActorBlock ActorValues = new ActorBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'Actor'------------------------------------------------------");
      ActorValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      ActorValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      ActorValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      ActorValues.WriteChildData(writer);
    }
public class ActorBlock : IBlock
{
private Flags  _flags;	
private Flags  _moreFlags;	
private Pad  __unnamed;	
private Enum _type = new Enum();
private Pad  __unnamed2;	
private Real _maxVisionDistance = new Real();
private Angle _centralVisionAngle = new Angle();
private Angle _maxVisionAngle = new Angle();
private Pad  __unnamed3;	
private Angle _peripheralVisionAngle = new Angle();
private Real _peripheralDistance = new Real();
private Pad  __unnamed4;	
private RealVector3D _standingGunOffset = new RealVector3D();
private RealVector3D _crouchingGunOffset = new RealVector3D();
private Real _hearingDistance = new Real();
private Real _noticeProjectileChance = new Real();
private Real _noticeVehicleChance = new Real();
private Pad  __unnamed5;	
private Real _combatPerceptionTime = new Real();
private Real _guardPerceptionTime = new Real();
private Real _no = new Real();
private Pad  __unnamed6;	
private Pad  __unnamed7;	
private Real _diveIntoCoverChance = new Real();
private Real _emergeFromCoverChance = new Real();
private Real _diveFromGrenadeChance = new Real();
private Real _pathfindingRadius = new Real();
private Real _glassIgnoranceChance = new Real();
private Real _stationaryMovementDist = new Real();
private Real _fre = new Real();
private Angle _beginMovingAngle = new Angle();
private Pad  __unnamed8;	
private RealEulerAngles2D _maximumAimingDeviation = new RealEulerAngles2D();
private RealEulerAngles2D _maximumLookingDeviation = new RealEulerAngles2D();
private Angle _noncombatLookDeltaL = new Angle();
private Angle _noncombatLookDeltaR = new Angle();
private Angle _combatLookDeltaL = new Angle();
private Angle _combatLookDeltaR = new Angle();
private RealEulerAngles2D _idleAimingRange = new RealEulerAngles2D();
private RealEulerAngles2D _idleLookingRange = new RealEulerAngles2D();
private RealBounds _eventLookTimeModifier = new RealBounds();
private RealBounds _noncombatIdleFacing = new RealBounds();
private RealBounds _noncombatIdleAiming = new RealBounds();
private RealBounds _noncombatIdleLooking = new RealBounds();
private RealBounds _guardIdleFacing = new RealBounds();
private RealBounds _guardIdleAiming = new RealBounds();
private RealBounds _guardIdleLooking = new RealBounds();
private RealBounds _combatIdleFacing = new RealBounds();
private RealBounds _combatIdleAiming = new RealBounds();
private RealBounds _combatIdleLooking = new RealBounds();
private Pad  __unnamed9;	
private Pad  __unnamed10;	
private TagReference _dONOTUSE = new TagReference();
private Pad  __unnamed11;	
private TagReference _dONOTUSE2 = new TagReference();
private Enum _unreachableDangerTrigger = new Enum();
private Enum _vehicleDangerTrigger = new Enum();
private Enum _playerDangerTrigger = new Enum();
private Pad  __unnamed12;	
private RealBounds _dangerTriggerTime = new RealBounds();
private ShortInteger _friendsKilledTrigger = new ShortInteger();
private ShortInteger _friendsRetreatingTrigger = new ShortInteger();
private Pad  __unnamed13;	
private RealBounds _retreatTime = new RealBounds();
private Pad  __unnamed14;	
private RealBounds _coweringTime = new RealBounds();
private Real _friendKilledPanicChance = new Real();
private Enum _leaderType = new Enum();
private Pad  __unnamed15;	
private Real _leaderKilledPanicChance = new Real();
private Real _panicDamageThreshold = new Real();
private Real _surpriseDistance = new Real();
private Pad  __unnamed16;	
private RealBounds _hideBehindCoverTime = new RealBounds();
private Real _hideTarge = new Real();
private Real _hideShieldFraction = new Real();
private Real _attackShieldFraction = new Real();
private Real _pursueShieldFraction = new Real();
private Pad  __unnamed17;	
private Enum _defensiveCrouchType = new Enum();
private Pad  __unnamed18;	
private Real _attackingCrouchThreshold = new Real();
private Real _defendingCrouchThreshold = new Real();
private Real _minStandTime = new Real();
private Real _minCrouchTime = new Real();
private Real _defendingHideTimeModifier = new Real();
private Real _attackingEvasionThreshold = new Real();
private Real _defendingEvasionThreshold = new Real();
private Real _evasionSee = new Real();
private Real _evasionDelayTime = new Real();
private Real _maxSee = new Real();
private Real _coverDamageThreshold = new Real();
private Real _stalkingDiscoveryTime = new Real();
private Real _stalkingMaxDistance = new Real();
private Angle _stationaryFacingAngle = new Angle();
private Real _chang = new Real();
private Pad  __unnamed19;	
private RealBounds _uncoverDelayTime = new RealBounds();
private RealBounds _targetSearchTime = new RealBounds();
private RealBounds _pursui = new RealBounds();
private ShortInteger _numPositionsCoord = new ShortInteger();
private ShortInteger _numPositionsNormal = new ShortInteger();
private Pad  __unnamed20;	
private Real _meleeAttackDelay = new Real();
private Real _meleeFudgeFactor = new Real();
private Real _meleeChargeTime = new Real();
private RealBounds _meleeLeapRange = new RealBounds();
private Real _meleeLeapVelocity = new Real();
private Real _meleeLeapChance = new Real();
private Real _meleeLeapBallistic = new Real();
private Real _berserkDamageAmount = new Real();
private Real _berserkDamageThreshold = new Real();
private Real _berserkProximity = new Real();
private Real _suicideSensingDist = new Real();
private Real _berserkGrenadeChance = new Real();
private Pad  __unnamed21;	
private RealBounds _guardPositionTime = new RealBounds();
private RealBounds _combatPositionTime = new RealBounds();
private Real _oldPositionAvoidDist = new Real();
private Real _friendAvoidDist = new Real();
private Pad  __unnamed22;	
private RealBounds _noncombatIdleSpeechTime = new RealBounds();
private RealBounds _combatIdleSpeechTime = new RealBounds();
private Pad  __unnamed23;	
private Pad  __unnamed24;	
private TagReference _dONOTUSE3 = new TagReference();
private Pad  __unnamed25;	
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Flags MoreFlags
{
  get { return _moreFlags; }
  set { _moreFlags = value; }
}
public Enum Type
{
  get { return _type; }
  set { _type = value; }
}
public Real MaxVisionDistance
{
  get { return _maxVisionDistance; }
  set { _maxVisionDistance = value; }
}
public Angle CentralVisionAngle
{
  get { return _centralVisionAngle; }
  set { _centralVisionAngle = value; }
}
public Angle MaxVisionAngle
{
  get { return _maxVisionAngle; }
  set { _maxVisionAngle = value; }
}
public Angle PeripheralVisionAngle
{
  get { return _peripheralVisionAngle; }
  set { _peripheralVisionAngle = value; }
}
public Real PeripheralDistance
{
  get { return _peripheralDistance; }
  set { _peripheralDistance = value; }
}
public RealVector3D StandingGunOffset
{
  get { return _standingGunOffset; }
  set { _standingGunOffset = value; }
}
public RealVector3D CrouchingGunOffset
{
  get { return _crouchingGunOffset; }
  set { _crouchingGunOffset = value; }
}
public Real HearingDistance
{
  get { return _hearingDistance; }
  set { _hearingDistance = value; }
}
public Real NoticeProjectileChance
{
  get { return _noticeProjectileChance; }
  set { _noticeProjectileChance = value; }
}
public Real NoticeVehicleChance
{
  get { return _noticeVehicleChance; }
  set { _noticeVehicleChance = value; }
}
public Real CombatPerceptionTime
{
  get { return _combatPerceptionTime; }
  set { _combatPerceptionTime = value; }
}
public Real GuardPerceptionTime
{
  get { return _guardPerceptionTime; }
  set { _guardPerceptionTime = value; }
}
public Real No
{
  get { return _no; }
  set { _no = value; }
}
public Real DiveIntoCoverChance
{
  get { return _diveIntoCoverChance; }
  set { _diveIntoCoverChance = value; }
}
public Real EmergeFromCoverChance
{
  get { return _emergeFromCoverChance; }
  set { _emergeFromCoverChance = value; }
}
public Real DiveFromGrenadeChance
{
  get { return _diveFromGrenadeChance; }
  set { _diveFromGrenadeChance = value; }
}
public Real PathfindingRadius
{
  get { return _pathfindingRadius; }
  set { _pathfindingRadius = value; }
}
public Real GlassIgnoranceChance
{
  get { return _glassIgnoranceChance; }
  set { _glassIgnoranceChance = value; }
}
public Real StationaryMovementDist
{
  get { return _stationaryMovementDist; }
  set { _stationaryMovementDist = value; }
}
public Real Fre
{
  get { return _fre; }
  set { _fre = value; }
}
public Angle BeginMovingAngle
{
  get { return _beginMovingAngle; }
  set { _beginMovingAngle = value; }
}
public RealEulerAngles2D MaximumAimingDeviation
{
  get { return _maximumAimingDeviation; }
  set { _maximumAimingDeviation = value; }
}
public RealEulerAngles2D MaximumLookingDeviation
{
  get { return _maximumLookingDeviation; }
  set { _maximumLookingDeviation = value; }
}
public Angle NoncombatLookDeltaL
{
  get { return _noncombatLookDeltaL; }
  set { _noncombatLookDeltaL = value; }
}
public Angle NoncombatLookDeltaR
{
  get { return _noncombatLookDeltaR; }
  set { _noncombatLookDeltaR = value; }
}
public Angle CombatLookDeltaL
{
  get { return _combatLookDeltaL; }
  set { _combatLookDeltaL = value; }
}
public Angle CombatLookDeltaR
{
  get { return _combatLookDeltaR; }
  set { _combatLookDeltaR = value; }
}
public RealEulerAngles2D IdleAimingRange
{
  get { return _idleAimingRange; }
  set { _idleAimingRange = value; }
}
public RealEulerAngles2D IdleLookingRange
{
  get { return _idleLookingRange; }
  set { _idleLookingRange = value; }
}
public RealBounds EventLookTimeModifier
{
  get { return _eventLookTimeModifier; }
  set { _eventLookTimeModifier = value; }
}
public RealBounds NoncombatIdleFacing
{
  get { return _noncombatIdleFacing; }
  set { _noncombatIdleFacing = value; }
}
public RealBounds NoncombatIdleAiming
{
  get { return _noncombatIdleAiming; }
  set { _noncombatIdleAiming = value; }
}
public RealBounds NoncombatIdleLooking
{
  get { return _noncombatIdleLooking; }
  set { _noncombatIdleLooking = value; }
}
public RealBounds GuardIdleFacing
{
  get { return _guardIdleFacing; }
  set { _guardIdleFacing = value; }
}
public RealBounds GuardIdleAiming
{
  get { return _guardIdleAiming; }
  set { _guardIdleAiming = value; }
}
public RealBounds GuardIdleLooking
{
  get { return _guardIdleLooking; }
  set { _guardIdleLooking = value; }
}
public RealBounds CombatIdleFacing
{
  get { return _combatIdleFacing; }
  set { _combatIdleFacing = value; }
}
public RealBounds CombatIdleAiming
{
  get { return _combatIdleAiming; }
  set { _combatIdleAiming = value; }
}
public RealBounds CombatIdleLooking
{
  get { return _combatIdleLooking; }
  set { _combatIdleLooking = value; }
}
public TagReference DONOTUSE
{
  get { return _dONOTUSE; }
  set { _dONOTUSE = value; }
}
public TagReference DONOTUSE2
{
  get { return _dONOTUSE2; }
  set { _dONOTUSE2 = value; }
}
public Enum UnreachableDangerTrigger
{
  get { return _unreachableDangerTrigger; }
  set { _unreachableDangerTrigger = value; }
}
public Enum VehicleDangerTrigger
{
  get { return _vehicleDangerTrigger; }
  set { _vehicleDangerTrigger = value; }
}
public Enum PlayerDangerTrigger
{
  get { return _playerDangerTrigger; }
  set { _playerDangerTrigger = value; }
}
public RealBounds DangerTriggerTime
{
  get { return _dangerTriggerTime; }
  set { _dangerTriggerTime = value; }
}
public ShortInteger FriendsKilledTrigger
{
  get { return _friendsKilledTrigger; }
  set { _friendsKilledTrigger = value; }
}
public ShortInteger FriendsRetreatingTrigger
{
  get { return _friendsRetreatingTrigger; }
  set { _friendsRetreatingTrigger = value; }
}
public RealBounds RetreatTime
{
  get { return _retreatTime; }
  set { _retreatTime = value; }
}
public RealBounds CoweringTime
{
  get { return _coweringTime; }
  set { _coweringTime = value; }
}
public Real FriendKilledPanicChance
{
  get { return _friendKilledPanicChance; }
  set { _friendKilledPanicChance = value; }
}
public Enum LeaderType
{
  get { return _leaderType; }
  set { _leaderType = value; }
}
public Real LeaderKilledPanicChance
{
  get { return _leaderKilledPanicChance; }
  set { _leaderKilledPanicChance = value; }
}
public Real PanicDamageThreshold
{
  get { return _panicDamageThreshold; }
  set { _panicDamageThreshold = value; }
}
public Real SurpriseDistance
{
  get { return _surpriseDistance; }
  set { _surpriseDistance = value; }
}
public RealBounds HideBehindCoverTime
{
  get { return _hideBehindCoverTime; }
  set { _hideBehindCoverTime = value; }
}
public Real HideTarge
{
  get { return _hideTarge; }
  set { _hideTarge = value; }
}
public Real HideShieldFraction
{
  get { return _hideShieldFraction; }
  set { _hideShieldFraction = value; }
}
public Real AttackShieldFraction
{
  get { return _attackShieldFraction; }
  set { _attackShieldFraction = value; }
}
public Real PursueShieldFraction
{
  get { return _pursueShieldFraction; }
  set { _pursueShieldFraction = value; }
}
public Enum DefensiveCrouchType
{
  get { return _defensiveCrouchType; }
  set { _defensiveCrouchType = value; }
}
public Real AttackingCrouchThreshold
{
  get { return _attackingCrouchThreshold; }
  set { _attackingCrouchThreshold = value; }
}
public Real DefendingCrouchThreshold
{
  get { return _defendingCrouchThreshold; }
  set { _defendingCrouchThreshold = value; }
}
public Real MinStandTime
{
  get { return _minStandTime; }
  set { _minStandTime = value; }
}
public Real MinCrouchTime
{
  get { return _minCrouchTime; }
  set { _minCrouchTime = value; }
}
public Real DefendingHideTimeModifier
{
  get { return _defendingHideTimeModifier; }
  set { _defendingHideTimeModifier = value; }
}
public Real AttackingEvasionThreshold
{
  get { return _attackingEvasionThreshold; }
  set { _attackingEvasionThreshold = value; }
}
public Real DefendingEvasionThreshold
{
  get { return _defendingEvasionThreshold; }
  set { _defendingEvasionThreshold = value; }
}
public Real EvasionSee
{
  get { return _evasionSee; }
  set { _evasionSee = value; }
}
public Real EvasionDelayTime
{
  get { return _evasionDelayTime; }
  set { _evasionDelayTime = value; }
}
public Real MaxSee
{
  get { return _maxSee; }
  set { _maxSee = value; }
}
public Real CoverDamageThreshold
{
  get { return _coverDamageThreshold; }
  set { _coverDamageThreshold = value; }
}
public Real StalkingDiscoveryTime
{
  get { return _stalkingDiscoveryTime; }
  set { _stalkingDiscoveryTime = value; }
}
public Real StalkingMaxDistance
{
  get { return _stalkingMaxDistance; }
  set { _stalkingMaxDistance = value; }
}
public Angle StationaryFacingAngle
{
  get { return _stationaryFacingAngle; }
  set { _stationaryFacingAngle = value; }
}
public Real Chang
{
  get { return _chang; }
  set { _chang = value; }
}
public RealBounds UncoverDelayTime
{
  get { return _uncoverDelayTime; }
  set { _uncoverDelayTime = value; }
}
public RealBounds TargetSearchTime
{
  get { return _targetSearchTime; }
  set { _targetSearchTime = value; }
}
public RealBounds Pursui
{
  get { return _pursui; }
  set { _pursui = value; }
}
public ShortInteger NumPositionsCoord
{
  get { return _numPositionsCoord; }
  set { _numPositionsCoord = value; }
}
public ShortInteger NumPositionsNormal
{
  get { return _numPositionsNormal; }
  set { _numPositionsNormal = value; }
}
public Real MeleeAttackDelay
{
  get { return _meleeAttackDelay; }
  set { _meleeAttackDelay = value; }
}
public Real MeleeFudgeFactor
{
  get { return _meleeFudgeFactor; }
  set { _meleeFudgeFactor = value; }
}
public Real MeleeChargeTime
{
  get { return _meleeChargeTime; }
  set { _meleeChargeTime = value; }
}
public RealBounds MeleeLeapRange
{
  get { return _meleeLeapRange; }
  set { _meleeLeapRange = value; }
}
public Real MeleeLeapVelocity
{
  get { return _meleeLeapVelocity; }
  set { _meleeLeapVelocity = value; }
}
public Real MeleeLeapChance
{
  get { return _meleeLeapChance; }
  set { _meleeLeapChance = value; }
}
public Real MeleeLeapBallistic
{
  get { return _meleeLeapBallistic; }
  set { _meleeLeapBallistic = value; }
}
public Real BerserkDamageAmount
{
  get { return _berserkDamageAmount; }
  set { _berserkDamageAmount = value; }
}
public Real BerserkDamageThreshold
{
  get { return _berserkDamageThreshold; }
  set { _berserkDamageThreshold = value; }
}
public Real BerserkProximity
{
  get { return _berserkProximity; }
  set { _berserkProximity = value; }
}
public Real SuicideSensingDist
{
  get { return _suicideSensingDist; }
  set { _suicideSensingDist = value; }
}
public Real BerserkGrenadeChance
{
  get { return _berserkGrenadeChance; }
  set { _berserkGrenadeChance = value; }
}
public RealBounds GuardPositionTime
{
  get { return _guardPositionTime; }
  set { _guardPositionTime = value; }
}
public RealBounds CombatPositionTime
{
  get { return _combatPositionTime; }
  set { _combatPositionTime = value; }
}
public Real OldPositionAvoidDist
{
  get { return _oldPositionAvoidDist; }
  set { _oldPositionAvoidDist = value; }
}
public Real FriendAvoidDist
{
  get { return _friendAvoidDist; }
  set { _friendAvoidDist = value; }
}
public RealBounds NoncombatIdleSpeechTime
{
  get { return _noncombatIdleSpeechTime; }
  set { _noncombatIdleSpeechTime = value; }
}
public RealBounds CombatIdleSpeechTime
{
  get { return _combatIdleSpeechTime; }
  set { _combatIdleSpeechTime = value; }
}
public TagReference DONOTUSE3
{
  get { return _dONOTUSE3; }
  set { _dONOTUSE3 = value; }
}
public ActorBlock()
{
_flags = new Flags(4);
_moreFlags = new Flags(4);
__unnamed = new Pad(12);
__unnamed2 = new Pad(2);
__unnamed3 = new Pad(4);
__unnamed4 = new Pad(4);
__unnamed5 = new Pad(8);
__unnamed6 = new Pad(12);
__unnamed7 = new Pad(8);
__unnamed8 = new Pad(4);
__unnamed9 = new Pad(8);
__unnamed10 = new Pad(16);
__unnamed11 = new Pad(268);
__unnamed12 = new Pad(2);
__unnamed13 = new Pad(12);
__unnamed14 = new Pad(8);
__unnamed15 = new Pad(2);
__unnamed16 = new Pad(28);
__unnamed17 = new Pad(16);
__unnamed18 = new Pad(2);
__unnamed19 = new Pad(4);
__unnamed20 = new Pad(32);
__unnamed21 = new Pad(12);
__unnamed22 = new Pad(40);
__unnamed23 = new Pad(48);
__unnamed24 = new Pad(128);
__unnamed25 = new Pad(48);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _moreFlags.Read(reader);
  __unnamed.Read(reader);
  _type.Read(reader);
  __unnamed2.Read(reader);
  _maxVisionDistance.Read(reader);
  _centralVisionAngle.Read(reader);
  _maxVisionAngle.Read(reader);
  __unnamed3.Read(reader);
  _peripheralVisionAngle.Read(reader);
  _peripheralDistance.Read(reader);
  __unnamed4.Read(reader);
  _standingGunOffset.Read(reader);
  _crouchingGunOffset.Read(reader);
  _hearingDistance.Read(reader);
  _noticeProjectileChance.Read(reader);
  _noticeVehicleChance.Read(reader);
  __unnamed5.Read(reader);
  _combatPerceptionTime.Read(reader);
  _guardPerceptionTime.Read(reader);
  _no.Read(reader);
  __unnamed6.Read(reader);
  __unnamed7.Read(reader);
  _diveIntoCoverChance.Read(reader);
  _emergeFromCoverChance.Read(reader);
  _diveFromGrenadeChance.Read(reader);
  _pathfindingRadius.Read(reader);
  _glassIgnoranceChance.Read(reader);
  _stationaryMovementDist.Read(reader);
  _fre.Read(reader);
  _beginMovingAngle.Read(reader);
  __unnamed8.Read(reader);
  _maximumAimingDeviation.Read(reader);
  _maximumLookingDeviation.Read(reader);
  _noncombatLookDeltaL.Read(reader);
  _noncombatLookDeltaR.Read(reader);
  _combatLookDeltaL.Read(reader);
  _combatLookDeltaR.Read(reader);
  _idleAimingRange.Read(reader);
  _idleLookingRange.Read(reader);
  _eventLookTimeModifier.Read(reader);
  _noncombatIdleFacing.Read(reader);
  _noncombatIdleAiming.Read(reader);
  _noncombatIdleLooking.Read(reader);
  _guardIdleFacing.Read(reader);
  _guardIdleAiming.Read(reader);
  _guardIdleLooking.Read(reader);
  _combatIdleFacing.Read(reader);
  _combatIdleAiming.Read(reader);
  _combatIdleLooking.Read(reader);
  __unnamed9.Read(reader);
  __unnamed10.Read(reader);
  _dONOTUSE.Read(reader);
  __unnamed11.Read(reader);
  _dONOTUSE2.Read(reader);
  _unreachableDangerTrigger.Read(reader);
  _vehicleDangerTrigger.Read(reader);
  _playerDangerTrigger.Read(reader);
  __unnamed12.Read(reader);
  _dangerTriggerTime.Read(reader);
  _friendsKilledTrigger.Read(reader);
  _friendsRetreatingTrigger.Read(reader);
  __unnamed13.Read(reader);
  _retreatTime.Read(reader);
  __unnamed14.Read(reader);
  _coweringTime.Read(reader);
  _friendKilledPanicChance.Read(reader);
  _leaderType.Read(reader);
  __unnamed15.Read(reader);
  _leaderKilledPanicChance.Read(reader);
  _panicDamageThreshold.Read(reader);
  _surpriseDistance.Read(reader);
  __unnamed16.Read(reader);
  _hideBehindCoverTime.Read(reader);
  _hideTarge.Read(reader);
  _hideShieldFraction.Read(reader);
  _attackShieldFraction.Read(reader);
  _pursueShieldFraction.Read(reader);
  __unnamed17.Read(reader);
  _defensiveCrouchType.Read(reader);
  __unnamed18.Read(reader);
  _attackingCrouchThreshold.Read(reader);
  _defendingCrouchThreshold.Read(reader);
  _minStandTime.Read(reader);
  _minCrouchTime.Read(reader);
  _defendingHideTimeModifier.Read(reader);
  _attackingEvasionThreshold.Read(reader);
  _defendingEvasionThreshold.Read(reader);
  _evasionSee.Read(reader);
  _evasionDelayTime.Read(reader);
  _maxSee.Read(reader);
  _coverDamageThreshold.Read(reader);
  _stalkingDiscoveryTime.Read(reader);
  _stalkingMaxDistance.Read(reader);
  _stationaryFacingAngle.Read(reader);
  _chang.Read(reader);
  __unnamed19.Read(reader);
  _uncoverDelayTime.Read(reader);
  _targetSearchTime.Read(reader);
  _pursui.Read(reader);
  _numPositionsCoord.Read(reader);
  _numPositionsNormal.Read(reader);
  __unnamed20.Read(reader);
  _meleeAttackDelay.Read(reader);
  _meleeFudgeFactor.Read(reader);
  _meleeChargeTime.Read(reader);
  _meleeLeapRange.Read(reader);
  _meleeLeapVelocity.Read(reader);
  _meleeLeapChance.Read(reader);
  _meleeLeapBallistic.Read(reader);
  _berserkDamageAmount.Read(reader);
  _berserkDamageThreshold.Read(reader);
  _berserkProximity.Read(reader);
  _suicideSensingDist.Read(reader);
  _berserkGrenadeChance.Read(reader);
  __unnamed21.Read(reader);
  _guardPositionTime.Read(reader);
  _combatPositionTime.Read(reader);
  _oldPositionAvoidDist.Read(reader);
  _friendAvoidDist.Read(reader);
  __unnamed22.Read(reader);
  _noncombatIdleSpeechTime.Read(reader);
  _combatIdleSpeechTime.Read(reader);
  __unnamed23.Read(reader);
  __unnamed24.Read(reader);
  _dONOTUSE3.Read(reader);
  __unnamed25.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_dONOTUSE.ReadString(reader);
_dONOTUSE2.ReadString(reader);
_dONOTUSE3.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _moreFlags.Write(writer);
    __unnamed.Write(writer);
    _type.Write(writer);
    __unnamed2.Write(writer);
    _maxVisionDistance.Write(writer);
    _centralVisionAngle.Write(writer);
    _maxVisionAngle.Write(writer);
    __unnamed3.Write(writer);
    _peripheralVisionAngle.Write(writer);
    _peripheralDistance.Write(writer);
    __unnamed4.Write(writer);
    _standingGunOffset.Write(writer);
    _crouchingGunOffset.Write(writer);
    _hearingDistance.Write(writer);
    _noticeProjectileChance.Write(writer);
    _noticeVehicleChance.Write(writer);
    __unnamed5.Write(writer);
    _combatPerceptionTime.Write(writer);
    _guardPerceptionTime.Write(writer);
    _no.Write(writer);
    __unnamed6.Write(writer);
    __unnamed7.Write(writer);
    _diveIntoCoverChance.Write(writer);
    _emergeFromCoverChance.Write(writer);
    _diveFromGrenadeChance.Write(writer);
    _pathfindingRadius.Write(writer);
    _glassIgnoranceChance.Write(writer);
    _stationaryMovementDist.Write(writer);
    _fre.Write(writer);
    _beginMovingAngle.Write(writer);
    __unnamed8.Write(writer);
    _maximumAimingDeviation.Write(writer);
    _maximumLookingDeviation.Write(writer);
    _noncombatLookDeltaL.Write(writer);
    _noncombatLookDeltaR.Write(writer);
    _combatLookDeltaL.Write(writer);
    _combatLookDeltaR.Write(writer);
    _idleAimingRange.Write(writer);
    _idleLookingRange.Write(writer);
    _eventLookTimeModifier.Write(writer);
    _noncombatIdleFacing.Write(writer);
    _noncombatIdleAiming.Write(writer);
    _noncombatIdleLooking.Write(writer);
    _guardIdleFacing.Write(writer);
    _guardIdleAiming.Write(writer);
    _guardIdleLooking.Write(writer);
    _combatIdleFacing.Write(writer);
    _combatIdleAiming.Write(writer);
    _combatIdleLooking.Write(writer);
    __unnamed9.Write(writer);
    __unnamed10.Write(writer);
    _dONOTUSE.Write(writer);
    __unnamed11.Write(writer);
    _dONOTUSE2.Write(writer);
    _unreachableDangerTrigger.Write(writer);
    _vehicleDangerTrigger.Write(writer);
    _playerDangerTrigger.Write(writer);
    __unnamed12.Write(writer);
    _dangerTriggerTime.Write(writer);
    _friendsKilledTrigger.Write(writer);
    _friendsRetreatingTrigger.Write(writer);
    __unnamed13.Write(writer);
    _retreatTime.Write(writer);
    __unnamed14.Write(writer);
    _coweringTime.Write(writer);
    _friendKilledPanicChance.Write(writer);
    _leaderType.Write(writer);
    __unnamed15.Write(writer);
    _leaderKilledPanicChance.Write(writer);
    _panicDamageThreshold.Write(writer);
    _surpriseDistance.Write(writer);
    __unnamed16.Write(writer);
    _hideBehindCoverTime.Write(writer);
    _hideTarge.Write(writer);
    _hideShieldFraction.Write(writer);
    _attackShieldFraction.Write(writer);
    _pursueShieldFraction.Write(writer);
    __unnamed17.Write(writer);
    _defensiveCrouchType.Write(writer);
    __unnamed18.Write(writer);
    _attackingCrouchThreshold.Write(writer);
    _defendingCrouchThreshold.Write(writer);
    _minStandTime.Write(writer);
    _minCrouchTime.Write(writer);
    _defendingHideTimeModifier.Write(writer);
    _attackingEvasionThreshold.Write(writer);
    _defendingEvasionThreshold.Write(writer);
    _evasionSee.Write(writer);
    _evasionDelayTime.Write(writer);
    _maxSee.Write(writer);
    _coverDamageThreshold.Write(writer);
    _stalkingDiscoveryTime.Write(writer);
    _stalkingMaxDistance.Write(writer);
    _stationaryFacingAngle.Write(writer);
    _chang.Write(writer);
    __unnamed19.Write(writer);
    _uncoverDelayTime.Write(writer);
    _targetSearchTime.Write(writer);
    _pursui.Write(writer);
    _numPositionsCoord.Write(writer);
    _numPositionsNormal.Write(writer);
    __unnamed20.Write(writer);
    _meleeAttackDelay.Write(writer);
    _meleeFudgeFactor.Write(writer);
    _meleeChargeTime.Write(writer);
    _meleeLeapRange.Write(writer);
    _meleeLeapVelocity.Write(writer);
    _meleeLeapChance.Write(writer);
    _meleeLeapBallistic.Write(writer);
    _berserkDamageAmount.Write(writer);
    _berserkDamageThreshold.Write(writer);
    _berserkProximity.Write(writer);
    _suicideSensingDist.Write(writer);
    _berserkGrenadeChance.Write(writer);
    __unnamed21.Write(writer);
    _guardPositionTime.Write(writer);
    _combatPositionTime.Write(writer);
    _oldPositionAvoidDist.Write(writer);
    _friendAvoidDist.Write(writer);
    __unnamed22.Write(writer);
    _noncombatIdleSpeechTime.Write(writer);
    _combatIdleSpeechTime.Write(writer);
    __unnamed23.Write(writer);
    __unnamed24.Write(writer);
    _dONOTUSE3.Write(writer);
    __unnamed25.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_dONOTUSE.WriteString(writer);
_dONOTUSE2.WriteString(writer);
_dONOTUSE3.WriteString(writer);
}
}
  }
}
