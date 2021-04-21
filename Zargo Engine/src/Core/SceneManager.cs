using System;
using System.Collections.Generic;
using System.Linq;
using ZargoEngine.Core;

namespace ZargoEngine
{
    public class SceneManager : NativeSingleton<SceneManager>
    {
        public List<Scene> scenes = new();
        public static Scene currentScene;

        public static void AddScene(Scene scene){
            instance.scenes.Add(scene);
        }

        public static void LoadScene(int index)
        {
            var findedScene = instance.scenes[index];

            if (findedScene == null){
                Console.WriteLine("Scene index doesnt exist");
                return;
            }
            currentScene = findedScene;
            currentScene.Start();
            // do stuff
        }

        public static void LoadScene(string name)
        {
            var findedScene = instance.scenes.Find(x => x.name == name);

            if (findedScene == null){
                Console.WriteLine("scene couldnt finded");
            }
            currentScene = findedScene;
            currentScene.Start();
            // do stuff
        }

        public static string GetUniqeName(string name){
            if (instance.scenes.Any(x => x.name == name)) return "scene" + instance.scenes.Count;
            return name;
        }

        public static string GetName(){
            return "scene" + instance.scenes.Count;
        }

        public static void Dispose()
        {
            currentScene.Dispose();
        }
    }
}