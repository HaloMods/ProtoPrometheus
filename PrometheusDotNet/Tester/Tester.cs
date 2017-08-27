using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Microsoft.CSharp;
using Prometheus.Core;
using Prometheus.Core.Project;
using Prometheus.Core.Tags;
using Prometheus.TagEditor;
using TagEditor;
using TagLibrary;
using TagLibrary.Halo1;
using UIControls.StandardControls;

namespace Tester
{
	/// <summary>
	/// Summary description for Tester.
	/// </summary>
	public class Tester : Form
	{
    private Label label1;
    private TabControl tabControl1;
    private TabPage tabPage1;
    private Button btnTestFieldCodeGeneration;
    private Button btnTestXMLConverter;
    private Button btnTestOptionsManager;
    private Button btnTestTagLoaders;
    private Button btnGenerateMasterTagList;
    private Button btnBuildTagListTreeview;
    private Button btnCreateNewProjectFile;
    private Button btnLoadProjectFile;
    private TabPage tabPage2;
    private Button buttonTestEditorDataBinder;
    private Button btnTestTagEditorGUI;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Button btnTestNotes;
		private TagEditor.Controls.AngleEdit angleEdit1;
		private DevExpress.XtraEditors.SpinEdit sizeChange;
		private System.Windows.Forms.Label label2;
		private TagEditor.Controls.RegionContainer regionContainer1;
    private ControlListPanel panel1;
    private System.Windows.Forms.Button button1;
    private TagEditor.Controls.BlockContainer blockContainer1;
    private System.Windows.Forms.Button button2;
    private TagEditor.Controls.ShortInteger shortInteger1;
		private UIControls.StandardControls.ImageButton imageButton1;
		private DevExpress.XtraEditors.PanelControl panelControl1;
    private UIControls.StandardControls.ImageLabel imageLabel1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main() 
    {
      Tester form = new Tester();
      form.CreateControl();
      Application.Run(form);
    }

		public Tester()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
   
      TagLibraryManager.Initialize();

//      // Open a file...
//      OpenFileDialog ofd = new OpenFileDialog();
//      ofd.Filter = "XML Files (*.xml)|*.xml";
//      if (ofd.ShowDialog() == DialogResult.Cancel) return;
//
//      StreamWriter writer = new StreamWriter(Path.GetFileNameWithoutExtension(ofd.FileName) + ".cs");
//      
//      XmlDocument doc = new XmlDocument();
//      doc.Load(ofd.FileName);
//      ClassGenerator generator = new ClassGenerator();
//      string result = generator.GenerateClass(doc.SelectSingleNode("//xml"));
//      writer.Write(result);
//      writer.Close();
//      MessageBox.Show("Wrote to the new file.");

		  //TagLibraryManager.Initialize();
		  GrenTest.Initialize();
//      TagLibraryManager.Initialize(
//        @"F:\Documents and Settings\Justin\My Documents\Visual Studio Projects\Prometheus\Source\PrometheusDotNet\Prometheus\bin\Debug\Games\PC\Halo\hpc.pta",
//        @"F:\Documents and Settings\Justin\My Documents\Visual Studio Projects\Prometheus\Source\PrometheusDotNet\Prometheus\bin\Debug\Games\Xbox\Halo\hxb.pta",
//        @"F:\Documents and Settings\Justin\My Documents\Visual Studio Projects\Prometheus\Source\PrometheusDotNet\Prometheus\bin\Debug\Games\Xbox\Halo 2\h2xb.pta");
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
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Tester));
      this.label1 = new System.Windows.Forms.Label();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.btnTestTagEditorGUI = new System.Windows.Forms.Button();
      this.btnLoadProjectFile = new System.Windows.Forms.Button();
      this.btnCreateNewProjectFile = new System.Windows.Forms.Button();
      this.btnBuildTagListTreeview = new System.Windows.Forms.Button();
      this.btnGenerateMasterTagList = new System.Windows.Forms.Button();
      this.btnTestTagLoaders = new System.Windows.Forms.Button();
      this.btnTestOptionsManager = new System.Windows.Forms.Button();
      this.btnTestXMLConverter = new System.Windows.Forms.Button();
      this.btnTestFieldCodeGeneration = new System.Windows.Forms.Button();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.buttonTestEditorDataBinder = new System.Windows.Forms.Button();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
      this.imageButton1 = new UIControls.StandardControls.ImageButton();
      this.label2 = new System.Windows.Forms.Label();
      this.sizeChange = new DevExpress.XtraEditors.SpinEdit();
      this.angleEdit1 = new TagEditor.Controls.AngleEdit();
      this.btnTestNotes = new System.Windows.Forms.Button();
      this.panel1 = new UIControls.StandardControls.ControlListPanel();
      this.button1 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.imageLabel1 = new UIControls.StandardControls.ImageLabel();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.tabPage3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
      this.panelControl1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.sizeChange.Properties)).BeginInit();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(8, 8);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(360, 40);
      this.label1.TabIndex = 0;
      this.label1.Text = "Test your new code here!  Make your own tab so you do not interefere with other p" +
        "eople\'s tests.";
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Controls.Add(this.tabPage3);
      this.tabControl1.Location = new System.Drawing.Point(8, 56);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(360, 320);
      this.tabControl1.TabIndex = 1;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.btnTestTagEditorGUI);
      this.tabPage1.Controls.Add(this.btnLoadProjectFile);
      this.tabPage1.Controls.Add(this.btnCreateNewProjectFile);
      this.tabPage1.Controls.Add(this.btnBuildTagListTreeview);
      this.tabPage1.Controls.Add(this.btnGenerateMasterTagList);
      this.tabPage1.Controls.Add(this.btnTestTagLoaders);
      this.tabPage1.Controls.Add(this.btnTestOptionsManager);
      this.tabPage1.Controls.Add(this.btnTestXMLConverter);
      this.tabPage1.Controls.Add(this.btnTestFieldCodeGeneration);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Size = new System.Drawing.Size(352, 294);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Mono";
      // 
      // btnTestTagEditorGUI
      // 
      this.btnTestTagEditorGUI.Location = new System.Drawing.Point(8, 264);
      this.btnTestTagEditorGUI.Name = "btnTestTagEditorGUI";
      this.btnTestTagEditorGUI.Size = new System.Drawing.Size(336, 23);
      this.btnTestTagEditorGUI.TabIndex = 8;
      this.btnTestTagEditorGUI.Text = "Test Tag Editor GUI";
      this.btnTestTagEditorGUI.Click += new System.EventHandler(this.btnTestTagEditorGUI_Click);
      // 
      // btnLoadProjectFile
      // 
      this.btnLoadProjectFile.Location = new System.Drawing.Point(8, 232);
      this.btnLoadProjectFile.Name = "btnLoadProjectFile";
      this.btnLoadProjectFile.Size = new System.Drawing.Size(336, 23);
      this.btnLoadProjectFile.TabIndex = 7;
      this.btnLoadProjectFile.Text = "Load a Project File";
      this.btnLoadProjectFile.Click += new System.EventHandler(this.btnLoadProjectFile_Click);
      // 
      // btnCreateNewProjectFile
      // 
      this.btnCreateNewProjectFile.Location = new System.Drawing.Point(8, 200);
      this.btnCreateNewProjectFile.Name = "btnCreateNewProjectFile";
      this.btnCreateNewProjectFile.Size = new System.Drawing.Size(336, 23);
      this.btnCreateNewProjectFile.TabIndex = 6;
      this.btnCreateNewProjectFile.Text = "Create New Project File";
      this.btnCreateNewProjectFile.Click += new System.EventHandler(this.btnCreateNewProjectFile_Click);
      // 
      // btnBuildTagListTreeview
      // 
      this.btnBuildTagListTreeview.Location = new System.Drawing.Point(8, 168);
      this.btnBuildTagListTreeview.Name = "btnBuildTagListTreeview";
      this.btnBuildTagListTreeview.Size = new System.Drawing.Size(336, 23);
      this.btnBuildTagListTreeview.TabIndex = 5;
      this.btnBuildTagListTreeview.Text = "Build Tag List TreeView";
      this.btnBuildTagListTreeview.Click += new System.EventHandler(this.btnBuildTagListTreeview_Click);
      // 
      // btnGenerateMasterTagList
      // 
      this.btnGenerateMasterTagList.Location = new System.Drawing.Point(8, 136);
      this.btnGenerateMasterTagList.Name = "btnGenerateMasterTagList";
      this.btnGenerateMasterTagList.Size = new System.Drawing.Size(336, 23);
      this.btnGenerateMasterTagList.TabIndex = 4;
      this.btnGenerateMasterTagList.Text = "Generate Master Tag List";
      this.btnGenerateMasterTagList.Click += new System.EventHandler(this.btnGenerateMasterTagList_Click);
      // 
      // btnTestTagLoaders
      // 
      this.btnTestTagLoaders.Location = new System.Drawing.Point(8, 104);
      this.btnTestTagLoaders.Name = "btnTestTagLoaders";
      this.btnTestTagLoaders.Size = new System.Drawing.Size(336, 23);
      this.btnTestTagLoaders.TabIndex = 3;
      this.btnTestTagLoaders.Text = "Test Tag Loaders";
      this.btnTestTagLoaders.Click += new System.EventHandler(this.btnTestTagLoaders_Click);
      // 
      // btnTestOptionsManager
      // 
      this.btnTestOptionsManager.Location = new System.Drawing.Point(8, 72);
      this.btnTestOptionsManager.Name = "btnTestOptionsManager";
      this.btnTestOptionsManager.Size = new System.Drawing.Size(336, 23);
      this.btnTestOptionsManager.TabIndex = 2;
      this.btnTestOptionsManager.Text = "Test OptionsManager";
      this.btnTestOptionsManager.Click += new System.EventHandler(this.btnTestOptionsManager_Click);
      // 
      // btnTestXMLConverter
      // 
      this.btnTestXMLConverter.Location = new System.Drawing.Point(8, 40);
      this.btnTestXMLConverter.Name = "btnTestXMLConverter";
      this.btnTestXMLConverter.Size = new System.Drawing.Size(336, 23);
      this.btnTestXMLConverter.TabIndex = 1;
      this.btnTestXMLConverter.Text = "Test XML Converter";
      this.btnTestXMLConverter.Click += new System.EventHandler(this.btnTestXMLConverter_Click);
      // 
      // btnTestFieldCodeGeneration
      // 
      this.btnTestFieldCodeGeneration.Location = new System.Drawing.Point(8, 8);
      this.btnTestFieldCodeGeneration.Name = "btnTestFieldCodeGeneration";
      this.btnTestFieldCodeGeneration.Size = new System.Drawing.Size(336, 23);
      this.btnTestFieldCodeGeneration.TabIndex = 0;
      this.btnTestFieldCodeGeneration.Text = "Test Field Code Generation";
      this.btnTestFieldCodeGeneration.Click += new System.EventHandler(this.btnTestFieldCodeGeneration_Click);
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.buttonTestEditorDataBinder);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Size = new System.Drawing.Size(352, 294);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Gren";
      // 
      // buttonTestEditorDataBinder
      // 
      this.buttonTestEditorDataBinder.Location = new System.Drawing.Point(8, 8);
      this.buttonTestEditorDataBinder.Name = "buttonTestEditorDataBinder";
      this.buttonTestEditorDataBinder.Size = new System.Drawing.Size(336, 23);
      this.buttonTestEditorDataBinder.TabIndex = 1;
      this.buttonTestEditorDataBinder.Text = "Test Editor Data Binder";
      this.buttonTestEditorDataBinder.Click += new System.EventHandler(this.buttonTestEditorDataBinder_Click);
      // 
      // tabPage3
      // 
      this.tabPage3.Controls.Add(this.panelControl1);
      this.tabPage3.Controls.Add(this.label2);
      this.tabPage3.Controls.Add(this.sizeChange);
      this.tabPage3.Controls.Add(this.angleEdit1);
      this.tabPage3.Controls.Add(this.btnTestNotes);
      this.tabPage3.Location = new System.Drawing.Point(4, 22);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Size = new System.Drawing.Size(352, 294);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "rec0";
      // 
      // panelControl1
      // 
      this.panelControl1.Controls.Add(this.imageButton1);
      this.panelControl1.Location = new System.Drawing.Point(136, 200);
      this.panelControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
      this.panelControl1.LookAndFeel.UseDefaultLookAndFeel = false;
      this.panelControl1.LookAndFeel.UseWindowsXPTheme = false;
      this.panelControl1.Name = "panelControl1";
      this.panelControl1.Size = new System.Drawing.Size(184, 80);
      this.panelControl1.TabIndex = 7;
      this.panelControl1.Text = "panelControl1";
      // 
      // imageButton1
      // 
      this.imageButton1.AltImage = null;
      this.imageButton1.Appearance.Options.UseBackColor = true;
      this.imageButton1.HotTrack = true;
      this.imageButton1.Image = ((System.Drawing.Image)(resources.GetObject("imageButton1.Image")));
      this.imageButton1.ImageSizeRatio = 0.8F;
      this.imageButton1.Location = new System.Drawing.Point(64, 16);
      this.imageButton1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
      this.imageButton1.LookAndFeel.UseDefaultLookAndFeel = false;
      this.imageButton1.LookAndFeel.UseWindowsXPTheme = false;
      this.imageButton1.Name = "imageButton1";
      this.imageButton1.PressedImage = null;
      this.imageButton1.Size = new System.Drawing.Size(56, 48);
      this.imageButton1.TabIndex = 6;
      this.imageButton1.Toggle = false;
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(216, 56);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(80, 24);
      this.label2.TabIndex = 4;
      this.label2.Text = "Control Size:";
      // 
      // sizeChange
      // 
      this.sizeChange.EditValue = new System.Decimal(new int[] {
                                                                 50,
                                                                 0,
                                                                 0,
                                                                 0});
      this.sizeChange.Location = new System.Drawing.Point(296, 56);
      this.sizeChange.Name = "sizeChange";
      // 
      // sizeChange.Properties
      // 
      this.sizeChange.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                       new DevExpress.XtraEditors.Controls.EditorButton()});
      this.sizeChange.Properties.UseCtrlIncrement = false;
      this.sizeChange.Size = new System.Drawing.Size(40, 20);
      this.sizeChange.TabIndex = 3;
      this.sizeChange.EditValueChanged += new System.EventHandler(this.sizeChange_EditValueChanged);
      // 
      // angleEdit1
      // 
      this.angleEdit1.Angle = 0;
      this.angleEdit1.Location = new System.Drawing.Point(24, 56);
      this.angleEdit1.Name = "angleEdit1";
      this.angleEdit1.Size = new System.Drawing.Size(50, 50);
      this.angleEdit1.TabIndex = 2;
      // 
      // btnTestNotes
      // 
      this.btnTestNotes.Location = new System.Drawing.Point(8, 8);
      this.btnTestNotes.Name = "btnTestNotes";
      this.btnTestNotes.Size = new System.Drawing.Size(336, 23);
      this.btnTestNotes.TabIndex = 1;
      this.btnTestNotes.Text = "Test Notes Dialog";
      this.btnTestNotes.Click += new System.EventHandler(this.btnTestNotes_Click);
      // 
      // panel1
      // 
      this.panel1.Location = new System.Drawing.Point(376, 8);
      this.panel1.Name = "panel1";
      this.panel1.Padding = 10;
      this.panel1.ResizeBehavior = UIControls.StandardControls.ResizeBehavior.None;
      this.panel1.Size = new System.Drawing.Size(122, 90);
      this.panel1.Spacing = 10;
      this.panel1.TabIndex = 2;
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(392, 232);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(104, 22);
      this.button1.TabIndex = 0;
      this.button1.Text = "More Benchmarks";
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(392, 200);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(104, 23);
      this.button2.TabIndex = 5;
      this.button2.Text = "Benchmark";
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // imageLabel1
      // 
      this.imageLabel1.Appearance.BackColor = System.Drawing.Color.IndianRed;
      this.imageLabel1.Appearance.Options.UseBackColor = true;
      this.imageLabel1.Captions = "This is a test of the emergency broadcast system.  This is only a test.";
      this.imageLabel1.ForegroundColor = System.Drawing.Color.DarkRed;
      this.imageLabel1.Image = ((System.Drawing.Image)(resources.GetObject("imageLabel1.Image")));
      this.imageLabel1.Location = new System.Drawing.Point(392, 144);
      this.imageLabel1.Name = "imageLabel1";
      this.imageLabel1.PanelColor = System.Drawing.Color.IndianRed;
      this.imageLabel1.Size = new System.Drawing.Size(288, 48);
      this.imageLabel1.TabIndex = 7;
      // 
      // Tester
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(792, 438);
      this.Controls.Add(this.imageLabel1);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.button1);
      this.Name = "Tester";
      this.Text = "Tester";
      this.Load += new System.EventHandler(this.Tester_Load);
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      this.tabPage3.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
      this.panelControl1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.sizeChange.Properties)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

    private void btnTestFieldCodeGeneration_Click(object sender, EventArgs e)
    {
      //OpenFileDialog ofd = new OpenFileDialog();
      //if (ofd.ShowDialog() == DialogResult.Cancel) return;
      //TagUtils.GenerateClassStructure(ofd.FileName, "");
      TagUtils.GenerateClassStructure(@"C:\jimbo\halotech\dev\Source\PrometheusDotNet\TagLibrary\Halo1\XML\scenario_structure_bsp.xml", "");

//      FolderBrowserDialog fbd = new FolderBrowserDialog();
//      fbd.ShowNewFolderButton = false;
//      fbd.SelectedPath = Application.StartupPath;
//
//      if(fbd.ShowDialog() != DialogResult.Cancel)
//      {
//        TagUtils.GenerateMultiClassStructure(fbd.SelectedPath, "");
//      }
    }

    private void btnTestXMLConverter_Click(object sender, EventArgs e)
    {
      int x=0;
      if (!Directory.Exists(Application.StartupPath + "\\XmlDefinitions\\Prometheus"))
        Directory.CreateDirectory(Application.StartupPath + "\\XmlDefinitions\\Prometheus");
      if (!Directory.Exists(Application.StartupPath + "\\XmlDefinitions\\Kornman"))
        Directory.CreateDirectory(Application.StartupPath + "\\XmlDefinitions\\Kornman");

      string inPath = Application.StartupPath + "\\XmlDefinitions\\Kornman";
      string outPath = Application.StartupPath + "\\XmlDefinitions\\Prometheus";

      foreach (string filename in Directory.GetFiles(inPath))
      {
        if (Path.GetExtension(filename) != ".xml") continue;
        XmlDocument original = new XmlDocument();
        original.Load(filename);

        XmlDocument doc = XMLConverter.ToPrometheusFormat(original);
        doc.Save(outPath + "\\" + Path.GetFileName(filename));
        x++;
      }
      MessageBox.Show(x.ToString() + " file(s) converted." + "\r\nFiles saved to " + outPath);
    }

    private void btnTestOptionsManager_Click(object sender, EventArgs e)
    {
      OptionsManager.Initialize(Application.StartupPath + "\\prometheus.ini", Application.StartupPath + "\\user.ini");
      bool isItTrue = (bool)OptionsManager.Core["Test Group", "Value1", false];
      string path = (string)OptionsManager.Core["Paths", "HaloCEPath", @"C:\STFU\Halo"];
      int someInteger = (int)OptionsManager.Core["Test Group", "IntegerTest", 12345];
      float someFloat = (float)OptionsManager.Core["Test Group", "FloatTest", (float)Math.PI];
      OptionsManager.Core["Stuff", "Something"] = false;
      OptionsManager.Core["Test Group", "IntegerTest"] = 555;
      OptionsManager.User["GUI", "Stuff"] = "MB";
      OptionsManager.User["GUI", "Nick is gay"] = true;
    }

    private void btnTestTagLoaders_Click(object sender, EventArgs e)
    {
      OpenFileDialog ofd = new OpenFileDialog();
      if (ofd.ShowDialog() == DialogResult.Cancel) return;
      string filename = ofd.FileName;
      
      int count = 1;
      ShaderTransparentGlass[] tags = new ShaderTransparentGlass[count];
      for (int x=0; x<count; x++)
      {
        BinaryReader br = new BinaryReader(new FileStream(filename, FileMode.Open)); 
        TagHeader header = new TagHeader();
        header.Read(ref br);
      
        tags[x] = new ShaderTransparentGlass();
        tags[x].Read(br);
        tags[x].ReadChildData(br);
        br.Close();
      }
      MessageBox.Show("Done!");
    }

    private void btnGenerateMasterTagList_Click(object sender, EventArgs e)
    {
      FolderBrowserDialog fbd = new FolderBrowserDialog();
      fbd.Description = "Select the folder which contains the HaloPC map files.";
      if (fbd.ShowDialog() == DialogResult.Cancel) return;

      HaloMapInterface map = new HaloMapInterface();

      foreach (string filename in Directory.GetFiles(fbd.SelectedPath))
      {
        string fileExtension = Path.GetExtension(filename);
        if (fileExtension == ".map")
        {
          if ((Path.GetFileNameWithoutExtension(filename) != "bitmaps") && (Path.GetFileNameWithoutExtension(filename) != "sounds"))
          {
            FileStream stream = new FileStream(filename, FileMode.Open);
            map.LoadMap(stream, Path.GetFileName(filename));
            Console.WriteLine("Processed " + filename);
          }
        }
      }
      
      // Write the XML Document
      XmlTextWriter writer = new XmlTextWriter(Application.StartupPath + "\\taglist.xml", Encoding.ASCII);
      writer.Formatting = Formatting.Indented;
      writer.WriteStartDocument();

      writer.WriteStartElement("xml");
      foreach (DictionaryEntry di in map.Tags)
      {
        TagInfo i = (TagInfo)di.Value;
        writer.WriteStartElement("tag");
        writer.WriteAttributeString("filename", i.path);
        
        foreach (DictionaryEntry entry2 in i.maps)
        {
          writer.WriteElementString("map", (string)entry2.Value);          
        }
        writer.WriteEndElement();
      }
      writer.WriteEndElement();
      writer.WriteEndDocument();
      writer.Close();

      MessageBox.Show("Done.");
    }

    private void btnBuildTagListTreeview_Click(object sender, EventArgs e)
    {
      OpenFileDialog ofd = new OpenFileDialog();
      if (ofd.ShowDialog() == DialogResult.Cancel) return;
      string filename = ofd.FileName;

      XmlDocument doc = new XmlDocument();
      doc.Load(filename);
      FileInfo fi = new FileInfo(filename);
      MessageBox.Show("Loaded " + filename + "\r\n " + fi.Length.ToString() + " bytes.");
    }

    private void vScrollBar1_ValueChanged(object sender, EventArgs e)
    {
      //Console.WriteLine(vScrollBar1.Value);
    }

    private void btnCreateNewProjectFile_Click(object sender, EventArgs e)
    {
      SaveFileDialog sfd = new SaveFileDialog();
      sfd.Filter = "Prometheus Project File (*.pproj)|*.pproj";
      if (sfd.ShowDialog() == DialogResult.Cancel) return;
      string filename = sfd.FileName;

      ProjectFile proj = new ProjectFile("Bloodgulch 2", "MonoxideC", ProjectTemplates.GetTemplate("HaloPCMultiplayer"));
      proj.Description = "This is the new 1337 shit baby.";
      proj.UiScreenShotFile = "bitchinshot.png";
      proj.Tags.Add(new ProjectTag("Scenario", @"levels\test\bloodgulch2\bloodgulch2.scenario"));
      proj.Tags.Add(new ProjectTag("Globals", @"globals\globals.globals"));
      string xml = proj.SaveToXML();
      StreamWriter sw = new StreamWriter(filename);
      sw.Write(xml);
      sw.Close();
      MessageBox.Show("Saved project file to: " + filename);
    }

    private void btnLoadProjectFile_Click(object sender, EventArgs e)
    {
      OpenFileDialog ofd = new OpenFileDialog();
      ofd.Filter = "Prometheus Project File (*.pproj)|*.pproj";
      if (ofd.ShowDialog() == DialogResult.Cancel) return;
      string filename = ofd.FileName;

      XmlDocument doc = new XmlDocument();
      doc.Load(filename);
      ProjectFile proj = new ProjectFile(doc);
      MessageBox.Show("Project Loaded: " + proj.MapName);
    }

    private void buttonTestEditorDataBinder_Click(object sender, EventArgs e)
    {
    }

    private void btnTestTagEditorGUI_Click(object sender, EventArgs e)
    {
      try
      {
        OpenFileDialog ofd = new OpenFileDialog();

        // Load the XML Tag Definition
        //ofd.Filter = "Antenna Tag Definition|antenna.xml";
				ofd.Filter = "XML Tag Definition|*.xml";
        if (ofd.ShowDialog() == DialogResult.Cancel) return;
        string tdfFilename = ofd.FileName;
        XmlDocument tagDefinition = new XmlDocument();
        tagDefinition.Load(tdfFilename);

        // Load the tag data
        string tagDataFilename = @"vehicles\warthog\warthog antenna.antenna";
        TagFileName testTagFileName = new TagFileName(
          tagDataFilename, MapfileVersion.HALOPC, TagSource.Archive);
        TagBase testTag = new TagBase();
        testTag.LoadTagBuffer(testTagFileName);
        BinaryReader br = new BinaryReader(testTag.Stream);
        Antenna antenna = new Antenna();
        antenna.Read(br);
        antenna.ReadChildData(br);

        Form test = new Form();
        test.Size = new Size(640, 480);
        TagEditorControl tagEditor = new TagEditorControl();
        tagEditor.Dock = DockStyle.Top;

        tagEditor.Create(tagDefinition, antenna);
      
        test.Controls.Add(tagEditor);
        test.ShowDialog();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
      }
    }

		private void btnTestNotes_Click(object sender, System.EventArgs e)
		{
			new NotesDialogGUI(new NotesDialog()).Show();
		}

		private void sizeChange_EditValueChanged(object sender, System.EventArgs e)
		{
			angleEdit1.Width = Convert.ToInt32(sizeChange.Value);
			angleEdit1.Height = angleEdit1.Width;
			angleEdit1.Refresh();
		}

    private void Tester_Load(object sender, System.EventArgs e)
    {
    
    }

    private void button2_Click(object sender, System.EventArgs e)
    {
      // Need to open a PTA file and do an assload of random seeks to benchmark.
      OpenFileDialog ofd = new OpenFileDialog();
      ofd.Filter = "All Files (*.*)|*.*";
      if (ofd.ShowDialog() == DialogResult.Cancel) return;
      string filename = ofd.FileName;

      BinaryReader reader;
      
      int choice = 0;

      if (choice == 1)
      {
        FileStream fs = new FileStream(filename, FileMode.Open);
        reader = new BinaryReader(fs);
      }
      else
      {
        FileStream fs = new FileStream(filename, FileMode.Open);
        byte[] data = new byte[fs.Length];
        fs.Read(data, 0, (int)fs.Length);
        MemoryStream ms = new MemoryStream(data);
        reader = new BinaryReader(ms);
      }
      int seekCount = 100000;
      Random r = new Random(DateTime.Now.Millisecond);
      int fileSize = (int)reader.BaseStream.Length;
      byte[] bin = new byte[100];

      DateTime startTime = DateTime.Now;

      for (int x=0; x<seekCount; x++)
      {
        int pos = r.Next(fileSize-101);
        reader.BaseStream.Position = pos;
        bin = reader.ReadBytes(100);
      }

      DateTime endTime = DateTime.Now;
      TimeSpan length = endTime.Subtract(startTime);
      Console.WriteLine(String.Format("Took {0} seconds to do {1} seek/reads.", length.TotalSeconds, seekCount));
      Console.WriteLine(String.Format("{0} seconds per read.", length.TotalSeconds / seekCount));
    }

    private void button1_Click(object sender, System.EventArgs e)
    {
      MasterLibraryArchive ma = new MasterLibraryArchive(TagLibraryManager.HaloPCMasterTagList, TagLibraryManager.HaloPC);
      MessageBox.Show("Archive was loaded.");
      DateTime startTime = DateTime.Now;
      string[] files = ma.GetRecursiveFileList("\\");
      DateTime endTime = DateTime.Now;
      TimeSpan length = endTime.Subtract(startTime);
      MessageBox.Show(String.Format("{0} files were returned from the archive in {1} seconds.", files.Length, length.TotalSeconds));
    }
  }
}
