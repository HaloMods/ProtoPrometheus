using System;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Collections;
using Prometheus.Core;
using Prometheus.Core.Tags;
namespace Prometheus.Core.Compiler
{
	/// <summary>
	/// Summary description for IndexBuilder.
	/// </summary>
	public struct sXBoxMapData
	{
		public sXVerts XVerts;
		public sXVerts XIndices;
		public sTXBSPVerts XBSPVerts;
		public void Create()
		{
			XVerts.Create();
			XIndices.Create();
			XBSPVerts.Create();
		}
	}
	public class IndexBuilder
	{
		public uint[] BSPSizes;
		public sXBoxMapData XBoxMapData;
		public struct IndexItem
		{
			public uint TagClass1;
			public uint TagClass2;
			public uint TagClass3;
			public ushort Index1;
			public ushort Index2;
			public uint StringOffset;
			public uint MetaOffset;
			public uint MetaSize;
			public uint Empty1;
			public uint Empty2;
		}
		public IndexItem[] IndexItems = new IndexItem[0];
		public string[] BuildIndex(string FileName,string TagsPath,string StructFolder,MapfileVersion MapVersion)
		{
			XBoxMapData.Create();
			string[] GlobalTable = new string[1];
			string[] BSPS = new string[0];
			string Replaced = "";
			//FileName = FileName.Remove(0,TagsPath.Length);
			GlobalTable[0] = FileName;
			ProcessDependants(FileName,ref GlobalTable,ref BSPS,TagsPath,StructFolder,MapVersion,ref XBoxMapData);

			byte[] tmpMapType = new byte[0x5b0];

			TagFileName tfn = new TagFileName(FileName,MapVersion);
			TagBase tag = new TagBase();
			tag.LoadTagBuffer(tfn);
			
			tag.Stream.Read(tmpMapType,0,tmpMapType.Length);


			ushort usMType = BitConverter.ToUInt16(tmpMapType,0x3C);
			tag = null;
			//TagFile.Close();
			switch (usMType)
			{
				case 0: // SP
					break;
				case 1: // MP
					//All Versions
					FixStringArray(ref GlobalTable,@"globals\globals.globals");
					ProcessDependants(@"globals\globals.globals",ref GlobalTable,ref BSPS,TagsPath,StructFolder,MapVersion,ref XBoxMapData);
					//Xbox Only
				switch (MapVersion)
				{
					case MapfileVersion.XHALO1:
						FixStringArray(ref GlobalTable,@"ui\multiplayer_game_text.unicode_string_list");
						FixStringArray(ref GlobalTable,@"ui\shell\bitmaps\white.bitmap");
						FixStringArray(ref GlobalTable,@"ui\shell\multiplayer.ui_widget_collection");
						ProcessDependants(@"ui\shell\multiplayer.ui_widget_collection",ref GlobalTable,ref BSPS,TagsPath,StructFolder,MapVersion,ref XBoxMapData);
						break;
					case MapfileVersion.HALOPC:
						//PC Only
						FixStringArray(ref GlobalTable,@"ui\ui_tags_loaded_all_scenario_types.tag_collection");
						ProcessDependants(@"ui\ui_tags_loaded_all_scenario_types.tag_collection",ref GlobalTable,ref BSPS,TagsPath,StructFolder,MapVersion,ref XBoxMapData);
						FixStringArray(ref GlobalTable,@"ui\ui_input_device_defaults.tag_collection");
						ProcessDependants(@"ui\ui_input_device_defaults.tag_collection",ref GlobalTable,ref BSPS,TagsPath,StructFolder,MapVersion,ref XBoxMapData);
						FixStringArray(ref GlobalTable,@"ui\ui_default_profiles.tag_collection");
						ProcessDependants(@"ui\ui_default_profiles.tag_collection",ref GlobalTable,ref BSPS,TagsPath,StructFolder,MapVersion,ref XBoxMapData);
						FixStringArray(ref GlobalTable,@"ui\shell\bitmaps\background.bitmap");
						FixStringArray(ref GlobalTable,@"ui\shell\strings\loading.unicode_string_list");
						FixStringArray(ref GlobalTable,@"ui\shell\main_menu\mp_map_list.unicode_string_list");
						FixStringArray(ref GlobalTable,@"ui\shell\bitmaps\trouble_brewing.bitmap");
						FixStringArray(ref GlobalTable,@"sound\sfx\ui\cursor.sound");
						FixStringArray(ref GlobalTable,@"sound\sfx\ui\forward.sound");
						FixStringArray(ref GlobalTable,@"sound\sfx\ui\back.sound");
						FixStringArray(ref GlobalTable,@"ui\ui_tags_loaded_multiplayer_scenario_type.tag_collection");
						ProcessDependants(@"ui\ui_tags_loaded_multiplayer_scenario_type.tag_collection",ref GlobalTable,ref BSPS,TagsPath,StructFolder,MapVersion,ref XBoxMapData);
						break;
				}
					BSPSizes = new uint[BSPS.Length];
					for (int b = 0;b < BSPS.Length;b++)
					{
						FixStringArray(ref GlobalTable,BSPS[b]);
						BSPSizes[b] = ProcessDependants(BSPS[b],ref GlobalTable,ref BSPS,TagsPath,StructFolder,MapVersion,ref XBoxMapData);
					}
					break;
				case 0x02: // UI
					FixStringArray(ref GlobalTable,@"globals\globals.globals");
					ProcessDependants(@"globals\globals.globals",ref GlobalTable,ref BSPS,TagsPath,StructFolder,MapVersion,ref XBoxMapData);
					FixStringArray(ref GlobalTable,@"ui\ui_tags_loaded_all_scenario_types.tag_collection");
					ProcessDependants(@"ui\ui_tags_loaded_all_scenario_types.tag_collection",ref GlobalTable,ref BSPS,TagsPath,StructFolder,MapVersion,ref XBoxMapData);
					FixStringArray(ref GlobalTable,@"ui\ui_input_device_defaults.tag_collection");
					ProcessDependants(@"ui\ui_input_device_defaults.tag_collection",ref GlobalTable,ref BSPS,TagsPath,StructFolder,MapVersion,ref XBoxMapData);
					FixStringArray(ref GlobalTable,@"sound\sfx\ui\flag_failure.sound");
					FixStringArray(ref GlobalTable,@"ui\ui_default_profiles.tag_collection");
					ProcessDependants(@"ui\ui_default_profiles.tag_collection",ref GlobalTable,ref BSPS,TagsPath,StructFolder,MapVersion,ref XBoxMapData);
					FixStringArray(ref GlobalTable,@"ui\shell\bitmaps\background.bitmap");
					FixStringArray(ref GlobalTable,@"ui\shell\strings\loading.unicode_string_list");
					FixStringArray(ref GlobalTable,@"ui\shell\main_menu\mp_map_list.unicode_string_list");
					FixStringArray(ref GlobalTable,@"ui\shell\bitmaps\trouble_brewing.bitmap");

					FixStringArray(ref GlobalTable,@"sound\sfx\ui\cursor.sound");
					FixStringArray(ref GlobalTable,@"sound\sfx\ui\forward.sound");

  				FixStringArray(ref GlobalTable,@"sound\sfx\ui\back.sound");
					FixStringArray(ref GlobalTable,@"ui\ui_tags_loaded_mainmenu_scenario_type.tag_collection");
					ProcessDependants(@"ui\ui_tags_loaded_mainmenu_scenario_type.tag_collection",ref GlobalTable,ref BSPS,TagsPath,StructFolder,MapVersion,ref XBoxMapData);
					BSPSizes = new uint[BSPS.Length];
					for (int b = 0;b < BSPS.Length;b++)
					{
						FixStringArray(ref GlobalTable,BSPS[b]);
						BSPSizes[b] = ProcessDependants(BSPS[b],ref GlobalTable,ref BSPS,TagsPath,StructFolder,MapVersion,ref XBoxMapData);
					}

					break;
			}
			 return GlobalTable;
		}
		public uint ProcessDependants(string FileName,ref string[] GlobalTags,ref string[] BSPS,string TagsPath,string StructFolder,MapfileVersion MapVersion,ref sXBoxMapData XBoxMapData)
		{
			TagFileName tfnTAG = new TagFileName(FileName,MapVersion);
			TagBase tbTAG = new TagBase();
			tbTAG.LoadTagBuffer(tfnTAG);
			TagReader Tag = new TagReader();
			MTSFReader mr = new MTSFReader();
			mr.MTSFRead("Core.Compiler.HALO_PC_SET.MTSF");
			TSFReader STSF = new TSFReader();
			STSF.TSF(ref mr,tbTAG.Header.TagClass0);
			string[] tmp = new string[0];
			Tag.DoProcessStruct(ref STSF, tbTAG.Stream,1,ref GlobalTags,ref BSPS,TagsPath,StructFolder,MapVersion,ref XBoxMapData);
			uint tmpSize = Tag.TagMetaSize;
			tbTAG = null;
			string test = " nothing ";
			return tmpSize;
		}
		public static void GetUInt(byte[] data,uint offset,ref uint Value)
		{
			Value = BitConverter.ToUInt32(data,(int)offset);
		}
		public static uint GetUInt(byte[] data,uint offset)
		{
			return BitConverter.ToUInt32(data,(int)offset);
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
		public void FixStringArray(ref string[] Global,string tmp)
		{
			bool Found = false;
			for(int y = 0; y < Global.Length;y++)
			{
				if(tmp == Global[y])
				{
					Found = true;
				}
			}
			if (Found == false)
			{
				StrArrayRedim(ref Global,tmp);
			}
		}
		private void StrArrayRedim(ref string[] MagArray,string InLine)
		{
			string[] TempStrArray = new string[MagArray.Length + 1];
			MagArray.CopyTo(TempStrArray,0);
			TempStrArray[TempStrArray.Length - 1] = InLine;
			MagArray = new string[TempStrArray.Length];

			TempStrArray.CopyTo(MagArray,0);
			TempStrArray = null;
		}

		public IndexBuilder()
		{
			//
			// TODO: Add constructor logic here
			//
		}

	}
	public struct sXVerts
	{
		public byte[] IndirectOffsets;// = new byte[0];
		public byte[] Verts;// = new byte[0];
		public uint ModelCount;
		uint ModelIndex;
		public void Create()
		{
			ModelCount =0;
			ModelIndex = 0;
			IndirectOffsets = new byte[0];
			Verts = new byte[0];
		}
		public void GetOffset(ref uint Value)
		{
			Value = BitConverter.ToUInt32(IndirectOffsets,(int)(ModelIndex * 0x0c) + 4);
			ModelIndex +=1;
		}
		public uint GetOffset()
		{
			uint Value = BitConverter.ToUInt32(IndirectOffsets,(int)(ModelIndex * 0x0c) + 4);
			ModelIndex +=1;
			return Value;
		}
		public void AddVerts(byte[] InVerts)
		{
			#region VertsHandler
			ModelCount +=1;
			uint CurrentPos = (uint)Verts.Length;
			byte[] tmpVerts = new byte[Verts.Length + InVerts.Length];
			Verts.CopyTo(tmpVerts,0);
			Verts = new byte[Verts.Length + InVerts.Length];
			tmpVerts.CopyTo(Verts,0);
			InVerts.CopyTo(Verts,CurrentPos);
			#endregion
			#region OffsetsStack
			int SavedSize = IndirectOffsets.Length;
			byte[] tmpOffsets = new byte[IndirectOffsets.Length + 0x0c];
			IndirectOffsets.CopyTo(tmpOffsets,0);
			IndirectOffsets = new byte[IndirectOffsets.Length + 0x0c];
			tmpOffsets.CopyTo(IndirectOffsets,0);

			byte[] CaCaPading = BitConverter.GetBytes((uint)0xcacacaca);
			byte[] Offset = BitConverter.GetBytes((uint)CurrentPos);
			CaCaPading.CopyTo(IndirectOffsets,SavedSize + 0);
			Offset.CopyTo(IndirectOffsets,SavedSize + 4);
			CaCaPading.CopyTo(IndirectOffsets,SavedSize + 8);
			#endregion
		}
		public void FixOffsets(uint Offset,uint Magic)
		{
			uint IndCount = (uint)(IndirectOffsets.Length / 0x0C);
			for(int i = 0;i<IndCount;i++)
			{
				uint Current = 0;
				CompUtil.GetUInt(IndirectOffsets,(uint)((i * 0x0c) + 4),ref Current);
				Current += (uint)IndirectOffsets.Length;
				CompUtil.PutUInt(ref IndirectOffsets,(uint)((i * 0x0c) + 4),(Current + Offset)+Magic);
			}
		}
	}
	public struct sXBSPVerts
	{
		public byte[] IndirectOffsets;// = new byte[0];
		public byte[] Verts;// = new byte[0];
		public uint ModelCount;
		public uint CurrentOffset;
		uint ModelIndex;
		public void Create()
		{
			CurrentOffset += 0x18;
			ModelCount =0;
			ModelIndex =0;
			IndirectOffsets = new byte[0];
			Verts = new byte[0];
		}
		public void GetOffset(ref uint Value)
		{
			Value = BitConverter.ToUInt32(IndirectOffsets,(int)(ModelIndex * 0x0c) + 4);
			ModelIndex +=2;
		}
		public uint GetOffset()
		{
			uint Value = BitConverter.ToUInt32(IndirectOffsets,(int)(ModelIndex * 0x0c) + 4);
			ModelIndex +=2;
			return Value;
		}
		public void AddVerts(byte[] InVerts,byte[] InLMD)
		{
			ModelCount +=1;
			uint CurrentPos = (uint)Verts.Length;
			uint VertPos = (uint)Verts.Length;
			ReDim(ref Verts,InVerts);

			int SavedSize = IndirectOffsets.Length;
			byte[] tmpOff = new byte[0x0c];
			ReDim(ref IndirectOffsets,tmpOff);			
			byte[] CaCaPading = BitConverter.GetBytes((uint)0xcacacaca);
			byte[] Offset = BitConverter.GetBytes((uint)CurrentPos);
			CaCaPading.CopyTo(IndirectOffsets,SavedSize + 0);
			Offset.CopyTo(IndirectOffsets,SavedSize + 4);
			CaCaPading.CopyTo(IndirectOffsets,SavedSize + 8);

			uint LMDPos = (uint)Verts.Length;
			ReDim(ref Verts,InLMD);
			Fix20BytePos(ref Verts);
			CurrentPos = (uint)Verts.Length;
			SavedSize = IndirectOffsets.Length;

			
			ReDim(ref IndirectOffsets,tmpOff);			
			CaCaPading = BitConverter.GetBytes((uint)0xcacacaca);
			Offset = BitConverter.GetBytes((uint)LMDPos);
			CaCaPading.CopyTo(IndirectOffsets,SavedSize + 0);
			Offset.CopyTo(IndirectOffsets,SavedSize + 4);
			CaCaPading.CopyTo(IndirectOffsets,SavedSize + 8);
		}
		public static void ReDim(ref byte[] Buffer,byte[] AddBuff)
		{
			int SvSz = Buffer.Length;
			byte[] tmp = new byte[Buffer.Length + AddBuff.Length];
			Buffer.CopyTo(tmp,0);
			Buffer = new byte[tmp.Length];
			tmp.CopyTo(Buffer,0);
			AddBuff.CopyTo(Buffer,SvSz);
			tmp = new byte[0];
			tmp = null;
		}
		public void FixOffsets(uint Offset,uint Magic)
		{
			uint IndCount = (uint)(IndirectOffsets.Length / 0x0C);
			for(int i = 0;i<IndCount;i++)
			{
				uint Current = 0;
				CompUtil.GetUInt(IndirectOffsets,(uint)((i * 0x0c) + 4),ref Current);
				CompUtil.PutUInt(ref IndirectOffsets,(uint)((i * 0x0c) + 4),(Current + Offset)+Magic);
			}
		}
		public static void Fix20BytePos(ref Byte[] Buffer)
		{
			if ((Buffer.Length % 0x20) != 0)
			{
				uint Size = (0x20 - ((uint)Buffer.Length % 0x20));
				Size += (uint)Buffer.Length;
				byte[] tmp = new byte[Size];
				Buffer.CopyTo(tmp,0);
				Buffer = new byte[Size];
				tmp.CopyTo(Buffer,0);
			}
		}
	}
	public struct sTXBSPVerts
	{
  	public byte[] VIndirectOffsets;// = new byte[0];
		public byte[] LIndirectOffsets;
		public byte[] ModelData;// = new byte[0];
		public uint VertIndex;
		public uint LightMapIndex;
		public uint CurrentOffset;
		public uint DataCount;
		public uint VLMapOffset;
		public uint ILMapOffset;
		public void Create()
		{
			CurrentOffset += 0x18;
			VertIndex =0;
			LightMapIndex =0;
			DataCount = 0;
			VIndirectOffsets = new byte[0];
			LIndirectOffsets = new byte[0];
			ModelData = new byte[0];
		}
		public void AddVerts(byte[] InVerts,byte[] InLMD)
		{
			DataCount +=1;
			VertIndex +=1;
			LightMapIndex +=1;
			uint CurrentPos = (uint)ModelData.Length;
			uint VertPos = (uint)ModelData.Length;
			ReDim(ref ModelData,InVerts);
      PushOffset(ref VIndirectOffsets,VertPos);
			uint LightMapPos = (uint)ModelData.Length;
			ReDim(ref ModelData,InLMD);
			PushOffset(ref LIndirectOffsets,LightMapPos); 
			Fix20BytePos(ref ModelData);
    }
		public void PushOffset(ref byte[] OffsetsList,uint Offset)
		{
			int SvSz = OffsetsList.Length;
			byte[] tmp = new byte[SvSz + 0x0c];
			OffsetsList.CopyTo(tmp,0x0c);
			byte[] CaCaPading = BitConverter.GetBytes((uint)0xcacacaca);
			byte[] baOffset = BitConverter.GetBytes((uint)Offset);
			CaCaPading.CopyTo(tmp,0);
			baOffset.CopyTo(tmp,4);
			CaCaPading.CopyTo(tmp,8);
			OffsetsList = new byte[tmp.Length];
			tmp.CopyTo(OffsetsList,0);
			tmp = null;
		}
		public void GetVOffset(ref uint Value)
		{
			VertIndex -=1;
			Value = BitConverter.ToUInt32(VIndirectOffsets,(int)(VertIndex * 0x0c) + 4);
		}
		public uint GetVOffset()
		{
			VertIndex -=1;
			uint Value = BitConverter.ToUInt32(VIndirectOffsets,(int)(VertIndex * 0x0c) + 4);
			return Value;
		}
		public void Finish()
		{
			Fix20BytePos(ref LIndirectOffsets);
		}
		public void GetLOffset(ref uint Value)
		{
			Value = BitConverter.ToUInt32(LIndirectOffsets,(int)(LightMapIndex * 0x0c) + 4);
			VertIndex -=1;
		}
		public uint GetLOffset()
		{
			uint Value = BitConverter.ToUInt32(LIndirectOffsets,(int)(LightMapIndex * 0x0c) + 4);
			VertIndex -=1;
			return Value;
		}

		public static void ReDim(ref byte[] Buffer,byte[] AddBuff)
		{
			int SvSz = Buffer.Length;
			byte[] tmp = new byte[Buffer.Length + AddBuff.Length];
			Buffer.CopyTo(tmp,0);
			Buffer = new byte[tmp.Length];
			tmp.CopyTo(Buffer,0);
			AddBuff.CopyTo(Buffer,SvSz);
			tmp = new byte[0];
			tmp = null;
		}
		public void FixOffsets(uint Offset,uint Magic)
		{
			uint IndCount = (uint)(VIndirectOffsets.Length / 0x0C);
			for(int i = 0;i<IndCount;i++)
			{
				uint Current = 0;
				CompUtil.GetUInt(VIndirectOffsets,(uint)((i * 0x0c) + 4),ref Current);
				CompUtil.PutUInt(ref VIndirectOffsets,(uint)((i * 0x0c) + 4),(Current + Offset)+Magic);
				CompUtil.GetUInt(LIndirectOffsets,(uint)((i * 0x0c) + 4),ref Current);
				CompUtil.PutUInt(ref LIndirectOffsets,(uint)((i * 0x0c) + 4),(Current + Offset)+Magic);
			}
		}
		public static void Fix20BytePos(ref Byte[] Buffer)
		{
			if ((Buffer.Length % 0x20) != 0)
			{
				uint Size = (0x20 - ((uint)Buffer.Length % 0x20));
				Size += (uint)Buffer.Length;
				byte[] tmp = new byte[Size];
				Buffer.CopyTo(tmp,0);
				Buffer = new byte[Size];
				tmp.CopyTo(Buffer,0);
			}
		}
	}

	public class TagReader
	{
		string TabReplace = "";
		byte TabByte = 0x09;
		ushort Name;
		uint Size;
		public uint TagMetaSize;
		byte[] TagBuff = new byte[0];
		public void DoProcessStruct(ref TSFReader sr,MemoryStream fs,uint Count,ref string[] TagList,ref string[] BSPS,string TagsPath,string StructFolder,MapfileVersion MapVersion,ref sXBoxMapData XBoxMapData)
		{
			TabReplace += (char)TabByte;
			//string InLine;
			//string[] CMD = LastInline.Replace(TabReplace,"").ToLower().Trim().Split(new char[]{' '},256);
			Name = sr.GetUName(); //CMD[2];
			Size = sr.GetSOC(); //Val(CMD[3]);
			TagBuff = new byte[Size * Count];
			TagMetaSize += (uint)TagBuff.Length;
			fs.Read(TagBuff,0,TagBuff.Length);
			uint StartOfStruct = sr.Position;
			uint sc = 0;
			bool ExitStruct = false;
			do
			{
				do
				{
          //if(BSPS.Length != 0)
          // Trace.WriteLine("found yar bsp");
					//InLine = sr.Read();
					//CMD = InLine.Replace(TabReplace,"").ToLower().Trim().Split(new char[]{' '},256);
					sr.Read();
					switch (sr.GetCMD())
					{
						case TSFReader.TSFStruct: // "struct":
							#region Structure
							uint ChildCount = GetUInt(TagBuff,(uint)(sr.GetOIP()) + (sc * Size));
							
							if (ChildCount != 0)
							{
								TagReader Child = new TagReader();
								Child.DoProcessStruct(ref sr,fs,ChildCount,ref TagList,ref BSPS,TagsPath,StructFolder,MapVersion,ref XBoxMapData);
								TagMetaSize += Child.TagMetaSize;
							}
							else
							{
								sr.SeekAheadTo(0xA7,sr.GetUName());

								//sr.SeekAheadTo("end " + CMD[2]);
								//sr.Seek(sr.Position + 1);
							}
							break;
							#endregion
						case TSFReader.TSFTagRef: // "tagref":
							#region TagRefernce
							uint tStringSize = 0;
							GetUInt(TagBuff,(uint)(sr.GetOIP() + (sc * Size)) + 0x08,ref tStringSize);
							if (tStringSize != 0x00)
							{
								uint TagClass = GetUInt(TagBuff,(uint)(sr.GetOIP() + (sc * Size)));
								string TagClassString = GetTagClassRev(BitConverter.GetBytes(TagClass),0);

								MTSFReader mr = new MTSFReader();
								mr.MTSFRead("Core.Compiler.HALO_PC_SET.MTSF");

								TSFReader tmpSTSF = new TSFReader();

								//SwapInt(TagClasses,0);
								tmpSTSF.TSF(ref mr,TagClass);

								//MAG tmpMR = new MAG(StructFolder + TagClassString.Trim() + ".mag");
								byte[] tmpStr = new byte[tStringSize + 1];
								fs.Read(tmpStr,0,tmpStr.Length);
								string tag = ReadString(tmpStr,0);

                if((tag.Length+1) != tmpStr.Length)
                {
                  Trace.WriteLine("detected bad tagref string.");
                  throw new Exception("detected bad tagref string.");
                }
                else
                {
                  bool Test = FixStringArray(ref TagList,(string)(tag + "." + tmpSTSF.Name));
                  if (Test == false)
                  {
                    ProcessDependants(tag + "." + tmpSTSF.Name,ref TagList,ref BSPS,TagsPath,StructFolder,MapVersion,ref XBoxMapData);
                  }
                }
								
								tmpSTSF= null;
								int a = 0;
							}
							break;
							#endregion
						case TSFReader.TSFBSPTagRef: // "bspstagref":
							#region BSPTagRefernce
							uint bStringSize = 0;
							GetUInt(TagBuff,(uint)(sr.GetOIP() + (sc * Size)) + 0x08,ref bStringSize);
							if (bStringSize != 0x00)
							{
								uint TagClass = GetUInt(TagBuff,(uint)(sr.GetOIP() + (sc * Size)));
								string TagClassString = GetTagClassRev(BitConverter.GetBytes(TagClass),0);

								MTSFReader mr = new MTSFReader();
								mr.MTSFRead("Core.Compiler.HALO_PC_SET.MTSF");

								TSFReader tmpSTSF = new TSFReader();

								//SwapInt(TagClasses,0);
								tmpSTSF.TSF(ref mr,TagClass);



								//MAG bspMR = new MAG(StructFolder + TagClassString.Trim() + ".mag");
								byte[] bspStr = new byte[bStringSize + 1];
								fs.Read(bspStr,0,bspStr.Length);
								string tag = ReadString(bspStr,0);
								//bspMR.Seek(0);
								bool Test = FixStringArray(ref BSPS,(string)(tag + "." + tmpSTSF.Name));
								//if (Test == false)
								//{
								//	ProcessDependants(tag + "." + tmpMR.Current,ref TagList,ref BSPS,TagsPath,StructFolder);
								//}
								tmpSTSF = null;
								int a = 0;
							}
							break;
							#endregion
						case TSFReader.TSFInternalRaw: // "internalraw":
							#region InternalRaw
							uint RawSize = GetUInt(TagBuff,(uint)(sr.GetOIP() + (sc * Size)));
							uint RawOffset = GetUInt(TagBuff,(uint)(sr.GetUName() + (sc * Size)));
							byte[] tmp = new byte[RawSize];
							TagMetaSize += RawSize;
							fs.Read(tmp,0,tmp.Length);
							tmp = null;
							break;
							#endregion
						case TSFReader.TSFSoundData:     // "sounddata":
							#region SoundData
							uint sRawSize = GetUInt(TagBuff,(uint)(sr.GetOIP() + (sc * Size)));
							uint sRawOffset = GetUInt(TagBuff,(uint)(sr.GetUName() + (sc * Size)));
							byte[] stmp = new byte[sRawSize];
							fs.Read(stmp,0,stmp.Length);
							stmp = null;
							break;
							#endregion
						case TSFReader.TSFBitmapRaw:     // "bitmapraw":
							#region BitmapRawData
							uint TiffSize=0; GetUInt(TagBuff,0x1C,ref TiffSize);
							byte[] TiffBuffer = new byte[TiffSize];
							fs.Read(TiffBuffer,0,TiffBuffer.Length);
							uint BitmapSize = GetUInt(TagBuff,0x30);
							TiffBuffer = new byte[BitmapSize];
							fs.Read(TiffBuffer,0,TiffBuffer.Length);
							TiffBuffer = null;
							break;
							#endregion
						case TSFReader.TSFModelData:     // "modeldata":
							#region ModelData
							long SaveMapFilePos = (uint)fs.Position;
							uint TrueVerticesSize = (GetUInt(TagBuff,(uint)(88 + (sc * Size)))) * 68; 
							uint TrueIndicesSize =(((GetUInt(TagBuff,(uint)(72 + (sc * Size)))) / 3) + 1 ) * 6; 
							uint NewIndicesDataSize = (GetUInt(TagBuff,(uint)(72 + (sc * Size))) / 3) + 1;
							byte[] RawModelBuffer = new byte[TrueVerticesSize];
							fs.Read(RawModelBuffer,0,RawModelBuffer.Length);
							RawModelBuffer = new byte[TrueIndicesSize];
							fs.Read(RawModelBuffer,0,RawModelBuffer.Length);
							byte[] TestBuffer = new byte[6];
							fs.Read(TestBuffer,0,6);
							if (TestBuffer[4] == 0xff)
							{
							}
							else
							{
								fs.Seek(fs.Position - 6,SeekOrigin.Begin);
							}
							break;
							#endregion
						case TSFReader.TSFRawXModelData: // "XModelData":
							#region XModelData
							uint XTrueVerticesSize = (GetUInt(TagBuff,88 + (sc * Size))) * 32;
							uint XTrueIndicesSize =(((GetUInt(TagBuff,72 + (sc * Size))) / 3) + 1 ) * 6;
							uint XNewIndicesDataSize = (GetUInt(TagBuff,72 + (sc * Size)) / 3) + 1;
							byte[] RawXModelBuffer = new byte[XTrueVerticesSize];
							fs.Read(RawXModelBuffer,0,(int)XTrueVerticesSize);
							XBoxMapData.XVerts.AddVerts(RawXModelBuffer);
							RawXModelBuffer = new byte[XTrueIndicesSize];
							fs.Read(RawXModelBuffer,0,RawXModelBuffer.Length);
							byte[] XTestBuffer = new byte[6];
							fs.Read(XTestBuffer,0,6);
							byte[] tmpBuff = new byte[0];
							if (XTestBuffer[4] == 0xff)
							{
								tmpBuff = new byte[XTrueIndicesSize + 6];
								RawXModelBuffer.CopyTo(tmpBuff,0);
								XTestBuffer.CopyTo(tmpBuff,XTrueIndicesSize);
							}
							else
							{
								tmpBuff = new byte[XTrueIndicesSize];
								RawXModelBuffer.CopyTo(tmpBuff,0);
								fs.Seek(fs.Position - 6,SeekOrigin.Begin);
							}
							XBoxMapData.XIndices.AddVerts(tmpBuff);
							CompUtil.FixByteArrayIntPosition(ref XBoxMapData.XIndices.Verts);
							break;
							#endregion
						case TSFReader.TSFBSPModel:      // "bspmodel":
							#region BspModels
							#region BSPSHandlerVars
							uint UnCompressedVerts = sr.GetOIP();
							uint CompressedVerts = sr.GetSOC();
							//uint VertsOffset = GetUInt(TagBuff,CompressedVerts);
							uint TrueVertCount = GetUInt(TagBuff,180 + (sc * Size));
							uint TrueLightMapDataCount = GetUInt(TagBuff,200 + (sc * Size));
							uint UnCompressedLightMapData = (TrueLightMapDataCount * 20);
							uint CompressedLightMapData = (TrueLightMapDataCount * 8);
							byte[] BspVertsUnCompressed = new byte[0];
							byte[] BspLightMapDataUnCompressed = new byte[0];
							#endregion
						switch(MapVersion)
						{
							case MapfileVersion.HALOPC:
								#region HaloPcBspHandler
								UnCompressedVerts = (TrueVertCount * 56);
								CompressedVerts = (TrueVertCount * 32);
								TagMetaSize += UnCompressedVerts + UnCompressedLightMapData;

								BspVertsUnCompressed = new byte[UnCompressedVerts];
								fs.Read(BspVertsUnCompressed,0,BspVertsUnCompressed.Length);
								//FO.Write(BspVertsUnCompressed,0,BspVertsUnCompressed.Length);
								//byte[] BspVertsCompressed = new byte[CompressedVerts];
								BspLightMapDataUnCompressed = new byte[UnCompressedLightMapData];
								fs.Read(BspLightMapDataUnCompressed,0,BspLightMapDataUnCompressed.Length);
								//FO.Write(BspLightMapDataUnCompressed,0,BspLightMapDataUnCompressed.Length);
								//byte[] BspLightMapDataCompressed = new byte[CompressedLightMapData];

								//viper told me to skip past compressed data as a quick fix to 
								// the crash problem - Grenadiac
								fs.Seek(CompressedVerts, SeekOrigin.Current); //gren added this
								fs.Seek(CompressedLightMapData, SeekOrigin.Current); //gren added this
								break;
								#endregion
							case MapfileVersion.XHALO1:
								#region HaloXboxBspHandler
								UnCompressedVerts = (TrueVertCount * 56);
								CompressedVerts = (TrueVertCount * 32);
								TagMetaSize += CompressedVerts + CompressedLightMapData + 0x0c;

								BspVertsUnCompressed = new byte[UnCompressedVerts];
								fs.Read(BspVertsUnCompressed,0,BspVertsUnCompressed.Length);
								//FO.Write(BspVertsUnCompressed,0,BspVertsUnCompressed.Length);

								BspLightMapDataUnCompressed = new byte[UnCompressedLightMapData];
								fs.Read(BspLightMapDataUnCompressed,0,BspLightMapDataUnCompressed.Length);
								//FO.Write(BspLightMapDataUnCompressed,0,BspLightMapDataUnCompressed.Length);
								byte[] BspVertsCompressed = new byte[CompressedVerts];
								byte[] BspLightMapDataCompressed = new byte[CompressedLightMapData];
								fs.Read(BspVertsCompressed,0,BspVertsCompressed.Length);
								fs.Read(BspLightMapDataCompressed,0,BspLightMapDataCompressed.Length);
								XBoxMapData.XBSPVerts.AddVerts(BspVertsCompressed,BspLightMapDataCompressed);
								//viper told me to skip past compressed data as a quick fix to 
								// the crash problem - Grenadiac
								//fs.Seek(CompressedVerts, SeekOrigin.Current); //gren added this
								//fs.Seek(CompressedLightMapData, SeekOrigin.Current); //gren added this
								#endregion
								break;
							}
							break;
							#endregion
						case TSFReader.TSFResource:      // "resource":
							#region Resources
							ushort rStringSize = GetUShort(TagBuff,0x06 + (sc * Size));
							if (rStringSize != 0x00)
							{
								uint TagClassFlag = GetUShort(TagBuff,0x00 + (sc * Size));
								string TagClassString ="";
								string TagClassEXT ="";
								switch(TagClassFlag)
								{
									case 0x00:
										TagClassString = "bitm";
										TagClassEXT = "bitmap";
										break;
									case 0x01:
										TagClassString = "snd!";
										TagClassEXT = "sound";
										break;
								}
								
								//MAG tmpMR = new MAG(StructFolder + TagClassString.Trim() + ".mag");

								byte[] tmpStr = new byte[rStringSize + 1];
								fs.Read(tmpStr,0,tmpStr.Length);
								string tag = ReadString(tmpStr,0);
								//tmpMR.Seek(0);
								bool Test = FixStringArray(ref TagList,(string)(tag + "." + TagClassEXT));
								if (Test == false)
								{
									ProcessDependants(tag + "." + TagClassEXT,ref TagList,ref BSPS,TagsPath,StructFolder,MapVersion,ref XBoxMapData);
								}
								//tmpMR = null;
								int a = 0;
							}
							break;
							#endregion
						case TSFReader.TSFEnd:           // "end":
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
		}
		public void ProcessDependants(string FileName, ref string[] GlobalTags,ref string[] BSPS, string TagsPath,string StructFolder,MapfileVersion MapVersion,ref sXBoxMapData XBoxMapData)
		{
      //FileInfo TagFile_info = new FileInfo(TagsPath + FileName);
			//FileStream TagFile;
			//TagFile = TagFile_info.Open(FileMode.Open,FileAccess.Read);
			TagFileName tfnTAG = new TagFileName(FileName,MapVersion);
			TagBase tbTAG = new TagBase();
      Trace.WriteLine("Processing Dependents:  " + FileName);

			tbTAG.LoadTagBuffer(tfnTAG);

			TagReader Tag = new TagReader();
			//byte[] TagHeader = new byte[0x40];
			//TagFile.Read(TagHeader,0,TagHeader.Length);
			//string TagName = Tag.GetTagClass(TagHeader,0x24);
			//MAG sm = new MAG(StructFolder + TagName.Trim() + ".mag");
			//sm.Seek(1);

			MTSFReader mr = new MTSFReader();
			mr.MTSFRead("Core.Compiler.HALO_PC_SET.MTSF");

			TSFReader STSF = new TSFReader();

			//SwapInt(TagClasses,0);
			STSF.TSF(ref mr,tbTAG.Header.TagClass0);


			string[] tmp = new string[0];
			Tag.DoProcessStruct(ref STSF,tbTAG.Stream,1,ref GlobalTags,ref BSPS,TagsPath,StructFolder,MapVersion,ref XBoxMapData);
			//TagFile.Close();
			tbTAG = null;
			STSF = null;
			string test = " nothing ";;
		}
		public bool FixStringArray(ref string[] Global,string tmp)
		{
			bool Found = false;
			for(int y = 0; y < Global.Length;y++)
			{
				if(tmp == Global[y])
				{
					Found = true;
				}
			}
			if (Found == false)
			{
				StrArrayRedim(ref Global,tmp);
			}
			return Found;
		}
		public static void GetUInt(byte[] data,uint offset,ref uint Value)
		{
			Value = BitConverter.ToUInt32(data,(int)offset);
		}
		public static void GetUShort(byte[] data,uint offset,ref ushort Value)
		{
			Value = BitConverter.ToUInt16(data,(int)offset);
		}
		public static ushort GetUShort(byte[] data,uint offset)
		{
			return BitConverter.ToUInt16(data,(int)offset);
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
		private string ReadString2(byte[] Array,uint Offset,uint size)
		{
			string OutString = "";
			uint count = Offset;
			for (int o = 0;o < size;o++)
			{
				OutString += (char)Array[count];
				count += 1;
			}
			return OutString;
		}

		private void StrArrayRedim(ref string[] MagArray,string InLine)
		{
			string[] TempStrArray = new string[MagArray.Length + 1];
			MagArray.CopyTo(TempStrArray,0);
			TempStrArray[TempStrArray.Length - 1] = InLine;
			MagArray = new string[TempStrArray.Length];

			TempStrArray.CopyTo(MagArray,0);
			TempStrArray = null;
		}

		public TagReader()
		{
		}
	}
}

namespace Prometheus.Core.Compiler_Gren
{
  public class TagFileNameCollection
  {
    private ArrayList tfnList = new ArrayList();

    public string[] RelativePathList
    {
      get
      {
        string[] filelist = new string[tfnList.Count];
        for(int i=0; i<filelist.Length; i++)
        {
          if(tfnList[i] != null)
            filelist[i] = ((TagFileName)tfnList[i]).RelativePath;
          else
            filelist[i] = "";
        }

        return(filelist);
      }
    }
    public bool AddTagFilename(TagFileName tfn)
    {
      //see if the tag is already in the index list
      bool Found = false;

      if(tfn != null)
      {
        for(int y=0; y<tfnList.Count; y++)
        {
          if(tfn.RelativePath == ((TagFileName)tfnList[y]).RelativePath)
          {
            Found = true;
            break;
          }
        }
      }

      //if it wasn't found, add it to the end of the list
      if(Found == false)
        tfnList.Add(tfn);

      return(Found);
    }
    public void Clear()
    {
      tfnList.Clear();
    }
  }
  /// <summary>
  /// Summary description for IndexBuilder.
  /// </summary>
  public class DependencyBuilder
  {
    public static TagFileNameCollection mapfileTagsIndex = new TagFileNameCollection();
    //public static string[] mapfileTagsIndex = new string[1];
    public static string[] BSPS = new string[0];
    public static TagFileNameCollection brokenDependenciesParents = new TagFileNameCollection();
    public static TagFileNameCollection brokenDependencies = new TagFileNameCollection();
    public static bool dependencyCheckFailed = false;

    public uint[] BSPSizes;
    public struct IndexItem
    {
      public uint TagClass1;
      public uint TagClass2;
      public uint TagClass3;
      public ushort Index1;
      public ushort Index2;
      public uint StringOffset;
      public uint MetaOffset;
      public uint MetaSize;
      public uint Empty1;
      public uint Empty2;
    }
    public IndexItem[] IndexItems = new IndexItem[0];
    public string[] BuildIndex(TagFileName ScenarioTFN)
    {
      mapfileTagsIndex.AddTagFilename(ScenarioTFN);
      ProcessDependants(ScenarioTFN);

      byte[] tmpMapType = new byte[0x5b0];

      TagBase tag = new TagBase();
      tag.LoadTagBuffer(ScenarioTFN);			
      tag.Stream.Read(tmpMapType,0,tmpMapType.Length);


      ushort usMType = BitConverter.ToUInt16(tmpMapType,0x3C);
      tag = null;
      //TagFile.Close();
      switch (usMType)
      {
        case 0: // SP
          break;
        case 1: // MP
          AddTagToIndex(@"globals\globals.globals");
          ProcessDependants(new TagFileName(@"globals\globals.globals", MapfileVersion.HALOPC));
          AddTagToIndex(@"ui\ui_tags_loaded_all_scenario_types.tag_collection");
          ProcessDependants(new TagFileName(@"ui\ui_tags_loaded_all_scenario_types.tag_collection", MapfileVersion.HALOPC));
          AddTagToIndex(@"ui\ui_input_device_defaults.tag_collection");
          ProcessDependants(new TagFileName(@"ui\ui_input_device_defaults.tag_collection", MapfileVersion.HALOPC));
          AddTagToIndex(@"ui\ui_default_profiles.tag_collection");
          ProcessDependants(new TagFileName(@"ui\ui_default_profiles.tag_collection", MapfileVersion.HALOPC));
          AddTagToIndex(@"ui\shell\bitmaps\background.bitmap");
          AddTagToIndex(@"ui\shell\strings\loading.unicode_string_list");
          AddTagToIndex(@"ui\shell\main_menu\mp_map_list.unicode_string_list");
          AddTagToIndex(@"ui\shell\bitmaps\trouble_brewing.bitmap");
          AddTagToIndex(@"sound\sfx\ui\cursor.sound");
          AddTagToIndex(@"sound\sfx\ui\forward.sound");
          AddTagToIndex(@"sound\sfx\ui\back.sound");
          AddTagToIndex(@"ui\ui_tags_loaded_multiplayer_scenario_type.tag_collection");
          ProcessDependants(new TagFileName(@"ui\ui_tags_loaded_multiplayer_scenario_type.tag_collection", MapfileVersion.HALOPC));
          BSPSizes = new uint[BSPS.Length];
          for (int b = 0;b < BSPS.Length;b++)
          {
            AddTagToIndex(BSPS[b]);
            BSPSizes[b] = ProcessDependants(new TagFileName(BSPS[b], MapfileVersion.HALOPC));
          }
          break;
        case 0x02: // UI
          AddTagToIndex(@"globals\globals.globals");
          ProcessDependants(new TagFileName(@"globals\globals.globals", MapfileVersion.HALOPC));
          AddTagToIndex(@"ui\ui_tags_loaded_all_scenario_types.tag_collection");
          ProcessDependants(new TagFileName(@"ui\ui_tags_loaded_all_scenario_types.tag_collection", MapfileVersion.HALOPC));
          AddTagToIndex(@"ui\ui_input_device_defaults.tag_collection");
          ProcessDependants(new TagFileName(@"ui\ui_input_device_defaults.tag_collection", MapfileVersion.HALOPC));
          AddTagToIndex(@"sound\sfx\ui\flag_failure.sound");
          AddTagToIndex(@"ui\ui_default_profiles.tag_collection");
          ProcessDependants(new TagFileName(@"ui\ui_default_profiles.tag_collection", MapfileVersion.HALOPC));
          AddTagToIndex(@"ui\shell\bitmaps\background.bitmap");
          AddTagToIndex(@"ui\shell\strings\loading.unicode_string_list");
          AddTagToIndex(@"ui\shell\main_menu\mp_map_list.unicode_string_list");
          AddTagToIndex(@"ui\shell\bitmaps\trouble_brewing.bitmap");
          AddTagToIndex(@"sound\sfx\ui\cursor.sound");
          AddTagToIndex(@"sound\sfx\ui\forward.sound");
          AddTagToIndex(@"sound\sfx\ui\back.sound");
          AddTagToIndex(@"ui\ui_tags_loaded_mainmenu_scenario_type.tag_collection");
          ProcessDependants(new TagFileName(@"ui\ui_tags_loaded_mainmenu_scenario_type.tag_collection", MapfileVersion.HALOPC));
          BSPSizes = new uint[BSPS.Length];
          for (int b = 0;b < BSPS.Length;b++)
          {
            AddTagToIndex(BSPS[b]);
            BSPSizes[b] = ProcessDependants(new TagFileName(BSPS[b], MapfileVersion.HALOPC));
          }
          break;
      }
      return mapfileTagsIndex.RelativePathList;
    }
    public DependencyBuilder()
    {
      brokenDependenciesParents.Clear();
      brokenDependencies.Clear();
    }
    public uint ProcessDependants(TagFileName tfn)
    {
      Trace.WriteLine("ProcessDependents( "+tfn.RelativePath+" )");
      TagBase tbTAG = new TagBase();
      TagReader Tag = new TagReader();
      Tag.localTFN = tfn;
      Compiler.TSFReader STSF = new Compiler.TSFReader();
      Compiler.MTSFReader mr = new Compiler.MTSFReader();

      tbTAG.LoadTagBuffer(tfn);


      mr.MTSFRead("Core.Compiler.HALO_PC_SET.MTSF");


      //SwapInt(TagClasses,0);
      STSF.TSF(ref mr,tbTAG.Header.TagClass0);

      Tag.DoProcessStruct(ref STSF, tbTAG.Stream,1);
      tbTAG = null;
      return Tag.TagMetaSize;
    }
    public static void GetUInt(byte[] data,uint offset,ref uint Value)
    {
      Value = BitConverter.ToUInt32(data,(int)offset);
    }
    public static uint GetUInt(byte[] data,uint offset)
    {
      return BitConverter.ToUInt32(data,(int)offset);
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
    static public bool AddTagToIndex(string TagPath)
    {
      Trace.WriteLine("AddTagToIndex( "+TagPath+" )");
      TagFileName tfn = new TagFileName(TagPath, MapfileVersion.HALOPC);
      bool bFound = mapfileTagsIndex.AddTagFilename(tfn);

      if(tfn.Exists == false)
      {
        dependencyCheckFailed = true;
        brokenDependencies.AddTagFilename(tfn);
      }

      return(bFound);
    }
    /*
    static private void StrArrayRedim(ref string[] MagArray,string InLine)
    {
      string[] TempStrArray = new string[MagArray.Length + 1];
      MagArray.CopyTo(TempStrArray,0);
      TempStrArray[TempStrArray.Length - 1] = InLine;
      MagArray = new string[TempStrArray.Length];

      TempStrArray.CopyTo(MagArray,0);
      TempStrArray = null;
    }
*/
  }
  public class TagReader
  {
    string TabReplace = "";
    byte TabByte = 0x09;
    ushort Name;
    uint Size;
    public uint TagMetaSize;
    byte[] TagBuff = new byte[0];
    public TagFileName localTFN = null;
    public TagFileName parentTFN = null;

    public void DoProcessStruct(ref Compiler.TSFReader sr,MemoryStream fs,uint Count)
    {
      Trace.WriteLine("DoProcessStruct()");
      TabReplace += (char)TabByte;
      //string InLine;
      //string[] CMD = LastInline.Replace(TabReplace,"").ToLower().Trim().Split(new char[]{' '},256);
      Name = sr.GetUName(); //CMD[2];
      Size = sr.GetSOC(); //Val(CMD[3]);
      TagBuff = new byte[Size * Count];
      TagMetaSize += (uint)TagBuff.Length;
      fs.Read(TagBuff,0,TagBuff.Length);
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
            case Compiler.TSFReader.TSFStruct: // "struct":
              uint ChildCount = GetUInt(TagBuff,(uint)(sr.GetOIP()) + (sc * Size));
							
              if (ChildCount != 0)
              {
                TagReader Child = new TagReader();
                Child.localTFN = this.localTFN;
                Child.parentTFN = this.parentTFN;
                Child.DoProcessStruct(ref sr,fs,ChildCount);
                TagMetaSize += Child.TagMetaSize;
              }
              else
              {
                sr.SeekAheadTo(0xA7,sr.GetUName());

                //sr.SeekAheadTo("end " + CMD[2]);
                //sr.Seek(sr.Position + 1);
              }
              break;
            case Compiler.TSFReader.TSFTagRef: // "tagref":
              uint tStringSize = 0;
              GetUInt(TagBuff,(uint)(sr.GetOIP() + (sc * Size)) + 0x08,ref tStringSize);
              if (tStringSize != 0x00)
              {
                uint TagClass = GetUInt(TagBuff,(uint)(sr.GetOIP() + (sc * Size)));
                string TagClassString = GetTagClassRev(BitConverter.GetBytes(TagClass),0);

                Compiler.MTSFReader mr = new Compiler.MTSFReader();
                mr.MTSFRead("Core.Compiler.HALO_PC_SET.MTSF");

                Compiler.TSFReader tmpSTSF = new Compiler.TSFReader();

                //SwapInt(TagClasses,0);
                tmpSTSF.TSF(ref mr,TagClass);

                //MAG tmpMR = new MAG(StructFolder + TagClassString.Trim() + ".mag");
                byte[] tmpStr = new byte[tStringSize + 1];
                fs.Read(tmpStr,0,tmpStr.Length);
                string tag = ReadString(tmpStr,0);

                if((tag.Length+1) != tmpStr.Length)
                {
                  Trace.WriteLine("detected bad tagref string.");
                  throw new Exception("detected bad tagref string.");
                }
                else
                {
                  bool Test = DependencyBuilder.AddTagToIndex(tag + "." + tmpSTSF.Name);
                  if(Test == false)
                  {
                    ProcessDependants(new TagFileName(tag + "." + tmpSTSF.Name, MapfileVersion.HALOPC), this.localTFN);
                  }
                }
								
                tmpSTSF= null;
                int a = 0;
              }
              break;
            case Compiler.TSFReader.TSFBSPTagRef: // "bspstagref":
              uint bStringSize = 0;
              GetUInt(TagBuff,(uint)(sr.GetOIP() + (sc * Size)) + 0x08,ref bStringSize);
              if (bStringSize != 0x00)
              {
                uint TagClass = GetUInt(TagBuff,(uint)(sr.GetOIP() + (sc * Size)));
                string TagClassString = GetTagClassRev(BitConverter.GetBytes(TagClass),0);

                Compiler.MTSFReader mr = new Compiler.MTSFReader();
                mr.MTSFRead("Core.Compiler.HALO_PC_SET.MTSF");

                Compiler.TSFReader tmpSTSF = new Compiler.TSFReader();

                //SwapInt(TagClasses,0);
                tmpSTSF.TSF(ref mr,TagClass);



                //MAG bspMR = new MAG(StructFolder + TagClassString.Trim() + ".mag");
                byte[] bspStr = new byte[bStringSize + 1];
                fs.Read(bspStr,0,bspStr.Length);
                string tag = ReadString(bspStr,0);
                //bspMR.Seek(0);
                bool Test = FixStringArray(ref DependencyBuilder.BSPS,(string)(tag + "." + tmpSTSF.Name));
                //if (Test == false)
                //{
                //	ProcessDependants(tag + "." + tmpMR.Current,ref TagList,ref BSPS,TagsPath,StructFolder);
                //}
                tmpSTSF = null;
                int a = 0;
              }
              break;
            case Compiler.TSFReader.TSFInternalRaw: // "internalraw":
              uint RawSize = GetUInt(TagBuff,(uint)(sr.GetOIP() + (sc * Size)));
              uint RawOffset = GetUInt(TagBuff,(uint)(sr.GetUName() + (sc * Size)));
              byte[] tmp = new byte[RawSize];
              TagMetaSize += RawSize;
              fs.Read(tmp,0,tmp.Length);
              tmp = null;
              break;
            case Compiler.TSFReader.TSFSoundData: // "sounddata":
              uint sRawSize = GetUInt(TagBuff,(uint)(sr.GetOIP() + (sc * Size)));
              uint sRawOffset = GetUInt(TagBuff,(uint)(sr.GetUName() + (sc * Size)));
              byte[] stmp = new byte[sRawSize];
              fs.Read(stmp,0,stmp.Length);
              stmp = null;
              break;
            case Compiler.TSFReader.TSFBitmapRaw: // "bitmapraw":
              uint TiffSize=0; GetUInt(TagBuff,0x1C,ref TiffSize);
              byte[] TiffBuffer = new byte[TiffSize];
              fs.Read(TiffBuffer,0,TiffBuffer.Length);
              uint BitmapSize = GetUInt(TagBuff,0x30);
              TiffBuffer = new byte[BitmapSize];
              fs.Read(TiffBuffer,0,TiffBuffer.Length);
              TiffBuffer = null;
              break;
            case Compiler.TSFReader.TSFModelData: // "modeldata":
              long SaveMapFilePos = (uint)fs.Position;
              uint TrueVerticesSize = (GetUInt(TagBuff,(uint)(88 + (sc * Size)))) * 68; 
              uint TrueIndicesSize =(((GetUInt(TagBuff,(uint)(72 + (sc * Size)))) / 3) + 1 ) * 6; 
              uint NewIndicesDataSize = (GetUInt(TagBuff,(uint)(72 + (sc * Size))) / 3) + 1;
              byte[] RawModelBuffer = new byte[TrueVerticesSize];
              fs.Read(RawModelBuffer,0,RawModelBuffer.Length);
              RawModelBuffer = new byte[TrueIndicesSize];
              fs.Read(RawModelBuffer,0,RawModelBuffer.Length);
              byte[] TestBuffer = new byte[6];
              fs.Read(TestBuffer,0,6);
              if (TestBuffer[4] == 0xff)
              {
              }
              else
              {
                fs.Seek(fs.Position - 6,SeekOrigin.Begin);
              }
              break;
            case Compiler.TSFReader.TSFBSPModel: // "bspmodel":
              uint UnCompressedVerts = sr.GetOIP();
              uint CompressedVerts = sr.GetSOC();
              //uint VertsOffset = GetUInt(TagBuff,CompressedVerts);
              uint TrueVertCount = GetUInt(TagBuff,180 + (sc * Size));
              uint TrueLightMapDataCount = GetUInt(TagBuff,200 + (sc * Size));
              uint UnCompressedLightMapData = (TrueLightMapDataCount * 20);
              uint CompressedLightMapData = (TrueLightMapDataCount * 8);
              UnCompressedVerts = (TrueVertCount * 56);
              CompressedVerts = (TrueVertCount * 32);
              TagMetaSize += UnCompressedVerts + UnCompressedLightMapData;

              byte[] BspVertsUnCompressed = new byte[UnCompressedVerts];
              fs.Read(BspVertsUnCompressed,0,BspVertsUnCompressed.Length);
              //FO.Write(BspVertsUnCompressed,0,BspVertsUnCompressed.Length);
              //byte[] BspVertsCompressed = new byte[CompressedVerts];
              byte[] BspLightMapDataUnCompressed = new byte[UnCompressedLightMapData];
              fs.Read(BspLightMapDataUnCompressed,0,BspLightMapDataUnCompressed.Length);
              //FO.Write(BspLightMapDataUnCompressed,0,BspLightMapDataUnCompressed.Length);
              //byte[] BspLightMapDataCompressed = new byte[CompressedLightMapData];

              //viper told me to skip past compressed data as a quick fix to 
              // the crash problem - Grenadiac
              fs.Seek(CompressedVerts, SeekOrigin.Current); //gren added this
              fs.Seek(CompressedLightMapData, SeekOrigin.Current); //gren added this
              break;
            case Compiler.TSFReader.TSFResource: // "resource":
              ushort rStringSize = GetUShort(TagBuff,0x06 + (sc * Size));
              if (rStringSize != 0x00)
              {
                uint TagClassFlag = GetUShort(TagBuff,0x00 + (sc * Size));
                string TagClassString ="";
                string TagClassEXT ="";
                switch(TagClassFlag)
                {
                  case 0x00:
                    TagClassString = "bitm";
                    TagClassEXT = "bitmap";
                    break;
                  case 0x01:
                    TagClassString = "snd!";
                    TagClassEXT = "sound";
                    break;
                }
								
                //MAG tmpMR = new MAG(StructFolder + TagClassString.Trim() + ".mag");

                byte[] tmpStr = new byte[rStringSize + 1];
                fs.Read(tmpStr,0,tmpStr.Length);
                string tag = ReadString(tmpStr,0);
                //tmpMR.Seek(0);
                bool Test = DependencyBuilder.AddTagToIndex(tag + "." + TagClassEXT);
                if (Test == false)
                {
                  ProcessDependants(new TagFileName(tag + "." + TagClassEXT, MapfileVersion.HALOPC), this.localTFN);
                }
                //tmpMR = null;
                int a = 0;
              }
              break;
            case Compiler.TSFReader.TSFEnd: // "end":
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
    }
    public void ProcessDependants(TagFileName tfn, TagFileName parent)
    {
      if(parent != null)
        Trace.WriteLine("ProcessDependents( "+tfn.RelativePath+", "+parent.RelativePath +" )");
      else
        Trace.WriteLine("ProcessDependents( "+tfn.RelativePath+", null)");

      this.parentTFN = parent;
      this.localTFN = tfn;
      if(tfn.Exists)
      {
        TagReader Tag = new TagReader();
        Tag.localTFN = tfn;
        Tag.parentTFN = parent;
        Compiler.TSFReader STSF = new Compiler.TSFReader();
        Compiler.MTSFReader mr = new Compiler.MTSFReader();
        TagBase tbTAG = new TagBase();

        tbTAG.LoadTagBuffer(tfn);
        mr.MTSFRead("Core.Compiler.HALO_PC_SET.MTSF");
        STSF.TSF(ref mr,tbTAG.Header.TagClass0);
        Tag.DoProcessStruct(ref STSF,tbTAG.Stream,1);
        tbTAG = null;
        STSF = null;
      }
      else
      {
        //this isn't the best way to sync up parents and children, but it will work
        //and we won't need to make a separate collection class with dual TFNs
        Trace.WriteLine("file doesn't exist, adding to broken depends");
        DependencyBuilder.dependencyCheckFailed = true;
        DependencyBuilder.brokenDependencies.AddTagFilename(tfn);
        DependencyBuilder.brokenDependenciesParents.AddTagFilename(parent);
      }
    }
    public static void GetUInt(byte[] data,uint offset,ref uint Value)
    {
      Value = BitConverter.ToUInt32(data,(int)offset);
    }
    public static void GetUShort(byte[] data,uint offset,ref ushort Value)
    {
      Value = BitConverter.ToUInt16(data,(int)offset);
    }
    public static ushort GetUShort(byte[] data,uint offset)
    {
      return BitConverter.ToUInt16(data,(int)offset);
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
    private string ReadString2(byte[] Array,uint Offset,uint size)
    {
      string OutString = "";
      uint count = Offset;
      for (int o = 0;o < size;o++)
      {
        OutString += (char)Array[count];
        count += 1;
      }
      return OutString;
    }
    public bool FixStringArray(ref string[] Global,string tmp)
    {
      bool Found = false;
      for(int y = 0; y < Global.Length;y++)
      {
        if(tmp == Global[y])
        {
          Found = true;
        }
      }
      if (Found == false)
      {
        StrArrayRedim(ref Global,tmp);
      }
      return(Found);
    }
    private void StrArrayRedim(ref string[] MagArray,string InLine)
    {
      string[] TempStrArray = new string[MagArray.Length + 1];
      MagArray.CopyTo(TempStrArray,0);
      TempStrArray[TempStrArray.Length - 1] = InLine;
      MagArray = new string[TempStrArray.Length];

      TempStrArray.CopyTo(MagArray,0);
      TempStrArray = null;
    }
  }
}
