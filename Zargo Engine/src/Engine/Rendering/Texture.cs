using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;

namespace ZargoEngine.Rendering
{
    public class Texture : IDisposable
    {
        public const SizedInternalFormat Srgb8Alpha8 = (SizedInternalFormat)All.Srgb8Alpha8;
        public const SizedInternalFormat RGB32F = (SizedInternalFormat)All.Rgb32f;
        public const GetPName MAX_TEXTURE_MAX_ANISOTROPY = (GetPName)0x84FF;

        public  readonly int texID;
        private readonly int MipmapLevels;
        private readonly SizedInternalFormat InternalFormat;

        public readonly int width, height;

        public static readonly float MaxAniso = GL.GetFloat(MAX_TEXTURE_MAX_ANISOTROPY);

        public Texture(string path)
        {
            Image<Rgba32> image = Image.Load<Rgba32>(path);

            //ImageSharp loads from the top-left pixel, whereas OpenGL loads from the bottom-left, causing the texture to be flipped vertically.
            //This will correct that, making the texture display properly.
            image.Mutate(x => x.Flip(FlipMode.Vertical));

            //Convert ImageSharp's format into a byte array, so we can use it with OpenGL.
            var pixels = new List<byte>(4 * image.Width * image.Height);

            for (int y = 0; y < image.Height; y++)
            {
                var row = image.GetPixelRowSpan(y);

                for (int x = 0; x < image.Width; x++){
                    pixels.Add(row[x].R);
                    pixels.Add(row[x].G);
                    pixels.Add(row[x].B);
                    pixels.Add(row[x].A);
                }
            }

            width = image.Width;
            height = image.Height;

            texID = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, texID);
            
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height,0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels.ToArray());
            
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            
            GL.GenerateTextureMipmap(texID);
        }

        public Texture(int width, int height, IntPtr data, bool generateMipmaps = false, bool srgb = false)
        {
            this.width  = width;
            this.height = height;

            GL.BindTexture(TextureTarget.Texture2D, texID);

            texID = GL.GenTexture();

            InternalFormat = srgb ? Srgb8Alpha8 : SizedInternalFormat.Rgba8;
            MipmapLevels = generateMipmaps == false ? 1 : (int)Math.Floor(Math.Log(Math.Max(width, height), 2));

            GL.TextureStorage2D(texID, MipmapLevels, InternalFormat, width, height);

            GL.TextureSubImage2D(texID, 0, 0, 0, width, height, PixelFormat.Bgra, PixelType.UnsignedByte, data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            
            if (generateMipmaps) GL.GenerateTextureMipmap(texID);
        }

        public void Bind(){
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D,texID);
        }

        public static void UnBind(){
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void SetMinFilter(TextureMinFilter filter)
        {
            GL.TextureParameter(texID, TextureParameterName.TextureMinFilter, (int)filter);
        }

        public void SetMagFilter(TextureMagFilter filter)
        {
            GL.TextureParameter(texID, TextureParameterName.TextureMagFilter, (int)filter);
        }

        public void SetAnisotropy(float level)
        {
            const TextureParameterName TEXTURE_MAX_ANISOTROPY = (TextureParameterName)0x84FE;
            GL.TextureParameter(texID, TEXTURE_MAX_ANISOTROPY, MathHelper.Clamp(level, 1, MaxAniso));
        }

        public void SetLod(int @base, int min, int max)
        {
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
