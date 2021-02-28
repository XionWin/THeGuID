using System;

namespace Pencil.Graphics
{
    public class VGPaint: ICloneable<VGPaint>
    {   
        public float[] XForm { get; set; } = new float[6];
        public float[] Extent { get; set; } = new float[2];
        public float Radius { get; set; }
        public float Feather { get; set; }
        public Colors.Color4 InnerColor { get; set; }
        public Colors.Color4 OuterColor { get; set; }
        public int Image { get; set; }

        public VGPaint Clone()
        {
            var newPaint = new VGPaint();
            Array.Copy(this.XForm, newPaint.XForm, this.XForm.Length);
            Array.Copy(this.Extent, newPaint.Extent, this.Extent.Length);
            return newPaint;
        }
    }
}