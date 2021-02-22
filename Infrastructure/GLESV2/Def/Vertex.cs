using System;

namespace GLESV2.Def
{

	public enum VertexAttribDPointerType : uint {
		Double = 0x140A,
	}
	public enum VertexAttribIPointerType : uint {
		Byte = 0x1400,
		UnsignedByte = 0x1401,
		Short = 0x1402,
		UnsignedShort = 0x1403,
		Int = 0x1404,
		UnsignedInt = 0x1405,
	}
	public enum VertexAttribParameter : uint {
		ArrayEnabled = 0x8622,
		ArraySize = 0x8623,
		ArrayStride = 0x8624,
		ArrayType = 0x8625,
		CurrentVertexAttrib = 0x8626,
		ArrayNormalized = 0x886A,
		VertexAttribArrayInteger = 0x88FD,
		VertexAttribArrayDivisor = 0x88FE,
	}
	public enum VertexAttribPointerParameter : uint {
		ArrayPointer = 0x8645,
	}
	public enum VertexAttribPointerType : uint {
		Byte = 0x1400,
		UnsignedByte = 0x1401,
		Short = 0x1402,
		UnsignedShort = 0x1403,
		Int = 0x1404,
		UnsignedInt = 0x1405,
		Float = 0x1406,
		Double = 0x140A,
		HalfFloat = 0x140B,
		Fixed = 0x140C,
		UnsignedInt2101010Rev = 0x8368,
		Int2101010Rev = 0x8D9F,
	}
	public enum VertexPointerType : uint {
		Short = 0x1402,
		Int = 0x1404,
		Float = 0x1406,
		Double = 0x140A,
		HalfFloat = 0x140B,
		UnsignedInt2101010Rev = 0x8368,
		Int2101010Rev = 0x8D9F,
	}
}