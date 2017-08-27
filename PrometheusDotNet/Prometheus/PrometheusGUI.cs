using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Timers;
using System.Windows.Forms;
using System.Xml;
using DevExpress.LookAndFeel;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTab;
using Prometheus.Controls;
using Prometheus.Core;
using Prometheus.Core.Project;
using Prometheus.Core.Render;
using Prometheus.Core.Tags;
using Prometheus.Core.Compiler;
using Prometheus.TagEditor;
using Prometheus.Testing;
using TagLibrary;
using TagLibrary.Types;
using UIControls.HackScriptEditor;
using Xceed.Compression;
using Timer = System.Timers.Timer;
using SharedControls;
using DependencyBuilder = Prometheus.Core.Compiler_Gren.DependencyBuilder;


namespace Prometheus
{
  /// <summary>
  /// Summary description for PrometheusGUI.
  /// </summary>
  public class PrometheusGUI : XtraForm
  {
    private DefaultLookAndFeel defaultLookAndFeel1;
    private BarManager barManager1;
    private BarDockControl barDockControlTop;
    private BarDockControl barDockControlBottom;
    private BarDockControl barDockControlLeft;
    private BarDockControl barDockControlRight;
    private DockManager dockManager1;
    private Bar bar1;
    private Bar bar2;
    private Bar bar3;
    private BarSubItem barSubItem4;
    private BarSubItem barSubItem6;
    private BarLinkContainerItem barLinkContainerItem1;
    private ControlContainer projectExplorer_Container;
    private ControlContainer dockPanel2_Container;
    private ControlContainer dockPanel3_Container;
    private DockPanel panelContainer1;
    private ControlContainer dockPanel4_Container;
    private PictureBox MdxControl;
    private ControlContainer dockPanel5_Container;
    private BarSubItem barSubItem10;
    private BarSubItem barSubItem11;
    private BarButtonItem barButtonItem11;
    private BarButtonItem barButtonItem12;
    private BarButtonItem barButtonItem13;
    private BarButtonItem barButtonItem14;
    private BarSubItem bsiFile;
    private BarSubItem bsiFile_New;
    private BarButtonItem bsiFile_New_File;
    private BarButtonItem bsiFile_New_Project;
    private BarSubItem bsiFile_Open;
    private BarButtonItem bsiFile_Open_File;
    private BarButtonItem bsiFile_Open_Project;
    private BarSubItem bsiEdit;
    private BarSubItem bsiView;
    private BarSubItem bsiBuild;
    private BarSubItem bsiHelp;
    private BarButtonItem bsiHelp_PrometheusHelp;
    private BarButtonItem bsiHelp_GettingStartedTutorial;
    private BarButtonItem bsiHelp_HaloDevOnline;
    private BarButtonItem bsiHelp_BugReports;
    private BarButtonItem bsiHelp_CheckForUpdates;
    private BarButtonItem bsiHelp_AboutPrometheus;
    private BarSubItem bsiTools;
    private BarSubItem bsiProject;
    private DockPanel dockTagExplorer;
    private DockPanel dockRenderWindow;
    private DockPanel dockRenderControls;
    private DockPanel dockOutput;
    private DockPanel dockTaskList;
    private XtraTabControl tabDocuments;
    private DockPanel dockHelp;
    private ControlContainer controlContainer1;
    private BarButtonItem bsiEdit_Undo;
    private BarButtonItem bsiEdit_Redo;
    private BarButtonItem bsiEdit_Cut;
    private BarButtonItem bsiEdit_Copy;
    private BarButtonItem bsiEdit_Paste;
    private BarButtonItem bsiEdit_SelectAll;
    private BarButtonItem bsiEdit_Find;
    private BarButtonItem bsiEdit_FindNext;
    private BarButtonItem bsiEdit_FindPrevious;
    private BarButtonItem bsiEdit_Replace;
    private BarCheckItem bsiView_MenuBar;
    private BarCheckItem bsiView_StatusBar;
    private BarSubItem bsiView_Windows;
    private BarSubItem bsiView_Toolbars;
    private BarCheckItem bsiView_FullScreenMode;
    private BarSubItem bsiView_UITheme;
    private BarSubItem bsiView_WindowLayout;
    private BarSubItem bsiView_Other;
    private RichTextBox rtbLog;
    private BarSubItem bsiProject_AddExisting;
    private BarSubItem bsiProject_CreateNew;
    private BarButtonItem bsiProject_SetAsStartupProject;
    private BarButtonItem bsiProject_ProjectDependencies;
    private BarButtonItem bsiProject_CloseProject;
    private BarButtonItem bsiProject_Properties;
    private BarButtonItem bsiFile_Close;
    private BarButtonItem bsiFile_Save;
    private BarButtonItem bsiFile_SaveAs;
    private BarButtonItem bsiFile_SaveEverything;
    private BarButtonItem bsiFile_Import;
    private BarButtonItem bsiFile_DecompileAMapFile;
    private BarButtonItem bsiFile_Exit;
    private BarSubItem barSubItem1;
    private BarCheckItem bsiView_UITheme_None;
    private BarCheckItem bsiView_UITheme_Default;
    private BarCheckItem bsiView_UITheme_Skin1;
    private BarCheckItem bsiView_UITheme_Skin2;
    private BarCheckItem bsiView_UITheme_Skin3;
    private BarCheckItem bsiView_UITheme_Skin4;
    private BarCheckItem bsiView_UITheme_Skin5;
    private BarCheckItem bsiView_UITheme_Skin6;
    private IContainer components;

    private MdxRender RenderEngine;
    private bool fullScreen;
    private Size size;
    private RepositoryItemTextEdit repositoryItemTextEdit1;
    private RepositoryItemTextEdit repositoryItemTextEdit2;
    private RepositoryItemComboBox repositoryItemComboBox1;
    private BarSubItem bsiDebug;
    private BarButtonItem barButtonItem1;
    private BarCheckItem bsiView_Other_FPSCounter;
    private BarCheckItem bsiView_Other_Fog;
    private ControlContainer controlContainer2;
    private DockPanel panelContainer3;
    private PopupMenu popupMenuRenderWin;
    private BarButtonItem barButtonItem2;
    private Point location;
    private Bitmap[] m_PopupImages;
    //custom control panels
    private TaskListGUI m_TaskListControl;
    private MapRenderCompact m_MapRenderCompact;
    private Timer timerRenderControls;
    private BarButtonItem barButtonTestTagBrowser;
    private DockPanel dockScenarioExplorer;
    private ControlContainer dockPanel1_Container;
    private ScenarioExplorer scenarioExplorer1;
    private BarButtonItem barButtonToolsOptions;
    private BarSubItem barPersonalTestingForms;
    private BarButtonItem bsiPersonalTestingForms_MonoxideC;
    private TagLibraryExplorer tagLibraryExplorer1;
    private BarButtonItem barButtonItemTestScript;
  	private BarButtonItem barButtonItem3;
    private XtraTabPage tabStartPage;
    private BarButtonItem barButtonItemGrenDebugger;
    private BarButtonItem barButtonItemBuildMap;
    private BarButtonItem barButtonItemRunRadiosity;
    private BarButtonItem barButtonItemBuildPrefab;
    private BarButtonItem barButtonItemDecompileMapfile;
    private DevExpress.XtraBars.Docking.DockPanel panelContainer2;
    private DevExpress.XtraBars.BarButtonItem barButtonItemScanDependencies;
    private DevExpress.XtraBars.BarButtonItem barButtonItem4;
    private DevExpress.XtraBars.BarSubItem barSubItemFileNewTag;
    private DevExpress.XtraBars.BarButtonItem barButtonItemNewBSP;
    private DevExpress.XtraBars.BarButtonItem barButtonItemNewSound;
    private DevExpress.XtraBars.BarButtonItem barButtonItemNewModel;
    private DevExpress.XtraBars.Docking.DockPanel dockProjectExplorer;
    private Prometheus.Controls.ProjectExplorer projectExplorer;
    private DevExpress.XtraBars.BarButtonItem barButtonItemViperTester;
    private DevExpress.XtraBars.BarButtonItem barButtonItemCompressMapfile;
    private DevExpress.XtraBars.BarButtonItem barButtonItemDecompressMapfile;
		private DevExpress.XtraBars.BarListItem barListItem1;
		private DevExpress.XtraBars.BarButtonItem barButtonItem5;
    private ModelRenderCompact m_ModelRenderCompact;

    public bool FullScreen  
    {
      get { return fullScreen; }
      set 
      {
        fullScreen = value;
        if (fullScreen) 
        {
          size = this.Size;
          location = this.Location;
          this.FormBorderStyle = FormBorderStyle.None;
          this.WindowState = FormWindowState.Maximized;
        }
        else 
        {
          this.WindowState = FormWindowState.Normal;
          this.FormBorderStyle = FormBorderStyle.Sizable;
          this.Location = location;
          this.Size = size;
        }
      }
    }

    public PrometheusGUI()
    {
      //Load Prometheus profile options from disk
      OptionsManager.LoadProfile();

      //Initialize Archive files
      Licenser.LicenseKey = "ZIN20-87A1K-SWNFN-W8AA";
      Xceed.FileSystem.Licenser.LicenseKey = "ZIN20-87A1K-SWNFN-W8AA";
      Xceed.Zip.Licenser.LicenseKey = "ZIN20-87A1K-SWNFN-W8AA";
      TagFileName.InitExtensionTables();
      TagLibraryManager.Initialize();

      //Create RenderEngine first so OnResize doesn't crash
      RenderEngine = new MdxRender();
      m_TaskListControl = new TaskListGUI();
      m_MapRenderCompact = new MapRenderCompact();
      m_ModelRenderCompact = new ModelRenderCompact();

      InitializeComponent();

      //Initialize TaskList control
      m_TaskListControl.TaskList = ProjectManager.TaskList;
      m_TaskListControl.Location = new Point(0, 0);
      m_TaskListControl.Size = dockTaskList.ClientSize;
      m_TaskListControl.Parent = dockTaskList;
      m_TaskListControl.View = View.Details;

      //Initialize Render Controls
      m_MapRenderCompact.Location = new Point(0,0);
      m_MapRenderCompact.Size = dockRenderControls.ClientSize;
      m_MapRenderCompact.Parent = dockRenderControls;
      m_MapRenderCompact.Hide();
      m_ModelRenderCompact.Location = new Point(0,0);
      m_ModelRenderCompact.Size = dockRenderControls.ClientSize;
      m_ModelRenderCompact.Parent = dockRenderControls;
      m_ModelRenderCompact.Show();

      EnableMapRenderControlMode();
      this.timerRenderControls.Start();

      // Set up our trace listeners
      Trace.Listeners.Add(new RichTextBoxTraceListener("LogWindow", ref rtbLog));
      Trace.Listeners.Add(new ConsoleTraceListener());

      // Setup the default render options for fog and fps.
      bsiView_Other_FPSCounter.Checked = true;
      MdxRender.ShowFPS = true;
      bsiView_Other_Fog.Checked = true;
      MdxRender.FogEnabled = true;

      this.tagLibraryExplorer1.PreviewActivated += new PreviewActivatedEventHandler(tagLibraryExplorer1_PreviewActivated);

      //Set up Image Lists for Popup Menu
      m_PopupImages = SharedControls.Utility.CreateImagesFromResourcePaths(
        new string[5] {
                        "Prometheus.Icons.App_Basics._16.document_edit.png",
                        "Prometheus.Icons.App_Basics._16.help2.png",
                        "Prometheus.Icons.App_Basics._16.media_play.png",
                        "Prometheus.Icons.Data_Coll._16.data_copy.png",
                        "Prometheus.Icons.Data_Coll._16.data_add.png"
                      });

      // Wire up custom events
      MdxControl.Click += new EventHandler(MdxControl_Click);
      MdxControl.MouseDown += new MouseEventHandler(MdxControl_MouseDown);
      MdxControl.MouseUp += new MouseEventHandler(MdxControl_MouseUp);
      MdxControl.MouseMove += new MouseEventHandler(MdxControl_MouseMove);
      MdxControl.MouseEnter += new EventHandler(MdxControl_MouseEnter);
      MdxControl.MouseLeave += new EventHandler(MdxControl_MouseLeave);
      dockRenderWindow.LostFocus += new EventHandler(dockRenderWindow_LostFocus);
      dockRenderWindow.GotFocus += new EventHandler(dockRenderWindow_GotFocus);
      dockRenderWindow.SizeChanged += new EventHandler(dockRenderWindow_SizeChanged);
      dockManager1.Docking += new DockingEventHandler(dockManager1_Docking);
      dockManager1.EndDocking += new EndDockingEventHandler(dockManager1_EndDocking);
      Application.Idle += new EventHandler(Application_Idle);

      bsiView_UITheme_None.ItemClick += new ItemClickEventHandler(ThemeClickHandler);
      bsiView_UITheme_Default.ItemClick += new ItemClickEventHandler(ThemeClickHandler);
      bsiView_UITheme_Skin1.ItemClick += new ItemClickEventHandler(ThemeClickHandler);
      bsiView_UITheme_Skin2.ItemClick += new ItemClickEventHandler(ThemeClickHandler);
      bsiView_UITheme_Skin3.ItemClick += new ItemClickEventHandler(ThemeClickHandler);
      bsiView_UITheme_Skin4.ItemClick += new ItemClickEventHandler(ThemeClickHandler);
      bsiView_UITheme_Skin5.ItemClick += new ItemClickEventHandler(ThemeClickHandler);
      bsiView_UITheme_Skin6.ItemClick += new ItemClickEventHandler(ThemeClickHandler);

      tabDocuments.TabPages.CollectionChanged += new CollectionChangeEventHandler(TabPages_CollectionChanged);
    }

    private void ThemeClickHandler(object sender, ItemClickEventArgs e)
    {
      (e.Item as BarCheckItem).Checked = true;

      // Activate the appropriate theme based on the state of the button that was pressed.
      if (e.Item.Caption == "None") this.defaultLookAndFeel1.LookAndFeel.SetFlatStyle();
      if (e.Item.Caption == "Default") this.defaultLookAndFeel1.LookAndFeel.SetSkinStyle("Caramel");
      if (e.Item.Caption == "Caramel") this.defaultLookAndFeel1.LookAndFeel.SetSkinStyle("Caramel");
      if (e.Item.Caption == "The Asphalt World") this.defaultLookAndFeel1.LookAndFeel.SetSkinStyle("The Asphalt World");
      if (e.Item.Caption == "Liquid Sky") this.defaultLookAndFeel1.LookAndFeel.SetSkinStyle("Liquid Sky");
      if (e.Item.Caption == "Coffee") this.defaultLookAndFeel1.LookAndFeel.SetSkinStyle("Coffee");
      if (e.Item.Caption == "Stardust") this.defaultLookAndFeel1.LookAndFeel.SetSkinStyle("Stardust");
      if (e.Item.Caption == "Glass Oceans") this.defaultLookAndFeel1.LookAndFeel.SetSkinStyle("Glass Oceans");
    }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main() 
    {
      using( new EnableThemingInScope(true) ) 
      {
        PrometheusGUI gui = new PrometheusGUI();
        gui.CreateControl();
        Application.Run(gui);
      }
    }

    #region RenderLoop Handling
    private void Application_Idle(object sender, EventArgs e)
    {
      while(AppStillIdle)
      {
        RenderEngine.RenderLoop();
      }
    }

    private bool AppStillIdle
    {
      get
      {
        Message msg;
        return !PeekMessage(out msg, IntPtr.Zero, 0, 0, 0);
      }
    }

    [StructLayout(LayoutKind.Sequential)]
      public struct Message
    {
      public IntPtr hWnd;
      public uint msg;  //WindowMessage
      public IntPtr wParam;
      public IntPtr lParam;
      public uint time;
      public Point p;
    }
    [SuppressUnmanagedCodeSecurity] // We won't use this maliciously
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern bool PeekMessage(out Message msg, IntPtr hWnd, uint messageFilterMin, uint messageFilterMax, uint flags);
    #endregion

    #region Event Handlers
    #region Control Events
    private void MdxControl_Click(object sender, EventArgs e)
    {
      this.dockRenderWindow.Focus();
      
      // Set this manually here, just in case the dock control
      // already has focus.
      RenderEngine.GetInput = true;
    }
    private void MdxControl_MouseDown(object sender, MouseEventArgs e)
    {
      if(MdxRender.PreviewManager.Mode == PreviewMode.ProjectMode)
      {
        ProjectManager.MapSpawns.MouseDown(e.X, e.Y);
      }
      else
      {
        MdxRender.IsRenderButtonClicked(e.X, e.Y);
      }
    }
    private void MdxControl_MouseUp(object sender, MouseEventArgs e)
    {
      ProjectManager.MapSpawns.MouseUp(e.X, e.Y);
    }
    private void MdxControl_MouseMove(object sender, MouseEventArgs e)
    {
      ProjectManager.MapSpawns.MouseMove(e.X, e.Y);
      MdxRender.MouseMove(e.X, e.Y, Color.Green);
    }

    private void MdxControl_MouseLeave(object sender, EventArgs e)
    {
      RenderEngine.MouseEnabled = false;
    }

    private void MdxControl_MouseEnter(object sender, EventArgs e)
    {
      RenderEngine.MouseEnabled = true;
    }
    private void dockRenderWindow_GotFocus(object sender, EventArgs e)
    {
      // The render window has the focus, so start accepting keyboard
      // and mouse input from it.
      RenderEngine.GetInput = true;
    }
    private void dockRenderWindow_LostFocus(object sender, EventArgs e)
    {
      // The render window has lost focus - we need to ignore input
      // until it receives focus again.
      RenderEngine.GetInput = false;
    }
    private void dockRenderWindow_SizeChanged(object sender, EventArgs e)
    {
      // Make sure that the window size is greater than 0,0
      if ((dockRenderWindow.Width > 0) && (dockRenderWindow.Height > 0))
        dockRenderWindow.FloatSize = dockRenderWindow.Size;
    }
    private void dockManager1_EndDocking(object sender, EndDockingEventArgs e)
    {
      // Docking has been completed - unpause rendering.
      RenderEngine.Pause = false;
    }
    private void dockManager1_Docking(object sender, DockingEventArgs e)
    {
      // A window has been undocked, and is being dragged.
      // Pause rendering to avoid flicker over the Render Window.
      RenderEngine.Pause = true;
    }
    #endregion
    #region Menu Events
    private void bsiFile_Exit_ItemClick(object sender, ItemClickEventArgs e)
    {
      // Exit the application by closing the form
      Trace.WriteLine("Application Closing", "info");
      this.Close();
    }
    private void bsiFile_New_Project_ItemClick(object sender, ItemClickEventArgs e)
    {
      // Start the New Project Wizard.
      Wizards.NewProjectWizard projectWizard = new Wizards.NewProjectWizard();
      if (projectWizard.ShowDialog() == DialogResult.Cancel)
      {
        return;
      }

      // Check current project status, and prompt to save if neccessary.
      if(OptionsManager.ActiveProjectPath != "")
      {
        if(ProjectManager.Dirty) //todo: check if dirty flag is set on project file and it's children (open docs, etc)
        {
          DialogResult result = Dialogs.ShowPrompt("Save changes to project?", MessageBoxButtons.YesNoCancel);

          if(result == DialogResult.Yes)
          {
            ProjectManager.CloseProject(true);
            //close gui items
          }
          else if(result == DialogResult.Cancel)
          {
            return;
          }
          else
          {
            //discard changes
            ProjectManager.CloseProject(false);
            //close gui items
          }
        }
      }

      //todo: Reset the project/guis and all that shit
      ProjectManager.OpenProject(projectWizard.ProjectFilename);
    }
    private void bsiFile_New_File_ItemClick(object sender, ItemClickEventArgs e)
    {
      // Add a new tag to the project.  Details of this process will be decided
      // upon later.
      Trace.WriteLine(sender + " : mbiFile_New_File");
    }
    private void bsiFile_Open_File_ItemClick(object sender, ItemClickEventArgs e)
    {
      // Browse for a tagfile
      TagBrowserDialog tbd;
      
      if(ProjectManager.ProjectLoaded)
        tbd = new TagBrowserDialog(ProjectManager.Version);
      else
        tbd = new TagBrowserDialog(MapfileVersion.HALOPC);//default version if no project loaded?

      tbd.AddFilter("All Files (*.*)|*.*");
      if (tbd.ShowDialog() == DialogResult.Cancel) return;
			TagFileName tagFile = tbd.SelectedTag;

      if(tagFile.Source == TagSource.Archive)
      {
        DialogResult result = DialogResult.Cancel;

        if(ProjectManager.ProjectLoaded)
        {
          result = MessageBox.Show("You selected an archive file, which cannot be modified.  Do you wish to edit this file?", 
            "Prometheus", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

          if(result == DialogResult.Yes)
          {
            //TODO: consider making "extract to project" a function for TFN to encapsulate functionality?
            //extract file to local project
            ZipTagLibrary archive = TagLibraryManager.GetLibrary(tagFile.Version);
            archive.ExtractFile(tagFile.RelativePath, OptionsManager.ActiveProjectTagsPath + tagFile.RelativePath);

            //modify tag file name source
            tagFile.Source = TagSource.LocalProject;
          }
        }
        else
        {
          result = MessageBox.Show("You selected an archive file, which cannot be modified.\r\nDo you wish to view this file in read only mode?", 
            "Prometheus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }

        if(result == DialogResult.Cancel)
          return;
      }

      // Load the proper tag definition.
      XmlDocument tagDefinition = TagDefinitionManager.GetTagDefinitionByName(tagFile.FileExtension);

      // Load the tag data
      TagBase tag = new TagBase();

      bool scenarioMatch = false;
      if (tagFile.FileExtension == "scenario")
      {
        if (ProjectManager.ProjectLoaded)
        {
          if (ProjectManager.m_ScenarioData.TagFilename == tagFile.RelativePath)
          {
            scenarioMatch = true;
          }
        }
      }
      
      IBlock tagBlock = null;
      if (!scenarioMatch)
      {
        tag.LoadTagBuffer(tagFile);
        BinaryReader br = new BinaryReader(tag.Stream);
     
        // Create the appropriate type.
        XmlNode nameNode = tagDefinition.SelectSingleNode("//name");
        string tagTypeString = "TagLibrary.Halo1." + nameNode.InnerText;
        Assembly a = Assembly.GetAssembly(typeof(IField));
        tagBlock = (a.CreateInstance(tagTypeString) as IBlock);
        
        tagBlock.Read(br);
        tagBlock.ReadChildData(br);
      }
      else
      {
        tagBlock = ProjectManager.m_ScenarioTag;
      }


      TagEditorControl tagEditor = new TagEditorControl();
      tagEditor.Dock = DockStyle.Fill;
      tagEditor.Create(tagDefinition, tagBlock);

      XtraTabPage tagEditorContainer = new XtraTabPage();
      tagEditorContainer.Text = Path.GetFileName(tagFile.RelativePath);
      tagEditorContainer.Controls.Add(tagEditor);
      tabDocuments.Controls.Add(tagEditorContainer);
      tabDocuments.TabPages.Add(tagEditorContainer);
      tabDocuments.SelectedTabPage = tagEditorContainer;
    }

    private void bsiFile_DecompileAMapFile_ItemClick(object sender, ItemClickEventArgs e)
    {
      Trace.WriteLine("File->Open->DecompileMapfile", "info");
      RenderEngine.Pause = true;
      
      // Browse for .map file
      OpenFileDialog ofd = new OpenFileDialog();
      ofd.Filter = "Halo Mapfile (*.map)|*.map";
      if(ofd.ShowDialog() == DialogResult.OK) 
      {
        // Load up the mapfile in the decompiler
        Trace.WriteLine("Selected '"+ofd.FileName+"' for decompilation","info");
        if (!File.Exists(ofd.FileName))
          throw new PrometheusException("Could not initialize map decompile - File not found: " + ofd.FileName, true);

        using( new EnableThemingInScope( true ) ) 
        {
          DecompileNavigator dn = new DecompileNavigator(ofd.FileName);

          dn.CreateControl();
          dn.ShowDialog();
        }
        
        //update the tag list
        this.tagLibraryExplorer1.TagLibrary = this.tagLibraryExplorer1.TagLibrary;

        RenderEngine.Pause = false;
      }
      else
      {
        Trace.WriteLine("Cancelled decompile mapfile.","info");
      }  
    }
    #endregion
    #region Form Events
    private void PrometheusGUI_Load(object sender, EventArgs e)
    {
      RenderEngine.SetControlReference(ref MdxControl);
      RenderEngine.InitMdx();
      MdxRender.Input.KeyboardInit(this);
      MdxRender.Input.MouseInit(this);
      RenderBox.Initialize();
      ProjectManager.Initialize();

      // TODO: We need to determine if the render window is visible,
      // and if so, give it focus.
      // For now, we'll assume that it has focus already.
      RenderEngine.GetInput = true;
    }
    #endregion
    #endregion

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose( bool disposing )
    {
      if( disposing )
      {
        if(components != null)
        {
          components.Dispose();
        }
      }
      
      // Perform some cleanup of our own.
      OptionsManager.SaveProfile();

      base.Dispose( disposing );
    }

    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PrometheusGUI));
			this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.barManager1 = new DevExpress.XtraBars.BarManager();
			this.bar1 = new DevExpress.XtraBars.Bar();
			this.bsiFile = new DevExpress.XtraBars.BarSubItem();
			this.bsiFile_New = new DevExpress.XtraBars.BarSubItem();
			this.bsiFile_New_File = new DevExpress.XtraBars.BarButtonItem();
			this.bsiFile_New_Project = new DevExpress.XtraBars.BarButtonItem();
			this.barSubItemFileNewTag = new DevExpress.XtraBars.BarSubItem();
			this.barButtonItemNewBSP = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemNewSound = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemNewModel = new DevExpress.XtraBars.BarButtonItem();
			this.bsiFile_Open = new DevExpress.XtraBars.BarSubItem();
			this.bsiFile_Open_File = new DevExpress.XtraBars.BarButtonItem();
			this.bsiFile_Open_Project = new DevExpress.XtraBars.BarButtonItem();
			this.bsiFile_Close = new DevExpress.XtraBars.BarButtonItem();
			this.bsiFile_Save = new DevExpress.XtraBars.BarButtonItem();
			this.bsiFile_SaveAs = new DevExpress.XtraBars.BarButtonItem();
			this.bsiFile_SaveEverything = new DevExpress.XtraBars.BarButtonItem();
			this.bsiFile_Import = new DevExpress.XtraBars.BarButtonItem();
			this.bsiFile_Exit = new DevExpress.XtraBars.BarButtonItem();
			this.bsiEdit = new DevExpress.XtraBars.BarSubItem();
			this.bsiEdit_Undo = new DevExpress.XtraBars.BarButtonItem();
			this.bsiEdit_Redo = new DevExpress.XtraBars.BarButtonItem();
			this.bsiEdit_Cut = new DevExpress.XtraBars.BarButtonItem();
			this.bsiEdit_Copy = new DevExpress.XtraBars.BarButtonItem();
			this.bsiEdit_Paste = new DevExpress.XtraBars.BarButtonItem();
			this.bsiEdit_SelectAll = new DevExpress.XtraBars.BarButtonItem();
			this.bsiEdit_Find = new DevExpress.XtraBars.BarButtonItem();
			this.bsiEdit_FindNext = new DevExpress.XtraBars.BarButtonItem();
			this.bsiEdit_FindPrevious = new DevExpress.XtraBars.BarButtonItem();
			this.bsiEdit_Replace = new DevExpress.XtraBars.BarButtonItem();
			this.bsiView = new DevExpress.XtraBars.BarSubItem();
			this.bsiView_MenuBar = new DevExpress.XtraBars.BarCheckItem();
			this.bsiView_StatusBar = new DevExpress.XtraBars.BarCheckItem();
			this.bsiView_Windows = new DevExpress.XtraBars.BarSubItem();
			this.bsiView_Toolbars = new DevExpress.XtraBars.BarSubItem();
			this.bsiView_FullScreenMode = new DevExpress.XtraBars.BarCheckItem();
			this.bsiView_UITheme = new DevExpress.XtraBars.BarSubItem();
			this.bsiView_UITheme_None = new DevExpress.XtraBars.BarCheckItem();
			this.bsiView_UITheme_Default = new DevExpress.XtraBars.BarCheckItem();
			this.bsiView_UITheme_Skin1 = new DevExpress.XtraBars.BarCheckItem();
			this.bsiView_UITheme_Skin2 = new DevExpress.XtraBars.BarCheckItem();
			this.bsiView_UITheme_Skin3 = new DevExpress.XtraBars.BarCheckItem();
			this.bsiView_UITheme_Skin4 = new DevExpress.XtraBars.BarCheckItem();
			this.bsiView_UITheme_Skin5 = new DevExpress.XtraBars.BarCheckItem();
			this.bsiView_UITheme_Skin6 = new DevExpress.XtraBars.BarCheckItem();
			this.bsiView_WindowLayout = new DevExpress.XtraBars.BarSubItem();
			this.bsiView_Other = new DevExpress.XtraBars.BarSubItem();
			this.bsiView_Other_FPSCounter = new DevExpress.XtraBars.BarCheckItem();
			this.bsiView_Other_Fog = new DevExpress.XtraBars.BarCheckItem();
			this.bsiBuild = new DevExpress.XtraBars.BarSubItem();
			this.barButtonItemBuildMap = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemRunRadiosity = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemBuildPrefab = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemScanDependencies = new DevExpress.XtraBars.BarButtonItem();
			this.bsiTools = new DevExpress.XtraBars.BarSubItem();
			this.barButtonToolsOptions = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemCompressMapfile = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemDecompressMapfile = new DevExpress.XtraBars.BarButtonItem();
			this.bsiHelp = new DevExpress.XtraBars.BarSubItem();
			this.bsiHelp_PrometheusHelp = new DevExpress.XtraBars.BarButtonItem();
			this.bsiHelp_GettingStartedTutorial = new DevExpress.XtraBars.BarButtonItem();
			this.bsiHelp_HaloDevOnline = new DevExpress.XtraBars.BarButtonItem();
			this.bsiHelp_BugReports = new DevExpress.XtraBars.BarButtonItem();
			this.bsiHelp_CheckForUpdates = new DevExpress.XtraBars.BarButtonItem();
			this.bsiHelp_AboutPrometheus = new DevExpress.XtraBars.BarButtonItem();
			this.bsiDebug = new DevExpress.XtraBars.BarSubItem();
			this.barButtonTestTagBrowser = new DevExpress.XtraBars.BarButtonItem();
			this.barPersonalTestingForms = new DevExpress.XtraBars.BarSubItem();
			this.bsiPersonalTestingForms_MonoxideC = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemTestScript = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemGrenDebugger = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemDecompileMapfile = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemViperTester = new DevExpress.XtraBars.BarButtonItem();
			this.bar2 = new DevExpress.XtraBars.Bar();
			this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
			this.bar3 = new DevExpress.XtraBars.Bar();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager();
			this.panelContainer3 = new DevExpress.XtraBars.Docking.DockPanel();
			this.dockProjectExplorer = new DevExpress.XtraBars.Docking.DockPanel();
			this.controlContainer2 = new DevExpress.XtraBars.Docking.ControlContainer();
			this.projectExplorer = new Prometheus.Controls.ProjectExplorer();
			this.dockTagExplorer = new DevExpress.XtraBars.Docking.DockPanel();
			this.dockPanel4_Container = new DevExpress.XtraBars.Docking.ControlContainer();
			this.tagLibraryExplorer1 = new Prometheus.Controls.TagLibraryExplorer();
			this.dockScenarioExplorer = new DevExpress.XtraBars.Docking.DockPanel();
			this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
			this.scenarioExplorer1 = new Prometheus.Controls.ScenarioExplorer();
			this.panelContainer1 = new DevExpress.XtraBars.Docking.DockPanel();
			this.dockOutput = new DevExpress.XtraBars.Docking.DockPanel();
			this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
			this.rtbLog = new System.Windows.Forms.RichTextBox();
			this.dockTaskList = new DevExpress.XtraBars.Docking.DockPanel();
			this.dockPanel3_Container = new DevExpress.XtraBars.Docking.ControlContainer();
			this.dockHelp = new DevExpress.XtraBars.Docking.DockPanel();
			this.controlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
			this.panelContainer2 = new DevExpress.XtraBars.Docking.DockPanel();
			this.dockRenderWindow = new DevExpress.XtraBars.Docking.DockPanel();
			this.projectExplorer_Container = new DevExpress.XtraBars.Docking.ControlContainer();
			this.MdxControl = new System.Windows.Forms.PictureBox();
			this.dockRenderControls = new DevExpress.XtraBars.Docking.DockPanel();
			this.dockPanel5_Container = new DevExpress.XtraBars.Docking.ControlContainer();
			this.barSubItem4 = new DevExpress.XtraBars.BarSubItem();
			this.barSubItem6 = new DevExpress.XtraBars.BarSubItem();
			this.barSubItem10 = new DevExpress.XtraBars.BarSubItem();
			this.barSubItem11 = new DevExpress.XtraBars.BarSubItem();
			this.barButtonItem11 = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItem12 = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItem13 = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItem14 = new DevExpress.XtraBars.BarButtonItem();
			this.bsiProject = new DevExpress.XtraBars.BarSubItem();
			this.bsiProject_AddExisting = new DevExpress.XtraBars.BarSubItem();
			this.bsiProject_CreateNew = new DevExpress.XtraBars.BarSubItem();
			this.bsiProject_SetAsStartupProject = new DevExpress.XtraBars.BarButtonItem();
			this.bsiProject_ProjectDependencies = new DevExpress.XtraBars.BarButtonItem();
			this.bsiProject_CloseProject = new DevExpress.XtraBars.BarButtonItem();
			this.bsiProject_Properties = new DevExpress.XtraBars.BarButtonItem();
			this.bsiFile_DecompileAMapFile = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
			this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			this.popupMenuRenderWin = new DevExpress.XtraBars.PopupMenu();
			this.barLinkContainerItem1 = new DevExpress.XtraBars.BarLinkContainerItem();
			this.tabDocuments = new DevExpress.XtraTab.XtraTabControl();
			this.tabStartPage = new DevExpress.XtraTab.XtraTabPage();
			this.timerRenderControls = new System.Timers.Timer();
			this.barListItem1 = new DevExpress.XtraBars.BarListItem();
			this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
			this.panelContainer3.SuspendLayout();
			this.dockProjectExplorer.SuspendLayout();
			this.controlContainer2.SuspendLayout();
			this.dockTagExplorer.SuspendLayout();
			this.dockPanel4_Container.SuspendLayout();
			this.dockScenarioExplorer.SuspendLayout();
			this.dockPanel1_Container.SuspendLayout();
			this.panelContainer1.SuspendLayout();
			this.dockOutput.SuspendLayout();
			this.dockPanel2_Container.SuspendLayout();
			this.dockTaskList.SuspendLayout();
			this.dockHelp.SuspendLayout();
			this.panelContainer2.SuspendLayout();
			this.dockRenderWindow.SuspendLayout();
			this.projectExplorer_Container.SuspendLayout();
			this.dockRenderControls.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.popupMenuRenderWin)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tabDocuments)).BeginInit();
			this.tabDocuments.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.timerRenderControls)).BeginInit();
			this.SuspendLayout();
			// 
			// defaultLookAndFeel1
			// 
			this.defaultLookAndFeel1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
			this.defaultLookAndFeel1.LookAndFeel.UseWindowsXPTheme = false;
			// 
			// barManager1
			// 
			this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
																																		 this.bar1,
																																		 this.bar2,
																																		 this.bar3});
			this.barManager1.DockControls.Add(this.barDockControlTop);
			this.barManager1.DockControls.Add(this.barDockControlBottom);
			this.barManager1.DockControls.Add(this.barDockControlLeft);
			this.barManager1.DockControls.Add(this.barDockControlRight);
			this.barManager1.DockManager = this.dockManager1;
			this.barManager1.Form = this;
			this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
																																					this.bsiFile,
																																					this.bsiEdit,
																																					this.bsiView,
																																					this.barSubItem4,
																																					this.bsiBuild,
																																					this.barSubItem6,
																																					this.bsiHelp,
																																					this.bsiFile_New,
																																					this.bsiFile_New_File,
																																					this.bsiFile_New_Project,
																																					this.bsiFile_Open,
																																					this.bsiFile_Open_File,
																																					this.bsiFile_Open_Project,
																																					this.bsiHelp_PrometheusHelp,
																																					this.bsiHelp_GettingStartedTutorial,
																																					this.bsiHelp_HaloDevOnline,
																																					this.bsiHelp_BugReports,
																																					this.bsiHelp_CheckForUpdates,
																																					this.bsiHelp_AboutPrometheus,
																																					this.barSubItem10,
																																					this.barSubItem11,
																																					this.barButtonItem11,
																																					this.barButtonItem12,
																																					this.barButtonItem13,
																																					this.barButtonItem14,
																																					this.bsiTools,
																																					this.bsiProject,
																																					this.bsiEdit_Undo,
																																					this.bsiEdit_Redo,
																																					this.bsiEdit_Cut,
																																					this.bsiEdit_Copy,
																																					this.bsiEdit_Paste,
																																					this.bsiEdit_SelectAll,
																																					this.bsiEdit_Find,
																																					this.bsiEdit_FindNext,
																																					this.bsiEdit_FindPrevious,
																																					this.bsiEdit_Replace,
																																					this.bsiView_MenuBar,
																																					this.bsiView_StatusBar,
																																					this.bsiView_Windows,
																																					this.bsiView_Toolbars,
																																					this.bsiView_FullScreenMode,
																																					this.bsiView_UITheme,
																																					this.bsiView_WindowLayout,
																																					this.bsiView_Other,
																																					this.bsiProject_AddExisting,
																																					this.bsiProject_CreateNew,
																																					this.bsiProject_SetAsStartupProject,
																																					this.bsiProject_ProjectDependencies,
																																					this.bsiProject_CloseProject,
																																					this.bsiProject_Properties,
																																					this.bsiFile_Close,
																																					this.bsiFile_Save,
																																					this.bsiFile_SaveAs,
																																					this.bsiFile_SaveEverything,
																																					this.bsiFile_Import,
																																					this.bsiFile_DecompileAMapFile,
																																					this.bsiFile_Exit,
																																					this.barSubItem1,
																																					this.bsiView_UITheme_None,
																																					this.bsiView_UITheme_Default,
																																					this.bsiView_UITheme_Skin1,
																																					this.bsiView_UITheme_Skin2,
																																					this.bsiView_UITheme_Skin3,
																																					this.bsiView_UITheme_Skin4,
																																					this.bsiView_UITheme_Skin5,
																																					this.bsiView_UITheme_Skin6,
																																					this.bsiDebug,
																																					this.barButtonItem1,
																																					this.bsiView_Other_FPSCounter,
																																					this.bsiView_Other_Fog,
																																					this.barButtonItem2,
																																					this.barButtonTestTagBrowser,
																																					this.barButtonToolsOptions,
																																					this.barPersonalTestingForms,
																																					this.bsiPersonalTestingForms_MonoxideC,
																																					this.barButtonItemTestScript,
																																					this.barButtonItem3,
																																					this.barButtonItemGrenDebugger,
																																					this.barButtonItemBuildMap,
																																					this.barButtonItemRunRadiosity,
																																					this.barButtonItemBuildPrefab,
																																					this.barButtonItemDecompileMapfile,
																																					this.barButtonItemScanDependencies,
																																					this.barButtonItem4,
																																					this.barSubItemFileNewTag,
																																					this.barButtonItemNewBSP,
																																					this.barButtonItemNewSound,
																																					this.barButtonItemNewModel,
																																					this.barButtonItemViperTester,
																																					this.barButtonItemCompressMapfile,
																																					this.barButtonItemDecompressMapfile,
																																					this.barListItem1,
																																					this.barButtonItem5});
			this.barManager1.MainMenu = this.bar1;
			this.barManager1.MaxItemId = 122;
			this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
																																																				 this.repositoryItemTextEdit1,
																																																				 this.repositoryItemTextEdit2,
																																																				 this.repositoryItemComboBox1});
			this.barManager1.StatusBar = this.bar3;
			// 
			// bar1
			// 
			this.bar1.BarItemHorzIndent = 7;
			this.bar1.BarName = "Custom 1";
			this.bar1.DockCol = 0;
			this.bar1.DockRow = 1;
			this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
																																											new DevExpress.XtraBars.LinkPersistInfo(this.bsiFile),
																																											new DevExpress.XtraBars.LinkPersistInfo(this.bsiEdit),
																																											new DevExpress.XtraBars.LinkPersistInfo(this.bsiView),
																																											new DevExpress.XtraBars.LinkPersistInfo(this.bsiBuild),
																																											new DevExpress.XtraBars.LinkPersistInfo(this.bsiTools),
																																											new DevExpress.XtraBars.LinkPersistInfo(this.bsiHelp),
																																											new DevExpress.XtraBars.LinkPersistInfo(this.bsiDebug)});
			this.bar1.OptionsBar.MultiLine = true;
			this.bar1.OptionsBar.UseWholeRow = true;
			this.bar1.Text = "Custom 1";
			// 
			// bsiFile
			// 
			this.bsiFile.Caption = "&File";
			this.bsiFile.Id = 0;
			this.bsiFile.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
																																												 new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bsiFile_New, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
																																												 new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.Caption | DevExpress.XtraBars.BarLinkUserDefines.PaintStyle))), this.bsiFile_Open, "&Open...", false, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiFile_Close),
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiFile_Save, true),
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiFile_SaveAs),
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiFile_SaveEverything),
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiFile_Import, true),
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiFile_Exit, true)});
			this.bsiFile.Name = "bsiFile";
			// 
			// bsiFile_New
			// 
			this.bsiFile_New.Caption = "&New...";
			this.bsiFile_New.Glyph = ((System.Drawing.Image)(resources.GetObject("bsiFile_New.Glyph")));
			this.bsiFile_New.Id = 8;
			this.bsiFile_New.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
																																														 new DevExpress.XtraBars.LinkPersistInfo(this.bsiFile_New_File),
																																														 new DevExpress.XtraBars.LinkPersistInfo(this.bsiFile_New_Project),
																																														 new DevExpress.XtraBars.LinkPersistInfo(this.barSubItemFileNewTag)});
			this.bsiFile_New.Name = "bsiFile_New";
			// 
			// bsiFile_New_File
			// 
			this.bsiFile_New_File.Caption = "&File...";
			this.bsiFile_New_File.Id = 9;
			this.bsiFile_New_File.Name = "bsiFile_New_File";
			this.bsiFile_New_File.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bsiFile_New_File_ItemClick);
			// 
			// bsiFile_New_Project
			// 
			this.bsiFile_New_Project.Caption = "&Project...";
			this.bsiFile_New_Project.Id = 13;
			this.bsiFile_New_Project.Name = "bsiFile_New_Project";
			this.bsiFile_New_Project.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bsiFile_New_Project_ItemClick);
			// 
			// barSubItemFileNewTag
			// 
			this.barSubItemFileNewTag.Caption = "Tag";
			this.barSubItemFileNewTag.Id = 113;
			this.barSubItemFileNewTag.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
																																																			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemNewBSP),
																																																			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemNewSound),
																																																			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemNewModel)});
			this.barSubItemFileNewTag.Name = "barSubItemFileNewTag";
			// 
			// barButtonItemNewBSP
			// 
			this.barButtonItemNewBSP.Caption = "BSP";
			this.barButtonItemNewBSP.Id = 114;
			this.barButtonItemNewBSP.Name = "barButtonItemNewBSP";
			this.barButtonItemNewBSP.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemNewBSP_ItemClick);
			// 
			// barButtonItemNewSound
			// 
			this.barButtonItemNewSound.Caption = "Sound";
			this.barButtonItemNewSound.Id = 115;
			this.barButtonItemNewSound.Name = "barButtonItemNewSound";
			// 
			// barButtonItemNewModel
			// 
			this.barButtonItemNewModel.Caption = "Model";
			this.barButtonItemNewModel.Id = 116;
			this.barButtonItemNewModel.Name = "barButtonItemNewModel";
			// 
			// bsiFile_Open
			// 
			this.bsiFile_Open.Caption = "&Open...";
			this.bsiFile_Open.Glyph = ((System.Drawing.Image)(resources.GetObject("bsiFile_Open.Glyph")));
			this.bsiFile_Open.Id = 14;
			this.bsiFile_Open.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
																																															new DevExpress.XtraBars.LinkPersistInfo(this.bsiFile_Open_File),
																																															new DevExpress.XtraBars.LinkPersistInfo(this.bsiFile_Open_Project)});
			this.bsiFile_Open.Name = "bsiFile_Open";
			// 
			// bsiFile_Open_File
			// 
			this.bsiFile_Open_File.Caption = "&File...";
			this.bsiFile_Open_File.Id = 15;
			this.bsiFile_Open_File.Name = "bsiFile_Open_File";
			this.bsiFile_Open_File.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bsiFile_Open_File_ItemClick);
			// 
			// bsiFile_Open_Project
			// 
			this.bsiFile_Open_Project.Caption = "&Project...";
			this.bsiFile_Open_Project.Id = 16;
			this.bsiFile_Open_Project.Name = "bsiFile_Open_Project";
			this.bsiFile_Open_Project.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bsiFile_Open_Project_ItemClick);
			// 
			// bsiFile_Close
			// 
			this.bsiFile_Close.Caption = "&Close";
			this.bsiFile_Close.Id = 64;
			this.bsiFile_Close.Name = "bsiFile_Close";
			// 
			// bsiFile_Save
			// 
			this.bsiFile_Save.Caption = "&Save";
			this.bsiFile_Save.Glyph = ((System.Drawing.Image)(resources.GetObject("bsiFile_Save.Glyph")));
			this.bsiFile_Save.Id = 65;
			this.bsiFile_Save.Name = "bsiFile_Save";
			// 
			// bsiFile_SaveAs
			// 
			this.bsiFile_SaveAs.Caption = "Save &As...";
			this.bsiFile_SaveAs.Glyph = ((System.Drawing.Image)(resources.GetObject("bsiFile_SaveAs.Glyph")));
			this.bsiFile_SaveAs.Id = 66;
			this.bsiFile_SaveAs.Name = "bsiFile_SaveAs";
			// 
			// bsiFile_SaveEverything
			// 
			this.bsiFile_SaveEverything.Caption = "Save &Everything";
			this.bsiFile_SaveEverything.Glyph = ((System.Drawing.Image)(resources.GetObject("bsiFile_SaveEverything.Glyph")));
			this.bsiFile_SaveEverything.Id = 67;
			this.bsiFile_SaveEverything.Name = "bsiFile_SaveEverything";
			this.bsiFile_SaveEverything.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bsiFile_SaveEverything_ItemClick);
			// 
			// bsiFile_Import
			// 
			this.bsiFile_Import.Caption = "&Import";
			this.bsiFile_Import.Glyph = ((System.Drawing.Image)(resources.GetObject("bsiFile_Import.Glyph")));
			this.bsiFile_Import.Id = 68;
			this.bsiFile_Import.Name = "bsiFile_Import";
			this.bsiFile_Import.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bsiFile_Import_ItemClick);
			// 
			// bsiFile_Exit
			// 
			this.bsiFile_Exit.Caption = "E&xit";
			this.bsiFile_Exit.Id = 70;
			this.bsiFile_Exit.Name = "bsiFile_Exit";
			this.bsiFile_Exit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bsiFile_Exit_ItemClick);
			// 
			// bsiEdit
			// 
			this.bsiEdit.Caption = "&Edit";
			this.bsiEdit.Id = 1;
			this.bsiEdit.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiEdit_Undo),
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiEdit_Redo),
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiEdit_Cut, true),
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiEdit_Copy),
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiEdit_Paste),
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiEdit_SelectAll),
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiEdit_Find, true),
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiEdit_FindNext),
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiEdit_FindPrevious),
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiEdit_Replace)});
			this.bsiEdit.Name = "bsiEdit";
			// 
			// bsiEdit_Undo
			// 
			this.bsiEdit_Undo.Caption = "&Undo";
			this.bsiEdit_Undo.Glyph = ((System.Drawing.Image)(resources.GetObject("bsiEdit_Undo.Glyph")));
			this.bsiEdit_Undo.Id = 34;
			this.bsiEdit_Undo.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z));
			this.bsiEdit_Undo.Name = "bsiEdit_Undo";
			this.bsiEdit_Undo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bsiEdit_Undo_ItemClick);
			// 
			// bsiEdit_Redo
			// 
			this.bsiEdit_Redo.Caption = "&Redo";
			this.bsiEdit_Redo.Glyph = ((System.Drawing.Image)(resources.GetObject("bsiEdit_Redo.Glyph")));
			this.bsiEdit_Redo.Id = 35;
			this.bsiEdit_Redo.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R));
			this.bsiEdit_Redo.Name = "bsiEdit_Redo";
			this.bsiEdit_Redo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bsiEdit_Redo_ItemClick);
			// 
			// bsiEdit_Cut
			// 
			this.bsiEdit_Cut.Caption = "Cu&t";
			this.bsiEdit_Cut.Glyph = ((System.Drawing.Image)(resources.GetObject("bsiEdit_Cut.Glyph")));
			this.bsiEdit_Cut.Id = 36;
			this.bsiEdit_Cut.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X));
			this.bsiEdit_Cut.Name = "bsiEdit_Cut";
			// 
			// bsiEdit_Copy
			// 
			this.bsiEdit_Copy.Caption = "&Copy";
			this.bsiEdit_Copy.Glyph = ((System.Drawing.Image)(resources.GetObject("bsiEdit_Copy.Glyph")));
			this.bsiEdit_Copy.Id = 37;
			this.bsiEdit_Copy.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C));
			this.bsiEdit_Copy.Name = "bsiEdit_Copy";
			// 
			// bsiEdit_Paste
			// 
			this.bsiEdit_Paste.Caption = "&Paste";
			this.bsiEdit_Paste.Glyph = ((System.Drawing.Image)(resources.GetObject("bsiEdit_Paste.Glyph")));
			this.bsiEdit_Paste.Id = 38;
			this.bsiEdit_Paste.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V));
			this.bsiEdit_Paste.Name = "bsiEdit_Paste";
			// 
			// bsiEdit_SelectAll
			// 
			this.bsiEdit_SelectAll.Caption = "Select &All";
			this.bsiEdit_SelectAll.Id = 39;
			this.bsiEdit_SelectAll.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A));
			this.bsiEdit_SelectAll.Name = "bsiEdit_SelectAll";
			// 
			// bsiEdit_Find
			// 
			this.bsiEdit_Find.Caption = "&Find";
			this.bsiEdit_Find.Glyph = ((System.Drawing.Image)(resources.GetObject("bsiEdit_Find.Glyph")));
			this.bsiEdit_Find.Id = 40;
			this.bsiEdit_Find.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F));
			this.bsiEdit_Find.Name = "bsiEdit_Find";
			// 
			// bsiEdit_FindNext
			// 
			this.bsiEdit_FindNext.Caption = "Find &Next";
			this.bsiEdit_FindNext.Glyph = ((System.Drawing.Image)(resources.GetObject("bsiEdit_FindNext.Glyph")));
			this.bsiEdit_FindNext.Id = 41;
			this.bsiEdit_FindNext.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2);
			this.bsiEdit_FindNext.Name = "bsiEdit_FindNext";
			// 
			// bsiEdit_FindPrevious
			// 
			this.bsiEdit_FindPrevious.Caption = "Find &Previous";
			this.bsiEdit_FindPrevious.Glyph = ((System.Drawing.Image)(resources.GetObject("bsiEdit_FindPrevious.Glyph")));
			this.bsiEdit_FindPrevious.Id = 42;
			this.bsiEdit_FindPrevious.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3);
			this.bsiEdit_FindPrevious.Name = "bsiEdit_FindPrevious";
			// 
			// bsiEdit_Replace
			// 
			this.bsiEdit_Replace.Caption = "R&eplace";
			this.bsiEdit_Replace.Glyph = ((System.Drawing.Image)(resources.GetObject("bsiEdit_Replace.Glyph")));
			this.bsiEdit_Replace.Id = 43;
			this.bsiEdit_Replace.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H));
			this.bsiEdit_Replace.Name = "bsiEdit_Replace";
			// 
			// bsiView
			// 
			this.bsiView.Caption = "&View";
			this.bsiView.Id = 2;
			this.bsiView.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiView_MenuBar),
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiView_StatusBar),
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiView_Windows),
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiView_Toolbars),
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiView_FullScreenMode, true),
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiView_UITheme, true),
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiView_WindowLayout),
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiView_Other, true)});
			this.bsiView.Name = "bsiView";
			// 
			// bsiView_MenuBar
			// 
			this.bsiView_MenuBar.Caption = "&Menu Bar";
			this.bsiView_MenuBar.Id = 44;
			this.bsiView_MenuBar.Name = "bsiView_MenuBar";
			// 
			// bsiView_StatusBar
			// 
			this.bsiView_StatusBar.Caption = "&Status Bar";
			this.bsiView_StatusBar.Id = 45;
			this.bsiView_StatusBar.Name = "bsiView_StatusBar";
			// 
			// bsiView_Windows
			// 
			this.bsiView_Windows.Caption = "&Windows";
			this.bsiView_Windows.Id = 51;
			this.bsiView_Windows.Name = "bsiView_Windows";
			// 
			// bsiView_Toolbars
			// 
			this.bsiView_Toolbars.Caption = "Tool&bars";
			this.bsiView_Toolbars.Id = 52;
			this.bsiView_Toolbars.Name = "bsiView_Toolbars";
			// 
			// bsiView_FullScreenMode
			// 
			this.bsiView_FullScreenMode.Caption = "&Full Screen Mode";
			this.bsiView_FullScreenMode.Glyph = ((System.Drawing.Image)(resources.GetObject("bsiView_FullScreenMode.Glyph")));
			this.bsiView_FullScreenMode.Id = 53;
			this.bsiView_FullScreenMode.Name = "bsiView_FullScreenMode";
			// 
			// bsiView_UITheme
			// 
			this.bsiView_UITheme.Caption = "&UI Theme";
			this.bsiView_UITheme.Id = 54;
			this.bsiView_UITheme.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
																																																 new DevExpress.XtraBars.LinkPersistInfo(this.bsiView_UITheme_None),
																																																 new DevExpress.XtraBars.LinkPersistInfo(this.bsiView_UITheme_Default),
																																																 new DevExpress.XtraBars.LinkPersistInfo(this.bsiView_UITheme_Skin1, true),
																																																 new DevExpress.XtraBars.LinkPersistInfo(this.bsiView_UITheme_Skin2),
																																																 new DevExpress.XtraBars.LinkPersistInfo(this.bsiView_UITheme_Skin3),
																																																 new DevExpress.XtraBars.LinkPersistInfo(this.bsiView_UITheme_Skin4),
																																																 new DevExpress.XtraBars.LinkPersistInfo(this.bsiView_UITheme_Skin5),
																																																 new DevExpress.XtraBars.LinkPersistInfo(this.bsiView_UITheme_Skin6)});
			this.bsiView_UITheme.Name = "bsiView_UITheme";
			// 
			// bsiView_UITheme_None
			// 
			this.bsiView_UITheme_None.Caption = "None";
			this.bsiView_UITheme_None.GroupIndex = 1;
			this.bsiView_UITheme_None.Id = 82;
			this.bsiView_UITheme_None.Name = "bsiView_UITheme_None";
			// 
			// bsiView_UITheme_Default
			// 
			this.bsiView_UITheme_Default.Caption = "Default";
			this.bsiView_UITheme_Default.Checked = true;
			this.bsiView_UITheme_Default.GroupIndex = 1;
			this.bsiView_UITheme_Default.Id = 83;
			this.bsiView_UITheme_Default.Name = "bsiView_UITheme_Default";
			// 
			// bsiView_UITheme_Skin1
			// 
			this.bsiView_UITheme_Skin1.Caption = "Caramel";
			this.bsiView_UITheme_Skin1.GroupIndex = 1;
			this.bsiView_UITheme_Skin1.Id = 84;
			this.bsiView_UITheme_Skin1.Name = "bsiView_UITheme_Skin1";
			// 
			// bsiView_UITheme_Skin2
			// 
			this.bsiView_UITheme_Skin2.Caption = "The Asphalt World";
			this.bsiView_UITheme_Skin2.GroupIndex = 1;
			this.bsiView_UITheme_Skin2.Id = 85;
			this.bsiView_UITheme_Skin2.Name = "bsiView_UITheme_Skin2";
			// 
			// bsiView_UITheme_Skin3
			// 
			this.bsiView_UITheme_Skin3.Caption = "Liquid Sky";
			this.bsiView_UITheme_Skin3.GroupIndex = 1;
			this.bsiView_UITheme_Skin3.Id = 86;
			this.bsiView_UITheme_Skin3.Name = "bsiView_UITheme_Skin3";
			// 
			// bsiView_UITheme_Skin4
			// 
			this.bsiView_UITheme_Skin4.Caption = "Coffee";
			this.bsiView_UITheme_Skin4.GroupIndex = 1;
			this.bsiView_UITheme_Skin4.Id = 87;
			this.bsiView_UITheme_Skin4.Name = "bsiView_UITheme_Skin4";
			// 
			// bsiView_UITheme_Skin5
			// 
			this.bsiView_UITheme_Skin5.Caption = "Stardust";
			this.bsiView_UITheme_Skin5.GroupIndex = 1;
			this.bsiView_UITheme_Skin5.Id = 88;
			this.bsiView_UITheme_Skin5.Name = "bsiView_UITheme_Skin5";
			// 
			// bsiView_UITheme_Skin6
			// 
			this.bsiView_UITheme_Skin6.Caption = "Glass Oceans";
			this.bsiView_UITheme_Skin6.GroupIndex = 1;
			this.bsiView_UITheme_Skin6.Id = 89;
			this.bsiView_UITheme_Skin6.Name = "bsiView_UITheme_Skin6";
			// 
			// bsiView_WindowLayout
			// 
			this.bsiView_WindowLayout.Caption = "&Window Layout";
			this.bsiView_WindowLayout.Id = 55;
			this.bsiView_WindowLayout.Name = "bsiView_WindowLayout";
			// 
			// bsiView_Other
			// 
			this.bsiView_Other.Caption = "&Other";
			this.bsiView_Other.Id = 56;
			this.bsiView_Other.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
																																															 new DevExpress.XtraBars.LinkPersistInfo(this.bsiView_Other_FPSCounter),
																																															 new DevExpress.XtraBars.LinkPersistInfo(this.bsiView_Other_Fog)});
			this.bsiView_Other.Name = "bsiView_Other";
			// 
			// bsiView_Other_FPSCounter
			// 
			this.bsiView_Other_FPSCounter.Caption = "FPS Counter";
			this.bsiView_Other_FPSCounter.Id = 96;
			this.bsiView_Other_FPSCounter.Name = "bsiView_Other_FPSCounter";
			this.bsiView_Other_FPSCounter.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItem1_CheckedChanged);
			// 
			// bsiView_Other_Fog
			// 
			this.bsiView_Other_Fog.Caption = "Fog";
			this.bsiView_Other_Fog.Id = 97;
			this.bsiView_Other_Fog.Name = "bsiView_Other_Fog";
			this.bsiView_Other_Fog.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bsiView_Other_Fog_CheckedChanged);
			// 
			// bsiBuild
			// 
			this.bsiBuild.Caption = "&Build";
			this.bsiBuild.Id = 4;
			this.bsiBuild.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
																																													new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemBuildMap),
																																													new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemRunRadiosity),
																																													new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemBuildPrefab),
																																													new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemScanDependencies)});
			this.bsiBuild.Name = "bsiBuild";
			// 
			// barButtonItemBuildMap
			// 
			this.barButtonItemBuildMap.Caption = "Build Map";
			this.barButtonItemBuildMap.Id = 106;
			this.barButtonItemBuildMap.Name = "barButtonItemBuildMap";
			this.barButtonItemBuildMap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemBuildMap_ItemClick);
			// 
			// barButtonItemRunRadiosity
			// 
			this.barButtonItemRunRadiosity.Caption = "Run Radiosity...";
			this.barButtonItemRunRadiosity.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemRunRadiosity.Glyph")));
			this.barButtonItemRunRadiosity.Id = 107;
			this.barButtonItemRunRadiosity.Name = "barButtonItemRunRadiosity";
			this.barButtonItemRunRadiosity.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemRunRadiosity_ItemClick);
			// 
			// barButtonItemBuildPrefab
			// 
			this.barButtonItemBuildPrefab.Caption = "Build Prefab...";
			this.barButtonItemBuildPrefab.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemBuildPrefab.Glyph")));
			this.barButtonItemBuildPrefab.Id = 108;
			this.barButtonItemBuildPrefab.Name = "barButtonItemBuildPrefab";
			this.barButtonItemBuildPrefab.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemBuildPrefab_ItemClick);
			// 
			// barButtonItemScanDependencies
			// 
			this.barButtonItemScanDependencies.Caption = "Scan Dependencies";
			this.barButtonItemScanDependencies.Id = 111;
			this.barButtonItemScanDependencies.Name = "barButtonItemScanDependencies";
			this.barButtonItemScanDependencies.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemScanDependencies_ItemClick);
			// 
			// bsiTools
			// 
			this.bsiTools.Caption = "&Tools";
			this.bsiTools.Id = 31;
			this.bsiTools.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
																																													new DevExpress.XtraBars.LinkPersistInfo(this.barButtonToolsOptions),
																																													new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemCompressMapfile),
																																													new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemDecompressMapfile)});
			this.bsiTools.Name = "bsiTools";
			// 
			// barButtonToolsOptions
			// 
			this.barButtonToolsOptions.Caption = "Options...";
			this.barButtonToolsOptions.Id = 100;
			this.barButtonToolsOptions.Name = "barButtonToolsOptions";
			this.barButtonToolsOptions.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonToolsOptions_ItemClick);
			// 
			// barButtonItemCompressMapfile
			// 
			this.barButtonItemCompressMapfile.Caption = "Compress Mapfile...";
			this.barButtonItemCompressMapfile.Id = 118;
			this.barButtonItemCompressMapfile.Name = "barButtonItemCompressMapfile";
			this.barButtonItemCompressMapfile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemCompressMapfile_ItemClick);
			// 
			// barButtonItemDecompressMapfile
			// 
			this.barButtonItemDecompressMapfile.Caption = "Decompress Mapfile...";
			this.barButtonItemDecompressMapfile.Id = 119;
			this.barButtonItemDecompressMapfile.Name = "barButtonItemDecompressMapfile";
			this.barButtonItemDecompressMapfile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemDecompressMapfile_ItemClick);
			// 
			// bsiHelp
			// 
			this.bsiHelp.Caption = "&Help";
			this.bsiHelp.Id = 6;
			this.bsiHelp.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiHelp_PrometheusHelp),
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiHelp_GettingStartedTutorial),
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiHelp_HaloDevOnline),
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiHelp_BugReports, true),
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiHelp_CheckForUpdates),
																																												 new DevExpress.XtraBars.LinkPersistInfo(this.bsiHelp_AboutPrometheus, true)});
			this.bsiHelp.Name = "bsiHelp";
			// 
			// bsiHelp_PrometheusHelp
			// 
			this.bsiHelp_PrometheusHelp.Caption = "Prometheus &Help";
			this.bsiHelp_PrometheusHelp.Glyph = ((System.Drawing.Image)(resources.GetObject("bsiHelp_PrometheusHelp.Glyph")));
			this.bsiHelp_PrometheusHelp.Id = 17;
			this.bsiHelp_PrometheusHelp.Name = "bsiHelp_PrometheusHelp";
			// 
			// bsiHelp_GettingStartedTutorial
			// 
			this.bsiHelp_GettingStartedTutorial.Caption = "&Getting Started Tutorial";
			this.bsiHelp_GettingStartedTutorial.Id = 18;
			this.bsiHelp_GettingStartedTutorial.Name = "bsiHelp_GettingStartedTutorial";
			// 
			// bsiHelp_HaloDevOnline
			// 
			this.bsiHelp_HaloDevOnline.Caption = "HaloDev &Online";
			this.bsiHelp_HaloDevOnline.Id = 19;
			this.bsiHelp_HaloDevOnline.Name = "bsiHelp_HaloDevOnline";
			this.bsiHelp_HaloDevOnline.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bsiHelp_HaloDevOnline_ItemClick);
			// 
			// bsiHelp_BugReports
			// 
			this.bsiHelp_BugReports.Caption = "Bug &Reports";
			this.bsiHelp_BugReports.Glyph = ((System.Drawing.Image)(resources.GetObject("bsiHelp_BugReports.Glyph")));
			this.bsiHelp_BugReports.Id = 20;
			this.bsiHelp_BugReports.Name = "bsiHelp_BugReports";
			// 
			// bsiHelp_CheckForUpdates
			// 
			this.bsiHelp_CheckForUpdates.Caption = "Check for &Updates";
			this.bsiHelp_CheckForUpdates.Id = 21;
			this.bsiHelp_CheckForUpdates.Name = "bsiHelp_CheckForUpdates";
			// 
			// bsiHelp_AboutPrometheus
			// 
			this.bsiHelp_AboutPrometheus.Caption = "&About Prometheus";
			this.bsiHelp_AboutPrometheus.Id = 22;
			this.bsiHelp_AboutPrometheus.Name = "bsiHelp_AboutPrometheus";
			// 
			// bsiDebug
			// 
			this.bsiDebug.Caption = "&Debug";
			this.bsiDebug.Id = 93;
			this.bsiDebug.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
																																													new DevExpress.XtraBars.LinkPersistInfo(this.barButtonTestTagBrowser),
																																													new DevExpress.XtraBars.LinkPersistInfo(this.barPersonalTestingForms),
																																													new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemTestScript),
																																													new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemGrenDebugger),
																																													new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemDecompileMapfile),
																																													new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemViperTester),
																																													new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem5)});
			this.bsiDebug.Name = "bsiDebug";
			// 
			// barButtonTestTagBrowser
			// 
			this.barButtonTestTagBrowser.Caption = "Test Tag Browser...";
			this.barButtonTestTagBrowser.Id = 99;
			this.barButtonTestTagBrowser.Name = "barButtonTestTagBrowser";
			this.barButtonTestTagBrowser.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonTestTagBrowser_ItemClick);
			// 
			// barPersonalTestingForms
			// 
			this.barPersonalTestingForms.Caption = "&Personal Testing Forms...";
			this.barPersonalTestingForms.Id = 101;
			this.barPersonalTestingForms.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
																																																				 new DevExpress.XtraBars.LinkPersistInfo(this.bsiPersonalTestingForms_MonoxideC)});
			this.barPersonalTestingForms.Name = "barPersonalTestingForms";
			// 
			// bsiPersonalTestingForms_MonoxideC
			// 
			this.bsiPersonalTestingForms_MonoxideC.Caption = "&MonoxideC";
			this.bsiPersonalTestingForms_MonoxideC.Id = 102;
			this.bsiPersonalTestingForms_MonoxideC.Name = "bsiPersonalTestingForms_MonoxideC";
			this.bsiPersonalTestingForms_MonoxideC.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bsiPersonalTestingForms_MonoxideC_ItemClick);
			// 
			// barButtonItemTestScript
			// 
			this.barButtonItemTestScript.Caption = "Test Script Editor";
			this.barButtonItemTestScript.Id = 103;
			this.barButtonItemTestScript.Name = "barButtonItemTestScript";
			this.barButtonItemTestScript.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemTestScript_ItemClick);
			// 
			// barButtonItemGrenDebugger
			// 
			this.barButtonItemGrenDebugger.Caption = "Gren Debugger...";
			this.barButtonItemGrenDebugger.Id = 105;
			this.barButtonItemGrenDebugger.Name = "barButtonItemGrenDebugger";
			this.barButtonItemGrenDebugger.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemGrenDebugger_ItemClick);
			// 
			// barButtonItemDecompileMapfile
			// 
			this.barButtonItemDecompileMapfile.Caption = "Add to Tag Library...";
			this.barButtonItemDecompileMapfile.Id = 110;
			this.barButtonItemDecompileMapfile.Name = "barButtonItemDecompileMapfile";
			this.barButtonItemDecompileMapfile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemDecompileMapfile_ItemClick);
			// 
			// barButtonItemViperTester
			// 
			this.barButtonItemViperTester.Caption = "Viper Test Dependencies";
			this.barButtonItemViperTester.Id = 117;
			this.barButtonItemViperTester.Name = "barButtonItemViperTester";
			this.barButtonItemViperTester.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemViperTester_ItemClick);
			// 
			// bar2
			// 
			this.bar2.BarName = "Custom 2";
			this.bar2.DockCol = 0;
			this.bar2.DockRow = 2;
			this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar2.FloatLocation = new System.Drawing.Point(41, 190);
			this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
																																											new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.Caption | DevExpress.XtraBars.BarLinkUserDefines.PaintStyle))), this.bsiFile_New, "", false, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
																																											new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.Caption | DevExpress.XtraBars.BarLinkUserDefines.PaintStyle))), this.bsiFile_Open, "", false, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
																																											new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.Caption | DevExpress.XtraBars.BarLinkUserDefines.PaintStyle))), this.barSubItem1, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
																																											new DevExpress.XtraBars.LinkPersistInfo(this.bsiEdit_Undo, true),
																																											new DevExpress.XtraBars.LinkPersistInfo(this.bsiEdit_Redo),
																																											new DevExpress.XtraBars.LinkPersistInfo(this.bsiEdit_Cut, true),
																																											new DevExpress.XtraBars.LinkPersistInfo(this.bsiEdit_Copy),
																																											new DevExpress.XtraBars.LinkPersistInfo(this.bsiEdit_Paste),
																																											new DevExpress.XtraBars.LinkPersistInfo(this.bsiEdit_Find, true),
																																											new DevExpress.XtraBars.LinkPersistInfo(this.bsiEdit_FindNext),
																																											new DevExpress.XtraBars.LinkPersistInfo(this.bsiEdit_FindPrevious),
																																											new DevExpress.XtraBars.LinkPersistInfo(this.bsiEdit_Replace)});
			this.bar2.Text = "Custom 2";
			// 
			// barSubItem1
			// 
			this.barSubItem1.Caption = "File";
			this.barSubItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("barSubItem1.Glyph")));
			this.barSubItem1.Id = 71;
			this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
																																														 new DevExpress.XtraBars.LinkPersistInfo(this.bsiFile_Save),
																																														 new DevExpress.XtraBars.LinkPersistInfo(this.bsiFile_SaveAs),
																																														 new DevExpress.XtraBars.LinkPersistInfo(this.bsiFile_SaveEverything)});
			this.barSubItem1.Name = "barSubItem1";
			// 
			// bar3
			// 
			this.bar3.BarName = "Custom 3";
			this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
			this.bar3.DockCol = 0;
			this.bar3.DockRow = 1;
			this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
			this.bar3.OptionsBar.AllowQuickCustomization = false;
			this.bar3.OptionsBar.DrawDragBorder = false;
			this.bar3.OptionsBar.UseWholeRow = true;
			this.bar3.Text = "Custom 3";
			// 
			// dockManager1
			// 
			this.dockManager1.DockingOptions.HideImmediatelyOnAutoHide = true;
			this.dockManager1.Form = this;
			this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
																																													this.panelContainer3,
																																													this.panelContainer1,
																																													this.panelContainer2});
			this.dockManager1.TopZIndexControls.AddRange(new string[] {
																																	"DevExpress.XtraBars.BarDockControl",
																																	"System.Windows.Forms.StatusBar"});
			// 
			// panelContainer3
			// 
			this.panelContainer3.ActiveChild = this.dockProjectExplorer;
			this.panelContainer3.Controls.Add(this.dockTagExplorer);
			this.panelContainer3.Controls.Add(this.dockProjectExplorer);
			this.panelContainer3.Controls.Add(this.dockScenarioExplorer);
			this.panelContainer3.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
			this.panelContainer3.FloatSize = new System.Drawing.Size(214, 420);
			this.panelContainer3.FloatVertical = true;
			this.panelContainer3.ID = new System.Guid("0cc38a69-cbb8-4386-94ef-867392e2087c");
			this.panelContainer3.Location = new System.Drawing.Point(715, 49);
			this.panelContainer3.Name = "panelContainer3";
			this.panelContainer3.Size = new System.Drawing.Size(223, 685);
			this.panelContainer3.Tabbed = true;
			this.panelContainer3.Text = "panelContainer3";
			// 
			// dockProjectExplorer
			// 
			this.dockProjectExplorer.Controls.Add(this.controlContainer2);
			this.dockProjectExplorer.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
			this.dockProjectExplorer.ID = new System.Guid("564c526e-f96e-4c6f-b67e-6c68f329c668");
			this.dockProjectExplorer.Location = new System.Drawing.Point(3, 25);
			this.dockProjectExplorer.Name = "dockProjectExplorer";
			this.dockProjectExplorer.Size = new System.Drawing.Size(217, 635);
			this.dockProjectExplorer.Text = "Project Explorer";
			// 
			// controlContainer2
			// 
			this.controlContainer2.Controls.Add(this.projectExplorer);
			this.controlContainer2.Location = new System.Drawing.Point(0, 0);
			this.controlContainer2.Name = "controlContainer2";
			this.controlContainer2.Size = new System.Drawing.Size(217, 635);
			this.controlContainer2.TabIndex = 0;
			// 
			// projectExplorer
			// 
			this.projectExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.projectExplorer.Location = new System.Drawing.Point(0, 0);
			this.projectExplorer.Name = "projectExplorer";
			this.projectExplorer.Project = null;
			this.projectExplorer.Size = new System.Drawing.Size(217, 635);
			this.projectExplorer.TabIndex = 0;
			// 
			// dockTagExplorer
			// 
			this.dockTagExplorer.Controls.Add(this.dockPanel4_Container);
			this.dockTagExplorer.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
			this.dockTagExplorer.ID = new System.Guid("d43d2b25-de40-491b-9b1d-205a05da6d76");
			this.dockTagExplorer.Location = new System.Drawing.Point(3, 25);
			this.dockTagExplorer.Name = "dockTagExplorer";
			this.dockTagExplorer.Size = new System.Drawing.Size(217, 635);
			this.dockTagExplorer.Text = "Tag Explorer";
			// 
			// dockPanel4_Container
			// 
			this.dockPanel4_Container.Controls.Add(this.tagLibraryExplorer1);
			this.dockPanel4_Container.Location = new System.Drawing.Point(0, 0);
			this.dockPanel4_Container.Name = "dockPanel4_Container";
			this.dockPanel4_Container.Size = new System.Drawing.Size(217, 635);
			this.dockPanel4_Container.TabIndex = 0;
			// 
			// tagLibraryExplorer1
			// 
			this.tagLibraryExplorer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tagLibraryExplorer1.Location = new System.Drawing.Point(0, 0);
			//this.tagLibraryExplorer1.View = Prometheus.Controls.TagLibraryExplorer.ViewMode.Merged;
			this.tagLibraryExplorer1.Name = "tagLibraryExplorer1";
			this.tagLibraryExplorer1.Size = new System.Drawing.Size(217, 635);
			this.tagLibraryExplorer1.TabIndex = 9;
			this.tagLibraryExplorer1.TagLibrary = null;
			// 
			// dockScenarioExplorer
			// 
			this.dockScenarioExplorer.Controls.Add(this.dockPanel1_Container);
			this.dockScenarioExplorer.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
			this.dockScenarioExplorer.ID = new System.Guid("941d05e7-4bdc-49e7-8597-478c14c6872b");
			this.dockScenarioExplorer.Location = new System.Drawing.Point(3, 25);
			this.dockScenarioExplorer.Name = "dockScenarioExplorer";
			this.dockScenarioExplorer.Size = new System.Drawing.Size(217, 635);
			this.dockScenarioExplorer.Text = "Scenario Explorer";
			// 
			// dockPanel1_Container
			// 
			this.dockPanel1_Container.Controls.Add(this.scenarioExplorer1);
			this.dockPanel1_Container.Location = new System.Drawing.Point(0, 0);
			this.dockPanel1_Container.Name = "dockPanel1_Container";
			this.dockPanel1_Container.Size = new System.Drawing.Size(217, 635);
			this.dockPanel1_Container.TabIndex = 0;
			// 
			// scenarioExplorer1
			// 
			this.scenarioExplorer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scenarioExplorer1.Location = new System.Drawing.Point(0, 0);
			this.scenarioExplorer1.Name = "scenarioExplorer1";
			this.scenarioExplorer1.Size = new System.Drawing.Size(217, 635);
			this.scenarioExplorer1.TabIndex = 0;
			// 
			// panelContainer1
			// 
			this.panelContainer1.ActiveChild = this.dockOutput;
			this.panelContainer1.Controls.Add(this.dockOutput);
			this.panelContainer1.Controls.Add(this.dockTaskList);
			this.panelContainer1.Controls.Add(this.dockHelp);
			this.panelContainer1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
			this.panelContainer1.FloatSize = new System.Drawing.Size(730, 128);
			this.panelContainer1.FloatVertical = true;
			this.panelContainer1.ID = new System.Guid("9fbfe1ea-efd4-47b2-8fb4-d1a8ef774b0f");
			this.panelContainer1.Location = new System.Drawing.Point(0, 596);
			this.panelContainer1.Name = "panelContainer1";
			this.panelContainer1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.panelContainer1.Size = new System.Drawing.Size(715, 138);
			this.panelContainer1.Tabbed = true;
			this.panelContainer1.TabsScroll = true;
			this.panelContainer1.Text = "panelContainer1";
			// 
			// dockOutput
			// 
			this.dockOutput.Controls.Add(this.dockPanel2_Container);
			this.dockOutput.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
			this.dockOutput.ID = new System.Guid("bc1d24df-2e8a-49fd-87ba-3760cb0c99b5");
			this.dockOutput.Location = new System.Drawing.Point(3, 25);
			this.dockOutput.Name = "dockOutput";
			this.dockOutput.Size = new System.Drawing.Size(709, 88);
			this.dockOutput.Text = "Output";
			// 
			// dockPanel2_Container
			// 
			this.dockPanel2_Container.Controls.Add(this.rtbLog);
			this.dockPanel2_Container.Location = new System.Drawing.Point(0, 0);
			this.dockPanel2_Container.Name = "dockPanel2_Container";
			this.dockPanel2_Container.Size = new System.Drawing.Size(709, 88);
			this.dockPanel2_Container.TabIndex = 0;
			// 
			// rtbLog
			// 
			this.rtbLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbLog.Location = new System.Drawing.Point(0, 0);
			this.rtbLog.Name = "rtbLog";
			this.rtbLog.Size = new System.Drawing.Size(709, 88);
			this.rtbLog.TabIndex = 9;
			this.rtbLog.Text = "";
			// 
			// dockTaskList
			// 
			this.dockTaskList.Controls.Add(this.dockPanel3_Container);
			this.dockTaskList.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
			this.dockTaskList.ID = new System.Guid("4bdd468c-be0b-4d01-9772-e8736c12351e");
			this.dockTaskList.Location = new System.Drawing.Point(3, 25);
			this.dockTaskList.Name = "dockTaskList";
			this.dockTaskList.Size = new System.Drawing.Size(709, 88);
			this.dockTaskList.Text = "Task List";
			this.dockTaskList.Resize += new System.EventHandler(this.dockTaskList_Resize);
			// 
			// dockPanel3_Container
			// 
			this.dockPanel3_Container.Location = new System.Drawing.Point(0, 0);
			this.dockPanel3_Container.Name = "dockPanel3_Container";
			this.dockPanel3_Container.Size = new System.Drawing.Size(709, 88);
			this.dockPanel3_Container.TabIndex = 0;
			// 
			// dockHelp
			// 
			this.dockHelp.Controls.Add(this.controlContainer1);
			this.dockHelp.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
			this.dockHelp.ID = new System.Guid("e10ef49c-30e7-4f02-9cca-6710187adcc3");
			this.dockHelp.Location = new System.Drawing.Point(3, 25);
			this.dockHelp.Name = "dockHelp";
			this.dockHelp.Size = new System.Drawing.Size(709, 88);
			this.dockHelp.Text = "Help";
			// 
			// controlContainer1
			// 
			this.controlContainer1.Location = new System.Drawing.Point(0, 0);
			this.controlContainer1.Name = "controlContainer1";
			this.controlContainer1.Size = new System.Drawing.Size(709, 88);
			this.controlContainer1.TabIndex = 0;
			// 
			// panelContainer2
			// 
			this.panelContainer2.Controls.Add(this.dockRenderWindow);
			this.panelContainer2.Controls.Add(this.dockRenderControls);
			this.panelContainer2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
			this.panelContainer2.FloatSize = new System.Drawing.Size(476, 99);
			this.panelContainer2.ID = new System.Guid("1f539881-7e35-4515-9677-66ca689053b3");
			this.panelContainer2.Location = new System.Drawing.Point(0, 49);
			this.panelContainer2.Name = "panelContainer2";
			this.panelContainer2.Size = new System.Drawing.Size(509, 547);
			this.panelContainer2.Text = "panelContainer2";
			// 
			// dockRenderWindow
			// 
			this.dockRenderWindow.Controls.Add(this.projectExplorer_Container);
			this.dockRenderWindow.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
			this.dockRenderWindow.ID = new System.Guid("fdea4897-a67e-4db2-a789-15adb7645635");
			this.dockRenderWindow.Location = new System.Drawing.Point(0, 0);
			this.dockRenderWindow.Name = "dockRenderWindow";
			this.dockRenderWindow.Size = new System.Drawing.Size(509, 440);
			this.dockRenderWindow.Text = "Render Window";
			// 
			// projectExplorer_Container
			// 
			this.projectExplorer_Container.Controls.Add(this.MdxControl);
			this.projectExplorer_Container.Location = new System.Drawing.Point(3, 25);
			this.projectExplorer_Container.Name = "projectExplorer_Container";
			this.projectExplorer_Container.Size = new System.Drawing.Size(503, 412);
			this.projectExplorer_Container.TabIndex = 0;
			// 
			// MdxControl
			// 
			this.MdxControl.BackColor = System.Drawing.Color.Black;
			this.MdxControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MdxControl.Location = new System.Drawing.Point(0, 0);
			this.MdxControl.Name = "MdxControl";
			this.barManager1.SetPopupContextMenu(this.MdxControl, this.popupMenuRenderWin);
			this.MdxControl.Size = new System.Drawing.Size(503, 412);
			this.MdxControl.TabIndex = 7;
			this.MdxControl.TabStop = false;
			// 
			// dockRenderControls
			// 
			this.dockRenderControls.Controls.Add(this.dockPanel5_Container);
			this.dockRenderControls.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
			this.dockRenderControls.FloatSize = new System.Drawing.Size(476, 99);
			this.dockRenderControls.FloatVertical = true;
			this.dockRenderControls.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.dockRenderControls.ID = new System.Guid("2f4a39ad-8b56-4956-a796-c238e1e12a6c");
			this.dockRenderControls.Location = new System.Drawing.Point(0, 440);
			this.dockRenderControls.Name = "dockRenderControls";
			this.dockRenderControls.Options.ShowMaximizeButton = false;
			this.dockRenderControls.Size = new System.Drawing.Size(509, 107);
			this.dockRenderControls.Text = "Render Controls";
			this.dockRenderControls.Resize += new System.EventHandler(this.dockRenderControls_Resize);
			// 
			// dockPanel5_Container
			// 
			this.dockPanel5_Container.Location = new System.Drawing.Point(3, 25);
			this.dockPanel5_Container.Name = "dockPanel5_Container";
			this.dockPanel5_Container.Size = new System.Drawing.Size(503, 79);
			this.dockPanel5_Container.TabIndex = 0;
			// 
			// barSubItem4
			// 
			this.barSubItem4.Id = 32;
			this.barSubItem4.Name = "barSubItem4";
			// 
			// barSubItem6
			// 
			this.barSubItem6.Caption = "&Tools";
			this.barSubItem6.Id = 5;
			this.barSubItem6.Name = "barSubItem6";
			// 
			// barSubItem10
			// 
			this.barSubItem10.Caption = "&Add Existing";
			this.barSubItem10.Id = 23;
			this.barSubItem10.Name = "barSubItem10";
			// 
			// barSubItem11
			// 
			this.barSubItem11.Caption = "Create &New";
			this.barSubItem11.Id = 24;
			this.barSubItem11.Name = "barSubItem11";
			// 
			// barButtonItem11
			// 
			this.barButtonItem11.Caption = "Set as &Startup Project";
			this.barButtonItem11.Id = 25;
			this.barButtonItem11.Name = "barButtonItem11";
			// 
			// barButtonItem12
			// 
			this.barButtonItem12.Caption = "Project &Dependencies";
			this.barButtonItem12.Id = 26;
			this.barButtonItem12.Name = "barButtonItem12";
			// 
			// barButtonItem13
			// 
			this.barButtonItem13.Caption = "&Close Project";
			this.barButtonItem13.Id = 27;
			this.barButtonItem13.Name = "barButtonItem13";
			// 
			// barButtonItem14
			// 
			this.barButtonItem14.Caption = "barButtonItem14";
			this.barButtonItem14.Id = 30;
			this.barButtonItem14.Name = "barButtonItem14";
			// 
			// bsiProject
			// 
			this.bsiProject.Caption = "&Project";
			this.bsiProject.Id = 33;
			this.bsiProject.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
																																														new DevExpress.XtraBars.LinkPersistInfo(this.bsiProject_AddExisting),
																																														new DevExpress.XtraBars.LinkPersistInfo(this.bsiProject_CreateNew),
																																														new DevExpress.XtraBars.LinkPersistInfo(this.bsiProject_SetAsStartupProject, true),
																																														new DevExpress.XtraBars.LinkPersistInfo(this.bsiProject_ProjectDependencies),
																																														new DevExpress.XtraBars.LinkPersistInfo(this.bsiProject_CloseProject, true),
																																														new DevExpress.XtraBars.LinkPersistInfo(this.bsiProject_Properties, true)});
			this.bsiProject.Name = "bsiProject";
			// 
			// bsiProject_AddExisting
			// 
			this.bsiProject_AddExisting.Caption = "&Add Existing";
			this.bsiProject_AddExisting.Glyph = ((System.Drawing.Image)(resources.GetObject("bsiProject_AddExisting.Glyph")));
			this.bsiProject_AddExisting.Id = 57;
			this.bsiProject_AddExisting.Name = "bsiProject_AddExisting";
			// 
			// bsiProject_CreateNew
			// 
			this.bsiProject_CreateNew.Caption = "&Create New";
			this.bsiProject_CreateNew.Glyph = ((System.Drawing.Image)(resources.GetObject("bsiProject_CreateNew.Glyph")));
			this.bsiProject_CreateNew.Id = 58;
			this.bsiProject_CreateNew.Name = "bsiProject_CreateNew";
			// 
			// bsiProject_SetAsStartupProject
			// 
			this.bsiProject_SetAsStartupProject.Caption = "Set as &Startup Project";
			this.bsiProject_SetAsStartupProject.Id = 59;
			this.bsiProject_SetAsStartupProject.Name = "bsiProject_SetAsStartupProject";
			// 
			// bsiProject_ProjectDependencies
			// 
			this.bsiProject_ProjectDependencies.Caption = "Project &Dependencies";
			this.bsiProject_ProjectDependencies.Id = 60;
			this.bsiProject_ProjectDependencies.Name = "bsiProject_ProjectDependencies";
			// 
			// bsiProject_CloseProject
			// 
			this.bsiProject_CloseProject.Caption = "&Close Project";
			this.bsiProject_CloseProject.Id = 61;
			this.bsiProject_CloseProject.Name = "bsiProject_CloseProject";
			// 
			// bsiProject_Properties
			// 
			this.bsiProject_Properties.Caption = "&Properties";
			this.bsiProject_Properties.Id = 62;
			this.bsiProject_Properties.Name = "bsiProject_Properties";
			// 
			// bsiFile_DecompileAMapFile
			// 
			this.bsiFile_DecompileAMapFile.Caption = "Add to Tag &Library...";
			this.bsiFile_DecompileAMapFile.Glyph = ((System.Drawing.Image)(resources.GetObject("bsiFile_DecompileAMapFile.Glyph")));
			this.bsiFile_DecompileAMapFile.Id = 69;
			this.bsiFile_DecompileAMapFile.Name = "bsiFile_DecompileAMapFile";
			this.bsiFile_DecompileAMapFile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bsiFile_DecompileAMapFile_ItemClick);
			// 
			// barButtonItem1
			// 
			this.barButtonItem1.Id = 109;
			this.barButtonItem1.Name = "barButtonItem1";
			// 
			// barButtonItem2
			// 
			this.barButtonItem2.Caption = "test";
			this.barButtonItem2.Id = 98;
			this.barButtonItem2.Name = "barButtonItem2";
			// 
			// barButtonItem3
			// 
			this.barButtonItem3.Caption = "Render Lightmap";
			this.barButtonItem3.Id = 104;
			this.barButtonItem3.Name = "barButtonItem3";
			// 
			// barButtonItem4
			// 
			this.barButtonItem4.Caption = "barButtonItem4";
			this.barButtonItem4.Id = 112;
			this.barButtonItem4.Name = "barButtonItem4";
			// 
			// repositoryItemTextEdit1
			// 
			this.repositoryItemTextEdit1.AutoHeight = false;
			this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
			// 
			// repositoryItemTextEdit2
			// 
			this.repositoryItemTextEdit2.AutoHeight = false;
			this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
			// 
			// repositoryItemComboBox1
			// 
			this.repositoryItemComboBox1.AutoHeight = false;
			this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
																																																				 new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
			// 
			// popupMenuRenderWin
			// 
			this.popupMenuRenderWin.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
																																																		new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2)});
			this.popupMenuRenderWin.Manager = this.barManager1;
			this.popupMenuRenderWin.Name = "popupMenuRenderWin";
			this.popupMenuRenderWin.BeforePopup += new System.EventHandler(this.popupMenuRenderWin_BeforePopup);
			// 
			// barLinkContainerItem1
			// 
			this.barLinkContainerItem1.Caption = "barLinkContainerItem1";
			this.barLinkContainerItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("barLinkContainerItem1.Glyph")));
			this.barLinkContainerItem1.Id = 10;
			this.barLinkContainerItem1.Name = "barLinkContainerItem1";
			// 
			// tabDocuments
			// 
			this.tabDocuments.Controls.Add(this.tabStartPage);
			this.tabDocuments.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabDocuments.HeaderAutoFill = DevExpress.Utils.DefaultBoolean.False;
			this.tabDocuments.HeaderButtons = DevExpress.XtraTab.TabButtons.Close;
			this.tabDocuments.HeaderButtonsShowMode = DevExpress.XtraTab.TabButtonShowMode.WhenNeeded;
			this.tabDocuments.Location = new System.Drawing.Point(509, 49);
			this.tabDocuments.MultiLine = DevExpress.Utils.DefaultBoolean.False;
			this.tabDocuments.Name = "tabDocuments";
			this.tabDocuments.SelectedTabPage = this.tabStartPage;
			this.tabDocuments.Size = new System.Drawing.Size(206, 547);
			this.tabDocuments.TabIndex = 5;
			this.tabDocuments.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
																																								 this.tabStartPage});
			this.tabDocuments.CloseButtonClick += new System.EventHandler(this.tabDocuments_CloseButtonClick);
			// 
			// tabStartPage
			// 
			this.tabStartPage.Name = "tabStartPage";
			this.tabStartPage.Size = new System.Drawing.Size(197, 517);
			this.tabStartPage.Text = "Start Page";
			// 
			// timerRenderControls
			// 
			this.timerRenderControls.Enabled = true;
			this.timerRenderControls.SynchronizingObject = this;
			this.timerRenderControls.Elapsed += new System.Timers.ElapsedEventHandler(this.timerRenderControls_Elapsed);
			// 
			// barListItem1
			// 
			this.barListItem1.Caption = "barListItem1";
			this.barListItem1.Id = 120;
			this.barListItem1.Name = "barListItem1";
			// 
			// barButtonItem5
			// 
			this.barButtonItem5.Caption = "Xbox Map Build test";
			this.barButtonItem5.Id = 121;
			this.barButtonItem5.Name = "barButtonItem5";
			this.barButtonItem5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem5_ItemClick);
			// 
			// PrometheusGUI
			// 
			this.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Appearance.Options.UseFont = true;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(938, 756);
			this.Controls.Add(this.tabDocuments);
			this.Controls.Add(this.panelContainer2);
			this.Controls.Add(this.panelContainer1);
			this.Controls.Add(this.panelContainer3);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.Name = "PrometheusGUI";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Prometheus";
			this.Load += new System.EventHandler(this.PrometheusGUI_Load);
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
			this.panelContainer3.ResumeLayout(false);
			this.dockProjectExplorer.ResumeLayout(false);
			this.controlContainer2.ResumeLayout(false);
			this.dockTagExplorer.ResumeLayout(false);
			this.dockPanel4_Container.ResumeLayout(false);
			this.dockScenarioExplorer.ResumeLayout(false);
			this.dockPanel1_Container.ResumeLayout(false);
			this.panelContainer1.ResumeLayout(false);
			this.dockOutput.ResumeLayout(false);
			this.dockPanel2_Container.ResumeLayout(false);
			this.dockTaskList.ResumeLayout(false);
			this.dockHelp.ResumeLayout(false);
			this.panelContainer2.ResumeLayout(false);
			this.dockRenderWindow.ResumeLayout(false);
			this.projectExplorer_Container.ResumeLayout(false);
			this.dockRenderControls.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.popupMenuRenderWin)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tabDocuments)).EndInit();
			this.tabDocuments.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.timerRenderControls)).EndInit();
			this.ResumeLayout(false);

		}
    #endregion

    #region Viper's Map Building Test Code
    /*
    private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
    {
      TagFileName scenario_tag = ProjectManager.ScenarioTagFileName;

      IndexBuilder IndexBuild = new IndexBuilder();
      //FileInfo TagFile_info = new FileInfo(scenario_tag.RelativePath);
      string BasFolder = Application.StartupPath + @"\Extracted Tags\";
      string StructFolder = Application.StartupPath + @"\Tag Structures\PcHalo\";
      //string MapsFolder = Application.StartupPath + @"\Maps\";
      if (true)
      {
        //Create the index table
        //lbCurTag.Text = "Building Index Table....";
        Application.DoEvents();
        string[] IndexList = IndexBuild.BuildIndex(scenario_tag.RelativePath,BasFolder,StructFolder);
        HaloPCMAP.HaloPC NewMap = new HaloPCMAP.HaloPC();
        NewMap.PCIndexHeader.Create((uint)IndexList.Length);
        NewMap.PCIndexHeader.IndexMagic = 0x40440028;
        uint StringTableSize = 0;
        //Fixing index items
        ushort gt = 0xE174;
        //lbCurTag.Text = "Building Index Table Items....";
        Application.DoEvents();

        for(int sc = 0;sc < IndexList.Length;sc++)
        {
          //FileInfo TagFile2_info = new FileInfo(BasFolder + IndexList[sc]);
          //FileStream TagFile2;
          //TagFile2 = TagFile2_info.Open(FileMode.Open,FileAccess.Read);
          //byte[] tmp = new byte[0x40]; 
          //TagFile2.Read(tmp,0x00,tmp.Length);
          //TagFile2.Close();

          TagFileName tfnTAG = new TagFileName(IndexList[sc],MapfileVersion.HALOPC);
          TagBase tbTAG = new TagBase();
          tbTAG.LoadTagBuffer(tfnTAG);

          //uint TagClass1 = BitConverter.ToUInt32(tmp,0x24);
          //uint TagClass2 = BitConverter.ToUInt32(tmp,0x28);
          //uint TagClass3 = BitConverter.ToUInt32(tmp,0x2C);
          NewMap.PCIndexHeader.IndexItems[sc].Create();
          NewMap.PCIndexHeader.IndexItems[sc].TagClass1 = SwapUInt(tbTAG.Header.TagClass0);// TagClass1;
          NewMap.PCIndexHeader.IndexItems[sc].TagClass2 = SwapUInt(tbTAG.Header.TagClass1);// TagClass2;
          NewMap.PCIndexHeader.IndexItems[sc].TagClass3 = SwapUInt(tbTAG.Header.TagClass2);// TagClass3;
          NewMap.PCIndexHeader.IndexItems[sc].IndexID1 = (ushort)sc;
          NewMap.PCIndexHeader.IndexItems[sc].IndexID2 = gt;
          NewMap.PCIndexHeader.IndexItems[sc].NameString = IndexList[sc];
          if (tbTAG.Header.TagClass0 == 0x70736273 || tbTAG.Header.TagClass0 == 0x73627370)
          {
            NewMap.PCIndexHeader.IndexItems[sc].MetaSize = IndexBuild.BSPSizes[0];
          }
          else
          {
            NewMap.PCIndexHeader.IndexItems[sc].MetaSize = 0;
          }
          if(tbTAG.Header.TagClass0 == 0x7274656d || tbTAG.Header.TagClass0 == 0x6d657472)//  6d657472)
          {
            gt += 2;
          }
          else
          {
            gt += 1;
          }
          string[] RemoveExt = IndexList[sc].Split(new char[]{'.'},256);
          NewMap.PCIndexHeader.IndexItems[sc].String = new byte[RemoveExt[0].Length + 1];
          HaloPCMAP.StringToByteArray(ref NewMap.PCIndexHeader.IndexItems[sc].String,RemoveExt[0]);
          StringTableSize += (uint)(IndexList[sc].Length + 1);
          tbTAG = null;
        }
        //Creating the temp files for map building
        FileInfo VertsFile_info = new FileInfo(Application.StartupPath + "TempVerts.Map");
        FileStream VertsFile;
        VertsFile = VertsFile_info.Open(FileMode.Create,FileAccess.ReadWrite);

        FileInfo IndicesFile_info = new FileInfo(Application.StartupPath + "TempIndices.Map");
        FileStream IndicesFile;
        IndicesFile = IndicesFile_info.Open(FileMode.Create,FileAccess.ReadWrite);

        FileInfo MetaFile_info = new FileInfo(Application.StartupPath + "TempMeta.map");
        FileStream MetaFile;
        MetaFile = MetaFile_info.Open(FileMode.Create,FileAccess.ReadWrite);

        FileInfo BSPFile_info = new FileInfo(Application.StartupPath + "TempBSP.map");
        FileStream BSPFile;
        BSPFile = BSPFile_info.Open(FileMode.Create,FileAccess.ReadWrite);

        //lbCurTag.Text = "Writeing Index Table....";
        Application.DoEvents();

        //writing the index table the temp files
        for(int sc = 0;sc < NewMap.PCIndexHeader.IndexItems.Length;sc++)
        {
          NewMap.PCIndexHeader.IndexItems[sc].Write(ref MetaFile);
        }
        //Rewriting index items to temp file to fix string offsets.
        //lbCurTag.Text = "Rewriteing Index Table....";
        Application.DoEvents();

        for(int sc = 0;sc < NewMap.PCIndexHeader.IndexItems.Length;sc++)
        {
          NewMap.PCIndexHeader.IndexItems[sc].StringOffset = (uint)MetaFile.Position + NewMap.PCIndexHeader.IndexMagic;
          MetaFile.Write(NewMap.PCIndexHeader.IndexItems[sc].String,0,NewMap.PCIndexHeader.IndexItems[sc].String.Length);
        }
        //save MetaFile position to return for meta writing.
        long TmpPosSave = MetaFile.Position;
        //lbCurTag.Text = "Writing Index Table with Srtring Offsets....";
        Application.DoEvents();

        for(int sc = 0;sc < NewMap.PCIndexHeader.IndexItems.Length;sc++)
        {
          MetaFile.Seek(NewMap.PCIndexHeader.IndexItems[sc].Position,SeekOrigin.Begin);
          NewMap.PCIndexHeader.IndexItems[sc].Write(ref MetaFile);
        }
        //restore position for meta writing
        MetaFile.Seek(TmpPosSave,SeekOrigin.Begin);
				
        //Add info to the Meta info struct 
        PCMeta.StructInfo Info = new PCMeta.StructInfo();
        Info.MapMagic = NewMap.PCIndexHeader.IndexMagic;
        Info.TagMagic = NewMap.PCIndexHeader.IndexMagic;
        Info.StructurePath = StructFolder;
        Info.TagsPath = BasFolder;
        Info.Items = NewMap.PCIndexHeader.IndexItems;
        //Starts processing meta and writing it to the meta file
        //lbCurTag.Text = "Adding Tags to map....";
        Application.DoEvents();

        for(int sc = 0;sc < NewMap.PCIndexHeader.IndexItems.Length;sc++)
        {
          if (NewMap.PCIndexHeader.IndexItems[sc].TagClass1 != 0x70736273) //   70736273)
          {
            PCMeta Meta = new PCMeta();
            //FileInfo Tag_info = new FileInfo(BasFolder + IndexList[sc]);
            //FileStream Tag;
            //Tag = Tag_info.Open(FileMode.Open,FileAccess.Read);
            //byte[] head = new byte[0x40];
            //Tag.Read(head,0,head.Length);
            TagFileName tfnTAG = new TagFileName(IndexList[sc],MapfileVersion.HALOPC);
            TagBase tbTAG = new TagBase();
            tbTAG.LoadTagBuffer(tfnTAG);

            //string Class = Meta.GetTagClass(head,0x24);
            //MAG sr = new MAG(StructFolder + Class.Trim() + ".mag");
            //sr.Seek(1);
            MTSFReader mr = new MTSFReader();
            mr.MTSFRead();

            TSFReader STSF = new TSFReader();

            //SwapInt(TagClasses,0);
            STSF.TSF(ref mr, tbTAG.Header.TagClass0);

						

            HaloPCMAP.FixIntPosition(ref MetaFile);
            Info.CurrentOffset = (uint)MetaFile.Position;
            NewMap.PCIndexHeader.IndexItems[sc].MetaOffset = (uint)(MetaFile.Position + NewMap.PCIndexHeader.IndexMagic);

            Meta.DoProcessMeta(ref STSF,tbTAG.Stream,ref MetaFile,ref VertsFile,ref IndicesFile,ref BSPFile,1,IndexList,Info);

            tbTAG = null;
            //Tag.Flush();
            //Tag.Close();
          }
        }
        //Create a new map header
        //lbCurTag.Text = "Creating New Map....";
        Application.DoEvents();

        NewMap.PCHeader.Create();
        string[] tmpName = scenario_tag.RelativePath.Split(new char[]{'\\'},256);
        tmpName = tmpName[tmpName.Length - 1].Split(new char[]{'.'},256);
        NewMap.PCHeader.MapName = tmpName[0];//otf.FileName.Split(new char[]{'/'},256   //"Cool";
        //NewMap.PCHeader.Write(ref HeadFile);
        //Rewrite indextable of meta offsets
        //lbCurTag.Text = "Fixing Meta Offsets in Index Table....";
        Application.DoEvents();

        TmpPosSave = MetaFile.Position;
        for(int sc = 0;sc < NewMap.PCIndexHeader.IndexItems.Length;sc++)
        {
          MetaFile.Seek(NewMap.PCIndexHeader.IndexItems[sc].Position,SeekOrigin.Begin);
          NewMap.PCIndexHeader.IndexItems[sc].Write(ref MetaFile);
        }
        MetaFile.Seek(TmpPosSave,SeekOrigin.Begin);
				
        //Create the new map file to begin construction of the new map.
        FileInfo Map_info = new FileInfo(Application.StartupPath + NewMap.PCHeader.MapName +".Map");
        FileStream Map;
        Map = Map_info.Open(FileMode.Create,FileAccess.ReadWrite);

        //lbCurTag.Text = "Copying Data to new map file....";
        Application.DoEvents();

        //Write the header to the new map file
        NewMap.PCHeader.Write(ref Map);
        //Flush the tmp BSPFile to get FileSize.
        BSPFile.Flush();
        uint BSPFileSize = (uint)BSPFile.Length;
        //Seek to the begining of the tmp bsp file
        BSPFile.Seek(0,SeekOrigin.Begin);
        //Copy the Tmp bsp file to the new map file
        for(int bc = 0;bc < BSPFileSize;bc+=4)
        {
          byte[] copy = new byte[4];
          BSPFile.Read(copy,0,copy.Length);
          Map.Write(copy,0,copy.Length);
        }
        //Flush the Temp verts file to get File size;
        VertsFile.Flush();
        uint VertsFileSize = (uint)VertsFile.Length;
        //Put Current Map Position in the indexheader, this sets the position of the verts in the map.
        NewMap.PCIndexHeader.VertsOffset = (uint)Map.Position;
        //seek to begining of the Verts file for coping to the new map file
        VertsFile.Seek(0,SeekOrigin.Begin);
        //Copy the verts to the map file
        for(int bc = 0;bc < VertsFileSize;bc+=4)
        {
          byte[] copy = new byte[4];
          VertsFile.Read(copy,0,copy.Length);
          Map.Write(copy,0,copy.Length);
        }
        //Flush Indeices file to get size
        IndicesFile.Flush();
        uint IndicesFileSize = (uint)IndicesFile.Length;
        //Put the Scnr index ID in the IndexHeader, you can move the scnr any were as long as you set this to the currect ID.
        NewMap.PCIndexHeader.ScnrID = (uint)((NewMap.PCIndexHeader.IndexItems[0].IndexID2 << 16) + NewMap.PCIndexHeader.IndexItems[0].IndexID1);
        //ajust the IndicesOffset with verts offset and map position
        NewMap.PCIndexHeader.IndicesOffset = (uint)Map.Position - NewMap.PCIndexHeader.VertsOffset;
        NewMap.PCIndexHeader.ModelAreaSize = VertsFileSize + IndicesFileSize;
        //Seek to begining of IndicesFile for coping
        IndicesFile.Seek(0,SeekOrigin.Begin);
        //Copy Indices to new map file
        for(int bc = 0;bc < IndicesFileSize;bc+=4)
        {
          byte[] copy = new byte[4];
          IndicesFile.Read(copy,0,copy.Length);
          Map.Write(copy,0,copy.Length);
        }
        //put the Current map position in the header so we know where the index header will be
        NewMap.PCHeader.IndexOffset = (uint)Map.Position;
        //Now we write the Index header to the map.
        NewMap.PCIndexHeader.Write(ref Map);
				
        //Flush the temp meta file so we can get the size.
        MetaFile.Flush();
        uint MetaFileSize = (uint)MetaFile.Length;
        //seek to the begining of the temp meta file for coping to the new map file
        MetaFile.Seek(0,SeekOrigin.Begin);
        //Copy the meta file to the new map file.
        for(int bc = 0;bc < MetaFileSize;bc+=4)
        {
          byte[] copy = new byte[4];
          MetaFile.Read(copy,0,copy.Length);
          Map.Write(copy,0,copy.Length);
        }
        //Now we fix the map version map size and rewrite it to the map.
        Map.Flush();
        NewMap.PCHeader.MapVersion = 0x07;
        NewMap.PCHeader.MapSize = (uint)Map.Length;
        NewMap.PCHeader.MetaSize = MetaFileSize + 0x28;
        Map.Seek(NewMap.PCHeader.Position,SeekOrigin.Begin);
        NewMap.PCHeader.Write(ref Map);
			

        //Now close the files and delete the temp files.
        VertsFile.Close();
        IndicesFile.Close();
        MetaFile.Flush();
        MetaFile.Close();
        BSPFile.Close();
        Map.Close();
        //FileStream TagFile;
        //TagFile = Tag
      }
    }
    private void SwapInt(byte[] Array,uint Offset)
    {
      byte st;
      st = Array[Offset + 3];
      Array[Offset + 3] = Array[Offset + 0];
      Array[Offset + 0] = st;
      st = Array[Offset + 2];
      Array[Offset + 2] = Array[Offset + 1];
      Array[Offset + 1] = st;
    }
    private uint SwapUInt(uint Value)
    {
      byte[] tmp = BitConverter.GetBytes(Value);
      SwapInt(tmp,0);
      return BitConverter.ToUInt32(tmp,0);
    }
    */
    #endregion

    private void barCheckItem1_CheckedChanged(object sender, ItemClickEventArgs e)
    {
      BarCheckItem i = (e.Item as BarCheckItem);
      MdxRender.ShowFPS = i.Checked;
    }

    private void bsiView_Other_Fog_CheckedChanged(object sender, ItemClickEventArgs e)
    {
      BarCheckItem i = (e.Item as BarCheckItem);
      MdxRender.FogEnabled = i.Checked;
    }

    private void contextRenderWinMenuItem_Click(object sender, ItemClickEventArgs e)
    {
      if(e.Item.Name.IndexOf("Add...") != -1)
      {
        //todo: Tag Browse Dialog
        TagBrowserDialog tbd = new TagBrowserDialog(ProjectManager.Version);
        if(tbd.ShowDialog() == DialogResult.OK)
        {
          ProjectManager.PerformContextAction(e.Item.Name, tbd.SelectedTag);
        }
      }
      else if(e.Item.Name.IndexOf("Duplicate Object") != -1)
      {
      }
      else if(e.Item.Name.IndexOf("View Object") != -1)
      {
      }
      else if(e.Item.Name.IndexOf("Edit Tag") != -1)
      {
      }
      else if(e.Item.Name.IndexOf("Snap To Normal") != -1)
      {
      }
      else if(e.Item.Name.IndexOf("Reset Orientation") != -1)
      {
        ProjectManager.MapSpawns.ResetSelectionOrientation();
      }
      else if(e.Item.Name.IndexOf("Help") != -1)
      {
      }
      else
      {
        ProjectManager.PerformContextAction(e.Item.Name, null);
      }
      Trace.WriteLine(e.Item.Name + " clicked.");
    }

    private void popupMenuRenderWin_BeforePopup(object sender, EventArgs e)
    {
      BarItem item = null;
      //BarItem[] menu_items;

      string[] MenuStrings = ProjectManager.GetContextMenuStrings();

      //clear all items from the popup menu

      if(MenuStrings != null)
      {
        this.popupMenuRenderWin.ClearLinks();
        //menu_items = new BarItem[MenuStrings.Length];

        for(int s=0; s<MenuStrings.Length; s++)
        {
          int submenu_delimiter = MenuStrings[s].IndexOf('.');
          if(MenuStrings[s] != "")
          {
            if((s < 3)&&(submenu_delimiter != -1))
            {
              //recent items
              string SubMenuItemName;
              int m = MenuStrings[s].LastIndexOf('\\');

              if(m == -1)
                SubMenuItemName = MenuStrings[s].Substring(submenu_delimiter+1, MenuStrings[s].Length - submenu_delimiter-1);
              else
                SubMenuItemName = MenuStrings[s].Substring(m+1, MenuStrings[s].Length - m-1);

              item = new BarButtonItem();
              item.Name = MenuStrings[s];
              item.Caption = SubMenuItemName;
              item.ItemClick += new ItemClickEventHandler(contextRenderWinMenuItem_Click);
              this.popupMenuRenderWin.AddItem(item);
            }
            else if(submenu_delimiter == -1)  //Is this a top level menu item?
            {
              item = new BarButtonItem();
              item.Name = MenuStrings[s];
              item.Caption = MenuStrings[s];
              item.ItemClick += new ItemClickEventHandler(contextRenderWinMenuItem_Click);
              this.popupMenuRenderWin.AddItem(item);

              Bitmap bmp = GetPopupMenuImageIndex(MenuStrings[s]);
              if(bmp != null)
                item.Glyph = bmp;
            }
            else //Create a submenu and its subitem
            {
              string SubMenuItemName;
              string SubMenuName = MenuStrings[s].Substring(0, submenu_delimiter);
              int m = MenuStrings[s].LastIndexOf('\\');
              if(m == -1)
                SubMenuItemName = MenuStrings[s].Substring(submenu_delimiter+1, MenuStrings[s].Length - submenu_delimiter-1);
              else
                SubMenuItemName = MenuStrings[s].Substring(m+1, MenuStrings[s].Length - m-1);

              BarSubItem submenu = null;

              //check to see if submenu already exists
              for(int i=0; i<popupMenuRenderWin.ItemLinks.Count; i++)
              {
                if(popupMenuRenderWin.ItemLinks[i].Item is BarSubItem)
                {
                  if(popupMenuRenderWin.ItemLinks[i].Item.Name == SubMenuName)
                  {
                    submenu = (BarSubItem)popupMenuRenderWin.ItemLinks[i].Item;
                    break;
                  }
                }
              }

              if(submenu == null)
              {
                submenu = new BarSubItem();
                submenu.Name = SubMenuName;
                submenu.Caption = SubMenuName;
                this.popupMenuRenderWin.AddItem(submenu);
              }

              item = new BarButtonItem();
              item.Name = MenuStrings[s];
              item.Caption = SubMenuItemName;
              if(SubMenuItemName == "Add...")
              {
                Bitmap bmp = GetPopupMenuImageIndex(SubMenuItemName);
                if(bmp != null)
                  item.Glyph = bmp;
              }

              item.ItemClick += new ItemClickEventHandler(contextRenderWinMenuItem_Click);
              submenu.AddItem(item);
            }
          }
        }
      }
    }
    private Bitmap GetPopupMenuImageIndex(string name)
    {
      Bitmap bm = null;
      switch(name)
      {
        case "Duplicate Object":
          bm = m_PopupImages[3];
          break;
        
        case "Edit Tag":
          bm = m_PopupImages[0];
          break;

        case "Help":
          bm = m_PopupImages[1];
          break;

        case "Add...":
          bm = m_PopupImages[4];
          break;
      }

      return(bm);
    }

    private void dockTaskList_Resize(object sender, EventArgs e)
    {
      m_TaskListControl.Size = dockTaskList.ClientSize;
    }

    #region Render Control Panel
    private void tagLibraryExplorer1_PreviewActivated(PreviewActivatedEventArgs e)
    {
      if ((e.PreviewMode == PreviewMode.Model) || (e.PreviewMode == PreviewMode.Animation))
      {
        DisableMapRenderControlMode();
      }
      else
      {
        EnableMapRenderControlMode();
      }
    }
    private void EnableMapRenderControlMode()
    {
      dockRenderControls.Text = "Map Render Info & Controls";
      m_ModelRenderCompact.Hide();
      m_MapRenderCompact.Show();
    }
    private void DisableMapRenderControlMode()
    {
      dockRenderControls.Text = "Model Render Info & Controls";
      m_ModelRenderCompact.UpdateControls();
      m_ModelRenderCompact.Show();
      m_MapRenderCompact.Hide();
    }

    private void dockRenderControls_Resize(object sender, EventArgs e)
    {
      m_MapRenderCompact.Size = dockRenderControls.ClientSize;
      m_ModelRenderCompact.Size = dockRenderControls.ClientSize;
    }

    private void timerRenderControls_Elapsed(object sender, ElapsedEventArgs e)
    {
      this.m_MapRenderCompact.UpdateContent();
      this.m_ModelRenderCompact.UpdateContent();
    }
    #endregion

    private void bsiHelp_HaloDevOnline_ItemClick(object sender, ItemClickEventArgs e)
    {
      Process.Start("http://www.halodev.org");
    }

    private void bsiFile_Open_Project_ItemClick(object sender, ItemClickEventArgs e)
    {
      OpenFileDialog ofd = new OpenFileDialog();
      ofd.InitialDirectory = Application.StartupPath;
      ofd.Filter = "Prometheus Project (*.pmproj)|*.pmproj";
      if(ofd.ShowDialog() == DialogResult.OK)
      {
        ProjectManager.OpenProject(ofd.FileName);
        EnableMapRenderControlMode();
        MdxRender.PreviewManager.Mode = PreviewMode.ProjectMode;
        this.projectExplorer.Project = ProjectManager.ActiveProject;
      }
    }

    private void bsiFile_SaveEverything_ItemClick(object sender, ItemClickEventArgs e)
    {
      ProjectManager.SaveProject();
    }

    private void barButtonTestTagBrowser_ItemClick(object sender, ItemClickEventArgs e)
    {
      TagBrowserDialog dlg = new TagBrowserDialog(MapfileVersion.HALOPC);

      if (dlg.ShowDialog() == DialogResult.Cancel) return;
      MessageBox.Show(String.Format("Selected tag:\r\n{0}\r\nFrom:\r\n{1}",
        dlg.SelectedTag.RelativePath,
        dlg.SelectedTag.Source.ToString()));
    }

    private void barButtonToolsOptions_ItemClick(object sender, ItemClickEventArgs e)
    {
      OptionsDialog dlg = new OptionsDialog();
      dlg.ShowDialog();
    }

    private void bsiPersonalTestingForms_MonoxideC_ItemClick(object sender, ItemClickEventArgs e)
    {
      MonoxideC form = new MonoxideC();
      form.Show();
    }

    private void tabDocuments_CloseButtonClick(object sender, EventArgs e)
    {
      if (tabDocuments.SelectedTabPage == null) return;
      XtraTabPage page = tabDocuments.SelectedTabPage;
      if (page != this.tabStartPage)
      {
        // TODO: Provide a standardized way of getting rid of documents (DocumentManager)
        // That way, if something needs to be saved, etc, then it can have a chance to do so
        // before it is just closed.
        tabDocuments.TabPages.Remove(page);
      }
    }
    // MB This?
    public interface IDocument
    {
      bool IsDirty { get; }
      void Save();
    }

    private void TabPages_CollectionChanged(object sender, CollectionChangeEventArgs e)
    {
      // There's no way right now to have the main render window fill the remaining
      // client window (and we can't count on it being there anyway) so there's no
      // need to hide the tab control even when it is empty.
      //      if (e.Action == CollectionChangeAction.Add)
      //      {
      //        tabDocuments.Show();
      //      }
      //      if (e.Action == CollectionChangeAction.Remove)
      //      {
      //        if (tabDocuments.TabPages.Count == 0)
      //        {
      //          tabDocuments.Hide();
      //        }
      //      }
    }

    #region Script Editor
    XtraTabPage scriptView;
    Editor scriptEditor;
//    private OpenFileDialog ScriptOpenDialog;
//    private SaveFileDialog ScriptSaveDialog;

    private void barButtonItemTestScript_ItemClick(object sender, ItemClickEventArgs e)
    {
      if(scriptEditor == null)
      {
        scriptEditor = new Editor();
        scriptEditor.Dock = DockStyle.Fill;
      }

      if(scriptView == null)
      {
        scriptView = new XtraTabPage();
        scriptView.Controls.Add(scriptEditor);
        tabDocuments.Controls.Add(scriptView);
        tabDocuments.TabPages.Add(scriptView);
      }
      else
        scriptEditor.Edit.Clear();
    }
    #endregion

    private void bsiEdit_Undo_ItemClick(object sender, ItemClickEventArgs e)
    {
      XtraTabPage selectedPage = this.tabDocuments.SelectedTabPage;
      if (selectedPage == null) return;
      if (selectedPage.Controls.Count < 1) return;
      if (selectedPage.Controls[0] is ISupportsUndoRedo)
      {
        (selectedPage.Controls[0] as ISupportsUndoRedo).Undo();
      }
    }

    private void bsiEdit_Redo_ItemClick(object sender, ItemClickEventArgs e)
    {
      XtraTabPage selectedPage = this.tabDocuments.SelectedTabPage;
      if (selectedPage == null) return;
      if (selectedPage.Controls.Count < 1) return;
      if (selectedPage.Controls[0] is ISupportsUndoRedo)
      {
        (selectedPage.Controls[0] as ISupportsUndoRedo).Redo();
      }
    }

    private void barButtonItemGrenDebugger_ItemClick(object sender, ItemClickEventArgs e)
    {
      GrenDebugger dlg = new GrenDebugger();
      dlg.Show();
    }

    private void barButtonItemBuildMap_ItemClick(object sender, ItemClickEventArgs e)
    {
      ProjectManager.BuildMap();
    }

    private void barButtonItemRunRadiosity_ItemClick(object sender, ItemClickEventArgs e)
    {
      LightMapDiaglog ro = new LightMapDiaglog();
      ro.Show();
    }

    private void barButtonItemBuildPrefab_ItemClick(object sender, ItemClickEventArgs e)
    {
      PrefabPackager dlg = new PrefabPackager();
      dlg.ShowDialog();
    }

    private void barButtonItemDecompileMapfile_ItemClick(object sender, ItemClickEventArgs e)
    {
      Trace.WriteLine("File->Open->DecompileMapfile", "info");
      RenderEngine.Pause = true;
      
      // Browse for .map file
      OpenFileDialog ofd = new OpenFileDialog();
      ofd.Filter = "Halo Mapfile (*.map)|*.map";
      if(ofd.ShowDialog() == DialogResult.OK) 
      {
        // Load up the mapfile in the decompiler
        Trace.WriteLine("Selected '"+ofd.FileName+"' for decompilation","info");
        if (!File.Exists(ofd.FileName))
          throw new PrometheusException("Could not initialize map decompile - File not found: " + ofd.FileName, true);

        using( new EnableThemingInScope( true ) ) 
        {
          DecompileNavigator dn = new DecompileNavigator(ofd.FileName);

          dn.CreateControl();
          dn.ShowDialog();
        }
        
        //update the tag list
        this.tagLibraryExplorer1.TagLibrary = this.tagLibraryExplorer1.TagLibrary;

        RenderEngine.Pause = false;
      }
      else
      {
        Trace.WriteLine("Cancelled decompile mapfile.","info");
      }  
    }

    private void barButtonItemScanDependencies_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
      ProjectManager.ScanDependencies();
    }

    private void bsiFile_Import_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
    
    }

    private void barButtonItemNewBSP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
      OpenFileDialog dlg = new OpenFileDialog();
      dlg.Title = "Open JMS File";
      dlg.Filter = "Halo Blitzkrieg file (*.jms)|*.jms|All files (*.*)|*.*";

      if(dlg.ShowDialog() == DialogResult.OK)
      {
        //scan jms file for shaders
        //display error dialog (missing shaders)
        //set up files, fake out tool
        //launch tool
      }    
    }

    private void barButtonItemViperTester_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
      TagBrowserDialog dlg = new TagBrowserDialog(MapfileVersion.HALOPC);
      if(dlg.ShowDialog() == DialogResult.OK)
      {
        DependencyBuilder ib = new DependencyBuilder();
        ib.ProcessDependants(dlg.SelectedTag);

        string[] temp = DependencyBuilder.mapfileTagsIndex.RelativePathList;
        for(int i=0; i<temp.Length; i++)
          Trace.WriteLine("Tag Dependency: " + temp[i]);


        Trace.WriteLine("Broken Dependency List");
        string[] broken = DependencyBuilder.brokenDependencies.RelativePathList;
        string[] broken_parents = DependencyBuilder.brokenDependenciesParents.RelativePathList;
        for(int i=0; i<broken.Length; i++)
        {
          Trace.WriteLine("Broken Dependency: " + broken[i]);
        }

        for(int i=0; i<broken_parents.Length; i++)
        {
          if(broken_parents[i] != null)
            Trace.WriteLine("Broken Parent: " + broken_parents[i]);
          else
            Trace.WriteLine("Broken Parent: none");
        }
      }
    }

    private void barButtonItemCompressMapfile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
      OpenFileDialog dlg = new OpenFileDialog();
      dlg.Title = "Compress Map File";
      dlg.Filter = "Halo Map File (*.map)|*.map|All Files (*.*)|*.*";
      int cr;

      if(dlg.ShowDialog() == DialogResult.OK)
      {
        DialogResult result = MessageBox.Show("Rename output mapfile?", "Prometheus", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        
        if(result == DialogResult.No)
        {
          cr = Prometheus.Core.Utility.CompressMapFile(dlg.FileName, dlg.FileName);

          if(cr == 0)
            MessageBox.Show("Compression successful.");
          else
            MessageBox.Show("Error compressing mapfile.");
        }
        else if(result == DialogResult.Yes)
        {
          SaveFileDialog newfiledlg = new SaveFileDialog();
          newfiledlg.Title = "Save Decompressed File As";
          newfiledlg.Filter = "Halo Map File (*.map)|*.map|All Files (*.*)|*.*";

          if(newfiledlg.ShowDialog() == DialogResult.OK)
          {
            cr = Prometheus.Core.Utility.CompressMapFile(dlg.FileName, newfiledlg.FileName);

            if(cr == 0)
              MessageBox.Show("Compression successful.");
            else
              MessageBox.Show("Error compressing mapfile.");
          }
        }
      }
    }

    private void barButtonItemDecompressMapfile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
      OpenFileDialog dlg = new OpenFileDialog();
      dlg.Title = "Decompress Map File";
      dlg.Filter = "Halo Map File (*.map)|*.map|All Files (*.*)|*.*";
      int dr;
      if(dlg.ShowDialog() == DialogResult.OK)
      {
        DialogResult result = MessageBox.Show("Rename output mapfile?", "Prometheus", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        
        if(result == DialogResult.No)
        {
          dr = Prometheus.Core.Utility.DecompressMapFile(dlg.FileName, dlg.FileName);
          if(dr == 0)
            MessageBox.Show("Decompression successful.");
          else
            MessageBox.Show("Error decompressing mapfile.");
        }
        else if(result == DialogResult.Yes)
        {
          SaveFileDialog newfiledlg = new SaveFileDialog();
          newfiledlg.Title = "Save Decompressed File As";
          newfiledlg.Filter = "Halo Map File (*.map)|*.map|All Files (*.*)|*.*";

          if(newfiledlg.ShowDialog() == DialogResult.OK)
          {
            dr = Prometheus.Core.Utility.DecompressMapFile(dlg.FileName, newfiledlg.FileName);
            if(dr == 0)
              MessageBox.Show("Decompression successful.");
            else
              MessageBox.Show("Error decompressing mapfile.");
          }
        }
      }
    }

		private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			XBoxHaloMap.Build();
		
		}
  }
} 