using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OurGraphics
{
    public static partial class GraphicsExtension
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

        public static Vertex CreateVertex(this Graphics g, List<DrawableObject> drawableObjects, TreeView treeView1, Point pos, bool isPartOfLine = false)
        {
            Vertex vertex = new Vertex(pos)
            {
                Name = $"Vertex{vertexCount++}"
            };

            drawableObjects.Add(vertex);

 
            if (!isPartOfLine)
            {
                TreeNode parent = new TreeNode($"{vertex.Name}");
                treeView1.Nodes.Add(parent);
            }

            return vertex;
        }
        #endregion

        #region Merge Vertecies
        public static void MergeVertices(List<DrawableObject> drawableObjects, TreeView treeView1)
        {
            

            // Régi vertekszek eltávolítása
            foreach (Vertex vertex in drawableObjects)
            {
                drawableObjects.Remove(vertex);
                // Törlés a TreeView-ból is
                var node = treeView1.Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == vertex.Name);
                if (node != null)
                {
                    treeView1.Nodes.Remove(node);
                }
            }

            
        }
        #endregion
    }
}
