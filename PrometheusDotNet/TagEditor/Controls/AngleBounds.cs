using System;
using System.Windows.Forms;
using UIControls.StandardControls;
using Types = TagLibrary.Types;

namespace TagEditor.Controls
{
	public class AngleBounds : Field
	{
		private AutoSizeLabel lblName;
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private TagEditor.Controls.AngleEdit angLower;
		private TagEditor.Controls.AngleEdit angUpper;
		private DevExpress.XtraEditors.TextEdit txtLower;
		private DevExpress.XtraEditors.TextEdit txtUpper;

		private Types.AngleBounds value;

		public Types.AngleBounds Value
		{
			get { return this.value;}
			set { this.value = value; }
		}

		public AngleBounds()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			this.angLower.AngleChanged += new EventHandler(angLower_AngleChanged);
			this.txtLower.EditValueChanged += new EventHandler(txtLower_EditValueChanged);
			this.angUpper.AngleChanged += new EventHandler(angUpper_AngleChanged);
			this.txtUpper.EditValueChanged += new EventHandler(txtUpper_EditValueChanged);
		}

		public override void Configure(System.Xml.XmlNode valueNode)
		{
			base.Configure(valueNode);
			this.lblName.Text = Caption;
		}

		public override void DataBind(Types.IField value)
		{
			if (!(value is Types.AngleBounds))
				throw new Exception("Cannot bind " + value.ToString() + " to AngleBounds control.");

			this.value = (value as Types.AngleBounds);
			this.txtLower.DataBindings.Clear();
			this.txtLower.DataBindings.Add(new Binding("Text", this.value, "LowerDegrees"));
			this.txtUpper.DataBindings.Clear();
			this.txtUpper.DataBindings.Add(new Binding("Text", this.value, "UpperDegrees"));
		}

		private void angLower_AngleChanged(object sender, EventArgs e)
		{
			//this.txtLower.Text = this.angLower.Angle.ToString();
			this.value.LowerDegrees = this.angLower.Angle;
		}
		private void txtLower_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				this.angLower.Angle = Convert.ToInt32(this.txtLower.Text);
			}
			catch
			{
				this.angLower.Angle = 0;
			}
		}

		private void angUpper_AngleChanged(object sender, EventArgs e)
		{
			//this.txtUpper.Text = this.angUpper.Angle.ToString();
			this.value.UpperDegrees = this.angUpper.Angle;
		}
		private void txtUpper_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				this.angUpper.Angle = Convert.ToInt32(this.txtUpper.Text);
			}
			catch
			{
				this.angUpper.Angle = 0;
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.angLower = new TagEditor.Controls.AngleEdit();
			this.angUpper = new TagEditor.Controls.AngleEdit();
			this.txtLower = new DevExpress.XtraEditors.TextEdit();
			this.txtUpper = new DevExpress.XtraEditors.TextEdit();
			((System.ComponentModel.ISupportInitialize)(this.txtLower.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtUpper.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// lblName
			// 
			this.lblName.AutoSizeHeight = false;
			this.lblName.Location = new System.Drawing.Point(4, 16);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(128, 16);
			this.lblName.TabIndex = 2;
			this.lblName.Text = "AngleBounds";
			this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(152, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(36, 16);
			this.label1.TabIndex = 5;
			this.label1.Text = "lower";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(256, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 16);
			this.label2.TabIndex = 6;
			this.label2.Text = "upper";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// angLower
			// 
			this.angLower.Angle = 0;
			this.angLower.Location = new System.Drawing.Point(192, 8);
			this.angLower.Name = "angLower";
			this.angLower.Size = new System.Drawing.Size(35, 35);
			this.angLower.TabIndex = 7;
			// 
			// angUpper
			// 
			this.angUpper.Angle = 0;
			this.angUpper.Location = new System.Drawing.Point(296, 8);
			this.angUpper.Name = "angUpper";
			this.angUpper.Size = new System.Drawing.Size(35, 35);
			this.angUpper.TabIndex = 8;
			// 
			// txtLower
			// 
			this.txtLower.EditValue = "";
			this.txtLower.Location = new System.Drawing.Point(152, 24);
			this.txtLower.Name = "txtLower";
			this.txtLower.Size = new System.Drawing.Size(32, 20);
			this.txtLower.TabIndex = 9;
			// 
			// txtUpper
			// 
			this.txtUpper.EditValue = "";
			this.txtUpper.Location = new System.Drawing.Point(256, 24);
			this.txtUpper.Name = "txtUpper";
			this.txtUpper.Size = new System.Drawing.Size(32, 20);
			this.txtUpper.TabIndex = 10;
			// 
			// AngleBounds
			// 
			this.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.Appearance.Options.UseBackColor = true;
			this.Controls.Add(this.txtUpper);
			this.Controls.Add(this.txtLower);
			this.Controls.Add(this.angUpper);
			this.Controls.Add(this.angLower);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lblName);
			this.Name = "AngleBounds";
			this.Size = new System.Drawing.Size(344, 48);
			((System.ComponentModel.ISupportInitialize)(this.txtLower.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtUpper.Properties)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
	}
}

