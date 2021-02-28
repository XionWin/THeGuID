using System;

namespace GLESV2.Def
{
	public enum StringName : uint {
		Vendor = 0x1F00,
		Renderer = 0x1F01,
		Version = 0x1F02,
		Extensions = 0x1F03,
		ShadingLanguageVersion = 0x8B8C,
	}
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


	public enum BeginFeedbackMode : uint {
		Points = 0x0000,
		Lines = 0x0001,
		Triangles = 0x0004,
	}
	public enum BeginMode : uint {
		Points = 0x0000,
		Lines = 0x0001,
		LineLoop = 0x0002,
		LineStrip = 0x0003,
		Triangles = 0x0004,
		TriangleStrip = 0x0005,
		TriangleFan = 0x0006,
		Quads = 0x0007,
		QuadStrip = 0x0008,
		Polygon = 0x0009,
		Patches = 0x000E,
		LinesAdjacency = 0xA,
		LineStripAdjacency = 0xB,
		TrianglesAdjacency = 0xC,
		TriangleStripAdjacency = 0xD,
	}

	public enum BufferUsageHint : uint {
		StreamDraw = 0x88E0,
		StreamRead = 0x88E1,
		StreamCopy = 0x88E2,
		StaticDraw = 0x88E4,
		StaticRead = 0x88E5,
		StaticCopy = 0x88E6,
		DynamicDraw = 0x88E8,
		DynamicRead = 0x88E9,
		DynamicCopy = 0x88EA,
	}
}