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
        // int fd_gpu;
        internal EGLDisplay dpy;
        internal EGLContext ctx;
        internal EGLSurface surface;
        internal EGLConfig currentCfg;

        int major, minor;

        public string Version => Egl.QueryString(dpy, Definition.VERSION);
        public string Vendor => Egl.QueryString(dpy, Definition.VENDOR);
        public string Extensions => Egl.QueryString(dpy, Definition.EXTENSIONS);
        public string OffScreenExtensions => Egl.QueryString(IntPtr.Zero, Definition.EXTENSIONS);

        public RenderableSurfaceType RenderableSurfaceType { get; init; }

        public nint EglDisplay => this.dpy;
        public nint EglContext => this.ctx;

        public nint EglSurface => this.surface;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate nint GetPlatformDisplayEXTHandler(uint platform, nint native_display, uint* attrib_list);

        #region ctor
        public Context(GBM.Gbm gbm, RenderableSurfaceType surfaceType)
        {
            const uint EGL_PLATFORM_GBM_KHR = 0x31D7;

            var handler = this.OffScreenExtensions.Contains("EGL_EXT_platform_base") ?
                (GetPlatformDisplayEXTHandler)Marshal.GetDelegateForFunctionPointer(Egl.eglGetProcAddress("eglGetPlatformDisplayEXT"), typeof(GetPlatformDisplayEXTHandler)) : null;

            dpy = handler is null ? Egl.eglGetDisplay((nint)gbm.Device.Handle) : handler(EGL_PLATFORM_GBM_KHR, (nint)gbm.Device.Handle, null);

            if (dpy == IntPtr.Zero)
                throw new NotSupportedException("[EGL] GetDisplay failed.: " + Egl.eglGetError());

            if (!Egl.eglInitialize(dpy, out major, out minor))
                throw new NotSupportedException("[EGL] Failed to initialize EGL display. Error code: " + Egl.eglGetError());


            var isModifiersSupported = this.Extensions.Contains("EGL_EXT_image_dma_buf_import_modifiers");


            if (!Egl.eglBindAPI(RenderApi.GLES))
                throw new NotSupportedException("[EGL] Failed to bind EGL Api: " + Egl.eglGetError());

            var desiredConfig = new[] {
                Definition.SURFACE_TYPE, (int)this.RenderableSurfaceType,
                Definition.RENDERABLE_TYPE, Definition.OPENGL_BIT,
                Definition.RED_SIZE, 8,
                Definition.GREEN_SIZE, 8,
                Definition.BLUE_SIZE, 8,
                Definition.ALPHA_SIZE, 8,
                Definition.STENCIL_SIZE, 8,
                Definition.SAMPLE_BUFFERS, 0,
                Definition.SAMPLES, 0,
                Definition.NONE
            };

            currentCfg = this.GetConfig(desiredConfig);

            var contextAttrib = new[] {
                Definition.CONTEXT_CLIENT_VERSION, 2,
                Definition.NONE
            };
            ctx = Egl.CreateContext(dpy, currentCfg, IntPtr.Zero, contextAttrib);
            if (ctx == IntPtr.Zero)
                throw new NotSupportedException(String.Format("[EGL] Failed to create egl context, error {0}.", Egl.eglGetError()));

            surface = Egl.eglCreateWindowSurface(dpy, currentCfg, gbm.Surface.Handle, null);

            if (surface == IntPtr.Zero)
                throw new NotSupportedException(String.Format("[EGL] Failed to create egl surface, error {0}.", Egl.eglGetError()));

            if (!Egl.eglMakeCurrent(dpy, surface, surface, ctx))
            {
                throw new NotSupportedException(String.Format("[EGL] Failed to make current, error {0}.", Egl.eglGetError()));
            }

        }
        #endregion

        
        public nint GetConfig(int[] desiredConfig)
        {
            int num_configs;
            var configs = new nint[1];
            if (!Egl.eglChooseConfig(dpy, desiredConfig, configs, 1, out num_configs) || num_configs < 1)
                throw new NotSupportedException(String.Format("[EGL] Failed to retrieve GraphicsMode, error {0}", Egl.eglGetError()));
            return configs[0];
        }
        public nint[] GetAllConfigs(int[] desiredConfig)
        {
            int num_configs;
            if (!Egl.eglChooseConfig(dpy, desiredConfig, null, 0, out num_configs) || num_configs == 0)
                throw new NotSupportedException(String.Format("[EGL] Failed to retrieve GraphicsMode, error {0}", Egl.eglGetError()));

            var configs = new nint[num_configs];
            if (!Egl.eglChooseConfig(dpy, null, configs, num_configs, out num_configs))
                throw new NotSupportedException(String.Format("[EGL] Failed to retrieve GraphicsMode, error {0}", Egl.eglGetError()));
            return configs;
        }
        public void DumpAllConf(int[] desiredConfig)
        {
            Console.Write("EGL Configs");
            nint[] configs = this.GetAllConfigs(desiredConfig);
            int[] attribs = new int[] {
                (int)Definitions.Attribute.BufferSize,
                Definition.RED_SIZE,
                Definition.GREEN_SIZE,
                Definition.BLUE_SIZE,
                Definition.ALPHA_SIZE,
                (int)Definitions.Attribute.DepthSize,
                Definition.WIDTH,
                Definition.HEIGHT,
                (int)Definitions.Attribute.Samples,
                (int)Definitions.Attribute.SampleBuffers,
                (int)Definitions.Attribute.RenderableType,
                (int)Definitions.Attribute.SurfaceType,
                (int)Definitions.Attribute.Level,
                (int)Definitions.Attribute.ConfigCaveat,
            };

            for (int i = 0; i < configs.Length; i++)
            {
                nint conf = configs[i];
                Console.Write("{0,-3}:", i);
                for (int j = 0; j < attribs.Length; j++)
                {
                    int value;
                    Egl.eglGetConfigAttrib(dpy, conf, attribs[j], out value);
                    Console.Write((j == 0 ? string.Empty : ", ") + "{0} = {1}", Egl.EglConstToString((int)attribs[j]), value);
                }
                Console.Write("\n");
            }
        }

        public void ResetMakeCurrent()
        {
            if (!Egl.eglMakeCurrent(dpy, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero))
                Console.WriteLine("egl clear current ctx failed");
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
                if (ctx != IntPtr.Zero)
                    Egl.eglDestroyContext(dpy, ctx);
                if (dpy != IntPtr.Zero)
                    Egl.eglTerminate(dpy);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error disposing egl context: {0}", ex.ToString());
            }
            finally
            {
                ctx = IntPtr.Zero;
                dpy = IntPtr.Zero;
            }
        }
        #endregion
    }
}

