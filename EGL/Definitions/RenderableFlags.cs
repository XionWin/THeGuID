using System;

namespace EGL.Definitions
{
    [Flags]
    public enum RenderableFlags
    {
        ES = Definition.OPENGL_ES_BIT,
        ES2 = Definition.OPENGL_ES2_BIT,
        ES3 = Definition.OPENGL_ES3_BIT,
        GL = Definition.OPENGL_BIT,
        VG = Definition.OPENVG_BIT,
    }
}