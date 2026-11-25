using PrutCredit.Lab14.Task1.Model;

namespace PrutCredit.Lab14.Task1.Interfaces;

public interface ILoanMediator
{
    void Notify(ILoanParticipant sender, LoanEvent loanEvent, LoanApplication application);
}
