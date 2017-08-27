using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class ItemCollection : IBlock
  {
    public ItemCollectionBlock ItemCollectionValues = new ItemCollectionBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'ItemCollection'------------------------------------------------------");
      ItemCollectionValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      ItemCollectionValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      ItemCollectionValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      ItemCollectionValues.WriteChildData(writer);
    }
public class ItemCollectionBlock : IBlock
{
private Block _itemPermutations = new Block();
private ShortInteger _spawnTimeInSecond = new ShortInteger();
private Pad  __unnamed;	
private Pad  __unnamed2;	
public class ItemPermutationBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ItemPermutationBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ItemPermutationBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ItemPermutationBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ItemPermutationBlock this[int index]
  {
    get { return (InnerList[index] as ItemPermutationBlock); }
  }
}
private ItemPermutationBlockCollection _itemPermutationsCollection;
public ItemPermutationBlockCollection ItemPermutations
{
  get { return _itemPermutationsCollection; }
}
public ShortInteger SpawnTimeInSecond
{
  get { return _spawnTimeInSecond; }
  set { _spawnTimeInSecond = value; }
}
public ItemCollectionBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(76);
_itemPermutationsCollection = new ItemPermutationBlockCollection(_itemPermutations);

}
public void Read(BinaryReader reader)
{
  _itemPermutations.Read(reader);
  _spawnTimeInSecond.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_itemPermutations.Count; x++)
{
  ItemPermutations.AddNew();
  ItemPermutations[x].Read(reader);
}
for (int x=0; x<_itemPermutations.Count; x++)
  ItemPermutations[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _itemPermutations.Write(writer);
    _spawnTimeInSecond.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_itemPermutations.UpdateReflexiveOffset(writer);
for (int x=0; x<_itemPermutations.Count; x++)
{
  ItemPermutations[x].Write(writer);
}
for (int x=0; x<_itemPermutations.Count; x++)
  ItemPermutations[x].WriteChildData(writer);
}
}
public class ItemPermutationBlock : IBlock
{
private Pad  __unnamed;	
private Real _weight = new Real();
private TagReference _item = new TagReference();
private Pad  __unnamed2;	
public Real Weight
{
  get { return _weight; }
  set { _weight = value; }
}
public TagReference Item
{
  get { return _item; }
  set { _item = value; }
}
public ItemPermutationBlock()
{
__unnamed = new Pad(32);
__unnamed2 = new Pad(32);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  _weight.Read(reader);
  _item.Read(reader);
  __unnamed2.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_item.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    _weight.Write(writer);
    _item.Write(writer);
    __unnamed2.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_item.WriteString(writer);
}
}
  }
}
