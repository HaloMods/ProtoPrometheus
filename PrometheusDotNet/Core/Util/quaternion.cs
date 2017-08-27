using System;

namespace Prometheus.Core.Util
{
	/// <summary>
	/// Summary description for quaternion.
	/// </summary>
  public class Quaternion
  {
    public float [] m_quat;
    private float RAD_TO_DEG;

    public Quaternion()
    {
      m_quat = new float[4];
      RAD_TO_DEG = 57.2957795f;
    }

    public void Load(float [] pQuat)
    {
      m_quat[0] = -pQuat[0];
      m_quat[1] = -pQuat[1];
      m_quat[2] = -pQuat[2];
      m_quat[3] = pQuat[3];
    }

    public void inverse()
    {
      m_quat[0] = -m_quat[0];
      m_quat[1] = -m_quat[1];
      m_quat[2] = -m_quat[2];
      m_quat[3] = -m_quat[3];
    }
    public void conjugate()
    {
      m_quat[0] = -m_quat[0];
      m_quat[1] = -m_quat[1];
      m_quat[2] = -m_quat[2];
      m_quat[3] = m_quat[3];
    }
    public void fromAngles(float [] angles)
    {
      float angle;
      double sr, sp, sy, cr, cp, cy;

      angle = angles[2]*0.5f;
      sy = Math.Sin(angle);
      cy = Math.Cos( angle );
      angle = angles[1]*0.5f;
      sp = Math.Sin( angle );
      cp = Math.Cos( angle );
      angle = angles[0]*0.5f;
      sr = Math.Sin( angle );
      cr = Math.Cos( angle );

      double crcp = cr*cp;
      double srsp = sr*sp;

      m_quat[0] = ( float )( sr*cp*cy-cr*sp*sy );
      m_quat[1] = ( float )( cr*sp*cy+sr*cp*sy );
      m_quat[2] = ( float )( crcp*sy-srsp*cy );
      m_quat[3] = ( float )( crcp*cy+srsp*sy ); 
    }
    public void lerp(ref Quaternion q1, ref Quaternion q2, float interp)
    {
      Quaternion start = new Quaternion();
      start.Load(q1.m_quat);
      start.conjugate();

      float cosOmega = start.m_quat[0]*q2.m_quat[0] +
        start.m_quat[1]*q2.m_quat[1] +
        start.m_quat[2]*q2.m_quat[2] +
        start.m_quat[3]*q2.m_quat[3];

      if(cosOmega < 0)
      {
        cosOmega *= -1;
        start.inverse();
      }

      if(Math.Abs(1+cosOmega) < 1.0e-6)
      {
        float tx = interp*start.m_quat[0];
        float ty = interp*start.m_quat[1];
        float tz = interp*start.m_quat[2];
        float tw = interp*start.m_quat[3];

        m_quat[0] = start.m_quat[0] - tx - ty;
        m_quat[1] = start.m_quat[1] - ty + tx;
        m_quat[2] = start.m_quat[2] - tz - tw;
        m_quat[3] = start.m_quat[2];
      }
      else
      {
        m_quat[0] = start.m_quat[0] + interp*(q2.m_quat[0] - start.m_quat[0]);
        m_quat[1] = start.m_quat[1] + interp*(q2.m_quat[1] - start.m_quat[1]);
        m_quat[2] = start.m_quat[2] + interp*(q2.m_quat[2] - start.m_quat[2]);
        m_quat[3] = start.m_quat[3] + interp*(q2.m_quat[3] - start.m_quat[3]);
      }
    }

/*
	if (IsZero( 1 + cosOmega )) 
	{
		// Ends nearly opposite
		float tx = t * copyStartRseQuat.x;
		float ty = t * copyStartRseQuat.y;
		float tz = t * copyStartRseQuat.z;
		float tw = t * copyStartRseQuat.w;

		return RSQuat( copyStartRseQuat.x - tx - ty,
					 copyStartRseQuat.y - ty + tx,
					 copyStartRseQuat.z - tz - tw,
					 copyStartRseQuat.z );

	}
	else 
	{
		return RSQuat( copyStartRseQuat.x + t * (endRseQuat.x - copyStartRseQuat.x) , 
					 copyStartRseQuat.y + t * (endRseQuat.y - copyStartRseQuat.y), 
					 copyStartRseQuat.z + t * (endRseQuat.z - copyStartRseQuat.z), 
					 copyStartRseQuat.w + t * (endRseQuat.w - copyStartRseQuat.w) ); 
	}

}  // End of Lerp()
 */

    public void slerp(ref Quaternion q1, ref Quaternion q2, float interp)
    {
      // Decide if one of the quaternions is backwards
      int i;
      float a = 0, b = 0;
      for ( i = 0; i < 4; i++ )
      {
        a += ( q1.m_quat[i]-q2.m_quat[i] )*( q1.m_quat[i]-q2.m_quat[i] );
        b += ( q1.m_quat[i]+q2.m_quat[i] )*( q1.m_quat[i]+q2.m_quat[i] );
      }
      if ( a > b )
        q2.inverse();

      float cosom = q1.m_quat[0]*q2.m_quat[0] +
        q1.m_quat[1]*q2.m_quat[1] +
        q1.m_quat[2]*q2.m_quat[2] +
        q1.m_quat[3]*q2.m_quat[3];
      double sclq1, sclq2;

      if (( 1.0+cosom ) > 0.00000001 )
      {
        if (( 1.0-cosom ) > 0.00000001 )
        {
          double omega = Math.Acos(cosom);
          double sinom = Math.Sin(omega);
          sclq1 = Math.Sin((1.0-interp)*omega )/sinom;
          sclq2 = Math.Sin(interp*omega)/sinom;
        }
        else
        {
          sclq1 = 1.0-interp;
          sclq2 = interp;
        }
        for ( i = 0; i < 4; i++ )
          m_quat[i] = ( float )( sclq1*q1.m_quat[i]+sclq2*q2.m_quat[i] );
      }
      else
      {
        m_quat[0] = -q1.m_quat[1];
        m_quat[1] = q1.m_quat[0];
        m_quat[2] = -q1.m_quat[3];
        m_quat[3] = q1.m_quat[2];

        sclq1 = Math.Sin(( 1.0-interp )*0.5*Math.PI );
        sclq2 = Math.Sin( interp*0.5*Math.PI );
        for ( i = 0; i < 3; i++ )
          m_quat[i] = ( float )( sclq1*q1.m_quat[i]+sclq2*m_quat[i] );
      }
    }

    public void ToAxisAngle(ref float [] pXYZ, ref float pAngleDeg)
    {
      // The quaternion representing the rotation is
      //   q = cos(A/2)+sin(A/2)*(x*i+y*j+z*k)

      double SqrLength = m_quat[0]*m_quat[0] +
        m_quat[1]*m_quat[1] +
        m_quat[2]*m_quat[2];
      if(SqrLength > 0.0)
      {
        pAngleDeg = (float)(RAD_TO_DEG*2.0*Math.Acos(m_quat[3]));
        float fInvLength = (float)(1.0/Math.Sqrt(SqrLength));
        pXYZ[0] = m_quat[0]*fInvLength;
        pXYZ[1] = m_quat[1]*fInvLength;
        pXYZ[2] = m_quat[2]*fInvLength;
      }
      else
      {
        // angle is 0 (mod 2*pi), so any axis will do
        pAngleDeg = 0.0F;
        pXYZ[0] = 1.0F;
        pXYZ[1] = 0.0F;
        pXYZ[2] = 0.0F;
      }
    }

    public static void DecompressQuaternion_64bit(short[] Input, float[] Output)
    {
      Output[0] = Input[0] / 32767.0f;
      Output[1] = Input[1] / 32767.0f;
      Output[2] = Input[2] / 32767.0f;
      Output[3] = Input[3] / 32767.0f;
    }

    public static void NormalizeQuaternion(float[] quat)
    {
      float mag = quat[0]*quat[0] + quat[1]*quat[1] + quat[2]*quat[2] + quat[3]*quat[3];
      mag = (float)Math.Sqrt(mag);
      quat[0] /= mag;
      quat[1] /= mag;
      quat[2] /= mag;
      quat[3] /= mag;
    }

    public static void CompressQuaternion_64bit(float[] Input, ushort[] Output)
    {
      Output[0] = (ushort)(Input[0] * 32767.0f);
      Output[1] = (ushort)(Input[1] * 32767.0f);
      Output[2] = (ushort)(Input[2] * 32767.0f);
      Output[3] = (ushort)(Input[3] * 32767.0f);
    }
  }
}
