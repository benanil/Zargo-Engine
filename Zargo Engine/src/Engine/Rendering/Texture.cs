using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace ZargoEngine.Rendering
{
    public class Texture : IDisposable
    {
        private readonly int texId;
        public readonly int width;
        public readonly int height;
        public readonly TextureUnit TexCoord;

        public Texture(string path, TextureUnit TexCoord = TextureUnit.Texture0) // TEXCOORD0 in cgprogram
        {
            this.TexCoord = TexCoord;

            Image<Rgba32> image = Image.Load<Rgba32>(path);

            width = image.Width;
            height = image.Height;

            image.Mutate(x => x.Flip(FlipMode.Vertical));

            int texId = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, texId);

            var pixels = new List<byte>(4 * image.Width * image.Height);
            for (int y = 0; y < image.Height; y++)
            {
                var row = image.GetPixelRowSpan(y);

                for (int x = 0; x < image.Width; x++)
                {
                    pixels.Add(row[x].R);
                    pixels.Add(row[x].G);
                    pixels.Add(row[x].B);
                    pixels.Add(row[x].A);
                }
            }

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels.ToArray());
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }

        public void Bind(){
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, texId);
        }

        public void Unbind(){
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void Dispose()
        {
            GL.DeleteTexture(texId);
            GC.SuppressFinalize(this);
        }
    }
}
