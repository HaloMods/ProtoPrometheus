using System;
using System.IO;

namespace Prometheus.Core.ArchiveFileSystem
{
	public class ArchiveFileSystem : ITagLibrary, IDisposable
	{
    private FileStream stream;
    private BinaryWriter writer;
    private string name;
    private FileEntryTable fileEntryTable;

    protected const int HeaderSize = 1024; // Update this when the header class is modified.

    // TODO: Both of these will need to be expanded to allow
    // for more detailed options.
    // private bool compression = false;
    // private bool encryption = false;

    public ArchiveFileSystem(string name, string filename)
		{
      this.name = name;
      stream = new FileStream(filename, FileMode.Open);
      writer = new BinaryWriter(stream);
      BinaryReader reader = new BinaryReader(stream);
      
      // TODO: Read the file header, and load the FileEntryTable into memory.
      byte[] tableBin = new byte[0];
      fileEntryTable = new FileEntryTable(tableBin);
      fileEntryTable.CacheUpdated += new CacheUpdatedHandler(fileEntryTable_CacheUpdated);
    }

	  private void fileEntryTable_CacheUpdated(CacheUpdatedEventArgs e)
	  {
	    // Seek to the proper location in the disk file and write the entry.
      // TODO: Any fault tolerance will go here as well.
      writer.BaseStream.Position = HeaderSize + e.Entry.EntryOffset;
	  }

	  public string Name
	  {
	    get { return name; }
	  }

	  /// <summary>
	  /// Returns all files in the specified path.
	  /// </summary>
	  public string[] GetFileList(string path)
	  {
	    return GetFileList(path, "*");
	  }

	  /// <summary>
	  /// Returns all files in the specified path with the specified extension.
	  /// </summary>
	  public string[] GetFileList(string path, string extension)
	  {
	    // Step 1: Locate FileSystemEntry by path.
      // Step 2: Call GetChildEntries on the FileSystemEntry.
      // Step 3: Add all unique entries to a string array and return.
      throw new NotImplementedException();
	  }

	  /// <summary>
	  /// Returns all folders in the specified path.
	  /// </summary>
	  public string[] GetFolderList(string path)
	  {
	    throw new NotImplementedException();
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

	  
    /// <summary>
    /// This archive does not support batch updates.
    /// </summary>
    public void BeginBatchUpdate() {; }

    /// <summary>
    /// This archive does not support batch updates.
    /// </summary>
    public void EndBatchUpdate() { ; }

	  public bool ExtractFile(string ArchivePath, string OutputPath)
	  {
	    throw new NotImplementedException();
	  }
    
    public void Dispose()
    {
      if (stream != null)
      {
        stream.Flush();
        stream.Close();
      }
    }
	}
}