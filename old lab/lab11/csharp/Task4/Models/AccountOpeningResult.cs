namespace PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Models;

public sealed record AccountOpeningResult(bool IsSuccess, string Message, Account? Account, PaymentCard? Card);
