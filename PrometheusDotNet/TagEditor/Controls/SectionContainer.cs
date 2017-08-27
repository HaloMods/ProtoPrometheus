using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using TagEditor.Controls;
using UIControls.StandardControls;

namespace Prometheus.TagEditor.Controls
{
	/// <summary>
	/// Summary description for SectionContainer.
	/// </summary>
	public class SectionContainer : XtraUserControl, IFieldContainer
	{
    private PanelControl panel;
		private Container components = null;
    
    private int normalHeight;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.LinkLabel linkMoreInfo;
    private System.Windows.Forms.Label spacerLabel1;
    private AutoSizeLabel lblDescription;
    private System.Windows.Forms.Panel titlePanel;
    private Prometheus.TagEditor.Controls.SectionContainerHeader sectionContainerHeader1;
    private ControlListPanel controlPanel;
    private UIControls.StandardControls.ImageButton imageButton1;

    private int collapsedHeight
    {
      get { return sectionContainerHeader1.Height + sectionContainerHeader1.Top; }
    }

    public string Title
    {
      get { return this.sectionContainerHeader1.Title; }
      set { this.sectionContainerHeader1.Title = value; }
    }

    public string Description
    {
      get { return this.sectionContainerHeader1.Description; }
      set { this.sectionContainerHeader1.Description = value; }
    }

    public int CollapsedHeight
    {
      get { return collapsedHeight; }
    }

    private bool collapsed = false;

    public bool Collapsed
    {
      get { return collapsed; }
    }

		public SectionContainer()
		{
      SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      SetStyle(ControlStyles.DoubleBuffer, true);
      SetStyle(ControlStyles.UserPaint, true);
      SetStyle(ControlStyles.AllPaintingInWmPaint, true);

      this.BackColor = Color.Transparent;

			InitializeComponent();

      this.sectionContainerHeader1.SizeChanged += new EventHandler(sectionContainerHeader1_SizeChanged);
      this.controlPanel.SizeChanged += new EventHandler(controlPanel_SizeChanged);
      this.controlPanel.ResizeBehavior = ResizeBehavior.Both;
		}

	  private void sectionContainerHeader1_SizeChanged(object sender, EventArgs e)
	  {
	    this.controlPanel.Location = new Point(this.controlPanel.Location.X,  this.sectionContainerHeader1.Bottom + 2);
      FitControls(panel);
      FitControls(this);
	  }

	  private void controlPanel_SizeChanged(object sender, EventArgs e)
	  {
	    FitControls(panel);
      this.Size = new Size(panel.Width, panel.Height);
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
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SectionContainer));
      this.panel = new DevExpress.XtraEditors.PanelControl();
      this.imageButton1 = new UIControls.StandardControls.ImageButton();
      this.controlPanel = new UIControls.StandardControls.ControlListPanel();
      this.sectionContainerHeader1 = new Prometheus.TagEditor.Controls.SectionContainerHeader();
      this.titlePanel = new System.Windows.Forms.Panel();
      this.lblDescription = new UIControls.StandardControls.AutoSizeLabel();
      this.label2 = new System.Windows.Forms.Label();
      this.linkMoreInfo = new System.Windows.Forms.LinkLabel();
      this.spacerLabel1 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.panel)).BeginInit();
      this.panel.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel
      // 
      this.panel.Controls.Add(this.imageButton1);
      this.panel.Controls.Add(this.controlPanel);
      this.panel.Controls.Add(this.sectionContainerHeader1);
      this.panel.Location = new System.Drawing.Point(0, 0);
      this.panel.Name = "panel";
      this.panel.Size = new System.Drawing.Size(424, 320);
      this.panel.TabIndex = 0;
      // 
      // imageButton1
      // 
      this.imageButton1.AltImage = ((System.Drawing.Image)(resources.GetObject("imageButton1.AltImage")));
      this.imageButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.imageButton1.Appearance.BackColor = System.Drawing.Color.Transparent;
      this.imageButton1.Appearance.Options.UseBackColor = true;
      this.imageButton1.HotTrack = false;
      this.imageButton1.Image = ((System.Drawing.Image)(resources.GetObject("imageButton1.Image")));
      this.imageButton1.ImageSizeRatio = 0.8F;
      this.imageButton1.Location = new System.Drawing.Point(392, 8);
      this.imageButton1.Name = "imageButton1";
      this.imageButton1.PressedImage = ((System.Drawing.Image)(resources.GetObject("imageButton1.PressedImage")));
      this.imageButton1.Size = new System.Drawing.Size(20, 20);
      this.imageButton1.TabIndex = 4;
      this.imageButton1.Toggle = false;
      this.imageButton1.Click += new System.EventHandler(this.imageButton1_Click);
      // 
      // controlPanel
      // 
      this.controlPanel.Location = new System.Drawing.Point(4, 36);
      this.controlPanel.Name = "controlPanel";
      this.controlPanel.Padding = 10;
      this.controlPanel.ResizeBehavior = UIControls.StandardControls.ResizeBehavior.None;
      this.controlPanel.Size = new System.Drawing.Size(416, 280);
      this.controlPanel.Spacing = 10;
      this.controlPanel.TabIndex = 3;
      // 
      // sectionContainerHeader1
      // 
      this.sectionContainerHeader1.Appearance.BackColor = System.Drawing.Color.Transparent;
      this.sectionContainerHeader1.Appearance.Options.UseBackColor = true;
      this.sectionContainerHeader1.Description = "";
      this.sectionContainerHeader1.Location = new System.Drawing.Point(4, 4);
      this.sectionContainerHeader1.Name = "sectionContainerHeader1";
      this.sectionContainerHeader1.Size = new System.Drawing.Size(416, 28);
      this.sectionContainerHeader1.TabIndex = 2;
      this.sectionContainerHeader1.Title = "";
      this.sectionContainerHeader1.Warning = "";
      // 
      // titlePanel
      // 
      this.titlePanel.Location = new System.Drawing.Point(0, 0);
      this.titlePanel.Name = "titlePanel";
      this.titlePanel.TabIndex = 0;
      // 
      // lblDescription
      // 
      this.lblDescription.AutoSizeHeight = false;
      this.lblDescription.Location = new System.Drawing.Point(0, 0);
      this.lblDescription.Name = "lblDescription";
      this.lblDescription.TabIndex = 0;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Dock = System.Windows.Forms.DockStyle.Left;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.label2.Location = new System.Drawing.Point(3, 3);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(71, 22);
      this.label2.TabIndex = 3;
      this.label2.Text = "Vehicles";
      // 
      // linkMoreInfo
      // 
      this.linkMoreInfo.Dock = System.Windows.Forms.DockStyle.Fill;
      this.linkMoreInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.linkMoreInfo.Location = new System.Drawing.Point(108, 3);
      this.linkMoreInfo.Name = "linkMoreInfo";
      this.linkMoreInfo.Size = new System.Drawing.Size(297, 22);
      this.linkMoreInfo.TabIndex = 4;
      this.linkMoreInfo.TabStop = true;
      this.linkMoreInfo.Text = "More Info...";
      this.linkMoreInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.linkMoreInfo.VisitedLinkColor = System.Drawing.Color.Blue;
      // 
      // spacerLabel1
      // 
      this.spacerLabel1.AutoSize = true;
      this.spacerLabel1.Dock = System.Windows.Forms.DockStyle.Left;
      this.spacerLabel1.Location = new System.Drawing.Point(74, 3);
      this.spacerLabel1.Name = "spacerLabel1";
      this.spacerLabel1.Size = new System.Drawing.Size(0, 16);
      this.spacerLabel1.TabIndex = 5;
      // 
      // SectionContainer
      // 
      this.Appearance.BackColor = System.Drawing.Color.Transparent;
      this.Appearance.Options.UseBackColor = true;
      this.Controls.Add(this.panel);
      this.Name = "SectionContainer";
      this.Size = new System.Drawing.Size(424, 320);
      ((System.ComponentModel.ISupportInitialize)(this.panel)).EndInit();
      this.panel.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
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

    protected void SetHeight()
    {
      this.SuspendLayout();
      if (collapsed)
      {
        panel.Size = new Size(panel.Width, collapsedHeight);
        FitControls(this);
      }
      else
      {
        panel.Size = new Size(panel.Width, normalHeight);
        FitControls(this);
      }
      this.ResumeLayout(true);
    }

    public void ToggleCollapse()
    {
      collapsed = !collapsed;
      if (collapsed) normalHeight = panel.Height;
      SetHeight();
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

    private void imageButton1_Click(object sender, System.EventArgs e)
    {
      ToggleCollapse();
    }
  }
}