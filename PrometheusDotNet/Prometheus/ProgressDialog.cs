using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Prometheus
{
	/// <summary>
	/// Summary description for ProgressDialog.
	/// </summary>
	public class ProgressDialog : System.Windows.Forms.Form
	{
    private System.Windows.Forms.Label lblText;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.ProgressBar pbProgress;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

    public string HelpText
    {
      get { return lblText.Text; }
      set { lblText.Text = value; }
    }

    public void UpdateProgress(int value, int max)
    {
      pbProgress.Maximum = max;
      UpdateProgress(value);
    }
    public void UpdateProgress(int value)
    {
      pbProgress.Value = value;
    }

		public ProgressDialog()
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
		private void InitializeComponent()
		{
			this.lblText = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.pbProgress = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// lblText
			// 
			this.lblText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblText.Location = new System.Drawing.Point(8, 8);
			this.lblText.Name = "lblText";
			this.lblText.Size = new System.Drawing.Size(304, 24);
			this.lblText.TabIndex = 0;
			this.lblText.Text = "[ insert text here ]";
			this.lblText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(120, 64);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			// 
			// pbProgress
			// 
			this.pbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pbProgress.Location = new System.Drawing.Point(8, 40);
			this.pbProgress.Name = "pbProgress";
			this.pbProgress.Size = new System.Drawing.Size(296, 16);
			this.pbProgress.TabIndex = 2;
			// 
			// ProgressDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(312, 94);
			this.Controls.Add(this.pbProgress);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.lblText);
			this.Name = "ProgressDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.ResumeLayout(false);

		}
		#endregion
	}
}
