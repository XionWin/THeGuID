using System;
using System.Runtime.InteropServices;

namespace GBM
{
    unsafe public class Surface : IDisposable
    {
        #region pinvoke
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        static extern nint gbm_surface_create(nint gbm, uint width, uint height, SurfaceFormat format, SurfaceFlags flags);

        [DllImport(Lib.Name, EntryPoint = "gbm_surface_create_with_modifiers", CallingConvention = CallingConvention.Cdecl)]
        static extern nint gbm_surface_create_with_modifiers(nint gbm, uint width, uint height, SurfaceFormat format, ulong* modifiers, uint count);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void gbm_surface_destroy(nint surface);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern nint gbm_surface_lock_front_buffer(nint surface);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        static extern void gbm_surface_release_buffer(nint surface, nint buffer);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        static extern int gbm_surface_has_free_buffers(nint surface);
        #endregion

        private nint handler;

        public nint Handler => this.handler;

        #region ctor
        public Surface(Device gbmDev, uint width, uint height, SurfaceFormat format, SurfaceFlags flags)
        {
            this.handler = gbm_surface_create(gbmDev.Handler, width, height, format, flags);

            if (this.handler == IntPtr.Zero)
                throw new NotSupportedException("[GBM] Failed to create GBM surface");
        }
        public Surface(Device gbmDev, uint width, uint height, SurfaceFormat format, ulong modifier)
        {
            this.handler = gbm_surface_create_with_modifiers(gbmDev.Handler, width, height, format, &modifier, 1);

            if (this.handler == IntPtr.Zero)
                throw new NotSupportedException("[GBM] Failed to create GBM surface");
        }


        #endregion

        public bool HasFreeBuffers { get { return gbm_surface_has_free_buffers(handler) > 0; } }

        public void Lock(Action<BufferObject> action)
        {
            unsafe
            {
                var handler = gbm_surface_lock_front_buffer(this.handler);

                if (handler == IntPtr.Zero)
                    throw new Exception("[GBM]: Failed to lock front buffer.");
                using(var bo = new BufferObject(handler))
                {
                    action?.Invoke(bo);
                }
            }
        }

        public void Release(nint bo)
        {
            gbm_surface_release_buffer(handler, bo);
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
            if (handler != IntPtr.Zero)
                gbm_surface_destroy(handler);
            handler = IntPtr.Zero;
        }
        #endregion
    }
}

