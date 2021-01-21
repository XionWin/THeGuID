using System;
using System.Runtime.InteropServices;

namespace GBM
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void DestroyUserDataCallback(nint bo, ref uint data);


[StructLayout(LayoutKind.Explicit)]
struct gbm_bo_handle
{
    [FieldOffset(0)]
    public nint ptr;

    [FieldOffset(0)]
    public int s32;

    [FieldOffset(0)]
    public uint u32;

    [FieldOffset(0)]
    public long s64;

    [FieldOffset(0)]
    public ulong u64;
}


    unsafe public class BufferObject : IDisposable
    {
        #region pinvoke
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern nint gbm_bo_create(nint gbm, uint width, uint height, SurfaceFormat format, SurfaceFlags flags);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void gbm_bo_destroy(nint bo);
        [DllImport(Lib.Name, EntryPoint = "gbm_bo_destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void destryBO(nint bo);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int gbm_bo_write(nint bo, nint buf, nint count);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern nint gbm_bo_get_device(nint bo);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern gbm_bo_handle gbm_bo_get_handle_for_plane(nint bo, int plane);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern gbm_bo_handle gbm_bo_get_handle(nint bo);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint gbm_bo_get_height(nint bo);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint gbm_bo_get_width(nint bo);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint gbm_bo_get_stride(nint bo);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern SurfaceFormat gbm_bo_get_format(nint bo);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong gbm_bo_get_modifier(nint bo);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint gbm_bo_get_stride_for_plane(nint bo, int plane);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint gbm_bo_get_offset(nint bo, int plane);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void gbm_bo_set_user_data(nint bo, ref uint data, DestroyUserDataCallback callback);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern nint gbm_bo_get_user_data(nint bo);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern nint gbm_bo_map(nint bo, uint x, uint y, uint width, uint height, TransferFlags flags, ref uint stride, out nint data);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void gbm_bo_unmap(nint bo, nint data);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int gbm_bo_get_plane_count(nint bo);
        #endregion

        internal nint boHandle;

        #region ctor
        public BufferObject(nint handle)
        {
            boHandle = handle;
        }
        public BufferObject(Device dev, uint _width, uint _height, SurfaceFormat format, SurfaceFlags flags)
        {
            boHandle = gbm_bo_create(dev.Handler, _width, _height, format, flags);
            if (boHandle == IntPtr.Zero)
                throw new NotSupportedException("[GBM] BO creation failed.");
        }
        #endregion


        public uint Handle => BufferObject.gbm_bo_get_handle(this.boHandle).u32;
        public uint PanelHandle(int panel) => BufferObject.gbm_bo_get_handle_for_plane(this.boHandle, panel).u32;

        public nint Device => BufferObject.gbm_bo_get_device(this.boHandle);
        public uint Width => BufferObject.gbm_bo_get_width(this.boHandle);
        public uint Height => BufferObject.gbm_bo_get_height(this.boHandle);
        public uint Stride => BufferObject.gbm_bo_get_stride(this.boHandle);
        public SurfaceFormat Format => BufferObject.gbm_bo_get_format(this.boHandle);
        public nint UserData => BufferObject.gbm_bo_get_user_data(this.boHandle);

        public int PanelCount => BufferObject.gbm_bo_get_plane_count(this.boHandle);
        public uint PanelStride(int panel) => BufferObject.gbm_bo_get_stride_for_plane(this.boHandle, panel);
        public uint PanelOffset(int panel) => BufferObject.gbm_bo_get_offset(this.boHandle, panel);
        public ulong Modifier => BufferObject.gbm_bo_get_modifier(this.boHandle);
        public void SetUserData(ref uint data, DestroyUserDataCallback destroyFB) =>
            BufferObject.gbm_bo_set_user_data(this.boHandle, ref data, destroyFB);

        public byte[] Data
        {
            set
            {
                fixed (byte* pdata = value)
                {
                    gbm_bo_write(boHandle, (nint)pdata, value.Length);
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
            if (boHandle != IntPtr.Zero)
                gbm_bo_destroy(boHandle);
            boHandle = 0;
        }
        #endregion
    }
}

