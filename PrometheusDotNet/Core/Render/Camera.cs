/* ---------------------------------------------------------------
 * Prometheus
 * Bungie Map-based Multi-Game Editing Kit
 * 2004-2005, Halo-Dev
 * ---------------------------------------------------------------
 * Class       : Prometheus.Core.Camera
 * Description : Quaternion based 3D camera.
 * Author      : Grenadiac
 * Co-Authors  : 
 * ---------------------------------------------------------------
 */

using Microsoft.DirectX;
using System.Diagnostics;
using Prometheus.Core.Tags;
using System;
namespace Prometheus.Core.Render
{
  public enum CamMode {Idle, KeyOn, Coast};
  public class RunningAvg
  {
    private static int BUF_SIZE = 10;
    private float[] m_Buffer = new float[BUF_SIZE];
    private int m_Index = 0;
    private float m_Average = 0;
    public float Average
    {
      get {return m_Average;}
    }
    public float Update(float data_pt)
    {
      m_Average = 0;
      m_Buffer[m_Index] = data_pt;
      m_Index++;
      if((m_Index % BUF_SIZE) == 0)
        m_Index = 0;

      for(int i=0; i<BUF_SIZE; i++)
        m_Average += m_Buffer[i];

      m_Average /= BUF_SIZE;

      return(m_Average);
    }
  }
  public class CameraSpeed
  {
    private float MAX_CAM_SPEED = 5.0f;
    private float CAM_ACC = 5.0f;
    private float CAM_COAST_ACC = 0.3f;
    public CamMode m_Mode = CamMode.Idle;
    private HighResTimer m_LaunchTimer = new HighResTimer();
    private HighResTimer m_CoastTimer = new HighResTimer();
    public float m_CurrentSpeed = 0;
    private bool m_bRotateMode = false;

    public void EnableRotateMode()
    {
      MAX_CAM_SPEED = 1.0f;
      CAM_ACC = 1.0f;
      CAM_COAST_ACC = 0.1f;
      m_bRotateMode = true;
    }
    public void EnableTranslateMode()
    {
      MAX_CAM_SPEED = 5.0f;
      CAM_ACC = 2.0f;
      CAM_COAST_ACC = 0.3f;
      m_bRotateMode = false;
    }
    public float Speed
    {
      get{return(m_CurrentSpeed);}
    }
    public bool IsCoasting
    {
      get{return(m_Mode == CamMode.Coast);}
    }
    public void MovementActivated(bool bMoved)
    {
      float dt = 0;
      float acc_speed = 0;
      MAX_CAM_SPEED = OptionsManager.CameraSpeed;
      //test comment
      CAM_ACC = 5.0f*(OptionsManager.CameraSpeed/10f);
      CAM_COAST_ACC = 4.0f*(OptionsManager.CameraSpeed/10f);

      switch(m_Mode)
      {
        case CamMode.Idle:
          if(bMoved == true)
          {
            m_LaunchTimer.ResetTimer();
            m_Mode = CamMode.KeyOn;
          }
          break;

        case CamMode.KeyOn:
          if(bMoved == false)
          {
            //m_CurrentSpeed *= -0.2f;//COAST_CAM_INITIAL_SPEED;
            m_CurrentSpeed *= -0.2f;//OptionsManager.CameraSpeed/20;//COAST_CAM_INITIAL_SPEED;
            m_CoastTimer.ResetTimer();
            m_Mode = CamMode.Coast;
          }
          break;

        case CamMode.Coast:
          if(bMoved == true)
          {
            m_Mode = CamMode.KeyOn;
            m_CurrentSpeed = 0;
          }
          if(m_CurrentSpeed > 0)
          {
            m_CurrentSpeed = 0;
            m_Mode = CamMode.Idle;
          }
          break;
      }

      //Trace.WriteLine("mode = " + m_Mode.ToString() + "spd = " + m_CurrentSpeed.ToString());

      //update camera speed
      switch(m_Mode)
      {
        case CamMode.Idle:
          break;

        case CamMode.KeyOn:
          dt = (float)m_LaunchTimer.GetElapsedTime();
          acc_speed = CAM_ACC*dt;

          if(acc_speed > MAX_CAM_SPEED)
            m_CurrentSpeed = MAX_CAM_SPEED;
          else
            m_CurrentSpeed = acc_speed;
          break;

        case CamMode.Coast:
          dt = (float)m_CoastTimer.GetElapsedTime();
          acc_speed = m_CurrentSpeed + CAM_COAST_ACC*dt;

          if(m_bRotateMode)
          {
            if(acc_speed < 0)
            {
              m_Mode = CamMode.Idle;
              m_CurrentSpeed = 0;
            }
            else
              m_CurrentSpeed = acc_speed;
          }
          else
          {
            if(acc_speed > 0)
            {
              m_Mode = CamMode.Idle;
              m_CurrentSpeed = 0;
            }
            else
              m_CurrentSpeed = acc_speed;
          }
          break;
      }
    }
  }

	/// <summary>
	/// Quaternion base 3D camera.
	/// </summary>
  public class Camera
  {
    private static float MIN_PITCH_RAD = -3.14f;
    private static float MAX_PITCH_RAD = 3.14f;
    private enum eDir		{ ceMove, ceStraf, ceUp };
    private enum eOrient	{ cePitch , ceRoll , ceYaw };
    private Matrix m_matView = new Matrix();
    private Matrix m_invView = new Matrix();
    public CameraSpeed TranslationControl = new CameraSpeed();
    //public CameraSpeed RotationControl = new CameraSpeed();
    private bool m_bNeedUpdated = true;
    private float m_fRPM = 30.0f;
    private float m_PitchAngle = 0;
    private float m_dt = 0.01f;
    private Vector3 m_TranslateCoastVector = new Vector3();

    //new implementation
    private Vector3 m_LookVector = new Vector3();
    private Vector3 m_UpVector = new Vector3(0,0,1);
    private Vector3 m_PositionVector = new Vector3();
	
    public Vector3 NewSpawnLocation
    {
      get
      {
        float dist = 5.0f;
        return(new Vector3(m_PositionVector.X + LookVector.X*dist,
          m_PositionVector.Y + LookVector.Y*dist,
          m_PositionVector.Z + LookVector.Z*dist));
      }
    }
    public Vector3 LookVector
    {
      get{return m_LookVector;}
    }
    public Vector3 Position
    {
      get{return m_PositionVector;}
    }
    public Matrix InverseView
    {
      get{return m_invView;}
    }
    public Camera()
    {
      TranslationControl.EnableTranslateMode();
      //RotationControl.EnableRotateMode();
    }
    public void SetLocation(Vector3 vFrom)
    {
      //leave the look vector the same
      m_PositionVector = vFrom;
      m_bNeedUpdated = true;
    }
    public void SetLookAt(Vector3 vFrom, Vector3 vTo)
    {
      //we don't use the "up" vector since it is always assumed to be +Z

      //Calculate the baseline "look" quaternion
      m_LookVector.X = vTo.X - vFrom.X;
      m_LookVector.Y = vTo.Y - vFrom.Y;
      m_LookVector.Z = vTo.Z - vFrom.Z;
      m_LookVector.Normalize();

      m_PositionVector = vFrom;

      //reset Pitch tracking to match look vector
      float xy = (float)Math.Sqrt(m_LookVector.X*m_LookVector.X + m_LookVector.Y*m_LookVector.Y);
      m_PitchAngle = (float)Math.Atan(m_LookVector.Z/xy);

      m_bNeedUpdated = true;
    }
    public void Pitch(float fAngle)
    {
      float scale = (OptionsManager.CameraSensitivity/10f)*4;

      float test_ang = m_PitchAngle + fAngle*scale;

      if((test_ang > MIN_PITCH_RAD)&&(test_ang < MAX_PITCH_RAD))
      {
        //fAngle *= RotationControl.Speed;
        m_PitchAngle += fAngle*scale;
        ApplyRotate(fAngle*scale, eOrient.cePitch);
      }
    }
    public void SetFPS(float fps)
    {
      if(fps != 0)
      {
        m_dt = 1.0f/fps;

        //if(m_dt > 0.01)
        //  m_dt = 0.01f;
      }
    }
    public void Yaw(float fAngle)
    {
      float scale = (OptionsManager.CameraSensitivity/10f)*4;
      //fAngle *= RotationControl.Speed;
      ApplyRotate(fAngle*scale, eOrient.ceYaw);
    }
    public void Move(float fDistance)
    {
      ApplyTranslation(fDistance, eDir.ceMove);
    }
    public void Strafe(float fDistance)
    {
      ApplyTranslation(fDistance, eDir.ceStraf);
    }
    public void Up(float fDistance)
    {
      ApplyTranslation(fDistance, eDir.ceUp);
    }
    public Matrix GetViewMatrix()
    {
      if(m_bNeedUpdated)
      {
        //Trace.WriteLine("update");
        Update();
      }

      return m_matView;
    }
    public void SetRPM(float fRPM)
    {
      m_fRPM = (fRPM < 0.0f) ? 0.0f : fRPM; 
    }
    protected void Update()
    {
      m_matView = Matrix.LookAtRH(m_PositionVector, m_PositionVector + m_LookVector, m_UpVector);
      m_invView = Matrix.Invert(m_matView);
      // 3) Set flag to false, to save CPU
      m_bNeedUpdated = false;
    }
    private void ApplyTranslation(float fDistance, eDir ceDir)
    {
      Vector3 vDir = new Vector3();

      switch (ceDir)
      {
        case eDir.ceMove:
        {
          vDir = -m_LookVector;
          vDir.Normalize();
          break;
        }
        case eDir.ceStraf:
        {
          vDir = Vector3.Cross(m_LookVector, m_UpVector);
          vDir.Normalize();
          vDir *= 0.75f;
          break;
        }
        case eDir.ceUp:
        {
          vDir = m_UpVector;
          vDir.Normalize();
          break;
        }
      }

      if(fDistance > 0)
        m_TranslateCoastVector = vDir;
      else
        m_TranslateCoastVector = -vDir;

      //fDistance is now just a 1 or -1 to indicate direction
      m_PositionVector += vDir*(fDistance*TranslationControl.Speed*m_dt);
      //Trace.WriteLine(string.Format("speed= {0}  dt = {1}", TranslationControl.Speed, m_dt));

      m_bNeedUpdated = true;
    }
    public void UpdateCoasting()
    {
      Vector3 tmp = new Vector3();
      if(TranslationControl.IsCoasting)
      {
        tmp = m_TranslateCoastVector*0.1f*TranslationControl.Speed;
        m_PositionVector += tmp;
        m_bNeedUpdated = true;
      }
    }
    private void ApplyRotate(float fAngle, eOrient oeOrient)
    {
      fAngle *= (m_fRPM / 60); // angle * per minute rotation

      switch(oeOrient)
      {
        case eOrient.cePitch:
        {
          Vector3 Axis = Vector3.Cross(m_LookVector, m_UpVector);
          RotateCamera(fAngle, Axis);
          break;
        }
        case eOrient.ceRoll:
        {
          break;
        }
        case eOrient.ceYaw:
        {
          Vector3 Axis = new Vector3(0,0,1);
          RotateCamera(fAngle, Axis);
          break;
        }
      }

      m_bNeedUpdated = true;
    }
    private void RotateCamera(float Angle, Vector3 axis)
    {
      Quaternion conj = new Quaternion();
      Quaternion temp = new Quaternion();
      Quaternion quat_view = new Quaternion();
      Quaternion result = new Quaternion();

      temp.X = axis.X * (float)Math.Sin(Angle/2);
      temp.Y = axis.Y * (float)Math.Sin(Angle/2);
      temp.Z = axis.Z * (float)Math.Sin(Angle/2);
      temp.W = (float)Math.Cos(Angle/2);
      temp.Normalize();

      quat_view.X = m_LookVector.X;
      quat_view.Y = m_LookVector.Y;
      quat_view.Z = m_LookVector.Z;
      quat_view.W = 0;
      //normalize?

      conj = temp;
      conj.Invert();

      temp.Multiply(quat_view);
      temp.Multiply(conj);

      m_LookVector.X = temp.X;
      m_LookVector.Y = temp.Y;
      m_LookVector.Z = temp.Z;
    }
    public void UpdateCameraByCentroid(BOUNDING_BOX bb)
    {
      Vector3 centroid = new Vector3();
      bb.GetCentroid(out centroid.X, out centroid.Y, out centroid.Z);

      SetLookAt(new Vector3(centroid.X - 10, centroid.Y - 10, centroid.Z + 4), centroid);
    }
    public void UpdateCameraByBoundingBox(ref BOUNDING_BOX bb, float scale, float speed)
    {
      float tx,ty,tz;
      float fx,fy,fz;
      float sizex, sizey, sizez, maxdim;
      
      //calculate centroid for the "to" vector
      bb.GetCenter(out tx, out ty, out tz);

      //calculate appropriate "from" vector
      sizex = bb.max[0] - bb.min[0];
      sizey = bb.max[1] - bb.min[1];
      sizez = bb.max[2] - bb.min[2];

      maxdim = sizex;
      if(sizey > maxdim)
        maxdim = sizey;
      if(sizez > maxdim)
        maxdim = sizez;

      fx = scale*maxdim;
      fy = ty;
      fz = tz;

      //SpeedControl.m_fSpeed = speed;

      SetLookAt(new Vector3(fx, fy, fz), new Vector3(tx,ty,tz));
    }
    /// <summary>
    /// Calculates the distance between the camera and the specified point.
    /// </summary>
    public float GetObjectDistance(float x, float y, float z)
    {
      //todo: optimize by using squared dist for LOD cutoff
      float dx = m_PositionVector.X - x;
      float dy = m_PositionVector.Y - y;
      float dz = m_PositionVector.Z - z;
      float dist = (float)Math.Sqrt(dx*dx + dy*dy + dz*dz);

      return(dist);
    }
    public void GetFrustumPlanes(int StartX, int StartY, int StopX, int StopY, out Plane[] FrustumPlanes)
    {
      FrustumPlanes = new Plane[6];
      Vector3 temp = new Vector3();
      Vector3 direction = new Vector3();
      Vector3 plane_norm = new Vector3();
      Vector3 pt = new Vector3();

      //make sure StartX < StartY, StopX < StopY
      if(StartY > StopY)
      {
        int tmp = StopY;
        StopY = StartY;
        StartY = tmp;
      }

      if(StartX > StopX)
      {
        int tmp = StopX;
        StopX = StartX;
        StartX = tmp;
      }

      for(int i=0; i<6; i++)
        FrustumPlanes[i] = new Plane();

      //near
      FrustumPlanes[0].A = LookVector.X;
      FrustumPlanes[0].B = LookVector.Y;
      FrustumPlanes[0].C = LookVector.Z;
      FrustumPlanes[0].Normalize();
      pt.X = m_PositionVector.X + m_LookVector.X*0.1f;
      pt.Y = m_PositionVector.Y + m_LookVector.Y*0.1f;
      pt.Z = m_PositionVector.Z + m_LookVector.Z*0.1f;
      CalculatePlaneDist(ref FrustumPlanes[0], pt);

      //far
      FrustumPlanes[1].A = -LookVector.X;
      FrustumPlanes[1].B = -LookVector.Y;
      FrustumPlanes[1].C = -LookVector.Z;
      FrustumPlanes[1].Normalize();
      pt.X = m_PositionVector.X + m_LookVector.X*30f;
      pt.Y = m_PositionVector.Y + m_LookVector.Y*30;
      pt.Z = m_PositionVector.Z + m_LookVector.Z*30f;
      CalculatePlaneDist(ref FrustumPlanes[1], pt);

      //i think the cross between the direction and up vectors will give the plane normal
      //but that may only be for a cases where the look vector is right in the center of the
      //screen

      //right
      MdxRender.CalculatePickRayWorld(StartX, 0, out direction, out temp);
      plane_norm = Vector3.Cross(direction, this.m_UpVector);
      plane_norm.Normalize();
      FrustumPlanes[2].A = plane_norm.X;
      FrustumPlanes[2].B = plane_norm.Y;
      FrustumPlanes[2].C = plane_norm.Z;
      CalculatePlaneDist(ref FrustumPlanes[2], m_PositionVector);

      //left
      MdxRender.CalculatePickRayWorld(StopX, 0, out direction, out temp);
      plane_norm = -Vector3.Cross(direction, this.m_UpVector);
      plane_norm.Normalize();
      FrustumPlanes[3].A = plane_norm.X;
      FrustumPlanes[3].B = plane_norm.Y;
      FrustumPlanes[3].C = plane_norm.Z;
      CalculatePlaneDist(ref FrustumPlanes[3], m_PositionVector);

      Vector3 left_vec = new Vector3();
      left_vec = Vector3.Cross(this.m_UpVector, this.m_LookVector);

      //top
      MdxRender.CalculatePickRayWorld(0, StopY, out direction, out temp);
      plane_norm = Vector3.Cross(direction, left_vec);
      plane_norm.Normalize();
      FrustumPlanes[4].A = plane_norm.X;
      FrustumPlanes[4].B = plane_norm.Y;
      FrustumPlanes[4].C = plane_norm.Z;
      CalculatePlaneDist(ref FrustumPlanes[4], m_PositionVector);

      //bottom
      MdxRender.CalculatePickRayWorld(0, StartY, out direction, out temp);
      plane_norm = -Vector3.Cross(direction, left_vec);
      plane_norm.Normalize();
      FrustumPlanes[5].A = plane_norm.X;
      FrustumPlanes[5].B = plane_norm.Y;
      FrustumPlanes[5].C = plane_norm.Z;
      CalculatePlaneDist(ref FrustumPlanes[5], m_PositionVector);
    }
    private void CalculatePlaneDist(ref Plane p, Vector3 PointOnPlane)
    {
      p.Normalize();
      p.D = -(p.A*PointOnPlane.X + p.B*PointOnPlane.Y + p.C*PointOnPlane.Z);
    }

    FrustumPlane[] frustumPlanes = new FrustumPlane[6];
    public void CalculateViewFrustum()
    {
      // Get combined matrix
      Matrix matrixComb = Matrix.Multiply(m_matView, MdxRender.CurrentProjectionMatrix);

      // Left clipping plane
      frustumPlanes[0] = new FrustumPlane();
      frustumPlanes[0].Normal = new Vector3(matrixComb.M14 + matrixComb.M11,
        matrixComb.M24 + matrixComb.M21,
        matrixComb.M34 + matrixComb.M31);
      frustumPlanes[0].Distance = matrixComb.M44 + matrixComb.M41;

      // Right clipping plane
      frustumPlanes[1] = new FrustumPlane();
      frustumPlanes[1].Normal = new Vector3(matrixComb.M14 - matrixComb.M11,
        matrixComb.M24 - matrixComb.M21,
        matrixComb.M34 - matrixComb.M31);
      frustumPlanes[1].Distance = matrixComb.M44 - matrixComb.M41;

      // Top clipping plane
      frustumPlanes[2] = new FrustumPlane();
      frustumPlanes[2].Normal = new Vector3(matrixComb.M14 - matrixComb.M12,
        matrixComb.M24 - matrixComb.M22,
        matrixComb.M34 - matrixComb.M32);
      frustumPlanes[2].Distance = matrixComb.M44 - matrixComb.M42;

      // Bottom clipping plane
      frustumPlanes[3] = new FrustumPlane();
      frustumPlanes[3].Normal = new Vector3(matrixComb.M14 + matrixComb.M12,
        matrixComb.M24 + matrixComb.M23,
        matrixComb.M34 + matrixComb.M33);
      frustumPlanes[3].Distance = matrixComb.M44 + matrixComb.M42;

      // Near clipping plane
      frustumPlanes[4] = new FrustumPlane();
      frustumPlanes[4].Normal = new Vector3(matrixComb.M13, matrixComb.M23, matrixComb.M33);
      frustumPlanes[4].Distance = matrixComb.M43;

      // Far clipping plane
      frustumPlanes[5] = new FrustumPlane();
      frustumPlanes[5].Normal = new Vector3(matrixComb.M14 - matrixComb.M13,
        matrixComb.M23 - matrixComb.M21,
        matrixComb.M33 - matrixComb.M31);
      frustumPlanes[5].Distance = matrixComb.M44 - matrixComb.M43;
    }

    /// <summary>
    /// Taking an AABB min and max in world space, work out its interaction with the view frustum.
    /// Note: the viewing frustum must be calculated first
    /// </summary>
    /// <returns>0 is outside, 1 is partially in, 2 is completely within.</returns>
    public int CullAABB(Vector3 aabbMin, Vector3 aabbMax)
    {
      bool intersect = false;
      int result=0;
      Vector3 minExtreme, maxExtreme;

      for (int i=0;i<6;i++)  
      {
        if (frustumPlanes[i].Normal.X <= 0)
        {
          minExtreme.X = aabbMin.X;
          maxExtreme.X = aabbMax.X;
        }
        else
        {
          minExtreme.X = aabbMax.X;
          maxExtreme.X = aabbMin.X;
        }

        if (frustumPlanes[i].Normal.Y <= 0)
        {
          minExtreme.Y = aabbMin.Y;
          maxExtreme.Y = aabbMax.Y;
        }
        else
        {
          minExtreme.Y = aabbMax.Y;
          maxExtreme.Y = aabbMin.Y;
        }

        if (frustumPlanes[i].Normal.Z <= 0)
        {
          minExtreme.Z = aabbMin.Z;
          maxExtreme.Z = aabbMax.Z;
        }
        else
        {
          minExtreme.Z = aabbMax.Z;
          maxExtreme.Z = aabbMin.Z;
        }
            
        if (frustumPlanes[i].DistanceToPoint(minExtreme) > 0)
        {
          result = 0;
          return result;
        }

        if (frustumPlanes[i].DistanceToPoint(maxExtreme) >= 0)
          intersect = true;
      }

      if (intersect)
        result = 1;
      else
        result = 2;

      return result;
    }
  }

  public class FrustumPlane
  {
    private Vector3 normal;
    private float distance;

    public Vector3 Normal
    {
      get { return normal; }
      set
      {
        normal = value;
        Normalize();
      }
    }

    public float Distance
    {
      get { return distance; }
      set { distance = value; }
    }

    private void Normalize()
    {
      float denom = (float)(1 / Math.Sqrt((normal.X*normal.X) + (normal.Y*normal.Y) + (normal.Z*normal.Z)));
      normal.X = normal.X * denom;
      normal.Y = normal.Y * denom;
      normal.Z = normal.Z * denom;
      distance = distance * denom;
    }

    public float DistanceToPoint(Vector3 point)
    {
      float value = Vector3.Dot(normal, point) + distance;
      return value;
    }
  }
}
