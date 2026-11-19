using Lab12_Variant7.Task1_BankStructure.Interfaces;

namespace Lab12_Variant7.Task1_BankStructure.Models;

public class Cashier : IBankUnit
{
    private readonly decimal _cashBalance;
    private readonly int _operationsCount;

    public Cashier(string name, decimal cashBalance, int operationsCount)
    {
        Name = name;
        _cashBalance = cashBalance;
        _operationsCount = operationsCount;
    }

    public string Name { get; }

    public IReadOnlyList<IBankUnit> Children => Array.Empty<IBankUnit>();

    public decimal GetCashBalance() => _cashBalance;

    public int GetOperationsCount() => _operationsCount;
}
