using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Prometheus.Controls
{
	/// <summary>
	/// Summary description for ProgressPanel.
	/// </summary>
	public class ProgressPanel : XtraUserControl
	{
    private Label lblOutput;
    private DevExpress.XtraEditors.PanelControl panelControl1;
    private DevExpress.XtraEditors.ProgressBarControl progressBarControl;
		private Container components = null;

    public string Caption
    {
      get { return lblOutput.Text; }
      set { lblOutput.Text = value; }
    }

    public int ProgressMinimum
    {
      get { return progressBarControl.Properties.Minimum; }
      set { progressBarControl.Properties.Minimum = value ; }
    }

    public int ProgressMaximum
    {
      get { return progressBarControl.Properties.Maximum; }
      set { progressBarControl.Properties.Maximum = value ; }
    }

    public int ProgressValue
    {
      get { return progressBarControl.Position; }
      set { progressBarControl.Position = value; }
    }

		public ProgressPanel()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.BackColor = Color.Transparent;

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
      this.lblOutput = new System.Windows.Forms.Label();
      this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
      this.progressBarControl = new DevExpress.XtraEditors.ProgressBarControl();
      ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
      this.panelControl1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.progressBarControl.Properties)).BeginInit();
      this.SuspendLayout();
      // 
      // lblOutput
      // 
      this.lblOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.lblOutput.Location = new System.Drawing.Point(8, 184);
      this.lblOutput.Name = "lblOutput";
      this.lblOutput.Size = new System.Drawing.Size(208, 28);
      this.lblOutput.TabIndex = 0;
      this.lblOutput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // panelControl1
      // 
      this.panelControl1.Controls.Add(this.progressBarControl);
      this.panelControl1.Controls.Add(this.lblOutput);
      this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelControl1.Location = new System.Drawing.Point(0, 0);
      this.panelControl1.Name = "panelControl1";
      this.panelControl1.Size = new System.Drawing.Size(224, 464);
      this.panelControl1.TabIndex = 2;
      this.panelControl1.Text = "panelControl1";
      // 
      // progressBarControl
      // 
      this.progressBarControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.progressBarControl.Location = new System.Drawing.Point(8, 216);
      this.progressBarControl.Name = "progressBarControl";
      this.progressBarControl.Size = new System.Drawing.Size(208, 14);
      this.progressBarControl.TabIndex = 1;
      this.progressBarControl.TabStop = false;
      // 
      // ProgressPanel
      // 
      this.Appearance.BackColor = System.Drawing.Color.White;
      this.Appearance.Options.UseBackColor = true;
      this.Controls.Add(this.panelControl1);
      this.Name = "ProgressPanel";
      this.Size = new System.Drawing.Size(224, 464);
      ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
      this.panelControl1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.progressBarControl.Properties)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion
	}
}

