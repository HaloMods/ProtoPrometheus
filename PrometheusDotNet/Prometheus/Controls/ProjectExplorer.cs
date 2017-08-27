using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Prometheus.Core;
using Prometheus.Core.Project;
using Prometheus.Core.Tags;

namespace Prometheus.Controls
{
	/// <summary>
	/// Summary description for ProjectExplorer.
	/// </summary>
	public class ProjectExplorer : XtraUserControl
	{
    private BarManager barManager1;
    private BarDockControl barDockControlTop;
    private BarDockControl barDockControlBottom;
    private BarDockControl barDockControlLeft;
    private BarDockControl barDockControlRight;
    private Bar bar1;
    private BarStaticItem barStaticItem1;
    private BarCheckItem barFileView;
    private BarCheckItem barObjectView;
    private BarButtonItem barButtonItem1;
    private BarButtonItem barButtonItem2;
    private BarButtonItem barButtonItem3;
		private Container components = null;
    private MultiSyncTreeView tagTree;
    private BarButtonItem barButtonItem4;
    private BarButtonItem barButtonItem5;
    private PopupMenu tagContainerPopup;
    private PopupMenu tagObjectPopup;
    private PopupMenu baseTagPopup;
    private DevExpress.XtraBars.BarButtonItem barButtonItem6;
    private ProjectFile project;

    public ProjectFile Project
    {
      get { return project; }
      set
      {
        if (value != null)
        {
          project = value; 
          BindToProject();
        }
      }
    }

		public ProjectExplorer()
		{
			InitializeComponent();
      tagTree.MouseUp += new MouseEventHandler(tagTree_MouseUp);
		}

	  private void tagTree_MouseUp(object sender, MouseEventArgs e)
	  {
      if(e.Button == MouseButtons.Right)
      {
        tagTree.ShowPopup();
      }
	  }

	  private void BindToProject()
    {
      tagTree.NodeHelpers.Clear();
      tagTree.Nodes.Clear();

      // TODO: Fix the project tag path stuff, and get a correct path here at runtime.
      //      string projectPath = "Insert Current Project Tag Path Here";
      //      DiskFolderTagLibrary pr = new DiskFolderTagLibrary(projectPath, "Project Folder");
      //      DiskLibraryNodeHelper project = new DiskLibraryNodeHelper("Project", pr);
      //      project.ForeColor = Color.LightGray;
      //      tagTree.NodeHelpers.Add(project);

	    BaseTagLibrary baseTags = new BaseTagLibrary("Base Tagset");
      BaseTagLibraryNodeHelper baseHelper = new BaseTagLibraryNodeHelper("BaseHelper",
        baseTags);
      baseHelper.NodeDefinitions["object"].PopupMenu = this.baseTagPopup;
      tagTree.NodeHelpers.Add(baseHelper);

      BaseTagLibraryNodeHelper baseHelper2 = new BaseTagLibraryNodeHelper("BaseHelper 2",
        baseTags);
      baseHelper2.NodeDefinitions["object"].PopupMenu = this.tagObjectPopup;
      tagTree.NodeHelpers.Add(baseHelper2);

      ArrayList tfnList = new ArrayList();
      foreach (TemplateTag tag in project.Template.TagSet)
      {
        if (project.Tags[tag.Name] == null) continue;
        baseTags.AddTag(project.Tags[tag.Name].Path);
        tfnList.Add(project.Tags[tag.Name].TagFileName);
      }
      
      ArchiveLibraryNodeHelper helper = new ArchiveLibraryNodeHelper("Project Dependencies",
        new DependencyTagLibrary(tfnList.ToArray(typeof(TagFileName)) as TagFileName[]));
     
      helper.NodeDefinitions["container"].PopupMenu = this.tagContainerPopup;
      helper.NodeDefinitions["object"].PopupMenu = this.tagObjectPopup;

      helper.OptionalRootNode = new MultiSyncTreeNode("Tags");
      helper.OptionalRootNode.NodeInformation = new NodeInformation(NodeType.Container);
      tagTree.NodeHelpers.Add(helper);

      tagTree.Initialize();
      tagTree.Nodes[0].Text = "Project '" + project.MapName + "'";
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
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ProjectExplorer));
      this.barManager1 = new DevExpress.XtraBars.BarManager();
      this.bar1 = new DevExpress.XtraBars.Bar();
      this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
      this.barFileView = new DevExpress.XtraBars.BarCheckItem();
      this.barObjectView = new DevExpress.XtraBars.BarCheckItem();
      this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
      this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
      this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
      this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
      this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
      this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
      this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
      this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
      this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
      this.tagTree = new Prometheus.Controls.MultiSyncTreeView();
      this.tagContainerPopup = new DevExpress.XtraBars.PopupMenu();
      this.tagObjectPopup = new DevExpress.XtraBars.PopupMenu();
      this.baseTagPopup = new DevExpress.XtraBars.PopupMenu();
      this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
      ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.tagContainerPopup)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.tagObjectPopup)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.baseTagPopup)).BeginInit();
      this.SuspendLayout();
      // 
      // barManager1
      // 
      this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
                                                                     this.bar1});
      this.barManager1.DockControls.Add(this.barDockControlTop);
      this.barManager1.DockControls.Add(this.barDockControlBottom);
      this.barManager1.DockControls.Add(this.barDockControlLeft);
      this.barManager1.DockControls.Add(this.barDockControlRight);
      this.barManager1.Form = this;
      this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
                                                                          this.barStaticItem1,
                                                                          this.barFileView,
                                                                          this.barObjectView,
                                                                          this.barButtonItem1,
                                                                          this.barButtonItem2,
                                                                          this.barButtonItem3,
                                                                          this.barButtonItem4,
                                                                          this.barButtonItem5,
                                                                          this.barButtonItem6});
      this.barManager1.MaxItemId = 12;
      // 
      // bar1
      // 
      this.bar1.BarName = "Custom 1";
      this.bar1.DockCol = 0;
      this.bar1.DockRow = 1;
      this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
      this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
                                                                                      new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem1),
                                                                                      new DevExpress.XtraBars.LinkPersistInfo(this.barFileView),
                                                                                      new DevExpress.XtraBars.LinkPersistInfo(this.barObjectView),
                                                                                      new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1, true),
                                                                                      new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2),
                                                                                      new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3)});
      this.bar1.OptionsBar.AllowQuickCustomization = false;
      this.bar1.OptionsBar.DisableClose = true;
      this.bar1.OptionsBar.DisableCustomization = true;
      this.bar1.OptionsBar.DrawDragBorder = false;
      this.bar1.OptionsBar.UseWholeRow = true;
      this.bar1.Text = "Custom 1";
      // 
      // barStaticItem1
      // 
      this.barStaticItem1.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
      this.barStaticItem1.Caption = "View: ";
      this.barStaticItem1.Id = 0;
      this.barStaticItem1.Name = "barStaticItem1";
      this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
      // 
      // barFileView
      // 
      this.barFileView.Caption = "File View";
      this.barFileView.Checked = true;
      this.barFileView.Glyph = ((System.Drawing.Image)(resources.GetObject("barFileView.Glyph")));
      this.barFileView.GroupIndex = 1;
      this.barFileView.Id = 1;
      this.barFileView.Name = "barFileView";
      // 
      // barObjectView
      // 
      this.barObjectView.Caption = "Object View";
      this.barObjectView.Glyph = ((System.Drawing.Image)(resources.GetObject("barObjectView.Glyph")));
      this.barObjectView.GroupIndex = 1;
      this.barObjectView.Id = 2;
      this.barObjectView.Name = "barObjectView";
      // 
      // barButtonItem1
      // 
      this.barButtonItem1.Caption = "barButtonItem1";
      this.barButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.Glyph")));
      this.barButtonItem1.Id = 6;
      this.barButtonItem1.Name = "barButtonItem1";
      // 
      // barButtonItem2
      // 
      this.barButtonItem2.Caption = "barButtonItem2";
      this.barButtonItem2.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.Glyph")));
      this.barButtonItem2.Id = 7;
      this.barButtonItem2.Name = "barButtonItem2";
      // 
      // barButtonItem3
      // 
      this.barButtonItem3.Caption = "barButtonItem3";
      this.barButtonItem3.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem3.Glyph")));
      this.barButtonItem3.Id = 8;
      this.barButtonItem3.Name = "barButtonItem3";
      // 
      // barButtonItem4
      // 
      this.barButtonItem4.Caption = "Container Test Item";
      this.barButtonItem4.Id = 9;
      this.barButtonItem4.Name = "barButtonItem4";
      // 
      // barButtonItem5
      // 
      this.barButtonItem5.Caption = "Object Test Item";
      this.barButtonItem5.Id = 10;
      this.barButtonItem5.Name = "barButtonItem5";
      // 
      // tagTree
      // 
      this.tagTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.tagTree.ImageIndex = -1;
      this.tagTree.Location = new System.Drawing.Point(8, 32);
      this.tagTree.Name = "tagTree";
      this.tagTree.SelectedImageIndex = -1;
      this.tagTree.ShowFiles = true;
      this.tagTree.Size = new System.Drawing.Size(168, 328);
      this.tagTree.TabIndex = 4;
      // 
      // tagContainerPopup
      // 
      this.tagContainerPopup.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
                                                                                                   new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem4)});
      this.tagContainerPopup.Manager = this.barManager1;
      this.tagContainerPopup.Name = "tagContainerPopup";
      // 
      // tagObjectPopup
      // 
      this.tagObjectPopup.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
                                                                                                new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem5)});
      this.tagObjectPopup.Manager = this.barManager1;
      this.tagObjectPopup.Name = "tagObjectPopup";
      // 
      // baseTagPopup
      // 
      this.baseTagPopup.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
                                                                                              new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem6)});
      this.baseTagPopup.Manager = this.barManager1;
      this.baseTagPopup.Name = "baseTagPopup";
      // 
      // barButtonItem6
      // 
      this.barButtonItem6.Caption = "This is definately a base tag!";
      this.barButtonItem6.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem6.Glyph")));
      this.barButtonItem6.Id = 11;
      this.barButtonItem6.Name = "barButtonItem6";
      // 
      // ProjectExplorer
      // 
      this.Controls.Add(this.tagTree);
      this.Controls.Add(this.barDockControlLeft);
      this.Controls.Add(this.barDockControlRight);
      this.Controls.Add(this.barDockControlBottom);
      this.Controls.Add(this.barDockControlTop);
      this.Name = "ProjectExplorer";
      this.Size = new System.Drawing.Size(184, 368);
      ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.tagContainerPopup)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.tagObjectPopup)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.baseTagPopup)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion
	}
}

