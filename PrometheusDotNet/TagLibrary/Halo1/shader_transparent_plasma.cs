using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class ShaderTransparentPlasma : Shader
  {
    public ShaderTransparentPlasmaBlock ShaderTransparentPlasmaValues = new ShaderTransparentPlasmaBlock();
    public new void Read(BinaryReader reader)
    {
      base.Read(reader);
      Trace.WriteLine("Loading 'ShaderTransparentPlasma'------------------------------------------------------");
      ShaderTransparentPlasmaValues.Read(reader);
    }
    public new void ReadChildData(BinaryReader reader)
    {
      base.ReadChildData(reader);
      ShaderTransparentPlasmaValues.ReadChildData(reader);
    }
    public new void Write(BinaryWriter writer)
    {
      base.Write(writer);
      ShaderTransparentPlasmaValues.Write(writer);
    }
    public new void WriteChildData(BinaryWriter writer)
    {
      base.WriteChildData(writer);
      ShaderTransparentPlasmaValues.WriteChildData(writer);
    }
public class ShaderTransparentPlasmaBlock : IBlock
{
private Pad  __unnamed;	
private Pad  __unnamed2;	
private Enum _intensitySource = new Enum();
private Pad  __unnamed3;	
private Real _intensityExponent = new Real();
private Enum _offsetSource = new Enum();
private Pad  __unnamed4;	
private Real _offsetAmount = new Real();
private Real _offsetExponent = new Real();
private Pad  __unnamed5;	
private RealFraction _perpendicularBrightness = new RealFraction();
private RealRGBColor _perpendicularTintColor = new RealRGBColor();
private RealFraction _parallelBrightness = new RealFraction();
private RealRGBColor _parallelTintColor = new RealRGBColor();
private Enum _tintColorSource = new Enum();
private Pad  __unnamed6;	
private Pad  __unnamed7;	
private Pad  __unnamed8;	
private Pad  __unnamed9;	
private Pad  __unnamed10;	
private Pad  __unnamed11;	
private Pad  __unnamed12;	
private Real _primaryAnimationPeriod = new Real();
private RealVector3D _primaryAnimationDirection = new RealVector3D();
private Real _primaryNoiseMapScale = new Real();
private TagReference _primaryNoiseMap = new TagReference();
private Pad  __unnamed13;	
private Pad  __unnamed14;	
private Real _secondaryAnimationPeriod = new Real();
private RealVector3D _secondaryAnimationDirection = new RealVector3D();
private Real _secondaryNoiseMapScale = new Real();
private TagReference _secondaryNoiseMap = new TagReference();
private Pad  __unnamed15;	
public Enum IntensitySource
{
  get { return _intensitySource; }
  set { _intensitySource = value; }
}
public Real IntensityExponent
{
  get { return _intensityExponent; }
  set { _intensityExponent = value; }
}
public Enum OffsetSource
{
  get { return _offsetSource; }
  set { _offsetSource = value; }
}
public Real OffsetAmount
{
  get { return _offsetAmount; }
  set { _offsetAmount = value; }
}
public Real OffsetExponent
{
  get { return _offsetExponent; }
  set { _offsetExponent = value; }
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
public Enum TintColorSource
{
  get { return _tintColorSource; }
  set { _tintColorSource = value; }
}
public Real PrimaryAnimationPeriod
{
  get { return _primaryAnimationPeriod; }
  set { _primaryAnimationPeriod = value; }
}
public RealVector3D PrimaryAnimationDirection
{
  get { return _primaryAnimationDirection; }
  set { _primaryAnimationDirection = value; }
}
public Real PrimaryNoiseMapScale
{
  get { return _primaryNoiseMapScale; }
  set { _primaryNoiseMapScale = value; }
}
public TagReference PrimaryNoiseMap
{
  get { return _primaryNoiseMap; }
  set { _primaryNoiseMap = value; }
}
public Real SecondaryAnimationPeriod
{
  get { return _secondaryAnimationPeriod; }
  set { _secondaryAnimationPeriod = value; }
}
public RealVector3D SecondaryAnimationDirection
{
  get { return _secondaryAnimationDirection; }
  set { _secondaryAnimationDirection = value; }
}
public Real SecondaryNoiseMapScale
{
  get { return _secondaryNoiseMapScale; }
  set { _secondaryNoiseMapScale = value; }
}
public TagReference SecondaryNoiseMap
{
  get { return _secondaryNoiseMap; }
  set { _secondaryNoiseMap = value; }
}
public ShaderTransparentPlasmaBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(2);
__unnamed3 = new Pad(2);
__unnamed4 = new Pad(2);
__unnamed5 = new Pad(32);
__unnamed6 = new Pad(2);
__unnamed7 = new Pad(32);
__unnamed8 = new Pad(2);
__unnamed9 = new Pad(2);
__unnamed10 = new Pad(16);
__unnamed11 = new Pad(4);
__unnamed12 = new Pad(4);
__unnamed13 = new Pad(32);
__unnamed14 = new Pad(4);
__unnamed15 = new Pad(32);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  _intensitySource.Read(reader);
  __unnamed3.Read(reader);
  _intensityExponent.Read(reader);
  _offsetSource.Read(reader);
  __unnamed4.Read(reader);
  _offsetAmount.Read(reader);
  _offsetExponent.Read(reader);
  __unnamed5.Read(reader);
  _perpendicularBrightness.Read(reader);
  _perpendicularTintColor.Read(reader);
  _parallelBrightness.Read(reader);
  _parallelTintColor.Read(reader);
  _tintColorSource.Read(reader);
  __unnamed6.Read(reader);
  __unnamed7.Read(reader);
  __unnamed8.Read(reader);
  __unnamed9.Read(reader);
  __unnamed10.Read(reader);
  __unnamed11.Read(reader);
  __unnamed12.Read(reader);
  _primaryAnimationPeriod.Read(reader);
  _primaryAnimationDirection.Read(reader);
  _primaryNoiseMapScale.Read(reader);
  _primaryNoiseMap.Read(reader);
  __unnamed13.Read(reader);
  __unnamed14.Read(reader);
  _secondaryAnimationPeriod.Read(reader);
  _secondaryAnimationDirection.Read(reader);
  _secondaryNoiseMapScale.Read(reader);
  _secondaryNoiseMap.Read(reader);
  __unnamed15.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_primaryNoiseMap.ReadString(reader);
_secondaryNoiseMap.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    _intensitySource.Write(writer);
    __unnamed3.Write(writer);
    _intensityExponent.Write(writer);
    _offsetSource.Write(writer);
    __unnamed4.Write(writer);
    _offsetAmount.Write(writer);
    _offsetExponent.Write(writer);
    __unnamed5.Write(writer);
    _perpendicularBrightness.Write(writer);
    _perpendicularTintColor.Write(writer);
    _parallelBrightness.Write(writer);
    _parallelTintColor.Write(writer);
    _tintColorSource.Write(writer);
    __unnamed6.Write(writer);
    __unnamed7.Write(writer);
    __unnamed8.Write(writer);
    __unnamed9.Write(writer);
    __unnamed10.Write(writer);
    __unnamed11.Write(writer);
    __unnamed12.Write(writer);
    _primaryAnimationPeriod.Write(writer);
    _primaryAnimationDirection.Write(writer);
    _primaryNoiseMapScale.Write(writer);
    _primaryNoiseMap.Write(writer);
    __unnamed13.Write(writer);
    __unnamed14.Write(writer);
    _secondaryAnimationPeriod.Write(writer);
    _secondaryAnimationDirection.Write(writer);
    _secondaryNoiseMapScale.Write(writer);
    _secondaryNoiseMap.Write(writer);
    __unnamed15.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_primaryNoiseMap.WriteString(writer);
_secondaryNoiseMap.WriteString(writer);
}
}
  }
}
