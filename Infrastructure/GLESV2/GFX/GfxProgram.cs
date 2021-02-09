using System;
using System.Collections.Generic;
using Extension;

namespace GLESV2.GFX
{
    public class GfxProgram: GfxObject
    {
        public GfxShader VertexShader { get; init; }
        public GfxShader FragmentShader { get; init; }

        public GfxProgram(string vertexShaderPath, string fragmentShaderPath): base()
        {
            this.Id = GL.glCreateProgram();
            (this.VertexShader = new GfxShader(ShaderType.Vertex, vertexShaderPath)).Load().Check();
            (this.FragmentShader = new GfxShader(ShaderType.Fragment, fragmentShaderPath)).Load().Check();
            
            this.AttachShaders(new GfxShader[] {this.VertexShader, this.FragmentShader})
            .Link()
            .Check();
        }
    }

    static class GfxProgramExtension {
        public static GfxProgram AttachShaders(this GfxProgram program, IEnumerable<GfxShader> shaders) {
            shaders.ForEach(s => GL.glAttachShader(program.Id, s.Id));
            return program;
        }
        public static GfxProgram Link(this GfxProgram program) {
            GL.glLinkProgram(program.Id);
            return program;
        }
    }
}
