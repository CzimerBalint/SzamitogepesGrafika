using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OurGraphics
{
    public static partial class OurGraphics
    {
        #region Circle Class
        public class Circle : DrawableObject
        {
            public Line Radius { get; set; }

            public Circle(Line radius) : base($"Circle", new Point())
            {
                Radius = radius;
            }

            public void SetName(string name)
            {
                Name = name;
            }

            public override void Draw(Graphics g)
            {
                g.AritmeticCircle(Pens.Black, Radius.Start, Radius.End);
            }

            public override void Move(int deltaX, int deltaY)
            {
                Radius.Start.Move(deltaX, deltaY);
                Radius.End.Move(deltaX, deltaY);
            }
        }

        #endregion

        #region Create Circle

        private static int circleCount = 1;

        public static void CreateCircle(List<DrawableObject> drawableObjects, TreeView treeView1, Point center, Point radius)
        {

            Line rad = CreateLine(drawableObjects, treeView1, center, radius, LineDrawingAlgo.Midpoint);

            Circle circle = new Circle(rad);

            circle.SetName($"Aritmetic_Circle{circleCount++}");
            rad.SetName("Circle_lenght");
            rad.Start.SetName($"Circle_Center");
            rad.End.SetName($"Circle_Radius");

            drawableObjects.Add(circle);


            TreeNode parent = new TreeNode($"{circle.Name}");

            TreeNode child0 = new TreeNode($"{rad.Name}");
            TreeNode child1 = new TreeNode($"{rad.Start.Name}");
            TreeNode child2 = new TreeNode($"{rad.End.Name}");

            parent.Nodes.Add(child0);
            parent.Nodes.Add(child1);
            parent.Nodes.Add(child2);

            treeView1.Nodes.Add(parent);
        }

        #endregion
    }
}
