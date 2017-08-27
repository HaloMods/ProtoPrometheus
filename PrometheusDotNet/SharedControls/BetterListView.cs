/* ---------------------------------------------------------------
 * Prometheus
 * Bungie Map-based Multi-Game Editing Kit
 * 2004-2005, Halo-Dev
 * ---------------------------------------------------------------
 * Class       : Prometheus.DecompileNavigator
 * Description : Extends the ListView class to to allow for
 *             : icons from resources with corrected alpha.
 * Author      : MonoxideC
 * Co-Authors  : 
 * ---------------------------------------------------------------
 */

using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace SharedControls
{
  /// <summary>
  /// Extends the ListView class to to allow for icons from resources
  /// with corrected alpha.
  /// </summary>
	public class BetterListView : ListView
	{
    #region Constructors
    /// <summary>
    /// Creates the BetterListView with a corrected-alpha ImageList
    /// created from embedded resources.
    /// </summary>
    /// <param name="imageResources">A string array of embedded bitmap resource names.</param>
    public BetterListView(string[] imageResources) : this()
    {
      CreateImageList(imageResources);
    }
    public BetterListView()
    {
    }
    #endregion

    /// <summary>
    /// Creates an corrected-alpha ImageList from embedded resources.
    /// </summary>
    /// <param name="imageResources">A string array of embedded bitmap resource names.</param>
    public void CreateImageList(params string[] imageResources)
    {
      Bitmap[] images = Utility.CreateImagesFromResourcePaths(imageResources);
      this.LargeImageList = Utility.GenerateImageList(images);
      this.SmallImageList = Utility.GenerateImageList(images);
    }
	}
}
