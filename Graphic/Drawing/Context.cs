

namespace Graphic.Drawing
{
    public class GLNVGcontext
	{
		public Shader shader;
		public Texture[] textures;
		// [2]
		public float[] view;
		public int ntextures;
		public int ctextures;
		public int textureId;
		public uint vertBuf;
#if NANOVG_GL3
		public uint vertArr;
#endif
		#if NANOVG_GL_USE_UNIFORMBUFFER
		public uint fragBuf;
#endif
		public int fragSize;
		public int flags;

		// Per frame buffers
		// public GLNVGcall[] calls;
		public int ccalls;
		public int ncalls;
		// public GLNVGpath[] paths;
		public int cpaths;
		public int npaths;
		// public NVGvertex[] verts;
		public int cverts;
		public int nverts;
		// public GLNVGfragUniforms[] uniforms;
		public int cuniforms;
		public int nuniforms;


		public GLNVGcontext()
		{
			view = new float[2];
		}
	}
}
