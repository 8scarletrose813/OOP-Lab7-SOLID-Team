using PrutCredit.Lab14.Task1.Model;

namespace PrutCredit.Lab14.Task1.Interfaces;

public interface ILoanManager : ILoanParticipant
{
    void Submit(LoanApplication application);
    void OnRejected(LoanApplication application, string rejectedBy);
    void OnCompleted(LoanApplication application);
}
