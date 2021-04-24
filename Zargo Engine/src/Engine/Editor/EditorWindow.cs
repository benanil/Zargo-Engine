
using ImGuiNET;
using System.Collections.Generic;

namespace ZargoEngine.Editor
{
    public class EditorWindow : IDrawable
    {
        private protected bool windowActive = false;
        public string title = "Editor Window";

        private List<Menu> menus = new List<Menu>(); 

        public void DrawGUI()
        {
            ImGui.Begin(title, ref windowActive,ImGuiWindowFlags.None);

            if (ImGui.BeginMenuBar())
            {
                menus.ForEach(x => x.Draw());
                ImGui.EndMenuBar();
            }

            OnGUI();

            ImGui.End();
        }

        public virtual void OnGUI()
        { 
            
        }

        public void AddMenuItem(Menu menu)
        {
            menus.Add(menu);
        }
    }
}
