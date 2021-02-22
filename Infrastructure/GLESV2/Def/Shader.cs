	
using System;

namespace GLESV2.Def
{
    public enum ShaderType : uint {
		FragmentShader = 0x8B30,
		VertexShader = 0x8B31,
		GeometryShader = 0x8DD9,
		GeometryShaderExt = 0x8DD9,
		TessEvaluationShader = 0x8E87,
		TessControlShader = 0x8E88,
        // Manual added
        ComputeShader = 0x91B9,
	}
	public enum ShaderParameter : uint {
		ShaderType = 0x8B4F,
		DeleteStatus = 0x8B80,
		CompileStatus = 0x8B81,
		InfoLogLength = 0x8B84,
		ShaderSourceLength = 0x8B88,
	}
	
	public enum ShaderPrecisionType : uint {
		LowFloat = 0x8DF0,
		MediumFloat = 0x8DF1,
		HighFloat = 0x8DF2,
		LowInt = 0x8DF3,
		MediumInt = 0x8DF4,
		HighInt = 0x8DF5,
	}

	public enum ShadingModel : uint {
		Flat = 0x1D00,
		Smooth = 0x1D01,
	}
}