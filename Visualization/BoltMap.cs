using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Thundergen.Visualization.Colormaps;

namespace Thundergen.Visualization
{
    class BoltMap
    {
        public static void Render(Vector3[] points, Graphics g, int width, int height)
        {
            g.Clear(Color.Gray);

            float xmin = points.Select(p => p.X).Min();
            float xmax = points.Select(p => p.X).Max();
            float ymin = points.Select(p => p.Y).Min();
            float ymax = points.Select(p => p.Y).Max();
            float zmin = points.Select(p => p.Z).Min();
            float zmax = points.Select(p => p.Z).Max();

            float xscale = Math.Min(0.5f * width / Math.Abs(xmax), 0.5f * width / Math.Abs(xmin));
            float yscale = Math.Min(0.5f * width / Math.Abs(ymax), 0.5f * width / Math.Abs(ymin));
            float zscale = height / (zmax - zmin);
            float scale = Math.Min(Math.Min(xscale, yscale), zscale);
            Func<Vector3, PointF> xformx = v => new PointF(v.X * scale + width / 2, (zmax - v.Z) * scale);
            Func<Vector3, PointF> xformy = v => new PointF(v.Y * scale + width / 2, (zmax - v.Z) * scale);

            float colorref = Math.Min(xmin, ymin);
            float colorscale = Math.Max(xmax - colorref, ymax - colorref);

            Vector3 pf = points[0];
            Vector3 pl = points[points.Length - 1];

            var color = new byte[4];
            for (int i = 0; i < points.Length - 1; i++)
            {
                Vector3 p0 = points[i];
                Vector3 p1 = points[i + 1];

                float x = 0.5f * (p0.X + p1.X);
                Magma.CopyColor(0.1f + 0.9f * (x - colorref) / colorscale, color, 0);
                using (var p = new Pen(Color.FromArgb(color[2], color[1], color[0])))
                {
                    g.DrawLine(p, xformy(p0), xformy(p1));
                }

                float y = 0.5f * (p0.Y + p1.Y);
                Magma.CopyColor(0.1f + 0.9f * (y - colorref) / colorscale, color, 0);
                using (var p = new Pen(Color.FromArgb(color[2], color[1], color[0])))
                {
                    g.DrawLine(p, xformx(p0), xformx(p1));
                }
            }

            g.FillEllipse(Brushes.Green, new RectangleF(xformx(pf).X - 4, xformx(pf).Y - 4, 7, 7));
            g.FillEllipse(Brushes.Red, new RectangleF(xformx(pl).X - 4, xformx(pl).Y - 4, 7, 7));
        }
    }
}
