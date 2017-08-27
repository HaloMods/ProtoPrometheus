using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.Utils.Drawing;

namespace UIControls.StandardControls
{
	/// <summary>
	/// Summary description for ImageButton.
	/// </summary>
	public class ImageButton : DevExpress.XtraEditors.XtraUserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private bool mouseIn = false;
		private bool mouseDown = false;

		public ImageButton()
		{
			InitializeComponent();
			this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.SetStyle(ControlStyles.UserPaint, true); 
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true); 
			this.SetStyle(ControlStyles.DoubleBuffer, true);
		}

		public bool HotTrack
		{
			get { return hotTrack; }
			set { hotTrack = value; if (hotTrack) imageSizeRatio = 1f; else imageSizeRatio = 0.8f; }
		}

		public Image Image
		{
			get { return image; }
			set { image = value; }
		}

		public Image PressedImage
		{
			get { return pressedImage; }
			set { pressedImage = value; }
		}

		public Image AltImage
		{
			get { return altImage; }
			set { altImage = value; }
		}

		public bool Toggle
		{
			get { return toggle; }
			set { toggle = value; this.Refresh(); }
		}

		public float ImageSizeRatio
		{
			get { return imageSizeRatio; }
			set { imageSizeRatio = value; }
		}

		private bool hotTrack = true;
		private Image image;
		private Image pressedImage;
		private Image altImage;

		private Image drawImage;

		private bool toggle = false;

		private float imageSizeRatio = 0.8f;

		protected override void OnPaint(PaintEventArgs e)
		{
			this.BackColor = Color.Transparent;
			ColorMatrix bm = null;
			//e.Graphics.DrawRectangle(SystemPens.Control, new Rectangle(this.Location,  this.Size));
			//e.Graphics.DrawRectangle(new Pen(Color.Transparent), new Rectangle(this.Location,  this.Size));
			
			if (!this.Enabled)
			{
				float af = -0.25f;

				bm = new ColorMatrix(new float[][]{

																						new float[]{1f,    0f,    0f,    af,    0f},

																						new float[]{0f,    1f,    0f,    af,    0f},

																						new float[]{0f,    0f,    1f,    af,    0f},

																						new float[]{0f,    0f,    0f,    1f,    0f},

																						new float[]{0f,    0f,    0f,    0f,    1f}});
			}
			else if (mouseIn)
			{
				if (hotTrack)
				{
					ObjectInfoArgs o = new ObjectInfoArgs(new GraphicsCache(e), new Rectangle(new Point(0, 0),  this.Size), (mouseDown) ? ObjectState.Pressed : ObjectState.Hot);
					this.LookAndFeel.Painter.Button.DrawObject(o);
				}
				float bf = 0.07f;

				bm = new ColorMatrix(new float[][]{

																						new float[]{1f,    0f,    0f,    0f,    0f},

																						new float[]{0f,    1f,    0f,    0f,    0f},

																						new float[]{0f,    0f,    1f,    0f,    0f},

																						new float[]{0f,    0f,    0f,    1f,    0f},

																						new float[]{bf,    bf,    bf,    0f,    1f}});
			}

			if (this.image != null)
			{
				if (mouseDown && pressedImage != null) drawImage = pressedImage;
				else
				{
					if (altImage != null && toggle)
					{
						drawImage = altImage;
					}
					else
					{
						drawImage = image;
					}
				}

				Size imageSize;

				float widthToHeightRatio = Convert.ToSingle(drawImage.Size.Height)/Convert.ToSingle(drawImage.Size.Width);

				if (drawImage.Size.Width > drawImage.Size.Height)
				{
					imageSize = new Size(Convert.ToInt32(this.Size.Width*imageSizeRatio), Convert.ToInt32(this.Size.Width*imageSizeRatio*widthToHeightRatio));
				}
				else
				{
					imageSize = new Size(Convert.ToInt32(this.Size.Height*imageSizeRatio*(1f/widthToHeightRatio)), Convert.ToInt32(this.Size.Width*imageSizeRatio));
				}

				Point imageLocation = new Point((this.Size.Width/2)-(imageSize.Width/2), (this.Size.Height/2)-(imageSize.Height/2));
			
				if (bm != null)
				{
					ImageAttributes ia = new ImageAttributes();
					ia.SetColorMatrix(bm);
					e.Graphics.DrawImage(drawImage, new Rectangle(imageLocation, imageSize), 0, 0, drawImage.Size.Width, drawImage.Size.Height, GraphicsUnit.Pixel, ia);
				}
				else
				{
					e.Graphics.DrawImage(drawImage, new Rectangle(imageLocation, imageSize));
				}
			}

		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter (e);
			if (!this.hotTrack) this.Cursor = Cursors.Hand;
      mouseIn = true;
			this.Refresh();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave (e);
      this.Cursor = Cursors.Default;
			mouseIn = false;
			this.Refresh();
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown (e);
			mouseDown = true;
			this.Refresh();
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp (e);
			mouseDown = false;
			if (altImage != null) toggle = !toggle;
			this.Refresh();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// ImageButton
			// 
			this.Appearance.Options.UseBackColor = true;
			this.Name = "ImageButton";
			this.Size = new System.Drawing.Size(64, 56);

		}
		#endregion

	}
}

