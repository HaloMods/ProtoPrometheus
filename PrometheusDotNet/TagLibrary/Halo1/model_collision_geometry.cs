using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class ModelCollisionGeometry : IBlock
  {
    public ModelCollisionGeometryBlock ModelCollisionGeometryValues = new ModelCollisionGeometryBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'ModelCollisionGeometry'------------------------------------------------------");
      ModelCollisionGeometryValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      ModelCollisionGeometryValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      ModelCollisionGeometryValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      ModelCollisionGeometryValues.WriteChildData(writer);
    }
public class ModelCollisionGeometryBlock : IBlock
{
private Flags  _flags;	
private ShortBlockIndex _indirectDamageMaterial = new ShortBlockIndex();
private Pad  __unnamed;	
private Real _maximumBodyVitality = new Real();
private Real _bodySystemShock = new Real();
private Pad  __unnamed2;	
private Pad  __unnamed3;	
private RealFraction _friendlyDamageResistance = new RealFraction();
private Pad  __unnamed4;	
private Pad  __unnamed5;	
private TagReference _localizedDamageEffect = new TagReference();
private Real _areaDamageEffectThreshold = new Real();
private TagReference _areaDamageEffect = new TagReference();
private Real _bodyDamagedThreshold = new Real();
private TagReference _bodyDamagedEffect = new TagReference();
private TagReference _bodyDepletedEffect = new TagReference();
private Real _bodyDestroyedThreshold = new Real();
private TagReference _bodyDestroyedEffect = new TagReference();
private Real _maximumShieldVitality = new Real();
private Pad  __unnamed6;	
private Enum _shieldMaterialType = new Enum();
private Pad  __unnamed7;	
private Enum _shieldFailureFunction = new Enum();
private Pad  __unnamed8;	
private RealFraction _shieldFailureThreshold = new RealFraction();
private RealFraction _failingShieldLeakFraction = new RealFraction();
private Pad  __unnamed9;	
private Real _minimumStunDamage = new Real();
private Real _stunTime = new Real();
private Real _rechargeTime = new Real();
private Pad  __unnamed10;	
private Pad  __unnamed11;	
private Real _shieldDamagedThreshold = new Real();
private TagReference _shieldDamagedEffect = new TagReference();
private TagReference _shieldDepletedEffect = new TagReference();
private TagReference _shieldRechargingEffect = new TagReference();
private Pad  __unnamed12;	
private Pad  __unnamed13;	
private Block _materials = new Block();
private Block _regions = new Block();
private Block _modifiers = new Block();
private Pad  __unnamed14;	
private RealBounds _x = new RealBounds();
private RealBounds _y = new RealBounds();
private RealBounds _z = new RealBounds();
private Block _pathfindingSpheres = new Block();
private Block _nodes = new Block();
public class DamageMaterialsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public DamageMaterialsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(DamageMaterialsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new DamageMaterialsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public DamageMaterialsBlock this[int index]
  {
    get { return (InnerList[index] as DamageMaterialsBlock); }
  }
}
private DamageMaterialsBlockCollection _materialsCollection;
public DamageMaterialsBlockCollection Materials
{
  get { return _materialsCollection; }
}
public class DamageRegionsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public DamageRegionsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(DamageRegionsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new DamageRegionsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public DamageRegionsBlock this[int index]
  {
    get { return (InnerList[index] as DamageRegionsBlock); }
  }
}
private DamageRegionsBlockCollection _regionsCollection;
public DamageRegionsBlockCollection Regions
{
  get { return _regionsCollection; }
}
public class DamageModifiersBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public DamageModifiersBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(DamageModifiersBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new DamageModifiersBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public DamageModifiersBlock this[int index]
  {
    get { return (InnerList[index] as DamageModifiersBlock); }
  }
}
private DamageModifiersBlockCollection _modifiersCollection;
public DamageModifiersBlockCollection Modifiers
{
  get { return _modifiersCollection; }
}
public class SphereBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public SphereBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(SphereBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new SphereBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public SphereBlock this[int index]
  {
    get { return (InnerList[index] as SphereBlock); }
  }
}
private SphereBlockCollection _pathfindingSpheresCollection;
public SphereBlockCollection PathfindingSpheres
{
  get { return _pathfindingSpheresCollection; }
}
public class NodeBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public NodeBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(NodeBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new NodeBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public NodeBlock this[int index]
  {
    get { return (InnerList[index] as NodeBlock); }
  }
}
private NodeBlockCollection _nodesCollection;
public NodeBlockCollection Nodes
{
  get { return _nodesCollection; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public ShortBlockIndex IndirectDamageMaterial
{
  get { return _indirectDamageMaterial; }
  set { _indirectDamageMaterial = value; }
}
public Real MaximumBodyVitality
{
  get { return _maximumBodyVitality; }
  set { _maximumBodyVitality = value; }
}
public Real BodySystemShock
{
  get { return _bodySystemShock; }
  set { _bodySystemShock = value; }
}
public RealFraction FriendlyDamageResistance
{
  get { return _friendlyDamageResistance; }
  set { _friendlyDamageResistance = value; }
}
public TagReference LocalizedDamageEffect
{
  get { return _localizedDamageEffect; }
  set { _localizedDamageEffect = value; }
}
public Real AreaDamageEffectThreshold
{
  get { return _areaDamageEffectThreshold; }
  set { _areaDamageEffectThreshold = value; }
}
public TagReference AreaDamageEffect
{
  get { return _areaDamageEffect; }
  set { _areaDamageEffect = value; }
}
public Real BodyDamagedThreshold
{
  get { return _bodyDamagedThreshold; }
  set { _bodyDamagedThreshold = value; }
}
public TagReference BodyDamagedEffect
{
  get { return _bodyDamagedEffect; }
  set { _bodyDamagedEffect = value; }
}
public TagReference BodyDepletedEffect
{
  get { return _bodyDepletedEffect; }
  set { _bodyDepletedEffect = value; }
}
public Real BodyDestroyedThreshold
{
  get { return _bodyDestroyedThreshold; }
  set { _bodyDestroyedThreshold = value; }
}
public TagReference BodyDestroyedEffect
{
  get { return _bodyDestroyedEffect; }
  set { _bodyDestroyedEffect = value; }
}
public Real MaximumShieldVitality
{
  get { return _maximumShieldVitality; }
  set { _maximumShieldVitality = value; }
}
public Enum ShieldMaterialType
{
  get { return _shieldMaterialType; }
  set { _shieldMaterialType = value; }
}
public Enum ShieldFailureFunction
{
  get { return _shieldFailureFunction; }
  set { _shieldFailureFunction = value; }
}
public RealFraction ShieldFailureThreshold
{
  get { return _shieldFailureThreshold; }
  set { _shieldFailureThreshold = value; }
}
public RealFraction FailingShieldLeakFraction
{
  get { return _failingShieldLeakFraction; }
  set { _failingShieldLeakFraction = value; }
}
public Real MinimumStunDamage
{
  get { return _minimumStunDamage; }
  set { _minimumStunDamage = value; }
}
public Real StunTime
{
  get { return _stunTime; }
  set { _stunTime = value; }
}
public Real RechargeTime
{
  get { return _rechargeTime; }
  set { _rechargeTime = value; }
}
public Real ShieldDamagedThreshold
{
  get { return _shieldDamagedThreshold; }
  set { _shieldDamagedThreshold = value; }
}
public TagReference ShieldDamagedEffect
{
  get { return _shieldDamagedEffect; }
  set { _shieldDamagedEffect = value; }
}
public TagReference ShieldDepletedEffect
{
  get { return _shieldDepletedEffect; }
  set { _shieldDepletedEffect = value; }
}
public TagReference ShieldRechargingEffect
{
  get { return _shieldRechargingEffect; }
  set { _shieldRechargingEffect = value; }
}
public RealBounds X
{
  get { return _x; }
  set { _x = value; }
}
public RealBounds Y
{
  get { return _y; }
  set { _y = value; }
}
public RealBounds Z
{
  get { return _z; }
  set { _z = value; }
}
public ModelCollisionGeometryBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(2);
__unnamed2 = new Pad(24);
__unnamed3 = new Pad(28);
__unnamed4 = new Pad(8);
__unnamed5 = new Pad(32);
__unnamed6 = new Pad(2);
__unnamed7 = new Pad(24);
__unnamed8 = new Pad(2);
__unnamed9 = new Pad(16);
__unnamed10 = new Pad(16);
__unnamed11 = new Pad(96);
__unnamed12 = new Pad(12);
__unnamed13 = new Pad(112);
__unnamed14 = new Pad(16);
_materialsCollection = new DamageMaterialsBlockCollection(_materials);
_regionsCollection = new DamageRegionsBlockCollection(_regions);
_modifiersCollection = new DamageModifiersBlockCollection(_modifiers);
_pathfindingSpheresCollection = new SphereBlockCollection(_pathfindingSpheres);
_nodesCollection = new NodeBlockCollection(_nodes);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _indirectDamageMaterial.Read(reader);
  __unnamed.Read(reader);
  _maximumBodyVitality.Read(reader);
  _bodySystemShock.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  _friendlyDamageResistance.Read(reader);
  __unnamed4.Read(reader);
  __unnamed5.Read(reader);
  _localizedDamageEffect.Read(reader);
  _areaDamageEffectThreshold.Read(reader);
  _areaDamageEffect.Read(reader);
  _bodyDamagedThreshold.Read(reader);
  _bodyDamagedEffect.Read(reader);
  _bodyDepletedEffect.Read(reader);
  _bodyDestroyedThreshold.Read(reader);
  _bodyDestroyedEffect.Read(reader);
  _maximumShieldVitality.Read(reader);
  __unnamed6.Read(reader);
  _shieldMaterialType.Read(reader);
  __unnamed7.Read(reader);
  _shieldFailureFunction.Read(reader);
  __unnamed8.Read(reader);
  _shieldFailureThreshold.Read(reader);
  _failingShieldLeakFraction.Read(reader);
  __unnamed9.Read(reader);
  _minimumStunDamage.Read(reader);
  _stunTime.Read(reader);
  _rechargeTime.Read(reader);
  __unnamed10.Read(reader);
  __unnamed11.Read(reader);
  _shieldDamagedThreshold.Read(reader);
  _shieldDamagedEffect.Read(reader);
  _shieldDepletedEffect.Read(reader);
  _shieldRechargingEffect.Read(reader);
  __unnamed12.Read(reader);
  __unnamed13.Read(reader);
  _materials.Read(reader);
  _regions.Read(reader);
  _modifiers.Read(reader);
  __unnamed14.Read(reader);
  _x.Read(reader);
  _y.Read(reader);
  _z.Read(reader);
  _pathfindingSpheres.Read(reader);
  _nodes.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_localizedDamageEffect.ReadString(reader);
_areaDamageEffect.ReadString(reader);
_bodyDamagedEffect.ReadString(reader);
_bodyDepletedEffect.ReadString(reader);
_bodyDestroyedEffect.ReadString(reader);
_shieldDamagedEffect.ReadString(reader);
_shieldDepletedEffect.ReadString(reader);
_shieldRechargingEffect.ReadString(reader);
for (int x=0; x<_materials.Count; x++)
{
  Materials.AddNew();
  Materials[x].Read(reader);
}
for (int x=0; x<_materials.Count; x++)
  Materials[x].ReadChildData(reader);
for (int x=0; x<_regions.Count; x++)
{
  Regions.AddNew();
  Regions[x].Read(reader);
}
for (int x=0; x<_regions.Count; x++)
  Regions[x].ReadChildData(reader);
for (int x=0; x<_modifiers.Count; x++)
{
  Modifiers.AddNew();
  Modifiers[x].Read(reader);
}
for (int x=0; x<_modifiers.Count; x++)
  Modifiers[x].ReadChildData(reader);
for (int x=0; x<_pathfindingSpheres.Count; x++)
{
  PathfindingSpheres.AddNew();
  PathfindingSpheres[x].Read(reader);
}
for (int x=0; x<_pathfindingSpheres.Count; x++)
  PathfindingSpheres[x].ReadChildData(reader);
for (int x=0; x<_nodes.Count; x++)
{
  Nodes.AddNew();
  Nodes[x].Read(reader);
}
for (int x=0; x<_nodes.Count; x++)
  Nodes[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _indirectDamageMaterial.Write(writer);
    __unnamed.Write(writer);
    _maximumBodyVitality.Write(writer);
    _bodySystemShock.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    _friendlyDamageResistance.Write(writer);
    __unnamed4.Write(writer);
    __unnamed5.Write(writer);
    _localizedDamageEffect.Write(writer);
    _areaDamageEffectThreshold.Write(writer);
    _areaDamageEffect.Write(writer);
    _bodyDamagedThreshold.Write(writer);
    _bodyDamagedEffect.Write(writer);
    _bodyDepletedEffect.Write(writer);
    _bodyDestroyedThreshold.Write(writer);
    _bodyDestroyedEffect.Write(writer);
    _maximumShieldVitality.Write(writer);
    __unnamed6.Write(writer);
    _shieldMaterialType.Write(writer);
    __unnamed7.Write(writer);
    _shieldFailureFunction.Write(writer);
    __unnamed8.Write(writer);
    _shieldFailureThreshold.Write(writer);
    _failingShieldLeakFraction.Write(writer);
    __unnamed9.Write(writer);
    _minimumStunDamage.Write(writer);
    _stunTime.Write(writer);
    _rechargeTime.Write(writer);
    __unnamed10.Write(writer);
    __unnamed11.Write(writer);
    _shieldDamagedThreshold.Write(writer);
    _shieldDamagedEffect.Write(writer);
    _shieldDepletedEffect.Write(writer);
    _shieldRechargingEffect.Write(writer);
    __unnamed12.Write(writer);
    __unnamed13.Write(writer);
    _materials.Write(writer);
    _regions.Write(writer);
    _modifiers.Write(writer);
    __unnamed14.Write(writer);
    _x.Write(writer);
    _y.Write(writer);
    _z.Write(writer);
    _pathfindingSpheres.Write(writer);
    _nodes.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_localizedDamageEffect.WriteString(writer);
_areaDamageEffect.WriteString(writer);
_bodyDamagedEffect.WriteString(writer);
_bodyDepletedEffect.WriteString(writer);
_bodyDestroyedEffect.WriteString(writer);
_shieldDamagedEffect.WriteString(writer);
_shieldDepletedEffect.WriteString(writer);
_shieldRechargingEffect.WriteString(writer);
_materials.UpdateReflexiveOffset(writer);
for (int x=0; x<_materials.Count; x++)
{
  Materials[x].Write(writer);
}
for (int x=0; x<_materials.Count; x++)
  Materials[x].WriteChildData(writer);
_regions.UpdateReflexiveOffset(writer);
for (int x=0; x<_regions.Count; x++)
{
  Regions[x].Write(writer);
}
for (int x=0; x<_regions.Count; x++)
  Regions[x].WriteChildData(writer);
_modifiers.UpdateReflexiveOffset(writer);
for (int x=0; x<_modifiers.Count; x++)
{
  Modifiers[x].Write(writer);
}
for (int x=0; x<_modifiers.Count; x++)
  Modifiers[x].WriteChildData(writer);
_pathfindingSpheres.UpdateReflexiveOffset(writer);
for (int x=0; x<_pathfindingSpheres.Count; x++)
{
  PathfindingSpheres[x].Write(writer);
}
for (int x=0; x<_pathfindingSpheres.Count; x++)
  PathfindingSpheres[x].WriteChildData(writer);
_nodes.UpdateReflexiveOffset(writer);
for (int x=0; x<_nodes.Count; x++)
{
  Nodes[x].Write(writer);
}
for (int x=0; x<_nodes.Count; x++)
  Nodes[x].WriteChildData(writer);
}
}
public class DamageMaterialsBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private Flags  _flags;	
private Enum _materialType = new Enum();
private Pad  __unnamed;	
private RealFraction _shieldLeakPercentage = new RealFraction();
private Real _shieldDamageMultiplier = new Real();
private Pad  __unnamed2;	
private Real _bodyDamageMultiplier = new Real();
private Pad  __unnamed3;	
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Enum MaterialType
{
  get { return _materialType; }
  set { _materialType = value; }
}
public RealFraction ShieldLeakPercentage
{
  get { return _shieldLeakPercentage; }
  set { _shieldLeakPercentage = value; }
}
public Real ShieldDamageMultiplier
{
  get { return _shieldDamageMultiplier; }
  set { _shieldDamageMultiplier = value; }
}
public Real BodyDamageMultiplier
{
  get { return _bodyDamageMultiplier; }
  set { _bodyDamageMultiplier = value; }
}
public DamageMaterialsBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(2);
__unnamed2 = new Pad(12);
__unnamed3 = new Pad(8);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _flags.Read(reader);
  _materialType.Read(reader);
  __unnamed.Read(reader);
  _shieldLeakPercentage.Read(reader);
  _shieldDamageMultiplier.Read(reader);
  __unnamed2.Read(reader);
  _bodyDamageMultiplier.Read(reader);
  __unnamed3.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _flags.Write(writer);
    _materialType.Write(writer);
    __unnamed.Write(writer);
    _shieldLeakPercentage.Write(writer);
    _shieldDamageMultiplier.Write(writer);
    __unnamed2.Write(writer);
    _bodyDamageMultiplier.Write(writer);
    __unnamed3.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class DamageRegionsBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private Flags  _flags;	
private Pad  __unnamed;	
private Real _damageThreshold = new Real();
private Pad  __unnamed2;	
private TagReference _destroyedEffect = new TagReference();
private Block _permutations = new Block();
public class DamagePermutationsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public DamagePermutationsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(DamagePermutationsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new DamagePermutationsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public DamagePermutationsBlock this[int index]
  {
    get { return (InnerList[index] as DamagePermutationsBlock); }
  }
}
private DamagePermutationsBlockCollection _permutationsCollection;
public DamagePermutationsBlockCollection Permutations
{
  get { return _permutationsCollection; }
}
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Real DamageThreshold
{
  get { return _damageThreshold; }
  set { _damageThreshold = value; }
}
public TagReference DestroyedEffect
{
  get { return _destroyedEffect; }
  set { _destroyedEffect = value; }
}
public DamageRegionsBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(4);
__unnamed2 = new Pad(12);
_permutationsCollection = new DamagePermutationsBlockCollection(_permutations);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _flags.Read(reader);
  __unnamed.Read(reader);
  _damageThreshold.Read(reader);
  __unnamed2.Read(reader);
  _destroyedEffect.Read(reader);
  _permutations.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_destroyedEffect.ReadString(reader);
for (int x=0; x<_permutations.Count; x++)
{
  Permutations.AddNew();
  Permutations[x].Read(reader);
}
for (int x=0; x<_permutations.Count; x++)
  Permutations[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _flags.Write(writer);
    __unnamed.Write(writer);
    _damageThreshold.Write(writer);
    __unnamed2.Write(writer);
    _destroyedEffect.Write(writer);
    _permutations.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_destroyedEffect.WriteString(writer);
_permutations.UpdateReflexiveOffset(writer);
for (int x=0; x<_permutations.Count; x++)
{
  Permutations[x].Write(writer);
}
for (int x=0; x<_permutations.Count; x++)
  Permutations[x].WriteChildData(writer);
}
}
public class DamagePermutationsBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public DamagePermutationsBlock()
{

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class DamageModifiersBlock : IBlock
{
private Pad  __unnamed;	
public DamageModifiersBlock()
{
__unnamed = new Pad(52);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class SphereBlock : IBlock
{
private ShortBlockIndex _node = new ShortBlockIndex();
private Pad  __unnamed;	
private Pad  __unnamed2;	
private RealPoint3D _center = new RealPoint3D();
private Real _radius = new Real();
public ShortBlockIndex Node
{
  get { return _node; }
  set { _node = value; }
}
public RealPoint3D Center
{
  get { return _center; }
  set { _center = value; }
}
public Real Radius
{
  get { return _radius; }
  set { _radius = value; }
}
public SphereBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(12);

}
public void Read(BinaryReader reader)
{
  _node.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  _center.Read(reader);
  _radius.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _node.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    _center.Write(writer);
    _radius.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class NodeBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private ShortBlockIndex _region = new ShortBlockIndex();
private ShortBlockIndex _parentNode = new ShortBlockIndex();
private ShortBlockIndex _nextSiblingNode = new ShortBlockIndex();
private ShortBlockIndex _firstChildNode = new ShortBlockIndex();
private Pad  __unnamed;	
private Block _bsps = new Block();
public class BspBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public BspBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(BspBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new BspBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public BspBlock this[int index]
  {
    get { return (InnerList[index] as BspBlock); }
  }
}
private BspBlockCollection _bspsCollection;
public BspBlockCollection Bsps
{
  get { return _bspsCollection; }
}
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public ShortBlockIndex Region
{
  get { return _region; }
  set { _region = value; }
}
public ShortBlockIndex ParentNode
{
  get { return _parentNode; }
  set { _parentNode = value; }
}
public ShortBlockIndex NextSiblingNode
{
  get { return _nextSiblingNode; }
  set { _nextSiblingNode = value; }
}
public ShortBlockIndex FirstChildNode
{
  get { return _firstChildNode; }
  set { _firstChildNode = value; }
}
public NodeBlock()
{
__unnamed = new Pad(12);
_bspsCollection = new BspBlockCollection(_bsps);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _region.Read(reader);
  _parentNode.Read(reader);
  _nextSiblingNode.Read(reader);
  _firstChildNode.Read(reader);
  __unnamed.Read(reader);
  _bsps.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_bsps.Count; x++)
{
  Bsps.AddNew();
  Bsps[x].Read(reader);
}
for (int x=0; x<_bsps.Count; x++)
  Bsps[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _region.Write(writer);
    _parentNode.Write(writer);
    _nextSiblingNode.Write(writer);
    _firstChildNode.Write(writer);
    __unnamed.Write(writer);
    _bsps.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_bsps.UpdateReflexiveOffset(writer);
for (int x=0; x<_bsps.Count; x++)
{
  Bsps[x].Write(writer);
}
for (int x=0; x<_bsps.Count; x++)
  Bsps[x].WriteChildData(writer);
}
}
public class BspBlock : IBlock
{
private Block _bsp3dNodes = new Block();
private Block _planes = new Block();
private Block _leaves = new Block();
private Block _bsp2dReferences = new Block();
private Block _bsp2dNodes = new Block();
private Block _surfaces = new Block();
private Block _edges = new Block();
private Block _vertices = new Block();
public class Bsp3dnodeBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public Bsp3dnodeBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(Bsp3dnodeBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new Bsp3dnodeBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public Bsp3dnodeBlock this[int index]
  {
    get { return (InnerList[index] as Bsp3dnodeBlock); }
  }
}
private Bsp3dnodeBlockCollection _bsp3dNodesCollection;
public Bsp3dnodeBlockCollection Bsp3dNodes
{
  get { return _bsp3dNodesCollection; }
}
public class PlaneBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public PlaneBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(PlaneBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new PlaneBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public PlaneBlock this[int index]
  {
    get { return (InnerList[index] as PlaneBlock); }
  }
}
private PlaneBlockCollection _planesCollection;
public PlaneBlockCollection Planes
{
  get { return _planesCollection; }
}
public class LeafBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public LeafBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(LeafBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new LeafBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public LeafBlock this[int index]
  {
    get { return (InnerList[index] as LeafBlock); }
  }
}
private LeafBlockCollection _leavesCollection;
public LeafBlockCollection Leaves
{
  get { return _leavesCollection; }
}
public class Bsp2dreferenceBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public Bsp2dreferenceBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(Bsp2dreferenceBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new Bsp2dreferenceBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public Bsp2dreferenceBlock this[int index]
  {
    get { return (InnerList[index] as Bsp2dreferenceBlock); }
  }
}
private Bsp2dreferenceBlockCollection _bsp2dReferencesCollection;
public Bsp2dreferenceBlockCollection Bsp2dReferences
{
  get { return _bsp2dReferencesCollection; }
}
public class Bsp2dnodeBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public Bsp2dnodeBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(Bsp2dnodeBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new Bsp2dnodeBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public Bsp2dnodeBlock this[int index]
  {
    get { return (InnerList[index] as Bsp2dnodeBlock); }
  }
}
private Bsp2dnodeBlockCollection _bsp2dNodesCollection;
public Bsp2dnodeBlockCollection Bsp2dNodes
{
  get { return _bsp2dNodesCollection; }
}
public class SurfaceBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public SurfaceBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(SurfaceBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new SurfaceBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public SurfaceBlock this[int index]
  {
    get { return (InnerList[index] as SurfaceBlock); }
  }
}
private SurfaceBlockCollection _surfacesCollection;
public SurfaceBlockCollection Surfaces
{
  get { return _surfacesCollection; }
}
public class EdgeBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public EdgeBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(EdgeBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new EdgeBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public EdgeBlock this[int index]
  {
    get { return (InnerList[index] as EdgeBlock); }
  }
}
private EdgeBlockCollection _edgesCollection;
public EdgeBlockCollection Edges
{
  get { return _edgesCollection; }
}
public class VertexBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public VertexBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(VertexBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new VertexBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public VertexBlock this[int index]
  {
    get { return (InnerList[index] as VertexBlock); }
  }
}
private VertexBlockCollection _verticesCollection;
public VertexBlockCollection Vertices
{
  get { return _verticesCollection; }
}
public BspBlock()
{
_bsp3dNodesCollection = new Bsp3dnodeBlockCollection(_bsp3dNodes);
_planesCollection = new PlaneBlockCollection(_planes);
_leavesCollection = new LeafBlockCollection(_leaves);
_bsp2dReferencesCollection = new Bsp2dreferenceBlockCollection(_bsp2dReferences);
_bsp2dNodesCollection = new Bsp2dnodeBlockCollection(_bsp2dNodes);
_surfacesCollection = new SurfaceBlockCollection(_surfaces);
_edgesCollection = new EdgeBlockCollection(_edges);
_verticesCollection = new VertexBlockCollection(_vertices);

}
public void Read(BinaryReader reader)
{
  _bsp3dNodes.Read(reader);
  _planes.Read(reader);
  _leaves.Read(reader);
  _bsp2dReferences.Read(reader);
  _bsp2dNodes.Read(reader);
  _surfaces.Read(reader);
  _edges.Read(reader);
  _vertices.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_bsp3dNodes.Count; x++)
{
  Bsp3dNodes.AddNew();
  Bsp3dNodes[x].Read(reader);
}
for (int x=0; x<_bsp3dNodes.Count; x++)
  Bsp3dNodes[x].ReadChildData(reader);
for (int x=0; x<_planes.Count; x++)
{
  Planes.AddNew();
  Planes[x].Read(reader);
}
for (int x=0; x<_planes.Count; x++)
  Planes[x].ReadChildData(reader);
for (int x=0; x<_leaves.Count; x++)
{
  Leaves.AddNew();
  Leaves[x].Read(reader);
}
for (int x=0; x<_leaves.Count; x++)
  Leaves[x].ReadChildData(reader);
for (int x=0; x<_bsp2dReferences.Count; x++)
{
  Bsp2dReferences.AddNew();
  Bsp2dReferences[x].Read(reader);
}
for (int x=0; x<_bsp2dReferences.Count; x++)
  Bsp2dReferences[x].ReadChildData(reader);
for (int x=0; x<_bsp2dNodes.Count; x++)
{
  Bsp2dNodes.AddNew();
  Bsp2dNodes[x].Read(reader);
}
for (int x=0; x<_bsp2dNodes.Count; x++)
  Bsp2dNodes[x].ReadChildData(reader);
for (int x=0; x<_surfaces.Count; x++)
{
  Surfaces.AddNew();
  Surfaces[x].Read(reader);
}
for (int x=0; x<_surfaces.Count; x++)
  Surfaces[x].ReadChildData(reader);
for (int x=0; x<_edges.Count; x++)
{
  Edges.AddNew();
  Edges[x].Read(reader);
}
for (int x=0; x<_edges.Count; x++)
  Edges[x].ReadChildData(reader);
for (int x=0; x<_vertices.Count; x++)
{
  Vertices.AddNew();
  Vertices[x].Read(reader);
}
for (int x=0; x<_vertices.Count; x++)
  Vertices[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _bsp3dNodes.Write(writer);
    _planes.Write(writer);
    _leaves.Write(writer);
    _bsp2dReferences.Write(writer);
    _bsp2dNodes.Write(writer);
    _surfaces.Write(writer);
    _edges.Write(writer);
    _vertices.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_bsp3dNodes.UpdateReflexiveOffset(writer);
for (int x=0; x<_bsp3dNodes.Count; x++)
{
  Bsp3dNodes[x].Write(writer);
}
for (int x=0; x<_bsp3dNodes.Count; x++)
  Bsp3dNodes[x].WriteChildData(writer);
_planes.UpdateReflexiveOffset(writer);
for (int x=0; x<_planes.Count; x++)
{
  Planes[x].Write(writer);
}
for (int x=0; x<_planes.Count; x++)
  Planes[x].WriteChildData(writer);
_leaves.UpdateReflexiveOffset(writer);
for (int x=0; x<_leaves.Count; x++)
{
  Leaves[x].Write(writer);
}
for (int x=0; x<_leaves.Count; x++)
  Leaves[x].WriteChildData(writer);
_bsp2dReferences.UpdateReflexiveOffset(writer);
for (int x=0; x<_bsp2dReferences.Count; x++)
{
  Bsp2dReferences[x].Write(writer);
}
for (int x=0; x<_bsp2dReferences.Count; x++)
  Bsp2dReferences[x].WriteChildData(writer);
_bsp2dNodes.UpdateReflexiveOffset(writer);
for (int x=0; x<_bsp2dNodes.Count; x++)
{
  Bsp2dNodes[x].Write(writer);
}
for (int x=0; x<_bsp2dNodes.Count; x++)
  Bsp2dNodes[x].WriteChildData(writer);
_surfaces.UpdateReflexiveOffset(writer);
for (int x=0; x<_surfaces.Count; x++)
{
  Surfaces[x].Write(writer);
}
for (int x=0; x<_surfaces.Count; x++)
  Surfaces[x].WriteChildData(writer);
_edges.UpdateReflexiveOffset(writer);
for (int x=0; x<_edges.Count; x++)
{
  Edges[x].Write(writer);
}
for (int x=0; x<_edges.Count; x++)
  Edges[x].WriteChildData(writer);
_vertices.UpdateReflexiveOffset(writer);
for (int x=0; x<_vertices.Count; x++)
{
  Vertices[x].Write(writer);
}
for (int x=0; x<_vertices.Count; x++)
  Vertices[x].WriteChildData(writer);
}
}
public class Bsp3dnodeBlock : IBlock
{
private LongInteger _plane = new LongInteger();
private LongInteger _backChild = new LongInteger();
private LongInteger _frontChild = new LongInteger();
public LongInteger Plane
{
  get { return _plane; }
  set { _plane = value; }
}
public LongInteger BackChild
{
  get { return _backChild; }
  set { _backChild = value; }
}
public LongInteger FrontChild
{
  get { return _frontChild; }
  set { _frontChild = value; }
}
public Bsp3dnodeBlock()
{

}
public void Read(BinaryReader reader)
{
  _plane.Read(reader);
  _backChild.Read(reader);
  _frontChild.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _plane.Write(writer);
    _backChild.Write(writer);
    _frontChild.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class PlaneBlock : IBlock
{
private RealPlane3D _plane = new RealPlane3D();
public RealPlane3D Plane
{
  get { return _plane; }
  set { _plane = value; }
}
public PlaneBlock()
{

}
public void Read(BinaryReader reader)
{
  _plane.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _plane.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class LeafBlock : IBlock
{
private Flags  _flags;	
private ShortInteger _bsp2dReferenceCount = new ShortInteger();
private LongInteger _firstBsp2dReference = new LongInteger();
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public ShortInteger Bsp2dReferenceCount
{
  get { return _bsp2dReferenceCount; }
  set { _bsp2dReferenceCount = value; }
}
public LongInteger FirstBsp2dReference
{
  get { return _firstBsp2dReference; }
  set { _firstBsp2dReference = value; }
}
public LeafBlock()
{
_flags = new Flags(2);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _bsp2dReferenceCount.Read(reader);
  _firstBsp2dReference.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _bsp2dReferenceCount.Write(writer);
    _firstBsp2dReference.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class Bsp2dreferenceBlock : IBlock
{
private LongInteger _plane = new LongInteger();
private LongInteger _bsp2dNode = new LongInteger();
public LongInteger Plane
{
  get { return _plane; }
  set { _plane = value; }
}
public LongInteger Bsp2dNode
{
  get { return _bsp2dNode; }
  set { _bsp2dNode = value; }
}
public Bsp2dreferenceBlock()
{

}
public void Read(BinaryReader reader)
{
  _plane.Read(reader);
  _bsp2dNode.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _plane.Write(writer);
    _bsp2dNode.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class Bsp2dnodeBlock : IBlock
{
private RealPlane2D _plane = new RealPlane2D();
private LongInteger _leftChild = new LongInteger();
private LongInteger _rightChild = new LongInteger();
public RealPlane2D Plane
{
  get { return _plane; }
  set { _plane = value; }
}
public LongInteger LeftChild
{
  get { return _leftChild; }
  set { _leftChild = value; }
}
public LongInteger RightChild
{
  get { return _rightChild; }
  set { _rightChild = value; }
}
public Bsp2dnodeBlock()
{

}
public void Read(BinaryReader reader)
{
  _plane.Read(reader);
  _leftChild.Read(reader);
  _rightChild.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _plane.Write(writer);
    _leftChild.Write(writer);
    _rightChild.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class SurfaceBlock : IBlock
{
private LongInteger _plane = new LongInteger();
private LongInteger _firstEdge = new LongInteger();
private Flags  _flags;	
private CharInteger _breakableSurface = new CharInteger();
private ShortInteger _material = new ShortInteger();
public LongInteger Plane
{
  get { return _plane; }
  set { _plane = value; }
}
public LongInteger FirstEdge
{
  get { return _firstEdge; }
  set { _firstEdge = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public CharInteger BreakableSurface
{
  get { return _breakableSurface; }
  set { _breakableSurface = value; }
}
public ShortInteger Material
{
  get { return _material; }
  set { _material = value; }
}
public SurfaceBlock()
{
_flags = new Flags(1);

}
public void Read(BinaryReader reader)
{
  _plane.Read(reader);
  _firstEdge.Read(reader);
  _flags.Read(reader);
  _breakableSurface.Read(reader);
  _material.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _plane.Write(writer);
    _firstEdge.Write(writer);
    _flags.Write(writer);
    _breakableSurface.Write(writer);
    _material.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class EdgeBlock : IBlock
{
private LongInteger _startVertex = new LongInteger();
private LongInteger _endVertex = new LongInteger();
private LongInteger _forwardEdge = new LongInteger();
private LongInteger _reverseEdge = new LongInteger();
private LongInteger _leftSurface = new LongInteger();
private LongInteger _rightSurface = new LongInteger();
public LongInteger StartVertex
{
  get { return _startVertex; }
  set { _startVertex = value; }
}
public LongInteger EndVertex
{
  get { return _endVertex; }
  set { _endVertex = value; }
}
public LongInteger ForwardEdge
{
  get { return _forwardEdge; }
  set { _forwardEdge = value; }
}
public LongInteger ReverseEdge
{
  get { return _reverseEdge; }
  set { _reverseEdge = value; }
}
public LongInteger LeftSurface
{
  get { return _leftSurface; }
  set { _leftSurface = value; }
}
public LongInteger RightSurface
{
  get { return _rightSurface; }
  set { _rightSurface = value; }
}
public EdgeBlock()
{

}
public void Read(BinaryReader reader)
{
  _startVertex.Read(reader);
  _endVertex.Read(reader);
  _forwardEdge.Read(reader);
  _reverseEdge.Read(reader);
  _leftSurface.Read(reader);
  _rightSurface.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _startVertex.Write(writer);
    _endVertex.Write(writer);
    _forwardEdge.Write(writer);
    _reverseEdge.Write(writer);
    _leftSurface.Write(writer);
    _rightSurface.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class VertexBlock : IBlock
{
private RealPoint3D _point = new RealPoint3D();
private LongInteger _firstEdge = new LongInteger();
public RealPoint3D Point
{
  get { return _point; }
  set { _point = value; }
}
public LongInteger FirstEdge
{
  get { return _firstEdge; }
  set { _firstEdge = value; }
}
public VertexBlock()
{

}
public void Read(BinaryReader reader)
{
  _point.Read(reader);
  _firstEdge.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _point.Write(writer);
    _firstEdge.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
  }
}
