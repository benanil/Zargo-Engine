


namespace ZargoEngine.Editor
{
    public class Inspector : EditorWindow
    {
        public IDrawable currentObject;

        public Inspector()
        {
            title = "Inspector";
        }

        public override void OnGUI()
        {
            currentObject?.DrawGUI();
        }
    }
}
