using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using static OurGraphics.OurGraphics;

namespace OurGraphics
{ 
    public static partial class OurGraphics
    {
        public enum LineDrawingAlgo
        {
            DDA,
            Midpoint,
        }

        public static void DrawPixel(this Graphics g, Pen pen, float x, float y)
        {
            g.DrawRectangle(pen, x, y, 0.5f, 0.5f);

        }
        public static void DrawPixel(this Graphics g, Color color, float x, float y)
        {
            g.DrawRectangle(new Pen(color), x, y, 0.5f, 0.5f);
        }
        public static void DDA(this Graphics g, Pen pen, float x0, float y0, float x1, float y1)
        {
            float dx = x1 - x0;
            float dy = y1 - y0;
            float length = Math.Abs(dx);
            if (Math.Abs(dy) > length)
                length = Math.Abs(dy);
            float incX = dx / length;
            float incY = dy / length;
            float x = x0;
            float y = y0;
            g.DrawPixel(pen, x, y);
            for (int i = 0; i < length; i++)
            {
                x += incX;
                y += incY;
                g.DrawPixel(pen, x, y);
            }

        }
        public static void DDA(this Graphics g, Pen pen, Vertex start, Vertex end)
        {
            g.DDA(pen, start.Location.X, start.Location.Y, end.Location.X, end.Location.Y);

        }

        public static void MidPoint(this Graphics g, Pen pen, int x0, int y0, int x1, int y1)
        {
            bool isSteep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);

            if (isSteep)
            {
                int temp = x0;
                x0 = y0;
                y0 = temp;

                temp = x1;
                x1 = y1;
                y1 = temp;
            }

            if (x0 > x1)
            {
                int temp = x0;
                x0 = x1;
                x1 = temp;

                temp = y0;
                y0 = y1;
                y1 = temp;
            }

            int dx = x1 - x0;
            int dy = Math.Abs(y1 - y0);
            int error = dx / 2;  // Initial error term

            int yStep = (y0 < y1) ? 1 : -1;  // Step direction for y
            int y = y0;

            // Loop through the points from x0 to x1
            for (int x = x0; x <= x1; x++)
            {
                // If the line is steep, we need to swap back the x and y when drawing
                if (isSteep)
                {
                    g.DrawPixel(pen, y, x);  // Draw swapped if steep
                }
                else
                {
                    g.DrawPixel(pen, x, y);  // Draw normally if not steep
                }

                error -= dy;
                if (error < 0)
                {
                    y += yStep;  // Move y based on the step direction
                    error += dx;  // Adjust the error term
                }
            }

        }
        public static void MidPoint(this Graphics g, Pen pen, Vertex start, Vertex end)
        {
            g.MidPoint(pen, start.Location.X, start.Location.Y, end.Location.X, end.Location.Y);
        }

        public static void CirclePoints(this Graphics g, Pen pen, Point center, int x, int y)
        {
            g.DrawPixel(pen, center.X + x, center.Y + y);
            g.DrawPixel(pen, center.X + x, center.Y - y);
            g.DrawPixel(pen, center.X - x, center.Y + y);
            g.DrawPixel(pen, center.X - x, center.Y - y);
            g.DrawPixel(pen, center.X + y, center.Y + x);
            g.DrawPixel(pen, center.X + y, center.Y - x);
            g.DrawPixel(pen, center.X - y, center.Y + x);
            g.DrawPixel(pen, center.X - y, center.Y - x);
        }

        public static void AritmeticCircle(this Graphics g, Pen pen, Point center, Point r)
        {
            int radius = (int)Math.Sqrt(Math.Pow(r.X - center.X, 2) + Math.Pow(r.Y - center.Y, 2));
            int x = 0;
            int y = radius;
            float d = 5 / 4 - radius;

            g.CirclePoints(pen, center, x, y);
            while (y > x)
            {
                if (d < 0)
                {
                    d += 2 * x + 3;
                }
                else
                {
                    d += 2 * (x - y) + 5;
                    y -= 1;
                }
                x += 1;
                g.CirclePoints(pen, center, x, y);
            }
        }

        public static void AritmeticCircle(this Graphics g, Pen pen, Vertex center, Vertex r)
        {
            g.AritmeticCircle(pen, center.Location, r.Location);
        }
        public static Vertex MergeVertices(List<DrawableObject> drawableObjects, TreeView treeView1, List<Vertex> verticesToMerge)
        {
            if (verticesToMerge == null || verticesToMerge.Count < 2)
                throw new ArgumentException("Legalább két vertekszet kell megadni az egyesítéshez.");

            // Az új vertex helyének kiszámítása (átlag alapján)
            int avgX = (int)verticesToMerge.Average(v => v.Location.X);
            int avgY = (int)verticesToMerge.Average(v => v.Location.Y);
            Point mergedLocation = new Point(avgX, avgY);

            // Új vertex létrehozása és hozzáadása
            Vertex mergedVertex = CreateVertex(drawableObjects, treeView1, mergedLocation);

            // Frissítsük azokat az objektumokat, amelyek a régi vertekszeket használták (pl. vonalak)
            foreach (var drawable in drawableObjects.OfType<Line>())
            {
                if (verticesToMerge.Contains(drawable.Start))
                {
                    drawable.Start = mergedVertex;
                }
                if (verticesToMerge.Contains(drawable.End))
                {
                    drawable.End = mergedVertex;
                }
            }

            // Régi vertekszek eltávolítása
            foreach (var vertex in verticesToMerge)
            {
                drawableObjects.Remove(vertex);
                // Törlés a TreeView-ból is
                var node = treeView1.Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == vertex.Name);
                if (node != null)
                {
                    treeView1.Nodes.Remove(node);
                }
            }

            return mergedVertex;
        }

    }
}
