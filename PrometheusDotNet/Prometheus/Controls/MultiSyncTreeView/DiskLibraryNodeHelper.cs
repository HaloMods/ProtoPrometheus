using System.Drawing;
using Prometheus.Core;

namespace Prometheus.Controls
{
  public class DiskLibraryNodeHelper : ArchiveLibraryNodeHelper
  {
    public DiskLibraryNodeHelper(string name, ITagLibrary library) : base(name, library)
    {
      Bitmap[] images = SharedControls.Utility.CreateImagesFromResourcePaths(
        "Prometheus.Icons.App_Basics._16.information.png");

      this.NodeDefinitions.Add(new NodeDefinition("object", images[0]));
    }

    // TODO: Change the icon used above.
    // This will be a first level node - if the file is found to exist inside the
    // project, the style will be overriden.
  }
}
