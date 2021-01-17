using System;
using System.Runtime.InteropServices;

namespace GBM
{
    unsafe public class Surface : IDisposable
    {
        #region pinvoke
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        static extern nint gbm_surface_create(nint gbm, int width, int height, SurfaceFormat format, SurfaceFlags flags);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void gbm_surface_destroy(nint surface);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        static extern gbm_bo* gbm_surface_lock_front_buffer(nint surface);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        static extern void gbm_surface_release_buffer(nint surface, gbm_bo* buffer);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        static extern int gbm_surface_has_free_buffers(nint surface);
        #endregion

        internal nint handle;

        #region ctor
        public Surface(Device gbmDev, int width, int height, SurfaceFlags flags, SurfaceFormat format = SurfaceFormat.ARGB8888)
        {
            handle = gbm_surface_create(gbmDev.Handle, width, height, format, flags);

            if (handle == IntPtr.Zero)
                throw new NotSupportedException("[GBM] Failed to create GBM surface");
        }
        #endregion

        public bool HasFreeBuffers { get { return gbm_surface_has_free_buffers(handle) > 0; } }

        public gbm_bo* Lock()
        {
            gbm_bo* bo = gbm_surface_lock_front_buffer(handle);
            if (bo == null)
                throw new Exception("[GBM]: Failed to lock front buffer.");
            return bo;
        }
        public void Release(gbm_bo* bo)
        {
            gbm_surface_release_buffer(handle, bo);
        }

        #region IDisposable implementation
        ~Surface()
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
                gbm_surface_destroy(handle);
            handle = IntPtr.Zero;
        }
        #endregion
    }
}

