using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Flag : IBlock
  {
    public FlagBlock FlagValues = new FlagBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'Flag'------------------------------------------------------");
      FlagValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      FlagValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      FlagValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      FlagValues.WriteChildData(writer);
    }
public class FlagBlock : IBlock
{
private Flags  _flags;	
private Enum _trailingEdgeShape = new Enum();
private ShortInteger _trailingEdgeShapeOffset = new ShortInteger();
private Enum _attachedEdgeShape = new Enum();
private Pad  __unnamed;	
private ShortInteger _width = new ShortInteger();
private ShortInteger _height = new ShortInteger();
private Real _cellWidth = new Real();
private Real _cellHeight = new Real();
private TagReference _redFlagShader = new TagReference();
private TagReference _physics = new TagReference();
private Real _windNoise = new Real();
private Pad  __unnamed2;	
private TagReference _blueFlagShader = new TagReference();
private Block _attachmentPoints = new Block();
public class FlagAttachmentPointBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public FlagAttachmentPointBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(FlagAttachmentPointBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new FlagAttachmentPointBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public FlagAttachmentPointBlock this[int index]
  {
    get { return (InnerList[index] as FlagAttachmentPointBlock); }
  }
}
private FlagAttachmentPointBlockCollection _attachmentPointsCollection;
public FlagAttachmentPointBlockCollection AttachmentPoints
{
  get { return _attachmentPointsCollection; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Enum TrailingEdgeShape
{
  get { return _trailingEdgeShape; }
  set { _trailingEdgeShape = value; }
}
public ShortInteger TrailingEdgeShapeOffset
{
  get { return _trailingEdgeShapeOffset; }
  set { _trailingEdgeShapeOffset = value; }
}
public Enum AttachedEdgeShape
{
  get { return _attachedEdgeShape; }
  set { _attachedEdgeShape = value; }
}
public ShortInteger Width
{
  get { return _width; }
  set { _width = value; }
}
public ShortInteger Height
{
  get { return _height; }
  set { _height = value; }
}
public Real CellWidth
{
  get { return _cellWidth; }
  set { _cellWidth = value; }
}
public Real CellHeight
{
  get { return _cellHeight; }
  set { _cellHeight = value; }
}
public TagReference RedFlagShader
{
  get { return _redFlagShader; }
  set { _redFlagShader = value; }
}
public TagReference Physics
{
  get { return _physics; }
  set { _physics = value; }
}
public Real WindNoise
{
  get { return _windNoise; }
  set { _windNoise = value; }
}
public TagReference BlueFlagShader
{
  get { return _blueFlagShader; }
  set { _blueFlagShader = value; }
}
public FlagBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(2);
__unnamed2 = new Pad(8);
_attachmentPointsCollection = new FlagAttachmentPointBlockCollection(_attachmentPoints);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _trailingEdgeShape.Read(reader);
  _trailingEdgeShapeOffset.Read(reader);
  _attachedEdgeShape.Read(reader);
  __unnamed.Read(reader);
  _width.Read(reader);
  _height.Read(reader);
  _cellWidth.Read(reader);
  _cellHeight.Read(reader);
  _redFlagShader.Read(reader);
  _physics.Read(reader);
  _windNoise.Read(reader);
  __unnamed2.Read(reader);
  _blueFlagShader.Read(reader);
  _attachmentPoints.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_redFlagShader.ReadString(reader);
_physics.ReadString(reader);
_blueFlagShader.ReadString(reader);
for (int x=0; x<_attachmentPoints.Count; x++)
{
  AttachmentPoints.AddNew();
  AttachmentPoints[x].Read(reader);
}
for (int x=0; x<_attachmentPoints.Count; x++)
  AttachmentPoints[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _trailingEdgeShape.Write(writer);
    _trailingEdgeShapeOffset.Write(writer);
    _attachedEdgeShape.Write(writer);
    __unnamed.Write(writer);
    _width.Write(writer);
    _height.Write(writer);
    _cellWidth.Write(writer);
    _cellHeight.Write(writer);
    _redFlagShader.Write(writer);
    _physics.Write(writer);
    _windNoise.Write(writer);
    __unnamed2.Write(writer);
    _blueFlagShader.Write(writer);
    _attachmentPoints.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_redFlagShader.WriteString(writer);
_physics.WriteString(writer);
_blueFlagShader.WriteString(writer);
_attachmentPoints.UpdateReflexiveOffset(writer);
for (int x=0; x<_attachmentPoints.Count; x++)
{
  AttachmentPoints[x].Write(writer);
}
for (int x=0; x<_attachmentPoints.Count; x++)
  AttachmentPoints[x].WriteChildData(writer);
}
}
public class FlagAttachmentPointBlock : IBlock
{
private ShortInteger _height_to_next_attachment = new ShortInteger();
private Pad  __unnamed;	
private Pad  __unnamed2;	
private FixedLengthString _markerName = new FixedLengthString();
public ShortInteger Height_to_next_attachment
{
  get { return _height_to_next_attachment; }
  set { _height_to_next_attachment = value; }
}
public FixedLengthString MarkerName
{
  get { return _markerName; }
  set { _markerName = value; }
}
public FlagAttachmentPointBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(16);

}
public void Read(BinaryReader reader)
{
  _height_to_next_attachment.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  _markerName.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _height_to_next_attachment.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    _markerName.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
  }
}
