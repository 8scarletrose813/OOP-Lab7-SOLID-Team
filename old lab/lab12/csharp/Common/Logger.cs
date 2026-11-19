namespace Lab12_Variant7.Common;

public static class Logger
{
    private static DateTime _time = new(2026, 11, 12, 9, 0, 0);

    public static void Log(string source, string message)
    {
        Console.WriteLine($"[{_time:HH:mm}] [{source}] {message}");
        _time = _time.AddMinutes(1);
    }
}
