using System;
using System.IO;
using Prometheus.Core.Render;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;
using TagLibrary.Halo1;
using System.Diagnostics;

namespace Prometheus.Core.Tags
{
	//declare your "structure" classes here, use all caps for these class names
	//this way we know it is a simple structure class, not something more complex like the entire tag

	/// <summary>
	/// Summary description for TagSenv.
	/// </summary>
	public class TagSenv : ShaderBase
	{
    private ShaderEnvironment Senv = new ShaderEnvironment();
    private int lightmapIndex = -1;
    private int lightmapSubIndex = -1;
    private Matrix primaryDetailTransform = new Matrix();
    private Matrix secondaryDetailTransform = new Matrix();
    private Matrix microDetailTransform = new Matrix();

		public void LoadTagData()
		{
      BinaryReader br = new BinaryReader(m_stream);
      Senv.Read(br);
      Senv.ReadChildData(br);

      float pdScale = Senv.ShaderEnvironmentValues.PrimaryDetailMapScale.Value;
      float sdScale = Senv.ShaderEnvironmentValues.SecondaryDetailMapScale.Value;
      float mScale = Senv.ShaderEnvironmentValues.MicroDetailMapScale.Value;
      primaryDetailTransform.Scale(pdScale, pdScale, 1);
      secondaryDetailTransform.Scale(sdScale, sdScale, 1);
      microDetailTransform.Scale(mScale, mScale, 1);
    }
    override public void LoadTextures(ref TextureManager TexMgr, int NumTextureStages)
    {
      this.NumTextureStages = NumTextureStages;

      string basemap = Senv.ShaderEnvironmentValues.BaseMap.Value;
      string primary_detail = Senv.ShaderEnvironmentValues.PrimaryDetailMap.Value;
      string secondary_detail = Senv.ShaderEnvironmentValues.SecondaryDetailMap.Value;
      string micro_detail = Senv.ShaderEnvironmentValues.MicroDetailMap.Value;

      this.Stage1TextureIndex = -1;
      this.Stage2TextureIndex = TexMgr.RegisterTexture(new TagFileName(Senv.ShaderEnvironmentValues.BaseMap.Value, "bitm", this.m_PromHeader.GameVersion));
      if(Senv.ShaderEnvironmentValues.PrimaryDetailMap.Value != "")
        this.Stage3TextureIndex = TexMgr.RegisterTexture(new TagFileName(Senv.ShaderEnvironmentValues.PrimaryDetailMap.Value, "bitm", this.m_PromHeader.GameVersion));
      if(Senv.ShaderEnvironmentValues.SecondaryDetailMap.Value != "")
        this.Stage4TextureIndex = TexMgr.RegisterTexture(new TagFileName(Senv.ShaderEnvironmentValues.SecondaryDetailMap.Value, "bitm", this.m_PromHeader.GameVersion));
      if(Senv.ShaderEnvironmentValues.MicroDetailMap.Value != "")
        this.Stage5TextureIndex = TexMgr.RegisterTexture(new TagFileName(Senv.ShaderEnvironmentValues.MicroDetailMap.Value, "bitm", this.m_PromHeader.GameVersion));

      Trace.WriteLine("index = " + Stage2TextureIndex.ToString() + "basemap:  " + basemap);
    }
    public void ActivateSenvLightmap(int Index, int SubIndex)
    {
      //incoming argument should be -1 if there is no lightmap
      lightmapIndex = Index;
      lightmapSubIndex = SubIndex;
    }
    virtual public void Pass1()
    {
      MdxRender.Dev.RenderState.AlphaBlendEnable = false;
      MdxRender.Dev.RenderState.AlphaTestEnable = true;

      if((Senv.ShaderEnvironmentValues.Flags.Value & 0x1) == 0)
      {
        MdxRender.Dev.RenderState.AlphaTestEnable = false;
      }
      else
      {
        MdxRender.Dev.RenderState.AlphaTestEnable = true;
        //MdxRender.Dev.RenderState.AlphaBlendEnable = true;
      }

      MdxRender.Dev.RenderState.SourceBlend = Blend.SourceAlpha;
      MdxRender.Dev.RenderState.DestinationBlend = Blend.InvSourceAlpha;
      MdxRender.Dev.RenderState.CullMode = Cull.CounterClockwise;

      //check to see if this shader uses a lightmap
      if(lightmapIndex != -1)
      {
        //base map, diffuse lighting
        MdxRender.SM.m_TextureManager.ActivateTexture(0, Stage2TextureIndex, 0);//basemap
        MdxRender.Dev.TextureState[0].ColorArgument1 = TextureArgument.Diffuse;
        MdxRender.Dev.TextureState[0].ColorArgument2 = TextureArgument.TextureColor;
        MdxRender.Dev.TextureState[0].ColorOperation = TextureOperation.Modulate;
        MdxRender.Dev.TextureState[0].AlphaArgument1 = TextureArgument.TextureColor;
        MdxRender.Dev.TextureState[0].AlphaArgument2 = TextureArgument.TextureColor;
        MdxRender.Dev.TextureState[0].AlphaOperation = TextureOperation.SelectArg1;
        MdxRender.Dev.TextureState[0].TextureCoordinateIndex = 0;
        MdxRender.Dev.TextureState[0].TextureTransform = TextureTransform.Disable;

        if(Senv.ShaderEnvironmentValues.Type.Value == 1)
        {
          MdxRender.SM.m_TextureManager.ActivateTexture(1, Stage3TextureIndex, 0);//primary detail
          MdxRender.SM.m_TextureManager.ActivateTexture(2, Stage4TextureIndex, 0);//secondary detail
          MdxRender.SM.m_TextureManager.ActivateTexture(4, lightmapIndex, lightmapSubIndex);//lightmap
          MdxRender.SM.m_TextureManager.ActivateTexture(3, Stage5TextureIndex, 0);//micro detail

          //primary detail map
          if(Stage3TextureIndex == -1)
          {
            BypassStage(1);
          }
          else
          {
            MdxRender.Dev.TextureState[1].ColorArgument1 = TextureArgument.TextureColor;
            MdxRender.Dev.TextureState[1].ColorArgument2 = TextureArgument.Current;
            MdxRender.Dev.TextureState[1].ColorOperation = TextureOperation.Modulate2X;
            MdxRender.Dev.TextureState[1].AlphaArgument1 = TextureArgument.TextureColor;
            MdxRender.Dev.TextureState[1].AlphaArgument2 = TextureArgument.Current;
            MdxRender.Dev.TextureState[1].AlphaOperation = TextureOperation.SelectArg2;
            MdxRender.Dev.TextureState[1].TextureCoordinateIndex = 0;
            MdxRender.Dev.TextureState[1].TextureTransform = TextureTransform.Count2;
            MdxRender.Dev.Transform.Texture1 = primaryDetailTransform;
          }

          //secondary detail map
          if(Stage4TextureIndex == -1)
          {
            BypassStage(2);
          }
          else
          {
            MdxRender.Dev.TextureState[2].ColorArgument1 = TextureArgument.Current;
            MdxRender.Dev.TextureState[2].ColorArgument2 = TextureArgument.TextureColor;
            MdxRender.Dev.TextureState[2].ColorOperation = TextureOperation.BlendCurrentAlpha;
            MdxRender.Dev.TextureState[2].AlphaArgument1 = TextureArgument.TextureColor;
            MdxRender.Dev.TextureState[2].AlphaArgument2 = TextureArgument.Current;
            MdxRender.Dev.TextureState[2].AlphaOperation = TextureOperation.SelectArg2;
            MdxRender.Dev.TextureState[2].TextureCoordinateIndex = 0;
            MdxRender.Dev.TextureState[2].TextureTransform = TextureTransform.Count2;
            MdxRender.Dev.Transform.Texture2 = secondaryDetailTransform;
          }

          if(Stage5TextureIndex == -1)
          {
            BypassStage(3);
          }
          else
          {
            MdxRender.Dev.TextureState[3].ColorArgument1 = TextureArgument.Current;
            MdxRender.Dev.TextureState[3].ColorArgument2 = TextureArgument.TextureColor;
            MdxRender.Dev.TextureState[3].ColorOperation = TextureOperation.Modulate;
            MdxRender.Dev.TextureState[3].AlphaArgument1 = TextureArgument.TextureColor;
            MdxRender.Dev.TextureState[3].AlphaArgument2 = TextureArgument.Current;
            MdxRender.Dev.TextureState[3].AlphaOperation = TextureOperation.Disable;
            MdxRender.Dev.TextureState[3].TextureCoordinateIndex = 0;
            MdxRender.Dev.TextureState[3].TextureTransform = TextureTransform.Count2;
            MdxRender.Dev.Transform.Texture3 = microDetailTransform;
          }

          //blend lightmap
          MdxRender.Dev.TextureState[4].ColorArgument1 = TextureArgument.TextureColor;
          MdxRender.Dev.TextureState[4].ColorArgument2 = TextureArgument.Current;
          MdxRender.Dev.TextureState[4].ColorOperation = TextureOperation.Modulate2X;
          MdxRender.Dev.TextureState[4].AlphaArgument1 = TextureArgument.TextureColor;
          MdxRender.Dev.TextureState[4].AlphaArgument2 = TextureArgument.TextureColor;
          MdxRender.Dev.TextureState[4].AlphaOperation = TextureOperation.Disable;
          MdxRender.Dev.TextureState[4].TextureCoordinateIndex = 1;
          MdxRender.Dev.TextureState[4].TextureTransform = TextureTransform.Disable;
        }
        else
        {
          MdxRender.SM.m_TextureManager.ActivateTexture(1, lightmapIndex, lightmapSubIndex);//lightmap
          MdxRender.Dev.TextureState[1].ColorArgument1 = TextureArgument.TextureColor;
          MdxRender.Dev.TextureState[1].ColorArgument2 = TextureArgument.Current;
          MdxRender.Dev.TextureState[1].ColorOperation = TextureOperation.Modulate;
          MdxRender.Dev.TextureState[1].AlphaArgument1 = TextureArgument.TextureColor;
          MdxRender.Dev.TextureState[1].AlphaArgument2 = TextureArgument.TextureColor;
          MdxRender.Dev.TextureState[1].AlphaOperation = TextureOperation.Disable;
          MdxRender.Dev.TextureState[1].TextureCoordinateIndex = 1;
          MdxRender.Dev.TextureState[1].TextureTransform = TextureTransform.Disable;

          MdxRender.Dev.TextureState[2].ColorOperation = TextureOperation.Disable;
          MdxRender.Dev.TextureState[2].AlphaOperation = TextureOperation.Disable;
          MdxRender.Dev.TextureState[3].ColorOperation = TextureOperation.Disable;
          MdxRender.Dev.TextureState[3].AlphaOperation = TextureOperation.Disable;
        }

        //DebugFixedFunctionShader();
      }
      else
      {
        MdxRender.SM.m_TextureManager.ActivateTexture(0, Stage2TextureIndex, 0);

        //alpha blend lit material
        MdxRender.Dev.TextureState[0].AlphaArgument1 = TextureArgument.TextureColor;
        MdxRender.Dev.TextureState[0].AlphaOperation = TextureOperation.SelectArg1;
        MdxRender.Dev.TextureState[0].ColorArgument1 = TextureArgument.Diffuse;
        MdxRender.Dev.TextureState[0].ColorArgument2 = TextureArgument.TextureColor;
        MdxRender.Dev.TextureState[0].ColorOperation = TextureOperation.ModulateInvAlphaAddColor;
        MdxRender.Dev.TextureState[0].TextureTransform = TextureTransform.Disable;

        MdxRender.Dev.TextureState[1].ColorOperation = TextureOperation.Disable;
        MdxRender.Dev.TextureState[1].AlphaOperation = TextureOperation.Disable;
        MdxRender.Dev.TextureState[2].ColorOperation = TextureOperation.Disable;
        MdxRender.Dev.TextureState[2].AlphaOperation = TextureOperation.Disable;
        MdxRender.Dev.TextureState[3].ColorOperation = TextureOperation.Disable;
        MdxRender.Dev.TextureState[3].AlphaOperation = TextureOperation.Disable;
      }
    }
  }
	public class DETAIL_MAP
	{
		public float Scale;
		public TAG_REFERENCE Map = new TAG_REFERENCE();

		public void Load(ref BinaryReader br)
		{
			Scale = br.ReadSingle();
			Map.Load(ref br);
		}
	}
}
