using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class UnicodeStringList : IBlock
  {
    public UnicodeStringListBlock UnicodeStringListValues = new UnicodeStringListBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'UnicodeStringList'------------------------------------------------------");
      UnicodeStringListValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      UnicodeStringListValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      UnicodeStringListValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      UnicodeStringListValues.WriteChildData(writer);
    }
public class UnicodeStringListBlock : IBlock
{
private Block _stringReferences = new Block();
public class StringreferenceBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public StringreferenceBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(StringreferenceBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new StringreferenceBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public StringreferenceBlock this[int index]
  {
    get { return (InnerList[index] as StringreferenceBlock); }
  }
}
private StringreferenceBlockCollection _stringReferencesCollection;
public StringreferenceBlockCollection StringReferences
{
  get { return _stringReferencesCollection; }
}
public UnicodeStringListBlock()
{
_stringReferencesCollection = new StringreferenceBlockCollection(_stringReferences);

}
public void Read(BinaryReader reader)
{
  _stringReferences.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_stringReferences.Count; x++)
{
  StringReferences.AddNew();
  StringReferences[x].Read(reader);
}
for (int x=0; x<_stringReferences.Count; x++)
  StringReferences[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _stringReferences.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_stringReferences.UpdateReflexiveOffset(writer);
for (int x=0; x<_stringReferences.Count; x++)
{
  StringReferences[x].Write(writer);
}
for (int x=0; x<_stringReferences.Count; x++)
  StringReferences[x].WriteChildData(writer);
}
}
public class StringreferenceBlock : IBlock
{
private Data _string = new Data();
public Data String
{
  get { return _string; }
  set { _string = value; }
}
public StringreferenceBlock()
{

}
public void Read(BinaryReader reader)
{
  _string.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_string.ReadBinary(reader);
}
public void Write(BinaryWriter writer)
{
    _string.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_string.WriteBinary(writer);
}
}
  }
}
