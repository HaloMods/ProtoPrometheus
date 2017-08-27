/* ---------------------------------------------------------------
 * Prometheus
 * Bungie Map-based Multi-Game Editing Kit
 * 2004-2005, Halo-Dev
 * ---------------------------------------------------------------
 * Class       : Prometheus.Core.Render.ModelManager
 * Description : Works in conjunction with the scenario editor so
 *               that models in the pallette can be accessed
 *               quickly as the scenario is rendered.
 * Author      : Grenadiac
 * Co-Authors  : 
 * ---------------------------------------------------------------
 */
using System;
using System.Diagnostics;
using Prometheus.Core.Tags;
using Prometheus.Core.Tags.Mode;

namespace Prometheus.Core.Render
{
  class ModelLookupElement
  {
    //public short index = -1;
    public string name = "";
    public Model3D model = null;
    private int m_ReferenceCount = 0;
    public ModelLookupElement()
    {
    }
    public void IncrementRefCount()
    {
      m_ReferenceCount++;
    }
    public void DecrementRefCount()
    {
      m_ReferenceCount--;

      if(m_ReferenceCount <= 0)
      {
        name = "";
        model = null;
        m_ReferenceCount = 0;
      }
    }
  }
  /// <summary>
  /// Works in conjunction with the scenario editor so
  /// that models in the pallette can be accessed
  /// quickly as the scenario is rendered.
  /// </summary>
  public class ModelManager
  {
    private const int DEFAULT_MODEL = -1;
    private const int DEFAULT_VEHICLE = 0;
    private const int SOUND_SCENERY = 1;
    private const int ARROW = 2;
    private const int LIGHT = 3;
    private const int PLAYER_SPAWN = 4;
    private const int MaxElements = 500;
    ModelLookupElement[] m_LookupTable = new ModelLookupElement[MaxElements];
    private int m_LookupCount = 0;

    public Model3D DefaultModel
    {
      get{return(m_LookupTable[DEFAULT_VEHICLE].model);}
    }
    public Model3D PlayerSpawnModel
    {
      get{return(m_LookupTable[PLAYER_SPAWN].model);}
    }
    public Model3D SoundSceneryModel
    {
      get{return(m_LookupTable[SOUND_SCENERY].model);}
    }
    public Model3D ArrowModel
    {
      get{return(m_LookupTable[ARROW].model);}
    }
    public Model3D LightModel
    {
      get{return(m_LookupTable[LIGHT].model);}
    }
    public ModelManager()
    {
      //load the default models
      m_LookupTable[DEFAULT_VEHICLE] = new ModelLookupElement();
      m_LookupTable[DEFAULT_VEHICLE].name = "default_vehicle";
      m_LookupTable[DEFAULT_VEHICLE].model = new Model3D("default_vehicle");
      m_LookupTable[DEFAULT_VEHICLE].model.LoadDefaultModel(UtilityShader.Vehicle);

      m_LookupTable[SOUND_SCENERY] = new ModelLookupElement();
      m_LookupTable[SOUND_SCENERY].name = "sound_scenery";
      m_LookupTable[SOUND_SCENERY].model = new Model3D("sound_scenery");
      m_LookupTable[SOUND_SCENERY].model.LoadDefaultModel(UtilityShader.Vehicle);
      
      m_LookupTable[ARROW] = new ModelLookupElement();
      m_LookupTable[ARROW].name = "arrow";
      m_LookupTable[ARROW].model = new Model3D("arrow");
      m_LookupTable[ARROW].model.LoadArrow("Core.Render.Resources.arrow.x");

      m_LookupTable[LIGHT] = new ModelLookupElement();
      m_LookupTable[LIGHT].name = "light";
      m_LookupTable[LIGHT].model = new Model3D("light");
      m_LookupTable[LIGHT].model.LoadResourceModel("Core.Render.Resources.lightbulb.x");

      m_LookupTable[PLAYER_SPAWN] = new ModelLookupElement();
      m_LookupTable[PLAYER_SPAWN].name = "player_spawn";
      m_LookupTable[PLAYER_SPAWN].model = new Model3D("player_spawn");
      m_LookupTable[PLAYER_SPAWN].model.LoadResourceModel("Core.Render.Resources.player.x");
      //m_LookupTable[PLAYER_SPAWN].model.LoadDefaultModel(UtilityShader.Vehicle);
      
      m_LookupCount = PLAYER_SPAWN + 1;
    }
    public int RegisterModel(TagFileName tfn)
    {
      int model_index = DEFAULT_MODEL;
      bool bModelFound = false;

      //TODO:  use hash table?
      for(model_index=0; ((model_index<MaxElements)&&(model_index<m_LookupCount)); model_index++)
      {
        if(m_LookupTable[model_index].name == tfn.RelativePath)
        {
          bModelFound = true;
          break;
        }
      }

      if(bModelFound == false)
      {
        try
        {
          Model3D generic_model = null;

          //load the model
          if(tfn.Version == MapfileVersion.XHALO2)
          {
            TagHalo2Model H2model = new TagHalo2Model();
            H2model.LoadTagBuffer(tfn);
            H2model.LoadTagData();
            Trace.WriteLine("Loading model: " + tfn.RelativePath);
            //generic_model = H2model.GetModel3D(tfn.RelativePath);
          }
          else if((tfn.Version == MapfileVersion.XHALO1)||
            (tfn.Version == MapfileVersion.HALOPC)||
            (tfn.Version == MapfileVersion.HALOCE))
          {
            TagModel H1model = new TagModel();
            H1model.LoadTagBuffer(tfn);
            H1model.LoadTagData();
            Trace.WriteLine("Loading model: " + tfn.RelativePath);
            generic_model = H1model.GetModel3D(tfn.RelativePath);
          }

          model_index = GetFirstAvailableIndex();

          //insert the lookup element into the table
          m_LookupTable[model_index] = new ModelLookupElement();
          m_LookupTable[model_index].name = tfn.RelativePath;
          m_LookupTable[model_index].model = generic_model;

          if(model_index == m_LookupCount)
            m_LookupCount++;
        }
        catch(Exception e)
        {
          Trace.WriteLine("ModelManager Exception: " + e.Message);
        }
      }

      return(model_index);
    }
    public Model3D GetModel(int index)
    {
      Model3D model = null;
      
      if((index <= m_LookupCount)&&(index > DEFAULT_MODEL)&&(m_LookupTable[index] != null))
        model = m_LookupTable[index].model;

      return(model);
    }
    public void RenderModel(int index, eLOD lod)
    {
      if((index <= m_LookupCount)&&(index > DEFAULT_MODEL)&&(m_LookupTable[index].model != null))
      {
        m_LookupTable[index].model.Render();
      }
      else
      {
        //todo: draw default model (cube)
      }
    }
    private int GetFirstAvailableIndex()
    {
      int i;
      for(i=0; i<m_LookupCount; i++)
      {
        if(m_LookupTable[i].name == "")
          break;
      }

      if(i >= m_LookupCount)
        i = m_LookupCount;

      return(i);
    }
  }
}
