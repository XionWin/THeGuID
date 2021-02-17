using System;

namespace Graphic.Drawing
{
    public class Path
    {
        public Path()
        {
        }
        
        public int First { get; set; }
        public int Count { get; set; }
        public bool IsClosed { get; set; }
        public int BevelCount { get; set; }

        //Fill

        public bool IsFill { get; set; }
        public int FillCount { get; set; }

        //Stroke
        public bool IsStroke { get; set; }
        public int StrokeCount { get; set; }

        public int Winding { get; set; }

        public int Convex { get; set; }
    }
}