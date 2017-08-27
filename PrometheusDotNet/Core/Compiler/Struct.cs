using System;
using System.IO;
using Prometheus.Core;
//using XpertStudios.Halo;
//using XpertStudios;
using System.Diagnostics;

namespace Prometheus.Core.Compiler
{
  #region HaloCE Bitmap File
  class CE_Bitmap_File
  {
    bool Loaded;
    public STRUCT_BITMAP_FILE_HDR HDR = new STRUCT_BITMAP_FILE_HDR();
    public STRUCT_BITMAP_LOCATOR_ELEMENT[] Locator;
    public void Read(BinaryReader br)
    {
      if(Loaded == false)
      {
        br.BaseStream.Seek(0,SeekOrigin.Begin);
        HDR.Read(br);
        br.BaseStream.Position = HDR.LocatorBlockOffset;
        Locator = new STRUCT_BITMAP_LOCATOR_ELEMENT[HDR.LocatorBlockCount];
        for(int o=0; o<HDR.LocatorBlockCount; o++)
        {
          Locator[o] = new STRUCT_BITMAP_LOCATOR_ELEMENT();
          Locator[o].Read(br);
        }
        Loaded = true;
      }
    }
  }
  class STRUCT_BITMAP_FILE_HDR
  {
    public uint unk1;
    public uint NameBlockOffset;
    public uint LocatorBlockOffset;
    public uint LocatorBlockCount;
    public void Read(BinaryReader br)
    {
      unk1 = br.ReadUInt32();
      NameBlockOffset = br.ReadUInt32();
      LocatorBlockOffset = br.ReadUInt32();
      LocatorBlockCount = br.ReadUInt32();
    }
  }

  class STRUCT_BITMAP_LOCATOR_ELEMENT
  {
    public uint name_offset;
    public uint size;
    public uint offset;
    public void Read(BinaryReader br)
    {
      name_offset = br.ReadUInt32();
      size = br.ReadUInt32();
      offset = br.ReadUInt32();
    }
  }

  #endregion
	/// <summary>
	/// Summary description for Struct.
	/// </summary>
	public class Struct
	{
    static private CE_Bitmap_File CeBitmapIndex = new CE_Bitmap_File();
		string TabReplace = "";
		byte TabByte = 0x09;
		ushort Name;
		uint Size;
		//static sc = 0;
		public struct StructInfo
		{
			public uint MapMagic;
			public uint TagMagic;
			public uint MapVersion;
			public uint VerticesOffset;
			public uint IndicesOffset;
			public uint CurrentOffset;
			public uint TagHeaderSize;
			public uint CurrentIndex;
			public Decompiler.h1_Tag_Index_Item[] IndexItems;  //Map.IndexItemExpanded[] IndexItems;
			public FileStream BitmapsFile;
			public FileStream SoundsFile;
		}
  	byte[] TagBuff = new byte[0];

	public void DoProcessStruct(ref TSFReader sr,ref FileStream fs,ref MemoryStream FO,uint Count,ushort uName,ref StructInfo Info,ref string[] TagList,ref string[] StringList)
	{
		long MapPosSave;
		TabReplace += (char)TabByte;
		//string InLine;// = sr.ReadLine();
		//InLine = sr.ReadLine();
		//string[] CMD = LastInline.Replace(TabReplace,"").ToLower().Trim().Split(new char[]{' '},256);
		Name = sr.GetUName();
		Size = sr.GetSOC();
		TagBuff = new byte[Size * Count];
		fs.Read(TagBuff,0,TagBuff.Length);
		//FixPos(ref fs);
		long StructPosition = FO.Position;
		FO.Write(TagBuff,0,TagBuff.Length);
		uint StartOfStruct = sr.Position;
		uint sc = 0;
		bool ExitStruct = false;
    
    bool fuckThisShitHack = false;
    BinaryReader br = new BinaryReader(Info.BitmapsFile);
    BinaryWriter bw = new BinaryWriter(FO);
    
    do
		{
			do
			{
				//InLine = "TEST";// sr.Read();
					//sr.Read//sr.cTSF[sr.Position].CMD;   //CMD = InLine.Replace(TabReplace,"").ToLower().Trim().Split(new char[]{' '},256);
			  sr.Read();
				switch (sr.GetCMD())
				{
					case TSFReader.TSFStruct:        // 0xA0: //Struct
						#region Structure
						//if(fuckThisShitHack)
            //  break;

            uint ChildCount = GetUInt(TagBuff,(uint)(sr.GetOIP()) + (sc * Size));
						if (sr.GetUName() == 0xffff)
						{
							ChildCount = 0;
							PutUInt(ref TagBuff,(uint)(sr.GetOIP()) + (sc * Size),0);
						}
						if (ChildCount != 0)
						{
							uint CSOffset = GetUInt(TagBuff,(uint)(sr.GetOIP()) + 4 + (sc * Size)) - Info.TagMagic;
							Struct Child = new Struct();
							fs.Seek((long)CSOffset,SeekOrigin.Begin);
							PutUInt(ref TagBuff,(uint)(sr.GetOIP() + 4 + (sc * Size)),(uint)FO.Position - Info.TagHeaderSize);
							Child.DoProcessStruct(ref sr,ref fs,ref FO,ChildCount,sr.GetUName(),ref Info,ref TagList,ref StringList);
						}else{
							sr.SeekAheadTo(0xA7,sr.GetUName());
							//sr.Seek(sr.Position + 1);
						}
						break;
						#endregion
					case TSFReader.TSFBSPTagRef:     // 0xA8: //BSPTagRef
					case TSFReader.TSFTagRef:        // 0xA1: //TagRef
						#region TagRef
						uint StringOffset = GetUInt(TagBuff,(uint)(sr.GetOIP() + (sc * Size)) + 0x04) - Info.MapMagic;
						ushort TagIndex = GetUShort(TagBuff,(uint)(sr.GetOIP() + (sc * Size)) + 0x0C);
						if (GetUInt(TagBuff,(uint)(sr.GetOIP() + (sc * Size)) + 0x04) != 0)
						{
							//Info.IndexItems[TagIndex].tagClass[0].Char.CopyTo(TagBuff,(long)(Val(CMD[1]) + (sc * Size)));
							
							PutUInt(ref TagBuff,(sr.GetOIP() + (sc * Size)),Info.IndexItems[TagIndex].Type1);//.fileData.tagClass[0].UInt);
							SwapInt(ref TagBuff,(sr.GetOIP() + (sc * Size)));
							MapPosSave = fs.Position;
							fs.Seek((long)StringOffset,SeekOrigin.Begin);
							byte[] tmpstr = new byte[256];
							fs.Read(tmpstr,0,tmpstr.Length);
							fs.Seek(MapPosSave,SeekOrigin.Begin);
							uint size = 0;
							do
							{
								size +=1;
							}while (tmpstr[size] != 0);
							byte[] OutString = new byte[size + 1];
							uint count = 0;
							do
              {
                OutString[count] = tmpstr[count];
                count +=1;
              }while (count != size);
              PutUInt(ref TagBuff,(uint)((sr.GetOIP() + (sc * Size)) + 0x04),(uint)FO.Position - Info.TagHeaderSize);
              PutUInt(ref TagBuff,(uint)((sr.GetOIP() + (sc * Size)) + 0x08),(uint)OutString.Length -1);
              PutUInt(ref TagBuff,(uint)((sr.GetOIP() + (sc * Size)) + 0x0C),(uint)0xffffffff);
              FO.Write(OutString,0,OutString.Length);
            }
            break;
						#endregion
          case TSFReader.TSFInternalRaw:   // 0xA3: //InternalRaw
						#region InternalRaw
            uint RawSize = GetUInt(TagBuff,(uint)(sr.GetOIP() + (sc * Size)));
            uint RawOffset = GetUInt(TagBuff,(uint)(sr.GetOIP() + 0x0c + (sc * Size))) - Info.TagMagic;
            fs.Seek((long)RawOffset,SeekOrigin.Begin);
            byte[] tmp = new byte[RawSize];
            //PutUInt(ref TagBuff,(uint)(Val(CMD[2]) + (sc * Size)),(uint)(FO.Position));
            fs.Read(tmp,0,tmp.Length);
            FixPos(ref fs);
            FO.Write(tmp,0,tmp.Length);
            tmp = null;
            break;
						#endregion
          case TSFReader.TSFBitmapRaw:     // 0xA9: //Bitmapraw
						#region BitmapRaw
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

						break;
						#endregion
					case TSFReader.TSFSoundData:     // 0xAA: //SoundData
						#region SoundData
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
						break;
						#endregion
					case TSFReader.TSFRawXModelData: // 0xB0: //XModelData
						#region XModelData
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
						break;
						#endregion
					case TSFReader.TSFModelData:     // 0xAB: //Modeldata
						#region ModelData
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
						break;
						#endregion
					case TSFReader.TSFBSPModel: // 0xAC: //BSPMODEL
						#region BSPModel
						uint VOOffset = 0xB4;
						uint LOOffset = 0xC8;

						s_TrueOffset VertsTrueOffset = new s_TrueOffset();
						s_TrueOffset LMUVTrueOffset  = new s_TrueOffset();

						VertsTrueOffset.Read(TagBuff,VOOffset + (sc * Size),Info.TagMagic,ref fs);
						LMUVTrueOffset.Read( TagBuff,LOOffset + (sc * Size),Info.TagMagic,ref fs);

						uint UnCompVertsSize = VertsTrueOffset.Count * 56;
            uint CompVertsSize   = VertsTrueOffset.Count * 32;  
						uint UnCompLMUVSize  = LMUVTrueOffset.Count  * 20;
						uint CompLMUVSize    = LMUVTrueOffset.Count  * 8;
            
						byte[] ba_UnCompVerts = new byte[UnCompVertsSize];
            byte[] ba_CompVerts   = new byte[CompVertsSize];
						byte[] ba_UnCompLMUV  = new byte[UnCompLMUVSize];
						byte[] ba_CompLMUV    = new byte[CompLMUVSize];

						switch (Info.MapVersion)
						{
							case 5:
								long Sav_F_Pos = fs.Position; 
								fs.Seek((long)VertsTrueOffset.TrueOffset,SeekOrigin.Begin);
								fs.Read(ba_CompVerts,0,ba_CompVerts.Length);
								fs.Seek((long)LMUVTrueOffset.TrueOffset, SeekOrigin.Begin);
								fs.Read(ba_CompLMUV, 0,ba_CompLMUV.Length);
								fs.Seek(Sav_F_Pos,SeekOrigin.Begin);
								break;
							case 7:
              case 0x261:
                fs.Read(ba_UnCompVerts,0,ba_UnCompVerts.Length);
								fs.Read(ba_UnCompLMUV,0,ba_UnCompLMUV.Length);
								break;
						}

						cs_UnCompVerts[] UnCompVertsBuff = new cs_UnCompVerts[VertsTrueOffset.Count];
						cs_CompVerts[]   CompVertsBuff   = new cs_CompVerts[  VertsTrueOffset.Count];
						cs_UnCompLMUV[]  UnCompLMUV      = new cs_UnCompLMUV[ LMUVTrueOffset.Count];
						cs_CompLMUV[]    CompLMUV        = new cs_CompLMUV[   LMUVTrueOffset.Count];
						
						for (int vl = 0; vl < VertsTrueOffset.Count; vl++)
						{
							CompVertsBuff[vl] = new cs_CompVerts();
							UnCompVertsBuff[vl] = new cs_UnCompVerts();
							switch (Info.MapVersion)
							{
								case 5:
									CompVertsBuff[vl].Read(ba_CompVerts,(uint)vl);
									break;
								case 7:
                case 0x261:
                  UnCompVertsBuff[vl].Read(ba_UnCompVerts,(uint)vl);
									break;
							}
						}
						switch (Info.MapVersion)
						{
							case 5:
								DeCompressVerts(CompVertsBuff,ref UnCompVertsBuff);
								break;
							case 7:
              case 0x261:
                CompressVerts(UnCompVertsBuff,ref CompVertsBuff);
								break;
						}
						for (int i = 0;i < VertsTrueOffset.Count;i++)
						{
							switch (Info.MapVersion)
							{
								case 5:
									UnCompVertsBuff[i].Write(ref ba_UnCompVerts,(uint)i);
									break;
								case 7:
                case 0x261:
                  CompVertsBuff[i].Write(ref ba_CompVerts,(uint)i);
									break;
							}
						}
						for (int i = 0;i < LMUVTrueOffset.Count; i++)
						{
							CompLMUV[i]   = new cs_CompLMUV();
							UnCompLMUV[i] = new cs_UnCompLMUV();
							switch (Info.MapVersion)
							{
								case 5:
									CompLMUV[i].Read(ba_CompLMUV,(uint)i);
									break;
								case 7:
                case 0x261:
                  UnCompLMUV[i].Read(ba_UnCompLMUV,(uint)i);
									break;
							}
						}
						switch (Info.MapVersion)
						{
							case 5:
								DeCompressLMUV(CompLMUV,ref UnCompLMUV);
								break;
							case 7:
              case 0x261:
                CompressLMUV(UnCompLMUV,ref CompLMUV);
								break;
						}
						for (int i = 0;i < LMUVTrueOffset.Count; i++)
						{
							switch (Info.MapVersion)
							{
								case 5:
									UnCompLMUV[i].Write(ref ba_UnCompLMUV,(uint)i);
									break;
								case 7:
                case 0x261:
                  CompLMUV[i].Write(ref ba_CompLMUV,(uint)i);
									break;
							}
						}
						PutUInt(ref TagBuff,0xd8 + (sc * Size),(uint)ba_UnCompVerts.Length);

						FO.Write(ba_UnCompVerts,0,ba_UnCompVerts.Length);
						FO.Write(ba_UnCompLMUV,0,ba_UnCompLMUV.Length);
						FO.Write(ba_CompVerts,0,ba_CompVerts.Length);
						FO.Write(ba_CompLMUV,0,ba_CompLMUV.Length);
						break;
						#endregion
					case TSFReader.TSFResource: // 0xAD: //Resource
						#region Resource
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
		public static void CompressVerts(cs_UnCompVerts[] UnCompVerts,ref cs_CompVerts[] CompVerts)
		{
			for(int i = 0;i<UnCompVerts.Length;i++)
			{
				CompVerts[i].XYZCords = UnCompVerts[i].XYZCords;
				float[] Normals = new float[3];
				float[] BiNormals = new float[3];
				float[] Tangents = new float[3];
				for(int n = 0;n<3;n++)
				{
					Normals[n] = BitConverter.ToSingle(BitConverter.GetBytes(UnCompVerts[i].Normals[n]),0);
					BiNormals[n] = BitConverter.ToSingle(BitConverter.GetBytes(UnCompVerts[i].BiNormals[n]),0);
					Tangents[n] = BitConverter.ToSingle(BitConverter.GetBytes(UnCompVerts[i].Tangents[n]),0);
				}
				CompVerts[i].Normals = CompressVector(Normals);
				CompVerts[i].BiNormals = CompressVector(BiNormals);
				CompVerts[i].Tangents = CompressVector(Tangents);
				CompVerts[i].UV = UnCompVerts[i].UV;
			}
		}

		public static void DeCompressVerts(cs_CompVerts[] CompVerts,ref cs_UnCompVerts[] UnCompVerts)
		{
			for(int i = 0;i<CompVerts.Length;i++)
			{
				UnCompVerts[i].XYZCords = CompVerts[i].XYZCords;
				float[] Normals = new float[3];
				float[] BiNormals = new float[3];
				float[] Tangents = new float[3];
				DecompressIntToVector(CompVerts[i].Normals,ref Normals);
				DecompressIntToVector(CompVerts[i].BiNormals,ref BiNormals);
				DecompressIntToVector(CompVerts[i].Tangents,ref Tangents);
				for (int n = 0;n<3;n++)
					UnCompVerts[i].Normals[n] = BitConverter.ToUInt32(BitConverter.GetBytes(Normals[n]),0);
				for (int bn = 0;bn<3;bn++)
					UnCompVerts[i].BiNormals[bn] = BitConverter.ToUInt32(BitConverter.GetBytes(BiNormals[bn]),0);
				for (int t = 0;t<3;t++)
					UnCompVerts[i].Tangents[t] = BitConverter.ToUInt32(BitConverter.GetBytes(Tangents[t]),0);
				UnCompVerts[i].UV = CompVerts[i].UV;
			}
		}
		public static void CompressLMUV(cs_UnCompLMUV[] UnCompLMUV,ref cs_CompLMUV[] CompLMUV)
		{
			for(int i = 0;i<CompLMUV.Length;i++)
			{
				CompLMUV[i].Normal = CompressVector(UnCompLMUV[i].Normal);
				CompLMUV[i].UV[0] = CompressFloatToShort(UnCompLMUV[i].UV[0]);
				CompLMUV[i].UV[1] = CompressFloatToShort(UnCompLMUV[i].UV[1]);

			}
		}
		public static void DeCompressLMUV(cs_CompLMUV[] CompLMUV,ref cs_UnCompLMUV[] UnCompLMUV)
		{
			for(int i = 0;i<UnCompLMUV.Length;i++)
			{
				DecompressIntToVector(CompLMUV[i].Normal,ref UnCompLMUV[i].Normal);
				UnCompLMUV[i].UV[0] = DecompressShortToFloat(CompLMUV[i].UV[0]);
				UnCompLMUV[i].UV[1] = DecompressShortToFloat(CompLMUV[i].UV[1]);
			}
		}
		public static uint CompressVector(Single[] pVector)
		{
			Single cx, cy, cz;
			int vector_x_int, vector_y_int, vector_z_int;
			int ReturnVal;
			cx = pVector[0];
			cy = pVector[1];
			cz = pVector[2];

			if(cx > 1.0)cx = (float)1.0;
			if(cx < -1.0)cx = (float)-1.0;

			if(cy > 1.0)cy = (float)1.0;
			if(cy < -1.0)cy = (float)-1.0;

			if(cz > 1.0)cz = (float)1.0;
			if(cz < -1.0)cz = (float)-1.0;

			vector_x_int = ((int)Math.Floor(cx*1023.5)) & 0x7FF;
			vector_y_int = ((int)Math.Floor(cy*1023.5)) & 0x7FF;
			vector_z_int = ((int)Math.Floor(cz*511.5)) & 0x3FF;
			ReturnVal = (((vector_y_int | (vector_z_int << 11)) << 11) | vector_x_int);
			return (uint)ReturnVal;
		}

		public static short CompressFloatToShort(Single input)
		{
			int temp;
			short output;
			if(input > 1.0)
				input = (float)1.0;
			if(input < -1.0)
				input = (float)-1.0;

			temp = (int)Math.Floor(input*32767.5);

			output = (short)temp;

			return (short)output;
		}

		public static float DecompressShortToFloat(short comp_vector)
    {
		 int temp;

		 temp = comp_vector + comp_vector;

		 float decomp_val;

		 decomp_val = (float)((((float)temp) + 1)/65535.0);

		 return(decomp_val);
	  }

		public static void DecompressIntToVector(uint comp_vector, ref float[] pVector)
		{
			int cx, cy, cz;

			cx = (int)(comp_vector << 21);
			cy = (int)(( comp_vector >> 11 ) << 21);
			cz = (int)(( comp_vector >> 22 ) << 22);
 
			pVector[0] = (float)((((float)cx/1048576.0) + 1.0)/2047.0);
			pVector[1] = (float)((((float)cy/1048576.0) + 1.0)/2047.0);
			pVector[2] = (float)((((float)cz/1048576.0) + 1.0)/2047.0);

			//float mag = sqrt(pVector[0]*pVector[0] + pVector[1]*pVector[1] + pVector[2]*pVector[2]);
		}
		public static void GetUInt(byte[] data,uint offset,ref uint Value)
		{
			Value = BitConverter.ToUInt32(data,(int)offset);
		}
		public static uint GetUInt(byte[] data,uint offset)
		{
			if ((data.Length - 4) >= offset)
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
		public static void GetShort(byte[] data,uint offset,ref short Value)
		{
			Value = BitConverter.ToInt16(data,(int)offset);
		}
		public static short GetShort(byte[] data,uint offset)
		{
			return BitConverter.ToInt16(data,(int)offset);
		}
		public static void PutUShort(ref byte[] data,uint offset,ushort Value)
		{
			byte[] tmp = BitConverter.GetBytes(Value);
			tmp.CopyTo(data,(long)offset);
		}
		public static void PutShort(ref byte[] data,uint offset,short Value)
		{
			byte[] tmp = BitConverter.GetBytes(Value);
			tmp.CopyTo(data,(long)offset);
		}
		public static void GetFloat(byte[] data,uint offset,ref float Value)
		{
			Value = BitConverter.ToSingle(data,(int)offset);
		}
		public static void PutFloat(ref byte[] data,uint offset,float Value)
		{
			byte[] tmp = BitConverter.GetBytes(Value);
			tmp.CopyTo(data,(long)offset);
		}
		private string GetTagClass(byte[] Array,uint Offset)
		{
			string NewString = "";
			for (uint count = 0; count < 4; count +=1)
			{
				NewString += (char)Array[count + Offset];
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
		private void SwapInt(ref byte[] Array,uint Offset)
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
		private void SwapShort(ref byte[] Array,uint Offset)
		{
			if (Array.Length - 2  >= Offset)
			{
				byte st;
				st = Array[Offset +1];
				Array[Offset+1] = Array[Offset];
				Array[Offset] = st;
			}
		}
		private void FixPos(ref FileStream FS)
		{
			if ((FS.Position % 0x4) != 0)
			{
				uint Size = (0x4 - ((uint)FS.Position % 0x4));
				byte[] tmp = new byte[Size];
				FS.Read(tmp,0,tmp.Length);
				tmp = null;
			}
		}
		public Struct()
		{
		}
	}
	public struct s_TrueOffset
	{
		public uint Count;
		public uint Zeros;
		public uint CheckSum;
		public uint IndirectOffset;
		public uint TrueOffset;
		public uint Read(byte[] Buffer,uint Offset,uint Magic,ref FileStream FI)
		{
			Struct.GetUInt(Buffer,Offset,ref Count);
			Struct.GetUInt(Buffer,Offset + 0x04,ref Zeros);
      Struct.GetUInt(Buffer,Offset + 0x08,ref CheckSum);
			Struct.GetUInt(Buffer,Offset + 0x0C,ref IndirectOffset);
			IndirectOffset -= Magic;
			long Sav_F_Pos = FI.Position;
			FI.Seek(IndirectOffset,SeekOrigin.Begin);
			byte[] Tmp = new byte[12];
			FI.Read(Tmp,0,Tmp.Length);
			FI.Seek(Sav_F_Pos,SeekOrigin.Begin);
			Struct.GetUInt(Tmp,4,ref TrueOffset);
			TrueOffset -= Magic;
			return TrueOffset;
    }
	}

	public class cs_CompVerts
	{
		public uint[] XYZCords;
		public uint   Normals;
		public uint   BiNormals;
		public uint   Tangents;
		public uint[] UV;
		public cs_CompVerts()
		{
			XYZCords = new uint[3];
			UV       = new uint[2];
		}
		public void Read(byte[] Buffer,uint Offset)
		{
			Offset   = Offset * 32;
			XYZCords = new uint[3];
			UV       = new uint[2];
			Struct.GetUInt(Buffer,Offset + 0x00,ref XYZCords[0]);
			Struct.GetUInt(Buffer,Offset + 0x04,ref XYZCords[1]);
			Struct.GetUInt(Buffer,Offset + 0x08,ref XYZCords[2]);
			Struct.GetUInt(Buffer,Offset + 0x0C,ref Normals);
			Struct.GetUInt(Buffer,Offset + 0x10,ref BiNormals);
			Struct.GetUInt(Buffer,Offset + 0x14,ref Tangents);
			Struct.GetUInt(Buffer,Offset + 0x18,ref UV[0]);
			Struct.GetUInt(Buffer,Offset + 0x1C,ref UV[1]);
		}
		public void Write(ref byte[] Buffer,uint Offset)
		{
			Offset = Offset * 32;
			Struct.PutUInt(ref Buffer,Offset + 0x00,XYZCords[0]);
			Struct.PutUInt(ref Buffer,Offset + 0x04,XYZCords[1]);
			Struct.PutUInt(ref Buffer,Offset + 0x08,XYZCords[2]);
			Struct.PutUInt(ref Buffer,Offset + 0x0C,Normals);
			Struct.PutUInt(ref Buffer,Offset + 0x10,BiNormals);
			Struct.PutUInt(ref Buffer,Offset + 0x14,Tangents);
			Struct.PutUInt(ref Buffer,Offset + 0x18,UV[0]);
			Struct.PutUInt(ref Buffer,Offset + 0x1C,UV[1]);
		}
	}
	public class cs_UnCompVerts
	{
		public uint[] XYZCords;
		public uint[] Normals;
		public uint[] BiNormals;
		public uint[] Tangents;
		public uint[] UV;
		public cs_UnCompVerts()
		{
			XYZCords  = new uint[3];
			Normals   = new uint[3];
			BiNormals = new uint[3];
			Tangents  = new uint[3];
			UV        = new uint[2];
		}
		public void Read(byte[] Buffer,uint Offset)
		{
			Offset = Offset * 56;
			Struct.GetUInt(Buffer,Offset + 0x00,ref XYZCords[0]);
			Struct.GetUInt(Buffer,Offset + 0x04,ref XYZCords[1]);
			Struct.GetUInt(Buffer,Offset + 0x08,ref XYZCords[2]);
			Struct.GetUInt(Buffer,Offset + 0x0C,ref Normals[0]);
			Struct.GetUInt(Buffer,Offset + 0x10,ref Normals[1]);
			Struct.GetUInt(Buffer,Offset + 0x14,ref Normals[2]);
			Struct.GetUInt(Buffer,Offset + 0x18,ref BiNormals[0]);
			Struct.GetUInt(Buffer,Offset + 0x1c,ref BiNormals[1]);
			Struct.GetUInt(Buffer,Offset + 0x20,ref BiNormals[2]);
			Struct.GetUInt(Buffer,Offset + 0x24,ref Tangents[0]);
			Struct.GetUInt(Buffer,Offset + 0x28,ref Tangents[1]);
			Struct.GetUInt(Buffer,Offset + 0x2C,ref Tangents[2]);
			Struct.GetUInt(Buffer,Offset + 0x30,ref UV[0]);
			Struct.GetUInt(Buffer,Offset + 0x34,ref UV[1]);
		}
		public void Write(ref byte[] Buffer,uint Offset)
		{
			Offset = Offset * 56;
			Struct.PutUInt(ref Buffer,Offset + 0x00,XYZCords[0]);
			Struct.PutUInt(ref Buffer,Offset + 0x04,XYZCords[1]);
			Struct.PutUInt(ref Buffer,Offset + 0x08,XYZCords[2]);
			Struct.PutUInt(ref Buffer,Offset + 0x0C,Normals[0]);
			Struct.PutUInt(ref Buffer,Offset + 0x10,Normals[1]);
			Struct.PutUInt(ref Buffer,Offset + 0x14,Normals[2]);
			Struct.PutUInt(ref Buffer,Offset + 0x18,BiNormals[0]);
			Struct.PutUInt(ref Buffer,Offset + 0x1C,BiNormals[1]);
			Struct.PutUInt(ref Buffer,Offset + 0x20,BiNormals[2]);
			Struct.PutUInt(ref Buffer,Offset + 0x24,Tangents[0]);
			Struct.PutUInt(ref Buffer,Offset + 0x28,Tangents[1]);
			Struct.PutUInt(ref Buffer,Offset + 0x2C,Tangents[2]);
			Struct.PutUInt(ref Buffer,Offset + 0x30,UV[0]);
			Struct.PutUInt(ref Buffer,Offset + 0x34,UV[1]);
		}
	}
	public class cs_CompLMUV
	{
		public uint Normal;
		public short[] UV;
		public cs_CompLMUV()
		{
			UV = new short[2];
		}
		public void Read(byte[] Buffer,uint Offset)
		{
			Offset = Offset * 8;
			UV = new short[2];
			Struct.GetUInt( Buffer,Offset + 0x00,ref Normal);
			Struct.GetShort(Buffer,Offset + 0x04,ref UV[0]);
			Struct.GetShort(Buffer,Offset + 0x06,ref UV[1]);
		}
		public void Write(ref byte[] Buffer,uint Offset)
		{
			Offset = Offset * 8;
			Struct.PutUInt( ref Buffer,Offset + 0x00,Normal);
			Struct.PutShort(ref Buffer,Offset + 0x04,UV[0]);
			Struct.PutShort(ref Buffer,Offset + 0x06,UV[1]);
		}
	}
	public class cs_UnCompLMUV
	{
		public float[] Normal;
		public float[] UV;
		public cs_UnCompLMUV()
		{
			Normal = new float[3];
			UV = new float[2];
		}
		public void Read(byte[] Buffer,uint Offset)
		{
			Offset = Offset * 20;
			Struct.GetFloat(Buffer,Offset + 0x00,ref Normal[0]);
			Struct.GetFloat(Buffer,Offset + 0x04,ref Normal[1]);
			Struct.GetFloat(Buffer,Offset + 0x08,ref Normal[2]);
			Struct.GetFloat(Buffer,Offset + 0x0C,ref UV[0]);
			Struct.GetFloat(Buffer,Offset + 0x10,ref UV[1]);
		}
		public void Write(ref byte[] Buffer,uint Offset)
		{
			Offset = Offset * 20;
			Struct.PutFloat(ref Buffer,Offset + 0x00,Normal[0]);
			Struct.PutFloat(ref Buffer,Offset + 0x04,Normal[1]);
			Struct.PutFloat(ref Buffer,Offset + 0x08,Normal[2]);
			Struct.PutFloat(ref Buffer,Offset + 0x0C,UV[0]);
			Struct.PutFloat(ref Buffer,Offset + 0x10,UV[1]);
		}
	}

	public abstract class BSPVertsBase 
	{
		public abstract void GenVerts(ref byte[] VertBuff,ref byte[] CompVertBuff);
	
		public class BSPVerts : BSPVertsBase
		{
			public override void GenVerts(ref byte[] VertBuff,ref byte[] CompVertBuff)
			{
			}
		}
		public class XBSPVerts : BSPVertsBase
		{
			public override void GenVerts(ref byte[] VertBuff,ref byte[] CompVertBuff)
			{
			}
		}
	}
}
