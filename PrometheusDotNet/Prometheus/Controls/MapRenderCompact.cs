using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Prometheus.Core.Render;
using Microsoft.DirectX;
using Prometheus.Core.Project;
using Prometheus.Core;

namespace Prometheus.Controls
{
	/// <summary>
	/// Summary description for RenderControls.
	/// </summary>
	public class MapRenderCompact : DevExpress.XtraEditors.XtraUserControl
	{
    private System.Windows.Forms.Label labelPointerLocation;
    private System.Windows.Forms.Label labelPointerZ;
    private System.Windows.Forms.Label labelPointerXValue;
    private System.Windows.Forms.Label labelPointerYValue;
    private System.Windows.Forms.Label labelPointerZValue;
    private System.Windows.Forms.Label labelCameraLocation;
    private System.Windows.Forms.Label labelCameraY;
    private System.Windows.Forms.Label labelCameraXValue;
    private System.Windows.Forms.Label labelCameraYValue;
    private System.Windows.Forms.Label labelCameraZValue;
    private System.Windows.Forms.Label labelCameraSpeedIndex;
    private System.Windows.Forms.Label labelCameraSpeedDesc;
    private DevExpress.XtraEditors.SimpleButton simpleButtonAdvancedMapControls;
    private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditMapRenderMode;
    private System.Windows.Forms.Label labelCameraSensitivityDesc;
    private System.Windows.Forms.Label labelCameraSensitivityIndex;
    private System.Windows.Forms.Label labelPointerX;
    private System.Windows.Forms.Label labelPointerY;
    private System.Windows.Forms.Label labelCameraZ;
    private System.Windows.Forms.Label labelCameraX;
    private System.Windows.Forms.Label labelCameraSpeed;
    private System.Windows.Forms.Label labelCameraSensitivity;
    private DevExpress.XtraEditors.CheckEdit checkEditCollisionToggle;
    private DevExpress.XtraBars.Docking.DockManager dockManager1;
    private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
    private DevExpress.XtraBars.Docking.DockPanel dockPanelMapRenderAdvDialog;

    // Custom Controls
    private MapRenderAdvanced mapRenderAdv;
    private System.Windows.Forms.Label labelDebug;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MapRenderCompact()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

      // Instantiate Advanced Render Controls
      mapRenderAdv = new MapRenderAdvanced();

      // Initialize Advanced Render Controls
      mapRenderAdv.Location = new Point(0,0);
      mapRenderAdv.Size = dockPanelMapRenderAdvDialog.ClientSize;
      mapRenderAdv.Parent = dockPanelMapRenderAdvDialog;
      mapRenderAdv.Show();

		}
    public void UpdateContent()
    {
      if(MdxRender.Camera != null)
      {
        Vector3 cam_position = MdxRender.Camera.Position;

        SharedControls.Utility.UpdateSignedLabel(labelCameraXValue, cam_position.X);
        SharedControls.Utility.UpdateSignedLabel(labelCameraYValue, cam_position.Y);
        SharedControls.Utility.UpdateSignedLabel(labelCameraZValue, cam_position.Z);
      }

      if(ProjectManager.MapSpawns.ObjectSelected == false)
      {
        labelPointerLocation.Enabled = false;
        labelPointerX.Enabled = false;
        labelPointerY.Enabled = false;
        labelPointerZ.Enabled = false;
        labelPointerXValue.Text = "";
        labelPointerYValue.Text = "";
        labelPointerZValue.Text = "";
        labelPointerXValue.Enabled = false;
        labelPointerYValue.Enabled = false;
        labelPointerZValue.Enabled = false;
        this.labelDebug.Text = "";
      }
      else
      {
        Vector3 sel_pos = ProjectManager.MapSpawns.SelectionPosition;
        SharedControls.Utility.UpdateSignedLabel(labelPointerXValue, sel_pos.X);
        SharedControls.Utility.UpdateSignedLabel(labelPointerYValue, sel_pos.Y);
        SharedControls.Utility.UpdateSignedLabel(labelPointerZValue, sel_pos.Z);
        labelPointerLocation.Enabled = true;
        labelPointerX.Enabled = true;
        labelPointerY.Enabled = true;
        labelPointerZ.Enabled = true;
        labelPointerXValue.Enabled = true;
        labelPointerYValue.Enabled = true;
        labelPointerZValue.Enabled = true;

        Attitude rot = ProjectManager.MapSpawns.SelectionRotation;
        this.labelDebug.Text = string.Format("Selection Rot: pitch={0} roll={1} yaw={2}", rot.Pitch, rot.Roll, rot.Yaw);
      }

      labelCameraSensitivityIndex.Text = string.Format("{0}", OptionsManager.CameraSensitivity);
      
      if(OptionsManager.CameraSensitivity > 7)
      {
        labelCameraSensitivityDesc.Text = "(High)";
        labelCameraSensitivityIndex.ForeColor = Color.Red;
      }
      else if(OptionsManager.CameraSensitivity > 4)
      {
        labelCameraSensitivityDesc.Text = "(Medium)";
        labelCameraSensitivityIndex.ForeColor = Color.Green;
      }
      else
      {
        labelCameraSensitivityDesc.Text = "(Low)";
        labelCameraSensitivityIndex.ForeColor = Color.Blue;
      }

      labelCameraSpeedIndex.Text = OptionsManager.CameraSpeed.ToString();
      if(OptionsManager.CameraSpeed > 40)
      {
        labelCameraSpeedDesc.Text = "(High)";
        labelCameraSpeedIndex.ForeColor = Color.Red;
      }
      else if(OptionsManager.CameraSpeed > 20)
      {
        labelCameraSpeedDesc.Text = "(Medium)";
        labelCameraSpeedIndex.ForeColor = Color.Green;
      }
      else
      {
        labelCameraSpeedDesc.Text = "(Low)";
        labelCameraSpeedIndex.ForeColor = Color.Blue;
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.labelPointerLocation = new System.Windows.Forms.Label();
      this.labelPointerX = new System.Windows.Forms.Label();
      this.labelPointerY = new System.Windows.Forms.Label();
      this.labelPointerZ = new System.Windows.Forms.Label();
      this.labelPointerXValue = new System.Windows.Forms.Label();
      this.labelPointerYValue = new System.Windows.Forms.Label();
      this.labelPointerZValue = new System.Windows.Forms.Label();
      this.labelCameraLocation = new System.Windows.Forms.Label();
      this.labelCameraZ = new System.Windows.Forms.Label();
      this.labelCameraY = new System.Windows.Forms.Label();
      this.labelCameraXValue = new System.Windows.Forms.Label();
      this.labelCameraYValue = new System.Windows.Forms.Label();
      this.labelCameraZValue = new System.Windows.Forms.Label();
      this.labelCameraSpeedIndex = new System.Windows.Forms.Label();
      this.labelCameraSpeedDesc = new System.Windows.Forms.Label();
      this.labelCameraSpeed = new System.Windows.Forms.Label();
      this.simpleButtonAdvancedMapControls = new DevExpress.XtraEditors.SimpleButton();
      this.comboBoxEditMapRenderMode = new DevExpress.XtraEditors.ComboBoxEdit();
      this.labelCameraSensitivityDesc = new System.Windows.Forms.Label();
      this.labelCameraSensitivityIndex = new System.Windows.Forms.Label();
      this.labelCameraSensitivity = new System.Windows.Forms.Label();
      this.labelCameraX = new System.Windows.Forms.Label();
      this.checkEditCollisionToggle = new DevExpress.XtraEditors.CheckEdit();
      this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager();
      this.dockPanelMapRenderAdvDialog = new DevExpress.XtraBars.Docking.DockPanel();
      this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
      this.labelDebug = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditMapRenderMode.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.checkEditCollisionToggle.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
      this.dockPanelMapRenderAdvDialog.SuspendLayout();
      this.SuspendLayout();
      // 
      // labelPointerLocation
      // 
      this.labelPointerLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.labelPointerLocation.Location = new System.Drawing.Point(4, 4);
      this.labelPointerLocation.Name = "labelPointerLocation";
      this.labelPointerLocation.Size = new System.Drawing.Size(118, 16);
      this.labelPointerLocation.TabIndex = 0;
      this.labelPointerLocation.Text = "Selection Location";
      // 
      // labelPointerX
      // 
      this.labelPointerX.CausesValidation = false;
      this.labelPointerX.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
      this.labelPointerX.Location = new System.Drawing.Point(4, 20);
      this.labelPointerX.Name = "labelPointerX";
      this.labelPointerX.Size = new System.Drawing.Size(16, 16);
      this.labelPointerX.TabIndex = 1;
      this.labelPointerX.Text = "X:";
      this.labelPointerX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // labelPointerY
      // 
      this.labelPointerY.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
      this.labelPointerY.Location = new System.Drawing.Point(70, 20);
      this.labelPointerY.Name = "labelPointerY";
      this.labelPointerY.Size = new System.Drawing.Size(16, 16);
      this.labelPointerY.TabIndex = 2;
      this.labelPointerY.Text = "Y:";
      this.labelPointerY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // labelPointerZ
      // 
      this.labelPointerZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
      this.labelPointerZ.Location = new System.Drawing.Point(136, 20);
      this.labelPointerZ.Name = "labelPointerZ";
      this.labelPointerZ.Size = new System.Drawing.Size(14, 16);
      this.labelPointerZ.TabIndex = 3;
      this.labelPointerZ.Text = "Z:";
      this.labelPointerZ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // labelPointerXValue
      // 
      this.labelPointerXValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
      this.labelPointerXValue.ForeColor = System.Drawing.Color.Red;
      this.labelPointerXValue.Location = new System.Drawing.Point(18, 20);
      this.labelPointerXValue.Name = "labelPointerXValue";
      this.labelPointerXValue.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.labelPointerXValue.Size = new System.Drawing.Size(48, 16);
      this.labelPointerXValue.TabIndex = 4;
      this.labelPointerXValue.Text = "-0,000.00";
      this.labelPointerXValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelPointerYValue
      // 
      this.labelPointerYValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
      this.labelPointerYValue.ForeColor = System.Drawing.Color.Red;
      this.labelPointerYValue.Location = new System.Drawing.Point(84, 20);
      this.labelPointerYValue.Name = "labelPointerYValue";
      this.labelPointerYValue.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.labelPointerYValue.Size = new System.Drawing.Size(48, 16);
      this.labelPointerYValue.TabIndex = 5;
      this.labelPointerYValue.Text = "-0,000.00";
      this.labelPointerYValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelPointerZValue
      // 
      this.labelPointerZValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
      this.labelPointerZValue.ForeColor = System.Drawing.Color.Red;
      this.labelPointerZValue.Location = new System.Drawing.Point(148, 20);
      this.labelPointerZValue.Name = "labelPointerZValue";
      this.labelPointerZValue.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.labelPointerZValue.Size = new System.Drawing.Size(48, 16);
      this.labelPointerZValue.TabIndex = 6;
      this.labelPointerZValue.Text = "-0,000.00";
      this.labelPointerZValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelCameraLocation
      // 
      this.labelCameraLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.labelCameraLocation.Location = new System.Drawing.Point(4, 40);
      this.labelCameraLocation.Name = "labelCameraLocation";
      this.labelCameraLocation.Size = new System.Drawing.Size(114, 16);
      this.labelCameraLocation.TabIndex = 7;
      this.labelCameraLocation.Text = "Camera Location";
      // 
      // labelCameraZ
      // 
      this.labelCameraZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
      this.labelCameraZ.Location = new System.Drawing.Point(136, 56);
      this.labelCameraZ.Name = "labelCameraZ";
      this.labelCameraZ.Size = new System.Drawing.Size(14, 16);
      this.labelCameraZ.TabIndex = 10;
      this.labelCameraZ.Text = "Z:";
      this.labelCameraZ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // labelCameraY
      // 
      this.labelCameraY.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
      this.labelCameraY.Location = new System.Drawing.Point(70, 56);
      this.labelCameraY.Name = "labelCameraY";
      this.labelCameraY.Size = new System.Drawing.Size(16, 16);
      this.labelCameraY.TabIndex = 9;
      this.labelCameraY.Text = "Y:";
      this.labelCameraY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // labelCameraXValue
      // 
      this.labelCameraXValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
      this.labelCameraXValue.ForeColor = System.Drawing.Color.Red;
      this.labelCameraXValue.Location = new System.Drawing.Point(18, 56);
      this.labelCameraXValue.Name = "labelCameraXValue";
      this.labelCameraXValue.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.labelCameraXValue.Size = new System.Drawing.Size(48, 16);
      this.labelCameraXValue.TabIndex = 13;
      this.labelCameraXValue.Text = "-0,000.00";
      this.labelCameraXValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelCameraYValue
      // 
      this.labelCameraYValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
      this.labelCameraYValue.ForeColor = System.Drawing.Color.Red;
      this.labelCameraYValue.Location = new System.Drawing.Point(84, 56);
      this.labelCameraYValue.Name = "labelCameraYValue";
      this.labelCameraYValue.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.labelCameraYValue.Size = new System.Drawing.Size(48, 16);
      this.labelCameraYValue.TabIndex = 12;
      this.labelCameraYValue.Text = "-0,000.00";
      this.labelCameraYValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelCameraZValue
      // 
      this.labelCameraZValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
      this.labelCameraZValue.ForeColor = System.Drawing.Color.Red;
      this.labelCameraZValue.Location = new System.Drawing.Point(148, 56);
      this.labelCameraZValue.Name = "labelCameraZValue";
      this.labelCameraZValue.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.labelCameraZValue.Size = new System.Drawing.Size(48, 16);
      this.labelCameraZValue.TabIndex = 11;
      this.labelCameraZValue.Text = "-0,000.00";
      this.labelCameraZValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelCameraSpeedIndex
      // 
      this.labelCameraSpeedIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
      this.labelCameraSpeedIndex.ForeColor = System.Drawing.Color.Green;
      this.labelCameraSpeedIndex.Location = new System.Drawing.Point(200, 56);
      this.labelCameraSpeedIndex.Name = "labelCameraSpeedIndex";
      this.labelCameraSpeedIndex.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.labelCameraSpeedIndex.Size = new System.Drawing.Size(26, 16);
      this.labelCameraSpeedIndex.TabIndex = 18;
      this.labelCameraSpeedIndex.Text = "5x";
      this.labelCameraSpeedIndex.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelCameraSpeedDesc
      // 
      this.labelCameraSpeedDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
      this.labelCameraSpeedDesc.Location = new System.Drawing.Point(226, 56);
      this.labelCameraSpeedDesc.Name = "labelCameraSpeedDesc";
      this.labelCameraSpeedDesc.Size = new System.Drawing.Size(88, 16);
      this.labelCameraSpeedDesc.TabIndex = 19;
      this.labelCameraSpeedDesc.Text = "(12.0 wu/sec)";
      this.labelCameraSpeedDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // labelCameraSpeed
      // 
      this.labelCameraSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.labelCameraSpeed.Location = new System.Drawing.Point(200, 42);
      this.labelCameraSpeed.Name = "labelCameraSpeed";
      this.labelCameraSpeed.Size = new System.Drawing.Size(100, 16);
      this.labelCameraSpeed.TabIndex = 20;
      this.labelCameraSpeed.Text = "Camera Speed";
      // 
      // simpleButtonAdvancedMapControls
      // 
      this.simpleButtonAdvancedMapControls.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
      this.simpleButtonAdvancedMapControls.Appearance.Options.UseFont = true;
      this.simpleButtonAdvancedMapControls.Location = new System.Drawing.Point(346, 50);
      this.simpleButtonAdvancedMapControls.Name = "simpleButtonAdvancedMapControls";
      this.simpleButtonAdvancedMapControls.Size = new System.Drawing.Size(80, 22);
      this.simpleButtonAdvancedMapControls.TabIndex = 22;
      this.simpleButtonAdvancedMapControls.Text = "Advanced";
      this.simpleButtonAdvancedMapControls.Click += new System.EventHandler(this.simpleButtonAdvancedMapControls_Click);
      // 
      // comboBoxEditMapRenderMode
      // 
      this.comboBoxEditMapRenderMode.EditValue = "Textures";
      this.comboBoxEditMapRenderMode.Location = new System.Drawing.Point(346, 4);
      this.comboBoxEditMapRenderMode.Name = "comboBoxEditMapRenderMode";
      // 
      // comboBoxEditMapRenderMode.Properties
      // 
      this.comboBoxEditMapRenderMode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
      this.comboBoxEditMapRenderMode.Properties.Appearance.Options.UseFont = true;
      this.comboBoxEditMapRenderMode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                                      new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.comboBoxEditMapRenderMode.Properties.Items.AddRange(new object[] {
                                                                              "Textures",
                                                                              "Wireframe",
                                                                              "Overlay"});
      this.comboBoxEditMapRenderMode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
      this.comboBoxEditMapRenderMode.Size = new System.Drawing.Size(80, 19);
      this.comboBoxEditMapRenderMode.TabIndex = 23;
      this.comboBoxEditMapRenderMode.ToolTip = "Textures:   Render with textures. Simulates in-game view.\nWireframe: View only th" +
        "e wireframe of models.\nOverlay:     See textures with the wireframe of the model" +
        " laying over the top of them.";
      this.comboBoxEditMapRenderMode.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
      // 
      // labelCameraSensitivityDesc
      // 
      this.labelCameraSensitivityDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Italic);
      this.labelCameraSensitivityDesc.Location = new System.Drawing.Point(226, 20);
      this.labelCameraSensitivityDesc.Name = "labelCameraSensitivityDesc";
      this.labelCameraSensitivityDesc.Size = new System.Drawing.Size(72, 16);
      this.labelCameraSensitivityDesc.TabIndex = 43;
      this.labelCameraSensitivityDesc.Text = "(High)";
      this.labelCameraSensitivityDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // labelCameraSensitivityIndex
      // 
      this.labelCameraSensitivityIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
      this.labelCameraSensitivityIndex.ForeColor = System.Drawing.Color.Red;
      this.labelCameraSensitivityIndex.Location = new System.Drawing.Point(200, 20);
      this.labelCameraSensitivityIndex.Name = "labelCameraSensitivityIndex";
      this.labelCameraSensitivityIndex.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.labelCameraSensitivityIndex.Size = new System.Drawing.Size(26, 16);
      this.labelCameraSensitivityIndex.TabIndex = 42;
      this.labelCameraSensitivityIndex.Text = "12";
      this.labelCameraSensitivityIndex.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelCameraSensitivity
      // 
      this.labelCameraSensitivity.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.labelCameraSensitivity.Location = new System.Drawing.Point(200, 4);
      this.labelCameraSensitivity.Name = "labelCameraSensitivity";
      this.labelCameraSensitivity.Size = new System.Drawing.Size(124, 16);
      this.labelCameraSensitivity.TabIndex = 41;
      this.labelCameraSensitivity.Text = "Camera Sensitivity";
      // 
      // labelCameraX
      // 
      this.labelCameraX.CausesValidation = false;
      this.labelCameraX.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
      this.labelCameraX.Location = new System.Drawing.Point(4, 56);
      this.labelCameraX.Name = "labelCameraX";
      this.labelCameraX.Size = new System.Drawing.Size(16, 16);
      this.labelCameraX.TabIndex = 44;
      this.labelCameraX.Text = "X:";
      this.labelCameraX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // checkEditCollisionToggle
      // 
      this.checkEditCollisionToggle.Location = new System.Drawing.Point(320, 27);
      this.checkEditCollisionToggle.Name = "checkEditCollisionToggle";
      // 
      // checkEditCollisionToggle.Properties
      // 
      this.checkEditCollisionToggle.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
      this.checkEditCollisionToggle.Properties.Appearance.Options.UseFont = true;
      this.checkEditCollisionToggle.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
      this.checkEditCollisionToggle.Properties.Caption = "Draw Collision";
      this.checkEditCollisionToggle.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
      this.checkEditCollisionToggle.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.checkEditCollisionToggle.Size = new System.Drawing.Size(108, 19);
      this.checkEditCollisionToggle.TabIndex = 45;
      // 
      // dockManager1
      // 
      this.dockManager1.Form = this;
      this.dockManager1.HiddenPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
                                                                                            this.dockPanelMapRenderAdvDialog});
      this.dockManager1.TopZIndexControls.AddRange(new string[] {
                                                                  "DevExpress.XtraBars.BarDockControl",
                                                                  "System.Windows.Forms.StatusBar"});
      // 
      // dockPanelMapRenderAdvDialog
      // 
      this.dockPanelMapRenderAdvDialog.Controls.Add(this.dockPanel1_Container);
      this.dockPanelMapRenderAdvDialog.Dock = DevExpress.XtraBars.Docking.DockingStyle.Float;
      this.dockPanelMapRenderAdvDialog.FloatLocation = new System.Drawing.Point(246, 208);
      this.dockPanelMapRenderAdvDialog.FloatSize = new System.Drawing.Size(283, 276);
      this.dockPanelMapRenderAdvDialog.FloatVertical = true;
      this.dockPanelMapRenderAdvDialog.ID = new System.Guid("04a294b6-97cd-46ff-bc93-549dea455c50");
      this.dockPanelMapRenderAdvDialog.Location = new System.Drawing.Point(246, 208);
      this.dockPanelMapRenderAdvDialog.Name = "dockPanelMapRenderAdvDialog";
      this.dockPanelMapRenderAdvDialog.Options.AllowDockBottom = false;
      this.dockPanelMapRenderAdvDialog.Options.AllowDockFill = false;
      this.dockPanelMapRenderAdvDialog.Options.AllowDockLeft = false;
      this.dockPanelMapRenderAdvDialog.Options.AllowDockRight = false;
      this.dockPanelMapRenderAdvDialog.Options.AllowDockTop = false;
      this.dockPanelMapRenderAdvDialog.Options.FloatOnDblClick = false;
      this.dockPanelMapRenderAdvDialog.Options.ShowAutoHideButton = false;
      this.dockPanelMapRenderAdvDialog.Options.ShowMaximizeButton = false;
      this.dockPanelMapRenderAdvDialog.SavedIndex = 0;
      this.dockPanelMapRenderAdvDialog.Size = new System.Drawing.Size(219, 176);
      this.dockPanelMapRenderAdvDialog.Text = "Advanced Map Render Controls";
      this.dockPanelMapRenderAdvDialog.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
      this.dockPanelMapRenderAdvDialog.Resize += new System.EventHandler(this.dockPanelMapRenderAdvDialog_Resize);
      // 
      // dockPanel1_Container
      // 
      this.dockPanel1_Container.Location = new System.Drawing.Point(2, 24);
      this.dockPanel1_Container.Name = "dockPanel1_Container";
      this.dockPanel1_Container.Size = new System.Drawing.Size(216, 164);
      this.dockPanel1_Container.TabIndex = 0;
      // 
      // labelDebug
      // 
      this.labelDebug.Location = new System.Drawing.Point(8, 82);
      this.labelDebug.Name = "labelDebug";
      this.labelDebug.Size = new System.Drawing.Size(280, 23);
      this.labelDebug.TabIndex = 46;
      this.labelDebug.Text = "Debug";
      // 
      // MapRenderCompact
      // 
      this.Controls.Add(this.labelDebug);
      this.Controls.Add(this.comboBoxEditMapRenderMode);
      this.Controls.Add(this.checkEditCollisionToggle);
      this.Controls.Add(this.labelCameraX);
      this.Controls.Add(this.labelCameraSensitivityDesc);
      this.Controls.Add(this.labelCameraSensitivityIndex);
      this.Controls.Add(this.labelCameraSensitivity);
      this.Controls.Add(this.simpleButtonAdvancedMapControls);
      this.Controls.Add(this.labelCameraSpeed);
      this.Controls.Add(this.labelCameraSpeedDesc);
      this.Controls.Add(this.labelCameraSpeedIndex);
      this.Controls.Add(this.labelCameraZValue);
      this.Controls.Add(this.labelCameraYValue);
      this.Controls.Add(this.labelCameraXValue);
      this.Controls.Add(this.labelCameraY);
      this.Controls.Add(this.labelCameraZ);
      this.Controls.Add(this.labelCameraLocation);
      this.Controls.Add(this.labelPointerZValue);
      this.Controls.Add(this.labelPointerYValue);
      this.Controls.Add(this.labelPointerXValue);
      this.Controls.Add(this.labelPointerZ);
      this.Controls.Add(this.labelPointerY);
      this.Controls.Add(this.labelPointerX);
      this.Controls.Add(this.labelPointerLocation);
      this.Name = "MapRenderCompact";
      this.Size = new System.Drawing.Size(432, 110);
      ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditMapRenderMode.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.checkEditCollisionToggle.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
      this.dockPanelMapRenderAdvDialog.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion

    private void dockPanelMapRenderAdvDialog_Resize(object sender, System.EventArgs e)
    {
      mapRenderAdv.Size = dockPanelMapRenderAdvDialog.ClientSize;
    }

    private void simpleButtonAdvancedMapControls_Click(object sender, System.EventArgs e)
    {
      dockPanelMapRenderAdvDialog.Show(); 
    }
	}
}

