using System;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace TagLibrary
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
  public class TagUtils
  {
    public TagUtils()
    {
    }
    static public void GenerateClassStructure(string InputPath, string OutputPath)
    {
      string result = "";
      XmlNode platform_node;
      string output_file;
      int k = Application.StartupPath.IndexOf(@"PrometheusDotNet\");

      if(k == -1)
        throw new Exception("Folder hierarchy does not contain 'PrometheusDotNet', cannot output source files.");

      string output_folder = Application.StartupPath.Substring(0, k) + @"PrometheusDotNet\TagLibrary\";

      XmlDocument doc = new XmlDocument();
      doc.Load(InputPath);
      k = InputPath.LastIndexOf('\\');

      Trace.WriteLine("================Generating file from: " + InputPath.Substring(k+1, InputPath.Length - (k+1))+
        "====================");
      ClassGenerator generator = new ClassGenerator();

      try
      {
        result = generator.GenerateClass(doc.SelectSingleNode("//xml"));
      }
      catch(Exception e)
      {
        Trace.WriteLine(e.Message + "( " + InputPath + ")");
      }
      finally
      {
        platform_node = doc.SelectSingleNode("//xml//platform");
          
        if(platform_node.InnerText == "")
          throw new Exception("Xml file did not specify platform: " + InputPath);

        //create the output filename based on project file path, xml platform node, and xml file name
        output_file = output_folder + platform_node.InnerText + @"\" + InputPath.Substring(k+1, InputPath.Length - (k+4)) + "cs";
          
        StreamWriter sw = new StreamWriter(output_file);
        sw.Write(result);        
        sw.Close();
//        if(File.Exists(output_file) == true)
//        {
//          FileAttributes fa = File.GetAttributes(output_file);
//
//          if((fa & FileAttributes.ReadOnly) == 0)
//          {
//            StreamWriter sw = new StreamWriter(output_file);
//            sw.Write(result);        
//            sw.Close();
//          }
//        }
      }
    }
    static public void GenerateMultiClassStructure(string InputPath, string OutputPath)
    {
      string result = "";
      XmlNode platform_node;
      string[] xml_list = null;
      string output_file;
      int k = Application.StartupPath.IndexOf(@"PrometheusDotNet\");

      if(k == -1)
        throw new Exception("Folder hierarchy does not contain 'PrometheusDotNet', cannot output source files.");

      string output_folder = Application.StartupPath.Substring(0, k) + @"PrometheusDotNet\TagLibrary\";

      //todo: get a list of xml files based on InputPath
      xml_list = Directory.GetFiles(InputPath, "*.xml");

      foreach (string filename in xml_list)
      {
        GenerateClassStructure(InputPath, OutputPath);
      }
    }
	}
}
