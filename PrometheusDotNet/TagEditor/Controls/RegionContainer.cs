using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Prometheus.Core;
using UIControls.StandardControls;

namespace TagEditor.Controls
{
	/// <summary>
	/// Summary description for RegionContainer.
	/// </summary>
	public class RegionContainer : DevExpress.XtraEditors.XtraUserControl, IFieldContainer
	{
		private ControlListPanel controlPanel;
		private DevExpress.XtraEditors.SimpleButton btnToggleExpand;
		private System.Windows.Forms.Panel pnlHeader;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public string Caption
		{
			get { return this.lblCaption.Text; }
			set { this.lblCaption.Text = value; }
		}

		public bool Collapsed
		{
			get { return collapsed; }
		}

		private bool collapsed = false;
		private System.Windows.Forms.Label lblCaption;
		private int fullHeight;

		public RegionContainer()
		{
			InitializeComponent();
      this.controlPanel.SizeChanged += new EventHandler(controlPanel_SizeChanged);
      this.controlPanel.ResizeBehavior = ResizeBehavior.Both;
      this.fullHeight = this.Height;
      this.Expand();
		}

	  private void controlPanel_SizeChanged(object sender, EventArgs e)
	  {
      FitControls(this);
	  }

    protected void FitControls(Control container)
    {
      int maxY = 0;
      int minY = 0;
      int maxX = 0;
      int minX = 0;
      foreach (Control c in container.Controls)
      {
        if (c.Top < minY) minY = c.Top;
        if (c.Bottom > maxY) maxY = c.Bottom;
        if (c.Left < minX) minX = c.Left;
        if (c.Right > maxX) maxX = c.Right;
      }

      if (container.Size.Height != maxY + minY)
        container.Size = new System.Drawing.Size(maxX, maxY + minY);
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
      this.controlPanel = new UIControls.StandardControls.ControlListPanel();
      this.btnToggleExpand = new DevExpress.XtraEditors.SimpleButton();
      this.lblCaption = new System.Windows.Forms.Label();
      this.pnlHeader = new System.Windows.Forms.Panel();
      this.pnlHeader.SuspendLayout();
      this.SuspendLayout();
      // 
      // controlPanel
      // 
      this.controlPanel.BackColor = System.Drawing.Color.Transparent;
      this.controlPanel.Location = new System.Drawing.Point(24, 32);
      this.controlPanel.Name = "controlPanel";
      this.controlPanel.Padding = 0;
      this.controlPanel.ResizeBehavior = UIControls.StandardControls.ResizeBehavior.None;
      this.controlPanel.Size = new System.Drawing.Size(352, 40);
      this.controlPanel.Spacing = 0;
      this.controlPanel.TabIndex = 0;
      // 
      // btnToggleExpand
      // 
      this.btnToggleExpand.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.btnToggleExpand.Appearance.Options.UseFont = true;
      this.btnToggleExpand.Location = new System.Drawing.Point(0, 8);
      this.btnToggleExpand.Name = "btnToggleExpand";
      this.btnToggleExpand.Size = new System.Drawing.Size(16, 16);
      this.btnToggleExpand.TabIndex = 1;
      this.btnToggleExpand.Text = "-";
      this.btnToggleExpand.Click += new System.EventHandler(this.btnToggleExpand_Click);
      // 
      // lblCaption
      // 
      this.lblCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.lblCaption.BackColor = System.Drawing.Color.Transparent;
      this.lblCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.lblCaption.Location = new System.Drawing.Point(24, 0);
      this.lblCaption.Name = "lblCaption";
      this.lblCaption.Size = new System.Drawing.Size(352, 32);
      this.lblCaption.TabIndex = 0;
      this.lblCaption.Text = "Region Caption";
      this.lblCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pnlHeader
      // 
      this.pnlHeader.Controls.Add(this.btnToggleExpand);
      this.pnlHeader.Controls.Add(this.lblCaption);
      this.pnlHeader.Location = new System.Drawing.Point(0, 0);
      this.pnlHeader.Name = "pnlHeader";
      this.pnlHeader.Size = new System.Drawing.Size(376, 32);
      this.pnlHeader.TabIndex = 2;
      // 
      // RegionContainer
      // 
      this.Controls.Add(this.pnlHeader);
      this.Controls.Add(this.controlPanel);
      this.Name = "RegionContainer";
      this.Size = new System.Drawing.Size(376, 72);
      this.pnlHeader.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint (e);
      
      if (!collapsed)
      {
        // Draw a line in the left edge.
        int top = this.controlPanel.Top+1;
        int bottom = this.controlPanel.Bottom-1;
        int left = this.btnToggleExpand.Left + (this.btnToggleExpand.Width/2);
        Pen p = new Pen(new SolidBrush(Color.DarkGray), 1.0f);
        e.Graphics.DrawLine(p, left, top, left, bottom);
      }
    }

		private void btnToggleExpand_Click(object sender, System.EventArgs e)
		{
      this.Parent.Focus();
			if (collapsed)
			{
        Expand();
			}
			else
			{
        Collapse();
			}
		}

    public void Expand()
    {
      collapsed = false;
      this.Size = new Size(this.Width, this.fullHeight);
      this.btnToggleExpand.Text = "-";
      this.btnToggleExpand.Appearance.Font = 
        new System.Drawing.Font(
          "Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
    }

    public void Collapse()
    {
      collapsed = true;
      this.fullHeight = this.Height;
      this.Size = new Size(this.Width, this.pnlHeader.Height);
      this.btnToggleExpand.Text = "+";
      this.btnToggleExpand.Appearance.Font =
        new System.Drawing.Font(
          "Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
    }

    public Field[] GetChildFields(int levels)
    {
      if (levels > 0) return new Field[0];

      ArrayList fields = new ArrayList();
      foreach (Control c in this.controlPanel.Controls)
      {
        if (c is Field)
        {
          fields.Add(c);
        }
        else if (c is IFieldContainer)
        {
          fields.AddRange((c as IFieldContainer).GetChildFields(levels-1));
        }
      }
      return (fields.ToArray(typeof(Field)) as Field[]);
    }

    public Field[] GetChildFields()
    {
      return GetChildFields(1);
    }

    public void AddField(Field field)
    {
      this.controlPanel.Controls.Add(field);
    }

    public void AddFieldContainer(IFieldContainer container)
    {
      this.controlPanel.Controls.Add(container as Control);
    }
	}
}

