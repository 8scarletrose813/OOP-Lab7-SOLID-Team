using PrutCreditBank.StructuralPatterns.Common.Logging;
using PrutCreditBank.StructuralPatterns.Common.Models;
using PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Interfaces;
using PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Models;

namespace PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Subsystems;

public sealed class OnlineBankingService(IEventLog log) : IOnlineBankingService
{
    public void Connect(Client client, Account account) =>
        log.Write(nameof(OnlineBankingService), $"Клієнта {client.FullName} підключено до онлайн-банкінгу, рахунок {account.Number}");
}
