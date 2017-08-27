using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Device : Object
  {
    public DeviceBlock DeviceValues = new DeviceBlock();
    public new void Read(BinaryReader reader)
    {
      base.Read(reader);
      Trace.WriteLine("Loading 'Device'------------------------------------------------------");
      DeviceValues.Read(reader);
    }
    public new void ReadChildData(BinaryReader reader)
    {
      base.ReadChildData(reader);
      DeviceValues.ReadChildData(reader);
    }
    public new void Write(BinaryWriter writer)
    {
      base.Write(writer);
      DeviceValues.Write(writer);
    }
    public new void WriteChildData(BinaryWriter writer)
    {
      base.WriteChildData(writer);
      DeviceValues.WriteChildData(writer);
    }
public class DeviceBlock : IBlock
{
private Flags  _flags;	
private Real _powerTransitionTime = new Real();
private Real _powerAccelerationTime = new Real();
private Real _positionTransitionTime = new Real();
private Real _positionAccelerationTime = new Real();
private Real _depoweredPositionTransitionTime = new Real();
private Real _depoweredPositionAccelerationTime = new Real();
private Enum _aIn = new Enum();
private Enum _bIn = new Enum();
private Enum _cIn = new Enum();
private Enum _dIn = new Enum();
private TagReference _openUp = new TagReference();
private TagReference _closeDown = new TagReference();
private TagReference _opened = new TagReference();
private TagReference _closed = new TagReference();
private TagReference _depowered = new TagReference();
private TagReference _repowered = new TagReference();
private Real _delayTime = new Real();
private Pad  __unnamed;	
private TagReference _delayEffect = new TagReference();
private Real _automaticActivationRadius = new Real();
private Pad  __unnamed2;	
private Pad  __unnamed3;	
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Real PowerTransitionTime
{
  get { return _powerTransitionTime; }
  set { _powerTransitionTime = value; }
}
public Real PowerAccelerationTime
{
  get { return _powerAccelerationTime; }
  set { _powerAccelerationTime = value; }
}
public Real PositionTransitionTime
{
  get { return _positionTransitionTime; }
  set { _positionTransitionTime = value; }
}
public Real PositionAccelerationTime
{
  get { return _positionAccelerationTime; }
  set { _positionAccelerationTime = value; }
}
public Real DepoweredPositionTransitionTime
{
  get { return _depoweredPositionTransitionTime; }
  set { _depoweredPositionTransitionTime = value; }
}
public Real DepoweredPositionAccelerationTime
{
  get { return _depoweredPositionAccelerationTime; }
  set { _depoweredPositionAccelerationTime = value; }
}
public Enum AIn
{
  get { return _aIn; }
  set { _aIn = value; }
}
public Enum BIn
{
  get { return _bIn; }
  set { _bIn = value; }
}
public Enum CIn
{
  get { return _cIn; }
  set { _cIn = value; }
}
public Enum DIn
{
  get { return _dIn; }
  set { _dIn = value; }
}
public TagReference OpenUp
{
  get { return _openUp; }
  set { _openUp = value; }
}
public TagReference CloseDown
{
  get { return _closeDown; }
  set { _closeDown = value; }
}
public TagReference Opened
{
  get { return _opened; }
  set { _opened = value; }
}
public TagReference Closed
{
  get { return _closed; }
  set { _closed = value; }
}
public TagReference Depowered
{
  get { return _depowered; }
  set { _depowered = value; }
}
public TagReference Repowered
{
  get { return _repowered; }
  set { _repowered = value; }
}
public Real DelayTime
{
  get { return _delayTime; }
  set { _delayTime = value; }
}
public TagReference DelayEffect
{
  get { return _delayEffect; }
  set { _delayEffect = value; }
}
public Real AutomaticActivationRadius
{
  get { return _automaticActivationRadius; }
  set { _automaticActivationRadius = value; }
}
public DeviceBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(8);
__unnamed2 = new Pad(84);
__unnamed3 = new Pad(28);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _powerTransitionTime.Read(reader);
  _powerAccelerationTime.Read(reader);
  _positionTransitionTime.Read(reader);
  _positionAccelerationTime.Read(reader);
  _depoweredPositionTransitionTime.Read(reader);
  _depoweredPositionAccelerationTime.Read(reader);
  _aIn.Read(reader);
  _bIn.Read(reader);
  _cIn.Read(reader);
  _dIn.Read(reader);
  _openUp.Read(reader);
  _closeDown.Read(reader);
  _opened.Read(reader);
  _closed.Read(reader);
  _depowered.Read(reader);
  _repowered.Read(reader);
  _delayTime.Read(reader);
  __unnamed.Read(reader);
  _delayEffect.Read(reader);
  _automaticActivationRadius.Read(reader);
  __unnamed2.Read(reader);
  __unnamed3.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_openUp.ReadString(reader);
_closeDown.ReadString(reader);
_opened.ReadString(reader);
_closed.ReadString(reader);
_depowered.ReadString(reader);
_repowered.ReadString(reader);
_delayEffect.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _powerTransitionTime.Write(writer);
    _powerAccelerationTime.Write(writer);
    _positionTransitionTime.Write(writer);
    _positionAccelerationTime.Write(writer);
    _depoweredPositionTransitionTime.Write(writer);
    _depoweredPositionAccelerationTime.Write(writer);
    _aIn.Write(writer);
    _bIn.Write(writer);
    _cIn.Write(writer);
    _dIn.Write(writer);
    _openUp.Write(writer);
    _closeDown.Write(writer);
    _opened.Write(writer);
    _closed.Write(writer);
    _depowered.Write(writer);
    _repowered.Write(writer);
    _delayTime.Write(writer);
    __unnamed.Write(writer);
    _delayEffect.Write(writer);
    _automaticActivationRadius.Write(writer);
    __unnamed2.Write(writer);
    __unnamed3.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_openUp.WriteString(writer);
_closeDown.WriteString(writer);
_opened.WriteString(writer);
_closed.WriteString(writer);
_depowered.WriteString(writer);
_repowered.WriteString(writer);
_delayEffect.WriteString(writer);
}
}
  }
}
