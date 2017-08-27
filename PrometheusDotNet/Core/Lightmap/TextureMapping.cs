using System;
using System.Drawing;
using System.Drawing.Imaging;

using Prometheus.Core;

namespace Prometheus.Core.Lightmap
{
	public enum Saturation{Maximum,StdDev3,StdDev2,StdDev1};
	public enum Filter{NoFilter,Gaussian3x3,Gaussian5x5};

	public class TextureMap
	{
		private static int fPRadius = 10;
		private static float fRate = 1.0f;

		private float[][][] fl_Bitmap;
		private Bitmap result;
		private int width, height;		//width = U, Height = V;

		/// <summary>
		/// A square texture map of size x size
		/// </summary>
		/// <param name="size">length of sides</param>
		public TextureMap(int width, int height)//, Color color, 
			//int fPRadius, float fRate)
		{
			this.height = height;
			this.width = width;
			this.result = new Bitmap(width, height);
			this.fl_Bitmap = new float[width][][];
			for(int i=0; i<width; i++)
			{
				this.fl_Bitmap[i] = new float[height][];
				for(int j=0; j<height; j++)
				{
					this.fl_Bitmap[i][j] = new float[3];
					this.fl_Bitmap[i][j][0] = 0.0f;
					this.fl_Bitmap[i][j][1] = 0.0f;
					this.fl_Bitmap[i][j][2] = 0.0f;
				}
			}
		}

		/// <summary>
		/// Adds the colour to the texel and updates
		/// surrounding texels. Needs some work.
		/// </summary>
		/// <param name="u">u parameter</param>
		/// <param name="v">v parameter</param>
		/// <param name="colour">Colour of texel</param>
		public void SetTexel(float u, float v, float[] rgb)
		{
			int U =(int)Math.Round(u*(this.width-1),0);
			int V =(int)Math.Round(v*(this.height-1),0);

			int t = (int)Math.Round((double)fPRadius/2);

			if(U>=fPRadius && U<(this.width-fPRadius) && V>fPRadius+1 
				&& V<(this.height-fPRadius-1))
			{
				for(int i=0; i<fPRadius; i++)
				{
					#region Void
					/*
					float tmp = (fRate/(i+1));
					//Cross
					this.fl_Bitmap[U][V-(i+1)][0] += (rgb[0]*tmp);
					this.fl_Bitmap[U][V-(i+1)][1] += (rgb[1]*tmp);
					this.fl_Bitmap[U][V-(i+1)][2] += (rgb[2]*tmp);

					this.fl_Bitmap[U][V+(i+1)][0] += (rgb[0]*tmp);
					this.fl_Bitmap[U][V+(i+1)][1] += (rgb[1]*tmp);
					this.fl_Bitmap[U][V+(i+1)][2] += (rgb[2]*tmp);

					this.fl_Bitmap[U-(i+1)][V][0] += (rgb[0]*tmp);
					this.fl_Bitmap[U-(i+1)][V][1] += (rgb[1]*tmp);
					this.fl_Bitmap[U-(i+1)][V][2] += (rgb[2]*tmp);

					this.fl_Bitmap[U+(i+1)][V][0] += (rgb[0]*tmp);
					this.fl_Bitmap[U+(i+1)][V][1] += (rgb[1]*tmp);
					this.fl_Bitmap[U+(i+1)][V][2] += (rgb[2]*tmp);

					//Corners
					this.fl_Bitmap[U-i][V-i][0] += (rgb[0]*tmp);
					this.fl_Bitmap[U-i][V-i][1] += (rgb[1]*tmp);
					this.fl_Bitmap[U-i][V-i][2] += (rgb[2]*tmp);

					this.fl_Bitmap[U+i][V+i][0] += (rgb[0]*tmp);
					this.fl_Bitmap[U+i][V+i][1] += (rgb[1]*tmp);
					this.fl_Bitmap[U+i][V+i][2] += (rgb[2]*tmp);

					this.fl_Bitmap[U-i][V+i][0] += (rgb[0]*tmp);
					this.fl_Bitmap[U-i][V+i][1] += (rgb[1]*tmp);
					this.fl_Bitmap[U-i][V+i][2] += (rgb[2]*tmp);

					this.fl_Bitmap[U+i][V-i][0] += (rgb[0]*tmp);
					this.fl_Bitmap[U+i][V-i][1] += (rgb[1]*tmp);
					this.fl_Bitmap[U+i][V-i][2] += (rgb[2]*tmp);

					//TODO: inbetween bits.*/
					#endregion
					for(int j=0; j<fPRadius; j++)
					{
						this.fl_Bitmap[U+i][V+j][0]=rgb[0];
						this.fl_Bitmap[U+i][V+j][1]=rgb[1];
						this.fl_Bitmap[U+i][V+j][2]=rgb[2];
					}
				}
			}
		}

		/// <summary>
		/// Saves the generated lightmaps to files.
		/// </summary>
		public static void Save()
		{
			System.IO.DirectoryInfo diFo = System.IO.Directory.CreateDirectory(OptionsManager.LightmapOutputPath);
			for(int i=0; i<Render.GetTextureMaps.Length; i++)
			{
				Render.GetTextureMaps[i].Save(OptionsManager.LightmapOutputPath + 
					string.Format("\\lightmap_{0}.bmp", i));
			}
		}

		public static void QuickSave()
		{
			System.IO.DirectoryInfo diFo = System.IO.Directory.CreateDirectory(OptionsManager.LightmapOutputPath);
			for(int i=0; i<Render.GetTextureMaps.Length; i++)
			{
				Bitmap bText = new Bitmap(256,256);
				bText.Save(OptionsManager.LightmapOutputPath + 
					string.Format("\\lightmap_{0}.bmp", i));
			}
		}

		public static void QuickSetPixel(short IX, float[] UV, Color colour)
		{

			Bitmap sText = new Bitmap(256,256,PixelFormat.Format24bppRgb);
			
			int U =(int)Math.Round(UV[0]*(256),0);
			int V =(int)Math.Round(UV[1]*(256),0);

			if(U>=fPRadius && U<(256-fPRadius) && V>fPRadius+1 
				&& V<(256-fPRadius-1))
			{
				for(int i=0; i<fPRadius; i++)
				{
					for(int j=0; j<fPRadius; j++)
					{
						sText.SetPixel(U+i,V+j, Color.White);
					}
				}
			}

			sText.Save(OptionsManager.LightmapOutputPath + 
				string.Format("\\lightmap_{0}.bmp", IX));
		}

		private void Save(string name)
		{
			float temp;
			float max_sqr_mag = 0;
			Color output;

			//Find the maximum pixel magnitude in the frame
			//todo:  might want to change this to max mag of each color component, not sure...maybe as an option?
			//or may want to use stddev and saturate some colors to get better balance
			for(int i=0; i<this.fl_Bitmap.Length; i++)
			{
				for(int j=0; j<this.fl_Bitmap[i].Length; j++)
				{

					temp = fl_Bitmap[i][j][0]*fl_Bitmap[i][j][0] + 
						fl_Bitmap[i][j][1]*fl_Bitmap[i][j][1] +
						fl_Bitmap[i][j][2]*fl_Bitmap[i][j][2];

					if(temp > max_sqr_mag)
						max_sqr_mag = temp;
				}
			}

			float mag=(float)Math.Sqrt(max_sqr_mag);

			for(int i=0; i<this.fl_Bitmap.Length; i++)
			{
				for(int j=0; j<this.fl_Bitmap[i].Length; j++)
				{
					int red,green,blue;
					if(mag>0)
					{
						red=(int)Math.Round((fl_Bitmap[i][j][0]/mag)*255);
						green=(int)Math.Round((fl_Bitmap[i][j][1]/mag)*255);
						blue=(int)Math.Round((fl_Bitmap[i][j][2]/mag)*255);
					}
					else //prevent 0 devision.
					{
						red=(int)Math.Round((fl_Bitmap[i][j][0])*255);
						green=(int)Math.Round((fl_Bitmap[i][j][1])*255);
						blue=(int)Math.Round((fl_Bitmap[i][j][2])*255);
					}

					//debug:
					red = (int)fl_Bitmap[i][j][0];
					green = (int)fl_Bitmap[i][j][1];
					blue = (int)fl_Bitmap[i][j][2];

					output = Color.FromArgb(red,green,blue);
					this.result.SetPixel(i,j, output);
				}
			}

			this.result.Save(name);
		}

		#region Properties
		/// <summary>
		/// Gets or sets the size of the bitmap.
		/// </summary>
		public int[] Size
		{
			get{ return new int[]{this.width, this.height}; }
			set
			{
				if(value[0]<=0 || value[1]<=0)
					throw new ApplicationException("Bad bitmap size");
				else
				{
					this.width=value[0];
					this.height=value[1];
				}
			}
		}

		/// <summary>
		/// Gets or sets the radius of the photon footprint.
		/// </summary>
		public static int FootPrintRadius
		{
			get{ return fPRadius; }
			set
			{
				if(value<=0)
					throw new ApplicationException("Bad Footprint radius");
				else
					fPRadius=value;
			}
		}

		/// <summary>
		/// Gets or sets the falloff percentage
		/// </summary>
		public static float FalloffRate
		{
			get{ return fRate*100; }
			set
			{
				if(value<0 || value >100)
					throw new ApplicationException("Falloff Rate out of bounds");
				else
					fRate=(value/100);
			}
		}

		/// <summary>
		/// Retrieves a texel RGB value.
		/// </summary>
		/// <param name="u">U dimension</param>
		/// <param name="v">V dimension</param>
		/// <returns></returns>
		public float[] GetTexel(int u, int v)
		{
			return this.fl_Bitmap[u][v];
		}
		#endregion
	}
}
