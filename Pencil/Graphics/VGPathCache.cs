using System.Collections.Generic;

namespace Pencil.Graphics
{
    public class VGPathCache
    {
        public IEnumerable<VGPoint> Points { get; set; }
        public IEnumerable<VGPath> Paths { get; set; }
        public IEnumerable<VGVertex> Vertexs { get; set; }
        public IEnumerable<float> Bounds { get; private set; }

        public VGPathCache()
        {
            this.Bounds = new float[4];
        }
    }
}