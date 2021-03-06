namespace Graphic.Core
{
    public enum RenderType: uint
    {
        OpenGL = EGL.RenderableSurfaceType.OpenGL,
        OpenGLES = EGL.RenderableSurfaceType.OpenGLES,
        // OpenGLESV2 = EGL.RenderableSurfaceType.OpenGLESV2,
        // OpenGLESV3 = EGL.RenderableSurfaceType.OpenGLESV3,
        Window = EGL.RenderableSurfaceType.Window,
    }
}
