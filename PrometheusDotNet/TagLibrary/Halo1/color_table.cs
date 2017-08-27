using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class ColorTable : IBlock
  {
    public ColorTableBlock ColorTableValues = new ColorTableBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'ColorTable'------------------------------------------------------");
      ColorTableValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      ColorTableValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      ColorTableValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      ColorTableValues.WriteChildData(writer);
    }
public class ColorTableBlock : IBlock
{
private Block _colors = new Block();
public class ColorBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ColorBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ColorBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ColorBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ColorBlock this[int index]
  {
    get { return (InnerList[index] as ColorBlock); }
  }
}
private ColorBlockCollection _colorsCollection;
public ColorBlockCollection Colors
{
  get { return _colorsCollection; }
}
public ColorTableBlock()
{
_colorsCollection = new ColorBlockCollection(_colors);

}
public void Read(BinaryReader reader)
{
  _colors.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_colors.Count; x++)
{
  Colors.AddNew();
  Colors[x].Read(reader);
}
for (int x=0; x<_colors.Count; x++)
  Colors[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _colors.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_colors.UpdateReflexiveOffset(writer);
for (int x=0; x<_colors.Count; x++)
{
  Colors[x].Write(writer);
}
for (int x=0; x<_colors.Count; x++)
  Colors[x].WriteChildData(writer);
}
}
public class ColorBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private RealARGBColor _color = new RealARGBColor();
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public RealARGBColor Color
{
  get { return _color; }
  set { _color = value; }
}
public ColorBlock()
{

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _color.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _color.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
  }
}
