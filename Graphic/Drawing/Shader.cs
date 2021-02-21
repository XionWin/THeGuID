using System;

namespace Graphic.Drawing
{
    public class Shader: IDisposable
	{
		public GLESV2.GFX.GfxProgram Program{ get; set; }
		public GLESV2.GFX.GfxShader VeterxShader{ get; set; }
		public GLESV2.GFX.GfxProgram FragmentShader{ get; set; }

        public void Dispose()
        {
			this.Program?.Dispose();
			this.VeterxShader?.Dispose();
			this.FragmentShader?.Dispose();
        }
    }
}
