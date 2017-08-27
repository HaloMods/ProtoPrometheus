using System;
using System.Diagnostics;
using System.Collections;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Prometheus.Core.Tags;
using Prometheus.Core.Tags.Antr;
using Prometheus.Core.Project;
using TagLibrary.Types;

namespace Prometheus.Core.Render
{
  public enum BillboardModel{CTF_Flag,CTF_Vehicle,Oddball,Race_Track, Race_Vehicle, Vegas_Bank, Teleport_From, Teleport_To, KOH, Sound_Scenery};
  /// <summary>
  /// Represents an instance of an Object3D.
  /// </summary>
  public class Instance3D
  {
    private ObjectType objectType = ObjectType.Unknown;
    public string objectName;
    
    private Vector3 m_Translation;
    private Attitude m_Rotation;
    private SelectTool m_SelectTool;

    private Angle m_BoundYaw;
    private RealEulerAngles3D m_BoundRotation;
    private RealPoint3D m_BoundTranslation;
    
    private bool m_bEditActive = false;
    private bool bBillboardMode = false;
    private bool bDecalMode = false;

    private static int billboardCount = 0;
    private Object3D instanceOf;
    protected Instance3D parent; 
    protected Instance3DCollection children = new Instance3DCollection();
    //protected string name;
    //                                          
    private Model3D m_Model;
    private Matrix m_Matrix = new Matrix();
    private Matrix m_BillboardMatrix;
    private bool m_bCulled = false;

    private bool m_bAnimationEnabled = false;
    private bool selectionLeader = false;
    private ModelNode[] m_AniNodes;
    private static Mesh nodeSphere = null;
    private CustomVertex.PositionOnly[] nodeLocations = null;
    private Material objectColor = new Material();
    private static Material fullBrightColor = new Material();
    private Billboard billboard;
    private PromDecal decal;
    private bool playerSpawn = false;


    public Color ObjectColor
    {
      set
      {
        if(this.playerSpawn == false)
        {
          objectColor.Ambient = MdxRender.Dev.RenderState.FogColor;
          objectColor.Diffuse = value;
        }
      }
    }
    public bool PlayerSpawn
    {
      set{playerSpawn = value;}
    }
    public Vector3 Translation
    {
      get{return m_Translation;}
    }
    public ObjectType ObjectType
    {
      get{return objectType;}
      set{objectType = value;}
    }
    public string ObjectName
    {
      get{return objectName;}
      set
      {
        //todo:  remove rest of path
        objectName = value;
      }
    }
    public Model3D Model
    {
      get{return m_Model;}
    }
    public float ZPlane
    {
      get{return m_Translation.Z;}
    }
    public EditMode EditMode
    {
      get{return m_SelectTool.m_Mode;}
      set{m_SelectTool.m_Mode = value;}
    }
    /// <summary>
    /// Returns true if the unit is an active selection.
    /// </summary>
    public bool Selected
    {
      get{return (m_SelectTool.m_Mode != EditMode.NotSelected);}
      set
      {
        if(value == true)
          m_SelectTool.m_Mode = EditMode.Selected;
        else
          m_SelectTool.m_Mode = EditMode.NotSelected;
      }
    }
    public bool SelectionLeader
    {
      get{return selectionLeader;}
      set{selectionLeader = value;}
    }
    /// <summary>
    /// Constructor.  Requires a model filename to initialize properly. 
    /// </summary>
    /// <param name="tfn"></param>
    public Instance3D(TagFileName tfn)
    {
      m_SelectTool = new SelectTool();
      SetModel(tfn);
      UpdateTransform();
      objectColor.Ambient = Color.Gray;
      objectColor.Diffuse = Color.White;
      fullBrightColor.Ambient = Color.White;
      fullBrightColor.Diffuse = Color.White;
    }

    public Instance3D(Model3D model)
    {
      m_SelectTool = new SelectTool();
      m_Model = model;
      UpdateTransform();
      objectColor.Ambient = Color.Gray;
      objectColor.Diffuse = Color.White;
      fullBrightColor.Ambient = Color.White;
      fullBrightColor.Diffuse = Color.White;
    }
    public Instance3D(BillboardModel bb)
    {
      int texture_index = MdxRender.SM.m_TextureManager.GetBillboardTextureIndex(bb);
      m_SelectTool = new SelectTool();
      m_Model = MdxRender.MM.ArrowModel;
      m_BillboardMatrix = new Matrix();
      billboard = new Billboard(0.5f, 0.5f, texture_index);
      UpdateTransform();
      objectColor.Ambient = Color.Gray;
      objectColor.Diffuse = Color.White;
      fullBrightColor.Ambient = Color.White;
      fullBrightColor.Diffuse = Color.White;
      bBillboardMode = true;
      billboardCount++;
    }
    public Instance3D(PromDecal promdecal)
    {
      m_SelectTool = new SelectTool();
      UpdateTransform();
      this.decal = promdecal;
      objectColor.Ambient = Color.Gray;
      objectColor.Diffuse = Color.White;
      fullBrightColor.Ambient = Color.White;
      fullBrightColor.Diffuse = Color.White;
      bDecalMode = true;
    }
    public void InitializeTranslationBinding(RealPoint3D translation)
    {
      m_BoundTranslation = translation;
      m_Translation.X = translation.X;
      m_Translation.Y = translation.Y;
      m_Translation.Z = translation.Z;
      translation.XChanged += new EventHandler(translationChanged);
      translation.YChanged += new EventHandler(translationChanged);
      translation.ZChanged += new EventHandler(translationChanged);
      UpdateTransform();
    }

    private void translationChanged(object sender, EventArgs e)
    {
//      if (m_BoundTranslation.X != m_Translation.X) m_Translation.X = m_BoundTranslation.X;
//      if (m_BoundTranslation.Y != m_Translation.Y) m_Translation.Y = m_BoundTranslation.Y;
//      if (m_BoundTranslation.Z != m_Translation.Z) m_Translation.Z = m_BoundTranslation.Z;
    }

    public void InitializeRotationBinding(RealEulerAngles3D angles)
    {
      m_BoundRotation = angles;
      m_Rotation.Roll = angles.R;
      m_Rotation.Pitch = angles.P;
      m_Rotation.Yaw = angles.Y;
      UpdateTransform();
    }
    public void InitializeRotationBinding(Angle yaw)
    {
      m_BoundYaw = yaw;
      m_Rotation.Roll = 0;
      m_Rotation.Pitch = 0;
      m_Rotation.Yaw = yaw.Value;
      UpdateTransform();
    }
    public void InitializeRotationBinding(CharInteger yaw, CharInteger pitch)
    {
      sbyte syaw = (sbyte)yaw.Value;
      sbyte spitch = (sbyte)pitch.Value;
      float degtorad = (float)Math.PI/180f;
      float degfactor = 2*0.70555f;

      //m_BoundYaw = yaw;
      m_Rotation.Roll = 0;//spitch*degfactor*degtorad;
      m_Rotation.Pitch = syaw*degfactor*degtorad;
      m_Rotation.Yaw = 0;//syaw*degfactor*degtorad;
      UpdateTransform();
      Trace.WriteLine(string.Format("yaw={0}  pitch={1}", syaw, spitch));
    }
    /// <summary>
    /// Called during initialization and when the gui changes rotation fields.
    /// </summary>
    /// <param name="roll"></param>
    /// <param name="pitch"></param>
    /// <param name="yaw"></param>
    public void SetRotation(float roll, float pitch, float yaw)
    {
      //m_Rotation.Roll = roll;
      //m_Rotation.Pitch = pitch;
      //m_Rotation.Yaw = yaw;
      m_Rotation.Roll = roll;
      m_Rotation.Pitch = pitch;
      m_Rotation.Yaw = yaw;
      UpdateTransform();
    }
    /// <summary>
    /// Called during initialization and when the gui changes translate fields.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public void SetTranslation(float x, float y, float z)
    {
      m_Translation.X = x;
      m_Translation.Y = y;
      m_Translation.Z = z;
      UpdateTransform();
    }
    /// <summary>
    /// Increments/decrements the rotation.  Called when item is moved with
    /// the mouse in the render window.
    /// </summary>
    /// <param name="droll"></param>
    /// <param name="dpitch"></param>
    /// <param name="dyaw"></param>
    public void IncrementRotation(float droll, float dpitch, float dyaw)
    {
      m_Rotation.Roll -= droll;
      m_Rotation.Pitch += dpitch;
      m_Rotation.Yaw += dyaw;
      UpdateTransform();
    }
    /// <summary>
    /// Increments/decrements the translation.  Called when item is moved with
    /// the mouse in the render window.
    /// </summary>
    /// <param name="dx"></param>
    /// <param name="dy"></param>
    /// <param name="dz"></param>
    public void IncrementTranslation(Vector3 delta)
    {
      m_Translation += delta;
      UpdateTransform();
    }
    /// <summary>
    /// Called to update the transformation matrix following any adjustment
    /// to the rotation or translation.
    /// </summary>
    private void UpdateTransform()
    {
      Matrix rot1 = new Matrix();
      Matrix rot2 = new Matrix();
      Matrix rot3 = new Matrix();
      Matrix trans = new Matrix();
      rot1 = Matrix.Identity;
      rot2 = Matrix.Identity;
      rot3 = Matrix.Identity;

      m_Matrix = Matrix.Identity;
      rot1.RotateX(m_Rotation.Pitch);
      rot2.RotateY(-m_Rotation.Roll);//TODO: figure out if actual game rotation is negative
      rot3.RotateZ(m_Rotation.Yaw); //perfect
      trans.Translate(m_Translation.X, m_Translation.Y, m_Translation.Z);
      m_Matrix.Multiply(rot3);
      m_Matrix.Multiply(rot2);
      m_Matrix.Multiply(rot1);
      m_Matrix.Multiply(trans);

      if(m_BoundTranslation != null)
      {
        m_BoundTranslation.X = m_Translation.X;
        m_BoundTranslation.Y = m_Translation.Y;
        m_BoundTranslation.Z = m_Translation.Z;
      }
      if(m_BoundRotation != null)
      {
        m_BoundRotation.Y = m_Rotation.Yaw;
        m_BoundRotation.P = m_Rotation.Pitch;
        m_BoundRotation.R = m_Rotation.Roll;
      }
    }
    /// <summary>
    /// Retrieve rotation information for the TagEditor gui or whatever reason.
    /// </summary>
    /// <param name="roll"></param>
    /// <param name="pitch"></param>
    /// <param name="yaw"></param>
    public void GetRotation(out float roll, out float pitch, out float yaw)
    {
      roll = m_Rotation.Roll;
      pitch = m_Rotation.Pitch;
      yaw = m_Rotation.Yaw;
    }
    /// <summary>
    /// Retrieve translation information for the TagEditor gui or whatever reason.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public void GetTranslation(out Vector3 translation)
    {
      translation.X = m_Translation.X;
      translation.Y = m_Translation.Y;
      translation.Z = m_Translation.Z;
    }
    /// <summary>
    /// Determines if the object is culled; if it isn't, it renders the object
    /// with the appropriate translation and rotation transform.
    /// </summary>
    public void Render()
    {
      //perform cull test on bounding sphere based on translation
      if(m_bCulled == false)
      {
        MdxRender.Dev.RenderState.CullMode = Cull.None;
        
        float dist = MdxRender.Camera.GetObjectDistance(m_Translation.X, m_Translation.Y, m_Translation.Z);

        eLOD lod;
        if(dist > 5)
          lod = eLOD.SUPER_LOW;
        else if(dist > 4)
          lod = eLOD.LOW;
        else if(dist > 3)
          lod = eLOD.MEDIUM;
        else if(dist > 2)
          lod = eLOD.HIGH;
        else if(dist > 1)
          lod = eLOD.SUPER_HIGH;
        else
          lod = eLOD.CAUSES_FIRES;

        //Trace.WriteLine("obj dist = " + dist.ToString());

        //max lod for screens
        lod = eLOD.CAUSES_FIRES;
        MdxRender.Dev.Material = objectColor;
        //        if(m_SelectTool.m_Mode == EditMode.NotSelected)
        //          MdxRender.Dev.Material = objectColor;
        //        else

        if(bBillboardMode)
        {
          if(true)//dist < 30)
          {
            if(m_SelectTool.m_Mode != EditMode.NotSelected)
            {
              MdxRender.Dev.Transform.World = m_Matrix;
              m_Model.Render();
            }

            m_BillboardMatrix = MdxRender.Camera.InverseView;
            m_BillboardMatrix.M41 = m_Translation.X;
            m_BillboardMatrix.M42 = m_Translation.Y;
            m_BillboardMatrix.M43 = m_Translation.Z;

            MdxRender.Dev.Transform.World = m_BillboardMatrix;
            billboard.Render();
          }
        }
        else if(bDecalMode)
        {
          MdxRender.Dev.Transform.World = m_Matrix;
          decal.Render();
        }
        else
        {
          MdxRender.Dev.Transform.World = m_Matrix;
          m_Model.LevelOfDetail = lod;
          m_Model.Render();

          if(m_SelectTool.m_Mode != EditMode.NotSelected)
          {
            if(m_Model.m_BoundingBox != null)
            {
              if(this.m_SelectTool.m_Mode == EditMode.Selected)
                m_Model.m_BoundingBox.RenderBoundingBox();
            }
          }
        }

        m_SelectTool.Render(m_Matrix, m_bEditActive);
      }
    }
    /// <summary>
    /// Performs a selection test by transforming the pick ray into
    /// model space.  It then performs a bounding sphere rejection test
    /// then proceeds to polygon tests if that passes.
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    public bool MouseUp(Vector3 origin, Vector3 direction, bool bLog)
    {
      bool bModelIntersected = false;
      //transform pick ray into model space
      Matrix inv = new Matrix();
      inv = m_Matrix;
      inv.Invert();

      Vector3 t_org = new Vector3();
      Vector3 t_dir = new Vector3();

      if((bBillboardMode == true)&&(m_SelectTool.m_Mode == EditMode.NotSelected))
      {
        Matrix binv = new Matrix();
        binv = m_BillboardMatrix;
        binv.Invert();
        t_org = origin;
        t_dir = direction;
        t_org.TransformCoordinate(binv);
        t_dir.TransformNormal(binv);

        if(billboard.PerformIntersectTest(t_org, t_dir))
        {
          Trace.WriteLine("intersected billboard: ");
          UpdateIdleSelectionState();
        }
      }
      else if((bDecalMode)&&(m_SelectTool.m_Mode == EditMode.NotSelected))
      {
        t_org = origin;
        t_dir = direction;
        t_org.TransformCoordinate(inv);
        t_dir.TransformNormal(inv);
        if(decal.PerformIntersectTest(t_org, t_dir))
        {
          Trace.WriteLine("intersected decal: ");
          UpdateIdleSelectionState();
        }
      }
      else if(m_Model != null)
      {
        //todo: perform ray-bounding sphere rejection test
        t_org = origin;
        t_dir = direction;
        t_org.TransformCoordinate(inv);
        t_dir.TransformNormal(inv);

        bool bSphereIntersected = m_Model.TestRaySphereIntersect(t_org, t_dir);
        if(bSphereIntersected)
        {
          bModelIntersected = m_Model.TestMeshIntersect(t_org, t_dir);
        }

        if(bModelIntersected == false)
        {
          if((m_SelectTool.m_Mode == EditMode.IdleTranslate)||
            (m_SelectTool.m_Mode == EditMode.Selected)||
            (m_SelectTool.m_Mode == EditMode.IdleRotate))
          {
            m_SelectTool.m_Mode = EditMode.NotSelected;
          }
          else
          {
            if(bLog)
              Trace.WriteLine("clicked outside of model: "+ m_SelectTool.m_Mode.ToString());
          }
        }
        else
        {
          UpdateIdleSelectionState();

          //Process the edit
          if(bLog)
            Trace.WriteLine("Processing edit: " + m_SelectTool.m_Mode.ToString());
        }
      }

      //Trace.WriteLine("sphere test = " + bSphereIntersected.ToString() + "  model test = " + bModelIntersected.ToString());
      
      //on mouse up, edit is ALWAYS done (can't hurt to reset to false otherwise)

      return(bModelIntersected);
    }
    public void UpdateIdleSelectionState()
    {
      switch(m_SelectTool.m_Mode)
      {
        case EditMode.NotSelected:
          m_SelectTool.m_Mode = EditMode.Selected;
          break;

        case EditMode.Selected:
          m_SelectTool.m_Mode = EditMode.IdleTranslate;
          break;

        case EditMode.IdleTranslate:
          m_SelectTool.m_Mode = EditMode.IdleRotate;
          break;

        case EditMode.IdleRotate:
          m_SelectTool.m_Mode = EditMode.NotSelected;
          break;
      }
    }

    public void MakeEditInactive()
    {
      m_bEditActive = false;
    }
    public void MouseDown(Vector3 origin, Vector3 direction)
    {
      //check to see if we are in an active edit mode, otherwise skip it
      if((m_SelectTool.m_Mode != EditMode.NotSelected)&&
        (m_SelectTool.m_Mode != EditMode.Selected)&&
        (m_SelectTool.m_Mode != EditMode.IdleTranslate)&&
        (m_SelectTool.m_Mode != EditMode.IdleRotate))
      {
        //tell the selection tool to ???
        m_SelectTool.MouseDown_StartEdit(m_Matrix, origin, direction, false);

        //we are now in active edit mode, set flag for mouse hover
        m_bEditActive = true;

        //Trace.WriteLine("MouseDown (edit start):  " + m_EditTranslationStart.ToString());
      }
    }
    public void Hover(Vector3 origin, Vector3 direction)
    {
      //transform pick ray into model space
      Matrix inv = new Matrix();
      inv = m_SelectTool.WorldSpaceTransform;//m_Matrix;
      inv.Invert();

      Vector3 t_org = new Vector3();
      Vector3 t_dir = new Vector3();
      t_org = origin;
      t_dir = direction;
      t_org.TransformCoordinate(inv);
      t_dir.TransformNormal(inv);

      //update the selection tool (rotation "pie" drawing)

      if(m_bEditActive)
      {
        Vector3 translation_delta;
        Attitude rotation_delta;
        m_SelectTool.HoverEdit(origin, direction, out translation_delta, out rotation_delta);
        IncrementTranslation(translation_delta);
        IncrementRotation(rotation_delta.Roll, rotation_delta.Pitch, rotation_delta.Yaw);
      }
      else
      {
        m_SelectTool.Hover(t_org, t_dir);
      }
    }
    /// <summary>
    /// Added this to the unit class so if the TagEditor changes the palette,
    /// the spawns of that type can be updated to the new model.
    /// </summary>
    /// <param name="tfn"></param>
    public void SetModel(TagFileName tfn)
    {
      int index = MdxRender.MM.RegisterModel(tfn);
      m_Model = MdxRender.MM.GetModel(index);
    }
    public void SetModel(Model3D model)
    {
      m_Model = model;
    }

    //                                          

    /// <summary>
    /// Gets the name of the object.
    /// </summary>
    public string Name
    {
      get
      { 
        if(m_Model != null)return m_Model.Name;
        else return null;
      }
    }

    public Instance3D(Object3D instanceOf, Vector3 location, Attitude rotation)
		{
      this.instanceOf = instanceOf;
      this.m_Translation = location;
      this.m_Rotation = rotation;
		}

    #region Animation Code
    private void ProcessNodeOrientation(int node_index)
    {
      Prometheus.Core.Util.Quaternion quat = new Prometheus.Core.Util.Quaternion();
      int n = node_index;

      do
      {
        //set the local transformation for this node
        quat.Load(m_AniNodes[n].Rotation);
        
        m_Model.NodeTransforms[n].m_relative.setRotationQuaternion(quat.m_quat);
        m_Model.NodeTransforms[n].m_relative.setInverseTranslation(m_AniNodes[n].Translation);

        //multiply by parent transform
        if(m_AniNodes[n].ParentNode != -1)
        {
          m_Model.NodeTransforms[n].m_absolute.set(m_Model.NodeTransforms[m_AniNodes[n].ParentNode].m_absolute.m_matrix);
          m_Model.NodeTransforms[n].m_absolute.postMultiply(ref m_Model.NodeTransforms[n].m_relative);
        }
        else
        {
          //this is a root node, do not need to multiply against parent
          m_Model.NodeTransforms[n].m_absolute.set(m_Model.NodeTransforms[n].m_relative.m_matrix);
        }

        //if this has a child, recurse down the hierarchy
        if(m_AniNodes[n].NextChildNode != -1)
          ProcessNodeOrientation(m_AniNodes[n].NextChildNode);

        //move to the next sibling at this recursion level
        n = m_AniNodes[n].NextSiblingNode;
      }while(n != -1);
    }
    private void InverseTransformModel()
    {
      for(int n=0; n<m_Model.NodeTransforms.Length; n++)
      {
        //Update node locations for our "debug" viewer
        m_Model.NodeTransforms[n].m_FinalNode[0] = 0;
        m_Model.NodeTransforms[n].m_FinalNode[1] = 0;
        m_Model.NodeTransforms[n].m_FinalNode[2] = 0;

        Prometheus.Core.Util.Matrix matrix = new Prometheus.Core.Util.Matrix();
        matrix.set(m_Model.NodeTransforms[n].m_absolute.m_matrix);
        matrix.inverseRotateVect(ref m_Model.NodeTransforms[n].m_FinalNode[0],
          ref m_Model.NodeTransforms[n].m_FinalNode[1],
          ref m_Model.NodeTransforms[n].m_FinalNode[2]);
        matrix.inverseTranslateVect(m_Model.NodeTransforms[n].m_FinalNode);
      }

      //Go through all the vertices and inverse transform them
      //During render, they will be transformed back
      for(int g=0; g<m_Model.m_MeshList.Length; g++)
      {
        for(int m=0; m<m_Model.m_MeshList[g].Length; m++)
          m_Model.m_MeshList[g][m].InverseNodeTransform(m_Model.NodeTransforms);
      }
    }

    public void InitializeAnimationProcessing()
    {
      m_AniNodes = new ModelNode[m_Model.m_Nodes.Length];
      m_Model.NodeTransforms = new NodeTransform[m_Model.m_Nodes.Length];
      for(int x=0; x<m_Model.m_Nodes.Length; x++)
      {
        m_AniNodes[x] = new ModelNode();
        m_AniNodes[x] = m_Model.m_Nodes[x];
        m_Model.NodeTransforms[x] = new NodeTransform();
      }

      ProcessNodeOrientation(0);
      InverseTransformModel();

      m_bAnimationEnabled = true;
    }
    public void RevertToStaticModel()
    {
      if(m_bAnimationEnabled == true)
      {
        m_AniNodes = new ModelNode[m_Model.m_Nodes.Length];
        m_Model.NodeTransforms = new NodeTransform[m_Model.m_Nodes.Length];
        for(int x=0; x<m_Model.m_Nodes.Length; x++)
        {
          m_AniNodes[x] = m_Model.m_Nodes[x];
        }

        ProcessNodeOrientation(0);
        m_bAnimationEnabled = false;
      }
    }
    public void UpdateKeyframe(Animations Ani)
    {
      Util.Matrix vert_transform = new Util.Matrix();

      //copy new nodes into local node structure
      for(int n=0; n<m_Model.m_Nodes.Length; n++)
        Ani.GetAnimationNode(n, ref m_AniNodes[n].Translation, ref m_AniNodes[n].Rotation);

      ProcessNodeOrientation(0);

      //TODO:  update local mesh buffers (DX buffers are updated during draw)
      for(int n=0; n<m_Model.m_Nodes.Length; n++ )
      {
        //Update node locations for our "debug" viewer
        m_Model.NodeTransforms[n].m_FinalNode[0] = 0;
        m_Model.NodeTransforms[n].m_FinalNode[1] = 0;
        m_Model.NodeTransforms[n].m_FinalNode[2] = 0;

        vert_transform = m_Model.NodeTransforms[n].m_absolute;

        vert_transform.inverseRotateVect(ref m_Model.NodeTransforms[n].m_FinalNode[0],
          ref m_Model.NodeTransforms[n].m_FinalNode[1],
          ref m_Model.NodeTransforms[n].m_FinalNode[2]);
          
        vert_transform.inverseTranslateVect(m_Model.NodeTransforms[n].m_FinalNode);
      }

      //Perform transform operations on active permutation/LOD to save CPU
      int geo_index = 0;
      int perm_index = 0;
      for(int region=0; region<m_Model.m_Regions.Length; region++)
      {
        geo_index = m_Model.m_Regions[region].Permutations[perm_index].LodMeshIndex[(int)m_Model.LevelOfDetail];

        for(int m=0; m<m_Model.m_MeshList[geo_index].Length; m++)
          m_Model.m_MeshList[geo_index][m].NodeTransform(m_Model.NodeTransforms);
      }
    }

    public void DrawNodes()
    {
      float node_offset = 0.0f;
      if(nodeSphere == null)
        nodeSphere = Mesh.Sphere(MdxRender.Dev, 0.01f, 6, 6);

      if(nodeLocations == null)
      {
        nodeLocations = new CustomVertex.PositionOnly[2*m_Model.m_Nodes.Length];
        for(int i=0; i<2*m_Model.m_Nodes.Length; i++)
        {
          nodeLocations[i] = new CustomVertex.PositionOnly();
        }
      }

      Microsoft.DirectX.Matrix mat = new Microsoft.DirectX.Matrix();
      MdxRender.SM.m_TextureManager.ActivateDefaultTexture();
      MdxRender.Dev.RenderState.ZBufferFunction = Compare.Always;

      int nc = 0;
      for(int i=0; i<m_Model.m_Nodes.Length; i++)
      {
        mat = Microsoft.DirectX.Matrix.Identity;

        mat.Translate(m_Model.NodeTransforms[i].m_FinalNode[0] + node_offset,
          m_Model.NodeTransforms[i].m_FinalNode[1],
          m_Model.NodeTransforms[i].m_FinalNode[2]);

        MdxRender.Dev.Transform.World = mat;
          
        nodeSphere.DrawSubset(0);

        if(m_AniNodes[i].ParentNode != -1)
        {
          nodeLocations[2*nc].X = m_Model.NodeTransforms[i].m_FinalNode[0]+node_offset;
          nodeLocations[2*nc].Y = m_Model.NodeTransforms[i].m_FinalNode[1];
          nodeLocations[2*nc].Z = m_Model.NodeTransforms[i].m_FinalNode[2];

          nodeLocations[2*nc+1].X = m_Model.NodeTransforms[m_AniNodes[i].ParentNode].m_FinalNode[0]+node_offset;
          nodeLocations[2*nc+1].Y = m_Model.NodeTransforms[m_AniNodes[i].ParentNode].m_FinalNode[1];
          nodeLocations[2*nc+1].Z = m_Model.NodeTransforms[m_AniNodes[i].ParentNode].m_FinalNode[2];
          nc++;
        }
      }

      MdxRender.Dev.Transform.World = Microsoft.DirectX.Matrix.Identity;
      MdxRender.Dev.DrawUserPrimitives(PrimitiveType.LineList, 
        nc,
        nodeLocations);
      MdxRender.Dev.RenderState.ZBufferFunction = Compare.LessEqual;
    }

    /*
    private void ProcessNodeOrientation(int node_index)
    {
      Quaternion quat = new Quaternion();
      Matrix translate = new Matrix();
      int n = node_index;

      do
      {
        //set the local transformation for this node
        quat.X = m_AniNodes[n].Rotation[0];
        quat.Y = m_AniNodes[n].Rotation[1];
        quat.Z = m_AniNodes[n].Rotation[2];
        quat.W = m_AniNodes[n].Rotation[3];
        m_NodeTransforms[n].m_relative.RotateQuaternion(quat);
        m_NodeTransforms[n].m_relative = Matrix.Identity;

        translate.Translate(m_AniNodes[n].Translation[0],
          m_AniNodes[n].Translation[1],
          m_AniNodes[n].Translation[2]);
        m_NodeTransforms[n].m_relative.Multiply(translate);
        
        //multiply by parent transform
        if(m_AniNodes[n].ParentNode != -1)
        {
          m_NodeTransforms[n].m_absolute = Matrix.Identity;//m_NodeTransforms[m_AniNodes[n].ParentNode].m_absolute;
          m_NodeTransforms[n].m_absolute.Multiply(m_NodeTransforms[n].m_relative);
        }
        else
        {
          //this is a root node, do not need to multiply against parent
          m_NodeTransforms[n].m_absolute = m_NodeTransforms[n].m_relative;
        }

        //if this has a child, recurse down the hierarchy
        if(m_AniNodes[n].NextChildNode != -1)
          ProcessNodeOrientation(m_AniNodes[n].NextChildNode);

        //move to the next sibling at this recursion level
        n = m_AniNodes[n].NextSiblingNode;
      }while(n != -1);
    }
    private void InverseTransformModel()
    {
      Microsoft.DirectX.Matrix inv = new Microsoft.DirectX.Matrix();
      Microsoft.DirectX.Matrix test = new Microsoft.DirectX.Matrix();
      Microsoft.DirectX.Vector3 vec = new Microsoft.DirectX.Vector3();

      for(int n=0; n<m_NodeTransforms.Length; n++)
      {
        m_NodeTransforms[n].m_AbsoluteInverse = m_NodeTransforms[n].m_absolute;
        m_NodeTransforms[n].m_AbsoluteInverse.Invert();
        test = m_NodeTransforms[n].m_absolute;
        test.Multiply(m_NodeTransforms[n].m_AbsoluteInverse);
        int ii=0;
      }

    #region node debug 1
//      for(int n=0; n<m_NodeTransforms.Length; n++)
//      {
//        //Update node locations for our "debug" viewer
//        m_NodeTransforms[n].m_FinalNode[0] = 0;
//        m_NodeTransforms[n].m_FinalNode[1] = 0;
//        m_NodeTransforms[n].m_FinalNode[2] = 0;
//        
//        inv = m_NodeTransforms[n].m_absolute;
//        inv.Invert();
//        vec.X = m_NodeTransforms[n].m_FinalNode[0];
//        vec.Y = m_NodeTransforms[n].m_FinalNode[1];
//        vec.Z = m_NodeTransforms[n].m_FinalNode[2];
//        vec.TransformCoordinate(inv);
//        m_NodeTransforms[n].m_FinalNode[0] = vec.X;
//        m_NodeTransforms[n].m_FinalNode[1] = vec.Y;
//        m_NodeTransforms[n].m_FinalNode[2] = vec.Z;
//      }
    #endregion

      //Go through all the vertices and inverse transform them
      //During render, they will be transformed back
      for(int g=0; g<m_MeshList.Length; g++)
        for(int m=0; m<m_MeshList[g].Length; m++)
          m_MeshList[g][m].NodeTransform(m_NodeTransforms, true);
    }

    public void InitializeAnimationProcessing()
    {
      m_AniNodes = new ModelNode[m_Nodes.Length];
      m_NodeTransforms = new NodeTransform[m_Nodes.Length];
      for(int x=0; x<m_Nodes.Length; x++)
      {
        m_AniNodes[x] = new ModelNode();
        m_AniNodes[x] = m_Nodes[x];
        m_NodeTransforms[x] = new NodeTransform();
      }

      ProcessNodeOrientation(0);
      InverseTransformModel();

      m_bAnimationEnabled = true;
    }
    public void RevertToStaticModel()
    {
      if(m_bAnimationEnabled == true)
      {
        m_AniNodes = new ModelNode[m_Nodes.Length];
        m_NodeTransforms = new NodeTransform[m_Nodes.Length];
        for(int x=0; x<m_Nodes.Length; x++)
        {
          m_AniNodes[x] = m_Nodes[x];
        }

        ProcessNodeOrientation(0);
        m_bAnimationEnabled = false;
      }
    }
    public void UpdateKeyframe(Animations Ani)
    {
      Util.Matrix vert_transform = new Util.Matrix();

      //copy new nodes into local node structure
      for(int n=0; n<m_Nodes.Length; n++)
      {
        //m_AniNodes[n] = this.m_Nodes[n];
        Ani.GetAnimationNode(n, ref m_AniNodes[n].Translation, ref m_AniNodes[n].Rotation);
      }

      ProcessNodeOrientation(0);

    #region debug nodes 2
//      //TODO:  update local mesh buffers (DX buffers are updated during draw)
//      for(int n=0; n<m_Nodes.Length; n++ )
//      {
//        //Update node locations for our "debug" viewer
//        m_NodeTransforms[n].m_FinalNode[0] = 0;
//        m_NodeTransforms[n].m_FinalNode[1] = 0;
//        m_NodeTransforms[n].m_FinalNode[2] = 0;
//
//        Microsoft.DirectX.Matrix inv = new Microsoft.DirectX.Matrix();
//        Microsoft.DirectX.Vector3 vec = new Microsoft.DirectX.Vector3();
//        
//        inv = m_NodeTransforms[n].m_absolute;
//        inv.Invert();
//        vec.X = m_NodeTransforms[n].m_FinalNode[0];
//        vec.Y = m_NodeTransforms[n].m_FinalNode[1];
//        vec.Z = m_NodeTransforms[n].m_FinalNode[2];
//        vec.TransformCoordinate(inv);
//      }
    #endregion

      //Perform transform operations on active permutation/LOD to save CPU
      int geo_index = 0;
      int perm_index = 0;
      for(int region=0; region<m_Regions.Length; region++)
      {
        geo_index = m_Regions[region].Permutations[perm_index].LodMeshIndex[(int)m_ActiveLOD];

        for(int m=0; m<m_MeshList[geo_index].Length; m++)
          m_MeshList[geo_index][m].NodeTransform(m_NodeTransforms, false);
      }
    }
*/
    #endregion

    /// <summary>
    /// The parent that contains this object.
    /// </summary>
    public Instance3D Parent
    {
      get { return parent; }
      set
      {
        Instance3D p = (value as Instance3D);
        if (p.ContainsChild(this))
        {
          parent = value;
        }
        else
        {
          throw new Exception("Unable to set parent object: The specified parent does not contain the current Object3D in its 'children' collection.");
        }
      }
    }

	  /// <summary>
	  /// Returns the bounding radius of the object.
	  /// </summary>
	  public float BoundingRadius
	  {
	    get { return instanceOf.BoundingRadius; }
	  }

    public void AddChild(Instance3D child) 
    { 
      children.Add(child); 
      child.Parent = this; 
    } 
      
    public void RemoveChild(string name) 
    { 
      children[name].Parent = null;
      children.Remove(name); 
    } 
      
    public Instance3D GetChild(string name) 
    { 
      return this.children[name];
    } 

    public bool ContainsChild(Instance3D instance)
    {
      return this.children.Contains(instance);
    }
	}
  
  /// <summary>
  /// Represents the roll, pitch, and yaw of a 3D object.
  /// </summary>
  public struct Attitude
  {
    public float Yaw;
    public float Pitch;
    public float Roll;
  }

  /// <summary>
  /// Represents the 3D location of an object.
  /// </summary>
  public struct Coordinate
  {
    public float X;
    public float Y;
    public float Z;
  }

}
