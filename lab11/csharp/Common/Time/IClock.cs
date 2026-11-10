namespace PrutCreditBank.StructuralPatterns.Common.Time;

public interface IClock
{
    DateTime Now { get; }

    void Advance(TimeSpan interval);
}
