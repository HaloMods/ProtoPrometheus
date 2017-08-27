using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using DevExpress.XtraNavBar.ViewInfo;
using Prometheus.Core;
using Prometheus.Core.Tags;
using Prometheus.Core.Project;

namespace SharedControls
{
  //TODO list
  //if no project loaded, grey out project icon
  //ability to switch dialog "versions"?
  //

	public class TagBrowserDialog : XtraForm
	{
    private SimpleButton simpleButtonOpenTag;
    private ComboBoxEdit comboFilename;
    private ComboBoxEdit comboTagFilter;
    private SimpleButton simpleButtonCancel;
    private TagFileName selectedTag = null;
    private MapfileVersion mapfileVersion = MapfileVersion.HALOPC;
    private Label label1;
    private Label label2;
    private Label label3;
    private SimpleButton btnParentFolder;
    private NavBarControl navBarControl1;
    private NavBarGroup navBarGroup1;
    private NavBarItem itemArchive;
    private NavBarItem itemUser;
    private NavBarItem itemProject;
    private TreeViewComboBox treeViewComboBox1;
    private TagArchiveFileListView tagArchiveFileListView1;
    private Container components = null;
    
    private string currentPath = "";
    private string filter;

    private ITagLibrary archiveLibrary;
    private ITagLibrary userLibrary;
    private ITagLibrary projectLibrary;
    private ITagLibrary activeLibrary;

    public ITagLibrary ArchiveLibrary
    {
      get { return archiveLibrary; }
      set { archiveLibrary = value; }
    }
    
    public ITagLibrary UserLibrary
    {
      get { return userLibrary; }
      set { userLibrary = value; }
    }
    public ITagLibrary ProjectLibrary
    {
      get { return projectLibrary; }
      set { projectLibrary = value; }
    }
    
    public TagFileName SelectedTag
    {
      get{return selectedTag;}
    }

    public ITagLibrary ActiveLibrary
    {
      get { return activeLibrary; }
      set
      {
        activeLibrary = value;
        this.tagArchiveFileListView1.TagLibrary = this.activeLibrary;
        this.treeViewComboBox1.TagLibrary = this.activeLibrary;
        UpdateTagComboBox("");
      }
    }

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
		public TagBrowserDialog(MapfileVersion ver)
		{
			InitializeComponent();

      if (!DesignMode)
      {
        mapfileVersion = ver;

        switch(mapfileVersion)
        {
          case MapfileVersion.HALOPC:
            this.archiveLibrary = TagLibraryManager.HaloPC;
            break;
          case MapfileVersion.HALOCE:
            this.archiveLibrary = TagLibraryManager.HaloPC;
            break;
          case MapfileVersion.XHALO1:
            this.archiveLibrary = TagLibraryManager.HaloXbox;
            break;
          case MapfileVersion.XHALO2:
            this.archiveLibrary = TagLibraryManager.Halo2Xbox;
            break;
        }
        this.treeViewComboBox1.PathChanged += new TreeViewComboBox.PathChangedEventHandler(treeViewComboBox1_PathChanged);
        this.navBarControl1.SelectedLink = this.navBarControl1.Items[0].Links[0];
        this.tagArchiveFileListView1.ItemDoubleClicked += new EventHandler(tagArchiveFileListView1_ItemDoubleClicked);
        this.tagArchiveFileListView1.SelectedIndexChanged += new EventHandler(tagArchiveFileListView1_SelectedIndexChanged);
        this.tagArchiveFileListView1.MultiSelect = false;

        //Initialize Project Section state
        navBarGroup1.ItemLinks[2].Item.Enabled = ProjectManager.ProjectLoaded;
        if(ProjectManager.ProjectLoaded)
        {
          this.projectLibrary = new DiskFolderTagLibrary(OptionsManager.ActiveProjectTagsPath, "Project Tags");
        }

        //Initialize User Library section
        navBarGroup1.ItemLinks[1].Item.Enabled = false;
        this.userLibrary = null;//TagLibraryManager.Halo2Xbox;


      }
    }

    public class FileFilter
    {
      string text;
      string filter;

      public string Text
      {
        get { return text; }
        set { text = value; }
      }

      public string Filter
      {
        get { return filter; }
        set { filter = value; }
      }

      public FileFilter(string text, string filter)
      {
        this.text = text;
        this.filter = filter;
      }

      public override string ToString()
      {
        return text + " (" + filter + ")";
      }
    }

    // TODO: Use a filter collection and make this match other standard browser dialogs.
    public void AddFilter(string filter)
    {
      string[] parts = filter.Split('|');
      if (parts.Length < 2) return;
      
      // TODO: Add support for more than one filter.
      comboTagFilter.Properties.Items.Clear();
      comboTagFilter.Properties.Items.Add(new FileFilter(parts[0], parts[1]));
      comboTagFilter.SelectedIndex = 0;
      this.filter = parts[1];
      this.tagArchiveFileListView1.Filter = this.filter;
    }

	  private void tagArchiveFileListView1_SelectedIndexChanged(object sender, EventArgs e)
	  {
      ListView.SelectedListViewItemCollection items = tagArchiveFileListView1.SelectedItems;
      if (items.Count == 0) return;
      ListViewItem item = items[0];
      if (item.Tag == null) return;
      if (!(item.Tag is FileEntryInformation)) return;
      FileEntryInformation entry = (item.Tag as FileEntryInformation);
      
      if (entry.FileType == FileEntryType.File)
      {
        UpdateTagComboBox(entry.FullPath);
        this.selectedTag = new TagFileName(entry.FullPath.Trim('\\'), mapfileVersion, WhichSource(this.activeLibrary));
      }
	  }

    internal void UpdateTagComboBox(string filename)
    {
      this.comboFilename.Properties.Items.Clear();
      this.comboFilename.Properties.Items.Add(Path.GetFileName(filename));
      this.comboFilename.SelectedIndex = 0;
    }

    internal TagSource WhichSource(ITagLibrary library)
    {
      if (this.ActiveLibrary == this.archiveLibrary)
        return TagSource.Archive;
      if (this.ActiveLibrary == this.userLibrary)
        return TagSource.LocalShared;
      if (this.ActiveLibrary == this.projectLibrary)
        return TagSource.LocalProject;
      
      throw new Exception("Could not determine the source of the specified library: " + library.Name);
    }

	  private void tagArchiveFileListView1_ItemDoubleClicked(object sender, EventArgs e)
	  {
	    ListView.SelectedListViewItemCollection items = tagArchiveFileListView1.SelectedItems;
      if (items.Count == 0) return;
      ListViewItem item = items[0];
      if (item.Tag == null) return;
      if (!(item.Tag is FileEntryInformation)) return;
      FileEntryInformation entry = (item.Tag as FileEntryInformation);
      
      if (entry.FileType == FileEntryType.Folder)
        ChangePath(entry.FullPath);

      if (entry.FileType == FileEntryType.File)
      {
        if (this.selectedTag == null) return;
        this.DialogResult = DialogResult.OK;
        this.Close();
      }
	  }

	  private void treeViewComboBox1_PathChanged(object sender, TreeViewComboBox.PathChangedEventArgs e)
	  {
      ChangePath(e.Path);
	  }

    private void ChangePath(string path)
    {
      this.currentPath = path.Trim('\\');
      if (this.currentPath.Length > 0)
      {
        this.btnParentFolder.Enabled = true;
      }
      else
      {
        this.btnParentFolder.Enabled = false;
      }
      this.selectedTag = null;
      this.tagArchiveFileListView1.SetCurrentDirectory(path);
      this.treeViewComboBox1.SelectPath(this.currentPath);
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TagBrowserDialog));
      this.simpleButtonOpenTag = new DevExpress.XtraEditors.SimpleButton();
      this.comboFilename = new DevExpress.XtraEditors.ComboBoxEdit();
      this.comboTagFilter = new DevExpress.XtraEditors.ComboBoxEdit();
      this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.btnParentFolder = new DevExpress.XtraEditors.SimpleButton();
      this.label3 = new System.Windows.Forms.Label();
      this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
      this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
      this.itemArchive = new DevExpress.XtraNavBar.NavBarItem();
      this.itemUser = new DevExpress.XtraNavBar.NavBarItem();
      this.itemProject = new DevExpress.XtraNavBar.NavBarItem();
      this.treeViewComboBox1 = new TreeViewComboBox();
      this.tagArchiveFileListView1 = new TagArchiveFileListView();
      ((System.ComponentModel.ISupportInitialize)(this.comboFilename.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboTagFilter.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
      this.SuspendLayout();
      // 
      // simpleButtonOpenTag
      // 
      this.simpleButtonOpenTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.simpleButtonOpenTag.Location = new System.Drawing.Point(528, 320);
      this.simpleButtonOpenTag.Name = "simpleButtonOpenTag";
      this.simpleButtonOpenTag.Size = new System.Drawing.Size(96, 24);
      this.simpleButtonOpenTag.TabIndex = 1;
      this.simpleButtonOpenTag.Text = "Open";
      this.simpleButtonOpenTag.Click += new System.EventHandler(this.simpleButtonOpenTag_Click);
      // 
      // comboFilename
      // 
      this.comboFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.comboFilename.EditValue = "-";
      this.comboFilename.Location = new System.Drawing.Point(202, 320);
      this.comboFilename.Name = "comboFilename";
      // 
      // comboFilename.Properties
      // 
      this.comboFilename.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                          new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.comboFilename.Size = new System.Drawing.Size(316, 22);
      this.comboFilename.TabIndex = 2;
      // 
      // comboTagFilter
      // 
      this.comboTagFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.comboTagFilter.EditValue = "-";
      this.comboTagFilter.Location = new System.Drawing.Point(202, 347);
      this.comboTagFilter.Name = "comboTagFilter";
      // 
      // comboTagFilter.Properties
      // 
      this.comboTagFilter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                           new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.comboTagFilter.Size = new System.Drawing.Size(316, 22);
      this.comboTagFilter.TabIndex = 3;
      this.comboTagFilter.SelectedIndexChanged += new System.EventHandler(this.comboTagFilter_SelectedIndexChanged);
      // 
      // simpleButtonCancel
      // 
      this.simpleButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.simpleButtonCancel.Location = new System.Drawing.Point(528, 347);
      this.simpleButtonCancel.Name = "simpleButtonCancel";
      this.simpleButtonCancel.Size = new System.Drawing.Size(96, 24);
      this.simpleButtonCancel.TabIndex = 4;
      this.simpleButtonCancel.Text = "Cancel";
      this.simpleButtonCancel.Click += new System.EventHandler(this.simpleButtonCancel_Click);
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(242)), ((System.Byte)(239)), ((System.Byte)(231)));
      this.label1.Location = new System.Drawing.Point(115, 320);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(67, 23);
      this.label1.TabIndex = 6;
      this.label1.Text = "File name:";
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(242)), ((System.Byte)(239)), ((System.Byte)(231)));
      this.label2.Location = new System.Drawing.Point(115, 347);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(87, 23);
      this.label2.TabIndex = 7;
      this.label2.Text = "Files of type:";
      // 
      // btnParentFolder
      // 
      this.btnParentFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnParentFolder.Image = ((System.Drawing.Image)(resources.GetObject("btnParentFolder.Image")));
      this.btnParentFolder.Location = new System.Drawing.Point(594, 6);
      this.btnParentFolder.Name = "btnParentFolder";
      this.btnParentFolder.Size = new System.Drawing.Size(31, 26);
      this.btnParentFolder.TabIndex = 10;
      this.btnParentFolder.Click += new System.EventHandler(this.btnParentFolder_Click);
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(10, 9);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(96, 18);
      this.label3.TabIndex = 11;
      this.label3.Text = "Look In:";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // navBarControl1
      // 
      this.navBarControl1.ActiveGroup = this.navBarGroup1;
      this.navBarControl1.AllowDrop = true;
      this.navBarControl1.AllowSelectedLink = true;
      this.navBarControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left)));
      this.navBarControl1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(242)), ((System.Byte)(239)), ((System.Byte)(231)));
      this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
                                                                                    this.navBarGroup1});
      this.navBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
                                                                                  this.itemArchive,
                                                                                  this.itemUser,
                                                                                  this.itemProject});
      this.navBarControl1.Location = new System.Drawing.Point(10, 37);
      this.navBarControl1.Name = "navBarControl1";
      this.navBarControl1.Size = new System.Drawing.Size(96, 338);
      this.navBarControl1.StoreDefaultPaintStyleName = true;
      this.navBarControl1.TabIndex = 12;
      this.navBarControl1.SelectedLinkChanged += new DevExpress.XtraNavBar.ViewInfo.NavBarSelectedLinkChangedEventHandler(this.navBarControl1_SelectedLinkChanged);
      // 
      // navBarGroup1
      // 
      this.navBarGroup1.Caption = "Source";
      this.navBarGroup1.Expanded = true;
      this.navBarGroup1.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsText;
      this.navBarGroup1.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
                                                                                        new DevExpress.XtraNavBar.NavBarItemLink(this.itemArchive),
                                                                                        new DevExpress.XtraNavBar.NavBarItemLink(this.itemUser),
                                                                                        new DevExpress.XtraNavBar.NavBarItemLink(this.itemProject)});
      this.navBarGroup1.Name = "navBarGroup1";
      // 
      // itemArchive
      // 
      this.itemArchive.Caption = "Archive";
      this.itemArchive.LargeImage = ((System.Drawing.Image)(resources.GetObject("itemArchive.LargeImage")));
      this.itemArchive.Name = "itemArchive";
      // 
      // itemUser
      // 
      this.itemUser.Caption = "User";
      this.itemUser.LargeImage = ((System.Drawing.Image)(resources.GetObject("itemUser.LargeImage")));
      this.itemUser.Name = "itemUser";
      // 
      // itemProject
      // 
      this.itemProject.Caption = "Project";
      this.itemProject.LargeImage = ((System.Drawing.Image)(resources.GetObject("itemProject.LargeImage")));
      this.itemProject.Name = "itemProject";
      // 
      // treeViewComboBox1
      // 
      this.treeViewComboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.treeViewComboBox1.Location = new System.Drawing.Point(115, 7);
      this.treeViewComboBox1.Name = "treeViewComboBox1";
      this.treeViewComboBox1.Size = new System.Drawing.Size(471, 23);
      this.treeViewComboBox1.TabIndex = 13;
      this.treeViewComboBox1.TagLibrary = null;
      // 
      // tagArchiveFileListView1
      // 
      this.tagArchiveFileListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.tagArchiveFileListView1.Filter = null;
      this.tagArchiveFileListView1.Location = new System.Drawing.Point(115, 37);
      this.tagArchiveFileListView1.MultiSelect = true;
      this.tagArchiveFileListView1.Name = "tagArchiveFileListView1";
      this.tagArchiveFileListView1.Size = new System.Drawing.Size(507, 274);
      this.tagArchiveFileListView1.TabIndex = 14;
      this.tagArchiveFileListView1.TagLibrary = null;
      // 
      // TagBrowserDialog
      // 
      this.Appearance.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(242)), ((System.Byte)(239)), ((System.Byte)(231)));
      this.Appearance.Options.UseBackColor = true;
      this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
      this.ClientSize = new System.Drawing.Size(633, 381);
      this.Controls.Add(this.tagArchiveFileListView1);
      this.Controls.Add(this.treeViewComboBox1);
      this.Controls.Add(this.navBarControl1);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.btnParentFolder);
      this.Controls.Add(this.comboTagFilter);
      this.Controls.Add(this.comboFilename);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.simpleButtonCancel);
      this.Controls.Add(this.simpleButtonOpenTag);
      this.MinimumSize = new System.Drawing.Size(504, 320);
      this.Name = "TagBrowserDialog";
      this.Text = "Browse for Tag...";
      ((System.ComponentModel.ISupportInitialize)(this.comboFilename.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboTagFilter.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

    private void simpleButtonOpenTag_Click(object sender, EventArgs e)
    {
      if (this.selectedTag != null)
      {
        this.DialogResult = DialogResult.OK;
        this.Close();
      }
    }

    private void simpleButtonCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void comboTagFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
      //collapse the tree?
    }

    private void navBarControl1_SelectedLinkChanged(object sender, NavBarSelectedLinkChangedEventArgs e)
    {
      if (e.Link.Item == this.itemArchive)
      {
        this.ActiveLibrary = this.archiveLibrary;
      }
      else if (e.Link.Item == this.itemUser)
      {
        this.ActiveLibrary = this.UserLibrary;
      }
      else if(e.Link.Item == this.itemProject)
      {
        this.ActiveLibrary = this.ProjectLibrary;
      }
    }

    private void btnParentFolder_Click(object sender, System.EventArgs e)
    {
      // Move up one folder.
      if (this.currentPath == "") return;
      string tempPath = "\\" + this.currentPath;
      int finalDelimiter = tempPath.LastIndexOf('\\');
      if (finalDelimiter == 0)
      {
        this.currentPath = "";  // Root path.
      }
      if (finalDelimiter > 0)
      {
        this.currentPath = tempPath.Substring(1, finalDelimiter-1);
      }
      treeViewComboBox1.SelectPath(this.currentPath);
    }
  }
}

