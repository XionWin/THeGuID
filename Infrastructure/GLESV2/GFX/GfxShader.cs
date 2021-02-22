using System;
using System.Runtime.InteropServices;

namespace GLESV2.GFX
{
    public class GfxShader: GfxObject
    {
        public string Source { get; init; }
        public Def.ShaderType ShaderType { get; init; }

        internal GfxShader(Def.ShaderType shaderType, string path): base()
        {
            this.Id = GL.glCreateShader(this.ShaderType = shaderType);
            using (var sr = new System.IO.StreamReader(path))
            {
                this.Source = sr.ReadToEnd();
            }
        }

        protected override void Release()
        {
            GL.glDeleteShader(this.Id);
        }
    }

    static class GfxShaderExtension {
        public static GfxShader Load(this GfxShader shader) {
            GL.glShaderSource(shader.Id, 1, new []{shader.Source}, 0);
            GL.glCompileShader(shader.Id);
            shader.CheckCompile();
            return shader;
        }

        public static GfxShader CheckCompile(this GfxShader shader) {
            if(!GL.glGetShaderCompiledStatus(shader))
            {
                throw new GLESV2Exception(GL.glGetShaderCompiledInformation(shader));
            }
            return shader;
        }
    }
}
