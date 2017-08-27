using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class ShaderTransparentGeneric : Shader
  {
    public ShaderTransparentGenericBlock ShaderTransparentGenericValues = new ShaderTransparentGenericBlock();
    public new void Read(BinaryReader reader)
    {
      base.Read(reader);
      Trace.WriteLine("Loading 'ShaderTransparentGeneric'------------------------------------------------------");
      ShaderTransparentGenericValues.Read(reader);
    }
    public new void ReadChildData(BinaryReader reader)
    {
      base.ReadChildData(reader);
      ShaderTransparentGenericValues.ReadChildData(reader);
    }
    public new void Write(BinaryWriter writer)
    {
      base.Write(writer);
      ShaderTransparentGenericValues.Write(writer);
    }
    public new void WriteChildData(BinaryWriter writer)
    {
      base.WriteChildData(writer);
      ShaderTransparentGenericValues.WriteChildData(writer);
    }
public class ShaderTransparentGenericBlock : IBlock
{
private CharInteger _numericCounterLimit = new CharInteger();
private Flags  _flags;	
private Enum _firstMapType = new Enum();
private Enum _framebufferBlendFunction = new Enum();
private Enum _framebufferFadeMode = new Enum();
private Enum _framebufferFadeSource = new Enum();
private Pad  __unnamed;	
private Real _lensFlareSpacing = new Real();
private TagReference _lensFlare = new TagReference();
private Block _extraLayers = new Block();
private Block _maps = new Block();
private Block _stages = new Block();
public class ShaderTransparentLayerBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ShaderTransparentLayerBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ShaderTransparentLayerBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ShaderTransparentLayerBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ShaderTransparentLayerBlock this[int index]
  {
    get { return (InnerList[index] as ShaderTransparentLayerBlock); }
  }
}
private ShaderTransparentLayerBlockCollection _extraLayersCollection;
public ShaderTransparentLayerBlockCollection ExtraLayers
{
  get { return _extraLayersCollection; }
}
public class ShaderTransparentGenericMapBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ShaderTransparentGenericMapBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ShaderTransparentGenericMapBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ShaderTransparentGenericMapBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ShaderTransparentGenericMapBlock this[int index]
  {
    get { return (InnerList[index] as ShaderTransparentGenericMapBlock); }
  }
}
private ShaderTransparentGenericMapBlockCollection _mapsCollection;
public ShaderTransparentGenericMapBlockCollection Maps
{
  get { return _mapsCollection; }
}
public class ShaderTransparentGenericStageBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ShaderTransparentGenericStageBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ShaderTransparentGenericStageBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ShaderTransparentGenericStageBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ShaderTransparentGenericStageBlock this[int index]
  {
    get { return (InnerList[index] as ShaderTransparentGenericStageBlock); }
  }
}
private ShaderTransparentGenericStageBlockCollection _stagesCollection;
public ShaderTransparentGenericStageBlockCollection Stages
{
  get { return _stagesCollection; }
}
public CharInteger NumericCounterLimit
{
  get { return _numericCounterLimit; }
  set { _numericCounterLimit = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Enum FirstMapType
{
  get { return _firstMapType; }
  set { _firstMapType = value; }
}
public Enum FramebufferBlendFunction
{
  get { return _framebufferBlendFunction; }
  set { _framebufferBlendFunction = value; }
}
public Enum FramebufferFadeMode
{
  get { return _framebufferFadeMode; }
  set { _framebufferFadeMode = value; }
}
public Enum FramebufferFadeSource
{
  get { return _framebufferFadeSource; }
  set { _framebufferFadeSource = value; }
}
public Real LensFlareSpacing
{
  get { return _lensFlareSpacing; }
  set { _lensFlareSpacing = value; }
}
public TagReference LensFlare
{
  get { return _lensFlare; }
  set { _lensFlare = value; }
}
public ShaderTransparentGenericBlock()
{
_flags = new Flags(1);
__unnamed = new Pad(2);
_extraLayersCollection = new ShaderTransparentLayerBlockCollection(_extraLayers);
_mapsCollection = new ShaderTransparentGenericMapBlockCollection(_maps);
_stagesCollection = new ShaderTransparentGenericStageBlockCollection(_stages);

}
public void Read(BinaryReader reader)
{
  _numericCounterLimit.Read(reader);
  _flags.Read(reader);
  _firstMapType.Read(reader);
  _framebufferBlendFunction.Read(reader);
  _framebufferFadeMode.Read(reader);
  _framebufferFadeSource.Read(reader);
  __unnamed.Read(reader);
  _lensFlareSpacing.Read(reader);
  _lensFlare.Read(reader);
  _extraLayers.Read(reader);
  _maps.Read(reader);
  _stages.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_lensFlare.ReadString(reader);
for (int x=0; x<_extraLayers.Count; x++)
{
  ExtraLayers.AddNew();
  ExtraLayers[x].Read(reader);
}
for (int x=0; x<_extraLayers.Count; x++)
  ExtraLayers[x].ReadChildData(reader);
for (int x=0; x<_maps.Count; x++)
{
  Maps.AddNew();
  Maps[x].Read(reader);
}
for (int x=0; x<_maps.Count; x++)
  Maps[x].ReadChildData(reader);
for (int x=0; x<_stages.Count; x++)
{
  Stages.AddNew();
  Stages[x].Read(reader);
}
for (int x=0; x<_stages.Count; x++)
  Stages[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _numericCounterLimit.Write(writer);
    _flags.Write(writer);
    _firstMapType.Write(writer);
    _framebufferBlendFunction.Write(writer);
    _framebufferFadeMode.Write(writer);
    _framebufferFadeSource.Write(writer);
    __unnamed.Write(writer);
    _lensFlareSpacing.Write(writer);
    _lensFlare.Write(writer);
    _extraLayers.Write(writer);
    _maps.Write(writer);
    _stages.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_lensFlare.WriteString(writer);
_extraLayers.UpdateReflexiveOffset(writer);
for (int x=0; x<_extraLayers.Count; x++)
{
  ExtraLayers[x].Write(writer);
}
for (int x=0; x<_extraLayers.Count; x++)
  ExtraLayers[x].WriteChildData(writer);
_maps.UpdateReflexiveOffset(writer);
for (int x=0; x<_maps.Count; x++)
{
  Maps[x].Write(writer);
}
for (int x=0; x<_maps.Count; x++)
  Maps[x].WriteChildData(writer);
_stages.UpdateReflexiveOffset(writer);
for (int x=0; x<_stages.Count; x++)
{
  Stages[x].Write(writer);
}
for (int x=0; x<_stages.Count; x++)
  Stages[x].WriteChildData(writer);
}
}
public class ShaderTransparentLayerBlock : IBlock
{
private TagReference _shader = new TagReference();
public TagReference Shader
{
  get { return _shader; }
  set { _shader = value; }
}
public ShaderTransparentLayerBlock()
{

}
public void Read(BinaryReader reader)
{
  _shader.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_shader.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _shader.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_shader.WriteString(writer);
}
}
public class ShaderTransparentGenericMapBlock : IBlock
{
private Flags  _flags;	
private Pad  __unnamed;	
private Real _map = new Real();
private Real _map2 = new Real();
private Real _map3 = new Real();
private Real _map4 = new Real();
private Real _mapRotation = new Real();
private RealFraction _mipmapBias = new RealFraction();
private TagReference _map5 = new TagReference();
private Enum __unnamed2 = new Enum();
private Enum __unnamed3 = new Enum();
private Real __unnamed4 = new Real();
private Real __unnamed5 = new Real();
private Real __unnamed6 = new Real();
private Enum __unnamed7 = new Enum();
private Enum __unnamed8 = new Enum();
private Real __unnamed9 = new Real();
private Real __unnamed10 = new Real();
private Real __unnamed11 = new Real();
private Enum _rotatio = new Enum();
private Enum _rotatio2 = new Enum();
private Real _rotatio3 = new Real();
private Real _rotatio4 = new Real();
private Real _rotatio5 = new Real();
private RealPoint2D _rotatio6 = new RealPoint2D();
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Real Map
{
  get { return _map; }
  set { _map = value; }
}
public Real Map2
{
  get { return _map2; }
  set { _map2 = value; }
}
public Real Map3
{
  get { return _map3; }
  set { _map3 = value; }
}
public Real Map4
{
  get { return _map4; }
  set { _map4 = value; }
}
public Real MapRotation
{
  get { return _mapRotation; }
  set { _mapRotation = value; }
}
public RealFraction MipmapBias
{
  get { return _mipmapBias; }
  set { _mipmapBias = value; }
}
public TagReference Map5
{
  get { return _map5; }
  set { _map5 = value; }
}
public Enum _unnamed2
{
  get { return __unnamed2; }
  set { __unnamed2 = value; }
}
public Enum _unnamed3
{
  get { return __unnamed3; }
  set { __unnamed3 = value; }
}
public Real _unnamed4
{
  get { return __unnamed4; }
  set { __unnamed4 = value; }
}
public Real _unnamed5
{
  get { return __unnamed5; }
  set { __unnamed5 = value; }
}
public Real _unnamed6
{
  get { return __unnamed6; }
  set { __unnamed6 = value; }
}
public Enum _unnamed7
{
  get { return __unnamed7; }
  set { __unnamed7 = value; }
}
public Enum _unnamed8
{
  get { return __unnamed8; }
  set { __unnamed8 = value; }
}
public Real _unnamed9
{
  get { return __unnamed9; }
  set { __unnamed9 = value; }
}
public Real _unnamed10
{
  get { return __unnamed10; }
  set { __unnamed10 = value; }
}
public Real _unnamed11
{
  get { return __unnamed11; }
  set { __unnamed11 = value; }
}
public Enum Rotatio
{
  get { return _rotatio; }
  set { _rotatio = value; }
}
public Enum Rotatio2
{
  get { return _rotatio2; }
  set { _rotatio2 = value; }
}
public Real Rotatio3
{
  get { return _rotatio3; }
  set { _rotatio3 = value; }
}
public Real Rotatio4
{
  get { return _rotatio4; }
  set { _rotatio4 = value; }
}
public Real Rotatio5
{
  get { return _rotatio5; }
  set { _rotatio5 = value; }
}
public RealPoint2D Rotatio6
{
  get { return _rotatio6; }
  set { _rotatio6 = value; }
}
public ShaderTransparentGenericMapBlock()
{
_flags = new Flags(2);
__unnamed = new Pad(2);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  __unnamed.Read(reader);
  _map.Read(reader);
  _map2.Read(reader);
  _map3.Read(reader);
  _map4.Read(reader);
  _mapRotation.Read(reader);
  _mipmapBias.Read(reader);
  _map5.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  __unnamed4.Read(reader);
  __unnamed5.Read(reader);
  __unnamed6.Read(reader);
  __unnamed7.Read(reader);
  __unnamed8.Read(reader);
  __unnamed9.Read(reader);
  __unnamed10.Read(reader);
  __unnamed11.Read(reader);
  _rotatio.Read(reader);
  _rotatio2.Read(reader);
  _rotatio3.Read(reader);
  _rotatio4.Read(reader);
  _rotatio5.Read(reader);
  _rotatio6.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_map5.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    __unnamed.Write(writer);
    _map.Write(writer);
    _map2.Write(writer);
    _map3.Write(writer);
    _map4.Write(writer);
    _mapRotation.Write(writer);
    _mipmapBias.Write(writer);
    _map5.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    __unnamed4.Write(writer);
    __unnamed5.Write(writer);
    __unnamed6.Write(writer);
    __unnamed7.Write(writer);
    __unnamed8.Write(writer);
    __unnamed9.Write(writer);
    __unnamed10.Write(writer);
    __unnamed11.Write(writer);
    _rotatio.Write(writer);
    _rotatio2.Write(writer);
    _rotatio3.Write(writer);
    _rotatio4.Write(writer);
    _rotatio5.Write(writer);
    _rotatio6.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_map5.WriteString(writer);
}
}
public class ShaderTransparentGenericStageBlock : IBlock
{
private Flags  _flags;	
private Pad  __unnamed;	
private Enum _color0Source = new Enum();
private Enum _color0AnimationFunction = new Enum();
private Real _color0AnimationPeriod = new Real();
private RealARGBColor _color0AnimationLowerBound = new RealARGBColor();
private RealARGBColor _color0AnimationUpperBound = new RealARGBColor();
private RealARGBColor _color1 = new RealARGBColor();
private Enum _inputA = new Enum();
private Enum _inputAMapping = new Enum();
private Enum _inputB = new Enum();
private Enum _inputBMapping = new Enum();
private Enum _inputC = new Enum();
private Enum _inputCMapping = new Enum();
private Enum _inputD = new Enum();
private Enum _inputDMapping = new Enum();
private Enum _outputAB = new Enum();
private Enum _outputABFunction = new Enum();
private Enum _outputCD = new Enum();
private Enum _outputCDFunction = new Enum();
private Enum _outputABCDMuxsum = new Enum();
private Enum _outputMapping = new Enum();
private Enum _inputA2 = new Enum();
private Enum _inputAMapping2 = new Enum();
private Enum _inputB2 = new Enum();
private Enum _inputBMapping2 = new Enum();
private Enum _inputC2 = new Enum();
private Enum _inputCMapping2 = new Enum();
private Enum _inputD2 = new Enum();
private Enum _inputDMapping2 = new Enum();
private Enum _outputAB2 = new Enum();
private Enum _outputCD2 = new Enum();
private Enum _outputABCDMuxsum2 = new Enum();
private Enum _outputMapping2 = new Enum();
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Enum Color0Source
{
  get { return _color0Source; }
  set { _color0Source = value; }
}
public Enum Color0AnimationFunction
{
  get { return _color0AnimationFunction; }
  set { _color0AnimationFunction = value; }
}
public Real Color0AnimationPeriod
{
  get { return _color0AnimationPeriod; }
  set { _color0AnimationPeriod = value; }
}
public RealARGBColor Color0AnimationLowerBound
{
  get { return _color0AnimationLowerBound; }
  set { _color0AnimationLowerBound = value; }
}
public RealARGBColor Color0AnimationUpperBound
{
  get { return _color0AnimationUpperBound; }
  set { _color0AnimationUpperBound = value; }
}
public RealARGBColor Color1
{
  get { return _color1; }
  set { _color1 = value; }
}
public Enum InputA
{
  get { return _inputA; }
  set { _inputA = value; }
}
public Enum InputAMapping
{
  get { return _inputAMapping; }
  set { _inputAMapping = value; }
}
public Enum InputB
{
  get { return _inputB; }
  set { _inputB = value; }
}
public Enum InputBMapping
{
  get { return _inputBMapping; }
  set { _inputBMapping = value; }
}
public Enum InputC
{
  get { return _inputC; }
  set { _inputC = value; }
}
public Enum InputCMapping
{
  get { return _inputCMapping; }
  set { _inputCMapping = value; }
}
public Enum InputD
{
  get { return _inputD; }
  set { _inputD = value; }
}
public Enum InputDMapping
{
  get { return _inputDMapping; }
  set { _inputDMapping = value; }
}
public Enum OutputAB
{
  get { return _outputAB; }
  set { _outputAB = value; }
}
public Enum OutputABFunction
{
  get { return _outputABFunction; }
  set { _outputABFunction = value; }
}
public Enum OutputCD
{
  get { return _outputCD; }
  set { _outputCD = value; }
}
public Enum OutputCDFunction
{
  get { return _outputCDFunction; }
  set { _outputCDFunction = value; }
}
public Enum OutputABCDMuxsum
{
  get { return _outputABCDMuxsum; }
  set { _outputABCDMuxsum = value; }
}
public Enum OutputMapping
{
  get { return _outputMapping; }
  set { _outputMapping = value; }
}
public Enum InputA2
{
  get { return _inputA2; }
  set { _inputA2 = value; }
}
public Enum InputAMapping2
{
  get { return _inputAMapping2; }
  set { _inputAMapping2 = value; }
}
public Enum InputB2
{
  get { return _inputB2; }
  set { _inputB2 = value; }
}
public Enum InputBMapping2
{
  get { return _inputBMapping2; }
  set { _inputBMapping2 = value; }
}
public Enum InputC2
{
  get { return _inputC2; }
  set { _inputC2 = value; }
}
public Enum InputCMapping2
{
  get { return _inputCMapping2; }
  set { _inputCMapping2 = value; }
}
public Enum InputD2
{
  get { return _inputD2; }
  set { _inputD2 = value; }
}
public Enum InputDMapping2
{
  get { return _inputDMapping2; }
  set { _inputDMapping2 = value; }
}
public Enum OutputAB2
{
  get { return _outputAB2; }
  set { _outputAB2 = value; }
}
public Enum OutputCD2
{
  get { return _outputCD2; }
  set { _outputCD2 = value; }
}
public Enum OutputABCDMuxsum2
{
  get { return _outputABCDMuxsum2; }
  set { _outputABCDMuxsum2 = value; }
}
public Enum OutputMapping2
{
  get { return _outputMapping2; }
  set { _outputMapping2 = value; }
}
public ShaderTransparentGenericStageBlock()
{
_flags = new Flags(2);
__unnamed = new Pad(2);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  __unnamed.Read(reader);
  _color0Source.Read(reader);
  _color0AnimationFunction.Read(reader);
  _color0AnimationPeriod.Read(reader);
  _color0AnimationLowerBound.Read(reader);
  _color0AnimationUpperBound.Read(reader);
  _color1.Read(reader);
  _inputA.Read(reader);
  _inputAMapping.Read(reader);
  _inputB.Read(reader);
  _inputBMapping.Read(reader);
  _inputC.Read(reader);
  _inputCMapping.Read(reader);
  _inputD.Read(reader);
  _inputDMapping.Read(reader);
  _outputAB.Read(reader);
  _outputABFunction.Read(reader);
  _outputCD.Read(reader);
  _outputCDFunction.Read(reader);
  _outputABCDMuxsum.Read(reader);
  _outputMapping.Read(reader);
  _inputA2.Read(reader);
  _inputAMapping2.Read(reader);
  _inputB2.Read(reader);
  _inputBMapping2.Read(reader);
  _inputC2.Read(reader);
  _inputCMapping2.Read(reader);
  _inputD2.Read(reader);
  _inputDMapping2.Read(reader);
  _outputAB2.Read(reader);
  _outputCD2.Read(reader);
  _outputABCDMuxsum2.Read(reader);
  _outputMapping2.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    __unnamed.Write(writer);
    _color0Source.Write(writer);
    _color0AnimationFunction.Write(writer);
    _color0AnimationPeriod.Write(writer);
    _color0AnimationLowerBound.Write(writer);
    _color0AnimationUpperBound.Write(writer);
    _color1.Write(writer);
    _inputA.Write(writer);
    _inputAMapping.Write(writer);
    _inputB.Write(writer);
    _inputBMapping.Write(writer);
    _inputC.Write(writer);
    _inputCMapping.Write(writer);
    _inputD.Write(writer);
    _inputDMapping.Write(writer);
    _outputAB.Write(writer);
    _outputABFunction.Write(writer);
    _outputCD.Write(writer);
    _outputCDFunction.Write(writer);
    _outputABCDMuxsum.Write(writer);
    _outputMapping.Write(writer);
    _inputA2.Write(writer);
    _inputAMapping2.Write(writer);
    _inputB2.Write(writer);
    _inputBMapping2.Write(writer);
    _inputC2.Write(writer);
    _inputCMapping2.Write(writer);
    _inputD2.Write(writer);
    _inputDMapping2.Write(writer);
    _outputAB2.Write(writer);
    _outputCD2.Write(writer);
    _outputABCDMuxsum2.Write(writer);
    _outputMapping2.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
  }
}
