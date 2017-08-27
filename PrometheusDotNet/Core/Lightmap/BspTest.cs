using System;
using System.IO;
using System.Diagnostics;
using Microsoft.DirectX;

using Prometheus.Core.Util;

namespace Prometheus.Core.Lightmap
{
	public class BSP_LEAF
	{
		private ushort cluster,LfSurfCnt;
		private ulong LfSurfBgnIX;

		public BSP_LEAF(ref BinaryReader br)
		{
			br.BaseStream.Position += 8;
			cluster = br.ReadUInt16();
			LfSurfCnt = br.ReadUInt16();
			LfSurfBgnIX = br.ReadUInt32();
		}

		public override string ToString()
		{
			string s = string.Format("Cluster index:{0} Surface Count:{1} Surface Begin Index:{2}",
				this.cluster, this.LfSurfCnt, this.LfSurfBgnIX);
			return base.ToString ();
		}

	}

	public class PLANE
	{
		private float a,b,c,d;
		private Plane plane;

		public PLANE(ref BinaryReader br)
		{
			this.a = br.ReadSingle();
			this.b = br.ReadSingle();
			this.c = br.ReadSingle();
			this.d = br.ReadSingle();
			
			this.plane = new Plane(a,b,c,d);
		}

		/// <summary>
		/// Ray-plane intersection fast.
		/// </summary>
		/// <param name="orig">Ray origin</param>
		/// <param name="dir">Ray direction</param>
		/// <returns>0 -parrell, -1 -back, 1 -front</returns>
		public int Intersect(Vector3 orig, Vector3 dir)
		{
			float EPSILION = 0.000001f;
			float Vd = Plane.DotNormal(this.plane, dir);

			if((Vd < -EPSILION)||(Vd > EPSILION))
			{
				float V0 = Plane.DotNormal(this.plane, orig);
				float t = V0/Vd;

				if(t>=0)
					return 1; //Front of origin.
				else
					return -1; //Back of origin.
			}
			else
				return 0;
		}

		public override string ToString()
		{
			string s = string.Format("a:{0} b:{1} c:{2} d:{3}",this.a,this.b,this.c,this.d);
			return s;
		}

	}

	public class BSP_3D_NODES
	{
		private PLANE plane = null;
		private BSP_3D_NODES backChld = null;
		private BSP_3D_NODES frontChld = null;
		private BSP_LEAF backLef = null;
		private BSP_LEAF frontLef = null;

		public BSP_3D_NODES(ref BinaryReader br, uint offsetN, uint offsetP, uint offsetL)
		{
			ulong planeIX = br.ReadUInt32();
			long backChldIX = br.ReadInt32();
			long frontChldIX = br.ReadInt32();

			br.BaseStream.Position = ((long)offsetP + (long)(16*planeIX));
			this.plane = new PLANE(ref br);

			if(backChldIX >= 0)
			{
				br.BaseStream.Position = offsetN + (12*backChldIX);
				this.backChld = new BSP_3D_NODES(ref br, offsetN, offsetP, offsetL);
			}

			if(backChldIX < -1)
			{
				br.BaseStream.Position = offsetL + (16*(backChldIX+2147483648));
				this.backLef = new BSP_LEAF(ref br);
			}

			if(frontChldIX >= 0)
			{
				br.BaseStream.Position = offsetN + (12*frontChldIX);
				this.frontChld = new BSP_3D_NODES(ref br, offsetN, offsetP, offsetL);
			}

			if(frontChldIX < -1)
			{
				br.BaseStream.Position = offsetL + (16*(frontChldIX+2147483648));
				this.frontLef = new BSP_LEAF(ref br);
			}
		}

		public bool RayIntersect(Vector3 orig, Vector3 dir)
		{
			int i =this.plane.Intersect(orig, dir);

			if(i == 0)
				throw new Exception("Doh...");

			if(i == 1)//Check all front side.
			{
				if(this.frontChld != null)
				{
					return this.frontChld.RayIntersect(orig, dir);
				}
				else if(this.frontLef != null)
				{
					return true;
				}
				else
					return false;
			}
			else
			{
				if(this.backChld != null) //Check all back side
				{
					return this.backChld.RayIntersect(orig, dir);
				}
				else if(this.backLef != null)
				{
					return true;
				}
				else
					return false;
			}
		}
	}
}
