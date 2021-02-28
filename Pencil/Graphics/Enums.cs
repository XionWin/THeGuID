using System;

namespace Pencil.Graphics
{
    public enum VGcommands
	{
		MOVETO = 0,
		LINETO = 1,
		BEZIERTO = 2,
		CLOSE = 3,
		WINDING = 4,
	}

	public enum VGpointFlags
	{
		PT_CORNER = 0x01,
		PT_LEFT = 0x02,
		PT_BEVEL = 0x04,
		PR_INNERBEVEL = 0x08,
	}

	public enum VGimageFlags
	{
		// Generate mipmaps during creation of the image.
		IMAGE_GENERATE_MIPMAPS	= 1 << 0,
		// Repeat image in X direction.
		IMAGE_REPEATX = 1 << 1,
		// Repeat image in Y direction.
		IMAGE_REPEATY = 1 << 2,
		// Flips (inverses) image in Y direction when rendered.
		IMAGE_FLIPY = 1 << 3,
		// Image data has premultiplied alpha.
		IMAGE_PREMULTIPLIED = 1 << 4,
	}

	public enum VGtexture
	{
		TEXTURE_ALPHA = 0x01,
		TEXTURE_RGBA = 0x02,
	}

	public enum VGcompositeOperation
	{
		SOURCE_OVER,
		SOURCE_IN,
		SOURCE_OUT,
		ATOP,
		DESTINATION_OVER,
		DESTINATION_IN,
		DESTINATION_OUT,
		DESTINATION_ATOP,
		LIGHTER,
		COPY,
		XOR,
	}

	public enum VGblendFactor
	{
		ZERO = 1 << 0,
		ONE = 1 << 1,
		SRC_COLOR = 1 << 2,
		ONE_MINUS_SRC_COLOR = 1 << 3,
		DST_COLOR = 1 << 4,
		ONE_MINUS_DST_COLOR = 1 << 5,
		SRC_ALPHA = 1 << 6,
		ONE_MINUS_SRC_ALPHA = 1 << 7,
		DST_ALPHA = 1 << 8,
		ONE_MINUS_DST_ALPHA = 1 << 9,
		SRC_ALPHA_SATURATE = 1 << 10,
	}

	public enum VGlineCap
	{
		BUTT,
		ROUND,
		SQUARE,
		BEVEL,
		MITER,
	}

	public enum VGalign
	{
		// Horizontal align
		ALIGN_LEFT = 1 << 0,
		// Default, align text horizontally to left.
		ALIGN_CENTER = 1 << 1,
		// Align text horizontally to center.
		ALIGN_RIGHT = 1 << 2,
		// Align text horizontally to right.
		// Vertical align
		ALIGN_TOP = 1 << 3,
		// Align text vertically to top.
		ALIGN_MIDDLE	= 1 << 4,
		// Align text vertically to middle.
		ALIGN_BOTTOM	= 1 << 5,
		// Align text vertically to bottom.
		ALIGN_BASELINE	= 1 << 6,
		// Default, align text vertically to baseline.
	}

	public enum VGcreateFlags
	{
		// Flag indicating if geometry based anti-aliasing is used (may not be needed when using MSAA).
		ANTIALIAS = 1 << 0,
		// Flag indicating if strokes should be drawn using stencil buffer. The rendering will be a little
		// slower, but path overlaps (i.e. self-intersecting or sharp turns) will be drawn just once.
		STENCIL_STROKES	= 1 << 1,
		// Flag indicating that additional debug checks are done.
		DEBUG = 1 << 2,
	}

	// These are additional flags on top of NVGimageFlags.
	public enum VGimageFlagsGL 
	{
		// Do not delete GL texture handle.
		IMAGE_NODELETE = 1<<16,	
	}

	public enum GLNVGuniformLoc
	{
		GLLOC_VIEWSIZE,
		GLLOC_TEX,
		GLLOC_FRAG,
		GLMAX_LOCS
	}

	public enum GLNVGcallType
	{
		GLNONE = 0,
		GLFILL,
		GLCONVEXFILL,
		GLSTROKE,
		GLTRIANGLES,
	}

	public enum VGsolidity
	{
		// Winding for solid shapes
		SOLID = 1,
		// Winding for holes
		HOLE = 2,
	}

	public enum VGwinding
	{
		// CCW
		CCW = 1,
		// CW
		CW = 2,
	}

	public enum GLNVGshaderType
	{
		NSVG_SHADER_FILLGRAD,
		NSVG_SHADER_FILLIMG,
		NSVG_SHADER_SIMPLE,
		NSVG_SHADER_IMG
	}

	public enum GraphrenderStyle
	{
		GRAPH_RENDER_FPS,
		GRAPH_RENDER_MS,
		GRAPH_RENDER_PERCENT,
	}

	public enum VGcodepointType
	{
		SPACE,
		NEWLINE,
		CHAR,
	}
}