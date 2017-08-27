using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Prometheus.Core;
using Prometheus.Core.Compiler_Gren;
using DependencyBuilder = Prometheus.Core.Compiler_Gren.DependencyBuilder;
using SharedControls;

namespace Prometheus
{
	/// <summary>
	/// Summary description for PrefabExportDialog.
	/// </summary>
	public class PrefabExportDialog : DevExpress.XtraEditors.XtraForm
	{
    private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControl1;
    private DevExpress.XtraEditors.SimpleButton simpleButtonAddPrefabTag;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.CheckBox checkBox1;
    private DevExpress.XtraEditors.TextEdit textEdit1;
    private System.Windows.Forms.Label label3;
    private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
    private DevExpress.XtraEditors.SimpleButton simpleButton1;
    private System.Windows.Forms.Label label2;
    private DevExpress.XtraEditors.GroupControl groupControl1;
    private DevExpress.XtraEditors.MemoEdit memoEdit1;
    private DevExpress.XtraEditors.TextEdit textEdit2;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PrefabExportDialog()
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
      this.checkedListBoxControl1 = new DevExpress.XtraEditors.CheckedListBoxControl();
      this.simpleButtonAddPrefabTag = new DevExpress.XtraEditors.SimpleButton();
      this.label1 = new System.Windows.Forms.Label();
      this.checkBox1 = new System.Windows.Forms.CheckBox();
      this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
      this.label3 = new System.Windows.Forms.Label();
      this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
      this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
      this.label2 = new System.Windows.Forms.Label();
      this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
      this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
      this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
      this.groupControl1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
      this.SuspendLayout();
      // 
      // checkedListBoxControl1
      // 
      this.checkedListBoxControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.checkedListBoxControl1.ItemHeight = 16;
      this.checkedListBoxControl1.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
                                                                                                            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("\\vehicles\\hoverhog\\hoverhog.vehicle", System.Windows.Forms.CheckState.Checked),
                                                                                                            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("\\vehicles\\hoverhog\\bitmaps\\warthog.bitmap"),
                                                                                                            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("\\vehicles\\hoverhog\\warthog.model_collision_geometry", System.Windows.Forms.CheckState.Checked),
                                                                                                            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("\\vehicles\\hoverhog\\hoverhog.gbxmodel", System.Windows.Forms.CheckState.Checked),
                                                                                                            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("\\vehicles\\hoverhog\\hoverhog.physics", System.Windows.Forms.CheckState.Checked),
                                                                                                            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("\\vehicles\\hoverhog\\bitmaps\\multipurpose.bitmap"),
                                                                                                            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("\\vehicles\\hoverhog\\bitmaps\\detail1.bitmap"),
                                                                                                            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("\\vehicles\\hoverhog\\biotmaps\\multipurpose.antenna"),
                                                                                                            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null),
                                                                                                            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null)});
      this.checkedListBoxControl1.Location = new System.Drawing.Point(8, 56);
      this.checkedListBoxControl1.Name = "checkedListBoxControl1";
      this.checkedListBoxControl1.Size = new System.Drawing.Size(512, 128);
      this.checkedListBoxControl1.TabIndex = 0;
      // 
      // simpleButtonAddPrefabTag
      // 
      this.simpleButtonAddPrefabTag.Location = new System.Drawing.Point(368, 192);
      this.simpleButtonAddPrefabTag.Name = "simpleButtonAddPrefabTag";
      this.simpleButtonAddPrefabTag.Size = new System.Drawing.Size(152, 23);
      this.simpleButtonAddPrefabTag.TabIndex = 1;
      this.simpleButtonAddPrefabTag.Text = "Add Tags to Prefab...";
      this.simpleButtonAddPrefabTag.Click += new System.EventHandler(this.simpleButtonAddPrefabTag_Click);
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(16, 32);
      this.label1.Name = "label1";
      this.label1.TabIndex = 2;
      this.label1.Text = "Prefab Items";
      // 
      // checkBox1
      // 
      this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.checkBox1.Location = new System.Drawing.Point(16, 192);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new System.Drawing.Size(256, 32);
      this.checkBox1.TabIndex = 3;
      this.checkBox1.Text = "Include Modified Dependencies after selection";
      this.checkBox1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // textEdit1
      // 
      this.textEdit1.EditValue = "textEdit1";
      this.textEdit1.Location = new System.Drawing.Point(88, 376);
      this.textEdit1.Name = "textEdit1";
      this.textEdit1.Size = new System.Drawing.Size(128, 20);
      this.textEdit1.TabIndex = 5;
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(16, 376);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(64, 23);
      this.label3.TabIndex = 7;
      this.label3.Text = "Password:";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // comboBoxEdit1
      // 
      this.comboBoxEdit1.EditValue = "Halo 1 PC/CE";
      this.comboBoxEdit1.Location = new System.Drawing.Point(160, 224);
      this.comboBoxEdit1.Name = "comboBoxEdit1";
      // 
      // comboBoxEdit1.Properties
      // 
      this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                          new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.comboBoxEdit1.Properties.Items.AddRange(new object[] {
                                                                  "Halo 1 PC/CE",
                                                                  "Halo 1 Xbox",
                                                                  "Halo 2 Xbox"});
      this.comboBoxEdit1.Size = new System.Drawing.Size(112, 20);
      this.comboBoxEdit1.TabIndex = 8;
      // 
      // simpleButton1
      // 
      this.simpleButton1.Location = new System.Drawing.Point(368, 368);
      this.simpleButton1.Name = "simpleButton1";
      this.simpleButton1.Size = new System.Drawing.Size(168, 32);
      this.simpleButton1.TabIndex = 9;
      this.simpleButton1.Text = "Generate Prefab";
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(80, 224);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(72, 23);
      this.label2.TabIndex = 10;
      this.label2.Text = "Prefab Type:";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // groupControl1
      // 
      this.groupControl1.Controls.Add(this.label1);
      this.groupControl1.Controls.Add(this.checkedListBoxControl1);
      this.groupControl1.Controls.Add(this.simpleButtonAddPrefabTag);
      this.groupControl1.Controls.Add(this.checkBox1);
      this.groupControl1.Controls.Add(this.comboBoxEdit1);
      this.groupControl1.Controls.Add(this.label2);
      this.groupControl1.Location = new System.Drawing.Point(8, 8);
      this.groupControl1.Name = "groupControl1";
      this.groupControl1.Size = new System.Drawing.Size(528, 256);
      this.groupControl1.TabIndex = 11;
      this.groupControl1.Text = "Prefab Tag List";
      // 
      // memoEdit1
      // 
      this.memoEdit1.EditValue = "memoEdit1";
      this.memoEdit1.Location = new System.Drawing.Point(88, 280);
      this.memoEdit1.Name = "memoEdit1";
      this.memoEdit1.Size = new System.Drawing.Size(448, 64);
      this.memoEdit1.TabIndex = 12;
      // 
      // textEdit2
      // 
      this.textEdit2.EditValue = "textEdit2";
      this.textEdit2.Location = new System.Drawing.Point(88, 352);
      this.textEdit2.Name = "textEdit2";
      this.textEdit2.Size = new System.Drawing.Size(128, 20);
      this.textEdit2.TabIndex = 13;
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(32, 352);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(48, 23);
      this.label4.TabIndex = 14;
      this.label4.Text = "Author:";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label5
      // 
      this.label5.Location = new System.Drawing.Point(16, 280);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(64, 23);
      this.label5.TabIndex = 15;
      this.label5.Text = "Description:";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // PrefabExportDialog
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
      this.ClientSize = new System.Drawing.Size(544, 406);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.textEdit2);
      this.Controls.Add(this.memoEdit1);
      this.Controls.Add(this.simpleButton1);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.textEdit1);
      this.Controls.Add(this.groupControl1);
      this.Name = "PrefabExportDialog";
      this.Text = "PrefabExportDialog";
      ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
      this.groupControl1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

    private void simpleButtonAddPrefabTag_Click(object sender, System.EventArgs e)
    {
      TagBrowserDialog dlg = new TagBrowserDialog(MapfileVersion.HALOPC);
      if(dlg.ShowDialog() == DialogResult.OK)
      {
        DependencyBuilder ib = new DependencyBuilder();
        ib.ProcessDependants(dlg.SelectedTag);

        string[] temp = DependencyBuilder.mapfileTagsIndex.RelativePathList;
        for(int i=0; i<temp.Length; i++)
          Trace.WriteLine("Tag Dependency: " + temp[i]);


        Trace.WriteLine("Broken Dependency List");
        string[] broken = DependencyBuilder.brokenDependencies.RelativePathList;
        string[] broken_parents = DependencyBuilder.brokenDependenciesParents.RelativePathList;
        for(int i=0; i<broken.Length; i++)
        {
          Trace.WriteLine("Broken Dependency: " + broken[i]);
        }

        for(int i=0; i<broken_parents.Length; i++)
        {
          if(broken_parents[i] != null)
            Trace.WriteLine("Broken Parent: " + broken_parents[i]);
          else
            Trace.WriteLine("Broken Parent: none");
        }
      }
    }
	}
}

