using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using Prometheus.Core;
using TagLibrary.Types;
using DevExpress.XtraEditors;

namespace TagEditor.Controls
{
	/// <summary>
	/// Summary description for Field.
	/// </summary>
  public class Field : XtraUserControl
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private Container components = null;
    private string boundPropertyName = "";
    private string caption;
    private System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();

    private bool autoResize = false;

    protected bool AutoResize
    {
      get { return autoResize; }
      //set { autoResize = value; }
      set { autoResize = false; }
    }

    public string BoundPropertyName
    {
      get { return boundPropertyName; }
      set { boundPropertyName = value; }
    }
    public string ToolTip
    {
      get { return toolTip.GetToolTip(this); }
      set
      {
        toolTip.SetToolTip(this, value);
        toolTip.Active = true;
      }
    }

    public string Caption
    {
      get { return caption; }
    }

    public Field()
    {
      // This call is required by the Windows.Forms Form Designer.
      //this.AutoScroll = true;
      InitializeComponent();
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
      // 
      // Field
      // 
      this.Name = "Field";
      this.Size = new System.Drawing.Size(264, 28);

    }
    #endregion

        
    protected override void OnPaint(PaintEventArgs e)
    {
      if (AutoResize)
      {
        int maxY = 0;
        int minY = 0;
        foreach (Control c in this.Controls)
        {
          if (c.Bottom > maxY) maxY = c.Bottom;
          if (c.Top < minY) minY = c.Top;
        }

        if (this.Size.Height != maxY + minY)
          this.Size = new System.Drawing.Size(this.Width, maxY + minY);
      }
      base.OnPaint (e);
    }

    public virtual void DataBind(IField value) { ; }

    public virtual Binding[] GetDataBindings()
    {
      return GetDataBindings(this);
    }

    protected virtual Binding[] GetDataBindings(Control control)
    {
      ArrayList bindings = new ArrayList();
      foreach (Control c in control.Controls)
      {
        Binding[] subBindings = GetDataBindings(c);
        bindings.AddRange(subBindings);
      }
      bindings.AddRange(control.DataBindings);
      return (Binding[])(bindings.ToArray(typeof(Binding)));
    }

    public virtual void Configure(XmlNode valueNode)
    {
      try
      {
        this.Name = valueNode.Attributes["name"].InnerText;
        if (valueNode.Attributes["caption"] != null)
        {
          this.caption = Utility.CapitalizeWords(valueNode.Attributes["caption"].InnerText);
        }
        else
        {
          this.caption = Utility.CapitalizeWords(this.Name);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("Unable to create value node: " + ex.Message);
      }
    }
	}
}