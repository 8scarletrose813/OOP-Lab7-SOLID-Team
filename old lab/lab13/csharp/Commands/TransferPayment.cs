using Lab13.Task2.Models;

namespace Lab13.Task2.Commands;

public class TransferPayment : PaymentCommand
{
    private readonly Account _from;
    private readonly Account _to;

    public TransferPayment(Account from, Account to, decimal amount, DateOnly scheduledDate, int? repeatEveryDays = null)
        : base(amount, scheduledDate, repeatEveryDays)
    {
        _from = from;
        _to = to;
    }

    public override string Title => $"Переказ #{Id}";
    public override string Description => $"{_from.Owner} → {_to.Owner}, {Amount:F2} грн";

    protected override void DoExecute()
    {
        _from.Withdraw(Amount);
        _to.Deposit(Amount);
    }

    protected override void DoReverse()
    {
        _to.Withdraw(Amount);
        _from.Deposit(Amount);
    }

    protected override PaymentCommand CreateCopy(DateOnly scheduledDate) =>
        new TransferPayment(_from, _to, Amount, scheduledDate, RepeatEveryDays);
}
