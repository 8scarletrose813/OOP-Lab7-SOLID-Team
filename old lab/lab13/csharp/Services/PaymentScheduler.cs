using Lab13.Task2.Interfaces;
using Lab13.Task2.Models;

namespace Lab13.Task2.Services;

public class PaymentScheduler
{
    private readonly List<IPaymentCommand> _queue = new();
    private readonly List<IPaymentCommand> _executed = new();
    private readonly IBankClock _clock;
    private readonly IOperationLog _log;

    public PaymentScheduler(IBankClock clock, IOperationLog log)
    {
        _clock = clock;
        _log = log;
    }

    public IReadOnlyList<IPaymentCommand> Queue =>
        _queue.OrderBy(p => p.ScheduledDate).ThenBy(p => p.Id).ToList();

    public void Schedule(IPaymentCommand payment)
    {
        _queue.Add(payment);
        _log.Write("Scheduler", $"{payment.Title} поставлено в чергу на {payment.ScheduledDate:dd.MM.yyyy}: {payment.Description}");
    }

    public void Cancel(int paymentId)
    {
        var payment = _queue.FirstOrDefault(p => p.Id == paymentId);
        if (payment is null)
        {
            _log.Write("Scheduler", $"скасування платежу #{paymentId} неможливе: його немає в черзі");
            return;
        }

        payment.Cancel();
        _queue.Remove(payment);
        _log.Write(payment.Title, $"скасовано до виконання: {payment.Description}");
    }

    public void ProcessDue()
    {
        var today = _clock.Today;
        var due = Queue.Where(p => p.ScheduledDate <= today).ToList();

        if (due.Count == 0)
        {
            _log.Write("Scheduler", "платежів до виконання немає");
            return;
        }

        foreach (var payment in due)
        {
            _queue.Remove(payment);

            try
            {
                payment.Execute(today);
                _executed.Add(payment);
                _log.Write(payment.Title, $"виконано: {payment.Description}");

                var next = payment.CreateNextOccurrence();
                if (next is not null)
                {
                    Schedule(next);
                }
            }
            catch (InvalidOperationException ex)
            {
                _log.Write(payment.Title, $"не виконано: {ex.Message}");
            }
        }
    }

    public void Reverse(int paymentId)
    {
        var payment = _executed.FirstOrDefault(p => p.Id == paymentId && p.Status == PaymentStatus.Executed);
        if (payment is null)
        {
            _log.Write("Scheduler", $"сторнування платежу #{paymentId} неможливе: виконаного платежу з таким номером немає");
            return;
        }

        if (payment.ExecutedOn != _clock.Today)
        {
            _log.Write(payment.Title, $"сторнування відхилено: операційний день {payment.ExecutedOn:dd.MM.yyyy} закрито");
            return;
        }

        try
        {
            payment.Reverse();
            _log.Write(payment.Title, $"сторновано: {payment.Description}");
        }
        catch (InvalidOperationException ex)
        {
            _log.Write(payment.Title, $"сторнування не вдалося: {ex.Message}");
        }
    }
}
