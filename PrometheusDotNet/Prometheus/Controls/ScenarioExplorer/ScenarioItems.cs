using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;

namespace Prometheus.Controls
{
	/// <summary>
	/// Summary description for ScenarioItems.
	/// </summary>
	public class ScenarioItems : XtraUserControl
	{
    private BarManager barManager1;
    private BarDockControl barDockControlTop;
    private BarDockControl barDockControlBottom;
    private BarDockControl barDockControlLeft;
    private BarDockControl barDockControlRight;
    private Bar bar1;
    private BarButtonItem buttonCreateNewInstance;
    private BarButtonItem buttonDeleteObject;
    private BarButtonItem buttonGoToObject;
    private BarButtonItem buttonInfo;
    private BarButtonItem buttonHelp;
    private ListView listViewFolderContents;
    private System.Windows.Forms.ColumnHeader columnHeader1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public ScenarioItems()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call

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
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ScenarioItems));
      this.barManager1 = new DevExpress.XtraBars.BarManager();
      this.bar1 = new DevExpress.XtraBars.Bar();
      this.buttonCreateNewInstance = new DevExpress.XtraBars.BarButtonItem();
      this.buttonDeleteObject = new DevExpress.XtraBars.BarButtonItem();
      this.buttonGoToObject = new DevExpress.XtraBars.BarButtonItem();
      this.buttonInfo = new DevExpress.XtraBars.BarButtonItem();
      this.buttonHelp = new DevExpress.XtraBars.BarButtonItem();
      this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
      this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
      this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
      this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
      this.listViewFolderContents = new System.Windows.Forms.ListView();
      this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
      ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
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
                                                                          this.buttonCreateNewInstance,
                                                                          this.buttonDeleteObject,
                                                                          this.buttonGoToObject,
                                                                          this.buttonInfo,
                                                                          this.buttonHelp});
      this.barManager1.MaxItemId = 5;
      // 
      // bar1
      // 
      this.bar1.BarName = "Custom 1";
      this.bar1.DockCol = 0;
      this.bar1.DockRow = 1;
      this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
      this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
                                                                                      new DevExpress.XtraBars.LinkPersistInfo(this.buttonCreateNewInstance),
                                                                                      new DevExpress.XtraBars.LinkPersistInfo(this.buttonDeleteObject),
                                                                                      new DevExpress.XtraBars.LinkPersistInfo(this.buttonGoToObject, true),
                                                                                      new DevExpress.XtraBars.LinkPersistInfo(this.buttonInfo, true),
                                                                                      new DevExpress.XtraBars.LinkPersistInfo(this.buttonHelp)});
      this.bar1.OptionsBar.AllowQuickCustomization = false;
      this.bar1.OptionsBar.DisableClose = true;
      this.bar1.OptionsBar.DisableCustomization = true;
      this.bar1.OptionsBar.DrawDragBorder = false;
      this.bar1.OptionsBar.UseWholeRow = true;
      this.bar1.Text = "Custom 1";
      // 
      // buttonCreateNewInstance
      // 
      this.buttonCreateNewInstance.Caption = "Create Instance";
      this.buttonCreateNewInstance.Description = "Creates a New Instance";
      this.buttonCreateNewInstance.Glyph = ((System.Drawing.Image)(resources.GetObject("buttonCreateNewInstance.Glyph")));
      this.buttonCreateNewInstance.Id = 0;
      this.buttonCreateNewInstance.Name = "buttonCreateNewInstance";
      this.buttonCreateNewInstance.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonCreateNewInstance_ItemClick);
      // 
      // buttonDeleteObject
      // 
      this.buttonDeleteObject.Caption = "Delete Object";
      this.buttonDeleteObject.Description = "Deletes the Selected Object";
      this.buttonDeleteObject.Enabled = false;
      this.buttonDeleteObject.Glyph = ((System.Drawing.Image)(resources.GetObject("buttonDeleteObject.Glyph")));
      this.buttonDeleteObject.Id = 1;
      this.buttonDeleteObject.Name = "buttonDeleteObject";
      // 
      // buttonGoToObject
      // 
      this.buttonGoToObject.Caption = "Go To Object";
      this.buttonGoToObject.Description = "Moves the Camera to the Selected Object";
      this.buttonGoToObject.Glyph = ((System.Drawing.Image)(resources.GetObject("buttonGoToObject.Glyph")));
      this.buttonGoToObject.Id = 2;
      this.buttonGoToObject.Name = "buttonGoToObject";
      this.buttonGoToObject.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonGoToObject_ItemClick);
      // 
      // buttonInfo
      // 
      this.buttonInfo.Caption = "Info";
      this.buttonInfo.Glyph = ((System.Drawing.Image)(resources.GetObject("buttonInfo.Glyph")));
      this.buttonInfo.Id = 3;
      this.buttonInfo.Name = "buttonInfo";
      // 
      // buttonHelp
      // 
      this.buttonHelp.Caption = "Help";
      this.buttonHelp.Glyph = ((System.Drawing.Image)(resources.GetObject("buttonHelp.Glyph")));
      this.buttonHelp.Id = 4;
      this.buttonHelp.Name = "buttonHelp";
      // 
      // listViewFolderContents
      // 
      this.listViewFolderContents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                             this.columnHeader1});
      this.listViewFolderContents.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listViewFolderContents.HideSelection = false;
      this.listViewFolderContents.Location = new System.Drawing.Point(0, 25);
      this.listViewFolderContents.MultiSelect = false;
      this.listViewFolderContents.Name = "listViewFolderContents";
      this.listViewFolderContents.Size = new System.Drawing.Size(272, 295);
      this.listViewFolderContents.TabIndex = 5;
      this.listViewFolderContents.View = System.Windows.Forms.View.List;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Width = 150;
      // 
      // ScenarioItems
      // 
      this.Controls.Add(this.listViewFolderContents);
      this.Controls.Add(this.barDockControlLeft);
      this.Controls.Add(this.barDockControlRight);
      this.Controls.Add(this.barDockControlBottom);
      this.Controls.Add(this.barDockControlTop);
      this.Name = "ScenarioItems";
      this.Size = new System.Drawing.Size(272, 320);
      this.Load += new System.EventHandler(this.ScenarioItems_Load);
      ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

    private void ScenarioItems_Load(object sender, EventArgs e)
    {
      this.listViewFolderContents.SmallImageList = SharedControls.Utility.GenerateImageList(
        "Prometheus.Icons.App_Basics._16.folder.png",
        "Prometheus.Icons.App_Basics._16.document.png");
    }

    public void Clear()
    {
      this.listViewFolderContents.Items.Clear();
    }

    public void AddItem(string title, object tag)
    {
      ListViewItem item = new ListViewItem(title);
      item.ImageIndex = 1;
      item.SubItems.Add(title);
      item.Tag = tag;
      this.listViewFolderContents.Items.Add(item);
    }

    private void buttonCreateNewInstance_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
      if (this.listViewFolderContents.SelectedItems.Count == 0) return;
      ListViewItem item = this.listViewFolderContents.SelectedItems[0];
      if (CreateInstance != null)
        CreateInstance(item.Text, item.Tag);
    }

    private void buttonGoToObject_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
      if (this.listViewFolderContents.SelectedItems.Count == 0) return;
      ListViewItem item = this.listViewFolderContents.SelectedItems[0];
      if (FocusOnObject != null)
        FocusOnObject(item.Text, item.Tag);
    }

    public event ItemClickedEventHandler CreateInstance;
    public event ItemClickedEventHandler FocusOnObject;
  }
  public delegate void ItemClickedEventHandler(string itemTitle, object innerObject);
}