using System;

namespace Graphic.Core
{
    public static class Manager
    {
        public string DeviceName { get; set; }
        public RenderType RenderType { get; set; }
        public bool VerticalSynchronization { get; set; }
    }
}
