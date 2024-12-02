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

            public Triangle(Vertex[] vertices) : base("Triangle", vertices[0].Location)
            {
                A = vertices[0];
                B = vertices[1];
                C = vertices[2];
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
        }
        #endregion

        #region Triangle Counter
        #endregion
    }
}
