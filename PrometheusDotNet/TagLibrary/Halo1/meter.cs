using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Meter : IBlock
  {
    public MeterBlock MeterValues = new MeterBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'Meter'------------------------------------------------------");
      MeterValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      MeterValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      MeterValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      MeterValues.WriteChildData(writer);
    }
public class MeterBlock : IBlock
{
private Flags  _flags;	
private TagReference _stencilBitmaps = new TagReference();
private TagReference _sourceBitmap = new TagReference();
private ShortInteger _stencilSequenceIndex = new ShortInteger();
private ShortInteger _sourceSequenceIndex = new ShortInteger();
private Pad  __unnamed;	
private Pad  __unnamed2;	
private Enum _interpolateColors_Pt_Pt_Pt = new Enum();
private Enum _anchorColors_Pt_Pt_Pt = new Enum();
private Pad  __unnamed3;	
private RealARGBColor _emptyColor = new RealARGBColor();
private RealARGBColor _fullColor = new RealARGBColor();
private Pad  __unnamed4;	
private Real _unmaskDistance = new Real();
private Real _maskDistance = new Real();
private Pad  __unnamed5;	
private Data _encodedStencil = new Data();
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public TagReference StencilBitmaps
{
  get { return _stencilBitmaps; }
  set { _stencilBitmaps = value; }
}
public TagReference SourceBitmap
{
  get { return _sourceBitmap; }
  set { _sourceBitmap = value; }
}
public ShortInteger StencilSequenceIndex
{
  get { return _stencilSequenceIndex; }
  set { _stencilSequenceIndex = value; }
}
public ShortInteger SourceSequenceIndex
{
  get { return _sourceSequenceIndex; }
  set { _sourceSequenceIndex = value; }
}
public Enum InterpolateColors_Pt_Pt_Pt
{
  get { return _interpolateColors_Pt_Pt_Pt; }
  set { _interpolateColors_Pt_Pt_Pt = value; }
}
public Enum AnchorColors_Pt_Pt_Pt
{
  get { return _anchorColors_Pt_Pt_Pt; }
  set { _anchorColors_Pt_Pt_Pt = value; }
}
public RealARGBColor EmptyColor
{
  get { return _emptyColor; }
  set { _emptyColor = value; }
}
public RealARGBColor FullColor
{
  get { return _fullColor; }
  set { _fullColor = value; }
}
public Real UnmaskDistance
{
  get { return _unmaskDistance; }
  set { _unmaskDistance = value; }
}
public Real MaskDistance
{
  get { return _maskDistance; }
  set { _maskDistance = value; }
}
public Data EncodedStencil
{
  get { return _encodedStencil; }
  set { _encodedStencil = value; }
}
public MeterBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(16);
__unnamed2 = new Pad(4);
__unnamed3 = new Pad(8);
__unnamed4 = new Pad(20);
__unnamed5 = new Pad(20);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _stencilBitmaps.Read(reader);
  _sourceBitmap.Read(reader);
  _stencilSequenceIndex.Read(reader);
  _sourceSequenceIndex.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  _interpolateColors_Pt_Pt_Pt.Read(reader);
  _anchorColors_Pt_Pt_Pt.Read(reader);
  __unnamed3.Read(reader);
  _emptyColor.Read(reader);
  _fullColor.Read(reader);
  __unnamed4.Read(reader);
  _unmaskDistance.Read(reader);
  _maskDistance.Read(reader);
  __unnamed5.Read(reader);
  _encodedStencil.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_stencilBitmaps.ReadString(reader);
_sourceBitmap.ReadString(reader);
_encodedStencil.ReadBinary(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _stencilBitmaps.Write(writer);
    _sourceBitmap.Write(writer);
    _stencilSequenceIndex.Write(writer);
    _sourceSequenceIndex.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    _interpolateColors_Pt_Pt_Pt.Write(writer);
    _anchorColors_Pt_Pt_Pt.Write(writer);
    __unnamed3.Write(writer);
    _emptyColor.Write(writer);
    _fullColor.Write(writer);
    __unnamed4.Write(writer);
    _unmaskDistance.Write(writer);
    _maskDistance.Write(writer);
    __unnamed5.Write(writer);
    _encodedStencil.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_stencilBitmaps.WriteString(writer);
_sourceBitmap.WriteString(writer);
_encodedStencil.WriteBinary(writer);
}
}
  }
}
