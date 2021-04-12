
using MiddleGames.Engine;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using MiddleGames.Engine.Rendering;
using ZargoEngine;
using ZargoEngine.Rendering;

namespace MiddleGames
{
    public class Game : GameWindow
    {
        Shader shader;
        Mesh firstMesh;
        Texture texture;
        Camera camera;

        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {

        }

        protected override void OnLoad()
        {
            Time.Start();
            shader = new Shader("../../Assets/Shaders/BasicVert.hlsl", "../../Assets/Shaders/BasicFrag.hlsl");
            firstMesh = new Mesh("../../Assets/cube.obj");
            texture = new Texture("../../Assets/Images/wood_img.jpg");
            camera = new Camera(Camera.CameraRenderMode.Perspective, 90, Size.X, Size.Y);

            GL.ClearColor(.2f, .3f, .4f, 1);

            base.OnLoad();
        }

        protected override void OnUnload()
        {
            shader.Dispose();
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            
            base.OnUnload();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Time.DeltaTime = args.Time;

            texture.Bind();
            shader.Use();

            shader.SetMatrix4("model",camera.mod );

            firstMesh.Draw();

            texture.Unbind();
            shader.Detach();

            SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);
            camera.ViewportWidth = e.Width;
            camera.ViewportWidth = e.Height;
            camera.ReCalculate();

            base.OnResize(e);
        }
        
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (IsKeyDown(Keys.Escape)){
                Close();
            }

            base.OnUpdateFrame(e);
        }
    }
}
