using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Prometheus
{
	/// <summary>
	/// Summary description for TagExplorer.
	/// </summary>
	public class TagExplorer : System.Windows.Forms.UserControl
	{
    private TD.SandBar.ToolBar toolBar1;
    private TD.SandBar.ButtonItem buttonItem1;
    private TD.SandBar.LabelItem labelItem1;
    private TD.SandBar.ButtonItem buttonItem2;
    private TD.SandBar.ButtonItem buttonItem3;
    private TD.SandBar.ComboBoxItem comboBoxItem1;
    private TD.SandBar.ToolBar toolBar2;
    private TD.SandBar.ButtonItem buttonItem4;
    private TD.SandBar.ButtonItem buttonItem5;
    private TD.SandBar.LabelItem labelItem2;
    private System.Windows.Forms.TreeView treeView1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TagExplorer()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TagExplorer));
      this.toolBar1 = new TD.SandBar.ToolBar();
      this.buttonItem1 = new TD.SandBar.ButtonItem();
      this.labelItem1 = new TD.SandBar.LabelItem();
      this.buttonItem2 = new TD.SandBar.ButtonItem();
      this.buttonItem3 = new TD.SandBar.ButtonItem();
      this.comboBoxItem1 = new TD.SandBar.ComboBoxItem();
      this.toolBar2 = new TD.SandBar.ToolBar();
      this.buttonItem4 = new TD.SandBar.ButtonItem();
      this.buttonItem5 = new TD.SandBar.ButtonItem();
      this.labelItem2 = new TD.SandBar.LabelItem();
      this.treeView1 = new System.Windows.Forms.TreeView();
      this.SuspendLayout();
      // 
      // toolBar1
      // 
      this.toolBar1.AllowHorizontalDock = false;
      this.toolBar1.AllowVerticalDock = false;
      this.toolBar1.Buttons.AddRange(new TD.SandBar.ToolbarItemBase[] {
                                                                        this.labelItem1,
                                                                        this.buttonItem1,
                                                                        this.buttonItem2,
                                                                        this.buttonItem3,
                                                                        this.buttonItem4,
                                                                        this.buttonItem5});
      this.toolBar1.Closable = false;
      this.toolBar1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolBar1.Guid = new System.Guid("532dde82-2815-430f-8a58-68b9380c1ac7");
      this.toolBar1.Location = new System.Drawing.Point(0, 27);
      this.toolBar1.Movable = false;
      this.toolBar1.Name = "toolBar1";
      this.toolBar1.Size = new System.Drawing.Size(168, 26);
      this.toolBar1.TabIndex = 3;
      this.toolBar1.Tearable = false;
      this.toolBar1.Text = "toolBar1";
      this.toolBar1.ButtonClick += new TD.SandBar.ToolBar.ButtonClickEventHandler(this.toolBar1_ButtonClick);
      // 
      // buttonItem1
      // 
      this.buttonItem1.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem1.Image")));
      // 
      // labelItem1
      // 
      this.labelItem1.Text = "View";
      // 
      // buttonItem2
      // 
      this.buttonItem2.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem2.Image")));
      // 
      // buttonItem3
      // 
      this.buttonItem3.BeginGroup = true;
      this.buttonItem3.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem3.Image")));
      // 
      // comboBoxItem1
      // 
      this.comboBoxItem1.ControlWidth = 120;
      this.comboBoxItem1.Padding.Bottom = 0;
      this.comboBoxItem1.Padding.Left = 1;
      this.comboBoxItem1.Padding.Right = 1;
      this.comboBoxItem1.Padding.Top = 0;
      // 
      // toolBar2
      // 
      this.toolBar2.AddRemoveButtonsVisible = false;
      this.toolBar2.Buttons.AddRange(new TD.SandBar.ToolbarItemBase[] {
                                                                        this.labelItem2,
                                                                        this.comboBoxItem1});
      this.toolBar2.Dock = System.Windows.Forms.DockStyle.None;
      this.toolBar2.DrawActionsButton = false;
      this.toolBar2.Guid = new System.Guid("bd0fd5e6-9e32-4df9-8c75-fd08511a7426");
      this.toolBar2.Location = new System.Drawing.Point(0, 0);
      this.toolBar2.Name = "toolBar2";
      this.toolBar2.Size = new System.Drawing.Size(168, 26);
      this.toolBar2.TabIndex = 4;
      this.toolBar2.Text = "toolBar2";
      // 
      // buttonItem4
      // 
      this.buttonItem4.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem4.Image")));
      // 
      // buttonItem5
      // 
      this.buttonItem5.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem5.Image")));
      // 
      // labelItem2
      // 
      this.labelItem2.Text = "Label";
      // 
      // treeView1
      // 
      this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.treeView1.ImageIndex = -1;
      this.treeView1.Location = new System.Drawing.Point(5, 58);
      this.treeView1.Name = "treeView1";
      this.treeView1.SelectedImageIndex = -1;
      this.treeView1.Size = new System.Drawing.Size(155, 254);
      this.treeView1.TabIndex = 5;
      // 
      // TagExplorer
      // 
      this.Controls.Add(this.treeView1);
      this.Controls.Add(this.toolBar2);
      this.Controls.Add(this.toolBar1);
      this.Name = "TagExplorer";
      this.Size = new System.Drawing.Size(168, 320);
      this.Load += new System.EventHandler(this.TagExplorer_Load);
      this.ResumeLayout(false);

    }
		#endregion

    private void toolBar1_ButtonClick(object sender, TD.SandBar.ToolBarItemEventArgs e)
    {
    
    }

    private void TagExplorer_Load(object sender, System.EventArgs e)
    {
    
    }
	}
}
