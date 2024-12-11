using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace OurGraphics
{
    public static partial class GraphicsExtension
    {
        public class Cube : DrawableObject
        {
            public Vertex A { get; set; }
            public Vertex B { get; set; }
            public Vertex C { get; set; }
            public Vertex D { get; set; }
            public Vertex E { get; set; }
            public Vertex F { get; set; }
            public Vertex G { get; set; }
            public Vertex H { get; set; }
            public Vertex Origin { get; set; }

            public Cube(Vertex[] vertices) : base("Cube", vertices[0].Location)
            {
                A = vertices[0];
                B = vertices[1];
                C = vertices[2];
                D = vertices[3];
                E = vertices[4];
                F = vertices[5];
                G = vertices[6];
                H = vertices[7];
                Origin = vertices[8];
            }

            public void SetName(string name)
            {
                Name = name;
            }

            public override void Draw(Graphics g)
            {
                Pen pen = Pens.Black;
                // top
                g.MidPoint(pen, A, B);
                g.MidPoint(pen, B, C);
                g.MidPoint(pen, C, D);
                g.MidPoint(pen, D, A);
                // middle
                g.MidPoint(pen, A, E);
                g.MidPoint(pen, B, F);
                g.MidPoint(pen, C, G);
                g.MidPoint(pen, D, H);
                // bottom
                g.MidPoint(pen, E, F);
                g.MidPoint(pen, F, G);
                g.MidPoint(pen, G, H);
                g.MidPoint(pen, H, E);
                
            }

            public override void Move(float deltaX, float deltaY, float deltaZ)
            {
                A.Move(deltaX, deltaY, deltaZ);
                B.Move(deltaX, deltaY, deltaZ);
                C.Move(deltaX, deltaY, deltaZ);
                D.Move(deltaX, deltaY, deltaZ);
                E.Move(deltaX, deltaY, deltaZ);
                F.Move(deltaX, deltaY, deltaZ);
                G.Move(deltaX, deltaY, deltaZ);
                H.Move(deltaX, deltaY, deltaZ);
                Origin.Move(deltaX, deltaY, deltaZ);
            }

            // Új transzformációs metódus
            public override void Transform(Matrix4 transformation)
            {
                A.Transform(transformation);
                B.Transform(transformation);
                C.Transform(transformation);
                D.Transform(transformation);
                E.Transform(transformation);
                F.Transform(transformation);
                G.Transform(transformation);
                H.Transform(transformation);
                Origin.Transform(transformation);
            }

            public override Vector4 GetCenter()
            {
                return (A.Location + B.Location + C.Location + D.Location + E.Location + F.Location + G.Location + H.Location) / 8;
            }
        }


       
        
    }
}
