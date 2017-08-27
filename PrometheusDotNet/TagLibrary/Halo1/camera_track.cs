using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class CameraTrack : IBlock
  {
    public CameraTrackBlock CameraTrackValues = new CameraTrackBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'CameraTrack'------------------------------------------------------");
      CameraTrackValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      CameraTrackValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      CameraTrackValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      CameraTrackValues.WriteChildData(writer);
    }
public class CameraTrackBlock : IBlock
{
private Flags  _flags;	
private Block _controlPoints = new Block();
private Pad  __unnamed;	
public class CameraTrackControlPointBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public CameraTrackControlPointBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(CameraTrackControlPointBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new CameraTrackControlPointBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public CameraTrackControlPointBlock this[int index]
  {
    get { return (InnerList[index] as CameraTrackControlPointBlock); }
  }
}
private CameraTrackControlPointBlockCollection _controlPointsCollection;
public CameraTrackControlPointBlockCollection ControlPoints
{
  get { return _controlPointsCollection; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public CameraTrackBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(32);
_controlPointsCollection = new CameraTrackControlPointBlockCollection(_controlPoints);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _controlPoints.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_controlPoints.Count; x++)
{
  ControlPoints.AddNew();
  ControlPoints[x].Read(reader);
}
for (int x=0; x<_controlPoints.Count; x++)
  ControlPoints[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _controlPoints.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_controlPoints.UpdateReflexiveOffset(writer);
for (int x=0; x<_controlPoints.Count; x++)
{
  ControlPoints[x].Write(writer);
}
for (int x=0; x<_controlPoints.Count; x++)
  ControlPoints[x].WriteChildData(writer);
}
}
public class CameraTrackControlPointBlock : IBlock
{
private RealVector3D _position = new RealVector3D();
private RealQuaternion _orientation = new RealQuaternion();
private Pad  __unnamed;	
public RealVector3D Position
{
  get { return _position; }
  set { _position = value; }
}
public RealQuaternion Orientation
{
  get { return _orientation; }
  set { _orientation = value; }
}
public CameraTrackControlPointBlock()
{
__unnamed = new Pad(32);

}
public void Read(BinaryReader reader)
{
  _position.Read(reader);
  _orientation.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _position.Write(writer);
    _orientation.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
  }
}
