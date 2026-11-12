namespace Lab12_Variant7.Task3_TransactionHistory.Models;

public class Merchant
{
    public Merchant(string name, string logo, string category, string mccCode, string address)
    {
        Name = name;
        Logo = logo;
        Category = category;
        MccCode = mccCode;
        Address = address;
    }

    public string Name { get; }
    public string Logo { get; }
    public string Category { get; }
    public string MccCode { get; }
    public string Address { get; }

    public override string ToString() => $"{Name} (MCC {MccCode}, {Category}, {Address})";
}
