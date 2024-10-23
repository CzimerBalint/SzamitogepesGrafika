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
        public enum LineDrawingAlgo
        {
            DDA,
            Midpoint, 
        }
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
            public abstract void Move(int deltaX, int deltaY);
        }

        #region Vertex
        public class Vertex : DrawableObject
        {
            public bool IsSelected { get; set; }
            public Vertex(Point location) : base($"Vertex", location) { 
            }

            public void SetName(string name)
            {
                Name = name;
            }
            public void Select()
            {
                IsSelected = true;
            }

            public void Deselect()
            {
                IsSelected = false;
            }

            public override void Draw(Graphics g)
            {
                Brush brush = IsSelected ? Brushes.Red : Brushes.Black;
                g.FillRectangle(brush, Location.X-5, Location.Y-5, 10, 10);
            }

            public bool Contains(Point p)
            {
                return new Rectangle(Location.X - 5, Location.Y - 5, 10, 10).Contains(p);
            }

            public override void Move(int deltaX, int deltaY)
            {
                Location = new Point(Location.X + deltaX, Location.Y + deltaY);
            }
        }
        #endregion

        #region Lines
        public class Line : DrawableObject
        {
            public Vertex Start { get; set; }
            public Vertex End { get; set; }
            private LineDrawingAlgo DrawingAlgo { get; set; }

            public Line(Vertex start, Vertex end, LineDrawingAlgo drawingAlgo) : base($"Line", new Point())
            {

                Start = start;
                End = end;
                DrawingAlgo = drawingAlgo;
            }
            public void SetName(string name)
            {
                Name = name;
            }

            public override void Draw(Graphics g)
            {
                switch (DrawingAlgo)
                {
                    case LineDrawingAlgo.DDA:
                        g.DDA(Pens.Black, Start, End);
                        break;
                    case LineDrawingAlgo.Midpoint:
                        g.MidPoint(Pens.Black, Start, End);
                        break;
                    default:
                        g.DDA(Pens.Black, Start, End);
                        break;
                }
            }

             
            public override void Move(int deltaX, int deltaY)
            {
                Start.Move(deltaX, deltaY);
                End.Move(deltaX, deltaY);
            }

        }
        #endregion

        #endregion

        private static int vertexCount = 1;
        private static int ddalineCount = 1;
        private static int MPlineCount = 1;

        public static Vertex CreateVertex(List<DrawableObject> drawableObjects, TreeView treeView1, Point location, bool isPartOfLine = false)
        {
            Vertex vertex = new Vertex(location);
            vertex.SetName($"Vertex{vertexCount++}");
            drawableObjects.Add(vertex);

            if (!isPartOfLine)
            {
                TreeNode parent = new TreeNode($"{vertex.Name}");
                treeView1.Nodes.Add(parent);
            }

            return vertex;
        }

        public static void CreateLine(List<DrawableObject> drawableObjects, TreeView treeView1, Point start, Point end,LineDrawingAlgo currentAlgo)
        {
            var startVertex = CreateVertex(drawableObjects, treeView1, start, true);
            var endVertex = CreateVertex(drawableObjects, treeView1, end,true);

            var line = new Line(startVertex, endVertex, currentAlgo);

            int lineCount = 1;

            if (currentAlgo == LineDrawingAlgo.DDA)
            {
                lineCount = ddalineCount;
                ddalineCount++;
            }
            else if (currentAlgo == LineDrawingAlgo.Midpoint)
            {
                lineCount = MPlineCount;
                MPlineCount++;
            }

            line.SetName($"{currentAlgo.ToString()}_Line{lineCount++}");
            startVertex.SetName($"{line.Name}_Start");
            endVertex.SetName($"{line.Name}_End");

            drawableObjects.Add(line);
            drawableObjects.Add(startVertex);
            drawableObjects.Add(endVertex);            

            TreeNode parent = new TreeNode($"{line.Name}");
            TreeNode child1 = new TreeNode($"{startVertex.Name}");
            TreeNode child2 = new TreeNode($"{endVertex.Name}");
            parent.Nodes.Add(child1);
            parent.Nodes.Add(child2);

            treeView1.Nodes.Add(parent);
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
            g.MidPoint(pen,start.Location.X,start.Location.Y,end.Location.X,end.Location.Y);
        }
    }
}
