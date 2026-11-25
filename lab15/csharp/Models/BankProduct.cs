namespace PrutCredit.Models;

public abstract class BankProduct
{
    protected BankProduct(string id, Client owner)
    {
        Id = id;
        Owner = owner;
    }

    public string Id { get; }

    public Client Owner { get; }

    public bool IsClosed { get; private set; }

    public abstract string DisplayName { get; }

    public abstract decimal Debt { get; }

    public void MarkClosed() => IsClosed = true;
}
