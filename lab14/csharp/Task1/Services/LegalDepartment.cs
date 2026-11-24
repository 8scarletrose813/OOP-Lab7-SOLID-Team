using PrutCredit.Lab14.Task1.Interfaces;
using PrutCredit.Lab14.Task1.Model;

namespace PrutCredit.Lab14.Task1.Services;

public class LegalDepartment : ParticipantBase, ILegalDepartment
{
    public LegalDepartment() : base("Юридичний відділ")
    {
    }

    public void PrepareContract(LoanApplication application)
    {
        Log($"готує кредитний договір по заявці #{application.Id}");
        Log($"договір підписано клієнтом {application.ClientName}");
        Mediator.Notify(this, LoanEvent.ContractSigned, application);
    }
}
