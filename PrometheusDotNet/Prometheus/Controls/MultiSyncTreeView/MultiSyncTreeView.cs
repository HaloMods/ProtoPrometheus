/* ---------------------------------------------------------------
 * Prometheus
 * Bungie Map-based Multi-Game Editing Kit
 * 2004-2005, Halo-Dev
 * ---------------------------------------------------------------
 * File        : MultiSyncTreeView.cs
 * Author      : MonoxideC
 * Co-Authors  : 
 * ---------------------------------------------------------------
 */

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using SharedControls;

namespace Prometheus.Controls
{
  
  ///////////////////////////////////////////////////////////////////////////////////////
  /// Important design note:
  /// To add the ability to have multiple menus and icons that change depending on the
  /// state of the object/container that the node represents, I am going to implement
  /// a hashtable for these objects.
  /// ex: tree.Menus.Add("container", somePopupMenu);
  ///     tree.Icons.Add("container", someIcon);
  /// Valid standard keys will be: container and object.
  /// Other keys can be added on a per-NodeHelper basis to handle different states.
  /// For example, an object tag that is explicitly linked to the Project source could
  /// have a different icon and a different popup menu than something that uses the
  /// default path.
  ///////////////////////////////////////////////////////////////////////////////////////
  public class BitmapCollection : CollectionBase
  {
    public void Add(Bitmap bitmap)
    {
      InnerList.Add(bitmap);
    }
    public Bitmap this[int index]
    {
      get { return InnerList[index] as Bitmap; }
    }
    public int IndexOf(Bitmap bitmap)
    {
      return InnerList.IndexOf(bitmap);
    }
    public bool Contains(Bitmap bitmap)
    {
      return InnerList.Contains(bitmap);
    }
    public Bitmap[] ToArray()
    {
      return (InnerList.ToArray(typeof(Bitmap)) as Bitmap[]);
    }
  }

  /// <summary>
	/// An advanced treeview that allows multiple independent node heirarchies to
	/// function together inside a single treeview control with intelligent
	/// right click menu grouping and node style overriding.
	/// </summary>
	public class MultiSyncTreeView : BetterTreeView
	{
    private NodeHelperCollection nodeHelpers = new NodeHelperCollection();
    private Container components = null;
    private PopupMenu popupMenu = null;
    private bool showFiles = true;
    private string[] imageResourcePaths;

    public BitmapCollection Bitmaps = new BitmapCollection();

    public PopupMenu PopupMenu
    {
      get { return popupMenu; }
    }

    private void CreatePopupMenu(params PopupMenu[] menus)
    {
      if (menus.Length == 0)
      {
        this.popupMenu = null;
        return;
      }
      // Create a single menu from the links in all of the specified menus,
      // and set it as the active menu for the TreeView.
      PopupMenu newMenu = new PopupMenu(menus[0].Manager);
      foreach (PopupMenu menu in menus)
      {
        int currentLink = 1;
        foreach (BarItemLink link in menu.ItemLinks)
        {
          newMenu.ItemLinks.Add(link.Item);
          // Start a new group with this first item.
          if (currentLink == 1)
          {
            newMenu.ItemLinks[newMenu.ItemLinks.Count-1].BeginGroup = true;
          }
          currentLink++;
        }
      }
      this.popupMenu = newMenu;
    }

    public NodeHelperCollection NodeHelpers
    {
      get { return nodeHelpers; }
    }

    // TODO: Handle refreshing of the treeview when this changes.
    public bool ShowFiles
    {
      get { return showFiles; }
      set { showFiles = value; }
    }

    #region Constructor
    public MultiSyncTreeView()
		{
			if (!DesignMode)
      {
        imageResourcePaths = new string[1];
        imageResourcePaths[0] = "Prometheus.Icons.Data_Coll._16.data.png";

        this.BeforeExpand += new TreeViewCancelEventHandler(tagTree_BeforeExpand);
        this.AfterSelect += new TreeViewEventHandler(tagTree_AfterSelect);
        this.MouseUp += new MouseEventHandler(tagTree_MouseUp);
        this.SelectedImageIndex = -1;
        nodeHelpers.NodeHelperAdded += new NodeHelperAddedHandler(nodeHelpers_NodeHelperAdded);
      }
		}
    #endregion

    private void nodeHelpers_NodeHelperAdded(object sender, NodeHelperAddedEventArgs e)
    {
      if (e.NodeHelper == null) return;
      foreach (NodeDefinition definition in e.NodeHelper.NodeDefinitions)
      {
        if (!Bitmaps.Contains(definition.CollapsedIcon))
          Bitmaps.Add(definition.CollapsedIcon);
        if (!Bitmaps.Contains(definition.ExpandedIcon))
          Bitmaps.Add(definition.ExpandedIcon);
      }
    
      this.ImageList = Utility.GenerateImageList(Bitmaps.ToArray());
      e.NodeHelper.ParentTreeView = this;
    }

    /// <summary>
    /// Searches the TreeView for the node containing the specified path.
    /// Child nodes are created as neccessary.
    /// </summary>
    protected MultiSyncTreeNode GetNodeFromPath(TreeNode rootNode, string path)
    {
      path = path.Trim('\\');
      string[] parts = path.Split('\\');
      
      foreach (MultiSyncTreeNode node in rootNode.Nodes)
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
              CreateGrandChildren(node.Parent as MultiSyncTreeNode);
            }
            
            return GetNodeFromPath(node, path.Substring(path.IndexOf('\\')+1));
          }
          return node;
        }
      }
      return null;
    }

    /// <summary>
    /// Scrolls to the node that corresponds to the specified path.
    /// </summary>
    public void ScrollToNode(string path)
    {
      if (path == null) return;

      TreeNode node = null;
      if (path == "")
      {
        node = Nodes[0];
      }
      else
      {
        node = GetNodeFromPath(Nodes[0], path) as TreeNode;
      }
     
      if (node != null)
      {
        SelectedNode = node;
        node.EnsureVisible();
      }
    }

    #region Event Handlers
	  private void tagTree_MouseUp(object sender, MouseEventArgs e)
	  {
      if(e.Button == MouseButtons.Right)
      {
        TreeNode node = this.GetNodeAt(e.X, e.Y);
        if (node != null)
        {
          if (this.SelectedNode != node)
          {
            this.SelectedNode = node;
          }
        }
        if (popupMenu != null) popupMenu.ShowPopup(Control.MousePosition);
      }
	  }

	  private void tagTree_AfterSelect(object sender, TreeViewEventArgs e)
	  {
      MultiSyncTreeNode node = e.Node as MultiSyncTreeNode;
	    ArrayList menus = new ArrayList();
      foreach (PathItem item in node.NodeInformation.PathItems)
      {
        if (node.NodeInformation.NodeType == NodeType.Container)
        {
          if (item.NodeHelper.NodeDefinitions["container"].PopupMenu != null)
            menus.Add(item.NodeHelper.NodeDefinitions["container"].PopupMenu);
        }
        else
        {
          if (item.NodeHelper.NodeDefinitions["object"].PopupMenu != null)
            menus.Add(item.NodeHelper.NodeDefinitions["object"].PopupMenu);
        }
      }
      CreatePopupMenu(menus.ToArray(typeof(PopupMenu)) as PopupMenu[]);

      // Raise the FolderSelected event so that any sub controls that need to
      // display files (listview for example) can do so.

      // TODO: Figure out the best way to go about doing this, since a single node can
      // contain multiple lists of children.

      //        FileEntryInformation entryInfo = (e.Node.Tag as FileEntryInformation) ;
      //        if (entryInfo != null)
      //        {
      //          if (entryInfo.FileType == FileEntryType.Folder)
      //          {
      //            string[] files = tagLibrary.GetFileList(entryInfo.FullPath);        
      //            if (ContainerSelected != null)
      //            {
      //              ContainerSelected(new FolderSelectedEventArgs(files, entryInfo.FullPath));
      //            }
      //          }
      //
      //          if (NodeSelected != null)
      //          {
      //            if (!IgnoreNodeSelectionEvents)
      //              NodeSelected(sender, e);
      //          }
      //        }
	  }
    	  
    private void tagTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
    {
      if (e.Node.Nodes.Count < 2)
      {
        if (e.Node.Nodes[0].Text == "dummy")
        {
          e.Node.Nodes.RemoveAt(0);
          try
          {
            Cursor.Current = Cursors.WaitCursor;
            BeginUpdate();
            foreach (PathItem item in (e.Node as MultiSyncTreeNode).NodeInformation.PathItems)
            {
              CreateChildren(e.Node as MultiSyncTreeNode, item);
            }
          }
          finally
          {
            EndUpdate();
            Cursor.Current = Cursors.Default;
          }
        }
      }
    }
    #endregion

	  public event FolderSelectedEventHandler ContainerSelected;
    
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


    public void Initialize()
    {
      InitializeTree();
    }

	  protected void InitializeTree()
    {
      Console.WriteLine("Initializing MultiSyncTreeView");
      BeginUpdate();
      Nodes.Clear();
      
      MultiSyncTreeNode rootNode = new MultiSyncTreeNode("Archive");
      rootNode.CollapsedImageIndex = 0;
      rootNode.ExpandedImageIndex = 0;
      rootNode.NodeInformation = new NodeInformation(NodeType.Container);
      
      foreach (NodeHelper helper in nodeHelpers)
      {
        // Need to determine where to put this NodeHelper's root PathItem.
        // If it has a definition for its own rootnode, create that node and place it under the
        // treeview's rootNode.  Otherwise, place this PathItem directly under the TreeView's rootNode.
        // TODO: Implement the above functionality.
        if (helper == null) continue;
        if (helper.OptionalRootNode != null)
        {
          rootNode.Nodes.Add(helper.OptionalRootNode);
          helper.OptionalRootNode.NodeInformation.PathItems.Add(new PathItem(helper, "\\"));
          if (helper.OptionalRootNode.Nodes.Count == 0)
            helper.OptionalRootNode.Nodes.Add(new TreeNode("dummy"));
        }
        else
        {
          rootNode.NodeInformation.PathItems.Add(new PathItem(helper, "\\"));
        }
      }
      Nodes.Add(rootNode);
      
      foreach (PathItem item in rootNode.NodeInformation.PathItems)
      {
        CreateChildren(rootNode, item);
      }
      //CreateGrandChildren(rootNode);

      Nodes[0].Expand();
      SelectedNode = Nodes[0];
      EndUpdate();
    }

    /// <summary>
    /// Creates the child nodes of the specified node's child nodes.
    /// Used for on-the-fly loading of tree nodes.
    /// </summary>
    protected void CreateGrandChildren(MultiSyncTreeNode parentNode)
    {
      // TODO: Look into a better method of handling on the fly loading.
      // In the CreateGrandChildrenMethod, rather than physically creating
      // all of the grandchild nodes, simply see if child nodes exist for
      // each child of the parent.  If so, create a dummy node (so that the
      // node is collapsable), and remove it before expansion.
      // TODO: On the above method, look for a way to make a node expandable
      // without it having to actually contain child nodes.
      
      foreach (MultiSyncTreeNode node in parentNode.Nodes)
      {
        if (node.NodeInformation.NodeType == NodeType.Container)
        {
          if (!node.NodeInformation.ChildrenCreated)
          {
            foreach (PathItem item in node.NodeInformation.PathItems)
            {
              CreateChildren(node, item);
            }
          }
        }
      }
    }

    /// <summary>
    /// Creates the direct children of the specified node.
    /// Children are constrained to those that exist in the specified PathItem.
    /// </summary>
    protected virtual void CreateChildren(MultiSyncTreeNode parentNode, PathItem pathItem)
    {
      string path = pathItem.Path;

      // Load folders first so they will be at the top.
      string[] folders = pathItem.NodeHelper.Library.GetFolderList(path);
      if (folders != null)
      {
        Array.Sort(folders);
        foreach (string folder in folders)
        {
          // If a node with this name and type does not already exist, create it.
          string nodeName = pathItem.NodeHelper.GetContainerNodeNameFromPath(folder);
          MultiSyncTreeNode newNode = GetOrCreateNode(parentNode, nodeName);
          if (!parentNode.Nodes.Contains(newNode)) parentNode.Nodes.Add(newNode);
          if (newNode.Nodes.Count == 0)
            newNode.Nodes.Add(new TreeNode("dummy"));

          pathItem.NodeHelper.ConfigureContainerNode(newNode, folder);
        }
      }

      if (this.showFiles)
      {
        string[] files = pathItem.NodeHelper.Library.GetFileList(path);
        if (files != null)
        {
          Array.Sort(files);
          foreach (string file in files)
          {
            // If a node with this name does not already exist, create it.
            string nodeName = pathItem.NodeHelper.GetObjectNodeNameFromPath(file);
            MultiSyncTreeNode newNode = GetOrCreateNode(parentNode, nodeName);
            if (!parentNode.Nodes.Contains(newNode)) parentNode.Nodes.Add(newNode);

            pathItem.NodeHelper.ConfigureObjectNode(newNode, file);
          }
        }
      }
      parentNode.NodeInformation.ChildrenCreated = true;
    }

    /// <summary>
    /// Returns the node with the specified text if it exists.
    /// Otherwise, creates a new node with the specified text.
    /// </summary>
    /// <param name="parentNode">The parent node whose children to search.</param>
    private MultiSyncTreeNode GetOrCreateNode(MultiSyncTreeNode parentNode, string text)
    {
      MultiSyncTreeNode newNode = null;
      
      foreach (MultiSyncTreeNode node in parentNode.Nodes)
      {
        if (node.Text == text)
        {
          newNode = node;
          break;
        }
      }
      if (newNode == null) newNode = new MultiSyncTreeNode(text);
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

    public void ShowPopup()
    {
      if (this.popupMenu != null)
      {
        if (this.popupMenu.ItemLinks.Count > 0)
        {
          this.PopupMenu.ShowPopup(Control.MousePosition);
        }
      }
    }
	}

  public class NodeInformation
  {
    private NodeType nodeType;
    private PathItemCollection pathItems = new PathItemCollection();
    private bool childrenCreated = false;

    public NodeType NodeType
    {
      get { return nodeType; }
    }

    public PathItemCollection PathItems
    {
      get { return pathItems; }
    }

    // TODO: Think of a better approach to determining if children have been created.
    // Perhaps add an event to a derived node class that fires when the children are created.
    public bool ChildrenCreated
    {
      get { return childrenCreated; }
      set { childrenCreated = value; }
    }

    public NodeInformation(NodeType nodeType)
    {
      this.nodeType = nodeType;
    }
  }

  public class PathItemCollection : CollectionBase
  {
    public void Add(PathItem item)
    {
      InnerList.Add(item);
    }
    public PathItem this[int index]
    {
      get { return InnerList[index] as PathItem; }
    }

    public PathItem this[string name]
    {
      get
      {
        foreach (PathItem item in InnerList)
        {
          if (item.NodeHelper.Name == name)
            return item;
        }
        return null;
      }
    }
  }

  public class PathItem
  {
    private NodeHelper nodeHelper;
    private string path;

    public NodeHelper NodeHelper
    {
      get { return nodeHelper; }
    }

    public string Path
    {
      get { return path; }
    }

    public PathItem(NodeHelper nodeHelper, string path)
    {
      this.nodeHelper = nodeHelper;
      this.path = path;
    }
  }

  public class NodeCollection : CollectionBase
  {
    public void Add(TreeNode node)
    {
      InnerList.Add(node);
    }
    public TreeNode this[int index]
    {
      get { return InnerList[index] as TreeNode; }
    }
    public bool Contains(TreeNode node)
    {
      return InnerList.Contains(node);
    }
  }

  public class FileEntryInformationCollection : CollectionBase
  {
    public void Add(FileEntryInformation entry)
    {
      InnerList.Add(entry);
    }
    public FileEntryInformation this[int index]
    {
      get { return InnerList[index] as FileEntryInformation; }
    }
  }

  public class MultiSyncTreeNode : BetterTreeView.BetterTreeNode
  {
    private NodeInformation nodeInformation = null;

    public NodeInformation NodeInformation
    {
      get { return nodeInformation; }
      set { nodeInformation = value; }
    }

    #region Base Class Constructors
    public MultiSyncTreeNode() : base() { ; }
    public MultiSyncTreeNode(string text) : base(text) { ; }
    public MultiSyncTreeNode(string text, TreeNode[] children) : base(text, children) { ; }
    public MultiSyncTreeNode(string text, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex) { ; }
    public MultiSyncTreeNode(string text, int imageIndex, int selectedImageIndex, TreeNode[] children) : base(text, imageIndex, selectedImageIndex, children) { ; }
    #endregion

    
  }
  
  public enum NodeType
  {
    Container,
    Object
  }
}