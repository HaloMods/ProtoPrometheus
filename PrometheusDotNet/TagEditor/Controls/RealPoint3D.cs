using System;
using System.Windows.Forms;
using Types = TagLibrary.Types;

namespace TagEditor.Controls
{
	public class RealPoint3D : Field
	{
		private System.Windows.Forms.Label lblName;
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private DevExpress.XtraEditors.TextEdit txtX;
		private DevExpress.XtraEditors.TextEdit txtY;
		private DevExpress.XtraEditors.TextEdit txtZ;

		private Types.RealPoint3D value;

		public Types.RealPoint3D Value
		{
			get { return this.value;}
			set { this.value = value; }
		}

		public RealPoint3D()
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
			if (!(value is Types.RealPoint3D))
				throw new Exception("Cannot bind " + value.ToString() + " to RealPoint3D control.");

			this.value = (value as Types.RealPoint3D);
			this.txtX.DataBindings.Clear();
			this.txtX.DataBindings.Add(new Binding("Text", this.value, "X"));
			this.txtY.DataBindings.Clear();
			this.txtY.DataBindings.Add(new Binding("Text", this.value, "Y"));
			this.txtZ.DataBindings.Clear();
			this.txtZ.DataBindings.Add(new Binding("Text", this.value, "Z"));
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
      this.txtX = new DevExpress.XtraEditors.TextEdit();
      this.lblName = new System.Windows.Forms.Label();
      this.txtZ = new DevExpress.XtraEditors.TextEdit();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.txtY = new DevExpress.XtraEditors.TextEdit();
      ((System.ComponentModel.ISupportInitialize)(this.txtX.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtZ.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtY.Properties)).BeginInit();
      this.SuspendLayout();
      // 
      // txtX
      // 
      this.txtX.EditValue = "";
      this.txtX.Location = new System.Drawing.Point(141, 4);
      this.txtX.Name = "txtX";
      this.txtX.Size = new System.Drawing.Size(64, 20);
      this.txtX.TabIndex = 3;
      // 
      // lblName
      // 
      this.lblName.Location = new System.Drawing.Point(4, 5);
      this.lblName.Name = "lblName";
      this.lblName.Size = new System.Drawing.Size(124, 16);
      this.lblName.TabIndex = 2;
      this.lblName.Text = "RealPoint3D";
      this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // txtZ
      // 
      this.txtZ.EditValue = "";
      this.txtZ.Location = new System.Drawing.Point(221, 4);
      this.txtZ.Name = "txtZ";
      this.txtZ.Size = new System.Drawing.Size(64, 20);
      this.txtZ.TabIndex = 4;
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(128, 6);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(8, 16);
      this.label1.TabIndex = 5;
      this.label1.Text = "x";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(211, 6);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(8, 16);
      this.label2.TabIndex = 6;
      this.label2.Text = "y";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(292, 6);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(8, 16);
      this.label4.TabIndex = 9;
      this.label4.Text = "z";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // txtY
      // 
      this.txtY.EditValue = "";
      this.txtY.Location = new System.Drawing.Point(306, 4);
      this.txtY.Name = "txtY";
      this.txtY.Size = new System.Drawing.Size(64, 20);
      this.txtY.TabIndex = 7;
      // 
      // RealPoint3D
      // 
      this.Appearance.BackColor = System.Drawing.Color.Transparent;
      this.Appearance.Options.UseBackColor = true;
      this.Controls.Add(this.label4);
      this.Controls.Add(this.txtY);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtZ);
      this.Controls.Add(this.txtX);
      this.Controls.Add(this.lblName);
      this.Name = "RealPoint3D";
      this.Size = new System.Drawing.Size(384, 28);
      ((System.ComponentModel.ISupportInitialize)(this.txtX.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtZ.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtY.Properties)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion


	}
}

