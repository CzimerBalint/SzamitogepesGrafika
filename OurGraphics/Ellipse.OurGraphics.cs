using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static OurGraphics.GraphicsExtension;

namespace OurGraphics
{
    public static partial class GraphicsExtension
    {
        #region Circle Class
        public class Ellipse : DrawableObject
        {
            public Vertex Center { get; set; }
            public Vertex MajorRadius { get; set; }
            public Vertex MinorRadius { get; set; }

            public Ellipse(Vertex[] vertices) : base("Ellipse", vertices[0].Location, new List<Vector4>())
            {
                Center = vertices[0];
                MajorRadius = vertices[1];
                MinorRadius = vertices[2];

                OriginalLocations = new List<Vector4>()
                {
                    Center.Location,
                    MajorRadius.Location,
                    MinorRadius.Location,

                };
            }

            public override void Draw(Graphics g)
            {
                g.DrawEllipse(Pens.Black, Center, MajorRadius, MinorRadius);
            }

            public override void Move(float deltaX, float deltaY, float deltaZ)
            {
                Center.Move(deltaX, deltaY, deltaZ);
                MajorRadius.Move(deltaX, deltaY, deltaZ);
                MinorRadius.Move(deltaX, deltaY, deltaZ);
            }

            public override void Transform(Matrix4 transformation)
            {
                Center.Transform(transformation);
                MajorRadius.Transform(transformation);
                MinorRadius.Transform(transformation);
            }

            public override Vector4 GetCenter()
            {
                return Center.Location;
            }

            public override void ResetTransform()
            {
                Center.Location = OriginalLocations[0];
                MajorRadius.Location = OriginalLocations[1];
                MinorRadius.Location = OriginalLocations[2];
            }
        }
        #endregion
    }
}
