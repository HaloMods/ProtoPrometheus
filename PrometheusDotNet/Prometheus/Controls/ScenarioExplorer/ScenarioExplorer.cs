using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.DirectX;
using Prometheus.Core.Project;
using Prometheus.Core.Render;

namespace Prometheus.Controls
{
	/// <summary>
	/// Summary description for ScenarioExplorer.
	/// </summary>
	public class ScenarioExplorer : XtraUserControl
	{
    private TreeView treeViewScenarioFolder;

    private TreeNode m_RootNode;
    private string activeFolder;
    private SplitContainerControl splitContainerControl1;
    private ScenarioItems scenarioItems;
		private Container components = null;

    public void CreateImageLists(params string[] imageResources)
    {
      Bitmap[] images = SharedControls.Utility.CreateImagesFromResourcePaths(imageResources);
      ImageList il = SharedControls.Utility.GenerateImageList(images);
      treeViewScenarioFolder.ImageList = il;
    }
    public ScenarioExplorer()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

      CreateImageLists(new string[4] {
                        "Prometheus.Icons.Data_Coll._16.data.png",
                        "Prometheus.Icons.App_Basics._16.folder_closed.png",
                        "Prometheus.Icons.App_Basics._16.folder.png",
                        "Prometheus.Icons.App_Basics._16.document.png"
                      });

      // TODO: Add any initialization after the InitForm call
      m_RootNode = new TreeNode("Mission", 1, 2);

      treeViewScenarioFolder.Nodes.Add(m_RootNode);
      treeViewScenarioFolder.AfterSelect += new TreeViewEventHandler(treeViewScenarioFolder_AfterSelect);
      m_RootNode.Nodes.Add(new TreeNode("Objects", 1, 2));
      
      m_RootNode.Nodes[0].Nodes.Add(new TreeNode("Devices", 1, 2));
      m_RootNode.Nodes[0].Nodes[0].Nodes.Add(new TreeNode("Machines", 1, 2));
      m_RootNode.Nodes[0].Nodes[0].Nodes.Add(new TreeNode("Controls", 1, 2));
      m_RootNode.Nodes[0].Nodes[0].Nodes.Add(new TreeNode("Light Fixtures", 1, 2));
      m_RootNode.Nodes[0].Nodes[0].Nodes.Add(new TreeNode("Device Groups", 1, 2));
      m_RootNode.Nodes[0].Nodes.Add(new TreeNode("Items", 1, 2));
      m_RootNode.Nodes[0].Nodes[1].Nodes.Add(new TreeNode("Equipment", 1, 2));
      m_RootNode.Nodes[0].Nodes[1].Nodes.Add(new TreeNode("Weapons", 1, 2));
      m_RootNode.Nodes[0].Nodes.Add(new TreeNode("Units", 1, 2));
      m_RootNode.Nodes[0].Nodes[2].Nodes.Add(new TreeNode("Bipeds", 1, 2));
      m_RootNode.Nodes[0].Nodes[2].Nodes.Add(new TreeNode("Vehicles", 1, 2));
      m_RootNode.Nodes[0].Nodes.Add(new TreeNode("Scenery", 1, 2));
      m_RootNode.Nodes[0].Nodes.Add(new TreeNode("Sound Scenery", 1, 2));
      
      m_RootNode.Nodes.Add(new TreeNode("Player Starting Points", 1, 2));

      m_RootNode.Nodes.Add(new TreeNode("AI", 1, 2));
      m_RootNode.Nodes[2].Nodes.Add(new TreeNode("Encounters", 1, 2));
      m_RootNode.Nodes[2].Nodes.Add(new TreeNode("Command Lists", 1, 2));
      m_RootNode.Nodes[2].Nodes.Add(new TreeNode("Animations", 1, 2));
      m_RootNode.Nodes[2].Nodes.Add(new TreeNode("Scripts", 1, 2));
      m_RootNode.Nodes[2].Nodes.Add(new TreeNode("Recordings", 1, 2));
      m_RootNode.Nodes[2].Nodes.Add(new TreeNode("Conversations", 1, 2));

      m_RootNode.Nodes.Add(new TreeNode("Comments", 1, 2));

      m_RootNode.Nodes.Add(new TreeNode("Game Data", 1, 2));
      m_RootNode.Nodes[4].Nodes.Add(new TreeNode("Trigger Volumes", 1, 2));
      m_RootNode.Nodes[4].Nodes.Add(new TreeNode("Recorded Animations", 1, 2));
      m_RootNode.Nodes[4].Nodes.Add(new TreeNode("Flags", 1, 2));
      m_RootNode.Nodes[4].Nodes.Add(new TreeNode("Camera Points", 1, 2));
      m_RootNode.Nodes[4].Nodes.Add(new TreeNode("Chapter Titles", 1, 2));
      m_RootNode.Nodes[4].Nodes.Add(new TreeNode("Netgame Flags", 1, 2));
      m_RootNode.Nodes[4].Nodes.Add(new TreeNode("Netgame Equipment", 1, 2));
      m_RootNode.Nodes[4].Nodes.Add(new TreeNode("Starting Profiles", 1, 2));
      m_RootNode.Nodes[4].Nodes.Add(new TreeNode("Detail Objects", 1, 2));
      m_RootNode.Nodes[4].Nodes.Add(new TreeNode("Decals", 1, 2));

      m_RootNode.Nodes.Add(new TreeNode("Cluster Properties", 1, 2));
      m_RootNode.Nodes[5].Nodes.Add(new TreeNode("Palettes", 1, 2));
      m_RootNode.Nodes[5].Nodes[0].Nodes.Add(new TreeNode("Fog Palette", 1, 2));
      m_RootNode.Nodes[5].Nodes[0].Nodes.Add(new TreeNode("Background Sound Palette", 1, 2));
      m_RootNode.Nodes[5].Nodes[0].Nodes.Add(new TreeNode("Sound Environment Palette", 1, 2));
      m_RootNode.Nodes[5].Nodes[0].Nodes.Add(new TreeNode("Weather Palette", 1, 2));
      m_RootNode.Nodes[5].Nodes.Add(new TreeNode("Fog", 1, 2));
      m_RootNode.Nodes[5].Nodes.Add(new TreeNode("Background Sound", 1, 2));
      m_RootNode.Nodes[5].Nodes.Add(new TreeNode("Sound Environment", 1, 2));
      m_RootNode.Nodes[5].Nodes.Add(new TreeNode("Weather", 1, 2));

      scenarioItems.CreateInstance += new ItemClickedEventHandler(scenarioItems_CreateInstance);
      scenarioItems.FocusOnObject += new ItemClickedEventHandler(scenarioItems_FocusOnObject);
    }

	  private void scenarioItems_FocusOnObject(string itemTitle, object innerObject)
	  {
      // Focus the camera on the specified object.
      Instance3D selected_instance = (Instance3D)innerObject;
      Vector3 from = new Vector3();
      from = selected_instance.Translation;
      from.X += 5;
      from.Y += 5;
      from.Z += 3;       
      MdxRender.Camera.SetLookAt(from, selected_instance.Translation);
	  }

	  private void scenarioItems_CreateInstance(string itemTitle, object innerObject)
	  {
	    AddItemToScenario();
	  }

	  public void treeViewScenarioFolder_AfterSelect(object sender, TreeViewEventArgs e)
    {
      ObjectType type = ProjectManager.GetObjectType(e.Node.Text);
      Instance3D[] instance_list = ProjectManager.MapSpawns.GetObjectList(type);
      string[] menu_list = ProjectManager.Menus.GetObjectList(e.Node.Text);
      activeFolder = e.Node.Text;

      scenarioItems.Clear();

      if(menu_list != null)
      {
        string temp;
        string itemName;

        for(int i=0; i<instance_list.Length; i++)
        {
          temp = instance_list[i].ObjectName;
          int n = temp.LastIndexOf('\\');
          int m = temp.LastIndexOf('\\');

          if(m == -1)
            itemName = temp.Substring(n+1, temp.Length - n-1);
          else
            itemName = temp.Substring(m+1, temp.Length - m-1);

          object tag = instance_list[i];
          this.scenarioItems.AddItem(itemName, tag);
        }
      }
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
      this.treeViewScenarioFolder = new System.Windows.Forms.TreeView();
      this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
      this.scenarioItems = new Prometheus.Controls.ScenarioItems();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
      this.splitContainerControl1.SuspendLayout();
      this.SuspendLayout();
      // 
      // treeViewScenarioFolder
      // 
      this.treeViewScenarioFolder.Dock = System.Windows.Forms.DockStyle.Fill;
      this.treeViewScenarioFolder.ImageIndex = -1;
      this.treeViewScenarioFolder.Location = new System.Drawing.Point(8, 8);
      this.treeViewScenarioFolder.Name = "treeViewScenarioFolder";
      this.treeViewScenarioFolder.SelectedImageIndex = -1;
      this.treeViewScenarioFolder.Size = new System.Drawing.Size(280, 362);
      this.treeViewScenarioFolder.TabIndex = 0;
      // 
      // splitContainerControl1
      // 
      this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainerControl1.Horizontal = false;
      this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
      this.splitContainerControl1.Name = "splitContainerControl1";
      this.splitContainerControl1.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
      this.splitContainerControl1.Panel1.Controls.Add(this.treeViewScenarioFolder);
      this.splitContainerControl1.Panel1.DockPadding.All = 8;
      this.splitContainerControl1.Panel1.Text = "splitContainerControl1_Panel1";
      this.splitContainerControl1.Panel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
      this.splitContainerControl1.Panel2.Controls.Add(this.scenarioItems);
      this.splitContainerControl1.Panel2.DockPadding.All = 8;
      this.splitContainerControl1.Panel2.Text = "splitContainerControl1_Panel2";
      this.splitContainerControl1.Size = new System.Drawing.Size(296, 696);
      this.splitContainerControl1.SplitterPosition = 378;
      this.splitContainerControl1.TabIndex = 3;
      this.splitContainerControl1.Text = "splitContainerControl1";
      // 
      // scenarioItems
      // 
      this.scenarioItems.Dock = System.Windows.Forms.DockStyle.Fill;
      this.scenarioItems.Location = new System.Drawing.Point(8, 8);
      this.scenarioItems.Name = "scenarioItems";
      this.scenarioItems.Size = new System.Drawing.Size(280, 298);
      this.scenarioItems.TabIndex = 0;
      // 
      // ScenarioExplorer
      // 
      this.Controls.Add(this.splitContainerControl1);
      this.Name = "ScenarioExplorer";
      this.Size = new System.Drawing.Size(296, 696);
      this.Resize += new System.EventHandler(this.ScenarioExplorer_Resize);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
      this.splitContainerControl1.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion

//    private void treeViewFolderContents_AfterSelect(object sender, TreeViewEventArgs e)
//    {
//      if(e.Node.Tag != null)//root folder clicked :P
//      {
//        Instance3D selected_instance = (Instance3D)e.Node.Tag;
//        ProjectManager.MapSpawns.AddSelection(selected_instance);
//      }
//    }

    private void ScenarioExplorer_Resize(object sender, EventArgs e)
    {
//      Point pta = new Point();
//      Point ptb = new Point();
//      Size sza = new Size();
//      Size szb = new Size();
//
//      int total_height = this.Size.Height - toolBar1.Size.Height;
//      int height1 = (int)(total_height*0.6);
//      int height2 = total_height - height1;
//
//      treeViewScenarioFolder.Height = height1;
//      pta.X = treeViewScenarioFolder.Location.X;
//      pta.Y = treeViewScenarioFolder.Location.Y + height1;
//      
//      toolBar1.Location = pta;
//      pta.Y += toolBar1.Size.Height;
//
//      treeViewFolderContents.Location = pta;
//      treeViewFolderContents.Height = height2 - 20;
    }

    private void AddItemToScenario()
    {
      //new instance
      PaletteSelector dlg = new PaletteSelector();
      string[] menu_list = ProjectManager.Menus.GetObjectList(activeFolder);
      string[] newlist = new string[menu_list.Length - 1];

      for(int i=0; i<menu_list.Length - 1; i++)
      {
        int n = menu_list[i].LastIndexOf("\\");
        if(n != -1)
          newlist[i] = menu_list[i].Substring(n+1, menu_list[i].Length-n-1);
      }
      dlg.InitializeList(newlist);
      dlg.ShowDialog();

      if(dlg.SelectedItemIndex != -1)
      {
        ProjectManager.PerformContextAction(menu_list[dlg.SelectedItemIndex], null);
      }
      Trace.WriteLine("Selected item #" + dlg.SelectedItemIndex.ToString());
    }
  }
}

