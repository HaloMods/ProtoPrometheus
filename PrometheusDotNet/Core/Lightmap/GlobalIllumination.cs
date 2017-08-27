using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Xml;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

using Prometheus.Core.Render;
using Prometheus.Core.Tags;
using Prometheus.Core.Tags.Sbsp;

namespace Prometheus.Core.Lightmap
{
	/// <summary>
	/// Describes a photon particle.
	/// 
	/// TODO: Process collisions on objects.
	///		  Determine out vector on shader properties.
	///		  Scale light more accurately.
	/// </summary>
	public sealed class Photon
	{
		private float u,v;
		private Vector3 pos;
		private Vector3 dir;
		private float[] color = new float[3];  

		#region Constructor
		/// <summary>
		/// Creates a photon, usually created by a lightsource. 
		/// <see cref="Prometheus.Core.Lightmap.LightSource"/>
		/// <seealso cref="LightSource.Uniform_Scatter"/>
		/// </summary>
		/// <param name="position">Position of photon.</param>
		/// <param name="direction">Initial direction of photon.</param>
		/// <param name="intensity">Power of the photon</param>
		/// <param name="rgb">Colour of photon</param>
		public Photon(Vector3 position, Vector3 direction, float[] color)
		{
			this.pos = position;
			this.dir = direction;
			this.color = color;
		}
		#endregion

		#region Process Collisions
		/*
		public void ProcessCollision()
		{
			if(MdxRender.m_Model.TestMeshIntersect(this.Position,this.Direction))
			{
				return;
			}
			
			for(int i=0; i<MdxRender.GetPlane.Length; i+=3)
			{
				Vector3[] tri = new Vector3[3];
				tri[0] = new Vector3(MdxRender.GetPlane[i].X, MdxRender.GetPlane[i].Y, MdxRender.GetPlane[i].Z);
				tri[1] = new Vector3(MdxRender.GetPlane[i+1].X, MdxRender.GetPlane[i+1].Y, MdxRender.GetPlane[i+1].Z);
				tri[2] = new Vector3(MdxRender.GetPlane[i+2].X, MdxRender.GetPlane[i+2].Y, MdxRender.GetPlane[i+2].Z);

				Vector3 temp;
				float u,v;
				if(Utility.IntersectTriangle(this.pos, this.dir, tri, true, out temp, out u, out v))
				{
					this.u =MdxRender.GetPlane[i+2].Tu*v + MdxRender.GetPlane[i+1].Tu*u + MdxRender.GetPlane[i].Tu*(1-u-v);
					this.v =MdxRender.GetPlane[i+2].Tv*v + MdxRender.GetPlane[i+1].Tv*u + MdxRender.GetPlane[i].Tv*(1-u-v);

					this.pos = temp;
					this.dir = this.GetNewDirection();
					Render.SetTexel(this.u, this.v, this.color);
					this.color = this.ScalePower();
					break;
				}
			}
		}*/
		#endregion

		public void ProcessCollision(Model3D obj)
		{
			float x,y,z;
			obj.m_BoundingBox.GetCentroid(out x, out y, out z);

			EnhancedMesh[][] testList = obj.GetMeshListLOD();

			if(obj.RayAABBInterset(this.pos, this.Direction))
			{
				for(int i=0; i<testList.Length; i++)
				{
					for(int j=0; j<testList[i].Length; j++)
					{
						if(testList[i][j].RayAABBIntersect(this.pos, this.dir))
						{
							float[] UV;
							Vector3 position;

							/*if(testList[i][j].RayModelIntersect(this.pos, this.dir, true, 
								out position, out UV))
							{
								this.pos = position;
								this.dir = this.GetNewDirection();
								break;
							}*/
						}
					}
				}
			}
		}

		public void ProcessCollision(Tags.Sbsp.TagBsp bsp)
		{
			float c = 0xfffffffffffff;
			Vector3 point;
			float[] UV;
			short LIx;
			float t;

			for(int i=0; i<bsp.GetLightMapCount; i++)
			{
				Prometheus.Core.Tags.Sbsp.BSP_LIGHTMAP tmp = bsp.GetLightMap(i);
				if(tmp.RayAABBTest(this.pos,this.dir, out point, out UV, out LIx, out t))
				{
					if(LIx != -1)
					{
						if(t<c)
						{
							MdxRender.lightmapDebugger.AddIntersectPoint(point);
							MdxRender.lightmapDebugger.AddRay(pos, point);
							Render.SetTexel(UV[0], UV[1], this.color, LIx);
							this.pos = point;
							this.dir = this.GetNewDirection();
						}
					}
				}
			}
		}

		#region Calculate out vector
		/// <summary>
		/// TODO:
		/// </summary>
		/// <returns></returns>
		private Vector3 GetNewDirection()
		{
			Vector3 result = new Vector3(0,0,0);
			result.X = -1*this.dir.X;
			result.Y = -1*this.dir.Y;
			result.Z = -1*this.dir.Z;
			return result;
		}
		#endregion

		#region Scale Power
		/// <summary>
		/// TODO:
		/// </summary>
		public float[] ScalePower()
		{
			float[] result = new float[3];
			result[0] = this.color[0]*0.9f;
			result[1] = this.Color[1]*0.9f;
			result[2] = this.Color[2]*0.9f;
			return result;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the position vector of the photon.
		/// </summary>
		public Vector3 Position
		{
			get{ return this.pos; }
			set{ this.pos = value; }
		}

		/// <summary>
		/// Gets or sets the direction vector of the photon.
		/// </summary>
		public Vector3 Direction
		{
			get{ return this.dir; }
			set{ this.dir = value; }
		}

		/// <summary>
		/// Gets or sets the colour of the photon.
		/// </summary>
		public float[] Color
		{
			get{ return this.color; }
			set{ this.color = value; }
		}

		/// <summary>
		/// Gets or sets the U component.
		/// </summary>
		public float U
		{
			get{ return this.u; }
			set{ this.u = value; }
		}

		/// <summary>
		/// Gets or sets the V component.
		/// </summary>
		public float V
		{
			get{ return this.v; }
			set{ this.v = value; }
		}
		#endregion
	}

	/// <summary>
	/// Describes a light source in a scene.
	/// </summary>
	public class LightSource
	{		
		public enum Type{ Diffuse, Directional };
		private LightSource.Type lType = LightSource.Type.Diffuse;
		private Vector3 pos;
		private Random RandomNumberGen;
		private float[] color = new float[3];

		#region Constructor
		/// <summary>
		/// Creates a colour light source at x,y,z.
		/// </summary>
		/// <param name="pos">position</param>
		/// <param name="power">Intensity of light (dim)0-1(bright)</param>
		/// <param name="color">Colour of the light (black)0-255(white)</param>
		public LightSource(Vector3 pos, double power, float[] color)
		{
			this.RandomNumberGen = new Random();
			this.pos = pos;
			this.color[0] = color[0]*(float)power;
			this.color[1] = color[1]*(float)power;
			this.color[2] = color[2]*(float)power;
		}
		#endregion

		#region Light scatter methods
		/// <summary>
		/// Creates a diffuse light scattering.
		/// </summary>
		/// <returns>Generates a random direction vector.</returns>
		public Photon Uniform_Scatter()
		{
			float x,y,z,r;
			do
			{
				x=-1+2*(float)this.RandomNumberGen.Next(0,0x7fff)/0x7fff;
				y=-1+2*(float)this.RandomNumberGen.Next(0,0x7fff)/0x7fff;
				z=-1+2*(float)this.RandomNumberGen.Next(0,0x7fff)/0x7fff;
				r=x*x+y*y+z*z;
			}while(r>1 || r==0);
			r=(float)Math.Sqrt(-2*Math.Log(r, Math.E)/r);
			x*=r; y*=r; z*=r;

			Vector3 tmp = new Vector3();
			tmp = new Vector3(x,y,z);
			tmp.Normalize();

			//Debug:
			//tmp = new Vector3(0,0,-1);

			return new Photon(this.pos, tmp, this.color);
		}

		/// <summary>
		/// TODO:
		/// </summary>
		/// <param name="direction">Direction of light</param>
		/// <param name="radius">Radius of spot on ground</param>
		/// <param name="fade_Angle">Angle to fade light at</param>
		/// <param name="Cut_off">Angle to not draw light</param>
		/// <returns>A photon within the spot light specification</returns>
		public Photon Spot_Light(Vector3 direction, float radius, float fade_Angle, 
			float Cut_off)
		{
			return new Photon(this.pos, new Vector3(0,0,-1), this.color);
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the position of this light.
		/// </summary>
		public Vector3 Position
		{
			get{ return this.pos; }
			set{ this.pos = value; }
		}

		/// <summary>
		/// Gets or sets the colour of the light.
		/// </summary>
		public float[] Color
		{
			get{ return this.color; }
			set{ this.color = value; }
		}

		/// <summary>
		/// Gets or sets the light type
		/// </summary>
		public LightSource.Type LType
		{
			get{ return this.lType; }
			set{ this.lType = value; }
		}
		#endregion
	}

	/// <summary>
	/// Renders a lightmap.
	/// </summary>
	public class Render: System.ComponentModel.Component
	{
		#region Events\Delegates
		public delegate void TimeChangeHandler(object sender, string result);
		public delegate void ProgressChangeHandler(object sender, int val);
		/// <summary>
		/// Called when time progress is updated.
		/// </summary>
		public event TimeChangeHandler TimeUpdate;
		/// <summary>
		/// Called when progress is updated.
		/// </summary>
		public event ProgressChangeHandler ProgressUpdate;
		#endregion


		//Debug:
		static private CustomVertex.PositionOnly[] lines = new CustomVertex.PositionOnly[0];
		static public int numhits =0;

		private int maxp = 1000;
		private int maxb = 1;
		private int maxl = 0;

		private bool saveProg = false;
		private bool stop = false;
		
		private Prometheus.Core.Render.Model3D[] meshes;
		private Prometheus.Core.Tags.Sbsp.TagBsp bsp;
		private Prometheus.Core.Lightmap.LightSource[] lights;
		private static TextureMap[] lightMaps;

		private System.Threading.Thread thread;
		private System.ComponentModel.Container components = null;

		#region Constructor\Deconstructor
		public Render()
		{
			this.InitializeComponent();
		}

		public Render(System.ComponentModel.IContainer container)
		{
			container.Add(this);
			InitializeComponent();
		}

		~Render()
		{
			this.Dispose(false);
		}
		#endregion

		//#region Photon Collisions
		private void Photon_Collision()
		{
			if(maxl ==0) this.Default();

			TimeSpan procT = new TimeSpan(100);
			for(int g=0; g<maxl; g++)
			{
				if(this.stop)
					break;
					
				for(int h=0; h<maxp; h++)
				{
					if(this.saveProg)
					{
						this.SaveProgress(g);
						this.stop =true;
						break;
					}

					int nHits=0;
					Photon photon = this.lights[g].Uniform_Scatter();

					//Render.Addline(photon.Position, photon.Direction);

					//if(this.lights[g].LType == LightSource.Type.Directional)
					//photon = this.lights[g].Spot_Light();

					DateTime store = DateTime.Now;

					while(nHits<maxb)
					{
						if(this.stop)
							break;

						for(int l=0; l<this.meshes.Length; l++)
						{
							photon.ProcessCollision(meshes[l]);
						}
						photon.ProcessCollision(this.bsp);
						nHits++;
					}

					Thread.Sleep(0);

					procT = (DateTime.Now - store);
					this.Progress(procT, g, h);
				}
			}
			this.Progress(procT, maxl, maxp);	
			TextureMap.Save();

			MdxRender.GetCurrentBsp.UpdateRadiosityPreview();
		}
		//#endregion

		private void Default()
		{
		}

		#region Save Progress
		private void SaveProgress(int h)
		{
			System.IO.Directory.SetCurrentDirectory(System.Windows.Forms.Application.StartupPath);
			XmlTextWriter textWriter = new XmlTextWriter("progress.xml", null);
			textWriter.WriteStartDocument(); 

			textWriter.WriteStartElement("Progress");
 
			for(int i=h; i<this.maxl; i++)
			{
				if(this.lights[i] != null)
				{
					textWriter.WriteStartElement("LS", "LightSource", "urs:"+i);  

					// Write X element
					textWriter.WriteStartElement("X", "");
					textWriter.WriteString(this.lights[i].Position.X.ToString());
					textWriter.WriteEndElement(); 
 
					// Write Y element
					textWriter.WriteStartElement("Y", "");
					textWriter.WriteString(this.lights[i].Position.Y.ToString());
					textWriter.WriteEndElement();

					// Write Z element
					textWriter.WriteStartElement("Z", "");
					textWriter.WriteString(this.lights[i].Position.Z.ToString());
					textWriter.WriteEndElement();

					// Write R element
					textWriter.WriteStartElement("R", "");
					textWriter.WriteString(this.lights[i].Color[0].ToString());
					textWriter.WriteEndElement();

					// Write G element
					textWriter.WriteStartElement("G", "");
					textWriter.WriteString(this.lights[i].Color[1].ToString());
					textWriter.WriteEndElement();

					// Write B element
					textWriter.WriteStartElement("B", "");
					textWriter.WriteString(this.lights[i].Color[2].ToString());
					textWriter.WriteEndElement();

					textWriter.WriteEndElement();
				}
			}

			textWriter.WriteEndDocument();
			textWriter.Close();

			//Save the lightmap to a data file.
			/*
			using(System.IO.StreamWriter sw = new System.IO.StreamWriter("data.dat"))
			{
				for(int k=0; k<lightMap.Size; k++)
				{
					for(int i=0; i<lightMap.Size; i++)
					{
						for(int j=0; j<3; j++)
						{
							sw.Write(lightMap.GetTexel(h,i)[j].ToString());
						}
					}
				}
				sw.Close();
				System.Windows.Forms.MessageBox.Show("Saved Progress", "Done!");
			}*/
		}
		#endregion

		#region Load Progress
		private void LoadProgress()
		{
			/*
			 * Load settings.
			 * Populate variables.
			 * Begin from resume point.
			 */
		}
		#endregion

		#region Timer
		private void Progress(TimeSpan interval, int lcount, int pcount)
		{
			TimeSpan result = TimeSpan.FromTicks((interval.Ticks*
				((this.maxl-lcount)*(this.maxp-pcount)))/2);

			if(result.Ticks!=0)
			{
				int p = (int)Math.Round(((interval.TotalSeconds*((lcount+1)*(pcount+1)))/
					(interval.TotalSeconds*(maxl*maxp)))*100);

				if(TimeUpdate != null)
					TimeUpdate(this, result.ToString());
				if(ProgressUpdate != null)
					ProgressUpdate(this, p);
			}
			else if(result.Ticks==0 && lcount==maxl)
			{
				if(TimeUpdate != null)
					TimeUpdate(this, result.ToString());
			}
		}
		#endregion

		#region Begin methods
		public void BeginThreaded()
		{
			thread = new Thread(new ThreadStart(this.Begin));
			thread.Start();
			Thread.Sleep(0);
		}

		public void Begin()
		{
			MdxRender.lightmapDebugger.Reset();
			if(this.bsp == null) throw new Exception("No bsp to test");
			this.Photon_Collision();
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the current lightmap image.
		/// </summary>
		public static TextureMap[] GetTextureMaps
		{
			get{ return Render.lightMaps; }
		}

		/// <summary>
		/// Sets a texel to the spcified value.
		/// </summary>
		/// <param name="tu">Texture U paramater</param>
		/// <param name="tv">Texture V paramater</param>
		/// <param name="color">Colour of texel</param>
		public static void SetTexel(float tu, float tv, float[] color, int ix)
		{
			lightMaps[ix].SetTexel(tu, tv, color);
		}

		/// <summary>
		/// Gets or sets the maximum number of photons per light.
		/// </summary>
		public int MaxPhotons
		{
			get{ return this.maxp; }
			set
			{
				if(value<1)
					throw new Exception("maxp <= 0");
				else
					this.maxp=value;
			}
		}

		/// <summary>
		/// Gets or sets the maximum number of bounces per light.
		/// </summary>
		public int MaxBounces
		{
			get{ return this.maxb; }
			set
			{
				if(value<1)
					throw new Exception("maxb <= 0");
				else
					this.maxb=value;
			}
		}

		/// <summary>
		/// Sets the stop value.
		/// </summary>
		public bool Stop
		{
			set{ this.stop=value; }
		}

		/// <summary>
		/// Save progress
		/// </summary>
		public bool SaveProg
		{
			set{ this.saveProg=value; }
		}

		/// <summary>
		/// Adds a light for processing. DON'T USE
		/// </summary>
		public LightSource AddLight
		{
			set
			{
				LightSource[] combArray = new LightSource[this.lights.Length+1];
				this.lights.CopyTo(combArray, 0);
				combArray[combArray.Length-1] = value;
				this.lights = new LightSource[combArray.Length];
				combArray.CopyTo(this.lights,0);
				this.maxl = this.lights.Length;
			}
		}
		#endregion

		#region Dispose
		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					if(this.thread != null)
					{
						this.thread.Abort();
						components.Dispose();
					}
					else
						components.Dispose();
				}
				else if(this.thread != null)
				{
					this.thread.Abort();
				}
			}
			base.Dispose( disposing );
		}
		#endregion
		
		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion 

		//#region Set scene
		public void SetScene(Scene scene)
		{
			lightMaps = new TextureMap[scene.GetBSP.GetLightMapCount];
			for(int i=0; i<lightMaps.Length; i++)
				lightMaps[i] = new TextureMap(256,256);
			this.bsp = scene.GetBSP;
			this.meshes = scene.GetModels; 
			this.lights = scene.GetLights;
			this.maxl = this.lights.Length;
		}
		//#endregion

		public static int PhotonRadius
		{
			get{ return TextureMap.FootPrintRadius; }
			set{ TextureMap.FootPrintRadius = value; }
		}

		public static void QuickSave()
		{
			TextureMap.QuickSave();
		}

		public static void QuickSetTexel(short IX, float[] UV, Color colour)
		{
			TextureMap.QuickSetPixel(IX, UV, colour);
		}
	}

	public class Scene
	{
		private Model3D[] scenery;
		private Tags.Sbsp.TagBsp bsp;
		private LightSource[] lights;

		public Scene(Tags.Sbsp.TagBsp bsp)
		{
			this.bsp = bsp;
			this.scenery = new Model3D[0];
			this.lights = new LightSource[0];
		}

		public void AddScenery(Model3D scenery)
		{
			Model3D[] combArray = new Model3D[this.scenery.Length+1];
			this.scenery.CopyTo(combArray, 0);
			combArray[combArray.Length-1] = scenery;
			this.scenery = new Model3D[combArray.Length];
			combArray.CopyTo(this.scenery,0);
		}

		public void AddLight(LightSource light, LightSource.Type type)
		{
			LightSource[] combArray = new LightSource[this.lights.Length+1];
			this.lights.CopyTo(combArray, 0);
			combArray[combArray.Length-1] = light;
			combArray[combArray.Length-1].LType = type;
			this.lights = new LightSource[combArray.Length];
			combArray.CopyTo(this.lights,0);
		}

		public Model3D[] GetModels
		{
			get{ return this.scenery; }
		}

		public LightSource[] GetLights
		{
			get{ return this.lights; }
		}

		public Tags.Sbsp.TagBsp GetBSP
		{
			get{ return this.bsp; }
		}
	}
}