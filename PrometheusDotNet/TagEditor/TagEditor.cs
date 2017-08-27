/* ---------------------------------------------------------------
 * Prometheus
 * Bungie Map-based Multi-Game Editing Kit
 * 2004-2005, Halo-Dev
 * ---------------------------------------------------------------
 * File        : TagEditor.cs 
 * Author      : MonoxideC
 * Co-Authors  : 
 * ---------------------------------------------------------------
 */

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using Prometheus.Core;
using Prometheus.TagEditor.Controls;
using TagEditor.Controls;
using TagLibrary;
using Types = TagLibrary.Types;

namespace Prometheus.TagEditor
{
	/// <summary>
	/// The TagEditor GUI control.
	/// </summary>
	public class TagEditorControl : XtraUserControl, ISupportsUndoRedo
	{
    private XtraTabControl xtraTabControl1;
		private Container components = null;
    private Types.IBlock tagData;
    private string className = "";
    private XmlDocument tagDefinition;
    private Hashtable standardControls = new Hashtable();
    private UndoManager undoManager = new UndoManager();
    private FieldContainerStack containers = new FieldContainerStack();
    private string mainStructName = "";
    private Stack buildDepth = new Stack();

		public TagEditorControl()
		{
			SetStyle(ControlStyles.DoubleBuffer, true);
      SetStyle(ControlStyles.UserPaint, true);
      SetStyle(ControlStyles.AllPaintingInWmPaint, true);

      InitializeComponent();

      xtraTabControl1.Dock = DockStyle.Fill;
      BuildStandardControlTable();
		}

    /// <summary>
    /// Creates the GUI from a tag definition and a loaded tag object.
    /// </summary>
    public void Create(XmlDocument tagDefinitionDocument, Types.IBlock tagData)
    {
      try
      {
        this.tagData = tagData;
        this.tagDefinition = tagDefinitionDocument;
        
        // Check to see if this is a derived type.
        XmlNode nameNode = tagDefinitionDocument.SelectSingleNode("//name");
        string parentClass = nameNode.Attributes["parenttype"].InnerText;
        if ((parentClass != "") && (parentClass != "????"))
        {
          XmlDocument tempDefinitionStorage = this.tagDefinition;
          XmlDocument parentDefinition = TagDefinitionManager.GetTagDefinition(parentClass);
          Create(parentDefinition, this.tagData);
          this.tagDefinition = tempDefinitionStorage;
        }

        mainStructName = nameNode.InnerText;
        XmlNode mainNode = tagDefinition.SelectSingleNode("//struct[@name='" + mainStructName + "']");
        
        // If it's not there, then this object doesn't match the supplied tag definition.
        if (mainNode == null)
          throw new Exception("'" + mainStructName + "' node was not found in the tag definition.");

        className = tagDefinition.SelectSingleNode("//xml/name").InnerText;
        
        buildDepth.Push(0);
        string tabName = mainNode.Attributes["name"].InnerText;
        if (mainNode.Attributes["caption"] != null)
          tabName = mainNode.Attributes["caption"].InnerText;
        CreateTab(tabName);
        BlockContainer container = BuildStruct(mainNode, true);
        container.DatabindChildrenToBlock(this.tagData);
        containers.Peek().AddFieldContainer(container);
        buildDepth.Pop();
      }
      catch (Exception ex)
      {
        throw new Exception("Could not load tag into TagEditor: " + ex.Message);
      }
    }

    public class FieldContainerPanel : Panel, IFieldContainer
    {
      public Field[] GetChildFields(int levels)
      {
        if (levels > 0) return new Field[0];

        ArrayList fields = new ArrayList();
        foreach (Control c in this.Controls)
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
        this.Controls.Add(field);
      }

      public void AddFieldContainer(IFieldContainer container)
      {
        this.Controls.Add(container as Control);
      }
    }

    /// <summary>
    /// Builds a BlockContainer control, populating it with the appropriate controls
    /// based on the supplied XML TDF data.
    /// </summary>
    /// <returns>The BlockContainer that was created.</returns>
    protected BlockContainer BuildStruct(XmlNode node, bool mainBlock)
    {
      int depth = 0;

      // Setup the block container.
      BlockContainer container = new BlockContainer();
      
      container.LinkedUndoManager = this.undoManager;
      containers.Push(container);
      
      XmlNodeList valueNodes = node.SelectNodes("*");
      foreach (XmlNode valueNode in valueNodes)
      {
        if (valueNode.Name.ToLower() == "group")
        {
          // Make sure that this node is a direct child of the Main Struct.
          // That's the only place that groups are allowed to exist.
          bool allowGroup = false;
          if (valueNode.ParentNode.Name.ToLower() == "struct")
          {
            if (valueNode.ParentNode.Attributes["name"] != null)
            {
              if (valueNode.ParentNode.Attributes["name"].InnerText == mainStructName)
              {
                allowGroup = true;
              }
            }
          }
          if (!allowGroup)
            throw new Exception("Unable to create group: Not a direct member of the main struct.");

          buildDepth.Push(0);
          CreateTab(valueNode.Attributes["caption"].InnerText);
          BlockContainer newContainer = BuildStruct(valueNode, mainBlock);
          newContainer.DatabindChildrenToBlock(this.tagData);
          containers.Peek().AddFieldContainer(newContainer);
          containers.Pop();
          buildDepth.Pop();
        }
        else if (valueNode.Name.ToLower() == "section")
        {
          // NOTE: This code is duplicated from the block creation.
          // A seperate method for building sections would really be ideal, but I don't feel
          // like going into that right now.
          IFieldContainer fieldContainer;
          if (((int)buildDepth.Peek()) < 1)
          {
            fieldContainer = new SectionContainer();
            (fieldContainer as SectionContainer).Title = valueNode.Attributes["caption"].InnerText;
            if (valueNode.Attributes["description"] != null)
              (fieldContainer as SectionContainer).Description = valueNode.Attributes["description"].InnerText;
          }
          else
          {
            fieldContainer = new RegionContainer();
            (fieldContainer as RegionContainer).Caption = valueNode.Attributes["caption"].InnerText;
          }
          depth = (int)(buildDepth.Pop());
          buildDepth.Push(depth+1);

          BlockContainer newContainer = BuildStruct(valueNode, mainBlock);
          newContainer.DatabindChildrenToBlock(this.tagData);
          fieldContainer.AddFieldContainer(newContainer);
          containers.Peek().AddFieldContainer(fieldContainer);
        }
        else if (valueNode.Name.ToLower() == "value")
        {
          string valueName = valueNode.Attributes["name"].InnerText;
          Field fieldControl = new Field();
          string valueText = valueNode.Attributes["type"].InnerText;
          string fullPropertyName = (mainBlock ? className + "Values." : "") + Types.GlobalMethods.MakePublicName(valueName);

          if (standardControls.ContainsKey(valueText))
          {
            //TODO: Look into adding the types into the hashtable, rather than using reflection.
            string controlTypeName = "TagEditor.Controls." + valueText;
            Type fieldControlType = Type.GetType(controlTypeName);
            Assembly targetAssembly = Assembly.GetAssembly(fieldControlType);
            fieldControl = (targetAssembly.CreateInstance(controlTypeName) as Field);
            fieldControl.Configure(valueNode);
            if (valueText != "Block")
            {
              containers.Peek().AddField(fieldControl);
            }
            else
            {
              fieldControl = new Block();
              fieldControl.Configure(valueNode);
              (fieldControl as Block).Caption = "Select Block:";
            
              // At this point, we need to recurse and create the sub control for this block.
              // Step 1: Locate the proper struct in the document.
              string structName = valueNode.Attributes["struct"].InnerText;
              XmlNode structNode = tagDefinition.SelectSingleNode("//struct[@name='" + structName + "']");
              
              IFieldContainer fieldContainer;
              if (((int)buildDepth.Peek()) < 1)
              {
                fieldContainer = new SectionContainer();
                (fieldContainer as SectionContainer).Title = Utility.CapitalizeWords(fieldControl.Caption);
                XmlNode descriptionNode = structNode.SelectSingleNode("description");
                if (descriptionNode != null)
                  (fieldContainer as SectionContainer).Description = descriptionNode.InnerText;
              }
              else
              {
                fieldContainer = new RegionContainer();
                (fieldContainer as RegionContainer).Caption = Utility.CapitalizeWords(fieldControl.Caption);
              }
              depth = (int)(buildDepth.Pop());
              buildDepth.Push(depth+1);
            
              // Step 2: Build the BlockContainer and dock it.
              BlockContainer subBlockContainer = BuildStruct(structNode, false);

              fieldContainer.AddField(fieldControl);
              fieldContainer.AddFieldContainer(subBlockContainer);

              containers.Peek().AddFieldContainer(fieldContainer);
              subBlockContainer.Refresh();

              // Step 3: Wire up the BlockChanged event for databinding.
              (fieldControl as Block).BlockChanged += new Block.BlockChangedHandler(subBlockContainer.DatabindChildrenToBlock);
              (fieldControl as Block).Initialize();
            }
          }
          else
          {
            continue;
          }
          fieldControl.BoundPropertyName = fullPropertyName;
        }
      }
      depth = (int)(buildDepth.Pop());
      buildDepth.Push(depth-1);
      containers.Pop();
      return container;
    }

    private void CreateTab(string caption)
    {
      XtraTabPage page = new XtraTabPage(); 
      page.Size = new Size(522, 406);
      page.Text = caption;
      page.Dock = DockStyle.Fill;
      this.xtraTabControl1.Controls.Add(page);
      this.xtraTabControl1.TabPages.Add(page);

      FieldContainerPanel p = new FieldContainerPanel();
      p.AutoScroll = true;
      p.Dock = DockStyle.Fill;
      page.Controls.Add(p);
      containers.Push(p);
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
      this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
      ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
      this.SuspendLayout();
      // 
      // xtraTabControl1
      // 
      this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.xtraTabControl1.HeaderAutoFill = DevExpress.Utils.DefaultBoolean.False;
      this.xtraTabControl1.HeaderButtons = DevExpress.XtraTab.TabButtons.None;
      this.xtraTabControl1.Location = new System.Drawing.Point(8, 8);
      this.xtraTabControl1.MultiLine = DevExpress.Utils.DefaultBoolean.True;
      this.xtraTabControl1.Name = "xtraTabControl1";
      this.xtraTabControl1.Size = new System.Drawing.Size(560, 528);
      this.xtraTabControl1.TabIndex = 0;
      this.xtraTabControl1.Text = "xtraTabControl1";
      // 
      // TagEditorControl
      // 
      this.Controls.Add(this.xtraTabControl1);
      this.DockPadding.All = 8;
      this.Name = "TagEditorControl";
      this.Size = new System.Drawing.Size(576, 544);
      ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

    protected void BuildStandardControlTable()
    {
      standardControls.Clear();
      standardControls.Add("Real", "Real");
      standardControls.Add("FixedLengthString", "FixedLengthString");
      standardControls.Add("RealFraction", "RealFraction");
      standardControls.Add("ShortInteger", "ShortInteger");
      standardControls.Add("CharInteger", "CharInteger");
      standardControls.Add("ShortBlockIndex", "ShortBlockIndex");
      standardControls.Add("RealEulerAngles2D", "RealEulerAngles2D");
      standardControls.Add("RealEulerAngles3D", "RealEulerAngles2D");
      standardControls.Add("RealARGBColor", "RealARGBColor");
      standardControls.Add("TagReference", "TagReference");
      standardControls.Add("Enum", "Enum");
      standardControls.Add("Flags", "Flags");
			standardControls.Add("Angle", "Angle");
			standardControls.Add("AngleBounds", "AngleBounds");
      standardControls.Add("RealPoint3D", "RealPoint3D");
      standardControls.Add("Block", "Block");
    }

	  public void Undo()
	  {
	    undoManager.Undo();
	  }

	  public void Redo()
	  {
	    undoManager.Redo();
	  }
	}

  public class UndoManager
  {
    public const int MaxItems = 20;
    private ArrayList items = new ArrayList();
    private int position = -1;
    public int Position
    {
      get { return position; }
    }
    public void AddState(object sender, string propertyName, object oldValue, object newValue)
    {
      if (position == MaxItems-1)
      {
        items.RemoveAt(0);
        position--;
      }
      if (position < items.Count-1)
      {
        // Drop off the items after this position.
        for (int x=items.Count-1; x>position; x--)
        {
          items.RemoveAt(x);
        }
      }
      ItemState item = new ItemState();
      item.Item = sender;
      item.OldValue = oldValue;
      item.NewValue = newValue;
      item.PropertyName = propertyName;
      if (items.Count > MaxItems-1) items.RemoveAt(0);
      items.Add(item);
      position++;
    }
    public void Undo()
    {
      if (position == -1) return;
      ItemState state = (items[position] as ItemState);
      Type itemType = state.Item.GetType();
      PropertyInfo pi = itemType.GetProperty(state.PropertyName);
      MethodInfo mi = pi.GetSetMethod();
      mi.Invoke(state.Item, new object[1] { state.OldValue });
      position--;
    }
    public void Redo()
    {
      if (position == items.Count-1) return;
      position++;
      ItemState state = (items[position] as ItemState);
      Type itemType = state.Item.GetType();
      PropertyInfo pi = itemType.GetProperty(state.PropertyName);
      MethodInfo mi = pi.GetSetMethod();
      mi.Invoke(state.Item, new object[1] { state.NewValue });
    }
  }

  // TODO: Implement the standard non-generic Stack methods and properties inside
  // a StackBase class, and inherit FieldContainerStack from that.
  public class FieldContainerStack
  {
    protected Stack InnerStack = new Stack();
    public IFieldContainer Peek()
    {
      return (InnerStack.Peek() as IFieldContainer);
    }
    public IFieldContainer Pop()
    {
      return (InnerStack.Pop() as IFieldContainer);
    }
    public void Push(IFieldContainer value)
    {
      InnerStack.Push(value);
    }
  }
  public class ItemState
  {
    public object Item;
    public object OldValue;
    public object NewValue;
    public string PropertyName;
  }

}