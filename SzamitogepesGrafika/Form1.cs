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

        public Form1()
        {
            InitializeComponent();
        }


        private void interface2d_Click(object sender, EventArgs e)
        {
            using (Graphics g = interface2d.CreateGraphics())
            {
                OurGraphics.OurGraphics.DrawPixel(g, Color.Lime, 300f, 300f);
            }
        }
    }
}
