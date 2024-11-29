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

            private LineDrawingAlgo DrawingAlgo { get; set; }


            public Cube(Vertex[] vertices) : base("Cube", new Point())
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
                //top
                g.MidPoint(pen, A, B);
                g.MidPoint(pen, B, C);
                g.MidPoint(pen, C, D);
                g.MidPoint(pen, D, A);
                //middle
                g.MidPoint(pen, A, E);
                g.MidPoint(pen, B, F);
                g.MidPoint(pen, C, G);
                g.MidPoint(pen, D, H);
                //bottom
                g.MidPoint(pen, E, F);
                g.MidPoint(pen, F, G);
                g.MidPoint(pen, G, H);
                g.MidPoint(pen, H, E);

            }

            public override void Move(int deltaX, int deltaY)
            {
                A.Move(deltaX, deltaY);
                B.Move(deltaX, deltaY);
                C.Move(deltaX, deltaY);
                D.Move(deltaX, deltaY);
                E.Move(deltaX, deltaY);
                F.Move(deltaX, deltaY);
                G.Move(deltaX, deltaY);
                H.Move(deltaX, deltaY);
            }
        }


        private static int cubeCount = 1;


        public static void CreateCube(this Graphics g, List<DrawableObject> drawableObjects, TreeView treeView1, string name, Vertex[] vertices)
        {
            Vertex[] vertices_cube = new Vertex[]
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

            Cube cube = new Cube(vertices_cube);

            cube.SetName("Cube");
            for (int i = 0; i < vertices_cube.Length; i++)
            {
                vertices_cube[i].SetName($"Vertex{i}");
            }

            drawableObjects.Add(cube);

            TreeNode parent = new TreeNode("Cube");

            TreeNode[] children = new TreeNode[] 
            {
            new TreeNode(vertices_cube[0].Name),
            new TreeNode(vertices_cube[1].Name),
            new TreeNode(vertices_cube[2].Name),
            new TreeNode(vertices_cube[3].Name),
            new TreeNode(vertices_cube[4].Name),
            new TreeNode(vertices_cube[5].Name),
            new TreeNode(vertices_cube[6].Name),
            new TreeNode(vertices_cube[7].Name)
            };
            

            parent.Nodes.AddRange(children);

            treeView1.Nodes.Add(parent);

            






        }
    }
}
