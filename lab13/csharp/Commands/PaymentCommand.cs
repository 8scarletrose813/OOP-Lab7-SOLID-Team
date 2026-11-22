using Lab13.Task2.Interfaces;
using Lab13.Task2.Models;

namespace Lab13.Task2.Commands;

public abstract class PaymentCommand : IPaymentCommand
{
    private static int _lastId;

    protected PaymentCommand(decimal amount, DateOnly scheduledDate, int? repeatEveryDays)
    {
        Id = ++_lastId;
        Amount = amount;
        ScheduledDate = scheduledDate;
        RepeatEveryDays = repeatEveryDays;
    }

    public int Id { get; }
    public decimal Amount { get; }
    public DateOnly ScheduledDate { get; }
    public int? RepeatEveryDays { get; }
    public PaymentStatus Status { get; private set; } = PaymentStatus.Pending;
    public DateOnly? ExecutedOn { get; private set; }

    public abstract string Title { get; }
    public abstract string Description { get; }

    protected abstract void DoExecute();
    protected abstract void DoReverse();
    protected abstract PaymentCommand CreateCopy(DateOnly scheduledDate);

    public void Execute(DateOnly operationDay)
    {
        EnsureStatus(PaymentStatus.Pending, "виконати");

        try
        {
            DoExecute();
        }
        catch (InvalidOperationException)
        {
            Status = PaymentStatus.Failed;
            throw;
        }

        Status = PaymentStatus.Executed;
        ExecutedOn = operationDay;
    }

    public void Reverse()
    {
        EnsureStatus(PaymentStatus.Executed, "сторнувати");
        DoReverse();
        Status = PaymentStatus.Reversed;
    }

    public void Cancel()
    {
        EnsureStatus(PaymentStatus.Pending, "скасувати");
        Status = PaymentStatus.Cancelled;
    }

    public IPaymentCommand? CreateNextOccurrence() =>
        RepeatEveryDays is null ? null : CreateCopy(ScheduledDate.AddDays(RepeatEveryDays.Value));

    private void EnsureStatus(PaymentStatus expected, string action)
    {
        if (Status != expected)
        {
            throw new InvalidOperationException($"неможливо {action} платіж у статусі {Status}");
        }
    }
}
