using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class LightVolume : IBlock
  {
    public LightVolumeBlock LightVolumeValues = new LightVolumeBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'LightVolume'------------------------------------------------------");
      LightVolumeValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      LightVolumeValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      LightVolumeValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      LightVolumeValues.WriteChildData(writer);
    }
public class LightVolumeBlock : IBlock
{
private FixedLengthString _attachmentMarker = new FixedLengthString();
private Pad  __unnamed;	
private Flags  _flags;	
private Pad  __unnamed2;	
private Real _nearFadeDistance = new Real();
private Real _farFadeDistance = new Real();
private RealFraction _perpendicularBrightnessScale = new RealFraction();
private RealFraction _parallelBrightnessScale = new RealFraction();
private Enum _brightnessScaleSource = new Enum();
private Pad  __unnamed3;	
private Pad  __unnamed4;	
private TagReference _map = new TagReference();
private ShortInteger _sequenceIndex = new ShortInteger();
private ShortInteger _count = new ShortInteger();
private Pad  __unnamed5;	
private Enum _frameAnimationSource = new Enum();
private Pad  __unnamed6;	
private Pad  __unnamed7;	
private Pad  __unnamed8;	
private Block _frames = new Block();
private Pad  __unnamed9;	
public class LightVolumeFrameBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public LightVolumeFrameBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(LightVolumeFrameBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new LightVolumeFrameBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public LightVolumeFrameBlock this[int index]
  {
    get { return (InnerList[index] as LightVolumeFrameBlock); }
  }
}
private LightVolumeFrameBlockCollection _framesCollection;
public LightVolumeFrameBlockCollection Frames
{
  get { return _framesCollection; }
}
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
public RealFraction PerpendicularBrightnessScale
{
  get { return _perpendicularBrightnessScale; }
  set { _perpendicularBrightnessScale = value; }
}
public RealFraction ParallelBrightnessScale
{
  get { return _parallelBrightnessScale; }
  set { _parallelBrightnessScale = value; }
}
public Enum BrightnessScaleSource
{
  get { return _brightnessScaleSource; }
  set { _brightnessScaleSource = value; }
}
public TagReference Map
{
  get { return _map; }
  set { _map = value; }
}
public ShortInteger SequenceIndex
{
  get { return _sequenceIndex; }
  set { _sequenceIndex = value; }
}
public ShortInteger Count
{
  get { return _count; }
  set { _count = value; }
}
public Enum FrameAnimationSource
{
  get { return _frameAnimationSource; }
  set { _frameAnimationSource = value; }
}
public LightVolumeBlock()
{
__unnamed = new Pad(2);
_flags = new Flags(2);
__unnamed2 = new Pad(16);
__unnamed3 = new Pad(2);
__unnamed4 = new Pad(20);
__unnamed5 = new Pad(72);
__unnamed6 = new Pad(2);
__unnamed7 = new Pad(36);
__unnamed8 = new Pad(64);
__unnamed9 = new Pad(32);
_framesCollection = new LightVolumeFrameBlockCollection(_frames);

}
public void Read(BinaryReader reader)
{
  _attachmentMarker.Read(reader);
  __unnamed.Read(reader);
  _flags.Read(reader);
  __unnamed2.Read(reader);
  _nearFadeDistance.Read(reader);
  _farFadeDistance.Read(reader);
  _perpendicularBrightnessScale.Read(reader);
  _parallelBrightnessScale.Read(reader);
  _brightnessScaleSource.Read(reader);
  __unnamed3.Read(reader);
  __unnamed4.Read(reader);
  _map.Read(reader);
  _sequenceIndex.Read(reader);
  _count.Read(reader);
  __unnamed5.Read(reader);
  _frameAnimationSource.Read(reader);
  __unnamed6.Read(reader);
  __unnamed7.Read(reader);
  __unnamed8.Read(reader);
  _frames.Read(reader);
  __unnamed9.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_map.ReadString(reader);
for (int x=0; x<_frames.Count; x++)
{
  Frames.AddNew();
  Frames[x].Read(reader);
}
for (int x=0; x<_frames.Count; x++)
  Frames[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _attachmentMarker.Write(writer);
    __unnamed.Write(writer);
    _flags.Write(writer);
    __unnamed2.Write(writer);
    _nearFadeDistance.Write(writer);
    _farFadeDistance.Write(writer);
    _perpendicularBrightnessScale.Write(writer);
    _parallelBrightnessScale.Write(writer);
    _brightnessScaleSource.Write(writer);
    __unnamed3.Write(writer);
    __unnamed4.Write(writer);
    _map.Write(writer);
    _sequenceIndex.Write(writer);
    _count.Write(writer);
    __unnamed5.Write(writer);
    _frameAnimationSource.Write(writer);
    __unnamed6.Write(writer);
    __unnamed7.Write(writer);
    __unnamed8.Write(writer);
    _frames.Write(writer);
    __unnamed9.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_map.WriteString(writer);
_frames.UpdateReflexiveOffset(writer);
for (int x=0; x<_frames.Count; x++)
{
  Frames[x].Write(writer);
}
for (int x=0; x<_frames.Count; x++)
  Frames[x].WriteChildData(writer);
}
}
public class LightVolumeFrameBlock : IBlock
{
private Pad  __unnamed;	
private Real _offsetFromMarker = new Real();
private Real _offsetExponent = new Real();
private Real _length = new Real();
private Pad  __unnamed2;	
private Real _radiusHither = new Real();
private Real _radiusYon = new Real();
private Real _radiusExponent = new Real();
private Pad  __unnamed3;	
private RealARGBColor _tintColorHither = new RealARGBColor();
private RealARGBColor _tintColorYon = new RealARGBColor();
private Real _tintColorExponent = new Real();
private Real _brightnessExponent = new Real();
private Pad  __unnamed4;	
public Real OffsetFromMarker
{
  get { return _offsetFromMarker; }
  set { _offsetFromMarker = value; }
}
public Real OffsetExponent
{
  get { return _offsetExponent; }
  set { _offsetExponent = value; }
}
public Real Length
{
  get { return _length; }
  set { _length = value; }
}
public Real RadiusHither
{
  get { return _radiusHither; }
  set { _radiusHither = value; }
}
public Real RadiusYon
{
  get { return _radiusYon; }
  set { _radiusYon = value; }
}
public Real RadiusExponent
{
  get { return _radiusExponent; }
  set { _radiusExponent = value; }
}
public RealARGBColor TintColorHither
{
  get { return _tintColorHither; }
  set { _tintColorHither = value; }
}
public RealARGBColor TintColorYon
{
  get { return _tintColorYon; }
  set { _tintColorYon = value; }
}
public Real TintColorExponent
{
  get { return _tintColorExponent; }
  set { _tintColorExponent = value; }
}
public Real BrightnessExponent
{
  get { return _brightnessExponent; }
  set { _brightnessExponent = value; }
}
public LightVolumeFrameBlock()
{
__unnamed = new Pad(16);
__unnamed2 = new Pad(32);
__unnamed3 = new Pad(32);
__unnamed4 = new Pad(32);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  _offsetFromMarker.Read(reader);
  _offsetExponent.Read(reader);
  _length.Read(reader);
  __unnamed2.Read(reader);
  _radiusHither.Read(reader);
  _radiusYon.Read(reader);
  _radiusExponent.Read(reader);
  __unnamed3.Read(reader);
  _tintColorHither.Read(reader);
  _tintColorYon.Read(reader);
  _tintColorExponent.Read(reader);
  _brightnessExponent.Read(reader);
  __unnamed4.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    _offsetFromMarker.Write(writer);
    _offsetExponent.Write(writer);
    _length.Write(writer);
    __unnamed2.Write(writer);
    _radiusHither.Write(writer);
    _radiusYon.Write(writer);
    _radiusExponent.Write(writer);
    __unnamed3.Write(writer);
    _tintColorHither.Write(writer);
    _tintColorYon.Write(writer);
    _tintColorExponent.Write(writer);
    _brightnessExponent.Write(writer);
    __unnamed4.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
  }
}
