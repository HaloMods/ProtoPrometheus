/* ---------------------------------------------------------------
 * Prometheus
 * Bungie Map-based Multi-Game Editing Kit
 * 2004-2005, Halo-Dev
 * ---------------------------------------------------------------
 * File        : Types.cs
 * Author      : MonoxideC
 * Co-Authors  : 
 * ---------------------------------------------------------------
 */

using System;
using System.CodeDom;
using System.IO;
using System.Xml;
using System.Collections;
using System.Diagnostics;

namespace TagLibrary.Types
{
  public class TagDebugger
  {
    public static bool EnableReadDebug = false;
  }

  public interface IBlock
  {
    void Read(BinaryReader reader);
    void ReadChildData(BinaryReader reader);
  }

  /// <summary>
  /// Provides a base for a field in a Blam tag definition.
  /// </summary>
  public interface IField
  {
    void Read(BinaryReader reader);
    void Write(BinaryWriter writer);
  }

  public interface IFieldCodeGenerator
  {
    string Name { get; }
    CodeMemberField GeneratePrivateMember();
    CodeMemberProperty GeneratePublicAccessors();
    string GenerateMetadataInitializer();
    CodeStatement GenerateConstructorLogic();
  }
 
  #region VariableLengthString
  public class VariableLengthString : IField
  {
    private string value = String.Empty;

    public string Value
    {
      get { return value; }
      set { this.value = value; }
    }

    public void Read(BinaryReader reader)
    {
      value = GlobalMethods.ReadCString(reader);
    }

    public void Write(BinaryWriter writer)
    {
      GlobalMethods.WriteCString(value, writer);
    }
  }

  /// <summary>
  /// Generates code for the VariableLengthString class.
  /// </summary>
  public class VariableLengthStringCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public VariableLengthStringCodeGenerator(XmlNode node)
    {
      this.name = node.Attributes["name"].InnerText;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("VariableLengthString", name, "new VariableLengthString()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("VariableLengthString", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region StringID
  // Ask kornman what this is.
  #endregion

  #region StringTable
  public class StringTable : IField
  {
    private int count;
    private string[] value = new string[0];

    public int Count
    {
      get { return count; }
      set { count = value; }
    }

    public string[] Value
    {
      get { return value; }
      set { this.value = value; }
    }

    public void Read(BinaryReader reader)
    {
      count = reader.ReadInt32();
      value = new string[count];
      for(int x = 0; x < count; x++)
      {
        value[x] = new string(reader.ReadChars(128)).Replace("\0", "");
      }
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      for(int x = 0; x < count; x++)
      {
        writer.Write(value[x].ToCharArray());
      }
    }
    public override string ToString()
    {
      return(string.Format("StringTable: <not implemented>"));
    }
  }

  public class StringTableCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public StringTableCodeGenerator(XmlNode node)
    {
      this.name = node.Attributes["name"].InnerText;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("StringTable", name, "new StringTable()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("StringTable", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region FixedLengthString
  public class FixedLengthString : IField
  {
    public string value = "";

    public event EventHandler ValueChanged;
    public string Value
    {
      get { return value; }
      set
      {
        this.value = value;
        if(ValueChanged!=null) ValueChanged(this, new EventArgs() );
      }
    }

    public void Read(BinaryReader reader)
    {
      value = new string(reader.ReadChars(32));
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString() + "\n");
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(value.ToCharArray());
    }
    public override string ToString()
    {
      return(string.Format("FixedLengthString: {0}", value));
    }
  }
  public class FixedLengthStringCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public FixedLengthStringCodeGenerator(XmlNode node)
    {
      name = node.Attributes["name"].InnerText;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("FixedLengthString", name, "new FixedLengthString()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("FixedLengthString", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region CharInteger
  public class CharInteger : IField
  {
    private byte value = 0;

    public byte Value
    {
      get { return value; }
      set { this.value = value; }
    }

    public void Read(BinaryReader reader)
    {
      value = reader.ReadByte();
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(value);
    }
    public override string ToString()
    {
      return(string.Format("CharInteger: {0}", value));
    }
  }

  public class CharIntegerCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public CharIntegerCodeGenerator(XmlNode node)
    {
      this.name = node.Attributes["name"].InnerText; 
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("CharInteger", name, "new CharInteger()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("CharInteger", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region ShortInteger
  public class ShortInteger : IField
  {
    private short value = 0;

    
    public event EventHandler ValueChanged;
    public short Value
    {
      get { return value; }
      set
      {
        this.value = value;
        if(ValueChanged!=null) ValueChanged(this, new EventArgs() );
      }
    }

    public void Read(BinaryReader reader)
    {
      value = reader.ReadInt16();
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(value);
    }
    public override string ToString()
    {
      return(string.Format("ShortInteger: {0}", value));
    }
  }

  public class ShortIntegerCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public ShortIntegerCodeGenerator(XmlNode node)
    {
      this.name = node.Attributes["name"].InnerText; 
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("ShortInteger", name, "new ShortInteger()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("ShortInteger", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region LongInteger
  public class LongInteger : IField
  {
    private int value = 0;

    public int Value
    {
      get { return value; }
      set { this.value = value; }
    }

    public void Read(BinaryReader reader)
    {
      value = reader.ReadInt32();
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(value);
    }
    public override string ToString()
    {
      return(string.Format("LongInteger: {0}", value));
    }
  }

  public class LongIntegerCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public LongIntegerCodeGenerator(XmlNode node)
    {
      this.name = node.Attributes["name"].InnerText; 
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("LongInteger", name, "new LongInteger()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("LongInteger", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region Angle
  public class Angle : IField
  {
    private float value = 0f;
		public event EventHandler ValueDegreesChanged; 

    public float Value
    {
      get { return value; }
      set { this.value = value; }
    }

		public int ValueDegrees
		{
			get { return Convert.ToInt32(Math.Round(value / (Math.PI / 180))); }
			set { this.value = Convert.ToSingle(Math.PI / 180) * value;  if(ValueDegreesChanged!=null) ValueDegreesChanged(this, new EventArgs() ); }
		}

    public void Read(BinaryReader reader)
    {
      value = reader.ReadSingle();
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(value);
    }
    public override string ToString()
    {
      return(string.Format("Angle: {0:N3}", value));
    }
  }

  public class AngleCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public AngleCodeGenerator(XmlNode node)
    {
      this.name = node.Attributes["name"].InnerText;  
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("Angle", name, "new Angle()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("Angle", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }

  #endregion

  #region Enum
  //TODO: Add support for 1 byte enums.
  public class Enum : IField
  {
    private short value;

    public event EventHandler ValueChanged;
    public short Value
    {
      get { return value; }
      set
      {
        this.value = value;
        if(ValueChanged!=null) ValueChanged(this, new EventArgs() );
      }
    }

    public void Read(BinaryReader reader)
    {
      value = reader.ReadInt16();
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(value);
    }
    public override string ToString()
    {
      return(string.Format("Enum: {0}", value));
    }
  }

  public class EnumCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public EnumCodeGenerator(XmlNode node)
    {
      this.name = node.Attributes["name"].InnerText; 
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("Enum", name, "new Enum()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("Enum", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region Flags
  public class Flags : IField
  {
    private int value;
    private int length;

    public event EventHandler ValueChanged;
    public int Value
    {
      get { return value; }
      set
      {
        this.value = value;
        if(ValueChanged!=null) ValueChanged(this, new EventArgs() );
      }
    }

    public int Length
    {
      get { return length; }
      set { length = value; }
    }

    /// <param name="length">Length in bytes of the Flags field.  Can be 1, 2, or 4.</param>
    public Flags(int length)
    {
      this.length = length;
    }

    public bool GetFlag(int index)
    {
      return ((value & (1 << (index-1))) > 0);
    }

    public void SetFlag(int index, bool on)
    {
      if (on)
      {
        Value = this.value | (1 << (index-1));
      }
      else
      {
        Value = this.value & ~(1 << (index-1));
      }
    }

    public void Read(BinaryReader reader)
    {
      if (length == 1) value = (int)reader.ReadByte();
      if (length == 2) value = (int)reader.ReadInt16();
      if (length == 4) value = reader.ReadInt32();
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      if (length == 1) writer.Write(Convert.ToByte(value));
      if (length == 2) writer.Write(Convert.ToInt16(value));
      if (length == 4) writer.Write(value);
    }
    public override string ToString()
    {
      return(string.Format("Flags: {0:X}", value));
    }
  }
  public class FlagsCodeGenerator : IFieldCodeGenerator
  {
    private string name;
    private XmlNode node;

    public string Name
    {
      get { return name; }
    }

    public FlagsCodeGenerator(XmlNode node)
    {
      this.name = node.Attributes["name"].InnerText;    
      this.node = node;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("Flags", name);
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("Flags", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      int length = Convert.ToInt32(node.Attributes["length"].InnerText);
      string privateName = GlobalMethods.MakePrivateName(name);
      CodeStatement statement = new CodeAssignStatement(
        new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), privateName),
        new CodeObjectCreateExpression("Flags", new CodePrimitiveExpression(length)));
      return statement;
    }
  }
  #endregion

  #region Point2D
  //todo:  verify that Point2D is short-based, not int-based
  public class Point2D : IField
  {
    private short x;
    private short y;

    public short X
    {
      get { return x; }
      set { this.x = value; }
    }

    public short Y
    {
      get { return y; }
      set { this.y = value; }
    }

    public void Read(BinaryReader reader)
    {
      x = reader.ReadInt16();
      y = reader.ReadInt16();
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(x);
      writer.Write(y);
    }

    public override string ToString()
    {
      return(string.Format("Point2D: ({0},{1})", x, y));
    }
  }

  public class Point2DCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public Point2DCodeGenerator(XmlNode node)
    {
      this.name = node.Attributes["name"].InnerText;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("Point2D", name, "new Point2D()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("Point2D", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region Rectangle2D
  public class Rectangle2D : IField
  {
    private short t;
    private short l;
    private short b;
    private short r;

    public short T
    {
      get { return t; }
      set { t = value; }
    }

    public short L
    {
      get { return l; }
      set { l = value; }
    }

    public short B
    {
      get { return b; }
      set { b = value; }
    }

    public short R
    {
      get { return r; }
      set { r = value; }
    }

    public void Read(BinaryReader reader)
    {
      t = reader.ReadInt16();
      l = reader.ReadInt16();
      b = reader.ReadInt16();
      r = reader.ReadInt16();
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0x}:  ", reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(t);
      writer.Write(l);
      writer.Write(b);
      writer.Write(r);
    }
    public override string ToString()
    {
      return(string.Format("Rectangle2D: {0:N5} {1:N5} {2:N5} {3:N5}", t,l,b,r));
    }
  }

  public class Rectangle2DCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public Rectangle2DCodeGenerator(XmlNode node)
    {
      this.name = node.Attributes["name"].InnerText;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("Rectangle2D", name, "new Rectangle2D()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("Rectangle2D", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region Point3D
  #endregion

  #region RealPoint2D
  public class RealPoint2D : IField
  {
    private float x;
    private float y;

    public float X
    {
      get { return x; }
      set { this.x = value; }
    }

    public float Y
    {
      get { return y; }
      set { this.y = value; }
    }

    public void Read(BinaryReader reader)
    {
      x = reader.ReadSingle();
      y = reader.ReadSingle();
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(x);
      writer.Write(y);
    }
    public override string ToString()
    {
      return(string.Format("RealPoint2D: ({0:N3},{1:N3})", x, y));
    }
  }

  public class RealPoint2DCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public RealPoint2DCodeGenerator(XmlNode node)
    {
      this.name = node.Attributes["name"].InnerText;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("RealPoint2D", name, "new RealPoint2D()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("RealPoint2D", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region RealPoint3D
  public class RealPoint3D : IField
  {
    private float x;
    private float y;
    private float z;

    public event EventHandler XChanged;
    public float X
    {
      get { return x; }
      set
      {
        this.x = value; 
        if(XChanged != null) XChanged(this, new EventArgs() );
      }
    }

    public event EventHandler YChanged;
    public float Y
    {
      get { return y; }
      set
      { 
        this.y = value;
        if(YChanged != null) YChanged(this, new EventArgs() );
      }
    }

    public event EventHandler ZChanged;
    public float Z
    {
      get { return z; }
      set
      { 
        this.z = value; 
        if(ZChanged != null) ZChanged(this, new EventArgs() );
      }
    }

    public void Read(BinaryReader reader)
    {
      x = reader.ReadSingle();
      y = reader.ReadSingle();
      z = reader.ReadSingle();
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(x);
      writer.Write(y);
      writer.Write(z);
    }
    public override string ToString()
    {
      return(string.Format("RealPoint3D: ({0:N3},{1:N3},{1:N3})", x,y,z));
    }
  }

  public class RealPoint3DCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public RealPoint3DCodeGenerator(XmlNode node)
    {
      this.name = node.Attributes["name"].InnerText;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("RealPoint3D", name, "new RealPoint3D()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("RealPoint3D", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region RGBColor
  public class RGBColor : IField
  {
    private byte r = 0;
    private byte g = 0;
    private byte b = 0;

    public byte R
    {
      get { return r; }
      set { r = value; }
    }

    public byte G
    {
      get { return g; }
      set { g = value; }
    }

    public byte B
    {
      get { return b; }
      set { b = value; }
    }

    public void Read(BinaryReader reader)
    {
      r = reader.ReadByte();
      g = reader.ReadByte();
      b = reader.ReadByte();
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(r);
      writer.Write(g);
      writer.Write(b);
    }
    public override string ToString()
    {
      return(string.Format("RGBColor: {0:X} {1:X} {2:X}", r,g,b));
    }
  }

  public class RGBColorCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public RGBColorCodeGenerator(XmlNode node)
    {
      this.name = node.Attributes["name"].InnerText;   
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("RGBColor", name, "new RGBColor()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("RGBColor", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region ARGBColor
  public class ARGBColor : IField
  {
    private byte a = 0;
    private byte r = 0;
    private byte g = 0;
    private byte b = 0;

    public byte A
    {
      get { return a; }
      set { a = value; }
    }

    public byte R
    {
      get { return r; }
      set { r = value; }
    }

    public byte G
    {
      get { return g; }
      set { g = value; }
    }

    public byte B
    {
      get { return b; }
      set { b = value; }
    }

    public void Read(BinaryReader reader)
    {
      a = reader.ReadByte();
      r = reader.ReadByte();
      g = reader.ReadByte();
      b = reader.ReadByte();
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(a);
      writer.Write(r);
      writer.Write(g);
      writer.Write(b);
    }
    public override string ToString()
    {
      return(string.Format("ARGBColor: {0:X} {1:X} {2:X} {3:X}", a,r,g,b));
    }
  }

  public class ARGBColorCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public ARGBColorCodeGenerator(XmlNode node)
    {
      this.name = node.Attributes["name"].InnerText;   
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("ARGBColor", name, "new ARGBColor()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("ARGBColor", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region Real
  public class Real : IField
  {
    private float value;
    
    public event EventHandler ValueChanged;
    public float Value
    {
      get { return value; }
      set
      {
        this.value = value;
        if(ValueChanged!=null) ValueChanged(this, new EventArgs() );
      }
    }

    public void Read(BinaryReader reader)
    {
      value = reader.ReadSingle();
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(value);
    }
    public override string ToString()
    {
      return(string.Format("Real: {0:N3}", value));
    }
  }

  public class RealCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public RealCodeGenerator(XmlNode node)
    {
      this.name = node.Attributes["name"].InnerText; 
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("Real", name, "new Real()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("Real", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region RealFraction
  public class RealFraction : IField
  {
    private float value;
    public event EventHandler ValueChanged;
    public float Value
    {
      get { return value; }
      set
      {
        this.value = value;
        if(ValueChanged!=null) ValueChanged(this, new EventArgs() );
      }
    }

    public void Read(BinaryReader reader)
    {
      value = reader.ReadSingle();
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(value);
    }
    public override string ToString()
    {
      return(string.Format("RealFraction: {0:N3}", value));
    }
  }

  public class RealFractionCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public RealFractionCodeGenerator(XmlNode node)
    {
      this.name = node.Attributes["name"].InnerText; 
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("RealFraction", name, "new RealFraction()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("RealFraction", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region RealPoint2D
  #endregion

  #region RealVector2D
  public class RealVector2D : IField
  {
    private float i;
    private float k;

    public float I
    {
      get { return i; }
      set { this.i = value; }
    }

    public float K
    {
      get { return k; }
      set { this.k = value; }
    }

    public void Read(BinaryReader reader)
    {
      i = reader.ReadSingle();
      k = reader.ReadSingle();
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(i);
      writer.Write(k);
    }
    public override string ToString()
    {
      return(string.Format("Flags: {0:N3}  {1:N3}", i, k));
    }
  }

  public class RealVector2DCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public RealVector2DCodeGenerator(XmlNode node)
    {
      this.name = node.Attributes["name"].InnerText;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("RealVector2D", name, "new RealVector2D()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("RealVector2D", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region RealVector3D
  public class RealVector3D : IField
  {
    private float i;
    private float j;
    private float k;

    public float I
    {
      get { return i; }
      set { this.i = value; }
    }

    public float K
    {
      get { return k; }
      set { this.k = value; }
    }

    public float J
    {
      get { return j; }
      set { this.j = value; }
    }

    public void Read(BinaryReader reader)
    {
      i = reader.ReadSingle();
      j = reader.ReadSingle();
      k = reader.ReadSingle();
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(i);
      writer.Write(j);
      writer.Write(k);
    }
    public override string ToString()
    {
      return(string.Format("RealVector3D: {0:N3}  {0:N3}  {0:N3}", i,j,k));
    }
  }

  public class RealVector3DCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public RealVector3DCodeGenerator(XmlNode node)
    {
      this.name = node.Attributes["name"].InnerText;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("RealVector3D", name, "new RealVector3D()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("RealVector3D", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region RealQuaternion
  public class RealQuaternion : IField
  {
    private float i = 0;
    private float j = 0;
    private float k = 0;
    private float w = 0;

    public float I
    {
      get { return i; }
      set { i = value; }
    }

    public float J
    {
      get { return j; }
      set { j = value; }
    }

    public float K
    {
      get { return k; }
      set { k = value; }
    }

    public float W
    {
      get { return w; }
      set { w = value; }
    }

    public void Read(BinaryReader reader)
    {
      i = reader.ReadSingle();
      j = reader.ReadSingle();
      k = reader.ReadSingle();
      w = reader.ReadSingle();
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(i);
      writer.Write(j);
      writer.Write(k);
      writer.Write(w);
    }
    public override string ToString()
    {
      return(string.Format("RealQuaternion: {0:N3}  {1:N3}  {2:N3}  {3:N3}", i, j, k, w));
    }
  }

  public class RealQuaternionCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public RealQuaternionCodeGenerator(XmlNode node)
    {
      name = node.Attributes["name"].InnerText;
    }

    public string Name
    {
      get { return name; }
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("RealQuaternion", name, "new RealQuaternion()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("RealQuaternion", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region RealEulerAngles2D
  public class RealEulerAngles2D : IField
  {
    private float y;
    private float p;

    
    public event EventHandler YChanged;
    public float Y
    {
      get { return y; }
      set
      {
        y = value;
        if(YChanged!=null) YChanged(this, new EventArgs() );
      }
    }

    public event EventHandler PChanged;
    public float P
    {
      get { return p; }
      set
      {
        p = value;
        if(PChanged!=null) PChanged(this, new EventArgs() );
      }
    }

    public void Read(BinaryReader reader)
    {
      y = reader.ReadSingle();
      p = reader.ReadSingle();
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(y);
      writer.Write(p);
    }
    public override string ToString()
    {
      return(string.Format("RealEulerAngles2D: {0:N3}  {0:N3}", y,p));
    }
  }

  public class RealEulerAngles2DCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public RealEulerAngles2DCodeGenerator(XmlNode node)
    {
      name = node.Attributes["name"].InnerText;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("RealEulerAngles2D", name, "new RealEulerAngles2D()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("RealEulerAngles2D", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region RealEulerAngles3D
  public class RealEulerAngles3D : IField
  {
    private float y;
    private float p;
    private float r;

    public event EventHandler YChanged;
    public float Y
    {
      get { return y; }
      set 
      {
        y = value; 
        if(YChanged!=null) YChanged(this, new EventArgs() );
      }
    }

    public event EventHandler PChanged;
    public float P
    {
      get { return p; }
      set 
      {
        p = value; 
        if(PChanged != null) PChanged(this, new EventArgs() );
      }
    }

    public event EventHandler RChanged;
    public float R
    {
      get { return r; }
      set
      {
        r = value; 
        if(RChanged!=null) RChanged(this, new EventArgs() );
      }
    }

    public void Read(BinaryReader reader)
    {
      y = reader.ReadSingle();
      p = reader.ReadSingle();
      r = reader.ReadSingle();
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(y);
      writer.Write(p);
      writer.Write(r);
    }
    public override string ToString()
    {
      return(string.Format("RealEulerAngles3D: {0:N3}  {1:N3}  {2:N3}", y, p, r));
    }
  }

  public class RealEulerAngles3DCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public RealEulerAngles3DCodeGenerator(XmlNode node)
    {
      name = node.Attributes["name"].InnerText;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("RealEulerAngles3D", name, "new RealEulerAngles3D()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("RealEulerAngles3D", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region RealPlane2D
  public class RealPlane2D : IField
  {
    private float i;
    private float j;
    private float d;

    public float I
    {
      get { return i; }
      set { this.i = value; }
    }

    public float J
    {
      get { return j; }
      set { this.j = value; }
    }

    public float D
    {
      get { return d; }
      set { this.d = value; }
    }

    public void Read(BinaryReader reader)
    {
      i = reader.ReadSingle();
      j = reader.ReadSingle();
      d = reader.ReadSingle();
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(i);
      writer.Write(j);
      writer.Write(d);
    }
    public override string ToString()
    {
      return(string.Format("RealPlane2D: {0:N3}  {1:N3}  {2:N3}", i,j,d));
    }
  }

  public class RealPlane2DCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public RealPlane2DCodeGenerator(XmlNode node)
    {
      this.name = node.Attributes["name"].InnerText;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("RealPlane2D", name, "new RealPlane2D()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("RealPlane2D", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region RealPlane3D
  public class RealPlane3D : IField
  {
    private float i;
    private float j;
    private float k;
    private float d;

    public float I
    {
      get { return i; }
      set { this.i = value; }
    }

    public float K
    {
      get { return k; }
      set { this.k = value; }
    }

    public float J
    {
      get { return j; }
      set { this.j = value; }
    }

    public float D
    {
      get { return d; }
      set { this.d = value; }
    }

    public void Read(BinaryReader reader)
    {
      i = reader.ReadSingle();
      j = reader.ReadSingle();
      k = reader.ReadSingle();
      d = reader.ReadSingle();
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(i);
      writer.Write(j);
      writer.Write(k);
      writer.Write(d);
    }
    public override string ToString()
    {
      return(string.Format("RealPlane3D: {0:N3}  {1:N3}  {2:N3}  {3:N3}", i,j,k,d));
    }
  }

  public class RealPlane3DCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public RealPlane3DCodeGenerator(XmlNode node)
    {
      this.name = node.Attributes["name"].InnerText;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("RealPlane3D", name, "new RealPlane3D()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("RealPlane3D", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region RealARGBColor
  public class RealARGBColor : IField
  {
    private float a;
    private float r;
    private float g;
    private float b;

    public event EventHandler AChanged;
    public float A
    {
      get { return a; }
      set
      {
        a = value;
        if(AChanged!=null) AChanged(this, new EventArgs() );
      }
    }

    public event EventHandler RChanged;
    public float R
    {
      get { return r; }
      set
      {
        r = value;
        if(RChanged!=null) RChanged(this, new EventArgs() );
      }
    }

    public event EventHandler GChanged;
    public float G
    {
      get { return g; }
      set
      {
        g = value;
        if(GChanged!=null) GChanged(this, new EventArgs() );
      }
    }

    public event EventHandler BChanged;
    public float B
    {
      get { return b; }
      set
      {
        b = value;
        if(BChanged!=null) BChanged(this, new EventArgs() );
      }
    }

    public void Read(BinaryReader reader)
    {
      a = reader.ReadSingle();
      r = reader.ReadSingle();
      g = reader.ReadSingle();
      b = reader.ReadSingle();
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(a);
      writer.Write(r);
      writer.Write(g);
      writer.Write(b);
    }
    public override string ToString()
    {
      return(string.Format("RealARGBColor: {0:N3} {1:N3} {2:N3} {3:N3}", a,r,g,b));
    }
  }

  public class RealARGBColorCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public RealARGBColorCodeGenerator(XmlNode node)
    {
      name = node.Attributes["name"].InnerText;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("RealARGBColor", name, "new RealARGBColor()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("RealARGBColor", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region RealRGBColor
  public class RealRGBColor : IField
  {
    private float r;
    private float g;
    private float b;

    public float R
    {
      get { return r; }
      set { r = value; }
    }

    public float G
    {
      get { return g; }
      set { g = value; }
    }

    public float B
    {
      get { return b; }
      set { b = value; }
    }

    public void Read(BinaryReader reader)
    {
      r = reader.ReadSingle();
      g = reader.ReadSingle();
      b = reader.ReadSingle();
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(r);
      writer.Write(g);
      writer.Write(b);
    }
    public override string ToString()
    {
      return(string.Format("RealRGBColor: {0:N3} {1:N3} {2:N3}", r,g,b));
    }
  }

  public class RealRGBColorCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public RealRGBColorCodeGenerator(XmlNode node)
    {
      name = node.Attributes["name"].InnerText;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("RealRGBColor", name, "new RealRGBColor()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("RealRGBColor", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region ShortBounds
  public class ShortBounds : IField
  {
    private short lower;
    private short upper;

    public short Lower
    {
      get { return lower; }
      set { lower = value; }
    }

    public short Upper
    {
      get { return upper; }
      set { upper = value; }
    }

    public void Read(BinaryReader reader)
    {
      lower = reader.ReadInt16();
      upper = reader.ReadInt16();
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(lower);
      writer.Write(upper);
    }
    public override string ToString()
    {
      return(string.Format("ShortBounds: {0}  {1}", lower, upper));
    }
  }

  public class ShortBoundsCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public ShortBoundsCodeGenerator(XmlNode node)
    {
      name = node.Attributes["name"].InnerText;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("ShortBounds", name, "new ShortBounds()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("ShortBounds", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region AngleBounds
  public class AngleBounds : IField
  {
    private float lower;
    private float upper;

    public float Lower
    {
      get { return lower; }
      set { lower = value; }
    }

    public float Upper
    {
      get { return upper; }
      set { upper = value; }
    }

		public event EventHandler LowerDegreesChanged;
		public event EventHandler UpperDegreesChanged;

		public int LowerDegrees
		{
			get { return Convert.ToInt32(Math.Round(lower / (Math.PI / 180))); }
			set { this.lower = Convert.ToSingle(Math.PI / 180) * value; if(LowerDegreesChanged!=null) LowerDegreesChanged(this, new EventArgs() ); }
		}

		public int UpperDegrees
		{
			get { return Convert.ToInt32(Math.Round(upper / (Math.PI / 180))); }
			set { this.upper = Convert.ToSingle(Math.PI / 180) * value; if(UpperDegreesChanged!=null) UpperDegreesChanged(this, new EventArgs() ); }
		}

    public void Read(BinaryReader reader)
    {
      lower = reader.ReadSingle();
      upper = reader.ReadSingle();
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(lower);
      writer.Write(upper);
    }
    public override string ToString()
    {
      return(string.Format("AngleBounds: {0}  {1}", lower, upper));
    }
  }

  public class AngleBoundsCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public AngleBoundsCodeGenerator(XmlNode node)
    {
      name = node.Attributes["name"].InnerText;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("AngleBounds", name, "new AngleBounds()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("AngleBounds", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region RealBounds
  public class RealBounds : IField
  {
    private float lower;
    private float upper;

    public float Lower
    {
      get { return lower; }
      set { lower = value; }
    }

    public float Upper
    {
      get { return upper; }
      set { upper = value; }
    }

    public void Read(BinaryReader reader)
    {
      lower = reader.ReadSingle();
      upper = reader.ReadSingle();
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(lower);
      writer.Write(upper);
    }
    public override string ToString()
    {
      return(string.Format("RealBounds: {0:N3}  {1:N3}", lower, upper));
    }
  }

  public class RealBoundsCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public RealBoundsCodeGenerator(XmlNode node)
    {
      name = node.Attributes["name"].InnerText;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("RealBounds", name, "new RealBounds()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("RealBounds", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region RealFractionBounds
  public class RealFractionBounds : IField
  {
    private float lower;
    private float upper;

    public float Lower
    {
      get { return lower; }
      set { lower = value; }
    }

    public float Upper
    {
      get { return upper; }
      set { upper = value; }
    }

    public void Read(BinaryReader reader)
    {
      lower = reader.ReadSingle();
      upper = reader.ReadSingle();
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(lower);
      writer.Write(upper);
    }
    public override string ToString()
    {
      return(string.Format("AngleBounds: {0}  {1}", lower, upper));
    }
  }

  public class RealFractionBoundsCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public RealFractionBoundsCodeGenerator(XmlNode node)
    {
      name = node.Attributes["name"].InnerText;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("RealFractionBounds", name, "new RealFractionBounds()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("RealFractionBounds", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region TagReference
  public class OffsetFIFO
  {
    ArrayList Stack = new ArrayList();
    public void Push(int offset)
    {
      Stack.Add(offset);
    }
    public int Pop()
    {
      int data = (int)Stack[0];
      Stack.RemoveAt(0);
      return(data);
    }
  }
  public class TagReference : IField
  {
    static public OffsetFIFO StringOffsetFIFO = new OffsetFIFO();
    private byte[] tagGroup = new byte[4];
    private int stringLength;
    private string value = "";

    public string TagGroup
    {
      get
      {
        char[] tmp = new char[4];
        tmp[0] = (char)tagGroup[0];
        tmp[1] = (char)tagGroup[1];
        tmp[2] = (char)tagGroup[2];
        tmp[3] = (char)tagGroup[3];
        return new string(tmp); 
      }
      set
      { 
        tagGroup[0] = (byte)value[0];
        tagGroup[1] = (byte)value[1];
        tagGroup[2] = (byte)value[2];
        tagGroup[3] = (byte)value[3];
      }
    }
    
    public event EventHandler ValueChanged;
    public string Value
    {
      get { return value; }
      set
      {
        this.value = value;
        stringLength = value.Length;
        if(ValueChanged!=null) ValueChanged(this, new EventArgs());
      }
    }

    public void Read(BinaryReader reader)
    {
      tagGroup = reader.ReadBytes(4);
      reader.BaseStream.Position += 4;
      stringLength = reader.ReadInt32();
      reader.BaseStream.Position += 4;
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToPreString());
    }

    public void ReadString(BinaryReader reader)
    {
      if (stringLength > 0)
      {
        byte[] temp = reader.ReadBytes(stringLength+1);
        value = System.Text.Encoding.ASCII.GetString(temp, 0, stringLength);
        if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
      }
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(tagGroup);
      if(stringLength > 0)
        TagReference.StringOffsetFIFO.Push((int)writer.BaseStream.Position);
      writer.Write(0);
      writer.Write(stringLength);
      writer.Write(-1);
    }

    public void WriteString(BinaryWriter writer)
    {
      if(stringLength > 0)
      {
        int current_offset = (int)writer.BaseStream.Position;
        int offset_location = TagReference.StringOffsetFIFO.Pop();
        writer.BaseStream.Position = offset_location;
        writer.Write(current_offset);
        writer.BaseStream.Position = current_offset;
        GlobalMethods.WriteCString(value, writer);
      }
    }
    public override string ToString()
    {
      return(string.Format("TagRef(string): {0}{1}{2}{3} -> {4}", tagGroup[0],tagGroup[1],tagGroup[2],tagGroup[3], value));
    }
    public string ToPreString()
    {
      return(string.Format("TagRef: {0}{1}{2}{3} strlen= {4}", tagGroup[0],tagGroup[1],tagGroup[2],tagGroup[3], stringLength));
    }
  }

  public class TagReferenceCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
      set { name = value; }
    }

    public TagReferenceCodeGenerator(XmlNode node)
    {
      name = node.Attributes["name"].InnerText;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("TagReference", name, "new TagReference()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("TagReference", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  // Put some thought into how to handle undoing addition/removal of blocks.
  // Idea #1: Put the functionality of restoring value into an Interface - IUndoItemState.RestoreState();
  // Then inherit two different item states - one for standard value changes, and one for blocks.
  #region Block
  public class Block : IField
  {
    private int count;
    private int offsetLocation;

    public int Count
    {
      get { return count; }
      set { count = value; }
    }

    public void Read(BinaryReader reader)
    {
      count = reader.ReadInt32();
      offsetLocation = reader.ReadInt32();
      reader.BaseStream.Position += 4;
      if(TagDebugger.EnableReadDebug  && (count > 0))Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(count);
      
      //store the location of the reflexive offset so we can update it later
      offsetLocation = (int)writer.BaseStream.Position;
      Trace.WriteLine(string.Format("offset at {0:X}", (int)writer.BaseStream.Position + 0x40));
      //writer.Write(0xbaadf00d);
      writer.BaseStream.Position += 8;
    }
    public override string ToString()
    {
      return(string.Format("Block:  Count = {0}  Offset = 0x{1:X}", count, offsetLocation));
    }
    public void UpdateReflexiveOffset(BinaryWriter writer)
    {
      if(count > 0)
      {
        int current_location = (int)writer.BaseStream.Position;
        writer.BaseStream.Position = this.offsetLocation;
        writer.Write(current_location);
        writer.BaseStream.Position = current_location;
      }
    }
  }
  public class BlockCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public BlockCodeGenerator(XmlNode node)
    {
      name = node.Attributes["name"].InnerText;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("Block", name, "new Block()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("Block", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }

  #endregion

  #region ShortBlockIndex
  public class ShortBlockIndex : IField
  {
    private short value;
    
    public short Value
    {
      get { return value; }
      set { this.value = value; }
    }

    public void Read(BinaryReader reader)
    {
      value = reader.ReadInt16();
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(value);
    }
    public override string ToString()
    {
      return(string.Format("ShortBlockIndex: {0}", value));
    }
  }

  public class ShortBlockIndexCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public ShortBlockIndexCodeGenerator(XmlNode node)
    {
      name = node.Attributes["name"].InnerText;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("ShortBlockIndex", name, "new ShortBlockIndex()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("ShortBlockIndex", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region LongBlockIndex
  public class LongBlockIndex : IField
  {
    private int _value;
    
    public int Value
    {
      get { return _value; }
      set { _value = value; }
    }

    public void Read(BinaryReader reader)
    {
      _value = reader.ReadInt32();
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(_value);
    }
    public override string ToString()
    {
      return(string.Format("LongBlockIndex: {0}", _value));
    }
  }

  public class LongBlockIndexCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public LongBlockIndexCodeGenerator(XmlNode node)
    {
      name = node.Attributes["name"].InnerText;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("LongBlockIndex", name, "new LongBlockIndex()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("LongBlockIndex", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region Data
  public class Data : IField
  {
    private int size;
    private uint tagDataOffset;
    private uint pcDataOffset;
    private byte[] binary;

    public byte[] Binary
    {
      get { return binary; }
    }

    public void Read(BinaryReader reader)
    {
      size = reader.ReadInt32();
      reader.BaseStream.Position += 4;
      tagDataOffset = reader.ReadUInt32(); //these change if stuff is modified - eek
      pcDataOffset = reader.ReadUInt32(); //this one probably doesn't matter
      reader.BaseStream.Position += 4;
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void ReadBinary(BinaryReader reader)
    {
      binary = reader.ReadBytes(size);
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(size);
      //this is one of those 5 element offsets viper was talking about
      //data is not matching except for 1st element
      writer.Write(0xbaadbaad);
      writer.Write(0xbaadbaad);
      writer.Write(0xbaadbaad);
      writer.Write(0xbaadbaad);
      //writer.BaseStream.Position += 16;
    }

    public void WriteBinary(BinaryWriter writer)
    {
      writer.Write(binary);
    }
    public override string ToString()
    {
      return(string.Format("Data: size = {0:X}", size));
    }
  }
  
  public class DataCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public DataCodeGenerator(XmlNode node)
    {
      name = node.Attributes["name"].InnerText;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("Data", name, "new Data()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("Data", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  #region Pad
  public class Pad : IField
  {
    private int length;

    public int Length
    {
      get { return length; }
    }

    public Pad(int length)
    {
      this.length = length;
    }

    public void Read(BinaryReader reader)
    {
      reader.BaseStream.Position += length;
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      //pad data isn't matching original, but it might because original
      //had garbage in there
      writer.BaseStream.Position += length;
    }
    public override string ToString()
    {
      return(string.Format("Pad: Length = 0x{0:X}", length));
    }
  }

  public class PadCodeGenerator : IFieldCodeGenerator
  {
    private XmlNode node;
    private string name;
    
    public string Name
    {
      get { return name; }
    }

    public PadCodeGenerator(XmlNode node)
    {
      this.node = node;
      this.name = node.Attributes["name"].InnerText;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("Pad", name);
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return null;
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }
    public CodeStatement GenerateConstructorLogic()
    {
      int length = Convert.ToInt32(node.SelectSingleNode("length").InnerText);
      string privateName = GlobalMethods.MakePrivateName(name);
      CodeStatement statement = new CodeAssignStatement(
        new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), privateName),
        new CodeObjectCreateExpression("Pad", new CodePrimitiveExpression(length)));
      return statement;
    }
  }
  #endregion

  #region Skip
  public class Skip : IField
  {
    private int length;

    public int Length
    {
      get { return length; }
    }

    public Skip(int length)
    {
      this.length = length;
    }

    public void Read(BinaryReader reader)
    {
      reader.BaseStream.Position += length;
    }

    public void Write(BinaryWriter writer)
    {
      writer.BaseStream.Position += length;
    }
    public override string ToString()
    {
      return(string.Format("Skip: Length = {0:X}", length));
    }
  }

  public class SkipCodeGenerator : IFieldCodeGenerator
  {
    private XmlNode node;
    private string name;
    
    public string Name
    {
      get { return name; }
    }

    public SkipCodeGenerator(XmlNode node)
    {
      this.node = node;
      this.name = node.Attributes["name"].InnerText;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("Skip", name);
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return null;
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }
    public CodeStatement GenerateConstructorLogic()
    {
      int length = Convert.ToInt32(node.SelectSingleNode("length").InnerText);
      string privateName = GlobalMethods.MakePrivateName(name);
      CodeStatement statement = new CodeAssignStatement(
        new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), privateName),
        new CodeObjectCreateExpression("Skip", new CodePrimitiveExpression(length)));
      return statement;
    }
  }
  #endregion

  #region TagDefinition
  public class TagDefinition : IField
  {
    private char[] tag = new char[4];

    public char[] Tag
    {
      get { return tag; }
      set { tag = value; }
    }

    public void Read(BinaryReader reader)
    {
      tag = reader.ReadChars(4);
      if(TagDebugger.EnableReadDebug)Trace.WriteLine(string.Format("{0:X}:  ", (int)reader.BaseStream.Position) + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(tag);
    }
    public override string ToString()
    {
      return(string.Format("TagDef: {0}{1}{2}{3}", tag[0],tag[1],tag[2],tag[3]));
    }
  }

  public class TagCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public TagCodeGenerator(XmlNode node)
    {
      name = node.Attributes["name"].InnerText;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("TagDefinition", name, "new TagDefinition()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("TagDefinition", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
  #endregion

  public enum Accessors
  {
    Get,
    Set,
    Both
  }

  public class GlobalMethods
  {
    public static string ReadCString(BinaryReader reader)
    {
      string s = "";
      char c = new char();
      do
      {
        c = reader.ReadChar();
        if (c != (char)0) s += c;
      }
      while (c != (char)0);
      return s;
    }

    public static void WriteString(string value, BinaryWriter writer)
    {
      byte[] bin = System.Text.Encoding.ASCII.GetBytes(value);
      writer.Write(bin);
    }

    public static void WriteCString(string value, BinaryWriter writer)
    {
      if(value != null)
      {
        byte[] bin = System.Text.Encoding.ASCII.GetBytes(value);
        writer.Write(bin);
        writer.Write((byte)0);
      }
    }
    //
    //    protected static string FixInvalidName(string name)
    //    {
    //      // We'll add more as they become apparant.
    //      name = name.Replace("'", "");
    //      if (name.IndexOf('<') > -1)
    //      {
    //        // We need to remove a <text> string from the name.
    //        int start = name.IndexOf('<');
    //        int end = name.LastIndexOf('>');
    //        name = name.Substring(0, start) + name.Substring(start+1, (end - start)-1) + name.Substring(end+1);
    //      }
    //      return name;
    //    }

    public static string MakePublicName(string name)
    {
      //name = FixInvalidName(name);
      string[] parts = name.Split(' ');
      string result = "";
      for (int x=0; x<parts.Length; x++)
      {
        result += parts[x].Substring(0, 1).ToUpper() + parts[x].Substring(1);
      }

      if(result.IndexOf('.') != -1)
        result = result.Replace(".", "_Pt");

      if(result.IndexOf('+') != -1)
        result = result.Replace("+", "_Plus");

      if(result.IndexOf('\"') != -1)
        result = result.Replace("\"", "");

      if(result.IndexOf('_') != -1)
        result = result.Replace("_", "");

      char[] numeric = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
      for(int i=0; i<10; i++)
        if(result[0] == numeric[i])
          result = "_" + result;

      result = result.Replace(" ", "");

      Trace.WriteLine("MakePublicName:  in = " + name + "   out = " + result);

      return result;
    }

    public static string MakePrivateName(string name)
    {
      string result = MakePublicName(name);
      result = result.Substring(0, 1).ToLower() + result.Substring(1);

//      if(result.IndexOf('.') != -1)
//        result = result.Replace(".", "_pt");
//    
//      if(result.IndexOf('+') != -1)
//        result = result.Replace("+", "_plus");
//
//      if(result.IndexOf('\"') != -1)
//        result = result.Replace("\"", "");
//
//      char[] numeric = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
//      for(int i=0; i<10; i++)
//        if(result[0] == numeric[i])
//          result = "_" + result;
//
//
      result = "_" + result;

      Trace.WriteLine("MakePrivateName:  in = " + name + "   out = " + result);
      return result;
    }

    public static CodeMemberProperty GeneratePublicAccessors(string type, string name, Accessors accessors)
    {
      string publicName = MakePublicName(name);
      name = MakePrivateName(name);

      CodeMemberProperty accessor = new CodeMemberProperty();
      accessor.Name = publicName;
      accessor.Type = new CodeTypeReference(type);
      accessor.Attributes = MemberAttributes.Public | MemberAttributes.Final;
      accessor.HasGet = false;
      accessor.HasSet = false;

      if ((accessors == Accessors.Get) || (accessors == Accessors.Both))
      {
        accessor.HasGet = true;
        accessor.GetStatements.Add(new CodeMethodReturnStatement(
          new CodeFieldReferenceExpression(
          new CodeThisReferenceExpression(), name)));
      }
      if ((accessors == Accessors.Set) || (accessors == Accessors.Both))
      {
        accessor.HasSet = true;
        accessor.SetStatements.Add(new CodeAssignStatement(
          new CodeFieldReferenceExpression(
          new CodeThisReferenceExpression(), name),
          new CodePropertySetValueReferenceExpression()));
      }
      return accessor;
    }

    public static CodeMemberField GeneratePrivateMember(string type, string name, string initialization)
    {
      string privateName = MakePrivateName(name);
      name = name.Replace(" ", "");
      CodeMemberField member = new CodeMemberField(type, privateName);
      if (initialization != null)
        member.InitExpression = new CodeObjectCreateExpression(type, new CodeExpression[] {});
      return member;
    }

    public static CodeMemberField GeneratePrivateMember(string type, string name)
    {
      return GeneratePrivateMember(type, name, null);
    }

    public static float DegreesToRadians(float angle)
    {
      if(angle == (float)0)
        return 0;

      double radians = (Math.PI / 180) * (double)angle;
      return (float)radians; 
    }

    public static float RadiansToDegrees(float radian)
    {
      if(radian == (float)0)
        return 0;

      double degrees = (180 / Math.PI) * (double)radian;
      return (float)degrees; 
    }
  }

  public class TagAbbreviationTable
  {
    private static Hashtable types;
    static TagAbbreviationTable()
    {
      types = new Hashtable();;
      types.Add("bitm" , "Bitmap");
      types.Add("scnr" , "Scenario");
      types.Add("sbsp" , "ScenarioStructureBsp");
      types.Add("ant!", "Antenna");
      types.Add("actr", "Actor");
      types.Add("actv", "ActorVariant");
      types.Add("antr", "ModelAnimations");
      types.Add("bipd", "Biped");
      types.Add("ctrl", "Control");
      types.Add("coll", "ModelCollisionGeometry");
      types.Add("cont", "Contrail");
      types.Add("deca", "Decal");
      types.Add("jpt!", "DamageEffect");
      types.Add("udlg", "Dialogue");
      types.Add("dobc", "DetailObjectCollection");
      types.Add("DeLa", "UIWidgetDefinition");
      types.Add("eqip", "Equipment");
      types.Add("effe", "Effect");
      types.Add("fog ", "Fog");
      types.Add("elec", "Lightning");
      types.Add("font", "Font");
      types.Add("garb", "Garbage");
      types.Add("grhi", "GrenadeHUDInterface");
      types.Add("matg", "Globals");
      types.Add("glw!", "Glow");
      types.Add("hud#", "HUDNumber");
      types.Add("hudg", "HUDGlobals");
      types.Add("hmt ", "HUDMessage Text");
      types.Add("item", "Item");
      types.Add("itmc", "ItemCollection");
      types.Add("lens", "Lens");
      types.Add("lsnd", "SoundLooping");
      types.Add("lifi", "LightFixture");
      types.Add("ligh", "Light");
      types.Add("mgs2", "LightVolume");
      types.Add("mach", "Machine");
      types.Add("mply", "Multiplayer Scenario");
      types.Add("metr", "Meter");
      types.Add("obje", "Object");
      types.Add("part", "Particle");
      types.Add("pctl", "Particle System");
      types.Add("phys", "Physics");
      types.Add("pphy", "Point Physics");
      types.Add("mod2", "GBXModel");
      types.Add("proj", "Projectile");
      types.Add("devc", "PC Device Default");
      types.Add("snd!", "Sound");
      types.Add("ssce", "SoundScenery");
      types.Add("snde", "SoundEnvironment");
      types.Add("scen", "Scenery");
      types.Add("senv", "ShaderEnvironment");
      types.Add("soso", "ShaderModel");
      types.Add("sotr", "ShaderTransparencyGeneric");
      types.Add("swat", "ShaderTransparentWater");
      types.Add("sgla", "ShaderTransparentGlass");
      types.Add("smet", "ShaderTransparentMeter");
      types.Add("spla", "ShaderTransparentPlasma");
      types.Add("schi", "ShaderTransparentChicago");
      types.Add("sky ", "Sky");
      types.Add("trak", "CameraTrack");
      types.Add("unhi", "UnitHUDInterface");
      types.Add("ustr", "UnicodeStringlist");
      types.Add("scex", "ShaderTransparentChicagoExtended");
      types.Add("tagc", "TagCollection");
      types.Add("foot", "MaterialEffects");
      types.Add("str#", "StringList");
      types.Add("colo", "ColorTable");
      types.Add("flag", "Flag"); 
      types.Add("Soul", "UIWidgetCollection");
      types.Add("vehi", "Vehicle");
      types.Add("vcky", "Virtualkeyboard");
      types.Add("weap", "Weapon");
      types.Add("wphi", "WeaponHUDInterface");
      types.Add("rain", "WeatherParticleSystem");
      types.Add("wind", "Wind");
      types.Add("mode", "Model");
      types.Add("plac", "Placeholder");
      types.Add("shdr", "Shader");
      types.Add("unit", "Unit");
      types.Add("cdmg", "ContinuousDamageEffect");
    }

    public static string GetType(string abbreviation)
    {
      return (string)(types[abbreviation]);
    }
  }
}