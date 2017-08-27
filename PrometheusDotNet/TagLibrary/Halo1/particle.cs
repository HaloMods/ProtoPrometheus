using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Particle : IBlock
  {
    public ParticleBlock ParticleValues = new ParticleBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'Particle'------------------------------------------------------");
      ParticleValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      ParticleValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      ParticleValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      ParticleValues.WriteChildData(writer);
    }
public class ParticleBlock : IBlock
{
private Flags  _flags;	
private TagReference _bitmap = new TagReference();
private TagReference _physics = new TagReference();
private TagReference _martyTradedHisKidsForThis = new TagReference();
private Pad  __unnamed;	
private RealBounds _lifespan = new RealBounds();
private Real _fadeInTime = new Real();
private Real _fadeOutTime = new Real();
private TagReference _collisionEffect = new TagReference();
private TagReference _deathEffect = new TagReference();
private Real _minimumSize = new Real();
private Pad  __unnamed2;	
private RealBounds _radiusAnimation = new RealBounds();
private Pad  __unnamed3;	
private RealBounds _animationRate = new RealBounds();
private Real _contactDeterioration = new Real();
private Real _fadeStartSize = new Real();
private Real _fadeEndSize = new Real();
private Pad  __unnamed4;	
private ShortInteger _firstSequenceIndex = new ShortInteger();
private ShortInteger _initialSequenceCount = new ShortInteger();
private ShortInteger _loopingSequenceCount = new ShortInteger();
private ShortInteger _finalSequenceCount = new ShortInteger();
private Pad  __unnamed5;	
private Enum _orientation = new Enum();
private Pad  __unnamed6;	
private Pad  __unnamed7;	
private Flags  _shaderFlags;	
private Enum _framebufferBlendFunction = new Enum();
private Enum _framebufferFadeMode = new Enum();
private Flags  _mapFlags;	
private Pad  __unnamed8;	
private TagReference _bitmap2 = new TagReference();
private Enum _anchor = new Enum();
private Flags  _flags2;	
private Enum __unnamed9 = new Enum();
private Enum __unnamed10 = new Enum();
private Real __unnamed11 = new Real();
private Real __unnamed12 = new Real();
private Real __unnamed13 = new Real();
private Enum __unnamed14 = new Enum();
private Enum __unnamed15 = new Enum();
private Real __unnamed16 = new Real();
private Real __unnamed17 = new Real();
private Real __unnamed18 = new Real();
private Enum _rotatio = new Enum();
private Enum _rotatio2 = new Enum();
private Real _rotatio3 = new Real();
private Real _rotatio4 = new Real();
private Real _rotatio5 = new Real();
private RealPoint2D _rotatio6 = new RealPoint2D();
private Pad  __unnamed19;	
private Real _zspriteRadiusScale = new Real();
private Pad  __unnamed20;	
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public TagReference Bitmap
{
  get { return _bitmap; }
  set { _bitmap = value; }
}
public TagReference Physics
{
  get { return _physics; }
  set { _physics = value; }
}
public TagReference MartyTradedHisKidsForThis
{
  get { return _martyTradedHisKidsForThis; }
  set { _martyTradedHisKidsForThis = value; }
}
public RealBounds Lifespan
{
  get { return _lifespan; }
  set { _lifespan = value; }
}
public Real FadeInTime
{
  get { return _fadeInTime; }
  set { _fadeInTime = value; }
}
public Real FadeOutTime
{
  get { return _fadeOutTime; }
  set { _fadeOutTime = value; }
}
public TagReference CollisionEffect
{
  get { return _collisionEffect; }
  set { _collisionEffect = value; }
}
public TagReference DeathEffect
{
  get { return _deathEffect; }
  set { _deathEffect = value; }
}
public Real MinimumSize
{
  get { return _minimumSize; }
  set { _minimumSize = value; }
}
public RealBounds RadiusAnimation
{
  get { return _radiusAnimation; }
  set { _radiusAnimation = value; }
}
public RealBounds AnimationRate
{
  get { return _animationRate; }
  set { _animationRate = value; }
}
public Real ContactDeterioration
{
  get { return _contactDeterioration; }
  set { _contactDeterioration = value; }
}
public Real FadeStartSize
{
  get { return _fadeStartSize; }
  set { _fadeStartSize = value; }
}
public Real FadeEndSize
{
  get { return _fadeEndSize; }
  set { _fadeEndSize = value; }
}
public ShortInteger FirstSequenceIndex
{
  get { return _firstSequenceIndex; }
  set { _firstSequenceIndex = value; }
}
public ShortInteger InitialSequenceCount
{
  get { return _initialSequenceCount; }
  set { _initialSequenceCount = value; }
}
public ShortInteger LoopingSequenceCount
{
  get { return _loopingSequenceCount; }
  set { _loopingSequenceCount = value; }
}
public ShortInteger FinalSequenceCount
{
  get { return _finalSequenceCount; }
  set { _finalSequenceCount = value; }
}
public Enum Orientation
{
  get { return _orientation; }
  set { _orientation = value; }
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
public Enum _unnamed9
{
  get { return __unnamed9; }
  set { __unnamed9 = value; }
}
public Enum _unnamed10
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
public Real _unnamed13
{
  get { return __unnamed13; }
  set { __unnamed13 = value; }
}
public Enum _unnamed14
{
  get { return __unnamed14; }
  set { __unnamed14 = value; }
}
public Enum _unnamed15
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
public Real _unnamed18
{
  get { return __unnamed18; }
  set { __unnamed18 = value; }
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
public ParticleBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(4);
__unnamed2 = new Pad(8);
__unnamed3 = new Pad(4);
__unnamed4 = new Pad(4);
__unnamed5 = new Pad(12);
__unnamed6 = new Pad(2);
__unnamed7 = new Pad(40);
_shaderFlags = new Flags(2);
_mapFlags = new Flags(2);
__unnamed8 = new Pad(28);
_flags2 = new Flags(2);
__unnamed19 = new Pad(4);
__unnamed20 = new Pad(20);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _bitmap.Read(reader);
  _physics.Read(reader);
  _martyTradedHisKidsForThis.Read(reader);
  __unnamed.Read(reader);
  _lifespan.Read(reader);
  _fadeInTime.Read(reader);
  _fadeOutTime.Read(reader);
  _collisionEffect.Read(reader);
  _deathEffect.Read(reader);
  _minimumSize.Read(reader);
  __unnamed2.Read(reader);
  _radiusAnimation.Read(reader);
  __unnamed3.Read(reader);
  _animationRate.Read(reader);
  _contactDeterioration.Read(reader);
  _fadeStartSize.Read(reader);
  _fadeEndSize.Read(reader);
  __unnamed4.Read(reader);
  _firstSequenceIndex.Read(reader);
  _initialSequenceCount.Read(reader);
  _loopingSequenceCount.Read(reader);
  _finalSequenceCount.Read(reader);
  __unnamed5.Read(reader);
  _orientation.Read(reader);
  __unnamed6.Read(reader);
  __unnamed7.Read(reader);
  _shaderFlags.Read(reader);
  _framebufferBlendFunction.Read(reader);
  _framebufferFadeMode.Read(reader);
  _mapFlags.Read(reader);
  __unnamed8.Read(reader);
  _bitmap2.Read(reader);
  _anchor.Read(reader);
  _flags2.Read(reader);
  __unnamed9.Read(reader);
  __unnamed10.Read(reader);
  __unnamed11.Read(reader);
  __unnamed12.Read(reader);
  __unnamed13.Read(reader);
  __unnamed14.Read(reader);
  __unnamed15.Read(reader);
  __unnamed16.Read(reader);
  __unnamed17.Read(reader);
  __unnamed18.Read(reader);
  _rotatio.Read(reader);
  _rotatio2.Read(reader);
  _rotatio3.Read(reader);
  _rotatio4.Read(reader);
  _rotatio5.Read(reader);
  _rotatio6.Read(reader);
  __unnamed19.Read(reader);
  _zspriteRadiusScale.Read(reader);
  __unnamed20.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_bitmap.ReadString(reader);
_physics.ReadString(reader);
_martyTradedHisKidsForThis.ReadString(reader);
_collisionEffect.ReadString(reader);
_deathEffect.ReadString(reader);
_bitmap2.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _bitmap.Write(writer);
    _physics.Write(writer);
    _martyTradedHisKidsForThis.Write(writer);
    __unnamed.Write(writer);
    _lifespan.Write(writer);
    _fadeInTime.Write(writer);
    _fadeOutTime.Write(writer);
    _collisionEffect.Write(writer);
    _deathEffect.Write(writer);
    _minimumSize.Write(writer);
    __unnamed2.Write(writer);
    _radiusAnimation.Write(writer);
    __unnamed3.Write(writer);
    _animationRate.Write(writer);
    _contactDeterioration.Write(writer);
    _fadeStartSize.Write(writer);
    _fadeEndSize.Write(writer);
    __unnamed4.Write(writer);
    _firstSequenceIndex.Write(writer);
    _initialSequenceCount.Write(writer);
    _loopingSequenceCount.Write(writer);
    _finalSequenceCount.Write(writer);
    __unnamed5.Write(writer);
    _orientation.Write(writer);
    __unnamed6.Write(writer);
    __unnamed7.Write(writer);
    _shaderFlags.Write(writer);
    _framebufferBlendFunction.Write(writer);
    _framebufferFadeMode.Write(writer);
    _mapFlags.Write(writer);
    __unnamed8.Write(writer);
    _bitmap2.Write(writer);
    _anchor.Write(writer);
    _flags2.Write(writer);
    __unnamed9.Write(writer);
    __unnamed10.Write(writer);
    __unnamed11.Write(writer);
    __unnamed12.Write(writer);
    __unnamed13.Write(writer);
    __unnamed14.Write(writer);
    __unnamed15.Write(writer);
    __unnamed16.Write(writer);
    __unnamed17.Write(writer);
    __unnamed18.Write(writer);
    _rotatio.Write(writer);
    _rotatio2.Write(writer);
    _rotatio3.Write(writer);
    _rotatio4.Write(writer);
    _rotatio5.Write(writer);
    _rotatio6.Write(writer);
    __unnamed19.Write(writer);
    _zspriteRadiusScale.Write(writer);
    __unnamed20.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_bitmap.WriteString(writer);
_physics.WriteString(writer);
_martyTradedHisKidsForThis.WriteString(writer);
_collisionEffect.WriteString(writer);
_deathEffect.WriteString(writer);
_bitmap2.WriteString(writer);
}
}
  }
}
