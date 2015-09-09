using UnityEngine;
using System;

// code taken from http://unity3d.ru/distribution/viewtopic.php?f=80&t=11849
// author DanielDem

namespace ExtensionMethods
{
    public static class XCoreExtension
    {
        public static Color32 ToColor32(this Int32 rgba_color)
        {
            Color32 color_value = new Color32();
            color_value.r = ((Byte)(rgba_color >> 24));
            color_value.g = ((Byte)(rgba_color >> 16));
            color_value.b = ((Byte)(rgba_color >> 8));
            color_value.a = ((Byte)(rgba_color >> 0));

            return (color_value);
        }

        public static Color ToColor(this Int32 rgba_color)
        {
            const Single factor = 1.0f / 255.0f;
            Color color_value = new Color();
            color_value.r = factor * (Single)((Byte)(rgba_color >> 24));
            color_value.g = factor * (Single)((Byte)(rgba_color >> 16));
            color_value.b = factor * (Single)((Byte)(rgba_color >> 8));
            color_value.a = factor * (Single)((Byte)(rgba_color >> 0));

            return (color_value);
        }

        public static Int32 ToRGBA(this Color32 color_value)
        {
            return ((Int32)((color_value.r << 24) | (color_value.g << 16) | (color_value.b << 8) | color_value.a));
        }

        public static Color ToColor(this Color32 color_value)
        {
            const Single factor = 1.0f / 255.0f;
            Color color = new Color();
            color.r = (Single)(color_value.r) * factor;
            color.g = (Single)(color_value.g) * factor;
            color.b = (Single)(color_value.b) * factor;
            color.a = (Single)(color_value.a) * factor;

            return (color);
        }

        public static Int32 ToRGBA(this Color color_value)
        {
            Int32 red = color_value.r >= 1.0f ? 0xff :
                                       color_value.r <= 0.0f ? 0x00 : (Int32)(color_value.r * 255.0f + 0.5f);
            Int32 green = color_value.g >= 1.0f ? 0xff :
                                       color_value.g <= 0.0f ? 0x00 : (Int32)(color_value.g * 255.0f + 0.5f);
            Int32 blue = color_value.b >= 1.0f ? 0xff :
                                       color_value.b <= 0.0f ? 0x00 : (Int32)(color_value.b * 255.0f + 0.5f);
            Int32 alpha = color_value.a >= 1.0f ? 0xff :
                                       color_value.a <= 0.0f ? 0x00 : (Int32)(color_value.a * 255.0f + 0.5f);

            return ((Int32)((red << 24) | (green << 16) | (blue << 8) | alpha));
        }

        public static Color32 ToColor32(this Color color_value)
        {
            const Single factor = 255.0f;
            Color32 color = new Color32();
            color.r = (Byte)((color_value.r) * factor);
            color.g = (Byte)((color_value.g) * factor);
            color.b = (Byte)((color_value.b) * factor);
            color.a = (Byte)((color_value.a) * factor);

            return (color);
        }

        public static Single GetMaxExtents(this Bounds bounds)
        {
            if (bounds.extents.x >= bounds.extents.y && bounds.extents.x >= bounds.extents.z)
            {
                return (bounds.extents.x);
            }
            else
            {
                if (bounds.extents.y >= bounds.extents.x && bounds.extents.y >= bounds.extents.z)
                {
                    return (bounds.extents.y);
                }
                else
                {
                    return (bounds.extents.z);
                }
            }
        }
    }
}
