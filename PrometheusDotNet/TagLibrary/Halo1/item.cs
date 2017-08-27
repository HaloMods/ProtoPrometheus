using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Item : Object
  {
    public ItemBlock ItemValues = new ItemBlock();
    public new void Read(BinaryReader reader)
    {
      base.Read(reader);
      Trace.WriteLine("Loading 'Item'------------------------------------------------------");
      ItemValues.Read(reader);
    }
    public new void ReadChildData(BinaryReader reader)
    {
      base.ReadChildData(reader);
      ItemValues.ReadChildData(reader);
    }
    public new void Write(BinaryWriter writer)
    {
      base.Write(writer);
      ItemValues.Write(writer);
    }
    public new void WriteChildData(BinaryWriter writer)
    {
      base.WriteChildData(writer);
      ItemValues.WriteChildData(writer);
    }
public class ItemBlock : IBlock
{
private Flags  _flags;	
private ShortInteger _messageIndex = new ShortInteger();
private ShortInteger _sortOrder = new ShortInteger();
private Real _scale = new Real();
private ShortInteger _hudMessageValueScale = new ShortInteger();
private Pad  __unnamed;	
private Pad  __unnamed2;	
private Enum _aIn = new Enum();
private Enum _bIn = new Enum();
private Enum _cIn = new Enum();
private Enum _dIn = new Enum();
private Pad  __unnamed3;	
private TagReference _materialEffects = new TagReference();
private TagReference _collisionSound = new TagReference();
private Pad  __unnamed4;	
private RealBounds _detonationDelay = new RealBounds();
private TagReference _detonatingEffect = new TagReference();
private TagReference _detonationEffect = new TagReference();
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public ShortInteger MessageIndex
{
  get { return _messageIndex; }
  set { _messageIndex = value; }
}
public ShortInteger SortOrder
{
  get { return _sortOrder; }
  set { _sortOrder = value; }
}
public Real Scale
{
  get { return _scale; }
  set { _scale = value; }
}
public ShortInteger HudMessageValueScale
{
  get { return _hudMessageValueScale; }
  set { _hudMessageValueScale = value; }
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
public TagReference MaterialEffects
{
  get { return _materialEffects; }
  set { _materialEffects = value; }
}
public TagReference CollisionSound
{
  get { return _collisionSound; }
  set { _collisionSound = value; }
}
public RealBounds DetonationDelay
{
  get { return _detonationDelay; }
  set { _detonationDelay = value; }
}
public TagReference DetonatingEffect
{
  get { return _detonatingEffect; }
  set { _detonatingEffect = value; }
}
public TagReference DetonationEffect
{
  get { return _detonationEffect; }
  set { _detonationEffect = value; }
}
public ItemBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(2);
__unnamed2 = new Pad(16);
__unnamed3 = new Pad(164);
__unnamed4 = new Pad(120);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _messageIndex.Read(reader);
  _sortOrder.Read(reader);
  _scale.Read(reader);
  _hudMessageValueScale.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  _aIn.Read(reader);
  _bIn.Read(reader);
  _cIn.Read(reader);
  _dIn.Read(reader);
  __unnamed3.Read(reader);
  _materialEffects.Read(reader);
  _collisionSound.Read(reader);
  __unnamed4.Read(reader);
  _detonationDelay.Read(reader);
  _detonatingEffect.Read(reader);
  _detonationEffect.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_materialEffects.ReadString(reader);
_collisionSound.ReadString(reader);
_detonatingEffect.ReadString(reader);
_detonationEffect.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _messageIndex.Write(writer);
    _sortOrder.Write(writer);
    _scale.Write(writer);
    _hudMessageValueScale.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    _aIn.Write(writer);
    _bIn.Write(writer);
    _cIn.Write(writer);
    _dIn.Write(writer);
    __unnamed3.Write(writer);
    _materialEffects.Write(writer);
    _collisionSound.Write(writer);
    __unnamed4.Write(writer);
    _detonationDelay.Write(writer);
    _detonatingEffect.Write(writer);
    _detonationEffect.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_materialEffects.WriteString(writer);
_collisionSound.WriteString(writer);
_detonatingEffect.WriteString(writer);
_detonationEffect.WriteString(writer);
}
}
  }
}
