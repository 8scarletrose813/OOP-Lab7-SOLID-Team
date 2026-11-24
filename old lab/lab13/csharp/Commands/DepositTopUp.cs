using Lab13.Task2.Models;

namespace Lab13.Task2.Commands;

public class DepositTopUp : PaymentCommand
{
    private readonly Account _account;
    private readonly Account _deposit;

    public DepositTopUp(Account account, Account deposit, decimal amount, DateOnly scheduledDate, int? repeatEveryDays = null)
        : base(amount, scheduledDate, repeatEveryDays)
    {
        _account = account;
        _deposit = deposit;
    }

    public override string Title => $"Поповнення депозиту #{Id}";
    public override string Description => $"{_account.Owner} → {_deposit.Owner}, {Amount:F2} грн";

    protected override void DoExecute()
    {
        _account.Withdraw(Amount);
        _deposit.Deposit(Amount);
    }

    protected override void DoReverse()
    {
        _deposit.Withdraw(Amount);
        _account.Deposit(Amount);
    }

    protected override PaymentCommand CreateCopy(DateOnly scheduledDate) =>
        new DepositTopUp(_account, _deposit, Amount, scheduledDate, RepeatEveryDays);
}
