using System;

namespace EGL.Definitions
{
    [Flags]
    enum ConformantType
    {
        DontCare = -1,
        None = 0x3038,

        SlowConfig = 0x3050,
        NonConformantConfig = 0x3051,
    }
}