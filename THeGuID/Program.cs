using System;
using System.Runtime.InteropServices;
using Graphic.Drawing.Color;
using Mathematics;

namespace THeGuID
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public float x { get; set; }
        public float y { get; set; }
        public float r { get; set; }
        public float g { get; set; }
        public float b { get; set; }
        public float a { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("The Grid...");

            #region mathematics test
            
            var v1 = Vector2.UnitX;
            var v2 = Vector2.UnitY; 
            var v3 = v1.Lerp(v2, .2f);

            var v4 = v1.BaryCentric(v2, v3, 0.2f, 0.3f);
            
            #endregion

            var fd = Libc.Context.open("/dev/dri/card1", Libc.OpenFlags.ReadWrite);

            using (var ctx = new EGL.Context(fd, EGL.RenderableSurfaceType.OpenGLESV2) { VerticalSynchronization = true })
            {
                Console.WriteLine($"GL Extensions: {GLESV2.GL.GetString(GLESV2.GLD.GL_EXTENSIONS)}");
                Console.WriteLine($"GL Version: {GLESV2.GL.GetString(GLESV2.GLD.GL_VERSION)}");
                Console.WriteLine($"GL Sharding Language Version: {GLESV2.GL.GetString(GLESV2.GLD.GL_SHADING_LANGUAGE_VERSION)}");
                Console.WriteLine($"GL Vendor: {GLESV2.GL.GetString(GLESV2.GLD.GL_VENDOR)}");
                Console.WriteLine($"GL Renderer: {GLESV2.GL.GetString(GLESV2.GLD.GL_RENDERER)}");

                GLESV2.GL.glClearColor(1.0f, 1.0f, 1.0f, 0.0f);
                GLESV2.GL.glViewport(0, 0, ctx.Width, ctx.Height);

                var st = DateTime.Now;
                var frame = 0u;
                var totalTime = TimeSpan.Zero;

                var hsl = new Graphic.Drawing.Color.HSLA(0.0d, 1.0d, 0.5d, 255);

                uint size = 5;
                const float TRIANGLE_SIZE = 0.8f;

                var vertices = new Vertex[size];
                vertices[0].x = .0f;
                vertices[0].y = .0f;
                vertices[0].r = .0f;
                vertices[0].g = .0f;
                vertices[0].b = .0f;
                vertices[0].a = 1.0f;
                
                unsafe
                {
                    for (int i = 0; i < size - 1; i++)
                    {
                        int pos = i + 1;
                        vertices[pos].x = (float)Math.Cos(-i * (2.0f * Math.PI / (size - 2))) * TRIANGLE_SIZE;
                        vertices[pos].y = (float)Math.Sin(-i * (2.0f * Math.PI / (size - 2))) * TRIANGLE_SIZE;
                        
                        vertices[pos].r = 0.0f;
                        vertices[pos].g = 0.0f;
                        vertices[pos].b = 0.0f;
                        vertices[pos].a = 1.0f;

                        fixed(Vertex *ptr = vertices)
                        {
                            var vPtr = ptr + pos;
                            float * fPtr = (float *)vPtr;
                            var fpp = fPtr + 2 + (i % 3);
                            *fpp = 1.0f;
                        }
                        
                    }
                }

                uint vbo = 0;
                GLESV2.GL.glGenBuffers(1, ref vbo);
                GLESV2.GL.glBindBuffer(GLESV2.GLD.GL_ARRAY_BUFFER, vbo);

                unsafe
                {
                    fixed(Vertex * verticesPtr = vertices)
                    {
                        GLESV2.GL.glBufferData(GLESV2.GLD.GL_ARRAY_BUFFER, (int)(Marshal.SizeOf(typeof(Vertex)) * size), (nint)verticesPtr, GLESV2.GLD.GL_STATIC_DRAW);
                    }
                }
                
                using (var program = new GLESV2.GFX.GfxProgram(@"Shader/simplevertshader.glsl", @"Shader/simplefragshader.glsl"))
                {
                    GLESV2.GL.glClearColor(1f, 1f, 1f, .2f);

                    GLESV2.GL.glUseProgram(program);

                    uint posAttrib = GLESV2.GL.glGetAttribLocation(program, "position");
                    GLESV2.GL.glEnableVertexAttribArray(posAttrib);
                    GLESV2.GL.glVertexAttribPointerN(posAttrib, 2, false, (uint)Marshal.SizeOf(typeof(Vertex)), 0);

                    uint colorAttrib = GLESV2.GL.glGetAttribLocation(program, "color");
                    GLESV2.GL.glEnableVertexAttribArray(colorAttrib);
                    GLESV2.GL.glVertexAttribPointerN(colorAttrib, 4, false, (uint)Marshal.SizeOf(typeof(Vertex)), 2 * Marshal.SizeOf(typeof(float)));

                    var proj_mat_location = GLESV2.GL.glGetUniformLocation(program, "proj_mat");
                    var model_mat_location = GLESV2.GL.glGetUniformLocation(program, "model_mat");

                    ctx.Initialize(
                        () => {
                            Resize(ctx.Width, ctx.Height, proj_mat_location);
                        }
                    ).Render(
                        () => {
                            var rgb = hsl.ToRGB();
                    
                            var angle = System.Environment.TickCount64 % (360 * 20d) / 20d;

                            GLESV2.GL.glClearColor((float)rgb.R / 255, (float)rgb.G / 255, (float)rgb.B / 255, .2f);

                            GLESV2.GL.glClear(GLESV2.ClearBufferMask.ColorBufferBit);

			                SetRotationMatrix(angle / 360d * Math.PI * 2, model_mat_location);
                            GLESV2.GL.glDrawArrays(GLESV2.GLD.GL_TRIANGLE_FAN, 0, size);

                            hsl.H = angle + 90;

                            var et = DateTime.Now;
                            var dt = et - st;
                            st = et;

                            // frame++;
                            // totalTime += dt;
                            // if (totalTime.TotalMilliseconds > 30 * 1000)
                            // {
                            //     using (var mproc = System.Diagnostics.Process.GetCurrentProcess())
                            //     {
                            //         Console.WriteLine($"[{DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss")}]: {frame} frames rendered in {(float)totalTime.TotalMilliseconds / 1000:.##} seconds -> FPS={(float)frame / totalTime.TotalMilliseconds * 1000:.##}, memory used: {(double)mproc.WorkingSet64 / 1024 / 1024:.##}M, system memory used: {(double)mproc.PrivateMemorySize64 / 1024 / 1024:.##}M");
                            //         frame = 0;
                            //         totalTime = TimeSpan.Zero;
                            //     }
                            // }
                        }
                    );
                }
            }
        }

        private static void SetRotationMatrix(double rad, uint model_mat_location)
        {
            // rotation around z axis
            float sin_angle = (float)Math.Sin(rad);
            float cos_angle = (float)Math.Cos(rad);
            // var mat = new float[16];
            // mat[0] = cos_angle;
            // mat[1] = sin_angle;
            // mat[2] = 0;
            // mat[3] = 0;

            // mat[4] = -sin_angle;
            // mat[5] = cos_angle;
            // mat[6] = 0;
            // mat[7] = 0;

            // mat[8] = 0;
            // mat[9] = 0;
            // mat[10] = 1;
            // mat[11] = 0;

            // mat[12] = 0;
            // mat[13] = 0;
            // mat[14] = 0;
            // mat[15] = 1;


            
            // System.Numerics.Matrix4x4 m2 = new System.Numerics.Matrix4x4();
            // m1.M11 = cos_angle;
            // m1.M13 = -sin_angle;

            // m1.M22 = 1;
            
            // m1.M31 = sin_angle;
            // m1.M33 = cos_angle;

            // m1.M44 = 1;
            
            // mat[0] = cos_angle;
            // mat[1] = 0;
            // mat[2] = -sin_angle;
            // mat[3] = 0;

            // mat[4] = 0;
            // mat[5] = 1;
            // mat[6] = 0;
            // mat[7] = 0;

            // mat[8] = sin_angle;
            // mat[9] = 0;
            // mat[10] = cos_angle;
            // mat[11] = 0;

            // mat[12] = 0;
            // mat[13] = 0;
            // mat[14] = 0;
            // mat[15] = 1;
            
            var mat = new float[16];
            unsafe
            {
                System.Numerics.Matrix4x4 m1 = new System.Numerics.Matrix4x4();
                m1.M11 = cos_angle;
                m1.M12 = sin_angle;

                m1.M21 = -sin_angle;
                m1.M22 = cos_angle;
                
                m1.M33 = 1;

                m1.M44 = 1;

                System.Numerics.Matrix4x4 m2 = new System.Numerics.Matrix4x4();
                m2.M11 = cos_angle;
                m2.M13 = -sin_angle;

                m2.M22 = 1;
                
                m2.M31 = sin_angle;
                m2.M33 = cos_angle;

                m2.M44 = 1;

                var mr = m1 * m2;

                System.Numerics.Matrix4x4 *ptr = &mr;
                float * ptr2 = (float *)ptr;
                for (int i = 0; i < 16; i++)
                {
                    mat[i] = *(ptr2 + i);
                }
            }


            GLESV2.GL.glUniformMatrix4fv(model_mat_location, 1, false, mat);
        }

        private static void Resize(int w, int h, uint proj_mat_location)
        {
            GLESV2.GL.glViewport(0, 0, w, h);
            // set orthogonal view so that coordinates [-1, 1] area always visible and proportional on x and y axis
            if (w > h)
            {
                double f = w / (double)h;
                SetOrthoMatrix(-f, f, -1, 1, -1, 1, proj_mat_location);
            }
            else
            {
                double f = h / (double)w;
                SetOrthoMatrix(-1, 1, -f, f, -1, 1, proj_mat_location);
            }
        }
        private static void SetOrthoMatrix(double left, double right, double bottom,
					double top, double n, double f, uint proj_mat_location)
        {
            // set orthogonal matrix
            var mat = new float[16];
            mat[0] = (float)(2.0 / (right - left));
            mat[1] = .0f;
            mat[2] = .0f;
            mat[3] = .0f;

            mat[4] = .0f;
            mat[5] = (float)(2.0 / (top - bottom));
            mat[6] = .0f;
            mat[7] = .0f;

            mat[8] = .0f;
            mat[9] = .0f;
            mat[10] = (float)(-2.0 / (f - n));
            mat[11] = .0f;

            mat[12] = (float)(-(right + left) / (right - left));
            mat[13] = (float)(-(top + bottom) / (top - bottom));
            mat[14] = (float)(-(f + n) / (f - n));
            mat[15] = 1.0f;
            GLESV2.GL.glUniformMatrix4fv(proj_mat_location, 1, false, mat);
        }

    }
}
