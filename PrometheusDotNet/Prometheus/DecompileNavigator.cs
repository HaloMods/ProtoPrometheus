/* ---------------------------------------------------------------
 * Prometheus
 * Bungie Map-based Multi-Game Editing Kit
 * 2004-2005, Halo-Dev
 * ---------------------------------------------------------------
 * Class       : Prometheus.DecompileNavigator
 * Description : Displays a file tree, allows selection of files,
 *             : and returns a list of selected files.
 * Author      : MonoxideC
 * Co-Authors  : 
 * ---------------------------------------------------------------
 */

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Prometheus.Controls;
using Prometheus.Core;
using Prometheus.Core.Tags;
using Utility = Prometheus.Core.Utility;
using Prometheus.Core.Compiler;

namespace Prometheus
{
	/// <summary>
	/// Summary description for DecompileNavigator.
	/// </summary>
	public class DecompileNavigator : Form
	{
    private FileTreeView TagTree;
    private Button ExtractAllTagsBtn;
    private Button ExtractSelectedTagsBtn;
    private CheckBox IncludeDependenciesChk;
    private FileListView TagList;
    private TextBox SelectedTagName;
		private ImageList imageList;
		private ContextMenu contextFolder;
		private ContextMenu contextFile;
		private MenuItem mnu_extractFolder;
		private MenuItem mnu_extractFile;
		private IContainer components;

		protected ProgressDialog pd;
    protected string NoTagSelected = "No tags are selected.";
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Splitter splitter1;
	  protected Prometheus.Core.Compiler.Decompiler m_decompiler;	
    
    public DecompileNavigator(string filename)
		{
      uint map_version;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

      // Setup the ImageList controls.
      TagList.CreateImageList(
        new string[1] {
          "Prometheus.Icons.App_Basics._32.document.png"
        });
			
      TagTree.CreateImageList(
        new string[4] {
          "Prometheus.Icons.Data_Coll._16.data.png",
          "Prometheus.Icons.App_Basics._16.folder.png",
          "Prometheus.Icons.App_Basics._16.folder_closed.png",
          "Prometheus.Icons.App_Basics._16.document.png"
        });

      TagTree.FileImageIndex = 3;
      TagTree.ShowFiles = FileTreeView.ShowFilesBehavior.ChildControl;
      TagTree.ChildControl = TagList;

			// Initalize the treeview.
      TagList.View = View.LargeIcon;
			TagTree.Nodes.Clear();
			TagTree.Nodes.Add(new TreeNode("Tags", 0, 0));
			TagTree.SelectedNode = TagTree.Nodes[0];
			TagTree.Nodes[0].Expand();

      // Initialize the decompiler.
      m_decompiler = new Decompiler();
      map_version = m_decompiler.InitDecompiler(filename);

      switch(map_version)
      {
        case 0x08:
          m_decompiler.SetOutputArchive(TagLibraryManager.Halo2Xbox);
          break;
        case 0x07:
        case 0x261:
          m_decompiler.SetOutputArchive(TagLibraryManager.HaloPC);
          break;
        case 0x05:
          m_decompiler.SetOutputArchive(TagLibraryManager.HaloXbox);
          break;
      }

      m_decompiler.ExtractionComplete += new Decompiler.ExtractionCompleteEventHandler(m_decompiler_ExtractionComplete);
      m_decompiler.ExtractedFile += new Decompiler.ExtractedFileEventHandler(m_decompiler_ExtractedFile);

      string[] tagList = m_decompiler.GetStringList();
      //Array.Sort(tagList);  // Sort in alphabetical order
      TagTree.LoadFiles(tagList);
		}

	  private void m_decompiler_ExtractedFile(object sender, Decompiler.ExtractedFileEventArgs e)
	  {
	    if (e.FilesExtracted >= e.TotalFiles-1)
	    {
	      pd.HelpText = "Rebuilding archive...";
	    }
      pd.UpdateProgress(e.FilesExtracted, e.TotalFiles);
	  }

	  private void m_decompiler_ExtractionComplete(object sender,
      Decompiler.ExtractionCompleteEventArgs e)
	  {
      pd.Close();
      MessageBox.Show(e.TotalFiles.ToString() + " file(s) were extracted.","Extract Files",MessageBoxButtons.OK, MessageBoxIcon.Information);
			//m_decompiler.m_OutputArchive.ZEndUpdate();
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
      this.components = new System.ComponentModel.Container();
      this.TagTree = new Prometheus.Controls.FileTreeView();
      this.contextFolder = new System.Windows.Forms.ContextMenu();
      this.mnu_extractFolder = new System.Windows.Forms.MenuItem();
      this.ExtractAllTagsBtn = new System.Windows.Forms.Button();
      this.ExtractSelectedTagsBtn = new System.Windows.Forms.Button();
      this.IncludeDependenciesChk = new System.Windows.Forms.CheckBox();
      this.TagList = new Prometheus.Controls.FileListView();
      this.contextFile = new System.Windows.Forms.ContextMenu();
      this.mnu_extractFile = new System.Windows.Forms.MenuItem();
      this.SelectedTagName = new System.Windows.Forms.TextBox();
      this.imageList = new System.Windows.Forms.ImageList(this.components);
      this.panel1 = new System.Windows.Forms.Panel();
      this.panel2 = new System.Windows.Forms.Panel();
      this.splitter1 = new System.Windows.Forms.Splitter();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // TagTree
      // 
      this.TagTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.TagTree.ContextMenu = this.contextFolder;
      this.TagTree.ImageIndex = -1;
      this.TagTree.Location = new System.Drawing.Point(10, 9);
      this.TagTree.Name = "TagTree";
      this.TagTree.SelectedImageIndex = -1;
      this.TagTree.ShowFiles = Prometheus.Controls.FileTreeView.ShowFilesBehavior.Self;
      this.TagTree.Size = new System.Drawing.Size(240, 333);
      this.TagTree.TabIndex = 0;
      // 
      // contextFolder
      // 
      this.contextFolder.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                  this.mnu_extractFolder});
      // 
      // mnu_extractFolder
      // 
      this.mnu_extractFolder.Index = 0;
      this.mnu_extractFolder.Text = "Extract All Files in Selected Folder...";
      this.mnu_extractFolder.Click += new System.EventHandler(this.mnu_extractFolder_Click);
      // 
      // ExtractAllTagsBtn
      // 
      this.ExtractAllTagsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.ExtractAllTagsBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.ExtractAllTagsBtn.Location = new System.Drawing.Point(10, 357);
      this.ExtractAllTagsBtn.Name = "ExtractAllTagsBtn";
      this.ExtractAllTagsBtn.Size = new System.Drawing.Size(240, 27);
      this.ExtractAllTagsBtn.TabIndex = 1;
      this.ExtractAllTagsBtn.Text = "Add All Files in Selected Folder";
      this.ExtractAllTagsBtn.Click += new System.EventHandler(this.ExtractAllTagsBtn_Click);
      // 
      // ExtractSelectedTagsBtn
      // 
      this.ExtractSelectedTagsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.ExtractSelectedTagsBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.ExtractSelectedTagsBtn.Location = new System.Drawing.Point(10, 357);
      this.ExtractSelectedTagsBtn.Name = "ExtractSelectedTagsBtn";
      this.ExtractSelectedTagsBtn.Size = new System.Drawing.Size(163, 27);
      this.ExtractSelectedTagsBtn.TabIndex = 2;
      this.ExtractSelectedTagsBtn.Text = "Add Selected Files";
      this.ExtractSelectedTagsBtn.Click += new System.EventHandler(this.ExtractSelectedTagsBtn_Click);
      // 
      // IncludeDependenciesChk
      // 
      this.IncludeDependenciesChk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.IncludeDependenciesChk.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.IncludeDependenciesChk.Location = new System.Drawing.Point(298, 357);
      this.IncludeDependenciesChk.Name = "IncludeDependenciesChk";
      this.IncludeDependenciesChk.Size = new System.Drawing.Size(172, 27);
      this.IncludeDependenciesChk.TabIndex = 3;
      this.IncludeDependenciesChk.Text = "Including Dependencies";
      // 
      // TagList
      // 
      this.TagList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.TagList.ContextMenu = this.contextFile;
      this.TagList.Location = new System.Drawing.Point(10, 37);
      this.TagList.Name = "TagList";
      this.TagList.Size = new System.Drawing.Size(460, 310);
      this.TagList.TabIndex = 4;
      this.TagList.SelectedIndexChanged += new System.EventHandler(this.TagList_SelectedIndexChanged);
      // 
      // contextFile
      // 
      this.contextFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                this.mnu_extractFile});
      // 
      // mnu_extractFile
      // 
      this.mnu_extractFile.Index = 0;
      this.mnu_extractFile.Text = "Extract Selected File(s)...";
      this.mnu_extractFile.Click += new System.EventHandler(this.mnu_extractFile_Click);
      // 
      // SelectedTagName
      // 
      this.SelectedTagName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.SelectedTagName.Location = new System.Drawing.Point(10, 9);
      this.SelectedTagName.Name = "SelectedTagName";
      this.SelectedTagName.ReadOnly = true;
      this.SelectedTagName.Size = new System.Drawing.Size(460, 22);
      this.SelectedTagName.TabIndex = 5;
      this.SelectedTagName.Text = "";
      // 
      // imageList
      // 
      this.imageList.ImageSize = new System.Drawing.Size(16, 16);
      this.imageList.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.TagTree);
      this.panel1.Controls.Add(this.ExtractAllTagsBtn);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(259, 393);
      this.panel1.TabIndex = 6;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.TagList);
      this.panel2.Controls.Add(this.SelectedTagName);
      this.panel2.Controls.Add(this.ExtractSelectedTagsBtn);
      this.panel2.Controls.Add(this.IncludeDependenciesChk);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(259, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(480, 393);
      this.panel2.TabIndex = 7;
      // 
      // splitter1
      // 
      this.splitter1.BackColor = System.Drawing.Color.Silver;
      this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.splitter1.Location = new System.Drawing.Point(259, 0);
      this.splitter1.Name = "splitter1";
      this.splitter1.Size = new System.Drawing.Size(4, 393);
      this.splitter1.TabIndex = 8;
      this.splitter1.TabStop = false;
      // 
      // DecompileNavigator
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
      this.ClientSize = new System.Drawing.Size(739, 393);
      this.Controls.Add(this.splitter1);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.panel1);
      this.MinimumSize = new System.Drawing.Size(535, 375);
      this.Name = "DecompileNavigator";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Add Tags to Library";
      this.panel1.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion
    
		private void TagList_SelectedIndexChanged(object sender, EventArgs e)
		{
			
      if (TagList.SelectedItems.Count < 1)
			{
				SelectedTagName.Text = NoTagSelected;
				return;
			}
			if (TagList.SelectedItems[0] == null)
			{
				SelectedTagName.Text = NoTagSelected;
			}
			else
			{
				if (TagList.SelectedItems.Count == 1)
				{
					SelectedTagName.Text = (string)TagList.SelectedItems[0].Tag;
				}
				else
				{
					SelectedTagName.Text = "(Multiple tags selected)";
				}
			}
		}

		private void mnu_extractFolder_Click(object sender, EventArgs e)
		{
			ExtractFolder();
		}

		private void mnu_extractFile_Click(object sender, EventArgs e)
		{
			ExtractFiles();
		}

		private void ExtractFolder()
		{
			if (TagTree.SelectedNode != null)
			{
				// Setup the progress dialog
        using( new EnableThemingInScope(true) ) 
        {
          pd = new ProgressDialog();
          pd.CreateControl();
        }
        pd.Text = "Prometheus";
        pd.HelpText = "Extracting tags...";
				//m_decompiler.m_OutputArchive.ZBeginUpdate();
        string[] results = TagTree.GetFilesUnderNode(TagTree.SelectedNode);
        if (!m_decompiler.ExtractTags(results))
        {
          MessageBox.Show("Extraction is already in progress!",
            "Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
        }
        else
        {
          pd.ShowDialog();
        }
				
  		}
		}

		private void ExtractFiles()
		{
			if (TagList.SelectedItems.Count < 1) return;
			string s = "";
			foreach (ListViewItem li in TagList.SelectedItems)
				s += (string)li.Tag + "\r\n";
      MessageBox.Show(s, "Extract Files",MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void ExtractSelectedTagsBtn_Click(object sender, EventArgs e)
		{
			ExtractFiles();
		}

		private void ExtractAllTagsBtn_Click(object sender, EventArgs e)
		{
			ExtractFolder();
		}
  }
}
