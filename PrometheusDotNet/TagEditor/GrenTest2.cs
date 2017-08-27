using System;
using System.Collections;
using TagEditor.Controls;
using DevExpress.XtraTab;

namespace TagEditor
{
  public class ContainerStack
  {
    ArrayList stack = new ArrayList();
    public bool Empty
    {
      get{return(stack.Count == 0);}
    }
    public int Push(object container)
    {
      stack.Add(container);

      return(stack.Count);
    }
    public object Pop()
    {
      object container = null;

      if(Empty == false)
        container = stack[stack.Count - 1];

      return(container);
    }
  }
  /// <summary>
  /// Summary description for GrenTest2.
  /// </summary>
  public class ControlManager
  {
    private XtraTabControl topLevelParent;
    private XtraTabPage activePage;
    private ContainerStack containerStack = new ContainerStack();

    public XtraTabControl TopLevelParent
    {
      get{return topLevelParent;}
      set
      {
        topLevelParent = value;
        activePage = topLevelParent.SelectedTabPage;
      }
    }
    public ControlManager()
    {
    }
    public void GenerateGuiControls()
    {
      //suspend tab layout
      ((System.ComponentModel.ISupportInitialize)(topLevelParent)).BeginInit();
      topLevelParent.SuspendLayout();


      //resume tab layout
      ((System.ComponentModel.ISupportInitialize)(topLevelParent)).EndInit();
      topLevelParent.ResumeLayout(false);
    }
    public void AddContainerToActiveContainer(object NewContainer)
    {
      //figure out where to place the container relative to where we are at currently
    }
    public void AddControlToActiveContainer(Field control)
    {
      //add control into current
    }
    public void AddNewTab(string TabLabel)
    {
      if(containerStack.Empty == false)
      {
        throw new Exception("Started new tab without closing all containers.");
      }
      else
      {
      }
    }
    public void EnterNewSection()
    {
      //create new container control
      object NewSectionControl = new object();
      int embed_level = containerStack.Push(NewSectionControl);
    }
    public void LeaveSection()
    {
      containerStack.Pop();
    }
  }
}
