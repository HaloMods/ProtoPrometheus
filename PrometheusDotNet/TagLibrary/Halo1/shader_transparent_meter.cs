using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class ShaderTransparentMeter : Shader
  {
    public ShaderTransparentMeterBlock ShaderTransparentMeterValues = new ShaderTransparentMeterBlock();
    public new void Read(BinaryReader reader)
    {
      base.Read(reader);
      Trace.WriteLine("Loading 'ShaderTransparentMeter'------------------------------------------------------");
      ShaderTransparentMeterValues.Read(reader);
    }
    public new void ReadChildData(BinaryReader reader)
    {
      base.ReadChildData(reader);
      ShaderTransparentMeterValues.ReadChildData(reader);
    }
    public new void Write(BinaryWriter writer)
    {
      base.Write(writer);
      ShaderTransparentMeterValues.Write(writer);
    }
    public new void WriteChildData(BinaryWriter writer)
    {
      base.WriteChildData(writer);
      ShaderTransparentMeterValues.WriteChildData(writer);
    }
public class ShaderTransparentMeterBlock : IBlock
{
private Flags  _flags;	
private Pad  __unnamed;	
private Pad  __unnamed2;	
private TagReference _map = new TagReference();
private Pad  __unnamed3;	
private RealRGBColor _gradientMinColor = new RealRGBColor();
private RealRGBColor _gradientMaxColor = new RealRGBColor();
private RealRGBColor _backgroundColor = new RealRGBColor();
private RealRGBColor _flashColor = new RealRGBColor();
private RealRGBColor _tintColor = new RealRGBColor();
private RealFraction _meterTransparency = new RealFraction();
private RealFraction _backgroundTransparency = new RealFraction();
private Pad  __unnamed4;	
private Enum _meterBrightnessSource = new Enum();
private Enum _flashBrightnessSource = new Enum();
private Enum _valueSource = new Enum();
private Enum _gradientSource = new Enum();
private Enum _flas = new Enum();
private Pad  __unnamed5;	
private Pad  __unnamed6;	
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public TagReference Map
{
  get { return _map; }
  set { _map = value; }
}
public RealRGBColor GradientMinColor
{
  get { return _gradientMinColor; }
  set { _gradientMinColor = value; }
}
public RealRGBColor GradientMaxColor
{
  get { return _gradientMaxColor; }
  set { _gradientMaxColor = value; }
}
public RealRGBColor BackgroundColor
{
  get { return _backgroundColor; }
  set { _backgroundColor = value; }
}
public RealRGBColor FlashColor
{
  get { return _flashColor; }
  set { _flashColor = value; }
}
public RealRGBColor TintColor
{
  get { return _tintColor; }
  set { _tintColor = value; }
}
public RealFraction MeterTransparency
{
  get { return _meterTransparency; }
  set { _meterTransparency = value; }
}
public RealFraction BackgroundTransparency
{
  get { return _backgroundTransparency; }
  set { _backgroundTransparency = value; }
}
public Enum MeterBrightnessSource
{
  get { return _meterBrightnessSource; }
  set { _meterBrightnessSource = value; }
}
public Enum FlashBrightnessSource
{
  get { return _flashBrightnessSource; }
  set { _flashBrightnessSource = value; }
}
public Enum ValueSource
{
  get { return _valueSource; }
  set { _valueSource = value; }
}
public Enum GradientSource
{
  get { return _gradientSource; }
  set { _gradientSource = value; }
}
public Enum Flas
{
  get { return _flas; }
  set { _flas = value; }
}
public ShaderTransparentMeterBlock()
{
_flags = new Flags(2);
__unnamed = new Pad(2);
__unnamed2 = new Pad(32);
__unnamed3 = new Pad(32);
__unnamed4 = new Pad(24);
__unnamed5 = new Pad(2);
__unnamed6 = new Pad(32);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  _map.Read(reader);
  __unnamed3.Read(reader);
  _gradientMinColor.Read(reader);
  _gradientMaxColor.Read(reader);
  _backgroundColor.Read(reader);
  _flashColor.Read(reader);
  _tintColor.Read(reader);
  _meterTransparency.Read(reader);
  _backgroundTransparency.Read(reader);
  __unnamed4.Read(reader);
  _meterBrightnessSource.Read(reader);
  _flashBrightnessSource.Read(reader);
  _valueSource.Read(reader);
  _gradientSource.Read(reader);
  _flas.Read(reader);
  __unnamed5.Read(reader);
  __unnamed6.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_map.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    _map.Write(writer);
    __unnamed3.Write(writer);
    _gradientMinColor.Write(writer);
    _gradientMaxColor.Write(writer);
    _backgroundColor.Write(writer);
    _flashColor.Write(writer);
    _tintColor.Write(writer);
    _meterTransparency.Write(writer);
    _backgroundTransparency.Write(writer);
    __unnamed4.Write(writer);
    _meterBrightnessSource.Write(writer);
    _flashBrightnessSource.Write(writer);
    _valueSource.Write(writer);
    _gradientSource.Write(writer);
    _flas.Write(writer);
    __unnamed5.Write(writer);
    __unnamed6.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_map.WriteString(writer);
}
}
  }
}
