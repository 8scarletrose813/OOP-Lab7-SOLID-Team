using Lab13.Task2.Models;

namespace Lab13.Task2.Commands;

public class UtilityPayment : PaymentCommand
{
    private readonly Account _payer;
    private readonly string _provider;
    private readonly string _service;

    public UtilityPayment(Account payer, string provider, string service, decimal amount, DateOnly scheduledDate, int? repeatEveryDays = null)
        : base(amount, scheduledDate, repeatEveryDays)
    {
        _payer = payer;
        _provider = provider;
        _service = service;
    }

    public override string Title => $"Комунальний платіж #{Id}";
    public override string Description => $"{_payer.Owner} → {_provider} ({_service}), {Amount:F2} грн";

    protected override void DoExecute() => _payer.Withdraw(Amount);

    protected override void DoReverse() => _payer.Deposit(Amount);

    protected override PaymentCommand CreateCopy(DateOnly scheduledDate) =>
        new UtilityPayment(_payer, _provider, _service, Amount, scheduledDate, RepeatEveryDays);
}
