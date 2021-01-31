using System;
using System.Linq;
using Extension;

namespace THeGuID
{
    class Program
    {
        public delegate void PageFilpHandler(int fd, uint frame, uint sec, uint usec, ref int data);
        static void Main(string[] args)
        {
            Console.WriteLine("The Grid. A digital frontier...");
            WndCreate();
        }

        static void WndCreate()
        {  
            var deviceName = "/dev/dri/card1";
            var fd = Libc.Context.open(deviceName, Libc.OpenFlags.ReadWrite);
            var drm = new DRM.Drm(fd);
            using (var resources = new DRM.Resources(fd))
            {
                /* find a connected connector: */
                drm.Connector = resources.Connectors.First(_ => _.State == DRM.ConnectionStatus.Connected);
                /* find preferred mode: */
                drm.Mode = drm.Connector.Modes.First(_ => _.type.BitwiseContains(DRM.DrmModeType.Preferred));
                /* find encoder: */
                var encoder = resources.Encoders.FirstOrDefault(_ => _.Id == drm.Connector.EncodeId);
                /* find crtc: */
                drm.Crtc = encoder?.CurrentCrtc ?? resources.Crtcs.FirstOrDefault(_ => _.ModeIsValid);
            }

            Console.WriteLine(drm.ToString());

            var dev = new GBM.Device(fd);
            foreach (GBM.SurfaceFormat format in Enum.GetValues(typeof(GBM.SurfaceFormat)))
            {
                if(dev.IsSupportedFormat(format, GBM.SurfaceFlags.Linear))
                {
                    Console.WriteLine(Enum.GetName(typeof(GBM.SurfaceFormat), format));
                }
            }

            var gbm = new GBM.Gbm(dev, drm.Crtc.Width, drm.Crtc.Height, GBM.SurfaceFormat.ARGB8888, GBM.FormatMod.DRM_FORMAT_MOD_LINEAR);

            Console.WriteLine(gbm.ToString());

            var ctx = new EGL.Context(gbm, EGL.RenderableSurfaceType.OpenGLESV2);
            Console.WriteLine($"GL Extensions: {GLESV2.GL.GetString(GLESV2.GLD.GL_EXTENSIONS)}");
            Console.WriteLine($"GL Version: {GLESV2.GL.GetString(GLESV2.GLD.GL_VERSION)}");
            Console.WriteLine($"GL Sharding Language Version: {GLESV2.GL.GetString(GLESV2.GLD.GL_SHADING_LANGUAGE_VERSION)}");
            Console.WriteLine($"GL Vendor: {GLESV2.GL.GetString(GLESV2.GLD.GL_VENDOR)}");
            Console.WriteLine($"GL Renderer: {GLESV2.GL.GetString(GLESV2.GLD.GL_RENDERER)}");

            GLESV2.GL.glClearColor(1.0f, 1.0f, 1.0f, 0.0f);

            MainLoop(drm, gbm, ctx);
        }

        unsafe static void MainLoop(DRM.Drm drm, GBM.Gbm gbm, EGL.Context ctx)
        {
            nint page_flip_handler = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(new PageFilpHandler(
                (int fd, uint frame, uint sec, uint usec, ref int data) => {
                    data = 0;
                }
            ));
            var eventCtx = new DRM.EventContext(){version = 2, page_flip_handler = page_flip_handler };

            var st = DateTime.Now;
            var frame = 0u;
            var totalTime = TimeSpan.Zero;
            
            gbm.Surface
            .RegisterSwapMethod(() => EGL.Context.eglSwapBuffers(ctx.EglDisplay, ctx.EglSurface))
            .Init((bo, fb) => {
                if (DRM.Native.SetCrtc(drm.Fd, drm.Crtc.Id, (uint)fb, 0, 0, new[] { drm.Connector.Id }, drm.Mode) is var setCrtcResult)
                    Console.WriteLine($"set crtc: {setCrtcResult}");
                GLESV2.GL.glViewport(0, 0, (int)bo.Width, (int)bo.Height);
            })
            .SwapBuffers(
                () => {
                    GLESV2.GL.glClearColor((DateTime.Now.Millisecond % 100 < 50) ? 0.0f : 0.5f, 0.0f, 0.0f, 1.0f);
                    GLESV2.GL.glClear(GLESV2.GLD.GL_COLOR_BUFFER_BIT);

                    var et = DateTime.Now;
                    var dt =  et - st;
                    st = et;

                    frame ++;
                    totalTime += dt;
                    if(totalTime.TotalMilliseconds > 30 * 1000)
                    {
                        using (var mproc = System.Diagnostics.Process.GetCurrentProcess())
                        {
                            Console.WriteLine($"[{DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss")}]: {frame} frames rendered in {(float)totalTime.TotalMilliseconds / 1000:.##} seconds -> FPS={(float)frame / totalTime.TotalMilliseconds * 1000:.##}, memory used: {(double)mproc.WorkingSet64 / 1024 / 1024:.##}M, system memory used: {(double)mproc.PrivateMemorySize64 / 1024 / 1024:.##}M");
                            frame = 0;
                            totalTime = TimeSpan.Zero;
                        }
                    }
                },
                (bo, fb) => {
                    var waitingFlag = 1;
                    DRM.Native.PageFlip(drm.Fd, drm.Crtc.Id, (uint)fb, DRM.PageFlipFlags.FlipEvent, ref waitingFlag);
                    while(waitingFlag != 0)
                    {
                        DRM.Native.HandleEvent(drm.Fd, ref eventCtx);
                    }
                }
            );
        }
    }

}
