


using ZargoEngine.Rendering;

namespace ZargoEngine.Editor
{
    public class Inspector : EditorWindow
    {
        public Component currentObject;

        public Inspector()
        {
            title = "Inspector";
        }

        public override void OnGUI()
        {
            switch (currentObject)
            {
                case GameObject go:
                    go.DrawGUI();
                    break;
                case MeshRenderer meshRenderer:
                    meshRenderer.DrawGUI();
                    break;
                case DirectionalLight directionalLight:
                    directionalLight.DrawGUI();
                    break;
            }
        }
    }
}
