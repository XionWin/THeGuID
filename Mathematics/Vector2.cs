using System;

namespace Mathematics
{
    public struct Vector2
    {
        public const int WIDTH = 2;
        public const int SIZE = sizeof(float) * Vector2.WIDTH;

        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
        public Vector2(float value)
        {
            this.X = this.Y = value;
        }

        public float X { get; set; }
        public float Y { get; set; }

        public static Vector2 UnitX => new Vector2(1, 0);
        public static Vector2 UnitY => new Vector2(0, 1);
        public static Vector2 Zero => new Vector2();

        public float this[int index]
        {
            get => index switch
            {
                0 => X,
                1 => Y,
                _ => throw new IndexOutOfRangeException($"You tried to access this vector at index:{index}"),
            };
            set
            {
                if (index < 0 || index >= Vector2.WIDTH)
                    throw new IndexOutOfRangeException($"You tried to access this vector at index:{index}");
                unsafe
                {
                    fixed (Vector2* p = &this)
                    {
                        var ptr = (float*)p;
                        *(ptr + index) = value;
                    }
                }
            }
        }

        public float LengthSquared => (float)(Math.Pow(this.X, 2) + Math.Pow(this.Y, 2));
        public float Length => (float)Math.Sqrt(Math.Pow(this.X, 2) + Math.Pow(this.Y, 2));
        public Vector2 PerpendicularRight => new Vector2(this.Y, -this.X);
        public Vector2 PerpendicularLeft => new Vector2(-this.Y, this.X);

        public void Normalize()
        {
            var scale = 1.0f / this.Length;
            X *= scale;
            Y *= scale;
        }


        /// <summary>
		/// Adds the specified instances.
		/// </summary>
		/// <param name="left">Left operand.</param>
		/// <param name="right">Right operand.</param>
		/// <returns>Result of addition.</returns>
		public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            left.X += right.X;
            left.Y += right.Y;
            return left;
        }

        /// <summary>
        /// Subtracts the specified instances.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>Result of subtraction.</returns>
        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            left.X -= right.X;
            left.Y -= right.Y;
            return left;
        }

        /// <summary>
        /// Negates the specified instance.
        /// </summary>
        /// <param name="vec">Operand.</param>
        /// <returns>Result of negation.</returns>
        public static Vector2 operator -(Vector2 vec)
        {
            vec.X = -vec.X;
            vec.Y = -vec.Y;
            return vec;
        }

        /// <summary>
        /// Multiplies the specified instance by a scalar.
        /// </summary>
        /// <param name="vec">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of multiplication.</returns>
        public static Vector2 operator *(Vector2 vec, float scale)
        {
            vec.X *= scale;
            vec.Y *= scale;
            return vec;
        }

        /// <summary>
        /// Multiplies the specified instance by a scalar.
        /// </summary>
        /// <param name="scale">Left operand.</param>
        /// <param name="vec">Right operand.</param>
        /// <returns>Result of multiplication.</returns>
        public static Vector2 operator *(float scale, Vector2 vec)
        {
            vec.X *= scale;
            vec.Y *= scale;
            return vec;
        }

        /// <summary>
        /// Divides the specified instance by a scalar.
        /// </summary>
        /// <param name="vec">Left operand</param>
        /// <param name="scale">Right operand</param>
        /// <returns>Result of the division.</returns>
        public static Vector2 operator /(Vector2 vec, float scale)
        {
            float mult = 1.0f / scale;
            vec.X *= mult;
            vec.Y *= mult;
            return vec;
        }
    }
}
