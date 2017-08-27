using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class ParticleSystem : IBlock
  {
    public ParticleSystemBlock ParticleSystemValues = new ParticleSystemBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'ParticleSystem'------------------------------------------------------");
      ParticleSystemValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      ParticleSystemValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      ParticleSystemValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      ParticleSystemValues.WriteChildData(writer);
    }
public class ParticleSystemBlock : IBlock
{
private Pad  __unnamed;	
private Pad  __unnamed2;	
private TagReference _pointPhysics = new TagReference();
private Enum _systemUpdatePhysics = new Enum();
private Pad  __unnamed3;	
private Flags  _physicsFlags;	
private Block _physicsConstants = new Block();
private Block _particleTypes = new Block();
public class ParticleSystemPhysicsConstantsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ParticleSystemPhysicsConstantsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ParticleSystemPhysicsConstantsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ParticleSystemPhysicsConstantsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ParticleSystemPhysicsConstantsBlock this[int index]
  {
    get { return (InnerList[index] as ParticleSystemPhysicsConstantsBlock); }
  }
}
private ParticleSystemPhysicsConstantsBlockCollection _physicsConstantsCollection;
public ParticleSystemPhysicsConstantsBlockCollection PhysicsConstants
{
  get { return _physicsConstantsCollection; }
}
public class ParticleSystemTypesBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ParticleSystemTypesBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ParticleSystemTypesBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ParticleSystemTypesBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ParticleSystemTypesBlock this[int index]
  {
    get { return (InnerList[index] as ParticleSystemTypesBlock); }
  }
}
private ParticleSystemTypesBlockCollection _particleTypesCollection;
public ParticleSystemTypesBlockCollection ParticleTypes
{
  get { return _particleTypesCollection; }
}
public TagReference PointPhysics
{
  get { return _pointPhysics; }
  set { _pointPhysics = value; }
}
public Enum SystemUpdatePhysics
{
  get { return _systemUpdatePhysics; }
  set { _systemUpdatePhysics = value; }
}
public Flags PhysicsFlags
{
  get { return _physicsFlags; }
  set { _physicsFlags = value; }
}
public ParticleSystemBlock()
{
__unnamed = new Pad(4);
__unnamed2 = new Pad(52);
__unnamed3 = new Pad(2);
_physicsFlags = new Flags(4);
_physicsConstantsCollection = new ParticleSystemPhysicsConstantsBlockCollection(_physicsConstants);
_particleTypesCollection = new ParticleSystemTypesBlockCollection(_particleTypes);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  _pointPhysics.Read(reader);
  _systemUpdatePhysics.Read(reader);
  __unnamed3.Read(reader);
  _physicsFlags.Read(reader);
  _physicsConstants.Read(reader);
  _particleTypes.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_pointPhysics.ReadString(reader);
for (int x=0; x<_physicsConstants.Count; x++)
{
  PhysicsConstants.AddNew();
  PhysicsConstants[x].Read(reader);
}
for (int x=0; x<_physicsConstants.Count; x++)
  PhysicsConstants[x].ReadChildData(reader);
for (int x=0; x<_particleTypes.Count; x++)
{
  ParticleTypes.AddNew();
  ParticleTypes[x].Read(reader);
}
for (int x=0; x<_particleTypes.Count; x++)
  ParticleTypes[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    _pointPhysics.Write(writer);
    _systemUpdatePhysics.Write(writer);
    __unnamed3.Write(writer);
    _physicsFlags.Write(writer);
    _physicsConstants.Write(writer);
    _particleTypes.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_pointPhysics.WriteString(writer);
_physicsConstants.UpdateReflexiveOffset(writer);
for (int x=0; x<_physicsConstants.Count; x++)
{
  PhysicsConstants[x].Write(writer);
}
for (int x=0; x<_physicsConstants.Count; x++)
  PhysicsConstants[x].WriteChildData(writer);
_particleTypes.UpdateReflexiveOffset(writer);
for (int x=0; x<_particleTypes.Count; x++)
{
  ParticleTypes[x].Write(writer);
}
for (int x=0; x<_particleTypes.Count; x++)
  ParticleTypes[x].WriteChildData(writer);
}
}
public class ParticleSystemPhysicsConstantsBlock : IBlock
{
private Real _k = new Real();
public Real K
{
  get { return _k; }
  set { _k = value; }
}
public ParticleSystemPhysicsConstantsBlock()
{

}
public void Read(BinaryReader reader)
{
  _k.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _k.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class ParticleSystemTypesBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private Flags  _flags;	
private ShortInteger _initialParticleCount = new ShortInteger();
private Pad  __unnamed;	
private Enum _complexSpriteRenderModes = new Enum();
private Pad  __unnamed2;	
private Real _radius = new Real();
private Pad  __unnamed3;	
private Enum _particleCreationPhysics = new Enum();
private Pad  __unnamed4;	
private Flags  _physicsFlags;	
private Block _physicsConstants = new Block();
private Block _states = new Block();
private Block _particleStates = new Block();
public class ParticleSystemPhysicsConstantsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ParticleSystemPhysicsConstantsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ParticleSystemPhysicsConstantsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ParticleSystemPhysicsConstantsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ParticleSystemPhysicsConstantsBlock this[int index]
  {
    get { return (InnerList[index] as ParticleSystemPhysicsConstantsBlock); }
  }
}
private ParticleSystemPhysicsConstantsBlockCollection _physicsConstantsCollection;
public ParticleSystemPhysicsConstantsBlockCollection PhysicsConstants
{
  get { return _physicsConstantsCollection; }
}
public class ParticleSystemTypeStatesBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ParticleSystemTypeStatesBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ParticleSystemTypeStatesBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ParticleSystemTypeStatesBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ParticleSystemTypeStatesBlock this[int index]
  {
    get { return (InnerList[index] as ParticleSystemTypeStatesBlock); }
  }
}
private ParticleSystemTypeStatesBlockCollection _statesCollection;
public ParticleSystemTypeStatesBlockCollection States
{
  get { return _statesCollection; }
}
public class ParticleSystemTypeParticleStatesBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ParticleSystemTypeParticleStatesBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ParticleSystemTypeParticleStatesBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ParticleSystemTypeParticleStatesBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ParticleSystemTypeParticleStatesBlock this[int index]
  {
    get { return (InnerList[index] as ParticleSystemTypeParticleStatesBlock); }
  }
}
private ParticleSystemTypeParticleStatesBlockCollection _particleStatesCollection;
public ParticleSystemTypeParticleStatesBlockCollection ParticleStates
{
  get { return _particleStatesCollection; }
}
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public ShortInteger InitialParticleCount
{
  get { return _initialParticleCount; }
  set { _initialParticleCount = value; }
}
public Enum ComplexSpriteRenderModes
{
  get { return _complexSpriteRenderModes; }
  set { _complexSpriteRenderModes = value; }
}
public Real Radius
{
  get { return _radius; }
  set { _radius = value; }
}
public Enum ParticleCreationPhysics
{
  get { return _particleCreationPhysics; }
  set { _particleCreationPhysics = value; }
}
public Flags PhysicsFlags
{
  get { return _physicsFlags; }
  set { _physicsFlags = value; }
}
public ParticleSystemTypesBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(2);
__unnamed2 = new Pad(2);
__unnamed3 = new Pad(36);
__unnamed4 = new Pad(2);
_physicsFlags = new Flags(4);
_physicsConstantsCollection = new ParticleSystemPhysicsConstantsBlockCollection(_physicsConstants);
_statesCollection = new ParticleSystemTypeStatesBlockCollection(_states);
_particleStatesCollection = new ParticleSystemTypeParticleStatesBlockCollection(_particleStates);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _flags.Read(reader);
  _initialParticleCount.Read(reader);
  __unnamed.Read(reader);
  _complexSpriteRenderModes.Read(reader);
  __unnamed2.Read(reader);
  _radius.Read(reader);
  __unnamed3.Read(reader);
  _particleCreationPhysics.Read(reader);
  __unnamed4.Read(reader);
  _physicsFlags.Read(reader);
  _physicsConstants.Read(reader);
  _states.Read(reader);
  _particleStates.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_physicsConstants.Count; x++)
{
  PhysicsConstants.AddNew();
  PhysicsConstants[x].Read(reader);
}
for (int x=0; x<_physicsConstants.Count; x++)
  PhysicsConstants[x].ReadChildData(reader);
for (int x=0; x<_states.Count; x++)
{
  States.AddNew();
  States[x].Read(reader);
}
for (int x=0; x<_states.Count; x++)
  States[x].ReadChildData(reader);
for (int x=0; x<_particleStates.Count; x++)
{
  ParticleStates.AddNew();
  ParticleStates[x].Read(reader);
}
for (int x=0; x<_particleStates.Count; x++)
  ParticleStates[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _flags.Write(writer);
    _initialParticleCount.Write(writer);
    __unnamed.Write(writer);
    _complexSpriteRenderModes.Write(writer);
    __unnamed2.Write(writer);
    _radius.Write(writer);
    __unnamed3.Write(writer);
    _particleCreationPhysics.Write(writer);
    __unnamed4.Write(writer);
    _physicsFlags.Write(writer);
    _physicsConstants.Write(writer);
    _states.Write(writer);
    _particleStates.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_physicsConstants.UpdateReflexiveOffset(writer);
for (int x=0; x<_physicsConstants.Count; x++)
{
  PhysicsConstants[x].Write(writer);
}
for (int x=0; x<_physicsConstants.Count; x++)
  PhysicsConstants[x].WriteChildData(writer);
_states.UpdateReflexiveOffset(writer);
for (int x=0; x<_states.Count; x++)
{
  States[x].Write(writer);
}
for (int x=0; x<_states.Count; x++)
  States[x].WriteChildData(writer);
_particleStates.UpdateReflexiveOffset(writer);
for (int x=0; x<_particleStates.Count; x++)
{
  ParticleStates[x].Write(writer);
}
for (int x=0; x<_particleStates.Count; x++)
  ParticleStates[x].WriteChildData(writer);
}
}
public class ParticleSystemTypeStatesBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private RealBounds _durationBounds = new RealBounds();
private RealBounds _transitionTimeBounds = new RealBounds();
private Pad  __unnamed;	
private Real _scaleMultiplier = new Real();
private Real _animation_rate_multiplier = new Real();
private Real _rotationRateMultiplier = new Real();
private RealARGBColor _colorMultiplier = new RealARGBColor();
private Real _radiusMultiplier = new Real();
private Real _minimumParticleCount = new Real();
private Real _particleCreationRate = new Real();
private Pad  __unnamed2;	
private Enum _particleCreationPhysics = new Enum();
private Enum _particleUpdatePhysics = new Enum();
private Block _physicsConstants = new Block();
public class ParticleSystemPhysicsConstantsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ParticleSystemPhysicsConstantsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ParticleSystemPhysicsConstantsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ParticleSystemPhysicsConstantsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ParticleSystemPhysicsConstantsBlock this[int index]
  {
    get { return (InnerList[index] as ParticleSystemPhysicsConstantsBlock); }
  }
}
private ParticleSystemPhysicsConstantsBlockCollection _physicsConstantsCollection;
public ParticleSystemPhysicsConstantsBlockCollection PhysicsConstants
{
  get { return _physicsConstantsCollection; }
}
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public RealBounds DurationBounds
{
  get { return _durationBounds; }
  set { _durationBounds = value; }
}
public RealBounds TransitionTimeBounds
{
  get { return _transitionTimeBounds; }
  set { _transitionTimeBounds = value; }
}
public Real ScaleMultiplier
{
  get { return _scaleMultiplier; }
  set { _scaleMultiplier = value; }
}
public Real Animation_rate_multiplier
{
  get { return _animation_rate_multiplier; }
  set { _animation_rate_multiplier = value; }
}
public Real RotationRateMultiplier
{
  get { return _rotationRateMultiplier; }
  set { _rotationRateMultiplier = value; }
}
public RealARGBColor ColorMultiplier
{
  get { return _colorMultiplier; }
  set { _colorMultiplier = value; }
}
public Real RadiusMultiplier
{
  get { return _radiusMultiplier; }
  set { _radiusMultiplier = value; }
}
public Real MinimumParticleCount
{
  get { return _minimumParticleCount; }
  set { _minimumParticleCount = value; }
}
public Real ParticleCreationRate
{
  get { return _particleCreationRate; }
  set { _particleCreationRate = value; }
}
public Enum ParticleCreationPhysics
{
  get { return _particleCreationPhysics; }
  set { _particleCreationPhysics = value; }
}
public Enum ParticleUpdatePhysics
{
  get { return _particleUpdatePhysics; }
  set { _particleUpdatePhysics = value; }
}
public ParticleSystemTypeStatesBlock()
{
__unnamed = new Pad(4);
__unnamed2 = new Pad(84);
_physicsConstantsCollection = new ParticleSystemPhysicsConstantsBlockCollection(_physicsConstants);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _durationBounds.Read(reader);
  _transitionTimeBounds.Read(reader);
  __unnamed.Read(reader);
  _scaleMultiplier.Read(reader);
  _animation_rate_multiplier.Read(reader);
  _rotationRateMultiplier.Read(reader);
  _colorMultiplier.Read(reader);
  _radiusMultiplier.Read(reader);
  _minimumParticleCount.Read(reader);
  _particleCreationRate.Read(reader);
  __unnamed2.Read(reader);
  _particleCreationPhysics.Read(reader);
  _particleUpdatePhysics.Read(reader);
  _physicsConstants.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_physicsConstants.Count; x++)
{
  PhysicsConstants.AddNew();
  PhysicsConstants[x].Read(reader);
}
for (int x=0; x<_physicsConstants.Count; x++)
  PhysicsConstants[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _durationBounds.Write(writer);
    _transitionTimeBounds.Write(writer);
    __unnamed.Write(writer);
    _scaleMultiplier.Write(writer);
    _animation_rate_multiplier.Write(writer);
    _rotationRateMultiplier.Write(writer);
    _colorMultiplier.Write(writer);
    _radiusMultiplier.Write(writer);
    _minimumParticleCount.Write(writer);
    _particleCreationRate.Write(writer);
    __unnamed2.Write(writer);
    _particleCreationPhysics.Write(writer);
    _particleUpdatePhysics.Write(writer);
    _physicsConstants.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_physicsConstants.UpdateReflexiveOffset(writer);
for (int x=0; x<_physicsConstants.Count; x++)
{
  PhysicsConstants[x].Write(writer);
}
for (int x=0; x<_physicsConstants.Count; x++)
  PhysicsConstants[x].WriteChildData(writer);
}
}
public class ParticleSystemTypeParticleStatesBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private RealBounds _durationBounds = new RealBounds();
private RealBounds _transitionTimeBounds = new RealBounds();
private TagReference _bitmaps = new TagReference();
private ShortInteger _sequenceIndex = new ShortInteger();
private Pad  __unnamed;	
private Pad  __unnamed2;	
private RealBounds _scale = new RealBounds();
private RealBounds _animationRate = new RealBounds();
private AngleBounds _rotationRate = new AngleBounds();
private RealARGBColor _color1 = new RealARGBColor();
private RealARGBColor _color2 = new RealARGBColor();
private Real _radiusMultiplier = new Real();
private TagReference _pointPhysics = new TagReference();
private Pad  __unnamed3;	
private Pad  __unnamed4;	
private Flags  _shaderFlags;	
private Enum _framebufferBlendFunction = new Enum();
private Enum _framebufferFadeMode = new Enum();
private Flags  _mapFlags;	
private Pad  __unnamed5;	
private TagReference _bitmap = new TagReference();
private Enum _anchor = new Enum();
private Flags  _flags;	
private Enum __unnamed6 = new Enum();
private Enum __unnamed7 = new Enum();
private Real __unnamed8 = new Real();
private Real __unnamed9 = new Real();
private Real __unnamed10 = new Real();
private Enum __unnamed11 = new Enum();
private Enum __unnamed12 = new Enum();
private Real __unnamed13 = new Real();
private Real __unnamed14 = new Real();
private Real __unnamed15 = new Real();
private Enum _rotatio = new Enum();
private Enum _rotatio2 = new Enum();
private Real _rotatio3 = new Real();
private Real _rotatio4 = new Real();
private Real _rotatio5 = new Real();
private RealPoint2D _rotatio6 = new RealPoint2D();
private Pad  __unnamed16;	
private Real _zspriteRadiusScale = new Real();
private Pad  __unnamed17;	
private Block _physicsConstants = new Block();
public class ParticleSystemPhysicsConstantsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ParticleSystemPhysicsConstantsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ParticleSystemPhysicsConstantsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ParticleSystemPhysicsConstantsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ParticleSystemPhysicsConstantsBlock this[int index]
  {
    get { return (InnerList[index] as ParticleSystemPhysicsConstantsBlock); }
  }
}
private ParticleSystemPhysicsConstantsBlockCollection _physicsConstantsCollection;
public ParticleSystemPhysicsConstantsBlockCollection PhysicsConstants
{
  get { return _physicsConstantsCollection; }
}
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public RealBounds DurationBounds
{
  get { return _durationBounds; }
  set { _durationBounds = value; }
}
public RealBounds TransitionTimeBounds
{
  get { return _transitionTimeBounds; }
  set { _transitionTimeBounds = value; }
}
public TagReference Bitmaps
{
  get { return _bitmaps; }
  set { _bitmaps = value; }
}
public ShortInteger SequenceIndex
{
  get { return _sequenceIndex; }
  set { _sequenceIndex = value; }
}
public RealBounds Scale
{
  get { return _scale; }
  set { _scale = value; }
}
public RealBounds AnimationRate
{
  get { return _animationRate; }
  set { _animationRate = value; }
}
public AngleBounds RotationRate
{
  get { return _rotationRate; }
  set { _rotationRate = value; }
}
public RealARGBColor Color1
{
  get { return _color1; }
  set { _color1 = value; }
}
public RealARGBColor Color2
{
  get { return _color2; }
  set { _color2 = value; }
}
public Real RadiusMultiplier
{
  get { return _radiusMultiplier; }
  set { _radiusMultiplier = value; }
}
public TagReference PointPhysics
{
  get { return _pointPhysics; }
  set { _pointPhysics = value; }
}
public Flags ShaderFlags
{
  get { return _shaderFlags; }
  set { _shaderFlags = value; }
}
public Enum FramebufferBlendFunction
{
  get { return _framebufferBlendFunction; }
  set { _framebufferBlendFunction = value; }
}
public Enum FramebufferFadeMode
{
  get { return _framebufferFadeMode; }
  set { _framebufferFadeMode = value; }
}
public Flags MapFlags
{
  get { return _mapFlags; }
  set { _mapFlags = value; }
}
public TagReference Bitmap
{
  get { return _bitmap; }
  set { _bitmap = value; }
}
public Enum Anchor
{
  get { return _anchor; }
  set { _anchor = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Enum _unnamed6
{
  get { return __unnamed6; }
  set { __unnamed6 = value; }
}
public Enum _unnamed7
{
  get { return __unnamed7; }
  set { __unnamed7 = value; }
}
public Real _unnamed8
{
  get { return __unnamed8; }
  set { __unnamed8 = value; }
}
public Real _unnamed9
{
  get { return __unnamed9; }
  set { __unnamed9 = value; }
}
public Real _unnamed10
{
  get { return __unnamed10; }
  set { __unnamed10 = value; }
}
public Enum _unnamed11
{
  get { return __unnamed11; }
  set { __unnamed11 = value; }
}
public Enum _unnamed12
{
  get { return __unnamed12; }
  set { __unnamed12 = value; }
}
public Real _unnamed13
{
  get { return __unnamed13; }
  set { __unnamed13 = value; }
}
public Real _unnamed14
{
  get { return __unnamed14; }
  set { __unnamed14 = value; }
}
public Real _unnamed15
{
  get { return __unnamed15; }
  set { __unnamed15 = value; }
}
public Enum Rotatio
{
  get { return _rotatio; }
  set { _rotatio = value; }
}
public Enum Rotatio2
{
  get { return _rotatio2; }
  set { _rotatio2 = value; }
}
public Real Rotatio3
{
  get { return _rotatio3; }
  set { _rotatio3 = value; }
}
public Real Rotatio4
{
  get { return _rotatio4; }
  set { _rotatio4 = value; }
}
public Real Rotatio5
{
  get { return _rotatio5; }
  set { _rotatio5 = value; }
}
public RealPoint2D Rotatio6
{
  get { return _rotatio6; }
  set { _rotatio6 = value; }
}
public Real ZspriteRadiusScale
{
  get { return _zspriteRadiusScale; }
  set { _zspriteRadiusScale = value; }
}
public ParticleSystemTypeParticleStatesBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(4);
__unnamed3 = new Pad(36);
__unnamed4 = new Pad(40);
_shaderFlags = new Flags(2);
_mapFlags = new Flags(2);
__unnamed5 = new Pad(28);
_flags = new Flags(2);
__unnamed16 = new Pad(4);
__unnamed17 = new Pad(20);
_physicsConstantsCollection = new ParticleSystemPhysicsConstantsBlockCollection(_physicsConstants);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _durationBounds.Read(reader);
  _transitionTimeBounds.Read(reader);
  _bitmaps.Read(reader);
  _sequenceIndex.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  _scale.Read(reader);
  _animationRate.Read(reader);
  _rotationRate.Read(reader);
  _color1.Read(reader);
  _color2.Read(reader);
  _radiusMultiplier.Read(reader);
  _pointPhysics.Read(reader);
  __unnamed3.Read(reader);
  __unnamed4.Read(reader);
  _shaderFlags.Read(reader);
  _framebufferBlendFunction.Read(reader);
  _framebufferFadeMode.Read(reader);
  _mapFlags.Read(reader);
  __unnamed5.Read(reader);
  _bitmap.Read(reader);
  _anchor.Read(reader);
  _flags.Read(reader);
  __unnamed6.Read(reader);
  __unnamed7.Read(reader);
  __unnamed8.Read(reader);
  __unnamed9.Read(reader);
  __unnamed10.Read(reader);
  __unnamed11.Read(reader);
  __unnamed12.Read(reader);
  __unnamed13.Read(reader);
  __unnamed14.Read(reader);
  __unnamed15.Read(reader);
  _rotatio.Read(reader);
  _rotatio2.Read(reader);
  _rotatio3.Read(reader);
  _rotatio4.Read(reader);
  _rotatio5.Read(reader);
  _rotatio6.Read(reader);
  __unnamed16.Read(reader);
  _zspriteRadiusScale.Read(reader);
  __unnamed17.Read(reader);
  _physicsConstants.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_bitmaps.ReadString(reader);
_pointPhysics.ReadString(reader);
_bitmap.ReadString(reader);
for (int x=0; x<_physicsConstants.Count; x++)
{
  PhysicsConstants.AddNew();
  PhysicsConstants[x].Read(reader);
}
for (int x=0; x<_physicsConstants.Count; x++)
  PhysicsConstants[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _durationBounds.Write(writer);
    _transitionTimeBounds.Write(writer);
    _bitmaps.Write(writer);
    _sequenceIndex.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    _scale.Write(writer);
    _animationRate.Write(writer);
    _rotationRate.Write(writer);
    _color1.Write(writer);
    _color2.Write(writer);
    _radiusMultiplier.Write(writer);
    _pointPhysics.Write(writer);
    __unnamed3.Write(writer);
    __unnamed4.Write(writer);
    _shaderFlags.Write(writer);
    _framebufferBlendFunction.Write(writer);
    _framebufferFadeMode.Write(writer);
    _mapFlags.Write(writer);
    __unnamed5.Write(writer);
    _bitmap.Write(writer);
    _anchor.Write(writer);
    _flags.Write(writer);
    __unnamed6.Write(writer);
    __unnamed7.Write(writer);
    __unnamed8.Write(writer);
    __unnamed9.Write(writer);
    __unnamed10.Write(writer);
    __unnamed11.Write(writer);
    __unnamed12.Write(writer);
    __unnamed13.Write(writer);
    __unnamed14.Write(writer);
    __unnamed15.Write(writer);
    _rotatio.Write(writer);
    _rotatio2.Write(writer);
    _rotatio3.Write(writer);
    _rotatio4.Write(writer);
    _rotatio5.Write(writer);
    _rotatio6.Write(writer);
    __unnamed16.Write(writer);
    _zspriteRadiusScale.Write(writer);
    __unnamed17.Write(writer);
    _physicsConstants.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_bitmaps.WriteString(writer);
_pointPhysics.WriteString(writer);
_bitmap.WriteString(writer);
_physicsConstants.UpdateReflexiveOffset(writer);
for (int x=0; x<_physicsConstants.Count; x++)
{
  PhysicsConstants[x].Write(writer);
}
for (int x=0; x<_physicsConstants.Count; x++)
  PhysicsConstants[x].WriteChildData(writer);
}
}
  }
}
