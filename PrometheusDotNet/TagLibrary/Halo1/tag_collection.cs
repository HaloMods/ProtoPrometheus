using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class TagCollection : IBlock
  {
    public TagCollectionBlock TagCollectionValues = new TagCollectionBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'TagCollection'------------------------------------------------------");
      TagCollectionValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      TagCollectionValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      TagCollectionValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      TagCollectionValues.WriteChildData(writer);
    }
public class TagCollectionBlock : IBlock
{
private Block _tagReferences = new Block();
public class TagreferenceBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public TagreferenceBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(TagreferenceBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new TagreferenceBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public TagreferenceBlock this[int index]
  {
    get { return (InnerList[index] as TagreferenceBlock); }
  }
}
private TagreferenceBlockCollection _tagReferencesCollection;
public TagreferenceBlockCollection TagReferences
{
  get { return _tagReferencesCollection; }
}
public TagCollectionBlock()
{
_tagReferencesCollection = new TagreferenceBlockCollection(_tagReferences);

}
public void Read(BinaryReader reader)
{
  _tagReferences.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_tagReferences.Count; x++)
{
  TagReferences.AddNew();
  TagReferences[x].Read(reader);
}
for (int x=0; x<_tagReferences.Count; x++)
  TagReferences[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _tagReferences.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_tagReferences.UpdateReflexiveOffset(writer);
for (int x=0; x<_tagReferences.Count; x++)
{
  TagReferences[x].Write(writer);
}
for (int x=0; x<_tagReferences.Count; x++)
  TagReferences[x].WriteChildData(writer);
}
}
public class TagreferenceBlock : IBlock
{
private TagReference _tag = new TagReference();
public TagReference Tag
{
  get { return _tag; }
  set { _tag = value; }
}
public TagreferenceBlock()
{

}
public void Read(BinaryReader reader)
{
  _tag.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_tag.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _tag.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_tag.WriteString(writer);
}
}
  }
}
