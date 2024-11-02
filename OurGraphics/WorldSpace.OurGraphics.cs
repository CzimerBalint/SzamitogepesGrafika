using System.Drawing;

namespace OurGraphics
{
    public static partial class GraphicsExtension
    {
        #region Connvert screenSpace to worldSpace
        public static Point WorldOrigin(this Graphics g, int width, int height)
        {
            return new Point(width / 2, height / 2);
        }
        public static Point WorldOrigin(this Graphics g, Point size)
        {
            return new Point(size.X / 2, size.Y / 2);
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
    }
}
