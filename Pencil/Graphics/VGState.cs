using System;
using System.Collections.Generic;

namespace Pencil.Graphics
{
    public class VGState: ICloneable<VGState>
    {
        public VGCompositeOperationState CompositeOperationState { get; set; }
        public VGPaint Fill { get; set; } = new VGPaint();
        public VGPaint Stroke { get; set; } = new VGPaint();
        public float StrokeWidth { get; set; }
        public float MiterLimit { get; set; }
        public int LineJoin { get; set; }
        public int LineCap { get; set; }
        public float Alpha { get; set; }
        public VGScissor Scissor { get; set; } = new VGScissor();
        public float FontSize { get; set; }
        public float LetterSpacing { get; set; }
        public float LineHeight { get; set; }
        public float FontBlur { get; set; }
        public int TextAlign { get; set; }
        public int FontId { get; set; }

        public VGState Clone()
        {
			VGState newState = new VGState();
			newState.CompositeOperationState = this.CompositeOperationState;
			newState.Fill = this.Fill.Clone();
			newState.Stroke = this.Stroke.Clone();
			newState.StrokeWidth = this.StrokeWidth;
			newState.MiterLimit = this.MiterLimit;
			newState.LineJoin = this.LineJoin;
			newState.LineCap = this.LineCap;
			newState.Alpha = this.Alpha;

			newState.Scissor = this.Scissor.Clone();
			newState.FontSize = this.FontSize;
			newState.LetterSpacing = this.LetterSpacing;
			newState.LineHeight = this.LineHeight;
			newState.FontBlur = this.FontBlur;
			newState.TextAlign = this.TextAlign;
			newState.FontId = this.FontId;

			return newState;
        }
    }
}