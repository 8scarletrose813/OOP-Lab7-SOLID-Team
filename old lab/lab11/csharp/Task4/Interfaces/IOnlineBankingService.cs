using PrutCreditBank.StructuralPatterns.Common.Models;
using PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Models;

namespace PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Interfaces;

public interface IOnlineBankingService
{
    void Connect(Client client, Account account);
}
