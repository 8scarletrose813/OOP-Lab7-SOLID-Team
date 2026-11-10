namespace PrutCreditBank.StructuralPatterns.Common.Logging;

public interface IEventLog
{
    void Write(string source, string message);
}
