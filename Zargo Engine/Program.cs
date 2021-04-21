
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
            using Game game = new(GameWindowSettings.Default, NativeWindowSettings.Default);
            MainGame = game;
            game.Run();
        }
    }
}
