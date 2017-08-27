using System;
using System.Diagnostics;
using Prometheus.Core.Render;
using Prometheus.Core;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Prometheus.Core.Tags;
using Prometheus.Core.Tags.Sbsp;
using System.Drawing;

namespace Prometheus.Core.Render
{
	/// <summary>
	/// Summary description for Decal.
	/// </summary>
	public class PromDecal
	{
    int decalTextureIndex = 0;
    static private CustomVertex.PositionTextured[] decalVerts;
    private float dx;
    private float dy;
    private Vector3 localNormal = new Vector3();
    static private VertexBuffer m_VertexBuffer;

    public PromDecal(float radius, TagFileName BitmapTFN)
    {
      dx = radius*0.5f;
      dy = radius*0.5f;

      decalTextureIndex = MdxRender.SM.m_TextureManager.RegisterTexture(BitmapTFN);
      decalVerts = new CustomVertex.PositionTextured[4];
      decalVerts[0] = new CustomVertex.PositionTextured(dx, -dy, 0, 1, 1);
      decalVerts[1] = new CustomVertex.PositionTextured(dx, dy, 0, 1, 0);
      decalVerts[2] = new CustomVertex.PositionTextured(-dx, -dy, 0, 0, 1);
      decalVerts[3] = new CustomVertex.PositionTextured(-dx, dy, 0, 0, 0);

      m_VertexBuffer = new VertexBuffer(typeof(CustomVertex.PositionTextured), decalVerts.Length, MdxRender.Dev, 
        Usage.WriteOnly, CustomVertex.PositionTextured.Format, Pool.Default);
      m_VertexBuffer.Created += new EventHandler(OnVertexBufferUpdate);
      OnVertexBufferUpdate(m_VertexBuffer, null);
    }
    static private void OnVertexBufferUpdate(object sender, EventArgs e)
    {
      VertexBuffer vb = (VertexBuffer)sender;
      GraphicsStream data = vb.Lock(0, 0, LockFlags.None);

      for(int v=0; v<decalVerts.Length; v++)
        data.Write(decalVerts[v]);

      vb.Unlock();
    }
    public void Render()
    {
      MdxRender.Dev.VertexFormat = CustomVertex.PositionTextured.Format;
      MdxRender.Dev.RenderState.CullMode = Cull.None;
      for(int i=1; i<5; i++)
      {
        MdxRender.Dev.TextureState[i].AlphaOperation = TextureOperation.Disable;
        MdxRender.Dev.TextureState[i].ColorOperation = TextureOperation.Disable;
      }

      MdxRender.SM.m_TextureManager.ActivateTexture(0, this.decalTextureIndex, 0);
      MdxRender.Dev.RenderState.SourceBlend = Blend.One;
      MdxRender.Dev.RenderState.DestinationBlend = Blend.One;
      MdxRender.Dev.RenderState.AlphaBlendEnable = true;
      MdxRender.Dev.TextureState[0].ColorArgument1 = TextureArgument.TextureColor;
      MdxRender.Dev.TextureState[0].ColorArgument2 = TextureArgument.TextureColor;
      MdxRender.Dev.TextureState[0].AlphaArgument1 = TextureArgument.TextureColor;
      MdxRender.Dev.TextureState[0].AlphaArgument2 = TextureArgument.TextureColor;
      MdxRender.Dev.TextureState[0].AlphaOperation = TextureOperation.SelectArg1;
      MdxRender.Dev.TextureState[0].ColorOperation = TextureOperation.SelectArg1;//.ModulateInvAlphaAddColor;
      MdxRender.Dev.TextureState[0].TextureTransform = TextureTransform.Disable;

      ShaderBase sb = new ShaderBase();
      sb.DebugFixedFunctionShader();
      MdxRender.Dev.SetStreamSource(0, m_VertexBuffer, 0);
      MdxRender.Dev.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
    }
    public bool PerformIntersectTest(Vector3 origin, Vector3 direction)
    {
      bool bIntersected = false;
      //set z=0 and see where x,y are
      float dz = origin.Z;
      float x_loc = origin.X + direction.X*dz;
      float y_loc = origin.Y + direction.Y*dz;

      if((x_loc > -dx)&&(x_loc < dx))
      {
        if((y_loc > -dy)&&(y_loc < dy))
          bIntersected = true;
      }

      Trace.WriteLine(string.Format("xloc = {0}   yloc = {1}  dx={2}  dy={3}", x_loc, y_loc, dx, dy));

      return(bIntersected);
    }
    public void AttachDecalToH1Bsp(TagBsp bsp)
    {
      //given coordinate, find nearest bsp surface and attach to it

      //
    }
  }
}
