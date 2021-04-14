
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

        private List<GameObject> gameObjects = new();
        private List<MeshRenderer> meshRenderers = new();
        
        private Vector2 lastPos;

        private bool started;
        
        float deltaX,deltaY;

        private const float cameraSens = 10;
        private const float cameraMoveSpeed = 10f;

        private float currentCameraSpeed;

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

            var mousePos = Game.instance.MousePosition;
            lastPos = new Vector2(mousePos.X, mousePos.Y);

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

        private void Input()
        {
            Vector2 mousePos = Game.instance.MousePosition;

            deltaX = mousePos.X - lastPos.X;
            deltaY = mousePos.Y - lastPos.Y;
            lastPos = new Vector2(mousePos.X, mousePos.Y);
            
            if (camera.Pitch > 89.0f)       camera.Pitch = 89.0f;
            else if (camera.Pitch < -89.0f) camera.Pitch = -89.0f;
            else                            camera.Pitch -= deltaX * cameraSens;

            if (camera.Yaw > 89.0f)         camera.Yaw = 89.0f;
            else if (camera.Yaw < -89.0f)   camera.Yaw = -89.0f;
            else                            camera.Yaw -= deltaY * cameraSens;

            currentCameraSpeed = Game.instance.IsKeyDown(Keys.LeftShift) ? cameraMoveSpeed * 2 : cameraMoveSpeed;
            
            if (Game.instance.IsKeyDown(Keys.W)) camera.Position += camera.Front * currentCameraSpeed; //Forward 
            if (Game.instance.IsKeyDown(Keys.S)) camera.Position -= camera.Front * currentCameraSpeed; //Backwards
            if (Game.instance.IsKeyDown(Keys.A)) camera.Position -= Vector3.Normalize(Vector3.Cross(camera.Front, camera.Up)) * currentCameraSpeed; //Left
            if (Game.instance.IsKeyDown(Keys.D)) camera.Position += Vector3.Normalize(Vector3.Cross(camera.Front, camera.Up)) * currentCameraSpeed; //Right
            if (Game.instance.IsKeyDown(Keys.E)) camera.Position += camera.Up * currentCameraSpeed; //Up 
            if (Game.instance.IsKeyDown(Keys.Q)) camera.Position -= camera.Up * currentCameraSpeed; //Down
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
