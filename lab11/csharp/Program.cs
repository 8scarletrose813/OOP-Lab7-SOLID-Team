using PrutCreditBank.StructuralPatterns.Common.Logging;
using PrutCreditBank.StructuralPatterns.Common.Models;
using PrutCreditBank.StructuralPatterns.Common.Time;
using PrutCreditBank.StructuralPatterns.Task1_CreditBureau.Interfaces;
using PrutCreditBank.StructuralPatterns.Task1_CreditBureau.Models;
using PrutCreditBank.StructuralPatterns.Task1_CreditBureau.Services;
using PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Channels;
using PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Facades;
using PrutCreditBank.StructuralPatterns.Task4_AccountOpening.Subsystems;

namespace PrutCreditBank.StructuralPatterns;

public static class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var clock = new SimulatedClock(new DateTime(2026, 11, 9, 9, 0, 0));
        var log = new ConsoleEventLog(clock);

        RunCreditBureauScenario(clock, log);

        Console.WriteLine();

        RunAccountOpeningScenario(log);
    }

    private static void RunCreditBureauScenario(IClock clock, IEventLog log)
    {
        var realBureau = new RealCreditBureauService(log, clock);
        ICreditBureauService bureauProxy = new CreditBureauProxy(realBureau, log, clock);
        var creditModule = new CreditRequestModule(bureauProxy, log);

        var manager = new Employee("Ірина Коваль", EmployeeRole.CreditManager);
        var cashier = new Employee("Олег Мельник", EmployeeRole.Cashier);
        var client = new Client("C-1001", "Петренко Андрій");

        PrintStep("Кредитний менеджер запитує історію клієнта: платний запит до бюро");
        creditModule.RequestHistory(manager, client);

        PrintStep("Той самий менеджер запитує історію ще двічі: результат з кешу");
        creditModule.RequestHistory(manager, client);
        creditModule.RequestHistory(manager, client);
        log.Write("Program", $"Кількість платних запитів до бюро: {realBureau.PaidRequestCount}");

        PrintStep("Касир намагається отримати кредитну історію: відмова");
        creditModule.RequestHistory(cashier, client);

        PrintStep("Наступний робочий день: кеш застарів, новий платний запит");
        clock.Advance(TimeSpan.FromDays(1));
        creditModule.RequestHistory(manager, client);
        log.Write("Program", $"Кількість платних запитів до бюро: {realBureau.PaidRequestCount}");
    }

    private static void RunAccountOpeningScenario(IEventLog log)
    {
        var cardService = new CardIssuingService(log);

        var facade = new AccountOpeningFacade(
            new ClientIdentificationService(log),
            new SanctionsCheckService(log),
            new AccountCreationService(log),
            cardService,
            new OnlineBankingService(log),
            new ContractDispatchService(log),
            log);

        var branch = new BranchChannel(facade, log);
        var online = new OnlineChannel(facade, log);

        var firstClient = new Client("C-2001", "Шевченко Оксана");
        var secondClient = new Client("C-2002", "Бондар Максим");

        PrintStep("Відкриття рахунку у відділенні однією операцією");
        var branchResult = branch.OpenAccount(firstClient);
        log.Write("Program", $"Результат: {branchResult.Message}");

        PrintStep("Відкриття рахунку онлайн тією самою операцією");
        var onlineResult = online.OpenAccount(secondClient);
        log.Write("Program", $"Результат: {onlineResult.Message}");

        PrintStep("Окреме перевипущення картки без відкриття нового рахунку");
        log.Write("Program", $"Запит на перевипуск картки до рахунку {branchResult.Account!.Number}");
        cardService.ReissueCard(branchResult.Account!);
    }

    private static void PrintHeader(string title)
    {
        Console.WriteLine(new string('=', 78));
        Console.WriteLine(title);
        Console.WriteLine(new string('=', 78));
    }

    private static void PrintStep(string title)
    {
        Console.WriteLine();
        Console.WriteLine($"--- {title} ---");
    }
}
