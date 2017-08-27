using System;
using System.ComponentModel;
using System.Drawing;
using DevExpress.XtraEditors;
using Prometheus.Controls;
using Prometheus.Core;
using Prometheus.Core.Tags;

namespace Prometheus.Testing
{
	/// <summary>
	/// Summary description for MonoxideC.
	/// </summary>
	public class MonoxideC : XtraForm
	{
    private MultiSyncTreeView treeView1;
    /// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public MonoxideC()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
      this.Load += new EventHandler(MonoxideC_Load);
    }

	  private void MonoxideC_Load(object sender, EventArgs e)
	  {
	    //MasterLibraryArchive ma = new MasterLibraryArchive(TagLibraryManager.HaloPCMasterTagList);
      MasterLibraryArchive ma = null;
      ArchiveLibraryNodeHelper master = new ArchiveLibraryNodeHelper("Master", ma);
      master.FontStyle = FontStyle.Italic;
      master.ForeColor = Color.Blue;
      treeView1.NodeHelpers.Add(master);

      DiskFolderTagLibrary pr = new DiskFolderTagLibrary(
        @"F:\Documents and Settings\Justin\My Documents\Visual Studio Projects\Prometheus\Source\PrometheusDotNet\Prometheus\bin\Debug\Games\PC\Halo\Projects\That New Map\Tags",
        "Project Folder");
      DiskLibraryNodeHelper project = new DiskLibraryNodeHelper("Project", pr);
      project.ForeColor = Color.LightGray;
      treeView1.NodeHelpers.Add(project);

      ArchiveLibraryNodeHelper archive = new ArchiveLibraryNodeHelper("Halo1", TagLibraryManager.HaloPC);
      archive.ForeColor = Color.Black;
      treeView1.NodeHelpers.Add(archive);

      ArchiveLibraryNodeHelper halo2 = new ArchiveLibraryNodeHelper("Halo2", TagLibraryManager.Halo2Xbox);
      halo2.ForeColor = Color.Red;
      treeView1.NodeHelpers.Add(halo2);

	    TagFileName tfn = new TagFileName(@"levels\test\bloodgulch\bloodgulch.scenario", MapfileVersion.HALOPC, TagSource.Archive);
      DependencyTagLibrary lib = new DependencyTagLibrary(tfn);      
      ArchiveLibraryNodeHelper dependencies = new ArchiveLibraryNodeHelper("Dependencies", lib);
      dependencies.ForeColor = Color.Green;
      treeView1.NodeHelpers.Add(dependencies);
      
      treeView1.Initialize();
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
      this.treeView1 = new Prometheus.Controls.MultiSyncTreeView();
      this.SuspendLayout();
      // 
      // treeView1
      // 
      this.treeView1.Location = new System.Drawing.Point(16, 16);
      this.treeView1.Name = "treeView1";
      this.treeView1.ShowFiles = true;
      this.treeView1.Size = new System.Drawing.Size(336, 224);
      this.treeView1.TabIndex = 0;
      // 
      // MonoxideC
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
      this.ClientSize = new System.Drawing.Size(376, 262);
      this.Controls.Add(this.treeView1);
      this.Name = "MonoxideC";
      this.Text = "MonoxideC";
      this.ResumeLayout(false);

    }
		#endregion
	}
}

