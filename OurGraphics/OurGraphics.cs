using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace OurGraphics
{
    public static class OurGraphics
    {
        #region Connvert screenSpace to worldSpace
        public static Point WorldOrigin(this Graphics g,int width, int height)
        {
            return new Point(width/2, height/2);
        }
        public static Point WorldOrigin(this Graphics g,Point size)
        {
            return new Point(size.X / 2, size.Y/ 2);
        }
        public static Point objectToWorldOrigin(this Graphics g, int w, int h, int x, int y)
        {
            return new Point((w - x) * -1, h - y);
        }
        public static Point objectToWorldOrigin(this Graphics g, Point size, Point location)
        {
            return new Point((size.X - location.X) * -1, size.Y - location.Y);
        }
        #endregion

        #region Abstract Object management
        public abstract class DrawableObject
        {
            public string Name { get; set; }
            public Point Location { get; set; }

            public DrawableObject(string name, Point location)
            {
                Name = name;
                Location = location;
            }
            public abstract void Draw(Graphics g);

        }

        #region Vertex
        public class Vertex : DrawableObject
        {
            public Vertex(Point location) : base($"Vertex", location) { 
            }

            public void SetName(string name)
            {
                Name = name;
            }

            public override void Draw(Graphics g)
            {
                g.FillRectangle(Brushes.Black, Location.X-5, Location.Y-5, 10, 10);
            }
        }
        #endregion

        #region Lines
        public class Line : DrawableObject
        {
            public Vertex Start { get; set; }
            public Vertex End { get; set; }

            public Line(Vertex start, Vertex end) : base($"Line", new Point())
            {

                Start = start;
                End = end;
            }
            public void SetName(string name)
            {
                Name = name;
            }

            public override void Draw(Graphics g)
            {
                g.DDA(Pens.Black,Start,End);
            }
        }
        #endregion

        #endregion

        private static int vertexCount = 1; 
        private static int lineCount = 1;

        public static Vertex CreateVertex(Point location)
        {
            Vertex vertex = new Vertex(location);
            vertex.SetName($"Vertex{vertexCount++}");
            return vertex;
        }

        public static void CreationVert(List<DrawableObject> drawableObjects, Point worldOrigin, TreeView treeView1)
        {
            var vert = CreateVertex(worldOrigin);
            drawableObjects.Add(vert);
            TreeNode parnet = new TreeNode($"{vert.Name}");

            // Itt a treeView1-t is hozzá kell adni, ha szükséges
            treeView1.Nodes.Add(parnet);
        }


        public static void CreationLine(List<DrawableObject> drawableObjects, Point start, Point end, TreeView treeView1)
        {
            var line = CreateLine(start, end); // Létrehozzuk a vonalat
            drawableObjects.Add(line); // Hozzáadjuk a vonalat a drawableObjects listához
            drawableObjects.Add(line.Start);
            drawableObjects.Add(line.End);
            TreeNode parnet = new TreeNode($"{line.Name}");
            TreeNode child1 = new TreeNode($"{line.Start.Name}");
            TreeNode child2 = new TreeNode($"{line.End.Name}");
            parnet.Nodes.Add(child1);
            parnet.Nodes.Add(child2);

            // Itt a treeView1-t is hozzá kell adni, ha szükséges
            treeView1.Nodes.Add(parnet);
        }

        public static Line CreateLine(Point start, Point end)
        {
            var startVertex = CreateVertex(start);
            var endVertex = CreateVertex(end);

            var line = new Line(startVertex, endVertex);

            startVertex.SetName($"DDA_Line{lineCount}_StartVert");
            endVertex.SetName($"DDA_Line{lineCount}_EndVert");
            line.SetName($"DDA_Line{lineCount}");

            lineCount++;

            return line;
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
            float dx = end.Location.X - start.Location.X;
            float dy = end.Location.Y - start.Location.Y;
            float length = Math.Abs(dx);
            if (Math.Abs(dy) > length)
                length = Math.Abs(dy);
            float incX = dx / length;
            float incY = dy / length;
            float x = start.Location.X;
            float y = start.Location.Y;
            g.DrawPixel(pen, x, y);
            for (int i = 0; i < length; i++)
            {
                x += incX;
                y += incY;
                g.DrawPixel(pen, x, y);
            }

        }

    }
}
