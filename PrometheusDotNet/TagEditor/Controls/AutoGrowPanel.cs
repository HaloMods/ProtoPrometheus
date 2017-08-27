using System.Drawing;
using System.Windows.Forms;

namespace Prometheus.TagEditor.Controls
{
	/// <summary>
	/// Extends the panel to support a list-style collection
	/// of controls in which they are automatically docked to
	/// the top of the panel at index 0, and the panel's
	/// height is automatically adjusted to show all controls.
	/// </summary>
	public class AutoGrowPanel : Panel
	{
		public AutoGrowPanel() : base() { ; }

    public void AddControl(Control c)
    {
      this.SuspendLayout();
      c.Dock = DockStyle.Top;
      this.Controls.Add(c);
      this.Controls.SetChildIndex(c, 0);

      // Calculate the new size
      int height = 0;
      height += c.Height;

      this.Size = new Size(this.Width, height);
      this.ResumeLayout();
    }
	}
}
