/* ---------------------------------------------------------------
 * Prometheus
 * Bungie Map-based Multi-Game Editing Kit
 * 2004-2005, Halo-Dev
 * ---------------------------------------------------------------
 * File        : OptionsDialog.cs
 * Description : Provides an interface to options configuration.
 * Author      : Nick
 * ---------------------------------------------------------------
 */

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Prometheus.Core;

namespace Prometheus
{
  public enum OptionsTab
  {
    General = 0,
    Logging = 1,
    Saving = 2,
    Documents = 3,
    Paths = 4,
    Security = 5,
    Rendering = 6,
    Navigation = 7,
    Compilation = 8,
    TestEnvironment = 9,
    NetworkAndInternet = 10,
    AllTabs = 100
  };

  public enum TabAction
  {
    Reset,
    GetGui,
    SetGui
  }

	/// <summary>
	/// Summary description for OptionsDialog.
	/// </summary>
	public class OptionsDialog : DevExpress.XtraEditors.XtraForm
	{
    private bool valuesChanged; // Did the user change any values?

    // Form Designer Vars
    private DevExpress.XtraBars.BarDockControl barDockControlTop;
    private DevExpress.XtraBars.BarDockControl barDockControlBottom;
    private DevExpress.XtraBars.BarDockControl barDockControlLeft;
    private DevExpress.XtraBars.BarDockControl barDockControlRight;
    private DevExpress.XtraBars.Bar bar1;
    private DevExpress.XtraTab.XtraTabControl xtcOptionTabs;
    private DevExpress.XtraTab.XtraTabPage xtpGeneral;
    private DevExpress.XtraEditors.GroupControl xgcDebugLogging;
    private DevExpress.XtraEditors.ComboBoxEdit xcbLogDetail;
    private System.Windows.Forms.Label lblLogDetail;
    private System.Windows.Forms.Label lblDebugLoggingInfo;
    private DevExpress.XtraEditors.GroupControl xgcDiskFileLimits;
    private System.Windows.Forms.Label lblfileSizeInfo;
    private System.Windows.Forms.Label lbldiskUsageInfo;
    private System.Windows.Forms.Label lblFileSize;
    private System.Windows.Forms.Label lbldiskUsage;
    private DevExpress.XtraEditors.SpinEdit xseFileSize;
    private DevExpress.XtraEditors.SpinEdit xseDiskUsage;
    private DevExpress.XtraEditors.CheckEdit xchkDebugLogging;
    private DevExpress.XtraTab.XtraTabPage xtpSaving;
    private DevExpress.XtraTab.XtraTabPage xtpDocuments;
    private DevExpress.XtraTab.XtraTabPage xtpFolders;
    private DevExpress.XtraTab.XtraTabPage xtpSecurity;
    private DevExpress.XtraTab.XtraTabPage xtpRendering;
    private DevExpress.XtraTab.XtraTabPage xtpNavigation;
    private DevExpress.XtraTab.XtraTabPage xtpCompilation;
    private DevExpress.XtraTab.XtraTabPage xtpTestEnv;
    private DevExpress.XtraTab.XtraTabPage xtpNetworking;
    private DevExpress.XtraBars.BarManager xbmBarManager;
    private DevExpress.XtraBars.Bar xbStatus;
    private DevExpress.XtraBars.BarStaticItem xstbUnsaved;
    private DevExpress.XtraEditors.SimpleButton xsbOk;
    private DevExpress.XtraEditors.SimpleButton xsbApply;
    private DevExpress.XtraEditors.SimpleButton xsbCancel;
    private DevExpress.XtraEditors.SimpleButton xsbDefaults;
    private DevExpress.LookAndFeel.DefaultLookAndFeel lnfDefault;
    private System.Windows.Forms.Label label8;
    private DevExpress.XtraTab.XtraTabPage xtpLogging;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label9;
    private DevExpress.XtraEditors.SpinEdit spinEditCameraSensitivity;
    private DevExpress.XtraEditors.SpinEdit spinEditCameraSpeed;
    private DevExpress.XtraEditors.CheckEdit checkEditShowFPS;
    private DevExpress.XtraEditors.ButtonEdit buttonEditHaloPCMaps;
    private DevExpress.XtraEditors.ButtonEdit buttonEditHalo2XboxMaps;
    private DevExpress.XtraEditors.ButtonEdit buttonEditHalo1XboxMaps;
    private DevExpress.XtraEditors.ButtonEdit buttonEditHaloCEMaps;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditTextureDetail;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
    private System.Windows.Forms.CheckBox checkBox1;
    private System.Windows.Forms.CheckBox checkBox2;
    private DevExpress.XtraEditors.GroupControl groupControl1;
    private System.Windows.Forms.CheckBox checkBox3;
    private System.Windows.Forms.CheckBox checkBox4;
    private System.Windows.Forms.CheckBox checkBox5;
    private DevExpress.XtraEditors.GroupControl groupControl2;
    private System.Windows.Forms.CheckBox checkBox6;
    private System.Windows.Forms.CheckBox checkBox7;
    private System.Windows.Forms.CheckBox checkBox8;
    private System.Windows.Forms.CheckBox checkBox9;
    private System.Windows.Forms.CheckBox checkBox10;
    private DevExpress.XtraEditors.GroupControl groupControl3;
    private DevExpress.XtraEditors.SpinEdit spinEditTranslationToolScale;
    private DevExpress.XtraEditors.SpinEdit spinEditRotationToolScale;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.CheckBox checkBox11;
    private System.Windows.Forms.CheckBox checkBox12;
    private System.Windows.Forms.CheckBox checkBox13;
    private System.Windows.Forms.CheckBox checkBox14;
    private DevExpress.XtraEditors.GroupControl groupControl4;
    private DevExpress.XtraEditors.GroupControl groupControl5;
    private DevExpress.XtraEditors.RadioGroup radioGroup1;
    private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit2;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.CheckBox checkBox15;
    private DevExpress.XtraEditors.GroupControl groupControl6;
    private DevExpress.XtraEditors.GroupControl groupControl7;
    private System.Windows.Forms.CheckBox checkBox16;
    private System.Windows.Forms.CheckBox checkBox17;
    private DevExpress.XtraEditors.TextEdit textEdit1;
    private System.Windows.Forms.Label label12;
    private System.ComponentModel.IContainer components;

		public OptionsDialog()
		{
			InitializeComponent();

      UpdateTab(OptionsTab.AllTabs);
      ValuesSet();
		}

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
      this.xtcOptionTabs = new DevExpress.XtraTab.XtraTabControl();
      this.xtpRendering = new DevExpress.XtraTab.XtraTabPage();
      this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
      this.checkEditShowFPS = new DevExpress.XtraEditors.CheckEdit();
      this.label9 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.spinEditCameraSpeed = new DevExpress.XtraEditors.SpinEdit();
      this.spinEditCameraSensitivity = new DevExpress.XtraEditors.SpinEdit();
      this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
      this.checkBox14 = new System.Windows.Forms.CheckBox();
      this.checkBox13 = new System.Windows.Forms.CheckBox();
      this.checkBox12 = new System.Windows.Forms.CheckBox();
      this.checkBox11 = new System.Windows.Forms.CheckBox();
      this.label10 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.spinEditRotationToolScale = new DevExpress.XtraEditors.SpinEdit();
      this.spinEditTranslationToolScale = new DevExpress.XtraEditors.SpinEdit();
      this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
      this.checkBox5 = new System.Windows.Forms.CheckBox();
      this.checkBox4 = new System.Windows.Forms.CheckBox();
      this.checkBox3 = new System.Windows.Forms.CheckBox();
      this.checkBox1 = new System.Windows.Forms.CheckBox();
      this.checkBox2 = new System.Windows.Forms.CheckBox();
      this.label6 = new System.Windows.Forms.Label();
      this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
      this.label5 = new System.Windows.Forms.Label();
      this.comboBoxEditTextureDetail = new DevExpress.XtraEditors.ComboBoxEdit();
      this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
      this.checkBox6 = new System.Windows.Forms.CheckBox();
      this.checkBox7 = new System.Windows.Forms.CheckBox();
      this.checkBox8 = new System.Windows.Forms.CheckBox();
      this.checkBox9 = new System.Windows.Forms.CheckBox();
      this.checkBox10 = new System.Windows.Forms.CheckBox();
      this.xtpGeneral = new DevExpress.XtraTab.XtraTabPage();
      this.xtpFolders = new DevExpress.XtraTab.XtraTabPage();
      this.label4 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.buttonEditHaloPCMaps = new DevExpress.XtraEditors.ButtonEdit();
      this.buttonEditHalo2XboxMaps = new DevExpress.XtraEditors.ButtonEdit();
      this.buttonEditHalo1XboxMaps = new DevExpress.XtraEditors.ButtonEdit();
      this.buttonEditHaloCEMaps = new DevExpress.XtraEditors.ButtonEdit();
      this.xtpLogging = new DevExpress.XtraTab.XtraTabPage();
      this.xgcDebugLogging = new DevExpress.XtraEditors.GroupControl();
      this.xcbLogDetail = new DevExpress.XtraEditors.ComboBoxEdit();
      this.lblLogDetail = new System.Windows.Forms.Label();
      this.lblDebugLoggingInfo = new System.Windows.Forms.Label();
      this.xgcDiskFileLimits = new DevExpress.XtraEditors.GroupControl();
      this.lblfileSizeInfo = new System.Windows.Forms.Label();
      this.lbldiskUsageInfo = new System.Windows.Forms.Label();
      this.lblFileSize = new System.Windows.Forms.Label();
      this.lbldiskUsage = new System.Windows.Forms.Label();
      this.xseFileSize = new DevExpress.XtraEditors.SpinEdit();
      this.xseDiskUsage = new DevExpress.XtraEditors.SpinEdit();
      this.xchkDebugLogging = new DevExpress.XtraEditors.CheckEdit();
      this.xtpSaving = new DevExpress.XtraTab.XtraTabPage();
      this.xtpDocuments = new DevExpress.XtraTab.XtraTabPage();
      this.xtpSecurity = new DevExpress.XtraTab.XtraTabPage();
      this.xtpNavigation = new DevExpress.XtraTab.XtraTabPage();
      this.xtpCompilation = new DevExpress.XtraTab.XtraTabPage();
      this.xtpTestEnv = new DevExpress.XtraTab.XtraTabPage();
      this.xtpNetworking = new DevExpress.XtraTab.XtraTabPage();
      this.xbmBarManager = new DevExpress.XtraBars.BarManager();
      this.xbStatus = new DevExpress.XtraBars.Bar();
      this.xstbUnsaved = new DevExpress.XtraBars.BarStaticItem();
      this.bar1 = new DevExpress.XtraBars.Bar();
      this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
      this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
      this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
      this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
      this.xsbOk = new DevExpress.XtraEditors.SimpleButton();
      this.xsbApply = new DevExpress.XtraEditors.SimpleButton();
      this.xsbCancel = new DevExpress.XtraEditors.SimpleButton();
      this.xsbDefaults = new DevExpress.XtraEditors.SimpleButton();
      this.lnfDefault = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
      this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
      this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
      this.comboBoxEdit2 = new DevExpress.XtraEditors.ComboBoxEdit();
      this.label11 = new System.Windows.Forms.Label();
      this.checkBox15 = new System.Windows.Forms.CheckBox();
      this.groupControl6 = new DevExpress.XtraEditors.GroupControl();
      this.groupControl7 = new DevExpress.XtraEditors.GroupControl();
      this.checkBox16 = new System.Windows.Forms.CheckBox();
      this.checkBox17 = new System.Windows.Forms.CheckBox();
      this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
      this.label12 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.xtcOptionTabs)).BeginInit();
      this.xtcOptionTabs.SuspendLayout();
      this.xtpRendering.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
      this.groupControl4.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.checkEditShowFPS.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.spinEditCameraSpeed.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.spinEditCameraSensitivity.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
      this.groupControl3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.spinEditRotationToolScale.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.spinEditTranslationToolScale.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
      this.groupControl1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditTextureDetail.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
      this.groupControl2.SuspendLayout();
      this.xtpFolders.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.buttonEditHaloPCMaps.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.buttonEditHalo2XboxMaps.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.buttonEditHalo1XboxMaps.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.buttonEditHaloCEMaps.Properties)).BeginInit();
      this.xtpLogging.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.xgcDebugLogging)).BeginInit();
      this.xgcDebugLogging.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.xcbLogDetail.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.xgcDiskFileLimits)).BeginInit();
      this.xgcDiskFileLimits.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.xseFileSize.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.xseDiskUsage.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.xchkDebugLogging.Properties)).BeginInit();
      this.xtpCompilation.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.xbmBarManager)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).BeginInit();
      this.groupControl5.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).BeginInit();
      this.groupControl6.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.groupControl7)).BeginInit();
      this.groupControl7.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
      this.SuspendLayout();
      // 
      // xtcOptionTabs
      // 
      this.xtcOptionTabs.AllowDrop = true;
      this.xtcOptionTabs.Controls.Add(this.xtpGeneral);
      this.xtcOptionTabs.Controls.Add(this.xtpFolders);
      this.xtcOptionTabs.Controls.Add(this.xtpRendering);
      this.xtcOptionTabs.Controls.Add(this.xtpLogging);
      this.xtcOptionTabs.Controls.Add(this.xtpSaving);
      this.xtcOptionTabs.Controls.Add(this.xtpDocuments);
      this.xtcOptionTabs.Controls.Add(this.xtpSecurity);
      this.xtcOptionTabs.Controls.Add(this.xtpNavigation);
      this.xtcOptionTabs.Controls.Add(this.xtpCompilation);
      this.xtcOptionTabs.Controls.Add(this.xtpTestEnv);
      this.xtcOptionTabs.Controls.Add(this.xtpNetworking);
      this.xtcOptionTabs.Location = new System.Drawing.Point(5, 5);
      this.xtcOptionTabs.MultiLine = DevExpress.Utils.DefaultBoolean.True;
      this.xtcOptionTabs.Name = "xtcOptionTabs";
      this.xtcOptionTabs.SelectedTabPage = this.xtpLogging;
      this.xtcOptionTabs.Size = new System.Drawing.Size(583, 612);
      this.xtcOptionTabs.TabIndex = 0;
      this.xtcOptionTabs.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
                                                                                  this.xtpGeneral,
                                                                                  this.xtpFolders,
                                                                                  this.xtpRendering,
                                                                                  this.xtpLogging,
                                                                                  this.xtpSaving,
                                                                                  this.xtpDocuments,
                                                                                  this.xtpSecurity,
                                                                                  this.xtpNavigation,
                                                                                  this.xtpCompilation,
                                                                                  this.xtpTestEnv,
                                                                                  this.xtpNetworking});
      // 
      // xtpRendering
      // 
      this.xtpRendering.Controls.Add(this.groupControl4);
      this.xtpRendering.Controls.Add(this.groupControl3);
      this.xtpRendering.Controls.Add(this.groupControl1);
      this.xtpRendering.Controls.Add(this.groupControl2);
      this.xtpRendering.Name = "xtpRendering";
      this.xtpRendering.Size = new System.Drawing.Size(574, 555);
      this.xtpRendering.Text = "Rendering";
      // 
      // groupControl4
      // 
      this.groupControl4.Controls.Add(this.checkEditShowFPS);
      this.groupControl4.Controls.Add(this.label9);
      this.groupControl4.Controls.Add(this.label1);
      this.groupControl4.Controls.Add(this.spinEditCameraSpeed);
      this.groupControl4.Controls.Add(this.spinEditCameraSensitivity);
      this.groupControl4.Location = new System.Drawing.Point(16, 16);
      this.groupControl4.Name = "groupControl4";
      this.groupControl4.Size = new System.Drawing.Size(288, 200);
      this.groupControl4.TabIndex = 16;
      this.groupControl4.Text = "General";
      // 
      // checkEditShowFPS
      // 
      this.checkEditShowFPS.Location = new System.Drawing.Point(16, 88);
      this.checkEditShowFPS.Name = "checkEditShowFPS";
      // 
      // checkEditShowFPS.Properties
      // 
      this.checkEditShowFPS.Properties.Caption = "Show FPS";
      this.checkEditShowFPS.Size = new System.Drawing.Size(82, 21);
      this.checkEditShowFPS.TabIndex = 4;
      this.checkEditShowFPS.CheckedChanged += new System.EventHandler(this.checkEditShowFPS_CheckedChanged);
      // 
      // label9
      // 
      this.label9.Location = new System.Drawing.Point(16, 56);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(104, 23);
      this.label9.TabIndex = 1;
      this.label9.Text = "Camera Speed:";
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(16, 24);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(120, 23);
      this.label1.TabIndex = 0;
      this.label1.Text = "Camera Sensitivity:";
      // 
      // spinEditCameraSpeed
      // 
      this.spinEditCameraSpeed.EditValue = new System.Decimal(new int[] {
                                                                          1,
                                                                          0,
                                                                          0,
                                                                          0});
      this.spinEditCameraSpeed.Location = new System.Drawing.Point(144, 56);
      this.spinEditCameraSpeed.Name = "spinEditCameraSpeed";
      // 
      // spinEditCameraSpeed.Properties
      // 
      this.spinEditCameraSpeed.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                                new DevExpress.XtraEditors.Controls.EditorButton()});
      this.spinEditCameraSpeed.Properties.IsFloatValue = false;
      this.spinEditCameraSpeed.Properties.Mask.EditMask = "N00";
      this.spinEditCameraSpeed.Properties.MaxValue = new System.Decimal(new int[] {
                                                                                    60,
                                                                                    0,
                                                                                    0,
                                                                                    0});
      this.spinEditCameraSpeed.Properties.MinValue = new System.Decimal(new int[] {
                                                                                    1,
                                                                                    0,
                                                                                    0,
                                                                                    0});
      this.spinEditCameraSpeed.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
      this.spinEditCameraSpeed.Properties.UseCtrlIncrement = false;
      this.spinEditCameraSpeed.Size = new System.Drawing.Size(75, 22);
      this.spinEditCameraSpeed.TabIndex = 3;
      this.spinEditCameraSpeed.TextChanged += new System.EventHandler(this.spinEditCameraSpeed_TextChanged);
      // 
      // spinEditCameraSensitivity
      // 
      this.spinEditCameraSensitivity.EditValue = new System.Decimal(new int[] {
                                                                                1,
                                                                                0,
                                                                                0,
                                                                                0});
      this.spinEditCameraSensitivity.Location = new System.Drawing.Point(144, 24);
      this.spinEditCameraSensitivity.Name = "spinEditCameraSensitivity";
      // 
      // spinEditCameraSensitivity.Properties
      // 
      this.spinEditCameraSensitivity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                                      new DevExpress.XtraEditors.Controls.EditorButton()});
      this.spinEditCameraSensitivity.Properties.IsFloatValue = false;
      this.spinEditCameraSensitivity.Properties.Mask.EditMask = "N00";
      this.spinEditCameraSensitivity.Properties.MaxValue = new System.Decimal(new int[] {
                                                                                          10,
                                                                                          0,
                                                                                          0,
                                                                                          0});
      this.spinEditCameraSensitivity.Properties.MinValue = new System.Decimal(new int[] {
                                                                                          1,
                                                                                          0,
                                                                                          0,
                                                                                          0});
      this.spinEditCameraSensitivity.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
      this.spinEditCameraSensitivity.Properties.UseCtrlIncrement = false;
      this.spinEditCameraSensitivity.Size = new System.Drawing.Size(75, 22);
      this.spinEditCameraSensitivity.TabIndex = 2;
      this.spinEditCameraSensitivity.TextChanged += new System.EventHandler(this.spinEditCameraSensitivity_TextChanged);
      // 
      // groupControl3
      // 
      this.groupControl3.Controls.Add(this.checkBox14);
      this.groupControl3.Controls.Add(this.checkBox13);
      this.groupControl3.Controls.Add(this.checkBox12);
      this.groupControl3.Controls.Add(this.checkBox11);
      this.groupControl3.Controls.Add(this.label10);
      this.groupControl3.Controls.Add(this.label7);
      this.groupControl3.Controls.Add(this.spinEditRotationToolScale);
      this.groupControl3.Controls.Add(this.spinEditTranslationToolScale);
      this.groupControl3.Location = new System.Drawing.Point(312, 16);
      this.groupControl3.Name = "groupControl3";
      this.groupControl3.Size = new System.Drawing.Size(248, 200);
      this.groupControl3.TabIndex = 15;
      this.groupControl3.Text = "Editor Tools";
      // 
      // checkBox14
      // 
      this.checkBox14.Location = new System.Drawing.Point(8, 168);
      this.checkBox14.Name = "checkBox14";
      this.checkBox14.Size = new System.Drawing.Size(136, 24);
      this.checkBox14.TabIndex = 7;
      this.checkBox14.Text = "Extended Z-Axis";
      // 
      // checkBox13
      // 
      this.checkBox13.Location = new System.Drawing.Point(8, 144);
      this.checkBox13.Name = "checkBox13";
      this.checkBox13.Size = new System.Drawing.Size(136, 24);
      this.checkBox13.TabIndex = 6;
      this.checkBox13.Text = "Extended Y-Axis";
      // 
      // checkBox12
      // 
      this.checkBox12.Location = new System.Drawing.Point(8, 120);
      this.checkBox12.Name = "checkBox12";
      this.checkBox12.Size = new System.Drawing.Size(136, 24);
      this.checkBox12.TabIndex = 5;
      this.checkBox12.Text = "Extended X-Axis";
      // 
      // checkBox11
      // 
      this.checkBox11.Location = new System.Drawing.Point(8, 88);
      this.checkBox11.Name = "checkBox11";
      this.checkBox11.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.checkBox11.Size = new System.Drawing.Size(232, 24);
      this.checkBox11.TabIndex = 4;
      this.checkBox11.Text = "Draw Bounding Box when selected";
      // 
      // label10
      // 
      this.label10.Location = new System.Drawing.Point(8, 56);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(144, 23);
      this.label10.TabIndex = 3;
      this.label10.Text = "Rotation Tool Scale:";
      this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label7
      // 
      this.label7.Location = new System.Drawing.Point(8, 24);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(144, 23);
      this.label7.TabIndex = 2;
      this.label7.Text = "Translation Tool Scale:";
      this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // spinEditRotationToolScale
      // 
      this.spinEditRotationToolScale.EditValue = new System.Decimal(new int[] {
                                                                                1,
                                                                                0,
                                                                                0,
                                                                                0});
      this.spinEditRotationToolScale.Location = new System.Drawing.Point(160, 56);
      this.spinEditRotationToolScale.Name = "spinEditRotationToolScale";
      // 
      // spinEditRotationToolScale.Properties
      // 
      this.spinEditRotationToolScale.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                                      new DevExpress.XtraEditors.Controls.EditorButton()});
      this.spinEditRotationToolScale.Properties.UseCtrlIncrement = false;
      this.spinEditRotationToolScale.Size = new System.Drawing.Size(75, 22);
      this.spinEditRotationToolScale.TabIndex = 1;
      this.spinEditRotationToolScale.EditValueChanged += new System.EventHandler(this.spinEditRotationToolScale_EditValueChanged);
      // 
      // spinEditTranslationToolScale
      // 
      this.spinEditTranslationToolScale.EditValue = new System.Decimal(new int[] {
                                                                                   1,
                                                                                   0,
                                                                                   0,
                                                                                   0});
      this.spinEditTranslationToolScale.Location = new System.Drawing.Point(160, 24);
      this.spinEditTranslationToolScale.Name = "spinEditTranslationToolScale";
      // 
      // spinEditTranslationToolScale.Properties
      // 
      this.spinEditTranslationToolScale.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                                         new DevExpress.XtraEditors.Controls.EditorButton()});
      this.spinEditTranslationToolScale.Properties.UseCtrlIncrement = false;
      this.spinEditTranslationToolScale.Size = new System.Drawing.Size(75, 22);
      this.spinEditTranslationToolScale.TabIndex = 0;
      this.spinEditTranslationToolScale.EditValueChanged += new System.EventHandler(this.spinEditTranslationToolScale_EditValueChanged);
      // 
      // groupControl1
      // 
      this.groupControl1.Controls.Add(this.checkBox5);
      this.groupControl1.Controls.Add(this.checkBox4);
      this.groupControl1.Controls.Add(this.checkBox3);
      this.groupControl1.Controls.Add(this.checkBox1);
      this.groupControl1.Controls.Add(this.checkBox2);
      this.groupControl1.Controls.Add(this.label6);
      this.groupControl1.Controls.Add(this.comboBoxEdit1);
      this.groupControl1.Controls.Add(this.label5);
      this.groupControl1.Controls.Add(this.comboBoxEditTextureDetail);
      this.groupControl1.Location = new System.Drawing.Point(16, 224);
      this.groupControl1.Name = "groupControl1";
      this.groupControl1.Size = new System.Drawing.Size(544, 152);
      this.groupControl1.TabIndex = 11;
      this.groupControl1.Text = "Map Editor Window";
      // 
      // checkBox5
      // 
      this.checkBox5.Location = new System.Drawing.Point(16, 120);
      this.checkBox5.Name = "checkBox5";
      this.checkBox5.Size = new System.Drawing.Size(216, 24);
      this.checkBox5.TabIndex = 13;
      this.checkBox5.Text = "Enable Bump Mapping";
      // 
      // checkBox4
      // 
      this.checkBox4.Location = new System.Drawing.Point(16, 96);
      this.checkBox4.Name = "checkBox4";
      this.checkBox4.Size = new System.Drawing.Size(216, 24);
      this.checkBox4.TabIndex = 12;
      this.checkBox4.Text = "Enable Diffuse Lighting";
      // 
      // checkBox3
      // 
      this.checkBox3.Location = new System.Drawing.Point(16, 72);
      this.checkBox3.Name = "checkBox3";
      this.checkBox3.Size = new System.Drawing.Size(216, 24);
      this.checkBox3.TabIndex = 11;
      this.checkBox3.Text = "Enable Specular Lighting";
      // 
      // checkBox1
      // 
      this.checkBox1.Location = new System.Drawing.Point(16, 24);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new System.Drawing.Size(184, 24);
      this.checkBox1.TabIndex = 9;
      this.checkBox1.Text = "Enable Detail Mapping";
      // 
      // checkBox2
      // 
      this.checkBox2.Location = new System.Drawing.Point(16, 48);
      this.checkBox2.Name = "checkBox2";
      this.checkBox2.Size = new System.Drawing.Size(216, 24);
      this.checkBox2.TabIndex = 10;
      this.checkBox2.Text = "Enable Environment Mapping";
      // 
      // label6
      // 
      this.label6.Location = new System.Drawing.Point(248, 64);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(128, 23);
      this.label6.TabIndex = 8;
      this.label6.Text = "Model Resolution:";
      // 
      // comboBoxEdit1
      // 
      this.comboBoxEdit1.EditValue = "Emulate Game";
      this.comboBoxEdit1.Location = new System.Drawing.Point(392, 64);
      this.comboBoxEdit1.Name = "comboBoxEdit1";
      // 
      // comboBoxEdit1.Properties
      // 
      this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                          new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.comboBoxEdit1.Properties.Items.AddRange(new object[] {
                                                                  "Emulate Game",
                                                                  "Maximum",
                                                                  "Medium",
                                                                  "Low",
                                                                  "Turdtastic"});
      this.comboBoxEdit1.Size = new System.Drawing.Size(112, 22);
      this.comboBoxEdit1.TabIndex = 7;
      // 
      // label5
      // 
      this.label5.Location = new System.Drawing.Point(248, 24);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(128, 23);
      this.label5.TabIndex = 6;
      this.label5.Text = "Texture Resolution:";
      // 
      // comboBoxEditTextureDetail
      // 
      this.comboBoxEditTextureDetail.EditValue = "High";
      this.comboBoxEditTextureDetail.Location = new System.Drawing.Point(392, 24);
      this.comboBoxEditTextureDetail.Name = "comboBoxEditTextureDetail";
      // 
      // comboBoxEditTextureDetail.Properties
      // 
      this.comboBoxEditTextureDetail.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                                      new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.comboBoxEditTextureDetail.Properties.Items.AddRange(new object[] {
                                                                              "High",
                                                                              "Low"});
      this.comboBoxEditTextureDetail.Size = new System.Drawing.Size(112, 22);
      this.comboBoxEditTextureDetail.TabIndex = 5;
      // 
      // groupControl2
      // 
      this.groupControl2.Controls.Add(this.checkBox6);
      this.groupControl2.Controls.Add(this.checkBox7);
      this.groupControl2.Controls.Add(this.checkBox8);
      this.groupControl2.Controls.Add(this.checkBox9);
      this.groupControl2.Controls.Add(this.checkBox10);
      this.groupControl2.Location = new System.Drawing.Point(16, 392);
      this.groupControl2.Name = "groupControl2";
      this.groupControl2.Size = new System.Drawing.Size(544, 152);
      this.groupControl2.TabIndex = 14;
      this.groupControl2.Text = "Model Preview Window";
      // 
      // checkBox6
      // 
      this.checkBox6.Location = new System.Drawing.Point(16, 120);
      this.checkBox6.Name = "checkBox6";
      this.checkBox6.Size = new System.Drawing.Size(216, 24);
      this.checkBox6.TabIndex = 13;
      this.checkBox6.Text = "Enable Bump Mapping";
      // 
      // checkBox7
      // 
      this.checkBox7.Location = new System.Drawing.Point(16, 96);
      this.checkBox7.Name = "checkBox7";
      this.checkBox7.Size = new System.Drawing.Size(216, 24);
      this.checkBox7.TabIndex = 12;
      this.checkBox7.Text = "Enable Diffuse Lighting";
      // 
      // checkBox8
      // 
      this.checkBox8.Location = new System.Drawing.Point(16, 72);
      this.checkBox8.Name = "checkBox8";
      this.checkBox8.Size = new System.Drawing.Size(216, 24);
      this.checkBox8.TabIndex = 11;
      this.checkBox8.Text = "Enable Specular Lighting";
      // 
      // checkBox9
      // 
      this.checkBox9.Location = new System.Drawing.Point(16, 24);
      this.checkBox9.Name = "checkBox9";
      this.checkBox9.Size = new System.Drawing.Size(184, 24);
      this.checkBox9.TabIndex = 9;
      this.checkBox9.Text = "Enable Detail Mapping";
      // 
      // checkBox10
      // 
      this.checkBox10.Location = new System.Drawing.Point(16, 48);
      this.checkBox10.Name = "checkBox10";
      this.checkBox10.Size = new System.Drawing.Size(216, 24);
      this.checkBox10.TabIndex = 10;
      this.checkBox10.Text = "Enable Environment Mapping";
      // 
      // xtpGeneral
      // 
      this.xtpGeneral.Name = "xtpGeneral";
      this.xtpGeneral.Size = new System.Drawing.Size(574, 555);
      this.xtpGeneral.Text = "General";
      // 
      // xtpFolders
      // 
      this.xtpFolders.Controls.Add(this.label4);
      this.xtpFolders.Controls.Add(this.label3);
      this.xtpFolders.Controls.Add(this.label2);
      this.xtpFolders.Controls.Add(this.label8);
      this.xtpFolders.Controls.Add(this.buttonEditHaloPCMaps);
      this.xtpFolders.Controls.Add(this.buttonEditHalo2XboxMaps);
      this.xtpFolders.Controls.Add(this.buttonEditHalo1XboxMaps);
      this.xtpFolders.Controls.Add(this.buttonEditHaloCEMaps);
      this.xtpFolders.Name = "xtpFolders";
      this.xtpFolders.Size = new System.Drawing.Size(574, 555);
      this.xtpFolders.Text = "Folders";
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(16, 184);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(160, 23);
      this.label4.TabIndex = 18;
      this.label4.Text = "Halo 2 Xbox Maps Folder:";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(16, 136);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(160, 23);
      this.label3.TabIndex = 17;
      this.label3.Text = "Halo 1 Xbox Maps Folder:";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(16, 88);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(160, 23);
      this.label2.TabIndex = 16;
      this.label2.Text = "Halo 1 CE Maps Folder:";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label8
      // 
      this.label8.Location = new System.Drawing.Point(16, 40);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(160, 23);
      this.label8.TabIndex = 15;
      this.label8.Text = "Halo 1 PC Maps Folder:";
      this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // buttonEditHaloPCMaps
      // 
      this.buttonEditHaloPCMaps.EditValue = "";
      this.buttonEditHaloPCMaps.Location = new System.Drawing.Point(184, 40);
      this.buttonEditHaloPCMaps.Name = "buttonEditHaloPCMaps";
      // 
      // buttonEditHaloPCMaps.Properties
      // 
      this.buttonEditHaloPCMaps.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                                 new DevExpress.XtraEditors.Controls.EditorButton()});
      this.buttonEditHaloPCMaps.Properties.ReadOnly = true;
      this.buttonEditHaloPCMaps.Size = new System.Drawing.Size(360, 22);
      this.buttonEditHaloPCMaps.TabIndex = 14;
      this.buttonEditHaloPCMaps.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEditHaloPCMaps_ButtonClick);
      this.buttonEditHaloPCMaps.EditValueChanged += new System.EventHandler(this.ValueChangedEvnt);
      // 
      // buttonEditHalo2XboxMaps
      // 
      this.buttonEditHalo2XboxMaps.EditValue = "";
      this.buttonEditHalo2XboxMaps.Location = new System.Drawing.Point(184, 184);
      this.buttonEditHalo2XboxMaps.Name = "buttonEditHalo2XboxMaps";
      // 
      // buttonEditHalo2XboxMaps.Properties
      // 
      this.buttonEditHalo2XboxMaps.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                                    new DevExpress.XtraEditors.Controls.EditorButton()});
      this.buttonEditHalo2XboxMaps.Properties.ReadOnly = true;
      this.buttonEditHalo2XboxMaps.Size = new System.Drawing.Size(360, 22);
      this.buttonEditHalo2XboxMaps.TabIndex = 8;
      this.buttonEditHalo2XboxMaps.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEditHalo2XboxMaps_ButtonClick);
      this.buttonEditHalo2XboxMaps.EditValueChanged += new System.EventHandler(this.ValueChangedEvnt);
      // 
      // buttonEditHalo1XboxMaps
      // 
      this.buttonEditHalo1XboxMaps.EditValue = "";
      this.buttonEditHalo1XboxMaps.Location = new System.Drawing.Point(184, 136);
      this.buttonEditHalo1XboxMaps.Name = "buttonEditHalo1XboxMaps";
      // 
      // buttonEditHalo1XboxMaps.Properties
      // 
      this.buttonEditHalo1XboxMaps.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                                    new DevExpress.XtraEditors.Controls.EditorButton()});
      this.buttonEditHalo1XboxMaps.Properties.ReadOnly = true;
      this.buttonEditHalo1XboxMaps.Size = new System.Drawing.Size(360, 22);
      this.buttonEditHalo1XboxMaps.TabIndex = 6;
      this.buttonEditHalo1XboxMaps.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEditHalo1XboxMaps_ButtonClick);
      this.buttonEditHalo1XboxMaps.EditValueChanged += new System.EventHandler(this.ValueChangedEvnt);
      // 
      // buttonEditHaloCEMaps
      // 
      this.buttonEditHaloCEMaps.EditValue = "";
      this.buttonEditHaloCEMaps.Location = new System.Drawing.Point(184, 88);
      this.buttonEditHaloCEMaps.Name = "buttonEditHaloCEMaps";
      // 
      // buttonEditHaloCEMaps.Properties
      // 
      this.buttonEditHaloCEMaps.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                                 new DevExpress.XtraEditors.Controls.EditorButton()});
      this.buttonEditHaloCEMaps.Properties.ReadOnly = true;
      this.buttonEditHaloCEMaps.Size = new System.Drawing.Size(360, 22);
      this.buttonEditHaloCEMaps.TabIndex = 4;
      this.buttonEditHaloCEMaps.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEditHaloCEMaps_ButtonClick);
      this.buttonEditHaloCEMaps.EditValueChanged += new System.EventHandler(this.ValueChangedEvnt);
      // 
      // xtpLogging
      // 
      this.xtpLogging.Controls.Add(this.xgcDebugLogging);
      this.xtpLogging.Name = "xtpLogging";
      this.xtpLogging.Size = new System.Drawing.Size(574, 555);
      this.xtpLogging.Text = "Logging";
      // 
      // xgcDebugLogging
      // 
      this.xgcDebugLogging.Controls.Add(this.xcbLogDetail);
      this.xgcDebugLogging.Controls.Add(this.lblLogDetail);
      this.xgcDebugLogging.Controls.Add(this.lblDebugLoggingInfo);
      this.xgcDebugLogging.Controls.Add(this.xgcDiskFileLimits);
      this.xgcDebugLogging.Controls.Add(this.xchkDebugLogging);
      this.xgcDebugLogging.Location = new System.Drawing.Point(10, 9);
      this.xgcDebugLogging.Name = "xgcDebugLogging";
      this.xgcDebugLogging.Size = new System.Drawing.Size(556, 295);
      this.xgcDebugLogging.TabIndex = 0;
      this.xgcDebugLogging.Text = "Debug Logging";
      // 
      // xcbLogDetail
      // 
      this.xcbLogDetail.EditValue = "Critical Errors";
      this.xcbLogDetail.Location = new System.Drawing.Point(115, 256);
      this.xcbLogDetail.Name = "xcbLogDetail";
      // 
      // xcbLogDetail.Properties
      // 
      this.xcbLogDetail.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                         new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.xcbLogDetail.Properties.Items.AddRange(new object[] {
                                                                 "All Logging",
                                                                 "Errors & Warnings",
                                                                 "Critical Errors"});
      this.xcbLogDetail.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
      this.xcbLogDetail.Size = new System.Drawing.Size(189, 22);
      this.xcbLogDetail.TabIndex = 6;
      this.xcbLogDetail.EditValueChanged += new System.EventHandler(this.ValueChangedEvnt);
      // 
      // lblLogDetail
      // 
      this.lblLogDetail.Location = new System.Drawing.Point(24, 256);
      this.lblLogDetail.Name = "lblLogDetail";
      this.lblLogDetail.Size = new System.Drawing.Size(77, 18);
      this.lblLogDetail.TabIndex = 5;
      this.lblLogDetail.Text = "Log Detail:";
      this.lblLogDetail.TextAlign = System.Drawing.ContentAlignment.BottomRight;
      // 
      // lblDebugLoggingInfo
      // 
      this.lblDebugLoggingInfo.Location = new System.Drawing.Point(19, 55);
      this.lblDebugLoggingInfo.Name = "lblDebugLoggingInfo";
      this.lblDebugLoggingInfo.Size = new System.Drawing.Size(528, 64);
      this.lblDebugLoggingInfo.TabIndex = 3;
      this.lblDebugLoggingInfo.Text = @"Enabling debug logging dumps more information about your activities to a text file. This information can help greatly in solving a problem you're running into and is required to submit a bug report.  It is recommended that you leave this option turned on so if a problem does arise it is logged.";
      // 
      // xgcDiskFileLimits
      // 
      this.xgcDiskFileLimits.Controls.Add(this.lblfileSizeInfo);
      this.xgcDiskFileLimits.Controls.Add(this.lbldiskUsageInfo);
      this.xgcDiskFileLimits.Controls.Add(this.lblFileSize);
      this.xgcDiskFileLimits.Controls.Add(this.lbldiskUsage);
      this.xgcDiskFileLimits.Controls.Add(this.xseFileSize);
      this.xgcDiskFileLimits.Controls.Add(this.xseDiskUsage);
      this.xgcDiskFileLimits.Location = new System.Drawing.Point(10, 128);
      this.xgcDiskFileLimits.Name = "xgcDiskFileLimits";
      this.xgcDiskFileLimits.Size = new System.Drawing.Size(537, 110);
      this.xgcDiskFileLimits.TabIndex = 2;
      this.xgcDiskFileLimits.Text = "Configure Disk Usage / File Size Limits";
      // 
      // lblfileSizeInfo
      // 
      this.lblfileSizeInfo.Location = new System.Drawing.Point(163, 73);
      this.lblfileSizeInfo.Name = "lblfileSizeInfo";
      this.lblfileSizeInfo.Size = new System.Drawing.Size(355, 18);
      this.lblfileSizeInfo.TabIndex = 5;
      this.lblfileSizeInfo.Text = "At what point do you want to start loging in a new file ?";
      // 
      // lbldiskUsageInfo
      // 
      this.lbldiskUsageInfo.Location = new System.Drawing.Point(163, 37);
      this.lbldiskUsageInfo.Name = "lbldiskUsageInfo";
      this.lbldiskUsageInfo.Size = new System.Drawing.Size(355, 18);
      this.lbldiskUsageInfo.TabIndex = 4;
      this.lbldiskUsageInfo.Text = "How much total hard disk space can debug logging use ?";
      // 
      // lblFileSize
      // 
      this.lblFileSize.Location = new System.Drawing.Point(10, 73);
      this.lblFileSize.Name = "lblFileSize";
      this.lblFileSize.Size = new System.Drawing.Size(67, 18);
      this.lblFileSize.TabIndex = 3;
      this.lblFileSize.Text = "File Size:";
      // 
      // lbldiskUsage
      // 
      this.lbldiskUsage.Location = new System.Drawing.Point(10, 37);
      this.lbldiskUsage.Name = "lbldiskUsage";
      this.lbldiskUsage.Size = new System.Drawing.Size(76, 26);
      this.lbldiskUsage.TabIndex = 2;
      this.lbldiskUsage.Text = "Disk Usage:";
      // 
      // xseFileSize
      // 
      this.xseFileSize.EditValue = new System.Decimal(new int[] {
                                                                  0,
                                                                  0,
                                                                  0,
                                                                  0});
      this.xseFileSize.Location = new System.Drawing.Point(96, 73);
      this.xseFileSize.Name = "xseFileSize";
      // 
      // xseFileSize.Properties
      // 
      this.xseFileSize.Properties.AccessibleDescription = "";
      this.xseFileSize.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                        new DevExpress.XtraEditors.Controls.EditorButton()});
      this.xseFileSize.Properties.IsFloatValue = false;
      this.xseFileSize.Properties.Mask.EditMask = "N00";
      this.xseFileSize.Properties.UseCtrlIncrement = false;
      this.xseFileSize.Size = new System.Drawing.Size(58, 22);
      this.xseFileSize.TabIndex = 1;
      this.xseFileSize.EditValueChanged += new System.EventHandler(this.ValueChangedEvnt);
      // 
      // xseDiskUsage
      // 
      this.xseDiskUsage.EditValue = new System.Decimal(new int[] {
                                                                   0,
                                                                   0,
                                                                   0,
                                                                   0});
      this.xseDiskUsage.Location = new System.Drawing.Point(96, 37);
      this.xseDiskUsage.Name = "xseDiskUsage";
      // 
      // xseDiskUsage.Properties
      // 
      this.xseDiskUsage.Properties.AccessibleDescription = "";
      this.xseDiskUsage.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                         new DevExpress.XtraEditors.Controls.EditorButton()});
      this.xseDiskUsage.Properties.IsFloatValue = false;
      this.xseDiskUsage.Properties.Mask.EditMask = "N00";
      this.xseDiskUsage.Properties.UseCtrlIncrement = false;
      this.xseDiskUsage.Size = new System.Drawing.Size(58, 22);
      this.xseDiskUsage.TabIndex = 0;
      this.xseDiskUsage.EditValueChanged += new System.EventHandler(this.ValueChangedEvnt);
      // 
      // xchkDebugLogging
      // 
      this.xchkDebugLogging.Location = new System.Drawing.Point(10, 27);
      this.xchkDebugLogging.Name = "xchkDebugLogging";
      // 
      // xchkDebugLogging.Properties
      // 
      this.xchkDebugLogging.Properties.Caption = "Enable Debug Logging";
      this.xchkDebugLogging.Size = new System.Drawing.Size(153, 21);
      this.xchkDebugLogging.TabIndex = 1;
      this.xchkDebugLogging.ToolTip = @"Enabling debug Logging dumps more informatio about your activities to a text file. This information can help greatly in solving a problem you're running into and is required to submit a bug report.  It is recommende that you leave this option turned on so if a problemm does arise it is logged.";
      this.xchkDebugLogging.CheckedChanged += new System.EventHandler(this.xchkDebugLogging_CheckedChanged);
      // 
      // xtpSaving
      // 
      this.xtpSaving.Name = "xtpSaving";
      this.xtpSaving.PageVisible = false;
      this.xtpSaving.Size = new System.Drawing.Size(574, 555);
      this.xtpSaving.Text = "Saving";
      // 
      // xtpDocuments
      // 
      this.xtpDocuments.Name = "xtpDocuments";
      this.xtpDocuments.PageVisible = false;
      this.xtpDocuments.Size = new System.Drawing.Size(574, 555);
      this.xtpDocuments.Text = "Documents";
      // 
      // xtpSecurity
      // 
      this.xtpSecurity.Name = "xtpSecurity";
      this.xtpSecurity.PageVisible = false;
      this.xtpSecurity.Size = new System.Drawing.Size(574, 555);
      this.xtpSecurity.Text = "Security";
      // 
      // xtpNavigation
      // 
      this.xtpNavigation.Name = "xtpNavigation";
      this.xtpNavigation.PageVisible = false;
      this.xtpNavigation.Size = new System.Drawing.Size(574, 555);
      this.xtpNavigation.Text = "Navigation";
      // 
      // xtpCompilation
      // 
      this.xtpCompilation.Controls.Add(this.groupControl7);
      this.xtpCompilation.Controls.Add(this.groupControl6);
      this.xtpCompilation.Controls.Add(this.groupControl5);
      this.xtpCompilation.Name = "xtpCompilation";
      this.xtpCompilation.PageVisible = false;
      this.xtpCompilation.Size = new System.Drawing.Size(574, 555);
      this.xtpCompilation.Text = "Compilation";
      // 
      // xtpTestEnv
      // 
      this.xtpTestEnv.Name = "xtpTestEnv";
      this.xtpTestEnv.PageVisible = false;
      this.xtpTestEnv.Size = new System.Drawing.Size(574, 555);
      this.xtpTestEnv.Text = "Test Enviorment";
      // 
      // xtpNetworking
      // 
      this.xtpNetworking.Name = "xtpNetworking";
      this.xtpNetworking.PageVisible = false;
      this.xtpNetworking.Size = new System.Drawing.Size(574, 555);
      this.xtpNetworking.Text = "Network / Internet";
      // 
      // xbmBarManager
      // 
      this.xbmBarManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
                                                                       this.xbStatus,
                                                                       this.bar1});
      this.xbmBarManager.DockControls.Add(this.barDockControlTop);
      this.xbmBarManager.DockControls.Add(this.barDockControlBottom);
      this.xbmBarManager.DockControls.Add(this.barDockControlLeft);
      this.xbmBarManager.DockControls.Add(this.barDockControlRight);
      this.xbmBarManager.Form = this;
      this.xbmBarManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
                                                                            this.xstbUnsaved});
      this.xbmBarManager.MainMenu = this.bar1;
      this.xbmBarManager.MaxItemId = 1;
      this.xbmBarManager.StatusBar = this.xbStatus;
      // 
      // xbStatus
      // 
      this.xbStatus.BarName = "Status Bar";
      this.xbStatus.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
      this.xbStatus.DockCol = 0;
      this.xbStatus.DockRow = 1;
      this.xbStatus.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
      this.xbStatus.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
                                                                                          new DevExpress.XtraBars.LinkPersistInfo(this.xstbUnsaved)});
      this.xbStatus.OptionsBar.AllowQuickCustomization = false;
      this.xbStatus.OptionsBar.DrawDragBorder = false;
      this.xbStatus.OptionsBar.UseWholeRow = true;
      this.xbStatus.Text = "Custom 2";
      // 
      // xstbUnsaved
      // 
      this.xstbUnsaved.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
      this.xstbUnsaved.Id = 0;
      this.xstbUnsaved.Name = "xstbUnsaved";
      this.xstbUnsaved.TextAlignment = System.Drawing.StringAlignment.Center;
      // 
      // bar1
      // 
      this.bar1.BarName = "Custom 3";
      this.bar1.DockCol = 0;
      this.bar1.DockRow = 1;
      this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
      this.bar1.OptionsBar.MultiLine = true;
      this.bar1.OptionsBar.UseWholeRow = true;
      this.bar1.Text = "Custom 3";
      this.bar1.Visible = false;
      // 
      // xsbOk
      // 
      this.xsbOk.Location = new System.Drawing.Point(10, 617);
      this.xsbOk.Name = "xsbOk";
      this.xsbOk.Size = new System.Drawing.Size(90, 26);
      this.xsbOk.TabIndex = 5;
      this.xsbOk.Text = "Ok";
      this.xsbOk.Click += new System.EventHandler(this.xsbOk_Click);
      // 
      // xsbApply
      // 
      this.xsbApply.Enabled = false;
      this.xsbApply.Location = new System.Drawing.Point(115, 617);
      this.xsbApply.Name = "xsbApply";
      this.xsbApply.Size = new System.Drawing.Size(90, 26);
      this.xsbApply.TabIndex = 6;
      this.xsbApply.Text = "Apply";
      this.xsbApply.Click += new System.EventHandler(this.xsbApply_Click);
      // 
      // xsbCancel
      // 
      this.xsbCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.xsbCancel.Location = new System.Drawing.Point(490, 617);
      this.xsbCancel.Name = "xsbCancel";
      this.xsbCancel.Size = new System.Drawing.Size(90, 26);
      this.xsbCancel.TabIndex = 8;
      this.xsbCancel.Text = "Cancel";
      this.xsbCancel.Click += new System.EventHandler(this.xsbCancel_Click);
      // 
      // xsbDefaults
      // 
      this.xsbDefaults.Location = new System.Drawing.Point(370, 617);
      this.xsbDefaults.Name = "xsbDefaults";
      this.xsbDefaults.Size = new System.Drawing.Size(90, 26);
      this.xsbDefaults.TabIndex = 7;
      this.xsbDefaults.Text = "Defaults";
      this.xsbDefaults.Click += new System.EventHandler(this.xsbDefaults_Click);
      // 
      // lnfDefault
      // 
      this.lnfDefault.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
      this.lnfDefault.LookAndFeel.UseWindowsXPTheme = false;
      // 
      // groupControl5
      // 
      this.groupControl5.Controls.Add(this.label11);
      this.groupControl5.Controls.Add(this.comboBoxEdit2);
      this.groupControl5.Controls.Add(this.radioGroup1);
      this.groupControl5.Location = new System.Drawing.Point(8, 360);
      this.groupControl5.Name = "groupControl5";
      this.groupControl5.Size = new System.Drawing.Size(560, 144);
      this.groupControl5.TabIndex = 1;
      this.groupControl5.Text = "Xbox Compile Options";
      // 
      // radioGroup1
      // 
      this.radioGroup1.Location = new System.Drawing.Point(16, 32);
      this.radioGroup1.Name = "radioGroup1";
      // 
      // radioGroup1.Properties
      // 
      this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
                                                                                                        new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Generate test cache file (padded)"),
                                                                                                        new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Generate Final Map (for LAN disk)")});
      this.radioGroup1.Size = new System.Drawing.Size(272, 56);
      this.radioGroup1.TabIndex = 0;
      // 
      // comboBoxEdit2
      // 
      this.comboBoxEdit2.EditValue = "cache003";
      this.comboBoxEdit2.Location = new System.Drawing.Point(160, 104);
      this.comboBoxEdit2.Name = "comboBoxEdit2";
      // 
      // comboBoxEdit2.Properties
      // 
      this.comboBoxEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                          new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.comboBoxEdit2.Properties.Items.AddRange(new object[] {
                                                                  "cache001",
                                                                  "cache002",
                                                                  "cache003",
                                                                  "cache004",
                                                                  "cache005"});
      this.comboBoxEdit2.Size = new System.Drawing.Size(128, 22);
      this.comboBoxEdit2.TabIndex = 1;
      // 
      // label11
      // 
      this.label11.Location = new System.Drawing.Point(16, 104);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(136, 23);
      this.label11.TabIndex = 2;
      this.label11.Text = "Test cache filename:";
      this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // checkBox15
      // 
      this.checkBox15.Location = new System.Drawing.Point(16, 24);
      this.checkBox15.Name = "checkBox15";
      this.checkBox15.Size = new System.Drawing.Size(208, 24);
      this.checkBox15.TabIndex = 2;
      this.checkBox15.Text = "Warn Before File Overwrite";
      // 
      // groupControl6
      // 
      this.groupControl6.Controls.Add(this.checkBox15);
      this.groupControl6.Location = new System.Drawing.Point(8, 16);
      this.groupControl6.Name = "groupControl6";
      this.groupControl6.Size = new System.Drawing.Size(560, 152);
      this.groupControl6.TabIndex = 3;
      this.groupControl6.Text = "General";
      // 
      // groupControl7
      // 
      this.groupControl7.Controls.Add(this.label12);
      this.groupControl7.Controls.Add(this.textEdit1);
      this.groupControl7.Controls.Add(this.checkBox17);
      this.groupControl7.Controls.Add(this.checkBox16);
      this.groupControl7.Location = new System.Drawing.Point(8, 184);
      this.groupControl7.Name = "groupControl7";
      this.groupControl7.Size = new System.Drawing.Size(560, 160);
      this.groupControl7.TabIndex = 4;
      this.groupControl7.Text = "PC/CE Options";
      // 
      // checkBox16
      // 
      this.checkBox16.Location = new System.Drawing.Point(16, 24);
      this.checkBox16.Name = "checkBox16";
      this.checkBox16.Size = new System.Drawing.Size(272, 24);
      this.checkBox16.TabIndex = 0;
      this.checkBox16.Text = "Copy output file to game maps directory";
      // 
      // checkBox17
      // 
      this.checkBox17.Location = new System.Drawing.Point(16, 48);
      this.checkBox17.Name = "checkBox17";
      this.checkBox17.Size = new System.Drawing.Size(240, 24);
      this.checkBox17.TabIndex = 1;
      this.checkBox17.Text = "Prompt to launch game after build";
      // 
      // textEdit1
      // 
      this.textEdit1.EditValue = "";
      this.textEdit1.Location = new System.Drawing.Point(16, 96);
      this.textEdit1.Name = "textEdit1";
      this.textEdit1.Size = new System.Drawing.Size(536, 22);
      this.textEdit1.TabIndex = 2;
      // 
      // label12
      // 
      this.label12.Location = new System.Drawing.Point(16, 72);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(192, 23);
      this.label12.TabIndex = 3;
      this.label12.Text = "Game command line options:";
      this.label12.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
      // 
      // OptionsDialog
      // 
      this.AcceptButton = this.xsbOk;
      this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
      this.CancelButton = this.xsbCancel;
      this.ClientSize = new System.Drawing.Size(590, 667);
      this.Controls.Add(this.xsbCancel);
      this.Controls.Add(this.xsbDefaults);
      this.Controls.Add(this.xsbApply);
      this.Controls.Add(this.xsbOk);
      this.Controls.Add(this.xtcOptionTabs);
      this.Controls.Add(this.barDockControlLeft);
      this.Controls.Add(this.barDockControlRight);
      this.Controls.Add(this.barDockControlBottom);
      this.Controls.Add(this.barDockControlTop);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "OptionsDialog";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Prometheus Configuration";
      this.TopMost = true;
      this.Closing += new System.ComponentModel.CancelEventHandler(this.xefOptionsDialog_Closing);
      this.Load += new System.EventHandler(this.OptionsDialog_Load);
      ((System.ComponentModel.ISupportInitialize)(this.xtcOptionTabs)).EndInit();
      this.xtcOptionTabs.ResumeLayout(false);
      this.xtpRendering.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
      this.groupControl4.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.checkEditShowFPS.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.spinEditCameraSpeed.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.spinEditCameraSensitivity.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
      this.groupControl3.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.spinEditRotationToolScale.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.spinEditTranslationToolScale.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
      this.groupControl1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditTextureDetail.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
      this.groupControl2.ResumeLayout(false);
      this.xtpFolders.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.buttonEditHaloPCMaps.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.buttonEditHalo2XboxMaps.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.buttonEditHalo1XboxMaps.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.buttonEditHaloCEMaps.Properties)).EndInit();
      this.xtpLogging.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.xgcDebugLogging)).EndInit();
      this.xgcDebugLogging.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.xcbLogDetail.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.xgcDiskFileLimits)).EndInit();
      this.xgcDiskFileLimits.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.xseFileSize.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.xseDiskUsage.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.xchkDebugLogging.Properties)).EndInit();
      this.xtpCompilation.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.xbmBarManager)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).EndInit();
      this.groupControl5.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).EndInit();
      this.groupControl6.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.groupControl7)).EndInit();
      this.groupControl7.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

    #region Options State Management
    /// <summary>
    /// This function sets or resets options for one specific TabPage or all TabPages.
    /// </summary>
    private void UpdateTab(OptionsTab tab)
    {
      switch(tab)
      {
        case OptionsTab.General:
          break;
        case OptionsTab.Logging:
          UpdateLoggingControls(TabAction.SetGui);
          break;
        case OptionsTab.Saving:
          break;
        case OptionsTab.Documents:
          break;
        case OptionsTab.Paths:
          UpdatePathsControls(TabAction.SetGui);
          break;
        case OptionsTab.Security:
          break;
        case OptionsTab.Rendering:
          UpdateRenderControls(TabAction.SetGui);
          break;
        case OptionsTab.Navigation:
          break;
        case OptionsTab.Compilation:
          break;
        case OptionsTab.TestEnvironment:
          break;
        case OptionsTab.NetworkAndInternet:
          break;
        case OptionsTab.AllTabs:
          UpdateLoggingControls(TabAction.SetGui);
          UpdatePathsControls(TabAction.SetGui);
          UpdateRenderControls(TabAction.SetGui);
          break;
      }
    }

    /// <summary>
    /// With this function, user changed values on the form are checked and set to the file.
    /// Every option should be added under its appropriate section, no if-else logic needed - set values to the control's value
    /// </summary>
    private void SaveOptions()
    {
      UpdateLoggingControls(TabAction.GetGui);
      UpdatePathsControls(TabAction.GetGui);
      UpdateRenderControls(TabAction.GetGui);
    }
    #endregion

    #region High Level GUI event handlers
    /// <summary>
    /// Ok Button - Check to see if the user changed anything without saving and then let them out
    /// </summary>
    private void xsbOk_Click(object sender, System.EventArgs e)
    {
      DialogResult choice;

      if(valuesChanged)
      {
        choice =  MessageBox.Show("Save changes?", "Prometheus", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
      
        if(choice == DialogResult.Yes)
        {
          SaveOptions();
          ValuesSet();
        }
        else if(choice == DialogResult.Cancel)
        {
          return;
        }
      }
      this.Close();
    }
    
    /// <summary>
    /// Apply Button - Saves changes without closing the dialog.
    /// </summary>
    private void xsbApply_Click(object sender, System.EventArgs e)
    {
      SaveOptions();
      ValuesSet();
    }


    /// <summary>
    /// Defaults Button - Reverts settings to defaults.
    /// </summary>
    private void xsbDefaults_Click(object sender, System.EventArgs e)
    {
      if(DialogResult.OK == MessageBox.Show("Restore default settings on this tab?", "Prometheus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
      {
        OptionsTab active_tab = (OptionsTab)xtcOptionTabs.SelectedTabPageIndex;

        switch(active_tab)
        {
          case OptionsTab.General:
            break;
          case OptionsTab.Logging:
            UpdateLoggingControls(TabAction.Reset);
            break;
          case OptionsTab.Saving:
            break;
          case OptionsTab.Documents:
            break;
          case OptionsTab.Paths:
            UpdatePathsControls(TabAction.Reset);
            break;
          case OptionsTab.Security:
            break;
          case OptionsTab.Rendering:
            UpdateRenderControls(TabAction.SetGui);
            break;
          case OptionsTab.Navigation:
            break;
          case OptionsTab.Compilation:
            break;
          case OptionsTab.TestEnvironment:
            break;
          case OptionsTab.NetworkAndInternet:
            break;
          case OptionsTab.AllTabs:
            UpdatePathsControls(TabAction.Reset);
            UpdateRenderControls(TabAction.Reset);
            UpdateLoggingControls(TabAction.Reset);
            break;
        }

        //Update the GUI
        UpdateTab(active_tab);

        // Since users are starting from the default options again, disregard state changes set prior to reset.
        ValuesSet();
      }
      else
      {
        MessageBox.Show("Press Ok if we totally saved you right there.", "Settings were not reverted!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
      }
    } 

    /// <summary>
    /// Cancel Button - So, the user wants out ... let them out but let them know they're about to lose their setting changes before they go.
    /// </summary>
    private void xsbCancel_Click(object sender, System.EventArgs e)
    {
      DialogResult choice;

      // Did they change any values at all? If not let them out without a prompt.
      if(valuesChanged)
      {
        choice =  MessageBox.Show("If you cancel now, you will lose changes you've made.", "Maybe this isn't a good time.", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
      
        if(choice == DialogResult.Cancel)
        {
          return;
        }
      }

      valuesChanged = false; // They wanted to cancel, so ignore the fact that values changed.

      this.Close(); // Nothing was changed or the user wants out, let's go.
    }

    /// <summary>
    /// Needed so we can used the ValueChanged() code as an event function and as a function without parameters throughout the code.
    /// </summary>
    private void ValueChangedEvnt(object sender, System.EventArgs e)
    {
      ValueChanged();
    }

    /// <summary>
    /// This function is called every time a value is changed. It sets up button actions and messages.
    /// </summary>
    private void ValueChanged()
    {
      valuesChanged = true;
      
      xsbApply.Enabled = true;
      xsbCancel.Enabled = true;

      xstbUnsaved.Border = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
      xstbUnsaved.Caption = "Your changes to the settings have not been saved. Press Apply to save them.";
      xstbUnsaved.Enabled = true;
    }

    /// <summary>
    /// Onces settings have been saved, this function is called to "clean up" the UI and button functions.
    /// Should always be called after SetOptions() and SaveOptions()
    /// </summary>
    private void ValuesSet()
    {
      valuesChanged = false;

      xsbApply.Enabled = false;
      xsbCancel.Enabled = false;

      xstbUnsaved.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
      xstbUnsaved.Caption = "";
      xstbUnsaved.Enabled = false;
    }


    private void xefOptionsDialog_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      DialogResult choice;

      if(valuesChanged)
      {
        choice =  MessageBox.Show("You have made changes to settings but have not saved them.\n\nWould you like to save these settings now?", "Hey! Wait a second.", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
      
        if(choice == DialogResult.Yes)
        {
          SaveOptions();
          ValuesSet();
          MessageBox.Show("Your settings were saved just fine.\nGood thing we asked you.", "Settings Saved");
        }
        else if(choice == DialogResult.Cancel)
        {
          return;
        }
      }    
    }

    #endregion
    
    private void UpdateControls(TabAction action)
    {
      switch(action)
      {
        case TabAction.GetGui:
          break;

        case TabAction.SetGui:
          break;

        case TabAction.Reset:
          break;
      }
    }
   
    #region Paths Tab
    private void UpdatePathsControls(TabAction action)
    {
      switch(action)
      {
        case TabAction.GetGui:
          OptionsManager.HaloPC_MapsPath = this.buttonEditHaloPCMaps.Text;
          OptionsManager.HaloCE_MapsPath = this.buttonEditHaloCEMaps.Text;
          OptionsManager.XHalo1_MapsPath = this.buttonEditHalo1XboxMaps.Text;
          OptionsManager.XHalo2_MapsPath = this.buttonEditHalo2XboxMaps.Text;
          break;

        case TabAction.SetGui:
          this.buttonEditHaloPCMaps.Text = OptionsManager.HaloPC_MapsPath;
          this.buttonEditHaloCEMaps.Text = OptionsManager.HaloCE_MapsPath;
          this.buttonEditHalo1XboxMaps.Text = OptionsManager.XHalo1_MapsPath;
          this.buttonEditHalo2XboxMaps.Text = OptionsManager.XHalo2_MapsPath;
          break;

        case TabAction.Reset:
          OptionsManager.Core.Delete("Paths", "HaloPC_MapsPath");
          OptionsManager.Core.Delete("Paths", "HaloCE_MapsPath");
          OptionsManager.Core.Delete("Paths", "XHalo1_MapsPath");
          OptionsManager.Core.Delete("Paths", "XHalo2_MapsPath");
          this.buttonEditHaloPCMaps.Text = OptionsManager.HaloPC_MapsPath;
          this.buttonEditHaloCEMaps.Text = OptionsManager.HaloCE_MapsPath;
          this.buttonEditHalo1XboxMaps.Text = OptionsManager.XHalo1_MapsPath;
          this.buttonEditHalo2XboxMaps.Text = OptionsManager.XHalo2_MapsPath;
          break;
      }
    }

    private void buttonEditHalo1XboxMaps_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
    {
      FolderBrowserDialog fbd = new FolderBrowserDialog();
      fbd.ShowNewFolderButton = false;
      fbd.SelectedPath = Application.StartupPath;

      if(fbd.ShowDialog() != DialogResult.Cancel)
      {
        this.buttonEditHalo1XboxMaps.Text = fbd.SelectedPath;
      }
    }
    private void buttonEditHaloPCMaps_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
    {
      FolderBrowserDialog fbd = new FolderBrowserDialog();
      fbd.ShowNewFolderButton = false;
      fbd.SelectedPath = Application.StartupPath;

      if(fbd.ShowDialog() != DialogResult.Cancel)
      {
        this.buttonEditHaloPCMaps.Text = fbd.SelectedPath;
      }
    }
    private void buttonEditHaloCEMaps_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
    {
      FolderBrowserDialog fbd = new FolderBrowserDialog();
      fbd.ShowNewFolderButton = false;
      fbd.SelectedPath = Application.StartupPath;

      if(fbd.ShowDialog() != DialogResult.Cancel)
      {
        this.buttonEditHaloCEMaps.Text = fbd.SelectedPath;
      }
    }
    private void buttonEditHalo2XboxMaps_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
    {
      FolderBrowserDialog fbd = new FolderBrowserDialog();
      fbd.ShowNewFolderButton = false;
      fbd.SelectedPath = Application.StartupPath;

      if(fbd.ShowDialog() != DialogResult.Cancel)
      {
        this.buttonEditHalo2XboxMaps.Text = fbd.SelectedPath;
      }
    }
    #endregion

    #region Logging Tab
    private void UpdateLoggingControls(TabAction action)
    {
      switch(action)
      {
        case TabAction.GetGui:
          OptionsManager.EnableDebugLogging = xchkDebugLogging.Checked;
          OptionsManager.MaxLogfileSize = (int)xseDiskUsage.Value;
          if(xcbLogDetail.SelectedIndex == 0)
            OptionsManager.LogDetail = "All Logging";
          if(xcbLogDetail.SelectedIndex == 1)
            OptionsManager.LogDetail = "Errors & Warnings";
          else
            OptionsManager.LogDetail = "Critical Errors";
          break;

        case TabAction.SetGui:
          xchkDebugLogging.Checked = OptionsManager.EnableDebugLogging;
          xseDiskUsage.Value = OptionsManager.MaxLogfileSize;

          if(OptionsManager.LogDetail == "All Logging")
            xcbLogDetail.SelectedIndex = 0;
          else if(OptionsManager.LogDetail == "Errors & Warnings")
            xcbLogDetail.SelectedIndex = 1;
          else //"Critical Errors"
            xcbLogDetail.SelectedIndex = 2;
          break;

        case TabAction.Reset:
          OptionsManager.Core.Delete("Logging","Debug Logging");
          OptionsManager.Core.Delete("Logging","Disk Size");
          OptionsManager.Core.Delete("Logging","LogDetail");
          xchkDebugLogging.Checked = OptionsManager.EnableDebugLogging;
          xseDiskUsage.Value = OptionsManager.MaxLogfileSize;
          break;
      }
    }
    private void xchkDebugLogging_CheckedChanged(object sender, System.EventArgs e)
    {
      lblDebugLoggingInfo.Enabled = xchkDebugLogging.Checked;
      xgcDiskFileLimits.Enabled = xchkDebugLogging.Checked;
      lblLogDetail.Enabled = xchkDebugLogging.Checked;
      xcbLogDetail.Enabled = xchkDebugLogging.Checked;

      ValueChanged();
    }
    #endregion

    #region Render Tab
    private void UpdateRenderControls(TabAction action)
    {
      switch(action)
      {
        case TabAction.GetGui:
          OptionsManager.CameraSensitivity = (int)spinEditCameraSensitivity.Value;
          OptionsManager.CameraSpeed = (int)spinEditCameraSpeed.Value;
          OptionsManager.EnableFPS = checkEditShowFPS.Checked;
          break;

        case TabAction.SetGui:
          spinEditCameraSensitivity.Value = OptionsManager.CameraSensitivity;
          spinEditCameraSpeed.Value = OptionsManager.CameraSpeed;
          checkEditShowFPS.Checked = OptionsManager.EnableFPS;
          break;

        case TabAction.Reset:
          OptionsManager.Core.Delete("Render","CameraSensitivity");
          OptionsManager.Core.Delete("Render","CameraSpeed");
          OptionsManager.Core.Delete("Render","EnableFPS");
          spinEditCameraSensitivity.Value = OptionsManager.CameraSensitivity;
          spinEditCameraSpeed.Value = OptionsManager.CameraSpeed;
          checkEditShowFPS.Checked = OptionsManager.EnableFPS;
          break;
      }
    }
    private void spinEditCameraSensitivity_TextChanged(object sender, System.EventArgs e)
    {
      OptionsManager.CameraSensitivity = (int)spinEditCameraSensitivity.Value;    
    }
    private void spinEditCameraSpeed_TextChanged(object sender, System.EventArgs e)
    {
      OptionsManager.CameraSpeed = (int)spinEditCameraSpeed.Value;
    }
    private void checkEditShowFPS_CheckedChanged(object sender, System.EventArgs e)
    {
      OptionsManager.EnableFPS = checkEditShowFPS.Checked;
    }
    #endregion
    private void OptionsDialog_Load(object sender, System.EventArgs e)
    {
      UpdateTab(OptionsTab.AllTabs);
    }

    private void buttonEditHalo2MainMenu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
    {
    
    }

    private void spinEditRotationToolScale_EditValueChanged(object sender, System.EventArgs e)
    {
    
    }

    private void spinEditTranslationToolScale_EditValueChanged(object sender, System.EventArgs e)
    {
    
    }
	}
}

