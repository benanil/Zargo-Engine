
using OpenTK.Windowing.Desktop;
using System;

namespace ZargoEngine
{
    public static class Program
    {
        public static Game MainGame;

        [STAThread]
        private static void Main(string[] args)
        {
            //Convert ImageSharp's format into a byte array, so we can use it with OpenGL.
            var pixels = ImageLoader.Load("/Images/Engine icon.png",out int width,out int height);
            
            GameWindowSettings gameWindowSettings = new GameWindowSettings();

            NativeWindowSettings nativeWindowSettings = new NativeWindowSettings(){
                Title = "Zargo Engine",
                Icon = new OpenTK.Windowing.Common.Input.WindowIcon(new OpenTK.Windowing.Common.Input.Image(width,height,pixels))
            };

            using Game game = new(gameWindowSettings, nativeWindowSettings);
            MainGame = game;
            game.Run();
        }
    }
}
