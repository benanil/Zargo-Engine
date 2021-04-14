
using MiddleGames.Engine;
using MiddleGames.Engine.Rendering;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using ZargoEngine.Rendering;

namespace ZargoEngine.Engine
{
    public static class AssetManager
    {
        private const string AssetsPath = "../../../Assets/";

        private static readonly Dictionary<string, Shader > Shaders  = new();
        private static readonly Dictionary<string, Texture> Textures = new();
        private static readonly Dictionary<string, Mesh> Meshes      = new();

        public static Shader GetShader(string vertexPath, string fragmentPath)
        {
            if (Shaders.ContainsKey(fragmentPath)){
                return Shaders[fragmentPath];
            }
            var shader = new Shader(AssetsPath + vertexPath, AssetsPath + fragmentPath);
            Shaders.Add(vertexPath, shader);
            return shader;
        }

        public static Texture GetTexture(string path, TextureUnit textureUnit)
        {
            if (Textures.ContainsKey(path)){
                if (Textures[path].TexCoord == textureUnit){ // look they have same coord 
                    return new Texture(AssetsPath + path);   // if so create new 
                }
                return Textures[path]; //
            }
            var texture = new Texture(AssetsPath + path);
            Textures.Add(path,texture);
            return texture;
        }

        public static Mesh LoadMesh(string path)
        {
            if (Meshes.ContainsKey(path)){
                return Meshes[path];
            }

            var mesh = new Mesh(AssetsPath + path);
            Meshes.Add(path, mesh);
            return mesh;
        }

        public static void DisposeAllShaders()
        {
            foreach (var shaderPair in Shaders)  shaderPair.Value.Dispose();
        }

        public static void DisposeAllTextures()
        {
            foreach (var texturePair in Textures) texturePair.Value.Dispose();
        }
    }
}
