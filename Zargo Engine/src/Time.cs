
using System.Diagnostics;

namespace ZargoEngine
{
    public static class Time
    {
        public static double DeltaTime;
        public static Stopwatch stopwatch;

        public static void Start()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

    }
}
