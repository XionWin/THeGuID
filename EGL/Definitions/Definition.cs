using System;

namespace EGL.Definitions
{
    using EGLNativeDisplayType = IntPtr;

    #region consts
    public static class Definition
    {
        public const int VERSION_1_0 = 1;
        public const int VERSION_1_1 = 1;
        public const int VERSION_1_2 = 1;
        public const int VERSION_1_3 = 1;
        public const int VERSION_1_4 = 1;
        public const int FALSE = 0;
        public const int TRUE = 1;
        public const int DONT_CARE = -1;
        public const int CONTEXT_LOST = 12302;
        public const int BUFFER_SIZE = 12320;
        public const int ALPHA_SIZE = 12321;
        public const int BLUE_SIZE = 12322;
        public const int GREEN_SIZE = 12323;
        public const int RED_SIZE = 12324;
        public const int DEPTH_SIZE = 12325;
        public const int STENCIL_SIZE = 12326;
        public const int CONFIG_CAVEAT = 12327;
        public const int CONFIG_ID = 12328;
        public const int LEVEL = 12329;
        public const int MAX_PBUFFER_HEIGHT = 12330;
        public const int MAX_PBUFFER_PIXELS = 12331;
        public const int MAX_PBUFFER_WIDTH = 12332;
        public const int NATIVE_RENDERABLE = 12333;
        public const int NATIVE_VISUAL_ID = 12334;
        public const int NATIVE_VISUAL_TYPE = 12335;
        public const int PRESERVED_RESOURCES = 12336;
        public const int SAMPLES = 12337;
        public const int SAMPLE_BUFFERS = 12338;
        public const int SURFACE_TYPE = 12339;
        public const int TRANSPARENT_TYPE = 12340;
        public const int TRANSPARENT_BLUE_VALUE = 12341;
        public const int TRANSPARENT_GREEN_VALUE = 12342;
        public const int TRANSPARENT_RED_VALUE = 12343;
        public const int NONE = 12344;
        public const int BIND_TO_TEXTURE_RGB = 12345;
        public const int BIND_TO_TEXTURE_RGBA = 12346;
        public const int MIN_SWAP_INTERVAL = 12347;
        public const int MAX_SWAP_INTERVAL = 12348;
        public const int LUMINANCE_SIZE = 12349;
        public const int ALPHA_MASK_SIZE = 12350;
        public const int COLOR_BUFFER_TYPE = 12351;
        public const int RENDERABLE_TYPE = 12352;
        public const int MATCH_NATIVE_PIXMAP = 12353;
        public const int CONFORMANT = 12354;
        public const int SLOW_CONFIG = 12368;
        public const int NON_CONFORMANT_CONFIG = 12369;
        public const int TRANSPARENT_RGB = 12370;
        public const int RGB_BUFFER = 12430;
        public const int LUMINANCE_BUFFER = 12431;
        public const int NO_TEXTURE = 12380;
        public const int TEXTURE_RGB = 12381;
        public const int TEXTURE_RGBA = 12382;
        public const int TEXTURE_2D = 12383;
        public const int PBUFFER_BIT = 1;
        public const int PIXMAP_BIT = 2;
        public const int WINDOW_BIT = 4;
        public const int VG_COLORSPACE_LINEAR_BIT = 32;
        public const int VG_ALPHA_FORMAT_PRE_BIT = 64;
        public const int MULTISAMPLE_RESOLVE_BOX_BIT = 512;
        public const int SWAP_BEHAVIOR_PRESERVED_BIT = 1024;
        public const int OPENGL_ES_BIT = 1;
        public const int OPENVG_BIT = 2;
        public const int OPENGL_ES2_BIT = 4;
        public const int OPENGL_BIT = 8;
        public const int OPENGL_ES3_BIT = 64;
        public const int VENDOR = 12371;
        public const int VERSION = 12372;
        public const int EXTENSIONS = 12373;
        public const int CLIENT_APIS = 12429;
        public const int HEIGHT = 12374;
        public const int WIDTH = 12375;
        public const int LARGEST_PBUFFER = 12376;
        public const int TEXTURE_FORMAT = 12416;
        public const int TEXTURE_TARGET = 12417;
        public const int MIPMAP_TEXTURE = 12418;
        public const int MIPMAP_LEVEL = 12419;
        public const int RENDER_BUFFER = 12422;
        public const int VG_COLORSPACE = 12423;
        public const int VG_ALPHA_FORMAT = 12424;
        public const int HORIZONTAL_RESOLUTION = 12432;
        public const int VERTICAL_RESOLUTION = 12433;
        public const int PIXEL_ASPECT_RATIO = 12434;
        public const int SWAP_BEHAVIOR = 12435;
        public const int MULTISAMPLE_RESOLVE = 12441;
        public const int BACK_BUFFER = 12420;
        public const int SINGLE_BUFFER = 12421;
        public const int VG_COLORSPACE_sRGB = 12425;
        public const int VG_COLORSPACE_LINEAR = 12426;
        public const int VG_ALPHA_FORMAT_NONPRE = 12427;
        public const int VG_ALPHA_FORMAT_PRE = 12428;
        public const int DISPLAY_SCALING = 10000;
        public const int UNKNOWN = -1;
        public const int BUFFER_PRESERVED = 12436;
        public const int BUFFER_DESTROYED = 12437;
        public const int OPENVG_IMAGE = 12438;
        public const int CONTEXT_CLIENT_TYPE = 12439;
        public const int CONTEXT_CLIENT_VERSION = 12440;
        public const int MULTISAMPLE_RESOLVE_DEFAULT = 12442;
        public const int MULTISAMPLE_RESOLVE_BOX = 12443;
        public const int OPENGL_ES_API = 12448;
        public const int OPENVG_API = 12449;
        public const int OPENGL_API = 12450;
        public const int DRAW = 12377;
        public const int READ = 12378;
        public const int CORE_NATIVE_ENGINE = 12379;
        public const int COLORSPACE = VG_COLORSPACE;
        public const int ALPHA_FORMAT = VG_ALPHA_FORMAT;
        public const int COLORSPACE_sRGB = VG_COLORSPACE_sRGB;
        public const int COLORSPACE_LINEAR = VG_COLORSPACE_LINEAR;
        public const int ALPHA_FORMAT_NONPRE = VG_ALPHA_FORMAT_NONPRE;
        public const int ALPHA_FORMAT_PRE = VG_ALPHA_FORMAT_PRE;

        // EGL_ANGLE_d3d_share_handle_client_buffer
        public const int D3D_TEXTURE_2D_SHARE_HANDLE_ANGLE = 0x3200;
        // EGL_ANGLE_window_fixed_size
        public const int FIXED_SIZE_ANGLE = 0x3201;
        // EGL_ANGLE_software_display
        public static readonly EGLNativeDisplayType SOFTWARE_DISPLAY_ANGLE = new EGLNativeDisplayType(-1);
        // EGL_ANGLE_direct3d_display
        public static readonly EGLNativeDisplayType D3D11_ELSE_D3D9_DISPLAY_ANGLE = new EGLNativeDisplayType(-2);
        public static readonly EGLNativeDisplayType D3D11_ONLY_DISPLAY_ANGLE = new EGLNativeDisplayType(-3);
        // EGL_ANGLE_device_d3d
        public const int D3D9_DEVICE_ANGLE = 0x33A0;
        public const int D3D11_DEVICE_ANGLE = 0x33A1;
        // EGL_ANGLE_platform_angle
        public const int PLATFORM_ANGLE_ANGLE = 0x3202;
        public const int PLATFORM_ANGLE_TYPE_ANGLE = 0x3203;
        public const int PLATFORM_ANGLE_MAX_VERSION_MAJOR_ANGLE = 0x3204;
        public const int PLATFORM_ANGLE_MAX_VERSION_MINOR_ANGLE = 0x3205;
        public const int PLATFORM_ANGLE_TYPE_DEFAULT_ANGLE = 0x3206;
        // EGL_ANGLE_platform_angle_d3d
        public const int PLATFORM_ANGLE_TYPE_D3D9_ANGLE = 0x3207;
        public const int PLATFORM_ANGLE_TYPE_D3D11_ANGLE = 0x3208;
        public const int PLATFORM_ANGLE_DEVICE_TYPE_ANGLE = 0x3209;
        public const int PLATFORM_ANGLE_DEVICE_TYPE_HARDWARE_ANGLE = 0x320A;
        public const int PLATFORM_ANGLE_DEVICE_TYPE_WARP_ANGLE = 0x320B;
        public const int PLATFORM_ANGLE_DEVICE_TYPE_REFERENCE_ANGLE = 0x320C;
        public const int PLATFORM_ANGLE_ENABLE_AUTOMATIC_TRIM_ANGLE = 0x320F;
        // EGL_ANGLE_platform_angle_opengl
        public const int PLATFORM_ANGLE_TYPE_OPENGL_ANGLE = 0x320D;
        public const int PLATFORM_ANGLE_TYPE_OPENGLES_ANGLE = 0x320E;

        public const int EGL_DONT_CARE = -1;
    }
    #endregion
    }