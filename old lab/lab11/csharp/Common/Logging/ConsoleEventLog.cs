using PrutCreditBank.StructuralPatterns.Common.Time;

namespace PrutCreditBank.StructuralPatterns.Common.Logging;

public sealed class ConsoleEventLog(IClock clock) : IEventLog
{
    public void Write(string source, string message)
    {
        Console.WriteLine($"[{clock.Now:HH:mm:ss}] [{source}] {message}");
        clock.Advance(TimeSpan.FromSeconds(1));
    }
}
