
using ImGuiNET;
using System;

namespace ZargoEngine.Editor
{
    public class MenuItem
    {
        public string name = "menu Item";
        public string shortcut = "";

        public event Action OnClick;

        public MenuItem(string name)
        {
            this.name = name;
        }
        
        public void Draw()
        {
            if (ImGui.MenuItem(name, shortcut))
            {
                OnClick();
            }
        }
    }
}
