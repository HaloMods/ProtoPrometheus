using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Vehicle : Unit
  {
    public VehicleBlock VehicleValues = new VehicleBlock();
    public override void Read(BinaryReader reader)
    {
      base.Read(reader);
      Trace.WriteLine("Loading 'Vehicle'------------------------------------------------------");
      VehicleValues.Read(reader);
    }
    public override void ReadChildData(BinaryReader reader)
    {
      base.ReadChildData(reader);
      VehicleValues.ReadChildData(reader);
    }
    public override void Write(BinaryWriter writer)
    {
      base.Write(writer);
      VehicleValues.Write(writer);
    }
    public override void WriteChildData(BinaryWriter writer)
    {
      base.WriteChildData(writer);
      VehicleValues.WriteChildData(writer);
    }
public class VehicleBlock : IBlock
{
private Flags  _flags;	
private Enum _type = new Enum();
private Pad  __unnamed;	
private Real _maximumForwardSpeed = new Real();
private Real _maximumReverseSpeed = new Real();
private Real _speedAcceleration = new Real();
private Real _speedDeceleration = new Real();
private Real _maximumLeftTurn = new Real();
private Real _maximumRightTurnNegative = new Real();
private Real _wheelCircumference = new Real();
private Real _turnRate = new Real();
private Real _blurSpeed = new Real();
private Enum _aIn = new Enum();
private Enum _bIn = new Enum();
private Enum _cIn = new Enum();
private Enum _dIn = new Enum();
private Pad  __unnamed2;	
private Real _maximumLeftSlide = new Real();
private Real _maximumRightSlide = new Real();
private Real _slideAcceleration = new Real();
private Real _slideDeceleration = new Real();
private Real _minimumFlippingAngularVelocity = new Real();
private Real _maximumFlippingAngularVelocity = new Real();
private Pad  __unnamed3;	
private Real _fixedGunYaw = new Real();
private Real _fixedGunPitch = new Real();
private Pad  __unnamed4;	
private Real _aiSideslipDistance = new Real();
private Real _aiDestinationRadius = new Real();
private Real _aiAvoidanceDistance = new Real();
private Real _aiPathfindingRadius = new Real();
private Real _aiChargeRepeatTimeout = new Real();
private Real _aiStrafingAbortRange = new Real();
private AngleBounds _aiOversteeringBounds = new AngleBounds();
private Angle _aiSteeringMaximum = new Angle();
private Real _aiThrottleMaximum = new Real();
private Real _aiMov = new Real();
private Pad  __unnamed5;	
private TagReference _suspensionSound = new TagReference();
private TagReference _crashSound = new TagReference();
private TagReference _materialEffects = new TagReference();
private TagReference _effect = new TagReference();
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
public Real MaximumForwardSpeed
{
  get { return _maximumForwardSpeed; }
  set { _maximumForwardSpeed = value; }
}
public Real MaximumReverseSpeed
{
  get { return _maximumReverseSpeed; }
  set { _maximumReverseSpeed = value; }
}
public Real SpeedAcceleration
{
  get { return _speedAcceleration; }
  set { _speedAcceleration = value; }
}
public Real SpeedDeceleration
{
  get { return _speedDeceleration; }
  set { _speedDeceleration = value; }
}
public Real MaximumLeftTurn
{
  get { return _maximumLeftTurn; }
  set { _maximumLeftTurn = value; }
}
public Real MaximumRightTurnNegative
{
  get { return _maximumRightTurnNegative; }
  set { _maximumRightTurnNegative = value; }
}
public Real WheelCircumference
{
  get { return _wheelCircumference; }
  set { _wheelCircumference = value; }
}
public Real TurnRate
{
  get { return _turnRate; }
  set { _turnRate = value; }
}
public Real BlurSpeed
{
  get { return _blurSpeed; }
  set { _blurSpeed = value; }
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
public Real MaximumLeftSlide
{
  get { return _maximumLeftSlide; }
  set { _maximumLeftSlide = value; }
}
public Real MaximumRightSlide
{
  get { return _maximumRightSlide; }
  set { _maximumRightSlide = value; }
}
public Real SlideAcceleration
{
  get { return _slideAcceleration; }
  set { _slideAcceleration = value; }
}
public Real SlideDeceleration
{
  get { return _slideDeceleration; }
  set { _slideDeceleration = value; }
}
public Real MinimumFlippingAngularVelocity
{
  get { return _minimumFlippingAngularVelocity; }
  set { _minimumFlippingAngularVelocity = value; }
}
public Real MaximumFlippingAngularVelocity
{
  get { return _maximumFlippingAngularVelocity; }
  set { _maximumFlippingAngularVelocity = value; }
}
public Real FixedGunYaw
{
  get { return _fixedGunYaw; }
  set { _fixedGunYaw = value; }
}
public Real FixedGunPitch
{
  get { return _fixedGunPitch; }
  set { _fixedGunPitch = value; }
}
public Real AiSideslipDistance
{
  get { return _aiSideslipDistance; }
  set { _aiSideslipDistance = value; }
}
public Real AiDestinationRadius
{
  get { return _aiDestinationRadius; }
  set { _aiDestinationRadius = value; }
}
public Real AiAvoidanceDistance
{
  get { return _aiAvoidanceDistance; }
  set { _aiAvoidanceDistance = value; }
}
public Real AiPathfindingRadius
{
  get { return _aiPathfindingRadius; }
  set { _aiPathfindingRadius = value; }
}
public Real AiChargeRepeatTimeout
{
  get { return _aiChargeRepeatTimeout; }
  set { _aiChargeRepeatTimeout = value; }
}
public Real AiStrafingAbortRange
{
  get { return _aiStrafingAbortRange; }
  set { _aiStrafingAbortRange = value; }
}
public AngleBounds AiOversteeringBounds
{
  get { return _aiOversteeringBounds; }
  set { _aiOversteeringBounds = value; }
}
public Angle AiSteeringMaximum
{
  get { return _aiSteeringMaximum; }
  set { _aiSteeringMaximum = value; }
}
public Real AiThrottleMaximum
{
  get { return _aiThrottleMaximum; }
  set { _aiThrottleMaximum = value; }
}
public Real AiMov
{
  get { return _aiMov; }
  set { _aiMov = value; }
}
public TagReference SuspensionSound
{
  get { return _suspensionSound; }
  set { _suspensionSound = value; }
}
public TagReference CrashSound
{
  get { return _crashSound; }
  set { _crashSound = value; }
}
public TagReference MaterialEffects
{
  get { return _materialEffects; }
  set { _materialEffects = value; }
}
public TagReference Effect
{
  get { return _effect; }
  set { _effect = value; }
}
public VehicleBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(2);
__unnamed2 = new Pad(12);
__unnamed3 = new Pad(24);
__unnamed4 = new Pad(24);
__unnamed5 = new Pad(4);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _type.Read(reader);
  __unnamed.Read(reader);
  _maximumForwardSpeed.Read(reader);
  _maximumReverseSpeed.Read(reader);
  _speedAcceleration.Read(reader);
  _speedDeceleration.Read(reader);
  _maximumLeftTurn.Read(reader);
  _maximumRightTurnNegative.Read(reader);
  _wheelCircumference.Read(reader);
  _turnRate.Read(reader);
  _blurSpeed.Read(reader);
  _aIn.Read(reader);
  _bIn.Read(reader);
  _cIn.Read(reader);
  _dIn.Read(reader);
  __unnamed2.Read(reader);
  _maximumLeftSlide.Read(reader);
  _maximumRightSlide.Read(reader);
  _slideAcceleration.Read(reader);
  _slideDeceleration.Read(reader);
  _minimumFlippingAngularVelocity.Read(reader);
  _maximumFlippingAngularVelocity.Read(reader);
  __unnamed3.Read(reader);
  _fixedGunYaw.Read(reader);
  _fixedGunPitch.Read(reader);
  __unnamed4.Read(reader);
  _aiSideslipDistance.Read(reader);
  _aiDestinationRadius.Read(reader);
  _aiAvoidanceDistance.Read(reader);
  _aiPathfindingRadius.Read(reader);
  _aiChargeRepeatTimeout.Read(reader);
  _aiStrafingAbortRange.Read(reader);
  _aiOversteeringBounds.Read(reader);
  _aiSteeringMaximum.Read(reader);
  _aiThrottleMaximum.Read(reader);
  _aiMov.Read(reader);
  __unnamed5.Read(reader);
  _suspensionSound.Read(reader);
  _crashSound.Read(reader);
  _materialEffects.Read(reader);
  _effect.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_suspensionSound.ReadString(reader);
_crashSound.ReadString(reader);
_materialEffects.ReadString(reader);
_effect.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _type.Write(writer);
    __unnamed.Write(writer);
    _maximumForwardSpeed.Write(writer);
    _maximumReverseSpeed.Write(writer);
    _speedAcceleration.Write(writer);
    _speedDeceleration.Write(writer);
    _maximumLeftTurn.Write(writer);
    _maximumRightTurnNegative.Write(writer);
    _wheelCircumference.Write(writer);
    _turnRate.Write(writer);
    _blurSpeed.Write(writer);
    _aIn.Write(writer);
    _bIn.Write(writer);
    _cIn.Write(writer);
    _dIn.Write(writer);
    __unnamed2.Write(writer);
    _maximumLeftSlide.Write(writer);
    _maximumRightSlide.Write(writer);
    _slideAcceleration.Write(writer);
    _slideDeceleration.Write(writer);
    _minimumFlippingAngularVelocity.Write(writer);
    _maximumFlippingAngularVelocity.Write(writer);
    __unnamed3.Write(writer);
    _fixedGunYaw.Write(writer);
    _fixedGunPitch.Write(writer);
    __unnamed4.Write(writer);
    _aiSideslipDistance.Write(writer);
    _aiDestinationRadius.Write(writer);
    _aiAvoidanceDistance.Write(writer);
    _aiPathfindingRadius.Write(writer);
    _aiChargeRepeatTimeout.Write(writer);
    _aiStrafingAbortRange.Write(writer);
    _aiOversteeringBounds.Write(writer);
    _aiSteeringMaximum.Write(writer);
    _aiThrottleMaximum.Write(writer);
    _aiMov.Write(writer);
    __unnamed5.Write(writer);
    _suspensionSound.Write(writer);
    _crashSound.Write(writer);
    _materialEffects.Write(writer);
    _effect.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_suspensionSound.WriteString(writer);
_crashSound.WriteString(writer);
_materialEffects.WriteString(writer);
_effect.WriteString(writer);
}
}
  }
}
