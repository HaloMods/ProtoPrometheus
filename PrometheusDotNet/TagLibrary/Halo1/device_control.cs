using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class DeviceControl : IBlock
  {
    public DeviceControlBlock DeviceControlValues = new DeviceControlBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'DeviceControl'------------------------------------------------------");
      DeviceControlValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      DeviceControlValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      DeviceControlValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      DeviceControlValues.WriteChildData(writer);
    }
public class DeviceControlBlock : IBlock
{
private Enum _type = new Enum();
private Enum _triggersWhen = new Enum();
private Real _callValue = new Real();
private Pad  __unnamed;	
private TagReference _on = new TagReference();
private TagReference _off = new TagReference();
private TagReference _deny = new TagReference();
public Enum Type
{
  get { return _type; }
  set { _type = value; }
}
public Enum TriggersWhen
{
  get { return _triggersWhen; }
  set { _triggersWhen = value; }
}
public Real CallValue
{
  get { return _callValue; }
  set { _callValue = value; }
}
public TagReference On
{
  get { return _on; }
  set { _on = value; }
}
public TagReference Off
{
  get { return _off; }
  set { _off = value; }
}
public TagReference Deny
{
  get { return _deny; }
  set { _deny = value; }
}
public DeviceControlBlock()
{
__unnamed = new Pad(80);

}
public void Read(BinaryReader reader)
{
  _type.Read(reader);
  _triggersWhen.Read(reader);
  _callValue.Read(reader);
  __unnamed.Read(reader);
  _on.Read(reader);
  _off.Read(reader);
  _deny.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_on.ReadString(reader);
_off.ReadString(reader);
_deny.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _type.Write(writer);
    _triggersWhen.Write(writer);
    _callValue.Write(writer);
    __unnamed.Write(writer);
    _on.Write(writer);
    _off.Write(writer);
    _deny.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_on.WriteString(writer);
_off.WriteString(writer);
_deny.WriteString(writer);
}
}
  }
}
