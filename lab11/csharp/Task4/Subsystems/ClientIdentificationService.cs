using PrutCreditBank.StructuralPatterns.Common.Logging;
using PrutCreditBank.StructuralPatterns.Common.Models;
using PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Interfaces;

namespace PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Subsystems;

public sealed class ClientIdentificationService(IEventLog log) : IClientIdentificationService
{
    public bool Identify(Client client)
    {
        log.Write(nameof(ClientIdentificationService), $"Клієнта {client.FullName} ідентифіковано");
        return true;
    }
}
