using ImGuiNET;
using System;

namespace ZargoEngine.Rendering
{
    public class MeshRenderer : Component, IDisposable
    {
        private readonly int model;
        private readonly int view;
        private readonly int projection;
        private readonly int worldSpacePosition;

        private const string texture0 = nameof(texture0);

        public Shader shader;
        public Mesh mesh;
        public Texture texture;

        public readonly Transform transform;
        public readonly GameObject gameObject;

        public MeshRenderer(Mesh mesh, Shader shader,GameObject gameObject, ref Texture texture)
        {
            name = "Mesh Renderer";
            this.mesh       = mesh;
            this.shader     = shader;
            this.transform  = gameObject.transform;
            this.texture    = texture;
            this.gameObject = gameObject;

            gameObject.AddComponent(this);

            projection = shader.GetUniformLocation("projection");
            view       = shader.GetUniformLocation("view");
            model      = shader.GetUniformLocation("model");
            worldSpacePosition = shader.GetUniformLocation("worldSpacePosition");
        }

        public override void DrawGUI()
        {
            // comining
        }

        public void Render(in Camera camera)
        {
            shader.Use();
            shader.SetInt(texture0, 0);
            
            texture?.Bind();

            Shader.SetMatrix4(model,      transform.GetTranslation(), false);
            Shader.SetMatrix4(projection, camera.GetProjectionMatrix());
            Shader.SetMatrix4(view      , camera.GetViewMatrix());
            Shader.SetVector3(worldSpacePosition, transform.position);

            mesh.Draw();

            Shader.DetachShader();
            Texture.UnBind();
        }

        public void Dispose()
        {
            mesh.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
