using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Effect : IBlock
  {
    public EffectBlock EffectValues = new EffectBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'Effect'------------------------------------------------------");
      EffectValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      EffectValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      EffectValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      EffectValues.WriteChildData(writer);
    }
public class EffectBlock : IBlock
{
private Flags  _flags;	
private ShortBlockIndex _loopStartEvent = new ShortBlockIndex();
private ShortBlockIndex _loopStopEvent = new ShortBlockIndex();
private Pad  __unnamed;	
private Block _locations = new Block();
private Block _events = new Block();
public class EffectLocationsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public EffectLocationsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(EffectLocationsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new EffectLocationsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public EffectLocationsBlock this[int index]
  {
    get { return (InnerList[index] as EffectLocationsBlock); }
  }
}
private EffectLocationsBlockCollection _locationsCollection;
public EffectLocationsBlockCollection Locations
{
  get { return _locationsCollection; }
}
public class EffectEventBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public EffectEventBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(EffectEventBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new EffectEventBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public EffectEventBlock this[int index]
  {
    get { return (InnerList[index] as EffectEventBlock); }
  }
}
private EffectEventBlockCollection _eventsCollection;
public EffectEventBlockCollection Events
{
  get { return _eventsCollection; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public ShortBlockIndex LoopStartEvent
{
  get { return _loopStartEvent; }
  set { _loopStartEvent = value; }
}
public ShortBlockIndex LoopStopEvent
{
  get { return _loopStopEvent; }
  set { _loopStopEvent = value; }
}
public EffectBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(32);
_locationsCollection = new EffectLocationsBlockCollection(_locations);
_eventsCollection = new EffectEventBlockCollection(_events);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _loopStartEvent.Read(reader);
  _loopStopEvent.Read(reader);
  __unnamed.Read(reader);
  _locations.Read(reader);
  _events.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_locations.Count; x++)
{
  Locations.AddNew();
  Locations[x].Read(reader);
}
for (int x=0; x<_locations.Count; x++)
  Locations[x].ReadChildData(reader);
for (int x=0; x<_events.Count; x++)
{
  Events.AddNew();
  Events[x].Read(reader);
}
for (int x=0; x<_events.Count; x++)
  Events[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _loopStartEvent.Write(writer);
    _loopStopEvent.Write(writer);
    __unnamed.Write(writer);
    _locations.Write(writer);
    _events.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_locations.UpdateReflexiveOffset(writer);
for (int x=0; x<_locations.Count; x++)
{
  Locations[x].Write(writer);
}
for (int x=0; x<_locations.Count; x++)
  Locations[x].WriteChildData(writer);
_events.UpdateReflexiveOffset(writer);
for (int x=0; x<_events.Count; x++)
{
  Events[x].Write(writer);
}
for (int x=0; x<_events.Count; x++)
  Events[x].WriteChildData(writer);
}
}
public class EffectLocationsBlock : IBlock
{
private FixedLengthString _markerName = new FixedLengthString();
public FixedLengthString MarkerName
{
  get { return _markerName; }
  set { _markerName = value; }
}
public EffectLocationsBlock()
{

}
public void Read(BinaryReader reader)
{
  _markerName.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _markerName.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class EffectEventBlock : IBlock
{
private Pad  __unnamed;	
private RealFraction _skipFraction = new RealFraction();
private RealBounds _delayBounds = new RealBounds();
private RealBounds _durationBounds = new RealBounds();
private Pad  __unnamed2;	
private Block _parts = new Block();
private Block _particles = new Block();
public class EffectPartBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public EffectPartBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(EffectPartBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new EffectPartBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public EffectPartBlock this[int index]
  {
    get { return (InnerList[index] as EffectPartBlock); }
  }
}
private EffectPartBlockCollection _partsCollection;
public EffectPartBlockCollection Parts
{
  get { return _partsCollection; }
}
public class EffectParticlesBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public EffectParticlesBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(EffectParticlesBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new EffectParticlesBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public EffectParticlesBlock this[int index]
  {
    get { return (InnerList[index] as EffectParticlesBlock); }
  }
}
private EffectParticlesBlockCollection _particlesCollection;
public EffectParticlesBlockCollection Particles
{
  get { return _particlesCollection; }
}
public RealFraction SkipFraction
{
  get { return _skipFraction; }
  set { _skipFraction = value; }
}
public RealBounds DelayBounds
{
  get { return _delayBounds; }
  set { _delayBounds = value; }
}
public RealBounds DurationBounds
{
  get { return _durationBounds; }
  set { _durationBounds = value; }
}
public EffectEventBlock()
{
__unnamed = new Pad(4);
__unnamed2 = new Pad(20);
_partsCollection = new EffectPartBlockCollection(_parts);
_particlesCollection = new EffectParticlesBlockCollection(_particles);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  _skipFraction.Read(reader);
  _delayBounds.Read(reader);
  _durationBounds.Read(reader);
  __unnamed2.Read(reader);
  _parts.Read(reader);
  _particles.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_parts.Count; x++)
{
  Parts.AddNew();
  Parts[x].Read(reader);
}
for (int x=0; x<_parts.Count; x++)
  Parts[x].ReadChildData(reader);
for (int x=0; x<_particles.Count; x++)
{
  Particles.AddNew();
  Particles[x].Read(reader);
}
for (int x=0; x<_particles.Count; x++)
  Particles[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    _skipFraction.Write(writer);
    _delayBounds.Write(writer);
    _durationBounds.Write(writer);
    __unnamed2.Write(writer);
    _parts.Write(writer);
    _particles.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_parts.UpdateReflexiveOffset(writer);
for (int x=0; x<_parts.Count; x++)
{
  Parts[x].Write(writer);
}
for (int x=0; x<_parts.Count; x++)
  Parts[x].WriteChildData(writer);
_particles.UpdateReflexiveOffset(writer);
for (int x=0; x<_particles.Count; x++)
{
  Particles[x].Write(writer);
}
for (int x=0; x<_particles.Count; x++)
  Particles[x].WriteChildData(writer);
}
}
public class EffectPartBlock : IBlock
{
private Enum _createIn = new Enum();
private Enum _createIn2 = new Enum();
private ShortBlockIndex _location = new ShortBlockIndex();
private Flags  _flags;	
private Pad  __unnamed;	
private TagReference _type = new TagReference();
private Pad  __unnamed2;	
private RealBounds _velocityBounds = new RealBounds();
private Angle _velocityConeAngle = new Angle();
private AngleBounds _angularVelocityBounds = new AngleBounds();
private RealBounds _radiusModifierBounds = new RealBounds();
private Pad  __unnamed3;	
private Flags  _aScalesValues;	
private Flags  _bScalesValues;	
public Enum CreateIn
{
  get { return _createIn; }
  set { _createIn = value; }
}
public Enum CreateIn2
{
  get { return _createIn2; }
  set { _createIn2 = value; }
}
public ShortBlockIndex Location
{
  get { return _location; }
  set { _location = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public TagReference Type
{
  get { return _type; }
  set { _type = value; }
}
public RealBounds VelocityBounds
{
  get { return _velocityBounds; }
  set { _velocityBounds = value; }
}
public Angle VelocityConeAngle
{
  get { return _velocityConeAngle; }
  set { _velocityConeAngle = value; }
}
public AngleBounds AngularVelocityBounds
{
  get { return _angularVelocityBounds; }
  set { _angularVelocityBounds = value; }
}
public RealBounds RadiusModifierBounds
{
  get { return _radiusModifierBounds; }
  set { _radiusModifierBounds = value; }
}
public Flags AScalesValues
{
  get { return _aScalesValues; }
  set { _aScalesValues = value; }
}
public Flags BScalesValues
{
  get { return _bScalesValues; }
  set { _bScalesValues = value; }
}
public EffectPartBlock()
{
_flags = new Flags(2);
__unnamed = new Pad(16);
__unnamed2 = new Pad(24);
__unnamed3 = new Pad(4);
_aScalesValues = new Flags(4);
_bScalesValues = new Flags(4);

}
public void Read(BinaryReader reader)
{
  _createIn.Read(reader);
  _createIn2.Read(reader);
  _location.Read(reader);
  _flags.Read(reader);
  __unnamed.Read(reader);
  _type.Read(reader);
  __unnamed2.Read(reader);
  _velocityBounds.Read(reader);
  _velocityConeAngle.Read(reader);
  _angularVelocityBounds.Read(reader);
  _radiusModifierBounds.Read(reader);
  __unnamed3.Read(reader);
  _aScalesValues.Read(reader);
  _bScalesValues.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_type.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _createIn.Write(writer);
    _createIn2.Write(writer);
    _location.Write(writer);
    _flags.Write(writer);
    __unnamed.Write(writer);
    _type.Write(writer);
    __unnamed2.Write(writer);
    _velocityBounds.Write(writer);
    _velocityConeAngle.Write(writer);
    _angularVelocityBounds.Write(writer);
    _radiusModifierBounds.Write(writer);
    __unnamed3.Write(writer);
    _aScalesValues.Write(writer);
    _bScalesValues.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_type.WriteString(writer);
}
}
public class EffectParticlesBlock : IBlock
{
private Enum _createIn = new Enum();
private Enum _createIn2 = new Enum();
private Enum _create = new Enum();
private Pad  __unnamed;	
private ShortBlockIndex _location = new ShortBlockIndex();
private Pad  __unnamed2;	
private RealEulerAngles2D _relativeDirection = new RealEulerAngles2D();
private RealVector3D _relativeOffset = new RealVector3D();
private Pad  __unnamed3;	
private Pad  __unnamed4;	
private TagReference _particleType = new TagReference();
private Flags  _flags;	
private Enum _distributionFunction = new Enum();
private Pad  __unnamed5;	
private ShortBounds _count = new ShortBounds();
private RealBounds _distributionRadius = new RealBounds();
private Pad  __unnamed6;	
private RealBounds _velocity = new RealBounds();
private Angle _velocityConeAngle = new Angle();
private AngleBounds _angularVelocity = new AngleBounds();
private Pad  __unnamed7;	
private RealBounds _radius = new RealBounds();
private Pad  __unnamed8;	
private RealARGBColor _tintLowerBound = new RealARGBColor();
private RealARGBColor _tintUpperBound = new RealARGBColor();
private Pad  __unnamed9;	
private Flags  _aScalesValues;	
private Flags  _bScalesValues;	
public Enum CreateIn
{
  get { return _createIn; }
  set { _createIn = value; }
}
public Enum CreateIn2
{
  get { return _createIn2; }
  set { _createIn2 = value; }
}
public Enum Create
{
  get { return _create; }
  set { _create = value; }
}
public ShortBlockIndex Location
{
  get { return _location; }
  set { _location = value; }
}
public RealEulerAngles2D RelativeDirection
{
  get { return _relativeDirection; }
  set { _relativeDirection = value; }
}
public RealVector3D RelativeOffset
{
  get { return _relativeOffset; }
  set { _relativeOffset = value; }
}
public TagReference ParticleType
{
  get { return _particleType; }
  set { _particleType = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Enum DistributionFunction
{
  get { return _distributionFunction; }
  set { _distributionFunction = value; }
}
public ShortBounds Count
{
  get { return _count; }
  set { _count = value; }
}
public RealBounds DistributionRadius
{
  get { return _distributionRadius; }
  set { _distributionRadius = value; }
}
public RealBounds Velocity
{
  get { return _velocity; }
  set { _velocity = value; }
}
public Angle VelocityConeAngle
{
  get { return _velocityConeAngle; }
  set { _velocityConeAngle = value; }
}
public AngleBounds AngularVelocity
{
  get { return _angularVelocity; }
  set { _angularVelocity = value; }
}
public RealBounds Radius
{
  get { return _radius; }
  set { _radius = value; }
}
public RealARGBColor TintLowerBound
{
  get { return _tintLowerBound; }
  set { _tintLowerBound = value; }
}
public RealARGBColor TintUpperBound
{
  get { return _tintUpperBound; }
  set { _tintUpperBound = value; }
}
public Flags AScalesValues
{
  get { return _aScalesValues; }
  set { _aScalesValues = value; }
}
public Flags BScalesValues
{
  get { return _bScalesValues; }
  set { _bScalesValues = value; }
}
public EffectParticlesBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(2);
__unnamed3 = new Pad(12);
__unnamed4 = new Pad(40);
_flags = new Flags(4);
__unnamed5 = new Pad(2);
__unnamed6 = new Pad(12);
__unnamed7 = new Pad(8);
__unnamed8 = new Pad(8);
__unnamed9 = new Pad(16);
_aScalesValues = new Flags(4);
_bScalesValues = new Flags(4);

}
public void Read(BinaryReader reader)
{
  _createIn.Read(reader);
  _createIn2.Read(reader);
  _create.Read(reader);
  __unnamed.Read(reader);
  _location.Read(reader);
  __unnamed2.Read(reader);
  _relativeDirection.Read(reader);
  _relativeOffset.Read(reader);
  __unnamed3.Read(reader);
  __unnamed4.Read(reader);
  _particleType.Read(reader);
  _flags.Read(reader);
  _distributionFunction.Read(reader);
  __unnamed5.Read(reader);
  _count.Read(reader);
  _distributionRadius.Read(reader);
  __unnamed6.Read(reader);
  _velocity.Read(reader);
  _velocityConeAngle.Read(reader);
  _angularVelocity.Read(reader);
  __unnamed7.Read(reader);
  _radius.Read(reader);
  __unnamed8.Read(reader);
  _tintLowerBound.Read(reader);
  _tintUpperBound.Read(reader);
  __unnamed9.Read(reader);
  _aScalesValues.Read(reader);
  _bScalesValues.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_particleType.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _createIn.Write(writer);
    _createIn2.Write(writer);
    _create.Write(writer);
    __unnamed.Write(writer);
    _location.Write(writer);
    __unnamed2.Write(writer);
    _relativeDirection.Write(writer);
    _relativeOffset.Write(writer);
    __unnamed3.Write(writer);
    __unnamed4.Write(writer);
    _particleType.Write(writer);
    _flags.Write(writer);
    _distributionFunction.Write(writer);
    __unnamed5.Write(writer);
    _count.Write(writer);
    _distributionRadius.Write(writer);
    __unnamed6.Write(writer);
    _velocity.Write(writer);
    _velocityConeAngle.Write(writer);
    _angularVelocity.Write(writer);
    __unnamed7.Write(writer);
    _radius.Write(writer);
    __unnamed8.Write(writer);
    _tintLowerBound.Write(writer);
    _tintUpperBound.Write(writer);
    __unnamed9.Write(writer);
    _aScalesValues.Write(writer);
    _bScalesValues.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_particleType.WriteString(writer);
}
}
  }
}
