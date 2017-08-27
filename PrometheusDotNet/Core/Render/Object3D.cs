using System;
using System.Collections;
using Microsoft.DirectX;

namespace Prometheus.Core.Render
{
  /// <summary>
  /// Provides a base class for 3D objects.
  /// </summary>
  public abstract class Object3D
  {
    protected string name; 

    /// <summary>
    /// Gets the name of the object.
    /// </summary>
    public string Name
    {
      get { return name; }
    }

    /// <summary>
    /// When overrideen in a derived class, returns the bounding radius of the object.
    /// </summary>
    public abstract float BoundingRadius { get; }

    public Object3D(string name)
    {
      this.name = name;
    }
    
    public abstract void Render();
  }
}
