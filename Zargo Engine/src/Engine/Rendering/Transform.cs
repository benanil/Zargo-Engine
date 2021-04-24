
using ImGuiNET;
using OpenTK.Mathematics;
using ZargoEngine.Helper;

namespace ZargoEngine.Rendering
{
    public class Transform : Component
    {
        public GameObject gameObject;

        public Vector3 position = Vector3.Zero;

        private System.Numerics.Vector3 eulerAngels;

        public float scale = 5f;

        public override void DrawGUI()
        {
            base.DrawGUI();

            ImGui.DragFloat3("sdfsdf",ref eulerAngels);
        }

        public Transform(GameObject gameObject,Vector3 position = new Vector3(), Vector3 rotation = new Vector3(), float scale = 1)
        {
            name = "Transform";
            gameObject.AddComponent(this);
            this.gameObject = gameObject;
            this.position = position;
            this.eulerAngels = rotation.ToSystem();
            this.scale = scale;
        }

        public Matrix4 GetTranslation(){
            return Matrix4.Transpose(Matrix4.CreateScale(scale) *
                   Matrix4.CreateTranslation(position)          *
                   Matrix4.CreateRotationX  (eulerAngels.X)     *
                   Matrix4.CreateRotationY  (eulerAngels.Y)     *
                   Matrix4.CreateRotationZ  (eulerAngels.Z));
        }
    }
}
