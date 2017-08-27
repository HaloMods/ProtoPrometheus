/* ---------------------------------------------------------------
 * Prometheus
 * Bungie Map-based Multi-Game Editing Kit
 * 2004-2005, Halo-Dev
 * ---------------------------------------------------------------
 * Class       : Core.Utility
 * Description : Provides useful static functions and helper
 *               classes.
 * Author      : MonoxideC
 * Co-Authors  : 
 * ---------------------------------------------------------------
 */

using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Prometheus.Core.Tags;

namespace Prometheus.Core
{
	/// <summary>
	/// Summary description for Utility.
	/// </summary>
  public class Utility
  {
    /// <summary>
    /// Read a C-style null-terminated string of characters from a file.
    /// </summary>
    /// <param name="br">The BinaryReader object to read from.</param>
    /// <returns></returns>
    public static string ReadCString(ref BinaryReader br)
    {
      string s = String.Empty;
      do 
      {
        char c = br.ReadChar();
        if (c == '\0') break;
        s += c.ToString();
      } while (true);
      return s;
    }
		
    /// <summary>
    /// Initialize all of the Trace Listeners
    /// </summary>
    public static void InitTraceListeners()
    {
      // In the event that we add more trace destinations, they can be added
      // here so that they are initialized automatically from the
      // ProemtheusException logging function.
    }

    /// <summary>
    /// Provides a more abstracted means of accessing XML document data.
    /// </summary>
    public class XmlHelper
    {
      private XmlNode m_rootNode;

      public class XmlNodeArrayList : ArrayList
      {
        // ToDo: if i'm not too lazy, make this typesafe later
      }
      public XmlHelper(ref XmlDocument xmlD)
      {
        m_rootNode = xmlD.SelectSingleNode("//*");
      }
      /// <summary>
      /// Returns a group of nodes matching 'name'.
      /// </summary>
      /// <param name="name">Name of the nodes to return.  Use "*" for all.</param>
      public XmlNodeArrayList GetGroup (string name)
      {
        return GetGroup (name, m_rootNode);
      }
      /// <summary>
      /// Returns a group of nodes matching 'name' belonging to 'baseNode'.
      /// </summary>
      /// <param name="name">Name of the nodes to return.  Use "*" for all.</param>
      /// <param name="baseNode">Base node to look under.</param>
      /// <returns></returns>
      public XmlNodeArrayList GetGroup (string name, XmlNode baseNode)
      {
        XmlNodeArrayList list = new XmlNodeArrayList();
			
        foreach (XmlNode n in baseNode)
        {
          if ((n.Name == name) || (name == "*")) list.Add(n);
        }
        return list;
      }
      /// <summary>
      /// Returns the InnerText value of a a child node matching 'name'.
      /// </summary>
      /// <param name="name">Name of the property (node) to read.</param>
      /// <param name="baseNode">Base node to look under.</param>
      /// <returns></returns>
      public string ReadProperty (string name, XmlNode baseNode)
      {
        if (baseNode.HasChildNodes == false) return "";
			
        foreach (XmlNode n in baseNode)
        {
          if (n.Name == name) return n.InnerText;
        }
        return "";
      }
    }
    public static byte[] SubArray(ref byte[] b, int offset, int length)
    {
      byte[] newArray = new byte[length];
      for (int x=0; x<length; x++)
      {
        newArray[x] = b[offset+x];
      }
      return newArray;
    }

    public static ArrayList DirSearch(string sDir, string pattern) 
    {
      ArrayList results = new ArrayList();
      foreach (string d in Directory.GetDirectories(sDir)) 
      {
        foreach (string f in Directory.GetFiles(d, pattern)) 
        {
          results.Add(f);
        }
        foreach (string n in DirSearch(d, pattern)) 
        {
          results.Add(n);
        }
      }
      return results;
    }
    public static float Swap(float value)
    {
      return BitConverter.ToSingle(SwapBytes(BitConverter.GetBytes(value)), 0);
    }
    static public void SwapCurrent4byte(long current_pos, byte[] buffer)
    {
      byte b0,b1,b2,b3;

      b0 = buffer[current_pos];
      b1 = buffer[current_pos+1];
      b2 = buffer[current_pos+2];
      b3 = buffer[current_pos+3];

      buffer[current_pos] = b3;
      buffer[current_pos+1] = b2;
      buffer[current_pos+2] = b1;
      buffer[current_pos+3] = b0;
    }
    public static int Swap(int value)
    {
      return BitConverter.ToInt32(SwapBytes(BitConverter.GetBytes(value)), 0);
    }
    public static uint Swap(uint value)
    {
      return BitConverter.ToUInt32(SwapBytes(BitConverter.GetBytes(value)), 0);
    }
    public static short Swap(short value)
    {
      return BitConverter.ToInt16(SwapBytes(BitConverter.GetBytes(value)), 0);
    }
    public static byte[] SwapBytes (byte[] bytes)
    {
      byte[] b = new byte[bytes.Length];
      for (int x=0; x<bytes.Length; x++)
      {
        b[(bytes.Length-x)-1] = bytes[x];
      }
      return b;      
    }
    static public void SwapCurrent2byte(long current_pos, byte[] buffer)
    {
      byte b0,b1;

      b0 = buffer[current_pos];
      b1 = buffer[current_pos+1];

      buffer[current_pos] = b1;
      buffer[current_pos+1] = b0;
    }
    /// <summary>
    /// Calculates point of intersection between Ray and Plane.
    /// </summary>
    /// <param name="ray_origin"></param>
    /// <param name="ray_direction"></param>
    /// <param name="plane_normal"></param>
    /// <param name="intersect">returned point of intersection.  Is (0,0,0) when return value is false.</param>
    /// <returns>true if intersection occurs, false otherwise</returns>
    static public bool RayIntersectPlane(Vector3 ray_origin, Vector3 ray_direction, Plane plane, out Vector3 intersect)
    {
      intersect.X = 0;
      intersect.Y = 0;
      intersect.Z = 0;
      bool bIntersected = false;
      float Vd, V0, t;
      Vd = Plane.DotNormal(plane, ray_direction);

      if((Vd < -0.001f)||(Vd > 0.001f)) //check for parallel to plane
      {
        V0 = -Plane.DotNormal(plane, ray_origin) + plane.D;
        t = V0/Vd;

        if(t >= 0) //intersection is not behind the ray origin
        {
          intersect.X = ray_origin.X + ray_direction.X * t;
          intersect.Y = ray_origin.Y + ray_direction.Y * t;
          intersect.Z = ray_origin.Z + ray_direction.Z * t;
          bIntersected = true;
        }
      }

      return(bIntersected);
    }

    public static string CapitalizeWords(string text)
    {
      string[] parts = text.Split(' ');
      string result = String.Empty;
      for (int x=0; x<parts.Length; x++)
      {
        string firstLetter = parts[x].Substring(0, 1);
        string remainingLetters = parts[x].Substring(1);
        result += firstLetter.ToUpper() + remainingLetters + " ";
      }
      return result.TrimEnd(' ');
    }

		private enum Region{ Right, Left, Middle };
		/// <summary>
		/// Ray-Axis Aligned Bounding Box intersection test. (Un-tested)
		/// </summary>
		/// <param name="position">Start position of ray.</param>
		/// <param name="direction">Direction of ray.</param>
		/// <returns></returns>
		public static bool RayAABBIntersect(Vector3 position, Vector3 direction, BOUNDING_BOX box)
		{
			int whichPlane;
			bool inside = true;
			const int NUMDIM =3;
			float[] coord = new float[NUMDIM]; //Point of intersection.
			float[] dir = new float[NUMDIM]{direction.X, direction.Y, direction.Z};
			float[] origin = new float[NUMDIM]{position.X, position.Y, position.Z};
			float[] minB = box.min;
			float[] maxB = box.max;
			float[] maxT = new float[NUMDIM];
			float[] candidatePlane = new float[NUMDIM];
			Region[] quadrant = new Region[NUMDIM];

			/* Find candidate planes; this loop can be avoided if
			  rays cast all from the eye(assume perpsective view) */
			for(int i=0; i<NUMDIM; i++)
			{
				if(origin[i] < minB[i]) 
				{
					quadrant[i] = Region.Left;
					candidatePlane[i] = minB[i];
					inside = false;
				}
				else if (origin[i] > maxB[i]) 
				{
					quadrant[i] = Region.Right;
					candidatePlane[i] = maxB[i];
					inside = false;
				}
				else	
				{
					quadrant[i] = Region.Middle;
				}
			}

			/* Ray origin inside bounding box */
			if(inside)	
			{
				coord = origin;
				return true;
			}

			/* Calculate T distances to candidate planes */
			for(int i=0; i<NUMDIM; i++)
			{
				if (quadrant[i] != Region.Middle && dir[i] !=0)
					maxT[i] = (candidatePlane[i]-origin[i]) / dir[i];
				else
					maxT[i] = -1;
			}

			/* Get largest of the maxT's for final choice of intersection */
			whichPlane = 0;
			for(int i=1; i<NUMDIM; i++)
			{
				if (maxT[whichPlane] < maxT[i])
					whichPlane = i;
			}

			/* Check final candidate actually inside box */
			if (maxT[whichPlane] < 0) 
				return false;

			for(int i=0; i<NUMDIM; i++)
			{
				if (whichPlane != i) 
				{
					coord[i] = origin[i] + maxT[whichPlane] *dir[i];
					if (coord[i] < minB[i] || coord[i] > maxB[i])
						return false;
				} 
				else 
				{
					coord[i] = candidatePlane[i];
				}
			}
			/* ray hits box */
			return true;
		}
    public static void TransformMesh(Mesh mesh, Microsoft.DirectX.Matrix transform)
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
    static private void swap(int[] Array, int Left, int Right) 
    {
      int temp = Array[Right];
      Array[Right] = Array[Left];
      Array[Left] = temp;
    }
    static private void swap(float[] Array, int Left, int Right) 
    {
      float temp = Array[Right];
      Array[Right] = Array[Left];
      Array[Left] = temp;
    }
    static public void sort(int[] Array, float[] CompareValues, int Left, int Right) 
    {
      int LHold = Left;
      int RHold = Right;
      Random ObjRan = new Random();
      int    Pivot  = ObjRan.Next(Left,Right);
      swap(Array,Pivot,Left);
      swap(CompareValues,Pivot,Left);
      Pivot = Left;
      Left++;

      while (Right >= Left) 
      {
        if (CompareValues[Left] >= CompareValues[Pivot] && CompareValues[Right] < CompareValues[Pivot])
        {
          swap(Array, Left, Right);
          swap(CompareValues,Left,Right);
        }
        else if (CompareValues[Left] >= CompareValues[Pivot])
          Right--;
        else if (CompareValues[Right] < CompareValues[Pivot])
          Left++;
        else 
        {
          Right--;
          Left++;
        }       
      }       
      swap(Array, Pivot, Right);
      swap(CompareValues,Pivot,Right);
      Pivot = Right;  
      if (Pivot > LHold)
      {
        sort(Array, CompareValues, LHold,   Pivot);
      }
      if (RHold > Pivot+1)
      {
        sort(Array, CompareValues, Pivot+1, RHold);
      }
      //      swap(Array,Pivot,Left);
      //      Pivot = Left;
      //      Left++;
      //
      //      while (Right >= Left) 
      //      {
      //        if (Array[Left] >= Array[Pivot] && Array[Right] < Array[Pivot])
      //          swap(Array, Left, Right);
      //        else if (Array[Left] >= Array[Pivot])
      //          Right--;
      //        else if (Array[Right] < Array[Pivot])
      //          Left++;
      //        else 
      //        {
      //          Right--;
      //          Left++;
      //        }       
      //      }       
      //      swap(Array, Pivot, Right);
      //      Pivot = Right;  
      //      if (Pivot > LHold)
      //        sort(Array, CompareValues, LHold,   Pivot);
      //      if (RHold > Pivot+1)
      //        sort(Array, CompareValues, Pivot+1, RHold);
    }
    
    [System.Security.SuppressUnmanagedCodeSecurity] 
    [System.Runtime.InteropServices.DllImport("umutil.dll", EntryPoint="CompressMapFile", CharSet = CharSet.Ansi)]
    public extern static int CompressMapFile([MarshalAs(UnmanagedType.LPStr)]string infile, [MarshalAs(UnmanagedType.LPStr)]string outfile);

    [System.Security.SuppressUnmanagedCodeSecurity] 
    [System.Runtime.InteropServices.DllImport("umutil.dll", EntryPoint="DecompressMapFile", CharSet = CharSet.Ansi)]
    public extern static int DecompressMapFile([MarshalAs(UnmanagedType.LPStr)]string infile, [MarshalAs(UnmanagedType.LPStr)]string outfile);

    //example
    //[System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "MessageBox", CharSet = CharSet.Ansi)]
    //public extern static int MessageBox(int hWnd, string lpText, string lpCaption, uint uType);
  }
  class HighResTimer
  {
    private long BeginTime;
    private long EndTime;
    private long Frequency;
    public HighResTimer()
    {
      BeginTime = 0;
      EndTime = 0;
      QueryPerformanceFrequency(ref Frequency);
    }
    public void ResetTimer()
    {
      QueryPerformanceCounter(ref BeginTime);
    }
    public double GetElapsedTime()
    {
      QueryPerformanceCounter(ref EndTime);
      double elapsed_ticks = EndTime - BeginTime;
      double delta_time = elapsed_ticks/(double)Frequency;

      return(delta_time);
    }
    [System.Security.SuppressUnmanagedCodeSecurity] 
    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    public extern static short QueryPerformanceCounter(ref long x);
    [System.Security.SuppressUnmanagedCodeSecurity] 
    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    public extern static short QueryPerformanceFrequency(ref long x);
  }

  /// <summary>
  /// Provides high-prevision timing functions, as well as a method
  /// of automatically logging frame times and calculating the
  /// average FPS value.
  /// </summary>
  public class FPSCounter
  {
    private long m_frameStart;
    private long m_frameEnd;
    private long[] times;
    private int m_currentSample;
    private long m_runningTotal;
    private uint m_totalFrames;
    private long m_tickFrequency;
    private long m_lastElapsed;

    public uint TotalFramesCounted
    {
      get { return m_totalFrames; }
    }

    /// <summary>
    /// Gets the average FPS based on a running average.
    /// </summary>
    public float FPS
    {
      get
      {
        if (m_runningTotal < 1) m_runningTotal = times.Length;
        return (m_tickFrequency/(m_runningTotal/times.Length));
      }
    }

    /// <summary>
    /// Gets the FPS value based on the time elapsed since the last frame.
    /// </summary>
    public float ExactFPS
    {
      get
      {
        if (m_lastElapsed < 1) m_lastElapsed = 1;
        return m_tickFrequency/m_lastElapsed;
      }
    }

    public FPSCounter(int totalSamples)
    {
      times = new long[totalSamples];
      for (int x=0; x<times.Length; x++)
        times[x] = 1;
      m_currentSample = 0;
      m_totalFrames = 0;
      QueryPerformanceFrequency(ref m_tickFrequency);
    }

    public void BeginFrame()
    {
      QueryPerformanceCounter(ref m_frameStart);
    }

    public void EndFrame()
    {
      QueryPerformanceCounter(ref m_frameEnd);
      long elapsed = m_frameEnd - m_frameStart;
      if (elapsed < 1) elapsed = 1; // Ensure we're not negative or DBZ
      
      // Update our running total.
      m_runningTotal -= times[m_currentSample]; 
      times[m_currentSample] = elapsed;
      m_runningTotal += times[m_currentSample];

      // Store elapsed value for exact FPS calculations;
      m_lastElapsed = elapsed;

      m_currentSample++;
      m_totalFrames++;
      if (m_currentSample >= times.Length) m_currentSample = 0;
    }
    [System.Security.SuppressUnmanagedCodeSecurity] 
    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    public extern static short QueryPerformanceCounter(ref long x);
    [System.Security.SuppressUnmanagedCodeSecurity] 
    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    public extern static short QueryPerformanceFrequency(ref long x);
  }

  public class Bitmask
  {
    private int value;
    public int Value
    {
      get { return value; }
      set { this.value = value; }
    }

    public Bitmask() : this(0) { ; }

    public Bitmask(int value)
    {
      this.value = value;
    }

    public bool GetBit(int index)
    {
      return ((value & (1 << (index-1))) > 0);
    }

    public void SetBit(int index, bool on)
    {
      if (on)
      {
        Value = this.value | (1 << (index-1));
      }
      else
      {
        Value = this.value & ~(1 << (index-1));
      }
    }
  }

  /// <summary>
  /// Allows the comparison of two filenames, including "*" wildcards.
  /// Other wildcard characters are not yet supported.  (And probably never will
  /// be unless regular expression are implemented - which would decrease speed).
  /// </summary>
  public class FilenameComparer
  {
    private string name;
    private string extension;

    public string Name
    {
      get { return name; }
    }

    public string Extension
    {
      get { return extension; }
    }

    public FilenameComparer(string filename)
    {
      name = Path.GetFileNameWithoutExtension(filename);
      extension = Path.GetExtension(filename);
    }

    public bool Compare(string filename)
    {
      string compareName = Path.GetFileNameWithoutExtension(filename);
      if (compareName == "") compareName = "*";
      string compareExtension = Path.GetExtension(filename);
      if (compareExtension == "") compareExtension = "*";

      bool nameEqual = false;
      bool extensionEqual = false;
      if (name == "*") nameEqual = true;
      else
      {
        if (name == compareName) nameEqual = true;
      }
      
      if (extension == "*") extensionEqual = true;
      else
      {
        if (extension == compareExtension) extensionEqual = true;
      }

      return (nameEqual && extensionEqual);
    }
  }
}
