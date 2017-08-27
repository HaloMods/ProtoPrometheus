using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Bitmap : IBlock
  {
    public BitmapBlock BitmapValues = new BitmapBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'Bitmap'------------------------------------------------------");
      BitmapValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      BitmapValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      BitmapValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      BitmapValues.WriteChildData(writer);
    }
public class BitmapBlock : IBlock
{
private Enum _type = new Enum();
private Enum _format = new Enum();
private Enum _usage = new Enum();
private Flags  _flags;	
private RealFraction _detailFadeFactor = new RealFraction();
private RealFraction _sharpenAmount = new RealFraction();
private RealFraction _bumpHeight = new RealFraction();
private Enum _spriteBudgetSize = new Enum();
private ShortInteger _spriteBudgetCount = new ShortInteger();
private ShortInteger _colorPlateWidth = new ShortInteger();
private ShortInteger _colorPlateHeight = new ShortInteger();
private Data _compressedColorPlateData = new Data();
private Data _processedPixelData = new Data();
private Real _blurFilterSize = new Real();
private Real _alphaBias = new Real();
private ShortInteger _mipmapCount = new ShortInteger();
private Enum _spriteUsage = new Enum();
private ShortInteger _spriteSpacing = new ShortInteger();
private Pad  __unnamed2;	
private Block _sequences = new Block();
private Block _bitmaps = new Block();
public class BitmapGroupSequenceBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public BitmapGroupSequenceBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(BitmapGroupSequenceBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new BitmapGroupSequenceBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public BitmapGroupSequenceBlock this[int index]
  {
    get { return (InnerList[index] as BitmapGroupSequenceBlock); }
  }
}
private BitmapGroupSequenceBlockCollection _sequencesCollection;
public BitmapGroupSequenceBlockCollection Sequences
{
  get { return _sequencesCollection; }
}
public class BitmapDataBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public BitmapDataBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(BitmapDataBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new BitmapDataBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public BitmapDataBlock this[int index]
  {
    get { return (InnerList[index] as BitmapDataBlock); }
  }
}
private BitmapDataBlockCollection _bitmapsCollection;
public BitmapDataBlockCollection Bitmaps
{
  get { return _bitmapsCollection; }
}
public Enum Type
{
  get { return _type; }
  set { _type = value; }
}
public Enum Format
{
  get { return _format; }
  set { _format = value; }
}
public Enum Usage
{
  get { return _usage; }
  set { _usage = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public RealFraction DetailFadeFactor
{
  get { return _detailFadeFactor; }
  set { _detailFadeFactor = value; }
}
public RealFraction SharpenAmount
{
  get { return _sharpenAmount; }
  set { _sharpenAmount = value; }
}
public RealFraction BumpHeight
{
  get { return _bumpHeight; }
  set { _bumpHeight = value; }
}
public Enum SpriteBudgetSize
{
  get { return _spriteBudgetSize; }
  set { _spriteBudgetSize = value; }
}
public ShortInteger SpriteBudgetCount
{
  get { return _spriteBudgetCount; }
  set { _spriteBudgetCount = value; }
}
public ShortInteger ColorPlateWidth
{
  get { return _colorPlateWidth; }
  set { _colorPlateWidth = value; }
}
public ShortInteger ColorPlateHeight
{
  get { return _colorPlateHeight; }
  set { _colorPlateHeight = value; }
}
public Data CompressedColorPlateData
{
  get { return _compressedColorPlateData; }
  set { _compressedColorPlateData = value; }
}
public Data ProcessedPixelData
{
  get { return _processedPixelData; }
  set { _processedPixelData = value; }
}
public Real BlurFilterSize
{
  get { return _blurFilterSize; }
  set { _blurFilterSize = value; }
}
public Real AlphaBias
{
  get { return _alphaBias; }
  set { _alphaBias = value; }
}
public ShortInteger MipmapCount
{
  get { return _mipmapCount; }
  set { _mipmapCount = value; }
}
public Enum SpriteUsage
{
  get { return _spriteUsage; }
  set { _spriteUsage = value; }
}
public ShortInteger SpriteSpacing
{
  get { return _spriteSpacing; }
  set { _spriteSpacing = value; }
}
public BitmapBlock()
{
_flags = new Flags(2);
__unnamed2 = new Pad(2);
_sequencesCollection = new BitmapGroupSequenceBlockCollection(_sequences);
_bitmapsCollection = new BitmapDataBlockCollection(_bitmaps);

}
public void Read(BinaryReader reader)
{
  _type.Read(reader);
  _format.Read(reader);
  _usage.Read(reader);
  _flags.Read(reader);
  _detailFadeFactor.Read(reader);
  _sharpenAmount.Read(reader);
  _bumpHeight.Read(reader);
  _spriteBudgetSize.Read(reader);
  _spriteBudgetCount.Read(reader);
  _colorPlateWidth.Read(reader);
  _colorPlateHeight.Read(reader);
  _compressedColorPlateData.Read(reader);
  _processedPixelData.Read(reader);
  _blurFilterSize.Read(reader);
  _alphaBias.Read(reader);
  _mipmapCount.Read(reader);
  _spriteUsage.Read(reader);
  _spriteSpacing.Read(reader);
  __unnamed2.Read(reader);
  _sequences.Read(reader);
  _bitmaps.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_compressedColorPlateData.ReadBinary(reader);
_processedPixelData.ReadBinary(reader);
for (int x=0; x<_sequences.Count; x++)
{
  Sequences.AddNew();
  Sequences[x].Read(reader);
}
for (int x=0; x<_sequences.Count; x++)
  Sequences[x].ReadChildData(reader);
for (int x=0; x<_bitmaps.Count; x++)
{
  Bitmaps.AddNew();
  Bitmaps[x].Read(reader);
}
for (int x=0; x<_bitmaps.Count; x++)
  Bitmaps[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _type.Write(writer);
    _format.Write(writer);
    _usage.Write(writer);
    _flags.Write(writer);
    _detailFadeFactor.Write(writer);
    _sharpenAmount.Write(writer);
    _bumpHeight.Write(writer);
    _spriteBudgetSize.Write(writer);
    _spriteBudgetCount.Write(writer);
    _colorPlateWidth.Write(writer);
    _colorPlateHeight.Write(writer);
    _compressedColorPlateData.Write(writer);
    _processedPixelData.Write(writer);
    _blurFilterSize.Write(writer);
    _alphaBias.Write(writer);
    _mipmapCount.Write(writer);
    _spriteUsage.Write(writer);
    _spriteSpacing.Write(writer);
    __unnamed2.Write(writer);
    _sequences.Write(writer);
    _bitmaps.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_compressedColorPlateData.WriteBinary(writer);
_processedPixelData.WriteBinary(writer);
_sequences.UpdateReflexiveOffset(writer);
for (int x=0; x<_sequences.Count; x++)
{
  Sequences[x].Write(writer);
}
for (int x=0; x<_sequences.Count; x++)
  Sequences[x].WriteChildData(writer);
_bitmaps.UpdateReflexiveOffset(writer);
for (int x=0; x<_bitmaps.Count; x++)
{
  Bitmaps[x].Write(writer);
}
for (int x=0; x<_bitmaps.Count; x++)
  Bitmaps[x].WriteChildData(writer);
}
}
public class BitmapGroupSequenceBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private ShortInteger _firstBitmapIndex = new ShortInteger();
private ShortInteger _bitmapCount = new ShortInteger();
private Pad  __unnamed;	
private Block _sprites = new Block();
public class BitmapGroupSpriteBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public BitmapGroupSpriteBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(BitmapGroupSpriteBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new BitmapGroupSpriteBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public BitmapGroupSpriteBlock this[int index]
  {
    get { return (InnerList[index] as BitmapGroupSpriteBlock); }
  }
}
private BitmapGroupSpriteBlockCollection _spritesCollection;
public BitmapGroupSpriteBlockCollection Sprites
{
  get { return _spritesCollection; }
}
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public ShortInteger FirstBitmapIndex
{
  get { return _firstBitmapIndex; }
  set { _firstBitmapIndex = value; }
}
public ShortInteger BitmapCount
{
  get { return _bitmapCount; }
  set { _bitmapCount = value; }
}
public BitmapGroupSequenceBlock()
{
__unnamed = new Pad(16);
_spritesCollection = new BitmapGroupSpriteBlockCollection(_sprites);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _firstBitmapIndex.Read(reader);
  _bitmapCount.Read(reader);
  __unnamed.Read(reader);
  _sprites.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_sprites.Count; x++)
{
  Sprites.AddNew();
  Sprites[x].Read(reader);
}
for (int x=0; x<_sprites.Count; x++)
  Sprites[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _firstBitmapIndex.Write(writer);
    _bitmapCount.Write(writer);
    __unnamed.Write(writer);
    _sprites.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_sprites.UpdateReflexiveOffset(writer);
for (int x=0; x<_sprites.Count; x++)
{
  Sprites[x].Write(writer);
}
for (int x=0; x<_sprites.Count; x++)
  Sprites[x].WriteChildData(writer);
}
}
public class BitmapGroupSpriteBlock : IBlock
{
private ShortInteger _bitmapIndex = new ShortInteger();
private Pad  __unnamed;	
private Pad  __unnamed2;	
private Real _left = new Real();
private Real _right = new Real();
private Real _top = new Real();
private Real _bottom = new Real();
private RealPoint2D _registrationPoint = new RealPoint2D();
public ShortInteger BitmapIndex
{
  get { return _bitmapIndex; }
  set { _bitmapIndex = value; }
}
public Real Left
{
  get { return _left; }
  set { _left = value; }
}
public Real Right
{
  get { return _right; }
  set { _right = value; }
}
public Real Top
{
  get { return _top; }
  set { _top = value; }
}
public Real Bottom
{
  get { return _bottom; }
  set { _bottom = value; }
}
public RealPoint2D RegistrationPoint
{
  get { return _registrationPoint; }
  set { _registrationPoint = value; }
}
public BitmapGroupSpriteBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(4);

}
public void Read(BinaryReader reader)
{
  _bitmapIndex.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  _left.Read(reader);
  _right.Read(reader);
  _top.Read(reader);
  _bottom.Read(reader);
  _registrationPoint.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _bitmapIndex.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    _left.Write(writer);
    _right.Write(writer);
    _top.Write(writer);
    _bottom.Write(writer);
    _registrationPoint.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class BitmapDataBlock : IBlock
{
private TagDefinition _signature = new TagDefinition();
private ShortInteger _width = new ShortInteger();
private ShortInteger _height = new ShortInteger();
private ShortInteger _depth = new ShortInteger();
private Enum _type = new Enum();
private Enum _format = new Enum();
private Flags  _flags;	
private Point2D _registrationPoint = new Point2D();
private ShortInteger _mipmapCount = new ShortInteger();
private Pad  __unnamed;	
private LongInteger _pixelsOffset = new LongInteger();
private Pad  __unnamed2;	
private Pad  __unnamed3;	
private Pad  __unnamed4;	
private Pad  __unnamed5;	
private LongInteger _baseAddress = new LongInteger();
public TagDefinition Signature
{
  get { return _signature; }
  set { _signature = value; }
}
public ShortInteger Width
{
  get { return _width; }
  set { _width = value; }
}
public ShortInteger Height
{
  get { return _height; }
  set { _height = value; }
}
public ShortInteger Depth
{
  get { return _depth; }
  set { _depth = value; }
}
public Enum Type
{
  get { return _type; }
  set { _type = value; }
}
public Enum Format
{
  get { return _format; }
  set { _format = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Point2D RegistrationPoint
{
  get { return _registrationPoint; }
  set { _registrationPoint = value; }
}
public ShortInteger MipmapCount
{
  get { return _mipmapCount; }
  set { _mipmapCount = value; }
}
public LongInteger PixelsOffset
{
  get { return _pixelsOffset; }
  set { _pixelsOffset = value; }
}
public LongInteger BaseAddress
{
  get { return _baseAddress; }
  set { _baseAddress = value; }
}
public BitmapDataBlock()
{
_flags = new Flags(2);
__unnamed = new Pad(2);
__unnamed2 = new Pad(4);
__unnamed3 = new Pad(4);
__unnamed4 = new Pad(4);
__unnamed5 = new Pad(4);

}
public void Read(BinaryReader reader)
{
  _signature.Read(reader);
  _width.Read(reader);
  _height.Read(reader);
  _depth.Read(reader);
  _type.Read(reader);
  _format.Read(reader);
  _flags.Read(reader);
  _registrationPoint.Read(reader);
  _mipmapCount.Read(reader);
  __unnamed.Read(reader);
  _pixelsOffset.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  __unnamed4.Read(reader);
  __unnamed5.Read(reader);
  _baseAddress.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _signature.Write(writer);
    _width.Write(writer);
    _height.Write(writer);
    _depth.Write(writer);
    _type.Write(writer);
    _format.Write(writer);
    _flags.Write(writer);
    _registrationPoint.Write(writer);
    _mipmapCount.Write(writer);
    __unnamed.Write(writer);
    _pixelsOffset.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    __unnamed4.Write(writer);
    __unnamed5.Write(writer);
    _baseAddress.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
  }
}
