using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class ModelAnimations : IBlock
  {
    public ModelAnimationsBlock ModelAnimationsValues = new ModelAnimationsBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'ModelAnimations'------------------------------------------------------");
      ModelAnimationsValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      ModelAnimationsValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      ModelAnimationsValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      ModelAnimationsValues.WriteChildData(writer);
    }
public class ModelAnimationsBlock : IBlock
{
private Block _oBJECTS = new Block();
private Block _uNITS = new Block();
private Block _wEAPONS = new Block();
private Block _vEHICLES = new Block();
private Block _dEVICES = new Block();
private Block _uNITDAMAGE = new Block();
private Block _fIRSTPERSONWEAPONS = new Block();
private Block _soundReferences = new Block();
private Real _limpBodyNodeRadius = new Real();
private Flags  _flags;	
private Pad  __unnamed;	
private Block _nodes = new Block();
private Block _animations = new Block();
public class AnimationGraphObjectOverlayBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public AnimationGraphObjectOverlayBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(AnimationGraphObjectOverlayBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new AnimationGraphObjectOverlayBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public AnimationGraphObjectOverlayBlock this[int index]
  {
    get { return (InnerList[index] as AnimationGraphObjectOverlayBlock); }
  }
}
private AnimationGraphObjectOverlayBlockCollection _oBJECTSCollection;
public AnimationGraphObjectOverlayBlockCollection OBJECTS
{
  get { return _oBJECTSCollection; }
}
public class AnimationGraphUnitSeatBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public AnimationGraphUnitSeatBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(AnimationGraphUnitSeatBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new AnimationGraphUnitSeatBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public AnimationGraphUnitSeatBlock this[int index]
  {
    get { return (InnerList[index] as AnimationGraphUnitSeatBlock); }
  }
}
private AnimationGraphUnitSeatBlockCollection _uNITSCollection;
public AnimationGraphUnitSeatBlockCollection UNITS
{
  get { return _uNITSCollection; }
}
public class AnimationGraphWeaponAnimationsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public AnimationGraphWeaponAnimationsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(AnimationGraphWeaponAnimationsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new AnimationGraphWeaponAnimationsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public AnimationGraphWeaponAnimationsBlock this[int index]
  {
    get { return (InnerList[index] as AnimationGraphWeaponAnimationsBlock); }
  }
}
private AnimationGraphWeaponAnimationsBlockCollection _wEAPONSCollection;
public AnimationGraphWeaponAnimationsBlockCollection WEAPONS
{
  get { return _wEAPONSCollection; }
}
public class AnimationGraphVehicleAnimationsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public AnimationGraphVehicleAnimationsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(AnimationGraphVehicleAnimationsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new AnimationGraphVehicleAnimationsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public AnimationGraphVehicleAnimationsBlock this[int index]
  {
    get { return (InnerList[index] as AnimationGraphVehicleAnimationsBlock); }
  }
}
private AnimationGraphVehicleAnimationsBlockCollection _vEHICLESCollection;
public AnimationGraphVehicleAnimationsBlockCollection VEHICLES
{
  get { return _vEHICLESCollection; }
}
public class DeviceAnimationsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public DeviceAnimationsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(DeviceAnimationsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new DeviceAnimationsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public DeviceAnimationsBlock this[int index]
  {
    get { return (InnerList[index] as DeviceAnimationsBlock); }
  }
}
private DeviceAnimationsBlockCollection _dEVICESCollection;
public DeviceAnimationsBlockCollection DEVICES
{
  get { return _dEVICESCollection; }
}
public class UnitDamageAnimationsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public UnitDamageAnimationsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(UnitDamageAnimationsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new UnitDamageAnimationsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public UnitDamageAnimationsBlock this[int index]
  {
    get { return (InnerList[index] as UnitDamageAnimationsBlock); }
  }
}
private UnitDamageAnimationsBlockCollection _uNITDAMAGECollection;
public UnitDamageAnimationsBlockCollection UNITDAMAGE
{
  get { return _uNITDAMAGECollection; }
}
public class AnimationGraphFirstPersonWeaponAnimationsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public AnimationGraphFirstPersonWeaponAnimationsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(AnimationGraphFirstPersonWeaponAnimationsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new AnimationGraphFirstPersonWeaponAnimationsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public AnimationGraphFirstPersonWeaponAnimationsBlock this[int index]
  {
    get { return (InnerList[index] as AnimationGraphFirstPersonWeaponAnimationsBlock); }
  }
}
private AnimationGraphFirstPersonWeaponAnimationsBlockCollection _fIRSTPERSONWEAPONSCollection;
public AnimationGraphFirstPersonWeaponAnimationsBlockCollection FIRSTPERSONWEAPONS
{
  get { return _fIRSTPERSONWEAPONSCollection; }
}
public class AnimationGraphSoundReferenceBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public AnimationGraphSoundReferenceBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(AnimationGraphSoundReferenceBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new AnimationGraphSoundReferenceBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public AnimationGraphSoundReferenceBlock this[int index]
  {
    get { return (InnerList[index] as AnimationGraphSoundReferenceBlock); }
  }
}
private AnimationGraphSoundReferenceBlockCollection _soundReferencesCollection;
public AnimationGraphSoundReferenceBlockCollection SoundReferences
{
  get { return _soundReferencesCollection; }
}
public class AnimationGraphNodeBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public AnimationGraphNodeBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(AnimationGraphNodeBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new AnimationGraphNodeBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public AnimationGraphNodeBlock this[int index]
  {
    get { return (InnerList[index] as AnimationGraphNodeBlock); }
  }
}
private AnimationGraphNodeBlockCollection _nodesCollection;
public AnimationGraphNodeBlockCollection Nodes
{
  get { return _nodesCollection; }
}
public class AnimationBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public AnimationBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(AnimationBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new AnimationBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public AnimationBlock this[int index]
  {
    get { return (InnerList[index] as AnimationBlock); }
  }
}
private AnimationBlockCollection _animationsCollection;
public AnimationBlockCollection Animations
{
  get { return _animationsCollection; }
}
public Real LimpBodyNodeRadius
{
  get { return _limpBodyNodeRadius; }
  set { _limpBodyNodeRadius = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public ModelAnimationsBlock()
{
_flags = new Flags(2);
__unnamed = new Pad(2);
_oBJECTSCollection = new AnimationGraphObjectOverlayBlockCollection(_oBJECTS);
_uNITSCollection = new AnimationGraphUnitSeatBlockCollection(_uNITS);
_wEAPONSCollection = new AnimationGraphWeaponAnimationsBlockCollection(_wEAPONS);
_vEHICLESCollection = new AnimationGraphVehicleAnimationsBlockCollection(_vEHICLES);
_dEVICESCollection = new DeviceAnimationsBlockCollection(_dEVICES);
_uNITDAMAGECollection = new UnitDamageAnimationsBlockCollection(_uNITDAMAGE);
_fIRSTPERSONWEAPONSCollection = new AnimationGraphFirstPersonWeaponAnimationsBlockCollection(_fIRSTPERSONWEAPONS);
_soundReferencesCollection = new AnimationGraphSoundReferenceBlockCollection(_soundReferences);
_nodesCollection = new AnimationGraphNodeBlockCollection(_nodes);
_animationsCollection = new AnimationBlockCollection(_animations);

}
public void Read(BinaryReader reader)
{
  _oBJECTS.Read(reader);
  _uNITS.Read(reader);
  _wEAPONS.Read(reader);
  _vEHICLES.Read(reader);
  _dEVICES.Read(reader);
  _uNITDAMAGE.Read(reader);
  _fIRSTPERSONWEAPONS.Read(reader);
  _soundReferences.Read(reader);
  _limpBodyNodeRadius.Read(reader);
  _flags.Read(reader);
  __unnamed.Read(reader);
  _nodes.Read(reader);
  _animations.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_oBJECTS.Count; x++)
{
  OBJECTS.AddNew();
  OBJECTS[x].Read(reader);
}
for (int x=0; x<_oBJECTS.Count; x++)
  OBJECTS[x].ReadChildData(reader);
for (int x=0; x<_uNITS.Count; x++)
{
  UNITS.AddNew();
  UNITS[x].Read(reader);
}
for (int x=0; x<_uNITS.Count; x++)
  UNITS[x].ReadChildData(reader);
for (int x=0; x<_wEAPONS.Count; x++)
{
  WEAPONS.AddNew();
  WEAPONS[x].Read(reader);
}
for (int x=0; x<_wEAPONS.Count; x++)
  WEAPONS[x].ReadChildData(reader);
for (int x=0; x<_vEHICLES.Count; x++)
{
  VEHICLES.AddNew();
  VEHICLES[x].Read(reader);
}
for (int x=0; x<_vEHICLES.Count; x++)
  VEHICLES[x].ReadChildData(reader);
for (int x=0; x<_dEVICES.Count; x++)
{
  DEVICES.AddNew();
  DEVICES[x].Read(reader);
}
for (int x=0; x<_dEVICES.Count; x++)
  DEVICES[x].ReadChildData(reader);
for (int x=0; x<_uNITDAMAGE.Count; x++)
{
  UNITDAMAGE.AddNew();
  UNITDAMAGE[x].Read(reader);
}
for (int x=0; x<_uNITDAMAGE.Count; x++)
  UNITDAMAGE[x].ReadChildData(reader);
for (int x=0; x<_fIRSTPERSONWEAPONS.Count; x++)
{
  FIRSTPERSONWEAPONS.AddNew();
  FIRSTPERSONWEAPONS[x].Read(reader);
}
for (int x=0; x<_fIRSTPERSONWEAPONS.Count; x++)
  FIRSTPERSONWEAPONS[x].ReadChildData(reader);
for (int x=0; x<_soundReferences.Count; x++)
{
  SoundReferences.AddNew();
  SoundReferences[x].Read(reader);
}
for (int x=0; x<_soundReferences.Count; x++)
  SoundReferences[x].ReadChildData(reader);
for (int x=0; x<_nodes.Count; x++)
{
  Nodes.AddNew();
  Nodes[x].Read(reader);
}
for (int x=0; x<_nodes.Count; x++)
  Nodes[x].ReadChildData(reader);
for (int x=0; x<_animations.Count; x++)
{
  Animations.AddNew();
  Animations[x].Read(reader);
}
for (int x=0; x<_animations.Count; x++)
  Animations[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _oBJECTS.Write(writer);
    _uNITS.Write(writer);
    _wEAPONS.Write(writer);
    _vEHICLES.Write(writer);
    _dEVICES.Write(writer);
    _uNITDAMAGE.Write(writer);
    _fIRSTPERSONWEAPONS.Write(writer);
    _soundReferences.Write(writer);
    _limpBodyNodeRadius.Write(writer);
    _flags.Write(writer);
    __unnamed.Write(writer);
    _nodes.Write(writer);
    _animations.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_oBJECTS.UpdateReflexiveOffset(writer);
for (int x=0; x<_oBJECTS.Count; x++)
{
  OBJECTS[x].Write(writer);
}
for (int x=0; x<_oBJECTS.Count; x++)
  OBJECTS[x].WriteChildData(writer);
_uNITS.UpdateReflexiveOffset(writer);
for (int x=0; x<_uNITS.Count; x++)
{
  UNITS[x].Write(writer);
}
for (int x=0; x<_uNITS.Count; x++)
  UNITS[x].WriteChildData(writer);
_wEAPONS.UpdateReflexiveOffset(writer);
for (int x=0; x<_wEAPONS.Count; x++)
{
  WEAPONS[x].Write(writer);
}
for (int x=0; x<_wEAPONS.Count; x++)
  WEAPONS[x].WriteChildData(writer);
_vEHICLES.UpdateReflexiveOffset(writer);
for (int x=0; x<_vEHICLES.Count; x++)
{
  VEHICLES[x].Write(writer);
}
for (int x=0; x<_vEHICLES.Count; x++)
  VEHICLES[x].WriteChildData(writer);
_dEVICES.UpdateReflexiveOffset(writer);
for (int x=0; x<_dEVICES.Count; x++)
{
  DEVICES[x].Write(writer);
}
for (int x=0; x<_dEVICES.Count; x++)
  DEVICES[x].WriteChildData(writer);
_uNITDAMAGE.UpdateReflexiveOffset(writer);
for (int x=0; x<_uNITDAMAGE.Count; x++)
{
  UNITDAMAGE[x].Write(writer);
}
for (int x=0; x<_uNITDAMAGE.Count; x++)
  UNITDAMAGE[x].WriteChildData(writer);
_fIRSTPERSONWEAPONS.UpdateReflexiveOffset(writer);
for (int x=0; x<_fIRSTPERSONWEAPONS.Count; x++)
{
  FIRSTPERSONWEAPONS[x].Write(writer);
}
for (int x=0; x<_fIRSTPERSONWEAPONS.Count; x++)
  FIRSTPERSONWEAPONS[x].WriteChildData(writer);
_soundReferences.UpdateReflexiveOffset(writer);
for (int x=0; x<_soundReferences.Count; x++)
{
  SoundReferences[x].Write(writer);
}
for (int x=0; x<_soundReferences.Count; x++)
  SoundReferences[x].WriteChildData(writer);
_nodes.UpdateReflexiveOffset(writer);
for (int x=0; x<_nodes.Count; x++)
{
  Nodes[x].Write(writer);
}
for (int x=0; x<_nodes.Count; x++)
  Nodes[x].WriteChildData(writer);
_animations.UpdateReflexiveOffset(writer);
for (int x=0; x<_animations.Count; x++)
{
  Animations[x].Write(writer);
}
for (int x=0; x<_animations.Count; x++)
  Animations[x].WriteChildData(writer);
}
}
public class AnimationGraphObjectOverlayBlock : IBlock
{
private ShortBlockIndex _animation = new ShortBlockIndex();
private Enum _function = new Enum();
private Enum _functionControls = new Enum();
private Pad  __unnamed;	
private Pad  __unnamed2;	
public ShortBlockIndex Animation
{
  get { return _animation; }
  set { _animation = value; }
}
public Enum Function
{
  get { return _function; }
  set { _function = value; }
}
public Enum FunctionControls
{
  get { return _functionControls; }
  set { _functionControls = value; }
}
public AnimationGraphObjectOverlayBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(12);

}
public void Read(BinaryReader reader)
{
  _animation.Read(reader);
  _function.Read(reader);
  _functionControls.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _animation.Write(writer);
    _function.Write(writer);
    _functionControls.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class AnimationGraphUnitSeatBlock : IBlock
{
private FixedLengthString _label = new FixedLengthString();
private Angle _rightYawPerFrame = new Angle();
private Angle _leftYawPerFrame = new Angle();
private ShortInteger _rightFrameCount = new ShortInteger();
private ShortInteger _leftFrameCount = new ShortInteger();
private Angle _downPitchPerFrame = new Angle();
private Angle _upPitchPerFrame = new Angle();
private ShortInteger _downPitchFrameCount = new ShortInteger();
private ShortInteger _upPitchFrameCount = new ShortInteger();
private Pad  __unnamed;	
private Block _animations = new Block();
private Block _ikPoints = new Block();
private Block _weapons = new Block();
public class UnitSeatAnimationBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public UnitSeatAnimationBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(UnitSeatAnimationBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new UnitSeatAnimationBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public UnitSeatAnimationBlock this[int index]
  {
    get { return (InnerList[index] as UnitSeatAnimationBlock); }
  }
}
private UnitSeatAnimationBlockCollection _animationsCollection;
public UnitSeatAnimationBlockCollection Animations
{
  get { return _animationsCollection; }
}
public class AnimationGraphUnitSeatIkPointBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public AnimationGraphUnitSeatIkPointBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(AnimationGraphUnitSeatIkPointBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new AnimationGraphUnitSeatIkPointBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public AnimationGraphUnitSeatIkPointBlock this[int index]
  {
    get { return (InnerList[index] as AnimationGraphUnitSeatIkPointBlock); }
  }
}
private AnimationGraphUnitSeatIkPointBlockCollection _ikPointsCollection;
public AnimationGraphUnitSeatIkPointBlockCollection IkPoints
{
  get { return _ikPointsCollection; }
}
public class AnimationGraphWeaponBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public AnimationGraphWeaponBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(AnimationGraphWeaponBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new AnimationGraphWeaponBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public AnimationGraphWeaponBlock this[int index]
  {
    get { return (InnerList[index] as AnimationGraphWeaponBlock); }
  }
}
private AnimationGraphWeaponBlockCollection _weaponsCollection;
public AnimationGraphWeaponBlockCollection Weapons
{
  get { return _weaponsCollection; }
}
public FixedLengthString Label
{
  get { return _label; }
  set { _label = value; }
}
public Angle RightYawPerFrame
{
  get { return _rightYawPerFrame; }
  set { _rightYawPerFrame = value; }
}
public Angle LeftYawPerFrame
{
  get { return _leftYawPerFrame; }
  set { _leftYawPerFrame = value; }
}
public ShortInteger RightFrameCount
{
  get { return _rightFrameCount; }
  set { _rightFrameCount = value; }
}
public ShortInteger LeftFrameCount
{
  get { return _leftFrameCount; }
  set { _leftFrameCount = value; }
}
public Angle DownPitchPerFrame
{
  get { return _downPitchPerFrame; }
  set { _downPitchPerFrame = value; }
}
public Angle UpPitchPerFrame
{
  get { return _upPitchPerFrame; }
  set { _upPitchPerFrame = value; }
}
public ShortInteger DownPitchFrameCount
{
  get { return _downPitchFrameCount; }
  set { _downPitchFrameCount = value; }
}
public ShortInteger UpPitchFrameCount
{
  get { return _upPitchFrameCount; }
  set { _upPitchFrameCount = value; }
}
public AnimationGraphUnitSeatBlock()
{
__unnamed = new Pad(8);
_animationsCollection = new UnitSeatAnimationBlockCollection(_animations);
_ikPointsCollection = new AnimationGraphUnitSeatIkPointBlockCollection(_ikPoints);
_weaponsCollection = new AnimationGraphWeaponBlockCollection(_weapons);

}
public void Read(BinaryReader reader)
{
  _label.Read(reader);
  _rightYawPerFrame.Read(reader);
  _leftYawPerFrame.Read(reader);
  _rightFrameCount.Read(reader);
  _leftFrameCount.Read(reader);
  _downPitchPerFrame.Read(reader);
  _upPitchPerFrame.Read(reader);
  _downPitchFrameCount.Read(reader);
  _upPitchFrameCount.Read(reader);
  __unnamed.Read(reader);
  _animations.Read(reader);
  _ikPoints.Read(reader);
  _weapons.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_animations.Count; x++)
{
  Animations.AddNew();
  Animations[x].Read(reader);
}
for (int x=0; x<_animations.Count; x++)
  Animations[x].ReadChildData(reader);
for (int x=0; x<_ikPoints.Count; x++)
{
  IkPoints.AddNew();
  IkPoints[x].Read(reader);
}
for (int x=0; x<_ikPoints.Count; x++)
  IkPoints[x].ReadChildData(reader);
for (int x=0; x<_weapons.Count; x++)
{
  Weapons.AddNew();
  Weapons[x].Read(reader);
}
for (int x=0; x<_weapons.Count; x++)
  Weapons[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _label.Write(writer);
    _rightYawPerFrame.Write(writer);
    _leftYawPerFrame.Write(writer);
    _rightFrameCount.Write(writer);
    _leftFrameCount.Write(writer);
    _downPitchPerFrame.Write(writer);
    _upPitchPerFrame.Write(writer);
    _downPitchFrameCount.Write(writer);
    _upPitchFrameCount.Write(writer);
    __unnamed.Write(writer);
    _animations.Write(writer);
    _ikPoints.Write(writer);
    _weapons.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_animations.UpdateReflexiveOffset(writer);
for (int x=0; x<_animations.Count; x++)
{
  Animations[x].Write(writer);
}
for (int x=0; x<_animations.Count; x++)
  Animations[x].WriteChildData(writer);
_ikPoints.UpdateReflexiveOffset(writer);
for (int x=0; x<_ikPoints.Count; x++)
{
  IkPoints[x].Write(writer);
}
for (int x=0; x<_ikPoints.Count; x++)
  IkPoints[x].WriteChildData(writer);
_weapons.UpdateReflexiveOffset(writer);
for (int x=0; x<_weapons.Count; x++)
{
  Weapons[x].Write(writer);
}
for (int x=0; x<_weapons.Count; x++)
  Weapons[x].WriteChildData(writer);
}
}
public class UnitSeatAnimationBlock : IBlock
{
private ShortBlockIndex _animation = new ShortBlockIndex();
public ShortBlockIndex Animation
{
  get { return _animation; }
  set { _animation = value; }
}
public UnitSeatAnimationBlock()
{

}
public void Read(BinaryReader reader)
{
  _animation.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _animation.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class AnimationGraphUnitSeatIkPointBlock : IBlock
{
private FixedLengthString _marker = new FixedLengthString();
private FixedLengthString _attachToMarker = new FixedLengthString();
public FixedLengthString Marker
{
  get { return _marker; }
  set { _marker = value; }
}
public FixedLengthString AttachToMarker
{
  get { return _attachToMarker; }
  set { _attachToMarker = value; }
}
public AnimationGraphUnitSeatIkPointBlock()
{

}
public void Read(BinaryReader reader)
{
  _marker.Read(reader);
  _attachToMarker.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _marker.Write(writer);
    _attachToMarker.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class AnimationGraphWeaponBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private FixedLengthString _gripMarker = new FixedLengthString();
private FixedLengthString _handMarker = new FixedLengthString();
private Angle _rightYawPerFrame = new Angle();
private Angle _leftYawPerFrame = new Angle();
private ShortInteger _rightFrameCount = new ShortInteger();
private ShortInteger _leftFrameCount = new ShortInteger();
private Angle _downPitchPerFrame = new Angle();
private Angle _upPitchPerFrame = new Angle();
private ShortInteger _downPitchFrameCount = new ShortInteger();
private ShortInteger _upPitchFrameCount = new ShortInteger();
private Pad  __unnamed;	
private Block _animations = new Block();
private Block _ikPoints = new Block();
private Block _weaponTypes = new Block();
public class WeaponClassAnimationBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public WeaponClassAnimationBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(WeaponClassAnimationBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new WeaponClassAnimationBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public WeaponClassAnimationBlock this[int index]
  {
    get { return (InnerList[index] as WeaponClassAnimationBlock); }
  }
}
private WeaponClassAnimationBlockCollection _animationsCollection;
public WeaponClassAnimationBlockCollection Animations
{
  get { return _animationsCollection; }
}
public class AnimationGraphUnitSeatIkPointBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public AnimationGraphUnitSeatIkPointBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(AnimationGraphUnitSeatIkPointBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new AnimationGraphUnitSeatIkPointBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public AnimationGraphUnitSeatIkPointBlock this[int index]
  {
    get { return (InnerList[index] as AnimationGraphUnitSeatIkPointBlock); }
  }
}
private AnimationGraphUnitSeatIkPointBlockCollection _ikPointsCollection;
public AnimationGraphUnitSeatIkPointBlockCollection IkPoints
{
  get { return _ikPointsCollection; }
}
public class AnimationGraphWeaponTypeBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public AnimationGraphWeaponTypeBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(AnimationGraphWeaponTypeBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new AnimationGraphWeaponTypeBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public AnimationGraphWeaponTypeBlock this[int index]
  {
    get { return (InnerList[index] as AnimationGraphWeaponTypeBlock); }
  }
}
private AnimationGraphWeaponTypeBlockCollection _weaponTypesCollection;
public AnimationGraphWeaponTypeBlockCollection WeaponTypes
{
  get { return _weaponTypesCollection; }
}
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public FixedLengthString GripMarker
{
  get { return _gripMarker; }
  set { _gripMarker = value; }
}
public FixedLengthString HandMarker
{
  get { return _handMarker; }
  set { _handMarker = value; }
}
public Angle RightYawPerFrame
{
  get { return _rightYawPerFrame; }
  set { _rightYawPerFrame = value; }
}
public Angle LeftYawPerFrame
{
  get { return _leftYawPerFrame; }
  set { _leftYawPerFrame = value; }
}
public ShortInteger RightFrameCount
{
  get { return _rightFrameCount; }
  set { _rightFrameCount = value; }
}
public ShortInteger LeftFrameCount
{
  get { return _leftFrameCount; }
  set { _leftFrameCount = value; }
}
public Angle DownPitchPerFrame
{
  get { return _downPitchPerFrame; }
  set { _downPitchPerFrame = value; }
}
public Angle UpPitchPerFrame
{
  get { return _upPitchPerFrame; }
  set { _upPitchPerFrame = value; }
}
public ShortInteger DownPitchFrameCount
{
  get { return _downPitchFrameCount; }
  set { _downPitchFrameCount = value; }
}
public ShortInteger UpPitchFrameCount
{
  get { return _upPitchFrameCount; }
  set { _upPitchFrameCount = value; }
}
public AnimationGraphWeaponBlock()
{
__unnamed = new Pad(32);
_animationsCollection = new WeaponClassAnimationBlockCollection(_animations);
_ikPointsCollection = new AnimationGraphUnitSeatIkPointBlockCollection(_ikPoints);
_weaponTypesCollection = new AnimationGraphWeaponTypeBlockCollection(_weaponTypes);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _gripMarker.Read(reader);
  _handMarker.Read(reader);
  _rightYawPerFrame.Read(reader);
  _leftYawPerFrame.Read(reader);
  _rightFrameCount.Read(reader);
  _leftFrameCount.Read(reader);
  _downPitchPerFrame.Read(reader);
  _upPitchPerFrame.Read(reader);
  _downPitchFrameCount.Read(reader);
  _upPitchFrameCount.Read(reader);
  __unnamed.Read(reader);
  _animations.Read(reader);
  _ikPoints.Read(reader);
  _weaponTypes.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_animations.Count; x++)
{
  Animations.AddNew();
  Animations[x].Read(reader);
}
for (int x=0; x<_animations.Count; x++)
  Animations[x].ReadChildData(reader);
for (int x=0; x<_ikPoints.Count; x++)
{
  IkPoints.AddNew();
  IkPoints[x].Read(reader);
}
for (int x=0; x<_ikPoints.Count; x++)
  IkPoints[x].ReadChildData(reader);
for (int x=0; x<_weaponTypes.Count; x++)
{
  WeaponTypes.AddNew();
  WeaponTypes[x].Read(reader);
}
for (int x=0; x<_weaponTypes.Count; x++)
  WeaponTypes[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _gripMarker.Write(writer);
    _handMarker.Write(writer);
    _rightYawPerFrame.Write(writer);
    _leftYawPerFrame.Write(writer);
    _rightFrameCount.Write(writer);
    _leftFrameCount.Write(writer);
    _downPitchPerFrame.Write(writer);
    _upPitchPerFrame.Write(writer);
    _downPitchFrameCount.Write(writer);
    _upPitchFrameCount.Write(writer);
    __unnamed.Write(writer);
    _animations.Write(writer);
    _ikPoints.Write(writer);
    _weaponTypes.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_animations.UpdateReflexiveOffset(writer);
for (int x=0; x<_animations.Count; x++)
{
  Animations[x].Write(writer);
}
for (int x=0; x<_animations.Count; x++)
  Animations[x].WriteChildData(writer);
_ikPoints.UpdateReflexiveOffset(writer);
for (int x=0; x<_ikPoints.Count; x++)
{
  IkPoints[x].Write(writer);
}
for (int x=0; x<_ikPoints.Count; x++)
  IkPoints[x].WriteChildData(writer);
_weaponTypes.UpdateReflexiveOffset(writer);
for (int x=0; x<_weaponTypes.Count; x++)
{
  WeaponTypes[x].Write(writer);
}
for (int x=0; x<_weaponTypes.Count; x++)
  WeaponTypes[x].WriteChildData(writer);
}
}
public class WeaponClassAnimationBlock : IBlock
{
private ShortBlockIndex _animation = new ShortBlockIndex();
public ShortBlockIndex Animation
{
  get { return _animation; }
  set { _animation = value; }
}
public WeaponClassAnimationBlock()
{

}
public void Read(BinaryReader reader)
{
  _animation.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _animation.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class AnimationGraphWeaponTypeBlock : IBlock
{
private FixedLengthString _label = new FixedLengthString();
private Pad  __unnamed;	
private Block _animations = new Block();
public class WeaponTypeAnimationBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public WeaponTypeAnimationBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(WeaponTypeAnimationBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new WeaponTypeAnimationBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public WeaponTypeAnimationBlock this[int index]
  {
    get { return (InnerList[index] as WeaponTypeAnimationBlock); }
  }
}
private WeaponTypeAnimationBlockCollection _animationsCollection;
public WeaponTypeAnimationBlockCollection Animations
{
  get { return _animationsCollection; }
}
public FixedLengthString Label
{
  get { return _label; }
  set { _label = value; }
}
public AnimationGraphWeaponTypeBlock()
{
__unnamed = new Pad(16);
_animationsCollection = new WeaponTypeAnimationBlockCollection(_animations);

}
public void Read(BinaryReader reader)
{
  _label.Read(reader);
  __unnamed.Read(reader);
  _animations.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_animations.Count; x++)
{
  Animations.AddNew();
  Animations[x].Read(reader);
}
for (int x=0; x<_animations.Count; x++)
  Animations[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _label.Write(writer);
    __unnamed.Write(writer);
    _animations.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_animations.UpdateReflexiveOffset(writer);
for (int x=0; x<_animations.Count; x++)
{
  Animations[x].Write(writer);
}
for (int x=0; x<_animations.Count; x++)
  Animations[x].WriteChildData(writer);
}
}
public class WeaponTypeAnimationBlock : IBlock
{
private ShortBlockIndex _animation = new ShortBlockIndex();
public ShortBlockIndex Animation
{
  get { return _animation; }
  set { _animation = value; }
}
public WeaponTypeAnimationBlock()
{

}
public void Read(BinaryReader reader)
{
  _animation.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _animation.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class AnimationGraphWeaponAnimationsBlock : IBlock
{
private Pad  __unnamed;	
private Block _animations = new Block();
public class WeaponAnimationBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public WeaponAnimationBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(WeaponAnimationBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new WeaponAnimationBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public WeaponAnimationBlock this[int index]
  {
    get { return (InnerList[index] as WeaponAnimationBlock); }
  }
}
private WeaponAnimationBlockCollection _animationsCollection;
public WeaponAnimationBlockCollection Animations
{
  get { return _animationsCollection; }
}
public AnimationGraphWeaponAnimationsBlock()
{
__unnamed = new Pad(16);
_animationsCollection = new WeaponAnimationBlockCollection(_animations);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  _animations.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_animations.Count; x++)
{
  Animations.AddNew();
  Animations[x].Read(reader);
}
for (int x=0; x<_animations.Count; x++)
  Animations[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    _animations.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_animations.UpdateReflexiveOffset(writer);
for (int x=0; x<_animations.Count; x++)
{
  Animations[x].Write(writer);
}
for (int x=0; x<_animations.Count; x++)
  Animations[x].WriteChildData(writer);
}
}
public class WeaponAnimationBlock : IBlock
{
private ShortBlockIndex _animation = new ShortBlockIndex();
public ShortBlockIndex Animation
{
  get { return _animation; }
  set { _animation = value; }
}
public WeaponAnimationBlock()
{

}
public void Read(BinaryReader reader)
{
  _animation.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _animation.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class AnimationGraphVehicleAnimationsBlock : IBlock
{
private Angle _rightYawPerFrame = new Angle();
private Angle _leftYawPerFrame = new Angle();
private ShortInteger _rightFrameCount = new ShortInteger();
private ShortInteger _leftFrameCount = new ShortInteger();
private Angle _downPitchPerFrame = new Angle();
private Angle _upPitchPerFrame = new Angle();
private ShortInteger _downPitchFrameCount = new ShortInteger();
private ShortInteger _upPitchFrameCount = new ShortInteger();
private Pad  __unnamed;	
private Block _animations = new Block();
private Block _suspensionAnimations = new Block();
public class VehicleAnimationBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public VehicleAnimationBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(VehicleAnimationBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new VehicleAnimationBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public VehicleAnimationBlock this[int index]
  {
    get { return (InnerList[index] as VehicleAnimationBlock); }
  }
}
private VehicleAnimationBlockCollection _animationsCollection;
public VehicleAnimationBlockCollection Animations
{
  get { return _animationsCollection; }
}
public class SuspensionAnimationBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public SuspensionAnimationBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(SuspensionAnimationBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new SuspensionAnimationBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public SuspensionAnimationBlock this[int index]
  {
    get { return (InnerList[index] as SuspensionAnimationBlock); }
  }
}
private SuspensionAnimationBlockCollection _suspensionAnimationsCollection;
public SuspensionAnimationBlockCollection SuspensionAnimations
{
  get { return _suspensionAnimationsCollection; }
}
public Angle RightYawPerFrame
{
  get { return _rightYawPerFrame; }
  set { _rightYawPerFrame = value; }
}
public Angle LeftYawPerFrame
{
  get { return _leftYawPerFrame; }
  set { _leftYawPerFrame = value; }
}
public ShortInteger RightFrameCount
{
  get { return _rightFrameCount; }
  set { _rightFrameCount = value; }
}
public ShortInteger LeftFrameCount
{
  get { return _leftFrameCount; }
  set { _leftFrameCount = value; }
}
public Angle DownPitchPerFrame
{
  get { return _downPitchPerFrame; }
  set { _downPitchPerFrame = value; }
}
public Angle UpPitchPerFrame
{
  get { return _upPitchPerFrame; }
  set { _upPitchPerFrame = value; }
}
public ShortInteger DownPitchFrameCount
{
  get { return _downPitchFrameCount; }
  set { _downPitchFrameCount = value; }
}
public ShortInteger UpPitchFrameCount
{
  get { return _upPitchFrameCount; }
  set { _upPitchFrameCount = value; }
}
public AnimationGraphVehicleAnimationsBlock()
{
__unnamed = new Pad(68);
_animationsCollection = new VehicleAnimationBlockCollection(_animations);
_suspensionAnimationsCollection = new SuspensionAnimationBlockCollection(_suspensionAnimations);

}
public void Read(BinaryReader reader)
{
  _rightYawPerFrame.Read(reader);
  _leftYawPerFrame.Read(reader);
  _rightFrameCount.Read(reader);
  _leftFrameCount.Read(reader);
  _downPitchPerFrame.Read(reader);
  _upPitchPerFrame.Read(reader);
  _downPitchFrameCount.Read(reader);
  _upPitchFrameCount.Read(reader);
  __unnamed.Read(reader);
  _animations.Read(reader);
  _suspensionAnimations.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_animations.Count; x++)
{
  Animations.AddNew();
  Animations[x].Read(reader);
}
for (int x=0; x<_animations.Count; x++)
  Animations[x].ReadChildData(reader);
for (int x=0; x<_suspensionAnimations.Count; x++)
{
  SuspensionAnimations.AddNew();
  SuspensionAnimations[x].Read(reader);
}
for (int x=0; x<_suspensionAnimations.Count; x++)
  SuspensionAnimations[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _rightYawPerFrame.Write(writer);
    _leftYawPerFrame.Write(writer);
    _rightFrameCount.Write(writer);
    _leftFrameCount.Write(writer);
    _downPitchPerFrame.Write(writer);
    _upPitchPerFrame.Write(writer);
    _downPitchFrameCount.Write(writer);
    _upPitchFrameCount.Write(writer);
    __unnamed.Write(writer);
    _animations.Write(writer);
    _suspensionAnimations.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_animations.UpdateReflexiveOffset(writer);
for (int x=0; x<_animations.Count; x++)
{
  Animations[x].Write(writer);
}
for (int x=0; x<_animations.Count; x++)
  Animations[x].WriteChildData(writer);
_suspensionAnimations.UpdateReflexiveOffset(writer);
for (int x=0; x<_suspensionAnimations.Count; x++)
{
  SuspensionAnimations[x].Write(writer);
}
for (int x=0; x<_suspensionAnimations.Count; x++)
  SuspensionAnimations[x].WriteChildData(writer);
}
}
public class VehicleAnimationBlock : IBlock
{
private ShortBlockIndex _animation = new ShortBlockIndex();
public ShortBlockIndex Animation
{
  get { return _animation; }
  set { _animation = value; }
}
public VehicleAnimationBlock()
{

}
public void Read(BinaryReader reader)
{
  _animation.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _animation.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class SuspensionAnimationBlock : IBlock
{
private ShortInteger _massPointIndex = new ShortInteger();
private ShortBlockIndex _animation = new ShortBlockIndex();
private Real _fullExtensionGround_depth = new Real();
private Real _fullCompressionGround_depth = new Real();
private Pad  __unnamed;	
public ShortInteger MassPointIndex
{
  get { return _massPointIndex; }
  set { _massPointIndex = value; }
}
public ShortBlockIndex Animation
{
  get { return _animation; }
  set { _animation = value; }
}
public Real FullExtensionGround_depth
{
  get { return _fullExtensionGround_depth; }
  set { _fullExtensionGround_depth = value; }
}
public Real FullCompressionGround_depth
{
  get { return _fullCompressionGround_depth; }
  set { _fullCompressionGround_depth = value; }
}
public SuspensionAnimationBlock()
{
__unnamed = new Pad(8);

}
public void Read(BinaryReader reader)
{
  _massPointIndex.Read(reader);
  _animation.Read(reader);
  _fullExtensionGround_depth.Read(reader);
  _fullCompressionGround_depth.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _massPointIndex.Write(writer);
    _animation.Write(writer);
    _fullExtensionGround_depth.Write(writer);
    _fullCompressionGround_depth.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class DeviceAnimationsBlock : IBlock
{
private Pad  __unnamed;	
private Block _animations = new Block();
public class DeviceAnimationBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public DeviceAnimationBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(DeviceAnimationBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new DeviceAnimationBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public DeviceAnimationBlock this[int index]
  {
    get { return (InnerList[index] as DeviceAnimationBlock); }
  }
}
private DeviceAnimationBlockCollection _animationsCollection;
public DeviceAnimationBlockCollection Animations
{
  get { return _animationsCollection; }
}
public DeviceAnimationsBlock()
{
__unnamed = new Pad(84);
_animationsCollection = new DeviceAnimationBlockCollection(_animations);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  _animations.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_animations.Count; x++)
{
  Animations.AddNew();
  Animations[x].Read(reader);
}
for (int x=0; x<_animations.Count; x++)
  Animations[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    _animations.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_animations.UpdateReflexiveOffset(writer);
for (int x=0; x<_animations.Count; x++)
{
  Animations[x].Write(writer);
}
for (int x=0; x<_animations.Count; x++)
  Animations[x].WriteChildData(writer);
}
}
public class DeviceAnimationBlock : IBlock
{
private ShortBlockIndex _animation = new ShortBlockIndex();
public ShortBlockIndex Animation
{
  get { return _animation; }
  set { _animation = value; }
}
public DeviceAnimationBlock()
{

}
public void Read(BinaryReader reader)
{
  _animation.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _animation.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class UnitDamageAnimationsBlock : IBlock
{
private ShortBlockIndex _animation = new ShortBlockIndex();
public ShortBlockIndex Animation
{
  get { return _animation; }
  set { _animation = value; }
}
public UnitDamageAnimationsBlock()
{

}
public void Read(BinaryReader reader)
{
  _animation.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _animation.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class AnimationGraphFirstPersonWeaponAnimationsBlock : IBlock
{
private Pad  __unnamed;	
private Block _animations = new Block();
public class FirstPersonWeaponBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public FirstPersonWeaponBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(FirstPersonWeaponBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new FirstPersonWeaponBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public FirstPersonWeaponBlock this[int index]
  {
    get { return (InnerList[index] as FirstPersonWeaponBlock); }
  }
}
private FirstPersonWeaponBlockCollection _animationsCollection;
public FirstPersonWeaponBlockCollection Animations
{
  get { return _animationsCollection; }
}
public AnimationGraphFirstPersonWeaponAnimationsBlock()
{
__unnamed = new Pad(16);
_animationsCollection = new FirstPersonWeaponBlockCollection(_animations);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  _animations.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_animations.Count; x++)
{
  Animations.AddNew();
  Animations[x].Read(reader);
}
for (int x=0; x<_animations.Count; x++)
  Animations[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    _animations.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_animations.UpdateReflexiveOffset(writer);
for (int x=0; x<_animations.Count; x++)
{
  Animations[x].Write(writer);
}
for (int x=0; x<_animations.Count; x++)
  Animations[x].WriteChildData(writer);
}
}
public class FirstPersonWeaponBlock : IBlock
{
private ShortBlockIndex _animation = new ShortBlockIndex();
public ShortBlockIndex Animation
{
  get { return _animation; }
  set { _animation = value; }
}
public FirstPersonWeaponBlock()
{

}
public void Read(BinaryReader reader)
{
  _animation.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _animation.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class AnimationGraphSoundReferenceBlock : IBlock
{
private TagReference _sound = new TagReference();
private Pad  __unnamed;	
public TagReference Sound
{
  get { return _sound; }
  set { _sound = value; }
}
public AnimationGraphSoundReferenceBlock()
{
__unnamed = new Pad(4);

}
public void Read(BinaryReader reader)
{
  _sound.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_sound.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _sound.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_sound.WriteString(writer);
}
}
public class AnimationGraphNodeBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private ShortBlockIndex _nextSiblingNodeIndex = new ShortBlockIndex();
private ShortBlockIndex _firstChildNodeIndex = new ShortBlockIndex();
private ShortBlockIndex _parentNodeIndex = new ShortBlockIndex();
private Pad  __unnamed;	
private Flags  _nodeJointFlags;	
private RealVector3D _baseVector = new RealVector3D();
private Real _vectorRange = new Real();
private Pad  __unnamed2;	
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public ShortBlockIndex NextSiblingNodeIndex
{
  get { return _nextSiblingNodeIndex; }
  set { _nextSiblingNodeIndex = value; }
}
public ShortBlockIndex FirstChildNodeIndex
{
  get { return _firstChildNodeIndex; }
  set { _firstChildNodeIndex = value; }
}
public ShortBlockIndex ParentNodeIndex
{
  get { return _parentNodeIndex; }
  set { _parentNodeIndex = value; }
}
public Flags NodeJointFlags
{
  get { return _nodeJointFlags; }
  set { _nodeJointFlags = value; }
}
public RealVector3D BaseVector
{
  get { return _baseVector; }
  set { _baseVector = value; }
}
public Real VectorRange
{
  get { return _vectorRange; }
  set { _vectorRange = value; }
}
public AnimationGraphNodeBlock()
{
__unnamed = new Pad(2);
_nodeJointFlags = new Flags(4);
__unnamed2 = new Pad(4);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _nextSiblingNodeIndex.Read(reader);
  _firstChildNodeIndex.Read(reader);
  _parentNodeIndex.Read(reader);
  __unnamed.Read(reader);
  _nodeJointFlags.Read(reader);
  _baseVector.Read(reader);
  _vectorRange.Read(reader);
  __unnamed2.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _nextSiblingNodeIndex.Write(writer);
    _firstChildNodeIndex.Write(writer);
    _parentNodeIndex.Write(writer);
    __unnamed.Write(writer);
    _nodeJointFlags.Write(writer);
    _baseVector.Write(writer);
    _vectorRange.Write(writer);
    __unnamed2.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class AnimationBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private Enum _type = new Enum();
private ShortInteger _frameCount = new ShortInteger();
private ShortInteger _frameSize = new ShortInteger();
private Enum _frameInfoType = new Enum();
private LongInteger _nodeListChecksum = new LongInteger();
private ShortInteger _nodeCount = new ShortInteger();
private ShortInteger _loopFrameIndex = new ShortInteger();
private RealFraction _weight = new RealFraction();
private ShortInteger _keyFrameIndex = new ShortInteger();
private ShortInteger _secondKeyFrameIndex = new ShortInteger();
private ShortBlockIndex _nextAnimation = new ShortBlockIndex();
private Flags  _flags;	
private ShortBlockIndex _sound = new ShortBlockIndex();
private ShortInteger _soundFrameIndex = new ShortInteger();
private CharInteger _leftFootFrameIndex = new CharInteger();
private CharInteger _rightFootFrameIndex = new CharInteger();
private Pad  __unnamed;	
private Pad  __unnamed2;	
private Data _frameInfo = new Data();
private Pad  __unnamed3;	
private Pad  __unnamed4;	
private Pad  __unnamed5;	
private LongInteger _offsetToCompressedData = new LongInteger();
private Data _defaultData = new Data();
private Data _frameData = new Data();
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public Enum Type
{
  get { return _type; }
  set { _type = value; }
}
public ShortInteger FrameCount
{
  get { return _frameCount; }
  set { _frameCount = value; }
}
public ShortInteger FrameSize
{
  get { return _frameSize; }
  set { _frameSize = value; }
}
public Enum FrameInfoType
{
  get { return _frameInfoType; }
  set { _frameInfoType = value; }
}
public LongInteger NodeListChecksum
{
  get { return _nodeListChecksum; }
  set { _nodeListChecksum = value; }
}
public ShortInteger NodeCount
{
  get { return _nodeCount; }
  set { _nodeCount = value; }
}
public ShortInteger LoopFrameIndex
{
  get { return _loopFrameIndex; }
  set { _loopFrameIndex = value; }
}
public RealFraction Weight
{
  get { return _weight; }
  set { _weight = value; }
}
public ShortInteger KeyFrameIndex
{
  get { return _keyFrameIndex; }
  set { _keyFrameIndex = value; }
}
public ShortInteger SecondKeyFrameIndex
{
  get { return _secondKeyFrameIndex; }
  set { _secondKeyFrameIndex = value; }
}
public ShortBlockIndex NextAnimation
{
  get { return _nextAnimation; }
  set { _nextAnimation = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public ShortBlockIndex Sound
{
  get { return _sound; }
  set { _sound = value; }
}
public ShortInteger SoundFrameIndex
{
  get { return _soundFrameIndex; }
  set { _soundFrameIndex = value; }
}
public CharInteger LeftFootFrameIndex
{
  get { return _leftFootFrameIndex; }
  set { _leftFootFrameIndex = value; }
}
public CharInteger RightFootFrameIndex
{
  get { return _rightFootFrameIndex; }
  set { _rightFootFrameIndex = value; }
}
public Data FrameInfo
{
  get { return _frameInfo; }
  set { _frameInfo = value; }
}
public LongInteger OffsetToCompressedData
{
  get { return _offsetToCompressedData; }
  set { _offsetToCompressedData = value; }
}
public Data DefaultData
{
  get { return _defaultData; }
  set { _defaultData = value; }
}
public Data FrameData
{
  get { return _frameData; }
  set { _frameData = value; }
}
public AnimationBlock()
{
_flags = new Flags(2);
__unnamed = new Pad(2);
__unnamed2 = new Pad(4);
__unnamed3 = new Pad(8);
__unnamed4 = new Pad(8);
__unnamed5 = new Pad(4);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _type.Read(reader);
  _frameCount.Read(reader);
  _frameSize.Read(reader);
  _frameInfoType.Read(reader);
  _nodeListChecksum.Read(reader);
  _nodeCount.Read(reader);
  _loopFrameIndex.Read(reader);
  _weight.Read(reader);
  _keyFrameIndex.Read(reader);
  _secondKeyFrameIndex.Read(reader);
  _nextAnimation.Read(reader);
  _flags.Read(reader);
  _sound.Read(reader);
  _soundFrameIndex.Read(reader);
  _leftFootFrameIndex.Read(reader);
  _rightFootFrameIndex.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  _frameInfo.Read(reader);
  __unnamed3.Read(reader);
  __unnamed4.Read(reader);
  __unnamed5.Read(reader);
  _offsetToCompressedData.Read(reader);
  _defaultData.Read(reader);
  _frameData.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_frameInfo.ReadBinary(reader);
_defaultData.ReadBinary(reader);
_frameData.ReadBinary(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _type.Write(writer);
    _frameCount.Write(writer);
    _frameSize.Write(writer);
    _frameInfoType.Write(writer);
    _nodeListChecksum.Write(writer);
    _nodeCount.Write(writer);
    _loopFrameIndex.Write(writer);
    _weight.Write(writer);
    _keyFrameIndex.Write(writer);
    _secondKeyFrameIndex.Write(writer);
    _nextAnimation.Write(writer);
    _flags.Write(writer);
    _sound.Write(writer);
    _soundFrameIndex.Write(writer);
    _leftFootFrameIndex.Write(writer);
    _rightFootFrameIndex.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    _frameInfo.Write(writer);
    __unnamed3.Write(writer);
    __unnamed4.Write(writer);
    __unnamed5.Write(writer);
    _offsetToCompressedData.Write(writer);
    _defaultData.Write(writer);
    _frameData.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_frameInfo.WriteBinary(writer);
_defaultData.WriteBinary(writer);
_frameData.WriteBinary(writer);
}
}
  }
}
