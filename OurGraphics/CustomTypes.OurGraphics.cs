using System;
using System.Collections.Generic;
using System.Drawing;
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

        public class Matrix
        {
            private int M { get; set; }
            private int N { get; set; }


            public Matrix(int m, int n) 
            {
                M = m;
                N = n;
            }
        }

    }
}
