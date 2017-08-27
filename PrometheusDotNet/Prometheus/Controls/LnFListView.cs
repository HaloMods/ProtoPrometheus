using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.Utils.Drawing;
using Prometheus;

namespace Prometheus.Controls
{
	/// <summary>
	/// Summary description for LnFListView.
	/// </summary>
	public class LnFListView : System.Windows.Forms.ListView, ISupportLookAndFeel
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		private HeaderControl header;
		private UserLookAndFeel lookAndFeel;

		public UserLookAndFeel LookAndFeel
		{
			get { return lookAndFeel; }
		}

		public bool IgnoreChildren
		{
			get { return false; }
		}

		private ColumnHeader[]  m_lockedHeaders;

		public ColumnHeader[] LockedHeaders
		{
			get { return m_lockedHeaders; }
			set { m_lockedHeaders = value; }
		}

		#region Events
		public delegate void VoidEventHandler();
		public event VoidEventHandler Scrolled;
		#endregion

		public LnFListView()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			this.m_lockedHeaders = new ColumnHeader[0];
			this.BorderStyle = BorderStyle.None;
			lookAndFeel = new UserLookAndFeel(this.Parent);

		}

		protected override void OnHandleCreated(EventArgs e)
		{
			//Create a new HeaderControl object
			header = new HeaderControl(this);
			if(header.Handle != IntPtr.Zero)
			{
				//if(headerImages != null)//If we have a valid header handle and a valid ImageList for it
				//send a message HDM_SETIMAGELIST
				Win32.SendMessage(header.Handle,0x1200+8,IntPtr.Zero,header.Handle);			
			}
			base.OnHandleCreated(e);
		}

		internal class HeaderControl : NativeWindow
		{
			LnFListView parent;
			bool mouseDown;
			public HeaderControl(LnFListView m)
			{
				parent = m;
				//Get the header control handle
				IntPtr header = Win32.SendMessage(parent.Handle, (0x1000+31), IntPtr.Zero, IntPtr.Zero);
				this.AssignHandle(header);				
			}

			#region Overriden WndProc

			protected override void WndProc(ref Message m)
			{



				switch(m.Msg)
				{
					case 0x000F://WM_PAINT

						Win32.RECT update = new Win32.RECT();
						if(Win32.GetUpdateRect(m.HWnd,ref update, false)==0)
							break;
						//Fill the paintstruct
						Win32.PAINTSTRUCT ps = new Win32.PAINTSTRUCT();
						IntPtr hdc = Win32.BeginPaint(m.HWnd, ref ps);
						//Create graphics object from the hdc
						Graphics g = Graphics.FromHdc(hdc);
						//Get the non-item rectangle
						int left = 0;
						Win32.RECT itemRect = new Win32.RECT();
						for(int i=0; i<parent.Columns.Count; i++)
						{								
							//HDM_GETITEMRECT
							Win32.SendMessage(m.HWnd, 0x1200+7, i, ref itemRect);
							left += itemRect.right-itemRect.left;								
						}
						//parent.headerHeight = itemRect.bottom-itemRect.top;
						if(left >= ps.rcPaint.left)
							left = ps.rcPaint.left;

						Rectangle r = new Rectangle(left, ps.rcPaint.top, 
							ps.rcPaint.right-left, ps.rcPaint.bottom-ps.rcPaint.top);
				//		Rectangle r1 = new Rectangle(ps.rcPaint.left, ps.rcPaint.top, 
				//		ps.rcPaint.right-left, ps.rcPaint.bottom-ps.rcPaint.top);

						g.FillRectangle(new SolidBrush(parent.BackColor),r);

						/*//If we have a valid event handler - call it
						if(parent.DrawHeader != null)
							parent.DrawHeader(new DrawHeaderEventArgs(g,r,
								itemRect.bottom-itemRect.top));*/
					
						//Now we have to check if we have owner-draw columns and fill
						//the DRAWITEMSTRUCT appropriately
						int counter = 0;
						foreach(ColumnHeader mm in parent.Columns)
						{

							Win32.DRAWITEMSTRUCT dis = new Win32.DRAWITEMSTRUCT();
							dis.ctrlType = 100;//ODT_HEADER
							dis.hwnd = m.HWnd;
							dis.hdc = hdc;
							dis.itemAction = 0x0001;//ODA_DRAWENTIRE
							dis.itemID = counter;
							//Must find if some item is pressed
							Win32.HDHITTESTINFO hi = new Win32.HDHITTESTINFO();
							hi.pt.X = parent.PointToClient(MousePosition).X;
							hi.pt.Y = parent.PointToClient(MousePosition).Y;
							int hotItem = Win32.SendMessage(m.HWnd, 0x1200+6, 0, ref hi);
							//If clicked on a divider - we don't have hot item
							if(hi.flags == 0x0004 || hotItem != counter)
								hotItem = -1;
							if(hotItem != -1 && mouseDown)
								dis.itemState = 0x0001;//ODS_SELECTED
							else
								dis.itemState = 0x0020;
							//HDM_GETITEMRECT
							Win32.SendMessage(m.HWnd, 0x1200+7, counter, ref itemRect);
							dis.rcItem = itemRect;
							//Send message WM_DRAWITEM
							Win32.SendMessage(parent.Handle,0x002B,0,ref dis);
							counter++;
						}
						Win32.EndPaint(m.HWnd, ref ps);
													
						break;
					case 0x0014://WM_ERASEBKGND
						//We don't need to do anything here in order to reduce flicker
						//	if(parent.FullyCustomHeader)
						break;						
					case 0x0201://WM_LBUTTONDOWN
						mouseDown = true;
						base.WndProc(ref m);
						break;
					case 0x0202://WM_LBUTTONUP
						mouseDown = false;
						base.WndProc(ref m);
						break;
					case 0x1200+5://HDM_LAYOUT
						base.WndProc(ref m);
						break;
						/*	case 0x0030://WM_SETFONT						
								if(parent.IncreaseHeaderHeight > 0)
								{
									System.Drawing.Font f = new System.Drawing.Font(parent.Font.Name,
										parent.Font.SizeInPoints + parent.IncreaseHeaderHeight);
									m.WParam = f.ToHfont();
								}						
								base.WndProc(ref m);						
								break;       */
					default:
						base.WndProc(ref m);
						break;
				}
			}

			#endregion
		}

		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case 522:
					base.WndProc(ref m);
					Scrolled();
					break;
				case 0xF://WM_PAINT
					base.WndProc(ref m);
		//		if ( this.View == View.Details && this.Columns.Count > 0 )
		//		{
		//			foreach (ColumnHeader h in LockedHeaders)
		//			{
		//				h.Width = 20;
		//			}
		//		}
					Scrolled();
					break ;
				case 5:
					base.WndProc(ref m);
					Scrolled();
					break;
				case 0x4E:
					base.WndProc(ref m);
					Win32.NMHDR nh = (Win32.NMHDR)Marshal.PtrToStructure(m.LParam, typeof(Win32.NMHDR));
			//		if (nh.code == -310)
			//		{
			//			if ( this.View == View.Details && this.Columns.Count > 0 )
			//			{
			//				foreach (ColumnHeader h in LockedHeaders)
			//				{
			//					h.Width = 20;
			//				}
			//			}
			//		}
					if (nh.code == -310 ||
						nh.code == -300 ||
						nh.code == -320)
						Scrolled();
					break;
				case 0x002B://WM_DRAWITEM
					//Get the DRAWITEMSTRUCT from the LParam of the message
					Win32.DRAWITEMSTRUCT dis = (Win32.DRAWITEMSTRUCT)Marshal.PtrToStructure(
						m.LParam,typeof(Win32.DRAWITEMSTRUCT));
					//Check if this message comes from the header
					if(dis.ctrlType == 100)//ODT_HEADER - it does come from the header
					{
						//Get the graphics from the hdc field of the DRAWITEMSTRUCT
						Graphics g = Graphics.FromHdc(dis.hdc);
						//Create a rectangle from the RECT struct
						Rectangle r = new Rectangle(dis.rcItem.left, dis.rcItem.top, dis.rcItem.right -
							dis.rcItem.left, dis.rcItem.bottom - dis.rcItem.top);

						//Create new DrawItemState in its default state					
						DrawItemState d = DrawItemState.Default;
						//Set the correct state for drawing
						if(dis.itemState == 0x0001)
							d = DrawItemState.Selected;
						//Create the DrawItemEventArgs object
						DrawItemEventArgs e = new DrawItemEventArgs(g,this.Font,r,dis.itemID,d);
						//If we have a handler attached call it and we don't want the default drawing
						OverrideDrawColumn(this.Columns[dis.itemID], e);
						//Release the graphics object					
						g.Dispose();					
					}
					break;
				default:
					base.WndProc(ref m);
					break;
			}
		}

		void OverrideDrawColumn(object sender, DrawItemEventArgs e)
		{
			ColumnHeader mc = sender as ColumnHeader;
			ObjectInfoArgs a = new ObjectInfoArgs(new GraphicsCache(e.Graphics), e.Bounds, ObjectState.Normal);
			lookAndFeel.Painter.FooterPanel.DrawObject(a);

			string s = mc.Text;

			if (e.Graphics.MeasureString(s, this.Font).Width > mc.Width-8)
			{
				s = mc.Text;
				for (int x=s.Length; x>=0; x--)
				{
					if (e.Graphics.MeasureString(s.Substring(0, x)+"...", this.Font).Width < mc.Width-8)
					{
						s = s.Substring(0, x)+"...";
						break;
					}
					else if (x==0)
					{
						s = "";
					}
				}
			}
			e.Graphics.DrawString(s,this.Font,Brushes.Black,e.Bounds.X + 4, e.Bounds.Y + 2);
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
			components = new System.ComponentModel.Container();
		}
		#endregion


		#region HeaderEventArgs class

		public class HeaderEventArgs : EventArgs
		{
			int columnIndex;
			int mouseButton;
			public HeaderEventArgs(int index, int button)
			{
				columnIndex = index;
				mouseButton = button;
			}
			public int ColumnIndex
			{
				get{return columnIndex;}
			}
			public int MouseButton
			{
				get{return mouseButton;}
			}
		}

		#endregion

		#region DrawHeaderEventArgs class

		public class DrawHeaderEventArgs : EventArgs
		{
			Graphics graphics;
			Rectangle bounds;
			int height;
			public DrawHeaderEventArgs(Graphics dc, Rectangle rect, int h)
			{
				graphics = dc;
				bounds = rect;
				height = h;
			}
			public Graphics Graphics
			{
				get{return graphics;}
			}		
			public Rectangle Bounds
			{
				get{return bounds;}
			}
			public int HeaderHeight
			{
				get{return height;}
			}
		}

		#endregion

	}
}
