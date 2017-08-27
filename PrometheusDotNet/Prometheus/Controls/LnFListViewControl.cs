using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Prometheus.Controls
{
	/// <summary>
	/// Summary description for LnFListViewControl.
	/// </summary>
	public class LnFListViewControl : DevExpress.XtraEditors.XtraUserControl
	{
		public DevExpress.XtraEditors.PanelControl pnlBorder;
		public DevExpress.XtraEditors.VScrollBar vScroll;
		public DevExpress.XtraEditors.HScrollBar hScroll;
		private LnFListView listView;
		//private UserLookAndFeel lookAndFeel;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#region Events
		public event LnFListView.VoidEventHandler CollectionChanged;
		public event ItemCheckEventHandler ItemCheck;
		public event ColumnClickEventHandler ColumnClick;
		public event EventHandler SelectedIndexChanged;
		public event MouseEventHandler ListViewMouseUp;
		#endregion

		#region Imports
		private struct ScrollInfoStruct
		{
			public int cbSize;
			public int fMask;
			public int nMin;
			public int nMax;
			public int nPage;
			public int nPos;
			public int nTrackPos;
		}

/*		private const int WM_HSCROLL = 0x114;
		private const int WM_VSCROLL = 0x115;

		private const int SB_LINELEFT = 0;
		private const int SB_LINERIGHT = 1;
		private const int SB_PAGELEFT = 2;
		private const int SB_PAGERIGHT = 3;
		private const int SB_THUMBPOSITION = 4;
		private const int SB_THUMBTRACK = 5;
		private const int SB_LEFT = 6;
		private const int SB_RIGHT = 7;
		private const int SB_ENDSCROLL = 8;   */

		private const int SIF_TRACKPOS = 0x10;
		private const int SIF_RANGE = 0x1;
		private const int SIF_POS = 0x4;
		private const int SIF_PAGE = 0x2;
		private System.Windows.Forms.Label lblCornerCover;
		private const int SIF_ALL = SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS;

		[DllImport("user32.dll", SetLastError=true) ]
		private static extern int SendMessage (IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
		
		[DllImport("user32.dll", SetLastError=true) ]
		private static extern int GetScrollInfo(
			IntPtr hWnd, int n, ref ScrollInfoStruct lpScrollInfo );

		#endregion

		#region Public Properties
		public Color BorderColor
		{
			get { return this.BackColor; }
			set { this.BackColor = value; }
		}
		public System.Windows.Forms.View View
		{
			get { return listView.View; }
			set { this.listView.View = value; }
		}
		public bool CheckBoxes
		{
			get { return listView.CheckBoxes; }
			set { this.listView.CheckBoxes = value; }
		}
		/*public System.Windows.Forms.ListView.ColumnHeaderCollection Columns
		{
			get { return listView.Columns; }
		}*/

		public ListView.ColumnHeaderCollection Columns
		{
			get { return listView.Columns; }
		}

		public ListView.ListViewItemCollection Items
		{
			get { return listView.Items; }
		}
		public ListView.SelectedListViewItemCollection SelectedItems
		{
			get { return listView.SelectedItems; }
		}

		public ColumnHeader[] LockedHeaders
		{
			get { return listView.LockedHeaders; }
			set { listView.LockedHeaders = value; }
		}

		#endregion

		#region Item Control
		public ListViewItem GetItemAt(int x, int y)
		{
			return listView.GetItemAt(x, y);
		}
		public void ClearItems()
		{
			listView.Items.Clear();
			CollectionChanged();
		}
		public void InsertItem(int index, ListViewItem item)
		{
			listView.Items.Insert(index, item);
			CollectionChanged();
		}
		public void AddItem(ListViewItem item)
		{
			listView.Items.Add(item);
			CollectionChanged();
		}
		public void AddItemRange(ListViewItem[] item)
		{
			listView.Items.AddRange(item);
			CollectionChanged();
		}
		public void RemoveItem(ListViewItem item)
		{
			listView.Items.Remove(item);
			CollectionChanged();
		}
		public void RemoveItemAt(int index)
		{
			listView.Items.RemoveAt(index);
			CollectionChanged();
		}
		#endregion

		#region Column Control
		public void ClearColumns()
		{
			listView.Columns.Clear();
			CollectionChanged();
		}
		public void AddColumn(ColumnHeader item)
		{
			listView.Columns.Add(item);
			CollectionChanged();
		}
		public void AddColumnRange(ColumnHeader[] item)
		{
			listView.Columns.AddRange(item);
			CollectionChanged();
		}
		public void RemoveColumn(ColumnHeader item)
		{
			listView.Columns.Remove(item);
			CollectionChanged();
		}
		public void RemoveColumnAt(int index)
		{
			listView.Columns.RemoveAt(index);
			CollectionChanged();
		}
		#endregion

		public LnFListViewControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			listView  = new LnFListView();
			listView.MouseUp += new MouseEventHandler(OnMouseUp);
			listView.ColumnClick+= new ColumnClickEventHandler(OnColumnClick);
			listView.ItemCheck += new ItemCheckEventHandler(OnItemCheck);
			listView.Location = new Point(2, 2);
			listView.Size = new Size(724, 100);
			listView.Parent = this;
			listView.FullRowSelect = true;
			listView.Anchor = ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right | AnchorStyles.Left)));
			this.pnlBorder.Controls.Add(this.listView);

			CollectionChanged += new LnFListView.VoidEventHandler(OnItemsChanged);
			listView.MouseMove += new MouseEventHandler(OnMouseMove);

			listView.Scrolled += new LnFListView.VoidEventHandler(OnItemsChanged);

			listView.SelectedIndexChanged += new EventHandler(OnSelectedChanged);
		}

		#region Event Handling
		private void OnMouseUp(object sender, MouseEventArgs e)
		{
			if (ListViewMouseUp != null)
				ListViewMouseUp(sender, e);
		}

		private void OnColumnClick(object sender, ColumnClickEventArgs e)
		{
			ColumnClick(sender, e);
		}

		private void OnItemsChanged()
		{
			RefreshScroller();
		}
		private void OnSelectedChanged(object sender, EventArgs e)
		{
			if (SelectedIndexChanged != null)
				SelectedIndexChanged(sender, e);
			RefreshScroller();
		}
		private void OnMouseMove(object sender, MouseEventArgs e)
		{
			//Not Working! :S
			if (e.Button == MouseButtons.Left)
			RefreshScroller();
		}

		private void OnItemCheck(object sender, ItemCheckEventArgs e)
		{
			ItemCheck(sender, e);
		}

		#endregion

		private void RefreshScroller()
		{
			bool hVisible, vVisible;

			//Retrieve scroll info
			ScrollInfoStruct vSI = new ScrollInfoStruct();
			vSI.fMask = SIF_ALL;
			vSI.cbSize = Marshal.SizeOf(vSI);
			GetScrollInfo(listView.Handle, 1, ref vSI);

			ScrollInfoStruct hSI = new ScrollInfoStruct();
			hSI.fMask = SIF_ALL;
			hSI.cbSize = Marshal.SizeOf(hSI);
			GetScrollInfo(listView.Handle, 0, ref hSI);

			//Shall we even bother?
			if (
				hScroll.Value == hSI.nPos && hScroll.Maximum == hSI.nMax &&
				vScroll.Value == vSI.nPos && vScroll.Maximum == vSI.nMax
				)
				return;

			// Vertical Scroll Handling Pwns You!
			vScroll.LargeChange = vSI.nPage;
			vScroll.Minimum = vSI.nMin;
			vScroll.Maximum = vSI.nMax;
			vScroll.Value = vSI.nPos;

			if ((vScroll.Maximum*18+26)-vScroll.Height <= 1 || !vScroll.Enabled)
			{
				vScroll.Visible = false;
				vVisible = false;
			}
			else
			{
				vScroll.Visible = true;
				vVisible = true;
			}


			// Horizontal Scroll Handling Pwns You!
			hScroll.LargeChange = hSI.nPage;
			hScroll.SmallChange = hSI.nPage;
			hScroll.Minimum = hSI.nMin;
			hScroll.Maximum = hSI.nMax;
			hScroll.Value = hSI.nPos;

			if (hScroll.Width-hScroll.Maximum > 0 || !hScroll.Enabled)
			{
				hScroll.Visible = false;
				hVisible = false;
			}
			else
			{
				hScroll.Visible = true;
				hVisible = true;
			}

			//Do we need to resize the scrollbars?


			if (hVisible && vVisible)
			{
				lblCornerCover.BackColor = this.BackColor;
				lblCornerCover.Visible = true;
			}
			else
			{
				lblCornerCover.Visible = false;
			}

			if (hVisible)
			{
				vScroll.Height = this.Height - 4 - hScroll.Height;
				vScroll.Visible = !vScroll.Visible;
				vScroll.Visible = !vScroll.Visible;
			}
			else
			{
				vScroll.Height = this.Height - 4;
			}

			if (vVisible)
			{
				hScroll.Width = this.Width - 4 - vScroll.Width;
				hScroll.Visible = !hScroll.Visible;
				hScroll.Visible = !hScroll.Visible;
			}
			else
			{
				hScroll.Width = this.Width - 4;
			}

		}


		public void FeedBack()
		{
			MessageBox.Show(vScroll.Height.ToString());
			MessageBox.Show(vScroll.Maximum.ToString());
			MessageBox.Show(hScroll.Width.ToString());
			MessageBox.Show(hScroll.Maximum.ToString());
		}

		public void HideScrolls()
		{
			vScroll.Visible = false;
			vScroll.Visible = true;
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
			this.pnlBorder = new DevExpress.XtraEditors.PanelControl();
			this.hScroll = new DevExpress.XtraEditors.HScrollBar();
			this.vScroll = new DevExpress.XtraEditors.VScrollBar();
			this.lblCornerCover = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pnlBorder)).BeginInit();
			this.pnlBorder.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlBorder
			// 
			this.pnlBorder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlBorder.Controls.Add(this.lblCornerCover);
			this.pnlBorder.Controls.Add(this.hScroll);
			this.pnlBorder.Controls.Add(this.vScroll);
			this.pnlBorder.Location = new System.Drawing.Point(0, 0);
			this.pnlBorder.Name = "pnlBorder";
			this.pnlBorder.Size = new System.Drawing.Size(728, 104);
			this.pnlBorder.TabIndex = 0;
			// 
			// hScroll
			// 
			this.hScroll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.hScroll.Location = new System.Drawing.Point(2, 86);
			this.hScroll.Name = "hScroll";
			this.hScroll.Size = new System.Drawing.Size(724, 16);
			this.hScroll.TabIndex = 3;
			this.hScroll.Visible = false;
			this.hScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScroll_Scroll);
			// 
			// vScroll
			// 
			this.vScroll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.vScroll.Location = new System.Drawing.Point(710, 2);
			this.vScroll.Name = "vScroll";
			this.vScroll.Size = new System.Drawing.Size(16, 100);
			this.vScroll.TabIndex = 2;
			this.vScroll.Visible = false;
			this.vScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScroll_Scroll);
			// 
			// lblCornerCover
			// 
			this.lblCornerCover.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCornerCover.BackColor = System.Drawing.Color.Transparent;
			this.lblCornerCover.Location = new System.Drawing.Point(710, 86);
			this.lblCornerCover.Name = "lblCornerCover";
			this.lblCornerCover.Size = new System.Drawing.Size(16, 16);
			this.lblCornerCover.TabIndex = 4;
			this.lblCornerCover.Visible = false;
			// 
			// LnFListViewControl
			// 
			this.Controls.Add(this.pnlBorder);
			this.Name = "LnFListViewControl";
			this.Size = new System.Drawing.Size(728, 104);
			((System.ComponentModel.ISupportInitialize)(this.pnlBorder)).EndInit();
			this.pnlBorder.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void vScroll_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			ScrollInfoStruct sI = new ScrollInfoStruct();
			sI.fMask = SIF_ALL;
			sI.cbSize = Marshal.SizeOf(sI);
			GetScrollInfo(listView.Handle, 1, ref sI);

			if (vScroll.Value != sI.nPos)
			{
				int scrollDifference = vScroll.Value - sI.nPos;
				if (scrollDifference < 0)
				{
					scrollDifference = Math.Abs(scrollDifference);
					for (int x=0; x<scrollDifference; x++)
						SendMessage( listView.Handle,277,(IntPtr) 0,(IntPtr)0 );
				}
				else
				{
					for (int x=0; x<scrollDifference; x++)
						SendMessage( listView.Handle,277,(IntPtr) 1,(IntPtr)0 );
				}
			}
		}

		private void hScroll_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			ScrollInfoStruct sI = new ScrollInfoStruct();
			sI.fMask = SIF_ALL;
			sI.cbSize = Marshal.SizeOf(sI);
			GetScrollInfo(listView.Handle, 0, ref sI);

			if (Math.Abs(hScroll.Value - sI.nPos) >= 6)
			{
				int scrollDifference = (int)Math.Round((double)((hScroll.Value - sI.nPos)/6), 0);
				if (scrollDifference < 0)
				{
					scrollDifference = Math.Abs(scrollDifference);
					for (int x=0; x<scrollDifference; x++)
						SendMessage( listView.Handle,276,(IntPtr) 0,(IntPtr)0 );
					//GetScrollInfo(listView.Handle, 0, ref sI);
					//int y0 = sI.nPos;
					//SendMessage( listView.Handle,276,(IntPtr) 1,(IntPtr)0 );
					//GetScrollInfo(listView.Handle, 0, ref sI);
					//MessageBox.Show((sI.nPos - y0).ToString());

				}
				else
				{
					for (int x=0; x<scrollDifference; x++)
						SendMessage( listView.Handle,276,(IntPtr) 1,(IntPtr)0 );
				}
			}
		}
	}
}

