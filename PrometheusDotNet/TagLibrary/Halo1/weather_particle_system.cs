using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class WeatherParticleSystem : IBlock
  {
    public WeatherParticleSystemBlock WeatherParticleSystemValues = new WeatherParticleSystemBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'WeatherParticleSystem'------------------------------------------------------");
      WeatherParticleSystemValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      WeatherParticleSystemValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      WeatherParticleSystemValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      WeatherParticleSystemValues.WriteChildData(writer);
    }
public class WeatherParticleSystemBlock : IBlock
{
private Flags  _flags;	
private Pad  __unnamed;	
private Block _particleTypes = new Block();
public class WeatherParticleTypeBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public WeatherParticleTypeBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(WeatherParticleTypeBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new WeatherParticleTypeBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public WeatherParticleTypeBlock this[int index]
  {
    get { return (InnerList[index] as WeatherParticleTypeBlock); }
  }
}
private WeatherParticleTypeBlockCollection _particleTypesCollection;
public WeatherParticleTypeBlockCollection ParticleTypes
{
  get { return _particleTypesCollection; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public WeatherParticleSystemBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(32);
_particleTypesCollection = new WeatherParticleTypeBlockCollection(_particleTypes);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  __unnamed.Read(reader);
  _particleTypes.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
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
    _flags.Write(writer);
    __unnamed.Write(writer);
    _particleTypes.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_particleTypes.UpdateReflexiveOffset(writer);
for (int x=0; x<_particleTypes.Count; x++)
{
  ParticleTypes[x].Write(writer);
}
for (int x=0; x<_particleTypes.Count; x++)
  ParticleTypes[x].WriteChildData(writer);
}
}
public class WeatherParticleTypeBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private Flags  _flags;	
private Real _fad = new Real();
private Real _fad2 = new Real();
private Real _fad3 = new Real();
private Real _fad4 = new Real();
private Real _fad5 = new Real();
private Real _fad6 = new Real();
private Real _fad7 = new Real();
private Real _fad8 = new Real();
private Pad  __unnamed;	
private RealBounds _particleCount = new RealBounds();
private TagReference _physics = new TagReference();
private Pad  __unnamed2;	
private RealBounds _accelerationMagnitude = new RealBounds();
private RealFraction _accelerationTurningRate = new RealFraction();
private Real _accelerationChangeRate = new Real();
private Pad  __unnamed3;	
private RealBounds _particleRadius = new RealBounds();
private RealBounds _animationRate = new RealBounds();
private AngleBounds _rotationRate = new AngleBounds();
private Pad  __unnamed4;	
private RealARGBColor _colorLowerBound = new RealARGBColor();
private RealARGBColor _colorUpperBound = new RealARGBColor();
private Pad  __unnamed5;	
private TagReference _spriteBitmap = new TagReference();
private Enum _renderMode = new Enum();
private Enum _renderDirectionSource = new Enum();
private Pad  __unnamed6;	
private Flags  _shaderFlags;	
private Enum _framebufferBlendFunction = new Enum();
private Enum _framebufferFadeMode = new Enum();
private Flags  _mapFlags;	
private Pad  __unnamed7;	
private TagReference _bitmap = new TagReference();
private Enum _anchor = new Enum();
private Flags  _flags2;	
private Enum __unnamed8 = new Enum();
private Enum __unnamed9 = new Enum();
private Real __unnamed10 = new Real();
private Real __unnamed11 = new Real();
private Real __unnamed12 = new Real();
private Enum __unnamed13 = new Enum();
private Enum __unnamed14 = new Enum();
private Real __unnamed15 = new Real();
private Real __unnamed16 = new Real();
private Real __unnamed17 = new Real();
private Enum _rotatio = new Enum();
private Enum _rotatio2 = new Enum();
private Real _rotatio3 = new Real();
private Real _rotatio4 = new Real();
private Real _rotatio5 = new Real();
private RealPoint2D _rotatio6 = new RealPoint2D();
private Pad  __unnamed18;	
private Real _zspriteRadiusScale = new Real();
private Pad  __unnamed19;	
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
public Real Fad
{
  get { return _fad; }
  set { _fad = value; }
}
public Real Fad2
{
  get { return _fad2; }
  set { _fad2 = value; }
}
public Real Fad3
{
  get { return _fad3; }
  set { _fad3 = value; }
}
public Real Fad4
{
  get { return _fad4; }
  set { _fad4 = value; }
}
public Real Fad5
{
  get { return _fad5; }
  set { _fad5 = value; }
}
public Real Fad6
{
  get { return _fad6; }
  set { _fad6 = value; }
}
public Real Fad7
{
  get { return _fad7; }
  set { _fad7 = value; }
}
public Real Fad8
{
  get { return _fad8; }
  set { _fad8 = value; }
}
public RealBounds ParticleCount
{
  get { return _particleCount; }
  set { _particleCount = value; }
}
public TagReference Physics
{
  get { return _physics; }
  set { _physics = value; }
}
public RealBounds AccelerationMagnitude
{
  get { return _accelerationMagnitude; }
  set { _accelerationMagnitude = value; }
}
public RealFraction AccelerationTurningRate
{
  get { return _accelerationTurningRate; }
  set { _accelerationTurningRate = value; }
}
public Real AccelerationChangeRate
{
  get { return _accelerationChangeRate; }
  set { _accelerationChangeRate = value; }
}
public RealBounds ParticleRadius
{
  get { return _particleRadius; }
  set { _particleRadius = value; }
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
public TagReference SpriteBitmap
{
  get { return _spriteBitmap; }
  set { _spriteBitmap = value; }
}
public Enum RenderMode
{
  get { return _renderMode; }
  set { _renderMode = value; }
}
public Enum RenderDirectionSource
{
  get { return _renderDirectionSource; }
  set { _renderDirectionSource = value; }
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
public Flags Flags2
{
  get { return _flags2; }
  set { _flags2 = value; }
}
public Enum _unnamed8
{
  get { return __unnamed8; }
  set { __unnamed8 = value; }
}
public Enum _unnamed9
{
  get { return __unnamed9; }
  set { __unnamed9 = value; }
}
public Real _unnamed10
{
  get { return __unnamed10; }
  set { __unnamed10 = value; }
}
public Real _unnamed11
{
  get { return __unnamed11; }
  set { __unnamed11 = value; }
}
public Real _unnamed12
{
  get { return __unnamed12; }
  set { __unnamed12 = value; }
}
public Enum _unnamed13
{
  get { return __unnamed13; }
  set { __unnamed13 = value; }
}
public Enum _unnamed14
{
  get { return __unnamed14; }
  set { __unnamed14 = value; }
}
public Real _unnamed15
{
  get { return __unnamed15; }
  set { __unnamed15 = value; }
}
public Real _unnamed16
{
  get { return __unnamed16; }
  set { __unnamed16 = value; }
}
public Real _unnamed17
{
  get { return __unnamed17; }
  set { __unnamed17 = value; }
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
public WeatherParticleTypeBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(96);
__unnamed2 = new Pad(16);
__unnamed3 = new Pad(32);
__unnamed4 = new Pad(32);
__unnamed5 = new Pad(64);
__unnamed6 = new Pad(40);
_shaderFlags = new Flags(2);
_mapFlags = new Flags(2);
__unnamed7 = new Pad(28);
_flags2 = new Flags(2);
__unnamed18 = new Pad(4);
__unnamed19 = new Pad(20);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _flags.Read(reader);
  _fad.Read(reader);
  _fad2.Read(reader);
  _fad3.Read(reader);
  _fad4.Read(reader);
  _fad5.Read(reader);
  _fad6.Read(reader);
  _fad7.Read(reader);
  _fad8.Read(reader);
  __unnamed.Read(reader);
  _particleCount.Read(reader);
  _physics.Read(reader);
  __unnamed2.Read(reader);
  _accelerationMagnitude.Read(reader);
  _accelerationTurningRate.Read(reader);
  _accelerationChangeRate.Read(reader);
  __unnamed3.Read(reader);
  _particleRadius.Read(reader);
  _animationRate.Read(reader);
  _rotationRate.Read(reader);
  __unnamed4.Read(reader);
  _colorLowerBound.Read(reader);
  _colorUpperBound.Read(reader);
  __unnamed5.Read(reader);
  _spriteBitmap.Read(reader);
  _renderMode.Read(reader);
  _renderDirectionSource.Read(reader);
  __unnamed6.Read(reader);
  _shaderFlags.Read(reader);
  _framebufferBlendFunction.Read(reader);
  _framebufferFadeMode.Read(reader);
  _mapFlags.Read(reader);
  __unnamed7.Read(reader);
  _bitmap.Read(reader);
  _anchor.Read(reader);
  _flags2.Read(reader);
  __unnamed8.Read(reader);
  __unnamed9.Read(reader);
  __unnamed10.Read(reader);
  __unnamed11.Read(reader);
  __unnamed12.Read(reader);
  __unnamed13.Read(reader);
  __unnamed14.Read(reader);
  __unnamed15.Read(reader);
  __unnamed16.Read(reader);
  __unnamed17.Read(reader);
  _rotatio.Read(reader);
  _rotatio2.Read(reader);
  _rotatio3.Read(reader);
  _rotatio4.Read(reader);
  _rotatio5.Read(reader);
  _rotatio6.Read(reader);
  __unnamed18.Read(reader);
  _zspriteRadiusScale.Read(reader);
  __unnamed19.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_physics.ReadString(reader);
_spriteBitmap.ReadString(reader);
_bitmap.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _flags.Write(writer);
    _fad.Write(writer);
    _fad2.Write(writer);
    _fad3.Write(writer);
    _fad4.Write(writer);
    _fad5.Write(writer);
    _fad6.Write(writer);
    _fad7.Write(writer);
    _fad8.Write(writer);
    __unnamed.Write(writer);
    _particleCount.Write(writer);
    _physics.Write(writer);
    __unnamed2.Write(writer);
    _accelerationMagnitude.Write(writer);
    _accelerationTurningRate.Write(writer);
    _accelerationChangeRate.Write(writer);
    __unnamed3.Write(writer);
    _particleRadius.Write(writer);
    _animationRate.Write(writer);
    _rotationRate.Write(writer);
    __unnamed4.Write(writer);
    _colorLowerBound.Write(writer);
    _colorUpperBound.Write(writer);
    __unnamed5.Write(writer);
    _spriteBitmap.Write(writer);
    _renderMode.Write(writer);
    _renderDirectionSource.Write(writer);
    __unnamed6.Write(writer);
    _shaderFlags.Write(writer);
    _framebufferBlendFunction.Write(writer);
    _framebufferFadeMode.Write(writer);
    _mapFlags.Write(writer);
    __unnamed7.Write(writer);
    _bitmap.Write(writer);
    _anchor.Write(writer);
    _flags2.Write(writer);
    __unnamed8.Write(writer);
    __unnamed9.Write(writer);
    __unnamed10.Write(writer);
    __unnamed11.Write(writer);
    __unnamed12.Write(writer);
    __unnamed13.Write(writer);
    __unnamed14.Write(writer);
    __unnamed15.Write(writer);
    __unnamed16.Write(writer);
    __unnamed17.Write(writer);
    _rotatio.Write(writer);
    _rotatio2.Write(writer);
    _rotatio3.Write(writer);
    _rotatio4.Write(writer);
    _rotatio5.Write(writer);
    _rotatio6.Write(writer);
    __unnamed18.Write(writer);
    _zspriteRadiusScale.Write(writer);
    __unnamed19.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_physics.WriteString(writer);
_spriteBitmap.WriteString(writer);
_bitmap.WriteString(writer);
}
}
  }
}
