
using System;
using System.Collections.Generic;
using ZargoEngine.Core;
using ZargoEngine.Rendering;

namespace ZargoEngine.AssetManagement
{
    public class AssetManager : NativeSingleton<AssetManager> , IDisposable
    {
        public const string AssetsPath = "../../../Assets/";

        private Dictionary<string, Shader>  shaders  = new();
        private Dictionary<string, Mesh>    meshes   = new();
        private Dictionary<string, Texture> textures = new();

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
            string realPath = AssetsPath + path;

            if (instance.textures.TryGetValue(realPath, out Texture texture)) return texture;

            texture = new Texture(realPath);

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

        public void Dispose()
        {
            foreach (var meshPair    in meshes)      meshPair.Value.Dispose();
            foreach (var texturePair in textures) texturePair.Value.Dispose();
            foreach (var shaderPair  in shaders)   shaderPair.Value.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
