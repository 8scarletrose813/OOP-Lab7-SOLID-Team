namespace Lab12_Variant7.Task3_TransactionHistory.Models;

public class Transaction
{
    public Transaction(decimal amount, DateTime date, string account, Merchant merchant)
    {
        Amount = amount;
        Date = date;
        Account = account;
        Merchant = merchant;
    }

    public decimal Amount { get; }
    public DateTime Date { get; }
    public string Account { get; }
    public Merchant Merchant { get; }

    public override string ToString() =>
        $"{Date:dd.MM.yyyy HH:mm} | рахунок {Account} | {Amount:N2} грн | {Merchant}";
}
