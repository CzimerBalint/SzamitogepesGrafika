using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OurGraphics
{
    public static partial class GraphicsExtension
    {
        public class Cylinder : DrawableObject
        {
            public Vertex[] Bottom {  get; set; }
            public Vertex[] Top {  get; set; }
            public Vertex Origin {  get; set; }
            public float Height { get; set; }

            public Cylinder(Vertex[] bottomVerts, Vertex[] topVerts, Vertex origin, float height, float radius): base("Cylinder", origin.Location)
            {
                Height = height;
                Bottom = bottomVerts;
                Top = topVerts;
                Origin = origin;

                GenerateVertices(origin, radius, height);
            }

            public void SetName(string name)
            {
                Name = name;
            }

            private void GenerateVertices(Vertex origin, float radius, float height)
            {
                float angleIncrement = 360.0f / 16;
                float angle = 0.0f;

                for (int i = 0; i < 16; i++)
                {
                    // Az alsó kör csúcsai
                    float x = origin.Location.X + radius * (float)Math.Cos(angle * Math.PI / 180.0);
                    float y = origin.Location.Y + radius * (float)Math.Sin(angle * Math.PI / 180.0);
                    Bottom[i] = new Vertex(new Vector3(x, y, 0));

                    // A felső kör csúcsai (magasabb z koordinátával)
                    Top[i] = new Vertex(new Vector3(x, y, height));
                   
                    angle += angleIncrement;
                }
            }


            public override void Draw(Graphics g)
            {
                Pen pen = Pens.Black;
                /* Desperate voltam hogy kiderítsem a hibát megvan 👍 https://tenor.com/view/dead-gif-5848418847127216186 2024.12.04 2:36 😭
                g.MidPoint(pen, Bottom[0], Bottom[1]);
                g.MidPoint(pen, Bottom[1], Bottom[2]);
                g.MidPoint(pen, Bottom[2], Bottom[3]);
                g.MidPoint(pen, Bottom[3], Bottom[4]);
                g.MidPoint(pen, Bottom[4], Bottom[5]);
                g.MidPoint(pen, Bottom[5], Bottom[6]);
                g.MidPoint(pen, Bottom[6], Bottom[7]);
                g.MidPoint(pen, Bottom[7], Bottom[8]);
                g.MidPoint(pen, Bottom[8], Bottom[9]);
                g.MidPoint(pen, Bottom[9], Bottom[10]);
                g.MidPoint(pen, Bottom[10], Bottom[11]);
                g.MidPoint(pen, Bottom[11], Bottom[12]);
                g.MidPoint(pen, Bottom[12], Bottom[13]);
                g.MidPoint(pen, Bottom[13], Bottom[14]);
                g.MidPoint(pen, Bottom[14], Bottom[15]); 
                g.MidPoint(pen, Bottom[15], Bottom[0]);

                g.MidPoint(pen, Top[0], Top[1]);
                g.MidPoint(pen, Top[1], Top[2]);
                g.MidPoint(pen, Top[2], Top[3]);
                g.MidPoint(pen, Top[3], Top[4]);
                g.MidPoint(pen, Top[4], Top[5]);
                g.MidPoint(pen, Top[5], Top[6]);
                g.MidPoint(pen, Top[6], Top[7]);
                g.MidPoint(pen, Top[7], Top[8]);
                g.MidPoint(pen, Top[8], Top[9]);
                g.MidPoint(pen, Top[9], Top[10]);
                g.MidPoint(pen, Top[10], Top[11]);
                g.MidPoint(pen, Top[11], Top[12]);
                g.MidPoint(pen, Top[12], Top[13]);
                g.MidPoint(pen, Top[13], Top[14]);
                g.MidPoint(pen, Top[14], Top[15]);
                g.MidPoint(pen, Top[15], Top[0]);*/

                // Rajzoljuk meg az alsó kör éleit
                for (int i = 0; i < 16; i++)
                {
                    g.MidPoint(Pens.Black, Bottom[i], Bottom[(i+1)%16]);
                }

                // Rajzoljuk meg a felső kör éleit
                for (int i = 0; i < 16; i++)
                {
                    g.MidPoint(Pens.Black, Top[i], Top[(i + 1) % 16]);
                }

                // Rajzoljuk meg az oldalsó éleket
                for (int i = 0; i < 16; i++)
                {
                    g.MidPoint(Pens.Black, Bottom[i], Top[i]);
                }
            }

            public override void Move(float deltaX, float deltaY, float deltaZ)
            {
                for (int i = 0; i < 16; i++)
                {
                    Bottom[i].Move(deltaX, deltaY, deltaZ);
                }

                for (int i = 0; i < 16; i++)
                {
                    Top[i].Move(deltaX, deltaY, deltaZ);
                }

                Origin.Move(deltaX, deltaY, deltaZ);
            }
           
            // Új transzformációs metódus
            public override void Transform(Matrix4 transformation)
            {
                foreach (var vertex in Bottom)
                {
                    vertex.Transform(transformation);
                }

                foreach (var vertex in Top)
                {
                    vertex.Transform(transformation);
                }
                Origin.Transform(transformation);
            }

            public override Vector4 GetCenter()
            {
                return Origin.Location;
            }
        }




    }
}
