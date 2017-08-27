using System;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;
using Prometheus.Core;

namespace Prometheus.Controls
{
	/// <summary>
	/// Summary description for ObjectViewNodeHelper.
	/// </summary>
	public class ObjectViewNodeHelper : ArchiveLibraryNodeHelper
	{
    public ObjectViewNodeHelper(string name, ITagLibrary library) : base(name, library)
    {
      this.Name = name;

      Bitmap[] images = SharedControls.Utility.CreateImagesFromResourcePaths(
        "Prometheus.Icons.App_Basics._16.folder.png",
        "Prometheus.Icons.App_Basics._16.folder_closed.png",
        "Prometheus.Icons.App_Basics._16.document.png",
        "Prometheus.Icons.Data_Coll._16.data.png");
     
      this.NodeDefinitions.Add(new NodeDefinition("container", images[0], images[1]));
      this.NodeDefinitions.Add(new NodeDefinition("object", images[2]));
      this.NodeDefinitions.Add(new NodeDefinition("child", images[3]));
    }
    public override void ConfigureContainerNode(MultiSyncTreeNode node, string path)
    {
      ConfigureStandardNode(node);

      // "[vehicles]"
      // "[vehicles]vehicles\warthog\warthog.vehicle"
      // "[vehicles]vehicles\warthog\warthog.vehicle[dependencies]
      // "[vehicles]vehicles\warthog\warthog.vehicle[dependencies]vehicles\warthog\warthog.gbxmodel"
      // path = @"[vehicles]vehicles\warthog\warthog.vehicle[dependencies]vehicles\warthog\warthog.gbxmodel";
      
      ObjectViewTagLibrary.ObjectViewPath objPath = new ObjectViewTagLibrary.ObjectViewPath(path);
      string text = "";
      string definition = "container";
      if (objPath.Category != "")
      {
        text = objPath.Category;
      }
      if (objPath.Tag != "")
      {
        definition = "child";
        text = GetObjectNodeNameFromPath(objPath.Tag);
      }
     
      if (node.Text != text) node.Text = text;

      // Add the NodeInformation to this node.
      if (node.NodeInformation == null) node.NodeInformation = new NodeInformation(NodeType.Container);
      node.NodeInformation.PathItems.Add(new PathItem(this, path));
      if (!nodes.Contains(node)) nodes.Add(node);

      // TODO: Possibly implement this path structure.
      // Ex: objects[Bipeds]:\cyborg_mp.biped\dependencies:\characters\cyborg\cyborg.model_animations
      
      node.CollapsedImageIndex = ParentTreeView.Bitmaps.IndexOf(NodeDefinitions[definition].CollapsedIcon);
      node.ExpandedImageIndex = ParentTreeView.Bitmaps.IndexOf(NodeDefinitions[definition].ExpandedIcon);
      node.ImageIndex = node.CollapsedImageIndex; 
      node.SelectedImageIndex = node.CollapsedImageIndex;
    }
	}
}
