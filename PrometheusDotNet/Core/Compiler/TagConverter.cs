using System;
using System.IO;

namespace Prometheus.Core.Compiler
{
	/// <summary>
	/// Summary description for TagConverter.
	/// </summary>
	public class TagConverter
	{
		public TagConverter()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public MemoryStream ConvertTag(string FileName)
		{
			//Open Tag and create a memeory stream//
			FileInfo fi_FileInfo = new FileInfo(FileName);
			FileStream fs_File = fi_FileInfo.Open(FileMode.Open,FileAccess.Read);
			byte[] inFileHeader = new byte[0x40];
			fs_File.Read(inFileHeader,0,0x40);
			byte[] inFileBuffer = new byte[fs_File.Length - 0x40];
			fs_File.Read(inFileBuffer,0,inFileBuffer.Length);
			fs_File.Close();
			MemoryStream ms_inFile = new MemoryStream(inFileBuffer,0,inFileBuffer.Length);
			//Create a memory stream for output tag//
			byte[] outFileBuffer = new byte[inFileBuffer.Length];
			MemoryStream ms_outFile = new MemoryStream(outFileBuffer,0,outFileBuffer.Length);
			//Get the TagClass and open the MTSFReader//
			uint TagClass = CompUtil.GetUInt(inFileHeader,0x24);
			MTSFReader mr;
			mr = new MTSFReader();
			mr.MTSFRead("Core.Compiler.TAG_CONVERSION_SET.MTSF");
			TSFReader STSF = new TSFReader();
			STSF.TSF(ref mr,TagClass);
			//Now Convert the tag From ce to prom//
			Converter Convert = new Converter();

			Convert.TagStruct(ref STSF,ref ms_inFile,ref ms_outFile,1,STSF.GetUName());
			ms_inFile.Close();
			return ms_outFile;
		}
	}
	public class Converter
	{
		static private CE_Bitmap_File CeBitmapIndex = new CE_Bitmap_File();
		string TabReplace = "";
		byte TabByte = 0x09;
		ushort Name;
		uint Size;
		byte[] TagBuff = new byte[0];
		public Converter()
		{
		}
		public void TagStruct(ref TSFReader sr,ref MemoryStream FI,ref MemoryStream FO,uint Count,ushort uName)
		{
			long MapPosSave;
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
						case TSFReader.TSFStruct:        // 0xA0: //Struct
							#region Structure
							uint ChildCount = CompUtil.GetUInt(TagBuff,(uint)(sr.GetOIP()) + (sc * Size));
							if (sr.GetUName() == 0xffff)
							{
								ChildCount = 0;
								CompUtil.PutUInt(ref TagBuff,(uint)(sr.GetOIP()) + (sc * Size),0);
							}
							if (ChildCount != 0)
							{
								Converter Child = new Converter();
								Child.TagStruct(ref sr,ref FI,ref FO,ChildCount,sr.GetUName());
							}
							else
							{
								sr.SeekAheadTo(0xA7,sr.GetUName());
							}
							break;
							#endregion
						case TSFReader.TSFBSPTagRef:     // 0xA8: //BSPTagRef
						case TSFReader.TSFTagRef:        // 0xA1: //TagRef
							#region TagRef
							int StringSize = (int)CompUtil.GetUInt(TagBuff,(uint)(sr.GetOIP() + (sc * Size)) + 0x08);
							if (StringSize != 0)
							{
								byte[] tmpstr = new byte[StringSize + 1];
								FI.Read(tmpstr,0,tmpstr.Length);
								FO.Write(tmpstr,0,tmpstr.Length);
							}
							break;
							#endregion
						case TSFReader.TSFInternalRaw:   // 0xA3: //InternalRaw
							#region InternalRaw
							uint RawSize = CompUtil.GetUInt(TagBuff,(uint)(sr.GetOIP() + (sc * Size)));
							byte[] tmp = new byte[RawSize];
							FI.Read(tmp,0,tmp.Length);
							FO.Write(tmp,0,tmp.Length);
							tmp = null;
							break;
							#endregion
						case TSFReader.TSFIntSwap:
							#region IntSwap
							uint is_Offset = (uint)sr.GetOIP();
							CompUtil.SwapInt(ref TagBuff,is_Offset + (sc * Size));
							
							break;
							#endregion
						case TSFReader.TSFIntSwapRange:
							#region IntSwapRange
							uint StartOffset = (uint)sr.GetOIP();
							uint EndOffset = (uint)sr.GetUName();
							for (uint i = StartOffset;i<EndOffset;i+=4)
							{
								CompUtil.SwapInt(ref TagBuff,i + (sc * Size));
							}
							break;
							#endregion
						case TSFReader.TSFLongSwap:
							#region ShortSwap
							uint Offset = (uint)sr.GetOIP();
							CompUtil.SwapShort(ref TagBuff,Offset + (sc * Size));
							break;
							#endregion
						case TSFReader.TSFLongSwapRange:
							#region ShortSwapRange
							uint lsr_StartOffset = (uint)sr.GetOIP();
							uint lsr_EndOffset = (uint)sr.GetUName();
							for (uint i = lsr_StartOffset;i<lsr_EndOffset;i+=2)
							{
								CompUtil.SwapShort(ref TagBuff,i + (sc * Size));
							}
							break;
							#endregion
						case TSFReader.TSFBitmapRaw:     // 0xA9: //Bitmapraw
							#region BitmapRaw
							/*
							uint BitMapStructCount;
							uint BitMapStructOffset;
							uint TiffSize=0; 
							byte[] tmpBMS;
							long sv = 0;

							#region grenbitmap
							if(Info.IndexItems[Info.CurrentIndex].RawTypeID != 0)
							{
								//Trace.WriteLine("extern ce bitmap:  " + Prometheus.Core.Compiler.Decompiler.CurrentTagName);
								CeBitmapIndex.Read(br);
								//meta header for bitmaps for extern ce files is actually in the bitmaps.map file
								//so we need to get the header from there.

								//fix the index offset, it doesn't use magic
								int bitmap_hdr_offset = (int)CeBitmapIndex.Locator[Info.IndexItems[Info.CurrentIndex].MetaOffset + Info.TagMagic].offset;
								Info.BitmapsFile.Position = bitmap_hdr_offset;
								TagBuff = br.ReadBytes(TagBuff.Length);
								PutUInt(ref TagBuff,0x54,0);
								//get bitmap header(s) offset/count
								BitMapStructCount=GetUInt(TagBuff,0x60);
								BitMapStructOffset=GetUInt(TagBuff,0x64);

								//copy bitmap header(s) from CE shared to output tag
								br.BaseStream.Position += (BitMapStructOffset-Size);
								tmpBMS = new byte[BitMapStructCount * 0x30];
								tmpBMS = br.ReadBytes(tmpBMS.Length);
              
								GetUInt(TagBuff,0x1C,ref TiffSize);
              
								bw.BaseStream.Position = 0;              
								bw.Write(TagBuff);
								bw.BaseStream.Position +=  (BitMapStructOffset-Size);
								//bw.Write(tmpBMS);
								//FO.Write(tmpBMS, (int)FO.Position, tmpBMS.Length);

								//skip over TIFF area in output tag
								GetUInt(TagBuff,0x1C,ref TiffSize);
								byte[] TiffBuffer = new byte[TiffSize];
								bw.Write(TiffBuffer);
								//FO.Write(TiffBuffer,0,TiffBuffer.Length);

								//copy bitmap data from CE shared to output tag
								uint accum = (uint)bw.BaseStream.Position;
								for(int i = 0;i<BitMapStructCount;i++)
								{
									uint BitMapDataOffset = GetUInt(tmpBMS,(uint)(0x18 + (i * 0x30)));
									uint BitMapDataSize = GetUInt(tmpBMS,(uint)(0x1C + (i * 0x30)));
									uint CurrentSize = GetUInt(TagBuff,(uint)0x30);
									PutUInt(ref TagBuff,0x30,CurrentSize + BitMapDataSize); 
									byte[] tmpBitmapData = new byte[BitMapDataSize];
									tmpBitmapData = br.ReadBytes((int)BitMapDataSize);
									bw.Write(tmpBitmapData);
									PutUInt(ref tmpBMS,(uint)(0x18 + (i * 0x30)), accum);
									accum += BitMapDataSize;
								}

								//go to the image header offsets
								//bw.BaseStream.Position = TagBuff.Length + TiffSize + 
								bw.BaseStream.Position += 0x40;
								bw.Write(tmpBMS);
								long eof = bw.BaseStream.Position;

								//fix the offsets
								bw.BaseStream.Position = 0x74;
								bw.Write(accum);
								bw.BaseStream.Position = eof;

								fuckThisShitHack = true;
								//FO.Write(tmpBitmapData, (int)FO.Position, tmpBitmapData.Length);
							}
							else
							{
								GetUInt(TagBuff,0x1C,ref TiffSize);
								byte[] TiffBuffer = new byte[TiffSize];
								FO.Write(TiffBuffer,0,TiffBuffer.Length);
								BitMapStructCount=GetUInt(TagBuff,0x60);
								BitMapStructOffset=GetUInt(TagBuff,0x64) - Info.MapMagic;
								sv = fs.Position;
								fs.Seek((long)BitMapStructOffset,SeekOrigin.Begin);
								tmpBMS = new byte[BitMapStructCount * 0x30];
								fs.Read(tmpBMS,0,tmpBMS.Length);

								for(int i = 0;i<BitMapStructCount;i++)
								{
									uint BitMapDataOffset = GetUInt(tmpBMS,(uint)(0x18 + (i * 0x30)));
									uint BitMapDataSize = GetUInt(tmpBMS,(uint)(0x1C + (i * 0x30)));
									uint CurrentSize = GetUInt(TagBuff,(uint)0x30);
									PutUInt(ref TagBuff,0x30,CurrentSize + BitMapDataSize); 
									byte[] tmpBitmapData = new byte[BitMapDataSize];
									switch (Info.MapVersion)
									{
										case 5:
											fs.Seek((long)BitMapDataOffset,SeekOrigin.Begin);
											fs.Read(tmpBitmapData,0,tmpBitmapData.Length);
											FO.Write(tmpBitmapData,0,tmpBitmapData.Length);
											break;
										case 7:
											Info.BitmapsFile.Seek((long)BitMapDataOffset,SeekOrigin.Begin);
											Info.BitmapsFile.Read(tmpBitmapData,0,tmpBitmapData.Length);
											FO.Write(tmpBitmapData,0,tmpBitmapData.Length);
											break;
										case 0x261:
											if (Info.IndexItems[Info.CurrentIndex].RawTypeID == 0)
											{
												fs.Seek((long)BitMapDataOffset,SeekOrigin.Begin);
												fs.Read(tmpBitmapData,0,tmpBitmapData.Length);
												FO.Write(tmpBitmapData,0,tmpBitmapData.Length);
											}
											else
											{
												//CE_Bitmap_File CEBF = new CE_Bitmap_File();
												//CEBF.HDR.Read(ref Info.BitmapsFile);
												//uint Current_offset = CEBF.Locator[Info.IndexItems[Info.CurrentIndex].MetaOffset].offset;
											}
											break;
									}
								}
								fs.Seek(sv,SeekOrigin.Begin);
							}
							
							#endregion
						*/
							break;
							
							#endregion
						case TSFReader.TSFSoundData:     // 0xAA: //SoundData
							#region SoundData
							/*
							uint DataSizeOffset = sr.GetOIP();
							uint NormalDataOffset = sr.GetUName();
							uint SndDataSize = GetUInt(TagBuff,(uint)(DataSizeOffset + (sc * Size)));
							uint TrueOffset = GetUInt(TagBuff,(uint)(NormalDataOffset + 4 + (sc * Size)));
							byte[] SoundData = new byte[SndDataSize];
						switch (Info.MapVersion)
						{
							case 5:
								long fssv = fs.Position;
								fs.Seek((long)TrueOffset,SeekOrigin.Begin);
								fs.Read(SoundData,0,SoundData.Length);
								fs.Seek(fssv,SeekOrigin.Begin);
								FO.Write(SoundData,0,SoundData.Length);
								break;
							case 7:
								Info.SoundsFile.Seek(TrueOffset,System.IO.SeekOrigin.Begin);
								Info.SoundsFile.Read(SoundData,0,SoundData.Length);
								FO.Write(SoundData,0,SoundData.Length);
								SoundData = null;
								break;
						}
						*/
							break;
							#endregion
						case TSFReader.TSFRawXModelData: // 0xB0: //XModelData
							#region XModelData
							/*
							uint XTrueVerticesSize = (GetUInt(TagBuff,88 + (sc * Size))) * 32;
							uint XTrueIndicesSize =(((GetUInt(TagBuff,72 + (sc * Size))) / 3) + 1 ) * 6;
							uint XNewIndicesDataSize = (GetUInt(TagBuff,72 + (sc * Size)) / 3) + 1;
							uint XTrueVerticesOffset = GetUInt(TagBuff,100 + (sc * Size)) - Info.MapMagic;
							uint XSaveMapFilePos = (uint)fs.Position;//MapFile.Position;
							byte[] XModelHeader = new byte[16];
							fs.Seek((long)XTrueVerticesOffset,System.IO.SeekOrigin.Begin);
							fs.Read(XModelHeader,0,XModelHeader.Length);
							fs.Seek(XSaveMapFilePos,System.IO.SeekOrigin.Begin);
							XTrueVerticesOffset = GetUInt(XModelHeader,4) - Info.MapMagic;
								
							XSaveMapFilePos = (uint)fs.Position;
							byte[] RawXModelBuffer = new byte[XTrueVerticesSize];
							fs.Seek((long)XTrueVerticesOffset,System.IO.SeekOrigin.Begin);
							fs.Read(RawXModelBuffer,0,(int)XTrueVerticesSize);
							FO.Write(RawXModelBuffer,0,(int)XTrueVerticesSize);

							byte[] XTestBuffer = new byte[6];
							uint XTrueIndicesOffset = GetUInt(TagBuff, 80+ (sc * Size)) - Info.MapMagic;
							RawXModelBuffer = new byte[XTrueIndicesSize];
							fs.Seek((long)XTrueIndicesOffset,System.IO.SeekOrigin.Begin);
							fs.Read(XModelHeader,0,XModelHeader.Length);
							XTrueIndicesOffset = GetUInt(XModelHeader,0x04) - Info.MapMagic;
							fs.Seek((long)XTrueIndicesOffset,System.IO.SeekOrigin.Begin);
							fs.Read(RawXModelBuffer,0,RawXModelBuffer.Length);
							FO.Write(RawXModelBuffer,0,RawXModelBuffer.Length);
							fs.Read(XTestBuffer,0,6);
							if (XTestBuffer[4] == 0xff)
							{
								FO.Write(XTestBuffer,0,6);
								XTrueIndicesSize += 6;
							}
							*/
							break;
							#endregion
						case TSFReader.TSFModelData:     // 0xAB: //Modeldata
							#region ModelData
							/*
							long SaveMapFilePos = (uint)fs.Position;
							uint TrueVerticesSize = (GetUInt(TagBuff,(uint)(88 + (sc * Size)))) * 68; 
							uint TrueIndicesSize =(((GetUInt(TagBuff,(uint)(72 + (sc * Size)))) / 3) + 1 ) * 6; 
							uint NewIndicesDataSize = (GetUInt(TagBuff,(uint)(72 + (sc * Size))) / 3) + 1;
							uint TrueVerticesOffset = GetUInt(TagBuff,(uint)(100+ (sc * Size))) + Info.VerticesOffset;  //80
							uint TrueIndicesOffset = GetUInt(TagBuff,(uint)(80+ (sc * Size))) +  Info.IndicesOffset;  //100
							//PutUInt(ref TagBuff,GetUInt(TagBuff,(uint)(88 + (i * Size))),(uint)(32 +(i * Size)));
							//PutUInt(ref TagBuff,NewIndicesDataSize,(uint)(56 + (i * Size)));
							uint CurrentStructSize = (uint)TagBuff.Length;
							byte[] RawModelBuffer = new byte[TrueVerticesSize];
							fs.Seek((long)TrueVerticesOffset,System.IO.SeekOrigin.Begin);
							fs.Read(RawModelBuffer,0,RawModelBuffer.Length);
							FO.Write(RawModelBuffer,0,RawModelBuffer.Length);

							RawModelBuffer = new byte[TrueIndicesSize];
							fs.Seek((long)TrueIndicesOffset,System.IO.SeekOrigin.Begin);
							fs.Read(RawModelBuffer,0,RawModelBuffer.Length);
							FO.Write(RawModelBuffer,0,RawModelBuffer.Length);
							fs.Seek(fs.Position,System.IO.SeekOrigin.Begin);
							byte[] TestBuffer = new byte[6];
							fs.Read(TestBuffer,0,6);
							if (TestBuffer[4] == 0xff)
							{
								//PutUInt(StructBuffer,GetUInt(StructBuffer,56 + (StructSize * cc)) + 1,56 + (StructSize * cc));
								FO.Write(TestBuffer,0,TestBuffer.Length);
								TrueIndicesSize += 6;
							}
							fs.Seek(SaveMapFilePos,SeekOrigin.Begin);
							*/
							break;
							#endregion
						case TSFReader.TSFBSPModel:			 // 0xAC: //BSPMODEL
							#region BSPModel
							uint VOOffset = 0xB4;
							uint LOOffset = 0xC8;

							uint UnCompVertsSize = CompUtil.GetUInt(TagBuff,VOOffset + (sc * Size)) * 56;
							uint CompVertsSize   = CompUtil.GetUInt(TagBuff,VOOffset + (sc * Size)) * 32;  
							uint UnCompLMUVSize  = CompUtil.GetUInt(TagBuff,LOOffset + (sc * Size)) * 20;
							uint CompLMUVSize    = CompUtil.GetUInt(TagBuff,LOOffset + (sc * Size)) * 8;
            
							byte[] ba_UnCompVerts = new byte[UnCompVertsSize];
							byte[] ba_CompVerts   = new byte[CompVertsSize];
							byte[] ba_UnCompLMUV  = new byte[UnCompLMUVSize];
							byte[] ba_CompLMUV    = new byte[CompLMUVSize];

							FI.Read(ba_UnCompVerts,0,ba_UnCompVerts.Length);
							FI.Read(ba_UnCompLMUV,0,ba_UnCompLMUV.Length);
							FI.Read(ba_CompVerts,0,ba_CompVerts.Length);
							FI.Read(ba_CompLMUV,0,ba_CompLMUV.Length);

							FO.Write(ba_UnCompVerts,0,ba_UnCompVerts.Length);
							FO.Write(ba_UnCompLMUV,0,ba_UnCompLMUV.Length);
							FO.Write(ba_CompVerts,0,ba_CompVerts.Length);
							FO.Write(ba_CompLMUV,0,ba_CompLMUV.Length);
							break;
							#endregion
						case TSFReader.TSFResource: // 0xAD: //Resource
							#region Resource
							/*
							ushort BitmapSoundFlag = GetUShort(TagBuff,0x00 + (sc * Size));
							ushort Index = GetUShort(TagBuff,0x02 + (sc * Size));
							ushort ID1 = GetUShort(TagBuff,0x04 + (sc * Size));
							ushort ID2 = GetUShort(TagBuff,0x06 + (sc * Size));
							MapPosSave = fs.Position;
							uint Offset = Info.IndexItems[ID1].OffsetToString; ///  fileData.stringOffset.UInt - Info.MapMagic;
							fs.Seek((long)Offset,SeekOrigin.Begin);
							byte[] rscstr = new byte[256];
							fs.Read(rscstr,0,rscstr.Length);
							fs.Seek(MapPosSave,SeekOrigin.Begin);
							uint rscsize = 0;
							do
							{
								rscsize +=1;
							}while (rscstr[rscsize] != 0);
							byte[] rscOutString = new byte[rscsize + 1];
							uint rsccount = 0;
							do
							{
								rscOutString[rsccount] = rscstr[rsccount];
								rsccount +=1;
							}while (rsccount != rscsize);
							PutUShort(ref TagBuff,(uint)(0x06 + (sc * Size)),(ushort)(rscOutString.Length - 1));
							FO.Write(rscOutString,0,rscOutString.Length);
							*/
							break;
							#endregion
						case TSFReader.TSFEnd: // 0xA7: //End
							#region StructEnd
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
				if (Count !=0)
				{
					sc += 1;
					if (sc != Count)
					{
						sr.Seek(StartOfStruct);
						ExitStruct = false;
					}
				}
			}while (sc != Count );
			long tmpPosSave = FO.Position;
			FO.Seek(StructPosition,SeekOrigin.Begin);
			FO.Write(TagBuff,0,TagBuff.Length);
			FO.Seek(tmpPosSave,SeekOrigin.Begin);
		}
	}
}
