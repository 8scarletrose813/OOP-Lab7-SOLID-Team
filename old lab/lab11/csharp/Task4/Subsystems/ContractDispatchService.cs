using PrutCreditBank.StructuralPatterns.Common.Logging;
using PrutCreditBank.StructuralPatterns.Common.Models;
using PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Interfaces;
using PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Models;

namespace PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Subsystems;

public sealed class ContractDispatchService(IEventLog log) : IContractDispatchService
{
    public void SendContract(Client client, Account account) =>
        log.Write(nameof(ContractDispatchService), $"Договір по рахунку {account.Number} відправлено клієнту {client.FullName}");
}
