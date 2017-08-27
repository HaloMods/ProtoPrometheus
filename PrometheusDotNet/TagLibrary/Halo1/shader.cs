using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Shader : IBlock
  {
    public ShaderBlock ShaderValues = new ShaderBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'Shader'------------------------------------------------------");
      ShaderValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      ShaderValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      ShaderValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      ShaderValues.WriteChildData(writer);
    }
public class ShaderBlock : IBlock
{
private Flags  _flags;	
private Enum _detailLevel = new Enum();
private Real _power = new Real();
private RealRGBColor _colorOfEmittedLight = new RealRGBColor();
private RealRGBColor _tintColor = new RealRGBColor();
private Flags  _flags2;	
private Enum _materialType = new Enum();
private Pad  __unnamed;	
private Pad  __unnamed2;	
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Enum DetailLevel
{
  get { return _detailLevel; }
  set { _detailLevel = value; }
}
public Real Power
{
  get { return _power; }
  set { _power = value; }
}
public RealRGBColor ColorOfEmittedLight
{
  get { return _colorOfEmittedLight; }
  set { _colorOfEmittedLight = value; }
}
public RealRGBColor TintColor
{
  get { return _tintColor; }
  set { _tintColor = value; }
}
public Flags Flags2
{
  get { return _flags2; }
  set { _flags2 = value; }
}
public Enum MaterialType
{
  get { return _materialType; }
  set { _materialType = value; }
}
public ShaderBlock()
{
_flags = new Flags(2);
_flags2 = new Flags(2);
__unnamed = new Pad(2);
__unnamed2 = new Pad(2);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _detailLevel.Read(reader);
  _power.Read(reader);
  _colorOfEmittedLight.Read(reader);
  _tintColor.Read(reader);
  _flags2.Read(reader);
  _materialType.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _detailLevel.Write(writer);
    _power.Write(writer);
    _colorOfEmittedLight.Write(writer);
    _tintColor.Write(writer);
    _flags2.Write(writer);
    _materialType.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
  }
}
