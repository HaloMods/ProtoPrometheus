using System;
using System.Drawing;

namespace UIControls.StandardControls
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class DropdownButton : DevExpress.XtraEditors.SimpleButton
	{
    private DevExpress.XtraBars.PopupMenu popupMenu = null;

    public DevExpress.XtraBars.PopupMenu PopupMenu
    {
      get { return this.popupMenu; }
      set { this.popupMenu = value; }
    }
  
		public DropdownButton() : base()
		{
      this.Click += new EventHandler(DropdownButton_Click);
		}

	  private void DropdownButton_Click(object sender, EventArgs e)
	  {
      if (popupMenu != null)
      {
        Point p = new Point();
        p.X = this.Location.X;
        p.Y = this.Location.Y + this.Size.Height;
        popupMenu.ShowPopup(p);
      }
	  }
	}
}