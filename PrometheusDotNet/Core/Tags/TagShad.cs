using System;
using System.IO;
using Prometheus.Core.Render;
using Microsoft.DirectX.Direct3D;

namespace Prometheus.Core.Tags
{
	/// <summary>
	/// Halo 2 Shader Tag Loader
	/// </summary>
	public class SHAD_HEADER
	{
		public H2TAG_REFERENCE tag_Template = new H2TAG_REFERENCE();
		public H2REFLEXIVE ref_Bitmaps = new H2REFLEXIVE();
		public H2REFLEXIVE ref_Struct2 = new H2REFLEXIVE();
		public H2REFLEXIVE ref_Struct3 = new H2REFLEXIVE();

		public void Load(ref BinaryReader br)
		{
			tag_Template.Load(ref br);
			br.BaseStream.Position += 4;
			ref_Bitmaps.Load(ref br);
			br.BaseStream.Position += 12;
			ref_Struct2.Load(ref br);
			br.BaseStream.Position += 4;
			ref_Struct3.Load(ref br);
			br.BaseStream.Position += 32;
			tag_Template.ReadString(ref br);
		}		
	}
	public class SHAD_BITMAPS
	{
		public H2TAG_REFERENCE Basemap = new H2TAG_REFERENCE();
		public H2TAG_REFERENCE Bitmap2 = new H2TAG_REFERENCE();
		public H2TAG_REFERENCE Bitmap3 = new H2TAG_REFERENCE();
		public H2TAG_REFERENCE Bitmap4 = new H2TAG_REFERENCE();

		public void Load(ref BinaryReader br)
		{
			Basemap.Load(ref br);
			Bitmap2.Load(ref br);
			br.BaseStream.Position += 28;
			Bitmap3.Load(ref br);
			Bitmap4.Load(ref br);
			br.BaseStream.Position += 20;
			Basemap.ReadString(ref br);
			Bitmap2.ReadString(ref br);
			Bitmap3.ReadString(ref br);
			Bitmap4.ReadString(ref br);
			
		}	
	}
	public class TagShad : ShaderBase
	{
		SHAD_HEADER m_Header = new SHAD_HEADER();
		SHAD_BITMAPS[] m_Bitmaps;

		// Enviorment Shader
		public ushort Environment_flags;

		// Enviorment Shader Type
		public ushort Environment_type;

		public void LoadTagData()
		{
			BinaryReader br = new BinaryReader(m_stream);

			m_Header.Load(ref br);
			m_Bitmaps = new SHAD_BITMAPS[m_Header.ref_Bitmaps.Count];
			br.BaseStream.Position = m_Header.ref_Bitmaps.Offset;
			for(int b=0; b<m_Header.ref_Bitmaps.Count; b++)
			{
				m_Bitmaps[b] = new SHAD_BITMAPS();
				m_Bitmaps[b].Load(ref br);
			}
		}																																											   
		public void ActivateShaderWithLightmap(ref TextureManager TexMgr)
		{
			if(false)//NumTextureStages >= 4)
			{
				//setup LM, base, 2 details

				//MdxRender.Dev.TextureState[0].ColorOperation = TextureOperation.SelectArg1;
				//m_Lightmaps[l].Materials[m].DrawMaterial(ref shdr_mgr);

				MdxRender.Dev.TextureState[0].ColorArgument0 = TextureArgument.Current;
				MdxRender.Dev.TextureState[1].ColorArgument0 = TextureArgument.Current;

				//MdxRender.Dev.TextureState[0].ColorArgument1 = TextureArgument.TextureColor;
				//MdxRender.Dev.TextureState[0].ColorOperation = TextureOperation.Modulate;
			}
			else if(NumTextureStages >= 1)
			{
				//setup base+lightmap blend
				MdxRender.Dev.TextureState[0].ColorArgument0 = TextureArgument.TextureColor;
				MdxRender.Dev.TextureState[0].ColorOperation = TextureOperation.Modulate2X;
				MdxRender.Dev.TextureState[0].TextureCoordinateIndex = 1;

				TexMgr.ActivateTexture(1, Stage1TextureIndex, 0);
				//MdxRender.Dev.TextureState[1].AlphaArgument1 = TextureArgument.TextureColor;
				//MdxRender.Dev.TextureState[1].AlphaArgument2 = TextureArgument.TextureColor;
				MdxRender.Dev.TextureState[1].ColorArgument1 = TextureArgument.TextureColor;
				MdxRender.Dev.TextureState[1].ColorArgument2 = TextureArgument.Current;
				MdxRender.Dev.TextureState[1].AlphaOperation = TextureOperation.SelectArg1;
				MdxRender.Dev.TextureState[1].ColorOperation = TextureOperation.Modulate;
				MdxRender.Dev.TextureState[1].TextureCoordinateIndex = 0;



				if(Environment_type == 1)
				{
					//detail blend based on alpha map
					TexMgr.ActivateTexture(3, Stage2TextureIndex, 0);
					MdxRender.Dev.TextureState[3].ColorArgument1 = TextureArgument.Current;
					MdxRender.Dev.TextureState[3].ColorArgument2 = TextureArgument.TextureColor;
					MdxRender.Dev.TextureState[3].ColorOperation = TextureOperation.BlendTextureAlpha;
					MdxRender.Dev.TextureState[3].TextureCoordinateIndex = 0;
				}
				else
				{
					//MdxRender.Dev.TextureState[3].ColorOperation = TextureOperation.Disable;
				}
			}
		}
    override public void LoadTextures(ref TextureManager TexMgr, int NumTextureStages)
    {
      this.NumTextureStages = NumTextureStages;

      if(NumTextureStages >= 1)
        this.Stage1TextureIndex = TexMgr.RegisterTexture2(new TagFileName(this.m_Bitmaps[0].Basemap.data, "bitm", this.m_PromHeader.GameVersion));

      //if(NumTextureStages >= 2)
      //  this.Stage2TextureIndex = TexMgr.RegisterTexture(Detail_map.data);
    }
	}
}
