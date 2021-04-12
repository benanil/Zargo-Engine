
using MiddleGames;
using MiddleGames.Engine.Rendering;
using System.Collections.Generic;
using ZargoEngine.Rendering;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;

namespace ZargoEngine
{
    public class Scene
    {
        public string name;
        public Camera camera;

        private List<GameObject> gameObjects = new List<GameObject>();
        private List<MeshRenderer> meshRenderers = new List<MeshRenderer>();
        
        private Vector2 lastPos;

        private bool FirstMove;
        private bool started;
        
        private const float speed = 1.5f;

        public Scene(Camera camera, string name) {
            this.name = SceneManager.GetUniqeName(name);
            this.camera = camera;
        }

        public Scene(Camera camera) {
            this.name = SceneManager.GetName();
            this.camera = camera;
        }

        public void Start() {
            started = true;

            for (int i = 0; i < gameObjects.Count; i++) {
                gameObjects[i].Start();
            }
        }

        public void Render()
        {
            if (!started)
                return;

            for (int i = 0; i < meshRenderers.Count; i++) {
                meshRenderers[i].Render(camera);
            }
        }

        public void Update() {
            if (!started)
                return;

            for (int i = 0; i < gameObjects.Count; i++) {
                gameObjects[i].Update();
            }

            Input();
        }

        float deltaX,deltaY;

        private const float cameraSens = 10;

        private void Input()
        {
            Vector2 mousePos = Game.instance.MousePosition;


            if (FirstMove){
                lastPos = new Vector2(mousePos.X, mousePos.Y);
                FirstMove = true;
            }
            else{
                deltaX = mousePos.X - lastPos.X;
                deltaY = mousePos.Y - lastPos.Y;
                lastPos = new Vector2(mousePos.X, mousePos.Y);
            }
            
            if (camera.Pitch > 89.0f)       camera.Pitch = 89.0f;
            else if (camera.Pitch < -89.0f) camera.Pitch = -89.0f;
            else                            camera.Pitch -= deltaX * cameraSens; 
            

            if (Game.instance.IsKeyDown(Keys.W))         camera.Position += camera.Front * speed; //Forward 
            if (Game.instance.IsKeyDown(Keys.S))         camera.Position -= camera.Front * speed; //Backwards
            if (Game.instance.IsKeyDown(Keys.A))         camera.Position -= Vector3.Normalize(Vector3.Cross(camera.Front, camera.Up)) * speed; //Left
            if (Game.instance.IsKeyDown(Keys.D))         camera.Position += Vector3.Normalize(Vector3.Cross(camera.Front, camera.Up)) * speed; //Right
            if (Game.instance.IsKeyDown(Keys.Space))     camera.Position += camera.Up * speed; //Up 
            if (Game.instance.IsKeyDown(Keys.LeftShift)) camera.Position -= camera.Up * speed; //Down
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
