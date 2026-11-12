using Lab12_Variant7.Task1_BankStructure.Interfaces;

namespace Lab12_Variant7.Task1_BankStructure.Models;

public class BankDivision : IBankUnit
{
    private readonly List<IBankUnit> _children = new();

    public BankDivision(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public IReadOnlyList<IBankUnit> Children => _children;

    public void Add(IBankUnit unit) => _children.Add(unit);

    public decimal GetCashBalance() => _children.Sum(c => c.GetCashBalance());

    public int GetOperationsCount() => _children.Sum(c => c.GetOperationsCount());
}
