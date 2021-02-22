using System;

namespace GLESV2.Def
{
	public enum ProgramParameter : uint {
		ProgramBinaryRetrievableHint = 0x8257,
		ProgramSeparable = 0x8258,
		ProgramBinaryLength = 0x8741,
		GeometryShaderInvocations = 0x887F,
		ActiveUniformBlockMaxNameLength = 0x8A35,
		ActiveUniformBlocks = 0x8A36,
		DeleteStatus = 0x8B80,
		LinkStatus = 0x8B82,
		ValidateStatus = 0x8B83,
		InfoLogLength = 0x8B84,
		AttachedShaders = 0x8B85,
		ActiveUniforms = 0x8B86,
		ActiveUniformMaxLength = 0x8B87,
		ActiveAttributes = 0x8B89,
		ActiveAttributeMaxLength = 0x8B8A,
		TransformFeedbackVaryingMaxLength = 0x8C76,
		TransformFeedbackBufferMode = 0x8C7F,
		TransformFeedbackVaryings = 0x8C83,
		GeometryVerticesOut = 0x8DDA,
		GeometryInputType = 0x8DDB,
		GeometryOutputType = 0x8DDC,
		TessControlOutputVertices = 0x8E75,
		TessGenMode = 0x8E76,
		TessGenSpacing = 0x8E77,
		TessGenVertexOrder = 0x8E78,
		TessGenPointMode = 0x8E79,
	}
	
	public enum ProgramPipelineParameter : uint {
		ActiveProgram = 0x8259,
	}
	[Flags]
	public enum ProgramStageMask : uint {
		VertexShaderBit = 0x00000001,
		FragmentShaderBit = 0x00000002,
		GeometryShaderBit = 0x00000004,
		TessControlShaderBit = 0x00000008,
		TessEvaluationShaderBit = 0x00000010,
		AllShaderBits = unchecked(0xFFFFFFFF),
        // Manual added
        ComputeShaderBit = 0x00000020,
	}
	
	public enum ProgramStageParameter : uint {
		ActiveSubroutines = 0x8DE5,
		ActiveSubroutineUniforms = 0x8DE6,
		ActiveSubroutineUniformLocations = 0x8E47,
		ActiveSubroutineMaxLength = 0x8E48,
		ActiveSubroutineUniformMaxLength = 0x8E49,
	}
}