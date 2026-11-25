using PrutCredit.Interfaces;
using PrutCredit.Models;
using PrutCredit.Services.CardStates;
using PrutCredit.Services.Closure;

namespace PrutCredit.Scenarios;

public class DemoRunner
{
    private const string Source = "Runner";

    private readonly ILogger _logger;

    public DemoRunner(ILogger logger)
    {
        _logger = logger;
    }

    public void RunAll()
    {
        ProductClosure();
        Console.WriteLine();
        CardLifecycle();
    }

    public void ProductClosure()
    {
        Console.WriteLine("=== Завдання 1. Закриття банківських продуктів (Template Method) ===");

        var client = new Client("C-001", "Ковальчук Олена");

        var deposit = new Deposit("DEP-1001", client, 100_000m, 12m, 12);
        var loan = new Loan("LN-2001", client, 50_000m, 25_000m);
        var card = new BankCard("CARD-3001", client, "1234", 1_500m, 0m, _logger, new ActiveState());

        _logger.Log(Source, "Сценарій 1: закриття депозиту з відсотками та податком");
        var depositResult = new DepositClosureProcess(_logger).Close(deposit, client);
        ReportClosure(depositResult, deposit);

        _logger.Log(Source, "Сценарій 2: закриття кредиту клієнта із заборгованістю");
        var loanResult = new LoanClosureProcess(_logger).Close(loan, client);
        ReportClosure(loanResult, loan);

        _logger.Log(Source, "Сценарій 3: закриття картки, довідка в тому ж форматі");
        var cardResult = new CardClosureProcess(_logger).Close(card, client);
        ReportClosure(cardResult, card);
    }

    public void CardLifecycle()
    {
        Console.WriteLine("=== Завдання 2. Стан банківської картки (State) ===");

        var client = new Client("C-001", "Ковальчук Олена");
        var card = new BankCard("CARD-4001", client, "1234", 5_000m, 0m, _logger, new IssuedState());

        _logger.Log(Source, $"Картку {card.Id} випущено, стан: «{card.State.Name}»");

        _logger.Log(Source, "Сценарій 1: платіж до активації, потім активація і платіж");
        card.Pay(200m, "1234");
        card.Activate();
        card.Pay(300m, "1234");

        _logger.Log(Source, "Сценарій 2: тричі невірний PIN, потім платіж з правильним PIN");
        card.Pay(100m, "0000");
        card.Pay(100m, "1111");
        card.Pay(100m, "2222");
        card.Pay(100m, "1234");

        _logger.Log(Source, "Сценарій 3: розблокування, блокування назавжди, повторне розблокування");
        card.Unblock();
        card.BlockPermanently();
        card.Unblock();

        _logger.Log(Source, "Сценарій 4: перевипуск заблокованої назавжди картки");
        card.Reissue();
        card.Activate();
    }

    private void ReportClosure(ClosureResult result, BankProduct product)
    {
        var outcome = result.Success ? "успішно" : "відмова";
        _logger.Log(Source, $"Результат: {outcome}; продукт закрито: {(product.IsClosed ? "так" : "ні")}");
    }
}
