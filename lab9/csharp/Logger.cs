using System.Globalization;

namespace PrutCredit.Task2;

public static class Logger
{
    private static DateTime _now = new(2026, 10, 28, 10, 0, 0);
    private static readonly object Sync = new();

    public static void Log(string source, string message)
    {
        lock (Sync)
        {
            _now = _now.AddSeconds(1);
            Console.WriteLine($"[{_now.ToString("HH:mm:ss", CultureInfo.InvariantCulture)}] [{source}] {message}");
        }
    }
}
