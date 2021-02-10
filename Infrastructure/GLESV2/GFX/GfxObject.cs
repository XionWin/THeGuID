using System;
using System.Collections.Generic;
using Extension;

namespace GLESV2.GFX
{
    public abstract class GfxObject: IDisposable
    {
        public uint Id { get; init; }

        protected abstract void Release();

        public void Dispose() => this.Release();
    }

    static class GfxObjectExtension {
        public static T Check<T>(this T obj)
        where T: GfxObject
        {
            if(GL.glGetError() != 0) throw new GLESV2Exception();
            return obj;
        }
    }
}
