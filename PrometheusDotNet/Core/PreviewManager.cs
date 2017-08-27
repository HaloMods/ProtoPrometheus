using System;
using System.IO;
using System.Threading;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Prometheus.Core.Tags;
using Prometheus.Core.Tags.Bitm;
using Prometheus.Core.Tags.Mode;
using Prometheus.Core.Tags.Antr;
using Bitm = Prometheus.Core.Tags.Bitm;
using Bitmap = Prometheus.Core.Tags.Bitm.Bitmap;
using Prometheus.Core.Tags.Sbsp;
using H2Bitmap = Prometheus.Core.Tags.H2Bitm;
using Prometheus.Core.Project;
using TagLibrary.Halo1;

namespace Prometheus.Core.Render
{
  public enum PreviewMode{Teapot, Model, Texture, Shader, Animation, Bsp, H2Model, H2Bsp, ProjectMode};
  /// <summary>
  /// Summary description for PreviewManager.
  /// </summary>
  public class PreviewManager
  {
    private PreviewMode m_PreviewMode = PreviewMode.ProjectMode;
    public bool m_animationPaused = false;
    private bool m_AnimationLoaded = false;
    private int m_DebugShaderIndex = -1;
    private int m_ActiveTextureIndex = -1;
    private Instance3D m_ActiveModel = null;

    private Animations m_activeAnimationTag = null;
    private TagBsp m_activeBsp = null;
    private Halo2BSP m_activeH2Bsp = null;
    private Mesh m_Teapot = null;
    private CustomVertex.PositionTextured[] m_debugTextureVertices;
    private Instance3D m_Cube = null;

    public PreviewManager()
    {
      float w = 5;
      float h = 5;
      m_debugTextureVertices = new CustomVertex.PositionTextured[6];
      m_debugTextureVertices[0] = new CustomVertex.PositionTextured(-w, -h, 1.0f,   0f, 0f);
      m_debugTextureVertices[1] = new CustomVertex.PositionTextured(-w,  h, 1.0f,   0f, 1.0f);
      m_debugTextureVertices[2] = new CustomVertex.PositionTextured(w,   h, 1.0f, 1.0f, 1.0f);
      m_debugTextureVertices[3] = new CustomVertex.PositionTextured(w,   h, 1.0f, 1.0f, 1.0f);
      m_debugTextureVertices[4] = new CustomVertex.PositionTextured(w,  -h, 1.0f, 1.0f, 0f);
      m_debugTextureVertices[5] = new CustomVertex.PositionTextured(-w, -h, 1.0f,   0f, 0f);
    }
    public PreviewMode Mode
    {
      get{return m_PreviewMode;}
      set{m_PreviewMode = value;}
    }
    public string[] AnimationList
    {
      get
      {
        string[] list = null;
        if(m_activeAnimationTag != null)
        {
          list = m_activeAnimationTag.m_AnimationNames;
        }

        return(list);
      }
    }
    public eLOD ModelLevelofDetail
    {
      get
      {
        eLOD lod = eLOD.UNKNOWN;
        if(m_ActiveModel != null)
        {
          lod = m_ActiveModel.Model.LevelOfDetail;
        }
        return lod;
      }
      set
      {
        if(m_ActiveModel != null)
        {
          m_ActiveModel.Model.LevelOfDetail = value;
        }
      }
    }
    public Animations ActiveAnimationTag
    {
      get { return m_activeAnimationTag; }
    }
    public bool AnimationPaused
    {
      get { return m_animationPaused; }
      set { m_animationPaused = value; }
    }
    public int AnimationIndex
    {
      set
      {
        if(m_activeAnimationTag != null)
        {
          m_activeAnimationTag.ActivateAnimation(value);
        }
      }
    }
    public void SetPreviewMode(PreviewMode mode)
    {
      m_PreviewMode = mode;
    }
    public int GetActiveModelTriangleCount()
    {
      int count = 0;

      if((m_PreviewMode == PreviewMode.Model)||(m_PreviewMode == PreviewMode.Animation))
      {
        if(m_ActiveModel != null)
          count = m_ActiveModel.Model.TriangleCount;
      }

      return(count);
    }
    public void RenderActivePreview()
    {
      switch(m_PreviewMode)
      {
        case PreviewMode.Teapot:
          MdxRender.Dev.Transform.World = Microsoft.DirectX.Matrix.Identity;
          MdxRender.Dev.RenderState.CullMode = Cull.CounterClockwise;
          //if(this.m_Teapot != null)
          //  m_Teapot.DrawSubset(0);
          break;
        case PreviewMode.Model:
          if(m_ActiveModel != null)
          {
            MdxRender.Dev.Transform.World = Microsoft.DirectX.Matrix.Identity;
            MdxRender.Dev.VertexFormat = CustomVertex.PositionNormalTextured.Format;
            m_ActiveModel.Render();
          }
          break;
        case PreviewMode.H2Model:
          if(m_ActiveModel != null)
          {
            MdxRender.Dev.Transform.World = Microsoft.DirectX.Matrix.Identity;
            MdxRender.Dev.VertexFormat = CustomVertex.PositionNormalTextured.Format;
            m_ActiveModel.Render();
          }
          break;
        case PreviewMode.Animation:
          if((m_ActiveModel != null)&&(m_AnimationLoaded)&& (!m_animationPaused))
          {
            m_ActiveModel.UpdateKeyframe(m_activeAnimationTag);
            this.m_activeAnimationTag.UpdateAnimationTimer();
            m_ActiveModel.Render();
            m_ActiveModel.DrawNodes();
          }
          break;
        case PreviewMode.Shader:
          MdxRender.Dev.Transform.World = Microsoft.DirectX.Matrix.Identity;
          MdxRender.Dev.VertexFormat = CustomVertex.PositionTextured.Format;
          MdxRender.SM.ActivateShader(m_DebugShaderIndex, ShaderPass.First);
          MdxRender.Dev.DrawUserPrimitives(PrimitiveType.TriangleList, 2, m_debugTextureVertices);
          break;
        case PreviewMode.Texture:
          MdxRender.Dev.Transform.World = Microsoft.DirectX.Matrix.Identity;
          MdxRender.Dev.VertexFormat = CustomVertex.PositionTextured.Format;
          MdxRender.Dev.SamplerState[0].MaxMipLevel = 0;
          MdxRender.SM.m_TextureManager.ActivateTexture(0, this.m_ActiveTextureIndex, 0);
          MdxRender.Dev.DrawUserPrimitives(PrimitiveType.TriangleList, 2, m_debugTextureVertices);
          break;
        case PreviewMode.Bsp:
          if(this.m_activeBsp != null)
          {
            MdxRender.Dev.Transform.World = Microsoft.DirectX.Matrix.Identity;
            this.m_activeBsp.DrawBsp();
          }
          break;
        case PreviewMode.H2Bsp:
          if(this.m_activeH2Bsp != null)
          {
            MdxRender.Dev.Transform.World = Microsoft.DirectX.Matrix.Identity;
            this.m_activeH2Bsp.DrawModel();
          }
          break;
      }
    }
    public void Debug_LoadTestMesh()
    {
      Model3D cube = new Model3D("default cube");
      cube.LoadDirectionModel();
      m_Cube = new Instance3D(cube);
      //m_Cube.SetTranslation(0,0,5);
      //m_Cube.SetRotation((float)Math.PI/4, (float)Math.PI/4, (float)Math.PI/4);
    }
    public bool LoadModelTag(TagFileName tfn)
    {
      try
      {
        int index = MdxRender.MM.RegisterModel(tfn);
        m_ActiveModel = new Instance3D(MdxRender.MM.GetModel(index));
        m_ActiveModel.Model.LevelOfDetail = eLOD.CAUSES_FIRES;
        MdxRender.Camera.UpdateCameraByBoundingBox(ref m_ActiveModel.Model.m_BoundingBox, 2.0f, 5.0f);
        m_PreviewMode = PreviewMode.Model;
      }
      catch
      {
        
      }
      return (false);
    }
    public bool LoadHalo2ModelTag(TagFileName filename)
    {
			try
			{
				int index = MdxRender.MM.RegisterModel(filename);
				Model3D model = MdxRender.MM.GetModel(index);
				//MdxRender.Camera.UpdateCameraByBoundingBox(ref model.m_BoundingBox, 2.0f, 5.0f);
				m_PreviewMode = PreviewMode.H2Model;
				return true;
			}
			catch
			{
        
			}
      return (false);
    }
    public bool LoadAnimationTag(TagFileName tfn)
    {
      m_AnimationLoaded = false;
      try
      {
        m_activeAnimationTag = new Animations();
        m_activeAnimationTag.LoadTagBuffer(tfn);
        m_activeAnimationTag.LoadTagData();
        m_ActiveModel.InitializeAnimationProcessing();
        m_activeAnimationTag.ActivateAnimation(0);
        m_PreviewMode = PreviewMode.Animation;
      }
      catch (Exception ex)
      {
        throw new PrometheusException(
          "Error loading animation tag: " + tfn.RelativePath,
          ex,
          true);
      }
      m_AnimationLoaded = true;

      return (false);
    }
    public void DebugLoadTexture(TagFileName name)
    {
      m_ActiveTextureIndex = MdxRender.SM.m_TextureManager.RegisterTexture(name);
      MdxRender.Dev.RenderState.Lighting = false;
      MdxRender.Dev.RenderState.CullMode = Cull.None;
      MdxRender.Dev.VertexFormat = CustomVertex.PositionTextured.Format;
      MdxRender.SM.DisableBlend();
      m_PreviewMode = PreviewMode.Texture;
      MdxRender.Camera.SetLookAt(new Vector3(-18, 0, 0), new Vector3());
    }
		public void DebugLoadTexture2(TagFileName name)
		{
			m_ActiveTextureIndex = MdxRender.SM.m_TextureManager.RegisterTexture2(name);
			MdxRender.Dev.RenderState.Lighting = false;
			MdxRender.Dev.RenderState.CullMode = Cull.CounterClockwise;
			MdxRender.Dev.VertexFormat = CustomVertex.PositionTextured.Format;
			MdxRender.SM.DisableBlend();
			m_PreviewMode = PreviewMode.Texture;
			MdxRender.Camera.SetLookAt(new Vector3(-18, 0, 0), new Vector3());
		}
    public void DebugLoadShader(string name)
    {
      //turn filename into the type we can use
      //m_DebugShaderIndex = shader_mgr.RegisterShader(name, "ihcs");
    }
    public void DebugLoadBsp(TagFileName name)
    {
      m_activeBsp = new TagBsp();
      m_activeBsp.LoadTagBuffer(name);
      m_activeBsp.LoadTagData();
      SetPreviewMode(PreviewMode.Bsp);
      MdxRender.SetZbufRange(30.0f, 300.0f);
      MdxRender.Camera.UpdateCameraByCentroid(m_activeBsp.m_BoundingBox);
    }
    public void DebugLoadH2Bsp(TagFileName name)
    {
      m_activeH2Bsp = new Halo2BSP();
      m_activeH2Bsp.LoadTagBuffer(name);
      m_activeH2Bsp.LoadTagData(name.RelativePath);
			SetPreviewMode(PreviewMode.H2Bsp);
			MdxRender.SetZbufRange(30.0f,300.0f);
      MdxRender.Camera.UpdateCameraByCentroid(m_activeH2Bsp.m_BoundingBox);
    }
    public void PreviewTag(TagFileName tfn)
    {
      if(tfn.Exists)
      {
        bool bEnableFog = false;
      
        switch(tfn.FileExtension)
        {
          case "bitmap":
          switch(tfn.Version)
          {
            case MapfileVersion.XHALO1:
            case MapfileVersion.HALOPC:
              DebugLoadTexture(tfn);
              break;
            case MapfileVersion.XHALO2:
              DebugLoadTexture2(tfn);
              break;
          }
            break;
          case "biped":
            LoadBipedPreview(tfn);
            break;
          case "vehicle":
            LoadVehiclePreview(tfn);
            break;
          case "gbxmodel":
            LoadModelTag(tfn);
            break;
          case "scenario":
            //todo:  don't use project, set up local vars for this
            ProjectManager.LoadScenario(tfn);
            bEnableFog = true;
            break;
          case "scenario_structure_bsp":
            if(tfn.Version == MapfileVersion.XHALO2)
              DebugLoadH2Bsp(tfn);
            else
              DebugLoadBsp(tfn);
            break;
          case "model_animations":
            LoadAnimationTag(tfn);
            break;
          case "shader_environment":
            break;
          case "shader_model":
            break;
          case "shader_transparent_chicago":
            break;
          case "model":
          switch(tfn.Version)
          {
            case MapfileVersion.XHALO1:
              LoadModelTag(tfn);
              break;
            case MapfileVersion.XHALO2:
              LoadHalo2ModelTag(tfn);
              break;
          }
            break;
          case "sky":
            break;
        }
        string str = tfn.FileExtension;
        MdxRender.FogEnabled = bEnableFog;
      }
    }
    private void LoadVehiclePreview(TagFileName tfn)
    {
      TagBase tagdata = new TagBase();
      tagdata.LoadTagBuffer(tfn);
      BinaryReader br = new BinaryReader(tagdata.Stream);

      Vehicle obj = new Vehicle();
      obj.Read(br);
      obj.ReadChildData(br);
      LoadModelTag(new TagFileName(obj.ObjectValues.Model.Value, "mod2", tfn.Version));
      LoadAnimationTag(new TagFileName(obj.ObjectValues.AnimationGraph.Value, "antr", tfn.Version));
    }
    private void LoadBipedPreview(TagFileName tfn)
    {
      TagBase tagdata = new TagBase();
      tagdata.LoadTagBuffer(tfn);
      BinaryReader br = new BinaryReader(tagdata.Stream);

      Biped obj = new Biped();
      obj.Read(br);
      obj.ReadChildData(br);
      LoadModelTag(new TagFileName(obj.ObjectValues.Model.Value, "mod2", tfn.Version));
      LoadAnimationTag(new TagFileName(obj.ObjectValues.AnimationGraph.Value, "antr", tfn.Version));
    }
    /// <summary>
    /// Manual handler for device lost so we can customize how it is activated.
    /// This cleans up any DX resources that should be cleared when device is lost.
    /// </summary>
    public void ProcessDeviceLost()
    {
      if(m_activeBsp != null)
        m_activeBsp.ProcessDeviceLost();
      
      if(m_Teapot != null)
        m_Teapot.Dispose();
    }
    /// <summary>
    /// Manual handler for device reset so we can customize how it is activated.
    /// This recreates any DX resources that need it (vert buffers, etc).
    /// </summary>
    public void ProcessDeviceReset()
    {
      //reallocate DX resources
      //if(m_activeModelTag != null)
      //  m_activeModelTag.ProcessDeviceReset();
      
      //if(m_activeBsp != null)
      //  m_activeBsp.ProcessDeviceReset(ref dev);
      
      //if(m_Teapot != null)
      //  m_Teapot = Mesh.Teapot(dev);
    }
  }
}
