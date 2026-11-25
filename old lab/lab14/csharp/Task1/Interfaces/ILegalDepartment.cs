using PrutCredit.Lab14.Task1.Model;

namespace PrutCredit.Lab14.Task1.Interfaces;

public interface ILegalDepartment : ILoanParticipant
{
    void PrepareContract(LoanApplication application);
}
