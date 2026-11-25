using PrutCredit.Lab14.Task1.Model;

namespace PrutCredit.Lab14.Task1.Interfaces;

public interface ISecurityService : ILoanParticipant
{
    void Check(LoanApplication application);
}
