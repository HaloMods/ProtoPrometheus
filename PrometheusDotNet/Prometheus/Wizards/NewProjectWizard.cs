using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using CristiPotlog.Controls;
using DevExpress.XtraEditors;
using Prometheus.Core;
using Prometheus.Core.Project;
using Prometheus.Core.Tags;
using SharedControls;

namespace Prometheus.Wizards
{
  public class NewProjectWizard : XtraForm
  {
    #region Fields
    #region Controls
    private Container components = null;
    private Wizard wizard1;
    private WizardPage pageWelcome;
    private WizardPage pageTargetPlatform;
    private WizardPage pageProjectInformation;
    private WizardPage pageMapFilenameSelect;
    private WizardPage pageFinish;
    private WizardPage pageChooseScenario;
    private CristiPotlog.Controls.WizardPage pageGenerateScenario;
    private DevExpress.XtraEditors.GroupControl pageProjectInformation_groupControlAuthorInformation;
    private System.Windows.Forms.Label pageProjectInformation_labelAuthorName;
    private DevExpress.XtraEditors.TextEdit pageProjectInformation_textEditAuthorName;
    private DevExpress.XtraEditors.GroupControl pageProjectInformation_groupComtrolMapInformation;
    private DevExpress.XtraEditors.MemoEdit pageProjectInformation_memoDescription;
    private DevExpress.XtraEditors.TextEdit pageProjectInformation_textMapTitle;
    private System.Windows.Forms.Label pageProjectInformation_labelMapTitle;
    private System.Windows.Forms.Label pageProjectInformation_labelDescription;
    private DevExpress.XtraEditors.GroupControl pageTargetPlatform_groupControlPlatforms;
    private DevExpress.XtraEditors.RadioGroup pageTargetPlatform_radioGroupPlatforms;
    private DevExpress.XtraEditors.GroupControl pageMapFilenameSelect_groupControlFileInformation;
    private System.Windows.Forms.Label pageMapFilenameSelect_labelFilename;
    private DevExpress.XtraEditors.ComboBoxEdit pageMapFilenameSelect_comboBoxFilename;
    private System.Windows.Forms.Label pageChooseScenario_labelHelpText;
    private DevExpress.XtraEditors.GroupControl pageGenerateScenario_groupControlSBSPTag;
    private DevExpress.XtraEditors.ButtonEdit pageGenerateScenario_buttonEditSBSPTag;
    private System.Windows.Forms.Label pageGenerateScenario_labelHelpText;
    private System.Windows.Forms.Label pageMapFilenameSelect_labelHelpText;
    private DevExpress.XtraEditors.GroupControl pageChooseScenario_groupControlScenarioTag;
    private DevExpress.XtraEditors.ButtonEdit pageChooseScenario_buttonEditScenarioTagName;
    private DevExpress.XtraEditors.RadioGroup pageChooseScenario_radioGroupScenarioAction;
    #endregion
    private ProjectFile project;
    private string projectFilename = "";
    MapfileVersion mapVersion;
    #endregion

    #region Properties
    public string ProjectFilename
    {
      get { return this.projectFilename; }
    }
    #endregion

    #region Constructor/Destructor
    public NewProjectWizard()
    {
      InitializeComponent();
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
    #endregion

    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(NewProjectWizard));
      this.wizard1 = new CristiPotlog.Controls.Wizard();
      this.pageTargetPlatform = new CristiPotlog.Controls.WizardPage();
      this.pageTargetPlatform_groupControlPlatforms = new DevExpress.XtraEditors.GroupControl();
      this.pageTargetPlatform_radioGroupPlatforms = new DevExpress.XtraEditors.RadioGroup();
      this.pageWelcome = new CristiPotlog.Controls.WizardPage();
      this.pageProjectInformation = new CristiPotlog.Controls.WizardPage();
      this.pageProjectInformation_groupControlAuthorInformation = new DevExpress.XtraEditors.GroupControl();
      this.pageProjectInformation_labelAuthorName = new System.Windows.Forms.Label();
      this.pageProjectInformation_textEditAuthorName = new DevExpress.XtraEditors.TextEdit();
      this.pageProjectInformation_groupComtrolMapInformation = new DevExpress.XtraEditors.GroupControl();
      this.pageProjectInformation_memoDescription = new DevExpress.XtraEditors.MemoEdit();
      this.pageProjectInformation_textMapTitle = new DevExpress.XtraEditors.TextEdit();
      this.pageProjectInformation_labelMapTitle = new System.Windows.Forms.Label();
      this.pageProjectInformation_labelDescription = new System.Windows.Forms.Label();
      this.pageMapFilenameSelect = new CristiPotlog.Controls.WizardPage();
      this.pageMapFilenameSelect_groupControlFileInformation = new DevExpress.XtraEditors.GroupControl();
      this.pageMapFilenameSelect_labelFilename = new System.Windows.Forms.Label();
      this.pageMapFilenameSelect_comboBoxFilename = new DevExpress.XtraEditors.ComboBoxEdit();
      this.pageMapFilenameSelect_labelHelpText = new System.Windows.Forms.Label();
      this.pageChooseScenario = new CristiPotlog.Controls.WizardPage();
      this.pageChooseScenario_labelHelpText = new System.Windows.Forms.Label();
      this.pageChooseScenario_groupControlScenarioTag = new DevExpress.XtraEditors.GroupControl();
      this.pageChooseScenario_buttonEditScenarioTagName = new DevExpress.XtraEditors.ButtonEdit();
      this.pageChooseScenario_radioGroupScenarioAction = new DevExpress.XtraEditors.RadioGroup();
      this.pageGenerateScenario = new CristiPotlog.Controls.WizardPage();
      this.pageGenerateScenario_groupControlSBSPTag = new DevExpress.XtraEditors.GroupControl();
      this.pageGenerateScenario_buttonEditSBSPTag = new DevExpress.XtraEditors.ButtonEdit();
      this.pageGenerateScenario_labelHelpText = new System.Windows.Forms.Label();
      this.pageFinish = new CristiPotlog.Controls.WizardPage();
      this.wizard1.SuspendLayout();
      this.pageTargetPlatform.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pageTargetPlatform_groupControlPlatforms)).BeginInit();
      this.pageTargetPlatform_groupControlPlatforms.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pageTargetPlatform_radioGroupPlatforms.Properties)).BeginInit();
      this.pageProjectInformation.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pageProjectInformation_groupControlAuthorInformation)).BeginInit();
      this.pageProjectInformation_groupControlAuthorInformation.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pageProjectInformation_textEditAuthorName.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pageProjectInformation_groupComtrolMapInformation)).BeginInit();
      this.pageProjectInformation_groupComtrolMapInformation.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pageProjectInformation_memoDescription.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pageProjectInformation_textMapTitle.Properties)).BeginInit();
      this.pageMapFilenameSelect.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pageMapFilenameSelect_groupControlFileInformation)).BeginInit();
      this.pageMapFilenameSelect_groupControlFileInformation.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pageMapFilenameSelect_comboBoxFilename.Properties)).BeginInit();
      this.pageChooseScenario.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pageChooseScenario_groupControlScenarioTag)).BeginInit();
      this.pageChooseScenario_groupControlScenarioTag.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pageChooseScenario_buttonEditScenarioTagName.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pageChooseScenario_radioGroupScenarioAction.Properties)).BeginInit();
      this.pageGenerateScenario.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pageGenerateScenario_groupControlSBSPTag)).BeginInit();
      this.pageGenerateScenario_groupControlSBSPTag.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pageGenerateScenario_buttonEditSBSPTag.Properties)).BeginInit();
      this.SuspendLayout();
      // 
      // wizard1
      // 
      this.wizard1.Controls.Add(this.pageWelcome);
      this.wizard1.Controls.Add(this.pageFinish);
      this.wizard1.Controls.Add(this.pageGenerateScenario);
      this.wizard1.Controls.Add(this.pageChooseScenario);
      this.wizard1.Controls.Add(this.pageMapFilenameSelect);
      this.wizard1.Controls.Add(this.pageProjectInformation);
      this.wizard1.Controls.Add(this.pageTargetPlatform);
      this.wizard1.HeaderImage = ((System.Drawing.Image)(resources.GetObject("wizard1.HeaderImage")));
      this.wizard1.HelpVisible = true;
      this.wizard1.Location = new System.Drawing.Point(0, 0);
      this.wizard1.Name = "wizard1";
      this.wizard1.Pages.AddRange(new CristiPotlog.Controls.WizardPage[] {
                                                                           this.pageWelcome,
                                                                           this.pageTargetPlatform,
                                                                           this.pageProjectInformation,
                                                                           this.pageMapFilenameSelect,
                                                                           this.pageChooseScenario,
                                                                           this.pageGenerateScenario,
                                                                           this.pageFinish});
      this.wizard1.Size = new System.Drawing.Size(482, 368);
      this.wizard1.TabIndex = 0;
      this.wizard1.WelcomeImage = ((System.Drawing.Image)(resources.GetObject("wizard1.WelcomeImage")));
      // 
      // pageTargetPlatform
      // 
      this.pageTargetPlatform.Controls.Add(this.pageTargetPlatform_groupControlPlatforms);
      this.pageTargetPlatform.Description = "Please select the platform that your map will be designed for from the list below" +
        ".";
      this.pageTargetPlatform.Location = new System.Drawing.Point(0, 0);
      this.pageTargetPlatform.Name = "pageTargetPlatform";
      this.pageTargetPlatform.Owner = this.wizard1;
      this.pageTargetPlatform.PreviousPage = this.pageWelcome;
      this.pageTargetPlatform.Size = new System.Drawing.Size(428, 208);
      this.pageTargetPlatform.TabIndex = 15;
      this.pageTargetPlatform.Title = "Target Platform";
      this.pageTargetPlatform.BeforeNext += new CristiPotlog.Controls.Wizard.ChangePageEventHandler(this.pageTargetPlatform_BeforeNext);
      // 
      // pageTargetPlatform_groupControlPlatforms
      // 
      this.pageTargetPlatform_groupControlPlatforms.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.pageTargetPlatform_groupControlPlatforms.Controls.Add(this.pageTargetPlatform_radioGroupPlatforms);
      this.pageTargetPlatform_groupControlPlatforms.Location = new System.Drawing.Point(8, 72);
      this.pageTargetPlatform_groupControlPlatforms.Name = "pageTargetPlatform_groupControlPlatforms";
      this.pageTargetPlatform_groupControlPlatforms.Size = new System.Drawing.Size(412, 96);
      this.pageTargetPlatform_groupControlPlatforms.TabIndex = 3;
      this.pageTargetPlatform_groupControlPlatforms.Text = "Platforms";
      // 
      // pageTargetPlatform_radioGroupPlatforms
      // 
      this.pageTargetPlatform_radioGroupPlatforms.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pageTargetPlatform_radioGroupPlatforms.Location = new System.Drawing.Point(2, 20);
      this.pageTargetPlatform_radioGroupPlatforms.Name = "pageTargetPlatform_radioGroupPlatforms";
      // 
      // pageTargetPlatform_radioGroupPlatforms.Properties
      // 
      this.pageTargetPlatform_radioGroupPlatforms.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
      this.pageTargetPlatform_radioGroupPlatforms.Properties.Appearance.Options.UseBackColor = true;
      this.pageTargetPlatform_radioGroupPlatforms.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
      this.pageTargetPlatform_radioGroupPlatforms.Properties.Columns = 1;
      this.pageTargetPlatform_radioGroupPlatforms.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
                                                                                                                                   new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Halo for the PC"),
                                                                                                                                   new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Halo Custom Edition for the PC"),
                                                                                                                                   new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "Halo for the Xbox"),
                                                                                                                                   new DevExpress.XtraEditors.Controls.RadioGroupItem(3, "Halo 2 for the Xbox")});
      this.pageTargetPlatform_radioGroupPlatforms.Size = new System.Drawing.Size(408, 74);
      this.pageTargetPlatform_radioGroupPlatforms.TabIndex = 2;
      // 
      // pageWelcome
      // 
      this.pageWelcome.Description = "This wizard will giude you through the process of creating a new Prometheus map p" +
        "roject.";
      this.pageWelcome.Location = new System.Drawing.Point(0, 0);
      this.pageWelcome.Name = "pageWelcome";
      this.pageWelcome.Owner = this.wizard1;
      this.pageWelcome.PreviousPage = null;
      this.pageWelcome.Size = new System.Drawing.Size(482, 320);
      this.pageWelcome.Style = CristiPotlog.Controls.WizardPageStyle.Welcome;
      this.pageWelcome.TabIndex = 14;
      this.pageWelcome.Title = "Create New Project";
      // 
      // pageProjectInformation
      // 
      this.pageProjectInformation.Controls.Add(this.pageProjectInformation_groupControlAuthorInformation);
      this.pageProjectInformation.Controls.Add(this.pageProjectInformation_groupComtrolMapInformation);
      this.pageProjectInformation.Description = "Provide the following information about your project.";
      this.pageProjectInformation.Location = new System.Drawing.Point(0, 0);
      this.pageProjectInformation.Name = "pageProjectInformation";
      this.pageProjectInformation.Owner = this.wizard1;
      this.pageProjectInformation.PreviousPage = this.pageTargetPlatform;
      this.pageProjectInformation.Size = new System.Drawing.Size(428, 208);
      this.pageProjectInformation.TabIndex = 16;
      this.pageProjectInformation.Title = "Project Settings";
      this.pageProjectInformation.BeforeNext += new CristiPotlog.Controls.Wizard.ChangePageEventHandler(this.pageProjectInformation_BeforeNext);
      // 
      // pageProjectInformation_groupControlAuthorInformation
      // 
      this.pageProjectInformation_groupControlAuthorInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.pageProjectInformation_groupControlAuthorInformation.Controls.Add(this.pageProjectInformation_labelAuthorName);
      this.pageProjectInformation_groupControlAuthorInformation.Controls.Add(this.pageProjectInformation_textEditAuthorName);
      this.pageProjectInformation_groupControlAuthorInformation.Location = new System.Drawing.Point(8, 216);
      this.pageProjectInformation_groupControlAuthorInformation.Name = "pageProjectInformation_groupControlAuthorInformation";
      this.pageProjectInformation_groupControlAuthorInformation.Size = new System.Drawing.Size(412, 72);
      this.pageProjectInformation_groupControlAuthorInformation.TabIndex = 22;
      this.pageProjectInformation_groupControlAuthorInformation.Text = "Author Information";
      // 
      // pageProjectInformation_labelAuthorName
      // 
      this.pageProjectInformation_labelAuthorName.Location = new System.Drawing.Point(8, 34);
      this.pageProjectInformation_labelAuthorName.Name = "pageProjectInformation_labelAuthorName";
      this.pageProjectInformation_labelAuthorName.Size = new System.Drawing.Size(80, 20);
      this.pageProjectInformation_labelAuthorName.TabIndex = 11;
      this.pageProjectInformation_labelAuthorName.Text = "Author Name:";
      this.pageProjectInformation_labelAuthorName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pageProjectInformation_textEditAuthorName
      // 
      this.pageProjectInformation_textEditAuthorName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.pageProjectInformation_textEditAuthorName.EditValue = "";
      this.pageProjectInformation_textEditAuthorName.Location = new System.Drawing.Point(92, 38);
      this.pageProjectInformation_textEditAuthorName.Name = "pageProjectInformation_textEditAuthorName";
      this.pageProjectInformation_textEditAuthorName.Size = new System.Drawing.Size(316, 20);
      this.pageProjectInformation_textEditAuthorName.TabIndex = 3;
      // 
      // pageProjectInformation_groupComtrolMapInformation
      // 
      this.pageProjectInformation_groupComtrolMapInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.pageProjectInformation_groupComtrolMapInformation.Controls.Add(this.pageProjectInformation_memoDescription);
      this.pageProjectInformation_groupComtrolMapInformation.Controls.Add(this.pageProjectInformation_textMapTitle);
      this.pageProjectInformation_groupComtrolMapInformation.Controls.Add(this.pageProjectInformation_labelMapTitle);
      this.pageProjectInformation_groupComtrolMapInformation.Controls.Add(this.pageProjectInformation_labelDescription);
      this.pageProjectInformation_groupComtrolMapInformation.Location = new System.Drawing.Point(8, 72);
      this.pageProjectInformation_groupComtrolMapInformation.Name = "pageProjectInformation_groupComtrolMapInformation";
      this.pageProjectInformation_groupComtrolMapInformation.Size = new System.Drawing.Size(412, 130);
      this.pageProjectInformation_groupComtrolMapInformation.TabIndex = 21;
      this.pageProjectInformation_groupComtrolMapInformation.Text = "Map Information";
      // 
      // pageProjectInformation_memoDescription
      // 
      this.pageProjectInformation_memoDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.pageProjectInformation_memoDescription.EditValue = "";
      this.pageProjectInformation_memoDescription.Location = new System.Drawing.Point(92, 64);
      this.pageProjectInformation_memoDescription.Name = "pageProjectInformation_memoDescription";
      this.pageProjectInformation_memoDescription.Size = new System.Drawing.Size(316, 56);
      this.pageProjectInformation_memoDescription.TabIndex = 2;
      // 
      // pageProjectInformation_textMapTitle
      // 
      this.pageProjectInformation_textMapTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.pageProjectInformation_textMapTitle.EditValue = "";
      this.pageProjectInformation_textMapTitle.Location = new System.Drawing.Point(92, 34);
      this.pageProjectInformation_textMapTitle.Name = "pageProjectInformation_textMapTitle";
      this.pageProjectInformation_textMapTitle.Size = new System.Drawing.Size(316, 20);
      this.pageProjectInformation_textMapTitle.TabIndex = 1;
      // 
      // pageProjectInformation_labelMapTitle
      // 
      this.pageProjectInformation_labelMapTitle.Location = new System.Drawing.Point(8, 30);
      this.pageProjectInformation_labelMapTitle.Name = "pageProjectInformation_labelMapTitle";
      this.pageProjectInformation_labelMapTitle.Size = new System.Drawing.Size(80, 20);
      this.pageProjectInformation_labelMapTitle.TabIndex = 19;
      this.pageProjectInformation_labelMapTitle.Text = "Map Title:";
      this.pageProjectInformation_labelMapTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pageProjectInformation_labelDescription
      // 
      this.pageProjectInformation_labelDescription.Location = new System.Drawing.Point(8, 64);
      this.pageProjectInformation_labelDescription.Name = "pageProjectInformation_labelDescription";
      this.pageProjectInformation_labelDescription.Size = new System.Drawing.Size(80, 20);
      this.pageProjectInformation_labelDescription.TabIndex = 15;
      this.pageProjectInformation_labelDescription.Text = "Description:";
      this.pageProjectInformation_labelDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pageMapFilenameSelect
      // 
      this.pageMapFilenameSelect.Controls.Add(this.pageMapFilenameSelect_groupControlFileInformation);
      this.pageMapFilenameSelect.Controls.Add(this.pageMapFilenameSelect_labelHelpText);
      this.pageMapFilenameSelect.Description = "Please select a filename for your map.";
      this.pageMapFilenameSelect.Location = new System.Drawing.Point(0, 0);
      this.pageMapFilenameSelect.Name = "pageMapFilenameSelect";
      this.pageMapFilenameSelect.Owner = this.wizard1;
      this.pageMapFilenameSelect.PreviousPage = this.pageProjectInformation;
      this.pageMapFilenameSelect.Size = new System.Drawing.Size(428, 208);
      this.pageMapFilenameSelect.TabIndex = 17;
      this.pageMapFilenameSelect.Title = "Filename Selection";
      this.pageMapFilenameSelect.BeforeShow += new System.EventHandler(this.pageMapFilenameSelect_BeforeShow);
      this.pageMapFilenameSelect.BeforeNext += new CristiPotlog.Controls.Wizard.ChangePageEventHandler(this.pageMapFilenameSelect_BeforeNext);
      // 
      // pageMapFilenameSelect_groupControlFileInformation
      // 
      this.pageMapFilenameSelect_groupControlFileInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.pageMapFilenameSelect_groupControlFileInformation.Controls.Add(this.pageMapFilenameSelect_labelFilename);
      this.pageMapFilenameSelect_groupControlFileInformation.Controls.Add(this.pageMapFilenameSelect_comboBoxFilename);
      this.pageMapFilenameSelect_groupControlFileInformation.Location = new System.Drawing.Point(8, 144);
      this.pageMapFilenameSelect_groupControlFileInformation.Name = "pageMapFilenameSelect_groupControlFileInformation";
      this.pageMapFilenameSelect_groupControlFileInformation.Size = new System.Drawing.Size(412, 72);
      this.pageMapFilenameSelect_groupControlFileInformation.TabIndex = 23;
      this.pageMapFilenameSelect_groupControlFileInformation.Text = "File Information";
      // 
      // pageMapFilenameSelect_labelFilename
      // 
      this.pageMapFilenameSelect_labelFilename.Location = new System.Drawing.Point(8, 32);
      this.pageMapFilenameSelect_labelFilename.Name = "pageMapFilenameSelect_labelFilename";
      this.pageMapFilenameSelect_labelFilename.Size = new System.Drawing.Size(80, 20);
      this.pageMapFilenameSelect_labelFilename.TabIndex = 19;
      this.pageMapFilenameSelect_labelFilename.Text = "Filename:";
      this.pageMapFilenameSelect_labelFilename.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pageMapFilenameSelect_comboBoxFilename
      // 
      this.pageMapFilenameSelect_comboBoxFilename.EditValue = "";
      this.pageMapFilenameSelect_comboBoxFilename.Location = new System.Drawing.Point(96, 32);
      this.pageMapFilenameSelect_comboBoxFilename.Name = "pageMapFilenameSelect_comboBoxFilename";
      // 
      // pageMapFilenameSelect_comboBoxFilename.Properties
      // 
      this.pageMapFilenameSelect_comboBoxFilename.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                                                   new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.pageMapFilenameSelect_comboBoxFilename.Properties.Items.AddRange(new object[] {
                                                                                           "beavercreek.map (Battle Creek)",
                                                                                           "bloodgulch.map (Blood Gulch)",
                                                                                           "carousel.map (Derelict)",
                                                                                           "sidewinder.map (Sidewinder)"});
      this.pageMapFilenameSelect_comboBoxFilename.Size = new System.Drawing.Size(360, 20);
      this.pageMapFilenameSelect_comboBoxFilename.TabIndex = 18;
      // 
      // pageMapFilenameSelect_labelHelpText
      // 
      this.pageMapFilenameSelect_labelHelpText.Location = new System.Drawing.Point(16, 80);
      this.pageMapFilenameSelect_labelHelpText.Name = "pageMapFilenameSelect_labelHelpText";
      this.pageMapFilenameSelect_labelHelpText.Size = new System.Drawing.Size(440, 48);
      this.pageMapFilenameSelect_labelHelpText.TabIndex = 20;
      this.pageMapFilenameSelect_labelHelpText.Text = "Maps created for the \'Halo for the PC\' and the \'Halo for the Xbox\' platforms must" +
        " overwrite an existing map that is included with the game.  Please choose the ma" +
        "p that you wish to replace from the choices below.";
      this.pageMapFilenameSelect_labelHelpText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pageChooseScenario
      // 
      this.pageChooseScenario.Controls.Add(this.pageChooseScenario_labelHelpText);
      this.pageChooseScenario.Controls.Add(this.pageChooseScenario_groupControlScenarioTag);
      this.pageChooseScenario.Description = "Specify the scenario tag that will be used in your map";
      this.pageChooseScenario.Location = new System.Drawing.Point(0, 0);
      this.pageChooseScenario.Name = "pageChooseScenario";
      this.pageChooseScenario.Owner = this.wizard1;
      this.pageChooseScenario.PreviousPage = this.pageMapFilenameSelect;
      this.pageChooseScenario.Size = new System.Drawing.Size(428, 208);
      this.pageChooseScenario.TabIndex = 19;
      this.pageChooseScenario.Title = "Scenario Tag";
      this.pageChooseScenario.BeforeShow += new System.EventHandler(this.pageChooseScenario_radioGroupScenarioAction_BeforeShow);
      this.pageChooseScenario.BeforeNext += new CristiPotlog.Controls.Wizard.ChangePageEventHandler(this.pageChooseScenario_BeforeNext);
      // 
      // pageChooseScenario_labelHelpText
      // 
      this.pageChooseScenario_labelHelpText.Location = new System.Drawing.Point(16, 80);
      this.pageChooseScenario_labelHelpText.Name = "pageChooseScenario_labelHelpText";
      this.pageChooseScenario_labelHelpText.Size = new System.Drawing.Size(456, 56);
      this.pageChooseScenario_labelHelpText.TabIndex = 25;
      this.pageChooseScenario_labelHelpText.Text = @"All maps must contain a valid 'Scenario' Tag.  This tag is used to store the locations of every item in the map, as well as numerous other values.  Please choose an option below specifying if you would like to have Prometheus create a new Scenario Tag for your project, or if you would like to use an existing Scenario Tag.";
      this.pageChooseScenario_labelHelpText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pageChooseScenario_groupControlScenarioTag
      // 
      this.pageChooseScenario_groupControlScenarioTag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.pageChooseScenario_groupControlScenarioTag.AutoScroll = true;
      this.pageChooseScenario_groupControlScenarioTag.Controls.Add(this.pageChooseScenario_buttonEditScenarioTagName);
      this.pageChooseScenario_groupControlScenarioTag.Controls.Add(this.pageChooseScenario_radioGroupScenarioAction);
      this.pageChooseScenario_groupControlScenarioTag.Location = new System.Drawing.Point(8, 152);
      this.pageChooseScenario_groupControlScenarioTag.Name = "pageChooseScenario_groupControlScenarioTag";
      this.pageChooseScenario_groupControlScenarioTag.Size = new System.Drawing.Size(412, 112);
      this.pageChooseScenario_groupControlScenarioTag.TabIndex = 24;
      this.pageChooseScenario_groupControlScenarioTag.Text = "Scenario Tag";
      // 
      // pageChooseScenario_buttonEditScenarioTagName
      // 
      this.pageChooseScenario_buttonEditScenarioTagName.EditValue = "";
      this.pageChooseScenario_buttonEditScenarioTagName.Location = new System.Drawing.Point(16, 80);
      this.pageChooseScenario_buttonEditScenarioTagName.Name = "pageChooseScenario_buttonEditScenarioTagName";
      // 
      // pageChooseScenario_buttonEditScenarioTagName.Properties
      // 
      this.pageChooseScenario_buttonEditScenarioTagName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                                                         new DevExpress.XtraEditors.Controls.EditorButton()});
      this.pageChooseScenario_buttonEditScenarioTagName.Properties.Enabled = false;
      this.pageChooseScenario_buttonEditScenarioTagName.Size = new System.Drawing.Size(448, 20);
      this.pageChooseScenario_buttonEditScenarioTagName.TabIndex = 1;
      this.pageChooseScenario_buttonEditScenarioTagName.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.pageChooseScenario_buttonEditScenarioTagName_ButtonClick);
      // 
      // pageChooseScenario_radioGroupScenarioAction
      // 
      this.pageChooseScenario_radioGroupScenarioAction.Location = new System.Drawing.Point(8, 24);
      this.pageChooseScenario_radioGroupScenarioAction.Name = "pageChooseScenario_radioGroupScenarioAction";
      // 
      // pageChooseScenario_radioGroupScenarioAction.Properties
      // 
      this.pageChooseScenario_radioGroupScenarioAction.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
      this.pageChooseScenario_radioGroupScenarioAction.Properties.Appearance.Options.UseBackColor = true;
      this.pageChooseScenario_radioGroupScenarioAction.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
      this.pageChooseScenario_radioGroupScenarioAction.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
                                                                                                                                        new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Create a New Scenario Tag"),
                                                                                                                                        new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Use an Existing Scenario Tag")});
      this.pageChooseScenario_radioGroupScenarioAction.Size = new System.Drawing.Size(264, 56);
      this.pageChooseScenario_radioGroupScenarioAction.TabIndex = 0;
      this.pageChooseScenario_radioGroupScenarioAction.SelectedIndexChanged += new System.EventHandler(this.pageChooseScenario_radioGroupScenarioAction_SelectedIndexChanged);
      // 
      // pageGenerateScenario
      // 
      this.pageGenerateScenario.Controls.Add(this.pageGenerateScenario_groupControlSBSPTag);
      this.pageGenerateScenario.Controls.Add(this.pageGenerateScenario_labelHelpText);
      this.pageGenerateScenario.Description = "Please select a BSP tag for your scenario.";
      this.pageGenerateScenario.Location = new System.Drawing.Point(0, 0);
      this.pageGenerateScenario.Name = "pageGenerateScenario";
      this.pageGenerateScenario.Owner = this.wizard1;
      this.pageGenerateScenario.PreviousPage = this.pageChooseScenario;
      this.pageGenerateScenario.Size = new System.Drawing.Size(428, 208);
      this.pageGenerateScenario.TabIndex = 20;
      this.pageGenerateScenario.Title = "Scenario BSP";
      this.pageGenerateScenario.BeforeNext += new CristiPotlog.Controls.Wizard.ChangePageEventHandler(this.pageGenerateScenario_BeforeNext);
      // 
      // pageGenerateScenario_groupControlSBSPTag
      // 
      this.pageGenerateScenario_groupControlSBSPTag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.pageGenerateScenario_groupControlSBSPTag.AutoScroll = true;
      this.pageGenerateScenario_groupControlSBSPTag.Controls.Add(this.pageGenerateScenario_buttonEditSBSPTag);
      this.pageGenerateScenario_groupControlSBSPTag.Location = new System.Drawing.Point(8, 120);
      this.pageGenerateScenario_groupControlSBSPTag.Name = "pageGenerateScenario_groupControlSBSPTag";
      this.pageGenerateScenario_groupControlSBSPTag.Size = new System.Drawing.Size(412, 72);
      this.pageGenerateScenario_groupControlSBSPTag.TabIndex = 25;
      this.pageGenerateScenario_groupControlSBSPTag.Text = "Scenario BSP Tag";
      // 
      // pageGenerateScenario_buttonEditSBSPTag
      // 
      this.pageGenerateScenario_buttonEditSBSPTag.EditValue = "";
      this.pageGenerateScenario_buttonEditSBSPTag.Location = new System.Drawing.Point(16, 32);
      this.pageGenerateScenario_buttonEditSBSPTag.Name = "pageGenerateScenario_buttonEditSBSPTag";
      // 
      // pageGenerateScenario_buttonEditSBSPTag.Properties
      // 
      this.pageGenerateScenario_buttonEditSBSPTag.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                                                   new DevExpress.XtraEditors.Controls.EditorButton()});
      this.pageGenerateScenario_buttonEditSBSPTag.Size = new System.Drawing.Size(448, 20);
      this.pageGenerateScenario_buttonEditSBSPTag.TabIndex = 1;
      this.pageGenerateScenario_buttonEditSBSPTag.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.pageGenerateScenario_buttonEditSBSPTag_ButtonClick);
      // 
      // pageGenerateScenario_labelHelpText
      // 
      this.pageGenerateScenario_labelHelpText.Location = new System.Drawing.Point(16, 80);
      this.pageGenerateScenario_labelHelpText.Name = "pageGenerateScenario_labelHelpText";
      this.pageGenerateScenario_labelHelpText.Size = new System.Drawing.Size(456, 32);
      this.pageGenerateScenario_labelHelpText.TabIndex = 0;
      this.pageGenerateScenario_labelHelpText.Text = "A scenario tag requires a pre-existing BSP tag containing the map\'s geometry and " +
        "collision data.  Please choose the BSP tag that you wish to use with your scenar" +
        "io.";
      // 
      // pageFinish
      // 
      this.pageFinish.Description = "You have successfully created a new project.  You may now close this wizard.";
      this.pageFinish.Location = new System.Drawing.Point(0, 0);
      this.pageFinish.Name = "pageFinish";
      this.pageFinish.Owner = this.wizard1;
      this.pageFinish.PreviousPage = this.pageGenerateScenario;
      this.pageFinish.Size = new System.Drawing.Size(482, 320);
      this.pageFinish.Style = CristiPotlog.Controls.WizardPageStyle.Finish;
      this.pageFinish.TabIndex = 18;
      this.pageFinish.Title = "New Project Created";
      this.pageFinish.BeforeShow += new System.EventHandler(this.pageFinish_BeforeShow);
      // 
      // NewProjectWizard
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
      this.ClientSize = new System.Drawing.Size(482, 368);
      this.Controls.Add(this.wizard1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "NewProjectWizard";
      this.Text = "NewProjectWizard";
      this.wizard1.ResumeLayout(false);
      this.pageTargetPlatform.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pageTargetPlatform_groupControlPlatforms)).EndInit();
      this.pageTargetPlatform_groupControlPlatforms.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pageTargetPlatform_radioGroupPlatforms.Properties)).EndInit();
      this.pageProjectInformation.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pageProjectInformation_groupControlAuthorInformation)).EndInit();
      this.pageProjectInformation_groupControlAuthorInformation.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pageProjectInformation_textEditAuthorName.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pageProjectInformation_groupComtrolMapInformation)).EndInit();
      this.pageProjectInformation_groupComtrolMapInformation.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pageProjectInformation_memoDescription.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pageProjectInformation_textMapTitle.Properties)).EndInit();
      this.pageMapFilenameSelect.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pageMapFilenameSelect_groupControlFileInformation)).EndInit();
      this.pageMapFilenameSelect_groupControlFileInformation.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pageMapFilenameSelect_comboBoxFilename.Properties)).EndInit();
      this.pageChooseScenario.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pageChooseScenario_groupControlScenarioTag)).EndInit();
      this.pageChooseScenario_groupControlScenarioTag.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pageChooseScenario_buttonEditScenarioTagName.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pageChooseScenario_radioGroupScenarioAction.Properties)).EndInit();
      this.pageGenerateScenario.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pageGenerateScenario_groupControlSBSPTag)).EndInit();
      this.pageGenerateScenario_groupControlSBSPTag.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pageGenerateScenario_buttonEditSBSPTag.Properties)).EndInit();
      this.ResumeLayout(false);

    }
    #endregion

    #region WizardPage: TargetPlatform
    private void pageTargetPlatform_BeforeNext(object sender, Wizard.ChangePageEventArgs e)
    {
      switch (this.pageTargetPlatform_radioGroupPlatforms.SelectedIndex)
      {
        case 0:
          mapVersion = MapfileVersion.HALOPC;
          //this.platform = (TargetPlatform)pageTargetPlatform_radioGroupPlatforms.SelectedIndex;
          break;
        case 1:
          mapVersion = MapfileVersion.HALOCE;
          e.Cancel = true;
          Dialogs.ShowError("The selected option is not yet supported: " + 
            pageTargetPlatform_radioGroupPlatforms.Properties.Items[pageTargetPlatform_radioGroupPlatforms.SelectedIndex]);
          return;
        case 2:
          mapVersion = MapfileVersion.XHALO1;
          //this.platform = (TargetPlatform)pageTargetPlatform_radioGroupPlatforms.SelectedIndex;
          break;
        case 3:
          //mapVersion = MapfileVersion.XHALO2;
          goto case 1;
        default:
          e.Cancel = true;
          Dialogs.ShowError("You must choose a target platform to continue.");
          return;
      }
    }
    #endregion

    #region WizardPage: ProjectInformation
    private void pageProjectInformation_BeforeNext(object sender, Wizard.ChangePageEventArgs e)
    {
      if (this.pageProjectInformation_textMapTitle.Text == "")
      {
        e.Cancel = true;
        Dialogs.ShowError("You must provide a title for the map.");
        return;
      }
      if (this.pageProjectInformation_textEditAuthorName.Text == "")
      {
        e.Cancel = true;
        Dialogs.ShowError("You must provide the author's name.");
        return;
      }
      // Setup the new project file.
      ProjectTemplate template;

      // Set the appropriate next page based on the type.
      if(this.mapVersion == MapfileVersion.HALOPC)
      {
        template = ProjectTemplates.GetTemplate("HaloPCMultiplayer");
        project = new ProjectFile(
          this.pageProjectInformation_textMapTitle.Text,
          this.pageProjectInformation_textEditAuthorName.Text,
          template);
        e.NextPage = this.pageMapFilenameSelect;
      }
      else if(this.mapVersion == MapfileVersion.XHALO1)
      {
        template = ProjectTemplates.GetTemplate("Halo1XboxMultiplayer");
        project = new ProjectFile(
          this.pageProjectInformation_textMapTitle.Text,
          this.pageProjectInformation_textEditAuthorName.Text,
          template);
        e.NextPage = this.pageMapFilenameSelect;
      }
      // TODO: Add pages for HaloCE and Halo2 as they are supported that will allow
      // the user to manually enter a filename rather than choosing from a list.
    }
    #endregion

    #region WizardPage: MapFilenameSelect
    private void pageMapFilenameSelect_BeforeNext(object sender, CristiPotlog.Controls.Wizard.ChangePageEventArgs e)
    {
      // Ensure that a filename was chosen.
      if (this.pageMapFilenameSelect_comboBoxFilename.SelectedItem != "")
      {
        project.Filename = (this.pageMapFilenameSelect_comboBoxFilename.SelectedItem as MapFilename).Filename;
      }
      else
      {
        e.Cancel = true;
        Dialogs.ShowError("You must select a map to be replaced from the list.");
        return;
      }
    }
    internal class MapFilename
    {
      public string Filename;
      public string Name;
      public MapFilename(string filename, string name)
      {
        Filename = filename;
        Name = name;
      }
      public override string ToString()
      {
        return String.Format("{0} ({1})", Name, Filename);
      }
    }
    private void pageMapFilenameSelect_BeforeShow(object sender, System.EventArgs e)
    {
      // NOTE: This is the HaloPC list - we will need to conditionally build
      // this list once Halo Xbox support has been added.
      this.pageMapFilenameSelect_comboBoxFilename.Properties.Items.Clear();
      this.pageMapFilenameSelect_comboBoxFilename.Properties.Items.Add(new MapFilename("beavercreek.map", "Battle Creek"));
      this.pageMapFilenameSelect_comboBoxFilename.Properties.Items.Add(new MapFilename("bloodgulch.map", "Blood Gulch"));
      this.pageMapFilenameSelect_comboBoxFilename.Properties.Items.Add(new MapFilename("boardingaction.map", "Boarding Action"));
      this.pageMapFilenameSelect_comboBoxFilename.Properties.Items.Add(new MapFilename("carousel.map", "Derelict"));
      this.pageMapFilenameSelect_comboBoxFilename.Properties.Items.Add(new MapFilename("chillout.map", "Chill Out"));
      this.pageMapFilenameSelect_comboBoxFilename.Properties.Items.Add(new MapFilename("putput.map", "Chiron TL34"));
      this.pageMapFilenameSelect_comboBoxFilename.Properties.Items.Add(new MapFilename("damnation.map", "Damnation"));
      this.pageMapFilenameSelect_comboBoxFilename.Properties.Items.Add(new MapFilename("dangercanyon.map", "Danger Canyon"));
      this.pageMapFilenameSelect_comboBoxFilename.Properties.Items.Add(new MapFilename("deathisland.map", "Death Island"));
      this.pageMapFilenameSelect_comboBoxFilename.Properties.Items.Add(new MapFilename("gephyrophobia.map", "Gephyrophobia"));
      this.pageMapFilenameSelect_comboBoxFilename.Properties.Items.Add(new MapFilename("hangemhigh.map", "Hang 'Em High"));
      this.pageMapFilenameSelect_comboBoxFilename.Properties.Items.Add(new MapFilename("icefields.map", "Ice Fields"));
      this.pageMapFilenameSelect_comboBoxFilename.Properties.Items.Add(new MapFilename("infinity.map", "Infinity"));
      this.pageMapFilenameSelect_comboBoxFilename.Properties.Items.Add(new MapFilename("longest.map", "Longestk"));
      this.pageMapFilenameSelect_comboBoxFilename.Properties.Items.Add(new MapFilename("prisoner.map", "Prisoner"));
      this.pageMapFilenameSelect_comboBoxFilename.Properties.Items.Add(new MapFilename("ratrace.map", "Rat Race"));
      this.pageMapFilenameSelect_comboBoxFilename.Properties.Items.Add(new MapFilename("sidewinder.map", "Sidewinder"));
      this.pageMapFilenameSelect_comboBoxFilename.Properties.Items.Add(new MapFilename("timberland.map", "Timberland"));
      this.pageMapFilenameSelect_comboBoxFilename.Properties.Items.Add(new MapFilename("wizard.map", "Wizard"));
    }
    #endregion

    #region WizardPage: ChooseScenario
    private void pageChooseScenario_radioGroupScenarioAction_BeforeShow(object sender, EventArgs e)
    {
      SetupControls_pageChooseScenario();
    }
    private void pageChooseScenario_radioGroupScenarioAction_SelectedIndexChanged(object sender, EventArgs e)
    {
      SetupControls_pageChooseScenario();
    }
    internal void SetupControls_pageChooseScenario()
    {
      switch (this.pageChooseScenario_radioGroupScenarioAction.SelectedIndex)
      {
        case 1:
          this.pageChooseScenario_buttonEditScenarioTagName.Enabled = true;
          break;
        default:
          this.pageChooseScenario_buttonEditScenarioTagName.Enabled = false;
          break;
      }
    }
    private void pageChooseScenario_buttonEditScenarioTagName_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
    {
      TagBrowserDialog tagBrowser = new TagBrowserDialog(this.mapVersion);
      tagBrowser.AddFilter("Scenario Tags|*.scenario");
      if (tagBrowser.ShowDialog() == DialogResult.Cancel) return;
      this.pageChooseScenario_buttonEditScenarioTagName.Text = tagBrowser.SelectedTag.RelativePath;
      this.pageChooseScenario_buttonEditScenarioTagName.Tag = tagBrowser.SelectedTag;
    }
    private void pageChooseScenario_BeforeNext(object sender, CristiPotlog.Controls.Wizard.ChangePageEventArgs e)
    {
      if (this.pageChooseScenario_radioGroupScenarioAction.SelectedIndex == 0)
      {
        e.NextPage = pageGenerateScenario;
      }
      else
      {
        if (this.pageChooseScenario_buttonEditScenarioTagName.Text == "")
        {
          e.Cancel = true;
          Dialogs.ShowError("You must provide a valid scenario filename.");
          return;
        }
        else
        {
          // TODO: Ensure that the selected file exsits before moving on.
          this.project.Tags.Add(new ProjectTag("Scenario", this.pageChooseScenario_buttonEditScenarioTagName.Text));
          e.NextPage = pageFinish;
        }
      }
    }
    #endregion

    #region WizardPage: GenerateScenario
    private void pageGenerateScenario_buttonEditSBSPTag_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
    {
      TagBrowserDialog tagBrowser = new TagBrowserDialog(MapfileVersion.HALOPC);
      tagBrowser.AddFilter("SBSP Tags|*.scenario_structure_bsp");
      if (tagBrowser.ShowDialog() == DialogResult.Cancel) return;
      this.pageGenerateScenario_buttonEditSBSPTag.Text = tagBrowser.SelectedTag.RelativePath;
    }
    private void pageGenerateScenario_BeforeNext(object sender, CristiPotlog.Controls.Wizard.ChangePageEventArgs e)
    {
      if (this.pageGenerateScenario_buttonEditSBSPTag.Text == "")
      {
        e.Cancel = true;
        Dialogs.ShowError("You must provide a valid SBSP tag.");
        return;
      }
      else
      {
        // TODO: We need to implement some code to generate a scenario tag and apply the selected BSP.
        e.Cancel = true;
        Dialogs.ShowError("TODO:\r\nThe scenario tag generation has not been implemented.\r\nUnable to continue beyond this point.");
        return;
      }
    }
    #endregion

    #region WizardParge: Finish
    private void pageFinish_BeforeShow(object sender, System.EventArgs e)
    {
      // Save the project file
      // This code will likely be moved into the ProjectManager to have a standardized
      // way of creating a new project and saving it, but it can go here for now.
      string path = OptionsManager.GetProjectBaseFolder(this.mapVersion) + project.MapName;
      if (!Directory.Exists(path)) Directory.CreateDirectory(path);

      // We need to copy this file into our project folder.
      TagFileName file = (this.pageChooseScenario_buttonEditScenarioTagName.Tag as TagFileName);
          
      // We need a more encapsulated method of getting files.
      if (file.Source == TagSource.Archive)
      {
        ITagLibrary lib = null;
        switch(this.mapVersion)
        {
          case MapfileVersion.HALOPC:
          case MapfileVersion.HALOCE:
            lib = TagLibraryManager.HaloPC;
            break;
          case MapfileVersion.XHALO1:
            lib = TagLibraryManager.HaloXbox;
            break;
          case MapfileVersion.XHALO2:
            lib = TagLibraryManager.Halo2Xbox;
            break;
        }

        byte[] tag = lib.ReadFile(file.RelativePath);
        string scenarioFilename = path + "\\Tags\\" + file.RelativePath;
        string scenarioFolder = Path.GetDirectoryName(scenarioFilename);
        if (!Directory.Exists(scenarioFolder)) Directory.CreateDirectory(scenarioFolder);
        if (File.Exists(scenarioFilename)) File.Delete(scenarioFilename);
        FileStream fs = File.Create(scenarioFilename);
        fs.Write(tag, 0, tag.Length);
        fs.Close();
      }

      string xmlData = project.SaveToXML();
      this.projectFilename = path + "\\" + project.MapName + ".pmproj";
      StreamWriter writer = new StreamWriter(this.projectFilename);
      writer.Write(xmlData);
      writer.Close();
    }
    #endregion
  }
}