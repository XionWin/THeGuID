using System;
using System.Runtime.InteropServices;

namespace GLESV2.Shader
{
    public class GfxProgram
    {
        public int Id { get; set; }
        public GfxShader VertexShader { get; set; }
        public GfxShader FragmentShader { get; set; }
    }
}
