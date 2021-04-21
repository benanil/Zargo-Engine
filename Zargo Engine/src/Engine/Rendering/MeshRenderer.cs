using System;

namespace ZargoEngine.Rendering
{
    public class MeshRenderer : IDisposable
    {
        private readonly int model;
        private readonly int view;
        private readonly int projection;
        private const string texture0 = nameof(texture0);

        public Shader shader;
        public Mesh mesh;
        public Texture texture;

        readonly Transform transform;

        public MeshRenderer(Mesh mesh, Shader shader,ref Transform transform, ref Texture texture)
        {
            this.mesh = mesh;
            this.shader = shader;
            this.transform = transform;
            this.texture = texture;

            projection = shader.GetUniformLocation("projection");
            view       = shader.GetUniformLocation("view");
            model      = shader.GetUniformLocation("model");
        }

        public void Render(in Camera camera)
        {
            shader.Use();
            shader.SetInt(texture0, 0);
            texture.Bind();

            Shader.SetMatrix4(model,      transform.GetTranslation(), false);
            Shader.SetMatrix4(projection, camera.GetProjectionMatrix());
            Shader.SetMatrix4(view      , camera.GetViewMatrix());

            mesh.Draw();

            Shader.DetachShader();
            Texture.UnBind();
        }

        public void Dispose()
        {
            shader.Dispose();
            mesh.Dispose();
            texture.Dispose();
        }
    }
}
