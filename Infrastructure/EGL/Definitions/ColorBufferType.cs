using System;

namespace EGL.Definitions
{
    [Flags]
    enum ColorBufferType
    {
        RgbBuffer = 0x308E,
        LuminanceBuffer = 0x308F,
    }
}