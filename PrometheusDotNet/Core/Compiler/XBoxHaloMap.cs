using System;
using System.IO;
using Prometheus.Core.Tags;
using System.Windows.Forms;
using Prometheus.Core.Project;
using System.Diagnostics;
using System.Collections;

namespace Prometheus.Core.Compiler
{
	/// <summary>
	/// Summary description for XBoxHaloMap.
	/// </summary>
	public class XBoxHaloMap
	{
		#region XBoxHaloMapStructures
		public struct sXBoxHalo
		{
			public sXBoxHeader XBoxHeader;
			public byte[] BspArea;
			public byte[] SoundsArea;
			public byte[] BitmapsArea;
			public byte[] VerticeArea;
			public byte[] IndeicesArea;
			public sXBoxIndexHeader XBoxIndexHeader;
			public byte[] MetaArea;
			public uint IndirectVerts;
			public uint IndirectIndices;
		}
		public struct sXBoxHeader
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
				string test = "01.10.12.2276";
				CompUtil.StringToByteArray(ref BuildDate,test);
			}
			public void Read(ref FileStream FI)
			{
				data = new byte[0x800];
				FI.Read(data,0,data.Length);
				CompUtil.GetUInt(data,0x00,ref Head);
				CompUtil.GetUInt(data,0x04,ref MapVersion);
				CompUtil.GetUInt(data,0x08,ref MapSize);
				CompUtil.GetUInt(data,0x10,ref IndexOffset);
			}
			public void Write(ref FileStream FO)
			{
				Position = FO.Position;
				CompUtil.PutUInt(ref data,0x00,0x68656164);
				CompUtil.PutUInt(ref data,0x04,MapVersion);
				CompUtil.PutUInt(ref data,0x08,MapSize);
				CompUtil.PutUInt(ref data,0x10,IndexOffset);
				CompUtil.PutUInt(ref data,0x14,MetaSize);
				CompUtil.PutUInt(ref data,0x7FC,0x666F6F74);
				Name = new byte[0x20];
				CompUtil.StringToByteArray(ref Name,MapName);
				Name.CopyTo(data,0x20);
				BuildDate.CopyTo(data,0x40);
				FO.Write(data,0,data.Length);
			}
		}
		public struct sXBoxIndexHeader
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
			public sXBoxIndexItem[] IndexItems;
			public void Create(uint IndexCount)
			{
				data = new byte[0x24];
				IndexItems = new sXBoxIndexItem[IndexCount];
				TagCount = IndexCount;
				IndexMagic = 0x803a6024;//pcindexmagic = 0x40440028;
			}
			public void Read()
			{
			}
			public void Write(ref FileStream FO)
			{
				Position = FO.Position;
				CompUtil.PutUInt(ref data,0x00,IndexMagic);
				CompUtil.PutUInt(ref data,0x04,ScnrID);
				CompUtil.PutUInt(ref data,0x08,0xfafafafa);
				CompUtil.PutUInt(ref data,0x0C,TagCount);
				CompUtil.PutUInt(ref data,0x10,ModelCount1);
				CompUtil.PutUInt(ref data,0x14,VertsOffset);
				CompUtil.PutUInt(ref data,0x18,ModelCount2);
				CompUtil.PutUInt(ref data,0x1C,IndicesOffset);
				CompUtil.PutUInt(ref data,0x20,0x74616773);
				FO.Write(data,0,data.Length);
			}
		}
		public struct sXBoxIndexItem
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
				CompUtil.PutUInt(ref data,0x00,TagClass1);
				CompUtil.SwapInt(ref data,0x00);
				CompUtil.PutUInt(ref data,0x04,TagClass2);
				CompUtil.SwapInt(ref data,0x04);
				CompUtil.PutUInt(ref data,0x08,TagClass3);
				CompUtil.SwapInt(ref data,0x08);
				CompUtil.PutUShort(ref data,0x0C,IndexID1);
				CompUtil.PutUShort(ref data,0x0E,IndexID2);
				CompUtil.PutUInt(ref data,0x10,StringOffset);
				CompUtil.PutUInt(ref data,0x14,MetaOffset);
				CompUtil.PutUInt(ref data,0x18,MetaDataID);
				CompUtil.PutUInt(ref data,0x1C,0x00000000);
				FO.Write(data,0,data.Length);
			}
			public string GetString()
			{
				return CompUtil.ReadString(String,0);
			}
		}
		#endregion
		public static void Build()
		{
			TagFileName scenario_tag = ProjectManager.ScenarioTagFileName;//new TagFileName(@"levels\test\beavercreek\beavercreek.scenario",MapfileVersion.XHALO1);
			IndexBuilder IndexBuild = new IndexBuilder();
			string BasFolder = Application.StartupPath + @"\Extracted Tags\";
			string StructFolder = Application.StartupPath + @"\Tag Structures\PcHalo\";
			if (true)
			{
				//Create the index table
				Application.DoEvents();
				string[] IndexList = IndexBuild.BuildIndex(scenario_tag.RelativePath, BasFolder, StructFolder,MapfileVersion.XHALO1);
				XBoxHaloMap.sXBoxHalo NewMap = new XBoxHaloMap.sXBoxHalo();
				NewMap.XBoxIndexHeader.Create((uint)IndexList.Length);
				NewMap.XBoxIndexHeader.IndexMagic = 0x803a6024; //0x40440028;
				uint StringTableSize = 0;
				//Fixing index items
				ushort gt = 0xE174;
				Application.DoEvents();

				for(int sc = 0;sc < IndexList.Length;sc++)
				{
					TagFileName tfnTAG = new TagFileName(IndexList[sc],MapfileVersion.XHALO1);
					TagBase tbTAG = new TagBase();
					tbTAG.LoadTagBuffer(tfnTAG);

					NewMap.XBoxIndexHeader.IndexItems[sc].Create();
					NewMap.XBoxIndexHeader.IndexItems[sc].TagClass1 = CompUtil.SwapUInt(tbTAG.Header.TagClass0);
					NewMap.XBoxIndexHeader.IndexItems[sc].TagClass2 = CompUtil.SwapUInt(tbTAG.Header.TagClass1);
					NewMap.XBoxIndexHeader.IndexItems[sc].TagClass3 = CompUtil.SwapUInt(tbTAG.Header.TagClass2);
					NewMap.XBoxIndexHeader.IndexItems[sc].IndexID1 = (ushort)sc;
					NewMap.XBoxIndexHeader.IndexItems[sc].IndexID2 = gt;
					NewMap.XBoxIndexHeader.IndexItems[sc].NameString = IndexList[sc];
					if (tbTAG.Header.TagClass0 == 0x70736273 || tbTAG.Header.TagClass0 == 0x73627370)
					{
						NewMap.XBoxIndexHeader.IndexItems[sc].MetaSize = IndexBuild.BSPSizes[0];
					}
					else
					{
						NewMap.XBoxIndexHeader.IndexItems[sc].MetaSize = 0;
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
					NewMap.XBoxIndexHeader.IndexItems[sc].String = new byte[RemoveExt[0].Length + 1];
					HaloPCMAP.StringToByteArray(ref NewMap.XBoxIndexHeader.IndexItems[sc].String,RemoveExt[0]);
					StringTableSize += (uint)(IndexList[sc].Length + 1);
					tbTAG = null;
				}
				//Creating the temp files for map building
				FileInfo MetaFile_info = new FileInfo(Application.StartupPath + "TempMeta.map");
				FileStream MetaFile;
				MetaFile = MetaFile_info.Open(FileMode.Create,FileAccess.ReadWrite);

				FileInfo BSPFile_info = new FileInfo(Application.StartupPath + "TempBSP.map");
				FileStream BSPFile;
				BSPFile = BSPFile_info.Open(FileMode.Create,FileAccess.ReadWrite);

				Application.DoEvents();

				//writing the index table the temp files
				for(int sc = 0;sc < NewMap.XBoxIndexHeader.IndexItems.Length;sc++)
				{
					NewMap.XBoxIndexHeader.IndexItems[sc].Write(ref MetaFile);
				}
				//Rewriting index items to temp file to fix string offsets.
				Application.DoEvents();

				for(int sc = 0;sc < NewMap.XBoxIndexHeader.IndexItems.Length;sc++)
				{
					NewMap.XBoxIndexHeader.IndexItems[sc].StringOffset = (uint)MetaFile.Position + NewMap.XBoxIndexHeader.IndexMagic;
					MetaFile.Write(NewMap.XBoxIndexHeader.IndexItems[sc].String,0,NewMap.XBoxIndexHeader.IndexItems[sc].String.Length);
				}
				CompUtil.FixIntPosition(ref MetaFile);
				//save MetaFile position to return for meta writing.
				long TmpPosSave = MetaFile.Position;
				Application.DoEvents();

				for(int sc = 0;sc < NewMap.XBoxIndexHeader.IndexItems.Length;sc++)
				{
					MetaFile.Seek(NewMap.XBoxIndexHeader.IndexItems[sc].Position,SeekOrigin.Begin);
					NewMap.XBoxIndexHeader.IndexItems[sc].Write(ref MetaFile);
				}
				//restore position for meta writing
				#region ModelData
				MetaFile.Seek(TmpPosSave,SeekOrigin.Begin);
				NewMap.XBoxIndexHeader.VertsOffset = (uint)MetaFile.Position + NewMap.XBoxIndexHeader.IndexMagic;
				NewMap.XBoxIndexHeader.ModelCount1 = (uint)IndexBuild.XBoxMapData.XVerts.ModelCount;
				NewMap.IndirectVerts = (uint)MetaFile.Position;
				IndexBuild.XBoxMapData.XVerts.FixOffsets((uint)MetaFile.Position,NewMap.XBoxIndexHeader.IndexMagic);
				MetaFile.Write(IndexBuild.XBoxMapData.XVerts.IndirectOffsets,0,IndexBuild.XBoxMapData.XVerts.IndirectOffsets.Length);
				MetaFile.Write(IndexBuild.XBoxMapData.XVerts.Verts,0,IndexBuild.XBoxMapData.XVerts.Verts.Length);
				NewMap.XBoxIndexHeader.ModelCount2 = (uint)IndexBuild.XBoxMapData.XIndices.ModelCount;
				NewMap.XBoxIndexHeader.IndicesOffset = (uint)MetaFile.Position + NewMap.XBoxIndexHeader.IndexMagic;
				NewMap.IndirectIndices = (uint)MetaFile.Position;
				IndexBuild.XBoxMapData.XIndices.FixOffsets((uint)MetaFile.Position,NewMap.XBoxIndexHeader.IndexMagic);
				MetaFile.Write(IndexBuild.XBoxMapData.XIndices.IndirectOffsets,0,IndexBuild.XBoxMapData.XIndices.IndirectOffsets.Length);
				MetaFile.Write(IndexBuild.XBoxMapData.XIndices.Verts,0,IndexBuild.XBoxMapData.XIndices.Verts.Length);
				#endregion
				//Add info to the Meta info struct 
				XBoxMeta.StructInfo Info = new XBoxMeta.StructInfo();
				Info.MapMagic = NewMap.XBoxIndexHeader.IndexMagic;
				Info.TagMagic = NewMap.XBoxIndexHeader.IndexMagic;
				Info.StructurePath = StructFolder;
				Info.TagsPath = BasFolder;
				Info.Items = NewMap.XBoxIndexHeader.IndexItems;
				//Starts processing meta and writing it to the meta file
				Application.DoEvents();

				for(int sc = 0;sc < NewMap.XBoxIndexHeader.IndexItems.Length;sc++)
				{
					if (NewMap.XBoxIndexHeader.IndexItems[sc].TagClass1 != 0x70736273) //   70736273)
					{
						XBoxMeta Meta = new XBoxMeta();
						Trace.WriteLine("Meta Processor: " + IndexList[sc]);
						TagFileName tfnTAG = new TagFileName(IndexList[sc],MapfileVersion.XHALO1);
						TagBase tbTAG = new TagBase();
						tbTAG.LoadTagBuffer(tfnTAG);

						MTSFReader mr = new MTSFReader();
						mr.MTSFRead("Core.Compiler.HALO_PC_SET.MTSF");

						TSFReader STSF = new TSFReader();

						STSF.TSF(ref mr, tbTAG.Header.TagClass0);

						HaloPCMAP.FixIntPosition(ref MetaFile);
						Info.CurrentOffset = (uint)MetaFile.Position;
						NewMap.XBoxIndexHeader.IndexItems[sc].MetaOffset = (uint)(MetaFile.Position + NewMap.XBoxIndexHeader.IndexMagic);

						Meta.DoProcessMeta(ref STSF,tbTAG.Stream,ref MetaFile,ref BSPFile,1,IndexList,Info,ref NewMap,ref IndexBuild);

						tbTAG = null;
					}
				}
				//Create a new map header
				Application.DoEvents();

				NewMap.XBoxHeader.Create();
				string[] tmpName = scenario_tag.RelativePath.Split(new char[]{'\\'},256);
				tmpName = tmpName[tmpName.Length - 1].Split(new char[]{'.'},256);
				NewMap.XBoxHeader.MapName = tmpName[0];//otf.FileName.Split(new char[]{'/'},256   //"Cool";
				//Rewrite indextable of meta offsets
				Application.DoEvents();

				TmpPosSave = MetaFile.Position;
				for(int sc = 0;sc < NewMap.XBoxIndexHeader.IndexItems.Length;sc++)
				{
					MetaFile.Seek(NewMap.XBoxIndexHeader.IndexItems[sc].Position,SeekOrigin.Begin);
					NewMap.XBoxIndexHeader.IndexItems[sc].Write(ref MetaFile);
				}
				MetaFile.Seek(TmpPosSave,SeekOrigin.Begin);
				
				//Create the new map file to begin construction of the new map.
				FileInfo Map_info = new FileInfo(Application.StartupPath + NewMap.XBoxHeader.MapName +".Map");
				FileStream Map;
				Map = Map_info.Open(FileMode.Create,FileAccess.ReadWrite);

				Application.DoEvents();

				//Write the header to the new map file
				NewMap.XBoxHeader.Write(ref Map);
				//Flush the tmp BSPFile to get FileSize.
				CompUtil.FixIntPosition2K(ref BSPFile);
				BSPFile.Flush();
				uint BSPFileSize = (uint)BSPFile.Length;
				//Seek to the begining of the tmp bsp file
				BSPFile.Seek(0,SeekOrigin.Begin);
				//Copy the Tmp bsp file to the new map file
				for(int bc = 0;bc < BSPFileSize;bc+=0x800)
				{
					byte[] copy = new byte[0x800];
					BSPFile.Read(copy,0,copy.Length);
					Map.Write(copy,0,copy.Length);
				}
				//Put the Scnr index ID in the IndexHeader, you can move the scnr any were as long as you set this to the currect ID.
				NewMap.XBoxIndexHeader.ScnrID = (uint)((NewMap.XBoxIndexHeader.IndexItems[0].IndexID2 << 16) + NewMap.XBoxIndexHeader.IndexItems[0].IndexID1);
				//put the Current map position in the header so we know where the index header will be
				NewMap.XBoxHeader.IndexOffset = (uint)Map.Position;
				//Now we write the Index header to the map.
				NewMap.XBoxIndexHeader.Write(ref Map);
				
				//Flush the temp meta file so we can get the size.
				MetaFile.Flush();
				uint MetaFileSize = (uint)MetaFile.Length;
				//seek to the begining of the temp meta file for coping to the new map file
				MetaFile.Seek(0,SeekOrigin.Begin);
				//Copy the meta file to the new map file.
				for(int bc = 0;bc < MetaFileSize;bc+=0x800)
				{
					byte[] copy = new byte[0x800];
					MetaFile.Read(copy,0,copy.Length);
					Map.Write(copy,0,copy.Length);
				}
				//Now we fix the map version map size and rewrite it to the map.
				Map.Flush();
				NewMap.XBoxHeader.MapVersion = 0x05;
				NewMap.XBoxHeader.MapSize = (uint)Map.Length;
				NewMap.XBoxHeader.MetaSize = MetaFileSize + 0x24;
				Map.Seek(NewMap.XBoxHeader.Position,SeekOrigin.Begin);
				NewMap.XBoxHeader.Write(ref Map);

				//Now close the files and delete the temp files.
				MetaFile.Flush();
				MetaFile.Close();
				File.Delete(MetaFile.Name);
				BSPFile.Close();
				File.Delete(BSPFile.Name);
				Map.Close();
			}
		}
	}
	public class XBoxMeta
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
			public XBoxHaloMap.sXBoxIndexItem[] Items;
			public string StructurePath;
			public string TagsPath;
			public uint CurrentOffset;
			public FileStream BitmapsFile;
			public FileStream SoundsFile;
		}
		byte[] TagBuff = new byte[0];

		public void DoProcessMeta(ref TSFReader sr, MemoryStream FI,ref FileStream FO,ref FileStream BSPFile,uint Count,string[] IndexStrings,StructInfo Info,ref XBoxHaloMap.sXBoxHalo XBoxMap,ref IndexBuilder IndexBuild)
		{
			TabReplace += (char)TabByte;
			Name = sr.GetUName();
			Size = sr.GetSOC();

			TagBuff = new byte[Size * Count];
			FI.Read(TagBuff,0,TagBuff.Length);
			long StructPosition = FO.Position;
			FO.Write(TagBuff,0,TagBuff.Length);
			uint StartOfStruct = sr.Position;
			uint sc = 0;

			bool ExitStruct = false;
			do
			{
				do
				{
					sr.Read();
					switch (sr.GetCMD())
					{
						case TSFReader.TSFStruct: // "struct":
							#region Structure
							uint ChildCount = CompUtil.GetUInt(TagBuff,(uint)(sr.GetOIP() + (sc * Size)));
							if (ChildCount != 0)
							{
								XBoxMeta Child = new XBoxMeta();
								CompUtil.PutUInt(ref TagBuff,(uint)(sr.GetOIP() + 4 + (sc * Size)),(uint)(FO.Position + Info.TagMagic));
								Child.DoProcessMeta(ref sr,FI,ref FO,ref BSPFile, ChildCount,IndexStrings,Info,ref XBoxMap,ref IndexBuild);
								CompUtil.FixIntPosition(ref FO);
							}
							else
							{
								sr.SeekAheadTo(0xA7,sr.GetUName());

								//sr.SeekAheadTo("end " + CMD[2]);
								//sr.Seek(sr.Position + 1);
							}
							break;
							#endregion
						case TSFReader.TSFBSPTagRef: // "bspstagref":
						case TSFReader.TSFTagRef: // "tagref":
							#region TagRefernce
							uint tStringSize = 0;
							CompUtil.GetUInt(TagBuff,(uint)(sr.GetOIP() + (sc * Size)) + 0x08,ref tStringSize);
							if (tStringSize != 0x00)
							{
								CompUtil.PutUInt(ref TagBuff,(uint)(sr.GetOIP() + (sc * Size)) + 0x04,0x00000000);
								CompUtil.PutUInt(ref TagBuff,(uint)(sr.GetOIP() + (sc * Size)) + 0x08,0x00000000);
								uint TagClass = CompUtil.GetUInt(TagBuff,(uint)(sr.GetOIP() + (sc * Size)));
								string TagClassString = CompUtil.GetTagClassRev(BitConverter.GetBytes(TagClass),0);
								byte[] tmpStr = new byte[tStringSize + 1];

								FI.Read(tmpStr,0,tmpStr.Length);
								string tag = CompUtil.ReadString(tmpStr,0);
								int t = 0;
								//string[] test;
								bool con = false;
								do
								{
									string[] test = Info.Items[t].NameString.Split(new char[]{'.'},256);//IndexStrings[t].Split(new char[]{'.'},256);
									string testClass = CompUtil.GetTagClassRev(BitConverter.GetBytes(Info.Items[t].TagClass1),0);
									if(testClass == TagClassString && test[0] == tag)
									{
										con = true;
									}
									else
									{
										t+=1;
									}
								}while (con == false);// testClass != TagClass);
								CompUtil.SwapInt(ref TagBuff,(uint)(sr.GetOIP() + (sc * Size)) + 0x00);
								CompUtil.PutUInt(ref TagBuff,(uint)(sr.GetOIP() + (sc * Size)) + 0x04,Info.Items[t].StringOffset);
								CompUtil.PutUShort(ref TagBuff,(uint)(sr.GetOIP() + (sc * Size)) + 0x0C,(ushort)Info.Items[t].IndexID1);
								CompUtil.PutUShort(ref TagBuff,(uint)(sr.GetOIP() + (sc * Size)) + 0x0E,(ushort)Info.Items[t].IndexID2);
								if (Name == 0x0468)
								{
									uint SaveTagMagic = Info.TagMagic;
									byte[] BspHeader = new byte[0x18];
									CompUtil.PutUInt(ref TagBuff,0x00 + (sc * Size),(uint)(0x800 + BSPFile.Position));
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
									Trace.WriteLine(tag + ".sbsp" + "   Hello I'm your bsp");
									TagFileName tfnTAG = new TagFileName(tag + "." + tmpSTSF.Name,MapfileVersion.XHALO1);
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
									uint tmpMagic = (uint)(0x819a5800 - BSPFileSize);
									Info.TagMagic = tmpMagic;
									CompUtil.PutUInt(ref TagBuff,0x08 + (sc * Size),tmpMagic);
									//IndexBuild.XBoxMapData.XBSPVerts.Finish();
									uint CompoundSize = (uint)IndexBuild.XBoxMapData.XBSPVerts.VIndirectOffsets.Length + (uint)IndexBuild.XBoxMapData.XBSPVerts.LIndirectOffsets.Length + (uint)IndexBuild.XBoxMapData.XBSPVerts.ModelData.Length;
									CompUtil.PutUInt(ref BspHeader,0x00 + (sc * Size),0x18 + CompoundSize + tmpMagic);
									CompUtil.PutUInt(ref BspHeader,0x04 + (sc * Size),IndexBuild.XBoxMapData.XBSPVerts.DataCount);
									CompUtil.PutUInt(ref BspHeader,0x08 + (sc * Size),0x18 + tmpMagic);
									CompUtil.PutUInt(ref BspHeader,0x0C + (sc * Size),IndexBuild.XBoxMapData.XBSPVerts.DataCount);
									CompUtil.PutUInt(ref BspHeader,0x10 + (sc * Size),(uint)((IndexBuild.XBoxMapData.XBSPVerts.VIndirectOffsets.Length + 0x18) + tmpMagic));
									CompUtil.PutUInt(ref BspHeader,0x14 + (sc * Size),0x73627370);
									long BSPHeaderPos = BSPFile.Position;
									BSPFile.Write(BspHeader,0,BspHeader.Length);

									//IndexBuild.XBoxMapData.XBSPVerts.FixOffsets((uint)BSPFile.Position + (uint)IndexBuild.XBoxMapData.XBSPVerts.VIndirectOffsets.Length + (uint)IndexBuild.XBoxMapData.XBSPVerts.LIndirectOffsets.Length,Info.TagMagic);
									long savepos = BSPFile.Position;
									BSPFile.Write(IndexBuild.XBoxMapData.XBSPVerts.VIndirectOffsets,0,IndexBuild.XBoxMapData.XBSPVerts.VIndirectOffsets.Length);
									BSPFile.Write(IndexBuild.XBoxMapData.XBSPVerts.LIndirectOffsets,0,IndexBuild.XBoxMapData.XBSPVerts.LIndirectOffsets.Length);
									CompUtil.Fix0x20Position(ref BSPFile);
									
									IndexBuild.XBoxMapData.XBSPVerts.FixOffsets((uint)BSPFile.Position,Info.TagMagic);
									BSPFile.Seek(savepos,SeekOrigin.Begin);
									IndexBuild.XBoxMapData.XBSPVerts.VLMapOffset = (uint)BSPFile.Position + (uint)IndexBuild.XBoxMapData.XBSPVerts.VIndirectOffsets.Length;
									BSPFile.Write(IndexBuild.XBoxMapData.XBSPVerts.VIndirectOffsets,0,IndexBuild.XBoxMapData.XBSPVerts.VIndirectOffsets.Length);
									IndexBuild.XBoxMapData.XBSPVerts.ILMapOffset = (uint)BSPFile.Position + (uint)IndexBuild.XBoxMapData.XBSPVerts.LIndirectOffsets.Length;
									BSPFile.Write(IndexBuild.XBoxMapData.XBSPVerts.LIndirectOffsets,0,IndexBuild.XBoxMapData.XBSPVerts.LIndirectOffsets.Length);
									CompUtil.Fix0x20Position(ref BSPFile);

									BSPFile.Write(IndexBuild.XBoxMapData.XBSPVerts.ModelData,0,IndexBuild.XBoxMapData.XBSPVerts.ModelData.Length);
									
									long BSPSavPos = BSPFile.Position;
									CompUtil.PutUInt(ref BspHeader,0x00,(uint)(BSPFile.Position + tmpMagic));	
									BSPFile.Seek(BSPHeaderPos,SeekOrigin.Begin);
									BSPFile.Write(BspHeader,0,BspHeader.Length);
									BSPFile.Seek(BSPSavPos,SeekOrigin.Begin);

									XBoxMeta BspMeta = new XBoxMeta();
									
									BspMeta.DoProcessMeta(ref tmpSTSF,tbTAG.Stream,ref BSPFile,ref BSPFile,1,IndexStrings,Info,ref XBoxMap,ref IndexBuild);
									CompUtil.FixIntPosition2K(ref BSPFile);

									CompUtil.PutUInt(ref TagBuff,0x04 + (sc * Size),(uint)(CurrentPosition + BSPFile.Position));
									//BspMetaFile.Close();
									tbTAG = null;
									Info.TagMagic = SaveTagMagic;
									int a = 0;
								}
							}
							break;
							#endregion
						case TSFReader.TSFInternalRaw: // "internalraw":
							#region InternalRaw
							uint RawSize = CompUtil.GetUInt(TagBuff,(uint)(sr.GetOIP() + (sc * Size)));       //Compile Mag.Map
							if (RawSize == 0)
								break;
							uint RawOffset = CompUtil.GetUInt(TagBuff,(uint)(sr.GetOIP() + 0x0c + (sc * Size)));     //Tag Text
							CompUtil.PutUInt(ref TagBuff,(uint)(sr.GetOIP()) + 0x0c + (sc * Size),((uint)FO.Position + Info.TagMagic) );
							byte[] tmp = new byte[RawSize];
							FI.Read(tmp,0,tmp.Length);
							FO.Write(tmp,0,tmp.Length);
							CompUtil.FixIntPosition(ref FO);
							tmp = null;
							break;
							#endregion
						case TSFReader.TSFBitmapRaw: // "bitmapraw":
							#region BitmapRaw
							MemoryStream BMD;
							uint TiffSize=0; CompUtil.GetUInt(TagBuff,0x1C,ref TiffSize);
							byte[] TiffBuffer = new byte[TiffSize];
							FI.Read(TiffBuffer,0,TiffBuffer.Length);
							uint BitmapSize = CompUtil.GetUInt(TagBuff,0x30);
							TiffBuffer = new byte[BitmapSize];
							FI.Read(TiffBuffer,0,TiffBuffer.Length);
							BMD = new MemoryStream(TiffBuffer);

							uint BitMapStructCount= CompUtil.GetUInt(TagBuff,0x60);
							uint BitMapStructOffset= CompUtil.GetUInt(TagBuff,0x64);
							byte[] tmpBMS = new byte[BitMapStructCount * 0x30];
							long sv = FI.Position;
							FI.Seek((long)BitMapStructOffset,SeekOrigin.Begin);
							FI.Read(tmpBMS,0,tmpBMS.Length);
							
							for(int i = 0;i<BitMapStructCount;i++)
							{
								uint BitMapDataSize = CompUtil.GetUInt(tmpBMS,(uint)(0x1C + (i * 0x30)));
								uint svBmPos = (uint)BSPFile.Position + 0x800;
								CompUtil.PutUInt(ref tmpBMS,(uint)(0x18 + (i * 0x30)),(uint)svBmPos);
								byte[] tmpBuff = new byte[BitMapDataSize];
								BMD.Read(tmpBuff,0,tmpBuff.Length);
								BSPFile.Write(tmpBuff,0,tmpBuff.Length);
								CompUtil.Fix0x100Position(ref BSPFile);
								svBmPos += BitMapDataSize;
							}
							FI.Seek((long)BitMapStructOffset,SeekOrigin.Begin);
							FI.Write(tmpBMS,0,tmpBMS.Length);
							FI.Seek(sv,SeekOrigin.Begin);
							//BSPFile.Write(TiffBuffer,0,TiffBuffer.Length);
							//Fix0x100Position(ref BSPFile);
							TiffBuffer = null;
							break;
							#endregion
						case TSFReader.TSFRawXModelData: // "XBoxModelData":
							#region XModelData
							uint XTrueVerticesSize = (CompUtil.GetUInt(TagBuff,88 + (sc * Size))) * 32;
							uint XTrueIndicesSize =(((CompUtil.GetUInt(TagBuff,72 + (sc * Size))) / 3) + 1 ) * 6;
							uint XNewIndicesDataSize = (CompUtil.GetUInt(TagBuff,72 + (sc * Size)) / 3) + 1;
							uint XTrueVerticesOffset = CompUtil.GetUInt(TagBuff,100 + (sc * Size)) - Info.MapMagic;
							uint XSaveMapFilePos = (uint)FI.Position;//MapFile.Position;
							byte[] XModelHeader = new byte[16];

							//uint XTrueVerticesOffset = GetUInt(TagBuff,100 + (sc * Size)) - Info.MapMagic;
							CompUtil.PutUInt(ref TagBuff,100 + (sc * Size),XBoxMap.IndirectVerts + Info.MapMagic);
							//uint XTrueIndicesOffset = GetUInt(TagBuff, 80+ (sc * Size)) - Info.MapMagic;
							CompUtil.PutUInt(ref TagBuff,80 + (sc *Size),XBoxMap.IndirectIndices + Info.MapMagic);
							XBoxMap.IndirectVerts += 0x0c;
							XBoxMap.IndirectIndices += 0x0c;
							CompUtil.PutUInt(ref TagBuff,0x4c + (sc * Size),IndexBuild.XBoxMapData.XIndices.GetOffset());
							byte[] RawXModelBuffer = new byte[XTrueVerticesSize];
							FI.Read(RawXModelBuffer,0,(int)XTrueVerticesSize);
							byte[] XTestBuffer = new byte[6];
							RawXModelBuffer = new byte[XTrueIndicesSize];
							FI.Read(RawXModelBuffer,0,RawXModelBuffer.Length);
							FI.Read(XTestBuffer,0,6);
							if (XTestBuffer[4] == 0xff)
							{
								XTrueIndicesSize += 6;
							}
							else
							{
								FI.Seek(FI.Position - 6,SeekOrigin.Begin);
							}
							break;
							#endregion
						case TSFReader.TSFBSPModel: // "bspmodel":
							#region BspModel
							uint UnCompressedVerts = sr.GetOIP();
							uint CompressedVerts = sr.GetSOC();
							uint UnCompressedVertsOffset = sr.GetOIP();
							//uint VertsOffset = GetUInt(TagBuff,CompressedVerts);
							uint TrueVertCount = CompUtil.GetUInt(TagBuff,180 + (sc * Size));
							uint TrueLightMapDataCount = CompUtil.GetUInt(TagBuff,200 + (sc * Size));
							uint UnCompressedLightMapData = (TrueLightMapDataCount * 20);
							uint CompressedLightMapData = (TrueLightMapDataCount * 8);
							UnCompressedVerts = (TrueVertCount * 56);
							CompressedVerts = (TrueVertCount * 32);

							byte[] BspVertsUnCompressed = new byte[UnCompressedVerts];
							FI.Read(BspVertsUnCompressed,0,BspVertsUnCompressed.Length);
							//PutUInt(ref TagBuff,0xe4 + (sc * Size),(uint)(FO.Position + Info.TagMagic));
							//FO.Write(BspVertsUnCompressed,0,BspVertsUnCompressed.Length);
							//byte[] BspVertsCompressed = new byte[CompressedVerts];
							byte[] BspLightMapDataUnCompressed = new byte[UnCompressedLightMapData];
							FI.Read(BspLightMapDataUnCompressed,0,BspLightMapDataUnCompressed.Length);
							//FO.Write(BspLightMapDataUnCompressed,0,BspLightMapDataUnCompressed.Length);
							//byte[] BspLightMapDataCompressed = new byte[CompressedLightMapData];
						  uint PotLuck = IndexBuild.XBoxMapData.XBSPVerts.CurrentOffset;// + 0x800;
							uint VOOffset = 0xB4;
							uint LOOffset = 0xC8;
							IndexBuild.XBoxMapData.XBSPVerts.VLMapOffset -= 0x0c;
							CompUtil.PutUInt(ref TagBuff,VOOffset + 0x0c + (sc * Size),(uint)(IndexBuild.XBoxMapData.XBSPVerts.VLMapOffset + Info.TagMagic));
							IndexBuild.XBoxMapData.XBSPVerts.ILMapOffset -= 0x0c;
							CompUtil.PutUInt(ref TagBuff,LOOffset + 0x0c + (sc * Size),(uint)(IndexBuild.XBoxMapData.XBSPVerts.ILMapOffset + Info.TagMagic));
							PotLuck += 0x0c;
							IndexBuild.XBoxMapData.XBSPVerts.CurrentOffset = PotLuck;
						  CompUtil.PutUInt(ref TagBuff,0xF8 + (sc * Size),IndexBuild.XBoxMapData.XBSPVerts.GetVOffset());
							//viper told me to skip past compressed data as a quick fix to 
							// the crash problem - Grenadiac
							FI.Seek(CompressedVerts, SeekOrigin.Current); //gren added this
							FI.Seek(CompressedLightMapData, SeekOrigin.Current); //gren added this
							break;
							#endregion
						case TSFReader.TSFResource: // "resources":
							#region Resources
							ushort rStringSize = CompUtil.GetUShort(TagBuff,0x06 + (sc * Size));
							if (rStringSize != 0x00)
							{
								uint TagClassFlag = CompUtil.GetUShort(TagBuff,0x00 + (sc * Size));
								uint TagResouceIndex = CompUtil.GetUShort(TagBuff,0x02 + (sc * Size));
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
								string tag = CompUtil.ReadString(tmpStr,0);
								Trace.WriteLine(tag + "." + TagClassString + "      PRIndex" + Convert.ToString(TagResouceIndex,10));
								//tmpMR.Seek(0);
								int t = 0;
								//string[] test;
								bool con = false;
								do
								{
									string[] test = Info.Items[t].GetString().Split(new char[]{'.'},256);//IndexStrings[t].Split(new char[]{'.'},256);
									string testClass = CompUtil.GetTagClass(BitConverter.GetBytes(Info.Items[t].TagClass1),0);
									if(testClass == TagClassString && test[0] == tag)
									{
										con = true;
									}
									else
									{
										t+=1;
									}
								}while (con == false);
								CompUtil.PutUShort(ref TagBuff,(uint)(0x04 + (sc * Size)),(ushort)Info.Items[t].IndexID1);
								CompUtil.PutUShort(ref TagBuff,(uint)(0x06 + (sc * Size)),(ushort)Info.Items[t].IndexID2);
							}
							break;
							#endregion
						case TSFReader.TSFSoundData: // "sounddata":
							#region SoundData
							uint sRawSize = CompUtil.GetUInt(TagBuff,(uint)(sr.GetOIP() + (sc * Size)));
							uint sRawOffset = CompUtil.GetUInt(TagBuff,(uint)(sr.GetUName() + 4 + (sc * Size)));
							byte[] stmp = new byte[sRawSize];
							uint svSndPos = (uint)BSPFile.Position + 0x800;
							CompUtil.PutUInt(ref TagBuff,(uint)(sr.GetUName() + 4 + (sc * Size)),svSndPos);
							FI.Read(stmp,0,stmp.Length);
							BSPFile.Write(stmp,0,stmp.Length);
							CompUtil.Fix0x100Position(ref BSPFile);
							stmp = null;
							break;
							#endregion
						case TSFReader.TSFEnd: // "end":
							#region EndStructure
							if(sr.GetOIP() == Name)
							{
								ExitStruct = true;
							}
							else
							{
								ExitStruct = false;
							}
							break;
							#endregion
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
	}
}
