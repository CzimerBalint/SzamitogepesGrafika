using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static OurGraphics.GraphicsExtension;

namespace OurGraphics
{
    public static partial class GraphicsExtension
    {
        #region Triangle Class

        public class Triangle : DrawableObject
        {
            public Vertex A { get; set; }
            public Vertex B { get; set; }
            public Vertex C { get; set; }

            public Triangle(Vertex[] vertices) : base("Triangle", vertices[0].Location, new List<Vector4>())
            {
                A = vertices[0];
                B = vertices[1];
                C = vertices[2];

                OriginalLocations = new List<Vector4>()
                {
                    A.Location, B.Location, C.Location
                };
            }

            public override void Draw(Graphics g)
            {
                g.MidPoint(Pens.Black, A, B);
                g.MidPoint(Pens.Black, A, C);
                g.MidPoint(Pens.Black, B, C);
            }

            public override void Move(float deltaX, float deltaY, float deltaZ)
            {
                A.Move(deltaX, deltaY, deltaZ);
                B.Move(deltaX, deltaY, deltaZ);
                C.Move(deltaX, deltaY, deltaZ);
            }

            public override void Transform(Matrix4 transformation)
            {
                A.Transform(transformation);
                B.Transform(transformation);
                C.Transform(transformation);
            }

            public override Vector4 GetCenter()
            {
                return (A.Location + B.Location+C.Location)/3;
            }

            public override void ResetTransform()
            {
                A.Location = OriginalLocations[0];
                B.Location = OriginalLocations[1];
                C.Location = OriginalLocations[2];
            }
        }
        #endregion
    }
}
