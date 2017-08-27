using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Prometheus.Controls
{
	/// <summary>
	/// Summary description for TransparantPanel.
	/// </summary>
	public class TransparantPanel : Panel
	{
    private float opacity = 0.5f;
    private ColorMatrix opacityMatrix = new ColorMatrix();

    [Category("Appearance"), DefaultValue(typeof(float), "0.1"),
    Description("Gets or Sets the opacity level of the panel.")]
    public float Opacity
    {
      set
      {
        opacity = value;
        this.Invalidate();
      }
      get { return opacity; }
    }

		public TransparantPanel()
		{
//      this.SetStyle(ControlStyles.DoubleBuffer, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.Move += new System.EventHandler(ProgressPanel_Move);
		}

    private void ProgressPanel_Move(object sender, EventArgs e)
    {
      this.Invalidate();
    }

    private  static extern  bool  BitBlt(
      IntPtr hdcDest, // handle to destination DC
      int nXDest, // x-coord of destination upper-left corner
      int nYDest, // y-coord of destination upper-left corner
      int nWidth, // width of destination rectangle
      int nHeight, // height of destination rectangle
      IntPtr hdcSrc, // handle to source DC
      int nXSrc, // x-coordinate of source upper-left corner
      int nYSrc, // y-coordinate of source upper-left corner
      System.Int32 dwRop // raster operation code
      );

    protected Image GetBackgroundImage()
    {
      Graphics g1 = this.Parent.CreateGraphics();
      Image MyImage = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height, g1);
      Graphics g2 = Graphics.FromImage(MyImage);
      IntPtr dc1 = g1.GetHdc();
      IntPtr dc2 = g2.GetHdc();
      BitBlt(dc2, 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height, dc1, 0, 0, 13369376);
      g1.ReleaseHdc(dc1);
      g2.ReleaseHdc(dc2);
      return MyImage;
    }

    protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
    {
      Bitmap mb = new Bitmap( GetBackgroundImage());
      if (this.BackgroundImage == null)
      {
        if (!((Parent.BackgroundImage == null)))
        {
          e.Graphics.DrawImage(mb, new Rectangle(0, 0, this.Width, this.Height), this.Left, this.Top, this.Width, this.Height, GraphicsUnit.Pixel);
        }
        else
        {
          e.Graphics.FillRectangle(new SolidBrush(Parent.BackColor), new Rectangle(0, 0, this.Width, this.Height));
        }
      }
 
      ImageAttributes attributes = new ImageAttributes();
      
      ColorMatrix matrix = new ColorMatrix();
      matrix.Matrix00 = matrix.Matrix11 = matrix.Matrix22 = matrix.Matrix44 = 1;
      matrix.Matrix33 = this.opacity;
      attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
      
      Bitmap bitmap = new Bitmap(this.Width, this.Height, PixelFormat.Format24bppRgb);
      Graphics g = Graphics.FromImage(bitmap); //e.Graphics;
      g.CompositingMode = CompositingMode.SourceOver;
      Brush brush = new SolidBrush(this.BackColor);
      g.FillRectangle(brush, new RectangleF(0, 0, this.Width, this.Height));
      brush.Dispose();
      e.Graphics.DrawImage(bitmap, new Rectangle(0, 0, this.Width, this.Height), 0, 0, this.Width, this.Height, GraphicsUnit.Pixel, attributes);
    }
	}
}
