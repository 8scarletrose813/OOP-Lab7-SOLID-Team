using Lab13.Task2.Models;

namespace Lab13.Task2.Interfaces;

public interface IPaymentCommand
{
    int Id { get; }
    string Title { get; }
    string Description { get; }
    decimal Amount { get; }
    DateOnly ScheduledDate { get; }
    int? RepeatEveryDays { get; }
    PaymentStatus Status { get; }
    DateOnly? ExecutedOn { get; }

    void Execute(DateOnly operationDay);
    void Reverse();
    void Cancel();
    IPaymentCommand? CreateNextOccurrence();
}
