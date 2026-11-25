using PrutCredit.Models;

namespace PrutCredit.Interfaces;

public interface ICardState
{
    string Name { get; }

    void Activate(BankCard card);

    void Pay(BankCard card, decimal amount, string pin);

    void Unblock(BankCard card);

    void BlockPermanently(BankCard card);

    void Reissue(BankCard card);

    void Expire(BankCard card);
}
