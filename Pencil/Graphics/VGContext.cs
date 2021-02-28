using System;
using System.Collections.Generic;
using System.Linq;

namespace Pencil.Graphics
{
    public struct VGContext
    {
        public IEnumerable<float> Commands { get; set; }
        public float CommandX { get; set; }
        public float CommandY { get; set; }
        public IEnumerable<VGState> States { get; set; }
        public VGPathCache Cache { get; set; }  
        public float TessTol { get; set; }
        public float DistTol { get; set; }
        public float FringeWidth { get; set; }
        public float DevicePixelRatio { get; set; } 
        public int FillTriangleCount { get; set; }
        public int StrokeTriangleCount { get; set; }
        public int TextTriangleCount { get; set; }

        public void SetPixelRatio(float ratio)
        {
            this.TessTol = .25f / ratio;
            this.DistTol = .01f /ratio;
            this.FringeWidth = 1f / ratio;
            this.DevicePixelRatio = ratio;
        }

        public VGState GetState() => this.States.Last();
        
    }
}