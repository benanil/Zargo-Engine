
using MiddleGames.Engine;
using MiddleGames.Engine.Rendering;
using ZargoEngine.Core;

namespace ZargoEngine.Rendering
{
    public class MeshRenderer : Component
    {
        private readonly int modelID;
        private readonly int viewID;
        private readonly int projectionID;

        public Shader shader;
        public Mesh mesh;
        public Texture texture;

        public MeshRenderer(GameObject gameObject, Shader shader, Mesh mesh, Texture texture) : base(gameObject)
        {
            this.gameObject = gameObject;
            this.shader = shader;
            this.mesh = mesh;
            this.texture = texture;

            modelID      = Shader.PoropertyToId(shader.program, "model");
            viewID       = Shader.PoropertyToId(shader.program, "view");
            projectionID = Shader.PoropertyToId(shader.program, "projection");

            SceneManager.currentScene.AddMeshRenderer(this);
        }

        public void Render(Camera camera)
        {
            shader.Use();
            texture.Bind();

            Shader.SetMatrix4(modelID     , gameObject.transform.Translation);
            Shader.SetMatrix4(viewID      , camera.GetViewMatrix());
            Shader.SetMatrix4(projectionID, camera.GetProjectionMatrix());
            
            mesh.Draw();

            texture.Unbind();
            Shader.Detach();
        }

    }
}
