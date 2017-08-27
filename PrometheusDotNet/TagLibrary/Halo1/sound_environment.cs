using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class SoundEnvironment : IBlock
  {
    public SoundEnvironmentBlock SoundEnvironmentValues = new SoundEnvironmentBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'SoundEnvironment'------------------------------------------------------");
      SoundEnvironmentValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      SoundEnvironmentValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      SoundEnvironmentValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      SoundEnvironmentValues.WriteChildData(writer);
    }
public class SoundEnvironmentBlock : IBlock
{
private Pad  __unnamed;	
private ShortInteger _priority = new ShortInteger();
private Pad  __unnamed2;	
private RealFraction _roomIntensity = new RealFraction();
private RealFraction _roomIntensityHf = new RealFraction();
private Real _roomRolloff0To10 = new Real();
private Real _decayTime_Pt1To20 = new Real();
private Real _decayHfRatio_Pt1To2 = new Real();
private RealFraction _reflectionsIntensity = new RealFraction();
private Real _reflectionsDelay0To_Pt3 = new Real();
private RealFraction _reverbIntensity = new RealFraction();
private Real _reverbDelay0To_Pt1 = new Real();
private Real _diffusion = new Real();
private Real _density = new Real();
private Real _hfReference20To2 = new Real();
private Pad  __unnamed3;	
public ShortInteger Priority
{
  get { return _priority; }
  set { _priority = value; }
}
public RealFraction RoomIntensity
{
  get { return _roomIntensity; }
  set { _roomIntensity = value; }
}
public RealFraction RoomIntensityHf
{
  get { return _roomIntensityHf; }
  set { _roomIntensityHf = value; }
}
public Real RoomRolloff0To10
{
  get { return _roomRolloff0To10; }
  set { _roomRolloff0To10 = value; }
}
public Real DecayTime_Pt1To20
{
  get { return _decayTime_Pt1To20; }
  set { _decayTime_Pt1To20 = value; }
}
public Real DecayHfRatio_Pt1To2
{
  get { return _decayHfRatio_Pt1To2; }
  set { _decayHfRatio_Pt1To2 = value; }
}
public RealFraction ReflectionsIntensity
{
  get { return _reflectionsIntensity; }
  set { _reflectionsIntensity = value; }
}
public Real ReflectionsDelay0To_Pt3
{
  get { return _reflectionsDelay0To_Pt3; }
  set { _reflectionsDelay0To_Pt3 = value; }
}
public RealFraction ReverbIntensity
{
  get { return _reverbIntensity; }
  set { _reverbIntensity = value; }
}
public Real ReverbDelay0To_Pt1
{
  get { return _reverbDelay0To_Pt1; }
  set { _reverbDelay0To_Pt1 = value; }
}
public Real Diffusion
{
  get { return _diffusion; }
  set { _diffusion = value; }
}
public Real Density
{
  get { return _density; }
  set { _density = value; }
}
public Real HfReference20To2
{
  get { return _hfReference20To2; }
  set { _hfReference20To2 = value; }
}
public SoundEnvironmentBlock()
{
__unnamed = new Pad(4);
__unnamed2 = new Pad(2);
__unnamed3 = new Pad(16);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  _priority.Read(reader);
  __unnamed2.Read(reader);
  _roomIntensity.Read(reader);
  _roomIntensityHf.Read(reader);
  _roomRolloff0To10.Read(reader);
  _decayTime_Pt1To20.Read(reader);
  _decayHfRatio_Pt1To2.Read(reader);
  _reflectionsIntensity.Read(reader);
  _reflectionsDelay0To_Pt3.Read(reader);
  _reverbIntensity.Read(reader);
  _reverbDelay0To_Pt1.Read(reader);
  _diffusion.Read(reader);
  _density.Read(reader);
  _hfReference20To2.Read(reader);
  __unnamed3.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    _priority.Write(writer);
    __unnamed2.Write(writer);
    _roomIntensity.Write(writer);
    _roomIntensityHf.Write(writer);
    _roomRolloff0To10.Write(writer);
    _decayTime_Pt1To20.Write(writer);
    _decayHfRatio_Pt1To2.Write(writer);
    _reflectionsIntensity.Write(writer);
    _reflectionsDelay0To_Pt3.Write(writer);
    _reverbIntensity.Write(writer);
    _reverbDelay0To_Pt1.Write(writer);
    _diffusion.Write(writer);
    _density.Write(writer);
    _hfReference20To2.Write(writer);
    __unnamed3.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
  }
}
