using System;
using System.Collections.Generic;
using ZargoEngine.Rendering;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ZargoEngine.Bindings;
using OpenTK.Windowing.Desktop;
using ZargoEngine.Editor;

namespace ZargoEngine
{
    public class Scene : IDisposable
    {
        public string name;
        public Camera camera;

        public List<GameObject> gameObjects = new List<GameObject>();
        public List<MeshRenderer> meshRenderers = new List<MeshRenderer>();

        private bool started;
        private Vector2 mouseOldPos;
        private readonly float cameraRotateSpeed = 100, cameraMoveSpeed = 3f;

        public Scene(Camera camera, string name)
        {
            this.name = SceneManager.GetUniqeName(name);
            this.camera = camera;
        }

        public Scene(Camera camera)
        {
            this.name = SceneManager.GetName();
            this.camera = camera;
        }

        public void Start()
        {
            mouseOldPos = Input.MousePosition();
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

        public void Update()
        {
            if (!started) return;

            for (int i = 0; i < gameObjects.Count; i++){
                gameObjects[i].Update();
            }

            if (Input.GetKeyDown(Keys.Enter) || Input.GetKeyDown(Keys.KeyPadEnter)){
                LogGame();
            }

            SceneMovement();
        }

        public void LogGame()
        {
            Console.Clear();
            Console.WriteLine("////////////////////////////////");
            Debug.Log("CAMERA");
            Debug.Log("position: " + camera.Position);
            Debug.Log($"pitch:{camera.Pitch}");
            Debug.Log($"pitch:{camera.Yaw}");
            Console.WriteLine('\n');
        }

        private void SceneMovement()
        {
            if (!Input.MouseButtonDown(MouseButton.Right) || (!GameViewWindow.instance.Hovered && !GameViewWindow.instance.Focused)){
                Program.MainGame.CursorVisible = true;
                return;
            }

            Program.MainGame.CursorVisible = false;
            float targetMoveSpeed = Input.GetKey(Keys.LeftShift) ? cameraMoveSpeed * 4 : cameraMoveSpeed;

            if (Input.GetKey(Keys.W)) camera.Position += camera.Front * targetMoveSpeed * Time.DeltaTime;
            if (Input.GetKey(Keys.S)) camera.Position -= camera.Front * targetMoveSpeed * Time.DeltaTime;
            if (Input.GetKey(Keys.A)) camera.Position -= camera.Right * targetMoveSpeed * Time.DeltaTime;
            if (Input.GetKey(Keys.D)) camera.Position += camera.Right * targetMoveSpeed * Time.DeltaTime;
            if (Input.GetKey(Keys.Q)) camera.Position -= camera.Up    * targetMoveSpeed * Time.DeltaTime;
            if (Input.GetKey(Keys.E)) camera.Position += camera.Up    * targetMoveSpeed * Time.DeltaTime;

            if ((Input.MousePosition() - mouseOldPos).Length < 200){
                Vector2 mouseDirection = Input.MousePosition() - mouseOldPos;
                camera.Pitch -= mouseDirection.Y * Time.DeltaTime * cameraRotateSpeed;
                camera.Yaw += mouseDirection.X * Time.DeltaTime * cameraRotateSpeed;
            }

            // infinite mouse
            MouseBindings.GetCursorPos(out MouseBindings.POINT point);

            if (point.X > 1438) MouseBindings.SetCursorPos(2, point.Y);
            if (point.X < 2)    MouseBindings.SetCursorPos(1400, point.Y);
            if (point.Y > 730)  MouseBindings.SetCursorPos(point.X, 2);
            if (point.Y < 2)    MouseBindings.SetCursorPos(point.X, 730);
            // infinite mouse end

            mouseOldPos = Input.MousePosition();
        }

        public void Stop()
        {
            started = false;
        }

        public GameObject FindGameObjectByName(string name)
        {
            return gameObjects.Find(x => x.name == name);
        }

        public GameObject AddGameObject(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
            return gameObject;
        }

        public MeshRenderer AddMeshRenderer(MeshRenderer meshRenderer)
        {
            meshRenderers.Add(meshRenderer);
            return meshRenderer;
        }

        public void Dispose()
        {
            meshRenderers.ForEach(x => x.Dispose());
            GC.SuppressFinalize(this);
        }
    }
}