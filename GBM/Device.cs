using System;
using System.Runtime.InteropServices;

namespace GBM
{
    unsafe public class Device : IDisposable
    {
        #region pinvoke
        [DllImport(Lib.Name, EntryPoint = "gbm_create_device", CallingConvention = CallingConvention.Cdecl)]
        static extern nint CreateDevice(int fd);
        [DllImport(Lib.Name, EntryPoint = "gbm_device_destroy", CallingConvention = CallingConvention.Cdecl)]
        static extern void DestroyDevice(nint gbm);
        [DllImport(Lib.Name, EntryPoint = "gbm_device_get_fd", CallingConvention = CallingConvention.Cdecl)]
        static extern int DeviceGetFD(nint gbm);
        [DllImport(Lib.Name, EntryPoint = "gbm_device_is_format_supported", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsFormatSupported(nint gbm, SurfaceFormat format, SurfaceFlags usage);


        #endregion

        int fd_gpu;
        nint handler;

        public nint Handler => this.handler;

        #region ctor
        public Device(int _fd_gpu)
        {
            fd_gpu = _fd_gpu;
            handler = CreateDevice(fd_gpu);

            if (handler == IntPtr.Zero)
                throw new NotSupportedException("[GBM] device creation failed.");
        }
        #endregion

        public int DeviceGetFD() => DeviceGetFD(this.handler);

        #region IDisposable implementation
        ~Device()
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
                DestroyDevice(handler);
            handler = IntPtr.Zero;
        }
        #endregion



        public override string ToString()
        {
            return string.Format("[Device: Handler={0}, GPU FD={1}]", Handler, fd_gpu);
        }
    }
}

