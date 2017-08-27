using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class DamageEffect : IBlock
  {
    public DamageEffectBlock DamageEffectValues = new DamageEffectBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'DamageEffect'------------------------------------------------------");
      DamageEffectValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      DamageEffectValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      DamageEffectValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      DamageEffectValues.WriteChildData(writer);
    }
public class DamageEffectBlock : IBlock
{
private RealBounds _radius = new RealBounds();
private RealFraction _cutoffScale = new RealFraction();
private Flags  _flags;	
private Pad  __unnamed;	
private Enum _type = new Enum();
private Enum _priority = new Enum();
private Pad  __unnamed2;	
private Real _duration = new Real();
private Enum _fadeFunction = new Enum();
private Pad  __unnamed3;	
private Pad  __unnamed4;	
private RealFraction _maximumIntensity = new RealFraction();
private Pad  __unnamed5;	
private RealARGBColor _color = new RealARGBColor();
private RealFraction _frequency = new RealFraction();
private Real _duration2 = new Real();
private Enum _fadeFunction2 = new Enum();
private Pad  __unnamed6;	
private Pad  __unnamed7;	
private RealFraction _frequency2 = new RealFraction();
private Real _duration3 = new Real();
private Enum _fadeFunction3 = new Enum();
private Pad  __unnamed8;	
private Pad  __unnamed9;	
private Pad  __unnamed10;	
private Pad  __unnamed11;	
private Real _duration4 = new Real();
private Enum _fadeFunction4 = new Enum();
private Pad  __unnamed12;	
private Angle _rotation = new Angle();
private Real _pushback = new Real();
private RealBounds _jitter = new RealBounds();
private Pad  __unnamed13;	
private Pad  __unnamed14;	
private Angle _angle = new Angle();
private Pad  __unnamed15;	
private Pad  __unnamed16;	
private Real _duration5 = new Real();
private Enum _falloffFunction = new Enum();
private Pad  __unnamed17;	
private Real _randomTranslation = new Real();
private Angle _randomRotation = new Angle();
private Pad  __unnamed18;	
private Enum _wobbleFunction = new Enum();
private Pad  __unnamed19;	
private Real _wobbleFunctionPeriod = new Real();
private RealFraction _wobbleWeight = new RealFraction();
private Pad  __unnamed20;	
private Pad  __unnamed21;	
private Pad  __unnamed22;	
private TagReference _sound = new TagReference();
private Pad  __unnamed23;	
private Real _forwardVelocity = new Real();
private Real _forwardRadius = new Real();
private Real _forwardExponent = new Real();
private Pad  __unnamed24;	
private Real _outwardVelocity = new Real();
private Real _outwardRadius = new Real();
private Real _outwardExponent = new Real();
private Pad  __unnamed25;	
private Enum _sideEffect = new Enum();
private Enum _category = new Enum();
private Flags  _flags2;	
private Real _aOECoreRadius = new Real();
private Real _damageLowerBound = new Real();
private RealBounds _damageUpperBound = new RealBounds();
private Real _vehiclePassthroughPenalty = new Real();
private Real _activeCamouflageDamage = new Real();
private Real _stun = new Real();
private Real _maximumStun = new Real();
private Real _stunTime = new Real();
private Pad  __unnamed26;	
private Real _instantaneousAcceleration = new Real();
private Pad  __unnamed27;	
private Pad  __unnamed28;	
private Real _dirt = new Real();
private Real _sand = new Real();
private Real _stone = new Real();
private Real _snow = new Real();
private Real _wood = new Real();
private Real _metalHollow = new Real();
private Real _metalThin = new Real();
private Real _metalThick = new Real();
private Real _rubber = new Real();
private Real _glass = new Real();
private Real _forceField = new Real();
private Real _grunt = new Real();
private Real _hunterArmor = new Real();
private Real _hunterSkin = new Real();
private Real _elite = new Real();
private Real _jackal = new Real();
private Real _jackalEnergyShield = new Real();
private Real _engineer = new Real();
private Real _engineerForceField = new Real();
private Real _floodCombatForm = new Real();
private Real _floodCarrierForm = new Real();
private Real _cyborg = new Real();
private Real _cyborgEnergyShield = new Real();
private Real _armoredHuman = new Real();
private Real _human = new Real();
private Real _sentinel = new Real();
private Real _monitor = new Real();
private Real _plastic = new Real();
private Real _water = new Real();
private Real _leaves = new Real();
private Real _eliteEnergyShield = new Real();
private Real _ice = new Real();
private Real _hunterShield = new Real();
private Pad  __unnamed29;	
public RealBounds Radius
{
  get { return _radius; }
  set { _radius = value; }
}
public RealFraction CutoffScale
{
  get { return _cutoffScale; }
  set { _cutoffScale = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Enum Type
{
  get { return _type; }
  set { _type = value; }
}
public Enum Priority
{
  get { return _priority; }
  set { _priority = value; }
}
public Real Duration
{
  get { return _duration; }
  set { _duration = value; }
}
public Enum FadeFunction
{
  get { return _fadeFunction; }
  set { _fadeFunction = value; }
}
public RealFraction MaximumIntensity
{
  get { return _maximumIntensity; }
  set { _maximumIntensity = value; }
}
public RealARGBColor Color
{
  get { return _color; }
  set { _color = value; }
}
public RealFraction Frequency
{
  get { return _frequency; }
  set { _frequency = value; }
}
public Real Duration2
{
  get { return _duration2; }
  set { _duration2 = value; }
}
public Enum FadeFunction2
{
  get { return _fadeFunction2; }
  set { _fadeFunction2 = value; }
}
public RealFraction Frequency2
{
  get { return _frequency2; }
  set { _frequency2 = value; }
}
public Real Duration3
{
  get { return _duration3; }
  set { _duration3 = value; }
}
public Enum FadeFunction3
{
  get { return _fadeFunction3; }
  set { _fadeFunction3 = value; }
}
public Real Duration4
{
  get { return _duration4; }
  set { _duration4 = value; }
}
public Enum FadeFunction4
{
  get { return _fadeFunction4; }
  set { _fadeFunction4 = value; }
}
public Angle Rotation
{
  get { return _rotation; }
  set { _rotation = value; }
}
public Real Pushback
{
  get { return _pushback; }
  set { _pushback = value; }
}
public RealBounds Jitter
{
  get { return _jitter; }
  set { _jitter = value; }
}
public Angle Angle
{
  get { return _angle; }
  set { _angle = value; }
}
public Real Duration5
{
  get { return _duration5; }
  set { _duration5 = value; }
}
public Enum FalloffFunction
{
  get { return _falloffFunction; }
  set { _falloffFunction = value; }
}
public Real RandomTranslation
{
  get { return _randomTranslation; }
  set { _randomTranslation = value; }
}
public Angle RandomRotation
{
  get { return _randomRotation; }
  set { _randomRotation = value; }
}
public Enum WobbleFunction
{
  get { return _wobbleFunction; }
  set { _wobbleFunction = value; }
}
public Real WobbleFunctionPeriod
{
  get { return _wobbleFunctionPeriod; }
  set { _wobbleFunctionPeriod = value; }
}
public RealFraction WobbleWeight
{
  get { return _wobbleWeight; }
  set { _wobbleWeight = value; }
}
public TagReference Sound
{
  get { return _sound; }
  set { _sound = value; }
}
public Real ForwardVelocity
{
  get { return _forwardVelocity; }
  set { _forwardVelocity = value; }
}
public Real ForwardRadius
{
  get { return _forwardRadius; }
  set { _forwardRadius = value; }
}
public Real ForwardExponent
{
  get { return _forwardExponent; }
  set { _forwardExponent = value; }
}
public Real OutwardVelocity
{
  get { return _outwardVelocity; }
  set { _outwardVelocity = value; }
}
public Real OutwardRadius
{
  get { return _outwardRadius; }
  set { _outwardRadius = value; }
}
public Real OutwardExponent
{
  get { return _outwardExponent; }
  set { _outwardExponent = value; }
}
public Enum SideEffect
{
  get { return _sideEffect; }
  set { _sideEffect = value; }
}
public Enum Category
{
  get { return _category; }
  set { _category = value; }
}
public Flags Flags2
{
  get { return _flags2; }
  set { _flags2 = value; }
}
public Real AOECoreRadius
{
  get { return _aOECoreRadius; }
  set { _aOECoreRadius = value; }
}
public Real DamageLowerBound
{
  get { return _damageLowerBound; }
  set { _damageLowerBound = value; }
}
public RealBounds DamageUpperBound
{
  get { return _damageUpperBound; }
  set { _damageUpperBound = value; }
}
public Real VehiclePassthroughPenalty
{
  get { return _vehiclePassthroughPenalty; }
  set { _vehiclePassthroughPenalty = value; }
}
public Real ActiveCamouflageDamage
{
  get { return _activeCamouflageDamage; }
  set { _activeCamouflageDamage = value; }
}
public Real Stun
{
  get { return _stun; }
  set { _stun = value; }
}
public Real MaximumStun
{
  get { return _maximumStun; }
  set { _maximumStun = value; }
}
public Real StunTime
{
  get { return _stunTime; }
  set { _stunTime = value; }
}
public Real InstantaneousAcceleration
{
  get { return _instantaneousAcceleration; }
  set { _instantaneousAcceleration = value; }
}
public Real Dirt
{
  get { return _dirt; }
  set { _dirt = value; }
}
public Real Sand
{
  get { return _sand; }
  set { _sand = value; }
}
public Real Stone
{
  get { return _stone; }
  set { _stone = value; }
}
public Real Snow
{
  get { return _snow; }
  set { _snow = value; }
}
public Real Wood
{
  get { return _wood; }
  set { _wood = value; }
}
public Real MetalHollow
{
  get { return _metalHollow; }
  set { _metalHollow = value; }
}
public Real MetalThin
{
  get { return _metalThin; }
  set { _metalThin = value; }
}
public Real MetalThick
{
  get { return _metalThick; }
  set { _metalThick = value; }
}
public Real Rubber
{
  get { return _rubber; }
  set { _rubber = value; }
}
public Real Glass
{
  get { return _glass; }
  set { _glass = value; }
}
public Real ForceField
{
  get { return _forceField; }
  set { _forceField = value; }
}
public Real Grunt
{
  get { return _grunt; }
  set { _grunt = value; }
}
public Real HunterArmor
{
  get { return _hunterArmor; }
  set { _hunterArmor = value; }
}
public Real HunterSkin
{
  get { return _hunterSkin; }
  set { _hunterSkin = value; }
}
public Real Elite
{
  get { return _elite; }
  set { _elite = value; }
}
public Real Jackal
{
  get { return _jackal; }
  set { _jackal = value; }
}
public Real JackalEnergyShield
{
  get { return _jackalEnergyShield; }
  set { _jackalEnergyShield = value; }
}
public Real Engineer
{
  get { return _engineer; }
  set { _engineer = value; }
}
public Real EngineerForceField
{
  get { return _engineerForceField; }
  set { _engineerForceField = value; }
}
public Real FloodCombatForm
{
  get { return _floodCombatForm; }
  set { _floodCombatForm = value; }
}
public Real FloodCarrierForm
{
  get { return _floodCarrierForm; }
  set { _floodCarrierForm = value; }
}
public Real Cyborg
{
  get { return _cyborg; }
  set { _cyborg = value; }
}
public Real CyborgEnergyShield
{
  get { return _cyborgEnergyShield; }
  set { _cyborgEnergyShield = value; }
}
public Real ArmoredHuman
{
  get { return _armoredHuman; }
  set { _armoredHuman = value; }
}
public Real Human
{
  get { return _human; }
  set { _human = value; }
}
public Real Sentinel
{
  get { return _sentinel; }
  set { _sentinel = value; }
}
public Real Monitor
{
  get { return _monitor; }
  set { _monitor = value; }
}
public Real Plastic
{
  get { return _plastic; }
  set { _plastic = value; }
}
public Real Water
{
  get { return _water; }
  set { _water = value; }
}
public Real Leaves
{
  get { return _leaves; }
  set { _leaves = value; }
}
public Real EliteEnergyShield
{
  get { return _eliteEnergyShield; }
  set { _eliteEnergyShield = value; }
}
public Real Ice
{
  get { return _ice; }
  set { _ice = value; }
}
public Real HunterShield
{
  get { return _hunterShield; }
  set { _hunterShield = value; }
}
public DamageEffectBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(20);
__unnamed2 = new Pad(12);
__unnamed3 = new Pad(2);
__unnamed4 = new Pad(8);
__unnamed5 = new Pad(4);
__unnamed6 = new Pad(2);
__unnamed7 = new Pad(8);
__unnamed8 = new Pad(2);
__unnamed9 = new Pad(8);
__unnamed10 = new Pad(4);
__unnamed11 = new Pad(16);
__unnamed12 = new Pad(2);
__unnamed13 = new Pad(4);
__unnamed14 = new Pad(4);
__unnamed15 = new Pad(4);
__unnamed16 = new Pad(12);
__unnamed17 = new Pad(2);
__unnamed18 = new Pad(12);
__unnamed19 = new Pad(2);
__unnamed20 = new Pad(4);
__unnamed21 = new Pad(20);
__unnamed22 = new Pad(8);
__unnamed23 = new Pad(112);
__unnamed24 = new Pad(12);
__unnamed25 = new Pad(12);
_flags2 = new Flags(4);
__unnamed26 = new Pad(4);
__unnamed27 = new Pad(4);
__unnamed28 = new Pad(4);
__unnamed29 = new Pad(28);

}
public void Read(BinaryReader reader)
{
  _radius.Read(reader);
  _cutoffScale.Read(reader);
  _flags.Read(reader);
  __unnamed.Read(reader);
  _type.Read(reader);
  _priority.Read(reader);
  __unnamed2.Read(reader);
  _duration.Read(reader);
  _fadeFunction.Read(reader);
  __unnamed3.Read(reader);
  __unnamed4.Read(reader);
  _maximumIntensity.Read(reader);
  __unnamed5.Read(reader);
  _color.Read(reader);
  _frequency.Read(reader);
  _duration2.Read(reader);
  _fadeFunction2.Read(reader);
  __unnamed6.Read(reader);
  __unnamed7.Read(reader);
  _frequency2.Read(reader);
  _duration3.Read(reader);
  _fadeFunction3.Read(reader);
  __unnamed8.Read(reader);
  __unnamed9.Read(reader);
  __unnamed10.Read(reader);
  __unnamed11.Read(reader);
  _duration4.Read(reader);
  _fadeFunction4.Read(reader);
  __unnamed12.Read(reader);
  _rotation.Read(reader);
  _pushback.Read(reader);
  _jitter.Read(reader);
  __unnamed13.Read(reader);
  __unnamed14.Read(reader);
  _angle.Read(reader);
  __unnamed15.Read(reader);
  __unnamed16.Read(reader);
  _duration5.Read(reader);
  _falloffFunction.Read(reader);
  __unnamed17.Read(reader);
  _randomTranslation.Read(reader);
  _randomRotation.Read(reader);
  __unnamed18.Read(reader);
  _wobbleFunction.Read(reader);
  __unnamed19.Read(reader);
  _wobbleFunctionPeriod.Read(reader);
  _wobbleWeight.Read(reader);
  __unnamed20.Read(reader);
  __unnamed21.Read(reader);
  __unnamed22.Read(reader);
  _sound.Read(reader);
  __unnamed23.Read(reader);
  _forwardVelocity.Read(reader);
  _forwardRadius.Read(reader);
  _forwardExponent.Read(reader);
  __unnamed24.Read(reader);
  _outwardVelocity.Read(reader);
  _outwardRadius.Read(reader);
  _outwardExponent.Read(reader);
  __unnamed25.Read(reader);
  _sideEffect.Read(reader);
  _category.Read(reader);
  _flags2.Read(reader);
  _aOECoreRadius.Read(reader);
  _damageLowerBound.Read(reader);
  _damageUpperBound.Read(reader);
  _vehiclePassthroughPenalty.Read(reader);
  _activeCamouflageDamage.Read(reader);
  _stun.Read(reader);
  _maximumStun.Read(reader);
  _stunTime.Read(reader);
  __unnamed26.Read(reader);
  _instantaneousAcceleration.Read(reader);
  __unnamed27.Read(reader);
  __unnamed28.Read(reader);
  _dirt.Read(reader);
  _sand.Read(reader);
  _stone.Read(reader);
  _snow.Read(reader);
  _wood.Read(reader);
  _metalHollow.Read(reader);
  _metalThin.Read(reader);
  _metalThick.Read(reader);
  _rubber.Read(reader);
  _glass.Read(reader);
  _forceField.Read(reader);
  _grunt.Read(reader);
  _hunterArmor.Read(reader);
  _hunterSkin.Read(reader);
  _elite.Read(reader);
  _jackal.Read(reader);
  _jackalEnergyShield.Read(reader);
  _engineer.Read(reader);
  _engineerForceField.Read(reader);
  _floodCombatForm.Read(reader);
  _floodCarrierForm.Read(reader);
  _cyborg.Read(reader);
  _cyborgEnergyShield.Read(reader);
  _armoredHuman.Read(reader);
  _human.Read(reader);
  _sentinel.Read(reader);
  _monitor.Read(reader);
  _plastic.Read(reader);
  _water.Read(reader);
  _leaves.Read(reader);
  _eliteEnergyShield.Read(reader);
  _ice.Read(reader);
  _hunterShield.Read(reader);
  __unnamed29.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_sound.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _radius.Write(writer);
    _cutoffScale.Write(writer);
    _flags.Write(writer);
    __unnamed.Write(writer);
    _type.Write(writer);
    _priority.Write(writer);
    __unnamed2.Write(writer);
    _duration.Write(writer);
    _fadeFunction.Write(writer);
    __unnamed3.Write(writer);
    __unnamed4.Write(writer);
    _maximumIntensity.Write(writer);
    __unnamed5.Write(writer);
    _color.Write(writer);
    _frequency.Write(writer);
    _duration2.Write(writer);
    _fadeFunction2.Write(writer);
    __unnamed6.Write(writer);
    __unnamed7.Write(writer);
    _frequency2.Write(writer);
    _duration3.Write(writer);
    _fadeFunction3.Write(writer);
    __unnamed8.Write(writer);
    __unnamed9.Write(writer);
    __unnamed10.Write(writer);
    __unnamed11.Write(writer);
    _duration4.Write(writer);
    _fadeFunction4.Write(writer);
    __unnamed12.Write(writer);
    _rotation.Write(writer);
    _pushback.Write(writer);
    _jitter.Write(writer);
    __unnamed13.Write(writer);
    __unnamed14.Write(writer);
    _angle.Write(writer);
    __unnamed15.Write(writer);
    __unnamed16.Write(writer);
    _duration5.Write(writer);
    _falloffFunction.Write(writer);
    __unnamed17.Write(writer);
    _randomTranslation.Write(writer);
    _randomRotation.Write(writer);
    __unnamed18.Write(writer);
    _wobbleFunction.Write(writer);
    __unnamed19.Write(writer);
    _wobbleFunctionPeriod.Write(writer);
    _wobbleWeight.Write(writer);
    __unnamed20.Write(writer);
    __unnamed21.Write(writer);
    __unnamed22.Write(writer);
    _sound.Write(writer);
    __unnamed23.Write(writer);
    _forwardVelocity.Write(writer);
    _forwardRadius.Write(writer);
    _forwardExponent.Write(writer);
    __unnamed24.Write(writer);
    _outwardVelocity.Write(writer);
    _outwardRadius.Write(writer);
    _outwardExponent.Write(writer);
    __unnamed25.Write(writer);
    _sideEffect.Write(writer);
    _category.Write(writer);
    _flags2.Write(writer);
    _aOECoreRadius.Write(writer);
    _damageLowerBound.Write(writer);
    _damageUpperBound.Write(writer);
    _vehiclePassthroughPenalty.Write(writer);
    _activeCamouflageDamage.Write(writer);
    _stun.Write(writer);
    _maximumStun.Write(writer);
    _stunTime.Write(writer);
    __unnamed26.Write(writer);
    _instantaneousAcceleration.Write(writer);
    __unnamed27.Write(writer);
    __unnamed28.Write(writer);
    _dirt.Write(writer);
    _sand.Write(writer);
    _stone.Write(writer);
    _snow.Write(writer);
    _wood.Write(writer);
    _metalHollow.Write(writer);
    _metalThin.Write(writer);
    _metalThick.Write(writer);
    _rubber.Write(writer);
    _glass.Write(writer);
    _forceField.Write(writer);
    _grunt.Write(writer);
    _hunterArmor.Write(writer);
    _hunterSkin.Write(writer);
    _elite.Write(writer);
    _jackal.Write(writer);
    _jackalEnergyShield.Write(writer);
    _engineer.Write(writer);
    _engineerForceField.Write(writer);
    _floodCombatForm.Write(writer);
    _floodCarrierForm.Write(writer);
    _cyborg.Write(writer);
    _cyborgEnergyShield.Write(writer);
    _armoredHuman.Write(writer);
    _human.Write(writer);
    _sentinel.Write(writer);
    _monitor.Write(writer);
    _plastic.Write(writer);
    _water.Write(writer);
    _leaves.Write(writer);
    _eliteEnergyShield.Write(writer);
    _ice.Write(writer);
    _hunterShield.Write(writer);
    __unnamed29.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_sound.WriteString(writer);
}
}
  }
}
