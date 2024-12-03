using OurGraphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static OurGraphics.GraphicsExtension;

namespace SzamitogepesGrafika
{
    public partial class Form1 : Form
    {
        public Graphics g;
        public Point WorldOrigin;
        public Prefabs prefabs;
        public List<DrawableObject> drawableObjects;
        private Vertex selectedVertex = null;
        private Point lastMousePos;
        public Bitmap bmp;
        public Bitmap ColorImg;
        private List<Vertex> selectedVertices = new List<Vertex>();

        private bool MBM_isDown = false; // MBM mouse button Middle
        private Point MBM_first;
        private Point MBM_last;
        



        public Form1()
        {
            InitializeComponent();

            WorldOrigin = new Point(interface2d.Width / 2, interface2d.Height / 2);

            drawableObjects = new List<DrawableObject>();

            prefabs = new Prefabs(drawableObjects,treeView1);

            CreateColorSelect(ColorSelector.Size);



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateImage(splitContainer1.Panel1.Size);
            interface2d.Image = bmp;
            ColorSelector.Image = ColorImg;

            toolStripStatusLabel1.Text = "ScreenSpace: {X=NaN,Y=NaN}";
            toolStripStatusLabel2.Text = "Worldspace: {X=NaN,Y=NaN}";
        }

        #region Vertex
        private void Option_Add_Vertex_Click(object sender, EventArgs e)
        {
            Vertex vertex = prefabs.CreateVertex(WorldOrigin);
            interface2d.Invalidate();
        }

        private void Burn_Shape_Click(object sender, EventArgs e)
        {
            if (drawableObjects.Count == 0)
            {
                MessageBox.Show("Nem lehet mit véglegesíteni!");
            }
            else
            {
                g.BurnShape(drawableObjects, treeView1, bmp);
                MessageBox.Show($"Égetés sikeres!");
                selectedVertices.Clear();
                interface2d.Invalidate();
            }
        }
        #endregion

        #region Lines
        private void Option_DDA_Click(object sender, EventArgs e)
        {
            Line DDA = prefabs.CreateDDA(WorldOrigin);
            interface2d.Invalidate();
        }

        private void Option_MidPoint_Click(object sender, EventArgs e)
        {
            Line MP = prefabs.CreateMidPoint(WorldOrigin); //MP as MidPoint
            interface2d.Invalidate();
        }
        #endregion

        #region Triangles
        private void Option_Triangle_Click(object sender, EventArgs e)
        {
            Triangle triangle = prefabs.CreateTriangle(WorldOrigin);
            interface2d.Invalidate();
        }
        #endregion

        #region Rectangles
        private void Option_Rectangle_Click(object sender, EventArgs e)
        {
            Rect rectangle = prefabs.CreateRectangle(WorldOrigin);
            interface2d.Invalidate();
        }

        private void Option_Square_Click(object sender, EventArgs e)
        {   
            Rect square = prefabs.CreateSquare(WorldOrigin);
            drawableObjects.Add(square);
            interface2d.Invalidate();
        }

        private void Option_Deltoid_Click(object sender, EventArgs e)
        {
            
            Rect deltoid = prefabs.CreateDeltoid(WorldOrigin);
            drawableObjects.Add(deltoid);
            interface2d.Invalidate();
        }

        private void Option_Parallelogram_Click(object sender, EventArgs e)
        {
            Rect parallelogram = prefabs.CreateParallelogram(WorldOrigin);
            drawableObjects.Add(parallelogram);
            interface2d.Invalidate();
        }
        #endregion

        #region Ellipsies
        private void Option_Aritmetic_Circle_Click(object sender, EventArgs e)
        {
            Ellipse circle = prefabs.CreateCircle(WorldOrigin);
            interface2d.Invalidate();
        }

        private void Option_MidPointCircle_Click(object sender, EventArgs e)
        {
            Ellipse circle = prefabs.CreateEllipse(WorldOrigin);
            interface2d.Invalidate();

        }

        #endregion


        #region tetraeder
        private void AddTetraeder_Click(object sender, EventArgs e)
        {
            Tetraeder tetraeder = prefabs.CreateTetra(WorldOrigin);
            interface2d.Invalidate();
        }
        #endregion

        #region Cube
        private void AddCube_Click(object sender, EventArgs e)
        {
            Cube cube = prefabs.CreateCube(WorldOrigin);
            interface2d.Invalidate();
        }
        #endregion

        #region Drawing to the interface
        private void CreateImage(Size size)
        {
            bmp = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
            }

        }

        private void CreateColorSelect(Size size)
        {
            ColorImg = new Bitmap(size.Width, size.Height);
            for (int x = 0; x < ColorImg.Width; x++)
            {
                for (int y = 0; y < ColorImg.Height; y++)
                {
                    // Hue (árnyalat) az X tengely alapján (0-360)
                    double hue = (double)x / ColorImg.Width * 360.0;

                    // Saturation (telítettség) az Y tengely alapján (0-1)
                    double saturation = 1.0 - (double)y / ColorImg.Height;

                    // Fényerő konstans (maximum érték)
                    double value = 1.0;

                    // Szín kiszámítása
                    Color color = ColorFromHSV(hue, saturation, value);

                    // Pixel színezése a bitmapen
                    ColorImg.SetPixel(x, y, color);
                }
            }

        }

        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value *= 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            switch (hi)
            {
                case 0:
                    return Color.FromArgb(255, v, t, p);
                case 1:
                    return Color.FromArgb(255, q, v, p);
                case 2:
                    return Color.FromArgb(255, p, v, t);
                case 3:
                    return Color.FromArgb(255, p, q, v);
                case 4:
                    return Color.FromArgb(255, t, p, v);
                default:
                    return Color.FromArgb(255, v, p, q);
            }
        }


        private void interface2d_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            foreach (var drawable in drawableObjects)
            {
                drawable.Draw(g);
            }
        }


        private void interface2d_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = $"ScreenSpace: {e.Location}";
            toolStripStatusLabel2.Text = $"Worldspace: {g.objectToWorldOrigin(WorldOrigin, e.Location)}";

           

            if (selectedVertex != null)
            {
                int deltaX = e.Location.X - lastMousePos.X;
                int deltaY = e.Location.Y - lastMousePos.Y;

                selectedVertex.Move(deltaX, deltaY, 0);
                lastMousePos = e.Location;
                interface2d.Invalidate();
            }

            if (MBM_isDown)
            {
                if (Control.ModifierKeys == Keys.Shift)
                {
                    // Mozgatás jobbra/balra/le/fel, ha a Shift lenyomva van
                    int deltaX = e.Location.X - MBM_first.X;
                    int deltaY = e.Location.Y - MBM_first.Y;

                    foreach (var drawable in drawableObjects)
                    {
                        drawable.Move(deltaX, deltaY, 0);
                    }

                    MBM_first = e.Location;
                }                
                else
                {


                    /* int befogo2 = MBM_first.Y - e.Y; 
                     int befogo = MBM_first.X - e.X;

                     double atfogo = Math.Sqrt(Math.Pow(MBM_first.X - e.X, 2) + Math.Pow(e.Y, 2));     // sqrt((mbm.x - e.x)^2 + (mbm.y - e.y)^2)
                     double angleX = Math.Asin(befogo / atfogo);

                     double atfogo2 = Math.Sqrt(Math.Pow(MBM_first.Y - e.Y, 2) + Math.Pow(e.X, 2));     // sqrt((mbm.x - e.x)^2 + (mbm.y - e.y)^2)
                     double angleY = Math.Acos(befogo2 / atfogo2);*/
                    int deltaX = e.X - MBM_last.X;
                    int deltaY = e.Y - MBM_last.Y;

                    // Érzékenységi faktor a forgatáshoz
                    float sensitivity = 0.01f;

                    // Forgási szögek kiszámítása az X és Y tengelyek körül
                    float angleX = deltaY * sensitivity;
                    float angleY = deltaX * sensitivity;

                    // Forgatási mátrixok létrehozása
                    Matrix4 rotationX = Matrix4.CreateRotationX(angleX);
                    Matrix4 rotationY = Matrix4.CreateRotationY(angleY);

                    // Összesített forgatási mátrix
                    Matrix4 rotation = rotationX * rotationY;

                    // Pivotpont beállítása (pl. az objektum középpontja)
                    Vector3 pivot = WorldOrigin; // Állítsd be az objektum középpontját, ha más kell

                    // Transzláció az origóba
                    Matrix4 translateToOrigin = Matrix4.CreateTranslation(-pivot.X, -pivot.Y, -pivot.Z);

                    // Visszatranszláció
                    Matrix4 translateBack = Matrix4.CreateTranslation(pivot.X, pivot.Y, pivot.Z);

                    // Kombinált transzformáció a pivot körül
                    Matrix4 combinedTransformation = translateBack * rotation * translateToOrigin;

                    // Alkalmazás az összes rajzolható objektumra
                    foreach (var drawable in drawableObjects)
                    {
                        drawable.Transform(combinedTransformation);
                    }

                    // Frissítsd az MBM_last pozíciót az aktuális egérpozícióra
                    MBM_last = e.Location;
                }

                interface2d.Invalidate();
            }
        }

        private void interface2d_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    selectedVertex = null;

                    foreach (DrawableObject obj in drawableObjects)
                    {
                        if (obj is Vertex vertex)
                        {
                            vertex.Deselect();
                        }
                    }

                    foreach (DrawableObject obj in drawableObjects)
                    {
                        if (obj is Vertex vertex && vertex.Contains(e.Location))
                        {
                            vertex.Select();
                            selectedVertex = vertex;

                            if (!selectedVertices.Contains(vertex))
                            {
                                selectedVertices.Add(vertex);
                            }

                            lastMousePos = e.Location;
                            break;
                        }
                    }

                    interface2d.Invalidate();
                    break;

                case MouseButtons.Right:
                    bmp.FillS4(e.Location.X, e.Location.Y, bmp.GetPixel(e.X, e.Y), Color.Aqua);//set color
                    interface2d.Invalidate();
                    break;

                case MouseButtons.Middle:
                    MBM_first = new Point(e.X, e.Y);
                    MBM_isDown = true;
                    break;
            }
        }

        private void interface2d_MouseUp(object sender, MouseEventArgs e)
        {
            selectedVertex = null;
            MBM_last = new Point(e.X, e.Y);
            MBM_isDown = false;
        }
        #endregion

        private void interface2d_Resize(object sender, EventArgs e)
        {
            WorldOrigin = new Point(interface2d.Width / 2, interface2d.Height / 2);

            interface2d.Invalidate();
        }

       

        private void Form1_Resize(object sender, EventArgs e)
        {
            CreateImage(interface2d.Size);
            interface2d.Image = bmp;
            interface2d.Invalidate();
        }

        private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
        {
            CreateImage(interface2d.Size);
            interface2d.Image = bmp;
            interface2d.Invalidate();
        }

        private void hengerHozzáadásaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cylinder cylinder = prefabs.CreateCylinder(WorldOrigin);
            interface2d.Invalidate();
        }

        private void addConeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cone cone = prefabs.CreateCone(WorldOrigin);
            interface2d.Invalidate();
        }
    }
}
