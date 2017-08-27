using System;
using System.Windows.Forms;
using UIControls.StandardControls;
using Types = TagLibrary.Types;

namespace TagEditor.Controls
{
	public class Angle : Field
	{
		private AutoSizeLabel lblName;
		private System.ComponentModel.IContainer components = null;
		private TagEditor.Controls.AngleEdit angLower;
		private DevExpress.XtraEditors.TextEdit txtLower;

		private Types.Angle value;

		public Types.Angle Value
		{
			get { return this.value;}
			set { this.value = value; }
		}

		public Angle()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			this.angLower.AngleChanged += new EventHandler(angLower_AngleChanged);
			this.txtLower.EditValueChanged += new EventHandler(txtLower_EditValueChanged);
		}

		public override void Configure(System.Xml.XmlNode valueNode)
		{
			base.Configure(valueNode);
			this.lblName.Text = Caption;
		}

		public override void DataBind(Types.IField value)
		{
			if (!(value is Types.Angle))
				throw new Exception("Cannot bind " + value.ToString() + " to Angle control.");

			this.value = (value as Types.Angle);
			this.txtLower.DataBindings.Clear();
			this.txtLower.DataBindings.Add(new Binding("Text", this.value, "ValueDegrees"));
		}

		private void angLower_AngleChanged(object sender, EventArgs e)
		{
			//this.txtLower.Text = this.angLower.Angle.ToString();
			this.value.ValueDegrees = this.angLower.Angle;
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
      this.angLower = new TagEditor.Controls.AngleEdit();
      this.txtLower = new DevExpress.XtraEditors.TextEdit();
      ((System.ComponentModel.ISupportInitialize)(this.txtLower.Properties)).BeginInit();
      this.SuspendLayout();
      // 
      // lblName
      // 
      this.lblName.AutoSizeHeight = false;
      this.lblName.Location = new System.Drawing.Point(4, 16);
      this.lblName.Name = "lblName";
      this.lblName.Size = new System.Drawing.Size(128, 16);
      this.lblName.TabIndex = 2;
      this.lblName.Text = "Angle";
      this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // angLower
      // 
      this.angLower.Angle = 0;
      this.angLower.Location = new System.Drawing.Point(192, 12);
      this.angLower.Name = "angLower";
      this.angLower.Size = new System.Drawing.Size(28, 28);
      this.angLower.TabIndex = 7;
      // 
      // txtLower
      // 
      this.txtLower.EditValue = "";
      this.txtLower.Location = new System.Drawing.Point(152, 16);
      this.txtLower.Name = "txtLower";
      this.txtLower.Size = new System.Drawing.Size(32, 20);
      this.txtLower.TabIndex = 9;
      // 
      // Angle
      // 
      this.Appearance.BackColor = System.Drawing.Color.Transparent;
      this.Appearance.Options.UseBackColor = true;
      this.Controls.Add(this.txtLower);
      this.Controls.Add(this.angLower);
      this.Controls.Add(this.lblName);
      this.Name = "Angle";
      this.Size = new System.Drawing.Size(240, 48);
      ((System.ComponentModel.ISupportInitialize)(this.txtLower.Properties)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion
	}
}

