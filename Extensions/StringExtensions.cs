

using UnityEngine;

namespace Laserbean.General
{

    public static class StringExtensions
    {

        public static string format(this string text, params string[] thing)
        {
            return string.Format(text, thing);
        }

        public static string DebugColor(this string text, string colour)
        {
            return string.Format("<color=" + colour + ">{0}</color>", text);
        }

        public static string DebugColor(this string text, Color colour)
        {
            string color = colour.ColorToHex();
            return string.Format("<color=" + color + ">{0}</color>", text);
        }

        public static string Color(this string text, Color colour)
        {
            string color = colour.ColorToHex();
            return text.RichFormat("color", color);
        }

        ///<summary>
        ///</summary>
        /// <param name="tag">b, i, size, color, material, quad</param>
        public static string RichFormat(this string text, string tag, string additional = "")
        {
            if (!additional.StartsWith("=") && !string.IsNullOrEmpty(additional)) additional = "=" + additional;
            return $"<{tag}{additional}>{text}</{tag}>";

        }

    }

    public static class ColorUtility
    {
        public static string ColorToHex(this Color color)
        {
            Color32 color32 = color;
            return "#" + color32.r.ToString("X2") + color32.g.ToString("X2") + color32.b.ToString("X2");
        }
    }

    // <color=blue>{0}</color>


}