using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Prometheus.Core;
using Prometheus.Core.Tags;
using Prometheus.Core.Render;
using TD.SandBar;
using ToolBar = System.Windows.Forms.ToolBar;

namespace Prometheus.Controls
{
	/// <summary>
	/// Summary description for TagExplorer.
	/// </summary>
	public class TagExplorer : UserControl
	{
    private ToolBar toolBar1;
    private ToolBarButton tbbLibView;
    private ToolBarButton tbbFileView;
    private ToolBarButton tbbSeperator1;
    private ToolBarButton tbbImport;
    private ToolBarButton tnnInformation;
    private ToolBarButton tbbHelp;
    private ImageList imageList;
    private Label label1;
    private Label label2;
    private FileTreeView TagTree;
    private IContainer components;
    private MenuBar menuBar1;
    private MenuBarItem mbiFolder;
    private MenuBarItem mbiFile;
    private MenuButtonItem mbiFolder_OpenAllForEdit;
    private MenuButtonItem mbiFolder_AddAllTags;
    private MenuButtonItem mbiFolder_DisplayFolderInfo;
    private MenuButtonItem mbiFolder_CheckDependencies;
    private MenuButtonItem mbiFolder_Help;
    private MenuButtonItem mbiFile_EditTagForProject;
    private MenuButtonItem mbiFile_AddTagToProject;
    private MenuButtonItem mbiFile_DisplayTagInfo;
    private MenuButtonItem mbiFile_CheckDependencies;
    private MenuButtonItem mbiFile_Help;
    private MenuButtonItem mbiFile_PreviewTag;

    private ITagLibrary m_ActiveArchive;
    private ComboBox cboGame;
    private TreeNode m_RootNode;
    public PrometheusGUI MainForm = null;

    public ITagLibrary Archive
    {
      get { return m_ActiveArchive; }
      set
      {
        m_ActiveArchive = value;
        if(m_ActiveArchive != null)
        {
          if(m_RootNode == null)
            m_RootNode = new TreeNode(m_ActiveArchive.Name);
          else
            m_RootNode.Text = m_ActiveArchive.Name;
        }

        LoadSet("\\");
      }
    }

//    public new bool DesignMode
//    {
//      get 
//      {
//        Control parent = Parent;
//        while(parent!=null)
//        {
//          if(parent.DesignMode) return true;
//          parent = parent.Parent;
//        }
//        return base.DesignMode;
//      }
//    }

    ///
    /// Indicates if the current view is being utilized in the VS.NET IDE or not.
    ///
    public new bool DesignMode
    {
      get
      {
        try
        {
          return (Process.GetCurrentProcess().ProcessName == "devenv");
        }
        catch
        {
          Console.WriteLine("Error getting current process - design mode will be assumed false.");
          return false;
        }
      }
    }

		public TagExplorer()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

      if (!DesignMode)
      {
        // Redo the ImageList with our custom method to fix it's alpha.
        string[] imageResources = new string[5] {
                                                  "Prometheus.Icons.Data_Coll._16.data.png",
                                                  "Prometheus.Icons.App_Basics._16.document.png",
                                                  "Prometheus.Icons.App_Basics._16.import2.png",
                                                  "Prometheus.Icons.App_Basics._16.information.png",
                                                  "Prometheus.Icons.App_Basics._16.help2.png"
                                                };
        Bitmap[] images = SharedControls.Utility.CreateImagesFromResourcePaths(imageResources);
        this.imageList = SharedControls.Utility.GenerateImageList(images);

        TagTree.CreateImageList(
          new string[4] {
                          "Prometheus.Icons.Data_Coll._16.data.png",
                          "Prometheus.Icons.App_Basics._16.folder.png",
                          "Prometheus.Icons.App_Basics._16.folder_closed.png",
                          "Prometheus.Icons.App_Basics._16.document.png"
                        });

        //Populate the Game select menu
        this.cboGame.Items.Add("Halo1 PC/CE");
        this.cboGame.Items.Add("Halo1 Xbox");
        this.cboGame.Items.Add("Halo2 Xbox");
        this.cboGame.SelectedIndex = 0;

        TagTree.ShowFiles = FileTreeView.ShowFilesBehavior.Self;
        TagTree.FileImageIndex = 3;
      
        // We're going to be loading the control's nodes on the fly, so
        // this will be handled on node's expand event.
        TagTree.AfterExpand += new TreeViewEventHandler(TagTree_AfterExpand);
        TagTree.ExpandAll();
      }
		}

    private void LoadSet(string BaseFolder)
    {
      if (m_ActiveArchive == null) return;

      //AbstractFolder[] folderArray;
      //AbstractFile[] fileArray;
      string[] files = Archive.GetFileList(BaseFolder);
      string[] folders = Archive.GetFolderList(BaseFolder);
      //Archive.GetFileList(BaseFolder, out folderArray, out fileArray);

      if(BaseFolder == "\\")
      {
        TagTree.Nodes.Clear();
        m_RootNode = new TreeNode(m_ActiveArchive.Name);
        TagTree.Nodes.Add(m_RootNode);
      }
      //TagTree.LoadFiles(folderArray, fileArray);
      TagTree.LoadFiles(folders);
      TagTree.LoadFiles(files);
    }

	  private void TagTree_AfterExpand(object sender, TreeViewEventArgs e)
	  {
      if (e.Node.Tag != null)
      {
        FileTreeView.FolderInformation fi = (FileTreeView.FolderInformation)e.Node.Tag;
        string currentPath = fi.FullPath;
        LoadSet(currentPath);
      }
	  }

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
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.components = new System.ComponentModel.Container();
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TagExplorer));
      this.toolBar1 = new System.Windows.Forms.ToolBar();
      this.tbbLibView = new System.Windows.Forms.ToolBarButton();
      this.tbbFileView = new System.Windows.Forms.ToolBarButton();
      this.tbbSeperator1 = new System.Windows.Forms.ToolBarButton();
      this.tbbImport = new System.Windows.Forms.ToolBarButton();
      this.tnnInformation = new System.Windows.Forms.ToolBarButton();
      this.tbbHelp = new System.Windows.Forms.ToolBarButton();
      this.imageList = new System.Windows.Forms.ImageList(this.components);
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.cboGame = new System.Windows.Forms.ComboBox();
      this.TagTree = new Prometheus.Controls.FileTreeView();
      this.menuBar1 = new TD.SandBar.MenuBar();
      this.mbiFolder = new TD.SandBar.MenuBarItem();
      this.mbiFolder_OpenAllForEdit = new TD.SandBar.MenuButtonItem();
      this.mbiFolder_AddAllTags = new TD.SandBar.MenuButtonItem();
      this.mbiFolder_DisplayFolderInfo = new TD.SandBar.MenuButtonItem();
      this.mbiFolder_CheckDependencies = new TD.SandBar.MenuButtonItem();
      this.mbiFolder_Help = new TD.SandBar.MenuButtonItem();
      this.mbiFile = new TD.SandBar.MenuBarItem();
      this.mbiFile_EditTagForProject = new TD.SandBar.MenuButtonItem();
      this.mbiFile_AddTagToProject = new TD.SandBar.MenuButtonItem();
      this.mbiFile_PreviewTag = new TD.SandBar.MenuButtonItem();
      this.mbiFile_DisplayTagInfo = new TD.SandBar.MenuButtonItem();
      this.mbiFile_CheckDependencies = new TD.SandBar.MenuButtonItem();
      this.mbiFile_Help = new TD.SandBar.MenuButtonItem();
      this.SuspendLayout();
      // 
      // toolBar1
      // 
      this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
      this.toolBar1.AutoSize = false;
      this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
                                                                                this.tbbLibView,
                                                                                this.tbbFileView,
                                                                                this.tbbSeperator1,
                                                                                this.tbbImport,
                                                                                this.tnnInformation,
                                                                                this.tbbHelp});
      this.toolBar1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolBar1.DropDownArrows = true;
      this.toolBar1.Enabled = false;
      this.toolBar1.ImageList = this.imageList;
      this.toolBar1.Location = new System.Drawing.Point(56, 32);
      this.toolBar1.Name = "toolBar1";
      this.toolBar1.ShowToolTips = true;
      this.toolBar1.Size = new System.Drawing.Size(124, 26);
      this.toolBar1.TabIndex = 0;
      // 
      // tbbLibView
      // 
      this.tbbLibView.ImageIndex = 0;
      this.tbbLibView.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
      // 
      // tbbFileView
      // 
      this.tbbFileView.ImageIndex = 1;
      this.tbbFileView.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
      // 
      // tbbSeperator1
      // 
      this.tbbSeperator1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
      // 
      // tbbImport
      // 
      this.tbbImport.ImageIndex = 2;
      // 
      // tnnInformation
      // 
      this.tnnInformation.ImageIndex = 3;
      // 
      // tbbHelp
      // 
      this.tbbHelp.ImageIndex = 4;
      // 
      // imageList
      // 
      this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
      this.imageList.ImageSize = new System.Drawing.Size(16, 16);
      this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
      this.imageList.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // label1
      // 
      this.label1.Enabled = false;
      this.label1.Location = new System.Drawing.Point(8, 40);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(40, 16);
      this.label1.TabIndex = 1;
      this.label1.Text = "View:";
      this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(7, 7);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(41, 16);
      this.label2.TabIndex = 2;
      this.label2.Text = "Game:";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // cboGame
      // 
      this.cboGame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboGame.Location = new System.Drawing.Point(56, 4);
      this.cboGame.Name = "cboGame";
      this.cboGame.Size = new System.Drawing.Size(128, 24);
      this.cboGame.TabIndex = 3;
      this.cboGame.SelectedIndexChanged += new System.EventHandler(this.cboGame_SelectedIndex);
      // 
      // TagTree
      // 
      this.TagTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.TagTree.ImageIndex = -1;
      this.TagTree.Location = new System.Drawing.Point(8, 64);
      this.TagTree.Name = "TagTree";
      this.menuBar1.SetSandBarMenu(this.TagTree, this.mbiFolder);
      this.TagTree.SelectedImageIndex = -1;
      this.TagTree.ShowFiles = Prometheus.Controls.FileTreeView.ShowFilesBehavior.Self;
      this.TagTree.Size = new System.Drawing.Size(176, 224);
      this.TagTree.TabIndex = 4;
      this.TagTree.DoubleClick += new System.EventHandler(this.TagTree_DoubleClick);
      this.TagTree.Click += new System.EventHandler(this.TagTree_Click);
      this.TagTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TagTree_AfterSelect);
      // 
      // menuBar1
      // 
      this.menuBar1.Guid = new System.Guid("5ea56852-1bdf-4f90-b53f-3aca9dcae076");
      this.menuBar1.Items.AddRange(new TD.SandBar.ToolbarItemBase[] {
                                                                      this.mbiFolder,
                                                                      this.mbiFile});
      this.menuBar1.Location = new System.Drawing.Point(0, 0);
      this.menuBar1.Name = "menuBar1";
      this.menuBar1.OwnerForm = null;
      this.menuBar1.Size = new System.Drawing.Size(192, 25);
      this.menuBar1.TabIndex = 5;
      this.menuBar1.Text = "menuBar1";
      this.menuBar1.Visible = false;
      // 
      // mbiFolder
      // 
      this.mbiFolder.Items.AddRange(new TD.SandBar.ToolbarItemBase[] {
                                                                       this.mbiFolder_OpenAllForEdit,
                                                                       this.mbiFolder_AddAllTags,
                                                                       this.mbiFolder_DisplayFolderInfo,
                                                                       this.mbiFolder_CheckDependencies,
                                                                       this.mbiFolder_Help});
      this.mbiFolder.Text = "Library Folder";
      // 
      // mbiFolder_OpenAllForEdit
      // 
      this.mbiFolder_OpenAllForEdit.Image = ((System.Drawing.Image)(resources.GetObject("mbiFolder_OpenAllForEdit.Image")));
      this.mbiFolder_OpenAllForEdit.Text = "Open All for &Edit";
      // 
      // mbiFolder_AddAllTags
      // 
      this.mbiFolder_AddAllTags.Image = ((System.Drawing.Image)(resources.GetObject("mbiFolder_AddAllTags.Image")));
      this.mbiFolder_AddAllTags.Text = "&Add All Tags";
      // 
      // mbiFolder_DisplayFolderInfo
      // 
      this.mbiFolder_DisplayFolderInfo.Image = ((System.Drawing.Image)(resources.GetObject("mbiFolder_DisplayFolderInfo.Image")));
      this.mbiFolder_DisplayFolderInfo.Text = "DIsplay Folder &Info";
      // 
      // mbiFolder_CheckDependencies
      // 
      this.mbiFolder_CheckDependencies.Image = ((System.Drawing.Image)(resources.GetObject("mbiFolder_CheckDependencies.Image")));
      this.mbiFolder_CheckDependencies.Text = "Check &Dependencies";
      // 
      // mbiFolder_Help
      // 
      this.mbiFolder_Help.BeginGroup = true;
      this.mbiFolder_Help.Image = ((System.Drawing.Image)(resources.GetObject("mbiFolder_Help.Image")));
      this.mbiFolder_Help.Text = "&Help";
      // 
      // mbiFile
      // 
      this.mbiFile.Items.AddRange(new TD.SandBar.ToolbarItemBase[] {
                                                                     this.mbiFile_EditTagForProject,
                                                                     this.mbiFile_AddTagToProject,
                                                                     this.mbiFile_PreviewTag,
                                                                     this.mbiFile_DisplayTagInfo,
                                                                     this.mbiFile_CheckDependencies,
                                                                     this.mbiFile_Help});
      this.mbiFile.Text = "Library File";
      // 
      // mbiFile_EditTagForProject
      // 
      this.mbiFile_EditTagForProject.Image = ((System.Drawing.Image)(resources.GetObject("mbiFile_EditTagForProject.Image")));
      this.mbiFile_EditTagForProject.Text = "Edit Tag for Project";
      this.mbiFile_EditTagForProject.Activate += new System.EventHandler(this.mbiFile_EditTagForProject_Activate);
      // 
      // mbiFile_AddTagToProject
      // 
      this.mbiFile_AddTagToProject.Image = ((System.Drawing.Image)(resources.GetObject("mbiFile_AddTagToProject.Image")));
      this.mbiFile_AddTagToProject.Text = "Add Tag to Project";
      this.mbiFile_AddTagToProject.Activate += new System.EventHandler(this.mbiFile_AddTagToProject_Activate);
      // 
      // mbiFile_PreviewTag
      // 
      this.mbiFile_PreviewTag.Image = ((System.Drawing.Image)(resources.GetObject("mbiFile_PreviewTag.Image")));
      this.mbiFile_PreviewTag.Text = "Preview Tag";
      this.mbiFile_PreviewTag.Activate += new System.EventHandler(this.mbiFile_PreviewTag_Activate);
      // 
      // mbiFile_DisplayTagInfo
      // 
      this.mbiFile_DisplayTagInfo.Image = ((System.Drawing.Image)(resources.GetObject("mbiFile_DisplayTagInfo.Image")));
      this.mbiFile_DisplayTagInfo.Text = "Display Tag Info";
      // 
      // mbiFile_CheckDependencies
      // 
      this.mbiFile_CheckDependencies.Image = ((System.Drawing.Image)(resources.GetObject("mbiFile_CheckDependencies.Image")));
      this.mbiFile_CheckDependencies.Text = "Check Dependencies";
      // 
      // mbiFile_Help
      // 
      this.mbiFile_Help.BeginGroup = true;
      this.mbiFile_Help.Image = ((System.Drawing.Image)(resources.GetObject("mbiFile_Help.Image")));
      this.mbiFile_Help.Text = "Help";
      // 
      // TagExplorer
      // 
      this.Controls.Add(this.menuBar1);
      this.Controls.Add(this.TagTree);
      this.Controls.Add(this.cboGame);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.toolBar1);
      this.Name = "TagExplorer";
      this.Size = new System.Drawing.Size(192, 296);
      this.ResumeLayout(false);

    }
		#endregion

    private void TagTree_Click(object sender, EventArgs e)
    {
    
    }

    private string BuildPath(TreeNode node)
    {
      string path = "";

      if(node != null)
      {
        if(node.Tag == null)
        {
          path = BuildPath(node.Parent) + "\\" + node.Text;
        }
        else
        {
          FileTreeView.FolderInformation fi = (FileTreeView.FolderInformation)node.Tag;
          path = fi.FullPath;
        }
      }

      return(path);
    }

    private void TagTree_AfterSelect(object sender, TreeViewEventArgs e)
    {
      if (TagTree.SelectedNode.Tag != null)
      {
        this.menuBar1.SetSandBarMenu(this.TagTree, this.mbiFolder);
      }
      else
      {
        UpdateMenu(TagTree.SelectedNode.Text, this.mbiFile);
        this.menuBar1.SetSandBarMenu(this.TagTree, this.mbiFile);
      }
    }

    private void UpdateMenu(string tag_name, MenuBarItem menu)
    {
      //Check to see if Preview should be enabled (not all tags can be previewed)
      bool bValidPreview = false;
      if(tag_name.IndexOf("Bitmap") != -1)bValidPreview = true;
      if(tag_name.IndexOf("GBXModel") != -1)bValidPreview = true;
      if(tag_name.IndexOf("Scenario_Structure_Bsp") != -1)bValidPreview = true;
      //if(tag_name.IndexOf("model_animations") != -1)bValidPreview = true;
      //if(tag_name.IndexOf("shader_environment") != -1)bValidPreview = true;
      //if(tag_name.IndexOf("shader_model") != -1)bValidPreview = true;
      //if(tag_name.IndexOf("shader_transparent_chicago") != -1)bValidPreview = true;
      //if(tag_name.IndexOf("sky") != -1)bValidPreview = true;

      mbiFile_PreviewTag.Enabled = bValidPreview;
    }

    private void mbiFile_EditTagForProject_Activate(object sender, EventArgs e)
    {
      string path = BuildPath(TagTree.SelectedNode);
      //this.m_ActiveArchive.ExtractFileToProject(OptionsManager.ActiveProjectPath, path);
      Trace.WriteLine("TagExplorer->EditTagForProject: " + path);

      //todo: launch tag editor
    }

    private void ActivatePreview()
    {
      string path = BuildPath(TagTree.SelectedNode);

      if(path.IndexOf('.') != -1)
      {
        Trace.WriteLine("TagExplorer->PreviewTag: " + path);
      
        // TODO: I think we need to look at this design involving passing the MapfileVersion.
        // It's confusing to me, and it doesn't seem like the best way to go.
        // NOTE: After thinking about it, we should probably get the version from the tag.
        // This would most likely be accomplished by calling a "GetGameType(filename)" on the
        // current ITagArchive.  I will add that method to the interface.
        MapfileVersion version = MapfileVersion.HALOPC;
        if (this.Archive.Name == "Halo Xbox") version = MapfileVersion.XHALO1;
        if (this.Archive.Name == "Halo 2 Xbox") version = MapfileVersion.XHALO2;
      
        TagFileName tfn = new TagFileName(path, version);

        // HACK: For now, until we can look at this, I'm just sending the MafileVersion based on the archive name.
        MdxRender.PreviewManager.PreviewTag(tfn);
      }
    }

    private void mbiFile_PreviewTag_Activate(object sender, EventArgs e)
    {
      ActivatePreview();
    }

    private void TagTree_DoubleClick(object sender, EventArgs e)
    {
      ActivatePreview();
      
      //check preview manager mode, set Main Window Render controls
//      if((MdxRender.PreviewManager.Mode == PreviewMode.Model)||
//        (MdxRender.PreviewManager.Mode == PreviewMode.Animation))
//        MainForm.EnableMapRenderControlMode(false);
//      else
//        MainForm.EnableMapRenderControlMode(true);
    }

    private void cboGame_SelectedIndex(object sender, EventArgs e)
    {
      switch(cboGame.SelectedIndex)
      {
        case 0:
          this.Archive = TagLibraryManager.HaloPC;
          break;
        case 1:
          this.Archive = TagLibraryManager.HaloXbox;
          break;
        case 2:
          this.Archive = TagLibraryManager.Halo2Xbox;
          break;
      }
    }

    private void mbiFile_AddTagToProject_Activate(object sender, EventArgs e)
    {
      string path = BuildPath(TagTree.SelectedNode);
      if(OptionsManager.ActiveProjectPath == "")
        MessageBox.Show("Cannot add file because no project is loaded.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      else
      {
        Archive.ExtractFile(path, OptionsManager.ActiveProjectPath + "tags" + path);
      }
    }
	}
}
