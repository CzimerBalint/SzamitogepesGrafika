using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace OurGraphics
{
    public static partial class GraphicsExtension
    {
        public class AxisGuide : DrawableObject
        {
            public Vertex A { get; set; }
            public Vertex B { get; set; }
            public Vertex C { get; set; }
            public Vertex D { get; set; }
            public Vertex E { get; set; }
            public Vertex F { get; set; }
            public Vertex G { get; set; }
           


            public AxisGuide(Vertex[] vertices) : base("AxisGuide", vertices[0].Location, new List<Vector4> ())
            {
                A = vertices[0];
                B = vertices[1];
                C = vertices[2];
                D = vertices[3];
                E = vertices[4];
                F = vertices[5];
                G = vertices[6];

                OriginalLocations = new List<Vector4>
                {
                    A.Location,
                    B.Location,
                    C.Location,
                    D.Location,
                    E.Location,
                    F.Location,
                    G.Location
                };


            }

            public override void ResetTransform()
            {
                A.Location = OriginalLocations[0];
                B.Location = OriginalLocations[1];
                C.Location = OriginalLocations[2];
                D.Location = OriginalLocations[3];
                E.Location = OriginalLocations[4];
                F.Location = OriginalLocations[5];
                G.Location = OriginalLocations[6];
            }

            public void SetName(string name)
            {
                Name = name;
            }

            public override void Draw(Graphics g)
            {
                Pen pen = Pens.Red;
                g.AxisGuideMP(pen, A, B);
                g.DrawString("X", new Font("Arial", 10), Brushes.Black,new PointF(B.Location.X,B.Location.Y));
                g.AxisGuideMP(pen, A, E);
                g.DrawString("-X", new Font("Arial", 10), Brushes.Black,new PointF(E.Location.X,E.Location.Y));
                pen = Pens.Green;
                g.AxisGuideMP(pen, A, C);
                g.DrawString("Y", new Font("Arial", 10), Brushes.Black,new PointF(C.Location.X,C.Location.Y));
                g.AxisGuideMP(pen, A, F);
                g.DrawString("-Y", new Font("Arial", 10), Brushes.Black,new PointF(F.Location.X,F.Location.Y));
                pen = Pens.Blue;
                g.AxisGuideMP(pen, A, D);
                g.DrawString("Z", new Font("Arial", 10), Brushes.Black, new PointF(D.Location.X, D.Location.Y));
                g.AxisGuideMP(pen, A, G);
                g.DrawString("-Z", new Font("Arial", 10), Brushes.Black, new PointF(G.Location.X, G.Location.Y));
            }

            public override void Transform(Matrix4 transformation)
            {
                A.Transform(transformation);
                B.Transform(transformation);
                C.Transform(transformation);
                D.Transform(transformation);
                E.Transform(transformation);
                F.Transform(transformation);
                G.Transform(transformation);
            }

            public override Vector4 GetCenter()
            {
                return A.Location;
            }

            public override void Move(float deltaX, float deltaY, float deltaZ)
            {
                A.Move(0, 0, 0);
                B.Move(0, 0, 0);
                C.Move(0, 0, 0);
                D.Move(0, 0, 0);
                E.Move(0, 0, 0);
                F.Move(0, 0, 0);
                G.Move(0, 0, 0);
            }

            
        }

    }
}
