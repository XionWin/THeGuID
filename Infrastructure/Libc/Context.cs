using System;
using System.Runtime.InteropServices;
using System.Text;

#pragma warning disable 0649 // field is never assigned

namespace Libc
{
    unsafe public partial class Context
    {
        [DllImport(Lib.Name)]
        public static extern int dup(int file);

        [DllImport(Lib.Name)]
        public static extern int dup2(int file1, int file2);

        [DllImport(Lib.Name)]
        public static extern int ioctl(int d, uint request, [Out] nint data);

        [DllImport(Lib.Name)]
        public static extern int open(byte *pathname, OpenFlags flags);

        [DllImport(Lib.Name)]
        public static extern int open([MarshalAs(UnmanagedType.LPStr)]string pathname, OpenFlags flags);

        [DllImport(Lib.Name)]
        public static extern int open(nint pathname, OpenFlags flags);

		[DllImport(Lib.Name)]
		public static extern int posix_openpt (OpenFlags flags);

        [DllImport(Lib.Name)]
        public static extern int close(int fd);

        [DllImport(Lib.Name)]
        unsafe public static extern nint read(int fd, void* buffer, nuint count);
		[DllImport(Lib.Name)]
		unsafe public static extern uint write(int fd, void *buffer, int count); 

		public static void write (int fd, byte[] b){
			unsafe {
				fixed (byte* pb = b) {
					write (fd, pb, b.Length); 
				}
			}
		}

        public static nint read(int fd, out byte b)
        {
            unsafe
            {
                fixed (byte* pb = &b)
                {
                    return read(fd, pb, 1);
                }
            }
        }

        public static nint read(int fd, out short s)
        {
            unsafe
            {
                fixed (short* ps = &s)
                {
                    return read(fd, ps, 2);
                }
            }
        }

        [DllImport(Lib.Name)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool isatty(int fd);

		[DllImport(Lib.Name)]
		public static extern int getgid ();
		[DllImport(Lib.Name)]
		public static extern int setpgid (int pid, int pgid);
		[DllImport(Lib.Name)]
		public static extern int getpgid (int pid);

		//public static extern int setpgrp (int pgid);
		[DllImport(Lib.Name)]
		public static extern int getpgrp ();
    }

    enum ErrorNumber
    {
        Interrupted = 4,
        Again = 11,
        InvalidValue = 22,
    }

    [Flags]
    enum DirectionFlags
    {
        None = 0,
        Write = 1,
        Read = 2
    }

    [Flags]
    public enum OpenFlags
    {
        ReadOnly = 0x0000,
        WriteOnly = 0x0001,
        ReadWrite = 0x0002,
        NonBlock = 0x0800,
        CloseOnExec = 0x0080000
    }

    [StructLayout(LayoutKind.Sequential)]
    struct Stat
    {
        public nint dev;     /* ID of device containing file */
        public nint ino;     /* inode number */
        public nint mode;    /* protection */
        public nint nlink;   /* number of hard links */
        public nint uid;     /* user ID of owner */
        public nint gid;     /* group ID of owner */
        public nint rdev;    /* device ID (if special file) */
        public nint size;    /* total size, in bytes */
        public nint blksize; /* blocksize for file system I/O */
        public nint blocks;  /* number of 512B blocks allocated */
        public nint atime;   /* time of last access */
        public nint mtime;   /* time of last modification */
        public nint ctime;   /* time of last status change */
    }

    struct EvdevInputId
    {
        public ushort BusType;
        public ushort Vendor;
        public ushort Product;
        public ushort Version;
    }

}
