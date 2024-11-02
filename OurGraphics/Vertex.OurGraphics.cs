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

        #region Merge Vertecies
        public static Vertex MergeVertices(List<DrawableObject> drawableObjects, TreeView treeView1, List<Vertex> verticesToMerge)
        {
            if (verticesToMerge == null || verticesToMerge.Count < 2)
                throw new ArgumentException("Legalább két vertekszet kell megadni az egyesítéshez.");

            // Az új vertex helyének kiszámítása (átlag alapján)
            int avgX = (int)verticesToMerge.Average(v => v.Location.X);
            int avgY = (int)verticesToMerge.Average(v => v.Location.Y);
            Point mergedLocation = new Point(avgX, avgY);

            // Új vertex létrehozása és hozzáadása
            Vertex mergedVertex = CreateVertex(drawableObjects, treeView1, mergedLocation);

            // Frissítsük azokat az objektumokat, amelyek a régi vertekszeket használták (pl. vonalak)
            foreach (var drawable in drawableObjects.OfType<Line>())
            {
                if (verticesToMerge.Contains(drawable.Start))
                {
                    drawable.Start = mergedVertex;
                }
                if (verticesToMerge.Contains(drawable.End))
                {
                    drawable.End = mergedVertex;
                }
            }

            // Régi vertekszek eltávolítása
            foreach (var vertex in verticesToMerge)
            {
                drawableObjects.Remove(vertex);
                // Törlés a TreeView-ból is
                var node = treeView1.Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == vertex.Name);
                if (node != null)
                {
                    treeView1.Nodes.Remove(node);
                }
            }

            return mergedVertex;
        }
        #endregion
    }
}
