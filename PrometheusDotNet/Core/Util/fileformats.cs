using System;
using System.IO;
using System.Runtime.InteropServices;

namespace File_Formats
{
	#region 3D Studio Max 6 file format.
	/// <summary>
	/// 3D Studio Max 6 file structures.
	/// Header + space = 512 bytes.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	struct MAX_HEADER
	{
		public long id; //Always = D0CF11E0A1B11AE1
	}
	[StructLayout(LayoutKind.Sequential)]
	struct MAX_BODY
	{
		public long unknown1;
	}
	[StructLayout(LayoutKind.Sequential)]
	struct MAX_FOOTER
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
		public string data;
	}

	struct MAX_FILE
	{
		public MAX_HEADER header;
		public MAX_BODY body;
		public MAX_FOOTER footer;
	}
	#endregion

	class Max6_File
	{
		private BinaryReader br;
		public MAX_FILE file;
		private MAX_HEADER header = new MAX_HEADER();
		private MAX_BODY body = new MAX_BODY();
		private MAX_FOOTER footer = new MAX_FOOTER();

		public Max6_File(string filen)
		{
			try
			{
				FileStream file = new FileStream(filen, FileMode.Open, FileAccess.Read);
				br = new BinaryReader(file);
				//Header
				byte[] buffer = new byte[Marshal.SizeOf(header)];
				buffer = br.ReadBytes(Marshal.SizeOf(header));
				header = (MAX_HEADER)RawDeserialize(buffer,header.GetType());
				//Body
				br.BaseStream.Seek(512, SeekOrigin.Begin);
				buffer = new byte[Marshal.SizeOf(body)];
				buffer = br.ReadBytes(Marshal.SizeOf(body));
				body = (MAX_BODY)RawDeserialize(buffer, body.GetType());
				//Footer
				br.BaseStream.Seek(131584, SeekOrigin.Begin); //Need to dynamically update based on body+header.
				buffer = new byte[Marshal.SizeOf(footer)];
				buffer = br.ReadBytes(Marshal.SizeOf(footer));
				footer = (MAX_FOOTER)RawDeserialize(buffer, footer.GetType());
				Console.WriteLine(footer.data);
				br.Close();
				string temp = Console.ReadLine();
			}
			catch(Exception e)
			{
				Console.WriteLine(e.ToString());
				string temp = Console.ReadLine();
			}
		}
		
		//public void Swap(int n)
		//{
		//	return (((n) << 24) | (((n) & 0xff00) << 8) | (((n) >> 8) & 0xff00) | ((n) >> 24));
		//}

		private object RawDeserialize( byte[] rawdatas, Type anytype )
		{
			int rawsize = Marshal.SizeOf( anytype );
			if( rawsize > rawdatas.Length )
				return null;
			IntPtr buffer = Marshal.AllocHGlobal( rawsize );
			Marshal.Copy( rawdatas, 0, buffer, rawsize );
			object retobj = Marshal.PtrToStructure( buffer, anytype );
			Marshal.FreeHGlobal( buffer );
			return retobj;
		}

		public MAX_FILE GetFile()
		{
			file = new MAX_FILE();
			file.header = this.header;
			file.body = this.body;
			file.footer = this.footer;
			return file;
		}

	}
}
