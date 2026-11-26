namespace BankIterator.Models;

public sealed record Operation(string Id, string AccountNumber, DateTime Date, decimal Amount, string Description)
{
    public override string ToString() =>
        $"{Id} {Date:yyyy-MM-dd HH:mm} {AccountNumber} {Amount,13:N2} грн {Description}";
}
