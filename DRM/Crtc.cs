using System;
using System.Runtime.InteropServices;

namespace DRM
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct drmCrtc
    {
        public uint crtc_id;
        public uint buffer_id;

        public uint x, y;
        public uint width, height;
        public int mode_valid;
        public ModeInfo mode;

        public int gamma_size;
    }

    unsafe public class Crtc : IDisposable
    {
        #region pinvoke
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern drmCrtc* drmModeGetCrtc(int fd, uint crtcId);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void drmModeFreeCrtc(drmCrtc* ptr);
        #endregion

        int fd_gpu;
        internal drmCrtc* handle;

        #region ctor
        internal Crtc(int _fd_gpu, uint _id)
        {
            fd_gpu = _fd_gpu;
            handle = drmModeGetCrtc(fd_gpu, _id);

            if (handle == null)
                throw new NotSupportedException("[DRI] drmModeGetCrtc failed.");
        }
        #endregion

        public uint Id { get { return handle->crtc_id; } }
        public ModeInfo CurrentMode { get { return handle->mode; } }
        public uint CurrentFbId { get { return handle->buffer_id; } }
        public bool ModeIsValid { get { return handle->mode_valid == 0 ? false : true; } }
        public uint X { get { return handle->x; } }
        public uint Y { get { return handle->x; } }
        public uint Height { get { return handle->height; } }
        public uint Width { get { return handle->width; } }
        public int GammaSize { get { return handle->gamma_size; } }

        #region IDisposable implementation
        ~Crtc()
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
                    drmModeFreeCrtc(handle);
                handle = null;
            }
        }
        #endregion

        public override string ToString()
        {
            return string.Format("[Crtc: Id={0}, CurrentMode={1}, CurrentFbId={2}, ModeIsValid={3}, X={4}, Y={5}, Width={7}, Height={6}, GammaSize={8}]", Id, CurrentMode, CurrentFbId, ModeIsValid, X, Y, Width, Height, GammaSize);
        }
    }
}

