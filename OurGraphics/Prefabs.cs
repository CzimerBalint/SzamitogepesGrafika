using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OurGraphics
{
    public static partial class GraphicsExtension
    {
        public class Prefabs
        {
            private static int triangleCount = 1;
            public static int rectangleCount = 1;
            public static int squareCount = 1;
            public static int deltoidCount = 1;
            public static int parallelogramCount = 1;

            private List<DrawableObject> DrawableObjects { get; set; }
            private TreeView TreeView1 { get; set; }


            public Prefabs(List<DrawableObject> drawableObjects, TreeView treeView1)
            {
                DrawableObjects = drawableObjects;
                TreeView1 = treeView1;
            }


            #region 3D
            public Vertex[] Cube_prefab(PointF wo)
            {
                Vertex[] vertices = new Vertex[]
                {
                   new Vertex(new Vector3(wo.X, wo.Y, 0)),               // A
                   new Vertex(new Vector3(wo.X + 100, wo.Y, 0)),         // B
                   new Vertex(new Vector3(wo.X + 100, wo.Y + 100, 0)),   // C
                   new Vertex(new Vector3(wo.X, wo.Y + 100, 0)),         // D
                   new Vertex(new Vector3(wo.X, wo.Y, 100)),             // E
                   new Vertex(new Vector3(wo.X + 100, wo.Y, 100)),       // F
                   new Vertex(new Vector3(wo.X + 100, wo.Y + 100, 100)), // G
                   new Vertex(new Vector3(wo.X, wo.Y + 100, 100))        // H
                };

                return vertices;

            }
            #endregion

            #region 2D

            #region triangle
            public Triangle CreateTriangle(PointF wo)
            {
                Vertex[] vertices = new Vertex[]
                {
                    new Vertex(new Vector3(wo.X, wo.Y, 0)),
                    new Vertex(new Vector3(wo.X + 100, wo.Y, 0)),
                    new Vertex(new Vector3(wo.X + 50, wo.Y + 100, 0))
                };

                var triangle = new Triangle(vertices)
                {
                    Name = $"Triangle{triangleCount++}"
                };

                DrawableObjects.Add(vertices[0]);
                DrawableObjects.Add(vertices[1]);
                DrawableObjects.Add(vertices[2]);
                DrawableObjects.Add(triangle);

                TreeNode parent = new TreeNode($"{triangle.Name}");
                TreeNode child1 = new TreeNode($"{vertices[0].Name}");
                TreeNode child2 = new TreeNode($"{vertices[1].Name}");
                TreeNode child3 = new TreeNode($"{vertices[2].Name}");
                parent.Nodes.Add(child1);
                parent.Nodes.Add(child2);
                parent.Nodes.Add(child3);

                TreeView1.Nodes.Add(parent);

                return triangle;
            }
            #endregion

            #region Rectangle
            public Rect CreateRectangle(PointF wo)
            {
                Vertex[] vertices = new Vertex[]
                {
                    new Vertex(new Vector3(wo.X, wo.Y, 0)),
                    new Vertex(new Vector3(wo.X, wo.Y + 100, 0)),
                    new Vertex(new Vector3(wo.X + 200, wo.Y + 100, 0)),
                    new Vertex(new Vector3(wo.X + 200, wo.Y, 0))
                };

                var rectangle = new Rect(vertices)
                {
                    Name = $"Rectangle{rectangleCount++}"
                };

                DrawableObjects.Add(vertices[0]);
                DrawableObjects.Add(vertices[1]);
                DrawableObjects.Add(vertices[2]);
                DrawableObjects.Add(vertices[3]);
                DrawableObjects.Add(rectangle);

                TreeNode parent = new TreeNode($"{rectangle.Name}");
                TreeNode child1 = new TreeNode($"{vertices[0].Name}");
                TreeNode child2 = new TreeNode($"{vertices[1].Name}");
                TreeNode child3 = new TreeNode($"{vertices[2].Name}");
                TreeNode child4 = new TreeNode($"{vertices[3].Name}");
                parent.Nodes.Add(child1);
                parent.Nodes.Add(child2);
                parent.Nodes.Add(child3);
                parent.Nodes.Add(child4);

                TreeView1.Nodes.Add(parent);

                return rectangle;
            }

            #endregion

            #region Square

            public Rect CreateSquare(PointF wo)
            {
                Vertex[] vertices = new Vertex[]
                {
                    new Vertex(new Vector3(wo.X, wo.Y, 0)),
                    new Vertex(new Vector3(wo.X, wo.Y + 100, 0)),
                    new Vertex(new Vector3(wo.X + 100, wo.Y + 100, 0)),
                    new Vertex(new Vector3(wo.X + 100, wo.Y, 0))
                };

                var square = new Rect(vertices)
                {
                    Name = $"Square{squareCount++}"
                };

                DrawableObjects.Add(vertices[0]);
                DrawableObjects.Add(vertices[1]);
                DrawableObjects.Add(vertices[2]);
                DrawableObjects.Add(vertices[3]);
                DrawableObjects.Add(square);

                TreeNode parent = new TreeNode($"{square.Name}");
                TreeNode child1 = new TreeNode($"{vertices[0].Name}");
                TreeNode child2 = new TreeNode($"{vertices[1].Name}");
                TreeNode child3 = new TreeNode($"{vertices[2].Name}");
                TreeNode child4 = new TreeNode($"{vertices[3].Name}");
                parent.Nodes.Add(child1);
                parent.Nodes.Add(child2);
                parent.Nodes.Add(child3);
                parent.Nodes.Add(child4);

                TreeView1.Nodes.Add(parent);

                return square;
            }


            #endregion

            #region Deltoid

            public Rect CreateDeltoid(PointF wo)
            {
                Vertex[] vertices = new Vertex[]
                {
                    new Vertex(new Vector3(wo.X + 25, wo.Y + 25, 0)),
                    new Vertex(new Vector3(wo.X, wo.Y + 100, 0)),
                    new Vertex(new Vector3(wo.X - 25, wo.Y + 25, 0)),
                    new Vertex(new Vector3(wo.X, wo.Y, 0))
                };

                var deltoid = new Rect(vertices)
                {
                    Name = $"Deltoid{deltoidCount++}"
                };

                DrawableObjects.Add(vertices[0]);
                DrawableObjects.Add(vertices[1]);
                DrawableObjects.Add(vertices[2]);
                DrawableObjects.Add(vertices[3]);
                DrawableObjects.Add(deltoid);

                TreeNode parent = new TreeNode($"{deltoid.Name}");
                TreeNode child1 = new TreeNode($"{vertices[0].Name}");
                TreeNode child2 = new TreeNode($"{vertices[1].Name}");
                TreeNode child3 = new TreeNode($"{vertices[2].Name}");
                TreeNode child4 = new TreeNode($"{vertices[3].Name}");
                parent.Nodes.Add(child1);
                parent.Nodes.Add(child2);
                parent.Nodes.Add(child3);
                parent.Nodes.Add(child4);

                TreeView1.Nodes.Add(parent);

                return deltoid;
            }


            #endregion

            #region Parallelogram

            public Rect CreateParallelogram(PointF wo)
            {
                Vertex[] vertices = new Vertex[]
                {
                    new Vertex(new Vector3(wo.X, wo.Y, 0)),
                    new Vertex(new Vector3(wo.X + 25, wo.Y + 50, 0)),
                    new Vertex(new Vector3(wo.X + 75, wo.Y + 50, 0)),
                    new Vertex(new Vector3(wo.X + 50, wo.Y, 0))
                };

                var parallelogram = new Rect(vertices)
                {
                    Name = $"Parallelogram{parallelogramCount++}"
                };

                DrawableObjects.Add(vertices[0]);
                DrawableObjects.Add(vertices[1]);
                DrawableObjects.Add(vertices[2]);
                DrawableObjects.Add(vertices[3]);
                DrawableObjects.Add(parallelogram);

                TreeNode parent = new TreeNode($"{parallelogram.Name}");
                TreeNode child1 = new TreeNode($"{vertices[0].Name}");
                TreeNode child2 = new TreeNode($"{vertices[1].Name}");
                TreeNode child3 = new TreeNode($"{vertices[2].Name}");
                TreeNode child4 = new TreeNode($"{vertices[3].Name}");
                parent.Nodes.Add(child1);
                parent.Nodes.Add(child2);
                parent.Nodes.Add(child3);
                parent.Nodes.Add(child4);

                TreeView1.Nodes.Add(parent);

                return parallelogram;
            }

            #endregion

            #region Circle
            #endregion


            #endregion
        }

    }
}