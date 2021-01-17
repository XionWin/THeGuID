using System;

namespace GBM
{
    [Flags]
    public enum TransferFlags : uint
    {
        /// <summary> Buffer contents read back (or accessed directly) at transfer create time.</summary>
        Read = 1 << 0,
        /// <summary> Buffer contents will be written back at unmap time (or modified as a result of being accessed directly).</summary>
        Write = 1 << 1,
        /// <summary>Read/modify/write</summary>
        ReadWrite = Read | Write,
    }
}