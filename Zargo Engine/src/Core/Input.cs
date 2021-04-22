
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace ZargoEngine
{
    public static partial class Input
    {
        private static float horizontal;
        private static float vertical;

        public static Vector2 mouseWheelOffset;

        public static float Horizontal(){
            float target = 0;
            if (GetKey(Keys.A) || GetKey(Keys.Left))  target += 1;
            if (GetKey(Keys.D) || GetKey(Keys.Right)) target -= 1;
            
            horizontal = MathHelper.Lerp(horizontal, target, Time.DeltaTime * 50);

            return horizontal;
        }

        public static float Vertical(){
            float target = 0;
            if (GetKey(Keys.W) || GetKey(Keys.Up))   target += 1;
            if (GetKey(Keys.S) || GetKey(Keys.Down)) target -= 1;

            vertical = MathHelper.Lerp(vertical, target, Time.DeltaTime * 50);

            return vertical;
        }

        public static Vector2 KeyAxis()         => new (Horizontal(), Vertical()); 
        
        public static bool GetKeyDown(Keys key) => Program.MainGame.IsKeyPressed(key);
        public static bool GetKey(Keys key)     => Program.MainGame.IsKeyDown(key);
        public static bool GetKeyUp(Keys key)   => Program.MainGame.IsKeyReleased(key);
    }
}
