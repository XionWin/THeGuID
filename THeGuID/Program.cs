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

            var gbm = new GBM.Gbm(new GBM.Device(fd), drm.Crtc.Width, drm.Crtc.Height, GBM.SurfaceFormat.ARGB8888, GBM.FormatMod.DRM_FORMAT_MOD_LINEAR);

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
            EGL.Context.eglSwapBuffers(ctx.EglDisplay, ctx.EglSurface);
            gbm.Surface.Lock(bo => {
                
                var userData = bo.UserData;

                var width = bo.Width;
                var height = bo.Height;
                var format = bo.Format;
                var modifier = bo.Modifier;

                var panelCount = bo.PanelCount;
                var handle = bo.Handle;
                Console.WriteLine($"userData: {userData}");
                Console.WriteLine($"width: {width}");
                Console.WriteLine($"height: {height}");
                Console.WriteLine($"format: {format}");
                Console.WriteLine($"modifier: {modifier}");
                Console.WriteLine($"panelCount: {panelCount}");
                Console.WriteLine($"handle: {handle}");


                var handles = new uint[panelCount];
                var strides = new uint[panelCount];
                var offsets = new uint[panelCount];
                for (int i = 0; i < panelCount; i++)
                {
                    strides[i] = bo.PanelStride(i);
                    handles[i] = bo.PanelHandle(i);
                    offsets[i] = bo.PanelOffset(i);
                }

                if (DRM.Native.GetFB2(gbm.Device.DeviceGetFD(), width, height, (uint)format, handles, strides, offsets, 0) is var fb)
                {
                    if (DRM.Native.SetCrtc(drm.Fd, drm.Crtc.Id, fb, 0, 0, new[] { drm.Connector.Id }, drm.Mode) is var setCrtcResult)
                        Console.WriteLine($"set crtc: {setCrtcResult}");
                }
                
                GLESV2.GL.glViewport(0, 0, (int)width, (int)height);

            });

            nint page_flip_handler = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(new PageFilpHandler(
                (int fd, uint frame, uint sec, uint usec, ref int data) => {
                    data = 0;
                }
            ));
            var eventCtx = new DRM.EventContext(){version = 2, page_flip_handler = page_flip_handler };

            var st = DateTime.Now;
            var frame = 0u;
            var totalTime = TimeSpan.Zero;
            while (true)
            {
                var et = DateTime.Now;
                var dt =  et - st;
                st = et;
                GLESV2.GL.glClearColor((DateTime.Now.Millisecond % 100 < 50) ? 0.0f : 1.0f, 0.0f, 0.0f, 1.0f);
                GLESV2.GL.glClear(GLESV2.GLD.GL_COLOR_BUFFER_BIT);

                if (EGL.Context.eglSwapBuffers(ctx.EglDisplay, ctx.EglSurface))
                {
                    gbm.Surface.Lock(bo => {
                        var userData = bo.UserData;

                        var width = bo.Width;
                        var height = bo.Height;
                        var format = bo.Format;
                        var panelCount = bo.PanelCount;

                        var handles = new uint[panelCount];
                        var strides = new uint[panelCount];
                        var offsets = new uint[panelCount];
                        for (int i = 0; i < panelCount; i++)
                        {
                            strides[i] = bo.PanelStride(i);
                            handles[i] = bo.PanelHandle(i);
                            offsets[i] = bo.PanelOffset(i);
                        }
                    
                        if (DRM.Native.GetFB2(gbm.Device.DeviceGetFD(), width, height, (uint)format, handles, strides, offsets, 0) is var fb)
                        {
                            var waitingFlag = 1;
                            DRM.Native.PageFlip(drm.Fd, drm.Crtc.Id, fb, DRM.PageFlipFlags.FlipEvent, ref waitingFlag);
                            while(waitingFlag != 0)
                            {
                                DRM.Native.HandleEvent(drm.Fd, ref eventCtx);
                            }
                        }
                    });
                }

                frame ++;
                totalTime += dt;
                if(totalTime.TotalMilliseconds > 2000)
                {
                    Console.WriteLine($"{frame} frames rendered in {(float)totalTime.TotalMilliseconds / 1000:.##} seconds -> FPS={(float)frame / totalTime.TotalMilliseconds * 1000:.##}");
                    frame = 0;
                    totalTime = TimeSpan.Zero;
                }
            }

        }
    }

}
