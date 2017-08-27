using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Wind : IBlock
  {
    public WindBlock WindValues = new WindBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'Wind'------------------------------------------------------");
      WindValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      WindValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      WindValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      WindValues.WriteChildData(writer);
    }
public class WindBlock : IBlock
{
private RealBounds _velocity = new RealBounds();
private RealEulerAngles2D _variationArea = new RealEulerAngles2D();
private Real _localVariationWeight = new Real();
private Real _localVariationRate = new Real();
private Real _damping = new Real();
private Pad  __unnamed;	
public RealBounds Velocity
{
  get { return _velocity; }
  set { _velocity = value; }
}
public RealEulerAngles2D VariationArea
{
  get { return _variationArea; }
  set { _variationArea = value; }
}
public Real LocalVariationWeight
{
  get { return _localVariationWeight; }
  set { _localVariationWeight = value; }
}
public Real LocalVariationRate
{
  get { return _localVariationRate; }
  set { _localVariationRate = value; }
}
public Real Damping
{
  get { return _damping; }
  set { _damping = value; }
}
public WindBlock()
{
__unnamed = new Pad(36);

}
public void Read(BinaryReader reader)
{
  _velocity.Read(reader);
  _variationArea.Read(reader);
  _localVariationWeight.Read(reader);
  _localVariationRate.Read(reader);
  _damping.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _velocity.Write(writer);
    _variationArea.Write(writer);
    _localVariationWeight.Write(writer);
    _localVariationRate.Write(writer);
    _damping.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
  }
}
