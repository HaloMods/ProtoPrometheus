using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class SoundLooping : IBlock
  {
    public SoundLoopingBlock SoundLoopingValues = new SoundLoopingBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'SoundLooping'------------------------------------------------------");
      SoundLoopingValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      SoundLoopingValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      SoundLoopingValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      SoundLoopingValues.WriteChildData(writer);
    }
public class SoundLoopingBlock : IBlock
{
private Flags  _flags;	
private Real _detailSoundPeriod = new Real();
private Pad  __unnamed;	
private Real _detailSoundPeriod2 = new Real();
private Pad  __unnamed2;	
private Pad  __unnamed3;	
private TagReference _continuousDamageEffect = new TagReference();
private Block _tracks = new Block();
private Block _detailSounds = new Block();
public class LoopingSoundTrackBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public LoopingSoundTrackBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(LoopingSoundTrackBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new LoopingSoundTrackBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public LoopingSoundTrackBlock this[int index]
  {
    get { return (InnerList[index] as LoopingSoundTrackBlock); }
  }
}
private LoopingSoundTrackBlockCollection _tracksCollection;
public LoopingSoundTrackBlockCollection Tracks
{
  get { return _tracksCollection; }
}
public class LoopingSoundDetailBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public LoopingSoundDetailBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(LoopingSoundDetailBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new LoopingSoundDetailBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public LoopingSoundDetailBlock this[int index]
  {
    get { return (InnerList[index] as LoopingSoundDetailBlock); }
  }
}
private LoopingSoundDetailBlockCollection _detailSoundsCollection;
public LoopingSoundDetailBlockCollection DetailSounds
{
  get { return _detailSoundsCollection; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Real DetailSoundPeriod
{
  get { return _detailSoundPeriod; }
  set { _detailSoundPeriod = value; }
}
public Real DetailSoundPeriod2
{
  get { return _detailSoundPeriod2; }
  set { _detailSoundPeriod2 = value; }
}
public TagReference ContinuousDamageEffect
{
  get { return _continuousDamageEffect; }
  set { _continuousDamageEffect = value; }
}
public SoundLoopingBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(8);
__unnamed2 = new Pad(8);
__unnamed3 = new Pad(16);
_tracksCollection = new LoopingSoundTrackBlockCollection(_tracks);
_detailSoundsCollection = new LoopingSoundDetailBlockCollection(_detailSounds);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _detailSoundPeriod.Read(reader);
  __unnamed.Read(reader);
  _detailSoundPeriod2.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
  _continuousDamageEffect.Read(reader);
  _tracks.Read(reader);
  _detailSounds.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_continuousDamageEffect.ReadString(reader);
for (int x=0; x<_tracks.Count; x++)
{
  Tracks.AddNew();
  Tracks[x].Read(reader);
}
for (int x=0; x<_tracks.Count; x++)
  Tracks[x].ReadChildData(reader);
for (int x=0; x<_detailSounds.Count; x++)
{
  DetailSounds.AddNew();
  DetailSounds[x].Read(reader);
}
for (int x=0; x<_detailSounds.Count; x++)
  DetailSounds[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _detailSoundPeriod.Write(writer);
    __unnamed.Write(writer);
    _detailSoundPeriod2.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
    _continuousDamageEffect.Write(writer);
    _tracks.Write(writer);
    _detailSounds.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_continuousDamageEffect.WriteString(writer);
_tracks.UpdateReflexiveOffset(writer);
for (int x=0; x<_tracks.Count; x++)
{
  Tracks[x].Write(writer);
}
for (int x=0; x<_tracks.Count; x++)
  Tracks[x].WriteChildData(writer);
_detailSounds.UpdateReflexiveOffset(writer);
for (int x=0; x<_detailSounds.Count; x++)
{
  DetailSounds[x].Write(writer);
}
for (int x=0; x<_detailSounds.Count; x++)
  DetailSounds[x].WriteChildData(writer);
}
}
public class LoopingSoundTrackBlock : IBlock
{
private Flags  _flags;	
private RealFraction _gain = new RealFraction();
private Real _fadeInDuration = new Real();
private Real _fadeOutDuration = new Real();
private Pad  __unnamed;	
private TagReference _start = new TagReference();
private TagReference _loop = new TagReference();
private TagReference _end = new TagReference();
private Pad  __unnamed2;	
private TagReference _alternateLoop = new TagReference();
private TagReference _alternateEnd = new TagReference();
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public RealFraction Gain
{
  get { return _gain; }
  set { _gain = value; }
}
public Real FadeInDuration
{
  get { return _fadeInDuration; }
  set { _fadeInDuration = value; }
}
public Real FadeOutDuration
{
  get { return _fadeOutDuration; }
  set { _fadeOutDuration = value; }
}
public TagReference Start
{
  get { return _start; }
  set { _start = value; }
}
public TagReference Loop
{
  get { return _loop; }
  set { _loop = value; }
}
public TagReference End
{
  get { return _end; }
  set { _end = value; }
}
public TagReference AlternateLoop
{
  get { return _alternateLoop; }
  set { _alternateLoop = value; }
}
public TagReference AlternateEnd
{
  get { return _alternateEnd; }
  set { _alternateEnd = value; }
}
public LoopingSoundTrackBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(32);
__unnamed2 = new Pad(32);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _gain.Read(reader);
  _fadeInDuration.Read(reader);
  _fadeOutDuration.Read(reader);
  __unnamed.Read(reader);
  _start.Read(reader);
  _loop.Read(reader);
  _end.Read(reader);
  __unnamed2.Read(reader);
  _alternateLoop.Read(reader);
  _alternateEnd.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_start.ReadString(reader);
_loop.ReadString(reader);
_end.ReadString(reader);
_alternateLoop.ReadString(reader);
_alternateEnd.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _gain.Write(writer);
    _fadeInDuration.Write(writer);
    _fadeOutDuration.Write(writer);
    __unnamed.Write(writer);
    _start.Write(writer);
    _loop.Write(writer);
    _end.Write(writer);
    __unnamed2.Write(writer);
    _alternateLoop.Write(writer);
    _alternateEnd.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_start.WriteString(writer);
_loop.WriteString(writer);
_end.WriteString(writer);
_alternateLoop.WriteString(writer);
_alternateEnd.WriteString(writer);
}
}
public class LoopingSoundDetailBlock : IBlock
{
private TagReference _sound = new TagReference();
private RealBounds _randomPeriodBounds = new RealBounds();
private RealFraction _gain = new RealFraction();
private Flags  _flags;	
private Pad  __unnamed;	
private AngleBounds _yawBounds = new AngleBounds();
private AngleBounds _pitchBounds = new AngleBounds();
private RealBounds _distanceBounds = new RealBounds();
public TagReference Sound
{
  get { return _sound; }
  set { _sound = value; }
}
public RealBounds RandomPeriodBounds
{
  get { return _randomPeriodBounds; }
  set { _randomPeriodBounds = value; }
}
public RealFraction Gain
{
  get { return _gain; }
  set { _gain = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public AngleBounds YawBounds
{
  get { return _yawBounds; }
  set { _yawBounds = value; }
}
public AngleBounds PitchBounds
{
  get { return _pitchBounds; }
  set { _pitchBounds = value; }
}
public RealBounds DistanceBounds
{
  get { return _distanceBounds; }
  set { _distanceBounds = value; }
}
public LoopingSoundDetailBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(48);

}
public void Read(BinaryReader reader)
{
  _sound.Read(reader);
  _randomPeriodBounds.Read(reader);
  _gain.Read(reader);
  _flags.Read(reader);
  __unnamed.Read(reader);
  _yawBounds.Read(reader);
  _pitchBounds.Read(reader);
  _distanceBounds.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_sound.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _sound.Write(writer);
    _randomPeriodBounds.Write(writer);
    _gain.Write(writer);
    _flags.Write(writer);
    __unnamed.Write(writer);
    _yawBounds.Write(writer);
    _pitchBounds.Write(writer);
    _distanceBounds.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_sound.WriteString(writer);
}
}
  }
}
