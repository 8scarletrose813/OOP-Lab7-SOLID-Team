using PrutCreditBank.StructuralPatterns.Common.Logging;
using PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Interfaces;
using PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Models;

namespace PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Subsystems;

public sealed class CardIssuingService(IEventLog log) : ICardIssuingService
{
    private int _sequence = 1;

    public PaymentCard IssueCard(Account account)
    {
        var card = CreateCard(account);
        log.Write(nameof(CardIssuingService), $"Випущено картку {card.MaskedNumber} до рахунку {account.Number}");
        return card;
    }

    public PaymentCard ReissueCard(Account account)
    {
        var card = CreateCard(account);
        log.Write(nameof(CardIssuingService), $"Перевипущено картку {card.MaskedNumber} до рахунку {account.Number}, стару картку заблоковано");
        return card;
    }

    private PaymentCard CreateCard(Account account) =>
        new($"53754141{_sequence++:D8}", account.Number);
}
