using System;
using System.IO;
using Microsoft.VisualBasic;
namespace Prometheus.Core
{
	/// <summary>
	/// Summary description for Struct.
	/// </summary>

	public class Structure2
	{
		public struct Vectors
		{
			public double x;
			public double y;
			public double z;
		}
		uint StructRef;
		uint TagRefIndex = 0;
		uint[] TagRefParentIndex = new uint[512];
		uint[] TagRefChunkCountIndex = new uint[512];
		string[] TagRefStrings = new string[512];
		string TabReplace = "";
		byte TabByte = 0x09;
		public Structure2()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public void StructureCS(uint MetaOffset,uint MagIndex,uint OffsetMagic,uint MapMagic,string[] StructureArray,uint ChunkCount,FileStream MapFile,MemoryStream TagFile,FileStream BitmapFile,FileStream SoundFile,uint VerticesOffset,uint IndicesOffset,uint MapVersion)
		{
			TabReplace += (char)TabByte;
			uint StructSize;
			uint TestInc = 0;
			uint StructOffset;
			uint MapFilePosSave;
			uint ChildChunkCount;
			uint PrevSize =0;
			uint BMAPtrigger = 0;
			uint NewIndexSize = 0;
			uint TagRefOffset = 0;
			uint TagRefStringOffset = 0;
			uint TagRefBufferOldSize = 0;
			uint TagRefBufferNewSize = 0;
			uint AfterStructChunkCount =0;
			byte[] TagRefBuffer = new byte[0];
			byte[] MapStringBuffer = new byte[1024];
			byte[] RawAnimationsBuffer = new byte[0];
			byte[] RawAnimationEditBuffer = new byte[0];
			byte[] RawBuffer = new byte[0];
			byte[] BSPVerticesData = new byte[0];
			string TestHelpString;
			string TagRefString;
			string[] CMD = new string[256];
			string[] TestString = new string[256];
			string[] NewStructureArray = new string[256];
			CMD = StructureArray[0].Split(new char[]{' '},256);
			StructSize = Val(CMD[3]);
			byte[] StructBuffer = new byte[StructSize * ChunkCount];
			byte[] OffsetBuffer = new byte[StructSize * ChunkCount];
			StructOffset = (uint)TagFile.Position;
			string StructureName = CMD[2];
			MapFile.Read(StructBuffer,0,(int)StructBuffer.Length);
			uint StructSavePos = (uint)MapFile.Position;
			StructBuffer.CopyTo(OffsetBuffer,0);
			uint BufferSize = (uint)StructBuffer.Length;
			StructRef = 0;
			for (uint cc = 0;cc < ChunkCount; cc++)
			{
				
				for (uint ee = 0;ee < MagIndex; ee++)
				{
					CMD = StructureArray[ee].Split(new char[]{' '},256);
					
					switch (CMD[0])
					{
						case "struct":
							
							if (CMD[2] != StructureName)
							{
								AfterStructChunkCount = GetUInt(OffsetBuffer,Val(CMD[1]) + (cc * StructSize));
								if (AfterStructChunkCount != 0)
								{
									StructRef +=1;
								}
								do 
								{
									
									TestString =  StructureArray[TestInc + ee].Split(new char[]{' '},256);
									if(TestString.Length > 1)
									{
										TestHelpString = TestString[1];
									}
									else
									{
										TestHelpString = "";
									}
									TestInc += 1;
								}while (CMD[2] != TestHelpString);
								ee = ee + (TestInc - 1);
								TestInc = 0;
							}	
							break;
						case "tagref":
							TagRefOffset = Val(CMD[1]);
							TagRefStringOffset = GetUInt(StructBuffer,(TagRefOffset + 4) + (StructSize * cc));
							if (TagRefStringOffset != 0)
							{
								TagRefStringOffset = TagRefStringOffset - MapMagic;
								MapFilePosSave = (uint)MapFile.Position;
								MapFile.Seek((long)TagRefStringOffset,System.IO.SeekOrigin.Begin);
								MapFile.Read(MapStringBuffer,0,512);
								MapFile.Seek((long)MapFilePosSave,System.IO.SeekOrigin.Begin);

								TagRefString = ReadString(MapStringBuffer);
								TagRefStrings[TagRefIndex] = TagRefString;
								TagRefParentIndex[TagRefIndex] = StructRef;
								TagRefChunkCountIndex[TagRefIndex] = cc;
								TagRefIndex = TagRefIndex + 1;

								PutUInt(StructBuffer,(uint)TagRefString.Length,(TagRefOffset + 8) + (StructSize * cc));
								PutUInt(StructBuffer,0,(TagRefOffset + 4) + (StructSize * cc));
								PutUInt(StructBuffer,4294967295,(TagRefOffset + 12) + (StructSize * cc));

								if (StructRef == 0)
								{
									TagRefBufferOldSize = (uint)TagRefBuffer.Length;
									TagRefBufferNewSize = TagRefBufferOldSize + (uint)TagRefString.Length + 1;
									Redim(ref TagRefBuffer,TagRefBufferOldSize,TagRefBufferNewSize);
									TagRefBuffer = AppendToByteArray(TagRefBuffer,TagRefString,TagRefBufferOldSize);
								}
							}
							break;
						case "vertices":
							uint UnCompressedVerts = Val(CMD[1]);
							uint CompressedVerts = Val(CMD[3]);
							long SaveFilePos = MapFile.Position;

							TagRefOffset = 0;
							TagRefStringOffset = GetUInt(StructBuffer,(TagRefOffset + 4) + (StructSize * cc));
							if (TagRefStringOffset != 0)
							{
								TagRefStringOffset = TagRefStringOffset - MapMagic;
								MapFilePosSave = (uint)MapFile.Position;
								MapFile.Seek((long)TagRefStringOffset,System.IO.SeekOrigin.Begin);
								MapFile.Read(MapStringBuffer,0,512);
								MapFile.Seek((long)MapFilePosSave,System.IO.SeekOrigin.Begin);
								TagRefString = ReadString(MapStringBuffer);
								PutUInt(StructBuffer,(uint)TagRefString.Length,(TagRefOffset + 8) + (StructSize * cc));
								PutUInt(StructBuffer,0,(TagRefOffset + 4) + (StructSize * cc));
								PutUInt(StructBuffer,4294967295,(TagRefOffset + 12) + (StructSize * cc));
								uint BSPVerticesDataOldSize = (uint)BSPVerticesData.Length;
								uint BSPVerticesDataNewSize = BSPVerticesDataOldSize + (uint)TagRefString.Length + 1;
								Redim(ref BSPVerticesData,BSPVerticesDataOldSize,BSPVerticesDataNewSize);
								BSPVerticesData = AppendToByteArray(BSPVerticesData,TagRefString,BSPVerticesDataOldSize);
							}							
							uint TrueVertCount = GetUInt(StructBuffer,180 + (StructSize * cc));
							uint TrueLightMapDataCount = GetUInt(StructBuffer,200 + (StructSize * cc));
							uint UnCompressedLightMapData = (TrueLightMapDataCount * 20);
							uint CompressedLightMapData = (TrueLightMapDataCount * 8);
							UnCompressedVerts = (TrueVertCount * 56);
							CompressedVerts = (TrueVertCount * 32);

							PutUInt(StructBuffer,CompressedVerts + CompressedLightMapData,Val(CMD[3]) + (StructSize * cc));
							
							byte[] BspVertsUnCompressed = new byte[UnCompressedVerts];
							byte[] BspVertsCompressed = new byte[CompressedVerts];
							byte[] BspLightMapDataUnCompressed = new byte[UnCompressedLightMapData];
							byte[] BspLightMapDataCompressed = new byte[CompressedLightMapData];

							MapFile.Read(BspVertsUnCompressed,0,BspVertsUnCompressed.Length);
							MapFile.Read(BspLightMapDataUnCompressed,0,BspLightMapDataUnCompressed.Length);
							
							////////////////////////////////////////////////////////////////////////
							///Now Compress the Verts and move them in to the compressed ByteArray//
							////////////////////////////////////////////////////////////////////////
							Single[] Vectors = new Single[3];
							Single[] UVs = new Single[2];
							for (uint commov = 0;commov < TrueVertCount; commov +=1)
							{
								PutUInt(BspVertsCompressed,GetUInt(BspVertsUnCompressed,(4 * 0) + (56 * commov)),(4 * 0) + (32 * commov)); //Moving X
								PutUInt(BspVertsCompressed,GetUInt(BspVertsUnCompressed,(4 * 1) + (56 * commov)),(4 * 1) + (32 * commov)); //Moving Y
								PutUInt(BspVertsCompressed,GetUInt(BspVertsUnCompressed,(4 * 2) + (56 * commov)),(4 * 2) + (32 * commov)); //Moving Z
								Vectors[0] = BitConverter.ToSingle(BspVertsUnCompressed,(int)((4 * 3) + (56 * commov))); // Loading normals
								Vectors[1] = BitConverter.ToSingle(BspVertsUnCompressed,(int)((4 * 4) + (56 * commov)));
								Vectors[2] = BitConverter.ToSingle(BspVertsUnCompressed,(int)((4 * 5) + (56 * commov)));
								PutUInt(BspVertsCompressed,CompressVector(Vectors),(4 * 3) + (32 * commov)); // compressing normals
								Vectors[0] = BitConverter.ToSingle(BspVertsUnCompressed,(int)((4 * 6) + (56 * commov))); // Loading BiNormals
								Vectors[1] = BitConverter.ToSingle(BspVertsUnCompressed,(int)((4 * 7) + (56 * commov)));
								Vectors[2] = BitConverter.ToSingle(BspVertsUnCompressed,(int)((4 * 8) + (56 * commov)));
								PutUInt(BspVertsCompressed,CompressVector(Vectors),(4 * 4) + (32 * commov)); // compressing binormals
								Vectors[0] = BitConverter.ToSingle(BspVertsUnCompressed,(int)((4 * 9) + (56 * commov))); // Loading Tangents
								Vectors[1] = BitConverter.ToSingle(BspVertsUnCompressed,(int)((4 * 10) + (56 * commov)));
								Vectors[2] = BitConverter.ToSingle(BspVertsUnCompressed,(int)((4 * 11) + (56 * commov)));
								PutUInt(BspVertsCompressed,CompressVector(Vectors),(4 * 5) + (32 * commov)); // compressing tangents
								PutUInt(BspVertsCompressed,GetUInt(BspVertsUnCompressed,(4 * 12) + (56 * commov)),(4 * 6) + (32 * commov)); // moveing uvs
								PutUInt(BspVertsCompressed,GetUInt(BspVertsUnCompressed,(4 * 13) + (56 * commov)),(4 * 7) + (32 * commov));
							}
							for (uint lcommov = 0;lcommov < TrueLightMapDataCount; lcommov +=1)
							{
								Vectors[0] = BitConverter.ToSingle(BspLightMapDataUnCompressed,(int)((4 * 0) + (20 * lcommov)));
								Vectors[1] = BitConverter.ToSingle(BspLightMapDataUnCompressed,(int)((4 * 1) + (20 * lcommov)));
								Vectors[2] = BitConverter.ToSingle(BspLightMapDataUnCompressed,(int)((4 * 2) + (20 * lcommov)));
								UVs[0] = BitConverter.ToSingle(BspLightMapDataUnCompressed,(int)((4 * 3) + (20 * lcommov)));
								UVs[1] = BitConverter.ToSingle(BspLightMapDataUnCompressed,(int)((4 * 4) + (20 * lcommov)));
								PutUInt(BspLightMapDataCompressed,CompressVector(Vectors),(4 * 0) + (8 * lcommov));
								PutUShort(BspLightMapDataCompressed,(ushort)CompressFloatToShort(UVs[0]),(4 * 1) + (8 * lcommov));
								PutUShort(BspLightMapDataCompressed,(ushort)CompressFloatToShort(UVs[1]),((4 * 1) + (8 * lcommov)+2));
							}
							//////////////////////////////////////////////////////////////////////////////
							///Now Move all the Compressed and uncompressed data in to the output array///
							//////////////////////////////////////////////////////////////////////////////
							uint OldBspVertsSize = (uint)BSPVerticesData.Length;
							Redim(ref BSPVerticesData,(uint)BSPVerticesData.Length,(uint)BSPVerticesData.Length + (uint)BspVertsUnCompressed.Length);
							AppendByteArrayToByteArray(ref BSPVerticesData,BspVertsUnCompressed,OldBspVertsSize);
							OldBspVertsSize = (uint)BSPVerticesData.Length;
							Redim(ref BSPVerticesData,(uint)BSPVerticesData.Length,(uint)BSPVerticesData.Length + (uint)BspLightMapDataUnCompressed.Length);
							AppendByteArrayToByteArray(ref BSPVerticesData,BspLightMapDataUnCompressed,OldBspVertsSize);
							OldBspVertsSize = (uint)BSPVerticesData.Length;
							Redim(ref BSPVerticesData,(uint)BSPVerticesData.Length,(uint)BSPVerticesData.Length + (uint)BspVertsCompressed.Length);
							AppendByteArrayToByteArray(ref BSPVerticesData,BspVertsCompressed,OldBspVertsSize);
							OldBspVertsSize = (uint)BSPVerticesData.Length;
							Redim(ref BSPVerticesData,(uint)BSPVerticesData.Length,(uint)BSPVerticesData.Length + (uint)BspLightMapDataCompressed.Length);
							AppendByteArrayToByteArray(ref BSPVerticesData,BspLightMapDataCompressed,OldBspVertsSize);
							
							break;
						case "bitmapraw":
							if (BMAPtrigger != 1)
							{
								BMAPtrigger = 1;
								uint bmOffset;
								uint bmChunkCount;
								uint bmfOffset = 0;
								uint bmfSize = 0;
								uint TifSize;
								uint SavePos; 
								uint TotalbmfSize = 0;
								TifSize = GetUInt(StructBuffer,28);

								bmOffset = Val(CMD[1].Replace(TabReplace,"").ToLower().Trim());
								bmChunkCount = GetUInt(StructBuffer,bmOffset);
								bmOffset = GetUInt(StructBuffer,bmOffset + 4) - MapMagic;

								SavePos = (uint)MapFile.Position;
								
								byte[] BitmapStructBuffer = new byte[48 * bmChunkCount];

								MapFile.Seek(bmOffset,System.IO.SeekOrigin.Begin);
								MapFile.Read(BitmapStructBuffer,0,48 * (int)bmChunkCount);
								MapFile.Seek(SavePos,System.IO.SeekOrigin.Begin);
								bmfOffset = GetUInt(BitmapStructBuffer,24);
								for (uint SpecCount = 0;SpecCount < bmChunkCount;SpecCount +=1)
								{
										bmfSize = GetUInt(BitmapStructBuffer,28 + (48 * SpecCount));
										TotalbmfSize = TotalbmfSize + bmfSize;
								}
								Redim(ref StructBuffer,BufferSize,BufferSize + TifSize + TotalbmfSize);
								switch (MapVersion)
								{
									case 5:
										SavePos = (uint)MapFile.Position;
										MapFile.Seek(bmfOffset,System.IO.SeekOrigin.Begin);
										MapFile.Read(StructBuffer,(int)(BufferSize + TifSize),(int)TotalbmfSize);
										MapFile.Seek(SavePos,System.IO.SeekOrigin.Begin);
										break;
									case 7:
										BitmapFile.Seek(bmfOffset,System.IO.SeekOrigin.Begin);
										BitmapFile.Read(StructBuffer,(int)(BufferSize + TifSize),(int)TotalbmfSize);
										break;
									case 0x261:
										SavePos = (uint)MapFile.Position;
										MapFile.Seek(bmfOffset,System.IO.SeekOrigin.Begin);
										MapFile.Read(StructBuffer,(int)(BufferSize + TifSize),(int)TotalbmfSize);
										MapFile.Seek(SavePos,System.IO.SeekOrigin.Begin);
										break;
								}

								BufferSize = BufferSize + TifSize + TotalbmfSize;

								PutUInt(StructBuffer,TotalbmfSize,48);
							}
							break;
						case "addprevsize":
							if ( (cc - 1) == 0xffffffff )
							{
								PutUInt(StructBuffer,0,24+ (StructSize * (cc)));
							}
							else
							{
								PrevSize = PrevSize + GetUInt(StructBuffer,28 + (StructSize * (cc - 1)));
								PutUInt(StructBuffer  ,PrevSize,24+ (StructSize * (cc)));
							}
							break;
						case "rawxmodeldata":
							uint XTrueVerticesSize = (GetUInt(StructBuffer,88 + (StructSize * cc))) * 32;
							uint XTrueIndicesSize =(((GetUInt(StructBuffer,72 + (StructSize * cc))) / 3) + 1 ) * 6;
							uint XNewIndicesDataSize = (GetUInt(StructBuffer,72 + (StructSize * cc)) / 3) + 1;
							uint XTrueVerticesOffset = GetUInt(StructBuffer,100 + (StructSize * cc)) - MapMagic;
							uint XSaveMapFilePos = (uint)MapFile.Position;
							byte[] XModelHeader = new byte[16];
							MapFile.Seek((long)XTrueVerticesOffset,System.IO.SeekOrigin.Begin);
							MapFile.Read(XModelHeader,0,XModelHeader.Length);
							MapFile.Seek(XSaveMapFilePos,System.IO.SeekOrigin.Begin);
							XTrueVerticesOffset = GetUInt(XModelHeader,4) - MapMagic;
								
							//XTrueVerticesOffset = GetUInt(StructBuffer,XTrueVerticesOffset) - MapMagic;
							uint XTrueIndicesOffset = GetUInt(StructBuffer, 80+ (StructSize * cc)) - MapMagic;

							byte[] XTestBuffer = new byte[6];
								
							PutUInt(StructBuffer,GetUInt(StructBuffer,88 + (StructSize * cc)),44 +(StructSize * cc));
							PutUInt(StructBuffer,XNewIndicesDataSize,56 + (StructSize * cc));

							XSaveMapFilePos = (uint)MapFile.Position;
							uint XCurrentStructSize = (uint)StructBuffer.Length;

							byte[] RawXModelBuffer = new byte[XTrueVerticesSize];
							Redim(ref StructBuffer,XCurrentStructSize,XCurrentStructSize + XTrueVerticesSize );
							MapFile.Seek((long)XTrueVerticesOffset,System.IO.SeekOrigin.Begin);
							MapFile.Read(RawXModelBuffer,0,(int)XTrueVerticesSize);
							for (uint Xcopy = 0;Xcopy < XTrueVerticesSize;Xcopy += 32)
							{
								PutUInt(StructBuffer,GetUInt(RawXModelBuffer,Xcopy),Xcopy + XCurrentStructSize);
								SwapInt(StructBuffer,Xcopy + XCurrentStructSize);
								PutUInt(StructBuffer,GetUInt(RawXModelBuffer,Xcopy + 4),Xcopy + XCurrentStructSize + 4);
								SwapInt(StructBuffer,Xcopy + XCurrentStructSize + 4);
								PutUInt(StructBuffer,GetUInt(RawXModelBuffer,Xcopy + 8),Xcopy + XCurrentStructSize + 8);
								SwapInt(StructBuffer,Xcopy + XCurrentStructSize + 8);
								PutUInt(StructBuffer,GetUInt(RawXModelBuffer,Xcopy + 12),Xcopy + XCurrentStructSize + 12);
								SwapInt(StructBuffer,Xcopy + XCurrentStructSize + 12);
								PutUInt(StructBuffer,GetUInt(RawXModelBuffer,Xcopy + 16),Xcopy + XCurrentStructSize + 16);
								SwapInt(StructBuffer,Xcopy + XCurrentStructSize + 16);
								PutUInt(StructBuffer,GetUInt(RawXModelBuffer,Xcopy + 20),Xcopy + XCurrentStructSize + 20);
								SwapInt(StructBuffer,Xcopy + XCurrentStructSize + 20);
								PutUInt(StructBuffer,GetUInt(RawXModelBuffer,Xcopy + 24),Xcopy + XCurrentStructSize + 24);
								SwapLong(StructBuffer,Xcopy + XCurrentStructSize + 24);
								SwapLong(StructBuffer,Xcopy + XCurrentStructSize + 26);
								PutUInt(StructBuffer,GetUInt(RawXModelBuffer,Xcopy + 28),Xcopy + XCurrentStructSize + 28);
								//SwapLong(StructBuffer,Xcopy + XCurrentStructSize + 28);
								SwapLong(StructBuffer,Xcopy + XCurrentStructSize + 30);

								//SwapInt(StructBuffer,Xcopy + XCurrentStructSize + 24);
								//PutUInt(StructBuffer,GetUInt(RawXModelBuffer,Xcopy + 28),Xcopy + XCurrentStructSize + 28);
								//SwapInt(StructBuffer,Xcopy + XCurrentStructSize + 28);

								//ushort XTestushort = GetUShort(StructBuffer,Xcopy + XCurrentStructSize + 58);
								//if (XTestULong == 0)
								//{
								//	PutUShort(StructBuffer,XTestULong - 1,Xcopy + XCurrentStructSize + 58);
								//}
								//PutUInt(StructBuffer,GetUInt(RawXModelBuffer,Xcopy + 60),Xcopy + XCurrentStructSize + 60);
								//SwapInt(StructBuffer,Xcopy + XCurrentStructSize + 60);
								//PutUInt(StructBuffer,GetUInt(RawXModelBuffer,Xcopy + 64),Xcopy + XCurrentStructSize + 64);
								//SwapInt(StructBuffer,Xcopy + XCurrentStructSize + 64);
							}
							XCurrentStructSize = (uint)StructBuffer.Length;
							Redim(ref StructBuffer,XCurrentStructSize,XCurrentStructSize + XTrueIndicesSize); //+ TrueIndicesSize
							RawXModelBuffer = new byte[XTrueIndicesSize + 6];
								
							MapFile.Seek((long)XTrueIndicesOffset,System.IO.SeekOrigin.Begin);
							MapFile.Read(XModelHeader,0,XModelHeader.Length);
							XTrueIndicesOffset = GetUInt(XModelHeader,4) - MapMagic;
							MapFile.Seek((long)XTrueIndicesOffset,System.IO.SeekOrigin.Begin);
							MapFile.Read(RawXModelBuffer,0,RawXModelBuffer.Length);
							MapFile.Seek(MapFile.Position - 6,System.IO.SeekOrigin.Begin);
							MapFile.Read(XTestBuffer,0,6);
							if (XTestBuffer[4] == 0xff)
							{
								//CurrentStructSize = (uint)StructBuffer.Length;
								Redim(ref StructBuffer,XCurrentStructSize,XCurrentStructSize + XTrueIndicesSize + 6); //+ TrueIndicesSize
								PutUInt(StructBuffer,GetUInt(StructBuffer,56 + (StructSize * cc)) + 1,56 + (StructSize * cc));
								XTrueIndicesSize += 6;
							}
							for (uint Xcopy = 0;Xcopy < XTrueIndicesSize ;Xcopy +=2)
							{
								StructBuffer[Xcopy + XCurrentStructSize] = RawXModelBuffer[Xcopy + 1];
								StructBuffer[Xcopy + XCurrentStructSize + 1] = RawXModelBuffer[Xcopy];
							}
							break;
						case "rawmodeldata":
							uint TrueVerticesSize = (GetUInt(StructBuffer,88 + (StructSize * cc))) * 68; 
							uint TrueIndicesSize =(((GetUInt(StructBuffer,72 + (StructSize * cc))) / 3) + 1 ) * 6; 
							uint NewIndicesDataSize = (GetUInt(StructBuffer,72 + (StructSize * cc)) / 3) + 1;
							uint TrueVerticesOffset = GetUInt(StructBuffer,	100+ (StructSize * cc)) + VerticesOffset;  //80
							uint TrueIndicesOffset = GetUInt(StructBuffer, 80+ (StructSize * cc)) +  IndicesOffset;  //100
							byte[] TestBuffer = new byte[6];
							PutUInt(StructBuffer,GetUInt(StructBuffer,88 + (StructSize * cc)),32 +(StructSize * cc));
							PutUInt(StructBuffer,NewIndicesDataSize,56 + (StructSize * cc));
							uint SaveMapFilePos = (uint)MapFile.Position;
							uint CurrentStructSize = (uint)StructBuffer.Length;
							byte[] RawModelBuffer = new byte[TrueVerticesSize];
							Redim(ref StructBuffer,CurrentStructSize,CurrentStructSize + TrueVerticesSize ); //+ TrueIndicesSize
							MapFile.Seek((long)TrueVerticesOffset,System.IO.SeekOrigin.Begin);
							MapFile.Read(StructBuffer,(int)CurrentStructSize,(int)TrueVerticesSize);	
							CurrentStructSize = (uint)StructBuffer.Length;
							Redim(ref StructBuffer,CurrentStructSize,CurrentStructSize + TrueIndicesSize); //+ TrueIndicesSize
							RawModelBuffer = new byte[TrueIndicesSize + 6];
							MapFile.Seek((long)TrueIndicesOffset,System.IO.SeekOrigin.Begin);
							MapFile.Read(RawModelBuffer,0,RawModelBuffer.Length);
							MapFile.Seek(MapFile.Position - 6,System.IO.SeekOrigin.Begin);
							MapFile.Read(TestBuffer,0,6);
							if (TestBuffer[4] == 0xff)
							{
								Redim(ref StructBuffer,CurrentStructSize,CurrentStructSize + TrueIndicesSize + 6); //+ TrueIndicesSize
								PutUInt(StructBuffer,GetUInt(StructBuffer,56 + (StructSize * cc)) + 1,56 + (StructSize * cc));
								TrueIndicesSize += 6;
							}
							for (uint copy = 0;copy < TrueIndicesSize ;copy +=1)
							{
								StructBuffer[copy + CurrentStructSize] = RawModelBuffer[copy];
							}
							break;
					}
				}
			}

			//MapFile.Read(RawBuffer,0,RawBuffer.Length);
			long DStructOffsetSave = TagFile.Position;
			TagFile.Write(StructBuffer,0,(int)StructBuffer.Length);
			TagFile.Write(TagRefBuffer,0,TagRefBuffer.Length);
			TagFile.Write(RawBuffer,0,RawBuffer.Length);
			//byte[] BSPVerticesData = new byte[SizeOfVerts];
			//MapFile.Read(BSPVerticesData,0,BSPVerticesData.Length);
			TagFile.Write(BSPVerticesData,0,BSPVerticesData.Length);

			//TagFile.Write(RawAnimationsBuffer,0,RawAnimationsBuffer.Length);
			BSPVerticesData = null;
			TagRefBuffer = new byte[0];
			StructRef = 0;
			string StructType = "internal";
			for (uint cc = 0;cc < ChunkCount; cc++)
			{
				for (uint dd = 0;dd < MagIndex; dd++)
				{
					CMD = StructureArray[dd].Split(new char[]{' '},256);
					switch (CMD[0])
					{
						case "externalsnddata":
							uint DataSizeOffset = Val(CMD[1]);
							uint NormalDataOffset = Val(CMD[2]);
							uint SndDataSize = BitConverter.ToUInt32(OffsetBuffer,(int)(DataSizeOffset + (cc * StructSize)));
							uint TrueOffset = BitConverter.ToUInt32(OffsetBuffer,(int)(NormalDataOffset + 4 + (cc * StructSize)));
							byte[] SoundData = new byte[SndDataSize];
							SoundFile.Seek(TrueOffset,System.IO.SeekOrigin.Begin);
							SoundFile.Read(SoundData,0,SoundData.Length);
							TagFile.Write(SoundData,0,SoundData.Length);
							SoundData = null;
							break;
						case "internalraw":
							uint RawSizeOffset = Val(CMD[1]);
							uint RawTrueOffset = Val(CMD[2]);
							uint TempMagic;
							if (CMD.Length == 4)
							{
								if (CMD[3] == "bsp")
								{
									TempMagic = OffsetMagic;
								}
								else
								{
									TempMagic = MapMagic;
								}
							}
							else
							{
								TempMagic = MapMagic;
							}
							uint RawDataSize = BitConverter.ToUInt32(OffsetBuffer,(int)(RawSizeOffset + (cc * StructSize)));
							uint RawDataTrueOffset = BitConverter.ToUInt32(OffsetBuffer,(int)(RawTrueOffset + 4 + (cc * StructSize))) - TempMagic;
							byte[] RawData = new byte[RawDataSize];
							long FilePosSave = MapFile.Position;
							MapFile.Seek((long)RawDataTrueOffset,System.IO.SeekOrigin.Begin);
							MapFile.Read(RawData,0,RawData.Length);
							TagFile.Write(RawData,0,RawData.Length);
							MapFile.Seek(FilePosSave,System.IO.SeekOrigin.Begin);
							RawData = null;
							break;

						case "struct":
							if (CMD[2] != StructureName)
							{
								Structure2 ChildStruct;
								AfterStructChunkCount = GetUInt(OffsetBuffer,Val(CMD[1]) + (cc * StructSize));
								long CurrentPosSave = TagFile.Position;
								uint CurentOffset = (GetUInt(OffsetBuffer,Val(CMD[1]) + (cc * StructSize) + 4) - MapMagic) - MetaOffset;
								PutUInt(StructBuffer,(uint)TagFile.Position - 64,Val(CMD[1]) + (cc * StructSize) + 8);
								PutUInt(StructBuffer,CurentOffset,Val(CMD[1]) + (cc * StructSize) + 4);
								TagFile.Seek(DStructOffsetSave,System.IO.SeekOrigin.Begin);
								TagFile.Write(StructBuffer,0,StructBuffer.Length);
								TagFile.Seek(CurrentPosSave,System.IO.SeekOrigin.Begin);
								
								if (AfterStructChunkCount != 0)
								{
									StructRef +=1;
								}
								do 
								{
									NewStructureArray[NewIndexSize] = StructureArray[dd + NewIndexSize];
									TestString =  StructureArray[dd + NewIndexSize].Split(new char[]{' '},256);
									NewIndexSize = NewIndexSize + 1;
									if(TestString.Length > 1)
									{
										TestHelpString = TestString[1];
									}
									else
									{
										TestHelpString = "";
									}
								}while (CMD[2] != TestHelpString);
								dd = dd + (NewIndexSize - 1);
								ChildChunkCount = 0;


								switch (CMD.Length)
								{
									case 6:
										uint Devided;
										uint Devidedby;
										Devided = GetUInt(OffsetBuffer,Val(CMD[5]) + (cc * StructSize));
										Devidedby = Val(CMD[3]);
										ChildChunkCount = (Devided / Devidedby) ;
										StructType = "internal";
										break;
									case 7:
										Devided = GetUInt(OffsetBuffer,Val(CMD[5]) + (cc * StructSize));
										Devidedby = Val(CMD[3]);
										ChildChunkCount = (Devided / Devidedby);
										OffsetMagic = 0;
										StructType = CMD[6];
										break;
									case 5:
										StructType = "internal";
										ChildChunkCount = GetUInt(OffsetBuffer,Val(CMD[1]) + (cc * StructSize));
										break;
									case 4:
										StructType = "internal";
										ChildChunkCount = GetUInt(OffsetBuffer,Val(CMD[1]) + (cc * StructSize));
										break;
								}

								MapFile.Seek(GetUInt(OffsetBuffer,Val(CMD[1]) + 4 + (cc * StructSize))-OffsetMagic,System.IO.SeekOrigin.Begin);
								//PutUInt(StructBuffer,TrueMetaOffset,Val(CMD[1]) + 4 + (cc * StructSize));
								if (ChildChunkCount !=0)
								{
									switch (StructType)
									{
										case "external":
											SoundFile.Seek(GetUInt(OffsetBuffer,Val(CMD[1]) + 4 + (cc * StructSize))-OffsetMagic,System.IO.SeekOrigin.Begin);
											ChildStruct = new Structure2();
											ChildStruct.StructureCS(MetaOffset,NewIndexSize,OffsetMagic,MapMagic,NewStructureArray,ChildChunkCount,SoundFile,TagFile,BitmapFile,SoundFile,VerticesOffset,IndicesOffset,MapVersion);
											ChildStruct = null;
											StructType = "internal";
											break;
										case "internal":
											ChildStruct = new Structure2();
											ChildStruct.StructureCS(MetaOffset,NewIndexSize,OffsetMagic,MapMagic,NewStructureArray,ChildChunkCount,MapFile,TagFile,BitmapFile,SoundFile,VerticesOffset,IndicesOffset,MapVersion);
											ChildStruct = null;
											break;
									}
								}
								
								for (uint TagHandler = 0;TagHandler < TagRefIndex;TagHandler +=1)
								{
									if (StructRef == TagRefParentIndex[TagHandler])
									{
										if (StructRef != 0)
										{
											TagRefBufferOldSize = (uint)TagRefBuffer.Length;
											TagRefBufferNewSize = TagRefBufferOldSize + (uint)TagRefStrings[TagHandler].Length + 1;
											Redim(ref TagRefBuffer,TagRefBufferOldSize,TagRefBufferNewSize);
											TagRefBuffer = AppendToByteArray(TagRefBuffer,TagRefStrings[TagHandler],TagRefBufferOldSize);
											TagRefStrings[TagHandler] ="";
											TagRefParentIndex[TagHandler] = 65535;
										}
									}
									
								}
								TagFile.Write(TagRefBuffer,0,TagRefBuffer.Length);
								TagRefBuffer = new byte[0];
								NewIndexSize = 0;
							}
							break;
						case "break":
							break;
					}

				}
			}
			StructBuffer = new byte[0];
			OffsetBuffer = new byte[0];
		}

		private uint GetUInt(byte[] Array,uint Offset)
		{
			uint RetVal = Array[Offset + 3];
			RetVal = (RetVal << 8) + (uint)Array[Offset + 2];
			RetVal = (RetVal << 8) + (uint)Array[Offset + 1];
			RetVal = (RetVal << 8) + (uint)Array[Offset];
			return RetVal;
		}
		private string ReadString(byte[] Array)
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
		private void Redim(ref byte[] Buffer,uint OldSize,uint NewSize)
		{
			byte[] TempBuff = new byte[NewSize];
			Buffer.CopyTo(TempBuff,0);
			Buffer = new byte[NewSize];
			TempBuff.CopyTo(Buffer,0);
			TempBuff = null;
		}
		private void PutUInt(byte[] Array,uint Value,uint Offset)
		{
			byte[] Temp = BitConverter.GetBytes(Value);
			Temp.CopyTo(Array,Offset);
		}
		private byte[] AppendToByteArray(byte[] Array,string String,uint OldSize)
		{
			
			char[] Append = new char[String.Length];
			Append = String.ToCharArray();
			for (uint CC=0;CC<String.Length;CC+=1)
			{
				Array[OldSize + CC] = (byte)Append[CC];
			}
			return Array;
		}
		
		private void AppendByteArrayToByteArray(ref byte[] Array1,byte[] Array2,uint Offset)
		{
			Array2.CopyTo(Array1,Offset);
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
		private void SwapLong(byte[] Array,uint Offset)
		{
			byte st;
			st = Array[Offset +1];
			Array[Offset+1] = Array[Offset];
			Array[Offset] = st;
		}
		private void ClearLong(byte[] Array,uint Offset)
		{
			byte[] Clear = new byte[2];
			Clear.CopyTo(Array,(int)Offset);
		}
		private void ClearInt(byte[] Array,uint Offset)
		{
			byte[] Clear = new byte[4];
			Clear.CopyTo(Array,(int)Offset);
		}
		private ushort GetUShort(byte[] Array,uint Offset)
		{
			return BitConverter.ToUInt16(Array,(int)Offset);
		}
		private void PutUShort(byte[] Array,ushort Value,uint Offset)
		{
			byte[] Temp = BitConverter.GetBytes(Value);
			Temp.CopyTo(Array,Offset);
		}

		private uint Val(string Hex)
		{
			string[] Test;
			Test = Hex.Split(new char[]{'x'},255);
			switch (Test.Length)
			{
				case 1:
					return Convert.ToUInt32(Hex);
					break;
				case 2:
					return Convert.ToUInt32(Hex,16);
					break;
			}
			return 0;
		}

		uint CompressVector(Single[] pVector)
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

		short CompressFloatToShort(Single input)
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


		private Single GetFloat(byte[] Array,uint Offset)
		{
			byte[] Converter = new byte[4];
			Single Out;
			Converter[0] = Array[Offset + 0];
			Converter[1] = Array[Offset + 1];
			Converter[2] = Array[Offset + 2];
			Converter[3] = Array[Offset + 3];
			Out = BitConverter.ToSingle(Converter,0);
			return Out;
		}
		
	}
}
