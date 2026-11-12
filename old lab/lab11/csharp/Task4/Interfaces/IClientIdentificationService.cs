using PrutCreditBank.StructuralPatterns.Common.Models;

namespace PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Interfaces;

public interface IClientIdentificationService
{
    bool Identify(Client client);
}
