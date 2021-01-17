using System;
using System.Runtime.InteropServices;

namespace DRM
{
    public class Drm
    {
        public Plane Plane { get; set; }
        
        public Crtc Crtc { get; set; }
        
        public Connector Connector { get; set; }

        public override string ToString()
        {
            return string.Format("[Drm: Plane={0}\n Crtc={1}\n Connector={2}]", Plane, Crtc, Connector);
        }
    }
}

