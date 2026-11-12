using Lab12_Variant7.Common;
using Lab12_Variant7.Task3_TransactionHistory.Services;

namespace Lab12_Variant7.Task3_TransactionHistory;

public static class Task3Runner
{
    public static void Run()
    {
        Logger.Log("Завдання 3", "Історія операцій (Flyweight)");

        var factory = new MerchantFactory();
        var service = new TransactionHistoryService(factory);

        var history = service.Generate(50_000);

        Logger.Log("Завдання 3", $"Кількість операцій: {history.Count}");
        Logger.Log("Завдання 3", $"Кількість об'єктів торговців: {factory.Count}");

        var silpo = history.Where(t => t.Merchant.Name == "Сільпо").Take(2).ToList();
        Logger.Log("Завдання 3", $"Операція 1: {silpo[0]}");
        Logger.Log("Завдання 3", $"Операція 2: {silpo[1]}");
        Logger.Log("Завдання 3",
            $"Посилаються на той самий об'єкт торговця: {ReferenceEquals(silpo[0].Merchant, silpo[1].Merchant)}");
    }
}
