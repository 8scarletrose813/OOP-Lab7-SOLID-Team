using PrutCredit.Lab14.Task1.Interfaces;
using PrutCredit.Lab14.Task1.Model;

namespace PrutCredit.Lab14.Task1.Services;

public class SecurityService : ParticipantBase, ISecurityService
{
    public SecurityService() : base("Служба безпеки")
    {
    }

    public void Check(LoanApplication application)
    {
        var passed = !application.IsRisky;
        var verdict = passed ? "ризиків не виявлено, висновок позитивний" : "клієнт у списку ризиків, висновок негативний";
        Log($"перевіряє заявку #{application.Id}: {verdict}");
        Mediator.Notify(this, passed ? LoanEvent.SecurityPassed : LoanEvent.SecurityFailed, application);
    }
}
