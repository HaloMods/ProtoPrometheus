using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.IO;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Prometheus.Core.Tags;
using Prometheus.Core.Lightmap;

namespace Prometheus.Core.Render
{
  public enum eLOD
  {
    SUPER_LOW = 0,
    LOW = 1,
    MEDIUM = 2,
    HIGH = 3,
    SUPER_HIGH = 4,
    CAUSES_FIRES = 5,
    UNKNOWN = 6
  };

  public class ModelNode
  {
    public short NextSiblingNode;
    public short NextChildNode;
    public short ParentNode;
    public float[] Translation = new float[3];
    public float[] Rotation = new float[4];
  }
  public class NodeTransform
  {
    public Prometheus.Core.Util.Matrix m_absolute = new Prometheus.Core.Util.Matrix();
    public Prometheus.Core.Util.Matrix m_relative = new Prometheus.Core.Util.Matrix();
//    public Microsoft.DirectX.Matrix m_absolute = new Microsoft.DirectX.Matrix();
//    public Microsoft.DirectX.Matrix m_relative = new Microsoft.DirectX.Matrix();
//    public Microsoft.DirectX.Matrix m_AbsoluteInverse = new Microsoft.DirectX.Matrix();
    public float[] m_FinalNode = new float[3];
    public NodeTransform()
    {
//      m_absolute = Matrix.Identity;
//      m_relative = Matrix.Identity;
//      m_AbsoluteInverse = Matrix.Identity;
    }
  }
  public class Permutation
  {
    public string Name;
    public short [] LodMeshIndex = new short[6];
  }

  public class Region
  {
    public string Name;
    public Permutation [] Permutations;
  }

	/// <summary>
	/// Summary description for Model.
	/// </summary>
	public class Model3D : Object3D
	{
    public EnhancedMesh [/*geometries*/][/*meshes*/] m_MeshList;
    private Mesh utilityModel = null;
    public Region [] m_Regions;
    public BOUNDING_BOX m_BoundingBox;
    private float m_BoundingRadius = -1;
    private int m_ActiveTriangleCount = 0;
    private eLOD m_ActiveLOD = eLOD.SUPER_HIGH;
    private eLOD m_PreviousLOD = eLOD.UNKNOWN;

    #region Animation Vars
    public ModelNode[] m_Nodes;
    public NodeTransform[] NodeTransforms;
    #endregion


    private static readonly short[] m_BoxIndices = {
                                                     0,1,2,   // Back Face
                                                     3,4,5,   // Back Face
                                                     6,7,8,   // Right Face
                                                     9,10,11, // Right Face
                                                     12,13,14, // Left Face
                                                     15,16,17, // Left Face
                                                     18,19,20, // Front Face
                                                     21,22,23, // Front Face
                                                     24,25,26, // Top Face
                                                     27,28,29, // Top Face
                                                     30,31,32, // Bottom Face
                                                     33,34,35  // Bottom Face
                                                   };

    public Model3D(string name) : base (name)
		{
		}
    public void LoadResourceModel(string ResourcePath)
    {
      Stream st = Assembly.GetExecutingAssembly().GetManifestResourceStream(ResourcePath);
      utilityModel = Mesh.FromStream(st, MeshFlags.Managed, MdxRender.Dev);  

      Matrix pitch = new Matrix();
      Matrix yaw = new Matrix();
      yaw.RotateZ((float)Math.PI);
      pitch.RotateX((float)Math.PI/2);
      SelectTool.TransformMesh(utilityModel, Matrix.Multiply(pitch, yaw));
    }
    public void LoadArrow(string ResourcePath)
    {
      Stream st = Assembly.GetExecutingAssembly().GetManifestResourceStream(ResourcePath);
      utilityModel = Mesh.FromStream(st, MeshFlags.Managed, MdxRender.Dev);  

      Matrix scale = new Matrix();
      scale.Scale(0.2f, 0.2f, 0.2f);
      Matrix pitch = new Matrix();
      Matrix yaw = new Matrix();
      yaw.RotateZ((float)Math.PI);
      //pitch.RotateX((float)Math.PI/2);
      SelectTool.TransformMesh(utilityModel, scale);
    }
    public int TriangleCount
    {
      get{return m_ActiveTriangleCount;}
    }
	  /// <summary>
	  /// When overrideen in a derived class, returns the bounding radius of the object.
	  /// </summary>
	  public override float BoundingRadius
	  {
	    get
      {
        if((m_BoundingRadius < 0)&&(utilityModel == null))
        {
          //calculate bounding radius
          float br;
          for(int i=0; i<m_MeshList.Length; i++)
          {
            for(int j=0; j<m_MeshList[i].Length; j++)
            {
              br = m_MeshList[i][j].GetBoundingRadius();
              if(br > m_BoundingRadius)
                m_BoundingRadius = br;
            }
          }
        }

        return m_BoundingRadius;
      }
	  }

    public eLOD LevelOfDetail
    {
      get{return m_ActiveLOD;}
      set
      {
        m_ActiveLOD = value;

        if(m_ActiveLOD != m_PreviousLOD)
          UpdateTriangleCount();

        m_PreviousLOD = m_ActiveLOD;
      }
    }

    public void UpdateTriangleCount()
    {
      m_ActiveTriangleCount = 0;
      if(utilityModel == null)
      {
        int geo_index = 0;
        int perm_index = 0;

        for(int region=0; region<m_Regions.Length; region++)
        {
          geo_index = m_Regions[region].Permutations[perm_index].LodMeshIndex[(int)m_ActiveLOD];
          for(int m=0; m<m_MeshList[geo_index].Length; m++)
            m_ActiveTriangleCount += m_MeshList[geo_index][m].TriangleCount;
        }
      }
    }
    public override void Render()
    {
      if(utilityModel != null)
      {
        MdxRender.Dev.RenderState.AlphaTestEnable = false;
        MdxRender.SM.ConfigureForDiffuseColor();
        utilityModel.DrawSubset(0);
      }
      else
      {
        int geo_index = 0;
        int perm_index = 0;

        for(int region=0; region<m_Regions.Length; region++)
        {
          geo_index = m_Regions[region].Permutations[perm_index].LodMeshIndex[(int)m_ActiveLOD];

          for(int m=0; m<m_MeshList[geo_index].Length; m++)
            m_MeshList[geo_index][m].RenderMesh();
        }
      }
    }
		public EnhancedMesh[][] GetMeshListLOD()
		{
			EnhancedMesh[][] result;
			int geo_index =0;
			int perm_index =0;

			result = new EnhancedMesh[m_Regions.Length][];
			for(int region=0; region<m_Regions.Length; region++)
			{
				geo_index = m_Regions[region].Permutations[perm_index].LodMeshIndex[(int)m_ActiveLOD];

				result[geo_index] = new EnhancedMesh[m_MeshList[geo_index].Length];
				for(int i=0; i<m_MeshList[geo_index].Length; i++)
				{
					result[geo_index][i] = m_MeshList[geo_index][i];
				}
			}
			return result;
		}
    public void LoadDirectionModel()
    {
      //create a cube model
      m_MeshList = new EnhancedMesh[1][];
      m_MeshList[0] = new EnhancedMesh[6];

      short[] ind = new short[6];
      for(short i=0; i<6; i++)ind[i] = i;

      for(int i=0; i<6; i++)
      {
        m_MeshList[0][i] = new EnhancedMesh(2);
        m_MeshList[0][i].AllocateVertexBuffer(MeshFormat.Model, 6);
        m_MeshList[0][i].AllocateAndSetIndexBuffer(PrimitiveType.TriangleList, ind, 0, ind.Length);
      }

      //back face (0,1,2) (3,4,5)
      m_MeshList[0][0].RegisterUtilityShader(UtilityShader.NX);
      m_MeshList[0][0].LoadVertex(0, -1.0f, -1.0f,  1.0f, -1, 0, 0, 0, 1);
      m_MeshList[0][0].LoadVertex(1, -1.0f,  1.0f,  1.0f, -1, 0, 0, 1, 1);
      m_MeshList[0][0].LoadVertex(2, -1.0f, -1.0f, -1.0f, -1, 0, 0, 0, 0);
      m_MeshList[0][0].LoadVertex(3, -1.0f,  1.0f,  1.0f, -1, 0, 0, 1, 1);
      m_MeshList[0][0].LoadVertex(4, -1.0f,  1.0f, -1.0f, -1, 0, 0, 1, 0);
      m_MeshList[0][0].LoadVertex(5, -1.0f, -1.0f, -1.0f, -1, 0, 0, 0, 0);
      
      //right face (6,7,8) (9,10,11)
      m_MeshList[0][1].RegisterUtilityShader(UtilityShader.PY);
      m_MeshList[0][1].LoadVertex(0, -1.0f,  1.0f,  1.0f,  0, 1, 0, 0, 1);
      m_MeshList[0][1].LoadVertex(1,  1.0f,  1.0f,  1.0f,  0, 1, 0, 1, 1);
      m_MeshList[0][1].LoadVertex(2, -1.0f,  1.0f, -1.0f,  0, 1, 0, 0, 0);
      m_MeshList[0][1].LoadVertex(3,  1.0f,  1.0f,  1.0f,  0, 1, 0, 1, 1);
      m_MeshList[0][1].LoadVertex(4,  1.0f,  1.0f, -1.0f,  0, 1, 0, 1, 0);
      m_MeshList[0][1].LoadVertex(5, -1.0f,  1.0f, -1.0f,  0, 1, 0, 0, 0);

      //left face (12,13,14) (15,16,17)
      m_MeshList[0][2].RegisterUtilityShader(UtilityShader.NY);
      m_MeshList[0][2].LoadVertex(0,  1.0f, -1.0f,  1.0f,  0, -1, 0, 0, 1);
      m_MeshList[0][2].LoadVertex(1, -1.0f, -1.0f,  1.0f,  0, -1, 0, 1, 1);
      m_MeshList[0][2].LoadVertex(2,  1.0f, -1.0f, -1.0f,  0, -1, 0, 0, 0);
      m_MeshList[0][2].LoadVertex(3, -1.0f, -1.0f,  1.0f,  0, -1, 0, 1, 1);
      m_MeshList[0][2].LoadVertex(4, -1.0f, -1.0f, -1.0f,  0, -1, 0, 1, 0);
      m_MeshList[0][2].LoadVertex(5,  1.0f, -1.0f, -1.0f,  0, -1, 0, 0, 0);

      //front face (18,19,20) (21,22,23)
      m_MeshList[0][3].RegisterUtilityShader(UtilityShader.PX);
      m_MeshList[0][3].LoadVertex(0,  1.0f,  1.0f,  1.0f,  1, 0, 0, 0, 1);
      m_MeshList[0][3].LoadVertex(1,  1.0f, -1.0f,  1.0f,  1, 0, 0, 1, 1);
      m_MeshList[0][3].LoadVertex(2,  1.0f,  1.0f, -1.0f,  1, 0, 0, 0, 0);
      m_MeshList[0][3].LoadVertex(3,  1.0f, -1.0f,  1.0f,  1, 0, 0, 1, 1);
      m_MeshList[0][3].LoadVertex(4,  1.0f, -1.0f, -1.0f,  1, 0, 0, 1, 0);
      m_MeshList[0][3].LoadVertex(5,  1.0f,  1.0f, -1.0f,  1, 0, 0, 0, 0);

      //top face (24,25,26) (27,28,29)
      m_MeshList[0][4].RegisterUtilityShader(UtilityShader.PZ);
      m_MeshList[0][4].LoadVertex(0,  1.0f,  1.0f,  1.0f,  0, 0, 1, 0, 1);
      m_MeshList[0][4].LoadVertex(1, -1.0f,  1.0f,  1.0f,  0, 0, 1, 1, 1);
      m_MeshList[0][4].LoadVertex(2,  1.0f, -1.0f,  1.0f,  0, 0, 1, 0, 0);
      m_MeshList[0][4].LoadVertex(3, -1.0f,  1.0f,  1.0f,  0, 0, 1, 1, 1);
      m_MeshList[0][4].LoadVertex(4, -1.0f, -1.0f,  1.0f,  0, 0, 1, 1, 0);
      m_MeshList[0][4].LoadVertex(5,  1.0f, -1.0f,  1.0f,  0, 0, 1, 0, 0);

      //bottom face (30,31,32) (33,34,35)
      m_MeshList[0][5].RegisterUtilityShader(UtilityShader.NZ);
      m_MeshList[0][5].LoadVertex(0, -1.0f,  1.0f, -1.0f,  0, 0, -1, 0, 1);
      m_MeshList[0][5].LoadVertex(1,  1.0f,  1.0f, -1.0f,  0, 0, -1, 1, 1);
      m_MeshList[0][5].LoadVertex(2, -1.0f, -1.0f, -1.0f,  0, 0, -1, 0, 0);
      m_MeshList[0][5].LoadVertex(3,  1.0f,  1.0f, -1.0f,  0, 0, -1, 1, 1);
      m_MeshList[0][5].LoadVertex(4,  1.0f, -1.0f, -1.0f,  0, 0, -1, 1, 0);
      m_MeshList[0][5].LoadVertex(5, -1.0f, -1.0f, -1.0f,  0, 0, -1, 0, 0);

      for(int i=0; i<6; i++)
        m_MeshList[0][i].UpdateDxBuffer();
      
      m_Regions = new Region[1];
      m_Regions[0] = new Region();
      m_Regions[0].Name = "default cube";
      m_Regions[0].Permutations = new Permutation[1];
      m_Regions[0].Permutations[0] = new Permutation();
      m_Regions[0].Permutations[0].LodMeshIndex[0] = 0;
      m_Regions[0].Permutations[0].LodMeshIndex[1] = 0;
      m_Regions[0].Permutations[0].LodMeshIndex[2] = 0;
      m_Regions[0].Permutations[0].LodMeshIndex[3] = 0;
      m_Regions[0].Permutations[0].LodMeshIndex[4] = 0;
      m_Regions[0].Permutations[0].LodMeshIndex[5] = 0;
      m_Regions[0].Permutations[0].Name = "default";
    }
    public void LoadDefaultModel(UtilityShader skin)
    {
      float scale = 0.1f;
      //create a cube model
      m_MeshList = new EnhancedMesh[1][];
      m_MeshList[0] = new EnhancedMesh[1];
      m_MeshList[0][0] = new EnhancedMesh(12);

      m_MeshList[0][0].AllocateVertexBuffer(MeshFormat.Model, 36);
      
      //back face (0,1,2) (3,4,5)
      m_MeshList[0][0].RegisterUtilityShader(skin);
      m_MeshList[0][0].LoadVertex(0, -scale, -scale,  scale, -1, 0, 0, 0, 1);
      m_MeshList[0][0].LoadVertex(1, -scale,  scale,  scale, -1, 0, 0, 1, 1);
      m_MeshList[0][0].LoadVertex(2, -scale, -scale, -scale, -1, 0, 0, 0, 0);
      m_MeshList[0][0].LoadVertex(3, -scale,  scale,  scale, -1, 0, 0, 1, 1);
      m_MeshList[0][0].LoadVertex(4, -scale,  scale, -scale, -1, 0, 0, 1, 0);
      m_MeshList[0][0].LoadVertex(5, -scale, -scale, -scale, -1, 0, 0, 0, 0);
      
      //right face (6,7,8) (9,10,11)
      m_MeshList[0][0].LoadVertex( 6, -scale,  scale,  scale,  0, 1, 0, 0, 1);
      m_MeshList[0][0].LoadVertex( 7,  scale,  scale,  scale,  0, 1, 0, 1, 1);
      m_MeshList[0][0].LoadVertex( 8, -scale,  scale, -scale,  0, 1, 0, 0, 0);
      m_MeshList[0][0].LoadVertex( 9,  scale,  scale,  scale,  0, 1, 0, 1, 1);
      m_MeshList[0][0].LoadVertex(10,  scale,  scale, -scale,  0, 1, 0, 1, 0);
      m_MeshList[0][0].LoadVertex(11, -scale,  scale, -scale,  0, 1, 0, 0, 0);

      //left face (12,13,14) (15,16,17)
      m_MeshList[0][0].LoadVertex(12,  scale, -scale,  scale,  0, -1, 0, 0, 1);
      m_MeshList[0][0].LoadVertex(13, -scale, -scale,  scale,  0, -1, 0, 1, 1);
      m_MeshList[0][0].LoadVertex(14,  scale, -scale, -scale,  0, -1, 0, 0, 0);
      m_MeshList[0][0].LoadVertex(15, -scale, -scale,  scale,  0, -1, 0, 1, 1);
      m_MeshList[0][0].LoadVertex(16, -scale, -scale, -scale,  0, -1, 0, 1, 0);
      m_MeshList[0][0].LoadVertex(17,  scale, -scale, -scale,  0, -1, 0, 0, 0);

      //front face (18,19,20) (21,22,23)
      m_MeshList[0][0].LoadVertex(18,  scale,  scale,  scale,  1, 0, 0, 0, 1);
      m_MeshList[0][0].LoadVertex(19,  scale, -scale,  scale,  1, 0, 0, 1, 1);
      m_MeshList[0][0].LoadVertex(20,  scale,  scale, -scale,  1, 0, 0, 0, 0);
      m_MeshList[0][0].LoadVertex(21,  scale, -scale,  scale,  1, 0, 0, 1, 1);
      m_MeshList[0][0].LoadVertex(22,  scale, -scale, -scale,  1, 0, 0, 1, 0);
      m_MeshList[0][0].LoadVertex(23,  scale,  scale, -scale,  1, 0, 0, 0, 0);

      //top face (24,25,26) (27,28,29)
      m_MeshList[0][0].LoadVertex(24,  scale,  scale,  scale,  0, 0, 1, 0, 1);
      m_MeshList[0][0].LoadVertex(25, -scale,  scale,  scale,  0, 0, 1, 1, 1);
      m_MeshList[0][0].LoadVertex(26,  scale, -scale,  scale,  0, 0, 1, 0, 0);
      m_MeshList[0][0].LoadVertex(27, -scale,  scale,  scale,  0, 0, 1, 1, 1);
      m_MeshList[0][0].LoadVertex(28, -scale, -scale,  scale,  0, 0, 1, 1, 0);
      m_MeshList[0][0].LoadVertex(29,  scale, -scale,  scale,  0, 0, 1, 0, 0);

      //bottom face (30,31,32) (33,34,35)
      m_MeshList[0][0].LoadVertex(30, -scale,  scale, -scale,  0, 0, -1, 0, 1);
      m_MeshList[0][0].LoadVertex(31,  scale,  scale, -scale,  0, 0, -1, 1, 1);
      m_MeshList[0][0].LoadVertex(32, -scale, -scale, -scale,  0, 0, -1, 0, 0);
      m_MeshList[0][0].LoadVertex(33,  scale,  scale, -scale,  0, 0, -1, 1, 1);
      m_MeshList[0][0].LoadVertex(34,  scale, -scale, -scale,  0, 0, -1, 1, 0);
      m_MeshList[0][0].LoadVertex(35, -scale, -scale, -scale,  0, 0, -1, 0, 0);

      m_MeshList[0][0].UpdateDxBuffer();
      
      m_MeshList[0][0].AllocateAndSetIndexBuffer(PrimitiveType.TriangleList, 
        Model3D.m_BoxIndices, 0, Model3D.m_BoxIndices.Length);
      m_Regions = new Region[1];
      m_Regions[0] = new Region();
      m_Regions[0].Name = "default cube";
      m_Regions[0].Permutations = new Permutation[1];
      m_Regions[0].Permutations[0] = new Permutation();
      m_Regions[0].Permutations[0].LodMeshIndex[0] = 0;
      m_Regions[0].Permutations[0].LodMeshIndex[1] = 0;
      m_Regions[0].Permutations[0].LodMeshIndex[2] = 0;
      m_Regions[0].Permutations[0].LodMeshIndex[3] = 0;
      m_Regions[0].Permutations[0].LodMeshIndex[4] = 0;
      m_Regions[0].Permutations[0].LodMeshIndex[5] = 0;
      m_Regions[0].Permutations[0].Name = "default";
    }

    public bool TestRaySphereIntersect(Vector3 rayOrigin, Vector3 rayDirection)
    {
      //taken from Dunn/Parberry page 287
      bool bIntersected = true;

      rayOrigin.Multiply(-1.0f);
      float a = Vector3.Dot(rayOrigin, rayDirection);

      float arg = BoundingRadius*BoundingRadius - Vector3.Dot(rayOrigin, rayOrigin) + a*a;

      if(arg < 0)
        bIntersected = false;

      //Trace.WriteLine("intersect sphere = " + bIntersected.ToString());
      return(bIntersected);
    }

		public bool RayAABBInterset(Vector3 Origin, Vector3 Direction)
		{
			if(Utility.RayAABBIntersect(Origin, Direction, this.m_BoundingBox))
				return true;
			else
				return false;
		}

    static public void DrawRotationMatrix(Matrix mat)
    {
      float scale = 2.0f;
      CustomVertex.PositionColored[] vertices = new CustomVertex.PositionColored[6];

      //x-axis
      vertices[0].Position = new Vector3(0,0,0);
      vertices[0].Color = Color.Red.ToArgb();    
      vertices[1].Position = new Vector3(mat.M11*scale, mat.M21*scale, mat.M31*scale);
      vertices[1].Color = Color.Red.ToArgb();

      //y-axis
      vertices[2].Position = new Vector3(0,0,0);
      vertices[2].Color = Color.Blue.ToArgb();    
      vertices[3].Position = new Vector3(mat.M12*scale, mat.M22*scale, mat.M32*scale);
      vertices[3].Color = Color.Blue.ToArgb();

      //z-axis
      vertices[4].Position = new Vector3(0,0,0);
      vertices[4].Color = Color.Green.ToArgb();    
      vertices[5].Position = new Vector3(mat.M13*scale, mat.M23*scale, mat.M33*scale);
      vertices[5].Color = Color.Green.ToArgb();

      MdxRender.Dev.VertexFormat = CustomVertex.PositionColored.Format;
      MdxRender.Dev.DrawUserPrimitives(PrimitiveType.LineList, 3, vertices);
    }

    public bool TestMeshIntersect(Vector3 rayOrigin, Vector3 rayDirection)
    {
      bool bIntersect = false;

      if(utilityModel != null)
      {
        bIntersect = utilityModel.IntersectSubset(0, rayOrigin, rayDirection);
      }
      else
      {
        int geo_index = 0;
        int perm_index = 0;

        for(int region=0; region<m_Regions.Length; region++)
        {
          geo_index = m_Regions[region].Permutations[perm_index].LodMeshIndex[(int)m_ActiveLOD];

          for(int m=0; m<m_MeshList[geo_index].Length; m++)
          {
            bIntersect = m_MeshList[geo_index][m].IntersectTest(rayOrigin, rayDirection);

            if(bIntersect)
              return(bIntersect);
          }
        }
      }

      return(bIntersect);
    }

  }
}
