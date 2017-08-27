using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace UIControls.StandardControls
{
	/// <summary>
	/// A Panel that automatically resizes to display all child controls.
	/// </summary>
	public class AutoSizePanel : System.Windows.Forms.Panel
	{
    private ResizeBehavior resizeBehavior = ResizeBehavior.None;
    private int padding;
    private bool fittingControls = false;

    [DefaultValue(ResizeBehavior.Both)]
    [Category("Behavior")]
    public ResizeBehavior ResizeBehavior
	  {
	    get { return resizeBehavior; }
	    set
      {
        resizeBehavior = value; 
        FitControls(this);
      }
	  }

    [Category("Appearance")]
    public int Padding
	  {
	    get { return padding; }
	    set
      {
        padding = value;
        FitControls(this);
      }
	  }

	  public AutoSizePanel()
    {
      SetStyle(ControlStyles.DoubleBuffer, true);
      SetStyle(ControlStyles.UserPaint, true);
      SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      
      this.SizeChanged += new EventHandler(AutoSizePanel_SizeChanged);
      this.ControlAdded += new System.Windows.Forms.ControlEventHandler(AutoSizePanel_ControlAdded);
      this.ControlRemoved += new ControlEventHandler(AutoSizePanel_ControlRemoved);
      FitControls(this);
		}

    private void AutoSizePanel_SizeChanged(object sender, EventArgs e)
    {
      FitControls(this);
    }

	  private void AutoSizePanel_ControlRemoved(object sender, ControlEventArgs e)
	  {
      e.Control.Move -= new EventHandler(Control_Move);
      e.Control.SizeChanged -= new EventHandler(Control_SizeChanged);
      FitControls(this);
	  }

	  private void AutoSizePanel_ControlAdded(object sender, ControlEventArgs e)
	  {
	    e.Control.Move += new EventHandler(Control_Move);
      e.Control.SizeChanged += new EventHandler(Control_SizeChanged);
      FitControls(this);
	  }

	  private void Control_SizeChanged(object sender, EventArgs e)
	  {
	    FitControls(this);
	  }

	  private void Control_Move(object sender, EventArgs e)
	  {
	    FitControls(this);
	  }
    
    protected void FitControls(Control container)
    {
      if (fittingControls) return;
      if (container.Controls.Count < 1) return;

      fittingControls = true;
      int maxY = 0;
      int minY = 0;
      int maxX = 0;
      int minX = 0;
      foreach (Control c in container.Controls)
      {
        // Fix locations if neccessary.
        if (c.Top < this.padding)
          c.Location = new Point(c.Left, this.padding);

        if (c.Left < this.padding)
          c.Location = new Point(this.padding, c.Top);

        // Check the locations.
        if (c.Top < minY) minY = c.Top;
        if (c.Bottom + padding > maxY) maxY = c.Bottom + padding;
        if (c.Left < minX) minX = c.Left;
        if (c.Right + padding > maxX) maxX = c.Right + padding;
      }

      if (this.resizeBehavior == ResizeBehavior.Both)
      {
        if ((container.Size.Height != maxY) || (container.Size.Width != maxX))
          container.Size = new System.Drawing.Size(maxX, maxY);
      }
      else if (this.resizeBehavior == ResizeBehavior.Width)
      {
        if (container.Size.Width != maxX)
          container.Size = new System.Drawing.Size(maxX, this.Height);
      }
      else if (this.resizeBehavior == ResizeBehavior.Height)
      {
        if (container.Size.Height != maxY)
          container.Size = new System.Drawing.Size(this.Width, maxY);
      }
      
      fittingControls = false;
    }
	}

  public enum ResizeBehavior
  {
    Height,
    Width,
    Both,
    None
  }

  [Serializable]
  public class EdgePadding
  {
    private int top = 0;
    private int bottom = 0;
    private int left = 0;
    private int right = 0;
    private int all;

    public int Top
    {
      get { return top; }
      set 
      { 
        all = 0;
        top = value;
      }
    }

    public int Bottom
    {
      get { return bottom; }
      set 
      {
        all = 0;
        bottom = value; 
      }
    }

    public int Left
    {
      get { return left; }
      set 
      {
        all = 0;
        left = value; 
      }
    }

    public int Right
    {
      get { return right; }
      set
      {
        all = 0;
        right = value; 
      }
    }

    public int All
    {
      get { return all; }
      set 
      {
        top = value;
        bottom = value;
        left = value;
        right = value;
        all = value;
      }
    }

    public EdgePadding() { ; }
  }
}
