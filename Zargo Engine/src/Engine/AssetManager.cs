
using MiddleGames.Engine.Rendering;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using ZargoEngine.Rendering;

namespace ZargoEngine.Engine
{
    public static class AssetManager
    {
        public static Dictionary<string,Shader > Shaders  = new Dictionary<string,Shader>();
        public static Dictionary<string,Texture> Textures = new Dictionary<string,Texture>();

        public static Shader GetShader(string vertexPath, string fragmentPath)
        {
            if (Shaders.ContainsKey(fragmentPath)){
                return Shaders[fragmentPath];
            }

            return new Shader(vertexPath,fragmentPath);
        }

        public static Texture GetTexture(string path,TextureUnit textureUnit)
        {
            if (Textures.ContainsKey(path)){
                if (Textures[path].TexCoord == textureUnit){ // look they have same coord 
                    return new Texture(path);   // if so create new 
                }
                return Textures[path]; //
            }
         
            return new Texture(path);
        }

        public static void DisposeAllShaders()
        {
            foreach (var shaderPair in Shaders)
            {
                shaderPair.Value.Dispose();
            }
        }

        public static void DisposeAllTextures()
        {
            foreach (var texturePair in Textures)
            {
                texturePair.Value.Dispose();
            }
        }
    }
}
