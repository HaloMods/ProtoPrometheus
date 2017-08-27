using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Biped : Unit
  {
    public BipedBlock BipedValues = new BipedBlock();
    public override void Read(BinaryReader reader)
    {
      base.Read(reader);
      Trace.WriteLine("Loading 'Biped'------------------------------------------------------");
      BipedValues.Read(reader);
    }
    public override void ReadChildData(BinaryReader reader)
    {
      base.ReadChildData(reader);
      BipedValues.ReadChildData(reader);
    }
    public override void Write(BinaryWriter writer)
    {
      base.Write(writer);
      BipedValues.Write(writer);
    }
    public override void WriteChildData(BinaryWriter writer)
    {
      base.WriteChildData(writer);
      BipedValues.WriteChildData(writer);
    }
public class BipedBlock : IBlock
{
private Angle _movingTurningSpeed = new Angle();
private Flags  _flags;	
private Angle _stationaryTurningThreshold = new Angle();
private Pad  __unnamed;	
private Enum _aIn = new Enum();
private Enum _bIn = new Enum();
private Enum _cIn = new Enum();
private Enum _dIn = new Enum();
private TagReference _dONTUSE = new TagReference();
private Angle _bankAngle = new Angle();
private Real _bankApplyTime = new Real();
private Real _bankDecayTime = new Real();
private Real _pitchRatio = new Real();
private Real _maxVelocity = new Real();
private Real _maxSidestepVelocity = new Real();
private Real _acceleration = new Real();
private Real _deceleration = new Real();
private Angle _angularVelocityMaximum = new Angle();
private Angle _angularAccelerationMaximum = new Angle();
private Real _crouchVelocityModifier = new Real();
private Pad  __unnamed2;	
private Angle _maximumSlopeAngle = new Angle();
private Angle _downhillFalloffAngle = new Angle();
private Angle _downhillCutoffAngle = new Angle();
private Real _downhillVelocityScale = new Real();
private Angle _uphillFalloffAngle = new Angle();
private Angle _uphillCutoffAngle = new Angle();
private Real _uphillVelocityScale = new Real();
private Pad  __unnamed3;	
private TagReference _footsteps = new TagReference();
private Pad  __unnamed4;	
private Real _jumpVelocity = new Real();
private Pad  __unnamed5;	
private Real _maximumSoftLandingTime = new Real();
private Real _maximumHardLandingTime = new Real();
private Real _minimumSoftLandingVelocity = new Real();
private Real _minimumHardLandingVelocity = new Real();
private Real _maximumHardLandingVelocity = new Real();
private Real _deathHardLandingVelocity = new Real();
private Pad  __unnamed6;	
private Real _standingCameraHeight = new Real();
private Real _crouchingCameraHeight = new Real();
private Real _crouchTransitionTime = new Real();
private Pad  __unnamed7;	
private Real _standingCollisionHeight = new Real();
private Real _crouchingCollisionHeight = new Real();
private Real _collisionRadius = new Real();
private Pad  __unnamed8;	
private Real _autoaimWidth = new Real();
private Pad  __unnamed9;	
private Block _contactPoints = new Block();
public class ContactPointBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ContactPointBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ContactPointBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ContactPointBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ContactPointBlock this[int index]
  {
    get { return (InnerList[index] as ContactPointBlock); }
  }
}
private ContactPointBlockCollection _contactPointsCollection;
public ContactPointBlockCollection ContactPoints
{
  get { return _contactPointsCollection; }
}
public Angle MovingTurningSpeed
{
  get { return _movingTurningSpeed; }
  set { _movingTurningSpeed = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Angle StationaryTurningThreshold
{
  get { return _stationaryTurningThreshold; }
  set { _stationaryTurningThreshold = value; }
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
public TagReference DONTUSE
{
  get { return _dONTUSE; }
  set { _dONTUSE = value; }
}
public Angle BankAngle
{
  get { return _bankAngle; }
  set { _bankAngle = value; }
}
public Real BankApplyTime
{
  get { return _bankApplyTime; }
  set { _bankApplyTime = value; }
}
public Real BankDecayTime
{
  get { return _bankDecayTime; }
  set { _bankDecayTime = value; }
}
public Real PitchRatio
{
  get { return _pitchRatio; }
  set { _pitchRatio = value; }
}
public Real MaxVelocity
{
  get { return _maxVelocity; }
  set { _maxVelocity = value; }
}
public Real MaxSidestepVelocity
{
  get { return _maxSidestepVelocity; }
  set { _maxSidestepVelocity = value; }
}
public Real Acceleration
{
  get { return _acceleration; }
  set { _acceleration = value; }
}
public Real Deceleration
{
  get { return _deceleration; }
  set { _deceleration = value; }
}
public Angle AngularVelocityMaximum
{
  get { return _angularVelocityMaximum; }
  set { _angularVelocityMaximum = value; }
}
public Angle AngularAccelerationMaximum
{
  get { return _angularAccelerationMaximum; }
  set { _angularAccelerationMaximum = value; }
}
public Real CrouchVelocityModifier
{
  get { return _crouchVelocityModifier; }
  set { _crouchVelocityModifier = value; }
}
public Angle MaximumSlopeAngle
{
  get { return _maximumSlopeAngle; }
  set { _maximumSlopeAngle = value; }
}
public Angle DownhillFalloffAngle
{
  get { return _downhillFalloffAngle; }
  set { _downhillFalloffAngle = value; }
}
public Angle DownhillCutoffAngle
{
  get { return _downhillCutoffAngle; }
  set { _downhillCutoffAngle = value; }
}
public Real DownhillVelocityScale
{
  get { return _downhillVelocityScale; }
  set { _downhillVelocityScale = value; }
}
public Angle UphillFalloffAngle
{
  get { return _uphillFalloffAngle; }
  set { _uphillFalloffAngle = value; }
}
public Angle UphillCutoffAngle
{
  get { return _uphillCutoffAngle; }
  set { _uphillCutoffAngle = value; }
}
public Real UphillVelocityScale
{
  get { return _uphillVelocityScale; }
  set { _uphillVelocityScale = value; }
}
public TagReference Footsteps
{
  get { return _footsteps; }
  set { _footsteps = value; }
}
public Real JumpVelocity
{
  get { return _jumpVelocity; }
  set { _jumpVelocity = value; }
}
public Real MaximumSoftLandingTime
{
  get { return _maximumSoftLandingTime; }
  set { _maximumSoftLandingTime = value; }
}
public Real MaximumHardLandingTime
{
  get { return _maximumHardLandingTime; }
  set { _maximumHardLandingTime = value; }
}
public Real MinimumSoftLandingVelocity
{
  get { return _minimumSoftLandingVelocity; }
  set { _minimumSoftLandingVelocity = value; }
}
public Real MinimumHardLandingVelocity
{
  get { return _minimumHardLandingVelocity; }
  set { _minimumHardLandingVelocity = value; }
}
public Real MaximumHardLandingVelocity
{
  get { return _maximumHardLandingVelocity; }
  set { _maximumHardLandingVelocity = value; }
}
public Real DeathHardLandingVelocity
{
  get { return _deathHardLandingVelocity; }
  set { _deathHardLandingVelocity = value; }
}
public Real StandingCameraHeight
{
  get { return _standingCameraHeight; }
  set { _standingCameraHeight = value; }
}
public Real CrouchingCameraHeight
{
  get { return _crouchingCameraHeight; }
  set { _crouchingCameraHeight = value; }
}
public Real CrouchTransitionTime
{
  get { return _crouchTransitionTime; }
  set { _crouchTransitionTime = value; }
}
public Real StandingCollisionHeight
{
  get { return _standingCollisionHeight; }
  set { _standingCollisionHeight = value; }
}
public Real CrouchingCollisionHeight
{
  get { return _crouchingCollisionHeight; }
  set { _crouchingCollisionHeight = value; }
}
public Real CollisionRadius
{
  get { return _collisionRadius; }
  set { _collisionRadius = value; }
}
public Real AutoaimWidth
{
  get { return _autoaimWidth; }
  set { _autoaimWidth = value; }
}
public BipedBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(16);
__unnamed2 = new Pad(8);
__unnamed3 = new Pad(24);
__unnamed4 = new Pad(24);
__unnamed5 = new Pad(28);
__unnamed6 = new Pad(20);
__unnamed7 = new Pad(24);
__unnamed8 = new Pad(40);
__unnamed9 = new Pad(140);
_contactPointsCollection = new ContactPointBlockCollection(_contactPoints);

}
public void Read(BinaryReader reader)
{
  _movingTurningSpeed.Read(reader);
  _flags.Read(reader);
  _stationaryTurningThreshold.Read(reader);
  __unnamed.Read(reader);
  _aIn.Read(reader);
  _bIn.Read(reader);
  _cIn.Read(reader);
  _dIn.Read(reader);
  _dONTUSE.Read(reader);
  _bankAngle.Read(reader);
  _bankApplyTime.Read(reader);
  _bankDecayTime.Read(reader);
  _pitchRatio.Read(reader);
  _maxVelocity.Read(reader);
  _maxSidestepVelocity.Read(reader);
  _acceleration.Read(reader);
  _deceleration.Read(reader);
  _angularVelocityMaximum.Read(reader);
  _angularAccelerationMaximum.Read(reader);
  _crouchVelocityModifier.Read(reader);
  __unnamed2.Read(reader);
  _maximumSlopeAngle.Read(reader);
  _downhillFalloffAngle.Read(reader);
  _downhillCutoffAngle.Read(reader);
  _downhillVelocityScale.Read(reader);
  _uphillFalloffAngle.Read(reader);
  _uphillCutoffAngle.Read(reader);
  _uphillVelocityScale.Read(reader);
  __unnamed3.Read(reader);
  _footsteps.Read(reader);
  __unnamed4.Read(reader);
  _jumpVelocity.Read(reader);
  __unnamed5.Read(reader);
  _maximumSoftLandingTime.Read(reader);
  _maximumHardLandingTime.Read(reader);
  _minimumSoftLandingVelocity.Read(reader);
  _minimumHardLandingVelocity.Read(reader);
  _maximumHardLandingVelocity.Read(reader);
  _deathHardLandingVelocity.Read(reader);
  __unnamed6.Read(reader);
  _standingCameraHeight.Read(reader);
  _crouchingCameraHeight.Read(reader);
  _crouchTransitionTime.Read(reader);
  __unnamed7.Read(reader);
  _standingCollisionHeight.Read(reader);
  _crouchingCollisionHeight.Read(reader);
  _collisionRadius.Read(reader);
  __unnamed8.Read(reader);
  _autoaimWidth.Read(reader);
  __unnamed9.Read(reader);
  _contactPoints.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_dONTUSE.ReadString(reader);
_footsteps.ReadString(reader);
for (int x=0; x<_contactPoints.Count; x++)
{
  ContactPoints.AddNew();
  ContactPoints[x].Read(reader);
}
for (int x=0; x<_contactPoints.Count; x++)
  ContactPoints[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _movingTurningSpeed.Write(writer);
    _flags.Write(writer);
    _stationaryTurningThreshold.Write(writer);
    __unnamed.Write(writer);
    _aIn.Write(writer);
    _bIn.Write(writer);
    _cIn.Write(writer);
    _dIn.Write(writer);
    _dONTUSE.Write(writer);
    _bankAngle.Write(writer);
    _bankApplyTime.Write(writer);
    _bankDecayTime.Write(writer);
    _pitchRatio.Write(writer);
    _maxVelocity.Write(writer);
    _maxSidestepVelocity.Write(writer);
    _acceleration.Write(writer);
    _deceleration.Write(writer);
    _angularVelocityMaximum.Write(writer);
    _angularAccelerationMaximum.Write(writer);
    _crouchVelocityModifier.Write(writer);
    __unnamed2.Write(writer);
    _maximumSlopeAngle.Write(writer);
    _downhillFalloffAngle.Write(writer);
    _downhillCutoffAngle.Write(writer);
    _downhillVelocityScale.Write(writer);
    _uphillFalloffAngle.Write(writer);
    _uphillCutoffAngle.Write(writer);
    _uphillVelocityScale.Write(writer);
    __unnamed3.Write(writer);
    _footsteps.Write(writer);
    __unnamed4.Write(writer);
    _jumpVelocity.Write(writer);
    __unnamed5.Write(writer);
    _maximumSoftLandingTime.Write(writer);
    _maximumHardLandingTime.Write(writer);
    _minimumSoftLandingVelocity.Write(writer);
    _minimumHardLandingVelocity.Write(writer);
    _maximumHardLandingVelocity.Write(writer);
    _deathHardLandingVelocity.Write(writer);
    __unnamed6.Write(writer);
    _standingCameraHeight.Write(writer);
    _crouchingCameraHeight.Write(writer);
    _crouchTransitionTime.Write(writer);
    __unnamed7.Write(writer);
    _standingCollisionHeight.Write(writer);
    _crouchingCollisionHeight.Write(writer);
    _collisionRadius.Write(writer);
    __unnamed8.Write(writer);
    _autoaimWidth.Write(writer);
    __unnamed9.Write(writer);
    _contactPoints.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_dONTUSE.WriteString(writer);
_footsteps.WriteString(writer);
_contactPoints.UpdateReflexiveOffset(writer);
for (int x=0; x<_contactPoints.Count; x++)
{
  ContactPoints[x].Write(writer);
}
for (int x=0; x<_contactPoints.Count; x++)
  ContactPoints[x].WriteChildData(writer);
}
}
public class ContactPointBlock : IBlock
{
private Pad  __unnamed;	
private FixedLengthString _markerName = new FixedLengthString();
public FixedLengthString MarkerName
{
  get { return _markerName; }
  set { _markerName = value; }
}
public ContactPointBlock()
{
__unnamed = new Pad(32);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  _markerName.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    _markerName.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
  }
}
