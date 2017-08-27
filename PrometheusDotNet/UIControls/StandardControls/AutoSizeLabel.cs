using System;
using System.Drawing;
using System.Windows.Forms;

namespace UIControls.StandardControls
{
	/// <summary>
	/// Summary description for AutoSizeLabel.
	/// </summary>
	public class AutoSizeLabel : Label
	{
		private bool autoSizeHeight = false;

    public bool AutoSizeHeight
    {
      get { return autoSizeHeight; }
      set
      {
        autoSizeHeight = value;
        this.Refresh();
      }
    }

    public override string Text
    {
      get { return base.Text; }
      set
      {
        base.Text = value;
        if (autoSizeHeight)
        {
          // Measure the size of the string.
          using (Graphics surface = this.CreateGraphics())
          {
            SizeF f = surface.MeasureString(this.Text, this.Font, this.Width);
            int height = Convert.ToInt32(f.Height);
            if (this.Size.Height != height)
            {
              this.Size = new Size(this.Size.Width, height);
            }
          }
        }
      }
    }

    public AutoSizeLabel() { ; }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (autoSizeHeight)
      {
        // Measure the size of the string.
        SizeF f = e.Graphics.MeasureString(this.Text, this.Font, this.Width);
        int height = Convert.ToInt32(f.Height);
        if (this.Size.Height != height)
        {
          this.Size = new Size(this.Size.Width, height);
        }
      }
      base.OnPaint (e);
    }

	}
}
