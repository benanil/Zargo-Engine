

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ZargoEngine.AssetManagement;
using ZargoEngine.Rendering;
using ZargoEngine.Editor;
using Dear_ImGui_Sample;
using ImGuiNET;

namespace ZargoEngine
{
    public class Game : GameWindow
    {
        private ImGuiController _controller;
        private Camera camera;
        private Inspector inspector;
        private GameObject firstObject;

        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings) 
        {

        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(Color4.Bisque);

            LoadScene();

            inspector = new Inspector();
            inspector.currentObject = firstObject;

            _controller = new ImGuiController(ClientSize.X, ClientSize.Y);

            Debug.Log("hour: " + System.DateTime.Now.Hour);
            
            if (System.DateTime.Now.Hour > 16){
                ImGuiController.DarkTheme();
            }
            else{
                ImGui.StyleColorsLight();
            }
        }

        private void LoadScene()
        { 
            camera    = new Camera(new Vector3(0, 0, 1), ClientRectangle.Size.X/ ClientRectangle.Size.Y,-Vector3.UnitZ);
            var scene = new Scene(camera, "first scene");

            var mesh = AssetManager.GetMesh("Models/Atilla.obj");
            //var mesh    = MeshCreator.CreateQuad();
            var shader  = AssetManager.GetShader("Shaders/BasicVert.hlsl", "Shaders/BasicFrag.hlsl");
            var texture = AssetManager.GetTexture("Images/hero texture.png");

            firstObject = new GameObject("first Obj");
            firstObject.transform  = new Transform(firstObject, new Vector3(0, 0, 0), new Vector3(MathHelper.PiOver2, 0, 0));

            firstObject.AddComponent(new FirstBehaviour());

            scene.AddMeshRenderer(new MeshRenderer(mesh, shader, firstObject, ref texture));
            scene.AddGameObject(firstObject);

            SceneManager.AddScene(scene);
            SceneManager.LoadScene(0);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
            
            GL.Enable(EnableCap.DepthTest);
            GL.CullFace(CullFaceMode.Back);
            
            SceneManager.currentScene?.Render();

            EditWindow();

            ImGui.ShowDemoWindow();

            _controller.Render();

            GL.Flush();
            SwapBuffers();
            base.OnRenderFrame(args);
        }

        private void EditWindow()
        {
            inspector.Render();
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
                camera.AspectRatio = ClientRectangle.Size.X / ClientRectangle.Size.Y;
            }
            GL.Viewport(0, 0, e.Width, e.Height);
            base.OnResize(e);
            _controller.WindowResized(e.Width, e.Height);
        }

        private void MainInput()
        {
            CursorVisible = !IsMouseButtonDown(MouseButton.Right);
            if (IsKeyReleased(Keys.Enter) || IsKeyReleased(Keys.KeyPadEnter)) LogGame();
            if (IsKeyPressed(Keys.Escape)) Close();
        }

        private void LogGame()
        {

        }

        protected override void OnClosed()
        {
            SceneManager.Dispose();
            AssetManager.instance.Dispose();
            CursorVisible = true;
            Close();
            base.OnClosed();
        }

    }
    
}
