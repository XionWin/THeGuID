using System;

namespace EGL.Definitions
{
    [Flags]
    public enum RenderableType
    {
        DontCare = -1,
        None = 0x3038,

        OpenglEs = 0x0001,
        Openvg = 0x0002,
        OpenglEs2 = 0x0004,
        Opengl = 0x0008,
        OpenglEs3 = 0x00000040,
    }
}