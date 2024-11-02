using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static OurGraphics.OurGraphics;

namespace OurGraphics
{
    public static partial class OurGraphics
    {
    #region Vertex class
        public class Vertex : DrawableObject
        {
            public bool IsSelected { get; set; }
            public Vertex(Point location) : base($"Vertex", location)
            {
            }

            public void SetName(string name)
            {
                Name = name;
            }
            public void Select()
            {
                IsSelected = true;
            }

            public void Deselect()
            {
                IsSelected = false;
            }

            public override void Draw(Graphics g)
            {
                Brush brush = IsSelected ? Brushes.Red : Brushes.Black;
                g.FillRectangle(brush, Location.X - 5, Location.Y - 5, 10, 10);
            }

            public bool Contains(Point p)
            {
                return new Rectangle(Location.X - 5, Location.Y - 5, 10, 10).Contains(p);
            }

            public override void Move(int deltaX, int deltaY)
            {
                Location = new Point(Location.X + deltaX, Location.Y + deltaY);
            }
        }
        #endregion

    #region Create Vertex
        private static int vertexCount = 1;

        public static Vertex CreateVertex(List<DrawableObject> drawableObjects, TreeView treeView1, Point location, bool isPartOfLine = false)
        {
            Vertex vertex = new Vertex(location);
            vertex.SetName($"Vertex{vertexCount++}");
            drawableObjects.Add(vertex);

            if (!isPartOfLine)
            {
                TreeNode parent = new TreeNode($"{vertex.Name}");
                treeView1.Nodes.Add(parent);
            }

            return vertex;
        }
     #endregion

    }
}
