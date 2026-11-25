using PrutCredit.Lab14.Task1.Interfaces;
using PrutCredit.Lab14.Task1.Model;

namespace PrutCredit.Lab14.Task1.Services;

public class LoanManager : ParticipantBase, ILoanManager
{
    public LoanManager() : base("Кредитний менеджер")
    {
    }

    public void Submit(LoanApplication application)
    {
        Log($"подає заявку #{application.Id} від {application.ClientName} на {application.Amount} грн");
        Mediator.Notify(this, LoanEvent.ApplicationSubmitted, application);
    }

    public void OnRejected(LoanApplication application, string rejectedBy)
    {
        Log($"отримав відмову по заявці #{application.Id}, ініціатор відмови: {rejectedBy}");
    }

    public void OnCompleted(LoanApplication application)
    {
        Log($"отримав підтвердження: кредит по заявці #{application.Id} виданий");
    }
}
