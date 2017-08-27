using System;
using System.Collections;

namespace TagEditor
{
	/// <summary>
	/// Summary description for NotesDialog.
	/// </summary>
	public class NotesDialog : CollectionBase
	{
		public string SectionName
		{
			get { return sectionName; }
			set { sectionName = value; }
		}
		string sectionName = "";

		public int SelectedIndex
		{
			get { return selectedIndex; }
			set { selectedIndex = value; if (SelectedChanged != null) SelectedChanged(value); }
		}
		int selectedIndex = -1;

		public delegate void NotesEventDelegate(int index);
		public event NotesEventDelegate New;
		public event NotesEventDelegate Delete;
		public event NotesEventDelegate NoteChanged;
		public event NotesEventDelegate SelectedChanged;

		public void SaveNote(string name, string description)
		{
			this[selectedIndex].Name = name;
			this[selectedIndex].Description = description;
			this[selectedIndex].Modified = false;
			if (NoteChanged != null) NoteChanged(selectedIndex);
		}

		public void SetModified()
		{
			this[selectedIndex].Modified = true;
			if (NoteChanged != null) NoteChanged(selectedIndex);
		}

		public void Add(Note n)
		{
			InnerList.Add(n);
			selectedIndex = InnerList.Count-1;
			if (New != null) New(selectedIndex);
			if (SelectedChanged != null) SelectedChanged(selectedIndex);
		}

		public void Remove()
		{
			int i = selectedIndex;
			SelectedIndex = -1;
			InnerList.RemoveAt(i);
			if (Delete != null) Delete(i);
		}

		public Note this[int x]
		{
			get { return InnerList[x] as Note; }
		}

		public void Remove(Note note)
		{
			for (int x=0; x<InnerList.Count; x++)
			{
				Note n = InnerList[x] as Note;
				if (n == note)
				{
					RemoveAt(x);
					return;
				}
			}
		}

		public NotesDialog()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}

	public class Note
	{
		public Note(string name, string description, bool modified)
		{
			this.name = name;
			this.description = description;
			this.modified = modified;
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public string Description
		{
			get { return description; }
			set { description = value; }
		}

		public bool Modified
		{
			get { return modified; }
			set { modified = value; }
		}

		string name;
		string description;
		bool modified = false;

		public override string ToString()
		{
			return (Modified ? "* " : "") +name;
		}
	}
}
