using System;

namespace Shareds.Formatting
{
    public static class UIntFormatter
    {
        public static uint ToUInt(this Enum @enum)
        {
            return (uint)(int)(object)@enum;
        }

        public static uint ToUInt(this int @enum)
        {
            return (uint)@enum;
        }
    }
}
