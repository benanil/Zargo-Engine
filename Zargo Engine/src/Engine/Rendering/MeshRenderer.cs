
using MiddleGames.Engine;
using MiddleGames.Engine.Rendering;
using ZargoEngine.Core;

namespace ZargoEngine.Rendering
{
    public class MeshRenderer : Component
    {
        public Shader shader;
        public Mesh mesh;
        public Texture texture;

        public MeshRenderer(GameObject gameObject, Shader shader, Mesh mesh, Texture texture) : base(gameObject)
        {
            this.gameObject = gameObject;
            this.shader = shader;
            this.mesh = mesh;
            this.texture = texture;

            SceneManager.currentScene.AddMeshRenderer(this);
        }

        public void Render(Camera camera)
        {
            texture.Bind();
            shader.Use();

            shader.SetMatrix4("model",gameObject.transform.Translation);
            shader.SetMatrix4("View", camera.WorldView);
            shader.SetMatrix4("Projection", camera.Projection);
            
            mesh.Draw();

            texture.Unbind();
            shader.Detach();
        }

    }
}
