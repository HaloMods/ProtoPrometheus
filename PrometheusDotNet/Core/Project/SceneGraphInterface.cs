using System;
using Prometheus.Core.Render;

namespace Prometheus.Core.Project
{
  abstract public class ISceneGraphInterface
  {
    abstract public void AddToPalette(ObjectType type, string relative_path);
    abstract public void AddInstance(ObjectType type, string item_path);
    abstract public void RemoveInstance(Instance3D Item);
    abstract public void EditInstance(Instance3D Item);
  }

  /// <summary>
	/// Summary description for SceneGraphInterface.
	/// </summary>
	public class SceneGraphInterface
	{
		public SceneGraphInterface()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}
