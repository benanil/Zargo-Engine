using ImGuiNET;
using System;

namespace ZargoEngine.Editor
{
    [AttributeUsage(AttributeTargets.Field)]
    public class InputAttribute : Attribute
    {
        public float step;
        public float step_fast;
        public string format = "value: {0}";
        public ImGuiInputTextFlags flags = ImGuiInputTextFlags.None;

        public InputAttribute(float step = 1, float step_fast = 3, string format = "", ImGuiInputTextFlags flags = ImGuiInputTextFlags.None)
        {
            this.format = format == "" ? "%.1f" : format;
            this.step = step;
            this.step_fast = step_fast;
            this.flags = flags;
        }
    }
}
