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
	/// Summary description for ImageLabel.
	/// </summary>
	public class ImageLabel : DevExpress.XtraEditors.XtraUserControl
	{
    private System.Windows.Forms.Label lblText;
    private System.Windows.Forms.PictureBox pictureBox;
		private System.ComponentModel.Container components = null;
    private Color panelColor;
    private Color foregroundColor;

    public Image Image
    {
      get { return pictureBox.Image; }
      set { pictureBox.Image = value; }
    }

    public string Captions
    {
      get { return this.lblText.Text; }
      set { this.lblText.Text = value; }
    }

    public Color PanelColor
    {
      get { return panelColor; }
      set
      {
        panelColor = value;
        this.BackColor = panelColor;
        this.pictureBox.BackColor = panelColor;
      }
    }

    public Color ForegroundColor
    {
      get { return foregroundColor; }
      set
      {
        foregroundColor = value;
        this.lblText.ForeColor = foregroundColor;

      }
    }

		public ImageLabel()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
      this.ResizeRedraw = true;
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
      this.lblText = new System.Windows.Forms.Label();
      this.pictureBox = new System.Windows.Forms.PictureBox();
      this.SuspendLayout();
      // 
      // lblText
      // 
      this.lblText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.lblText.Location = new System.Drawing.Point(48, 8);
      this.lblText.Name = "lblText";
      this.lblText.Size = new System.Drawing.Size(152, 56);
      this.lblText.TabIndex = 0;
      // 
      // pictureBox
      // 
      this.pictureBox.BackColor = System.Drawing.Color.Transparent;
      this.pictureBox.Location = new System.Drawing.Point(8, 8);
      this.pictureBox.Name = "pictureBox";
      this.pictureBox.Size = new System.Drawing.Size(32, 32);
      this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pictureBox.TabIndex = 1;
      this.pictureBox.TabStop = false;
      // 
      // ImageLabel
      // 
      this.Controls.Add(this.pictureBox);
      this.Controls.Add(this.lblText);
      this.Name = "ImageLabel";
      this.Size = new System.Drawing.Size(208, 72);
      this.ResumeLayout(false);

    }
		#endregion


    protected override void OnPaint(PaintEventArgs e)
    {
      e.Graphics.FillRectangle(new SolidBrush(this.panelColor), new Rectangle(0, 0, this.Width, this.Height));
      base.OnPaint (e);
      
      // Draw a border around the control.
      Pen p = new Pen(new SolidBrush(this.foregroundColor), 2f);
      e.Graphics.DrawRectangle(p, 1, 1, this.Width-2, this.Height-2);
    }
	}
}

