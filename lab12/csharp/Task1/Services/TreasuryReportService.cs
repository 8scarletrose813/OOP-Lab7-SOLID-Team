using Lab12_Variant7.Common;
using Lab12_Variant7.Task1_BankStructure.Interfaces;

namespace Lab12_Variant7.Task1_BankStructure.Services;

public class TreasuryReportService
{
    private const string Source = "Казначейство";

    public void PrintTree(IBankUnit unit, int depth = 0)
    {
        Logger.Log(Source,
            $"{new string(' ', depth * 2)}{unit.Name}: залишок {unit.GetCashBalance():N2} грн, операцій {unit.GetOperationsCount()}");

        foreach (var child in unit.Children)
            PrintTree(child, depth + 1);
    }

    public void PrintSummary(IBankUnit unit)
    {
        Logger.Log(Source,
            $"Звіт '{unit.Name}': залишок {unit.GetCashBalance():N2} грн, операцій за день {unit.GetOperationsCount()}");
    }
}
