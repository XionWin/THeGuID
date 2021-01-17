using System;
using System.Runtime.InteropServices;

namespace DRM
{
    [StructLayout(LayoutKind.Sequential)]
    unsafe internal struct drmPlaneRes
    {
        public uint count_planes;
        public uint* planes;
    }

    unsafe public class PlaneResources : IDisposable
    {
        #region pinvoke
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        unsafe internal static extern drmPlaneRes* drmModeGetPlaneResources(int fd);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        unsafe internal static extern void drmModeFreePlaneResources(drmPlaneRes* ptr);
        #endregion

        int gpu_fd;
        drmPlaneRes* handle;

        internal PlaneResources(int fd_gpu)
        {
            gpu_fd = fd_gpu;
            handle = drmModeGetPlaneResources(fd_gpu);

            if (handle == null)
                throw new NotSupportedException("[DRI] drmModeGetPlaneResources failed.");
        }

        #region IDisposable implementation
        ~PlaneResources()
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
                    drmModeFreePlaneResources(handle);
                handle = null;
            }
        }
        #endregion
    }
}

