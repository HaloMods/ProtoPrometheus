using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Prometheus.Controls
{
	/// <summary>
	/// Summary description for RenderControls.
	/// </summary>
	public class ModelRenderAdvanced : DevExpress.XtraEditors.XtraUserControl
	{
    private DevExpress.XtraEditors.SimpleButton simpleButtonOpenShader;
    private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditShaders;
    private DevExpress.XtraEditors.GroupControl groupControlVisualAttributes;
    private DevExpress.XtraEditors.CheckEdit checkEditNormalmaps;
    private DevExpress.XtraEditors.CheckEdit checkEditBumpmaps;
    private DevExpress.XtraEditors.CheckEdit checkEditShowMarkers;
    private DevExpress.XtraEditors.CheckEdit checkEditShowBones;
    private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditModelPermutation;
    private System.Windows.Forms.Label labelShaders;
    private System.Windows.Forms.Label labelModelPermutation;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ModelRenderAdvanced()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call

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
      this.simpleButtonOpenShader = new DevExpress.XtraEditors.SimpleButton();
      this.comboBoxEditShaders = new DevExpress.XtraEditors.ComboBoxEdit();
      this.groupControlVisualAttributes = new DevExpress.XtraEditors.GroupControl();
      this.checkEditNormalmaps = new DevExpress.XtraEditors.CheckEdit();
      this.checkEditBumpmaps = new DevExpress.XtraEditors.CheckEdit();
      this.checkEditShowMarkers = new DevExpress.XtraEditors.CheckEdit();
      this.checkEditShowBones = new DevExpress.XtraEditors.CheckEdit();
      this.comboBoxEditModelPermutation = new DevExpress.XtraEditors.ComboBoxEdit();
      this.labelShaders = new System.Windows.Forms.Label();
      this.labelModelPermutation = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditShaders.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.groupControlVisualAttributes)).BeginInit();
      this.groupControlVisualAttributes.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.checkEditNormalmaps.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.checkEditBumpmaps.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.checkEditShowMarkers.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.checkEditShowBones.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditModelPermutation.Properties)).BeginInit();
      this.SuspendLayout();
      // 
      // simpleButtonOpenShader
      // 
      this.simpleButtonOpenShader.Location = new System.Drawing.Point(164, 126);
      this.simpleButtonOpenShader.Name = "simpleButtonOpenShader";
      this.simpleButtonOpenShader.Size = new System.Drawing.Size(46, 20);
      this.simpleButtonOpenShader.TabIndex = 11;
      this.simpleButtonOpenShader.Text = "Open";
      // 
      // comboBoxEditShaders
      // 
      this.comboBoxEditShaders.EditValue = "warthog hull.shader_model";
      this.comboBoxEditShaders.Location = new System.Drawing.Point(4, 126);
      this.comboBoxEditShaders.Name = "comboBoxEditShaders";
      // 
      // comboBoxEditShaders.Properties
      // 
      this.comboBoxEditShaders.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.comboBoxEditShaders.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
      this.comboBoxEditShaders.Size = new System.Drawing.Size(158, 20);
      this.comboBoxEditShaders.TabIndex = 10;
      // 
      // groupControlVisualAttributes
      // 
      this.groupControlVisualAttributes.Controls.Add(this.checkEditNormalmaps);
      this.groupControlVisualAttributes.Controls.Add(this.checkEditBumpmaps);
      this.groupControlVisualAttributes.Controls.Add(this.checkEditShowMarkers);
      this.groupControlVisualAttributes.Controls.Add(this.checkEditShowBones);
      this.groupControlVisualAttributes.Location = new System.Drawing.Point(4, 4);
      this.groupControlVisualAttributes.Name = "groupControlVisualAttributes";
      this.groupControlVisualAttributes.Size = new System.Drawing.Size(208, 72);
      this.groupControlVisualAttributes.TabIndex = 6;
      this.groupControlVisualAttributes.Text = "Visual Attributes";
      // 
      // checkEditNormalmaps
      // 
      this.checkEditNormalmaps.Location = new System.Drawing.Point(100, 48);
      this.checkEditNormalmaps.Name = "checkEditNormalmaps";
      // 
      // checkEditNormalmaps.Properties
      // 
      this.checkEditNormalmaps.Properties.Caption = "Use Normal Maps";
      this.checkEditNormalmaps.Size = new System.Drawing.Size(104, 19);
      this.checkEditNormalmaps.TabIndex = 3;
      // 
      // checkEditBumpmaps
      // 
      this.checkEditBumpmaps.Location = new System.Drawing.Point(100, 24);
      this.checkEditBumpmaps.Name = "checkEditBumpmaps";
      // 
      // checkEditBumpmaps.Properties
      // 
      this.checkEditBumpmaps.Properties.Caption = "Use Bump Maps";
      this.checkEditBumpmaps.Size = new System.Drawing.Size(98, 19);
      this.checkEditBumpmaps.TabIndex = 2;
      // 
      // checkEditShowMarkers
      // 
      this.checkEditShowMarkers.Location = new System.Drawing.Point(6, 48);
      this.checkEditShowMarkers.Name = "checkEditShowMarkers";
      // 
      // checkEditShowMarkers.Properties
      // 
      this.checkEditShowMarkers.Properties.Caption = "Show Markers";
      this.checkEditShowMarkers.Size = new System.Drawing.Size(90, 19);
      this.checkEditShowMarkers.TabIndex = 1;
      // 
      // checkEditShowBones
      // 
      this.checkEditShowBones.Location = new System.Drawing.Point(6, 24);
      this.checkEditShowBones.Name = "checkEditShowBones";
      // 
      // checkEditShowBones.Properties
      // 
      this.checkEditShowBones.Properties.Caption = "Show Bones";
      this.checkEditShowBones.Size = new System.Drawing.Size(82, 19);
      this.checkEditShowBones.TabIndex = 0;
      // 
      // comboBoxEditModelPermutation
      // 
      this.comboBoxEditModelPermutation.EditValue = "__base";
      this.comboBoxEditModelPermutation.Location = new System.Drawing.Point(4, 90);
      this.comboBoxEditModelPermutation.Name = "comboBoxEditModelPermutation";
      // 
      // comboBoxEditModelPermutation.Properties
      // 
      this.comboBoxEditModelPermutation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                                         new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.comboBoxEditModelPermutation.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
      this.comboBoxEditModelPermutation.Size = new System.Drawing.Size(206, 20);
      this.comboBoxEditModelPermutation.TabIndex = 8;
      // 
      // labelShaders
      // 
      this.labelShaders.BackColor = System.Drawing.Color.Transparent;
      this.labelShaders.Location = new System.Drawing.Point(4, 112);
      this.labelShaders.Name = "labelShaders";
      this.labelShaders.Size = new System.Drawing.Size(104, 16);
      this.labelShaders.TabIndex = 9;
      this.labelShaders.Text = "Shaders";
      // 
      // labelModelPermutation
      // 
      this.labelModelPermutation.BackColor = System.Drawing.Color.Transparent;
      this.labelModelPermutation.Location = new System.Drawing.Point(4, 76);
      this.labelModelPermutation.Name = "labelModelPermutation";
      this.labelModelPermutation.Size = new System.Drawing.Size(104, 16);
      this.labelModelPermutation.TabIndex = 7;
      this.labelModelPermutation.Text = "Model Permutation";
      // 
      // ModelRenderAdvanced
      // 
      this.Controls.Add(this.simpleButtonOpenShader);
      this.Controls.Add(this.comboBoxEditShaders);
      this.Controls.Add(this.groupControlVisualAttributes);
      this.Controls.Add(this.comboBoxEditModelPermutation);
      this.Controls.Add(this.labelShaders);
      this.Controls.Add(this.labelModelPermutation);
      this.Name = "ModelRenderAdvanced";
      this.Size = new System.Drawing.Size(216, 152);
      ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditShaders.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.groupControlVisualAttributes)).EndInit();
      this.groupControlVisualAttributes.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.checkEditNormalmaps.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.checkEditBumpmaps.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.checkEditShowMarkers.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.checkEditShowBones.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditModelPermutation.Properties)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion
	}
}

