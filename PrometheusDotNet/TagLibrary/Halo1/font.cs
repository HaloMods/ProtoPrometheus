using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Font : IBlock
  {
    public FontBlock FontValues = new FontBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'Font'------------------------------------------------------");
      FontValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      FontValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      FontValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      FontValues.WriteChildData(writer);
    }
public class FontBlock : IBlock
{
private LongInteger _flags = new LongInteger();
private ShortInteger _ascendingHeight = new ShortInteger();
private ShortInteger _decendingHeight = new ShortInteger();
private ShortInteger _leadingHeight = new ShortInteger();
private ShortInteger _leadinWidth = new ShortInteger();
private Pad  __unnamed;	
private Block _characterTables = new Block();
private TagReference _bold = new TagReference();
private TagReference _italic = new TagReference();
private TagReference _condense = new TagReference();
private TagReference _underline = new TagReference();
private Block _characters = new Block();
private Data _pixels = new Data();
public class FontCharacterTablesBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public FontCharacterTablesBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(FontCharacterTablesBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new FontCharacterTablesBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public FontCharacterTablesBlock this[int index]
  {
    get { return (InnerList[index] as FontCharacterTablesBlock); }
  }
}
private FontCharacterTablesBlockCollection _characterTablesCollection;
public FontCharacterTablesBlockCollection CharacterTables
{
  get { return _characterTablesCollection; }
}
public class CharacterBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public CharacterBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(CharacterBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new CharacterBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public CharacterBlock this[int index]
  {
    get { return (InnerList[index] as CharacterBlock); }
  }
}
private CharacterBlockCollection _charactersCollection;
public CharacterBlockCollection Characters
{
  get { return _charactersCollection; }
}
public LongInteger Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public ShortInteger AscendingHeight
{
  get { return _ascendingHeight; }
  set { _ascendingHeight = value; }
}
public ShortInteger DecendingHeight
{
  get { return _decendingHeight; }
  set { _decendingHeight = value; }
}
public ShortInteger LeadingHeight
{
  get { return _leadingHeight; }
  set { _leadingHeight = value; }
}
public ShortInteger LeadinWidth
{
  get { return _leadinWidth; }
  set { _leadinWidth = value; }
}
public TagReference Bold
{
  get { return _bold; }
  set { _bold = value; }
}
public TagReference Italic
{
  get { return _italic; }
  set { _italic = value; }
}
public TagReference Condense
{
  get { return _condense; }
  set { _condense = value; }
}
public TagReference Underline
{
  get { return _underline; }
  set { _underline = value; }
}
public Data Pixels
{
  get { return _pixels; }
  set { _pixels = value; }
}
public FontBlock()
{
__unnamed = new Pad(36);
_characterTablesCollection = new FontCharacterTablesBlockCollection(_characterTables);
_charactersCollection = new CharacterBlockCollection(_characters);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _ascendingHeight.Read(reader);
  _decendingHeight.Read(reader);
  _leadingHeight.Read(reader);
  _leadinWidth.Read(reader);
  __unnamed.Read(reader);
  _characterTables.Read(reader);
  _bold.Read(reader);
  _italic.Read(reader);
  _condense.Read(reader);
  _underline.Read(reader);
  _characters.Read(reader);
  _pixels.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_characterTables.Count; x++)
{
  CharacterTables.AddNew();
  CharacterTables[x].Read(reader);
}
for (int x=0; x<_characterTables.Count; x++)
  CharacterTables[x].ReadChildData(reader);
_bold.ReadString(reader);
_italic.ReadString(reader);
_condense.ReadString(reader);
_underline.ReadString(reader);
for (int x=0; x<_characters.Count; x++)
{
  Characters.AddNew();
  Characters[x].Read(reader);
}
for (int x=0; x<_characters.Count; x++)
  Characters[x].ReadChildData(reader);
_pixels.ReadBinary(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _ascendingHeight.Write(writer);
    _decendingHeight.Write(writer);
    _leadingHeight.Write(writer);
    _leadinWidth.Write(writer);
    __unnamed.Write(writer);
    _characterTables.Write(writer);
    _bold.Write(writer);
    _italic.Write(writer);
    _condense.Write(writer);
    _underline.Write(writer);
    _characters.Write(writer);
    _pixels.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_characterTables.UpdateReflexiveOffset(writer);
for (int x=0; x<_characterTables.Count; x++)
{
  CharacterTables[x].Write(writer);
}
for (int x=0; x<_characterTables.Count; x++)
  CharacterTables[x].WriteChildData(writer);
_bold.WriteString(writer);
_italic.WriteString(writer);
_condense.WriteString(writer);
_underline.WriteString(writer);
_characters.UpdateReflexiveOffset(writer);
for (int x=0; x<_characters.Count; x++)
{
  Characters[x].Write(writer);
}
for (int x=0; x<_characters.Count; x++)
  Characters[x].WriteChildData(writer);
_pixels.WriteBinary(writer);
}
}
public class FontCharacterTablesBlock : IBlock
{
private Block _characterTable = new Block();
public class FontCharacterTableBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public FontCharacterTableBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(FontCharacterTableBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new FontCharacterTableBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public FontCharacterTableBlock this[int index]
  {
    get { return (InnerList[index] as FontCharacterTableBlock); }
  }
}
private FontCharacterTableBlockCollection _characterTableCollection;
public FontCharacterTableBlockCollection CharacterTable
{
  get { return _characterTableCollection; }
}
public FontCharacterTablesBlock()
{
_characterTableCollection = new FontCharacterTableBlockCollection(_characterTable);

}
public void Read(BinaryReader reader)
{
  _characterTable.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_characterTable.Count; x++)
{
  CharacterTable.AddNew();
  CharacterTable[x].Read(reader);
}
for (int x=0; x<_characterTable.Count; x++)
  CharacterTable[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _characterTable.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_characterTable.UpdateReflexiveOffset(writer);
for (int x=0; x<_characterTable.Count; x++)
{
  CharacterTable[x].Write(writer);
}
for (int x=0; x<_characterTable.Count; x++)
  CharacterTable[x].WriteChildData(writer);
}
}
public class FontCharacterTableBlock : IBlock
{
private ShortBlockIndex _characterIndex = new ShortBlockIndex();
public ShortBlockIndex CharacterIndex
{
  get { return _characterIndex; }
  set { _characterIndex = value; }
}
public FontCharacterTableBlock()
{

}
public void Read(BinaryReader reader)
{
  _characterIndex.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _characterIndex.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class CharacterBlock : IBlock
{
private ShortInteger _character = new ShortInteger();
private ShortInteger _characterWidth = new ShortInteger();
private ShortInteger _bitmapWidth = new ShortInteger();
private ShortInteger _bitmapHeight = new ShortInteger();
private ShortInteger _bitmapOriginX = new ShortInteger();
private ShortInteger _bitmapOriginY = new ShortInteger();
private ShortInteger _hardwareCharacterIndex = new ShortInteger();
private Pad  __unnamed;	
private LongInteger _pixelsOffset = new LongInteger();
public ShortInteger Character
{
  get { return _character; }
  set { _character = value; }
}
public ShortInteger CharacterWidth
{
  get { return _characterWidth; }
  set { _characterWidth = value; }
}
public ShortInteger BitmapWidth
{
  get { return _bitmapWidth; }
  set { _bitmapWidth = value; }
}
public ShortInteger BitmapHeight
{
  get { return _bitmapHeight; }
  set { _bitmapHeight = value; }
}
public ShortInteger BitmapOriginX
{
  get { return _bitmapOriginX; }
  set { _bitmapOriginX = value; }
}
public ShortInteger BitmapOriginY
{
  get { return _bitmapOriginY; }
  set { _bitmapOriginY = value; }
}
public ShortInteger HardwareCharacterIndex
{
  get { return _hardwareCharacterIndex; }
  set { _hardwareCharacterIndex = value; }
}
public LongInteger PixelsOffset
{
  get { return _pixelsOffset; }
  set { _pixelsOffset = value; }
}
public CharacterBlock()
{
__unnamed = new Pad(2);

}
public void Read(BinaryReader reader)
{
  _character.Read(reader);
  _characterWidth.Read(reader);
  _bitmapWidth.Read(reader);
  _bitmapHeight.Read(reader);
  _bitmapOriginX.Read(reader);
  _bitmapOriginY.Read(reader);
  _hardwareCharacterIndex.Read(reader);
  __unnamed.Read(reader);
  _pixelsOffset.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _character.Write(writer);
    _characterWidth.Write(writer);
    _bitmapWidth.Write(writer);
    _bitmapHeight.Write(writer);
    _bitmapOriginX.Write(writer);
    _bitmapOriginY.Write(writer);
    _hardwareCharacterIndex.Write(writer);
    __unnamed.Write(writer);
    _pixelsOffset.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
  }
}
