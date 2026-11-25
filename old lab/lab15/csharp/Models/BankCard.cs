using PrutCredit.Interfaces;

namespace PrutCredit.Models;

public class BankCard : BankProduct
{
    public const int MaxPinAttempts = 3;

    private readonly string _pin;
    private int _failedPinAttempts;

    public BankCard(
        string id,
        Client owner,
        string pin,
        decimal balance,
        decimal overdraft,
        ILogger logger,
        ICardState initialState)
        : base(id, owner)
    {
        _pin = pin;
        Balance = balance;
        Overdraft = overdraft;
        Logger = logger;
        State = initialState;
    }

    public ICardState State { get; private set; }

    public decimal Balance { get; private set; }

    public decimal Overdraft { get; }

    public override string DisplayName => "Картка";

    public override decimal Debt => Overdraft;

    internal ILogger Logger { get; }

    public void Activate() => State.Activate(this);

    public void Pay(decimal amount, string pin) => State.Pay(this, amount, pin);

    public void Unblock() => State.Unblock(this);

    public void BlockPermanently() => State.BlockPermanently(this);

    public void Reissue() => State.Reissue(this);

    public void Expire() => State.Expire(this);

    internal void SetState(ICardState next)
    {
        Logger.Log($"BankCard {Id}", $"Перехід стану: «{State.Name}» → «{next.Name}»");
        State = next;
    }

    internal bool VerifyPin(string pin) => _pin == pin;

    internal int RegisterFailedPin() => ++_failedPinAttempts;

    internal void ResetPinAttempts() => _failedPinAttempts = 0;

    internal bool TryDebit(decimal amount)
    {
        if (amount > Balance)
        {
            return false;
        }

        Balance -= amount;
        return true;
    }
}
