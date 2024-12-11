using OurGraphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
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

        //filehandle
        private string fileContent = string.Empty;
        private string filePath = string.Empty;

        //rotation
        private bool isXDown;
        private bool isYDown;
        private bool isZDown;






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
                int deltaX = e.Location.X - MBM_last.X;
                int deltaY = e.Location.Y - MBM_last.Y;

                if (Control.ModifierKeys == Keys.Shift)
                {
                    // Mozgatás jobbra/balra/le/fel, ha a Shift lenyomva van
                    int deltaMoveX = e.Location.X - MBM_first.X;
                    int deltaMoveY = e.Location.Y - MBM_first.Y;

                    Debug.WriteLine("Shift held down - moving objects");

                    foreach (var drawable in drawableObjects)
                    {
                        drawable.Move(deltaMoveX, deltaMoveY, 0);
                    }

                    MBM_first = e.Location;
                }
                else
                {
                    // Forgási szögek kiszámítása
                    float sensitivity = 0.017f;
                    float angleX = deltaY * sensitivity;
                    float angleY = deltaX * sensitivity;

                    // Forgatási mátrix inicializálása
                    Matrix4 rotation = new Matrix4().IdentityMatrix();

                    // Forgatási tengely kiválasztása
                    if (isXDown)
                    {
                        rotation = Matrix4.CreateRotationX(angleX);
                    }
                    else if (isYDown)
                    {
                        rotation = Matrix4.CreateRotationY(angleY);
                    }
                    else if (isZDown)
                    {
                        float angleZ = (deltaX + deltaY) * sensitivity;
                        rotation = Matrix4.CreateRotationZ(angleZ);
                    }

                    // Alkalmazás az összes rajzolható objektumra
                    foreach (var drawable in drawableObjects)
                    {
                        
                        drawable.Transform(rotation);
                    }

                    
                }

                // Frissítsd az MBM_last pozíciót az aktuális egérpozícióra
                MBM_last = e.Location;
            }


            interface2d.Invalidate();
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

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            //LoadOBJ();
            //interface2d.Invalidate();


        }

        private void LoadOBJ()
        {
            List<Vertex> vertices = new List<Vertex>();
            string objName = string.Empty;
            int vertCounter = 0;
            


            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Wavefront file (*.obj)|*.obj";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                var fileStream = openFileDialog1.OpenFile();
                List<string> test = new List<string>();


                using (StreamReader reader = new StreamReader(fileStream))
                {
                    while (!reader.EndOfStream)
                    {
                        fileContent = reader.ReadLine();
                        if (fileContent.StartsWith("o "))
                        {
                            string[] asd = fileContent.Split(new string[] { "o " }, StringSplitOptions.None);
                            objName = asd[1];
                            MessageBox.Show(objName);

                        }
                        if (fileContent.StartsWith("v "))
                        {

                            string[] asd = fileContent.Split(new string[] { "v " }, StringSplitOptions.None);
                            string[] coords = asd[1].Replace('.',',').Split(' ');
                            //Debug.WriteLine(coords[0]);
                            //Debug.WriteLine(coords[1]);
                            //Debug.WriteLine(coords[2]);
                            float x = float.Parse(coords[0]);
                            float y = float.Parse(coords[1]);
                            float z = float.Parse(coords[2]);
                            vertCounter++;
                            //Console.WriteLine($"X: {x:F6}, Y: {y:F6}, Z: {z:F6}");
                            vertices.Add(new Vertex(WorldOrigin - new Vector3(x * 100, y * 100, z * 100)) { Name = $"{vertCounter}"});
                        }
                        if (fileContent.StartsWith("vn "))
                        {
                            string[] asd = fileContent.Split(new string[] { "vn " }, StringSplitOptions.None);
                        }
                        if (fileContent.StartsWith("vt "))
                        {
                            string[] asd = fileContent.Split(new string[] { "vt " }, StringSplitOptions.None);
                        }
                        if (fileContent.StartsWith("f "))
                        {
                            string[] asd = fileContent.Split(new string[] { "f " }, StringSplitOptions.None);
                            string[] coords = asd[1].Split(' ','/');
                            string concatenated = "";
                            for (int i = 0; i < coords.Length; i+=3)
                            {
                                concatenated += coords[i];
                                if (i + 3 < coords.Length) // Csak akkor ad hozzá vesszőt, ha nem az utolsó elem
                                {
                                    concatenated += ",";
                                }
                            }
                            string a = concatenated+";";
                            test.Add(a);
                            Debug.WriteLine(a); // Debug output az ellenőrzéshez

                        }
                    }
                    foreach (var item in vertices)
                    {
                        drawableObjects.Add(item);
                        treeView1.Nodes.Add(item.Name);
                    }
                    List<Vertex> vertices2 = new List<Vertex>();

                    foreach (var item in test)
                    {
                        string[] b = item.Split(';', ','); // Szétválasztjuk az elemeket
                        vertices2.Clear(); // Töröljük az előző iteráció elemeit
                        for (int i = 0; i < b.Length; i++)
                        {
                            for (int j = 0; j < vertices.Count; j++) // Nincs -3, az összes vertexet végigmegy
                            {
                                if (b[i] == vertices[j].Name) // Ha egyezik a `test` elem a `vertices` Name-jével
                                {
                                    vertices2.Add(vertices[j]); // Hozzáadjuk az aktuális vertexet
                                    break; // Megtaláltuk az elemet, nem kell tovább keresni
                                }
                            }
                        }

                        // Ellenőrizzük, hogy elegendő vertex van az összekötéshez
                        if (vertices2.Count >= 4)
                        {
                            // Körkörös összekötés az első és az utolsó között
                            vertices2.Add(vertices2[0]); // Az első elemet hozzáadjuk a lista végéhez

                            using (Graphics g = interface2d.CreateGraphics())
                            {
                                Rect tmp = new Rect(vertices2.ToArray());
                                drawableObjects.Add(tmp);
                            }
                        }
                    }


                }

            }


            

        } //TODO sok javítás

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.X)
            {
                isXDown = true;
            }
            if (e.KeyCode == Keys.Y)
            {
                isYDown = true;
            }
            if (e.KeyCode == Keys.Z)
            {
                isZDown = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.X)
            {
                isXDown = !true; //2024.12.10 1:56
            }
            if (e.KeyCode == Keys.Y)
            {
                isYDown = !true;
            }
            if (e.KeyCode == Keys.Z)
            {
                isZDown = !true;
            }
        }
    }
}
