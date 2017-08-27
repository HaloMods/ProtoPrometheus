using System;
using System.Diagnostics;
using System.Xml;
using TagLibrary.Types;
using System.Collections;


namespace TagEditor
{
  public enum ControlProperty
  {
    ToolTip,
    Item,
    Bit
  }
  public enum TagEditorType
  {
    VariableLengthString,
    StringTable,
    FixedLengthString,
    CharInteger,
    ShortInteger,
    LongInteger,
    Angle,
    Enum,
    Flags,
    Point2D,
    Rectangle2D,
    RealPoint2D,
    RealPoint3D,
    RGBColor,
    ARGBColor,
    Real,
    RealFraction,
    RealVector2D,
    RealVector3D,
    RealQuaternion,
    RealEulerAngles2D,
    RealEulerAngles3D,
    RealPlane2D,
    RealPlane3D,
    RealARGBColor,
    RealRGBColor,
    ShortBounds,
    AngleBounds,
    RealBounds,
    RealFractionBounds,
    TagReference,
    Block,
    ShortBlockIndex,
    LongBlockIndex,
    Data,
    TagDefinition,
    Pad,
    Custom,
    Skip
  };

  /// <summary>
  /// Summary description for GrenTest.
  /// </summary>
  public class GrenTest
  {
    static private Hashtable ControlHashTable = new Hashtable();
    static private string AccessorNamespace;
    static private ControlManager controlManager = null;
    //active tab ref
    //active collapsing panel ref
    public GrenTest()
    {
    }
    static public void Initialize()
    {

      ControlHashTable.Add("VariableLengthString", TagEditorType.VariableLengthString);
      ControlHashTable.Add("StringTable", TagEditorType.StringTable);
      ControlHashTable.Add("FixedLengthString", TagEditorType.FixedLengthString);
      ControlHashTable.Add("CharInteger", TagEditorType.CharInteger);
      ControlHashTable.Add("ShortInteger", TagEditorType.ShortInteger);
      ControlHashTable.Add("LongInteger", TagEditorType.LongInteger);
      ControlHashTable.Add("Angle", TagEditorType.Angle);
      ControlHashTable.Add("Enum", TagEditorType.Enum);
      ControlHashTable.Add("Flags", TagEditorType.Flags);
      ControlHashTable.Add("Point2D", TagEditorType.Point2D);
      ControlHashTable.Add("Rectangle2D", TagEditorType.Rectangle2D);
      ControlHashTable.Add("RealPoint2D", TagEditorType.RealPoint2D);
      ControlHashTable.Add("RealPoint3D", TagEditorType.RealPoint3D);
      ControlHashTable.Add("RGBColor", TagEditorType.RGBColor);
      ControlHashTable.Add("ARGBColor", TagEditorType.ARGBColor);
      ControlHashTable.Add("Real", TagEditorType.Real);
      ControlHashTable.Add("RealFraction", TagEditorType.RealFraction);
      ControlHashTable.Add("RealVector2D", TagEditorType.RealVector2D);
      ControlHashTable.Add("RealVector3D", TagEditorType.RealVector3D);
      ControlHashTable.Add("RealQuaternion", TagEditorType.RealQuaternion);
      ControlHashTable.Add("RealEulerAngles2D", TagEditorType.RealEulerAngles2D);
      ControlHashTable.Add("RealEulerAngles3D", TagEditorType.RealEulerAngles3D);
      ControlHashTable.Add("RealPlane2D", TagEditorType.RealPlane2D);
      ControlHashTable.Add("RealPlane3D", TagEditorType.RealPlane3D);
      ControlHashTable.Add("RealARGBColor", TagEditorType.RealARGBColor);
      ControlHashTable.Add("RealRGBColor", TagEditorType.RealRGBColor);
      ControlHashTable.Add("ShortBounds", TagEditorType.ShortBounds);
      ControlHashTable.Add("AngleBounds", TagEditorType.AngleBounds);
      ControlHashTable.Add("RealBounds", TagEditorType.RealBounds);
      ControlHashTable.Add("RealFractionBounds", TagEditorType.RealFractionBounds);
      ControlHashTable.Add("TagReference", TagEditorType.TagReference);
      ControlHashTable.Add("Block", TagEditorType.Block);
      ControlHashTable.Add("ShortBlockIndex", TagEditorType.ShortBlockIndex);
      ControlHashTable.Add("LongBlockIndex", TagEditorType.LongBlockIndex);
      ControlHashTable.Add("Data", TagEditorType.Data);
      ControlHashTable.Add("TagDefinition", TagEditorType.TagDefinition);
      ControlHashTable.Add("Pad", TagEditorType.Pad);
      ControlHashTable.Add("Custom", TagEditorType.Custom);
      ControlHashTable.Add("Skip", TagEditorType.Skip);

      ControlHashTable.Add("tooltip", ControlProperty.ToolTip);
    }
    static public void GenerateDatabindList(string XmlFilename, ControlManager cm)
    {
      string class_name;
      string platform_name;

      controlManager = cm;

      XmlDocument doc = new XmlDocument();
      doc.Load(XmlFilename);
      XmlNode rootnode = doc.SelectSingleNode("//xml");

      platform_name = (rootnode.SelectSingleNode("platform")).InnerText;
      class_name = (rootnode.SelectSingleNode("name")).InnerText;
      AccessorNamespace = "TagLibrary." + platform_name + "." + class_name + ".";

      ProcessChildNodes(rootnode.SelectSingleNode("//plugin"));
    }
    static public void ProcessChildNodes(XmlNode LocalNode)
    {
      foreach (XmlNode child_node in LocalNode.ChildNodes)
      {
        switch(child_node.Name)
        {
          case "struct":
            ProcessChildNodes(child_node);
            break;
          case "value":
            ProcessControlDefinition(child_node);
            break;
          default:
            Trace.WriteLine("unhandled category:" + child_node.Name);
            break;
        }
      }

      //get control type
      //get property to bind to, "by name?"
      //verify data type matches property
      //get control label
    }

    static public void ProcessControlDefinition(XmlNode ControlNode)
    {
      string data_type = ControlNode.Attributes["type"].InnerText;
      string binding_name = AccessorNamespace + GlobalMethods.MakePublicName(ControlNode.Attributes["name"].InnerText);

      //Switch to the custom creation routine depending on the control desired
      switch((TagEditorType)ControlHashTable[data_type])
      {
        case TagEditorType.VariableLengthString:
          CreateVariableLengthStringControl(ControlNode, binding_name);
          break;
        case TagEditorType.StringTable:
          CreateStringTableControl(ControlNode, binding_name);
          break;
        case TagEditorType.FixedLengthString:
          CreateFixedLengthStringControl(ControlNode, binding_name);
          break;
        case TagEditorType.CharInteger:
          CreateCharIntegerControl(ControlNode, binding_name);
          break;
        case TagEditorType.ShortInteger:
          CreateShortIntegerControl(ControlNode, binding_name);
          break;
        case TagEditorType.LongInteger:
          CreateLongIntegerControl(ControlNode, binding_name);
          break;
        case TagEditorType.Angle:
          CreateAngleControl(ControlNode, binding_name);
          break;
        case TagEditorType.Enum:
          CreateEnumControl(ControlNode, binding_name);
          break;
        case TagEditorType.Flags:
          CreateFlagsControl(ControlNode, binding_name);
          break;
        case TagEditorType.Point2D:
          CreatePoint2DControl(ControlNode, binding_name);
          break;
        case TagEditorType.Rectangle2D:
          CreateRectangle2DControl(ControlNode, binding_name);
          break;
        case TagEditorType.RealPoint2D:
          CreateRealPoint2DControl(ControlNode, binding_name);
          break;
        case TagEditorType.RealPoint3D:
          CreateRealPoint3DControl(ControlNode, binding_name);
          break;
        case TagEditorType.RGBColor:
          CreateRGBColorControl(ControlNode, binding_name);
          break;
        case TagEditorType.ARGBColor:
          CreateARGBColorControl(ControlNode, binding_name);
          break;
        case TagEditorType.Real:
          CreateRealControl(ControlNode, binding_name);
          break;
        case TagEditorType.RealFraction:
          CreateRealFractionControl(ControlNode, binding_name);
          break;
        case TagEditorType.RealVector2D:
          CreateRealVector2DControl(ControlNode, binding_name);
          break;
        case TagEditorType.RealVector3D:
          CreateRealVector3DControl(ControlNode, binding_name);
          break;
        case TagEditorType.RealQuaternion:
          CreateRealQuaternionControl(ControlNode, binding_name);
          break;
        case TagEditorType.RealEulerAngles2D:
          CreateRealEulerAngles2DControl(ControlNode, binding_name);
          break;
        case TagEditorType.RealEulerAngles3D:
          CreateRealEulerAngles3DControl(ControlNode, binding_name);
          break;
        case TagEditorType.RealPlane2D:
          CreateRealPlane2DControl(ControlNode, binding_name);
          break;
        case TagEditorType.RealPlane3D:
          CreateRealPlane3DControl(ControlNode, binding_name);
          break;
        case TagEditorType.RealARGBColor:
          CreateRealARGBColorControl(ControlNode, binding_name);
          break;
        case TagEditorType.RealRGBColor:
          CreateRealRGBColorControl(ControlNode, binding_name);
          break;
        case TagEditorType.ShortBounds:
          CreateShortBoundsControl(ControlNode, binding_name);
          break;
        case TagEditorType.AngleBounds:
          CreateAngleBoundsControl(ControlNode, binding_name);
          break;
        case TagEditorType.RealBounds:
          CreateRealBoundsControl(ControlNode, binding_name);
          break;
        case TagEditorType.RealFractionBounds:
          CreateRealFractionBoundsControl(ControlNode, binding_name);
          break;
        case TagEditorType.TagReference:
          CreateTagReferenceControl(ControlNode, binding_name);
          break;
        case TagEditorType.Block:
          CreateBlockControl(ControlNode, binding_name);
          break;
        case TagEditorType.ShortBlockIndex:
          CreateShortBlockIndexControl(ControlNode, binding_name);
          break;
        case TagEditorType.LongBlockIndex:
          CreateLongBlockIndexControl(ControlNode, binding_name);
          break;
        case TagEditorType.Data:
          CreateDataControl(ControlNode, binding_name);
          break;
        case TagEditorType.TagDefinition:
          CreateTagDefinitionControl(ControlNode, binding_name);
          break;
        default:
          Trace.WriteLine(" (Warning:  no control found for " + data_type + ")");
          break;
      }      
    }
  
    static private void CreateVariableLengthStringControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("VariableLengthString, Bind to: " + BindingName);
    }
    static private void CreateStringTableControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("StringTable, Bind to: " + BindingName);
    }
    static private void CreateFixedLengthStringControl(XmlNode node, string BindingName)
    {
      Controls.FixedLengthString ctrl = new Controls.FixedLengthString();

      foreach (XmlNode ctrl_child_node in node.ChildNodes)
      {
        switch((ControlProperty)ControlHashTable[ctrl_child_node.Name])
        {
          case ControlProperty.ToolTip:
            ctrl.ToolTip = ctrl_child_node.InnerText;
            break;
          default:
            Trace.WriteLine("   unhandled ctrl_child category:" + ctrl_child_node.Name);
            break;
        }
      }

      Trace.WriteLine("FixedLengthString, Bind to: " + BindingName);

      controlManager.AddControlToActiveContainer(ctrl);
    }
    static private void CreateCharIntegerControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("CharInteger, Bind to: " + BindingName);
    }
    static private void CreateShortIntegerControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("ShortInteger, Bind to: " + BindingName);
    }
    static private void CreateLongIntegerControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("LongInteger, Bind to: " + BindingName);
    }
    static private void CreateAngleControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("Angle, Bind to: " + BindingName);
    }
    static private void CreateEnumControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("Enum, Bind to: " + BindingName);
    }
    static private void CreateFlagsControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("Flags, Bind to: " + BindingName);
    }
    static private void CreatePoint2DControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("Point2D, Bind to: " + BindingName);
    }
    static private void CreateRectangle2DControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("Rectangle2D, Bind to: " + BindingName);
    }
    static private void CreateRealPoint2DControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("RealPoint2D, Bind to: " + BindingName);
    }
    static private void CreateRealPoint3DControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("RealPoint3D, Bind to: " + BindingName);
    }
    static private void CreateRGBColorControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("RGBColor, Bind to: " + BindingName);
    }
    static private void CreateARGBColorControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("ARGBColor, Bind to: " + BindingName);
    }
    static private void CreateRealControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("Real, Bind to: " + BindingName);
    }
    static private void CreateRealFractionControl(XmlNode node, string BindingName)
    {
      Controls.RealFraction ctrl = new Controls.RealFraction();
      //ctrl.DataBind();
      Trace.WriteLine("RealFraction, Bind to: " + BindingName);
    }
    static private void CreateRealVector2DControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("RealVector2D, Bind to: " + BindingName);
    }
    static private void CreateRealVector3DControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("RealVector3D, Bind to: " + BindingName);
    }
    static private void CreateRealQuaternionControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("RealQuaternion, Bind to: " + BindingName);
    }
    static private void CreateRealEulerAngles2DControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("RealEulerAngles2D, Bind to: " + BindingName);
    }
    static private void CreateRealEulerAngles3DControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("RealEulerAngles3D, Bind to: " + BindingName);
    }
    static private void CreateRealPlane2DControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("RealPlane2D, Bind to: " + BindingName);
    }
    static private void CreateRealPlane3DControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("RealPlane3D, Bind to: " + BindingName);
    }
    static private void CreateRealARGBColorControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("RealARGBColor, Bind to: " + BindingName);
    }
    static private void CreateRealRGBColorControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("RealRGBColor, Bind to: " + BindingName);
    }
    static private void CreateShortBoundsControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("ShortBounds, Bind to: " + BindingName);
    }
    static private void CreateAngleBoundsControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("AngleBounds, Bind to: " + BindingName);
    }
    static private void CreateRealBoundsControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("RealBounds, Bind to: " + BindingName);
    }
    static private void CreateRealFractionBoundsControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("RealFractionBounds, Bind to: " + BindingName);
    }
    static private void CreateTagReferenceControl(XmlNode node, string BindingName)
    {
      //todo: create tagrefcontrol
      Trace.WriteLine("TagReference, Bind to: " + BindingName);
    }
    static private void CreateBlockControl(XmlNode node, string BindingName)
    {
      //maxelements
      Trace.WriteLine("Block, Bind to: " + BindingName);
    }
    static private void CreateShortBlockIndexControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("ShortBlockIndex, Bind to: " + BindingName);
    }
    static private void CreateLongBlockIndexControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("LongBlockIndex, Bind to: " + BindingName);
    }
    static private void CreateDataControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("Data, Bind to: " + BindingName);
    }
    static private void CreateTagDefinitionControl(XmlNode node, string BindingName)
    {
      Trace.WriteLine("TagDefinition, Bind to: " + BindingName);
    }
  }
}
