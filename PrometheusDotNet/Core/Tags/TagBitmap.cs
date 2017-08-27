using System;
using System.IO;
using MDX = Microsoft.DirectX.Direct3D;

namespace Prometheus.Core.Tags.Bitm.New
{
	/// <summary>
	/// Base bitmap class
	/// </summary>
	public class BitmapGroup
	{
		#region Bitmap Constants
		public enum BitmapType : short
		{
			Texture2D = 0x0,
			Texture3D = 0x1,
			CubeMap = 0x2,
			White = 0x3,
			Sprites = 0x3,
			InterfaceBitmaps = 0x4
		};

		public enum BitmapFormat : short
		{
			CompressedWithColorKey,
			CompressedWithExplicitAlpha,
			CompressedWithInterpolatedAlpha,
			Bit16Color,
			Bit32Color,
			Monochrome
		};

		public enum BitmapDataFormat : short
		{
			A8 = 0x00,
			Y8 = 0x01,
			AY8 = 0x02,
			A8Y8 = 0x03,
			// 
			// 
			R5G6B5 = 0x06,
			// 
			A1R5G5B5 = 0x08,
			A4R4G4B4 = 0x09,
			X8R8G8B8 = 0x0A,
			A8R8G8B8 = 0x0B,
			// 
			// 
			Dxt1 = 0x0E,
			Dxt2 = 0x0F,
			Dxt4 = 0x10,
			P8 = 0x11,
			L8 = 0x12
		};

		public enum BitmapUsage : short
		{
			AlphaBlend,
			Default,
			HeightMap,
			DetailMap,
			LightMap,
			VectorMap
		};

		[Flags()]
		public enum BitmapFlags
		{
			EnableDiffusionDithering,
			DisableHeightMapCompression,
			UniformSpriteSequences,
			FilthySpriteBugFix
		};

		[Flags()]
		public enum BitmapDataFlags : short
		{
			PowerOfTwoDimensions = 1,
			Compressed = 2,
			Palettized = 4,
			Swizzled = 8,
			Linear = 16,
			V16_U16 = 32,
		};
		#endregion

		public BitmapType Type;
		public BitmapFormat Format;
		public BitmapUsage Usage;
		public short Flags;
		public int ColorPlateWidth;
		public int ColorPlateHeight;
		public int ColorPlateData;
		public int TIFFSize;

		public REFLEXIVE SequencesBlock = new REFLEXIVE();
		public REFLEXIVE BitmapsBlock = new REFLEXIVE();

		public virtual void Load(ref BinaryReader input)
		{
			Type = (BitmapType)input.ReadInt16();
			Format = (BitmapFormat)input.ReadInt16();
			Usage = (BitmapUsage)input.ReadInt16();
			input.BaseStream.Position += 16;
			ColorPlateWidth = input.ReadInt16();
			ColorPlateHeight = input.ReadInt16();
			ColorPlateData = input.ReadInt32();
		}


		#region Bitmap Base Blocks
		/// <summary>
		/// bitmap_group_sequence_block
		/// </summary>
		public class SequenceBlock
		{
			/// <summary>
			/// bitmap_group_sprite_block
			/// </summary>
			public class BitmapSpriteBlock
			{
				public virtual void Load(ref BinaryReader input)
				{
				}
			};

			public virtual void Load(ref BinaryReader input)
			{
			}
		};

		/// <summary>
		/// bitmap_data_block
		/// </summary>
		public class BitmapBlock
		{
			public uint Signature;			// 'bitm'
			public short Width;
			public short Height;
			public short Depth;
			public BitmapType Type;
			public BitmapDataFormat Format;
			public BitmapDataFlags Flags;
			public short RegPointX;
			public short RegPointY;
			public short NumMipmaps;
			public short PixelOffset;
			public byte[] DDSData;
			public MDX.Format DxTextureFormat;

			public virtual int BitmapSize()
			{
				return 0;
			}

			public virtual void Load(ref BinaryReader input)
			{
				Signature = input.ReadUInt32();
				Width = input.ReadInt16();
				Height = input.ReadInt16();
				Depth = input.ReadInt16();
				Type = (BitmapType)input.ReadInt16();
				Format = (BitmapDataFormat)input.ReadInt16();
				Flags = (BitmapDataFlags)input.ReadInt16();
				RegPointX = input.ReadInt16();
				RegPointY = input.ReadInt16();
				NumMipmaps = input.ReadInt16();
				PixelOffset = input.ReadInt16();
			}
		};
		#endregion
	};

	/// <summary>
	/// Halo1 bitmap class
	/// </summary>
	public class BitmapGroupHalo1 : BitmapGroup
	{
		public int ProcessedPixelData;

		public override void Load(ref BinaryReader input)
		{
			base.Load (ref input);
			input.BaseStream.Position += 16;
			ProcessedPixelData = input.ReadInt32();
			input.BaseStream.Position += 4;
			TIFFSize = input.ReadInt32();
			input.BaseStream.Position += 24;
			SequencesBlock.Load(ref input);
			BitmapsBlock.Load(ref input);
		}

		
		#region bitmap_data_block
		/// <summary>
		/// Halo1 bitmap_data_block
		/// </summary>
		public class BitmapDataBlock : BitmapGroup.BitmapBlock
		{
			public int Offset;
			public int Size;

			public override int BitmapSize()
			{
				return Size;
			}

			public override void Load(ref BinaryReader input)
			{
				base.Load (ref input);
				Offset = input.ReadInt32();
				Size = input.ReadInt32();
				input.BaseStream.Position += 16;

				base.DxTextureFormat = (MDX.Format)Enum.Parse(typeof(MDX.Format), base.Type.ToString());
			}
		};
		#endregion
	};

	/// <summary>
	/// Halo2 bitmap class
	/// </summary>
	public class BitmapGroupHalo2 : BitmapGroup
	{
		public override void Load(ref BinaryReader input)
		{
			base.Load (ref input);
			input.BaseStream.Position += 36;
			BitmapsBlock.Load(ref input);
		}

		
		#region bitmap_data_block
		/// <summary>
		/// Halo2 bitmap_data_block
		/// </summary>
		public class BitmapDataBlock : BitmapGroup.BitmapBlock
		{
			public int[] Offset = new int[6];
			public int[] Size = new int[6];

			public override int BitmapSize()
			{
				return Size[0];
			}

			public override void Load(ref BinaryReader input)
			{
				base.Load (ref input);
				input.BaseStream.Position += 4;

				for (int x = 0; x < 6; x++)
					Offset[x] = input.ReadInt32();
				for (int x = 0; x < 6; x++)
					Size[x] = input.ReadInt32();

				base.DxTextureFormat = (MDX.Format)Enum.Parse(typeof(MDX.Format), base.Type.ToString());
			}
		};
		#endregion
	};

	public class BitmapHalo1 : TagBase
	{
		public new BitmapGroupHalo1 Header;
		public BitmapGroupHalo1.BitmapDataBlock[] Bitmaps;

		public void Load()
		{
			BinaryReader input = new BinaryReader(m_stream);
			Header.Load(ref input);

			input.BaseStream.Seek(Header.BitmapsBlock.Unknown, SeekOrigin.Begin);
			Bitmaps = new BitmapGroupHalo1.BitmapDataBlock[Header.BitmapsBlock.Count];
			for (int x = 0; x < Header.BitmapsBlock.Count; x++)
			{
				Bitmaps[x] = new BitmapGroupHalo1.BitmapDataBlock();
				Bitmaps[x].Load(ref input);
			}

			// Now load the raw data
			for (int x = 0; x < Header.BitmapsBlock.Count; x++)
			{
				Bitmaps[x].DDSData = new byte[Bitmaps[x].Size];
				input.BaseStream.Seek(Bitmaps[x].Offset + Header.TIFFSize, SeekOrigin.Begin);
				Bitmaps[x].DDSData = input.ReadBytes(Bitmaps[x].Size);
			}
		}
	};

	public class BitmapHalo2 : TagBase
	{
		public new BitmapGroupHalo2 Header;
		public BitmapGroupHalo2.BitmapDataBlock[] Bitmaps;

		public void Load()
		{
			BinaryReader input = new BinaryReader(m_stream);
			Header.Load(ref input);

			input.BaseStream.Seek(Header.BitmapsBlock.Unknown, SeekOrigin.Begin);
			Bitmaps = new BitmapGroupHalo2.BitmapDataBlock[Header.BitmapsBlock.Count];
			for (int x = 0; x < Header.BitmapsBlock.Count; x++)
			{
				Bitmaps[x] = new BitmapGroupHalo2.BitmapDataBlock();
				Bitmaps[x].Load(ref input);
			}

			// Now load the raw data
			for (int x = 0; x < Header.BitmapsBlock.Count; x++)
			{
				Bitmaps[x].DDSData = new byte[Bitmaps[x].Size[0]];
				input.BaseStream.Seek(Bitmaps[x].Offset[0] + Header.TIFFSize, SeekOrigin.Begin);
				Bitmaps[x].DDSData = input.ReadBytes(Bitmaps[x].Size[0]);

				for (int s = 1; s < 6; s++)
					input.BaseStream.Position += Bitmaps[x].Size[s];
			}
		}
	};

	public struct ColorARGB
	{
		public uint R, G, B, A;

		public uint ToUInt()
		{
			return (A << 24) | (R << 16) | (G << 8) | B;
		}

		public void FromShort(ushort color)
		{
			R = (uint)(((color >> 11) & 0x1F) * 0xFF) / 31;
			G = (uint)(((color >>  5) & 0x3F) * 0xFF) / 63;
			B = (uint)(((color >>  0) & 0x1F) * 0xFF) / 31;
			A = 255;
		}
	};

	public class Swizzler
	{
		public int	MaskX,
					MaskY,
					MaskZ;

		public Swizzler(int width, int height, int depth)
		{
			for(int Bit=1, Idx=1; (Bit < width) || (Bit < height) || (Bit < depth); Bit <<= 1)
			{
				if (Bit < width)
				{
					MaskX |= Idx;
					Idx <<= 1;
				}

				if (Bit < height)
				{
					MaskY |= Idx;
					Idx <<= 1;
				}

				if (Bit < depth)
				{
					MaskZ |= Idx;
					Idx <<= 1;
				}
			}
		}

		public uint Swizzle(int sx, int sy)
		{
			return Swizzle(sx, sy, -1);
		}

		public uint Swizzle(int sx, int sy, int sz)
		{
			return	SwizzleAxis(sx, MaskX) | 
					SwizzleAxis(sy, MaskY) | 
					(
						(sz != -1) 
						? 
							SwizzleAxis(sz, MaskZ) 
							: 
							0
					);
		}

		private uint SwizzleAxis(int value, int mask)
		{
			uint result = 0;

			for(int Bit = 1; Bit <= mask; Bit <<= 1)
			{
				if((mask & Bit) != 0)
					result |= (uint)(value & Bit);
				else
					value <<= 1;
			}

			return result;
		}
	};

	public class DeSwizzler
	{
		public short Width;
		public short Height;
		public short Depth;

		public void DeSwizzle(ref byte[] data, uint bitCount)
		{
			Swizzler swiz = new Swizzler(Width, Height, Depth);
			uint tmp1;
			uint tmp2;
			byte[] swizData = new byte[data.Length];
			for(ushort x = 0; x < Width; x++) 
			{
				for(int y = 0; y < Height; y++)
				{
					tmp1 = (uint)((y * Width) + x) * (bitCount / 8);
					tmp2 = (uint)(swiz.Swizzle(x, y, -1)) * (bitCount / 8);

					for(int i = 0; i < (bitCount / 8); i++) 
						swizData[tmp1 + i] = data[tmp2 + i];
				}
			}

			for(int i = 0;i < data.Length; i++)
				data[i] = swizData[i];
		}
	};

	public class DDSFile
	{
		public void Save(ref BinaryWriter output, ref BitmapGroup.BitmapBlock image)
		{
			Header head = new Header();
			head.Generate(ref image);
			head.Save(ref output);
		}

		public enum DDSEnum : int
		{
			DDSD_CAPS = 1,
			DDSD_HEIGHT = 2,
			DDSD_WIDTH = 4,
			DDSD_PITCH = 8,
			DDSD_PIXELFORMAT = 4096,
			DDSD_MIPMAPCOUNT = 131072,
			DDSD_LINEARSIZE = 524288,
			DDSD_DEPTH = 8388608,
			DDPF_ALPHAPIXELS = 1,
			DDPF_FOURCC = 4,
			DDPF_RGB = 64,
			DDSCAPS_COMPLEX = 8,
			DDSCAPS_TEXTURE = 4096,
			DDSCAPS_MIPMAP = 4194304,
			DDSCAPS2_CUBEMAP = 512,
			DDSCAPS2_CUBEMAP_POSITIVEX = 1024,
			DDSCAPS2_CUBEMAP_NEGATIVEX = 2048,
			DDSCAPS2_CUBEMAP_POSITIVEY = 4096,
			DDSCAPS2_CUBEMAP_NEGATIVEY = 8192,
			DDSCAPS2_CUBEMAP_POSITIVEZ = 16384,
			DDSCAPS2_CUBEMAP_NEGATIVEZ = 32768,
			DDSCAPS2_VOLUME = 2097152,
		};

		public class Header
		{
			public char[] Signature; // 4
			public DDSURFACEDESC2 ddsd;

			public void Save(ref BinaryWriter output)
			{
				output.Write("DDS ".ToCharArray());
				ddsd.Save(ref output);
			}

			public void Generate(ref BitmapGroup.BitmapBlock image)
			{
				ddsd.Generate(ref image);
			}
		};

		public class DDSURFACEDESC2
		{
			int size_of_structure;
			public int Flags;
			public int Height;
			public int Width;
			public int PitchOrLinearSize;
			public int Depth;
			public int MipMapCount;
			public int[] Reserved1;
			public DDPIXELFORMAT ddfPixelFormat;
			public DDSCAPS2 ddsCaps;
			public int Reserved2;

			public void Save(ref BinaryWriter output)
			{
				output.Write(size_of_structure);
				output.Write(Flags);
				output.Write(Height);
				output.Write(Width);
				output.Write(PitchOrLinearSize);
				output.Write(Depth);
				output.Write(MipMapCount);
				for (int x = 0 ; x <= 10; x++) 
					output.Write(Reserved1[x]);

				ddfPixelFormat.Save(ref output);
				ddsCaps.Save(ref output);
				output.Write(Reserved2);
			}

			public void Generate(ref BitmapGroup.BitmapBlock image)
			{
				size_of_structure = 124;
				Flags += (int)DDSEnum.DDSD_CAPS;
				Flags += (int)DDSEnum.DDSD_PIXELFORMAT;
				Flags += (int)DDSEnum.DDSD_WIDTH;
				Flags += (int)DDSEnum.DDSD_HEIGHT;

				if (image.Format == BitmapGroup.BitmapDataFormat.Dxt1 ||
					image.Format == BitmapGroup.BitmapDataFormat.Dxt2 ||
					image.Format == BitmapGroup.BitmapDataFormat.Dxt4) 
				{
					Flags += (int)DDSEnum.DDSD_LINEARSIZE;
				} 
				else 
				{
					Flags += (short)DDSEnum.DDSD_PITCH;
				}

				Height = image.Height;
				Width = image.Width;
				int RGBBitCount = 0;
				if (image.Format == BitmapGroup.BitmapDataFormat.R5G6B5) 
					RGBBitCount = 16;
				else if (image.Format == BitmapGroup.BitmapDataFormat.A1R5G5B5) 
					RGBBitCount = 16;
				else if (image.Format == BitmapGroup.BitmapDataFormat.A4R4G4B4) 
					RGBBitCount = 16;
				else if (image.Format == BitmapGroup.BitmapDataFormat.X8R8G8B8) 
					RGBBitCount = 32;
				else if (image.Format == BitmapGroup.BitmapDataFormat.A8R8G8B8) 
					RGBBitCount = 32;

				if (((short)image.Flags & (short)DDSEnum.DDSD_PITCH) > 0) 
					PitchOrLinearSize = image.Width * (RGBBitCount / 8);

				if (((short)image.Flags & (int)DDSEnum.DDSD_LINEARSIZE) > 0) 
					PitchOrLinearSize = image.BitmapSize();

				Depth = 0;
				MipMapCount = image.NumMipmaps;
				if (MipMapCount > 0) 
					Flags += (int)DDSEnum.DDSD_MIPMAPCOUNT;

				Reserved1 = new int[11];
				Reserved1[0] = 0;
				Reserved1[1] = 0;
				Reserved1[2] = 0;
				Reserved1[3] = 0;
				Reserved1[4] = 0;
				Reserved1[5] = 0;
				Reserved1[6] = 0;
				Reserved1[7] = 0;
				Reserved1[8] = 0;
				Reserved1[9] = 0;
				Reserved1[10] = 0;
				ddfPixelFormat.Generate(ref image);
				ddsCaps.Generate(ref image);
			}
		};

		public class DDPIXELFORMAT
		{
			public int size;
			public int Flags;
			public string FourCC;
			public int RGBBitCount;
			public int RBitMask;
			public int GBitMask;
			public int BBitMask;
			public int RGBAlphaBitMask;

			public void Save(ref BinaryWriter output)
			{
				output.Write(size);
				output.Write(Flags);
				output.Write(FourCC.ToCharArray());
				output.Write(RGBBitCount);
				output.Write(RBitMask);
				output.Write(GBitMask);
				output.Write(BBitMask);
				output.Write(RGBAlphaBitMask);
			}

			public void Generate(ref BitmapGroup.BitmapBlock image)
			{
				size = 32;
				Flags = 0;
				if (image.Format == BitmapGroup.BitmapDataFormat.Dxt1) 
				{
					FourCC = "DXT1";
					Flags += (short)DDSEnum.DDPF_FOURCC;
				} 
				else if (image.Format == BitmapGroup.BitmapDataFormat.Dxt2) 
				{
					FourCC = "DXT3";
					Flags += (short)DDSEnum.DDPF_FOURCC;
				} 
				else if (image.Format == BitmapGroup.BitmapDataFormat.Dxt4) 
				{
					FourCC = "DXT4";
					Flags += (short)DDSEnum.DDPF_FOURCC;
				} 
				else 
				{
					FourCC = "\0\0\0\0";
					Flags += (short)DDSEnum.DDPF_RGB;
				}
				if (image.Format == BitmapGroup.BitmapDataFormat.R5G6B5) 
				{
					RGBBitCount = 16;
				} 
				else if (image.Format == BitmapGroup.BitmapDataFormat.A1R5G5B5) 
				{
					RGBBitCount = 16;
					Flags += (short)DDSEnum.DDPF_ALPHAPIXELS;
				} 
				else if (image.Format == BitmapGroup.BitmapDataFormat.A4R4G4B4) 
				{
					RGBBitCount = 16;
					Flags += (short)DDSEnum.DDPF_ALPHAPIXELS;
				} 
				else if (image.Format == BitmapGroup.BitmapDataFormat.X8R8G8B8) 
				{
					RGBBitCount = 32;
					Flags += (short)DDSEnum.DDPF_ALPHAPIXELS;
				} 
				else if (image.Format == BitmapGroup.BitmapDataFormat.A8R8G8B8) 
				{
					RGBBitCount = 32;
					Flags += (short)DDSEnum.DDPF_ALPHAPIXELS;
				} 
				else 
				{
					RGBBitCount = 0;
				}
				if (image.Format == BitmapGroup.BitmapDataFormat.R5G6B5) 
				{
					RBitMask = 63488;
					GBitMask = 2016;
					BBitMask = 31;
					RGBAlphaBitMask = 0;
				} 
				else if (image.Format == BitmapGroup.BitmapDataFormat.A1R5G5B5) 
				{
					RBitMask = 31744;
					GBitMask = 992;
					BBitMask = 31;
					RGBAlphaBitMask = 32768;
				} 
				else if (image.Format == BitmapGroup.BitmapDataFormat.A4R4G4B4) 
				{
					RBitMask = 61440;
					GBitMask = 240;
					BBitMask = 15;
					RGBAlphaBitMask = 15728640;
				} 
				else if (image.Format == BitmapGroup.BitmapDataFormat.X8R8G8B8) 
				{
					RBitMask = 1044480;
					GBitMask = 65280;
					BBitMask = 255;
					RGBAlphaBitMask = (int)278190080;
				} 
				else if (image.Format == BitmapGroup.BitmapDataFormat.A8R8G8B8) 
				{
					RBitMask = 16711680;
					GBitMask = 65280;
					BBitMask = 255;
					//RGBAlphaBitMask = (int)4278190080;
				} 
				else 
				{
					RBitMask = 0;
					GBitMask = 0;
					BBitMask = 0;
					RGBAlphaBitMask = 0;
				}
			}
		};

		public class DDSCAPS2
		{
			public int caps1;
			public int caps2;
			public int[] Reserved;

			public void Save(ref BinaryWriter output)
			{
				output.Write(caps1);
				output.Write(caps2);
				output.Write(Reserved[0]);
				output.Write(Reserved[1]);
			}

			public void Generate(ref BitmapGroup.BitmapBlock image)
			{
				caps1 = caps1 + (short)DDSEnum.DDSCAPS_TEXTURE;
				if (image.NumMipmaps > 0) 
				{
					caps1 = caps1 + (int)DDSEnum.DDSCAPS_MIPMAP;
				}
				caps1 = caps1 + (short)DDSEnum.DDSCAPS_COMPLEX;
				caps2 = 0;
				Reserved = new int[2];
				Reserved[0] = 0;
				Reserved[1] = 0;
			}
		}
	};
}