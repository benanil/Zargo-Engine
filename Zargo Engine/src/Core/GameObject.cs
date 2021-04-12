
using System.Collections.Generic;
using ZargoEngine.src.Core;

namespace ZargoEngine
{
    public class GameObject
    {
        public string name;
        public Transform transform;

        private List<Behaviour> components = new List<Behaviour>();

        public GameObject(string name,Transform transform){
            this.name = name;
            this.transform = transform;
            SceneManager.currentScene.AddGameObject(this);
        }

        public void Start(){
            for (int i = 0; i < components.Count; i++){
                components[i].Start();
            }
        }

        public void Update()
        {
            for (int i = 0; i < components.Count; i++){
                components[i].Update();
            }
        }

        public void AddComponent(Behaviour component)
        {
            components.Add(component);
        }

    }
}
