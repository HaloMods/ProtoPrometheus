using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Light : IBlock
  {
    public LightBlock LightValues = new LightBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'Light'------------------------------------------------------");
      LightValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      LightValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      LightValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      LightValues.WriteChildData(writer);
    }
public class LightBlock : IBlock
{
private Flags  _flags;	
private Real _radius = new Real();
private RealBounds _radiusModifer = new RealBounds();
private Angle _falloffAngle = new Angle();
private Angle _cutoffAngle = new Angle();
private Real _lensFlareOnlyRadius = new Real();
private Pad  __unnamed;	
private Flags  _interpolationFlags;	
private RealARGBColor _colorLowerBound = new RealARGBColor();
private RealARGBColor _colorUpperBound = new RealARGBColor();
private Pad  __unnamed2;	
private TagReference _primaryCubeMap = new TagReference();
private Pad  __unnamed3;	
private Enum _textureAnimationFunction = new Enum();
private Real _textureAnimationPeriod = new Real();
private TagReference _secondaryCubeMap = new TagReference();
private Pad  __unnamed4;	
private Enum _yawFunction = new Enum();
private Real _yawPeriod = new Real();
private Pad  __unnamed5;	
private Enum _rollFunction = new Enum();
private Real _rollPeriod = new Real();
private Pad  __unnamed6;	
private Enum _pitchFunction = new Enum();
private Real _pitchPeriod = new Real();
private Pad  __unnamed7;	
private TagReference _lensFlare = new TagReference();
private Pad  __unnamed8;	
private Real _intensity = new Real();
private RealRGBColor _color = new RealRGBColor();
private Pad  __unnamed9;	
private Real _duration = new Real();
private Pad  __unnamed10;	
private Enum _falloffFunction = new Enum();
private Pad  __unnamed11;	
private Pad  __unnamed12;	
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Real Radius
{
  get { return _radius; }
  set { _radius = value; }
}
public RealBounds RadiusModifer
{
  get { return _radiusModifer; }
  set { _radiusModifer = value; }
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
public Real LensFlareOnlyRadius
{
  get { return _lensFlareOnlyRadius; }
  set { _lensFlareOnlyRadius = value; }
}
public Flags InterpolationFlags
{
  get { return _interpolationFlags; }
  set { _interpolationFlags = value; }
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
public TagReference PrimaryCubeMap
{
  get { return _primaryCubeMap; }
  set { _primaryCubeMap = value; }
}
public Enum TextureAnimationFunction
{
  get { return _textureAnimationFunction; }
  set { _textureAnimationFunction = value; }
}
public Real TextureAnimationPeriod
{
  get { return _textureAnimationPeriod; }
  set { _textureAnimationPeriod = value; }
}
public TagReference SecondaryCubeMap
{
  get { return _secondaryCubeMap; }
  set { _secondaryCubeMap = value; }
}
public Enum YawFunction
{
  get { return _yawFunction; }
  set { _yawFunction = value; }
}
public Real YawPeriod
{
  get { return _yawPeriod; }
  set { _yawPeriod = value; }
}
public Enum RollFunction
{
  get { return _rollFunction; }
  set { _rollFunction = value; }
}
public Real RollPeriod
{
  get { return _rollPeriod; }
  set { _rollPeriod = value; }
}
public Enum PitchFunction
{
  get { return _pitchFunction; }
  set { _pitchFunction = value; }
}
public Real PitchPeriod
{
  get { return _pitchPeriod; }
  set { _pitchPeriod = value; }
}
public TagReference LensFlare
{
  get { return _lensFlare; }
  set { _lensFlare = value; }
}
public Real Intensity
{
  get { return _intensity; }
  set { _intensity = value; }
}
public RealRGBColor Color
{
  get { return _color; }
  set { _color = value; }
}
public Real Duration
{
  get { return _duration; }
  set { _duration = value; }
}
public Enum FalloffFunction
{
  get { return _falloffFunction; }
  set { _falloffFunction = value; }
}
public LightBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(24);
_interpolationFlags = new Flags(4);
__unnamed2 = new Pad(12);
__unnamed3 = new Pad(2);
__unnamed4 = new Pad(2);
__unnamed5 = new Pad(2);
__unnamed6 = new Pad(2);
__unnamed7 = new Pad(8);
__unnamed8 = new Pad(24);
__unnamed9 = new Pad(16);
__unnamed10 = new Pad(2);
__unnamed11 = new Pad(8);
__unnamed12 = new Pad(92);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _radius.Read(reader);
  _radiusModifer.Read(reader);
  _falloffAngle.Read(reader);
  _cutoffAngle.Read(reader);
  _lensFlareOnlyRadius.Read(reader);
  __unnamed.Read(reader);
  _interpolationFlags.Read(reader);
  _colorLowerBound.Read(reader);
  _colorUpperBound.Read(reader);
  __unnamed2.Read(reader);
  _primaryCubeMap.Read(reader);
  __unnamed3.Read(reader);
  _textureAnimationFunction.Read(reader);
  _textureAnimationPeriod.Read(reader);
  _secondaryCubeMap.Read(reader);
  __unnamed4.Read(reader);
  _yawFunction.Read(reader);
  _yawPeriod.Read(reader);
  __unnamed5.Read(reader);
  _rollFunction.Read(reader);
  _rollPeriod.Read(reader);
  __unnamed6.Read(reader);
  _pitchFunction.Read(reader);
  _pitchPeriod.Read(reader);
  __unnamed7.Read(reader);
  _lensFlare.Read(reader);
  __unnamed8.Read(reader);
  _intensity.Read(reader);
  _color.Read(reader);
  __unnamed9.Read(reader);
  _duration.Read(reader);
  __unnamed10.Read(reader);
  _falloffFunction.Read(reader);
  __unnamed11.Read(reader);
  __unnamed12.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_primaryCubeMap.ReadString(reader);
_secondaryCubeMap.ReadString(reader);
_lensFlare.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _radius.Write(writer);
    _radiusModifer.Write(writer);
    _falloffAngle.Write(writer);
    _cutoffAngle.Write(writer);
    _lensFlareOnlyRadius.Write(writer);
    __unnamed.Write(writer);
    _interpolationFlags.Write(writer);
    _colorLowerBound.Write(writer);
    _colorUpperBound.Write(writer);
    __unnamed2.Write(writer);
    _primaryCubeMap.Write(writer);
    __unnamed3.Write(writer);
    _textureAnimationFunction.Write(writer);
    _textureAnimationPeriod.Write(writer);
    _secondaryCubeMap.Write(writer);
    __unnamed4.Write(writer);
    _yawFunction.Write(writer);
    _yawPeriod.Write(writer);
    __unnamed5.Write(writer);
    _rollFunction.Write(writer);
    _rollPeriod.Write(writer);
    __unnamed6.Write(writer);
    _pitchFunction.Write(writer);
    _pitchPeriod.Write(writer);
    __unnamed7.Write(writer);
    _lensFlare.Write(writer);
    __unnamed8.Write(writer);
    _intensity.Write(writer);
    _color.Write(writer);
    __unnamed9.Write(writer);
    _duration.Write(writer);
    __unnamed10.Write(writer);
    _falloffFunction.Write(writer);
    __unnamed11.Write(writer);
    __unnamed12.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_primaryCubeMap.WriteString(writer);
_secondaryCubeMap.WriteString(writer);
_lensFlare.WriteString(writer);
}
}
  }
}
