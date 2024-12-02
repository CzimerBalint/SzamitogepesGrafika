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
            public abstract void Move(float deltaX, float deltaY, float deltaZ);

            // Új transzformációs metódus
            public virtual void Transform(Matrix4 transformation)
            {
                Vector4 transformedLocation = transformation * new Vector4(Location.X, Location.Y, Location.Z, 1.0f);
                Location = new Vector3(transformedLocation.X, transformedLocation.Y, transformedLocation.Z);
            }
        }

        #endregion
    }
}
