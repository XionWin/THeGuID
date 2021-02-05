using System;

namespace Graphic.Color
{
    public struct HSLA
    {
        public HSLA(double h, double s, double l, byte a = 255): this()
        {
            this.H = h;
            this.S = s;
            this.L = l;
            this.A = a;
        }
        
        public double H { get; set; }
        public double S { get; set; }
        public double L { get; set; }
        public byte A { get; set; }
    }
}