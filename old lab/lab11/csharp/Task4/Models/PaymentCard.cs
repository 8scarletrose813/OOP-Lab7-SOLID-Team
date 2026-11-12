namespace PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Models;

public sealed record PaymentCard(string Number, string AccountNumber)
{
    public string MaskedNumber => $"{Number[..4]} **** **** {Number[^4..]}";
}
