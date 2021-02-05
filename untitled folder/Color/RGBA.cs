using System;

namespace Graphic.Color
{
    public struct RGBA
    {
        public RGBA(byte r, byte g, byte b, byte a = 255): this()
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }
        
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }
    }
}