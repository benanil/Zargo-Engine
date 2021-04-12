using MiddleGames;
using System;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;

namespace ZargoEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            GameWindowSettings gameWindowSettings = new GameWindowSettings()
            {
                IsMultiThreaded = true,
                UpdateFrequency = 1 / 60,
            };

            NativeWindowSettings nativeWindowSettings = new NativeWindowSettings()
            {
                Title = "Zargo Engine",
                Size = new Vector2i(800, 500),
            };

            using (Game game = new Game(gameWindowSettings,nativeWindowSettings))
            {
                game.Run();
            }
        }
    }
}
