/* ---------------------------------------------------------------
 * Prometheus
 * Bungie Map-based Multi-Game Editing Kit
 * 2004-2005, Halo-Dev
 * ---------------------------------------------------------------
 * File        : TagLibraryExplorer.cs
 * Author      : MonoxideC
 * Co-Authors  : 
 * ---------------------------------------------------------------
 */

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using Prometheus.Core;
using Prometheus.Core.Tags;
using Prometheus.Core.Compiler;
using Prometheus.Core.Render;

namespace Prometheus.Controls
{
	/// <summary>
	/// Summary description for TagLibraryExplorer.
	/// </summary>
	public class TagLibraryExplorer : XtraUserControl
	{
    private BarManager barManager1;
    private BarDockControl barDockControlTop;
    private BarDockControl barDockControlBottom;
    private BarDockControl barDockControlLeft;
    private BarDockControl barDockControlRight;
    
    private PopupMenu popupArchiveFolder;
    private BarButtonItem barArchiveFolder_OpenAllForEdit;
    private BarButtonItem barArchiveFolder_AddAllTags;
    private BarButtonItem barArchiveFolder_DisplayFolderInfo;
    private BarButtonItem barArchiveFolder_CheckDependencies;
    private BarButtonItem barArchiveFolder_Help;   

    private PopupMenu popupArchiveFile;
    private PopupMenu popupMasterFolder;
    
    private PopupMenu popupMasterFile;
    private BarButtonItem barArchiveFile_EditTagForProject;
    private BarButtonItem barArchiveFile_AddTagToProject;
    private BarButtonItem barArchiveFile_DisplayTagInfo;
    private BarButtonItem barArchiveFile_CheckDependencies;
    private BarButtonItem barArchiveFile_Help;
    
    private Bar bar1;
    private Bar bar2;
    private BarStaticItem barStaticItem2;
    private BarEditItem barFilterComboBox;
    private RepositoryItemComboBox repositoryItemComboBox1;
    private BarButtonItem barButtonItem1;
    private BarButtonItem barInformation;
    private BarButtonItem barHelp;
    private BarSubItem barArchive;
    private BarCheckItem barArchive_ExtractedFilesOnly;
    private BarCheckItem barArchive_AllFiles_MergedView;
    private BarCheckItem barArchive_AllFiles_SplitView;
		
    /// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;
    private BarCheckItem barObjectView;
    private BarCheckItem barFileView;

    private int selectedIndex = 0;
    private BarButtonItem barMasterFolder_AddFolderToTagLibrary;
    private BarButtonItem barMasterFile_AddFileToTagLibrary;
    protected ITagLibrary tagLibrary;
    private ProgressPanel progressPanel;

	  Decompiler decompiler;
    public PrometheusGUI MainForm = null;

    private int totalFilesExtracted = 0;
    private int totalFilesToExtract = 0;

    public int totalMapsToExtractFrom = 0;
    public int totalMapsExtractedFrom = 0;
    private Panel panel1;
    private PopupMenu popupMasterRoot;
    private BarButtonItem barButtonItem2;
    private MultiSyncTreeView archiveTree;
    private MultiSyncTreeNode selectedNode = null;

    private LayoutMode viewLayout;

    private ViewMode view;

    private System.Windows.Forms.Panel panel3;
    private DevExpress.XtraEditors.SplitterControl splitterControl;
    private System.Windows.Forms.Panel panel2;
    private Prometheus.Controls.MultiSyncTreeView masterTree;

    DecompilerList[] lists = new DecompilerList[0];

    public ITagLibrary TagLibrary
    {
      get { return tagLibrary; }
      set { tagLibrary = value; }
    }

    /// <summary>
    /// Sets the current ViewLayout.
    /// </summary>
    public LayoutMode ViewLayout
    {
      set
      {
        this.viewLayout = value;
        switch (value)
        {
          case LayoutMode.Merged:
            this.SetMergedView();
            break;
          case LayoutMode.Split:
            this.SetSplitView();
            break;
          case LayoutMode.ExtractedOnly:
            this.SetExtractedOnlyView();
            break;
        }
      }
      get { return this.viewLayout; }
    }

    public ViewMode View
    {
      get { return view; }
      set
      {
        view = value;
        if (view == ViewMode.File)
        {
          SetFileMode();
        }
        if (view == ViewMode.Object)
        {
          SetObjectMode();
        }
      }
    }

	  private void SetObjectMode()
	  {
	    throw new NotImplementedException();
	  }

	  private void SetFileMode()
	  {
	    throw new NotImplementedException();
	  }

	  public TagLibraryExplorer()
		{
			// This call is required by the Windows.Forms Form Designer.
      InitializeComponent();

      //Populate the Game select menu
      RepositoryItemComboBox edit = repositoryItemComboBox1;
      edit.SelectedIndexChanged += new EventHandler(edit_SelectedIndexChanged);
      edit.Items.Add("Halo1 PC/CE");
      edit.Items.Add("Halo1 Xbox");
      edit.Items.Add("Halo2 Xbox");

      //this.View = ViewMode.Merged;
      archiveTree.AfterSelect += new TreeViewEventHandler(tree_AfterSelect);
      masterTree.AfterSelect += new TreeViewEventHandler(tree_AfterSelect);
      archiveTree.DoubleClick += new EventHandler(archiveTree_DoubleClick);
		}

	  private void tree_AfterSelect(object sender, TreeViewEventArgs e)
	  {
	    this.selectedNode = e.Node as MultiSyncTreeNode;
	  }

	  // TODO: Move these to a more appropriate location.
    NodeHelper archiveHelper = null;
    NodeHelper masterHelper = null;
    NodeHelper archiveObjectViewHelper = null;
    NodeHelper masterObjectViewHelper = null;

    MasterLibraryArchive masterArchive = null;

    /// <summary>
    /// Updates the archive sources when a new item in the combo box is selected.
    /// </summary>
    private void edit_SelectedIndexChanged(object sender, EventArgs e)
	  {
      ComboBoxEdit edit = (sender as ComboBoxEdit);
      this.selectedIndex = edit.SelectedIndex;
      BindArchive();
    }

	  private void BindArchive()
	  {
      ITagLibrary activeLibrary = null;
      XmlDocument masterList = null;
      if (this.selectedIndex == 0)
      {
        activeLibrary = TagLibraryManager.HaloPC;
        masterList = TagLibraryManager.HaloPCMasterTagList;
      }
      else if (this.selectedIndex == 1)
      {
        activeLibrary = TagLibraryManager.HaloXbox;
        masterList = TagLibraryManager.HaloPCMasterTagList;
      }
      else if (this.selectedIndex == 2)
      {
        activeLibrary = TagLibraryManager.Halo2Xbox;
        masterList = TagLibraryManager.HaloPCMasterTagList;
      }

      // Setup the new NodeHelpers.
      archiveHelper = new ArchiveLibraryNodeHelper("Tag Library", activeLibrary);
      archiveHelper.NodeDefinitions["container"].PopupMenu = this.popupArchiveFolder;
      archiveHelper.NodeDefinitions["object"].PopupMenu = this.popupArchiveFile;
      
      archiveObjectViewHelper = new ObjectViewNodeHelper("Tag Library Object View", 
        new ObjectViewTagLibrary("Object View", activeLibrary as ZipTagLibrary));
      archiveObjectViewHelper.OptionalRootNode = new MultiSyncTreeNode("Object View Demo");
      archiveObjectViewHelper.OptionalRootNode.NodeInformation = new NodeInformation(NodeType.Container);
      
      masterArchive = new MasterLibraryArchive(masterList, activeLibrary);
      masterHelper = new ArchiveLibraryNodeHelper("Master List", masterArchive);
      masterHelper.NodeDefinitions["container"].PopupMenu = this.popupMasterFolder;
      masterHelper.NodeDefinitions["object"].PopupMenu = this.popupMasterFile;
      masterHelper.FontStyle = FontStyle.Bold;
      
      // TODO: Move this code to the methods that setup the views, and
      // manually setup the default view here for now.
      if (this.viewLayout == LayoutMode.Merged) SetMergedView();
      if (this.viewLayout == LayoutMode.Split) SetSplitView();
      if (this.viewLayout == LayoutMode.ExtractedOnly) SetExtractedOnlyView();
	  }

	  public event PreviewActivatedEventHandler PreviewActivated;
    private void archiveTree_DoubleClick(object sender, EventArgs e)
    {
      ActivatePreview();
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
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TagLibraryExplorer));
      this.popupArchiveFolder = new DevExpress.XtraBars.PopupMenu();
      this.barArchiveFolder_OpenAllForEdit = new DevExpress.XtraBars.BarButtonItem();
      this.barArchiveFolder_AddAllTags = new DevExpress.XtraBars.BarButtonItem();
      this.barArchiveFolder_DisplayFolderInfo = new DevExpress.XtraBars.BarButtonItem();
      this.barArchiveFolder_CheckDependencies = new DevExpress.XtraBars.BarButtonItem();
      this.barArchiveFolder_Help = new DevExpress.XtraBars.BarButtonItem();
      this.barManager1 = new DevExpress.XtraBars.BarManager();
      this.bar1 = new DevExpress.XtraBars.Bar();
      this.barStaticItem2 = new DevExpress.XtraBars.BarStaticItem();
      this.barFilterComboBox = new DevExpress.XtraBars.BarEditItem();
      this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
      this.bar2 = new DevExpress.XtraBars.Bar();
      this.barArchive = new DevExpress.XtraBars.BarSubItem();
      this.barArchive_AllFiles_MergedView = new DevExpress.XtraBars.BarCheckItem();
      this.barArchive_AllFiles_SplitView = new DevExpress.XtraBars.BarCheckItem();
      this.barArchive_ExtractedFilesOnly = new DevExpress.XtraBars.BarCheckItem();
      this.barFileView = new DevExpress.XtraBars.BarCheckItem();
      this.barObjectView = new DevExpress.XtraBars.BarCheckItem();
      this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
      this.barInformation = new DevExpress.XtraBars.BarButtonItem();
      this.barHelp = new DevExpress.XtraBars.BarButtonItem();
      this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
      this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
      this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
      this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
      this.barArchiveFile_EditTagForProject = new DevExpress.XtraBars.BarButtonItem();
      this.barArchiveFile_AddTagToProject = new DevExpress.XtraBars.BarButtonItem();
      this.barArchiveFile_DisplayTagInfo = new DevExpress.XtraBars.BarButtonItem();
      this.barArchiveFile_CheckDependencies = new DevExpress.XtraBars.BarButtonItem();
      this.barArchiveFile_Help = new DevExpress.XtraBars.BarButtonItem();
      this.barMasterFolder_AddFolderToTagLibrary = new DevExpress.XtraBars.BarButtonItem();
      this.barMasterFile_AddFileToTagLibrary = new DevExpress.XtraBars.BarButtonItem();
      this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
      this.popupArchiveFile = new DevExpress.XtraBars.PopupMenu();
      this.popupMasterFolder = new DevExpress.XtraBars.PopupMenu();
      this.popupMasterFile = new DevExpress.XtraBars.PopupMenu();
      this.progressPanel = new Prometheus.Controls.ProgressPanel();
      this.panel1 = new System.Windows.Forms.Panel();
      this.archiveTree = new Prometheus.Controls.MultiSyncTreeView();
      this.popupMasterRoot = new DevExpress.XtraBars.PopupMenu();
      this.panel3 = new System.Windows.Forms.Panel();
      this.splitterControl = new DevExpress.XtraEditors.SplitterControl();
      this.panel2 = new System.Windows.Forms.Panel();
      this.masterTree = new Prometheus.Controls.MultiSyncTreeView();
      ((System.ComponentModel.ISupportInitialize)(this.popupArchiveFolder)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.popupArchiveFile)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.popupMasterFolder)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.popupMasterFile)).BeginInit();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.popupMasterRoot)).BeginInit();
      this.panel3.SuspendLayout();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // popupArchiveFolder
      // 
      this.popupArchiveFolder.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
                                                                                                    new DevExpress.XtraBars.LinkPersistInfo(this.barArchiveFolder_OpenAllForEdit),
                                                                                                    new DevExpress.XtraBars.LinkPersistInfo(this.barArchiveFolder_AddAllTags),
                                                                                                    new DevExpress.XtraBars.LinkPersistInfo(this.barArchiveFolder_DisplayFolderInfo),
                                                                                                    new DevExpress.XtraBars.LinkPersistInfo(this.barArchiveFolder_CheckDependencies),
                                                                                                    new DevExpress.XtraBars.LinkPersistInfo(this.barArchiveFolder_Help, true)});
      this.popupArchiveFolder.Manager = this.barManager1;
      this.popupArchiveFolder.Name = "popupArchiveFolder";
      // 
      // barArchiveFolder_OpenAllForEdit
      // 
      this.barArchiveFolder_OpenAllForEdit.Caption = "Open All for &Edit";
      this.barArchiveFolder_OpenAllForEdit.Glyph = ((System.Drawing.Image)(resources.GetObject("barArchiveFolder_OpenAllForEdit.Glyph")));
      this.barArchiveFolder_OpenAllForEdit.Id = 0;
      this.barArchiveFolder_OpenAllForEdit.Name = "barArchiveFolder_OpenAllForEdit";
      // 
      // barArchiveFolder_AddAllTags
      // 
      this.barArchiveFolder_AddAllTags.Caption = "&Add All Tags";
      this.barArchiveFolder_AddAllTags.Glyph = ((System.Drawing.Image)(resources.GetObject("barArchiveFolder_AddAllTags.Glyph")));
      this.barArchiveFolder_AddAllTags.Id = 1;
      this.barArchiveFolder_AddAllTags.Name = "barArchiveFolder_AddAllTags";
      // 
      // barArchiveFolder_DisplayFolderInfo
      // 
      this.barArchiveFolder_DisplayFolderInfo.Caption = "Display Folder &Info";
      this.barArchiveFolder_DisplayFolderInfo.Glyph = ((System.Drawing.Image)(resources.GetObject("barArchiveFolder_DisplayFolderInfo.Glyph")));
      this.barArchiveFolder_DisplayFolderInfo.Id = 2;
      this.barArchiveFolder_DisplayFolderInfo.Name = "barArchiveFolder_DisplayFolderInfo";
      // 
      // barArchiveFolder_CheckDependencies
      // 
      this.barArchiveFolder_CheckDependencies.Caption = "Check &Dependencies";
      this.barArchiveFolder_CheckDependencies.Glyph = ((System.Drawing.Image)(resources.GetObject("barArchiveFolder_CheckDependencies.Glyph")));
      this.barArchiveFolder_CheckDependencies.Id = 3;
      this.barArchiveFolder_CheckDependencies.Name = "barArchiveFolder_CheckDependencies";
      // 
      // barArchiveFolder_Help
      // 
      this.barArchiveFolder_Help.Caption = "&Help";
      this.barArchiveFolder_Help.Glyph = ((System.Drawing.Image)(resources.GetObject("barArchiveFolder_Help.Glyph")));
      this.barArchiveFolder_Help.Id = 9;
      this.barArchiveFolder_Help.Name = "barArchiveFolder_Help";
      // 
      // barManager1
      // 
      this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
                                                                     this.bar1,
                                                                     this.bar2});
      this.barManager1.DockControls.Add(this.barDockControlTop);
      this.barManager1.DockControls.Add(this.barDockControlBottom);
      this.barManager1.DockControls.Add(this.barDockControlLeft);
      this.barManager1.DockControls.Add(this.barDockControlRight);
      this.barManager1.Form = this;
      this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
                                                                          this.barArchiveFolder_OpenAllForEdit,
                                                                          this.barArchiveFolder_AddAllTags,
                                                                          this.barArchiveFolder_DisplayFolderInfo,
                                                                          this.barArchiveFolder_CheckDependencies,
                                                                          this.barArchiveFile_EditTagForProject,
                                                                          this.barArchiveFile_AddTagToProject,
                                                                          this.barArchiveFile_DisplayTagInfo,
                                                                          this.barArchiveFile_CheckDependencies,
                                                                          this.barArchiveFile_Help,
                                                                          this.barArchiveFolder_Help,
                                                                          this.barStaticItem2,
                                                                          this.barFilterComboBox,
                                                                          this.barButtonItem1,
                                                                          this.barInformation,
                                                                          this.barHelp,
                                                                          this.barArchive,
                                                                          this.barArchive_ExtractedFilesOnly,
                                                                          this.barArchive_AllFiles_MergedView,
                                                                          this.barArchive_AllFiles_SplitView,
                                                                          this.barObjectView,
                                                                          this.barFileView,
                                                                          this.barMasterFolder_AddFolderToTagLibrary,
                                                                          this.barMasterFile_AddFileToTagLibrary,
                                                                          this.barButtonItem2});
      this.barManager1.MaxItemId = 34;
      this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
                                                                                                         this.repositoryItemComboBox1});
      // 
      // bar1
      // 
      this.bar1.BarName = "Custom 1";
      this.bar1.DockCol = 0;
      this.bar1.DockRow = 1;
      this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
      this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
                                                                                      new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem2),
                                                                                      new DevExpress.XtraBars.LinkPersistInfo(this.barFilterComboBox)});
      this.bar1.OptionsBar.AllowQuickCustomization = false;
      this.bar1.OptionsBar.DisableClose = true;
      this.bar1.OptionsBar.DisableCustomization = true;
      this.bar1.OptionsBar.DrawDragBorder = false;
      this.bar1.OptionsBar.UseWholeRow = true;
      this.bar1.Text = "Custom 1";
      // 
      // barStaticItem2
      // 
      this.barStaticItem2.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
      this.barStaticItem2.Caption = "Filter:";
      this.barStaticItem2.Id = 11;
      this.barStaticItem2.Name = "barStaticItem2";
      this.barStaticItem2.TextAlignment = System.Drawing.StringAlignment.Near;
      // 
      // barFilterComboBox
      // 
      this.barFilterComboBox.AutoFillWidth = true;
      this.barFilterComboBox.Caption = "barEditItem1";
      this.barFilterComboBox.Edit = this.repositoryItemComboBox1;
      this.barFilterComboBox.Id = 12;
      this.barFilterComboBox.Name = "barFilterComboBox";
      // 
      // repositoryItemComboBox1
      // 
      this.repositoryItemComboBox1.AutoHeight = false;
      this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                         new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
      this.repositoryItemComboBox1.NullText = "Select a Library...";
      // 
      // bar2
      // 
      this.bar2.BarName = "Custom 2";
      this.bar2.DockCol = 0;
      this.bar2.DockRow = 2;
      this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
      this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
                                                                                      new DevExpress.XtraBars.LinkPersistInfo(this.barArchive),
                                                                                      new DevExpress.XtraBars.LinkPersistInfo(this.barFileView, true),
                                                                                      new DevExpress.XtraBars.LinkPersistInfo(this.barObjectView),
                                                                                      new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1, true),
                                                                                      new DevExpress.XtraBars.LinkPersistInfo(this.barInformation),
                                                                                      new DevExpress.XtraBars.LinkPersistInfo(this.barHelp)});
      this.bar2.OptionsBar.AllowQuickCustomization = false;
      this.bar2.OptionsBar.DisableClose = true;
      this.bar2.OptionsBar.DisableCustomization = true;
      this.bar2.OptionsBar.DrawDragBorder = false;
      this.bar2.OptionsBar.UseWholeRow = true;
      this.bar2.Text = "Custom 2";
      // 
      // barArchive
      // 
      this.barArchive.Caption = "View";
      this.barArchive.Id = 20;
      this.barArchive.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
                                                                                            new DevExpress.XtraBars.LinkPersistInfo(this.barArchive_AllFiles_MergedView),
                                                                                            new DevExpress.XtraBars.LinkPersistInfo(this.barArchive_AllFiles_SplitView),
                                                                                            new DevExpress.XtraBars.LinkPersistInfo(this.barArchive_ExtractedFilesOnly)});
      this.barArchive.Name = "barArchive";
      // 
      // barArchive_AllFiles_MergedView
      // 
      this.barArchive_AllFiles_MergedView.Caption = "&Merged View";
      this.barArchive_AllFiles_MergedView.Checked = true;
      this.barArchive_AllFiles_MergedView.GroupIndex = 1;
      this.barArchive_AllFiles_MergedView.Id = 26;
      this.barArchive_AllFiles_MergedView.Name = "barArchive_AllFiles_MergedView";
      this.barArchive_AllFiles_MergedView.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.barArchive_AllFiles_MergedView_CheckedChanged);
      // 
      // barArchive_AllFiles_SplitView
      // 
      this.barArchive_AllFiles_SplitView.Caption = "&Split View";
      this.barArchive_AllFiles_SplitView.GroupIndex = 1;
      this.barArchive_AllFiles_SplitView.Id = 28;
      this.barArchive_AllFiles_SplitView.Name = "barArchive_AllFiles_SplitView";
      this.barArchive_AllFiles_SplitView.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.barArchive_AllFiles_SplitView_CheckedChanged);
      // 
      // barArchive_ExtractedFilesOnly
      // 
      this.barArchive_ExtractedFilesOnly.Caption = "&Extracted Files Only";
      this.barArchive_ExtractedFilesOnly.GroupIndex = 1;
      this.barArchive_ExtractedFilesOnly.Id = 21;
      this.barArchive_ExtractedFilesOnly.Name = "barArchive_ExtractedFilesOnly";
      this.barArchive_ExtractedFilesOnly.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.barArchive_ExtractedFilesOnly_CheckedChanged);
      // 
      // barFileView
      // 
      this.barFileView.Caption = "File View";
      this.barFileView.Checked = true;
      this.barFileView.Glyph = ((System.Drawing.Image)(resources.GetObject("barFileView.Glyph")));
      this.barFileView.GroupIndex = 2;
      this.barFileView.Id = 30;
      this.barFileView.Name = "barFileView";
      this.barFileView.ItemPress += new DevExpress.XtraBars.ItemClickEventHandler(this.barFileView_ItemPress);
      // 
      // barObjectView
      // 
      this.barObjectView.Caption = "Object View";
      this.barObjectView.Glyph = ((System.Drawing.Image)(resources.GetObject("barObjectView.Glyph")));
      this.barObjectView.GroupIndex = 2;
      this.barObjectView.Id = 29;
      this.barObjectView.Name = "barObjectView";
      this.barObjectView.ItemPress += new DevExpress.XtraBars.ItemClickEventHandler(this.barObjectView_ItemPress);
      // 
      // barButtonItem1
      // 
      this.barButtonItem1.Caption = "Import";
      this.barButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.Glyph")));
      this.barButtonItem1.Id = 15;
      this.barButtonItem1.Name = "barButtonItem1";
      // 
      // barInformation
      // 
      this.barInformation.Caption = "Information";
      this.barInformation.Glyph = ((System.Drawing.Image)(resources.GetObject("barInformation.Glyph")));
      this.barInformation.Id = 16;
      this.barInformation.Name = "barInformation";
      // 
      // barHelp
      // 
      this.barHelp.Caption = "Help";
      this.barHelp.Glyph = ((System.Drawing.Image)(resources.GetObject("barHelp.Glyph")));
      this.barHelp.Id = 17;
      this.barHelp.Name = "barHelp";
      // 
      // barArchiveFile_EditTagForProject
      // 
      this.barArchiveFile_EditTagForProject.Caption = "&Edit Tag for Project";
      this.barArchiveFile_EditTagForProject.Glyph = ((System.Drawing.Image)(resources.GetObject("barArchiveFile_EditTagForProject.Glyph")));
      this.barArchiveFile_EditTagForProject.Id = 4;
      this.barArchiveFile_EditTagForProject.Name = "barArchiveFile_EditTagForProject";
      // 
      // barArchiveFile_AddTagToProject
      // 
      this.barArchiveFile_AddTagToProject.Caption = "&Add Tag to Project";
      this.barArchiveFile_AddTagToProject.Glyph = ((System.Drawing.Image)(resources.GetObject("barArchiveFile_AddTagToProject.Glyph")));
      this.barArchiveFile_AddTagToProject.Id = 5;
      this.barArchiveFile_AddTagToProject.Name = "barArchiveFile_AddTagToProject";
      // 
      // barArchiveFile_DisplayTagInfo
      // 
      this.barArchiveFile_DisplayTagInfo.Caption = "Display Tag &Info";
      this.barArchiveFile_DisplayTagInfo.Glyph = ((System.Drawing.Image)(resources.GetObject("barArchiveFile_DisplayTagInfo.Glyph")));
      this.barArchiveFile_DisplayTagInfo.Id = 6;
      this.barArchiveFile_DisplayTagInfo.Name = "barArchiveFile_DisplayTagInfo";
      // 
      // barArchiveFile_CheckDependencies
      // 
      this.barArchiveFile_CheckDependencies.Caption = "Check &Dependencies";
      this.barArchiveFile_CheckDependencies.Glyph = ((System.Drawing.Image)(resources.GetObject("barArchiveFile_CheckDependencies.Glyph")));
      this.barArchiveFile_CheckDependencies.Id = 7;
      this.barArchiveFile_CheckDependencies.Name = "barArchiveFile_CheckDependencies";
      // 
      // barArchiveFile_Help
      // 
      this.barArchiveFile_Help.Caption = "&Help";
      this.barArchiveFile_Help.Glyph = ((System.Drawing.Image)(resources.GetObject("barArchiveFile_Help.Glyph")));
      this.barArchiveFile_Help.Id = 8;
      this.barArchiveFile_Help.Name = "barArchiveFile_Help";
      // 
      // barMasterFolder_AddFolderToTagLibrary
      // 
      this.barMasterFolder_AddFolderToTagLibrary.Caption = "Add &Folder to Tag Library...";
      this.barMasterFolder_AddFolderToTagLibrary.Glyph = ((System.Drawing.Image)(resources.GetObject("barMasterFolder_AddFolderToTagLibrary.Glyph")));
      this.barMasterFolder_AddFolderToTagLibrary.Id = 31;
      this.barMasterFolder_AddFolderToTagLibrary.Name = "barMasterFolder_AddFolderToTagLibrary";
      this.barMasterFolder_AddFolderToTagLibrary.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barMasterFolder_AddFolderToTagLibrary_ItemClick);
      // 
      // barMasterFile_AddFileToTagLibrary
      // 
      this.barMasterFile_AddFileToTagLibrary.Caption = "Add F&ile to Tag Library";
      this.barMasterFile_AddFileToTagLibrary.Glyph = ((System.Drawing.Image)(resources.GetObject("barMasterFile_AddFileToTagLibrary.Glyph")));
      this.barMasterFile_AddFileToTagLibrary.Id = 32;
      this.barMasterFile_AddFileToTagLibrary.Name = "barMasterFile_AddFileToTagLibrary";
      this.barMasterFile_AddFileToTagLibrary.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barMasterFile_AddFileToTagLibrary_ItemClick);
      // 
      // barButtonItem2
      // 
      this.barButtonItem2.Caption = "&Build Entire Archive";
      this.barButtonItem2.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.Glyph")));
      this.barButtonItem2.Id = 33;
      this.barButtonItem2.Name = "barButtonItem2";
      this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
      // 
      // popupArchiveFile
      // 
      this.popupArchiveFile.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
                                                                                                  new DevExpress.XtraBars.LinkPersistInfo(this.barArchiveFile_EditTagForProject),
                                                                                                  new DevExpress.XtraBars.LinkPersistInfo(this.barArchiveFile_AddTagToProject),
                                                                                                  new DevExpress.XtraBars.LinkPersistInfo(this.barArchiveFile_DisplayTagInfo),
                                                                                                  new DevExpress.XtraBars.LinkPersistInfo(this.barArchiveFile_CheckDependencies),
                                                                                                  new DevExpress.XtraBars.LinkPersistInfo(this.barArchiveFile_Help, true)});
      this.popupArchiveFile.Manager = this.barManager1;
      this.popupArchiveFile.Name = "popupArchiveFile";
      // 
      // popupMasterFolder
      // 
      this.popupMasterFolder.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
                                                                                                   new DevExpress.XtraBars.LinkPersistInfo(this.barMasterFolder_AddFolderToTagLibrary)});
      this.popupMasterFolder.Manager = this.barManager1;
      this.popupMasterFolder.Name = "popupMasterFolder";
      // 
      // popupMasterFile
      // 
      this.popupMasterFile.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
                                                                                                 new DevExpress.XtraBars.LinkPersistInfo(this.barMasterFile_AddFileToTagLibrary)});
      this.popupMasterFile.Manager = this.barManager1;
      this.popupMasterFile.Name = "popupMasterFile";
      // 
      // progressPanel
      // 
      this.progressPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.progressPanel.Appearance.BackColor = System.Drawing.Color.Transparent;
      this.progressPanel.Appearance.Options.UseBackColor = true;
      this.progressPanel.Caption = "";
      this.progressPanel.Location = new System.Drawing.Point(104, 0);
      this.progressPanel.Name = "progressPanel";
      this.progressPanel.ProgressMaximum = 100;
      this.progressPanel.ProgressMinimum = 0;
      this.progressPanel.ProgressValue = 0;
      this.progressPanel.Size = new System.Drawing.Size(272, 544);
      this.progressPanel.TabIndex = 6;
      this.progressPanel.Visible = false;
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.Color.Transparent;
      this.panel1.Controls.Add(this.archiveTree);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.DockPadding.All = 10;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(272, 347);
      this.panel1.TabIndex = 7;
      // 
      // archiveTree
      // 
      this.archiveTree.Dock = System.Windows.Forms.DockStyle.Fill;
      this.archiveTree.ImageIndex = -1;
      this.archiveTree.Location = new System.Drawing.Point(10, 10);
      this.archiveTree.Name = "archiveTree";
      this.archiveTree.SelectedImageIndex = -1;
      this.archiveTree.ShowFiles = true;
      this.archiveTree.Size = new System.Drawing.Size(252, 327);
      this.archiveTree.TabIndex = 0;
      // 
      // popupMasterRoot
      // 
      this.popupMasterRoot.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
                                                                                                 new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2),
                                                                                                 new DevExpress.XtraBars.LinkPersistInfo(this.barMasterFolder_AddFolderToTagLibrary)});
      this.popupMasterRoot.Manager = this.barManager1;
      this.popupMasterRoot.Name = "popupMasterRoot";
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.panel1);
      this.panel3.Controls.Add(this.splitterControl);
      this.panel3.Controls.Add(this.panel2);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel3.Location = new System.Drawing.Point(0, 49);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(272, 495);
      this.panel3.TabIndex = 9;
      // 
      // splitterControl
      // 
      this.splitterControl.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.splitterControl.Location = new System.Drawing.Point(0, 347);
      this.splitterControl.Name = "splitterControl";
      this.splitterControl.Size = new System.Drawing.Size(272, 4);
      this.splitterControl.TabIndex = 9;
      this.splitterControl.TabStop = false;
      this.splitterControl.Visible = false;
      // 
      // panel2
      // 
      this.panel2.BackColor = System.Drawing.Color.Transparent;
      this.panel2.Controls.Add(this.masterTree);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel2.DockPadding.All = 10;
      this.panel2.Location = new System.Drawing.Point(0, 351);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(272, 144);
      this.panel2.TabIndex = 10;
      // 
      // masterTree
      // 
      this.masterTree.Dock = System.Windows.Forms.DockStyle.Fill;
      this.masterTree.ImageIndex = -1;
      this.masterTree.Location = new System.Drawing.Point(10, 10);
      this.masterTree.Name = "masterTree";
      this.masterTree.SelectedImageIndex = -1;
      this.masterTree.ShowFiles = true;
      this.masterTree.Size = new System.Drawing.Size(252, 124);
      this.masterTree.TabIndex = 0;
      // 
      // TagLibraryExplorer
      // 
      this.Controls.Add(this.progressPanel);
      this.Controls.Add(this.panel3);
      this.Controls.Add(this.barDockControlLeft);
      this.Controls.Add(this.barDockControlRight);
      this.Controls.Add(this.barDockControlBottom);
      this.Controls.Add(this.barDockControlTop);
      this.Name = "TagLibraryExplorer";
      this.Size = new System.Drawing.Size(272, 544);
      ((System.ComponentModel.ISupportInitialize)(this.popupArchiveFolder)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.popupArchiveFile)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.popupMasterFolder)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.popupMasterFile)).EndInit();
      this.panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.popupMasterRoot)).EndInit();
      this.panel3.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion

    private void ActivatePreview()
    {
      if (archiveTree.SelectedNode == null) return;
      if (this.selectedIndex == -1) return;
      MultiSyncTreeNode selectedNode = archiveTree.SelectedNode as MultiSyncTreeNode;
      if (selectedNode.NodeInformation.NodeType != NodeType.Object) return;

      // Ensure that the node is representing a tag in the real archive.
      //foreach (PathItem item in selectedNode.NodeInformation.PathItems)
      //{
      //  if (item.NodeHelper.Name == "Master List")
      //    return;
      //}

      int pathItemCount = selectedNode.NodeInformation.PathItems.Count;
      string path = selectedNode.NodeInformation.PathItems[pathItemCount-1].Path;

      if(path.IndexOf('.') != -1)
      {
        Trace.WriteLine("TagExplorer->PreviewTag: " + path);
      
        // TODO: I think we need to look at this design involving passing the MapfileVersion.
        // It's confusing to me, and it doesn't seem like the best way to go.
        // NOTE: After thinking about it, we should probably get the version from the tag.
        // This would most likely be accomplished by calling a "GetGameType(filename)" on the
        // current ITagArchive.  I will add that method to the interface.
        string archiveName = (string)repositoryItemComboBox1.Items[this.selectedIndex];
        MapfileVersion version = MapfileVersion.HALOPC;
        if (archiveName == "Halo1 Xbox") version = MapfileVersion.XHALO1;
        if (archiveName == "Halo2 Xbox") version = MapfileVersion.XHALO2;
      
        TagFileName tfn = new TagFileName(path, version);

        // HACK: For now, until we can look at this, I'm just sending the MapileVersion based on the archive name.
        MdxRender.PreviewManager.PreviewTag(tfn);

        if (PreviewActivated != null)
          PreviewActivated(new PreviewActivatedEventArgs(MdxRender.PreviewManager.Mode));
      }
    }

    #region Helper Methods
    protected void HideMasterArchive()
    {
      this.splitterControl.Hide();
      this.masterTree.Hide();
    }

    protected void ShowMasterArchive()
    {
      this.masterTree.Show();
      this.splitterControl.Show();
    }

    protected void SetMergedView()
    {
      HideMasterArchive();
      this.archiveTree.NodeHelpers.Clear();
      this.archiveTree.NodeHelpers.Add(masterHelper);
      this.archiveTree.NodeHelpers.Add(archiveHelper);
      this.archiveTree.NodeHelpers.Add(archiveObjectViewHelper);
      this.archiveTree.Initialize();
    }

    protected void SetSplitView()
    {
      ShowMasterArchive();
      this.masterTree.NodeHelpers.Clear();
      this.masterTree.NodeHelpers.Add(masterHelper);
      this.masterTree.Initialize();

      this.archiveTree.NodeHelpers.Clear();
      this.archiveTree.NodeHelpers.Add(archiveHelper);
      this.archiveTree.NodeHelpers.Add(archiveObjectViewHelper);
      this.archiveTree.Initialize();
    }

    protected void SetExtractedOnlyView()
    {
      HideMasterArchive();
      this.archiveTree.NodeHelpers.Clear();
      this.archiveTree.NodeHelpers.Add(archiveHelper);
      this.archiveTree.Initialize();
    }
    #endregion

    #region GUI Event Handlers
    private void barArchive_AllFiles_MergedView_CheckedChanged(object sender, ItemClickEventArgs e)
    {
      if ((e.Item as BarCheckItem).Checked)
      {
        viewLayout = LayoutMode.Merged;
      }
    }

    private void barArchive_AllFiles_SplitView_CheckedChanged(object sender, ItemClickEventArgs e)
    {
      if ((e.Item as BarCheckItem).Checked)
      {
        viewLayout = LayoutMode.Split;
      }
    }

    private void barArchive_ExtractedFilesOnly_CheckedChanged(object sender, ItemClickEventArgs e)
    {
      if ((e.Item as BarCheckItem).Checked)
      {
        viewLayout = LayoutMode.ExtractedOnly;
      }
    }

    private void barMasterFile_AddFileToTagLibrary_ItemClick(object sender, ItemClickEventArgs e)
    {
      MultiSyncTreeNode selectedNode = masterTree.SelectedNode as MultiSyncTreeNode;
      if (selectedNode == null) return;

      // Make sure that this is tag from the master list.
      bool fromMasterList = false;
      foreach (PathItem item in selectedNode.NodeInformation.PathItems)
      {
        if (item.NodeHelper.Name == "Master List")
        {
          fromMasterList = true;
          break;
        }
      }

      if (!fromMasterList) return;

      string itemPath = selectedNode.NodeInformation.PathItems["Master List"].Path;

      // Get the map location data about this file.
      TagInformation tagInfo = masterArchive.GetTagInformation(itemPath);
      DecompilerList[] lists = GenerateDecompilerLists(tagInfo);
      PerformExtraction(lists);
    }

    private void barMasterFolder_AddFolderToTagLibrary_ItemClick(object sender, ItemClickEventArgs e)
    {
      MasterArchiveExtractFolder();
    }

    #endregion

    #region Decompilation
    /// <summary>
    /// A list of files to be decompiled from a specified map file.
    /// </summary>
    public class DecompilerList
    {
      private ArrayList files = new ArrayList();

      public string mapFile;
      public string[] Files
      {
        get { return (string[])files.ToArray(typeof(string)); }
      }

      public void AddFile(string filename)
      {
        files.Add(filename);
      }
    }

    public class DecompilerListCollection : CollectionBase
    {
      public void Add(DecompilerList value)
      {
        InnerList.Add(value);
      }
      public DecompilerList this[int index]
      {
        get { return (DecompilerList)InnerList[index]; }
      }
      public DecompilerList this[string mapName]
      {
        get
        {
          foreach (DecompilerList list in InnerList)
          {
            if (list.mapFile == mapName) return list;
          }
          return null;
        }  
      }
      public DecompilerList[] ToArray()
      {
        return (DecompilerList[])InnerList.ToArray(typeof(DecompilerList));
      }
    }

    public class DecompilerNeedsMapsException : Exception
    {
      private DecompilerList[] maps;

      public DecompilerList[] Maps
      {
        get { return maps; }
      }

      public DecompilerNeedsMapsException(DecompilerList[] maps)
      {
        this.maps = maps;
      }
    }

    protected class IntegerEncapsulator
    {
      public int Value = 0;
    }

    protected DecompilerList[] CreateOptimizedList(params TagInformation[] tags)
    {
      // Get the map counts.    
      Hashtable count = new Hashtable();
      foreach (TagInformation tag in tags)
      {
        foreach (string map in tag.Maps)
        {
          if (count.ContainsKey(map))
          {
            (count[map] as IntegerEncapsulator).Value++;
          }
          else
          {
            count.Add(map, new IntegerEncapsulator());
          }
        }
      }

      // Select the map with the highest count for every tag.
      DecompilerListCollection lists = new DecompilerListCollection();
      foreach (TagInformation tag in tags)
      {
        string chosenMap = "";
        foreach (string map in tag.Maps)
        {
          if (chosenMap == "")
          {
            chosenMap = map;
            continue;
          }
          if ((count[map] as IntegerEncapsulator).Value > (count[chosenMap] as IntegerEncapsulator).Value)
            chosenMap = map;
        }
        // Add this file to the list, creating a new entry for the map if it does not exist.
        DecompilerList list = lists[chosenMap];
        if (list == null)
        {
          list = new DecompilerList();
          list.mapFile = chosenMap;
          list.AddFile(tag.Filename.ToLower());
          lists.Add(list);
        }
        else
        {
          list.AddFile(tag.Filename); 
        }
      }
      return lists.ToArray();
    }

    protected DecompilerList[] GenerateDecompilerLists(params TagInformation[] tags)
    {
      // Get a list of all of the maps that the user has installed.
      string mapPath = OptionsManager.HaloPC_MapsPath;//Path.GetDirectoryName(OptionsManager.HaloPC_MapsPath);
      string[] maps = Directory.GetFiles(mapPath, "*.map");

      // Make sure that the user has all of the maps neccessary 
      // to decompile the specified tags.
      ArrayList uncompilableTags = new ArrayList();
      foreach (TagInformation tag in tags)
      {
        bool hasRequiredMap = false;
        foreach (string map in tag.Maps)
        {
          for (int x=0; x<maps.Length; x++)
          {
            if (map == Path.GetFileName(maps[x]))
            {
              hasRequiredMap = true;
              break;
            }
          }
          if (hasRequiredMap) break;
        }
        if (!hasRequiredMap) uncompilableTags.Add(tag);
      }
      if (uncompilableTags.Count > 0)
      {
        // One or more selected tags could not be added to the Tag Library, because the
        // map from which they must be extracted could not be found.
        throw new DecompilerNeedsMapsException(
          CreateOptimizedList((TagInformation[])uncompilableTags.ToArray(typeof(TagInformation))));
      }
      return CreateOptimizedList(tags);
    }


    protected void MasterArchiveExtractFolder()
    {
      if (selectedNode.NodeInformation.NodeType != NodeType.Container) return;
      PathItem item = selectedNode.NodeInformation.PathItems["Master List"];
      if (item == null) return;

      // Generate a complete list of all files in this node and all child nodes.
      string[] files = masterArchive.GetRecursiveFileList(item.Path);
      
      // Get the map location data about this file.
      TagInformation[] tagInfo = new TagInformation[files.Length];
      for (int x=0; x<tagInfo.Length; x++)
      {
        tagInfo[x] = masterArchive.GetTagInformation(files[x]);
      }
      
      DecompilerList[] lists = GenerateDecompilerLists(tagInfo);
      if (lists.Length > 0)
      {
        PerformExtraction(lists);
      }
      else
      {
        Dialogs.ShowError("All tags in the selected path have already been added to the archive.");
      }
    }

    protected void PerformExtraction(DecompilerList[] lists)
    {
      this.totalFilesExtracted = 0;
      this.totalFilesToExtract = 0;
      foreach (DecompilerList list in lists)
      {
        this.totalFilesToExtract += list.Files.Length;
      }

      this.totalMapsToExtractFrom = lists.Length;
      this.totalMapsExtractedFrom = 0;

      this.lists = lists;
      this.progressPanel.Caption = "Extracting files...";
      this.progressPanel.ProgressValue = this.progressPanel.ProgressMinimum;
      this.progressPanel.ProgressMaximum = this.progressPanel.ProgressMinimum + this.totalFilesToExtract;
      this.progressPanel.Visible = true;

      decompiler = new Decompiler();
      decompiler.AutoCloseBatch = false;
      decompiler.SetOutputArchive(TagLibraryManager.HaloPC); // Hard coded for testing.
      decompiler.ExtractedFile += new Decompiler.ExtractedFileEventHandler(decompiler_ExtractedFile);
      decompiler.ExtractionComplete += new Decompiler.ExtractionCompleteEventHandler(decompiler_ExtractionComplete);
      DoExtraction();
    }

    protected void DoExtraction()
    {
      DecompilerList list = this.lists[totalMapsExtractedFrom];
      // Decompile the selected file.
      // TODO: We will need to modify the decompiler so that it doesn't end the batch update of the zip file
      // until we have processed all of the DecompilerList items (different map files).
      string fullMapFilePath = OptionsManager.HaloPC_MapsPath + "\\" + list.mapFile;
      decompiler.InitDecompiler(fullMapFilePath);
      decompiler.ExtractTags(list.Files);
    }


    private delegate void TreeInitializer();

	  private void decompiler_ExtractionComplete(object sender, Decompiler.ExtractionCompleteEventArgs e)
	  {
      this.totalMapsExtractedFrom++;
      if (totalMapsToExtractFrom == totalMapsExtractedFrom)
      {
        this.progressPanel.Caption = "Rebuilding Archive";
        decompiler.CloseBatch();        
        if (this.archiveTree.InvokeRequired)
        {
          this.archiveTree.Invoke(new TreeInitializer(this.archiveTree.Initialize));
        }
        else
        {
          this.archiveTree.Initialize();  
        }
      
        if (this.masterTree.InvokeRequired)
        {
          this.masterTree.Invoke(new TreeInitializer(this.masterTree.Initialize));
        }
        else
        {
          this.masterTree.Initialize();  
        }
        this.progressPanel.Visible = false;
      }
      else
      {
        // Start the next extraction.
        DoExtraction();
      }
	  }

	  private void decompiler_ExtractedFile(object sender, Decompiler.ExtractedFileEventArgs e)
	  {
      this.totalFilesExtracted++;
      this.progressPanel.ProgressValue = this.totalFilesExtracted;
	  }
    #endregion

    private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
    {
      MasterArchiveExtractFolder();
    }

    private void barObjectView_ItemPress(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
      // Setup Object View
    }

    private void barFileView_ItemPress(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
      // Setup File View
    }

    public enum ViewMode
    {
      File,
      Object
    }

	  public enum LayoutMode
    {
      Merged,
      Split,
      ExtractedOnly
    }
	}

  public class PreviewActivatedEventArgs : EventArgs
  {
    private PreviewMode previewMode;
    public PreviewMode PreviewMode
    {
      get { return previewMode; }
      set { previewMode = value; }
    }
    public PreviewActivatedEventArgs(PreviewMode mode)
    {
      this.previewMode = mode;
    }
  }

  public delegate void PreviewActivatedEventHandler(PreviewActivatedEventArgs e);

}