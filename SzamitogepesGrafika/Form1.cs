﻿using System;
using System.Collections.Generic;
using System.Drawing;
using OurGraphics;
using System.Windows.Forms;
using static OurGraphics.GraphicsExtension;
using System.Diagnostics;
using System.Linq;

namespace SzamitogepesGrafika
{
    public partial class Form1 : Form
    {
        public Graphics g;
        public Point WorldOrigin;
        public List<DrawableObject> drawableObjects;
        private Vertex selectedVertex = null;
        private Point lastMousePos;
        public Bitmap bmp;
        private List<Vertex> selectedVertices = new List<Vertex>();


        private bool MBM_isDown = false; //MBM mouse button Middle
        private Point MBM_first;
        private Point MBM_last;


        //kamera
        private Matrix4 projectionMatrix;


        public Form1()
        {
            InitializeComponent();
            WorldOrigin = g.WorldOrigin(interface2d.Width, interface2d.Height);
            drawableObjects = new List<DrawableObject>(); // Lista inicializálása
           
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "ScreenSpace: {X=NaN,Y=NaN}";
            toolStripStatusLabel2.Text = "Worldspace: {X=NaN,Y=NaN}";
            CreateImage(interface2d.Width, interface2d.Height);
            interface2d.Image = bmp;
            InitializeProjectionMatrix();
            interface2d.Invalidate();


        }

        #region Vertex
        private void Option_Add_Vertex_Click(object sender, EventArgs e)
        {
            
             g.CreateVertex(drawableObjects, treeView1, WorldOrigin);

            
            interface2d.Invalidate();
        }
        private void Burn_Shape_Click(object sender, EventArgs e)
        {

            if (drawableObjects.Count == 0)
            {
                MessageBox.Show("Nem lehet mit véglegesíteni!");
            }
            else
            {
                // Merge the selected vertices
                g.BurnShape(drawableObjects, treeView1, bmp);
                MessageBox.Show($"Égetés sikeres!");

                // Clear the selected vertices list
                selectedVertices.Clear();

                // Redraw the form
                interface2d.Invalidate();
            }
        

        }
        #endregion

        #region Lines
        private void Option_DDA_Click(object sender, EventArgs e)
        {
            g.CreateLine(drawableObjects, treeView1, WorldOrigin, new Point(WorldOrigin.X + 100, WorldOrigin.Y), LineDrawingAlgo.DDA);
            interface2d.Invalidate();
        }
        private void Option_MidPoint_Click(object sender, EventArgs e)
        {
            
            g.CreateLine(drawableObjects, treeView1, WorldOrigin, new Point(WorldOrigin.X + 100, WorldOrigin.Y), LineDrawingAlgo.Midpoint);
            interface2d.Invalidate();
        }
        #endregion

        #region Triangles
        private void Option_Triangle_Click(object sender, EventArgs e)
        {
            g.CreateTriangle(drawableObjects, treeView1, WorldOrigin, new Point(WorldOrigin.X + 100, WorldOrigin.Y), new Point(WorldOrigin.X + 50, WorldOrigin.Y + 100));

            interface2d.Invalidate();
        }
        #endregion

        #region Rectangles

        private void Option_Rectangle_Click(object sender, EventArgs e)
        {
            g.CreateRectangle(drawableObjects, treeView1, "Rectangle", WorldOrigin, new Point(WorldOrigin.X, WorldOrigin.Y + 100), new Point(WorldOrigin.X + 200, WorldOrigin.Y + 100), new Point(WorldOrigin.X + 200, WorldOrigin.Y));
            interface2d.Invalidate();
        }

        private void Option_Square_Click(object sender, EventArgs e)
        {
            g.CreateRectangle(drawableObjects, treeView1, "Square", WorldOrigin, new Point(WorldOrigin.X, WorldOrigin.Y + 100), new Point(WorldOrigin.X + 100, WorldOrigin.Y + 100), new Point(WorldOrigin.X + 100, WorldOrigin.Y));
            interface2d.Invalidate();
        }

        private void Option_Deltoid_Click(object sender, EventArgs e)
        {
            g.CreateRectangle(drawableObjects, treeView1, "Deltoid", new Point(WorldOrigin.X + 25, WorldOrigin.Y + 25), new Point(WorldOrigin.X, WorldOrigin.Y + 100), new Point(WorldOrigin.X - 25, WorldOrigin.Y + 25), WorldOrigin);
            interface2d.Invalidate();
        }

        private void Option_Parallelogram_Click(object sender, EventArgs e)
        {
            g.CreateRectangle(drawableObjects, treeView1, "Parallelogram", WorldOrigin, new Point(WorldOrigin.X + 25, WorldOrigin.Y + 50), new Point(WorldOrigin.X + 75, WorldOrigin.Y + 50), new Point(WorldOrigin.X + 50, WorldOrigin.Y));
            interface2d.Invalidate();
        }
        
        #endregion

        #region Circles
        private void Option_Aritmetic_Circle_Click(object sender, EventArgs e)
        {
            g.CreateCircle(drawableObjects, treeView1, WorldOrigin, new Point(WorldOrigin.X + 100, WorldOrigin.Y));
            interface2d.Invalidate();
        }
        private void Option_MidPointCircle_Click(object sender, EventArgs e) // yet to implement
        {
            bmp.Save("kaka.jpg");
        }
        #endregion

        #region Drawing to the interface

        private void CreateImage(int w, int h)
        {
            bmp = new Bitmap(w, h);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
            }
        }

        private void InitializeProjectionMatrix()
        {
            float fov = (float)(Math.PI / 2); // 90 fok
            float aspectRatio = (float)interface2d.Width / interface2d.Height;
            float near = 0.1f;
            float far = 1f;

            projectionMatrix = CentralProjection.CreatePojectionMat(fov, aspectRatio, near, far);
        }


        private void interface2d_Paint(object sender, PaintEventArgs e)
        {

            g = e.Graphics;

            List<Vector3> points = new List<Vector3>
            {
                new Vector3(0, 1, 5),   // P1
                new Vector3(-1, -1, 6), // P2
                new Vector3(1, -1, 6),  // P3
                new Vector3(0, -1.2f, 4)   // P4
            };

            // Tetraéder élei (indexek a pontok listájára)
            List<(int, int)> edges = new List<(int, int)>
            {
                (0, 1), (0, 2), (0, 3), // Csúcsból bázisokba
                (1, 2), (1, 3), (2, 3)  // Bázis élek
            };

            // Skálázás mértéke
            float scale = 1f; // Ennyivel nagyobb méretű lesz a tetraéder

            // Élek kirajzolása
            foreach (var edge in edges)
            {
                // Az él kezdő- és végpontja
                Vector3 start3D = points[edge.Item1];
                Vector3 end3D = points[edge.Item2];

                // Pontok kivetítése 2D-re
                Vector2 start2D = CentralProjection.ProjectPoint(start3D, projectionMatrix);
                Vector2 end2D = CentralProjection.ProjectPoint(end3D, projectionMatrix);

                // Skálázás és eltolás középre
                float startX = start2D.X * (interface2d.Width / 2) * scale + (interface2d.Width / 2);
                float startY = -start2D.Y * (interface2d.Height / 2) * scale + (interface2d.Height / 2);
                float endX = end2D.X * (interface2d.Width / 2) * scale + (interface2d.Width / 2);
                float endY = -end2D.Y * (interface2d.Height / 2) * scale + (interface2d.Height / 2);

                // Él rajzolása
                g.DrawLine(Pens.Black, (int)startX, (int)startY, (int)endX, (int)endY);
            }



            //Debug.WriteLine($"{drawableObjects.Count}, {drawableObjects.Capacity}");
            foreach (var drawable in drawableObjects)
            {
                //Debug.WriteLine($"{drawable.Name}, {drawable.Location}");
               
                drawable.Draw(g);


            }




        }

        



        private void interface2d_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = $"ScreenSpace: {e.Location}";
            toolStripStatusLabel2.Text = $"Worldspace: {g.objectToWorldOrigin(WorldOrigin, e.Location)}";

            if (selectedVertex != null)
            {
                // Calculate the movement delta
                int deltaX = e.Location.X - lastMousePos.X;
                int deltaY = e.Location.Y - lastMousePos.Y;

                // Move the selected vertex
                selectedVertex.Move(deltaX, deltaY);

                // Update the last mouse position
                lastMousePos = e.Location;

                // Redraw the form
                interface2d.Invalidate();
            }

            if (MBM_isDown) 
            {
                
            }



        }

        private void interface2d_Resize(object sender, EventArgs e)
        {
            WorldOrigin = g.WorldOrigin(interface2d.Width, interface2d.Height);
            toolStripStatusLabel3.Text = WorldOrigin.ToString();
            
        }

        private void interface2d_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {

                case MouseButtons.Left:
                    
                    selectedVertex = null;

                    // Deselect all vertices
                    foreach (DrawableObject obj in drawableObjects)
                    {
                        if (obj is Vertex vertex)
                        {
                            vertex.Deselect();
                        }
                    }

                    // Check if any vertex is clicked
                    foreach (DrawableObject obj in drawableObjects)
                    {
                        if (obj is Vertex vertex && vertex.Contains(e.Location))
                        {
                            vertex.Select();
                            selectedVertex = vertex;

                            // Kiválasztott vertex ellenőrzése hogy tartalmazza-e a lista
                            if (!selectedVertices.Contains(vertex))
                            {
                                selectedVertices.Add(vertex);
                            }

                            lastMousePos = e.Location;  // Remember the initial position of the mouse
                            break;
                        }
                    }

                    
                    // Redraw the form
                    interface2d.Invalidate();
                    break;

                case MouseButtons.Right:
                    bmp.FillS4(e.Location.X, e.Location.Y, bmp.GetPixel(e.X, e.Y), Color.Aqua);
                    interface2d.Invalidate();
                    break;

                case MouseButtons.Middle:
                    MBM_first = new Point(e.X, e.Y);
                    Debug.WriteLine("First: "+MBM_first);

                    MBM_isDown = true;
                    break;


                default:
                    break;
            }

           
        }

        private void interface2d_MouseUp(object sender, MouseEventArgs e)
        {
            selectedVertex = null;

            MBM_last = new Point(e.X, e.Y); //worldOrigint használni
            Debug.WriteLine("Last: "+MBM_last);
            MBM_isDown = false;
           
           
        }





        #endregion

        private void Form1_Resize(object sender, EventArgs e)
        {
            CreateImage(interface2d.Width, interface2d.Height);
            interface2d.Image = bmp;
            interface2d.Invalidate();
        }

        private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
        {
            CreateImage(interface2d.Width, interface2d.Height);
            interface2d.Image = bmp;
            interface2d.Invalidate();
        }

        private void AddCube_Click(object sender, EventArgs e)
        {




            Vertex[] vertices = new Vertex[]
            {
                new Vertex(new Point(WorldOrigin.X, WorldOrigin.Y)),               // A
                new Vertex(new Point(WorldOrigin.X + 100, WorldOrigin.Y)),         // B
                new Vertex(new Point(WorldOrigin.X + 100, WorldOrigin.Y + 100)),   // C
                new Vertex(new Point(WorldOrigin.X, WorldOrigin.Y + 100)),         // D
                new Vertex(new Point(WorldOrigin.X, WorldOrigin.Y - 100)),         // E
                new Vertex(new Point(WorldOrigin.X + 100, WorldOrigin.Y - 100)),   // F
                new Vertex(new Point(WorldOrigin.X + 100, WorldOrigin.Y)),         // G
                new Vertex(new Point(WorldOrigin.X, WorldOrigin.Y - 100))          // H
            };

            g.CreateCube(drawableObjects, treeView1, "Cube", vertices);
            interface2d.Invalidate();
        }
    }
}
