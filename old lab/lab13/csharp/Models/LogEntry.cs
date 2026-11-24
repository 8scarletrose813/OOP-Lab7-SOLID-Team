namespace Lab13.Task2.Models;

public record LogEntry(DateOnly Date, string Source, string Message)
{
    public override string ToString() => $"[{Date:dd.MM.yyyy}] [{Source}] {Message}";
}
