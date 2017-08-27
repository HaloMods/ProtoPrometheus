/* ---------------------------------------------------------------
 * Prometheus
 * Bungie Map-based Multi-Game Editing Kit
 * 2004-2005, Halo-Dev
 * ---------------------------------------------------------------
 * Author      : MonoxideC
 * Co-Authors  : 
 * ---------------------------------------------------------------
 */

using System;
using System.Collections;
using System.Windows.Forms;
using Xceed.FileSystem;
using SharedControls;

namespace Prometheus.Controls
{
	/// <summary>
	/// Extends BetterTreeView to show folders and files in a heirarchial
	/// fashion, while also allowing files to be displayed in a child control.
	/// </summary>
	public class FileTreeView : BetterTreeView
	{

    // This is for the sole purpose of placeholding when caching data.
    public class DummyTreeNode : TreeNode
    {
      public DummyTreeNode() : base() { ; }
      public DummyTreeNode(string text) : base(text) { ; }
    }

    public class FolderInformation
    {
      public ArrayList Items;
      public string FullPath;
      public FolderInformation()
      {
        Items = new ArrayList();
      }
    }

    private ShowFilesBehavior m_showFiles;

    /// <summary>
    /// Gets or sets the value specifying if the control should show
    /// files as child nodes of folders, or if it should forward the
    /// file info to a child control for display.
    /// </summary>
    public ShowFilesBehavior ShowFiles
    {
      get { return m_showFiles; }
      set { m_showFiles = value; }
    }

    /// <summary>
    /// Sets the index in the control's ImageList that contains a file icon.
    /// </summary>
    public int FileImageIndex;

    public IShowFiles ChildControl;
    
    public enum ShowFilesBehavior
		{
		  Self,
      ChildControl
		}

    #region Constructors
    public FileTreeView(string[] imageResources) : base(imageResources)
    {
      SetupEventHandlers();
    }
    public FileTreeView() : base ()
    {
      SetupEventHandlers();
    }
    private void SetupEventHandlers()
    {
      this.AfterSelect += new TreeViewEventHandler(FileTreeView_AfterSelect); 
    }
    #endregion

    #region Event Handlers
    private void FileTreeView_AfterSelect(object sender, TreeViewEventArgs e)
    {
      // If neccessary, update the child control.
      if (m_showFiles == ShowFilesBehavior.ChildControl)
      {
        if (e.Node.Tag != null)
        {
          FolderInformation fi = (FolderInformation)e.Node.Tag;
          object[] list = ((ArrayList)fi.Items).ToArray();
          if (list.Length > 0)
          {
            string[] files = new string[list.Length];
            for (int x=0; x<list.Length; x++)
            {
              //FileNode fn = (FileNode)list[x];
              files[x] = (string)list[x];
            }
            ChildControl.UpdateList(files);
          }
        }
      }
    }
    #endregion

    public void LoadFiles(string[] filenames)
    {
      foreach(string s in filenames)
        AddTreeNode(s);
    }
//    public void LoadFiles(AbstractFolder[] folderList, AbstractFile[] fileList)
//    {
//      foreach(AbstractFolder folder in folderList)
//        AddTreeNode(folder.FullName);
//
//      foreach(AbstractFile file in fileList)
//        AddTreeNode(file.FullName);
//    }
    /// <summary>
    /// Reads a string array of file names, and populates the control with
    /// nodes and child nodes as appropriate.
    /// </summary>
    /// <param name="nodeName">A string array containing the filenames to
    /// be added to the control.</param>
    public void AddTreeNode(string nodeName)
    {
        string[] parts = nodeName.Split('\\');
        TreeNode n = this.Nodes[0];
        for (int y=0; y<parts.Length; y++)
        {
          bool addNodes = true;
          bool isFolder = true;
          if (y == parts.Length-1)
          {
            if (parts[y].IndexOfAny(".".ToCharArray()) > 0)
            {
              // We can assume that is there is no '.' then this is
              // not a folder.  This wouldn't work in a non-Halo setting.
              isFolder = false;
            }
          }

          if ((!isFolder) && (m_showFiles == ShowFilesBehavior.ChildControl))
          {
            addNodes = false;
          }
            
          if (!addNodes)
          {
            // Add files to the node's Tag for persistance and forwarding
            // to the child IShowFiles control.
            string text = parts[parts.Length-1];
            if (n.Tag == null) n.Tag = new FolderInformation();
            FolderInformation fi = (FolderInformation)n.Tag;
            ArrayList list = (ArrayList)fi.Items;
            if (!list.Contains(text)) list.Add(nodeName);
          }
          else
          {
            bool nodeFound = false;
            foreach (TreeNode node in n.Nodes)
            {
              if (node.Text == parts[y])
              {
                nodeFound = true;
                n = node;
                break;
              }
            }
            if (!nodeFound)
            {
              if (!isFolder) // It's a file
              {
                BetterTreeNode newNode = new BetterTreeNode(parts[y]);
                newNode.CollapsedImageIndex = this.FileImageIndex;
                newNode.ExpandedImageIndex = this.FileImageIndex;
                newNode.ImageIndex = newNode.CollapsedImageIndex;
                
                if (n.Nodes.Count == 1)
                {
                  if (n.Nodes[0] is DummyTreeNode)
                  {
                    n.Nodes.Clear();
                  }
                }
                n.Nodes.Add(newNode);
                n = newNode;
              }
              else // This is another folder.
              {
                if(parts[y] != "")
                {
                  BetterTreeNode newNode = new BetterTreeNode(parts[y]);
                  newNode.CollapsedImageIndex = 2;
                  newNode.ExpandedImageIndex = 1;
                  newNode.ImageIndex = newNode.CollapsedImageIndex;
                  newNode.SelectedImageIndex = newNode.ImageIndex;
                  newNode.Nodes.Add(new DummyTreeNode("Loading Data..."));
                
                  FolderInformation fi = new FolderInformation();
                  string fullPath = "";
                  for (int z=0; z<y+1; z++)
                    fullPath += parts[z] + "\\";
                  fullPath = fullPath.TrimEnd("\\".ToCharArray());
                  fi.FullPath = fullPath;
                  newNode.Tag = fi;

                  if (n.Nodes.Count == 1)
                  {
                    if (n.Nodes[0] is DummyTreeNode)
                    {
                      n.Nodes.Clear();
                    }
                  }
                  n.Nodes.Add(newNode);
                  n = newNode;
                }
              }
            }
          }
        }
    }

    /// <summary>
    /// Returns a list of files under a given node in the FileTreeView.
    /// </summary>
    /// <param name="node">A node contained in the current FileTreeView</param>
    /// <returns>A string array containing a list of files under a node.</returns>
    public string[] GetFilesUnderNode (TreeNode node)
    {
      string[] results = new string[0];
      foreach (TreeNode n in node.Nodes)
      {
        string[] files = GetFilesUnderNode(n);
        results = Utility.MergeArrays(results, files);
      }
      if (node.Tag != null)
      {
        FolderInformation fi = (FolderInformation)node.Tag;
        ArrayList items = (ArrayList)fi.Items;
        if (items != null)
        {
          string[] files = (string[])items.ToArray(typeof(String));
          results = Utility.MergeArrays(results, files);
        }
      }
      return results;
    }
	}

  /// <summary>
  /// Provides a method for a class to accept a list of file strings.
  /// </summary>
  public interface IShowFiles
  {
    void UpdateList(string[] files);
  }
}
