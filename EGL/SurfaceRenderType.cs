namespace EGL
{
    public enum RenderableSurfaceType: uint
    {
        OpenGL = Egl.OPENGL_BIT,
        OpenGLES = Egl.OPENGL_ES_BIT,
        OpenGLESV2 = Egl.OPENGL_ES2_BIT,
        OpenGLESV3 = Egl.OPENGL_ES3_BIT,
        Window = Egl.WINDOW_BIT,
    }
}
