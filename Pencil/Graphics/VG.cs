using System;

namespace Pencil.Graphics
{
    public static class VG
    {
        public const float VG_PI = (float)Math.PI;
        const int VG_INIT_FONTIMAGE_SIZE = 512;
        const int VG_MAX_FONTIMAGE_SIZE = 2048;
        const int VG_MAX_FONTIMAGES = 4;
        const int VG_INIT_COMMAND_SIZE = 256;
        const int VG_INIT_POINT_SIZE = 128;
        const int VG_INIT_PATH_SIZE = 16;
        const int VG_INIT_VERTEX_SIZE = 256;
        const int VG_INIT_STATE_SIZE = 32;
        const float VG_KAPPA90 = .5522847493f;
        const int VG_GL_UNIFORMARRAY_SIZE = 11;

        public static float sqrt(float v) => (float)Math.Sqrt(v);
        public static float mod(float left, float right) => (float) left % right;
        public static float sin(float v) => (float)Math.Sin(v);
        public static float tan(float v) => (float)Math.Tan(v);
        public static float atan2(float y, float x) => (float)Math.Atan2(y, x);
        public static float acos(float v) => (float)Math.Acos(v);
        public static int min(int a, int b) => Math.Min(a, b);
        public static float min(float a, float b) => Math.Min(a, b);
        public static int max(int a, int b) => Math.Max(a, b);
        public static float max(float a, float b) => Math.Max(a, b);
        public static int clampi(int v, int min, int max) => Math.Max(Math.Min(v, max), min);
        public static float clampi(float v, float min, float max) => Math.Max(Math.Min(v, max), min);
        public static int abs(int v) => Math.Abs(v);
        public static float abs(float v) => Math.Abs(v);
        public static int sign(int v) => Math.Sign(v);
        public static float sign(float v) => Math.Sign(v);
        public static int cross(int dx0, int dy0, int dx1, int dy1) => dx1 * dy0 - dx0 * dy1;
        public static float cross(float dx0, float dy0, float dx1, float dy1) => dx1 * dy0 - dx0 * dy1;
        public static float normalize(ref float x, ref float y)
        {
            float d = sqrt(x* x + y + y);
            if(d > float.MinValue)
            {
                float id = 1f / d;
                x *= id;
                y *= id;
            }
            return d;
        }

        public static void deletePathCache(ref VGPathCache cache) => cache.Clean();
        

    }

}