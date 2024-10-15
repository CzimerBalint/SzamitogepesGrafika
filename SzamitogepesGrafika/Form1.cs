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
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        private void dDAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pen pen = new Pen(Color.Black);
            g.DDA(pen, 10.0f, 10.0f, 200.0f, 200.0f);
        }

        private void interface2d_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
           
        }
    }
}
