
using MiddleGames.Engine.Rendering;
using System.Collections.Generic;
using ZargoEngine.Rendering;

namespace ZargoEngine
{
    public class Scene
    {
        public string name;
        public Camera camera;

        private List<GameObject> gameObjects = new List<GameObject>();
        private List<MeshRenderer> meshRenderers = new List<MeshRenderer>();

        private bool started;

        public Scene(Camera camera,string name){
            this.name = SceneManager.GetUniqeName(name);
            this.camera = camera;
        }

        public Scene(Camera camera){
            this.name = SceneManager.GetName();
            this.camera = camera;
        }

        public void Start(){
            started = true;

            for (int i = 0; i < gameObjects.Count; i++){
                gameObjects[i].Start();
            }
        }

        public void Render()
        {
            if (!started)
                return;

            for (int i = 0; i < meshRenderers.Count; i++){
                meshRenderers[i].Render(camera);
            }
        }

        public void Update(){
            if (!started)
                return;

            for (int i = 0; i < gameObjects.Count; i++){
                gameObjects[i].Update();
            }
        }

        public void Stop(){
            started = false;
        }

        public GameObject FindGameObjectByName(string name){
            return gameObjects.Find(x => x.name == name);
        }

        public GameObject AddGameObject(GameObject gameObject){
            gameObjects.Add(gameObject);
            return gameObject;
        }

        public MeshRenderer AddMeshRenderer(MeshRenderer meshRenderer){
            meshRenderers.Add(meshRenderer);
            return meshRenderer;
        }

    }
}
