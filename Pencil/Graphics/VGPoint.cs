using System;

namespace Pencil.Graphics
{
    public struct VGPoint
    {
        public float X { get; set; } 
        public float Y { get; set; }
        public float DX { get; set; }
        public float DY { get; set; }
        public float DMX { get; set; }
        public float MDY { get; set; }
        public float Length { get; set; }
        public byte Flags { get; set; }

    }
}