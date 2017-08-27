using System;
using System.IO;
using Prometheus.Core.Tags;
using MDX = Microsoft.DirectX.Direct3D;

namespace Prometheus.Core.Tags.H2Bitm
{
  public class H2BITM_HEADER
  {
    public short type;
    public short format;
    public short usage;
    public short flags;
    public int colorPlateWidth;
    public int colorPlateHeight;
    public int colorPlateData;
    public H2REFLEXIVE Bitmaps = new H2REFLEXIVE();

    public void Load (ref BinaryReader br)
    {
      type = br.ReadInt16();
      format = br.ReadInt16();
      usage = br.ReadInt16();
      flags = br.ReadInt16();
      br.BaseStream.Seek(16, SeekOrigin.Current);
      colorPlateWidth = br.ReadInt16();
      colorPlateHeight = br.ReadInt16();
      colorPlateData = br.ReadInt32();
      br.BaseStream.Seek(36, SeekOrigin.Current);
      Bitmaps.Load(ref br);
      //br.BaseStream.Seek(6, SeekOrigin.Current); //new viper tags
    }    
  }
  public class H2BITM_IMAGE
  {
    public int signature;			// 'bitm'
    public short width;
    public short height;
    public short depth;
    public short type;
    public short format;
    public short flags;
    public short reg_point_x;
    public short reg_point_y;
    public short num_mipmaps;
    public short pixel_offset;
    public int[] offset = new int[6];
    public int[] size = new int[6];
    public byte[] DDSData;
    public MDX.Format DxTextureFormat;

    public void Load (ref BinaryReader br)
    {
      signature = br.ReadInt32();
      width = br.ReadInt16();
      height = br.ReadInt16();
      depth = br.ReadInt16();
      type = br.ReadInt16();
      format = br.ReadInt16();
      flags = br.ReadInt16();
      reg_point_x = br.ReadInt16();
      reg_point_y = br.ReadInt16();
      num_mipmaps = br.ReadInt16();
      pixel_offset = br.ReadInt16();
      br.BaseStream.Position += 4;

      for (int x=0; x<6; x++)
      {
        offset[x] = br.ReadInt32();
      }
      for (int x=0; x<6; x++)
      {
        size[x] = br.ReadInt32();
      }

      br.BaseStream.Seek(40, SeekOrigin.Current);

      switch(type)
      {
        case 0x00:
          DxTextureFormat = MDX.Format.A8;
          break;
        case 0x01:
          //#define BITM_FORMAT_Y8			
          //DxTextureFormat = MDX.Format.Y;
          break;
        case 0x02:
          //#define BITM_FORMAT_AY8			
          //DxTextureFormat = MDX.Format.AY;
          break;
        case 0x03:
          //#define BITM_FORMAT_A8Y8		
          //DxTextureFormat = MDX.Format;
          break;
        case 0x06:
          DxTextureFormat = MDX.Format.R5G6B5;
          break;
        case 0x08:
          DxTextureFormat = MDX.Format.A1R5G5B5;
          break;
        case 0x09:
          DxTextureFormat = MDX.Format.A4R4G4B4;
          break;
        case 0x0A:
          DxTextureFormat = MDX.Format.X8R8G8B8;
          break;
        case 0x0B:
          DxTextureFormat = MDX.Format.A8R8G8B8;
          break;
        case 0x0E:
          DxTextureFormat = MDX.Format.Dxt1;
          break;
        case 0x0F:
          //#define BITM_FORMAT_DXT2AND3	
          DxTextureFormat = MDX.Format.Dxt2;
          break;
        case 0x10:
          //#define BITM_FORMAT_DXT4AND5	
          DxTextureFormat = MDX.Format.Dxt4;
          break;
        case 0x11:
          //#define BITM_FORMAT_P8			
          DxTextureFormat = MDX.Format.P8;
          break;
				case 0x12:
					DxTextureFormat = MDX.Format.L8;
					break;
        default:
          //throw exception
          break;
      }

    }    
  }
  public class DDSWriter
  {
    public void WriteHeader(ref BinaryWriter bw, ref H2BITM_IMAGE b)
    {
      DDS_HEADER header = new DDS_HEADER();
      header.generate(ref b);
      header.WriteStruct(ref bw);
    }
  }

  public class H2Bitmap : TagBase
  {
    public H2BITM_HEADER m_header= new H2BITM_HEADER();
    public H2BITM_IMAGE[] m_images;
		DeSwizzle DeSwiz = new DeSwizzle();
    public void LoadTagData()
    {
      BinaryReader br = new BinaryReader(m_stream);

      m_header.Load(ref br);
      m_images = new H2BITM_IMAGE[m_header.Bitmaps.Count];
      for (int x=0; x<m_header.Bitmaps.Count; x++)
      {
        m_images[x] = new H2BITM_IMAGE();
        m_images[x].Load(ref br);
      }
      // Now load the raw data
      for (int x=0; x<m_header.Bitmaps.Count; x++)
      {
        m_images[x].DDSData = new byte[m_images[x].size[0]];
        m_images[x].DDSData = br.ReadBytes(m_images[x].size[0]);
				if ((m_images[x].flags & 0x8) == 0x8)
				{
					DeSwiz.depth = m_images[x].depth;
					DeSwiz.height = m_images[x].height;
					DeSwiz.width = m_images[x].width;
					DeSwiz.DeSwizzleData(ref m_images[x].DDSData,8);
			  } 
        for (int s=1; s<6; s++)
        {
          br.BaseStream.Position += m_images[x].size[s];
        }

      }
    }
  }

  public struct rgba_color_t
  {
    public uint r, g, b, a;
  }

	public class DeSwizzle
	{
		public short height;
		public short width;
		public short depth;
		public DeSwizzle()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public void DeSwizzleData(ref byte[] data,uint bitCount)
		{
			Swizzle mySwiz = new Swizzle((uint)width, (uint)height, (uint)depth);
			uint tmp1;
			uint tmp2;
			byte[] swizData = new byte[data.Length];
			for(ushort x1=0;x1<width;x1++) 
			{
				for(int y=0;y<height;y++) 
				{
					tmp1 = (uint)((y * width) + x1) * (bitCount / 8);
					tmp2 = (uint)(mySwiz.Swiz(x1, y, -1)) * (bitCount / 8);
					for(int i=0;i<(bitCount / 8);i++) 
					{
						swizData[tmp1 + i] = data[tmp2 + i];
					}
				}
			}
			for(int i=0;i<data.Length;i++) data[i] = swizData[i];
		}
	}
  /// <summary>
  /// Summary description for TagBitmap.
  /// </summary>
	public class Swizzle 
	{
		public uint m_MaskX;
		public uint m_MaskY;
		public uint m_MaskZ;
		public Swizzle(uint width, uint height, uint depth)
		{
			m_MaskX = 0;
			m_MaskY = 0;
			m_MaskZ = 0;
			uint bit = 1;
			uint idx = 1;
			while(bit < width || bit < height || bit < depth) 
			{
				if(bit < width) 
				{
					m_MaskX |= idx;
					idx <<= 1;
				}
				if(bit < height) 
				{
					m_MaskY |= idx;
					idx <<= 1;
				}
				if(bit < depth) 
				{
					m_MaskZ |= idx;
					idx <<= 1;
				}
				bit <<= 1;
			}
		}
		public uint Swiz(int x, int y, int z) 
		{
			return SwizzleAxis(x, m_MaskX) | SwizzleAxis(y, m_MaskY) | (z != -1 ? SwizzleAxis(z, m_MaskZ) : 0);
		}
		public uint SwizzleAxis(int val, uint mask) 
		{
			uint result = 0;
			for(uint bit = 1;bit <= mask;bit <<= 1)
			{
				if((mask & bit) != 0) 
					result |= (uint)(val & bit);
				else 
					val <<= 1;
			}
			return result;
		}
	}

  public class TagBitmap : TagBase
  {
    const int BITM_FLAG_LINEAR = (1 << 4);

    public enum BitmFormat
    {
      A8 = 0x00,
      Y8 = 0x01,
      AY8 = 0x02,
      A8Y8 = 0x03,
      R5G6B5 = 0x06,
      A1R5G5B5 = 0x08,
      A4R4G4B4 = 0x09,
      X8R8G8B8 = 0x0A,
      A8R8G8B8 = 0x0B,
      DXT1 = 0x0E,
      DXT2AND3 = 0x0F,
      DXT4AND5 = 0x10,
      P8 = 0x11,
			L8 = 0x12
    };
    public enum BitmType
    {
      Type2D = 0x00,
      Type3D = 0x01,
      TypeCubeMap = 0x02
    };
    public TagBitmap()
    {
    }
    static uint rgba_to_int(rgba_color_t color) 
    { 
      return (color.a << 24) | (color.r << 16) | (color.g << 8) | color.b;
    }
    static rgba_color_t short_to_rgba(ushort color)
    {
      rgba_color_t rc;
      rc.r = (uint)(((color >> 11) & 0x1F) * 0xFF) / 31;
      rc.g = (uint)(((color >>  5) & 0x3F) * 0xFF) / 63;
      rc.b = (uint)(((color >>  0) & 0x1F) * 0xFF) / 31;
      rc.a = 255;

      return rc;
    }
  }
  
  enum DDSEnum : int
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
  }
 
  public struct DDS_HEADER
  {
    public char[] ddsID; // 4
    public DDSURFACEDESC2 ddsd;
    public void WriteStruct(ref BinaryWriter bw)
    {
      bw.Write("DDS ".ToCharArray());
      ddsd.Write(ref bw);
    }
    public void generate(ref H2BITM_IMAGE image)
    {
      ddsd.Generate(ref image);
    }
  }
  public struct DDSURFACEDESC2
  {
    public int size_of_structure;
    public int flags;
    public int height;
    public int width;
    public int PitchOrLinearSize;
    public int depth;
    public int MipMapCount;
    public int[] Reserved1;
    public DDPIXELFORMAT ddfPixelFormat;
    public DDSCAPS2 ddsCaps;
    public int Reserved2;

    public void Write(ref BinaryWriter bw)
    {
      bw.Write(size_of_structure);
			bw.Write(flags);
      bw.Write(height);
      bw.Write(width);
      bw.Write(PitchOrLinearSize);
      bw.Write(depth);
      bw.Write(MipMapCount);
      for (int x = 0 ; x <= 10; x++) 
      {
        bw.Write(Reserved1[x]);
      }
      ddfPixelFormat.writeStruct(ref bw);
      ddsCaps.writeStruct(ref bw);
      bw.Write(Reserved2);
    }

    public void Generate(ref H2BITM_IMAGE image)
    {
      size_of_structure = 124;
      flags = flags + (int)DDSEnum.DDSD_CAPS;
      flags = flags + (int)DDSEnum.DDSD_PIXELFORMAT;
      flags = flags + (int)DDSEnum.DDSD_WIDTH;
      flags = flags + (int)DDSEnum.DDSD_HEIGHT;
      if (image.format == (short)TagBitmap.BitmFormat.DXT1 ||
        image.format == (short)TagBitmap.BitmFormat.DXT2AND3 ||
        image.format == (short)TagBitmap.BitmFormat.DXT4AND5) 
      {
        flags = flags + (int)DDSEnum.DDSD_LINEARSIZE;
      } 
      else 
      {
				if (image.format != (short)TagBitmap.BitmFormat.L8)
				{
					flags = flags + (short)DDSEnum.DDSD_PITCH;
				}
      }
      height = image.height;
      width = image.width;
      int RGBBitCount = 0;
			if (image.format == (short)TagBitmap.BitmFormat.L8)
			{
				RGBBitCount = 8;
			}
			else if (image.format == (short)TagBitmap.BitmFormat.R5G6B5) 
			{
				RGBBitCount = 16;
			} 
			else if (image.format == (short)TagBitmap.BitmFormat.A1R5G5B5) 
			{
				RGBBitCount = 16;
			} 
			else if (image.format == (short)TagBitmap.BitmFormat.A4R4G4B4) 
			{
				RGBBitCount = 16;
			} 
			else if (image.format == (short)TagBitmap.BitmFormat.X8R8G8B8) 
			{
				RGBBitCount = 32;
			} 
			else if (image.format == (short)TagBitmap.BitmFormat.A8R8G8B8) 
			{
				RGBBitCount = 32;
			}
      if ((image.flags & (short)DDSEnum.DDSD_PITCH) > 0) 
      {
        PitchOrLinearSize = image.width * (RGBBitCount / 8);
      }
      if ((image.flags & (int)DDSEnum.DDSD_LINEARSIZE) > 0) 
      {
        PitchOrLinearSize = image.size[0];
      }
      depth = 0;
      MipMapCount = image.num_mipmaps;
      if (MipMapCount > 0) 
      {
        flags = flags + (int)DDSEnum.DDSD_MIPMAPCOUNT;
      }
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
  }
  public struct DDPIXELFORMAT
  {
    public int size;
    public int Flags;
    public string FourCC;
    public int RGBBitCount;
    public int RBitMask;
    public int GBitMask;
    public int BBitMask;
    public int RGBAlphaBitMask;

    public void writeStruct(ref BinaryWriter bw)
    {
			
      bw.Write(size);
      bw.Write(Flags);
      bw.Write(FourCC.ToCharArray());
      bw.Write(RGBBitCount);
      bw.Write(RBitMask);
      bw.Write(GBitMask);
      bw.Write(BBitMask);
      bw.Write(RGBAlphaBitMask);
    }

    public void Generate(ref H2BITM_IMAGE image)
    {
      size = 32;
      Flags = 0;
			if (image.format == 0x12)
			{
				FourCC = "\0\0\0\0";
				Flags = Flags + 0x020000;
			}
			else if (image.format == 14) 
			{
				FourCC = "DXT1";
				Flags = Flags + (short)DDSEnum.DDPF_FOURCC;
			} 
			else if (image.format == 15) 
			{
				FourCC = "DXT3";
				Flags = Flags + (short)DDSEnum.DDPF_FOURCC;
			} 
			else if (image.format == 16) 
			{
				FourCC = "DXT4";
				Flags = Flags + (short)DDSEnum.DDPF_FOURCC;
			} 
			else 
			{
				FourCC = "\0\0\0\0";
				Flags = Flags + (short)DDSEnum.DDPF_RGB;
			}
			if (image.format == (short)TagBitmap.BitmFormat.L8)
			{
				RGBBitCount = 8;
			}
			else if (image.format == (short)TagBitmap.BitmFormat.R5G6B5) 
			{
				RGBBitCount = 16;
			} 
			else if (image.format == (short)TagBitmap.BitmFormat.A1R5G5B5) 
			{
				RGBBitCount = 16;
				Flags = Flags + (short)DDSEnum.DDPF_ALPHAPIXELS;
			} 
			else if (image.format == (short)TagBitmap.BitmFormat.A4R4G4B4) 
			{
				RGBBitCount = 16;
				Flags = Flags + (short)DDSEnum.DDPF_ALPHAPIXELS;
			} 
			else if (image.format == (short)TagBitmap.BitmFormat.X8R8G8B8) 
			{
				Flags = Flags + (short)DDSEnum.DDPF_ALPHAPIXELS;
				RGBBitCount = 32;
			} 
			else if (image.format == (short)TagBitmap.BitmFormat.A8R8G8B8) 
			{
				Flags = Flags + (short)DDSEnum.DDPF_ALPHAPIXELS;
				RGBBitCount = 32;
			} 
			else 
			{
				RGBBitCount = 0;
			}
			if (image.format == (short)TagBitmap.BitmFormat.L8)
			{
				RBitMask = 0xFF;
				GBitMask = 0;
				BBitMask = 0;
				RGBAlphaBitMask = 0;
			}
			else
				if (image.format == (short)TagBitmap.BitmFormat.R5G6B5) 
			{
				RBitMask = 63488;
				GBitMask = 2016;
				BBitMask = 31;
				RGBAlphaBitMask = 0;
			} 
			else if (image.format == (short)TagBitmap.BitmFormat.A1R5G5B5) 
			{
				RBitMask = 31744;
				GBitMask = 992;
				BBitMask = 31;
				RGBAlphaBitMask = 32768;
			} 
			else if (image.format == (short)TagBitmap.BitmFormat.A4R4G4B4) 
			{
				RBitMask = 61440;
				GBitMask = 240;
				BBitMask = 15;
				RGBAlphaBitMask = 15728640;
			} 
			else if (image.format == (short)TagBitmap.BitmFormat.X8R8G8B8) 
			{
				RBitMask = 1044480;
				GBitMask = 65280;
				BBitMask = 255;
				RGBAlphaBitMask = (int)278190080;
			} 
			else if (image.format == (short)TagBitmap.BitmFormat.A8R8G8B8) 
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
  }
  public struct DDSCAPS2
  {
    public int caps1;
    public int caps2;
    public int[] Reserved;

    public void writeStruct(ref BinaryWriter bw)
    {
      bw.Write(caps1);
      bw.Write(caps2);
      bw.Write(Reserved[0]);
      bw.Write(Reserved[1]);
    }

    public void Generate(ref H2BITM_IMAGE image)
    {
      caps1 = caps1 + (short)DDSEnum.DDSCAPS_TEXTURE;
      if (image.num_mipmaps > 0) 
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
}
