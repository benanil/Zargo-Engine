
using OpenTK.Windowing.Desktop;
using System.Diagnostics;

namespace ZargoEngine
{
    public static class Program
    {
        public static Game MainGame;

        private static void Main(string[] args)
        {
            //Convert ImageSharp's format into a byte array, so we can use it with OpenGL.
            var pixels = ImageLoader.Load("/Images/Engine icon.png",out int width,out int height);

            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

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
