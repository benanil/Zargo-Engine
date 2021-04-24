using OpenTK.Graphics.OpenGL4;
using System;

namespace ZargoEngine.Rendering
{
    public class CubeMap : IDisposable
    {
        public int TexId;

        public CubeMap(string[] textures)
        {
            TexId = GL.GenTexture();

            GL.BindTexture(TextureTarget.TextureCubeMap, TexId);

            for (int i = 0; i < textures.Length; i++)
            {
                byte[] data = ImageLoader.Load(textures[i], out int width, out int height,true);
                GL.TexImage2D(TextureTarget.TextureCubeMapPositiveX + i, 0 , PixelInternalFormat.Rgba, 
                              width, height, 0, PixelFormat.Rgb, PixelType.Byte, data);
            }

            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
        }

        public void Bind()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.TextureCubeMap, TexId);
        }

        public void Dispose()
        {
            GL.DeleteTexture(TexId);
            GC.SuppressFinalize(this);
        }
    }
}
