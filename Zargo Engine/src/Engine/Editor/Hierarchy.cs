
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
            ImGui.Begin("Scene Hierarchy");

            SceneManager.currentScene.gameObjects.ForEach(x =>
            {
                DrawEntity(x);
            });

            deletedObject?.Dispose();
            ImGui.End();
        }

        GameObject deletedObject;

        private  void DrawEntity(GameObject entity)
        {
            var flags = (Inspector.instance.currentObject == entity) ? ImGuiTreeNodeFlags.OpenOnArrow : 0 | ImGuiTreeNodeFlags.Selected;
            bool opened = ImGui.TreeNodeEx(entity.name, flags);
            
            if (ImGui.IsItemClicked()){
                Inspector.instance.currentObject = entity;
            }

            if (opened){
                ImGui.TreePop();
            }

            if (ImGui.BeginPopupContextWindow())
            {
                if (ImGui.MenuItem("Delete")){
                    deletedObject = entity;
                }
                ImGui.EndPopup();
            }
        }
    }
}
