using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using UIControls.StandardControls;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;
using Types = TagLibrary.Types;

namespace TagEditor.Controls
{
  public class Flags : Field
  {
    private AutoSizeLabel lblName;
    private IContainer components = null;
    private TagEditor.Controls.AutoSizeCheckBoxControl checkValue;

    private Types.Flags value;

    public Types.Flags Value
    {
      get { return this.value;}
      set {  this.value = value; }
    }

    public Flags()
    {
      // This call is required by the Windows Form Designer.
      this.AutoResize = true;
      InitializeComponent();
      lblName.AutoSize = true;
      this.checkValue.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(checkValue_ItemCheck);
      this.checkValue.SizeChanged += new EventHandler(checkValue_SizeChanged);
    }

    private void checkValue_SizeChanged(object sender, EventArgs e)
    {
      this.Size = new Size(this.Width, this.checkValue.Height + this.checkValue.Top);
    }

    private void checkValue_ItemCheck(object sender, ItemCheckEventArgs e)
    {
      if (e.State == CheckState.Checked)
      {
        value.SetFlag(e.Index+1, true);
      }
      else
      {
        value.SetFlag(e.Index+1, false);
      }
    }

    public override void Configure(System.Xml.XmlNode valueNode)
    {
      base.Configure(valueNode);
      this.lblName.Text = Name;
      this.checkValue.Items.Clear();
      XmlNodeList items = valueNode.SelectNodes("bit");
      foreach (XmlNode bitNode in items)
      {
        // ex: <bit index="0" name="turns without animating" />
        this.checkValue.Items.Add(
          new DevExpress.XtraEditors.Controls.CheckedListBoxItem(
          bitNode.Attributes["name"].InnerText, false));
      }
    }

    public override void DataBind(Types.IField value)
    {
      if (!(value is Types.Flags))
        throw new Exception("Cannot bind " + value.ToString() + " to Flags control.");

      if (this.value != null)
        this.value.ValueChanged -= new EventHandler(value_ValueChanged);
      this.value = (Types.Flags)value;
      this.value.ValueChanged += new EventHandler(value_ValueChanged);
      UpdateValue();
    }

    private void value_ValueChanged(object sender, EventArgs e)
    {
      UpdateValue();
    }

    internal void UpdateValue()
    {
      // Update the checkboxes.
      for (int x=0; x<this.checkValue.ItemCount; x++)
      {
        this.checkValue.Items[x].CheckState = (this.value.GetFlag(x+1) ? CheckState.Checked : CheckState.Unchecked);
      } 
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose( bool disposing )
    {
      if( disposing )
      {
        if (components != null) 
        {
          components.Dispose();
        }
      }
      base.Dispose( disposing );
    }

    #region Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.lblName = new UIControls.StandardControls.AutoSizeLabel();
      this.checkValue = new TagEditor.Controls.AutoSizeCheckBoxControl();
      ((System.ComponentModel.ISupportInitialize)(this.checkValue)).BeginInit();
      this.SuspendLayout();
      // 
      // lblName
      // 
      this.lblName.AutoSizeHeight = false;
      this.lblName.Location = new System.Drawing.Point(4, 5);
      this.lblName.Name = "lblName";
      this.lblName.Size = new System.Drawing.Size(128, 16);
      this.lblName.TabIndex = 2;
      this.lblName.Text = "Flags";
      this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // checkValue
      // 
      this.checkValue.CheckOnClick = true;
      this.checkValue.ItemHeight = 17;
      this.checkValue.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
                                                                                                new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null)});
      this.checkValue.Location = new System.Drawing.Point(140, 4);
      this.checkValue.Name = "checkValue";
      this.checkValue.Size = new System.Drawing.Size(236, 19);
      this.checkValue.TabIndex = 5;
      // 
      // Flags
      // 
      this.Appearance.BackColor = System.Drawing.Color.Transparent;
      this.Appearance.Options.UseBackColor = true;
      this.Controls.Add(this.checkValue);
      this.Controls.Add(this.lblName);
      this.Name = "Flags";
      this.Size = new System.Drawing.Size(384, 28);
      ((System.ComponentModel.ISupportInitialize)(this.checkValue)).EndInit();
      this.ResumeLayout(false);

    }
    #endregion
  }

  public class AutoSizeCheckBoxControl : DevExpress.XtraEditors.CheckedListBoxControl
  {
    internal AutoSizeCheckBoxControl()
    {
      this.Items.ListChanged += new ListChangedEventHandler(Items_ListChanged);
    }
    private void Items_ListChanged(object sender, ListChangedEventArgs e)
    {
      this.Size = new Size(this.Width, (this.ItemHeight * this.ItemCount + 2));
    }
  }
}