﻿using System;
using System.Runtime.InteropServices;

namespace GLESV2
{
    public partial class GL
    {
        [DllImport(Lib.Name, EntryPoint = "glGetString")]
        private static extern nint glGetString(uint name);
        public static string GetString(uint name) => Marshal.PtrToStringAuto(glGetString(name));
    }
}