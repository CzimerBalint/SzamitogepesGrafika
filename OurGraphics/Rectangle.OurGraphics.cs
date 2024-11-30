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

            public Rect(Vertex a, Vertex b, Vertex c, Vertex d) : base("Rectangle", a.Location)
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

            public override void Move(float deltaX, float deltaY, float deltaZ)
            {
                A.Move(deltaX, deltaY, deltaZ);
                B.Move(deltaX, deltaY, deltaZ);
                C.Move(deltaX, deltaY, deltaZ);
                D.Move(deltaX, deltaY, deltaZ);
            }

            // Új transzformációs metódus
            public override void Transform(Matrix4 transformation)
            {
                A.Transform(transformation);
                B.Transform(transformation);
                C.Transform(transformation);
                D.Transform(transformation);
            }
        }
        #endregion

        #region Create Rectangle
        private static int rectangleCount = 1;

        public static void CreateRectangle(this Graphics g, List<DrawableObject> drawableObjects, TreeView treeView1, string name, Point A, Point B, Point C, Point D)
        {
            var vertexA = CreateVertex(g, drawableObjects, treeView1, A, true);
            var vertexB = CreateVertex(g, drawableObjects, treeView1, B, true);
            var vertexC = CreateVertex(g, drawableObjects, treeView1, C, true);
            var vertexD = CreateVertex(g, drawableObjects, treeView1, D, true);

            var rectangle = new Rect(vertexA, vertexB, vertexC, vertexD);

            rectangle.SetName($"Midpoint_{name}{rectangleCount++}");
            vertexA.SetName("A");
            vertexB.SetName("B");
            vertexC.SetName("C");
            vertexD.SetName("D");

            drawableObjects.Add(rectangle);

            TreeNode parent = new TreeNode($"{rectangle.Name}");
            TreeNode child1 = new TreeNode($"{vertexA.Name}");
            TreeNode child2 = new TreeNode($"{vertexB.Name}");
            TreeNode child3 = new TreeNode($"{vertexC.Name}");
            TreeNode child4 = new TreeNode($"{vertexD.Name}");

            parent.Nodes.Add(child1);
            parent.Nodes.Add(child2);
            parent.Nodes.Add(child3);
            parent.Nodes.Add(child4);

            treeView1.Nodes.Add(parent);
        }
        #endregion
    }
}
