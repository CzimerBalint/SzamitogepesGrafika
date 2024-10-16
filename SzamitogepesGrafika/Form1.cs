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

namespace SzamitogepesGrafika
{
    public partial class Form1 : Form
    {
        Graphics g;
        public Point WorldOrigin;
        DrawingTool currentTool = DrawingTool.None;
        private enum DrawingTool
        {
            None,
            DDA,
        }

        public Form1()
        {
            InitializeComponent();
            WorldOrigin = g.WorldOrigin(interface2d.Width, interface2d.Height); ;

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
            interface2d.Invalidate();
        }

        private void interface2d_Paint(object sender, PaintEventArgs e)
        {
            
            g = e.Graphics;
            g.FillRectangle(Brushes.Black,WorldOrigin.X,WorldOrigin.Y, 10,10);
            switch (currentTool)
            {
                case DrawingTool.DDA:
                    g.DDA(new Pen(Color.Black), 10.0f, 10.0f, 200.0f, 200.0f);
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
