using System;
using Prometheus.Core.Render;
using Prometheus.Core;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Prometheus.Core.Tags;
using System.Drawing;

namespace Prometheus.Core.Render
{
	/// <summary>
	/// Summary description for Billboard.
	/// </summary>
	public class Billboard
	{
    int billboardTextureIndex = 0;
    static private CustomVertex.PositionTextured[] billboardVerts;
    private float dx;
    private float dy;
    static private VertexBuffer m_VertexBuffer;

		public Billboard(float width, float length, int TextureIndex)
		{
      dx = width*0.5f;
      dy = length;

      billboardTextureIndex = TextureIndex;
      billboardVerts = new CustomVertex.PositionTextured[4];
      billboardVerts[0] = new CustomVertex.PositionTextured(dx, 0, 0, 1, 1);
      billboardVerts[1] = new CustomVertex.PositionTextured(dx, length, 0, 1, 0);
      billboardVerts[2] = new CustomVertex.PositionTextured(-dx, 0, 0, 0, 1);
      billboardVerts[3] = new CustomVertex.PositionTextured(-dx, length, 0, 0, 0);

      m_VertexBuffer = new VertexBuffer(typeof(CustomVertex.PositionTextured), billboardVerts.Length, MdxRender.Dev, 
        Usage.WriteOnly, CustomVertex.PositionTextured.Format, Pool.Default);
      m_VertexBuffer.Created += new EventHandler(OnVertexBufferUpdate);
      OnVertexBufferUpdate(m_VertexBuffer, null);
    }
    static private void OnVertexBufferUpdate(object sender, EventArgs e)
    {
      VertexBuffer vb = (VertexBuffer)sender;
      GraphicsStream data = vb.Lock(0, 0, LockFlags.None);

      for(int v=0; v<billboardVerts.Length; v++)
        data.Write(billboardVerts[v]);

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
      MdxRender.Dev.RenderState.SourceBlend = Blend.SourceAlpha;
      MdxRender.Dev.RenderState.DestinationBlend = Blend.InvSourceAlpha;
      MdxRender.Dev.RenderState.AlphaBlendEnable = true;
      MdxRender.Dev.TextureState[0].ColorArgument1 = TextureArgument.TextureColor;
      MdxRender.Dev.TextureState[0].AlphaArgument1 = TextureArgument.TextureColor;
      MdxRender.Dev.TextureState[0].AlphaOperation = TextureOperation.SelectArg1;
      MdxRender.Dev.TextureState[0].ColorOperation = TextureOperation.SelectArg1;//.ModulateInvAlphaAddColor;
      MdxRender.Dev.TextureState[0].TextureTransform = TextureTransform.Disable;

      MdxRender.SM.m_TextureManager.ActivateTexture(0, this.billboardTextureIndex, 0);
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

      if(Math.Abs(x_loc) < dx)
      {
        if((y_loc > 0)&&(y_loc < dy))
          bIntersected = true;
      }

      return(bIntersected);
    }
  }
}
