using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Prometheus.TagEditor;
using Prometheus.TagEditor.Controls;
using TagLibrary.Types;
using UIControls.StandardControls;

namespace TagEditor.Controls
{
	/// <summary>
	/// Summary description for BlockContainer.
	/// </summary>
  public class BlockContainer : Field, IFieldContainer
	{
		private Container components = null;
    private IBlock linkedBlock;
    private ControlListPanel panel = new ControlListPanel();

    private UndoManager undoManager = new UndoManager();

    public UndoManager LinkedUndoManager
    {
      get { return undoManager; }
      set { undoManager = value; }
    }


		public BlockContainer()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
      this.Controls.Add(panel);
      this.Size = new Size(panel.Width, panel.Height);
      //panel.BackColor = Color.Blue;
      panel.Spacing = 2;
      panel.Padding = 7;
      panel.SizeChanged += new EventHandler(panel_SizeChanged);
      panel.ResizeBehavior = ResizeBehavior.Both;
      panel.ControlAdded += new ControlEventHandler(Panel_ControlAdded);
		}

	  private void Panel_ControlAdded(object sender, ControlEventArgs e)
	  {
      //NOTE: For some reason, this is not working at all...
      if (!(e.Control is SectionContainer)) return;
      // Sync the width of all of the child SectionContainers.
      // 1. Find the widest container.
      int width = 0;
      foreach (Control c in this.panel.Controls)
      {
        if (c is SectionContainer)
          if (c.Width > width) width = c.Width;
      }
      // 2. Sync all of the widths.
      foreach (Control c in this.panel.Controls)
      {
        if (c is SectionContainer)
          if (c.Width < width) c.Size = new Size(width, c.Height);
      }
	  }

	  private void panel_SizeChanged(object sender, EventArgs e)
	  {
	    // Update the control's size to match the panel's size.
      this.Size = new Size(panel.Width, panel.Height);
	  }

	  public void DatabindChildrenToBlock(Block.BlockChangedEventArgs e)
    {
      DatabindChildrenToBlock(e.Block);
    }

    public void DatabindChildrenToBlock(IBlock block)
    {
      this.linkedBlock = block;
      foreach (Control c in this.GetChildFields())
      {
        if (block != null)
        {
          c.Enabled = true;
          Field field = (c as Field);
          if (!(c is Block))
          {
            IField binding = (LocateFieldByName(this.linkedBlock, field.BoundPropertyName) as IField);
            field.DataBind(binding);
            Binding[] bindings = field.GetDataBindings();
            foreach (Binding bind in bindings)
            {
              bind.Parse += new ConvertEventHandler(bind_Parse);
            }
          }
          else
          {
            CollectionBase binding = (LocateFieldByName(this.linkedBlock, field.BoundPropertyName) as CollectionBase);
            (c as Block).DataBindCollection(binding);            
            (c as Block).Initialize();
          }
        }
        else
        {
          c.Enabled = false;
        }
      }
    }

	  private void bind_Parse(object sender, ConvertEventArgs e)
    {
      try
      {
        // Get the existing value from the object.
        Binding binding = (sender as Binding);
        string boundProperty = binding.BindingMemberInfo.BindingField;
        object datasource = binding.DataSource;
        Type controlType = datasource.GetType();
        PropertyInfo pi = controlType.GetProperty(boundProperty);
        object oldValue = pi.GetValue(datasource, null);
      
        // Convert to the desired type.
        string[] parts = e.DesiredType.ToString().Split('.');
        if (parts[0] != "System")
          throw new Exception("Cannot convert type '" + e.DesiredType.ToString() + "' - it is not a standard System type.");

        Type t = Type.GetType("System.Convert");
        MethodInfo mi = t.GetMethod("To" + parts[1], new Type[1] { typeof(string) });
        object newValue = mi.Invoke(null, new object[1] { e.Value.ToString() });

        undoManager.AddState(binding.DataSource, boundProperty, oldValue, newValue);
      }
      catch (Exception ex)
      {
        Trace.WriteLine("Could not set undo state for object: " + ex.Message);
      }
    }

    private object LocateFieldByName(object obj, string name)
    {
      string[] parts = name.Split('.');
      string currentObject = parts[0];
      Type objectType = obj.GetType();
      object value = null;
      FieldInfo fi = objectType.GetField(currentObject);
      if (fi != null)
      {
        value = fi.GetValue(obj);
      }
      else
      {
        // This is a property, not a field.
        PropertyInfo pi = objectType.GetProperty(currentObject);
        if (pi != null)
        {
          value = pi.GetValue(obj, null);
        }
        else
        {
          return null;
        }
      }
      if (parts.Length > 1)
      {
        return LocateFieldByName(value, name.Replace(currentObject + ".", ""));
      }
      else
      {
        return value;
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
      // 
      // BlockContainer
      // 
      this.Appearance.BackColor = System.Drawing.Color.Transparent;
      this.Appearance.Options.UseBackColor = true;
      this.Name = "BlockContainer";
      this.Size = new System.Drawing.Size(352, 32);

    }
		#endregion

	  public Field[] GetChildFields(int levels)
	  {
      if (levels < 0) return new Field[0];

      ArrayList fields = new ArrayList();
      foreach (Control c in panel.Controls)
      {
        if (c is Field)
        {
          fields.Add(c);
        }
        else if (c is IFieldContainer)
        {
          fields.AddRange((c as IFieldContainer).GetChildFields(levels-1));
        }
      }
      return (fields.ToArray(typeof(Field)) as Field[]);
	  }

	  public Field[] GetChildFields()
	  {
	    return GetChildFields(1);
	  }

	  public void AddField(Field field)
	  {
	    panel.Controls.Add(field);
	  }

	  public void AddFieldContainer(IFieldContainer container)
	  {
	    panel.Controls.Add(container as Control);
	  }
	}

  public interface IFieldContainer
  {
    Field[] GetChildFields(int levels);
    Field[] GetChildFields();
    void AddField(Field field);
    void AddFieldContainer(IFieldContainer container);
  }
}