using System;
using System.Runtime.InteropServices;

namespace GBM
{
    public struct gbm_surface
    {

    }
    unsafe public class Surface : IDisposable
    {
        #region pinvoke
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        static extern gbm_surface *gbm_surface_create(gbm_device *deviceHandle, uint width, uint height, SurfaceFormat format, SurfaceFlags flags);

        [DllImport(Lib.Name, EntryPoint = "gbm_surface_create_with_modifiers", CallingConvention = CallingConvention.Cdecl)]
        static extern gbm_surface *gbm_surface_create_with_modifiers(gbm_device *deviceHandle, uint width, uint height, SurfaceFormat format, ulong* modifiers, uint count);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void gbm_surface_destroy(gbm_surface *surface);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern gbm_bo *gbm_surface_lock_front_buffer(gbm_surface *surface);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        static extern void gbm_surface_release_buffer(gbm_surface *surface, gbm_bo *bo);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        static extern int gbm_surface_has_free_buffers(gbm_surface *surface);
        #endregion

        private gbm_surface *surfaceHandle;

        public nint Handler => (nint)this.surfaceHandle;

        #region ctor
        public Surface(Device gbmDev, uint width, uint height, SurfaceFormat format, SurfaceFlags flags)
        {
            this.surfaceHandle = gbm_surface_create(gbmDev.Handle, width, height, format, flags);

            if (this.surfaceHandle == null)
                throw new NotSupportedException("[GBM] Failed to create GBM surface");
        }
        public Surface(Device gbmDev, uint width, uint height, SurfaceFormat format, ulong modifier)
        {
            this.surfaceHandle = gbm_surface_create_with_modifiers(gbmDev.Handle, width, height, format, &modifier, 1);

            if (this.surfaceHandle == null)
                throw new NotSupportedException("[GBM] Failed to create GBM surface");
        }


        #endregion

        public bool HasFreeBuffers { get { return gbm_surface_has_free_buffers(surfaceHandle) > 0; } }

        public BufferObject Lock()
        {
            unsafe
            {
                var handler = gbm_surface_lock_front_buffer(this.surfaceHandle);

                if (handler == null)
                    throw new Exception("[GBM]: Failed to lock front buffer.");
                return new BufferObject(handler);
            }
        }

        public void Release(gbm_bo *bo)
        {
            gbm_surface_release_buffer(surfaceHandle, bo);
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
            if (surfaceHandle != null)
                gbm_surface_destroy(surfaceHandle);
            surfaceHandle = null;
        }
        #endregion
    }
}

