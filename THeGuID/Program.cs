using System;
using System.Runtime.InteropServices;
using Graphic.Drawing.Color;

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

                var hsl = new Graphic.Drawing.Color.HSLA(0.0d, 1.0d, 0.0d, 255);
                var direction = true;


                
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
                        vertices[pos].x = (float)Math.Cos(i * (2.0f * Math.PI / (size - 2))) * TRIANGLE_SIZE;
                        vertices[pos].y = (float)Math.Sin(i * (2.0f * Math.PI / (size - 2))) * TRIANGLE_SIZE;
                        
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
                    const double maxL = 1d;


                    GLESV2.GL.glUseProgram(program);

                    uint posAttrib = GLESV2.GL.glGetAttribLocation(program, "position");
                    GLESV2.GL.glEnableVertexAttribArray(posAttrib);
                    GLESV2.GL.glVertexAttribPointerN(posAttrib, 2, false, (uint)Marshal.SizeOf(typeof(Vertex)), 0);

                    uint colorAttrib = GLESV2.GL.glGetAttribLocation(program, "color");
                    GLESV2.GL.glEnableVertexAttribArray(colorAttrib);
                    GLESV2.GL.glVertexAttribPointerN(colorAttrib, 4, false, (uint)Marshal.SizeOf(typeof(Vertex)), 2 * Marshal.SizeOf(typeof(float)));

                    var proj_mat_location = GLESV2.GL.glGetUniformLocation(program, "proj_mat");
                    var model_mat_location = GLESV2.GL.glGetUniformLocation(program, "model_mat");
                    
                    Resize(ctx.Width, ctx.Height, proj_mat_location);

                    ctx.Render(() =>
                        {
                            var rgb = hsl.ToRGB();

                            Resize(ctx.Width, ctx.Height, proj_mat_location);

                            GLESV2.GL.glClearColor((float)rgb.R / 255, (float)rgb.G / 255, (float)rgb.B / 255, .3f);

                            GLESV2.GL.glClear(GLESV2.GLD.GL_COLOR_BUFFER_BIT);


			                SetRotationMatrix((DateTime.Now.Second * 1000.0 + DateTime.Now.Millisecond) / 1000.0 * Math.PI / 2.0, model_mat_location);
                            GLESV2.GL.glDrawArrays(GLESV2.GLD.GL_TRIANGLE_FAN, 0, size);

                            direction = hsl.L switch
                            {
                                >= maxL => false,
                                <= 0 => true,
                                _ => direction,
                            };
                            hsl.H += 2;
                            if (hsl.H >= 360)
                            {
                                hsl.H = 0;
                            }
                            var stepValue = (float)hsl.H / 360 * 0.01;
                            hsl.L += direction ? stepValue : -stepValue;
                            hsl.L = Math.Min(Math.Max(hsl.L, 0), maxL);

                            var et = DateTime.Now;
                            var dt = et - st;
                            st = et;

                            frame++;
                            totalTime += dt;
                            if (totalTime.TotalMilliseconds > 30 * 1000)
                            {
                                using (var mproc = System.Diagnostics.Process.GetCurrentProcess())
                                {
                                    Console.WriteLine($"[{DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss")}]: {frame} frames rendered in {(float)totalTime.TotalMilliseconds / 1000:.##} seconds -> FPS={(float)frame / totalTime.TotalMilliseconds * 1000:.##}, memory used: {(double)mproc.WorkingSet64 / 1024 / 1024:.##}M, system memory used: {(double)mproc.PrivateMemorySize64 / 1024 / 1024:.##}M");
                                    frame = 0;
                                    totalTime = TimeSpan.Zero;
                                }
                            }
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
            var mat = new float[16];
            mat[0] = cos_angle;
            mat[1] = sin_angle;
            mat[2] = 0;
            mat[3] = 0;

            mat[4] = -sin_angle;
            mat[5] = cos_angle;
            mat[6] = 0;
            mat[7] = 0;

            mat[8] = 0;
            mat[9] = 0;
            mat[10] = 1;
            mat[11] = 0;

            mat[12] = 0;
            mat[13] = 0;
            mat[14] = 0;
            mat[15] = 1;
            GLESV2.GL.glUniformMatrix4fv(model_mat_location, 1, false, mat);
        }

        private static void Resize(int w, int h, uint proj_mat_location)
        {
            GLESV2.GL.glViewport(0, 0, w, h);
            // set orthogonal view so that coordinates [-1, 1] area always visible and proportional on x and y axis
            if (w > h)
            {
                float f = w / (float)h;
                SetOrthoMatrix(-f, f, -1, 1, -1, 1, proj_mat_location);
            }
            else
            {
                float f = h / (float)w;
                SetOrthoMatrix(-1, 1, -f, f, -1, 1, proj_mat_location);
            }
        }
        private static void SetOrthoMatrix(float left, float right, float bottom,
					float top, float n, float f, uint proj_mat_location)
        {
            // set orthogonal matrix
            var mat = new float[16];
            mat[0] = 2.0f / (right - left);
            mat[1] = .0f;
            mat[2] = .0f;
            mat[3] = .0f;

            mat[4] = .0f;
            mat[5] = 2.0f / (top - bottom);
            mat[6] = .0f;
            mat[7] = .0f;

            mat[8] = .0f;
            mat[9] = .0f;
            mat[10] = -2.0f / (f - n);
            mat[11] = .0f;

            mat[12] = -(right + left) / (right - left);
            mat[13] = -(top + bottom) / (top - bottom);
            mat[14] = -(f + n) / (f - n);
            mat[15] = 1.0f;
            GLESV2.GL.glUniformMatrix4fv(proj_mat_location, 1, false, mat);
        }

    }
}
