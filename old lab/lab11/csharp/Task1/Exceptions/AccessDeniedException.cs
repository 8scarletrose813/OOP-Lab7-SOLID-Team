namespace PrutCreditBank.StructuralPatterns.Task1_CreditBureau.Exceptions;

public sealed class AccessDeniedException(string message) : Exception(message);
