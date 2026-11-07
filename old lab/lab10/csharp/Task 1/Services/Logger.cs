using System;

namespace PrutCreditBank.Task1.Services
{
    public static class Logger
    {
        public static void Log(string source, string message)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] [{source}] {message}");
        }
    }
}