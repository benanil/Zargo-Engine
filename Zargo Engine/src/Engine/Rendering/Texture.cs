using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;

namespace ZargoEngine.Rendering
{
    public class Texture : IDisposable
    {
        public const SizedInternalFormat Srgb8Alpha8 = (SizedInternalFormat)All.Srgb8Alpha8;
        public const SizedInternalFormat RGB32F = (SizedInternalFormat)All.Rgb32f;
        public const GetPName MAX_TEXTURE_MAX_ANISOTROPY = (GetPName)0x84FF;

        public readonly int texID;

        public readonly int width, height;

        public static readonly float MaxAniso = GL.GetFloat(MAX_TEXTURE_MAX_ANISOTROPY);

        public Texture(string path)
        {
            var pixels = ImageLoader.Load(path, out width, out height,true);

            texID = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, texID);
            
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height,0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels);
            
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            
            GL.GenerateTextureMipmap(texID);
        }

        public Texture(int width, int height){
            texID = GL.GenTexture();
            GL.BindTexture (TextureTarget.Texture2D, texID);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Linear);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, width, height, 0,PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);
        }

        public void Bind(){
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D,texID);
        }

        public static void UnBind()                       => GL.BindTexture(TextureTarget.Texture2D, 0);
        public void SetMinFilter(TextureMinFilter filter) => GL.TextureParameter(texID, TextureParameterName.TextureMinFilter, (int)filter);
        public void SetMagFilter(TextureMagFilter filter) => GL.TextureParameter(texID, TextureParameterName.TextureMagFilter, (int)filter);

        public void SetAnisotropy(float level){
            const TextureParameterName TEXTURE_MAX_ANISOTROPY = (TextureParameterName)0x84FE;
            GL.TextureParameter(texID, TEXTURE_MAX_ANISOTROPY, MathHelper.Clamp(level, 1, MaxAniso));
        }

        public void SetLod(int @base, int min, int max){
            GL.TextureParameter(texID, TextureParameterName.TextureLodBias, @base);
            GL.TextureParameter(texID, TextureParameterName.TextureMinLod, min);
            GL.TextureParameter(texID, TextureParameterName.TextureMaxLod, max);
        }

        public void Dispose()
        {
            GL.DeleteTexture(texID);
            GC.SuppressFinalize(this);
        }
    }
}
