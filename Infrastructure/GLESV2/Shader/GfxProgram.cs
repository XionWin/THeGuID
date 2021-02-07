using System;
using System.Runtime.InteropServices;

namespace GLESV2.Shader
{
    public class GfxProgram
    {
        public int Id { get; init; }
        public GfxShader VertexShader { get; init; }
        public GfxShader FragmentShader { get; init; }
    }
}
