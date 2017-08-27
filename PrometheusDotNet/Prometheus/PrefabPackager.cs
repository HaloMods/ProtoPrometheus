using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Prometheus
{
	/// <summary>
	/// Summary description for PrefabPackager.
	/// </summary>
	public class PrefabPackager : DevExpress.XtraEditors.XtraForm
	{
    private DevExpress.XtraEditors.TextEdit textEdit1;
    private System.Windows.Forms.Label label1;
    private DevExpress.XtraEditors.MemoEdit memoEdit1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private DevExpress.XtraEditors.ButtonEdit buttonEdit1;
    private DevExpress.XtraEditors.SimpleButton simpleButton1;
    private DevExpress.XtraEditors.TextEdit textEdit2;
    private System.Windows.Forms.Label label4;
    private DevExpress.XtraEditors.GroupControl groupControl1;
    private System.Windows.Forms.RadioButton radioButton1;
    private System.Windows.Forms.RadioButton radioButton2;
    private System.Windows.Forms.RadioButton radioButton3;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private DevExpress.XtraEditors.TextEdit textEdit3;
    private DevExpress.XtraEditors.TextEdit textEdit4;
    private System.Windows.Forms.LinkLabel linkLabel1;
    private System.Windows.Forms.CheckBox checkBox1;
    private System.Windows.Forms.LinkLabel linkLabel2;
    private DevExpress.XtraEditors.SimpleButton simpleButton2;
    private DevExpress.XtraEditors.SimpleButton simpleButton3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PrefabPackager()
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
      this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
      this.label1 = new System.Windows.Forms.Label();
      this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.buttonEdit1 = new DevExpress.XtraEditors.ButtonEdit();
      this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
      this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
      this.label4 = new System.Windows.Forms.Label();
      this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
      this.linkLabel1 = new System.Windows.Forms.LinkLabel();
      this.textEdit4 = new DevExpress.XtraEditors.TextEdit();
      this.textEdit3 = new DevExpress.XtraEditors.TextEdit();
      this.label6 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.radioButton3 = new System.Windows.Forms.RadioButton();
      this.radioButton2 = new System.Windows.Forms.RadioButton();
      this.radioButton1 = new System.Windows.Forms.RadioButton();
      this.checkBox1 = new System.Windows.Forms.CheckBox();
      this.linkLabel2 = new System.Windows.Forms.LinkLabel();
      this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
      this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
      ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
      this.groupControl1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.textEdit4.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).BeginInit();
      this.SuspendLayout();
      // 
      // textEdit1
      // 
      this.textEdit1.EditValue = "Orca";
      this.textEdit1.Location = new System.Drawing.Point(88, 8);
      this.textEdit1.Name = "textEdit1";
      this.textEdit1.Size = new System.Drawing.Size(280, 20);
      this.textEdit1.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
      this.label1.Location = new System.Drawing.Point(24, 8);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(56, 23);
      this.label1.TabIndex = 1;
      this.label1.Text = "Name:";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // memoEdit1
      // 
      this.memoEdit1.EditValue = @"Designed to be the human's main water-based transport, the Orca carries up to 8 Marine passengers, an operator, and a gunner.  It is outfitted with a machine gun in the front, navigation in the back, and protective carbon composite plating along the sides.";
      this.memoEdit1.Location = new System.Drawing.Point(88, 40);
      this.memoEdit1.Name = "memoEdit1";
      this.memoEdit1.Size = new System.Drawing.Size(280, 96);
      this.memoEdit1.TabIndex = 2;
      // 
      // label2
      // 
      this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
      this.label2.Location = new System.Drawing.Point(0, 40);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(80, 23);
      this.label2.TabIndex = 3;
      this.label2.Text = "Description:";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label3
      // 
      this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
      this.label3.Location = new System.Drawing.Point(0, 152);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(80, 23);
      this.label3.TabIndex = 4;
      this.label3.Text = "Root Tag:";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // buttonEdit1
      // 
      this.buttonEdit1.EditValue = "vehicles\\orca\\orca.vehicle";
      this.buttonEdit1.Location = new System.Drawing.Point(88, 152);
      this.buttonEdit1.Name = "buttonEdit1";
      // 
      // buttonEdit1.Properties
      // 
      this.buttonEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                        new DevExpress.XtraEditors.Controls.EditorButton()});
      this.buttonEdit1.Size = new System.Drawing.Size(280, 20);
      this.buttonEdit1.TabIndex = 5;
      // 
      // simpleButton1
      // 
      this.simpleButton1.Location = new System.Drawing.Point(216, 176);
      this.simpleButton1.Name = "simpleButton1";
      this.simpleButton1.Size = new System.Drawing.Size(152, 23);
      this.simpleButton1.TabIndex = 6;
      this.simpleButton1.Text = "Customize Packaged Tags";
      this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
      // 
      // textEdit2
      // 
      this.textEdit2.EditValue = "BobTheBuilder";
      this.textEdit2.Location = new System.Drawing.Point(88, 216);
      this.textEdit2.Name = "textEdit2";
      this.textEdit2.Size = new System.Drawing.Size(120, 20);
      this.textEdit2.TabIndex = 7;
      // 
      // label4
      // 
      this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
      this.label4.Location = new System.Drawing.Point(0, 216);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(80, 23);
      this.label4.TabIndex = 8;
      this.label4.Text = "Creator:";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // groupControl1
      // 
      this.groupControl1.Controls.Add(this.linkLabel1);
      this.groupControl1.Controls.Add(this.textEdit4);
      this.groupControl1.Controls.Add(this.textEdit3);
      this.groupControl1.Controls.Add(this.label6);
      this.groupControl1.Controls.Add(this.label5);
      this.groupControl1.Controls.Add(this.radioButton3);
      this.groupControl1.Controls.Add(this.radioButton2);
      this.groupControl1.Controls.Add(this.radioButton1);
      this.groupControl1.Location = new System.Drawing.Point(8, 248);
      this.groupControl1.Name = "groupControl1";
      this.groupControl1.Size = new System.Drawing.Size(360, 112);
      this.groupControl1.TabIndex = 9;
      this.groupControl1.Text = "Protection";
      // 
      // linkLabel1
      // 
      this.linkLabel1.Location = new System.Drawing.Point(224, 24);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new System.Drawing.Size(136, 23);
      this.linkLabel1.TabIndex = 7;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "Which protection scheme?";
      // 
      // textEdit4
      // 
      this.textEdit4.EditValue = "Num83r5&13773rs";
      this.textEdit4.Location = new System.Drawing.Point(184, 80);
      this.textEdit4.Name = "textEdit4";
      this.textEdit4.Size = new System.Drawing.Size(168, 20);
      this.textEdit4.TabIndex = 6;
      // 
      // textEdit3
      // 
      this.textEdit3.EditValue = "orcapub";
      this.textEdit3.Location = new System.Drawing.Point(184, 56);
      this.textEdit3.Name = "textEdit3";
      this.textEdit3.Size = new System.Drawing.Size(168, 20);
      this.textEdit3.TabIndex = 5;
      // 
      // label6
      // 
      this.label6.Location = new System.Drawing.Point(152, 80);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(32, 23);
      this.label6.TabIndex = 4;
      this.label6.Text = "Pass:";
      this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label5
      // 
      this.label5.Location = new System.Drawing.Point(152, 56);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(32, 23);
      this.label5.TabIndex = 3;
      this.label5.Text = "Pass:";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // radioButton3
      // 
      this.radioButton3.Location = new System.Drawing.Point(16, 80);
      this.radioButton3.Name = "radioButton3";
      this.radioButton3.Size = new System.Drawing.Size(128, 24);
      this.radioButton3.TabIndex = 2;
      this.radioButton3.Text = "Private Use and Edit";
      // 
      // radioButton2
      // 
      this.radioButton2.Location = new System.Drawing.Point(16, 56);
      this.radioButton2.Name = "radioButton2";
      this.radioButton2.TabIndex = 1;
      this.radioButton2.Text = "Private Use";
      // 
      // radioButton1
      // 
      this.radioButton1.Location = new System.Drawing.Point(16, 32);
      this.radioButton1.Name = "radioButton1";
      this.radioButton1.TabIndex = 0;
      this.radioButton1.Text = "No Protection";
      // 
      // checkBox1
      // 
      this.checkBox1.Location = new System.Drawing.Point(16, 376);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new System.Drawing.Size(248, 24);
      this.checkBox1.TabIndex = 10;
      this.checkBox1.Text = "Register Prefab with HaloDev database";
      // 
      // linkLabel2
      // 
      this.linkLabel2.Location = new System.Drawing.Point(272, 376);
      this.linkLabel2.Name = "linkLabel2";
      this.linkLabel2.Size = new System.Drawing.Size(40, 23);
      this.linkLabel2.TabIndex = 8;
      this.linkLabel2.TabStop = true;
      this.linkLabel2.Text = "Why?";
      this.linkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // simpleButton2
      // 
      this.simpleButton2.Location = new System.Drawing.Point(16, 408);
      this.simpleButton2.Name = "simpleButton2";
      this.simpleButton2.Size = new System.Drawing.Size(104, 23);
      this.simpleButton2.TabIndex = 11;
      this.simpleButton2.Text = "Create Prefab";
      // 
      // simpleButton3
      // 
      this.simpleButton3.Location = new System.Drawing.Point(264, 408);
      this.simpleButton3.Name = "simpleButton3";
      this.simpleButton3.Size = new System.Drawing.Size(104, 23);
      this.simpleButton3.TabIndex = 12;
      this.simpleButton3.Text = "Cancel";
      // 
      // PrefabPackager
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
      this.ClientSize = new System.Drawing.Size(376, 438);
      this.Controls.Add(this.simpleButton3);
      this.Controls.Add(this.simpleButton2);
      this.Controls.Add(this.checkBox1);
      this.Controls.Add(this.groupControl1);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.textEdit2);
      this.Controls.Add(this.simpleButton1);
      this.Controls.Add(this.buttonEdit1);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.memoEdit1);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.textEdit1);
      this.Controls.Add(this.linkLabel2);
      this.Name = "PrefabPackager";
      this.Text = "PrefabPackager";
      this.Load += new System.EventHandler(this.PrefabPackager_Load);
      ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
      this.groupControl1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.textEdit4.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

    private void PrefabPackager_Load(object sender, System.EventArgs e)
    {
    
    }

    private void simpleButton1_Click(object sender, System.EventArgs e)
    {
      CustomizePackagedTags dlg = new CustomizePackagedTags();
      dlg.ShowDialog();
    }
	}
}

