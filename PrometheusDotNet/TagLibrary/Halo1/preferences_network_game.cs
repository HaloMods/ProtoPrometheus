using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class PreferencesNetworkGame : IBlock
  {
    public PreferencesNetworkGameBlock PreferencesNetworkGameValues = new PreferencesNetworkGameBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'PreferencesNetworkGame'------------------------------------------------------");
      PreferencesNetworkGameValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      PreferencesNetworkGameValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      PreferencesNetworkGameValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      PreferencesNetworkGameValues.WriteChildData(writer);
    }
public class PreferencesNetworkGameBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private RealRGBColor _primaryColor = new RealRGBColor();
private RealRGBColor _secondaryColor = new RealRGBColor();
private TagReference _pattern = new TagReference();
private ShortInteger _bitmapIndex = new ShortInteger();
private Pad  __unnamed;	
private TagReference _decal = new TagReference();
private ShortInteger _bitmapIndex2 = new ShortInteger();
private Pad  __unnamed2;	
private Pad  __unnamed3;	
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public RealRGBColor PrimaryColor
{
  get { return _primaryColor; }
  set { _primaryColor = value; }
}
public RealRGBColor SecondaryColor
{
  get { return _secondaryColor; }
  set { _secondaryColor = value; }
}
public TagReference Pattern
{
  get { return _pattern; }
  set { _pattern = value; }
}
public ShortInteger BitmapIndex
{
  get { return _bitmapIndex; }
  set { _bitmapIndex = value; }
}
public TagReference Decal
{
  get { return _decal; }
  set { _decal = value; }
}
public ShortInteger BitmapIndex2
{
  get { return _bitmapIndex2; }
  set { _bitmapIndex2 = value; }
}
public PreferencesNetworkGameBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(2);
__unnamed3 = new Pad(800);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _primaryColor.Read(reader);
  _secondaryColor.Read(reader);
  _pattern.Read(reader);
  _bitmapIndex.Read(reader);
  __unnamed.Read(reader);
  _decal.Read(reader);
  _bitmapIndex2.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_pattern.ReadString(reader);
_decal.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _primaryColor.Write(writer);
    _secondaryColor.Write(writer);
    _pattern.Write(writer);
    _bitmapIndex.Write(writer);
    __unnamed.Write(writer);
    _decal.Write(writer);
    _bitmapIndex2.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_pattern.WriteString(writer);
_decal.WriteString(writer);
}
}
  }
}
