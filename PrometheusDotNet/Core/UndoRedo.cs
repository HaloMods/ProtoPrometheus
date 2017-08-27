using System;

namespace Prometheus.Core
{
  public interface ISupportsUndoRedo
  {
    void Undo();

    void Redo();
  }
}
