using System;
using System.Collections;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Prometheus.Core.Render;

namespace Core.Lightmap
{
  public class GrenLightmapDebug
  {
    private CustomVertex.PositionColored[] vertData = new CustomVertex.PositionColored[1000];
    private int mycount = 0;
    private CustomVertex.PositionColored[] lineData = new CustomVertex.PositionColored[2000];
    private int lineCount = 0;

    public void AddRay(Vector3 start, Vector3 end)
    {
      if(lineCount < 1999)
      {
        lineData[lineCount] = new CustomVertex.PositionColored(start.X, start.Y, start.Z, Color.Magenta.ToArgb());
        lineCount++;
        lineData[lineCount] = new CustomVertex.PositionColored(end.X, end.Y, end.Z, Color.Magenta.ToArgb());
        lineCount++;        
      }
    }
    public void AddIntersectPoint(Vector3 pt)
    {
      if(mycount < 1000)
      {
        vertData[mycount] = new CustomVertex.PositionColored(pt.X, pt.Y, pt.Z, Color.Magenta.ToArgb());
        mycount++;
      }
    }
    public void Reset()
    {
      for(int i=0; i<1000; i++)
        vertData[i] = new CustomVertex.PositionColored(0,0,0, Color.Magenta.ToArgb());

      for(int i=0; i<2000; i++)
        lineData[i] = new CustomVertex.PositionColored(0,0,0, Color.Magenta.ToArgb());

      mycount = 0;
      lineCount = 0;
    }
    public void Render()
    {
      Material bbColor = new Material();
      bbColor.Diffuse = Color.Magenta;
      bbColor.Ambient = Color.White;
      MdxRender.Dev.Material = bbColor;
      MdxRender.Dev.TextureState[0].ColorArgument2 = TextureArgument.Diffuse;
      MdxRender.Dev.TextureState[0].ColorOperation = TextureOperation.SelectArg2;
      MdxRender.Dev.TextureState[1].ColorOperation = TextureOperation.Disable;
      MdxRender.Dev.RenderState.Lighting = false;
      MdxRender.Dev.DrawUserPrimitives(PrimitiveType.PointList, mycount, vertData);
      MdxRender.Dev.DrawUserPrimitives(PrimitiveType.LineList, lineCount/2, lineData);
      MdxRender.Dev.RenderState.Lighting = true;
    }
  }
  public enum RadiosityLightType {Diffuse, Directional};
  public class RadiosityLight
  {
    public RadiosityLightType lightType = RadiosityLightType.Diffuse;
    public Vector3 position = new Vector3();
    public Vector3 direction;
    public float[] color = new float[3];
    public float power;
  }

  public class RadiosityLightCollection : CollectionBase
  {
    public void Add(RadiosityLight value)
    {
      InnerList.Add(value);
    }
    public RadiosityLight this[int index]
    {
      get { return (RadiosityLight)InnerList[index]; }
      set { InnerList[index] = value; }
    }
    public RadiosityLightCollection()
    {
    }
    public RadiosityLightCollection(RadiosityLight[] values)
    {
      AddRange(values);
    }
    public void AddRange(RadiosityLight[] values)
    {
      //foreach (RadiosityLight rl in values)
      //  Add(rl); 
      
      // Note from Mono: For future reference, InnerList exposes a generic AddRange method.
      InnerList.AddRange(values);
    }
    public RadiosityLight[] ToArray()
    {
      //RadiosityLight[] values = new RadiosityLight[InnerList.Count];
      //for (int x=0; x<InnerList.Count; x++)
      //{
      //  values[x] = (RadiosityLight)InnerList[x];
      //}
      //return values;

      // Note from Mono: Here's a faster way to typecast an ArrayList to a typed array.
      return InnerList.ToArray(typeof(RadiosityLight)) as RadiosityLight[];
    }
  }
}
