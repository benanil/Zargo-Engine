
using OpenTK.Mathematics;
using System;

namespace ZargoEngine.Mathmatic
{
    public static class Mathmatic
    {
        public static Vector3 V3DegreToRadian(this Vector3 from)
        {
            return new Vector3(MathHelper.DegreesToRadians(from.X),
                               MathHelper.DegreesToRadians(from.Y),
                               MathHelper.DegreesToRadians(from.Z));
        }

        public static float Repeat(float t, float length)
        {
            return (float)Math.Clamp(t - Math.Floor(t / length) * length, 0, length);
        }
        
        public static Vector3 V3RadianToDegree(this Vector3 from)
        {
            return new Vector3(MathHelper.RadiansToDegrees(from.X),
                               MathHelper.RadiansToDegrees(from.Y),
                               MathHelper.RadiansToDegrees(from.Z));
        }

    }
}
