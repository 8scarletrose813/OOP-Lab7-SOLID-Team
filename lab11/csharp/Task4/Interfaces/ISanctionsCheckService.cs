using PrutCreditBank.StructuralPatterns.Common.Models;

namespace PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Interfaces;

public interface ISanctionsCheckService
{
    bool IsClear(Client client);
}
