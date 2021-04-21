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
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(LogString + value.ToString());
        }

        public static void LogError(object value)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ErrorString + value.ToString());
        }

        public static void LogWarning(object value)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(WarningString + value.ToString());
        }
    }
}