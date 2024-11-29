using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurGraphics
{
    public static partial class GraphicsExtension
    {
        public class CentralProjection
        {
            public static Matrix4 CreatePojectionMat(float fov, float near, float far, float ascpectR)
            {
                Matrix4 projection = new Matrix4();
                float tanFov = 1 / (float)Math.Tan(fov/2);
                projection[0, 0] = tanFov / ascpectR;
                projection[1, 1] = tanFov;
                projection[2, 2] = -(far + near) / (far - near);
                projection[2, 3] = -(2 * far * near) / (far - near);
                projection[3, 2] = -1;
                projection[3, 3] = 0;
                return projection;
            }

            public static Vector2 ProjectPoint(Vector3 point, Matrix4 projectionMatrix)
            {
                Vector4 p = new Vector4(point.X, point.Y, point.Z, 1);
                Vector4 projected = projectionMatrix * p;
                if (projected.W != 0)
                {
                    projected.X /= projected.W;
                    projected.Y /= projected.W;
                }
                return new Vector2(projected.X, projected.Y);
            }

        }

    }
}
