using PrutCredit.Lab14.Task1.Common;
using PrutCredit.Lab14.Task1.Interfaces;

namespace PrutCredit.Lab14.Task1.Services;

public abstract class ParticipantBase : ILoanParticipant
{
    protected ILoanMediator Mediator { get; private set; } = null!;

    public string Name { get; }

    protected ParticipantBase(string name)
    {
        Name = name;
    }

    public void SetMediator(ILoanMediator mediator)
    {
        Mediator = mediator;
    }

    protected void Log(string message)
    {
        EventLog.Write(Name, message);
    }
}
