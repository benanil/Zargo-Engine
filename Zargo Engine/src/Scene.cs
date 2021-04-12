
namespace ZargoEngine
{
    public class Scene
    {
        public string name;

        public Scene(string name){
            this.name = SceneManager.GetUniqeName(name);
        }

        public Scene(){
            this.name = SceneManager.GetName();
        }

        public void Render()
        { 
            
        }

    }
}
