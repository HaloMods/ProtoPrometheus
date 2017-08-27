using System;
using System.Windows.Forms;
using Types = TagLibrary.Types;

namespace TagEditor.Controls
{
	public class RealARGBColor : Field
	{
    private System.Windows.Forms.Label lblName;
		private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private DevExpress.XtraEditors.TextEdit txtA;
    private DevExpress.XtraEditors.TextEdit txtR;
    private DevExpress.XtraEditors.TextEdit txtB;
    private DevExpress.XtraEditors.TextEdit txtG;

    private Types.RealARGBColor value;

    public Types.RealARGBColor Value
    {
      get { return this.value;}
      set { this.value = value; }
    }

		public RealARGBColor()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
		}

    public override void Configure(System.Xml.XmlNode valueNode)
    {
      base.Configure(valueNode);
      this.lblName.Text = Caption;
    }

    public override void DataBind(Types.IField value)
    {
      if (!(value is Types.RealARGBColor))
        throw new Exception("Cannot bind " + value.ToString() + " to RealARGBColor control.");

      this.value = (value as Types.RealARGBColor);
      this.txtA.DataBindings.Clear();
      this.txtA.DataBindings.Add(new Binding("Text", this.value, "A"));
      this.txtR.DataBindings.Clear();
      this.txtR.DataBindings.Add(new Binding("Text", this.value, "R"));
      this.txtG.DataBindings.Clear();
      this.txtG.DataBindings.Add(new Binding("Text", this.value, "G"));
      this.txtB.DataBindings.Clear();
      this.txtB.DataBindings.Add(new Binding("Text", this.value, "B"));
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
      this.txtA = new DevExpress.XtraEditors.TextEdit();
      this.lblName = new System.Windows.Forms.Label();
      this.txtR = new DevExpress.XtraEditors.TextEdit();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.txtB = new DevExpress.XtraEditors.TextEdit();
      this.txtG = new DevExpress.XtraEditors.TextEdit();
      ((System.ComponentModel.ISupportInitialize)(this.txtA.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtR.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtB.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtG.Properties)).BeginInit();
      this.SuspendLayout();
      // 
      // txtA
      // 
      this.txtA.EditValue = "";
      this.txtA.Location = new System.Drawing.Point(160, 4);
      this.txtA.Name = "txtA";
      this.txtA.Size = new System.Drawing.Size(44, 20);
      this.txtA.TabIndex = 3;
      // 
      // lblName
      // 
      this.lblName.Location = new System.Drawing.Point(4, 5);
      this.lblName.Name = "lblName";
      this.lblName.Size = new System.Drawing.Size(128, 16);
      this.lblName.TabIndex = 2;
      this.lblName.Text = "ARGB Color";
      this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // txtR
      // 
      this.txtR.EditValue = "";
      this.txtR.Location = new System.Drawing.Point(224, 4);
      this.txtR.Name = "txtR";
      this.txtR.Size = new System.Drawing.Size(44, 20);
      this.txtR.TabIndex = 4;
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(148, 6);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(8, 16);
      this.label1.TabIndex = 5;
      this.label1.Text = "a";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(212, 6);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(8, 16);
      this.label2.TabIndex = 6;
      this.label2.Text = "r";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(340, 6);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(8, 16);
      this.label3.TabIndex = 10;
      this.label3.Text = "b";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(276, 6);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(8, 16);
      this.label4.TabIndex = 9;
      this.label4.Text = "g";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // txtB
      // 
      this.txtB.EditValue = "";
      this.txtB.Location = new System.Drawing.Point(352, 4);
      this.txtB.Name = "txtB";
      this.txtB.Size = new System.Drawing.Size(44, 20);
      this.txtB.TabIndex = 8;
      // 
      // txtG
      // 
      this.txtG.EditValue = "";
      this.txtG.Location = new System.Drawing.Point(288, 4);
      this.txtG.Name = "txtG";
      this.txtG.Size = new System.Drawing.Size(44, 20);
      this.txtG.TabIndex = 7;
      // 
      // RealARGBColor
      // 
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.txtB);
      this.Controls.Add(this.txtG);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtR);
      this.Controls.Add(this.txtA);
      this.Controls.Add(this.lblName);
      this.Name = "RealARGBColor";
      this.Size = new System.Drawing.Size(404, 28);
      ((System.ComponentModel.ISupportInitialize)(this.txtA.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtR.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtB.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtG.Properties)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion


	}
}

