using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace DRM
{
    [StructLayout(LayoutKind.Sequential)]
    unsafe internal struct drmResources
    {
        public int count_fbs;
        public uint* fbs;
        public int count_crtcs;
        public uint* crtcs;
        public int count_connectors;
        public uint* connectors;
        public int count_encoders;
        public uint* encoders;
        public uint min_width, max_width;
        public uint min_height, max_height;
    }
    
    unsafe public class Resources : IDisposable
    {
        #region pinvoke
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern drmResources* drmModeGetResources(int fd);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void drmModeFreeResources(drmResources* ptr);
        #endregion

        int gpu_fd;
        drmResources* handle;

        #region ctor
        public Resources(int fd_gpu)
        {
            gpu_fd = fd_gpu;
            handle = drmModeGetResources(fd_gpu);

            if (handle == null)
                throw new NotSupportedException("[DRI] drmModeGetResources failed.");
        }
        #endregion

        public uint MinWidth => handle->min_width;
        public uint MaxWidth => handle->max_width;
        public uint MinHeight => handle->min_height;
        public uint MaxHeight => handle->max_height;

        public Connector[] Connectors
        {
            get
            {
                Connector[] tmp = new Connector[handle->count_connectors];
                for (int i = 0; i < handle->count_connectors; i++)
                    tmp[i] = new Connector(gpu_fd, *(handle->connectors + i));
                return tmp;
            }
        }
        public Encoder[] Encoders
        {
            get
            {
                Encoder[] tmp = new Encoder[handle->count_encoders];
                for (int i = 0; i < handle->count_encoders; i++)
                    tmp[i] = new Encoder(gpu_fd, *(handle->encoders + i));
                return tmp;
            }
        }
        public Crtc[] Crtcs
        {
            get
            {
                Crtc[] tmp = new Crtc[handle->count_encoders];
                for (int i = 0; i < handle->count_crtcs; i++)
                    tmp[i] = new Crtc(gpu_fd, *(handle->crtcs + i));
                return tmp.Where(_ => _ is Crtc).ToArray();
            }
        }

        #region IDisposable implementation
        ~Resources()
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
                drmModeFreeResources(handle);
            handle = null;
        }
        #endregion

    }
}

