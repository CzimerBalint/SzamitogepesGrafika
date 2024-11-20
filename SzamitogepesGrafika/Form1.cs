﻿using System;
using System.Collections.Generic;
using System.Drawing;
using OurGraphics;
using System.Windows.Forms;
using static OurGraphics.GraphicsExtension;
using System.Diagnostics;

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
                BurnShape(drawableObjects, treeView1);
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


        private void interface2d_Paint(object sender, PaintEventArgs e)
        {

            g = e.Graphics;
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

                default:
                    break;
            }

           
        }

        private void interface2d_MouseUp(object sender, MouseEventArgs e)
        {
            selectedVertex = null;
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


    }
}
