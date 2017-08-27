using System;
using System.Drawing;
using Prometheus.Core;

namespace Prometheus.Controls
{
  public class BaseTagLibraryNodeHelper : ArchiveLibraryNodeHelper
  {
    public BaseTagLibraryNodeHelper(string name, ITagLibrary library) : base(name, library)
    {
      Bitmap[] images = SharedControls.Utility.CreateImagesFromResourcePaths(
        "Prometheus.Icons.App_Basics._16.document.png");

      this.NodeDefinitions.Add(new NodeDefinition("object", images[0]));
    }
  }

}
