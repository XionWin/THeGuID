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
        static extern gbm_bo *gbm_surface_lock_front_buffer(gbm_surface *surface);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        static extern void gbm_surface_release_buffer(gbm_surface *surface, gbm_bo *bo);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        static extern bool gbm_surface_has_free_buffers(gbm_surface *surface);
        #endregion

        private gbm_surface *surfaceHandle;

        public nint Handle => (nint)this.surfaceHandle;


        public Device Device { get; private set; }

        #region ctor
        public Surface(Device gbmDev, uint width, uint height, SurfaceFormat format, SurfaceFlags flags)
        {
            this.Device = gbmDev;
            this.surfaceHandle = gbm_surface_create(gbmDev.Handle, width, height, format, flags);

            if (this.surfaceHandle == null)
                throw new NotSupportedException("[GBM] Failed to create GBM surface");
        }
        public Surface(Device gbmDev, uint width, uint height, SurfaceFormat format, ulong modifier)
        {
            this.Device = gbmDev;
            this.surfaceHandle = gbm_surface_create_with_modifiers(gbmDev.Handle, width, height, format, &modifier, 1);

            if (this.surfaceHandle == null)
                throw new NotSupportedException("[GBM] Failed to create GBM surface");
        }

        #endregion

        public bool HasFreeBuffers => gbm_surface_has_free_buffers(surfaceHandle);
        private gbm_bo *boHandle;

        private Action eglSwap;
        public Surface RegisterSwapMethod(Action eglSwap)
        {
            this.eglSwap = eglSwap;
            return this;
        }

        public Surface Initialize(Action<BufferObject, uint> action)
        {
            this.eglSwap();
            this.Lock((bo, fb) => {
                action(bo, fb);
            });
            return this;
        }

        public void SwapBuffers(Action renderAction, Action<BufferObject, uint> action)
        {
            while (true)
            {
                renderAction();
                this.eglSwap();
                this.Lock((bo, fb) => {
                    action(bo, fb);
                });
            }
        }

        private void Lock(Action<BufferObject, uint> action)
        {
            unsafe
            {
                var lastBo = this.boHandle;
                this.boHandle = gbm_surface_lock_front_buffer(this.surfaceHandle);

                if (this.boHandle == null)
                    throw new Exception("[GBM]: Failed to lock front buffer.");
                
                var bo = new BufferObject(this.boHandle);
                action?.Invoke(bo, GetFb(bo));
                if (lastBo is not null)
                {
                    this.Release(lastBo);
                }
            }
        }

        private uint GetFb(BufferObject bo)
        {
            if(bo.UserData is var fb && fb == IntPtr.Zero)
            {
                var userData = bo.UserData;

                var width = bo.Width;
                var height = bo.Height;
                var format = bo.Format;
                var panelCount = bo.PanelCount;

                var handles = new uint[panelCount];
                var strides = new uint[panelCount];
                var offsets = new uint[panelCount];
                for (int i = 0; i < panelCount; i++)
                {
                    strides[i] = bo.PanelStride(i);
                    handles[i] = bo.PanelHandle(i);
                    offsets[i] = bo.PanelOffset(i);
                }

                fb = (nint)DRM.Native.GetFB2(this.Device.DeviceGetFD(), width, height, (uint)format, handles, strides, offsets, 0);
                bo.SetUserData((nint)fb, new GBM.DestroyUserDataCallback(destroyUserDataCallbackFunc));
            }
            return (uint)fb;
        }

        

        Action<nint, nint> destroyUserDataCallbackFunc = (bo, data) => {

        };

        private void Release(gbm_bo *bo)
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

