using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Prometheus
{
	/// <summary>
	/// Summary description for PaletteSelector.
	/// </summary>
  public class PaletteSelector : DevExpress.XtraEditors.XtraForm
  {
    private DevExpress.XtraEditors.SimpleButton simpleButtonOk;
    private DevExpress.XtraEditors.ListBoxControl listBoxObjectList;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    public int SelectedItemIndex
    {
      get{return(listBoxObjectList.SelectedIndex);}
    }
    public void InitializeList(string[] ObjectList)
    {
      listBoxObjectList.Items.AddRange(ObjectList);
    }
		public PaletteSelector()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
      this.simpleButtonOk = new DevExpress.XtraEditors.SimpleButton();
      this.listBoxObjectList = new DevExpress.XtraEditors.ListBoxControl();
      ((System.ComponentModel.ISupportInitialize)(this.listBoxObjectList)).BeginInit();
      this.SuspendLayout();
      // 
      // simpleButtonOk
      // 
      this.simpleButtonOk.Location = new System.Drawing.Point(8, 152);
      this.simpleButtonOk.Name = "simpleButtonOk";
      this.simpleButtonOk.Size = new System.Drawing.Size(208, 23);
      this.simpleButtonOk.TabIndex = 0;
      this.simpleButtonOk.Text = "Ok";
      this.simpleButtonOk.Click += new System.EventHandler(this.simpleButtonOk_Click);
      // 
      // listBoxObjectList
      // 
      this.listBoxObjectList.ItemHeight = 16;
      this.listBoxObjectList.Location = new System.Drawing.Point(8, 8);
      this.listBoxObjectList.Name = "listBoxObjectList";
      this.listBoxObjectList.Size = new System.Drawing.Size(208, 136);
      this.listBoxObjectList.TabIndex = 1;
      // 
      // PaletteSelector
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
      this.ClientSize = new System.Drawing.Size(224, 182);
      this.Controls.Add(this.listBoxObjectList);
      this.Controls.Add(this.simpleButtonOk);
      this.Name = "PaletteSelector";
      this.Text = "Select Object to Create...";
      ((System.ComponentModel.ISupportInitialize)(this.listBoxObjectList)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

    private void simpleButtonOk_Click(object sender, System.EventArgs e)
    {
      this.Close();
    }
	}
}

