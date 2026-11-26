namespace BankIterator.Services;

public sealed class EventLog
{
    private DateTime _clock;

    public EventLog(DateTime start) => _clock = start;

    public void Write(string source, string message)
    {
        _clock = _clock.AddSeconds(1);
        Console.WriteLine($"[{_clock:HH:mm:ss}] [{source}] {message}");
    }
}
