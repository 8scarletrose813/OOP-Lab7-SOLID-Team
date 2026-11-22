using Lab13.Task2.Commands;
using Lab13.Task2.Models;
using Lab13.Task2.Services;

namespace Lab13.Task2;

public static class DeferredPaymentsDemo
{
    public static void Run()
    {
        var clock = new BankClock(new DateOnly(2026, 11, 20));
        var log = new OperationLog(clock);
        var scheduler = new PaymentScheduler(clock, log);

        var today = clock.Today;
        var client = new Account("Іваненко І.", 20000m);
        var friend = new Account("Петренко О.", 0m);
        var deposit = new Account("Депозит «Накопичувальний»", 5000m);

        var transfer = new TransferPayment(client, friend, 3000m, today.AddDays(1));
        var utility = new UtilityPayment(client, "Чернівціводоканал", "водопостачання", 450m, today.AddDays(2));
        var topUp = new DepositTopUp(client, deposit, 2000m, today.AddDays(3), repeatEveryDays: 30);

        Header("1. Створення трьох платежів на різні дати");
        scheduler.Schedule(transfer);
        scheduler.Schedule(utility);
        scheduler.Schedule(topUp);
        PrintQueue(scheduler);
        PrintBalances(client, friend, deposit);

        Header("2. Скасування комунального платежу до виконання");
        scheduler.Cancel(utility.Id);
        PrintQueue(scheduler);

        Header("3. Перехід до дати виконання решти платежів");
        clock.AdvanceTo(today.AddDays(3));
        scheduler.ProcessDue();
        PrintQueue(scheduler);
        PrintBalances(client, friend, deposit);

        Header("4. Сторнування виконаного переказу в той самий операційний день");
        scheduler.Reverse(transfer.Id);
        PrintBalances(client, friend, deposit);

        Header("5. Спроба сторнувати платіж наступного операційного дня");
        clock.AdvanceTo(today.AddDays(4));
        scheduler.Reverse(topUp.Id);
        PrintBalances(client, friend, deposit);

        Header("Журнал операцій");
        foreach (var entry in log.Entries)
        {
            Console.WriteLine(entry);
        }
    }

    private static void Header(string title)
    {
        Console.WriteLine();
        Console.WriteLine($"=== {title} ===");
    }

    private static void PrintQueue(PaymentScheduler scheduler)
    {
        Console.WriteLine("Черга платежів:");
        if (scheduler.Queue.Count == 0)
        {
            Console.WriteLine("  (порожня)");
            return;
        }

        foreach (var p in scheduler.Queue)
        {
            var repeat = p.RepeatEveryDays is null ? "" : $", регулярний (кожні {p.RepeatEveryDays} дн.)";
            Console.WriteLine($"  {p.ScheduledDate:dd.MM.yyyy} | {p.Title}: {p.Description}{repeat}");
        }
    }

    private static void PrintBalances(params Account[] accounts)
    {
        Console.WriteLine("Залишки:");
        foreach (var a in accounts)
        {
            Console.WriteLine($"  {a.Owner}: {a.Balance:F2} грн");
        }
    }
}
