using System;
using System.IO;
using System.Drawing;
using Prometheus.Core.Render;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;

namespace Prometheus.Core.Tags
{
  //declare your "structure" classes here, use all caps for these class names
  //this way we know it is a simple structure class, not something more complex like the entire tag

  //ie, SOSO_HEADER

  /// <summary>
  /// Summary description for TagSoso.
  /// </summary>
  public class TagSoso : ShaderBase
  {
  //Radiosity Properties
    public ushort Radiosity_flags; // SingleParameterization = 1, IgnoreNormals = 2, TransparentLit = 3
    public ushort Detail_level; // High = 0, Medium = 1, Low = 2, Turd = 3
    public float Power;
    public COLOR Emitted = new COLOR();
    public COLOR Tint = new COLOR();

  // Physics Properties
    public ushort Material_Type;
        
  // Model Shader
    public ushort Shader_flags; // add blah here
    public float Translucency;
    
  // Change Color
    public ushort Change_color_source; // None = 0, A = 1, B = 2. C = 3, D = 4

  // Self Illumination
    public ushort Illumination_flags; // No Random Phase = 1
    public ushort Illumination_color_source; // None = 0, A = 1, B = 2. C = 3, D = 4
    public ushort Animation_function;
    public ushort Animation_period;
    public COLOR Animation_lower = new COLOR();
    public COLOR Animation_upper = new COLOR();

  // Maps
    public float Map_uscale;
    public float Map_vscale;
    public TAG_REFERENCE Base_map = new TAG_REFERENCE();
    public TAG_REFERENCE Multipurpose_map = new TAG_REFERENCE();
    public ushort Detail_function;
    public ushort Detail_mask;
    public float Detail_map_scale;
    public TAG_REFERENCE Detail_map = new TAG_REFERENCE();
    public float Detail_map_vscale;

  // Texture Scrolling Animation
    public SCROLLING_MAP U_map = new SCROLLING_MAP();
    public SCROLLING_MAP V_map = new SCROLLING_MAP();
    public SCROLLING_MAP Rotation_map = new SCROLLING_MAP();
    public float Rotation_center_x;
    public float Rotation_center_y;

  // Reflection Properties
    public uint Falloff_distance;
    public uint Cutoff_distance;
    public float Perpendicular_brightness;
    public COLOR Perpendicular_tint = new COLOR();
    public float Parallel_brightness;
    public COLOR Parallel_tint = new COLOR();
    public TAG_REFERENCE Cube_map = new TAG_REFERENCE();

    private Matrix detailMapTransform = new Matrix();

    
      public bool TwoSided
      {
        get
        {
          return(((Shader_flags & 0x2) == 0x2));
        }
      }
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
      br.BaseStream.Position += 4;
            Shader_flags = br.ReadUInt16();
      br.BaseStream.Position += 14;
      Translucency = br.ReadSingle(); 
      br.BaseStream.Position += 16;
      Change_color_source = br.ReadUInt16();
      br.BaseStream.Position += 30;
      Illumination_flags = br.ReadUInt16();
      br.BaseStream.Position += 2;
      Illumination_color_source = br.ReadUInt16();
      Animation_function = br.ReadUInt16();
      Animation_period = br.ReadUInt16();
      br.BaseStream.Position += 2;
      Animation_lower.Load(ref br);
      Animation_upper.Load(ref br);
      br.BaseStream.Position += 12;
      Map_uscale = br.ReadSingle();
      Map_vscale = br.ReadSingle();
      Base_map.Load(ref br);
      br.BaseStream.Position += 8;
      Multipurpose_map.Load(ref br);
      br.BaseStream.Position += 8;
      Detail_function = br.ReadUInt16();
      Detail_mask = br.ReadUInt16();
      Detail_map_scale = br.ReadSingle();
      Detail_map.Load(ref br);
      Detail_map_vscale = br.ReadSingle();
      U_map.Load(ref br);
      V_map.Load(ref br);
      Rotation_map.Load(ref br);
      Rotation_center_x = br.ReadSingle();
      Rotation_center_y = br.ReadSingle();
      br.BaseStream.Position += 8;
      Falloff_distance = br.ReadUInt32();
      Cutoff_distance = br.ReadUInt32();
      Perpendicular_brightness = br.ReadSingle();
      Perpendicular_tint.Load(ref br);
      Parallel_brightness = br.ReadSingle();
      Parallel_tint.Load(ref br);
      Cube_map.Load(ref br);

      // *******************************
      // !!!!!!!!!!! HACK !!!!!!!!!!!!!!
      // *******************************
      // *** Not sure where it's happening, but we are getting out of place.
      // I'm just gonna move to the literal location for now (since
      // these tags have no sub blocks)
      // *******************************
      br.BaseStream.Position = 0x1B8;
      Base_map.ReadString(ref br);
      Multipurpose_map.ReadString(ref br);
      Detail_map.ReadString(ref br);

      //calculate scale transform since it doesn't change
      detailMapTransform = Matrix.Identity;
      detailMapTransform.Scale(Detail_map_scale, Detail_map_scale*Detail_map_vscale, 1);
    }
    override public void LoadTextures(ref TextureManager TexMgr, int NumTextureStages)
    {
      this.NumTextureStages = NumTextureStages;
      this.Stage1TextureIndex = TexMgr.RegisterTexture(new TagFileName(Base_map.data, "bitm", this.m_PromHeader.GameVersion));
      if(Detail_map.data != null)
        this.Stage2TextureIndex = TexMgr.RegisterTexture(new TagFileName(Detail_map.data, "bitm", this.m_PromHeader.GameVersion));
    }
    public void Pass1()
    {
      MdxRender.Dev.RenderState.SourceBlend = Blend.SourceAlpha;
      MdxRender.Dev.RenderState.DestinationBlend = Blend.InvSourceAlpha;
      MdxRender.Dev.RenderState.AlphaBlendEnable = true;

      ushort flags = Shader_flags;


      //not alpha tested
      if((Shader_flags & 0x4) == 0)
        MdxRender.Dev.RenderState.AlphaTestEnable = true;
      else
        MdxRender.Dev.RenderState.AlphaTestEnable = false;

      //two-sided
      if((Shader_flags & 0x2) == 0)
        MdxRender.Dev.RenderState.CullMode = Cull.CounterClockwise;
      else
      {
        MdxRender.Dev.RenderState.CullMode = Cull.None;
        MdxRender.Dev.RenderState.AlphaTestEnable = true;
      }

      //transparency
      MdxRender.SM.m_TextureManager.ActivateTexture(0, Stage1TextureIndex, 0);
      MdxRender.SM.m_TextureManager.ActivateTexture(1, Stage1TextureIndex, 0);

      //alpha blend lit material
      MdxRender.Dev.TextureState[0].AlphaArgument1 = TextureArgument.TextureColor;
      MdxRender.Dev.TextureState[0].AlphaOperation = TextureOperation.SelectArg1;
      MdxRender.Dev.TextureState[0].ColorArgument1 = TextureArgument.Diffuse;
      MdxRender.Dev.TextureState[0].ColorArgument2 = TextureArgument.TextureColor;
      MdxRender.Dev.TextureState[0].ColorOperation = TextureOperation.ModulateInvAlphaAddColor;
      MdxRender.Dev.TextureState[0].TextureTransform = TextureTransform.Disable;

      //alpha blend output of stage 1 with texture color
      MdxRender.Dev.TextureState[1].AlphaArgument1 = TextureArgument.Current;
      MdxRender.Dev.TextureState[1].AlphaOperation = TextureOperation.SelectArg1;
      MdxRender.Dev.TextureState[1].ColorArgument1 = TextureArgument.Current;
      MdxRender.Dev.TextureState[1].ColorArgument2 = TextureArgument.TextureColor;
      MdxRender.Dev.TextureState[1].ColorOperation = TextureOperation.Modulate;
      MdxRender.Dev.TextureState[1].TextureTransform = TextureTransform.Disable;
      MdxRender.Dev.TextureState[1].TextureCoordinateIndex = 0;

      if(Stage2TextureIndex != -1)
      {
        MdxRender.SM.m_TextureManager.ActivateTexture(2, Stage2TextureIndex, 0);
        MdxRender.Dev.TextureState[2].AlphaArgument1 = TextureArgument.TextureColor;
        MdxRender.Dev.TextureState[2].AlphaOperation = TextureOperation.SelectArg1;
        MdxRender.Dev.TextureState[2].ColorArgument1 = TextureArgument.Current;
        MdxRender.Dev.TextureState[2].ColorArgument2 = TextureArgument.TextureColor;
        MdxRender.Dev.TextureState[2].TextureCoordinateIndex = 0;
        MdxRender.Dev.TextureState[2].TextureTransform = TextureTransform.Count2;
        MdxRender.Dev.Transform.Texture2 = detailMapTransform;

        SetDetailBlendFunction(this.Detail_function, 2);
      }
      else
      {
        MdxRender.Dev.TextureState[2].ColorOperation = TextureOperation.Disable;
        MdxRender.Dev.TextureState[2].AlphaOperation = TextureOperation.Disable;
        MdxRender.Dev.TextureState[2].TextureTransform = TextureTransform.Disable;
      }
      MdxRender.Dev.TextureState[3].AlphaOperation = TextureOperation.Disable;
      MdxRender.Dev.TextureState[3].ColorOperation = TextureOperation.Disable;
      MdxRender.Dev.TextureState[4].AlphaOperation = TextureOperation.Disable;
      MdxRender.Dev.TextureState[4].ColorOperation = TextureOperation.Disable;
      DebugFixedFunctionShader();
    }
    public void Pass2()
    {
      //lighting
      MdxRender.SM.m_TextureManager.ActivateTexture(0, Stage1TextureIndex, 0);

      MdxRender.Dev.RenderState.BlendFactor = Color.Gray;
      MdxRender.Dev.RenderState.SourceBlend = Blend.BlendFactor;
      MdxRender.Dev.RenderState.DestinationBlend = Blend.InvBlendFactor;
      MdxRender.Dev.RenderState.AlphaBlendEnable = true;
      MdxRender.Dev.TextureState[0].ColorArgument1 = TextureArgument.Current;
      MdxRender.Dev.TextureState[0].ColorArgument2 = TextureArgument.Diffuse;
      MdxRender.Dev.TextureState[0].AlphaOperation = TextureOperation.Disable;
      MdxRender.Dev.TextureState[0].ColorOperation = TextureOperation.ModulateAlphaAddColor;
      MdxRender.Dev.TextureState[1].ColorOperation = TextureOperation.Disable;
    }
  }
  public class SCROLLING_MAP
  {
    public ushort animation_source;
    public ushort animation_function;
    public float animation_period;
    public float animation_phase;
    public float animation_scale;

    public void Load(ref BinaryReader br)
    {
      animation_source = br.ReadUInt16();
      animation_function = br.ReadUInt16();
      animation_period = br.ReadSingle();
      animation_phase = br.ReadSingle();
      animation_scale = br.ReadSingle();
    }
  }
}
