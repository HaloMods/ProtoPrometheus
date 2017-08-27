using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class UiWidgetCollection : IBlock
  {
    public UiWidgetCollectionBlock UiWidgetCollectionValues = new UiWidgetCollectionBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'UiWidgetCollection'------------------------------------------------------");
      UiWidgetCollectionValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      UiWidgetCollectionValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      UiWidgetCollectionValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      UiWidgetCollectionValues.WriteChildData(writer);
    }
public class UiWidgetCollectionBlock : IBlock
{
private Block _uiWidgetDefinitions = new Block();
public class UiWidgetReferencesBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public UiWidgetReferencesBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(UiWidgetReferencesBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new UiWidgetReferencesBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public UiWidgetReferencesBlock this[int index]
  {
    get { return (InnerList[index] as UiWidgetReferencesBlock); }
  }
}
private UiWidgetReferencesBlockCollection _uiWidgetDefinitionsCollection;
public UiWidgetReferencesBlockCollection UiWidgetDefinitions
{
  get { return _uiWidgetDefinitionsCollection; }
}
public UiWidgetCollectionBlock()
{
_uiWidgetDefinitionsCollection = new UiWidgetReferencesBlockCollection(_uiWidgetDefinitions);

}
public void Read(BinaryReader reader)
{
  _uiWidgetDefinitions.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_uiWidgetDefinitions.Count; x++)
{
  UiWidgetDefinitions.AddNew();
  UiWidgetDefinitions[x].Read(reader);
}
for (int x=0; x<_uiWidgetDefinitions.Count; x++)
  UiWidgetDefinitions[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _uiWidgetDefinitions.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_uiWidgetDefinitions.UpdateReflexiveOffset(writer);
for (int x=0; x<_uiWidgetDefinitions.Count; x++)
{
  UiWidgetDefinitions[x].Write(writer);
}
for (int x=0; x<_uiWidgetDefinitions.Count; x++)
  UiWidgetDefinitions[x].WriteChildData(writer);
}
}
public class UiWidgetReferencesBlock : IBlock
{
private TagReference _ui_widget_definition = new TagReference();
public TagReference Ui_widget_definition
{
  get { return _ui_widget_definition; }
  set { _ui_widget_definition = value; }
}
public UiWidgetReferencesBlock()
{

}
public void Read(BinaryReader reader)
{
  _ui_widget_definition.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_ui_widget_definition.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _ui_widget_definition.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_ui_widget_definition.WriteString(writer);
}
}
  }
}
