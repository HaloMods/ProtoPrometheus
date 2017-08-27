using System.IO;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Prometheus.Core.Render;
using TagLibrary.Halo1;

namespace Prometheus.Core.Tags
{
	//declare your "structure" classes here, use all caps for these class names
	//this way we know it is a simple structure class, not something more complex like the entire tag

	//ie, SOSO_HEADER

	/// <summary>
	/// Summary description for TagSwat.
	/// </summary>
  public class TagSchi : ShaderBase
	{
	  ShaderTransparentChicago m_Schi = new ShaderTransparentChicago();
    private Matrix textureTranslationMatrix = Matrix.Identity;
    private Matrix[] mapTransforms;

    private int Stage1ColorOp;
    private int Stage1AlphaOp;
    private bool Stage1AlphaReplicate = false;
    private int Stage2ColorOp;
    private int Stage2AlphaOp;
    private bool Stage2AlphaReplicate = false;

    public bool TwoSided
    {
      get
      {
        return(m_Schi.ShaderTransparentChicagoValues.Flags.GetFlag(3));
      }
    }
		public void Load(TagFileName tfn)
		{
      LoadTagBuffer(tfn);
		  BinaryReader br = new BinaryReader(this.Stream);
      m_Schi.Read(br);
      m_Schi.ReadChildData(br);
      br.Close();

      int map_count = m_Schi.ShaderTransparentChicagoValues.Maps.Count;
      mapTransforms = new Matrix[map_count];

      for(int i=0; i<map_count; i++)
      {
        float u_scale = m_Schi.ShaderTransparentChicagoValues.Maps[i].Map.Value;
        float v_scale = m_Schi.ShaderTransparentChicagoValues.Maps[i].Map2.Value;

        mapTransforms[i] = new Matrix();
        mapTransforms[i].Scale(u_scale, v_scale, 1);
      }

      Stage1ColorOp = m_Schi.ShaderTransparentChicagoValues.Maps[0].ColorFunction.Value;
      Stage1AlphaOp = m_Schi.ShaderTransparentChicagoValues.Maps[0].AlphaFunction.Value;
      Stage1AlphaReplicate = m_Schi.ShaderTransparentChicagoValues.Maps[0].Flags.GetFlag(2);

      if(map_count > 1)
      {
        Stage2ColorOp = m_Schi.ShaderTransparentChicagoValues.Maps[1].ColorFunction.Value;
        Stage2AlphaOp = m_Schi.ShaderTransparentChicagoValues.Maps[1].AlphaFunction.Value;
        Stage2AlphaReplicate = m_Schi.ShaderTransparentChicagoValues.Maps[1].Flags.GetFlag(2);
      }
    }

    public void UpdateShaderAnimation()
    {
      for(int i=0; i<m_Schi.ShaderTransparentChicagoValues.Maps.Count; i++)
      {
        //check for "slide" animation function
        if(m_Schi.ShaderTransparentChicagoValues.Maps[i]._unnamed6.Value == 6)
        {
          float uScroll = 1.0f / (m_Schi.ShaderTransparentChicagoValues.Maps[i]._unnamed7.Value * MdxRender.CurrentFPS);
          mapTransforms[i].M31 += uScroll;
          if (mapTransforms[i].M31 > 1) mapTransforms[i].M31 -= 1;
        }
      }
    }

    public void TwoStage_LoadTextures()
    {
      string map1, map2;

      map1 = m_Schi.ShaderTransparentChicagoValues.Maps[0].Map5.Value;
      Stage1TextureIndex = MdxRender.SM.m_TextureManager.RegisterTexture(new TagFileName(map1, "bitm", this.m_PromHeader.GameVersion));
      if(m_Schi.ShaderTransparentChicagoValues.Maps.Count > 1)
      {
        map2 = m_Schi.ShaderTransparentChicagoValues.Maps[1].Map5.Value;
        Stage2TextureIndex = MdxRender.SM.m_TextureManager.RegisterTexture(new TagFileName(map2, "bitm", this.m_PromHeader.GameVersion));
        textureTranslationMatrix.Scale(
          m_Schi.ShaderTransparentChicagoValues.Maps[1].Map.Value,
          m_Schi.ShaderTransparentChicagoValues.Maps[1].Map2.Value, 1);
      }
    }
    public void TwoStage_Pass1()
    {
      SetFramebufferBlendFunction(m_Schi.ShaderTransparentChicagoValues.FramebufferBlendFunction.Value);
      MdxRender.Dev.RenderState.AlphaBlendEnable = true;
      MdxRender.Dev.RenderState.AlphaTestEnable = m_Schi.ShaderTransparentChicagoValues.Flags.GetFlag(1);

//      MdxRender.Dev.RenderState.SourceBlend = Blend.SourceAlpha;
//      MdxRender.Dev.RenderState.DestinationBlend = Blend.InvSourceAlpha;
//      MdxRender.Dev.RenderState.AlphaTestEnable = true;
//      MdxRender.Dev.RenderState.AlphaBlendEnable = true;

      if(this.TwoSided)
        MdxRender.Dev.RenderState.CullMode = Cull.None;
      else
        MdxRender.Dev.RenderState.CullMode = Cull.CounterClockwise;

      if(Stage2TextureIndex != -1)
      {
        MdxRender.SM.m_TextureManager.ActivateTexture(0, Stage2TextureIndex, 0);
        SetTextureTransform(0, mapTransforms[1]);
        SetColorOperation(0, Stage2ColorOp, Stage2AlphaReplicate);
        SetAlphaOperation(0, Stage2AlphaOp, Stage2AlphaReplicate);

        MdxRender.SM.m_TextureManager.ActivateTexture(1, Stage1TextureIndex, 0);
        SetTextureTransform(1, mapTransforms[0]);
        SetColorOperation(1, Stage1ColorOp, Stage1AlphaReplicate);
        SetAlphaOperation(1, Stage1AlphaOp, Stage1AlphaReplicate);

        MdxRender.Dev.TextureState[2].ColorOperation = TextureOperation.Disable;
        MdxRender.Dev.TextureState[2].AlphaOperation = TextureOperation.Disable;
      }
      else
      {
        MdxRender.SM.m_TextureManager.ActivateTexture(0, Stage1TextureIndex, 0);
        SetTextureTransform(0, mapTransforms[0]);
        SetColorOperation(0, Stage1ColorOp, Stage1AlphaReplicate);
        SetAlphaOperation(0, Stage1AlphaOp, Stage1AlphaReplicate);

        MdxRender.Dev.TextureState[1].ColorOperation = TextureOperation.Disable;
        MdxRender.Dev.TextureState[1].AlphaOperation = TextureOperation.Disable;
        MdxRender.Dev.TextureState[2].ColorOperation = TextureOperation.Disable;
        MdxRender.Dev.TextureState[2].AlphaOperation = TextureOperation.Disable;
      }
      
//      if(Stage2TextureIndex != -1)
//      {
//        //maps are reversed (I don't know why the hell they did that), switch the order
//
//        //stage 1 setup
//        MdxRender.SM.m_TextureManager.ActivateTexture(0, Stage2TextureIndex, 0);
//        SetTextureTransform(0, mapTransforms[1]);
//        MdxRender.Dev.TextureState[0].ColorArgument1 = TextureArgument.TextureColor;
//        MdxRender.Dev.TextureState[0].ColorArgument2 = TextureArgument.TextureColor;
//        MdxRender.Dev.TextureState[0].ColorOperation = TextureOperation.SelectArg1;
//        MdxRender.Dev.TextureState[0].AlphaArgument1 = TextureArgument.TextureColor;
//        MdxRender.Dev.TextureState[0].AlphaArgument2 = TextureArgument.TextureColor;
//        MdxRender.Dev.TextureState[0].AlphaOperation = TextureOperation.SelectArg1;
//
//        //stage 2 setup
//        MdxRender.SM.m_TextureManager.ActivateTexture(1, Stage1TextureIndex, 0);
//        SetTextureTransform(1, mapTransforms[0]);
//        MdxRender.Dev.TextureState[1].ColorArgument1 = TextureArgument.Current;
//        MdxRender.Dev.TextureState[1].ColorArgument2 = TextureArgument.TextureColor;
//        MdxRender.Dev.TextureState[1].ColorOperation = TextureOperation.ModulateInvAlphaAddColor;
//        MdxRender.Dev.TextureState[1].AlphaArgument1 = TextureArgument.Current;
//        MdxRender.Dev.TextureState[1].AlphaArgument2 = TextureArgument.TextureColor;
//        MdxRender.Dev.TextureState[1].AlphaOperation = TextureOperation.Modulate;
//        MdxRender.Dev.TextureState[1].TextureCoordinateIndex = 0;
//
//        MdxRender.Dev.TextureState[2].ColorOperation = TextureOperation.Disable;
//        MdxRender.Dev.TextureState[2].AlphaOperation = TextureOperation.Disable;
//      }
//      else
//      {
//        MdxRender.SM.m_TextureManager.ActivateTexture(0, Stage1TextureIndex, 0);
//        MdxRender.Dev.TextureState[0].ColorArgument1 = TextureArgument.TextureColor;
//        MdxRender.Dev.TextureState[0].ColorArgument2 = TextureArgument.TextureColor;
//        MdxRender.Dev.TextureState[0].ColorOperation = TextureOperation.SelectArg1;
//        MdxRender.Dev.TextureState[0].AlphaArgument1 = TextureArgument.TextureColor;
//        MdxRender.Dev.TextureState[0].AlphaArgument2 = TextureArgument.TextureColor;
//        MdxRender.Dev.TextureState[0].AlphaOperation = TextureOperation.SelectArg1;
//        SetTextureTransform(0, mapTransforms[0]);
//     
//        MdxRender.Dev.TextureState[1].ColorOperation = TextureOperation.Disable;
//        MdxRender.Dev.TextureState[1].AlphaOperation = TextureOperation.Disable;
//        MdxRender.Dev.TextureState[2].ColorOperation = TextureOperation.Disable;
//        MdxRender.Dev.TextureState[2].AlphaOperation = TextureOperation.Disable;
//      }

      //DebugFixedFunctionShader();
    }
  }
}
