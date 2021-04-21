
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Diagnostics;

namespace ZargoEngine
{
    public static class Time
    {
        public static float DeltaTime;
        public static float StartTime;
        public static float time = (float)GLFW.GetTime();

        public static float TimeSinceStartUp
        {
            get
            {
                return time - StartTime;
            }
        }

        public static void Start()
        {
            StartTime = (float)GLFW.GetTime();
        }
    }
}
