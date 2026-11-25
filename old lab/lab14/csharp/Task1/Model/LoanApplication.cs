namespace PrutCredit.Lab14.Task1.Model;

public class LoanApplication
{
    public int Id { get; }
    public string ClientName { get; }
    public decimal Amount { get; }
    public int CreditScore { get; }
    public bool IsRisky { get; }

    public LoanStatus Status { get; set; } = LoanStatus.Submitted;
    public bool? ScoringApproved { get; set; }
    public bool? SecurityApproved { get; set; }

    public LoanApplication(int id, string clientName, decimal amount, int creditScore, bool isRisky)
    {
        Id = id;
        ClientName = clientName;
        Amount = amount;
        CreditScore = creditScore;
        IsRisky = isRisky;
    }
}
