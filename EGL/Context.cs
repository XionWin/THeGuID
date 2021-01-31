using System;
using System.Runtime.InteropServices;
using EGL.Definitions;

namespace EGL
{
    using EGLConfig = IntPtr;
    using EGLContext = IntPtr;
    using EGLDisplay = IntPtr;
    using EGLSurface = IntPtr;

    unsafe public class Context : IDisposable
    {
        public EGLDisplay EglDisplay {get; private set; }
        public EGLContext EglContext {get; private set; }
        public EGLSurface EglSurface {get; private set; }
        public EGLConfig EGLConfig {get; private set; }
        public int Major {get; private set; }
        public int Minor {get; private set; }


        public RenderableSurfaceType RenderableSurfaceType { get; init; }


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate nint GetPlatformDisplayEXTHandler(uint platform, nint native_display, uint* attrib_list);

        #region ctor
        public Context(GBM.Gbm gbm, RenderableSurfaceType surfaceType)
        {
            var (display, context, surface, config, major, minor) = ContextExtension.CrateContext(gbm, surfaceType);
            this.EglDisplay = display;
            this.EglContext = context;
            this.EglSurface = surface;
            this.EGLConfig = config;
            this.Major = major;
            this.Minor = minor;
        }
        #endregion



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

