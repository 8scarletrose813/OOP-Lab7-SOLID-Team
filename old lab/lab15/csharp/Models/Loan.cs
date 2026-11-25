namespace PrutCredit.Models;

public class Loan : BankProduct
{
    private readonly decimal _debt;

    public Loan(string id, Client owner, decimal principal, decimal debt)
        : base(id, owner)
    {
        Principal = principal;
        _debt = debt;
    }

    public decimal Principal { get; }

    public override string DisplayName => "Кредит";

    public override decimal Debt => _debt;
}
