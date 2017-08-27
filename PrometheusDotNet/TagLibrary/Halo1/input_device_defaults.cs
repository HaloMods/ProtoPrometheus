using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class InputDeviceDefaults : IBlock
  {
    public InputDeviceDefaultsBlock InputDeviceDefaultsValues = new InputDeviceDefaultsBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'InputDeviceDefaults'------------------------------------------------------");
      InputDeviceDefaultsValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      InputDeviceDefaultsValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      InputDeviceDefaultsValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      InputDeviceDefaultsValues.WriteChildData(writer);
    }
public class InputDeviceDefaultsBlock : IBlock
{
private Enum _deviceType = new Enum();
private Flags  _flags;	
private Data _deviceId = new Data();
private Data _profile = new Data();
public Enum DeviceType
{
  get { return _deviceType; }
  set { _deviceType = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Data DeviceId
{
  get { return _deviceId; }
  set { _deviceId = value; }
}
public Data Profile
{
  get { return _profile; }
  set { _profile = value; }
}
public InputDeviceDefaultsBlock()
{
_flags = new Flags(2);

}
public void Read(BinaryReader reader)
{
  _deviceType.Read(reader);
  _flags.Read(reader);
  _deviceId.Read(reader);
  _profile.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_deviceId.ReadBinary(reader);
_profile.ReadBinary(reader);
}
public void Write(BinaryWriter writer)
{
    _deviceType.Write(writer);
    _flags.Write(writer);
    _deviceId.Write(writer);
    _profile.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_deviceId.WriteBinary(writer);
_profile.WriteBinary(writer);
}
}
  }
}
