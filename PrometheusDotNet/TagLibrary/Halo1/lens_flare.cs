using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class LensFlare : IBlock
  {
    public LensFlareBlock LensFlareValues = new LensFlareBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'LensFlare'------------------------------------------------------");
      LensFlareValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      LensFlareValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      LensFlareValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      LensFlareValues.WriteChildData(writer);
    }
public class LensFlareBlock : IBlock
{
private Angle _falloffAngle = new Angle();
private Angle _cutoffAngle = new Angle();
private Pad  __unnamed;	
private Real _occlusionRadius = new Real();
private Enum _occlusionOffsetDirection = new Enum();
private Pad  __unnamed2;	
private Real _nearFadeDistance = new Real();
private Real _farFadeDistance = new Real();
private TagReference _bitmap = new TagReference();
private Flags  _flags;	
private Pad  __unnamed3;	
private Pad  __unnamed4;	
private Enum _rotationFunction = new Enum();
private Pad  __unnamed5;	
private Angle _rotationFunctionScale = new Angle();
private Pad  __unnamed6;	
private Real _horizontalScale = new Real();
private Real _verticalScale = new Real();
private Pad  __unnamed7;	
private Block _reflections = new Block();
private Pad  __unnamed8;	
public class LensFlareReflectionBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public LensFlareReflectionBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(LensFlareReflectionBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new LensFlareReflectionBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public LensFlareReflectionBlock this[int index]
  {
    get { return (InnerList[index] as LensFlareReflectionBlock); }
  }
}
private LensFlareReflectionBlockCollection _reflectionsCollection;
public LensFlareReflectionBlockCollection Reflections
{
  get { return _reflectionsCollection; }
}
public Angle FalloffAngle
{
  get { return _falloffAngle; }
  set { _falloffAngle = value; }
}
public Angle CutoffAngle
{
  get { return _cutoffAngle; }
  set { _cutoffAngle = value; }
}
public Real OcclusionRadius
{
  get { return _occlusionRadius; }
  set { _occlusionRadius = value; }
}
public Enum OcclusionOffsetDirection
{
  get { return _occlusionOffsetDirection; }
  set { _occlusionOffsetDirection = value; }
}
public Real NearFadeDistance
{
  get { return _nearFadeDistance; }
  set { _nearFadeDistance = value; }
}
public Real FarFadeDistance
{
  get { return _farFadeDistance; }
  set { _farFadeDistance = value; }
}
public TagReference Bitmap
{
  get { return _bitmap; }
  set { _bitmap = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Enum RotationFunction
{
  get { return _rotationFunction; }
  set { _rotationFunction = value; }
}
public Angle RotationFunctionScale
{
  get { return _rotationFunctionScale; }
  set { _rotationFunctionScale = value; }
}
public Real HorizontalScale
{
  get { return _horizontalScale; }
  set { _horizontalScale = value; }
}
public Real VerticalScale
{
  get { return _verticalScale; }
  set { _verticalScale = value; }
}
public LensFlareBlock()
{
__unnamed = new Pad(8);
__unnamed2 = new Pad(2);
_flags = new Flags(2);
__unnamed3 = new Pad(2);
__unnamed4 = new Pad(76);
__unnamed5 = new Pad(2);
__unnamed6 = new Pad(24);
__unnamed7 = new Pad(28);
__unnamed8 = new Pad(32);
_reflectionsCollection = new LensFlareReflectionBlockCollection(_reflections);

}
public void Read(BinaryReader reader)
{
  _falloffAngle.Read(reader);
  _cutoffAngle.Read(reader);
  __unnamed.Read(reader);
  _occlusionRadius.Read(reader);
  _occlusionOffsetDirection.Read(reader);
  __unnamed2.Read(reader);
  _nearFadeDistance.Read(reader);
  _farFadeDistance.Read(reader);
  _bitmap.Read(reader);
  _flags.Read(reader);
  __unnamed3.Read(reader);
  __unnamed4.Read(reader);
  _rotationFunction.Read(reader);
  __unnamed5.Read(reader);
  _rotationFunctionScale.Read(reader);
  __unnamed6.Read(reader);
  _horizontalScale.Read(reader);
  _verticalScale.Read(reader);
  __unnamed7.Read(reader);
  _reflections.Read(reader);
  __unnamed8.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_bitmap.ReadString(reader);
for (int x=0; x<_reflections.Count; x++)
{
  Reflections.AddNew();
  Reflections[x].Read(reader);
}
for (int x=0; x<_reflections.Count; x++)
  Reflections[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _falloffAngle.Write(writer);
    _cutoffAngle.Write(writer);
    __unnamed.Write(writer);
    _occlusionRadius.Write(writer);
    _occlusionOffsetDirection.Write(writer);
    __unnamed2.Write(writer);
    _nearFadeDistance.Write(writer);
    _farFadeDistance.Write(writer);
    _bitmap.Write(writer);
    _flags.Write(writer);
    __unnamed3.Write(writer);
    __unnamed4.Write(writer);
    _rotationFunction.Write(writer);
    __unnamed5.Write(writer);
    _rotationFunctionScale.Write(writer);
    __unnamed6.Write(writer);
    _horizontalScale.Write(writer);
    _verticalScale.Write(writer);
    __unnamed7.Write(writer);
    _reflections.Write(writer);
    __unnamed8.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_bitmap.WriteString(writer);
_reflections.UpdateReflexiveOffset(writer);
for (int x=0; x<_reflections.Count; x++)
{
  Reflections[x].Write(writer);
}
for (int x=0; x<_reflections.Count; x++)
  Reflections[x].WriteChildData(writer);
}
}
public class LensFlareReflectionBlock : IBlock
{
private Flags  _flags;	
private Pad  __unnamed;	
private ShortInteger _bitmapIndex = new ShortInteger();
private Pad  __unnamed2;	
private Pad  __unnamed3;	
private Real _position = new Real();
private Real _rotationOffset = new Real();
private Pad  __unnamed4;	
private RealBounds _radius = new RealBounds();
private Enum _radiusScaledBy = new Enum();
private Pad  __unnamed5;	
private RealFractionBounds _brightness = new RealFractionBounds();
private Enum _brightnessScaledBy = new Enum();
private Pad  __unnamed6;	
private RealARGBColor _tintColor = new RealARGBColor();
private RealARGBColor _colorLowerBound = new RealARGBColor();
private RealARGBColor _colorUpperBound = new RealARGBColor();
private Flags  _flags2;	
private Enum _animationFunction = new Enum();
private Real _animationPeriod = new Real();
private Real _animationPhase = new Real();
private Pad  __unnamed7;	
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public ShortInteger BitmapIndex
{
  get { return _bitmapIndex; }
  set { _bitmapIndex = value; }
}
public Real Position
{
  get { return _position; }
  set { _position = value; }
}
public Real RotationOffset
{
  get { return _rotationOffset; }
  set { _rotationOffset = value; }
}
public RealBounds Radius
{
  get { return _radius; }
  set { _radius = value; }
}
public Enum RadiusScaledBy
{
  get { return _radiusScaledBy; }
  set { _radiusScaledBy = value; }
}
public RealFractionBounds Brightness
{
  get { return _brightness; }
  set { _brightness = value; }
}
public Enum BrightnessScaledBy
{
  get { return _brightnessScaledBy; }
  set { _brightnessScaledBy = value; }
}
public RealARGBColor TintColor
{
  get { return _tintColor; }
  set { _tintColor = value; }
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
public Flags Flags2
{
  get { return _flags2; }
  set { _flags2 = value; }
}
public Enum AnimationFunction
{
  get { return _animationFunction; }
  set { _animationFunction = value; }
}
public Real AnimationPeriod
{
  get { return _animationPeriod; }
  set { _animationPeriod = value; }
}
public Real AnimationPhase
{
  get { return _animationPhase; }
  set { _animationPhase = value; }
}
public LensFlareReflectionBlock()
{
_flags = new Flags(2);
__unnamed = new Pad(2);
__unnamed2 = new Pad(2);
__unnamed3 = new Pad(20);
__unnamed4 = new Pad(4);
__unnamed5 = new Pad(2);
__unnamed6 = new Pad(2);
_flags2 = new Flags(2);
__unnamed7 = new Pad(4);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  __unnamed.Read(reader);
  _bitmapIndex.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  _position.Read(reader);
  _rotationOffset.Read(reader);
  __unnamed4.Read(reader);
  _radius.Read(reader);
  _radiusScaledBy.Read(reader);
  __unnamed5.Read(reader);
  _brightness.Read(reader);
  _brightnessScaledBy.Read(reader);
  __unnamed6.Read(reader);
  _tintColor.Read(reader);
  _colorLowerBound.Read(reader);
  _colorUpperBound.Read(reader);
  _flags2.Read(reader);
  _animationFunction.Read(reader);
  _animationPeriod.Read(reader);
  _animationPhase.Read(reader);
  __unnamed7.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    __unnamed.Write(writer);
    _bitmapIndex.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    _position.Write(writer);
    _rotationOffset.Write(writer);
    __unnamed4.Write(writer);
    _radius.Write(writer);
    _radiusScaledBy.Write(writer);
    __unnamed5.Write(writer);
    _brightness.Write(writer);
    _brightnessScaledBy.Write(writer);
    __unnamed6.Write(writer);
    _tintColor.Write(writer);
    _colorLowerBound.Write(writer);
    _colorUpperBound.Write(writer);
    _flags2.Write(writer);
    _animationFunction.Write(writer);
    _animationPeriod.Write(writer);
    _animationPhase.Write(writer);
    __unnamed7.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
  }
}
