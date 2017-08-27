using System.Collections;
using System.IO;

namespace Prometheus.Core
{
	/// <summary>
	/// Summary description for SimpleArchiveFileSystem.
	/// </summary>
	public class SimpleArchiveFileSystem
	{
		public class FileHeader
		{
		  public const int HeaderSize = 12;
      private char[] header = new char[4] { 'P', 'A', 'F', 'S'};
      public int FileIndexOffset;
      public int FolderIndexOffset;

      public FileHeader()
      {
        FileIndexOffset = HeaderSize;  
        FolderIndexOffset = FileIndexOffset + FileIndex.HeaderSize;
      }

      public void Read(BinaryReader reader)
      {
        header = reader.ReadChars(4);
        FileIndexOffset = reader.ReadInt32();
        FolderIndexOffset = reader.ReadInt32();
      }
      public void Write(BinaryWriter writer)
      {
        writer.Write(header);
        writer.Write(FileIndexOffset);
        writer.Write(FolderIndexOffset);
      }
		}

    public class FileIndex
    {
      public const int HeaderSize = 4;
      public int TotalEntries;
      public Hashtable Entries = new Hashtable();
      public void Read (BinaryReader reader)
      {
        TotalEntries = reader.ReadInt32();
        for (int x=0; x<TotalEntries; x++)
        {
          FileEntry entry = new FileEntry();
          string filename = entry.Read(reader);
          Entries.Add(filename, entry);
        }
      }
      public void Write (BinaryWriter writer)
      {
        writer.Write(TotalEntries);
        foreach (DictionaryEntry di in Entries)
        {
          FileEntry entry = (FileEntry)di.Value;
          entry.Write(writer, (string)di.Key);
        }
      }
    }

    public class FileEntry
    {
      public int CompressedSize;
      public int UncompressedSize;
      public int Offset;
      public string Read(BinaryReader br)
      {
        string filename = br.ReadString();
        CompressedSize = br.ReadInt32();
        UncompressedSize = br.ReadInt32();
        Offset = br.ReadInt32();
        return filename;
      }
      public void Write(BinaryWriter bw, string filename)
      {
        bw.Write(filename);
        bw.Write(CompressedSize);
        bw.Write(UncompressedSize);
        bw.Write(Offset);
      }
    }

    public class FolderIndex
    {
      public int TotalEntries;
      public ArrayList Entries = new ArrayList();

      public int GetFolderID(string path)
      {
        return GetFolderID(path, -1);
      }

      public int GetFolderID(string path, int parentIndex)
      {
        int i = path.IndexOf('\\');
        string currentLevelName = path;
        if (i > -1) currentLevelName = path.Substring(0, i-1);
        
        int currentIndex = 0;
        foreach (FolderEntry entry in Entries)
        {
          if (entry.Name == currentLevelName)
          {
            if (entry.ParentFolderIndex == parentIndex)
            {
              if (i > -1)
              {
                return GetFolderID(path.Substring(i+1));
              }
              else
              {
                return currentIndex;
              }
            }
          }
          currentIndex++;
        }
        return -1;
      }
     
      public void Read (BinaryReader reader)
      {
        TotalEntries = reader.ReadInt32();
        for (int x=0; x<TotalEntries; x++)
        {
          FolderEntry entry = new FolderEntry();
          entry.Read(reader);
          Entries.Add(entry);
        }
      }
      public void Write (BinaryWriter writer)
      {
        writer.Write(TotalEntries);
        foreach (FolderEntry entry in Entries)
        {
          entry.Write(writer);
        }
      }
    }

    public class FolderEntry
    {
      public int ParentFolderIndex;
      public string Name;
      public void Read(BinaryReader br)
      {
        ParentFolderIndex = br.ReadInt32();
        Name = br.ReadString();
      }
      public void Write(BinaryWriter bw)
      {
        bw.Write(ParentFolderIndex);
        bw.Write(Name);
      }
    }

    private Stream stream;
    private FileHeader header;
    private FileIndex fileIndex;
    private FolderIndex folderIndex;

    public SimpleArchiveFileSystem(string filename)
		{
      if (!File.Exists(filename)) CreateArchive(filename);
      stream = new FileStream(filename, FileMode.Open);
      BinaryReader br = new BinaryReader(stream);
      
      header = new FileHeader();
      header.Read(br);
      
      br.BaseStream.Position = header.FileIndexOffset;
      fileIndex = new FileIndex();
      fileIndex.Read(br);

      br.BaseStream.Position = header.FolderIndexOffset;
      folderIndex = new FolderIndex();
      folderIndex.Read(br);
		}

    public void CreateArchive(string filename)
    {
      BinaryWriter bw = new BinaryWriter(new FileStream(filename, FileMode.Create));
      FileHeader tempHead = new FileHeader();
      tempHead.Write(bw);
      FileIndex tempFileIndex = new FileIndex();
      tempFileIndex.TotalEntries = 0;
      tempFileIndex.Write(bw);
      FolderIndex tempFolderIndex = new FolderIndex();
      tempFolderIndex.TotalEntries = 0;
      tempFolderIndex.Write(bw);
      bw.Close();
    }

    public string[] GetFileList(string path)
    {
      // Remove the beginning "\\" if it exists.
      if (path.StartsWith("\\")) path = path.TrimStart('\\');

      ArrayList results = new ArrayList();
      foreach (DictionaryEntry di in fileIndex.Entries)
      {
        string key = (string)di.Key;
        if (key.StartsWith(path))
        {
          if (key != path) results.Add(key);
        }
      }
      return (string[])results.ToArray(typeof(string));
    }

    public string[] GetFolderList(string path)
    {
      // TODO: Create a second index in the file that is for folders only, and add
      // a folder index to the FileEntry.  This will make it much easier to sort
      // through folders - this existing method will be slow.
      // EXAMPLE: "\levels\test\bloodgulch"
      ArrayList results = new ArrayList();
      
      // Remove the beginning "\\" if it exists.
      if (path.StartsWith("\\")) path = path.TrimStart('\\');

      foreach (DictionaryEntry di in fileIndex.Entries)
      {
        string key = (string)di.Key;
        if (key.StartsWith(path))
        {
          if (key != path)
          {
            if (key.IndexOf('.') < 0)
            {
              // This is not a file - now we need to make sure that it
              // is definaetly directly under the folder, and not a
              // subfolder of a subfolder.
              string temp = key.Remove(0, path.Length);
              if (temp.IndexOf('\\') < 0) results.Add(key);
            }
          }
        }
      }
      return (string[])results.ToArray(typeof(string));
    }

    public bool Exists(string filename)
    {
      return fileIndex.Entries.ContainsKey(filename);
    }

    public void AddFile(string filename, byte[] buffer)
    {
      // Get the filename and the foldername.
      string folder = Path.GetDirectoryName(filename);
      string file = Path.GetFileName(filename);

      // Get the folder index.
      int folderID = folderIndex.GetFolderID(folder);
      if (folderID == -1)
      {
        // We need to create the folder.
        folderID = AddFolder(folder);
      }

      // Add the entry to the file index and update the count.
      // TODO: We need add the Folder Index to the FileEntry.
      // Also, need to determine how to handle quick searching the filenames,
      // becasue we can't use a hastable since there will be duplicate names.
      FileEntry entry = new FileEntry();
      entry.CompressedSize = buffer.Length;
      entry.UncompressedSize = buffer.Length;
      entry.Offset = (int)stream.Position;
      fileIndex.Entries.Add(filename, entry);
      fileIndex.TotalEntries++;

      // Write the new file buffer to the old index position.
      stream.Position = header.FileIndexOffset;
      stream.Write(buffer, 0, buffer.Length);

      // TODO: At some point, we need to add batch updating capabilities
      // like the Xceed Zip library provided so that the index is only
      // written out to the file once in a long batch.
      
      WriteFAT((int)stream.Position);
    }

    private void WriteFAT(int fileIndexOffset)
    {
      BinaryWriter bw = new BinaryWriter(stream);
      // Write the index to it's new position.
      stream.Position = fileIndexOffset;
      fileIndex.Write(bw);
      int folderIndexOffset = (int)stream.Position;
      folderIndex.Write(bw);


      // Update the file header.
      stream.Position = 0;
      header.FileIndexOffset = fileIndexOffset;
      header.FolderIndexOffset = folderIndexOffset;
      header.Write(new BinaryWriter(stream));

      // Save.
      stream.Flush();
    }

    public byte[] GetFile(string filename)
    {
      FileEntry entry = (FileEntry)fileIndex.Entries[filename] ;
      stream.Position = entry.Offset;

      byte[] buffer = new byte[entry.UncompressedSize];
      stream.Read(buffer, 0, entry.UncompressedSize);

      return buffer;
    }

    public int AddFolder(string path)
    {
      string[] parts = path.Split('\\');

      int parentIndex = -1;
      int folderID = 0;
      for (int x=0; x<parts.Length; x++)
      {
        folderID = folderIndex.GetFolderID(parts[x], parentIndex);
        if (folderID == -1)
        {
          // Create this folder.
          FolderEntry entry = new FolderEntry();
          entry.Name = parts[x];
          entry.ParentFolderIndex = parentIndex;
          folderIndex.Entries.Add(entry);
          folderIndex.TotalEntries++;
          parentIndex = folderID;
        }
        else
        {
          parentIndex = folderID;
        }
      }
      return folderID;
    }
	}
}