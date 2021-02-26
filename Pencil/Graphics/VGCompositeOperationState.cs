using System;
using System.Collections.Generic;

namespace Pencil.Graphics
{
    public struct VGCompositeOperationState
    {
        public int Src { get; set; }
        public int Dst { get; set; }
        public int SrcAlpha { get; set; }
        public int DstAlpha { get; set; }
    }
}