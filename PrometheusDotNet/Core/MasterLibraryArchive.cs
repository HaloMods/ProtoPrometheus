using System;
using System.Collections;
using System.IO;
using System.Xml;

namespace Prometheus.Core
{
	/// <summary>
	/// Summary description for MasterLibraryArchive.
	/// </summary>
	public class MasterLibraryArchive : ITagLibrary
	{
		FileSystemHeirarchy tagList = new FileSystemHeirarchy();
    ITagLibrary extractedTags;

    public MasterLibraryArchive(XmlDocument tagDocument, ITagLibrary extractedTags)
    {
      XmlNodeList tagNodes = tagDocument.SelectNodes("//tag");
      foreach (XmlNode tagNode in tagNodes)
      {
        TagInformation tagInfo = new TagInformation();
        tagInfo.Filename = tagNode.Attributes["filename"].InnerText;
        XmlNodeList mapNodes = tagNode.SelectNodes("map");
        tagInfo.Maps = new string[mapNodes.Count];
        int x=0;
        foreach (XmlNode mapNode in mapNodes)
        {
          tagInfo.Maps[x] = mapNode.InnerText;
          x++;
        }
        tagList.Add(tagInfo);
      }
      this.extractedTags = extractedTags;
    }

    public string Name
	  {
	    get { return ""; }
	  }

    public string[] GetRecursiveFileList(string path)
    {
      path = FixPath(path);
      ArrayList files = new ArrayList();
      string[] fileList = GetFileList(path);
      
      if (fileList != null) files.AddRange(fileList);
      string[] folderPaths = GetFolderList(path);
      if (folderPaths != null)
      {
        foreach (string folderPath in folderPaths)
        {
          // BUG: For some reason, we're (rarely) getting "" folder paths from the
          // GetFolderList method.  Need to debug that at some point.
          // HACK: Skipping those folders that are effected by the bug.
          if (folderPath != "")
          {
            // There's a bug that I haven't found yet that is causing (sometimes) for a folder to
            // contain a reference to itself as a child folder.  This of course causes infinite recursion.
            // I'm manually hacking these out for now.
            string[] paths = GetRecursiveFileList(folderPath);
            files.AddRange(paths);
//            ArrayList newPaths = new ArrayList();
//            foreach (string s in paths)
//            {
//              if (!s.EndsWith("\\"))
//              {
//                if (!s.StartsWith(path))
//                {
//                  newPaths.Add(s);
//                }
//              }
//            }
//            files.AddRange(newPaths.ToArray(typeof(string)) as string[]);
          }
        }
      }
      return (string[])files.ToArray(typeof(string));
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
      FolderEntry entry = tagList.LocateFolderByPath(FixPath(path));
      if (entry == null) return null;
      
      // Remove any files that exist in the ITagLibrary
//      return RemoveStringsFromArray(
//        entry.FileEntries.GetItems(extension), extractedTags.GetFileList(path)); 
      return RemoveStringsFromArray(
        entry.FileEntries.GetItems(extension), null); 
    }

	  /// <summary>
	  /// Returns all folders in the specified path.
	  /// </summary>
	  public string[] GetFolderList(string path)
	  {
      FolderEntry entry = tagList.LocateFolderByPath(FixPath(path));
      if (entry == null) return null;
      
      return RemoveStringsFromArray(
        entry.FolderEntries.GetItems(), new string[] { path + "\\", path } ); 
      //return entry.FolderEntries.GetItems(); 
    }

    private string[] RemoveStringsFromArray(string[] sourceArray, string[] itemsToRemove)
    {
      if (itemsToRemove == null) return sourceArray;

      ArrayList items = new ArrayList();
      items.AddRange(sourceArray);

      ArrayList removeList = new ArrayList();
      foreach (string item in items)
      {
        foreach (string remove in itemsToRemove)
        {
          if (FixPath(item) == FixPath(remove))
          {
            removeList.Add(item);
            break;
          }
        }
      }
      foreach (string s in removeList)
      {
        items.Remove(s);
      }
      return items.ToArray(typeof(string)) as string[];
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
    
    public TagInformation GetTagInformation(string path)
    {
      FileEntry entry = tagList.LocateFileByPath(path);
      if (entry == null)
        throw new Exception("The specified file was not found: " + path);

      return entry.TagInfo;
    }

    private string FixPath(string path)
    {
      return path.TrimStart('\\').TrimEnd('\\');
    }
	}

  /// <summary>
  /// Represents the heirarchy information of a file system.
  /// </summary>
  public class FileSystemHeirarchy
  {
    public FolderEntry rootFolder = new FolderEntry("", null);

    /// <summary>
    /// Parses a full path containing the folder and filename, and creates
    /// the appropriate folder and file entries in the heirarchy.
    /// </summary>
    public FileEntry Add(string filename)
    {
      string fullPath = filename;
      string[] parts = fullPath.Split('\\');
      FolderEntry currentEntry = this.rootFolder;
      for (int x=0; x<parts.Length-1; x++) // Last part should be a filename.
      {
        int index = currentEntry.FolderEntries.IndexOf(parts[x]);
        if (index == -1)
        {
          FolderEntry entry = new FolderEntry(parts[x], currentEntry);
          currentEntry.FolderEntries.Add(entry);
          currentEntry = entry;
        }
        else
        {
          currentEntry = currentEntry.FolderEntries[index];
        }
      }
      FileEntry file = new FileEntry(parts[parts.Length-1], currentEntry);
      currentEntry.FileEntries.Add(file);
      return file;
    }

    public FileEntry Add(TagInformation tagInfo)
    {
      FileEntry entry = Add(tagInfo.Filename);
      entry.TagInfo = tagInfo;
      return entry;
    }
  
    private string FixPath(string path)
    {
      return path.TrimStart('\\').TrimEnd('\\');
    }

    public FolderEntry LocateFolderByPath(string path)
    {
      path = FixPath(path);
      if (path == "") return this.rootFolder;
     
      string[] parts = path.Split('\\'); 
      FolderEntry currentEntry = this.rootFolder;
      // TODO: Consolidate the redundant code between this method and the Add method.
      for (int x=0; x<parts.Length; x++)
      {
        int index = currentEntry.FolderEntries.IndexOf(parts[x]);
        if (index > -1)
        {
          currentEntry = currentEntry.FolderEntries[index];
        }
        else
        {
          return null;          
        }
      }
      return currentEntry;
    }

    public FileEntry LocateFileByPath(string path)
    {
      path = FixPath(path);
      string folder = Path.GetDirectoryName(path);
      FolderEntry folderEntry = LocateFolderByPath(folder);
      foreach (FileEntry fileEntry in folderEntry.FileEntries)
      {
        string entryLower = fileEntry.FullPath.ToLower();
        string pathLower = path.ToLower();
        if (entryLower == pathLower)
          return fileEntry;
      }
      return null;
    }
  }

  /// <summary>
  /// Represents a folder in the heirarchy.
  /// </summary>
  public class FolderEntry
  {
    private string name;
    private FolderEntry parentFolder;
    private FileEntryCollection fileEntries = new FileEntryCollection();
    private FolderEntryCollection folderEntries = new FolderEntryCollection();

    public string Name
    {
      get { return name; }
    }

    public FolderEntry ParentFolder
    {
      get { return parentFolder; }
    }

    public FileEntryCollection FileEntries
    {
      get { return fileEntries; }
      set { fileEntries = value; }
    }

    public FolderEntryCollection FolderEntries
    {
      get { return folderEntries; }
      set { folderEntries = value; }
    }

    public FolderEntry(string name, FolderEntry parentFolder)
    {
      this.name = name;
      this.parentFolder = parentFolder;
    }

    public string FullPath
    {
      get
      {
        FolderEntry entry = this;
        string fullPath = entry.name;
        while (entry.ParentFolder != null)
        {
          entry = entry.ParentFolder;
          fullPath = entry.Name + "\\" + fullPath;
        }
        // HACK: It's adding the root directory as well, which is causing that
        // string to begin with a backslash.  I'm just trimming it off for now
        // rather than fixing the problem.. because I'm lazy ;p
        return fullPath.TrimStart('\\');
      }
    }
  }

  /// <summary>
  /// Represents a file in the heirarchy.
  /// </summary>
  public class FileEntry
  {
    private string name = "";
    private string extension = "";
    private FolderEntry parentFolder;
    private TagInformation tagInfo = new TagInformation();

    public string Name
    {
      get { return name; }
      set { name = value; }
    }

    public string Extension
    {
      get { return extension; }
      set { extension = value; }
    }

    public FolderEntry ParentFolder
    {
      get { return parentFolder; }
    }

    public TagInformation TagInfo
    {
      get { return tagInfo; }
      set { tagInfo = value; }
    }

    public string FullPath
    {
      get
      {
        return this.parentFolder.FullPath + "\\" + name + "." + extension;
      }
    }

    public FileEntry(string filename, FolderEntry parentFolder)
    {
      name = Path.GetFileNameWithoutExtension(filename);
      if (filename.IndexOf('.') > -1)
        extension = Path.GetExtension(filename).Substring(1);
      else
        extension = "";
      this.parentFolder = parentFolder;
    }
  }

  /// <summary>
  /// A strongly-typed collection of FolderEntry objects.
  /// </summary>
  public class FolderEntryCollection : CollectionBase
  {
    public void Add(FolderEntry folder)
    {
      InnerList.Add(folder);
    }
    public int IndexOf(string name)
    {
      int x=0;
      foreach (FolderEntry entry in InnerList)
      {
        if (entry.Name == name) return x;
        x++;
      }
      return -1;
    }
    public FolderEntry this[int index]
    {
      get { return (InnerList[index] as FolderEntry); }
    }
    public string[] GetItems()
    {
      ArrayList values = new ArrayList();
      foreach (FolderEntry entry in InnerList)
      {
        //values.Add(entry.Name);
        values.Add(entry.FullPath);
      }
      return (string[])values.ToArray(typeof(string));
    }
  }

  /// <summary>
  /// A strongly-typed collection of FileEntry objects.
  /// </summary>
  public class FileEntryCollection : CollectionBase
  {
    public void Add(FileEntry file)
    {
      InnerList.Add(file);
    }
    public int IndexOf(string name)
    {
      int x=0;
      foreach (FileEntry entry in InnerList)
      {
        if ((entry.Name + entry.Extension) == name) return x;
        x++;
      }
      return -1;
    }
    public FileEntry this[int index]
    {
      get { return (InnerList[index] as FileEntry); }
    }
    public string[] GetItems()
    {
      return GetItems("");
    }
    public string[] GetItems(string extension)
    {
      ArrayList values = new ArrayList();
      foreach (FileEntry entry in InnerList)
      {
        if ((entry.Extension == extension) || (extension == ""))
        {
          //values.Add(entry.Name + "." + entry.Extension);
          values.Add(entry.FullPath);
        }
      }
      return (string[])values.ToArray(typeof(string));
    }
  }
}
