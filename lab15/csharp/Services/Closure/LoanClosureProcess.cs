using PrutCredit.Interfaces;
using PrutCredit.Models;

namespace PrutCredit.Services.Closure;

public class LoanClosureProcess : ProductClosureProcess<Loan>
{
    public LoanClosureProcess(ILogger logger) : base(logger)
    {
    }

    protected override decimal FinalSettlement(Loan loan)
    {
        Log($"Кредитний договір на {loan.Principal:F2} грн погашено, заставу знято");
        return 0m;
    }
}
