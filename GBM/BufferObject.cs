using System;
using System.Runtime.InteropServices;

namespace GBM
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void DestroyUserDataCallback(ref GBM.gbm_bo bo, ref uint data);

    [StructLayout(LayoutKind.Sequential)]
    public struct gbm_bo
    {
        nint device;
        public uint Width, Height;
        public uint Stride;
        public SurfaceFormat Format;

        public uint Handler
        {
            get { return (uint)BufferObject.gbm_bo_get_handle(ref this); }

        public nint UserData => BufferObject.gbm_bo_get_user_data(ref this);

        public int PanelCount => BufferObject.gbm_bo_get_plane_count(ref this);
        public void SetUserData(ref uint data, DestroyUserDataCallback destroyFB)
        {
            BufferObject.gbm_bo_set_user_data(ref this, ref data, destroyFB);
        }

    }
    unsafe public class BufferObject : IDisposable
    {
        #region pinvoke
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern gbm_bo* gbm_bo_create(nint gbm, uint width, uint height, SurfaceFormat format, SurfaceFlags flags);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void gbm_bo_destroy(gbm_bo* bo);
        [DllImport(Lib.Name, EntryPoint = "gbm_bo_destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void destryBO(ref gbm_bo bo);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int gbm_bo_write(gbm_bo* bo, nint buf, nint count);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern Device gbm_bo_get_device(ref gbm_bo bo);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong gbm_bo_get_handle(ref gbm_bo bo);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint gbm_bo_get_height(ref gbm_bo bo);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint gbm_bo_get_width(ref gbm_bo bo);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint gbm_bo_get_stride(ref gbm_bo bo);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void gbm_bo_set_user_data(ref gbm_bo bo, ref uint data, DestroyUserDataCallback callback);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern nint gbm_bo_get_user_data(ref gbm_bo bo);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern nint gbm_bo_map(ref gbm_bo bo, uint x, uint y, uint width, uint height, TransferFlags flags, ref uint stride, out nint data);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void gbm_bo_unmap(ref gbm_bo bo, nint data);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int gbm_bo_get_plane_count(ref gbm_bo bo);
        #endregion

        internal gbm_bo *handler;

        #region ctor
        public BufferObject(gbm_bo* _handle)
        {
            handler = _handle;
        }
        public BufferObject(Device dev, uint _width, uint _height, SurfaceFormat format, SurfaceFlags flags)
        {
            var handler = gbm_bo_create(dev.Handler, _width, _height, format, flags);
            if (handler == null)
                throw new NotSupportedException("[GBM] BO creation failed.");
        }
        #endregion


        public uint Stride { get { return handler->Stride; } }
        public byte[] Data
        {
            set
            {
                fixed (byte* pdata = value)
                {
                    gbm_bo_write(handler, (nint)pdata, value.Length);
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
            if (handler != null)
                gbm_bo_destroy(handler);
            handler = null;
        }
        #endregion
    }
}

