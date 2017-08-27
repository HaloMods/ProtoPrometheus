using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class SoundScenery : Object
  {
    public SoundSceneryBlock SoundSceneryValues = new SoundSceneryBlock();
    public new void Read(BinaryReader reader)
    {
      base.Read(reader);
      Trace.WriteLine("Loading 'SoundScenery'------------------------------------------------------");
      SoundSceneryValues.Read(reader);
    }
    public new void ReadChildData(BinaryReader reader)
    {
      base.ReadChildData(reader);
      SoundSceneryValues.ReadChildData(reader);
    }
    public new void Write(BinaryWriter writer)
    {
      base.Write(writer);
      SoundSceneryValues.Write(writer);
    }
    public new void WriteChildData(BinaryWriter writer)
    {
      base.WriteChildData(writer);
      SoundSceneryValues.WriteChildData(writer);
    }
public class SoundSceneryBlock : IBlock
{
private Pad  __unnamed;	
public SoundSceneryBlock()
{
__unnamed = new Pad(128);

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
