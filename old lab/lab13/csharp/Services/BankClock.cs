using Lab13.Task2.Interfaces;

namespace Lab13.Task2.Services;

public class BankClock : IBankClock
{
    public BankClock(DateOnly today) => Today = today;

    public DateOnly Today { get; private set; }

    public void AdvanceTo(DateOnly date)
    {
        if (date < Today)
        {
            throw new ArgumentException("Не можна повернутися в минуле");
        }

        Today = date;
    }
}
