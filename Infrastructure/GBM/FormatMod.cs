using System;

namespace GBM
{

    public enum DRM_FORMAT_MOD_VENDOR
    {

        None = 0,
        Intel = 0x01,
        AMD = 0x02,
        Nvidia = 0x03,
        Samsung = 0x04,
        QCom = 0x05,
        Vivante = 0x06,
        BroadCom = 0x07,
        Arm = 0x08,
        AllWinner = 0x09,
    }


    public class FormatMod
    {
        private const ulong DRM_FORMAT_RESERVED = ((ulong)1 << 56) - 1;

        private static Func<DRM_FORMAT_MOD_VENDOR, ulong, ulong> fourcc_mod_code = (vendor, val) => ((ulong)vendor << 56) | ((val) & (ulong)0x00ffffffffffffff);

        public static ulong DRM_FORMAT_MOD_INVALID => fourcc_mod_code(DRM_FORMAT_MOD_VENDOR.None, DRM_FORMAT_RESERVED);
        public static ulong DRM_FORMAT_MOD_LINEAR => fourcc_mod_code(DRM_FORMAT_MOD_VENDOR.None, 0);
        public static ulong I915_FORMAT_MOD_X_TILED => fourcc_mod_code(DRM_FORMAT_MOD_VENDOR.Intel, 1);
        public static ulong I915_FORMAT_MOD_Y_TILED => fourcc_mod_code(DRM_FORMAT_MOD_VENDOR.Intel, 2);
        public static ulong I915_FORMAT_MOD_Yf_TILED => fourcc_mod_code(DRM_FORMAT_MOD_VENDOR.Intel, 3);
        public static ulong I915_FORMAT_MOD_Y_TILED_CCS => fourcc_mod_code(DRM_FORMAT_MOD_VENDOR.Intel, 4);
        public static ulong I915_FORMAT_MOD_Yf_TILED_CCS => fourcc_mod_code(DRM_FORMAT_MOD_VENDOR.Intel, 5);
        public static ulong I915_FORMAT_MOD_Y_TILED_GEN12_RC_CCS => fourcc_mod_code(DRM_FORMAT_MOD_VENDOR.Intel, 6);
        public static ulong I915_FORMAT_MOD_Y_TILED_GEN12_MC_CCS => fourcc_mod_code(DRM_FORMAT_MOD_VENDOR.Intel, 7);
        public static ulong DRM_FORMAT_MOD_SAMSUNG_64_32_TILE => fourcc_mod_code(DRM_FORMAT_MOD_VENDOR.Samsung, 1);
        public static ulong DRM_FORMAT_MOD_SAMSUNG_16_16_TILE => fourcc_mod_code(DRM_FORMAT_MOD_VENDOR.Samsung, 2);
        public static ulong DRM_FORMAT_MOD_QCOM_COMPRESSED => fourcc_mod_code(DRM_FORMAT_MOD_VENDOR.QCom, 1);
        public static ulong DRM_FORMAT_MOD_VIVANTE_TILED => fourcc_mod_code(DRM_FORMAT_MOD_VENDOR.Vivante, 1);
        public static ulong DRM_FORMAT_MOD_VIVANTE_SUPER_TILED => fourcc_mod_code(DRM_FORMAT_MOD_VENDOR.Vivante, 2);
        public static ulong DRM_FORMAT_MOD_VIVANTE_SPLIT_TILED => fourcc_mod_code(DRM_FORMAT_MOD_VENDOR.Vivante, 3);
        public static ulong DRM_FORMAT_MOD_VIVANTE_SPLIT_SUPER_TILED => fourcc_mod_code(DRM_FORMAT_MOD_VENDOR.Vivante, 4);
        public static ulong DRM_FORMAT_MOD_NVIDIA_TEGRA_TILED => fourcc_mod_code(DRM_FORMAT_MOD_VENDOR.Nvidia, 1);

    }
}
