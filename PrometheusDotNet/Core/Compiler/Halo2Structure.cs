using System;
using System.IO;

namespace Prometheus.Core.Compiler
{
	/// <summary>
	/// Summary description for Struct.
	/// </summary>
	public class Halo2Structure
	{
		bool DebugStructs = false;
		uint StructRef;
		uint da =0;
		uint TagRefIndex = 0;
		uint ScriptRefIndex = 0;
		uint[] TagRefParentIndex = new uint[8048];
		uint[] ScriptRefParentIndex = new uint[8048];
		uint[] TagRefChunkCountIndex = new uint[8048];
		uint[] ScriptRefChunkCountIndex = new uint[8048];
		string[] TagRefStrings = new string[8048];
		string[] ScriptRefStrings = new string[8048];
		string TabReplace = "";
		byte TabByte = 0x09;
		public struct sKnownStucts
		{
			public string Name;
			public uint OffsetInParent;
			public uint SizeOfStruct;
		}
		public struct sKnownRSRCs
		{
			public string ParentName;
			public uint OffsetInParent;
		}
		public struct sMissedStructs
		{
			public string ParentName;
			public uint OffsetInParent;
			public uint OffsetToChild;
		}
		public struct sKnownTagRefs
		{
			public string ParentName;
			public uint OffsetInParent;
		}
		public struct sKnownScpRefs
		{
			public string ParentName;
			public uint OffsetInParent;
		}
			public Halo2Structure()
			{
				//
				// TODO: Add constructor logic here
				//
			}
			public void StructureCS(uint MagIndex,uint OffsetMagic,uint MapMagic,string[] StructureArray,uint ChunkCount,FileStream MapFile,MemoryStream TagFile,FileStream BitmapFile,uint VerticesOffset,uint IndicesOffset,byte[] StringTable,byte[] StringTableOffsetList,FileStream single_player_shared_map,FileStream mainmenu_map,FileStream shared_map,uint StartOfTags,uint MapSize,StreamWriter DebugFile,byte[] TagTable,string[] ScriptStringArray)
			{
				TabReplace += (char)TabByte;
				sKnownStucts[] KnownStructs = new sKnownStucts[8096];
				sMissedStructs[] MissedStructs = new sMissedStructs[8096];
				sKnownTagRefs[] KnownTagRefs = new sKnownTagRefs[8096];
				sKnownScpRefs[] KnownScpRefs = new sKnownScpRefs[8096];
				sKnownRSRCs[] KnownRSRCs = new sKnownRSRCs[8096];
				//byte[] OutByte;
				//string SaveStructName = "";
				//uint StructCount = 0;
	
				string[] VertsStringBuffer = new string[0x2ffff];
				string[] UVsStringBuffer = new string[0x2ffff];
				string[] IndicesStringBuffer = new string[0x2ffff];
				byte[] RSRC_BLOCK = new byte[0];
				byte[] BITMAPS = new byte[0];
				uint SizeOfVerts = 0;
				uint StructSize;
				uint TestInc = 0;
				uint StructOffset;
				uint ChildChunkCount;
				uint NewIndexSize = 0;
				uint TagRefOffset = 0;
				uint TagRefStringOffset = 0;
				uint TagRefBufferOldSize = 0;
				uint TagRefBufferNewSize = 0;
				uint ScriptRefBufferOldSize = 0;
				uint ScriptRefBufferNewSize = 0;
				uint ScriptRefOffset = 0;
				uint ScriptRefStringIndex = 0;
				uint AfterStructChunkCount =0;
				byte[] TagRefBuffer = new byte[0];
				byte[] ScriptRefBuffer = new byte[0];
				byte[] MapStringBuffer = new byte[512];
				byte[] RawAnimationsBuffer = new byte[0];
				byte[] RawAnimationEditBuffer = new byte[0];
				string TestHelpString;
				string TagRefString;
				uint vs = 0;
				uint vi = 0;
				uint vu = 0;
				uint SizeLastVerts = 0;
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
				uint TagRefCount = 0;
				uint ScpRefCount = 0;
				uint RSRCCount = 0;
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
									KnownStructs[StructRef].Name = CMD[2];
									KnownStructs[StructRef].OffsetInParent = Val(CMD[1]);
									KnownStructs[StructRef].SizeOfStruct = Val(CMD[3]);


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
											if (TestHelpString == "hlmt_sub_1")
											{
												//int j = 0;
											}
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
							case "bspmodels":
								uint UnKnownSection1Size;
								uint UnKnownSection2Size;
								uint IndicesSectionSize;
								uint UnKnownSection3Size;
								uint UnKnownSection4Size;
								uint UnKnownSection5Size;
								uint VertsSectionSize;
								uint BlockOffset;
								uint BlockSize;
								uint fPosSave;

								BlockOffset = GetUInt(StructBuffer,0x28 + (StructSize * cc));
								BlockSize = GetUInt(StructBuffer,0x2C + (StructSize * cc));
								VertsSectionSize = (uint)(BitConverter.ToUInt16(StructBuffer,(0 + (int)(StructSize * cc))) * 0xc);						
								fPosSave = (uint)MapFile.Position;
								MapFile.Seek(BlockOffset,System.IO.SeekOrigin.Begin);

								//read BlockHeader
								byte[] BlockBuffer = new byte[0x4c];
								MapFile.Read(BlockBuffer,0,BlockBuffer.Length);
								MapFile.Seek(MapFile.Position + 4,System.IO.SeekOrigin.Begin);

								//Read Indices counts
								ushort[] TotalIndsPerMesh = new ushort[GetUInt(BlockBuffer,0x08)];
								ushort[] ShaderId = new ushort[GetUInt(BlockBuffer,0x04)];
								UnKnownSection1Size = GetUInt(BlockBuffer,0x08) * 0x48;
								byte[] aBuffer = new byte[UnKnownSection1Size];
								MapFile.Read(aBuffer,0,aBuffer.Length);
								for (uint ti = 0;ti < GetUInt(BlockBuffer,0x08);ti +=1)
								{
									TotalIndsPerMesh[ti] = BitConverter.ToUInt16(aBuffer,(int)(0x08 + (0x48 * ti)));
									ShaderId[ti] = BitConverter.ToUInt16(aBuffer,(int)(0x04 + (0x48 * ti)));
								}
								ushort[] h = new ushort[15];
								h[1]= BitConverter.ToUInt16(StructBuffer,(int)(0x52 + (0xb0 * cc)));
								h[2]= BitConverter.ToUInt16(StructBuffer,(int)(0x54 + (0xb0 * cc)));
								h[3]= BitConverter.ToUInt16(StructBuffer,(int)(0x56 + (0xb0 * cc)));
								h[4]= BitConverter.ToUInt16(StructBuffer,(int)(0x58 + (0xb0 * cc)));
								h[5]= BitConverter.ToUInt16(StructBuffer,(int)(0x5a + (0xb0 * cc)));
								h[6]= BitConverter.ToUInt16(StructBuffer,(int)(0x5c + (0xb0 * cc)));
								h[7]= BitConverter.ToUInt16(StructBuffer,(int)(0x5e + (0xb0 * cc)));
								h[8]= BitConverter.ToUInt16(StructBuffer,(int)(0x60 + (0xb0 * cc)));
								h[9]= BitConverter.ToUInt16(StructBuffer,(int)(0x62 + (0xb0 * cc)));
								h[10] = BitConverter.ToUInt16(StructBuffer,(int)(0x64 + (0xb0 * cc)));
								h[11] = BitConverter.ToUInt16(StructBuffer,(int)(0x66 + (0xb0 * cc)));
								h[12] = BitConverter.ToUInt16(StructBuffer,(int)(0x68 + (0xb0 * cc)));
								h[13] = BitConverter.ToUInt16(StructBuffer,(int)(0x6a + (0xb0 * cc)));
								h[14] = BitConverter.ToUInt16(StructBuffer,(int)(0x6c + (0xb0 * cc)));
								string out1 = "";
								for (int uit = 1;uit <= 14; uit +=1)
								{
									out1 = out1 + " " + string.Format("0x{0:X}",h[uit]);
										
								}
								DebugFile.WriteLine(out1);
								DebugFile.Flush();

								UnKnownSection2Size = GetUInt(BlockBuffer,0x10) * 0x8;
								aBuffer = new byte[UnKnownSection2Size + 4];
								MapFile.Read(aBuffer,0,aBuffer.Length);

								IndicesSectionSize = GetUInt(BlockBuffer,0x28) * 0x2;
								uint IndicesC = GetUInt(BlockBuffer,0x28);
								byte[] IndicesBuffer = new byte[IndicesSectionSize];
								MapFile.Seek(MapFile.Position + 4,System.IO.SeekOrigin.Begin);
								MapFile.Read(IndicesBuffer,0,IndicesBuffer.Length);
								byte[] tt = new byte[1];
								do
								{
									MapFile.Read(tt,0,1);
									if (tt[0] != 0x63)
									{
										MapFile.Seek(MapFile.Position + 1,System.IO.SeekOrigin.Begin);
									}
								}while (tt[0] != 0x63);
								MapFile.Seek(MapFile.Position - 1,System.IO.SeekOrigin.Begin);
								UnKnownSection3Size = GetUInt(BlockBuffer,0x30);
								if (UnKnownSection3Size != 0)
								{
									aBuffer = new byte[UnKnownSection3Size + 4];
									MapFile.Read(aBuffer,0,aBuffer.Length);
								}
								UnKnownSection4Size = GetUInt(BlockBuffer,0x38) * 0x2;
								if (UnKnownSection4Size !=0)
								{
									aBuffer = new byte[UnKnownSection4Size + 4];
									MapFile.Read(aBuffer,0,aBuffer.Length);
								}
								UnKnownSection5Size = GetUInt(BlockBuffer,0x40) * 0x20;
								if (UnKnownSection4Size !=0)
								{
									aBuffer = new byte[UnKnownSection5Size + 4];
									MapFile.Read(aBuffer,0,aBuffer.Length);
								}
							
								byte[] VertsBuffer = new byte[VertsSectionSize];
								MapFile.Seek(MapFile.Position + 4,System.IO.SeekOrigin.Begin);
								MapFile.Read(VertsBuffer,0,VertsBuffer.Length);

								byte[] UVsBuffer = new byte[(VertsSectionSize / 0xc) * 8];
								MapFile.Seek(MapFile.Position + 4,System.IO.SeekOrigin.Begin);
								MapFile.Read(UVsBuffer,0,UVsBuffer.Length);
							
								//StreamWriter ObjOut;
								//ObjOut = new StreamWriter(TagFile.Name + cc.ToString() + ".obj");
							
								//ObjOut.WriteLine("o " + TagFile.Name);

								for (uint vo = 0; vo < (VertsSectionSize / 0xc); vo +=1)
								{
									Single x,y,z;
									x = BitConverter.ToSingle(VertsBuffer,(int)(0 + (vo * 0xc)));
									y = BitConverter.ToSingle(VertsBuffer,(int)(4 + (vo * 0xc)));
									z = BitConverter.ToSingle(VertsBuffer,(int)(8 + (vo * 0xc)));
									VertsStringBuffer[vs] = "v " + x.ToString() + " " + y.ToString() + " " + z.ToString();
									vs +=1;
									//ObjOut.WriteLine("v " + x.ToString() + " " + y.ToString() + " " + z.ToString());
								}
								for (uint vt = 0; vt < (VertsSectionSize / 0xc); vt +=1)
								{
									Single u,v;
									u = BitConverter.ToSingle(UVsBuffer,(int)(0 + (vt * 0x8)));
									v = BitConverter.ToSingle(UVsBuffer,(int)(4 + (vt * 0x8)));
									UVsStringBuffer[vu] = "vt " + u.ToString() + " " + v.ToString() + " " + "1.0";
									vu +=1;
									//ObjOut.WriteLine("vt " + u.ToString() + " " + v.ToString());
								}

								uint IndicesCount = (IndicesSectionSize / 2);
								int[] face1 = new int[IndicesCount * 3];
							
								for (uint fc = 0;fc < IndicesCount;fc +=1)
								{
									face1[fc] = BitConverter.ToUInt16(IndicesBuffer,(int)(fc * 2));
								}
								uint ind = 0;
								for(int ia = 0;ia < TotalIndsPerMesh.Length;ia +=1)
								{
									//ind += 2;
									//IndicesStringBuffer[vi] = "matid " + ShaderId[ia].ToString();
									//vi +=1;

									//IndicesStringBuffer[vi] = "g SubMesh" + da.ToString();
									//vi +=1;
									//ObjOut.WriteLine("g SubMesh" + da.ToString());
									da = da + 1;
									for(uint z = 0;z < TotalIndsPerMesh[ia]; z +=3)
									{
										int i1;
										int i2;
										int i3;
										i1 = (face1[ind] + 1) + (int)SizeLastVerts;
										i2 = (face1[ind +1] + 1) + (int)SizeLastVerts;
										i3 = (face1[ind +2] + 1) + (int)SizeLastVerts;
										IndicesStringBuffer[vi] = "matid " + (ShaderId[ia] + 1).ToString();
										vi +=1;

										IndicesStringBuffer[vi] = "f " + i1 + "/" + i1 + "/" + i1 + " " + i3 + "/" + i3 + "/" + i3+ " " + i2 + "/" + i2 + "/" + i2;
										//ObjOut.WriteLine("f " + i1 + "/" + i1 + "/" + i1 + " " + i2 + "/" + i2 + "/" + i2+ " " + i3 + "/" + i3 + "/" + i3);
										vi +=1;
										ind+=3;
									}
								}
								SizeLastVerts = SizeLastVerts + (VertsSectionSize / 0xc);
								//ObjOut.Flush();
								//ObjOut.Close();

								MapFile.Seek(fPosSave,System.IO.SeekOrigin.Begin);
								break;
							case "writemodels":
                /*
								if (cc == (ChunkCount -1))
								{
									StreamWriter ObjOut;
									ObjOut = new StreamWriter(TagFile.Name + ".obj");
									ObjOut.WriteLine("o " + TagFile.Name);
									
									for(int vo = 0;vo < vs; vo +=1)
									{
										ObjOut.WriteLine(VertsStringBuffer[vo]);
									}
									ObjOut.WriteLine("#" + vs.ToString() + " Of Verts");
									for(int vo = 0;vo < vu;vo +=1)
									{
										ObjOut.WriteLine(UVsStringBuffer[vo]);
									}
									ObjOut.WriteLine("g LevelMesh");
									for(int vo = 0;vo < vi;vo +=1)
									{
										ObjOut.WriteLine(IndicesStringBuffer[vo]);
									}
									ObjOut.Flush();
									ObjOut.Close();
								}
                */
								break;
							case "tagref":
									TagRefOffset = Val(CMD[1]);
									TagRefStringOffset = GetUInt(StructBuffer,(TagRefOffset + 4) + (StructSize * cc));
									KnownTagRefs[TagRefCount].ParentName = StructureName;
									KnownTagRefs[TagRefCount].OffsetInParent = Val(CMD[1]);
									TagRefCount +=1;
								ushort TagIndexId = BitConverter.ToUInt16(StructBuffer,(int)((TagRefOffset + 4) + (StructSize * cc)));

								if (TagRefStringOffset != 0 && TagIndexId != 0xffff && (TagIndexId * 4) < StringTableOffsetList.Length)
								{
									TagRefStringOffset = TagRefStringOffset - MapMagic;
									TagRefString = ReadString(StringTable,BitConverter.ToUInt32(StringTableOffsetList,(TagIndexId)*4));
									TagRefStrings[TagRefIndex] = TagRefString;
									TagRefParentIndex[TagRefIndex] = StructRef;
									TagRefChunkCountIndex[TagRefIndex] = cc;
									TagRefIndex = TagRefIndex + 1;

									PutUInt(StructBuffer,(uint)TagRefString.Length,(TagRefOffset + 4) + (StructSize * cc));

									if (StructRef == 0)
									{
										TagRefBufferOldSize = (uint)TagRefBuffer.Length;
										TagRefBufferNewSize = TagRefBufferOldSize + (uint)TagRefString.Length + 1;
										TagRefBuffer = Redim(TagRefBuffer,TagRefBufferOldSize,TagRefBufferNewSize);
										TagRefBuffer = AppendToByteArray(TagRefBuffer,TagRefString,TagRefBufferOldSize);
									}
								}
								exitcase:;
								break;
							case "scriptref":
									ScriptRefOffset = Val(CMD[1]);
									ScriptRefStringIndex = BitConverter.ToUInt16(StructBuffer,(int)(ScriptRefOffset + (StructSize * cc)));
									byte StrLen = StructBuffer[ScriptRefOffset + 3 + (StructSize * cc)];
									KnownScpRefs[ScpRefCount].ParentName = StructureName;
									KnownScpRefs[ScpRefCount].OffsetInParent = ScriptRefOffset;
									ScpRefCount = ScpRefCount + 1;

									if (ScriptRefStringIndex <= ScriptStringArray.Length && StrLen != 0)
									{
										ScriptRefStrings[ScriptRefIndex] = ScriptStringArray[ScriptRefStringIndex];
										ScriptRefParentIndex[ScriptRefIndex] = StructRef;
										ScriptRefChunkCountIndex[ScriptRefIndex] = cc;									
									
										ScriptRefIndex = ScriptRefIndex + 1;
										if (StructRef == 0)
										{
											ScriptRefBufferOldSize = (uint)ScriptRefBuffer.Length;
											ScriptRefBufferNewSize = ScriptRefBufferOldSize + (uint)ScriptRefStrings[ScriptRefIndex - 1].Length + 1;
											ScriptRefBuffer = Redim(ScriptRefBuffer,ScriptRefBufferOldSize,ScriptRefBufferNewSize);
											ScriptRefBuffer = AppendToByteArray(ScriptRefBuffer,ScriptRefStrings[ScriptRefIndex - 1],ScriptRefBufferOldSize);
										}
									}
								break;
							case "rsrc":
								uint OffsetInStruct = Val(CMD[1]);
								uint Offset = BitConverter.ToUInt32(StructBuffer,(int)(OffsetInStruct + (StructSize * cc)));
								KnownRSRCs[RSRCCount].OffsetInParent = OffsetInStruct;
								KnownRSRCs[RSRCCount].ParentName = StructureName;
								uint Size = BitConverter.ToUInt32(StructBuffer,(int)(OffsetInStruct + 4 + (StructSize * cc)));
								uint RSRCOffset = (uint)RSRC_BLOCK.Length;
								
								RSRC_BLOCK = Redim(RSRC_BLOCK,(uint)RSRC_BLOCK.Length,(uint)(RSRC_BLOCK.Length + Size));

								uint RSRCMapTest = (Offset >> 30);
							switch (RSRCMapTest)
							{
								case 0x00:
									//local file
									uint RSRCSavFPos = (uint)MapFile.Position;
									MapFile.Seek(Offset,System.IO.SeekOrigin.Begin);
									MapFile.Read(RSRC_BLOCK,(int)RSRCOffset,(int)Size);
									MapFile.Seek(RSRCSavFPos,System.IO.SeekOrigin.Begin);
									break;
								case 0x01:
									//mainmenu.map
									RSRCSavFPos = (uint)mainmenu_map.Position;
									mainmenu_map.Seek(Offset & 0x3FFFFFFF,System.IO.SeekOrigin.Begin);
									mainmenu_map.Read(RSRC_BLOCK,(int)RSRCOffset,(int)Size);
									mainmenu_map.Seek(RSRCSavFPos,System.IO.SeekOrigin.Begin);
									break;
								case 0x02:
									//shared.map
									RSRCSavFPos = (uint)shared_map.Position;
									shared_map.Seek(Offset & 0x3FFFFFFF,System.IO.SeekOrigin.Begin);
									shared_map.Read(RSRC_BLOCK,(int)RSRCOffset,(int)Size);
									shared_map.Seek(RSRCSavFPos,System.IO.SeekOrigin.Begin);
									break;
								case 0x03:
									//single_player_shared.map
									RSRCSavFPos = (uint)single_player_shared_map.Position;
									single_player_shared_map.Seek(Offset & 0x3FFFFFFF,System.IO.SeekOrigin.Begin);
									single_player_shared_map.Read(RSRC_BLOCK,(int)RSRCOffset,(int)Size);
									single_player_shared_map.Seek(RSRCSavFPos,System.IO.SeekOrigin.Begin);
									break;
							}

								break;
							case "bitmaps":
								uint[] boffsets = new uint[6];
								uint[] bsizes = new uint[6];
								uint MapTest;
								for(int bl = 0;bl < 6;bl +=1)
								{
									boffsets[bl] = BitConverter.ToUInt32(StructBuffer,(int)(((bl * 4) + 0x1C) + (StructSize * cc)));
									bsizes[bl] = BitConverter.ToUInt32(StructBuffer,(int)(((bl * 4) + 0x34) + (StructSize * cc)));
								}
								for (int lfm = 0;lfm < 6;lfm +=1)
								{
									MapTest = (boffsets[lfm] >> 30);
									switch (MapTest)
									{
										case 0x00:
											//local file
											uint tbOffset = (uint)BITMAPS.Length;
											uint SavFPos = (uint)MapFile.Position;
											if (bsizes[lfm] != 0)
											{

												BITMAPS = Redim(BITMAPS,(uint)BITMAPS.Length,(uint)(BITMAPS.Length + bsizes[lfm]));
												MapFile.Seek(boffsets[lfm],System.IO.SeekOrigin.Begin);
												MapFile.Read(BITMAPS,(int)tbOffset,(int)bsizes[lfm]);
												MapFile.Seek(SavFPos,System.IO.SeekOrigin.Begin);
											}
											break;
										case 0x01:
											//mainmenu.map
											tbOffset = (uint)BITMAPS.Length;
											SavFPos = (uint)mainmenu_map.Position;
											if (bsizes[lfm] != 0)
											{
												BITMAPS = Redim(BITMAPS,(uint)BITMAPS.Length,(uint)(BITMAPS.Length + bsizes[lfm]));
												mainmenu_map.Seek(boffsets[lfm] & 0x3FFFFFFF,System.IO.SeekOrigin.Begin);
												mainmenu_map.Read(BITMAPS,(int)tbOffset,(int)bsizes[lfm]);
												mainmenu_map.Seek(SavFPos,System.IO.SeekOrigin.Begin);
											}
											break;
										case 0x02:
											//shared.map
											tbOffset = (uint)BITMAPS.Length;
											SavFPos = (uint)shared_map.Position;
											if (bsizes[lfm] != 0)
											{
												BITMAPS = Redim(BITMAPS,(uint)BITMAPS.Length,(uint)(BITMAPS.Length + bsizes[lfm]));
												shared_map.Seek(boffsets[lfm] & 0x3FFFFFFF,System.IO.SeekOrigin.Begin);
												shared_map.Read(BITMAPS,(int)tbOffset,(int)bsizes[lfm]);
												shared_map.Seek(SavFPos,System.IO.SeekOrigin.Begin);
											}
											break;
										case 0x03:
											//single_player_shared.map
											tbOffset = (uint)BITMAPS.Length;
											SavFPos = (uint)single_player_shared_map.Position;
											if (bsizes[lfm] != 0)
											{
												BITMAPS = Redim(BITMAPS,(uint)BITMAPS.Length,(uint)(BITMAPS.Length + bsizes[lfm]));
												single_player_shared_map.Seek(boffsets[lfm] & 0x3FFFFFFF,System.IO.SeekOrigin.Begin);
												single_player_shared_map.Read(BITMAPS,(int)tbOffset,(int)bsizes[lfm]);
												single_player_shared_map.Seek(SavFPos,System.IO.SeekOrigin.Begin);
											}
											break;
									}
								}
								break;
						}
					}
				}
				//////////////////////////////
				///Test for Missed Children///
				//////////////////////////////
				if (DebugStructs == true)
				{
					for(int osc = 0;osc < StructSize; osc +=4)
					{
						if (osc + 4 <= StructSize)
						{
							uint Offset = BitConverter.ToUInt32(StructBuffer,osc) - OffsetMagic;
							if (Offset < MapSize && Offset > StartOfTags)
							{
								if (osc >= 4)
								{
									uint Count = BitConverter.ToUInt32(StructBuffer,osc - 4);
									if (Count < 10000)
									{
										if (Count != 0)
										{
											//IncSize(ref OffBufList,Offset);
											//IncSize(ref OffsetList,(uint)(StartOfTags + osc));
											//IncSize(ref CountsList,Count);
											bool InStruct = false;
											for (int jk = 0;jk < KnownStructs.Length;jk +=1)
											{
												if (KnownStructs[jk].OffsetInParent == osc -4)
												{
													InStruct = true;
													goto Finshed;
												}
											}
										Finshed:;
											if (InStruct == false)
											{
												// Add offset to Debug List //
												if (StructureName != "snd!_main")
												{
													DebugFile.WriteLine( "Parent Stucture name" + StructureName);
													DebugFile.WriteLine( "Offset In Parent == " + string.Format("0x{0:X}",osc - 4) + "------Offset In Map == " + string.Format("0x{0:X}",Offset) + "------Count == " + string.Format("0x{0:X}",Count));
													DebugFile.Flush();
												}
											}
										}
										//Builder.WriteLine( string.Format("0x{0:X}",OffsetList[OffsetList.Length - 1]) + "-->" + Count.ToString() + "--->" + string.Format("0x{0:X}",Offset));
									}
								}
							}
						}
					}
				

					for (int osc = 0;osc <StructSize;osc +=4)
					{
						if ((osc + 8) <= StructSize)
						{
							uint TagID = BitConverter.ToUInt32(StructBuffer,osc);
							uint TagID2 = BitConverter.ToUInt32(StructBuffer,osc + 4);
							for (int ost = 0;ost < TagTable.Length; ost +=16)
							{
								if(TagID == BitConverter.ToUInt32(TagTable,ost) && TagID2 == BitConverter.ToUInt32(TagTable,ost + 4))
								{
									for (int gg = 0;gg < TagRefCount; gg +=1)
									{
										if (osc == KnownTagRefs[gg].OffsetInParent)
										{
											goto SkipWrite;
										}
									}
									DebugFile.WriteLine( "Parent Stucture name" + StructureName);
									DebugFile.WriteLine( "Offset Of TagRef in Parent ==" + string.Format("0x{0:X}",osc));
									DebugFile.Flush();
									goto ExitLoop;
								SkipWrite:;
								}
							}
						ExitLoop:;
						}
					}
					for (int osc = 0;osc < StructSize;osc += 4)
					{
						if ((osc + 8) <= StructSize)
						{
							ushort ScriptIndex = BitConverter.ToUInt16(StructBuffer,osc);
							SwapLong(StructBuffer,(uint)(osc + 2));
							ushort ScriptRefLenght = BitConverter.ToUInt16(StructBuffer,osc + 2);
							SwapLong(StructBuffer,(uint)(osc + 2));
							if (ScriptIndex <= ScriptStringArray.Length)
							{
								if (ScriptStringArray[ScriptIndex].Length == ScriptRefLenght && ScriptRefLenght <= 0x80)
								{
									for (int gg = 0;gg < ScpRefCount; gg +=1)
									{
										if (osc == KnownScpRefs[gg].OffsetInParent)
										{
											goto SkipScpWrite;
										}
									}
									DebugFile.WriteLine( "Parent Stucture name" + StructureName);
									DebugFile.WriteLine( "Offset Of Script Ref" + string.Format("0x{0:X}",osc));
									DebugFile.Flush();
								}
							SkipScpWrite:;
							}
						
						}
					}
					for (int osc = 0;osc < StructSize;osc += 4)
					{
						if ((osc + 8) <= StructSize)
						{
							long SVPOS = MapFile.Position;
							uint InMapOffset = BitConverter.ToUInt32(StructBuffer,osc);
							uint RSRCMapTest = (InMapOffset >> 30);
							if ((InMapOffset & 0x3FFFFFFF) <= (MapSize - 4))
							{
								InMapOffset = InMapOffset & 0x3FFFFFFF;
								byte[] gf = new byte[4];
								switch (RSRCMapTest)
								{
									case 00:
										MapFile.Seek((long)InMapOffset,System.IO.SeekOrigin.Begin);
										MapFile.Read(gf,0,4);
										break;
									case 01:
										mainmenu_map.Seek((long)InMapOffset,System.IO.SeekOrigin.Begin);
										mainmenu_map.Read(gf,0,4);
										break;
									case 02:
										shared_map.Seek((long)InMapOffset,System.IO.SeekOrigin.Begin);
										shared_map.Read(gf,0,4);
										break;
									case 03:
										single_player_shared_map.Seek((long)InMapOffset,System.IO.SeekOrigin.Begin);
										single_player_shared_map.Read(gf,0,4);
										break;
								}
							
								uint RSRCTest = BitConverter.ToUInt32(gf,0);
								bool InThere;
								if (RSRCTest == 0x626c6b68)  //0x686B6C62
								{
									InThere = false;
									for (int itsin = 0; itsin < KnownRSRCs.Length;itsin += 1)
									{
									
										if (osc == KnownRSRCs[itsin].OffsetInParent)
										{
											InThere = true;
										}
									}
									if (InThere == false)
									{
										DebugFile.WriteLine( "Parent Stucture name" + StructureName);
										DebugFile.WriteLine( "Offset Of rsrc block " + string.Format("0x{0:X}",osc));
										DebugFile.Flush();
									}
								}
								MapFile.Seek(SVPOS,System.IO.SeekOrigin.Begin);
							}
						}
					}
				}

				uint ReWriteStructOffset = (uint)TagFile.Position;
				TagFile.Write(StructBuffer,0,(int)StructBuffer.Length);
				TagFile.Write(BITMAPS,0,(int)BITMAPS.Length);
				TagFile.Write(TagRefBuffer,0,TagRefBuffer.Length);
				TagFile.Write(ScriptRefBuffer,0,ScriptRefBuffer.Length);
				TagFile.Write(RSRC_BLOCK,0,RSRC_BLOCK.Length);
				byte[] BSPVerticesData = new byte[SizeOfVerts];
				//MapFile.Read(BSPVerticesData,0,BSPVerticesData.Length);
				//TagFile.Write(BSPVerticesData,0,BSPVerticesData.Length);
				uint ChildStucturePosition = (uint)TagFile.Position;
				//TagFile.Write(RawAnimationsBuffer,0,RawAnimationsBuffer.Length);
				SizeOfVerts = 0;
				BSPVerticesData = null;
				TagRefBuffer = new byte[0];
				ScriptRefBuffer = new byte[0];
				StructRef = 0;
				for (uint cc = 0;cc < ChunkCount; cc++)
				{
					for (uint dd = 0;dd < MagIndex; dd++)
					{
						CMD = StructureArray[dd].Split(new char[]{' '},256);
						switch (CMD[0])
						{
							case "struct":
								if (CMD[2] != StructureName)
								{
									Halo2Structure ChildStruct;
									AfterStructChunkCount = GetUInt(OffsetBuffer,Val(CMD[1]) + (cc * StructSize));
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
									if (CMD.Length == 6)
									{
										uint Devided;
										uint Devidedby;
										Devided = GetUInt(OffsetBuffer,Convert.ToUInt32(CMD[5]) + (cc * StructSize));
										Devidedby = Convert.ToUInt32(CMD[3]);
										ChildChunkCount = (Devided / Devidedby) ;
									}
									else
									{
										ChildChunkCount = GetUInt(OffsetBuffer,Val(CMD[1]) + (cc * StructSize));
									}
									MapFile.Seek(GetUInt(OffsetBuffer,Val(CMD[1]) + 4 + (cc * StructSize))-OffsetMagic,System.IO.SeekOrigin.Begin);
									if (ChildChunkCount !=0)
									{
									
										ChildStruct = new Halo2Structure();
										long CurrentPos = TagFile.Position;
										PutUInt(StructBuffer,(uint)CurrentPos - 0x40,Val(CMD[1]) + 4 + (cc * StructSize));
										ChildStruct.StructureCS(NewIndexSize,OffsetMagic,MapMagic,NewStructureArray,ChildChunkCount,MapFile,TagFile,BitmapFile,VerticesOffset,IndicesOffset,StringTable,StringTableOffsetList,single_player_shared_map,mainmenu_map,shared_map,StartOfTags,MapSize,DebugFile,TagTable,ScriptStringArray);
										long HSavPos = TagFile.Position;
										TagFile.Seek(ReWriteStructOffset,System.IO.SeekOrigin.Begin);
										TagFile.Write(StructBuffer,0,StructBuffer.Length);
										TagFile.Seek(HSavPos,System.IO.SeekOrigin.Begin);
										ChildStruct = null;
									
									}
								
									for (uint TagHandler = 0;TagHandler < TagRefIndex;TagHandler +=1)
									{
										if (StructRef == TagRefParentIndex[TagHandler])
										{
											if (StructRef != 0)
											{
												TagRefBufferOldSize = (uint)TagRefBuffer.Length;
												TagRefBufferNewSize = TagRefBufferOldSize + (uint)TagRefStrings[TagHandler].Length + 1;
												TagRefBuffer = Redim(TagRefBuffer,TagRefBufferOldSize,TagRefBufferNewSize);
												TagRefBuffer = AppendToByteArray(TagRefBuffer,TagRefStrings[TagHandler],TagRefBufferOldSize);
												TagRefStrings[TagHandler] ="";
												TagRefParentIndex[TagHandler] = 65535;
											}
										}
									
									}
									TagFile.Write(TagRefBuffer,0,TagRefBuffer.Length);
									TagRefBuffer = new byte[0];
									NewIndexSize = 0;
									for (uint scpHandler = 0;scpHandler < ScriptRefIndex;scpHandler +=1)
									{
										if (StructRef == ScriptRefParentIndex[scpHandler])
										{
											if (StructRef != 0)
											{
												ScriptRefBufferOldSize = (uint)ScriptRefBuffer.Length;
												ScriptRefBufferNewSize = ScriptRefBufferOldSize + (uint)ScriptRefStrings[scpHandler].Length + 1;
												ScriptRefBuffer = Redim(ScriptRefBuffer,ScriptRefBufferOldSize,ScriptRefBufferNewSize);
												ScriptRefBuffer = AppendToByteArray(ScriptRefBuffer,ScriptRefStrings[scpHandler],ScriptRefBufferOldSize);
												ScriptRefStrings[scpHandler] ="";
												ScriptRefParentIndex[scpHandler] = 65535;
											}
										}
									
									}
									TagFile.Write(ScriptRefBuffer,0,ScriptRefBuffer.Length);
									ScriptRefBuffer = new byte[0];

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
			private byte[] Redim(byte[] Buffer,uint OldSize,uint NewSize)
			{
				byte[] OldBuffer = new byte[NewSize];
				Buffer.CopyTo(OldBuffer,0);
				return OldBuffer;
			}
			private void PutUInt(byte[] Array,uint Value,uint Offset)
			{
				Array[Offset + 3] = (byte)((((Value >> 8) >> 8) >> 8) & 0xff);
				Array[Offset + 2] = (byte)(((Value >> 8) >> 8) & 0xff);
				Array[Offset + 1] = (byte)((Value >> 8) & 0xff);
				Array[Offset] = (byte)(Value & 0xff);
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
				Array[Offset] = 0;
				Array[Offset + 1] = 0;
			}
			private void ClearInt(byte[] Array,uint Offset)
			{
				Array[Offset] = 0;
				Array[Offset + 1] = 0;
				Array[Offset + 2] = 0;
				Array[Offset + 3] = 0;
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
			public float Signed16ToFloat(short val, float min, float max) 
			{
				float percent = (((float)val + 32768) / 65535); 
				return (percent * (max - min)) + min;
			}

		}
}
