using System;

namespace Prometheus.Core.Util
{
	/// <summary>
	/// Summary description for vector.
	/// </summary>
	public class Vector
	{
    public float [] m_vector;
		
    public Vector()
		{
      m_vector = new float[4];
      reset();
		}

    public void reset()
    {
      m_vector[0] = m_vector[1] = m_vector[2] = 0;
      m_vector[3] = 1;
    }

    public void load(float x, float y, float z)
    {
      m_vector[0] = x;
      m_vector[1] = y;
      m_vector[2] = z;
    }

    public void add(ref Vector v)
    {
      m_vector[0] += v.m_vector[0];
      m_vector[1] += v.m_vector[1];
      m_vector[2] += v.m_vector[2];
      m_vector[3] += v.m_vector[3];
    }

    public void normalize()
    {
      float len = length();

      m_vector[0] /= len;
      m_vector[1] /= len;
      m_vector[2] /= len;
    }

    public float length()
    {
      return ( float )Math.Sqrt(m_vector[0]*m_vector[0]+m_vector[1]*m_vector[1]+m_vector[2]*m_vector[2]);
    }


    /// <summary>
    /// Transforms the vector by an input 4x4 matrix (rotation and translation).
    /// This code was hacked to make halo animations work with OGL, 
    /// not sure if it will work with DX.
    /// </summary>
    public void transform(ref float [] matrix)
    {
      double [] vector = new double[4];

      //const float *matrix = m.getMatrix();
      vector[0] = m_vector[0]*matrix[0]+m_vector[1]*matrix[4]+m_vector[2]*matrix[8]-matrix[12];
      vector[1] = m_vector[0]*matrix[1]+m_vector[1]*matrix[5]+m_vector[2]*matrix[9]-matrix[13];
      vector[2] = m_vector[0]*matrix[2]+m_vector[1]*matrix[6]+m_vector[2]*matrix[10]-matrix[14];
      vector[3] = m_vector[0]*matrix[3]+m_vector[1]*matrix[7]+m_vector[2]*matrix[11]+matrix[15];

      m_vector[0] = ( float )( vector[0] );
      m_vector[1] = ( float )( vector[1] );
      m_vector[2] = ( float )( vector[2] );
      m_vector[3] = ( float )( vector[3] );
    }

    /// <summary>
    /// transforms the vector by an input 4x4 matrix (rotation only).
    /// </summary>
    public void transform3(ref float [] matrix)
    {
      double [] vector = new double[3];
      //const float *matrix = m.getMatrix();

      vector[0] = m_vector[0]*matrix[0]+m_vector[1]*matrix[4]+m_vector[2]*matrix[8];
      vector[1] = m_vector[0]*matrix[1]+m_vector[1]*matrix[5]+m_vector[2]*matrix[9];
      vector[2] = m_vector[0]*matrix[2]+m_vector[1]*matrix[6]+m_vector[2]*matrix[10];

      m_vector[0] = ( float )( vector[0] );
      m_vector[1] = ( float )( vector[1] );
      m_vector[2] = ( float )( vector[2] );
      m_vector[3] = 1;
    }
	}
}
