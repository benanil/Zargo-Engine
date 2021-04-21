

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Collections.Generic;
using ZargoEngine.AssetManagement;
using ZargoEngine.Rendering;

namespace ZargoEngine
{
    public class Game : GameWindow
    {
        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {

        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(Color4.Bisque);
            
            CursorVisible = false;

            Camera camera    = new Camera(new Vector3(0, 0, 1), ClientRectangle.Size.X/ ClientRectangle.Size.Y,-Vector3.UnitZ);
            var scene = new Scene(camera, "first scene");

            var mesh = AssetManager.GetMesh("Models/cube.obj");
            //var mesh    = MeshCreator.CreateCube();
            var shader  = AssetManager.GetShader("Shaders/BasicVert.hlsl", "Shaders/BasicFrag.hlsl");
            var texture = AssetManager.GetTexture("Images/wood_img.jpg");

            const float distanceBetweenCubes = 5f;

            for (int x = 0; x < 10; x++)
                for (int y = 0; y < 10; y++)
                    for (int z = 0; z < 10; z++)
                        scene.AddMeshRenderer(new MeshRenderer(mesh, shader, new Transform(new Vector3(x * distanceBetweenCubes, y * distanceBetweenCubes, z * distanceBetweenCubes)), ref texture));

            SceneManager.AddScene(scene);
            SceneManager.LoadScene(0);
            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.CullFace(CullFaceMode.Back);

            SceneManager.currentScene?.Render();

            GL.Flush();
            SwapBuffers();
            base.OnRenderFrame(args);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            Time.DeltaTime = (float)args.Time;

            SceneManager.currentScene.Update();
            Input();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            //camera.AspectRatio = ClientRectangle.Size.X / ClientRectangle.Size.Y;
            GL.Viewport(0, 0, e.Width, e.Height);
            base.OnResize(e);
        }

        private void Input()
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
