using System;
using System.Runtime.InteropServices;
using EGL.Definitions;

namespace EGL
{
    using EGLNativeDisplayType = IntPtr;
    using EGLNativeWindowType = IntPtr;
    using EGLNativePixmapType = IntPtr;
    using EGLConfig = IntPtr;
    using EGLContext = IntPtr;
    using EGLDisplay = IntPtr;
    using EGLSurface = IntPtr;

    unsafe public static class Egl
    {
        #region pinvoke
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern ErrorCode eglGetError();
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern EGLDisplay eglGetDisplay(EGLNativeDisplayType display_id);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern bool eglInitialize(EGLDisplay dpy, out int major, out int minor);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern bool eglTerminate(EGLDisplay dpy);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        private static extern nint eglQueryString(EGLDisplay dpy, int name);
        public static string QueryString(EGLDisplay dpy, int name) => Marshal.PtrToStringAuto(eglQueryString(dpy, name));

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern bool eglGetConfigs(EGLDisplay dpy, EGLConfig[] configs, int config_size, out int num_config);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern bool eglChooseConfig(EGLDisplay dpy, int[] attrib_list, [In, Out] EGLConfig[] configs, int config_size, out int num_config);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern bool eglGetConfigAttrib(EGLDisplay dpy, EGLConfig config, int attribute, out int value);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern bool eglBindAPI(RenderApi api);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern int eglQueryAPI();
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern bool eglWaitClient();
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern bool eglReleaseThread();

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern bool eglSwapInterval(EGLDisplay dpy, int interval);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        static extern nint eglCreateContext(EGLDisplay dpy, EGLConfig config, EGLContext share_context, int[] attrib_list);
        public static EGLContext CreateContext(EGLDisplay dpy, EGLConfig config, EGLContext share_context, int[] attrib_list)
        {
            nint ptr = eglCreateContext(dpy, config, share_context, attrib_list);
            if (ptr == IntPtr.Zero)
                throw new Exception(String.Format("Failed to create EGL context, error: {0}.", eglGetError()));
            return ptr;
        }
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern bool eglDestroyContext(EGLDisplay dpy, EGLContext ctx);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern bool eglMakeCurrent(EGLDisplay dpy, EGLSurface draw, EGLSurface read, EGLContext ctx);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern EGLContext eglGetCurrentContext();
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern EGLSurface eglGetCurrentSurface(int readdraw);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern EGLDisplay eglGetCurrentDisplay();
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern bool eglQueryContext(EGLDisplay dpy, EGLContext ctx, int attribute, out int value);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern bool eglWaitGL();
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern bool eglWaitNative(int engine);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern bool CopyBuffers(EGLDisplay dpy, EGLSurface surface, EGLNativePixmapType target);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern nint eglGetProcAddress(string funcname);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern nint eglGetProcAddress(nint funcname);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern EGLSurface eglCreateWindowSurface(EGLDisplay dpy, EGLConfig config, EGLNativeWindowType native_window, int[] attrib_list);

        // EGL_EXT_platform_base
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern EGLDisplay eglGetPlatformDisplayEXT(int platform, EGLNativeDisplayType native_display, int[] attrib_list);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern EGLSurface eglCreatePlatformWindowSurfaceEXT(EGLDisplay dpy, EGLConfig config, EGLNativeWindowType native_window, int[] attrib_list);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern EGLSurface eglCreatePlatformPixmapSurfaceEXT(EGLDisplay dpy, EGLConfig config, EGLNativePixmapType native_pixmap, int[] attrib_list);

        // EGL_ANGLE_query_surface_pointer 
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern bool eglQuerySurfacePointerANGLE(EGLDisplay display, EGLSurface surface, int attribute, out nint value);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern bool eglSwapBuffers(EGLDisplay dpy, EGLSurface surface);

        // Returns true if Egl drivers exist on the system.
        public static bool IsSupported
        {
            get
            {
                try { Egl.eglGetCurrentContext(); }
                catch { return false; }
                return true;
            }
        }
        #endregion

        public static string EglConstToString(int cst)
        {
            switch (cst)
            {
                /*			not hex value: EGL_DONT_CARE                     ((EGLint)-1)
                            not hex value: EGL_FALSE                         0
                            not hex value: EGL_NO_CONTEXT                    ((EGLContext)0)
                            not hex value: EGL_NO_DISPLAY                    ((EGLDisplay)0)
                            not hex value: EGL_NO_SURFACE                    ((EGLSurface)0)
                            not hex value: EGL_TRUE                          1
                            not hex value: EGL_VERSION_1_1 1
                            not hex value: EGL_VERSION_1_2 1
                            not hex value: EGL_DISPLAY_SCALING               10000
                            not hex value: EGL_UNKNOWN                       ((EGLint)-1)
                            not hex value: EGL_VERSION_1_3 1
                            not hex value: EGL_VERSION_1_4 1
                            not hex value: EGL_DEFAULT_DISPLAY               ((EGLNativeDisplayType)0)
                            not hex value: EGL_VERSION_1_5 1
                            parsing error: EGL_FOREVER                       0xFFFFFFFFFFFFFFFFull
                            not hex value: EGL_NO_SYNC                       ((EGLSync)0)
                            not hex value: EGL_NO_IMAGE                      ((EGLImage)0)*/
                case 1:
                    return "PBUFFER_BIT|OPENGL_ES_BIT|CONTEXT_OPENGL_CORE_PROFILE_BIT|SYNC_FLUSH_COMMANDS_BIT";
                case 2:
                    return "PIXMAP_BIT|OPENVG_BIT|CONTEXT_OPENGL_COMPATIBILITY_PROFILE_BIT";
                case 4:
                    return "WINDOW_BIT|OPENGL_ES2_BIT";
                case 8:
                    return "OPENGL_BIT";
                case 32:
                    return "VG_COLORSPACE_LINEAR_BIT";
                case 64:
                    return "VG_ALPHA_FORMAT_PRE_BIT|OPENGL_ES3_BIT";
                case 512:
                    return "MULTISAMPLE_RESOLVE_BOX_BIT";
                case 1024:
                    return "SWAP_BEHAVIOR_PRESERVED_BIT";
                case 12288:
                    return "SUCCESS";
                case 12289:
                    return "NOT_INITIALIZED";
                case 12290:
                    return "BAD_ACCESS";
                case 12291:
                    return "BAD_ALLOC";
                case 12292:
                    return "BAD_ATTRIBUTE";
                case 12293:
                    return "BAD_CONFIG";
                case 12294:
                    return "BAD_CONTEXT";
                case 12295:
                    return "BAD_CURRENT_SURFACE";
                case 12296:
                    return "BAD_DISPLAY";
                case 12297:
                    return "BAD_MATCH";
                case 12298:
                    return "BAD_NATIVE_PIXMAP";
                case 12299:
                    return "BAD_NATIVE_WINDOW";
                case 12300:
                    return "BAD_PARAMETER";
                case 12301:
                    return "BAD_SURFACE";
                case 12302:
                    return "CONTEXT_LOST";
                case 12320:
                    return "BUFFER_SIZE";
                case 12321:
                    return "ALPHA_SIZE";
                case 12322:
                    return "BLUE_SIZE";
                case 12323:
                    return "GREEN_SIZE";
                case 12324:
                    return "RED_SIZE";
                case 12325:
                    return "DEPTH_SIZE";
                case 12326:
                    return "STENCIL_SIZE";
                case 12327:
                    return "CONFIG_CAVEAT";
                case 12328:
                    return "CONFIG_ID";
                case 12329:
                    return "LEVEL";
                case 12330:
                    return "MAX_PBUFFER_HEIGHT";
                case 12331:
                    return "MAX_PBUFFER_PIXELS";
                case 12332:
                    return "MAX_PBUFFER_WIDTH";
                case 12333:
                    return "NATIVE_RENDERABLE";
                case 12334:
                    return "NATIVE_VISUAL_ID";
                case 12335:
                    return "NATIVE_VISUAL_TYPE";
                case 12337:
                    return "SAMPLES";
                case 12338:
                    return "SAMPLE_BUFFERS";
                case 12339:
                    return "SURFACE_TYPE";
                case 12340:
                    return "TRANSPARENT_TYPE";
                case 12341:
                    return "TRANSPARENT_BLUE_VALUE";
                case 12342:
                    return "TRANSPARENT_GREEN_VALUE";
                case 12343:
                    return "TRANSPARENT_RED_VALUE";
                case 12344:
                    return "NONE";
                case 12345:
                    return "BIND_TO_TEXTURE_RGB";
                case 12346:
                    return "BIND_TO_TEXTURE_RGBA";
                case 12347:
                    return "MIN_SWAP_INTERVAL";
                case 12348:
                    return "MAX_SWAP_INTERVAL";
                case 12349:
                    return "LUMINANCE_SIZE";
                case 12350:
                    return "ALPHA_MASK_SIZE";
                case 12351:
                    return "COLOR_BUFFER_TYPE";
                case 12352:
                    return "RENDERABLE_TYPE";
                case 12353:
                    return "MATCH_NATIVE_PIXMAP";
                case 12354:
                    return "CONFORMANT";
                case 12368:
                    return "SLOW_CONFIG";
                case 12369:
                    return "NON_CONFORMANT_CONFIG";
                case 12370:
                    return "TRANSPARENT_RGB";
                case 12371:
                    return "VENDOR";
                case 12372:
                    return "VERSION";
                case 12373:
                    return "EXTENSIONS";
                case 12374:
                    return "HEIGHT";
                case 12375:
                    return "WIDTH";
                case 12376:
                    return "LARGEST_PBUFFER";
                case 12377:
                    return "DRAW";
                case 12378:
                    return "READ";
                case 12379:
                    return "CORE_NATIVE_ENGINE";
                case 12380:
                    return "NO_TEXTURE";
                case 12381:
                    return "TEXTURE_RGB";
                case 12382:
                    return "TEXTURE_RGBA";
                case 12383:
                    return "TEXTURE_2D";
                case 12416:
                    return "TEXTURE_FORMAT";
                case 12417:
                    return "TEXTURE_TARGET";
                case 12418:
                    return "MIPMAP_TEXTURE";
                case 12419:
                    return "MIPMAP_LEVEL";
                case 12420:
                    return "BACK_BUFFER";
                case 12421:
                    return "SINGLE_BUFFER";
                case 12422:
                    return "RENDER_BUFFER";
                case 12423:
                    return "COLORSPACE|VG_COLORSPACE";
                case 12424:
                    return "ALPHA_FORMAT|VG_ALPHA_FORMAT";
                case 12425:
                    return "COLORSPACE_sRGB|VG_COLORSPACE_sRGB|GL_COLORSPACE_SRGB";
                case 12426:
                    return "COLORSPACE_LINEAR|VG_COLORSPACE_LINEAR|GL_COLORSPACE_LINEAR";
                case 12427:
                    return "ALPHA_FORMAT_NONPRE|VG_ALPHA_FORMAT_NONPRE";
                case 12428:
                    return "ALPHA_FORMAT_PRE|VG_ALPHA_FORMAT_PRE";
                case 12429:
                    return "CLIENT_APIS";
                case 12430:
                    return "RGB_BUFFER";
                case 12431:
                    return "LUMINANCE_BUFFER";
                case 12432:
                    return "HORIZONTAL_RESOLUTION";
                case 12433:
                    return "VERTICAL_RESOLUTION";
                case 12434:
                    return "PIXEL_ASPECT_RATIO";
                case 12435:
                    return "SWAP_BEHAVIOR";
                case 12436:
                    return "BUFFER_PRESERVED";
                case 12437:
                    return "BUFFER_DESTROYED";
                case 12438:
                    return "OPENVG_IMAGE";
                case 12439:
                    return "CONTEXT_CLIENT_TYPE";
                case 12440:
                    return "CONTEXT_CLIENT_VERSION|CONTEXT_MAJOR_VERSION";
                case 12441:
                    return "MULTISAMPLE_RESOLVE";
                case 12442:
                    return "MULTISAMPLE_RESOLVE_DEFAULT";
                case 12443:
                    return "MULTISAMPLE_RESOLVE_BOX";
                case 12444:
                    return "CL_EVENT_HANDLE";
                case 12445:
                    return "GL_COLORSPACE";
                case 12448:
                    return "OPENGL_ES_API";
                case 12449:
                    return "OPENVG_API";
                case 12450:
                    return "OPENGL_API";
                case 12465:
                    return "GL_TEXTURE_2D";
                case 12466:
                    return "GL_TEXTURE_3D";
                case 12467:
                    return "GL_TEXTURE_CUBE_MAP_POSITIVE_X";
                case 12468:
                    return "GL_TEXTURE_CUBE_MAP_NEGATIVE_X";
                case 12469:
                    return "GL_TEXTURE_CUBE_MAP_POSITIVE_Y";
                case 12470:
                    return "GL_TEXTURE_CUBE_MAP_NEGATIVE_Y";
                case 12471:
                    return "GL_TEXTURE_CUBE_MAP_POSITIVE_Z";
                case 12472:
                    return "GL_TEXTURE_CUBE_MAP_NEGATIVE_Z";
                case 12473:
                    return "GL_RENDERBUFFER";
                case 12476:
                    return "GL_TEXTURE_LEVEL";
                case 12477:
                    return "GL_TEXTURE_ZOFFSET";
                case 12498:
                    return "IMAGE_PRESERVED";
                case 12528:
                    return "SYNC_PRIOR_COMMANDS_COMPLETE";
                case 12529:
                    return "SYNC_STATUS";
                case 12530:
                    return "SIGNALED";
                case 12531:
                    return "UNSIGNALED";
                case 12533:
                    return "TIMEOUT_EXPIRED";
                case 12534:
                    return "CONDITION_SATISFIED";
                case 12535:
                    return "SYNC_TYPE";
                case 12536:
                    return "SYNC_CONDITION";
                case 12537:
                    return "SYNC_FENCE";
                case 12539:
                    return "CONTEXT_MINOR_VERSION";
                case 12541:
                    return "CONTEXT_OPENGL_PROFILE_MASK";
                case 12542:
                    return "SYNC_CL_EVENT";
                case 12543:
                    return "SYNC_CL_EVENT_COMPLETE";
                case 12720:
                    return "CONTEXT_OPENGL_DEBUG";
                case 12721:
                    return "CONTEXT_OPENGL_FORWARD_COMPATIBLE";
                case 12722:
                    return "CONTEXT_OPENGL_ROBUST_ACCESS";
                case 12733:
                    return "CONTEXT_OPENGL_RESET_NOTIFICATION_STRATEGY";
                case 12734:
                    return "NO_RESET_NOTIFICATION";
                case 12735:
                    return "LOSE_CONTEXT_ON_RESET";
                default:
                    return "unknown";
            }
        }
    }
}
