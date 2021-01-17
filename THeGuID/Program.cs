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

            var gbm = new GBM.Gbm(new GBM.Device(fd), drm.Crtc.Width, drm.Crtc.Height, GBM.Format.DRM_FORMAT_XRGB8888, GBM.FormatMod.DRM_FORMAT_MOD_LINEAR);

            Console.WriteLine(gbm.ToString());

            var ctx = new EGL.Context(gbm, EGL.RenderableSurfaceType.OpenGLESV2);
            
            // ctx.DumpAllConf();

            var exts = GLESV2.GL.GetString(GLESV2.GL.GL_EXTENSIONS);
            var ver = GLESV2.GL.GetString(GLESV2.GL.GL_VERSION);
            var lanVer = GLESV2.GL.GetString(GLESV2.GL.GL_SHADING_LANGUAGE_VERSION);
            var vendor = GLESV2.GL.GetString(GLESV2.GL.GL_VENDOR);
            var renderer = GLESV2.GL.GetString(GLESV2.GL.GL_RENDERER);
            Console.WriteLine($"GL exts: {exts}");
            Console.WriteLine($"GL ver: {ver}");
            Console.WriteLine($"GL lanVer: {lanVer}");
            Console.WriteLine($"GL vendor: {vendor}");
            Console.WriteLine($"GL renderer: {renderer}");

            Console.ReadLine();
        }
    }

}
