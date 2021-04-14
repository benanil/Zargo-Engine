using MiddleGames;
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
            };

            NativeWindowSettings nativeWindowSettings = new NativeWindowSettings()
            {
                Title = "Zargo Engine",
                Size = new Vector2i(800, 500),
            };

            using Game game = new(gameWindowSettings, nativeWindowSettings);
            game.Run();
	    
        }
    }
}
