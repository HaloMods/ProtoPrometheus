using System;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using Prometheus.Core.Tags;
using Prometheus.Core.Render;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
//using Core.Lightmap;
using Prometheus.Core.Lightmap;

namespace Prometheus.Core.Tags.Sbsp
{
	public class BSP_SECTION_HEADER
	{
		uint BspHeaderOffset,
			Xbox_Vert_ReflexiveCount,
			Xbox_Vert_ReflexiveStart,
			Xbox_LightmapVert_ReflexiveCount,
			Xbox_LightmapVert_ReflexiveStart;
		char[] tag = new char[4]; // sbsp
       
		public void Load(ref BinaryReader br)
		{
			BspHeaderOffset = br.ReadUInt32();
			Xbox_Vert_ReflexiveCount = br.ReadUInt32();
			Xbox_Vert_ReflexiveStart = br.ReadUInt32();
			Xbox_LightmapVert_ReflexiveCount = br.ReadUInt32();
			Xbox_LightmapVert_ReflexiveStart = br.ReadUInt32();

			tag = br.ReadChars(4);
		}
	}

	/// <summary>
	/// This struct (or array of structs) is located at the end 
	/// of the scenario. It defines the location of the BSP and 
	/// its size.
	/// </summary>
	public class SCENARIO_BSP_INFO
	{
		uint BspStart,
			BspSize,
			Magic,
			Zero1;
		TAG_REFERENCE BspTag;

		public SCENARIO_BSP_INFO()
		{
			BspTag = new TAG_REFERENCE();
		}

		public void Load(ref BinaryReader br)
		{
			BspStart = br.ReadUInt32();
			BspSize = br.ReadUInt32();
			Magic = br.ReadUInt32();
			Zero1 = br.ReadUInt32();

			BspTag.Load(ref br);
		}
	}

	/// <summary>
	///  This is the BSP Header, it defines the location of 
	///  everything in the BSP.
	/// </summary>
	public class BSP_HEADER
	{
		public BOUNDING_BOX2 WorldBounds;
		public REFLEXIVE Shaders,
			CollBspHeader,
			Nodes,
			Leaves,
			LeafSurfaces,
			SubmeshTriIndices,
			Lightmap,
			Chunk10,
			Chunk11,
			Chunk12,
			Clusters,
			ClusterData,
			ClusterPortals,
			Chunk16a,
			BreakableSurfaces,
			FogPlanes,
			FogRegions,
			FogOrWeatherPallette,
			Chunk16f,
			Chunk16g,
			Weather,
			WeatherPolyhedra,
			Chunk19, // bitmaps related 19/20
			Chunk20,
			PathfindingSurface,
			Chunk24,
			BackgroundSound,
			SoundEnvironment,
			SoundPASData,
			Chunk26,
			Chunk27,
			Markers,
			DetailObjects,
			RuntimeDecals;
		public TAG_REFERENCE LightmapsTag;
		public int ClusterDataSize,
			SoundPASDataSize;
		public uint[]  unk4,
			unk10;
		public uint  unk11,
			unk12;

		//new code
		public short vehicleFloor;
		public short vehicleCeiling;
		public float[] ambientColor = new float[3];
		public float[] defaultColorLight0 = new float[3];
		public Vector3 defaultDistLight0 = new Vector3();
		public float[] defaultColorLight1 = new float[3];
		public Vector3 defaultDistLight1 = new Vector3();
		public float[] defaultReflectionTint = new float[4];
		public Vector3 defaultShadowVector = new Vector3();
		public float[] defaultShadowColor = new float[3];

		public BSP_HEADER()
		{
			LightmapsTag = new TAG_REFERENCE();
			unk4 = new uint[0x25];
			Shaders = new REFLEXIVE();
			CollBspHeader = new REFLEXIVE();
			Nodes = new REFLEXIVE();
			WorldBounds = new BOUNDING_BOX2();
			Leaves = new REFLEXIVE();
			LeafSurfaces = new REFLEXIVE();
			SubmeshTriIndices = new REFLEXIVE();
			Lightmap = new REFLEXIVE();
			Chunk10 = new REFLEXIVE();
			Chunk11 = new REFLEXIVE();
			Chunk12 = new REFLEXIVE();
			Clusters = new REFLEXIVE();
			uint unk11;
			ClusterData = new REFLEXIVE();
			ClusterPortals = new REFLEXIVE();
			Chunk16a = new REFLEXIVE();
			BreakableSurfaces = new REFLEXIVE();
			FogPlanes = new REFLEXIVE();
			FogRegions = new REFLEXIVE();
			FogOrWeatherPallette = new REFLEXIVE();
			Chunk16f = new REFLEXIVE();
			Chunk16g = new REFLEXIVE();
			Weather = new REFLEXIVE();
			WeatherPolyhedra = new REFLEXIVE();
			Chunk19 = new REFLEXIVE();
			Chunk20 = new REFLEXIVE();
			PathfindingSurface = new REFLEXIVE();
			Chunk24 = new REFLEXIVE();
			BackgroundSound = new REFLEXIVE();
			SoundEnvironment = new REFLEXIVE();
			SoundPASData = new REFLEXIVE();
			Chunk26 = new REFLEXIVE();
			Chunk27 = new REFLEXIVE();
			Markers = new REFLEXIVE();
			DetailObjects = new REFLEXIVE();
			RuntimeDecals = new REFLEXIVE();
			unk10 = new uint[9];
		}

		public void Load(ref BinaryReader br)
		{
			LightmapsTag.Load(ref br);

			//new code
			//br.BaseStream.Position += 4;//not sure if this should be at start or end
			vehicleFloor = br.ReadInt16();
			vehicleCeiling = br.ReadInt16();
			br.BaseStream.Position += 20;

			ambientColor[0] = br.ReadSingle();
			ambientColor[1] = br.ReadSingle();
			ambientColor[2] = br.ReadSingle();
			br.BaseStream.Position += 4;
			defaultColorLight0[0] = br.ReadSingle();
			defaultColorLight0[1] = br.ReadSingle();
			defaultColorLight0[2] = br.ReadSingle();

			defaultDistLight0.X = br.ReadSingle();
			defaultDistLight0.Y = br.ReadSingle();
			defaultDistLight0.Z = br.ReadSingle();

			defaultColorLight1[0] = br.ReadSingle();
			defaultColorLight1[1] = br.ReadSingle();
			defaultColorLight1[2] = br.ReadSingle();

			defaultDistLight1.X = br.ReadSingle();
			defaultDistLight1.Y = br.ReadSingle();
			defaultDistLight1.Z = br.ReadSingle();
			br.BaseStream.Position += 12;

			defaultReflectionTint[0] = br.ReadSingle();
			defaultReflectionTint[1] = br.ReadSingle();
			defaultReflectionTint[2] = br.ReadSingle();
			defaultReflectionTint[3] = br.ReadSingle();

			defaultShadowVector.X = br.ReadSingle();
			defaultShadowVector.Y = br.ReadSingle();
			defaultShadowVector.Z = br.ReadSingle();

			defaultShadowColor[0] = br.ReadSingle();
			defaultShadowColor[1] = br.ReadSingle();
			defaultShadowColor[2] = br.ReadSingle();
			br.BaseStream.Position += 4;
			br.BaseStream.Position += 4;//not sure if this should be at start or end

			//for(int i=0; i<0x25; i++)
			//  unk4[i] = br.ReadUInt32();
			Shaders.Load(ref br);
			CollBspHeader.Load(ref br);
			Nodes.Load(ref br);
			WorldBounds.Load(ref br);
			Leaves.Load(ref br);
			LeafSurfaces.Load(ref br);
			SubmeshTriIndices.Load(ref br);
			Lightmap.Load(ref br);
			Chunk10.Load(ref br);
			Chunk11.Load(ref br);
			Chunk12.Load(ref br);
			Clusters.Load(ref br);
			ClusterDataSize = br.ReadInt32();
			unk11 = br.ReadUInt32();
			ClusterData.Load(ref br);
			ClusterPortals.Load(ref br);
			Chunk16a.Load(ref br);
			BreakableSurfaces.Load(ref br);
			FogPlanes.Load(ref br);
			FogRegions.Load(ref br);
			FogOrWeatherPallette.Load(ref br);
			Chunk16f.Load(ref br);
			Chunk16g.Load(ref br);
			Weather.Load(ref br);
			WeatherPolyhedra.Load(ref br);
			Chunk19.Load(ref br);
			Chunk20.Load(ref br);
			PathfindingSurface.Load(ref br);
			Chunk24.Load(ref br);
			BackgroundSound.Load(ref br);
			SoundEnvironment.Load(ref br);
			SoundPASDataSize = br.ReadInt32();
			unk12 = br.ReadUInt32();
			SoundPASData.Load(ref br);
			Chunk26.Load(ref br);
			Chunk27.Load(ref br);
			Markers.Load(ref br);
			DetailObjects.Load(ref br);
			RuntimeDecals.Load(ref br);
			for(int i=0; i<9; i++)
				unk10[i] = br.ReadUInt32();

			//load lightmap pathname
			char[] tmp = new char[LightmapsTag.StringLength];
			tmp = br.ReadChars(LightmapsTag.StringLength);
			LightmapsTag.data = new string(tmp);
			br.BaseStream.Position += 1;
		}
	}

	
	public class DEFAULT_LIGHT_BLOCK
	{
		public short vehicleFloor;
		public short vehicleCeiling;
		public float[] ambientColor = new float[3];
		public float[] defaultColorLight0 = new float[3];
		public Vector3 defaultDistLight0 = new Vector3();
		public float[] defaultColorLight1 = new float[3];
		public Vector3 defaultDistLight1 = new Vector3();
		public float[] defaultReflectionTint = new float[4];
		public Vector3 defaultShadowVector = new Vector3();
		public float[] defaultShadowColor = new float[3];
    
		public void Load(BinaryReader br)
		{
			/*
		_lightmaps.Read(reader);
		_vehicleFloor.Read(reader);
		_vehicleCeiling.Read(reader);
		__unnamed.Read(reader);20
		_defaultAmbientColor.Read(reader);
		__unnamed2.Read(reader);4
		_defaultDistantLight0Color.Read(reader);
		_defaultDistantLight0Direction.Read(reader);
		_defaultDistantLight1Color.Read(reader);
		_defaultDistantLight1Direction.Read(reader);
		__unnamed3.Read(reader);12
		_defaultReflectionTint.Read(reader);
		_defaultShadowVector.Read(reader);
		_defaultShadowColor.Read(reader);
		__unnamed4.Read(reader);4
  
      
	   __unnamed = new Pad(20);
	  __unnamed2 = new Pad(4);
	  __unnamed3 = new Pad(12);
	  __unnamed4 = new Pad(4);
	  __unnamed5 = new Pad(12);
	  __unnamed6 = new Pad(12);
	  __unnamed7 = new Pad(24);


			 */

			//lightmaps tagref, string
			br.BaseStream.Position = 16;
			vehicleFloor = br.ReadInt16();
			vehicleCeiling = br.ReadInt16();
			br.BaseStream.Position += 20;

			ambientColor[0] = br.ReadSingle();
			ambientColor[1] = br.ReadSingle();
			ambientColor[2] = br.ReadSingle();
			br.BaseStream.Position += 4;
			defaultColorLight0[0] = br.ReadSingle();
			defaultColorLight0[1] = br.ReadSingle();
			defaultColorLight0[2] = br.ReadSingle();

			defaultDistLight0.X = br.ReadSingle();
			defaultDistLight0.Y = br.ReadSingle();
			defaultDistLight0.Z = br.ReadSingle();

			defaultColorLight1[0] = br.ReadSingle();
			defaultColorLight1[1] = br.ReadSingle();
			defaultColorLight1[2] = br.ReadSingle();

			defaultDistLight1.X = br.ReadSingle();
			defaultDistLight1.Y = br.ReadSingle();
			defaultDistLight1.Z = br.ReadSingle();
			br.BaseStream.Position += 12;

			defaultReflectionTint[0] = br.ReadSingle();
			defaultReflectionTint[1] = br.ReadSingle();
			defaultReflectionTint[2] = br.ReadSingle();
			defaultReflectionTint[3] = br.ReadSingle();

			defaultShadowVector.X = br.ReadSingle();
			defaultShadowVector.Y = br.ReadSingle();
			defaultShadowVector.Z = br.ReadSingle();

			defaultShadowColor[0] = br.ReadSingle();
			defaultShadowColor[1] = br.ReadSingle();
			defaultShadowColor[2] = br.ReadSingle();
			br.BaseStream.Position += 4;
		}
	}

	public class BSP_SHADER
	{
		public TAG_REFERENCE LightmapsTag;
		public ushort[] UnkFlags;

		public BSP_SHADER()
		{
			LightmapsTag = new TAG_REFERENCE();
			UnkFlags = new ushort[2];
		}

		public void Load(ref BinaryReader br)
		{
			LightmapsTag.Load(ref br);

			UnkFlags[0] = br.ReadUInt16();
			UnkFlags[1] = br.ReadUInt16();
		}
	}

	#region COLLBSP_HEADER
	public class COLLBSP_HEADER
	{
		public REFLEXIVE bsp_3d_nodes,
			planes,
			leaves,
			bsp_2D_refs,
			bsp_2D_nodes,
			surfaces,
			edges,
			vertices;

		public COLLBSP_HEADER()
		{
			this.bsp_3d_nodes = new REFLEXIVE();
			this.planes = new REFLEXIVE();
			this.leaves = new REFLEXIVE();
			this.bsp_2D_refs = new REFLEXIVE();
			this.bsp_2D_nodes = new REFLEXIVE();
			this.surfaces = new REFLEXIVE();
			this.edges = new REFLEXIVE();
			this.vertices = new REFLEXIVE();
		}

		public void Load(ref BinaryReader br)
		{
			this.bsp_3d_nodes.Load(ref br);
			this.planes.Load(ref br);
			this.leaves.Load(ref br);
			this.bsp_2D_refs.Load(ref br);
			this.bsp_2D_nodes.Load(ref br);
			this.surfaces.Load(ref br);
			this.edges.Load(ref br);
			this.vertices.Load(ref br);
		}
	}
	#endregion

	//#region BSP_3D_NODES
	/*public class BSP_3D_NODES //DO NOT USE THIS ANYMORE (JamesD);
	{
		public ulong planeIX;	
		public ushort backChldFlg;
		public long backChldIX;
		public ushort frontChldFlg;
		public long frontChldIX;

		public void Load(ref BinaryReader br)
		{
			this.planeIX = br.ReadUInt32();
			this.backChldIX = br.ReadInt32();
			//this.backChldFlg = br.ReadUInt16();
			this.frontChldIX = br.ReadInt32();
			//this.frontChldFlg = br.ReadUInt16();

			Trace.WriteLine(string.Format("{0},{1}",backChldIX,frontChldIX));
		}
	}*/
	//#endregion

	/*#region PLANES
	public class PLANES
	{
		public Plane D3dPlane;
		public float a,b,c,d;

		public PLANES()
		{
			this.a = new float();
			this.b = new float();
			this.c = new float();
			this.d = new float();
		}

		public void Load(ref BinaryReader br)
		{
			Trace.WriteLine(br.BaseStream.Position);
			this.a = br.ReadSingle();
			this.b = br.ReadSingle();
			this.c = br.ReadSingle();
			this.d = br.ReadSingle();
		}
	}
	#endregion*/

	public class LEAVES
	{
		public short flagDss;
		public short bsp2drefCount;
		public long fbsp2dref;

		public void Load(ref BinaryReader br)
		{
			this.flagDss = br.ReadInt16();
			this.bsp2drefCount = br.ReadInt16();
			this.fbsp2dref = br.ReadInt32();
		}
	}

	public class BSP_2D_REFS
	{
		public long planeIX;
		public long bsp2dIX;
		
		public void Load(ref BinaryReader br)
		{
			this.planeIX = br.ReadInt32();
			this.bsp2dIX = br.ReadInt32();
		}
	}

	public class BSP_2D_NODES
	{
		public float i;
		public float j;
		public float k;
		public long leftChld;
		public long rightChld;
		
		public void Load(ref BinaryReader br)
		{
			this.i = br.ReadSingle();
			this.j = br.ReadSingle();
			this.k = br.ReadSingle();
			this.leftChld = br.ReadInt32();
			this.rightChld = br.ReadInt32();	
		}
	}

	public class SURFACES
	{
		public long planeIX;
		public long fedgeIX;
		public bool flagDss;
		public bool flagInv;
		public bool flagClm;
		public bool flagBrk;
		public byte SurfBrk;
		public short SurfMat;
		private bool FlagSkip;
		
		public void Load(ref BinaryReader br)
		{
			this.planeIX = br.ReadInt32();
			this.fedgeIX = br.ReadInt32();
			this.flagDss = (0 != br.ReadInt32());
			this.flagInv = (0 != br.ReadInt32());
			this.flagClm = (0 != br.ReadInt32());
			this.flagBrk = (0 != br.ReadInt32());
			br.BaseStream.Position += 16;
			//FlagSkip = (bool)br.ReadInt32();
			//FlagSkip = (bool)br.ReadInt32();
			//FlagSkip = (bool)br.ReadInt32();
			//FlagSkip = (bool)br.ReadInt32();
			this.SurfBrk = br.ReadByte();
			this.SurfMat = br.ReadInt16();		
		}		
	}

	#region EDGES
	public class EDGES
	{
		public int startVertIX,
			endVertIX,
			fedgeIX,
			redgeIX,
			lsurfaceIX,
			rsurfaceIX;

		public EDGES()
		{
			this.startVertIX = new int();
			this.endVertIX = new int();
			this.fedgeIX = new int();
			this.redgeIX = new int();
			this.lsurfaceIX = new int();
			this.rsurfaceIX = new int();
		}

		public void Load(ref BinaryReader br)
		{
			this.startVertIX = br.ReadInt32();
			this.endVertIX = br.ReadInt32();
			this.fedgeIX = br.ReadInt32();
			this.redgeIX = br.ReadInt32();
			this.lsurfaceIX = br.ReadInt32();
			this.rsurfaceIX = br.ReadInt32();
		}
	}
	#endregion

	#region VERTICES
	public class VERTICES
	{
		public float x,y,z;
		public int edgeIX;

		public VERTICES()
		{
			this.x = new float();
			this.y = new float();
			this.z = new float();
			this.edgeIX = new int();
		}

		public void Load(ref BinaryReader br)
		{
			this.x = br.ReadSingle();
			this.y = br.ReadSingle();
			this.z = br.ReadSingle();
			this.edgeIX = br.ReadInt32();
		}
	}
	#endregion

	/*public class BSP_NODES
	{
		public ushort[] unknown;

		public BSP_NODES()
		{
			unknown = new ushort[3];
		}

		public void Load(ref BinaryReader br)
		{
			unknown[0] = br.ReadUInt16();
			unknown[1] = br.ReadUInt16();
			unknown[2] = br.ReadUInt16();
		}
	}

	public class BSP_LEAF
	{
		short cluster;
		short LfSurfCnt;
		short LfSurfBgn;
		short unknown;

		public void Load(ref BinaryReader br)
		{
			cluster = br.ReadInt16();
			LfSurfCnt = br.ReadInt16();
			LfSurfBgn = br.ReadInt16();
			unknown = br.ReadInt16();
		}
	}*/

	public class BSP_LEAF_SURFACE
	{
		short Surf3x;
		short Surf;
		short node;
		short unknown;

		public void Load(ref BinaryReader br)
		{
			Surf3x = br.ReadInt16();
			Surf = br.ReadInt16();
			node = br.ReadInt16();
			unknown = br.ReadInt16();
		}
	}

	public class BspRay
	{
		/// <param name="intersect">returned point of intersection. Is (0,0,0) when return value is false.</param>
		/// <returns>true if intersection occurs, false otherwise</returns>
		public static bool RayIntersectPlanedist(Vector3 ray_origin, Vector3 ray_direction, Plane plane, float max, out float distance)
		{
			bool bIntersected = false;
			float Vd, V0, t;
			distance = -1;

			Vd = Plane.DotNormal(plane, ray_direction);

			if((Vd < -0.001f)||(Vd > 0.001f)) //check for parallel to plane
			{
				V0 = -Plane.DotNormal(plane, ray_origin) + plane.D;
				t = V0/Vd;
				distance =  ((ray_direction.X*t)*(ray_direction.X*t) + 
					(ray_direction.Y*t)*(ray_direction.Y*t) + (ray_direction.Z*t)*(ray_direction.Z * t));
				distance = (float)Math.Sqrt(distance);
				distance =  distance*Math.Sign(t);
				if(distance >= 0) //intersection is not behind the ray origin
				{
					if(distance < max) 
					{
						bIntersected = true;
					}
				}
			}
			return(bIntersected);
		}

		public static bool OriginNodeFront(Vector3 ray_origin, Plane plane)
		{
			float dist; //distance along normal to point mb negative if in oppisite direction
			bool OriginFront = false;
      
			if(plane.Dot(ray_origin) > 0)
				OriginFront = true;

			//dist = plane.A*ray_origin.X + plane.B*ray_origin.Y + plane.C*ray_origin.Z + plane.D;
			//if (dist > 0)
			//{
			//  OriginFront = true;
			//}
		
			return OriginFront;
		}
		// must always call function original at root node of 0
		// length must pre calculated as the distance from the ray_origin along the ray_direction to the farthest possible intersection of the bounding box

		/*
		public static bool BspRayIntersect(short node, ushort nodeflag, Vector3 ray_origin, Vector3 ray_direction, float length, out short surface, Vector3 intersection)
		{
		  float dist;
		  short near;
		  short nearflg;
		  short far;
		  short farflg;
		  Vector3 new_origin = new Vector3();
		
		  if(node == -1) // this 65535 is "ffff" in hex, missed bsp
		  {
			return(false);
		  }
		  if(nodeflag == 32768) // 32768 is "8000" in hex, your in a leaf
		  {
			// do james intersect test here using the triangle list form the leaf
			// return closest one
		  }
		  // grab near node 
		  if(OriginNodeFront == true)
		  {
			nearflg = BSP_3D_NODES(node).frontChldFlg;
			near =    BSP_3D_NODES(node).frontChldIX;
			farflg = BSP_3D_NODES(node).backChldFlg;
			far =    BSP_3D_NODES(node).backChldIX;
		  }
		  else
		  {
			farflg = BSP_3D_NODES(node).frontChldFlg;
			far =    BSP_3D_NODES(node).frontChldIX;
			nearflg = BSP_3D_NODES(node).backChldFlg;
			near =    BSP_3D_NODES(node).backChldIX;
		  }

		  if(RayIntersectPlanedist(ray_origin, ray_direction, PLANES(BSP_3D_NODES[node].planeIX, length), out dist) == true)
		  {
			if(BspRayIntersect(near, nearflg, ray_origin, ray_direction, dist, out surface, intersection) == false)
			{
			  new_origin.X = ray_origin.X + d*ray_direction.X;
			  new_origin.Y = ray_origin.Y + d*ray_direction.Y;
			  new_origin.Z = ray_origin.Z + d*ray_direction.Z;
			  return(BspRayIntersect(far, farflg, new_origin, ray_direction, length, out surface, intersection));
			}
		  }
		  else
		  {		 
			return(BspRayIntersect(near, nearflg, ray_origin, ray_direction, length, out surface, intersection));
		  }	  
		}
		*/
	}
	
	
	
	
	
	/// <summary>
	///  Lightmaps (world meshes)
	/// </summary>

	/// <summary>
	/// This struct (array of structs actually) is pointed to 
	/// by the BSP Header under the "Lightmap" field. It
	/// in turn points to the visible submesh headers that 
	/// contain the actual vertex counts and pointers, etc.
	/// The purpose of this is to group the world meshes by 
	/// texture to optimize texture cacheing.
	/// </summary>
	public class BSP_LIGHTMAP
	{
		public short LightmapIndex;
		public REFLEXIVE Material;
		public MATERIAL_SUBMESH_HEADER[] Materials;

		public BSP_LIGHTMAP()
		{
			Material = new REFLEXIVE();
		}

		public void Load(ref BinaryReader br)
		{
			LightmapIndex = br.ReadInt16();
			br.BaseStream.Position += 18;

			Material.Load(ref br);
			Materials = new MATERIAL_SUBMESH_HEADER[Material.Count];
		}

		public void LoadMaterials(ref BinaryReader br, MapfileVersion ver, short[] IndexData)
		{
			int m;
			br.BaseStream.Position = Material.Offset;
			for(m=0; m<Material.Count; m++)
			{
				Materials[m] = new MATERIAL_SUBMESH_HEADER();
				Materials[m].Load(ref br);
			}

			for(m=0; m<Material.Count; m++)
			{
				char[] tmp = new char[Materials[m].ShaderTag.StringLength];
				//br.BaseStream.Position = Materials[m].ShaderTag.NamePtr;
				tmp = br.ReadChars(Materials[m].ShaderTag.StringLength);
				br.BaseStream.Position += 1;
				Materials[m].ShaderTag.data = new string(tmp);
				string tag_type = new string(Materials[m].ShaderTag.tag);
				Materials[m].ShaderManagerIndex = MdxRender.SM.RegisterShader(new TagFileName(Materials[m].ShaderTag.data, tag_type, ver));
				bool bUsesLightmap = (this.LightmapIndex != -1);
				//Trace.WriteLine("  material["+m.ToString()+"]  "+Materials[m].ShaderTag.data + " uses lightmap = " + bUsesLightmap.ToString());
				Materials[m].LoadRawData(ref br, bUsesLightmap, IndexData);
			}
		}

		/// <summary>
		/// Tests the mesh group assiciated with this lightmap for ray intersection.
		/// </summary>
		/// <param name="Origin">Ray Origin.</param>
		/// <param name="Direction">Ray Direction.</param>
		/// <param name="point">Point of intersection.</param>
		/// <param name="UV">UV co-ordinates.</param>
		/// <param name="LIx">Lightmap index UV relate to.</param>
		/// <returns></returns>
		public bool RayAABBTest(Vector3 Origin, Vector3 Direction, 
			out Vector3 point, out float[] UV, out short LIx, out float t)
		{
			float c = 0xffffffffffffff;
			bool result = false;
			Vector3 tpoint = new Vector3();
			float[] tUV = new float[2];
			short tLIx = new short();

			for(int i=0; i<Materials.Length; i++)
			{
				if(Materials[i].RayAABBIntersect(Origin, Direction, out point, out UV, out t))
				{
					result = true;
					if(t<c)
					{
						c = t;
						tLIx = this.LightmapIndex;
						tpoint = point;
						tUV = UV;
					}
				}
			}
			if(result)
			{
				t=c;
				LIx = tLIx;
				point = tpoint;
				UV = tUV;
				return true;
			}
			else
			{
				t=-1;
				LIx = -1;
				point = new Vector3(0,0,0);
				UV = new float[2]{0,0};
				return false;
			}
		}
	}


	/// <summary>
	///  There is one of these structs for every submesh in 
	///  the map.
	/// </summary>
	public class MATERIAL_SUBMESH_HEADER
	{
		public TAG_REFERENCE ShaderTag;
		public uint UnkZero2,
			SurfaceIndicesOffset,
			SurfaceCount,
			DistLightCount,
			BreakableSurface,
			UnkCount1,
			TagVertexCount,
			VertexOffset1,
			MemoryVertexOffset,
			Vert_Reflexive,
			UnkAlways3,
			VertexCount2,
			VertexOffset2,
			MemoryLightmapOffset,
			LightmapVert_Reflexive,
			TagVertexOffset;
		public int PcVertexDataOffset;
		public uint UnkZero6,
			CompVertBufferSize,
			UnkZero7,
			SomeOffset2,
			VertexDataOffset,
			UnkZero8,
			UnkZero5,
			UncompVertBufferSize;
		public float[] Centroid,
			AmbientColor,
			DistLight1,
			DistLight2,
			unkFloat2,
			ReflectTint,
			ShadowVector,
			ShadowColor,
			Plane;

		//Render stuff
		public PositionTexture2[] DxVerts;
		public int ShaderManagerIndex = -1;
    public int LightmapTextureIndex = -1;
    public int LightmapSubTextureIndex = -1;
    public int MinIndex = 40000;
		public int MaxIndex = -40000;
		protected VertexBuffer VertData = null;
		public EnhancedMesh MaterialMesh = null;

		public MATERIAL_SUBMESH_HEADER()
		{
			ShaderTag = new TAG_REFERENCE();

			Centroid = new float[3];
			AmbientColor = new float[3];
			DistLight1 = new float[6];
			DistLight2 = new float[6];
			unkFloat2 = new float[3];
			ReflectTint = new float[4];
			ShadowVector = new float[3];
			ShadowColor = new float[3];
			Plane = new float[4];
		}

		public void Load(ref BinaryReader br)
		{
			ShaderTag.Load(ref br);
			UnkZero2 = br.ReadUInt32();
			SurfaceIndicesOffset = br.ReadUInt32();
			SurfaceCount = br.ReadUInt32();

			Centroid[0] = br.ReadSingle();
			Centroid[1] = br.ReadSingle();
			Centroid[2] = br.ReadSingle();

			AmbientColor[0] = br.ReadSingle();
			AmbientColor[1] = br.ReadSingle();
			AmbientColor[2] = br.ReadSingle();
    
			DistLightCount = br.ReadUInt32();

			DistLight1[0] = br.ReadSingle();
			DistLight1[1] = br.ReadSingle();
			DistLight1[2] = br.ReadSingle();
			DistLight1[3] = br.ReadSingle();
			DistLight1[4] = br.ReadSingle();
			DistLight1[5] = br.ReadSingle();

			DistLight2[0] = br.ReadSingle();
			DistLight2[1] = br.ReadSingle();
			DistLight2[2] = br.ReadSingle();
			DistLight2[3] = br.ReadSingle();
			DistLight2[4] = br.ReadSingle();
			DistLight2[5] = br.ReadSingle();

			unkFloat2[0] = br.ReadSingle();
			unkFloat2[1] = br.ReadSingle();
			unkFloat2[2] = br.ReadSingle();

			ReflectTint[0] = br.ReadSingle();
			ReflectTint[1] = br.ReadSingle();
			ReflectTint[2] = br.ReadSingle();
			ReflectTint[3] = br.ReadSingle();

			ShadowVector[0] = br.ReadSingle();
			ShadowVector[1] = br.ReadSingle();
			ShadowVector[2] = br.ReadSingle();

			ShadowColor[0] = br.ReadSingle();
			ShadowColor[1] = br.ReadSingle();
			ShadowColor[2] = br.ReadSingle();

			Plane[0] = br.ReadSingle();
			Plane[1] = br.ReadSingle();
			Plane[2] = br.ReadSingle();
			Plane[3] = br.ReadSingle();

			BreakableSurface = br.ReadUInt32();
			UnkCount1 = br.ReadUInt32();
			TagVertexCount = br.ReadUInt32();
			VertexOffset1 = br.ReadUInt32();
			MemoryVertexOffset = br.ReadUInt32();
			Vert_Reflexive = br.ReadUInt32();
			UnkAlways3 = br.ReadUInt32();
			VertexCount2 = br.ReadUInt32();
			VertexOffset2 = br.ReadUInt32();
			MemoryLightmapOffset = br.ReadUInt32();
			LightmapVert_Reflexive = br.ReadUInt32();

			UncompVertBufferSize = br.ReadUInt32();
			UnkZero5 = br.ReadUInt32();

			TagVertexOffset = br.ReadUInt32();
			PcVertexDataOffset = br.ReadInt32();
			UnkZero6 = br.ReadUInt32();
			CompVertBufferSize = br.ReadUInt32();
			UnkZero7 = br.ReadUInt32();
			SomeOffset2 = br.ReadUInt32();
			VertexDataOffset = br.ReadUInt32();
			UnkZero8 = br.ReadUInt32();
		}

		public void LoadRawData(ref BinaryReader br, bool bUsesLightmap, short[] IndexData)
		{
			PositionTexture2 vert = new PositionTexture2(0,0,0,0,0,0,0,0,0,0);
			MaterialMesh = new EnhancedMesh((int)SurfaceCount);

			//read in vertex data
      if(bUsesLightmap)
			  MaterialMesh.AllocateVertexBuffer(MeshFormat.Bsp, (int)TagVertexCount);
      else
        MaterialMesh.AllocateVertexBuffer(MeshFormat.Model, (int)TagVertexCount);

			for(int v=0; v<TagVertexCount; v++)
			{
				vert.position.X = br.ReadSingle();
				vert.position.Y = br.ReadSingle();
				vert.position.Z = br.ReadSingle();
				vert.normal.X = br.ReadSingle();
				vert.normal.Y = br.ReadSingle();
				vert.normal.Z = br.ReadSingle();
				br.BaseStream.Position += 24;
				vert.u1 = br.ReadSingle();
				vert.v1 = br.ReadSingle();

        if(bUsesLightmap)
				  MaterialMesh.SetBspVertexData(v, vert.position, vert.normal, vert.u1, vert.v1);
        else
          MaterialMesh.LoadVertex(v,
            vert.position.X,
            vert.position.Y,
            vert.position.Z,
            vert.normal.X,
            vert.normal.Y,
            vert.normal.Z,
            vert.u1, vert.v1);
      }

			//vertex sizes:
			//	uncompressed lightmap 20 (u,v,Nx,Ny,Nz)
			//  compressed lightmap    8 (u16,v16, Nx11, Ny11, Nz10)
			//  uncompressed verts    56 (x,y,z,u,v,Nx,Ny,Nz,Bx,By,Bz,Tx,Ty,Tz)
			//  compressed verts      32 (x32,y32,z32,...
			if(bUsesLightmap)
			{
				for(int v=0; v<TagVertexCount; v++)
				{
					br.BaseStream.Position += 12;
					vert.u2 = br.ReadSingle();
					vert.v2 = br.ReadSingle();
					MaterialMesh.SetBspVertexLightmapUV(v, vert.u2, vert.v2);
				}
				//skip over compressed data
        
				int skip = (int)TagVertexCount*40;
				br.BaseStream.Position += skip;
			}
			else
			{
				//skip over compressed data
				int skip = (int)TagVertexCount*32;
				br.BaseStream.Position += skip;
			}

			//load index data
			MaterialMesh.AllocateAndSetIndexBuffer(PrimitiveType.TriangleList, IndexData, 
				(int)SurfaceIndicesOffset*3, (int)SurfaceCount*3);

			MaterialMesh.UpdateDxBuffer();
		}

		public void DrawMaterial()
		{
      TagSenv BspSenv = MdxRender.SM.GetBspShader(ShaderManagerIndex);

      if(BspSenv != null)
      {
        BspSenv.ActivateSenvLightmap(LightmapTextureIndex, LightmapSubTextureIndex);
        BspSenv.Pass1();
      }
      else
      {
        if(LightmapSubTextureIndex != -1)
        {
          int betterfixthis = 0;
        }
        MdxRender.SM.ActivateShader(ShaderManagerIndex, 0);
      }

      //activate lightmap
      //this.LightmapTextureIndex;
      //this.LightmapSubTextureIndex;
			MaterialMesh.RenderMesh();
		}

		/// <summary>
		/// First checks ray\aabb intersection, if true performs lightmap intersection, else otherwise.
		/// </summary>
		/// <param name="Origin">Origin of ray.</param>
		/// <param name="Direction">Direction of ray.</param>
		/// <param name="point">Point of intersection.</param>
		/// <param name="UV">UV co-ordinates of intersection.</param>
		/// <returns></returns>
		public bool RayAABBIntersect(Vector3 Origin, Vector3 Direction, out Vector3 point, out float[] UV, 
			out float t)
		{
			if(MaterialMesh.RayAABBIntersect(Origin, Direction))
			{
				if(MaterialMesh.RayBSPIntersectLight(Origin, Direction, false, out point, out UV, out t))
					return true;
				else
					return false;
			}
			else
			{
				t=0;
				point = new Vector3(0,0,0);
				UV = new float[2]{-1,-1};
				return false;
			}
		}

		public bool TextureColor(Vector3 orig, Vector3 dir, out float t, out float[] UV)
		{
			if(MaterialMesh.RayBSPIntersectTexture(orig,dir,true,out UV,out t))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}

	/// <summary>
	///  Triangle index; an array of these is pointed to from
	///  the MATERIAL_SUBMESH_HEADER.
	/// </summary>
	public class TRI_INDICES
	{
		ushort[] tri_ind;

		public TRI_INDICES()
		{
			tri_ind = new ushort[3];
		}

		public void Load(ref BinaryReader br)
		{
			tri_ind[0] = br.ReadUInt16();
			tri_ind[1] = br.ReadUInt16();
			tri_ind[2] = br.ReadUInt16();
		}
	}

	/// <summary>
	///  Xbox specific vertex
	/// </summary>
	public class COMPRESSED_BSP_VERT
	{
		float[] vertex_k,
			uv;
		uint  comp_normal,
			comp_binormal,
			comp_tangent;

		public COMPRESSED_BSP_VERT()
		{
			vertex_k = new float[3];
			uv = new float[2];
		}

		public void Load(ref BinaryReader br)
		{
			vertex_k[0] = br.ReadSingle();
			vertex_k[1] = br.ReadSingle();
			vertex_k[2] = br.ReadSingle();

			comp_normal = br.ReadUInt32();
			comp_binormal = br.ReadUInt32();
			comp_tangent = br.ReadUInt32();

			uv[0] = br.ReadSingle();
			uv[1] = br.ReadSingle();
		}
	}

	/// <summary>
	///  PC (uncompressed) vertex.  I think the xbox puts the
	///  verts in this format at runtime.
	/// </summary>
	public class UNCOMPRESSED_BSP_VERT
	{
		public float X;
		public float Y;
		public float Z;

		public float Tu;
		public float Tv;
		public float Lu;
		public float Lv;

		public UNCOMPRESSED_BSP_VERT()
		{
		}
	}

	/// <summary>
	///  Xbox specific lightmap vertex
	/// </summary>
	public class COMPRESSED_LIGHTMAP_VERT
	{
		uint comp_normal;
		short[] comp_uv;

		public COMPRESSED_LIGHTMAP_VERT()
		{
			comp_uv = new short[2];
		}

		public void Load(ref BinaryReader br)
		{
			comp_normal = br.ReadUInt32();

			comp_uv[0] = br.ReadInt16();
			comp_uv[1] = br.ReadInt16();
		}
	}

	/// <summary>
	///  PC (uncompressed) lightmap vertex.  I think the xbox
	///  puts the verts in this format at runtime.
	/// </summary>
	public class PC_LIGHTMAP_VERT
	{
		float[] normal,
			uv;

		public PC_LIGHTMAP_VERT()
		{
			normal = new float[3];
			uv = new float[2];
		}

		public void Load(ref BinaryReader br)
		{
			normal[0] = br.ReadSingle();
			normal[1] = br.ReadSingle();
			normal[2] = br.ReadSingle();

			uv[0] = br.ReadSingle();
			uv[1] = br.ReadSingle();
		}
	}

	/// <summary>
	///  BSP Clusters
	/// </summary>
	public class BSP_CLUSTER
	{
		REFLEXIVE PredictedResources;
		REFLEXIVE SubCluster;
		REFLEXIVE Portals;
		short SkyIndex;
		short	FogIndex;
		short	BackgroundSoundIndex;
		short	SoundEnvIndex;
		short	WeatherIndex;
		short TransitionBsp;

		BSP_SUBCLUSTER[] m_SubClusters = null;

		public BSP_CLUSTER()
		{
			PredictedResources = new REFLEXIVE();
			SubCluster = new REFLEXIVE();
			Portals = new REFLEXIVE();
		}

		public void Load(ref BinaryReader br)
		{
			SkyIndex = br.ReadInt16();
			FogIndex = br.ReadInt16();
			BackgroundSoundIndex = br.ReadInt16();
			SoundEnvIndex = br.ReadInt16();
			WeatherIndex = br.ReadInt16();
			TransitionBsp = br.ReadInt16();
			br.BaseStream.Position += 28;
			PredictedResources.Load(ref br);
			SubCluster.Load(ref br);
			br.BaseStream.Position += 28;
			Portals.Load(ref br);
		}
		public void LoadSubclusters(ref BinaryReader br)
		{
			br.BaseStream.Position = SubCluster.Offset;
			m_SubClusters = new BSP_SUBCLUSTER[SubCluster.Count];
			for(int s=0; s<SubCluster.Count; s++)
			{
				m_SubClusters[s] = new BSP_SUBCLUSTER();
				m_SubClusters[s].Load(ref br);
			}
		}
		public void RenderSubclusters(int index)
		{
			MdxRender.Dev.VertexFormat = CustomVertex.PositionColored.Format;
			for(int i=0; i<m_SubClusters.Length; i++)
				RenderBox.RenderLines(ref m_SubClusters[i].ClusterBounds, Color.White);
		}
	}

	public class BSP_SUBCLUSTER
	{
		public BOUNDING_BOX2 ClusterBounds;
		public REFLEXIVE SurfaceIndices;
  
		public BSP_SUBCLUSTER()
		{
			ClusterBounds = new BOUNDING_BOX2();
			SurfaceIndices = new REFLEXIVE();
		}

		public void Load(ref BinaryReader br)
		{
			ClusterBounds.Load(ref br);
			SurfaceIndices.Load(ref br);
		}
	}

	public class BSP_CLUSTER_PREDICTED_RESOURCE
	{
		short[] unk2;
		uint Precache_TagId;
  
		public BSP_CLUSTER_PREDICTED_RESOURCE()
		{
			unk2 = new short[2];
		}

		public void Load(ref BinaryReader br)
		{
			unk2[0] = br.ReadInt16();
			unk2[1] = br.ReadInt16();

			Precache_TagId = br.ReadUInt32();
		}
	}

	public class BSP_CLUSTER_PORTAL
	{
		short front_cluster,
			back_cluster;
		int   plane_index;
		float[] centroid;
		float bounding_radius;
		uint[]  unk1;
		REFLEXIVE Vertices;

		public BSP_CLUSTER_PORTAL()
		{
			centroid = new float[3];
			unk1 = new uint[7];
			Vertices = new REFLEXIVE();
		}

		public void Load(ref BinaryReader br)
		{
			front_cluster = br.ReadInt16();
			back_cluster = br.ReadInt16();

			plane_index = br.ReadInt32();

			centroid[0] = br.ReadSingle();
			centroid[1] = br.ReadSingle();
			centroid[2] = br.ReadSingle();

			bounding_radius = br.ReadSingle();

			unk1[0] = br.ReadUInt32();
			unk1[1] = br.ReadUInt32();
			unk1[2] = br.ReadUInt32();
			unk1[3] = br.ReadUInt32();
			unk1[4] = br.ReadUInt32();
			unk1[5] = br.ReadUInt32();
			unk1[6] = br.ReadUInt32();

			Vertices.Load(ref br);
		}
	}

	public class BSP_WEATHER
	{
		char[] Name;
		TAG_REFERENCE WeatherTag,
			WeatherTag2;
		uint[]  reserved,
			unk;
  
       
		public BSP_WEATHER()
		{
			Name = new char[32];

			reserved = new uint[20];
			unk = new uint[24];

			WeatherTag = new TAG_REFERENCE();
			WeatherTag2 = new TAG_REFERENCE();
		}

		public void Load(ref BinaryReader br)
		{
			Name = br.ReadChars(32);
			WeatherTag.Load(ref br);

			for(int i=0;i<20;i++)
				reserved[i] = br.ReadUInt32();

			WeatherTag2.Load(ref br);

			for(int i=0;i<24;i++)
				unk[i] = br.ReadUInt32();
		}
	}

	public class BSP_BACKGROUND_SOUND
	{
		char[] Name;
		TAG_REFERENCE SoundTag;
		uint[] unk;
  
		public BSP_BACKGROUND_SOUND()
		{
			Name = new char[32];
			SoundTag = new TAG_REFERENCE();
			unk = new uint[17];
		}

		public void Load(ref BinaryReader br)
		{
			Name = br.ReadChars(32);
			SoundTag.Load(ref br);
			for(int i=0;i<17;i++)
				unk[i] = br.ReadUInt32();
		}
	}

	public class BSP_SOUND_ENV
	{
		char[] Name;
		TAG_REFERENCE SoundTag;
		uint[] unk;
  
		public BSP_SOUND_ENV()
		{
			Name = new char[32];
			SoundTag = new TAG_REFERENCE();
			unk = new uint[8];
		}

		public void Load(ref BinaryReader br)
		{
			Name = br.ReadChars(32);
			SoundTag.Load(ref br);
			for(int i=0;i<8;i++)
				unk[i] = br.ReadUInt32();
		}
	}

	public class BSP_MARKER
	{
		uint unk;
  
		public BSP_MARKER()
		{
		}

		public void Load(ref BinaryReader br)
		{
			unk = br.ReadUInt32();
		}
	}

	public class BSP_DETAIL_OBJECT
	{
		uint[] unk;
  
		public BSP_DETAIL_OBJECT()
		{
			unk = new uint[16];
		}

		public void Load(ref BinaryReader br)
		{
			for(int i=0;i<16;i++)
				unk[i] = br.ReadUInt32();
		}
	}

	public class BSP_RUNTIME_DECAL
	{
		float[] unk;
		uint unk2;

		public BSP_RUNTIME_DECAL()
		{
			unk = new float[3];
		}

		public void Load(ref BinaryReader br)
		{
			unk[0] = br.ReadSingle();
			unk[1] = br.ReadSingle();
			unk[2] = br.ReadSingle();

			unk2 = br.ReadUInt32();
		}
	}

  
	/// <summary>
	/// Summary description for TagBsp.
	/// </summary>
	public class TagBsp : TagBase
	{
		bool LUpdate = true;
		private BSP_HEADER m_Header = new BSP_HEADER();
		private BSP_SHADER[] m_Shaders = null;
		private COLLBSP_HEADER[] collBSPheader;
		private BSP_3D_NODES Root;
		private EDGES[] edges;
		private VERTICES[] vertices;
		private BSP_LIGHTMAP[] m_Lightmaps = null;
		public short[] m_BspIndices = null;
		private BSP_CLUSTER[] m_Clusters = null;
		protected IndexBuffer IndexData = null;
		protected int TotalVertCount = 0;
		protected bool m_bBspLoaded = false;
		public BOUNDING_BOX m_BoundingBox = new BOUNDING_BOX();
		public int m_LightmapIndex = -1;
		private DEFAULT_LIGHT_BLOCK defaultLB = new DEFAULT_LIGHT_BLOCK();
		//private BSP_NODES[] VisibleNodes = null;
		private BSP_LEAF[] VisibleLeaves = null;
		private BSP_LEAF_SURFACE[] VisibleLeafSurfaces = null;

		//public CustomVertex.PositionTextured[] DebugVerts;

		public TagBsp()
		{
		}
		public void LoadTagData()
		{
			BinaryReader br = new BinaryReader(m_stream);

			//load header, shaders, Lightmaps and children (verts & indices), string for lightmaps tag
			//lower priority:  clusters, cluster data

			m_Header.Load(ref br);
			m_LightmapIndex = MdxRender.SM.LoadLightmaps(new TagFileName(m_Header.LightmapsTag.data, "bitm", this.m_PromHeader.GameVersion));

			//todo:  skip past lightmaps
			defaultLB.Load(br);

			//Load shader block
			m_Shaders = new BSP_SHADER[m_Header.Shaders.Count];
			br.BaseStream.Position = m_Header.Shaders.Offset;
			for(int s=0; s<m_Header.Shaders.Count; s++)
			{
				m_Shaders[s] = new BSP_SHADER();
				m_Shaders[s].Load(ref br);
			}
			for(int s=0; s<m_Header.Shaders.Count; s++)
			{
				char[] tmp = new char[m_Shaders[s].LightmapsTag.StringLength];
				tmp = br.ReadChars(m_Shaders[s].LightmapsTag.StringLength);
				br.BaseStream.Position += 1;
				m_Shaders[s].LightmapsTag.data = new string(tmp);
				string tag_type = new string(m_Shaders[s].LightmapsTag.tag);
				TagFileName tfn = new TagFileName(m_Shaders[s].LightmapsTag.data, tag_type, m_PromHeader.GameVersion);
				MdxRender.SM.RegisterShader(tfn);
			}

			//#region BSP Tree
			//Load Collision BSP.
			br.BaseStream.Position = this.m_Header.CollBspHeader.Offset;
			this.collBSPheader = new COLLBSP_HEADER[this.m_Header.CollBspHeader.Count];
			for(int i=0; i<collBSPheader.Length; i++)
			{
				this.collBSPheader[i] = new COLLBSP_HEADER();
				this.collBSPheader[i].Load(ref br);

				//Load 3d nodes
				br.BaseStream.Position = this.collBSPheader[i].bsp_3d_nodes.Offset;
				Root = new BSP_3D_NODES(ref br, this.collBSPheader[i].bsp_3d_nodes.Offset,
					this.collBSPheader[i].planes.Offset,
					this.m_Header.Leaves.Offset);

				//skip to edges and verts.

				//Load edges
				this.edges = new EDGES[this.collBSPheader[i].edges.Count];
				br.BaseStream.Position = this.collBSPheader[i].edges.Offset;
				for(int l=0; l<edges.Length; l++)
				{
					this.edges[l] = new EDGES();
					this.edges[l].Load(ref br);
				}

				//Load Vertices
				this.vertices = new VERTICES[this.collBSPheader[i].vertices.Count];
				br.BaseStream.Position = this.collBSPheader[i].vertices.Offset;
				for(int m=0; m<vertices.Length; m++)
				{
					this.vertices[m] = new VERTICES();
					this.vertices[m].Load(ref br);
				}
			}

			br.BaseStream.Position = m_Header.LeafSurfaces.Offset;
			VisibleLeafSurfaces = new BSP_LEAF_SURFACE[m_Header.LeafSurfaces.Count];
			for(int n=0; n<m_Header.LeafSurfaces.Count; n++)
			{
				VisibleLeafSurfaces[n] = new BSP_LEAF_SURFACE();
				VisibleLeafSurfaces[n].Load(ref br);
			}
			//#endregion

			//load triangle indices
			br.BaseStream.Position = m_Header.SubmeshTriIndices.Offset;
			m_BspIndices = new short[m_Header.SubmeshTriIndices.Count*3];
			for(int i=0; i<m_Header.SubmeshTriIndices.Count*3; i++)
				m_BspIndices[i] = br.ReadInt16();

			//Load lightmap blocks
			m_Lightmaps = new BSP_LIGHTMAP[m_Header.Lightmap.Count];
			br.BaseStream.Position = m_Header.Lightmap.Offset; 
			for(int l=0; l<m_Header.Lightmap.Count ;l++)
			{
				m_Lightmaps[l] = new BSP_LIGHTMAP();
				m_Lightmaps[l].Load(ref br);
			}

			//load lightmap raw data blocks
			for(int l=0; l<m_Header.Lightmap.Count ;l++)
			{
				Trace.WriteLine("lightmap["+l.ToString()+"]");
				m_Lightmaps[l].LoadMaterials(ref br, this.m_PromHeader.GameVersion, m_BspIndices);
        
				//register shaders
				for(int m=0; m<m_Lightmaps[l].Material.Count; m++)
				{
					TagFileName tfn = new TagFileName(m_Lightmaps[l].Materials[m].ShaderTag.data, 
						new string(m_Lightmaps[l].Materials[m].ShaderTag.tag), m_PromHeader.GameVersion);
					m_Lightmaps[l].Materials[m].MaterialMesh.RegisterShader(tfn);
          m_Lightmaps[l].Materials[m].LightmapTextureIndex = m_LightmapIndex;
          m_Lightmaps[l].Materials[m].LightmapSubTextureIndex = l;
        }
			}

			//Load clusters
			m_Clusters = new BSP_CLUSTER[m_Header.Clusters.Count];
			br.BaseStream.Position = m_Header.Clusters.Offset;
			for(int c=0; c<m_Header.Clusters.Count; c++)
			{
				m_Clusters[c] = new BSP_CLUSTER();
				m_Clusters[c].Load(ref br);
			}

			for(int c=0; c<m_Header.Clusters.Count; c++)
			{
				m_Clusters[c].LoadSubclusters(ref br);
			}

			//Calculate the total vertex buffer size needed
			TotalVertCount = 0;
			for(int l=0; l<m_Header.Lightmap.Count-1; l++)
				for(int m=0; m<m_Lightmaps[l].Material.Count; m++)
					TotalVertCount += (int)m_Lightmaps[l].Materials[m].TagVertexCount;

			//update bounding box for camera
			for(int l=0; l<m_Header.Lightmap.Count-1; l++)
				for(int m=0; m<m_Lightmaps[l].Material.Count; m++)
					for(int v=0; v<m_Lightmaps[l].Materials[m].TagVertexCount; v++)
						m_BoundingBox.Update(m_Lightmaps[l].Materials[m].MaterialMesh.BoundingBox);

			//Allocate and load the DX buffers
			IndexData = new IndexBuffer(typeof(short), m_BspIndices.Length, MdxRender.Dev, Usage.WriteOnly, Pool.Default);
			IndexData.Created += new EventHandler(this.OnIndexBufferCreate);
			OnIndexBufferCreate(IndexData, null);

			m_bBspLoaded = true;
		}

		private void OnIndexBufferCreate(object sender, EventArgs e)
		{
			IndexBuffer buffer = (IndexBuffer)sender;
			buffer.SetData(m_BspIndices, 0, LockFlags.None);
		}

		public void DrawBsp()
		{
			if(m_bBspLoaded == true)
			{
				//Bsp has one large vert & one large index buffer for all meshes
				//MdxRender.Dev.RenderState.Lighting = false;
				//m_Clusters[1].RenderSubclusters(0);

				//MdxRender.Dev.RenderState.FillMode = FillMode.WireFrame;
				MdxRender.Dev.Indices = this.IndexData;
				MdxRender.Dev.VertexFormat = PositionTexture2.Format;

        for(int l=0; l<m_Header.Lightmap.Count; l++)
          for(int m=0; m<m_Lightmaps[l].Material.Count; m++)
            m_Lightmaps[l].Materials[m].DrawMaterial();
				//MdxRender.Dev.RenderState.FillMode = FillMode.Solid;
			}
		}

		public void DrawWireframeBsp()
		{
			if(m_bBspLoaded == true)
			{
				TagSenv BspShader = null;
				MdxRender.Dev.Indices = this.IndexData;
				MdxRender.Dev.VertexFormat = PositionTexture2.Format;
				MdxRender.Dev.RenderState.FillMode = FillMode.WireFrame;

				for(int l=0; l<m_Header.Lightmap.Count; l++)
				{
					for(int m=0; m<m_Lightmaps[l].Material.Count; m++)
					{

						BspShader = MdxRender.SM.GetBspShader(m_Lightmaps[l].Materials[m].ShaderManagerIndex);
            
						if(BspShader != null)
						{
							if(m_LightmapIndex != -1)
							{
								if(m_Lightmaps[l].LightmapIndex != -1)
								{
									//MdxRender.SM.ConfigureLightmapForBlend(this.m_LightmapIndex, l);
									//BspShader.ActivateShaderWithLightmap();
									m_Lightmaps[l].Materials[m].DrawMaterial();
								}
							}
						}
					}
				}
			}
		}

		public int GetLightMapCount
		{
			get{ return (int)this.m_Header.Lightmap.Count; }
		}

		public void UpdateRadiosityPreview()
		{
			MdxRender.SM.m_TextureManager.PreviewRadiosity(m_LightmapIndex, OptionsManager.LightmapOutputPath);
		}

		public BSP_LIGHTMAP GetLightMap(int ix)
		{
			return this.m_Lightmaps[ix];
		}

		public DEFAULT_LIGHT_BLOCK GetDefaultData
		{
			get{ return this.defaultLB; }
		}

		public void ProcessDeviceReset(ref Device dev)
		{
		}

		public void ProcessDeviceLost()
		{
		}

    public void ApplyLightmapColor(Vector3 Origin, Vector3 Direction, Color color)
    {
		#region hide
		/*
		float[] UV = new float[2];
		Vector3 point = new Vector3();
		short LIx;
		float t;

		Prometheus.Core.Lightmap.Render render= new Prometheus.Core.Lightmap.Render(); 
		Prometheus.Core.Lightmap.Scene scene= new Scene(MdxRender.GetCurrentBsp); 

		Vector3 tmpPos = new Vector3(60.0f,-120.0f,20.0f);
		//scene.AddLight(new LightSource(tmpPos ,1.0,new float[3]{255,255,255}), 
		//	LightSource.Type.Diffuse);

		//render.SetScene(scene);

		if(this.LUpdate)
		{
		//	Prometheus.Core.Lightmap.Render.QuickSave();
		//	MdxRender.GetCurrentBsp.UpdateRadiosityPreview();
		//	this.LUpdate = false;
		}


		for(int i=0; i< this.GetLightMapCount; i++)
		{
			if(this.m_Lightmaps[i].RayAABBTest(Origin,Direction,out point,out UV,out LIx, out t))
			{
				if(LIx != -1)
				{
					Prometheus.Core.Lightmap.Render.QuickSetTexel(LIx,UV,color);
          MdxRender.SM.m_TextureManager.PaintLightmapTexel(m_LightmapIndex, LIx, UV);
					//MdxRender.SM.m_TextureManager.UpdateLightmap(m_LightmapIndex, LIx, OptionsManager.LightmapOutputPath + 
					//								string.Format("\\lightmap_{0}.bmp",LIx));
				}
				break;
			}
		}*/
		#endregion

		DateTime st = DateTime.Now; 
		bool t = this.Root.RayIntersect(Origin, Direction);
		DateTime ft = DateTime.Now;
		TimeSpan rs = ft-st;
		Trace.WriteLine(string.Format("Result:{0} Time Taken:{1} milliseconds",t,rs.Milliseconds));

		st = DateTime.Now;
		float[] UV; int LIx;
      if(this.GetBSPIntersect(Origin, Direction, out UV, out LIx))
      {
        MdxRender.SM.m_TextureManager.PaintLightmapTexel(MdxRender.GetCurrentBsp.m_LightmapIndex, LIx, UV);
        Trace.WriteLine("found");
      }
		ft = DateTime.Now;
		rs = ft-st;
		Trace.WriteLine(string.Format("UV ={0} LIx ={1} Time Taken: {2}", UV, LIx, rs.Milliseconds));
    }

    static public CustomVertex.PositionColored[] nearestNeighborVerts;

    static public void Debug_RenderNearestNeighborVerts()
    {
      if(nearestNeighborVerts != null)
      {
        MdxRender.Dev.RenderState.AmbientMaterialSource = ColorSource.Color1;
        MdxRender.Dev.DrawUserPrimitives(PrimitiveType.PointList, 4, nearestNeighborVerts);
      }
    }

		public bool GetBSPIntersect(Vector3 Origin, Vector3 Direction, out float[] UV, out int LIx)
		{
			bool result = false;
			float[] tUV;
			float distance = 0xffffffffff; //set really big so that distance is always smaller.
			float tdistance;
			UV = new float[2]{-1,-1};
			LIx = -2;
			for(int i = 0; i< this.m_Header.Lightmap.Count; i++)
				for(int j = 0; j< this.m_Lightmaps[i].Material.Count; j++)
				{
					if(m_Lightmaps[i].Materials[j].TextureColor(Origin, Direction, out tdistance, out tUV))
					{
						//result = true;
						//if(tdistance < distance)
						//{
							UV = tUV;
							LIx = i;
						//}
						result = true;
					}
				}
			return result;
		}
  }
}
