using System;

namespace GBM
{
    [Flags]
    public enum SurfaceFlags : uint
    {
        Scanout = (1 << 0),
        Cursor64x64 = (1 << 1),
        Rendering = (1 << 2),
        Write = (1 << 3),
        Linear = (1 << 4),
    }
}