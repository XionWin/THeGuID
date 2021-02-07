using System;
using System.Runtime.InteropServices;

namespace GLESV2.Shader
{
    public class GfxShader
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public ShaderType Type { get; set; }
    }
}
