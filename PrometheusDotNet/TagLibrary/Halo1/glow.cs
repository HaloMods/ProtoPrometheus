using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Glow : IBlock
  {
    public GlowBlock GlowValues = new GlowBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'Glow'------------------------------------------------------");
      GlowValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      GlowValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      GlowValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      GlowValues.WriteChildData(writer);
    }
public class GlowBlock : IBlock
{
private FixedLengthString _attachmentMarker = new FixedLengthString();
private ShortInteger _numberOfParticles = new ShortInteger();
private Enum _boundaryEffect = new Enum();
private Enum _normalParticleDistribution = new Enum();
private Enum _trailingParticleDistribution = new Enum();
private Flags  _glowFlags;	
private Pad  __unnamed;	
private Pad  __unnamed2;	
private Pad  __unnamed3;	
private Pad  __unnamed4;	
private Enum _attachment = new Enum();
private Pad  __unnamed5;	
private Real _particleRotationalVelocity = new Real();
private Real _particleRotVelMulLow = new Real();
private Real _particleRotVelMulHigh = new Real();
private Enum _attachment2 = new Enum();
private Pad  __unnamed6;	
private Real _effectRotationalVelocity = new Real();
private Real _effectRotVelMulLow = new Real();
private Real _effectRotVelMulHigh = new Real();
private Enum _attachment3 = new Enum();
private Pad  __unnamed7;	
private Real _effectTranslationalVelocity = new Real();
private Real _effectTransVelMulLow = new Real();
private Real _effectTransVelMulHigh = new Real();
private Enum _attachment4 = new Enum();
private Pad  __unnamed8;	
private Real _minDistanceParticleToObject = new Real();
private Real _maxDistanceParticleToObject = new Real();
private Real _distanceToObjectMulLow = new Real();
private Real _distanceToObjectMulHigh = new Real();
private Pad  __unnamed9;	
private Enum _attachment5 = new Enum();
private Pad  __unnamed10;	
private RealBounds _particleSizeBounds = new RealBounds();
private RealBounds _sizeAttachmentMultiplier = new RealBounds();
private Enum _attachment6 = new Enum();
private Pad  __unnamed11;	
private RealARGBColor _color_bound_0 = new RealARGBColor();
private RealARGBColor _color_bound_1 = new RealARGBColor();
private RealARGBColor _scaleColor0 = new RealARGBColor();
private RealARGBColor _scaleColor1 = new RealARGBColor();
private Real _colorRateOfChange = new Real();
private Real _fadingPercentageOfGlow = new Real();
private Real _particleGenerationFreq = new Real();
private Real _lifetimeOfTrailingParticles = new Real();
private Real _velocityOfTrailingParticles = new Real();
private Real _trailingParticleMinimumT = new Real();
private Real _trailingParticleMaximumT = new Real();
private Pad  __unnamed12;	
private TagReference _texture = new TagReference();
public FixedLengthString AttachmentMarker
{
  get { return _attachmentMarker; }
  set { _attachmentMarker = value; }
}
public ShortInteger NumberOfParticles
{
  get { return _numberOfParticles; }
  set { _numberOfParticles = value; }
}
public Enum BoundaryEffect
{
  get { return _boundaryEffect; }
  set { _boundaryEffect = value; }
}
public Enum NormalParticleDistribution
{
  get { return _normalParticleDistribution; }
  set { _normalParticleDistribution = value; }
}
public Enum TrailingParticleDistribution
{
  get { return _trailingParticleDistribution; }
  set { _trailingParticleDistribution = value; }
}
public Flags GlowFlags
{
  get { return _glowFlags; }
  set { _glowFlags = value; }
}
public Enum Attachment
{
  get { return _attachment; }
  set { _attachment = value; }
}
public Real ParticleRotationalVelocity
{
  get { return _particleRotationalVelocity; }
  set { _particleRotationalVelocity = value; }
}
public Real ParticleRotVelMulLow
{
  get { return _particleRotVelMulLow; }
  set { _particleRotVelMulLow = value; }
}
public Real ParticleRotVelMulHigh
{
  get { return _particleRotVelMulHigh; }
  set { _particleRotVelMulHigh = value; }
}
public Enum Attachment2
{
  get { return _attachment2; }
  set { _attachment2 = value; }
}
public Real EffectRotationalVelocity
{
  get { return _effectRotationalVelocity; }
  set { _effectRotationalVelocity = value; }
}
public Real EffectRotVelMulLow
{
  get { return _effectRotVelMulLow; }
  set { _effectRotVelMulLow = value; }
}
public Real EffectRotVelMulHigh
{
  get { return _effectRotVelMulHigh; }
  set { _effectRotVelMulHigh = value; }
}
public Enum Attachment3
{
  get { return _attachment3; }
  set { _attachment3 = value; }
}
public Real EffectTranslationalVelocity
{
  get { return _effectTranslationalVelocity; }
  set { _effectTranslationalVelocity = value; }
}
public Real EffectTransVelMulLow
{
  get { return _effectTransVelMulLow; }
  set { _effectTransVelMulLow = value; }
}
public Real EffectTransVelMulHigh
{
  get { return _effectTransVelMulHigh; }
  set { _effectTransVelMulHigh = value; }
}
public Enum Attachment4
{
  get { return _attachment4; }
  set { _attachment4 = value; }
}
public Real MinDistanceParticleToObject
{
  get { return _minDistanceParticleToObject; }
  set { _minDistanceParticleToObject = value; }
}
public Real MaxDistanceParticleToObject
{
  get { return _maxDistanceParticleToObject; }
  set { _maxDistanceParticleToObject = value; }
}
public Real DistanceToObjectMulLow
{
  get { return _distanceToObjectMulLow; }
  set { _distanceToObjectMulLow = value; }
}
public Real DistanceToObjectMulHigh
{
  get { return _distanceToObjectMulHigh; }
  set { _distanceToObjectMulHigh = value; }
}
public Enum Attachment5
{
  get { return _attachment5; }
  set { _attachment5 = value; }
}
public RealBounds ParticleSizeBounds
{
  get { return _particleSizeBounds; }
  set { _particleSizeBounds = value; }
}
public RealBounds SizeAttachmentMultiplier
{
  get { return _sizeAttachmentMultiplier; }
  set { _sizeAttachmentMultiplier = value; }
}
public Enum Attachment6
{
  get { return _attachment6; }
  set { _attachment6 = value; }
}
public RealARGBColor Color_bound_0
{
  get { return _color_bound_0; }
  set { _color_bound_0 = value; }
}
public RealARGBColor Color_bound_1
{
  get { return _color_bound_1; }
  set { _color_bound_1 = value; }
}
public RealARGBColor ScaleColor0
{
  get { return _scaleColor0; }
  set { _scaleColor0 = value; }
}
public RealARGBColor ScaleColor1
{
  get { return _scaleColor1; }
  set { _scaleColor1 = value; }
}
public Real ColorRateOfChange
{
  get { return _colorRateOfChange; }
  set { _colorRateOfChange = value; }
}
public Real FadingPercentageOfGlow
{
  get { return _fadingPercentageOfGlow; }
  set { _fadingPercentageOfGlow = value; }
}
public Real ParticleGenerationFreq
{
  get { return _particleGenerationFreq; }
  set { _particleGenerationFreq = value; }
}
public Real LifetimeOfTrailingParticles
{
  get { return _lifetimeOfTrailingParticles; }
  set { _lifetimeOfTrailingParticles = value; }
}
public Real VelocityOfTrailingParticles
{
  get { return _velocityOfTrailingParticles; }
  set { _velocityOfTrailingParticles = value; }
}
public Real TrailingParticleMinimumT
{
  get { return _trailingParticleMinimumT; }
  set { _trailingParticleMinimumT = value; }
}
public Real TrailingParticleMaximumT
{
  get { return _trailingParticleMaximumT; }
  set { _trailingParticleMaximumT = value; }
}
public TagReference Texture
{
  get { return _texture; }
  set { _texture = value; }
}
public GlowBlock()
{
_glowFlags = new Flags(4);
__unnamed = new Pad(28);
__unnamed2 = new Pad(2);
__unnamed3 = new Pad(2);
__unnamed4 = new Pad(4);
__unnamed5 = new Pad(2);
__unnamed6 = new Pad(2);
__unnamed7 = new Pad(2);
__unnamed8 = new Pad(2);
__unnamed9 = new Pad(8);
__unnamed10 = new Pad(2);
__unnamed11 = new Pad(2);
__unnamed12 = new Pad(52);

}
public void Read(BinaryReader reader)
{
  _attachmentMarker.Read(reader);
  _numberOfParticles.Read(reader);
  _boundaryEffect.Read(reader);
  _normalParticleDistribution.Read(reader);
  _trailingParticleDistribution.Read(reader);
  _glowFlags.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  __unnamed4.Read(reader);
  _attachment.Read(reader);
  __unnamed5.Read(reader);
  _particleRotationalVelocity.Read(reader);
  _particleRotVelMulLow.Read(reader);
  _particleRotVelMulHigh.Read(reader);
  _attachment2.Read(reader);
  __unnamed6.Read(reader);
  _effectRotationalVelocity.Read(reader);
  _effectRotVelMulLow.Read(reader);
  _effectRotVelMulHigh.Read(reader);
  _attachment3.Read(reader);
  __unnamed7.Read(reader);
  _effectTranslationalVelocity.Read(reader);
  _effectTransVelMulLow.Read(reader);
  _effectTransVelMulHigh.Read(reader);
  _attachment4.Read(reader);
  __unnamed8.Read(reader);
  _minDistanceParticleToObject.Read(reader);
  _maxDistanceParticleToObject.Read(reader);
  _distanceToObjectMulLow.Read(reader);
  _distanceToObjectMulHigh.Read(reader);
  __unnamed9.Read(reader);
  _attachment5.Read(reader);
  __unnamed10.Read(reader);
  _particleSizeBounds.Read(reader);
  _sizeAttachmentMultiplier.Read(reader);
  _attachment6.Read(reader);
  __unnamed11.Read(reader);
  _color_bound_0.Read(reader);
  _color_bound_1.Read(reader);
  _scaleColor0.Read(reader);
  _scaleColor1.Read(reader);
  _colorRateOfChange.Read(reader);
  _fadingPercentageOfGlow.Read(reader);
  _particleGenerationFreq.Read(reader);
  _lifetimeOfTrailingParticles.Read(reader);
  _velocityOfTrailingParticles.Read(reader);
  _trailingParticleMinimumT.Read(reader);
  _trailingParticleMaximumT.Read(reader);
  __unnamed12.Read(reader);
  _texture.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_texture.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _attachmentMarker.Write(writer);
    _numberOfParticles.Write(writer);
    _boundaryEffect.Write(writer);
    _normalParticleDistribution.Write(writer);
    _trailingParticleDistribution.Write(writer);
    _glowFlags.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    __unnamed4.Write(writer);
    _attachment.Write(writer);
    __unnamed5.Write(writer);
    _particleRotationalVelocity.Write(writer);
    _particleRotVelMulLow.Write(writer);
    _particleRotVelMulHigh.Write(writer);
    _attachment2.Write(writer);
    __unnamed6.Write(writer);
    _effectRotationalVelocity.Write(writer);
    _effectRotVelMulLow.Write(writer);
    _effectRotVelMulHigh.Write(writer);
    _attachment3.Write(writer);
    __unnamed7.Write(writer);
    _effectTranslationalVelocity.Write(writer);
    _effectTransVelMulLow.Write(writer);
    _effectTransVelMulHigh.Write(writer);
    _attachment4.Write(writer);
    __unnamed8.Write(writer);
    _minDistanceParticleToObject.Write(writer);
    _maxDistanceParticleToObject.Write(writer);
    _distanceToObjectMulLow.Write(writer);
    _distanceToObjectMulHigh.Write(writer);
    __unnamed9.Write(writer);
    _attachment5.Write(writer);
    __unnamed10.Write(writer);
    _particleSizeBounds.Write(writer);
    _sizeAttachmentMultiplier.Write(writer);
    _attachment6.Write(writer);
    __unnamed11.Write(writer);
    _color_bound_0.Write(writer);
    _color_bound_1.Write(writer);
    _scaleColor0.Write(writer);
    _scaleColor1.Write(writer);
    _colorRateOfChange.Write(writer);
    _fadingPercentageOfGlow.Write(writer);
    _particleGenerationFreq.Write(writer);
    _lifetimeOfTrailingParticles.Write(writer);
    _velocityOfTrailingParticles.Write(writer);
    _trailingParticleMinimumT.Write(writer);
    _trailingParticleMaximumT.Write(writer);
    __unnamed12.Write(writer);
    _texture.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_texture.WriteString(writer);
}
}
  }
}
