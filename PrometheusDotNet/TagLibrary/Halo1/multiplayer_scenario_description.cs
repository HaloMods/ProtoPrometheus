using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class MultiplayerScenarioDescription : IBlock
  {
    public MultiplayerScenarioDescriptionBlock MultiplayerScenarioDescriptionValues = new MultiplayerScenarioDescriptionBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'MultiplayerScenarioDescription'------------------------------------------------------");
      MultiplayerScenarioDescriptionValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      MultiplayerScenarioDescriptionValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      MultiplayerScenarioDescriptionValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      MultiplayerScenarioDescriptionValues.WriteChildData(writer);
    }
public class MultiplayerScenarioDescriptionBlock : IBlock
{
private Block _multiplayerScenarios = new Block();
public class ScenarioDescriptionBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ScenarioDescriptionBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ScenarioDescriptionBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ScenarioDescriptionBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ScenarioDescriptionBlock this[int index]
  {
    get { return (InnerList[index] as ScenarioDescriptionBlock); }
  }
}
private ScenarioDescriptionBlockCollection _multiplayerScenariosCollection;
public ScenarioDescriptionBlockCollection MultiplayerScenarios
{
  get { return _multiplayerScenariosCollection; }
}
public MultiplayerScenarioDescriptionBlock()
{
_multiplayerScenariosCollection = new ScenarioDescriptionBlockCollection(_multiplayerScenarios);

}
public void Read(BinaryReader reader)
{
  _multiplayerScenarios.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_multiplayerScenarios.Count; x++)
{
  MultiplayerScenarios.AddNew();
  MultiplayerScenarios[x].Read(reader);
}
for (int x=0; x<_multiplayerScenarios.Count; x++)
  MultiplayerScenarios[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _multiplayerScenarios.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_multiplayerScenarios.UpdateReflexiveOffset(writer);
for (int x=0; x<_multiplayerScenarios.Count; x++)
{
  MultiplayerScenarios[x].Write(writer);
}
for (int x=0; x<_multiplayerScenarios.Count; x++)
  MultiplayerScenarios[x].WriteChildData(writer);
}
}
public class ScenarioDescriptionBlock : IBlock
{
private TagReference _descriptiveBitmap = new TagReference();
private TagReference _displayedMapName = new TagReference();
private FixedLengthString _scenarioTagDirectoryPath = new FixedLengthString();
private Pad  __unnamed;	
public TagReference DescriptiveBitmap
{
  get { return _descriptiveBitmap; }
  set { _descriptiveBitmap = value; }
}
public TagReference DisplayedMapName
{
  get { return _displayedMapName; }
  set { _displayedMapName = value; }
}
public FixedLengthString ScenarioTagDirectoryPath
{
  get { return _scenarioTagDirectoryPath; }
  set { _scenarioTagDirectoryPath = value; }
}
public ScenarioDescriptionBlock()
{
__unnamed = new Pad(4);

}
public void Read(BinaryReader reader)
{
  _descriptiveBitmap.Read(reader);
  _displayedMapName.Read(reader);
  _scenarioTagDirectoryPath.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_descriptiveBitmap.ReadString(reader);
_displayedMapName.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _descriptiveBitmap.Write(writer);
    _displayedMapName.Write(writer);
    _scenarioTagDirectoryPath.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_descriptiveBitmap.WriteString(writer);
_displayedMapName.WriteString(writer);
}
}
  }
}
