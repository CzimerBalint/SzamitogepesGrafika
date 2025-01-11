using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static OurGraphics.GraphicsExtension;

namespace OurGraphics
{
    public static partial class GraphicsExtension
    {
        public class Prefabs
        {
            private static int vertexCount = 1;

            private static int MidPointCount = 1;
            private static int DDACount = 1;
            private static int triangleCount = 1;
            private static int rectangleCount = 1;
            private static int squareCount = 1;
            private static int deltoidCount = 1;
            private static int parallelogramCount = 1;
            private static int ellipseCount = 1;

            private static int cubeCount = 1;
            private static int tetraederCount = 1;
            private static int cylinderCount = 1;
            private static int ConeCount = 1;




            private List<DrawableObject> DrawableObjects { get; set; }
            private List<DrawableObject> GuideObjects { get; set; }
            private System.Windows.Forms.TreeView TreeView1 { get; set; }


            public Prefabs(List<DrawableObject> drawableObjects, List<DrawableObject> guideObjects, System.Windows.Forms.TreeView treeView1)
            {
                DrawableObjects = drawableObjects;
                GuideObjects = guideObjects;
                TreeView1 = treeView1;
            }


            #region 3D

            #region Cube
            public Cube CreateCube(PointF wo)
            {
                Vertex center = new Vertex(new Vector3(0, 0, 0));
                Vertex[] vertices = new Vertex[]
                {
                   new Vertex(new Vector3(wo.X, wo.Y, 0)),               // A
                   new Vertex(new Vector3(wo.X + 100, wo.Y, 0)),         // B
                   new Vertex(new Vector3(wo.X + 100, wo.Y + 100, 0)),   // C
                   new Vertex(new Vector3(wo.X, wo.Y + 100, 0)),         // D
                   new Vertex(new Vector3(wo.X, wo.Y, 100)),             // E
                   new Vertex(new Vector3(wo.X + 100, wo.Y, 100)),       // F
                   new Vertex(new Vector3(wo.X + 100, wo.Y + 100, 100)), // G
                   new Vertex(new Vector3(wo.X, wo.Y + 100, 100)),       // H
                };


                foreach (Vertex v in vertices)
                {
                    center.Location += v.Location;
                }
                center.Location /= vertices.Length;

                Vertex[] tmp = new Vertex[vertices.Length + 1];
                Array.Copy(vertices, tmp, vertices.Length);
                tmp[tmp.Length - 1] = center;

                Cube cube = new Cube(tmp);

                cube.SetName($"Cube{cubeCount++}");
                for (int i = 0; i < tmp.Length; i++)
                {
                    tmp[i].SetName($"Vertex{i}");
                }
                center.SetName("Center");

                DrawableObjects.Add(cube);
                DrawableObjects.AddRange(tmp);

                TreeNode parent = new TreeNode(cube.Name);

                TreeNode[] children = new TreeNode[]
                {
                    new TreeNode(tmp[0].Name),
                    new TreeNode(tmp[1].Name),
                    new TreeNode(tmp[2].Name),
                    new TreeNode(tmp[3].Name),
                    new TreeNode(tmp[4].Name),
                    new TreeNode(tmp[5].Name),
                    new TreeNode(tmp[6].Name),
                    new TreeNode(tmp[7].Name),
                    new TreeNode(tmp[8].Name)
                };

                parent.Nodes.AddRange(children);
                TreeView1.Nodes.Add(parent);

                return cube;

            }
            #endregion

            #region Tetraeder
            public Tetraeder CreateTetra(PointF wo)
            {
                Vertex center = new Vertex(new Vector3(0, 0, 0));
                Vertex[] vertices = new Vertex[]
                {
                   new Vertex(new Vector3(wo.X, wo.Y, 0)),              // A
                   new Vertex(new Vector3(wo.X+100, wo.Y, 0)),          // B
                   new Vertex(new Vector3(wo.X, wo.Y+100, 0)),          // C
                   new Vertex(new Vector3(wo.X, wo.Y, 100)),            // D
                  
                };

                foreach (Vertex v in vertices)
                {
                    center.Location += v.Location;
                }
                center.Location /= vertices.Length;

                Vertex[] tmp = new Vertex[vertices.Length + 1];
                Array.Copy(vertices, tmp, vertices.Length);
                tmp[vertices.Length] = center;

                Tetraeder tetraeder = new Tetraeder(tmp);

                tetraeder.SetName($"Tetraeder{tetraederCount++}");
                for (int i = 0; i < tmp.Length; i++)
                {
                    tmp[i].SetName($"Vertex{i}");
                }
                center.SetName("Center");

                DrawableObjects.Add(tetraeder);
                DrawableObjects.AddRange(tmp);

                TreeNode parent = new TreeNode(tetraeder.Name);

                TreeNode[] children = new TreeNode[]
                {
                    new TreeNode(tmp[0].Name),
                    new TreeNode(tmp[1].Name),
                    new TreeNode(tmp[2].Name),
                    new TreeNode(tmp[3].Name),
                    new TreeNode(tmp[4].Name),

                };

                parent.Nodes.AddRange(children);
                TreeView1.Nodes.Add(parent);

                return tetraeder;

            }
            #endregion

            #region Cylinder
            public Cylinder CreateCylinder(PointF wo)
            {
                Vertex[] Bottomvertices = new Vertex[16];
                Vertex[] TopVertices = new Vertex[16];
                float angleIncrement = 360.0f / 16;
                float angle = 0.0f;

                float radius = 200;
                float height = 100;

                Vertex center = new Vertex(new Vector3(wo.X, wo.Y, height));

                for (int i = 0; i < 16; i++)
                {
                    float x = wo.X + radius * (float)Math.Cos(angle * Math.PI / 180.0);
                    float z = wo.Y + radius * (float)Math.Sin(angle * Math.PI / 180.0); // float z mert nekem az y a függőleges
                    Bottomvertices[i] = new Vertex(new Vector3(x, 0, z)) { Name = $"AlapVertex{i}" };
                    TopVertices[i] = new Vertex(new Vector3(x, height, z)) { Name = $"TopVertex{i}" };
                    angle += angleIncrement;

                }


                Cylinder cylinder = new Cylinder(Bottomvertices, TopVertices, center, radius, height)
                {
                    Name = $"Henger{cylinderCount++}"
                };
                center.SetName("Center");

                DrawableObjects.Add(cylinder);
                DrawableObjects.AddRange(Bottomvertices);
                DrawableObjects.AddRange(TopVertices);
                DrawableObjects.Add(center);

                TreeNode parent = new TreeNode(cylinder.Name);
                for (int i = 0; i < 16; i++)
                {
                    parent.Nodes.Add(new TreeNode(Bottomvertices[i].Name));
                    parent.Nodes.Add(new TreeNode(TopVertices[i].Name));
                }
                parent.Nodes.Add(center.Name);
                TreeView1.Nodes.Add(parent);


                return cylinder;

            }

            #endregion

            #region Cone
            public Cone CreateCone(PointF wo)
            {
                float angleIncrement = 360.0f / 16;
                float angle = 0.0f;

                float radius = 100;
                float height = 200;

                Vertex[] BottomVertices = new Vertex[16];
                Vertex TopVertex = new Vertex(new Vector3(wo.X, wo.Y, height)) { Name = "ConeTop" };


                for (int i = 0; i < 16; i++)
                {
                    float x = wo.X + radius * (float)Math.Cos(angle * Math.PI / 180.0);
                    float y = wo.Y + radius * (float)Math.Sin(angle * Math.PI / 180.0);
                    BottomVertices[i] = new Vertex(new Vector3(x, y, 0)) { Name = $"BaseVertex{i}" };
                    angle += angleIncrement;
                }

                Cone cone = new Cone(BottomVertices, TopVertex, new Vertex(wo), radius, height)
                {
                    Name = $"Kúp{ConeCount++}"
                };

                DrawableObjects.Add(cone);
                DrawableObjects.AddRange(BottomVertices);
                DrawableObjects.Add(TopVertex);

                TreeNode parent = new TreeNode(cone.Name);
                for (int i = 0; i < 16; i++)
                {
                    parent.Nodes.Add(new TreeNode(BottomVertices[i].Name));
                }
                parent.Nodes.Add(new TreeNode(TopVertex.Name));
                TreeView1.Nodes.Add(parent);


                return cone;

            }
            #endregion

            #region FaceBuilder

            public FaceBuilder Face(Vector3 wo, List<Vector3> locations, string[] ConnectionOrder)
            {
                Vertex center = new Vertex(new Vector3(0,0,0));
                List<Vertex> temp = new List<Vertex>();
                foreach (Vector3 v in locations)
                {
                    temp.Add(new Vertex(new Vector3(wo.X + v.X*100,
                                                    wo.Y + v.Y*100,
                                                    wo.Z + v.Z*100)));//ha a lustaság fája ordítanék de szerencsére nem fáj ez pedig működik
                }
                
                foreach (Vertex v in temp)
                {
                    center.Location += v.Location;
                }
                center.Location /= temp.Count;

                temp.Add(center);   

                FaceBuilder face = new FaceBuilder(temp, ConnectionOrder);
                
                DrawableObjects.Add(face);
                DrawableObjects.AddRange(temp);

                return face;
            } 

            #endregion

            #region AxisGuide
            public AxisGuide CreateAxisGuide(PointF wo)
            {
                Vertex[] vertices = new Vertex[]
                {
                   new Vertex(new Vector3(wo.X, wo.Y, 0 )),          // A Közép

                   //Pozitív
                   new Vertex(new Vector3(wo.X + 50, wo.Y, 0)),     // B
                   new Vertex(new Vector3(wo.X, wo.Y - 50, 0)),     // C
                   new Vertex(new Vector3(wo.X, wo.Y, 50)),         // D

                   //Negatív
                   new Vertex(new Vector3(wo.X - 50, wo.Y, 0)),     // E
                   new Vertex(new Vector3(wo.X, wo.Y + 50, 0)),     // F
                   new Vertex(new Vector3(wo.X, wo.Y, -50)),        // G

                };

                AxisGuide axisGuide = new AxisGuide(vertices);

                GuideObjects.Add(axisGuide);

                return axisGuide;

            }
            #endregion


           



            #endregion

            #region 2D

            #region DDA

            public Line CreateDDA(PointF wo)
            {
                Vertex[] vertices = new Vertex[]
                {
                    new Vertex(new Vector3(wo.X, wo.Y,0)),
                    new Vertex(new Vector3(wo.X + 100, wo.Y,0)),

                };

                var DDA = new Line(vertices,LineDrawingAlgo.DDA)
                {
                    Name = $"DDA_Line{DDACount++}"
                };

                DrawableObjects.Add(vertices[0]);
                DrawableObjects.Add(vertices[1]);
                DrawableObjects.Add(DDA);

                TreeNode parent = new TreeNode($"{DDA.Name}");
                TreeNode child1 = new TreeNode($"{DDA.Start.Name}");
                TreeNode child2 = new TreeNode($"{DDA.End.Name}");
                parent.Nodes.Add(child1);
                parent.Nodes.Add(child2);

                TreeView1.Nodes.Add(parent);

               
                return DDA;
            }

            #endregion

            #region MidPoint
            public Line CreateMidPoint(PointF wo)
            {
                Vertex[] vertices = new Vertex[]
                {
                    new Vertex(new Vector3(wo.X, wo.Y,0)),
                    new Vertex(new Vector3(wo.X + 100, wo.Y,0)),

                };

                var MidPoint = new Line(vertices, LineDrawingAlgo.Midpoint)
                {
                    Name = $"MidPoint_Line{MidPointCount++}"
                };

                DrawableObjects.Add(vertices[0]);
                DrawableObjects.Add(vertices[1]);
                DrawableObjects.Add(MidPoint);

                TreeNode parent = new TreeNode($"{MidPoint.Name}");
                TreeNode child1 = new TreeNode($"{MidPoint.Start.Name}");
                TreeNode child2 = new TreeNode($"{MidPoint.End.Name}");
                parent.Nodes.Add(child1);
                parent.Nodes.Add(child2);

                TreeView1.Nodes.Add(parent);


                return MidPoint;
            }

            #endregion

            #region Triangle
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
            public Ellipse CreateCircle(PointF wo)
            {
                Vertex[] vertices = new Vertex[]
                {
                    new Vertex(new Vector3(wo.X,wo.Y,0)),
                    new Vertex(new Vector3(wo.X+100,wo.Y,0)),
                    new Vertex(new Vector3(wo.X,wo.Y+100,0)),
                };

                // Create the ellipse
                Ellipse circle = new Ellipse(vertices)
                {
                    Name = $"Circle{ellipseCount++}"
                };

                // Add ellipse to drawable objects
                DrawableObjects.Add(circle);
                DrawableObjects.AddRange(vertices);

                // Create tree view structure
                TreeNode parent = new TreeNode($"{circle.Name}");

                TreeNode centerNode = new TreeNode($"Közép");
                TreeNode majorRadiusNode = new TreeNode($"X méret");
                TreeNode minorRadiusNode = new TreeNode($"Y méret");

                // Add nodes to tree view
                parent.Nodes.Add(centerNode);
                parent.Nodes.Add(majorRadiusNode);
                parent.Nodes.Add(minorRadiusNode);

                TreeView1.Nodes.Add(parent);

                return circle;
            }


            #endregion

            #region Ellipse

            public Ellipse CreateEllipse(PointF wo)
            {
                Vertex[] vertices = new Vertex[]
                {
                    new Vertex(new Vector3(wo.X,wo.Y,0)),
                    new Vertex(new Vector3(wo.X+150,wo.Y,0)),
                    new Vertex(new Vector3(wo.X,wo.Y+100,0)),
                };

                // Create the ellipse
                Ellipse ellipse = new Ellipse(vertices)
                {
                    Name = $"Ellipse{ellipseCount++}"
                };

                // Add ellipse to drawable objects
                DrawableObjects.Add(ellipse);
                DrawableObjects.AddRange(vertices);

                // Create tree view structure
                TreeNode parent = new TreeNode($"{ellipse.Name}");

                TreeNode centerNode = new TreeNode($"Közép");
                TreeNode majorRadiusNode = new TreeNode($"X méret");
                TreeNode minorRadiusNode = new TreeNode($"Y méret");

                // Add nodes to tree view
                parent.Nodes.Add(centerNode);
                parent.Nodes.Add(majorRadiusNode);
                parent.Nodes.Add(minorRadiusNode);

                TreeView1.Nodes.Add(parent);

                return ellipse;
            }
            #endregion


            #endregion

            #region 1D

            #region Vertex

            public Vertex CreateVertex(PointF wo)
            {
                Vertex vertex = new Vertex(new Vector3(wo.X, wo.Y, 0))
                {
                    Name = $"Vertex{vertexCount++}"
                };

                DrawableObjects.Add(vertex);
                TreeView1.Nodes.Add(new TreeNode(vertex.Name));
                return vertex;
            }

            #endregion

            #endregion
        }

    }
}