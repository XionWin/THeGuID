using System;
using System.Linq;
using Extension;

namespace EGL
{
    using EGLConfig = IntPtr;
    using EGLContext = IntPtr;
    using EGLDisplay = IntPtr;
    using EGLSurface = IntPtr;

    public delegate void PageFilpHandler(int fd, uint frame, uint sec, uint usec, ref int data);
    unsafe public class Context : IDisposable
    {
        public DRM.Drm Drm { get; private set; }
        public GBM.Gbm Gbm { get; private set; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public EGLDisplay EglDisplay { get; private set; }
        public EGLContext EglContext { get; private set; }
        public EGLSurface EglSurface { get; private set; }
        public EGLConfig EGLConfig { get; private set; }
        public int Major { get; private set; }
        public int Minor { get; private set; }

        public bool IsVerticalSynchronization { get; set; }
        
        public RenderableSurfaceType RenderableSurfaceType { get; init; }

        #region ctor
        public Context(int fd, RenderableSurfaceType surfaceType)
        {
            this.Drm = new DRM.Drm(fd);
            this.Gbm = GetGbm(this.Drm, surfaceType);
            var (display, context, surface, config, major, minor) = ContextExtension.CrateContext(this.Gbm, surfaceType);
            this.EglDisplay = display;
            this.EglContext = context;
            this.EglSurface = surface;
            this.EGLConfig = config;
            this.Major = major;
            this.Minor = minor;
        }
        #endregion

        private GBM.Gbm GetGbm(DRM.Drm drm, RenderableSurfaceType surfaceType)
        {
            using (var resources = new DRM.Resources(drm.Fd))
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

            var dev = new GBM.Device(drm.Fd);
            foreach (GBM.SurfaceFormat format in Enum.GetValues(typeof(GBM.SurfaceFormat)))
            {
                if (dev.IsSupportedFormat(format, GBM.SurfaceFlags.Linear))
                {
                    Console.WriteLine(Enum.GetName(typeof(GBM.SurfaceFormat), format));
                }
            }

            var gbm = new GBM.Gbm(dev, drm.Crtc.Width, drm.Crtc.Height, GBM.SurfaceFormat.ARGB8888, GBM.FormatMod.DRM_FORMAT_MOD_LINEAR);
            Console.WriteLine(gbm.ToString());
            return gbm;
        }

        public void Render(Action renderFunc)
        {
            nint page_flip_handler = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(new PageFilpHandler(
                (int fd, uint frame, uint sec, uint usec, ref int data) =>
                {
                    data = 0;
                }
            ));
            var eventCtx = new DRM.EventContext() { version = 2, page_flip_handler = page_flip_handler };

            this.Gbm.Surface
            .RegisterSwapMethod(() => EGL.Egl.eglSwapBuffers(this.EglDisplay, this.EglSurface))
            .Init((bo, fb) =>
            {
                if (DRM.Native.SetCrtc(this.Drm.Fd, this.Drm.Crtc.Id, (uint)fb, 0, 0, new[] { this.Drm.Connector.Id }, this.Drm.Mode) is var setCrtcResult)
                    Console.WriteLine($"set crtc: {setCrtcResult}");
                this.Width = (int)bo.Width;
                this.Height = (int)bo.Height;
            })
            .SwapBuffers(
                renderFunc,
                (bo, fb) =>
                {
                    if(this.IsVerticalSynchronization)
                    {
                        var waitingFlag = 1;
                        DRM.Native.PageFlip(this.Drm.Fd, this.Drm.Crtc.Id, (uint)fb, DRM.PageFlipFlags.FlipEvent, ref waitingFlag);
                        while(waitingFlag != 0)
                        {
                            DRM.Native.HandleEvent(this.Drm.Fd, ref eventCtx);
                        }
                    }
                }
            );
        }



        #region IDisposable implementation
        ~Context()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (this.EglContext != IntPtr.Zero)
                    Egl.eglDestroyContext(this.EglDisplay, this.EglContext);
                if (this.EglDisplay != IntPtr.Zero)
                    Egl.eglTerminate(this.EglDisplay);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error disposing egl context: {0}", ex.ToString());
            }
            finally
            {
                this.EglContext = IntPtr.Zero;
                this.EglDisplay = IntPtr.Zero;
            }
        }
        #endregion
    }
}

