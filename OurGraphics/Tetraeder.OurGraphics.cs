using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OurGraphics
{
    public static partial class GraphicsExtension
    {
        public class Tetraeder : DrawableObject
        {
            public Vertex A { get; set; }
            public Vertex B { get; set; }
            public Vertex C { get; set; }
            public Vertex D { get; set; }
            public Vertex Origin { get; set; }


            public Tetraeder(Vertex[] vertices) : base("Tetraeder", vertices[4].Location, new List<Vector4>())
            {
                A = vertices[0];
                B = vertices[1];
                C = vertices[2];
                D = vertices[3];
                Origin = vertices[4];

                OriginalLocations = new List<Vector4>()
                {
                    A.Location, B.Location, C.Location, D.Location, Origin.Location
                };
            }

            public void SetName(string name)
            {
                Name = name;
            }

            public override void Draw(Graphics g)
            {
                Pen pen = Pens.Black;

                //bottom
                g.MidPoint(pen, A, B);
                g.MidPoint(pen, A, C);
                g.MidPoint(pen, B, C);

                //top
                g.MidPoint(pen, D, A);
                g.MidPoint(pen, D, B);
                g.MidPoint(pen, D, C);
                
            }

            public override void Move(float deltaX, float deltaY, float deltaZ)
            {
                A.Move(deltaX, deltaY, deltaZ);
                B.Move(deltaX, deltaY, deltaZ);
                C.Move(deltaX, deltaY, deltaZ);
                D.Move(deltaX, deltaY, deltaZ);
                Origin.Move(deltaX, deltaY, deltaZ);

            }

            public override void Transform(Matrix4 transformation)
            {
                A.Transform(transformation);
                B.Transform(transformation);
                C.Transform(transformation);
                D.Transform(transformation);
                Origin.Transform(transformation);
            }

            public override Vector4 GetCenter()
            {
                return (A.Location+B.Location+C.Location+D.Location)/ 4;
            }

            public override void ResetTransform()
            {
                A.Location = OriginalLocations[0];
                B.Location = OriginalLocations[1];
                C.Location = OriginalLocations[2];
                D.Location = OriginalLocations[3];
                Origin.Location = OriginalLocations[4];
            }
        }




    }
}
