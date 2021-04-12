
using System;
using System.Collections.Generic;
using System.Linq;

namespace ZargoEngine
{
    public static class SceneManager
    {
        public static List<Scene> scenes = new List<Scene>();
        public static Scene currentScene;

        public static void AddScene(Scene scene)
        {
            scenes.Add(scene);
        }

        public static void LoadScecene(string name)
        {
            // do stuff

            var findedScene = scenes.Find(x => x.name == name);

            if (findedScene != null){
                currentScene = findedScene;
            }
            else{
                Console.WriteLine("scene couldnt finded");
            }
        }

        public static string GetUniqeName(string name)
        {
            if (scenes.Any(x => x.name == name)) return "scene" + scenes.Count;

            return name;
        }

        public static string GetName()
        {
            return "scene" + scenes.Count;
        }

    }
}
