using System;

namespace GBM
{
    using GbmHandler = IntPtr;
    using GbmSurfaceHandler = IntPtr;
    public class Gbm
    {
        public Gbm(Device gbmDevice, uint width, uint height, uint format, ulong modifier)
        {
            this.GbmHandler = gbmDevice.Handle;
            this.Width = width;
            this.Height = height;
            this.Format = format;
            this.Modifier = modifier;
            this.SurfaceHandler = gbmDevice.CreateSurface(this.Width, this.Height, this.Format, this.Modifier);
        }

        public GbmHandler GbmHandler { get; set; }
        public GbmSurfaceHandler SurfaceHandler { get; set; }
        public uint Format { get; set; }
        public ulong Modifier { get; set; }
        public uint Width { get; set; }
        public uint Height { get; set; }

        public override string ToString()
        {
            return string.Format("[Gbm: GbmHandler=0x{0:x}, GbmSurfaceHandler=0x{1:x}, Width={2}, Height={3}, Format={4}, Modifier={5}]", GbmHandler, SurfaceHandler, Width, Height, Format, Modifier);
        }
    }
}
