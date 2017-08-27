using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.DirectX;

using Prometheus.Core.Lightmap;
using Prometheus.Core.Render;
using DevExpress.XtraEditors;

namespace Prometheus
{
	/// <summary>
	/// Summary description for RenderOptions.
	/// </summary>
	public class LightMapDiaglog : DevExpress.XtraEditors.XtraForm
	{
		private Prometheus.Core.Lightmap.Render render;
		private Prometheus.Core.Lightmap.Scene scene;
		private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
		private DevExpress.XtraTab.XtraTabPage pp;
		private DevExpress.XtraTab.XtraTabPage rend;
		private DevExpress.XtraEditors.GroupControl groupControl1;
		private DevExpress.XtraEditors.SimpleButton btn_Rnd;
		private DevExpress.XtraEditors.SimpleButton btn_RndTh;
		private DevExpress.XtraEditors.SimpleButton btn_Res;
		private DevExpress.XtraEditors.SimpleButton btn_Save;
		private DevExpress.XtraEditors.ProgressBarControl pb;
		private System.Windows.Forms.Label lb_pc;
		private System.Windows.Forms.Label lb_bl;
		private System.Windows.Forms.Label lb_pr;
		private DevExpress.XtraEditors.GroupControl grp_ps;
		private DevExpress.XtraEditors.ColorEdit def_Diffuse;
		private System.Windows.Forms.Label lb_dDiff;
		private DevExpress.XtraEditors.SimpleButton btn_Stp;
		private DevExpress.XtraEditors.SpinEdit spin_pc;
		private DevExpress.XtraEditors.SpinEdit spin_pr;
		private DevExpress.XtraEditors.SpinEdit spin_bl;
		private DevExpress.XtraEditors.GroupControl groupControl2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private DevExpress.XtraEditors.GroupControl groupControl3;
		private DevExpress.XtraEditors.SimpleButton simpleButtonGenerateFullBright;
		private DevExpress.XtraEditors.SimpleButton simpleButton1;
		private DevExpress.XtraEditors.GroupControl groupControl4;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label6;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditFilterAlgo;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditSaturationAlgo;
		private DevExpress.XtraEditors.SpinEdit spinEditSaveHistoryDepth;
		private DevExpress.XtraEditors.SpinEdit spinEditPassesBetweenSaves;
		private System.Windows.Forms.CheckBox checkBoxUseAutoSave;
		private DevExpress.XtraEditors.MemoEdit memoEditAvailableBackups;
		private DevExpress.XtraEditors.SpinEdit spinEditRenderPassCount;
		private System.ComponentModel.IContainer components;

		public LightMapDiaglog()
		{
			try
			{
				this.scene = new Scene(MdxRender.GetCurrentBsp);
				//Debug light:
				Vector3 tmpPos = new Vector3(60.0f,-120.0f,20.0f);
				this.scene.AddLight(new LightSource(tmpPos ,1.0,new float[3]{255,255,255}), 
					LightSource.Type.Diffuse);

				InitializeComponent();
				this.spin_pc.Value = (decimal)this.render.MaxPhotons;
				this.spin_pr.Value = (decimal)Render.PhotonRadius;
				this.spin_bl.Value = (decimal)this.render.MaxBounces;
				this.render.SetScene(this.scene);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, "Error in Lightmap Module",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
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
			this.components = new System.ComponentModel.Container();
			this.render = new Prometheus.Core.Lightmap.Render(this.components);
			this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
			this.rend = new DevExpress.XtraTab.XtraTabPage();
			this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
			this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButtonGenerateFullBright = new DevExpress.XtraEditors.SimpleButton();
			this.btn_Rnd = new DevExpress.XtraEditors.SimpleButton();
			this.btn_RndTh = new DevExpress.XtraEditors.SimpleButton();
			this.grp_ps = new DevExpress.XtraEditors.GroupControl();
			this.spinEditRenderPassCount = new DevExpress.XtraEditors.SpinEdit();
			this.label5 = new System.Windows.Forms.Label();
			this.spin_bl = new DevExpress.XtraEditors.SpinEdit();
			this.spin_pr = new DevExpress.XtraEditors.SpinEdit();
			this.spin_pc = new DevExpress.XtraEditors.SpinEdit();
			this.lb_pr = new System.Windows.Forms.Label();
			this.lb_bl = new System.Windows.Forms.Label();
			this.lb_pc = new System.Windows.Forms.Label();
			this.lb_dDiff = new System.Windows.Forms.Label();
			this.def_Diffuse = new DevExpress.XtraEditors.ColorEdit();
			this.pp = new DevExpress.XtraTab.XtraTabPage();
			this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
			this.memoEditAvailableBackups = new DevExpress.XtraEditors.MemoEdit();
			this.label6 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.spinEditSaveHistoryDepth = new DevExpress.XtraEditors.SpinEdit();
			this.label3 = new System.Windows.Forms.Label();
			this.spinEditPassesBetweenSaves = new DevExpress.XtraEditors.SpinEdit();
			this.checkBoxUseAutoSave = new System.Windows.Forms.CheckBox();
			this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBoxEditFilterAlgo = new DevExpress.XtraEditors.ComboBoxEdit();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBoxEditSaturationAlgo = new DevExpress.XtraEditors.ComboBoxEdit();
			this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
			this.btn_Stp = new DevExpress.XtraEditors.SimpleButton();
			this.btn_Save = new DevExpress.XtraEditors.SimpleButton();
			this.btn_Res = new DevExpress.XtraEditors.SimpleButton();
			this.pb = new DevExpress.XtraEditors.ProgressBarControl();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
			this.xtraTabControl1.SuspendLayout();
			this.rend.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
			this.groupControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grp_ps)).BeginInit();
			this.grp_ps.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spinEditRenderPassCount.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spin_bl.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spin_pr.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spin_pc.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.def_Diffuse.Properties)).BeginInit();
			this.pp.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
			this.groupControl4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.memoEditAvailableBackups.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spinEditSaveHistoryDepth.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spinEditPassesBetweenSaves.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
			this.groupControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditFilterAlgo.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSaturationAlgo.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
			this.groupControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pb.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// render
			// 
			this.render.MaxBounces = 1;
			this.render.MaxPhotons = 1000;
			this.render.ProgressUpdate += new Prometheus.Core.Lightmap.Render.ProgressChangeHandler(this.render_ProgressUpdate);
			// 
			// xtraTabControl1
			// 
			this.xtraTabControl1.Controls.Add(this.rend);
			this.xtraTabControl1.Controls.Add(this.pp);
			this.xtraTabControl1.Location = new System.Drawing.Point(8, 8);
			this.xtraTabControl1.Name = "xtraTabControl1";
			this.xtraTabControl1.SelectedTabPage = this.rend;
			this.xtraTabControl1.Size = new System.Drawing.Size(512, 279);
			this.xtraTabControl1.TabIndex = 0;
			this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
																							this.rend,
																							this.pp});
			this.xtraTabControl1.Text = "xtraTabControl1";
			// 
			// rend
			// 
			this.rend.Controls.Add(this.groupControl3);
			this.rend.Controls.Add(this.grp_ps);
			this.rend.Name = "rend";
			this.rend.Size = new System.Drawing.Size(506, 253);
			this.rend.Text = "Rendering";
			// 
			// groupControl3
			// 
			this.groupControl3.Controls.Add(this.simpleButton1);
			this.groupControl3.Controls.Add(this.simpleButtonGenerateFullBright);
			this.groupControl3.Controls.Add(this.btn_Rnd);
			this.groupControl3.Controls.Add(this.btn_RndTh);
			this.groupControl3.Location = new System.Drawing.Point(267, 7);
			this.groupControl3.Name = "groupControl3";
			this.groupControl3.Size = new System.Drawing.Size(226, 231);
			this.groupControl3.TabIndex = 4;
			this.groupControl3.Text = "Lightmap Generation";
			// 
			// simpleButton1
			// 
			this.simpleButton1.Location = new System.Drawing.Point(13, 56);
			this.simpleButton1.Name = "simpleButton1";
			this.simpleButton1.Size = new System.Drawing.Size(134, 28);
			this.simpleButton1.TabIndex = 1;
			this.simpleButton1.Text = "BSP Shadow Map";
			// 
			// simpleButtonGenerateFullBright
			// 
			this.simpleButtonGenerateFullBright.Location = new System.Drawing.Point(13, 21);
			this.simpleButtonGenerateFullBright.Name = "simpleButtonGenerateFullBright";
			this.simpleButtonGenerateFullBright.Size = new System.Drawing.Size(134, 28);
			this.simpleButtonGenerateFullBright.TabIndex = 0;
			this.simpleButtonGenerateFullBright.Text = "Generate Full Bright";
			// 
			// btn_Rnd
			// 
			this.btn_Rnd.Location = new System.Drawing.Point(13, 91);
			this.btn_Rnd.Name = "btn_Rnd";
			this.btn_Rnd.Size = new System.Drawing.Size(194, 28);
			this.btn_Rnd.TabIndex = 2;
			this.btn_Rnd.Text = "Create Photon Map (Silent Mode)";
			this.btn_Rnd.Click += new System.EventHandler(this.btn_Rnd_Click);
			// 
			// btn_RndTh
			// 
			this.btn_RndTh.Location = new System.Drawing.Point(13, 126);
			this.btn_RndTh.Name = "btn_RndTh";
			this.btn_RndTh.Size = new System.Drawing.Size(194, 28);
			this.btn_RndTh.TabIndex = 3;
			this.btn_RndTh.Text = "Create Photon Map (Preview Mode)";
			this.btn_RndTh.Click += new System.EventHandler(this.btn_RndTh_Click);
			// 
			// grp_ps
			// 
			this.grp_ps.Controls.Add(this.spinEditRenderPassCount);
			this.grp_ps.Controls.Add(this.label5);
			this.grp_ps.Controls.Add(this.spin_bl);
			this.grp_ps.Controls.Add(this.spin_pr);
			this.grp_ps.Controls.Add(this.spin_pc);
			this.grp_ps.Controls.Add(this.lb_pr);
			this.grp_ps.Controls.Add(this.lb_bl);
			this.grp_ps.Controls.Add(this.lb_pc);
			this.grp_ps.Controls.Add(this.lb_dDiff);
			this.grp_ps.Controls.Add(this.def_Diffuse);
			this.grp_ps.Location = new System.Drawing.Point(7, 7);
			this.grp_ps.Name = "grp_ps";
			this.grp_ps.Size = new System.Drawing.Size(253, 231);
			this.grp_ps.TabIndex = 3;
			this.grp_ps.Text = "Photon Mapping Settings";
			// 
			// spinEditRenderPassCount
			// 
			this.spinEditRenderPassCount.EditValue = new System.Decimal(new int[] {
																					  0,
																					  0,
																					  0,
																					  0});
			this.spinEditRenderPassCount.Location = new System.Drawing.Point(140, 106);
			this.spinEditRenderPassCount.Name = "spinEditRenderPassCount";
			// 
			// spinEditRenderPassCount.Properties
			// 
			this.spinEditRenderPassCount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
																															new DevExpress.XtraEditors.Controls.EditorButton()});
			this.spinEditRenderPassCount.Properties.UseCtrlIncrement = false;
			this.spinEditRenderPassCount.Size = new System.Drawing.Size(80, 20);
			this.spinEditRenderPassCount.TabIndex = 8;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(53, 106);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(87, 17);
			this.label5.TabIndex = 7;
			this.label5.Text = "Render Passes:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// spin_bl
			// 
			this.spin_bl.EditValue = new System.Decimal(new int[] {
																	  0,
																	  0,
																	  0,
																	  0});
			this.spin_bl.Location = new System.Drawing.Point(140, 77);
			this.spin_bl.Name = "spin_bl";
			// 
			// spin_bl.Properties
			// 
			this.spin_bl.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
																											new DevExpress.XtraEditors.Controls.EditorButton()});
			this.spin_bl.Properties.UseCtrlIncrement = false;
			this.spin_bl.Size = new System.Drawing.Size(80, 20);
			this.spin_bl.TabIndex = 6;
			// 
			// spin_pr
			// 
			this.spin_pr.EditValue = new System.Decimal(new int[] {
																	  0,
																	  0,
																	  0,
																	  0});
			this.spin_pr.Location = new System.Drawing.Point(140, 49);
			this.spin_pr.Name = "spin_pr";
			// 
			// spin_pr.Properties
			// 
			this.spin_pr.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
																											new DevExpress.XtraEditors.Controls.EditorButton()});
			this.spin_pr.Properties.UseCtrlIncrement = false;
			this.spin_pr.Size = new System.Drawing.Size(80, 20);
			this.spin_pr.TabIndex = 5;
			// 
			// spin_pc
			// 
			this.spin_pc.EditValue = new System.Decimal(new int[] {
																	  0,
																	  0,
																	  0,
																	  0});
			this.spin_pc.Location = new System.Drawing.Point(140, 21);
			this.spin_pc.Name = "spin_pc";
			// 
			// spin_pc.Properties
			// 
			this.spin_pc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
																											new DevExpress.XtraEditors.Controls.EditorButton()});
			this.spin_pc.Properties.UseCtrlIncrement = false;
			this.spin_pc.Size = new System.Drawing.Size(80, 20);
			this.spin_pc.TabIndex = 4;
			// 
			// lb_pr
			// 
			this.lb_pr.Location = new System.Drawing.Point(27, 49);
			this.lb_pr.Name = "lb_pr";
			this.lb_pr.Size = new System.Drawing.Size(111, 16);
			this.lb_pr.TabIndex = 2;
			this.lb_pr.Text = "Photon Texel Radius:";
			this.lb_pr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lb_bl
			// 
			this.lb_bl.Location = new System.Drawing.Point(64, 77);
			this.lb_bl.Name = "lb_bl";
			this.lb_bl.Size = new System.Drawing.Size(72, 17);
			this.lb_bl.TabIndex = 1;
			this.lb_bl.Text = "Bounce Limit:";
			this.lb_bl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lb_pc
			// 
			this.lb_pc.Location = new System.Drawing.Point(60, 21);
			this.lb_pc.Name = "lb_pc";
			this.lb_pc.Size = new System.Drawing.Size(80, 17);
			this.lb_pc.TabIndex = 0;
			this.lb_pc.Text = "Photon Count:";
			this.lb_pc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lb_dDiff
			// 
			this.lb_dDiff.Location = new System.Drawing.Point(20, 133);
			this.lb_dDiff.Name = "lb_dDiff";
			this.lb_dDiff.Size = new System.Drawing.Size(120, 17);
			this.lb_dDiff.TabIndex = 2;
			this.lb_dDiff.Text = "Default Diffuse Colour:";
			this.lb_dDiff.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lb_dDiff.Click += new System.EventHandler(this.lb_dDiff_Click);
			// 
			// def_Diffuse
			// 
			this.def_Diffuse.EditValue = System.Drawing.Color.White;
			this.def_Diffuse.Location = new System.Drawing.Point(140, 133);
			this.def_Diffuse.Name = "def_Diffuse";
			// 
			// def_Diffuse.Properties
			// 
			this.def_Diffuse.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
																												new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.def_Diffuse.Size = new System.Drawing.Size(80, 20);
			this.def_Diffuse.TabIndex = 0;
			this.def_Diffuse.EditValueChanged += new System.EventHandler(this.def_Diffuse_EditValueChanged);
			// 
			// pp
			// 
			this.pp.Controls.Add(this.groupControl4);
			this.pp.Controls.Add(this.groupControl2);
			this.pp.Name = "pp";
			this.pp.Size = new System.Drawing.Size(506, 253);
			this.pp.Text = "Post Processing";
			// 
			// groupControl4
			// 
			this.groupControl4.Controls.Add(this.memoEditAvailableBackups);
			this.groupControl4.Controls.Add(this.label6);
			this.groupControl4.Controls.Add(this.label4);
			this.groupControl4.Controls.Add(this.spinEditSaveHistoryDepth);
			this.groupControl4.Controls.Add(this.label3);
			this.groupControl4.Controls.Add(this.spinEditPassesBetweenSaves);
			this.groupControl4.Controls.Add(this.checkBoxUseAutoSave);
			this.groupControl4.Location = new System.Drawing.Point(7, 105);
			this.groupControl4.Name = "groupControl4";
			this.groupControl4.Size = new System.Drawing.Size(486, 140);
			this.groupControl4.TabIndex = 6;
			this.groupControl4.Text = "Auto Save Settings";
			// 
			// memoEditAvailableBackups
			// 
			this.memoEditAvailableBackups.EditValue = "";
			this.memoEditAvailableBackups.Location = new System.Drawing.Point(207, 42);
			this.memoEditAvailableBackups.Name = "memoEditAvailableBackups";
			// 
			// memoEditAvailableBackups.Properties
			// 
			this.memoEditAvailableBackups.Properties.ReadOnly = true;
			this.memoEditAvailableBackups.Size = new System.Drawing.Size(273, 91);
			this.memoEditAvailableBackups.TabIndex = 7;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(207, 21);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(120, 20);
			this.label6.TabIndex = 6;
			this.label6.Text = "Available Backups:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(13, 84);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(107, 20);
			this.label4.TabIndex = 4;
			this.label4.Text = "Save History Depth:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// spinEditSaveHistoryDepth
			// 
			this.spinEditSaveHistoryDepth.EditValue = new System.Decimal(new int[] {
																					   0,
																					   0,
																					   0,
																					   0});
			this.spinEditSaveHistoryDepth.Location = new System.Drawing.Point(133, 84);
			this.spinEditSaveHistoryDepth.Name = "spinEditSaveHistoryDepth";
			// 
			// spinEditSaveHistoryDepth.Properties
			// 
			this.spinEditSaveHistoryDepth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
																															 new DevExpress.XtraEditors.Controls.EditorButton()});
			this.spinEditSaveHistoryDepth.Properties.UseCtrlIncrement = false;
			this.spinEditSaveHistoryDepth.Size = new System.Drawing.Size(40, 20);
			this.spinEditSaveHistoryDepth.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(27, 49);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(93, 28);
			this.label3.TabIndex = 2;
			this.label3.Text = "Number of Passes Between Saves:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// spinEditPassesBetweenSaves
			// 
			this.spinEditPassesBetweenSaves.EditValue = new System.Decimal(new int[] {
																						 0,
																						 0,
																						 0,
																						 0});
			this.spinEditPassesBetweenSaves.Location = new System.Drawing.Point(133, 56);
			this.spinEditPassesBetweenSaves.Name = "spinEditPassesBetweenSaves";
			// 
			// spinEditPassesBetweenSaves.Properties
			// 
			this.spinEditPassesBetweenSaves.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
																															   new DevExpress.XtraEditors.Controls.EditorButton()});
			this.spinEditPassesBetweenSaves.Properties.UseCtrlIncrement = false;
			this.spinEditPassesBetweenSaves.Size = new System.Drawing.Size(40, 20);
			this.spinEditPassesBetweenSaves.TabIndex = 1;
			// 
			// checkBoxUseAutoSave
			// 
			this.checkBoxUseAutoSave.Location = new System.Drawing.Point(13, 21);
			this.checkBoxUseAutoSave.Name = "checkBoxUseAutoSave";
			this.checkBoxUseAutoSave.Size = new System.Drawing.Size(107, 21);
			this.checkBoxUseAutoSave.TabIndex = 0;
			this.checkBoxUseAutoSave.Text = "Use Auto Save";
			// 
			// groupControl2
			// 
			this.groupControl2.Controls.Add(this.button1);
			this.groupControl2.Controls.Add(this.label1);
			this.groupControl2.Controls.Add(this.comboBoxEditFilterAlgo);
			this.groupControl2.Controls.Add(this.label2);
			this.groupControl2.Controls.Add(this.comboBoxEditSaturationAlgo);
			this.groupControl2.Location = new System.Drawing.Point(7, 14);
			this.groupControl2.Name = "groupControl2";
			this.groupControl2.Size = new System.Drawing.Size(486, 84);
			this.groupControl2.TabIndex = 0;
			this.groupControl2.Text = "Texture Generation";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(280, 28);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(187, 21);
			this.button1.TabIndex = 13;
			this.button1.Text = "Update Lightmap Post Processing";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(20, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(113, 20);
			this.label1.TabIndex = 12;
			this.label1.Text = "Filter Algorithm:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboBoxEditFilterAlgo
			// 
			this.comboBoxEditFilterAlgo.EditValue = "No Filtering";
			this.comboBoxEditFilterAlgo.Location = new System.Drawing.Point(133, 56);
			this.comboBoxEditFilterAlgo.Name = "comboBoxEditFilterAlgo";
			// 
			// comboBoxEditFilterAlgo.Properties
			// 
			this.comboBoxEditFilterAlgo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
																														   new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditFilterAlgo.Properties.Items.AddRange(new object[] {
																				   "No Filtering",
																				   "3x3 Gaussian",
																				   "5x5 Gaussian"});
			this.comboBoxEditFilterAlgo.ShowToolTips = false;
			this.comboBoxEditFilterAlgo.Size = new System.Drawing.Size(127, 20);
			this.comboBoxEditFilterAlgo.TabIndex = 11;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(20, 28);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(113, 20);
			this.label2.TabIndex = 10;
			this.label2.Text = "Saturation Algorithm:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboBoxEditSaturationAlgo
			// 
			this.comboBoxEditSaturationAlgo.EditValue = "Use Scene Maximum";
			this.comboBoxEditSaturationAlgo.Location = new System.Drawing.Point(133, 28);
			this.comboBoxEditSaturationAlgo.Name = "comboBoxEditSaturationAlgo";
			// 
			// comboBoxEditSaturationAlgo.Properties
			// 
			this.comboBoxEditSaturationAlgo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
																															   new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditSaturationAlgo.Properties.Items.AddRange(new object[] {
																					   "Use Scene Maximum",
																					   "Scene Mean + 3 StdDev",
																					   "Scene Mean + 2 StdDev",
																					   "Scene Mean + 1 StdDev"});
			this.comboBoxEditSaturationAlgo.Size = new System.Drawing.Size(127, 20);
			this.comboBoxEditSaturationAlgo.TabIndex = 9;
			// 
			// groupControl1
			// 
			this.groupControl1.Controls.Add(this.btn_Stp);
			this.groupControl1.Controls.Add(this.btn_Save);
			this.groupControl1.Controls.Add(this.btn_Res);
			this.groupControl1.Controls.Add(this.pb);
			this.groupControl1.Location = new System.Drawing.Point(8, 294);
			this.groupControl1.Name = "groupControl1";
			this.groupControl1.Size = new System.Drawing.Size(512, 80);
			this.groupControl1.TabIndex = 1;
			this.groupControl1.Text = "Progress";
			// 
			// btn_Stp
			// 
			this.btn_Stp.Enabled = false;
			this.btn_Stp.Location = new System.Drawing.Point(93, 48);
			this.btn_Stp.Name = "btn_Stp";
			this.btn_Stp.TabIndex = 3;
			this.btn_Stp.Text = "Stop";
			this.btn_Stp.Click += new System.EventHandler(this.btn_Stp_Click);
			// 
			// btn_Save
			// 
			this.btn_Save.Enabled = false;
			this.btn_Save.Location = new System.Drawing.Point(180, 49);
			this.btn_Save.Name = "btn_Save";
			this.btn_Save.Size = new System.Drawing.Size(112, 23);
			this.btn_Save.TabIndex = 2;
			this.btn_Save.Text = "Save Progress";
			this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
			// 
			// btn_Res
			// 
			this.btn_Res.Enabled = false;
			this.btn_Res.Location = new System.Drawing.Point(8, 48);
			this.btn_Res.Name = "btn_Res";
			this.btn_Res.TabIndex = 1;
			this.btn_Res.Text = "Resume";
			// 
			// pb
			// 
			this.pb.Location = new System.Drawing.Point(7, 21);
			this.pb.Name = "pb";
			this.pb.Size = new System.Drawing.Size(500, 14);
			this.pb.TabIndex = 0;
			this.pb.TabStop = false;
			// 
			// LightMapDiaglog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(528, 388);
			this.Controls.Add(this.groupControl1);
			this.Controls.Add(this.xtraTabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "LightMapDiaglog";
			this.Text = "Lightmapping Options";
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
			this.xtraTabControl1.ResumeLayout(false);
			this.rend.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
			this.groupControl3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grp_ps)).EndInit();
			this.grp_ps.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spinEditRenderPassCount.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spin_bl.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spin_pr.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spin_pc.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.def_Diffuse.Properties)).EndInit();
			this.pp.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
			this.groupControl4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.memoEditAvailableBackups.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spinEditSaveHistoryDepth.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spinEditPassesBetweenSaves.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
			this.groupControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditFilterAlgo.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSaturationAlgo.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
			this.groupControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pb.Properties)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void render_ProgressUpdate(object sender, int val)
		{
			this.pb.Position = val;
		}

		private void btn_Rnd_Click(object sender, System.EventArgs e)
		{
			this.UpdateDefaults();
			this.render.Begin();
		}

		private void btn_RndTh_Click(object sender, System.EventArgs e)
		{
			this.UpdateDefaults();
			this.btn_Save.Enabled = true;
			this.btn_Stp.Enabled = true;
			this.render.BeginThreaded();
		}

		private void btn_Save_Click(object sender, System.EventArgs e)
		{
			this.render.SaveProg = true;
		}

		private void btn_Stp_Click(object sender, System.EventArgs e)
		{
			this.btn_Stp.Enabled = false;
			this.btn_Save.Enabled = false;
			this.render.Stop = true;
		}

		private void UpdateDefaults()
		{
			this.render.MaxBounces = (int)this.spin_bl.Value;
			this.render.MaxPhotons = (int)this.spin_pc.Value;
			Render.PhotonRadius = (int)this.spin_pr.Value;
		}

		private void comboBoxEdit1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
    
		}

		private void lb_dDiff_Click(object sender, System.EventArgs e)
		{
    
		}

		private void def_Diffuse_EditValueChanged(object sender, System.EventArgs e)
		{
    
		}
	}
}
