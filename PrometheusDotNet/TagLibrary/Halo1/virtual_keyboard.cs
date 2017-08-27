using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class VirtualKeyboard : IBlock
  {
    public VirtualKeyboardBlock VirtualKeyboardValues = new VirtualKeyboardBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'VirtualKeyboard'------------------------------------------------------");
      VirtualKeyboardValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      VirtualKeyboardValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      VirtualKeyboardValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      VirtualKeyboardValues.WriteChildData(writer);
    }
public class VirtualKeyboardBlock : IBlock
{
private TagReference _displayFont = new TagReference();
private TagReference _backgroundBitmap = new TagReference();
private TagReference _specialKeyLabelsStringList = new TagReference();
private Block _virtualKeys = new Block();
public class VirtualKeyBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public VirtualKeyBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(VirtualKeyBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new VirtualKeyBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public VirtualKeyBlock this[int index]
  {
    get { return (InnerList[index] as VirtualKeyBlock); }
  }
}
private VirtualKeyBlockCollection _virtualKeysCollection;
public VirtualKeyBlockCollection VirtualKeys
{
  get { return _virtualKeysCollection; }
}
public TagReference DisplayFont
{
  get { return _displayFont; }
  set { _displayFont = value; }
}
public TagReference BackgroundBitmap
{
  get { return _backgroundBitmap; }
  set { _backgroundBitmap = value; }
}
public TagReference SpecialKeyLabelsStringList
{
  get { return _specialKeyLabelsStringList; }
  set { _specialKeyLabelsStringList = value; }
}
public VirtualKeyboardBlock()
{
_virtualKeysCollection = new VirtualKeyBlockCollection(_virtualKeys);

}
public void Read(BinaryReader reader)
{
  _displayFont.Read(reader);
  _backgroundBitmap.Read(reader);
  _specialKeyLabelsStringList.Read(reader);
  _virtualKeys.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_displayFont.ReadString(reader);
_backgroundBitmap.ReadString(reader);
_specialKeyLabelsStringList.ReadString(reader);
for (int x=0; x<_virtualKeys.Count; x++)
{
  VirtualKeys.AddNew();
  VirtualKeys[x].Read(reader);
}
for (int x=0; x<_virtualKeys.Count; x++)
  VirtualKeys[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _displayFont.Write(writer);
    _backgroundBitmap.Write(writer);
    _specialKeyLabelsStringList.Write(writer);
    _virtualKeys.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_displayFont.WriteString(writer);
_backgroundBitmap.WriteString(writer);
_specialKeyLabelsStringList.WriteString(writer);
_virtualKeys.UpdateReflexiveOffset(writer);
for (int x=0; x<_virtualKeys.Count; x++)
{
  VirtualKeys[x].Write(writer);
}
for (int x=0; x<_virtualKeys.Count; x++)
  VirtualKeys[x].WriteChildData(writer);
}
}
public class VirtualKeyBlock : IBlock
{
private Enum _keyboardKey = new Enum();
private ShortInteger _lowercaseCharacter = new ShortInteger();
private ShortInteger _shiftCharacter = new ShortInteger();
private ShortInteger _capsCharacter = new ShortInteger();
private ShortInteger _symbolsCharacter = new ShortInteger();
private ShortInteger _shift_PluscapsCharacter = new ShortInteger();
private ShortInteger _shift_PlussymbolsCharacter = new ShortInteger();
private ShortInteger _caps_PlussymbolsCharacter = new ShortInteger();
private TagReference _unselectedBackgroundBitmap = new TagReference();
private TagReference _selectedBackgroundBitmap = new TagReference();
private TagReference _activeBackgroundBitmap = new TagReference();
private TagReference _stickyBackgroundBitmap = new TagReference();
public Enum KeyboardKey
{
  get { return _keyboardKey; }
  set { _keyboardKey = value; }
}
public ShortInteger LowercaseCharacter
{
  get { return _lowercaseCharacter; }
  set { _lowercaseCharacter = value; }
}
public ShortInteger ShiftCharacter
{
  get { return _shiftCharacter; }
  set { _shiftCharacter = value; }
}
public ShortInteger CapsCharacter
{
  get { return _capsCharacter; }
  set { _capsCharacter = value; }
}
public ShortInteger SymbolsCharacter
{
  get { return _symbolsCharacter; }
  set { _symbolsCharacter = value; }
}
public ShortInteger Shift_PluscapsCharacter
{
  get { return _shift_PluscapsCharacter; }
  set { _shift_PluscapsCharacter = value; }
}
public ShortInteger Shift_PlussymbolsCharacter
{
  get { return _shift_PlussymbolsCharacter; }
  set { _shift_PlussymbolsCharacter = value; }
}
public ShortInteger Caps_PlussymbolsCharacter
{
  get { return _caps_PlussymbolsCharacter; }
  set { _caps_PlussymbolsCharacter = value; }
}
public TagReference UnselectedBackgroundBitmap
{
  get { return _unselectedBackgroundBitmap; }
  set { _unselectedBackgroundBitmap = value; }
}
public TagReference SelectedBackgroundBitmap
{
  get { return _selectedBackgroundBitmap; }
  set { _selectedBackgroundBitmap = value; }
}
public TagReference ActiveBackgroundBitmap
{
  get { return _activeBackgroundBitmap; }
  set { _activeBackgroundBitmap = value; }
}
public TagReference StickyBackgroundBitmap
{
  get { return _stickyBackgroundBitmap; }
  set { _stickyBackgroundBitmap = value; }
}
public VirtualKeyBlock()
{

}
public void Read(BinaryReader reader)
{
  _keyboardKey.Read(reader);
  _lowercaseCharacter.Read(reader);
  _shiftCharacter.Read(reader);
  _capsCharacter.Read(reader);
  _symbolsCharacter.Read(reader);
  _shift_PluscapsCharacter.Read(reader);
  _shift_PlussymbolsCharacter.Read(reader);
  _caps_PlussymbolsCharacter.Read(reader);
  _unselectedBackgroundBitmap.Read(reader);
  _selectedBackgroundBitmap.Read(reader);
  _activeBackgroundBitmap.Read(reader);
  _stickyBackgroundBitmap.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_unselectedBackgroundBitmap.ReadString(reader);
_selectedBackgroundBitmap.ReadString(reader);
_activeBackgroundBitmap.ReadString(reader);
_stickyBackgroundBitmap.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _keyboardKey.Write(writer);
    _lowercaseCharacter.Write(writer);
    _shiftCharacter.Write(writer);
    _capsCharacter.Write(writer);
    _symbolsCharacter.Write(writer);
    _shift_PluscapsCharacter.Write(writer);
    _shift_PlussymbolsCharacter.Write(writer);
    _caps_PlussymbolsCharacter.Write(writer);
    _unselectedBackgroundBitmap.Write(writer);
    _selectedBackgroundBitmap.Write(writer);
    _activeBackgroundBitmap.Write(writer);
    _stickyBackgroundBitmap.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_unselectedBackgroundBitmap.WriteString(writer);
_selectedBackgroundBitmap.WriteString(writer);
_activeBackgroundBitmap.WriteString(writer);
_stickyBackgroundBitmap.WriteString(writer);
}
}
  }
}
