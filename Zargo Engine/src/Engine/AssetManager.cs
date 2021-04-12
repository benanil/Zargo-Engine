﻿
using MiddleGames.Engine.Rendering;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using ZargoEngine.Rendering;

namespace ZargoEngine.Engine
{
    public static class AssetManager
    {
        private static readonly Dictionary<string,Shader > Shaders  = new Dictionary<string,Shader>();
        private static readonly Dictionary<string,Texture> Textures = new Dictionary<string,Texture>();

        public static Shader GetShader(string vertexPath, string fragmentPath)
        {
            if (Shaders.ContainsKey(fragmentPath)){
                return Shaders[fragmentPath];
            }
            var shader = new Shader(vertexPath, fragmentPath);
            Shaders.Add(vertexPath, shader);
            return shader;
        }

        public static Texture GetTexture(string path,TextureUnit textureUnit)
        {
            if (Textures.ContainsKey(path)){
                if (Textures[path].TexCoord == textureUnit){ // look they have same coord 
                    return new Texture(path);   // if so create new 
                }
                return Textures[path]; //
            }
            var texture = new Texture(path);
            Textures.Add(path,texture);
            return texture;
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