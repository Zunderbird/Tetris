using UnityEngine;
using System;

namespace Assets.MVC.View
{
    public static class XCoreExtension
    {
        public static Color ToColor(this int hexVal)
        {
            var r = (float)((hexVal >> 16) & 0xFF) / 0xFF;
            var g = (float)((hexVal >> 8) & 0xFF) / 0xFF;
            var b = (float)((hexVal) & 0xFF) / 0xFF;
            return new Color(r, g, b);
        }

        public static Color32 ToColor32(this int hexVal)
        {
            var r = (byte)((hexVal >> 16) & 0xFF);
            var g = (byte)((hexVal >> 8) & 0xFF);
            var b = (byte)((hexVal) & 0xFF);
            return new Color32(r, g, b, 1);
        }

        public static Color ToColor(this string hexVal)
        {
            var tempValue = Convert.ToInt32(hexVal, 16);
            return tempValue.ToColor();
        }

        public static Color32 ToColor32(this string hexVal)
        {
            var tempValue = Convert.ToInt32(hexVal, 16);
            return tempValue.ToColor32();
        }
    }
}
