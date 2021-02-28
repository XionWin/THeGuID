using System;

namespace GLESV2.Def
{
    public enum BlendEquationMode : uint
    {
        FuncAdd = 0x8006,
        Min = 0x8007,
        Max = 0x8008,
        FuncSubtract = 0x800A,
        FuncReverseSubtract = 0x800B,
    }
    /*public enum BlendEquationModeExt : uint {
		LogicOp = 0x0BF1,
		FuncAddExt = 0x8006,
		MinExt = 0x8007,
		MaxExt = 0x8008,
		FuncSubtractExt = 0x800A,
		FuncReverseSubtractExt = 0x800B,
		AlphaMinSgix = 0x8320,
		AlphaMaxSgix = 0x8321,
	}*/
    public enum BlendingFactorSrc : uint
    {
        Zero = 0,
        SrcAlpha = 0x0302,
        OneMinusSrcAlpha = 0x0303,
        DstAlpha = 0x0304,
        OneMinusDstAlpha = 0x0305,
        DstColor = 0x0306,
        OneMinusDstColor = 0x0307,
        SrcAlphaSaturate = 0x0308,
        ConstantColor = 0x8001,
        ConstantColorExt = 0x8001,
        OneMinusConstantColor = 0x8002,
        OneMinusConstantColorExt = 0x8002,
        ConstantAlpha = 0x8003,
        ConstantAlphaExt = 0x8003,
        OneMinusConstantAlpha = 0x8004,
        OneMinusConstantAlphaExt = 0x8004,
        Src1Alpha = 0x8589,
        Src1Color = 0x88F9,
        OneMinusSrc1Color = 0x88FA,
        OneMinusSrc1Alpha = 0x88FB,
        One = 1,
    }
    public enum BlendingFactorDest : uint
    {
        Zero = 0,
        SrcColor = 0x0300,
        OneMinusSrcColor = 0x0301,
        SrcAlpha = 0x0302,
        OneMinusSrcAlpha = 0x0303,
        DstAlpha = 0x0304,
        OneMinusDstAlpha = 0x0305,
        ConstantColor = 0x8001,
        ConstantColorExt = 0x8001,
        OneMinusConstantColor = 0x8002,
        OneMinusConstantColorExt = 0x8002,
        ConstantAlpha = 0x8003,
        ConstantAlphaExt = 0x8003,
        OneMinusConstantAlpha = 0x8004,
        OneMinusConstantAlphaExt = 0x8004,
        Src1Alpha = 0x8589,
        Src1Color = 0x88F9,
        OneMinusSrc1Color = 0x88FA,
        OneMinusSrc1Alpha = 0x88FB,
        One = 1,
    }
}