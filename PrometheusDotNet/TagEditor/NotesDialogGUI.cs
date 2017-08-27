using System;

namespace TagEditor
{
	/// <summary>
	/// Summary description for NotesDialogGUI.
	/// </summary>
	public class NotesDialogGUI : DevExpress.XtraEditors.XtraForm
	{
		private NotesDialog m_notes;
		private DevExpress.XtraBars.BarManager bmanMain;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraBars.Bar bar1;
		private DevExpress.XtraBars.BarStaticItem Status;
		private System.Windows.Forms.Label lblInfo;
		private DevExpress.XtraEditors.GroupControl grpStoredNotes;
		private DevExpress.XtraEditors.SimpleButton btnSave;
		private DevExpress.XtraEditors.GroupControl grpProperties;
		private DevExpress.XtraEditors.ListBoxControl lstStoredNotes;
		private DevExpress.XtraEditors.TextEdit npName;
		private DevExpress.XtraEditors.MemoEdit npDescription;
		private System.Windows.Forms.Label lblName;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private UIControls.StandardControls.ImageButton btnNew;
		private UIControls.StandardControls.ImageButton btnEdit;
		private UIControls.StandardControls.ImageButton btnDelete;

		public enum Access {ReadOnly, ReadWrite};

		public Access CurrentAccess
		{
			get { return currentAccess; }
			set { currentAccess = value; }
		}

		Access currentAccess = Access.ReadOnly;

		public NotesDialogGUI(NotesDialog notes)
		{
			m_notes = notes;

			InitializeComponent();

			lblInfo.Text = "You may view and enter notes regarding "+m_notes.SectionName+" here. "+
			"These notes will be stored with the rest of the tag information and anyone using the "+
			"tag will be able to see the information.";

			this.Text = m_notes.SectionName+" - Notes";

			m_notes.SelectedChanged += new NotesDialog.NotesEventDelegate(m_notes_SelectedChanged);
			m_notes.NoteChanged += new NotesDialog.NotesEventDelegate(m_notes_NoteChanged);
			m_notes.New += new NotesDialog.NotesEventDelegate(m_notes_New);
			m_notes.Delete += new NotesDialog.NotesEventDelegate(m_notes_Delete);
		}

		public void OpenEdit()
		{
			grpProperties.Enabled = true;
			btnSave.Enabled = true;
			CurrentAccess = Access.ReadWrite;
			Status.Caption = "Viewing Note '"+m_notes[m_notes.SelectedIndex].Name+"' in "+(currentAccess == Access.ReadOnly ? "read-only" : "editable")+" mode.";
			btnEdit.Enabled = false;
		}

		public void CloseEdit()
		{
			grpProperties.Enabled = false;
			btnSave.Enabled = false;
			CurrentAccess = Access.ReadOnly;
			try
			{
				m_notes[m_notes.SelectedIndex].Modified = false;
			}
			catch
			{}
			btnSave.Enabled = false;
			btnEdit.Enabled = true;
			this.Refresh();
		}

		public void DisplayNote(Note n)
		{
			grpProperties.Text = n.Name+" Contains:";
			npName.Text = n.Name;
			npDescription.Text = n.Description;
			Status.Caption = "Viewing Note '"+n.Name+"' in "+(currentAccess == Access.ReadOnly ? "read-only" : "editable")+" mode.";
			try
			{
				m_notes[m_notes.SelectedIndex].Modified = false;
			}
			catch
			{}
			btnSave.Enabled = false;
			this.Refresh();
		}

		public override void Refresh()
		{
			/*lstStoredNotes.Items.Clear();
			for (int x=m_notes.Count-1; x>=0; x--)
			{
				lstStoredNotes.Items.Add(m_notes[x]);
			}*/
			lstStoredNotes.Refresh();
		}

		public void ClearProperties()
		{
			CurrentAccess = Access.ReadOnly;

			this.DisplayNote(new Note("", "", false));
			grpProperties.Text = "Note Properties";
			Status.Caption = "No note selected";
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(NotesDialogGUI));
			this.bmanMain = new DevExpress.XtraBars.BarManager();
			this.bar1 = new DevExpress.XtraBars.Bar();
			this.Status = new DevExpress.XtraBars.BarStaticItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.lblInfo = new System.Windows.Forms.Label();
			this.grpStoredNotes = new DevExpress.XtraEditors.GroupControl();
			this.lstStoredNotes = new DevExpress.XtraEditors.ListBoxControl();
			this.btnSave = new DevExpress.XtraEditors.SimpleButton();
			this.grpProperties = new DevExpress.XtraEditors.GroupControl();
			this.lblName = new System.Windows.Forms.Label();
			this.npDescription = new DevExpress.XtraEditors.MemoEdit();
			this.npName = new DevExpress.XtraEditors.TextEdit();
			this.btnNew = new UIControls.StandardControls.ImageButton();
			this.btnEdit = new UIControls.StandardControls.ImageButton();
			this.btnDelete = new UIControls.StandardControls.ImageButton();
			((System.ComponentModel.ISupportInitialize)(this.bmanMain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grpStoredNotes)).BeginInit();
			this.grpStoredNotes.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.lstStoredNotes)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grpProperties)).BeginInit();
			this.grpProperties.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.npDescription.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.npName.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// bmanMain
			// 
			this.bmanMain.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
																																	this.bar1});
			this.bmanMain.DockControls.Add(this.barDockControlTop);
			this.bmanMain.DockControls.Add(this.barDockControlBottom);
			this.bmanMain.DockControls.Add(this.barDockControlLeft);
			this.bmanMain.DockControls.Add(this.barDockControlRight);
			this.bmanMain.Form = this;
			this.bmanMain.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
																																			 this.Status});
			this.bmanMain.MaxItemId = 1;
			this.bmanMain.StatusBar = this.bar1;
			// 
			// bar1
			// 
			this.bar1.BarName = "Custom 1";
			this.bar1.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
			this.bar1.DockCol = 0;
			this.bar1.DockRow = 1;
			this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
			this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
																																											new DevExpress.XtraBars.LinkPersistInfo(this.Status)});
			this.bar1.OptionsBar.AllowQuickCustomization = false;
			this.bar1.OptionsBar.DrawDragBorder = false;
			this.bar1.OptionsBar.UseWholeRow = true;
			this.bar1.Text = "Custom 1";
			// 
			// Status
			// 
			this.Status.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.Status.Caption = "No note selected";
			this.Status.Id = 0;
			this.Status.Name = "Status";
			this.Status.TextAlignment = System.Drawing.StringAlignment.Near;
			// 
			// lblInfo
			// 
			this.lblInfo.Location = new System.Drawing.Point(8, 16);
			this.lblInfo.Name = "lblInfo";
			this.lblInfo.Size = new System.Drawing.Size(464, 32);
			this.lblInfo.TabIndex = 4;
			// 
			// grpStoredNotes
			// 
			this.grpStoredNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.grpStoredNotes.Controls.Add(this.lstStoredNotes);
			this.grpStoredNotes.Location = new System.Drawing.Point(8, 56);
			this.grpStoredNotes.Name = "grpStoredNotes";
			this.grpStoredNotes.Size = new System.Drawing.Size(136, 192);
			this.grpStoredNotes.TabIndex = 5;
			this.grpStoredNotes.Text = "Stored Notes";
			// 
			// lstStoredNotes
			// 
			this.lstStoredNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.lstStoredNotes.ItemHeight = 16;
			this.lstStoredNotes.Location = new System.Drawing.Point(8, 26);
			this.lstStoredNotes.Name = "lstStoredNotes";
			this.lstStoredNotes.Size = new System.Drawing.Size(120, 160);
			this.lstStoredNotes.TabIndex = 0;
			this.lstStoredNotes.SelectedIndexChanged += new System.EventHandler(this.lstStoredNotes_SelectedIndexChanged);
			// 
			// btnSave
			// 
			this.btnSave.Enabled = false;
			this.btnSave.Location = new System.Drawing.Point(152, 208);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(40, 24);
			this.btnSave.TabIndex = 6;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// grpProperties
			// 
			this.grpProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grpProperties.Controls.Add(this.lblName);
			this.grpProperties.Controls.Add(this.npDescription);
			this.grpProperties.Controls.Add(this.npName);
			this.grpProperties.Enabled = false;
			this.grpProperties.Location = new System.Drawing.Point(200, 56);
			this.grpProperties.Name = "grpProperties";
			this.grpProperties.Size = new System.Drawing.Size(272, 192);
			this.grpProperties.TabIndex = 7;
			this.grpProperties.Text = "Note Properties";
			// 
			// lblName
			// 
			this.lblName.Location = new System.Drawing.Point(8, 34);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(48, 16);
			this.lblName.TabIndex = 3;
			this.lblName.Text = "Name:";
			// 
			// npDescription
			// 
			this.npDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.npDescription.EditValue = "";
			this.npDescription.Location = new System.Drawing.Point(6, 66);
			this.npDescription.Name = "npDescription";
			this.npDescription.Size = new System.Drawing.Size(260, 120);
			this.npDescription.TabIndex = 2;
			this.npDescription.EditValueChanged += new System.EventHandler(this.NoteEdited);
			// 
			// npName
			// 
			this.npName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.npName.EditValue = "";
			this.npName.Location = new System.Drawing.Point(54, 34);
			this.npName.Name = "npName";
			this.npName.Size = new System.Drawing.Size(212, 20);
			this.npName.TabIndex = 0;
			this.npName.EditValueChanged += new System.EventHandler(this.NoteEdited);
			// 
			// btnNew
			// 
			this.btnNew.AltImage = null;
			this.btnNew.Appearance.Options.UseBackColor = true;
			this.btnNew.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.btnNew.ForeColor = System.Drawing.Color.Black;
			this.btnNew.HotTrack = true;
			this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
			this.btnNew.ImageSizeRatio = 0.8F;
			this.btnNew.Location = new System.Drawing.Point(152, 72);
			this.btnNew.Name = "btnNew";
			this.btnNew.PressedImage = null;
			this.btnNew.Size = new System.Drawing.Size(40, 40);
			this.btnNew.TabIndex = 11;
			this.btnNew.Toggle = false;
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// btnEdit
			// 
			this.btnEdit.AltImage = null;
			this.btnEdit.Appearance.Options.UseBackColor = true;
			this.btnEdit.Enabled = false;
			this.btnEdit.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.btnEdit.ForeColor = System.Drawing.Color.Black;
			this.btnEdit.HotTrack = true;
			this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
			this.btnEdit.ImageSizeRatio = 0.8F;
			this.btnEdit.Location = new System.Drawing.Point(152, 112);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.PressedImage = null;
			this.btnEdit.Size = new System.Drawing.Size(40, 40);
			this.btnEdit.TabIndex = 12;
			this.btnEdit.Toggle = false;
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.AltImage = null;
			this.btnDelete.Appearance.Options.UseBackColor = true;
			this.btnDelete.Enabled = false;
			this.btnDelete.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.btnDelete.ForeColor = System.Drawing.Color.Black;
			this.btnDelete.HotTrack = true;
			this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
			this.btnDelete.ImageSizeRatio = 0.8F;
			this.btnDelete.Location = new System.Drawing.Point(152, 152);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.PressedImage = null;
			this.btnDelete.Size = new System.Drawing.Size(40, 40);
			this.btnDelete.TabIndex = 13;
			this.btnDelete.Toggle = false;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// NotesDialogGUI
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(480, 278);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.grpProperties);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.grpStoredNotes);
			this.Controls.Add(this.lblInfo);
			this.Controls.Add(this.btnNew);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.LookAndFeel.SkinName = "Liquid Sky";
			this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
			this.LookAndFeel.UseDefaultLookAndFeel = false;
			this.LookAndFeel.UseWindowsXPTheme = false;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NotesDialogGUI";
			this.Text = "Notes";
			((System.ComponentModel.ISupportInitialize)(this.bmanMain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grpStoredNotes)).EndInit();
			this.grpStoredNotes.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.lstStoredNotes)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grpProperties)).EndInit();
			this.grpProperties.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.npDescription.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.npName.Properties)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void btnNew_Click(object sender, System.EventArgs e)
		{
			this.CloseEdit();
			m_notes.Add(new Note("New Note", "", true));
			this.OpenEdit();
		}

		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			this.OpenEdit();
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			m_notes.Remove();
			btnEdit.Enabled = (lstStoredNotes.SelectedIndex != -1);
			btnDelete.Enabled = (lstStoredNotes.SelectedIndex != -1);
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			m_notes.SaveNote(npName.Text, npDescription.Text);
		}

		private void lstStoredNotes_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (lstStoredNotes.SelectedIndex != m_notes.SelectedIndex)
			{
				m_notes.SelectedIndex = lstStoredNotes.SelectedIndex;
			}

			btnEdit.Enabled = (lstStoredNotes.SelectedIndex != -1);
			btnDelete.Enabled = (lstStoredNotes.SelectedIndex != -1);
		}

		private void NoteEdited(object sender, EventArgs e)
		{
			if (CurrentAccess == Access.ReadOnly) return;
			if (npName.Text != m_notes[m_notes.SelectedIndex].Name || npDescription.Text != m_notes[m_notes.SelectedIndex].Description)
			{
				m_notes.SetModified();
				btnSave.Enabled = true;
			}
			this.Refresh();
		}

		private void m_notes_SelectedChanged(int index)
		{
			lstStoredNotes.SelectedIndex = index;
			if (index == -1 || index > m_notes.Count-1)
			{
				ClearProperties();
			}
			else
			{
				this.CloseEdit();
				this.DisplayNote(m_notes[m_notes.SelectedIndex]);
			}

		}

		private void m_notes_NoteChanged(int index)
		{
			lstStoredNotes.Refresh();
			Status.Caption = "Viewing Note '"+m_notes[m_notes.SelectedIndex].Name+"' in "+(currentAccess == Access.ReadOnly ? "read-only" : "editable")+" mode.";
		}

		private void m_notes_New(int index)
		{
			lstStoredNotes.Items.Add(m_notes[index]);
		}

		private void m_notes_Delete(int index)
		{
			this.CloseEdit();
			lstStoredNotes.Items.RemoveAt(index);
		}
		
	}
}

