using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class DetailObjectCollection : IBlock
  {
    public DetailObjectCollectionBlock DetailObjectCollectionValues = new DetailObjectCollectionBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'DetailObjectCollection'------------------------------------------------------");
      DetailObjectCollectionValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      DetailObjectCollectionValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      DetailObjectCollectionValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      DetailObjectCollectionValues.WriteChildData(writer);
    }
public class DetailObjectCollectionBlock : IBlock
{
private Enum _collectionType = new Enum();
private Pad  __unnamed;	
private Real _globalZOffset = new Real();
private Pad  __unnamed2;	
private TagReference _spritePlate = new TagReference();
private Block _types = new Block();
private Pad  __unnamed3;	
public class DetailObjectTypeBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public DetailObjectTypeBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(DetailObjectTypeBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new DetailObjectTypeBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public DetailObjectTypeBlock this[int index]
  {
    get { return (InnerList[index] as DetailObjectTypeBlock); }
  }
}
private DetailObjectTypeBlockCollection _typesCollection;
public DetailObjectTypeBlockCollection Types
{
  get { return _typesCollection; }
}
public Enum CollectionType
{
  get { return _collectionType; }
  set { _collectionType = value; }
}
public Real GlobalZOffset
{
  get { return _globalZOffset; }
  set { _globalZOffset = value; }
}
public TagReference SpritePlate
{
  get { return _spritePlate; }
  set { _spritePlate = value; }
}
public DetailObjectCollectionBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(44);
__unnamed3 = new Pad(48);
_typesCollection = new DetailObjectTypeBlockCollection(_types);

}
public void Read(BinaryReader reader)
{
  _collectionType.Read(reader);
  __unnamed.Read(reader);
  _globalZOffset.Read(reader);
  __unnamed2.Read(reader);
  _spritePlate.Read(reader);
  _types.Read(reader);
  __unnamed3.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_spritePlate.ReadString(reader);
for (int x=0; x<_types.Count; x++)
{
  Types.AddNew();
  Types[x].Read(reader);
}
for (int x=0; x<_types.Count; x++)
  Types[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _collectionType.Write(writer);
    __unnamed.Write(writer);
    _globalZOffset.Write(writer);
    __unnamed2.Write(writer);
    _spritePlate.Write(writer);
    _types.Write(writer);
    __unnamed3.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_spritePlate.WriteString(writer);
_types.UpdateReflexiveOffset(writer);
for (int x=0; x<_types.Count; x++)
{
  Types[x].Write(writer);
}
for (int x=0; x<_types.Count; x++)
  Types[x].WriteChildData(writer);
}
}
public class DetailObjectTypeBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private CharInteger _sequenceIndex = new CharInteger();
private Flags  _typeFlags;	
private Pad  __unnamed;	
private RealFraction _colorOverrideFactor = new RealFraction();
private Pad  __unnamed2;	
private Real _nearFadeDistance = new Real();
private Real _farFadeDistance = new Real();
private Real _size = new Real();
private Pad  __unnamed3;	
private RealRGBColor _minimumColor = new RealRGBColor();
private RealRGBColor _maximumColor = new RealRGBColor();
private ARGBColor _ambientColor = new ARGBColor();
private Pad  __unnamed4;	
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public CharInteger SequenceIndex
{
  get { return _sequenceIndex; }
  set { _sequenceIndex = value; }
}
public Flags TypeFlags
{
  get { return _typeFlags; }
  set { _typeFlags = value; }
}
public RealFraction ColorOverrideFactor
{
  get { return _colorOverrideFactor; }
  set { _colorOverrideFactor = value; }
}
public Real NearFadeDistance
{
  get { return _nearFadeDistance; }
  set { _nearFadeDistance = value; }
}
public Real FarFadeDistance
{
  get { return _farFadeDistance; }
  set { _farFadeDistance = value; }
}
public Real Size
{
  get { return _size; }
  set { _size = value; }
}
public RealRGBColor MinimumColor
{
  get { return _minimumColor; }
  set { _minimumColor = value; }
}
public RealRGBColor MaximumColor
{
  get { return _maximumColor; }
  set { _maximumColor = value; }
}
public ARGBColor AmbientColor
{
  get { return _ambientColor; }
  set { _ambientColor = value; }
}
public DetailObjectTypeBlock()
{
_typeFlags = new Flags(1);
__unnamed = new Pad(2);
__unnamed2 = new Pad(8);
__unnamed3 = new Pad(4);
__unnamed4 = new Pad(4);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _sequenceIndex.Read(reader);
  _typeFlags.Read(reader);
  __unnamed.Read(reader);
  _colorOverrideFactor.Read(reader);
  __unnamed2.Read(reader);
  _nearFadeDistance.Read(reader);
  _farFadeDistance.Read(reader);
  _size.Read(reader);
  __unnamed3.Read(reader);
  _minimumColor.Read(reader);
  _maximumColor.Read(reader);
  _ambientColor.Read(reader);
  __unnamed4.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _sequenceIndex.Write(writer);
    _typeFlags.Write(writer);
    __unnamed.Write(writer);
    _colorOverrideFactor.Write(writer);
    __unnamed2.Write(writer);
    _nearFadeDistance.Write(writer);
    _farFadeDistance.Write(writer);
    _size.Write(writer);
    __unnamed3.Write(writer);
    _minimumColor.Write(writer);
    _maximumColor.Write(writer);
    _ambientColor.Write(writer);
    __unnamed4.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
  }
}
