using System;
using System.IO;
using Prometheus.Core.Tags;
using MDX = Microsoft.DirectX.Direct3D;
using Prometheus.Core.Compiler;

namespace Prometheus.Core.Tags.Bitm
{
  public class BITM_HEADER
  {
    public short type;
    public short format;
    public short usage;
    public short flags;
    public int[] skip1 = new int[4];
    public int colorPlateWidth;
    public int colorPlateHeight;
    public int colorPlateData;
    public int[] skip2 = new int[4];
    public int processedPixelData;
    public int skip2b;
    public int TIFFSize;
    public int[] skip3 = new int[6];
    public REFLEXIVE Sequences = new REFLEXIVE();
    public REFLEXIVE Bitmaps = new REFLEXIVE();

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
      br.BaseStream.Seek(16, SeekOrigin.Current);
      processedPixelData = br.ReadInt32() ;
      br.BaseStream.Seek(4, SeekOrigin.Current);
      TIFFSize = br.ReadInt32();
      br.BaseStream.Seek(24, SeekOrigin.Current);
      Sequences.Load(ref br);
      Bitmaps.Load(ref br);
     }    
  }

  public class BITM_IMAGE
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
    public int offset;
    public int size;
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
      offset = br.ReadInt32();
      size = br.ReadInt32();
      br.BaseStream.Seek(16, SeekOrigin.Current);

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
        default:
          //throw exception
          break;
      }

    }    
  }

  public class DDSWriter
  {
    public DDS_HEADER header = new DDS_HEADER();
    public void GenerateAndWriteHeader(ref BinaryWriter bw, ref BITM_IMAGE b, bool bCubemap)
    {
      header.generate(ref b, bCubemap);
      header.WriteStruct(ref bw);
    }
  }

  public class Bitmap : TagBase
  {
    public BITM_HEADER m_header;
    public BITM_IMAGE[] m_images;
    public Bitmap()
    {
      m_header = new BITM_HEADER();
    }
    public void LoadTagData()
    {
      BinaryReader br = new BinaryReader(m_stream);

      m_header.Load(ref br);
      br.BaseStream.Seek(m_header.Bitmaps.Offset, SeekOrigin.Begin);
      //br.BaseStream.Seek(m_header.Bitmaps.Unknown, SeekOrigin.Begin);
      m_images = new BITM_IMAGE[m_header.Bitmaps.Count];
      for (int x=0; x<m_header.Bitmaps.Count; x++)
      {
        m_images[x] = new BITM_IMAGE();
        m_images[x].Load(ref br);
      }
      // Now load the raw data
      br.BaseStream.Seek(m_header.TIFFSize, SeekOrigin.Begin);
      for (int x=0; x<m_header.Bitmaps.Count; x++)
      {
        m_images[x].DDSData = new byte[m_images[x].size];
        m_images[x].DDSData = br.ReadBytes(m_images[x].size);
				if ((m_images[x].flags & 0x08) == 0x08)
				{
					DeSwizzleBitmap(ref m_images[x]);
				}
      }
    }
		public void DeSwizzleBitmap(ref BITM_IMAGE bi_Obj)
		{
			DeSwizzle DeSwizz = new DeSwizzle();
			DeSwizz.depth = (ushort)bi_Obj.depth;
			DeSwizz.height = (ushort)bi_Obj.height;
			DeSwizz.width = (ushort)bi_Obj.width;
			switch (bi_Obj.format)
			{
				case 0x06:
					DeSwizz.DeSwizzleData(ref bi_Obj.DDSData,16);
					DeSwizz = null;
					break;
				case 0x0B:
					DeSwizz.DeSwizzleData(ref bi_Obj.DDSData,32);
					DeSwizz = null;
					break;
				case 0x02:
					DeSwizz.DeSwizzleData(ref bi_Obj.DDSData,8);
					DeSwizz = null;
					break;
				case 0x03:
					DeSwizz.DeSwizzleData(ref bi_Obj.DDSData,16);
					DeSwizz = null;
					break;
				case 0x11:
					DeSwizz.DeSwizzleData(ref bi_Obj.DDSData,8);
					DeSwizz = null;
					break;
				case 0x01:
					DeSwizz.DeSwizzleData(ref bi_Obj.DDSData,8);
					DeSwizz = null;
					break;
				case 0x0A:
					DeSwizz.DeSwizzleData(ref bi_Obj.DDSData,32);
					DeSwizz = null;
					break;
				case 0x0F:
					DeSwizz.DeSwizzleData(ref bi_Obj.DDSData,16);
					DeSwizz = null;
					break;
				case 0x09:
					DeSwizz.DeSwizzleData(ref bi_Obj.DDSData,16);
					DeSwizz = null;
					break;
			}
		}
  }
  
  public struct rgba_color_t
  {
    public uint r, g, b, a;
  }

  public class Swizzler
  {
    private int m_MaskX, m_MaskY, m_MaskZ;

    public Swizzler(int Width, int Height, int Depth)
    {
      //default arg for Depth = 0
      m_MaskX = 0;
      m_MaskY = 0;
      m_MaskZ = 0;

      for(int Bit=1, Idx=1; (Bit < Width) || (Bit < Height) || (Bit < Depth); Bit <<= 1)
      {
        if (Bit < Width)
        {
          m_MaskX |= Idx;
          Idx <<= 1;
        }

        if (Bit < Height)
        {
          m_MaskY |= Idx;
          Idx <<= 1;
        }

        if (Bit < Depth)
        {
          m_MaskZ |= Idx;
          Idx <<= 1;
        }
      }
    }
    public uint Swizzle(int Sx, int Sy)
    {
      return(Swizzle(Sx, Sy, -1));
    }
    public uint Swizzle(int Sx, int Sy, int Sz)
    {
      return SwizzleAxis(Sx, m_MaskX) | SwizzleAxis(Sy, m_MaskY) | ((Sz != -1) ? SwizzleAxis(Sz, m_MaskZ) : 0);
    }
    private uint SwizzleAxis(int Value, int Mask)
    {
      uint Result=0;

      for(int Bit = 1; Bit <= Mask; Bit <<= 1)
      {
        if((Mask & Bit) != 0)
          Result |= (uint)(Value & Bit);
        else
          Value <<= 1;
      }
      return Result;
    }
  }

	/// <summary>
	/// Summary description for TagBitmap.
	/// </summary>
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
      P8 = 0x11
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
    DDPF_ALPHAPIXELS = 0x0001,
    DDPF_ALPHAPREMULT = 0x8000,
    DDPF_FOURCC = 0x0004,
    DDPF_RGB = 0x0040,
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
    public void generate(ref BITM_IMAGE image, bool bCubemap)
    {
      ddsd.Generate(ref image, bCubemap);
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

    public void Generate(ref BITM_IMAGE image, bool bCubemap)
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
        flags = flags + (short)DDSEnum.DDSD_PITCH;
      }
      height = image.height;
      width = image.width;

      /*
		switch((TagBitmap.BitmFormat)image.format)
		{
      case TagBitmap.BitmFormat.DXT1:
				RGBBitCount = 4;   
				break;
      case TagBitmap.BitmFormat.DXT2AND3:
      case TagBitmap.BitmFormat.DXT4AND5:
				RGBBitCount = 8;   
				break;
      case TagBitmap.BitmFormat.R5G6B5:
				RGBBitCount = 16;   
				break;
			case TagBitmap.BitmFormat.A8:
				RGBBitCount = 8;   
				break;
			case  TagBitmap.BitmFormat.Y8:
				RGBBitCount = 8;   
				break;
			case TagBitmap.BitmFormat.P8:
				RGBBitCount = 8;   
				break;
			case TagBitmap.BitmFormat.AY8:
				RGBBitCount = 8;   
				break;
			case TagBitmap.BitmFormat.A8Y8:
				RGBBitCount = 16;   
				break;
			case TagBitmap.BitmFormat.A1R5G5B5:
				RGBBitCount = 16;   
				break;
			case TagBitmap.BitmFormat.A4R4G4B4:
				RGBBitCount = 16;   
				break;
			case TagBitmap.BitmFormat.X8R8G8B8:
				RGBBitCount = 32;   
				break;
			case TagBitmap.BitmFormat.A8R8G8B8:
				RGBBitCount = 32;   
				break;
			default:
				RGBBitCount = 0;   
				break;
		}
    */

      depth = 0;
      //disable mips for cubemaps, for now anyway
      if(bCubemap)
        MipMapCount = 0;
      else
        MipMapCount = image.num_mipmaps;

      if((MipMapCount > 0)&&(!bCubemap))
      {
        flags = flags + (int)DDSEnum.DDSD_MIPMAPCOUNT;
      }

      Reserved1 = new int[11];
      for(int i=0; i<Reserved1.Length; i++)
        Reserved1[i] = 0;

      ddfPixelFormat.Generate(ref image);
      ddsCaps.Generate(ref image, bCubemap);

      if((image.flags & (short)DDSEnum.DDSD_PITCH) != 0) 
        PitchOrLinearSize = (image.width * ddfPixelFormat.RGBBitCount)/8;

      if((image.flags & (int)DDSEnum.DDSD_LINEARSIZE) != 0) 
        PitchOrLinearSize = image.size;
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

    public void Generate(ref BITM_IMAGE image)
    {
      size = 32;
      Flags = 0;
      if (image.format == 14) 
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

      switch((TagBitmap.BitmFormat)image.format)
      {
        case TagBitmap.BitmFormat.DXT1:
          Flags |= (int)DDSEnum.DDPF_ALPHAPIXELS;
          Flags |= (int)DDSEnum.DDPF_ALPHAPREMULT;
          RGBBitCount = 4;   
          break;
        case TagBitmap.BitmFormat.DXT2AND3:
        case TagBitmap.BitmFormat.DXT4AND5:
          RGBBitCount = 8;   
          break;
        case TagBitmap.BitmFormat.R5G6B5:
          RGBBitCount = 16;   
          RBitMask = 0xF800;
          GBitMask = 0x7E0;
          BBitMask = 0x1F;
          RGBAlphaBitMask = 0x0;
          break;
        case TagBitmap.BitmFormat.A8:
          RGBBitCount = 8;   
          Flags = Flags + (short)DDSEnum.DDPF_ALPHAPIXELS;
          RBitMask = 0x0;
          GBitMask = 0x0;
          BBitMask = 0x0;
          RGBAlphaBitMask = 0xFF;
          break;
        case  TagBitmap.BitmFormat.Y8:
          RGBBitCount = 8;   
          Flags = Flags + (short)DDSEnum.DDPF_ALPHAPIXELS;
          RBitMask = 0x0;
          GBitMask = 0x0;
          BBitMask = 0x0;
          RGBAlphaBitMask = 0xFF;
          break;
        case TagBitmap.BitmFormat.P8:
          RGBBitCount = 8;   
          Flags = Flags + (short)DDSEnum.DDPF_ALPHAPIXELS;
          RBitMask = 0x0;
          GBitMask = 0x0;
          BBitMask = 0x0;
          RGBAlphaBitMask = 0xFF;
          break;
        case TagBitmap.BitmFormat.AY8:
          RGBBitCount = 8;   
          Flags = Flags + (short)DDSEnum.DDPF_ALPHAPIXELS;
          RBitMask = 0xF;
          GBitMask = 0x0;
          BBitMask = 0x0;
          RGBAlphaBitMask = 0xF0;
          break;
        case TagBitmap.BitmFormat.A8Y8:
          RGBBitCount = 16;   
          Flags = Flags + (short)DDSEnum.DDPF_ALPHAPIXELS;
          RBitMask = 0xFF;
          GBitMask = 0x0;
          BBitMask = 0x0;
          RGBAlphaBitMask = 0xFF00;
          break;
        case TagBitmap.BitmFormat.A1R5G5B5:
          RGBBitCount = 16;   
          Flags = Flags + (short)DDSEnum.DDPF_ALPHAPIXELS;
          RBitMask = 0x7C00;
          GBitMask = 0x3E0;
          BBitMask = 0x1F;
          RGBAlphaBitMask = 0x8000;
          break;
        case TagBitmap.BitmFormat.A4R4G4B4:
          RGBBitCount = 16;   
          Flags = Flags + (short)DDSEnum.DDPF_ALPHAPIXELS;
          RBitMask = 0xF00;
          GBitMask = 0xF0;
          BBitMask = 0xF;
          RGBAlphaBitMask = 0xF000;
          break;
        case TagBitmap.BitmFormat.X8R8G8B8:
          RGBBitCount = 32;
          RBitMask = 0xFF0000;
          GBitMask = 0xFF00;
          BBitMask = 0xFF;
          RGBAlphaBitMask = 0x00000000;
          break;
        case TagBitmap.BitmFormat.A8R8G8B8:
          RGBBitCount = 32;   
          Flags = Flags + (short)DDSEnum.DDPF_ALPHAPIXELS;
          RBitMask = 0xFF0000;
          GBitMask = 0xFF00;
          BBitMask = 0xFF;
          unchecked
          {
            RGBAlphaBitMask = (int)0xFF000000;
          }
          break;
        default:
          RGBBitCount = 0;   
          break;
      }
    }
  }
  public struct DDSCAPS2
  {
    public int caps1;
    public int caps2;
    public int caps3;
    public int caps4;

    public void writeStruct(ref BinaryWriter bw)
    {
      bw.Write(caps1);
      bw.Write(caps2);
      bw.Write(caps3);
      bw.Write(caps4);
    }

    public void Generate(ref BITM_IMAGE image, bool bCubemap)
    {
      caps1 = 0;
      caps1 |= (int)DDSEnum.DDSCAPS_TEXTURE;
      if(image.num_mipmaps > 0)
      {
        caps1 |= (int)DDSEnum.DDSCAPS_MIPMAP;
        caps1 |= (int)DDSEnum.DDSCAPS_COMPLEX;
      }

      caps2 = 0;
      if(bCubemap)
      {
        caps2 |= (int)DDSEnum.DDSCAPS2_CUBEMAP;
        caps2 |= (int)DDSEnum.DDSCAPS2_CUBEMAP_NEGATIVEX;
        caps2 |= (int)DDSEnum.DDSCAPS2_CUBEMAP_POSITIVEZ;
        caps2 |= (int)DDSEnum.DDSCAPS2_CUBEMAP_POSITIVEY;
        caps2 |= (int)DDSEnum.DDSCAPS2_CUBEMAP_POSITIVEX;
        caps2 |= (int)DDSEnum.DDSCAPS2_CUBEMAP_NEGATIVEZ;
        caps2 |= (int)DDSEnum.DDSCAPS2_CUBEMAP_NEGATIVEY;
        caps1 |= (int)DDSEnum.DDSCAPS_COMPLEX;
      }

      caps3 = 0;
      caps4 = 0;
    }
  }
}
