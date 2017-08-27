using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Contrail : IBlock
  {
    public ContrailBlock ContrailValues = new ContrailBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'Contrail'------------------------------------------------------");
      ContrailValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      ContrailValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      ContrailValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      ContrailValues.WriteChildData(writer);
    }
public class ContrailBlock : IBlock
{
private Flags  _flags;	
private Flags  _scaleFlags;	
private Real _pointGenerationRate = new Real();
private RealBounds _pointVelocity = new RealBounds();
private Angle _pointVelocityConeAngle = new Angle();
private RealFraction _inheritedVelocityFraction = new RealFraction();
private Enum _renderType = new Enum();
private Pad  __unnamed;	
private Real _textureRepeatsU = new Real();
private Real _textureRepeatsV = new Real();
private Real _textureAnimationU = new Real();
private Real _textureAnimationV = new Real();
private Real _animationRate = new Real();
private TagReference _bitmap = new TagReference();
private ShortInteger _firstSequenceIndex = new ShortInteger();
private ShortInteger _sequenceCount = new ShortInteger();
private Pad  __unnamed2;	
private Pad  __unnamed3;	
private Flags  _shaderFlags;	
private Enum _framebufferBlendFunction = new Enum();
private Enum _framebufferFadeMode = new Enum();
private Flags  _mapFlags;	
private Pad  __unnamed4;	
private TagReference _bitmap2 = new TagReference();
private Enum _anchor = new Enum();
private Flags  _flags2;	
private Enum __unnamed5 = new Enum();
private Enum __unnamed6 = new Enum();
private Real __unnamed7 = new Real();
private Real __unnamed8 = new Real();
private Real __unnamed9 = new Real();
private Enum __unnamed10 = new Enum();
private Enum __unnamed11 = new Enum();
private Real __unnamed12 = new Real();
private Real __unnamed13 = new Real();
private Real __unnamed14 = new Real();
private Enum _rotatio = new Enum();
private Enum _rotatio2 = new Enum();
private Real _rotatio3 = new Real();
private Real _rotatio4 = new Real();
private Real _rotatio5 = new Real();
private RealPoint2D _rotatio6 = new RealPoint2D();
private Pad  __unnamed15;	
private Real _zspriteRadiusScale = new Real();
private Pad  __unnamed16;	
private Block _pointStates = new Block();
public class ContrailPointStatesBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ContrailPointStatesBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ContrailPointStatesBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ContrailPointStatesBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ContrailPointStatesBlock this[int index]
  {
    get { return (InnerList[index] as ContrailPointStatesBlock); }
  }
}
private ContrailPointStatesBlockCollection _pointStatesCollection;
public ContrailPointStatesBlockCollection PointStates
{
  get { return _pointStatesCollection; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Flags ScaleFlags
{
  get { return _scaleFlags; }
  set { _scaleFlags = value; }
}
public Real PointGenerationRate
{
  get { return _pointGenerationRate; }
  set { _pointGenerationRate = value; }
}
public RealBounds PointVelocity
{
  get { return _pointVelocity; }
  set { _pointVelocity = value; }
}
public Angle PointVelocityConeAngle
{
  get { return _pointVelocityConeAngle; }
  set { _pointVelocityConeAngle = value; }
}
public RealFraction InheritedVelocityFraction
{
  get { return _inheritedVelocityFraction; }
  set { _inheritedVelocityFraction = value; }
}
public Enum RenderType
{
  get { return _renderType; }
  set { _renderType = value; }
}
public Real TextureRepeatsU
{
  get { return _textureRepeatsU; }
  set { _textureRepeatsU = value; }
}
public Real TextureRepeatsV
{
  get { return _textureRepeatsV; }
  set { _textureRepeatsV = value; }
}
public Real TextureAnimationU
{
  get { return _textureAnimationU; }
  set { _textureAnimationU = value; }
}
public Real TextureAnimationV
{
  get { return _textureAnimationV; }
  set { _textureAnimationV = value; }
}
public Real AnimationRate
{
  get { return _animationRate; }
  set { _animationRate = value; }
}
public TagReference Bitmap
{
  get { return _bitmap; }
  set { _bitmap = value; }
}
public ShortInteger FirstSequenceIndex
{
  get { return _firstSequenceIndex; }
  set { _firstSequenceIndex = value; }
}
public ShortInteger SequenceCount
{
  get { return _sequenceCount; }
  set { _sequenceCount = value; }
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
public TagReference Bitmap2
{
  get { return _bitmap2; }
  set { _bitmap2 = value; }
}
public Enum Anchor
{
  get { return _anchor; }
  set { _anchor = value; }
}
public Flags Flags2
{
  get { return _flags2; }
  set { _flags2 = value; }
}
public Enum _unnamed5
{
  get { return __unnamed5; }
  set { __unnamed5 = value; }
}
public Enum _unnamed6
{
  get { return __unnamed6; }
  set { __unnamed6 = value; }
}
public Real _unnamed7
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
public Enum _unnamed10
{
  get { return __unnamed10; }
  set { __unnamed10 = value; }
}
public Enum _unnamed11
{
  get { return __unnamed11; }
  set { __unnamed11 = value; }
}
public Real _unnamed12
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
public ContrailBlock()
{
_flags = new Flags(2);
_scaleFlags = new Flags(2);
__unnamed = new Pad(2);
__unnamed2 = new Pad(64);
__unnamed3 = new Pad(40);
_shaderFlags = new Flags(2);
_mapFlags = new Flags(2);
__unnamed4 = new Pad(28);
_flags2 = new Flags(2);
__unnamed15 = new Pad(4);
__unnamed16 = new Pad(20);
_pointStatesCollection = new ContrailPointStatesBlockCollection(_pointStates);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _scaleFlags.Read(reader);
  _pointGenerationRate.Read(reader);
  _pointVelocity.Read(reader);
  _pointVelocityConeAngle.Read(reader);
  _inheritedVelocityFraction.Read(reader);
  _renderType.Read(reader);
  __unnamed.Read(reader);
  _textureRepeatsU.Read(reader);
  _textureRepeatsV.Read(reader);
  _textureAnimationU.Read(reader);
  _textureAnimationV.Read(reader);
  _animationRate.Read(reader);
  _bitmap.Read(reader);
  _firstSequenceIndex.Read(reader);
  _sequenceCount.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  _shaderFlags.Read(reader);
  _framebufferBlendFunction.Read(reader);
  _framebufferFadeMode.Read(reader);
  _mapFlags.Read(reader);
  __unnamed4.Read(reader);
  _bitmap2.Read(reader);
  _anchor.Read(reader);
  _flags2.Read(reader);
  __unnamed5.Read(reader);
  __unnamed6.Read(reader);
  __unnamed7.Read(reader);
  __unnamed8.Read(reader);
  __unnamed9.Read(reader);
  __unnamed10.Read(reader);
  __unnamed11.Read(reader);
  __unnamed12.Read(reader);
  __unnamed13.Read(reader);
  __unnamed14.Read(reader);
  _rotatio.Read(reader);
  _rotatio2.Read(reader);
  _rotatio3.Read(reader);
  _rotatio4.Read(reader);
  _rotatio5.Read(reader);
  _rotatio6.Read(reader);
  __unnamed15.Read(reader);
  _zspriteRadiusScale.Read(reader);
  __unnamed16.Read(reader);
  _pointStates.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_bitmap.ReadString(reader);
_bitmap2.ReadString(reader);
for (int x=0; x<_pointStates.Count; x++)
{
  PointStates.AddNew();
  PointStates[x].Read(reader);
}
for (int x=0; x<_pointStates.Count; x++)
  PointStates[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _scaleFlags.Write(writer);
    _pointGenerationRate.Write(writer);
    _pointVelocity.Write(writer);
    _pointVelocityConeAngle.Write(writer);
    _inheritedVelocityFraction.Write(writer);
    _renderType.Write(writer);
    __unnamed.Write(writer);
    _textureRepeatsU.Write(writer);
    _textureRepeatsV.Write(writer);
    _textureAnimationU.Write(writer);
    _textureAnimationV.Write(writer);
    _animationRate.Write(writer);
    _bitmap.Write(writer);
    _firstSequenceIndex.Write(writer);
    _sequenceCount.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    _shaderFlags.Write(writer);
    _framebufferBlendFunction.Write(writer);
    _framebufferFadeMode.Write(writer);
    _mapFlags.Write(writer);
    __unnamed4.Write(writer);
    _bitmap2.Write(writer);
    _anchor.Write(writer);
    _flags2.Write(writer);
    __unnamed5.Write(writer);
    __unnamed6.Write(writer);
    __unnamed7.Write(writer);
    __unnamed8.Write(writer);
    __unnamed9.Write(writer);
    __unnamed10.Write(writer);
    __unnamed11.Write(writer);
    __unnamed12.Write(writer);
    __unnamed13.Write(writer);
    __unnamed14.Write(writer);
    _rotatio.Write(writer);
    _rotatio2.Write(writer);
    _rotatio3.Write(writer);
    _rotatio4.Write(writer);
    _rotatio5.Write(writer);
    _rotatio6.Write(writer);
    __unnamed15.Write(writer);
    _zspriteRadiusScale.Write(writer);
    __unnamed16.Write(writer);
    _pointStates.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_bitmap.WriteString(writer);
_bitmap2.WriteString(writer);
_pointStates.UpdateReflexiveOffset(writer);
for (int x=0; x<_pointStates.Count; x++)
{
  PointStates[x].Write(writer);
}
for (int x=0; x<_pointStates.Count; x++)
  PointStates[x].WriteChildData(writer);
}
}
public class ContrailPointStatesBlock : IBlock
{
private RealBounds _duration = new RealBounds();
private RealBounds _transitionDuration = new RealBounds();
private TagReference _physics = new TagReference();
private Pad  __unnamed;	
private Real _width = new Real();
private RealARGBColor _colorLowerBound = new RealARGBColor();
private RealARGBColor _colorUpperBound = new RealARGBColor();
private Flags  _scaleFlags;	
public RealBounds Duration
{
  get { return _duration; }
  set { _duration = value; }
}
public RealBounds TransitionDuration
{
  get { return _transitionDuration; }
  set { _transitionDuration = value; }
}
public TagReference Physics
{
  get { return _physics; }
  set { _physics = value; }
}
public Real Width
{
  get { return _width; }
  set { _width = value; }
}
public RealARGBColor ColorLowerBound
{
  get { return _colorLowerBound; }
  set { _colorLowerBound = value; }
}
public RealARGBColor ColorUpperBound
{
  get { return _colorUpperBound; }
  set { _colorUpperBound = value; }
}
public Flags ScaleFlags
{
  get { return _scaleFlags; }
  set { _scaleFlags = value; }
}
public ContrailPointStatesBlock()
{
__unnamed = new Pad(32);
_scaleFlags = new Flags(4);

}
public void Read(BinaryReader reader)
{
  _duration.Read(reader);
  _transitionDuration.Read(reader);
  _physics.Read(reader);
  __unnamed.Read(reader);
  _width.Read(reader);
  _colorLowerBound.Read(reader);
  _colorUpperBound.Read(reader);
  _scaleFlags.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_physics.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _duration.Write(writer);
    _transitionDuration.Write(writer);
    _physics.Write(writer);
    __unnamed.Write(writer);
    _width.Write(writer);
    _colorLowerBound.Write(writer);
    _colorUpperBound.Write(writer);
    _scaleFlags.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_physics.WriteString(writer);
}
}
  }
}
