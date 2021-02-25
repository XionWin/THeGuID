using System;
using System.Collections.Generic;

namespace Pencil.Graphics
{
    public struct VGPath
    {   
        public int First { get; set; }
        public int Count { get; set; }
        public bool IsClosed { get; set; }
        public int BevelCount { get; set; }
        public IEnumerable<VGVertex> Fill { get; set; }
        public bool IsFill { get; set; }
        public IEnumerable<VGVertex> Stroke { get; set; }
        public bool IsStroke { get; set; }
        public int Winding { get; set; }
        public int Convex { get; set; }
    }
}