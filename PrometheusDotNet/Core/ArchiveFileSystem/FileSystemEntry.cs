using System.Collections;
using System.IO;

namespace Prometheus.Core.ArchiveFileSystem
{
  public class FileSystemEntry
  {
    int entryOffset;
    EntryType entryType;
    string name;
    int parentOffset;
    int firstChildOffset;
    int siblingOffset;
    int dataOffset;
    int dataLength;
    //Bitmask sourceMapfiles;
    FileSystemEntry parent = null;
    FileSystemEntry firstChild = null;
    FileSystemEntry sibling = null;

    #region Properties
    public int EntryOffset
    {
      get { return entryOffset; }
    }

    public EntryType EntryType
    {
      get { return entryType; }
      set { entryType = value; }
    }

    public string Name
    {
      get { return name; }
    }

    public int ParentOffset
    {
      get { return parentOffset; }
    }

    public int FirstChildOffset
    {
      get { return firstChildOffset; }
    }

    public int SiblingOffset
    {
      get { return siblingOffset; }
    }

    public int DataOffset
    {
      get { return dataOffset; }
    }

    public int DataLength
    {
      get { return dataLength; }
    }

//    public Bitmask SourceMapfiles
//    {
//      get { return sourceMapfiles; }
//    }

    public FileSystemEntry Parent
    {
      get { return parent; }
      set { parent = value; }
    }

    public FileSystemEntry FirstChild
    {
      get { return firstChild; }
      set { firstChild = value; }
    }

    public FileSystemEntry Sibling
    {
      get { return sibling; }
      set
      {
        sibling = value;
        siblingOffset = value.EntryOffset;
      }
    }
    #endregion

    public const int NullPointer = -1;

    public virtual void Read(BinaryReader reader)
    {
      entryOffset = (int)reader.BaseStream.Position;
      entryType = (EntryType)reader.ReadByte();
      name = reader.ReadString();
      parentOffset = reader.ReadInt32();
      firstChildOffset = reader.ReadInt32();
      siblingOffset = reader.ReadInt32();
      dataOffset = reader.ReadInt32();
      dataLength = reader.ReadInt32();
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write((byte)entryType);
      writer.Write(name);
      writer.Write(parentOffset);
      writer.Write(firstChildOffset);
      writer.Write(siblingOffset);
      writer.Write(dataOffset);
      writer.Write(dataLength);
      //writer.Write(sourceMapfiles.Value);
    }
	}

  /// <summary>
  /// A strongly-typed collection of FileSystemEntry objects.
  /// </summary>
  public class FileSystemEntryCollection : CollectionBase
  {
    public void Add(FileSystemEntry file)
    {
      InnerList.Add(file);
    }
    public int IndexOf(string name)
    {
      int x=0;
      foreach (FileSystemEntry entry in InnerList)
      {
        if (entry.Name == name) return x;
        x++;
      }
      return -1;
    }
    public FileSystemEntry this[int index]
    {
      get { return (InnerList[index] as FileSystemEntry); }
    }
    public string[] GetItems()
    {
      return GetItems("");
    }
    public string[] GetItems(string extension)
    {
      ArrayList values = new ArrayList();
      foreach (FileSystemEntry entry in InnerList)
      {
        if ((Path.GetExtension(entry.Name) == extension))
        {
          values.Add(entry.Name);
        }
      }
      return (string[])values.ToArray(typeof(string));
    }
  }

  public enum EntryType : byte
  {
    File,
    Folder
  }
}