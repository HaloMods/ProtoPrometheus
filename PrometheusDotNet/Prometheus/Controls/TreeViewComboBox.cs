using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Prometheus.Core;

namespace Prometheus.Controls
{
	/// <summary>
	/// Encapsulates a dropdown treeview that is part of a file browser dialog.
	/// </summary>
	public class TreeViewComboBox : XtraUserControl
	{
    private ImageComboBoxEdit cboSelectedNodeText;
    private IContainer components = null;
    private ITagLibrary tagLibrary;
    private PopupContainerControl popupContainer;
    private PopupContainerEdit popupContainerEdit1;
    private System.Threading.Timer popupTimer;
    private TagArchiveTree tagTree;

    private bool buttonActionOpenPopup = true;
    private string selectedPath = null;

    public ITagLibrary TagLibrary
    {
      get { return tagLibrary; }
      set
      {
        tagLibrary = value;
        if (tagLibrary != null)
        {
          tagTree.TagLibrary = tagLibrary;
          tagTree.Initialize();
          tagTree.Nodes[0].Text = tagLibrary.Name;
          UpdatePath(tagTree.Nodes[0]);
        }
      }
    }

		public TreeViewComboBox()
		{
			InitializeComponent();

      this.cboSelectedNodeText.Properties.SmallImages =  Utility.GenerateImageList("Prometheus.Icons.App_Basics._16.folder.png");
      this.popupContainerEdit1.Closed += new ClosedEventHandler(popupContainerEdit1_Closed);
      this.popupContainerEdit1.Popup += new EventHandler(popupContainerEdit1_Popup);
      this.popupContainerEdit1.Properties.PopupSizeable = false;
      this.popupContainerEdit1.Properties.PopupFormWidth = this.popupContainerEdit1.Width;
    }

	  private void popupContainerEdit1_Popup(object sender, EventArgs e)
	  {
      // Scroll to the node that matches the selected path.
      tagTree.ScrollToNode(this.selectedPath);
	  }

	  private void popupContainerEdit1_Closed(object sender, ClosedEventArgs e)
	  {
      if (e.CloseMode == PopupCloseMode.Immediate)
      {
        buttonActionOpenPopup = false;
        popupTimer = new System.Threading.Timer(
          new TimerCallback(PopupTimerCallback), null, 100, Timeout.Infinite);
      }
	  }

    private void PopupTimerCallback(object state)
	  {
	    buttonActionOpenPopup = true;
	  }

    public event PathChangedEventHandler PathChanged;
    public delegate void PathChangedEventHandler(object sender, PathChangedEventArgs e);
    public class PathChangedEventArgs : EventArgs
    {
      private string path;
      public string Path
      {
        get { return path; }
      }
      public PathChangedEventArgs(string path)
      {
        this.path = path;
      }
    }

    private void cboSelectedNodeText_ButtonPressed(object sender, ButtonPressedEventArgs e)
    {
      if (!buttonActionOpenPopup)
      {
        this.popupContainerEdit1.ClosePopup();  
      }
      else
      {
        tagTree.IgnoreNodeSelectionEvents = true;
        this.popupContainerEdit1.ShowPopup();  
        tagTree.IgnoreNodeSelectionEvents = false;
      }
    }

    public void SelectPath(string path)
    {
      tagTree.ScrollToNode(path);
    }

	  private void tagTree_NodeSelected(object sender, TreeViewEventArgs e)
	  {
      UpdatePath(e.Node);
      this.popupContainerEdit1.ClosePopup();
      this.ParentForm.Focus();
	  }

    private void UpdatePath(TreeNode node)
    {
      this.selectedPath = (node.Tag as FileEntryInformation).FullPath;
      this.cboSelectedNodeText.Properties.Items.Clear();
      
      ImageComboBoxItem item = new ImageComboBoxItem();
      item.Description = node.Text;
      item.ImageIndex = 0;
      this.cboSelectedNodeText.Properties.Items.Add(item);
      this.cboSelectedNodeText.SelectedIndex = 0;

      if (PathChanged != null)
        PathChanged(this, new PathChangedEventArgs(this.selectedPath));
    }

	  /// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if (popupTimer != null) popupTimer.Dispose();
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
      this.cboSelectedNodeText = new DevExpress.XtraEditors.ImageComboBoxEdit();
      this.popupContainer = new DevExpress.XtraEditors.PopupContainerControl();
      this.tagTree = new Prometheus.Controls.TagArchiveTree();
      this.popupContainerEdit1 = new DevExpress.XtraEditors.PopupContainerEdit();
      ((System.ComponentModel.ISupportInitialize)(this.cboSelectedNodeText.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.popupContainer)).BeginInit();
      this.popupContainer.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.popupContainerEdit1.Properties)).BeginInit();
      this.SuspendLayout();
      // 
      // cboSelectedNodeText
      // 
      this.cboSelectedNodeText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.cboSelectedNodeText.EditValue = "";
      this.cboSelectedNodeText.Location = new System.Drawing.Point(0, 0);
      this.cboSelectedNodeText.Name = "cboSelectedNodeText";
      // 
      // cboSelectedNodeText.Properties
      // 
      this.cboSelectedNodeText.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.cboSelectedNodeText.Properties.ReadOnly = true;
      this.cboSelectedNodeText.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
      this.cboSelectedNodeText.Size = new System.Drawing.Size(336, 20);
      this.cboSelectedNodeText.TabIndex = 0;
      this.cboSelectedNodeText.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cboSelectedNodeText_ButtonPressed);
      // 
      // popupContainer
      // 
      this.popupContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.popupContainer.Appearance.BackColor = System.Drawing.SystemColors.Control;
      this.popupContainer.Appearance.Options.UseBackColor = true;
      this.popupContainer.Controls.Add(this.tagTree);
      this.popupContainer.Location = new System.Drawing.Point(0, 24);
      this.popupContainer.Name = "popupContainer";
      this.popupContainer.Size = new System.Drawing.Size(328, 216);
      this.popupContainer.TabIndex = 1;
      // 
      // tagTree
      // 
      this.tagTree.ActiveTagSource = Prometheus.Controls.FileSource.TagLibrary;
      this.tagTree.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tagTree.HotTracking = true;
      this.tagTree.Location = new System.Drawing.Point(0, 0);
      this.tagTree.MasterArchive = null;
      this.tagTree.Name = "tagTree";
      this.tagTree.PopupMenu = null;
      this.tagTree.ShowFiles = false;
      this.tagTree.Size = new System.Drawing.Size(328, 216);
      this.tagTree.TabIndex = 0;
      this.tagTree.TagLibrary = null;
      this.tagTree.NodeSelected += new System.Windows.Forms.TreeViewEventHandler(this.tagTree_NodeSelected);
      // 
      // popupContainerEdit1
      // 
      this.popupContainerEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.popupContainerEdit1.EditValue = "";
      this.popupContainerEdit1.Location = new System.Drawing.Point(0, 0);
      this.popupContainerEdit1.Name = "popupContainerEdit1";
      // 
      // popupContainerEdit1.Properties
      // 
      this.popupContainerEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.popupContainerEdit1.Properties.PopupControl = this.popupContainer;
      this.popupContainerEdit1.Properties.ShowPopupCloseButton = false;
      this.popupContainerEdit1.Size = new System.Drawing.Size(336, 20);
      this.popupContainerEdit1.TabIndex = 2;
      // 
      // TreeViewComboBox
      // 
      this.Controls.Add(this.popupContainer);
      this.Controls.Add(this.cboSelectedNodeText);
      this.Controls.Add(this.popupContainerEdit1);
      this.Name = "TreeViewComboBox";
      this.Size = new System.Drawing.Size(336, 248);
      ((System.ComponentModel.ISupportInitialize)(this.cboSelectedNodeText.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.popupContainer)).EndInit();
      this.popupContainer.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.popupContainerEdit1.Properties)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

	}
}