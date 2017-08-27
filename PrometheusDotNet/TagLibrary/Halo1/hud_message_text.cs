using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class HudMessageText : IBlock
  {
    public HudMessageTextBlock HudMessageTextValues = new HudMessageTextBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'HudMessageText'------------------------------------------------------");
      HudMessageTextValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      HudMessageTextValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      HudMessageTextValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      HudMessageTextValues.WriteChildData(writer);
    }
public class HudMessageTextBlock : IBlock
{
private Data _textData = new Data();
private Block _messageElements = new Block();
private Block _messages = new Block();
private Pad  __unnamed;	
public class HudMessageElementsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public HudMessageElementsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(HudMessageElementsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new HudMessageElementsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public HudMessageElementsBlock this[int index]
  {
    get { return (InnerList[index] as HudMessageElementsBlock); }
  }
}
private HudMessageElementsBlockCollection _messageElementsCollection;
public HudMessageElementsBlockCollection MessageElements
{
  get { return _messageElementsCollection; }
}
public class HudMessagesBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public HudMessagesBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(HudMessagesBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new HudMessagesBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public HudMessagesBlock this[int index]
  {
    get { return (InnerList[index] as HudMessagesBlock); }
  }
}
private HudMessagesBlockCollection _messagesCollection;
public HudMessagesBlockCollection Messages
{
  get { return _messagesCollection; }
}
public Data TextData
{
  get { return _textData; }
  set { _textData = value; }
}
public HudMessageTextBlock()
{
__unnamed = new Pad(84);
_messageElementsCollection = new HudMessageElementsBlockCollection(_messageElements);
_messagesCollection = new HudMessagesBlockCollection(_messages);

}
public void Read(BinaryReader reader)
{
  _textData.Read(reader);
  _messageElements.Read(reader);
  _messages.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_textData.ReadBinary(reader);
for (int x=0; x<_messageElements.Count; x++)
{
  MessageElements.AddNew();
  MessageElements[x].Read(reader);
}
for (int x=0; x<_messageElements.Count; x++)
  MessageElements[x].ReadChildData(reader);
for (int x=0; x<_messages.Count; x++)
{
  Messages.AddNew();
  Messages[x].Read(reader);
}
for (int x=0; x<_messages.Count; x++)
  Messages[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _textData.Write(writer);
    _messageElements.Write(writer);
    _messages.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_textData.WriteBinary(writer);
_messageElements.UpdateReflexiveOffset(writer);
for (int x=0; x<_messageElements.Count; x++)
{
  MessageElements[x].Write(writer);
}
for (int x=0; x<_messageElements.Count; x++)
  MessageElements[x].WriteChildData(writer);
_messages.UpdateReflexiveOffset(writer);
for (int x=0; x<_messages.Count; x++)
{
  Messages[x].Write(writer);
}
for (int x=0; x<_messages.Count; x++)
  Messages[x].WriteChildData(writer);
}
}
public class HudMessageElementsBlock : IBlock
{
private CharInteger _type = new CharInteger();
private CharInteger _data = new CharInteger();
public CharInteger Type
{
  get { return _type; }
  set { _type = value; }
}
public CharInteger Data
{
  get { return _data; }
  set { _data = value; }
}
public HudMessageElementsBlock()
{

}
public void Read(BinaryReader reader)
{
  _type.Read(reader);
  _data.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _type.Write(writer);
    _data.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class HudMessagesBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private ShortInteger _startIndexIntoTextBlob = new ShortInteger();
private ShortInteger _startIndexOfMessageBlock = new ShortInteger();
private CharInteger _panelCount = new CharInteger();
private Pad  __unnamed;	
private Pad  __unnamed2;	
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public ShortInteger StartIndexIntoTextBlob
{
  get { return _startIndexIntoTextBlob; }
  set { _startIndexIntoTextBlob = value; }
}
public ShortInteger StartIndexOfMessageBlock
{
  get { return _startIndexOfMessageBlock; }
  set { _startIndexOfMessageBlock = value; }
}
public CharInteger PanelCount
{
  get { return _panelCount; }
  set { _panelCount = value; }
}
public HudMessagesBlock()
{
__unnamed = new Pad(3);
__unnamed2 = new Pad(24);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _startIndexIntoTextBlob.Read(reader);
  _startIndexOfMessageBlock.Read(reader);
  _panelCount.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _startIndexIntoTextBlob.Write(writer);
    _startIndexOfMessageBlock.Write(writer);
    _panelCount.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
  }
}
