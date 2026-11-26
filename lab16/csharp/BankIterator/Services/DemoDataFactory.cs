using BankIterator.Models;

namespace BankIterator.Services;

public static class DemoDataFactory
{
    public static Client CreateClient()
    {
        var client = new Client("Коваленко Марія");
        var current = client.OpenAccount("UA-CUR-001", "Поточний");
        var deposit = client.OpenAccount("UA-DEP-002", "Депозитний");
        var card = client.OpenAccount("UA-CRD-003", "Картковий");

        var entries = new (Account Account, DateTime Date, decimal Amount, string Description)[]
        {
            (current, At(2025, 1, 10), 85_000m, "Зарплата, січень"),
            (current, At(2025, 2, 10), 85_000m, "Зарплата, лютий"),
            (current, At(2025, 3, 12), -4_200.75m, "Комунальні послуги"),
            (current, At(2025, 4, 2), 520_000m, "Продаж автомобіля"),
            (current, At(2025, 6, 10), 90_000m, "Зарплата, червень"),
            (current, At(2025, 8, 15), -45_000m, "Оплата навчання"),
            (current, At(2025, 11, 3), -415_000m, "Купівля обладнання"),
            (current, At(2025, 12, 10), 95_000m, "Зарплата, грудень"),
            (current, At(2026, 1, 12), 95_000m, "Зарплата, січень"),
            (current, At(2026, 3, 5), -12_750m, "Страхування"),
            (current, At(2026, 5, 20), -400_000m, "Переказ на інший рахунок"),
            (current, At(2026, 6, 10), 98_000m, "Зарплата, червень"),
            (current, At(2026, 9, 1), -22_000m, "Відпустка"),
            (current, At(2026, 10, 12), 98_000m, "Зарплата, жовтень"),
            (current, At(2026, 11, 10), 98_000m, "Зарплата, листопад"),

            (deposit, At(2025, 3, 1), 300_000m, "Внесок на депозит"),
            (deposit, At(2025, 9, 1), 150_000m, "Додатковий внесок"),
            (deposit, At(2025, 12, 31), 21_450.30m, "Відсотки за 2025 рік"),
            (deposit, At(2026, 2, 2), -200_000m, "Часткове зняття"),
            (deposit, At(2026, 7, 1), 450_000m, "Поповнення депозиту"),
            (deposit, At(2026, 10, 31), 9_800.15m, "Відсотки за жовтень"),

            (card, At(2025, 5, 20), -3_250.40m, "Супермаркет"),
            (card, At(2025, 7, 14), -8_900m, "Авіаквитки"),
            (card, At(2025, 10, 22), -1_560.90m, "Ресторан"),
            (card, At(2026, 2, 14), -2_780m, "Подарунки"),
            (card, At(2026, 4, 18), -6_400.25m, "Електроніка"),
            (card, At(2026, 8, 9), -3_120m, "АЗС"),
            (card, At(2026, 11, 2), -5_890.60m, "Супермаркет"),
            (card, At(2026, 11, 20), -1_250m, "Кав'ярня"),
            (card, At(2026, 11, 24), -740.50m, "Аптека"),
        };

        var counter = 0;
        foreach (var entry in entries)
        {
            entry.Account.Add(new Operation(
                $"OP-{++counter:000}", entry.Account.Number, entry.Date, entry.Amount, entry.Description));
        }

        var archiveCutoff = new DateTime(2026, 1, 1);
        foreach (var account in client.Accounts)
        {
            account.ArchiveOlderThan(archiveCutoff);
        }

        return client;
    }

    private static DateTime At(int year, int month, int day) => new(year, month, day, 10, 0, 0);
}
