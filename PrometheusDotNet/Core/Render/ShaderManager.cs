/* ---------------------------------------------------------------
 * Prometheus
 * Bungie Map-based Multi-Game Editing Kit
 * 2004-2005, Halo-Dev
 * ---------------------------------------------------------------
 * Class       : Prometheus.Core.Render.ModelManager
 * Description : This class gives us an easy way to register and
 *               access shaders and their component textures
 *               during rendering of models and bsps.
 * Author      : Grenadiac
 * Co-Authors  : MonoxideC
 * ---------------------------------------------------------------
 */
using System;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using Prometheus.Core.Tags;
using Microsoft.DirectX.Direct3D;

namespace Prometheus.Core.Render
{
  public enum UtilityShader {PX, NX, PY, NY, PZ, NZ, Vehicle, Scenery};
  public enum ShaderType {Soso, Senv, Schi, Scex, Smet, Sgla, Spla, Swat, Shad, Unknown};
  struct SHADER_LOOKUP_ELEMENT
  {
    public short index;
    public ShaderType type;
    public string name;
    public SHADER_LOOKUP_ELEMENT(string name)
    {
      this.index = -1;
      this.type = ShaderType.Unknown;
      this.name = new string(name.ToCharArray());
    }
  }
  /// <summary>
  /// Summary description for ShaderManager.
  /// </summary>
  public class ShaderManager
  {
    private const int UTIL_SHADER_MAX = 10;
    private const int NUM_ELEMENTS = 1000;
    private const int NUM_SHADERS = 2;
    private int LookupCount = UTIL_SHADER_MAX;
    private int MaxTextureStages = 1;
    //TODO: store device caps up here so we know how many texture stages we can support

    //TODO: create lookup table that stores shader name, shader type, index
    SHADER_LOOKUP_ELEMENT[] m_LookupTable = new SHADER_LOOKUP_ELEMENT[NUM_ELEMENTS];

    //TODO: contain arrays of shaders here (different arrays for each shader type?)
    ArrayList m_Utility = new ArrayList(20);
    ArrayList m_SosoArray = new ArrayList(100);
    ArrayList m_SenvArray = new ArrayList(100);
    ArrayList m_SchiArray = new ArrayList(20);

    ArrayList m_ScexArray = new ArrayList(20);
    ArrayList m_SmetArray = new ArrayList(20);
    ArrayList m_SglaArray = new ArrayList(20);
    ArrayList m_SplaArray = new ArrayList(20);
    ArrayList m_SwatArray = new ArrayList(20);
    ArrayList m_ShadArray = new ArrayList(20);

    public TextureManager m_TextureManager = null;

    public ShaderManager()
    {
      m_TextureManager = new TextureManager();
    }
    public ShaderType GetShaderType(string shader_type)
    {
      ShaderType type = ShaderType.Unknown;

      switch(shader_type)
      {
        case "soso":
          type = ShaderType.Soso;
          break;
				case "dahs":
        case "shad":
          type = ShaderType.Shad;
          break;
        case "senv":
          type = ShaderType.Senv;
          break;
        case "schi":
          type = ShaderType.Schi;
          break;
        case "scex":
          type = ShaderType.Scex;
          break;
        case "stem":
          type = ShaderType.Smet;
          break;
        case "sgla":
          type = ShaderType.Sgla;
          break;
        case "spla":
          type = ShaderType.Spla;
          break;
        case "swat":
          type = ShaderType.Swat;
          break;
      }

      return(type);
    }

    public int GetUtilShaderIndex(UtilityShader us)
    {
      int shader_index = 0;

      switch(us)
      {
        case UtilityShader.NX:
          shader_index = 1;
          break;
        case UtilityShader.NY:
          shader_index = 2;
          break;
        case UtilityShader.NZ:
          shader_index = 3;
          break;
        case UtilityShader.PX:
          shader_index = 4;
          break;
        case UtilityShader.PY:
          shader_index = 5;
          break;
        case UtilityShader.PZ:
          shader_index = 6;
          break;
      }

      return(shader_index);
    }
    /// <summary>
    /// Call RegisterShader when loading the model/bsp tag.  This should be called 
    /// before any rendering is performed.
    /// </summary>
    public int RegisterShader(TagFileName tfn)
    {
      int shader_index = -1;
      bool bShaderFound = false;

      //TODO:  use hash table?
      // search through existing lookup table to see if it is already loaded
      // TODO: Change this so that it doesn't loop thorugh the entire empty list of elements in the table.
      // There's really no need to declare the number of elements up front - maybe we should take that out.
      for(shader_index=UTIL_SHADER_MAX; shader_index<NUM_ELEMENTS; shader_index++)
      {
        if(m_LookupTable[shader_index].name == tfn.RelativePath)
        {
          bShaderFound = true;
          break;
        }
      }

      // if it isn't in the list, allocate it and load
      if(bShaderFound == false)
      {
        if(LookupCount >= NUM_ELEMENTS)
        {
          Trace.WriteLine("Shader Manager has registered maximum number of shaders: " + LookupCount.ToString());
          return(-1);
        }

        m_LookupTable[LookupCount] = new SHADER_LOOKUP_ELEMENT(tfn.RelativePath);
        m_LookupTable[LookupCount].type = GetShaderType(tfn.TagClass);

        //Load shaders and textures, update collections
        //determine which textures to load (depends on device caps if we load detail textures)
        //Trace.WriteLine("Loading shader: " + tfn.RelativePath);
        switch(m_LookupTable[LookupCount].type)
        {
          case ShaderType.Soso:
            LoadSosoShader(tfn);
            break;
          
          case ShaderType.Shad:
            LoadShadShader(tfn);
            break;

          case ShaderType.Senv:
            LoadSenvShader(tfn);
            break;

          case ShaderType.Schi:
            LoadSchiShader(tfn);
            break;

          case ShaderType.Scex:
            LoadScexShader(tfn);
            break;

          case ShaderType.Smet:
            LoadSmetShader(tfn);
            break;

          case ShaderType.Sgla:
            LoadSglaShader(tfn);
            break;

          case ShaderType.Spla:
            LoadSplaShader(tfn);
            break;

          case ShaderType.Swat:
            LoadSwatShader(tfn);
            break;
        }

        shader_index = LookupCount;
        LookupCount++;
      }

      return(shader_index);
    }
    private void ActivateUtilShader(int shader_index)
    {
      switch(shader_index)
      {
        case 0:
        case -1:
          MdxRender.SM.m_TextureManager.ActivateDefaultTexture();
          break;
        case 1:
          MdxRender.SM.m_TextureManager.ActivateTexture(0, 1, 0);
          break;
        case 2:
          MdxRender.SM.m_TextureManager.ActivateTexture(0, 2, 0);
          break;
        case 3:
          MdxRender.SM.m_TextureManager.ActivateTexture(0, 3, 0);
          break;
        case 4:
          MdxRender.SM.m_TextureManager.ActivateTexture(0, 4, 0);
          break;
        case 5:
          MdxRender.SM.m_TextureManager.ActivateTexture(0, 5, 0);
          break;
        case 6:
          MdxRender.SM.m_TextureManager.ActivateTexture(0, 6, 0);
          break;
      }
    }

    public void SetupStates(int shader_index)
    {
      if(shader_index >= 0)
      {
        // TEST CODE: Enable scrolling texture on transparant chicago shaders.
        if(m_LookupTable[shader_index].type == ShaderType.Schi)
        {
          TagSchi s = (TagSchi)m_SchiArray[m_LookupTable[shader_index].index];
          s.UpdateShaderAnimation();
        }
        else if(m_LookupTable[shader_index].type == ShaderType.Scex)
        {
          TagScex s = (TagScex)m_ScexArray[m_LookupTable[shader_index].index];
          s.UpdateShaderAnimation();
        }
        else if(m_LookupTable[shader_index].type == ShaderType.Swat)
        {
          TagSwat s = (TagSwat)m_SwatArray[m_LookupTable[shader_index].index];
          s.UpdateShaderAnimation();
        }
      }
    }

    public void RestoreStates(int shader_index)
    {
      switch(m_LookupTable[shader_index].type)
      {
        case ShaderType.Schi:
          TagSchi s = (TagSchi)m_SchiArray[m_LookupTable[shader_index].index];
          s.RestoreStates();
          break;
        case ShaderType.Soso:
          MdxRender.Dev.TextureState[1].ColorOperation = TextureOperation.Disable;
          MdxRender.Dev.TextureState[3].TextureTransform = TextureTransform.Disable;
          MdxRender.Dev.TextureState[3].TextureCoordinateIndex = 0 | (int)TextureCoordinateIndex.PassThru;
          break;
        case ShaderType.Sgla:
          MdxRender.Dev.TextureState[1].TextureTransform = TextureTransform.Disable;
          MdxRender.Dev.TextureState[1].TextureCoordinateIndex = 0 | (int)TextureCoordinateIndex.PassThru;
          break;
      }
      if(m_LookupTable[shader_index].type == ShaderType.Schi)
      {
        // TEST CODE: Enable scrolling texture on transparant chicago shaders.
      }
    }

    public bool ActivateShader(int shader_index, int pass_count)
    {
      ShaderPass pass;

      if(pass_count == 0)
        pass = ShaderPass.First;
      else if(pass_count == 1)
        pass = ShaderPass.Second;
      else 
        return(false);

      return(ActivateShader(shader_index, pass));
    }
    /// <summary>
    /// Call ActivateShader from the model/bsp "draw" function.  It will set up
    /// the appropriate texture stages, etc.
    /// </summary>
    public bool ActivateShader(int shader_index, ShaderPass pass)
    {
      bool bDoneProcessing = true;

      if(shader_index < UTIL_SHADER_MAX)
      {
        ActivateUtilShader(shader_index);
      }
      else
      {
        switch(m_LookupTable[shader_index].type)
        {
          case ShaderType.Soso:
            TagSoso tsoso = (TagSoso)m_SosoArray[m_LookupTable[shader_index].index];
            if(pass == ShaderPass.First)
            {
              tsoso.Pass1();
              bDoneProcessing = true;
            }
            else if(pass == ShaderPass.Second)
            {
              tsoso.Pass2();
              bDoneProcessing = true;
            }
            break;

          case ShaderType.Shad:
            TagShad tshad = (TagShad)m_ShadArray[m_LookupTable[shader_index].index];
            tshad.SetupBlendFunction();
            tshad.ActivatePass1(ref m_TextureManager);
            break;

          case ShaderType.Senv:
            TagSenv tsenv = (TagSenv)m_SenvArray[m_LookupTable[shader_index].index];
            if(pass == ShaderPass.First)
            {
              tsenv.Pass1();
              bDoneProcessing = true;
            }
            break;

          case ShaderType.Schi:
            TagSchi tschi = (TagSchi)m_SchiArray[m_LookupTable[shader_index].index];
            if(pass == ShaderPass.First)
            {
              tschi.TwoStage_Pass1();
              bDoneProcessing = true;
            }
            break;

          case ShaderType.Scex:
            TagScex tscex = (TagScex)m_ScexArray[m_LookupTable[shader_index].index];
            if(pass == ShaderPass.First)
            {
              tscex.Pass1();
              bDoneProcessing = true;
            }
            break;
          case ShaderType.Sgla:
            TagSgla tsgla = (TagSgla)m_SglaArray[m_LookupTable[shader_index].index];
            if(pass == ShaderPass.First)
            {
              tsgla.Pass1();
              bDoneProcessing = true;
            }
            break;
          case ShaderType.Swat:
            TagSwat tswat = (TagSwat)m_SwatArray[m_LookupTable[shader_index].index];
            if(pass == ShaderPass.First)
            {
              tswat.Pass1();
              bDoneProcessing = true;
            }
            break;
          default:
            ShaderBase shdr = new ShaderBase();
            shdr.SetupBlendFunction();
            break;
        }
      }

      return(bDoneProcessing);
    }
    void LoadSosoShader(TagFileName tfn)
    {
      TagSoso shdr = new TagSoso();
      
      try
      {
        shdr.LoadTagBuffer(tfn);
        shdr.LoadTagData();
        //shdr.Load(tfn);
        shdr.LoadTextures(ref m_TextureManager, this.MaxTextureStages);
      }
      catch(Exception e)
      {
        Trace.WriteLine("Senv Shader Load failed: " + tfn.RelativePath + "("+e.Message+")");
        shdr.EnableDefaultShading();
      }

      m_LookupTable[LookupCount].index = (short)m_SosoArray.Count;
      m_SosoArray.Add(shdr);
    }
    void LoadShadShader(TagFileName tfn)
    {
      TagShad shdr = new TagShad();
      
      try
      {
        shdr.LoadTagBuffer(tfn);
        shdr.LoadTagData();
        shdr.LoadTextures(ref m_TextureManager, this.MaxTextureStages);
      }
      catch(Exception e)
      {
        Trace.WriteLine("Senv Shader Load failed: "+tfn.RelativePath + "("+e.Message+")");
        shdr.EnableDefaultShading();
      }

      m_LookupTable[LookupCount].index = (short)m_ShadArray.Count;
      m_ShadArray.Add(shdr);
    }
    void LoadSenvShader(TagFileName tfn)
    {
      TagSenv shdr = new TagSenv();
      
      try
      {
        shdr.LoadTagBuffer(tfn);
        shdr.LoadTagData();
        shdr.LoadTextures(ref m_TextureManager, this.MaxTextureStages);
      }
      catch(Exception e)
      {
        Trace.WriteLine("Senv Shader Load failed: "+tfn.RelativePath + "("+e.Message+")");
        shdr.EnableDefaultShading();
      }

      m_LookupTable[LookupCount].index = (short)m_SenvArray.Count;
      m_SenvArray.Add(shdr);
    }
    void LoadSchiShader(TagFileName tfn)
    {
      TagSchi shdr = new TagSchi();

      try
      {
        shdr.Load(tfn);
        shdr.TwoStage_LoadTextures();
      }
      catch(Exception e)
      {
        Trace.WriteLine("Schi Shader Load failed: "+tfn.RelativePath + "("+e.Message+")");
        shdr.EnableDefaultShading();
      }

      m_LookupTable[LookupCount].index = (short)m_SchiArray.Count;
      m_SchiArray.Add(shdr);
    }
    void LoadScexShader(TagFileName tfn)
    {
      TagScex shdr = new TagScex();
      
      try
      {
        shdr.Load(tfn);
        shdr.LoadTextures(this.MaxTextureStages);
      }
      catch(Exception e)
      {
        Trace.WriteLine("Scex Shader Load failed: "+tfn.RelativePath + "("+e.Message+")");
        shdr.EnableDefaultShading();
      }

      m_LookupTable[LookupCount].index = (short)m_ScexArray.Count;
      m_ScexArray.Add(shdr);
    }
    void LoadSmetShader(TagFileName tfn)
    {
      m_LookupTable[LookupCount].index = -1;
    }
    void LoadSglaShader(TagFileName tfn)
    {
      TagSgla shdr = new TagSgla();
      
      try
      {
        shdr.Load(tfn);
        shdr.LoadTextures();
      }
      catch(Exception e)
      {
        Trace.WriteLine("Sgla Shader Load failed: "+tfn.RelativePath + "("+e.Message+")");
        shdr.EnableDefaultShading();
      }

      m_LookupTable[LookupCount].index = (short)m_SglaArray.Count;
      m_SglaArray.Add(shdr);
    }
    void LoadSplaShader(TagFileName tfn)
    {
      m_LookupTable[LookupCount].index = -1;
    }
    void LoadSwatShader(TagFileName tfn)
    {
      TagSwat shdr = new TagSwat();
      
      try
      {
        shdr.Load(tfn);
        shdr.LoadTextures();
      }
      catch(Exception e)
      {
        Trace.WriteLine("Swat Shader Load failed: "+tfn.RelativePath + "("+e.Message+")");
        shdr.EnableDefaultShading();
      }

      m_LookupTable[LookupCount].index = (short)m_SwatArray.Count;
      m_SwatArray.Add(shdr);
    }
    public int LoadLightmaps(TagFileName tfn)
    {
      int lightmap_index = m_TextureManager.RegisterTexture(tfn);

      return(lightmap_index);
    }
		public int LoadLightmaps2(string lightmap_name)
		{
			int lightmap_index = m_TextureManager.RegisterTexture2(new TagFileName(lightmap_name, MapfileVersion.XHALO2));

			return(lightmap_index);
		}

    public TagSenv GetBspShader(int index)
    {
      TagSenv ts = null;

      if(m_LookupTable[index].type == ShaderType.Senv)
        ts = (TagSenv)m_SenvArray[m_LookupTable[index].index];

      return(ts);
    }
		public TagShad GetH2BspShader(int index)
		{
			TagShad ts = null;
			if(m_LookupTable[index].type == ShaderType.Shad)
				ts = (TagShad)m_ShadArray[m_LookupTable[index].index];
			return(ts);
		}
    public TagSchi GetSchiShader(int index)
    {
      TagSchi ts = null;
      if(m_LookupTable[index].type == ShaderType.Schi)
        ts = (TagSchi)m_SchiArray[m_LookupTable[index].index];
      return(ts);
    }
    public TagScex GetScexShader(int index)
    {
      TagScex ts = null;
      if(m_LookupTable[index].type == ShaderType.Scex)
        ts = (TagScex)m_ScexArray[m_LookupTable[index].index];
      return(ts);
    }
    public TagSoso GetSosoShader(int index)
    {
      TagSoso ts = null;
      if(m_LookupTable[index].type == ShaderType.Soso)
        ts = (TagSoso)m_SosoArray[m_LookupTable[index].index];
      return(ts);
    }
    public void DisableBlend()
    {
      MdxRender.Dev.VertexFormat = CustomVertex.PositionTextured.Format;
      for(int i=0; i<4; i++)
      {
        MdxRender.Dev.TextureState[i].AlphaArgument0 = TextureArgument.Current;
        MdxRender.Dev.TextureState[i].AlphaArgument1 = TextureArgument.TextureColor;
        MdxRender.Dev.TextureState[i].AlphaArgument2 = TextureArgument.Current;
        MdxRender.Dev.TextureState[i].AlphaOperation = TextureOperation.SelectArg1;

        MdxRender.Dev.TextureState[i].ColorArgument0 = TextureArgument.Current;
        MdxRender.Dev.TextureState[i].ColorArgument1 = TextureArgument.TextureColor;
        MdxRender.Dev.TextureState[i].ColorArgument2 = TextureArgument.Current;
        MdxRender.Dev.TextureState[i].ColorOperation = TextureOperation.SelectArg1;

        MdxRender.Dev.TextureState[i].TextureCoordinateIndex = 0;
      }
      MdxRender.Dev.TextureState[1].ColorOperation = TextureOperation.Disable;
      MdxRender.Dev.TextureState[2].ColorOperation = TextureOperation.Disable;
      MdxRender.Dev.TextureState[3].ColorOperation = TextureOperation.Disable;
    }
    public void ConfigureForDiffuseColor()
    {
      MdxRender.Dev.RenderState.AlphaBlendEnable = false;
      MdxRender.Dev.TextureState[0].ColorArgument1 = TextureArgument.Diffuse;
      MdxRender.Dev.TextureState[0].ColorOperation = TextureOperation.SelectArg1;
      MdxRender.Dev.TextureState[1].ColorOperation = TextureOperation.Disable;
    }
    public bool ConfigureLightmapForBlend(int ShaderIndex, int LightmapIndex)
    {
      if(true)//NumTextureStages >= 1)
      {
        MdxRender.Dev.RenderState.SourceBlend = Blend.One;
        MdxRender.Dev.RenderState.DestinationBlend = Blend.Zero;
        MdxRender.Dev.RenderState.AlphaBlendEnable = false;

        m_TextureManager.ActivateTexture(0, ShaderIndex, LightmapIndex);
      }

      return(false);
    }
		public bool ConfigureLightmapForBlend2(int ShaderIndex1,int ShaderIndex2, int LightmapIndex1,int LightmapIndex2)
		{
			if(true)//NumTextureStages >= 1)
			{
					m_TextureManager.ActivateTexture(0, ShaderIndex1, LightmapIndex1);
					m_TextureManager.ActivateTexture(1, ShaderIndex2, LightmapIndex2);
			}

			return(false);
		}

  }
}
