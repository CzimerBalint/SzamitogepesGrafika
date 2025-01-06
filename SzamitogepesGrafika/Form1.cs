using OurGraphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using static OurGraphics.GraphicsExtension;

namespace SzamitogepesGrafika
{
    public partial class Form1 : Form
    {

        //Graphics
        public Graphics g;
        public Prefabs prefabs;
        public List<DrawableObject> drawableObjects;
        public List<DrawableObject> guideObjects;
        public Bitmap bmp;


        public Vector3 WorldOrigin;
        private Vertex selectedVertex = null;
        private List<Vertex> selectedVertices = new List<Vertex>();


        //Mouse
        private Point lastMousePos;
        private bool MBM_isDown = false; // MBM mouse button Middle
        private Point MBM_first;
        private Point MBM_last;

        //filehandle
        private string fileContent = string.Empty;
        private string filePath = string.Empty;

        //rotation
        private Matrix4 rotation;
        private bool isXDown;
        private bool isYDown;
        private bool isZDown;

        private AxisGuide axisGuide;

        private float angleX;
        private float angleY;
        private float angleZ;

        //Colors
        private int brightness = 255;
        private Color SelectedColor;
        public Bitmap ColorImg;
        private bool ContainsCursor = false;



        public Form1()
        {
            InitializeComponent();

            WorldOrigin = new Point(interface2d.Width / 2, interface2d.Height / 2);

            drawableObjects = new List<DrawableObject>();
            guideObjects = new List<DrawableObject>();

            prefabs = new Prefabs(drawableObjects,guideObjects, treeView1);



        }

       

        private void Form1_Load(object sender, EventArgs e)
        {

            interface2d.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(interface2d, true, null);

            createAxisGuide();
            rotation = new Matrix4().IdentityMatrix();
            CreateColorSelect();

            CreateImage(splitContainer1.Panel1.Size);
            interface2d.Image = bmp;
            ColorSelector.Image = ColorImg;

            toolStripStatusLabel1.Text = "ScreenSpace: {X=NaN,Y=NaN}";
            toolStripStatusLabel2.Text = "Worldspace: {X=NaN,Y=NaN}";
        }

        #region AxisGuide

        private void createAxisGuide()
        {
            int offsetX = 100;
            PointF fixPlace = new PointF(interface2d.Size.Width - offsetX, WorldOrigin.Y / 6);
            axisGuide = prefabs.CreateAxisGuide(fixPlace); 

        }


        #endregion

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
            Line MP = prefabs.CreateMidPoint(WorldOrigin);
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



        //Color Selector
        private void CreateColorSelect()
        {
            ColorImg = new Bitmap(splitContainer4.Panel1.Width, splitContainer4.Panel1.Height);
            Color color = new Color();

            for (int x = 0; x < ColorImg.Width; x++)
            {
                for (int y = 0; y < ColorImg.Height; y++)
                {
                    // X tengely: árnyalat (Hue), Y tengely: telítettség (Saturation)
                    double hue = (double)x / ColorImg.Width * 360.0;
                    double saturation = (double)y / ColorImg.Height;
                    double value = (double)brightness / 255.0;

                    // Szín konvertálása HSV-ből RGB-be
                    color = ColorFromHSV(hue, saturation, value);

                    ColorImg.SetPixel(x, y, color);
                }
            }
            ColorSelector.Image = ColorImg;

        }

        private Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Clamp((int)value, 0, 255);
            int p = Clamp((int)(value * (1 - saturation)), 0, 255);
            int q = Clamp((int)(value * (1 - f * saturation)), 0, 255);
            int t = Clamp((int)(value * (1 - (1 - f) * saturation)), 0, 255);

            switch (hi)
            {
                case 0: return Color.FromArgb(v, t, p);
                case 1: return Color.FromArgb(q, v, p);
                case 2: return Color.FromArgb(p, v, t);
                case 3: return Color.FromArgb(p, q, v);
                case 4: return Color.FromArgb(t, p, v);
                default: return Color.FromArgb(v, p, q);
            }
        }
        
        private int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        private void BrightnessBar_Scroll(object sender, EventArgs e)
        {
            Color color = new Color();
            brightness = BrightnessBar.Value;

            color = Color.FromArgb(brightness, brightness, brightness);

            panel1.BackColor = color;
            CreateColorSelect();
        }

        private void ColorSelector_MouseDown(object sender, MouseEventArgs e)
        {

            
        }

        private void ColorSelector_MouseUp(object sender, MouseEventArgs e)
        {
            if (ContainsCursor)
            {
                SelectedColor = ColorImg.GetPixel(e.X, e.Y);
                splitContainer4.Panel2.BackColor = SelectedColor;
                
            }

            
        }

        private void ColorSelector_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.X >= 0 && e.X < ColorImg.Width && e.Y >= 0 && e.Y < ColorImg.Height)
            {
                ContainsCursor = true;
            }
            else
            {
                ContainsCursor = false;
            }
            
        }

        private Color InvertColor(Color color)
        {
            int invR = 255 - SelectedColor.R;
            int invG = 255 - SelectedColor.G;
            int invB = 255 - SelectedColor.B;

            return Color.FromArgb(invR, invG, invB);
                
        }


        private void interface2d_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            foreach (var drawable in drawableObjects)
            {
                drawable.Draw(g);
            }
            foreach (var guide in guideObjects)
            {
                guide.Draw(g);
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

                    foreach (var drawable in drawableObjects)
                    {
                        drawable.Move(deltaMoveX, deltaMoveY, 0);
                    }

                    MBM_first = e.Location;
                }
                else
                {
                    float sensitivity = 0.017f;
                    angleX = deltaX * sensitivity;
                    angleY = deltaY * sensitivity;

                    //Matrix4 rotation = new Matrix4().IdentityMatrix();

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
                        angleZ = (deltaX + deltaY) * sensitivity;
                        rotation = Matrix4.CreateRotationZ(angleZ);
                    }

                    // összes drawable forgatása a c# szerinti {0,0} ban és majd a képerő közepére transzfomrálás
                    foreach (var drawable in drawableObjects)
                    {
                        drawable.Transform(Matrix4.Translate(drawable.Location) * rotation * Matrix4.Translate(-drawable.Location));
                    }
                    foreach (var guide in guideObjects)
                    {
                        guide.Transform(Matrix4.Translate(guide.Location) * rotation * Matrix4.Translate(-guide.Location));
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
                    bmp.FillS4(e.Location.X, e.Location.Y, bmp.GetPixel(e.X, e.Y), SelectedColor);//set color
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

            LoadOBJ();
            interface2d.Invalidate();


        }

        private void LoadOBJ()
        {
            List<Vertex> vertices = new List<Vertex>();
            List<Rect> Faces = new List<Rect>();
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
                List<string> names = new List<string>();
                List<TreeNode> Parents = new List<TreeNode>();


                using (StreamReader reader = new StreamReader(fileStream))
                {
                    while (!reader.EndOfStream)
                    {
                        fileContent = reader.ReadLine();
                        if (fileContent.StartsWith("o "))
                        {
                            string[] asd = fileContent.Split(new string[] { "o " }, StringSplitOptions.None);
                            names.Add(asd[1]);

                        }
                        if (fileContent.StartsWith("v "))
                        {

                            string[] asd = fileContent.Split(new string[] { "v " }, StringSplitOptions.None);
                            string[] coords = asd[1].Replace('.', ',').Split(' ');
                            //Debug.WriteLine(coords[0]);
                            //Debug.WriteLine(coords[1]);
                            //Debug.WriteLine(coords[2]);
                            float x = float.Parse(coords[0]);
                            float y = float.Parse(coords[1]);
                            float z = float.Parse(coords[2]);
                            vertCounter++;
                            //Console.WriteLine($"X: {x:F6}, Y: {y:F6}, Z: {z:F6}");
                            vertices.Add(new Vertex(WorldOrigin - new Vector3(x * 100, y * 100, z * 100)) { Name = $"{vertCounter}" });
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
                            string[] coords = asd[1].Split(' ', '/');
                            string concatenated = "";
                            for (int i = 0; i < coords.Length; i += 3)
                            {
                                concatenated += coords[i];
                                if (i + 3 < coords.Length) // Csak akkor ad hozzá vesszőt, ha nem az utolsó elem
                                {
                                    concatenated += ",";
                                }
                            }
                            string a = concatenated + ";";
                            test.Add(a);
                            //Debug.WriteLine(a); // Debug output az ellenőrzéshez

                        }
                    }

                    foreach (string name in names)
                    {
                        Parents.Add(new TreeNode(name));
                    }


                    foreach (var item in vertices)
                    {
                        drawableObjects.Add(item);
                    }
                    treeView1.Nodes.AddRange(Parents.ToArray());

                    

                    


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
            //2024.12.10 1:56

            if (e.KeyCode == Keys.X)
            {
                isXDown = false; 
            }
            if (e.KeyCode == Keys.Y)
            {
                isYDown = false;
            }
            if (e.KeyCode == Keys.Z)
            {
                isZDown = false;
            }
        }

        private void resetRotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axisGuide.ResetTransform();
            foreach (DrawableObject drawable in drawableObjects)
            {
                drawable.ResetTransform();
            }
            interface2d.Invalidate();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SelectedColor = InvertColor(SelectedColor);
            splitContainer4.Panel2.BackColor = SelectedColor;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateImage(splitContainer1.Panel1.Size);
            axisGuide.ResetTransform();
            treeView1.Nodes.Clear();
            drawableObjects.Clear();
            SelectedColor = Color.White;
            splitContainer4.Panel2.BackColor = SelectedColor;

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            saveFileDialog1.InitialDirectory = "c:\\";
            saveFileDialog1.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
            saveFileDialog1.FilterIndex = 0;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Formátum kiválasztása a fájlkiterjesztés alapján
                string extension = System.IO.Path.GetExtension(saveFileDialog1.FileName).ToLower();
                ImageFormat format = ImageFormat.Png; // Alapértelmezett

                switch (extension)
                {
                    case ".jpg":
                    case ".jpeg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                    case ".png":
                        format = ImageFormat.Png;
                        break;
                    default:
                        MessageBox.Show("Ismeretlen fájlformátum! PNG formátum mentve.");
                        break;
                }

                // Bitmap mentése a kiválasztott formátumban
                try
                {
                    bmp.Save(saveFileDialog1.FileName, format);
                    MessageBox.Show("Kép sikeresen elmentve!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hiba történt a mentés során: " + ex.Message);
                }
            }

        }
    }
}

