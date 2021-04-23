

namespace ZargoEngine.Editor
{
    public class Inspector : EditorWindow
    {
        public GameObject currentObject;

        public Inspector()
        {
            title = "Inspector";
        }

        public override void OnGUI()
        {
            currentObject?.OnGUI();
        }
    }
}
