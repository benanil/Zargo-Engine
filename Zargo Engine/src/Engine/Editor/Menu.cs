
using ImGuiNET;
using System.Collections.Generic;

namespace ZargoEngine.Editor
{
    public class Menu
    {
        public string name = "Menu Name";

        private List<MenuItem> menuItems = new List<MenuItem>();

        public void Draw()
        {
            if (ImGui.BeginMenu(name))
            {
                menuItems.ForEach(x => x.Draw());
                ImGui.EndMenu();
            }
        }

        public void AddMenuItem(MenuItem menuItem)
        {
            menuItems.Add(menuItem);
        }

        public Menu(string name)
        {
            this.name = name;
        }
    }
}
