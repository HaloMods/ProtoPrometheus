using System;
using System.IO;
using TagLibrary.Halo1;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Prometheus.Core.Render;

namespace Prometheus.Core.Tags
{
	//declare your "structure" classes here, use all caps for these class names
	//this way we know it is a simple structure class, not something more complex like the entire tag

	//ie, SOSO_HEADER

	/// <summary>
	/// Summary description for TagSgla.
	/// </summary>
	public class TagSgla : ShaderBase
	{
    ShaderTransparentGlass m_Sgla = new ShaderTransparentGlass();

    public void Load(TagFileName tfn)
    {
      LoadTagBuffer(tfn);
      BinaryReader br = new BinaryReader(this.Stream);
      m_Sgla.Read(br);
      m_Sgla.ReadChildData(br);
      br.Close();
    }
    public void LoadTextures()
    {
      string diffuse_map, diffuse_detail_map, reflection_map, map4;

      reflection_map = m_Sgla.ShaderTransparentGlassValues.ReflectionMap.Value;
      diffuse_map = m_Sgla.ShaderTransparentGlassValues.DiffuseMap.Value;
      diffuse_detail_map = m_Sgla.ShaderTransparentGlassValues.DiffuseDetailMap.Value;

      if(diffuse_map != "")
      {
        Stage1TextureIndex = MdxRender.SM.m_TextureManager.RegisterTexture(new TagFileName(diffuse_map, "bitm", this.m_PromHeader.GameVersion));
      }

      if(reflection_map != "")
      {
        Stage2TextureIndex = MdxRender.SM.m_TextureManager.RegisterCubemap(new TagFileName(reflection_map, "bitm", this.m_PromHeader.GameVersion));
      }
    }
    public bool TwoSided
    {
      get
      {
        return(m_Sgla.ShaderTransparentGlassValues.Flags.GetFlag(3));
      }
    }
    public void Pass1()
    {
      MdxRender.Dev.RenderState.SourceBlend = Blend.BlendFactor;
      MdxRender.Dev.RenderState.DestinationBlend = Blend.InvBlendFactor;
      MdxRender.Dev.RenderState.AlphaBlendEnable = true;

      MdxRender.Dev.RenderState.AlphaTestEnable = m_Sgla.ShaderTransparentGlassValues.Flags.GetFlag(1);

      if(this.TwoSided)
        MdxRender.Dev.RenderState.CullMode = Cull.None;
      else
        MdxRender.Dev.RenderState.CullMode = Cull.CounterClockwise;

      MdxRender.SM.m_TextureManager.ActivateTexture(0, Stage1TextureIndex, 0);
      MdxRender.Dev.TextureState[0].ColorArgument1 = TextureArgument.TextureColor;
      MdxRender.Dev.TextureState[0].ColorArgument2 = TextureArgument.Diffuse;
      MdxRender.Dev.TextureState[0].ColorOperation = TextureOperation.Modulate;
      MdxRender.Dev.TextureState[0].AlphaArgument1 = TextureArgument.TextureColor;
      MdxRender.Dev.TextureState[0].AlphaArgument2 = TextureArgument.TextureColor;
      MdxRender.Dev.TextureState[0].AlphaOperation = TextureOperation.SelectArg1;
      //SetTextureTransform(0, mapTransforms[0]);

      MdxRender.SM.m_TextureManager.ActivateTexture(1, Stage2TextureIndex, 0);
      MdxRender.Dev.TextureState[1].ColorArgument1 = TextureArgument.TextureColor;
      MdxRender.Dev.TextureState[1].ColorArgument2 = TextureArgument.Current;
      MdxRender.Dev.TextureState[1].ColorOperation = TextureOperation.Add;
      MdxRender.Dev.TextureState[1].AlphaArgument1 = TextureArgument.TextureColor;
      MdxRender.Dev.TextureState[1].AlphaArgument2 = TextureArgument.Current;
      MdxRender.Dev.TextureState[1].AlphaOperation = TextureOperation.SelectArg1;
      MdxRender.Dev.TextureState[1].TextureTransform = TextureTransform.Count3;
      MdxRender.Dev.Transform.Texture1 = Matrix.Identity;
      MdxRender.Dev.TextureState[1].TextureCoordinateIndex = 0 | (int)TextureCoordinateIndex.CameraSpaceReflectionVector;


      for(int i=2; i<5; i++)
      {
        MdxRender.Dev.TextureState[i].ColorOperation = TextureOperation.Disable;
        MdxRender.Dev.TextureState[i].AlphaOperation = TextureOperation.Disable;
      }
      //DebugFixedFunctionShader();
    }
    public void DisableCubemapping()
    {
      MdxRender.Dev.TextureState[1].TextureTransform = TextureTransform.Disable;
      MdxRender.Dev.TextureState[1].TextureCoordinateIndex = 0 | (int)TextureCoordinateIndex.PassThru;
    }
    #region OLD
		//Radiosity Properties
		public ushort Radiosity_flags; // SingleParameterization = 1, IgnoreNormals = 2, TransparentLit = 3
		public ushort Detail_level; // High = 0, Medium = 1, Low = 2, Turd = 3
		public float Power;
		public COLOR Emitted = new COLOR();
		public COLOR Tint = new COLOR();

		// Physics Properties
		public ushort Material_Type;
        
		// Glass Shader
		public ushort Glass_flags; // add blah here

		// Background Tint Properties
		public COLOR Background_tint = new COLOR();
		DETAIL_MAP Background_tint_map = new DETAIL_MAP();

		// Reflection Properties
		public ushort Reflection_type;
		public float Perpendicular_brightness;
		public COLOR Perpendicular_color = new COLOR();
		public float Parallel_brightness;
		public COLOR Parallel_color = new COLOR();
		TAG_REFERENCE Reflection_map = new TAG_REFERENCE();
		DETAIL_MAP Bump_map = new DETAIL_MAP();

		// Diffuse Properties
		DETAIL_MAP Diffuse_map = new DETAIL_MAP();
		DETAIL_MAP Diffuse_detail_map = new DETAIL_MAP();

		// Specular Properties
		DETAIL_MAP Specular_map = new DETAIL_MAP();
		DETAIL_MAP Specular_detail_map = new DETAIL_MAP();


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
			Glass_flags = br.ReadUInt16();
			br.BaseStream.Position += 42;
			Background_tint.Load(ref br);
			Background_tint_map.Load(ref br);
			br.BaseStream.Position += 22;
			Reflection_type = br.ReadUInt16();
			Perpendicular_brightness = br.ReadSingle();
			Perpendicular_color.Load(ref br);
			Parallel_brightness = br.ReadSingle();
			Parallel_color.Load(ref br);
			Reflection_map.Load(ref br);
			Bump_map.Load(ref br);
			br.BaseStream.Position += 132;
			Diffuse_map.Load(ref br);
			Diffuse_detail_map.Load(ref br);
			br.BaseStream.Position += 32;
			Specular_map.Load(ref br);
			Specular_detail_map.Load(ref br);
		}
    #endregion
	}

}
