using static OurGraphics.GraphicsExtension;
using System;

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
        
    }
}
