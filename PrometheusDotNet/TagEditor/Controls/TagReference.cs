using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraEditors;
using TagLibrary.Types;
using UIControls.StandardControls;
using Prometheus;
using Prometheus.Core;
using Prometheus.Core.Project;
using SharedControls;

namespace TagEditor.Controls
{
	public class TagReference : Field
	{
    private TextEdit txtValue;
    private AutoSizeLabel lblName;
		private IContainer components = null;
		private SimpleButton btnBrowse;

    private TagLibrary.Types.TagReference value;

    public TagLibrary.Types.TagReference Value
    {
      get { return this.value;}
      set { this.value = value; }
    }

		public TagReference()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
      lblName.AutoSize = true;
		}

    public override void Configure(XmlNode valueNode)
    {
      base.Configure(valueNode);
      this.lblName.Text = Caption;
    }

    public override void DataBind(IField value)
    {
      if (!(value is TagLibrary.Types.TagReference))
        throw new Exception("Cannot bind " + value.ToString() + " to TagReference control.");

      this.value = (TagLibrary.Types.TagReference)value;
      
      this.txtValue.DataBindings.Clear();
      this.txtValue.DataBindings.Add(new Binding("Text", this.value, "Value"));
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
			this.txtValue = new DevExpress.XtraEditors.TextEdit();
			this.lblName = new UIControls.StandardControls.AutoSizeLabel();
			this.btnBrowse = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.txtValue.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// txtValue
			// 
			this.txtValue.EditValue = "";
			this.txtValue.Location = new System.Drawing.Point(140, 4);
			this.txtValue.Name = "txtValue";
			this.txtValue.Size = new System.Drawing.Size(236, 20);
			this.txtValue.TabIndex = 3;
			// 
			// lblName
			// 
			this.lblName.AutoSizeHeight = false;
			this.lblName.Location = new System.Drawing.Point(4, 5);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(128, 16);
			this.lblName.TabIndex = 2;
			this.lblName.Text = "TagReference";
			this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnBrowse
			// 
			this.btnBrowse.Location = new System.Drawing.Point(380, 4);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(28, 20);
			this.btnBrowse.TabIndex = 4;
			this.btnBrowse.Text = "...";
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// TagReference
			// 
			this.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.Appearance.Options.UseBackColor = true;
			this.Controls.Add(this.btnBrowse);
			this.Controls.Add(this.txtValue);
			this.Controls.Add(this.lblName);
			this.Name = "TagReference";
			this.Size = new System.Drawing.Size(416, 28);
			((System.ComponentModel.ISupportInitialize)(this.txtValue.Properties)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void btnBrowse_Click(object sender, EventArgs e)
		{
      TagBrowserDialog tbd = null;

      //TODO: fix this so that the edited tag has the version info and we launch the right type of browser
      if(ProjectManager.ProjectLoaded)
			  tbd = new TagBrowserDialog(ProjectManager.Version);
      else
        tbd = new TagBrowserDialog(MapfileVersion.HALOPC);

			//tbd.SelectedTag = this.value;
			tbd.AddFilter("All Files (*.*)|*.*");
			if(tbd.ShowDialog() == DialogResult.Cancel) return;
			this.value.Value = tbd.SelectedTag.PathNoExtension;
		}
	}
}