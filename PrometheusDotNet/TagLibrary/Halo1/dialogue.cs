using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Dialogue : IBlock
  {
    public DialogueBlock DialogueValues = new DialogueBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'Dialogue'------------------------------------------------------");
      DialogueValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      DialogueValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      DialogueValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      DialogueValues.WriteChildData(writer);
    }
public class DialogueBlock : IBlock
{
private Skip  __unnamed;	
private Pad  __unnamed2;	
private Pad  __unnamed3;	
private TagReference _idleNoncombat = new TagReference();
private TagReference _idleCombat = new TagReference();
private TagReference _idleFlee = new TagReference();
private Pad  __unnamed4;	
private Pad  __unnamed5;	
private Pad  __unnamed6;	
private TagReference _painBodyMinor = new TagReference();
private TagReference _painBodyMajor = new TagReference();
private TagReference _painShield = new TagReference();
private TagReference _painFalling = new TagReference();
private TagReference _screamFear = new TagReference();
private TagReference _screamPain = new TagReference();
private TagReference _maimedLimb = new TagReference();
private TagReference _maimedHead = new TagReference();
private TagReference _deathQuiet = new TagReference();
private TagReference _deathViolent = new TagReference();
private TagReference _deathFalling = new TagReference();
private TagReference _deathAgonizing = new TagReference();
private TagReference _deathInstant = new TagReference();
private TagReference _deathFlying = new TagReference();
private Pad  __unnamed7;	
private TagReference _damagedFriend = new TagReference();
private TagReference _damagedFriendPlayer = new TagReference();
private TagReference _damagedEnemy = new TagReference();
private TagReference _damagedEnemyCm = new TagReference();
private Pad  __unnamed8;	
private Pad  __unnamed9;	
private Pad  __unnamed10;	
private Pad  __unnamed11;	
private TagReference _hurtFriend = new TagReference();
private TagReference _hurtFriendRe = new TagReference();
private TagReference _hurtFriendPlayer = new TagReference();
private TagReference _hurtEnemy = new TagReference();
private TagReference _hurtEnemyRe = new TagReference();
private TagReference _hurtEnemyCm = new TagReference();
private TagReference _hurtEnemyBullet = new TagReference();
private TagReference _hurtEnemyNeedler = new TagReference();
private TagReference _hurtEnemyPlasma = new TagReference();
private TagReference _hurtEnemySniper = new TagReference();
private TagReference _hurtEnemyGrenade = new TagReference();
private TagReference _hurtEnemyExplosion = new TagReference();
private TagReference _hurtEnemyMelee = new TagReference();
private TagReference _hurtEnemyFlame = new TagReference();
private TagReference _hurtEnemyShotgun = new TagReference();
private TagReference _hurtEnemyVehicle = new TagReference();
private TagReference _hurtEnemyMountedweapon = new TagReference();
private Pad  __unnamed12;	
private Pad  __unnamed13;	
private Pad  __unnamed14;	
private TagReference _killedFriend = new TagReference();
private TagReference _killedFriendCm = new TagReference();
private TagReference _killedFriendPlayer = new TagReference();
private TagReference _killedFriendPlayerCm = new TagReference();
private TagReference _killedEnemy = new TagReference();
private TagReference _killedEnemyCm = new TagReference();
private TagReference _killedEnemyPlayer = new TagReference();
private TagReference _killedEnemyPlayerCm = new TagReference();
private TagReference _killedEnemyCovenant = new TagReference();
private TagReference _killedEnemyCovenantCm = new TagReference();
private TagReference _killedEnemyFloodcombat = new TagReference();
private TagReference _killedEnemyFloodcombatCm = new TagReference();
private TagReference _killedEnemyFloodcarrier = new TagReference();
private TagReference _killedEnemyFloodcarrierCm = new TagReference();
private TagReference _killedEnemySentinel = new TagReference();
private TagReference _killedEnemySentinelCm = new TagReference();
private TagReference _killedEnemyBullet = new TagReference();
private TagReference _killedEnemyNeedler = new TagReference();
private TagReference _killedEnemyPlasma = new TagReference();
private TagReference _killedEnemySniper = new TagReference();
private TagReference _killedEnemyGrenade = new TagReference();
private TagReference _killedEnemyExplosion = new TagReference();
private TagReference _killedEnemyMelee = new TagReference();
private TagReference _killedEnemyFlame = new TagReference();
private TagReference _killedEnemyShotgun = new TagReference();
private TagReference _killedEnemyVehicle = new TagReference();
private TagReference _killedEnemyMountedweapon = new TagReference();
private TagReference _killingSpree = new TagReference();
private Pad  __unnamed15;	
private Pad  __unnamed16;	
private Pad  __unnamed17;	
private TagReference _playerKillCm = new TagReference();
private TagReference _playerKillBulletCm = new TagReference();
private TagReference _playerKillNeedlerCm = new TagReference();
private TagReference _playerKillPlasmaCm = new TagReference();
private TagReference _playerKillSniperCm = new TagReference();
private TagReference _anyoneKillGrenadeCm = new TagReference();
private TagReference _playerKillExplosionCm = new TagReference();
private TagReference _playerKillMeleeCm = new TagReference();
private TagReference _playerKillFlameCm = new TagReference();
private TagReference _playerKillShotgunCm = new TagReference();
private TagReference _playerKillVehicleCm = new TagReference();
private TagReference _playerKillMountedweaponCm = new TagReference();
private TagReference _playerKilllingSpreeCm = new TagReference();
private Pad  __unnamed18;	
private Pad  __unnamed19;	
private Pad  __unnamed20;	
private TagReference _friendDied = new TagReference();
private TagReference _friendPlayerDied = new TagReference();
private TagReference _friendKilledByFriend = new TagReference();
private TagReference _friendKilledByFriendlyPlayer = new TagReference();
private TagReference _friendKilledByEnemy = new TagReference();
private TagReference _friendKilledByEnemyPlayer = new TagReference();
private TagReference _friendKilledByCovenant = new TagReference();
private TagReference _friendKilledByFlood = new TagReference();
private TagReference _friendKilledBySentinel = new TagReference();
private TagReference _friendBetrayed = new TagReference();
private Pad  __unnamed21;	
private Pad  __unnamed22;	
private TagReference _newCombatAlone = new TagReference();
private TagReference _newEnemyRecentCombat = new TagReference();
private TagReference _oldEnemySighted = new TagReference();
private TagReference _unexpectedEnemy = new TagReference();
private TagReference _deadFriendFound = new TagReference();
private TagReference _allianceBroken = new TagReference();
private TagReference _allianceReformed = new TagReference();
private TagReference _grenadeThrowing = new TagReference();
private TagReference _grenadeSighted = new TagReference();
private TagReference _grenadeStartle = new TagReference();
private TagReference _grenadeDangerEnemy = new TagReference();
private TagReference _grenadeDangerSelf = new TagReference();
private TagReference _grenadeDangerFriend = new TagReference();
private Pad  __unnamed23;	
private Pad  __unnamed24;	
private TagReference _newCombatGroupRe = new TagReference();
private TagReference _newCombatNearbyRe = new TagReference();
private TagReference _alertFriend = new TagReference();
private TagReference _alertFriendRe = new TagReference();
private TagReference _alertLostContact = new TagReference();
private TagReference _alertLostContactRe = new TagReference();
private TagReference _blocked = new TagReference();
private TagReference _blockedRe = new TagReference();
private TagReference _searchStart = new TagReference();
private TagReference _searchQuery = new TagReference();
private TagReference _searchQueryRe = new TagReference();
private TagReference _searchReport = new TagReference();
private TagReference _searchAbandon = new TagReference();
private TagReference _searchGroupAbandon = new TagReference();
private TagReference _groupUncover = new TagReference();
private TagReference _groupUncoverRe = new TagReference();
private TagReference _advance = new TagReference();
private TagReference _advanceRe = new TagReference();
private TagReference _retreat = new TagReference();
private TagReference _retreatRe = new TagReference();
private TagReference _cover = new TagReference();
private Pad  __unnamed25;	
private Pad  __unnamed26;	
private Pad  __unnamed27;	
private Pad  __unnamed28;	
private TagReference _sightedFriendPlayer = new TagReference();
private TagReference _shooting = new TagReference();
private TagReference _shootingVehicle = new TagReference();
private TagReference _shootingBerserk = new TagReference();
private TagReference _shootingGroup = new TagReference();
private TagReference _shootingTraitor = new TagReference();
private TagReference _taunt = new TagReference();
private TagReference _tauntRe = new TagReference();
private TagReference _flee = new TagReference();
private TagReference _fleeRe = new TagReference();
private TagReference _fleeLeaderDied = new TagReference();
private TagReference _attemptedFlee = new TagReference();
private TagReference _attemptedFleeRe = new TagReference();
private TagReference _lostContact = new TagReference();
private TagReference _hidingFinished = new TagReference();
private TagReference _vehicleEntry = new TagReference();
private TagReference _vehicleExit = new TagReference();
private TagReference _vehicleWoohoo = new TagReference();
private TagReference _vehicleScared = new TagReference();
private TagReference _vehicleCollision = new TagReference();
private TagReference _partiallySighted = new TagReference();
private TagReference _nothingThere = new TagReference();
private TagReference _pleading = new TagReference();
private Pad  __unnamed29;	
private Pad  __unnamed30;	
private Pad  __unnamed31;	
private Pad  __unnamed32;	
private Pad  __unnamed33;	
private Pad  __unnamed34;	
private TagReference _surprise = new TagReference();
private TagReference _berserk = new TagReference();
private TagReference _meleeAttack = new TagReference();
private TagReference _dive = new TagReference();
private TagReference _uncoverExclamation = new TagReference();
private TagReference _leapAttack = new TagReference();
private TagReference _resurrection = new TagReference();
private Pad  __unnamed35;	
private Pad  __unnamed36;	
private Pad  __unnamed37;	
private Pad  __unnamed38;	
private TagReference _celebration = new TagReference();
private TagReference _checkBodyEnemy = new TagReference();
private TagReference _checkBodyFriend = new TagReference();
private TagReference _shootingDeadEnemy = new TagReference();
private TagReference _shootingDeadEnemyPlayer = new TagReference();
private Pad  __unnamed39;	
private Pad  __unnamed40;	
private Pad  __unnamed41;	
private Pad  __unnamed42;	
private TagReference _alone = new TagReference();
private TagReference _unscathed = new TagReference();
private TagReference _seriouslyWounded = new TagReference();
private TagReference _seriouslyWoundedRe = new TagReference();
private TagReference _massacre = new TagReference();
private TagReference _massacreRe = new TagReference();
private TagReference _rout = new TagReference();
private TagReference _routRe = new TagReference();
private Pad  __unnamed43;	
private Pad  __unnamed44;	
private Pad  __unnamed45;	
private Pad  __unnamed46;	
private Pad  __unnamed47;	
public TagReference IdleNoncombat
{
  get { return _idleNoncombat; }
  set { _idleNoncombat = value; }
}
public TagReference IdleCombat
{
  get { return _idleCombat; }
  set { _idleCombat = value; }
}
public TagReference IdleFlee
{
  get { return _idleFlee; }
  set { _idleFlee = value; }
}
public TagReference PainBodyMinor
{
  get { return _painBodyMinor; }
  set { _painBodyMinor = value; }
}
public TagReference PainBodyMajor
{
  get { return _painBodyMajor; }
  set { _painBodyMajor = value; }
}
public TagReference PainShield
{
  get { return _painShield; }
  set { _painShield = value; }
}
public TagReference PainFalling
{
  get { return _painFalling; }
  set { _painFalling = value; }
}
public TagReference ScreamFear
{
  get { return _screamFear; }
  set { _screamFear = value; }
}
public TagReference ScreamPain
{
  get { return _screamPain; }
  set { _screamPain = value; }
}
public TagReference MaimedLimb
{
  get { return _maimedLimb; }
  set { _maimedLimb = value; }
}
public TagReference MaimedHead
{
  get { return _maimedHead; }
  set { _maimedHead = value; }
}
public TagReference DeathQuiet
{
  get { return _deathQuiet; }
  set { _deathQuiet = value; }
}
public TagReference DeathViolent
{
  get { return _deathViolent; }
  set { _deathViolent = value; }
}
public TagReference DeathFalling
{
  get { return _deathFalling; }
  set { _deathFalling = value; }
}
public TagReference DeathAgonizing
{
  get { return _deathAgonizing; }
  set { _deathAgonizing = value; }
}
public TagReference DeathInstant
{
  get { return _deathInstant; }
  set { _deathInstant = value; }
}
public TagReference DeathFlying
{
  get { return _deathFlying; }
  set { _deathFlying = value; }
}
public TagReference DamagedFriend
{
  get { return _damagedFriend; }
  set { _damagedFriend = value; }
}
public TagReference DamagedFriendPlayer
{
  get { return _damagedFriendPlayer; }
  set { _damagedFriendPlayer = value; }
}
public TagReference DamagedEnemy
{
  get { return _damagedEnemy; }
  set { _damagedEnemy = value; }
}
public TagReference DamagedEnemyCm
{
  get { return _damagedEnemyCm; }
  set { _damagedEnemyCm = value; }
}
public TagReference HurtFriend
{
  get { return _hurtFriend; }
  set { _hurtFriend = value; }
}
public TagReference HurtFriendRe
{
  get { return _hurtFriendRe; }
  set { _hurtFriendRe = value; }
}
public TagReference HurtFriendPlayer
{
  get { return _hurtFriendPlayer; }
  set { _hurtFriendPlayer = value; }
}
public TagReference HurtEnemy
{
  get { return _hurtEnemy; }
  set { _hurtEnemy = value; }
}
public TagReference HurtEnemyRe
{
  get { return _hurtEnemyRe; }
  set { _hurtEnemyRe = value; }
}
public TagReference HurtEnemyCm
{
  get { return _hurtEnemyCm; }
  set { _hurtEnemyCm = value; }
}
public TagReference HurtEnemyBullet
{
  get { return _hurtEnemyBullet; }
  set { _hurtEnemyBullet = value; }
}
public TagReference HurtEnemyNeedler
{
  get { return _hurtEnemyNeedler; }
  set { _hurtEnemyNeedler = value; }
}
public TagReference HurtEnemyPlasma
{
  get { return _hurtEnemyPlasma; }
  set { _hurtEnemyPlasma = value; }
}
public TagReference HurtEnemySniper
{
  get { return _hurtEnemySniper; }
  set { _hurtEnemySniper = value; }
}
public TagReference HurtEnemyGrenade
{
  get { return _hurtEnemyGrenade; }
  set { _hurtEnemyGrenade = value; }
}
public TagReference HurtEnemyExplosion
{
  get { return _hurtEnemyExplosion; }
  set { _hurtEnemyExplosion = value; }
}
public TagReference HurtEnemyMelee
{
  get { return _hurtEnemyMelee; }
  set { _hurtEnemyMelee = value; }
}
public TagReference HurtEnemyFlame
{
  get { return _hurtEnemyFlame; }
  set { _hurtEnemyFlame = value; }
}
public TagReference HurtEnemyShotgun
{
  get { return _hurtEnemyShotgun; }
  set { _hurtEnemyShotgun = value; }
}
public TagReference HurtEnemyVehicle
{
  get { return _hurtEnemyVehicle; }
  set { _hurtEnemyVehicle = value; }
}
public TagReference HurtEnemyMountedweapon
{
  get { return _hurtEnemyMountedweapon; }
  set { _hurtEnemyMountedweapon = value; }
}
public TagReference KilledFriend
{
  get { return _killedFriend; }
  set { _killedFriend = value; }
}
public TagReference KilledFriendCm
{
  get { return _killedFriendCm; }
  set { _killedFriendCm = value; }
}
public TagReference KilledFriendPlayer
{
  get { return _killedFriendPlayer; }
  set { _killedFriendPlayer = value; }
}
public TagReference KilledFriendPlayerCm
{
  get { return _killedFriendPlayerCm; }
  set { _killedFriendPlayerCm = value; }
}
public TagReference KilledEnemy
{
  get { return _killedEnemy; }
  set { _killedEnemy = value; }
}
public TagReference KilledEnemyCm
{
  get { return _killedEnemyCm; }
  set { _killedEnemyCm = value; }
}
public TagReference KilledEnemyPlayer
{
  get { return _killedEnemyPlayer; }
  set { _killedEnemyPlayer = value; }
}
public TagReference KilledEnemyPlayerCm
{
  get { return _killedEnemyPlayerCm; }
  set { _killedEnemyPlayerCm = value; }
}
public TagReference KilledEnemyCovenant
{
  get { return _killedEnemyCovenant; }
  set { _killedEnemyCovenant = value; }
}
public TagReference KilledEnemyCovenantCm
{
  get { return _killedEnemyCovenantCm; }
  set { _killedEnemyCovenantCm = value; }
}
public TagReference KilledEnemyFloodcombat
{
  get { return _killedEnemyFloodcombat; }
  set { _killedEnemyFloodcombat = value; }
}
public TagReference KilledEnemyFloodcombatCm
{
  get { return _killedEnemyFloodcombatCm; }
  set { _killedEnemyFloodcombatCm = value; }
}
public TagReference KilledEnemyFloodcarrier
{
  get { return _killedEnemyFloodcarrier; }
  set { _killedEnemyFloodcarrier = value; }
}
public TagReference KilledEnemyFloodcarrierCm
{
  get { return _killedEnemyFloodcarrierCm; }
  set { _killedEnemyFloodcarrierCm = value; }
}
public TagReference KilledEnemySentinel
{
  get { return _killedEnemySentinel; }
  set { _killedEnemySentinel = value; }
}
public TagReference KilledEnemySentinelCm
{
  get { return _killedEnemySentinelCm; }
  set { _killedEnemySentinelCm = value; }
}
public TagReference KilledEnemyBullet
{
  get { return _killedEnemyBullet; }
  set { _killedEnemyBullet = value; }
}
public TagReference KilledEnemyNeedler
{
  get { return _killedEnemyNeedler; }
  set { _killedEnemyNeedler = value; }
}
public TagReference KilledEnemyPlasma
{
  get { return _killedEnemyPlasma; }
  set { _killedEnemyPlasma = value; }
}
public TagReference KilledEnemySniper
{
  get { return _killedEnemySniper; }
  set { _killedEnemySniper = value; }
}
public TagReference KilledEnemyGrenade
{
  get { return _killedEnemyGrenade; }
  set { _killedEnemyGrenade = value; }
}
public TagReference KilledEnemyExplosion
{
  get { return _killedEnemyExplosion; }
  set { _killedEnemyExplosion = value; }
}
public TagReference KilledEnemyMelee
{
  get { return _killedEnemyMelee; }
  set { _killedEnemyMelee = value; }
}
public TagReference KilledEnemyFlame
{
  get { return _killedEnemyFlame; }
  set { _killedEnemyFlame = value; }
}
public TagReference KilledEnemyShotgun
{
  get { return _killedEnemyShotgun; }
  set { _killedEnemyShotgun = value; }
}
public TagReference KilledEnemyVehicle
{
  get { return _killedEnemyVehicle; }
  set { _killedEnemyVehicle = value; }
}
public TagReference KilledEnemyMountedweapon
{
  get { return _killedEnemyMountedweapon; }
  set { _killedEnemyMountedweapon = value; }
}
public TagReference KillingSpree
{
  get { return _killingSpree; }
  set { _killingSpree = value; }
}
public TagReference PlayerKillCm
{
  get { return _playerKillCm; }
  set { _playerKillCm = value; }
}
public TagReference PlayerKillBulletCm
{
  get { return _playerKillBulletCm; }
  set { _playerKillBulletCm = value; }
}
public TagReference PlayerKillNeedlerCm
{
  get { return _playerKillNeedlerCm; }
  set { _playerKillNeedlerCm = value; }
}
public TagReference PlayerKillPlasmaCm
{
  get { return _playerKillPlasmaCm; }
  set { _playerKillPlasmaCm = value; }
}
public TagReference PlayerKillSniperCm
{
  get { return _playerKillSniperCm; }
  set { _playerKillSniperCm = value; }
}
public TagReference AnyoneKillGrenadeCm
{
  get { return _anyoneKillGrenadeCm; }
  set { _anyoneKillGrenadeCm = value; }
}
public TagReference PlayerKillExplosionCm
{
  get { return _playerKillExplosionCm; }
  set { _playerKillExplosionCm = value; }
}
public TagReference PlayerKillMeleeCm
{
  get { return _playerKillMeleeCm; }
  set { _playerKillMeleeCm = value; }
}
public TagReference PlayerKillFlameCm
{
  get { return _playerKillFlameCm; }
  set { _playerKillFlameCm = value; }
}
public TagReference PlayerKillShotgunCm
{
  get { return _playerKillShotgunCm; }
  set { _playerKillShotgunCm = value; }
}
public TagReference PlayerKillVehicleCm
{
  get { return _playerKillVehicleCm; }
  set { _playerKillVehicleCm = value; }
}
public TagReference PlayerKillMountedweaponCm
{
  get { return _playerKillMountedweaponCm; }
  set { _playerKillMountedweaponCm = value; }
}
public TagReference PlayerKilllingSpreeCm
{
  get { return _playerKilllingSpreeCm; }
  set { _playerKilllingSpreeCm = value; }
}
public TagReference FriendDied
{
  get { return _friendDied; }
  set { _friendDied = value; }
}
public TagReference FriendPlayerDied
{
  get { return _friendPlayerDied; }
  set { _friendPlayerDied = value; }
}
public TagReference FriendKilledByFriend
{
  get { return _friendKilledByFriend; }
  set { _friendKilledByFriend = value; }
}
public TagReference FriendKilledByFriendlyPlayer
{
  get { return _friendKilledByFriendlyPlayer; }
  set { _friendKilledByFriendlyPlayer = value; }
}
public TagReference FriendKilledByEnemy
{
  get { return _friendKilledByEnemy; }
  set { _friendKilledByEnemy = value; }
}
public TagReference FriendKilledByEnemyPlayer
{
  get { return _friendKilledByEnemyPlayer; }
  set { _friendKilledByEnemyPlayer = value; }
}
public TagReference FriendKilledByCovenant
{
  get { return _friendKilledByCovenant; }
  set { _friendKilledByCovenant = value; }
}
public TagReference FriendKilledByFlood
{
  get { return _friendKilledByFlood; }
  set { _friendKilledByFlood = value; }
}
public TagReference FriendKilledBySentinel
{
  get { return _friendKilledBySentinel; }
  set { _friendKilledBySentinel = value; }
}
public TagReference FriendBetrayed
{
  get { return _friendBetrayed; }
  set { _friendBetrayed = value; }
}
public TagReference NewCombatAlone
{
  get { return _newCombatAlone; }
  set { _newCombatAlone = value; }
}
public TagReference NewEnemyRecentCombat
{
  get { return _newEnemyRecentCombat; }
  set { _newEnemyRecentCombat = value; }
}
public TagReference OldEnemySighted
{
  get { return _oldEnemySighted; }
  set { _oldEnemySighted = value; }
}
public TagReference UnexpectedEnemy
{
  get { return _unexpectedEnemy; }
  set { _unexpectedEnemy = value; }
}
public TagReference DeadFriendFound
{
  get { return _deadFriendFound; }
  set { _deadFriendFound = value; }
}
public TagReference AllianceBroken
{
  get { return _allianceBroken; }
  set { _allianceBroken = value; }
}
public TagReference AllianceReformed
{
  get { return _allianceReformed; }
  set { _allianceReformed = value; }
}
public TagReference GrenadeThrowing
{
  get { return _grenadeThrowing; }
  set { _grenadeThrowing = value; }
}
public TagReference GrenadeSighted
{
  get { return _grenadeSighted; }
  set { _grenadeSighted = value; }
}
public TagReference GrenadeStartle
{
  get { return _grenadeStartle; }
  set { _grenadeStartle = value; }
}
public TagReference GrenadeDangerEnemy
{
  get { return _grenadeDangerEnemy; }
  set { _grenadeDangerEnemy = value; }
}
public TagReference GrenadeDangerSelf
{
  get { return _grenadeDangerSelf; }
  set { _grenadeDangerSelf = value; }
}
public TagReference GrenadeDangerFriend
{
  get { return _grenadeDangerFriend; }
  set { _grenadeDangerFriend = value; }
}
public TagReference NewCombatGroupRe
{
  get { return _newCombatGroupRe; }
  set { _newCombatGroupRe = value; }
}
public TagReference NewCombatNearbyRe
{
  get { return _newCombatNearbyRe; }
  set { _newCombatNearbyRe = value; }
}
public TagReference AlertFriend
{
  get { return _alertFriend; }
  set { _alertFriend = value; }
}
public TagReference AlertFriendRe
{
  get { return _alertFriendRe; }
  set { _alertFriendRe = value; }
}
public TagReference AlertLostContact
{
  get { return _alertLostContact; }
  set { _alertLostContact = value; }
}
public TagReference AlertLostContactRe
{
  get { return _alertLostContactRe; }
  set { _alertLostContactRe = value; }
}
public TagReference Blocked
{
  get { return _blocked; }
  set { _blocked = value; }
}
public TagReference BlockedRe
{
  get { return _blockedRe; }
  set { _blockedRe = value; }
}
public TagReference SearchStart
{
  get { return _searchStart; }
  set { _searchStart = value; }
}
public TagReference SearchQuery
{
  get { return _searchQuery; }
  set { _searchQuery = value; }
}
public TagReference SearchQueryRe
{
  get { return _searchQueryRe; }
  set { _searchQueryRe = value; }
}
public TagReference SearchReport
{
  get { return _searchReport; }
  set { _searchReport = value; }
}
public TagReference SearchAbandon
{
  get { return _searchAbandon; }
  set { _searchAbandon = value; }
}
public TagReference SearchGroupAbandon
{
  get { return _searchGroupAbandon; }
  set { _searchGroupAbandon = value; }
}
public TagReference GroupUncover
{
  get { return _groupUncover; }
  set { _groupUncover = value; }
}
public TagReference GroupUncoverRe
{
  get { return _groupUncoverRe; }
  set { _groupUncoverRe = value; }
}
public TagReference Advance
{
  get { return _advance; }
  set { _advance = value; }
}
public TagReference AdvanceRe
{
  get { return _advanceRe; }
  set { _advanceRe = value; }
}
public TagReference Retreat
{
  get { return _retreat; }
  set { _retreat = value; }
}
public TagReference RetreatRe
{
  get { return _retreatRe; }
  set { _retreatRe = value; }
}
public TagReference Cover
{
  get { return _cover; }
  set { _cover = value; }
}
public TagReference SightedFriendPlayer
{
  get { return _sightedFriendPlayer; }
  set { _sightedFriendPlayer = value; }
}
public TagReference Shooting
{
  get { return _shooting; }
  set { _shooting = value; }
}
public TagReference ShootingVehicle
{
  get { return _shootingVehicle; }
  set { _shootingVehicle = value; }
}
public TagReference ShootingBerserk
{
  get { return _shootingBerserk; }
  set { _shootingBerserk = value; }
}
public TagReference ShootingGroup
{
  get { return _shootingGroup; }
  set { _shootingGroup = value; }
}
public TagReference ShootingTraitor
{
  get { return _shootingTraitor; }
  set { _shootingTraitor = value; }
}
public TagReference Taunt
{
  get { return _taunt; }
  set { _taunt = value; }
}
public TagReference TauntRe
{
  get { return _tauntRe; }
  set { _tauntRe = value; }
}
public TagReference Flee
{
  get { return _flee; }
  set { _flee = value; }
}
public TagReference FleeRe
{
  get { return _fleeRe; }
  set { _fleeRe = value; }
}
public TagReference FleeLeaderDied
{
  get { return _fleeLeaderDied; }
  set { _fleeLeaderDied = value; }
}
public TagReference AttemptedFlee
{
  get { return _attemptedFlee; }
  set { _attemptedFlee = value; }
}
public TagReference AttemptedFleeRe
{
  get { return _attemptedFleeRe; }
  set { _attemptedFleeRe = value; }
}
public TagReference LostContact
{
  get { return _lostContact; }
  set { _lostContact = value; }
}
public TagReference HidingFinished
{
  get { return _hidingFinished; }
  set { _hidingFinished = value; }
}
public TagReference VehicleEntry
{
  get { return _vehicleEntry; }
  set { _vehicleEntry = value; }
}
public TagReference VehicleExit
{
  get { return _vehicleExit; }
  set { _vehicleExit = value; }
}
public TagReference VehicleWoohoo
{
  get { return _vehicleWoohoo; }
  set { _vehicleWoohoo = value; }
}
public TagReference VehicleScared
{
  get { return _vehicleScared; }
  set { _vehicleScared = value; }
}
public TagReference VehicleCollision
{
  get { return _vehicleCollision; }
  set { _vehicleCollision = value; }
}
public TagReference PartiallySighted
{
  get { return _partiallySighted; }
  set { _partiallySighted = value; }
}
public TagReference NothingThere
{
  get { return _nothingThere; }
  set { _nothingThere = value; }
}
public TagReference Pleading
{
  get { return _pleading; }
  set { _pleading = value; }
}
public TagReference Surprise
{
  get { return _surprise; }
  set { _surprise = value; }
}
public TagReference Berserk
{
  get { return _berserk; }
  set { _berserk = value; }
}
public TagReference MeleeAttack
{
  get { return _meleeAttack; }
  set { _meleeAttack = value; }
}
public TagReference Dive
{
  get { return _dive; }
  set { _dive = value; }
}
public TagReference UncoverExclamation
{
  get { return _uncoverExclamation; }
  set { _uncoverExclamation = value; }
}
public TagReference LeapAttack
{
  get { return _leapAttack; }
  set { _leapAttack = value; }
}
public TagReference Resurrection
{
  get { return _resurrection; }
  set { _resurrection = value; }
}
public TagReference Celebration
{
  get { return _celebration; }
  set { _celebration = value; }
}
public TagReference CheckBodyEnemy
{
  get { return _checkBodyEnemy; }
  set { _checkBodyEnemy = value; }
}
public TagReference CheckBodyFriend
{
  get { return _checkBodyFriend; }
  set { _checkBodyFriend = value; }
}
public TagReference ShootingDeadEnemy
{
  get { return _shootingDeadEnemy; }
  set { _shootingDeadEnemy = value; }
}
public TagReference ShootingDeadEnemyPlayer
{
  get { return _shootingDeadEnemyPlayer; }
  set { _shootingDeadEnemyPlayer = value; }
}
public TagReference Alone
{
  get { return _alone; }
  set { _alone = value; }
}
public TagReference Unscathed
{
  get { return _unscathed; }
  set { _unscathed = value; }
}
public TagReference SeriouslyWounded
{
  get { return _seriouslyWounded; }
  set { _seriouslyWounded = value; }
}
public TagReference SeriouslyWoundedRe
{
  get { return _seriouslyWoundedRe; }
  set { _seriouslyWoundedRe = value; }
}
public TagReference Massacre
{
  get { return _massacre; }
  set { _massacre = value; }
}
public TagReference MassacreRe
{
  get { return _massacreRe; }
  set { _massacreRe = value; }
}
public TagReference Rout
{
  get { return _rout; }
  set { _rout = value; }
}
public TagReference RoutRe
{
  get { return _routRe; }
  set { _routRe = value; }
}
public DialogueBlock()
{
__unnamed = new Skip(2);
__unnamed2 = new Pad(2);
__unnamed3 = new Pad(12);
__unnamed4 = new Pad(16);
__unnamed5 = new Pad(16);
__unnamed6 = new Pad(16);
__unnamed7 = new Pad(16);
__unnamed8 = new Pad(16);
__unnamed9 = new Pad(16);
__unnamed10 = new Pad(16);
__unnamed11 = new Pad(16);
__unnamed12 = new Pad(16);
__unnamed13 = new Pad(16);
__unnamed14 = new Pad(16);
__unnamed15 = new Pad(16);
__unnamed16 = new Pad(16);
__unnamed17 = new Pad(16);
__unnamed18 = new Pad(16);
__unnamed19 = new Pad(16);
__unnamed20 = new Pad(16);
__unnamed21 = new Pad(16);
__unnamed22 = new Pad(16);
__unnamed23 = new Pad(16);
__unnamed24 = new Pad(16);
__unnamed25 = new Pad(16);
__unnamed26 = new Pad(16);
__unnamed27 = new Pad(16);
__unnamed28 = new Pad(16);
__unnamed29 = new Pad(16);
__unnamed30 = new Pad(16);
__unnamed31 = new Pad(16);
__unnamed32 = new Pad(16);
__unnamed33 = new Pad(16);
__unnamed34 = new Pad(16);
__unnamed35 = new Pad(16);
__unnamed36 = new Pad(16);
__unnamed37 = new Pad(16);
__unnamed38 = new Pad(16);
__unnamed39 = new Pad(16);
__unnamed40 = new Pad(16);
__unnamed41 = new Pad(16);
__unnamed42 = new Pad(16);
__unnamed43 = new Pad(16);
__unnamed44 = new Pad(16);
__unnamed45 = new Pad(16);
__unnamed46 = new Pad(16);
__unnamed47 = new Pad(752);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  _idleNoncombat.Read(reader);
  _idleCombat.Read(reader);
  _idleFlee.Read(reader);
  __unnamed4.Read(reader);
  __unnamed5.Read(reader);
  __unnamed6.Read(reader);
  _painBodyMinor.Read(reader);
  _painBodyMajor.Read(reader);
  _painShield.Read(reader);
  _painFalling.Read(reader);
  _screamFear.Read(reader);
  _screamPain.Read(reader);
  _maimedLimb.Read(reader);
  _maimedHead.Read(reader);
  _deathQuiet.Read(reader);
  _deathViolent.Read(reader);
  _deathFalling.Read(reader);
  _deathAgonizing.Read(reader);
  _deathInstant.Read(reader);
  _deathFlying.Read(reader);
  __unnamed7.Read(reader);
  _damagedFriend.Read(reader);
  _damagedFriendPlayer.Read(reader);
  _damagedEnemy.Read(reader);
  _damagedEnemyCm.Read(reader);
  __unnamed8.Read(reader);
  __unnamed9.Read(reader);
  __unnamed10.Read(reader);
  __unnamed11.Read(reader);
  _hurtFriend.Read(reader);
  _hurtFriendRe.Read(reader);
  _hurtFriendPlayer.Read(reader);
  _hurtEnemy.Read(reader);
  _hurtEnemyRe.Read(reader);
  _hurtEnemyCm.Read(reader);
  _hurtEnemyBullet.Read(reader);
  _hurtEnemyNeedler.Read(reader);
  _hurtEnemyPlasma.Read(reader);
  _hurtEnemySniper.Read(reader);
  _hurtEnemyGrenade.Read(reader);
  _hurtEnemyExplosion.Read(reader);
  _hurtEnemyMelee.Read(reader);
  _hurtEnemyFlame.Read(reader);
  _hurtEnemyShotgun.Read(reader);
  _hurtEnemyVehicle.Read(reader);
  _hurtEnemyMountedweapon.Read(reader);
  __unnamed12.Read(reader);
  __unnamed13.Read(reader);
  __unnamed14.Read(reader);
  _killedFriend.Read(reader);
  _killedFriendCm.Read(reader);
  _killedFriendPlayer.Read(reader);
  _killedFriendPlayerCm.Read(reader);
  _killedEnemy.Read(reader);
  _killedEnemyCm.Read(reader);
  _killedEnemyPlayer.Read(reader);
  _killedEnemyPlayerCm.Read(reader);
  _killedEnemyCovenant.Read(reader);
  _killedEnemyCovenantCm.Read(reader);
  _killedEnemyFloodcombat.Read(reader);
  _killedEnemyFloodcombatCm.Read(reader);
  _killedEnemyFloodcarrier.Read(reader);
  _killedEnemyFloodcarrierCm.Read(reader);
  _killedEnemySentinel.Read(reader);
  _killedEnemySentinelCm.Read(reader);
  _killedEnemyBullet.Read(reader);
  _killedEnemyNeedler.Read(reader);
  _killedEnemyPlasma.Read(reader);
  _killedEnemySniper.Read(reader);
  _killedEnemyGrenade.Read(reader);
  _killedEnemyExplosion.Read(reader);
  _killedEnemyMelee.Read(reader);
  _killedEnemyFlame.Read(reader);
  _killedEnemyShotgun.Read(reader);
  _killedEnemyVehicle.Read(reader);
  _killedEnemyMountedweapon.Read(reader);
  _killingSpree.Read(reader);
  __unnamed15.Read(reader);
  __unnamed16.Read(reader);
  __unnamed17.Read(reader);
  _playerKillCm.Read(reader);
  _playerKillBulletCm.Read(reader);
  _playerKillNeedlerCm.Read(reader);
  _playerKillPlasmaCm.Read(reader);
  _playerKillSniperCm.Read(reader);
  _anyoneKillGrenadeCm.Read(reader);
  _playerKillExplosionCm.Read(reader);
  _playerKillMeleeCm.Read(reader);
  _playerKillFlameCm.Read(reader);
  _playerKillShotgunCm.Read(reader);
  _playerKillVehicleCm.Read(reader);
  _playerKillMountedweaponCm.Read(reader);
  _playerKilllingSpreeCm.Read(reader);
  __unnamed18.Read(reader);
  __unnamed19.Read(reader);
  __unnamed20.Read(reader);
  _friendDied.Read(reader);
  _friendPlayerDied.Read(reader);
  _friendKilledByFriend.Read(reader);
  _friendKilledByFriendlyPlayer.Read(reader);
  _friendKilledByEnemy.Read(reader);
  _friendKilledByEnemyPlayer.Read(reader);
  _friendKilledByCovenant.Read(reader);
  _friendKilledByFlood.Read(reader);
  _friendKilledBySentinel.Read(reader);
  _friendBetrayed.Read(reader);
  __unnamed21.Read(reader);
  __unnamed22.Read(reader);
  _newCombatAlone.Read(reader);
  _newEnemyRecentCombat.Read(reader);
  _oldEnemySighted.Read(reader);
  _unexpectedEnemy.Read(reader);
  _deadFriendFound.Read(reader);
  _allianceBroken.Read(reader);
  _allianceReformed.Read(reader);
  _grenadeThrowing.Read(reader);
  _grenadeSighted.Read(reader);
  _grenadeStartle.Read(reader);
  _grenadeDangerEnemy.Read(reader);
  _grenadeDangerSelf.Read(reader);
  _grenadeDangerFriend.Read(reader);
  __unnamed23.Read(reader);
  __unnamed24.Read(reader);
  _newCombatGroupRe.Read(reader);
  _newCombatNearbyRe.Read(reader);
  _alertFriend.Read(reader);
  _alertFriendRe.Read(reader);
  _alertLostContact.Read(reader);
  _alertLostContactRe.Read(reader);
  _blocked.Read(reader);
  _blockedRe.Read(reader);
  _searchStart.Read(reader);
  _searchQuery.Read(reader);
  _searchQueryRe.Read(reader);
  _searchReport.Read(reader);
  _searchAbandon.Read(reader);
  _searchGroupAbandon.Read(reader);
  _groupUncover.Read(reader);
  _groupUncoverRe.Read(reader);
  _advance.Read(reader);
  _advanceRe.Read(reader);
  _retreat.Read(reader);
  _retreatRe.Read(reader);
  _cover.Read(reader);
  __unnamed25.Read(reader);
  __unnamed26.Read(reader);
  __unnamed27.Read(reader);
  __unnamed28.Read(reader);
  _sightedFriendPlayer.Read(reader);
  _shooting.Read(reader);
  _shootingVehicle.Read(reader);
  _shootingBerserk.Read(reader);
  _shootingGroup.Read(reader);
  _shootingTraitor.Read(reader);
  _taunt.Read(reader);
  _tauntRe.Read(reader);
  _flee.Read(reader);
  _fleeRe.Read(reader);
  _fleeLeaderDied.Read(reader);
  _attemptedFlee.Read(reader);
  _attemptedFleeRe.Read(reader);
  _lostContact.Read(reader);
  _hidingFinished.Read(reader);
  _vehicleEntry.Read(reader);
  _vehicleExit.Read(reader);
  _vehicleWoohoo.Read(reader);
  _vehicleScared.Read(reader);
  _vehicleCollision.Read(reader);
  _partiallySighted.Read(reader);
  _nothingThere.Read(reader);
  _pleading.Read(reader);
  __unnamed29.Read(reader);
  __unnamed30.Read(reader);
  __unnamed31.Read(reader);
  __unnamed32.Read(reader);
  __unnamed33.Read(reader);
  __unnamed34.Read(reader);
  _surprise.Read(reader);
  _berserk.Read(reader);
  _meleeAttack.Read(reader);
  _dive.Read(reader);
  _uncoverExclamation.Read(reader);
  _leapAttack.Read(reader);
  _resurrection.Read(reader);
  __unnamed35.Read(reader);
  __unnamed36.Read(reader);
  __unnamed37.Read(reader);
  __unnamed38.Read(reader);
  _celebration.Read(reader);
  _checkBodyEnemy.Read(reader);
  _checkBodyFriend.Read(reader);
  _shootingDeadEnemy.Read(reader);
  _shootingDeadEnemyPlayer.Read(reader);
  __unnamed39.Read(reader);
  __unnamed40.Read(reader);
  __unnamed41.Read(reader);
  __unnamed42.Read(reader);
  _alone.Read(reader);
  _unscathed.Read(reader);
  _seriouslyWounded.Read(reader);
  _seriouslyWoundedRe.Read(reader);
  _massacre.Read(reader);
  _massacreRe.Read(reader);
  _rout.Read(reader);
  _routRe.Read(reader);
  __unnamed43.Read(reader);
  __unnamed44.Read(reader);
  __unnamed45.Read(reader);
  __unnamed46.Read(reader);
  __unnamed47.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_idleNoncombat.ReadString(reader);
_idleCombat.ReadString(reader);
_idleFlee.ReadString(reader);
_painBodyMinor.ReadString(reader);
_painBodyMajor.ReadString(reader);
_painShield.ReadString(reader);
_painFalling.ReadString(reader);
_screamFear.ReadString(reader);
_screamPain.ReadString(reader);
_maimedLimb.ReadString(reader);
_maimedHead.ReadString(reader);
_deathQuiet.ReadString(reader);
_deathViolent.ReadString(reader);
_deathFalling.ReadString(reader);
_deathAgonizing.ReadString(reader);
_deathInstant.ReadString(reader);
_deathFlying.ReadString(reader);
_damagedFriend.ReadString(reader);
_damagedFriendPlayer.ReadString(reader);
_damagedEnemy.ReadString(reader);
_damagedEnemyCm.ReadString(reader);
_hurtFriend.ReadString(reader);
_hurtFriendRe.ReadString(reader);
_hurtFriendPlayer.ReadString(reader);
_hurtEnemy.ReadString(reader);
_hurtEnemyRe.ReadString(reader);
_hurtEnemyCm.ReadString(reader);
_hurtEnemyBullet.ReadString(reader);
_hurtEnemyNeedler.ReadString(reader);
_hurtEnemyPlasma.ReadString(reader);
_hurtEnemySniper.ReadString(reader);
_hurtEnemyGrenade.ReadString(reader);
_hurtEnemyExplosion.ReadString(reader);
_hurtEnemyMelee.ReadString(reader);
_hurtEnemyFlame.ReadString(reader);
_hurtEnemyShotgun.ReadString(reader);
_hurtEnemyVehicle.ReadString(reader);
_hurtEnemyMountedweapon.ReadString(reader);
_killedFriend.ReadString(reader);
_killedFriendCm.ReadString(reader);
_killedFriendPlayer.ReadString(reader);
_killedFriendPlayerCm.ReadString(reader);
_killedEnemy.ReadString(reader);
_killedEnemyCm.ReadString(reader);
_killedEnemyPlayer.ReadString(reader);
_killedEnemyPlayerCm.ReadString(reader);
_killedEnemyCovenant.ReadString(reader);
_killedEnemyCovenantCm.ReadString(reader);
_killedEnemyFloodcombat.ReadString(reader);
_killedEnemyFloodcombatCm.ReadString(reader);
_killedEnemyFloodcarrier.ReadString(reader);
_killedEnemyFloodcarrierCm.ReadString(reader);
_killedEnemySentinel.ReadString(reader);
_killedEnemySentinelCm.ReadString(reader);
_killedEnemyBullet.ReadString(reader);
_killedEnemyNeedler.ReadString(reader);
_killedEnemyPlasma.ReadString(reader);
_killedEnemySniper.ReadString(reader);
_killedEnemyGrenade.ReadString(reader);
_killedEnemyExplosion.ReadString(reader);
_killedEnemyMelee.ReadString(reader);
_killedEnemyFlame.ReadString(reader);
_killedEnemyShotgun.ReadString(reader);
_killedEnemyVehicle.ReadString(reader);
_killedEnemyMountedweapon.ReadString(reader);
_killingSpree.ReadString(reader);
_playerKillCm.ReadString(reader);
_playerKillBulletCm.ReadString(reader);
_playerKillNeedlerCm.ReadString(reader);
_playerKillPlasmaCm.ReadString(reader);
_playerKillSniperCm.ReadString(reader);
_anyoneKillGrenadeCm.ReadString(reader);
_playerKillExplosionCm.ReadString(reader);
_playerKillMeleeCm.ReadString(reader);
_playerKillFlameCm.ReadString(reader);
_playerKillShotgunCm.ReadString(reader);
_playerKillVehicleCm.ReadString(reader);
_playerKillMountedweaponCm.ReadString(reader);
_playerKilllingSpreeCm.ReadString(reader);
_friendDied.ReadString(reader);
_friendPlayerDied.ReadString(reader);
_friendKilledByFriend.ReadString(reader);
_friendKilledByFriendlyPlayer.ReadString(reader);
_friendKilledByEnemy.ReadString(reader);
_friendKilledByEnemyPlayer.ReadString(reader);
_friendKilledByCovenant.ReadString(reader);
_friendKilledByFlood.ReadString(reader);
_friendKilledBySentinel.ReadString(reader);
_friendBetrayed.ReadString(reader);
_newCombatAlone.ReadString(reader);
_newEnemyRecentCombat.ReadString(reader);
_oldEnemySighted.ReadString(reader);
_unexpectedEnemy.ReadString(reader);
_deadFriendFound.ReadString(reader);
_allianceBroken.ReadString(reader);
_allianceReformed.ReadString(reader);
_grenadeThrowing.ReadString(reader);
_grenadeSighted.ReadString(reader);
_grenadeStartle.ReadString(reader);
_grenadeDangerEnemy.ReadString(reader);
_grenadeDangerSelf.ReadString(reader);
_grenadeDangerFriend.ReadString(reader);
_newCombatGroupRe.ReadString(reader);
_newCombatNearbyRe.ReadString(reader);
_alertFriend.ReadString(reader);
_alertFriendRe.ReadString(reader);
_alertLostContact.ReadString(reader);
_alertLostContactRe.ReadString(reader);
_blocked.ReadString(reader);
_blockedRe.ReadString(reader);
_searchStart.ReadString(reader);
_searchQuery.ReadString(reader);
_searchQueryRe.ReadString(reader);
_searchReport.ReadString(reader);
_searchAbandon.ReadString(reader);
_searchGroupAbandon.ReadString(reader);
_groupUncover.ReadString(reader);
_groupUncoverRe.ReadString(reader);
_advance.ReadString(reader);
_advanceRe.ReadString(reader);
_retreat.ReadString(reader);
_retreatRe.ReadString(reader);
_cover.ReadString(reader);
_sightedFriendPlayer.ReadString(reader);
_shooting.ReadString(reader);
_shootingVehicle.ReadString(reader);
_shootingBerserk.ReadString(reader);
_shootingGroup.ReadString(reader);
_shootingTraitor.ReadString(reader);
_taunt.ReadString(reader);
_tauntRe.ReadString(reader);
_flee.ReadString(reader);
_fleeRe.ReadString(reader);
_fleeLeaderDied.ReadString(reader);
_attemptedFlee.ReadString(reader);
_attemptedFleeRe.ReadString(reader);
_lostContact.ReadString(reader);
_hidingFinished.ReadString(reader);
_vehicleEntry.ReadString(reader);
_vehicleExit.ReadString(reader);
_vehicleWoohoo.ReadString(reader);
_vehicleScared.ReadString(reader);
_vehicleCollision.ReadString(reader);
_partiallySighted.ReadString(reader);
_nothingThere.ReadString(reader);
_pleading.ReadString(reader);
_surprise.ReadString(reader);
_berserk.ReadString(reader);
_meleeAttack.ReadString(reader);
_dive.ReadString(reader);
_uncoverExclamation.ReadString(reader);
_leapAttack.ReadString(reader);
_resurrection.ReadString(reader);
_celebration.ReadString(reader);
_checkBodyEnemy.ReadString(reader);
_checkBodyFriend.ReadString(reader);
_shootingDeadEnemy.ReadString(reader);
_shootingDeadEnemyPlayer.ReadString(reader);
_alone.ReadString(reader);
_unscathed.ReadString(reader);
_seriouslyWounded.ReadString(reader);
_seriouslyWoundedRe.ReadString(reader);
_massacre.ReadString(reader);
_massacreRe.ReadString(reader);
_rout.ReadString(reader);
_routRe.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    _idleNoncombat.Write(writer);
    _idleCombat.Write(writer);
    _idleFlee.Write(writer);
    __unnamed4.Write(writer);
    __unnamed5.Write(writer);
    __unnamed6.Write(writer);
    _painBodyMinor.Write(writer);
    _painBodyMajor.Write(writer);
    _painShield.Write(writer);
    _painFalling.Write(writer);
    _screamFear.Write(writer);
    _screamPain.Write(writer);
    _maimedLimb.Write(writer);
    _maimedHead.Write(writer);
    _deathQuiet.Write(writer);
    _deathViolent.Write(writer);
    _deathFalling.Write(writer);
    _deathAgonizing.Write(writer);
    _deathInstant.Write(writer);
    _deathFlying.Write(writer);
    __unnamed7.Write(writer);
    _damagedFriend.Write(writer);
    _damagedFriendPlayer.Write(writer);
    _damagedEnemy.Write(writer);
    _damagedEnemyCm.Write(writer);
    __unnamed8.Write(writer);
    __unnamed9.Write(writer);
    __unnamed10.Write(writer);
    __unnamed11.Write(writer);
    _hurtFriend.Write(writer);
    _hurtFriendRe.Write(writer);
    _hurtFriendPlayer.Write(writer);
    _hurtEnemy.Write(writer);
    _hurtEnemyRe.Write(writer);
    _hurtEnemyCm.Write(writer);
    _hurtEnemyBullet.Write(writer);
    _hurtEnemyNeedler.Write(writer);
    _hurtEnemyPlasma.Write(writer);
    _hurtEnemySniper.Write(writer);
    _hurtEnemyGrenade.Write(writer);
    _hurtEnemyExplosion.Write(writer);
    _hurtEnemyMelee.Write(writer);
    _hurtEnemyFlame.Write(writer);
    _hurtEnemyShotgun.Write(writer);
    _hurtEnemyVehicle.Write(writer);
    _hurtEnemyMountedweapon.Write(writer);
    __unnamed12.Write(writer);
    __unnamed13.Write(writer);
    __unnamed14.Write(writer);
    _killedFriend.Write(writer);
    _killedFriendCm.Write(writer);
    _killedFriendPlayer.Write(writer);
    _killedFriendPlayerCm.Write(writer);
    _killedEnemy.Write(writer);
    _killedEnemyCm.Write(writer);
    _killedEnemyPlayer.Write(writer);
    _killedEnemyPlayerCm.Write(writer);
    _killedEnemyCovenant.Write(writer);
    _killedEnemyCovenantCm.Write(writer);
    _killedEnemyFloodcombat.Write(writer);
    _killedEnemyFloodcombatCm.Write(writer);
    _killedEnemyFloodcarrier.Write(writer);
    _killedEnemyFloodcarrierCm.Write(writer);
    _killedEnemySentinel.Write(writer);
    _killedEnemySentinelCm.Write(writer);
    _killedEnemyBullet.Write(writer);
    _killedEnemyNeedler.Write(writer);
    _killedEnemyPlasma.Write(writer);
    _killedEnemySniper.Write(writer);
    _killedEnemyGrenade.Write(writer);
    _killedEnemyExplosion.Write(writer);
    _killedEnemyMelee.Write(writer);
    _killedEnemyFlame.Write(writer);
    _killedEnemyShotgun.Write(writer);
    _killedEnemyVehicle.Write(writer);
    _killedEnemyMountedweapon.Write(writer);
    _killingSpree.Write(writer);
    __unnamed15.Write(writer);
    __unnamed16.Write(writer);
    __unnamed17.Write(writer);
    _playerKillCm.Write(writer);
    _playerKillBulletCm.Write(writer);
    _playerKillNeedlerCm.Write(writer);
    _playerKillPlasmaCm.Write(writer);
    _playerKillSniperCm.Write(writer);
    _anyoneKillGrenadeCm.Write(writer);
    _playerKillExplosionCm.Write(writer);
    _playerKillMeleeCm.Write(writer);
    _playerKillFlameCm.Write(writer);
    _playerKillShotgunCm.Write(writer);
    _playerKillVehicleCm.Write(writer);
    _playerKillMountedweaponCm.Write(writer);
    _playerKilllingSpreeCm.Write(writer);
    __unnamed18.Write(writer);
    __unnamed19.Write(writer);
    __unnamed20.Write(writer);
    _friendDied.Write(writer);
    _friendPlayerDied.Write(writer);
    _friendKilledByFriend.Write(writer);
    _friendKilledByFriendlyPlayer.Write(writer);
    _friendKilledByEnemy.Write(writer);
    _friendKilledByEnemyPlayer.Write(writer);
    _friendKilledByCovenant.Write(writer);
    _friendKilledByFlood.Write(writer);
    _friendKilledBySentinel.Write(writer);
    _friendBetrayed.Write(writer);
    __unnamed21.Write(writer);
    __unnamed22.Write(writer);
    _newCombatAlone.Write(writer);
    _newEnemyRecentCombat.Write(writer);
    _oldEnemySighted.Write(writer);
    _unexpectedEnemy.Write(writer);
    _deadFriendFound.Write(writer);
    _allianceBroken.Write(writer);
    _allianceReformed.Write(writer);
    _grenadeThrowing.Write(writer);
    _grenadeSighted.Write(writer);
    _grenadeStartle.Write(writer);
    _grenadeDangerEnemy.Write(writer);
    _grenadeDangerSelf.Write(writer);
    _grenadeDangerFriend.Write(writer);
    __unnamed23.Write(writer);
    __unnamed24.Write(writer);
    _newCombatGroupRe.Write(writer);
    _newCombatNearbyRe.Write(writer);
    _alertFriend.Write(writer);
    _alertFriendRe.Write(writer);
    _alertLostContact.Write(writer);
    _alertLostContactRe.Write(writer);
    _blocked.Write(writer);
    _blockedRe.Write(writer);
    _searchStart.Write(writer);
    _searchQuery.Write(writer);
    _searchQueryRe.Write(writer);
    _searchReport.Write(writer);
    _searchAbandon.Write(writer);
    _searchGroupAbandon.Write(writer);
    _groupUncover.Write(writer);
    _groupUncoverRe.Write(writer);
    _advance.Write(writer);
    _advanceRe.Write(writer);
    _retreat.Write(writer);
    _retreatRe.Write(writer);
    _cover.Write(writer);
    __unnamed25.Write(writer);
    __unnamed26.Write(writer);
    __unnamed27.Write(writer);
    __unnamed28.Write(writer);
    _sightedFriendPlayer.Write(writer);
    _shooting.Write(writer);
    _shootingVehicle.Write(writer);
    _shootingBerserk.Write(writer);
    _shootingGroup.Write(writer);
    _shootingTraitor.Write(writer);
    _taunt.Write(writer);
    _tauntRe.Write(writer);
    _flee.Write(writer);
    _fleeRe.Write(writer);
    _fleeLeaderDied.Write(writer);
    _attemptedFlee.Write(writer);
    _attemptedFleeRe.Write(writer);
    _lostContact.Write(writer);
    _hidingFinished.Write(writer);
    _vehicleEntry.Write(writer);
    _vehicleExit.Write(writer);
    _vehicleWoohoo.Write(writer);
    _vehicleScared.Write(writer);
    _vehicleCollision.Write(writer);
    _partiallySighted.Write(writer);
    _nothingThere.Write(writer);
    _pleading.Write(writer);
    __unnamed29.Write(writer);
    __unnamed30.Write(writer);
    __unnamed31.Write(writer);
    __unnamed32.Write(writer);
    __unnamed33.Write(writer);
    __unnamed34.Write(writer);
    _surprise.Write(writer);
    _berserk.Write(writer);
    _meleeAttack.Write(writer);
    _dive.Write(writer);
    _uncoverExclamation.Write(writer);
    _leapAttack.Write(writer);
    _resurrection.Write(writer);
    __unnamed35.Write(writer);
    __unnamed36.Write(writer);
    __unnamed37.Write(writer);
    __unnamed38.Write(writer);
    _celebration.Write(writer);
    _checkBodyEnemy.Write(writer);
    _checkBodyFriend.Write(writer);
    _shootingDeadEnemy.Write(writer);
    _shootingDeadEnemyPlayer.Write(writer);
    __unnamed39.Write(writer);
    __unnamed40.Write(writer);
    __unnamed41.Write(writer);
    __unnamed42.Write(writer);
    _alone.Write(writer);
    _unscathed.Write(writer);
    _seriouslyWounded.Write(writer);
    _seriouslyWoundedRe.Write(writer);
    _massacre.Write(writer);
    _massacreRe.Write(writer);
    _rout.Write(writer);
    _routRe.Write(writer);
    __unnamed43.Write(writer);
    __unnamed44.Write(writer);
    __unnamed45.Write(writer);
    __unnamed46.Write(writer);
    __unnamed47.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_idleNoncombat.WriteString(writer);
_idleCombat.WriteString(writer);
_idleFlee.WriteString(writer);
_painBodyMinor.WriteString(writer);
_painBodyMajor.WriteString(writer);
_painShield.WriteString(writer);
_painFalling.WriteString(writer);
_screamFear.WriteString(writer);
_screamPain.WriteString(writer);
_maimedLimb.WriteString(writer);
_maimedHead.WriteString(writer);
_deathQuiet.WriteString(writer);
_deathViolent.WriteString(writer);
_deathFalling.WriteString(writer);
_deathAgonizing.WriteString(writer);
_deathInstant.WriteString(writer);
_deathFlying.WriteString(writer);
_damagedFriend.WriteString(writer);
_damagedFriendPlayer.WriteString(writer);
_damagedEnemy.WriteString(writer);
_damagedEnemyCm.WriteString(writer);
_hurtFriend.WriteString(writer);
_hurtFriendRe.WriteString(writer);
_hurtFriendPlayer.WriteString(writer);
_hurtEnemy.WriteString(writer);
_hurtEnemyRe.WriteString(writer);
_hurtEnemyCm.WriteString(writer);
_hurtEnemyBullet.WriteString(writer);
_hurtEnemyNeedler.WriteString(writer);
_hurtEnemyPlasma.WriteString(writer);
_hurtEnemySniper.WriteString(writer);
_hurtEnemyGrenade.WriteString(writer);
_hurtEnemyExplosion.WriteString(writer);
_hurtEnemyMelee.WriteString(writer);
_hurtEnemyFlame.WriteString(writer);
_hurtEnemyShotgun.WriteString(writer);
_hurtEnemyVehicle.WriteString(writer);
_hurtEnemyMountedweapon.WriteString(writer);
_killedFriend.WriteString(writer);
_killedFriendCm.WriteString(writer);
_killedFriendPlayer.WriteString(writer);
_killedFriendPlayerCm.WriteString(writer);
_killedEnemy.WriteString(writer);
_killedEnemyCm.WriteString(writer);
_killedEnemyPlayer.WriteString(writer);
_killedEnemyPlayerCm.WriteString(writer);
_killedEnemyCovenant.WriteString(writer);
_killedEnemyCovenantCm.WriteString(writer);
_killedEnemyFloodcombat.WriteString(writer);
_killedEnemyFloodcombatCm.WriteString(writer);
_killedEnemyFloodcarrier.WriteString(writer);
_killedEnemyFloodcarrierCm.WriteString(writer);
_killedEnemySentinel.WriteString(writer);
_killedEnemySentinelCm.WriteString(writer);
_killedEnemyBullet.WriteString(writer);
_killedEnemyNeedler.WriteString(writer);
_killedEnemyPlasma.WriteString(writer);
_killedEnemySniper.WriteString(writer);
_killedEnemyGrenade.WriteString(writer);
_killedEnemyExplosion.WriteString(writer);
_killedEnemyMelee.WriteString(writer);
_killedEnemyFlame.WriteString(writer);
_killedEnemyShotgun.WriteString(writer);
_killedEnemyVehicle.WriteString(writer);
_killedEnemyMountedweapon.WriteString(writer);
_killingSpree.WriteString(writer);
_playerKillCm.WriteString(writer);
_playerKillBulletCm.WriteString(writer);
_playerKillNeedlerCm.WriteString(writer);
_playerKillPlasmaCm.WriteString(writer);
_playerKillSniperCm.WriteString(writer);
_anyoneKillGrenadeCm.WriteString(writer);
_playerKillExplosionCm.WriteString(writer);
_playerKillMeleeCm.WriteString(writer);
_playerKillFlameCm.WriteString(writer);
_playerKillShotgunCm.WriteString(writer);
_playerKillVehicleCm.WriteString(writer);
_playerKillMountedweaponCm.WriteString(writer);
_playerKilllingSpreeCm.WriteString(writer);
_friendDied.WriteString(writer);
_friendPlayerDied.WriteString(writer);
_friendKilledByFriend.WriteString(writer);
_friendKilledByFriendlyPlayer.WriteString(writer);
_friendKilledByEnemy.WriteString(writer);
_friendKilledByEnemyPlayer.WriteString(writer);
_friendKilledByCovenant.WriteString(writer);
_friendKilledByFlood.WriteString(writer);
_friendKilledBySentinel.WriteString(writer);
_friendBetrayed.WriteString(writer);
_newCombatAlone.WriteString(writer);
_newEnemyRecentCombat.WriteString(writer);
_oldEnemySighted.WriteString(writer);
_unexpectedEnemy.WriteString(writer);
_deadFriendFound.WriteString(writer);
_allianceBroken.WriteString(writer);
_allianceReformed.WriteString(writer);
_grenadeThrowing.WriteString(writer);
_grenadeSighted.WriteString(writer);
_grenadeStartle.WriteString(writer);
_grenadeDangerEnemy.WriteString(writer);
_grenadeDangerSelf.WriteString(writer);
_grenadeDangerFriend.WriteString(writer);
_newCombatGroupRe.WriteString(writer);
_newCombatNearbyRe.WriteString(writer);
_alertFriend.WriteString(writer);
_alertFriendRe.WriteString(writer);
_alertLostContact.WriteString(writer);
_alertLostContactRe.WriteString(writer);
_blocked.WriteString(writer);
_blockedRe.WriteString(writer);
_searchStart.WriteString(writer);
_searchQuery.WriteString(writer);
_searchQueryRe.WriteString(writer);
_searchReport.WriteString(writer);
_searchAbandon.WriteString(writer);
_searchGroupAbandon.WriteString(writer);
_groupUncover.WriteString(writer);
_groupUncoverRe.WriteString(writer);
_advance.WriteString(writer);
_advanceRe.WriteString(writer);
_retreat.WriteString(writer);
_retreatRe.WriteString(writer);
_cover.WriteString(writer);
_sightedFriendPlayer.WriteString(writer);
_shooting.WriteString(writer);
_shootingVehicle.WriteString(writer);
_shootingBerserk.WriteString(writer);
_shootingGroup.WriteString(writer);
_shootingTraitor.WriteString(writer);
_taunt.WriteString(writer);
_tauntRe.WriteString(writer);
_flee.WriteString(writer);
_fleeRe.WriteString(writer);
_fleeLeaderDied.WriteString(writer);
_attemptedFlee.WriteString(writer);
_attemptedFleeRe.WriteString(writer);
_lostContact.WriteString(writer);
_hidingFinished.WriteString(writer);
_vehicleEntry.WriteString(writer);
_vehicleExit.WriteString(writer);
_vehicleWoohoo.WriteString(writer);
_vehicleScared.WriteString(writer);
_vehicleCollision.WriteString(writer);
_partiallySighted.WriteString(writer);
_nothingThere.WriteString(writer);
_pleading.WriteString(writer);
_surprise.WriteString(writer);
_berserk.WriteString(writer);
_meleeAttack.WriteString(writer);
_dive.WriteString(writer);
_uncoverExclamation.WriteString(writer);
_leapAttack.WriteString(writer);
_resurrection.WriteString(writer);
_celebration.WriteString(writer);
_checkBodyEnemy.WriteString(writer);
_checkBodyFriend.WriteString(writer);
_shootingDeadEnemy.WriteString(writer);
_shootingDeadEnemyPlayer.WriteString(writer);
_alone.WriteString(writer);
_unscathed.WriteString(writer);
_seriouslyWounded.WriteString(writer);
_seriouslyWoundedRe.WriteString(writer);
_massacre.WriteString(writer);
_massacreRe.WriteString(writer);
_rout.WriteString(writer);
_routRe.WriteString(writer);
}
}
  }
}
