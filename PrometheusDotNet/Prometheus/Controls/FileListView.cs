using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using SharedControls;

namespace Prometheus.Controls
{
	/// <summary>
	/// Summary description for FilListView.
	/// </summary>
	public class FileListView : BetterListView, IShowFiles
	{
		public FileListView()
		{
		}

    public void UpdateList(string[] files)
    {
      this.Clear();
      foreach (string s in files)
      {
        AddFileToList(s);
      }
    } 

    public void AddFileToList(string path)
	  {
      string filename = Path.GetFileName(path);
      ListViewItem i = new ListViewItem(filename);
      i.ImageIndex = 0;
      i.Tag = path;
      //this.Items.Add(i); 
	  }
	}
}
