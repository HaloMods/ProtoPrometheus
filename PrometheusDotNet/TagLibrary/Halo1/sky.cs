using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Sky : IBlock
  {
    public SkyBlock SkyValues = new SkyBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'Sky'------------------------------------------------------");
      SkyValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      SkyValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      SkyValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      SkyValues.WriteChildData(writer);
    }
public class SkyBlock : IBlock
{
private TagReference _model = new TagReference();
private TagReference _animation_graph = new TagReference();
private Pad  __unnamed;	
private RealRGBColor _indoorAmbientRadiosityColor = new RealRGBColor();
private Real _indoorAmbientRadiosityPower = new Real();
private RealRGBColor _outdoorAmbientRadiosityColor = new RealRGBColor();
private Real _outdoorAmbientRadiosityPower = new Real();
private RealRGBColor _outdoorFogColor = new RealRGBColor();
private Pad  __unnamed2;	
private RealFraction _outdoorFogMaximumDensity = new RealFraction();
private Real _outdoorFogStartDistance = new Real();
private Real _outdoorFogOpaqueDistance = new Real();
private RealRGBColor _indoorFogColor = new RealRGBColor();
private Pad  __unnamed3;	
private RealFraction _indoorFogMaximumDensity = new RealFraction();
private Real _indoorFogStartDistance = new Real();
private Real _indoorFogOpaqueDistance = new Real();
private TagReference _indoorFogScreen = new TagReference();
private Pad  __unnamed4;	
private Block _shaderFunctions = new Block();
private Block _animations = new Block();
private Block _lights = new Block();
public class SkyShaderFunctionBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public SkyShaderFunctionBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(SkyShaderFunctionBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new SkyShaderFunctionBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public SkyShaderFunctionBlock this[int index]
  {
    get { return (InnerList[index] as SkyShaderFunctionBlock); }
  }
}
private SkyShaderFunctionBlockCollection _shaderFunctionsCollection;
public SkyShaderFunctionBlockCollection ShaderFunctions
{
  get { return _shaderFunctionsCollection; }
}
public class SkyAnimationBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public SkyAnimationBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(SkyAnimationBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new SkyAnimationBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public SkyAnimationBlock this[int index]
  {
    get { return (InnerList[index] as SkyAnimationBlock); }
  }
}
private SkyAnimationBlockCollection _animationsCollection;
public SkyAnimationBlockCollection Animations
{
  get { return _animationsCollection; }
}
public class SkyLightBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public SkyLightBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(SkyLightBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new SkyLightBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public SkyLightBlock this[int index]
  {
    get { return (InnerList[index] as SkyLightBlock); }
  }
}
private SkyLightBlockCollection _lightsCollection;
public SkyLightBlockCollection Lights
{
  get { return _lightsCollection; }
}
public TagReference Model
{
  get { return _model; }
  set { _model = value; }
}
public TagReference Animation_graph
{
  get { return _animation_graph; }
  set { _animation_graph = value; }
}
public RealRGBColor IndoorAmbientRadiosityColor
{
  get { return _indoorAmbientRadiosityColor; }
  set { _indoorAmbientRadiosityColor = value; }
}
public Real IndoorAmbientRadiosityPower
{
  get { return _indoorAmbientRadiosityPower; }
  set { _indoorAmbientRadiosityPower = value; }
}
public RealRGBColor OutdoorAmbientRadiosityColor
{
  get { return _outdoorAmbientRadiosityColor; }
  set { _outdoorAmbientRadiosityColor = value; }
}
public Real OutdoorAmbientRadiosityPower
{
  get { return _outdoorAmbientRadiosityPower; }
  set { _outdoorAmbientRadiosityPower = value; }
}
public RealRGBColor OutdoorFogColor
{
  get { return _outdoorFogColor; }
  set { _outdoorFogColor = value; }
}
public RealFraction OutdoorFogMaximumDensity
{
  get { return _outdoorFogMaximumDensity; }
  set { _outdoorFogMaximumDensity = value; }
}
public Real OutdoorFogStartDistance
{
  get { return _outdoorFogStartDistance; }
  set { _outdoorFogStartDistance = value; }
}
public Real OutdoorFogOpaqueDistance
{
  get { return _outdoorFogOpaqueDistance; }
  set { _outdoorFogOpaqueDistance = value; }
}
public RealRGBColor IndoorFogColor
{
  get { return _indoorFogColor; }
  set { _indoorFogColor = value; }
}
public RealFraction IndoorFogMaximumDensity
{
  get { return _indoorFogMaximumDensity; }
  set { _indoorFogMaximumDensity = value; }
}
public Real IndoorFogStartDistance
{
  get { return _indoorFogStartDistance; }
  set { _indoorFogStartDistance = value; }
}
public Real IndoorFogOpaqueDistance
{
  get { return _indoorFogOpaqueDistance; }
  set { _indoorFogOpaqueDistance = value; }
}
public TagReference IndoorFogScreen
{
  get { return _indoorFogScreen; }
  set { _indoorFogScreen = value; }
}
public SkyBlock()
{
__unnamed = new Pad(24);
__unnamed2 = new Pad(8);
__unnamed3 = new Pad(8);
__unnamed4 = new Pad(4);
_shaderFunctionsCollection = new SkyShaderFunctionBlockCollection(_shaderFunctions);
_animationsCollection = new SkyAnimationBlockCollection(_animations);
_lightsCollection = new SkyLightBlockCollection(_lights);

}
public void Read(BinaryReader reader)
{
  _model.Read(reader);
  _animation_graph.Read(reader);
  __unnamed.Read(reader);
  _indoorAmbientRadiosityColor.Read(reader);
  _indoorAmbientRadiosityPower.Read(reader);
  _outdoorAmbientRadiosityColor.Read(reader);
  _outdoorAmbientRadiosityPower.Read(reader);
  _outdoorFogColor.Read(reader);
  __unnamed2.Read(reader);
  _outdoorFogMaximumDensity.Read(reader);
  _outdoorFogStartDistance.Read(reader);
  _outdoorFogOpaqueDistance.Read(reader);
  _indoorFogColor.Read(reader);
  __unnamed3.Read(reader);
  _indoorFogMaximumDensity.Read(reader);
  _indoorFogStartDistance.Read(reader);
  _indoorFogOpaqueDistance.Read(reader);
  _indoorFogScreen.Read(reader);
  __unnamed4.Read(reader);
  _shaderFunctions.Read(reader);
  _animations.Read(reader);
  _lights.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_model.ReadString(reader);
_animation_graph.ReadString(reader);
_indoorFogScreen.ReadString(reader);
for (int x=0; x<_shaderFunctions.Count; x++)
{
  ShaderFunctions.AddNew();
  ShaderFunctions[x].Read(reader);
}
for (int x=0; x<_shaderFunctions.Count; x++)
  ShaderFunctions[x].ReadChildData(reader);
for (int x=0; x<_animations.Count; x++)
{
  Animations.AddNew();
  Animations[x].Read(reader);
}
for (int x=0; x<_animations.Count; x++)
  Animations[x].ReadChildData(reader);
for (int x=0; x<_lights.Count; x++)
{
  Lights.AddNew();
  Lights[x].Read(reader);
}
for (int x=0; x<_lights.Count; x++)
  Lights[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _model.Write(writer);
    _animation_graph.Write(writer);
    __unnamed.Write(writer);
    _indoorAmbientRadiosityColor.Write(writer);
    _indoorAmbientRadiosityPower.Write(writer);
    _outdoorAmbientRadiosityColor.Write(writer);
    _outdoorAmbientRadiosityPower.Write(writer);
    _outdoorFogColor.Write(writer);
    __unnamed2.Write(writer);
    _outdoorFogMaximumDensity.Write(writer);
    _outdoorFogStartDistance.Write(writer);
    _outdoorFogOpaqueDistance.Write(writer);
    _indoorFogColor.Write(writer);
    __unnamed3.Write(writer);
    _indoorFogMaximumDensity.Write(writer);
    _indoorFogStartDistance.Write(writer);
    _indoorFogOpaqueDistance.Write(writer);
    _indoorFogScreen.Write(writer);
    __unnamed4.Write(writer);
    _shaderFunctions.Write(writer);
    _animations.Write(writer);
    _lights.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_model.WriteString(writer);
_animation_graph.WriteString(writer);
_indoorFogScreen.WriteString(writer);
_shaderFunctions.UpdateReflexiveOffset(writer);
for (int x=0; x<_shaderFunctions.Count; x++)
{
  ShaderFunctions[x].Write(writer);
}
for (int x=0; x<_shaderFunctions.Count; x++)
  ShaderFunctions[x].WriteChildData(writer);
_animations.UpdateReflexiveOffset(writer);
for (int x=0; x<_animations.Count; x++)
{
  Animations[x].Write(writer);
}
for (int x=0; x<_animations.Count; x++)
  Animations[x].WriteChildData(writer);
_lights.UpdateReflexiveOffset(writer);
for (int x=0; x<_lights.Count; x++)
{
  Lights[x].Write(writer);
}
for (int x=0; x<_lights.Count; x++)
  Lights[x].WriteChildData(writer);
}
}
public class SkyShaderFunctionBlock : IBlock
{
private Pad  __unnamed;	
private FixedLengthString _globalFunctionName = new FixedLengthString();
public FixedLengthString GlobalFunctionName
{
  get { return _globalFunctionName; }
  set { _globalFunctionName = value; }
}
public SkyShaderFunctionBlock()
{
__unnamed = new Pad(4);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  _globalFunctionName.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    _globalFunctionName.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class SkyAnimationBlock : IBlock
{
private ShortInteger _animationIndex = new ShortInteger();
private Pad  __unnamed;	
private Real _period = new Real();
private Pad  __unnamed2;	
public ShortInteger AnimationIndex
{
  get { return _animationIndex; }
  set { _animationIndex = value; }
}
public Real Period
{
  get { return _period; }
  set { _period = value; }
}
public SkyAnimationBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(28);

}
public void Read(BinaryReader reader)
{
  _animationIndex.Read(reader);
  __unnamed.Read(reader);
  _period.Read(reader);
  __unnamed2.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _animationIndex.Write(writer);
    __unnamed.Write(writer);
    _period.Write(writer);
    __unnamed2.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class SkyLightBlock : IBlock
{
private TagReference _lensFlare = new TagReference();
private FixedLengthString _lensFlareMarkerName = new FixedLengthString();
private Pad  __unnamed;	
private Flags  _flags;	
private RealRGBColor _color = new RealRGBColor();
private Real _power = new Real();
private Real _testDistance = new Real();
private Pad  __unnamed2;	
private RealEulerAngles2D _direction = new RealEulerAngles2D();
private Angle _diameter = new Angle();
public TagReference LensFlare
{
  get { return _lensFlare; }
  set { _lensFlare = value; }
}
public FixedLengthString LensFlareMarkerName
{
  get { return _lensFlareMarkerName; }
  set { _lensFlareMarkerName = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public RealRGBColor Color
{
  get { return _color; }
  set { _color = value; }
}
public Real Power
{
  get { return _power; }
  set { _power = value; }
}
public Real TestDistance
{
  get { return _testDistance; }
  set { _testDistance = value; }
}
public RealEulerAngles2D Direction
{
  get { return _direction; }
  set { _direction = value; }
}
public Angle Diameter
{
  get { return _diameter; }
  set { _diameter = value; }
}
public SkyLightBlock()
{
__unnamed = new Pad(28);
_flags = new Flags(4);
__unnamed2 = new Pad(4);

}
public void Read(BinaryReader reader)
{
  _lensFlare.Read(reader);
  _lensFlareMarkerName.Read(reader);
  __unnamed.Read(reader);
  _flags.Read(reader);
  _color.Read(reader);
  _power.Read(reader);
  _testDistance.Read(reader);
  __unnamed2.Read(reader);
  _direction.Read(reader);
  _diameter.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_lensFlare.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _lensFlare.Write(writer);
    _lensFlareMarkerName.Write(writer);
    __unnamed.Write(writer);
    _flags.Write(writer);
    _color.Write(writer);
    _power.Write(writer);
    _testDistance.Write(writer);
    __unnamed2.Write(writer);
    _direction.Write(writer);
    _diameter.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_lensFlare.WriteString(writer);
}
}
  }
}
