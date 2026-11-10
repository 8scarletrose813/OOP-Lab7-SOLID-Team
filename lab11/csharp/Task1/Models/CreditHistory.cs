namespace PrutCreditBank.StructuralPatterns.Task1_CreditBureau.Models;

public sealed record CreditHistory(string ClientId, int Score, int ActiveLoans, bool HasOverdue);
