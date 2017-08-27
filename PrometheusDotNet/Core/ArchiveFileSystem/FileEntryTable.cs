using System;
using System.Collections;
using System.IO;

namespace Prometheus.Core.ArchiveFileSystem
{
  public class FileEntryTable
  {
    /// <summary>
    /// The maximum number of files/folders that can exist inside a folder.
    /// </summary>
    protected const int MaxSiblingCount = 1024;

    private FileSystemEntry rootEntry;
    private Stream stream;
    private BinaryReader reader;
    private BinaryWriter writer;

    public FileEntryTable(byte[] table)
    {
      stream = new MemoryStream(table);
      reader = new BinaryReader(stream);
      writer = new BinaryWriter(stream);
      rootEntry = ReadEntry(0x0);
    }

    protected void LoadLinkedEntries(FileSystemEntry entry)
    {
      entry.FirstChild = ReadEntry(rootEntry.FirstChildOffset);
      if (entry.FirstChild != null) entry.FirstChild.Parent = entry;
      entry.Sibling = ReadEntry(rootEntry.SiblingOffset);
    }

    /// <summary>
    /// Returns a new FileSystemEntry containing the data at the specified offset.
    /// </summary>
    protected FileSystemEntry ReadEntry(int offset)
    {
      if (offset == FileSystemEntry.NullPointer) return null;

      reader.BaseStream.Position = offset;
      FileSystemEntry entry = new FileSystemEntry();
      entry.Read(reader);
      LoadLinkedEntries(entry);
      return entry;
    }

    public event CacheUpdatedHandler CacheUpdated;

    /// <summary>
    /// Returns a new FileSystemEntry containing the data at the specified offset.
    /// </summary>
    protected void WriteEntry(FileSystemEntry entry)
    {
      writer.BaseStream.Position = entry.EntryOffset;
      entry.Write(writer);
      OnCacheUpdated(entry);
    }

    private void OnCacheUpdated(FileSystemEntry entry)
    {
      if (CacheUpdated != null)
        CacheUpdated(new CacheUpdatedEventArgs(entry));
    }

    protected FileSystemEntry LocateEntryByPath(string path, EntryType entryType)
    {
      if (path == "\\") path = "";
      if (path == "") return rootEntry;
    
      string[] parts = path.Split('\\'); 
      FileSystemEntry currentEntry = rootEntry;
      for (int x=0; x<parts.Length; x++)
      {
        FileSystemEntry childEntry = GetChildEntry(parts[x], currentEntry, entryType);
        if (childEntry == null) return null;

        currentEntry = childEntry;
      }
      return currentEntry;
    }

    protected string BuildPath(FileSystemEntry entry)
    {
      string path = entry.Name;
      while (entry.Parent != null)
      {
        // TODO: Add some infinite loop detection here.
        // Perhaps keep a list of all entries that have been evaluated to
        // make sure that we don't get linked back to one that we've already processed
        // earlier in the chain.
        entry = entry.Parent;
        path = entry.Name + "\\" + path;
      }
      return path;
    }

    /// <summary>
    /// Get the first direct child entry of the specified type that matches the specified criteria.
    /// </summary>
    internal FileSystemEntry GetChildEntry(
      string name, FileSystemEntry parentEntry, EntryType entryType)
    {
      FileSystemEntry[] results = GetChildEntries(name, parentEntry, entryType, 1);
      if (results.Length == 0) return null;
      return results[0];
    }

    /// <summary>
    /// Get all direct child entries of the specified type that match the specified criteria.
    /// </summary>
    internal FileSystemEntry[] GetChildEntries(
      string name, FileSystemEntry parentEntry, EntryType entryType)
    {
      return GetChildEntries(name, parentEntry, entryType, MaxSiblingCount);
    }

    /// <summary>
    /// Get up to the specified number of child entries of the specified type that match the specified criteria.
    /// </summary>
    internal FileSystemEntry[] GetChildEntries(
      string name, FileSystemEntry parentEntry, EntryType entryType, int maxEntries)
    {
      if (parentEntry.FirstChild == null) return new FileSystemEntry[0];
      
      FilenameComparer comparer = new FilenameComparer(name);
      ArrayList entries = new ArrayList();

      FileSystemEntry currentEntry = parentEntry.FirstChild;
      
      if (currentEntry.EntryType == entryType)
        if (comparer.Compare(currentEntry.Name))
        {
          entries.Add(currentEntry);
          if (entries.Count >= maxEntries)
          {
            return entries.ToArray(typeof(FileSystemEntry)) as FileSystemEntry[];
          }
        }
      
      int siblingCount = 0;
      
      while ((currentEntry.Sibling != null) && (siblingCount <= MaxSiblingCount))
      {
        currentEntry = currentEntry.Sibling;
        if (currentEntry.EntryType == entryType)
          if (currentEntry.Name == name)
          {
            entries.Add(currentEntry);
            if (entries.Count >= maxEntries)
            {
              return entries.ToArray(typeof(FileSystemEntry)) as FileSystemEntry[];
            }
          }

        siblingCount++;
      }
      if (siblingCount > MaxSiblingCount) 
        throw new MaxSiblingCountExceededException(MaxSiblingCount, BuildPath(parentEntry));

      return entries.ToArray(typeof(FileSystemEntry)) as FileSystemEntry[];
    }
  }

  /// <summary>
  /// The exception that is thrown then the maximum number of objects exist inside a folder.
  /// </summary>
  public class MaxSiblingCountExceededException : Exception
  {
    private int maxCount;
    private string path;

    public int MaxCount
    {
      get { return maxCount; }
    }

    public string Path
    {
      get { return path; }
    }

    public MaxSiblingCountExceededException(int maxCount, string path)
    {
      this.maxCount = maxCount;
      this.path = path;
    }
  }

  public class CacheUpdatedEventArgs : EventArgs
  {
    private FileSystemEntry entry;

    public FileSystemEntry Entry
    {
      get { return entry; }
    }

    public CacheUpdatedEventArgs(FileSystemEntry entry)
    {
      this.entry = entry;
    }
  }

  public delegate void CacheUpdatedHandler(CacheUpdatedEventArgs e);
}