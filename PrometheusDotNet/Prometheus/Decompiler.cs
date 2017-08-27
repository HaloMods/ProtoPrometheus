using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Prometheus;
using Prometheus.Core;
using System.Collections;
using Prometheus.Core.Tags;

namespace Prometheus.Core
{
  /// <summary>
  /// Summary description for Decompiler.
  /// </summary>
  /// 
  public class Decompiler
  {
    struct Map
    {
      public H1_MapHeader MapHeader;
      public H1_Index_Header IndexHeader;
      public uint Map_Magic;
      public h1_Tag_Index_Item[] IndexItems;
      public string[] IndexItemStringList;
      public Hashtable FilenameTable;
      
      public void Load(ref FileStream FI)
      {
        MapHeader = new H1_MapHeader();
        IndexHeader = new H1_Index_Header();
        MapHeader.Load(ref FI);
        FI.Seek(MapHeader.Offset_To_Index,System.IO.SeekOrigin.Begin);
        IndexHeader.Load(ref FI);
        uint StartOfTags = MapHeader.Offset_To_Index + 36 + 4;
        Map_Magic = IndexHeader.IndexMagic - StartOfTags;
        FI.Seek(StartOfTags,System.IO.SeekOrigin.Begin);
        IndexItems = new h1_Tag_Index_Item[IndexHeader.TagCount];
        IndexItemStringList = new string[IndexHeader.TagCount];
        uint BSPCount = 0;
        for (int ind = 0;ind < IndexHeader.TagCount;ind +=1)
        {
          IndexItems[ind] = new h1_Tag_Index_Item();
          IndexItems[ind].Load(ref FI);
          if (IndexItems[ind].Type1 == 0x73627370) //Test for BSP tags
          {
            uint MapFileSavePos = (uint)FI.Position;
            //Seek to Scnr
            FI.Seek(IndexItems[0].MetaOffset,System.IO.SeekOrigin.Begin);
            //Read SCNR header
            byte[] SCNRHeader = new byte[1456];
            FI.Read(SCNRHeader,0,SCNRHeader.Length);
            uint BSPSectionChunkCount = BitConverter.ToUInt32(SCNRHeader,1444);
            uint BSPSectionOffset = BitConverter.ToUInt32(SCNRHeader,1444 + 4) - Map_Magic;
            byte[] BSPInfo = new byte[32];
            FI.Seek(BSPSectionOffset + (32 * BSPCount),System.IO.SeekOrigin.Begin);
            FI.Read(BSPInfo,0,BSPInfo.Length);
            IndexItems[ind].MetaOffset = BitConverter.ToUInt32(BSPInfo,0);
            IndexItems[ind].MetaMagic = BitConverter.ToUInt32(BSPInfo,8) - IndexItems[ind].MetaOffset;
            IndexItems[ind].MetaOffset += 24;
            IndexItems[ind].OffsetToString = IndexItems[ind].OffsetToString - Map_Magic;//BitConverter.ToUInt32(BSPInfo,20) - Map_Magic;//IndexItems[ind].OffsetToString - Map_Magic;
            BSPCount +=1;
            FI.Seek(MapFileSavePos,System.IO.SeekOrigin.Begin);
          }
          else
          {
            IndexItems[ind].MetaOffset = IndexItems[ind].MetaOffset - Map_Magic;
            IndexItems[ind].OffsetToString = IndexItems[ind].OffsetToString - Map_Magic;
            IndexItems[ind].MetaMagic = Map_Magic;
          }
        }
        FilenameTable = new Hashtable((int)IndexHeader.TagCount);
        for (int strs = 0; strs < IndexHeader.TagCount;strs +=1)
        {
          IndexItemStringList[strs] = StringLoader(ref FI,IndexItems[strs].OffsetToString) + GetTagLongName(IndexItems[strs].Type1);
          FilenameTable.Add(IndexItemStringList[strs], strs);
        }
      }
    }
    struct H1_MapHeader
    {
      public uint Offset_To_Index;
      public uint Map_Version;
      public uint Decompressed_Size;
      public void Load(ref FileStream FO)
      {
        byte[] Buffer = new byte[2048];
        FO.Read(Buffer,0,Buffer.Length);
        Map_Version = BitConverter.ToUInt32(Buffer,0x04);
        Offset_To_Index = BitConverter.ToUInt32(Buffer,0x10);
        Buffer = null;
      }
    }
    struct H1_Index_Header
    {
      public uint IndexMagic;
      public uint TagCount;
      public uint Verts_Offset;
      public uint Index_Offset;
      public void Load(ref FileStream FI)
      {
        byte[] Buffer = new byte[36];
        FI.Read(Buffer,0,Buffer.Length);
        IndexMagic = BitConverter.ToUInt32(Buffer,0x00);
        TagCount = BitConverter.ToUInt32(Buffer,12);
        Verts_Offset = BitConverter.ToUInt32(Buffer,20);
        Index_Offset = BitConverter.ToUInt32(Buffer,28) + Verts_Offset;
      }
    }

    struct h1_Tag_Index_Item
    {
      public uint Type1;
      public uint Type2;
      public uint Type3;
      public uint ID;
      public uint OffsetToString;
      public uint MetaOffset;
      public uint MetaMagic;
      public void Load(ref FileStream FI)
      {
        byte[] Buffer = new byte[32];
        FI.Read(Buffer,0,Buffer.Length);
        Type1 = BitConverter.ToUInt32(Buffer,0);
        Type2 = BitConverter.ToUInt32(Buffer,4);
        Type3 = BitConverter.ToUInt32(Buffer,8);
        ID = BitConverter.ToUInt32(Buffer,12);
        OffsetToString = BitConverter.ToUInt32(Buffer,16);
        MetaOffset = BitConverter.ToUInt32(Buffer,20);
        Buffer = null;
      }
    }

    public struct sHalo2Map
    {
      public sMapHeader MapHeader;
      public sIndexHeader IndexHeader;
      public sStringTable StringTable;
      public sIndexItem[] IndexItem;
      public Hashtable FilenameTable;
      public sScnrBsp[] sbspltmpref;
      public uint MapMagic;
      public void Read(ref FileStream FI)
      {
        MapHeader.Read(ref FI);
        FI.Seek(MapHeader.IndexOffset,System.IO.SeekOrigin.Begin);
        IndexHeader.Read(ref FI);
        IndexItem = new sIndexItem[IndexHeader.TagCount];
        MapMagic = IndexHeader.IndexMagic - IndexHeader.TagStartOffset;
        StringTable.OffsetsOffset =  MapHeader.StringTableListOffset;
        StringTable.OffsetsBufferSize = MapHeader.StringTableListSize;
        StringTable.StringOffset = MapHeader.StringTableOffset;
        StringTable.StringBufferSize = MapHeader.StringTableSize;
        StringTable.Read(ref FI);
        FI.Seek(IndexHeader.TagStartOffset,System.IO.SeekOrigin.Begin);
        uint ScnrOffset = 0;
        for (int tc = 0;tc < IndexHeader.TagCount; tc +=1)
        {
          IndexItem[tc].Read(ref FI);
          //IndexItem[tc].TagOffset = IndexItem[tc].TagOffset; 
          
          byte[] strtemp = BitConverter.GetBytes(IndexItem[tc].TagType);
          string tmpstr = GetTagClass(strtemp,0);
          if (tmpstr == "<fx>")
            tmpstr = "FXFX";
          IndexItem[tc].NameString = StringTable.TagStrings[tc] + "." + tmpstr;
          StringTable.TagStrings[tc] = StringTable.TagStrings[tc] + "." + tmpstr;

        }
        MapMagic = IndexItem[0].TagOffset - MapHeader.OffsetToFirstObject;
        uint BspIndex = 0;
        for (int tc = 0;tc < IndexHeader.TagCount;tc +=1)
        {
          byte[] strtemp = BitConverter.GetBytes(IndexItem[tc].TagType);
          string tmpstr = GetTagClass(strtemp,0);

					if (tmpstr == "scnr")
					{
						byte[] TmpScnrBuff = new byte[0x3E0];
						long fpSav = FI.Position;
						IndexItem[tc].TagOffset = IndexItem[tc].TagOffset - MapMagic;
						IndexItem[tc].TagMagic = MapMagic;
						IndexItem[tc].TagOffsetMagic = MapMagic;
						FI.Seek(IndexItem[tc].TagOffset,System.IO.SeekOrigin.Begin);
						FI.Read(TmpScnrBuff,0,TmpScnrBuff.Length);
						uint BSPCount = BitConverter.ToUInt32(TmpScnrBuff,0x210); //Struct Size 0x44
						uint BSPOffset = BitConverter.ToUInt32(TmpScnrBuff,0x214) - MapMagic;
						FI.Seek((long)BSPOffset,System.IO.SeekOrigin.Begin);
						sbspltmpref = new sScnrBsp[BSPCount]; 
						for (int i = 0;i < BSPCount; i++)
						{
							sbspltmpref[i].Read(ref FI);
						}
						FI.Seek(fpSav,System.IO.SeekOrigin.Begin);
					}
					else if (tmpstr == "sbsp")
					{
						//byte[] tmp = new byte[0x10];
						//long possave = FI.Position;
						//FI.Seek((long)sbspltmpref[BspIndex].Offset,System.IO.SeekOrigin.Begin);
						//FI.Read(tmp,0,tmp.Length);
						//FI.Seek(possave,System.IO.SeekOrigin.Begin);
						IndexItem[tc].TagOffset = sbspltmpref[BspIndex].Offset + 0x10;
						IndexItem[tc].TagSize = sbspltmpref[BspIndex].Size;
						IndexItem[tc].TagMagic = MapMagic;
						IndexItem[tc].TagOffsetMagic = sbspltmpref[BspIndex].Magic - (IndexItem[tc].TagOffset - 0x10) ;
						BspIndex +=1;
					}
					else if (tmpstr == "ltmp")   //lovers brain << My wifes atemp to program...
					{
						byte[] tmp = new byte[0x10];
						long possave = FI.Position;
						FI.Seek((long)sbspltmpref[BspIndex].Offset,System.IO.SeekOrigin.Begin);
						FI.Read(tmp,0,tmp.Length);
						FI.Seek(possave,System.IO.SeekOrigin.Begin);
						IndexItem[tc].TagOffset = BitConverter.ToUInt32(tmp,0x08) - sbspltmpref[BspIndex].Magic + sbspltmpref[BspIndex].Offset; //sbspltmpref[BspIndex].Offset;
						IndexItem[tc].TagSize = BitConverter.ToUInt32(tmp,0x0);    //sbspltmpref[BspIndex].Size;
						IndexItem[tc].TagMagic = MapMagic;
						IndexItem[tc].TagOffsetMagic = sbspltmpref[BspIndex].Magic - sbspltmpref[BspIndex].Offset;
					}
					else
					{
						IndexItem[tc].TagOffset = IndexItem[tc].TagOffset - MapMagic;
						IndexItem[tc].TagMagic = MapMagic;
						IndexItem[tc].TagOffsetMagic = MapMagic;
					}
        }
        FilenameTable = new Hashtable((int)IndexHeader.TagCount);
        for (int strs = 0; strs < IndexHeader.TagCount;strs +=1)
        {
          //IndexItemStringList[strs] = StringLoader(ref FI,IndexItems[strs].OffsetToString) + GetTagLongName(IndexItems[strs].Type1);
          FilenameTable.Add(StringTable.TagStrings[strs], strs);
        }
      }
      public void Write(ref FileStream FO)
      {
        MapHeader.Write(ref FO);
        IndexHeader.Write(ref FO);
      }
    }

    public struct sScnrBsp
    {
      public byte[] data;
      public uint Offset;
      public uint Size;
      public uint Magic;
      public uint Zeros;
      public uint SbspTagName;
      public uint SbspTagID;
      public uint LtmpTagName;
      public uint LtmpTagID;
      public void Read(ref FileStream FI)
      {
        data = new byte[0x44];
        FI.Read(data,0,data.Length);
        Offset = BitConverter.ToUInt32(data,0);
        Size = BitConverter.ToUInt32(data,4);
        Magic = BitConverter.ToUInt32(data,8);
        SbspTagName = BitConverter.ToUInt32(data,16);
        SbspTagID = BitConverter.ToUInt32(data,20);
        LtmpTagName = BitConverter.ToUInt32(data,24);
        LtmpTagID = BitConverter.ToUInt32(data,28);
      }
    }
    public struct sMapHeader
    {
      public byte[] Buffer;
      public uint FileSize;
      public uint IndexOffset;
      public uint MetaStart;

      public uint OffsetToFirstObject;
      public uint StringTableOffset;
      public uint StringTableListOffset;
      public uint StringTableListSize;
      public uint StringTableSize;
      public uint ScriptStringsOffset;
      public uint ScriptStringsCount;
      public string[] ScriptStringArray;
      public void Read(ref FileStream FI)
      {
        Buffer = new byte[2048];
        FI.Read(Buffer,0,Buffer.Length);
        FileSize = BitConverter.ToUInt32(Buffer,0x08);
        IndexOffset = BitConverter.ToUInt32(Buffer,0x10);
        MetaStart = BitConverter.ToUInt32(Buffer,0x14);
        OffsetToFirstObject = IndexOffset + MetaStart;
        StringTableOffset = BitConverter.ToUInt32(Buffer,0x2c4);
        StringTableListOffset = BitConverter.ToUInt32(Buffer,0x2cc);
        StringTableSize = BitConverter.ToUInt32(Buffer,0x2c8);
        StringTableListSize = BitConverter.ToUInt32(Buffer,0x2c0) * 4;
        ScriptStringsOffset = BitConverter.ToUInt32(Buffer,0x160);
        ScriptStringsCount = BitConverter.ToUInt32(Buffer,0x164);

        byte[] ScriptBuff = new byte[0x80];
        long savpox = FI.Position;
        ScriptStringArray = new string[0];
        FI.Seek((long)ScriptStringsOffset,System.IO.SeekOrigin.Begin);
        for(int i = 0;i < ScriptStringsCount; i +=1)
        {
          FI.Read(ScriptBuff,0,0x80);
          AppendString(ref ScriptStringArray,ReadString(ScriptBuff));
        }

        FI.Seek(savpox,System.IO.SeekOrigin.Begin);
      }
      public void Write(ref FileStream FO)
      {
        FO.Write(Buffer,0,Buffer.Length);
      }
    }

    public struct sIndexHeader
    {
      public byte[] Buffer;
      public uint IndexMagic;
      public uint TagTypesCount;
      public uint OffsetToTags;
      public uint TagStartOffset;
      public uint TagCount;
      public void Read(ref FileStream FI)
      {
        Buffer = new byte[32];
        FI.Read(Buffer,0,Buffer.Length);
        IndexMagic = BitConverter.ToUInt32(Buffer,0);
        TagTypesCount = BitConverter.ToUInt32(Buffer,4);
        OffsetToTags = BitConverter.ToUInt32(Buffer,8);
        TagStartOffset = (uint)((OffsetToTags - IndexMagic) + FI.Position);
        TagCount = BitConverter.ToUInt32(Buffer,0x18);
      }
      public void Write(ref FileStream FO)
      {
        FO.Write(Buffer,0,Buffer.Length);
      }
    }
    public struct sStringTable
    {
      public uint StringOffset;
      public uint StringBufferSize;
      public uint OffsetsOffset;
      public uint OffsetsBufferSize;
      public byte[] StringBuffer;
      public byte[] OffsetsBuffer;
      public string[] TagStrings;
      public void Read(ref FileStream FI)
      {
        StringBuffer = new byte[StringBufferSize];
        OffsetsBuffer = new byte[OffsetsBufferSize];
        TagStrings = new string[OffsetsBufferSize / 4];
        FI.Seek(StringOffset,System.IO.SeekOrigin.Begin);
        FI.Read(StringBuffer,0,StringBuffer.Length);
        FI.Seek(OffsetsOffset,System.IO.SeekOrigin.Begin);
        FI.Read(OffsetsBuffer,0,OffsetsBuffer.Length);
        for (int sc = 0;sc < (TagStrings.Length);sc +=1)
        {
          TagStrings[sc] = GetString(StringBuffer,OffsetsBuffer,(uint)sc);
        }
      }
      public void Write(ref FileStream FO)
      {
        FO.Write(StringBuffer,0,StringBuffer.Length);
        FO.Write(OffsetsBuffer,0,OffsetsBuffer.Length);
      }
    }
    public struct sIndexItem
    {
      public byte[] Buffer;
      public uint TagType;
      public uint TagIndexID;
      public uint TagOffset;
      public uint TagSize;
      public uint TagMagic;
      public uint TagOffsetMagic;
      public string NameString;
      public void Read(ref FileStream FI)
      {
        Buffer = new byte[0x10];
        FI.Read(Buffer,0,Buffer.Length);
        TagType = BitConverter.ToUInt32(Buffer,0);
        TagIndexID = BitConverter.ToUInt32(Buffer,4);
        TagOffset = BitConverter.ToUInt32(Buffer,8);
        TagSize = BitConverter.ToUInt32(Buffer,0xc);
        
      }
    }
    uint IndexMagic = 0;
    public uint Magic = 0;
    public uint indexOffset = 0;
    public uint StartOfTags = 0;
    public uint TagCount = 0;
    uint MagIndex;
    uint[] BSPOffset;
    uint[] BSPMagic;
    uint[] BSPStringOffset;
    FileStream fsin;
    Map HaloMap;
    sHalo2Map Halo2Map;
    private LibraryArchive m_OutputArchive = null;
    static Hashtable strConversionTable = new Hashtable();
    public Decompiler()
    {
    }
    static private void AppendString(ref string[] str,string appstr)
    {
      string[] temparray = new string[str.Length + 1];
      str.CopyTo(temparray,0);
      str = new string[str.Length + 1];
      temparray.CopyTo(str,0);
      str[str.Length - 1] = appstr;
      temparray = null;
    }

    public void SetOutputArchive(LibraryArchive OutputArchive)
    {
      m_OutputArchive = OutputArchive;
    }

    /// <summary>
    /// Signifies that the extraction batch operation has been completed.
    /// </summary>
    public event ExtractionCompleteEventHandler ExtractionComplete;
    public delegate void ExtractionCompleteEventHandler(
      object sender,
      ExtractionCompleteEventArgs e);
    public class ExtractionCompleteEventArgs : EventArgs
    {
      public int TotalFiles;
      public ExtractionCompleteEventArgs(int totalFiles)
      {
        TotalFiles = totalFiles;
      }
    }

    /// <summary>
    /// Signifies that a file has been successfully extracted.
    /// </summary>
    public event ExtractedFileEventHandler ExtractedFile;
    public delegate void ExtractedFileEventHandler(
      object sender,
      ExtractedFileEventArgs e);
    public class ExtractedFileEventArgs : EventArgs
    {
      public int TotalFiles;
      public int FilesExtracted;
      public ExtractedFileEventArgs(int filesExtracted, int totalFiles)
      {
        TotalFiles = totalFiles;
        FilesExtracted = filesExtracted;
      }
    }

    private string[] m_batchList;
    private Thread thExtract;
    public bool ExtractTags(string[] filenames)
    {
      // Make sure the thread isn't running
      if (thExtract != null)
      {
        if (thExtract.ThreadState == ThreadState.Running) return false;
      }

      m_batchList = filenames;
      thExtract = new Thread(new ThreadStart(BatchExtract));
      thExtract.Start();
      return true;
    }
    
    public void BatchExtract()
    {
      m_OutputArchive.StartBatch();

      try
      {
        for (int fc = 0;fc < m_batchList.Length;fc +=1)
        {
          switch(m_MapVersion)
          {
            case MapfileVersion.HALOPC:
              int h1index = (int)HaloMap.FilenameTable[m_batchList[fc]];
              ExtractTag(h1index);
              //CombinedExtractTag(h1index);
              if (ExtractedFile != null)
                ExtractedFile(this, new ExtractedFileEventArgs(fc, m_batchList.Length));
              break;
            case MapfileVersion.XHALO2:
              int h2index = (int)Halo2Map.FilenameTable[m_batchList[fc]];
              ExtractH2Tag(h2index);
              //CombinedExtractTag(h2index);
              if (ExtractedFile != null)
                ExtractedFile(this, new ExtractedFileEventArgs(fc, m_batchList.Length));
              break;
          }
        }
      }
      finally
      {
        m_OutputArchive.EndBatch();
      }
      // Notify that the operation is complete
      if (ExtractionComplete != null)
        ExtractionComplete(this, new ExtractionCompleteEventArgs(m_batchList.Length));

      //m_OutputArchive.SetArchiveMode(ArchiveMode.BROWSER);
    }
    private void ExtractH2Tag(int index)
    {
      //Load Shared Map streams
      FileStream fsShared = OptionsManager.GetHalo2SharedStream();
      FileStream fsSinglePlayerShared = OptionsManager.GetHalo2SinglePlayerSharedStream();
      FileStream fsMainMenu = OptionsManager.GetHalo2MainMenuStream();

      //Tag Header loader start
      string TagClass = GetTagClass(BitConverter.GetBytes(Halo2Map.IndexItem[index].TagType),0);
      if (TagClass == "<fx>")
        TagClass = "FXFX";

      byte[] TagHeaderBuffer = new byte[64];
      OptionsManager.GetGuerillaHeader(m_MapVersion, TagClass, TagHeaderBuffer);

      string MagfilePathRoot = Application.StartupPath + @"\Tag Structures\Halo2\";
      string OutputPathRoot = Application.StartupPath + @"\Games\Xbox\Halo2\";
          
          
      //Creating Directorys for extacted tag
      string[] Directorys = new string[256];
      string DirSep = @"\";
      Directorys = Halo2Map.StringTable.TagStrings[index].Split(DirSep.ToCharArray() ,256);
      uint Dircount = (uint)Directorys.Length - 1;
      string NewDirectoryStructure = "";
      for (uint Count = 0;Count < Dircount; Count +=1)
      {
        if (Directory.Exists(OutputPathRoot + NewDirectoryStructure + Directorys[Count]) == false)
        {
          Directory.CreateDirectory(OutputPathRoot + NewDirectoryStructure + Directorys[Count]);
          NewDirectoryStructure +=  Directorys[Count] + @"\";
        }
        else
        {
          NewDirectoryStructure +=  Directorys[Count] + @"\";
        }
      }

      //Loading Tag Long name from mag file
      string ProcessTagFile = TagClass.Trim() + ".mag";
      StreamReader MagReader = new StreamReader(MagfilePathRoot + ProcessTagFile);
      string InLine = MagReader.ReadLine(); 
      
      //Create the Tag on disk
      string SplitChar = ".";
      string[] FixTagName = Halo2Map.StringTable.TagStrings[index].Split(SplitChar.ToCharArray(),2);
      FileInfo fiout = new FileInfo(OptionsManager.GetExtractPath(m_MapVersion) + FixTagName[0] + "." + InLine.Trim());
      MemoryStream msout = new MemoryStream();
      TagHeader.SeekToTagDataStart(ref msout);

      // Create a tag table for
      byte[] TagTable = new byte[TagCount * 16];
      fsin.Seek(Halo2Map.IndexHeader.OffsetToTags,System.IO.SeekOrigin.Begin);
      fsin.Read(TagTable,0,TagTable.Length);

      // Move File pointer to start of tag
      fsin.Seek(Halo2Map.IndexItem[index].TagOffset,System.IO.SeekOrigin.Begin);

      // Create the Tab char to provent errors
      byte TabByte = 0x09;
      string TabReplace = "";
      TabReplace += (char)TabByte;

      // Read the Structure from the mag file
      string[] MagArray = new string[256];
      int MagIndex = 0;
      do 
      {
        InLine = MagReader.ReadLine();
        MagArray[MagIndex] = InLine.Replace(TabReplace,"").ToLower().Trim();
        MagIndex= MagIndex + 1;
      }while(MagReader.Peek() != -1);
      MagReader.Close();

      // Create debug stream writer
      //StreamWriter DebugFile;
      //DebugFile = new StreamWriter(msout.Name + ".txt" );

      // Start the Tags extraction
      Halo2Structure MainStruct = new Halo2Structure();
      //if(TagClass == "sbsp")
        MainStruct.StructureCS((uint)MagIndex,Halo2Map.IndexItem[index].TagOffsetMagic,Halo2Map.IndexItem[index].TagMagic,MagArray,1,fsin,msout,fsShared,0,0,Halo2Map.StringTable.StringBuffer,Halo2Map.StringTable.OffsetsBuffer,fsSinglePlayerShared,fsMainMenu,fsShared,Halo2Map.MapHeader.MetaStart,Halo2Map.MapHeader.FileSize,null,TagTable,Halo2Map.MapHeader.ScriptStringArray);
			//DebugFile.Flush();
      //DebugFile.Close();
      //For Refernce only
      //public void StructureCS(uint MagIndex,uint OffsetMagic,uint MapMagic,string[] StructureArray,uint ChunkCount,FileStream MapFile,FileStream TagFile,FileStream BitmapFile,uint VerticesOffset,uint IndicesOffset,byte[] StringTable,byte[] StringTableOffsetList,FileStream single_player_shared_map,FileStream mainmenu_map,FileStream shared_map,uint StartOfTags,uint MapSize,StreamWriter DebugFile,byte[] TagTable,string[] ScriptStringArray)

      // Initialize and write out the PROM tag header
      TagHeader tag_hdr = new TagHeader();
      tag_hdr.TagClass0 = TagClass;
      tag_hdr.GameVersion = (MapfileVersion)m_MapVersion;
      tag_hdr.TagSize = (int)msout.Position - TagHeader.PROM_HEADER_SIZE;
      tag_hdr.Write(ref msout);

      //Write out a zero-attachment header
      tag_hdr.SeekToAttachStart(ref msout);
      AttachmentHeader attach_hdr = new AttachmentHeader();
      attach_hdr.Write(ref msout);

      msout.Close();
      fsShared.Close();
      fsSinglePlayerShared.Close();
      fsMainMenu.Close();
    }
    private void ExtractTag(int index)
    {
      FileStream fsBitmap = OptionsManager.GetBitmapStream(m_MapVersion);
      FileStream fsSound = OptionsManager.GetSoundStream(m_MapVersion);

      string TagClass = GetTagClass(BitConverter.GetBytes(HaloMap.IndexItems[index].Type1),0);

      //Loading Tag Long name and seting the map version folder
      string ProcessTagFile = TagClass.Trim() + ".mag";
      string MagfilePathRoot = "";
      string OutputPathRoot = "";
      switch(m_MapVersion)
      {
        case MapfileVersion.HALOPC:
          MagfilePathRoot = Application.StartupPath + @"\Tag Structures\PcHalo\";
          OutputPathRoot = Application.StartupPath + @"\Games\Pc\Halo\";
          break;
        case MapfileVersion.HALOCE:
          MagfilePathRoot = Application.StartupPath + @"\Tag Structures\CeHalo\";
          OutputPathRoot = Application.StartupPath + @"\Games\Pc\Halo\";
          break;
        case MapfileVersion.XHALO1:
          MagfilePathRoot = Application.StartupPath + @"\Tag Structures\XHalo\";
          OutputPathRoot = Application.StartupPath + @"\Games\Xbox\Halo\";
          break;
      }

      //Creating Directorys for extracted tags;
      string[] Directorys = new string[256];
      string DirSep = @"\";
      Directorys = HaloMap.IndexItemStringList[index].Split(DirSep.ToCharArray() ,256);
      uint Dircount = (uint)Directorys.Length - 1;
      string NewDirectoryStructure = "";
      for (uint Count = 0;Count < Dircount; Count +=1)
      {
        if (Directory.Exists(OutputPathRoot + NewDirectoryStructure + Directorys[Count]) == false)
        {
          Directory.CreateDirectory(OutputPathRoot + NewDirectoryStructure + Directorys[Count]);
          NewDirectoryStructure +=  Directorys[Count] + @"\";
        }
        else
        {
          NewDirectoryStructure +=  Directorys[Count] + @"\";
        }
      }

      StreamReader MagReader = new StreamReader(MagfilePathRoot + ProcessTagFile);
      string InLine = MagReader.ReadLine(); 
              
      FileInfo fiout = new FileInfo(OutputPathRoot + HaloMap.IndexItemStringList[index]);
      MemoryStream msout = new MemoryStream();
      //msout = fiout.Open(FileMode.Create,FileAccess.ReadWrite);  //Grenadiac changed this to RW for archive hack
      TagHeader.SeekToTagDataStart(ref msout);

      fsin.Seek(HaloMap.IndexItems[index].MetaOffset,System.IO.SeekOrigin.Begin);

      byte TabByte = 0x09;
      string TabReplace = "";
      TabReplace += (char)TabByte;

      string[] MagArray = new string[256];
      int MagIndex = 0;
      do 
      {
        InLine = MagReader.ReadLine();
        //MagArray[Count] = InLine.Split(new char[]{' '},256);
        MagArray[MagIndex] = InLine.Replace(TabReplace,"").ToLower().Trim();
        MagIndex= MagIndex + 1;
      }while (MagReader.Peek() != -1 );
      MagReader.Close();

      Structure2 MainStruct = new Structure2();
      MainStruct.StructureCS(HaloMap.IndexItems[index].MetaOffset,(uint)MagIndex,HaloMap.IndexItems[index].MetaMagic,HaloMap.Map_Magic,MagArray,1,fsin,msout,fsBitmap,fsSound,HaloMap.IndexHeader.Verts_Offset,HaloMap.IndexHeader.Index_Offset,HaloMap.MapHeader.Map_Version);
      //public void StructureCS(uint MetaOffset,uint MagIndex,uint OffsetMagic,uint MapMagic,string[] StructureArray,uint ChunkCount,FileStream MapFile,FileStream TagFile,FileStream BitmapFile,FileStream SoundFile,uint VerticesOffset,uint IndicesOffset,StreamWriter ObjFile,uint MapVersion)

      // Initialize and write out the PROM tag header
      TagHeader tag_hdr = new TagHeader();
      tag_hdr.TagClass0 = TagClass;
      tag_hdr.GameVersion = (MapfileVersion)m_MapVersion;
      tag_hdr.TagSize = (int)msout.Position - TagHeader.PROM_HEADER_SIZE;
      tag_hdr.Write(ref msout);

      //Write out a zero-attachment header
      tag_hdr.SeekToAttachStart(ref msout);
      AttachmentHeader attach_hdr = new AttachmentHeader();
      attach_hdr.Write(ref msout);

      //GRENADIAC HACK to put tagfile into archive
      if(m_OutputArchive != null)
      {
        m_OutputArchive.AddTagfileToArchive(HaloMap.IndexItemStringList[index], msout.GetBuffer(), (int)msout.Position);
      }


      msout.Close();
      fsBitmap.Close();
      fsSound.Close();
    }

    private void CombinedExtractTag(int index)
    {
      FileStream fsBitmap = null;
      FileStream fsSound = null;
      FileStream fsShared = null;
      FileStream fsSinglePlayerShared = null;
      FileStream fsMainMenu = null;

      switch(m_MapVersion)
      {
        case MapfileVersion.HALOPC:
        case MapfileVersion.HALOCE:
          fsBitmap = OptionsManager.GetBitmapStream(m_MapVersion);
          fsSound = OptionsManager.GetSoundStream(m_MapVersion);
          break;
        case MapfileVersion.XHALO2:
          fsShared = OptionsManager.GetHalo2SharedStream();
          fsSinglePlayerShared = OptionsManager.GetHalo2SinglePlayerSharedStream();
          fsMainMenu = OptionsManager.GetHalo2MainMenuStream();
          break;
      }

      string TagClass;
      if(m_MapVersion == MapfileVersion.XHALO2)
        TagClass = GetTagClass(BitConverter.GetBytes(Halo2Map.IndexItem[index].TagType),0);
      else
        TagClass = GetTagClass(BitConverter.GetBytes(HaloMap.IndexItems[index].Type1),0);

      if(TagClass == "<fx>")
        TagClass = "FXFX";
      
      //byte[] TagHeaderBuffer = new byte[64];
      //OptionsManager.GetGuerillaHeader(m_MapVersion, TagClass, TagHeaderBuffer);

      //Loading Tag Long name and seting the map version folder
      string MagfilePathRoot = OptionsManager.GetMagfilePath(m_MapVersion);
      string OutputPathRoot = OptionsManager.GetExtractPath(m_MapVersion);

      //Creating Directorys for extracted tags;
      string[] Directorys = new string[256];
      string DirSep = @"\";
      
      if(m_MapVersion == MapfileVersion.XHALO2)
        Directorys = Halo2Map.StringTable.TagStrings[index].Split(DirSep.ToCharArray() ,256);
      else
        Directorys = HaloMap.IndexItemStringList[index].Split(DirSep.ToCharArray() ,256);

      //Create tag directory if it doesn't exist
      uint Dircount = (uint)Directorys.Length - 1;
      string NewDirectoryStructure = "";
      for (uint Count = 0;Count < Dircount; Count +=1)
      {
        if (Directory.Exists(OutputPathRoot + NewDirectoryStructure + Directorys[Count]) == false)
        {
          Directory.CreateDirectory(OutputPathRoot + NewDirectoryStructure + Directorys[Count]);
          NewDirectoryStructure +=  Directorys[Count] + @"\";
        }
        else
        {
          NewDirectoryStructure +=  Directorys[Count] + @"\";
        }
      }

      //Parse mag file
      string ProcessTagFile = TagClass.Trim() + ".mag";
      StreamReader MagReader = new StreamReader(MagfilePathRoot + ProcessTagFile);
      string InLine = MagReader.ReadLine();
      string TagExtension = InLine;
      byte TabByte = 0x09;
      string TabReplace = "";
      TabReplace += (char)TabByte;

      string[] MagArray = new string[256];
      int MagIndex = 0;
      do 
      {
        InLine = MagReader.ReadLine();
        MagArray[MagIndex] = InLine.Replace(TabReplace,"").ToLower().Trim();
        MagIndex++;
      }while(MagReader.Peek() != -1);
      MagReader.Close();

      //Create output tag stream/file
      FileInfo fiout;
      if(m_MapVersion == MapfileVersion.XHALO2)
      {
        string SplitChar = ".";
        string[] FixTagName = Halo2Map.StringTable.TagStrings[index].Split(SplitChar.ToCharArray(),2);
        fiout = new FileInfo(OutputPathRoot + FixTagName[0] + "." + TagExtension.Trim());
      }
      else
      {
        fiout = new FileInfo(OutputPathRoot + HaloMap.IndexItemStringList[index]);
      }

      MemoryStream msout = new MemoryStream();
      //msout = fiout.Open(FileMode.Create,FileAccess.ReadWrite);  //Grenadiac changed this to RW for archive hack
      //msout.Seek(0,System.IO.SeekOrigin.Begin);
      TagHeader.SeekToTagDataStart(ref msout);
      //msout.Write(TagHeaderBuffer,0,64);

      //Parse the meta and reformat output tag according to Magfile rules
      if(m_MapVersion == MapfileVersion.XHALO2)
      {
        // Create debug stream writer
        StreamWriter DebugFile;
        DebugFile = null;//new StreamWriter(Application.StartupPath + @"\TagDebug.txt");
        byte[] TagTable = new byte[TagCount * 16];
        fsin.Seek(Halo2Map.IndexHeader.OffsetToTags,System.IO.SeekOrigin.Begin);
        fsin.Read(TagTable,0,TagTable.Length);
        Halo2Structure MainStruct = new Halo2Structure();
        if(TagClass == "sbsp")
          MainStruct.StructureCS((uint)MagIndex,Halo2Map.IndexItem[index].TagOffsetMagic,Halo2Map.IndexItem[index].TagMagic,MagArray,1,fsin,msout,fsShared,0,0,Halo2Map.StringTable.StringBuffer,Halo2Map.StringTable.OffsetsBuffer,fsSinglePlayerShared,fsMainMenu,fsShared,Halo2Map.MapHeader.MetaStart,Halo2Map.MapHeader.FileSize,DebugFile,TagTable,Halo2Map.MapHeader.ScriptStringArray);
        fsShared.Close();
        fsSinglePlayerShared.Close();
        fsMainMenu.Close();
      }
      else
      {
        fsin.Seek(HaloMap.IndexItems[index].MetaOffset,System.IO.SeekOrigin.Begin);
        Structure2 MainStruct = new Structure2();
        MainStruct.StructureCS(HaloMap.IndexItems[index].MetaOffset,(uint)MagIndex,HaloMap.IndexItems[index].MetaMagic,HaloMap.Map_Magic,MagArray,1,fsin,msout,fsBitmap,fsSound,HaloMap.IndexHeader.Verts_Offset,HaloMap.IndexHeader.Index_Offset,HaloMap.MapHeader.Map_Version);
        fsBitmap.Close();
        fsSound.Close();
      }

      // Initialize and write out the PROM tag header
      TagHeader tag_hdr = new TagHeader();
      tag_hdr.TagClass0 = TagClass;
      tag_hdr.GameVersion = (MapfileVersion)m_MapVersion;
      tag_hdr.TagSize = (int)msout.Position - TagHeader.PROM_HEADER_SIZE;
      tag_hdr.Write(ref msout);

      //Write out a zero-attachment header
      AttachmentHeader attach_hdr = new AttachmentHeader();
      attach_hdr.Write(ref msout);

      if(m_OutputArchive != null)
      {
        m_OutputArchive.AddTagfileToArchive(HaloMap.IndexItemStringList[index], msout.GetBuffer(), (int)msout.Position);
      }

      msout.Close();
    }

    public void InitStringConversionTable()
    {
      //strConversionTable = new Hashtable();
      if (strConversionTable.Count == 0)
      {
        strConversionTable.Add("bitm" , "Bitmap");
        strConversionTable.Add("scnr" , "Scenario");
        strConversionTable.Add("sbsp" , "Scenario_Structure_Bsp");
        strConversionTable.Add("ant!", "Antenna");
        strConversionTable.Add("actr", "Actor");
        strConversionTable.Add("actv", "Actor Variant");
        strConversionTable.Add("antr", "Model_Animations");
        strConversionTable.Add("bipd", "Biped");
        strConversionTable.Add("ctrl", "Control");
        strConversionTable.Add("coll", "Model_Collision_Geometry");
        strConversionTable.Add("cont", "Contrail");
        strConversionTable.Add("deca", "Decal");
        strConversionTable.Add("jpt!", "Damage_Effect");
        strConversionTable.Add("udlg", "Dialogue");
        strConversionTable.Add("dobc", "Detail_Object_Collection");
        strConversionTable.Add("DeLa", "UI_Widget_Definition");
        strConversionTable.Add("eqip", "Equipment");
        strConversionTable.Add("effe", "Effect");
        strConversionTable.Add("fog ", "Fog");
        strConversionTable.Add("elec", "Lightning");
        strConversionTable.Add("font", "Font");
        strConversionTable.Add("garb", "Garbage");
        strConversionTable.Add("grhi", "Grenade_HUD_Interface");
        strConversionTable.Add("matg", "Globals");
        strConversionTable.Add("glw!", "Glow");
        strConversionTable.Add("hud#", "HUD Number");
        strConversionTable.Add("hudg", "HUD Globals");
        strConversionTable.Add("hmt ", "HUD Message Text");
        strConversionTable.Add("item", "Item");
        strConversionTable.Add("itmc", "Item Collection");
        strConversionTable.Add("lens", "Lens");
        strConversionTable.Add("lsnd", "Sound_Looping");
        strConversionTable.Add("lifi", "Light Fixture");
        strConversionTable.Add("ligh", "Light");
        strConversionTable.Add("mgs2", "Light_Volume");
        strConversionTable.Add("mach", "Machine");
        strConversionTable.Add("mply", "Multiplayer Scenario");
        strConversionTable.Add("metr", "Meter");
        strConversionTable.Add("part", "Particle");
        strConversionTable.Add("pctl", "Particle System");
        strConversionTable.Add("phys", "Physics");
        strConversionTable.Add("pphy", "Point Physics");
        strConversionTable.Add("mod2", "GBXModel");
        strConversionTable.Add("proj", "Projectile");
        strConversionTable.Add("devc", "PC Device Default");
        strConversionTable.Add("snd!", "Sound");
        strConversionTable.Add("ssce", "Sound_Scenery");
        strConversionTable.Add("snde", "Sound_Environment");
        strConversionTable.Add("scen", "Scenery");
        strConversionTable.Add("senv", "Shader_Environment");
        strConversionTable.Add("soso", "Shader_Model");
        strConversionTable.Add("sotr", "Shader_Transparency_Generic");
        strConversionTable.Add("swat", "Shader_Transparent_Water");
        strConversionTable.Add("sgla", "Shader_Transparent_Glass");
        strConversionTable.Add("smet", "Shader_Transparent_Meter");
        strConversionTable.Add("spla", "Shader_Transparent_Plasma");
        strConversionTable.Add("schi", "Shader_Transparent_Chicago");
        strConversionTable.Add("sky ", "Sky");
        strConversionTable.Add("trak", "Camera_Track");
        strConversionTable.Add("unhi", "Unit_HUD_Interface");
        strConversionTable.Add("ustr", "Unicode_String_list");
        strConversionTable.Add("scex", "Shader_Transparent_Chicago_Extended");
        strConversionTable.Add("tagc", "Tag_Collection");
        strConversionTable.Add("foot", "Material_Effects");
        strConversionTable.Add("str#", "String_List");
        strConversionTable.Add("colo", "Color_Table");
        strConversionTable.Add("flag", "Flag"); 
        strConversionTable.Add("Soul", "UI_Widget_Collection");
        strConversionTable.Add("vehi", "Vehicle");
        strConversionTable.Add("vcky", "Virtual_keyboard");
        strConversionTable.Add("weap", "Weapon");
        strConversionTable.Add("wphi", "Weapon_HUD_Interface");
        strConversionTable.Add("rain", "Weather_Particle_System");
        strConversionTable.Add("wind", "Wind");
        strConversionTable.Add("mode", "Model");
        strConversionTable.Add("plac", "Placeholder");
        strConversionTable.Add("shdr", "Shader");
        strConversionTable.Add("unit", "Unit");
        strConversionTable.Add("cdmg", "Continuous_Damage_Effect");
        strConversionTable.Add("ÿÿÿÿ" , "NULL");
      }
    }
  
    static public string GetTagLongName(uint TagType)
    {
      byte[] sf = new byte[4];
      sf = BitConverter.GetBytes(TagType);
      string Class = GetTagClass(sf,0);
      string LongName = (string)strConversionTable[Class];
      return LongName;
    }
    public MapfileVersion m_MapVersion;
    public uint InitDecompiler(string FileName)
    {
      InitStringConversionTable();
      FileInfo fiin = new FileInfo(FileName);
      fsin = fiin.Open(FileMode.Open,FileAccess.ReadWrite);
      byte[] TempHeaderBuffer = new byte[0x800];
      fsin.Read(TempHeaderBuffer,0,0x800);
      fsin.Seek(0,System.IO.SeekOrigin.Begin);

      m_MapVersion = (MapfileVersion)BitConverter.ToUInt32(TempHeaderBuffer,0x04);
      TempHeaderBuffer = null;
      
      switch(m_MapVersion)
      {
        case MapfileVersion.XHALO1:
          HaloMap = new Map();
          HaloMap.Load(ref fsin);
          break;
        case MapfileVersion.HALOPC:
        case MapfileVersion.HALOCE:
          HaloMap = new Map();
          HaloMap.Load(ref fsin);
          break;
        case MapfileVersion.XHALO2:
          Halo2Map = new sHalo2Map();
          Halo2Map.Read(ref fsin);
          break;
      }

      //HaloMap = new Map();
      //HaloMap.Load(ref fsin);
      
      return((uint)m_MapVersion);
    }
    public string[] GetStringList()
    {
      switch (m_MapVersion)
      {
        case MapfileVersion.XHALO1:
        case MapfileVersion.HALOPC:
          return HaloMap.IndexItemStringList;
        case MapfileVersion.XHALO2:
          return Halo2Map.StringTable.TagStrings;
      }
      return HaloMap.IndexItemStringList;
    }
    static private string ReadString(byte[] Array)
    {
      string OutString = "";
      uint count = 0;
      do 
      {
        OutString += (char)Array[count];
        count += 1;
      } while (Array[count] != 00);
      return OutString;
    }

    static private string GetTagClass(byte[] Array,uint Offset)
    {
      string NewString = "";
      for (uint count = 4; count > 0; count -=1)
      {
        NewString += (char)Array[count + Offset-1];
      }
      return NewString;
    }

    private uint GetULong(byte[] Array,uint Offset)
    {
      uint RetVal = Array[Offset + 1];
      RetVal = (RetVal << 8) + (uint)Array[Offset];
      //RetVal = RetVal + (uint)Array[Offset];
      return RetVal;
    }

    private uint GetUInt(byte[] Array,uint Offset)
    {
      uint RetVal = Array[Offset + 3];
      RetVal = (RetVal << 8) + (uint)Array[Offset + 2];
      RetVal = (RetVal << 8) + (uint)Array[Offset + 1];
      RetVal = (RetVal << 8) + (uint)Array[Offset];
      return RetVal;
    }

    private void PutUInt(byte[] Array,uint Value,uint Offset)
    {
      Array[Offset + 3] = (byte)((((Value >> 8) >> 8) >> 8) & 0xff);
      Array[Offset + 2] = (byte)(((Value >> 8) >> 8) & 0xff);
      Array[Offset + 1] = (byte)((Value >> 8) & 0xff);
      Array[Offset] = (byte)(Value & 0xff);
    }

    private void SwapInt(byte[] Array,uint Offset)
    {
      byte st;
      st = Array[Offset + 3];
      Array[Offset + 3] = Array[Offset + 0];
      Array[Offset + 0] = st;
      st = Array[Offset + 2];
      Array[Offset + 2] = Array[Offset + 1];
      Array[Offset + 1] = st;
    }
    static public string StringLoader(ref FileStream FI,uint Offset)
    {
      long possave = FI.Position;
      byte[] Buffer = new byte[256];
      FI.Seek(Offset,System.IO.SeekOrigin.Begin);
      FI.Read(Buffer,0,Buffer.Length);
      FI.Seek(possave,System.IO.SeekOrigin.Begin);
      string Temp = ReadString(Buffer) + ".";
      return Temp;
    }
    static public string GetString(byte[] StrBuf,byte[] OffBuf,uint StrInd)
    {
      string OutString = "";
      uint count = BitConverter.ToUInt32(OffBuf,(int)(StrInd * 4));
      do 
      {
        OutString += (char)StrBuf[count];
        count += 1;
      } while (StrBuf[count] != 00);
      return OutString;
    }
  }
}
