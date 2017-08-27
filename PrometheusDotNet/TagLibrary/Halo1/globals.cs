using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Globals : IBlock
  {
    public GlobalsBlock GlobalsValues = new GlobalsBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'Globals'------------------------------------------------------");
      GlobalsValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      GlobalsValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      GlobalsValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      GlobalsValues.WriteChildData(writer);
    }
public class GlobalsBlock : IBlock
{
private Pad  __unnamed;	
private Block _sounds = new Block();
private Block _camera = new Block();
private Block _playerControl = new Block();
private Block _difficulty = new Block();
private Block _grenades = new Block();
private Block _rasterizerData = new Block();
private Block _interfaceBitmaps = new Block();
private Block _weaponListUpdate_weapon_listEnumInGame_globals_Pth = new Block();
private Block _cheatPowerups = new Block();
private Block _multiplayerInformation = new Block();
private Block _playerInformation = new Block();
private Block _firstPersonInterface = new Block();
private Block _fallingDamage = new Block();
private Block _materials = new Block();
private Block _playlistMembers = new Block();
public class SoundBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public SoundBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(SoundBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new SoundBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public SoundBlock this[int index]
  {
    get { return (InnerList[index] as SoundBlock); }
  }
}
private SoundBlockCollection _soundsCollection;
public SoundBlockCollection Sounds
{
  get { return _soundsCollection; }
}
public class CameraBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public CameraBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(CameraBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new CameraBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public CameraBlock this[int index]
  {
    get { return (InnerList[index] as CameraBlock); }
  }
}
private CameraBlockCollection _cameraCollection;
public CameraBlockCollection Camera
{
  get { return _cameraCollection; }
}
public class PlayerControlBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public PlayerControlBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(PlayerControlBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new PlayerControlBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public PlayerControlBlock this[int index]
  {
    get { return (InnerList[index] as PlayerControlBlock); }
  }
}
private PlayerControlBlockCollection _playerControlCollection;
public PlayerControlBlockCollection PlayerControl
{
  get { return _playerControlCollection; }
}
public class DifficultyBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public DifficultyBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(DifficultyBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new DifficultyBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public DifficultyBlock this[int index]
  {
    get { return (InnerList[index] as DifficultyBlock); }
  }
}
private DifficultyBlockCollection _difficultyCollection;
public DifficultyBlockCollection Difficulty
{
  get { return _difficultyCollection; }
}
public class GrenadesBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public GrenadesBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(GrenadesBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new GrenadesBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public GrenadesBlock this[int index]
  {
    get { return (InnerList[index] as GrenadesBlock); }
  }
}
private GrenadesBlockCollection _grenadesCollection;
public GrenadesBlockCollection Grenades
{
  get { return _grenadesCollection; }
}
public class RasterizerDataBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public RasterizerDataBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(RasterizerDataBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new RasterizerDataBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public RasterizerDataBlock this[int index]
  {
    get { return (InnerList[index] as RasterizerDataBlock); }
  }
}
private RasterizerDataBlockCollection _rasterizerDataCollection;
public RasterizerDataBlockCollection RasterizerData
{
  get { return _rasterizerDataCollection; }
}
public class InterfaceTagReferencesBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public InterfaceTagReferencesBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(InterfaceTagReferencesBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new InterfaceTagReferencesBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public InterfaceTagReferencesBlock this[int index]
  {
    get { return (InnerList[index] as InterfaceTagReferencesBlock); }
  }
}
private InterfaceTagReferencesBlockCollection _interfaceBitmapsCollection;
public InterfaceTagReferencesBlockCollection InterfaceBitmaps
{
  get { return _interfaceBitmapsCollection; }
}
public class CheatWeaponsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public CheatWeaponsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(CheatWeaponsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new CheatWeaponsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public CheatWeaponsBlock this[int index]
  {
    get { return (InnerList[index] as CheatWeaponsBlock); }
  }
}
private CheatWeaponsBlockCollection _weaponListUpdate_weapon_listEnumInGame_globals_PthCollection;
public CheatWeaponsBlockCollection WeaponListUpdate_weapon_listEnumInGame_globals_Pth
{
  get { return _weaponListUpdate_weapon_listEnumInGame_globals_PthCollection; }
}
public class CheatPowerupsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public CheatPowerupsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(CheatPowerupsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new CheatPowerupsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public CheatPowerupsBlock this[int index]
  {
    get { return (InnerList[index] as CheatPowerupsBlock); }
  }
}
private CheatPowerupsBlockCollection _cheatPowerupsCollection;
public CheatPowerupsBlockCollection CheatPowerups
{
  get { return _cheatPowerupsCollection; }
}
public class MultiplayerInformationBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public MultiplayerInformationBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(MultiplayerInformationBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new MultiplayerInformationBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public MultiplayerInformationBlock this[int index]
  {
    get { return (InnerList[index] as MultiplayerInformationBlock); }
  }
}
private MultiplayerInformationBlockCollection _multiplayerInformationCollection;
public MultiplayerInformationBlockCollection MultiplayerInformation
{
  get { return _multiplayerInformationCollection; }
}
public class PlayerInformationBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public PlayerInformationBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(PlayerInformationBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new PlayerInformationBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public PlayerInformationBlock this[int index]
  {
    get { return (InnerList[index] as PlayerInformationBlock); }
  }
}
private PlayerInformationBlockCollection _playerInformationCollection;
public PlayerInformationBlockCollection PlayerInformation
{
  get { return _playerInformationCollection; }
}
public class FirstPersonInterfaceBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public FirstPersonInterfaceBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(FirstPersonInterfaceBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new FirstPersonInterfaceBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public FirstPersonInterfaceBlock this[int index]
  {
    get { return (InnerList[index] as FirstPersonInterfaceBlock); }
  }
}
private FirstPersonInterfaceBlockCollection _firstPersonInterfaceCollection;
public FirstPersonInterfaceBlockCollection FirstPersonInterface
{
  get { return _firstPersonInterfaceCollection; }
}
public class FallingDamageBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public FallingDamageBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(FallingDamageBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new FallingDamageBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public FallingDamageBlock this[int index]
  {
    get { return (InnerList[index] as FallingDamageBlock); }
  }
}
private FallingDamageBlockCollection _fallingDamageCollection;
public FallingDamageBlockCollection FallingDamage
{
  get { return _fallingDamageCollection; }
}
public class MaterialsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public MaterialsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(MaterialsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new MaterialsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public MaterialsBlock this[int index]
  {
    get { return (InnerList[index] as MaterialsBlock); }
  }
}
private MaterialsBlockCollection _materialsCollection;
public MaterialsBlockCollection Materials
{
  get { return _materialsCollection; }
}
public class PlaylistAutogenerateChoiceBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public PlaylistAutogenerateChoiceBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(PlaylistAutogenerateChoiceBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new PlaylistAutogenerateChoiceBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public PlaylistAutogenerateChoiceBlock this[int index]
  {
    get { return (InnerList[index] as PlaylistAutogenerateChoiceBlock); }
  }
}
private PlaylistAutogenerateChoiceBlockCollection _playlistMembersCollection;
public PlaylistAutogenerateChoiceBlockCollection PlaylistMembers
{
  get { return _playlistMembersCollection; }
}
public GlobalsBlock()
{
__unnamed = new Pad(248);
_soundsCollection = new SoundBlockCollection(_sounds);
_cameraCollection = new CameraBlockCollection(_camera);
_playerControlCollection = new PlayerControlBlockCollection(_playerControl);
_difficultyCollection = new DifficultyBlockCollection(_difficulty);
_grenadesCollection = new GrenadesBlockCollection(_grenades);
_rasterizerDataCollection = new RasterizerDataBlockCollection(_rasterizerData);
_interfaceBitmapsCollection = new InterfaceTagReferencesBlockCollection(_interfaceBitmaps);
_weaponListUpdate_weapon_listEnumInGame_globals_PthCollection = new CheatWeaponsBlockCollection(_weaponListUpdate_weapon_listEnumInGame_globals_Pth);
_cheatPowerupsCollection = new CheatPowerupsBlockCollection(_cheatPowerups);
_multiplayerInformationCollection = new MultiplayerInformationBlockCollection(_multiplayerInformation);
_playerInformationCollection = new PlayerInformationBlockCollection(_playerInformation);
_firstPersonInterfaceCollection = new FirstPersonInterfaceBlockCollection(_firstPersonInterface);
_fallingDamageCollection = new FallingDamageBlockCollection(_fallingDamage);
_materialsCollection = new MaterialsBlockCollection(_materials);
_playlistMembersCollection = new PlaylistAutogenerateChoiceBlockCollection(_playlistMembers);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  _sounds.Read(reader);
  _camera.Read(reader);
  _playerControl.Read(reader);
  _difficulty.Read(reader);
  _grenades.Read(reader);
  _rasterizerData.Read(reader);
  _interfaceBitmaps.Read(reader);
  _weaponListUpdate_weapon_listEnumInGame_globals_Pth.Read(reader);
  _cheatPowerups.Read(reader);
  _multiplayerInformation.Read(reader);
  _playerInformation.Read(reader);
  _firstPersonInterface.Read(reader);
  _fallingDamage.Read(reader);
  _materials.Read(reader);
  _playlistMembers.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_sounds.Count; x++)
{
  Sounds.AddNew();
  Sounds[x].Read(reader);
}
for (int x=0; x<_sounds.Count; x++)
  Sounds[x].ReadChildData(reader);
for (int x=0; x<_camera.Count; x++)
{
  Camera.AddNew();
  Camera[x].Read(reader);
}
for (int x=0; x<_camera.Count; x++)
  Camera[x].ReadChildData(reader);
for (int x=0; x<_playerControl.Count; x++)
{
  PlayerControl.AddNew();
  PlayerControl[x].Read(reader);
}
for (int x=0; x<_playerControl.Count; x++)
  PlayerControl[x].ReadChildData(reader);
for (int x=0; x<_difficulty.Count; x++)
{
  Difficulty.AddNew();
  Difficulty[x].Read(reader);
}
for (int x=0; x<_difficulty.Count; x++)
  Difficulty[x].ReadChildData(reader);
for (int x=0; x<_grenades.Count; x++)
{
  Grenades.AddNew();
  Grenades[x].Read(reader);
}
for (int x=0; x<_grenades.Count; x++)
  Grenades[x].ReadChildData(reader);
for (int x=0; x<_rasterizerData.Count; x++)
{
  RasterizerData.AddNew();
  RasterizerData[x].Read(reader);
}
for (int x=0; x<_rasterizerData.Count; x++)
  RasterizerData[x].ReadChildData(reader);
for (int x=0; x<_interfaceBitmaps.Count; x++)
{
  InterfaceBitmaps.AddNew();
  InterfaceBitmaps[x].Read(reader);
}
for (int x=0; x<_interfaceBitmaps.Count; x++)
  InterfaceBitmaps[x].ReadChildData(reader);
for (int x=0; x<_weaponListUpdate_weapon_listEnumInGame_globals_Pth.Count; x++)
{
  WeaponListUpdate_weapon_listEnumInGame_globals_Pth.AddNew();
  WeaponListUpdate_weapon_listEnumInGame_globals_Pth[x].Read(reader);
}
for (int x=0; x<_weaponListUpdate_weapon_listEnumInGame_globals_Pth.Count; x++)
  WeaponListUpdate_weapon_listEnumInGame_globals_Pth[x].ReadChildData(reader);
for (int x=0; x<_cheatPowerups.Count; x++)
{
  CheatPowerups.AddNew();
  CheatPowerups[x].Read(reader);
}
for (int x=0; x<_cheatPowerups.Count; x++)
  CheatPowerups[x].ReadChildData(reader);
for (int x=0; x<_multiplayerInformation.Count; x++)
{
  MultiplayerInformation.AddNew();
  MultiplayerInformation[x].Read(reader);
}
for (int x=0; x<_multiplayerInformation.Count; x++)
  MultiplayerInformation[x].ReadChildData(reader);
for (int x=0; x<_playerInformation.Count; x++)
{
  PlayerInformation.AddNew();
  PlayerInformation[x].Read(reader);
}
for (int x=0; x<_playerInformation.Count; x++)
  PlayerInformation[x].ReadChildData(reader);
for (int x=0; x<_firstPersonInterface.Count; x++)
{
  FirstPersonInterface.AddNew();
  FirstPersonInterface[x].Read(reader);
}
for (int x=0; x<_firstPersonInterface.Count; x++)
  FirstPersonInterface[x].ReadChildData(reader);
for (int x=0; x<_fallingDamage.Count; x++)
{
  FallingDamage.AddNew();
  FallingDamage[x].Read(reader);
}
for (int x=0; x<_fallingDamage.Count; x++)
  FallingDamage[x].ReadChildData(reader);
for (int x=0; x<_materials.Count; x++)
{
  Materials.AddNew();
  Materials[x].Read(reader);
}
for (int x=0; x<_materials.Count; x++)
  Materials[x].ReadChildData(reader);
for (int x=0; x<_playlistMembers.Count; x++)
{
  PlaylistMembers.AddNew();
  PlaylistMembers[x].Read(reader);
}
for (int x=0; x<_playlistMembers.Count; x++)
  PlaylistMembers[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    _sounds.Write(writer);
    _camera.Write(writer);
    _playerControl.Write(writer);
    _difficulty.Write(writer);
    _grenades.Write(writer);
    _rasterizerData.Write(writer);
    _interfaceBitmaps.Write(writer);
    _weaponListUpdate_weapon_listEnumInGame_globals_Pth.Write(writer);
    _cheatPowerups.Write(writer);
    _multiplayerInformation.Write(writer);
    _playerInformation.Write(writer);
    _firstPersonInterface.Write(writer);
    _fallingDamage.Write(writer);
    _materials.Write(writer);
    _playlistMembers.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_sounds.UpdateReflexiveOffset(writer);
for (int x=0; x<_sounds.Count; x++)
{
  Sounds[x].Write(writer);
}
for (int x=0; x<_sounds.Count; x++)
  Sounds[x].WriteChildData(writer);
_camera.UpdateReflexiveOffset(writer);
for (int x=0; x<_camera.Count; x++)
{
  Camera[x].Write(writer);
}
for (int x=0; x<_camera.Count; x++)
  Camera[x].WriteChildData(writer);
_playerControl.UpdateReflexiveOffset(writer);
for (int x=0; x<_playerControl.Count; x++)
{
  PlayerControl[x].Write(writer);
}
for (int x=0; x<_playerControl.Count; x++)
  PlayerControl[x].WriteChildData(writer);
_difficulty.UpdateReflexiveOffset(writer);
for (int x=0; x<_difficulty.Count; x++)
{
  Difficulty[x].Write(writer);
}
for (int x=0; x<_difficulty.Count; x++)
  Difficulty[x].WriteChildData(writer);
_grenades.UpdateReflexiveOffset(writer);
for (int x=0; x<_grenades.Count; x++)
{
  Grenades[x].Write(writer);
}
for (int x=0; x<_grenades.Count; x++)
  Grenades[x].WriteChildData(writer);
_rasterizerData.UpdateReflexiveOffset(writer);
for (int x=0; x<_rasterizerData.Count; x++)
{
  RasterizerData[x].Write(writer);
}
for (int x=0; x<_rasterizerData.Count; x++)
  RasterizerData[x].WriteChildData(writer);
_interfaceBitmaps.UpdateReflexiveOffset(writer);
for (int x=0; x<_interfaceBitmaps.Count; x++)
{
  InterfaceBitmaps[x].Write(writer);
}
for (int x=0; x<_interfaceBitmaps.Count; x++)
  InterfaceBitmaps[x].WriteChildData(writer);
_weaponListUpdate_weapon_listEnumInGame_globals_Pth.UpdateReflexiveOffset(writer);
for (int x=0; x<_weaponListUpdate_weapon_listEnumInGame_globals_Pth.Count; x++)
{
  WeaponListUpdate_weapon_listEnumInGame_globals_Pth[x].Write(writer);
}
for (int x=0; x<_weaponListUpdate_weapon_listEnumInGame_globals_Pth.Count; x++)
  WeaponListUpdate_weapon_listEnumInGame_globals_Pth[x].WriteChildData(writer);
_cheatPowerups.UpdateReflexiveOffset(writer);
for (int x=0; x<_cheatPowerups.Count; x++)
{
  CheatPowerups[x].Write(writer);
}
for (int x=0; x<_cheatPowerups.Count; x++)
  CheatPowerups[x].WriteChildData(writer);
_multiplayerInformation.UpdateReflexiveOffset(writer);
for (int x=0; x<_multiplayerInformation.Count; x++)
{
  MultiplayerInformation[x].Write(writer);
}
for (int x=0; x<_multiplayerInformation.Count; x++)
  MultiplayerInformation[x].WriteChildData(writer);
_playerInformation.UpdateReflexiveOffset(writer);
for (int x=0; x<_playerInformation.Count; x++)
{
  PlayerInformation[x].Write(writer);
}
for (int x=0; x<_playerInformation.Count; x++)
  PlayerInformation[x].WriteChildData(writer);
_firstPersonInterface.UpdateReflexiveOffset(writer);
for (int x=0; x<_firstPersonInterface.Count; x++)
{
  FirstPersonInterface[x].Write(writer);
}
for (int x=0; x<_firstPersonInterface.Count; x++)
  FirstPersonInterface[x].WriteChildData(writer);
_fallingDamage.UpdateReflexiveOffset(writer);
for (int x=0; x<_fallingDamage.Count; x++)
{
  FallingDamage[x].Write(writer);
}
for (int x=0; x<_fallingDamage.Count; x++)
  FallingDamage[x].WriteChildData(writer);
_materials.UpdateReflexiveOffset(writer);
for (int x=0; x<_materials.Count; x++)
{
  Materials[x].Write(writer);
}
for (int x=0; x<_materials.Count; x++)
  Materials[x].WriteChildData(writer);
_playlistMembers.UpdateReflexiveOffset(writer);
for (int x=0; x<_playlistMembers.Count; x++)
{
  PlaylistMembers[x].Write(writer);
}
for (int x=0; x<_playlistMembers.Count; x++)
  PlaylistMembers[x].WriteChildData(writer);
}
}
public class SoundBlock : IBlock
{
private TagReference _sound = new TagReference();
public TagReference Sound
{
  get { return _sound; }
  set { _sound = value; }
}
public SoundBlock()
{

}
public void Read(BinaryReader reader)
{
  _sound.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_sound.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _sound.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_sound.WriteString(writer);
}
}
public class CameraBlock : IBlock
{
private TagReference _defaultUnitCameraTrack = new TagReference();
public TagReference DefaultUnitCameraTrack
{
  get { return _defaultUnitCameraTrack; }
  set { _defaultUnitCameraTrack = value; }
}
public CameraBlock()
{

}
public void Read(BinaryReader reader)
{
  _defaultUnitCameraTrack.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_defaultUnitCameraTrack.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _defaultUnitCameraTrack.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_defaultUnitCameraTrack.WriteString(writer);
}
}
public class PlayerControlBlock : IBlock
{
private RealFraction _magnetismFriction = new RealFraction();
private RealFraction _magnetismAdhesion = new RealFraction();
private RealFraction _inconsequentialTargetScale = new RealFraction();
private Pad  __unnamed;	
private Real _lookAccelerationTime = new Real();
private Real _lookAccelerationScale = new Real();
private RealFraction _lookPegThreshold = new RealFraction();
private Real _lookDefaultPitchRate = new Real();
private Real _lookDefaultYawRate = new Real();
private Real _lookAutolevellingScale = new Real();
private Pad  __unnamed2;	
private ShortInteger _minimumWeaponSwapTicks = new ShortInteger();
private ShortInteger _minimumAutolevellingTicks = new ShortInteger();
private Angle _minimumAngleForVehicleFlipping = new Angle();
private Block _lookFunction = new Block();
public class LookFunctionBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public LookFunctionBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(LookFunctionBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new LookFunctionBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public LookFunctionBlock this[int index]
  {
    get { return (InnerList[index] as LookFunctionBlock); }
  }
}
private LookFunctionBlockCollection _lookFunctionCollection;
public LookFunctionBlockCollection LookFunction
{
  get { return _lookFunctionCollection; }
}
public RealFraction MagnetismFriction
{
  get { return _magnetismFriction; }
  set { _magnetismFriction = value; }
}
public RealFraction MagnetismAdhesion
{
  get { return _magnetismAdhesion; }
  set { _magnetismAdhesion = value; }
}
public RealFraction InconsequentialTargetScale
{
  get { return _inconsequentialTargetScale; }
  set { _inconsequentialTargetScale = value; }
}
public Real LookAccelerationTime
{
  get { return _lookAccelerationTime; }
  set { _lookAccelerationTime = value; }
}
public Real LookAccelerationScale
{
  get { return _lookAccelerationScale; }
  set { _lookAccelerationScale = value; }
}
public RealFraction LookPegThreshold
{
  get { return _lookPegThreshold; }
  set { _lookPegThreshold = value; }
}
public Real LookDefaultPitchRate
{
  get { return _lookDefaultPitchRate; }
  set { _lookDefaultPitchRate = value; }
}
public Real LookDefaultYawRate
{
  get { return _lookDefaultYawRate; }
  set { _lookDefaultYawRate = value; }
}
public Real LookAutolevellingScale
{
  get { return _lookAutolevellingScale; }
  set { _lookAutolevellingScale = value; }
}
public ShortInteger MinimumWeaponSwapTicks
{
  get { return _minimumWeaponSwapTicks; }
  set { _minimumWeaponSwapTicks = value; }
}
public ShortInteger MinimumAutolevellingTicks
{
  get { return _minimumAutolevellingTicks; }
  set { _minimumAutolevellingTicks = value; }
}
public Angle MinimumAngleForVehicleFlipping
{
  get { return _minimumAngleForVehicleFlipping; }
  set { _minimumAngleForVehicleFlipping = value; }
}
public PlayerControlBlock()
{
__unnamed = new Pad(52);
__unnamed2 = new Pad(20);
_lookFunctionCollection = new LookFunctionBlockCollection(_lookFunction);

}
public void Read(BinaryReader reader)
{
  _magnetismFriction.Read(reader);
  _magnetismAdhesion.Read(reader);
  _inconsequentialTargetScale.Read(reader);
  __unnamed.Read(reader);
  _lookAccelerationTime.Read(reader);
  _lookAccelerationScale.Read(reader);
  _lookPegThreshold.Read(reader);
  _lookDefaultPitchRate.Read(reader);
  _lookDefaultYawRate.Read(reader);
  _lookAutolevellingScale.Read(reader);
  __unnamed2.Read(reader);
  _minimumWeaponSwapTicks.Read(reader);
  _minimumAutolevellingTicks.Read(reader);
  _minimumAngleForVehicleFlipping.Read(reader);
  _lookFunction.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_lookFunction.Count; x++)
{
  LookFunction.AddNew();
  LookFunction[x].Read(reader);
}
for (int x=0; x<_lookFunction.Count; x++)
  LookFunction[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _magnetismFriction.Write(writer);
    _magnetismAdhesion.Write(writer);
    _inconsequentialTargetScale.Write(writer);
    __unnamed.Write(writer);
    _lookAccelerationTime.Write(writer);
    _lookAccelerationScale.Write(writer);
    _lookPegThreshold.Write(writer);
    _lookDefaultPitchRate.Write(writer);
    _lookDefaultYawRate.Write(writer);
    _lookAutolevellingScale.Write(writer);
    __unnamed2.Write(writer);
    _minimumWeaponSwapTicks.Write(writer);
    _minimumAutolevellingTicks.Write(writer);
    _minimumAngleForVehicleFlipping.Write(writer);
    _lookFunction.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_lookFunction.UpdateReflexiveOffset(writer);
for (int x=0; x<_lookFunction.Count; x++)
{
  LookFunction[x].Write(writer);
}
for (int x=0; x<_lookFunction.Count; x++)
  LookFunction[x].WriteChildData(writer);
}
}
public class LookFunctionBlock : IBlock
{
private Real _scale = new Real();
public Real Scale
{
  get { return _scale; }
  set { _scale = value; }
}
public LookFunctionBlock()
{

}
public void Read(BinaryReader reader)
{
  _scale.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _scale.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class DifficultyBlock : IBlock
{
private Real _easyEnemyDamage = new Real();
private Real _normalEnemyDamage = new Real();
private Real _hardEnemyDamage = new Real();
private Real _imposs_PtEnemyDamage = new Real();
private Real _easyEnemyVitality = new Real();
private Real _normalEnemyVitality = new Real();
private Real _hardEnemyVitality = new Real();
private Real _imposs_PtEnemyVitality = new Real();
private Real _easyEnemyShield = new Real();
private Real _normalEnemyShield = new Real();
private Real _hardEnemyShield = new Real();
private Real _imposs_PtEnemyShield = new Real();
private Real _easyEnemyRecharge = new Real();
private Real _normalEnemyRecharge = new Real();
private Real _hardEnemyRecharge = new Real();
private Real _imposs_PtEnemyRecharge = new Real();
private Real _easyFriendDamage = new Real();
private Real _normalFriendDamage = new Real();
private Real _hardFriendDamage = new Real();
private Real _imposs_PtFriendDamage = new Real();
private Real _easyFriendVitality = new Real();
private Real _normalFriendVitality = new Real();
private Real _hardFriendVitality = new Real();
private Real _imposs_PtFriendVitality = new Real();
private Real _easyFriendShield = new Real();
private Real _normalFriendShield = new Real();
private Real _hardFriendShield = new Real();
private Real _imposs_PtFriendShield = new Real();
private Real _easyFriendRecharge = new Real();
private Real _normalFriendRecharge = new Real();
private Real _hardFriendRecharge = new Real();
private Real _imposs_PtFriendRecharge = new Real();
private Real _easyInfectionForms = new Real();
private Real _normalInfectionForms = new Real();
private Real _hardInfectionForms = new Real();
private Real _imposs_PtInfectionForms = new Real();
private Pad  __unnamed;	
private Real _easyRateOfFire = new Real();
private Real _normalRateOfFire = new Real();
private Real _hardRateOfFire = new Real();
private Real _imposs_PtRateOfFire = new Real();
private Real _easyProjectileError = new Real();
private Real _normalProjectileError = new Real();
private Real _hardProjectileError = new Real();
private Real _imposs_PtProjectileError = new Real();
private Real _easyBurstError = new Real();
private Real _normalBurstError = new Real();
private Real _hardBurstError = new Real();
private Real _imposs_PtBurstError = new Real();
private Real _easyNewTargetDelay = new Real();
private Real _normalNewTargetDelay = new Real();
private Real _hardNewTargetDelay = new Real();
private Real _imposs_PtNewTargetDelay = new Real();
private Real _easyBurstSeparation = new Real();
private Real _normalBurstSeparation = new Real();
private Real _hardBurstSeparation = new Real();
private Real _imposs_PtBurstSeparation = new Real();
private Real _easyTargetTracking = new Real();
private Real _normalTargetTracking = new Real();
private Real _hardTargetTracking = new Real();
private Real _imposs_PtTargetTracking = new Real();
private Real _easyTargetLeading = new Real();
private Real _normalTargetLeading = new Real();
private Real _hardTargetLeading = new Real();
private Real _imposs_PtTargetLeading = new Real();
private Real _easyOverchargeChance = new Real();
private Real _normalOverchargeChance = new Real();
private Real _hardOverchargeChance = new Real();
private Real _imposs_PtOverchargeChance = new Real();
private Real _easySpecialFireDelay = new Real();
private Real _normalSpecialFireDelay = new Real();
private Real _hardSpecialFireDelay = new Real();
private Real _imposs_PtSpecialFireDelay = new Real();
private Real _easyGuidanceVsPlayer = new Real();
private Real _normalGuidanceVsPlayer = new Real();
private Real _hardGuidanceVsPlayer = new Real();
private Real _imposs_PtGuidanceVsPlayer = new Real();
private Real _easyMeleeDelayBase = new Real();
private Real _normalMeleeDelayBase = new Real();
private Real _hardMeleeDelayBase = new Real();
private Real _imposs_PtMeleeDelayBase = new Real();
private Real _easyMeleeDelayScale = new Real();
private Real _normalMeleeDelayScale = new Real();
private Real _hardMeleeDelayScale = new Real();
private Real _imposs_PtMeleeDelayScale = new Real();
private Pad  __unnamed2;	
private Real _easyGrenadeChanceScale = new Real();
private Real _normalGrenadeChanceScale = new Real();
private Real _hardGrenadeChanceScale = new Real();
private Real _imposs_PtGrenadeChanceScale = new Real();
private Real _easyGrenadeTimerScale = new Real();
private Real _normalGrenadeTimerScale = new Real();
private Real _hardGrenadeTimerScale = new Real();
private Real _imposs_PtGrenadeTimerScale = new Real();
private Pad  __unnamed3;	
private Pad  __unnamed4;	
private Pad  __unnamed5;	
private Real _easyMajorUpgradeNormal = new Real();
private Real _normalMajorUpgradeNormal = new Real();
private Real _hardMajorUpgradeNormal = new Real();
private Real _imposs_PtMajorUpgradeNormal = new Real();
private Real _easyMajorUpgradeFew = new Real();
private Real _normalMajorUpgradeFew = new Real();
private Real _hardMajorUpgradeFew = new Real();
private Real _imposs_PtMajorUpgradeFew = new Real();
private Real _easyMajorUpgradeMany = new Real();
private Real _normalMajorUpgradeMany = new Real();
private Real _hardMajorUpgradeMany = new Real();
private Real _imposs_PtMajorUpgradeMany = new Real();
private Pad  __unnamed6;	
private Pad  __unnamed7;	
private Pad  __unnamed8;	
private Pad  __unnamed9;	
private Pad  __unnamed10;	
public Real EasyEnemyDamage
{
  get { return _easyEnemyDamage; }
  set { _easyEnemyDamage = value; }
}
public Real NormalEnemyDamage
{
  get { return _normalEnemyDamage; }
  set { _normalEnemyDamage = value; }
}
public Real HardEnemyDamage
{
  get { return _hardEnemyDamage; }
  set { _hardEnemyDamage = value; }
}
public Real Imposs_PtEnemyDamage
{
  get { return _imposs_PtEnemyDamage; }
  set { _imposs_PtEnemyDamage = value; }
}
public Real EasyEnemyVitality
{
  get { return _easyEnemyVitality; }
  set { _easyEnemyVitality = value; }
}
public Real NormalEnemyVitality
{
  get { return _normalEnemyVitality; }
  set { _normalEnemyVitality = value; }
}
public Real HardEnemyVitality
{
  get { return _hardEnemyVitality; }
  set { _hardEnemyVitality = value; }
}
public Real Imposs_PtEnemyVitality
{
  get { return _imposs_PtEnemyVitality; }
  set { _imposs_PtEnemyVitality = value; }
}
public Real EasyEnemyShield
{
  get { return _easyEnemyShield; }
  set { _easyEnemyShield = value; }
}
public Real NormalEnemyShield
{
  get { return _normalEnemyShield; }
  set { _normalEnemyShield = value; }
}
public Real HardEnemyShield
{
  get { return _hardEnemyShield; }
  set { _hardEnemyShield = value; }
}
public Real Imposs_PtEnemyShield
{
  get { return _imposs_PtEnemyShield; }
  set { _imposs_PtEnemyShield = value; }
}
public Real EasyEnemyRecharge
{
  get { return _easyEnemyRecharge; }
  set { _easyEnemyRecharge = value; }
}
public Real NormalEnemyRecharge
{
  get { return _normalEnemyRecharge; }
  set { _normalEnemyRecharge = value; }
}
public Real HardEnemyRecharge
{
  get { return _hardEnemyRecharge; }
  set { _hardEnemyRecharge = value; }
}
public Real Imposs_PtEnemyRecharge
{
  get { return _imposs_PtEnemyRecharge; }
  set { _imposs_PtEnemyRecharge = value; }
}
public Real EasyFriendDamage
{
  get { return _easyFriendDamage; }
  set { _easyFriendDamage = value; }
}
public Real NormalFriendDamage
{
  get { return _normalFriendDamage; }
  set { _normalFriendDamage = value; }
}
public Real HardFriendDamage
{
  get { return _hardFriendDamage; }
  set { _hardFriendDamage = value; }
}
public Real Imposs_PtFriendDamage
{
  get { return _imposs_PtFriendDamage; }
  set { _imposs_PtFriendDamage = value; }
}
public Real EasyFriendVitality
{
  get { return _easyFriendVitality; }
  set { _easyFriendVitality = value; }
}
public Real NormalFriendVitality
{
  get { return _normalFriendVitality; }
  set { _normalFriendVitality = value; }
}
public Real HardFriendVitality
{
  get { return _hardFriendVitality; }
  set { _hardFriendVitality = value; }
}
public Real Imposs_PtFriendVitality
{
  get { return _imposs_PtFriendVitality; }
  set { _imposs_PtFriendVitality = value; }
}
public Real EasyFriendShield
{
  get { return _easyFriendShield; }
  set { _easyFriendShield = value; }
}
public Real NormalFriendShield
{
  get { return _normalFriendShield; }
  set { _normalFriendShield = value; }
}
public Real HardFriendShield
{
  get { return _hardFriendShield; }
  set { _hardFriendShield = value; }
}
public Real Imposs_PtFriendShield
{
  get { return _imposs_PtFriendShield; }
  set { _imposs_PtFriendShield = value; }
}
public Real EasyFriendRecharge
{
  get { return _easyFriendRecharge; }
  set { _easyFriendRecharge = value; }
}
public Real NormalFriendRecharge
{
  get { return _normalFriendRecharge; }
  set { _normalFriendRecharge = value; }
}
public Real HardFriendRecharge
{
  get { return _hardFriendRecharge; }
  set { _hardFriendRecharge = value; }
}
public Real Imposs_PtFriendRecharge
{
  get { return _imposs_PtFriendRecharge; }
  set { _imposs_PtFriendRecharge = value; }
}
public Real EasyInfectionForms
{
  get { return _easyInfectionForms; }
  set { _easyInfectionForms = value; }
}
public Real NormalInfectionForms
{
  get { return _normalInfectionForms; }
  set { _normalInfectionForms = value; }
}
public Real HardInfectionForms
{
  get { return _hardInfectionForms; }
  set { _hardInfectionForms = value; }
}
public Real Imposs_PtInfectionForms
{
  get { return _imposs_PtInfectionForms; }
  set { _imposs_PtInfectionForms = value; }
}
public Real EasyRateOfFire
{
  get { return _easyRateOfFire; }
  set { _easyRateOfFire = value; }
}
public Real NormalRateOfFire
{
  get { return _normalRateOfFire; }
  set { _normalRateOfFire = value; }
}
public Real HardRateOfFire
{
  get { return _hardRateOfFire; }
  set { _hardRateOfFire = value; }
}
public Real Imposs_PtRateOfFire
{
  get { return _imposs_PtRateOfFire; }
  set { _imposs_PtRateOfFire = value; }
}
public Real EasyProjectileError
{
  get { return _easyProjectileError; }
  set { _easyProjectileError = value; }
}
public Real NormalProjectileError
{
  get { return _normalProjectileError; }
  set { _normalProjectileError = value; }
}
public Real HardProjectileError
{
  get { return _hardProjectileError; }
  set { _hardProjectileError = value; }
}
public Real Imposs_PtProjectileError
{
  get { return _imposs_PtProjectileError; }
  set { _imposs_PtProjectileError = value; }
}
public Real EasyBurstError
{
  get { return _easyBurstError; }
  set { _easyBurstError = value; }
}
public Real NormalBurstError
{
  get { return _normalBurstError; }
  set { _normalBurstError = value; }
}
public Real HardBurstError
{
  get { return _hardBurstError; }
  set { _hardBurstError = value; }
}
public Real Imposs_PtBurstError
{
  get { return _imposs_PtBurstError; }
  set { _imposs_PtBurstError = value; }
}
public Real EasyNewTargetDelay
{
  get { return _easyNewTargetDelay; }
  set { _easyNewTargetDelay = value; }
}
public Real NormalNewTargetDelay
{
  get { return _normalNewTargetDelay; }
  set { _normalNewTargetDelay = value; }
}
public Real HardNewTargetDelay
{
  get { return _hardNewTargetDelay; }
  set { _hardNewTargetDelay = value; }
}
public Real Imposs_PtNewTargetDelay
{
  get { return _imposs_PtNewTargetDelay; }
  set { _imposs_PtNewTargetDelay = value; }
}
public Real EasyBurstSeparation
{
  get { return _easyBurstSeparation; }
  set { _easyBurstSeparation = value; }
}
public Real NormalBurstSeparation
{
  get { return _normalBurstSeparation; }
  set { _normalBurstSeparation = value; }
}
public Real HardBurstSeparation
{
  get { return _hardBurstSeparation; }
  set { _hardBurstSeparation = value; }
}
public Real Imposs_PtBurstSeparation
{
  get { return _imposs_PtBurstSeparation; }
  set { _imposs_PtBurstSeparation = value; }
}
public Real EasyTargetTracking
{
  get { return _easyTargetTracking; }
  set { _easyTargetTracking = value; }
}
public Real NormalTargetTracking
{
  get { return _normalTargetTracking; }
  set { _normalTargetTracking = value; }
}
public Real HardTargetTracking
{
  get { return _hardTargetTracking; }
  set { _hardTargetTracking = value; }
}
public Real Imposs_PtTargetTracking
{
  get { return _imposs_PtTargetTracking; }
  set { _imposs_PtTargetTracking = value; }
}
public Real EasyTargetLeading
{
  get { return _easyTargetLeading; }
  set { _easyTargetLeading = value; }
}
public Real NormalTargetLeading
{
  get { return _normalTargetLeading; }
  set { _normalTargetLeading = value; }
}
public Real HardTargetLeading
{
  get { return _hardTargetLeading; }
  set { _hardTargetLeading = value; }
}
public Real Imposs_PtTargetLeading
{
  get { return _imposs_PtTargetLeading; }
  set { _imposs_PtTargetLeading = value; }
}
public Real EasyOverchargeChance
{
  get { return _easyOverchargeChance; }
  set { _easyOverchargeChance = value; }
}
public Real NormalOverchargeChance
{
  get { return _normalOverchargeChance; }
  set { _normalOverchargeChance = value; }
}
public Real HardOverchargeChance
{
  get { return _hardOverchargeChance; }
  set { _hardOverchargeChance = value; }
}
public Real Imposs_PtOverchargeChance
{
  get { return _imposs_PtOverchargeChance; }
  set { _imposs_PtOverchargeChance = value; }
}
public Real EasySpecialFireDelay
{
  get { return _easySpecialFireDelay; }
  set { _easySpecialFireDelay = value; }
}
public Real NormalSpecialFireDelay
{
  get { return _normalSpecialFireDelay; }
  set { _normalSpecialFireDelay = value; }
}
public Real HardSpecialFireDelay
{
  get { return _hardSpecialFireDelay; }
  set { _hardSpecialFireDelay = value; }
}
public Real Imposs_PtSpecialFireDelay
{
  get { return _imposs_PtSpecialFireDelay; }
  set { _imposs_PtSpecialFireDelay = value; }
}
public Real EasyGuidanceVsPlayer
{
  get { return _easyGuidanceVsPlayer; }
  set { _easyGuidanceVsPlayer = value; }
}
public Real NormalGuidanceVsPlayer
{
  get { return _normalGuidanceVsPlayer; }
  set { _normalGuidanceVsPlayer = value; }
}
public Real HardGuidanceVsPlayer
{
  get { return _hardGuidanceVsPlayer; }
  set { _hardGuidanceVsPlayer = value; }
}
public Real Imposs_PtGuidanceVsPlayer
{
  get { return _imposs_PtGuidanceVsPlayer; }
  set { _imposs_PtGuidanceVsPlayer = value; }
}
public Real EasyMeleeDelayBase
{
  get { return _easyMeleeDelayBase; }
  set { _easyMeleeDelayBase = value; }
}
public Real NormalMeleeDelayBase
{
  get { return _normalMeleeDelayBase; }
  set { _normalMeleeDelayBase = value; }
}
public Real HardMeleeDelayBase
{
  get { return _hardMeleeDelayBase; }
  set { _hardMeleeDelayBase = value; }
}
public Real Imposs_PtMeleeDelayBase
{
  get { return _imposs_PtMeleeDelayBase; }
  set { _imposs_PtMeleeDelayBase = value; }
}
public Real EasyMeleeDelayScale
{
  get { return _easyMeleeDelayScale; }
  set { _easyMeleeDelayScale = value; }
}
public Real NormalMeleeDelayScale
{
  get { return _normalMeleeDelayScale; }
  set { _normalMeleeDelayScale = value; }
}
public Real HardMeleeDelayScale
{
  get { return _hardMeleeDelayScale; }
  set { _hardMeleeDelayScale = value; }
}
public Real Imposs_PtMeleeDelayScale
{
  get { return _imposs_PtMeleeDelayScale; }
  set { _imposs_PtMeleeDelayScale = value; }
}
public Real EasyGrenadeChanceScale
{
  get { return _easyGrenadeChanceScale; }
  set { _easyGrenadeChanceScale = value; }
}
public Real NormalGrenadeChanceScale
{
  get { return _normalGrenadeChanceScale; }
  set { _normalGrenadeChanceScale = value; }
}
public Real HardGrenadeChanceScale
{
  get { return _hardGrenadeChanceScale; }
  set { _hardGrenadeChanceScale = value; }
}
public Real Imposs_PtGrenadeChanceScale
{
  get { return _imposs_PtGrenadeChanceScale; }
  set { _imposs_PtGrenadeChanceScale = value; }
}
public Real EasyGrenadeTimerScale
{
  get { return _easyGrenadeTimerScale; }
  set { _easyGrenadeTimerScale = value; }
}
public Real NormalGrenadeTimerScale
{
  get { return _normalGrenadeTimerScale; }
  set { _normalGrenadeTimerScale = value; }
}
public Real HardGrenadeTimerScale
{
  get { return _hardGrenadeTimerScale; }
  set { _hardGrenadeTimerScale = value; }
}
public Real Imposs_PtGrenadeTimerScale
{
  get { return _imposs_PtGrenadeTimerScale; }
  set { _imposs_PtGrenadeTimerScale = value; }
}
public Real EasyMajorUpgradeNormal
{
  get { return _easyMajorUpgradeNormal; }
  set { _easyMajorUpgradeNormal = value; }
}
public Real NormalMajorUpgradeNormal
{
  get { return _normalMajorUpgradeNormal; }
  set { _normalMajorUpgradeNormal = value; }
}
public Real HardMajorUpgradeNormal
{
  get { return _hardMajorUpgradeNormal; }
  set { _hardMajorUpgradeNormal = value; }
}
public Real Imposs_PtMajorUpgradeNormal
{
  get { return _imposs_PtMajorUpgradeNormal; }
  set { _imposs_PtMajorUpgradeNormal = value; }
}
public Real EasyMajorUpgradeFew
{
  get { return _easyMajorUpgradeFew; }
  set { _easyMajorUpgradeFew = value; }
}
public Real NormalMajorUpgradeFew
{
  get { return _normalMajorUpgradeFew; }
  set { _normalMajorUpgradeFew = value; }
}
public Real HardMajorUpgradeFew
{
  get { return _hardMajorUpgradeFew; }
  set { _hardMajorUpgradeFew = value; }
}
public Real Imposs_PtMajorUpgradeFew
{
  get { return _imposs_PtMajorUpgradeFew; }
  set { _imposs_PtMajorUpgradeFew = value; }
}
public Real EasyMajorUpgradeMany
{
  get { return _easyMajorUpgradeMany; }
  set { _easyMajorUpgradeMany = value; }
}
public Real NormalMajorUpgradeMany
{
  get { return _normalMajorUpgradeMany; }
  set { _normalMajorUpgradeMany = value; }
}
public Real HardMajorUpgradeMany
{
  get { return _hardMajorUpgradeMany; }
  set { _hardMajorUpgradeMany = value; }
}
public Real Imposs_PtMajorUpgradeMany
{
  get { return _imposs_PtMajorUpgradeMany; }
  set { _imposs_PtMajorUpgradeMany = value; }
}
public DifficultyBlock()
{
__unnamed = new Pad(16);
__unnamed2 = new Pad(16);
__unnamed3 = new Pad(16);
__unnamed4 = new Pad(16);
__unnamed5 = new Pad(16);
__unnamed6 = new Pad(16);
__unnamed7 = new Pad(16);
__unnamed8 = new Pad(16);
__unnamed9 = new Pad(16);
__unnamed10 = new Pad(84);

}
public void Read(BinaryReader reader)
{
  _easyEnemyDamage.Read(reader);
  _normalEnemyDamage.Read(reader);
  _hardEnemyDamage.Read(reader);
  _imposs_PtEnemyDamage.Read(reader);
  _easyEnemyVitality.Read(reader);
  _normalEnemyVitality.Read(reader);
  _hardEnemyVitality.Read(reader);
  _imposs_PtEnemyVitality.Read(reader);
  _easyEnemyShield.Read(reader);
  _normalEnemyShield.Read(reader);
  _hardEnemyShield.Read(reader);
  _imposs_PtEnemyShield.Read(reader);
  _easyEnemyRecharge.Read(reader);
  _normalEnemyRecharge.Read(reader);
  _hardEnemyRecharge.Read(reader);
  _imposs_PtEnemyRecharge.Read(reader);
  _easyFriendDamage.Read(reader);
  _normalFriendDamage.Read(reader);
  _hardFriendDamage.Read(reader);
  _imposs_PtFriendDamage.Read(reader);
  _easyFriendVitality.Read(reader);
  _normalFriendVitality.Read(reader);
  _hardFriendVitality.Read(reader);
  _imposs_PtFriendVitality.Read(reader);
  _easyFriendShield.Read(reader);
  _normalFriendShield.Read(reader);
  _hardFriendShield.Read(reader);
  _imposs_PtFriendShield.Read(reader);
  _easyFriendRecharge.Read(reader);
  _normalFriendRecharge.Read(reader);
  _hardFriendRecharge.Read(reader);
  _imposs_PtFriendRecharge.Read(reader);
  _easyInfectionForms.Read(reader);
  _normalInfectionForms.Read(reader);
  _hardInfectionForms.Read(reader);
  _imposs_PtInfectionForms.Read(reader);
  __unnamed.Read(reader);
  _easyRateOfFire.Read(reader);
  _normalRateOfFire.Read(reader);
  _hardRateOfFire.Read(reader);
  _imposs_PtRateOfFire.Read(reader);
  _easyProjectileError.Read(reader);
  _normalProjectileError.Read(reader);
  _hardProjectileError.Read(reader);
  _imposs_PtProjectileError.Read(reader);
  _easyBurstError.Read(reader);
  _normalBurstError.Read(reader);
  _hardBurstError.Read(reader);
  _imposs_PtBurstError.Read(reader);
  _easyNewTargetDelay.Read(reader);
  _normalNewTargetDelay.Read(reader);
  _hardNewTargetDelay.Read(reader);
  _imposs_PtNewTargetDelay.Read(reader);
  _easyBurstSeparation.Read(reader);
  _normalBurstSeparation.Read(reader);
  _hardBurstSeparation.Read(reader);
  _imposs_PtBurstSeparation.Read(reader);
  _easyTargetTracking.Read(reader);
  _normalTargetTracking.Read(reader);
  _hardTargetTracking.Read(reader);
  _imposs_PtTargetTracking.Read(reader);
  _easyTargetLeading.Read(reader);
  _normalTargetLeading.Read(reader);
  _hardTargetLeading.Read(reader);
  _imposs_PtTargetLeading.Read(reader);
  _easyOverchargeChance.Read(reader);
  _normalOverchargeChance.Read(reader);
  _hardOverchargeChance.Read(reader);
  _imposs_PtOverchargeChance.Read(reader);
  _easySpecialFireDelay.Read(reader);
  _normalSpecialFireDelay.Read(reader);
  _hardSpecialFireDelay.Read(reader);
  _imposs_PtSpecialFireDelay.Read(reader);
  _easyGuidanceVsPlayer.Read(reader);
  _normalGuidanceVsPlayer.Read(reader);
  _hardGuidanceVsPlayer.Read(reader);
  _imposs_PtGuidanceVsPlayer.Read(reader);
  _easyMeleeDelayBase.Read(reader);
  _normalMeleeDelayBase.Read(reader);
  _hardMeleeDelayBase.Read(reader);
  _imposs_PtMeleeDelayBase.Read(reader);
  _easyMeleeDelayScale.Read(reader);
  _normalMeleeDelayScale.Read(reader);
  _hardMeleeDelayScale.Read(reader);
  _imposs_PtMeleeDelayScale.Read(reader);
  __unnamed2.Read(reader);
  _easyGrenadeChanceScale.Read(reader);
  _normalGrenadeChanceScale.Read(reader);
  _hardGrenadeChanceScale.Read(reader);
  _imposs_PtGrenadeChanceScale.Read(reader);
  _easyGrenadeTimerScale.Read(reader);
  _normalGrenadeTimerScale.Read(reader);
  _hardGrenadeTimerScale.Read(reader);
  _imposs_PtGrenadeTimerScale.Read(reader);
  __unnamed3.Read(reader);
  __unnamed4.Read(reader);
  __unnamed5.Read(reader);
  _easyMajorUpgradeNormal.Read(reader);
  _normalMajorUpgradeNormal.Read(reader);
  _hardMajorUpgradeNormal.Read(reader);
  _imposs_PtMajorUpgradeNormal.Read(reader);
  _easyMajorUpgradeFew.Read(reader);
  _normalMajorUpgradeFew.Read(reader);
  _hardMajorUpgradeFew.Read(reader);
  _imposs_PtMajorUpgradeFew.Read(reader);
  _easyMajorUpgradeMany.Read(reader);
  _normalMajorUpgradeMany.Read(reader);
  _hardMajorUpgradeMany.Read(reader);
  _imposs_PtMajorUpgradeMany.Read(reader);
  __unnamed6.Read(reader);
  __unnamed7.Read(reader);
  __unnamed8.Read(reader);
  __unnamed9.Read(reader);
  __unnamed10.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _easyEnemyDamage.Write(writer);
    _normalEnemyDamage.Write(writer);
    _hardEnemyDamage.Write(writer);
    _imposs_PtEnemyDamage.Write(writer);
    _easyEnemyVitality.Write(writer);
    _normalEnemyVitality.Write(writer);
    _hardEnemyVitality.Write(writer);
    _imposs_PtEnemyVitality.Write(writer);
    _easyEnemyShield.Write(writer);
    _normalEnemyShield.Write(writer);
    _hardEnemyShield.Write(writer);
    _imposs_PtEnemyShield.Write(writer);
    _easyEnemyRecharge.Write(writer);
    _normalEnemyRecharge.Write(writer);
    _hardEnemyRecharge.Write(writer);
    _imposs_PtEnemyRecharge.Write(writer);
    _easyFriendDamage.Write(writer);
    _normalFriendDamage.Write(writer);
    _hardFriendDamage.Write(writer);
    _imposs_PtFriendDamage.Write(writer);
    _easyFriendVitality.Write(writer);
    _normalFriendVitality.Write(writer);
    _hardFriendVitality.Write(writer);
    _imposs_PtFriendVitality.Write(writer);
    _easyFriendShield.Write(writer);
    _normalFriendShield.Write(writer);
    _hardFriendShield.Write(writer);
    _imposs_PtFriendShield.Write(writer);
    _easyFriendRecharge.Write(writer);
    _normalFriendRecharge.Write(writer);
    _hardFriendRecharge.Write(writer);
    _imposs_PtFriendRecharge.Write(writer);
    _easyInfectionForms.Write(writer);
    _normalInfectionForms.Write(writer);
    _hardInfectionForms.Write(writer);
    _imposs_PtInfectionForms.Write(writer);
    __unnamed.Write(writer);
    _easyRateOfFire.Write(writer);
    _normalRateOfFire.Write(writer);
    _hardRateOfFire.Write(writer);
    _imposs_PtRateOfFire.Write(writer);
    _easyProjectileError.Write(writer);
    _normalProjectileError.Write(writer);
    _hardProjectileError.Write(writer);
    _imposs_PtProjectileError.Write(writer);
    _easyBurstError.Write(writer);
    _normalBurstError.Write(writer);
    _hardBurstError.Write(writer);
    _imposs_PtBurstError.Write(writer);
    _easyNewTargetDelay.Write(writer);
    _normalNewTargetDelay.Write(writer);
    _hardNewTargetDelay.Write(writer);
    _imposs_PtNewTargetDelay.Write(writer);
    _easyBurstSeparation.Write(writer);
    _normalBurstSeparation.Write(writer);
    _hardBurstSeparation.Write(writer);
    _imposs_PtBurstSeparation.Write(writer);
    _easyTargetTracking.Write(writer);
    _normalTargetTracking.Write(writer);
    _hardTargetTracking.Write(writer);
    _imposs_PtTargetTracking.Write(writer);
    _easyTargetLeading.Write(writer);
    _normalTargetLeading.Write(writer);
    _hardTargetLeading.Write(writer);
    _imposs_PtTargetLeading.Write(writer);
    _easyOverchargeChance.Write(writer);
    _normalOverchargeChance.Write(writer);
    _hardOverchargeChance.Write(writer);
    _imposs_PtOverchargeChance.Write(writer);
    _easySpecialFireDelay.Write(writer);
    _normalSpecialFireDelay.Write(writer);
    _hardSpecialFireDelay.Write(writer);
    _imposs_PtSpecialFireDelay.Write(writer);
    _easyGuidanceVsPlayer.Write(writer);
    _normalGuidanceVsPlayer.Write(writer);
    _hardGuidanceVsPlayer.Write(writer);
    _imposs_PtGuidanceVsPlayer.Write(writer);
    _easyMeleeDelayBase.Write(writer);
    _normalMeleeDelayBase.Write(writer);
    _hardMeleeDelayBase.Write(writer);
    _imposs_PtMeleeDelayBase.Write(writer);
    _easyMeleeDelayScale.Write(writer);
    _normalMeleeDelayScale.Write(writer);
    _hardMeleeDelayScale.Write(writer);
    _imposs_PtMeleeDelayScale.Write(writer);
    __unnamed2.Write(writer);
    _easyGrenadeChanceScale.Write(writer);
    _normalGrenadeChanceScale.Write(writer);
    _hardGrenadeChanceScale.Write(writer);
    _imposs_PtGrenadeChanceScale.Write(writer);
    _easyGrenadeTimerScale.Write(writer);
    _normalGrenadeTimerScale.Write(writer);
    _hardGrenadeTimerScale.Write(writer);
    _imposs_PtGrenadeTimerScale.Write(writer);
    __unnamed3.Write(writer);
    __unnamed4.Write(writer);
    __unnamed5.Write(writer);
    _easyMajorUpgradeNormal.Write(writer);
    _normalMajorUpgradeNormal.Write(writer);
    _hardMajorUpgradeNormal.Write(writer);
    _imposs_PtMajorUpgradeNormal.Write(writer);
    _easyMajorUpgradeFew.Write(writer);
    _normalMajorUpgradeFew.Write(writer);
    _hardMajorUpgradeFew.Write(writer);
    _imposs_PtMajorUpgradeFew.Write(writer);
    _easyMajorUpgradeMany.Write(writer);
    _normalMajorUpgradeMany.Write(writer);
    _hardMajorUpgradeMany.Write(writer);
    _imposs_PtMajorUpgradeMany.Write(writer);
    __unnamed6.Write(writer);
    __unnamed7.Write(writer);
    __unnamed8.Write(writer);
    __unnamed9.Write(writer);
    __unnamed10.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class GrenadesBlock : IBlock
{
private ShortInteger _maximumCount = new ShortInteger();
private ShortInteger _mpSpawnDefault = new ShortInteger();
private TagReference _throwingEffect = new TagReference();
private TagReference _hudInterface = new TagReference();
private TagReference _equipment = new TagReference();
private TagReference _projectile = new TagReference();
public ShortInteger MaximumCount
{
  get { return _maximumCount; }
  set { _maximumCount = value; }
}
public ShortInteger MpSpawnDefault
{
  get { return _mpSpawnDefault; }
  set { _mpSpawnDefault = value; }
}
public TagReference ThrowingEffect
{
  get { return _throwingEffect; }
  set { _throwingEffect = value; }
}
public TagReference HudInterface
{
  get { return _hudInterface; }
  set { _hudInterface = value; }
}
public TagReference Equipment
{
  get { return _equipment; }
  set { _equipment = value; }
}
public TagReference Projectile
{
  get { return _projectile; }
  set { _projectile = value; }
}
public GrenadesBlock()
{

}
public void Read(BinaryReader reader)
{
  _maximumCount.Read(reader);
  _mpSpawnDefault.Read(reader);
  _throwingEffect.Read(reader);
  _hudInterface.Read(reader);
  _equipment.Read(reader);
  _projectile.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_throwingEffect.ReadString(reader);
_hudInterface.ReadString(reader);
_equipment.ReadString(reader);
_projectile.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _maximumCount.Write(writer);
    _mpSpawnDefault.Write(writer);
    _throwingEffect.Write(writer);
    _hudInterface.Write(writer);
    _equipment.Write(writer);
    _projectile.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_throwingEffect.WriteString(writer);
_hudInterface.WriteString(writer);
_equipment.WriteString(writer);
_projectile.WriteString(writer);
}
}
public class RasterizerDataBlock : IBlock
{
private TagReference _distanceAttenuation = new TagReference();
private TagReference _vectorNormalization = new TagReference();
private TagReference _atmosphericFogDensity = new TagReference();
private TagReference _planarFogDensity = new TagReference();
private TagReference _linearCornerFade = new TagReference();
private TagReference _activeCamouflageDistortion = new TagReference();
private TagReference _glow = new TagReference();
private Pad  __unnamed;	
private TagReference _default2D = new TagReference();
private TagReference _default3D = new TagReference();
private TagReference _defaultCubeMap = new TagReference();
private TagReference _test0 = new TagReference();
private TagReference _test1 = new TagReference();
private TagReference _test2 = new TagReference();
private TagReference _test3 = new TagReference();
private TagReference _videoScanlineMap = new TagReference();
private TagReference _videoNoiseMap = new TagReference();
private Pad  __unnamed2;	
private Flags  _flags;	
private Pad  __unnamed3;	
private Real _refractionAmount = new Real();
private Real _distanceFalloff = new Real();
private RealRGBColor _tintColor = new RealRGBColor();
private Real _hype = new Real();
private Real _hype2 = new Real();
private RealRGBColor _hype3 = new RealRGBColor();
private TagReference _distanceAttenuation2D = new TagReference();
public TagReference DistanceAttenuation
{
  get { return _distanceAttenuation; }
  set { _distanceAttenuation = value; }
}
public TagReference VectorNormalization
{
  get { return _vectorNormalization; }
  set { _vectorNormalization = value; }
}
public TagReference AtmosphericFogDensity
{
  get { return _atmosphericFogDensity; }
  set { _atmosphericFogDensity = value; }
}
public TagReference PlanarFogDensity
{
  get { return _planarFogDensity; }
  set { _planarFogDensity = value; }
}
public TagReference LinearCornerFade
{
  get { return _linearCornerFade; }
  set { _linearCornerFade = value; }
}
public TagReference ActiveCamouflageDistortion
{
  get { return _activeCamouflageDistortion; }
  set { _activeCamouflageDistortion = value; }
}
public TagReference Glow
{
  get { return _glow; }
  set { _glow = value; }
}
public TagReference Default2D
{
  get { return _default2D; }
  set { _default2D = value; }
}
public TagReference Default3D
{
  get { return _default3D; }
  set { _default3D = value; }
}
public TagReference DefaultCubeMap
{
  get { return _defaultCubeMap; }
  set { _defaultCubeMap = value; }
}
public TagReference Test0
{
  get { return _test0; }
  set { _test0 = value; }
}
public TagReference Test1
{
  get { return _test1; }
  set { _test1 = value; }
}
public TagReference Test2
{
  get { return _test2; }
  set { _test2 = value; }
}
public TagReference Test3
{
  get { return _test3; }
  set { _test3 = value; }
}
public TagReference VideoScanlineMap
{
  get { return _videoScanlineMap; }
  set { _videoScanlineMap = value; }
}
public TagReference VideoNoiseMap
{
  get { return _videoNoiseMap; }
  set { _videoNoiseMap = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Real RefractionAmount
{
  get { return _refractionAmount; }
  set { _refractionAmount = value; }
}
public Real DistanceFalloff
{
  get { return _distanceFalloff; }
  set { _distanceFalloff = value; }
}
public RealRGBColor TintColor
{
  get { return _tintColor; }
  set { _tintColor = value; }
}
public Real Hype
{
  get { return _hype; }
  set { _hype = value; }
}
public Real Hype2
{
  get { return _hype2; }
  set { _hype2 = value; }
}
public RealRGBColor Hype3
{
  get { return _hype3; }
  set { _hype3 = value; }
}
public TagReference DistanceAttenuation2D
{
  get { return _distanceAttenuation2D; }
  set { _distanceAttenuation2D = value; }
}
public RasterizerDataBlock()
{
__unnamed = new Pad(60);
__unnamed2 = new Pad(52);
_flags = new Flags(2);
__unnamed3 = new Pad(2);

}
public void Read(BinaryReader reader)
{
  _distanceAttenuation.Read(reader);
  _vectorNormalization.Read(reader);
  _atmosphericFogDensity.Read(reader);
  _planarFogDensity.Read(reader);
  _linearCornerFade.Read(reader);
  _activeCamouflageDistortion.Read(reader);
  _glow.Read(reader);
  __unnamed.Read(reader);
  _default2D.Read(reader);
  _default3D.Read(reader);
  _defaultCubeMap.Read(reader);
  _test0.Read(reader);
  _test1.Read(reader);
  _test2.Read(reader);
  _test3.Read(reader);
  _videoScanlineMap.Read(reader);
  _videoNoiseMap.Read(reader);
  __unnamed2.Read(reader);
  _flags.Read(reader);
  __unnamed3.Read(reader);
  _refractionAmount.Read(reader);
  _distanceFalloff.Read(reader);
  _tintColor.Read(reader);
  _hype.Read(reader);
  _hype2.Read(reader);
  _hype3.Read(reader);
  _distanceAttenuation2D.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_distanceAttenuation.ReadString(reader);
_vectorNormalization.ReadString(reader);
_atmosphericFogDensity.ReadString(reader);
_planarFogDensity.ReadString(reader);
_linearCornerFade.ReadString(reader);
_activeCamouflageDistortion.ReadString(reader);
_glow.ReadString(reader);
_default2D.ReadString(reader);
_default3D.ReadString(reader);
_defaultCubeMap.ReadString(reader);
_test0.ReadString(reader);
_test1.ReadString(reader);
_test2.ReadString(reader);
_test3.ReadString(reader);
_videoScanlineMap.ReadString(reader);
_videoNoiseMap.ReadString(reader);
_distanceAttenuation2D.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _distanceAttenuation.Write(writer);
    _vectorNormalization.Write(writer);
    _atmosphericFogDensity.Write(writer);
    _planarFogDensity.Write(writer);
    _linearCornerFade.Write(writer);
    _activeCamouflageDistortion.Write(writer);
    _glow.Write(writer);
    __unnamed.Write(writer);
    _default2D.Write(writer);
    _default3D.Write(writer);
    _defaultCubeMap.Write(writer);
    _test0.Write(writer);
    _test1.Write(writer);
    _test2.Write(writer);
    _test3.Write(writer);
    _videoScanlineMap.Write(writer);
    _videoNoiseMap.Write(writer);
    __unnamed2.Write(writer);
    _flags.Write(writer);
    __unnamed3.Write(writer);
    _refractionAmount.Write(writer);
    _distanceFalloff.Write(writer);
    _tintColor.Write(writer);
    _hype.Write(writer);
    _hype2.Write(writer);
    _hype3.Write(writer);
    _distanceAttenuation2D.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_distanceAttenuation.WriteString(writer);
_vectorNormalization.WriteString(writer);
_atmosphericFogDensity.WriteString(writer);
_planarFogDensity.WriteString(writer);
_linearCornerFade.WriteString(writer);
_activeCamouflageDistortion.WriteString(writer);
_glow.WriteString(writer);
_default2D.WriteString(writer);
_default3D.WriteString(writer);
_defaultCubeMap.WriteString(writer);
_test0.WriteString(writer);
_test1.WriteString(writer);
_test2.WriteString(writer);
_test3.WriteString(writer);
_videoScanlineMap.WriteString(writer);
_videoNoiseMap.WriteString(writer);
_distanceAttenuation2D.WriteString(writer);
}
}
public class InterfaceTagReferencesBlock : IBlock
{
private TagReference _fontSystem = new TagReference();
private TagReference _fontTerminal = new TagReference();
private TagReference _screenColorTable = new TagReference();
private TagReference _hudColorTable = new TagReference();
private TagReference _editorColorTable = new TagReference();
private TagReference _dialogColorTable = new TagReference();
private TagReference _hudGlobals = new TagReference();
private TagReference _motionSensorSweepBitmap = new TagReference();
private TagReference _motionSensorSweepBitmapMask = new TagReference();
private TagReference _multiplayerHudBitmap = new TagReference();
private TagReference _localization = new TagReference();
private TagReference _hudDigitsDefinition = new TagReference();
private TagReference _motionSensorBlipBitmap = new TagReference();
private TagReference _interfaceGooMap1 = new TagReference();
private TagReference _interfaceGooMap2 = new TagReference();
private TagReference _interfaceGooMap3 = new TagReference();
private Pad  __unnamed;	
public TagReference FontSystem
{
  get { return _fontSystem; }
  set { _fontSystem = value; }
}
public TagReference FontTerminal
{
  get { return _fontTerminal; }
  set { _fontTerminal = value; }
}
public TagReference ScreenColorTable
{
  get { return _screenColorTable; }
  set { _screenColorTable = value; }
}
public TagReference HudColorTable
{
  get { return _hudColorTable; }
  set { _hudColorTable = value; }
}
public TagReference EditorColorTable
{
  get { return _editorColorTable; }
  set { _editorColorTable = value; }
}
public TagReference DialogColorTable
{
  get { return _dialogColorTable; }
  set { _dialogColorTable = value; }
}
public TagReference HudGlobals
{
  get { return _hudGlobals; }
  set { _hudGlobals = value; }
}
public TagReference MotionSensorSweepBitmap
{
  get { return _motionSensorSweepBitmap; }
  set { _motionSensorSweepBitmap = value; }
}
public TagReference MotionSensorSweepBitmapMask
{
  get { return _motionSensorSweepBitmapMask; }
  set { _motionSensorSweepBitmapMask = value; }
}
public TagReference MultiplayerHudBitmap
{
  get { return _multiplayerHudBitmap; }
  set { _multiplayerHudBitmap = value; }
}
public TagReference Localization
{
  get { return _localization; }
  set { _localization = value; }
}
public TagReference HudDigitsDefinition
{
  get { return _hudDigitsDefinition; }
  set { _hudDigitsDefinition = value; }
}
public TagReference MotionSensorBlipBitmap
{
  get { return _motionSensorBlipBitmap; }
  set { _motionSensorBlipBitmap = value; }
}
public TagReference InterfaceGooMap1
{
  get { return _interfaceGooMap1; }
  set { _interfaceGooMap1 = value; }
}
public TagReference InterfaceGooMap2
{
  get { return _interfaceGooMap2; }
  set { _interfaceGooMap2 = value; }
}
public TagReference InterfaceGooMap3
{
  get { return _interfaceGooMap3; }
  set { _interfaceGooMap3 = value; }
}
public InterfaceTagReferencesBlock()
{
__unnamed = new Pad(48);

}
public void Read(BinaryReader reader)
{
  _fontSystem.Read(reader);
  _fontTerminal.Read(reader);
  _screenColorTable.Read(reader);
  _hudColorTable.Read(reader);
  _editorColorTable.Read(reader);
  _dialogColorTable.Read(reader);
  _hudGlobals.Read(reader);
  _motionSensorSweepBitmap.Read(reader);
  _motionSensorSweepBitmapMask.Read(reader);
  _multiplayerHudBitmap.Read(reader);
  _localization.Read(reader);
  _hudDigitsDefinition.Read(reader);
  _motionSensorBlipBitmap.Read(reader);
  _interfaceGooMap1.Read(reader);
  _interfaceGooMap2.Read(reader);
  _interfaceGooMap3.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_fontSystem.ReadString(reader);
_fontTerminal.ReadString(reader);
_screenColorTable.ReadString(reader);
_hudColorTable.ReadString(reader);
_editorColorTable.ReadString(reader);
_dialogColorTable.ReadString(reader);
_hudGlobals.ReadString(reader);
_motionSensorSweepBitmap.ReadString(reader);
_motionSensorSweepBitmapMask.ReadString(reader);
_multiplayerHudBitmap.ReadString(reader);
_localization.ReadString(reader);
_hudDigitsDefinition.ReadString(reader);
_motionSensorBlipBitmap.ReadString(reader);
_interfaceGooMap1.ReadString(reader);
_interfaceGooMap2.ReadString(reader);
_interfaceGooMap3.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _fontSystem.Write(writer);
    _fontTerminal.Write(writer);
    _screenColorTable.Write(writer);
    _hudColorTable.Write(writer);
    _editorColorTable.Write(writer);
    _dialogColorTable.Write(writer);
    _hudGlobals.Write(writer);
    _motionSensorSweepBitmap.Write(writer);
    _motionSensorSweepBitmapMask.Write(writer);
    _multiplayerHudBitmap.Write(writer);
    _localization.Write(writer);
    _hudDigitsDefinition.Write(writer);
    _motionSensorBlipBitmap.Write(writer);
    _interfaceGooMap1.Write(writer);
    _interfaceGooMap2.Write(writer);
    _interfaceGooMap3.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_fontSystem.WriteString(writer);
_fontTerminal.WriteString(writer);
_screenColorTable.WriteString(writer);
_hudColorTable.WriteString(writer);
_editorColorTable.WriteString(writer);
_dialogColorTable.WriteString(writer);
_hudGlobals.WriteString(writer);
_motionSensorSweepBitmap.WriteString(writer);
_motionSensorSweepBitmapMask.WriteString(writer);
_multiplayerHudBitmap.WriteString(writer);
_localization.WriteString(writer);
_hudDigitsDefinition.WriteString(writer);
_motionSensorBlipBitmap.WriteString(writer);
_interfaceGooMap1.WriteString(writer);
_interfaceGooMap2.WriteString(writer);
_interfaceGooMap3.WriteString(writer);
}
}
public class CheatWeaponsBlock : IBlock
{
private TagReference _weapon = new TagReference();
public TagReference Weapon
{
  get { return _weapon; }
  set { _weapon = value; }
}
public CheatWeaponsBlock()
{

}
public void Read(BinaryReader reader)
{
  _weapon.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_weapon.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _weapon.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_weapon.WriteString(writer);
}
}
public class CheatPowerupsBlock : IBlock
{
private TagReference _powerup = new TagReference();
public TagReference Powerup
{
  get { return _powerup; }
  set { _powerup = value; }
}
public CheatPowerupsBlock()
{

}
public void Read(BinaryReader reader)
{
  _powerup.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_powerup.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _powerup.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_powerup.WriteString(writer);
}
}
public class MultiplayerInformationBlock : IBlock
{
private TagReference _flag = new TagReference();
private TagReference _unit = new TagReference();
private Block _vehicles = new Block();
private TagReference _hillShader = new TagReference();
private TagReference _flagShader = new TagReference();
private TagReference _ball = new TagReference();
private Block _sounds = new Block();
private Pad  __unnamed;	
public class VehiclesBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public VehiclesBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(VehiclesBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new VehiclesBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public VehiclesBlock this[int index]
  {
    get { return (InnerList[index] as VehiclesBlock); }
  }
}
private VehiclesBlockCollection _vehiclesCollection;
public VehiclesBlockCollection Vehicles
{
  get { return _vehiclesCollection; }
}
public class SoundsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public SoundsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(SoundsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new SoundsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public SoundsBlock this[int index]
  {
    get { return (InnerList[index] as SoundsBlock); }
  }
}
private SoundsBlockCollection _soundsCollection;
public SoundsBlockCollection Sounds
{
  get { return _soundsCollection; }
}
public TagReference Flag
{
  get { return _flag; }
  set { _flag = value; }
}
public TagReference Unit
{
  get { return _unit; }
  set { _unit = value; }
}
public TagReference HillShader
{
  get { return _hillShader; }
  set { _hillShader = value; }
}
public TagReference FlagShader
{
  get { return _flagShader; }
  set { _flagShader = value; }
}
public TagReference Ball
{
  get { return _ball; }
  set { _ball = value; }
}
public MultiplayerInformationBlock()
{
__unnamed = new Pad(56);
_vehiclesCollection = new VehiclesBlockCollection(_vehicles);
_soundsCollection = new SoundsBlockCollection(_sounds);

}
public void Read(BinaryReader reader)
{
  _flag.Read(reader);
  _unit.Read(reader);
  _vehicles.Read(reader);
  _hillShader.Read(reader);
  _flagShader.Read(reader);
  _ball.Read(reader);
  _sounds.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_flag.ReadString(reader);
_unit.ReadString(reader);
for (int x=0; x<_vehicles.Count; x++)
{
  Vehicles.AddNew();
  Vehicles[x].Read(reader);
}
for (int x=0; x<_vehicles.Count; x++)
  Vehicles[x].ReadChildData(reader);
_hillShader.ReadString(reader);
_flagShader.ReadString(reader);
_ball.ReadString(reader);
for (int x=0; x<_sounds.Count; x++)
{
  Sounds.AddNew();
  Sounds[x].Read(reader);
}
for (int x=0; x<_sounds.Count; x++)
  Sounds[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _flag.Write(writer);
    _unit.Write(writer);
    _vehicles.Write(writer);
    _hillShader.Write(writer);
    _flagShader.Write(writer);
    _ball.Write(writer);
    _sounds.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_flag.WriteString(writer);
_unit.WriteString(writer);
_vehicles.UpdateReflexiveOffset(writer);
for (int x=0; x<_vehicles.Count; x++)
{
  Vehicles[x].Write(writer);
}
for (int x=0; x<_vehicles.Count; x++)
  Vehicles[x].WriteChildData(writer);
_hillShader.WriteString(writer);
_flagShader.WriteString(writer);
_ball.WriteString(writer);
_sounds.UpdateReflexiveOffset(writer);
for (int x=0; x<_sounds.Count; x++)
{
  Sounds[x].Write(writer);
}
for (int x=0; x<_sounds.Count; x++)
  Sounds[x].WriteChildData(writer);
}
}
public class VehiclesBlock : IBlock
{
private TagReference _vehicle = new TagReference();
public TagReference Vehicle
{
  get { return _vehicle; }
  set { _vehicle = value; }
}
public VehiclesBlock()
{

}
public void Read(BinaryReader reader)
{
  _vehicle.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_vehicle.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _vehicle.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_vehicle.WriteString(writer);
}
}
public class SoundsBlock : IBlock
{
private TagReference _sound = new TagReference();
public TagReference Sound
{
  get { return _sound; }
  set { _sound = value; }
}
public SoundsBlock()
{

}
public void Read(BinaryReader reader)
{
  _sound.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_sound.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _sound.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_sound.WriteString(writer);
}
}
public class PlayerInformationBlock : IBlock
{
private TagReference _unit = new TagReference();
private Pad  __unnamed;	
private Real _walkingSpeed = new Real();
private Real _doubleSpeedMultiplier = new Real();
private Real _runForward = new Real();
private Real _runBackward = new Real();
private Real _runSideways = new Real();
private Real _runAcceleration = new Real();
private Real _sneakForward = new Real();
private Real _sneakBackward = new Real();
private Real _sneakSideways = new Real();
private Real _sneakAcceleration = new Real();
private Real _airborneAcceleration = new Real();
private Real _speedMultiplier = new Real();
private Pad  __unnamed2;	
private RealPoint3D _grenadeOrigin = new RealPoint3D();
private Pad  __unnamed3;	
private Real _stunMovementPenalty = new Real();
private Real _stunTurningPenalty = new Real();
private Real _stunJumpingPenalty = new Real();
private Real _minimumStunTime = new Real();
private Real _maximumStunTime = new Real();
private Pad  __unnamed4;	
private RealBounds _firstPersonIdleTime = new RealBounds();
private RealFraction _firstPersonSkipFraction = new RealFraction();
private Pad  __unnamed5;	
private TagReference _coopRespawnEffect = new TagReference();
private Pad  __unnamed6;	
public TagReference Unit
{
  get { return _unit; }
  set { _unit = value; }
}
public Real WalkingSpeed
{
  get { return _walkingSpeed; }
  set { _walkingSpeed = value; }
}
public Real doubleSpeedMultiplier
{
  get { return _doubleSpeedMultiplier; }
  set { _doubleSpeedMultiplier = value; }
}
public Real RunForward
{
  get { return _runForward; }
  set { _runForward = value; }
}
public Real RunBackward
{
  get { return _runBackward; }
  set { _runBackward = value; }
}
public Real RunSideways
{
  get { return _runSideways; }
  set { _runSideways = value; }
}
public Real RunAcceleration
{
  get { return _runAcceleration; }
  set { _runAcceleration = value; }
}
public Real SneakForward
{
  get { return _sneakForward; }
  set { _sneakForward = value; }
}
public Real SneakBackward
{
  get { return _sneakBackward; }
  set { _sneakBackward = value; }
}
public Real SneakSideways
{
  get { return _sneakSideways; }
  set { _sneakSideways = value; }
}
public Real SneakAcceleration
{
  get { return _sneakAcceleration; }
  set { _sneakAcceleration = value; }
}
public Real AirborneAcceleration
{
  get { return _airborneAcceleration; }
  set { _airborneAcceleration = value; }
}
public Real SpeedMultiplier
{
  get { return _speedMultiplier; }
  set { _speedMultiplier = value; }
}
public RealPoint3D GrenadeOrigin
{
  get { return _grenadeOrigin; }
  set { _grenadeOrigin = value; }
}
public Real StunMovementPenalty
{
  get { return _stunMovementPenalty; }
  set { _stunMovementPenalty = value; }
}
public Real StunTurningPenalty
{
  get { return _stunTurningPenalty; }
  set { _stunTurningPenalty = value; }
}
public Real StunJumpingPenalty
{
  get { return _stunJumpingPenalty; }
  set { _stunJumpingPenalty = value; }
}
public Real MinimumStunTime
{
  get { return _minimumStunTime; }
  set { _minimumStunTime = value; }
}
public Real MaximumStunTime
{
  get { return _maximumStunTime; }
  set { _maximumStunTime = value; }
}
public RealBounds FirstPersonIdleTime
{
  get { return _firstPersonIdleTime; }
  set { _firstPersonIdleTime = value; }
}
public RealFraction FirstPersonSkipFraction
{
  get { return _firstPersonSkipFraction; }
  set { _firstPersonSkipFraction = value; }
}
public TagReference CoopRespawnEffect
{
  get { return _coopRespawnEffect; }
  set { _coopRespawnEffect = value; }
}
public PlayerInformationBlock()
{
__unnamed = new Pad(28);
__unnamed2 = new Pad(12);
__unnamed3 = new Pad(12);
__unnamed4 = new Pad(8);
__unnamed5 = new Pad(16);
__unnamed6 = new Pad(44);

}
public void Read(BinaryReader reader)
{
  _unit.Read(reader);
  __unnamed.Read(reader);
  _walkingSpeed.Read(reader);
  _doubleSpeedMultiplier.Read(reader);
  _runForward.Read(reader);
  _runBackward.Read(reader);
  _runSideways.Read(reader);
  _runAcceleration.Read(reader);
  _sneakForward.Read(reader);
  _sneakBackward.Read(reader);
  _sneakSideways.Read(reader);
  _sneakAcceleration.Read(reader);
  _airborneAcceleration.Read(reader);
  _speedMultiplier.Read(reader);
  __unnamed2.Read(reader);
  _grenadeOrigin.Read(reader);
  __unnamed3.Read(reader);
  _stunMovementPenalty.Read(reader);
  _stunTurningPenalty.Read(reader);
  _stunJumpingPenalty.Read(reader);
  _minimumStunTime.Read(reader);
  _maximumStunTime.Read(reader);
  __unnamed4.Read(reader);
  _firstPersonIdleTime.Read(reader);
  _firstPersonSkipFraction.Read(reader);
  __unnamed5.Read(reader);
  _coopRespawnEffect.Read(reader);
  __unnamed6.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_unit.ReadString(reader);
_coopRespawnEffect.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _unit.Write(writer);
    __unnamed.Write(writer);
    _walkingSpeed.Write(writer);
    _doubleSpeedMultiplier.Write(writer);
    _runForward.Write(writer);
    _runBackward.Write(writer);
    _runSideways.Write(writer);
    _runAcceleration.Write(writer);
    _sneakForward.Write(writer);
    _sneakBackward.Write(writer);
    _sneakSideways.Write(writer);
    _sneakAcceleration.Write(writer);
    _airborneAcceleration.Write(writer);
    _speedMultiplier.Write(writer);
    __unnamed2.Write(writer);
    _grenadeOrigin.Write(writer);
    __unnamed3.Write(writer);
    _stunMovementPenalty.Write(writer);
    _stunTurningPenalty.Write(writer);
    _stunJumpingPenalty.Write(writer);
    _minimumStunTime.Write(writer);
    _maximumStunTime.Write(writer);
    __unnamed4.Write(writer);
    _firstPersonIdleTime.Write(writer);
    _firstPersonSkipFraction.Write(writer);
    __unnamed5.Write(writer);
    _coopRespawnEffect.Write(writer);
    __unnamed6.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_unit.WriteString(writer);
_coopRespawnEffect.WriteString(writer);
}
}
public class FirstPersonInterfaceBlock : IBlock
{
private TagReference _firstPersonHands = new TagReference();
private TagReference _baseBitmap = new TagReference();
private TagReference _shieldMeter = new TagReference();
private Point2D _shieldMeterOrigin = new Point2D();
private TagReference _bodyMeter = new TagReference();
private Point2D _bodyMeterOrigin = new Point2D();
private TagReference _nigh = new TagReference();
private TagReference _nigh2 = new TagReference();
private Pad  __unnamed;	
public TagReference FirstPersonHands
{
  get { return _firstPersonHands; }
  set { _firstPersonHands = value; }
}
public TagReference BaseBitmap
{
  get { return _baseBitmap; }
  set { _baseBitmap = value; }
}
public TagReference ShieldMeter
{
  get { return _shieldMeter; }
  set { _shieldMeter = value; }
}
public Point2D ShieldMeterOrigin
{
  get { return _shieldMeterOrigin; }
  set { _shieldMeterOrigin = value; }
}
public TagReference BodyMeter
{
  get { return _bodyMeter; }
  set { _bodyMeter = value; }
}
public Point2D BodyMeterOrigin
{
  get { return _bodyMeterOrigin; }
  set { _bodyMeterOrigin = value; }
}
public TagReference Nigh
{
  get { return _nigh; }
  set { _nigh = value; }
}
public TagReference Nigh2
{
  get { return _nigh2; }
  set { _nigh2 = value; }
}
public FirstPersonInterfaceBlock()
{
__unnamed = new Pad(88);

}
public void Read(BinaryReader reader)
{
  _firstPersonHands.Read(reader);
  _baseBitmap.Read(reader);
  _shieldMeter.Read(reader);
  _shieldMeterOrigin.Read(reader);
  _bodyMeter.Read(reader);
  _bodyMeterOrigin.Read(reader);
  _nigh.Read(reader);
  _nigh2.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_firstPersonHands.ReadString(reader);
_baseBitmap.ReadString(reader);
_shieldMeter.ReadString(reader);
_bodyMeter.ReadString(reader);
_nigh.ReadString(reader);
_nigh2.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _firstPersonHands.Write(writer);
    _baseBitmap.Write(writer);
    _shieldMeter.Write(writer);
    _shieldMeterOrigin.Write(writer);
    _bodyMeter.Write(writer);
    _bodyMeterOrigin.Write(writer);
    _nigh.Write(writer);
    _nigh2.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_firstPersonHands.WriteString(writer);
_baseBitmap.WriteString(writer);
_shieldMeter.WriteString(writer);
_bodyMeter.WriteString(writer);
_nigh.WriteString(writer);
_nigh2.WriteString(writer);
}
}
public class FallingDamageBlock : IBlock
{
private Pad  __unnamed;	
private RealBounds _harmfulFallingDistance = new RealBounds();
private TagReference _fallingDamage = new TagReference();
private Pad  __unnamed2;	
private Real _maximumFallingDistance = new Real();
private TagReference _distanceDamage = new TagReference();
private TagReference _vehicleEnvironemtnCollisionDamageEffect = new TagReference();
private TagReference _vehicleKilledUnitDamageEffect = new TagReference();
private TagReference _vehicleCollisionDamage = new TagReference();
private TagReference _flamingDeathDamage = new TagReference();
private Pad  __unnamed3;	
public RealBounds HarmfulFallingDistance
{
  get { return _harmfulFallingDistance; }
  set { _harmfulFallingDistance = value; }
}
public TagReference FallingDamage
{
  get { return _fallingDamage; }
  set { _fallingDamage = value; }
}
public Real MaximumFallingDistance
{
  get { return _maximumFallingDistance; }
  set { _maximumFallingDistance = value; }
}
public TagReference DistanceDamage
{
  get { return _distanceDamage; }
  set { _distanceDamage = value; }
}
public TagReference VehicleEnvironemtnCollisionDamageEffect
{
  get { return _vehicleEnvironemtnCollisionDamageEffect; }
  set { _vehicleEnvironemtnCollisionDamageEffect = value; }
}
public TagReference VehicleKilledUnitDamageEffect
{
  get { return _vehicleKilledUnitDamageEffect; }
  set { _vehicleKilledUnitDamageEffect = value; }
}
public TagReference VehicleCollisionDamage
{
  get { return _vehicleCollisionDamage; }
  set { _vehicleCollisionDamage = value; }
}
public TagReference FlamingDeathDamage
{
  get { return _flamingDeathDamage; }
  set { _flamingDeathDamage = value; }
}
public FallingDamageBlock()
{
__unnamed = new Pad(8);
__unnamed2 = new Pad(8);
__unnamed3 = new Pad(28);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  _harmfulFallingDistance.Read(reader);
  _fallingDamage.Read(reader);
  __unnamed2.Read(reader);
  _maximumFallingDistance.Read(reader);
  _distanceDamage.Read(reader);
  _vehicleEnvironemtnCollisionDamageEffect.Read(reader);
  _vehicleKilledUnitDamageEffect.Read(reader);
  _vehicleCollisionDamage.Read(reader);
  _flamingDeathDamage.Read(reader);
  __unnamed3.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_fallingDamage.ReadString(reader);
_distanceDamage.ReadString(reader);
_vehicleEnvironemtnCollisionDamageEffect.ReadString(reader);
_vehicleKilledUnitDamageEffect.ReadString(reader);
_vehicleCollisionDamage.ReadString(reader);
_flamingDeathDamage.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    _harmfulFallingDistance.Write(writer);
    _fallingDamage.Write(writer);
    __unnamed2.Write(writer);
    _maximumFallingDistance.Write(writer);
    _distanceDamage.Write(writer);
    _vehicleEnvironemtnCollisionDamageEffect.Write(writer);
    _vehicleKilledUnitDamageEffect.Write(writer);
    _vehicleCollisionDamage.Write(writer);
    _flamingDeathDamage.Write(writer);
    __unnamed3.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_fallingDamage.WriteString(writer);
_distanceDamage.WriteString(writer);
_vehicleEnvironemtnCollisionDamageEffect.WriteString(writer);
_vehicleKilledUnitDamageEffect.WriteString(writer);
_vehicleCollisionDamage.WriteString(writer);
_flamingDeathDamage.WriteString(writer);
}
}
public class MaterialsBlock : IBlock
{
private Pad  __unnamed;	
private Pad  __unnamed2;	
private Real _groundFrictionScale = new Real();
private Real _groundFrictionNormalK1Scale = new Real();
private Real _groundFrictionNormalK0Scale = new Real();
private Real _groundDepthScale = new Real();
private Real _groundDampFractionScale = new Real();
private Pad  __unnamed3;	
private Pad  __unnamed4;	
private Real _maximumVitality = new Real();
private Pad  __unnamed5;	
private Pad  __unnamed6;	
private TagReference _effect = new TagReference();
private TagReference _sound = new TagReference();
private Pad  __unnamed7;	
private Block _particleEffects = new Block();
private Pad  __unnamed8;	
private TagReference _meleeHitSound = new TagReference();
public class BreakableSurfaceParticleEffectBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public BreakableSurfaceParticleEffectBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(BreakableSurfaceParticleEffectBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new BreakableSurfaceParticleEffectBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public BreakableSurfaceParticleEffectBlock this[int index]
  {
    get { return (InnerList[index] as BreakableSurfaceParticleEffectBlock); }
  }
}
private BreakableSurfaceParticleEffectBlockCollection _particleEffectsCollection;
public BreakableSurfaceParticleEffectBlockCollection ParticleEffects
{
  get { return _particleEffectsCollection; }
}
public Real GroundFrictionScale
{
  get { return _groundFrictionScale; }
  set { _groundFrictionScale = value; }
}
public Real GroundFrictionNormalK1Scale
{
  get { return _groundFrictionNormalK1Scale; }
  set { _groundFrictionNormalK1Scale = value; }
}
public Real GroundFrictionNormalK0Scale
{
  get { return _groundFrictionNormalK0Scale; }
  set { _groundFrictionNormalK0Scale = value; }
}
public Real GroundDepthScale
{
  get { return _groundDepthScale; }
  set { _groundDepthScale = value; }
}
public Real GroundDampFractionScale
{
  get { return _groundDampFractionScale; }
  set { _groundDampFractionScale = value; }
}
public Real MaximumVitality
{
  get { return _maximumVitality; }
  set { _maximumVitality = value; }
}
public TagReference Effect
{
  get { return _effect; }
  set { _effect = value; }
}
public TagReference Sound
{
  get { return _sound; }
  set { _sound = value; }
}
public TagReference MeleeHitSound
{
  get { return _meleeHitSound; }
  set { _meleeHitSound = value; }
}
public MaterialsBlock()
{
__unnamed = new Pad(100);
__unnamed2 = new Pad(48);
__unnamed3 = new Pad(76);
__unnamed4 = new Pad(480);
__unnamed5 = new Pad(8);
__unnamed6 = new Pad(4);
__unnamed7 = new Pad(24);
__unnamed8 = new Pad(60);
_particleEffectsCollection = new BreakableSurfaceParticleEffectBlockCollection(_particleEffects);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  _groundFrictionScale.Read(reader);
  _groundFrictionNormalK1Scale.Read(reader);
  _groundFrictionNormalK0Scale.Read(reader);
  _groundDepthScale.Read(reader);
  _groundDampFractionScale.Read(reader);
  __unnamed3.Read(reader);
  __unnamed4.Read(reader);
  _maximumVitality.Read(reader);
  __unnamed5.Read(reader);
  __unnamed6.Read(reader);
  _effect.Read(reader);
  _sound.Read(reader);
  __unnamed7.Read(reader);
  _particleEffects.Read(reader);
  __unnamed8.Read(reader);
  _meleeHitSound.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_effect.ReadString(reader);
_sound.ReadString(reader);
for (int x=0; x<_particleEffects.Count; x++)
{
  ParticleEffects.AddNew();
  ParticleEffects[x].Read(reader);
}
for (int x=0; x<_particleEffects.Count; x++)
  ParticleEffects[x].ReadChildData(reader);
_meleeHitSound.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    _groundFrictionScale.Write(writer);
    _groundFrictionNormalK1Scale.Write(writer);
    _groundFrictionNormalK0Scale.Write(writer);
    _groundDepthScale.Write(writer);
    _groundDampFractionScale.Write(writer);
    __unnamed3.Write(writer);
    __unnamed4.Write(writer);
    _maximumVitality.Write(writer);
    __unnamed5.Write(writer);
    __unnamed6.Write(writer);
    _effect.Write(writer);
    _sound.Write(writer);
    __unnamed7.Write(writer);
    _particleEffects.Write(writer);
    __unnamed8.Write(writer);
    _meleeHitSound.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_effect.WriteString(writer);
_sound.WriteString(writer);
_particleEffects.UpdateReflexiveOffset(writer);
for (int x=0; x<_particleEffects.Count; x++)
{
  ParticleEffects[x].Write(writer);
}
for (int x=0; x<_particleEffects.Count; x++)
  ParticleEffects[x].WriteChildData(writer);
_meleeHitSound.WriteString(writer);
}
}
public class BreakableSurfaceParticleEffectBlock : IBlock
{
private TagReference _particleType = new TagReference();
private Flags  _flags;	
private Real _density = new Real();
private RealBounds _velocityScale = new RealBounds();
private Pad  __unnamed;	
private AngleBounds _angularVelocity = new AngleBounds();
private Pad  __unnamed2;	
private RealBounds _radius = new RealBounds();
private Pad  __unnamed3;	
private RealARGBColor _tintLowerBound = new RealARGBColor();
private RealARGBColor _tintUpperBound = new RealARGBColor();
private Pad  __unnamed4;	
public TagReference ParticleType
{
  get { return _particleType; }
  set { _particleType = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Real Density
{
  get { return _density; }
  set { _density = value; }
}
public RealBounds VelocityScale
{
  get { return _velocityScale; }
  set { _velocityScale = value; }
}
public AngleBounds AngularVelocity
{
  get { return _angularVelocity; }
  set { _angularVelocity = value; }
}
public RealBounds Radius
{
  get { return _radius; }
  set { _radius = value; }
}
public RealARGBColor TintLowerBound
{
  get { return _tintLowerBound; }
  set { _tintLowerBound = value; }
}
public RealARGBColor TintUpperBound
{
  get { return _tintUpperBound; }
  set { _tintUpperBound = value; }
}
public BreakableSurfaceParticleEffectBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(4);
__unnamed2 = new Pad(8);
__unnamed3 = new Pad(8);
__unnamed4 = new Pad(28);

}
public void Read(BinaryReader reader)
{
  _particleType.Read(reader);
  _flags.Read(reader);
  _density.Read(reader);
  _velocityScale.Read(reader);
  __unnamed.Read(reader);
  _angularVelocity.Read(reader);
  __unnamed2.Read(reader);
  _radius.Read(reader);
  __unnamed3.Read(reader);
  _tintLowerBound.Read(reader);
  _tintUpperBound.Read(reader);
  __unnamed4.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_particleType.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _particleType.Write(writer);
    _flags.Write(writer);
    _density.Write(writer);
    _velocityScale.Write(writer);
    __unnamed.Write(writer);
    _angularVelocity.Write(writer);
    __unnamed2.Write(writer);
    _radius.Write(writer);
    __unnamed3.Write(writer);
    _tintLowerBound.Write(writer);
    _tintUpperBound.Write(writer);
    __unnamed4.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_particleType.WriteString(writer);
}
}
public class PlaylistAutogenerateChoiceBlock : IBlock
{
private FixedLengthString _mapName = new FixedLengthString();
private FixedLengthString _gameVariant = new FixedLengthString();
private LongInteger _minimumExperience = new LongInteger();
private LongInteger _maximumExperience = new LongInteger();
private LongInteger _minimumPlayerCount = new LongInteger();
private LongInteger _maximumPlayerCount = new LongInteger();
private LongInteger _rating = new LongInteger();
private Pad  __unnamed;	
public FixedLengthString MapName
{
  get { return _mapName; }
  set { _mapName = value; }
}
public FixedLengthString GameVariant
{
  get { return _gameVariant; }
  set { _gameVariant = value; }
}
public LongInteger MinimumExperience
{
  get { return _minimumExperience; }
  set { _minimumExperience = value; }
}
public LongInteger MaximumExperience
{
  get { return _maximumExperience; }
  set { _maximumExperience = value; }
}
public LongInteger MinimumPlayerCount
{
  get { return _minimumPlayerCount; }
  set { _minimumPlayerCount = value; }
}
public LongInteger MaximumPlayerCount
{
  get { return _maximumPlayerCount; }
  set { _maximumPlayerCount = value; }
}
public LongInteger Rating
{
  get { return _rating; }
  set { _rating = value; }
}
public PlaylistAutogenerateChoiceBlock()
{
__unnamed = new Pad(64);

}
public void Read(BinaryReader reader)
{
  _mapName.Read(reader);
  _gameVariant.Read(reader);
  _minimumExperience.Read(reader);
  _maximumExperience.Read(reader);
  _minimumPlayerCount.Read(reader);
  _maximumPlayerCount.Read(reader);
  _rating.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _mapName.Write(writer);
    _gameVariant.Write(writer);
    _minimumExperience.Write(writer);
    _maximumExperience.Write(writer);
    _minimumPlayerCount.Write(writer);
    _maximumPlayerCount.Write(writer);
    _rating.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
  }
}
