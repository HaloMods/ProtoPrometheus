using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Prometheus.Core.Render;
using Prometheus.Core;

namespace Prometheus.Controls
{
	/// <summary>
	/// Summary description for RenderControls.
	/// </summary>
	public class MapRenderAdvanced : DevExpress.XtraEditors.XtraUserControl
	{
    private DevExpress.XtraEditors.GroupControl groupControlVisualAttributes;
    private DevExpress.XtraEditors.CheckEdit checkEditDrawTransGeom;
    private DevExpress.XtraEditors.CheckEdit checkEditShowFPS;
    private DevExpress.XtraEditors.CheckEdit checkEditDrawLightmaps;
    private DevExpress.XtraEditors.CheckEdit checkEditShowLights;
    private DevExpress.XtraEditors.GroupControl groupControlObjectPlacement;
    private DevExpress.XtraEditors.CheckEdit checkEditSnapMarkers;
    private DevExpress.XtraEditors.SimpleButton simpleButtonSnapNormal;
    private System.Windows.Forms.Label labelAngNudge;
    private DevExpress.XtraEditors.TextEdit textEditTransNudgeValue;
    private System.Windows.Forms.Label labelTransNudge;
    private DevExpress.XtraEditors.TextEdit textEditAngNudgeValue;
    private System.Windows.Forms.Label labelCameraX;
    private System.Windows.Forms.Label labelCameraY;
    private System.Windows.Forms.Label labelCameraZ;
    private DevExpress.XtraEditors.SimpleButton simpleButtonGo;
    private DevExpress.XtraEditors.TextEdit textEditYJump;
    private DevExpress.XtraEditors.TextEdit textEditXJump;
    private DevExpress.XtraEditors.TextEdit textEditZJump;
    private System.Windows.Forms.Label labelJumpTo;
    private DevExpress.XtraEditors.GroupControl groupControlRandomize;
    private DevExpress.XtraEditors.CheckEdit checkEditYaw;
    private DevExpress.XtraEditors.CheckEdit checkEditPitch;
    private DevExpress.XtraEditors.CheckEdit checkEditRoll;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MapRenderAdvanced()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call
      checkEditShowFPS.Checked = OptionsManager.EnableFPS;
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
      this.groupControlVisualAttributes = new DevExpress.XtraEditors.GroupControl();
      this.checkEditShowLights = new DevExpress.XtraEditors.CheckEdit();
      this.checkEditDrawLightmaps = new DevExpress.XtraEditors.CheckEdit();
      this.checkEditDrawTransGeom = new DevExpress.XtraEditors.CheckEdit();
      this.checkEditShowFPS = new DevExpress.XtraEditors.CheckEdit();
      this.groupControlObjectPlacement = new DevExpress.XtraEditors.GroupControl();
      this.textEditAngNudgeValue = new DevExpress.XtraEditors.TextEdit();
      this.textEditTransNudgeValue = new DevExpress.XtraEditors.TextEdit();
      this.labelTransNudge = new System.Windows.Forms.Label();
      this.labelAngNudge = new System.Windows.Forms.Label();
      this.simpleButtonSnapNormal = new DevExpress.XtraEditors.SimpleButton();
      this.checkEditSnapMarkers = new DevExpress.XtraEditors.CheckEdit();
      this.labelJumpTo = new System.Windows.Forms.Label();
      this.labelCameraX = new System.Windows.Forms.Label();
      this.labelCameraY = new System.Windows.Forms.Label();
      this.labelCameraZ = new System.Windows.Forms.Label();
      this.textEditYJump = new DevExpress.XtraEditors.TextEdit();
      this.textEditXJump = new DevExpress.XtraEditors.TextEdit();
      this.textEditZJump = new DevExpress.XtraEditors.TextEdit();
      this.simpleButtonGo = new DevExpress.XtraEditors.SimpleButton();
      this.groupControlRandomize = new DevExpress.XtraEditors.GroupControl();
      this.checkEditRoll = new DevExpress.XtraEditors.CheckEdit();
      this.checkEditPitch = new DevExpress.XtraEditors.CheckEdit();
      this.checkEditYaw = new DevExpress.XtraEditors.CheckEdit();
      ((System.ComponentModel.ISupportInitialize)(this.groupControlVisualAttributes)).BeginInit();
      this.groupControlVisualAttributes.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.checkEditShowLights.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.checkEditDrawLightmaps.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.checkEditDrawTransGeom.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.checkEditShowFPS.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.groupControlObjectPlacement)).BeginInit();
      this.groupControlObjectPlacement.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.textEditAngNudgeValue.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.textEditTransNudgeValue.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.checkEditSnapMarkers.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.textEditYJump.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.textEditXJump.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.textEditZJump.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.groupControlRandomize)).BeginInit();
      this.groupControlRandomize.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.checkEditRoll.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.checkEditPitch.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.checkEditYaw.Properties)).BeginInit();
      this.SuspendLayout();
      // 
      // groupControlVisualAttributes
      // 
      this.groupControlVisualAttributes.Controls.Add(this.checkEditShowLights);
      this.groupControlVisualAttributes.Controls.Add(this.checkEditDrawLightmaps);
      this.groupControlVisualAttributes.Controls.Add(this.checkEditDrawTransGeom);
      this.groupControlVisualAttributes.Controls.Add(this.checkEditShowFPS);
      this.groupControlVisualAttributes.Location = new System.Drawing.Point(4, 4);
      this.groupControlVisualAttributes.Name = "groupControlVisualAttributes";
      this.groupControlVisualAttributes.Size = new System.Drawing.Size(272, 72);
      this.groupControlVisualAttributes.TabIndex = 7;
      this.groupControlVisualAttributes.Text = "Visual Attributes";
      // 
      // checkEditShowLights
      // 
      this.checkEditShowLights.Location = new System.Drawing.Point(168, 48);
      this.checkEditShowLights.Name = "checkEditShowLights";
      // 
      // checkEditShowLights.Properties
      // 
      this.checkEditShowLights.Properties.Caption = "Show Lights";
      this.checkEditShowLights.Size = new System.Drawing.Size(84, 21);
      this.checkEditShowLights.TabIndex = 3;
      // 
      // checkEditDrawLightmaps
      // 
      this.checkEditDrawLightmaps.Location = new System.Drawing.Point(168, 24);
      this.checkEditDrawLightmaps.Name = "checkEditDrawLightmaps";
      // 
      // checkEditDrawLightmaps.Properties
      // 
      this.checkEditDrawLightmaps.Properties.Caption = "Draw Lightmaps";
      this.checkEditDrawLightmaps.Size = new System.Drawing.Size(98, 21);
      this.checkEditDrawLightmaps.TabIndex = 2;
      // 
      // checkEditDrawTransGeom
      // 
      this.checkEditDrawTransGeom.Location = new System.Drawing.Point(6, 48);
      this.checkEditDrawTransGeom.Name = "checkEditDrawTransGeom";
      // 
      // checkEditDrawTransGeom.Properties
      // 
      this.checkEditDrawTransGeom.Properties.Caption = "Draw Transparent Geometry";
      this.checkEditDrawTransGeom.Size = new System.Drawing.Size(158, 21);
      this.checkEditDrawTransGeom.TabIndex = 1;
      // 
      // checkEditShowFPS
      // 
      this.checkEditShowFPS.Location = new System.Drawing.Point(6, 24);
      this.checkEditShowFPS.Name = "checkEditShowFPS";
      // 
      // checkEditShowFPS.Properties
      // 
      this.checkEditShowFPS.Properties.Caption = "Show FPS";
      this.checkEditShowFPS.Size = new System.Drawing.Size(82, 21);
      this.checkEditShowFPS.TabIndex = 0;
      this.checkEditShowFPS.CheckedChanged += new System.EventHandler(this.checkEditShowFPS_CheckedChanged);
      // 
      // groupControlObjectPlacement
      // 
      this.groupControlObjectPlacement.Controls.Add(this.textEditAngNudgeValue);
      this.groupControlObjectPlacement.Controls.Add(this.textEditTransNudgeValue);
      this.groupControlObjectPlacement.Controls.Add(this.labelTransNudge);
      this.groupControlObjectPlacement.Controls.Add(this.labelAngNudge);
      this.groupControlObjectPlacement.Controls.Add(this.simpleButtonSnapNormal);
      this.groupControlObjectPlacement.Controls.Add(this.checkEditSnapMarkers);
      this.groupControlObjectPlacement.Location = new System.Drawing.Point(4, 120);
      this.groupControlObjectPlacement.Name = "groupControlObjectPlacement";
      this.groupControlObjectPlacement.Size = new System.Drawing.Size(272, 72);
      this.groupControlObjectPlacement.TabIndex = 8;
      this.groupControlObjectPlacement.Text = "Object Placement";
      // 
      // textEditAngNudgeValue
      // 
      this.textEditAngNudgeValue.EditValue = "0";
      this.textEditAngNudgeValue.Location = new System.Drawing.Point(214, 24);
      this.textEditAngNudgeValue.Name = "textEditAngNudgeValue";
      // 
      // textEditAngNudgeValue.Properties
      // 
      this.textEditAngNudgeValue.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
      this.textEditAngNudgeValue.Properties.DisplayFormat.FormatString = "0";
      this.textEditAngNudgeValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
      this.textEditAngNudgeValue.Properties.EditFormat.FormatString = "0";
      this.textEditAngNudgeValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
      this.textEditAngNudgeValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.textEditAngNudgeValue.Size = new System.Drawing.Size(52, 22);
      this.textEditAngNudgeValue.TabIndex = 8;
      // 
      // textEditTransNudgeValue
      // 
      this.textEditTransNudgeValue.EditValue = "0.00";
      this.textEditTransNudgeValue.Location = new System.Drawing.Point(214, 46);
      this.textEditTransNudgeValue.Name = "textEditTransNudgeValue";
      // 
      // textEditTransNudgeValue.Properties
      // 
      this.textEditTransNudgeValue.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
      this.textEditTransNudgeValue.Properties.DisplayFormat.FormatString = "0.00";
      this.textEditTransNudgeValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
      this.textEditTransNudgeValue.Properties.EditFormat.FormatString = "0.00";
      this.textEditTransNudgeValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
      this.textEditTransNudgeValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.textEditTransNudgeValue.Size = new System.Drawing.Size(52, 22);
      this.textEditTransNudgeValue.TabIndex = 7;
      // 
      // labelTransNudge
      // 
      this.labelTransNudge.Font = new System.Drawing.Font("Tahoma", 8.25F);
      this.labelTransNudge.Location = new System.Drawing.Point(126, 50);
      this.labelTransNudge.Name = "labelTransNudge";
      this.labelTransNudge.Size = new System.Drawing.Size(88, 14);
      this.labelTransNudge.TabIndex = 6;
      this.labelTransNudge.Text = "Translate Nudge";
      // 
      // labelAngNudge
      // 
      this.labelAngNudge.Font = new System.Drawing.Font("Tahoma", 8.25F);
      this.labelAngNudge.Location = new System.Drawing.Point(134, 27);
      this.labelAngNudge.Name = "labelAngNudge";
      this.labelAngNudge.Size = new System.Drawing.Size(80, 14);
      this.labelAngNudge.TabIndex = 5;
      this.labelAngNudge.Text = "Angular Nudge";
      // 
      // simpleButtonSnapNormal
      // 
      this.simpleButtonSnapNormal.Location = new System.Drawing.Point(6, 48);
      this.simpleButtonSnapNormal.Name = "simpleButtonSnapNormal";
      this.simpleButtonSnapNormal.Size = new System.Drawing.Size(106, 20);
      this.simpleButtonSnapNormal.TabIndex = 4;
      this.simpleButtonSnapNormal.Text = "Snap to Normal";
      // 
      // checkEditSnapMarkers
      // 
      this.checkEditSnapMarkers.Location = new System.Drawing.Point(6, 24);
      this.checkEditSnapMarkers.Name = "checkEditSnapMarkers";
      // 
      // checkEditSnapMarkers.Properties
      // 
      this.checkEditSnapMarkers.Properties.Caption = "Snap to Markers";
      this.checkEditSnapMarkers.Size = new System.Drawing.Size(106, 21);
      this.checkEditSnapMarkers.TabIndex = 0;
      // 
      // labelJumpTo
      // 
      this.labelJumpTo.Font = new System.Drawing.Font("Tahoma", 8.25F);
      this.labelJumpTo.Location = new System.Drawing.Point(6, 80);
      this.labelJumpTo.Name = "labelJumpTo";
      this.labelJumpTo.Size = new System.Drawing.Size(80, 14);
      this.labelJumpTo.TabIndex = 9;
      this.labelJumpTo.Text = "Jump to:";
      // 
      // labelCameraX
      // 
      this.labelCameraX.CausesValidation = false;
      this.labelCameraX.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
      this.labelCameraX.Location = new System.Drawing.Point(8, 98);
      this.labelCameraX.Name = "labelCameraX";
      this.labelCameraX.Size = new System.Drawing.Size(16, 16);
      this.labelCameraX.TabIndex = 47;
      this.labelCameraX.Text = "X:";
      this.labelCameraX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // labelCameraY
      // 
      this.labelCameraY.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
      this.labelCameraY.Location = new System.Drawing.Point(84, 98);
      this.labelCameraY.Name = "labelCameraY";
      this.labelCameraY.Size = new System.Drawing.Size(16, 16);
      this.labelCameraY.TabIndex = 45;
      this.labelCameraY.Text = "Y:";
      this.labelCameraY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // labelCameraZ
      // 
      this.labelCameraZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
      this.labelCameraZ.Location = new System.Drawing.Point(159, 98);
      this.labelCameraZ.Name = "labelCameraZ";
      this.labelCameraZ.Size = new System.Drawing.Size(14, 16);
      this.labelCameraZ.TabIndex = 46;
      this.labelCameraZ.Text = "Z:";
      this.labelCameraZ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // textEditYJump
      // 
      this.textEditYJump.EditValue = "0";
      this.textEditYJump.Location = new System.Drawing.Point(100, 96);
      this.textEditYJump.Name = "textEditYJump";
      // 
      // textEditYJump.Properties
      // 
      this.textEditYJump.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
      this.textEditYJump.Properties.DisplayFormat.FormatString = "#.##";
      this.textEditYJump.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
      this.textEditYJump.Properties.EditFormat.FormatString = "#.##";
      this.textEditYJump.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
      this.textEditYJump.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.textEditYJump.Size = new System.Drawing.Size(54, 22);
      this.textEditYJump.TabIndex = 48;
      // 
      // textEditXJump
      // 
      this.textEditXJump.EditValue = "0";
      this.textEditXJump.Location = new System.Drawing.Point(24, 96);
      this.textEditXJump.Name = "textEditXJump";
      // 
      // textEditXJump.Properties
      // 
      this.textEditXJump.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
      this.textEditXJump.Properties.DisplayFormat.FormatString = "#.##";
      this.textEditXJump.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
      this.textEditXJump.Properties.EditFormat.FormatString = "#.##";
      this.textEditXJump.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
      this.textEditXJump.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.textEditXJump.Size = new System.Drawing.Size(54, 22);
      this.textEditXJump.TabIndex = 10;
      // 
      // textEditZJump
      // 
      this.textEditZJump.EditValue = "0";
      this.textEditZJump.Location = new System.Drawing.Point(174, 96);
      this.textEditZJump.Name = "textEditZJump";
      // 
      // textEditZJump.Properties
      // 
      this.textEditZJump.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
      this.textEditZJump.Properties.DisplayFormat.FormatString = "#.##";
      this.textEditZJump.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
      this.textEditZJump.Properties.EditFormat.FormatString = "#.##";
      this.textEditZJump.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
      this.textEditZJump.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.textEditZJump.Size = new System.Drawing.Size(54, 22);
      this.textEditZJump.TabIndex = 49;
      // 
      // simpleButtonGo
      // 
      this.simpleButtonGo.Location = new System.Drawing.Point(232, 94);
      this.simpleButtonGo.Name = "simpleButtonGo";
      this.simpleButtonGo.Size = new System.Drawing.Size(42, 23);
      this.simpleButtonGo.TabIndex = 50;
      this.simpleButtonGo.Text = "Go";
      this.simpleButtonGo.Click += new System.EventHandler(this.simpleButtonGo_Click);
      // 
      // groupControlRandomize
      // 
      this.groupControlRandomize.Controls.Add(this.checkEditRoll);
      this.groupControlRandomize.Controls.Add(this.checkEditPitch);
      this.groupControlRandomize.Controls.Add(this.checkEditYaw);
      this.groupControlRandomize.Location = new System.Drawing.Point(4, 196);
      this.groupControlRandomize.Name = "groupControlRandomize";
      this.groupControlRandomize.Size = new System.Drawing.Size(272, 50);
      this.groupControlRandomize.TabIndex = 51;
      this.groupControlRandomize.Text = "During Object Creation, Randomize";
      // 
      // checkEditRoll
      // 
      this.checkEditRoll.Location = new System.Drawing.Point(220, 24);
      this.checkEditRoll.Name = "checkEditRoll";
      // 
      // checkEditRoll.Properties
      // 
      this.checkEditRoll.Properties.Caption = "Roll";
      this.checkEditRoll.Size = new System.Drawing.Size(46, 21);
      this.checkEditRoll.TabIndex = 2;
      // 
      // checkEditPitch
      // 
      this.checkEditPitch.Location = new System.Drawing.Point(112, 24);
      this.checkEditPitch.Name = "checkEditPitch";
      // 
      // checkEditPitch.Properties
      // 
      this.checkEditPitch.Properties.Caption = "Pitch";
      this.checkEditPitch.Size = new System.Drawing.Size(46, 21);
      this.checkEditPitch.TabIndex = 1;
      // 
      // checkEditYaw
      // 
      this.checkEditYaw.Location = new System.Drawing.Point(6, 24);
      this.checkEditYaw.Name = "checkEditYaw";
      // 
      // checkEditYaw.Properties
      // 
      this.checkEditYaw.Properties.Caption = "Yaw";
      this.checkEditYaw.Size = new System.Drawing.Size(44, 21);
      this.checkEditYaw.TabIndex = 0;
      // 
      // MapRenderAdvanced
      // 
      this.Controls.Add(this.groupControlRandomize);
      this.Controls.Add(this.simpleButtonGo);
      this.Controls.Add(this.textEditZJump);
      this.Controls.Add(this.textEditYJump);
      this.Controls.Add(this.labelCameraX);
      this.Controls.Add(this.labelCameraY);
      this.Controls.Add(this.labelCameraZ);
      this.Controls.Add(this.textEditXJump);
      this.Controls.Add(this.labelJumpTo);
      this.Controls.Add(this.groupControlObjectPlacement);
      this.Controls.Add(this.groupControlVisualAttributes);
      this.Name = "MapRenderAdvanced";
      this.Size = new System.Drawing.Size(280, 252);
      ((System.ComponentModel.ISupportInitialize)(this.groupControlVisualAttributes)).EndInit();
      this.groupControlVisualAttributes.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.checkEditShowLights.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.checkEditDrawLightmaps.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.checkEditDrawTransGeom.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.checkEditShowFPS.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.groupControlObjectPlacement)).EndInit();
      this.groupControlObjectPlacement.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.textEditAngNudgeValue.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.textEditTransNudgeValue.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.checkEditSnapMarkers.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.textEditYJump.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.textEditXJump.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.textEditZJump.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.groupControlRandomize)).EndInit();
      this.groupControlRandomize.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.checkEditRoll.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.checkEditPitch.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.checkEditYaw.Properties)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

    private void simpleButtonGo_Click(object sender, System.EventArgs e)
    {
      Vector3 to = new Vector3();
      to.X = (float)Convert.ChangeType(textEditXJump.Text, typeof(float));
      to.Y = (float)Convert.ChangeType(textEditYJump.Text, typeof(float));
      to.Z = (float)Convert.ChangeType(textEditZJump.Text, typeof(float));

      Vector3 from = new Vector3(to.X+5, to.Y+5, to.Z);
      MdxRender.Camera.SetLookAt(from, to);
    }

    private void checkEditShowFPS_CheckedChanged(object sender, System.EventArgs e)
    {
      OptionsManager.EnableFPS = checkEditShowFPS.Checked;
      //todo:  update menu check?
    }
	}
}

