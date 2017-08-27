using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using UIControls.StandardControls;
using Types = TagLibrary.Types;

namespace TagEditor.Controls
{
  public class Enum : Field
  {
    private AutoSizeLabel lblName;
    private IContainer components = null;
    private DevExpress.XtraEditors.ComboBoxEdit cboValue;

    private Types.Enum value;

    public Types.Enum Value
    {
      get { return this.value;}
      set { this.value = value; }
    }

    public Enum()
    {
      // This call is required by the Windows Form Designer.
      InitializeComponent();
      lblName.AutoSize = true;
    }

    public override void Configure(System.Xml.XmlNode valueNode)
    {
      base.Configure(valueNode);
      this.lblName.Text = Caption;

      XmlNodeList items = valueNode.SelectNodes("item");
      foreach (XmlNode itemNode in items)
      {
        // ex: <item value="0" name="none" />
        cboValue.Properties.Items.Add(itemNode.Attributes["name"].InnerText);
      }
    }

    public override void DataBind(Types.IField value)
    {
      if (!(value is Types.Enum))
        throw new Exception("Cannot bind " + value.ToString() + " to Enum control.");

      this.value = (Types.Enum)value;
      this.cboValue.DataBindings.Clear();
      this.cboValue.DataBindings.Add(new Binding("SelectedIndex", this.value, "Value"));
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
      this.lblName = new AutoSizeLabel();
      this.cboValue = new DevExpress.XtraEditors.ComboBoxEdit();
      ((System.ComponentModel.ISupportInitialize)(this.cboValue.Properties)).BeginInit();
      this.SuspendLayout();
      // 
      // lblName
      // 
      this.lblName.AutoSizeHeight = false;
      this.lblName.Location = new System.Drawing.Point(4, 5);
      this.lblName.Name = "lblName";
      this.lblName.Size = new System.Drawing.Size(128, 16);
      this.lblName.TabIndex = 2;
      this.lblName.Text = "Enum";
      this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cboValue
      // 
      this.cboValue.EditValue = "";
      this.cboValue.Location = new System.Drawing.Point(140, 4);
      this.cboValue.Name = "cboValue";
      // 
      // cboValue.Properties
      // 
      this.cboValue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                     new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.cboValue.Size = new System.Drawing.Size(236, 20);
      this.cboValue.TabIndex = 4;
      // 
      // Enum
      // 
      this.Appearance.BackColor = System.Drawing.Color.Transparent;
      this.Appearance.Options.UseBackColor = true;
      this.Controls.Add(this.cboValue);
      this.Controls.Add(this.lblName);
      this.Name = "Enum";
      this.Size = new System.Drawing.Size(384, 28);
      ((System.ComponentModel.ISupportInitialize)(this.cboValue.Properties)).EndInit();
      this.ResumeLayout(false);

    }
    #endregion
  }
}