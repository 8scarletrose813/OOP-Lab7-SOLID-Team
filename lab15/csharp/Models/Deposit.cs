namespace PrutCredit.Models;

public class Deposit : BankProduct
{
    public Deposit(string id, Client owner, decimal principal, decimal annualRatePercent, int months)
        : base(id, owner)
    {
        Principal = principal;
        AnnualRatePercent = annualRatePercent;
        Months = months;
    }

    public decimal Principal { get; }

    public decimal AnnualRatePercent { get; }

    public int Months { get; }

    public decimal AccruedInterest =>
        Math.Round(Principal * AnnualRatePercent / 100m * Months / 12m, 2);

    public override string DisplayName => "Депозит";

    public override decimal Debt => 0m;
}
