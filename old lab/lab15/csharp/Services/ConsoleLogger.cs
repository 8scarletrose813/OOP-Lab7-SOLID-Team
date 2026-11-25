using PrutCredit.Interfaces;

namespace PrutCredit.Services;

public class ConsoleLogger : ILogger
{
    private DateTime _now = new(2026, 11, 24, 9, 0, 0);

    public void Log(string source, string message)
    {
        Console.WriteLine($"[{_now:HH:mm:ss}] [{source}] {message}");
        _now = _now.AddSeconds(20);
    }
}
