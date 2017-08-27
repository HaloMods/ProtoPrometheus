using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using TagLibrary.Types;
using DevExpress.XtraEditors;

namespace TagEditor.Controls
{
	public class Block : Field
	{
    private Label lblName;
		private IContainer components = null;
    private DevExpress.XtraEditors.SimpleButton btnAdd;
    private DevExpress.XtraEditors.SimpleButton btnInsert;
    private DevExpress.XtraEditors.SimpleButton btnDuplicate;
    private DevExpress.XtraEditors.SimpleButton btnDelete;
    private DevExpress.XtraEditors.SimpleButton btnDeleteAll;
    private DevExpress.XtraEditors.ComboBoxEdit cboBlockList;

    private CollectionBase blockCollection;

    public string Caption
    {
      get { return lblName.Text; }
      set { lblName.Text = value; }
    }

		public Block()
		{
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.BackColor = Color.Transparent;

      // This call is required by the Windows Form Designer.
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
      this.lblName = new System.Windows.Forms.Label();
      this.cboBlockList = new DevExpress.XtraEditors.ComboBoxEdit();
      this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
      this.btnInsert = new DevExpress.XtraEditors.SimpleButton();
      this.btnDuplicate = new DevExpress.XtraEditors.SimpleButton();
      this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
      this.btnDeleteAll = new DevExpress.XtraEditors.SimpleButton();
      ((System.ComponentModel.ISupportInitialize)(this.cboBlockList.Properties)).BeginInit();
      this.SuspendLayout();
      // 
      // lblName
      // 
      this.lblName.Location = new System.Drawing.Point(4, 5);
      this.lblName.Name = "lblName";
      this.lblName.Size = new System.Drawing.Size(128, 16);
      this.lblName.TabIndex = 2;
      this.lblName.Text = "Block";
      this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cboBlockList
      // 
      this.cboBlockList.EditValue = "";
      this.cboBlockList.Location = new System.Drawing.Point(136, 4);
      this.cboBlockList.Name = "cboBlockList";
      // 
      // cboBlockList.Properties
      // 
      this.cboBlockList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                                                                         new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.cboBlockList.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
      this.cboBlockList.Size = new System.Drawing.Size(269, 20);
      this.cboBlockList.TabIndex = 4;
      this.cboBlockList.SelectedIndexChanged += new System.EventHandler(this.cboBlockList_SelectedIndexChanged);
      // 
      // btnAdd
      // 
      this.btnAdd.Location = new System.Drawing.Point(136, 28);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new System.Drawing.Size(40, 23);
      this.btnAdd.TabIndex = 5;
      this.btnAdd.Text = "Add";
      this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
      // 
      // btnInsert
      // 
      this.btnInsert.Location = new System.Drawing.Point(180, 28);
      this.btnInsert.Name = "btnInsert";
      this.btnInsert.Size = new System.Drawing.Size(44, 23);
      this.btnInsert.TabIndex = 6;
      this.btnInsert.Text = "Insert";
      // 
      // btnDuplicate
      // 
      this.btnDuplicate.Location = new System.Drawing.Point(228, 28);
      this.btnDuplicate.Name = "btnDuplicate";
      this.btnDuplicate.Size = new System.Drawing.Size(60, 23);
      this.btnDuplicate.TabIndex = 7;
      this.btnDuplicate.Text = "Duplicate";
      // 
      // btnDelete
      // 
      this.btnDelete.Location = new System.Drawing.Point(292, 28);
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new System.Drawing.Size(48, 23);
      this.btnDelete.TabIndex = 8;
      this.btnDelete.Text = "Delete";
      this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
      // 
      // btnDeleteAll
      // 
      this.btnDeleteAll.Location = new System.Drawing.Point(344, 28);
      this.btnDeleteAll.Name = "btnDeleteAll";
      this.btnDeleteAll.Size = new System.Drawing.Size(60, 23);
      this.btnDeleteAll.TabIndex = 9;
      this.btnDeleteAll.Text = "Delete All";
      this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
      // 
      // Block
      // 
      this.Appearance.BackColor = System.Drawing.Color.Transparent;
      this.Appearance.Options.UseBackColor = true;
      this.Controls.Add(this.btnDeleteAll);
      this.Controls.Add(this.btnDelete);
      this.Controls.Add(this.btnDuplicate);
      this.Controls.Add(this.btnInsert);
      this.Controls.Add(this.btnAdd);
      this.Controls.Add(this.cboBlockList);
      this.Controls.Add(this.lblName);
      this.Name = "Block";
      this.Size = new System.Drawing.Size(412, 56);
      ((System.ComponentModel.ISupportInitialize)(this.cboBlockList.Properties)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

    public event BlockChangedHandler BlockChanged;
    public delegate void BlockChangedHandler(BlockChangedEventArgs e);
    public class BlockChangedEventArgs : EventArgs
    {
      private IBlock block;
      public IBlock Block
      {
        get { return block; }
      }
      public BlockChangedEventArgs(IBlock block)
      {
        this.block = block;
      }
    }


    public override void Configure(System.Xml.XmlNode valueNode)
    {
      base.Configure(valueNode);
      this.lblName.Text = Name;
    }

    public void DataBindCollection(CollectionBase blockCollection)
    {
      this.blockCollection = blockCollection;
      UpdateComboBox();
    }

    public void Initialize()
    {
      SelectBlock(this.cboBlockList.Properties.Items.Count-1);
    }

    protected void SelectBlock(int index)
    {
      if (BlockChanged != null)
      {
        if (index > -1)
        {
          Type type = blockCollection.GetType();
          PropertyInfo pi = type.GetProperty("Item");  // Default name of an indexer is "Item"
          IBlock block = (pi.GetGetMethod().Invoke(blockCollection, new object[1] { index }) as IBlock);
          BlockChanged(new BlockChangedEventArgs(block));
        }
        else
        {
          BlockChanged(new BlockChangedEventArgs(null));
        }
      }
    }

    private void UpdateComboBox()
    {
      UpdateComboBox(0);
    }

    private void UpdateComboBox(int selectedIndex)
    {
      this.cboBlockList.Properties.Items.Clear();
      for (int x=0; x<blockCollection.Count; x++)
      {
        this.cboBlockList.Properties.Items.Add("block " + x.ToString());
      }
      this.cboBlockList.SelectedIndex = selectedIndex;

      if (blockCollection.Count > 0)
      {
        this.cboBlockList.Enabled = true;
      }
      else
      {
        this.cboBlockList.Enabled = false;
        SelectBlock(-1);
      }
      this.btnDelete.Enabled = (blockCollection.Count > 0);
      this.btnDeleteAll.Enabled = (blockCollection.Count > 0);
      this.btnInsert.Enabled = (blockCollection.Count > 0);
      this.btnDuplicate.Enabled = (blockCollection.Count > 0);
      // TODO: Disable Adding blocks if the count is greater than the max block count.

      if (this.cboBlockList.SelectedIndex == -1)
        this.cboBlockList.SelectedText = "";
    }

    private void btnAdd_Click(object sender, System.EventArgs e)
    {
      // Use reflection to invoke the AddNew() method of this collection.
      Type type = blockCollection.GetType();
      MethodInfo mi = type.GetMethod("AddNew");
      mi.Invoke(blockCollection, null);
      UpdateComboBox(blockCollection.Count-1);
    }

    private void cboBlockList_SelectedIndexChanged(object sender, System.EventArgs e)
    {
      // Update to the new block.
      SelectBlock(cboBlockList.SelectedIndex);
    }

    private void btnDelete_Click(object sender, System.EventArgs e)
    {
      blockCollection.RemoveAt(this.cboBlockList.SelectedIndex);
      int newIndex = this.cboBlockList.SelectedIndex-1;
      if ((newIndex < 0) && (blockCollection.Count > 0))
        newIndex = 0;
      UpdateComboBox(newIndex);
    }

    private void btnDeleteAll_Click(object sender, System.EventArgs e)
    {
      blockCollection.Clear();
      UpdateComboBox(-1);
    }
	}
}

