using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Antenna : IBlock
  {
    public AntennaBlock AntennaValues = new AntennaBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'Antenna'------------------------------------------------------");
      AntennaValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      AntennaValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      AntennaValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      AntennaValues.WriteChildData(writer);
    }
public class AntennaBlock : IBlock
{
private FixedLengthString _attachmentMarkerName = new FixedLengthString();
private TagReference _bitmaps = new TagReference();
private TagReference _physics = new TagReference();
private Pad  __unnamed;	
private RealFraction _springStrengthCoefficient = new RealFraction();
private Real _falloffPixels = new Real();
private Real _cutoffPixels = new Real();
private Pad  __unnamed2;	
private Block _vertices = new Block();
public class AntennaVertexBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public AntennaVertexBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(AntennaVertexBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new AntennaVertexBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public AntennaVertexBlock this[int index]
  {
    get { return (InnerList[index] as AntennaVertexBlock); }
  }
}
private AntennaVertexBlockCollection _verticesCollection;
public AntennaVertexBlockCollection Vertices
{
  get { return _verticesCollection; }
}
public FixedLengthString AttachmentMarkerName
{
  get { return _attachmentMarkerName; }
  set { _attachmentMarkerName = value; }
}
public TagReference Bitmaps
{
  get { return _bitmaps; }
  set { _bitmaps = value; }
}
public TagReference Physics
{
  get { return _physics; }
  set { _physics = value; }
}
public RealFraction SpringStrengthCoefficient
{
  get { return _springStrengthCoefficient; }
  set { _springStrengthCoefficient = value; }
}
public Real FalloffPixels
{
  get { return _falloffPixels; }
  set { _falloffPixels = value; }
}
public Real CutoffPixels
{
  get { return _cutoffPixels; }
  set { _cutoffPixels = value; }
}
public AntennaBlock()
{
__unnamed = new Pad(80);
__unnamed2 = new Pad(40);
_verticesCollection = new AntennaVertexBlockCollection(_vertices);

}
public void Read(BinaryReader reader)
{
  _attachmentMarkerName.Read(reader);
  _bitmaps.Read(reader);
  _physics.Read(reader);
  __unnamed.Read(reader);
  _springStrengthCoefficient.Read(reader);
  _falloffPixels.Read(reader);
  _cutoffPixels.Read(reader);
  __unnamed2.Read(reader);
  _vertices.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_bitmaps.ReadString(reader);
_physics.ReadString(reader);
for (int x=0; x<_vertices.Count; x++)
{
  Vertices.AddNew();
  Vertices[x].Read(reader);
}
for (int x=0; x<_vertices.Count; x++)
  Vertices[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _attachmentMarkerName.Write(writer);
    _bitmaps.Write(writer);
    _physics.Write(writer);
    __unnamed.Write(writer);
    _springStrengthCoefficient.Write(writer);
    _falloffPixels.Write(writer);
    _cutoffPixels.Write(writer);
    __unnamed2.Write(writer);
    _vertices.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_bitmaps.WriteString(writer);
_physics.WriteString(writer);
_vertices.UpdateReflexiveOffset(writer);
for (int x=0; x<_vertices.Count; x++)
{
  Vertices[x].Write(writer);
}
for (int x=0; x<_vertices.Count; x++)
  Vertices[x].WriteChildData(writer);
}
}
public class AntennaVertexBlock : IBlock
{
private RealFraction _springStrengthCoefficient = new RealFraction();
private Pad  __unnamed;	
private RealEulerAngles2D _angles = new RealEulerAngles2D();
private Real _length = new Real();
private ShortInteger _sequenceIndex = new ShortInteger();
private Pad  __unnamed2;	
private RealARGBColor _color = new RealARGBColor();
private RealARGBColor _lODColor = new RealARGBColor();
private Pad  __unnamed3;	
private Pad  __unnamed4;	
public RealFraction SpringStrengthCoefficient
{
  get { return _springStrengthCoefficient; }
  set { _springStrengthCoefficient = value; }
}
public RealEulerAngles2D Angles
{
  get { return _angles; }
  set { _angles = value; }
}
public Real Length
{
  get { return _length; }
  set { _length = value; }
}
public ShortInteger SequenceIndex
{
  get { return _sequenceIndex; }
  set { _sequenceIndex = value; }
}
public RealARGBColor Color
{
  get { return _color; }
  set { _color = value; }
}
public RealARGBColor LODColor
{
  get { return _lODColor; }
  set { _lODColor = value; }
}
public AntennaVertexBlock()
{
__unnamed = new Pad(24);
__unnamed2 = new Pad(2);
__unnamed3 = new Pad(40);
__unnamed4 = new Pad(12);

}
public void Read(BinaryReader reader)
{
  _springStrengthCoefficient.Read(reader);
  __unnamed.Read(reader);
  _angles.Read(reader);
  _length.Read(reader);
  _sequenceIndex.Read(reader);
  __unnamed2.Read(reader);
  _color.Read(reader);
  _lODColor.Read(reader);
  __unnamed3.Read(reader);
  __unnamed4.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _springStrengthCoefficient.Write(writer);
    __unnamed.Write(writer);
    _angles.Write(writer);
    _length.Write(writer);
    _sequenceIndex.Write(writer);
    __unnamed2.Write(writer);
    _color.Write(writer);
    _lODColor.Write(writer);
    __unnamed3.Write(writer);
    __unnamed4.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
  }
}
