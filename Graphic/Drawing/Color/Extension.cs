using System;

namespace Graphic.Drawing.Color
{
    public static class Extension
    {
        public static HSLA ToHSL(this RGBA rgb)
        {
            var hsl = new HSLA();
            // Convert RGB to a 0.0 to 1.0 range.
            double r = rgb.R / 255.0;
            double g = rgb.G / 255.0;
            double b = rgb.B / 255.0;

            // Get the maximum and minimum RGB components.
            double max = r;
            if (max < g) max = g;
            if (max < b) max = b;

            double min = r;
            if (min > g) min = g;
            if (min > b) min = b;

            double diff = max - min;
            hsl.L = (max + min) / 2;
            if (Math.Abs(diff) < 0.00001)
            {
                hsl.S = 0;
                hsl.H = 0;  // H is really undefined.
            }
            else
            {
                if (hsl.L <= 0.5) hsl.S = diff / (max + min);
                else hsl.S = diff / (2 - max - min);

                double rd = (max - r) / diff;
                double gd = (max - g) / diff;
                double bd = (max - b) / diff;

                if (r == max) hsl.H = bd - gd;
                else if (g == max) hsl.H = 2 + rd - bd;
                else hsl.H = 4 + gd - rd;

                hsl.H *= 60;
                if (hsl.H < 0) hsl.H += 360;
            }
            return hsl;
        }

        // Convert an HSL value into an RGB value.
        public static RGBA ToRGB(this HSLA hsl)
        {
            double p2;
            if (hsl.L <= 0.5) p2 = hsl.L * (1 + hsl.S);
            else p2 = hsl.L + hsl.S - hsl.L * hsl.S;

            double p1 = 2 * hsl.L - p2;
            double r, g, b;
            if (hsl.S == 0)
            {
                r = hsl.L;
                g = hsl.L;
                b = hsl.L;
            }
            else
            {
                r = QqhToRgb(p1, p2, hsl.H + 120);
                g = QqhToRgb(p1, p2, hsl.H);
                b = QqhToRgb(p1, p2, hsl.H - 120);
            }

            // Convert RGB to the 0 to 255 range.
            return new RGBA((byte)(r * 255.0), (byte)(g * 255.0), (byte)(b * 255.0));
        }

        private static double QqhToRgb(double q1, double q2, double hue)
        {
            if (hue > 360) hue -= 360;
            else if (hue < 0) hue += 360;

            if (hue < 60) return q1 + (q2 - q1) * hue / 60;
            if (hue < 180) return q2;
            if (hue < 240) return q1 + (q2 - q1) * (240 - hue) / 60;
            return q1;
        }
    }
}