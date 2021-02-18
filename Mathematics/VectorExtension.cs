using System;

namespace Mathematics
{
    public static class VectorExtension
    {
        public static Vector2 Normalize(this Vector2 vec) => new Vector2(vec.X * vec.Length, vec.Y * vec.Length);
        public static float Dot(this Vector2 left, Vector2 right) => left.X * right.X + left.Y * right.Y;
        public static float PerpDot(this Vector2 left, Vector2 right) => left.X * right.Y - left.Y * right.X;

        public static Vector2 Lerp(this Vector2 a, Vector2 b, float blend)
        {
            a.X = blend * (b.X - a.X) + a.X;
            a.Y = blend * (b.Y - a.Y) + a.Y;
            return a;
        }
        public static Vector2 BaryCentric(this Vector2 a, Vector2 b, Vector2 c, float u, float v)
        {
            return a + u * (b - a) + v * (c - a);
        }
    }
}
