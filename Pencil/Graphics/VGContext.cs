using System;
using System.Collections.Generic;

namespace Pencil.Graphics
{
    public struct CGContext
    {
        public IEnumerable<float> Commands { get; set; }
        public float CommandX { get; set; }
        public float CommandY { get; set; }
        
    }
}