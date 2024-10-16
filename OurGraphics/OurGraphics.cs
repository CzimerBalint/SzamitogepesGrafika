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
        #region Connvert screenSpace to worldSpace
        public static Point WorldOrigin(this Graphics g,int width, int height)
        {
            return new Point(width/2, height/2);
        }
        public static Point WorldOrigin(this Graphics g,Point size)
        {
            return new Point(size.X / 2, size.Y/ 2);
        }
        public static Point objectToWorldOrigin(this Graphics g, int w, int h, int x, int y)
        {
            return new Point((w - x) * -1, h - y);
        }
        public static Point objectToWorldOrigin(this Graphics g, Point size, Point location)
        {
            return new Point((size.X - location.X) * -1, size.Y - location.Y);
        }
        #endregion

        public static void DrawPixel(this Graphics g, Pen pen, float x, float y)
        {
            g.DrawRectangle(pen, x, y, 0.5f, 0.5f);
        }
        public static void DrawPixel(this Graphics g, Color color, float x, float y)
        {
            g.DrawRectangle(new Pen(color), x, y, 0.5f, 0.5f);
        }
        public static void DDA(this Graphics g, Pen pen, float x0, float y0, float x1, float y1) 
        {
            float dx = x1 - x0;
            float dy = y1 - y0;
            float length = Math.Abs(dx);
            if (Math.Abs(dy) > length)
                length = Math.Abs(dy);
            float incX = dx / length;
            float incY = dy / length;
            float x = x0;
            float y = y0;
            g.DrawPixel(pen, x, y);
            for (int i = 0; i < length; i++)
            {
                x += incX;
                y += incY;
                g.DrawPixel(pen, x, y);
            }

        }

    }
}
