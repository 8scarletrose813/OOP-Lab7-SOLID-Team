using PrutCredit.Interfaces;
using PrutCredit.Models;

namespace PrutCredit.Services.Closure;

public class CardClosureProcess : ProductClosureProcess<BankCard>
{
    public CardClosureProcess(ILogger logger) : base(logger)
    {
    }

    protected override decimal FinalSettlement(BankCard card)
    {
        Log($"Залишок на картці {card.Balance:F2} грн повернуто клієнту, картку деактивовано");
        return card.Balance;
    }
}
