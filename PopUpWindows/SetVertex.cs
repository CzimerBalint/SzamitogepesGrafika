using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PopUpWindows
{
    public class SetVertex : BaseForm
    {
        protected NumericUpDown xPos;
        protected NumericUpDown yPos;
        protected HScrollBar xScroll;
        protected HScrollBar yScroll;
        protected Label xLabel;
        protected Label yLabel;
        protected Button okBtn;

        protected TableLayoutPanel tableLayoutPanel;
        public SetVertex() : base()
        {
            tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, // Fill the available space
                ColumnCount = 2,       // Two columns: one for labels, one for NumericUpDowns and scroll bars
                RowCount = 6,          // Four rows for labels, NumericUpDowns, and scroll bars
                AutoSize = true,
            };



            xLabel = new Label { Text = "X:" };
            yLabel = new Label { Text = "Y:" };
            xPos = new NumericUpDown { Minimum = 0, Maximum = Interface2d.Width, Value = 0, };
            yPos = new NumericUpDown { Minimum = 0, Maximum = Interface2d.Height, Value = 0, };
            xScroll = new HScrollBar
            {
                
                Minimum = 0,
                Maximum = Interface2d.Width,
                SmallChange = 1,
                LargeChange = 5
            };
            yScroll = new HScrollBar
            {
                Minimum = 0,
                Maximum = Interface2d.Height,
                SmallChange = 1,
                LargeChange = 5
                
            };
            okBtn = new Button { Text = "OK" };
                                            //oszlop sor
            tableLayoutPanel.Controls.Add(xLabel, 0, 0); 
            tableLayoutPanel.Controls.Add(xPos, 1, 0);    

            tableLayoutPanel.Controls.Add(xScroll, 0, 1); 
            tableLayoutPanel.SetColumnSpan(xScroll, 2);   
            xScroll.Dock = DockStyle.Fill;

            tableLayoutPanel.Controls.Add(yLabel, 0, 2); 
            tableLayoutPanel.Controls.Add(yPos, 1, 2);    

            tableLayoutPanel.Controls.Add(yScroll, 0, 3); 
            tableLayoutPanel.SetColumnSpan(yScroll, 2);   
            yScroll.Dock = DockStyle.Fill;

            tableLayoutPanel.Controls.Add(okBtn, 0, 5);
            tableLayoutPanel.SetColumnSpan(okBtn, 2);
            okBtn.Dock = DockStyle.Fill;

            this.Controls.Add(tableLayoutPanel);


        }

        public static void ShowWindow()
        {
            SetVertex setVertex = new SetVertex();
            setVertex.Show();
        }
    }
}
