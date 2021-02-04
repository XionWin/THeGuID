using System;
using System.Runtime.InteropServices;

namespace DRM
{
    public enum PlaneType
    {
        Overlay = 0,
        Primary = 1,
        Cursor = 2
    }
    [StructLayout(LayoutKind.Sequential)]
    unsafe public struct drmPlane
    {
        public uint count_formats;
        public uint* formats;
        public uint plane_id;

        public uint crtc_id;
        public uint fb_id;

        public uint crtc_x, crtc_y;
        public uint x, y;

        public uint possible_crtcs;
        public uint gamma_size;
    }

    unsafe public class Plane : IDisposable
    {
        #region pinvoke
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        unsafe internal static extern drmPlane* drmModeGetPlane(int fd, uint id);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        unsafe internal static extern void drmModeFreePlane(drmPlane* ptr);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        unsafe static extern int drmModeSetPlane(int fd, uint plane_id, uint crtc_id,
            uint fb_id, uint flags,
            int crtc_x, int crtc_y,
            uint crtc_w, uint crtc_h,
            uint src_x, uint src_y,
            uint src_w, uint src_h);

        #endregion

        drmPlane* handle;

        internal Plane(drmPlane* _handle)
        {
            handle = _handle;
        }

        public uint Id { get { return handle->plane_id; } }

        #region IDisposable implementation
        ~Plane()
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
            unsafe
            {
                if (handle != null)
                    drmModeFreePlane(handle);
                handle = null;
            }
        }
        #endregion
    }
}

