using System;

namespace Prometheus.Core.Compiler
{
	/// <summary>
	/// Summary description for Swizzle.
	/// </summary>
	public class DeSwizzle
	{
		public ushort height;
		public ushort width;
		public ushort depth;
		public DeSwizzle()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public void DeSwizzleData(ref byte[] data,uint bitCount)
		{
			Swizzle mySwiz = new Swizzle((uint)width, (uint)height, (uint)depth);
			uint tmp1;
			uint tmp2;
			byte[] swizData = new byte[data.Length];
			for(ushort x1=0;x1<width;x1++) 
			{
				for(int y=0;y<height;y++) 
				{
					tmp1 = (uint)((y * width) + x1) * (bitCount / 8);
					tmp2 = (uint)(mySwiz.Swiz(x1, y, -1)) * (bitCount / 8);
					for(int i=0;i<(bitCount / 8);i++) 
					{
						swizData[tmp1 + i] = data[tmp2 + i];
					}
				}
			}
			for(int i=0;i<data.Length;i++) data[i] = swizData[i];
		}
	}
	public class Swizzle 
	{
		public uint m_MaskX;
		public uint m_MaskY;
		public uint m_MaskZ;
		public Swizzle(uint width, uint height, uint depth)
		{
			m_MaskX = 0;
			m_MaskY = 0;
			m_MaskZ = 0;
			uint bit = 1;
			uint idx = 1;
			while(bit < width || bit < height || bit < depth) 
			{
				if(bit < width) 
				{
					m_MaskX |= idx;
					idx <<= 1;
				}
				if(bit < height) 
				{
					m_MaskY |= idx;
					idx <<= 1;
				}
				if(bit < depth) 
				{
					m_MaskZ |= idx;
					idx <<= 1;
				}
				bit <<= 1;
			}
		}
		public uint Swiz(int x, int y, int z) 
		{
			return SwizzleAxis(x, m_MaskX) | SwizzleAxis(y, m_MaskY) | (z != -1 ? SwizzleAxis(z, m_MaskZ) : 0);
		}
		public uint SwizzleAxis(int val, uint mask) 
		{
			uint result = 0;
			for(uint bit = 1;bit <= mask;bit <<= 1)
			{
				if((mask & bit) != 0) 
					result |= (uint)(val & bit);
				else 
					val <<= 1;
			}
			return result;
		}
	}
}
