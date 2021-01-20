using System;
using System.Runtime.InteropServices;

namespace GBM
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void DestroyUserDataCallback(ref GBM.gbm_bo bo, ref uint data);

    [StructLayout(LayoutKind.Sequential)]
    public struct gbm_bo
    {
    }
    unsafe public class BufferObject : IDisposable
    {
        #region pinvoke
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern gbm_bo* gbm_bo_create(nint gbm, uint width, uint height, SurfaceFormat format, SurfaceFlags flags);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void gbm_bo_destroy(gbm_bo* bo);
        [DllImport(Lib.Name, EntryPoint = "gbm_bo_destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void destryBO(gbm_bo *bo);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int gbm_bo_write(gbm_bo *bo, nint buf, nint count);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern Device gbm_bo_get_device(gbm_bo *bo);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong gbm_bo_get_handle(gbm_bo *bo);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint gbm_bo_get_height(gbm_bo *bo);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint gbm_bo_get_width(gbm_bo *bo);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint gbm_bo_get_stride(gbm_bo *bo);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern SurfaceFormat gbm_bo_get_format(gbm_bo *bo);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong gbm_bo_get_modifier(gbm_bo *bo);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint gbm_bo_get_stride_for_plane(gbm_bo *bo, int plane);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint gbm_bo_get_offset(gbm_bo *bo, int plane);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void gbm_bo_set_user_data(gbm_bo *bo, ref uint data, DestroyUserDataCallback callback);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern nint gbm_bo_get_user_data(gbm_bo *bo);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern nint gbm_bo_map(gbm_bo *bo, uint x, uint y, uint width, uint height, TransferFlags flags, ref uint stride, out nint data);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void gbm_bo_unmap(gbm_bo *bo, nint data);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int gbm_bo_get_plane_count(gbm_bo *bo);
        #endregion

        internal gbm_bo *handle;

        #region ctor
        public BufferObject(gbm_bo* _handle)
        {
            handle = _handle;
        }
        public BufferObject(Device dev, uint _width, uint _height, SurfaceFormat format, SurfaceFlags flags)
        {
            handle = gbm_bo_create(dev.Handler, _width, _height, format, flags);
            if (handle == null)
                throw new NotSupportedException("[GBM] BO creation failed.");
        }
        #endregion


        public uint Handle => (uint)BufferObject.gbm_bo_get_handle(this.handle);
        public uint Width => BufferObject.gbm_bo_get_width(this.handle);
        public uint Height => BufferObject.gbm_bo_get_height(this.handle);
        public uint Stride => BufferObject.gbm_bo_get_stride(this.handle);
        public SurfaceFormat Format => BufferObject.gbm_bo_get_format(this.handle);
        public nint UserData => BufferObject.gbm_bo_get_user_data(this.handle);

        public int PanelCount => BufferObject.gbm_bo_get_plane_count(this.handle);
        public uint PanelStride(int panel) => BufferObject.gbm_bo_get_stride_for_plane(this.handle, panel);
        public uint PanelOffset(int panel) => BufferObject.gbm_bo_get_offset(this.handle, panel);
        public ulong Modifier => BufferObject.gbm_bo_get_modifier(this.handle);
        public void SetUserData(ref uint data, DestroyUserDataCallback destroyFB) => 
            BufferObject.gbm_bo_set_user_data(this.handle, ref data, destroyFB);

        public byte[] Data
        {
            set
            {
                fixed (byte* pdata = value)
                {
                    gbm_bo_write(handle, (nint)pdata, value.Length);
                }
            }
        }

        #region IDisposable implementation
        ~BufferObject()
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
            if (handle != null)
                gbm_bo_destroy(handle);
            handle = null;
        }
        #endregion
    }
}

