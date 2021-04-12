
using System;
using System.Collections.Generic;
using ZargoEngine.src.Core;

namespace ZargoEngine
{
    public class GameObject
    {
        public string name;
        public Transform transform;

        private List<Behaviour> behaviours = new List<Behaviour>();

        public GameObject(string name,Transform transform){
            this.name = name;
            this.transform = transform;
            SceneManager.currentScene.AddGameObject(this);
        }

        public void Start(){
            for (int i = 0; i < behaviours.Count; i++){
                behaviours[i].Start();
            }
        }

        public void Update()
        {
            for (int i = 0; i < behaviours.Count; i++){
                behaviours[i].Update();
            }
        }

        public void AddComponent(Behaviour component)
        {
            behaviours.Add(component);
        }

        public Behaviour GetBehaviour(Type type)
        {
            return behaviours.Find(x => x.GetType() == type);
        }

    }
}
