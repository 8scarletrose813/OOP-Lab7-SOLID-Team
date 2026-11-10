using PrutCreditBank.StructuralPatterns.Common.Logging;
using PrutCreditBank.StructuralPatterns.Common.Models;
using PrutCreditBank.StructuralPatterns.Task1_CreditBureau.Exceptions;
using PrutCreditBank.StructuralPatterns.Task1_CreditBureau.Interfaces;
using PrutCreditBank.StructuralPatterns.Task1_CreditBureau.Models;

namespace PrutCreditBank.StructuralPatterns.Task1_CreditBureau.Services;

public sealed class CreditRequestModule(ICreditBureauService bureau, IEventLog log)
{
    public void RequestHistory(Employee employee, Client client)
    {
        log.Write(nameof(CreditRequestModule), $"{employee.Name} ({employee.Role}) запитує кредитну історію клієнта {client.FullName}");

        try
        {
            var history = bureau.GetCreditHistory(employee, client);
            log.Write(nameof(CreditRequestModule), $"Отримано історію: рейтинг {history.Score}, активних кредитів {history.ActiveLoans}, прострочення {(history.HasOverdue ? "є" : "немає")}");
        }
        catch (AccessDeniedException ex)
        {
            log.Write(nameof(CreditRequestModule), $"Відмова: {ex.Message}");
        }
    }
}
