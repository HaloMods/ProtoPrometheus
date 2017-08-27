using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class HudGlobals : IBlock
  {
    public HudGlobalsBlock HudGlobalsValues = new HudGlobalsBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'HudGlobals'------------------------------------------------------");
      HudGlobalsValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      HudGlobalsValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      HudGlobalsValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      HudGlobalsValues.WriteChildData(writer);
    }
public class HudGlobalsBlock : IBlock
{
private Enum _anchor = new Enum();
private Pad  __unnamed;	
private Pad  __unnamed2;	
private Point2D _anchorOffset = new Point2D();
private Real _widthScale = new Real();
private Real _heightScale = new Real();
private Flags  _scalingFlags;	
private Pad  __unnamed3;	
private Pad  __unnamed4;	
private TagReference _singlePlayerFont = new TagReference();
private TagReference _multiPlayerFont = new TagReference();
private Real _upTime = new Real();
private Real _fadeTime = new Real();
private RealARGBColor _iconColor = new RealARGBColor();
private RealARGBColor _textColor = new RealARGBColor();
private Real _textSpacing = new Real();
private TagReference _itemMessageText = new TagReference();
private TagReference _iconBitmap = new TagReference();
private TagReference _alternateIconText = new TagReference();
private Block _buttonIcons = new Block();
private ARGBColor _defaultColor = new ARGBColor();
private ARGBColor _flashingColor = new ARGBColor();
private Real _flashPeriod = new Real();
private Real _flashDelay = new Real();
private ShortInteger _numberOfFlashes = new ShortInteger();
private Flags  _flashFlags;	
private Real _flashLength = new Real();
private ARGBColor _disabledColor = new ARGBColor();
private Pad  __unnamed5;	
private TagReference _hudMessages = new TagReference();
private ARGBColor _defaultColor2 = new ARGBColor();
private ARGBColor _flashingColor2 = new ARGBColor();
private Real _flashPeriod2 = new Real();
private Real _flashDelay2 = new Real();
private ShortInteger _numberOfFlashes2 = new ShortInteger();
private Flags  _flashFlags2;	
private Real _flashLength2 = new Real();
private ARGBColor _disabledColor2 = new ARGBColor();
private ShortInteger _uptimeTicks = new ShortInteger();
private ShortInteger _fadeTicks = new ShortInteger();
private Real _topOffset = new Real();
private Real _bottomOffset = new Real();
private Real _leftOffset = new Real();
private Real _rightOffset = new Real();
private Pad  __unnamed6;	
private TagReference _arrowBitmap = new TagReference();
private Block _waypointArrows = new Block();
private Pad  __unnamed7;	
private Real _hudScaleInMultiplayer = new Real();
private Pad  __unnamed8;	
private TagReference _defaultWeaponHud = new TagReference();
private Real _motionSensorRange = new Real();
private Real _motionSensorVelocitySensitivity = new Real();
private Real _motionSensorScaleDONTTOUCHEVER = new Real();
private Rectangle2D _defaultChapterTitleBounds = new Rectangle2D();
private Pad  __unnamed9;	
private ShortInteger _topOffset2 = new ShortInteger();
private ShortInteger _bottomOffset2 = new ShortInteger();
private ShortInteger _leftOffset2 = new ShortInteger();
private ShortInteger _rightOffset2 = new ShortInteger();
private Pad  __unnamed10;	
private TagReference _indicatorBitmap = new TagReference();
private ShortInteger _sequenceIndex = new ShortInteger();
private ShortInteger _multiplayerSequenceIndex = new ShortInteger();
private ARGBColor _color = new ARGBColor();
private Pad  __unnamed11;	
private ARGBColor _defaultColor3 = new ARGBColor();
private ARGBColor _flashingColor3 = new ARGBColor();
private Real _flashPeriod3 = new Real();
private Real _flashDelay3 = new Real();
private ShortInteger _numberOfFlashes3 = new ShortInteger();
private Flags  _flashFlags3;	
private Real _flashLength3 = new Real();
private ARGBColor _disabledColor3 = new ARGBColor();
private Pad  __unnamed12;	
private ARGBColor _defaultColor4 = new ARGBColor();
private ARGBColor _flashingColor4 = new ARGBColor();
private Real _flashPeriod4 = new Real();
private Real _flashDelay4 = new Real();
private ShortInteger _numberOfFlashes4 = new ShortInteger();
private Flags  _flashFlags4;	
private Real _flashLength4 = new Real();
private ARGBColor _disabledColor4 = new ARGBColor();
private Pad  __unnamed13;	
private Pad  __unnamed14;	
private TagReference _carnageReportBitmap = new TagReference();
private ShortInteger _loadingBeginText = new ShortInteger();
private ShortInteger _loadingEndText = new ShortInteger();
private ShortInteger _checkpointBeginText = new ShortInteger();
private ShortInteger _checkpointEndText = new ShortInteger();
private TagReference _checkpointSound = new TagReference();
private Pad  __unnamed15;	
public class HudButtonIconBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public HudButtonIconBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(HudButtonIconBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new HudButtonIconBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public HudButtonIconBlock this[int index]
  {
    get { return (InnerList[index] as HudButtonIconBlock); }
  }
}
private HudButtonIconBlockCollection _buttonIconsCollection;
public HudButtonIconBlockCollection ButtonIcons
{
  get { return _buttonIconsCollection; }
}
public class HudWaypointArrowBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public HudWaypointArrowBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(HudWaypointArrowBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new HudWaypointArrowBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public HudWaypointArrowBlock this[int index]
  {
    get { return (InnerList[index] as HudWaypointArrowBlock); }
  }
}
private HudWaypointArrowBlockCollection _waypointArrowsCollection;
public HudWaypointArrowBlockCollection WaypointArrows
{
  get { return _waypointArrowsCollection; }
}
public Enum Anchor
{
  get { return _anchor; }
  set { _anchor = value; }
}
public Point2D AnchorOffset
{
  get { return _anchorOffset; }
  set { _anchorOffset = value; }
}
public Real WidthScale
{
  get { return _widthScale; }
  set { _widthScale = value; }
}
public Real HeightScale
{
  get { return _heightScale; }
  set { _heightScale = value; }
}
public Flags ScalingFlags
{
  get { return _scalingFlags; }
  set { _scalingFlags = value; }
}
public TagReference SinglePlayerFont
{
  get { return _singlePlayerFont; }
  set { _singlePlayerFont = value; }
}
public TagReference MultiPlayerFont
{
  get { return _multiPlayerFont; }
  set { _multiPlayerFont = value; }
}
public Real UpTime
{
  get { return _upTime; }
  set { _upTime = value; }
}
public Real FadeTime
{
  get { return _fadeTime; }
  set { _fadeTime = value; }
}
public RealARGBColor IconColor
{
  get { return _iconColor; }
  set { _iconColor = value; }
}
public RealARGBColor TextColor
{
  get { return _textColor; }
  set { _textColor = value; }
}
public Real TextSpacing
{
  get { return _textSpacing; }
  set { _textSpacing = value; }
}
public TagReference ItemMessageText
{
  get { return _itemMessageText; }
  set { _itemMessageText = value; }
}
public TagReference IconBitmap
{
  get { return _iconBitmap; }
  set { _iconBitmap = value; }
}
public TagReference AlternateIconText
{
  get { return _alternateIconText; }
  set { _alternateIconText = value; }
}
public ARGBColor DefaultColor
{
  get { return _defaultColor; }
  set { _defaultColor = value; }
}
public ARGBColor FlashingColor
{
  get { return _flashingColor; }
  set { _flashingColor = value; }
}
public Real FlashPeriod
{
  get { return _flashPeriod; }
  set { _flashPeriod = value; }
}
public Real FlashDelay
{
  get { return _flashDelay; }
  set { _flashDelay = value; }
}
public ShortInteger NumberOfFlashes
{
  get { return _numberOfFlashes; }
  set { _numberOfFlashes = value; }
}
public Flags FlashFlags
{
  get { return _flashFlags; }
  set { _flashFlags = value; }
}
public Real FlashLength
{
  get { return _flashLength; }
  set { _flashLength = value; }
}
public ARGBColor DisabledColor
{
  get { return _disabledColor; }
  set { _disabledColor = value; }
}
public TagReference HudMessages
{
  get { return _hudMessages; }
  set { _hudMessages = value; }
}
public ARGBColor DefaultColor2
{
  get { return _defaultColor2; }
  set { _defaultColor2 = value; }
}
public ARGBColor FlashingColor2
{
  get { return _flashingColor2; }
  set { _flashingColor2 = value; }
}
public Real FlashPeriod2
{
  get { return _flashPeriod2; }
  set { _flashPeriod2 = value; }
}
public Real FlashDelay2
{
  get { return _flashDelay2; }
  set { _flashDelay2 = value; }
}
public ShortInteger NumberOfFlashes2
{
  get { return _numberOfFlashes2; }
  set { _numberOfFlashes2 = value; }
}
public Flags FlashFlags2
{
  get { return _flashFlags2; }
  set { _flashFlags2 = value; }
}
public Real FlashLength2
{
  get { return _flashLength2; }
  set { _flashLength2 = value; }
}
public ARGBColor DisabledColor2
{
  get { return _disabledColor2; }
  set { _disabledColor2 = value; }
}
public ShortInteger UptimeTicks
{
  get { return _uptimeTicks; }
  set { _uptimeTicks = value; }
}
public ShortInteger FadeTicks
{
  get { return _fadeTicks; }
  set { _fadeTicks = value; }
}
public Real TopOffset
{
  get { return _topOffset; }
  set { _topOffset = value; }
}
public Real BottomOffset
{
  get { return _bottomOffset; }
  set { _bottomOffset = value; }
}
public Real LeftOffset
{
  get { return _leftOffset; }
  set { _leftOffset = value; }
}
public Real RightOffset
{
  get { return _rightOffset; }
  set { _rightOffset = value; }
}
public TagReference ArrowBitmap
{
  get { return _arrowBitmap; }
  set { _arrowBitmap = value; }
}
public Real HudScaleInMultiplayer
{
  get { return _hudScaleInMultiplayer; }
  set { _hudScaleInMultiplayer = value; }
}
public TagReference DefaultWeaponHud
{
  get { return _defaultWeaponHud; }
  set { _defaultWeaponHud = value; }
}
public Real MotionSensorRange
{
  get { return _motionSensorRange; }
  set { _motionSensorRange = value; }
}
public Real MotionSensorVelocitySensitivity
{
  get { return _motionSensorVelocitySensitivity; }
  set { _motionSensorVelocitySensitivity = value; }
}
public Real MotionSensorScaleDONTTOUCHEVER
{
  get { return _motionSensorScaleDONTTOUCHEVER; }
  set { _motionSensorScaleDONTTOUCHEVER = value; }
}
public Rectangle2D DefaultChapterTitleBounds
{
  get { return _defaultChapterTitleBounds; }
  set { _defaultChapterTitleBounds = value; }
}
public ShortInteger TopOffset2
{
  get { return _topOffset2; }
  set { _topOffset2 = value; }
}
public ShortInteger BottomOffset2
{
  get { return _bottomOffset2; }
  set { _bottomOffset2 = value; }
}
public ShortInteger LeftOffset2
{
  get { return _leftOffset2; }
  set { _leftOffset2 = value; }
}
public ShortInteger RightOffset2
{
  get { return _rightOffset2; }
  set { _rightOffset2 = value; }
}
public TagReference IndicatorBitmap
{
  get { return _indicatorBitmap; }
  set { _indicatorBitmap = value; }
}
public ShortInteger SequenceIndex
{
  get { return _sequenceIndex; }
  set { _sequenceIndex = value; }
}
public ShortInteger MultiplayerSequenceIndex
{
  get { return _multiplayerSequenceIndex; }
  set { _multiplayerSequenceIndex = value; }
}
public ARGBColor Color
{
  get { return _color; }
  set { _color = value; }
}
public ARGBColor DefaultColor3
{
  get { return _defaultColor3; }
  set { _defaultColor3 = value; }
}
public ARGBColor FlashingColor3
{
  get { return _flashingColor3; }
  set { _flashingColor3 = value; }
}
public Real FlashPeriod3
{
  get { return _flashPeriod3; }
  set { _flashPeriod3 = value; }
}
public Real FlashDelay3
{
  get { return _flashDelay3; }
  set { _flashDelay3 = value; }
}
public ShortInteger NumberOfFlashes3
{
  get { return _numberOfFlashes3; }
  set { _numberOfFlashes3 = value; }
}
public Flags FlashFlags3
{
  get { return _flashFlags3; }
  set { _flashFlags3 = value; }
}
public Real FlashLength3
{
  get { return _flashLength3; }
  set { _flashLength3 = value; }
}
public ARGBColor DisabledColor3
{
  get { return _disabledColor3; }
  set { _disabledColor3 = value; }
}
public ARGBColor DefaultColor4
{
  get { return _defaultColor4; }
  set { _defaultColor4 = value; }
}
public ARGBColor FlashingColor4
{
  get { return _flashingColor4; }
  set { _flashingColor4 = value; }
}
public Real FlashPeriod4
{
  get { return _flashPeriod4; }
  set { _flashPeriod4 = value; }
}
public Real FlashDelay4
{
  get { return _flashDelay4; }
  set { _flashDelay4 = value; }
}
public ShortInteger NumberOfFlashes4
{
  get { return _numberOfFlashes4; }
  set { _numberOfFlashes4 = value; }
}
public Flags FlashFlags4
{
  get { return _flashFlags4; }
  set { _flashFlags4 = value; }
}
public Real FlashLength4
{
  get { return _flashLength4; }
  set { _flashLength4 = value; }
}
public ARGBColor DisabledColor4
{
  get { return _disabledColor4; }
  set { _disabledColor4 = value; }
}
public TagReference CarnageReportBitmap
{
  get { return _carnageReportBitmap; }
  set { _carnageReportBitmap = value; }
}
public ShortInteger LoadingBeginText
{
  get { return _loadingBeginText; }
  set { _loadingBeginText = value; }
}
public ShortInteger LoadingEndText
{
  get { return _loadingEndText; }
  set { _loadingEndText = value; }
}
public ShortInteger CheckpointBeginText
{
  get { return _checkpointBeginText; }
  set { _checkpointBeginText = value; }
}
public ShortInteger CheckpointEndText
{
  get { return _checkpointEndText; }
  set { _checkpointEndText = value; }
}
public TagReference CheckpointSound
{
  get { return _checkpointSound; }
  set { _checkpointSound = value; }
}
public HudGlobalsBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(32);
_scalingFlags = new Flags(2);
__unnamed3 = new Pad(2);
__unnamed4 = new Pad(20);
_flashFlags = new Flags(2);
__unnamed5 = new Pad(4);
_flashFlags2 = new Flags(2);
__unnamed6 = new Pad(32);
__unnamed7 = new Pad(80);
__unnamed8 = new Pad(256);
__unnamed9 = new Pad(44);
__unnamed10 = new Pad(32);
__unnamed11 = new Pad(16);
_flashFlags3 = new Flags(2);
__unnamed12 = new Pad(4);
_flashFlags4 = new Flags(2);
__unnamed13 = new Pad(4);
__unnamed14 = new Pad(40);
__unnamed15 = new Pad(96);
_buttonIconsCollection = new HudButtonIconBlockCollection(_buttonIcons);
_waypointArrowsCollection = new HudWaypointArrowBlockCollection(_waypointArrows);

}
public void Read(BinaryReader reader)
{
  _anchor.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  _anchorOffset.Read(reader);
  _widthScale.Read(reader);
  _heightScale.Read(reader);
  _scalingFlags.Read(reader);
  __unnamed3.Read(reader);
  __unnamed4.Read(reader);
  _singlePlayerFont.Read(reader);
  _multiPlayerFont.Read(reader);
  _upTime.Read(reader);
  _fadeTime.Read(reader);
  _iconColor.Read(reader);
  _textColor.Read(reader);
  _textSpacing.Read(reader);
  _itemMessageText.Read(reader);
  _iconBitmap.Read(reader);
  _alternateIconText.Read(reader);
  _buttonIcons.Read(reader);
  _defaultColor.Read(reader);
  _flashingColor.Read(reader);
  _flashPeriod.Read(reader);
  _flashDelay.Read(reader);
  _numberOfFlashes.Read(reader);
  _flashFlags.Read(reader);
  _flashLength.Read(reader);
  _disabledColor.Read(reader);
  __unnamed5.Read(reader);
  _hudMessages.Read(reader);
  _defaultColor2.Read(reader);
  _flashingColor2.Read(reader);
  _flashPeriod2.Read(reader);
  _flashDelay2.Read(reader);
  _numberOfFlashes2.Read(reader);
  _flashFlags2.Read(reader);
  _flashLength2.Read(reader);
  _disabledColor2.Read(reader);
  _uptimeTicks.Read(reader);
  _fadeTicks.Read(reader);
  _topOffset.Read(reader);
  _bottomOffset.Read(reader);
  _leftOffset.Read(reader);
  _rightOffset.Read(reader);
  __unnamed6.Read(reader);
  _arrowBitmap.Read(reader);
  _waypointArrows.Read(reader);
  __unnamed7.Read(reader);
  _hudScaleInMultiplayer.Read(reader);
  __unnamed8.Read(reader);
  _defaultWeaponHud.Read(reader);
  _motionSensorRange.Read(reader);
  _motionSensorVelocitySensitivity.Read(reader);
  _motionSensorScaleDONTTOUCHEVER.Read(reader);
  _defaultChapterTitleBounds.Read(reader);
  __unnamed9.Read(reader);
  _topOffset2.Read(reader);
  _bottomOffset2.Read(reader);
  _leftOffset2.Read(reader);
  _rightOffset2.Read(reader);
  __unnamed10.Read(reader);
  _indicatorBitmap.Read(reader);
  _sequenceIndex.Read(reader);
  _multiplayerSequenceIndex.Read(reader);
  _color.Read(reader);
  __unnamed11.Read(reader);
  _defaultColor3.Read(reader);
  _flashingColor3.Read(reader);
  _flashPeriod3.Read(reader);
  _flashDelay3.Read(reader);
  _numberOfFlashes3.Read(reader);
  _flashFlags3.Read(reader);
  _flashLength3.Read(reader);
  _disabledColor3.Read(reader);
  __unnamed12.Read(reader);
  _defaultColor4.Read(reader);
  _flashingColor4.Read(reader);
  _flashPeriod4.Read(reader);
  _flashDelay4.Read(reader);
  _numberOfFlashes4.Read(reader);
  _flashFlags4.Read(reader);
  _flashLength4.Read(reader);
  _disabledColor4.Read(reader);
  __unnamed13.Read(reader);
  __unnamed14.Read(reader);
  _carnageReportBitmap.Read(reader);
  _loadingBeginText.Read(reader);
  _loadingEndText.Read(reader);
  _checkpointBeginText.Read(reader);
  _checkpointEndText.Read(reader);
  _checkpointSound.Read(reader);
  __unnamed15.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_singlePlayerFont.ReadString(reader);
_multiPlayerFont.ReadString(reader);
_itemMessageText.ReadString(reader);
_iconBitmap.ReadString(reader);
_alternateIconText.ReadString(reader);
for (int x=0; x<_buttonIcons.Count; x++)
{
  ButtonIcons.AddNew();
  ButtonIcons[x].Read(reader);
}
for (int x=0; x<_buttonIcons.Count; x++)
  ButtonIcons[x].ReadChildData(reader);
_hudMessages.ReadString(reader);
_arrowBitmap.ReadString(reader);
for (int x=0; x<_waypointArrows.Count; x++)
{
  WaypointArrows.AddNew();
  WaypointArrows[x].Read(reader);
}
for (int x=0; x<_waypointArrows.Count; x++)
  WaypointArrows[x].ReadChildData(reader);
_defaultWeaponHud.ReadString(reader);
_indicatorBitmap.ReadString(reader);
_carnageReportBitmap.ReadString(reader);
_checkpointSound.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _anchor.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    _anchorOffset.Write(writer);
    _widthScale.Write(writer);
    _heightScale.Write(writer);
    _scalingFlags.Write(writer);
    __unnamed3.Write(writer);
    __unnamed4.Write(writer);
    _singlePlayerFont.Write(writer);
    _multiPlayerFont.Write(writer);
    _upTime.Write(writer);
    _fadeTime.Write(writer);
    _iconColor.Write(writer);
    _textColor.Write(writer);
    _textSpacing.Write(writer);
    _itemMessageText.Write(writer);
    _iconBitmap.Write(writer);
    _alternateIconText.Write(writer);
    _buttonIcons.Write(writer);
    _defaultColor.Write(writer);
    _flashingColor.Write(writer);
    _flashPeriod.Write(writer);
    _flashDelay.Write(writer);
    _numberOfFlashes.Write(writer);
    _flashFlags.Write(writer);
    _flashLength.Write(writer);
    _disabledColor.Write(writer);
    __unnamed5.Write(writer);
    _hudMessages.Write(writer);
    _defaultColor2.Write(writer);
    _flashingColor2.Write(writer);
    _flashPeriod2.Write(writer);
    _flashDelay2.Write(writer);
    _numberOfFlashes2.Write(writer);
    _flashFlags2.Write(writer);
    _flashLength2.Write(writer);
    _disabledColor2.Write(writer);
    _uptimeTicks.Write(writer);
    _fadeTicks.Write(writer);
    _topOffset.Write(writer);
    _bottomOffset.Write(writer);
    _leftOffset.Write(writer);
    _rightOffset.Write(writer);
    __unnamed6.Write(writer);
    _arrowBitmap.Write(writer);
    _waypointArrows.Write(writer);
    __unnamed7.Write(writer);
    _hudScaleInMultiplayer.Write(writer);
    __unnamed8.Write(writer);
    _defaultWeaponHud.Write(writer);
    _motionSensorRange.Write(writer);
    _motionSensorVelocitySensitivity.Write(writer);
    _motionSensorScaleDONTTOUCHEVER.Write(writer);
    _defaultChapterTitleBounds.Write(writer);
    __unnamed9.Write(writer);
    _topOffset2.Write(writer);
    _bottomOffset2.Write(writer);
    _leftOffset2.Write(writer);
    _rightOffset2.Write(writer);
    __unnamed10.Write(writer);
    _indicatorBitmap.Write(writer);
    _sequenceIndex.Write(writer);
    _multiplayerSequenceIndex.Write(writer);
    _color.Write(writer);
    __unnamed11.Write(writer);
    _defaultColor3.Write(writer);
    _flashingColor3.Write(writer);
    _flashPeriod3.Write(writer);
    _flashDelay3.Write(writer);
    _numberOfFlashes3.Write(writer);
    _flashFlags3.Write(writer);
    _flashLength3.Write(writer);
    _disabledColor3.Write(writer);
    __unnamed12.Write(writer);
    _defaultColor4.Write(writer);
    _flashingColor4.Write(writer);
    _flashPeriod4.Write(writer);
    _flashDelay4.Write(writer);
    _numberOfFlashes4.Write(writer);
    _flashFlags4.Write(writer);
    _flashLength4.Write(writer);
    _disabledColor4.Write(writer);
    __unnamed13.Write(writer);
    __unnamed14.Write(writer);
    _carnageReportBitmap.Write(writer);
    _loadingBeginText.Write(writer);
    _loadingEndText.Write(writer);
    _checkpointBeginText.Write(writer);
    _checkpointEndText.Write(writer);
    _checkpointSound.Write(writer);
    __unnamed15.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_singlePlayerFont.WriteString(writer);
_multiPlayerFont.WriteString(writer);
_itemMessageText.WriteString(writer);
_iconBitmap.WriteString(writer);
_alternateIconText.WriteString(writer);
_buttonIcons.UpdateReflexiveOffset(writer);
for (int x=0; x<_buttonIcons.Count; x++)
{
  ButtonIcons[x].Write(writer);
}
for (int x=0; x<_buttonIcons.Count; x++)
  ButtonIcons[x].WriteChildData(writer);
_hudMessages.WriteString(writer);
_arrowBitmap.WriteString(writer);
_waypointArrows.UpdateReflexiveOffset(writer);
for (int x=0; x<_waypointArrows.Count; x++)
{
  WaypointArrows[x].Write(writer);
}
for (int x=0; x<_waypointArrows.Count; x++)
  WaypointArrows[x].WriteChildData(writer);
_defaultWeaponHud.WriteString(writer);
_indicatorBitmap.WriteString(writer);
_carnageReportBitmap.WriteString(writer);
_checkpointSound.WriteString(writer);
}
}
public class HudButtonIconBlock : IBlock
{
private ShortInteger _sequenceIndex = new ShortInteger();
private ShortInteger _widthOffset = new ShortInteger();
private Point2D _offsetFromReferenceCorner = new Point2D();
private ARGBColor _overrideIconColor = new ARGBColor();
private CharInteger _frameRate = new CharInteger();
private Flags  _flags;	
private ShortInteger _textIndex = new ShortInteger();
public ShortInteger SequenceIndex
{
  get { return _sequenceIndex; }
  set { _sequenceIndex = value; }
}
public ShortInteger WidthOffset
{
  get { return _widthOffset; }
  set { _widthOffset = value; }
}
public Point2D OffsetFromReferenceCorner
{
  get { return _offsetFromReferenceCorner; }
  set { _offsetFromReferenceCorner = value; }
}
public ARGBColor OverrideIconColor
{
  get { return _overrideIconColor; }
  set { _overrideIconColor = value; }
}
public CharInteger FrameRate
{
  get { return _frameRate; }
  set { _frameRate = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public ShortInteger TextIndex
{
  get { return _textIndex; }
  set { _textIndex = value; }
}
public HudButtonIconBlock()
{
_flags = new Flags(1);

}
public void Read(BinaryReader reader)
{
  _sequenceIndex.Read(reader);
  _widthOffset.Read(reader);
  _offsetFromReferenceCorner.Read(reader);
  _overrideIconColor.Read(reader);
  _frameRate.Read(reader);
  _flags.Read(reader);
  _textIndex.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _sequenceIndex.Write(writer);
    _widthOffset.Write(writer);
    _offsetFromReferenceCorner.Write(writer);
    _overrideIconColor.Write(writer);
    _frameRate.Write(writer);
    _flags.Write(writer);
    _textIndex.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class HudWaypointArrowBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private Pad  __unnamed;	
private RGBColor _color = new RGBColor();
private Real _opacity = new Real();
private Real _translucency = new Real();
private ShortInteger _onScreenSequenceIndex = new ShortInteger();
private ShortInteger _offScreenSequenceIndex = new ShortInteger();
private ShortInteger _occludedSequenceIndex = new ShortInteger();
private Pad  __unnamed2;	
private Pad  __unnamed3;	
private Flags  _flags;	
private Pad  __unnamed4;	
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public RGBColor Color
{
  get { return _color; }
  set { _color = value; }
}
public Real Opacity
{
  get { return _opacity; }
  set { _opacity = value; }
}
public Real Translucency
{
  get { return _translucency; }
  set { _translucency = value; }
}
public ShortInteger OnScreenSequenceIndex
{
  get { return _onScreenSequenceIndex; }
  set { _onScreenSequenceIndex = value; }
}
public ShortInteger OffScreenSequenceIndex
{
  get { return _offScreenSequenceIndex; }
  set { _offScreenSequenceIndex = value; }
}
public ShortInteger OccludedSequenceIndex
{
  get { return _occludedSequenceIndex; }
  set { _occludedSequenceIndex = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public HudWaypointArrowBlock()
{
__unnamed = new Pad(8);
__unnamed2 = new Pad(2);
__unnamed3 = new Pad(16);
_flags = new Flags(4);
__unnamed4 = new Pad(24);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  __unnamed.Read(reader);
  _color.Read(reader);
  _opacity.Read(reader);
  _translucency.Read(reader);
  _onScreenSequenceIndex.Read(reader);
  _offScreenSequenceIndex.Read(reader);
  _occludedSequenceIndex.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  _flags.Read(reader);
  __unnamed4.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    __unnamed.Write(writer);
    _color.Write(writer);
    _opacity.Write(writer);
    _translucency.Write(writer);
    _onScreenSequenceIndex.Write(writer);
    _offScreenSequenceIndex.Write(writer);
    _occludedSequenceIndex.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    _flags.Write(writer);
    __unnamed4.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
  }
}
