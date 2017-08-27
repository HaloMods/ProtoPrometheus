using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Prometheus.Core.Tags;
using Microsoft.DirectX.Direct3D;

namespace Prometheus
{
	/// <summary>
	/// Summary description for GrenDebugger.
	/// </summary>
	public class GrenDebugger : DevExpress.XtraEditors.XtraForm
	{
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.CheckBox checkBoxEnableShaderDebug;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private Prometheus.Controls.TextureStageControl textureStageControl0;
    private Prometheus.Controls.TextureStageControl textureStageControl1;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label8;
    private Prometheus.Controls.TextureStageControl textureStageControl2;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label10;
    private Prometheus.Controls.TextureStageControl textureStageControl3;
    private System.Windows.Forms.CheckBox checkBoxEnableAlphaBlend;
    private System.Windows.Forms.CheckBox checkBoxEnableAlphaTest;
    private System.Windows.Forms.ComboBox comboBoxSourceBlend;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.ComboBox comboBoxDestinationBlend;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public GrenDebugger()
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
      this.checkBoxEnableShaderDebug = new System.Windows.Forms.CheckBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.label12 = new System.Windows.Forms.Label();
      this.comboBoxDestinationBlend = new System.Windows.Forms.ComboBox();
      this.label11 = new System.Windows.Forms.Label();
      this.comboBoxSourceBlend = new System.Windows.Forms.ComboBox();
      this.checkBoxEnableAlphaTest = new System.Windows.Forms.CheckBox();
      this.label10 = new System.Windows.Forms.Label();
      this.textureStageControl3 = new Prometheus.Controls.TextureStageControl();
      this.label9 = new System.Windows.Forms.Label();
      this.textureStageControl2 = new Prometheus.Controls.TextureStageControl();
      this.label8 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.textureStageControl1 = new Prometheus.Controls.TextureStageControl();
      this.textureStageControl0 = new Prometheus.Controls.TextureStageControl();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.checkBoxEnableAlphaBlend = new System.Windows.Forms.CheckBox();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(24, 168);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(72, 23);
      this.label1.TabIndex = 1;
      this.label1.Text = "Color Arg1:";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // checkBoxEnableShaderDebug
      // 
      this.checkBoxEnableShaderDebug.Location = new System.Drawing.Point(32, 40);
      this.checkBoxEnableShaderDebug.Name = "checkBoxEnableShaderDebug";
      this.checkBoxEnableShaderDebug.Size = new System.Drawing.Size(160, 24);
      this.checkBoxEnableShaderDebug.TabIndex = 2;
      this.checkBoxEnableShaderDebug.Text = "Enable Shader Debug";
      this.checkBoxEnableShaderDebug.CheckedChanged += new System.EventHandler(this.checkBoxEnableShaderDebug_CheckedChanged);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.label12);
      this.groupBox1.Controls.Add(this.comboBoxDestinationBlend);
      this.groupBox1.Controls.Add(this.label11);
      this.groupBox1.Controls.Add(this.comboBoxSourceBlend);
      this.groupBox1.Controls.Add(this.checkBoxEnableAlphaTest);
      this.groupBox1.Controls.Add(this.label10);
      this.groupBox1.Controls.Add(this.textureStageControl3);
      this.groupBox1.Controls.Add(this.label9);
      this.groupBox1.Controls.Add(this.textureStageControl2);
      this.groupBox1.Controls.Add(this.label8);
      this.groupBox1.Controls.Add(this.label7);
      this.groupBox1.Controls.Add(this.label6);
      this.groupBox1.Controls.Add(this.label5);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.textureStageControl1);
      this.groupBox1.Controls.Add(this.textureStageControl0);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.checkBoxEnableAlphaBlend);
      this.groupBox1.Location = new System.Drawing.Point(16, 16);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(736, 392);
      this.groupBox1.TabIndex = 3;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Fixed Function Pipeline";
      // 
      // label12
      // 
      this.label12.Location = new System.Drawing.Point(400, 56);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(136, 23);
      this.label12.TabIndex = 22;
      this.label12.Text = "Destination Blend:";
      this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // comboBoxDestinationBlend
      // 
      this.comboBoxDestinationBlend.Items.AddRange(new object[] {
                                                                  "Zero",
                                                                  "InvBlendFactor",
                                                                  "BlendFactor",
                                                                  "BothInvSourceAlpha",
                                                                  "SourceAlphaSat",
                                                                  "InvDestinationColor",
                                                                  "DestinationColor",
                                                                  "InvDestinationAlpha",
                                                                  "DestinationAlpha",
                                                                  "InvSourceAlpha",
                                                                  "SourceAlpha",
                                                                  "InvSourceColor",
                                                                  "SourceColor",
                                                                  "One "});
      this.comboBoxDestinationBlend.Location = new System.Drawing.Point(544, 56);
      this.comboBoxDestinationBlend.Name = "comboBoxDestinationBlend";
      this.comboBoxDestinationBlend.Size = new System.Drawing.Size(176, 24);
      this.comboBoxDestinationBlend.TabIndex = 21;
      this.comboBoxDestinationBlend.Text = "comboBoxSourceBlend";
      this.comboBoxDestinationBlend.SelectedIndexChanged += new System.EventHandler(this.comboBoxDestinationBlend_SelectedIndexChanged);
      // 
      // label11
      // 
      this.label11.Location = new System.Drawing.Point(440, 24);
      this.label11.Name = "label11";
      this.label11.TabIndex = 20;
      this.label11.Text = "Source Blend:";
      this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // comboBoxSourceBlend
      // 
      this.comboBoxSourceBlend.Items.AddRange(new object[] {
                                                             "Zero",
                                                             "InvBlendFactor",
                                                             "BlendFactor",
                                                             "BothInvSourceAlpha",
                                                             "SourceAlphaSat",
                                                             "InvDestinationColor",
                                                             "DestinationColor",
                                                             "InvDestinationAlpha",
                                                             "DestinationAlpha",
                                                             "InvSourceAlpha",
                                                             "SourceAlpha",
                                                             "InvSourceColor",
                                                             "SourceColor",
                                                             "One "});
      this.comboBoxSourceBlend.Location = new System.Drawing.Point(544, 24);
      this.comboBoxSourceBlend.Name = "comboBoxSourceBlend";
      this.comboBoxSourceBlend.Size = new System.Drawing.Size(176, 24);
      this.comboBoxSourceBlend.TabIndex = 19;
      this.comboBoxSourceBlend.Text = "comboBoxSourceBlend";
      this.comboBoxSourceBlend.SelectedIndexChanged += new System.EventHandler(this.comboBoxSourceBlend_SelectedIndexChanged);
      // 
      // checkBoxEnableAlphaTest
      // 
      this.checkBoxEnableAlphaTest.Location = new System.Drawing.Point(104, 96);
      this.checkBoxEnableAlphaTest.Name = "checkBoxEnableAlphaTest";
      this.checkBoxEnableAlphaTest.Size = new System.Drawing.Size(160, 24);
      this.checkBoxEnableAlphaTest.TabIndex = 18;
      this.checkBoxEnableAlphaTest.Text = "Enable Alpha Test";
      // 
      // label10
      // 
      this.label10.Font = new System.Drawing.Font("Tahoma", 10F);
      this.label10.ForeColor = System.Drawing.Color.Blue;
      this.label10.Location = new System.Drawing.Point(592, 136);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(72, 23);
      this.label10.TabIndex = 17;
      this.label10.Text = "Stage 3";
      // 
      // textureStageControl3
      // 
      this.textureStageControl3.Location = new System.Drawing.Point(552, 160);
      this.textureStageControl3.Name = "textureStageControl3";
      this.textureStageControl3.Size = new System.Drawing.Size(152, 216);
      this.textureStageControl3.TabIndex = 16;
      this.textureStageControl3.StateChanged += new Prometheus.Controls.StateChangeHandler(this.textureStageControl3_StateChanged);
      // 
      // label9
      // 
      this.label9.Font = new System.Drawing.Font("Tahoma", 10F);
      this.label9.ForeColor = System.Drawing.Color.Blue;
      this.label9.Location = new System.Drawing.Point(440, 136);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(72, 23);
      this.label9.TabIndex = 15;
      this.label9.Text = "Stage 2";
      // 
      // textureStageControl2
      // 
      this.textureStageControl2.Location = new System.Drawing.Point(400, 160);
      this.textureStageControl2.Name = "textureStageControl2";
      this.textureStageControl2.Size = new System.Drawing.Size(152, 216);
      this.textureStageControl2.TabIndex = 14;
      this.textureStageControl2.StateChanged += new Prometheus.Controls.StateChangeHandler(this.textureStageControl2_StateChanged);
      // 
      // label8
      // 
      this.label8.Font = new System.Drawing.Font("Tahoma", 10F);
      this.label8.ForeColor = System.Drawing.Color.Blue;
      this.label8.Location = new System.Drawing.Point(288, 136);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(72, 23);
      this.label8.TabIndex = 13;
      this.label8.Text = "Stage 1";
      // 
      // label7
      // 
      this.label7.Location = new System.Drawing.Point(16, 304);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(80, 23);
      this.label7.TabIndex = 12;
      this.label7.Text = "Alpha Arg2:";
      this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label6
      // 
      this.label6.Location = new System.Drawing.Point(16, 272);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(80, 23);
      this.label6.TabIndex = 11;
      this.label6.Text = "Alpha Arg1:";
      this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label5
      // 
      this.label5.Location = new System.Drawing.Point(24, 232);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(72, 23);
      this.label5.TabIndex = 10;
      this.label5.Text = "Color Op:";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(24, 200);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(72, 23);
      this.label4.TabIndex = 9;
      this.label4.Text = "Color Arg2:";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // textureStageControl1
      // 
      this.textureStageControl1.Location = new System.Drawing.Point(248, 160);
      this.textureStageControl1.Name = "textureStageControl1";
      this.textureStageControl1.Size = new System.Drawing.Size(152, 216);
      this.textureStageControl1.TabIndex = 8;
      this.textureStageControl1.StateChanged += new Prometheus.Controls.StateChangeHandler(this.textureStageControl1_StateChanged);
      // 
      // textureStageControl0
      // 
      this.textureStageControl0.Location = new System.Drawing.Point(96, 160);
      this.textureStageControl0.Name = "textureStageControl0";
      this.textureStageControl0.Size = new System.Drawing.Size(152, 216);
      this.textureStageControl0.TabIndex = 7;
      this.textureStageControl0.StateChanged += new Prometheus.Controls.StateChangeHandler(this.textureStageControl0_StateChanged);
      // 
      // label3
      // 
      this.label3.Font = new System.Drawing.Font("Tahoma", 10F);
      this.label3.ForeColor = System.Drawing.Color.Blue;
      this.label3.Location = new System.Drawing.Point(136, 136);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(72, 23);
      this.label3.TabIndex = 6;
      this.label3.Text = "Stage 0";
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(32, 336);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(64, 23);
      this.label2.TabIndex = 5;
      this.label2.Text = "Alpha Op:";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // checkBoxEnableAlphaBlend
      // 
      this.checkBoxEnableAlphaBlend.Location = new System.Drawing.Point(104, 72);
      this.checkBoxEnableAlphaBlend.Name = "checkBoxEnableAlphaBlend";
      this.checkBoxEnableAlphaBlend.Size = new System.Drawing.Size(160, 24);
      this.checkBoxEnableAlphaBlend.TabIndex = 4;
      this.checkBoxEnableAlphaBlend.Text = "Enable Alpha Blend";
      this.checkBoxEnableAlphaBlend.CheckedChanged += new System.EventHandler(this.checkBoxEnableAlphaBlend_CheckedChanged);
      // 
      // GrenDebugger
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
      this.ClientSize = new System.Drawing.Size(775, 424);
      this.Controls.Add(this.checkBoxEnableShaderDebug);
      this.Controls.Add(this.groupBox1);
      this.Name = "GrenDebugger";
      this.Text = "GrenDebugger";
      this.groupBox1.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion

    private void checkBoxEnableShaderDebug_CheckedChanged(object sender, System.EventArgs e)
    {
      ShaderBase.enableShaderOverride = checkBoxEnableShaderDebug.Checked;
    }

    private void textureStageControl0_StateChanged()
    {
      ShaderBase.Stage[0].ColorArg1 = textureStageControl0.ColorArg1;
      ShaderBase.Stage[0].ColorArg2 = textureStageControl0.ColorArg2;
      ShaderBase.Stage[0].ColorOp = textureStageControl0.ColorOp;

      ShaderBase.Stage[0].AlphaArg1 = textureStageControl0.AlphaArg1;
      ShaderBase.Stage[0].AlphaArg2 = textureStageControl0.AlphaArg2;
      ShaderBase.Stage[0].AlphaOp = textureStageControl0.AlphaOp;
    }

    private void checkBoxEnableAlphaBlend_CheckedChanged(object sender, System.EventArgs e)
    {
      ShaderBase.enableAlphaBlend = checkBoxEnableAlphaBlend.Checked;
    }

    private void checkBoxEnableAlphaTest_CheckedChanged(object sender, System.EventArgs e)
    {
      ShaderBase.enableAlphaTest = checkBoxEnableAlphaTest.Checked;
    }

    private void textureStageControl1_StateChanged()
    {
      ShaderBase.Stage[1].ColorArg1 = textureStageControl1.ColorArg1;
      ShaderBase.Stage[1].ColorArg2 = textureStageControl1.ColorArg2;
      ShaderBase.Stage[1].ColorOp = textureStageControl1.ColorOp;

      ShaderBase.Stage[1].AlphaArg1 = textureStageControl1.AlphaArg1;
      ShaderBase.Stage[1].AlphaArg2 = textureStageControl1.AlphaArg2;
      ShaderBase.Stage[1].AlphaOp = textureStageControl1.AlphaOp;
    }

    private void textureStageControl2_StateChanged()
    {
      ShaderBase.Stage[2].ColorArg1 = textureStageControl2.ColorArg1;
      ShaderBase.Stage[2].ColorArg2 = textureStageControl2.ColorArg2;
      ShaderBase.Stage[2].ColorOp = textureStageControl2.ColorOp;

      ShaderBase.Stage[2].AlphaArg1 = textureStageControl2.AlphaArg1;
      ShaderBase.Stage[2].AlphaArg2 = textureStageControl2.AlphaArg2;
      ShaderBase.Stage[2].AlphaOp = textureStageControl2.AlphaOp;    
    }

    private void textureStageControl3_StateChanged()
    {
      ShaderBase.Stage[3].ColorArg1 = textureStageControl3.ColorArg1;
      ShaderBase.Stage[3].ColorArg2 = textureStageControl3.ColorArg2;
      ShaderBase.Stage[3].ColorOp = textureStageControl3.ColorOp;

      ShaderBase.Stage[3].AlphaArg1 = textureStageControl3.AlphaArg1;
      ShaderBase.Stage[3].AlphaArg2 = textureStageControl3.AlphaArg2;
      ShaderBase.Stage[3].AlphaOp = textureStageControl3.AlphaOp;
    }

    private void comboBoxSourceBlend_SelectedIndexChanged(object sender, System.EventArgs e)
    {
      switch(comboBoxSourceBlend.SelectedIndex)
      {
        case 0:
          ShaderBase.SourceBlend = Blend.Zero;
          break;
        case 1:
          ShaderBase.SourceBlend = Blend.InvBlendFactor;
          break;
        case 2:
          ShaderBase.SourceBlend = Blend.BlendFactor;
          break;
        case 3:
          ShaderBase.SourceBlend = Blend.BothInvSourceAlpha;
          break;
        case 4:
          ShaderBase.SourceBlend = Blend.SourceAlphaSat;
          break;
        case 5:
          ShaderBase.SourceBlend = Blend.InvDestinationColor;
          break;
        case 6:
          ShaderBase.SourceBlend = Blend.DestinationColor;
          break;
        case 7:
          ShaderBase.SourceBlend = Blend.InvDestinationAlpha;
          break;
        case 8:
          ShaderBase.SourceBlend = Blend.DestinationAlpha;
          break;
        case 9:
          ShaderBase.SourceBlend = Blend.InvSourceAlpha;
          break;
        case 10:
          ShaderBase.SourceBlend = Blend.SourceAlpha;
          break;
        case 11:
          ShaderBase.SourceBlend = Blend.InvSourceColor;
          break;
        case 12:
          ShaderBase.SourceBlend = Blend.SourceColor;
          break;
        case 13:
          ShaderBase.SourceBlend = Blend.One;
          break;
      }
    }
    private void comboBoxDestinationBlend_SelectedIndexChanged(object sender, System.EventArgs e)
    {
      switch(comboBoxDestinationBlend.SelectedIndex)
      {
        case 0:
          ShaderBase.DestinationBlend = Blend.Zero;
          break;
        case 1:
          ShaderBase.DestinationBlend = Blend.InvBlendFactor;
          break;
        case 2:
          ShaderBase.DestinationBlend = Blend.BlendFactor;
          break;
        case 3:
          ShaderBase.DestinationBlend = Blend.BothInvSourceAlpha;
          break;
        case 4:
          ShaderBase.DestinationBlend = Blend.SourceAlphaSat;
          break;
        case 5:
          ShaderBase.DestinationBlend = Blend.InvDestinationColor;
          break;
        case 6:
          ShaderBase.DestinationBlend = Blend.DestinationColor;
          break;
        case 7:
          ShaderBase.DestinationBlend = Blend.InvDestinationAlpha;
          break;
        case 8:
          ShaderBase.DestinationBlend = Blend.DestinationAlpha;
          break;
        case 9:
          ShaderBase.DestinationBlend = Blend.InvSourceAlpha;
          break;
        case 10:
          ShaderBase.DestinationBlend = Blend.SourceAlpha;
          break;
        case 11:
          ShaderBase.DestinationBlend = Blend.InvSourceColor;
          break;
        case 12:
          ShaderBase.DestinationBlend = Blend.SourceColor;
          break;
        case 13:
          ShaderBase.DestinationBlend = Blend.One;
          break;
      }
    }
  }
}

