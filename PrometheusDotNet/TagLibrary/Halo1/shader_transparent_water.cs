using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class ShaderTransparentWater : Shader
  {
    public ShaderTransparentWaterBlock ShaderTransparentWaterValues = new ShaderTransparentWaterBlock();
    public new void Read(BinaryReader reader)
    {
      base.Read(reader);
      Trace.WriteLine("Loading 'ShaderTransparentWater'------------------------------------------------------");
      ShaderTransparentWaterValues.Read(reader);
    }
    public new void ReadChildData(BinaryReader reader)
    {
      base.ReadChildData(reader);
      ShaderTransparentWaterValues.ReadChildData(reader);
    }
    public new void Write(BinaryWriter writer)
    {
      base.Write(writer);
      ShaderTransparentWaterValues.Write(writer);
    }
    public new void WriteChildData(BinaryWriter writer)
    {
      base.WriteChildData(writer);
      ShaderTransparentWaterValues.WriteChildData(writer);
    }
public class ShaderTransparentWaterBlock : IBlock
{
private Flags  _flags;	
private Pad  __unnamed;	
private Pad  __unnamed2;	
private TagReference _baseMap = new TagReference();
private Pad  __unnamed3;	
private RealFraction _viewPerpendicularBrightness = new RealFraction();
private RealRGBColor _viewPerpendicularTintColor = new RealRGBColor();
private RealFraction _viewParallelBrightness = new RealFraction();
private RealRGBColor _viewParallelTintColor = new RealRGBColor();
private Pad  __unnamed4;	
private TagReference _reflectionMap = new TagReference();
private Pad  __unnamed5;	
private Angle _rippleAnimationAngle = new Angle();
private Real _rippleAnimationVelocity = new Real();
private Real _rippleScale = new Real();
private TagReference _rippleMaps = new TagReference();
private ShortInteger _rippleMipmapLevels = new ShortInteger();
private Pad  __unnamed6;	
private RealFraction _rippleMipmapFadeFactor = new RealFraction();
private Real _rippleMipmapDetailBias = new Real();
private Pad  __unnamed7;	
private Block _ripples = new Block();
private Pad  __unnamed8;	
public class ShaderTransparentWaterRippleBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ShaderTransparentWaterRippleBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ShaderTransparentWaterRippleBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ShaderTransparentWaterRippleBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ShaderTransparentWaterRippleBlock this[int index]
  {
    get { return (InnerList[index] as ShaderTransparentWaterRippleBlock); }
  }
}
private ShaderTransparentWaterRippleBlockCollection _ripplesCollection;
public ShaderTransparentWaterRippleBlockCollection Ripples
{
  get { return _ripplesCollection; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public TagReference BaseMap
{
  get { return _baseMap; }
  set { _baseMap = value; }
}
public RealFraction ViewPerpendicularBrightness
{
  get { return _viewPerpendicularBrightness; }
  set { _viewPerpendicularBrightness = value; }
}
public RealRGBColor ViewPerpendicularTintColor
{
  get { return _viewPerpendicularTintColor; }
  set { _viewPerpendicularTintColor = value; }
}
public RealFraction ViewParallelBrightness
{
  get { return _viewParallelBrightness; }
  set { _viewParallelBrightness = value; }
}
public RealRGBColor ViewParallelTintColor
{
  get { return _viewParallelTintColor; }
  set { _viewParallelTintColor = value; }
}
public TagReference ReflectionMap
{
  get { return _reflectionMap; }
  set { _reflectionMap = value; }
}
public Angle RippleAnimationAngle
{
  get { return _rippleAnimationAngle; }
  set { _rippleAnimationAngle = value; }
}
public Real RippleAnimationVelocity
{
  get { return _rippleAnimationVelocity; }
  set { _rippleAnimationVelocity = value; }
}
public Real RippleScale
{
  get { return _rippleScale; }
  set { _rippleScale = value; }
}
public TagReference RippleMaps
{
  get { return _rippleMaps; }
  set { _rippleMaps = value; }
}
public ShortInteger RippleMipmapLevels
{
  get { return _rippleMipmapLevels; }
  set { _rippleMipmapLevels = value; }
}
public RealFraction RippleMipmapFadeFactor
{
  get { return _rippleMipmapFadeFactor; }
  set { _rippleMipmapFadeFactor = value; }
}
public Real RippleMipmapDetailBias
{
  get { return _rippleMipmapDetailBias; }
  set { _rippleMipmapDetailBias = value; }
}
public ShaderTransparentWaterBlock()
{
_flags = new Flags(2);
__unnamed = new Pad(2);
__unnamed2 = new Pad(32);
__unnamed3 = new Pad(16);
__unnamed4 = new Pad(16);
__unnamed5 = new Pad(16);
__unnamed6 = new Pad(2);
__unnamed7 = new Pad(64);
__unnamed8 = new Pad(16);
_ripplesCollection = new ShaderTransparentWaterRippleBlockCollection(_ripples);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  _baseMap.Read(reader);
  __unnamed3.Read(reader);
  _viewPerpendicularBrightness.Read(reader);
  _viewPerpendicularTintColor.Read(reader);
  _viewParallelBrightness.Read(reader);
  _viewParallelTintColor.Read(reader);
  __unnamed4.Read(reader);
  _reflectionMap.Read(reader);
  __unnamed5.Read(reader);
  _rippleAnimationAngle.Read(reader);
  _rippleAnimationVelocity.Read(reader);
  _rippleScale.Read(reader);
  _rippleMaps.Read(reader);
  _rippleMipmapLevels.Read(reader);
  __unnamed6.Read(reader);
  _rippleMipmapFadeFactor.Read(reader);
  _rippleMipmapDetailBias.Read(reader);
  __unnamed7.Read(reader);
  _ripples.Read(reader);
  __unnamed8.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_baseMap.ReadString(reader);
_reflectionMap.ReadString(reader);
_rippleMaps.ReadString(reader);
for (int x=0; x<_ripples.Count; x++)
{
  Ripples.AddNew();
  Ripples[x].Read(reader);
}
for (int x=0; x<_ripples.Count; x++)
  Ripples[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    _baseMap.Write(writer);
    __unnamed3.Write(writer);
    _viewPerpendicularBrightness.Write(writer);
    _viewPerpendicularTintColor.Write(writer);
    _viewParallelBrightness.Write(writer);
    _viewParallelTintColor.Write(writer);
    __unnamed4.Write(writer);
    _reflectionMap.Write(writer);
    __unnamed5.Write(writer);
    _rippleAnimationAngle.Write(writer);
    _rippleAnimationVelocity.Write(writer);
    _rippleScale.Write(writer);
    _rippleMaps.Write(writer);
    _rippleMipmapLevels.Write(writer);
    __unnamed6.Write(writer);
    _rippleMipmapFadeFactor.Write(writer);
    _rippleMipmapDetailBias.Write(writer);
    __unnamed7.Write(writer);
    _ripples.Write(writer);
    __unnamed8.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_baseMap.WriteString(writer);
_reflectionMap.WriteString(writer);
_rippleMaps.WriteString(writer);
_ripples.UpdateReflexiveOffset(writer);
for (int x=0; x<_ripples.Count; x++)
{
  Ripples[x].Write(writer);
}
for (int x=0; x<_ripples.Count; x++)
  Ripples[x].WriteChildData(writer);
}
}
public class ShaderTransparentWaterRippleBlock : IBlock
{
private Pad  __unnamed;	
private Pad  __unnamed2;	
private RealFraction _contributionFactor = new RealFraction();
private Pad  __unnamed3;	
private Angle _animationAngle = new Angle();
private Real _animationVelocity = new Real();
private RealVector2D _mapOffset = new RealVector2D();
private ShortInteger _mapRepeats = new ShortInteger();
private ShortInteger _mapIndex = new ShortInteger();
private Pad  __unnamed4;	
public RealFraction ContributionFactor
{
  get { return _contributionFactor; }
  set { _contributionFactor = value; }
}
public Angle AnimationAngle
{
  get { return _animationAngle; }
  set { _animationAngle = value; }
}
public Real AnimationVelocity
{
  get { return _animationVelocity; }
  set { _animationVelocity = value; }
}
public RealVector2D MapOffset
{
  get { return _mapOffset; }
  set { _mapOffset = value; }
}
public ShortInteger MapRepeats
{
  get { return _mapRepeats; }
  set { _mapRepeats = value; }
}
public ShortInteger MapIndex
{
  get { return _mapIndex; }
  set { _mapIndex = value; }
}
public ShaderTransparentWaterRippleBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(2);
__unnamed3 = new Pad(32);
__unnamed4 = new Pad(16);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  _contributionFactor.Read(reader);
  __unnamed3.Read(reader);
  _animationAngle.Read(reader);
  _animationVelocity.Read(reader);
  _mapOffset.Read(reader);
  _mapRepeats.Read(reader);
  _mapIndex.Read(reader);
  __unnamed4.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    _contributionFactor.Write(writer);
    __unnamed3.Write(writer);
    _animationAngle.Write(writer);
    _animationVelocity.Write(writer);
    _mapOffset.Write(writer);
    _mapRepeats.Write(writer);
    _mapIndex.Write(writer);
    __unnamed4.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
  }
}
