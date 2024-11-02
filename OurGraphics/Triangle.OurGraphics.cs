using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OurGraphics
{
    public static partial class GraphicsExtension
    {
        #region TriangleClass

        public class Triangle : DrawableObject
        {
            public Vertex A { get; set; }
            public Vertex B { get; set; }
            public Vertex C { get; set; }
            private LineDrawingAlgo DrawingAlgo { get; set; }




            public Triangle(Vertex a, Vertex b, Vertex c) : base("Triangle", new Point())
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

            public override void Move(int deltaX, int deltaY)
            {
                A.Move(deltaX, deltaY);
                B.Move(deltaX, deltaY);
                C.Move(deltaX, deltaY);
            }
        }

        #endregion

        #region Create Triangle
        private static int triangleCount = 1;

        public static void CreateTriangle(List<DrawableObject> drawableObjects, TreeView treeView1, Point A, Point B, Point C)
        {
            var VertexA = CreateVertex(drawableObjects, treeView1, A, true);
            var VertexB = CreateVertex(drawableObjects, treeView1, B, true);
            var VertexC = CreateVertex(drawableObjects, treeView1, C, true);



            var triangle = new Triangle(VertexA, VertexB, VertexC);

            triangle.SetName($"Midpoint_Triangle{triangleCount++}");
            VertexA.SetName($"A");
            VertexB.SetName($"B");
            VertexC.SetName($"C");

            drawableObjects.Add(triangle);

            TreeNode parent = new TreeNode($"{triangle.Name}");
            TreeNode child1 = new TreeNode($"{VertexA.Name}");
            TreeNode child2 = new TreeNode($"{VertexB.Name}");
            TreeNode child3 = new TreeNode($"{VertexC.Name}");
            parent.Nodes.Add(child1);
            parent.Nodes.Add(child2);
            parent.Nodes.Add(child3);

            treeView1.Nodes.Add(parent);
        }
        #endregion
    }
}
