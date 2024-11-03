using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PopUpWindows
{
    public partial class BaseForm : Form
    {
        protected static PictureBox Interface2d { get; set; }

        public BaseForm()
        {
            Padding = new Padding(10, 5, 5, 10);
            InitializeComponent();
        }

        public static void SetInterface2d(PictureBox interface2d)
        {
            Interface2d = interface2d;
        }
    }
}
