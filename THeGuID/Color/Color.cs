using System;

namespace THeGuID.Color
{
    public struct Color
    {
        private ColorBase _lastUpdateFrom;
        public Color(byte r, byte g, byte b, byte a = 255) : this()
        {
            this._r = r;
            this._g = g;
            this._b = b;
            this._a = a;
            this._lastUpdateFrom = ColorBase.RGB;
            this.Update();
        }
        public Color(double h, double s, double l, byte a = 255) : this()
        {
            this._h = h;
            this._s = s;
            this._l = l;
            this._a = a;
            this._lastUpdateFrom = ColorBase.HSL;
            this.Update();
        }

        private byte _r;
        public byte R 
        {
            get => this._r;
            set
            {
                this._r = value;
                this._lastUpdateFrom = ColorBase.RGB;
                this.Update();
            }
        }
        private byte _g;
        public byte G
        {
            get => this._g;
            set
            {
                this._g = value;
                this._lastUpdateFrom = ColorBase.RGB;
                this.Update();
            }
        }
        private byte _b;
        public byte B
        {
            get => this._b;
            set
            {
                this._b = value;
                this._lastUpdateFrom = ColorBase.RGB;
                this.Update();
            }
        }
        private byte _a;
        public byte A
        {
            get => this._a;
            set
            {
                this._a = value;
                this._lastUpdateFrom = ColorBase.RGB;
                this.Update();
            }
        }

        private double _h;
        public double H
        {
            get => this._h;
            set
            {
                this._h = value;
                this._lastUpdateFrom = ColorBase.HSL;
                this.Update();
            }
        }
        private double _s;
        public double S
        {
            get => this._s;
            set
            {
                this._s = value;
                this._lastUpdateFrom = ColorBase.HSL;
                this.Update();
            }
        }
        private double _l;
        public double L
        {
            get => this._l;
            set
            {
                this._l = value;
                this._lastUpdateFrom = ColorBase.HSL;
                this.Update();
            }
        }


        private void Update()
        {
            switch (this._lastUpdateFrom)
            {
                case ColorBase.RGB:
                    ColorExtension.RgbToHsl(_r, _g, _b, out var h, out var s, out var l);
                    this._h = h; this._s = s; this._l = l;
                    break;
                case ColorBase.HSL:
                    ColorExtension.HslToRgb(_h, _s, _l, out var r, out var g, out var b);
                    this._r = r; this._g = g; this._b = b;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public RGBA RGBA => new RGBA(this._r, this._g, this._b, this._a);

        public static implicit operator RGBA(Color color)
        {
            return new RGBA(color._r, color._g, color._b, color._a);
        }
    }

    internal enum ColorBase
    {
        RGB,
        HSL
    }
}