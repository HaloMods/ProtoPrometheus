using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class ContinuousDamageEffect : IBlock
  {
    public ContinuousDamageEffectBlock ContinuousDamageEffectValues = new ContinuousDamageEffectBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'ContinuousDamageEffect'------------------------------------------------------");
      ContinuousDamageEffectValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      ContinuousDamageEffectValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      ContinuousDamageEffectValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      ContinuousDamageEffectValues.WriteChildData(writer);
    }
public class ContinuousDamageEffectBlock : IBlock
{
private RealBounds _radius = new RealBounds();
private RealFraction _cutoffScale = new RealFraction();
private Pad  __unnamed;	
private RealFraction _lowFrequency = new RealFraction();
private RealFraction _highFrequency = new RealFraction();
private Pad  __unnamed2;	
private Real _randomTranslation = new Real();
private Angle _randomRotation = new Angle();
private Pad  __unnamed3;	
private Enum _wobbleFunction = new Enum();
private Pad  __unnamed4;	
private Real _wobbleFunctionPeriod = new Real();
private RealFraction _wobbleWeight = new RealFraction();
private Pad  __unnamed5;	
private Pad  __unnamed6;	
private Pad  __unnamed7;	
private Pad  __unnamed8;	
private Enum _sideEffect = new Enum();
private Enum _category = new Enum();
private Flags  _flags;	
private Pad  __unnamed9;	
private Real _damageLowerBound = new Real();
private RealBounds _damageUpperBound = new RealBounds();
private Real _vehiclePassthroughPenalty = new Real();
private Pad  __unnamed10;	
private Real _stun = new Real();
private Real _maximumStun = new Real();
private Real _stunTime = new Real();
private Pad  __unnamed11;	
private Real _instantaneousAcceleration = new Real();
private Pad  __unnamed12;	
private Pad  __unnamed13;	
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
private Pad  __unnamed14;	
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
public RealFraction LowFrequency
{
  get { return _lowFrequency; }
  set { _lowFrequency = value; }
}
public RealFraction HighFrequency
{
  get { return _highFrequency; }
  set { _highFrequency = value; }
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
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
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
public ContinuousDamageEffectBlock()
{
__unnamed = new Pad(24);
__unnamed2 = new Pad(24);
__unnamed3 = new Pad(12);
__unnamed4 = new Pad(2);
__unnamed5 = new Pad(4);
__unnamed6 = new Pad(20);
__unnamed7 = new Pad(8);
__unnamed8 = new Pad(160);
_flags = new Flags(4);
__unnamed9 = new Pad(4);
__unnamed10 = new Pad(4);
__unnamed11 = new Pad(4);
__unnamed12 = new Pad(4);
__unnamed13 = new Pad(4);
__unnamed14 = new Pad(28);

}
public void Read(BinaryReader reader)
{
  _radius.Read(reader);
  _cutoffScale.Read(reader);
  __unnamed.Read(reader);
  _lowFrequency.Read(reader);
  _highFrequency.Read(reader);
  __unnamed2.Read(reader);
  _randomTranslation.Read(reader);
  _randomRotation.Read(reader);
  __unnamed3.Read(reader);
  _wobbleFunction.Read(reader);
  __unnamed4.Read(reader);
  _wobbleFunctionPeriod.Read(reader);
  _wobbleWeight.Read(reader);
  __unnamed5.Read(reader);
  __unnamed6.Read(reader);
  __unnamed7.Read(reader);
  __unnamed8.Read(reader);
  _sideEffect.Read(reader);
  _category.Read(reader);
  _flags.Read(reader);
  __unnamed9.Read(reader);
  _damageLowerBound.Read(reader);
  _damageUpperBound.Read(reader);
  _vehiclePassthroughPenalty.Read(reader);
  __unnamed10.Read(reader);
  _stun.Read(reader);
  _maximumStun.Read(reader);
  _stunTime.Read(reader);
  __unnamed11.Read(reader);
  _instantaneousAcceleration.Read(reader);
  __unnamed12.Read(reader);
  __unnamed13.Read(reader);
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
  __unnamed14.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _radius.Write(writer);
    _cutoffScale.Write(writer);
    __unnamed.Write(writer);
    _lowFrequency.Write(writer);
    _highFrequency.Write(writer);
    __unnamed2.Write(writer);
    _randomTranslation.Write(writer);
    _randomRotation.Write(writer);
    __unnamed3.Write(writer);
    _wobbleFunction.Write(writer);
    __unnamed4.Write(writer);
    _wobbleFunctionPeriod.Write(writer);
    _wobbleWeight.Write(writer);
    __unnamed5.Write(writer);
    __unnamed6.Write(writer);
    __unnamed7.Write(writer);
    __unnamed8.Write(writer);
    _sideEffect.Write(writer);
    _category.Write(writer);
    _flags.Write(writer);
    __unnamed9.Write(writer);
    _damageLowerBound.Write(writer);
    _damageUpperBound.Write(writer);
    _vehiclePassthroughPenalty.Write(writer);
    __unnamed10.Write(writer);
    _stun.Write(writer);
    _maximumStun.Write(writer);
    _stunTime.Write(writer);
    __unnamed11.Write(writer);
    _instantaneousAcceleration.Write(writer);
    __unnamed12.Write(writer);
    __unnamed13.Write(writer);
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
    __unnamed14.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
  }
}
