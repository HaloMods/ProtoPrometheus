using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Decal : IBlock
  {
    public DecalBlock DecalValues = new DecalBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'Decal'------------------------------------------------------");
      DecalValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      DecalValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      DecalValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      DecalValues.WriteChildData(writer);
    }
public class DecalBlock : IBlock
{
private Flags  _flags;	
private Enum _type = new Enum();
private Enum _layer = new Enum();
private Pad  __unnamed;	
private TagReference _nextDecalInChain = new TagReference();
private RealBounds _radius = new RealBounds();
private Pad  __unnamed2;	
private RealFractionBounds _intensity = new RealFractionBounds();
private RealRGBColor _colorLowerBounds = new RealRGBColor();
private RealRGBColor _colorUpperBounds = new RealRGBColor();
private Pad  __unnamed3;	
private ShortInteger _animationLoopFrame = new ShortInteger();
private ShortInteger _animationSpeed = new ShortInteger();
private Pad  __unnamed4;	
private RealBounds _lifetime = new RealBounds();
private RealBounds _decayTime = new RealBounds();
private Pad  __unnamed5;	
private Pad  __unnamed6;	
private Pad  __unnamed7;	
private Pad  __unnamed8;	
private Enum _framebufferBlendFunction = new Enum();
private Pad  __unnamed9;	
private Pad  __unnamed10;	
private TagReference _map = new TagReference();
private Pad  __unnamed11;	
private Real _maximumSpriteExtent = new Real();
private Pad  __unnamed12;	
private Pad  __unnamed13;	
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Enum Type
{
  get { return _type; }
  set { _type = value; }
}
public Enum Layer
{
  get { return _layer; }
  set { _layer = value; }
}
public TagReference NextDecalInChain
{
  get { return _nextDecalInChain; }
  set { _nextDecalInChain = value; }
}
public RealBounds Radius
{
  get { return _radius; }
  set { _radius = value; }
}
public RealFractionBounds Intensity
{
  get { return _intensity; }
  set { _intensity = value; }
}
public RealRGBColor ColorLowerBounds
{
  get { return _colorLowerBounds; }
  set { _colorLowerBounds = value; }
}
public RealRGBColor ColorUpperBounds
{
  get { return _colorUpperBounds; }
  set { _colorUpperBounds = value; }
}
public ShortInteger AnimationLoopFrame
{
  get { return _animationLoopFrame; }
  set { _animationLoopFrame = value; }
}
public ShortInteger AnimationSpeed
{
  get { return _animationSpeed; }
  set { _animationSpeed = value; }
}
public RealBounds Lifetime
{
  get { return _lifetime; }
  set { _lifetime = value; }
}
public RealBounds DecayTime
{
  get { return _decayTime; }
  set { _decayTime = value; }
}
public Enum FramebufferBlendFunction
{
  get { return _framebufferBlendFunction; }
  set { _framebufferBlendFunction = value; }
}
public TagReference Map
{
  get { return _map; }
  set { _map = value; }
}
public Real MaximumSpriteExtent
{
  get { return _maximumSpriteExtent; }
  set { _maximumSpriteExtent = value; }
}
public DecalBlock()
{
_flags = new Flags(2);
__unnamed = new Pad(2);
__unnamed2 = new Pad(12);
__unnamed3 = new Pad(12);
__unnamed4 = new Pad(28);
__unnamed5 = new Pad(12);
__unnamed6 = new Pad(40);
__unnamed7 = new Pad(2);
__unnamed8 = new Pad(2);
__unnamed9 = new Pad(2);
__unnamed10 = new Pad(20);
__unnamed11 = new Pad(20);
__unnamed12 = new Pad(4);
__unnamed13 = new Pad(8);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _type.Read(reader);
  _layer.Read(reader);
  __unnamed.Read(reader);
  _nextDecalInChain.Read(reader);
  _radius.Read(reader);
  __unnamed2.Read(reader);
  _intensity.Read(reader);
  _colorLowerBounds.Read(reader);
  _colorUpperBounds.Read(reader);
  __unnamed3.Read(reader);
  _animationLoopFrame.Read(reader);
  _animationSpeed.Read(reader);
  __unnamed4.Read(reader);
  _lifetime.Read(reader);
  _decayTime.Read(reader);
  __unnamed5.Read(reader);
  __unnamed6.Read(reader);
  __unnamed7.Read(reader);
  __unnamed8.Read(reader);
  _framebufferBlendFunction.Read(reader);
  __unnamed9.Read(reader);
  __unnamed10.Read(reader);
  _map.Read(reader);
  __unnamed11.Read(reader);
  _maximumSpriteExtent.Read(reader);
  __unnamed12.Read(reader);
  __unnamed13.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_nextDecalInChain.ReadString(reader);
_map.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _type.Write(writer);
    _layer.Write(writer);
    __unnamed.Write(writer);
    _nextDecalInChain.Write(writer);
    _radius.Write(writer);
    __unnamed2.Write(writer);
    _intensity.Write(writer);
    _colorLowerBounds.Write(writer);
    _colorUpperBounds.Write(writer);
    __unnamed3.Write(writer);
    _animationLoopFrame.Write(writer);
    _animationSpeed.Write(writer);
    __unnamed4.Write(writer);
    _lifetime.Write(writer);
    _decayTime.Write(writer);
    __unnamed5.Write(writer);
    __unnamed6.Write(writer);
    __unnamed7.Write(writer);
    __unnamed8.Write(writer);
    _framebufferBlendFunction.Write(writer);
    __unnamed9.Write(writer);
    __unnamed10.Write(writer);
    _map.Write(writer);
    __unnamed11.Write(writer);
    _maximumSpriteExtent.Write(writer);
    __unnamed12.Write(writer);
    __unnamed13.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_nextDecalInChain.WriteString(writer);
_map.WriteString(writer);
}
}
  }
}
