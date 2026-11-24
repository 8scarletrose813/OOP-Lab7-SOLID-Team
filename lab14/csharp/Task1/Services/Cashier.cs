using PrutCredit.Lab14.Task1.Interfaces;
using PrutCredit.Lab14.Task1.Model;

namespace PrutCredit.Lab14.Task1.Services;

public class Cashier : ParticipantBase, ICashier
{
    public Cashier() : base("Касир")
    {
    }

    public void IssueFunds(LoanApplication application)
    {
        Log($"видає {application.Amount} грн клієнту {application.ClientName}");
        Mediator.Notify(this, LoanEvent.FundsIssued, application);
    }
}
