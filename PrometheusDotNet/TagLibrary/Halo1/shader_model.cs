using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class ShaderModel : Shader
  {
    public ShaderModelBlock ShaderModelValues = new ShaderModelBlock();
    public new void Read(BinaryReader reader)
    {
      base.Read(reader);
      Trace.WriteLine("Loading 'ShaderModel'------------------------------------------------------");
      ShaderModelValues.Read(reader);
    }
    public new void ReadChildData(BinaryReader reader)
    {
      base.ReadChildData(reader);
      ShaderModelValues.ReadChildData(reader);
    }
    public new void Write(BinaryWriter writer)
    {
      base.Write(writer);
      ShaderModelValues.Write(writer);
    }
    public new void WriteChildData(BinaryWriter writer)
    {
      base.WriteChildData(writer);
      ShaderModelValues.WriteChildData(writer);
    }
public class ShaderModelBlock : IBlock
{
private Flags  _flags;	
private Pad  __unnamed;	
private Pad  __unnamed2;	
private RealFraction _translucency = new RealFraction();
private Pad  __unnamed3;	
private Enum _changeColorSource = new Enum();
private Pad  __unnamed4;	
private Pad  __unnamed5;	
private Flags  _flags2;	
private Pad  __unnamed6;	
private Enum _colorSource = new Enum();
private Enum _animationFunction = new Enum();
private Real _animationPeriod = new Real();
private RealRGBColor _animationColorLowerBound = new RealRGBColor();
private RealRGBColor _animationColorUpperBound = new RealRGBColor();
private Pad  __unnamed7;	
private Real _map = new Real();
private Real _map2 = new Real();
private TagReference _baseMap = new TagReference();
private Pad  __unnamed8;	
private TagReference _multipurposeMap = new TagReference();
private Pad  __unnamed9;	
private Enum _detailFunction = new Enum();
private Enum _detailMask = new Enum();
private Real _detailMapScale = new Real();
private TagReference _detailMap = new TagReference();
private Real _detailMap2 = new Real();
private Pad  __unnamed10;	
private Enum __unnamed11 = new Enum();
private Enum __unnamed12 = new Enum();
private Real __unnamed13 = new Real();
private Real __unnamed14 = new Real();
private Real __unnamed15 = new Real();
private Enum __unnamed16 = new Enum();
private Enum __unnamed17 = new Enum();
private Real __unnamed18 = new Real();
private Real __unnamed19 = new Real();
private Real __unnamed20 = new Real();
private Enum _rotatio = new Enum();
private Enum _rotatio2 = new Enum();
private Real _rotatio3 = new Real();
private Real _rotatio4 = new Real();
private Real _rotatio5 = new Real();
private RealPoint2D _rotatio6 = new RealPoint2D();
private Pad  __unnamed21;	
private Real _reflectionFalloffDistance = new Real();
private Real _reflectionCutoffDistance = new Real();
private RealFraction _perpendicularBrightness = new RealFraction();
private RealRGBColor _perpendicularTintColor = new RealRGBColor();
private RealFraction _parallelBrightness = new RealFraction();
private RealRGBColor _parallelTintColor = new RealRGBColor();
private TagReference _reflectionCubeMap = new TagReference();
private Pad  __unnamed22;	
private Pad  __unnamed23;	
private Pad  __unnamed24;	
private Pad  __unnamed25;	
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public RealFraction Translucency
{
  get { return _translucency; }
  set { _translucency = value; }
}
public Enum ChangeColorSource
{
  get { return _changeColorSource; }
  set { _changeColorSource = value; }
}
public Flags Flags2
{
  get { return _flags2; }
  set { _flags2 = value; }
}
public Enum ColorSource
{
  get { return _colorSource; }
  set { _colorSource = value; }
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
public RealRGBColor AnimationColorLowerBound
{
  get { return _animationColorLowerBound; }
  set { _animationColorLowerBound = value; }
}
public RealRGBColor AnimationColorUpperBound
{
  get { return _animationColorUpperBound; }
  set { _animationColorUpperBound = value; }
}
public Real Map
{
  get { return _map; }
  set { _map = value; }
}
public Real Map2
{
  get { return _map2; }
  set { _map2 = value; }
}
public TagReference BaseMap
{
  get { return _baseMap; }
  set { _baseMap = value; }
}
public TagReference MultipurposeMap
{
  get { return _multipurposeMap; }
  set { _multipurposeMap = value; }
}
public Enum DetailFunction
{
  get { return _detailFunction; }
  set { _detailFunction = value; }
}
public Enum DetailMask
{
  get { return _detailMask; }
  set { _detailMask = value; }
}
public Real DetailMapScale
{
  get { return _detailMapScale; }
  set { _detailMapScale = value; }
}
public TagReference DetailMap
{
  get { return _detailMap; }
  set { _detailMap = value; }
}
public Real DetailMap2
{
  get { return _detailMap2; }
  set { _detailMap2 = value; }
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
public Enum _unnamed16
{
  get { return __unnamed16; }
  set { __unnamed16 = value; }
}
public Enum _unnamed17
{
  get { return __unnamed17; }
  set { __unnamed17 = value; }
}
public Real _unnamed18
{
  get { return __unnamed18; }
  set { __unnamed18 = value; }
}
public Real _unnamed19
{
  get { return __unnamed19; }
  set { __unnamed19 = value; }
}
public Real _unnamed20
{
  get { return __unnamed20; }
  set { __unnamed20 = value; }
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
public Real ReflectionFalloffDistance
{
  get { return _reflectionFalloffDistance; }
  set { _reflectionFalloffDistance = value; }
}
public Real ReflectionCutoffDistance
{
  get { return _reflectionCutoffDistance; }
  set { _reflectionCutoffDistance = value; }
}
public RealFraction PerpendicularBrightness
{
  get { return _perpendicularBrightness; }
  set { _perpendicularBrightness = value; }
}
public RealRGBColor PerpendicularTintColor
{
  get { return _perpendicularTintColor; }
  set { _perpendicularTintColor = value; }
}
public RealFraction ParallelBrightness
{
  get { return _parallelBrightness; }
  set { _parallelBrightness = value; }
}
public RealRGBColor ParallelTintColor
{
  get { return _parallelTintColor; }
  set { _parallelTintColor = value; }
}
public TagReference ReflectionCubeMap
{
  get { return _reflectionCubeMap; }
  set { _reflectionCubeMap = value; }
}
public ShaderModelBlock()
{
_flags = new Flags(2);
__unnamed = new Pad(2);
__unnamed2 = new Pad(12);
__unnamed3 = new Pad(16);
__unnamed4 = new Pad(2);
__unnamed5 = new Pad(28);
_flags2 = new Flags(2);
__unnamed6 = new Pad(2);
__unnamed7 = new Pad(12);
__unnamed8 = new Pad(8);
__unnamed9 = new Pad(8);
__unnamed10 = new Pad(12);
__unnamed21 = new Pad(8);
__unnamed22 = new Pad(16);
__unnamed23 = new Pad(4);
__unnamed24 = new Pad(16);
__unnamed25 = new Pad(32);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  _translucency.Read(reader);
  __unnamed3.Read(reader);
  _changeColorSource.Read(reader);
  __unnamed4.Read(reader);
  __unnamed5.Read(reader);
  _flags2.Read(reader);
  __unnamed6.Read(reader);
  _colorSource.Read(reader);
  _animationFunction.Read(reader);
  _animationPeriod.Read(reader);
  _animationColorLowerBound.Read(reader);
  _animationColorUpperBound.Read(reader);
  __unnamed7.Read(reader);
  _map.Read(reader);
  _map2.Read(reader);
  _baseMap.Read(reader);
  __unnamed8.Read(reader);
  _multipurposeMap.Read(reader);
  __unnamed9.Read(reader);
  _detailFunction.Read(reader);
  _detailMask.Read(reader);
  _detailMapScale.Read(reader);
  _detailMap.Read(reader);
  _detailMap2.Read(reader);
  __unnamed10.Read(reader);
  __unnamed11.Read(reader);
  __unnamed12.Read(reader);
  __unnamed13.Read(reader);
  __unnamed14.Read(reader);
  __unnamed15.Read(reader);
  __unnamed16.Read(reader);
  __unnamed17.Read(reader);
  __unnamed18.Read(reader);
  __unnamed19.Read(reader);
  __unnamed20.Read(reader);
  _rotatio.Read(reader);
  _rotatio2.Read(reader);
  _rotatio3.Read(reader);
  _rotatio4.Read(reader);
  _rotatio5.Read(reader);
  _rotatio6.Read(reader);
  __unnamed21.Read(reader);
  _reflectionFalloffDistance.Read(reader);
  _reflectionCutoffDistance.Read(reader);
  _perpendicularBrightness.Read(reader);
  _perpendicularTintColor.Read(reader);
  _parallelBrightness.Read(reader);
  _parallelTintColor.Read(reader);
  _reflectionCubeMap.Read(reader);
  __unnamed22.Read(reader);
  __unnamed23.Read(reader);
  __unnamed24.Read(reader);
  __unnamed25.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_baseMap.ReadString(reader);
_multipurposeMap.ReadString(reader);
_detailMap.ReadString(reader);
_reflectionCubeMap.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    _translucency.Write(writer);
    __unnamed3.Write(writer);
    _changeColorSource.Write(writer);
    __unnamed4.Write(writer);
    __unnamed5.Write(writer);
    _flags2.Write(writer);
    __unnamed6.Write(writer);
    _colorSource.Write(writer);
    _animationFunction.Write(writer);
    _animationPeriod.Write(writer);
    _animationColorLowerBound.Write(writer);
    _animationColorUpperBound.Write(writer);
    __unnamed7.Write(writer);
    _map.Write(writer);
    _map2.Write(writer);
    _baseMap.Write(writer);
    __unnamed8.Write(writer);
    _multipurposeMap.Write(writer);
    __unnamed9.Write(writer);
    _detailFunction.Write(writer);
    _detailMask.Write(writer);
    _detailMapScale.Write(writer);
    _detailMap.Write(writer);
    _detailMap2.Write(writer);
    __unnamed10.Write(writer);
    __unnamed11.Write(writer);
    __unnamed12.Write(writer);
    __unnamed13.Write(writer);
    __unnamed14.Write(writer);
    __unnamed15.Write(writer);
    __unnamed16.Write(writer);
    __unnamed17.Write(writer);
    __unnamed18.Write(writer);
    __unnamed19.Write(writer);
    __unnamed20.Write(writer);
    _rotatio.Write(writer);
    _rotatio2.Write(writer);
    _rotatio3.Write(writer);
    _rotatio4.Write(writer);
    _rotatio5.Write(writer);
    _rotatio6.Write(writer);
    __unnamed21.Write(writer);
    _reflectionFalloffDistance.Write(writer);
    _reflectionCutoffDistance.Write(writer);
    _perpendicularBrightness.Write(writer);
    _perpendicularTintColor.Write(writer);
    _parallelBrightness.Write(writer);
    _parallelTintColor.Write(writer);
    _reflectionCubeMap.Write(writer);
    __unnamed22.Write(writer);
    __unnamed23.Write(writer);
    __unnamed24.Write(writer);
    __unnamed25.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_baseMap.WriteString(writer);
_multipurposeMap.WriteString(writer);
_detailMap.WriteString(writer);
_reflectionCubeMap.WriteString(writer);
}
}
  }
}
