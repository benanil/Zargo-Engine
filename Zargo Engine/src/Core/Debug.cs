using System;

namespace ZargoEngine
{
    public static class Debug
    {
        private const string LogString     = "[LOG] ";
        private const string ErrorString   = "[ERROR] ";
        private const string WarningString = "[Warning] ";

        public static void Log(object value)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(LogString + value.ToString());
            Console.ForegroundColor = oldColor;
        }

        public static void LogError(object value)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ErrorString + value.ToString());
            Console.ForegroundColor = oldColor;
        }

        public static void LogWarning(object value)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(WarningString + value.ToString());
            Console.ForegroundColor = oldColor;
        }

        public class SlowDebugger
        {
            private readonly float updateTime;

            private float lastTime;

            public void LogSlow(object value)
            {
                if (lastTime <= 0)
                {
                    lastTime = updateTime;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(LogString + value.ToString());
                }
                lastTime -= Time.DeltaTime;
            }

            public SlowDebugger(float updateTime = .8f)
            {
                this.updateTime = updateTime;
            }
        }
    }
}