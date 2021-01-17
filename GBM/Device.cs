using System;
using System.Runtime.InteropServices;

namespace GBM
{
    using GbmHandler = IntPtr;
    using GbmSurfaceHandler = IntPtr;

    [Flags]
    public enum gbm_bo_flags : uint
    {
        GBM_BO_USE_SCANOUT = 1 << 0,
        GBM_BO_USE_CURSOR = 1 << 1,
        GBM_BO_USE_CURSOR_64X64 = GBM_BO_USE_CURSOR,
        GBM_BO_USE_RENDERING = 1 << 2,
        GBM_BO_USE_WRITE = 1 << 3,
        GBM_BO_USE_LINEAR = 1 << 4,
    }


    unsafe public class Device : IDisposable
    {

        #region pinvoke
        [DllImport(Lib.Name, EntryPoint = "gbm_create_device", CallingConvention = CallingConvention.Cdecl)]
        static extern GbmHandler CreateDevice(int fd);
        [DllImport(Lib.Name, EntryPoint = "gbm_device_destroy", CallingConvention = CallingConvention.Cdecl)]
        static extern void DestroyDevice(GbmHandler gbm);
        [DllImport(Lib.Name, EntryPoint = "gbm_device_get_fd", CallingConvention = CallingConvention.Cdecl)]
        static extern int DeviceGetFD(nint gbm);
        [DllImport(Lib.Name, EntryPoint = "gbm_device_is_format_supported", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsFormatSupported(nint gbm, SurfaceFormat format, SurfaceFlags usage);


        [DllImport(Lib.Name, EntryPoint = "gbm_surface_create", CallingConvention = CallingConvention.Cdecl)]
        static extern GbmSurfaceHandler gbm_surface_create(GbmHandler gbm,
                   uint width, uint height,
           uint format, gbm_bo_flags flags);

        [DllImport(Lib.Name, EntryPoint = "gbm_surface_create_with_modifiers", CallingConvention = CallingConvention.Cdecl)]
        static extern GbmSurfaceHandler gbm_surface_create_with_modifiers(GbmHandler gbm,
                                        uint width, uint height,
                                        uint format,
                                        ulong* modifiers,
                                        uint count);


        #endregion

        int fd_gpu;
        GbmHandler handle;

        public GbmHandler Handler => this.handle;

        public nint Handle => this.handle;

        #region ctor
        public Device(int _fd_gpu)
        {
            fd_gpu = _fd_gpu;
            handle = CreateDevice(fd_gpu);

            if (handle == IntPtr.Zero)
                throw new NotSupportedException("[GBM] device creation failed.");
        }
        #endregion

        unsafe internal GbmSurfaceHandler CreateSurface(uint width, uint height, uint format, ulong modifier)
        {
            var surfaceHandler = gbm_surface_create_with_modifiers(this.handle, width, height, format, &modifier, 1);
            if (surfaceHandler == IntPtr.Zero)
            {
                surfaceHandler = gbm_surface_create(this.handle, width, height, format, gbm_bo_flags.GBM_BO_USE_SCANOUT | gbm_bo_flags.GBM_BO_USE_RENDERING);
            }
            return surfaceHandler;
        }

        #region IDisposable implementation
        ~Device()
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
            if (handle != IntPtr.Zero)
                DestroyDevice(handle);
            handle = IntPtr.Zero;
        }
        #endregion
    }
}

