
using System;
using System.Collections.Generic;
using System.Linq;

namespace ZargoEngine
{
    public static class SceneManager
    {
        public static List<Scene> scenes = new List<Scene>();

        private static Scene _currentScene;
        public static Scene currentScene
        {
            get
            {
                if (_currentScene == null){
                    _currentScene = scenes[0];
                }
                return _currentScene;
            }
            set
            {
                _currentScene = value;
            }
        }

        public static void AddScene(Scene scene){
            if (!scenes.Contains(scene)){
                scenes.Add(scene);
            }
        }

        public static void LoadScene(int index){
            var findedScene = scenes[index];
            
            if (findedScene == null){
                Console.WriteLine("Scene index doesnt exist");
                return;
            }
            currentScene = findedScene;
            currentScene.Start();
            // do stuff
        }

        public static void LoadScene(string name){
            var findedScene = scenes.Find(x => x.name == name);

            if (findedScene == null){
                Console.WriteLine("scene couldnt finded");
            }
            currentScene = findedScene;
            currentScene.Start();
            // do stuff
        }

        public static string GetUniqeName(string name){
            if (scenes.Any(x => x.name == name)) return "scene" + scenes.Count;
            return name;
        }

        public static string GetName(){
            return "scene" + scenes.Count;
        }

    }
}
