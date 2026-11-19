namespace Lab12_Variant7.Task1_BankStructure.Interfaces;

public interface IBankUnit
{
    string Name { get; }
    IReadOnlyList<IBankUnit> Children { get; }
    decimal GetCashBalance();
    int GetOperationsCount();
}
