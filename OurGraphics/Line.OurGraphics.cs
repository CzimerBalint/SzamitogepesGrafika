using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static OurGraphics.GraphicsExtension;

namespace OurGraphics
{
    public static partial class GraphicsExtension
    {
        #region Lines Class
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

        #region Create Line
        private static int ddalineCount = 1;
        private static int MPlineCount = 1;


        public static Line CreateLine(this Graphics g, List<DrawableObject> drawableObjects, TreeView treeView1, Point start, Point end, LineDrawingAlgo currentAlgo, bool isPartOfCircle = false)
        {

            Line line = new Line(g.CreateVertex(drawableObjects, treeView1, start, true), g.CreateVertex(drawableObjects, treeView1, end, true), currentAlgo);
            

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
            line.Start.SetName($"{line.Name}_Start");
            line.End.SetName($"{line.Name}_End");

            drawableObjects.Add(line);
           

            if (!isPartOfCircle)
            {
                TreeNode parent = new TreeNode($"{line.Name}");

                TreeNode child1 = new TreeNode($"{line.Start.Name}");
                TreeNode child2 = new TreeNode($"{line.End.Name}");
                parent.Nodes.Add(child1);
                parent.Nodes.Add(child2);

                treeView1.Nodes.Add(parent);
            }
            
            

            return line;
        }
        #endregion
    }
}
