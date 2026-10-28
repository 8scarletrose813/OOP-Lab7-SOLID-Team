using PrutCredit.Task2;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Task2_PaymentConnections();

static void Task2_PaymentConnections()
{
    Logger.Log("Runner", "=== Завдання 2: З'єднання з платіжною системою ===");
    var pool = new PaymentConnectionPool(maxSize: 4);
    var gateway = new PaymentGateway(pool);

    // 7 платежів одночасно: 4 отримують з'єднання, 3 чекають у черзі
    decimal[] amounts = { 15_000, 2_300, 87_500, 1_200, 40_000, 9_990, 310_000 };
    for (int i = 0; i < amounts.Length; i++)
        gateway.Submit(new Payment(1001 + i, $"UA00{i:D4}", amounts[i]));

    // Перший платіж проведено → з'єднання очищується й одразу бере наступний платіж з черги
    gateway.Complete(1001);
    gateway.Complete(1002);
    gateway.Complete(1003);

    // Решта платежів
    gateway.CompleteAll();

    pool.PrintStatistics();
}
