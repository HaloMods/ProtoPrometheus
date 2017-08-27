using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Fog : IBlock
  {
    public FogBlock FogValues = new FogBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'Fog'------------------------------------------------------");
      FogValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      FogValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      FogValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      FogValues.WriteChildData(writer);
    }
public class FogBlock : IBlock
{
private Flags  _flags;	
private Pad  __unnamed;	
private Pad  __unnamed2;	
private Pad  __unnamed3;	
private RealFraction _maximumDensity = new RealFraction();
private Pad  __unnamed4;	
private Real _opaqueDistance = new Real();
private Pad  __unnamed5;	
private Real _opaqueDepth = new Real();
private Pad  __unnamed6;	
private Real _distanceToWaterPlane = new Real();
private RealRGBColor _color = new RealRGBColor();
private Flags  _flags2;	
private ShortInteger _layerCount = new ShortInteger();
private RealBounds _distanceGradient = new RealBounds();
private RealFractionBounds _densityGradient = new RealFractionBounds();
private Real _startDistanceFromFogPlane = new Real();
private Pad  __unnamed7;	
private RGBColor _color2 = new RGBColor();
private RealFraction _rotationMultiplier = new RealFraction();
private RealFraction _strafingMultiplier = new RealFraction();
private RealFraction _zoomMultiplier = new RealFraction();
private Pad  __unnamed8;	
private Real _mapScale = new Real();
private TagReference _map = new TagReference();
private Real _animationPeriod = new Real();
private Pad  __unnamed9;	
private RealBounds _windVelocity = new RealBounds();
private RealBounds _windPeriod = new RealBounds();
private RealFraction _windAccelerationWeight = new RealFraction();
private RealFraction _windPerpendicularWeight = new RealFraction();
private Pad  __unnamed10;	
private TagReference _backgroundSound = new TagReference();
private TagReference _soundEnvironment = new TagReference();
private Pad  __unnamed11;	
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public RealFraction MaximumDensity
{
  get { return _maximumDensity; }
  set { _maximumDensity = value; }
}
public Real OpaqueDistance
{
  get { return _opaqueDistance; }
  set { _opaqueDistance = value; }
}
public Real OpaqueDepth
{
  get { return _opaqueDepth; }
  set { _opaqueDepth = value; }
}
public Real DistanceToWaterPlane
{
  get { return _distanceToWaterPlane; }
  set { _distanceToWaterPlane = value; }
}
public RealRGBColor Color
{
  get { return _color; }
  set { _color = value; }
}
public Flags Flags2
{
  get { return _flags2; }
  set { _flags2 = value; }
}
public ShortInteger LayerCount
{
  get { return _layerCount; }
  set { _layerCount = value; }
}
public RealBounds DistanceGradient
{
  get { return _distanceGradient; }
  set { _distanceGradient = value; }
}
public RealFractionBounds DensityGradient
{
  get { return _densityGradient; }
  set { _densityGradient = value; }
}
public Real StartDistanceFromFogPlane
{
  get { return _startDistanceFromFogPlane; }
  set { _startDistanceFromFogPlane = value; }
}
public RGBColor Color2
{
  get { return _color2; }
  set { _color2 = value; }
}
public RealFraction RotationMultiplier
{
  get { return _rotationMultiplier; }
  set { _rotationMultiplier = value; }
}
public RealFraction StrafingMultiplier
{
  get { return _strafingMultiplier; }
  set { _strafingMultiplier = value; }
}
public RealFraction ZoomMultiplier
{
  get { return _zoomMultiplier; }
  set { _zoomMultiplier = value; }
}
public Real MapScale
{
  get { return _mapScale; }
  set { _mapScale = value; }
}
public TagReference Map
{
  get { return _map; }
  set { _map = value; }
}
public Real AnimationPeriod
{
  get { return _animationPeriod; }
  set { _animationPeriod = value; }
}
public RealBounds WindVelocity
{
  get { return _windVelocity; }
  set { _windVelocity = value; }
}
public RealBounds WindPeriod
{
  get { return _windPeriod; }
  set { _windPeriod = value; }
}
public RealFraction WindAccelerationWeight
{
  get { return _windAccelerationWeight; }
  set { _windAccelerationWeight = value; }
}
public RealFraction WindPerpendicularWeight
{
  get { return _windPerpendicularWeight; }
  set { _windPerpendicularWeight = value; }
}
public TagReference BackgroundSound
{
  get { return _backgroundSound; }
  set { _backgroundSound = value; }
}
public TagReference SoundEnvironment
{
  get { return _soundEnvironment; }
  set { _soundEnvironment = value; }
}
public FogBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(4);
__unnamed2 = new Pad(76);
__unnamed3 = new Pad(4);
__unnamed4 = new Pad(4);
__unnamed5 = new Pad(4);
__unnamed6 = new Pad(8);
_flags2 = new Flags(2);
__unnamed7 = new Pad(4);
__unnamed8 = new Pad(8);
__unnamed9 = new Pad(4);
__unnamed10 = new Pad(8);
__unnamed11 = new Pad(120);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  _maximumDensity.Read(reader);
  __unnamed4.Read(reader);
  _opaqueDistance.Read(reader);
  __unnamed5.Read(reader);
  _opaqueDepth.Read(reader);
  __unnamed6.Read(reader);
  _distanceToWaterPlane.Read(reader);
  _color.Read(reader);
  _flags2.Read(reader);
  _layerCount.Read(reader);
  _distanceGradient.Read(reader);
  _densityGradient.Read(reader);
  _startDistanceFromFogPlane.Read(reader);
  __unnamed7.Read(reader);
  _color2.Read(reader);
  _rotationMultiplier.Read(reader);
  _strafingMultiplier.Read(reader);
  _zoomMultiplier.Read(reader);
  __unnamed8.Read(reader);
  _mapScale.Read(reader);
  _map.Read(reader);
  _animationPeriod.Read(reader);
  __unnamed9.Read(reader);
  _windVelocity.Read(reader);
  _windPeriod.Read(reader);
  _windAccelerationWeight.Read(reader);
  _windPerpendicularWeight.Read(reader);
  __unnamed10.Read(reader);
  _backgroundSound.Read(reader);
  _soundEnvironment.Read(reader);
  __unnamed11.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_map.ReadString(reader);
_backgroundSound.ReadString(reader);
_soundEnvironment.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    _maximumDensity.Write(writer);
    __unnamed4.Write(writer);
    _opaqueDistance.Write(writer);
    __unnamed5.Write(writer);
    _opaqueDepth.Write(writer);
    __unnamed6.Write(writer);
    _distanceToWaterPlane.Write(writer);
    _color.Write(writer);
    _flags2.Write(writer);
    _layerCount.Write(writer);
    _distanceGradient.Write(writer);
    _densityGradient.Write(writer);
    _startDistanceFromFogPlane.Write(writer);
    __unnamed7.Write(writer);
    _color2.Write(writer);
    _rotationMultiplier.Write(writer);
    _strafingMultiplier.Write(writer);
    _zoomMultiplier.Write(writer);
    __unnamed8.Write(writer);
    _mapScale.Write(writer);
    _map.Write(writer);
    _animationPeriod.Write(writer);
    __unnamed9.Write(writer);
    _windVelocity.Write(writer);
    _windPeriod.Write(writer);
    _windAccelerationWeight.Write(writer);
    _windPerpendicularWeight.Write(writer);
    __unnamed10.Write(writer);
    _backgroundSound.Write(writer);
    _soundEnvironment.Write(writer);
    __unnamed11.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_map.WriteString(writer);
_backgroundSound.WriteString(writer);
_soundEnvironment.WriteString(writer);
}
}
  }
}
