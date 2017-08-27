using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Prometheus.Core.Compiler
{
	/// <summary>
	/// Summary description for MTSFReader.
	/// </summary>
	public class MTSFReader
	{
		public MTSFReader()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public struct sItems
		{
			byte[] data;
			public uint Class;
			public uint Size;
			public uint Offset; 
			public void Read(ref BinaryReader br)
			{
				data = new byte[0x0C];
				data = br.ReadBytes(0x0C);
				GetUInt(data,0x00,ref Class);
				if (Class < 0x20000000)
					Class += 0x20000000;
				GetUInt(data,0x04,ref Size);
				GetUInt(data,0x08,ref Offset);
			}
		}
		public uint Current;

		public sItems[] Items;
		public BinaryReader br;
		public Stream s;
		public void MTSFRead(string MTSF_File_Name)
		{
			//"Core.Compiler.HALO_PC_SET.MTSF"//
			//"Core.Compiler.TAG_CONVERSION_SET.MTSF"//
			s = Assembly.GetExecutingAssembly().GetManifestResourceStream(MTSF_File_Name);
			br = new BinaryReader(s);
			byte[] Head = new byte[0x30];
			Head = br.ReadBytes(0x30);
			byte[] VersionHead = new byte[0x08];
			VersionHead = br.ReadBytes(0x08);
			uint PCCount = BitConverter.ToUInt32(VersionHead,0);
			uint PCOffset = BitConverter.ToUInt32(VersionHead,4);
			Items = new sItems[PCCount];
			for(int i = 0;i<PCCount;i++)
			{
				Items[i].Read(ref br);
			}
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
	}
	public class TSFReader
	{
		//Normal Tag Handler Commands//
		public const byte TSFStruct = 0xA0;
		public const byte TSFTagRef = 0xA1;
		public const byte TSFBSPTagRef = 0xA8;
		public const byte TSFInternalRaw = 0xA3;
		public const byte TSFBitmapRaw = 0xA9;
		public const byte TSFSoundData = 0xAA;
		public const byte TSFModelData = 0xAB;
		public const byte TSFBSPModel = 0xAC;
		public const byte TSFResource = 0xAD;
		public const byte TSFEnd = 0xA7;
		public const byte TSFRawXModelData = 0xB0;
		//Tag Conversion Handler Commands//
		public const byte TSFIntSwap = 0xC0;
		public const byte TSFIntSwapRange = 0xC1;
		public const byte TSFLongSwap = 0xC2;
		public const byte TSFLongSwapRange = 0xC3;

		public string Name;
		public struct sTSF
		{
			byte[] data;
			public byte CMD;
			public ushort OIP;
			public ushort uName;
			public ushort SOC;
			public void Read(ref BinaryReader br)
			{
				data = new byte[0x07];
				data = br.ReadBytes(0x07);
				CMD = data[0];
				GetUShort(data,1 + 0x00,ref OIP);
				GetUShort(data,1 + 0x02,ref uName);
				GetUShort(data,1 + 0x04,ref SOC);
			}
		}
		public sTSF[] cTSF = new sTSF[0];
		public uint Position;
		public void TSF(ref MTSFReader MTSF,uint Class)
		{
			//Find Class in MTSF
			bool FoundTSF = false;
			int i=0;
			if (Class == 0x696d6566)
				Class = 0x72746361;
			do
			{
				if(MTSF.Items[i].Class == Class || MTSF.Items[i].Class == SwapUInt(Class))
					FoundTSF = true;
				i +=1;
			}while (FoundTSF != true);
			i -=1;
			MTSF.br.BaseStream.Seek((long)MTSF.Items[i].Offset,SeekOrigin.Begin);
			byte[] StringBuff = new byte[0x40];  //ReadString();
			StringBuff = MTSF.br.ReadBytes(0x40);
			ASCIIEncoding enc = new ASCIIEncoding();
			byte bZero = 0;
			char[] Zero = new char[1];
			Zero[0] = (char)bZero;
			Name = enc.GetString(StringBuff).Trim(Zero[0]);
			uint CMDSCount = (MTSF.Items[i].Size - 0x40) / 0x07;
			cTSF = new sTSF[CMDSCount];
			MTSF.br.BaseStream.Seek((long)MTSF.Items[i].Offset + 0x40,SeekOrigin.Begin);
			for(int c = 0;c < CMDSCount;c++)
			{
				cTSF[c].Read(ref MTSF.br);
			}		
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
		private uint SwapUInt(uint Value)
		{
			byte[] tmp = BitConverter.GetBytes(Value);
			SwapInt(tmp,0);
			return BitConverter.ToUInt32(tmp,0);
		}
		public void Seek(uint pos)
		{
			Position = pos;
		}
		public void SeekTo(ushort CMDName)
		{
			uint c=0;
			do
			{
				c+=1;
			}while(CMDName != cTSF[c].uName);
			Position = (uint)c;
		}
		public void SeekAheadTo(byte CMD,ushort uName)
		{
			uint i = Position;
			bool tmpTest = false;
			do
			{
				i+=1;
				if (cTSF[i].CMD == CMD && cTSF[i].OIP == uName)
					tmpTest = true;
			}while(tmpTest != true);
			Position = i;
		}
		public void Read()
		{
			Position +=1;
		}
		public byte GetCMD()
		{
			return cTSF[Position].CMD;
		}
		public ushort GetUName()
		{
			return cTSF[Position].uName;
		}
		public ushort GetOIP()
		{
			return cTSF[Position].OIP;
		}
		public ushort GetSOC()
		{
			return cTSF[Position].SOC;
		}
		public static void GetUShort(byte[] data,uint offset,ref ushort Value)
		{
			Value = BitConverter.ToUInt16(data,(int)offset);
		}
		public static ushort GetUShort(byte[] data,uint offset)
		{
			return BitConverter.ToUInt16(data,(int)offset);
		}
	}
}
