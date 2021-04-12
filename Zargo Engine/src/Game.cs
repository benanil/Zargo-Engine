
using MiddleGames.Engine;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using MiddleGames.Engine.Rendering;
using ZargoEngine;
using ZargoEngine.Rendering;
using ZargoEngine.Engine;
using OpenTK.Mathematics;

namespace MiddleGames
{
    public class Game : GameWindow
    {
        private static Game _instance;
        public static Game instance 
        {
            get
            { 
                return _instance;
            }
            private set
            {
                _instance = value;
            }
        }

        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {

        }

        protected override void OnLoad()
        {
            instance = this;

            CursorVisible = false;
            CursorGrabbed = true;

            Time.Start();

            var shader = new Shader("../../Assets/Shaders/BasicVert.hlsl", "../../Assets/Shaders/BasicFrag.hlsl");
            var firstMesh = new Mesh("../../Assets/cube.obj");
            var texture = new Texture("../../Assets/Images/wood_img.jpg");
            var camera = new Camera(new Vector3(-2,1,0), Size.X, Size.Y);

            Scene scene = new(camera,"first Scene");

            // create cubes
            for (int x = 0; x < 10; x++){
                for (int y = 0; y < 10; y++){
                    for (int z = 0; z < 10; z++){
                        var go = new GameObject("go" + x + y + z, new Transform(new Vector3(x,y,z), Quaternion.Identity));
                        scene.AddGameObject(go);
                        scene.AddMeshRenderer(new MeshRenderer(go, shader, firstMesh, texture));
                    }
                }
            }

            SceneManager.AddScene(scene);
            SceneManager.LoadScene(0); // starts first scene 

            GL.ClearColor(.2f, .3f, .4f, 1);

            base.OnLoad();
        }

        protected override void OnUnload()
        {
            AssetManager.DisposeAllShaders();
            AssetManager.DisposeAllTextures();
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            
            base.OnUnload();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            SceneManager.currentScene.Render();

            SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);
            SceneManager.currentScene.camera.ScreenWidth  = e.Width;
            SceneManager.currentScene.camera.ScreenHeight = e.Height;

            base.OnResize(e);
        }
        
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            MainKeyEvents();

            Time.DeltaTime = e.Time;
            SceneManager.currentScene.Update();

            base.OnUpdateFrame(e);
        }

        private void MainKeyEvents()
        { 
            if (IsKeyDown(Keys.Escape)){
                Close();
            }
        }

    }
}
