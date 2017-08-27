using System;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Prometheus.Core.Tags;

namespace Prometheus.Core.Render
{
  public enum EditMode {NotSelected, Selected, IdleTranslate, IdleRotate, MoveXY, MoveYZ, MoveXZ, MoveDX, MoveDY, MoveDZ, Pitch, Roll, Yaw};
  public enum ArcMode {Pitch, Roll, Yaw};
  public class ArcHandle
  {
    #region Arc Members
    private static int CircleVertCount = 100;
    private static float RadiusScale = 0.5f;
    private ArcMode m_Mode = ArcMode.Pitch;

    private CustomVertex.PositionOnly[] m_Verts = null;
    
    private short[] m_Arc = new short[CircleVertCount-1];
    private short[] m_Pie = new short[(CircleVertCount-1)*3];
    private VertexBuffer m_VertexBuffer;
    private IndexBuffer m_ArcIndexBuffer;
    private IndexBuffer m_PieIndexBuffer;

    private CustomVertex.PositionOnly[] m_IntersectPoint = new CustomVertex.PositionOnly[2];
    #endregion

    #region Arc Rendering
    public void InitializeModel(ArcMode mode)
    {
      m_IntersectPoint[0] = new CustomVertex.PositionOnly();
      m_IntersectPoint[1] = new CustomVertex.PositionOnly();

      m_Mode = mode;
      //Set up rotation handles
      m_Verts = new CustomVertex.PositionOnly[CircleVertCount+1];

      float dc = 2f*(float)Math.PI/(CircleVertCount-2);
      float arc = 0;
      float a,b;

      m_Verts[CircleVertCount] = new CustomVertex.PositionOnly(0, 0, 0);
      for(int i=0; i<CircleVertCount; i++)
      {
        b = (float)(RadiusScale*Math.Cos(arc));
        a = (float)(RadiusScale*Math.Sin(arc));

        switch(m_Mode)
        {
          case ArcMode.Pitch:
            m_Verts[i] = new CustomVertex.PositionOnly(0, b, a);
            break;
          case ArcMode.Roll:
            m_Verts[i] = new CustomVertex.PositionOnly(a, 0, b);
            break;
          case ArcMode.Yaw:
            m_Verts[i] = new CustomVertex.PositionOnly(b, a, 0);
            break;
        }
        arc += dc;
      }

      for(short i=0; i<CircleVertCount-1; i++)
      {
        m_Arc[i] = i;

        m_Pie[i*3] = (short)CircleVertCount;
        m_Pie[i*3+1] = i;
        m_Pie[i*3+2] = (short)(i+1);
      }

      m_VertexBuffer = new VertexBuffer(typeof(CustomVertex.PositionOnly), m_Verts.Length, MdxRender.Dev, 
        Usage.WriteOnly, CustomVertex.PositionOnly.Format, Pool.Default);
      m_VertexBuffer.Created += new EventHandler(this.OnVertexBufferCreate);
      OnVertexBufferCreate(m_VertexBuffer, null);

      m_ArcIndexBuffer = new IndexBuffer(typeof(short), m_Arc.Length, MdxRender.Dev, Usage.WriteOnly, Pool.Default);
      m_ArcIndexBuffer.Created += new EventHandler(this.OnArcIndexBufferCreate);
      OnArcIndexBufferCreate(m_ArcIndexBuffer, null);

      m_PieIndexBuffer = new IndexBuffer(typeof(short), m_Pie.Length, MdxRender.Dev, Usage.WriteOnly, Pool.Default);
      m_PieIndexBuffer.Created += new EventHandler(this.OnPieIndexBufferCreate);
      OnPieIndexBufferCreate(m_PieIndexBuffer, null);
    }
    private void OnArcIndexBufferCreate(object sender, EventArgs e)
    {
      IndexBuffer buffer = (IndexBuffer)sender;
      buffer.SetData(m_Arc, 0, LockFlags.None);
    }
    private void OnPieIndexBufferCreate(object sender, EventArgs e)
    {
      IndexBuffer buffer = (IndexBuffer)sender;
      buffer.SetData(m_Pie, 0, LockFlags.None);
    }
    private void OnVertexBufferCreate(object sender, EventArgs e)
    {
      VertexBuffer vb = (VertexBuffer)sender;
      GraphicsStream data = vb.Lock(0, 0, LockFlags.None);

      for(int v=0; v<m_Verts.Length; v++)
        data.Write(m_Verts[v]);

      vb.Unlock();
    }
    private void DebugRenderCutPlane()
    {
      float x,y,z;
      float a,b,c;
      int angle_start = 0, angle_stop = 0;
      CustomVertex.PositionOnly[] cross_line = new CustomVertex.PositionOnly[2];
      
      a = -MdxRender.Camera.LookVector.X;
      b = -MdxRender.Camera.LookVector.Y;
      c = -MdxRender.Camera.LookVector.Z;

      //Calculate cut plane intersect points
      switch(m_Mode)
      {
        case ArcMode.Pitch:
          z = (float)Math.Sqrt((RadiusScale*RadiusScale)/((c*c)/(b*b) + 1.0));
          y = -(c/b)*z;
          x = 0;
          cross_line[0] = new CustomVertex.PositionOnly(x,y,z);
          cross_line[1] = new CustomVertex.PositionOnly(-x,-y,-z);
          angle_start = (int)((GetQuadrantAngle(y,z)/(Math.PI*2))*CircleVertCount-1);
          angle_stop = (int)((GetQuadrantAngle(-y,-z)/(Math.PI*2))*CircleVertCount-1);
          break;

        case ArcMode.Roll:
          z = (float)Math.Sqrt((RadiusScale*RadiusScale)/((c*c)/(a*a) + 1.0));
          x = -(c/a)*z;
          y = 0;
          cross_line[0] = new CustomVertex.PositionOnly(x,y,z);
          cross_line[1] = new CustomVertex.PositionOnly(-x,-y,-z);
          angle_start = (int)((GetQuadrantAngle(x,z)/(Math.PI*2))*CircleVertCount-1);
          angle_stop = (int)((GetQuadrantAngle(-x,-z)/(Math.PI*2))*CircleVertCount-1);
          break;

        case ArcMode.Yaw:
          y = (float)Math.Sqrt((RadiusScale*RadiusScale)/((b*b)/(a*a) + 1.0));
          x = -(b/a)*y;
          z = 0;
          cross_line[0] = new CustomVertex.PositionOnly(x,y,z);
          cross_line[1] = new CustomVertex.PositionOnly(-x,-y,-z);
          angle_start = (int)((GetQuadrantAngle(y,x)/(Math.PI*2))*CircleVertCount-1);
          angle_stop = (int)((GetQuadrantAngle(-y,-x)/(Math.PI*2))*CircleVertCount-1);
          break;
      }

      MdxRender.Dev.DrawUserPrimitives(PrimitiveType.LineList, 1, cross_line);
//      MdxRender.Dev.VertexFormat = CustomVertex.PositionOnly.Format;
//      MdxRender.Dev.Indices = m_ArcIndexBuffer;
//      MdxRender.Dev.SetStreamSource(0, m_VertexBuffer, 0);
//      if(angle_stop > angle_start)
//        MdxRender.Dev.DrawIndexedPrimitives(PrimitiveType.LineStrip, 0, 0, m_Verts.Length, angle_start*2, angle_stop-angle_start);
    }
    public void RenderArc(float RadStart, float RadEnd)
    {
      MdxRender.Dev.VertexFormat = CustomVertex.PositionOnly.Format;
      MdxRender.Dev.Indices = m_ArcIndexBuffer;
      MdxRender.Dev.SetStreamSource(0, m_VertexBuffer, 0);

      int index_start = 0;
      int index_count = m_Arc.Length-1;
      MdxRender.Dev.DrawIndexedPrimitives(PrimitiveType.LineStrip, 0, 0, m_Verts.Length, index_start, index_count);
      //DebugRenderCutPlane();
    }
    public void RenderPie(float RadStart, float RadLength)
    {
      MdxRender.Dev.VertexFormat = CustomVertex.PositionOnly.Format;
      MdxRender.Dev.Indices = m_PieIndexBuffer;
      MdxRender.Dev.SetStreamSource(0, m_VertexBuffer, 0);

      //determine pie start index
      int rad_start_index = (int)(RadStart/(Math.PI*2)*CircleVertCount-1);
      float stop_angle = RadStart+RadLength;
      int rad_stop_index = (int)((stop_angle)/(Math.PI*2)*CircleVertCount-1);

      if(RadLength < 0)
      {
        int temp = rad_start_index;
        rad_start_index = rad_stop_index;
        rad_stop_index = temp;
      }

      //if((m_Counter%10)==0)
       // Trace.WriteLine(string.Format("before:  pie_start = {0}  pie_stop = {1}", rad_start_index, rad_stop_index));


      if(rad_start_index < 0)
        rad_start_index = 0;
      if(rad_start_index >= CircleVertCount)
        rad_start_index = CircleVertCount-1;

      //determine pie stop index
      while(stop_angle >= 2*(float)Math.PI)
        stop_angle -= 2*(float)Math.PI;
      if(rad_stop_index < 0)
        rad_stop_index = 0;
      if(rad_stop_index >= CircleVertCount)
        rad_stop_index = CircleVertCount-1;

      //if((m_Counter++%10)==0)
       // Trace.WriteLine(string.Format("after:   pie_start = {0}  pie_stop = {1}", rad_start_index, rad_stop_index));

      //Draw stop and start angle lines
      MdxRender.Dev.DrawIndexedPrimitives(PrimitiveType.LineList, 0, 0, m_Verts.Length, rad_start_index*3, 1);
      MdxRender.Dev.DrawIndexedPrimitives(PrimitiveType.LineList, 0, 0, m_Verts.Length, rad_stop_index*3, 1);

      //Draw the pie
      if(rad_stop_index != rad_start_index)
      {
        //enable alpha blending
        if(MdxRender.DeviceInfo.caps.SourceBlendCaps.SupportsBlendFactor)
        {
          MdxRender.Dev.RenderState.BlendFactor = Color.Gray;
          MdxRender.Dev.RenderState.SourceBlend = Blend.BlendFactor;
          MdxRender.Dev.RenderState.DestinationBlend = Blend.InvBlendFactor;
          MdxRender.Dev.RenderState.AlphaBlendEnable = true;
        }

        //see if pie needs to be rendered in 2 pieces (it wraps around theta = 0)
        if(rad_stop_index < rad_start_index)
        {
          MdxRender.Dev.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, m_Verts.Length, 0, rad_stop_index);
          MdxRender.Dev.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, m_Verts.Length, 
            rad_start_index*3, CircleVertCount-rad_start_index-2);
        }
        else
        {
          MdxRender.Dev.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, m_Verts.Length, 
            rad_start_index*3, rad_stop_index-rad_start_index);
        }
        //disable alpha blending
        if(MdxRender.DeviceInfo.caps.SourceBlendCaps.SupportsBlendFactor)
        {
          MdxRender.Dev.RenderState.AlphaBlendEnable = false;
        }
      }
    }
    public void DrawIntersectPoint()
    {
      MdxRender.Dev.RenderState.PointSize = 3;
      MdxRender.Dev.DrawUserPrimitives(PrimitiveType.LineList, 1, m_IntersectPoint);
    }
    #endregion

    #region Arc Interaction
    public float HoverEdit(Vector3 PickRayOrigin, Vector3 PickRayDirection)
    {
      float current_angle = GetCurrentAngle(PickRayOrigin, PickRayDirection);

      return(current_angle);
    }
    public float Hover(Vector3 PickRayOrigin, Vector3 PickRayDirection)
    {
      Vector3 intersect_pt = new Vector3();
      bool bIntersected = false;
      float proximity = -1;

      switch(m_Mode)
      {
        case ArcMode.Pitch:
          bIntersected = Utility.RayIntersectPlane(PickRayOrigin, PickRayDirection, new Plane(1,0,0,0), out intersect_pt);
          break;
        case ArcMode.Roll:
          bIntersected = Utility.RayIntersectPlane(PickRayOrigin, PickRayDirection, new Plane(0,1,0,0), out intersect_pt);
          break;
        case ArcMode.Yaw:
          bIntersected = Utility.RayIntersectPlane(PickRayOrigin, PickRayDirection, new Plane(0,0,1,0), out intersect_pt);
          break;
      }

      if(bIntersected)
        proximity = (float)Math.Abs(RadiusScale - intersect_pt.Length());

      return(proximity);
    }
    protected float GetCurrentAngle(Vector3 PickRayOrigin, Vector3 PickRayDirection)
    {
      Vector3 intersect_pt = new Vector3();
      bool bIntersected = false;
      float angle = 0;
      float a = 0;
      float b = 0;

      switch(m_Mode)
      {
        case ArcMode.Pitch: //blue
          bIntersected = Utility.RayIntersectPlane(PickRayOrigin, PickRayDirection, new Plane(1,0,0,0), out intersect_pt);
          a = intersect_pt.Z;
          b = intersect_pt.Y;
          break;
        case ArcMode.Roll: //green
          bIntersected = Utility.RayIntersectPlane(PickRayOrigin, PickRayDirection, new Plane(0,1,0,0), out intersect_pt);
          a = intersect_pt.X;
          b = intersect_pt.Z;
          break;
        case ArcMode.Yaw: //red
          bIntersected = Utility.RayIntersectPlane(PickRayOrigin, PickRayDirection, new Plane(0,0,1,0), out intersect_pt);
          a = intersect_pt.Y;
          b = intersect_pt.X;
          break;
      }

      if(bIntersected)
      {
        m_IntersectPoint[1].X = a;
        m_IntersectPoint[1].Y = b;

        angle = GetQuadrantAngle(b, a);
      }

      while(angle < 0)
        angle += (float)Math.PI*2;

      while(angle > Math.PI*2)
        angle -= (float)Math.PI*2;

      return(angle);
    }
    float GetQuadrantAngle(float x, float y)
    {
      float angle = (float)Math.Atan(y/x);

      //correct angle based on quadrant
      if(y >= 0)
      {
        if(x >= 0)
        {
          //quadrant I
          angle = angle;
          //Trace.WriteLine("quadrant I");
        }
        else
        {
          //quadrant II
          angle = (float)Math.PI + angle;
          //Trace.WriteLine("quadrant II");
        }
      }
      else
      {
        if(x >= 0)
        {
          //quadrant IV
          angle = (float)Math.PI*2 + angle;
          //Trace.WriteLine("quadrant IV");
        }
        else
        {
          //quadrant III
          angle = (float)Math.PI + angle;
          //Trace.WriteLine("quadrant III");
        }
      }

    return(angle);
  }
    public float MouseDown_StartEdit(Vector3 PickRayOrigin, Vector3 PickRayDirection)
    {
      float start_angle = GetCurrentAngle(PickRayOrigin, PickRayDirection);
      Trace.WriteLine("start_angle = " + start_angle.ToString());

      return(start_angle);
    }
    #endregion
  }
  /// <summary>
	/// Summary description for SelectTool.
	/// </summary>
	public class SelectTool
	{
    #region Tool Members
    private static float Scale = 1.0f;
    public EditMode m_Mode = EditMode.NotSelected;
    //private int m_Counter = 0;

    private Matrix m_WorldSpaceTransform;
    private Matrix m_ModelSpaceTransform;

    private Vector3 m_EditTranslationStart = new Vector3();
    private Attitude m_EditRotationStart = new Attitude();
    private Attitude m_MouseDownAngle = new Attitude();

    private Plane m_EditAxisTestPlane = new Plane();

    private static CustomVertex.PositionOnly[] m_XAxis = new CustomVertex.PositionOnly[2];
    private static CustomVertex.PositionOnly[] m_XAxisY = new CustomVertex.PositionOnly[2];
    private static CustomVertex.PositionOnly[] m_XAxisZ = new CustomVertex.PositionOnly[2];

    private static CustomVertex.PositionOnly[] m_YAxis = new CustomVertex.PositionOnly[2];
    private static CustomVertex.PositionOnly[] m_YAxisX = new CustomVertex.PositionOnly[2];
    private static CustomVertex.PositionOnly[] m_YAxisZ = new CustomVertex.PositionOnly[2];

    private static CustomVertex.PositionOnly[] m_ZAxis = new CustomVertex.PositionOnly[2];
    private static CustomVertex.PositionOnly[] m_ZAxisX = new CustomVertex.PositionOnly[2];
    private static CustomVertex.PositionOnly[] m_ZAxisY = new CustomVertex.PositionOnly[2];

    private static Mesh m_XAxisTip;
    private static Mesh m_YAxisTip;
    private static Mesh m_ZAxisTip;
    private static Mesh m_XYPlane;
    private static Mesh m_XZPlane;
    private static Mesh m_YZPlane;
    private static Mesh m_Selector;

    private static Material m_BlueX;
    private static Material m_RedZ;
    private static Material m_GreenY;
    private static Material m_Gray;
    private static Material m_Yellow;
    private static Material m_SelectorColor;

    private static ArcHandle m_PitchHandle = new ArcHandle();
    private static ArcHandle m_RollHandle = new ArcHandle();
    private static ArcHandle m_YawHandle = new ArcHandle();
    #endregion

    public SelectTool()
		{
		}
    public Matrix WorldSpaceTransform
    {
      get{return m_WorldSpaceTransform;}
    }

    #region Tool Rendering
    public static void TransformMesh(Mesh mesh, Matrix transform)
    {

      CustomVertex.PositionNormal[] tmp = new CustomVertex.PositionNormal[mesh.NumberVertices];
      Vector3 pos = new Vector3();
      Vector3 norm = new Vector3();
      using (VertexBuffer vb = mesh.VertexBuffer)
      {
        GraphicsStream vertexData = vb.Lock(0, 0, LockFlags.None);
        BinaryReader br = new BinaryReader(vertexData);
        for(int v=0; v<mesh.NumberVertices; v++)
        {
          pos.X = br.ReadSingle();
          pos.Y = br.ReadSingle();
          pos.Z = br.ReadSingle();
          norm.X = br.ReadSingle();
          norm.Y = br.ReadSingle();
          norm.Z = br.ReadSingle();
          pos.TransformCoordinate(transform);
          norm.TransformNormal(transform);
          tmp[v] = new CustomVertex.PositionNormal(pos.X, pos.Y, pos.Z, norm.X, norm.Y, norm.Z);
        }
        vb.Unlock();
      }

      mesh.SetVertexBufferData(tmp, LockFlags.None);
    }
    public static void EnableExtendedAxes(bool bLongX, bool bLongY, bool bLongZ)
    {
      if(bLongX)
      {
        m_XAxis[0] = new CustomVertex.PositionOnly(-100, 0, 0);
        m_XAxis[1] = new CustomVertex.PositionOnly(100,0,0);
      }
      else
      {
        m_XAxis[0] = new CustomVertex.PositionOnly(0.35f*Scale, 0, 0);
        m_XAxis[1] = new CustomVertex.PositionOnly(Scale, 0, 0);
      }

      if(bLongY)
      {
        m_YAxis[0] = new CustomVertex.PositionOnly(0, -100, 0);
        m_YAxis[1] = new CustomVertex.PositionOnly(0,100,0);
      }
      else
      {
        m_YAxis[0] = new CustomVertex.PositionOnly(0, 0.35f*Scale, 0);
        m_YAxis[1] = new CustomVertex.PositionOnly(0,Scale,0);
      }

      if(bLongZ)
      {
        m_ZAxis[0] = new CustomVertex.PositionOnly(0, 0, -100);
        m_ZAxis[1] = new CustomVertex.PositionOnly(0,0,100);
      }
      else
      {
        m_ZAxis[0] = new CustomVertex.PositionOnly(0, 0, 0.35f*Scale);
        m_ZAxis[1] = new CustomVertex.PositionOnly(0,0,Scale);
      }
    }
    public static void InitializeHandleModel()
    {
      m_SelectorColor = new Material();
      m_SelectorColor.Ambient = Color.DarkGray;
      m_SelectorColor.Diffuse = Color.DarkGray;

      m_GreenY = new Material();
      m_GreenY.Ambient = Color.Green;
      m_GreenY.Diffuse = Color.Green;

      m_Gray = new Material();
      m_Gray.Ambient = Color.Gray;
      m_Gray.Diffuse = Color.Gray;

      m_BlueX = new Material();
      m_BlueX.Ambient = Color.Blue;
      m_BlueX.Diffuse = Color.Blue;

      m_RedZ = new Material();
      m_RedZ.Ambient = Color.Red;
      m_RedZ.Diffuse = Color.Red;

      Color transparent_yellow = Color.Yellow;
      m_Yellow = new Material();
      m_Yellow.Ambient = Color.Yellow;
      m_Yellow.Diffuse = Color.Yellow;

      m_XAxis[0] = new CustomVertex.PositionOnly(0.35f*Scale, 0, 0);
      m_XAxis[1] = new CustomVertex.PositionOnly(Scale,0,0);
      m_XAxisY[0] = new CustomVertex.PositionOnly(0.5f*Scale, 0, 0);
      m_XAxisY[1] = new CustomVertex.PositionOnly(0.5f*Scale,0.5f*Scale,0);
      m_XAxisZ[0] = new CustomVertex.PositionOnly(0.5f*Scale, 0, 0);
      m_XAxisZ[1] = new CustomVertex.PositionOnly(0.5f*Scale,0,0.5f*Scale);

      m_YAxis[0] = new CustomVertex.PositionOnly(0, 0.35f*Scale, 0);
      m_YAxis[1] = new CustomVertex.PositionOnly(0,Scale,0);
      m_YAxisX[0] = new CustomVertex.PositionOnly(0,0.5f*Scale,0);
      m_YAxisX[1] = new CustomVertex.PositionOnly(0.5f*Scale,0.5f*Scale,0);
      m_YAxisZ[0] = new CustomVertex.PositionOnly(0,0.5f*Scale,0);
      m_YAxisZ[1] = new CustomVertex.PositionOnly(0,0.5f*Scale,0.5f*Scale);

      m_ZAxis[0] = new CustomVertex.PositionOnly(0, 0, 0.35f*Scale);
      m_ZAxis[1] = new CustomVertex.PositionOnly(0,0,Scale);
      m_ZAxisX[0] = new CustomVertex.PositionOnly(0,0,0.5f*Scale);
      m_ZAxisX[1] = new CustomVertex.PositionOnly(0.5f*Scale,0,0.5f*Scale);
      m_ZAxisY[0] = new CustomVertex.PositionOnly(0,0,0.5f*Scale);
      m_ZAxisY[1] = new CustomVertex.PositionOnly(0,0.5f*Scale,0.5f*Scale);
      
      m_Selector = Mesh.Sphere(MdxRender.Dev, 0.05f*Scale, 8, 4);

      Matrix rotate = new Matrix();
      Matrix translate = new Matrix();
      Matrix scale = new Matrix();
      Matrix overall_transform = new Matrix();
      
      //x-axis
      rotate.RotateY((float)Math.PI/2);
      translate.Translate(1*Scale, 0, 0);
      overall_transform = Matrix.Multiply(rotate, translate);

      m_XAxisTip = Mesh.Cylinder(MdxRender.Dev, 0.05f*Scale, 0f, 0.3f*Scale, 6, 1);
      TransformMesh(m_XAxisTip, overall_transform);

      //y-axis
      rotate.RotateX((float)Math.PI/-2);
      translate.Translate(0, 1*Scale, 0);
      overall_transform = Matrix.Multiply(rotate, translate);

      m_YAxisTip = Mesh.Cylinder(MdxRender.Dev, 0.05f*Scale, 0f, 0.3f*Scale, 6, 1);
      TransformMesh(m_YAxisTip, overall_transform);

      //z-axis
      rotate.RotateZ((float)Math.PI/-2);
      translate.Translate(0, 0, 1*Scale);
      overall_transform = Matrix.Multiply(rotate, translate);

      m_ZAxisTip = Mesh.Cylinder(MdxRender.Dev, 0.05f*Scale, 0f, 0.3f*Scale, 6, 1);
      TransformMesh(m_ZAxisTip, overall_transform);

      m_XYPlane = Mesh.Box(MdxRender.Dev, 0.5f*Scale, 0.5f*Scale, 0.001f*Scale);
      translate.Translate(0.25f*Scale, 0.25f*Scale, 0);
      TransformMesh(m_XYPlane, translate);

      m_XZPlane = Mesh.Box(MdxRender.Dev, 0.5f*Scale, 0.001f*Scale, 0.5f*Scale);
      translate.Translate(0.25f*Scale, 0, 0.25f*Scale);
      TransformMesh(m_XZPlane, translate);

      m_YZPlane = Mesh.Box(MdxRender.Dev, 0.001f*Scale, 0.5f*Scale, 0.5f*Scale);
      translate.Translate(0, 0.25f*Scale, 0.25f*Scale);
      TransformMesh(m_YZPlane, translate);


      m_PitchHandle.InitializeModel(ArcMode.Pitch);
      m_RollHandle.InitializeModel(ArcMode.Roll);
      m_YawHandle.InitializeModel(ArcMode.Yaw);
      EnableExtendedAxes(true, true, true);
    }
    public void DrawXAxis(bool bActive, bool bY, bool bZ)
    {
      if(bActive)
        MdxRender.Dev.Material = m_Yellow;
      else
        MdxRender.Dev.Material = m_BlueX;

      MdxRender.Dev.RenderState.ZBufferEnable = true;
      MdxRender.Dev.DrawUserPrimitives(PrimitiveType.LineList, 1, m_XAxis);
      MdxRender.Dev.RenderState.ZBufferEnable = false;
      
      MdxRender.Dev.Material = m_BlueX;
      MdxRender.Dev.RenderState.Ambient = Color.Gray;
      m_XAxisTip.DrawSubset(0);

      MdxRender.Dev.RenderState.Ambient = Color.White;

      if(bY)
        MdxRender.Dev.Material = m_Yellow;
      else
        MdxRender.Dev.Material = m_BlueX;
      
      MdxRender.Dev.DrawUserPrimitives(PrimitiveType.LineList, 1, m_XAxisY);

      if(bZ)
        MdxRender.Dev.Material = m_Yellow;
      else
        MdxRender.Dev.Material = m_BlueX;
        
      MdxRender.Dev.DrawUserPrimitives(PrimitiveType.LineList, 1, m_XAxisZ);
    }
    public void DrawYAxis(bool bActive, bool bX, bool bZ)
    {
      if(bActive)
        MdxRender.Dev.Material = m_Yellow;
      else
        MdxRender.Dev.Material = m_GreenY;
      MdxRender.Dev.RenderState.ZBufferEnable = true;
      MdxRender.Dev.DrawUserPrimitives(PrimitiveType.LineList, 1, m_YAxis);
      MdxRender.Dev.RenderState.ZBufferEnable = false;

      MdxRender.Dev.Material = m_GreenY;
      MdxRender.Dev.RenderState.Ambient = Color.Gray;
      m_YAxisTip.DrawSubset(0);

      MdxRender.Dev.RenderState.Ambient = Color.White;

      if(bX)
        MdxRender.Dev.Material = m_Yellow;
      else
        MdxRender.Dev.Material = m_GreenY;
      
      MdxRender.Dev.DrawUserPrimitives(PrimitiveType.LineList, 1, m_YAxisX);

      if(bZ)
        MdxRender.Dev.Material = m_Yellow;
      else
        MdxRender.Dev.Material = m_GreenY;
        
      MdxRender.Dev.DrawUserPrimitives(PrimitiveType.LineList, 1, m_YAxisZ);
    }
    public void DrawZAxis(bool bActive, bool bX, bool bY)
    {
      if(bActive)
        MdxRender.Dev.Material = m_Yellow;
      else
        MdxRender.Dev.Material = m_RedZ;

      MdxRender.Dev.RenderState.ZBufferEnable = true;
      MdxRender.Dev.DrawUserPrimitives(PrimitiveType.LineList, 1, m_ZAxis);
      MdxRender.Dev.RenderState.ZBufferEnable = false;
      MdxRender.Dev.Material = m_RedZ;
      MdxRender.Dev.RenderState.Ambient = Color.Gray;
      m_ZAxisTip.DrawSubset(0);

      MdxRender.Dev.RenderState.Ambient = Color.White;

      if(bX)
        MdxRender.Dev.Material = m_Yellow;
      else
        MdxRender.Dev.Material = m_RedZ;
      
      MdxRender.Dev.DrawUserPrimitives(PrimitiveType.LineList, 1, m_ZAxisX);

      if(bY)
        MdxRender.Dev.Material = m_Yellow;
      else
        MdxRender.Dev.Material = m_RedZ;
        
      MdxRender.Dev.DrawUserPrimitives(PrimitiveType.LineList, 1, m_ZAxisY);
    }
    public void DrawXYPlane(bool bActive)
    {
      if(bActive)
      {
        if(MdxRender.DeviceInfo.caps.SourceBlendCaps.SupportsBlendFactor)
        {
          MdxRender.Dev.RenderState.BlendFactor = Color.Gray;
          MdxRender.Dev.RenderState.SourceBlend = Blend.BlendFactor;
          MdxRender.Dev.RenderState.DestinationBlend = Blend.InvBlendFactor;
          MdxRender.Dev.RenderState.AlphaBlendEnable = true;
        }

        MdxRender.Dev.Material = m_Yellow;
        m_XYPlane.DrawSubset(0);

        if(MdxRender.DeviceInfo.caps.SourceBlendCaps.SupportsBlendFactor)
        {
          MdxRender.Dev.RenderState.AlphaBlendEnable = false;
        }
      }
    }
    public void DrawXZPlane(bool bActive)
    {
      if(bActive)
      {
        if(MdxRender.DeviceInfo.caps.SourceBlendCaps.SupportsBlendFactor)
        {
          MdxRender.Dev.RenderState.BlendFactor = Color.Gray;
          MdxRender.Dev.RenderState.SourceBlend = Blend.BlendFactor;
          MdxRender.Dev.RenderState.DestinationBlend = Blend.InvBlendFactor;
          MdxRender.Dev.RenderState.AlphaBlendEnable = true;
        }

        MdxRender.Dev.Material = m_Yellow;
        m_XZPlane.DrawSubset(0);

        if(MdxRender.DeviceInfo.caps.SourceBlendCaps.SupportsBlendFactor)
        {
          MdxRender.Dev.RenderState.AlphaBlendEnable = false;
        }
      }
      else
      {
      }
    }
    public void DrawYZPlane(bool bActive)
    {
      if(bActive)
      {
        if(MdxRender.DeviceInfo.caps.SourceBlendCaps.SupportsBlendFactor)
        {
          MdxRender.Dev.RenderState.BlendFactor = Color.Gray;
          MdxRender.Dev.RenderState.SourceBlend = Blend.BlendFactor;
          MdxRender.Dev.RenderState.DestinationBlend = Blend.InvBlendFactor;
          MdxRender.Dev.RenderState.AlphaBlendEnable = true;
        }

        MdxRender.Dev.Material = m_Yellow;
        m_YZPlane.DrawSubset(0);

        if(MdxRender.DeviceInfo.caps.SourceBlendCaps.SupportsBlendFactor)
        {
          MdxRender.Dev.RenderState.AlphaBlendEnable = false;
        }
      }
    }

    public void Render(Matrix WorldTransform, bool bEditActive)
    {
      if(m_Mode != EditMode.NotSelected)
      {
        m_WorldSpaceTransform = Matrix.Identity;
        m_WorldSpaceTransform.M41 = WorldTransform.M41;
        m_WorldSpaceTransform.M42 = WorldTransform.M42;
        m_WorldSpaceTransform.M43 = WorldTransform.M43;
        m_ModelSpaceTransform = WorldTransform;
        MdxRender.Dev.RenderState.Ambient = Color.White;

        MdxRender.Dev.Transform.World = m_WorldSpaceTransform;
        MdxRender.SM.ConfigureForDiffuseColor();
        MdxRender.Dev.RenderState.AlphaBlendEnable = false;
        MdxRender.Dev.RenderState.CullMode = Cull.Clockwise;

        if(m_Mode != EditMode.NotSelected)
        {
          MdxRender.Dev.RenderState.ZBufferEnable = false;
          MdxRender.Dev.Material = m_SelectorColor;
          m_Selector.DrawSubset(0);
          MdxRender.Dev.RenderState.ZBufferEnable = true;
        }
        MdxRender.Dev.RenderState.ZBufferEnable = false;
        switch(m_Mode)
        {
          case EditMode.NotSelected:
            break;

          case EditMode.Selected:
            MdxRender.Dev.Material = m_SelectorColor;
            break;

          case EditMode.IdleTranslate:
            DrawXAxis(false, false, false);
            DrawYAxis(false, false, false);
            DrawZAxis(false, false, false);
            MdxRender.Dev.Material = m_SelectorColor;
            break;

          case EditMode.IdleRotate:
            MdxRender.Dev.Material = m_BlueX;
            m_PitchHandle.RenderArc(0,0);
            MdxRender.Dev.Material = m_GreenY;
            m_RollHandle.RenderArc(0,0);
            MdxRender.Dev.Material = m_RedZ;
            m_YawHandle.RenderArc(0,0);
            break;

          case EditMode.MoveXY:
            DrawXYPlane(true);
            DrawXAxis(false, true, false);
            DrawYAxis(false, true, false);
            DrawZAxis(false, false, false);
            break;

          case EditMode.MoveYZ:
            DrawYZPlane(true);
            DrawXAxis(false, false, false);
            DrawYAxis(false, false, true);
            DrawZAxis(false, false, true);
            break;

          case EditMode.MoveXZ:
            DrawXZPlane(true);
            DrawXAxis(false, false, true);
            DrawYAxis(false, false, false);
            DrawZAxis(false, true, false);
            break;

          case EditMode.MoveDX:
            DrawXAxis(true, false, false);
            DrawYAxis(false, false, false);
            DrawZAxis(false, false, false);
            break;

          case EditMode.MoveDY:
            DrawXAxis(false, false, false);
            DrawYAxis(true, false, false);
            DrawZAxis(false, false, false);
            break;

          case EditMode.MoveDZ:
            DrawXAxis(false, false, false);
            DrawYAxis(false, false, false);
            DrawZAxis(true, false, false);
            break;

          case EditMode.Pitch:
            MdxRender.Dev.Material = m_Yellow;
            m_PitchHandle.RenderArc(0,0);
            if(bEditActive)
              m_PitchHandle.RenderPie(m_MouseDownAngle.Pitch, m_EditRotationStart.Pitch-m_MouseDownAngle.Pitch);

            MdxRender.Dev.Material = m_GreenY;
            m_RollHandle.RenderArc(0,0);
            MdxRender.Dev.Material = m_RedZ;
            m_YawHandle.RenderArc(0,0);
            break;
          case EditMode.Roll:
            MdxRender.Dev.Material = m_BlueX;
            m_PitchHandle.RenderArc(0,0);
            MdxRender.Dev.Material = m_Yellow;
            m_RollHandle.RenderArc(0,0);
            if(bEditActive)
              m_RollHandle.RenderPie(m_MouseDownAngle.Roll, m_EditRotationStart.Roll-m_MouseDownAngle.Roll);

            MdxRender.Dev.Material = m_RedZ;
            m_YawHandle.RenderArc(0,0);
            break;
          case EditMode.Yaw:
            MdxRender.Dev.Material = m_BlueX;
            m_PitchHandle.RenderArc(0,0);
            MdxRender.Dev.Material = m_GreenY;
            m_RollHandle.RenderArc(0,0);
            MdxRender.Dev.Material = m_Yellow;
            m_YawHandle.RenderArc(0,0);
            if(bEditActive)
              m_YawHandle.RenderPie(m_MouseDownAngle.Yaw, m_EditRotationStart.Yaw-m_MouseDownAngle.Yaw);
            break;
        }

        MdxRender.SM.DisableBlend();
        MdxRender.Dev.RenderState.Ambient = Color.Gray;
        //MdxRender.Dev.RenderState.Lighting = true;
        MdxRender.Dev.RenderState.ZBufferEnable = true;
        MdxRender.Dev.Transform.World = WorldTransform;
      }
    }
    #endregion

    #region Tool Interaction
    public void MouseDown_StartEdit(Matrix WorldTransform, Vector3 origin, Vector3 direction, bool bLog)
    {
      Vector3 t_org = new Vector3();
      Vector3 t_dir = new Vector3();

      //transform pick ray into model space (only for rotation)
      if(ModeIsRotation() == true)
      {
        Matrix inv = new Matrix();
        inv = m_WorldSpaceTransform;
        inv.Invert();

        t_org = origin;
        t_dir = direction;
        t_org.TransformCoordinate(inv);
        t_dir.TransformNormal(inv);
      }

      //select best test plane if we are using x,y,z direct movement
      //larger dot product indicates larger incidence angle
      Vector3 look = MdxRender.Camera.LookVector;
      float xplane_score = (float)Math.Abs(Vector3.Dot(look, new Vector3(1,0,0)));
      float yplane_score = (float)Math.Abs(Vector3.Dot(look, new Vector3(0,1,0)));
      float zplane_score = (float)Math.Abs(Vector3.Dot(look, new Vector3(0,0,1)));

      switch(m_Mode)
      {
        case EditMode.Pitch:
          m_EditRotationStart.Pitch = m_PitchHandle.MouseDown_StartEdit(t_org, t_dir);
          m_MouseDownAngle.Pitch = m_EditRotationStart.Pitch;
          break;

        case EditMode.Roll:
          m_EditRotationStart.Roll = m_RollHandle.MouseDown_StartEdit(t_org, t_dir);
          m_MouseDownAngle.Roll = m_EditRotationStart.Roll;
          break;

        case EditMode.Yaw:
          m_EditRotationStart.Yaw = m_YawHandle.MouseDown_StartEdit(t_org, t_dir);
          m_MouseDownAngle.Yaw = m_EditRotationStart.Yaw;
          break;

        case EditMode.MoveDX:
          if(yplane_score > zplane_score)
            m_EditAxisTestPlane = new Plane(0,1,0, m_ModelSpaceTransform.M42);
          else
            m_EditAxisTestPlane = new Plane(0,0,1, m_ModelSpaceTransform.M43);

          Utility.RayIntersectPlane(origin, direction, m_EditAxisTestPlane, out m_EditTranslationStart);
          break;

        case EditMode.MoveDY:
          if(xplane_score > zplane_score)
            m_EditAxisTestPlane = new Plane(1,0,0, m_ModelSpaceTransform.M41);
          else
            m_EditAxisTestPlane = new Plane(0,0,1, m_ModelSpaceTransform.M43);

          Utility.RayIntersectPlane(origin, direction, m_EditAxisTestPlane, out m_EditTranslationStart);
          break;

        case EditMode.MoveDZ:
          if(xplane_score > yplane_score)
            m_EditAxisTestPlane = new Plane(1,0,0, m_ModelSpaceTransform.M41);
          else
            m_EditAxisTestPlane = new Plane(0,1,0, m_ModelSpaceTransform.M42);

          Utility.RayIntersectPlane(origin, direction, m_EditAxisTestPlane, out m_EditTranslationStart);
          break;

        case EditMode.MoveXY:
          m_EditAxisTestPlane = new Plane(0,0,1, m_ModelSpaceTransform.M43);
          Utility.RayIntersectPlane(origin, direction, m_EditAxisTestPlane, out m_EditTranslationStart);
          break;

        case EditMode.MoveXZ:
          m_EditAxisTestPlane = new Plane(0,1,0, m_ModelSpaceTransform.M42);
          Utility.RayIntersectPlane(origin, direction, m_EditAxisTestPlane, out m_EditTranslationStart);
          break;

        case EditMode.MoveYZ:
          m_EditAxisTestPlane = new Plane(1,0,0, m_ModelSpaceTransform.M41);
          Utility.RayIntersectPlane(origin, direction, m_EditAxisTestPlane, out m_EditTranslationStart);
          break;
      }

      Trace.WriteLine(string.Format("sPitch = {0:N3}  sRoll = {1:N3}  sYaw = {2:N3}", 
        m_EditRotationStart.Pitch,
        m_EditRotationStart.Roll,
        m_EditRotationStart.Yaw));
    }
    private bool ModeIsRotation()
    {
      bool bRotate = false;
      if((m_Mode == EditMode.IdleRotate)||
        (m_Mode == EditMode.Pitch)||
        (m_Mode == EditMode.Roll)||
        (m_Mode == EditMode.Yaw))
        bRotate = true;

      return(bRotate);
    }
    public void HoverEdit(Vector3 origin, Vector3 direction, out Vector3 trans_delta, out Attitude rot_delta)
    {
      Vector3 t_org = new Vector3();
      Vector3 t_dir = new Vector3();

      //transform pick ray into model space (only for rotation)
      if(ModeIsRotation() == true)
      {
        Matrix inv = new Matrix();
        inv = m_WorldSpaceTransform;
        inv.Invert();

        t_org = origin;
        t_dir = direction;
        t_org.TransformCoordinate(inv);
        t_dir.TransformNormal(inv);
      }

      Vector3 intersect_point = new Vector3();
      Attitude rotation_edit = new Attitude();
      
      trans_delta.X = 0;
      trans_delta.Y = 0;
      trans_delta.Z = 0;
      rot_delta.Pitch = 0;
      rot_delta.Roll = 0;
      rot_delta.Yaw = 0;

      switch(m_Mode)
      {
        case EditMode.Pitch:
          rotation_edit.Pitch =  m_PitchHandle.HoverEdit(t_org, t_dir);
          rot_delta.Pitch = rotation_edit.Pitch - m_EditRotationStart.Pitch;
          m_EditRotationStart.Pitch = rotation_edit.Pitch;
          //rotation_edit.Roll =  m_PitchHandle.HoverEdit(t_org, t_dir);
          //rot_delta.Roll = rotation_edit.Roll - m_EditRotationStart.Roll;
          //m_EditRotationStart.Roll = rotation_edit.Roll;
          break;

        case EditMode.Roll:
          rotation_edit.Roll =  m_RollHandle.HoverEdit(t_org, t_dir);
          rot_delta.Roll = rotation_edit.Roll - m_EditRotationStart.Roll;
          m_EditRotationStart.Roll = rotation_edit.Roll;
          //rotation_edit.Pitch =  m_RollHandle.HoverEdit(t_org, t_dir);
          //rot_delta.Pitch = rotation_edit.Pitch - m_EditRotationStart.Pitch;
          //m_EditRotationStart.Pitch = rotation_edit.Pitch;
          break;

        case EditMode.Yaw:
          rotation_edit.Yaw =  m_YawHandle.HoverEdit(t_org, t_dir);
          rot_delta.Yaw = rotation_edit.Yaw - m_EditRotationStart.Yaw;

//          if(Math.Abs(rot_delta.Yaw) > 0.5)
//          {
//            int j=0;
//          }
          m_EditRotationStart.Yaw = rotation_edit.Yaw;
          break;

        case EditMode.MoveDX:
          Utility.RayIntersectPlane(origin, direction, m_EditAxisTestPlane, out intersect_point);
          intersect_point.Y = m_EditTranslationStart.Y;
          intersect_point.Z = m_EditTranslationStart.Z;
          trans_delta = intersect_point - m_EditTranslationStart;
          m_EditTranslationStart = intersect_point;
          break;

        case EditMode.MoveDY:
          Utility.RayIntersectPlane(origin, direction, m_EditAxisTestPlane, out intersect_point);
          intersect_point.X = m_EditTranslationStart.X;
          intersect_point.Z = m_EditTranslationStart.Z;
          trans_delta = intersect_point - m_EditTranslationStart;
          m_EditTranslationStart = intersect_point;
          break;

        case EditMode.MoveDZ:
          Utility.RayIntersectPlane(origin, direction, m_EditAxisTestPlane, out intersect_point);
          intersect_point.X = m_EditTranslationStart.X;
          intersect_point.Y = m_EditTranslationStart.Y;
          trans_delta = intersect_point - m_EditTranslationStart;
          m_EditTranslationStart = intersect_point;
          break;

        case EditMode.MoveXY:
          Utility.RayIntersectPlane(origin, direction, m_EditAxisTestPlane, out intersect_point);
          intersect_point.Z = m_EditTranslationStart.Z;
          trans_delta = intersect_point - m_EditTranslationStart;
          m_EditTranslationStart = intersect_point;
          break;

        case EditMode.MoveXZ:
          Utility.RayIntersectPlane(origin, direction, m_EditAxisTestPlane, out intersect_point);
          intersect_point.Y = m_EditTranslationStart.Y;
          trans_delta = intersect_point - m_EditTranslationStart;
          m_EditTranslationStart = intersect_point;
          break;

        case EditMode.MoveYZ:
          Utility.RayIntersectPlane(origin, direction, m_EditAxisTestPlane, out intersect_point);
          intersect_point.X = m_EditTranslationStart.X;
          trans_delta = intersect_point - m_EditTranslationStart;
          m_EditTranslationStart = intersect_point;
          break;
      }

      //if((Math.Abs(rot_delta.Pitch) > 0.001)||(Math.Abs(rot_delta.Roll) > 0.001)||(Math.Abs(rot_delta.Yaw) > 0.5))
      //  Trace.WriteLine(string.Format("droll={0}  dpitch={1}  dyaw={2}",
      //    rot_delta.Pitch,
      //    rot_delta.Roll,
      //    rot_delta.Yaw));
    }
    public void Hover(Vector3 t_origin, Vector3 t_direction)
    {
      if((m_Mode == EditMode.NotSelected)||(m_Mode == EditMode.Selected))
      {
        //do nothing
      }
      else if(ModeIsRotation() == true)
      {
        float closest_prox = 100;
        float pitch_prox = m_PitchHandle.Hover(t_origin, t_direction);
        float roll_prox = m_RollHandle.Hover(t_origin, t_direction);
        float yaw_prox = m_YawHandle.Hover(t_origin, t_direction);


        //get the closest intersection
        EditMode tmp = EditMode.IdleRotate;

        if((pitch_prox < 0.2f)&&(pitch_prox >= 0))
        {
          closest_prox = pitch_prox;
          tmp = EditMode.Pitch;
        }

        if((roll_prox < 0.2f)&&(roll_prox >= 0)&&(roll_prox < closest_prox))
        {
          closest_prox = roll_prox;
          tmp = EditMode.Roll;
        }

        if((yaw_prox < 0.2f)&&(yaw_prox >= 0)&&(yaw_prox < closest_prox))
        {
          closest_prox = yaw_prox;
          tmp = EditMode.Yaw;
        }

        //if((m_Counter++ % 20) == 0)
        //  Trace.WriteLine(string.Format("pitchprx: {0} rollprx: {1} yaw_prox: {2}", pitch_prox, roll_prox, yaw_prox) + tmp.ToString());

        m_Mode = tmp;
      }
      else
      {
        if(m_XAxisTip.Intersect(t_origin, t_direction))
        {
          m_Mode = EditMode.MoveDX;
        }
        else if(m_YAxisTip.Intersect(t_origin, t_direction))
        {
          m_Mode = EditMode.MoveDY;
        }
        else if(m_ZAxisTip.Intersect(t_origin, t_direction))
        {
          m_Mode = EditMode.MoveDZ;
        }
        else if(m_XYPlane.Intersect(t_origin, t_direction))
        {
          m_Mode = EditMode.MoveXY;
        }
        else if(m_XZPlane.Intersect(t_origin, t_direction))
        {
          m_Mode = EditMode.MoveXZ;
        }
        else if(m_YZPlane.Intersect(t_origin, t_direction))
        {
          m_Mode = EditMode.MoveYZ;
        }
        else
        {
          m_Mode = EditMode.IdleTranslate;
        }
      }
    }
    #endregion
	}
  public class SelectionBox
  {
    public enum SelectionBoxState
    {
      Inactive,
      FindPlane,
      FindStartCorner,
      FindEndCorner,
      FindHeight,
      Active
    };

    private SelectionBoxState currentState = SelectionBoxState.Inactive;
    static private Plane basePlane = new Plane(0,0,1,0);
    static private Plane intersectPlane = new Plane(0,0,1,0);
    static private Vector3 startCorner = new Vector3();
    static private Vector3 endCorner = new Vector3();
    static private Vector3 tempVector = new Vector3();
    static private float boxHeight = 0;
    
    //rendering
    static private BOUNDING_BOX boxOutline = new BOUNDING_BOX();
    static private CustomVertex.PositionOnly[] xLine = new CustomVertex.PositionOnly[2];
    static private CustomVertex.PositionOnly[] yLine = new CustomVertex.PositionOnly[2];
    static private CustomVertex.PositionOnly[] zLine = new CustomVertex.PositionOnly[2];
    static private Mesh boxMesh;
    static private Mesh cursor;
    static private Material aqua;
    static private Material white;

    public bool UpdateNeeded
    {
      get{return(currentState != SelectionBoxState.Inactive);}
    }
    public SelectionBox()
    {
    }
    public void Begin()
    {
      Vector3 pos = new Vector3();
      Vector3 dir = new Vector3();
      Vector3 point_in_plane = new Vector3();
      pos = MdxRender.Camera.Position;
      dir = MdxRender.Camera.LookVector;

      intersectPlane.A = dir.X; 
      intersectPlane.B = dir.Y;
      intersectPlane.C = dir.Z;
      intersectPlane = Plane.Normalize(intersectPlane);
      
      //calculate the coordinate that is 20 units directly in front of camera
      point_in_plane.X = pos.X + dir.X*20;
      point_in_plane.Y = pos.Y + dir.Y*20;
      point_in_plane.Z = pos.Z + dir.Z*20;
      Trace.WriteLine("cam_loc = " + pos.ToString());
      Trace.WriteLine("cam_dir = " + dir.ToString());
      Trace.WriteLine("point in plane = " + point_in_plane.ToString());

      //distance here is from the point to the origin, not the nearest distance from plane to origin...need to fix
      intersectPlane.D = (point_in_plane.X*intersectPlane.A + point_in_plane.Y*intersectPlane.B + point_in_plane.Z*intersectPlane.C);

      Trace.WriteLine("intersect plane = " + intersectPlane.ToString());

      //todo: get the optimal intersect plane based on camera orientation
      currentState = SelectionBoxState.FindPlane;
    }
    public void Click(Vector3 origin, Vector3 dir)
    {
      switch(currentState)
      {
        case SelectionBoxState.FindPlane:
          if(Utility.RayIntersectPlane(origin, dir, intersectPlane, out startCorner))
          {
            basePlane.D = startCorner.Z;
            currentState = SelectionBoxState.FindStartCorner;
          }
          break;

        case SelectionBoxState.FindStartCorner:
          Utility.RayIntersectPlane(origin, dir, basePlane, out startCorner);
          currentState = SelectionBoxState.FindEndCorner;
          break;

        case SelectionBoxState.FindEndCorner:
          Utility.RayIntersectPlane(origin, dir, basePlane, out endCorner);
          currentState = SelectionBoxState.FindHeight;
          break;

        case SelectionBoxState.FindHeight:
          Vector3 temp = new Vector3();
          if(Utility.RayIntersectPlane(origin, dir, intersectPlane, out temp))
          {
            boxHeight = temp.Z - startCorner.Z;
          }
          //todo: calc new vertical intersectPlane based on endCorner
          currentState = SelectionBoxState.Active;
          break;

        case SelectionBoxState.Active:
          //todo: if clicked outside of bounding box model, deselect it
          currentState = SelectionBoxState.Inactive;
          break;
      }
      Trace.WriteLine(string.Format("Selection Box State: {0}", currentState));
      Trace.WriteLine(string.Format("start: {0} {1} {2}", startCorner.X, startCorner.Y, startCorner.Z));
      Trace.WriteLine(string.Format("end  : {0} {1} {2}", endCorner.X, endCorner.Y, endCorner.Z));
    }
    public void Hover(Vector3 origin, Vector3 dir)
    {
      switch(currentState)
      {
        case SelectionBoxState.FindPlane:
          if(Utility.RayIntersectPlane(origin, dir, intersectPlane, out startCorner))
            basePlane.D = startCorner.Z;
          Trace.WriteLine(string.Format("findplane:  {0} {1} {2}", startCorner.X, startCorner.Y, startCorner.Z));
          UpdatePrimitives();
          break;

        case SelectionBoxState.FindStartCorner:
          if(Utility.RayIntersectPlane(origin, dir, basePlane, out startCorner) == false)
          {
            startCorner.X = 0;
            startCorner.Y = 0;
            startCorner.Z = 0;
          }
          Trace.WriteLine(string.Format("start corner:  {0} {1} {2}", startCorner.X, startCorner.Y, startCorner.Z));
          UpdatePrimitives();
          break;

        case SelectionBoxState.FindEndCorner:
          if(Utility.RayIntersectPlane(origin, dir, basePlane, out endCorner) == false)
          {
            endCorner.X = 0;
            endCorner.Y = 0;
            endCorner.Z = 0;
          }
          UpdateBox(startCorner, endCorner, 0.005f);
          boxOutline.min[0] = startCorner.X;
          boxOutline.min[1] = startCorner.Y;
          boxOutline.min[2] = startCorner.Z;
          boxOutline.max[0] = endCorner.X;
          boxOutline.max[1] = endCorner.Y;
          boxOutline.max[2] = endCorner.Z + 0.005f;
          break;

        case SelectionBoxState.FindHeight:
          Vector3 temp = new Vector3();
          if(Utility.RayIntersectPlane(origin, dir, intersectPlane, out temp))
          {
            boxHeight = temp.Z - startCorner.Z;
          }
          UpdateBox(startCorner, endCorner, boxHeight);
          boxOutline.min[0] = startCorner.X;
          boxOutline.min[1] = startCorner.Y;
          boxOutline.min[2] = startCorner.Z;
          boxOutline.max[0] = endCorner.X;
          boxOutline.max[1] = endCorner.Y;
          boxOutline.max[2] = endCorner.Z + boxHeight;
          break;
      }
    }
    
    public void Render()
    {
      if(boxMesh != null)
      {

        MdxRender.Dev.Transform.World = Matrix.Identity;
        switch(currentState)
        {
          case SelectionBoxState.FindPlane:
            MdxRender.Dev.Material = white;
            MdxRender.Dev.DrawUserPrimitives(PrimitiveType.LineList, 1, xLine);
            MdxRender.Dev.DrawUserPrimitives(PrimitiveType.LineList, 1, yLine);
            MdxRender.Dev.DrawUserPrimitives(PrimitiveType.LineList, 1, zLine);
            RenderTransparentBox();
            break;

          case SelectionBoxState.FindStartCorner:
            MdxRender.Dev.Material = white;
            MdxRender.Dev.DrawUserPrimitives(PrimitiveType.LineList, 1, xLine);
            MdxRender.Dev.DrawUserPrimitives(PrimitiveType.LineList, 1, yLine);
            MdxRender.Dev.DrawUserPrimitives(PrimitiveType.LineList, 1, zLine);
            RenderTransparentBox();
            break;

          case SelectionBoxState.FindEndCorner:
            MdxRender.Dev.Material = white;
            MdxRender.Dev.DrawUserPrimitives(PrimitiveType.LineList, 1, xLine);
            MdxRender.Dev.DrawUserPrimitives(PrimitiveType.LineList, 1, yLine);
            boxOutline.RenderBoundingBox();
            RenderTransparentBox();
            break;

          case SelectionBoxState.FindHeight:
          case SelectionBoxState.Active:
            MdxRender.Dev.Material = white;
            boxOutline.RenderBoundingBox();
            RenderTransparentBox();
            break;
        }

      }
    }
    public void RenderTransparentBox()
    {
      if(MdxRender.DeviceInfo.caps.SourceBlendCaps.SupportsBlendFactor)
      {
        MdxRender.Dev.RenderState.BlendFactor = Color.Gray;
        MdxRender.Dev.RenderState.SourceBlend = Blend.BlendFactor;
        MdxRender.Dev.RenderState.DestinationBlend = Blend.InvBlendFactor;
        MdxRender.Dev.RenderState.AlphaBlendEnable = true;
      }

      MdxRender.Dev.Material = aqua;
      boxMesh.DrawSubset(0);

      if(MdxRender.DeviceInfo.caps.SourceBlendCaps.SupportsBlendFactor)
      {
        MdxRender.Dev.RenderState.AlphaBlendEnable = false;
      }
    }
    
    static private void UpdatePrimitives()
    {
      UpdateBox(new Vector3(startCorner.X-500, startCorner.Y-500, startCorner.Z), 
        new Vector3(startCorner.X+500, startCorner.Y+500, startCorner.Z), 0.005f);

      zLine[0].X = startCorner.X;
      zLine[0].Y = startCorner.Y;
      zLine[0].Z = startCorner.Z - 500;
      zLine[1].X = startCorner.X;
      zLine[1].Y = startCorner.Y;
      zLine[1].Z = startCorner.Z + 500;

      //update plane lines
      xLine[0].X = startCorner.X - 500;
      xLine[0].Y = startCorner.Y;
      xLine[0].Z = basePlane.D;
      xLine[1].X = startCorner.X + 500;
      xLine[1].Y = startCorner.Y;
      xLine[1].Z = basePlane.D + 0.06f;

      yLine[0].X = startCorner.X;
      yLine[0].Y = startCorner.Y - 500;
      yLine[0].Z = basePlane.D;
      yLine[1].X = startCorner.X;
      yLine[1].Y = startCorner.Y + 500;
      yLine[1].Z = basePlane.D + 0.06f;
    }
    static public void Initialize()
    {
      boxMesh = Mesh.Box(MdxRender.Dev, 1,1,1);
      cursor = Mesh.Box(MdxRender.Dev, 0.5f, 0.5f, 2);

      xLine[0] = new CustomVertex.PositionOnly();
      xLine[1] = new CustomVertex.PositionOnly();
      yLine[0] = new CustomVertex.PositionOnly();
      yLine[1] = new CustomVertex.PositionOnly();
      zLine[0] = new CustomVertex.PositionOnly();
      zLine[1] = new CustomVertex.PositionOnly();

      aqua = new Material();
      aqua.Ambient = Color.Aqua;
      aqua.Diffuse = Color.Aqua;
      white = new Material();
      white.Ambient = Color.White;
      white.Diffuse = Color.White;

      UpdatePrimitives();
    }
    public static void UpdateBox(Vector3 start, Vector3 end, float height)
    {
      CustomVertex.PositionNormal[] tmp = new CustomVertex.PositionNormal[boxMesh.NumberVertices];
      Vector3 pos = new Vector3();
      Vector3 norm = new Vector3();
      using (VertexBuffer vb = boxMesh.VertexBuffer)
      {
        GraphicsStream vertexData = vb.Lock(0, 0, LockFlags.None);
        BinaryReader br = new BinaryReader(vertexData);
        for(int v=0; v<boxMesh.NumberVertices; v++)
        {
          pos.X = br.ReadSingle();
          pos.Y = br.ReadSingle();
          pos.Z = br.ReadSingle();
          norm.X = br.ReadSingle();
          norm.Y = br.ReadSingle();
          norm.Z = br.ReadSingle();
          tmp[v] = new CustomVertex.PositionNormal(pos.X, pos.Y, pos.Z, norm.X, norm.Y, norm.Z);
        }
        vb.Unlock();
      }

      tmp[0].X = start.X;
      tmp[0].Y = start.Y;
      tmp[0].Z = start.Z;

      tmp[1].X = start.X;
      tmp[1].Y = start.Y;
      tmp[1].Z = start.Z+height;

      tmp[2].X = start.X;
      tmp[2].Y = end.Y;
      tmp[2].Z = start.Z+height;

      tmp[3].X = start.X;
      tmp[3].Y = end.Y;
      tmp[3].Z = start.Z;

      tmp[4].X = start.X;
      tmp[4].Y = end.Y;
      tmp[4].Z = start.Z;

      tmp[5].X = start.X;
      tmp[5].Y = end.Y;
      tmp[5].Z = start.Z+height;

      tmp[6].X = end.X;
      tmp[6].Y = end.Y;
      tmp[6].Z = start.Z+height;

      tmp[7].X = end.X;
      tmp[7].Y = end.Y;
      tmp[7].Z = start.Z;

      tmp[8].X = end.X;
      tmp[8].Y = end.Y;
      tmp[8].Z = start.Z;

      tmp[9].X = end.X;
      tmp[9].Y = end.Y;
      tmp[9].Z = start.Z+height;

      tmp[10].X = end.X;
      tmp[10].Y = start.Y;
      tmp[10].Z = start.Z+height;

      tmp[11].X = end.X;
      tmp[11].Y = start.Y;
      tmp[11].Z = start.Z;

      tmp[12].X = start.X;
      tmp[12].Y = start.Y;
      tmp[12].Z = end.Z+height;

      tmp[13].X = start.X;
      tmp[13].Y = start.Y;
      tmp[13].Z = start.Z;

      tmp[14].X = end.X;
      tmp[14].Y = start.Y;
      tmp[14].Z = start.Z;

      tmp[15].X = end.X;
      tmp[15].Y = start.Y;
      tmp[15].Z = start.Z+height;

      tmp[16].X = start.X;
      tmp[16].Y = start.Y;
      tmp[16].Z = start.Z+height;

      tmp[17].X = end.X;
      tmp[17].Y = start.Y;
      tmp[17].Z = start.Z+height;

      tmp[18].X = end.X;
      tmp[18].Y = end.Y;
      tmp[18].Z = start.Z+height;

      tmp[19].X = start.X;
      tmp[19].Y = end.Y;
      tmp[19].Z = start.Z+height;

      tmp[20].X = start.X;
      tmp[20].Y = start.Y;
      tmp[20].Z = start.Z;

      tmp[21].X = start.X;
      tmp[21].Y = end.Y;
      tmp[21].Z = start.Z;

      tmp[22].X = end.X;
      tmp[22].Y = end.Y;
      tmp[22].Z = start.Z;

      tmp[23].X = end.X;
      tmp[23].Y = start.Y;
      tmp[23].Z = start.Z;

      boxMesh.SetVertexBufferData(tmp, LockFlags.None);
    }

    public bool Inside(Vector3 ObjectOrigin)
    {
      bool bOverall = false;

      if(currentState == SelectionBoxState.FindHeight)
      {
        //test if x within bounds
        if((ObjectOrigin.X > startCorner.X)&&(ObjectOrigin.X < endCorner.X))
          bOverall = true;
        else if((ObjectOrigin.X > endCorner.X)&&(ObjectOrigin.X < startCorner.X))
          bOverall = true;

        if(bOverall)
        {
          bOverall = false;
          //test if y within bounds
          if((ObjectOrigin.Y > startCorner.Y)&&(ObjectOrigin.Y < endCorner.Y))
            bOverall = true;
          else if((ObjectOrigin.Y > endCorner.Y)&&(ObjectOrigin.Y < startCorner.Y))
            bOverall = true;

          if(bOverall)
          {
            bOverall = false;
            float ht_start = startCorner.Z;
            float ht_end = startCorner.Z + boxHeight;

            //test if z within bounds
            if((ObjectOrigin.Z > ht_start)&&(ObjectOrigin.Z < ht_end))
              bOverall = true;
            else if((ObjectOrigin.Z > ht_end)&&(ObjectOrigin.Z < ht_start))
              bOverall = true;
          }
        }
      }

      return(bOverall);
    }
  }
}
