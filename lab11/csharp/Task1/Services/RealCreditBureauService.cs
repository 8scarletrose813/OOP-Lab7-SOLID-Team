using PrutCreditBank.StructuralPatterns.Common.Logging;
using PrutCreditBank.StructuralPatterns.Common.Models;
using PrutCreditBank.StructuralPatterns.Common.Time;
using PrutCreditBank.StructuralPatterns.Task1_CreditBureau.Interfaces;
using PrutCreditBank.StructuralPatterns.Task1_CreditBureau.Models;

namespace PrutCreditBank.StructuralPatterns.Task1_CreditBureau.Services;

public sealed class RealCreditBureauService(IEventLog log, IClock clock) : ICreditBureauService
{
    private static readonly TimeSpan RequestDuration = TimeSpan.FromSeconds(4);

    public int PaidRequestCount { get; private set; }

    public CreditHistory GetCreditHistory(Employee requester, Client client)
    {
        PaidRequestCount++;
        log.Write(nameof(RealCreditBureauService), $"Платний запит до бюро кредитних історій: клієнт {client.FullName} (запит №{PaidRequestCount})");
        clock.Advance(RequestDuration);
        var history = new CreditHistory(client.Id, 742, 1, false);
        log.Write(nameof(RealCreditBureauService), $"Бюро повернуло історію клієнта {client.FullName}");
        return history;
    }
}
