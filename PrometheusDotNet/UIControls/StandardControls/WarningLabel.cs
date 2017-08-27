using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace UIControls.StandardControls
{
	/// <summary>
	/// Summary description for WarningLabel.
	/// </summary>
	public class WarningLabel : DevExpress.XtraEditors.XtraUserControl
	{
    private UIControls.StandardControls.ImageLabel imageLabel1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

    public string Caption
    {
      get { return imageLabel1.Captions; }
      set { imageLabel1.Captions = value; }
    }

		public WarningLabel()
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
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(WarningLabel));
      this.imageLabel1 = new UIControls.StandardControls.ImageLabel();
      this.SuspendLayout();
      // 
      // imageLabel1
      // 
      this.imageLabel1.Appearance.BackColor = System.Drawing.Color.IndianRed;
      this.imageLabel1.Appearance.Options.UseBackColor = true;
      this.imageLabel1.Captions = "Type in your warning text here.";
      this.imageLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.imageLabel1.ForegroundColor = System.Drawing.Color.Maroon;
      this.imageLabel1.Image = ((System.Drawing.Image)(resources.GetObject("imageLabel1.Image")));
      this.imageLabel1.Location = new System.Drawing.Point(0, 0);
      this.imageLabel1.Name = "imageLabel1";
      this.imageLabel1.PanelColor = System.Drawing.Color.IndianRed;
      this.imageLabel1.Size = new System.Drawing.Size(272, 144);
      this.imageLabel1.TabIndex = 0;
      // 
      // WarningLabel
      // 
      this.Controls.Add(this.imageLabel1);
      this.Name = "WarningLabel";
      this.Size = new System.Drawing.Size(272, 144);
      this.ResumeLayout(false);

    }
		#endregion
	}
}

