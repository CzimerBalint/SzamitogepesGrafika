using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static OurGraphics.GraphicsExtension;


namespace OurGraphics
{
    public static partial class GraphicsExtension
    {
        #region Face Class

        public class FaceBuilder : DrawableObject
        {
            public List<Vertex> Vertices { get; set; } = new List<Vertex>();
            public List<Vertex> faceVertices;
            public static Vertex Center { get; set; }
            private string[] ConnectionOrder;


            public FaceBuilder(List<Vertex> vertices, string[] connect) : base("Face", vertices.Last().Location, new List<Vector4>())
            {
                Center = vertices.Last();
                vertices.Remove(Center);
                foreach (Vertex vertex in vertices) 
                {
                    Vertices.Add(vertex);
                }
                ConnectionOrder = connect;

                faceVertices = ConnectionOrder.Where((c, i) => i % 3 == 0).Select(c => Vertices[int.Parse(c) - 1]).ToList();


                OriginalLocations = vertices.Select(v => new Vector4(v.Location.X,v.Location.Y,v.Location.Z, 1)).ToList();

            }

            public override void Draw(Graphics g)
            {

                for (int i = 0; i < faceVertices.Count; i++)
                {
                    Vertex current = faceVertices[i];
                    Vertex next = faceVertices[(i + 1) % faceVertices.Count]; // Az utolsó pontot az elsővel köti össze
                    g.MidPoint(Pens.Black, current, next);
                }
                


            }

            public override void Move(float deltaX, float deltaY, float deltaZ)
            {
                foreach (Vertex item in Vertices)
                {
                    item.Move(deltaX,deltaY,deltaZ);
                }
                Center.Move(deltaX,deltaY,deltaZ);  
            }

            public override void Transform(Matrix4 transformation)
            {
                foreach (Vertex item in Vertices)
                {
                    item.Transform(transformation);
                }
                Center.Transform(transformation);
            }

            public override Vector4 GetCenter()
            {
                Vector3 tmp = new Vector3(0,0,0);

                for (int i = 0; i < Vertices.Count+1; i++)
                {
                    tmp = Vertices[i].Location + Vertices[i + 1].Location;
                }
                return tmp / Vertices.Count;

            }

            public override void ResetTransform()
            {
                for (int i = 0; i < Vertices.Count; i++)
                {
                    Vertices[i].Location = OriginalLocations[i];
                }
                
            }
        }
        #endregion
    }
}