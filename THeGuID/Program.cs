using System;
using System.Linq;
using Extension;

namespace THeGuID
{
    class Program
    {
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
                /* find preferred mode or the highest resolution mode: */
                drm.Mode = drm.Connector.Modes.First(_ => _.type.BitwiseContains(DRM.DrmModeType.Preferred));
                /* find encoder: */
                var encoder = resources.Encoders.FirstOrDefault(_ => _.Id == drm.Connector.EncodeId);

                /* find crtc: */
                drm.Crtc = encoder?.CurrentCrtc ?? resources.Crtcs.FirstOrDefault(_ => _.ModeIsValid);
            }

            Console.WriteLine(drm.ToString());

            var gbm = new GBM.Gbm(new GBM.Device(fd), drm.Crtc.Width, drm.Crtc.Height, GBM.SurfaceFormat.XRGB8888, GBM.FormatMod.DRM_FORMAT_MOD_LINEAR);

            Console.WriteLine(gbm.ToString());

            var ctx = new EGL.Context(gbm, EGL.RenderableSurfaceType.OpenGLESV2);
            Console.WriteLine($"GL Extensions: {GLESV2.GL.GetString(GLESV2.GLD.GL_EXTENSIONS)}");
            Console.WriteLine($"GL Version: {GLESV2.GL.GetString(GLESV2.GLD.GL_VERSION)}");
            Console.WriteLine($"GL Sharding Language Version: {GLESV2.GL.GetString(GLESV2.GLD.GL_SHADING_LANGUAGE_VERSION)}");
            Console.WriteLine($"GL Vendor: {GLESV2.GL.GetString(GLESV2.GLD.GL_VENDOR)}");
            Console.WriteLine($"GL Renderer: {GLESV2.GL.GetString(GLESV2.GLD.GL_RENDERER)}");

            MainLoop(drm, gbm, ctx);

            Console.ReadLine();
        }

        static void MainLoop(DRM.Drm drm, GBM.Gbm gbm, EGL.Context ctx)
        {
            EGL.Context.eglSwapBuffers(ctx.EglDisplay, ctx.EglSurface);
            var bo = gbm.Surface.Lock();
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

            while (true)
            {
                if (EGL.Context.eglSwapBuffers(ctx.EglDisplay, ctx.EglSurface))
                {
                    bo = gbm.Surface.Lock();


                    Console.WriteLine($"Loop: {DateTime.Now.Millisecond}");

                    System.Threading.Thread.Sleep(10);
                }
            }

        }
    }

}
