
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ZargoEngine
{
    public static class JsonManager
    {
        [Serializable]
        public struct ArrayHolder<T>
        {
            public T[] Data;

            public ArrayHolder(T[] data){
                Data = data;
            }
        }

        public static void EnsurePath(string suredPath,string targetPath)
        {
            string currentPath = suredPath;

            Queue<string> targetPaths = new Queue<string>(targetPath.Split('/'));
            currentPath += targetPaths.Dequeue();

            while (!Path.HasExtension(currentPath))
            {
                Directory.CreateDirectory(currentPath);
                currentPath += targetPaths.Dequeue();
            }
        }

        public static void Save<T>(this T obj, string file)
        {
            if (!File.Exists(file)){
                Debug.Log("path directory doesnt exist");
                return;
            }

            string saveTxt = JsonConvert.SerializeObject(obj);

            using StreamWriter writer = new StreamWriter(file);
            writer.Write(saveTxt);
        }

        public static void SaveArray<T>(this T[] obj, string file)
        {
            if (!File.Exists(file)){
                Debug.Log("path directory doesnt exist");
                return;
            }

            var convertedObj = new ArrayHolder<T>(obj);
            string saveTxt = JsonConvert.SerializeObject(convertedObj);

            using StreamWriter writer = new StreamWriter(file);
            writer.Write(saveTxt);
        }

        public static T Load<T>(string file) where T : class
        { 
            if (!File.Exists(file))
            {
                Debug.Log("path directory doesnt exist");
                return default;
            }

            using StreamReader reader = new StreamReader(file);
            string convertedTxt = reader.ReadToEnd();

            return JsonConvert.DeserializeObject<T>(convertedTxt);
        }

        public static T[] LoadArray<T>(string file) where T : class 
        {
            if (!File.Exists(file)){
                Debug.Log("path directory doesnt exist");
                return null;
            }

            using StreamReader reader = new StreamReader(file);
            string convertedTxt = reader.ReadToEnd();

            return JsonConvert.DeserializeObject<ArrayHolder<T>>(convertedTxt).Data;
        }
    }
}
