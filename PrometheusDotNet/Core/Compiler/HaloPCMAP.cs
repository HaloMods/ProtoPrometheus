using System;
using System.IO;
using Prometheus.Core.Tags;
using System.Windows.Forms;
using Prometheus.Core.Project;
//using XpertStudios.Halo;

namespace Prometheus.Core.Compiler
{
	/// <summary>
	/// Summary description for HaloPCMAP.
	/// </summary>
	public class HaloPCMAP
	{
		public struct HaloPC
		{
			public sPCHeader PCHeader;
			public byte[] BspArea;
			public byte[] SoundsArea;
			public byte[] BitmapsArea;
			public byte[] VerticeArea;
			public byte[] IndeicesArea;
			public sPCIndexHeader PCIndexHeader;
			public byte[] MetaArea;
		}
		public struct sPCHeader
		{
			public long Position;
			public byte[] data;
			public uint Head;
			public uint MapVersion;
			public uint MapSize;
			public uint IndexOffset;
			public uint MetaSize;
			public byte[] Name;
			public string MapName;
			public byte[] BuildDate;
			public uint Unk2;
			public uint Unk3;
			public void Create()
			{
				data = new byte[0x800];
				BuildDate = new byte[32];
				string test = "01.00.00.0564";
				StringToByteArray(ref BuildDate,test);
			}
			public void Read(ref FileStream FI)
			{
				data = new byte[0x800];
				FI.Read(data,0,data.Length);
				GetUInt(data,0x00,ref Head);
				GetUInt(data,0x04,ref MapVersion);
				GetUInt(data,0x08,ref MapSize);
				GetUInt(data,0x10,ref IndexOffset);
			}
			public void Write(ref FileStream FO)
			{
				Position = FO.Position;
				PutUInt(ref data,0x00,0x68656164);
				PutUInt(ref data,0x04,MapVersion);
				PutUInt(ref data,0x08,MapSize);
				PutUInt(ref data,0x10,IndexOffset);
				PutUInt(ref data,0x14,MetaSize);
				PutUInt(ref data,0x7FC,0x666F6F74);
				Name = new byte[0x20];
				StringToByteArray(ref Name,MapName);
				Name.CopyTo(data,0x20);
				BuildDate.CopyTo(data,0x40);
				FO.Write(data,0,data.Length);
			}
		}
		public struct sPCIndexHeader
		{
			public long Position;
			public byte[] data;
			public uint IndexMagic;
			public uint ScnrID;
			public uint Unk1;
			public uint TagCount;
			public uint ModelCount1;
			public uint VertsOffset;
			public uint ModelCount2;
			public uint IndicesOffset;
			public uint ModelAreaSize;
			public sPCIndexItem[] IndexItems;
			public void Create(uint IndexCount)
			{
				data = new byte[0x28];
				IndexItems = new sPCIndexItem[IndexCount];
				TagCount = IndexCount;
				IndexMagic = 0x40440028;
			}
			public void Read()
			{
			}
			public void Write(ref FileStream FO)
			{
				Position = FO.Position;
				PutUInt(ref data,0x00,IndexMagic);
				PutUInt(ref data,0x20,ModelAreaSize);
				PutUInt(ref data,0x14,VertsOffset);
				PutUInt(ref data,0x1C,IndicesOffset);
				PutUInt(ref data,0x0C,TagCount);
				PutUInt(ref data,0x04,ScnrID);
				FO.Write(data,0,data.Length);

			}
		}
		public struct sPCIndexItem
		{
			public long Position;
			public byte[] data;
			public uint TagClass1;
			public uint TagClass2;
			public uint TagClass3;
			public ushort IndexID1;
			public ushort IndexID2;
			public byte[] String;
			public uint StringOffset;
			public uint MetaOffset;
			public uint MetaDataID;
			public string NameString;
			public uint MetaSize;
			public void Create()
			{
				data = new byte[0x20];
			}
			public void Read(ref FileStream FI)
			{
			}
			public void Write(ref FileStream FO)
			{
				Position = FO.Position;
				PutUInt(ref data,0x00,TagClass1);
				SwapInt(ref data,0x00);
				PutUInt(ref data,0x04,TagClass2);
				SwapInt(ref data,0x04);
				PutUInt(ref data,0x08,TagClass3);
				SwapInt(ref data,0x08);
				PutUShort(ref data,0x0C,IndexID1);
				PutUShort(ref data,0x0E,IndexID2);
				PutUInt(ref data,0x10,StringOffset);
				PutUInt(ref data,0x14,MetaOffset);
				PutUInt(ref data,0x18,MetaDataID);
				PutUInt(ref data,0x1C,0x00000000);
				FO.Write(data,0,data.Length);
			}
			public string GetString()
			{
				return ReadString(String,0);
			}
		}
		public static void StringToByteArray(ref byte[] Dest,string Src)
		{
			char[] tmp = new char[Src.Length];
			Src.CopyTo(0,tmp,0,tmp.Length);
			for(int i = 0;i < tmp.Length;i++)
			{
				Dest[i] = (byte)tmp[i];
			}
		}
		private static string ReadString(byte[] Array,uint Offset)
		{
			string OutString = "";
			uint count = Offset;
			do 
			{
				OutString += (char)Array[count];
				count += 1;
			} while (Array[count] != 00);
			return OutString;
		}
		public HaloPCMAP()
		{
			//
			// TODO: Add constructor logic here
			//
		}
//    private void SwapInt(byte[] Array,uint Offset)
//    {
//      byte st;
//      st = Array[Offset + 3];
//      Array[Offset + 3] = Array[Offset + 0];
//      Array[Offset + 0] = st;
//      st = Array[Offset + 2];
//      Array[Offset + 2] = Array[Offset + 1];
//      Array[Offset + 1] = st;
//    }
    private static uint SwapUInt(uint Value)
    {
      byte[] tmp = BitConverter.GetBytes(Value);
      SwapInt(ref tmp,0);
      return BitConverter.ToUInt32(tmp,0);
    }		
    public static void SwapInt(ref byte[] Array,uint Offset)
		{
			if (Array.Length - 4 >= Offset)
			{
				byte st;
				st = Array[Offset + 3];
				Array[Offset + 3] = Array[Offset + 0];
				Array[Offset + 0] = st;
				st = Array[Offset + 2];
				Array[Offset + 2] = Array[Offset + 1];
				Array[Offset + 1] = st;
			}
		}
		public static void GetUInt(byte[] data,uint offset,ref uint Value)
		{
			Value = BitConverter.ToUInt32(data,(int)offset);
		}
		public static void PutUInt(ref byte[] data,uint offset,uint Value)
		{
			byte[] tmp = BitConverter.GetBytes(Value);
			tmp.CopyTo(data,(long)offset);
		}
		public static void GetUShort(byte[] data,uint offset,ref ushort Value)
		{
			Value = BitConverter.ToUInt16(data,(int)offset);
		}
		public static void PutUShort(ref byte[] data,uint offset,ushort Value)
		{
			byte[] tmp = BitConverter.GetBytes(Value);
			tmp.CopyTo(data,(long)offset);
		}
		public static void FixPosition(ref FileStream FX)
		{
			if ((FX.Position % 0x200) != 0)
			{
				uint Size = (0x200 - ((uint)FX.Position % 0x200));
				byte[] tmp = new byte[Size];
				FX.Write(tmp,0,tmp.Length);
			}
		}
		public static void FixIntPosition(ref FileStream FX)
		{
			if ((FX.Position % 0x04) != 0)
			{
				uint Size = (0x04 - ((uint)FX.Position % 0x04));
				byte[] tmp = new byte[Size];
				FX.Write(tmp,0,tmp.Length);
			}
		}
    public static void Build()
    {
      TagFileName scenario_tag = ProjectManager.ScenarioTagFileName;

      //IndexBuilder IndexBuild = new IndexBuilder();
      Compiler_Gren.DependencyBuilder IndexBuild = new Compiler_Gren.DependencyBuilder();
      //FileInfo TagFile_info = new FileInfo(scenario_tag.RelativePath);
      string BasFolder = Application.StartupPath + @"\Extracted Tags\";
      string StructFolder = Application.StartupPath + @"\Tag Structures\PcHalo\";
      //string MapsFolder = Application.StartupPath + @"\Maps\";
      if (true)
      {
        //Create the index table
        //lbCurTag.Text = "Building Index Table....";
        Application.DoEvents();
        //string[] IndexList = IndexBuild.BuildIndex(scenario_tag.RelativePath, BasFolder, StructFolder);
        string[] IndexList = IndexBuild.BuildIndex(scenario_tag);
        HaloPCMAP.HaloPC NewMap = new HaloPCMAP.HaloPC();
        NewMap.PCIndexHeader.Create((uint)IndexList.Length);
        NewMap.PCIndexHeader.IndexMagic = 0x40440028;
        uint StringTableSize = 0;
        //Fixing index items
        ushort gt = 0xE174;
        //lbCurTag.Text = "Building Index Table Items....";
        Application.DoEvents();

        for(int sc = 0;sc < IndexList.Length;sc++)
        {
          //FileInfo TagFile2_info = new FileInfo(BasFolder + IndexList[sc]);
          //FileStream TagFile2;
          //TagFile2 = TagFile2_info.Open(FileMode.Open,FileAccess.Read);
          //byte[] tmp = new byte[0x40]; 
          //TagFile2.Read(tmp,0x00,tmp.Length);
          //TagFile2.Close();

          TagFileName tfnTAG = new TagFileName(IndexList[sc],MapfileVersion.HALOPC);
          TagBase tbTAG = new TagBase();
          tbTAG.LoadTagBuffer(tfnTAG);

          //uint TagClass1 = BitConverter.ToUInt32(tmp,0x24);
          //uint TagClass2 = BitConverter.ToUInt32(tmp,0x28);
          //uint TagClass3 = BitConverter.ToUInt32(tmp,0x2C);
          NewMap.PCIndexHeader.IndexItems[sc].Create();
          NewMap.PCIndexHeader.IndexItems[sc].TagClass1 = SwapUInt(tbTAG.Header.TagClass0);// TagClass1;
          NewMap.PCIndexHeader.IndexItems[sc].TagClass2 = SwapUInt(tbTAG.Header.TagClass1);// TagClass2;
          NewMap.PCIndexHeader.IndexItems[sc].TagClass3 = SwapUInt(tbTAG.Header.TagClass2);// TagClass3;
          NewMap.PCIndexHeader.IndexItems[sc].IndexID1 = (ushort)sc;
          NewMap.PCIndexHeader.IndexItems[sc].IndexID2 = gt;
          NewMap.PCIndexHeader.IndexItems[sc].NameString = IndexList[sc];
          if (tbTAG.Header.TagClass0 == 0x70736273 || tbTAG.Header.TagClass0 == 0x73627370)
          {
            NewMap.PCIndexHeader.IndexItems[sc].MetaSize = IndexBuild.BSPSizes[0];
          }
          else
          {
            NewMap.PCIndexHeader.IndexItems[sc].MetaSize = 0;
          }
          if(tbTAG.Header.TagClass0 == 0x7274656d || tbTAG.Header.TagClass0 == 0x6d657472)//  6d657472)
          {
            gt += 2;
          }
          else
          {
            gt += 1;
          }
          string[] RemoveExt = IndexList[sc].Split(new char[]{'.'},256);
          NewMap.PCIndexHeader.IndexItems[sc].String = new byte[RemoveExt[0].Length + 1];
          HaloPCMAP.StringToByteArray(ref NewMap.PCIndexHeader.IndexItems[sc].String,RemoveExt[0]);
          StringTableSize += (uint)(IndexList[sc].Length + 1);
          tbTAG = null;
        }
        //Creating the temp files for map building
        FileInfo VertsFile_info = new FileInfo(Application.StartupPath + "TempVerts.Map");
        FileStream VertsFile;
        VertsFile = VertsFile_info.Open(FileMode.Create,FileAccess.ReadWrite);

        FileInfo IndicesFile_info = new FileInfo(Application.StartupPath + "TempIndices.Map");
        FileStream IndicesFile;
        IndicesFile = IndicesFile_info.Open(FileMode.Create,FileAccess.ReadWrite);

        FileInfo MetaFile_info = new FileInfo(Application.StartupPath + "TempMeta.map");
        FileStream MetaFile;
        MetaFile = MetaFile_info.Open(FileMode.Create,FileAccess.ReadWrite);

        FileInfo BSPFile_info = new FileInfo(Application.StartupPath + "TempBSP.map");
        FileStream BSPFile;
        BSPFile = BSPFile_info.Open(FileMode.Create,FileAccess.ReadWrite);

        //lbCurTag.Text = "Writeing Index Table....";
        Application.DoEvents();

        //writing the index table the temp files
        for(int sc = 0;sc < NewMap.PCIndexHeader.IndexItems.Length;sc++)
        {
          NewMap.PCIndexHeader.IndexItems[sc].Write(ref MetaFile);
        }
        //Rewriting index items to temp file to fix string offsets.
        //lbCurTag.Text = "Rewriteing Index Table....";
        Application.DoEvents();

        for(int sc = 0;sc < NewMap.PCIndexHeader.IndexItems.Length;sc++)
        {
          NewMap.PCIndexHeader.IndexItems[sc].StringOffset = (uint)MetaFile.Position + NewMap.PCIndexHeader.IndexMagic;
          MetaFile.Write(NewMap.PCIndexHeader.IndexItems[sc].String,0,NewMap.PCIndexHeader.IndexItems[sc].String.Length);
        }
        //save MetaFile position to return for meta writing.
        long TmpPosSave = MetaFile.Position;
        //lbCurTag.Text = "Writing Index Table with Srtring Offsets....";
        Application.DoEvents();

        for(int sc = 0;sc < NewMap.PCIndexHeader.IndexItems.Length;sc++)
        {
          MetaFile.Seek(NewMap.PCIndexHeader.IndexItems[sc].Position,SeekOrigin.Begin);
          NewMap.PCIndexHeader.IndexItems[sc].Write(ref MetaFile);
        }
        //restore position for meta writing
        MetaFile.Seek(TmpPosSave,SeekOrigin.Begin);
				
        //Add info to the Meta info struct 
        PCMeta.StructInfo Info = new PCMeta.StructInfo();
        Info.MapMagic = NewMap.PCIndexHeader.IndexMagic;
        Info.TagMagic = NewMap.PCIndexHeader.IndexMagic;
        Info.StructurePath = StructFolder;
        Info.TagsPath = BasFolder;
        Info.Items = NewMap.PCIndexHeader.IndexItems;
        //Starts processing meta and writing it to the meta file
        //lbCurTag.Text = "Adding Tags to map....";
        Application.DoEvents();

        for(int sc = 0;sc < NewMap.PCIndexHeader.IndexItems.Length;sc++)
        {
          if (NewMap.PCIndexHeader.IndexItems[sc].TagClass1 != 0x70736273) //   70736273)
          {
            PCMeta Meta = new PCMeta();
            //FileInfo Tag_info = new FileInfo(BasFolder + IndexList[sc]);
            //FileStream Tag;
            //Tag = Tag_info.Open(FileMode.Open,FileAccess.Read);
            //byte[] head = new byte[0x40];
            //Tag.Read(head,0,head.Length);
            TagFileName tfnTAG = new TagFileName(IndexList[sc],MapfileVersion.HALOPC);
            TagBase tbTAG = new TagBase();
            tbTAG.LoadTagBuffer(tfnTAG);

            //string Class = Meta.GetTagClass(head,0x24);
            //MAG sr = new MAG(StructFolder + Class.Trim() + ".mag");
            //sr.Seek(1);
            MTSFReader mr = new MTSFReader();
            mr.MTSFRead("Core.Compiler.HALO_PC_SET.MTSF");

            TSFReader STSF = new TSFReader();

            //SwapInt(TagClasses,0);
            STSF.TSF(ref mr, tbTAG.Header.TagClass0);

						

            HaloPCMAP.FixIntPosition(ref MetaFile);
            Info.CurrentOffset = (uint)MetaFile.Position;
            NewMap.PCIndexHeader.IndexItems[sc].MetaOffset = (uint)(MetaFile.Position + NewMap.PCIndexHeader.IndexMagic);

            Meta.DoProcessMeta(ref STSF,tbTAG.Stream,ref MetaFile,ref VertsFile,ref IndicesFile,ref BSPFile,1,IndexList,Info);

            tbTAG = null;
            //Tag.Flush();
            //Tag.Close();
          }
        }
        //Create a new map header
        //lbCurTag.Text = "Creating New Map....";
        Application.DoEvents();

        NewMap.PCHeader.Create();
        string[] tmpName = scenario_tag.RelativePath.Split(new char[]{'\\'},256);
        tmpName = tmpName[tmpName.Length - 1].Split(new char[]{'.'},256);
        NewMap.PCHeader.MapName = tmpName[0];//otf.FileName.Split(new char[]{'/'},256   //"Cool";
        //NewMap.PCHeader.Write(ref HeadFile);
        //Rewrite indextable of meta offsets
        //lbCurTag.Text = "Fixing Meta Offsets in Index Table....";
        Application.DoEvents();

        TmpPosSave = MetaFile.Position;
        for(int sc = 0;sc < NewMap.PCIndexHeader.IndexItems.Length;sc++)
        {
          MetaFile.Seek(NewMap.PCIndexHeader.IndexItems[sc].Position,SeekOrigin.Begin);
          NewMap.PCIndexHeader.IndexItems[sc].Write(ref MetaFile);
        }
        MetaFile.Seek(TmpPosSave,SeekOrigin.Begin);
				
        //Create the new map file to begin construction of the new map.
        FileInfo Map_info = new FileInfo(Application.StartupPath + NewMap.PCHeader.MapName +".Map");
        FileStream Map;
        Map = Map_info.Open(FileMode.Create,FileAccess.ReadWrite);

        //lbCurTag.Text = "Copying Data to new map file....";
        Application.DoEvents();

        //Write the header to the new map file
        NewMap.PCHeader.Write(ref Map);
        //Flush the tmp BSPFile to get FileSize.
        BSPFile.Flush();
        uint BSPFileSize = (uint)BSPFile.Length;
        //Seek to the begining of the tmp bsp file
        BSPFile.Seek(0,SeekOrigin.Begin);
        //Copy the Tmp bsp file to the new map file
        for(int bc = 0;bc < BSPFileSize;bc+=4)
        {
          byte[] copy = new byte[4];
          BSPFile.Read(copy,0,copy.Length);
          Map.Write(copy,0,copy.Length);
        }
        //Flush the Temp verts file to get File size;
        VertsFile.Flush();
        uint VertsFileSize = (uint)VertsFile.Length;
        //Put Current Map Position in the indexheader, this sets the position of the verts in the map.
        NewMap.PCIndexHeader.VertsOffset = (uint)Map.Position;
        //seek to begining of the Verts file for coping to the new map file
        VertsFile.Seek(0,SeekOrigin.Begin);
        //Copy the verts to the map file
        for(int bc = 0;bc < VertsFileSize;bc+=4)
        {
          byte[] copy = new byte[4];
          VertsFile.Read(copy,0,copy.Length);
          Map.Write(copy,0,copy.Length);
        }
        //Flush Indeices file to get size
        IndicesFile.Flush();
        uint IndicesFileSize = (uint)IndicesFile.Length;
        //Put the Scnr index ID in the IndexHeader, you can move the scnr any were as long as you set this to the currect ID.
        NewMap.PCIndexHeader.ScnrID = (uint)((NewMap.PCIndexHeader.IndexItems[0].IndexID2 << 16) + NewMap.PCIndexHeader.IndexItems[0].IndexID1);
        //ajust the IndicesOffset with verts offset and map position
        NewMap.PCIndexHeader.IndicesOffset = (uint)Map.Position - NewMap.PCIndexHeader.VertsOffset;
        NewMap.PCIndexHeader.ModelAreaSize = VertsFileSize + IndicesFileSize;
        //Seek to begining of IndicesFile for coping
        IndicesFile.Seek(0,SeekOrigin.Begin);
        //Copy Indices to new map file
        for(int bc = 0;bc < IndicesFileSize;bc+=4)
        {
          byte[] copy = new byte[4];
          IndicesFile.Read(copy,0,copy.Length);
          Map.Write(copy,0,copy.Length);
        }
        //put the Current map position in the header so we know where the index header will be
        NewMap.PCHeader.IndexOffset = (uint)Map.Position;
        //Now we write the Index header to the map.
        NewMap.PCIndexHeader.Write(ref Map);
				
        //Flush the temp meta file so we can get the size.
        MetaFile.Flush();
        uint MetaFileSize = (uint)MetaFile.Length;
        //seek to the begining of the temp meta file for coping to the new map file
        MetaFile.Seek(0,SeekOrigin.Begin);
        //Copy the meta file to the new map file.
        for(int bc = 0;bc < MetaFileSize;bc+=4)
        {
          byte[] copy = new byte[4];
          MetaFile.Read(copy,0,copy.Length);
          Map.Write(copy,0,copy.Length);
        }
        //Now we fix the map version map size and rewrite it to the map.
        Map.Flush();
        NewMap.PCHeader.MapVersion = 0x07;
        NewMap.PCHeader.MapSize = (uint)Map.Length;
        NewMap.PCHeader.MetaSize = MetaFileSize + 0x28;
        Map.Seek(NewMap.PCHeader.Position,SeekOrigin.Begin);
        NewMap.PCHeader.Write(ref Map);
			

        //Now close the files and delete the temp files.
        VertsFile.Close();
        IndicesFile.Close();
        MetaFile.Flush();
        MetaFile.Close();
        BSPFile.Close();
        Map.Close();
        //FileStream TagFile;
        //TagFile = Tag
      }
    }
  }
	public class PCMeta
	{
		string TabReplace = "";
		byte TabByte = 0x09;
		ushort Name;
		uint Size;
		public struct StructInfo
		{
			public uint MapMagic;
			public uint TagMagic;
			public uint MapVersion;
			public uint VerticesOffset;
			public uint IndicesOffset;
			public HaloPCMAP.sPCIndexItem[] Items;
			public string StructurePath;
			public string TagsPath;
			public uint CurrentOffset;
			//public Map.IndexItemExpanded[] IndexItems;
			public FileStream BitmapsFile;
			public FileStream SoundsFile;
		}
		byte[] TagBuff = new byte[0];

		public void DoProcessMeta(ref TSFReader sr, MemoryStream FI,ref FileStream FO,ref FileStream VertsFile,ref FileStream IndFile,ref FileStream BSPFile,uint Count,string[] IndexStrings,StructInfo Info)
		{
			TabReplace += (char)TabByte;
			Name = sr.GetUName(); //CMD[2];
			Size = sr.GetSOC();

			TagBuff = new byte[Size * Count];
			FI.Read(TagBuff,0,TagBuff.Length);
			//FixIntPosition(ref FO);
			long StructPosition = FO.Position;
			FO.Write(TagBuff,0,TagBuff.Length);
			uint StartOfStruct = sr.Position;
			uint sc = 0;

			bool ExitStruct = false;
			do
			{
				do
				{
					//InLine = sr.Read();
					//CMD = InLine.Replace(TabReplace,"").ToLower().Trim().Split(new char[]{' '},256);
					sr.Read();
					switch (sr.GetCMD())
					{
						case TSFReader.TSFStruct: // "struct":
							uint ChildCount = GetUInt(TagBuff,(uint)(sr.GetOIP() + (sc * Size)));
							if (ChildCount != 0)
							{
								PCMeta Child = new PCMeta();
								PutUInt(ref TagBuff,(uint)(sr.GetOIP() + 4 + (sc * Size)),(uint)(FO.Position + Info.TagMagic));
								Child.DoProcessMeta(ref sr,FI,ref FO,ref VertsFile,ref IndFile,ref BSPFile, ChildCount,IndexStrings,Info);
								FixIntPosition(ref FO);
							}
							else
							{
								sr.SeekAheadTo(0xA7,sr.GetUName());

								//sr.SeekAheadTo("end " + CMD[2]);
								//sr.Seek(sr.Position + 1);
							}
							break;
						case TSFReader.TSFBSPTagRef: // "bspstagref":
						case TSFReader.TSFTagRef: // "tagref":
							uint tStringSize = 0;
							GetUInt(TagBuff,(uint)(sr.GetOIP() + (sc * Size)) + 0x08,ref tStringSize);
							if (tStringSize != 0x00)
							{
								PutUInt(ref TagBuff,(uint)(sr.GetOIP() + (sc * Size)) + 0x04,0x00000000);
								PutUInt(ref TagBuff,(uint)(sr.GetOIP() + (sc * Size)) + 0x08,0x00000000);
								uint TagClass = GetUInt(TagBuff,(uint)(sr.GetOIP() + (sc * Size)));
								string TagClassString = GetTagClassRev(BitConverter.GetBytes(TagClass),0);
								byte[] tmpStr = new byte[tStringSize + 1];

								FI.Read(tmpStr,0,tmpStr.Length);
								string tag = ReadString(tmpStr,0);
								int t = 0;
								//string[] test;
								bool con = false;
								do
								{
									string[] test = Info.Items[t].NameString.Split(new char[]{'.'},256);//IndexStrings[t].Split(new char[]{'.'},256);
									string testClass = GetTagClassRev(BitConverter.GetBytes(Info.Items[t].TagClass1),0);
									if(testClass == TagClassString && test[0] == tag)
									{
										con = true;
									}
									else
									{
										t+=1;
									}
								}while (con == false);// testClass != TagClass);
								PutUInt(ref TagBuff,(uint)(sr.GetOIP() + (sc * Size)) + 0x04,Info.Items[t].StringOffset);
								PutUShort(ref TagBuff,(uint)(sr.GetOIP() + (sc * Size)) + 0x0C,(ushort)Info.Items[t].IndexID1);
								PutUShort(ref TagBuff,(uint)(sr.GetOIP() + (sc * Size)) + 0x0E,(ushort)Info.Items[t].IndexID2);
								if (Name == 0x0468)
								{
									uint SaveTagMagic = Info.TagMagic;
									byte[] BspHeader = new byte[0x18];
									PutUInt(ref TagBuff,0x00 + (sc * Size),(uint)(0x800 + BSPFile.Position));
									uint CurrentPosition = (uint)BSPFile.Position;
									Info.CurrentOffset = (uint)BSPFile.Position;

									MTSFReader mr = new MTSFReader();
									mr.MTSFRead("Core.Compiler.HALO_PC_SET.MTSF");

									TSFReader tmpSTSF = new TSFReader();

									//SwapInt(TagClasses,0);
									tmpSTSF.TSF(ref mr,0x70736273); //   73627370);



									//MAG tmpsr = new MAG(Info.StructurePath + "sbsp.mag");
									
									//FileInfo BspMetaFile_info = new FileInfo(Info.TagsPath + tag + "." + tmpSTSF.Name);
									//FileStream BspMetaFile;
									//BspMetaFile = BspMetaFile_info.Open(FileMode.Open,FileAccess.Read);
									//BspMetaFile.Seek(0x40,SeekOrigin.Begin);
									TagFileName tfnTAG = new TagFileName(tag + "." + tmpSTSF.Name,MapfileVersion.HALOPC);
									TagBase tbTAG = new TagBase();
									tbTAG.LoadTagBuffer(tfnTAG);

									string[] gh;
									int tt = -1;
									do
									{
										tt+=1;
									}while((tag + "." + tmpSTSF.Name) != Info.Items[tt].NameString);
									uint BSPFileSize = (0x800 - ((uint)(Info.Items[tt].MetaSize) % 0x800));
									BSPFileSize += (uint)(Info.Items[tt].MetaSize);
									uint tmpMagic = (uint)(0x41B40000 - BSPFileSize);
									Info.TagMagic = tmpMagic;
									PutUInt(ref TagBuff,0x08 + (sc * Size),tmpMagic);
									PutUInt(ref BspHeader,0x00 + (sc * Size),0x18 + tmpMagic);
									PutUInt(ref BspHeader,0x14 + (sc * Size),0x73627370);
									BSPFile.Write(BspHeader,0,BspHeader.Length);
									PCMeta BspMeta = new PCMeta();
									
									BspMeta.DoProcessMeta(ref tmpSTSF,tbTAG.Stream,ref BSPFile,ref VertsFile,ref IndFile,ref BSPFile,1,IndexStrings,Info);
									FixIntPosition2K(ref BSPFile);
									PutUInt(ref TagBuff,0x04 + (sc * Size),(uint)(CurrentPosition + BSPFile.Position));
									//BspMetaFile.Close();
									tbTAG = null;
									Info.TagMagic = SaveTagMagic;
									int a = 0;
								}
							}
							break;
						case TSFReader.TSFInternalRaw: // "internalraw":
							uint RawSize = GetUInt(TagBuff,(uint)(sr.GetOIP() + (sc * Size)));       //Compile Mag.Map
							if (RawSize == 0)
								break;
							uint RawOffset = GetUInt(TagBuff,(uint)(sr.GetOIP() + 0x0c + (sc * Size)));     //Tag Text
							PutUInt(ref TagBuff,(uint)(sr.GetOIP()) + 0x0c + (sc * Size),((uint)FO.Position + Info.TagMagic) );
							byte[] tmp = new byte[RawSize];
							FI.Read(tmp,0,tmp.Length);
							FO.Write(tmp,0,tmp.Length);
							FixIntPosition(ref FO);
							tmp = null;
							break;
						case TSFReader.TSFBitmapRaw: // "bitmapraw":
							uint TiffSize=0; GetUInt(TagBuff,0x1C,ref TiffSize);
							byte[] TiffBuffer = new byte[TiffSize];
							FI.Read(TiffBuffer,0,TiffBuffer.Length);
							uint BitmapSize = GetUInt(TagBuff,0x30);
							TiffBuffer = new byte[BitmapSize];
							FI.Read(TiffBuffer,0,TiffBuffer.Length);
							TiffBuffer = null;
							break;
						case TSFReader.TSFModelData: // "modeldata":
							long SaveMapFilePos = (uint)FI.Position;
							uint TrueVerticesSize = (GetUInt(TagBuff,(uint)(88 + (sc * Size)))) * 68; 
							uint TrueIndicesSize =(((GetUInt(TagBuff,(uint)(72 + (sc * Size)))) / 3) + 1 ) * 6; 
							uint NewIndicesDataSize = (GetUInt(TagBuff,(uint)(72 + (sc * Size))) / 3) + 1;
							byte[] RawModelBuffer = new byte[TrueVerticesSize];
							FI.Read(RawModelBuffer,0,RawModelBuffer.Length);
							PutUInt(ref TagBuff,(uint)(0x64 + (sc * Size)),(uint)VertsFile.Position);
							PutUInt(ref TagBuff,(uint)(0x4C + (sc * Size)),(uint)IndFile.Position); 
							PutUInt(ref TagBuff,(uint)(0x50 + (sc * Size)),(uint)IndFile.Position);
							VertsFile.Write(RawModelBuffer,0,RawModelBuffer.Length);
							RawModelBuffer = new byte[TrueIndicesSize];
							FI.Read(RawModelBuffer,0,RawModelBuffer.Length);
							IndFile.Write(RawModelBuffer,0,RawModelBuffer.Length);
							byte[] TestBuffer = new byte[6];
							FI.Read(TestBuffer,0,6);
							if (TestBuffer[4] == 0xff)
							{
								IndFile.Write(TestBuffer,0,TestBuffer.Length);
							}
							else
							{
								FI.Seek(FI.Position - 6,SeekOrigin.Begin);
							}
							FixIntPosition(ref IndFile);
							break;
						case TSFReader.TSFBSPModel: // "bspmodel":
							uint UnCompressedVerts = sr.GetOIP();
							uint CompressedVerts = sr.GetSOC();
							uint UnCompressedVertsOffset = sr.GetOIP();
							//uint VertsOffset = GetUInt(TagBuff,CompressedVerts);
							uint TrueVertCount = GetUInt(TagBuff,180 + (sc * Size));
							uint TrueLightMapDataCount = GetUInt(TagBuff,200 + (sc * Size));
							uint UnCompressedLightMapData = (TrueLightMapDataCount * 20);
							uint CompressedLightMapData = (TrueLightMapDataCount * 8);
							UnCompressedVerts = (TrueVertCount * 56);
							CompressedVerts = (TrueVertCount * 32);

							byte[] BspVertsUnCompressed = new byte[UnCompressedVerts];
							FI.Read(BspVertsUnCompressed,0,BspVertsUnCompressed.Length);
							PutUInt(ref TagBuff,0xe4 + (sc * Size),(uint)(FO.Position + Info.TagMagic));
							FO.Write(BspVertsUnCompressed,0,BspVertsUnCompressed.Length);
							//byte[] BspVertsCompressed = new byte[CompressedVerts];
							byte[] BspLightMapDataUnCompressed = new byte[UnCompressedLightMapData];
							FI.Read(BspLightMapDataUnCompressed,0,BspLightMapDataUnCompressed.Length);
							FO.Write(BspLightMapDataUnCompressed,0,BspLightMapDataUnCompressed.Length);
							//byte[] BspLightMapDataCompressed = new byte[CompressedLightMapData];
						
              //viper told me to skip past compressed data as a quick fix to 
              // the crash problem - Grenadiac
              FI.Seek(CompressedVerts, SeekOrigin.Current); //gren added this
              FI.Seek(CompressedLightMapData, SeekOrigin.Current); //gren added this
              break;

						case TSFReader.TSFResource: // "resources":
							ushort rStringSize = GetUShort(TagBuff,0x06 + (sc * Size));
							if (rStringSize != 0x00)
							{
								uint TagClassFlag = GetUShort(TagBuff,0x00 + (sc * Size));
								string TagClassString ="";
								switch(TagClassFlag)
								{
									case 0x00:
										TagClassString = "bitm";
										break;
									case 0x01:
										TagClassString = "snd!";
										break;
								}
								
								//MAG tmpMR = new MAG(Info.StructurePath + TagClassString.Trim() + ".mag");
								byte[] tmpStr = new byte[rStringSize + 1];



								FI.Read(tmpStr,0,tmpStr.Length);
								string tag = ReadString(tmpStr,0);
								//tmpMR.Seek(0);
								int t = 0;
								//string[] test;
								bool con = false;
								do
								{
									string[] test = Info.Items[t].GetString().Split(new char[]{'.'},256);//IndexStrings[t].Split(new char[]{'.'},256);
									string testClass = GetTagClassRev(BitConverter.GetBytes(Info.Items[t].TagClass1),0);
									if(testClass == TagClassString && test[0] == tag)
									{
										con = true;
									}
									else
									{
										t+=1;
									}
								}while (con == false);
								PutUShort(ref TagBuff,(uint)(0x04 + (sc * Size)),(ushort)Info.Items[t].IndexID1);
								PutUShort(ref TagBuff,(uint)(0x06 + (sc * Size)),(ushort)Info.Items[t].IndexID2);
							}
							break;
						case TSFReader.TSFSoundData: // "sounddata":
							uint sRawSize = GetUInt(TagBuff,(uint)(sr.GetOIP() + (sc * Size)));
							uint sRawOffset = GetUInt(TagBuff,(uint)(sr.GetUName() + (sc * Size)));
							byte[] stmp = new byte[sRawSize];
							FI.Read(stmp,0,stmp.Length);
							stmp = null;
							break;
						case TSFReader.TSFEnd: // "end":
							if(sr.GetOIP() == Name)
							{
								ExitStruct = true;
							}
							else
							{
								ExitStruct = false;
							}
							break;
					}
				}while(ExitStruct == false);
				if (Count != 0)
				{
					sc +=1;
					if (sc != Count)
					{
						sr.Seek(StartOfStruct);
						ExitStruct = false;
					}
				}
			}while(sc != Count);
			long tmpPosSave = FO.Position;
			FO.Seek(StructPosition,SeekOrigin.Begin);
			FO.Write(TagBuff,0,TagBuff.Length);
			FO.Seek(tmpPosSave,SeekOrigin.Begin);
		}
		public uint Val(string Hex)
		{
			string[] Test;
			Test = Hex.Split(new char[]{'x'},255);
			switch (Test.Length)
			{
				case 1:
					return Convert.ToUInt32(Hex);
				case 2:
					return Convert.ToUInt32(Hex,16);
				case 3:
					return 0;
			}
			return 0;
		}
		public static void GetUInt(byte[] data,uint offset,ref uint Value)
		{
			Value = BitConverter.ToUInt32(data,(int)offset);
		}
		public static uint GetUInt(byte[] data,uint offset)
		{
			if (data.Length - 4 > offset )
			{
				return BitConverter.ToUInt32(data,(int)offset);
			}
			return 0;
		}
		public static void PutUInt(ref byte[] data,uint offset,uint Value)
		{
			byte[] tmp = BitConverter.GetBytes(Value);
			tmp.CopyTo(data,(long)offset);
		}
		public static void GetUShort(byte[] data,uint offset,ref ushort Value)
		{
			Value = BitConverter.ToUInt16(data,(int)offset);
		}
		public static ushort GetUShort(byte[] data,uint offset)
		{
			return BitConverter.ToUInt16(data,(int)offset);
		}
		public static void PutUShort(ref byte[] data,uint offset,ushort Value)
		{
			byte[] tmp = BitConverter.GetBytes(Value);
			tmp.CopyTo(data,(long)offset);
		}

		public string GetTagClass(byte[] Array,uint Offset)
		{
			string NewString = "";
			for (uint count = 0; count < 4; count +=1)
			{
				NewString += (char)Array[count + Offset];
			}
			return NewString;
		}
		public string GetTagClassRev(byte[] Array,uint Offset)
		{
			string NewString = "";
			for (uint count = 4; count > 0; count -=1)
			{
				NewString += (char)Array[(count-1) + Offset];
			}
			return NewString;
		}
		private string ReadString(byte[] Array,uint Offset)
		{
			string OutString = "";
			uint count = Offset;
			do 
			{
				OutString += (char)Array[count];
				count += 1;
			} while (Array[count] != 00);
			return OutString;
		}
		public static void FixIntPosition(ref FileStream FX)
		{
			if ((FX.Position % 0x04) != 0)
			{
				uint Size = (0x04 - ((uint)FX.Position % 0x04));
				byte[] tmp = new byte[Size];
				for(int i = 0; i<tmp.Length;i++)
				{
					tmp[i] = 0x2D;
				}
				FX.Write(tmp,0,tmp.Length);
			}
		}
		public static void FixIntPosition2K(ref FileStream FX)
		{
			if ((FX.Position % 0x800) != 0)
			{
				uint Size = (0x800 - ((uint)FX.Position % 0x800));
				byte[] tmp = new byte[Size];
				for(int i = 0; i<tmp.Length;i++)
				{
					tmp[i] = 0x2D;
				}
				FX.Write(tmp,0,tmp.Length);
			}
		}
	}
}
