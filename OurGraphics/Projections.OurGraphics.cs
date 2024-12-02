using static OurGraphics.GraphicsExtension;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace OurGraphics
{
    public static partial class GraphicsExtension
    {
        public static Matrix4 CreatePerspectiveProjection(float fov, float aspectRatio, float near, float far)
        {
            // Perspektív vetítési mátrix létrehozása
            Matrix4 projection = new Matrix4();
            float scale = 1 / (float)Math.Tan(fov * 0.5);
            projection[0, 0] = scale / aspectRatio;
            projection[1, 1] = scale;
            projection[2, 2] = -far / (far - near);
            projection[2, 3] = -1;
            projection[3, 2] = -(far * near) / (far - near);
            projection[3, 3] = 0;

            return projection;
        }

        public static Matrix4 CreateOrthographicProjection(float left, float right, float bottom, float top, float near, float far)
        {
            // Ortografikus vetítési mátrix létrehozása
            Matrix4 projection = new Matrix4();
            projection[0, 0] = 2 / (right - left);
            projection[1, 1] = 2 / (top - bottom);
            projection[2, 2] = -2 / (far - near);
            projection[3, 0] = -(right + left) / (right - left);
            projection[3, 1] = -(top + bottom) / (top - bottom);
            projection[3, 2] = -(far + near) / (far - near);
            projection[3, 3] = 1;

            return projection;
        }

        public static void ApplyProjection(Graphics g, List<Vertex> vertices, Matrix4 projectionMatrix, int width, int height, float scale)
        {
            List<(int, int)> edges = new List<(int, int)>();
            for (int i = 0; i < vertices.Count; i++)
            {
                for (int j = i + 1; j < vertices.Count; j++)
                {
                    edges.Add((i, j));
                }
            }

            foreach (var edge in edges)
            {
                Vector3 start3D = vertices[edge.Item1].Location;
                Vector3 end3D = vertices[edge.Item2].Location;

                Vector4 startTransformed = projectionMatrix * new Vector4(start3D.X, start3D.Y, start3D.Z, 1.0f);
                Vector4 endTransformed = projectionMatrix * new Vector4(end3D.X, end3D.Y, end3D.Z, 1.0f);

                if (startTransformed.W != 0 && endTransformed.W != 0)
                {
                    startTransformed.X /= startTransformed.W;
                    startTransformed.Y /= startTransformed.W;
                    endTransformed.X /= endTransformed.W;
                    endTransformed.Y /= endTransformed.W;
                }

                float startX = startTransformed.X * (width / 2) * scale + (width / 2);
                float startY = -startTransformed.Y * (height / 2) * scale + (height / 2);
                float endX = endTransformed.X * (width / 2) * scale + (width / 2);
                float endY = -endTransformed.Y * (height / 2) * scale + (height / 2);

                g.MidPoint(Pens.Black, (int)startX, (int)startY, (int)endX, (int)endY);
            }
        }
    }
}
