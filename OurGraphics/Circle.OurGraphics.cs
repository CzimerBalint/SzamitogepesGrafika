using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static OurGraphics.GraphicsExtension;

namespace OurGraphics
{
    public static partial class GraphicsExtension
    {
        #region Circle Class
        public class Circle : DrawableObject
        {
            public Line Radius { get; set; }

            public Circle(Line radius) : base("Circle", radius.Start.Location)
            {
                Radius = radius;
            }

            public override void Draw(Graphics g)
            {
                g.AritmeticCircle(Pens.Black, Radius.Start, Radius.End);
            }

            public override void Move(float deltaX, float deltaY, float deltaZ)
            {
                Radius.Start.Move(deltaX, deltaY, deltaZ);
                Radius.End.Move(deltaX, deltaY, deltaZ);
            }

            public override void Transform(Matrix4 transformation)
            {
                Radius.Start.Transform(transformation);
                Radius.End.Transform(transformation);
            }
        }
        #endregion

        #region Create Circle
        private static int circleCount = 1;

        public static Circle CreateCircle(this Graphics g, List<DrawableObject> drawableObjects, TreeView treeView1, Vertex center, Vertex radius)
        {
            Line rad = CreateLine(g, drawableObjects, treeView1, center, radius, LineDrawingAlgo.Midpoint);

            Circle circle = new Circle(rad)
            {
                Name = $"Circle{circleCount++}"
            };

            drawableObjects.Add(circle);

            TreeNode parent = new TreeNode($"{circle.Name}");

            TreeNode child0 = new TreeNode($"{rad.Name}");
            TreeNode child1 = new TreeNode($"{rad.Start.Name}");
            TreeNode child2 = new TreeNode($"{rad.End.Name}");

            parent.Nodes.Add(child0);
            child0.Nodes.Add(child1);
            child0.Nodes.Add(child2);

            treeView1.Nodes.Add(parent);

            return circle;
        }
        #endregion
    }
}
