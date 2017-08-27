using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Sound : IBlock
  {
    public SoundBlock SoundValues = new SoundBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'Sound'------------------------------------------------------");
      SoundValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      SoundValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      SoundValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      SoundValues.WriteChildData(writer);
    }
public class SoundBlock : IBlock
{
private Flags  _flags;	
private Enum _class = new Enum();
private Enum _sampleRate = new Enum();
private Real _minimumDistance = new Real();
private Real _maximumDistance = new Real();
private RealFraction _skipFraction = new RealFraction();
private RealBounds _randomPitchBounds = new RealBounds();
private Angle _innerConeAngle = new Angle();
private Angle _outerConeAngle = new Angle();
private RealFraction _outerConeGain = new RealFraction();
private Real _gainModifier = new Real();
private Real _maximumBendPerSecond = new Real();
private Pad  __unnamed;	
private Real _skipFractionModifier = new Real();
private Real _gainModifier2 = new Real();
private Real _pitchModifier = new Real();
private Pad  __unnamed2;	
private Real _skipFractionModifier2 = new Real();
private Real _gainModifier3 = new Real();
private Real _pitchModifier2 = new Real();
private Pad  __unnamed3;	
private Enum _encoding = new Enum();
private Enum _compression = new Enum();
private TagReference _promotionSound = new TagReference();
private ShortInteger _promotionCount = new ShortInteger();
private Pad  __unnamed4;	
private Pad  __unnamed5;	
private Block _pitchRanges = new Block();
public class SoundPitchRangeBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public SoundPitchRangeBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(SoundPitchRangeBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new SoundPitchRangeBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public SoundPitchRangeBlock this[int index]
  {
    get { return (InnerList[index] as SoundPitchRangeBlock); }
  }
}
private SoundPitchRangeBlockCollection _pitchRangesCollection;
public SoundPitchRangeBlockCollection PitchRanges
{
  get { return _pitchRangesCollection; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Enum Class
{
  get { return _class; }
  set { _class = value; }
}
public Enum SampleRate
{
  get { return _sampleRate; }
  set { _sampleRate = value; }
}
public Real MinimumDistance
{
  get { return _minimumDistance; }
  set { _minimumDistance = value; }
}
public Real MaximumDistance
{
  get { return _maximumDistance; }
  set { _maximumDistance = value; }
}
public RealFraction SkipFraction
{
  get { return _skipFraction; }
  set { _skipFraction = value; }
}
public RealBounds RandomPitchBounds
{
  get { return _randomPitchBounds; }
  set { _randomPitchBounds = value; }
}
public Angle InnerConeAngle
{
  get { return _innerConeAngle; }
  set { _innerConeAngle = value; }
}
public Angle OuterConeAngle
{
  get { return _outerConeAngle; }
  set { _outerConeAngle = value; }
}
public RealFraction OuterConeGain
{
  get { return _outerConeGain; }
  set { _outerConeGain = value; }
}
public Real GainModifier
{
  get { return _gainModifier; }
  set { _gainModifier = value; }
}
public Real MaximumBendPerSecond
{
  get { return _maximumBendPerSecond; }
  set { _maximumBendPerSecond = value; }
}
public Real SkipFractionModifier
{
  get { return _skipFractionModifier; }
  set { _skipFractionModifier = value; }
}
public Real GainModifier2
{
  get { return _gainModifier2; }
  set { _gainModifier2 = value; }
}
public Real PitchModifier
{
  get { return _pitchModifier; }
  set { _pitchModifier = value; }
}
public Real SkipFractionModifier2
{
  get { return _skipFractionModifier2; }
  set { _skipFractionModifier2 = value; }
}
public Real GainModifier3
{
  get { return _gainModifier3; }
  set { _gainModifier3 = value; }
}
public Real PitchModifier2
{
  get { return _pitchModifier2; }
  set { _pitchModifier2 = value; }
}
public Enum Encoding
{
  get { return _encoding; }
  set { _encoding = value; }
}
public Enum Compression
{
  get { return _compression; }
  set { _compression = value; }
}
public TagReference PromotionSound
{
  get { return _promotionSound; }
  set { _promotionSound = value; }
}
public ShortInteger PromotionCount
{
  get { return _promotionCount; }
  set { _promotionCount = value; }
}
public SoundBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(12);
__unnamed2 = new Pad(12);
__unnamed3 = new Pad(12);
__unnamed4 = new Pad(2);
__unnamed5 = new Pad(20);
_pitchRangesCollection = new SoundPitchRangeBlockCollection(_pitchRanges);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _class.Read(reader);
  _sampleRate.Read(reader);
  _minimumDistance.Read(reader);
  _maximumDistance.Read(reader);
  _skipFraction.Read(reader);
  _randomPitchBounds.Read(reader);
  _innerConeAngle.Read(reader);
  _outerConeAngle.Read(reader);
  _outerConeGain.Read(reader);
  _gainModifier.Read(reader);
  _maximumBendPerSecond.Read(reader);
  __unnamed.Read(reader);
  _skipFractionModifier.Read(reader);
  _gainModifier2.Read(reader);
  _pitchModifier.Read(reader);
  __unnamed2.Read(reader);
  _skipFractionModifier2.Read(reader);
  _gainModifier3.Read(reader);
  _pitchModifier2.Read(reader);
  __unnamed3.Read(reader);
  _encoding.Read(reader);
  _compression.Read(reader);
  _promotionSound.Read(reader);
  _promotionCount.Read(reader);
  __unnamed4.Read(reader);
  __unnamed5.Read(reader);
  _pitchRanges.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_promotionSound.ReadString(reader);
for (int x=0; x<_pitchRanges.Count; x++)
{
  PitchRanges.AddNew();
  PitchRanges[x].Read(reader);
}
for (int x=0; x<_pitchRanges.Count; x++)
  PitchRanges[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _class.Write(writer);
    _sampleRate.Write(writer);
    _minimumDistance.Write(writer);
    _maximumDistance.Write(writer);
    _skipFraction.Write(writer);
    _randomPitchBounds.Write(writer);
    _innerConeAngle.Write(writer);
    _outerConeAngle.Write(writer);
    _outerConeGain.Write(writer);
    _gainModifier.Write(writer);
    _maximumBendPerSecond.Write(writer);
    __unnamed.Write(writer);
    _skipFractionModifier.Write(writer);
    _gainModifier2.Write(writer);
    _pitchModifier.Write(writer);
    __unnamed2.Write(writer);
    _skipFractionModifier2.Write(writer);
    _gainModifier3.Write(writer);
    _pitchModifier2.Write(writer);
    __unnamed3.Write(writer);
    _encoding.Write(writer);
    _compression.Write(writer);
    _promotionSound.Write(writer);
    _promotionCount.Write(writer);
    __unnamed4.Write(writer);
    __unnamed5.Write(writer);
    _pitchRanges.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_promotionSound.WriteString(writer);
_pitchRanges.UpdateReflexiveOffset(writer);
for (int x=0; x<_pitchRanges.Count; x++)
{
  PitchRanges[x].Write(writer);
}
for (int x=0; x<_pitchRanges.Count; x++)
  PitchRanges[x].WriteChildData(writer);
}
}
public class SoundPitchRangeBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private Real _naturalPitch = new Real();
private RealBounds _bendBounds = new RealBounds();
private ShortInteger _actualPermutationCount = new ShortInteger();
private Pad  __unnamed;	
private Pad  __unnamed2;	
private Block _permutations = new Block();
public class SoundPermutationsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public SoundPermutationsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(SoundPermutationsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new SoundPermutationsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public SoundPermutationsBlock this[int index]
  {
    get { return (InnerList[index] as SoundPermutationsBlock); }
  }
}
private SoundPermutationsBlockCollection _permutationsCollection;
public SoundPermutationsBlockCollection Permutations
{
  get { return _permutationsCollection; }
}
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public Real NaturalPitch
{
  get { return _naturalPitch; }
  set { _naturalPitch = value; }
}
public RealBounds BendBounds
{
  get { return _bendBounds; }
  set { _bendBounds = value; }
}
public ShortInteger ActualPermutationCount
{
  get { return _actualPermutationCount; }
  set { _actualPermutationCount = value; }
}
public SoundPitchRangeBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(12);
_permutationsCollection = new SoundPermutationsBlockCollection(_permutations);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _naturalPitch.Read(reader);
  _bendBounds.Read(reader);
  _actualPermutationCount.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
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
    _naturalPitch.Write(writer);
    _bendBounds.Write(writer);
    _actualPermutationCount.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
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
public class SoundPermutationsBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private RealFraction _skipFraction = new RealFraction();
private RealFraction _gain = new RealFraction();
private Enum _compression = new Enum();
private ShortInteger _nextPermutationIndex = new ShortInteger();
private Pad  __unnamed;	
private Data _samples = new Data();
private Data _mouthData = new Data();
private Data _subtitleData = new Data();
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public RealFraction SkipFraction
{
  get { return _skipFraction; }
  set { _skipFraction = value; }
}
public RealFraction Gain
{
  get { return _gain; }
  set { _gain = value; }
}
public Enum Compression
{
  get { return _compression; }
  set { _compression = value; }
}
public ShortInteger NextPermutationIndex
{
  get { return _nextPermutationIndex; }
  set { _nextPermutationIndex = value; }
}
public Data Samples
{
  get { return _samples; }
  set { _samples = value; }
}
public Data MouthData
{
  get { return _mouthData; }
  set { _mouthData = value; }
}
public Data SubtitleData
{
  get { return _subtitleData; }
  set { _subtitleData = value; }
}
public SoundPermutationsBlock()
{
__unnamed = new Pad(20);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _skipFraction.Read(reader);
  _gain.Read(reader);
  _compression.Read(reader);
  _nextPermutationIndex.Read(reader);
  __unnamed.Read(reader);
  _samples.Read(reader);
  _mouthData.Read(reader);
  _subtitleData.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_samples.ReadBinary(reader);
_mouthData.ReadBinary(reader);
_subtitleData.ReadBinary(reader);
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _skipFraction.Write(writer);
    _gain.Write(writer);
    _compression.Write(writer);
    _nextPermutationIndex.Write(writer);
    __unnamed.Write(writer);
    _samples.Write(writer);
    _mouthData.Write(writer);
    _subtitleData.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_samples.WriteBinary(writer);
_mouthData.WriteBinary(writer);
_subtitleData.WriteBinary(writer);
}
}
  }
}
