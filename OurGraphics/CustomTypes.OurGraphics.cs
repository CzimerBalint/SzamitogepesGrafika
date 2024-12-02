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
            public float X { get; set; }
            public float Y { get; set; }

            public Vector2(float x, float y)
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
                return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
            }

            public static Vector2 operator +(Vector2 v1, float scalar)
            {
                return new Vector2(v1.X + scalar, v1.Y + scalar);
            }

            public static Vector2 operator -(Vector2 v1, Vector2 v2)
            {
                return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
            }

            public static float operator *(Vector2 v1, Vector2 v2)
            {
                return (v1.X * v2.X) + (v1.Y * v2.Y);
            }

            public static Vector2 operator *(Vector2 v1, float scalar)
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

            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }

            public Vector3(float x, float y, float z)
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

            public static Vector3 operator +(Vector3 v1, float scalar)
            {
                return new Vector3(v1.X + scalar, v1.Y + scalar, v1.Z + scalar);
            }

            public static Vector3 operator -(Vector3 v1, Vector3 v2)
            {
                return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
            }

            public static float operator *(Vector3 v1, Vector3 v2)
            {
                return (v1.X * v2.X) + (v1.Y * v2.Y) + (v1.Z * v2.Z);
            }

            public static Vector3 operator *(Vector3 v1, float scalar)
            {
                return new Vector3(v1.X * scalar, v1.Y * scalar, v1.Z * scalar);
            }
            #endregion

            #region impicit konverzió

            public static implicit operator Point(Vector3 v)
            {
                return new Point((int)v.X, (int)v.Y);
            }

            public static implicit operator PointF(Vector3 v)
            {
                return new PointF((float)v.X, (float)v.Y);
            }

            public static implicit operator Vector3(Point p)
            {
                return new Vector3(p.X, p.Y, 0);
            }

            public static implicit operator Vector3(PointF p)
            {
                return new Vector3(p.X, p.Y, 0);
            }

            #endregion
        }

        public class Vector4
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }
            public float W { get; set; }

            public Vector4(float x, float y, float z, float w)
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

            public static Vector4 operator +(Vector4 v1, float scalar)
            {
                return new Vector4(v1.X + scalar, v1.Y + scalar, v1.Z + scalar, v1.W + scalar);
            }

            public static Vector4 operator -(Vector4 v1, Vector4 v2)
            {
                return new Vector4(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
            }

            public static float operator *(Vector4 v1, Vector4 v2)
            {
                return (v1.X * v2.X) + (v1.Y * v2.Y) + (v1.Z * v2.Z) + (v1.W * v2.W);
            }

            public static Vector4 operator *(Vector4 v1, float scalar)
            {
                return new Vector4(v1.X * scalar, v1.Y * scalar, v1.Z * scalar, v1.W * scalar);
            }
            #endregion

            #region impicit konverzió

            public static implicit operator Point(Vector4 v)
            {
                return new Point((int)v.X, (int)v.Y);
            }

            public static implicit operator PointF(Vector4 v)
            {
                return new PointF((float)v.X, (float)v.Y);
            }

            public static implicit operator Vector4(Point p)
            {
                return new Vector4(p.X, p.Y, 0, 0);
            }

            public static implicit operator Vector4(PointF p)
            {
                return new Vector4(p.X, p.Y, 0, 0);
            }

            #endregion
        }

        public class Matrix4
        {
            private float[,] Values;

            public Matrix4()
            {
                Values = new float[4, 4];
                IdentityMatrix();
            }

            public Matrix4(Matrix4 matrix)
            {
                this.Values = new float[4, 4];

                for (int i = 0; i < Values.GetLength(0); i++)
                {
                    for (int j = 0; j < Values.GetLength(0); j++)
                    {
                        this[i, j] = matrix[i, j];
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


            public float this[int row, int col]
            {
                get { return Values[row, col]; }
                set { Values[row, col] = value; }
            }

            public static Matrix4 CreateRotationX(float angle)
            {

                Matrix4 rotation = new Matrix4();
                rotation[1, 1] = (float)Math.Cos(angle);
                rotation[1, 2] = -(float)Math.Sin(angle);
                rotation[2, 1] = (float)Math.Sin(angle);
                rotation[2, 2] = (float)Math.Cos(angle);
                rotation[0, 0] = 1;
                rotation[3, 3] = 1;
                return rotation;
            }

            public static Matrix4 CreateRotationY(float angle)
            {

                Matrix4 rotation = new Matrix4();
                rotation[0, 0] = (float)Math.Cos(angle);
                rotation[0, 2] = (float)Math.Sin(angle);
                rotation[2, 0] = -(float)Math.Sin(angle);
                rotation[2, 2] = (float)Math.Cos(angle);
                rotation[1, 1] = 1;
                rotation[3, 3] = 1;
                return rotation;
            }


            public static Matrix4 operator +(Matrix4 a, Matrix4 b)
            {
                Matrix4 result = new Matrix4();
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        result[i, j] = a[i, j] + b[i, j];
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
                        float temp = 0;
                        for (int k = 0; k < 4; k++)
                        {
                            temp += a[i, k] * b[k, j];

                        }
                        result[i, j] = temp;
                    }
                }
                return result;
            }

            public static Matrix4 operator *(Matrix4 a, float scalar)
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
                return new Vector4(
                    m[0, 0] * v.X + m[0, 1] * v.Y + m[0, 2] * v.Z + m[0, 3] * v.W,
                    m[1, 0] * v.X + m[1, 1] * v.Y + m[1, 2] * v.Z + m[1, 3] * v.W,
                    m[2, 0] * v.X + m[2, 1] * v.Y + m[2, 2] * v.Z + m[2, 3] * v.W,
                    m[3, 0] * v.X + m[3, 1] * v.Y + m[3, 2] * v.Z + m[3, 3] * v.W
                );
            }
            public Matrix4 Transpose(Matrix4 matrix)
            {
                Matrix4 result = new Matrix4();

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        result[j, i] = Values[i, j];
                    }
                }

                return result;
            }

        }

    }
}
