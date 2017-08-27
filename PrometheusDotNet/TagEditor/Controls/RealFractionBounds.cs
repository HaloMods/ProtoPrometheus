using System;
using System.Windows.Forms;
using UIControls.StandardControls;
using Types = TagLibrary.Types;

namespace TagEditor.Controls
{
	public class RealFractionBounds : Field
	{
		private AutoSizeLabel lblName;
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private DevExpress.XtraEditors.TextEdit txtLower;
		private DevExpress.XtraEditors.TextEdit txtUpper;

		private Types.RealFractionBounds value;

		public Types.RealFractionBounds Value
		{
			get { return this.value;}
			set { this.value = value; }
		}

		public RealFractionBounds()
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
			if (!(value is Types.RealFractionBounds))
				throw new Exception("Cannot bind " + value.ToString() + " to RealFractionBounds control.");

			this.value = (value as Types.RealFractionBounds);
			this.txtLower.DataBindings.Clear();
			this.txtLower.DataBindings.Add(new Binding("Text", this.value, "Lower"));
			this.txtUpper.DataBindings.Clear();
			this.txtUpper.DataBindings.Add(new Binding("Text", this.value, "Upper"));
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
			this.txtLower = new DevExpress.XtraEditors.TextEdit();
			this.lblName = new UIControls.StandardControls.AutoSizeLabel();
			this.txtUpper = new DevExpress.XtraEditors.TextEdit();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.txtLower.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtUpper.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// txtLower
			// 
			this.txtLower.EditValue = "";
			this.txtLower.Location = new System.Drawing.Point(184, 4);
			this.txtLower.Name = "txtLower";
			this.txtLower.Size = new System.Drawing.Size(64, 20);
			this.txtLower.TabIndex = 3;
			// 
			// lblName
			// 
			this.lblName.AutoSizeHeight = false;
			this.lblName.Location = new System.Drawing.Point(4, 5);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(128, 16);
			this.lblName.TabIndex = 2;
			this.lblName.Text = "Real Fraction Bounds";
			this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtUpper
			// 
			this.txtUpper.EditValue = "";
			this.txtUpper.Location = new System.Drawing.Point(296, 4);
			this.txtUpper.Name = "txtUpper";
			this.txtUpper.Size = new System.Drawing.Size(64, 20);
			this.txtUpper.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(148, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(36, 16);
			this.label1.TabIndex = 5;
			this.label1.Text = "lower";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(256, 6);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 16);
			this.label2.TabIndex = 6;
			this.label2.Text = "upper";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// RealFractionBounds
			// 
			this.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.Appearance.Options.UseBackColor = true;
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtUpper);
			this.Controls.Add(this.txtLower);
			this.Controls.Add(this.lblName);
			this.Name = "RealFractionBounds";
			this.Size = new System.Drawing.Size(368, 28);
			((System.ComponentModel.ISupportInitialize)(this.txtLower.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtUpper.Properties)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
	}
}

