using System;

namespace GBM
{
    public class Format
    {
        private static Func<char, char, char, char, uint> fourcc_code = (a, b, c, d) => ((uint)(a) | ((uint)(b) << 8) | ((uint)(c) << 16) | ((uint)(d) << 24));


        // #define DRM_FORMAT_BIG_ENDIAN (1U<<31) /* format is big endian instead of little endian */

        // /* Reserve 0 for the invalid format specifier */
        // #define DRM_FORMAT_INVALID	0

        public static uint DRM_FORMAT_INVALID => 0;

        /* color index */
        public static uint DRM_FORMAT_C8 => fourcc_code('C', '8', ' ', ' '); /* [7:0] C */

        /* 8 bpp Red */
        public static uint DRM_FORMAT_R8 => fourcc_code('R', '8', ' ', ' '); /* [7:0] R */

        /* 16 bpp Red */
        public static uint DRM_FORMAT_R16 => fourcc_code('R', '1', '6', ' '); /* [15:0] R little endian */

        /* 16 bpp RG */
        public static uint DRM_FORMAT_RG88 => fourcc_code('R', 'G', '8', '8'); /* [15:0] R:G 8:8 little endian */
        public static uint DRM_FORMAT_GR88 => fourcc_code('G', 'R', '8', '8'); /* [15:0] G:R 8:8 little endian */

        /* 32 bpp RG */
        public static uint DRM_FORMAT_RG1616 => fourcc_code('R', 'G', '3', '2'); /* [31:0] R:G 16:16 little endian */
        public static uint DRM_FORMAT_GR1616 => fourcc_code('G', 'R', '3', '2'); /* [31:0] G:R 16:16 little endian */

        /* 8 bpp RGB */
        public static uint DRM_FORMAT_RGB332 => fourcc_code('R', 'G', 'B', '8'); /* [7:0] R:G:B 3:3:2 */
        public static uint DRM_FORMAT_BGR233 => fourcc_code('B', 'G', 'R', '8'); /* [7:0] B:G:R 2:3:3 */

        /* 16 bpp RGB */
        public static uint DRM_FORMAT_XRGB4444 => fourcc_code('X', 'R', '1', '2'); /* [15:0] x:R:G:B 4:4:4:4 little endian */
        public static uint DRM_FORMAT_XBGR4444 => fourcc_code('X', 'B', '1', '2'); /* [15:0] x:B:G:R 4:4:4:4 little endian */
        public static uint DRM_FORMAT_RGBX4444 => fourcc_code('R', 'X', '1', '2'); /* [15:0] R:G:B:x 4:4:4:4 little endian */
        public static uint DRM_FORMAT_BGRX4444 => fourcc_code('B', 'X', '1', '2'); /* [15:0] B:G:R:x 4:4:4:4 little endian */

        public static uint DRM_FORMAT_ARGB4444 => fourcc_code('A', 'R', '1', '2'); /* [15:0] A:R:G:B 4:4:4:4 little endian */
        public static uint DRM_FORMAT_ABGR4444 => fourcc_code('A', 'B', '1', '2'); /* [15:0] A:B:G:R 4:4:4:4 little endian */
        public static uint DRM_FORMAT_RGBA4444 => fourcc_code('R', 'A', '1', '2'); /* [15:0] R:G:B:A 4:4:4:4 little endian */
        public static uint DRM_FORMAT_BGRA4444 => fourcc_code('B', 'A', '1', '2'); /* [15:0] B:G:R:A 4:4:4:4 little endian */

        public static uint DRM_FORMAT_XRGB1555 => fourcc_code('X', 'R', '1', '5'); /* [15:0] x:R:G:B 1:5:5:5 little endian */
        public static uint DRM_FORMAT_XBGR1555 => fourcc_code('X', 'B', '1', '5'); /* [15:0] x:B:G:R 1:5:5:5 little endian */
        public static uint DRM_FORMAT_RGBX5551 => fourcc_code('R', 'X', '1', '5'); /* [15:0] R:G:B:x 5:5:5:1 little endian */
        public static uint DRM_FORMAT_BGRX5551 => fourcc_code('B', 'X', '1', '5'); /* [15:0] B:G:R:x 5:5:5:1 little endian */

        public static uint DRM_FORMAT_ARGB1555 => fourcc_code('A', 'R', '1', '5'); /* [15:0] A:R:G:B 1:5:5:5 little endian */
        public static uint DRM_FORMAT_ABGR1555 => fourcc_code('A', 'B', '1', '5'); /* [15:0] A:B:G:R 1:5:5:5 little endian */
        public static uint DRM_FORMAT_RGBA5551 => fourcc_code('R', 'A', '1', '5'); /* [15:0] R:G:B:A 5:5:5:1 little endian */
        public static uint DRM_FORMAT_BGRA5551 => fourcc_code('B', 'A', '1', '5'); /* [15:0] B:G:R:A 5:5:5:1 little endian */

        public static uint DRM_FORMAT_RGB565 => fourcc_code('R', 'G', '1', '6'); /* [15:0] R:G:B 5:6:5 little endian */
        public static uint DRM_FORMAT_BGR565 => fourcc_code('B', 'G', '1', '6'); /* [15:0] B:G:R 5:6:5 little endian */

        /* 24 bpp RGB */
        public static uint DRM_FORMAT_RGB888 => fourcc_code('R', 'G', '2', '4'); /* [23:0] R:G:B little endian */
        public static uint DRM_FORMAT_BGR888 => fourcc_code('B', 'G', '2', '4'); /* [23:0] B:G:R little endian */

        /* 32 bpp RGB */
        public static uint DRM_FORMAT_XRGB8888 => fourcc_code('X', 'R', '2', '4'); /* [31:0] x:R:G:B 8:8:8:8 little endian */
        public static uint DRM_FORMAT_XBGR8888 => fourcc_code('X', 'B', '2', '4'); /* [31:0] x:B:G:R 8:8:8:8 little endian */
        public static uint DRM_FORMAT_RGBX8888 => fourcc_code('R', 'X', '2', '4'); /* [31:0] R:G:B:x 8:8:8:8 little endian */
        public static uint DRM_FORMAT_BGRX8888 => fourcc_code('B', 'X', '2', '4'); /* [31:0] B:G:R:x 8:8:8:8 little endian */

        public static uint DRM_FORMAT_ARGB8888 => fourcc_code('A', 'R', '2', '4'); /* [31:0] A:R:G:B 8:8:8:8 little endian */
        public static uint DRM_FORMAT_ABGR8888 => fourcc_code('A', 'B', '2', '4'); /* [31:0] A:B:G:R 8:8:8:8 little endian */
        public static uint DRM_FORMAT_RGBA8888 => fourcc_code('R', 'A', '2', '4'); /* [31:0] R:G:B:A 8:8:8:8 little endian */
        public static uint DRM_FORMAT_BGRA8888 => fourcc_code('B', 'A', '2', '4'); /* [31:0] B:G:R:A 8:8:8:8 little endian */

        public static uint DRM_FORMAT_XRGB2101010 => fourcc_code('X', 'R', '3', '0'); /* [31:0] x:R:G:B 2:10:10:10 little endian */
        public static uint DRM_FORMAT_XBGR2101010 => fourcc_code('X', 'B', '3', '0'); /* [31:0] x:B:G:R 2:10:10:10 little endian */
        public static uint DRM_FORMAT_RGBX1010102 => fourcc_code('R', 'X', '3', '0'); /* [31:0] R:G:B:x 10:10:10:2 little endian */
        public static uint DRM_FORMAT_BGRX1010102 => fourcc_code('B', 'X', '3', '0'); /* [31:0] B:G:R:x 10:10:10:2 little endian */

        public static uint DRM_FORMAT_ARGB2101010 => fourcc_code('A', 'R', '3', '0'); /* [31:0] A:R:G:B 2:10:10:10 little endian */
        public static uint DRM_FORMAT_ABGR2101010 => fourcc_code('A', 'B', '3', '0'); /* [31:0] A:B:G:R 2:10:10:10 little endian */
        public static uint DRM_FORMAT_RGBA1010102 => fourcc_code('R', 'A', '3', '0'); /* [31:0] R:G:B:A 10:10:10:2 little endian */
        public static uint DRM_FORMAT_BGRA1010102 => fourcc_code('B', 'A', '3', '0'); /* [31:0] B:G:R:A 10:10:10:2 little endian */

        /*
         * Floating point 64bpp RGB
         * IEEE 754-2008 binary16 half-precision float
         * [15:0] sign:exponent:mantissa 1:5:10
         */
        public static uint DRM_FORMAT_XRGB16161616F => fourcc_code('X', 'R', '4', 'H'); /* [63:0] x:R:G:B 16:16:16:16 little endian */
        public static uint DRM_FORMAT_XBGR16161616F => fourcc_code('X', 'B', '4', 'H'); /* [63:0] x:B:G:R 16:16:16:16 little endian */

        public static uint DRM_FORMAT_ARGB16161616F => fourcc_code('A', 'R', '4', 'H'); /* [63:0] A:R:G:B 16:16:16:16 little endian */
        public static uint DRM_FORMAT_ABGR16161616F => fourcc_code('A', 'B', '4', 'H'); /* [63:0] A:B:G:R 16:16:16:16 little endian */

        /* packed YCbCr */
        public static uint DRM_FORMAT_YUYV => fourcc_code('Y', 'U', 'Y', 'V'); /* [31:0] Cr0:Y1:Cb0:Y0 8:8:8:8 little endian */
        public static uint DRM_FORMAT_YVYU => fourcc_code('Y', 'V', 'Y', 'U'); /* [31:0] Cb0:Y1:Cr0:Y0 8:8:8:8 little endian */
        public static uint DRM_FORMAT_UYVY => fourcc_code('U', 'Y', 'V', 'Y'); /* [31:0] Y1:Cr0:Y0:Cb0 8:8:8:8 little endian */
        public static uint DRM_FORMAT_VYUY => fourcc_code('V', 'Y', 'U', 'Y'); /* [31:0] Y1:Cb0:Y0:Cr0 8:8:8:8 little endian */

        public static uint DRM_FORMAT_AYUV => fourcc_code('A', 'Y', 'U', 'V'); /* [31:0] A:Y:Cb:Cr 8:8:8:8 little endian */
        public static uint DRM_FORMAT_XYUV8888 => fourcc_code('X', 'Y', 'U', 'V'); /* [31:0] X:Y:Cb:Cr 8:8:8:8 little endian */
        public static uint DRM_FORMAT_VUY888 => fourcc_code('V', 'U', '2', '4'); /* [23:0] Cr:Cb:Y 8:8:8 little endian */
        public static uint DRM_FORMAT_VUY101010 => fourcc_code('V', 'U', '3', '0'); /* Y followed by U then V, 10:10:10. Non-linear modifier only */

        /*
         * packed Y2xx indicate for each component, xx valid data occupy msb
         * 16-xx padding occupy lsb
         */
        public static uint DRM_FORMAT_Y210 => fourcc_code('Y', '2', '1', '0'); /* [63:0] Cr0:0:Y1:0:Cb0:0:Y0:0 10:6:10:6:10:6:10:6 little endian per 2 Y pixels */
        public static uint DRM_FORMAT_Y212 => fourcc_code('Y', '2', '1', '2'); /* [63:0] Cr0:0:Y1:0:Cb0:0:Y0:0 12:4:12:4:12:4:12:4 little endian per 2 Y pixels */
        public static uint DRM_FORMAT_Y216 => fourcc_code('Y', '2', '1', '6'); /* [63:0] Cr0:Y1:Cb0:Y0 16:16:16:16 little endian per 2 Y pixels */

        /*
         * packed Y4xx indicate for each component, xx valid data occupy msb
         * 16-xx padding occupy lsb except Y410
         */
        public static uint DRM_FORMAT_Y410 => fourcc_code('Y', '4', '1', '0'); /* [31:0] A:Cr:Y:Cb 2:10:10:10 little endian */
        public static uint DRM_FORMAT_Y412 => fourcc_code('Y', '4', '1', '2'); /* [63:0] A:0:Cr:0:Y:0:Cb:0 12:4:12:4:12:4:12:4 little endian */
        public static uint DRM_FORMAT_Y416 => fourcc_code('Y', '4', '1', '6'); /* [63:0] A:Cr:Y:Cb 16:16:16:16 little endian */

        public static uint DRM_FORMAT_XVYU2101010 => fourcc_code('X', 'V', '3', '0'); /* [31:0] X:Cr:Y:Cb 2:10:10:10 little endian */
        public static uint DRM_FORMAT_XVYU12_16161616 => fourcc_code('X', 'V', '3', '6'); /* [63:0] X:0:Cr:0:Y:0:Cb:0 12:4:12:4:12:4:12:4 little endian */
        public static uint DRM_FORMAT_XVYU16161616 => fourcc_code('X', 'V', '4', '8'); /* [63:0] X:Cr:Y:Cb 16:16:16:16 little endian */

        /*
         * packed YCbCr420 2x2 tiled formats
         * first 64 bits will contain Y,Cb,Cr components for a 2x2 tile
         */
        /* [63:0]   A3:A2:Y3:0:Cr0:0:Y2:0:A1:A0:Y1:0:Cb0:0:Y0:0  1:1:8:2:8:2:8:2:1:1:8:2:8:2:8:2 little endian */
        public static uint DRM_FORMAT_Y0L0 => fourcc_code('Y', '0', 'L', '0');
        /* [63:0]   X3:X2:Y3:0:Cr0:0:Y2:0:X1:X0:Y1:0:Cb0:0:Y0:0  1:1:8:2:8:2:8:2:1:1:8:2:8:2:8:2 little endian */
        public static uint DRM_FORMAT_X0L0 => fourcc_code('X', '0', 'L', '0');

        /* [63:0]   A3:A2:Y3:Cr0:Y2:A1:A0:Y1:Cb0:Y0  1:1:10:10:10:1:1:10:10:10 little endian */
        public static uint DRM_FORMAT_Y0L2 => fourcc_code('Y', '0', 'L', '2');
        /* [63:0]   X3:X2:Y3:Cr0:Y2:X1:X0:Y1:Cb0:Y0  1:1:10:10:10:1:1:10:10:10 little endian */
        public static uint DRM_FORMAT_X0L2 => fourcc_code('X', '0', 'L', '2');

        /*
         * 1-plane YUV 4:2:0
         * In these formats, the component ordering is specified (Y, followed by U
         * then V), but the exact Linear layout is undefined.
         * These formats can only be used with a non-Linear modifier.
         */
        public static uint DRM_FORMAT_YUV420_8BIT => fourcc_code('Y', 'U', '0', '8');
        public static uint DRM_FORMAT_YUV420_10BIT => fourcc_code('Y', 'U', '1', '0');

        /*
         * 2 plane RGB + A
         * index 0 = RGB plane, same format as the corresponding non _A8 format has
         * index 1 = A plane, [7:0] A
         */
        public static uint DRM_FORMAT_XRGB8888_A8 => fourcc_code('X', 'R', 'A', '8');
        public static uint DRM_FORMAT_XBGR8888_A8 => fourcc_code('X', 'B', 'A', '8');
        public static uint DRM_FORMAT_RGBX8888_A8 => fourcc_code('R', 'X', 'A', '8');
        public static uint DRM_FORMAT_BGRX8888_A8 => fourcc_code('B', 'X', 'A', '8');
        public static uint DRM_FORMAT_RGB888_A8 => fourcc_code('R', '8', 'A', '8');
        public static uint DRM_FORMAT_BGR888_A8 => fourcc_code('B', '8', 'A', '8');
        public static uint DRM_FORMAT_RGB565_A8 => fourcc_code('R', '5', 'A', '8');
        public static uint DRM_FORMAT_BGR565_A8 => fourcc_code('B', '5', 'A', '8');

        /*
         * 2 plane YCbCr
         * index 0 = Y plane, [7:0] Y
         * index 1 = Cr:Cb plane, [15:0] Cr:Cb little endian
         * or
         * index 1 = Cb:Cr plane, [15:0] Cb:Cr little endian
         */
        public static uint DRM_FORMAT_NV12 => fourcc_code('N', 'V', '1', '2'); /* 2x2 subsampled Cr:Cb plane */
        public static uint DRM_FORMAT_NV21 => fourcc_code('N', 'V', '2', '1'); /* 2x2 subsampled Cb:Cr plane */
        public static uint DRM_FORMAT_NV16 => fourcc_code('N', 'V', '1', '6'); /* 2x1 subsampled Cr:Cb plane */
        public static uint DRM_FORMAT_NV61 => fourcc_code('N', 'V', '6', '1'); /* 2x1 subsampled Cb:Cr plane */
        public static uint DRM_FORMAT_NV24 => fourcc_code('N', 'V', '2', '4'); /* non-subsampled Cr:Cb plane */
        public static uint DRM_FORMAT_NV42 => fourcc_code('N', 'V', '4', '2'); /* non-subsampled Cb:Cr plane */

        /*
         * 2 plane YCbCr MSB aligned
         * index 0 = Y plane, [15:0] Y:x [10:6] little endian
         * index 1 = Cr:Cb plane, [31:0] Cr:x:Cb:x [10:6:10:6] little endian
         */
        public static uint DRM_FORMAT_P210 => fourcc_code('P', '2', '1', '0'); /* 2x1 subsampled Cr:Cb plane, 10 bit per channel */

        /*
         * 2 plane YCbCr MSB aligned
         * index 0 = Y plane, [15:0] Y:x [10:6] little endian
         * index 1 = Cr:Cb plane, [31:0] Cr:x:Cb:x [10:6:10:6] little endian
         */
        public static uint DRM_FORMAT_P010 => fourcc_code('P', '0', '1', '0'); /* 2x2 subsampled Cr:Cb plane 10 bits per channel */

        /*
         * 2 plane YCbCr MSB aligned
         * index 0 = Y plane, [15:0] Y:x [12:4] little endian
         * index 1 = Cr:Cb plane, [31:0] Cr:x:Cb:x [12:4:12:4] little endian
         */
        public static uint DRM_FORMAT_P012 => fourcc_code('P', '0', '1', '2'); /* 2x2 subsampled Cr:Cb plane 12 bits per channel */

        /*
         * 2 plane YCbCr MSB aligned
         * index 0 = Y plane, [15:0] Y little endian
         * index 1 = Cr:Cb plane, [31:0] Cr:Cb [16:16] little endian
         */
        public static uint DRM_FORMAT_P016 => fourcc_code('P', '0', '1', '6'); /* 2x2 subsampled Cr:Cb plane 16 bits per channel */

        /*
         * 3 plane YCbCr
         * index 0: Y plane, [7:0] Y
         * index 1: Cb plane, [7:0] Cb
         * index 2: Cr plane, [7:0] Cr
         * or
         * index 1: Cr plane, [7:0] Cr
         * index 2: Cb plane, [7:0] Cb
         */
        public static uint DRM_FORMAT_YUV410 => fourcc_code('Y', 'U', 'V', '9'); /* 4x4 subsampled Cb (1) and Cr (2) planes */
        public static uint DRM_FORMAT_YVU410 => fourcc_code('Y', 'V', 'U', '9'); /* 4x4 subsampled Cr (1) and Cb (2) planes */
        public static uint DRM_FORMAT_YUV411 => fourcc_code('Y', 'U', '1', '1'); /* 4x1 subsampled Cb (1) and Cr (2) planes */
        public static uint DRM_FORMAT_YVU411 => fourcc_code('Y', 'V', '1', '1'); /* 4x1 subsampled Cr (1) and Cb (2) planes */
        public static uint DRM_FORMAT_YUV420 => fourcc_code('Y', 'U', '1', '2'); /* 2x2 subsampled Cb (1) and Cr (2) planes */
        public static uint DRM_FORMAT_YVU420 => fourcc_code('Y', 'V', '1', '2'); /* 2x2 subsampled Cr (1) and Cb (2) planes */
        public static uint DRM_FORMAT_YUV422 => fourcc_code('Y', 'U', '1', '6'); /* 2x1 subsampled Cb (1) and Cr (2) planes */
        public static uint DRM_FORMAT_YVU422 => fourcc_code('Y', 'V', '1', '6'); /* 2x1 subsampled Cr (1) and Cb (2) planes */
        public static uint DRM_FORMAT_YUV444 => fourcc_code('Y', 'U', '2', '4'); /* non-subsampled Cb (1) and Cr (2) planes */
        public static uint DRM_FORMAT_YVU444 => fourcc_code('Y', 'V', '2', '4'); /* non-subsampled Cr (1) and Cb (2) planes */


    }
}
