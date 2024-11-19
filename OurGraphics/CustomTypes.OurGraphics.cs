using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurGraphics
{
    public static partial class GraphicsExtension
    {

        public class Vector2
        {
            public double X { get; set; }
            public double Y { get; set; }

            public Vector2(double x, double y)
            {
                X = x;
                Y = y;
            }

            public override string ToString()
            {
                return $"({X}, {Y})";
            }

            #region műveletek
            public static Vector2 operator +(Vector2 v1, Vector2 v2)
            {
                return new Vector2(v1.X+v2.X, v1.Y+v2.Y);
            }

            public static Vector2 operator +(Vector2 v1, double scalar)
            {
                return new Vector2(v1.X + scalar, v1.Y + scalar);
            }

            public static Vector2 operator -(Vector2 v1, Vector2 v2)
            {
                return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
            }

            public static double operator *(Vector2 v1, Vector2 v2)
            {
                return (v1.X * v2.X) + (v1.Y * v2.Y);
            }

            public static Vector2 operator *(Vector2 v1, double scalar)
            {
                return new Vector2(v1.X * scalar, v1.Y * scalar);
            }
            #endregion

            #region Implicit Konverizó

            public static implicit operator Point(Vector2 v)
            {
                return new Point((int)v.X, (int)v.Y);
            }

            public static implicit operator PointF(Vector2 v)
            {
                return new PointF((float)v.X, (float)v.Y);
            }

            public static implicit operator Vector2(Point p)
            {
                return new Vector2(p.X, p.Y);
            }

            public static implicit operator Vector2(PointF p)
            {
                return new Vector2(p.X, p.Y);
            }


            #endregion
        }

        public class Vector3
        {

            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }

            public Vector3(double x, double y, double z)
            {
                X = x;
                Y = y;
                Z = z;
            }

            public override string ToString()
            {
                return $"({X}, {Y}, {Z})";
            }

            #region műveletek
            public static Vector3 operator +(Vector3 v1, Vector3 v2)
            {
                return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
            }

            public static Vector3 operator +(Vector3 v1, double scalar)
            {
                return new Vector3(v1.X + scalar, v1.Y + scalar, v1.Z + scalar);
            }

            public static Vector3 operator -(Vector3 v1, Vector3 v2)
            {
                return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
            }

            public static double operator *(Vector3 v1, Vector3 v2)
            {
                return (v1.X * v2.X) + (v1.Y * v2.Y) + (v1.Z * v2.Z);
            }

            public static Vector3 operator *(Vector3 v1, double scalar)
            {
                return new Vector3(v1.X * scalar, v1.Y * scalar, v1.Z * scalar);
            }
            #endregion
        }

        public class Vector4
        {

            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
            public double W { get; set; }

            public Vector4(double x, double y, double z, double w)
            {
                X = x;
                Y = y;
                Z = z;
                W = w;
            }

            public override string ToString()
            {
                return $"({X}, {Y}, {Z}, {W})";
            }

            #region műveletek
            public static Vector4 operator +(Vector4 v1, Vector4 v2)
            {
                return new Vector4(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
            }

            public static Vector4 operator +(Vector4 v1, double scalar)
            {
                return new Vector4(v1.X + scalar, v1.Y + scalar, v1.Z + scalar, v1.W + scalar);
            }

            public static Vector4 operator -(Vector4 v1, Vector4 v2)
            {
                return new Vector4(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
            }

            public static double operator *(Vector4 v1, Vector4 v2)
            {
                return (v1.X * v2.X) + (v1.Y * v2.Y) + (v1.Z * v2.Z) + (v1.W * v2.W);
            }

            public static Vector4 operator *(Vector4 v1, double scalar)
            {
                return new Vector4(v1.X * scalar, v1.Y * scalar, v1.Z * scalar, v1.W * scalar);
            }
            #endregion
        }

        public class Matrix4
        {
            private double[,] Values;

            public Matrix4() 
            {
                this.Values = new double[4,4];
                for (int i = 0; i < 4; i++)
                {
                    for(int j = 0; j < 4; j++)
                    {
                        this[i, j] = 0;
                    }
                }
            }

            public Matrix4(Matrix4 matrix)
            {
                this.Values = new double[4, 4];

                for (int i = 0; i < Values.GetLength(0); i++)
                {
                    for (int j = 0; j < Values.GetLength(0); j++)
                    {
                        this[i, j] = matrix[i,j];
                    }
                }
            }

            public void IdentityMatrix()
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (i == j)
                        {
                            this[i, j] = 1;

                        }
                        else
                        {
                            this[i, j] = 0;
                        }
                    }
                }
            }


            public double this[int row, int col]
            {
                get { return Values[row, col]; }
                set { Values[row, col] = value; }
            }

            public static Matrix4 operator +(Matrix4 a, Matrix4 b)
            {
                Matrix4 result = new Matrix4();
                for(int i = 0;i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        result[i,j] = a[i,j] + b[i,j];   
                    }
                }
                return result;
            }

            public static Matrix4 operator *(Matrix4 a, Matrix4 b)
            {
                Matrix4 result = new Matrix4();
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        double temp = 0;
                        for (int k = 0; k < 4; k++)
                        {
                            temp += a[i, k] * b[k, j];

                        }
                        result[i,j] = temp;
                    }
                }
                return result;
            }

            public static Matrix4 operator *(Matrix4 a, double scalar)
            {

                Matrix4 result = new Matrix4();

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        result[i, j] = a[i, j] * scalar;
                    }
                }

                return result;
            }

            public static Vector4 operator *(Matrix4 m, Vector4 v)
            {
                Vector4 res = new Vector4(0, 0, 0, 1);
                res.X = m[0, 0] * v.X + m[0, 1] * v.Y + m[0, 2] * v.Z + m[0, 3] * v.W;
                res.Y = m[1, 0] * v.X + m[1, 1] * v.Y + m[1, 2] * v.Z + m[1, 3] * v.W;
                res.Z = m[2, 0] * v.X + m[2, 1] * v.Y + m[2, 2] * v.Z + m[2, 3] * v.W;
                res.W = m[3, 0] * v.X + m[3, 1] * v.Y + m[3, 2] * v.Z + m[3, 3] * v.W;
                return res;
            }


        }

    }
}
