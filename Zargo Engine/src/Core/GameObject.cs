using ImGuiNET;
using System;
using System.Collections.Generic;
using ZargoEngine.Rendering;

namespace ZargoEngine
{
    public class GameObject : Component
    {
        public Transform _transform;
        public Transform transform
        {
            get => _transform;
            set
            {
                // value changed
                if (value != transform)
                {
                    _transform = value;
                }
            }
        }

        private List<Component> behaviours = new List<Component>();

        public GameObject(string name)
        {
            this.name = name;
        }

        public override void Start()
        {
            for (int i = 0; i < behaviours.Count; i++){
                behaviours[i].Start();
            }
        }

        public override void Update()
        {
            for (int i = 0; i < behaviours.Count; i++){
                behaviours[i].Update();
            }
        }

        public override void DrawGUI()
        {
            ImGui.LabelText(name, "Game Object");

            for (int i = 0; i < behaviours.Count; i++){
                behaviours[i].DrawGUI();
            }
        }

        public Component AddComponent(Component component)
        {
            behaviours.Add(component);
            return component;
        }

        public Component GetComponent(Type type)
        {
            return behaviours.Find(x => x.GetType() == type);
        }
    }
}