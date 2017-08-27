/* ---------------------------------------------------------------
 * Prometheus
 * Bungie Map-based Multi-Game Editing Kit
 * 2004-2005, Halo-Dev
 * ---------------------------------------------------------------
 * Class       : Prometheus.DecompileNavigator
 * Description : Extends the TreeView class to to allow for
 *             : icons from resources with corrected alpha,
 *             : and alternate icons for selected nodes.
 * Author      : MonoxideC
 * Co-Authors  : 
 * ---------------------------------------------------------------
 */

using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace SharedControls
{
	/// <summary>
  /// Extends the TreeView class to to allow for icons from resources
  /// with corrected alpha, and alternate icons for selected nodes.
	/// </summary>
	public class BetterTreeView : TreeView
	{
    private ArrayList selectedNodes = new ArrayList();
    
    protected TreeNode lastNode;
    protected TreeNode firstNode;

	  #region Constructors
    /// <summary>
		/// Creates the BetterTreeView with a corrected-alpha ImageList
		/// created from embedded resources.
		/// </summary>
		/// <param name="imageResources">A string array of embedded bitmap resource names.</param>
    public BetterTreeView(string[] imageResources) : this()
		{
      CreateImageList(imageResources);
		}
    public BetterTreeView()
    {
      // Setup internal event handlers
      this.AfterExpand += new TreeViewEventHandler(BetterTreeView_AfterExpand);
      this.AfterCollapse += new TreeViewEventHandler(BetterTreeView_AfterCollapse);
    }
    #endregion

    /// <summary>
    /// Creates an corrected-alpha ImageList from embedded resources.
    /// </summary>
    /// <param name="imageResources">A string array of embedded bitmap resource names.</param>
    public void CreateImageList(params string[] imageResources)
    {
      Bitmap[] images = Utility.CreateImagesFromResourcePaths(imageResources);
      this.ImageList = Utility.GenerateImageList(images);
    }

    protected bool isParent(TreeNode parentNode, TreeNode childNode)
    {
      while (childNode.Parent != null)
      {
        if (childNode.Parent == parentNode) return true;
        childNode = childNode.Parent;
      }
      return false;
    }

    #region Event Handlers
    /// <summary>
    /// Updates the Node's icon to reflect the expanded state.
    /// </summary>
    protected void BetterTreeView_AfterExpand(object sender, TreeViewEventArgs e)
    {
      if (e.Node is BetterTreeNode)
      {
        BetterTreeNode n = (BetterTreeNode)e.Node;
        n.ImageIndex = n.ExpandedImageIndex;
        n.SelectedImageIndex = n.ExpandedImageIndex;
      }
    }
    
    /// <summary>
    /// Updates the Node's icon to reflect the collapsed state.
    /// </summary>
    protected void BetterTreeView_AfterCollapse(object sender, TreeViewEventArgs e)
    {
      if (e.Node is BetterTreeNode)
      {
        BetterTreeNode n = (BetterTreeNode)e.Node;
        n.ImageIndex = n.CollapsedImageIndex;
        n.SelectedImageIndex = n.CollapsedImageIndex;
      }
    }
    #endregion

    #region Child Classes
    /// <summary>
    /// Extends the TreeNode control to allow different icons for the
    /// Expanded and Collapsed states of the node.
    /// </summary>
    public class BetterTreeNode : TreeNode
    {
      private int expandedImageIndex;
      private int collapsedImageIndex;

      public int ExpandedImageIndex
      {
        get { return expandedImageIndex; }
        set { expandedImageIndex = value; }
      }

      public int CollapsedImageIndex
      {
        get { return collapsedImageIndex; }
        set { collapsedImageIndex = value; }
      }

      #region BaseClassConstructors
      public BetterTreeNode() : base() { ; }
      public BetterTreeNode(string text) : base(text) { ; }
      public BetterTreeNode(string text, TreeNode[] children) : base(text, children) { ; }
      public BetterTreeNode(string text, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex) { ; }
      public BetterTreeNode(string text, int imageIndex, int selectedImageIndex, TreeNode[] children) : base(text, imageIndex, selectedImageIndex, children) { ; }
      #endregion
    }
    #endregion
	}
}
