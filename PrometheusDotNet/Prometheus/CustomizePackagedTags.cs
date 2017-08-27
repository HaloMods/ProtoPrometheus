using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Prometheus
{
	/// <summary>
	/// Summary description for CustomizePackagedTags.
	/// </summary>
	public class CustomizePackagedTags : DevExpress.XtraEditors.XtraForm
	{
    private System.Windows.Forms.Label label1;
    private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControl1;
    private DevExpress.XtraEditors.SimpleButton simpleButton1;
    private DevExpress.XtraEditors.SimpleButton simpleButton2;
    private DevExpress.XtraEditors.SimpleButton simpleButton3;
    private DevExpress.XtraEditors.SimpleButton simpleButton4;
    private DevExpress.XtraEditors.SimpleButton simpleButton5;
    private DevExpress.XtraEditors.SimpleButton simpleButton6;
    private DevExpress.XtraEditors.SimpleButton simpleButton7;
    private DevExpress.XtraEditors.SimpleButton simpleButton8;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CustomizePackagedTags()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.label1 = new System.Windows.Forms.Label();
      this.checkedListBoxControl1 = new DevExpress.XtraEditors.CheckedListBoxControl();
      this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
      this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
      this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
      this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
      this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
      this.simpleButton6 = new DevExpress.XtraEditors.SimpleButton();
      this.simpleButton7 = new DevExpress.XtraEditors.SimpleButton();
      this.simpleButton8 = new DevExpress.XtraEditors.SimpleButton();
      ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl1)).BeginInit();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(16, 16);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(376, 48);
      this.label1.TabIndex = 0;
      this.label1.Text = "The following will be packaged with the prefab \"Orca\" for distribution.  Dependin" +
        "g on your protection settings, other users may have access to these tags.";
      // 
      // checkedListBoxControl1
      // 
      this.checkedListBoxControl1.ItemHeight = 17;
      this.checkedListBoxControl1.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
                                                                                                            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("vehicles\\orca\\bitmaps\\orca.bitmap", System.Windows.Forms.CheckState.Checked),
                                                                                                            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("vehicles\\warthog\\bitmaps\\metaldetail.bitmap"),
                                                                                                            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("vehicles\\orca\\bitmaps\\orca_nav.bitmap", System.Windows.Forms.CheckState.Checked),
                                                                                                            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("vehicles\\orca\\shaders\\orca.shader_model", System.Windows.Forms.CheckState.Checked),
                                                                                                            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("vehicles\\orca\\orca.gbxmodel", System.Windows.Forms.CheckState.Checked),
                                                                                                            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("vehicles\\orca\\orca.model_animations", System.Windows.Forms.CheckState.Checked),
                                                                                                            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("vehicles\\warthog\\warthog.antenna"),
                                                                                                            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("vehicles\\orca\\orca.physics", System.Windows.Forms.CheckState.Checked),
                                                                                                            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("vehicles\\orca\\shaders\\thrust.shader")});
      this.checkedListBoxControl1.Location = new System.Drawing.Point(8, 64);
      this.checkedListBoxControl1.Name = "checkedListBoxControl1";
      this.checkedListBoxControl1.Size = new System.Drawing.Size(384, 152);
      this.checkedListBoxControl1.TabIndex = 1;
      // 
      // simpleButton1
      // 
      this.simpleButton1.Location = new System.Drawing.Point(8, 224);
      this.simpleButton1.Name = "simpleButton1";
      this.simpleButton1.Size = new System.Drawing.Size(48, 23);
      this.simpleButton1.TabIndex = 2;
      this.simpleButton1.Text = "Add";
      // 
      // simpleButton2
      // 
      this.simpleButton2.Location = new System.Drawing.Point(64, 224);
      this.simpleButton2.Name = "simpleButton2";
      this.simpleButton2.Size = new System.Drawing.Size(72, 23);
      this.simpleButton2.TabIndex = 3;
      this.simpleButton2.Text = "Remove";
      // 
      // simpleButton3
      // 
      this.simpleButton3.Location = new System.Drawing.Point(192, 224);
      this.simpleButton3.Name = "simpleButton3";
      this.simpleButton3.Size = new System.Drawing.Size(72, 23);
      this.simpleButton3.TabIndex = 4;
      this.simpleButton3.Text = "Select All";
      // 
      // simpleButton4
      // 
      this.simpleButton4.Location = new System.Drawing.Point(272, 224);
      this.simpleButton4.Name = "simpleButton4";
      this.simpleButton4.Size = new System.Drawing.Size(120, 23);
      this.simpleButton4.TabIndex = 5;
      this.simpleButton4.Text = "All Dependencies";
      // 
      // simpleButton5
      // 
      this.simpleButton5.Location = new System.Drawing.Point(232, 256);
      this.simpleButton5.Name = "simpleButton5";
      this.simpleButton5.Size = new System.Drawing.Size(160, 23);
      this.simpleButton5.TabIndex = 6;
      this.simpleButton5.Text = "Customized Dependencies";
      // 
      // simpleButton6
      // 
      this.simpleButton6.Location = new System.Drawing.Point(8, 296);
      this.simpleButton6.Name = "simpleButton6";
      this.simpleButton6.Size = new System.Drawing.Size(96, 23);
      this.simpleButton6.TabIndex = 7;
      this.simpleButton6.Text = "Save Changes";
      // 
      // simpleButton7
      // 
      this.simpleButton7.Location = new System.Drawing.Point(200, 296);
      this.simpleButton7.Name = "simpleButton7";
      this.simpleButton7.Size = new System.Drawing.Size(112, 23);
      this.simpleButton7.TabIndex = 8;
      this.simpleButton7.Text = "Clear All Checks";
      // 
      // simpleButton8
      // 
      this.simpleButton8.Location = new System.Drawing.Point(320, 296);
      this.simpleButton8.Name = "simpleButton8";
      this.simpleButton8.Size = new System.Drawing.Size(72, 23);
      this.simpleButton8.TabIndex = 9;
      this.simpleButton8.Text = "Cancel";
      // 
      // CustomizePackagedTags
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
      this.ClientSize = new System.Drawing.Size(400, 326);
      this.Controls.Add(this.simpleButton8);
      this.Controls.Add(this.simpleButton7);
      this.Controls.Add(this.simpleButton6);
      this.Controls.Add(this.simpleButton5);
      this.Controls.Add(this.simpleButton4);
      this.Controls.Add(this.simpleButton3);
      this.Controls.Add(this.simpleButton2);
      this.Controls.Add(this.simpleButton1);
      this.Controls.Add(this.checkedListBoxControl1);
      this.Controls.Add(this.label1);
      this.Name = "CustomizePackagedTags";
      this.Text = "CustomizePackagedTags";
      ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl1)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion
	}
}

