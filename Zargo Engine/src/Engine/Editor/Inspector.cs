


using ImGuiNET;
using ZargoEngine.Rendering;

namespace ZargoEngine.Editor
{
    public class Inspector : EditorWindow
    {
        public IDrawable currentObject;

        public static Inspector instance;

        public Inspector()
        {
            instance = this;
            title = "Inspector";
        }

        public override void OnGUI()
        {
            currentObject?.DrawGUI();
        }
    }
}
