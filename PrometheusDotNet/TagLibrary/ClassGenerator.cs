/* ---------------------------------------------------------------
 * Prometheus
 * Bungie Map-based Multi-Game Editing Kit
 * 2004-2005, Halo-Dev
 * ---------------------------------------------------------------
 * File        : ClassGenerator.cs
 * Author      : MonoxideC
 * Co-Authors  : 
 * ---------------------------------------------------------------
 */

using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Xml;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using TagLibrary.Types;

namespace TagLibrary
{
	/// <summary>
	/// Generates a c# class for a tag definition based on an XML plugin file.
	/// </summary>
  public class ClassGenerator
  {
    static public bool DebugMode = false;
    private OutputLanguage outputLanguage = OutputLanguage.CSharp;

    public OutputLanguage OutputLanguage
    {
      get { return outputLanguage; }
      set { outputLanguage = value; }
    }

    public string GenerateClass(XmlNode node)
    {
      CodeDomProvider provider;
      
      if (this.outputLanguage == OutputLanguage.CSharp)
      {
        provider = new CSharpCodeProvider();
      }
      else if (this.outputLanguage == OutputLanguage.VB)
      {
        provider = new VBCodeProvider();
      }
      else
      {
        throw new Exception("The specified output language is unsupported: " + this.outputLanguage.ToString());
      }

      ICodeGenerator generator = provider.CreateGenerator();
      CodeGeneratorOptions options = new CodeGeneratorOptions();
      options.BlankLinesBetweenMembers = false;
      options.BracingStyle = "C";
      options.IndentString = "  ";

      StringCollection structs = new StringCollection();
      
      // Get the namespace and class type from the XML.
      XmlNode platformNode = node.SelectSingleNode("platform");
      XmlNode nameNode = node.SelectSingleNode("name");
      string className = nameNode.InnerText;
      string parentType = TagAbbreviationTable.GetType(nameNode.Attributes["parenttype"].InnerText);

      // Setup the namespace and imports.
      CodeNamespace ns = new CodeNamespace("TagLibrary." + platformNode.InnerText);
      
      string headerLine1 = String.Format("Prometheus Tag Library - {0}", platformNode.InnerText);
      string userName = WindowsIdentity.GetCurrent().Name.ToString();
      string objectName = String.Format("Tag definition for '{0}'", className);
      if (parentType != null) objectName += String.Format(" (derived from '{0}')", parentType);
      string buildInfo = String.Format("Generated on {0} at {1} by {2}", DateTime.Today.ToShortDateString(),
        DateTime.Now.ToShortTimeString(), userName);
      string commentBorder = new string('-', LongestStringLength(headerLine1, buildInfo, objectName));
      
      ns.Comments.Add(new CodeCommentStatement(commentBorder));
      ns.Comments.Add(new CodeCommentStatement(headerLine1));
      ns.Comments.Add(new CodeCommentStatement(objectName));
      ns.Comments.Add(new CodeCommentStatement(buildInfo));
      ns.Comments.Add(new CodeCommentStatement(commentBorder));
      
      ns.Imports.Add(new CodeNamespaceImport("System.IO"));
      ns.Imports.Add(new CodeNamespaceImport("TagLibrary.Types"));
      
      // Setup the class type declaration.
      CodeTypeDeclaration classType = new CodeTypeDeclaration(className);
      classType.IsClass = true;
      classType.TypeAttributes = TypeAttributes.Public;
      if (parentType != null)
        classType.BaseTypes.Add(parentType);
      else
        classType.BaseTypes.Add("IBlock");

      // Write the Main block field.
      // TODO: Perhaps look into making this a property in the future.
      XmlNode mainNode = node.SelectSingleNode("//struct");
      string mainBlockName = mainNode.Attributes["name"].InnerText + "Block";
      CodeMemberField mainField = new CodeMemberField(mainBlockName, className + "Values");
      mainField.InitExpression = new CodeObjectCreateExpression(mainBlockName, new CodeExpression[] {});
      classType.Members.Add(mainField);

      // Create the standard methods.
      classType.Members.Add(CreateReadWriteMethod("Read", parentType, "BinaryReader", "reader", className));
      classType.Members.Add(CreateReadWriteMethod("ReadChildData", parentType, "BinaryReader", "reader", className));
//      classType.Members.Add(CreateReadWriteMethod("Write", parentType, "BinaryWriter", "writer", className));
//      classType.Members.Add(CreateReadWriteMethod("WriteChildData", parentType, "BinaryWriter", "writer", className));

      // Select all struct nodes in the document.
      XmlNodeList structsList = node.SelectNodes("//struct");
      
      // Generate all of the block classes.
      foreach (XmlNode structNode in structsList)
      {
        string nameOfClass = structNode.Attributes["name"].InnerText;
        
        // If the class already exists, don't generate it again.
        if (!structs.Contains(nameOfClass))
        {
          CodeTypeDeclaration ct = GenerateBlockClass(structNode);
          classType.Members.Add(ct);
          structs.Add(nameOfClass);
        }
      }
      ns.Types.Add(classType);
      
      // Generate the code and return it as a string.
      StreamWriter writer = new StreamWriter(new MemoryStream());
      generator.GenerateCodeFromNamespace(ns, writer, options);
      writer.Flush();
      StreamReader reader = new StreamReader(writer.BaseStream);
      reader.BaseStream.Position = 0;
      string code = reader.ReadToEnd();
      return code;
    }

    /// <summary>
    /// Generates a c# tag block class from a struct node.
    /// </summary>
    protected CodeTypeDeclaration GenerateBlockClass(XmlNode node)
    {
      // Setup the class type declaration.
      string nameOfClass = node.Attributes["name"].InnerText + "Block";
      CodeTypeDeclaration classType = new CodeTypeDeclaration(nameOfClass);
      classType.IsClass = true;
      classType.TypeAttributes = TypeAttributes.Public;
      classType.BaseTypes.Add("IBlock");

      // Append the private members.
      XmlNodeList fieldList = node.SelectNodes("value");
      foreach (XmlNode fieldNode in fieldList)
      {
        try
        {
          if (fieldNode.Attributes["type"].InnerText == "Custom") continue;
          classType.Members.Add(GeneratePrivateMember(fieldNode));
        }
        catch (Exception ex)
        {
          throw new Exception(fieldNode.Attributes["type"].InnerText + " - GeneratePrivateMember on " + fieldNode.Attributes["name"].InnerText + " : " +  ex.Message);
        }
      }
      
      // Generate the constructor.
      CodeConstructor constructor = new CodeConstructor();
      constructor.Attributes = MemberAttributes.Public;	
      classType.Members.Add(constructor);

      foreach (XmlNode fieldNode in fieldList)
      {
        try
        {
          if (fieldNode.Attributes["type"].InnerText == "Custom") continue;
          if (fieldNode.Attributes["type"].InnerText == "Block")
          {
            string blockName = fieldNode.Attributes["struct"].InnerText + "Block";
            string collectionName = blockName + "Collection";
            string linkName = GlobalMethods.MakePrivateName(fieldNode.Attributes["name"].InnerText);
            string memberName = linkName + "Collection";
            //fix underscore problem
            //string publicName = linkName.Substring(0, 1).ToUpper() + linkName.Substring(1);
            string publicName = linkName.Substring(1, 1).ToUpper() + linkName.Substring(2);
            
            // Add the block collection.
            classType.Members.Add(GenerateBlockCollection(blockName));
            classType.Members.Add(new CodeMemberField(collectionName, memberName));
            
            constructor.Statements.Add(new CodeAssignStatement(
              new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), memberName),
              new CodeObjectCreateExpression(collectionName, new CodeFieldReferenceExpression(
              new CodeThisReferenceExpression(), linkName))));
            
            // Generate the public collection accessor.
            CodeMemberProperty collectionAccessor = new CodeMemberProperty();
            collectionAccessor.Name = publicName;
            collectionAccessor.Type = new CodeTypeReference(collectionName);
            collectionAccessor.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            //collectionAccessor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(int), "index"));
            collectionAccessor.HasGet = true;
            collectionAccessor.HasSet = false;
            collectionAccessor.GetStatements.Add(
              new CodeMethodReturnStatement(
              new CodeFieldReferenceExpression(
              new CodeThisReferenceExpression(), memberName)));
            classType.Members.Add(collectionAccessor);
          }
        }
        catch (Exception ex)
        {
          throw new Exception(fieldNode.Attributes["type"].InnerText + " - GenerateBlockCollection on " + fieldNode.Attributes["name"].InnerText + " : " + ex.Message);
        }
      }

      // Generate the other public accessors.
      foreach (XmlNode fieldNode in fieldList)
      {
        try
        {
          // We don't need public accessors for pad or block fields.
          if (fieldNode.Attributes["type"].InnerText == "Pad") continue;
          if (fieldNode.Attributes["type"].InnerText == "Block") continue;
          if (fieldNode.Attributes["type"].InnerText == "Custom") continue;
          if (fieldNode.Attributes["type"].InnerText == "Skip") continue;
          classType.Members.Add(GeneratePublicAccessors(fieldNode));
        }
        catch (Exception ex)
        {
          throw new Exception(fieldNode.Attributes["type"].InnerText + " - GeneratePublicAccessors on " + fieldNode.Attributes["name"].InnerText + " : " +  ex.Message);
        }
      }
      
      // Finish generating the constructor.
      foreach (XmlNode fieldNode in fieldList)
      {
        try
        {
          if (fieldNode.Attributes["type"].InnerText == "Custom") continue;
          CodeStatement constructorStatement = GenerateConstructorLogic(fieldNode);
          if (constructorStatement != null) constructor.Statements.Add(constructorStatement);
        }
        catch (Exception ex)
        {
          throw new Exception(fieldNode.Attributes["type"].InnerText + " - GenerateConstructorLogic on " + fieldNode.Attributes["name"].InnerText + " : " +  ex.Message);
        }
      }

      // Generate the Read method.
      CodeMemberMethod readMethod = new CodeMemberMethod();
      readMethod.ReturnType = new CodeTypeReference(typeof(void));
      readMethod.Name = "Read";
      readMethod.Parameters.Add(new CodeParameterDeclarationExpression("BinaryReader", "reader"));
      readMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;

      foreach (XmlNode fieldNode in fieldList)
      {
        if (fieldNode.Attributes["type"].InnerText == "Custom") continue;
        string name = GlobalMethods.MakePrivateName(fieldNode.Attributes["name"].InnerText);
        // *******************************************************************************
        // Language specific statement - must be changed to be cross-language compatible.
        // *******************************************************************************
        readMethod.Statements.Add(new CodeSnippetStatement(name + ".Read(reader);"));
      }
      classType.Members.Add(readMethod);
 
      // Generate the ReadChildData method.
      // This reads strings, child blocks, and data fields in the order that they appear.
      CodeMemberMethod readChildDataMethod = new CodeMemberMethod();
      readChildDataMethod.ReturnType = new CodeTypeReference(typeof(void));
      readChildDataMethod.Name = "ReadChildData";
      readChildDataMethod.Parameters.Add(new CodeParameterDeclarationExpression("BinaryReader", "reader"));
      readChildDataMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;

      foreach (XmlNode fieldNode in fieldList)
      {
        if (fieldNode.Attributes["type"].InnerText == "Block")
        {
          readChildDataMethod.Statements.Add(new CodeVariableDeclarationStatement(typeof(int), "x", new CodePrimitiveExpression(0)));
          break;
        }
      }
    
      foreach (XmlNode fieldNode in fieldList)
      {
        string name = GlobalMethods.MakePrivateName(fieldNode.Attributes["name"].InnerText);
        string type = fieldNode.Attributes["type"].InnerText;
        if (type == "Block")
        {
          //fix underscore problem
          //string blockCollection = name.Substring(0, 1).ToUpper() + name.Substring(1);
          string blockCollection = name.Substring(1, 1).ToUpper() + name.Substring(2);

          CodeIterationStatement forLoopRead = new CodeIterationStatement(
            new CodeAssignStatement( new CodeVariableReferenceExpression("x"), new CodePrimitiveExpression(0) ),
            new CodeBinaryOperatorExpression( new CodeVariableReferenceExpression("x"), 
            CodeBinaryOperatorType.LessThan, new CodeFieldReferenceExpression(
              new CodeVariableReferenceExpression(name), "Count")),
            new CodeAssignStatement( new CodeVariableReferenceExpression("x"), new CodeBinaryOperatorExpression( 
            new CodeVariableReferenceExpression("x"), CodeBinaryOperatorType.Add, new CodePrimitiveExpression(1))),
            new CodeStatement[]
            {
              new CodeExpressionStatement(
              new CodeMethodInvokeExpression(new CodeVariableReferenceExpression(blockCollection), "AddNew", new CodeExpression[] {})),
              new CodeExpressionStatement(
                new CodeMethodInvokeExpression(new CodeIndexerExpression(
                new CodeVariableReferenceExpression(blockCollection), new CodeVariableReferenceExpression("x")), "Read",
                new CodeVariableReferenceExpression("reader"))),
          });
          readChildDataMethod.Statements.Add(forLoopRead);

          CodeIterationStatement forLoopReadChildren = new CodeIterationStatement(
            new CodeAssignStatement( new CodeVariableReferenceExpression("x"), new CodePrimitiveExpression(0) ),
            new CodeBinaryOperatorExpression( new CodeVariableReferenceExpression("x"), 
            CodeBinaryOperatorType.LessThan, new CodeFieldReferenceExpression(
            new CodeVariableReferenceExpression(name), "Count")),
            new CodeAssignStatement( new CodeVariableReferenceExpression("x"), new CodeBinaryOperatorExpression( 
            new CodeVariableReferenceExpression("x"), CodeBinaryOperatorType.Add, new CodePrimitiveExpression(1) )),
            new CodeStatement[]
            {
              new CodeExpressionStatement(
                new CodeMethodInvokeExpression(new CodeIndexerExpression(
                new CodeVariableReferenceExpression(blockCollection), new CodeVariableReferenceExpression("x")), "ReadChildData",
                new CodeVariableReferenceExpression("reader"))),
          });
          readChildDataMethod.Statements.Add(forLoopReadChildren);
        }

        if (type == "TagReference")
        {
          CodeMethodInvokeExpression readString = new CodeMethodInvokeExpression(
            new CodeVariableReferenceExpression(name), "ReadString", new CodeVariableReferenceExpression("reader"));
          readChildDataMethod.Statements.Add(readString);
        }

        if (type == "Data")
        {
          CodeMethodInvokeExpression readBinary = new CodeMethodInvokeExpression(
            new CodeVariableReferenceExpression(name), "ReadBinary", new CodeVariableReferenceExpression("reader"));
          readChildDataMethod.Statements.Add(readBinary);
        }
      }
      classType.Members.Add(readChildDataMethod);

//      // Append the Write method.
//      sb.Append("public void Write(BinaryWriter writer)\r\n");
//      sb.Append("{\r\n");
//      foreach (XmlNode fieldNode in fieldList)
//      {
//        if (fieldNode.Attributes["type"].InnerText == "Custom") continue;
//        string name = "_" + GlobalMethods.MakePrivateName(fieldNode.Attributes["name"].InnerText);
//        sb.Append("    " + name + ".Write(writer);\r\n");
//      }
//      sb.Append("}\r\n");
//
//      // Append the WriteChildData method.
//      // This writes strings, child blocks, and data fields in the order that they appear.
//      sb.Append("public void WriteChildData(BinaryWriter writer)\r\n");
//      sb.Append("{\r\n");      
//      foreach (XmlNode fieldNode in fieldList)
//      {
//        string name = "_" + GlobalMethods.MakePrivateName(fieldNode.Attributes["name"].InnerText);
//        string type = fieldNode.Attributes["type"].InnerText;
//        if (type == "Block")
//        {
//          string count = name + ".Count";
//          string blockCollection = name.Substring(1, 1).ToUpper() + name.Substring(2);
//          sb.Append(name + ".UpdateReflexiveOffset(writer);\r\n");
//          sb.Append("for (int x=0; x<" + count + "; x++)\r\n");
//          sb.Append("{\r\n");
//          sb.Append("  " + blockCollection + "[x].Write(writer);\r\n");
//          sb.Append("}\r\n");
//          sb.Append("for (int x=0; x<" + count + "; x++)\r\n");
//          sb.Append("  " + blockCollection + "[x].WriteChildData(writer);\r\n");
//        }
//        if (type == "TagReference")
//        {
//          sb.Append(name + ".WriteString(writer);\r\n");
//        }
//        if (type == "Data")
//        {
//          sb.Append(name + ".WriteBinary(writer);\r\n");
//        }
//      }
//      sb.Append("}\r\n");

      //close up the class
      return classType;
    }

    protected CodeTypeDeclaration GenerateBlockCollection(string blockName)
    {
      // Setup the class type declaration.
      string collectionName = blockName + "Collection";
      CodeTypeDeclaration classType = new CodeTypeDeclaration(collectionName);
      classType.IsClass = true;
      classType.TypeAttributes = TypeAttributes.Public;
      classType.BaseTypes.Add("System.Collections.CollectionBase");

      CodeMemberField linkedBlock = new CodeMemberField("Block", "linkedBlock");
      linkedBlock.Attributes = MemberAttributes.Private;
      classType.Members.Add(linkedBlock);

      CodeConstructor constructor = new CodeConstructor();
      constructor.Attributes = MemberAttributes.Public;	
      constructor.Parameters.Add(new CodeParameterDeclarationExpression("Block", "linkedBlock"));
      // *******************************************************************************
      // Language specific statement - must be changed to be cross-language compatible.
      // *******************************************************************************
      constructor.Statements.Add(new CodeSnippetStatement("this.linkedBlock = linkedBlock;"));
      classType.Members.Add(constructor);

      CodeMemberMethod addMethod = new CodeMemberMethod();
      addMethod.ReturnType = new CodeTypeReference(typeof(void));
      addMethod.Name = "Add";
      addMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
      addMethod.Parameters.Add(new CodeParameterDeclarationExpression(blockName, "block"));
      // *******************************************************************************
      // Language specific statement - must be changed to be cross-language compatible.
      // *******************************************************************************
      addMethod.Statements.Add(new CodeSnippetStatement("InnerList.Add(block);"));
      // *******************************************************************************
      // Language specific statement - must be changed to be cross-language compatible.
      // *******************************************************************************
      addMethod.Statements.Add(new CodeSnippetStatement(
        "if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;"));
      classType.Members.Add(addMethod);
      
      CodeMemberMethod addNewMethod = new CodeMemberMethod();
      addNewMethod.ReturnType = new CodeTypeReference(typeof(void));
      addNewMethod.Name = "AddNew";
      addNewMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
      addNewMethod.Statements.Add(new CodeMethodInvokeExpression(
        new CodeThisReferenceExpression(), "Add", new CodeObjectCreateExpression(blockName, new CodeExpression[] {})));
      classType.Members.Add(addNewMethod);

      CodeMemberMethod removeMethod = new CodeMemberMethod();
      removeMethod.ReturnType = new CodeTypeReference(typeof(void));
      removeMethod.Name = "Remove";
      removeMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
      removeMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(int), "index"));
      // *******************************************************************************
      // Language specific statement - must be changed to be cross-language compatible.
      // *******************************************************************************
      removeMethod.Statements.Add(new CodeSnippetStatement("InnerList.RemoveAt(index);"));
      // *******************************************************************************
      // Language specific statement - must be changed to be cross-language compatible.
      // *******************************************************************************
      removeMethod.Statements.Add(new CodeSnippetStatement(
        "if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;"));
      classType.Members.Add(removeMethod);

      CodeMemberProperty itemAccessor = new CodeMemberProperty();
      itemAccessor.Name = "Item";
      itemAccessor.Type = new CodeTypeReference(blockName);
      itemAccessor.Attributes = MemberAttributes.Public | MemberAttributes.Final;
      itemAccessor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(int), "index"));
      itemAccessor.HasGet = true;
      itemAccessor.HasSet = false;
      itemAccessor.GetStatements.Add(new CodeMethodReturnStatement(
        new CodeCastExpression(blockName, 
        new CodeArrayIndexerExpression(new CodeFieldReferenceExpression(
        new CodeThisReferenceExpression(), "InnerList"),
        new CodeArgumentReferenceExpression("index")))));
      classType.Members.Add(itemAccessor);

      return classType;
    }

    /// <summary>
    /// Creates one of the standard Read/Write methods for the class.
    /// </summary>
    private CodeMemberMethod CreateReadWriteMethod(
      string methodName, string parentType, string paramObject, string paramName, string className)
    {
      CodeMemberMethod method = new CodeMemberMethod();
      method.Name = methodName;
      method.Parameters.Add(new CodeParameterDeclarationExpression(paramObject, paramName));
      method.ReturnType = new CodeTypeReference(typeof(void));
      if (parentType != null)
      {
        method.Attributes = MemberAttributes.Public | MemberAttributes.Override;
        // *******************************************************************************
        // Language specific statement - must be changed to be cross-language compatible.
        // *******************************************************************************
        method.Statements.Add(new CodeSnippetStatement(String.Format("base.{0}({1});", methodName, paramName)));
      }
      else
      { 
        method.Attributes = MemberAttributes.Public;
      }
      // *******************************************************************************
      // Language specific statement - must be changed to be cross-language compatible.
      // *******************************************************************************
      method.Statements.Add(new CodeSnippetStatement(
        String.Format("{0}Values.{1}({2});", className, methodName, paramName)));
      return method;
    }

    protected CodeMemberField GeneratePrivateMember(XmlNode node)
    {
      string fieldClassName = node.Attributes["type"].InnerText;
      string fullName = "TagLibrary.Types" + "." + fieldClassName + "CodeGenerator";
      return (CodeMemberField)InvokeMethod(fullName, "GeneratePrivateMember", node);
    }

    protected CodeMemberProperty GeneratePublicAccessors(XmlNode node)
    {
      string fieldClassName = node.Attributes["type"].InnerText;
      string fullName = "TagLibrary.Types" + "." + fieldClassName + "CodeGenerator";
      return (CodeMemberProperty)InvokeMethod(fullName, "GeneratePublicAccessors", node);
    }

    protected CodeStatement GenerateConstructorLogic(XmlNode node)
    {
      string fieldClassName = node.Attributes["type"].InnerText;
      string fullName = "TagLibrary.Types" + "." + fieldClassName + "CodeGenerator";
      return (CodeStatement)InvokeMethod(fullName, "GenerateConstructorLogic", node);
    }

    protected object InvokeMethod(string fullName, string methodName, params object[] args)
    {
      object instance = Assembly.GetExecutingAssembly().CreateInstance(fullName, true, BindingFlags.CreateInstance, null, args, null, null);
      Type type = instance.GetType();

      MethodInfo mi = type.GetMethod(methodName);
      object r = mi.Invoke(instance, null);
      return r; 
    }

    static ClassGenerator()
    {
    }

    public class StringCollection : CollectionBase
    {
      public void Add(string value)
      {
        InnerList.Add(value);
      }
      public bool Contains(string value)
      {
        foreach (string s in InnerList)
        {
          if (s == value) return true;
        }
        return false;
      }
    }

    public int LongestStringLength(params string[] values)
    {
      int longestString = 0;
      foreach (string s in values)
      {
        if (s.Length > longestString) longestString = s.Length;
      }
      return longestString;
    }
	}
  public enum OutputLanguage
  {
    CSharp,
    VB
  }
}