using Lab13.Task2.Models;

namespace Lab13.Task2.Interfaces;

public interface IOperationLog
{
    IReadOnlyList<LogEntry> Entries { get; }
    void Write(string source, string message);
}
