using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace DRM
{
    #region DRM callback signatures
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void VBlankCallback(int fd, int sequence, int tv_sec, int tv_usec, nint user_data);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void PageFlipCallback(int fd, int sequence, int tv_sec, int tv_usec, ref int user_data);
    #endregion

    #region DRM enums

    [Flags]
    public enum PageFlipFlags : uint
    {
        FlipEvent = 0x01,
        FlipAsync = 0x02,
        FlipFlags = FlipEvent | FlipAsync
    }
    /// <summary>Video mode flags, bit compatible with the xorg definitions. </summary>
    [Flags]
    public enum VideoMode
    {
        PHSYNC = 0x01,
        NHSYNC = 0x02,
        PVSYNC = 0x04,
        NVSYNC = 0x08,
        INTERLACE = 0x10,
        DBLSCAN = 0x20,
        CSYNC = 0x40,
        PCSYNC = 0x80,
        NCSYNC = 0x10,
        HSKEW = 0x0200,
        BCAST = 0x0400,
        PIXMUX = 0x0800,
        DBLCLK = 0x1000,
        CLKDIV2 = 0x2000,
        //		FLAG_3D_MASK			(0x1f<<14)
        //		FLAG_3D_NONE = 0x0;
        //		FLAG_3D_FRAME_PACKING = 0x4000,
        //		FLAG_3D_FIELD_ALTERNATIVE = 0x8000,
        //		FLAG_3D_LINE_ALTERNATIVE	(3<<14)
        //		FLAG_3D_SIDE_BY_SIDE_FULL	(4<<14)
        //		FLAG_3D_L_DEPTH		(5<<14)
        //		FLAG_3D_L_DEPTH_GFX_GFX_DEPTH	(6<<14)
        //		FLAG_3D_TOP_AND_BOTTOM	(7<<14)
        //		FLAG_3D_SIDE_BY_SIDE_HALF	(8<<14)
    }
    #endregion

    #region DRM structs
    [StructLayout(LayoutKind.Sequential)]
    public struct EventContext
    {
        public int version;
        public nint vblank_handler;
        public nint page_flip_handler;
        public static readonly int Version = 2;
    }
    [StructLayout(LayoutKind.Sequential)]
    unsafe public struct drmFrameBuffer
    {
        public uint fb_id;
        public uint width, height;
        public uint pitch;
        public uint bpp;
        public uint depth;
        /* driver specific handle */
        public uint handle;
    }
    [StructLayout(LayoutKind.Sequential)]
    struct drmClip
    {
        public ushort x1;
        public ushort y1;
        public ushort x2;
        public ushort y2;
    }
    #endregion

    unsafe public class Native
    {
        #region PINVOKE
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int drmHandleEvent(int fd, ref EventContext evctx);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int drmModeAddFB(int fd, uint width, uint height, byte depth, byte bpp,
            uint stride, uint bo_handle, out uint buf_id);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int drmModeRmFB(int fd, uint bufferId);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int drmModeDirtyFB(int fd, uint bufferId, nint clips, uint num_clips);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        unsafe internal static extern drmFrameBuffer* drmModeGetFB(int fd, uint fb_id);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        unsafe internal static extern void drmModeFreeFB(drmFrameBuffer* ptr);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        static extern int drmModePageFlip(int fd, uint crtc_id, uint fb_id, PageFlipFlags flags, ref int user_data);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        unsafe static extern int drmModeSetCrtc(int fd, uint crtcId, uint bufferId, uint x, uint y, [MarshalAs(UnmanagedType.LPArray)] uint[] connectors, int count, ref ModeInfo mode);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int drmModeSetCursor2(int fd, uint crtcId, uint bo_handle, uint width, uint height, int hot_x, int hot_y);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int drmModeMoveCursor(int fd, uint crtcId, uint x, uint y);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int drmModeAddFB2(int fd, uint width, uint height,
			 uint pixel_format, uint[] bo_handles,
			 uint[] pitches, uint[] offsets,
			 uint *buf_id, uint flags);
        #endregion

        public static uint GetFB2(int fd, uint width, uint height,
			uint pixel_format, uint[] bo_handles,
			uint[] pitches, uint[] offsets, uint flags)
        {
            uint bufId = 0;
            if(drmModeAddFB2(fd, width, height, pixel_format, bo_handles, pitches, offsets, &bufId, flags) is var result && result == 0)
            {
                return bufId;
            }
            throw new NotSupportedException("[DRM] drmModeAddFB2 failed.");
        }
    
    
        public static bool SetCrtc
        (int fd, uint crtcId, uint bufferId, uint x, uint y, IEnumerable<uint> connectors, ModeInfo mode)
        {
            return drmModeSetCrtc(fd, crtcId, bufferId, x, y, connectors.ToArray(), connectors.Count(), ref mode)  == 0;
        }
        

    }
}