
using Newtonsoft.Json;
using System.IO;

namespace ZargoEngine
{
    public static class JsonManager
    {
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

            string saveTxt = JsonConvert.SerializeObject(obj);

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

            return JsonConvert.DeserializeObject<T>(convertedTxt) as T[];
        }
    }
}
