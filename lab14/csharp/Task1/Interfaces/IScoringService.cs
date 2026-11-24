using PrutCredit.Lab14.Task1.Model;

namespace PrutCredit.Lab14.Task1.Interfaces;

public interface IScoringService : ILoanParticipant
{
    void Evaluate(LoanApplication application);
}
