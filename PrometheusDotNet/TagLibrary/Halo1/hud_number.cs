using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class HudNumber : IBlock
  {
    public HudNumberBlock HudNumberValues = new HudNumberBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'HudNumber'------------------------------------------------------");
      HudNumberValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      HudNumberValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      HudNumberValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      HudNumberValues.WriteChildData(writer);
    }
public class HudNumberBlock : IBlock
{
private TagReference _digitsBitmap = new TagReference();
private CharInteger _bitmapDigitWidth = new CharInteger();
private CharInteger _screenDigitWidth = new CharInteger();
private CharInteger _xOffset = new CharInteger();
private CharInteger _yOffset = new CharInteger();
private CharInteger _decimalPointWidth = new CharInteger();
private CharInteger _colonWidth = new CharInteger();
private Pad  __unnamed;	
private Pad  __unnamed2;	
public TagReference DigitsBitmap
{
  get { return _digitsBitmap; }
  set { _digitsBitmap = value; }
}
public CharInteger BitmapDigitWidth
{
  get { return _bitmapDigitWidth; }
  set { _bitmapDigitWidth = value; }
}
public CharInteger ScreenDigitWidth
{
  get { return _screenDigitWidth; }
  set { _screenDigitWidth = value; }
}
public CharInteger XOffset
{
  get { return _xOffset; }
  set { _xOffset = value; }
}
public CharInteger YOffset
{
  get { return _yOffset; }
  set { _yOffset = value; }
}
public CharInteger DecimalPointWidth
{
  get { return _decimalPointWidth; }
  set { _decimalPointWidth = value; }
}
public CharInteger ColonWidth
{
  get { return _colonWidth; }
  set { _colonWidth = value; }
}
public HudNumberBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(76);

}
public void Read(BinaryReader reader)
{
  _digitsBitmap.Read(reader);
  _bitmapDigitWidth.Read(reader);
  _screenDigitWidth.Read(reader);
  _xOffset.Read(reader);
  _yOffset.Read(reader);
  _decimalPointWidth.Read(reader);
  _colonWidth.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_digitsBitmap.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _digitsBitmap.Write(writer);
    _bitmapDigitWidth.Write(writer);
    _screenDigitWidth.Write(writer);
    _xOffset.Write(writer);
    _yOffset.Write(writer);
    _decimalPointWidth.Write(writer);
    _colonWidth.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_digitsBitmap.WriteString(writer);
}
}
  }
}
