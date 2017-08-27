using System;
using System.IO;
using System.Diagnostics;
using Prometheus.Core.Render;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;
using Core.TagSystem.TagDefinitions;

namespace Prometheus.Core.Tags
{
	//declare your "structure" classes here, use all caps for these class names
	//this way we know it is a simple structure class, not something more complex like the entire tag

	//ie, SOSO_HEADER

	/// <summary>
	/// Summary description for TagSwat.
	/// </summary>
	public class TagScex : ShaderBase
	{
    private ShaderTransparentChicagoExtended m_Scex = new ShaderTransparentChicagoExtended();
    private Matrix[] mapTransforms;
    private bool bUseFourStage = false;
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
        return(m_Scex.ShaderTransparentChicagoExtendedValues.Flags.GetFlag(3));
      }
    }
    public void Load(TagFileName tfn)
    {
      LoadTagBuffer(tfn);
      BinaryReader br = new BinaryReader(this.Stream);
      m_Scex.Read(br);
      m_Scex.ReadChildData(br);
      br.Close();

      int two_stage_count = m_Scex.ShaderTransparentChicagoExtendedValues.TwoStageMaps.Count;
      int four_stage_count = m_Scex.ShaderTransparentChicagoExtendedValues.FourStageMaps.Count;

      //print out debug info
      Trace.WriteLine("scex name:  " + tfn.RelativePath);
      Trace.WriteLine("Framebuffer Blend Mode = " + m_Scex.ShaderTransparentChicagoExtendedValues.FramebufferBlendFunction.Value.ToString());
      for(int i=0; i<two_stage_count; i++)
        Trace.WriteLine(string.Format("(2): ColorOp[{0}] = {1}  AlphaOp = {2}  flags = {3:X}", i, 
          m_Scex.ShaderTransparentChicagoExtendedValues.TwoStageMaps[i].ColorFunction.Value.ToString(),
          m_Scex.ShaderTransparentChicagoExtendedValues.TwoStageMaps[i].AlphaFunction.Value.ToString(),
          m_Scex.ShaderTransparentChicagoExtendedValues.TwoStageMaps[i].Flags.Value
          ));
      
      Trace.WriteLine("");
      for(int i=0; i<four_stage_count; i++)
        Trace.WriteLine(string.Format("(4): ColorOp[{0}] = {1}  AlphaOp = {2}  flags = {3:X}", i, 
          m_Scex.ShaderTransparentChicagoExtendedValues.FourStageMaps[i].ColorFunction.Value.ToString(),
          m_Scex.ShaderTransparentChicagoExtendedValues.FourStageMaps[i].AlphaFunction.Value.ToString(),
          m_Scex.ShaderTransparentChicagoExtendedValues.FourStageMaps[i].Flags.Value
          ));

      if(two_stage_count != 0)
      {
        int map_count = m_Scex.ShaderTransparentChicagoExtendedValues.TwoStageMaps.Count;
        mapTransforms = new Matrix[map_count];

        for(int i=0; i<map_count; i++)
        {
          float u_scale = m_Scex.ShaderTransparentChicagoExtendedValues.TwoStageMaps[i].Map.Value;
          float v_scale = m_Scex.ShaderTransparentChicagoExtendedValues.TwoStageMaps[i].Map2.Value;

          mapTransforms[i] = new Matrix();
          //if there is no scaling, then just leave matrix at null
          if((Math.Abs(1.0 - u_scale) > 0.001)||(Math.Abs(1.0 - u_scale) > 0.001))
            mapTransforms[i].Scale(u_scale, v_scale, 1);
          else
            mapTransforms[i] = Matrix.Identity;
        }

        Stage1ColorOp = m_Scex.ShaderTransparentChicagoExtendedValues.TwoStageMaps[0].ColorFunction.Value;
        Stage1AlphaOp = m_Scex.ShaderTransparentChicagoExtendedValues.TwoStageMaps[0].AlphaFunction.Value;
        Stage1AlphaReplicate = m_Scex.ShaderTransparentChicagoExtendedValues.TwoStageMaps[0].Flags.GetFlag(2);
        Stage2ColorOp = m_Scex.ShaderTransparentChicagoExtendedValues.TwoStageMaps[1].ColorFunction.Value;
        Stage2AlphaOp = m_Scex.ShaderTransparentChicagoExtendedValues.TwoStageMaps[1].AlphaFunction.Value;
        Stage2AlphaReplicate = m_Scex.ShaderTransparentChicagoExtendedValues.TwoStageMaps[1].Flags.GetFlag(2);
      }
      else
      {
        bUseFourStage = true;
        int map_count = m_Scex.ShaderTransparentChicagoExtendedValues.FourStageMaps.Count;
        mapTransforms = new Matrix[map_count];

        for(int i=0; i<map_count; i++)
        {
          float u_scale = m_Scex.ShaderTransparentChicagoExtendedValues.FourStageMaps[i].Map.Value;
          float v_scale = m_Scex.ShaderTransparentChicagoExtendedValues.FourStageMaps[i].Map2.Value;

          mapTransforms[i] = new Matrix();
          //if there is no scaling, then just leave matrix at null
          if((Math.Abs(1.0 - u_scale) > 0.001)||(Math.Abs(1.0 - u_scale) > 0.001))
            mapTransforms[i].Scale(u_scale, v_scale, 1);
          else
            mapTransforms[i] = Matrix.Identity;
        }

        Stage1ColorOp = m_Scex.ShaderTransparentChicagoExtendedValues.FourStageMaps[0].ColorFunction.Value;
        Stage1AlphaOp = m_Scex.ShaderTransparentChicagoExtendedValues.FourStageMaps[0].AlphaFunction.Value;
        Stage1AlphaReplicate = m_Scex.ShaderTransparentChicagoExtendedValues.FourStageMaps[0].Flags.GetFlag(2);
        Stage2ColorOp = m_Scex.ShaderTransparentChicagoExtendedValues.FourStageMaps[1].ColorFunction.Value;
        Stage2AlphaOp = m_Scex.ShaderTransparentChicagoExtendedValues.FourStageMaps[1].AlphaFunction.Value;
        Stage2AlphaReplicate = m_Scex.ShaderTransparentChicagoExtendedValues.FourStageMaps[1].Flags.GetFlag(2);
      }
    }

    public void LoadTextures(int NumTextureStages)
    {
      string map1, map2, map3, map4;

      int two_stage_count = m_Scex.ShaderTransparentChicagoExtendedValues.TwoStageMaps.Count;
      int four_stage_count = m_Scex.ShaderTransparentChicagoExtendedValues.FourStageMaps.Count;

      //by default, we use 2 stage map, but if that isn't set up, use 4 (like in anxiety map)
      if(two_stage_count == 0)
      {
        if(four_stage_count > 0)
        {
          map1 = m_Scex.ShaderTransparentChicagoExtendedValues.FourStageMaps[0].Map5.Value;
          Stage1TextureIndex = MdxRender.SM.m_TextureManager.RegisterTexture(new TagFileName(map1, "bitm", this.m_PromHeader.GameVersion));
        }

        if(four_stage_count > 1)
        {
          map2 = m_Scex.ShaderTransparentChicagoExtendedValues.FourStageMaps[1].Map5.Value;
          Stage2TextureIndex = MdxRender.SM.m_TextureManager.RegisterTexture(new TagFileName(map2, "bitm", this.m_PromHeader.GameVersion));
        }
      }
      else
      {
        if(two_stage_count > 0)
        {
          map1 = m_Scex.ShaderTransparentChicagoExtendedValues.TwoStageMaps[0].Map5.Value;
          Stage1TextureIndex = MdxRender.SM.m_TextureManager.RegisterTexture(new TagFileName(map1, "bitm", this.m_PromHeader.GameVersion));
        }

        if(two_stage_count > 1)
        {
          map2 = m_Scex.ShaderTransparentChicagoExtendedValues.TwoStageMaps[1].Map5.Value;
          Stage2TextureIndex = MdxRender.SM.m_TextureManager.RegisterTexture(new TagFileName(map2, "bitm", this.m_PromHeader.GameVersion));
        }

        if(two_stage_count > 2)
        {
          map3 = m_Scex.ShaderTransparentChicagoExtendedValues.TwoStageMaps[2].Map5.Value;
          Stage3TextureIndex = MdxRender.SM.m_TextureManager.RegisterTexture(new TagFileName(map3, "bitm", this.m_PromHeader.GameVersion));
        }
      }
    }

    public void Pass1()
    {
      SetFramebufferBlendFunction(m_Scex.ShaderTransparentChicagoExtendedValues.FramebufferBlendFunction.Value);
      MdxRender.Dev.RenderState.AlphaBlendEnable = true;
      MdxRender.Dev.RenderState.AlphaTestEnable = m_Scex.ShaderTransparentChicagoExtendedValues.Flags.GetFlag(1);

      MdxRender.Dev.TextureState[0].TextureCoordinateIndex = 0;
      MdxRender.Dev.TextureState[1].TextureCoordinateIndex = 0;

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

//        MdxRender.Dev.TextureState[0].ColorArgument1 = TextureArgument.TextureColor;
//        MdxRender.Dev.TextureState[0].ColorArgument2 = TextureArgument.TextureColor;
//        MdxRender.Dev.TextureState[0].ColorOperation = TextureOperation.SelectArg1;
//        MdxRender.Dev.TextureState[0].AlphaArgument1 = TextureArgument.TextureColor;
//        MdxRender.Dev.TextureState[0].AlphaArgument2 = TextureArgument.TextureColor;
//        MdxRender.Dev.TextureState[0].AlphaOperation = TextureOperation.SelectArg1;

//        MdxRender.Dev.TextureState[1].ColorArgument1 = TextureArgument.TextureColor;
//        MdxRender.Dev.TextureState[1].ColorArgument2 = TextureArgument.Current;
//        MdxRender.Dev.TextureState[1].ColorOperation = TextureOperation.ModulateAlphaAddColor;//works for sky
//        //MdxRender.Dev.TextureState[1].ColorOperation = TextureOperation.Add;//.Modulate;//works for teleporter
//        MdxRender.Dev.TextureState[1].AlphaArgument1 = TextureArgument.TextureColor;
//        MdxRender.Dev.TextureState[1].AlphaArgument2 = TextureArgument.Current;
//        MdxRender.Dev.TextureState[1].AlphaOperation = TextureOperation.SelectArg1;
      }
      else
      {
        MdxRender.SM.m_TextureManager.ActivateTexture(0, Stage1TextureIndex, 0);
        MdxRender.Dev.TextureState[0].AlphaArgument1 = TextureArgument.TextureColor;
        MdxRender.Dev.TextureState[0].AlphaOperation = TextureOperation.SelectArg1;
        MdxRender.Dev.TextureState[0].ColorArgument1 = TextureArgument.TextureColor;
        MdxRender.Dev.TextureState[0].ColorArgument2 = TextureArgument.AlphaReplicate;
        MdxRender.Dev.TextureState[0].ColorOperation = TextureOperation.ModulateInvAlphaAddColor;

        if(mapTransforms.Length > 0)
          SetTextureTransform(0, mapTransforms[0]);

        MdxRender.Dev.TextureState[1].ColorOperation = TextureOperation.Disable;
        MdxRender.Dev.TextureState[1].AlphaOperation = TextureOperation.Disable;
      }

      for(int i=2; i<6; i++)
      {
        MdxRender.Dev.TextureState[i].ColorOperation = TextureOperation.Disable;
        MdxRender.Dev.TextureState[i].AlphaOperation = TextureOperation.Disable;
      }
      DebugFixedFunctionShader();
    }
    public void UpdateShaderAnimation()
    {
      int map_count = m_Scex.ShaderTransparentChicagoExtendedValues.TwoStageMaps.Count;
      
      for(int i=0; i<map_count; i++)
      {
        short u_animation_function = m_Scex.ShaderTransparentChicagoExtendedValues.TwoStageMaps[i]._unnamed6.Value;
        float u_animation_period = m_Scex.ShaderTransparentChicagoExtendedValues.TwoStageMaps[i]._unnamed7.Value;
        //check to see if this map is animated
        if(u_animation_function == 6)
        {
          float uScroll = 1.0f / (u_animation_function * MdxRender.CurrentFPS* u_animation_period);
          mapTransforms[i].M31 += uScroll;
          if (mapTransforms[i].M31 > 1) mapTransforms[i].M31 -= 1;
        }

        short v_animation_function = m_Scex.ShaderTransparentChicagoExtendedValues.TwoStageMaps[i]._unnamed11.Value;
        float v_animation_period = m_Scex.ShaderTransparentChicagoExtendedValues.TwoStageMaps[i]._unnamed12.Value;
        if((v_animation_function == 6)||(v_animation_function == 0xb))
        {
          float vScroll = 1.0f / (v_animation_function * MdxRender.CurrentFPS * v_animation_period);
          mapTransforms[i].M32 += vScroll;
          if (mapTransforms[i].M32 > 0) mapTransforms[i].M32 -= 1;
        }

/*
        _map5.Read(reader);
        __unnamed4.Read(reader); 
        __unnamed5.Read(reader); u animation source
        __unnamed6.Read(reader); u animation function (slide = 6)
        __unnamed7.Read(reader); u period 
        __unnamed8.Read(reader); u phase
        __unnamed9.Read(reader); u animation scale
        __unnamed10.Read(reader); v animation source
        __unnamed11.Read(reader); v animation function (slide = 6)
        __unnamed12.Read(reader); v period 
        __unnamed13.Read(reader); v phase
        __unnamed14.Read(reader); v animation scale
*/
      }
      // NOTE: Perform the texture scrolling here for now.
//      if(m_Scex.ShaderTransparentChicagoValues.TwoStageMaps[0].Map5.Value; > 1)
//      {
//        if(m_Scex.ShaderTransparentChicagoValues.Maps[1]._unnamed6.Value == 6)
//        {
//          float uScroll = 1.0f / (m_Schi.ShaderTransparentChicagoValues.Maps[1]._unnamed7.Value * MdxRender.CurrentFPS);
//          textureTranslationMatrix.M31 += uScroll;
//          if (textureTranslationMatrix.M32 > 1) textureTranslationMatrix.M32 -= 1;
//          MdxRender.Dev.SetTextureStageState(1, TextureStageStates.TextureTransform, (int)TextureTransform.Count2);
//          MdxRender.Dev.SetTransform(TransformType.Texture1, textureTranslationMatrix);
//        }
//      }
    }
    public void LoadTagData()
		{
		}
    override public void LoadTextures(ref TextureManager TexMgr, int NumTextureStages)
    {
    }
	}
}