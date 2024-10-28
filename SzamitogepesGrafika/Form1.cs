using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OurGraphics;
using static OurGraphics.OurGraphics;

namespace SzamitogepesGrafika
{
    public partial class Form1 : Form
    {
        Graphics g;
        public Point WorldOrigin;
        public List<DrawableObject> drawableObjects;
        private Vertex selectedVertex = null;
        private Point lastMousePos;
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
        }

        private void vertexHozzáadásaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateVertex(drawableObjects, treeView1, WorldOrigin);
            interface2d.Invalidate();
        }

        private void vertexekEgyesítéseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedVertices.Count < 2)
            {
                MessageBox.Show("Legalább két vertekszet kell kijelölni az egyesítéshez.");
                return;
            }

            // Merge the selected vertices
            var mergedVertex = MergeVertices(drawableObjects, treeView1, selectedVertices);
            MessageBox.Show($"Egyesítés sikeres: {mergedVertex.Name}");

            // Clear the selected vertices list
            selectedVertices.Clear();

            // Redraw the form
            interface2d.Invalidate();
        }

        private void Option_DDA_Click(object sender, EventArgs e)
        {
            CreateLine(drawableObjects, treeView1, WorldOrigin, new Point(WorldOrigin.X + 100, WorldOrigin.Y), LineDrawingAlgo.DDA);
            interface2d.Invalidate();
        }
        private void Option_MidPoint_Click(object sender, EventArgs e)
        {
            CreateLine(drawableObjects, treeView1, WorldOrigin, new Point(WorldOrigin.X + 100, WorldOrigin.Y), LineDrawingAlgo.Midpoint);
            interface2d.Invalidate();
        }
        private void Option_Aritmetic_Circle_Click(object sender, EventArgs e)
        {
            g.AritmeticCircle(Pens.Black, 100);
        }
        private void Option_MidPointCircle_Click(object sender, EventArgs e)
        {

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
            toolStripStatusLabel2.Text = $"Worldspace: {g.objectToWorldOrigin(WorldOrigin,e.Location)}";

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
        }

        private void interface2d_MouseUp(object sender, MouseEventArgs e)
        {
            selectedVertex = null;
        }

        private void midpointTriangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateTriangle(drawableObjects,treeView1,WorldOrigin,new Point(WorldOrigin.X+100,WorldOrigin.Y),new Point(WorldOrigin.X+50,WorldOrigin.Y+100));
            
            interface2d.Invalidate();
        }

        private void midpointRectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateRectangle(drawableObjects, treeView1, "Rectangle", WorldOrigin, new Point(WorldOrigin.X, WorldOrigin.Y + 100), new Point(WorldOrigin.X + 200, WorldOrigin.Y + 100), new Point(WorldOrigin.X + 200, WorldOrigin.Y));
            interface2d.Invalidate();
        }

        private void midpointSquareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateRectangle(drawableObjects, treeView1, "Square", WorldOrigin, new Point(WorldOrigin.X, WorldOrigin.Y + 100), new Point(WorldOrigin.X + 100, WorldOrigin.Y + 100), new Point(WorldOrigin.X + 100, WorldOrigin.Y));
            interface2d.Invalidate();
        }

        private void midpointDeltoidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateRectangle(drawableObjects, treeView1, "Deltoid", new Point(WorldOrigin.X + 25, WorldOrigin.Y + 25), new Point(WorldOrigin.X, WorldOrigin.Y + 100), new Point(WorldOrigin.X - 25, WorldOrigin.Y + 25), WorldOrigin);
            interface2d.Invalidate();
        }

        private void midpointParallelogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateRectangle(drawableObjects, treeView1, "Parallelogram", WorldOrigin, new Point(WorldOrigin.X + 25, WorldOrigin.Y + 50), new Point(WorldOrigin.X + 75, WorldOrigin.Y + 50), new Point(WorldOrigin.X + 50, WorldOrigin.Y));
            interface2d.Invalidate();
        }
    }
}
