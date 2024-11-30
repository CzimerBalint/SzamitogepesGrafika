using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OurGraphics
{
    public static partial class GraphicsExtension
    {
        #region Triangle Class

        public class Triangle : DrawableObject
        {
            public Vertex A { get; set; }
            public Vertex B { get; set; }
            public Vertex C { get; set; }

            public Triangle(Vertex a, Vertex b, Vertex c) : base("Triangle", a.Location)
            {
                A = a;
                B = b;
                C = c;
            }

            public void SetName(string name)
            {
                Name = name;
            }

            public override void Draw(Graphics g)
            {
                g.MidPoint(Pens.Black, A, B);
                g.MidPoint(Pens.Black, A, C);
                g.MidPoint(Pens.Black, B, C);
            }

            public override void Move(float deltaX, float deltaY, float deltaZ)
            {
                A.Move(deltaX, deltaY, deltaZ);
                B.Move(deltaX, deltaY, deltaZ);
                C.Move(deltaX, deltaY, deltaZ);
            }

            // Új transzformációs metódus
            public override void Transform(Matrix4 transformation)
            {
                A.Transform(transformation);
                B.Transform(transformation);
                C.Transform(transformation);
            }
        }
        #endregion

        #region Create Triangle
        private static int triangleCount = 1;

        public static void CreateTriangle(this Graphics g, List<DrawableObject> drawableObjects, TreeView treeView1, Point A, Point B, Point C)
        {
            var vertexA = CreateVertex(g, drawableObjects, treeView1, A, true);
            var vertexB = CreateVertex(g, drawableObjects, treeView1, B, true);
            var vertexC = CreateVertex(g, drawableObjects, treeView1, C, true);

            var triangle = new Triangle(vertexA, vertexB, vertexC);

            triangle.SetName($"Midpoint_Triangle{triangleCount++}");
            vertexA.SetName("A");
            vertexB.SetName("B");
            vertexC.SetName("C");

            drawableObjects.Add(triangle);

            TreeNode parent = new TreeNode($"{triangle.Name}");
            TreeNode child1 = new TreeNode($"{vertexA.Name}");
            TreeNode child2 = new TreeNode($"{vertexB.Name}");
            TreeNode child3 = new TreeNode($"{vertexC.Name}");
            parent.Nodes.Add(child1);
            parent.Nodes.Add(child2);
            parent.Nodes.Add(child3);

            treeView1.Nodes.Add(parent);
        }
        #endregion
    }
}
