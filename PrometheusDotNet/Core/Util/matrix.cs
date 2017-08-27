using System;
using Prometheus.Core.Render;

namespace Prometheus.Core.Util
{
  /// <summary>
  /// Summary description for Matrix.
  /// </summary>
  public class Matrix
  {
    private enum MI
    {
      M11 = 0,
      M21 = 1,
      M31 = 2,
      M41 = 3,
      M12 = 4,
      M22 = 5,
      M32 = 6,
      M42 = 7,
      M13 = 8,
      M23 = 9,
      M33 = 10,
      M43 = 11,
      M14 = 12,
      M24 = 13,
      M34 = 14,
      M44 = 15
    }

    public float[] m_matrix;

    public Matrix()
    {
      m_matrix = new float[16];
      loadIdentity();
    }

    public void loadIdentity()
    {
      for(int i=0; i<m_matrix.GetLength(0); i++)
        m_matrix[i] = 0;
      m_matrix[0] = m_matrix[5] = m_matrix[10] = m_matrix[15] = 1;
    }

    public void set(float[] array_in)
    {
      for(int i=0; i<m_matrix.GetLength(0); i++)
        m_matrix[i] = array_in[i];
    }

    public void postMultiply(ref Matrix matrix)
    {
      float[] newMatrix = new float[16];
      float[] m1 = m_matrix, m2 = matrix.m_matrix;

      newMatrix[0] = m1[0]*m2[0] + m1[4]*m2[1] + m1[8]*m2[2];
      newMatrix[1] = m1[1]*m2[0] + m1[5]*m2[1] + m1[9]*m2[2];
      newMatrix[2] = m1[2]*m2[0] + m1[6]*m2[1] + m1[10]*m2[2];
      newMatrix[3] = 0;

      newMatrix[4] = m1[0]*m2[4] + m1[4]*m2[5] + m1[8]*m2[6];
      newMatrix[5] = m1[1]*m2[4] + m1[5]*m2[5] + m1[9]*m2[6];
      newMatrix[6] = m1[2]*m2[4] + m1[6]*m2[5] + m1[10]*m2[6];
      newMatrix[7] = 0;

      newMatrix[8] = m1[0]*m2[8] + m1[4]*m2[9] + m1[8]*m2[10];
      newMatrix[9] = m1[1]*m2[8] + m1[5]*m2[9] + m1[9]*m2[10];
      newMatrix[10] = m1[2]*m2[8] + m1[6]*m2[9] + m1[10]*m2[10];
      newMatrix[11] = 0;

      newMatrix[12] = m1[0]*m2[12] + m1[4]*m2[13] + m1[8]*m2[14] + m1[12];
      newMatrix[13] = m1[1]*m2[12] + m1[5]*m2[13] + m1[9]*m2[14] + m1[13];
      newMatrix[14] = m1[2]*m2[12] + m1[6]*m2[13] + m1[10]*m2[14] + m1[14];
      newMatrix[15] = 1;

      set(newMatrix);
    }

    public void setTranslation(float[] translation)
    {
      m_matrix[12] = translation[0];
      m_matrix[13] = translation[1];
      m_matrix[14] = translation[2];
    }

    public void setInverseTranslation(float[] translation)
    {
      m_matrix[12] = -translation[0];
      m_matrix[13] = -translation[1];
      m_matrix[14] = -translation[2];
    }

    public void setRotationDegrees(float[] angles)
    {
      float[] vec = new float[3];
      vec[0] = (float)(angles[0]*180.0/Math.PI);
      vec[1] = (float)(angles[1]*180.0/Math.PI);
      vec[2] = (float)(angles[2]*180.0/Math.PI);
      setRotationRadians(vec);
    }

    public void inverseRotateVect(ref float x, ref float y, ref float z)
    {
      float[] temp = new float[3];

      temp[0] = x*m_matrix[0]+y*m_matrix[1]+z*m_matrix[2];
      temp[1] = x*m_matrix[4]+y*m_matrix[5]+z*m_matrix[6];
      temp[2] = x*m_matrix[8]+y*m_matrix[9]+z*m_matrix[10];

      x = temp[0];
      y = temp[1];
      z = temp[2];
    }

    public void inverseTranslateVect(float[] Vector)
    {
      Vector[0] = Vector[0]-m_matrix[12];
      Vector[1] = Vector[1]-m_matrix[13];
      Vector[2] = Vector[2]-m_matrix[14];
    }
    public void TranslateVect(ref float x, ref float y, ref float z)
    {
      x = x + m_matrix[12];
      y = y + m_matrix[13];
      z = z + m_matrix[14];
    }
    public void setInverseRotationDegrees(float[] angles)
    {
      float[] vec = new float[3];
      vec[0] = (float)(angles[0]*180.0/Math.PI);
      vec[1] = (float)(angles[1]*180.0/Math.PI);
      vec[2] = (float)(angles[2]*180.0/Math.PI);
      setInverseRotationRadians(vec);
    }

    void setRotationRadians(float[] angles)
    {
      double cr = Math.Cos( angles[0] );
      double sr = Math.Sin( angles[0] );
      double cp = Math.Cos( angles[1] );
      double sp = Math.Sin( angles[1] );
      double cy = Math.Cos( angles[2] );
      double sy = Math.Sin( angles[2] );

      m_matrix[0] = ( float )( cp*cy );
      m_matrix[1] = ( float )( cp*sy );
      m_matrix[2] = ( float )( -sp );

      double srsp = sr*sp;
      double crsp = cr*sp;

      m_matrix[4] = ( float )( srsp*cy-cr*sy );
      m_matrix[5] = ( float )( srsp*sy+cr*cy );
      m_matrix[6] = ( float )( sr*cp );

      m_matrix[8] = ( float )( crsp*cy+sr*sy );
      m_matrix[9] = ( float )( crsp*sy-sr*cy );
      m_matrix[10] = ( float )( cr*cp );
    }

    public void setInverseRotationRadians(float[] angles)
    {
      double cr = Math.Cos( angles[0] );
      double sr = Math.Sin( angles[0] );
      double cp = Math.Cos( angles[1] );
      double sp = Math.Sin( angles[1] );
      double cy = Math.Cos( angles[2] );
      double sy = Math.Sin( angles[2] );

      m_matrix[0] = ( float )( cp*cy );
      m_matrix[4] = ( float )( cp*sy );
      m_matrix[8] = ( float )( -sp );

      double srsp = sr*sp;
      double crsp = cr*sp;

      m_matrix[1] = ( float )( srsp*cy-cr*sy );
      m_matrix[5] = ( float )( srsp*sy+cr*cy );
      m_matrix[9] = ( float )( sr*cp );

      m_matrix[2] = ( float )( crsp*cy+sr*sy );
      m_matrix[6] = ( float )( crsp*sy-sr*cy );
      m_matrix[10] = ( float )( cr*cp );
    }

    public void setRotationQuaternion(float[] quat)
    {
      m_matrix[0] = ( float )( 1.0 - 2.0*quat[1]*quat[1] - 2.0*quat[2]*quat[2] );
      m_matrix[1] = ( float )( 2.0*quat[0]*quat[1] + 2.0*quat[3]*quat[2] );
      m_matrix[2] = ( float )( 2.0*quat[0]*quat[2] - 2.0*quat[3]*quat[1] );

      m_matrix[4] = ( float )( 2.0*quat[0]*quat[1] - 2.0*quat[3]*quat[2] );
      m_matrix[5] = ( float )( 1.0 - 2.0*quat[0]*quat[0] - 2.0*quat[2]*quat[2] );
      m_matrix[6] = ( float )( 2.0*quat[1]*quat[2] + 2.0*quat[3]*quat[0] );

      m_matrix[8] = ( float )( 2.0*quat[0]*quat[2] + 2.0*quat[3]*quat[1] );
      m_matrix[9] = ( float )( 2.0*quat[1]*quat[2] - 2.0*quat[3]*quat[0] );
      m_matrix[10] = ( float )( 1.0 - 2.0*quat[0]*quat[0] - 2.0*quat[1]*quat[1] );
    }

    public void ToAxisAngle(float[] axis, out float rfRadians)
    {
      // Let (x,y,z) be the unit-length axis and let A be an angle of rotation.
      // The rotation matrix is R = I + sin(A)*P + (1-cos(A))*P^2 where
      // I is the identity and
      //
      //       +-        -+
      //   P = |  0 -z +y |
      //       | +z  0 -x |
      //       | -y +x  0 |
      //       +-        -+
      //
      // If A > 0, R represents a counterclockwise rotation about the axis in
      // the sense of looking from the tip of the axis vector towards the
      // origin.  Some algebra will show that
      //
      //   cos(A) = (trace(R)-1)/2  and  R - R^t = 2*sin(A)*P
      //
      // In the event that A = pi, R-R^t = 0 which prevents us from extracting
      // the axis through P.  Instead note that R = I+2*P^2 when A = pi, so
      // P^2 = (R-I)/2.  The diagonal entries of P^2 are x^2-1, y^2-1, and
      // z^2-1.  We can solve these for axis (x,y,z).  Because the angle is pi,
      // it does not matter which sign you choose on the square roots.

      //float fTrace = m_aafEntry[0][0] + m_aafEntry[1][1] + m_aafEntry[2][2];
      float fTrace = m_matrix[(int)MI.M11] + m_matrix[(int)(int)MI.M22] + m_matrix[(int)(int)MI.M33];
      float fCos = (float)0.5f*(fTrace-1.0f);
      rfRadians = (float)Math.Acos(fCos);  // in [0,PI]

      if(rfRadians > 0.0)
      {
        if(rfRadians < Math.PI)
        {
          axis[0] = m_matrix[(int)(int)MI.M32]-m_matrix[(int)MI.M23];
          axis[1] = m_matrix[(int)MI.M13]-m_matrix[(int)MI.M31];
          axis[2] = m_matrix[(int)MI.M21]-m_matrix[(int)MI.M12];

          float mag = (float)Math.Sqrt(axis[0]*axis[0] + axis[1]*axis[1] + axis[2]*axis[2]);
          axis[0] /= mag;
          axis[1] /= mag;
          axis[2] /= mag;
        }
        else
        {
          // angle is PI
          float fHalfInverse;
          if(m_matrix[(int)MI.M11] >= m_matrix[(int)MI.M22])
          {
            // r00 >= r11
            if(m_matrix[(int)MI.M11] >= m_matrix[(int)MI.M33])
            {
              // r00 is maximum diagonal term
              axis[0] = 0.5f*(float)Math.Sqrt(m_matrix[(int)MI.M11] -
                m_matrix[(int)MI.M22] - m_matrix[(int)MI.M33] + 1.0);
              fHalfInverse = 0.5f/axis[0];
              axis[1] = fHalfInverse*m_matrix[(int)MI.M12];
              axis[2] = fHalfInverse*m_matrix[(int)MI.M13];
            }
            else
            {
              // r22 is maximum diagonal term
              axis[2] = 0.5f*(float)Math.Sqrt(m_matrix[(int)MI.M33] -
                m_matrix[(int)MI.M11] - m_matrix[(int)MI.M22] + 1.0);
              fHalfInverse = 0.5f/axis[2];
              axis[0] = fHalfInverse*m_matrix[(int)MI.M13];
              axis[1] = fHalfInverse*m_matrix[(int)MI.M23];
            }
          }
          else
          {
            // r11 > r00
            if(m_matrix[(int)MI.M22] >= m_matrix[(int)MI.M33])
            {
              // r11 is maximum diagonal term
              axis[1] = 0.5f*(float)Math.Sqrt(m_matrix[(int)MI.M22] -
                m_matrix[(int)MI.M11] - m_matrix[(int)MI.M33] + 1.0);
              fHalfInverse  = 0.5f/axis[1];
              axis[0] = fHalfInverse*m_matrix[(int)MI.M12];
              axis[2] = fHalfInverse*m_matrix[(int)MI.M23];
            }
            else
            {
              // r22 is maximum diagonal term
              axis[2] = 0.5f*(float)Math.Sqrt(m_matrix[(int)MI.M33] -
                m_matrix[(int)MI.M11] - m_matrix[(int)MI.M22] + 1.0);
              fHalfInverse = 0.5f/axis[2];
              axis[0] = fHalfInverse*m_matrix[(int)MI.M13];
              axis[1] = fHalfInverse*m_matrix[(int)MI.M23];
            }
          }
        }
      }
      else
      {
        // The angle is 0 and the matrix is the identity.  Any axis will
        // work, so just use the x-axis.
        axis[0] = 1.0f;
        axis[1] = 0.0f;
        axis[2] = 0.0f;
      }
    }
  }
}
