using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Model : IBlock
  {
    public ModelBlock ModelValues = new ModelBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'Model'------------------------------------------------------");
      ModelValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      ModelValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      ModelValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      ModelValues.WriteChildData(writer);
    }
public class ModelBlock : IBlock
{
private Flags  _flags;	
private LongInteger _nodeListChecksum = new LongInteger();
private Real _supe = new Real();
private Real _highDetailCutoff = new Real();
private Real _mediumDetailCutoff = new Real();
private Real _lowDetailCutoff = new Real();
private Real _supe2 = new Real();
private ShortInteger _supe3 = new ShortInteger();
private ShortInteger _highDetailNodeCount = new ShortInteger();
private ShortInteger _mediumDetailNodeCount = new ShortInteger();
private ShortInteger _lowDetailNodeCount = new ShortInteger();
private ShortInteger _supe4 = new ShortInteger();
private Pad  __unnamed;	
private Pad  __unnamed2;	
private Real _baseMap = new Real();
private Real _baseMap2 = new Real();
private Pad  __unnamed3;	
private Block _markers = new Block();
private Block _nodes = new Block();
private Block _regions = new Block();
private Block _geometries = new Block();
private Block _shaders = new Block();
public class ModelMarkersBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ModelMarkersBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ModelMarkersBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ModelMarkersBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ModelMarkersBlock this[int index]
  {
    get { return (InnerList[index] as ModelMarkersBlock); }
  }
}
private ModelMarkersBlockCollection _markersCollection;
public ModelMarkersBlockCollection Markers
{
  get { return _markersCollection; }
}
public class ModelNodeBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ModelNodeBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ModelNodeBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ModelNodeBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ModelNodeBlock this[int index]
  {
    get { return (InnerList[index] as ModelNodeBlock); }
  }
}
private ModelNodeBlockCollection _nodesCollection;
public ModelNodeBlockCollection Nodes
{
  get { return _nodesCollection; }
}
public class ModelRegionBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ModelRegionBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ModelRegionBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ModelRegionBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ModelRegionBlock this[int index]
  {
    get { return (InnerList[index] as ModelRegionBlock); }
  }
}
private ModelRegionBlockCollection _regionsCollection;
public ModelRegionBlockCollection Regions
{
  get { return _regionsCollection; }
}
public class ModelGeometryBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ModelGeometryBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ModelGeometryBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ModelGeometryBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ModelGeometryBlock this[int index]
  {
    get { return (InnerList[index] as ModelGeometryBlock); }
  }
}
private ModelGeometryBlockCollection _geometriesCollection;
public ModelGeometryBlockCollection Geometries
{
  get { return _geometriesCollection; }
}
public class ModelShaderReferenceBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ModelShaderReferenceBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ModelShaderReferenceBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ModelShaderReferenceBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ModelShaderReferenceBlock this[int index]
  {
    get { return (InnerList[index] as ModelShaderReferenceBlock); }
  }
}
private ModelShaderReferenceBlockCollection _shadersCollection;
public ModelShaderReferenceBlockCollection Shaders
{
  get { return _shadersCollection; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public LongInteger NodeListChecksum
{
  get { return _nodeListChecksum; }
  set { _nodeListChecksum = value; }
}
public Real Supe
{
  get { return _supe; }
  set { _supe = value; }
}
public Real HighDetailCutoff
{
  get { return _highDetailCutoff; }
  set { _highDetailCutoff = value; }
}
public Real MediumDetailCutoff
{
  get { return _mediumDetailCutoff; }
  set { _mediumDetailCutoff = value; }
}
public Real LowDetailCutoff
{
  get { return _lowDetailCutoff; }
  set { _lowDetailCutoff = value; }
}
public Real Supe2
{
  get { return _supe2; }
  set { _supe2 = value; }
}
public ShortInteger Supe3
{
  get { return _supe3; }
  set { _supe3 = value; }
}
public ShortInteger HighDetailNodeCount
{
  get { return _highDetailNodeCount; }
  set { _highDetailNodeCount = value; }
}
public ShortInteger MediumDetailNodeCount
{
  get { return _mediumDetailNodeCount; }
  set { _mediumDetailNodeCount = value; }
}
public ShortInteger LowDetailNodeCount
{
  get { return _lowDetailNodeCount; }
  set { _lowDetailNodeCount = value; }
}
public ShortInteger Supe4
{
  get { return _supe4; }
  set { _supe4 = value; }
}
public Real BaseMap
{
  get { return _baseMap; }
  set { _baseMap = value; }
}
public Real BaseMap2
{
  get { return _baseMap2; }
  set { _baseMap2 = value; }
}
public ModelBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(2);
__unnamed2 = new Pad(8);
__unnamed3 = new Pad(116);
_markersCollection = new ModelMarkersBlockCollection(_markers);
_nodesCollection = new ModelNodeBlockCollection(_nodes);
_regionsCollection = new ModelRegionBlockCollection(_regions);
_geometriesCollection = new ModelGeometryBlockCollection(_geometries);
_shadersCollection = new ModelShaderReferenceBlockCollection(_shaders);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _nodeListChecksum.Read(reader);
  _supe.Read(reader);
  _highDetailCutoff.Read(reader);
  _mediumDetailCutoff.Read(reader);
  _lowDetailCutoff.Read(reader);
  _supe2.Read(reader);
  _supe3.Read(reader);
  _highDetailNodeCount.Read(reader);
  _mediumDetailNodeCount.Read(reader);
  _lowDetailNodeCount.Read(reader);
  _supe4.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  _baseMap.Read(reader);
  _baseMap2.Read(reader);
  __unnamed3.Read(reader);
  _markers.Read(reader);
  _nodes.Read(reader);
  _regions.Read(reader);
  _geometries.Read(reader);
  _shaders.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_markers.Count; x++)
{
  Markers.AddNew();
  Markers[x].Read(reader);
}
for (int x=0; x<_markers.Count; x++)
  Markers[x].ReadChildData(reader);
for (int x=0; x<_nodes.Count; x++)
{
  Nodes.AddNew();
  Nodes[x].Read(reader);
}
for (int x=0; x<_nodes.Count; x++)
  Nodes[x].ReadChildData(reader);
for (int x=0; x<_regions.Count; x++)
{
  Regions.AddNew();
  Regions[x].Read(reader);
}
for (int x=0; x<_regions.Count; x++)
  Regions[x].ReadChildData(reader);
for (int x=0; x<_geometries.Count; x++)
{
  Geometries.AddNew();
  Geometries[x].Read(reader);
}
for (int x=0; x<_geometries.Count; x++)
  Geometries[x].ReadChildData(reader);
for (int x=0; x<_shaders.Count; x++)
{
  Shaders.AddNew();
  Shaders[x].Read(reader);
}
for (int x=0; x<_shaders.Count; x++)
  Shaders[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _nodeListChecksum.Write(writer);
    _supe.Write(writer);
    _highDetailCutoff.Write(writer);
    _mediumDetailCutoff.Write(writer);
    _lowDetailCutoff.Write(writer);
    _supe2.Write(writer);
    _supe3.Write(writer);
    _highDetailNodeCount.Write(writer);
    _mediumDetailNodeCount.Write(writer);
    _lowDetailNodeCount.Write(writer);
    _supe4.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    _baseMap.Write(writer);
    _baseMap2.Write(writer);
    __unnamed3.Write(writer);
    _markers.Write(writer);
    _nodes.Write(writer);
    _regions.Write(writer);
    _geometries.Write(writer);
    _shaders.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_markers.UpdateReflexiveOffset(writer);
for (int x=0; x<_markers.Count; x++)
{
  Markers[x].Write(writer);
}
for (int x=0; x<_markers.Count; x++)
  Markers[x].WriteChildData(writer);
_nodes.UpdateReflexiveOffset(writer);
for (int x=0; x<_nodes.Count; x++)
{
  Nodes[x].Write(writer);
}
for (int x=0; x<_nodes.Count; x++)
  Nodes[x].WriteChildData(writer);
_regions.UpdateReflexiveOffset(writer);
for (int x=0; x<_regions.Count; x++)
{
  Regions[x].Write(writer);
}
for (int x=0; x<_regions.Count; x++)
  Regions[x].WriteChildData(writer);
_geometries.UpdateReflexiveOffset(writer);
for (int x=0; x<_geometries.Count; x++)
{
  Geometries[x].Write(writer);
}
for (int x=0; x<_geometries.Count; x++)
  Geometries[x].WriteChildData(writer);
_shaders.UpdateReflexiveOffset(writer);
for (int x=0; x<_shaders.Count; x++)
{
  Shaders[x].Write(writer);
}
for (int x=0; x<_shaders.Count; x++)
  Shaders[x].WriteChildData(writer);
}
}
public class ModelMarkersBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private ShortInteger _magicIdentifier = new ShortInteger();
private Pad  __unnamed;	
private Pad  __unnamed2;	
private Block _instances = new Block();
public class ModelMarkerInstanceBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ModelMarkerInstanceBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ModelMarkerInstanceBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ModelMarkerInstanceBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ModelMarkerInstanceBlock this[int index]
  {
    get { return (InnerList[index] as ModelMarkerInstanceBlock); }
  }
}
private ModelMarkerInstanceBlockCollection _instancesCollection;
public ModelMarkerInstanceBlockCollection Instances
{
  get { return _instancesCollection; }
}
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public ShortInteger MagicIdentifier
{
  get { return _magicIdentifier; }
  set { _magicIdentifier = value; }
}
public ModelMarkersBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(16);
_instancesCollection = new ModelMarkerInstanceBlockCollection(_instances);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _magicIdentifier.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  _instances.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_instances.Count; x++)
{
  Instances.AddNew();
  Instances[x].Read(reader);
}
for (int x=0; x<_instances.Count; x++)
  Instances[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _magicIdentifier.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    _instances.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_instances.UpdateReflexiveOffset(writer);
for (int x=0; x<_instances.Count; x++)
{
  Instances[x].Write(writer);
}
for (int x=0; x<_instances.Count; x++)
  Instances[x].WriteChildData(writer);
}
}
public class ModelMarkerInstanceBlock : IBlock
{
private CharInteger _regionIndex = new CharInteger();
private CharInteger _permutationIndex = new CharInteger();
private CharInteger _nodeIndex = new CharInteger();
private Pad  __unnamed;	
private RealPoint3D _translation = new RealPoint3D();
private RealQuaternion _rotation = new RealQuaternion();
public CharInteger RegionIndex
{
  get { return _regionIndex; }
  set { _regionIndex = value; }
}
public CharInteger PermutationIndex
{
  get { return _permutationIndex; }
  set { _permutationIndex = value; }
}
public CharInteger NodeIndex
{
  get { return _nodeIndex; }
  set { _nodeIndex = value; }
}
public RealPoint3D Translation
{
  get { return _translation; }
  set { _translation = value; }
}
public RealQuaternion Rotation
{
  get { return _rotation; }
  set { _rotation = value; }
}
public ModelMarkerInstanceBlock()
{
__unnamed = new Pad(1);

}
public void Read(BinaryReader reader)
{
  _regionIndex.Read(reader);
  _permutationIndex.Read(reader);
  _nodeIndex.Read(reader);
  __unnamed.Read(reader);
  _translation.Read(reader);
  _rotation.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _regionIndex.Write(writer);
    _permutationIndex.Write(writer);
    _nodeIndex.Write(writer);
    __unnamed.Write(writer);
    _translation.Write(writer);
    _rotation.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class ModelNodeBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private ShortBlockIndex _nextSiblingNodeIndex = new ShortBlockIndex();
private ShortBlockIndex _firstChildNodeIndex = new ShortBlockIndex();
private ShortBlockIndex _parentNodeIndex = new ShortBlockIndex();
private Pad  __unnamed;	
private RealPoint3D _defaultTranslation = new RealPoint3D();
private RealQuaternion _defaultRotation = new RealQuaternion();
private Real _nodeDistanceFromParent = new Real();
private Pad  __unnamed2;	
private Pad  __unnamed3;	
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
public RealPoint3D DefaultTranslation
{
  get { return _defaultTranslation; }
  set { _defaultTranslation = value; }
}
public RealQuaternion DefaultRotation
{
  get { return _defaultRotation; }
  set { _defaultRotation = value; }
}
public Real NodeDistanceFromParent
{
  get { return _nodeDistanceFromParent; }
  set { _nodeDistanceFromParent = value; }
}
public ModelNodeBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(32);
__unnamed3 = new Pad(52);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _nextSiblingNodeIndex.Read(reader);
  _firstChildNodeIndex.Read(reader);
  _parentNodeIndex.Read(reader);
  __unnamed.Read(reader);
  _defaultTranslation.Read(reader);
  _defaultRotation.Read(reader);
  _nodeDistanceFromParent.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
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
    _defaultTranslation.Write(writer);
    _defaultRotation.Write(writer);
    _nodeDistanceFromParent.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class ModelRegionBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private Pad  __unnamed;	
private Block _permutations = new Block();
public class ModelRegionPermutationBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ModelRegionPermutationBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ModelRegionPermutationBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ModelRegionPermutationBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ModelRegionPermutationBlock this[int index]
  {
    get { return (InnerList[index] as ModelRegionPermutationBlock); }
  }
}
private ModelRegionPermutationBlockCollection _permutationsCollection;
public ModelRegionPermutationBlockCollection Permutations
{
  get { return _permutationsCollection; }
}
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public ModelRegionBlock()
{
__unnamed = new Pad(32);
_permutationsCollection = new ModelRegionPermutationBlockCollection(_permutations);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  __unnamed.Read(reader);
  _permutations.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
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
    __unnamed.Write(writer);
    _permutations.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_permutations.UpdateReflexiveOffset(writer);
for (int x=0; x<_permutations.Count; x++)
{
  Permutations[x].Write(writer);
}
for (int x=0; x<_permutations.Count; x++)
  Permutations[x].WriteChildData(writer);
}
}
public class ModelRegionPermutationBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private Flags  _flags;	
private Pad  __unnamed;	
private ShortBlockIndex _superLow = new ShortBlockIndex();
private ShortBlockIndex _low = new ShortBlockIndex();
private ShortBlockIndex _medium = new ShortBlockIndex();
private ShortBlockIndex _high = new ShortBlockIndex();
private ShortBlockIndex _superHigh = new ShortBlockIndex();
private Pad  __unnamed2;	
private Block _markers = new Block();
public class ModelRegionPermutationMarkerBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ModelRegionPermutationMarkerBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ModelRegionPermutationMarkerBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ModelRegionPermutationMarkerBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ModelRegionPermutationMarkerBlock this[int index]
  {
    get { return (InnerList[index] as ModelRegionPermutationMarkerBlock); }
  }
}
private ModelRegionPermutationMarkerBlockCollection _markersCollection;
public ModelRegionPermutationMarkerBlockCollection Markers
{
  get { return _markersCollection; }
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
public ShortBlockIndex SuperLow
{
  get { return _superLow; }
  set { _superLow = value; }
}
public ShortBlockIndex Low
{
  get { return _low; }
  set { _low = value; }
}
public ShortBlockIndex Medium
{
  get { return _medium; }
  set { _medium = value; }
}
public ShortBlockIndex High
{
  get { return _high; }
  set { _high = value; }
}
public ShortBlockIndex SuperHigh
{
  get { return _superHigh; }
  set { _superHigh = value; }
}
public ModelRegionPermutationBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(28);
__unnamed2 = new Pad(2);
_markersCollection = new ModelRegionPermutationMarkerBlockCollection(_markers);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _flags.Read(reader);
  __unnamed.Read(reader);
  _superLow.Read(reader);
  _low.Read(reader);
  _medium.Read(reader);
  _high.Read(reader);
  _superHigh.Read(reader);
  __unnamed2.Read(reader);
  _markers.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_markers.Count; x++)
{
  Markers.AddNew();
  Markers[x].Read(reader);
}
for (int x=0; x<_markers.Count; x++)
  Markers[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _flags.Write(writer);
    __unnamed.Write(writer);
    _superLow.Write(writer);
    _low.Write(writer);
    _medium.Write(writer);
    _high.Write(writer);
    _superHigh.Write(writer);
    __unnamed2.Write(writer);
    _markers.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_markers.UpdateReflexiveOffset(writer);
for (int x=0; x<_markers.Count; x++)
{
  Markers[x].Write(writer);
}
for (int x=0; x<_markers.Count; x++)
  Markers[x].WriteChildData(writer);
}
}
public class ModelRegionPermutationMarkerBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private ShortBlockIndex _nodeIndex = new ShortBlockIndex();
private Pad  __unnamed;	
private RealQuaternion _rotation = new RealQuaternion();
private RealPoint3D _translation = new RealPoint3D();
private Pad  __unnamed2;	
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public ShortBlockIndex NodeIndex
{
  get { return _nodeIndex; }
  set { _nodeIndex = value; }
}
public RealQuaternion Rotation
{
  get { return _rotation; }
  set { _rotation = value; }
}
public RealPoint3D Translation
{
  get { return _translation; }
  set { _translation = value; }
}
public ModelRegionPermutationMarkerBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(16);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _nodeIndex.Read(reader);
  __unnamed.Read(reader);
  _rotation.Read(reader);
  _translation.Read(reader);
  __unnamed2.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _nodeIndex.Write(writer);
    __unnamed.Write(writer);
    _rotation.Write(writer);
    _translation.Write(writer);
    __unnamed2.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class ModelGeometryBlock : IBlock
{
private Flags  _flags;	
private Pad  __unnamed;	
private Block _parts = new Block();
public class ModelGeometryPartBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ModelGeometryPartBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ModelGeometryPartBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ModelGeometryPartBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ModelGeometryPartBlock this[int index]
  {
    get { return (InnerList[index] as ModelGeometryPartBlock); }
  }
}
private ModelGeometryPartBlockCollection _partsCollection;
public ModelGeometryPartBlockCollection Parts
{
  get { return _partsCollection; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public ModelGeometryBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(32);
_partsCollection = new ModelGeometryPartBlockCollection(_parts);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  __unnamed.Read(reader);
  _parts.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_parts.Count; x++)
{
  Parts.AddNew();
  Parts[x].Read(reader);
}
for (int x=0; x<_parts.Count; x++)
  Parts[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    __unnamed.Write(writer);
    _parts.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_parts.UpdateReflexiveOffset(writer);
for (int x=0; x<_parts.Count; x++)
{
  Parts[x].Write(writer);
}
for (int x=0; x<_parts.Count; x++)
  Parts[x].WriteChildData(writer);
}
}
public class ModelGeometryPartBlock : IBlock
{
private Flags  _flags;	
private ShortBlockIndex _shaderIndex = new ShortBlockIndex();
private CharInteger _prevFilthyPartIndex = new CharInteger();
private CharInteger _nextFilthyPartIndex = new CharInteger();
private ShortInteger _centroidPrimaryNode = new ShortInteger();
private ShortInteger _centroidSecondaryNode = new ShortInteger();
private RealFraction _centroidPrimaryWeight = new RealFraction();
private RealFraction _centroidSecondaryWeight = new RealFraction();
private RealPoint3D _centroid = new RealPoint3D();
private Block _uncompressedVertices = new Block();
private Block _compressedVertices = new Block();
private Block _triangles = new Block();
private Pad  __unnamed;	
private Pad  __unnamed2;	
public class ModelVertexUncompressedBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ModelVertexUncompressedBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ModelVertexUncompressedBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ModelVertexUncompressedBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ModelVertexUncompressedBlock this[int index]
  {
    get { return (InnerList[index] as ModelVertexUncompressedBlock); }
  }
}
private ModelVertexUncompressedBlockCollection _uncompressedVerticesCollection;
public ModelVertexUncompressedBlockCollection UncompressedVertices
{
  get { return _uncompressedVerticesCollection; }
}
public class ModelVertexCompressedBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ModelVertexCompressedBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ModelVertexCompressedBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ModelVertexCompressedBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ModelVertexCompressedBlock this[int index]
  {
    get { return (InnerList[index] as ModelVertexCompressedBlock); }
  }
}
private ModelVertexCompressedBlockCollection _compressedVerticesCollection;
public ModelVertexCompressedBlockCollection CompressedVertices
{
  get { return _compressedVerticesCollection; }
}
public class ModelTriangleBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ModelTriangleBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ModelTriangleBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ModelTriangleBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ModelTriangleBlock this[int index]
  {
    get { return (InnerList[index] as ModelTriangleBlock); }
  }
}
private ModelTriangleBlockCollection _trianglesCollection;
public ModelTriangleBlockCollection Triangles
{
  get { return _trianglesCollection; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public ShortBlockIndex ShaderIndex
{
  get { return _shaderIndex; }
  set { _shaderIndex = value; }
}
public CharInteger PrevFilthyPartIndex
{
  get { return _prevFilthyPartIndex; }
  set { _prevFilthyPartIndex = value; }
}
public CharInteger NextFilthyPartIndex
{
  get { return _nextFilthyPartIndex; }
  set { _nextFilthyPartIndex = value; }
}
public ShortInteger CentroidPrimaryNode
{
  get { return _centroidPrimaryNode; }
  set { _centroidPrimaryNode = value; }
}
public ShortInteger CentroidSecondaryNode
{
  get { return _centroidSecondaryNode; }
  set { _centroidSecondaryNode = value; }
}
public RealFraction CentroidPrimaryWeight
{
  get { return _centroidPrimaryWeight; }
  set { _centroidPrimaryWeight = value; }
}
public RealFraction CentroidSecondaryWeight
{
  get { return _centroidSecondaryWeight; }
  set { _centroidSecondaryWeight = value; }
}
public RealPoint3D Centroid
{
  get { return _centroid; }
  set { _centroid = value; }
}
public ModelGeometryPartBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(20);
__unnamed2 = new Pad(16);
_uncompressedVerticesCollection = new ModelVertexUncompressedBlockCollection(_uncompressedVertices);
_compressedVerticesCollection = new ModelVertexCompressedBlockCollection(_compressedVertices);
_trianglesCollection = new ModelTriangleBlockCollection(_triangles);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _shaderIndex.Read(reader);
  _prevFilthyPartIndex.Read(reader);
  _nextFilthyPartIndex.Read(reader);
  _centroidPrimaryNode.Read(reader);
  _centroidSecondaryNode.Read(reader);
  _centroidPrimaryWeight.Read(reader);
  _centroidSecondaryWeight.Read(reader);
  _centroid.Read(reader);
  _uncompressedVertices.Read(reader);
  _compressedVertices.Read(reader);
  _triangles.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_uncompressedVertices.Count; x++)
{
  UncompressedVertices.AddNew();
  UncompressedVertices[x].Read(reader);
}
for (int x=0; x<_uncompressedVertices.Count; x++)
  UncompressedVertices[x].ReadChildData(reader);
for (int x=0; x<_compressedVertices.Count; x++)
{
  CompressedVertices.AddNew();
  CompressedVertices[x].Read(reader);
}
for (int x=0; x<_compressedVertices.Count; x++)
  CompressedVertices[x].ReadChildData(reader);
for (int x=0; x<_triangles.Count; x++)
{
  Triangles.AddNew();
  Triangles[x].Read(reader);
}
for (int x=0; x<_triangles.Count; x++)
  Triangles[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _shaderIndex.Write(writer);
    _prevFilthyPartIndex.Write(writer);
    _nextFilthyPartIndex.Write(writer);
    _centroidPrimaryNode.Write(writer);
    _centroidSecondaryNode.Write(writer);
    _centroidPrimaryWeight.Write(writer);
    _centroidSecondaryWeight.Write(writer);
    _centroid.Write(writer);
    _uncompressedVertices.Write(writer);
    _compressedVertices.Write(writer);
    _triangles.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_uncompressedVertices.UpdateReflexiveOffset(writer);
for (int x=0; x<_uncompressedVertices.Count; x++)
{
  UncompressedVertices[x].Write(writer);
}
for (int x=0; x<_uncompressedVertices.Count; x++)
  UncompressedVertices[x].WriteChildData(writer);
_compressedVertices.UpdateReflexiveOffset(writer);
for (int x=0; x<_compressedVertices.Count; x++)
{
  CompressedVertices[x].Write(writer);
}
for (int x=0; x<_compressedVertices.Count; x++)
  CompressedVertices[x].WriteChildData(writer);
_triangles.UpdateReflexiveOffset(writer);
for (int x=0; x<_triangles.Count; x++)
{
  Triangles[x].Write(writer);
}
for (int x=0; x<_triangles.Count; x++)
  Triangles[x].WriteChildData(writer);
}
}
public class ModelVertexUncompressedBlock : IBlock
{
private RealPoint3D _position = new RealPoint3D();
private RealVector3D _normal = new RealVector3D();
private RealVector3D _binormal = new RealVector3D();
private RealVector3D _tangent = new RealVector3D();
private RealPoint2D _textureCoords = new RealPoint2D();
private ShortInteger _node0Index = new ShortInteger();
private ShortInteger _node1Index = new ShortInteger();
private Real _node0Weight = new Real();
private Real _node1Weight = new Real();
public RealPoint3D Position
{
  get { return _position; }
  set { _position = value; }
}
public RealVector3D Normal
{
  get { return _normal; }
  set { _normal = value; }
}
public RealVector3D Binormal
{
  get { return _binormal; }
  set { _binormal = value; }
}
public RealVector3D Tangent
{
  get { return _tangent; }
  set { _tangent = value; }
}
public RealPoint2D TextureCoords
{
  get { return _textureCoords; }
  set { _textureCoords = value; }
}
public ShortInteger Node0Index
{
  get { return _node0Index; }
  set { _node0Index = value; }
}
public ShortInteger Node1Index
{
  get { return _node1Index; }
  set { _node1Index = value; }
}
public Real Node0Weight
{
  get { return _node0Weight; }
  set { _node0Weight = value; }
}
public Real Node1Weight
{
  get { return _node1Weight; }
  set { _node1Weight = value; }
}
public ModelVertexUncompressedBlock()
{

}
public void Read(BinaryReader reader)
{
  _position.Read(reader);
  _normal.Read(reader);
  _binormal.Read(reader);
  _tangent.Read(reader);
  _textureCoords.Read(reader);
  _node0Index.Read(reader);
  _node1Index.Read(reader);
  _node0Weight.Read(reader);
  _node1Weight.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _position.Write(writer);
    _normal.Write(writer);
    _binormal.Write(writer);
    _tangent.Write(writer);
    _textureCoords.Write(writer);
    _node0Index.Write(writer);
    _node1Index.Write(writer);
    _node0Weight.Write(writer);
    _node1Weight.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class ModelVertexCompressedBlock : IBlock
{
private RealPoint3D _position = new RealPoint3D();
private LongInteger _normal11_Pt11_Pt1 = new LongInteger();
private LongInteger _binormal11_Pt11_Pt1 = new LongInteger();
private LongInteger _tangent11_Pt11_Pt1 = new LongInteger();
private ShortInteger _textureCoordinateU1 = new ShortInteger();
private ShortInteger _textureCoordinateV1 = new ShortInteger();
private CharInteger _node0Indexx3 = new CharInteger();
private CharInteger _node1Indexx3 = new CharInteger();
private ShortInteger _node0Weight1 = new ShortInteger();
public RealPoint3D Position
{
  get { return _position; }
  set { _position = value; }
}
public LongInteger Normal11_Pt11_Pt1
{
  get { return _normal11_Pt11_Pt1; }
  set { _normal11_Pt11_Pt1 = value; }
}
public LongInteger Binormal11_Pt11_Pt1
{
  get { return _binormal11_Pt11_Pt1; }
  set { _binormal11_Pt11_Pt1 = value; }
}
public LongInteger Tangent11_Pt11_Pt1
{
  get { return _tangent11_Pt11_Pt1; }
  set { _tangent11_Pt11_Pt1 = value; }
}
public ShortInteger TextureCoordinateU1
{
  get { return _textureCoordinateU1; }
  set { _textureCoordinateU1 = value; }
}
public ShortInteger TextureCoordinateV1
{
  get { return _textureCoordinateV1; }
  set { _textureCoordinateV1 = value; }
}
public CharInteger Node0Indexx3
{
  get { return _node0Indexx3; }
  set { _node0Indexx3 = value; }
}
public CharInteger Node1Indexx3
{
  get { return _node1Indexx3; }
  set { _node1Indexx3 = value; }
}
public ShortInteger Node0Weight1
{
  get { return _node0Weight1; }
  set { _node0Weight1 = value; }
}
public ModelVertexCompressedBlock()
{

}
public void Read(BinaryReader reader)
{
  _position.Read(reader);
  _normal11_Pt11_Pt1.Read(reader);
  _binormal11_Pt11_Pt1.Read(reader);
  _tangent11_Pt11_Pt1.Read(reader);
  _textureCoordinateU1.Read(reader);
  _textureCoordinateV1.Read(reader);
  _node0Indexx3.Read(reader);
  _node1Indexx3.Read(reader);
  _node0Weight1.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _position.Write(writer);
    _normal11_Pt11_Pt1.Write(writer);
    _binormal11_Pt11_Pt1.Write(writer);
    _tangent11_Pt11_Pt1.Write(writer);
    _textureCoordinateU1.Write(writer);
    _textureCoordinateV1.Write(writer);
    _node0Indexx3.Write(writer);
    _node1Indexx3.Write(writer);
    _node0Weight1.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class ModelTriangleBlock : IBlock
{
private ShortInteger _vertex0Index = new ShortInteger();
private ShortInteger _vertex1Index = new ShortInteger();
private ShortInteger _vertex2Index = new ShortInteger();
public ShortInteger Vertex0Index
{
  get { return _vertex0Index; }
  set { _vertex0Index = value; }
}
public ShortInteger Vertex1Index
{
  get { return _vertex1Index; }
  set { _vertex1Index = value; }
}
public ShortInteger Vertex2Index
{
  get { return _vertex2Index; }
  set { _vertex2Index = value; }
}
public ModelTriangleBlock()
{

}
public void Read(BinaryReader reader)
{
  _vertex0Index.Read(reader);
  _vertex1Index.Read(reader);
  _vertex2Index.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _vertex0Index.Write(writer);
    _vertex1Index.Write(writer);
    _vertex2Index.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class ModelShaderReferenceBlock : IBlock
{
private TagReference _shader = new TagReference();
private ShortInteger _permutation = new ShortInteger();
private Pad  __unnamed;	
private Pad  __unnamed2;	
public TagReference Shader
{
  get { return _shader; }
  set { _shader = value; }
}
public ShortInteger Permutation
{
  get { return _permutation; }
  set { _permutation = value; }
}
public ModelShaderReferenceBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(12);

}
public void Read(BinaryReader reader)
{
  _shader.Read(reader);
  _permutation.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_shader.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _shader.Write(writer);
    _permutation.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_shader.WriteString(writer);
}
}
  }
}
