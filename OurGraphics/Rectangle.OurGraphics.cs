using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static OurGraphics.GraphicsExtension;


namespace OurGraphics
{
    public static partial class GraphicsExtension
    {
        #region Rectangle Class

        public class Rect : DrawableObject
        {
            public Vertex A { get; set; }
            public Vertex B { get; set; }
            public Vertex C { get; set; }
            public Vertex D { get; set; }

            public Rect(Vertex[] vertices) : base("Rectangle", vertices[0].Location)
            {
                A = vertices[0];
                B = vertices[1];
                C = vertices[2];
                D = vertices[3];
            }

            public override void Draw(Graphics g)
            {
                g.MidPoint(Pens.Black, A, B);
                g.MidPoint(Pens.Black, B, C);
                g.MidPoint(Pens.Black, C, D);
                g.MidPoint(Pens.Black, D, A);
            }

            public override void Move(float deltaX, float deltaY, float deltaZ)
            {
                A.Move(deltaX, deltaY, deltaZ);
                B.Move(deltaX, deltaY, deltaZ);
                C.Move(deltaX, deltaY, deltaZ);
                D.Move(deltaX, deltaY, deltaZ);
            }

            public override void Transform(Matrix4 transformation)
            {
                A.Transform(transformation);
                B.Transform(transformation);
                C.Transform(transformation);
                D.Transform(transformation);
            }

            public override Vector4 GetCenter()
            {
                return (A.Location + B.Location + C.Location + D.Location) / 4;
            }
        }
        #endregion
    }
}