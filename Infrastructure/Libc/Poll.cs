
using System;
using System.Runtime.InteropServices;

namespace Libc
{
    public partial class Context
    {
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
        public static extern int poll(ref PollFD fd, nint fd_count, int timeout);

        public static int poll(ref PollFD fd, int fd_count, int timeout)
        {
            return poll(ref fd, fd_count, timeout);
        }
    }

    [Flags]
    public enum PollFlags : short
    {
        In = 0x01,
        Pri = 0x02,
        Out = 0x04,
        Error = 0x08,
        Hup = 0x10,
        Invalid = 0x20,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PollFD
    {
        public int fd;
        public PollFlags events;
        public PollFlags revents;
    }
}

