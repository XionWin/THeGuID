using System;

namespace GBM
{
    public class Gbm
    {
        public Gbm(Device device, uint width, uint height, SurfaceFormat format, ulong modifier)
        {
            this.Device = device;
            this.Width = width;
            this.Height = height;
            this.Format = format;
            this.Modifier = modifier;
            this.Surface = new Surface(Device, width, height, format, modifier);
        }

        public Device Device { get; set; }
        public Surface Surface { get; set; }
        public SurfaceFormat Format { get; set; }
        public ulong Modifier { get; set; }
        public uint Width { get; set; }
        public uint Height { get; set; }

        public override string ToString()
        {
            return string.Format("[Gbm: GbmDevice={0}, GbmSurface={1}, Width={2}, Height={3}, Format={4}, Modifier={5}]", Device, Surface, Width, Height, Format, Modifier);
        }
    }
}
