using System;
using System.Runtime.InteropServices;

namespace GLESV2
{
    public partial class GL
    {
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        private static extern nint glGetString(uint name);
        public static string GetString(GLD definition) => Marshal.PtrToStringAuto(glGetString((uint)definition));

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glClearColor(float red, float green, float blue, float alpha);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glViewport(int x, int y, int width, int height);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glClear(GLD mask);

    }
}
