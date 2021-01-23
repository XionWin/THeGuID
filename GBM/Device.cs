using System;
using System.Runtime.InteropServices;

namespace GBM
{
    public struct gbm_device
    {

    }

    unsafe public class Device : IDisposable
    {
        #region pinvoke
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        static extern gbm_device *gbm_create_device(int fd);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        static extern void gbm_device_destroy(gbm_device *devHandle);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        static extern int gbm_device_get_fd(gbm_device *devHandle);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool gbm_device_is_format_supported(gbm_device *devHandle, SurfaceFormat format, SurfaceFlags usage);
        
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        static extern nint gbm_device_get_backend_name(gbm_device *devHandle);

        #endregion

        int gpu;
        gbm_device *handle;

        public gbm_device *Handle => this.handle;

        #region ctor
        public Device(int gpu)
        {
            this.gpu = gpu;
            handle = gbm_create_device(this.gpu);
            if (handle == null)
                throw new NotSupportedException("[GBM] device creation failed.");
        }
        #endregion

        public int DeviceGetFD() => gbm_device_get_fd(this.handle);
        public string BackendName => Marshal.PtrToStringAuto(gbm_device_get_backend_name(this.handle));

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
            if (handle != null)
                gbm_device_destroy(handle);
            handle = null;
        }
        #endregion



        public override string ToString()
        {
            return string.Format("[Device: Handler={0}, GPU FD={1}]", (nint)handle, gpu);
        }
    }
}

