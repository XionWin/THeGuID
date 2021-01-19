﻿using System;
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
            var deviceName = "/dev/dri/card0";
            var drm = new DRM.Drm();
            var fd = Libc.Context.open(deviceName, Libc.OpenFlags.ReadWrite);
            using (var resources = new DRM.Resources(fd))
            {
                /* find a connected connector: */
                drm.Connector = resources.Connectors.First(_ => _.State == DRM.ConnectionStatus.Connected);
                /* find preferred mode or the highest resolution mode: */
                var drmMode = drm.Connector.Modes.First(_ => _.type.BitwiseContains(DRM.DrmModeType.Preferred));
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

            MainLoop(gbm, ctx);

            Console.ReadLine();
        }

        static void MainLoop(GBM.Gbm gbm, EGL.Context ctx)
        {
            var b = EGL.Context.SwapBuffers(ctx.EglDisplay, ctx.EglSurface);

            gbm.Surface.Lock(bo =>
            {
                var userData = bo.UserData;
                var panelCount = bo.PanelCount;
                var width = bo.Width;
                var height = bo.Height;
                var format = bo.Format;
                var stride = bo.Stride;
                var modifier = bo.Modifier;

                for (int i = 0; i < panelCount; i++)
                {
                    var s1 = bo.PanelStride(i);
                    var offset = bo.PanelOffset(i);
                }
            });
        }
    }

}
