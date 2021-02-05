using System;
using Graphic.Drawing.Color;

namespace THeGuID
{
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

                const double maxL = 1d;
                ctx.Render(() =>
                    {
                        var rgb = hsl.ToRGB();
                        GLESV2.GL.glClearColor((float)rgb.R / 255, (float)rgb.G / 255, (float)rgb.B / 255, .8f);
                        GLESV2.GL.glClear(GLESV2.GLD.GL_COLOR_BUFFER_BIT);
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
}
