using Lab13.Task2.Interfaces;
using Lab13.Task2.Models;

namespace Lab13.Task2.Services;

public class OperationLog : IOperationLog
{
    private readonly IBankClock _clock;
    private readonly List<LogEntry> _entries = new();

    public OperationLog(IBankClock clock) => _clock = clock;

    public IReadOnlyList<LogEntry> Entries => _entries;

    public void Write(string source, string message)
    {
        var entry = new LogEntry(_clock.Today, source, message);
        _entries.Add(entry);
        Console.WriteLine(entry);
    }
}
