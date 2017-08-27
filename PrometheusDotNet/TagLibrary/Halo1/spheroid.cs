using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Spheroid : IBlock
  {
    public SpheroidBlock SpheroidValues = new SpheroidBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'Spheroid'------------------------------------------------------");
      SpheroidValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      SpheroidValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      SpheroidValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      SpheroidValues.WriteChildData(writer);
    }
public class SpheroidBlock : IBlock
{
private Pad  __unnamed;	
public SpheroidBlock()
{
__unnamed = new Pad(4);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
  }
}
