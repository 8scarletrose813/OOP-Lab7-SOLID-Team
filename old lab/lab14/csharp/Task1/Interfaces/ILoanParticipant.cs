namespace PrutCredit.Lab14.Task1.Interfaces;

public interface ILoanParticipant
{
    string Name { get; }
    void SetMediator(ILoanMediator mediator);
}
