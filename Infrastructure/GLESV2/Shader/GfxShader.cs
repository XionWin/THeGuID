using System;
using System.Runtime.InteropServices;

namespace GLESV2.Shader
{
    public class GfxShader
    {
        public uint Id { get; init; }
        public string Source { get; init; }
        public ShaderType Type { get; init; }

        private GfxShader(string path, ShaderType shaderType)
        {
            this.Id = GL.glCreateShader(shaderType);
            using (var sr = new System.IO.StreamReader(path))
            {
                this.Source = sr.ReadToEnd();
            }
            GL.glShaderSource(this.Id, 1, new []{this.Source}, 0);
            GL.glCompileShader(this.Id);
        }

        public static GfxShader Load(string path, ShaderType shaderType)
        {
            return new GfxShader(path, shaderType);
        }
    }
}
