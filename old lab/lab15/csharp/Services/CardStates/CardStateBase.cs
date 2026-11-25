using PrutCredit.Interfaces;
using PrutCredit.Models;

namespace PrutCredit.Services.CardStates;

public abstract class CardStateBase : ICardState
{
    public abstract string Name { get; }

    protected virtual string Hint => string.Empty;

    public virtual void Activate(BankCard card) => Deny(card, "активація");

    public virtual void Pay(BankCard card, decimal amount, string pin) => Deny(card, "платіж");

    public virtual void Unblock(BankCard card) => Deny(card, "розблокування");

    public virtual void BlockPermanently(BankCard card) => Deny(card, "блокування назавжди");

    public virtual void Reissue(BankCard card) => Deny(card, "перевипуск");

    public virtual void Expire(BankCard card) => Deny(card, "завершення терміну дії");

    protected void Log(BankCard card, string message) => card.Logger.Log(GetType().Name, message);

    private void Deny(BankCard card, string operation)
    {
        var hint = string.IsNullOrEmpty(Hint) ? string.Empty : $". {Hint}";
        Log(card, $"Відмова: операція «{operation}» недоступна, картка у стані «{Name}»{hint}");
    }
}
