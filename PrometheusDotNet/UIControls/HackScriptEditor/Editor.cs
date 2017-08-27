using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace UIControls.HackScriptEditor
{
	/// <summary>
	/// Summary description for Editor.
	/// </summary>
	public class Editor : DevExpress.XtraEditors.XtraUserControl
	{
		public SyntaxHighlightingTextBox Edit;

		public Editor()
		{
			InitializeComponent();

			Edit.Seperators.Add(' ');
			Edit.Seperators.Add('\r');
			Edit.Seperators.Add('\n');
			Edit.Seperators.Add('.');
			//Edit.Seperators.Add('-');
			//Edit.Seperators.Add('+');
			Edit.Seperators.Add('(');
			Edit.Seperators.Add(')');
			Edit.WordWrap = false;
			Edit.ScrollBars = RichTextBoxScrollBars.Both;

			Edit.HighlightDescriptors.Add(new HighlightDescriptor("/*", "*/", Color.Green, null, DescriptorType.ToCloseToken, DescriptorRecognition.StartsWith, false));
			Edit.HighlightDescriptors.Add(new HighlightDescriptor("//", Color.Green, null, DescriptorType.ToEOL, DescriptorRecognition.StartsWith, false));
			Edit.HighlightDescriptors.Add(new HighlightDescriptor(";", Color.Green, null, DescriptorType.ToEOL, DescriptorRecognition.StartsWith, false));
			Edit.HighlightDescriptors.Add(new HighlightDescriptor("value", Color.Blue, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
			Edit.HighlightDescriptors.Add(new HighlightDescriptor("script", Color.Blue, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Edit = new UIControls.HackScriptEditor.SyntaxHighlightingTextBox();
			this.SuspendLayout();
			// 
			// Edit
			// 
			this.Edit.CaseSensitive = false;
			this.Edit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Edit.FilterAutoComplete = false;
			this.Edit.Location = new System.Drawing.Point(0, 0);
			this.Edit.MaxUndoRedoSteps = 50;
			this.Edit.Name = "Edit";
			this.Edit.Size = new System.Drawing.Size(150, 150);
			this.Edit.TabIndex = 0;
			this.Edit.Text = "";
			// 
			// Editor
			// 
			this.Controls.Add(this.Edit);
			this.Name = "Editor";
			this.Load += new System.EventHandler(this.OnLoad);
			this.ResumeLayout(false);

		}
		#endregion

		private void OnLoad(object sender, System.EventArgs e)
		{
			string file = Application.StartupPath + "\\keywords.txt";

			if( !System.IO.File.Exists(file) ) return;

			System.IO.StreamReader input = new System.IO.StreamReader(file);

			while((file = input.ReadLine()) != null)
				Edit.HighlightDescriptors.Add(new HighlightDescriptor(file, Color.Blue, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
		}
	};
}