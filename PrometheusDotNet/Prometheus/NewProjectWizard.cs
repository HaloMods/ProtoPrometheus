using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Prometheus.Core.Project;

namespace Prometheus
{
	/// <summary>
	/// Summary description for NewProjectWizard.
	/// </summary>
	public class NewProjectWizard : DevExpress.XtraEditors.XtraForm
	{
    private CristiPotlog.Controls.Wizard wizard1;
    private CristiPotlog.Controls.WizardPage wizardNewProject1;
    private CristiPotlog.Controls.WizardPage wizardNewProject2;
    private DevExpress.XtraEditors.TextEdit textEditAuthorName;
    private System.Windows.Forms.Label label1;
    private DevExpress.XtraEditors.ComboBoxEdit comboBoxTargetPlatform;
    private System.Windows.Forms.Label label2;
    private DevExpress.XtraEditors.MemoEdit memoEditProjectDescription;
    private System.Windows.Forms.Label label3;
    private DevExpress.XtraEditors.ComboBoxEdit comboBoxXboxMapName;
    private System.Windows.Forms.Label label4;
    private DevExpress.XtraEditors.TextEdit textEditMapfileName;
    private CristiPotlog.Controls.WizardPage wizardNewProject3;
    private DevExpress.XtraEditors.TextEdit textEdit1;
    
    class TagTemplateControlSet
    {
      private System.Drawing.Point BUTTON_START = new Point(488, 80); 
      private System.Drawing.Point FILENAME_START = new Point(32, 80); 
      public DevExpress.XtraEditors.SimpleButton browseButton;
      public DevExpress.XtraEditors.TextEdit tagFilename;
      
      public TagTemplateControlSet()
      {
        browseButton = new SimpleButton();
        tagFilename = new TextEdit();
      }
      public void InitLayout()
      {
        ((System.ComponentModel.ISupportInitialize)(this.tagFilename.Properties)).BeginInit();
      }
      public void EndLayout()
      {
        ((System.ComponentModel.ISupportInitialize)(this.tagFilename.Properties)).EndInit();
      }
      public void PerformControlInit(int index, string default_tag, string tag_name)
      {
        browseButton.Location = new System.Drawing.Point(BUTTON_START.X, BUTTON_START.Y + index*30);
        browseButton.Name = "templateButton" + index.ToString();
        browseButton.Size = new System.Drawing.Size(200, 23);
        browseButton.Text = tag_name + "...";

        tagFilename.EditValue = default_tag;
        tagFilename.Location = new System.Drawing.Point(FILENAME_START.X, FILENAME_START.Y + index*30);
        tagFilename.Name = "templateEdit" + index.ToString();
        tagFilename.Size = new System.Drawing.Size(432, 23);
      }
    }

    TagTemplateControlSet[] m_RootTagControls;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public NewProjectWizard()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

      CreateHaloPcTagList();
		}
    public void CreateHaloPcTagList()
    {
      //allocate template-based controls
      ProjectTemplate proj = ProjectTemplates.GetTemplate("HaloPCMultiplayer");
      m_RootTagControls = new TagTemplateControlSet[proj.TagSet.Length];

      for(int i=0; i<proj.TagSet.Length; i++)
        m_RootTagControls[i] = new TagTemplateControlSet();

      //suspend layout of appropriate parent controls
      this.SuspendLayout();
      this.wizardNewProject2.SuspendLayout();
      
      //do the "BeginInit()" calls for the template-based edit controls
      for(int i=0; i<m_RootTagControls.Length; i++)
        m_RootTagControls[i].InitLayout();

      for(int i=0; i<m_RootTagControls.Length; i++)
      {
        this.wizardNewProject3.Controls.Add(m_RootTagControls[i].tagFilename);
        this.wizardNewProject3.Controls.Add(m_RootTagControls[i].browseButton);
      }

      for(int i=0; i<proj.TagSet.Length; i++)
        m_RootTagControls[i].PerformControlInit(i, proj.TagSet[i].DefaultFile, proj.TagSet[i].Name);

      for(int i=0; i<m_RootTagControls.Length; i++)
        m_RootTagControls[i].EndLayout();

      this.wizardNewProject2.ResumeLayout(false);
      this.ResumeLayout(false);
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
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(NewProjectWizard));
      

      this.wizard1 = new CristiPotlog.Controls.Wizard();
      this.wizardNewProject3 = new CristiPotlog.Controls.WizardPage();
      this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
      this.wizardNewProject2 = new CristiPotlog.Controls.WizardPage();
      this.textEditMapfileName = new DevExpress.XtraEditors.TextEdit();
      this.label4 = new System.Windows.Forms.Label();
      this.comboBoxXboxMapName = new DevExpress.XtraEditors.ComboBoxEdit();
      this.label3 = new System.Windows.Forms.Label();
      this.memoEditProjectDescription = new DevExpress.XtraEditors.MemoEdit();
      this.label2 = new System.Windows.Forms.Label();
      this.comboBoxTargetPlatform = new DevExpress.XtraEditors.ComboBoxEdit();
      this.label1 = new System.Windows.Forms.Label();
      this.textEditAuthorName = new DevExpress.XtraEditors.TextEdit();
      this.wizardNewProject1 = new CristiPotlog.Controls.WizardPage();
      this.wizard1.SuspendLayout();
      this.wizardNewProject3.SuspendLayout();
      this.wizardNewProject2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.textEditMapfileName.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboBoxXboxMapName.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.memoEditProjectDescription.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboBoxTargetPlatform.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.textEditAuthorName.Properties)).BeginInit();
      this.SuspendLayout();
      // 
      // wizard1
      // 
      this.wizard1.Controls.Add(this.wizardNewProject3);
      this.wizard1.Controls.Add(this.wizardNewProject2);
      this.wizard1.Controls.Add(this.wizardNewProject1);
      this.wizard1.HeaderImage = ((System.Drawing.Image)(resources.GetObject("wizard1.HeaderImage")));
      this.wizard1.Location = new System.Drawing.Point(0, 0);
      this.wizard1.Name = "wizard1";
      this.wizard1.Pages.AddRange(new CristiPotlog.Controls.WizardPage[] {
                                                                           this.wizardNewProject1,
                                                                           this.wizardNewProject2,
                                                                           this.wizardNewProject3});
      this.wizard1.Size = new System.Drawing.Size(712, 456);
      this.wizard1.TabIndex = 0;
      this.wizard1.WelcomeImage = ((System.Drawing.Image)(resources.GetObject("wizard1.WelcomeImage")));
      // 
      // wizardNewProject3
      // 

      this.wizardNewProject3.Description = "Set project global tag settings that are necessary to compile the map.";
      this.wizardNewProject3.Location = new System.Drawing.Point(0, 0);
      this.wizardNewProject3.Name = "wizardNewProject3";
      this.wizardNewProject3.Size = new System.Drawing.Size(712, 408);
      this.wizardNewProject3.TabIndex = 12;
      this.wizardNewProject3.Title = "Global Tags";
      // 
      // wizardNewProject2
      // 
      this.wizardNewProject2.Controls.Add(this.textEditMapfileName);
      this.wizardNewProject2.Controls.Add(this.label4);
      this.wizardNewProject2.Controls.Add(this.comboBoxXboxMapName);
      this.wizardNewProject2.Controls.Add(this.label3);
      this.wizardNewProject2.Controls.Add(this.memoEditProjectDescription);
      this.wizardNewProject2.Controls.Add(this.label2);
      this.wizardNewProject2.Controls.Add(this.comboBoxTargetPlatform);
      this.wizardNewProject2.Controls.Add(this.label1);
      this.wizardNewProject2.Controls.Add(this.textEditAuthorName);
      this.wizardNewProject2.Description = "Configure mapfile target and author information.";
      this.wizardNewProject2.Location = new System.Drawing.Point(0, 0);
      this.wizardNewProject2.Name = "wizardNewProject2";
      this.wizardNewProject2.Size = new System.Drawing.Size(712, 408);
      this.wizardNewProject2.TabIndex = 11;
      this.wizardNewProject2.Title = "Map Settings";
      this.wizardNewProject2.Click += new System.EventHandler(this.wizardNewProject2_Click);
      // 
      // textEditMapfileName
      // 
      this.textEditMapfileName.EditValue = "";
      this.textEditMapfileName.Location = new System.Drawing.Point(136, 168);
      this.textEditMapfileName.Name = "textEditMapfileName";
      this.textEditMapfileName.Size = new System.Drawing.Size(128, 22);
      this.textEditMapfileName.TabIndex = 9;
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(24, 168);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(96, 23);
      this.label4.TabIndex = 8;
      this.label4.Text = "Mapfile Name:";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // comboBoxXboxMapName
      // 
      this.comboBoxXboxMapName.EditValue = "bloodgulch.map";
      this.comboBoxXboxMapName.Location = new System.Drawing.Point(280, 168);
      this.comboBoxXboxMapName.Name = "comboBoxXboxMapName";
      // 
      // comboBoxXboxMapName.Properties
      // 
      this.comboBoxXboxMapName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.comboBoxXboxMapName.Properties.Enabled = false;
      this.comboBoxXboxMapName.Size = new System.Drawing.Size(144, 22);
      this.comboBoxXboxMapName.TabIndex = 7;
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(32, 216);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(88, 23);
      this.label3.TabIndex = 6;
      this.label3.Text = "Description:";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // memoEditProjectDescription
      // 
      this.memoEditProjectDescription.EditValue = "";
      this.memoEditProjectDescription.Location = new System.Drawing.Point(136, 208);
      this.memoEditProjectDescription.Name = "memoEditProjectDescription";
      this.memoEditProjectDescription.Size = new System.Drawing.Size(288, 80);
      this.memoEditProjectDescription.TabIndex = 5;
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(16, 128);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(110, 23);
      this.label2.TabIndex = 3;
      this.label2.Text = "Target Platform:";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // comboBoxTargetPlatform
      // 
      this.comboBoxTargetPlatform.EditValue = "Halo 2 Xbox";
      this.comboBoxTargetPlatform.Location = new System.Drawing.Point(136, 128);
      this.comboBoxTargetPlatform.Name = "comboBoxTargetPlatform";
      // 
      // comboBoxTargetPlatform.Properties
      // 
      this.comboBoxTargetPlatform.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                                   new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.comboBoxTargetPlatform.Size = new System.Drawing.Size(128, 22);
      this.comboBoxTargetPlatform.TabIndex = 2;
      this.comboBoxTargetPlatform.SelectedIndexChanged += new System.EventHandler(this.comboBoxTargetPlatform_SelectedIndexChanged);
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(32, 88);
      this.label1.Name = "label1";
      this.label1.TabIndex = 1;
      this.label1.Text = "Author Name:";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // textEditAuthorName
      // 
      this.textEditAuthorName.EditValue = "";
      this.textEditAuthorName.Location = new System.Drawing.Point(136, 88);
      this.textEditAuthorName.Name = "textEditAuthorName";
      this.textEditAuthorName.Size = new System.Drawing.Size(128, 22);
      this.textEditAuthorName.TabIndex = 0;
      // 
      // wizardNewProject1
      // 
      this.wizardNewProject1.Description = "This wizard will guide you through the steps of creating a new map project.";
      this.wizardNewProject1.Location = new System.Drawing.Point(0, 0);
      this.wizardNewProject1.Name = "wizardNewProject1";
      this.wizardNewProject1.Size = new System.Drawing.Size(712, 408);
      this.wizardNewProject1.Style = CristiPotlog.Controls.WizardPageStyle.Welcome;
      this.wizardNewProject1.TabIndex = 10;
      this.wizardNewProject1.Title = "New Prometheus Project";
      // 
      // NewProjectWizard
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
      this.ClientSize = new System.Drawing.Size(712, 456);
      this.Controls.Add(this.wizard1);
      this.Name = "NewProjectWizard";
      this.Text = "Project Wizard";
      this.wizard1.ResumeLayout(false);
      this.wizardNewProject3.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
      this.wizardNewProject2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.textEditMapfileName.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboBoxXboxMapName.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.memoEditProjectDescription.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboBoxTargetPlatform.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.textEditAuthorName.Properties)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

    private void comboBoxTargetPlatform_SelectedIndexChanged(object sender, System.EventArgs e)
    {
    
    }

    private void wizardNewProject2_Click(object sender, System.EventArgs e)
    {
    
    }
	}
}

