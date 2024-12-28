using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OurGraphics
{
    public static partial class GraphicsExtension
    {
        public class Cone : DrawableObject
        {
            public Vertex[] Bottom { get; set; }
            public Vertex Top { get; set; }
            public Vertex Origin { get; set; }
            public float Height { get; set; }

            public Cone(Vertex[] bottmVerts, Vertex top, Vertex origin, float radius, float height): base("Cylinder", origin.Location, new List<Vector4> ())
            {
                Top = top;
                Origin = origin;
                Bottom = bottmVerts;
                Height = height;

                OriginalLocations = new List<Vector4>();
                OriginalLocations.Add(Top.Location);
                OriginalLocations.Add(Origin.Location);
                for (int i = 0; i < Bottom.Length; i++) 
                {
                    OriginalLocations.Add(Bottom[i].Location);
                }

                GenerateVertices(origin, radius);
            }

            public override void ResetTransform()
            {
                Top.Location = OriginalLocations[0];
                Origin.Location = OriginalLocations[1];
                for (int i = 0; i < Bottom.Length; i++)// itt lehet 2-től kell majd
                {
                    Bottom[i].Location = OriginalLocations[i];
                }
            }

            public void SetName(string name)
            {
                Name = name;
            }

            private void GenerateVertices(Vertex origin, float radius)
            {
                float angleIncrement = 360.0f / 16;
                float angle = 0.0f;

                for (int i = 0; i < 16; i++)
                {
                    // Az alsó kör csúcsai
                    float x = origin.Location.X + radius * (float)Math.Cos(angle * Math.PI / 180.0);
                    float y = origin.Location.Y + radius * (float)Math.Sin(angle * Math.PI / 180.0);
                    Bottom[i] = new Vertex(new Vector3(x, y, 0));
                    angle += angleIncrement;
                }
            }


            public override void Draw(Graphics g)
            {
                // Rajzoljuk meg az alsó kör éleit
                for (int i = 0; i < 16; i++)
                {
                    Vertex start = Bottom[i];
                    Vertex end = Bottom[(i + 1) % 16];
                    g.MidPoint(Pens.Black, start, end);
                }

                foreach (var BottomVert in Bottom)
                {
                    g.MidPoint(Pens.Black, BottomVert, Top);
                }
            }

            public override void Move(float deltaX, float deltaY, float deltaZ)
            {
                foreach (var vertex in Bottom)
                {
                    vertex.Move(deltaX, deltaY, deltaZ);
                }

               
                Top.Move(deltaX, deltaY, deltaZ);

                Origin.Move(deltaX, deltaY, deltaZ);

            }

            // Új transzformációs metódus
            public override void Transform(Matrix4 transformation)
            {
                foreach (var vertex in Bottom)
                {
                    vertex.Transform(transformation);
                }

                Top.Transform(transformation);

                Origin.Transform(transformation);
            }

            public override Vector4 GetCenter()
            {
               return Origin.Location;
            }
        }

       



    }
}
