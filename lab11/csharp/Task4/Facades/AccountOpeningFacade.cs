using PrutCreditBank.StructuralPatterns.Common.Logging;
using PrutCreditBank.StructuralPatterns.Common.Models;
using PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Interfaces;
using PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Models;

namespace PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Facades;

public sealed class AccountOpeningFacade(
    IClientIdentificationService identification,
    ISanctionsCheckService sanctions,
    IAccountCreationService accounts,
    ICardIssuingService cards,
    IOnlineBankingService onlineBanking,
    IContractDispatchService contracts,
    IEventLog log)
{
    public AccountOpeningResult OpenAccount(Client client)
    {
        log.Write(nameof(AccountOpeningFacade), $"Початок відкриття рахунку для клієнта {client.FullName}");

        if (!identification.Identify(client))
        {
            return Reject("Ідентифікацію не пройдено");
        }

        if (!sanctions.IsClear(client))
        {
            return Reject("Клієнта знайдено в санкційних списках");
        }

        var account = accounts.CreateAccount(client);
        var card = cards.IssueCard(account);
        onlineBanking.Connect(client, account);
        contracts.SendContract(client, account);

        log.Write(nameof(AccountOpeningFacade), $"Відкриття рахунку {account.Number} завершено");
        return new AccountOpeningResult(true, "Рахунок відкрито", account, card);
    }

    private AccountOpeningResult Reject(string reason)
    {
        log.Write(nameof(AccountOpeningFacade), $"Відкриття рахунку перервано: {reason}");
        return new AccountOpeningResult(false, reason, null, null);
    }
}
