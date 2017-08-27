using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Tester
{
	/// <summary>
	/// Summary description for TagEditorParent.
	/// </summary>
	public class TagEditorParent : DevExpress.XtraEditors.XtraForm
	{
    public DevExpress.XtraTab.XtraTabControl xtraTabControl1;
    private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
    private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
    /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TagEditorParent()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
    public void BeginGuiBuild()
    {
      this.SuspendLayout();
    }
    public void EndGuiBuild()
    {
      this.ResumeLayout(false);
    }
    private void InitializeComponent()
		{
      this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
      this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
      this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
      ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
      this.xtraTabControl1.SuspendLayout();
      this.SuspendLayout();
      // 
      // xtraTabControl1
      // 
      this.xtraTabControl1.Controls.Add(this.xtraTabPage1);
      this.xtraTabControl1.Controls.Add(this.xtraTabPage2);
      this.xtraTabControl1.Location = new System.Drawing.Point(16, 8);
      this.xtraTabControl1.Name = "xtraTabControl1";
      this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
      this.xtraTabControl1.Size = new System.Drawing.Size(520, 456);
      this.xtraTabControl1.TabIndex = 0;
      this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
                                                                                    this.xtraTabPage1,
                                                                                    this.xtraTabPage2});
      this.xtraTabControl1.Text = "xtraTabControl1";
      // 
      // xtraTabPage1
      // 
      this.xtraTabPage1.Name = "xtraTabPage1";
      this.xtraTabPage1.Size = new System.Drawing.Size(511, 426);
      this.xtraTabPage1.Text = "xtraTabPage1";
      // 
      // xtraTabPage2
      // 
      this.xtraTabPage2.Name = "xtraTabPage2";
      this.xtraTabPage2.Size = new System.Drawing.Size(0, 0);
      this.xtraTabPage2.Text = "xtraTabPage2";
      // 
      // TagEditorParent
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
      this.ClientSize = new System.Drawing.Size(544, 470);
      this.Controls.Add(this.xtraTabControl1);
      this.Name = "TagEditorParent";
      this.Text = "TagEditorParent";
      ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
      this.xtraTabControl1.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion
	}
}

