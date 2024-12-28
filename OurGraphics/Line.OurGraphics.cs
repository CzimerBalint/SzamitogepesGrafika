using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static OurGraphics.GraphicsExtension;

namespace OurGraphics
{
    public static partial class GraphicsExtension
    {
        #region Lines Class
        public class Line : DrawableObject
        {
            public Vertex Start { get; set; }
            public Vertex End { get; set; }
            private LineDrawingAlgo DrawingAlgo { get; set; }

            public Line(Vertex[] vertices, LineDrawingAlgo drawingAlgo) : base("Line", vertices[0].Location, new List<Vector4>())
            {
                Start = vertices[0];
                End = vertices[1];
                DrawingAlgo = drawingAlgo;

                OriginalLocations = new List<Vector4>()
                {
                    Start.Location, 
                    End.Location
                };
            }

            public Line(Vertex start, Vertex end, LineDrawingAlgo drawingAlgo) : base("Line", start.Location, new List<Vector4>())
            {
                Start = start;
                End = end;
                DrawingAlgo = drawingAlgo;
            }
            public override void Draw(Graphics g)
            {
                switch (DrawingAlgo)
                {
                    case LineDrawingAlgo.DDA:
                        DDA(g, Pens.Black, Start, End);
                        break;
                    case LineDrawingAlgo.Midpoint:
                        MidPoint(g, Pens.Black, Start, End);
                        break;
                    default:
                        MidPoint(g, Pens.Black, Start, End);
                        break;
                }
            }

            public override void Move(float deltaX, float deltaY, float deltaZ)
            {
                Start.Move(deltaX, deltaY, deltaZ);
                End.Move(deltaX, deltaY, deltaZ);
            }

            public override void Transform(Matrix4 transformation)
            {
                Start.Transform(transformation);
                End.Transform(transformation);
            }

            public override Vector4 GetCenter()
            {
                return (Start.Location + End.Location)/2;
            }

            public override void ResetTransform()
            {
                Start.Location = OriginalLocations[0];
                End.Location = OriginalLocations[1];
            }
        }
        #endregion
    }
}
