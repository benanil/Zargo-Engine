using ImGuiNET;
using System;

namespace ZargoEngine.Editor
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SliderAttribute : Attribute
    {
        public float min = 0;
        public float max = float.MaxValue;
        public string format = string.Empty;
        public ImGuiSliderFlags imGuiSliderFlags;

        public SliderAttribute(float min = 0, float max = 20, string format = "",ImGuiSliderFlags imGuiSliderFlags = ImGuiSliderFlags.None)
        {
            this.imGuiSliderFlags = imGuiSliderFlags;
            this.format = format == "" ? "%.1f" : format;
            this.min = min;
            this.max = max;
        }
    }
}
