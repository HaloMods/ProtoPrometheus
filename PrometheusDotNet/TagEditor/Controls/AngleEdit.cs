using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DevExpress.Skins;

namespace TagEditor.Controls
{
	/// <summary>
	/// Summary description for AngleEdit.
	/// </summary>
	public class AngleEdit : DevExpress.XtraEditors.XtraUserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

    private Color foreColor;
    private Color backColor;

		public event EventHandler AngleChanged;

		public int Angle
		{
			get { return angle; }
			set { angle = value; this.Refresh(); }
		}

		public int OverAngle
		{
			get { return overAngle; }
		}

		private Size circleSize
		{
			get { return new Size(this.Width-2, this.Height-2); }
		}

		private int angle = 0;
		private int overAngle = 0;

		public AngleEdit()
		{
			InitializeComponent();
			this.SetStyle(ControlStyles.UserPaint, true); 
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true); 
			this.SetStyle(ControlStyles.DoubleBuffer, true);

      this.LookAndFeel.StyleChanged += new EventHandler(LookAndFeel_StyleChanged);
      SetupColors();
		}

	  private void LookAndFeel_StyleChanged(object sender, EventArgs e)
	  {
	    SetupColors();
	  }

	  private void SetupColors()
    {
      backColor = CommonSkins.GetSkin(LookAndFeel).TranslateColor(SystemColors.Control);
      ICollection collection = CommonSkins.GetSkin(LookAndFeel).GetElements();
      foreach (object o in collection)
      {
        if (o.ToString() == "TextBorder")
        {
          SkinElement element = (o as SkinElement);
          foreColor = element.Border.All;
          break;
        }
      }
    }
   
		private bool mouseIn = false;

		protected override void OnClick(EventArgs e)
		{
			base.OnClick (e);
			angle = overAngle;
			if (AngleChanged != null) AngleChanged(this, new EventArgs());
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter (e);
			mouseIn = true;
			this.Cursor = Cursors.Hand;
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseEnter (e);
			mouseIn = false;
			this.Cursor = Cursors.Default;
			this.Refresh();
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			overAngle = Convert.ToInt32(Math.Round(Math.Atan2(e.X-this.circleSize.Width/2, this.circleSize.Height/2-e.Y) / (Math.PI / 180)));
			overAngle = (overAngle < 0) ? overAngle+360 : overAngle;
			if ((e.Button & MouseButtons.Left) > 0)
			{
				angle = overAngle;
				if (AngleChanged != null) AngleChanged(this, new EventArgs());
			}
			this.Refresh();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
      Color penColor = this.foreColor;
      Color lightPenColor = Color.LightGray;
      Color backColor = this.backColor;
     
      if (!this.Enabled)
      {
        // Fade all of the colors.
        penColor = Color.FromArgb(45, penColor);
        lightPenColor = Color.FromArgb(45, lightPenColor);
        backColor = Color.FromArgb(45, backColor);
      }

      float innerCircleWidth = (this.circleSize.Width * 0.20f);

      Graphics g = e.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias; 
			g.PixelOffsetMode = PixelOffsetMode.HighQuality;

			Pen p_dark = new Pen(penColor, 1);
			Pen p_light_thin = new Pen(lightPenColor, 1);
			
			g.FillRectangle(new SolidBrush(this.Appearance.BackColor), new Rectangle(0, 0, this.Width, this.Height));
			
      g.FillEllipse(new SolidBrush(backColor), new Rectangle(1,1,this.circleSize.Width,this.circleSize.Height));
			g.DrawEllipse(p_dark, new Rectangle(1,1,this.circleSize.Width,this.circleSize.Height));
		
			double angleR;
			int destinationX, destinationY;

			if (mouseIn)
			{
				angleR = overAngle * (Math.PI / 180);

				destinationX = Convert.ToInt32(Math.Round(this.circleSize.Width/2 + (Math.Sin(angleR) * this.circleSize.Width/2))+1);
				destinationY = Convert.ToInt32(Math.Round(this.circleSize.Height/2 - (Math.Cos(angleR) * this.circleSize.Height/2))+1);

				g.DrawLine(p_light_thin, this.circleSize.Width/2, this.circleSize.Height/2, destinationX, destinationY);
			}
			
			angleR = angle * (Math.PI / 180);
      double angle2 = (angle-90) * (Math.PI / 180);
      double angle3 = (angle+90) * (Math.PI / 180);

      g.FillPolygon(new SolidBrush(penColor),
        new PointF[]
        {
          new PointF((float)(this.circleSize.Width/2 + (Math.Sin(angleR) * this.circleSize.Width/2)+1), 
                    (float)(this.circleSize.Height/2 - (Math.Cos(angleR) * this.circleSize.Height/2)+1)),
          new PointF((float)(this.circleSize.Width/2 + (Math.Sin(angle2) * innerCircleWidth/2)+1), 
                     (float)(this.circleSize.Height/2 - (Math.Cos(angle2) * innerCircleWidth/2)+1)),
          new PointF((float)(this.circleSize.Width/2 + (Math.Sin(angle3) * innerCircleWidth/2)+1), 
                     (float)(this.circleSize.Height/2 - (Math.Cos(angle3) * innerCircleWidth/2)+1))
        });

      g.FillEllipse(new SolidBrush(backColor),
        ((float)this.circleSize.Width/2)-(innerCircleWidth/2)-0.5f,
        ((float)this.circleSize.Height/2)-(innerCircleWidth/2)-0.5f,
        innerCircleWidth+2,
        innerCircleWidth+2);
      g.DrawEllipse(p_dark,
        ((float)this.circleSize.Width/2)-(innerCircleWidth/2)-0.5f,
        ((float)this.circleSize.Height/2)-(innerCircleWidth/2)-0.5f,
        innerCircleWidth+2,
        innerCircleWidth+2);
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
			// AngleEdit
			// 
			this.Name = "AngleEdit";
			this.Size = new System.Drawing.Size(51, 51);

		}
		#endregion
	}
}

