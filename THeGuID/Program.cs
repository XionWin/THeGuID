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
            var b = EGL.Context.SwapBuffers(ctx.EglDisplay, ctx.EglSurface);

            gbm.Surface.Lock((bo) =>
            {
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


                uint[] handles = { 0, 0, 0, 0 };
                uint[] strides = { 0, 0, 0, 0 };
                uint[] offsets = { 0, 0, 0, 0 };
                for (int i = 0; i < panelCount; i++)
                {
                    strides[i] = bo.PanelStride(i);
                    handles[i] = bo.PanelHandle(i);
                    offsets[i] = bo.PanelOffset(i);
                }

                unsafe
                {
                    uint fb_id = 0;
                    var r = DRM.Native.AddFB2(gbm.Device.DeviceGetFD(), width, height, (uint)format, handles, strides, offsets, &fb_id, 0);
                    
                    var drmConnectorId = drm.Connector.Id;
                    var mode = drm.Mode;
                    var rr = DRM.Native.SetCrtc(drm.Fd, drm.Crtc.Id, fb_id, 0, 0, &drmConnectorId, 1, ref mode);
                }

                Console.ReadLine();
            });
        }
    }

}
