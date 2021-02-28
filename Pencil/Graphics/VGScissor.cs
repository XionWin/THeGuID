using System;

namespace Pencil.Graphics
{
    public class VGScissor: ICloneable<VGScissor>
    {
        public float[] XForm { get; private set; } = new float[6];
        public float[] Extent { get; private set; } = new float[2];

        public VGScissor Clone()
        {
            var newScissor = new VGScissor();
            Array.Copy(this.XForm, newScissor.XForm, this.XForm.Length);
            Array.Copy(this.Extent, newScissor.Extent, this.Extent.Length);
            return newScissor;
        }
    }
}