using System;
using System.Collections;
using System.IO;

namespace Prometheus.Core.Maps.Halo1
{
	#region Enums
	public enum VERSION : int
	{
		XBOX = 0x5,
		PC = 0x7,
		PC_DEMO = 0x36373530,
		CE = 0x609,
	};

	public enum MAP_TYPE : int
	{
		SINGLE_PLAYER,
		MULTIPLAYER,
		UI,
	};

	/// <summary>
	/// Used in the halo1 index item class
	/// </summary>
	public enum BITMAP_TYPE : int
	{
		INTERNAL = 0x0,
		EXTERNAL = 0x1,
	};
	#endregion

	#region Header
	/// <summary>
	/// Header of a halo1 map
	/// </summary>
	public class MapHeader
	{
		#region ID
		private int id;
		/// <summary>
		/// ID of this map
		/// </summary>
		public int ID
		{
			get { return id; }
			set { id = value; }
		}
		#endregion

		#region Version
		private VERSION version;
		/// <summary>
		/// This map's version
		/// </summary>
		public VERSION Version
		{
			get { return version; }
			set { version = value; }
		}
		#endregion

		#region DecompileLength
		private uint decompileLength;
		/// <summary>
		/// When you decompile it, this is how long it s
		/// </summary>
		public uint DecompileLength
		{
			get { return decompileLength; }
			set { decompileLength = value; }
		}
		#endregion

		#region OffsetToIndex
		private uint offsetToIndex;
		/// <summary>
		/// Offset in the map to the map's index
		/// </summary>
		public uint OffsetToIndex
		{
			get { return offsetToIndex; }
			set { offsetToIndex = value; }
		}
		#endregion

		#region MetaDataSize
		private uint metaDataSize;
		/// <summary>
		/// The size of the meta data
		/// </summary>
		public uint MetaDataSize
		{
			get { return metaDataSize; }
			set { metaDataSize = value; }
		}
		#endregion

		#region Name
		private string name;
		/// <summary>
		/// Name of this map
		/// </summary>
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		#endregion

		#region BuildDate
		private string buildDate;
		/// <summary>
		/// The build date of this map
		/// </summary>
		public string BuildDate
		{
			get { return buildDate; }
			set { buildDate = value; }
		}
		#endregion

		#region MapType
		private MAP_TYPE mapType;
		/// <summary>
		/// The map type of this map
		/// </summary>
		public MAP_TYPE MapType
		{
			get { return mapType; }
			set { mapType = value; }
		}
		#endregion

		public void Read(ref EndianReader er)
		{
			uint pos = 0; // temp position

			er.ReadInt32(); // head

			Version = (VERSION)er.ReadInt32();
			DecompileLength = er.ReadUInt32();

			//if(DecompileLength < er.Length)
				//("Compressed map editing not avaliable yet!");
			
			er.ReadInt32();

			if(Version == VERSION.PC_DEMO)
			{
				pos = er.Position;
				er.Position = 0x5EC;
				OffsetToIndex = er.ReadUInt32();
				er.Position = pos;
			}
			else
				OffsetToIndex = er.ReadUInt32();

			MetaDataSize = er.ReadUInt32();

			er.ReadInt32(); er.ReadInt32();

			if(Version == VERSION.PC_DEMO)
			{
				pos = er.Position;
				er.Position = 0x58C;
				Name = er.ReadString();
				er.Position = pos;
			}
			else
				Name = er.ReadString();
			BuildDate = er.ReadString();
			MapType = (MAP_TYPE)er.ReadInt32();
			ID = er.ReadInt32();

			er.ReadInt32();
			for(int x = 0; x < 484; x++)
				er.ReadInt32();

			er.ReadInt32(); // foot
		}

		public void Write(ref EndianWriter ew)
		{
			uint pos = 0; // temp position

			ew.WriteTag("head".ToCharArray());
			ew.Write((int)Version);
			ew.Write(DecompileLength);
			
			ew.Write(0);

			if(Version == VERSION.PC_DEMO)
			{
				pos = ew.Position;
				ew.Position = 0x5EC;
				ew.Write(OffsetToIndex);
				ew.Position = pos;
			}
			else
				ew.Write(OffsetToIndex);

			ew.Write(MetaDataSize);

			ew.Write(0); ew.Write(0);

			if(Version == VERSION.PC_DEMO)
			{
				pos = ew.Position;
				ew.Position = 0x5EC;
				ew.Write(Name, false);
				ew.Position = pos;
			}
			else
				ew.Write(Name, false);
			ew.Write(BuildDate, false);
			ew.Write((int)MapType);
			ew.Write(ID);

			ew.Write(0);
			for(int x = 0; x < 484; x++)
				ew.Write(0);

			ew.WriteTag(TagGroups.foot.Tag);
		}
	};
	#endregion

	/// <summary>
	/// Index of a halo1 map
	/// </summary>
	public class MapIndex
	{
		#region IndexMagic
		private uint indexMagic;
		/// <summary>
		/// The index's magic
		/// </summary>
		public uint IndexMagic
		{
			get { return indexMagic; }
			set { indexMagic = value; }
		}
		#endregion

		#region StartingID
		private uint startingID;
		/// <summary>
		/// The value of the first object identifier in the index
		/// </summary>
		public uint StartingID
		{
			get { return startingID; }
			set { startingID = value; }
		}
		#endregion

		#region Unknown2
		private int unknown2;
		public int Unknown2
		{
			get { return unknown2; }
			set { unknown2 = value; }
		}
		#endregion

		#region Unknown3
		private int unknown3;
		/// <summary>
		/// PC Version Only
		/// </summary>
		public int Unknown3
		{
			get { return unknown3; }
			set { unknown3 = value; }
		}
		#endregion

		#region TagCount
		private uint tagCount;
		/// <summary>
		/// The tag count for this index
		/// </summary>
		public uint TagCount
		{
			get { return tagCount; }
			set { tagCount = value; }
		}
		#endregion

		#region VertexObjectCount
		private int vertexObjectCount;
		/// <summary>
		/// 
		/// </summary>
		public int VertexObjectCount
		{
			get { return vertexObjectCount; }
			set { vertexObjectCount = value; }
		}
		#endregion

		#region VertexOffset
		private int vertexOffset;
		/// <summary>
		/// 
		/// </summary>
		public int VertexOffset
		{
			get { return vertexOffset; }
			set { vertexOffset = value; }
		}
		#endregion

		#region IndecesOffsetCount
		private int indecesOffsetCount;
		/// <summary>
		/// 
		/// </summary>
		public int IndecesOffsetCount
		{
			get { return indecesOffsetCount; }
			set { indecesOffsetCount= value; }
		}
		#endregion

		#region IndecesOffset
		private int indecesOffset;
		/// <summary>
		/// 
		/// </summary>
		public int IndecesOffset
		{
			get { return indecesOffset; }
			set { indecesOffset = value; }
		}
		#endregion

		#region TagsOffset
		private uint tagsOffset;
		/// <summary>
		/// The offset to the tags
		/// </summary>
		public uint TagsOffset
		{
			get { return tagsOffset; }
			set { tagsOffset = value; }
		}
		#endregion

		#region IndexItems
		private MapIndexItem[] indexItems;
		public MapIndexItem[] IndexItems
		{
			get { return indexItems; }
			set { indexItems = value; }
		}

		public MapIndexItem this [int index]
		{
			get { return indexItems[index]; }
			set { indexItems[index] = value; }
		}
		#endregion

		#region LookupTable
		private Hashtable lookupTable = new Hashtable();
		public Hashtable IDTable
		{
			get { return lookupTable; }
			set { lookupTable = value; }
		}
		#endregion

		#region BSP count
		private int bspCount = 0;
		public int BSPCount
		{
			get { return bspCount; }
		}
		#endregion

		public void Read(ref EndianReader er, bool PC, ref uint Magic, ref uint[] BspsMagic)
		{
			IndexMagic = er.ReadUInt32();

			Magic = (IndexMagic - ((er.Position-4)  + 40)); //(PC ? 40 : 36)));

			StartingID = er.ReadUInt32();
			Unknown2 = er.ReadInt32();
			TagCount = er.ReadUInt32();
			VertexObjectCount = er.ReadInt32();
			VertexOffset = er.ReadInt32();
			IndecesOffsetCount = er.ReadInt32();
			IndecesOffset = er.ReadInt32();
			if(PC) Unknown3 = er.ReadInt32();
			er.ReadUInt32();

			TagsOffset = er.Position;

			// Load tag item data
			IndexItems = new MapIndexItem[TagCount];
			uint sbsp_offset = 0;
			for(uint x = 0; x < IndexItems.Length; x++)
			{
				IndexItems[x] = new MapIndexItem();
				IndexItems[x].Read(ref er, x, Magic);
				IDTable.Add(IndexItems[x].ID, IndexItems[x]);

				// load scnr's bsps block header
				if(x == 0)
				{
					uint temp_pos = er.Position;

					er.Seek(this[0].MetaOffset + 1444, System.IO.SeekOrigin.Begin);
					BspsMagic = new uint[er.ReadUInt32()];
					sbsp_offset = er.ReadUInt32() - Magic;

					er.Seek(temp_pos, System.IO.SeekOrigin.Begin);
				}

				if( Core.TAG.TestTag(IndexItems[x].TagClass, Core.TagGroups.sbsp.Tag) )
				{
					uint temp_pos = er.Position;

					// I swear I'm going to kill VS with its BS about converting "long to unit"
					// when there IS NO FUCKING LONG!
					er.Seek(sbsp_offset + (uint)(bspCount * 32), System.IO.SeekOrigin.Begin);
					IndexItems[x].MetaOffset = er.ReadUInt32();
					IndexItems[x].MetaSize = er.ReadUInt32();
					BspsMagic[bspCount] = er.ReadUInt32() - IndexItems[x].MetaOffset;
					IndexItems[x].MetaOffset += 24;
					IndexItems[x].BSPIndex = bspCount;
					++bspCount;

					er.Seek(temp_pos, System.IO.SeekOrigin.Begin);
				}
			}

			// Load tag strings
			for(int x = 0; x < IndexItems.Length; x++)
				IndexItems[x].FileName = er.ReadCString();
		}

		public void Write(ref EndianWriter ew, bool PC)
		{
			ew.Write(IndexMagic);
			ew.Write(StartingID);
			ew.Write(Unknown2);
			ew.Write(TagCount);
			ew.Write(VertexObjectCount);
			ew.Write(VertexOffset);
			ew.Write(IndecesOffsetCount);
			ew.Write(IndecesOffset);
			if(PC) ew.Write(Unknown3);
			ew.Write(TagsOffset);
		}

		
		/// <summary>
		/// Index Item
		/// </summary>
		public class MapIndexItem
		{
			#region TagClass
			private char[] tagClass = new char[4];
			/// <summary>
			/// The actual tag short hand name
			/// </summary>
			public char[] TagClass
			{
				get { return tagClass; }
				set { tagClass = value; }
			}

			private char[] tagParent1 = new char[4];
			/// <summary>
			/// The first parent tag of this tag
			/// </summary>
			public char[] TagParent1
			{
				get { return tagParent1; }
				set { tagParent1 = value; }
			}

			private char[] tagParent2 = new char[4];
			/// <summary>
			/// The second parent tag of this tag
			/// </summary>
			public char[] TagParent2
			{
				get { return tagParent2; }
				set { tagParent2 = value; }
			}
			#endregion

			#region ID
			private uint id;
			/// <summary>
			/// The tag's ID
			/// </summary>
			public uint ID
			{
				get { return id; }
				set { id = value; }
			}
			#endregion

			#region FileNameOffset
			private uint fileNameOffset;
			/// <summary>
			/// The CString file name offset
			/// </summary>
			public uint FileNameOffset
			{
				get { return fileNameOffset; }
				set { fileNameOffset = value; }
			}
			#endregion

			#region FileName
			private string fileName;
			/// <summary>
			/// The tag's filename
			/// </summary>
			public string FileName
			{
				get { return fileName; }
				set { fileName = value; }
			}
			#endregion

			#region Offset
			private uint offset;
			/// <summary>
			/// offset
			/// </summary>
			public uint Offset
			{
				get { return offset; }
				set { offset = value; }
			}
			#endregion

			#region MetaSize
			private uint metaSize;
			/// <summary>
			/// Meta Size of this item
			/// </summary>
			public uint MetaSize
			{
				get { return metaSize; }
				set { metaSize = value; }
			}
			#endregion

			#region BitmapType
			private BITMAP_TYPE bitmapType;
			/// <summary>
			/// 0x0 if its in the map, 0x1 if its in bitmaps.map
			/// </summary>
			public BITMAP_TYPE BitmapType
			{
				get { return bitmapType; }
				set { bitmapType = value; }
			}
			#endregion

			#region MetaOffset
			private uint metaOffset;
			/// <summary>
			/// The meta offset
			/// </summary>
			public uint MetaOffset
			{
				get { return metaOffset; }
				set { metaOffset = value; }
			}
			#endregion

			public uint ItemsIndex = 0xFFFFFFFF;
			public int BSPIndex = -1;

			public void Read(ref EndianReader er, uint index, uint Magic)
			{
				ItemsIndex = index;

				Offset = er.Position;
				TagClass = er.ReadTag();
				TagParent2 = er.ReadTag();
				TagParent1 = er.ReadTag();
				ID = er.ReadUInt32();
				FileNameOffset = er.ReadUInt32() - Magic;
				FileName = "";
				MetaOffset = er.ReadUInt32() - Magic;

				BitmapType = (BITMAP_TYPE)er.ReadInt32();

				er.ReadInt32();
			}

			public void Write(ref EndianWriter ew)
			{
				ew.WriteTag(TagClass);
				ew.WriteTag(TagParent2);
				ew.WriteTag(TagParent1);
				ew.Write(ID);
				ew.Write(FileNameOffset);
				ew.Write(MetaOffset);
				ew.Write((int)BitmapType);

				ew.Write(0);
			}
		};
	};

	/// <summary>
	/// Class for handling Halo1 maps.
	/// </summary>
	public class MapFile
	{
		#region Header
		private MapHeader header;
		public MapHeader Header
		{
			get { return header; }
			set { header = value; }
		}
		#endregion

		#region IndexHeader
		private MapIndex indexHeader;
		public MapIndex IndexHeader
		{
			get { return indexHeader; }
			set { indexHeader = value; }
		}
		#endregion

		#region Magic
		private uint magic;
		public uint Magic
		{
			get 
			{ 
				if(useBspsIndex != -1)
					return BspsMagic[useBspsIndex];

				return magic; 
			}
			set { magic = value; }
		}

		private int useBspsIndex = -1;
		private uint[] bspsMagic;
		public uint[] BspsMagic
		{
			get { return bspsMagic; }
		}
		#endregion

		/// <summary>
		/// For IndexHeader
		/// </summary>
		private bool PC = false;

		#region IO Streams
		public EndianReader InputStream;

		public EndianWriter OutputStream;
		#endregion

		/// <summary>
		/// Creates the map streams and inits values
		/// </summary>
		/// <param name="map_name"></param>
		public MapFile(string map_name)
		{
			InputStream = new EndianReader(map_name, STREAM_TYPE.HALO1);
			OutputStream = new EndianWriter(map_name, STREAM_TYPE.HALO1);

			Header = new MapHeader();
			IndexHeader = new MapIndex();
			Magic = 0;
		}

		/// <summary>
		/// Reads the map values
		/// </summary>
		public void Read()
		{
			// Read header
			Header.Read(ref InputStream);

			// determine if this is a pc map or not
			PC = Header.Version != VERSION.XBOX;

			// Move to the index table
			InputStream.Seek(Header.OffsetToIndex, SeekOrigin.Begin);

			// Read the index table and index elements
			IndexHeader.Read(ref InputStream, PC, ref magic, ref bspsMagic);
		}

		/// <summary>
		/// Closes map streams
		/// </summary>
		public void Close()
		{
			InputStream.Close();
			OutputStream.Close();
		}

		/// <summary>
		/// Changes the magic to use, when extracting a bsp
		/// </summary>
		/// <param name="tag_index"></param>
		public void UseBspsMagic(int tag_index)
		{
			useBspsIndex = tag_index;
		}

		/// <summary>
		/// Finds a tag item based on a given id
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public MapIndex.MapIndexItem LocateByID(uint ID)
		{
			return ((MapIndex.MapIndexItem)IndexHeader.IDTable[ID]);
		}
	};

	#region RawData maps
	/// <summary>
	/// Class for handling HaloPC bitmaps.map file
	/// </summary>
	public class Bitmap
	{
		EndianReader file;

		/// <summary>
		/// Creates a bitmap raw data reader from a file name
		/// </summary>
		/// <param name="path"></param>
		public Bitmap(string path)
		{
			file = new EndianReader(path);
		}

		/// <summary>
		/// Gets the raw data of a bitmap from an offset
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="size"></param>
		/// <returns></returns>
		public byte[] GetBitmapData(uint offset, uint size)
		{
			file.Seek(offset, SeekOrigin.Begin);
			return file.ReadBytes(size);
		}
	};

	/// <summary>
	/// Class for handling HaloPC sounds.map file
	/// </summary>
	public class Sound
	{
		EndianReader file;

		/// <summary>
		/// Creates a sound raw data reader from a file name
		/// </summary>
		/// <param name="path"></param>
		public Sound(string path)
		{
			file = new EndianReader(path);
		}

		/// <summary>
		/// Gets the raw data of a sound from an offset
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="size"></param>
		/// <returns></returns>
		public byte[] GetSoundData(uint offset, uint size)
		{
			file.Seek(offset, SeekOrigin.Begin);
			return file.ReadBytes(size);
		}
	};
	#endregion
};

namespace Prometheus.Core.Maps.Halo2
{
	#region Enums
	/// <summary>
	/// Halo2 maptype enum
	/// </summary>
	public enum MAP_TYPE : uint
	{
		SINGLE_PLAYER = 0x0,
		MULTI_PLAYER = 0x1,
		MAIN_MENU = 0x2,
		MP_SHARED = 0x3,
		SP_SHARED = 0x4,
	};
	#endregion

	/// <summary>
	/// Header of a halo2 map
	/// </summary>
	public class MapHeader
	{
		#region Header Members
		#region Version
		private int version;
		/// <summary>
		/// Should always be 8
		/// </summary>
		public int Version
		{
			get { return version; }
			set { version = value; }
		}
		#endregion

		#region FileSize
		private uint filesize;
		/// <summary>
		/// The size of the map
		/// </summary>
		public uint FileSize
		{
			get { return filesize; }
			set { filesize = value; }
		}
		#endregion

		#region IndexOffset
		private uint indexOffset;
		/// <summary>
		/// The offset to the index
		/// </summary>
		public uint IndexOffset
		{
			get { return indexOffset; }
			set { indexOffset = value; }
		}
		#endregion

		#region MetaStart
		private uint metaStart;
		/// <summary>
		/// indexOffset + metaStart == start of meta
		/// </summary>
		public uint MetaStart
		{
			get { return metaStart; }
			set { metaStart = value; }
		}
		#endregion

		#region MapOrigin
		private string mapOrigin;
		/// <summary>
		/// 
		/// </summary>
		public string MapOrigin
		{
			get { return mapOrigin; }
			set { mapOrigin = value; }
		}
		#endregion

		#region Build
		public string build;
		/// <summary>
		/// Build # of the BlamEngine
		/// </summary>
		public string Build
		{
			get { return build; }
			set { build = value; }
		}
		#endregion

		#region MapType
		public MAP_TYPE maptype;
		/// <summary>
		/// The map's type
		/// </summary>
		public MAP_TYPE MapType
		{
			get { return maptype; }
			set { maptype = value; }
		}
		#endregion

		#region Dependecy List
		private uint dependecyTagListOffset;
		/// <summary>
		/// 
		/// </summary>
		public uint DependecyTagListOffset
		{
			get { return dependecyTagListOffset; }
			set { dependecyTagListOffset = value; }
		}
		#endregion

		#region Name List
		private uint nameListOffset;
		/// <summary>
		/// 
		/// </summary>
		public uint NameListOffset
		{
			get { return nameListOffset; }
			set { nameListOffset = value; }
		}

		private int nameListSize;
		/// <summary>
		/// 
		/// </summary>
		public int NameListSize
		{
			get { return nameListSize; }
			set { nameListSize = value; }
		}
		#endregion

		#region SizeOfStringsBeforeFileTable
		private uint sizeOfStringsBeforeFileTable;
		public uint SizeOfStringsBeforeFileTable
		{
			get { return sizeOfStringsBeforeFileTable; }
			set { sizeOfStringsBeforeFileTable = value; }
		}
		#endregion
		
		#region IntListOffset
		private uint intListOffset; 
		/// <summary>
		/// 
		/// </summary>
		public uint IntListOffset
		{
			get { return intListOffset; }
			set { intListOffset = value; }
		}
		#endregion

		#region CStringTableOffset
		private uint cstringTableOffset;
		/// <summary>
		/// 
		/// </summary>
		public uint CStringTableOffset
		{
			get { return cstringTableOffset; }
			set { cstringTableOffset = value; }
		}
		#endregion

		#region MapName
		private string mapName;
		/// <summary>
		/// Map name
		/// </summary>
		public string MapName
		{
			get { return mapName; }
			set { mapName = value; }
		}
		#endregion

		#region ScenarioPath
		private string scenarioPath;
		/// <summary>
		/// Tag path of the scenario
		/// </summary>
		public string ScenarioPath
		{
			get { return scenarioPath; }
			set { scenarioPath = value; }
		}
		#endregion

		#region FileCount
		private int fileCount;
		/// <summary>
		/// 
		/// </summary>
		public int FileCount
		{
			get { return fileCount; }
			set { fileCount = value; }
		}
		#endregion

		#region FileTableOffset
		private uint fileTableOffset;
		/// <summary>
		/// 
		/// </summary>
		public uint FileTableOffset
		{
			get { return fileTableOffset; }
			set { fileTableOffset = value; }
		}
		#endregion

		#region FileTableSize
		private uint fileTableSize;
		/// <summary>
		/// 
		/// </summary>
		public uint FileTableSize
		{
			get { return fileTableSize; }
			set { fileTableSize = value; }
		}
		#endregion

		#region FileOffsetsList
		private uint fileOffsetsList;
		/// <summary>
		/// 
		/// </summary>
		public uint FileOffsetsList
		{
			get { return fileOffsetsList; }
			set { fileOffsetsList = value; }
		}
		#endregion

		#region Signature
		private uint signature;
		/// <summary>
		/// Map's signature
		/// </summary>
		public uint Signature
		{
			get { return signature; }
			set { signature = value; }
		}
		#endregion
		#endregion

		public void Read(ref EndianReader br)
		{
			br.ReadInt32(); // 'daeh'

			version = br.ReadInt32();
			filesize = br.ReadUInt32();

			br.ReadInt32();

			indexOffset = br.ReadUInt32(); 
			metaStart = br.ReadUInt32();
			
			br.ReadInt32();  // unknown1a
			br.ReadInt32();  // unknown1b

			br.Position += 0x100;
			
			build = br.ReadString();
			maptype = (MAP_TYPE)br.ReadUInt32();

			br.ReadBytes(16); // unknown2 [16]
			br.ReadInt32(); // unknown3

			dependecyTagListOffset = br.ReadUInt32();
			br.ReadInt32(); // unknown

			nameListOffset = br.ReadUInt32();
			nameListSize = br.ReadInt32();
			sizeOfStringsBeforeFileTable = br.ReadUInt32(); 
			intListOffset = br.ReadUInt32();
			cstringTableOffset = br.ReadUInt32();

			br.ReadBytes(36); // unknown6 [36]
			
			mapName = br.ReadString();
			br.ReadInt32();
			scenarioPath = br.ReadCString();

			br.Seek(700, SeekOrigin.Begin);

			br.ReadInt32(); // unknown7
			fileCount = br.ReadInt32();
			fileTableOffset = br.ReadUInt32();
			fileTableSize = br.ReadUInt32();
			fileOffsetsList = br.ReadUInt32();
			signature = br.ReadUInt32();

			br.Seek(2048, SeekOrigin.Begin);
		}
	};

	/// <summary>
	/// 
	/// </summary>
	public class MapIndex
	{
		/// <summary>
		/// The index
		/// </summary>
		public MapIndexHeader IndexHeader;

		#region IndexItems
		/// <summary>
		/// The Index's items
		/// </summary>
		public MapIndexItem[] IndexItems;
		public MapIndexItem this [int index]
		{
			get { return IndexItems[index]; }
			set { IndexItems[index] = value; }
		}
		#endregion

		public Hashtable IDLookUp = new Hashtable();

		/// <summary>
		/// The index's tag class items
		/// </summary>
		public MapTagClassItem[] TagClassItem;

		private ArrayList bspList = new ArrayList();
		/// <summary>
		/// The bsps in this halo2 map
		/// </summary>
		public MapIndexItem[] bsps;
			
		public void Read(ref EndianReader br, ref uint[] BspsMagic)
		{
			IndexHeader = new MapIndexHeader();
			IndexHeader.Read(ref br);

			br.Seek(IndexHeader.CalculatedIndexOffset, SeekOrigin.Begin);
			IndexItems = new MapIndexItem[IndexHeader.TagCount];
			TagClassItem = new MapTagClassItem[IndexHeader.TagCountInTagList];

			for (int x = 0; x < IndexHeader.TagCount; x++)
			{
				IndexItems[x] = new MapIndexItem();
				IndexItems[x].Read(ref br);
				IndexItems[x].Index = x;
				IDLookUp.Add(IndexItems[x].ID, IndexItems[x]);
				if( TAG.TestTag(IndexItems[x].TagType, TagGroups.sbsp.Tag) )
					bspList.Add(IndexItems[x]);
			}

			bsps = new MapIndexItem[bspList.Count];
			int i = 0;
			foreach(MapIndexItem bsp in bspList)
			{
				bsps[i] = bsp;
				i++;
			}
		}

		/// <summary>
		/// Index header of halo2
		/// </summary>
		public class MapIndexHeader
		{
			#region Magic
			private uint primaryMagicValue;
			/// <summary>
			/// primaryMagic is this value,  
			/// subtract (mapHeader.indexOffset + sizeof(indexHeader)) 
			/// </summary>
			public uint Magic
			{
				get { return primaryMagicValue; }
				set { primaryMagicValue = value; }
			}

			private uint secondaryMagic;
			/// <summary>
			/// The second magic value of the map
			/// </summary>
			public uint SecondMagic
			{
				get { return secondaryMagic; }
				set { secondaryMagic = value; }
			}
			#endregion

			#region TagCountInTagList
			private int tagCountInTagList;
			/// <summary>
			/// 
			/// </summary>
			public int TagCountInTagList
			{
				get { return tagCountInTagList; }
				set { tagCountInTagList = value; }
			}
			#endregion

			#region RealOffset
			private uint realIndexOffset;
			/// <summary>
			/// Depending on the map, this value subtract primaryMagic, 
			/// will give the true offset of the index, or the size of the tagList 
			/// </summary>
			public uint RealOffset
			{
				get { return realIndexOffset; }
				set { realIndexOffset = value; }
			}
			#endregion

			#region ScenarioID
			private int scenarioID;
			/// <summary>
			/// 
			/// </summary>
			public int ScenarioID
			{
				get { return scenarioID; }
				set { scenarioID = value; }
			}
			#endregion

			#region TagIDStart
			private uint tagIDStart; 
			/// <summary>
			/// 0x000074E1: ID of first tag.
			/// </summary>
			public uint TagIDStart
			{
				get { return tagIDStart; }
				set { tagIDStart = value; }
			}
			#endregion

			#region Unknown
			private int unknown;
			/// <summary>
			/// 
			/// </summary>
			public int Unknown
			{
				get { return unknown; }
				set { unknown = value; }
			}
			#endregion

			#region TagCount
			private int tagCount;
			/// <summary>
			/// 
			/// </summary>
			public int TagCount
			{
				get { return tagCount; }
				set { tagCount = value; }
			}
			#endregion

			#region Tags
			private int tags;
			/// <summary>
			/// 
			/// </summary>
			public int Tags
			{
				get { return tags; }
				set { tags = value; }
			}
			#endregion

			#region Offset
			private uint _offset;
			/// <summary>
			/// used to denote the offset in the file of the index header.
			/// </summary>
			public uint Offset
			{
				get { return _offset; }
				set { _offset = value; }
			}
			#endregion

			private int bspCount = 0;
			public int BSPCount
			{
				get { return bspCount; }
			}

			/// <summary>
			/// Get the values from the map file
			/// </summary>
			/// <param name="br"></param>
			public void Read(ref EndianReader br)
			{
				_offset = br.Position;
				primaryMagicValue = br.ReadUInt32();
				tagCountInTagList = br.ReadInt32();
				realIndexOffset = br.ReadUInt32();
				scenarioID = br.ReadInt32();
				tagIDStart = br.ReadUInt32();
				unknown = br.ReadInt32();
				tagCount = br.ReadInt32();
				tags = br.ReadInt32();
			}

			/// <summary>
			/// 
			/// </summary>
			public uint CalculatedIndexOffset
			{
				get
				{
					uint offset = realIndexOffset - primaryMagicValue;
					offset += 32;
					if (offset < _offset)
						return _offset + offset;
					else
						return offset;
				}
			}
		};

		/// <summary>
		/// Index entry
		/// </summary>
		public class MapIndexItem
		{
			#region TrueOffset
			private uint _trueOffset;
			/// <summary>
			/// Item's true offset
			/// </summary>
			public uint TrueOffset
			{
				get { return _trueOffset; }
				set { _trueOffset = value; }
			}
			#endregion

			#region FileName
			private string _fileName;
			/// <summary>
			/// Item's tag file name
			/// </summary>
			public string FileName
			{
				get { return _fileName; }
				set { _fileName = value; }
			}
			#endregion

			#region TagType
			private char[] tag;
			/// <summary>
			/// The tag's tag type
			/// </summary>
			public char[] TagType
			{
				get { return tag; }
				set { tag = value; }
			}
			#endregion

			#region ID
			private uint id;
			/// <summary>
			/// The ID of this tag
			/// </summary>
			public uint ID
			{
				get { return id; }
				set { id = value; }
			}
			#endregion

			#region Offset
			private uint offset; 
			/// <summary>
			/// The offset to this tags meta
			/// </summary>
			public uint Offset
			{
				get { return offset; }
				set { offset = value; }
			}
			#endregion

			#region MetaSize
			private int metaSize;
			/// <summary>
			/// The size of the meta
			/// </summary>
			public int MetaSize
			{
				get { return metaSize; }
				set { metaSize = value; }
			}
			#endregion

			#region Index
			private int index;
			/// <summary>
			/// The position of this tag in the map file's IndexItems list
			/// </summary>
			public int Index
			{
				get { return index; }
				set { index = value; }
			}
			#endregion

			#region StringTableOffset
			private uint stringTableOffset;
			/// <summary>
			/// 
			/// </summary>
			public uint StringTableOffset
			{
				get { return stringTableOffset; }
				set { stringTableOffset = value; }
			}
			#endregion

			public void Read (ref EndianReader br)
			{
				tag = br.ReadTag();
				id = br.ReadUInt32();
				offset = br.ReadUInt32();
				metaSize = br.ReadInt32();
			}	
		};

		/// <summary>
		/// Tag class entry
		/// </summary>
		public struct MapTagClassItem
		{
			public char[] TagClass1;
			public char[] TagClass2;
			public char[] TagClass3;

			public void Read(ref EndianReader br)
			{
				TagClass3 = br.ReadTag();
				TagClass2 = br.ReadTag();
				TagClass1 = br.ReadTag();
			}
		};
	}

	/// <summary>
	/// Class for handling halo2 map files
	/// </summary>
	public class MapFile
	{
		#region Halo2Map Members
		public EndianReader InputStream;
		public EndianWriter OutputStream;

		#region Header
		private MapHeader header;
		public MapHeader Header
		{
			get { return header; }
			set { header = value; }
		}
		#endregion

		#region Index
		private MapIndex index;
		public MapIndex Index
		{
			get { return index; }
			set { index = value; }
		}
		#endregion

		#region Magic
		private uint magic;
		public uint Magic
		{
			get 
			{ 
				if(useBspsIndex != -1)
					return BspsMagic[useBspsIndex];

				return magic; 
			}
			set { magic = value; }
		}

		private int useBspsIndex = -1;
		private uint[] bspsMagic;
		public uint[] BspsMagic
		{
			get { return bspsMagic; }
		}
		#endregion

		public uint SecondaryMagic;
		#endregion Halo2Map Members

		#region Constructer
		public MapFile(string filename)
		{
			InputStream = new EndianReader(filename, STREAM_TYPE.HALO2);
			OutputStream = new EndianWriter(filename, STREAM_TYPE.HALO2);
		}
		#endregion

		public void Close()
		{
			InputStream.Close();
			OutputStream.Close();
		}

		public void Read()
		{
			header = new MapHeader();
			header.Read(ref InputStream);
			if (header.Version != 8) return;
			
			InputStream.Seek(header.IndexOffset, SeekOrigin.Begin);
			index = new MapIndex();
			index.Read(ref InputStream, ref bspsMagic);
			
			// Calculate the true Magic
			uint mStart = header.IndexOffset + header.MetaStart;
			Magic = index.IndexItems[0].Offset - mStart;
			SecondaryMagic = index.IndexItems[0].Offset - Magic;

			InputStream.Seek(header.IndexOffset, SeekOrigin.Begin);
			for(int x = 0; x < index.IndexHeader.TagCountInTagList; x++)
				index.TagClassItem[x].Read(ref InputStream);

			// Read the filenames and calculate true offset
			InputStream.Seek(header.FileTableOffset, SeekOrigin.Begin);
			for (int x = 0; x < index.IndexHeader.TagCount; x++)
			{
				index.IndexItems[x].TrueOffset = index.IndexItems[x].Offset - Magic;
				index.IndexItems[x].StringTableOffset = InputStream.Position;
				index.IndexItems[x].FileName = InputStream.ReadCString();
			}
		}

		#region Util
		/// <summary>
		/// Re-reads the map's signature
		/// </summary>
		public void UpdateSignature()
		{
			uint temp_pos = InputStream.Position; // keep our position just incase

			InputStream.Position = 0x2D0; // go to signature offset
			header.Signature = InputStream.ReadUInt32(); // re-read signature

			InputStream.Position = temp_pos; // go back to where we were
		}

		/// <summary>
		/// Checks a offset for a external map offset
		/// </summary>
		/// <param name="offset">offset to check</param>
		/// <returns>
		/// the external map that the offset needs. Returns the map header's
		/// map type if it doesn't need one
		/// </returns>
		public MAP_TYPE ExternalMapCheck(uint offset)
		{
			if( (offset + 0x80000000) > 0x40000000 )return MAP_TYPE.SP_SHARED;
			else if( offset < 0)					return MAP_TYPE.MP_SHARED;
			else if( offset > 0x40000000 )			return MAP_TYPE.MAIN_MENU;
			else									return header.MapType;
		}

		public MapIndex.MapIndexItem LocateByID(uint ID)
		{
			return ((MapIndex.MapIndexItem)this.index.IDLookUp[ID]);
		}

		public bool IsID(uint ID)
		{
			try
			{
				string bleh = LocateByID(ID).FileName;
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool IsOffset(long intOffset)
		{
			if (intOffset > Magic & intOffset <= (Magic + InputStream.Length)) 
				return true;
			else 
				return false;
		}
		#endregion Util
	};
};