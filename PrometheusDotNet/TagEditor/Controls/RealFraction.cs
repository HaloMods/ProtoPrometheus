using System;
using System.Windows.Forms;
using UIControls.StandardControls;
using Types = TagLibrary.Types;

namespace TagEditor.Controls
{
	public class RealFraction : Field
	{
    private DevExpress.XtraEditors.TextEdit txtValue;
    private AutoSizeLabel lblName;
		private System.ComponentModel.IContainer components = null;

    private Types.RealFraction value;

    public Types.RealFraction Value
    {
      get { return this.value;}
      set { this.value = value; }
    }

		public RealFraction()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
      lblName.AutoSizeHeight = true;
		}

    public override void Configure(System.Xml.XmlNode valueNode)
    {
      base.Configure(valueNode);
      this.lblName.Text = Caption;
    }

    public override void DataBind(Types.IField value)
    {
      if (!(value is Types.RealFraction))
        throw new Exception("Cannot bind " + value.GetType().ToString() + " to RealFraction control.");

      this.value = (value as Types.RealFraction);
      this.txtValue.DataBindings.Clear();
      this.txtValue.DataBindings.Add(new Binding("Text", this.value, "Value"));
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
      this.txtValue = new DevExpress.XtraEditors.TextEdit();
      this.lblName = new AutoSizeLabel();
      ((System.ComponentModel.ISupportInitialize)(this.txtValue.Properties)).BeginInit();
      this.SuspendLayout();
      // 
      // txtValue
      // 
      this.txtValue.EditValue = "";
      this.txtValue.Location = new System.Drawing.Point(140, 4);
      this.txtValue.Name = "txtValue";
      this.txtValue.Size = new System.Drawing.Size(120, 20);
      this.txtValue.TabIndex = 3;
      // 
      // lblName
      // 
      this.lblName.Location = new System.Drawing.Point(4, 5);
      this.lblName.Name = "lblName";
      this.lblName.Size = new System.Drawing.Size(128, 16);
      this.lblName.TabIndex = 2;
      this.lblName.Text = "Real";
      this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // Real
      // 
      this.Controls.Add(this.txtValue);
      this.Controls.Add(this.lblName);
      this.Name = "Real";
      ((System.ComponentModel.ISupportInitialize)(this.txtValue.Properties)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion
	}
}

