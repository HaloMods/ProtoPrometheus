using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Equipment : Item
  {
    public EquipmentBlock EquipmentValues = new EquipmentBlock();
    public new void Read(BinaryReader reader)
    {
      base.Read(reader);
      Trace.WriteLine("Loading 'Equipment'------------------------------------------------------");
      EquipmentValues.Read(reader);
    }
    public new void ReadChildData(BinaryReader reader)
    {
      base.ReadChildData(reader);
      EquipmentValues.ReadChildData(reader);
    }
    public new void Write(BinaryWriter writer)
    {
      base.Write(writer);
      EquipmentValues.Write(writer);
    }
    public new void WriteChildData(BinaryWriter writer)
    {
      base.WriteChildData(writer);
      EquipmentValues.WriteChildData(writer);
    }
public class EquipmentBlock : IBlock
{
private Enum _powerupType = new Enum();
private Enum _grenadeType = new Enum();
private Real _powerupTime = new Real();
private TagReference _pickupSound = new TagReference();
private Pad  __unnamed;	
public Enum PowerupType
{
  get { return _powerupType; }
  set { _powerupType = value; }
}
public Enum GrenadeType
{
  get { return _grenadeType; }
  set { _grenadeType = value; }
}
public Real PowerupTime
{
  get { return _powerupTime; }
  set { _powerupTime = value; }
}
public TagReference PickupSound
{
  get { return _pickupSound; }
  set { _pickupSound = value; }
}
public EquipmentBlock()
{
__unnamed = new Pad(144);

}
public void Read(BinaryReader reader)
{
  _powerupType.Read(reader);
  _grenadeType.Read(reader);
  _powerupTime.Read(reader);
  _pickupSound.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_pickupSound.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _powerupType.Write(writer);
    _grenadeType.Write(writer);
    _powerupTime.Write(writer);
    _pickupSound.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_pickupSound.WriteString(writer);
}
}
  }
}
