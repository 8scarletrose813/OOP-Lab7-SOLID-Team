namespace PrutCreditBank.StructuralPatterns.Common.Time;

public sealed class SimulatedClock(DateTime start) : IClock
{
    private DateTime _now = start;

    public DateTime Now => _now;

    public void Advance(TimeSpan interval) => _now += interval;
}
