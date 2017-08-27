/* ---------------------------------------------------------------
 * Prometheus
 * Bungie Map-based Multi-Game Editing Kit
 * 2004-2005, Halo-Dev
 * ---------------------------------------------------------------
 * File        : TagArchiveTree.cs
 * Author      : MonoxideC
 * Co-Authors  : 
 * ---------------------------------------------------------------
 */

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Core;
using DevExpress.XtraEditors;
using Prometheus.Core;

namespace SharedControls
{
	/// <summary>
	/// Allows TreeView based browsing of the contents of an ITagArchive.
	/// </summary>
	public class TagArchiveTree : XtraUserControl
	{
    // TODO: Add a method to select a node by path.

    protected BetterTreeView tagTree;
		private Container components = null;

    protected ITagLibrary tagLibrary;
    private bool showFiles = true;

    private MasterLibraryArchive masterArchive;
    private DevExpress.XtraBars.PopupMenu popupMenu;
    private FileSource activeTagSource = FileSource.Both;

    public bool IgnoreNodeSelectionEvents = false;
    
    public TreeNodeCollection Nodes
    {
      get { return tagTree.Nodes; }
    }

    public bool HotTracking
    {
      get { return tagTree.HotTracking; }
      set { tagTree.HotTracking = value; }
    }

    /// <summary>
    /// Indicates if the tag from the tag library should be displayed.
    /// </summary>
    protected bool tagLibraryEnabled
    {
      get
      {
        if (tagLibrary == null) return false;

        switch (this.activeTagSource)
        {
          case FileSource.TagLibrary:
            return true;
          case FileSource.Both:
            return true;
          case FileSource.MasterArchive:
            return false;
          default:
            return false; // Just to be safe.
        }
      }
    }

    /// <summary>
    /// Indicates if the tag from the master library should be displayed.
    /// </summary>
    protected bool masterLibraryEnabled
    {
      get
      {
        if (masterArchive == null) return false;

        switch (this.activeTagSource)
        {
          case FileSource.TagLibrary:
            return false;
          case FileSource.Both:
            return true;
          case FileSource.MasterArchive:
            return true;
          default:
            return false; // Just to be safe.
        }
      }
    }

    public FileSource ActiveTagSource
    {
      get { return activeTagSource; }
      set { activeTagSource = value; }
    }

    public DevExpress.XtraBars.PopupMenu PopupMenu
    {
      get { return this.popupMenu; }
      set { this.popupMenu = value; }
    }

    public TreeNode SelectedNode
    {
      get { return tagTree.SelectedNode; }
    }

    public MasterLibraryArchive MasterArchive
    {
      set { this.masterArchive = value; }
      get { return this.masterArchive; }
    }

    public string RootNodeText
    {
      set
      {
        if (tagTree.Nodes.Count < 1)
          tagTree.Nodes.Add(new TreeNode());
        tagTree.Nodes[0].Text = value;
      }
    }

    public ITagLibrary TagLibrary
    {
      get { return tagLibrary; }
      set { tagLibrary = value; }
    }

    // TODO: Handle refreshing of the treeview when this changes.
    public bool ShowFiles
    {
      get { return showFiles; }
      set { showFiles = value; }
    }

    /// <summary>
    /// Workaround to indicate if the current control is in design view or now.
    /// This is used as an alternative to the native DesignView property, which
    /// reports true only when the control is top level (not inside a container).
    /// <note>This method will fail and return false if the user does not have
    /// administrator privileges.</note>
    /// </summary>
    public new bool DesignMode
    {
      get
      {
        try
        {
          return (Process.GetCurrentProcess().ProcessName == "devenv");
        }
        catch (Exception ex)
        {
          Console.WriteLine("Error getting current process - design mode will be assumed false.", ex);
          return false;
        }
      }
    }

    public event TreeViewEventHandler NodeSelected;
    public new event EventHandler DoubleClick;

		public TagArchiveTree()
		{
			InitializeComponent();

      if (!DesignMode)
      {
        tagTree.CreateImageList(
          "Prometheus.Icons.Data_Coll._16.data.png",
          "Prometheus.Icons.App_Basics._16.folder.png",
          "Prometheus.Icons.App_Basics._16.folder_closed.png",
          "Prometheus.Icons.App_Basics._16.document.png");

        tagTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(tagTree_BeforeExpand);
        tagTree.AfterSelect += new TreeViewEventHandler(tagTree_AfterSelect);
        tagTree.MouseUp += new MouseEventHandler(tagTree_MouseUp);
        tagTree.SelectedImageIndex = -1;
        tagTree.DoubleClick += new EventHandler(tagTree_DoubleClick);
      }
		}

    protected TreeNode LocateNodeByPath(TreeNode rootNode, string path)
    {
      path = path.Trim('\\');
      string[] parts = path.Split('\\');
      
      foreach (TreeNode node in rootNode.Nodes)
      {
        if (node.Text == parts[0])
        {
          if (parts.Length > 1)
          {
            // If cildren have not been created, create them.
            if (node.Tag == null) continue;
            if (!(node.Tag is FileEntryInformation)) continue;
            FileEntryInformation info = (node.Tag as FileEntryInformation);
            if (!info.ChildrenCreated)
            {
              CreateSubLevel(node.Parent);
            }
            
            return LocateNodeByPath(node, path.Substring(path.IndexOf('\\')+1));
          }
          return node;
        }
      }
      return null;
    }

    private bool defaultNodeBeingSelected = false;
    public void ScrollToNode(string path)
    {
      if (path == null) return;
      TreeNode node;
      if (path == "")
      {
        node = tagTree.Nodes[0];
      }
      else
      {
        node = LocateNodeByPath(tagTree.Nodes[0], path);
      }
      
      if (node != null)
      {
        defaultNodeBeingSelected = true;
        tagTree.SelectedNode = node;
        node.EnsureVisible();
        defaultNodeBeingSelected = false;
      }
    }

	  private void tagTree_DoubleClick(object sender, EventArgs e)
	  {
	    if (DoubleClick != null) DoubleClick(sender, e);
	  }

	  private void tagTree_MouseUp(object sender, MouseEventArgs e)
	  {
      if(e.Button == MouseButtons.Right)
      {
        if (popupMenu != null) popupMenu.ShowPopup(Control.MousePosition);
      }
	  }

    public void SetupMasterArchive(XmlDocument archive)
    {
      if (archive != null)
      {
        //masterArchive = new MasterLibraryArchive(archive);
      }
      else
      {
        masterArchive = null;
      }
    }

	  public event FolderSelectedEventHandler FolderSelected;
    
    public delegate void FolderSelectedEventHandler(FolderSelectedEventArgs e);
    
    public class FolderSelectedEventArgs : EventArgs
    {
      private string[] fileList;
      private string path;
      
      public string[] FileList
      {
        get { return fileList; }
      }

      public string Path
      {
        get { return path; }
      }

      public FolderSelectedEventArgs(string[] fileList, string path)
      {
        this.fileList = fileList;  
        this.path = path;
      }
    }

	  private void tagTree_AfterSelect(object sender, TreeViewEventArgs e)
	  {
	    //if (defaultNodeBeingSelected) return;
      // Raise the FolderSelected event so that any sub controls that need to
      // display files (listview for example) can do so.
      if (tagLibrary != null)
      {
        FileEntryInformation entryInfo = (e.Node.Tag as FileEntryInformation) ;
        if (entryInfo != null)
        {
          if (entryInfo.FileType == FileEntryType.Folder)
          {
            string[] files = tagLibrary.GetFileList(entryInfo.FullPath);        
            if (FolderSelected != null)
            {
              FolderSelected(new FolderSelectedEventArgs(files, entryInfo.FullPath));
            }
          }

          if (NodeSelected != null)
          {
            if (!IgnoreNodeSelectionEvents)
              NodeSelected(sender, e);
          }
        }
      }
	  }

    public void Initialize()
    {
      InitializeTree();
    }

	  protected void InitializeTree()
    {
      tagTree.BeginUpdate();
      tagTree.Nodes.Clear();
      
      // TODO: Move BetterTreeNode outside of BetterTreeView class.
      BetterTreeView.BetterTreeNode rootNode = new BetterTreeView.BetterTreeNode("Archive");
      rootNode.CollapsedImageIndex = 0;
      rootNode.ExpandedImageIndex = 0;
      FileEntryInformation fileInfo = new FileEntryInformation(FileEntryType.Folder, "\\");
      rootNode.Tag = fileInfo;
      tagTree.Nodes.Add(rootNode);
      
      AddFiles(rootNode, "\\");
      CreateSubLevel(rootNode);
      tagTree.Nodes[0].Expand();
      tagTree.SelectedNode = tagTree.Nodes[0];
      tagTree.EndUpdate();
    }

	  private void tagTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
	  {
      tagTree.BeginUpdate();
      CreateSubLevel(e.Node);
      tagTree.EndUpdate();
	  }

    protected void CreateSubLevel(TreeNode parentNode)
    {
      foreach (TreeNode node in parentNode.Nodes)
      {
        // If neccessary, create all of this node's child nodes.
        FileEntryInformation fileInfo = (node.Tag as FileEntryInformation);
        if (fileInfo.FileType == FileEntryType.Folder)
        {
          if (!fileInfo.ChildrenCreated)
          {
            AddFiles(node, fileInfo.FullPath);
            fileInfo.ChildrenCreated = true;
          }
        }
      }
    }

    protected virtual void AddFiles(TreeNode parentNode, string path)
    {
      // Take care of folders first so they will be at the top.
      if (this.tagLibraryEnabled)
      {
        string[] folders = tagLibrary.GetFolderList(path);
        if (folders != null)
        {
          Array.Sort(folders);
          foreach (string folder in folders)
          {
            parentNode.Nodes.Add(CreateFolderNode(folder));
          }
        }
      }
      if (this.masterLibraryEnabled)
      {
        string[] folders = masterArchive.GetFolderList(path);
        if (folders != null)
        {
          Array.Sort(folders);
          foreach (string folder in folders)
          {
            // Check is this folder exists in the tag library.
            bool createNode = true;
            if (this.tagLibraryEnabled)
              if (tagLibrary.FolderExists(path + folder))
                  createNode = false;
            
            if (createNode)
            {
              TreeNode node = CreateFolderNode(path + folder, true);
              node.NodeFont = new Font(tagTree.Font, FontStyle.Bold);
              parentNode.Nodes.Add(node);
            }
          }
        }
      }

      if (this.showFiles)
      {
        if (this.tagLibraryEnabled)
        {
          string[] files = tagLibrary.GetFileList(path);
          if (files != null)
          {
            Array.Sort(files);
            foreach (string file in files)
            {
              parentNode.Nodes.Add(CreateFileNode(file));
            }
          }
        }
        if (this.masterLibraryEnabled)
        {
          string[] files = masterArchive.GetFileList(path);
          if (files != null)
          {
            Array.Sort(files);
            foreach (string file in files)
            {
              string lowercaseFile = file.ToLower();
              
              // Check is this file exists in the tag library.
              bool createNode = true;
              if (this.tagLibraryEnabled)
                if (tagLibrary.FileExists(path + lowercaseFile))
                  createNode = false;
            
              if (createNode)
              {
                TreeNode node = CreateFileNode(path + lowercaseFile, true);
                node.NodeFont = new Font(tagTree.Font, FontStyle.Bold);
                parentNode.Nodes.Add(node);
              }
            }
          }
        }        
      }
    }  

    protected BetterTreeView.BetterTreeNode CreateFolderNode(string path)
    {
      return CreateFolderNode(path, null);
    }

    protected BetterTreeView.BetterTreeNode CreateFolderNode(string path, object tag)
    {
      // TODO: We need to standardize how these files are named.
      // Slashes are a bit unneccessary IMO, and we have to keep removing them
      // for parsing paths - which sucks.
      // HACK: For now, we need to add a leading and trailing backslash.
      if (!path.StartsWith("\\")) path = "\\" + path;
      if (!path.EndsWith("\\")) path = path + "\\";

      string trimmedFolder = path.TrimEnd('\\');
      int slashIndex = trimmedFolder.LastIndexOf('\\');
      string folderName = trimmedFolder;
      if (slashIndex > -1) folderName = trimmedFolder.Substring(slashIndex);

      string nodeTitle = folderName.Replace("\\", "");

      BetterTreeView.BetterTreeNode newNode = new BetterTreeView.BetterTreeNode(nodeTitle);
      newNode.CollapsedImageIndex = 2;
      newNode.ExpandedImageIndex = 1;
      newNode.SelectedImageIndex = newNode.CollapsedImageIndex;
      newNode.ImageIndex = newNode.CollapsedImageIndex;        

      FileEntryInformation fileInfo = new FileEntryInformation(FileEntryType.Folder, path);
      fileInfo.Tag = tag;
      newNode.Tag = fileInfo;
      
      return newNode;
    }

    protected BetterTreeView.BetterTreeNode CreateFileNode(string path)
    {
      return CreateFileNode(path, null);
    }

    protected BetterTreeView.BetterTreeNode CreateFileNode(string path, object tag)
    {
      string filename = Path.GetFileName(path);
      BetterTreeView.BetterTreeNode newNode = new BetterTreeView.BetterTreeNode(filename);
      newNode.CollapsedImageIndex = 3;
      newNode.ExpandedImageIndex = 3;
      newNode.SelectedImageIndex = newNode.CollapsedImageIndex;
      newNode.ImageIndex = newNode.CollapsedImageIndex;        

      FileEntryInformation fileInfo = new FileEntryInformation(FileEntryType.File, path);
      fileInfo.Tag = tag;
      newNode.Tag = fileInfo;
      
      return newNode;
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
      this.tagTree = new BetterTreeView();
      this.SuspendLayout();
      // 
      // tagTree
      // 
      this.tagTree.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tagTree.ImageIndex = -1;
      this.tagTree.Location = new System.Drawing.Point(0, 0);
      this.tagTree.Name = "tagTree";
      this.tagTree.SelectedImageIndex = -1;
      this.tagTree.Size = new System.Drawing.Size(224, 280);
      this.tagTree.TabIndex = 0;
      // 
      // TagArchiveTree
      // 
      this.Controls.Add(this.tagTree);
      this.Name = "TagArchiveTree";
      this.Size = new System.Drawing.Size(224, 280);
      this.ResumeLayout(false);

    }
		#endregion
	}

  public class FileEntryInformation
  {
    public FileEntryType FileType;
    public string FullPath = "";
    public bool ChildrenCreated = false;
    public object Tag;

    public FileEntryInformation(FileEntryType fileType, string fullPath)
    {
      this.FileType = fileType;
      this.FullPath = fullPath;
    }
  }
  public enum FileEntryType
  {
    File,
    Folder
  }
  public enum FileSource
  {
    TagLibrary,
    MasterArchive,
    Both
  }
}