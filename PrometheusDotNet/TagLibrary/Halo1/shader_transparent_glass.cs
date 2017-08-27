using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class ShaderTransparentGlass : Shader
  {
    public ShaderTransparentGlassBlock ShaderTransparentGlassValues = new ShaderTransparentGlassBlock();
    public new void Read(BinaryReader reader)
    {
      base.Read(reader);
      Trace.WriteLine("Loading 'ShaderTransparentGlass'------------------------------------------------------");
      ShaderTransparentGlassValues.Read(reader);
    }
    public new void ReadChildData(BinaryReader reader)
    {
      base.ReadChildData(reader);
      ShaderTransparentGlassValues.ReadChildData(reader);
    }
    public new void Write(BinaryWriter writer)
    {
      base.Write(writer);
      ShaderTransparentGlassValues.Write(writer);
    }
    public new void WriteChildData(BinaryWriter writer)
    {
      base.WriteChildData(writer);
      ShaderTransparentGlassValues.WriteChildData(writer);
    }
public class ShaderTransparentGlassBlock : IBlock
{
private Flags  _flags;	
private Pad  __unnamed;	
private Pad  __unnamed2;	
private RealRGBColor _backgroundTintColor = new RealRGBColor();
private Real _backgroundTintMapScale = new Real();
private TagReference _backgroundTintMap = new TagReference();
private Pad  __unnamed3;	
private Pad  __unnamed4;	
private Enum _reflectionType = new Enum();
private RealFraction _perpendicularBrightness = new RealFraction();
private RealRGBColor _perpendicularTintColor = new RealRGBColor();
private RealFraction _parallelBrightness = new RealFraction();
private RealRGBColor _parallelTintColor = new RealRGBColor();
private TagReference _reflectionMap = new TagReference();
private Real _bumpMapScale = new Real();
private TagReference _bumpMap = new TagReference();
private Pad  __unnamed5;	
private Pad  __unnamed6;	
private Real _diffuseMapScale = new Real();
private TagReference _diffuseMap = new TagReference();
private Real _diffuseDetailMapScale = new Real();
private TagReference _diffuseDetailMap = new TagReference();
private Pad  __unnamed7;	
private Pad  __unnamed8;	
private Real _specularMapScale = new Real();
private TagReference _specularMap = new TagReference();
private Real _specularDetailMapScale = new Real();
private TagReference _specularDetailMap = new TagReference();
private Pad  __unnamed9;	
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public RealRGBColor BackgroundTintColor
{
  get { return _backgroundTintColor; }
  set { _backgroundTintColor = value; }
}
public Real BackgroundTintMapScale
{
  get { return _backgroundTintMapScale; }
  set { _backgroundTintMapScale = value; }
}
public TagReference BackgroundTintMap
{
  get { return _backgroundTintMap; }
  set { _backgroundTintMap = value; }
}
public Enum ReflectionType
{
  get { return _reflectionType; }
  set { _reflectionType = value; }
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
public TagReference ReflectionMap
{
  get { return _reflectionMap; }
  set { _reflectionMap = value; }
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
public Real DiffuseMapScale
{
  get { return _diffuseMapScale; }
  set { _diffuseMapScale = value; }
}
public TagReference DiffuseMap
{
  get { return _diffuseMap; }
  set { _diffuseMap = value; }
}
public Real DiffuseDetailMapScale
{
  get { return _diffuseDetailMapScale; }
  set { _diffuseDetailMapScale = value; }
}
public TagReference DiffuseDetailMap
{
  get { return _diffuseDetailMap; }
  set { _diffuseDetailMap = value; }
}
public Real SpecularMapScale
{
  get { return _specularMapScale; }
  set { _specularMapScale = value; }
}
public TagReference SpecularMap
{
  get { return _specularMap; }
  set { _specularMap = value; }
}
public Real SpecularDetailMapScale
{
  get { return _specularDetailMapScale; }
  set { _specularDetailMapScale = value; }
}
public TagReference SpecularDetailMap
{
  get { return _specularDetailMap; }
  set { _specularDetailMap = value; }
}
public ShaderTransparentGlassBlock()
{
_flags = new Flags(2);
__unnamed = new Pad(2);
__unnamed2 = new Pad(40);
__unnamed3 = new Pad(20);
__unnamed4 = new Pad(2);
__unnamed5 = new Pad(128);
__unnamed6 = new Pad(4);
__unnamed7 = new Pad(28);
__unnamed8 = new Pad(4);
__unnamed9 = new Pad(28);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  _backgroundTintColor.Read(reader);
  _backgroundTintMapScale.Read(reader);
  _backgroundTintMap.Read(reader);
  __unnamed3.Read(reader);
  __unnamed4.Read(reader);
  _reflectionType.Read(reader);
  _perpendicularBrightness.Read(reader);
  _perpendicularTintColor.Read(reader);
  _parallelBrightness.Read(reader);
  _parallelTintColor.Read(reader);
  _reflectionMap.Read(reader);
  _bumpMapScale.Read(reader);
  _bumpMap.Read(reader);
  __unnamed5.Read(reader);
  __unnamed6.Read(reader);
  _diffuseMapScale.Read(reader);
  _diffuseMap.Read(reader);
  _diffuseDetailMapScale.Read(reader);
  _diffuseDetailMap.Read(reader);
  __unnamed7.Read(reader);
  __unnamed8.Read(reader);
  _specularMapScale.Read(reader);
  _specularMap.Read(reader);
  _specularDetailMapScale.Read(reader);
  _specularDetailMap.Read(reader);
  __unnamed9.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_backgroundTintMap.ReadString(reader);
_reflectionMap.ReadString(reader);
_bumpMap.ReadString(reader);
_diffuseMap.ReadString(reader);
_diffuseDetailMap.ReadString(reader);
_specularMap.ReadString(reader);
_specularDetailMap.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    _backgroundTintColor.Write(writer);
    _backgroundTintMapScale.Write(writer);
    _backgroundTintMap.Write(writer);
    __unnamed3.Write(writer);
    __unnamed4.Write(writer);
    _reflectionType.Write(writer);
    _perpendicularBrightness.Write(writer);
    _perpendicularTintColor.Write(writer);
    _parallelBrightness.Write(writer);
    _parallelTintColor.Write(writer);
    _reflectionMap.Write(writer);
    _bumpMapScale.Write(writer);
    _bumpMap.Write(writer);
    __unnamed5.Write(writer);
    __unnamed6.Write(writer);
    _diffuseMapScale.Write(writer);
    _diffuseMap.Write(writer);
    _diffuseDetailMapScale.Write(writer);
    _diffuseDetailMap.Write(writer);
    __unnamed7.Write(writer);
    __unnamed8.Write(writer);
    _specularMapScale.Write(writer);
    _specularMap.Write(writer);
    _specularDetailMapScale.Write(writer);
    _specularDetailMap.Write(writer);
    __unnamed9.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_backgroundTintMap.WriteString(writer);
_reflectionMap.WriteString(writer);
_bumpMap.WriteString(writer);
_diffuseMap.WriteString(writer);
_diffuseDetailMap.WriteString(writer);
_specularMap.WriteString(writer);
_specularDetailMap.WriteString(writer);
}
}
  }
}
