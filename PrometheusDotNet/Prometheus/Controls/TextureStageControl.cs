using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.DirectX.Direct3D;

namespace Prometheus.Controls
{
  public delegate void StateChangeHandler();
	/// <summary>
	/// Summary description for TextureStageControl.
	/// </summary>
	public class TextureStageControl : DevExpress.XtraEditors.XtraUserControl
	{
    private System.Windows.Forms.ComboBox comboBoxColorArg1;
    private System.Windows.Forms.ComboBox comboBoxColorArg2;
    private System.Windows.Forms.ComboBox comboBoxColorOp;
    private System.Windows.Forms.ComboBox comboBoxAlphaOp;
    private System.Windows.Forms.ComboBox comboBoxAlphaArg1;
    private System.Windows.Forms.ComboBox comboBoxAlphaArg2;

    public event StateChangeHandler StateChanged;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TextureStageControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call
      comboBoxColorArg1.SelectedIndex = 1;
      comboBoxColorArg2.SelectedIndex = 1;
      comboBoxColorOp.SelectedIndex = 20;
      comboBoxAlphaOp.SelectedIndex = 20;
      comboBoxAlphaArg1.SelectedIndex = 1;
      comboBoxAlphaArg2.SelectedIndex = 1;
    }

    public TextureArgument ColorArg1
    {
      get
      {
        TextureArgument ta = TextureArgument.Current;

        switch(comboBoxColorArg1.SelectedIndex)
        {
          case 0:
            ta = TextureArgument.TextureColor | TextureArgument.AlphaReplicate;
            break;
          case 1:
            ta = TextureArgument.TextureColor;
            break;
          case 2:
            ta = TextureArgument.Current;
            break;
          case 3:
            ta = TextureArgument.Diffuse;
            break;
          case 4:
            ta = TextureArgument.TextureColor | TextureArgument.AlphaReplicate | TextureArgument.Complement;
            break;
          case 5:
            ta = TextureArgument.TextureColor | TextureArgument.Complement;
            break;
          case 6:
            ta = TextureArgument.Current | TextureArgument.Complement;
            break;
          case 7:
            ta = TextureArgument.Diffuse | TextureArgument.Complement;
            break;
        }

        return(ta);
      }
    }
    public TextureArgument AlphaArg1
    {
      get
      {
        TextureArgument ta = TextureArgument.Current;

        switch(comboBoxAlphaArg1.SelectedIndex)
        {
          case 0:
            ta = TextureArgument.TextureColor | TextureArgument.AlphaReplicate;
            break;
          case 1:
            ta = TextureArgument.TextureColor;
            break;
          case 2:
            ta = TextureArgument.Current;
            break;
          case 3:
            ta = TextureArgument.Diffuse;
            break;
          case 4:
            ta = TextureArgument.TextureColor | TextureArgument.AlphaReplicate | TextureArgument.Complement;
            break;
          case 5:
            ta = TextureArgument.TextureColor | TextureArgument.Complement;
            break;
          case 6:
            ta = TextureArgument.Current | TextureArgument.Complement;
            break;
          case 7:
            ta = TextureArgument.Diffuse | TextureArgument.Complement;
            break;
        }

        return(ta);
      }
    }
    public TextureArgument AlphaArg2
    {
      get
      {
        TextureArgument ta = TextureArgument.Current;

        switch(comboBoxAlphaArg2.SelectedIndex)
        {
          case 0:
            ta = TextureArgument.TextureColor | TextureArgument.AlphaReplicate;
            break;
          case 1:
            ta = TextureArgument.TextureColor;
            break;
          case 2:
            ta = TextureArgument.Current;
            break;
          case 3:
            ta = TextureArgument.Diffuse;
            break;
          case 4:
            ta = TextureArgument.AlphaReplicate | TextureArgument.Complement;
            break;
          case 5:
            ta = TextureArgument.TextureColor | TextureArgument.Complement;
            break;
          case 6:
            ta = TextureArgument.Current | TextureArgument.Complement;
            break;
          case 7:
            ta = TextureArgument.Diffuse | TextureArgument.Complement;
            break;
        }

        return(ta);
      }
    }

    public TextureArgument ColorArg2
    {
      get
      {
        TextureArgument ta = TextureArgument.Current;

        switch(comboBoxColorArg2.SelectedIndex)
        {
          case 0:
            ta = TextureArgument.TextureColor | TextureArgument.AlphaReplicate;
            break;
          case 1:
            ta = TextureArgument.TextureColor;
            break;
          case 2:
            ta = TextureArgument.Current;
            break;
          case 3:
            ta = TextureArgument.Diffuse;
            break;
          case 4:
            ta = TextureArgument.TextureColor | TextureArgument.AlphaReplicate | TextureArgument.Complement;
            break;
          case 5:
            ta = TextureArgument.TextureColor | TextureArgument.Complement;
            break;
          case 6:
            ta = TextureArgument.Current | TextureArgument.Complement;
            break;
          case 7:
            ta = TextureArgument.Diffuse | TextureArgument.Complement;
            break;
        }

        return(ta);
      }
    }

    public TextureOperation ColorOp
    {
      get
      {
        TextureOperation ta = TextureOperation.Disable;

        switch(comboBoxColorOp.SelectedIndex)
        {
          case 0:
            ta = TextureOperation.Subtract;
            break;
          case 1:
            ta = TextureOperation.MultiplyAdd;
            break;
          case 2:
            ta = TextureOperation.DotProduct3;
            break;
          case 3:
            ta = TextureOperation.ModulateInvColorAddAlpha;
            break;
          case 4:
            ta = TextureOperation.ModulateInvAlphaAddColor;
            break;
          case 5:
            ta = TextureOperation.ModulateColorAddAlpha;
            break;
          case 6:
            ta = TextureOperation.ModulateAlphaAddColor;
            break;
          case 7:
            ta = TextureOperation.PreModulate;
            break;
          case 8:
            ta = TextureOperation.BlendCurrentAlpha;
            break;
          case 9:
            ta = TextureOperation.BlendTextureAlphaPM;
            break;
          case 10:
            ta = TextureOperation.BlendFactorAlpha;
            break;
          case 11:
            ta = TextureOperation.BlendTextureAlpha;
            break;
          case 12:
            ta = TextureOperation.BlendDiffuseAlpha;
            break;
          case 13:
            ta = TextureOperation.AddSigned2X;
            break;
          case 14:
            ta = TextureOperation.AddSigned;
            break;
          case 15:
            ta = TextureOperation.Add;
            break;
          case 16:
            ta = TextureOperation.Modulate2X;
            break;
          case 17:
            ta = TextureOperation.Modulate;
            break;
          case 18:
            ta = TextureOperation.SelectArg2;
            break;
          case 19:
            ta = TextureOperation.SelectArg1;
            break;
          case 20:
            ta = TextureOperation.Disable;
            break;
          case 21:
            ta = TextureOperation.BumpEnvironmentMap;
            break;
          case 22:
            ta = TextureOperation.BumpEnvironmentMapLuminance;
            break;
        }
        return(ta);
      }
    }

    public TextureOperation AlphaOp
    {
      get
      {
        TextureOperation ta = TextureOperation.Disable;

        switch(comboBoxAlphaOp.SelectedIndex)
        {
          case 0:
            ta = TextureOperation.Subtract;
            break;
          case 1:
            ta = TextureOperation.MultiplyAdd;
            break;
          case 2:
            ta = TextureOperation.DotProduct3;
            break;
          case 3:
            ta = TextureOperation.ModulateInvColorAddAlpha;
            break;
          case 4:
            ta = TextureOperation.ModulateInvAlphaAddColor;
            break;
          case 5:
            ta = TextureOperation.ModulateColorAddAlpha;
            break;
          case 6:
            ta = TextureOperation.ModulateAlphaAddColor;
            break;
          case 7:
            ta = TextureOperation.PreModulate;
            break;
          case 8:
            ta = TextureOperation.BlendCurrentAlpha;
            break;
          case 9:
            ta = TextureOperation.BlendTextureAlphaPM;
            break;
          case 10:
            ta = TextureOperation.BlendFactorAlpha;
            break;
          case 11:
            ta = TextureOperation.BlendTextureAlpha;
            break;
          case 12:
            ta = TextureOperation.BlendDiffuseAlpha;
            break;
          case 13:
            ta = TextureOperation.AddSigned2X;
            break;
          case 14:
            ta = TextureOperation.AddSigned;
            break;
          case 15:
            ta = TextureOperation.Add;
            break;
          case 16:
            ta = TextureOperation.Modulate2X;
            break;
          case 17:
            ta = TextureOperation.Modulate;
            break;
          case 18:
            ta = TextureOperation.SelectArg2;
            break;
          case 19:
            ta = TextureOperation.SelectArg1;
            break;
          case 20:
            ta = TextureOperation.Disable;
            break;
          case 21:
            ta = TextureOperation.BumpEnvironmentMap;
            break;
          case 22:
            ta = TextureOperation.BumpEnvironmentMapLuminance;
            break;
        }
        return(ta);
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
      this.comboBoxColorArg1 = new System.Windows.Forms.ComboBox();
      this.comboBoxColorArg2 = new System.Windows.Forms.ComboBox();
      this.comboBoxColorOp = new System.Windows.Forms.ComboBox();
      this.comboBoxAlphaOp = new System.Windows.Forms.ComboBox();
      this.comboBoxAlphaArg1 = new System.Windows.Forms.ComboBox();
      this.comboBoxAlphaArg2 = new System.Windows.Forms.ComboBox();
      this.SuspendLayout();
      // 
      // comboBoxColorArg1
      // 
      this.comboBoxColorArg1.Items.AddRange(new object[] {
                                                           "AlphaReplicate",
                                                           "TextureColor",
                                                           "Current",
                                                           "Diffuse",
                                                           "AlphaReplicateInv",
                                                           "TextureColorInv",
                                                           "CurrentInv",
                                                           "DiffuseInv"});
      this.comboBoxColorArg1.Location = new System.Drawing.Point(16, 8);
      this.comboBoxColorArg1.Name = "comboBoxColorArg1";
      this.comboBoxColorArg1.Size = new System.Drawing.Size(128, 21);
      this.comboBoxColorArg1.TabIndex = 0;
      this.comboBoxColorArg1.Text = "comboBoxColorArg1";
      this.comboBoxColorArg1.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
      // 
      // comboBoxColorArg2
      // 
      this.comboBoxColorArg2.Items.AddRange(new object[] {
                                                           "AlphaReplicate",
                                                           "TextureColor",
                                                           "Current",
                                                           "Diffuse",
                                                           "AlphaReplicateInv",
                                                           "TextureColorInv",
                                                           "CurrentInv",
                                                           "DiffuseInv"});
      this.comboBoxColorArg2.Location = new System.Drawing.Point(16, 40);
      this.comboBoxColorArg2.Name = "comboBoxColorArg2";
      this.comboBoxColorArg2.Size = new System.Drawing.Size(128, 21);
      this.comboBoxColorArg2.TabIndex = 1;
      this.comboBoxColorArg2.Text = "comboBoxColorArg2";
      this.comboBoxColorArg2.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
      // 
      // comboBoxColorOp
      // 
      this.comboBoxColorOp.Items.AddRange(new object[] {
                                                         "Subtract",
                                                         "MultiplyAdd",
                                                         "DotProduct3",
                                                         "ModulateInvColorAddAlpha",
                                                         "ModulateInvAlphaAddColor",
                                                         "ModulateColorAddAlpha",
                                                         "ModulateAlphaAddColor",
                                                         "PreModulate",
                                                         "BlendCurrentAlpha",
                                                         "BlendTextureAlphaPM",
                                                         "BlendFactorAlpha",
                                                         "BlendTextureAlpha",
                                                         "BlendDiffuseAlpha",
                                                         "AddSigned2x",
                                                         "AddSigned",
                                                         "Add",
                                                         "Modulate2x",
                                                         "Modulate",
                                                         "SelectArg2",
                                                         "SelectArg1",
                                                         "Disable",
                                                         "BumpEnvMap",
                                                         "BumpEnvMapLum"});
      this.comboBoxColorOp.Location = new System.Drawing.Point(16, 72);
      this.comboBoxColorOp.Name = "comboBoxColorOp";
      this.comboBoxColorOp.Size = new System.Drawing.Size(128, 21);
      this.comboBoxColorOp.TabIndex = 2;
      this.comboBoxColorOp.Text = "comboBoxColorOp";
      this.comboBoxColorOp.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
      // 
      // comboBoxAlphaOp
      // 
      this.comboBoxAlphaOp.Items.AddRange(new object[] {
                                                         "Subtract",
                                                         "MultiplyAdd",
                                                         "DotProduct3",
                                                         "ModulateInvColorAddAlpha",
                                                         "ModulateInvAlphaAddColor",
                                                         "ModulateColorAddAlpha",
                                                         "ModulateAlphaAddColor",
                                                         "PreModulate",
                                                         "BlendCurrentAlpha",
                                                         "BlendTextureAlphaPM",
                                                         "BlendFactorAlpha",
                                                         "BlendTextureAlpha",
                                                         "BlendDiffuseAlpha",
                                                         "AddSigned2x",
                                                         "AddSigned",
                                                         "Add",
                                                         "Modulate2x",
                                                         "Modulate",
                                                         "SelectArg2",
                                                         "SelectArg1",
                                                         "Disable",
                                                         "BumpEnvMap",
                                                         "BumpEnvMapLum"});
      this.comboBoxAlphaOp.Location = new System.Drawing.Point(16, 176);
      this.comboBoxAlphaOp.Name = "comboBoxAlphaOp";
      this.comboBoxAlphaOp.Size = new System.Drawing.Size(128, 21);
      this.comboBoxAlphaOp.TabIndex = 3;
      this.comboBoxAlphaOp.Text = "comboBoxAlphaOp";
      this.comboBoxAlphaOp.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
      // 
      // comboBoxAlphaArg1
      // 
      this.comboBoxAlphaArg1.Items.AddRange(new object[] {
                                                           "AlphaReplicate",
                                                           "TextureColor",
                                                           "Current",
                                                           "Diffuse",
                                                           "AlphaReplicateInv",
                                                           "TextureColorInv",
                                                           "CurrentInv",
                                                           "DiffuseInv"});
      this.comboBoxAlphaArg1.Location = new System.Drawing.Point(16, 112);
      this.comboBoxAlphaArg1.Name = "comboBoxAlphaArg1";
      this.comboBoxAlphaArg1.Size = new System.Drawing.Size(128, 21);
      this.comboBoxAlphaArg1.TabIndex = 4;
      this.comboBoxAlphaArg1.Text = "comboBoxAlphaArg1";
      this.comboBoxAlphaArg1.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
      // 
      // comboBoxAlphaArg2
      // 
      this.comboBoxAlphaArg2.Items.AddRange(new object[] {
                                                           "AlphaReplicate",
                                                           "TextureColor",
                                                           "Current",
                                                           "Diffuse",
                                                           "AlphaReplicateInv",
                                                           "TextureColorInv",
                                                           "CurrentInv",
                                                           "DiffuseInv"});
      this.comboBoxAlphaArg2.Location = new System.Drawing.Point(16, 144);
      this.comboBoxAlphaArg2.Name = "comboBoxAlphaArg2";
      this.comboBoxAlphaArg2.Size = new System.Drawing.Size(128, 21);
      this.comboBoxAlphaArg2.TabIndex = 5;
      this.comboBoxAlphaArg2.Text = "comboBoxAlphaArg2";
      this.comboBoxAlphaArg2.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
      // 
      // TextureStageControl
      // 
      this.Controls.Add(this.comboBoxAlphaArg2);
      this.Controls.Add(this.comboBoxAlphaArg1);
      this.Controls.Add(this.comboBoxAlphaOp);
      this.Controls.Add(this.comboBoxColorOp);
      this.Controls.Add(this.comboBoxColorArg2);
      this.Controls.Add(this.comboBoxColorArg1);
      this.Name = "TextureStageControl";
      this.Size = new System.Drawing.Size(152, 208);
      this.Load += new System.EventHandler(this.TextureStageControl_Load);
      this.ResumeLayout(false);

    }
		#endregion

    private void TextureStageControl_Load(object sender, System.EventArgs e)
    {
    
    }

    private void comboBox_SelectedIndexChanged(object sender, System.EventArgs e)
    {
      if(StateChanged != null)
        StateChanged();
    }
	}
}

