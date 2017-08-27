using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class ShaderEnvironment : Shader
  {
    public ShaderEnvironmentBlock ShaderEnvironmentValues = new ShaderEnvironmentBlock();
    public new void Read(BinaryReader reader)
    {
      base.Read(reader);
      Trace.WriteLine("Loading 'ShaderEnvironment'------------------------------------------------------");
      ShaderEnvironmentValues.Read(reader);
    }
    public new void ReadChildData(BinaryReader reader)
    {
      base.ReadChildData(reader);
      ShaderEnvironmentValues.ReadChildData(reader);
    }
    public new void Write(BinaryWriter writer)
    {
      base.Write(writer);
      ShaderEnvironmentValues.Write(writer);
    }
    public new void WriteChildData(BinaryWriter writer)
    {
      base.WriteChildData(writer);
      ShaderEnvironmentValues.WriteChildData(writer);
    }
public class ShaderEnvironmentBlock : IBlock
{
private Flags  _flags;	
private Enum _type = new Enum();
private Real _lensFlareSpacing = new Real();
private TagReference _lensFlare = new TagReference();
private Pad  __unnamed;	
private Flags  _flags2;	
private Pad  __unnamed2;	
private Pad  __unnamed3;	
private TagReference _baseMap = new TagReference();
private Pad  __unnamed4;	
private Enum _detailMapFunction = new Enum();
private Pad  __unnamed5;	
private Real _primaryDetailMapScale = new Real();
private TagReference _primaryDetailMap = new TagReference();
private Real _secondaryDetailMapScale = new Real();
private TagReference _secondaryDetailMap = new TagReference();
private Pad  __unnamed6;	
private Enum _microDetailMapFunction = new Enum();
private Pad  __unnamed7;	
private Real _microDetailMapScale = new Real();
private TagReference _microDetailMap = new TagReference();
private RealRGBColor _materialColor = new RealRGBColor();
private Pad  __unnamed8;	
private Real _bumpMapScale = new Real();
private TagReference _bumpMap = new TagReference();
private Pad  __unnamed9;	
private Pad  __unnamed10;	
private Enum __unnamed11 = new Enum();
private Pad  __unnamed12;	
private Real __unnamed13 = new Real();
private Real __unnamed14 = new Real();
private Enum __unnamed15 = new Enum();
private Pad  __unnamed16;	
private Real __unnamed17 = new Real();
private Real __unnamed18 = new Real();
private Pad  __unnamed19;	
private Flags  _flags3;	
private Pad  __unnamed20;	
private Pad  __unnamed21;	
private RealRGBColor _primaryOnColor = new RealRGBColor();
private RealRGBColor _primaryOffColor = new RealRGBColor();
private Enum _primaryAnimationFunction = new Enum();
private Pad  __unnamed22;	
private Real _primaryAnimationPeriod = new Real();
private Real _primaryAnimationPhase = new Real();
private Pad  __unnamed23;	
private RealRGBColor _secondaryOnColor = new RealRGBColor();
private RealRGBColor _secondaryOffColor = new RealRGBColor();
private Enum _secondaryAnimationFunction = new Enum();
private Pad  __unnamed24;	
private Real _secondaryAnimationPeriod = new Real();
private Real _secondaryAnimationPhase = new Real();
private Pad  __unnamed25;	
private RealRGBColor _plasmaOnColor = new RealRGBColor();
private RealRGBColor _plasmaOffColor = new RealRGBColor();
private Enum _plasmaAnimationFunction = new Enum();
private Pad  __unnamed26;	
private Real _plasmaAnimationPeriod = new Real();
private Real _plasmaAnimationPhase = new Real();
private Pad  __unnamed27;	
private Real _mapScale = new Real();
private TagReference _map = new TagReference();
private Pad  __unnamed28;	
private Flags  _flags4;	
private Pad  __unnamed29;	
private Pad  __unnamed30;	
private RealFraction _brightness = new RealFraction();
private Pad  __unnamed31;	
private RealRGBColor _perpendicularColor = new RealRGBColor();
private RealRGBColor _parallelColor = new RealRGBColor();
private Pad  __unnamed32;	
private Flags  _flags5;	
private Enum _type2 = new Enum();
private RealFraction _lightmapBrightnessScale = new RealFraction();
private Pad  __unnamed33;	
private RealFraction _perpendicularBrightness = new RealFraction();
private RealFraction _parallelBrightness = new RealFraction();
private Pad  __unnamed34;	
private Pad  __unnamed35;	
private Pad  __unnamed36;	
private TagReference _reflectionCubeMap = new TagReference();
private Pad  __unnamed37;	
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
public Real LensFlareSpacing
{
  get { return _lensFlareSpacing; }
  set { _lensFlareSpacing = value; }
}
public TagReference LensFlare
{
  get { return _lensFlare; }
  set { _lensFlare = value; }
}
public Flags Flags2
{
  get { return _flags2; }
  set { _flags2 = value; }
}
public TagReference BaseMap
{
  get { return _baseMap; }
  set { _baseMap = value; }
}
public Enum DetailMapFunction
{
  get { return _detailMapFunction; }
  set { _detailMapFunction = value; }
}
public Real PrimaryDetailMapScale
{
  get { return _primaryDetailMapScale; }
  set { _primaryDetailMapScale = value; }
}
public TagReference PrimaryDetailMap
{
  get { return _primaryDetailMap; }
  set { _primaryDetailMap = value; }
}
public Real SecondaryDetailMapScale
{
  get { return _secondaryDetailMapScale; }
  set { _secondaryDetailMapScale = value; }
}
public TagReference SecondaryDetailMap
{
  get { return _secondaryDetailMap; }
  set { _secondaryDetailMap = value; }
}
public Enum MicroDetailMapFunction
{
  get { return _microDetailMapFunction; }
  set { _microDetailMapFunction = value; }
}
public Real MicroDetailMapScale
{
  get { return _microDetailMapScale; }
  set { _microDetailMapScale = value; }
}
public TagReference MicroDetailMap
{
  get { return _microDetailMap; }
  set { _microDetailMap = value; }
}
public RealRGBColor MaterialColor
{
  get { return _materialColor; }
  set { _materialColor = value; }
}
public Real BumpMapScale
{
  get { return _bumpMapScale; }
  set { _bumpMapScale = value; }
}
public TagReference BumpMap
{
  get { return _bumpMap; }
  set { _bumpMap = value; }
}
public Enum _unnamed11
{
  get { return __unnamed11; }
  set { __unnamed11 = value; }
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
public Enum _unnamed15
{
  get { return __unnamed15; }
  set { __unnamed15 = value; }
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
public Flags Flags3
{
  get { return _flags3; }
  set { _flags3 = value; }
}
public RealRGBColor PrimaryOnColor
{
  get { return _primaryOnColor; }
  set { _primaryOnColor = value; }
}
public RealRGBColor PrimaryOffColor
{
  get { return _primaryOffColor; }
  set { _primaryOffColor = value; }
}
public Enum PrimaryAnimationFunction
{
  get { return _primaryAnimationFunction; }
  set { _primaryAnimationFunction = value; }
}
public Real PrimaryAnimationPeriod
{
  get { return _primaryAnimationPeriod; }
  set { _primaryAnimationPeriod = value; }
}
public Real PrimaryAnimationPhase
{
  get { return _primaryAnimationPhase; }
  set { _primaryAnimationPhase = value; }
}
public RealRGBColor SecondaryOnColor
{
  get { return _secondaryOnColor; }
  set { _secondaryOnColor = value; }
}
public RealRGBColor SecondaryOffColor
{
  get { return _secondaryOffColor; }
  set { _secondaryOffColor = value; }
}
public Enum SecondaryAnimationFunction
{
  get { return _secondaryAnimationFunction; }
  set { _secondaryAnimationFunction = value; }
}
public Real SecondaryAnimationPeriod
{
  get { return _secondaryAnimationPeriod; }
  set { _secondaryAnimationPeriod = value; }
}
public Real SecondaryAnimationPhase
{
  get { return _secondaryAnimationPhase; }
  set { _secondaryAnimationPhase = value; }
}
public RealRGBColor PlasmaOnColor
{
  get { return _plasmaOnColor; }
  set { _plasmaOnColor = value; }
}
public RealRGBColor PlasmaOffColor
{
  get { return _plasmaOffColor; }
  set { _plasmaOffColor = value; }
}
public Enum PlasmaAnimationFunction
{
  get { return _plasmaAnimationFunction; }
  set { _plasmaAnimationFunction = value; }
}
public Real PlasmaAnimationPeriod
{
  get { return _plasmaAnimationPeriod; }
  set { _plasmaAnimationPeriod = value; }
}
public Real PlasmaAnimationPhase
{
  get { return _plasmaAnimationPhase; }
  set { _plasmaAnimationPhase = value; }
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
public Flags Flags4
{
  get { return _flags4; }
  set { _flags4 = value; }
}
public RealFraction Brightness
{
  get { return _brightness; }
  set { _brightness = value; }
}
public RealRGBColor PerpendicularColor
{
  get { return _perpendicularColor; }
  set { _perpendicularColor = value; }
}
public RealRGBColor ParallelColor
{
  get { return _parallelColor; }
  set { _parallelColor = value; }
}
public Flags Flags5
{
  get { return _flags5; }
  set { _flags5 = value; }
}
public Enum Type2
{
  get { return _type2; }
  set { _type2 = value; }
}
public RealFraction LightmapBrightnessScale
{
  get { return _lightmapBrightnessScale; }
  set { _lightmapBrightnessScale = value; }
}
public RealFraction PerpendicularBrightness
{
  get { return _perpendicularBrightness; }
  set { _perpendicularBrightness = value; }
}
public RealFraction ParallelBrightness
{
  get { return _parallelBrightness; }
  set { _parallelBrightness = value; }
}
public TagReference ReflectionCubeMap
{
  get { return _reflectionCubeMap; }
  set { _reflectionCubeMap = value; }
}
public ShaderEnvironmentBlock()
{
_flags = new Flags(2);
__unnamed = new Pad(44);
_flags2 = new Flags(2);
__unnamed2 = new Pad(2);
__unnamed3 = new Pad(24);
__unnamed4 = new Pad(24);
__unnamed5 = new Pad(2);
__unnamed6 = new Pad(24);
__unnamed7 = new Pad(2);
__unnamed8 = new Pad(12);
__unnamed9 = new Pad(8);
__unnamed10 = new Pad(16);
__unnamed12 = new Pad(2);
__unnamed16 = new Pad(2);
__unnamed19 = new Pad(24);
_flags3 = new Flags(2);
__unnamed20 = new Pad(2);
__unnamed21 = new Pad(24);
__unnamed22 = new Pad(2);
__unnamed23 = new Pad(24);
__unnamed24 = new Pad(2);
__unnamed25 = new Pad(24);
__unnamed26 = new Pad(2);
__unnamed27 = new Pad(24);
__unnamed28 = new Pad(24);
_flags4 = new Flags(2);
__unnamed29 = new Pad(2);
__unnamed30 = new Pad(16);
__unnamed31 = new Pad(20);
__unnamed32 = new Pad(16);
_flags5 = new Flags(2);
__unnamed33 = new Pad(28);
__unnamed34 = new Pad(16);
__unnamed35 = new Pad(8);
__unnamed36 = new Pad(16);
__unnamed37 = new Pad(16);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _type.Read(reader);
  _lensFlareSpacing.Read(reader);
  _lensFlare.Read(reader);
  __unnamed.Read(reader);
  _flags2.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  _baseMap.Read(reader);
  __unnamed4.Read(reader);
  _detailMapFunction.Read(reader);
  __unnamed5.Read(reader);
  _primaryDetailMapScale.Read(reader);
  _primaryDetailMap.Read(reader);
  _secondaryDetailMapScale.Read(reader);
  _secondaryDetailMap.Read(reader);
  __unnamed6.Read(reader);
  _microDetailMapFunction.Read(reader);
  __unnamed7.Read(reader);
  _microDetailMapScale.Read(reader);
  _microDetailMap.Read(reader);
  _materialColor.Read(reader);
  __unnamed8.Read(reader);
  _bumpMapScale.Read(reader);
  _bumpMap.Read(reader);
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
  __unnamed19.Read(reader);
  _flags3.Read(reader);
  __unnamed20.Read(reader);
  __unnamed21.Read(reader);
  _primaryOnColor.Read(reader);
  _primaryOffColor.Read(reader);
  _primaryAnimationFunction.Read(reader);
  __unnamed22.Read(reader);
  _primaryAnimationPeriod.Read(reader);
  _primaryAnimationPhase.Read(reader);
  __unnamed23.Read(reader);
  _secondaryOnColor.Read(reader);
  _secondaryOffColor.Read(reader);
  _secondaryAnimationFunction.Read(reader);
  __unnamed24.Read(reader);
  _secondaryAnimationPeriod.Read(reader);
  _secondaryAnimationPhase.Read(reader);
  __unnamed25.Read(reader);
  _plasmaOnColor.Read(reader);
  _plasmaOffColor.Read(reader);
  _plasmaAnimationFunction.Read(reader);
  __unnamed26.Read(reader);
  _plasmaAnimationPeriod.Read(reader);
  _plasmaAnimationPhase.Read(reader);
  __unnamed27.Read(reader);
  _mapScale.Read(reader);
  _map.Read(reader);
  __unnamed28.Read(reader);
  _flags4.Read(reader);
  __unnamed29.Read(reader);
  __unnamed30.Read(reader);
  _brightness.Read(reader);
  __unnamed31.Read(reader);
  _perpendicularColor.Read(reader);
  _parallelColor.Read(reader);
  __unnamed32.Read(reader);
  _flags5.Read(reader);
  _type2.Read(reader);
  _lightmapBrightnessScale.Read(reader);
  __unnamed33.Read(reader);
  _perpendicularBrightness.Read(reader);
  _parallelBrightness.Read(reader);
  __unnamed34.Read(reader);
  __unnamed35.Read(reader);
  __unnamed36.Read(reader);
  _reflectionCubeMap.Read(reader);
  __unnamed37.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_lensFlare.ReadString(reader);
_baseMap.ReadString(reader);
_primaryDetailMap.ReadString(reader);
_secondaryDetailMap.ReadString(reader);
_microDetailMap.ReadString(reader);
_bumpMap.ReadString(reader);
_map.ReadString(reader);
_reflectionCubeMap.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _type.Write(writer);
    _lensFlareSpacing.Write(writer);
    _lensFlare.Write(writer);
    __unnamed.Write(writer);
    _flags2.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    _baseMap.Write(writer);
    __unnamed4.Write(writer);
    _detailMapFunction.Write(writer);
    __unnamed5.Write(writer);
    _primaryDetailMapScale.Write(writer);
    _primaryDetailMap.Write(writer);
    _secondaryDetailMapScale.Write(writer);
    _secondaryDetailMap.Write(writer);
    __unnamed6.Write(writer);
    _microDetailMapFunction.Write(writer);
    __unnamed7.Write(writer);
    _microDetailMapScale.Write(writer);
    _microDetailMap.Write(writer);
    _materialColor.Write(writer);
    __unnamed8.Write(writer);
    _bumpMapScale.Write(writer);
    _bumpMap.Write(writer);
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
    __unnamed19.Write(writer);
    _flags3.Write(writer);
    __unnamed20.Write(writer);
    __unnamed21.Write(writer);
    _primaryOnColor.Write(writer);
    _primaryOffColor.Write(writer);
    _primaryAnimationFunction.Write(writer);
    __unnamed22.Write(writer);
    _primaryAnimationPeriod.Write(writer);
    _primaryAnimationPhase.Write(writer);
    __unnamed23.Write(writer);
    _secondaryOnColor.Write(writer);
    _secondaryOffColor.Write(writer);
    _secondaryAnimationFunction.Write(writer);
    __unnamed24.Write(writer);
    _secondaryAnimationPeriod.Write(writer);
    _secondaryAnimationPhase.Write(writer);
    __unnamed25.Write(writer);
    _plasmaOnColor.Write(writer);
    _plasmaOffColor.Write(writer);
    _plasmaAnimationFunction.Write(writer);
    __unnamed26.Write(writer);
    _plasmaAnimationPeriod.Write(writer);
    _plasmaAnimationPhase.Write(writer);
    __unnamed27.Write(writer);
    _mapScale.Write(writer);
    _map.Write(writer);
    __unnamed28.Write(writer);
    _flags4.Write(writer);
    __unnamed29.Write(writer);
    __unnamed30.Write(writer);
    _brightness.Write(writer);
    __unnamed31.Write(writer);
    _perpendicularColor.Write(writer);
    _parallelColor.Write(writer);
    __unnamed32.Write(writer);
    _flags5.Write(writer);
    _type2.Write(writer);
    _lightmapBrightnessScale.Write(writer);
    __unnamed33.Write(writer);
    _perpendicularBrightness.Write(writer);
    _parallelBrightness.Write(writer);
    __unnamed34.Write(writer);
    __unnamed35.Write(writer);
    __unnamed36.Write(writer);
    _reflectionCubeMap.Write(writer);
    __unnamed37.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_lensFlare.WriteString(writer);
_baseMap.WriteString(writer);
_primaryDetailMap.WriteString(writer);
_secondaryDetailMap.WriteString(writer);
_microDetailMap.WriteString(writer);
_bumpMap.WriteString(writer);
_map.WriteString(writer);
_reflectionCubeMap.WriteString(writer);
}
}
  }
}
