using System;

namespace Zargo_Engine
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game(700, 500, GraphicsMode.Default, "Mesh Wiewer"))
            {
                game.Run(60.0);
            }
        }
    }
}
