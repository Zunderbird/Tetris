using UnityEngine;
using System;

namespace ExtensionMethods
{
    public static class XCoreExtension
    {
        public static Color ToColor(this int i_hexVal)
        {
            byte R = (byte)((i_hexVal >> 16) & 0xFF);
            byte G = (byte)((i_hexVal >> 8) & 0xFF);
            byte B = (byte)((i_hexVal) & 0xFF);
            return new Color(R, G, B);
        }

        public static int ToHexInt(this string i_string)
        {
            return Int32.Parse(i_string, System.Globalization.NumberStyles.HexNumber);
        }
    }
}
