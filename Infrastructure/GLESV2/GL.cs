using System;
using System.Runtime.InteropServices;

namespace GLESV2
{
    public class GL
    {
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        private static extern nint glGetString(uint name);
        public static string GetString(GLD definition) => Marshal.PtrToStringAuto(glGetString((uint)definition));

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glClearColor(float red, float green, float blue, float alpha);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glViewport(int x, int y, int width, int height);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glClear(Def.ClearBufferMask mask);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint glGetError();

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint glCreateProgram ();

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint glCreateShader(GFX.ShaderType shaderType);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glShaderSource (uint shader, int count, string[] source, int len);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glCompileShader (uint shader);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glAttachShader (uint programId, uint shaderId);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glLinkProgram (uint programId);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glDeleteShader (uint shaderId);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        private static extern void glGetShaderiv (uint shaderId, GLD pname, ref int value);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        private static extern void glGetShaderInfoLog (uint shaderId, int bufSize, int[] length, byte[] infoLog);
        public static bool glGetShaderCompiledStatus(GFX.GfxShader shader)
        {
            var isCompiled = 0;
            glGetShaderiv(shader.Id, GLD.GL_COMPILE_STATUS, ref isCompiled);
            return isCompiled == 1;
        }

        public static string glGetShaderCompiledInformation(GFX.GfxShader shader)
        {
            var len = 0;
            glGetShaderiv(shader.Id, GLD.GL_INFO_LOG_LENGTH, ref len);
            if(len > 1)
            {
                var bs = new byte[len];
                glGetShaderInfoLog(shader.Id, len, null, bs);
                return System.Text.Encoding.ASCII.GetString(bs);
            }
            return string.Empty;
        }
        
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glDeleteProgram(uint programId);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        private static extern void glGetProgramiv (uint shaderId, GLD pname, ref int value);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        private static extern void glGetProgramInfoLog (uint shaderId, int bufSize, int[] length, byte[] infoLog);
        public static bool glGetProgramLinkedStatus(GFX.GfxProgram program)
        {
            var isLinked = 0;
            glGetProgramiv(program.Id, GLD.GL_LINK_STATUS, ref isLinked);
            return isLinked == 1;
        }

        public static string glGetProgramLinkedInformation(GFX.GfxProgram program)
        {
            var len = 0;
            glGetProgramiv(program.Id, GLD.GL_INFO_LOG_LENGTH, ref len);
            if(len > 1)
            {
                var bs = new byte[len];
                glGetProgramInfoLog(program.Id, len, null, bs);
                return System.Text.Encoding.ASCII.GetString(bs);
            }
            return string.Empty;
        }

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        private static extern void glUseProgram(uint programId);

        public static void glUseProgram(GFX.GfxProgram program) => glUseProgram(program.Id);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        private static extern void glVertexAttribPointer (uint index, int size, GLD type, bool normalized, uint stride, nint pointer);

        public unsafe static void glVertexAttribPointerF(uint index, int size, bool normalized, uint stride, float[] data)
        {
            fixed(float *ptr = data)
            {
                glVertexAttribPointer(index, size, GLD.GL_FLOAT, normalized, stride, (nint)ptr);
            }
        }
        public unsafe static void glVertexAttribPointerN(uint index, int size, bool normalized, uint stride, nint offset)
        {
            glVertexAttribPointer(index, size, GLD.GL_FLOAT, normalized, stride, (nint)offset);
        }


        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        private static extern int glGetAttribLocation (uint programId, [MarshalAs(UnmanagedType.LPStr)]string name);
        public static uint glGetAttribLocation (GFX.GfxProgram program, string name)
        {
            return glGetAttribLocation(program.Id, name) is var r && r >= 0 ? (uint)r : throw new GLESV2Exception();
        }

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        private static extern int glGetUniformLocation (uint programId, [MarshalAs(UnmanagedType.LPStr)]string name);
        public static uint glGetUniformLocation (GFX.GfxProgram program, string name)
        {
            return glGetUniformLocation(program.Id, name) is var r && r >= 0 ? (uint)r : throw new GLESV2Exception();
        }


        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glEnableVertexAttribArray (uint index);
        
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glDrawArrays(GLD mode, int first, uint count);


        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glGenBuffers (uint n, out uint bufferId);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glBindBuffer (Def.BufferTarget target, uint bufferId);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glBufferData (Def.BufferTarget target, int size, nint data, GLD usage);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glUniformMatrix4fv (uint location, uint count, [MarshalAs(UnmanagedType.Bool)]bool transpose, float[] value);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void glUniformMatrix4fv (uint location, uint count, [MarshalAs(UnmanagedType.Bool)]bool transpose, float *value);
    }
}
