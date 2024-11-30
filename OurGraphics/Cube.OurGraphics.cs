using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OurGraphics
{
    public static partial class GraphicsExtension
    {
        public class Cube : DrawableObject
        {
            public Vertex A { get; set; }
            public Vertex B { get; set; }
            public Vertex C { get; set; }
            public Vertex D { get; set; }
            public Vertex E { get; set; }
            public Vertex F { get; set; }
            public Vertex G { get; set; }
            public Vertex H { get; set; }

            public Cube(Vertex[] vertices) : base("Cube", vertices[0].Location)
            {
                A = vertices[0];
                B = vertices[1];
                C = vertices[2];
                D = vertices[3];
                E = vertices[4];
                F = vertices[5];
                G = vertices[6];
                H = vertices[7];
            }

            public void SetName(string name)
            {
                Name = name;
            }

            public override void Draw(Graphics g)
            {
                Pen pen = Pens.Black;
                // top
                g.MidPoint(pen, A, B);
                g.MidPoint(pen, B, C);
                g.MidPoint(pen, C, D);
                g.MidPoint(pen, D, A);
                // middle
                g.MidPoint(pen, A, E);
                g.MidPoint(pen, B, F);
                g.MidPoint(pen, C, G);
                g.MidPoint(pen, D, H);
                // bottom
                g.MidPoint(pen, E, F);
                g.MidPoint(pen, F, G);
                g.MidPoint(pen, G, H);
                g.MidPoint(pen, H, E);
            }

            public override void Move(float deltaX, float deltaY, float deltaZ)
            {
                A.Move(deltaX, deltaY, deltaZ);
                B.Move(deltaX, deltaY, deltaZ);
                C.Move(deltaX, deltaY, deltaZ);
                D.Move(deltaX, deltaY, deltaZ);
                E.Move(deltaX, deltaY, deltaZ);
                F.Move(deltaX, deltaY, deltaZ);
                G.Move(deltaX, deltaY, deltaZ);
                H.Move(deltaX, deltaY, deltaZ);
            }

            // Új transzformációs metódus
            public override void Transform(Matrix4 transformation)
            {
                A.Transform(transformation);
                B.Transform(transformation);
                C.Transform(transformation);
                D.Transform(transformation);
                E.Transform(transformation);
                F.Transform(transformation);
                G.Transform(transformation);
                H.Transform(transformation);
            }
        }

        private static int cubeCount = 1;

        public static void CreateCube(this Graphics g, List<DrawableObject> drawableObjects, TreeView treeView1, string name, Vertex[] vertices)
        {
            Vertex[] verticesCube = new Vertex[]
            {
                CreateVertex(g, drawableObjects, treeView1, vertices[0].Location, true),
                CreateVertex(g, drawableObjects, treeView1, vertices[1].Location, true),
                CreateVertex(g, drawableObjects, treeView1, vertices[2].Location, true),
                CreateVertex(g, drawableObjects, treeView1, vertices[3].Location, true),
                CreateVertex(g, drawableObjects, treeView1, vertices[4].Location, true),
                CreateVertex(g, drawableObjects, treeView1, vertices[5].Location, true),
                CreateVertex(g, drawableObjects, treeView1, vertices[6].Location, true),
                CreateVertex(g, drawableObjects, treeView1, vertices[7].Location, true)
            };

            Cube cube = new Cube(verticesCube);

            cube.SetName($"{name}{cubeCount++}");
            for (int i = 0; i < verticesCube.Length; i++)
            {
                verticesCube[i].SetName($"Vertex{i}");
            }

            drawableObjects.Add(cube);

            TreeNode parent = new TreeNode(cube.Name);

            TreeNode[] children = new TreeNode[]
            {
                new TreeNode(verticesCube[0].Name),
                new TreeNode(verticesCube[1].Name),
                new TreeNode(verticesCube[2].Name),
                new TreeNode(verticesCube[3].Name),
                new TreeNode(verticesCube[4].Name),
                new TreeNode(verticesCube[5].Name),
                new TreeNode(verticesCube[6].Name),
                new TreeNode(verticesCube[7].Name)
            };

            parent.Nodes.AddRange(children);
            treeView1.Nodes.Add(parent);
        }
    }
}
