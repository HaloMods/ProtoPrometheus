using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class PointPhysics : IBlock
  {
    public PointPhysicsBlock PointPhysicsValues = new PointPhysicsBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'PointPhysics'------------------------------------------------------");
      PointPhysicsValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      PointPhysicsValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      PointPhysicsValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      PointPhysicsValues.WriteChildData(writer);
    }
public class PointPhysicsBlock : IBlock
{
private Flags  _flags;	
private Pad  __unnamed;	
private Real _density = new Real();
private Real _airFriction = new Real();
private Real _waterFriction = new Real();
private Real _surfaceFriction = new Real();
private Real _elasticity = new Real();
private Pad  __unnamed2;	
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Real Density
{
  get { return _density; }
  set { _density = value; }
}
public Real AirFriction
{
  get { return _airFriction; }
  set { _airFriction = value; }
}
public Real WaterFriction
{
  get { return _waterFriction; }
  set { _waterFriction = value; }
}
public Real SurfaceFriction
{
  get { return _surfaceFriction; }
  set { _surfaceFriction = value; }
}
public Real Elasticity
{
  get { return _elasticity; }
  set { _elasticity = value; }
}
public PointPhysicsBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(28);
__unnamed2 = new Pad(12);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  __unnamed.Read(reader);
  _density.Read(reader);
  _airFriction.Read(reader);
  _waterFriction.Read(reader);
  _surfaceFriction.Read(reader);
  _elasticity.Read(reader);
  __unnamed2.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    __unnamed.Write(writer);
    _density.Write(writer);
    _airFriction.Write(writer);
    _waterFriction.Write(writer);
    _surfaceFriction.Write(writer);
    _elasticity.Write(writer);
    __unnamed2.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
  }
}
