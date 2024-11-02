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
        protected Button btnOk;

        public BaseForm(string title)
        {
            InitializeComponent();
            Padding = new Padding(10, 5, 5, 10);
            Text = title;

            btnOk = new Button { Text = "OK", Dock = DockStyle.Bottom };
            Controls.Add(btnOk);

            Deactivate += (sender, e) => Close();

            //TODO: 
            //
            //Patch ha az ablak deselected akkor ne villogjon hanem legyen bezárva
            //ezt absztraktá tenni hogy lehessen származtatni belőle a többihez mint pl vertex line stb...
            //
        }

        
    }
}
