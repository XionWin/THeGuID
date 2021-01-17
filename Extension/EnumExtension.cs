using System;

namespace Extension
{
    public static class EnumExtension
    {
        public static bool BitwiseContains<T>(this T item, T value)
        where T: Enum => (Convert.ToUInt16(item) & Convert.ToUInt16(value)) == Convert.ToUInt16(value);
    }
}
