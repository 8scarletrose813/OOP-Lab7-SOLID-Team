namespace PrutCredit.Lab14.Task1.Common;

public static class EventLog
{
    private static DateTime _time = new(2026, 11, 24, 9, 0, 0);

    public static void Write(string source, string message)
    {
        Console.WriteLine($"[{_time:HH:mm:ss}] [{source}] {message}");
        _time = _time.AddSeconds(15);
    }
}
