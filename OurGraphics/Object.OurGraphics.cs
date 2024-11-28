using System.Drawing;

namespace OurGraphics
{
    public static partial class GraphicsExtension
    {
        #region Abstract Object management
        public abstract class DrawableObject
        {
            public string Name { get; set; }
            public Vector3 Location { get; set; }

            public DrawableObject(string name, Vector3 location)
            {
                Name = name;
                Location = location;
            }
            public abstract void Draw(Graphics g);
            public abstract void Move(int deltaX, int deltaY);
        }


        #endregion
    }
}
