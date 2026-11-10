using PrutCreditBank.StructuralPatterns.Common.Logging;
using PrutCreditBank.StructuralPatterns.Common.Models;
using PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Facades;
using PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Models;

namespace PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Channels;

public sealed class BranchChannel(AccountOpeningFacade facade, IEventLog log)
{
    public AccountOpeningResult OpenAccount(Client client)
    {
        log.Write(nameof(BranchChannel), $"Менеджер відділення відкриває рахунок клієнту {client.FullName}");
        return facade.OpenAccount(client);
    }
}
