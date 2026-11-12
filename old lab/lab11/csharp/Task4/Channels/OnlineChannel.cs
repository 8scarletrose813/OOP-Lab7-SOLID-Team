using PrutCreditBank.StructuralPatterns.Common.Logging;
using PrutCreditBank.StructuralPatterns.Common.Models;
using PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Facades;
using PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Models;

namespace PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Channels;

public sealed class OnlineChannel(AccountOpeningFacade facade, IEventLog log)
{
    public AccountOpeningResult OpenAccount(Client client)
    {
        log.Write(nameof(OnlineChannel), $"Клієнт {client.FullName} подав заявку на відкриття рахунку онлайн");
        return facade.OpenAccount(client);
    }
}
