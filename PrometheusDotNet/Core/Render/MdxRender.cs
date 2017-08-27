/* ---------------------------------------------------------------
 * Prometheus
 * Bungie Map-based Multi-Game Editing Kit
 * 2004-2005, Halo-Dev
 * ---------------------------------------------------------------
 * Class       : Prometheus.Core.Render.MdxRender
 * Description : The DirectX renderer - handles all rendering
 *             : related tasks, including mouse/keyboard input.
 * Author      : Grenadiac
 * Co-Authors  : MonoxideC
 * ---------------------------------------------------------------
 */

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX.DirectInput;
using Device = Microsoft.DirectX.Direct3D.Device;
using DeviceType = Microsoft.DirectX.Direct3D.DeviceType;
using DI = Microsoft.DirectX.DirectInput;
using D3D = Microsoft.DirectX.Direct3D;
using Font = System.Drawing.Font;
using Manager = Microsoft.DirectX.Direct3D.Manager;
using Prometheus.Core.Tags;
using Prometheus.Core.Tags.Sbsp;
using Prometheus.Core.Project;
using Core.Lightmap;

namespace Prometheus.Core.Render
{
  /// <summary>
  /// The DirectX renderer - handles all rendering
  /// related tasks, including mouse/keyboard input.
  /// </summary>
  public class MdxRender : IDisposable
  {
    public class DeviceInfoStruct
    {
      public Caps caps;
      public PresentParameters pp = new PresentParameters();
      public int default_adapter;
      public CreateFlags flags;
      public PictureBox renderWin;
      public bool bDeviceLost = false;
      public bool bWindowCreated = false;
      public int LastViewportWidth;
      public int LastViewportHeight;
    }

    static public Device Dev;
    static public ShaderManager SM;
    static public ModelManager MM;
    static public InputManager Input;
    static public bool m_bDeviceInReset = false;
    static public PreviewManager PreviewManager = new PreviewManager();
    static private TagBsp Halo1_Bsp = null;
    static private Model3D SkyBox = null;
    static public Camera Camera;
    public static GrenLightmapDebug lightmapDebugger;
    static private RenderButton mapViewButton = null;

    private static FPSCounter m_FPS;
    private D3D.Font m_fontFPS;
    private D3D.Font consoleFPS;

    private bool m_getInput = false;
    private bool m_IsShuttingDown = false;
    private bool m_ready;
    private bool m_isPaused;
    private bool m_mouseEnabled;
    static private bool m_showFPS;
    static private float m_MinDrawDistance = 0.5f;
    static private float m_MaxDrawDistance = 1000.0f;
    static private bool m_bUpdateViewTransform = true;
    static public DeviceInfoStruct DeviceInfo = new DeviceInfoStruct();
    static public Material whiteMaterial;

    private RunningAvg m_PitchAvg = new RunningAvg();
    private RunningAvg m_RollAvg = new RunningAvg();

    private int viewportHeight;
    private int fpsFontHeight;
    static bool fogEnabled;

    static private RGB fogColor;
    static private float fogStart;
    static private float fogEnd;
    static private float fogDensity;

    static private Color clearColor = Color.DarkBlue;


    public static float CurrentFPS
    {
      get { return m_FPS.FPS; }
    }

    public static Color ClearColor
    {
      get { return clearColor; }
      set { clearColor = value; }
    }
    public struct RGB
    {
      public float R, G, B;
    }

    public static RGB FogColor
    {
      get { return fogColor; }
      set { fogColor = value; }
    }

    public static float FogStart
    {
      get { return fogStart; }
      set { fogStart = value; }
    }

    public static float FogEnd
    {
      get { return fogEnd; }
      set { fogEnd = value; }
    }

    public static float FogDensity
    {
      get { return fogDensity; }
      set { fogDensity = value; }
    }

    public MdxRender()
    {
      m_FPS = new FPSCounter(100);
      m_isPaused = false;

      //get device caps necessary for creation
      MdxRender.DeviceInfo.default_adapter = Manager.Adapters.Default.Adapter;
      MdxRender.DeviceInfo.caps = Manager.GetDeviceCaps(MdxRender.DeviceInfo.default_adapter, DeviceType.Hardware);

      MdxRender.DeviceInfo.pp.Windowed = true;
      MdxRender.DeviceInfo.pp.SwapEffect = SwapEffect.Discard;
      MdxRender.DeviceInfo.pp.AutoDepthStencilFormat = DepthFormat.D16;
      MdxRender.DeviceInfo.pp.EnableAutoDepthStencil = true;

      MdxRender.DeviceInfo.flags = CreateFlags.SoftwareVertexProcessing;
      if (MdxRender.DeviceInfo.caps.DeviceCaps.SupportsHardwareTransformAndLight)
        MdxRender.DeviceInfo.flags = CreateFlags.HardwareVertexProcessing;
    }

    public static bool DeviceInReset
    {
      get{return(m_bDeviceInReset);}
      set{m_bDeviceInReset = value;}
    }

	  public static TagBsp GetCurrentBsp
	  {
		  get{ return Halo1_Bsp; }
	  }

    public void Dispose()
    {
    }
    
    public static bool FogEnabled
    {
      get { return fogEnabled; }
      set
      {
        if(MdxRender.Dev != null)
        {
          //always update when true in case fog properties changed
          if(value == true)
          {
            MdxRender.Dev.SetRenderState(RenderStates.FogEnable, true);
            MdxRender.Dev.RenderState.FogColor = Color.FromArgb(
              (int)(fogColor.R * 255),  (int)(fogColor.G * 255), (int)(fogColor.B * 255));
            MdxRender.Dev.SetRenderState(RenderStates.FogDensity, fogDensity);
            MdxRender.Dev.SetRenderState(RenderStates.FogVertexMode, (int)FogMode.Linear);
            MdxRender.Dev.SetRenderState(RenderStates.FogStart, fogStart);
            MdxRender.Dev.SetRenderState(RenderStates.FogEnd, fogEnd);
          }
          else
          {
            MdxRender.Dev.SetRenderState(RenderStates.FogEnable, false);
            MdxRender.ClearColor = Color.DarkBlue;
          }
        }

        fogEnabled = value;
      }
    }

    public bool MouseEnabled
    {
      get { return m_mouseEnabled; }
      set { m_mouseEnabled = value; }
    }

    public bool Pause
    {
      get { return m_isPaused; }
      set { m_isPaused = value; }
    }

    public static bool ShowFPS
    {
      get { return m_showFPS; }
      set { m_showFPS = value; }
    }

    public float FPS
    {
      get { return m_FPS.FPS; }
    }

    public bool IsReady
    {
      get { return m_ready; }
    }

    public bool GetInput
    {
      get { return m_getInput; }
      set { m_getInput = value; }
    }
    public Matrix CurrentViewMatrix
    {
      get { return Camera.GetViewMatrix(); }
    }
    public static Matrix CurrentProjectionMatrix
    {
      get 
      {
        Viewport vp = MdxRender.Dev.Viewport;
        return  Matrix.PerspectiveFovRH((float)Math.PI/4, 
            (float)vp.Width/(float)vp.Height, m_MinDrawDistance, m_MaxDrawDistance);
      }
    }

    public static void CalculatePickRayWorld(int ScreenX, int ScreenY, out Vector3 Direction, out Vector3 Origin)
    {
      Vector3 screen_vector = new Vector3();
      Viewport vp = new Viewport();
      Matrix proj = new Matrix();
      vp = MdxRender.Dev.Viewport;
      proj = MdxRender.Dev.Transform.Projection;
      float zoom_x = proj.M11;
      float zoom_y = proj.M22;
      //Determine projection space vector
      screen_vector.X =  ( ( ( 2.0f * ScreenX ) / vp.Width  ) - 1 ) / zoom_x;
      screen_vector.Y = -( ( ( 2.0f * ScreenY ) / vp.Height ) - 1 ) / zoom_y;
      screen_vector.Z =  1.0f;

      //transform vector into world coords
      Matrix view = new Matrix();
      view = MdxRender.Dev.Transform.View;
      view.Invert();

      // Transform the screen space pick ray into 3D space
      Direction.X  =  (screen_vector.X*view.M11 + screen_vector.Y*view.M21 - screen_vector.Z*view.M31);
      Direction.Y  =  (screen_vector.X*view.M12 + screen_vector.Y*view.M22 - screen_vector.Z*view.M32);
      Direction.Z  =  (screen_vector.X*view.M13 + screen_vector.Y*view.M23 - screen_vector.Z*view.M33);
      Direction.Normalize();
      
      Origin.X = view.M41;
      Origin.Y = view.M42;
      Origin.Z = view.M43;

      // calc origin as intersection with near frustum
      
      Origin += Direction*vp.MinZ;

      //Trace.WriteLine("World Space PickRay:  origin = ( "+ Origin.X.ToString() + ", " + Origin.Y.ToString() + ", " + Origin.Z.ToString() +" )  direction = ( "+
      //  Direction.X.ToString() + ", " + Direction.Y.ToString() + ", " + Direction.Z.ToString() + " )");
    }

    public static bool IsRenderButtonClicked(int x, int y)
    {
      bool bClicked = false;
      
      if(PreviewManager.Mode != PreviewMode.ProjectMode)
      {
        bClicked = mapViewButton.MouseClick(x, y);

        if(bClicked)
        {
          PreviewManager.Mode = PreviewMode.ProjectMode;
          if(Halo1_Bsp != null)
            MdxRender.Camera.UpdateCameraByCentroid(Halo1_Bsp.m_BoundingBox);
        }
      }

      return(bClicked);
    }
    public void RenderLoop()
    {
      if((m_isPaused == false)&&
         (m_IsShuttingDown == false))
      {
        MdxRender.Input.Update();

        if(Input.IncreaseGamma)
        {
          GammaFactor++;
          //UpdateGammaCorrection();
        }
        if(Input.DecreaseGamma)
        {
          GammaFactor--;
          //UpdateGammaCorrection();
        }

        if(m_getInput)
          UpdateCameraFromInput();

        UpdateViewTransform();
        DrawScene();
      }
    }
    public void UpdateViewTransform()
    {
      if(m_ready)
      {
        //TODO:  implement MDX callback method so we don't pull the vp every frame (inefficient)
        Viewport vp = MdxRender.Dev.Viewport;

        if((MdxRender.DeviceInfo.LastViewportWidth != vp.Width)||
          (MdxRender.DeviceInfo.LastViewportHeight != vp.Height)||
          (m_bUpdateViewTransform == true))
        {
          vp.MinZ = 0.1f;
          vp.MaxZ = 1.0f;
          MdxRender.Dev.Viewport = vp;
          MdxRender.Dev.Transform.Projection = Matrix.PerspectiveFovRH((float)Math.PI/4, 
            (float)vp.Width/(float)vp.Height, 0.5f, m_MaxDrawDistance);

          MdxRender.DeviceInfo.LastViewportWidth = vp.Width;
          MdxRender.DeviceInfo.LastViewportHeight = vp.Height;
          m_bUpdateViewTransform = false;
        }
      }
    }
    static public void SetZbufRange(float NearplaneDist, float FarplaneDist)
    {
      m_MinDrawDistance = NearplaneDist;
      m_MaxDrawDistance = FarplaneDist;
      m_bUpdateViewTransform = true;
    }
    public void SetControlReference(ref PictureBox RenderWin)
    {
      // We try to avoid mixing gui code, but we need a reference to
      // the picturebox control for init.
      MdxRender.DeviceInfo.renderWin = RenderWin;
      MdxRender.DeviceInfo.bWindowCreated = true;
    }
    public bool InitMdx()
    {
      Dev = new Device(MdxRender.DeviceInfo.default_adapter, DeviceType.Hardware, 
        MdxRender.DeviceInfo.renderWin, MdxRender.DeviceInfo.flags, MdxRender.DeviceInfo.pp);

      MdxRender.SM = new ShaderManager();
      MdxRender.MM = new ModelManager();
      MdxRender.Input = new InputManager();
      SelectTool.InitializeHandleModel();
      SelectionBox.Initialize();
      ProjectManager.MapSpawns.InitDebug();
      ShaderBase.InitializeDebug();
      mapViewButton = new RenderButton();
      //mapViewButton.Width = MdxRender.DeviceInfo.renderWin.Width;
      //mapViewButton.Height = MdxRender.DeviceInfo.renderWin.Height;
      mapViewButton.Initialize(35, 35, 15, 40);

      //set up lights that don't work
      SetupLights();

      whiteMaterial = new Material();
      whiteMaterial.Ambient = Color.White;
      whiteMaterial.Diffuse = Color.White;
      //whiteMaterial.Emissive = Color.White;

      //Set up texture filtering to get rid of blocky uglies
      MdxRender.Dev.SamplerState[0].MagFilter = TextureFilter.Linear;
      MdxRender.Dev.SamplerState[0].MinFilter = TextureFilter.Linear;
      MdxRender.Dev.SamplerState[0].MipFilter = TextureFilter.Linear;
      MdxRender.Dev.SamplerState[1].MagFilter = TextureFilter.Linear;
      MdxRender.Dev.SamplerState[1].MinFilter = TextureFilter.Linear;
      MdxRender.Dev.SamplerState[1].MipFilter = TextureFilter.Linear;
      MdxRender.Dev.SamplerState[2].MagFilter = TextureFilter.Linear;
      MdxRender.Dev.SamplerState[2].MinFilter = TextureFilter.Linear;
      MdxRender.Dev.SamplerState[2].MipFilter = TextureFilter.Linear;
      MdxRender.Dev.SamplerState[3].MagFilter = TextureFilter.Linear;
      MdxRender.Dev.SamplerState[3].MinFilter = TextureFilter.Linear;
      MdxRender.Dev.SamplerState[3].MipFilter = TextureFilter.Linear;
      MdxRender.Dev.SamplerState[4].MagFilter = TextureFilter.Linear;
      MdxRender.Dev.SamplerState[4].MinFilter = TextureFilter.Linear;
      MdxRender.Dev.SamplerState[4].MipFilter = TextureFilter.Linear;

      MdxRender.Dev.RenderState.AlphaFunction = Compare.GreaterEqual;
      MdxRender.Dev.SetRenderState(RenderStates.ReferenceAlpha, 0x80);
      //UpdateGammaCorrection();
      
      //disable vsync, might get some tearing
      MdxRender.Dev.PresentationParameters.PresentationInterval = PresentInterval.Immediate;

      MdxRender.lightmapDebugger = new GrenLightmapDebug();
      MdxRender.Dev.RenderState.PointSize = 10.0f;

      MdxRender.Dev.RenderState.CullMode = Cull.CounterClockwise;
      MdxRender.Dev.RenderState.ZBufferEnable = true;
      MdxRender.Dev.DeviceResizing += new System.ComponentModel.CancelEventHandler(device_DeviceResizing);
      MdxRender.Dev.DeviceReset += new EventHandler(device_DeviceReset);

      //Device.IsUsingEventHandlers = false;

      // Initialize the font
      m_fontFPS = new D3D.Font(Dev, new Font("Arial", 12, FontStyle.Bold));
      consoleFPS = new D3D.Font(Dev, new Font("Arial", 10, FontStyle.Bold));

      // Initialize the camera placement and orientation
      Camera = new Camera();
      Camera.SetLookAt(new Vector3(-18, 0, 0), new Vector3());

      PreviewManager.Debug_LoadTestMesh();
      m_ready = true;

      return (false);
    }

    private void device_DeviceResizing(object sender, CancelEventArgs e)
    {
      if((MdxRender.DeviceInfo.renderWin.Height < 10) || (MdxRender.DeviceInfo.renderWin.Width < 20))
      {
        e.Cancel = true;
      }
      else
      {
        Trace.WriteLine("MDX Device resizing");
        MdxRender.DeviceInReset = true;
        UpdateViewTransform();
      }
    }
    public void AttemptRecovery()
    {
      try
      {
        MdxRender.Dev.TestCooperativeLevel();
      }
      catch(DeviceLostException)
      {
      }
      catch(DeviceNotResetException)
      {
        try
        {
          MdxRender.Dev.Reset(MdxRender.DeviceInfo.pp);
          MdxRender.DeviceInfo.bDeviceLost = false;
          Trace.WriteLine("Device successfully reset.");
        }
        catch(DeviceLostException)
        {
        }
      }
    }

    public void DrawScene()
    {
      if(m_ready)
      {
        m_FPS.BeginFrame();

        if(MdxRender.DeviceInfo.bDeviceLost)
        {
          AttemptRecovery();
        }
        else
        {
          try
          {
            MdxRender.Dev.Clear(ClearFlags.ZBuffer | ClearFlags.Target, ClearColor, 1.0f, 0);
            MdxRender.Dev.Transform.View = Camera.GetViewMatrix();
            MdxRender.Dev.Transform.World = Matrix.Identity;

            //todo: preview vs bsp mode
            if(PreviewManager.Mode != PreviewMode.ProjectMode)
            {
              MdxRender.SetZbufRange(0.01f, 300.0f);
              UpdateViewTransform();
            }
            else
            {
              SetZbufRange(30.0f, 100000.0f);
              UpdateViewTransform();
            }

            MdxRender.Dev.BeginScene();

            if(!m_isPaused)
            {
              MdxRender.Dev.Material = whiteMaterial;
              //The beef of the matter
              if(PreviewManager.Mode != PreviewMode.ProjectMode)
              {
                PreviewManager.RenderActivePreview();
                //mapViewButton.DrawButton(true);
              }
              else
              {
                #region DrawProject
                MdxRender.Dev.Material = whiteMaterial;
                MdxRender.Dev.RenderState.DiffuseMaterialSource = ColorSource.Material;
                MdxRender.Dev.RenderState.SpecularMaterialSource = ColorSource.Material;
                MdxRender.Dev.RenderState.SpecularEnable = false;

                //Render the skybox if we are in map view mode
                if(MdxRender.SkyBox != null)
                {
                  //turn lighting off for skybox
                  //MdxRender.Dev.RenderState.Lighting = false;

                  //turn off fog or you won't be able to see the skybox through it
                  if(MdxRender.FogEnabled)
                    MdxRender.Dev.SetRenderState(RenderStates.FogEnable, false);  
                
                  //disable z-writing for the skybox since it is behind everything
                  //and to get rid of that stupid clipping artifact
                  MdxRender.Dev.RenderState.ZBufferWriteEnable = false;
                  SkyBox.Render();
                  MdxRender.Dev.RenderState.ZBufferWriteEnable = true;

                  //turn fog back on
                  if(MdxRender.FogEnabled)
                    MdxRender.Dev.SetRenderState(RenderStates.FogEnable, true);
                }
                else
                {
                  //turn off fog when we are in model preview mode
                  MdxRender.Dev.SetRenderState(RenderStates.FogEnable, false);
                }

                //draw the map BSP
                if(Halo1_Bsp != null)
                {
                  MdxRender.Dev.RenderState.Lighting = true;
                  Halo1_Bsp.DrawBsp();
                  TagBsp.Debug_RenderNearestNeighborVerts();
                  MdxRender.lightmapDebugger.Render();
                  //Halo1_Bsp.DrawWireframeBsp();
                  //MdxRender.Dev.RenderState.FillMode = FillMode.Solid;
                }

                //turn on lighting for scenery
                MdxRender.Dev.RenderState.Lighting = true;
                MdxRender.SM.DisableBlend();  //this necessary?

                //draw all ye map objects
                //MdxRender.Dev.RenderState.DiffuseMaterialSource = ColorSource.Material;
                //MdxRender.Dev.RenderState.SpecularMaterialSource = ColorSource.Material;
                //MdxRender.Dev.RenderState.SpecularEnable = true;
                ProjectManager.MapSpawns.Render();
                #endregion


                if (m_showFPS) DrawFPS();
              }
            }
            else
            {
              m_fontFPS.DrawText(null, "Rendering Paused", 3, 3, Color.White);
              Thread.Sleep(250);
            }
          }
          catch(Exception e)
          {
            Trace.WriteLine("Error during DrawScene(): " + e.Message);
          }
          finally
          {
            try
            {
              MdxRender.Dev.EndScene();
              MdxRender.Dev.Present();
              //SwapChain sc = MdxRender.Dev.GetSwapChain(0);
              //sc.Present(Present.LinearContent);
            }
            catch(DeviceLostException)
            {
              MdxRender.DeviceInfo.bDeviceLost = true;
              Trace.WriteLine("Device was lost.");
            }
            catch(Exception e)
            {
              Trace.WriteLine("Error during Device.Present(): " + e.Message);
            }
          }
        }
        m_FPS.EndFrame();
      }
    }
    private void DrawFPS()
    {
      if(OptionsManager.EnableFPS)
        m_fontFPS.DrawText(null, Convert.ToString(m_FPS.FPS) + " fps", 3, 3, Color.White);
    }

    private void SetupLights()
    {
      MdxRender.Dev.RenderState.Ambient = Color.Gray;
      MdxRender.Dev.Lights[0].Type = LightType.Directional;
      MdxRender.Dev.Lights[0].Diffuse = Color.White;
      MdxRender.Dev.Lights[0].Specular = Color.White;
      MdxRender.Dev.Lights[0].Direction = new Vector3(0.5f, -1, -1);
      MdxRender.Dev.Lights[0].Update();
      MdxRender.Dev.Lights[0].Enabled = true;
      MdxRender.Dev.RenderState.Lighting = true;
    }
    public void UpdateCameraFromInput()
    {
      if (m_ready)
      {
        bool bTranslated = false;
        Camera.SetFPS(m_FPS.FPS);

        if(RenderConsole.InputMode)
        {
          
        }
        else
        {
          if(Input.CameraForward)
          {
            Camera.Move(-1);
            bTranslated = true;
          }

          if(Input.CameraLeft)
          {
            Camera.Strafe(-1);
            bTranslated = true;
          }

          if(Input.CameraBack)
          {
            Camera.Move(1);
            bTranslated = true;
          }

          if(Input.CameraRight)
          {
            Camera.Strafe(1);
            bTranslated = true;
          }

          if(Input.CameraUp)
          {
            Camera.Up(1);
            bTranslated = true;
          }

          if(Input.CameraDown)
          {
            Camera.Up(-1);
            bTranslated = true;
          }

          //if(state[Key.PageUp])
          //  Camera.IncreaseSpeed();

          //if(state[Key.PageDown])
          //  Camera.DecreaseSpeed();

          if(m_mouseEnabled)
          {
            float input_x = Input.MouseX;
            float input_y = Input.MouseY;

            float xScale = -0.006f;
            float yScale = -0.006f;
            input_x *= xScale;
            input_y *= yScale;

            if((Input.CameraActive)&&(ProjectManager.MapSpawns.CameraLockRequested == false))
            {
              m_PitchAvg.Update(-input_y);
              m_RollAvg.Update(-input_x);
              Camera.Pitch(m_PitchAvg.Average);
              Camera.Yaw(m_RollAvg.Average);
            }
          }
        }

        Camera.TranslationControl.MovementActivated(bTranslated);
        Camera.UpdateCoasting();
      }
    }
    static public void LoadBsp(TagFileName bsp_tfn, TagFileName sky_tfn)
    {
      if(bsp_tfn.Version == MapfileVersion.XHALO2)
      {
        Halo1_Bsp = null;
        //TODO: load Halo2 bsp
      }
      else
      {
        if(sky_tfn != null)
        {
          int index = MdxRender.MM.RegisterModel(sky_tfn);
          MdxRender.SkyBox = MdxRender.MM.GetModel(index);
        }
        //TODO: null out Halo2 bsp, clean up old resources

        Halo1_Bsp = new TagBsp();
        Halo1_Bsp.LoadTagBuffer(bsp_tfn);
        Halo1_Bsp.LoadTagData();
        MdxRender.Camera.UpdateCameraByCentroid(Halo1_Bsp.m_BoundingBox);
      }
    }
    public void DumpDeviceStatus()
    {
      int atm = MdxRender.Dev.AvailableTextureMemory;
      TextureCaps tc = MdxRender.DeviceInfo.caps.TextureCaps;
      Trace.WriteLine("Available texture memory: "+atm.ToString());
      Trace.WriteLine("Texture caps:");
      Trace.WriteLine(tc.ToString());
    }
    private void device_DeviceReset(object sender, EventArgs e)
    {
      Trace.WriteLine("MDX Device was reset");
      //TODO:  broadcast resource reload to all DX resources, set flag to suspend drawing until reset complete
      MdxRender.Dev.RenderState.CullMode = Cull.CounterClockwise;
      MdxRender.Dev.RenderState.ZBufferEnable = true;
      MdxRender.DeviceInfo.LastViewportWidth = -1; //lets UpdateProjectionMatrix() know it needs to execute
      SetupLights();

      MdxRender.Dev.SamplerState[0].MagFilter = TextureFilter.Linear;
      MdxRender.Dev.SamplerState[0].MinFilter = TextureFilter.Linear;
      MdxRender.Dev.SamplerState[0].MipFilter = TextureFilter.Linear;
      MdxRender.Dev.SamplerState[1].MagFilter = TextureFilter.Linear;
      MdxRender.Dev.SamplerState[1].MinFilter = TextureFilter.Linear;
      MdxRender.Dev.SamplerState[1].MipFilter = TextureFilter.Linear;
      MdxRender.Dev.SamplerState[2].MagFilter = TextureFilter.Linear;
      MdxRender.Dev.SamplerState[2].MinFilter = TextureFilter.Linear;
      MdxRender.Dev.SamplerState[2].MipFilter = TextureFilter.Linear;
      MdxRender.Dev.SamplerState[3].MagFilter = TextureFilter.Linear;
      MdxRender.Dev.SamplerState[3].MinFilter = TextureFilter.Linear;
      MdxRender.Dev.SamplerState[3].MipFilter = TextureFilter.Linear;
      MdxRender.Dev.SamplerState[4].MagFilter = TextureFilter.Linear;
      MdxRender.Dev.SamplerState[4].MinFilter = TextureFilter.Linear;
      MdxRender.Dev.SamplerState[4].MipFilter = TextureFilter.Linear;

      if(MdxRender.FogEnabled)
      {
        MdxRender.Dev.SetRenderState(RenderStates.FogEnable, true);
        MdxRender.Dev.RenderState.FogColor = Color.FromArgb(
          (int)(fogColor.R * 255),  (int)(fogColor.G * 255), (int)(fogColor.B * 255));
        MdxRender.Dev.SetRenderState(RenderStates.FogDensity, fogDensity);
        MdxRender.Dev.SetRenderState(RenderStates.FogVertexMode, (int)FogMode.Linear);
        MdxRender.Dev.SetRenderState(RenderStates.FogStart, fogStart);
        MdxRender.Dev.SetRenderState(RenderStates.FogEnd, fogEnd);
      }
      else
      {
        MdxRender.Dev.SetRenderState(RenderStates.FogEnable, false);
        MdxRender.ClearColor = Color.DarkBlue;
      }

      PreviewManager.ProcessDeviceReset();
      MdxRender.DeviceInReset = false;
    }
    static public void AddGlobalLight(int index, RadiosityLight rl)
    {
      MdxRender.Dev.RenderState.Ambient = Color.Gray;
      MdxRender.Dev.Lights[0].Type = LightType.Directional;
      MdxRender.Dev.Lights[0].Diffuse = Color.FromArgb((int)(rl.color[0]*255), (int)(rl.color[1]*255), (int)(rl.color[2]*255));
      MdxRender.Dev.Lights[0].Direction = rl.direction;
      MdxRender.Dev.Lights[0].Update();
      MdxRender.Dev.Lights[0].Enabled = true;
      MdxRender.Dev.RenderState.Lighting = true;
    }
    static public void MouseMove(int x, int y, Color clr)
    {
      if((Halo1_Bsp != null)&&(Input.ApplyLightmapPaint))
      {
        Vector3 origin = new Vector3();
        Vector3 direction = new Vector3();
        MdxRender.CalculatePickRayWorld(x, y, out direction, out origin);
        Halo1_Bsp.ApplyLightmapColor(origin, direction, clr);
      }
    }
    static short GammaFactor = 220;
    static public void UpdateGammaCorrection()
    {
      short[] gray_gamma = new short[256];
      //GammaRamp gr = MdxRender.Dev.GetGammaRamp(0);
      //short[] red_gamma = gr.GetRed();
      //short[] green_gamma = gr.GetGreen();
      //short[] blue_gamma = gr.GetBlue();
      GammaRamp gr = new GammaRamp();

      float recip_gray = recip_gamma(GammaFactor);
      // compute i**(1/gamma) for all i and scale to range
      for(short i = 0; i < 256; i++)
      {
        gray_gamma[i] = ramp_val(i, recip_gray, 255);
      }

      gr.SetRed(gray_gamma);
      gr.SetGreen(gray_gamma);
      gr.SetBlue(gray_gamma);

      MdxRender.Dev.SetGammaRamp(0, false, gr);


      /*
      m_gamma[G_GRAY] = m_gamma[G_RED] = m_gamma[G_GREEN] = m_gamma[G_BLUE]
        = INITIAL_GAMMA;

      if (m_test_gamma && m_can_gamma_ramp)
      {
        if (m_single_gamma)
        {
          // compute 1/gamma
          const float recip_gray = recip_gamma(m_gamma[G_GRAY]);
          // compute i**(1/gamma) for all i and scale to range
          for (UINT i = 0; i < 256; i++)
          {
            m_ramp.red[i] = m_ramp.green[i] = m_ramp.blue[i] =
              ramp_val(i, recip_gray);
          }
        }
      }
      else
      {
        for (UINT i = 0; i < 256; i++)
        {
          m_ramp.red[i] = m_ramp.green[i] = m_ramp.blue[i] = 65535*i/255;
        }
      }
      */

    }
    private static float recip_gamma(short i)
    {
      return (float)(Math.Log(i/255f)/Math.Log(0.5f));
    }

    private static float gamma(short i)
    {
      return (float)(Math.Log(0.5f)/Math.Log(i/255f));
    }
    private static short ramp_val(short i, float recip_gamma, int scale/* = 65535*/)
    {
      return (short)(scale*Math.Pow(i/255f, recip_gamma));
    }
  }

  public class RenderBox
  {
    private static VertexBuffer m_VertexBuffer;
    private static IndexBuffer m_IndexBuffer;
    private static CustomVertex.PositionColored[] m_BoundingBoxVerts;
    private static readonly short[] m_TriIndices = 
     {
        2,1,0, // Front Face
        2,3,1, // Front Face
        6,5,4, // Back Face
        7,5,6, // Back Face
        4,5,0, // Top Face
        5,2,0, // Top Face
        7,6,1, // Bottom Face
        3,7,1, // Bottom Face
        1,6,0, // Left Face
        0,6,4, // Left Face
        7,3,2, // Right Face
        7,2,5  // Right Face
     };
    private static readonly short[] m_LineIndices = 
     {0,1,
      0,2,
      3,1,
      3,2,
      4,5,
      4,6,
      7,5,
      7,6,
      0,5,
      1,6,
      2,7,
      3,4};
    public static void Initialize()
    {
      m_BoundingBoxVerts = new CustomVertex.PositionColored[8];
      for(int i=0; i<8; i++)
        m_BoundingBoxVerts[i] = new CustomVertex.PositionColored(0, 0, 0, Color.White.ToArgb());

      m_IndexBuffer = new IndexBuffer(typeof(short), 36 , MdxRender.Dev, Usage.WriteOnly, Pool.Default);
      m_IndexBuffer.Created += new EventHandler(RenderBox.OnIndexBufferCreate);
      OnIndexBufferCreate(m_IndexBuffer, null);

      m_VertexBuffer = new VertexBuffer(typeof(CustomVertex.PositionColored), 8, MdxRender.Dev, Usage.Dynamic |
        Usage.WriteOnly, CustomVertex.PositionColored.Format, Pool.Default);
      m_VertexBuffer.Created += new EventHandler(RenderBox.OnVertexBufferCreate);
      OnVertexBufferCreate(m_VertexBuffer, null);
    }

    /// <summary>
    /// This function recreates the buffer automatically following a device reset.
    /// </summary>
    /// <param name="sender">VertexBuffer object is the sender.</param>
    /// <param name="e">Not Used.</param>
    private static void OnVertexBufferCreate(object sender, EventArgs e)
    {
      VertexBuffer vb = (VertexBuffer)sender;
      GraphicsStream data = vb.Lock(0, 0, LockFlags.None);

      for(int v=0; v<8; v++)
        data.Write(m_BoundingBoxVerts[v]);

      vb.Unlock();
    }
    private static void OnIndexBufferCreate(object sender, EventArgs e)
    {
      IndexBuffer buffer = (IndexBuffer)sender;
      buffer.SetData(m_TriIndices, 0, LockFlags.None);
    }
    public static void RenderLines(ref BOUNDING_BOX2 bb2, Color color)
    {
      CustomVertex.PositionColored[] verts = new CustomVertex.PositionColored[8];

      // Vertices
      m_BoundingBoxVerts[0] = new CustomVertex.PositionColored(bb2.coordinates[0,0], bb2.coordinates[1,1], bb2.coordinates[2,1], color.ToArgb());
      m_BoundingBoxVerts[1] = new CustomVertex.PositionColored(bb2.coordinates[0,0], bb2.coordinates[1,0], bb2.coordinates[2,1], color.ToArgb());
      m_BoundingBoxVerts[2] = new CustomVertex.PositionColored(bb2.coordinates[0,1], bb2.coordinates[1,1], bb2.coordinates[2,1], color.ToArgb());
      m_BoundingBoxVerts[3] = new CustomVertex.PositionColored(bb2.coordinates[0,1], bb2.coordinates[1,0], bb2.coordinates[2,1], color.ToArgb());
      m_BoundingBoxVerts[4] = new CustomVertex.PositionColored(bb2.coordinates[0,0], bb2.coordinates[1,1], bb2.coordinates[2,0], color.ToArgb());
      m_BoundingBoxVerts[5] = new CustomVertex.PositionColored(bb2.coordinates[0,1], bb2.coordinates[1,1], bb2.coordinates[2,0], color.ToArgb());
      m_BoundingBoxVerts[6] = new CustomVertex.PositionColored(bb2.coordinates[0,0], bb2.coordinates[1,0], bb2.coordinates[2,0], color.ToArgb());
      m_BoundingBoxVerts[7] = new CustomVertex.PositionColored(bb2.coordinates[0,1], bb2.coordinates[1,0], bb2.coordinates[2,0], color.ToArgb());
      m_VertexBuffer.SetData(m_BoundingBoxVerts, 0, LockFlags.None);

      MdxRender.Dev.Indices = m_IndexBuffer;
      MdxRender.Dev.SetStreamSource(0, m_VertexBuffer, 0);
      MdxRender.Dev.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 8, 0, 12);
    }
  }
  public class RenderButton
  {
    private CustomVertex.TransformedTextured[] renderButtonVerts = new CustomVertex.TransformedTextured[4];
    private int InactiveTextureIndex = 0;
    private int PressedTextureIndex = 0;
    
    private Point origin = new Point();
    private float Height;
    private float Width;

    public void Initialize(int height, int width, int posx, int posy)
    {
      origin.X = posx;
      origin.Y = posy;
      Height = height;
      Width = width;

      renderButtonVerts[0] = new CustomVertex.TransformedTextured(origin.X,       origin.Y, 0.5f, 1, 0, 0);
      renderButtonVerts[1] = new CustomVertex.TransformedTextured(origin.X,       origin.Y+Height, 0.5f, 1, 0, 1);
      renderButtonVerts[2] = new CustomVertex.TransformedTextured(origin.X+width, origin.Y, 0.5f, 1, 1, 0);
      renderButtonVerts[3] = new CustomVertex.TransformedTextured(origin.X+width, origin.Y+Height, 0.5f, 1, 1, 1);
    }
    public void DrawButton(bool bPressed)
    {
      MdxRender.Dev.RenderState.SourceBlend = Blend.SourceAlpha;
      MdxRender.Dev.RenderState.DestinationBlend = Blend.InvSourceAlpha;
      MdxRender.Dev.RenderState.CullMode = Cull.None;

      MdxRender.Dev.RenderState.AlphaBlendEnable = true;
      MdxRender.Dev.RenderState.FogEnable = false;
      MdxRender.Dev.TextureState[0].AlphaArgument1 = TextureArgument.TextureColor;
      MdxRender.Dev.TextureState[0].AlphaOperation = TextureOperation.SelectArg1;
      MdxRender.Dev.TextureState[0].ColorArgument1 = TextureArgument.TextureColor;
      MdxRender.Dev.TextureState[0].ColorOperation = TextureOperation.ModulateInvAlphaAddColor;
      MdxRender.Dev.TextureState[0].TextureTransform = TextureTransform.Disable;
      MdxRender.SM.m_TextureManager.ActivateTexture(0, MdxRender.SM.m_TextureManager.NavArrowIndex, 0);

      MdxRender.Dev.VertexFormat = CustomVertex.TransformedTextured.Format;
      MdxRender.Dev.DrawUserPrimitives(PrimitiveType.TriangleStrip, 2, renderButtonVerts);
    }
    public bool MouseClick(int x, int y)
    {
      bool bClicked = false;

      if((x>origin.X)&&(y>origin.Y))
      {
        if((x<(origin.X+Width))&&(y<(origin.Y+Height)))
          bClicked = true;
      }
      return(bClicked);
    }
    public void UpdateButtonPlacement()
    {
    }
  }
}
