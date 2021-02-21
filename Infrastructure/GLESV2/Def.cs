using System;

namespace GLESV2.Def
{
    [Flags]
	public enum ClearBufferMask : uint {
		DepthBufferBit = 0x00000100,
		AccumBufferBit = 0x00000200,
		StencilBufferBit = 0x00000400,
		ColorBufferBit = 0x00004000,
		CoverageBufferBitNv = 0x00008000,
	}

	public enum BufferTarget : uint {
		ArrayBuffer = 0x8892,
		ElementArrayBuffer = 0x8893,
		PixelPackBuffer = 0x88EB,
		PixelUnpackBuffer = 0x88EC,
		UniformBuffer = 0x8A11,
		TextureBuffer = 0x8C2A,
		TransformFeedbackBuffer = 0x8C8E,
		CopyReadBuffer = 0x8F36,
		CopyWriteBuffer = 0x8F37,
		DrawIndirectBuffer = 0x8F3F,
        // Manual added
        AtomicCounterBuffer = 0x92C0,
        DispatchIndirectBuffer = 0x90EE,
        ShaderStorageBuffer = 0x90D2,
	}
}