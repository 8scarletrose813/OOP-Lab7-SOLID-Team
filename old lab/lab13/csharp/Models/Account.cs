namespace Lab13.Task2.Models;

public class Account
{
    public Account(string owner, decimal balance)
    {
        Owner = owner;
        Balance = balance;
    }

    public string Owner { get; }
    public decimal Balance { get; private set; }

    public void Withdraw(decimal amount)
    {
        if (amount > Balance)
        {
            throw new InvalidOperationException(
                $"недостатньо коштів на рахунку «{Owner}»: {Balance:F2} грн, потрібно {amount:F2} грн");
        }

        Balance -= amount;
    }

    public void Deposit(decimal amount) => Balance += amount;
}
