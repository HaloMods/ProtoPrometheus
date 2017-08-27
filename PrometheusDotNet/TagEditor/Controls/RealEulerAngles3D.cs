using System;
using System.Windows.Forms;
using Types = TagLibrary.Types;

namespace TagEditor.Controls
{
	public class RealEulerAngles3D : Field
	{
		private System.Windows.Forms.Label lblName;
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private DevExpress.XtraEditors.TextEdit txtY;
		private DevExpress.XtraEditors.TextEdit txtP;
		private DevExpress.XtraEditors.TextEdit txtL;

		private Types.RealEulerAngles3D value;

		public Types.RealEulerAngles3D Value
		{
			get { return this.value;}
			set { this.value = value; }
		}

		public RealEulerAngles3D()
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
			if (!(value is Types.RealEulerAngles3D))
				throw new Exception("Cannot bind " + value.ToString() + " to RealEulerAngles3D control.");

			this.value = (value as Types.RealEulerAngles3D);
			this.txtY.DataBindings.Clear();
			this.txtY.DataBindings.Add(new Binding("Text", this.value, "Y"));
			this.txtP.DataBindings.Clear();
			this.txtP.DataBindings.Add(new Binding("Text", this.value, "P"));
			this.txtL.DataBindings.Clear();
			this.txtL.DataBindings.Add(new Binding("Text", this.value, "R"));
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
			this.txtY = new DevExpress.XtraEditors.TextEdit();
			this.lblName = new System.Windows.Forms.Label();
			this.txtP = new DevExpress.XtraEditors.TextEdit();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtL = new DevExpress.XtraEditors.TextEdit();
			((System.ComponentModel.ISupportInitialize)(this.txtY.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtP.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtL.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// txtY
			// 
			this.txtY.EditValue = "";
			this.txtY.Location = new System.Drawing.Point(160, 4);
			this.txtY.Name = "txtY";
			this.txtY.Size = new System.Drawing.Size(44, 20);
			this.txtY.TabIndex = 3;
			// 
			// lblName
			// 
			this.lblName.Location = new System.Drawing.Point(4, 5);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(128, 16);
			this.lblName.TabIndex = 2;
			this.lblName.Text = "RealEulerAngles3D";
			this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtP
			// 
			this.txtP.EditValue = "";
			this.txtP.Location = new System.Drawing.Point(224, 4);
			this.txtP.Name = "txtP";
			this.txtP.Size = new System.Drawing.Size(44, 20);
			this.txtP.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(148, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(8, 16);
			this.label1.TabIndex = 5;
			this.label1.Text = "y";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(208, 6);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(8, 16);
			this.label2.TabIndex = 6;
			this.label2.Text = "p";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(276, 6);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(8, 16);
			this.label4.TabIndex = 9;
			this.label4.Text = "l";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtL
			// 
			this.txtL.EditValue = "";
			this.txtL.Location = new System.Drawing.Point(288, 4);
			this.txtL.Name = "txtL";
			this.txtL.Size = new System.Drawing.Size(44, 20);
			this.txtL.TabIndex = 7;
			// 
			// RealEulerAngles3D
			// 
			this.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.Appearance.Options.UseBackColor = true;
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtL);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtP);
			this.Controls.Add(this.txtY);
			this.Controls.Add(this.lblName);
			this.Name = "RealEulerAngles3D";
			this.Size = new System.Drawing.Size(336, 28);
			((System.ComponentModel.ISupportInitialize)(this.txtY.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtP.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtL.Properties)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion


	}
}

