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
        DrawingTool currentTool = DrawingTool.None;
        private List<DrawableObject> drawableObjects; // Lista deklarálása


        private enum DrawingTool
        {
            None,
            DDA,
            Vertex,
        }

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
            toolStripStatusLabel3.Text = WorldOrigin.ToString();
        }

        private void dDAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTool = DrawingTool.DDA;
            CreateLine(drawableObjects, treeView1, WorldOrigin, new Point(WorldOrigin.X + 100, WorldOrigin.Y));
            interface2d.Invalidate();
        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTool = DrawingTool.Vertex;
            CreateVertex(drawableObjects, treeView1, WorldOrigin);
            interface2d.Invalidate();

        }

       

        private void interface2d_Paint(object sender, PaintEventArgs e)
        {

            
            g = e.Graphics;
            switch (currentTool)
            {
                case DrawingTool.DDA:
                    //g.DDA(Pens.Black,WorldOrigin.X,WorldOrigin.Y,150,150);
                    foreach (var drawable in drawableObjects)
                    {
                        drawable.Draw(g);
                    }

                    break;
                case DrawingTool.Vertex:
                    foreach (var drawable in drawableObjects)
                    {
                        drawable.Draw(g);
                    }
                    break;
                case DrawingTool.None:
                default:
                        //Üres alapból
                    break;
            }

        }

       

        private void interface2d_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = $"ScreenSpace: {e.Location}";
            toolStripStatusLabel2.Text = $"Worldspace: {g.objectToWorldOrigin(WorldOrigin,e.Location)}";

           
        }

        private void interface2d_Resize(object sender, EventArgs e)
        {
            WorldOrigin = g.WorldOrigin(interface2d.Width, interface2d.Height);
            toolStripStatusLabel3.Text = WorldOrigin.ToString();
        }

    }
}
