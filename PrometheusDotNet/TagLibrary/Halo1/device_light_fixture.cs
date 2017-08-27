using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class DeviceLightFixture : IBlock
  {
    public DeviceLightFixtureBlock DeviceLightFixtureValues = new DeviceLightFixtureBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'DeviceLightFixture'------------------------------------------------------");
      DeviceLightFixtureValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      DeviceLightFixtureValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      DeviceLightFixtureValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      DeviceLightFixtureValues.WriteChildData(writer);
    }
public class DeviceLightFixtureBlock : IBlock
{
private Pad  __unnamed;	
public DeviceLightFixtureBlock()
{
__unnamed = new Pad(64);

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
