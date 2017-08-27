using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Prometheus.Core;

namespace SharedControls
{
	public class TagArchiveFileListView : XtraUserControl
	{
    private BetterListView tagList;
    private IContainer components;
    private ColumnHeader columnHeader1;
    private ToolTip toolTip1;
    private ITagLibrary tagLibrary;
    private string filter;

	  public string Filter
	  {
	    get { return filter; }
	    set { filter = value; }
	  }

	  public ListView.SelectedListViewItemCollection  SelectedItems
    {
      get { return tagList.SelectedItems; }
    }

    public bool MultiSelect
    {
      get { return tagList.MultiSelect; }
      set { tagList.MultiSelect = value; }
    }

    public ITagLibrary TagLibrary
    {
      get { return tagLibrary; }
      set
      {
        tagLibrary = value; 
        if (tagLibrary != null)
        {
          this.SetCurrentDirectory("\\");
        }
      }
    }

		public TagArchiveFileListView()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
      tagList.View = View.Details;
      tagList.SelectedIndexChanged += new EventHandler(tagList_SelectedIndexChanged);
      tagList.DoubleClick += new EventHandler(tagList_DoubleClick);
    }

	  private void tagList_DoubleClick(object sender, EventArgs e)
	  {
	    if (ItemDoubleClicked != null)
        ItemDoubleClicked(sender, e);
	  }

	  private void tagList_SelectedIndexChanged(object sender, EventArgs e)
	  {
	    if (SelectedIndexChanged != null)
        SelectedIndexChanged(sender, e);
	  }

    public event EventHandler ItemDoubleClicked;
    public event EventHandler SelectedIndexChanged;

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
      this.tagList = new BetterListView();
      this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.SuspendLayout();
      // 
      // tagList
      // 
      this.tagList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tagList.Location = new System.Drawing.Point(0, 0);
      this.tagList.Name = "tagList";
      this.tagList.Size = new System.Drawing.Size(336, 320);
      this.tagList.TabIndex = 0;
      this.tagList.View = System.Windows.Forms.View.List;
      this.tagList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tagList_MouseMove);
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Name";
      this.columnHeader1.Width = 140;
      // 
      // TagArchiveFileListView
      // 
      this.Controls.Add(this.tagList);
      this.Name = "TagArchiveFileListView";
      this.Size = new System.Drawing.Size(336, 320);
      this.Load += new System.EventHandler(this.TagArchiveFileListView_Load);
      this.ResumeLayout(false);

    }
		#endregion

    public void SetCurrentDirectory(string path)
    {
      string[] folders = tagLibrary.GetFolderList(path);
      string[] files = tagLibrary.GetFileList(path, this.filter);

      this.tagList.Clear();
      this.tagList.Columns.Clear();
      this.tagList.Columns.AddRange(
        new ColumnHeader[] { this.columnHeader1 } );
      tagList.Columns[0].Width = tagList.Width;

      // TODO: Look at making these methods return empty string arrays, rather than null.
      if (folders != null)
      {
        foreach(string folder in folders)
        {
          CreateFolder(folder);
        }
      }

      if (files != null)
      {
        foreach (string file in files)
        {
          CreateFile(file);
        }
      }
    }

    protected void CreateFile(string path)
    {
      string filename = Path.GetFileName(path);
      ListViewItem i = this.tagList.Items.Add(new ListViewItem(filename, 1)); 
      FileEntryInformation entry = new FileEntryInformation(FileEntryType.File, path);
      i.Tag = entry;
    }

    protected void CreateFolder(string path)
    {
      path = path.TrimStart('\\').TrimEnd('\\');
      string folderName = Path.GetFileNameWithoutExtension(path + ".bin");  // Hack to make it return the highest level folder.
      ListViewItem item = this.tagList.Items.Add(new ListViewItem(folderName, 0));
      FileEntryInformation entry = new FileEntryInformation(FileEntryType.Folder, path);
      item.Tag = entry;
    }

    private void TagArchiveFileListView_Load(object sender, EventArgs e)
    {
      if (!DesignMode)
      {
        tagList.CreateImageList(
          "Prometheus.Icons.App_Basics._16.folder_closed.png",
          "Prometheus.Icons.App_Basics._16.document.png");
      }
    }

    private void tagList_MouseMove(object sender, MouseEventArgs e)
    {
      ListViewItem item = this.tagList.GetItemAt(e.X,e.Y);
      if (item != null)
      {
        toolTip1.SetToolTip(this.tagList, item.Text );        
        toolTip1.Active = true;
      }
      else
      {
        toolTip1.Active = false;
      }
    }
	}
}

