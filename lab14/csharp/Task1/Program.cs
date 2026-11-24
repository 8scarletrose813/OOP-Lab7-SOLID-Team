using System.Text;
using PrutCredit.Lab14.Task1.Interfaces;
using PrutCredit.Lab14.Task1.Model;
using PrutCredit.Lab14.Task1.Services;

namespace PrutCredit.Lab14.Task1;

public static class Program
{
    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        RunSuccessfulIssuance();
        RunSecurityRejection();
    }

    private static void RunSuccessfulIssuance()
    {
        PrintHeader("Сценарій 1: успішна видача кредиту");

        var manager = BuildSystem();
        manager.Submit(new LoanApplication(1, "Іваненко О.", 250000m, 720, false));
    }

    private static void RunSecurityRejection()
    {
        PrintHeader("Сценарій 2: відмова служби безпеки");

        var manager = BuildSystem();
        manager.Submit(new LoanApplication(2, "Петренко І.", 500000m, 680, true));
    }

    private static ILoanManager BuildSystem()
    {
        var manager = new LoanManager();
        _ = new LoanMediator(manager, new ScoringService(), new SecurityService(), new LegalDepartment(), new Cashier());
        return manager;
    }

    private static void PrintHeader(string title)
    {
        Console.WriteLine();
        Console.WriteLine($"=== {title} ===");
    }
}
