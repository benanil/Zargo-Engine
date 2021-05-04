
using OpenTK.Mathematics;

namespace ZargoEngine.Helper
{
    public static class Extensions
    {
        public static Vector2 ToOpenTk(this System.Numerics.Vector2 from){
            return new Vector2(from.X, from.Y);
        }

        public static Vector3 ToOpenTk(this System.Numerics.Vector3 from){
            return new Vector3(from.X,from.Y,from.Z);
        }

        public static Vector4 ToOpenTk(this System.Numerics.Vector4 from){
            return new Vector4(from.X, from.Y, from.Z, from.W);
        }
        
        public static Color4 ToOpenTkColor(this System.Numerics.Vector4 from){
            return new Color4(from.X, from.Y, from.Z,from.W);
        }

        public static System.Numerics.Vector2 ToSystem(this Vector2 from){
            return new System.Numerics.Vector2(from.X, from.Y);
        }

        public static System.Numerics.Vector3 ToSystem(this Vector3 from){
            return new System.Numerics.Vector3(from.X, from.Y, from.Z);
        }

        public static System.Numerics.Vector4 ToSystem(this Color4 from){
            return new System.Numerics.Vector4(from.R, from.G, from.B, from.A);
        }
        
        ///     Arrays     ////////////////////////////
        
        public static Vector2[] ToOpenTk(this System.Numerics.Vector2[] from){
            var temp = new Vector2[from.Length];
            for (int i = 0; i < from.Length; i++) temp[i] = from[i].ToOpenTk();
            return temp;
        }

        public static Vector3[] ToOpenTk(this System.Numerics.Vector3[] from){
            var temp = new Vector3[from.Length];
            for (int i = 0; i < from.Length; i++) temp[i] = from[i].ToOpenTk();
            return temp;
        }

        public static Vector4[] ToOpenTk(this System.Numerics.Vector4[] from){
            var temp = new Vector4[from.Length];
            for (int i = 0; i < from.Length; i++) temp[i] = from[i].ToOpenTk();
            return temp;
        }

        ////////

        public static System.Numerics.Vector2[] ToSystem(this Vector2[] from)
        {
            var temp = new System.Numerics.Vector2[from.Length];
            for (int i = 0; i < from.Length; i++) temp[i] = from[i].ToSystem();
            return temp;
        }

        public static System.Numerics.Vector3[] ToSystem(this Vector3[] from)
        {
            var temp = new System.Numerics.Vector3[from.Length];
            for (int i = 0; i < from.Length; i++) temp[i] = from[i].ToSystem();
            return temp;
        }

        public static System.Numerics.Vector4[] ToSystem(this Color4[] from)
        {
            var temp = new System.Numerics.Vector4[from.Length];
            for (int i = 0; i < from.Length; i++) temp[i] = from[i].ToSystem();
            return temp;
        }

        /// Arrays end //////////////////////////
    }
}
