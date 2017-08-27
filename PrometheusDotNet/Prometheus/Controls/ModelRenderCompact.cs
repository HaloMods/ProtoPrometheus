using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.DirectX;
using Prometheus.Core.Render;
using Prometheus.Core;

namespace Prometheus.Controls
{
	/// <summary>
	/// Summary description for RenderControls.
	/// </summary>
	public class ModelRenderCompact : DevExpress.XtraEditors.XtraUserControl
	{
    private DevExpress.XtraEditors.SimpleButton simpleButtonRefocusModel;
    private DevExpress.XtraEditors.SimpleButton simpleButtonAdvanced;
    private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditAnimation;
    private System.Windows.Forms.Label labelCameraZValue;
    private System.Windows.Forms.Label labelCameraYValue;
    private System.Windows.Forms.Label labelCameraXValue;
    private System.Windows.Forms.Label labelCameraY;
    private System.Windows.Forms.Label labelCameraLocation;
    private System.Windows.Forms.Label labelCameraSensitivityDesc;
    private System.Windows.Forms.Label labelCameraSensitivityIndex;
    private System.Windows.Forms.Label labelPolygonUnit;
    private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditModelRenderMode;
    private System.Windows.Forms.Label labelCameraSensitivity;
    private DevExpress.XtraEditors.CheckEdit checkEditCollisionToggle;
    private System.Windows.Forms.Label labelPolygonCount;
    private System.Windows.Forms.Label labelPolygonCountValue;
    private System.Windows.Forms.Label labelCameraX;
    private System.Windows.Forms.Label labelCameraZ;
    private DevExpress.XtraBars.Docking.DockManager dockManager1;
    private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
    private DevExpress.XtraBars.Docking.DockPanel dockPanelModelRenderAdvDialog;

    // Custom Controls
    private ModelRenderAdvanced modelRenderAdv;

    /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ModelRenderCompact()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

      // Instantiate Advanced Render Controls
      modelRenderAdv = new ModelRenderAdvanced();

      // Initialize Advanced Render Controls
      modelRenderAdv.Location = new Point(0,0);
      modelRenderAdv.Size = dockPanelModelRenderAdvDialog.ClientSize;
      modelRenderAdv.Parent = dockPanelModelRenderAdvDialog;
      modelRenderAdv.Show();

		}
    public void UpdateContent()
    {
      if(MdxRender.Camera != null)
      {
        Vector3 cam_position = MdxRender.Camera.Position;

        SharedControls.Utility.UpdateSignedLabel(labelCameraXValue, cam_position.X);
        SharedControls.Utility.UpdateSignedLabel(labelCameraYValue, cam_position.Y);
        SharedControls.Utility.UpdateSignedLabel(labelCameraZValue, cam_position.Z);

        labelPolygonCountValue.Text = MdxRender.PreviewManager.GetActiveModelTriangleCount().ToString("#,###,###;Broken ;No ");
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
    } 
    public void UpdateControls()
    {
      string[] ani_list = MdxRender.PreviewManager.AnimationList;
      
      comboBoxEditAnimation.ResetText();
      if(ani_list != null)
      {
        //comboBoxEditAnimation.Properties.Items.Add("Animations");
        comboBoxEditAnimation.Properties.Items.AddRange(ani_list);
        comboBoxEditAnimation.SelectedIndex = 0;
        MdxRender.PreviewManager.AnimationIndex = 0;
        MdxRender.PreviewManager.Mode = PreviewMode.Animation;
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
      this.simpleButtonRefocusModel = new DevExpress.XtraEditors.SimpleButton();
      this.simpleButtonAdvanced = new DevExpress.XtraEditors.SimpleButton();
      this.labelPolygonCount = new System.Windows.Forms.Label();
      this.labelPolygonCountValue = new System.Windows.Forms.Label();
      this.comboBoxEditAnimation = new DevExpress.XtraEditors.ComboBoxEdit();
      this.labelCameraZValue = new System.Windows.Forms.Label();
      this.labelCameraYValue = new System.Windows.Forms.Label();
      this.labelCameraXValue = new System.Windows.Forms.Label();
      this.labelCameraX = new System.Windows.Forms.Label();
      this.labelCameraY = new System.Windows.Forms.Label();
      this.labelCameraZ = new System.Windows.Forms.Label();
      this.labelCameraLocation = new System.Windows.Forms.Label();
      this.labelCameraSensitivityDesc = new System.Windows.Forms.Label();
      this.labelCameraSensitivityIndex = new System.Windows.Forms.Label();
      this.labelCameraSensitivity = new System.Windows.Forms.Label();
      this.labelPolygonUnit = new System.Windows.Forms.Label();
      this.comboBoxEditModelRenderMode = new DevExpress.XtraEditors.ComboBoxEdit();
      this.checkEditCollisionToggle = new DevExpress.XtraEditors.CheckEdit();
      this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager();
      this.dockPanelModelRenderAdvDialog = new DevExpress.XtraBars.Docking.DockPanel();
      this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
      ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditAnimation.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditModelRenderMode.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.checkEditCollisionToggle.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
      this.dockPanelModelRenderAdvDialog.SuspendLayout();
      this.SuspendLayout();
      // 
      // simpleButtonRefocusModel
      // 
      this.simpleButtonRefocusModel.Location = new System.Drawing.Point(4, 46);
      this.simpleButtonRefocusModel.Name = "simpleButtonRefocusModel";
      this.simpleButtonRefocusModel.Size = new System.Drawing.Size(96, 22);
      this.simpleButtonRefocusModel.TabIndex = 22;
      this.simpleButtonRefocusModel.Text = "Refocus Model";
      this.simpleButtonRefocusModel.Click += new System.EventHandler(this.simpleButtonRefocusModel_Click);
      // 
      // simpleButtonAdvanced
      // 
      this.simpleButtonAdvanced.Location = new System.Drawing.Point(104, 46);
      this.simpleButtonAdvanced.Name = "simpleButtonAdvanced";
      this.simpleButtonAdvanced.Size = new System.Drawing.Size(75, 22);
      this.simpleButtonAdvanced.TabIndex = 23;
      this.simpleButtonAdvanced.Text = "Advanced";
      this.simpleButtonAdvanced.Click += new System.EventHandler(this.simpleButtonAdvanced_Click);
      // 
      // labelPolygonCount
      // 
      this.labelPolygonCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.labelPolygonCount.Location = new System.Drawing.Point(200, 38);
      this.labelPolygonCount.Name = "labelPolygonCount";
      this.labelPolygonCount.Size = new System.Drawing.Size(100, 16);
      this.labelPolygonCount.TabIndex = 27;
      this.labelPolygonCount.Text = "Polygon Count";
      // 
      // labelPolygonCountValue
      // 
      this.labelPolygonCountValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
      this.labelPolygonCountValue.ForeColor = System.Drawing.Color.Green;
      this.labelPolygonCountValue.Location = new System.Drawing.Point(200, 54);
      this.labelPolygonCountValue.Name = "labelPolygonCountValue";
      this.labelPolygonCountValue.Size = new System.Drawing.Size(40, 16);
      this.labelPolygonCountValue.TabIndex = 28;
      this.labelPolygonCountValue.Text = "12,578";
      this.labelPolygonCountValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // comboBoxEditAnimation
      // 
      this.comboBoxEditAnimation.EditValue = "Animation";
      this.comboBoxEditAnimation.Location = new System.Drawing.Point(340, 4);
      this.comboBoxEditAnimation.Name = "comboBoxEditAnimation";
      // 
      // comboBoxEditAnimation.Properties
      // 
      this.comboBoxEditAnimation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                                  new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.comboBoxEditAnimation.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
      this.comboBoxEditAnimation.Size = new System.Drawing.Size(90, 22);
      this.comboBoxEditAnimation.TabIndex = 29;
      this.comboBoxEditAnimation.SelectedIndexChanged += new System.EventHandler(this.comboBoxEditAnimation_SelectedIndexChanged);
      // 
      // labelCameraZValue
      // 
      this.labelCameraZValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
      this.labelCameraZValue.ForeColor = System.Drawing.Color.Red;
      this.labelCameraZValue.Location = new System.Drawing.Point(148, 20);
      this.labelCameraZValue.Name = "labelCameraZValue";
      this.labelCameraZValue.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.labelCameraZValue.Size = new System.Drawing.Size(48, 16);
      this.labelCameraZValue.TabIndex = 34;
      this.labelCameraZValue.Text = "-0,000.00";
      this.labelCameraZValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelCameraYValue
      // 
      this.labelCameraYValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
      this.labelCameraYValue.ForeColor = System.Drawing.Color.Red;
      this.labelCameraYValue.Location = new System.Drawing.Point(84, 20);
      this.labelCameraYValue.Name = "labelCameraYValue";
      this.labelCameraYValue.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.labelCameraYValue.Size = new System.Drawing.Size(48, 16);
      this.labelCameraYValue.TabIndex = 35;
      this.labelCameraYValue.Text = "-0,000.00";
      this.labelCameraYValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelCameraXValue
      // 
      this.labelCameraXValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
      this.labelCameraXValue.ForeColor = System.Drawing.Color.Red;
      this.labelCameraXValue.Location = new System.Drawing.Point(18, 20);
      this.labelCameraXValue.Name = "labelCameraXValue";
      this.labelCameraXValue.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.labelCameraXValue.Size = new System.Drawing.Size(48, 16);
      this.labelCameraXValue.TabIndex = 36;
      this.labelCameraXValue.Text = "-0,000.00";
      this.labelCameraXValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelCameraX
      // 
      this.labelCameraX.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
      this.labelCameraX.Location = new System.Drawing.Point(4, 20);
      this.labelCameraX.Name = "labelCameraX";
      this.labelCameraX.Size = new System.Drawing.Size(16, 16);
      this.labelCameraX.TabIndex = 37;
      this.labelCameraX.Text = "X:";
      this.labelCameraX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // labelCameraY
      // 
      this.labelCameraY.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
      this.labelCameraY.Location = new System.Drawing.Point(70, 20);
      this.labelCameraY.Name = "labelCameraY";
      this.labelCameraY.Size = new System.Drawing.Size(18, 16);
      this.labelCameraY.TabIndex = 32;
      this.labelCameraY.Text = "Y:";
      this.labelCameraY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // labelCameraZ
      // 
      this.labelCameraZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
      this.labelCameraZ.Location = new System.Drawing.Point(136, 20);
      this.labelCameraZ.Name = "labelCameraZ";
      this.labelCameraZ.Size = new System.Drawing.Size(16, 16);
      this.labelCameraZ.TabIndex = 33;
      this.labelCameraZ.Text = "Z:";
      this.labelCameraZ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // labelCameraLocation
      // 
      this.labelCameraLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.labelCameraLocation.Location = new System.Drawing.Point(4, 4);
      this.labelCameraLocation.Name = "labelCameraLocation";
      this.labelCameraLocation.Size = new System.Drawing.Size(114, 16);
      this.labelCameraLocation.TabIndex = 31;
      this.labelCameraLocation.Text = "Camera Location";
      // 
      // labelCameraSensitivityDesc
      // 
      this.labelCameraSensitivityDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Italic);
      this.labelCameraSensitivityDesc.Location = new System.Drawing.Point(218, 20);
      this.labelCameraSensitivityDesc.Name = "labelCameraSensitivityDesc";
      this.labelCameraSensitivityDesc.Size = new System.Drawing.Size(72, 16);
      this.labelCameraSensitivityDesc.TabIndex = 40;
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
      this.labelCameraSensitivityIndex.Size = new System.Drawing.Size(18, 16);
      this.labelCameraSensitivityIndex.TabIndex = 39;
      this.labelCameraSensitivityIndex.Text = "12";
      this.labelCameraSensitivityIndex.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelCameraSensitivity
      // 
      this.labelCameraSensitivity.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.labelCameraSensitivity.Location = new System.Drawing.Point(200, 4);
      this.labelCameraSensitivity.Name = "labelCameraSensitivity";
      this.labelCameraSensitivity.Size = new System.Drawing.Size(122, 16);
      this.labelCameraSensitivity.TabIndex = 38;
      this.labelCameraSensitivity.Text = "Camera Sensitivity";
      // 
      // labelPolygonUnit
      // 
      this.labelPolygonUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
      this.labelPolygonUnit.ForeColor = System.Drawing.Color.Black;
      this.labelPolygonUnit.Location = new System.Drawing.Point(240, 54);
      this.labelPolygonUnit.Name = "labelPolygonUnit";
      this.labelPolygonUnit.Size = new System.Drawing.Size(62, 16);
      this.labelPolygonUnit.TabIndex = 41;
      this.labelPolygonUnit.Text = "polygons";
      this.labelPolygonUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // comboBoxEditModelRenderMode
      // 
      this.comboBoxEditModelRenderMode.EditValue = "Textures";
      this.comboBoxEditModelRenderMode.Location = new System.Drawing.Point(350, 50);
      this.comboBoxEditModelRenderMode.Name = "comboBoxEditModelRenderMode";
      // 
      // comboBoxEditModelRenderMode.Properties
      // 
      this.comboBoxEditModelRenderMode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                                        new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.comboBoxEditModelRenderMode.Properties.Items.AddRange(new object[] {
                                                                                "Textures",
                                                                                "Wireframe",
                                                                                "Overlay"});
      this.comboBoxEditModelRenderMode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
      this.comboBoxEditModelRenderMode.Size = new System.Drawing.Size(80, 22);
      this.comboBoxEditModelRenderMode.TabIndex = 42;
      this.comboBoxEditModelRenderMode.ToolTip = "Textures:   Render with textures. Simulates in-game view.\nWireframe: View only th" +
        "e wireframe of models.\nOverlay:     See textures with the wireframe of the model" +
        " laying over the top of them.";
      this.comboBoxEditModelRenderMode.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
      // 
      // checkEditCollisionToggle
      // 
      this.checkEditCollisionToggle.Location = new System.Drawing.Point(320, 28);
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
      this.checkEditCollisionToggle.Size = new System.Drawing.Size(112, 21);
      this.checkEditCollisionToggle.TabIndex = 43;
      // 
      // dockManager1
      // 
      this.dockManager1.Form = this;
      this.dockManager1.HiddenPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
                                                                                            this.dockPanelModelRenderAdvDialog});
      this.dockManager1.TopZIndexControls.AddRange(new string[] {
                                                                  "DevExpress.XtraBars.BarDockControl",
                                                                  "System.Windows.Forms.StatusBar"});
      // 
      // dockPanelModelRenderAdvDialog
      // 
      this.dockPanelModelRenderAdvDialog.Controls.Add(this.dockPanel1_Container);
      this.dockPanelModelRenderAdvDialog.Dock = DevExpress.XtraBars.Docking.DockingStyle.Float;
      this.dockPanelModelRenderAdvDialog.FloatLocation = new System.Drawing.Point(246, 208);
      this.dockPanelModelRenderAdvDialog.FloatSize = new System.Drawing.Size(219, 176);
      this.dockPanelModelRenderAdvDialog.FloatVertical = true;
      this.dockPanelModelRenderAdvDialog.ID = new System.Guid("04a294b6-97cd-46ff-bc93-549dea455c50");
      this.dockPanelModelRenderAdvDialog.Location = new System.Drawing.Point(246, 208);
      this.dockPanelModelRenderAdvDialog.Name = "dockPanelModelRenderAdvDialog";
      this.dockPanelModelRenderAdvDialog.Options.AllowDockBottom = false;
      this.dockPanelModelRenderAdvDialog.Options.AllowDockFill = false;
      this.dockPanelModelRenderAdvDialog.Options.AllowDockLeft = false;
      this.dockPanelModelRenderAdvDialog.Options.AllowDockRight = false;
      this.dockPanelModelRenderAdvDialog.Options.AllowDockTop = false;
      this.dockPanelModelRenderAdvDialog.Options.FloatOnDblClick = false;
      this.dockPanelModelRenderAdvDialog.Options.ShowAutoHideButton = false;
      this.dockPanelModelRenderAdvDialog.Options.ShowMaximizeButton = false;
      this.dockPanelModelRenderAdvDialog.SavedIndex = 0;
      this.dockPanelModelRenderAdvDialog.Size = new System.Drawing.Size(219, 176);
      this.dockPanelModelRenderAdvDialog.Text = "Advanced Model Render Controls";
      this.dockPanelModelRenderAdvDialog.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
      this.dockPanelModelRenderAdvDialog.Resize += new System.EventHandler(this.dockPanelModelRenderAdvDialog_Resize);
      // 
      // dockPanel1_Container
      // 
      this.dockPanel1_Container.Location = new System.Drawing.Point(2, 24);
      this.dockPanel1_Container.Name = "dockPanel1_Container";
      this.dockPanel1_Container.Size = new System.Drawing.Size(216, 164);
      this.dockPanel1_Container.TabIndex = 0;
      // 
      // ModelRenderCompact
      // 
      this.Controls.Add(this.comboBoxEditAnimation);
      this.Controls.Add(this.checkEditCollisionToggle);
      this.Controls.Add(this.comboBoxEditModelRenderMode);
      this.Controls.Add(this.labelPolygonUnit);
      this.Controls.Add(this.labelCameraSensitivityDesc);
      this.Controls.Add(this.labelCameraSensitivityIndex);
      this.Controls.Add(this.labelCameraSensitivity);
      this.Controls.Add(this.labelCameraZValue);
      this.Controls.Add(this.labelCameraYValue);
      this.Controls.Add(this.labelCameraXValue);
      this.Controls.Add(this.labelCameraX);
      this.Controls.Add(this.labelCameraY);
      this.Controls.Add(this.labelCameraZ);
      this.Controls.Add(this.labelCameraLocation);
      this.Controls.Add(this.labelPolygonCountValue);
      this.Controls.Add(this.labelPolygonCount);
      this.Controls.Add(this.simpleButtonAdvanced);
      this.Controls.Add(this.simpleButtonRefocusModel);
      this.Name = "ModelRenderCompact";
      this.Size = new System.Drawing.Size(436, 76);
      ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditAnimation.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditModelRenderMode.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.checkEditCollisionToggle.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
      this.dockPanelModelRenderAdvDialog.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion

    private void simpleButtonAdvanced_Click(object sender, System.EventArgs e)
    {
      dockPanelModelRenderAdvDialog.Show();    
    }

    private void dockPanelModelRenderAdvDialog_Resize(object sender, System.EventArgs e)
    {
      modelRenderAdv.Size = dockPanelModelRenderAdvDialog.ClientSize;
    }

    private void simpleButtonRefocusModel_Click(object sender, System.EventArgs e)
    {
      MdxRender.Camera.SetLookAt(new Vector3(-18, 0, 0), new Vector3());
    }

    private void comboBoxEditAnimation_SelectedIndexChanged(object sender, System.EventArgs e)
    {
      MdxRender.PreviewManager.AnimationIndex = comboBoxEditAnimation.SelectedIndex;
    }
	}
}

