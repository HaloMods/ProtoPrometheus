using System.Drawing;
using Prometheus.Core;
using SharedControls;

namespace Prometheus.Controls
{
  public class ArchiveLibraryNodeHelper : NodeHelper
  {
    public ArchiveLibraryNodeHelper(string name, ITagLibrary library)
    {
      this.Name = name;
      this.Library = library;

      Bitmap[] images = SharedControls.Utility.CreateImagesFromResourcePaths(
        "Prometheus.Icons.App_Basics._16.folder.png",
        "Prometheus.Icons.App_Basics._16.folder_closed.png",
        "Prometheus.Icons.App_Basics._16.document.png");

      this.NodeDefinitions.Add(new NodeDefinition("container", images[0], images[1]));
      this.NodeDefinitions.Add(new NodeDefinition("object", images[2]));
    }

    public override void ConfigureContainerNode(MultiSyncTreeNode node, string path)
    {
      ConfigureStandardNode(node);

      string text = GetContainerNodeNameFromPath(path);
      if (node.Text != text) node.Text = text;

      // TODO: Add font configuration.
      // TODO: Extend BetterTreeNode to support font style changes inside
      // a single text string. (ex: <i>This is <b>bold</b></i>)

      // Add the NodeInformation to this node.
      if (node.NodeInformation == null) node.NodeInformation = new NodeInformation(NodeType.Container);
      node.NodeInformation.PathItems.Add(new PathItem(this, path));
      if (!nodes.Contains(node)) nodes.Add(node);

      // TODO: Implement dependency paths from object view paths.
      // Ex: objects[Bipeds]:\cyborg_mp.biped\dependencies:\characters\cyborg\cyborg.model_animations
      
      node.CollapsedImageIndex = ParentTreeView.Bitmaps.IndexOf(NodeDefinitions["container"].CollapsedIcon);
      node.ExpandedImageIndex = ParentTreeView.Bitmaps.IndexOf(NodeDefinitions["container"].ExpandedIcon);
      node.ImageIndex = node.CollapsedImageIndex; 
      node.SelectedImageIndex = node.CollapsedImageIndex;
    }

    public override void ConfigureObjectNode(MultiSyncTreeNode node, string path)
    {
      ConfigureStandardNode(node);
      string text = GetObjectNodeNameFromPath(path);
      if (node.Text != text) node.Text = text;

      // Add the NodeInformation to this node.
      if (node.NodeInformation == null) node.NodeInformation = new NodeInformation(NodeType.Object);
      node.NodeInformation.PathItems.Add(new PathItem(this, path));
      if (!nodes.Contains(node)) nodes.Add(node);

      node.CollapsedImageIndex = ParentTreeView.Bitmaps.IndexOf(NodeDefinitions["object"].CollapsedIcon);
      node.ExpandedImageIndex = node.CollapsedImageIndex;
      node.ImageIndex = node.CollapsedImageIndex; 
      node.SelectedImageIndex = node.CollapsedImageIndex;
    }
  }
}