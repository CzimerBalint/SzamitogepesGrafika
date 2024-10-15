using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace OurGraphics
{
    public static class OurGraphics
    {
        public static void DrawPixel(this Graphics g, Pen pen, float x, float y)
        {
            g.DrawRectangle(pen, x, y, 0.5f,0.5f);
        }
        public static void DrawPixel(this Graphics g, Color color, float x, float y)
        {
            g.DrawRectangle(new Pen(color), x, y, 0.5f, 0.5f);
        }


    }
}
