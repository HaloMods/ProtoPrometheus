using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraBars;
using Prometheus.Core;

namespace Prometheus.Controls
{
  /// <summary>
  /// Defines multiple styles and behaviors for a Node, and
  /// automatically configures a given node accordingly.
  /// </summary>
  public abstract class NodeHelper
  {
    private string name = String.Empty;
    private NodeDefinitionCollection nodeDefinitions = new NodeDefinitionCollection();
    private MultiSyncTreeView parentTreeView = null;
    private ITagLibrary library;

    public Color ForeColor = Color.Empty;
    private MultiSyncTreeNode optionalRootNode = null;

    public FontStyle FontStyle = FontStyle.Regular;
    protected NodeCollection nodes = new NodeCollection();

    /// <summary>
    /// The name of the NodeHelper.
    /// </summary>
    public string Name
    {
      get { return name; }
      set { name = value; }
    }

    /// <summary>
    /// A collection of the NodeDefinitions associated with this NodeHelper.
    /// </summary>
    public NodeDefinitionCollection NodeDefinitions
    {
      get { return nodeDefinitions; }
    }

    // The TreeView control that contains this NodeHelper's owner TreeNode.
    public MultiSyncTreeView ParentTreeView
    {
      get { return parentTreeView; }
      set { parentTreeView = value; }
    }

    /// <summary>
    /// The ITagLibrary that is associated with this NodeHelper.
    /// </summary>
    public ITagLibrary Library
    {
      get { return library; }
      set { library = value; }
    }

    /// <summary>
    /// Optionally specifies a parent Node that this node helper will use as its root.
    /// </summary>
    public MultiSyncTreeNode OptionalRootNode
    {
      get { return optionalRootNode; }
      set { optionalRootNode = value; }
    }

    /// <summary>
    /// Configures the specified MultiSyncTreeNode as a Container node when overriden in a derived class.
    /// </summary>
    public abstract void ConfigureContainerNode(MultiSyncTreeNode node, string path);

    /// <summary>
    /// Configures the specified MultiSyncTreeNode as an Object node when overriden in a derived class.
    /// </summary>
    public abstract void ConfigureObjectNode(MultiSyncTreeNode node, string path);

    /// <summary>
    /// Performs the standard node configuration.
    /// </summary>
    /// <param name="node"></param>
    protected virtual void ConfigureStandardNode(MultiSyncTreeNode node)
    {
      if ((node.TreeView.Font.Style & FontStyle) == 0)
      {
        Font f = new Font(node.TreeView.Font, node.TreeView.Font.Style | FontStyle);
        node.NodeFont = f;
      }
      if (ForeColor != Color.Empty)
        if (node.ForeColor != this.ForeColor) node.ForeColor = this.ForeColor;
    }

    /// <summary>
    /// Generates a standardized Container node name from the given path.
    /// </summary>
    public virtual string GetContainerNodeNameFromPath(string path)
    {
      if (!path.StartsWith("\\"))
      {
        path = "\\" + path;
      }
      string trimmedFolder = path.TrimEnd('\\');
      int slashIndex = trimmedFolder.LastIndexOf('\\');
      string folderName = trimmedFolder;
      if (slashIndex > -1)
      {
        folderName = trimmedFolder.Substring(slashIndex);
      }
      string nodeTitle = folderName.Replace("\\", "");
      return nodeTitle;
    }

    /// <summary>
    /// Generates a standardized Object node name from the given path.
    /// </summary>
    public virtual string GetObjectNodeNameFromPath(string path)
    {
      return Path.GetFileName(path);
    }
  }

  /// <summary>
  /// A strongly-typed collection of NodeHelper objects.
  /// </summary>
  public class NodeHelperCollection : CollectionBase
  {
    /// <summary>
    /// Raised when a NodeHelper is added to the collection.
    /// </summary>
    public event NodeHelperAddedHandler NodeHelperAdded;

    public void Add(NodeHelper value)
    {
      InnerList.Add(value);
      if (NodeHelperAdded != null)
        NodeHelperAdded(this, new NodeHelperAddedEventArgs(value));
    }
    public NodeHelper this[int index]
    {
      get { return InnerList[index] as NodeHelper; }
    }
    public int IndexOf(NodeHelper value)
    {
      return InnerList.IndexOf(value);
    }
  }
  
  /// <summary>
  /// Represents the method that will handle the NodeHelperAdded event.
  /// </summary>
  public delegate void NodeHelperAddedHandler(object sender, NodeHelperAddedEventArgs e);
  
  /// <summary>
  /// Provides data for the NodeHelperAdded event.
  /// </summary>
  public class NodeHelperAddedEventArgs : EventArgs
  {
    private NodeHelper nodeHelper;
    public NodeHelper NodeHelper
    {
      get { return nodeHelper; }
    }
    public NodeHelperAddedEventArgs(NodeHelper nodeHelper)
    {
      this.nodeHelper = nodeHelper;
    }
  }

  /// <summary>
  /// Defines a preset look and behavior for a Node.
  /// </summary>
  public class NodeDefinition
  {
    private string name;
    private Bitmap collapsedIcon;
    private Bitmap expandedIcon;
    private PopupMenu popupMenu;

    /// <summary>
    /// The name used to identify the NodeDefinition.
    /// </summary>
    public string Name
    {
      get { return name; }
    }
    
    /// <summary>
    /// The icon that the node will display when this definition is
    /// active and the node is expanded.
    /// </summary>
    public Bitmap ExpandedIcon
    {
      get { return expandedIcon; }
      set { expandedIcon = value; }
    }
    
    /// <summary>
    /// The icon that the node will display when this definition is
    /// active and the node is collapsed.
    /// </summary>
    public Bitmap CollapsedIcon
    {
      get { return collapsedIcon; }
      set { collapsedIcon = value; }
    }
    
    /// <summary>
    /// The Popupmenu that the node will display when this definition is active.
    /// </summary>
    public PopupMenu PopupMenu
    {
      get  { return popupMenu;}
      set  { popupMenu = value;}
    }

    #region Constructors
    public NodeDefinition(string name) : this(name, null, null, null) { ; }
    public NodeDefinition(string name, Bitmap icon) : this(name, icon, icon, null) { ; }
    public NodeDefinition(string name, PopupMenu popupMenu) : this(name, null, null, popupMenu) { ; }
    public NodeDefinition(string name, Bitmap expandedIcon, Bitmap collapsedIcon) : this(name, expandedIcon, collapsedIcon, null) { ; }
    public NodeDefinition(string name, Bitmap expandedIcon, Bitmap collapsedIcon, PopupMenu popupMenu)
    {
      this.name = name;
      this.collapsedIcon = collapsedIcon;
      this.expandedIcon = expandedIcon;
      this.popupMenu = popupMenu;
    }
    #endregion
  }

  /// <summary>
  /// A strongly typed collection of NodeDefinition objects.
  /// </summary>
  public class NodeDefinitionCollection : CollectionBase
  {
    /// <summary>
    /// Add the NodeDefinition to the collection.
    /// If a NodeDefinition already exists with the specified name, it will
    /// be removed before the new NodeDefinition is added.
    /// </summary>
    /// <param name="definition"></param>
    public void Add(NodeDefinition definition)
    {
      NodeDefinition existingDefinition = this[definition.Name];
      if (existingDefinition != null)
      {
        InnerList.Remove(existingDefinition);
      }
      InnerList.Add(definition);
    }
    public NodeDefinition this[string name]
    {
      get
      {
        foreach (NodeDefinition definition in InnerList)
        {
          if (definition.Name == name) return definition;
        }
        return null;
      }
    }
    public NodeDefinition this[int index]
    {
      get { return InnerList[index] as NodeDefinition; }
    }
    
    public int IndexOf(NodeDefinition definition)
    {
      return InnerList.IndexOf(definition);
    }

    public int IndexOf(string name)
    {
      NodeDefinition definition = this[name];
      if (definition == null) return -1;
      return IndexOf(definition);
    }
  }
}