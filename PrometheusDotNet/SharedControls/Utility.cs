using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;

namespace SharedControls
{
	/// <summary>
	/// Contains static helper functions for various controls.
	/// </summary>
	public class Utility
	{
    public static ImageList GenerateImageList(params string[] resourcePaths)
    {
      return GenerateImageList(CreateImagesFromResourcePaths(resourcePaths));
    }

    public static ImageList GenerateImageList(Bitmap[] images)
    {
      ImageList il = new ImageList();
      il.ColorDepth = ColorDepth.Depth32Bit;

      if (images.Length > 0)
      {
        for (int i = 0; i < images.Length; i++)
        {
          int ReSizeHeight = 256;
          int ReSizeWidth = 256;

          if (ReSizeHeight > 256)
          {
            ReSizeHeight = 256;
          }
					
          if (ReSizeWidth > 256)
          {
            ReSizeWidth = 256;
          }

          if (images[i].Width > ReSizeWidth || images[i].Height > ReSizeHeight)
          {
            double Scaling;
            double WidthScaling = ReSizeWidth / (double)images[i].Width;
            double HeightScaling = ReSizeHeight / (double)images[i].Height;

            if (WidthScaling < HeightScaling)
            {
              Scaling = WidthScaling;
            }
            else
            {
              Scaling = HeightScaling;
            }

            int newWidth = (int)(images[i].Width * Scaling);
            int newHeight = (int)(images[i].Height * Scaling);
            Bitmap bm = new Bitmap(newWidth, newHeight);
            Graphics graphics = Graphics.FromImage(bm);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(images[i], 0, 0, newWidth, newHeight);
            images[i] = bm;
          }
        }

        il.ImageSize = new Size(images[0].Width, images[0].Height);

        foreach (Bitmap image in images)
        {
          il.Images.Add(image);
          Bitmap bm = (Bitmap)il.Images[il.Images.Count - 1];
          for (int x = 0; x < bm.Width; x++)
          {
            for (int y = 0; y < bm.Height; y++)
            {
              bm.SetPixel(x, y, image.GetPixel(x, y));
            }
          }
        }
      }
      return il;
    }
    public static string[] MergeArrays (string[] a, string[] b)
    {
      int total = a.Length + b.Length;
      string[] c = new string[total];
      Array.Copy(a, 0, c, 0, a.Length);
      Array.Copy(b, 0, c, a.Length, b.Length);
      return c;
    }
    public static Image[] MergeArrays (Image[] a, Image[] b)
    {
      int total = a.Length + b.Length;
      Image[] c = new Image[total];
      Array.Copy(a, 0, c, 0, a.Length);
      Array.Copy(b, 0, c, a.Length, b.Length);
      return c;
    }
    public static Bitmap[] CreateImagesFromResourcePaths(params string[] paths)
    {
      int imageCount = paths.Length;
      Bitmap[] images = new Bitmap[imageCount];
      for (int x=0; x<imageCount; x++)
      {
        //use GetEntryAssembly since resources are still located in that assembly
        images[x] = (Bitmap)Bitmap.FromStream(
          Assembly.GetEntryAssembly().GetManifestResourceStream(paths[x]));  
      }
      return images;
    }

    public static void UpdateSignedLabel(Label lbl, float val)
    {
      if(val >= 0)
        lbl.ForeColor = Color.Blue;
      else
        lbl.ForeColor = Color.Red;

      lbl.Text = string.Format("{0:N2}", val);
    }
	}
}
