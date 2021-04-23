
using OpenTK.Windowing.Desktop;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using ZargoEngine.AssetManagement;

namespace ZargoEngine
{
    public static class Program
    {
        public static Game MainGame;

        [STAThread]
        private static void Main(string[] args)
        {
            Image<Rgba32> image = Image.Load<Rgba32>(AssetManager.AssetsPath + "/Images/Engine icon.png");

            //Convert ImageSharp's format into a byte array, so we can use it with OpenGL.
            var pixels = createIcon(ref image);
            
            GameWindowSettings gameWindowSettings = new GameWindowSettings();

            NativeWindowSettings nativeWindowSettings = new NativeWindowSettings(){
                Title = "Zargo Engine",
                Icon = new OpenTK.Windowing.Common.Input.WindowIcon(new OpenTK.Windowing.Common.Input.Image(image.Width,image.Height,pixels))
            };

            using Game game = new(gameWindowSettings, nativeWindowSettings);
            MainGame = game;
            game.Run();
        }

        private static byte[] createIcon(ref Image<Rgba32> image)
        {
            //Convert ImageSharp's format into a byte array, so we can use it with OpenGL.
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
            return pixels.ToArray();
        }
    }
}
