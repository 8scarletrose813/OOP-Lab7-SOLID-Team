using PrutCreditBank.StructuralPatterns.Common.Logging;
using PrutCreditBank.StructuralPatterns.Common.Models;
using PrutCreditBank.StructuralPatterns.Common.Time;
using PrutCreditBank.StructuralPatterns.Task1_CreditBureau.Exceptions;
using PrutCreditBank.StructuralPatterns.Task1_CreditBureau.Interfaces;
using PrutCreditBank.StructuralPatterns.Task1_CreditBureau.Models;

namespace PrutCreditBank.StructuralPatterns.Task1_CreditBureau.Services;

public sealed class CreditBureauProxy(ICreditBureauService realService, IEventLog log, IClock clock) : ICreditBureauService
{
    private readonly Dictionary<string, (CreditHistory History, DateOnly Day)> _cache = new();

    public CreditHistory GetCreditHistory(Employee requester, Client client)
    {
        EnsureAccess(requester);

        var today = DateOnly.FromDateTime(clock.Now);

        if (_cache.TryGetValue(client.Id, out var entry) && entry.Day == today)
        {
            log.Write(nameof(CreditBureauProxy), $"Історія клієнта {client.FullName} з кешу, звернення до бюро не потрібне");
            return entry.History;
        }

        log.Write(nameof(CreditBureauProxy), $"Кеш порожній або застарів для клієнта {client.FullName}, запит передано бюро");
        var history = realService.GetCreditHistory(requester, client);
        _cache[client.Id] = (history, today);
        return history;
    }

    private void EnsureAccess(Employee requester)
    {
        if (requester.Role != EmployeeRole.CreditManager)
        {
            log.Write(nameof(CreditBureauProxy), $"Доступ заборонено: {requester.Name} ({requester.Role}), запит до бюро не виконується");
            throw new AccessDeniedException("Недостатньо прав");
        }

        log.Write(nameof(CreditBureauProxy), $"Доступ дозволено: {requester.Name} ({requester.Role})");
    }
}
