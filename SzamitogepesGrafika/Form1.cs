using OurGraphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static OurGraphics.GraphicsExtension;

namespace SzamitogepesGrafika
{
    public partial class Form1 : Form
    {
        public Graphics g;
        public Point WorldOrigin;
        public Prefabs prefabs;
        public List<DrawableObject> drawableObjects;
        private Vertex selectedVertex = null;
        private Point lastMousePos;
        public Bitmap bmp;
        private List<Vertex> selectedVertices = new List<Vertex>();

        private bool MBM_isDown = false; // MBM mouse button Middle
        private Point MBM_first;
        private Point MBM_last;



        public Form1()
        {
            InitializeComponent();

            WorldOrigin = new Point(interface2d.Width / 2, interface2d.Height / 2);

            drawableObjects = new List<DrawableObject>();

            prefabs = new Prefabs(drawableObjects,treeView1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateImage(splitContainer1.Panel1.Size);
            interface2d.Image = bmp;


            toolStripStatusLabel1.Text = "ScreenSpace: {X=NaN,Y=NaN}";
            toolStripStatusLabel2.Text = "Worldspace: {X=NaN,Y=NaN}";
        }

        #region Vertex
        private void Option_Add_Vertex_Click(object sender, EventArgs e)
        {
            Vertex vertex = new Vertex(new Vector3(WorldOrigin.X, WorldOrigin.Y, 0))
            {
                Name = $"Vertex{drawableObjects.Count + 1}"
            };
            drawableObjects.Add(vertex);
            treeView1.Nodes.Add(new TreeNode(vertex.Name));
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
                g.BurnShape(drawableObjects, treeView1, bmp);
                MessageBox.Show($"Égetés sikeres!");
                selectedVertices.Clear();
                interface2d.Invalidate();
            }
        }
        #endregion

        #region Lines
        private void Option_DDA_Click(object sender, EventArgs e)
        {
            Vertex start = new Vertex(new Vector3(WorldOrigin.X, WorldOrigin.Y, 0));
            Vertex end = new Vertex(new Vector3(WorldOrigin.X + 100, WorldOrigin.Y, 0));
            Line line = g.CreateLine(drawableObjects, treeView1, start, end, LineDrawingAlgo.DDA);
            drawableObjects.Add(line);
            interface2d.Invalidate();
        }

        private void Option_MidPoint_Click(object sender, EventArgs e)
        {
            Vertex start = new Vertex(new Vector3(WorldOrigin.X, WorldOrigin.Y, 0));
            Vertex end = new Vertex(new Vector3(WorldOrigin.X + 100, WorldOrigin.Y, 0));
            Line line = g.CreateLine(drawableObjects, treeView1, start, end, LineDrawingAlgo.Midpoint);
            drawableObjects.Add(line);
            interface2d.Invalidate();
        }
        #endregion

        #region Triangles
        private void Option_Triangle_Click(object sender, EventArgs e)
        {
            Triangle triangle = prefabs.CreateTriangle(WorldOrigin);
            interface2d.Invalidate();
        }
        #endregion

        #region Rectangles
        private void Option_Rectangle_Click(object sender, EventArgs e)
        {
            Rect rectangle = prefabs.CreateRectangle(WorldOrigin);
            interface2d.Invalidate();
        }

        private void Option_Square_Click(object sender, EventArgs e)
        {   
            Rect square = prefabs.CreateSquare(WorldOrigin);
            drawableObjects.Add(square);
            interface2d.Invalidate();
        }

        private void Option_Deltoid_Click(object sender, EventArgs e)
        {
            
            Rect deltoid = prefabs.CreateDeltoid(WorldOrigin);
            drawableObjects.Add(deltoid);
            interface2d.Invalidate();
        }

        private void Option_Parallelogram_Click(object sender, EventArgs e)
        {
            Rect parallelogram = prefabs.CreateParallelogram(WorldOrigin);
            drawableObjects.Add(parallelogram);
            interface2d.Invalidate();
        }
        #endregion

        #region Circles
        private void Option_Aritmetic_Circle_Click(object sender, EventArgs e)
        {
            Vertex center = new Vertex(new Vector3(WorldOrigin.X, WorldOrigin.Y, 0));
            Vertex radius = new Vertex(new Vector3(WorldOrigin.X + 100, WorldOrigin.Y, 0));
            Circle circle = g.CreateCircle(drawableObjects, treeView1, center, radius);
            drawableObjects.Add(circle);
            interface2d.Invalidate();
        }
        #endregion

        #region Drawing to the interface
        private void CreateImage(Size size)
        {
            bmp = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
            }

        }

        private void interface2d_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            foreach (var drawable in drawableObjects)
            {
                drawable.Draw(g);
            }
        }

        private void interface2d_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = $"ScreenSpace: {e.Location}";
            toolStripStatusLabel2.Text = $"Worldspace: {g.objectToWorldOrigin(WorldOrigin, e.Location)}";

            if (selectedVertex != null)
            {
                int deltaX = e.Location.X - lastMousePos.X;
                int deltaY = e.Location.Y - lastMousePos.Y;

                selectedVertex.Move(deltaX, deltaY, 0);
                lastMousePos = e.Location;
                interface2d.Invalidate();
            }

            if (MBM_isDown)
            {
                if (Control.ModifierKeys == Keys.Shift)
                {
                    // Mozgatás jobbra/balra/le/fel, ha a Shift lenyomva van
                    int deltaX = e.Location.X - MBM_first.X;
                    int deltaY = e.Location.Y - MBM_first.Y;

                    foreach (var drawable in drawableObjects)
                    {
                        drawable.Move(deltaX, deltaY, 0);
                    }

                    MBM_first = e.Location;
                }                
                else
                {
                    int deltaX = e.Location.X - MBM_first.X;
                    int deltaY = e.Location.Y - MBM_first.Y;

                    float angleX = deltaY * 0.005f;
                    float angleY = deltaX * 0.005f;

                    Matrix4 rotationX = Matrix4.CreateRotationX(angleX);
                    Matrix4 rotationY = Matrix4.CreateRotationY(angleY);
                    Matrix4 rotation = rotationX * rotationY;

                    foreach (var drawable in drawableObjects)
                    {
                        drawable.Transform(rotation);
                    }

                    MBM_first = e.Location;
                }

                interface2d.Invalidate();
            }
        }

        private void interface2d_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    selectedVertex = null;

                    foreach (DrawableObject obj in drawableObjects)
                    {
                        if (obj is Vertex vertex)
                        {
                            vertex.Deselect();
                        }
                    }

                    foreach (DrawableObject obj in drawableObjects)
                    {
                        if (obj is Vertex vertex && vertex.Contains(e.Location))
                        {
                            vertex.Select();
                            selectedVertex = vertex;

                            if (!selectedVertices.Contains(vertex))
                            {
                                selectedVertices.Add(vertex);
                            }

                            lastMousePos = e.Location;
                            break;
                        }
                    }

                    interface2d.Invalidate();
                    break;

                case MouseButtons.Right:
                    bmp.FillS4(e.Location.X, e.Location.Y, bmp.GetPixel(e.X, e.Y), Color.Aqua);
                    interface2d.Invalidate();
                    break;

                case MouseButtons.Middle:
                    MBM_first = new Point(e.X, e.Y);
                    MBM_isDown = true;
                    break;
            }
        }

        private void interface2d_MouseUp(object sender, MouseEventArgs e)
        {
            selectedVertex = null;
            MBM_last = new Point(e.X, e.Y);
            Debug.WriteLine("Last: " + MBM_last);
            MBM_isDown = false;
        }
        #endregion

        private void Option_MidPointCircle_Click(object sender, EventArgs e)
        {
            MessageBox.Show("MidPointCircle function is yet to be implemented.");
        }

        private void interface2d_Resize(object sender, EventArgs e)
        {
            WorldOrigin = new Point(interface2d.Width / 2, interface2d.Height / 2);

            interface2d.Invalidate();
        }

        private void AddCube_Click(object sender, EventArgs e)
        {
            g.CreateCube(drawableObjects, treeView1, "Cube", prefabs.Cube_prefab(WorldOrigin));
            interface2d.Invalidate();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            CreateImage(interface2d.Size);
            interface2d.Image = bmp;
            interface2d.Invalidate();
        }

        private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
        {
            CreateImage(interface2d.Size);
            interface2d.Image = bmp;
            interface2d.Invalidate();
        }



        
    }
}
