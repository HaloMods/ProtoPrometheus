using System;
using System.IO;
namespace Prometheus.Core.Compiler
{
	/// <summary>
	/// Summary description for utility.
	/// </summary>
	public class CompUtil
	{
		public CompUtil()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		static public void SwapInt(ref byte[] Array,uint Offset)
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
		public static void SwapShort(ref byte[] Array,uint Offset)
		{
			if (Array.Length - 2  >= Offset)
			{
				byte st;
				st = Array[Offset +1];
				Array[Offset+1] = Array[Offset];
				Array[Offset] = st;
			}
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
		static public uint RevUInt(uint Value)
		{
			byte[] tmp = BitConverter.GetBytes(Value);
			SwapInt(ref tmp,0);
			return BitConverter.ToUInt32(tmp,0);
		}
		static public string ReadString(byte[] Array)
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

		static public string GetTagClass(byte[] Array,uint Offset)
		{
			string NewString = "";
			for (uint count = 4; count > 0; count -=1)
			{
				NewString += (char)Array[count + Offset-1];
			}
			return NewString;
		}
		public static void FixByteArrayIntPosition(ref byte[] FX)
		{
			if ((FX.Length % 0x04) != 0)
			{
				uint Size = (0x04 - ((uint)FX.Length % 0x04));
				byte[] tmp = new byte[Size];
				for(int i = 0; i<tmp.Length;i++)
				{
					tmp[i] = 0xca;
				}
				byte[] SaveArray = new byte[FX.Length + Size];
				FX.CopyTo(SaveArray,0);
				FX = new byte[FX.Length + Size];
				SaveArray.CopyTo(FX,0);
				tmp.CopyTo(FX,FX.Length - Size);
			}
		}
		public static uint Val(string Hex)
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
			if (data.Length - 4 >= offset )
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

		public static string GetTagClassRev(byte[] Array,uint Offset)
		{
			string NewString = "";
			for (uint count = 4; count > 0; count -=1)
			{
				NewString += (char)Array[(count-1) + Offset];
			}
			return NewString;
		}
		public static string ReadString(byte[] Array,uint Offset)
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
					tmp[i] = 0xca;
				}
				FX.Write(tmp,0,tmp.Length);
			}
		}
		
		public static void Fix0x20Position(ref FileStream FX)
		{
			if ((FX.Position % 0x20) != 0)
			{
				uint Size = (0x20 - ((uint)FX.Position % 0x20));
				byte[] tmp = new byte[Size];
				for(int i = 0; i<tmp.Length;i++)
				{
					tmp[i] = 0x2D;
				}
				FX.Write(tmp,0,tmp.Length);
			}
		}
		public static void Fix0x100Position(ref FileStream FX)
		{
			if ((FX.Position % 0x200) != 0)
			{
				uint Size = (0x200 - ((uint)FX.Position % 0x200));
				byte[] tmp = new byte[Size];
				for(int i = 0; i<tmp.Length;i++)
				{
					tmp[i] = 0x00;
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
		public static void StringToByteArray(ref byte[] Dest,string Src)
		{
			char[] tmp = new char[Src.Length];
			Src.CopyTo(0,tmp,0,tmp.Length);
			for(int i = 0;i < tmp.Length;i++)
			{
				Dest[i] = (byte)tmp[i];
			}
		}


		public static uint SwapUInt(uint Value)
		{
			byte[] tmp = BitConverter.GetBytes(Value);
			SwapInt(ref tmp,0);
			return BitConverter.ToUInt32(tmp,0);
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
	}
}
