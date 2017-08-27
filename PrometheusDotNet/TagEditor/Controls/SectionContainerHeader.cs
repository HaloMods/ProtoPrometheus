using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using UIControls.StandardControls;

namespace Prometheus.TagEditor.Controls
{
	/// <summary>
	/// Summary description for SectionContainerHeader.
	/// </summary>
	public class SectionContainerHeader : XtraUserControl
	{
    private Panel titlePanel;
    private Label lblTitle;
    private AutoSizeLabel lblDescription;
    private Panel mainPanel;
    private System.Windows.Forms.LinkLabel linkBottom;
    private System.Windows.Forms.LinkLabel linkTop;
    private Panel descriptionPanel;
		private Container components = null;
    private WarningLabel warning;

    private string warningText = "";
    private string descriptionText = "";
    private bool moreInfoEnabled = false;

    public string Title
    {
      get { return lblTitle.Text; }
      set 
      {
        lblTitle.Text = value; 
        this.linkTop.Location = new Point(lblTitle.Right + 10, lblTitle.Top);
        SizeDescriptionPanel();
      }
    }

    public string Description
    {
      get { return descriptionText; }
      set
      {
        descriptionText = value;
        if (descriptionText == null) descriptionText = "";
        UpdateInfoEnabled();
        lblDescription.Text = descriptionText; 
      }
    }

    private void UpdateInfoEnabled()
    {
      if ((descriptionText.Length > 0) || (warningText.Length > 0))
      { 
        moreInfoEnabled = true;
      }
      else
      {
        moreInfoEnabled = false;
      }

      linkTop.Visible = moreInfoEnabled;
    }

    public string Warning
    {
      get { return warningText; }
      set
      {
        warningText = value;
        if (warningText == null) warningText = "";
        UpdateInfoEnabled();
        warning = new WarningLabel();
        warning.Caption = warningText;
      }
    }

		public SectionContainerHeader()
		{
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.BackColor = Color.Transparent;

      // This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
      this.descriptionPanel.SizeChanged += new System.EventHandler(descriptionPanel_SizeChanged);
      this.titlePanel.SizeChanged += new EventHandler(titlePanel_SizeChanged);
      this.SizeChanged += new EventHandler(SectionContainerHeader_SizeChanged);
      this.lblTitle.SizeChanged += new EventHandler(lblTitle_SizeChanged);
      this.lblDescription.SizeChanged += new EventHandler(lblDescription_SizeChanged);
      this.lblDescription.AutoSizeHeight = true;
      HideMoreInfo();
		}

	  private void lblDescription_SizeChanged(object sender, EventArgs e)
	  {
	    //this.linkBottom.Top = lblDescription.Bottom+1;
	  }

	  protected void FitControls(Control container)
    {
      int maxY = 0;
      int minY = 0;
      int maxX = 0;
      int minX = 0;
      foreach (Control c in container.Controls)
      {
        if (!c.Visible) continue;
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
      this.mainPanel = new System.Windows.Forms.Panel();
      this.titlePanel = new System.Windows.Forms.Panel();
      this.linkTop = new System.Windows.Forms.LinkLabel();
      this.lblTitle = new System.Windows.Forms.Label();
      this.descriptionPanel = new System.Windows.Forms.Panel();
      this.lblDescription = new UIControls.StandardControls.AutoSizeLabel();
      this.linkBottom = new System.Windows.Forms.LinkLabel();
      this.mainPanel.SuspendLayout();
      this.titlePanel.SuspendLayout();
      this.descriptionPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // mainPanel
      // 
      this.mainPanel.Controls.Add(this.titlePanel);
      this.mainPanel.Controls.Add(this.descriptionPanel);
      this.mainPanel.Location = new System.Drawing.Point(0, 0);
      this.mainPanel.Name = "mainPanel";
      this.mainPanel.Size = new System.Drawing.Size(416, 72);
      this.mainPanel.TabIndex = 7;
      // 
      // titlePanel
      // 
      this.titlePanel.BackColor = System.Drawing.Color.Transparent;
      this.titlePanel.Controls.Add(this.linkTop);
      this.titlePanel.Controls.Add(this.lblTitle);
      this.titlePanel.DockPadding.All = 3;
      this.titlePanel.Location = new System.Drawing.Point(0, 0);
      this.titlePanel.Name = "titlePanel";
      this.titlePanel.Size = new System.Drawing.Size(384, 28);
      this.titlePanel.TabIndex = 5;
      // 
      // linkTop
      // 
      this.linkTop.ActiveLinkColor = System.Drawing.Color.LightGray;
      this.linkTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.linkTop.LinkColor = System.Drawing.Color.DimGray;
      this.linkTop.Location = new System.Drawing.Point(56, 3);
      this.linkTop.Name = "linkTop";
      this.linkTop.Size = new System.Drawing.Size(70, 22);
      this.linkTop.TabIndex = 4;
      this.linkTop.TabStop = true;
      this.linkTop.Text = "More Info...";
      this.linkTop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.linkTop.VisitedLinkColor = System.Drawing.Color.DimGray;
      this.linkTop.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkTop_LinkClicked);
      // 
      // lblTitle
      // 
      this.lblTitle.AutoSize = true;
      this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.lblTitle.Location = new System.Drawing.Point(8, 3);
      this.lblTitle.Name = "lblTitle";
      this.lblTitle.Size = new System.Drawing.Size(39, 22);
      this.lblTitle.TabIndex = 3;
      this.lblTitle.Text = "Title";
      // 
      // descriptionPanel
      // 
      this.descriptionPanel.AutoScroll = true;
      this.descriptionPanel.BackColor = System.Drawing.Color.Transparent;
      this.descriptionPanel.Controls.Add(this.lblDescription);
      this.descriptionPanel.Controls.Add(this.linkBottom);
      this.descriptionPanel.Location = new System.Drawing.Point(0, 28);
      this.descriptionPanel.Name = "descriptionPanel";
      this.descriptionPanel.Size = new System.Drawing.Size(416, 44);
      this.descriptionPanel.TabIndex = 0;
      // 
      // lblDescription
      // 
      this.lblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.lblDescription.AutoSizeHeight = true;
      this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.lblDescription.Location = new System.Drawing.Point(8, 8);
      this.lblDescription.Name = "lblDescription";
      this.lblDescription.Size = new System.Drawing.Size(404, 0);
      this.lblDescription.TabIndex = 2;
      // 
      // linkBottom
      // 
      this.linkBottom.ActiveLinkColor = System.Drawing.Color.LightGray;
      this.linkBottom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.linkBottom.LinkColor = System.Drawing.Color.DimGray;
      this.linkBottom.Location = new System.Drawing.Point(8, 24);
      this.linkBottom.Name = "linkBottom";
      this.linkBottom.Size = new System.Drawing.Size(96, 16);
      this.linkBottom.TabIndex = 6;
      this.linkBottom.TabStop = true;
      this.linkBottom.Text = "Hide More Info...";
      this.linkBottom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.linkBottom.VisitedLinkColor = System.Drawing.Color.DimGray;
      this.linkBottom.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkBottom_LinkClicked);
      // 
      // SectionContainerHeader
      // 
      this.Appearance.BackColor = System.Drawing.Color.Transparent;
      this.Appearance.Options.UseBackColor = true;
      this.Controls.Add(this.mainPanel);
      this.Name = "SectionContainerHeader";
      this.Size = new System.Drawing.Size(420, 72);
      this.mainPanel.ResumeLayout(false);
      this.titlePanel.ResumeLayout(false);
      this.descriptionPanel.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion

    private void lblTitle_SizeChanged(object sender, EventArgs e)
    {
      this.linkTop.Location = new Point(lblTitle.Right + 10, lblTitle.Top);
    }

    private void SectionContainerHeader_SizeChanged(object sender, EventArgs e)
	  {
	    ShowResize("SectionContainerHeader", this);
	  }

    private void SizeDescriptionPanel()
    {
      int height = this.lblDescription.Height + this.linkBottom.Height + 10;
      this.descriptionPanel.Size = new Size(this.descriptionPanel.Width, height);
    }

    private void titlePanel_SizeChanged(object sender, EventArgs e)
    {
      this.descriptionPanel.Location = new Point(0, titlePanel.Bottom+1);
      
      FitControls(mainPanel);
      FitControls(this);
      
      // Update the positions.
      lblTitle.Location = new Point(lblTitle.Left, (titlePanel.Height - lblTitle.Height)/2);
    }

    private void descriptionPanel_SizeChanged(object sender, EventArgs e)
    {
      FitControls(mainPanel);
      FitControls(this);
    }

    protected string GetSize(Control c)
    {
      return String.Format("{0}x{1}",c.Width, c.Height);
    }

    protected void ShowResize(string name, Control c)
    {
      Console.WriteLine(name + " resize: " + GetSize(c));
    }

    protected void ShowMoreInfo()
    {
      linkTop.Visible = false;
      
      if (this.Warning.Length > 0)
      {
        warning = new WarningLabel();
        warning.Size = new Size((titlePanel.Width - lblTitle.Right), 48);
        warning.Location = new Point(this.lblTitle.Right + 25, this.linkTop.Top);
        this.titlePanel.Controls.Add(warning);
      }
      int width = titlePanel.Width;
      FitControls(titlePanel);
      titlePanel.Width = width;

      // Position the controls.
      if (lblDescription.Text.Length > 0)
      {
        linkBottom.Top = lblDescription.Bottom+1;
      }
      else
      {
        linkBottom.Top = 2;
      }
      descriptionPanel.Top = titlePanel.Bottom + 1;
      descriptionPanel.Visible = true;

      descriptionPanel.Height = linkBottom.Bottom + 1;;
      FitControls(mainPanel);
      FitControls(this);
    }

    protected void HideMoreInfo()
    {
      if (warning != null)
      {
        warning.Visible = false;
        if (titlePanel.Controls.Contains(warning))
          titlePanel.Controls.Remove(warning);
        warning = null;
      }

      UpdateInfoEnabled();
      
      lblTitle.Top = linkTop.Top;
      this.titlePanel.Size = new Size(titlePanel.Width, lblTitle.Bottom + lblTitle.Top);
      
      descriptionPanel.Visible = false;
      
      FitControls(mainPanel);
      FitControls(this);
    }

    private void linkTop_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
    {
      ShowMoreInfo();
    }

    private void linkBottom_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
    {
      HideMoreInfo();
    }
  }
}