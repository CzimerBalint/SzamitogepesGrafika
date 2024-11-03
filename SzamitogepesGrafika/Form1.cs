using System;
using System.Collections.Generic;
using System.Drawing;
using OurGraphics;
using System.Windows.Forms;
using static OurGraphics.GraphicsExtension;

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
            megoldas();
            megoldas2();//ez a fainabb
            
        }

        #region Vertex
        private void Option_Add_Vertex_Click(object sender, EventArgs e)
        {
           
            CreateVertex(drawableObjects, treeView1, WorldOrigin);
            interface2d.Invalidate();
        }

        private void Option_Merge_Vertex_Click(object sender, EventArgs e)
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
        #endregion

        #region Lines
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
        #endregion

        #region Triangles
        private void Option_Triangle_Click(object sender, EventArgs e)
        {
            CreateTriangle(drawableObjects, treeView1, WorldOrigin, new Point(WorldOrigin.X + 100, WorldOrigin.Y), new Point(WorldOrigin.X + 50, WorldOrigin.Y + 100));

            interface2d.Invalidate();
        }
        #endregion

        #region Rectangles

        private void Option_Rectangle_Click(object sender, EventArgs e)
        {
            CreateRectangle(drawableObjects, treeView1, "Rectangle", WorldOrigin, new Point(WorldOrigin.X, WorldOrigin.Y + 100), new Point(WorldOrigin.X + 200, WorldOrigin.Y + 100), new Point(WorldOrigin.X + 200, WorldOrigin.Y));
            interface2d.Invalidate();
        }

        private void Option_Square_Click(object sender, EventArgs e)
        {
            CreateRectangle(drawableObjects, treeView1, "Square", WorldOrigin, new Point(WorldOrigin.X, WorldOrigin.Y + 100), new Point(WorldOrigin.X + 100, WorldOrigin.Y + 100), new Point(WorldOrigin.X + 100, WorldOrigin.Y));
            interface2d.Invalidate();
        }

        private void Option_Deltoid_Click(object sender, EventArgs e)
        {
            CreateRectangle(drawableObjects, treeView1, "Deltoid", new Point(WorldOrigin.X + 25, WorldOrigin.Y + 25), new Point(WorldOrigin.X, WorldOrigin.Y + 100), new Point(WorldOrigin.X - 25, WorldOrigin.Y + 25), WorldOrigin);
            interface2d.Invalidate();
        }

        private void Option_Parallelogram_Click(object sender, EventArgs e)
        {
            CreateRectangle(drawableObjects, treeView1, "Parallelogram", WorldOrigin, new Point(WorldOrigin.X + 25, WorldOrigin.Y + 50), new Point(WorldOrigin.X + 75, WorldOrigin.Y + 50), new Point(WorldOrigin.X + 50, WorldOrigin.Y));
            interface2d.Invalidate();
        }
        
        #endregion

        #region Circles
        private void Option_Aritmetic_Circle_Click(object sender, EventArgs e)
        {
            CreateCircle(drawableObjects, treeView1, WorldOrigin, new Point(WorldOrigin.X + 100, WorldOrigin.Y));
            interface2d.Invalidate();
        }
        private void Option_MidPointCircle_Click(object sender, EventArgs e) // yet to implement
        {

        }
        #endregion

        #region Drawing to the interface
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




        #endregion

       private void megoldas()
        {
            

            // FlowLayoutPanel beállítása
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true
            };
            Move_tabPage1.Controls.Add(flowLayoutPanel);

            // Első Panel szett létrehozása
            Panel panel1 = new Panel { AutoSize = true };
            Label label1 = new Label { Text = "Label 1", AutoSize = true };
            NumericUpDown numericUpDown1 = new NumericUpDown();
            HScrollBar hScrollBar1 = new HScrollBar { Width = 150 };

            panel1.Controls.Add(label1);
            panel1.Controls.Add(numericUpDown1);
            panel1.Controls.Add(hScrollBar1);

            label1.Location = new Point(0, 0);
            numericUpDown1.Location = new Point(80, 0);
            hScrollBar1.Location = new Point(0, 30);

            // Második Panel szett létrehozása
            Panel panel2 = new Panel { AutoSize = true };
            Label label2 = new Label { Text = "Label 2", AutoSize = true };
            NumericUpDown numericUpDown2 = new NumericUpDown();
            HScrollBar hScrollBar2 = new HScrollBar { Width = 150 };

            panel2.Controls.Add(label2);
            panel2.Controls.Add(numericUpDown2);
            panel2.Controls.Add(hScrollBar2);

            label2.Location = new Point(0, 0);
            numericUpDown2.Location = new Point(80, 0);
            hScrollBar2.Location = new Point(0, 30);

            // Panelek hozzáadása a FlowLayoutPanel-hez
            flowLayoutPanel.Controls.Add(panel1);
            flowLayoutPanel.Controls.Add(panel2);

        }

        private void megoldas2()
        {


            

            // Első GroupBox és TableLayoutPanel
            GroupBox groupBox1 = new GroupBox { Text = "Group 1", AutoSize = true, Dock = DockStyle.Top };
            TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel
            {
                RowCount = 2,
                ColumnCount = 2,
                Dock = DockStyle.Fill,
                AutoSize = true
            };

            Label label1 = new Label { Text = "Label 1", Anchor = AnchorStyles.Left };
            NumericUpDown numericUpDown1 = new NumericUpDown { Anchor = AnchorStyles.Left };
            HScrollBar hScrollBar1 = new HScrollBar { Dock = DockStyle.Fill };

            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(numericUpDown1, 1, 0);
            tableLayoutPanel1.Controls.Add(hScrollBar1, 0, 1);
            tableLayoutPanel1.SetColumnSpan(hScrollBar1, 2);  // Két oszlopra kiterjed

            groupBox1.Controls.Add(tableLayoutPanel1);

            // Második GroupBox és TableLayoutPanel
            GroupBox groupBox2 = new GroupBox { Text = "Group 2", AutoSize = true, Dock = DockStyle.Top };
            TableLayoutPanel tableLayoutPanel2 = new TableLayoutPanel
            {
                RowCount = 2,
                ColumnCount = 2,
                Dock = DockStyle.Fill,
                AutoSize = true
            };

            Label label2 = new Label { Text = "Label 2", Anchor = AnchorStyles.Left };
            NumericUpDown numericUpDown2 = new NumericUpDown { Anchor = AnchorStyles.Left };
            HScrollBar hScrollBar2 = new HScrollBar { Dock = DockStyle.Fill };

            tableLayoutPanel2.Controls.Add(label2, 0, 0);
            tableLayoutPanel2.Controls.Add(numericUpDown2, 1, 0);
            tableLayoutPanel2.Controls.Add(hScrollBar2, 0, 1);
            tableLayoutPanel2.SetColumnSpan(hScrollBar2, 2);  // Két oszlopra kiterjed

            groupBox2.Controls.Add(tableLayoutPanel2);

            // GroupBox-ok hozzáadása a TabPage-hez
            tabPage2.Controls.Add(groupBox2);
            tabPage2.Controls.Add(groupBox1);


        }
    }
}
