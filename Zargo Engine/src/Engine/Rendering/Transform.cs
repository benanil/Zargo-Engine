
using ImGuiNET;
using OpenTK.Mathematics;
using ZargoEngine.Helper;
using ZargoEngine.Mathmatics;

namespace ZargoEngine.Rendering
{
    public class Transform : Component
    {
        public GameObject gameObject;

        public Vector3 position = Vector3.Zero;

        private System.Numerics.Vector3 _eulerAngels = new System.Numerics.Vector3(90,0,0);

        private System.Numerics.Vector3 eulerAngels;

        public float scale = 5f;

        public Matrix4 Translation;

        public override void DrawGUI()
        {
            ImGui.TextColored(Color4.Orange.ToSystem(), name);
            SerializeFields();
            ImGui.DragFloat3("Euler Angles",ref _eulerAngels);
            eulerAngels = _eulerAngels.V3DegreToRadian();
            ImGui.Separator();
        }

        public Transform(GameObject gameObject,Vector3 position = new Vector3(), Vector3 rotation = new Vector3(), float scale = 1)
        {
            name = "Transform";
            Translation = GetTranslation();
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
