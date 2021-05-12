#define Editor

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ZargoEngine.AssetManagement;
using ZargoEngine.Rendering;
using ZargoEngine.Editor;
using Dear_ImGui_Sample;

namespace ZargoEngine
{
    public class Game : GameWindow
    {
        private ImGuiController _controller;
        private Camera camera;

        private Inspector inspector;
        private Hierarchy hierarchy;
        private GameViewWindow GameViewWindow;
        
        public GameObject firstObject;

        public FrameBuffer frameBuffer;

        private Skybox skybox;

        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings) 
        {

        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(Color4.Gray);

            LoadScene();
            LoadGUI();
        }
        
        private void LoadScene()
        {
            skybox = new Skybox();

            camera    = new Camera(new Vector3(0, 0, 1), ClientRectangle.Size.X/ ClientRectangle.Size.Y,-Vector3.UnitZ);
            var scene = new Scene(camera, "first scene");

            var mesh = AssetManager.GetMesh("Models/Atilla.obj");
            //var mesh    = MeshCreator.CreateQuad();
            var shader  = AssetManager.GetShader("Shaders/BasicVert.glsl", "Shaders/BasicFrag.glsl");
            var texture = AssetManager.GetTexture("Images/hero texture.png");

            firstObject = new GameObject("first Obj");
            firstObject.transform  = new Transform(firstObject, new Vector3(0, 0, 0), new Vector3(MathHelper.PiOver2, 0, 0));

            firstObject.AddComponent(new FirstBehaviour());

            var meshrenderer = new MeshRenderer(mesh, shader, firstObject, ref texture);

            firstObject.AddComponent(meshrenderer);

            scene.AddMeshRenderer(meshrenderer);
            scene.AddGameObject(firstObject);

            scene.AddGameObject(new GameObject("secondObj"));

            SceneManager.AddScene(scene);
            SceneManager.LoadScene(0);
        }
        
        private void LoadGUI()
        { 
            inspector      = new Inspector();
            hierarchy      = new Hierarchy();
            _controller    = new ImGuiController(ClientSize.X, ClientSize.Y);
            GameViewWindow = new GameViewWindow(this);
            frameBuffer    = new FrameBuffer(1440, 900);

            inspector.currentObject = firstObject;

            ImGuiController.DarkTheme();

            /*
            Debug.Log("hour: " + DateTime.Now.Hour);
            if (DateTime.Now.Hour > 16 || DateTime.Now.Hour < 5) ImGuiController.DarkTheme();
            else ImGui.StyleColorsLight();
            */
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            frameBuffer.Bind();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
            
            GL.Enable(EnableCap.DepthTest);
            GL.CullFace(CullFaceMode.Back);
            
            //skybox.Use(camera);
            
            SceneManager.currentScene?.Render();
            
            FrameBuffer.Unbind();

           // EditorWindow();

            _controller.GenerateDockspace(EditorWindow);
            _controller.Render();

            GL.Flush();
            SwapBuffers();
            base.OnRenderFrame(args);
        }

        public void EditorWindow()
        {
            GameViewWindow.Render();
            inspector.DrawGUI();
            hierarchy.DrawGUI();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            Time.DeltaTime = (float)args.Time;

            _controller.Update(this, Time.DeltaTime);

            SceneManager.currentScene.Update();
            MainInput();
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            if (ClientRectangle.Size.X > 0 && ClientRectangle.Size.Y > 0)
            {
                camera.AspectRatio = AspectRatio();
            }
            GL.Viewport(0, 0, e.Width, e.Height);
            base.OnResize(e);
            _controller.WindowResized(e.Width, e.Height);
        }

        private void MainInput()
        {
            if (IsKeyReleased(Keys.Enter) || IsKeyReleased(Keys.KeyPadEnter)) LogGame();
            if (IsKeyPressed(Keys.Escape)) Close();
        }

        public FrameBuffer GetFrameBuffer()
        {
            return frameBuffer;
        }

        private float lastAspectRatio = 16 / 9;

        public float AspectRatio()
        {
#if Editor
            if (GameViewWindow.Scale.X > 0 && GameViewWindow.Scale.Y > 0)
            {
                lastAspectRatio = GameViewWindow.Scale.X / GameViewWindow.Scale.Y;
                //if (GameViewWindow.Scale.X > GameViewWindow.Scale.Y){
                //    lastAspectRatio = GameViewWindow.Scale.X / GameViewWindow.Scale.Y;
                //}
                //else{
                //    lastAspectRatio = GameViewWindow.Scale.Y / GameViewWindow.Scale.X;
                //}
                return lastAspectRatio;
            }
            else
            {
                return lastAspectRatio;
            }
#else
            if (ClientRectangle.Size.X > 0 && ClientRectangle.Size.Y > 0){
                lastAspectRatio = ClientRectangle.Size.X / ClientRectangle.Size.Y;
                return lastAspectRatio;
            }
            else{
                return lastAspectRatio;
            }
#endif
        }

        private void LogGame()
        {

        }

        protected override void OnClosed()
        {
            SceneManager.Dispose();
            AssetManager.instance.Dispose();
            CursorVisible = true;
            base.OnClosed();
        }

    }
    
}
