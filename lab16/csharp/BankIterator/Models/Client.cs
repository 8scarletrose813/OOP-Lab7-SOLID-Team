namespace BankIterator.Models;

public sealed class Client
{
    private readonly List<Account> _accounts = new();

    public Client(string name) => Name = name;

    public string Name { get; }

    public IReadOnlyList<Account> Accounts => _accounts;

    public Account OpenAccount(string number, string title)
    {
        var account = new Account(number, title);
        _accounts.Add(account);
        return account;
    }
}
