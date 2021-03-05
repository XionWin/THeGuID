using System;
using System.Collections.Generic;

namespace Pencil.Graphics
{
    public class VGPathCache
    {
        public IList<float> Bounds { get; private set; }
        public IList<VGPoint> Points { get; private set; }
        public IList<VGPath> Paths { get; private set; }
        public IList<VGVertex> Vertexs { get; private set; }

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