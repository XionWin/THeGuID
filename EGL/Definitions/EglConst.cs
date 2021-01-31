using System;

namespace EGL.Definitions
{
    [Flags]
    public enum EglConst
    {
        Version10 = 1,

        CoreNativeEngine = 0x305B,


        Draw = 0x3059,
        Extensions = 0x3055,
        False = 0,
        NativeRenderable = 0x302D,
        NativeVisualId = 0x302E,
        NativeVisualType = 0x302F,


        Read = 0x305A,


        Success = 0x3000,


        True = 1,

        Vendor = 0x3053,
        Version = 0x3054,


        Version11 = 1,

        BackBuffer = 0x3084,


        MipmapTexture = 0x3082,
        MipmapLevel = 0x3083,
        NoTexture = 0x305C,
        Texture2d = 0x305F,
        TextureFormat = 0x3080,
        TextureRgb = 0x305D,
        TextureRgba = 0x305E,
        TextureTarget = 0x3081,
        Version12 = 1,
        AlphaFormat = 0x3088,
        AlphaFormatNonpre = 0x308B,
        AlphaFormatPre = 0x308C,

        BufferPreserved = 0x3094,
        BufferDestroyed = 0x3095,
        ClientApis = 0x308D,
        Colorspace = 0x3087,
        ColorspaceSrgb = 0x3089,
        ColorspaceLinear = 0x308A,

        ContextClientType = 0x3097,
        DisplayScaling = 10000,
        HorizontalResolution = 0x3090,
        LuminanceSize = 0x303D,

        OpenglEsApi = 0x30A0,
        OpenvgApi = 0x30A1,
        OpenvgImage = 0x3096,
        PixelAspectRatio = 0x3092,

        RenderBuffer = 0x3086,



        SingleBuffer = 0x3085,
        SwapBehavior = 0x3093,
        Unknown = -1,
        VerticalResolution = 0x3091,
        Version13 = 1,
        Conformant = 0x3042,
        ContextClientVersion = 0x3098,

        VgAlphaFormat = 0x3088,
        VgAlphaFormatNonpre = 0x308B,
        VgAlphaFormatPre = 0x308C,

        VgColorspace = 0x3087,
        VgColorspaceSrgb = 0x3089,
        VgColorspaceLinear = 0x308A,

        Version14 = 1,

        DefaultDisplay = 0,

        MultisampleResolve = 0x3099,
        MultisampleResolveDefault = 0x309A,
        MultisampleResolveBox = 0x309B,
        OpenglApi = 0x30A2,
        Version15 = 1,
        ContextMajorVersion = 0x3098,
        ContextMinorVersion = 0x30FB,
        ContextOpenglProfileMask = 0x30FD,
        ContextOpenglResetNotificationStrategy = 0x31BD,
        NoResetNotification = 0x31BE,
        LoseContextOnReset = 0x31BF,
        ContextOpenglCoreProfileBit = 0x00000001,
        ContextOpenglCompatibilityProfileBit = 0x00000002,
        ContextOpenglDebug = 0x31B0,
        ContextOpenglForwardCompatible = 0x31B1,
        ContextOpenglRobustAccess = 0x31B2,

        ClEventHandle = 0x309C,
        SyncClEvent = 0x30FE,
        SyncClEventComplete = 0x30FF,
        SyncPriorCommandsComplete = 0x30F0,
        SyncType = 0x30F7,
        SyncStatus = 0x30F1,
        SyncCondition = 0x30F8,
        Signaled = 0x30F2,
        Unsignaled = 0x30F3,
        SyncFlushCommandsBit = 0x0001,
        Forever = int.MinValue,
        TimeoutExpired = 0x30F5,
        ConditionSatisfied = 0x30F6,
        NoSync = 0,
        SyncFence = 0x30F9,

        GlColorspace = 0x309D,
        GlColorspaceSrgb = 0x3089,
        GlColorspaceLinear = 0x308A,
        GlRenderbuffer = 0x30B9,
        GlTexture2d = 0x30B1,
        GlTextureLevel = 0x30BC,
        GlTexture3d = 0x30B2,
        GlTextureZoffset = 0x30BD,
        GlTextureCubeMapPositiveX = 0x30B3,
        GlTextureCubeMapNegativeX = 0x30B4,
        GlTextureCubeMapPositiveY = 0x30B5,
        GlTextureCubeMapNegativeY = 0x30B6,
        GlTextureCubeMapPositiveZ = 0x30B7,
        GlTextureCubeMapNegativeZ = 0x30B8,
        ImagePreserved = 0x30D2,
        NoImage = 0,
    }
}