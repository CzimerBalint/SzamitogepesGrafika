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

            public Line(Vertex start, Vertex end, LineDrawingAlgo drawingAlgo) : base("Line", start.Location)
            {
                Start = start;
                End = end;
                DrawingAlgo = drawingAlgo;
            }

            public override void Draw(Graphics g)
            {
                switch (DrawingAlgo)
                {
                    case LineDrawingAlgo.DDA:
                        DDA(g, Pens.Black, Start, End);
                        break;
                    case LineDrawingAlgo.Midpoint:
                        MidPoint(g, Pens.Black, Start, End);
                        break;
                    default:
                        MidPoint(g, Pens.Black, Start, End);
                        break;
                }
            }

            public override void Move(float deltaX, float deltaY, float deltaZ)
            {
                Start.Move(deltaX, deltaY, deltaZ);
                End.Move(deltaX, deltaY, deltaZ);
            }

            public override void Transform(Matrix4 transformation)
            {
                Start.Transform(transformation);
                End.Transform(transformation);
            }
        }
        #endregion

        #region Create Line
        private static int lineCount = 1;

        public static Line CreateLine(this Graphics g, List<DrawableObject> drawableObjects, TreeView treeView1, Vertex start, Vertex end, LineDrawingAlgo drawingAlgo)
        {
            Line line = new Line(start, end, drawingAlgo)
            {
                Name = $"Line{lineCount++}"
            };

            drawableObjects.Add(start);
            drawableObjects.Add(end);
            drawableObjects.Add(line);

            TreeNode parent = new TreeNode($"{line.Name}");
            TreeNode child1 = new TreeNode($"{line.Start.Name}");
            TreeNode child2 = new TreeNode($"{line.End.Name}");
            parent.Nodes.Add(child1);
            parent.Nodes.Add(child2);

            treeView1.Nodes.Add(parent);

            return line;
        }
        #endregion
    }
}
