using PrutCreditBank.StructuralPatterns.Common.Logging;
using PrutCreditBank.StructuralPatterns.Common.Models;
using PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Interfaces;
using PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Models;

namespace PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Subsystems;

public sealed class AccountCreationService(IEventLog log) : IAccountCreationService
{
    private int _sequence = 1;

    public Account CreateAccount(Client client)
    {
        var account = new Account($"UA21305299000000{_sequence++:D11}", client.Id);
        log.Write(nameof(AccountCreationService), $"Створено рахунок {account.Number} для клієнта {client.FullName}");
        return account;
    }
}
