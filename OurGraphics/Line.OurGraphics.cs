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


        public static Line CreateLine(List<DrawableObject> drawableObjects, TreeView treeView1, Point start, Point end, LineDrawingAlgo currentAlgo, bool isPartOfCircle = false)
        {
            Vertex startVertex = CreateVertex(drawableObjects, treeView1, start, true);
            Vertex endVertex = CreateVertex(drawableObjects, treeView1, end, true);

            Line line = new Line(startVertex, endVertex, currentAlgo);

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

            if (!isPartOfCircle)
            {
                TreeNode parent = new TreeNode($"{line.Name}");
                treeView1.Nodes.Add(parent);

                TreeNode child1 = new TreeNode($"{startVertex.Name}");
                TreeNode child2 = new TreeNode($"{endVertex.Name}");
                parent.Nodes.Add(child1);
                parent.Nodes.Add(child2);

                treeView1.Nodes.Add(parent);
            }

            

            return line;
        }
        #endregion
    }
}
