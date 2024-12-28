using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static OurGraphics.GraphicsExtension;

namespace OurGraphics
{
    public static partial class GraphicsExtension
    {
        #region Vertex class
        public class Vertex : DrawableObject
        {
            public bool IsSelected { get; set; }


            public Vertex(Vector3 location) : base("Vertex", location, new List<Vector4>())
            {
                OriginalLocations = new List<Vector4>()
                {
                    location
                };
            }

            public void SetName(string name)
            {
                Name = name;
            }
            public void Select() => IsSelected = true;
            public void Deselect() => IsSelected = false;

            public override void Draw(Graphics g)
            {
                Brush brush = IsSelected ? Brushes.Red : Brushes.Black;
                RectangleF rect = new RectangleF(Location.X - 5, Location.Y - 5, 10, 10);
                g.FillEllipse(brush, rect);
            }

            public bool Contains(Point p)
            {
                return new Rectangle((int)Location.X - 5, (int)Location.Y - 5, 10, 10).Contains(p);
            }

            public override void Move(float deltaX, float deltaY, float deltaZ)
            {
                Location = new Vector3(Location.X + deltaX, Location.Y + deltaY, Location.Z + deltaZ);
            }

            public override Vector4 GetCenter()
            {
                return new Vector4(Location.X,Location.Y,Location.Z,1);
            }

            public override void ResetTransform()
            {
                Location = OriginalLocations[0];
            }
        }
        #endregion

        #region Burn Shape
        public static void BurnShape(this Graphics g, List<DrawableObject> drawableObjects, TreeView treeView1, Bitmap bitmap)
        {
            // Tárolja az eltávolítandó elemeket
            var verticesToRemove = drawableObjects.OfType<Vertex>().ToList();
            Graphics bitmapGraphics = Graphics.FromImage(bitmap);

            // Az eltávolítandó vertekszeket kiszedjük
            foreach (var vertex in verticesToRemove)
            {
                treeView1.Nodes.Clear();
                drawableObjects.Remove(vertex);

                // Törlés a TreeView-ból is
            }

            foreach (var item in drawableObjects)
            {
                item.Draw(bitmapGraphics);
            }

            drawableObjects.Clear(); 
        }
        #endregion
    }
}
