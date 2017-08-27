using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace UIControls.StandardControls
{
	/// <summary>
	/// A List-Style collection of controls inside a Panel.
	/// Also provides docking-style features such as auto-repositioning of controls.
	/// </summary>
	public class ControlListPanel : AutoSizePanel
	{
		private int spacing = 0;
    private ArrayList controlList = new ArrayList();

    public int Spacing
    {
      get { return spacing; }
      set { spacing = value; }
    }

    public ControlListPanel() : base()
		{
      this.ControlAdded += new System.Windows.Forms.ControlEventHandler(ControlListPanel_ControlAdded);
      this.ControlRemoved += new ControlEventHandler(ControlListPanel_ControlRemoved);
		}

	  private void ControlListPanel_ControlRemoved(object sender, ControlEventArgs e)
	  {
	    e.Control.SizeChanged -= new EventHandler(Control_SizeChanged);
	  }

	  private void ControlListPanel_ControlAdded(object sender, ControlEventArgs e)
	  {
	    controlList.Add(e.Control);
      
      // "Dock" the control to the top.
      int top = this.Padding;
      if (controlList.Count > 1)
        top = (controlList[controlList.Count-2] as Control).Bottom + spacing;
      
      e.Control.Location = new Point(this.Padding, top);
      e.Control.SizeChanged += new EventHandler(Control_SizeChanged);
    }

	  private void Control_SizeChanged(object sender, EventArgs e)
	  {
      Control control = sender as Control;
      //Console.WriteLine("Control size changed: {0} - {1}x{2} - (Child of {3})", control, control.Width, control.Height, control.Parent);

      // Get the relative change in vertical position based on the next control.
      //int index = this.Controls.GetChildIndex(control);
      int index = controlList.IndexOf(control);
      if (index == controlList.Count-1) return; // This is the last control - nothing to reposition.
      Control nextControl = (controlList[index+1] as Control);
      int verticalChange = ((nextControl.Top - control.Bottom) - spacing);

      // Reposition all of the controls that are under this control.
      for (int x=index+1; x<controlList.Count; x++)
      {
        (controlList[x] as Control).Location = new Point(this.Padding, (controlList[x] as Control).Top - verticalChange);
      }
	  }
	}
}