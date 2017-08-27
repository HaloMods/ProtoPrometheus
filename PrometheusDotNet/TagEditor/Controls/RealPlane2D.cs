using System;
using System.Windows.Forms;
using Types = TagLibrary.Types;

namespace TagEditor.Controls
{
	public class RealPlane2D : Field
	{
		private System.Windows.Forms.Label lblName;
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private DevExpress.XtraEditors.TextEdit txtI;
		private DevExpress.XtraEditors.TextEdit txtJ;
		private DevExpress.XtraEditors.TextEdit txtD;

		private Types.RealPlane2D value;

		public Types.RealPlane2D Value
		{
			get { return this.value;}
			set { this.value = value; }
		}

		public RealPlane2D()
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
			if (!(value is Types.RealPlane2D))
				throw new Exception("Cannot bind " + value.ToString() + " to RealPlane2D control.");

			this.value = (value as Types.RealPlane2D);
			this.txtI.DataBindings.Clear();
			this.txtI.DataBindings.Add(new Binding("Text", this.value, "I"));
			this.txtJ.DataBindings.Clear();
			this.txtJ.DataBindings.Add(new Binding("Text", this.value, "J"));
			this.txtD.DataBindings.Clear();
			this.txtD.DataBindings.Add(new Binding("Text", this.value, "D"));
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
			this.txtI = new DevExpress.XtraEditors.TextEdit();
			this.lblName = new System.Windows.Forms.Label();
			this.txtJ = new DevExpress.XtraEditors.TextEdit();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtD = new DevExpress.XtraEditors.TextEdit();
			((System.ComponentModel.ISupportInitialize)(this.txtI.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtJ.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtD.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// txtI
			// 
			this.txtI.EditValue = "";
			this.txtI.Location = new System.Drawing.Point(160, 4);
			this.txtI.Name = "txtI";
			this.txtI.Size = new System.Drawing.Size(44, 20);
			this.txtI.TabIndex = 3;
			// 
			// lblName
			// 
			this.lblName.Location = new System.Drawing.Point(4, 5);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(128, 16);
			this.lblName.TabIndex = 2;
			this.lblName.Text = "RealPlane2D";
			this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtJ
			// 
			this.txtJ.EditValue = "";
			this.txtJ.Location = new System.Drawing.Point(224, 4);
			this.txtJ.Name = "txtJ";
			this.txtJ.Size = new System.Drawing.Size(44, 20);
			this.txtJ.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(148, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(8, 16);
			this.label1.TabIndex = 5;
			this.label1.Text = "i";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(208, 6);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(8, 16);
			this.label2.TabIndex = 6;
			this.label2.Text = "j";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(276, 6);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(8, 16);
			this.label4.TabIndex = 9;
			this.label4.Text = "d";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtD
			// 
			this.txtD.EditValue = "";
			this.txtD.Location = new System.Drawing.Point(288, 4);
			this.txtD.Name = "txtD";
			this.txtD.Size = new System.Drawing.Size(44, 20);
			this.txtD.TabIndex = 7;
			// 
			// RealPlane2D
			// 
			this.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.Appearance.Options.UseBackColor = true;
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtD);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtJ);
			this.Controls.Add(this.txtI);
			this.Controls.Add(this.lblName);
			this.Name = "RealPlane2D";
			this.Size = new System.Drawing.Size(336, 28);
			((System.ComponentModel.ISupportInitialize)(this.txtI.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtJ.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtD.Properties)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion


	}
}

