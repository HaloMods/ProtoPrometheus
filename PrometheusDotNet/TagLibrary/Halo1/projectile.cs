using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Projectile : Object
  {
    public ProjectileBlock ProjectileValues = new ProjectileBlock();
    public new void Read(BinaryReader reader)
    {
      base.Read(reader);
      Trace.WriteLine("Loading 'Projectile'------------------------------------------------------");
      ProjectileValues.Read(reader);
    }
    public new void ReadChildData(BinaryReader reader)
    {
      base.ReadChildData(reader);
      ProjectileValues.ReadChildData(reader);
    }
    public new void Write(BinaryWriter writer)
    {
      base.Write(writer);
      ProjectileValues.Write(writer);
    }
    public new void WriteChildData(BinaryWriter writer)
    {
      base.WriteChildData(writer);
      ProjectileValues.WriteChildData(writer);
    }
public class ProjectileBlock : IBlock
{
private Flags  _flags;	
private Enum _detonationTimerStarts = new Enum();
private Enum _impactNoise = new Enum();
private Enum _aIn = new Enum();
private Enum _bIn = new Enum();
private Enum _cIn = new Enum();
private Enum _dIn = new Enum();
private TagReference _superDetonation = new TagReference();
private Real _aIPerceptionRadius = new Real();
private Real _collisionRadius = new Real();
private Real _armingTime = new Real();
private Real _dangerRadius = new Real();
private TagReference _effect = new TagReference();
private RealBounds _timer = new RealBounds();
private Real _minimumVelocity = new Real();
private Real _maximumRange = new Real();
private Real _airGravityScale = new Real();
private RealBounds _airDamageRange = new RealBounds();
private Real _waterGravityScale = new Real();
private RealBounds _waterDamageRange = new RealBounds();
private Real _initialVelocity = new Real();
private Real _finalVelocity = new Real();
private Angle _guidedAngularVelocity = new Angle();
private Enum _detonationNoise = new Enum();
private Pad  __unnamed;	
private TagReference _detonationStarted = new TagReference();
private TagReference _flybySound = new TagReference();
private TagReference _attachedDetonationDamage = new TagReference();
private TagReference _impactDamage = new TagReference();
private Pad  __unnamed2;	
private Block _materialResponses = new Block();
public class ProjectileMaterialResponseBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ProjectileMaterialResponseBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ProjectileMaterialResponseBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ProjectileMaterialResponseBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ProjectileMaterialResponseBlock this[int index]
  {
    get { return (InnerList[index] as ProjectileMaterialResponseBlock); }
  }
}
private ProjectileMaterialResponseBlockCollection _materialResponsesCollection;
public ProjectileMaterialResponseBlockCollection MaterialResponses
{
  get { return _materialResponsesCollection; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Enum DetonationTimerStarts
{
  get { return _detonationTimerStarts; }
  set { _detonationTimerStarts = value; }
}
public Enum ImpactNoise
{
  get { return _impactNoise; }
  set { _impactNoise = value; }
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
public TagReference SuperDetonation
{
  get { return _superDetonation; }
  set { _superDetonation = value; }
}
public Real AIPerceptionRadius
{
  get { return _aIPerceptionRadius; }
  set { _aIPerceptionRadius = value; }
}
public Real CollisionRadius
{
  get { return _collisionRadius; }
  set { _collisionRadius = value; }
}
public Real ArmingTime
{
  get { return _armingTime; }
  set { _armingTime = value; }
}
public Real DangerRadius
{
  get { return _dangerRadius; }
  set { _dangerRadius = value; }
}
public TagReference Effect
{
  get { return _effect; }
  set { _effect = value; }
}
public RealBounds Timer
{
  get { return _timer; }
  set { _timer = value; }
}
public Real MinimumVelocity
{
  get { return _minimumVelocity; }
  set { _minimumVelocity = value; }
}
public Real MaximumRange
{
  get { return _maximumRange; }
  set { _maximumRange = value; }
}
public Real AirGravityScale
{
  get { return _airGravityScale; }
  set { _airGravityScale = value; }
}
public RealBounds AirDamageRange
{
  get { return _airDamageRange; }
  set { _airDamageRange = value; }
}
public Real WaterGravityScale
{
  get { return _waterGravityScale; }
  set { _waterGravityScale = value; }
}
public RealBounds WaterDamageRange
{
  get { return _waterDamageRange; }
  set { _waterDamageRange = value; }
}
public Real InitialVelocity
{
  get { return _initialVelocity; }
  set { _initialVelocity = value; }
}
public Real FinalVelocity
{
  get { return _finalVelocity; }
  set { _finalVelocity = value; }
}
public Angle GuidedAngularVelocity
{
  get { return _guidedAngularVelocity; }
  set { _guidedAngularVelocity = value; }
}
public Enum DetonationNoise
{
  get { return _detonationNoise; }
  set { _detonationNoise = value; }
}
public TagReference DetonationStarted
{
  get { return _detonationStarted; }
  set { _detonationStarted = value; }
}
public TagReference FlybySound
{
  get { return _flybySound; }
  set { _flybySound = value; }
}
public TagReference AttachedDetonationDamage
{
  get { return _attachedDetonationDamage; }
  set { _attachedDetonationDamage = value; }
}
public TagReference ImpactDamage
{
  get { return _impactDamage; }
  set { _impactDamage = value; }
}
public ProjectileBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(2);
__unnamed2 = new Pad(12);
_materialResponsesCollection = new ProjectileMaterialResponseBlockCollection(_materialResponses);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _detonationTimerStarts.Read(reader);
  _impactNoise.Read(reader);
  _aIn.Read(reader);
  _bIn.Read(reader);
  _cIn.Read(reader);
  _dIn.Read(reader);
  _superDetonation.Read(reader);
  _aIPerceptionRadius.Read(reader);
  _collisionRadius.Read(reader);
  _armingTime.Read(reader);
  _dangerRadius.Read(reader);
  _effect.Read(reader);
  _timer.Read(reader);
  _minimumVelocity.Read(reader);
  _maximumRange.Read(reader);
  _airGravityScale.Read(reader);
  _airDamageRange.Read(reader);
  _waterGravityScale.Read(reader);
  _waterDamageRange.Read(reader);
  _initialVelocity.Read(reader);
  _finalVelocity.Read(reader);
  _guidedAngularVelocity.Read(reader);
  _detonationNoise.Read(reader);
  __unnamed.Read(reader);
  _detonationStarted.Read(reader);
  _flybySound.Read(reader);
  _attachedDetonationDamage.Read(reader);
  _impactDamage.Read(reader);
  __unnamed2.Read(reader);
  _materialResponses.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_superDetonation.ReadString(reader);
_effect.ReadString(reader);
_detonationStarted.ReadString(reader);
_flybySound.ReadString(reader);
_attachedDetonationDamage.ReadString(reader);
_impactDamage.ReadString(reader);
for (int x=0; x<_materialResponses.Count; x++)
{
  MaterialResponses.AddNew();
  MaterialResponses[x].Read(reader);
}
for (int x=0; x<_materialResponses.Count; x++)
  MaterialResponses[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _detonationTimerStarts.Write(writer);
    _impactNoise.Write(writer);
    _aIn.Write(writer);
    _bIn.Write(writer);
    _cIn.Write(writer);
    _dIn.Write(writer);
    _superDetonation.Write(writer);
    _aIPerceptionRadius.Write(writer);
    _collisionRadius.Write(writer);
    _armingTime.Write(writer);
    _dangerRadius.Write(writer);
    _effect.Write(writer);
    _timer.Write(writer);
    _minimumVelocity.Write(writer);
    _maximumRange.Write(writer);
    _airGravityScale.Write(writer);
    _airDamageRange.Write(writer);
    _waterGravityScale.Write(writer);
    _waterDamageRange.Write(writer);
    _initialVelocity.Write(writer);
    _finalVelocity.Write(writer);
    _guidedAngularVelocity.Write(writer);
    _detonationNoise.Write(writer);
    __unnamed.Write(writer);
    _detonationStarted.Write(writer);
    _flybySound.Write(writer);
    _attachedDetonationDamage.Write(writer);
    _impactDamage.Write(writer);
    __unnamed2.Write(writer);
    _materialResponses.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_superDetonation.WriteString(writer);
_effect.WriteString(writer);
_detonationStarted.WriteString(writer);
_flybySound.WriteString(writer);
_attachedDetonationDamage.WriteString(writer);
_impactDamage.WriteString(writer);
_materialResponses.UpdateReflexiveOffset(writer);
for (int x=0; x<_materialResponses.Count; x++)
{
  MaterialResponses[x].Write(writer);
}
for (int x=0; x<_materialResponses.Count; x++)
  MaterialResponses[x].WriteChildData(writer);
}
}
public class ProjectileMaterialResponseBlock : IBlock
{
private Flags  _flags;	
private Enum _response = new Enum();
private TagReference _effect = new TagReference();
private Pad  __unnamed;	
private Enum _response2 = new Enum();
private Flags  _flags2;	
private RealFraction _skipFraction = new RealFraction();
private AngleBounds _between = new AngleBounds();
private RealBounds _and = new RealBounds();
private TagReference _effect2 = new TagReference();
private Pad  __unnamed2;	
private Enum _scaleEffectsBy = new Enum();
private Pad  __unnamed3;	
private Angle _angularNoise = new Angle();
private Real _velocityNoise = new Real();
private TagReference _detonationEffect = new TagReference();
private Pad  __unnamed4;	
private Real _initialFriction = new Real();
private Real _maximumDistance = new Real();
private Real _parallelFriction = new Real();
private Real _perpendicularFriction = new Real();
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Enum Response
{
  get { return _response; }
  set { _response = value; }
}
public TagReference Effect
{
  get { return _effect; }
  set { _effect = value; }
}
public Enum Response2
{
  get { return _response2; }
  set { _response2 = value; }
}
public Flags Flags2
{
  get { return _flags2; }
  set { _flags2 = value; }
}
public RealFraction SkipFraction
{
  get { return _skipFraction; }
  set { _skipFraction = value; }
}
public AngleBounds Between
{
  get { return _between; }
  set { _between = value; }
}
public RealBounds And
{
  get { return _and; }
  set { _and = value; }
}
public TagReference Effect2
{
  get { return _effect2; }
  set { _effect2 = value; }
}
public Enum ScaleEffectsBy
{
  get { return _scaleEffectsBy; }
  set { _scaleEffectsBy = value; }
}
public Angle AngularNoise
{
  get { return _angularNoise; }
  set { _angularNoise = value; }
}
public Real VelocityNoise
{
  get { return _velocityNoise; }
  set { _velocityNoise = value; }
}
public TagReference DetonationEffect
{
  get { return _detonationEffect; }
  set { _detonationEffect = value; }
}
public Real InitialFriction
{
  get { return _initialFriction; }
  set { _initialFriction = value; }
}
public Real MaximumDistance
{
  get { return _maximumDistance; }
  set { _maximumDistance = value; }
}
public Real ParallelFriction
{
  get { return _parallelFriction; }
  set { _parallelFriction = value; }
}
public Real PerpendicularFriction
{
  get { return _perpendicularFriction; }
  set { _perpendicularFriction = value; }
}
public ProjectileMaterialResponseBlock()
{
_flags = new Flags(2);
__unnamed = new Pad(16);
_flags2 = new Flags(2);
__unnamed2 = new Pad(16);
__unnamed3 = new Pad(2);
__unnamed4 = new Pad(24);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _response.Read(reader);
  _effect.Read(reader);
  __unnamed.Read(reader);
  _response2.Read(reader);
  _flags2.Read(reader);
  _skipFraction.Read(reader);
  _between.Read(reader);
  _and.Read(reader);
  _effect2.Read(reader);
  __unnamed2.Read(reader);
  _scaleEffectsBy.Read(reader);
  __unnamed3.Read(reader);
  _angularNoise.Read(reader);
  _velocityNoise.Read(reader);
  _detonationEffect.Read(reader);
  __unnamed4.Read(reader);
  _initialFriction.Read(reader);
  _maximumDistance.Read(reader);
  _parallelFriction.Read(reader);
  _perpendicularFriction.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_effect.ReadString(reader);
_effect2.ReadString(reader);
_detonationEffect.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _response.Write(writer);
    _effect.Write(writer);
    __unnamed.Write(writer);
    _response2.Write(writer);
    _flags2.Write(writer);
    _skipFraction.Write(writer);
    _between.Write(writer);
    _and.Write(writer);
    _effect2.Write(writer);
    __unnamed2.Write(writer);
    _scaleEffectsBy.Write(writer);
    __unnamed3.Write(writer);
    _angularNoise.Write(writer);
    _velocityNoise.Write(writer);
    _detonationEffect.Write(writer);
    __unnamed4.Write(writer);
    _initialFriction.Write(writer);
    _maximumDistance.Write(writer);
    _parallelFriction.Write(writer);
    _perpendicularFriction.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_effect.WriteString(writer);
_effect2.WriteString(writer);
_detonationEffect.WriteString(writer);
}
}
  }
}
