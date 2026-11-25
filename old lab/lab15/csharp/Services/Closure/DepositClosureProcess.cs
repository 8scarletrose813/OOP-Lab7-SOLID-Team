using PrutCredit.Interfaces;
using PrutCredit.Models;

namespace PrutCredit.Services.Closure;

public class DepositClosureProcess : ProductClosureProcess<Deposit>
{
    private const decimal TaxRate = 0.195m;

    public DepositClosureProcess(ILogger logger) : base(logger)
    {
    }

    protected override decimal FinalSettlement(Deposit deposit)
    {
        var interest = deposit.AccruedInterest;
        Log($"Нараховано відсотки: {interest:F2} грн (тіло: {deposit.Principal:F2} грн)");
        return deposit.Principal + interest;
    }

    protected override decimal WithholdTax(Deposit deposit, decimal payout)
    {
        var tax = Math.Round(deposit.AccruedInterest * TaxRate, 2);
        Log($"Утримано податок {TaxRate:P1} з відсотків: {tax:F2} грн");
        return payout - tax;
    }
}
