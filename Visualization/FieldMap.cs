using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Thundergen.Visualization.Colormaps;

namespace Thundergen.Visualization
{
    class FieldMap
    {
        public static Bitmap Render(float[,] data)
        {
            var img = new Bitmap(data.GetLength(0), data.GetLength(1), PixelFormat.Format32bppArgb);

            BitmapData bmpdata = null;
            try
            {
                bmpdata = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.WriteOnly, img.PixelFormat);
                int numbytes = bmpdata.Stride * img.Height;
                byte[] bytedata = new byte[numbytes];
                int i0 = 0;
                float min = float.MaxValue;
                float max = float.MinValue;
                for (int y = 0; y < img.Height; y++)
                {
                    for (int x = 0; x < img.Width; x++)
                    {
                        if (data[x, y] < min) min = data[x, y];
                        if (data[x, y] > max) max = data[x, y];
                    }
                }
                float range = max - min;
                for (int y = 0; y < img.Height; y++)
                {
                    int i = i0;
                    for (int x = 0; x < img.Width; x++)
                    {
                        Magma.CopyColor((data[x, y] - min) / range, bytedata, i);
                        bytedata[i + 3] = 255;
                        i += 4;
                    }
                    i0 += bmpdata.Stride;
                }
                IntPtr ptr = bmpdata.Scan0;
                Marshal.Copy(bytedata, 0, ptr, numbytes);
            }
            finally
            {
                if (bmpdata != null)
                {
                    img.UnlockBits(bmpdata);
                }
            }

            return img;
        }
    }
}
