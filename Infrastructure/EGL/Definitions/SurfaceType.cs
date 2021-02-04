using System;

namespace EGL.Definitions
{
    [Flags]
    public enum SurfaceType
    {
        DontCare = -1,
        None = 0x3038,

        Pbuffer = 0x0001,
        Pixmap = 0x0002,
        Window = 0x0004,
        VgColorspaceLinear = 0x0020,
        VgAlphaFormatPre = 0x0040,
        MultisampleResolveBox = 0x0200,
        SwapBehaviorPreserved = 0x0400,
    }
}