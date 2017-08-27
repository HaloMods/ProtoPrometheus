/* ---------------------------------------------------------------
 * Prometheus
 * Bungie Map-based Multi-Game Editing Kit
 * 2004-2005, Halo-Dev
 * ---------------------------------------------------------------
 * Filename    : LibraryArchive.cs
 * Author      : Grenadiac
 * Co-Authors  : MonoxideC
 * ---------------------------------------------------------------
 */

/*
public class DiskFolderTagLibrary : ITagLibrary
  -used in TagBrowserDialog

public class BaseTagLibrary : ITagLibrary
  -used in Project Explorer, "Project Tags", dependency use

public class DependencyTagLibrary : ITagLibrary
  -used in Project Explorer  

public class SimpleArchiveTagLibrary : ITagLibrary
  -not used, should be removed

public class ObjectViewTagLibrary : ITagLibrary
  -tag explorer

public class ArchiveTagLibrary : ITagLibrary
  -not used, should be removed

public class ZipTagLibrary : ITagLibrary
  -used in TagLibraryManager, need to replace this class with new file system

public class MasterLibraryArchive : ITagLibrary
  -Tree View (archive view?)

Recommendations:

1) ZipTagLibrary needs to be replaced with the new file system

2) The file entry table should be a separate class.

3) The file entry should have an optional field for a "symbolic link".  Symbolic links could be used in 
the dependency views to directly access files.  That way the entry table could match up with the Tree View
and probably reduce the number of classes needed to support things.  The entry table would need to be regenerated
(or at least partially) when changes are made to the project.

4) Perhaps there could be two types of entries used in an entry table.  The first would be the simple version
used in the new file system.  The second type would be used for symbolic and dependency tables, which are used
in project designs.

5) Remove ArchiveTagLibrary and SimpleArchiveTagLibrary.  They are not used.

6) I might suggest thinking about consolidating some functionality into a parent class if code is being duplicated.
There are 8 classes derived from ITagLibrary, and it would seem to me that there could be some
overlap in functionality.  We should think hard about a way to simplify the system and consolidate code.

  */

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Xml;
using Prometheus.Core.Compiler_Gren;
using Prometheus.Core.Tags;
using TagLibrary;
using TagLibrary.Types;
using Xceed.Compression;
using Xceed.FileSystem;
using Xceed.Zip;

namespace Prometheus.Core
{
  public class TagLibraryManager
  {
    private static ZipTagLibrary haloPC;
    private static ZipTagLibrary haloXbox;
    private static ZipTagLibrary halo2Xbox;

    private static bool initialized = false;

    public static ZipTagLibrary HaloPC
    {
      get
      {
        if (!initialized)
          throw new Exception("The HaloPC Tag Library has not yet been initialized.");
        return haloPC;
      }
    }

    public static ZipTagLibrary HaloXbox
    {
      get
      {
        if (!initialized)
          throw new Exception("The HaloXbox Tag Library has not yet been initialized.");
        return haloXbox;
      }
    }

    public static ZipTagLibrary Halo2Xbox
    {
      get
      {
        if (!initialized)
          throw new Exception("The Halo2Xbox Tag Library has not yet been initialized.");
        return halo2Xbox;
      }
    }

    public static XmlDocument HaloPCMasterTagList
    {
      get
      {
        Assembly thisAssembly = Assembly.GetExecutingAssembly();
        Stream rgbxml = thisAssembly.GetManifestResourceStream("Core.Halo1MasterTagList.xml");
        XmlDocument doc = new XmlDocument();
        doc.Load(rgbxml);
        return doc;
      }
    }

    public static XmlDocument Halo1XboxMasterTagList
    {
      get
      {
        Assembly thisAssembly = Assembly.GetExecutingAssembly();
        Stream rgbxml = thisAssembly.GetManifestResourceStream("Core.Halo1MasterTagList.xml");
        XmlDocument doc = new XmlDocument();
        doc.Load(rgbxml);
        return doc;
      }
    }

    /// <summary>
    /// Initializes the Taglibrarymanager.
    /// This must be called before any of the TagLibrary objects can be accessed.
    /// </summary>
    static public void Initialize()
    {
      if (!initialized)
      {
        Xceed.Compression.Licenser.LicenseKey = "ZIN20-87A1K-SWNFN-W8AA";
        Xceed.FileSystem.Licenser.LicenseKey = "ZIN20-87A1K-SWNFN-W8AA";
        Xceed.Zip.Licenser.LicenseKey = "ZIN20-87A1K-SWNFN-W8AA";

        // TODO: Pull the paths from the OptionsManager.
        haloPC = new ZipTagLibrary(Application.StartupPath + "\\Games\\PC\\Halo\\hpc.pta", "Halo PC");
        haloXbox = new ZipTagLibrary(Application.StartupPath + "\\Games\\Xbox\\Halo\\hxb.pta", "Halo Xbox");
        halo2Xbox = new ZipTagLibrary(Application.StartupPath + "\\Games\\Xbox\\Halo 2\\h2xb.pta", "Halo 2 Xbox");
        initialized = true;
      }
    }

    /// <summary>
    /// Initializes the Taglibrarymanager.
    /// This must be called before any of the TagLibrary objects can be accessed.
    /// </summary>
    static public void Initialize(string haloPCPath, string haloXboxPath, string halo2XboxPath)
    {
      if (!initialized)
      {
        // TODO: Pull the paths from the OptionsManager.
        haloPC = new ZipTagLibrary(haloPCPath, "Halo PC");
        haloXbox = new ZipTagLibrary(haloXboxPath, "Halo Xbox");
        halo2Xbox = new ZipTagLibrary(halo2XboxPath, "Halo 2 Xbox");
        initialized = true;
      }
    }

    public static void AddTagSet(string tagSetName, string masterLibraryFilename, ITagLibrary tagLibrary)
    {
      //TagSet tagSet = new TagSet();
      
      XmlDocument masterDoc = new XmlDocument();
      masterDoc.Load(masterLibraryFilename);
      XmlNodeList fileNodes = masterDoc.SelectNodes("//tag");
      MasterTagList masterList = new MasterTagList();
      foreach (XmlNode node in fileNodes)
      {
        TagInformation info = new TagInformation();
        info.Filename = node.Attributes["filename"].InnerText;
        XmlNodeList mapNodes = node.SelectNodes("map");
        info.Maps = new string[mapNodes.Count];
        int count=0;
        foreach (XmlNode mapNode in mapNodes)
        {
          info.Maps[count] = mapNode.InnerText;
          count++;
        }
        masterList.Add(info);
      }

      //      tagSet.MasterList = masterList;
      //      tagSet.tagLibrary = tagLibrary;
      //      this.tagSets.Add(tagSet);
      //
      //      this.repositoryItemComboBox1.Items.Add(tagSetName);
      //      this.repositoryItemComboBox1.SelectedIndexChanged += new EventHandler(repositoryItemComboBox1_SelectedIndexChanged);
      //      //this.comboFilter.ItemClick += new ItemClickEventHandler(comboFilter_ItemClick);
    }
  
    static public ZipTagLibrary GetLibrary(MapfileVersion ver)
    {
      ZipTagLibrary lib = null;

      switch(ver)
      {
        case MapfileVersion.HALOPC:
        case MapfileVersion.HALOCE:
          lib = haloPC;
          break;
        case MapfileVersion.XHALO1:
          lib = haloXbox;
          break;
        case MapfileVersion.XHALO2:
          lib = halo2Xbox;
          break;
      }

      return(lib);
    }
  }

  /// <summary>
  /// Stores an index of maps in which a tag can be located for decompiling.
  /// </summary>
  public class TagInformation
  {
    private string filename;

    public string Filename
    {
      get {return filename; }
      set { filename = value.ToLower(); }
    }

    public string[] Maps;
  }

  /// <summary>
  /// Type-safe Hashtable of TagInformation objects.
  /// </summary>
  public class MasterTagList : Hashtable
  {
    public TagInformation this[string filename]
    {
      get { return (base[filename] as TagInformation); }
    }

    public void Add(TagInformation value)
    {
      base.Add(value.Filename, value);
    }
  }

  /// <summary>
  /// Represents a master set of tags, an interface their archive file, and an
  /// index of maps in which they can be located for decompilation.
  /// </summary>
  public class TagSet
  {
    public MasterTagList MasterList;
    public ITagLibrary tagLibrary;
  }

  /// <summary>
  /// A type-safe collection of TagSet objects.
  /// </summary>
  public class TagSetCollection : CollectionBase
  {
    public void Add(TagSet value)
    {
      InnerList.Add(value);
    }
    public TagSet this[int index]
    {
      get { return (InnerList[index] as TagSet); }
    }
  }

  /// <summary>
  /// Provides a base for creating a TagLibrary.
  /// </summary>
  public interface ITagLibrary
  {
    string Name { get; }

    /// <summary>
    /// Returns all files in the specified path.
    /// </summary>
    string[] GetFileList(string path);

    /// <summary>
    /// Returns all files in the specified path with the specified extension.
    /// </summary>
    string[] GetFileList(string path, string extension);

    /// <summary>
    /// Returns all folders in the specified path.
    /// </summary>
    string[] GetFolderList(string path);

    /// <summary>
    /// Returns a boolean indicating if the specified file exists in the library.
    /// </summary>
    bool FileExists(string filename);

    /// <summary>
    /// Returns a boolean indicating if the specified folder exists in the library.
    /// </summary>
    bool FolderExists(string path);

    /// <summary>
    /// Creates a file in the archive with the specified name from a byte buffer.
    /// </summary>
    void AddFile(string filename, byte[] buffer);

    /// <summary>
    /// Reads a file from the archive into a byte array.
    /// </summary>
    byte[] ReadFile(string filename);

    void BeginBatchUpdate();

    void EndBatchUpdate();
    bool ExtractFile(string ArchivePath, string OutputPath);
  }

  
  /// <summary>
  /// A strongly typed collection of unique strings.
  /// </summary>
  public class UniqueStringCollection : CollectionBase
  {
    /// <summary>
    /// Used internally to add a string to the InnerList, and return a
    /// value indicating if the string already exists in the list.
    /// </summary>
    private bool AddInternal(string value)
    {
      if (!InnerList.Contains(value))
      {
        InnerList.Add(value);
        return true;
      }
      else
      {
        return false;
      }
    }

    /// <summary>
    /// Adds a string to the collection - ignoring the string if it already exists.
    /// </summary>
    public void Add(string value)
    {
      AddInternal(value);
    }

    /// <summary>
    /// Adds a string to the collection - throwing an exception if the string already exists.
    /// </summary>
    public void AddUnique(string value)
    {
      if (!AddInternal(value))
      {
        throw new StringNotUniqueException("The specified string already exists in the collection: " + value);
      }
    }

    /// <summary>
    /// Adds a range of strings to the collection - ignoring any strings that already exist.
    /// </summary>
    public void AddRange(string[] values)
    {
      foreach (string value in values)
      {
        Add(value);
      }
    }

    public string this[int index]
    {
      get { return InnerList[index] as string; }
    }
    
    /// <summary>
    /// Converts the collection into an array of strings.
    /// </summary>
    public string[] ToArray()
    {
      return (InnerList.ToArray(typeof(string)) as string[]);
    }

    /// <summary>
    /// Returns a boolean indicating if the specified string exists in the collection.
    /// </summary>
    public bool Contains(string value)
    {
      return InnerList.Contains(value);
    }
    
    /// <summary>
    /// Sorts the string collection alphabetically.
    /// </summary>
    public void Sort()
    {
      // Since we are able to guarantee that the InnerList only contains string
      // objects, we can use its default 'Sort' method.
      InnerList.Sort();
    }
  }

  /// <summary>
  /// This exception is thrown when a string is added to a UniqeStringCollection using
  /// the 'AddUnique' method, and the string already exists in the collection.
  /// </summary>
  public class StringNotUniqueException : Exception
  {
    public StringNotUniqueException() { ; }

    public StringNotUniqueException(string message) : base(message) { ; }

    public StringNotUniqueException(string message, Exception innerException) : base(message, innerException) { ; }

    public StringNotUniqueException(SerializationInfo info, StreamingContext context) : base(info, context) { ; }
  }

  public class DependencyBuilder
  {
    public string[] Dependencies;

    public DependencyBuilder(TagFileName tagFile)
    {
      // Load the proper tag definition.
      XmlDocument tagDefinition = TagDefinitionManager.GetTagDefinitionByName(tagFile.FileExtension);

      // Load the tag data
      TagBase tag = new TagBase();
      tag.LoadTagBuffer(tagFile);
      BinaryReader br = new BinaryReader(tag.Stream);
      
      // Create the appropriate type.
      XmlNode nameNode = tagDefinition.SelectSingleNode("//name");
      string tagTypeString = "TagLibrary.Halo1." + nameNode.InnerText;
      Assembly a = Assembly.GetAssembly(typeof(IField));
      IBlock tagBlock = (a.CreateInstance(tagTypeString) as IBlock);
      
      tagBlock.Read(br);
      tagBlock.ReadChildData(br);

      Dependencies = ProcessDependencies(tagBlock);
    }

    private string[] ProcessDependencies(IBlock block)
    {
      // Get all of the dependencies.
      UniqueStringCollection deps = new UniqueStringCollection();
        
      PropertyInfo[] properties = block.GetType().GetProperties();
      foreach (MemberInfo info in properties)
      {
        object o = (info as PropertyInfo).GetValue(block, null);
        deps.AddRange(ProcessItem(o));
      }

      FieldInfo[] fields = block.GetType().GetFields();
      foreach (MemberInfo info in fields)
      {
        object o = (info as FieldInfo).GetValue(block);
        deps.AddRange(ProcessItem(o));
      }

      return deps.ToArray();
    }
      
    private string[] ProcessItem(object o)
    {
      UniqueStringCollection deps = new UniqueStringCollection();
      if (o is TagReference)
      {
        TagReference tagRef = (o as TagReference);
        if (tagRef.Value != null)
        {
          if (tagRef.Value.Length > 0)
          {
            string filename = tagRef.Value + TagFileName.GetFileExtension(tagRef.TagGroup);
            deps.Add(filename);
          }
        }
      }
      if (o is CollectionBase)
      {
        foreach (object child in (o as CollectionBase))
        {
          if (child is IBlock)
          {
            string[] items = ProcessDependencies(child as IBlock);
            deps.AddRange(items);
          }
        }
      }
      if (o is IBlock)
      {
        string[] items = ProcessDependencies(o as IBlock);
        deps.AddRange(items);
      }
      return deps.ToArray();
    }
  }

  /// <summary>
  /// Encapsulates a physical disk folder heirarchy in an ITagLibrary.
  /// </summary>
  public class DiskFolderTagLibrary : ITagLibrary
  {
    private string name;
    private string rootPath;

    public string Name
    {
      get { return name; }
    }

    public string RootPath
    {
      get { return rootPath; }
    }

    public DiskFolderTagLibrary(string rootPath, string name)
    {
      this.name = name;
      this.rootPath = rootPath.Trim('\\');
    }

    /// <summary>
    /// Returns all files in the specified path.
    /// </summary>
    public string[] GetFileList(string path)
    {
      return (GetFileList(path, "*.*"));
    }

    /// <summary>
    /// Returns all files in the specified path with the specified extension.
    /// </summary>
    public string[] GetFileList(string path, string extension)
    {
      try
      {
        if ((extension == null) || (extension == "")) extension = "*.*";
        path = path.Trim('\\');
        string[] results = Directory.GetFiles(rootPath + "\\" + path, extension);
        return ReformatPaths(results);
      }
      catch (Exception ex)
      {
        Trace.WriteLine(ex.Message, "error");
        return new string[0];
      }
    }

    /// <summary>
    /// Returns all folders in the specified path.
    /// </summary>
    public string[] GetFolderList(string path)
    {
      try
      {
        path = path.Trim('\\');
        string[] results = Directory.GetDirectories(rootPath + "\\" + path);
        return ReformatPaths(results);
      }
      catch (Exception ex)
      {
        Trace.WriteLine(ex.Message, "error");
        return new string[0];
      }
    }

    /// <summary>
    /// Returns a boolean indicating if the specified file exists in the library.
    /// </summary>
    public bool FileExists(string filename)
    {
      filename = filename.Trim('\\');
      return File.Exists(rootPath + "\\" + filename);
    }

    /// <summary>
    /// Returns a boolean indicating if the specified folder exists in the library.
    /// </summary>
    public bool FolderExists(string path)
    {
      path = path.Trim('\\');
      return Directory.Exists(rootPath + "\\" + path);
    }

    /// <summary>
    /// Creates a file in the archive with the specified name from a byte buffer.
    /// </summary>
    public void AddFile(string filename, byte[] buffer)
    {
      FileStream fs = File.Create(rootPath + filename);
      fs.Write(buffer, 0, buffer.Length);
      fs.Close();
    }

    /// <summary>
    /// Reads a file from the archive into a byte array.
    /// </summary>
    public byte[] ReadFile(string filename)
    {
      BinaryReader br = new BinaryReader(new FileStream(rootPath + filename, FileMode.Open));
      byte[] fileData = br.ReadBytes((int)br.BaseStream.Length);
      return fileData;
    }

    public void BeginBatchUpdate()
    {
      return;
    }

    public void EndBatchUpdate()
    {
      return;
    }

    public bool ExtractFile(string ArchivePath, string OutputPath)
    {
      return true;
    }

    /// <summary>
    /// Removes the base path from a set of path strings.
    /// </summary>
    internal string[] ReformatPaths(params string[] paths)
    {
      for (int x=0; x<paths.Length; x++)
      {
        paths[x] = paths[x].Substring(rootPath.Length);
      }
      return paths;
    }
  }


  public class BaseTagLibrary : ITagLibrary
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    private UniqueStringCollection tags = new UniqueStringCollection();

    public BaseTagLibrary(string name)
    {
      this.name = name;
    }

    public void AddTag(string filename)
    {
      // Strip the path if neccessary.
      tags.Add(Path.GetFileName(filename));
    }

    /// <summary>
    /// Returns all files in the specified path.
    /// </summary>
    public string[] GetFileList(string path)
    {
      return GetFileList(path, null);
    }

    /// <summary>
    /// Returns all files in the specified path with the specified extension.
    /// </summary>
    public string[] GetFileList(string path, string extension)
    {
      return tags.ToArray();
    }

    /// <summary>
    /// Returns all folders in the specified path.
    /// </summary>
    public string[] GetFolderList(string path)
    {
      return new string[0];
    }

    /// <summary>
    /// Returns a boolean indicating if the specified file exists in the library.
    /// </summary>
    public bool FileExists(string filename)
    {
      return tags.Contains(Path.GetFileName(filename));
    }

    /// <summary>
    /// Returns a boolean indicating if the specified folder exists in the library.
    /// </summary>
    public bool FolderExists(string path)
    {
      if (path == "\\") return true;
      if (path == "") return true;
      return false;
    }

    /// <summary>
    /// Creates a file in the archive with the specified name from a byte buffer.
    /// </summary>
    public void AddFile(string filename, byte[] buffer)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Reads a file from the archive into a byte array.
    /// </summary>
    public byte[] ReadFile(string filename)
    {
      throw new NotImplementedException();
    }

    public void BeginBatchUpdate()
    {
      throw new NotImplementedException();
    }

    public void EndBatchUpdate()
    {
      throw new NotImplementedException();
    }

    public bool ExtractFile(string ArchivePath, string OutputPath)
    {
      throw new NotImplementedException();
    }
  }

  public class DependencyTagLibrary : ITagLibrary
  {
    private string name = "";
    private TagFileName[] tags;
    private FileSystemHeirarchy fileSystem = new FileSystemHeirarchy();

    private string[] dependencies;
    private string[] brokenDependencies;

    public string Name
    {
      get { return name; }
    }

    public DependencyTagLibrary(params TagFileName[] tags)
    {
      this.tags = tags;
      
      ArrayList deps = new ArrayList();
      foreach (TagFileName tag in tags)
      {
        DependencyBuilder db = new DependencyBuilder(tag);
        deps.AddRange(db.Dependencies);
      }
      foreach (string dep in deps)
      {
        fileSystem.Add(dep);
      }
    }

    /// <summary>
    /// Returns all files in the specified path.
    /// </summary>
    public string[] GetFileList(string path)
    {
      return GetFileList(path, "");
    }

    /// <summary>
    /// Returns all files in the specified path with the specified extension.
    /// </summary>
    public string[] GetFileList(string path, string extension)
    {
      FolderEntry entry = fileSystem.LocateFolderByPath(FixPath(path));
      if (entry != null)
      {
        return entry.FileEntries.GetItems(extension);    
      }
      else
      {
        return null;
      }
    }

    /// <summary>
    /// Returns all folders in the specified path.
    /// </summary>
    public string[] GetFolderList(string path)
    {
      FolderEntry entry = fileSystem.LocateFolderByPath(FixPath(path));
      if (entry == null) return null;

      return entry.FolderEntries.GetItems();
    }

    private string FixPath(string path)
    {
      return path.TrimStart('\\').TrimEnd('\\');
    }

    /// <summary>
    /// Returns a boolean indicating if the specified file exists in the library.
    /// </summary>
    public bool FileExists(string filename)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Returns a boolean indicating if the specified folder exists in the library.
    /// </summary>
    public bool FolderExists(string path)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Creates a file in the archive with the specified name from a byte buffer.
    /// </summary>
    public void AddFile(string filename, byte[] buffer)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Reads a file from the archive into a byte array.
    /// </summary>
    public byte[] ReadFile(string filename)
    {
      throw new NotImplementedException();
    }

    public void BeginBatchUpdate() { ; }

    public void EndBatchUpdate() { ; }

    public bool ExtractFile(string ArchivePath, string OutputPath)
    {
      throw new NotImplementedException();
    }
  }

/*
  public class SimpleArchiveTagLibrary : ITagLibrary
  {
    private SimpleArchiveFileSystem archive;
    private string name;

    public string Name
    {
      get { return name; }
    }

    public SimpleArchiveTagLibrary(string archivePath, string name)
    {
      this.name = name;
      archive = new SimpleArchiveFileSystem(archivePath);
    }

    /// <summary>
    /// Returns all files in the specified path.
    /// </summary>
    public string[] GetFileList(string path)
    {
      return archive.GetFileList(path);
    }

    /// <summary>
    /// Returns all files in the specified path with the specified extension.
    /// </summary>
    public string[] GetFileList(string path, string extension)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Returns all folders in the specified path.
    /// </summary>
    public string[] GetFolderList(string path)
    {
      return archive.GetFolderList(path);
    }

    /// <summary>
    /// Returns a boolean indicating if the specified file exists in the library.
    /// </summary>
    public bool FileExists(string filename)
    {
      return archive.Exists(filename);
    }

    /// <summary>
    /// Returns a boolean indicating if the specified folder exists in the library.
    /// </summary>
    public bool FolderExists(string path)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Creates a file in the archive with the specified name from a byte buffer.
    /// </summary>
    public void AddFile(string filename, byte[] buffer)
    {
      archive.AddFile(filename, buffer);
    }

    /// <summary>
    /// Reads a file from the archive into a byte array.
    /// </summary>
    public byte[] ReadFile(string filename)
    {
      return archive.GetFile(filename);
    }

    public void BeginBatchUpdate()
    {
    }

    public void EndBatchUpdate()
    {
    }
    public bool ExtractFile(string ArchivePath, string OutputPath)
    {
      return(false);
    }
  }

*/
  public class ObjectViewTagLibrary : ITagLibrary
  {
    // Note: This will need to be modified to a generic ITagLibrary, unless
    // this class is used as a base to inherit specialized ObjectView libraries.
    private ZipTagLibrary sourceArchive;
    private string name;

    public string Name
    {
      get { return name; }
    }

    public ObjectViewTagLibrary(string name, ZipTagLibrary sourceArchive)
    {
      this.name = name;
      this.sourceArchive = sourceArchive;
    }

    /// <summary>
    /// Returns all files in the specified path.
    /// </summary>
    public string[] GetFileList(string path)
    {
      return GetFileList(path, "");
    }

    /// <summary>
    /// This is a shitty implementation, but it'll get better.. maybe :P
    /// </summary>
    public class ObjectViewPath
    {
      private bool rootPath = false;
      private string category = "";
      private string tag = "";
      private string dependency = "";

      public bool Rootpath
      {
        get { return rootPath; }
      }

      public string Path
      {
        get
        {
          string path = "[" + category + "]" + tag;
          if (dependency != "") path += "[dependencies]" + dependency;
          return path;
        }
        set
        {
          ParsePath(value);
        }
      }

      public string Category
      {
        get { return category; }
        set { category = value; }
      }

      public string Tag
      {
        get { return tag; }
        set { tag = value; }
      }

      public string Dependency
      {
        get { return dependency; }
        set { dependency = value; }
      }

      public ObjectViewPath(string path)
      {
        Path = path;
      }

      protected void ParsePath(string path)
      {
        if (path == "\\")
        {
          rootPath = true;
          return;
        }

        // Do t3h parsing.
        string[] parts = path.Split('[', ']');
        if (parts.Length < 2) // Something is wrong with the path.
          throw new Exception("The supplied path was invalid: " + path);

        // Remove the empty strings.
        StringCollection strings = new StringCollection();
        for (int x=0; x<parts.Length; x++)
        {
          if (parts[x] != "") strings.Add(parts[x]);
        }
        parts = new string[strings.Count];
        strings.CopyTo(parts, 0);

        // Hard code this to allow for one dependency level for now.
        if (parts.Length >= 1)
        {
          category = parts[0];
        }
        if (parts.Length >= 2)
        {
          tag = parts[1];
        }
        if (parts.Length == 3)
        {
          // At some point, need to continue parsing form here and
          // fill in the depepdency tag.
        }
      }
    }

    /// <summary>
    /// Returns all files in the specified path with the specified extension.
    /// </summary>
    public string[] GetFileList(string path, string extension)
    {
      // "[vehicles]"
      // "[vehicles]vehicles\warthog\warthog.vehicle"
      // "[vehicles]vehicles\warthog\warthog.vehicle[dependencies]
      // "[vehicles]vehicles\warthog\warthog.vehicle[dependencies]vehicles\warthog\warthog.gbxmodel"
      ObjectViewPath objPath = new ObjectViewPath(path);
      if (objPath.Tag != "")
      {
        // Get the dependencies of this tag.
        // HACK: This is hardcoded - need to decide on how to determine these items.
        //DependencyTagLibrary dep = new DependencyTagLibrary(new TagFileName(objPath.Tag, MapfileVersion.HALOPC, TagSource.Archive));
        DependencyBuilder builder = new DependencyBuilder(new TagFileName(objPath.Tag, MapfileVersion.HALOPC, TagSource.Archive));
        
        StringCollection strings = new StringCollection();
        string basePath = objPath.Path;
        foreach (string s in builder.Dependencies)
        {
          if (s != "") strings.Add(basePath + "[dependencies]" + s);
        }
        string[] filePaths = new string[strings.Count];
        strings.CopyTo(filePaths, 0);
        return filePaths;
      }
      return new string[0];
    }

    /// <summary>
    /// Returns all folders in the specified path.
    /// </summary>
    public string[] GetFolderList(string path)
    {
      // NOTE: This is a hardcoded set of paths for now, until
      // I can get some test results to show the speed.  I don't
      // feel comfortable putting a ton of time into it until I
      // know that it will be fast enough.
      // HACK: This should actually an empty string, not a slash.
      if (path == "\\")
      {
        return new string[]
        {
          "[biped]",
          "[scenario]",
          "[scenery]",
          "[vehicle]"
        };
      }

      string[] parts = path.Split('\\');
      if (parts.Length > 1) return new string[0]; // When we do infinite dependency levels,
                                                  // this will need to support sub containers.
      string extension = parts[0].Trim('[', ']');

      string[] files = sourceArchive.GetFileList("\\", "*." + extension, true);
      for (int x=0; x<files.Length; x++)
      {
        files[x] = "[" + extension + "]" + files[x].Trim('\\');
      }
      return files;
    }

    /// <summary>
    /// Returns a boolean indicating if the specified file exists in the library.
    /// </summary>
    public bool FileExists(string filename)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Returns a boolean indicating if the specified folder exists in the library.
    /// </summary>
    public bool FolderExists(string path)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Creates a file in the archive with the specified name from a byte buffer.
    /// </summary>
    public void AddFile(string filename, byte[] buffer)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Reads a file from the archive into a byte array.
    /// </summary>
    public byte[] ReadFile(string filename)
    {
      throw new NotImplementedException();
    }

    public void BeginBatchUpdate()
    {
      throw new NotImplementedException();
    }

    public void EndBatchUpdate()
    {
      throw new NotImplementedException();
    }

    public bool ExtractFile(string ArchivePath, string OutputPath)
    {
      throw new NotImplementedException();
    }
  }

/*
  public class ArchiveTagLibrary : ITagLibrary
  {
    private Archive archive;
    private string name;

    public string Name
    {
      get { return name; }
    }

    public ArchiveTagLibrary(string archivePath, string name)
    {
      if (!File.Exists(archivePath))
      {
        FileStream fs = File.Create(archivePath);
        fs.Close();
      }

      this.name = name;
      archive = new Archive(archivePath, FileMode.Open);
    }

    public string[] GetFileList(string path)
    {
      archive.SetCurrentDirectory(path);
      return archive.GetFiles();
    }

    public string[] GetFileList(string path, string extension)
    {
      throw new NotImplementedException();
    }

    public string[] GetFolderList(string path)
    {
      archive.SetCurrentDirectory(path);
      return archive.GetDirectories();
    }

    public bool FileExists(string filename)
    {
      return archive.Exists(filename);
    }

    /// <summary>
    /// Returns a boolean indicating if the specified folder exists in the library.
    /// </summary>
    public bool FolderExists(string path)
    {
      throw new NotImplementedException();
    }

    public void AddFile(string filename, byte[] buffer)
    {
      string folder = Path.GetDirectoryName(filename);
      string file = Path.GetFileName(filename);
      if (!archive.Exists(folder)) archive.CreateDirectory(folder);
      archive.SetCurrentDirectory(folder);
      BinaryWriter bw = archive.OpenWrite(file);
      bw.Write(buffer);
    }

    public byte[] ReadFile(string filename)
    {
      string folder = Path.GetDirectoryName(filename);
      string file = Path.GetFileName(filename);

      archive.SetCurrentDirectory(folder);
      BinaryReader br = archive.OpenRead(file);
      return br.ReadBytes((int)br.BaseStream.Length);
    }

    public void BeginBatchUpdate() { ; }
    public void EndBatchUpdate() { ; }

    public bool ExtractFile(string ArchivePath, string OutputPath)
    {
      return(false);
    }
  }

*/
  /// <summary>
  /// Encapsulates a tag library located within a zip file.
  /// </summary>
  public class ZipTagLibrary : ITagLibrary
  {
    private ZipArchive archive;
    private string name;

    public string Name
    {
      get { return name; }
    }

    /// <summary>
    /// Creates an instance of the class with normal compression.
    /// </summary>
    public ZipTagLibrary(string archivePath, string name) : this(archivePath, name, CompressionLevel.Normal) { ; }

    /// <summary>
    /// Creates an instance of the class with the specified compression level.
    /// </summary>
    public ZipTagLibrary(string archivePath, string name, CompressionLevel compressionLevel)
    {
      try
      {
        this.name = name;
        DiskFile zipFile = new DiskFile(archivePath);

        if(zipFile.Exists)
        {
          archive = new ZipArchive(zipFile);
          archive.DefaultCompressionLevel = compressionLevel;
          Trace.WriteLine("Opened archive: " + archivePath, "info");
        }
        else
        {
          // Make sure directory exists - if not, create it.
          string path = Path.GetPathRoot(archivePath);
          if (!Directory.Exists(path)) Directory.CreateDirectory(path);

          zipFile.Create();
          archive = new ZipArchive(zipFile);
          archive.DefaultCompressionLevel = compressionLevel;
          Trace.WriteLine("Created archive: " + archivePath, "info");
        }
      }
      catch (Exception ex)
      {
        throw new PrometheusException("Could not open ZipTagLibrary: " + archivePath, ex, true);
      }
    }

    public string[] GetFileList(string path)
    {
      return GetFileList(path, "*.*", false);
    }

    /// <summary>
    /// Returns all files in the specified path with the specified extension.
    /// </summary>
    public string[] GetFileList(string path, string extension)
    {
      return GetFileList(path, extension, false);
    }

    /// <summary>
    /// Returns all folders in the specified path.
    /// </summary>
    public string[] GetFileList(string path, bool recursive)
    {
      return GetFileList(path, "*.*", recursive);
    }

    /// <summary>
    /// Returns all files in the specified path with the specified extension.
    /// </summary>
    /// <param name="extension" example="*.scenario">The extension you would like to filter by.</param>
    public string[] GetFileList(string path, string extension, bool recursive)
    {
      if ((extension == null) || (extension == ""))
        extension = "*.*";

      if (path == "") return new string[0];

      AbstractFolder rootFolder = archive.GetFolder(path);
      
      if (!(rootFolder as ZippedFolder).Exists)
      {
        return null;
      }

      AbstractFile[] fileList = rootFolder.GetFiles(recursive, new NameFilter(extension));
      //AbstractFile[] fileList = rootFolder.GetFiles(false);
      string[] results = new string[fileList.Length];
      
      for (int x=0; x<fileList.Length; x++)
      {
        results[x] = fileList[x].FullName;  
      }
      return results;
    }

    /// <summary>
    /// Returns a list of all files in the archive.
    /// </summary>
    /// <returns></returns>
    public string[] GetFileList()
    {
      AbstractFile[] fileList = archive.GetFiles(true);
      string[] results = new string[fileList.Length];
      
      for (int x=0; x<fileList.Length; x++)
      {
        results[x] = fileList[x].FullName;  
      }
      return results;
    }

    /// <summary>
    /// Returns all folders in the specified path.
    /// </summary>
    public string[] GetFolderList(string path)
    {
      AbstractFolder rootFolder = archive.GetFolder(path);
      if (!(rootFolder as ZippedFolder).Exists)
      {
        return null;
      }

      AbstractFolder[] fileList = rootFolder.GetFolders(false);
      string[] results = new string[fileList.Length];
      
      for (int x=0; x<fileList.Length; x++)
      {
        results[x] = fileList[x].FullName;  
      }
      return results;
    }

    /// <summary>
    /// Returns a boolean indicating if the specified file exists in the library.
    /// </summary>
    public bool FileExists(string filename)
    {
      ZippedFile file = (ZippedFile)archive.GetFile(filename);
      return file.Exists;
    }

    /// <summary>
    /// Returns a boolean indicating if the specified folder exists in the library.
    /// </summary>
    public bool FolderExists(string path)
    {
      ZippedFolder folder = (archive.GetFolder(path) as ZippedFolder);
      return folder.Exists;
    }

    /// <summary>
    /// Creates a file in the archive with the specified name from a byte buffer.
    /// </summary>
    public void AddFile(string filename, byte[] buffer)
    {
      try
      {
        ZippedFile file = (ZippedFile)archive.GetFile(filename);

        if(!file.Exists)
        {
          file.Create();
          // Write the buffer to the ZippedFile object
          using (Stream stream = file.OpenWrite(true))
          {
            stream.Write(buffer, 0, buffer.Length);
          }
        }
      }
      catch (Exception ex)
      {
        throw new PrometheusException("Error adding file to archive: " + filename, ex, true);
      }
    }

    /// <summary>
    /// Reads a file from the zip archive.
    /// </summary>
    /// <returns>A byte array containing the file.</returns>
    public byte[] ReadFile(string filename)
    {
      try
      {
        ZippedFile file = (ZippedFile)archive.GetFile(@filename);
        byte[] buffer = new byte[file.Size];

        using(Stream stream = file.OpenRead())
        {
          int totalBytesRead = 0;
          do
          {
            int byteCount = stream.Read(buffer, totalBytesRead, (int)file.Size - totalBytesRead);
            totalBytesRead += byteCount;
          }
          while (totalBytesRead < file.Size);
        }
        return(buffer);
      }
      catch (Exception ex)
      {
        throw new PrometheusException("Error reading file from zip archive: " + filename, ex, true);
      }
    }

    /// <summary>
    /// Begins a batch update to the archive.
    /// Files will not be written to the archive until BatchEndUpdate is called.
    /// </summary>
    public void BeginBatchUpdate()
    {
      archive.BeginUpdate();
    }

    /// <summary>
    /// Ends a batch update to the archive.
    /// Any pending files from the update will be written when this method is called.
    /// </summary>
    public void EndBatchUpdate()
    {
      archive.EndUpdate();
    }
    public bool ExtractFile(string ArchivePath, string OutputPath)
    {
      ZippedFile file = (ZippedFile)archive.GetFile(ArchivePath);
      DiskFile outfile = new DiskFile(OutputPath);
      file.CopyTo(outfile, true);
      return(false);
    }
  }
}