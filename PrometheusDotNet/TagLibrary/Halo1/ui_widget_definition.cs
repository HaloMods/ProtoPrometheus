using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class UiWidgetDefinition : IBlock
  {
    public UiWidgetDefinitionBlock UiWidgetDefinitionValues = new UiWidgetDefinitionBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'UiWidgetDefinition'------------------------------------------------------");
      UiWidgetDefinitionValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      UiWidgetDefinitionValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      UiWidgetDefinitionValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      UiWidgetDefinitionValues.WriteChildData(writer);
    }
public class UiWidgetDefinitionBlock : IBlock
{
private Enum _widgetType = new Enum();
private Enum _controllerIndex = new Enum();
private FixedLengthString _name = new FixedLengthString();
private Rectangle2D _bounds = new Rectangle2D();
private Flags  _flags;	
private LongInteger _millisecondsToAutoClose = new LongInteger();
private LongInteger _millisecondsAutoCloseFadeTime = new LongInteger();
private TagReference _backgroundBitmap = new TagReference();
private Block _gameDataInputs = new Block();
private Block _eventHandlers = new Block();
private Block _searchAndReplaceFunctions = new Block();
private Pad  __unnamed;	
private TagReference _textLabelUnicodeStringsList = new TagReference();
private TagReference _textFont = new TagReference();
private RealARGBColor _textColor = new RealARGBColor();
private Enum _justification = new Enum();
private Flags  _flags2;	
private Pad  __unnamed2;	
private ShortInteger _stringListIndex = new ShortInteger();
private ShortInteger _horizOffset = new ShortInteger();
private ShortInteger _vertOffset = new ShortInteger();
private Pad  __unnamed3;	
private Pad  __unnamed4;	
private Flags  _flags3;	
private TagReference _listHeaderBitmap = new TagReference();
private TagReference _listFooterBitmap = new TagReference();
private Rectangle2D _headerBounds = new Rectangle2D();
private Rectangle2D _footerBounds = new Rectangle2D();
private Pad  __unnamed5;	
private TagReference _extendedDescriptionWidget = new TagReference();
private Pad  __unnamed6;	
private Pad  __unnamed7;	
private Block _conditionalWidgets = new Block();
private Pad  __unnamed8;	
private Pad  __unnamed9;	
private Block _childWidgets = new Block();
public class GameDataInputReferencesBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public GameDataInputReferencesBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(GameDataInputReferencesBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new GameDataInputReferencesBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public GameDataInputReferencesBlock this[int index]
  {
    get { return (InnerList[index] as GameDataInputReferencesBlock); }
  }
}
private GameDataInputReferencesBlockCollection _gameDataInputsCollection;
public GameDataInputReferencesBlockCollection GameDataInputs
{
  get { return _gameDataInputsCollection; }
}
public class EventHandlerReferencesBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public EventHandlerReferencesBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(EventHandlerReferencesBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new EventHandlerReferencesBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public EventHandlerReferencesBlock this[int index]
  {
    get { return (InnerList[index] as EventHandlerReferencesBlock); }
  }
}
private EventHandlerReferencesBlockCollection _eventHandlersCollection;
public EventHandlerReferencesBlockCollection EventHandlers
{
  get { return _eventHandlersCollection; }
}
public class SearchAndReplaceReferenceBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public SearchAndReplaceReferenceBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(SearchAndReplaceReferenceBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new SearchAndReplaceReferenceBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public SearchAndReplaceReferenceBlock this[int index]
  {
    get { return (InnerList[index] as SearchAndReplaceReferenceBlock); }
  }
}
private SearchAndReplaceReferenceBlockCollection _searchAndReplaceFunctionsCollection;
public SearchAndReplaceReferenceBlockCollection SearchAndReplaceFunctions
{
  get { return _searchAndReplaceFunctionsCollection; }
}
public class ConditionalWidgetReferenceBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ConditionalWidgetReferenceBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ConditionalWidgetReferenceBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ConditionalWidgetReferenceBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ConditionalWidgetReferenceBlock this[int index]
  {
    get { return (InnerList[index] as ConditionalWidgetReferenceBlock); }
  }
}
private ConditionalWidgetReferenceBlockCollection _conditionalWidgetsCollection;
public ConditionalWidgetReferenceBlockCollection ConditionalWidgets
{
  get { return _conditionalWidgetsCollection; }
}
public class ChildWidgetReferenceBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ChildWidgetReferenceBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ChildWidgetReferenceBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ChildWidgetReferenceBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ChildWidgetReferenceBlock this[int index]
  {
    get { return (InnerList[index] as ChildWidgetReferenceBlock); }
  }
}
private ChildWidgetReferenceBlockCollection _childWidgetsCollection;
public ChildWidgetReferenceBlockCollection ChildWidgets
{
  get { return _childWidgetsCollection; }
}
public Enum WidgetType
{
  get { return _widgetType; }
  set { _widgetType = value; }
}
public Enum ControllerIndex
{
  get { return _controllerIndex; }
  set { _controllerIndex = value; }
}
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public Rectangle2D Bounds
{
  get { return _bounds; }
  set { _bounds = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public LongInteger MillisecondsToAutoClose
{
  get { return _millisecondsToAutoClose; }
  set { _millisecondsToAutoClose = value; }
}
public LongInteger MillisecondsAutoCloseFadeTime
{
  get { return _millisecondsAutoCloseFadeTime; }
  set { _millisecondsAutoCloseFadeTime = value; }
}
public TagReference BackgroundBitmap
{
  get { return _backgroundBitmap; }
  set { _backgroundBitmap = value; }
}
public TagReference TextLabelUnicodeStringsList
{
  get { return _textLabelUnicodeStringsList; }
  set { _textLabelUnicodeStringsList = value; }
}
public TagReference TextFont
{
  get { return _textFont; }
  set { _textFont = value; }
}
public RealARGBColor TextColor
{
  get { return _textColor; }
  set { _textColor = value; }
}
public Enum Justification
{
  get { return _justification; }
  set { _justification = value; }
}
public Flags Flags2
{
  get { return _flags2; }
  set { _flags2 = value; }
}
public ShortInteger StringListIndex
{
  get { return _stringListIndex; }
  set { _stringListIndex = value; }
}
public ShortInteger HorizOffset
{
  get { return _horizOffset; }
  set { _horizOffset = value; }
}
public ShortInteger VertOffset
{
  get { return _vertOffset; }
  set { _vertOffset = value; }
}
public Flags Flags3
{
  get { return _flags3; }
  set { _flags3 = value; }
}
public TagReference ListHeaderBitmap
{
  get { return _listHeaderBitmap; }
  set { _listHeaderBitmap = value; }
}
public TagReference ListFooterBitmap
{
  get { return _listFooterBitmap; }
  set { _listFooterBitmap = value; }
}
public Rectangle2D HeaderBounds
{
  get { return _headerBounds; }
  set { _headerBounds = value; }
}
public Rectangle2D FooterBounds
{
  get { return _footerBounds; }
  set { _footerBounds = value; }
}
public TagReference ExtendedDescriptionWidget
{
  get { return _extendedDescriptionWidget; }
  set { _extendedDescriptionWidget = value; }
}
public UiWidgetDefinitionBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(128);
_flags2 = new Flags(4);
__unnamed2 = new Pad(12);
__unnamed3 = new Pad(26);
__unnamed4 = new Pad(2);
_flags3 = new Flags(4);
__unnamed5 = new Pad(32);
__unnamed6 = new Pad(32);
__unnamed7 = new Pad(256);
__unnamed8 = new Pad(128);
__unnamed9 = new Pad(128);
_gameDataInputsCollection = new GameDataInputReferencesBlockCollection(_gameDataInputs);
_eventHandlersCollection = new EventHandlerReferencesBlockCollection(_eventHandlers);
_searchAndReplaceFunctionsCollection = new SearchAndReplaceReferenceBlockCollection(_searchAndReplaceFunctions);
_conditionalWidgetsCollection = new ConditionalWidgetReferenceBlockCollection(_conditionalWidgets);
_childWidgetsCollection = new ChildWidgetReferenceBlockCollection(_childWidgets);

}
public void Read(BinaryReader reader)
{
  _widgetType.Read(reader);
  _controllerIndex.Read(reader);
  _name.Read(reader);
  _bounds.Read(reader);
  _flags.Read(reader);
  _millisecondsToAutoClose.Read(reader);
  _millisecondsAutoCloseFadeTime.Read(reader);
  _backgroundBitmap.Read(reader);
  _gameDataInputs.Read(reader);
  _eventHandlers.Read(reader);
  _searchAndReplaceFunctions.Read(reader);
  __unnamed.Read(reader);
  _textLabelUnicodeStringsList.Read(reader);
  _textFont.Read(reader);
  _textColor.Read(reader);
  _justification.Read(reader);
  _flags2.Read(reader);
  __unnamed2.Read(reader);
  _stringListIndex.Read(reader);
  _horizOffset.Read(reader);
  _vertOffset.Read(reader);
  __unnamed3.Read(reader);
  __unnamed4.Read(reader);
  _flags3.Read(reader);
  _listHeaderBitmap.Read(reader);
  _listFooterBitmap.Read(reader);
  _headerBounds.Read(reader);
  _footerBounds.Read(reader);
  __unnamed5.Read(reader);
  _extendedDescriptionWidget.Read(reader);
  __unnamed6.Read(reader);
  __unnamed7.Read(reader);
  _conditionalWidgets.Read(reader);
  __unnamed8.Read(reader);
  __unnamed9.Read(reader);
  _childWidgets.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_backgroundBitmap.ReadString(reader);
for (int x=0; x<_gameDataInputs.Count; x++)
{
  GameDataInputs.AddNew();
  GameDataInputs[x].Read(reader);
}
for (int x=0; x<_gameDataInputs.Count; x++)
  GameDataInputs[x].ReadChildData(reader);
for (int x=0; x<_eventHandlers.Count; x++)
{
  EventHandlers.AddNew();
  EventHandlers[x].Read(reader);
}
for (int x=0; x<_eventHandlers.Count; x++)
  EventHandlers[x].ReadChildData(reader);
for (int x=0; x<_searchAndReplaceFunctions.Count; x++)
{
  SearchAndReplaceFunctions.AddNew();
  SearchAndReplaceFunctions[x].Read(reader);
}
for (int x=0; x<_searchAndReplaceFunctions.Count; x++)
  SearchAndReplaceFunctions[x].ReadChildData(reader);
_textLabelUnicodeStringsList.ReadString(reader);
_textFont.ReadString(reader);
_listHeaderBitmap.ReadString(reader);
_listFooterBitmap.ReadString(reader);
_extendedDescriptionWidget.ReadString(reader);
for (int x=0; x<_conditionalWidgets.Count; x++)
{
  ConditionalWidgets.AddNew();
  ConditionalWidgets[x].Read(reader);
}
for (int x=0; x<_conditionalWidgets.Count; x++)
  ConditionalWidgets[x].ReadChildData(reader);
for (int x=0; x<_childWidgets.Count; x++)
{
  ChildWidgets.AddNew();
  ChildWidgets[x].Read(reader);
}
for (int x=0; x<_childWidgets.Count; x++)
  ChildWidgets[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _widgetType.Write(writer);
    _controllerIndex.Write(writer);
    _name.Write(writer);
    _bounds.Write(writer);
    _flags.Write(writer);
    _millisecondsToAutoClose.Write(writer);
    _millisecondsAutoCloseFadeTime.Write(writer);
    _backgroundBitmap.Write(writer);
    _gameDataInputs.Write(writer);
    _eventHandlers.Write(writer);
    _searchAndReplaceFunctions.Write(writer);
    __unnamed.Write(writer);
    _textLabelUnicodeStringsList.Write(writer);
    _textFont.Write(writer);
    _textColor.Write(writer);
    _justification.Write(writer);
    _flags2.Write(writer);
    __unnamed2.Write(writer);
    _stringListIndex.Write(writer);
    _horizOffset.Write(writer);
    _vertOffset.Write(writer);
    __unnamed3.Write(writer);
    __unnamed4.Write(writer);
    _flags3.Write(writer);
    _listHeaderBitmap.Write(writer);
    _listFooterBitmap.Write(writer);
    _headerBounds.Write(writer);
    _footerBounds.Write(writer);
    __unnamed5.Write(writer);
    _extendedDescriptionWidget.Write(writer);
    __unnamed6.Write(writer);
    __unnamed7.Write(writer);
    _conditionalWidgets.Write(writer);
    __unnamed8.Write(writer);
    __unnamed9.Write(writer);
    _childWidgets.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_backgroundBitmap.WriteString(writer);
_gameDataInputs.UpdateReflexiveOffset(writer);
for (int x=0; x<_gameDataInputs.Count; x++)
{
  GameDataInputs[x].Write(writer);
}
for (int x=0; x<_gameDataInputs.Count; x++)
  GameDataInputs[x].WriteChildData(writer);
_eventHandlers.UpdateReflexiveOffset(writer);
for (int x=0; x<_eventHandlers.Count; x++)
{
  EventHandlers[x].Write(writer);
}
for (int x=0; x<_eventHandlers.Count; x++)
  EventHandlers[x].WriteChildData(writer);
_searchAndReplaceFunctions.UpdateReflexiveOffset(writer);
for (int x=0; x<_searchAndReplaceFunctions.Count; x++)
{
  SearchAndReplaceFunctions[x].Write(writer);
}
for (int x=0; x<_searchAndReplaceFunctions.Count; x++)
  SearchAndReplaceFunctions[x].WriteChildData(writer);
_textLabelUnicodeStringsList.WriteString(writer);
_textFont.WriteString(writer);
_listHeaderBitmap.WriteString(writer);
_listFooterBitmap.WriteString(writer);
_extendedDescriptionWidget.WriteString(writer);
_conditionalWidgets.UpdateReflexiveOffset(writer);
for (int x=0; x<_conditionalWidgets.Count; x++)
{
  ConditionalWidgets[x].Write(writer);
}
for (int x=0; x<_conditionalWidgets.Count; x++)
  ConditionalWidgets[x].WriteChildData(writer);
_childWidgets.UpdateReflexiveOffset(writer);
for (int x=0; x<_childWidgets.Count; x++)
{
  ChildWidgets[x].Write(writer);
}
for (int x=0; x<_childWidgets.Count; x++)
  ChildWidgets[x].WriteChildData(writer);
}
}
public class GameDataInputReferencesBlock : IBlock
{
private Enum _function = new Enum();
private Pad  __unnamed;	
private Pad  __unnamed2;	
public Enum Function
{
  get { return _function; }
  set { _function = value; }
}
public GameDataInputReferencesBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(32);

}
public void Read(BinaryReader reader)
{
  _function.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _function.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class EventHandlerReferencesBlock : IBlock
{
private Flags  _flags;	
private Enum _eventType = new Enum();
private Enum _function = new Enum();
private TagReference _widgetTag = new TagReference();
private TagReference _soundEffect = new TagReference();
private FixedLengthString _script = new FixedLengthString();
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Enum EventType
{
  get { return _eventType; }
  set { _eventType = value; }
}
public Enum Function
{
  get { return _function; }
  set { _function = value; }
}
public TagReference WidgetTag
{
  get { return _widgetTag; }
  set { _widgetTag = value; }
}
public TagReference SoundEffect
{
  get { return _soundEffect; }
  set { _soundEffect = value; }
}
public FixedLengthString Script
{
  get { return _script; }
  set { _script = value; }
}
public EventHandlerReferencesBlock()
{
_flags = new Flags(4);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _eventType.Read(reader);
  _function.Read(reader);
  _widgetTag.Read(reader);
  _soundEffect.Read(reader);
  _script.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_widgetTag.ReadString(reader);
_soundEffect.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _eventType.Write(writer);
    _function.Write(writer);
    _widgetTag.Write(writer);
    _soundEffect.Write(writer);
    _script.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_widgetTag.WriteString(writer);
_soundEffect.WriteString(writer);
}
}
public class SearchAndReplaceReferenceBlock : IBlock
{
private FixedLengthString _searchString = new FixedLengthString();
private Enum _replaceFunction = new Enum();
public FixedLengthString SearchString
{
  get { return _searchString; }
  set { _searchString = value; }
}
public Enum ReplaceFunction
{
  get { return _replaceFunction; }
  set { _replaceFunction = value; }
}
public SearchAndReplaceReferenceBlock()
{

}
public void Read(BinaryReader reader)
{
  _searchString.Read(reader);
  _replaceFunction.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _searchString.Write(writer);
    _replaceFunction.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class ConditionalWidgetReferenceBlock : IBlock
{
private TagReference _widgetTag = new TagReference();
private FixedLengthString _nameUnused = new FixedLengthString();
private Flags  _flags;	
private ShortInteger _customControllerIndexUnused = new ShortInteger();
private Pad  __unnamed;	
public TagReference WidgetTag
{
  get { return _widgetTag; }
  set { _widgetTag = value; }
}
public FixedLengthString NameUnused
{
  get { return _nameUnused; }
  set { _nameUnused = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public ShortInteger CustomControllerIndexUnused
{
  get { return _customControllerIndexUnused; }
  set { _customControllerIndexUnused = value; }
}
public ConditionalWidgetReferenceBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(26);

}
public void Read(BinaryReader reader)
{
  _widgetTag.Read(reader);
  _nameUnused.Read(reader);
  _flags.Read(reader);
  _customControllerIndexUnused.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_widgetTag.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _widgetTag.Write(writer);
    _nameUnused.Write(writer);
    _flags.Write(writer);
    _customControllerIndexUnused.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_widgetTag.WriteString(writer);
}
}
public class ChildWidgetReferenceBlock : IBlock
{
private TagReference _widgetTag = new TagReference();
private FixedLengthString _nameUnused = new FixedLengthString();
private Flags  _flags;	
private ShortInteger _customControllerIndex = new ShortInteger();
private ShortInteger _verticalOffset = new ShortInteger();
private ShortInteger _horizontalOffset = new ShortInteger();
private Pad  __unnamed;	
public TagReference WidgetTag
{
  get { return _widgetTag; }
  set { _widgetTag = value; }
}
public FixedLengthString NameUnused
{
  get { return _nameUnused; }
  set { _nameUnused = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public ShortInteger CustomControllerIndex
{
  get { return _customControllerIndex; }
  set { _customControllerIndex = value; }
}
public ShortInteger VerticalOffset
{
  get { return _verticalOffset; }
  set { _verticalOffset = value; }
}
public ShortInteger HorizontalOffset
{
  get { return _horizontalOffset; }
  set { _horizontalOffset = value; }
}
public ChildWidgetReferenceBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(22);

}
public void Read(BinaryReader reader)
{
  _widgetTag.Read(reader);
  _nameUnused.Read(reader);
  _flags.Read(reader);
  _customControllerIndex.Read(reader);
  _verticalOffset.Read(reader);
  _horizontalOffset.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_widgetTag.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _widgetTag.Write(writer);
    _nameUnused.Write(writer);
    _flags.Write(writer);
    _customControllerIndex.Write(writer);
    _verticalOffset.Write(writer);
    _horizontalOffset.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_widgetTag.WriteString(writer);
}
}
  }
}
