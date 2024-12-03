﻿using System;
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
            Vertex Top { get; set; }

            public float Height { get; set; }

            public Cone(Vertex[] bottmVerts, Vertex top,PointF origin, float radius, float height): base("Cylinder", new Vector3(origin.X, origin.Y, 0))
            {
                Height = height;
                Bottom = bottmVerts;
                Top = top;

                GenerateVertices(origin, radius);
            }

            public void SetName(string name)
            {
                Name = name;
            }

            private void GenerateVertices(PointF origin, float radius)
            {
                float angleIncrement = 360.0f / 16;
                float angle = 0.0f;

                for (int i = 0; i < 16; i++)
                {
                    // Az alsó kör csúcsai
                    float x = origin.X + radius * (float)Math.Cos(angle * Math.PI / 180.0);
                    float y = origin.Y + radius * (float)Math.Sin(angle * Math.PI / 180.0);
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

            }

            // Új transzformációs metódus
            public override void Transform(Matrix4 transformation)
            {
                foreach (var vertex in Bottom)
                {
                    vertex.Transform(transformation);
                }

               Top.Transform(transformation);
            }
        }




    }
}
