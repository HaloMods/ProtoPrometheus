using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class DeviceMachine : IBlock
  {
    public DeviceMachineBlock DeviceMachineValues = new DeviceMachineBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'DeviceMachine'------------------------------------------------------");
      DeviceMachineValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      DeviceMachineValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      DeviceMachineValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      DeviceMachineValues.WriteChildData(writer);
    }
public class DeviceMachineBlock : IBlock
{
private Enum _type = new Enum();
private Flags  _flags;	
private Real _doorOpenTime = new Real();
private Pad  __unnamed;	
private Enum _collisionResponse = new Enum();
private ShortInteger _elevatorNode = new ShortInteger();
private Pad  __unnamed2;	
private Pad  __unnamed3;	
public Enum Type
{
  get { return _type; }
  set { _type = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Real DoorOpenTime
{
  get { return _doorOpenTime; }
  set { _doorOpenTime = value; }
}
public Enum CollisionResponse
{
  get { return _collisionResponse; }
  set { _collisionResponse = value; }
}
public ShortInteger ElevatorNode
{
  get { return _elevatorNode; }
  set { _elevatorNode = value; }
}
public DeviceMachineBlock()
{
_flags = new Flags(2);
__unnamed = new Pad(80);
__unnamed2 = new Pad(52);
__unnamed3 = new Pad(4);

}
public void Read(BinaryReader reader)
{
  _type.Read(reader);
  _flags.Read(reader);
  _doorOpenTime.Read(reader);
  __unnamed.Read(reader);
  _collisionResponse.Read(reader);
  _elevatorNode.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _type.Write(writer);
    _flags.Write(writer);
    _doorOpenTime.Write(writer);
    __unnamed.Write(writer);
    _collisionResponse.Write(writer);
    _elevatorNode.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
  }
}
