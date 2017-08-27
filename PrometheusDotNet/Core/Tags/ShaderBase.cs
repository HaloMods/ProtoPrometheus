using System;
using System.Drawing;
using Prometheus.Core.Render;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;

namespace Prometheus.Core.Tags
{
  public enum ShaderPass{First, Second};
  public class ShaderParms
  {
    public TextureOperation ColorOp;
    public TextureOperation AlphaOp;
    public TextureArgument ColorArg1;
    public TextureArgument ColorArg2;
    public TextureArgument AlphaArg1;
    public TextureArgument AlphaArg2;
  }
  public class ShaderBase : TagBase
  {
    //Shader Manager Stuff
    protected int NumTextureStages;
    protected bool bUseBaseShader = false;

    //set texture stages to default texture -- "good ol' pinky"
    protected int Stage1TextureIndex = -1;
    protected int Stage2TextureIndex = -1;
    protected int Stage3TextureIndex = -1;
    protected int Stage4TextureIndex = -1;
    protected int Stage5TextureIndex = -1;

    public static bool enableShaderOverride = false;
    public static bool enableAlphaBlend = false;
    public static bool enableAlphaTest = false;
    public static ShaderParms[] Stage = null;
    public static ShaderParms Stage1 = new ShaderParms();
    public static ShaderParms Stage2 = new ShaderParms();
    public static ShaderParms Stage3 = new ShaderParms();
    public int NumberOfPasses = 1;
    public static Blend SourceBlend = Blend.One;
    public static Blend DestinationBlend = Blend.One;

    public ShaderBase()
    {
    }
    static public void InitializeDebug()
    {
      Stage = new ShaderParms[4];
      Stage[0] = new ShaderParms();
      Stage[1] = new ShaderParms();
      Stage[2] = new ShaderParms();
      Stage[3] = new ShaderParms();

      for(int i=0; i<4; i++)
      {
        Stage[i].AlphaArg1 = TextureArgument.TextureColor;
        Stage[i].AlphaArg2 = TextureArgument.TextureColor;
        Stage[i].AlphaOp = TextureOperation.Disable;
        Stage[i].ColorArg1 = TextureArgument.TextureColor;
        Stage[i].ColorArg2 = TextureArgument.TextureColor;
        Stage[i].ColorOp = TextureOperation.Disable;
      }
    }
    virtual public void SetupBlendFunction()
    {
      //Default behavior is to use Texture0 with no blending

      MdxRender.Dev.RenderState.BlendFactor = Color.Gray;
      MdxRender.Dev.RenderState.SourceBlend = Blend.BlendFactor;
      MdxRender.Dev.RenderState.DestinationBlend = Blend.InvBlendFactor;
      MdxRender.Dev.RenderState.AlphaBlendEnable = true;
      MdxRender.Dev.TextureState[0].ColorArgument0 = TextureArgument.Current;
      MdxRender.Dev.TextureState[0].ColorArgument1 = TextureArgument.TextureColor;
      MdxRender.Dev.TextureState[0].ColorArgument2 = TextureArgument.Diffuse;
      MdxRender.Dev.TextureState[0].ColorOperation = TextureOperation.SelectArg2;
      MdxRender.Dev.TextureState[1].ColorOperation = TextureOperation.Disable;
    }
    public void EnableDefaultShading()
    {
      bUseBaseShader = true;
    }
    public virtual void SetStates()
    {
    }
    public virtual void RestoreStates()
    {
    }
    public virtual void ActivatePass1(ref TextureManager TexMgr)
    {
      if(bUseBaseShader)
      {
        TexMgr.ActivateTexture(0, 0, 0);
      }
      else
      {
        //Activate real textures
        if(NumTextureStages >= 1)
        {
          TexMgr.ActivateTexture(0, Stage1TextureIndex, 0);
        }

        if(NumTextureStages >= 2)
          TexMgr.ActivateTexture(1, Stage2TextureIndex, 0);
      }
    }
    virtual public void LoadTextures(ref TextureManager TexMgr, int NumTextureStages)
    {
    }
    public void SetDetailBlendFunction(int Operation, int TextureStage)
    {
      switch(Operation)
      {
        case 0:
          MdxRender.Dev.TextureState[TextureStage].ColorOperation = TextureOperation.Modulate2X;
          break;
        case 1:
          MdxRender.Dev.TextureState[TextureStage].ColorOperation = TextureOperation.Modulate;
          break;
        case 2:
          MdxRender.Dev.TextureState[TextureStage].ColorOperation = TextureOperation.AddSigned2X;
          break;
        default:
          MdxRender.Dev.TextureState[TextureStage].ColorOperation = TextureOperation.Disable;
          break;
      }
    }
    public void SetTextureTransform(int Stage, Matrix transform)
    {
      if(transform.M44 == -1)
      {
        MdxRender.Dev.TextureState[Stage].TextureTransform = TextureTransform.Disable;
      }
      else
      {
        //enable uv transform (2d)
        MdxRender.Dev.TextureState[Stage].TextureTransform = TextureTransform.Count2;
        
        switch(Stage)
        {
          case 0:
            MdxRender.Dev.Transform.Texture0 = transform;
            break;
          case 1:
            MdxRender.Dev.Transform.Texture1 = transform;
            break;
          case 2:
            MdxRender.Dev.Transform.Texture2 = transform;
            break;
          case 3:
            MdxRender.Dev.Transform.Texture3 = transform;
            break;
        }
      }
    }
    public void DebugFixedFunctionShader()
    {
      if(ShaderBase.enableShaderOverride)
      {
        MdxRender.Dev.RenderState.AlphaBlendEnable = enableAlphaBlend;
        MdxRender.Dev.RenderState.AlphaTestEnable = enableAlphaTest;
        MdxRender.Dev.RenderState.SourceBlend = SourceBlend;
        MdxRender.Dev.RenderState.DestinationBlend = DestinationBlend;

        for(int i=0; i<4; i++)
        {
          MdxRender.Dev.TextureState[i].ColorArgument1 = Stage[i].ColorArg1;
          MdxRender.Dev.TextureState[i].ColorArgument2 = Stage[i].ColorArg2;
          MdxRender.Dev.TextureState[i].ColorOperation = Stage[i].ColorOp;
          MdxRender.Dev.TextureState[i].AlphaArgument1 = Stage[i].AlphaArg1;
          MdxRender.Dev.TextureState[i].AlphaArgument2 = Stage[i].AlphaArg2;
          MdxRender.Dev.TextureState[i].AlphaOperation = Stage[i].AlphaOp;
        }
      }
    }
    public void SetColorOperation(int TextureStage, int Operation, bool bAlphaReplicate)
    {
      MdxRender.Dev.TextureState[TextureStage].ColorArgument1 = TextureArgument.TextureColor;
      if(bAlphaReplicate)
       MdxRender.Dev.TextureState[TextureStage].ColorArgument1 |= TextureArgument.AlphaReplicate;

      if(TextureStage == 0)
        MdxRender.Dev.TextureState[TextureStage].ColorArgument2 = TextureArgument.TextureColor;
      else
        MdxRender.Dev.TextureState[TextureStage].ColorArgument2 = TextureArgument.Current;

      switch(Operation)
      {
        case 0:
          //current
          MdxRender.Dev.TextureState[TextureStage].ColorOperation = TextureOperation.SelectArg1;
          break;
        case 1:
          //next_map
          MdxRender.Dev.TextureState[TextureStage].ColorOperation = TextureOperation.SelectArg2;
          break;
        case 2:
          //multiply
          MdxRender.Dev.TextureState[TextureStage].ColorOperation = TextureOperation.Modulate;
          break;
        case 3:
          //double_multiply
          MdxRender.Dev.TextureState[TextureStage].ColorOperation = TextureOperation.Modulate2X;
          break;
        case 4:
          //add
          MdxRender.Dev.TextureState[TextureStage].ColorOperation = TextureOperation.Add;
          break;
        
          //after this, i dunno wtf they are doing
        case 5:
          //add_signed_current
          MdxRender.Dev.TextureState[TextureStage].ColorOperation = TextureOperation.AddSigned;
          break;
        case 6:
          //add_signed_next_map
          MdxRender.Dev.TextureState[TextureStage].ColorOperation = TextureOperation.AddSigned;
          break;
        case 7:
          //subtract_current
          MdxRender.Dev.TextureState[TextureStage].ColorOperation = TextureOperation.Subtract;
          break;
        case 8:
          //subtract_current_next_map
          MdxRender.Dev.TextureState[TextureStage].ColorOperation = TextureOperation.Subtract;
          break;
        case 9:
          //blend_current_alpha
          MdxRender.Dev.TextureState[TextureStage].ColorOperation = TextureOperation.ModulateAlphaAddColor;
          break;
        case 10:
          //blend_current_alpha_inverse
          MdxRender.Dev.TextureState[TextureStage].ColorOperation = TextureOperation.ModulateInvAlphaAddColor;
          break;
        case 11:
          //blend_next_map_alpha
          MdxRender.Dev.TextureState[TextureStage].ColorOperation = TextureOperation.ModulateAlphaAddColor;
          break;
        case 12:
          //blend_next_map_alpha_inverse
          MdxRender.Dev.TextureState[TextureStage].ColorOperation = TextureOperation.ModulateInvAlphaAddColor;
          break;
      }
    }
    public void SetAlphaOperation(int TextureStage, int Operation, bool bAlphaReplicate)
    {
      MdxRender.Dev.TextureState[TextureStage].AlphaArgument1 = TextureArgument.TextureColor;
      if(bAlphaReplicate)
        MdxRender.Dev.TextureState[TextureStage].AlphaArgument1 |= TextureArgument.AlphaReplicate;

      if(TextureStage == 0)
        MdxRender.Dev.TextureState[TextureStage].AlphaArgument2 = TextureArgument.TextureColor;
      else
        MdxRender.Dev.TextureState[TextureStage].AlphaArgument2 = TextureArgument.Current;

      switch(Operation)
      {
        case 0:
          //current
          MdxRender.Dev.TextureState[TextureStage].AlphaOperation = TextureOperation.SelectArg1;
          break;
        case 1:
          //next_map
          MdxRender.Dev.TextureState[TextureStage].AlphaOperation = TextureOperation.SelectArg2;
          break;
        case 2:
          //multiply
          MdxRender.Dev.TextureState[TextureStage].AlphaOperation = TextureOperation.Modulate;
          break;
        case 3:
          //double_multiply
          MdxRender.Dev.TextureState[TextureStage].AlphaOperation = TextureOperation.Modulate2X;
          break;
        case 4:
          //add
          MdxRender.Dev.TextureState[TextureStage].AlphaOperation = TextureOperation.Add;
          break;
        case 5:
          //add_signed_current
          MdxRender.Dev.TextureState[TextureStage].AlphaOperation = TextureOperation.AddSigned;
          break;
        case 6:
          //add_signed_next_map
          MdxRender.Dev.TextureState[TextureStage].AlphaOperation = TextureOperation.AddSigned;
          break;
        case 7:
          //subtract_current
          MdxRender.Dev.TextureState[TextureStage].AlphaOperation = TextureOperation.Subtract;
          break;
        case 8:
          //subtract_current_next_map
          MdxRender.Dev.TextureState[TextureStage].AlphaOperation = TextureOperation.Subtract;
          break;
        case 9:
          //blend_current_alpha
          MdxRender.Dev.TextureState[TextureStage].AlphaOperation = TextureOperation.ModulateAlphaAddColor;
          break;
        case 10:
          //blend_current_alpha_inverse
          MdxRender.Dev.TextureState[TextureStage].AlphaOperation = TextureOperation.ModulateInvAlphaAddColor;
          break;
        case 11:
          //blend_next_map_alpha
          MdxRender.Dev.TextureState[TextureStage].AlphaOperation = TextureOperation.ModulateAlphaAddColor;
          break;
        case 12:
          //blend_next_map_alpha_inverse
          MdxRender.Dev.TextureState[TextureStage].AlphaOperation = TextureOperation.ModulateInvAlphaAddColor;
          break;
      }
    }
    public void SetFramebufferBlendFunction(int BlendMode)
    {
      switch(BlendMode)
      {
        case 0:
          //alpha blend
          MdxRender.Dev.RenderState.BlendOperation = BlendOperation.Add;
          MdxRender.Dev.RenderState.SourceBlend = Blend.SourceAlpha;
          MdxRender.Dev.RenderState.DestinationBlend = Blend.InvSourceAlpha;
          break;
        case 1:
          //multiply
          //MdxRender.Dev.RenderState.SourceBlend = Blend.SourceAlpha;
          //MdxRender.Dev.RenderState.SourceBlend = Blend.InvSourceAlpha;
          break;
        case 2:
          //double multiply
          //MdxRender.Dev.RenderState.SourceBlend = Blend.SourceAlpha;
          //MdxRender.Dev.RenderState.SourceBlend = Blend.InvSourceAlpha;
          break;
        case 3:
          //add
          MdxRender.Dev.RenderState.BlendOperation = BlendOperation.Add;
          MdxRender.Dev.RenderState.SourceBlend = Blend.One;
          MdxRender.Dev.RenderState.DestinationBlend = Blend.One;
          break;
      }
    }
    public void BypassStage(int stage)
    {
      MdxRender.Dev.TextureState[stage].ColorArgument2 = TextureArgument.Current;
      MdxRender.Dev.TextureState[stage].ColorOperation = TextureOperation.SelectArg2;
      MdxRender.Dev.TextureState[stage].AlphaArgument2 = TextureArgument.Current;
      MdxRender.Dev.TextureState[stage].AlphaOperation = TextureOperation.SelectArg2;
    }
  }
}
