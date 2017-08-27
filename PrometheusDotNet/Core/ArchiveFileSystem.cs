using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Prometheus.Core
{
  public sealed class Archive 
  {
    private static readonly DirInfo ROOT_DIR = new DirInfo(null, "", 0, -1);
    private const int BLOCK_SIZE = 1536, DATA_SIZE = 1530;

    private DirInfoCollection entries = new DirInfoCollection();
    private DirInfo current = ROOT_DIR;
    private BinaryReader br;
    private BinaryWriter bw;
    private Stream stream;
    private int next_dir;

    public class DirInfoCollection : CollectionBase
    {
      public void Add(DirInfo value)
      {
        InnerList.Add(value);
      }
      public DirInfo this[int index]
      {
        get { return (DirInfo)InnerList[index]; }
      }
      public void Remove(DirInfo value)
      {
        InnerList.Remove(value);
      }
    }

    public class StringCollection : CollectionBase
    {
      public void Add(string value)
      {
        InnerList.Add(value);
      }
      public string this[int index]
      {
        get { return (string)InnerList[index]; }
        set { InnerList[index] = value; }
      }
      public StringCollection()
      {
      }
      public StringCollection(string[] values)
      {
        AddRange(values);
      }
      public void AddRange(string[] values)
      {
        foreach (string s in values)
          Add(s); 
      }
      public string[] ToArray()
      {
        //return (string[])InnerList.ToArray();
        string[] values = new string[InnerList.Count];
        for (int x=0; x<InnerList.Count; x++)
        {
          values[x] = (string)InnerList[x];
        }
        return values;
      }
    }

    public class IntCollection : CollectionBase
    {
      public void Add(int value)
      {
        InnerList.Add(value);
      }
      public int this[int index]
      {
        get { return (int)InnerList[index]; }
      }
      public IntCollection()
      {
      }
      public IntCollection(int[] values)
      {
        AddRange(values);
      }
      public void AddRange(int[] values)
      {
        foreach (int s in values)
          Add(s); 
      }
    }

    /// <summary>
    /// Creates a new archive based on the specified stream.
    /// </summary>
    public Archive(Stream stream) 
    {
      if(!stream.CanRead)
        throw new IOException("Archive stream must be readable.");
      if(!stream.CanSeek)
        throw new IOException("Archive stream must be seekable.");
      this.stream = stream;

      br = new BinaryReader(stream);
      bw = new BinaryWriter(stream);
      
      if(stream.Length == 0) 
      {
        bw.Write(Encoding.ASCII.GetBytes("Archive:"));
        Reserve();
        stream.Position = 8;
        bw.Write((short)5);
        bw.Write(0);
        bw.Write(-1);
        bw.Write("");
        bw.Flush();
      }
      stream.Position = 0;
      if(Encoding.ASCII.GetString(br.ReadBytes(8)) != "Archive:")
        throw new Exception("Stream is not a valid archive.");

      entries.Add(new FileInfo(this, "", 0, 1));
      AStream a = new AStream((FileInfo)entries[0]);
      BinaryReader ar = new BinaryReader(a);
      next_dir = ar.ReadInt32();
      string s;
      while((s = ar.ReadString()) != "") 
      {
        int i = ar.ReadInt32(), j = ar.ReadInt32();
        if(i < 0) entries.Add(new DirInfo(this, s, i, j));
        else entries.Add(new FileInfo(this, s, i, j));
      }
      a.Close();
    }

    public Archive(string path): this(new FileStream(path, FileMode.OpenOrCreate)) { }

    public Archive(string path, FileMode mode): this(new FileStream(path, mode)) { }

    ~Archive() 
    {
      this.Close();
    }

    public StreamWriter AppendText(string path) 
    {
      Stream st = Open(path, FileMode.Append, FileAccess.Write);
      st.Position = st.Length;
      return new StreamWriter(st);
    }

    public void Close() 
    {
      if(stream == null) return;
      this.Flush();
      stream.Close();
      stream = null;
    }

    public void CreateDirectory(string path) 
    {
      string[] sa = ResolvePath(path);
      bool created = false;
      DirInfo d1 = ROOT_DIR;
      foreach(string s in sa) 
      {
        DirInfo d2 = null;
        foreach(DirInfo di in entries) 
        {
          if(di.Parent != d1.Index || di.Name.ToLower() != s.ToLower())
            continue;
          if(di is FileInfo)
            throw new IOException("");
          d2 = di;
          break;
        }
        if(d2 == null) 
        {
          entries.Add(d2 = new DirInfo(this, s, next_dir--, d1.Index));
          created = true;
        }
        d1 = d2;
      }
      if(created) UpdateDirs();
    }

    public void Delete(string path) 
    {
      Delete(path, false);
    }

    public void Delete(string path, bool recursive) 
    {
      DirInfo di = FindInfo(path);
      if(di == null)
        throw new FileNotFoundException(string.Format("Could not find file '{0}'.", path));
      if(di.ToString() == "\\") 
      {
        if(!recursive)
          throw new IOException("The archive root cannot be deleted.");
        StringCollection sa = new StringCollection(GetDirectories("\\"));
        sa.AddRange(GetFiles("\\"));
        foreach(string s in sa)
          Delete("\\" + s, true);
      } 
      else if(di is FileInfo) 
      {
        entries.Remove(di);
        FileInfo fi = (FileInfo)di;
        lock(stream)
        {
          foreach(int i in fi.seq) 
          {
            stream.Position = GetOffset(i);
            bw.Write((short)-1);
            bw.Write(new byte[BLOCK_SIZE - 2]);
          }
        }
      } 
      else 
      {
        StringCollection sa = new StringCollection(GetDirectories(di.ToString()));
        sa.AddRange(GetFiles(di.ToString()));
        if(recursive)
          foreach(string s in sa)
            Delete(di.ToString() + '\\' + s, true);
        else if(sa.Count > 0)
          throw new IOException("Directory is not empty.");
        entries.Remove(di);
        if(di == current) 
        {
          sa = new StringCollection(ResolvePath(di.ToString()));
          sa.RemoveAt(sa.Count - 1);
          current = FindInfo("\\" + string.Join("\\", sa.ToArray()));
        }
      }
      UpdateDirs();
    }

    public bool Exists(string path) 
    {
      return FindInfo(path) != null;
    }

    private DirInfo FindInfo(string path) 
    {
      string[] sa = ResolvePath(path);
      DirInfo d1 = ROOT_DIR, d2 = d1;
      for(int i = 0; i < sa.Length; ++i) 
      {
        string s = sa[i].ToLower();
        d1 = null;
        foreach(DirInfo di in entries) 
        {
          if(di.Parent != d2.Index || di.Name.ToLower() != s)
            continue;
          if((d1 = di) is FileInfo && i != sa.Length - 1)
            throw new DirectoryNotFoundException(path);
          else d2 = d1;
          break;
        }
        if(d1 == null) break;
      }
      return d1;
    }

    public void Flush() 
    {
      if(stream == null) return;
      lock(stream) 
      {
        int i = (int)((stream.Length - 8) / BLOCK_SIZE);
        while(--i > 0) 
        {
          stream.Position = GetOffset(i);
          if(br.ReadInt16() != -1) break;
        }
        stream.SetLength(GetOffset(++i));
        stream.Flush();
      }
    }

    public string GetCurrentDirectory() 
    {
      return current.ToString();
    }

    public string[] GetDirectories() 
    {
      return GetEntries("", true);
    }

    public string[] GetDirectories(string pattern) 
    {
      return GetEntries(pattern, false);
    }

    private string[] GetEntries(string pattern, bool files) 
    {
      StringCollection sa = new StringCollection(ResolvePath(pattern, true));
      pattern = "\\" + string.Join("\\", sa.ToArray());
      DirInfo di = null;
      try 
      {
        di = FindInfo(pattern.Trim() == "" ? "\\" : pattern);
        if(di == null) throw new Exception();
        if(!(di is FileInfo))
          pattern = "*";
      } 
      catch 
      {
        pattern = sa[sa.Count - 1];
        sa.RemoveAt(sa.Count - 1);
      }
      di = FindInfo('\\' + string.Join("\\", sa.ToArray()));
      pattern = pattern.Replace(".", ":").Replace("?", "\\S?");
      pattern = pattern.Replace("*", "\\S*").Replace(":", "\\.");
      sa.Clear();
      Regex re = new Regex(pattern, RegexOptions.IgnoreCase);
      foreach(DirInfo d1 in entries) 
      {
        if(d1.Parent != di.Index || (files && !(d1 is FileInfo)) ||
          (!files && d1 is FileInfo)) continue;
        Match m = re.Match(d1.Name);
        if(re.Replace(d1.Name, "", 1, 0) == "")
          sa.Add(d1.Name);
      }
      return sa.ToArray();
    }

    public string[] GetFiles() 
    {
      return GetEntries("", true);
    }

    public string[] GetFiles(string pattern) 
    {
      return GetEntries(pattern, true);
    }


    internal static long GetOffset(int index) 
    {
      if(index < 0) throw new Exception("Cannot point to a negative index.");
      return index * BLOCK_SIZE + 8;
    }

    public void Move(string path, string dest) 
    {
      DirInfo d1 = FindInfo(path), d2 = FindInfo(dest);
      if(d1 == null)
        throw new FileNotFoundException(string.Format("Could not find file '{0}'.", path));
      if(d2 == null) 
      {
        StringCollection sa = new StringCollection(ResolvePath(dest));
        string s = sa[sa.Count - 1];
        sa.RemoveAt(sa.Count - 1);
        d2 = FindInfo("\\" + string.Join("\\", sa.ToArray()));
        if(d2 == null)
          throw new FileNotFoundException(string.Format("Could not find file '{0}'.", dest));
        d1.Name = s;
      } 
      else if(d2 is FileInfo)
        throw new IOException("A file or folder with this name already exists.");
      d1.Parent = d2.Index;
      if(d1 == current) 
      {
        StringCollection sa = new StringCollection(ResolvePath(d1.ToString()));
        sa.RemoveAt(sa.Count - 1);
        current = FindInfo("\\" + string.Join("\\", sa.ToArray()));
      }
    }

    public Stream Open(string path) 
    {
      return Open(path, FileMode.OpenOrCreate);
    }

    public Stream Open(string path, FileAccess access) 
    {
      return Open(path, FileMode.OpenOrCreate, access);
    }

    public Stream Open(string path, FileMode mode) 
    {
      return Open(path, mode, FileAccess.ReadWrite);
    }

    public Stream Open(string path, FileMode mode, FileAccess access) 
    {
      DirInfo di = FindInfo(path);
      if(di != null && !(di is FileInfo))
        throw new FileNotFoundException(string.Format("Could not find file '{0}'.", path));
      switch(mode) 
      {
        case FileMode.Append:
          if(access == FileAccess.Read)
            throw new IOException("Appending requires writing access.");
          if(di == null) goto case FileMode.CreateNew;
          break;
        case FileMode.Create:
          if(di == null) goto case FileMode.CreateNew;
          else goto case FileMode.Truncate;
        case FileMode.CreateNew:
          if(di != null)
            throw new IOException("File already exists.");
          StringCollection sa = new StringCollection(ResolvePath(path));
          string s = sa[sa.Count - 1];
          sa.RemoveAt(sa.Count - 1);
          di = FindInfo("\\" + string.Join("\\", sa.ToArray()));
          if(di == null)
            throw new IOException("Path does not exist.");
          di = new FileInfo(this, s, Reserve(), di.Index);
          entries.Add(di);
          break;
        case FileMode.Open:
          if(di == null)
            throw new FileNotFoundException(string.Format("Could not find file '{0}'.", path));
          break;
        case FileMode.OpenOrCreate:
          if(di == null) goto case FileMode.CreateNew;
          else break;
        case FileMode.Truncate:
          ((FileInfo)di).Length = 0;
          break;
      }
      Stream st = new AStream((FileInfo)di, access);
      if(mode == FileMode.Append)
        st.Position = st.Length;
      return st;
    }

    public BinaryReader OpenRead(string path) 
    {
      return new BinaryReader(Open(path, FileAccess.Read));
    }

    public StreamReader OpenText(string path) 
    {
      return new StreamReader(Open(path, FileAccess.Read));
    }

    public BinaryWriter OpenWrite(string path) 
    {
      Stream st = Open(path, FileAccess.Write);
      st.Position = st.Length;
      return new BinaryWriter(st);
    }

    private int Reserve() 
    {
      int ct = (int)((stream.Length - 8) / BLOCK_SIZE);
      lock(stream) 
      {
        for(int i = 1; i < ct; ++i) 
        {
          stream.Position = GetOffset(i);
          if(br.ReadInt16() == -1) 
          {
            stream.Position -= 2;
            bw.Write((short)0);
            return i;
          }
        }
        stream.Position = GetOffset(ct);
        bw.Write(new byte[BLOCK_SIZE]);
        return ct;
      }
    }

    private string[] ResolvePath(string path) 
    {
      return ResolvePath(path, false);
    }

    private string[] ResolvePath(string path, bool ignoreMask) 
    {
      if(path == null)
        throw new ArgumentNullException("path");
//      if(path.Trim() == "")
//        throw new ArgumentException("Path is empty.");
      if(!path.Trim().StartsWith("\\"))
        path = current.ToString() + '\\' + path;
      StringCollection sa = new StringCollection(path.Split('\\'));
      char[] invalidChars = new char[] { '\\', '/', ':', '*', '?', '\"', '<', '>', '|' };
      if(ignoreMask) invalidChars = new char[] { '\\', '/', ':', '\"', '<', '>', '|' };
      for(int i = 0; i < sa.Count; ) 
      {
        if(sa[i].Trim() == "") sa.RemoveAt(i);
        else if(sa[i].Trim() == ".") sa.RemoveAt(i);
        else if(sa[i].Trim() == "..") 
        {
          sa.RemoveAt(i--);
          if(i >= 0) sa.RemoveAt(i);
        } 
        else 
        {
          sa[i] = sa[i].Trim();
          if(sa[i].Length > 255)
            throw new PathTooLongException("File or directory name is too long.");
          if(sa[i++].IndexOfAny(invalidChars) >= 0)
            throw new ArgumentException("Path name contains invalid characters.");
        }
      }
      return sa.ToArray();
    }

    public void SetCurrentDirectory(string path) 
    {
      DirInfo di = FindInfo(path);
      if(di == null || di is FileInfo)
        throw new FileNotFoundException(string.Format("Could not find file '{0}'.", path));
      current = di;
    }

    internal void UpdateDirs() 
    {
      AStream a = new AStream((FileInfo)entries[0], FileAccess.ReadWrite);
      BinaryWriter aw = new BinaryWriter(a);
      aw.Write(next_dir);
      foreach(DirInfo di in entries) 
      {
        if(di.Name == "") continue;
        aw.Write(di.Name);
        aw.Write(di.Index);
        aw.Write(di.Parent);
      }
      aw.Write("");
      a.SetLength(a.Position);
      a.Close();
    }

    #region Properties and Events
    #endregion

    #region Internal Class [AStream]
    private sealed class AStream: Stream 
    {

      #region Internal Fields
      private FileAccess access;
      private FileInfo file;
      private long pos = 0;
      #endregion

      #region Constructors
      internal AStream(FileInfo file): this(file, FileAccess.Read) { }

      internal AStream(FileInfo file, FileAccess access) 
      {
        if(file == null)
          throw new ArgumentNullException("file");
        this.access = access;
        this.file = file;
        if(!LocalCheck())
          throw new ObjectDisposedException("ArchiveStream");
      }
      #endregion

      #region Methods and Overrides
      public override void Close() 
      {
        if(!LocalCheck()) return;
        Flush();
        base.Close();
        file = null;
      }

      public override void Flush() 
      {
        if(!LocalCheck())
          throw new ObjectDisposedException("ArchiveStream");
        file.Owner.stream.Flush();
      }

      private bool LocalCheck() 
      {
        return file != null && file.Owner != null && file.Owner.stream != null;
      }

      public override int Read(byte[] buffer, int offset, int count) 
      {
        if(!LocalCheck())
          throw new ObjectDisposedException("ArchiveStream");
        if(access == FileAccess.Write)
          throw new NotSupportedException("Stream cannot be read.");
        if(buffer == null)
          throw new ArgumentNullException("buffer");
        if(offset < buffer.GetLowerBound(0) || offset > buffer.GetUpperBound(0))
          throw new IndexOutOfRangeException("Offset is out of range for buffer.");
        if(count < 0) throw new ArgumentOutOfRangeException("count");
        if(offset + count - 1 > buffer.GetUpperBound(0))
          throw new ArgumentException("Not enough space on buffer to read.");
        for(int i = 0; i < count; ++i)
          buffer[offset + i] = file.Read(pos++);
        return count;
      }

      public override long Seek(long offset, SeekOrigin origin) 
      {
        if(!LocalCheck())
          throw new ObjectDisposedException("ArchiveStream");
        switch(origin) 
        {
          case SeekOrigin.Begin: pos = offset; break;
          case SeekOrigin.Current: pos += offset; break;
          case SeekOrigin.End: pos = Length + offset; break;
        }
        return pos;
      }

      public override void SetLength(long value) 
      {
        if(!LocalCheck())
          throw new ObjectDisposedException("ArchiveStream");
        file.Length = value;
      }

      public override void Write(byte[] buffer, int offset, int count) 
      {
        if(!LocalCheck())
          throw new ObjectDisposedException("ArchiveStream");
        if(access == FileAccess.Read)
          throw new NotSupportedException("Stream cannot be written.");
        if(buffer == null)
          throw new ArgumentNullException("buffer");
        if(offset < buffer.GetLowerBound(0) || offset > buffer.GetUpperBound(0))
          throw new IndexOutOfRangeException("Offset is out of range for buffer.");
        if(count < 0) throw new ArgumentOutOfRangeException("count");
        if(offset + count - 1 > buffer.GetUpperBound(0))
          throw new ArgumentException("Not enough data on buffer to write.");
        for(int i = 0; i < count; ++i)
          file.Write(pos++, buffer[offset + i]);
      }
      #endregion

      #region Properties and Events
      public override bool CanRead 
      {
        get 
        {
          return LocalCheck() && file.CanRead && (access == FileAccess.Read ||
            access == FileAccess.ReadWrite);
        }
      }

      public override bool CanSeek 
      {
        get { return LocalCheck() && file.CanSeek; }
      }

      public override bool CanWrite 
      {
        get 
        {
          return LocalCheck() && file.CanWrite && (access == FileAccess.Write ||
            access == FileAccess.ReadWrite);
        }
      }

      public override long Length 
      {
        get { return file.Length; }
      }

      public override long Position 
      {
        get { return pos; }
        set { Seek(value, SeekOrigin.Begin); }
      }
      #endregion
    }
    #endregion

    #region Internal Class [DirInfo]
    public class DirInfo 
    {

      #region Internal Fields
      protected int start, parent;
      protected Archive owner;
      protected string name;
      #endregion

      #region Constructors
      public DirInfo(Archive owner, string name, int start, int parent) 
      {
        if(start != 0 && owner == null)
          throw new ArgumentNullException("owner");
        if(name == null)
          throw new ArgumentNullException("name");
        if(start != 0 && owner.stream == null)
          throw new ObjectDisposedException("CommonInfo");
        this.owner = owner;
        this.parent = parent;
        this.start = start;
        this.name = name;
        PosCreate();
      }
      #endregion

      #region Methods and Overrides
      private void Invalidate() 
      {
        owner.UpdateDirs();
      }

      protected virtual void PosCreate() { }

      public override string ToString() 
      {
        if(start != 0 && owner == null)
          throw new ObjectDisposedException("DirInfo");
        if(start == 0 || parent == 0)
          return "\\" + name;
        foreach(DirInfo di in owner.entries)
          if(di.start == this.parent)
            return di.ToString() + '\\' + name;
        return null;
      }
      #endregion

      #region Properties and Events
      public int Index 
      {
        get { return start; }
      }

      public string Name 
      {
        get { return name; }
        set 
        {
          if(name == value) return;
          if(name.Length >= 256)
            throw new IOException("File name is not valid; Too long.");
          name = value;
          Invalidate();
        }
      }

      public Archive Owner 
      {
        get { return owner; }
      }

      public int Parent 
      {
        get { return parent; }
        set 
        {
          if(parent == value) return;
          parent = value;
          Invalidate();
        }
      }
      #endregion
    }
    #endregion

    #region Internal Class [FileInfo]
    private sealed class FileInfo: DirInfo 
    {

      #region Internal Fields
      internal IntCollection seq = new IntCollection();
      private ushort lastct = 0;
      #endregion

      #region Constructors
      public FileInfo(Archive owner, string name, int start, int parent): base(owner, name, start, parent) 
      {
        int i = start;
        do 
        {
          seq.Add(i);
          owner.stream.Position = Archive.GetOffset(i);
          lastct = owner.br.ReadUInt16();
          i = owner.br.ReadInt32();
        } while(i > 0);
      }
      #endregion

      #region Methods and Overrides
      public byte Read(long pos) 
      {
        if(owner == null || owner.stream == null)
          throw new ObjectDisposedException("FileInfo");
        long j, i = Math.DivRem(pos, DATA_SIZE, out j);
        if(i >= seq.Count || (i == seq.Count - 1 && j >= lastct))
          throw new IOException("Tried to read beyond end of file.");
        lock(owner.stream) 
        {
          owner.stream.Position = Archive.GetOffset(seq[(int)i]) + j + 6;
          return owner.br.ReadByte();
        }
      }

      public void Write(long pos, byte data) 
      {
        if(owner == null || owner.stream == null)
          throw new ObjectDisposedException("FileInfo");
        long j, i = Math.DivRem(pos, DATA_SIZE, out j);
        lock(owner.stream) 
        {
          while(i >= seq.Count) 
          {
            seq.Add(owner.Reserve());
            lastct = 0;
          }
          owner.stream.Position = Archive.GetOffset(seq[(int)i]) + j++ + 6;
          owner.bw.Write(data);
          if(i == seq.Count - 1 && j > lastct)
            lastct = (ushort)j;
        }
      }
      #endregion

      #region Properties and Events
      public bool CanRead 
      {
        get { return owner.stream.CanRead; }
      }

      public bool CanSeek 
      {
        get { return owner.stream.CanRead; }
      }

      public bool CanWrite 
      {
        get { return owner.stream.CanRead; }
      }

      public long Length 
      {
        get { return (seq.Count - 1) * DATA_SIZE + lastct; }
        set 
        {
          if(owner == null || owner.stream == null)
            throw new ObjectDisposedException("FileInfo");
          lock(owner.stream) 
          {
            long j, i = Math.DivRem(value, DATA_SIZE, out j);
            if(j == 0 && i > 0) { j = (short)DATA_SIZE; --i; }
            lastct = (ushort)j;
            int l = (int)i;
            owner.stream.Position = Archive.GetOffset(l) + 2;
            owner.bw.Write(0);
            while(seq.Count > l + 1) 
            {
              owner.stream.Position = Archive.GetOffset(l + 1);
              owner.bw.Write((short)-1);
              owner.bw.Write(new byte[BLOCK_SIZE - 2]);
              seq.RemoveAt(l + 1);
            }
            while(seq.Count < l + 1) 
            {
              owner.stream.Position = Archive.GetOffset(seq.Count - 1);
              owner.bw.Write(DATA_SIZE);
              i = owner.Reserve();
              owner.bw.Write((int)i);
              seq.Add((int)i);
            }
            owner.stream.Position = Archive.GetOffset(seq.Count - 1);
            owner.bw.Write(lastct);
            owner.stream.Position += lastct + 4;
            owner.bw.Write(new byte[DATA_SIZE - lastct]);
            owner.stream.Flush();
          }
        }
      }
      #endregion
    }
    #endregion
  }
}
