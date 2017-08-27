using System;
using System.IO;
using TagLibrary.Halo1;
using Prometheus.Core.Render;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Drawing;

namespace Prometheus.Core.Tags
{
	//declare your "structure" classes here, use all caps for these class names
	//this way we know it is a simple structure class, not something more complex like the entire tag

	//ie, SOSO_HEADER

	/// <summary>
	/// Summary description for TagSwat.
	/// </summary>
	public class TagSwat : ShaderBase
	{
    public ShaderTransparentWater m_Swat = new ShaderTransparentWater();
    private Matrix[] rippleTransforms;
    private Material tint = new Material();

    public void Load(TagFileName tfn)
    {
      LoadTagBuffer(tfn);
      BinaryReader br = new BinaryReader(this.Stream);
      m_Swat.Read(br);
      m_Swat.ReadChildData(br);
      br.Close();

      tint.Diffuse = Color.LightSkyBlue;
      rippleTransforms = new Matrix[m_Swat.ShaderTransparentWaterValues.Ripples.Count];

      for(int i=0; i<m_Swat.ShaderTransparentWaterValues.Ripples.Count; i++)
      {
        float u_offset = m_Swat.ShaderTransparentWaterValues.Ripples[i].MapOffset.I;
        float v_offset = m_Swat.ShaderTransparentWaterValues.Ripples[i].MapOffset.K;

        rippleTransforms[i] = new Matrix();
        rippleTransforms[i] = Matrix.Identity;
        rippleTransforms[i].M31 = u_offset;
        rippleTransforms[i].M32 = v_offset;
        rippleTransforms[i].Scale(m_Swat.ShaderTransparentWaterValues.RippleScale.Value,
          m_Swat.ShaderTransparentWaterValues.RippleScale.Value, 0);

      }
    }
    public void UpdateShaderAnimation()
    {
      for(int i=0; i<m_Swat.ShaderTransparentWaterValues.Ripples.Count; i++)
      {
        float velocity = m_Swat.ShaderTransparentWaterValues.Ripples[i].AnimationVelocity.Value;
        float angle = m_Swat.ShaderTransparentWaterValues.Ripples[i].AnimationAngle.Value;
        float u_vel = velocity * (float)Math.Sin(angle);
        float v_vel = velocity * (float)Math.Cos(angle);

        if(Math.Abs(u_vel) > 0.001)
        {
          rippleTransforms[i].M31 += (u_vel*0.25f/MdxRender.CurrentFPS);
          if (rippleTransforms[i].M31 > 1) rippleTransforms[i].M31 -= 1;
        }
        if(Math.Abs(v_vel) > 0.001)
        {
          rippleTransforms[i].M31 += (v_vel*0.25f/MdxRender.CurrentFPS);
          if (rippleTransforms[i].M32 > 1) rippleTransforms[i].M32 -= 1;
        }
      }
    }
    public void LoadTextures()
    {
      string base_map, ripple_map, reflection_map;

      base_map = m_Swat.ShaderTransparentWaterValues.BaseMap.Value;
      reflection_map = m_Swat.ShaderTransparentWaterValues.ReflectionMap.Value;
      ripple_map = m_Swat.ShaderTransparentWaterValues.RippleMaps.Value;

      if(base_map != "")
        Stage1TextureIndex = MdxRender.SM.m_TextureManager.RegisterTexture(new TagFileName(base_map, "bitm", this.m_PromHeader.GameVersion));

      if(reflection_map != "")
        Stage2TextureIndex = MdxRender.SM.m_TextureManager.RegisterCubemap(new TagFileName(reflection_map, "bitm", this.m_PromHeader.GameVersion));

      if(ripple_map != "")
        Stage3TextureIndex = MdxRender.SM.m_TextureManager.RegisterTexture(new TagFileName(ripple_map, "bitm", this.m_PromHeader.GameVersion));
    }
    public void Pass1()
    {
      MdxRender.Dev.RenderState.SourceBlend = Blend.InvSourceColor;
      MdxRender.Dev.RenderState.DestinationBlend = Blend.DestinationAlpha;
      MdxRender.Dev.RenderState.AlphaBlendEnable = true;
      MdxRender.Dev.Material = tint;

      for(int i=0; i<m_Swat.ShaderTransparentWaterValues.Ripples.Count; i++)
      {
        MdxRender.SM.m_TextureManager.ActivateTexture(i, Stage3TextureIndex, 0);
        MdxRender.Dev.TextureState[i].TextureTransform = TextureTransform.Count2;
      }

      MdxRender.Dev.Transform.Texture0 = rippleTransforms[0];
      MdxRender.Dev.Transform.Texture1 = rippleTransforms[1];
      MdxRender.Dev.Transform.Texture2 = rippleTransforms[2];
      MdxRender.Dev.Transform.Texture3 = rippleTransforms[3];


      MdxRender.Dev.TextureState[0].ColorArgument1 = TextureArgument.TextureColor;
      MdxRender.Dev.TextureState[0].ColorArgument2 = TextureArgument.Current;
      MdxRender.Dev.TextureState[0].ColorOperation = TextureOperation.SelectArg1;
        
      MdxRender.Dev.TextureState[1].ColorArgument1 = TextureArgument.TextureColor;
      MdxRender.Dev.TextureState[1].ColorArgument2 = TextureArgument.Current;
      MdxRender.Dev.TextureState[1].ColorOperation = TextureOperation.Modulate;
        
      MdxRender.Dev.TextureState[2].ColorArgument1 = TextureArgument.TextureColor;
      MdxRender.Dev.TextureState[2].ColorArgument2 = TextureArgument.Current;
      MdxRender.Dev.TextureState[2].ColorOperation = TextureOperation.Add;
        
      MdxRender.Dev.TextureState[3].ColorArgument1 = TextureArgument.TextureColor;
      MdxRender.Dev.TextureState[3].ColorArgument2 = TextureArgument.Current;
      MdxRender.Dev.TextureState[3].ColorOperation = TextureOperation.DotProduct3;
        
      MdxRender.Dev.TextureState[4].AlphaArgument1 = TextureArgument.TextureColor;
      MdxRender.Dev.TextureState[4].AlphaArgument2 = TextureArgument.Diffuse;
      MdxRender.Dev.TextureState[4].AlphaOperation = TextureOperation.Add;

//      MdxRender.SM.m_TextureManager.ActivateTexture(4, Stage2TextureIndex, 0);
//      MdxRender.Dev.TextureState[4].ColorArgument1 = TextureArgument.TextureColor;
//      MdxRender.Dev.TextureState[4].ColorArgument2 = TextureArgument.Current;
//      MdxRender.Dev.TextureState[4].ColorOperation = TextureOperation.Modulate;
//      MdxRender.Dev.TextureState[4].AlphaArgument1 = TextureArgument.TextureColor;
//      MdxRender.Dev.TextureState[4].AlphaArgument2 = TextureArgument.Current;
//      MdxRender.Dev.TextureState[4].AlphaOperation = TextureOperation.SelectArg2;
//      MdxRender.Dev.TextureState[4].TextureTransform = TextureTransform.Count3;
//      MdxRender.Dev.Transform.Texture4 = Matrix.Identity;
//      MdxRender.Dev.TextureState[4].TextureCoordinateIndex = 0 | (int)TextureCoordinateIndex.CameraSpaceReflectionVector;

//      MdxRender.SM.m_TextureManager.ActivateTexture(4, Stage1TextureIndex, 0);
//      MdxRender.Dev.TextureState[4].ColorArgument1 = TextureArgument.TextureColor;
//      MdxRender.Dev.TextureState[4].ColorArgument2 = TextureArgument.Current;
//      MdxRender.Dev.TextureState[4].ColorOperation = TextureOperation.BlendCurrentAlpha;
//      MdxRender.Dev.TextureState[4].AlphaArgument1 = TextureArgument.TextureColor;
//      MdxRender.Dev.TextureState[4].AlphaArgument2 = TextureArgument.TextureColor;
//      MdxRender.Dev.TextureState[4].AlphaOperation = TextureOperation.SelectArg1;
//      //SetTextureTransform(0, mapTransforms[0]);


      for(int i=4; i<5; i++)
      {
        MdxRender.Dev.TextureState[i].ColorOperation = TextureOperation.Disable;
        MdxRender.Dev.TextureState[i].AlphaOperation = TextureOperation.Disable;
      }
      DebugFixedFunctionShader();
    }
	}
}
