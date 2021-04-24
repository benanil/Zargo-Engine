using ImGuiNET;
using System;

namespace ZargoEngine.Editor
{
    public class Hierarchy : EditorWindow
    {
        public Hierarchy()
        {
            title = "Hierarchy";
        }

        public override void OnGUI()
        {
            for (int i = 0; i < 10; i++)
            {
                ImGui.Text("asdfasdf");
            }
        }
    }
}
