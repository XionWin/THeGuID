using System;
using System.Runtime.InteropServices;

namespace GLESV2.GFX
{
    public class GfxShader: GfxObject
    {
        public string Source { get; init; }
        public ShaderType Type { get; init; }

        internal GfxShader(ShaderType shaderType, string path): base()
        {
            this.Id = GL.glCreateShader(this.Type = shaderType);
            using (var sr = new System.IO.StreamReader(path))
            {
                this.Source = sr.ReadToEnd();
            }
        }
    }

    static class GfxShaderExtension {
        public static GfxShader Load(this GfxShader shader) {
            GL.glShaderSource(shader.Id, 1, new []{shader.Source}, 0);
            GL.glCompileShader(shader.Id);
            return shader;
        }
    }
}
