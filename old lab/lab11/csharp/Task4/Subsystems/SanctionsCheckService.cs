using PrutCreditBank.StructuralPatterns.Common.Logging;
using PrutCreditBank.StructuralPatterns.Common.Models;
using PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Interfaces;

namespace PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Subsystems;

public sealed class SanctionsCheckService(IEventLog log) : ISanctionsCheckService
{
    public bool IsClear(Client client)
    {
        log.Write(nameof(SanctionsCheckService), $"Клієнта {client.FullName} у санкційних списках не знайдено");
        return true;
    }
}
