using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using Prometheus.Core.Project;

namespace Prometheus.Controls
{
	/// <summary>
	/// Summary description for TaskListGUI.
	/// </summary>
	public class TaskListGUI : LnFListViewControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label lblCoverItem;
		private DevExpress.XtraEditors.TextEdit txtNewItemDescription;
		private DevExpress.XtraBars.BarManager barmRightClick;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraBars.PopupMenu ppRightClick;
		private Prometheus.Core.Project.TaskList m_taskList = null;

		#region Public Properties
		public Prometheus.Core.Project.TaskList TaskList
		{
			get { return m_taskList; }
			set
			{
				m_taskList = value;
				RefreshTasks();
				m_taskList.ListChanged += new TaskList.TaskListEventDelegate(OnTaskListChanged);
				m_taskList.TaskChanged += new TaskList.TaskListEventDelegate(OnTaskChanged);

			}
		}
		#endregion

		#region Overriden Methods
		public new void ClearItems()
		{
			base.ClearItems();
			AddItem(new ListViewItem("null::"));
		}

		#endregion

		public TaskListGUI()
		{
			InitializeComponent();
			this.ItemCheck += new ItemCheckEventHandler(OnItemChecked);
			this.SizeChanged += new EventHandler(SizeChangedHandler);
			this.ColumnClick += new ColumnClickEventHandler(OnColumnClick);
			this.ListViewMouseUp += new MouseEventHandler(OnListViewClick);
			// This call is required by the Windows.Forms Form Designer.
			this.View = View.Details;
			this.CheckBoxes = true;
			RefreshColumns();
		}

		#region Event Handling
		private void OnListViewClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				ppRightClick.ItemLinks.Clear();
				if (GetItemAt(e.X,  e.Y) == null)
					return;

				if (SelectedItems.Count == 0)
					return;

				foreach (ListViewItem item in SelectedItems)
				{
					try
					{
						if ((item.Tag as Task).Type != TaskList.TaskType.Task)
							return;
					}
					catch
					{
						return;
					}
				}
			
				PopulateRightClick();
			}
		}
		private void PopulateRightClick()
		{
			//MessageBox.Show("yo");
			BarButtonItem b = new BarButtonItem();
			b.Caption = "Priority 1";
			b.ItemClick += new ItemClickEventHandler(OnSetPriority1);
			this.ppRightClick.ItemLinks.Add(b);

			b = new BarButtonItem();
			b.Caption = "Priority 2";
			b.ItemClick += new ItemClickEventHandler(OnSetPriority2);
			this.ppRightClick.ItemLinks.Add(b);

			b = new BarButtonItem();
			b.Caption = "Priority 3";
			b.ItemClick += new ItemClickEventHandler(OnSetPriority3);
			this.ppRightClick.ItemLinks.Add(b);

			b = new BarButtonItem();
			b.Caption = "Remove Task";
			b.ItemClick += new ItemClickEventHandler(OnRemoveItemClick);
			this.ppRightClick.ItemLinks.Add(b);
		}
		
		private void OnRemoveItemClick(object sender, ItemClickEventArgs e)
		{
			foreach (ListViewItem item in SelectedItems)
			{
				m_taskList.Remove(item.Tag as Task);
			}
		}
		#region Priorities
		private void OnSetPriority1(object sender, ItemClickEventArgs e)
		{
			foreach (ListViewItem item in SelectedItems)
			{
				(item.Tag as Task).Priority = 1;
			}
		}
		private void OnSetPriority2(object sender, ItemClickEventArgs e)
		{
			foreach (ListViewItem item in SelectedItems)
			{
				(item.Tag as Task).Priority = 2;
			}
		}
		private void OnSetPriority3(object sender, ItemClickEventArgs e)
		{
			foreach (ListViewItem item in SelectedItems)
			{
				(item.Tag as Task).Priority = 3;
			}
		}
		#endregion
		private void OnColumnClick(object sender, ColumnClickEventArgs e)
		{
			string[] s = new string[m_taskList.Count];

			if (e.Column == 1)
			{
				for (int x=0; x<m_taskList.Count; x++)
				{
					s[x] = m_taskList[x].Priority.ToString();
				}
			}
			else if (e.Column == 2)
			{
				for (int x=0; x<m_taskList.Count; x++)
				{
					if (m_taskList[x].Type == TaskList.TaskType.Task)
						s[x] = "0"+m_taskList[x].Description.ToLower();
					else
						s[x] = "1"+m_taskList[x].Description.ToLower();
				}
			}
			else if (e.Column == 3)
			{
				for (int x=0; x<m_taskList.Count; x++)
				{
					s[x] = m_taskList[x].Description.ToLower();
				}
			}
			else if (e.Column == 4)
			{
				for (int x=0; x<m_taskList.Count; x++)
				{
					s[x] = m_taskList[x].Description.ToLower();
				}
			}
			else
			{
				return;
			}

			Task[] t = m_taskList.GetTaskArray();
			Array.Sort(s, t);

			if (t[0] == m_taskList.GetTaskArray()[0])
			{
				Array.Reverse(t);
			}
			
			m_taskList.SetTaskArray(t);
			this.RefreshTasks();
		}

		private void OnItemChecked(object sender, ItemCheckEventArgs e)
		{
			if (e.NewValue == CheckState.Checked)
			{
				m_taskList[e.Index-1].Active = false;
				Items[e.Index].Font = new Font("Arial", 8, FontStyle.Strikeout);
			}
			else if (e.NewValue == CheckState.Unchecked)
			{
				m_taskList[e.Index-1].Active = true;
				Items[e.Index].Font = new Font("Arial", 8);
			}
		}

		private void OnTaskListChanged(object sender, TaskList.TaskListEventArgs e)
		{
			if (e.Action == TaskList.TaskListAction.Add)
			{
				InsertItem(1, TaskToListViewItem(m_taskList[0]));
			}
			else
			{
				RemoveItemAt(e.TaskIndex+1);
			}
		}

		private void OnTaskChanged(object sender, TaskList.TaskListEventArgs e)
		{
			if (e.Action == TaskList.TaskListAction.ActiveChanged)
				return;
			Items[e.TaskIndex+1] = TaskToListViewItem(m_taskList[e.TaskIndex]);
		}

		public void SizeChangedHandler(object sender, EventArgs e)
		{
			RefreshColumns();
		}
		#endregion

		public void RefreshColumns()
		{
			ClearColumns();

			int extra = 0;
			if (vScroll.Visible)
				extra = vScroll.Width + 1;

			ColumnHeader[] columns = new ColumnHeader[5];
			columns[0] = new ColumnHeader();
			columns[0].Text = "";
			columns[0].Width = 20;

			columns[1] = new ColumnHeader();
			columns[1].Text = " !";
			columns[1].Width = 20;

			this.LockedHeaders = new ColumnHeader[2];
			this.LockedHeaders[0] = columns[0];
			this.LockedHeaders[1] = columns[1];

			columns[2] = new ColumnHeader();
			columns[2].Text = "Description";
			columns[2].Width = (this.Width - columns[0].Width - columns[1].Width - extra)/2;

			columns[3] = new ColumnHeader();
			columns[3].Text = "Tag";
			columns[3].Width = (this.Width - columns[0].Width - columns[1].Width - extra)/4;

			columns[4] = new ColumnHeader();
			columns[4].Text = "Section";
			columns[4].Width = ((this.Width - columns[0].Width - columns[1].Width - extra)/4)-4;

			/*MessageBox.Show(this.Width.ToString());
			MessageBox.Show((this.Width - columns[0].Width).ToString());
			MessageBox.Show((columns[0].Width + columns[1].Width + columns[2].Width).ToString());*/

			AddColumnRange(columns);
		}

		public void RefreshTasks()
		{
			ClearItems();
			foreach (Task t in m_taskList)
			{
				AddItem(TaskToListViewItem(t));
			}
		}

		private ListViewItem TaskToListViewItem(Task t)
		{
			ListViewItem item = new ListViewItem();
			item.Tag = t;
			item.Text = "";
			item.Checked = !t.Active;
			
			//Strikeout or not?
			Font f;

			if (t.Active)
			{
				f = new Font("Arial", 8);
			}
			else
			{
				f = new Font("Arial", 8, FontStyle.Strikeout);
			}

			item.Font = f;

			//Sort out the rest of the item
			ListViewItem.ListViewSubItem[] subItems = new ListViewItem.ListViewSubItem[4];

			string displayDescription;
			switch(t.Type) //Determine the display of the 'Description' field
			{
				case TaskList.TaskType.Task:
					displayDescription = t.Description;
					break;
				case TaskList.TaskType.Warning:
					displayDescription = "Warning: "+t.Description;
					break;
				case TaskList.TaskType.Error:
					displayDescription = "Error: "+t.Description;
					break;
				default:
					displayDescription = "";
					break;
			}
			subItems[0] = new ListViewItem.ListViewSubItem(item, t.Priority.ToString()); //Priority
			subItems[1] = new ListViewItem.ListViewSubItem(item, displayDescription); //Description
			subItems[2] = new ListViewItem.ListViewSubItem(item, t.TagRef); //Tag Reference
			subItems[3] = new ListViewItem.ListViewSubItem(item, t.Section); //Section

			item.SubItems.AddRange(subItems);

			item.Tag = t;

			return item;
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

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblCoverItem = new System.Windows.Forms.Label();
			this.txtNewItemDescription = new DevExpress.XtraEditors.TextEdit();
			this.ppRightClick = new DevExpress.XtraBars.PopupMenu();
			this.barmRightClick = new DevExpress.XtraBars.BarManager();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			((System.ComponentModel.ISupportInitialize)(this.pnlBorder)).BeginInit();
			this.pnlBorder.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtNewItemDescription.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ppRightClick)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barmRightClick)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlBorder
			// 
			this.pnlBorder.Controls.Add(this.txtNewItemDescription);
			this.pnlBorder.Controls.Add(this.lblCoverItem);
			this.pnlBorder.Name = "pnlBorder";
			this.pnlBorder.Controls.SetChildIndex(this.lblCoverItem, 0);
			this.pnlBorder.Controls.SetChildIndex(this.vScroll, 0);
			this.pnlBorder.Controls.SetChildIndex(this.hScroll, 0);
			this.pnlBorder.Controls.SetChildIndex(this.txtNewItemDescription, 0);
			// 
			// vScroll
			// 
			this.vScroll.Name = "vScroll";
			// 
			// hScroll
			// 
			this.hScroll.Name = "hScroll";
			// 
			// lblCoverItem
			// 
			this.lblCoverItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblCoverItem.BackColor = System.Drawing.Color.White;
			this.lblCoverItem.Location = new System.Drawing.Point(2, 22);
			this.lblCoverItem.Name = "lblCoverItem";
			this.lblCoverItem.Size = new System.Drawing.Size(724, 18);
			this.lblCoverItem.TabIndex = 4;
			this.lblCoverItem.Text = "               Click here to add a new task";
			this.lblCoverItem.Click += new System.EventHandler(this.lblCoverItem_Click);
			// 
			// txtNewItemDescription
			// 
			this.txtNewItemDescription.Location = new System.Drawing.Point(30, 20);
			this.txtNewItemDescription.Name = "txtNewItemDescription";
			// 
			// txtNewItemDescription.Properties
			// 
			this.txtNewItemDescription.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.txtNewItemDescription.Size = new System.Drawing.Size(75, 18);
			this.txtNewItemDescription.TabIndex = 5;
			this.txtNewItemDescription.Visible = false;
			// 
			// ppRightClick
			// 
			this.ppRightClick.Manager = this.barmRightClick;
			this.ppRightClick.Name = "ppRightClick";
			// 
			// barmRightClick
			// 
			this.barmRightClick.DockControls.Add(this.barDockControlTop);
			this.barmRightClick.DockControls.Add(this.barDockControlBottom);
			this.barmRightClick.DockControls.Add(this.barDockControlLeft);
			this.barmRightClick.DockControls.Add(this.barDockControlRight);
			this.barmRightClick.Form = this;
			this.barmRightClick.MaxItemId = 0;
			// 
			// TaskListGUI
			// 
			this.BorderColor = System.Drawing.Color.FromArgb(((System.Byte)(247)), ((System.Byte)(245)), ((System.Byte)(241)));
			this.Controls.Add(this.barDockControlTop);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Name = "TaskListGUI";
			this.barmRightClick.SetPopupContextMenu(this, this.ppRightClick);
			this.Controls.SetChildIndex(this.barDockControlRight, 0);
			this.Controls.SetChildIndex(this.barDockControlLeft, 0);
			this.Controls.SetChildIndex(this.barDockControlBottom, 0);
			this.Controls.SetChildIndex(this.barDockControlTop, 0);
			this.Controls.SetChildIndex(this.pnlBorder, 0);
			((System.ComponentModel.ISupportInitialize)(this.pnlBorder)).EndInit();
			this.pnlBorder.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.txtNewItemDescription.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ppRightClick)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barmRightClick)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void lblCoverItem_Click(object sender, System.EventArgs e)
		{
			this.txtNewItemDescription.Location = new Point(Columns[0].Width+Columns[1].Width+8, 20);
			this.txtNewItemDescription.Size = new System.Drawing.Size(500, 18);
			this.txtNewItemDescription.Visible = true;
			this.txtNewItemDescription.Focus();
			this.txtNewItemDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewItemDescription_KeyDown);
			this.txtNewItemDescription.LostFocus += new System.EventHandler(this.txtNewItemDescription_LostFocus);
		}

		private void txtNewItemDescription_LostFocus(object sender, System.EventArgs e)
		{
			CreateNewItemFromInput();
		}

		private void txtNewItemDescription_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch(e.KeyCode)
			{
				case Keys.Enter:
					CreateNewItemFromInput();
					break;
				case Keys.Escape:
					ResetInputBox();
					break;
			}
		}

		private void CreateNewItemFromInput()
		{
			if (txtNewItemDescription.Text != "")
				m_taskList.Add(TaskList.TaskType.Task, txtNewItemDescription.Text,  "", "", true);
			ResetInputBox();
		}

		private void ResetInputBox()
		{
			txtNewItemDescription.Visible = false;
			txtNewItemDescription.Text = "";
			txtNewItemDescription.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.txtNewItemDescription_KeyDown);
			txtNewItemDescription.LostFocus -= new System.EventHandler(this.txtNewItemDescription_LostFocus);
		}


	}
}

