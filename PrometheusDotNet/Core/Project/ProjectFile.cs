/* ---------------------------------------------------------------
 * Prometheus
 * Bungie Map-based Multi-Game Editing Kit
 * 2004-2005, Halo-Dev
 * ---------------------------------------------------------------
 * Filename    : ProjectFile.cs
 * Author      : MonoxideC
 * Co-Authors  : 
 * ---------------------------------------------------------------
 */

using System;
using System.Collections;
using System.IO;
using System.Xml;
using Prometheus.Core;
using Prometheus.Core.Tags;

namespace Prometheus.Core.Project
{
	/// <summary>
	/// A Prometheus project file.
	/// </summary>
  public class ProjectFile
	{
    private Version version;
    private string author;
    private string description;
	  
    // NOTE: Eventually, we will need to be able to include multiple templates
    // so that the map can be compiled to multiple target platforms.
    private ProjectTemplate template;

    private string mapName;
    private string filename;
    private string uiText;
    private string uiScreenShotFile;

    private ProjectTagCollection tags;

    /// <summary>
    /// Gets the version of the project.
    /// </summary>
	  public Version Version
	  {
	    get { return version; }
	  }

	  /// <summary>
	  /// Gets or sets the author of the project.
	  /// </summary>
    public string Author
	  {
	    get { return author; }
      set { author = value; }
	  }

	  /// <summary>
	  /// Gets or sets the description of the project.
	  /// </summary>
    public string Description
	  {
	    get { return description; }
      set { description = value; }
	  }

	  /// <summary>
	  /// Gets the template associated with this project.
	  /// </summary>
    public ProjectTemplate Template
	  {
	    get { return template; }
	  }

	  /// <summary>
	  /// Gets or sets the map name of the project.
	  /// </summary>
    public string MapName
	  {
	    get { return mapName; }
    	set { mapName = value; }
	  }

    /// <summary>
    /// Gets or sets the filename to be used when compiling the project to a map file.
    /// </summary>
    public string Filename
    {
      get { return filename; }
      set { filename = value; }
    }


	  /// <summary>
	  /// Gets or sets the text displayed in the UI.
	  /// </summary>
    public string UIText
	  {
	    get { return uiText; }
      set { uiText = value; }
	  }

	  /// <summary>
	  /// Gets or sets filename of the image that will be dispalyed in the UI.
	  /// </summary>
    public string UiScreenShotFile
	  {
	    get { return uiScreenShotFile; }
      set { uiScreenShotFile = value; }
	  }

	  /// <summary>
	  /// Returns a collection of tags contained within this project.
	  /// </summary>
    public ProjectTagCollection Tags
	  {
	    get { return tags; }
	  }

    public ProjectFile(string mapName, string author, ProjectTemplate template)
    {
      this.mapName = mapName;
      this.author = author;
      this.version = new Version();
      this.template = template;
      tags = new ProjectTagCollection();
    }

    public ProjectFile(XmlDocument document)
    {
      XmlNode projectNode = document.SelectSingleNode("//project");
      XmlNode infoNode = projectNode.SelectSingleNode("info");
      this.author = infoNode.SelectSingleNode("author").InnerText;
      this.description = infoNode.SelectSingleNode("description").InnerText;
      this.version = new Version(infoNode.SelectSingleNode("version").InnerText);

      XmlNode templatesNode = projectNode.SelectSingleNode("templates");
      string templateName = templatesNode.SelectSingleNode("template").InnerText;
      this.template = ProjectTemplates.GetTemplate(templateName);

      XmlNode mapInfoNode = projectNode.SelectSingleNode("mapinfo");
      this.mapName = mapInfoNode.SelectSingleNode("name").InnerText;
      this.filename = mapInfoNode.SelectSingleNode("filename").InnerText;
      this.uiText = mapInfoNode.SelectSingleNode("uitext").InnerText;
      this.uiScreenShotFile = mapInfoNode.SelectSingleNode("uiscreenshot").InnerText;

      this.tags = new ProjectTagCollection();
      XmlNodeList taglistNodeList = projectNode.SelectNodes("taglist//tag");
      foreach (XmlNode tagNode in taglistNodeList)
      {
        string templateID = tagNode.Attributes["template_id"].InnerText;
        string path = tagNode.InnerText;
        ProjectTag tag = new ProjectTag(templateID, path);
        this.tags.Add(tag);
      }
    }

    /// <summary>
    /// Returns a XML-formatted string containing the ProjectFile data.
    /// </summary>
    public string SaveToXML()
    {
      XmlTextWriter writer = new XmlTextWriter(new MemoryStream(), new System.Text.ASCIIEncoding());
      writer.Formatting = Formatting.Indented;
      writer.WriteStartDocument();
      writer.WriteStartElement("project");
      /**/writer.WriteStartElement("info");
      /**//**/writer.WriteElementString("author", this.author);
      /**//**/writer.WriteElementString("description", this.description);
      /**//**/writer.WriteElementString("version", this.version.ToString());
      /**/writer.WriteEndElement();
      /**/writer.WriteStartElement("templates");
      /**//**/writer.WriteElementString("template", this.template.Name);
      /**/writer.WriteEndElement();
      /**/writer.WriteStartElement("mapinfo");
      /**//**/writer.WriteElementString("name", this.mapName);
      /**//**/writer.WriteElementString("filename", this.filename);
      /**//**/writer.WriteElementString("uitext", this.uiText);
      /**//**/writer.WriteElementString("uiscreenshot", this.uiScreenShotFile);
      /**/writer.WriteEndElement();
      /**/writer.WriteStartElement("taglist");

      foreach (ProjectTag tag in tags)
      {
        writer.WriteStartElement("tag");
        writer.WriteAttributeString("template_id", tag.TemplateElement);
        writer.WriteString(tag.Path);
        writer.WriteEndElement();
      }

      /**/writer.WriteEndElement();
      writer.WriteEndElement();
      writer.WriteEndDocument();
      writer.Flush();
      
      byte[] data = new byte[writer.BaseStream.Length];
      writer.BaseStream.Position = 0;
      writer.BaseStream.Read(data, 0, data.Length);
      string s = System.Text.Encoding.ASCII.GetString(data);
      return s;
    }
	}

  /// <summary>
  /// Provides versioning information,
  /// </summary>
  public class Version
  {
    private int minorRevision;
    private int majorRevision;
    private VersionDesignation designation;

    /// <summary>
    /// Gets the minor revision number.
    /// </summary>
    public int MinorRevision
    {
      get { return minorRevision; }
    }

    /// <summary>
    /// Gets the major revision number.
    /// </summary>
    public int MajorRevision
    {
      get { return majorRevision; }
    }

    /// <summary>
    /// Gets or sets the designation (alpha, beta, release)
    /// </summary>
    public VersionDesignation Designation
    {
      get { return designation; }
      set { designation = value; }
    }

    public Version() : this("") { ; }

    public Version(string text)
    {
      string[] parts = text.Split('.');
      if (parts.Length == 2)
      {
        try
        {
          this.minorRevision = Convert.ToInt32(parts[0]);
          this.majorRevision = Convert.ToInt32(parts[1].Substring(0, parts[1].Length-1));
          if (parts[1].Substring(parts[1].Length-1, 1) == "a")
          {
            this.designation = VersionDesignation.Alpha;
          }
          else if (parts[1].Substring(parts[1].Length-1, 1) == "b")
          {
            this.designation = VersionDesignation.Beta;
          }
          else
          {
            this.designation = VersionDesignation.Release;
          }
        }
        catch (Exception ex)
        {
          throw new PrometheusException("Could not parse version string '" + text + "'", ex, true);
        }
      }
      else
      {
        minorRevision = 0;
        majorRevision = 0;
        designation = VersionDesignation.Alpha;
      }
    }

    /// <summary>
    /// Incremements to the next major revision.
    /// </summary>
    public void IncremenMajorRevision()
    {
      this.majorRevision++;
    }

    /// <summary>
    /// Incremements to the next minor revision.
    /// </summary>
    public void IncremenMinorRevision()
    {
      this.minorRevision++;
    }

    /// <summary>
    /// Manually sets the version.
    /// </summary>
    public void SetVersion(int major, int minor, VersionDesignation designation)
    {
      this.majorRevision = major;
      this.minorRevision = minor;
      this.designation = designation;
    }

    public override string ToString()
    {
      string s = this.majorRevision.ToString() + "." + this.minorRevision.ToString();
      if (designation == VersionDesignation.Alpha) s += "a";
      if (designation == VersionDesignation.Beta) s += "b";
      return s;
    }
  }

  public enum VersionDesignation
  {
    Alpha,
    Beta,
    Release
  }

  /// <summary>
  /// A type-safe collection of Tag objects.
  /// </summary>
  public class ProjectTagCollection : CollectionBase
  {
    public void Add(ProjectTag tag)
    {
      InnerList.Add(tag);
    }
    public ProjectTag this[int index]
    {
      get { return (InnerList[index] as ProjectTag); }
    }
    public ProjectTag this[string templateElement]
    {
      get
      {
        foreach (ProjectTag tag in InnerList)
        {
          if (tag.TemplateElement.ToLower() == templateElement.ToLower())
            return tag;
        }
        return null;
      }
    }
  }

  /// <summary>
  /// A tag inside a project file.
  /// </summary>
  public class ProjectTag
  {
    private string templateElement;
    private string path;
    
    /// <summary>
    /// Gets the string identifier of the template element that this tag corresponds to.
    /// </summary>
    public string TemplateElement
    {
      get { return templateElement; }
    }

    /// <summary>
    /// Gets the path of the tag.
    /// </summary>
    public string Path
    {
      get { return path; }
    }

    public TagFileName TagFileName
    {
      get
      {
        //todo: figure out how to handle the version stuff
        TagFileName tfn = new TagFileName(path, ProjectManager.Version);
        return(tfn);
      }
    }

    public ProjectTag(string templateElement, string path)
    {
      this.templateElement = templateElement;
      this.path = path;
    }
  }

	/// <summary>
	/// A global static collection of all available project file templates.
	/// </summary>
  public class ProjectTemplates
	{
    private static ProjectTemplateCollection templates;

    /// <summary>
    /// Gets the template with the specified name.
    /// </summary>
    /// <param name="name">The name of the template you wish to retreive.</param>
    public static ProjectTemplate GetTemplate(string name)
    {
      return templates[name];
    }

    static ProjectTemplates()
    {
      templates = new ProjectTemplateCollection();
      templates.Add(new ProjectTemplate("HaloPCMultiplayer",
        new TemplateTag("scenario", "Scenario"),
        new TemplateTag("globals", "Globals", @"globals\globals"),
        new TemplateTag("tag collection", "All Scenario Types", @"ui\ui_tags_loaded_all_scenario_types"),
        new TemplateTag("bitmap", "Background", @"ui\shell\bitmaps\background"),
        new TemplateTag("unicode string list", "Loading", @"ui\shell\strings\loading"),
        new TemplateTag("unicode string list", "MP Map List", @"ui\shell\main_menu\mp_map_list"),
        new TemplateTag("bitmap", "Disconnect Icon", @"ui\bitmaps\trouble_brewing"),
        new TemplateTag("sound", "Cursor", @"sound\sfx\ui\cursor"),
        new TemplateTag("sound", "Forward", @"sound\sfx\ui\forward"),
        new TemplateTag("sound", "Back", @"sound\sfx\ui\back"),
        new TemplateTag("tag collection", "Multiplayer Scenario Types", @"ui\ui_tags_loaded_multiplayer_scenario_type"))
      );

      templates.Add(new ProjectTemplate("Halo1XboxMultiplayer",
        new TemplateTag("scenario", "Scenario"),
        new TemplateTag("globals", "Globals", @"globals\globals"),
        new TemplateTag("tag collection", "All Scenario Types", @"ui\ui_tags_loaded_all_scenario_types"),
        new TemplateTag("bitmap", "Background", @"ui\shell\bitmaps\background"),
        new TemplateTag("unicode string list", "Loading", @"ui\shell\strings\loading"),
        new TemplateTag("unicode string list", "MP Map List", @"ui\shell\main_menu\mp_map_list"),
        new TemplateTag("bitmap", "Disconnect Icon", @"ui\bitmaps\trouble_brewing"),
        new TemplateTag("sound", "Cursor", @"sound\sfx\ui\cursor"),
        new TemplateTag("sound", "Forward", @"sound\sfx\ui\forward"),
        new TemplateTag("sound", "Back", @"sound\sfx\ui\back"),
        new TemplateTag("tag collection", "Multiplayer Scenario Types", @"ui\ui_tags_loaded_multiplayer_scenario_type"))
        );
    }
	}

  /// <summary>
  /// A type-safe collection of ProjectTemplate objects.
  /// </summary>
  public class ProjectTemplateCollection : CollectionBase
  {
    public void Add(ProjectTemplate template)
    {
      InnerList.Add(template);
    }
    public ProjectTemplate this[string name]
    {
      get
      {
        foreach (ProjectTemplate template in InnerList)
        {
          if (template.Name == name) return template;
        }
        throw new Exception("The specified ProjectTemplate was not found in the collection: " + name);
      }
    }
  }

  /// <summary>
  /// Defines the required tags that a project needs in order 
  /// to compile to a specified format.
  /// </summary>
  public class ProjectTemplate
  {
    private string name;
    private TemplateTag[] tagSet;

    public string Name
    {
      get { return name; }
    }

    public TemplateTag[] TagSet
    {
      get { return tagSet; }
    }

    public ProjectTemplate(string name, params TemplateTag[] tagSet)
    {
      this.name = name;
      this.tagSet = tagSet;
    }
  }

  /// <summary>
  /// A tag entry within a ProjectTemplate.
  /// </summary>
  public class TemplateTag
  {
    private string fileType;
    private string name;
    private string defaultFile;

    public string FileType
    {
      get { return fileType; }
    }

    public string Name
    {
      get { return name; }
    }

    public string DefaultFile
    {
      get { return defaultFile; }
    }

    public TemplateTag(string fileType, string name) : this(fileType, name, null) { ; }

    public TemplateTag(string fileType, string name, string defaultFile)
    {
      this.fileType = fileType;
      this.name = name;
      this.defaultFile = defaultFile;
    }
  }
}