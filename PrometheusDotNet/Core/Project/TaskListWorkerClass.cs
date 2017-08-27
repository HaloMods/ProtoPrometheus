using System;
using System.Collections;
using System.Xml;

namespace Prometheus.Core.Project
{
	/// <summary>
	/// A strongly typed collection of Task objects with
	/// extra functionality to manage and control user
	/// created tasks and compile warnings and errors
	/// </summary>
	public class TaskList : CollectionBase
	{

		public delegate void TaskListEventDelegate(object sender, TaskListEventArgs e);
		public event TaskListEventDelegate ListChanged;
		public event TaskListEventDelegate TaskChanged;

		public enum TaskType {Task, Warning, Error};
		public enum TaskListAction {Add, Remove, TextChanged, ActiveChanged};

		public class TaskListEventArgs : EventArgs
		{
			public TaskListEventArgs(int taskIndex, TaskListAction action)
			{
				this.taskIndex = taskIndex;
				this.action = action;
			}

			public int TaskIndex
			{
				get { return taskIndex; }
			}

			public TaskListAction Action
			{
				get { return action; }
			}

			private int taskIndex;
			private TaskListAction action;
		}


		#region Task Handling
		public new int Count
		{
			get { return InnerList.Count; }
		}
		public Task[] GetTaskArray()
		{
			Task[] t = new Task[InnerList.Count];
			for (int x=0; x<InnerList.Count; x++)
			{
				t[x] = InnerList[x] as Task;
			}
			return t;
		}
		public void SetTaskArray(Task[] t)
		{
			for (int x=0; x<t.Length; x++)
			{
				InnerList[x] = t[x] as Task;
			}
		}
		public void Add(Task task)
		{
			task.Changed += new Task.TaskEventHandler(OnTaskChanged);
			//InnerList.Add(task);
			InnerList.Insert(0, task);
			if (ListChanged != null)
				ListChanged(this, new TaskListEventArgs(InnerList.Count-1, TaskListAction.Add));
		}
		public void Add(TaskList.TaskType type, string description, string tagRef, string section, bool active)
		{
			Add(new Task(type, description, tagRef, section, active));
		}

		public new void RemoveAt(int index)
		{
			InnerList.RemoveAt(index);
			ListChanged(this, new TaskListEventArgs(index, TaskListAction.Remove));
		}

		public void Remove(Task task)
		{
			for (int x=0; x<InnerList.Count; x++)
			{
				Task t = InnerList[x] as Task;
				if (t == task)
				{
					RemoveAt(x);
					return;
				}
			}
		}

		public Task this[int x]
		{
			get { return (InnerList[x] as Task); }
		}

		public void ClearErrors()
		{
			for (int x=0; x<InnerList.Count; x++)
			{
				if ((InnerList[x] as Task).Type != TaskList.TaskType.Task)
				{
					InnerList.RemoveAt(x);
					x--;
				}
			}
		}
		public void Reset()
		{
			ClearErrors();
			for (int x=0; x<InnerList.Count; x++)
			{
				if (!(InnerList[x] as Task).Active)
				{
					InnerList.RemoveAt(x);
					x--;
				}
			}
		}
		#endregion

		#region Xml
		public void LoadFromXML(XmlNode tasks)
		{
			TaskList.TaskType type = TaskList.TaskType.Task;
			string description = "";
			string tagRef = "";
			string section = "";
			bool active = true;
			int priority = 0;

			foreach (XmlNode task in tasks.ChildNodes)
			{
				switch (task.Attributes.GetNamedItem("type").Value)
				{
					case "Task":
						type = TaskList.TaskType.Task;
						break;
					case "Warning":
						type = TaskList.TaskType.Warning;
						break;
					case "Error":
						type = TaskList.TaskType.Error;
						break;
				}

				priority = Convert.ToInt32(task.Attributes.GetNamedItem("priority").Value);
				
				foreach (XmlNode property in task.ChildNodes)
				{
					if (property.Name == "Description") description = property.Value;
					else if (property.Name == "TagRef") tagRef = property.Value;
					else if (property.Name == "Section") section = property.Value;
					else if (property.Name == "Active") active = XmlConvert.ToBoolean(property.Value);
				}

				Add(type, description, tagRef, section, active);
			}
		}

		public void ExportToXml(XmlTextWriter writer)
		{
			writer.WriteStartElement("Tasks");
			for (int x=InnerList.Count; x>0; x--)
			{
				Task t = InnerList[x] as Task;
				if (t.Type != TaskType.Task) continue; //Only write User Created Tasks
				writer.WriteStartElement("Task");
				writer.WriteAttributeString("type", "Task");
				writer.WriteAttributeString("priority", t.Priority.ToString());
				writer.WriteElementString("Description", t.Description);
				writer.WriteElementString("TagRef", t.TagRef);
				writer.WriteElementString("Section", t.Section);
				writer.WriteElementString("Active", XmlConvert.ToString(t.Active));
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}
		#endregion

		private void OnTaskChanged(Task t, TaskListAction action)
		{
			int x = InnerList.IndexOf(t);
			if (x < 0)
				return;
			if (TaskChanged != null)
				TaskChanged(this, new TaskListEventArgs(x, action));
		}
	}

	/// <summary>
	/// A Task object, either user created, or a compile
	/// warning or error
	/// </summary>
	public class Task
	{

		public delegate void TaskEventHandler(Task sender, TaskList.TaskListAction action);
		public event TaskEventHandler Changed;

		public Task(TaskList.TaskType type, string description, string tagRef, string section, bool active)
		{
			this.type = type;
			this.description = description;
			this.tagRef = tagRef;
			this.section = section;
			this.active = active;

			switch (this.type)
			{
				case TaskList.TaskType.Error:
					this.priority = 1;
					break;
				case TaskList.TaskType.Warning:
					this.priority = 2;
					break;
				case TaskList.TaskType.Task:
					this.priority = 3;
					break;
			}
		}

		public TaskList.TaskType Type
		{
			get { return type; }
		}

		public string Description
		{
			get { return description; }
			set
			{
				description = value;
				if (Changed != null)
					Changed(this, TaskList.TaskListAction.TextChanged);
			}
		}

		public string TagRef
		{
			get { return tagRef; }
			set
			{
				tagRef = value;
				if (Changed != null)
					Changed(this, TaskList.TaskListAction.TextChanged);
			}
		}

		public string Section
		{
			get { return section; }
			set
			{
				section = value;
				if (Changed != null)
					Changed(this, TaskList.TaskListAction.TextChanged);
			}
		}

		public bool Active
		{
			get { return active; }
			set
			{
				active = value;
				if (Changed != null)
					Changed(this, TaskList.TaskListAction.ActiveChanged);
			}
		}

		public int Priority
		{
			get { return priority; }
			set
			{
				priority = value;
				if (Changed != null)
					Changed(this, TaskList.TaskListAction.TextChanged);
			}
		}

		private TaskList.TaskType type;
		private string description;
		private string tagRef;
		private string section;
		private bool active;
		private int priority;

	}

}
