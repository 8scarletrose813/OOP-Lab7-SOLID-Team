using PrutCredit.Lab14.Task1.Interfaces;
using PrutCredit.Lab14.Task1.Model;

namespace PrutCredit.Lab14.Task1.Services;

public class ScoringService : ParticipantBase, IScoringService
{
    private const int MinScore = 600;

    public ScoringService() : base("Скоринг")
    {
    }

    public void Evaluate(LoanApplication application)
    {
        var passed = application.CreditScore >= MinScore;
        var verdict = passed ? "позитивний" : "негативний";
        Log($"оцінює заявку #{application.Id}: {application.CreditScore} балів, висновок {verdict}");
        Mediator.Notify(this, passed ? LoanEvent.ScoringPassed : LoanEvent.ScoringFailed, application);
    }
}
