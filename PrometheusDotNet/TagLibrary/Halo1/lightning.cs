using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Lightning : IBlock
  {
    public LightningBlock LightningValues = new LightningBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'Lightning'------------------------------------------------------");
      LightningValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      LightningValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      LightningValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      LightningValues.WriteChildData(writer);
    }
public class LightningBlock : IBlock
{
private Pad  __unnamed;	
private ShortInteger _count = new ShortInteger();
private Pad  __unnamed2;	
private Real _nearFadeDistance = new Real();
private Real _farFadeDistance = new Real();
private Pad  __unnamed3;	
private Enum _jitterScaleSource = new Enum();
private Enum _thicknessScaleSource = new Enum();
private Enum _tintModulationSource = new Enum();
private Enum _brightnessScaleSource = new Enum();
private TagReference _bitmap = new TagReference();
private Pad  __unnamed4;	
private Block _markers = new Block();
private Block _shader = new Block();
private Pad  __unnamed5;	
public class LightningMarkerBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public LightningMarkerBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(LightningMarkerBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new LightningMarkerBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public LightningMarkerBlock this[int index]
  {
    get { return (InnerList[index] as LightningMarkerBlock); }
  }
}
private LightningMarkerBlockCollection _markersCollection;
public LightningMarkerBlockCollection Markers
{
  get { return _markersCollection; }
}
public class LightningShaderBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public LightningShaderBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(LightningShaderBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new LightningShaderBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public LightningShaderBlock this[int index]
  {
    get { return (InnerList[index] as LightningShaderBlock); }
  }
}
private LightningShaderBlockCollection _shaderCollection;
public LightningShaderBlockCollection Shader
{
  get { return _shaderCollection; }
}
public ShortInteger Count
{
  get { return _count; }
  set { _count = value; }
}
public Real NearFadeDistance
{
  get { return _nearFadeDistance; }
  set { _nearFadeDistance = value; }
}
public Real FarFadeDistance
{
  get { return _farFadeDistance; }
  set { _farFadeDistance = value; }
}
public Enum JitterScaleSource
{
  get { return _jitterScaleSource; }
  set { _jitterScaleSource = value; }
}
public Enum ThicknessScaleSource
{
  get { return _thicknessScaleSource; }
  set { _thicknessScaleSource = value; }
}
public Enum TintModulationSource
{
  get { return _tintModulationSource; }
  set { _tintModulationSource = value; }
}
public Enum BrightnessScaleSource
{
  get { return _brightnessScaleSource; }
  set { _brightnessScaleSource = value; }
}
public TagReference Bitmap
{
  get { return _bitmap; }
  set { _bitmap = value; }
}
public LightningBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(16);
__unnamed3 = new Pad(16);
__unnamed4 = new Pad(84);
__unnamed5 = new Pad(88);
_markersCollection = new LightningMarkerBlockCollection(_markers);
_shaderCollection = new LightningShaderBlockCollection(_shader);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  _count.Read(reader);
  __unnamed2.Read(reader);
  _nearFadeDistance.Read(reader);
  _farFadeDistance.Read(reader);
  __unnamed3.Read(reader);
  _jitterScaleSource.Read(reader);
  _thicknessScaleSource.Read(reader);
  _tintModulationSource.Read(reader);
  _brightnessScaleSource.Read(reader);
  _bitmap.Read(reader);
  __unnamed4.Read(reader);
  _markers.Read(reader);
  _shader.Read(reader);
  __unnamed5.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_bitmap.ReadString(reader);
for (int x=0; x<_markers.Count; x++)
{
  Markers.AddNew();
  Markers[x].Read(reader);
}
for (int x=0; x<_markers.Count; x++)
  Markers[x].ReadChildData(reader);
for (int x=0; x<_shader.Count; x++)
{
  Shader.AddNew();
  Shader[x].Read(reader);
}
for (int x=0; x<_shader.Count; x++)
  Shader[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    _count.Write(writer);
    __unnamed2.Write(writer);
    _nearFadeDistance.Write(writer);
    _farFadeDistance.Write(writer);
    __unnamed3.Write(writer);
    _jitterScaleSource.Write(writer);
    _thicknessScaleSource.Write(writer);
    _tintModulationSource.Write(writer);
    _brightnessScaleSource.Write(writer);
    _bitmap.Write(writer);
    __unnamed4.Write(writer);
    _markers.Write(writer);
    _shader.Write(writer);
    __unnamed5.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_bitmap.WriteString(writer);
_markers.UpdateReflexiveOffset(writer);
for (int x=0; x<_markers.Count; x++)
{
  Markers[x].Write(writer);
}
for (int x=0; x<_markers.Count; x++)
  Markers[x].WriteChildData(writer);
_shader.UpdateReflexiveOffset(writer);
for (int x=0; x<_shader.Count; x++)
{
  Shader[x].Write(writer);
}
for (int x=0; x<_shader.Count; x++)
  Shader[x].WriteChildData(writer);
}
}
public class LightningMarkerBlock : IBlock
{
private FixedLengthString _attachmentMarker = new FixedLengthString();
private Flags  _flags;	
private Pad  __unnamed;	
private ShortInteger _octavesToNextMarker = new ShortInteger();
private Pad  __unnamed2;	
private Pad  __unnamed3;	
private RealVector3D _randomPositionBounds = new RealVector3D();
private Real _randomJitter = new Real();
private Real _thickness = new Real();
private RealARGBColor _tint = new RealARGBColor();
private Pad  __unnamed4;	
public FixedLengthString AttachmentMarker
{
  get { return _attachmentMarker; }
  set { _attachmentMarker = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public ShortInteger OctavesToNextMarker
{
  get { return _octavesToNextMarker; }
  set { _octavesToNextMarker = value; }
}
public RealVector3D RandomPositionBounds
{
  get { return _randomPositionBounds; }
  set { _randomPositionBounds = value; }
}
public Real RandomJitter
{
  get { return _randomJitter; }
  set { _randomJitter = value; }
}
public Real Thickness
{
  get { return _thickness; }
  set { _thickness = value; }
}
public RealARGBColor Tint
{
  get { return _tint; }
  set { _tint = value; }
}
public LightningMarkerBlock()
{
_flags = new Flags(2);
__unnamed = new Pad(2);
__unnamed2 = new Pad(2);
__unnamed3 = new Pad(76);
__unnamed4 = new Pad(76);

}
public void Read(BinaryReader reader)
{
  _attachmentMarker.Read(reader);
  _flags.Read(reader);
  __unnamed.Read(reader);
  _octavesToNextMarker.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  _randomPositionBounds.Read(reader);
  _randomJitter.Read(reader);
  _thickness.Read(reader);
  _tint.Read(reader);
  __unnamed4.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _attachmentMarker.Write(writer);
    _flags.Write(writer);
    __unnamed.Write(writer);
    _octavesToNextMarker.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    _randomPositionBounds.Write(writer);
    _randomJitter.Write(writer);
    _thickness.Write(writer);
    _tint.Write(writer);
    __unnamed4.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class LightningShaderBlock : IBlock
{
private Pad  __unnamed;	
private Flags  _shaderFlags;	
private Enum _framebufferBlendFunction = new Enum();
private Enum _framebufferFadeMode = new Enum();
private Flags  _mapFlags;	
private Pad  __unnamed2;	
private Pad  __unnamed3;	
private Pad  __unnamed4;	
private Pad  __unnamed5;	
private Pad  __unnamed6;	
private Pad  __unnamed7;	
public Flags ShaderFlags
{
  get { return _shaderFlags; }
  set { _shaderFlags = value; }
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
public Flags MapFlags
{
  get { return _mapFlags; }
  set { _mapFlags = value; }
}
public LightningShaderBlock()
{
__unnamed = new Pad(40);
_shaderFlags = new Flags(2);
_mapFlags = new Flags(2);
__unnamed2 = new Pad(28);
__unnamed3 = new Pad(16);
__unnamed4 = new Pad(2);
__unnamed5 = new Pad(2);
__unnamed6 = new Pad(56);
__unnamed7 = new Pad(28);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  _shaderFlags.Read(reader);
  _framebufferBlendFunction.Read(reader);
  _framebufferFadeMode.Read(reader);
  _mapFlags.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  __unnamed4.Read(reader);
  __unnamed5.Read(reader);
  __unnamed6.Read(reader);
  __unnamed7.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    _shaderFlags.Write(writer);
    _framebufferBlendFunction.Write(writer);
    _framebufferFadeMode.Write(writer);
    _mapFlags.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    __unnamed4.Write(writer);
    __unnamed5.Write(writer);
    __unnamed6.Write(writer);
    __unnamed7.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
  }
}
