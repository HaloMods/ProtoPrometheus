using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Prometheus
{
	/// <summary>
	/// Summary description for ModelToolbox.
	/// </summary>
	public class ModelToolbox : UserControl
	{
    private ComboBox cbAnimationList;
    private Button btnPlay;
    private Button btnStop;
    private Button btnPause;
    private Label label1;
    private Label label2;
    private NumericUpDown numLOD;
    private NumericUpDown numPermutations;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public ModelToolbox()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

      // Setup event handlers
      btnPlay.Click += new EventHandler(btnPlay_Click);
      btnPause.Click += new EventHandler(btnPause_Click);
      btnStop.Click += new EventHandler(btnStop_Click);
		}

	  private void btnStop_Click(object sender, EventArgs e)
	  {
	    if (Stop != null) Stop(this, new EventArgs());
	  }

	  private void btnPause_Click(object sender, EventArgs e)
	  {
	    if (Pause != null) Pause(this, new EventArgs());
	  }

	  private void btnPlay_Click(object sender, EventArgs e)
	  {
	    if (Play != null) Play(this, new EventArgs());
	  }

	  public NumericUpDown LODControl
    {
      get { return numLOD; }
    }
    public ComboBox AnimationList
    {
      get { return cbAnimationList; }
    }
    public event EventHandler Play;
    public event EventHandler Stop;
    public event EventHandler Pause;

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
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ModelToolbox));
      this.cbAnimationList = new System.Windows.Forms.ComboBox();
      this.btnPlay = new System.Windows.Forms.Button();
      this.btnStop = new System.Windows.Forms.Button();
      this.btnPause = new System.Windows.Forms.Button();
      this.numLOD = new System.Windows.Forms.NumericUpDown();
      this.label1 = new System.Windows.Forms.Label();
      this.numPermutations = new System.Windows.Forms.NumericUpDown();
      this.label2 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.numLOD)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.numPermutations)).BeginInit();
      this.SuspendLayout();
      // 
      // cbAnimationList
      // 
      this.cbAnimationList.Location = new System.Drawing.Point(240, 2);
      this.cbAnimationList.Name = "cbAnimationList";
      this.cbAnimationList.Size = new System.Drawing.Size(121, 21);
      this.cbAnimationList.TabIndex = 0;
      // 
      // btnPlay
      // 
      this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnPlay.Image")));
      this.btnPlay.Location = new System.Drawing.Point(160, 0);
      this.btnPlay.Name = "btnPlay";
      this.btnPlay.Size = new System.Drawing.Size(24, 23);
      this.btnPlay.TabIndex = 1;
      // 
      // btnStop
      // 
      this.btnStop.Enabled = false;
      this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
      this.btnStop.Location = new System.Drawing.Point(184, 0);
      this.btnStop.Name = "btnStop";
      this.btnStop.Size = new System.Drawing.Size(24, 23);
      this.btnStop.TabIndex = 2;
      // 
      // btnPause
      // 
      this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnPause.Image = ((System.Drawing.Image)(resources.GetObject("btnPause.Image")));
      this.btnPause.Location = new System.Drawing.Point(208, 0);
      this.btnPause.Name = "btnPause";
      this.btnPause.Size = new System.Drawing.Size(24, 23);
      this.btnPause.TabIndex = 3;
      // 
      // numLOD
      // 
      this.numLOD.Location = new System.Drawing.Point(8, 2);
      this.numLOD.Maximum = new System.Decimal(new int[] {
                                                           5,
                                                           0,
                                                           0,
                                                           0});
      this.numLOD.Name = "numLOD";
      this.numLOD.Size = new System.Drawing.Size(33, 20);
      this.numLOD.TabIndex = 4;
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(41, 5);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(32, 12);
      this.label1.TabIndex = 5;
      this.label1.Text = "LOD";
      // 
      // numPermutations
      // 
      this.numPermutations.Enabled = false;
      this.numPermutations.Location = new System.Drawing.Point(79, 2);
      this.numPermutations.Maximum = new System.Decimal(new int[] {
                                                                    0,
                                                                    0,
                                                                    0,
                                                                    0});
      this.numPermutations.Name = "numPermutations";
      this.numPermutations.Size = new System.Drawing.Size(32, 20);
      this.numPermutations.TabIndex = 6;
      // 
      // label2
      // 
      this.label2.Enabled = false;
      this.label2.Location = new System.Drawing.Point(113, 5);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(40, 14);
      this.label2.TabIndex = 7;
      this.label2.Text = "Perms.";
      // 
      // ModelToolbox
      // 
      this.Controls.Add(this.label2);
      this.Controls.Add(this.numPermutations);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.numLOD);
      this.Controls.Add(this.btnPause);
      this.Controls.Add(this.btnStop);
      this.Controls.Add(this.btnPlay);
      this.Controls.Add(this.cbAnimationList);
      this.Name = "ModelToolbox";
      this.Size = new System.Drawing.Size(368, 24);
      ((System.ComponentModel.ISupportInitialize)(this.numLOD)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.numPermutations)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion
	}
}
