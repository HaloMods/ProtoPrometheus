using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class WeaponHudInterface : IBlock
  {
    public WeaponHudInterfaceBlock WeaponHudInterfaceValues = new WeaponHudInterfaceBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'WeaponHudInterface'------------------------------------------------------");
      WeaponHudInterfaceValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      WeaponHudInterfaceValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      WeaponHudInterfaceValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      WeaponHudInterfaceValues.WriteChildData(writer);
    }
public class WeaponHudInterfaceBlock : IBlock
{
private TagReference _childHud = new TagReference();
private Flags  _flags;	
private Pad  __unnamed;	
private ShortInteger _totalAmmoCutoff = new ShortInteger();
private ShortInteger _loadedAmmoCutoff = new ShortInteger();
private ShortInteger _heatCutoff = new ShortInteger();
private ShortInteger _ageCutoff = new ShortInteger();
private Pad  __unnamed2;	
private Enum _anchor = new Enum();
private Pad  __unnamed3;	
private Pad  __unnamed4;	
private Block _staticElements = new Block();
private Block _meterElements = new Block();
private Block _numberElements = new Block();
private Block _crosshairs = new Block();
private Block _overlayElements = new Block();
private Pad  __unnamed5;	
private Pad  __unnamed6;	
private Block _screenEffect = new Block();
private Pad  __unnamed7;	
private ShortInteger _sequenceIndex = new ShortInteger();
private ShortInteger _widthOffset = new ShortInteger();
private Point2D _offsetFromReferenceCorner = new Point2D();
private ARGBColor _overrideIconColor = new ARGBColor();
private CharInteger _frameRate = new CharInteger();
private Flags  _flags2;	
private ShortInteger _textIndex = new ShortInteger();
private Pad  __unnamed8;	
public class WeaponHudStaticBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public WeaponHudStaticBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(WeaponHudStaticBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new WeaponHudStaticBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public WeaponHudStaticBlock this[int index]
  {
    get { return (InnerList[index] as WeaponHudStaticBlock); }
  }
}
private WeaponHudStaticBlockCollection _staticElementsCollection;
public WeaponHudStaticBlockCollection StaticElements
{
  get { return _staticElementsCollection; }
}
public class WeaponHudMeterBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public WeaponHudMeterBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(WeaponHudMeterBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new WeaponHudMeterBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public WeaponHudMeterBlock this[int index]
  {
    get { return (InnerList[index] as WeaponHudMeterBlock); }
  }
}
private WeaponHudMeterBlockCollection _meterElementsCollection;
public WeaponHudMeterBlockCollection MeterElements
{
  get { return _meterElementsCollection; }
}
public class WeaponHudNumberBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public WeaponHudNumberBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(WeaponHudNumberBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new WeaponHudNumberBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public WeaponHudNumberBlock this[int index]
  {
    get { return (InnerList[index] as WeaponHudNumberBlock); }
  }
}
private WeaponHudNumberBlockCollection _numberElementsCollection;
public WeaponHudNumberBlockCollection NumberElements
{
  get { return _numberElementsCollection; }
}
public class WeaponHudCrosshairBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public WeaponHudCrosshairBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(WeaponHudCrosshairBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new WeaponHudCrosshairBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public WeaponHudCrosshairBlock this[int index]
  {
    get { return (InnerList[index] as WeaponHudCrosshairBlock); }
  }
}
private WeaponHudCrosshairBlockCollection _crosshairsCollection;
public WeaponHudCrosshairBlockCollection Crosshairs
{
  get { return _crosshairsCollection; }
}
public class WeaponHudOverlaysBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public WeaponHudOverlaysBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(WeaponHudOverlaysBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new WeaponHudOverlaysBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public WeaponHudOverlaysBlock this[int index]
  {
    get { return (InnerList[index] as WeaponHudOverlaysBlock); }
  }
}
private WeaponHudOverlaysBlockCollection _overlayElementsCollection;
public WeaponHudOverlaysBlockCollection OverlayElements
{
  get { return _overlayElementsCollection; }
}
public class GlobalHudScreenEffectDefinitionBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public GlobalHudScreenEffectDefinitionBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(GlobalHudScreenEffectDefinitionBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new GlobalHudScreenEffectDefinitionBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public GlobalHudScreenEffectDefinitionBlock this[int index]
  {
    get { return (InnerList[index] as GlobalHudScreenEffectDefinitionBlock); }
  }
}
private GlobalHudScreenEffectDefinitionBlockCollection _screenEffectCollection;
public GlobalHudScreenEffectDefinitionBlockCollection ScreenEffect
{
  get { return _screenEffectCollection; }
}
public TagReference ChildHud
{
  get { return _childHud; }
  set { _childHud = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public ShortInteger TotalAmmoCutoff
{
  get { return _totalAmmoCutoff; }
  set { _totalAmmoCutoff = value; }
}
public ShortInteger LoadedAmmoCutoff
{
  get { return _loadedAmmoCutoff; }
  set { _loadedAmmoCutoff = value; }
}
public ShortInteger HeatCutoff
{
  get { return _heatCutoff; }
  set { _heatCutoff = value; }
}
public ShortInteger AgeCutoff
{
  get { return _ageCutoff; }
  set { _ageCutoff = value; }
}
public Enum Anchor
{
  get { return _anchor; }
  set { _anchor = value; }
}
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
public Flags Flags2
{
  get { return _flags2; }
  set { _flags2 = value; }
}
public ShortInteger TextIndex
{
  get { return _textIndex; }
  set { _textIndex = value; }
}
public WeaponHudInterfaceBlock()
{
_flags = new Flags(2);
__unnamed = new Pad(2);
__unnamed2 = new Pad(32);
__unnamed3 = new Pad(2);
__unnamed4 = new Pad(32);
__unnamed5 = new Pad(4);
__unnamed6 = new Pad(12);
__unnamed7 = new Pad(132);
_flags2 = new Flags(1);
__unnamed8 = new Pad(48);
_staticElementsCollection = new WeaponHudStaticBlockCollection(_staticElements);
_meterElementsCollection = new WeaponHudMeterBlockCollection(_meterElements);
_numberElementsCollection = new WeaponHudNumberBlockCollection(_numberElements);
_crosshairsCollection = new WeaponHudCrosshairBlockCollection(_crosshairs);
_overlayElementsCollection = new WeaponHudOverlaysBlockCollection(_overlayElements);
_screenEffectCollection = new GlobalHudScreenEffectDefinitionBlockCollection(_screenEffect);

}
public void Read(BinaryReader reader)
{
  _childHud.Read(reader);
  _flags.Read(reader);
  __unnamed.Read(reader);
  _totalAmmoCutoff.Read(reader);
  _loadedAmmoCutoff.Read(reader);
  _heatCutoff.Read(reader);
  _ageCutoff.Read(reader);
  __unnamed2.Read(reader);
  _anchor.Read(reader);
  __unnamed3.Read(reader);
  __unnamed4.Read(reader);
  _staticElements.Read(reader);
  _meterElements.Read(reader);
  _numberElements.Read(reader);
  _crosshairs.Read(reader);
  _overlayElements.Read(reader);
  __unnamed5.Read(reader);
  __unnamed6.Read(reader);
  _screenEffect.Read(reader);
  __unnamed7.Read(reader);
  _sequenceIndex.Read(reader);
  _widthOffset.Read(reader);
  _offsetFromReferenceCorner.Read(reader);
  _overrideIconColor.Read(reader);
  _frameRate.Read(reader);
  _flags2.Read(reader);
  _textIndex.Read(reader);
  __unnamed8.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_childHud.ReadString(reader);
for (int x=0; x<_staticElements.Count; x++)
{
  StaticElements.AddNew();
  StaticElements[x].Read(reader);
}
for (int x=0; x<_staticElements.Count; x++)
  StaticElements[x].ReadChildData(reader);
for (int x=0; x<_meterElements.Count; x++)
{
  MeterElements.AddNew();
  MeterElements[x].Read(reader);
}
for (int x=0; x<_meterElements.Count; x++)
  MeterElements[x].ReadChildData(reader);
for (int x=0; x<_numberElements.Count; x++)
{
  NumberElements.AddNew();
  NumberElements[x].Read(reader);
}
for (int x=0; x<_numberElements.Count; x++)
  NumberElements[x].ReadChildData(reader);
for (int x=0; x<_crosshairs.Count; x++)
{
  Crosshairs.AddNew();
  Crosshairs[x].Read(reader);
}
for (int x=0; x<_crosshairs.Count; x++)
  Crosshairs[x].ReadChildData(reader);
for (int x=0; x<_overlayElements.Count; x++)
{
  OverlayElements.AddNew();
  OverlayElements[x].Read(reader);
}
for (int x=0; x<_overlayElements.Count; x++)
  OverlayElements[x].ReadChildData(reader);
for (int x=0; x<_screenEffect.Count; x++)
{
  ScreenEffect.AddNew();
  ScreenEffect[x].Read(reader);
}
for (int x=0; x<_screenEffect.Count; x++)
  ScreenEffect[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _childHud.Write(writer);
    _flags.Write(writer);
    __unnamed.Write(writer);
    _totalAmmoCutoff.Write(writer);
    _loadedAmmoCutoff.Write(writer);
    _heatCutoff.Write(writer);
    _ageCutoff.Write(writer);
    __unnamed2.Write(writer);
    _anchor.Write(writer);
    __unnamed3.Write(writer);
    __unnamed4.Write(writer);
    _staticElements.Write(writer);
    _meterElements.Write(writer);
    _numberElements.Write(writer);
    _crosshairs.Write(writer);
    _overlayElements.Write(writer);
    __unnamed5.Write(writer);
    __unnamed6.Write(writer);
    _screenEffect.Write(writer);
    __unnamed7.Write(writer);
    _sequenceIndex.Write(writer);
    _widthOffset.Write(writer);
    _offsetFromReferenceCorner.Write(writer);
    _overrideIconColor.Write(writer);
    _frameRate.Write(writer);
    _flags2.Write(writer);
    _textIndex.Write(writer);
    __unnamed8.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_childHud.WriteString(writer);
_staticElements.UpdateReflexiveOffset(writer);
for (int x=0; x<_staticElements.Count; x++)
{
  StaticElements[x].Write(writer);
}
for (int x=0; x<_staticElements.Count; x++)
  StaticElements[x].WriteChildData(writer);
_meterElements.UpdateReflexiveOffset(writer);
for (int x=0; x<_meterElements.Count; x++)
{
  MeterElements[x].Write(writer);
}
for (int x=0; x<_meterElements.Count; x++)
  MeterElements[x].WriteChildData(writer);
_numberElements.UpdateReflexiveOffset(writer);
for (int x=0; x<_numberElements.Count; x++)
{
  NumberElements[x].Write(writer);
}
for (int x=0; x<_numberElements.Count; x++)
  NumberElements[x].WriteChildData(writer);
_crosshairs.UpdateReflexiveOffset(writer);
for (int x=0; x<_crosshairs.Count; x++)
{
  Crosshairs[x].Write(writer);
}
for (int x=0; x<_crosshairs.Count; x++)
  Crosshairs[x].WriteChildData(writer);
_overlayElements.UpdateReflexiveOffset(writer);
for (int x=0; x<_overlayElements.Count; x++)
{
  OverlayElements[x].Write(writer);
}
for (int x=0; x<_overlayElements.Count; x++)
  OverlayElements[x].WriteChildData(writer);
_screenEffect.UpdateReflexiveOffset(writer);
for (int x=0; x<_screenEffect.Count; x++)
{
  ScreenEffect[x].Write(writer);
}
for (int x=0; x<_screenEffect.Count; x++)
  ScreenEffect[x].WriteChildData(writer);
}
}
public class WeaponHudStaticBlock : IBlock
{
private Enum _stateAttachedTo = new Enum();
private Pad  __unnamed;	
private Enum _canUseOnMapType = new Enum();
private Pad  __unnamed2;	
private Pad  __unnamed3;	
private Point2D _anchorOffset = new Point2D();
private Real _widthScale = new Real();
private Real _heightScale = new Real();
private Flags  _scalingFlags;	
private Pad  __unnamed4;	
private Pad  __unnamed5;	
private TagReference _interfaceBitmap = new TagReference();
private ARGBColor _defaultColor = new ARGBColor();
private ARGBColor _flashingColor = new ARGBColor();
private Real _flashPeriod = new Real();
private Real _flashDelay = new Real();
private ShortInteger _numberOfFlashes = new ShortInteger();
private Flags  _flashFlags;	
private Real _flashLength = new Real();
private ARGBColor _disabledColor = new ARGBColor();
private Pad  __unnamed6;	
private ShortInteger _sequenceIndex = new ShortInteger();
private Pad  __unnamed7;	
private Block _multitexOverlay = new Block();
private Pad  __unnamed8;	
private Pad  __unnamed9;	
public class GlobalHudMultitextureOverlayDefinitionBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public GlobalHudMultitextureOverlayDefinitionBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(GlobalHudMultitextureOverlayDefinitionBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new GlobalHudMultitextureOverlayDefinitionBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public GlobalHudMultitextureOverlayDefinitionBlock this[int index]
  {
    get { return (InnerList[index] as GlobalHudMultitextureOverlayDefinitionBlock); }
  }
}
private GlobalHudMultitextureOverlayDefinitionBlockCollection _multitexOverlayCollection;
public GlobalHudMultitextureOverlayDefinitionBlockCollection MultitexOverlay
{
  get { return _multitexOverlayCollection; }
}
public Enum StateAttachedTo
{
  get { return _stateAttachedTo; }
  set { _stateAttachedTo = value; }
}
public Enum CanUseOnMapType
{
  get { return _canUseOnMapType; }
  set { _canUseOnMapType = value; }
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
public TagReference InterfaceBitmap
{
  get { return _interfaceBitmap; }
  set { _interfaceBitmap = value; }
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
public ShortInteger SequenceIndex
{
  get { return _sequenceIndex; }
  set { _sequenceIndex = value; }
}
public WeaponHudStaticBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(2);
__unnamed3 = new Pad(28);
_scalingFlags = new Flags(2);
__unnamed4 = new Pad(2);
__unnamed5 = new Pad(20);
_flashFlags = new Flags(2);
__unnamed6 = new Pad(4);
__unnamed7 = new Pad(2);
__unnamed8 = new Pad(4);
__unnamed9 = new Pad(40);
_multitexOverlayCollection = new GlobalHudMultitextureOverlayDefinitionBlockCollection(_multitexOverlay);

}
public void Read(BinaryReader reader)
{
  _stateAttachedTo.Read(reader);
  __unnamed.Read(reader);
  _canUseOnMapType.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  _anchorOffset.Read(reader);
  _widthScale.Read(reader);
  _heightScale.Read(reader);
  _scalingFlags.Read(reader);
  __unnamed4.Read(reader);
  __unnamed5.Read(reader);
  _interfaceBitmap.Read(reader);
  _defaultColor.Read(reader);
  _flashingColor.Read(reader);
  _flashPeriod.Read(reader);
  _flashDelay.Read(reader);
  _numberOfFlashes.Read(reader);
  _flashFlags.Read(reader);
  _flashLength.Read(reader);
  _disabledColor.Read(reader);
  __unnamed6.Read(reader);
  _sequenceIndex.Read(reader);
  __unnamed7.Read(reader);
  _multitexOverlay.Read(reader);
  __unnamed8.Read(reader);
  __unnamed9.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_interfaceBitmap.ReadString(reader);
for (int x=0; x<_multitexOverlay.Count; x++)
{
  MultitexOverlay.AddNew();
  MultitexOverlay[x].Read(reader);
}
for (int x=0; x<_multitexOverlay.Count; x++)
  MultitexOverlay[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _stateAttachedTo.Write(writer);
    __unnamed.Write(writer);
    _canUseOnMapType.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    _anchorOffset.Write(writer);
    _widthScale.Write(writer);
    _heightScale.Write(writer);
    _scalingFlags.Write(writer);
    __unnamed4.Write(writer);
    __unnamed5.Write(writer);
    _interfaceBitmap.Write(writer);
    _defaultColor.Write(writer);
    _flashingColor.Write(writer);
    _flashPeriod.Write(writer);
    _flashDelay.Write(writer);
    _numberOfFlashes.Write(writer);
    _flashFlags.Write(writer);
    _flashLength.Write(writer);
    _disabledColor.Write(writer);
    __unnamed6.Write(writer);
    _sequenceIndex.Write(writer);
    __unnamed7.Write(writer);
    _multitexOverlay.Write(writer);
    __unnamed8.Write(writer);
    __unnamed9.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_interfaceBitmap.WriteString(writer);
_multitexOverlay.UpdateReflexiveOffset(writer);
for (int x=0; x<_multitexOverlay.Count; x++)
{
  MultitexOverlay[x].Write(writer);
}
for (int x=0; x<_multitexOverlay.Count; x++)
  MultitexOverlay[x].WriteChildData(writer);
}
}
public class GlobalHudMultitextureOverlayDefinitionBlock : IBlock
{
private Pad  __unnamed;	
private ShortInteger _type = new ShortInteger();
private Enum _framebufferBlendFunc = new Enum();
private Pad  __unnamed2;	
private Pad  __unnamed3;	
private Enum _primaryAnchor = new Enum();
private Enum _secondaryAnchor = new Enum();
private Enum _tertiaryAnchor = new Enum();
private Enum __0To1BlendFunc = new Enum();
private Enum __1To2BlendFunc = new Enum();
private Pad  __unnamed4;	
private RealPoint2D _primaryScale = new RealPoint2D();
private RealPoint2D _secondaryScale = new RealPoint2D();
private RealPoint2D _tertiaryScale = new RealPoint2D();
private RealPoint2D _primaryOffset = new RealPoint2D();
private RealPoint2D _secondaryOffset = new RealPoint2D();
private RealPoint2D _tertiaryOffset = new RealPoint2D();
private TagReference _primary = new TagReference();
private TagReference _secondary = new TagReference();
private TagReference _tertiary = new TagReference();
private Enum _primaryWrapMode = new Enum();
private Enum _secondaryWrapMode = new Enum();
private Enum _tertiaryWrapMode = new Enum();
private Pad  __unnamed5;	
private Pad  __unnamed6;	
private Block _effectors = new Block();
private Pad  __unnamed7;	
public class GlobalHudMultitextureOverlayEffectorDefinitionBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public GlobalHudMultitextureOverlayEffectorDefinitionBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(GlobalHudMultitextureOverlayEffectorDefinitionBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new GlobalHudMultitextureOverlayEffectorDefinitionBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public GlobalHudMultitextureOverlayEffectorDefinitionBlock this[int index]
  {
    get { return (InnerList[index] as GlobalHudMultitextureOverlayEffectorDefinitionBlock); }
  }
}
private GlobalHudMultitextureOverlayEffectorDefinitionBlockCollection _effectorsCollection;
public GlobalHudMultitextureOverlayEffectorDefinitionBlockCollection Effectors
{
  get { return _effectorsCollection; }
}
public ShortInteger Type
{
  get { return _type; }
  set { _type = value; }
}
public Enum FramebufferBlendFunc
{
  get { return _framebufferBlendFunc; }
  set { _framebufferBlendFunc = value; }
}
public Enum PrimaryAnchor
{
  get { return _primaryAnchor; }
  set { _primaryAnchor = value; }
}
public Enum SecondaryAnchor
{
  get { return _secondaryAnchor; }
  set { _secondaryAnchor = value; }
}
public Enum TertiaryAnchor
{
  get { return _tertiaryAnchor; }
  set { _tertiaryAnchor = value; }
}
public Enum _0To1BlendFunc
{
  get { return __0To1BlendFunc; }
  set { __0To1BlendFunc = value; }
}
public Enum _1To2BlendFunc
{
  get { return __1To2BlendFunc; }
  set { __1To2BlendFunc = value; }
}
public RealPoint2D PrimaryScale
{
  get { return _primaryScale; }
  set { _primaryScale = value; }
}
public RealPoint2D SecondaryScale
{
  get { return _secondaryScale; }
  set { _secondaryScale = value; }
}
public RealPoint2D TertiaryScale
{
  get { return _tertiaryScale; }
  set { _tertiaryScale = value; }
}
public RealPoint2D PrimaryOffset
{
  get { return _primaryOffset; }
  set { _primaryOffset = value; }
}
public RealPoint2D SecondaryOffset
{
  get { return _secondaryOffset; }
  set { _secondaryOffset = value; }
}
public RealPoint2D TertiaryOffset
{
  get { return _tertiaryOffset; }
  set { _tertiaryOffset = value; }
}
public TagReference Primary
{
  get { return _primary; }
  set { _primary = value; }
}
public TagReference Secondary
{
  get { return _secondary; }
  set { _secondary = value; }
}
public TagReference Tertiary
{
  get { return _tertiary; }
  set { _tertiary = value; }
}
public Enum PrimaryWrapMode
{
  get { return _primaryWrapMode; }
  set { _primaryWrapMode = value; }
}
public Enum SecondaryWrapMode
{
  get { return _secondaryWrapMode; }
  set { _secondaryWrapMode = value; }
}
public Enum TertiaryWrapMode
{
  get { return _tertiaryWrapMode; }
  set { _tertiaryWrapMode = value; }
}
public GlobalHudMultitextureOverlayDefinitionBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(2);
__unnamed3 = new Pad(32);
__unnamed4 = new Pad(2);
__unnamed5 = new Pad(2);
__unnamed6 = new Pad(184);
__unnamed7 = new Pad(128);
_effectorsCollection = new GlobalHudMultitextureOverlayEffectorDefinitionBlockCollection(_effectors);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  _type.Read(reader);
  _framebufferBlendFunc.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  _primaryAnchor.Read(reader);
  _secondaryAnchor.Read(reader);
  _tertiaryAnchor.Read(reader);
  __0To1BlendFunc.Read(reader);
  __1To2BlendFunc.Read(reader);
  __unnamed4.Read(reader);
  _primaryScale.Read(reader);
  _secondaryScale.Read(reader);
  _tertiaryScale.Read(reader);
  _primaryOffset.Read(reader);
  _secondaryOffset.Read(reader);
  _tertiaryOffset.Read(reader);
  _primary.Read(reader);
  _secondary.Read(reader);
  _tertiary.Read(reader);
  _primaryWrapMode.Read(reader);
  _secondaryWrapMode.Read(reader);
  _tertiaryWrapMode.Read(reader);
  __unnamed5.Read(reader);
  __unnamed6.Read(reader);
  _effectors.Read(reader);
  __unnamed7.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_primary.ReadString(reader);
_secondary.ReadString(reader);
_tertiary.ReadString(reader);
for (int x=0; x<_effectors.Count; x++)
{
  Effectors.AddNew();
  Effectors[x].Read(reader);
}
for (int x=0; x<_effectors.Count; x++)
  Effectors[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    _type.Write(writer);
    _framebufferBlendFunc.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    _primaryAnchor.Write(writer);
    _secondaryAnchor.Write(writer);
    _tertiaryAnchor.Write(writer);
    __0To1BlendFunc.Write(writer);
    __1To2BlendFunc.Write(writer);
    __unnamed4.Write(writer);
    _primaryScale.Write(writer);
    _secondaryScale.Write(writer);
    _tertiaryScale.Write(writer);
    _primaryOffset.Write(writer);
    _secondaryOffset.Write(writer);
    _tertiaryOffset.Write(writer);
    _primary.Write(writer);
    _secondary.Write(writer);
    _tertiary.Write(writer);
    _primaryWrapMode.Write(writer);
    _secondaryWrapMode.Write(writer);
    _tertiaryWrapMode.Write(writer);
    __unnamed5.Write(writer);
    __unnamed6.Write(writer);
    _effectors.Write(writer);
    __unnamed7.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_primary.WriteString(writer);
_secondary.WriteString(writer);
_tertiary.WriteString(writer);
_effectors.UpdateReflexiveOffset(writer);
for (int x=0; x<_effectors.Count; x++)
{
  Effectors[x].Write(writer);
}
for (int x=0; x<_effectors.Count; x++)
  Effectors[x].WriteChildData(writer);
}
}
public class GlobalHudMultitextureOverlayEffectorDefinitionBlock : IBlock
{
private Pad  __unnamed;	
private Enum _destinationType = new Enum();
private Enum _destination = new Enum();
private Enum _source = new Enum();
private Pad  __unnamed2;	
private RealBounds _inBounds = new RealBounds();
private RealBounds _outBounds = new RealBounds();
private Pad  __unnamed3;	
private RealRGBColor _tintColorLowerBound = new RealRGBColor();
private RealRGBColor _tintColorUpperBound = new RealRGBColor();
private Enum _periodicFunction = new Enum();
private Pad  __unnamed4;	
private Real _functionPeriod = new Real();
private Real _functionPhase = new Real();
private Pad  __unnamed5;	
public Enum DestinationType
{
  get { return _destinationType; }
  set { _destinationType = value; }
}
public Enum Destination
{
  get { return _destination; }
  set { _destination = value; }
}
public Enum Source
{
  get { return _source; }
  set { _source = value; }
}
public RealBounds InBounds
{
  get { return _inBounds; }
  set { _inBounds = value; }
}
public RealBounds OutBounds
{
  get { return _outBounds; }
  set { _outBounds = value; }
}
public RealRGBColor TintColorLowerBound
{
  get { return _tintColorLowerBound; }
  set { _tintColorLowerBound = value; }
}
public RealRGBColor TintColorUpperBound
{
  get { return _tintColorUpperBound; }
  set { _tintColorUpperBound = value; }
}
public Enum PeriodicFunction
{
  get { return _periodicFunction; }
  set { _periodicFunction = value; }
}
public Real FunctionPeriod
{
  get { return _functionPeriod; }
  set { _functionPeriod = value; }
}
public Real FunctionPhase
{
  get { return _functionPhase; }
  set { _functionPhase = value; }
}
public GlobalHudMultitextureOverlayEffectorDefinitionBlock()
{
__unnamed = new Pad(64);
__unnamed2 = new Pad(2);
__unnamed3 = new Pad(64);
__unnamed4 = new Pad(2);
__unnamed5 = new Pad(32);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  _destinationType.Read(reader);
  _destination.Read(reader);
  _source.Read(reader);
  __unnamed2.Read(reader);
  _inBounds.Read(reader);
  _outBounds.Read(reader);
  __unnamed3.Read(reader);
  _tintColorLowerBound.Read(reader);
  _tintColorUpperBound.Read(reader);
  _periodicFunction.Read(reader);
  __unnamed4.Read(reader);
  _functionPeriod.Read(reader);
  _functionPhase.Read(reader);
  __unnamed5.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    _destinationType.Write(writer);
    _destination.Write(writer);
    _source.Write(writer);
    __unnamed2.Write(writer);
    _inBounds.Write(writer);
    _outBounds.Write(writer);
    __unnamed3.Write(writer);
    _tintColorLowerBound.Write(writer);
    _tintColorUpperBound.Write(writer);
    _periodicFunction.Write(writer);
    __unnamed4.Write(writer);
    _functionPeriod.Write(writer);
    _functionPhase.Write(writer);
    __unnamed5.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class WeaponHudMeterBlock : IBlock
{
private Enum _stateAttachedTo = new Enum();
private Pad  __unnamed;	
private Enum _canUseOnMapType = new Enum();
private Pad  __unnamed2;	
private Pad  __unnamed3;	
private Point2D _anchorOffset = new Point2D();
private Real _widthScale = new Real();
private Real _heightScale = new Real();
private Flags  _scalingFlags;	
private Pad  __unnamed4;	
private Pad  __unnamed5;	
private TagReference _meterBitmap = new TagReference();
private RGBColor _colorAtMeterMinimum = new RGBColor();
private RGBColor _colorAtMeterMaximum = new RGBColor();
private RGBColor _flashColor = new RGBColor();
private ARGBColor _emptyColor = new ARGBColor();
private Flags  _flags;	
private CharInteger _minumumMeterValue = new CharInteger();
private ShortInteger _sequenceIndex = new ShortInteger();
private CharInteger _alphaMultiplier = new CharInteger();
private CharInteger _alphaBias = new CharInteger();
private ShortInteger _valueScale = new ShortInteger();
private Real _opacity = new Real();
private Real _translucency = new Real();
private ARGBColor _disabledColor = new ARGBColor();
private Pad  __unnamed6;	
private Pad  __unnamed7;	
public Enum StateAttachedTo
{
  get { return _stateAttachedTo; }
  set { _stateAttachedTo = value; }
}
public Enum CanUseOnMapType
{
  get { return _canUseOnMapType; }
  set { _canUseOnMapType = value; }
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
public TagReference MeterBitmap
{
  get { return _meterBitmap; }
  set { _meterBitmap = value; }
}
public RGBColor ColorAtMeterMinimum
{
  get { return _colorAtMeterMinimum; }
  set { _colorAtMeterMinimum = value; }
}
public RGBColor ColorAtMeterMaximum
{
  get { return _colorAtMeterMaximum; }
  set { _colorAtMeterMaximum = value; }
}
public RGBColor FlashColor
{
  get { return _flashColor; }
  set { _flashColor = value; }
}
public ARGBColor EmptyColor
{
  get { return _emptyColor; }
  set { _emptyColor = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public CharInteger MinumumMeterValue
{
  get { return _minumumMeterValue; }
  set { _minumumMeterValue = value; }
}
public ShortInteger SequenceIndex
{
  get { return _sequenceIndex; }
  set { _sequenceIndex = value; }
}
public CharInteger AlphaMultiplier
{
  get { return _alphaMultiplier; }
  set { _alphaMultiplier = value; }
}
public CharInteger AlphaBias
{
  get { return _alphaBias; }
  set { _alphaBias = value; }
}
public ShortInteger ValueScale
{
  get { return _valueScale; }
  set { _valueScale = value; }
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
public ARGBColor DisabledColor
{
  get { return _disabledColor; }
  set { _disabledColor = value; }
}
public WeaponHudMeterBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(2);
__unnamed3 = new Pad(28);
_scalingFlags = new Flags(2);
__unnamed4 = new Pad(2);
__unnamed5 = new Pad(20);
_flags = new Flags(1);
__unnamed6 = new Pad(16);
__unnamed7 = new Pad(40);

}
public void Read(BinaryReader reader)
{
  _stateAttachedTo.Read(reader);
  __unnamed.Read(reader);
  _canUseOnMapType.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  _anchorOffset.Read(reader);
  _widthScale.Read(reader);
  _heightScale.Read(reader);
  _scalingFlags.Read(reader);
  __unnamed4.Read(reader);
  __unnamed5.Read(reader);
  _meterBitmap.Read(reader);
  _colorAtMeterMinimum.Read(reader);
  _colorAtMeterMaximum.Read(reader);
  _flashColor.Read(reader);
  _emptyColor.Read(reader);
  _flags.Read(reader);
  _minumumMeterValue.Read(reader);
  _sequenceIndex.Read(reader);
  _alphaMultiplier.Read(reader);
  _alphaBias.Read(reader);
  _valueScale.Read(reader);
  _opacity.Read(reader);
  _translucency.Read(reader);
  _disabledColor.Read(reader);
  __unnamed6.Read(reader);
  __unnamed7.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_meterBitmap.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _stateAttachedTo.Write(writer);
    __unnamed.Write(writer);
    _canUseOnMapType.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    _anchorOffset.Write(writer);
    _widthScale.Write(writer);
    _heightScale.Write(writer);
    _scalingFlags.Write(writer);
    __unnamed4.Write(writer);
    __unnamed5.Write(writer);
    _meterBitmap.Write(writer);
    _colorAtMeterMinimum.Write(writer);
    _colorAtMeterMaximum.Write(writer);
    _flashColor.Write(writer);
    _emptyColor.Write(writer);
    _flags.Write(writer);
    _minumumMeterValue.Write(writer);
    _sequenceIndex.Write(writer);
    _alphaMultiplier.Write(writer);
    _alphaBias.Write(writer);
    _valueScale.Write(writer);
    _opacity.Write(writer);
    _translucency.Write(writer);
    _disabledColor.Write(writer);
    __unnamed6.Write(writer);
    __unnamed7.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_meterBitmap.WriteString(writer);
}
}
public class WeaponHudNumberBlock : IBlock
{
private Enum _stateAttachedTo = new Enum();
private Pad  __unnamed;	
private Enum _canUseOnMapType = new Enum();
private Pad  __unnamed2;	
private Pad  __unnamed3;	
private Point2D _anchorOffset = new Point2D();
private Real _widthScale = new Real();
private Real _heightScale = new Real();
private Flags  _scalingFlags;	
private Pad  __unnamed4;	
private Pad  __unnamed5;	
private ARGBColor _defaultColor = new ARGBColor();
private ARGBColor _flashingColor = new ARGBColor();
private Real _flashPeriod = new Real();
private Real _flashDelay = new Real();
private ShortInteger _numberOfFlashes = new ShortInteger();
private Flags  _flashFlags;	
private Real _flashLength = new Real();
private ARGBColor _disabledColor = new ARGBColor();
private Pad  __unnamed6;	
private CharInteger _maximumNumberOfDigits = new CharInteger();
private Flags  _flags;	
private CharInteger _numberOfFractionalDigits = new CharInteger();
private Pad  __unnamed7;	
private Pad  __unnamed8;	
private Flags  _weaponSpecificFlags;	
private Pad  __unnamed9;	
private Pad  __unnamed10;	
public Enum StateAttachedTo
{
  get { return _stateAttachedTo; }
  set { _stateAttachedTo = value; }
}
public Enum CanUseOnMapType
{
  get { return _canUseOnMapType; }
  set { _canUseOnMapType = value; }
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
public CharInteger MaximumNumberOfDigits
{
  get { return _maximumNumberOfDigits; }
  set { _maximumNumberOfDigits = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public CharInteger NumberOfFractionalDigits
{
  get { return _numberOfFractionalDigits; }
  set { _numberOfFractionalDigits = value; }
}
public Flags WeaponSpecificFlags
{
  get { return _weaponSpecificFlags; }
  set { _weaponSpecificFlags = value; }
}
public WeaponHudNumberBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(2);
__unnamed3 = new Pad(28);
_scalingFlags = new Flags(2);
__unnamed4 = new Pad(2);
__unnamed5 = new Pad(20);
_flashFlags = new Flags(2);
__unnamed6 = new Pad(4);
_flags = new Flags(1);
__unnamed7 = new Pad(1);
__unnamed8 = new Pad(12);
_weaponSpecificFlags = new Flags(2);
__unnamed9 = new Pad(2);
__unnamed10 = new Pad(36);

}
public void Read(BinaryReader reader)
{
  _stateAttachedTo.Read(reader);
  __unnamed.Read(reader);
  _canUseOnMapType.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  _anchorOffset.Read(reader);
  _widthScale.Read(reader);
  _heightScale.Read(reader);
  _scalingFlags.Read(reader);
  __unnamed4.Read(reader);
  __unnamed5.Read(reader);
  _defaultColor.Read(reader);
  _flashingColor.Read(reader);
  _flashPeriod.Read(reader);
  _flashDelay.Read(reader);
  _numberOfFlashes.Read(reader);
  _flashFlags.Read(reader);
  _flashLength.Read(reader);
  _disabledColor.Read(reader);
  __unnamed6.Read(reader);
  _maximumNumberOfDigits.Read(reader);
  _flags.Read(reader);
  _numberOfFractionalDigits.Read(reader);
  __unnamed7.Read(reader);
  __unnamed8.Read(reader);
  _weaponSpecificFlags.Read(reader);
  __unnamed9.Read(reader);
  __unnamed10.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _stateAttachedTo.Write(writer);
    __unnamed.Write(writer);
    _canUseOnMapType.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    _anchorOffset.Write(writer);
    _widthScale.Write(writer);
    _heightScale.Write(writer);
    _scalingFlags.Write(writer);
    __unnamed4.Write(writer);
    __unnamed5.Write(writer);
    _defaultColor.Write(writer);
    _flashingColor.Write(writer);
    _flashPeriod.Write(writer);
    _flashDelay.Write(writer);
    _numberOfFlashes.Write(writer);
    _flashFlags.Write(writer);
    _flashLength.Write(writer);
    _disabledColor.Write(writer);
    __unnamed6.Write(writer);
    _maximumNumberOfDigits.Write(writer);
    _flags.Write(writer);
    _numberOfFractionalDigits.Write(writer);
    __unnamed7.Write(writer);
    __unnamed8.Write(writer);
    _weaponSpecificFlags.Write(writer);
    __unnamed9.Write(writer);
    __unnamed10.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class WeaponHudCrosshairBlock : IBlock
{
private Enum _crosshairType = new Enum();
private Pad  __unnamed;	
private Enum _canUseOnMapType = new Enum();
private Pad  __unnamed2;	
private Pad  __unnamed3;	
private TagReference _crosshairBitmap = new TagReference();
private Block _crosshairOverlays = new Block();
private Pad  __unnamed4;	
public class WeaponHudCrosshairItemBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public WeaponHudCrosshairItemBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(WeaponHudCrosshairItemBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new WeaponHudCrosshairItemBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public WeaponHudCrosshairItemBlock this[int index]
  {
    get { return (InnerList[index] as WeaponHudCrosshairItemBlock); }
  }
}
private WeaponHudCrosshairItemBlockCollection _crosshairOverlaysCollection;
public WeaponHudCrosshairItemBlockCollection CrosshairOverlays
{
  get { return _crosshairOverlaysCollection; }
}
public Enum CrosshairType
{
  get { return _crosshairType; }
  set { _crosshairType = value; }
}
public Enum CanUseOnMapType
{
  get { return _canUseOnMapType; }
  set { _canUseOnMapType = value; }
}
public TagReference CrosshairBitmap
{
  get { return _crosshairBitmap; }
  set { _crosshairBitmap = value; }
}
public WeaponHudCrosshairBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(2);
__unnamed3 = new Pad(28);
__unnamed4 = new Pad(40);
_crosshairOverlaysCollection = new WeaponHudCrosshairItemBlockCollection(_crosshairOverlays);

}
public void Read(BinaryReader reader)
{
  _crosshairType.Read(reader);
  __unnamed.Read(reader);
  _canUseOnMapType.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  _crosshairBitmap.Read(reader);
  _crosshairOverlays.Read(reader);
  __unnamed4.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_crosshairBitmap.ReadString(reader);
for (int x=0; x<_crosshairOverlays.Count; x++)
{
  CrosshairOverlays.AddNew();
  CrosshairOverlays[x].Read(reader);
}
for (int x=0; x<_crosshairOverlays.Count; x++)
  CrosshairOverlays[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _crosshairType.Write(writer);
    __unnamed.Write(writer);
    _canUseOnMapType.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    _crosshairBitmap.Write(writer);
    _crosshairOverlays.Write(writer);
    __unnamed4.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_crosshairBitmap.WriteString(writer);
_crosshairOverlays.UpdateReflexiveOffset(writer);
for (int x=0; x<_crosshairOverlays.Count; x++)
{
  CrosshairOverlays[x].Write(writer);
}
for (int x=0; x<_crosshairOverlays.Count; x++)
  CrosshairOverlays[x].WriteChildData(writer);
}
}
public class WeaponHudCrosshairItemBlock : IBlock
{
private Point2D _anchorOffset = new Point2D();
private Real _widthScale = new Real();
private Real _heightScale = new Real();
private Flags  _scalingFlags;	
private Pad  __unnamed;	
private Pad  __unnamed2;	
private ARGBColor _defaultColor = new ARGBColor();
private ARGBColor _flashingColor = new ARGBColor();
private Real _flashPeriod = new Real();
private Real _flashDelay = new Real();
private ShortInteger _numberOfFlashes = new ShortInteger();
private Flags  _flashFlags;	
private Real _flashLength = new Real();
private ARGBColor _disabledColor = new ARGBColor();
private Pad  __unnamed3;	
private ShortInteger _frameRate = new ShortInteger();
private ShortInteger _sequenceIndex = new ShortInteger();
private Flags  _flags;	
private Pad  __unnamed4;	
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
public ShortInteger FrameRate
{
  get { return _frameRate; }
  set { _frameRate = value; }
}
public ShortInteger SequenceIndex
{
  get { return _sequenceIndex; }
  set { _sequenceIndex = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public WeaponHudCrosshairItemBlock()
{
_scalingFlags = new Flags(2);
__unnamed = new Pad(2);
__unnamed2 = new Pad(20);
_flashFlags = new Flags(2);
__unnamed3 = new Pad(4);
_flags = new Flags(4);
__unnamed4 = new Pad(32);

}
public void Read(BinaryReader reader)
{
  _anchorOffset.Read(reader);
  _widthScale.Read(reader);
  _heightScale.Read(reader);
  _scalingFlags.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  _defaultColor.Read(reader);
  _flashingColor.Read(reader);
  _flashPeriod.Read(reader);
  _flashDelay.Read(reader);
  _numberOfFlashes.Read(reader);
  _flashFlags.Read(reader);
  _flashLength.Read(reader);
  _disabledColor.Read(reader);
  __unnamed3.Read(reader);
  _frameRate.Read(reader);
  _sequenceIndex.Read(reader);
  _flags.Read(reader);
  __unnamed4.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _anchorOffset.Write(writer);
    _widthScale.Write(writer);
    _heightScale.Write(writer);
    _scalingFlags.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    _defaultColor.Write(writer);
    _flashingColor.Write(writer);
    _flashPeriod.Write(writer);
    _flashDelay.Write(writer);
    _numberOfFlashes.Write(writer);
    _flashFlags.Write(writer);
    _flashLength.Write(writer);
    _disabledColor.Write(writer);
    __unnamed3.Write(writer);
    _frameRate.Write(writer);
    _sequenceIndex.Write(writer);
    _flags.Write(writer);
    __unnamed4.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class WeaponHudOverlaysBlock : IBlock
{
private Enum _stateAttachedTo = new Enum();
private Pad  __unnamed;	
private Enum _canUseOnMapType = new Enum();
private Pad  __unnamed2;	
private Pad  __unnamed3;	
private TagReference _overlayBitmap = new TagReference();
private Block _overlays = new Block();
private Pad  __unnamed4;	
public class WeaponHudOverlayBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public WeaponHudOverlayBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(WeaponHudOverlayBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new WeaponHudOverlayBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public WeaponHudOverlayBlock this[int index]
  {
    get { return (InnerList[index] as WeaponHudOverlayBlock); }
  }
}
private WeaponHudOverlayBlockCollection _overlaysCollection;
public WeaponHudOverlayBlockCollection Overlays
{
  get { return _overlaysCollection; }
}
public Enum StateAttachedTo
{
  get { return _stateAttachedTo; }
  set { _stateAttachedTo = value; }
}
public Enum CanUseOnMapType
{
  get { return _canUseOnMapType; }
  set { _canUseOnMapType = value; }
}
public TagReference OverlayBitmap
{
  get { return _overlayBitmap; }
  set { _overlayBitmap = value; }
}
public WeaponHudOverlaysBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(2);
__unnamed3 = new Pad(28);
__unnamed4 = new Pad(40);
_overlaysCollection = new WeaponHudOverlayBlockCollection(_overlays);

}
public void Read(BinaryReader reader)
{
  _stateAttachedTo.Read(reader);
  __unnamed.Read(reader);
  _canUseOnMapType.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  _overlayBitmap.Read(reader);
  _overlays.Read(reader);
  __unnamed4.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_overlayBitmap.ReadString(reader);
for (int x=0; x<_overlays.Count; x++)
{
  Overlays.AddNew();
  Overlays[x].Read(reader);
}
for (int x=0; x<_overlays.Count; x++)
  Overlays[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _stateAttachedTo.Write(writer);
    __unnamed.Write(writer);
    _canUseOnMapType.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    _overlayBitmap.Write(writer);
    _overlays.Write(writer);
    __unnamed4.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_overlayBitmap.WriteString(writer);
_overlays.UpdateReflexiveOffset(writer);
for (int x=0; x<_overlays.Count; x++)
{
  Overlays[x].Write(writer);
}
for (int x=0; x<_overlays.Count; x++)
  Overlays[x].WriteChildData(writer);
}
}
public class WeaponHudOverlayBlock : IBlock
{
private Point2D _anchorOffset = new Point2D();
private Real _widthScale = new Real();
private Real _heightScale = new Real();
private Flags  _scalingFlags;	
private Pad  __unnamed;	
private Pad  __unnamed2;	
private ARGBColor _defaultColor = new ARGBColor();
private ARGBColor _flashingColor = new ARGBColor();
private Real _flashPeriod = new Real();
private Real _flashDelay = new Real();
private ShortInteger _numberOfFlashes = new ShortInteger();
private Flags  _flashFlags;	
private Real _flashLength = new Real();
private ARGBColor _disabledColor = new ARGBColor();
private Pad  __unnamed3;	
private ShortInteger _frameRate = new ShortInteger();
private Pad  __unnamed4;	
private ShortInteger _sequenceIndex = new ShortInteger();
private Flags  _type;	
private Flags  _flags;	
private Pad  __unnamed5;	
private Pad  __unnamed6;	
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
public ShortInteger FrameRate
{
  get { return _frameRate; }
  set { _frameRate = value; }
}
public ShortInteger SequenceIndex
{
  get { return _sequenceIndex; }
  set { _sequenceIndex = value; }
}
public Flags Type
{
  get { return _type; }
  set { _type = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public WeaponHudOverlayBlock()
{
_scalingFlags = new Flags(2);
__unnamed = new Pad(2);
__unnamed2 = new Pad(20);
_flashFlags = new Flags(2);
__unnamed3 = new Pad(4);
__unnamed4 = new Pad(2);
_type = new Flags(2);
_flags = new Flags(4);
__unnamed5 = new Pad(16);
__unnamed6 = new Pad(40);

}
public void Read(BinaryReader reader)
{
  _anchorOffset.Read(reader);
  _widthScale.Read(reader);
  _heightScale.Read(reader);
  _scalingFlags.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  _defaultColor.Read(reader);
  _flashingColor.Read(reader);
  _flashPeriod.Read(reader);
  _flashDelay.Read(reader);
  _numberOfFlashes.Read(reader);
  _flashFlags.Read(reader);
  _flashLength.Read(reader);
  _disabledColor.Read(reader);
  __unnamed3.Read(reader);
  _frameRate.Read(reader);
  __unnamed4.Read(reader);
  _sequenceIndex.Read(reader);
  _type.Read(reader);
  _flags.Read(reader);
  __unnamed5.Read(reader);
  __unnamed6.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _anchorOffset.Write(writer);
    _widthScale.Write(writer);
    _heightScale.Write(writer);
    _scalingFlags.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    _defaultColor.Write(writer);
    _flashingColor.Write(writer);
    _flashPeriod.Write(writer);
    _flashDelay.Write(writer);
    _numberOfFlashes.Write(writer);
    _flashFlags.Write(writer);
    _flashLength.Write(writer);
    _disabledColor.Write(writer);
    __unnamed3.Write(writer);
    _frameRate.Write(writer);
    __unnamed4.Write(writer);
    _sequenceIndex.Write(writer);
    _type.Write(writer);
    _flags.Write(writer);
    __unnamed5.Write(writer);
    __unnamed6.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class GlobalHudScreenEffectDefinitionBlock : IBlock
{
private Pad  __unnamed;	
private Flags  _flags;	
private Pad  __unnamed2;	
private Pad  __unnamed3;	
private TagReference _maskFullscreen = new TagReference();
private TagReference _maskSplitscreen = new TagReference();
private Pad  __unnamed4;	
private Flags  _flags2;	
private Pad  __unnamed5;	
private AngleBounds _fOVInBounds = new AngleBounds();
private RealBounds _radiusOutBounds = new RealBounds();
private Pad  __unnamed6;	
private Flags  _flags3;	
private ShortInteger _scriptSource = new ShortInteger();
private RealFraction _intensity = new RealFraction();
private Pad  __unnamed7;	
private Flags  _flags4;	
private ShortInteger _scriptSource2 = new ShortInteger();
private RealFraction _intensity2 = new RealFraction();
private RealRGBColor _tint = new RealRGBColor();
private Pad  __unnamed8;	
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public TagReference MaskFullscreen
{
  get { return _maskFullscreen; }
  set { _maskFullscreen = value; }
}
public TagReference MaskSplitscreen
{
  get { return _maskSplitscreen; }
  set { _maskSplitscreen = value; }
}
public Flags Flags2
{
  get { return _flags2; }
  set { _flags2 = value; }
}
public AngleBounds FOVInBounds
{
  get { return _fOVInBounds; }
  set { _fOVInBounds = value; }
}
public RealBounds RadiusOutBounds
{
  get { return _radiusOutBounds; }
  set { _radiusOutBounds = value; }
}
public Flags Flags3
{
  get { return _flags3; }
  set { _flags3 = value; }
}
public ShortInteger ScriptSource
{
  get { return _scriptSource; }
  set { _scriptSource = value; }
}
public RealFraction Intensity
{
  get { return _intensity; }
  set { _intensity = value; }
}
public Flags Flags4
{
  get { return _flags4; }
  set { _flags4 = value; }
}
public ShortInteger ScriptSource2
{
  get { return _scriptSource2; }
  set { _scriptSource2 = value; }
}
public RealFraction Intensity2
{
  get { return _intensity2; }
  set { _intensity2 = value; }
}
public RealRGBColor Tint
{
  get { return _tint; }
  set { _tint = value; }
}
public GlobalHudScreenEffectDefinitionBlock()
{
__unnamed = new Pad(4);
_flags = new Flags(2);
__unnamed2 = new Pad(2);
__unnamed3 = new Pad(16);
__unnamed4 = new Pad(8);
_flags2 = new Flags(2);
__unnamed5 = new Pad(2);
__unnamed6 = new Pad(24);
_flags3 = new Flags(2);
__unnamed7 = new Pad(24);
_flags4 = new Flags(2);
__unnamed8 = new Pad(24);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  _flags.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  _maskFullscreen.Read(reader);
  _maskSplitscreen.Read(reader);
  __unnamed4.Read(reader);
  _flags2.Read(reader);
  __unnamed5.Read(reader);
  _fOVInBounds.Read(reader);
  _radiusOutBounds.Read(reader);
  __unnamed6.Read(reader);
  _flags3.Read(reader);
  _scriptSource.Read(reader);
  _intensity.Read(reader);
  __unnamed7.Read(reader);
  _flags4.Read(reader);
  _scriptSource2.Read(reader);
  _intensity2.Read(reader);
  _tint.Read(reader);
  __unnamed8.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_maskFullscreen.ReadString(reader);
_maskSplitscreen.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    _flags.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    _maskFullscreen.Write(writer);
    _maskSplitscreen.Write(writer);
    __unnamed4.Write(writer);
    _flags2.Write(writer);
    __unnamed5.Write(writer);
    _fOVInBounds.Write(writer);
    _radiusOutBounds.Write(writer);
    __unnamed6.Write(writer);
    _flags3.Write(writer);
    _scriptSource.Write(writer);
    _intensity.Write(writer);
    __unnamed7.Write(writer);
    _flags4.Write(writer);
    _scriptSource2.Write(writer);
    _intensity2.Write(writer);
    _tint.Write(writer);
    __unnamed8.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_maskFullscreen.WriteString(writer);
_maskSplitscreen.WriteString(writer);
}
}
  }
}
