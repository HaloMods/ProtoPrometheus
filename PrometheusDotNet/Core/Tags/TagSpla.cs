using System;
using System.IO;

namespace Prometheus.Core.Tags
{
  //declare your "structure" classes here, use all caps for these class names
  //this way we know it is a simple structure class, not something more complex like the entire tag

  //ie, SOSO_HEADER

  /// <summary>
  /// Summary description for TagSgla.
  /// </summary>
  public class TagSpla : TagBase
  {
    //Radiosity Properties
    public ushort Radiosity_flags; // SingleParameterization = 1, IgnoreNormals = 2, TransparentLit = 3
    public ushort Detail_level; // High = 0, Medium = 1, Low = 2, Turd = 3
    public float Power;
    public COLOR Emitted = new COLOR();
    public COLOR Tint = new COLOR();

    // Physics Properties
    public ushort Material_Type;
        
    // Plasma Shader
    //Nothing

    // Intensity
    public ushort Intensity_source;
    public float Intensity_exponent;

    // Offset
    public ushort Offset_source;
    public float Offset_amount;
    public float Offset_exponent;

    // Color
    public float Perpendicular_brightness;
    public COLOR Perpendicular_color = new COLOR();
    public float Parallel_brightness;
    public COLOR Parallel_color = new COLOR();
    public ushort Tint_color_source;

    // Primary Noise Map
    NOISE_MAP Primary_noise = new NOISE_MAP();

    // Secondary Noise Map
    NOISE_MAP Secondary_noise = new NOISE_MAP();

    public void LoadTagData()
    {
      BinaryReader br = new BinaryReader(m_stream);

      Radiosity_flags = br.ReadUInt16();
      Detail_level = br.ReadUInt16();
      Power = br.ReadSingle();
      Emitted.Load(ref br);
      Tint.Load(ref br);
      br.BaseStream.Position += 2;
      Material_Type = br.ReadUInt16();
      br.BaseStream.Position += 8;
      Intensity_source = br.ReadUInt16();
      br.BaseStream.Position += 2;
      Intensity_exponent = br.ReadSingle();
      Offset_source = br.ReadUInt16();
      br.BaseStream.Position += 2;
      Offset_amount = br.ReadSingle();
      Offset_exponent = br.ReadSingle();
      br.BaseStream.Position += 32;
      Perpendicular_brightness = br.ReadSingle();
      Perpendicular_color.Load(ref br);
      Parallel_brightness = br.ReadSingle();
      Parallel_color.Load(ref br);
      Tint_color_source = br.ReadUInt16();
      br.BaseStream.Position += 62;
      Primary_noise.Load(ref br);
      br.BaseStream.Position += 36;
      Secondary_noise.Load(ref br);
    }
  }
  public class NOISE_MAP
  {
    public float animation_period;
    public float animation_direction_i;
    public float animation_direction_j;
    public float animation_direction_k;
    public float Noise_map_scale;
    TAG_REFERENCE Noise_map = new TAG_REFERENCE();

    public void Load(ref BinaryReader br)
    {
      animation_period = br.ReadSingle();
      animation_direction_i = br.ReadSingle();
      animation_direction_j = br.ReadSingle();
      animation_direction_k = br.ReadSingle();
      Noise_map_scale = br.ReadSingle();
      Noise_map.Load(ref br);
    }
  }
}
