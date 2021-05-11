﻿
using OpenTK.Mathematics;
using System.Runtime.CompilerServices;

namespace ZargoEngine.Helper
{
    public static class Extensions
    {
        public static ref Vector2 ToOpenTKRef(ref this System.Numerics.Vector2 from)
            => ref Unsafe.As<System.Numerics.Vector2, Vector2>(ref from);

        public static ref Vector3 ToOpenTKRef(ref this System.Numerics.Vector3 vec3) 
            => ref Unsafe.As<System.Numerics.Vector3, Vector3>(ref vec3);

        public static ref Vector4 ToOpenTKRef(ref this System.Numerics.Vector4 vec4) 
            => ref Unsafe.As<System.Numerics.Vector4, Vector4>(ref vec4);

        public static ref Color4 ToOpenTkColorRef(ref this System.Numerics.Vector4 from) 
            => ref Unsafe.As<System.Numerics.Vector4, Color4>(ref from);

        public static ref Quaternion ToOpenTKRef(ref this System.Numerics.Quaternion quat) 
            => ref Unsafe.As<System.Numerics.Quaternion, Quaternion>(ref quat);

        public static ref System.Numerics.Vector2 ToSystemRef(ref this Vector2 from) 
            => ref Unsafe.As<Vector2,System.Numerics.Vector2>(ref from);

        public static ref System.Numerics.Vector3 ToSystemRef(ref this Vector3 vec3) 
            => ref Unsafe.As<Vector3, System.Numerics.Vector3>(ref vec3);

        public static ref System.Numerics.Vector4 ToSystemRef(ref this Vector4 vec4) 
            => ref Unsafe.As<Vector4, System.Numerics.Vector4>(ref vec4);

        public static ref System.Numerics.Quaternion ToSystemRef(ref this Quaternion quat) 
            => ref Unsafe.As<Quaternion, System.Numerics.Quaternion>(ref quat);

        public static ref System.Numerics.Vector4 ToSystemRef(ref this Color4 vec4) 
            => ref Unsafe.As<Color4, System.Numerics.Vector4>(ref vec4);


        public static Vector2 ToOpenTK(this System.Numerics.Vector2 from)
            => Unsafe.As<System.Numerics.Vector2, Vector2>(ref from);

        public static  Vector3 ToOpenTK(this System.Numerics.Vector3 vec3)
            => Unsafe.As<System.Numerics.Vector3, Vector3>(ref vec3);

        public static Vector4 ToOpenTK(this System.Numerics.Vector4 vec4)
            =>  Unsafe.As<System.Numerics.Vector4, Vector4>(ref vec4);

        public static Color4 ToOpenTkColor(this System.Numerics.Vector4 from)
            => Unsafe.As<System.Numerics.Vector4, Color4>(ref from);

        public static Quaternion ToOpenTK(this System.Numerics.Quaternion quat)
            => Unsafe.As<System.Numerics.Quaternion, Quaternion>(ref quat);

        public static System.Numerics.Vector2 ToSystem(this Vector2 from)
            => Unsafe.As<Vector2, System.Numerics.Vector2>(ref from);

        public static System.Numerics.Vector3 ToSystem(this Vector3 vec3)
            => Unsafe.As<Vector3, System.Numerics.Vector3>(ref vec3);

        public static System.Numerics.Vector4 ToSystem(this Vector4 vec4)
            => Unsafe.As<Vector4, System.Numerics.Vector4>(ref vec4);

        public static System.Numerics.Quaternion ToSystem(this Quaternion quat)
            => Unsafe.As<Quaternion, System.Numerics.Quaternion>(ref quat);

        public static System.Numerics.Vector4 ToSystem(this Color4 vec4)
            => Unsafe.As<Color4, System.Numerics.Vector4>(ref vec4);

        ///     Arrays     ////////////////////////////

        public static Vector2[] ToOpenTk(this System.Numerics.Vector2[] from){
            var temp = new Vector2[from.Length];
            for (int i = 0; i < from.Length; i++) temp[i] = from[i].ToOpenTK();

            return temp;
        }

        public static Vector3[] ToOpenTk(this System.Numerics.Vector3[] from){
            var temp = new Vector3[from.Length];
            for (int i = 0; i < from.Length; i++) temp[i] = from[i].ToOpenTK();
            return temp;
        }

        public static Vector4[] ToOpenTk(this System.Numerics.Vector4[] from){
            var temp = new Vector4[from.Length];
            for (int i = 0; i < from.Length; i++) temp[i] = from[i].ToOpenTK();
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
