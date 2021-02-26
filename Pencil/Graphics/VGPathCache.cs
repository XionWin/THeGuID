using System;
using System.Collections.Generic;

namespace Pencil.Graphics
{
    public class VGPathCache
    {
        public IEnumerable<float> Bounds { get; private set; }
        public IEnumerable<VGPoint> Points { get; set; }
        public IEnumerable<VGPath> Paths { get; set; }
        public IEnumerable<VGVertex> Vertexs { get; set; }

        public VGPathCache()
        {
            this.Bounds = new float[4];
            this.Init();
        }

        private void Init()
        {
            this.Points = new List<VGPoint>();
            this.Paths = new List<VGPath>();
            this.Vertexs = new List<VGVertex>();
        }

        public void Clean() => this.Init();
    }
}