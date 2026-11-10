using PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Models;

namespace PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Interfaces;

public interface ICardIssuingService
{
    PaymentCard IssueCard(Account account);

    PaymentCard ReissueCard(Account account);
}
