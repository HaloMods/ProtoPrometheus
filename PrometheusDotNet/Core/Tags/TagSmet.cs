using System;
using System.IO;

namespace Prometheus.Core.Tags
{
	//declare your "structure" classes here, use all caps for these class names
	//this way we know it is a simple structure class, not something more complex like the entire tag

	//ie, SOSO_HEADER

	/// <summary>
	/// Summary description for TagSmet.
	/// </summary>
	public class TagSmet : TagBase
	{
		//Radiosity Properties
		public ushort Radiosity_flags; // SingleParameterization = 1, IgnoreNormals = 2, TransparentLit = 3
		public ushort Detail_level; // High = 0, Medium = 1, Low = 2, Turd = 3
		public float Power;
		public COLOR Emitted = new COLOR();
		public COLOR Tint = new COLOR();

		// Physics Properties
		public ushort Material_Type;
        
		// Meter Shader
		public ushort Meter_flags; // add blah here
		public TAG_REFERENCE Map = new TAG_REFERENCE();
		
		// Colors
		public COLOR Gradient_min = new COLOR();
		public COLOR Gradient_max = new COLOR();
		public COLOR Background = new COLOR();
		public COLOR Flash = new COLOR();
		public COLOR Colors_Tint = new COLOR();
		public float Meter_transparency;
		public float Background_transparency;

		// External Function Sources
		public ushort Meter_brightness;
		public ushort Flash_brightness;
		public ushort Value_source;
		public ushort Gradient_source;
		public ushort Flash_extensive;

		public void LoadTagData()
		{
			BinaryReader br = new BinaryReader(m_stream);

			Radiosity_flags = br.ReadUInt16();
			Detail_level = br.ReadUInt16();
			Power = br.ReadSingle();
			Emitted.Load(ref br);
			Tint.Load(ref br);
			br.BaseStream.Position += 2;
			Material_Type = br.ReadUInt16();
			br.BaseStream.Position += 4;
			Meter_flags = br.ReadUInt16();
			br.BaseStream.Position += 34;
			Map.Load(ref br);
			br.BaseStream.Position += 32;
			Gradient_min.Load(ref br);
			Gradient_max.Load(ref br);
			Background.Load(ref br);
			Flash.Load(ref br);
			Colors_Tint.Load(ref br);
			Meter_transparency = br.ReadSingle();
			Background_transparency = br.ReadSingle();
			br.BaseStream.Position += 24;
			Meter_brightness = br.ReadUInt16();
			Flash_brightness = br.ReadUInt16();
			Value_source = br.ReadUInt16();
			Gradient_source = br.ReadUInt16();
			Flash_extensive = br.ReadUInt16();

		}
	}
}
