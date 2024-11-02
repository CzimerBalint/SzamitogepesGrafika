using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OurGraphics
{
    public static partial class GraphicsExtension
    {
        #region Rectangle Class

        public class Rect : DrawableObject
        {
            public Vertex A { get; set; }
            public Vertex B { get; set; }
            public Vertex C { get; set; }
            public Vertex D { get; set; }
            private LineDrawingAlgo DrawingAlgo { get; set; }


            public Rect(Vertex a, Vertex b, Vertex c, Vertex d) : base("Rectangle", new Point())
            {
                A = a;
                B = b;
                C = c;
                D = d;
            }

            public void SetName(string name)
            {
                Name = name;
            }

            public override void Draw(Graphics g)
            {
                g.MidPoint(Pens.Black, A, B);
                g.MidPoint(Pens.Black, A, D);
                g.MidPoint(Pens.Black, D, C);
                g.MidPoint(Pens.Black, C, B);

            }

            public override void Move(int deltaX, int deltaY)
            {
                A.Move(deltaX, deltaY);
                B.Move(deltaX, deltaY);
                C.Move(deltaX, deltaY);
                D.Move(deltaX, deltaY);
            }


        }


        #endregion

        #region Create Rect

        private static int rectangleCount = 1;

        public static void CreateRectangle(List<DrawableObject> drawableObjects, TreeView treeView1, string name, Point A, Point B, Point C, Point D)
        {
            var VertexA = CreateVertex(drawableObjects, treeView1, A, true);
            var VertexB = CreateVertex(drawableObjects, treeView1, B, true);
            var VertexC = CreateVertex(drawableObjects, treeView1, C, true);
            var VertexD = CreateVertex(drawableObjects, treeView1, D, true);


            var rectangle = new Rect(VertexA, VertexB, VertexC, VertexD);

            rectangle.SetName($"Midpoint_{name}{rectangleCount++}");
            VertexA.SetName($"A");
            VertexB.SetName($"B");
            VertexC.SetName($"C");
            VertexD.SetName($"D");


            drawableObjects.Add(rectangle);

            TreeNode parent = new TreeNode($"{rectangle.Name}");
            TreeNode child1 = new TreeNode($"{VertexA.Name}");
            TreeNode child2 = new TreeNode($"{VertexB.Name}");
            TreeNode child3 = new TreeNode($"{VertexC.Name}");
            TreeNode child4 = new TreeNode($"{VertexD.Name}");

            parent.Nodes.Add(child1);
            parent.Nodes.Add(child2);
            parent.Nodes.Add(child3);
            parent.Nodes.Add(child4);

            treeView1.Nodes.Add(parent);

        }
        #endregion
    }
}
