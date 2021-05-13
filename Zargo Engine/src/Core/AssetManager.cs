
using System;
using System.Collections.Generic;
using System.IO;
using ZargoEngine.Core;
using ZargoEngine.Rendering;

namespace ZargoEngine.AssetManagement
{
    public class AssetManager : NativeSingleton<AssetManager> , IDisposable
    {
        public const string AssetsPath = "../../../Assets/";

        private readonly Dictionary<string, Shader>  shaders  = new();
        private readonly Dictionary<string, Mesh>    meshes   = new();
        private readonly Dictionary<string, Texture> textures = new();

        public static Shader GetShader(string vertexPath, string fragmentPath)
        {
            string realvertexPath   = AssetsPath + vertexPath;
            string realFragmentPath = AssetsPath + fragmentPath;

            if (instance.shaders.TryGetValue(realvertexPath,out Shader shader)) return shader;

            shader = new Shader(realvertexPath,realFragmentPath);

            instance.shaders.Add(vertexPath,shader);

            return shader;
        }

        public static Texture GetTexture(string path)
        {
            if (instance.textures.TryGetValue(path, out Texture texture)) return texture;

            texture = new Texture(path);

            instance.textures.Add(path, texture);

            return texture;
        }

        public static Mesh GetMesh(string path)
        {
            string realPath = AssetsPath + path;

            if (instance.meshes.TryGetValue(path, out Mesh mesh)) return mesh;
            
            mesh = new Mesh(realPath);

            instance.meshes.Add(path, mesh);

            return mesh;
        }

        public static bool GetFileLocation(ref string path)
        {
            path = AssetsPath + path;
            string realFile = Directory.GetCurrentDirectory() + "\\" + path;
            if (!File.Exists(realFile)){
                Debug.LogError("sound file is not exist:");
                Debug.LogError(realFile);
                return false;
            }
            return true;
        }

        public static string GetFileLocation(string path)
        {
            path = AssetsPath + path;
            string realFile = Directory.GetCurrentDirectory() + "\\" + path;
            if (!File.Exists(realFile))
            {
                Debug.LogError("sound file is not exist:");
                Debug.LogError(realFile);
                return null;
            }
            return path;
        }

        public void Dispose()
        {
            foreach (var meshPair    in meshes)      meshPair.Value.Dispose();
            foreach (var texturePair in textures) texturePair.Value.Dispose();
            foreach (var shaderPair  in shaders)   shaderPair.Value.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
