using System;
using System.Collections.Generic;
using ZargoEngine.Rendering;

namespace ZargoEngine
{
    public class GameObject
    {
        public string name;
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

        private List<MonoBehaviour> behaviours = new List<MonoBehaviour>();

        public GameObject(string name)
        {
            this.name = name;
        }

        public void Start()
        {
            for (int i = 0; i < behaviours.Count; i++)
            {
                behaviours[i].Start();
            }
        }

        public void Update()
        {
            for (int i = 0; i < behaviours.Count; i++)
            {
                behaviours[i].Update();
            }
        }

        public void OnGUI()
        {
            for (int i = 0; i < behaviours.Count; i++)
            {
                behaviours[i].OnGUI();
            }
        }

        public void AddComponent(MonoBehaviour component)
        {
            behaviours.Add(component);
        }

        public MonoBehaviour GetBehaviour(Type type)
        {
            return behaviours.Find(x => x.GetType() == type);
        }
    }
}