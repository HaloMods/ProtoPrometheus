/* ---------------------------------------------------------------
 * Prometheus
 * Bungie Map-based Multi-Game Editing Kit
 * 2004-2005, Halo-Dev
 * ---------------------------------------------------------------
 * Class       : Core.Render.TextureManager
 * Description : Central static collection for loading, storing,
 *             : and activating textures.
 * Author      : Grenadiac
 * Co-Authors  : MonoxideC
 * ---------------------------------------------------------------
 */

using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Prometheus.Core.Tags;
using H1BitmTag = Prometheus.Core.Tags.Bitm;
using H2BitmTag = Prometheus.Core.Tags.H2Bitm;


namespace Prometheus.Core.Render
{
  class TextureWithFormat
  {
    public Texture texture;
    public int Width;
    public int Height;
    public int Format;
    public int TextureDataLength;
  }

  class TEXTURE_LOOKUP_ELEMENT
  {
    //public short index = -1;
    public string name = "";
    public TextureWithFormat [] TextureList = null;
    public bool bCubemap = false;
    public TEXTURE_LOOKUP_ELEMENT()
    {
    }
  }
  class CUBE_TEXTURE_LOOKUP_ELEMENT : TEXTURE_LOOKUP_ELEMENT
  {
    public CubeTexture CubeTexture = null;
  }
  /// <summary>
  /// Central static collection for loading, storing,
  /// and activating textures.
  /// </summary>
  public class TextureManager
  {
    private const int DEFAULT_TEXTURE = 0;
    private const int MaxElements = 500;
    TEXTURE_LOOKUP_ELEMENT[] m_LookupTable = new TEXTURE_LOOKUP_ELEMENT[MaxElements];
    private int m_LookupCount = 0;

    public int NavArrowIndex;
    private int BillboardBaseIndex;

    public TextureManager()
    {
      //load default pink texture
      for(int i=0; i<18; i++)
      {
        m_LookupTable[i] = new TEXTURE_LOOKUP_ELEMENT();
        m_LookupTable[i].TextureList = new TextureWithFormat[1];
        m_LookupTable[i].TextureList[0] = new TextureWithFormat();
      }
      
      // Load the default texture from the embedded resource
      m_LookupTable[m_LookupCount].TextureList[0].texture = TextureLoader.FromStream(
        MdxRender.Dev, Assembly.GetExecutingAssembly().GetManifestResourceStream("Core.Render.Resources.default.dds"));  
      m_LookupCount++;

      //load the "direction textures"
      m_LookupTable[m_LookupCount].TextureList[0].texture = TextureLoader.FromStream(
        MdxRender.Dev, Assembly.GetExecutingAssembly().GetManifestResourceStream("Core.Render.Resources.nx.dds"));  
      m_LookupCount++;

      m_LookupTable[m_LookupCount].TextureList[0].texture = TextureLoader.FromStream(
        MdxRender.Dev, Assembly.GetExecutingAssembly().GetManifestResourceStream("Core.Render.Resources.ny.dds"));  
      m_LookupCount++;

      m_LookupTable[m_LookupCount].TextureList[0].texture = TextureLoader.FromStream(
        MdxRender.Dev, Assembly.GetExecutingAssembly().GetManifestResourceStream("Core.Render.Resources.nz.dds"));  
      m_LookupCount++;

      m_LookupTable[m_LookupCount].TextureList[0].texture = TextureLoader.FromStream(
        MdxRender.Dev, Assembly.GetExecutingAssembly().GetManifestResourceStream("Core.Render.Resources.px.dds"));  
      m_LookupCount++;

      m_LookupTable[m_LookupCount].TextureList[0].texture = TextureLoader.FromStream(
        MdxRender.Dev, Assembly.GetExecutingAssembly().GetManifestResourceStream("Core.Render.Resources.py.dds"));  
      m_LookupCount++;

      m_LookupTable[m_LookupCount].TextureList[0].texture = TextureLoader.FromStream(
        MdxRender.Dev, Assembly.GetExecutingAssembly().GetManifestResourceStream("Core.Render.Resources.pz.dds"));  
      m_LookupCount++;

      m_LookupTable[m_LookupCount].TextureList[0].texture = TextureLoader.FromStream(
        MdxRender.Dev, Assembly.GetExecutingAssembly().GetManifestResourceStream("Core.Render.Resources.nav_arrow.dds"));  
      NavArrowIndex = m_LookupCount;
      m_LookupCount++;

      #region Billboard Textures
      //It is important to load these textures in the same order as the BillboardModel 
      //enumeration so they match up properly
      BillboardBaseIndex = m_LookupCount;
      //public enum BillboardModel{CTF_Flag,CTF_Vehicle,Oddball,Race_Track, Race_Vehicle, Vegas_Bank, Teleport_From, Teleport_To, KOH};

      m_LookupTable[m_LookupCount].TextureList[0].texture = TextureLoader.FromStream(
        MdxRender.Dev, Assembly.GetExecutingAssembly().GetManifestResourceStream("Core.Render.Resources.ctf.dds"));  
      m_LookupCount++;

      m_LookupTable[m_LookupCount].TextureList[0].texture = TextureLoader.FromStream(
        MdxRender.Dev, Assembly.GetExecutingAssembly().GetManifestResourceStream("Core.Render.Resources.ctf_vehicle.dds"));  
      m_LookupCount++;

      m_LookupTable[m_LookupCount].TextureList[0].texture = TextureLoader.FromStream(
        MdxRender.Dev, Assembly.GetExecutingAssembly().GetManifestResourceStream("Core.Render.Resources.oddball.dds"));  
      m_LookupCount++;

      m_LookupTable[m_LookupCount].TextureList[0].texture = TextureLoader.FromStream(
        MdxRender.Dev, Assembly.GetExecutingAssembly().GetManifestResourceStream("Core.Render.Resources.race.dds"));  
      m_LookupCount++;

      m_LookupTable[m_LookupCount].TextureList[0].texture = TextureLoader.FromStream(
        MdxRender.Dev, Assembly.GetExecutingAssembly().GetManifestResourceStream("Core.Render.Resources.race_vehicle.dds"));  
      m_LookupCount++;

      m_LookupTable[m_LookupCount].TextureList[0].texture = TextureLoader.FromStream(
        MdxRender.Dev, Assembly.GetExecutingAssembly().GetManifestResourceStream("Core.Render.Resources.vegas_bank.dds"));  
      m_LookupCount++;

      m_LookupTable[m_LookupCount].TextureList[0].texture = TextureLoader.FromStream(
        MdxRender.Dev, Assembly.GetExecutingAssembly().GetManifestResourceStream("Core.Render.Resources.teleporter_enter.dds"));  
      m_LookupCount++;

      m_LookupTable[m_LookupCount].TextureList[0].texture = TextureLoader.FromStream(
        MdxRender.Dev, Assembly.GetExecutingAssembly().GetManifestResourceStream("Core.Render.Resources.teleporter_exit.dds"));  
      m_LookupCount++;

      m_LookupTable[m_LookupCount].TextureList[0].texture = TextureLoader.FromStream(
        MdxRender.Dev, Assembly.GetExecutingAssembly().GetManifestResourceStream("Core.Render.Resources.koh.dds"));  
      m_LookupCount++;

      m_LookupTable[m_LookupCount].TextureList[0].texture = TextureLoader.FromStream(
        MdxRender.Dev, Assembly.GetExecutingAssembly().GetManifestResourceStream("Core.Render.Resources.sound_scenery.dds"));  
      m_LookupCount++;
      #endregion
    }
    public int GetBillboardTextureIndex(BillboardModel bm)
    {
      int texture_index = BillboardBaseIndex + (int)bm;
      return(texture_index);
    }
    public int RegisterTexture(TagFileName tfn)
    {
      int tex_index = DEFAULT_TEXTURE;
      bool bTextureFound = false;

      //TODO:  use hash table?
      for(tex_index=0; ((tex_index<MaxElements)&&(tex_index<m_LookupCount)); tex_index++)
      {
        if(m_LookupTable[tex_index].name == tfn.RelativePath)
        {
          bTextureFound = true;
          break;
        }
      }

      if(bTextureFound == false)
      {
        try
        {

          if(tfn.Exists == false)
          {
            Trace.WriteLine("Could not locate texture: " + tfn.RelativePath);
          }
          else
          {
            H1BitmTag.Bitmap bmp = new H1BitmTag.Bitmap();
            //H2BitmTag.H2Bitmap bmp = new H2BitmTag.H2Bitmap();

            if(!bmp.LoadTagBuffer(tfn))
            {
              throw new Exception("Could not load bitmap: " + tfn.RelativePath);
            }
            bmp.LoadTagData();
          
            m_LookupTable[m_LookupCount] = new TEXTURE_LOOKUP_ELEMENT();
            m_LookupTable[m_LookupCount].name = tfn.RelativePath;

            //TODO: create array of textures
            m_LookupTable[m_LookupCount].TextureList = new TextureWithFormat[bmp.m_header.Bitmaps.Count];
            Trace.WriteLine("Loading texture: " + tfn.RelativePath);
            for(int t=0; t<bmp.m_header.Bitmaps.Count; t++)
            {
              m_LookupTable[m_LookupCount].TextureList[t] = new TextureWithFormat();

              MemoryStream stream = new MemoryStream();
              BinaryWriter bw = new BinaryWriter(stream);
              // Write the DDS header
              H1BitmTag.DDSWriter d = new H1BitmTag.DDSWriter();
              d.GenerateAndWriteHeader(ref bw, ref bmp.m_images[t], false);
              // Now copy the binary data to the stream
              bw.Write(bmp.m_images[t].DDSData);

//              if(tfn.RelativePath == @"levels\test\bloodgulch\bitmaps\mp lights small strips red.bitmap")
//              {
//                //write data out to dds file
//                FileStream fs = new FileStream(@"c:\temp\test.dds", FileMode.Create);
//                byte[] tmp = new byte[stream.Length];
//                stream.Seek(0, SeekOrigin.Begin);
//                stream.Read(tmp, 0, (int)stream.Length);
//                fs.Write(tmp, 0, (int)stream.Length);
//                fs.Close();
//              }

              stream.Seek(0, SeekOrigin.Begin);
              m_LookupTable[m_LookupCount].TextureList[t].texture = TextureLoader.FromStream(MdxRender.Dev, stream);
              m_LookupTable[m_LookupCount].TextureList[t].Width = bmp.m_images[t].width;
              m_LookupTable[m_LookupCount].TextureList[t].Height = bmp.m_images[t].height;
              m_LookupTable[m_LookupCount].TextureList[t].Format = bmp.m_images[t].format;
              m_LookupTable[m_LookupCount].TextureList[t].TextureDataLength = bmp.m_images[t].size;
            }

            tex_index = m_LookupCount;
            m_LookupCount++;
          }
        }
        catch(Exception e)
        {
          Trace.WriteLine("TextureManager Exception: " + e.Message);
        }
      }

      return(tex_index);
    }

    public int RegisterTexture2(TagFileName tfn)
    {
      int tex_index = DEFAULT_TEXTURE;
      bool bTextureFound = false;

      //TODO:  use hash table?
      for(tex_index=0; ((tex_index<MaxElements)&&(tex_index<m_LookupCount)); tex_index++)
      {
        if(m_LookupTable[tex_index].name == tfn.RelativePath)
        {
          bTextureFound = true;
          break;
        }
      }

      if(bTextureFound == false)
      {
        try
        {
          H2BitmTag.H2Bitmap bmp = new H2BitmTag.H2Bitmap();
          if(!bmp.LoadTagBuffer(tfn))
          {
            throw new Exception("Could not load bitmap: " + tfn.RelativePath);
          }
          bmp.LoadTagData();
          
          m_LookupTable[m_LookupCount] = new TEXTURE_LOOKUP_ELEMENT();

          //TODO: create array of textures
          m_LookupTable[m_LookupCount].TextureList = new TextureWithFormat[bmp.m_header.Bitmaps.Count];
          Trace.WriteLine("Loading texture: " + tfn.RelativePath);
          for(int t=0; t<bmp.m_header.Bitmaps.Count; t++)
          {
            m_LookupTable[m_LookupCount].TextureList[t] = new TextureWithFormat();

            MemoryStream stream = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(stream);
            // Write the DDS header
            H2BitmTag.DDSWriter d = new H2BitmTag.DDSWriter();
            d.WriteHeader(ref bw, ref bmp.m_images[t]);
            // Now copy the binary data to the stream
            bw.Write(bmp.m_images[t].DDSData);
            stream.Seek(0, SeekOrigin.Begin);
        
            m_LookupTable[m_LookupCount].TextureList[t].texture = TextureLoader.FromStream(MdxRender.Dev, stream);
          }

          tex_index = m_LookupCount;
          m_LookupCount++;
        }
        catch(Exception e)
        {
          Trace.WriteLine("TextureManager Exception: " + e.Message);
        }
      }

      return(tex_index);
    }
    public int RegisterTexture3(string texture_name,ref byte[] ColorMaps,ref ushort[] LightMapIndex,ref ushort[] ColorMapIndex,ref H2BitmTag.H2Bitmap bmp)
    {
      int tex_index = DEFAULT_TEXTURE;
      bool bTextureFound = false;

      //TODO:  use hash table?
      for(tex_index=0; ((tex_index<MaxElements)&&(tex_index<m_LookupCount)); tex_index++)
      {
        if(m_LookupTable[tex_index].name == texture_name)
        {
          bTextureFound = true;
          break;
        }
      }

      if(bTextureFound == false)
      {
        //try
        //{

        m_LookupTable[m_LookupCount] = new TEXTURE_LOOKUP_ELEMENT();

        //TODO: create array of textures
        m_LookupTable[m_LookupCount].TextureList = new TextureWithFormat[LightMapIndex.Length];
        byte[] header = new byte[0x80]
            {
              0x44,0x44,0x53,0x20,0x7C,0x00,0x00,0x00,0x07,0x10,0x00,0x00,0x10,0x00,0x00,0x00,
              0x10,0x00,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
              0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
              0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
              0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x20,0x00,0x00,0x00,
              0x41,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x20,0x00,0x00,0x00,0x00,0x00,0xff,0x00,
              0x00,0xff,0x00,0x00,0xff,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0x00,0x10,0x00,0x00,
              0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00};

        for(int t=0; t < LightMapIndex.Length; t++)
        {
					if (LightMapIndex[t] !=0xffff)
					{
						int ColorsOffset = ColorMapIndex[t] * 0x400;
						MemoryStream stream = new MemoryStream();
						BinaryWriter bw = new BinaryWriter(stream);
						//if (LightMapIndex[t] != 0xffff)
						//{
						byte[] tmpBuf = new byte[bmp.m_images[LightMapIndex[t]].DDSData.Length * 4];
						// Write the DDS header
						PutUInt(ref header,12, (uint)bmp.m_images[LightMapIndex[t]].height);
						PutUInt(ref header,16, (uint)bmp.m_images[LightMapIndex[t]].width);
										
						bw.Seek(0,SeekOrigin.Begin);
						bw.Write(header);
					
						for (int cpo = 0;cpo < bmp.m_images[LightMapIndex[t]].DDSData.Length -4 ;cpo++) 
						{ 
							//SwapInt(ref ColorMaps,(uint)cpo); 
							byte index = bmp.m_images[LightMapIndex[t]].DDSData[cpo]; 
 
							tmpBuf[4*cpo] = ColorMaps[ColorsOffset + index *4]; 
							tmpBuf[4*cpo+1] = ColorMaps[ColorsOffset + index *4+1]; 
							tmpBuf[4*cpo+2] = ColorMaps[ColorsOffset + index *4+2]; 
							tmpBuf[4*cpo+3] = ColorMaps[ColorsOffset + index *4+3]; 
						}          //ColorMaps.CopyTo(tmpBuf,);
						bw.Write(tmpBuf,0,tmpBuf.Length);
						stream.Seek(0, SeekOrigin.Begin);
        
            m_LookupTable[m_LookupCount].TextureList[t] = new TextureWithFormat();
            m_LookupTable[m_LookupCount].TextureList[t].texture = TextureLoader.FromStream(MdxRender.Dev, stream);
					}
        }

        tex_index = m_LookupCount;
        m_LookupCount++;
      }
      //catch(Exception e)
      //{
      //	Trace.WriteLine("TextureManager Exception: " + e.Message);
      //}
      //}
      return(tex_index);
    }
    public int RegisterCubemap(TagFileName tfn)
    {
      int tex_index = DEFAULT_TEXTURE;
      bool bTextureFound = false;

      //TODO:  use hash table?
      for(tex_index=0; ((tex_index<MaxElements)&&(tex_index<m_LookupCount)); tex_index++)
      {
        if(m_LookupTable[tex_index].name == tfn.RelativePath)
        {
          bTextureFound = true;
          break;
        }
      }

      if(bTextureFound == false)
      {
        try
        {

          if(tfn.Exists == false)
          {
            Trace.WriteLine("Could not locate texture: " + tfn.RelativePath);
          }
          else
          {
            H1BitmTag.Bitmap bmp = new H1BitmTag.Bitmap();

            if(!bmp.LoadTagBuffer(tfn))
            {
              throw new Exception("Could not load bitmap: " + tfn.RelativePath);
            }
            bmp.LoadTagData();
          
            m_LookupTable[m_LookupCount] = new CUBE_TEXTURE_LOOKUP_ELEMENT();
            m_LookupTable[m_LookupCount].name = tfn.RelativePath;

            //TODO: create array of textures
            Trace.WriteLine("Loading texture: " + tfn.RelativePath);

            //assume there is only 1 cube map in the .bitmap file?
            if(bmp.m_header.Bitmaps.Count > 1)
              throw new Exception("Cannot load multiple cubemaps from one tag.");

            MemoryStream stream = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(stream);
            // Write the DDS header
            H1BitmTag.DDSWriter d = new H1BitmTag.DDSWriter();
            d.GenerateAndWriteHeader(ref bw, ref bmp.m_images[0], true);
            
            // Now copy the raw pixel data (6 sides), but skip past the mipmaps
            MemoryStream tmpStream = new MemoryStream(bmp.m_images[0].DDSData);
            int rSize = (bmp.m_images[0].width * bmp.m_images[0].height * d.header.ddsd.ddfPixelFormat.RGBBitCount)/8;  
            int pSize = bmp.m_images[0].DDSData.Length / 6 - rSize; 
 
            byte[] rawbytes = new byte[rSize];
            //figure out sorting
            tmpStream.Position = (rSize * 0);
            tmpStream.Read(rawbytes, 0, rawbytes.Length);
            bw.Write(rawbytes); 

            tmpStream.Position = (rSize * 2);
            tmpStream.Read(rawbytes, 0, rawbytes.Length);
            bw.Write(rawbytes); 

            tmpStream.Position = (rSize * 1);
            tmpStream.Read(rawbytes, 0, rawbytes.Length);
            bw.Write(rawbytes); 

            tmpStream.Position = (rSize * 3);
            tmpStream.Read(rawbytes, 0, rawbytes.Length);
            bw.Write(rawbytes); 

            tmpStream.Position = (rSize * 4);//g
            tmpStream.Read(rawbytes, 0, rawbytes.Length);
            bw.Write(rawbytes); 

            tmpStream.Position = (rSize * 5);//g
            tmpStream.Read(rawbytes, 0, rawbytes.Length);
            bw.Write(rawbytes); 


            rawbytes = null;
            
            //write data out to dds file
//            FileStream fs = new FileStream(@"c:\temp\cubemap.dds", FileMode.Create);
//            byte[] tmp = new byte[stream.Length];
//            stream.Seek(0, SeekOrigin.Begin);
//            stream.Read(tmp, 0, (int)stream.Length);
//            fs.Write(tmp, 0, (int)stream.Length);
//            fs.Close();

            stream.Seek(0, SeekOrigin.Begin);

            ((CUBE_TEXTURE_LOOKUP_ELEMENT)m_LookupTable[m_LookupCount]).CubeTexture = TextureLoader.FromCubeStream(MdxRender.Dev, stream);
            m_LookupTable[m_LookupCount].bCubemap = true;

            tex_index = m_LookupCount;
            m_LookupCount++;
          }
        }
        catch(Exception e)
        {
          Trace.WriteLine("TextureManager Exception: " + e.Message);
        }
      }

      return(tex_index);
    }

    
    
    public static void PutUInt(ref byte[] data,uint offset,uint Value)
		{
			byte[] tmp = BitConverter.GetBytes(Value);
			tmp.CopyTo(data,(long)offset);
		}
		private void SwapInt(ref byte[] Array,uint Offset)
		{
			if (Array.Length - 4 >= Offset)
			{
				byte st;
				st = Array[Offset + 3];
				Array[Offset + 3] = Array[Offset + 0];
				Array[Offset + 0] = st;
				st = Array[Offset + 2];
				Array[Offset + 2] = Array[Offset + 1];
				Array[Offset + 1] = st;
			}
		}
    private int activeTextureIndex = 0;
    private int activeTextureSubIndex = 0;
    private int activeStage = 0;
    public void ActivateTexture(int Stage, int TextureIndex, int TextureSubIndex)
    {
      if((TextureIndex >= 0)&&(TextureIndex < m_LookupCount))
      {
        if(m_LookupTable[TextureIndex].bCubemap == true)
        {
          MdxRender.Dev.SetTexture(Stage, ((CUBE_TEXTURE_LOOKUP_ELEMENT)m_LookupTable[TextureIndex]).CubeTexture);
          activeTextureIndex = TextureIndex;
          activeTextureSubIndex = 0;
        }
        else
        {
          if(TextureSubIndex < m_LookupTable[TextureIndex].TextureList.Length)
          {
            if ((TextureIndex == activeTextureIndex) && (TextureSubIndex == activeTextureSubIndex) && (Stage == activeStage))
              return;

            MdxRender.Dev.SetTexture(Stage, m_LookupTable[TextureIndex].TextureList[TextureSubIndex].texture);
            activeTextureIndex = TextureIndex;
            activeTextureSubIndex = TextureSubIndex;
          }
          else
          {
            ActivateDefaultTexture();
          }
        }
      }
      activeStage = Stage;
    }
    public void ActivateDefaultTexture()
    {
      activeTextureIndex = 0;
      activeTextureSubIndex = 0;
      MdxRender.Dev.SetTexture(0, m_LookupTable[0].TextureList[0].texture);
    }
    public void PreviewRadiosity(int LightmapTextureIndex, string LightmapPath)
    {
      //get lightmap counts, verify that bitmap file count matches
      string[] bitmap_list = Directory.GetFiles(LightmapPath, "*.bmp");
      TextureWithFormat[] lightmap_list = m_LookupTable[LightmapTextureIndex].TextureList;

      if(lightmap_list.Length != bitmap_list.Length)
        Trace.WriteLine("Warning:  Lightmap count in directory did not match BSP lightmap count.");

      for(int l=0; l<lightmap_list.Length; l++)
      {
        //reload texture based on lightmap name
        string bmp_path = string.Format("{0}\\lightmap_{1}.bmp", LightmapPath, l);
        if(File.Exists(bmp_path) == false)
        {
          Trace.WriteLine("Skipping lightmap update, could not find bitmap: " +  bmp_path);
        }
        else
        {
          Bitmap bmp = new Bitmap(bmp_path);

          //do i need to dispose of the old bitmap?
          lightmap_list[l].texture = Texture.FromBitmap(MdxRender.Dev, bmp, Usage.None, Pool.Managed);
        }
      }
    }
    public void UpdateLightmap(int LightmapTextureIndex, int LightmapSubIndex, string LightmapPath)
    {
      //get lightmap counts, verify that bitmap file count matches
      TextureWithFormat[] lightmap_list = m_LookupTable[LightmapTextureIndex].TextureList;

      //reload texture based on lightmap name
      //string bmp_path = string.Format("{0}\\lightmap_{1}.bmp", LightmapPath, l);
      if(File.Exists(LightmapPath) == false)
      {
        Trace.WriteLine("Skipping lightmap update, could not find bitmap: " +  LightmapPath);
      }
      else
      {
        Bitmap bmp = new Bitmap(LightmapPath);
        lightmap_list[LightmapSubIndex].texture = Texture.FromBitmap(MdxRender.Dev, bmp, Usage.None, Pool.Managed);
      }
    }
    public void PaintLightmapTexel(int LightmapTextureIndex, int LightmapSubIndex, float[] uv)
    {
      Color clr = Color.Tomato;
      //validate texture indices
      if((LightmapTextureIndex >= 0)&&(LightmapTextureIndex < m_LookupCount))
      {
        if((LightmapSubIndex >= 0)&&(LightmapSubIndex < m_LookupTable[LightmapTextureIndex].TextureList.Length))
        {
          TextureWithFormat t_data = m_LookupTable[LightmapTextureIndex].TextureList[LightmapSubIndex];

          int u_index = (int)Math.Round(uv[0]*(t_data.Width - 1));
          int v_index = (int)Math.Round(uv[1]*(t_data.Height - 1));
          //int seekpos = (v_index*t_data.Height + u_index)*2;
          //int seekpos = (u_index*t_data.Height + v_index)*2;
          //int seekpos = (u_index*t_data.Width + v_index)*2;
          int seekpos = (v_index*t_data.Width + u_index)*2;

          if((seekpos > (t_data.TextureDataLength - 2))||(seekpos < 0))
          {
            Trace.WriteLine("tried to find a lightmap pixel outside bounds of texture");
          }
          else
          {
            if(t_data.Format == 6) //lightmap format is R5G6B5
            {
              ushort pixel;
              GraphicsStream gs = t_data.texture.LockRectangle(0, LockFlags.None);
              BinaryReader br = new BinaryReader(gs);
              BinaryWriter bw = new BinaryWriter(gs);
              br.BaseStream.Seek(seekpos, SeekOrigin.Begin);
              pixel = br.ReadUInt16();
              br.BaseStream.Seek(seekpos, SeekOrigin.Begin);
              ushort taco = 0xF000;
              bw.Write(taco);
              t_data.texture.UnlockRectangle(0);
              //MdxRender.Dev.UpdateTexture(t_data.texture, t_data.texture);

              Trace.WriteLine(string.Format("u_index = {0}  v_index = {1}  pixel = {2:X}", u_index, v_index, pixel)); 

              //convert pixel into Color
              int red =   (int)((((pixel & 0xF800) >> 11)/32f)*255);
              int green = (int)((((pixel & 0x07E0) >> 5)/64f)*255);
              int blue =  (int)(((pixel & 0x001F)/32f)*255);
              clr = Color.FromArgb(red, green, blue);
            }
          }
        }
      }
    }
    public Color GetLightmapTexelColor(int LightmapTextureIndex, int LightmapSubIndex, float u, float v)
    {
      Color clr = Color.HotPink;
      //validate texture indices
      if((LightmapTextureIndex >= 0)&&(LightmapTextureIndex < m_LookupCount))
      {
        if((LightmapSubIndex >= 0)&&(LightmapSubIndex < m_LookupTable[LightmapTextureIndex].TextureList.Length))
        {
          TextureWithFormat t_data = m_LookupTable[LightmapTextureIndex].TextureList[LightmapSubIndex];

          int u_index = (int)Math.Round(u*(t_data.Width - 1));
          int v_index = (int)Math.Round(v*(t_data.Height - 1));
          //int seekpos = (v_index*t_data.Height + u_index)*2;
          //int seekpos = (u_index*t_data.Height + v_index)*2;
          //int seekpos = (u_index*t_data.Width + v_index)*2;
          int seekpos = (v_index*t_data.Width + u_index)*2;

          if((seekpos > (t_data.TextureDataLength - 2))||(seekpos < 0))
          {
            Trace.WriteLine("tried to find a lightmap pixel outside bounds of texture");
          }
          else
          {
            if(t_data.Format == 6) //lightmap format is R5G6B5
            {
              ushort pixel;
              GraphicsStream gs = t_data.texture.LockRectangle(0, LockFlags.None);
              BinaryReader br = new BinaryReader(gs);
              BinaryWriter bw = new BinaryWriter(gs);
              br.BaseStream.Seek(seekpos, SeekOrigin.Begin);
              pixel = br.ReadUInt16();
              //br.BaseStream.Seek(seekpos, SeekOrigin.Begin);
              //ushort taco = 0;
              //bw.Write(taco);
              t_data.texture.UnlockRectangle(0);
              //MdxRender.Dev.UpdateTexture(t_data.texture, t_data.texture);

              //Trace.WriteLine(string.Format("u_index = {0}  v_index = {1}  pixel = {2:X}", u_index, v_index, pixel)); 

              //convert pixel into Color
              int red =   (int)((((pixel & 0xF800) >> 11)/32f)*255);
              int green = (int)((((pixel & 0x07E0) >> 5)/64f)*255);
              int blue =  (int)(((pixel & 0x001F)/32f)*255);
              clr = Color.FromArgb(red, green, blue);
            }
          }
        }
      }

      return(clr);
    }
  }
}
